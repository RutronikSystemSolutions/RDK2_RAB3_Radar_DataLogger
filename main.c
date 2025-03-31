/******************************************************************************
* File Name:   main.c
*
* Description: This is the source code for the RutDevKit-USB_CDC_Test
*              Application for ModusToolbox.
*
* Related Document: See README.md
*
*
*  Created on: 2021-06-01
*  Company: Rutronik Elektronische Bauelemente GmbH
*  Address: Jonavos g. 30, Kaunas 44262, Lithuania
*  Author: GDR
*
*******************************************************************************
* (c) 2019-2021, Cypress Semiconductor Corporation. All rights reserved.
*******************************************************************************
* This software, including source code, documentation and related materials
* ("Software"), is owned by Cypress Semiconductor Corporation or one of its
* subsidiaries ("Cypress") and is protected by and subject to worldwide patent
* protection (United States and foreign), United States copyright laws and
* international treaty provisions. Therefore, you may use this Software only
* as provided in the license agreement accompanying the software package from
* which you obtained this Software ("EULA").
*
* If no EULA applies, Cypress hereby grants you a personal, non-exclusive,
* non-transferable license to copy, modify, and compile the Software source
* code solely for use in connection with Cypress's integrated circuit products.
* Any reproduction, modification, translation, compilation, or representation
* of this Software except as specified above is prohibited without the express
* written permission of Cypress.
*
* Disclaimer: THIS SOFTWARE IS PROVIDED AS-IS, WITH NO WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, NONINFRINGEMENT, IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. Cypress
* reserves the right to make changes to the Software without notice. Cypress
* does not assume any liability arising out of the application or use of the
* Software or any product or circuit described in the Software. Cypress does
* not authorize its products for use in any products where a malfunction or
* failure of the Cypress product may reasonably be expected to result in
* significant property damage, injury or death ("High Risk Product"). By
* including Cypress's product in a High Risk Product, the manufacturer of such
* system or application assumes all risk of such use and in doing so agrees to
* indemnify Cypress against all liability.
*
* Rutronik Elektronische Bauelemente GmbH Disclaimer: The evaluation board
* including the software is for testing purposes only and,
* because it has limited functions and limited resilience, is not suitable
* for permanent use under real conditions. If the evaluation board is
* nevertheless used under real conditions, this is done at one’s responsibility;
* any liability of Rutronik is insofar excluded
*******************************************************************************/

#include "cy_pdl.h"
#include "cyhal.h"
#include "cybsp.h"
#include "cycfg.h"
#include "cy_retarget_io.h"

#include "bgt60tr13c.h"

#include <stdio.h>
#include <stdlib.h>

#include "custom_alloc.h"

#define LOG_TO_SD_CARD
#define SEND_DATA_TO_GUI

#ifdef LOG_TO_SD_CARD
#include "ff.h"
#include "diskio.h"
#endif

void handle_error(void);

#ifdef LOG_TO_SD_CARD
/**
 * @brief Those 3 variables must be global -> needs to exist as long as the file system is mounted
 */
FATFS  SDFatFS;
TCHAR SDPath = DEV_SD;
FIL SDFile;

static int init_sd_card()
{
	FRESULT fs_result;

	fs_result = f_mount(&SDFatFS, &SDPath, 1);
	if (fs_result == FR_OK)
	{
		printf("Mount was successful\r\n");

		FILINFO fno;
		const char *fname = "file.txt";
		fs_result = f_stat(fname, &fno);
		switch(fs_result)
		{
		case FR_OK:
			printf("%s exists\r\n", fname);
			break;
		case FR_NO_FILE:
		case FR_NO_PATH:
			printf("%s DOES NOT exists\r\n", fname);
			break;
		default:
			printf("Error: %d \r\n", fs_result);
		}
	}
	else
	{
		printf("Cannot mount SD card\r\n");
		return -1;
	}

	return 0;
}

#define NAME_LEN 32

static int open_next_file()
{
	char file_name[NAME_LEN] = {0};
	for(uint16_t i = 0; i < 10; ++i)
	{
		sprintf(file_name, "radar_%d.dat", i);
		printf("Check file: %s \r\n", file_name);

		FILINFO fno;
		FRESULT fs_result;

		fs_result = f_stat(file_name, &fno);
		switch(fs_result)
		{
		case FR_OK:
			printf("%s exists\r\n", file_name);
			break;
		case FR_NO_FILE:
		case FR_NO_PATH:
			printf("%s DOES NOT exists, create it\r\n", file_name);

			// Create the file
			fs_result = f_open(&SDFile, file_name, FA_WRITE | FA_CREATE_ALWAYS);
			if (fs_result == FR_OK)
			{
				printf("%s created\r\n", file_name);
				// Call f_sync() to minimize critical section
				f_sync(&SDFile);
			}
			else
			{
				printf("Error :-(\r\n");
			}

			return 0;
		default:
			printf("Error: %d \r\n", fs_result);
		}
	}
	return 0;
}

#endif

#ifdef SEND_DATA_TO_GUI
/**
 * Send a packet over uart
 * /!\ UART FIFO size if 128 -> That function also works for big data packet
 */
static int send_over_uart(uint8_t* buffer, size_t len)
{
	size_t remaining = len;
	size_t write_index = 0;

	for(;;)
	{
		size_t transmit = remaining;

		if (remaining == 0) break;

		cyhal_uart_write(&cy_retarget_io_uart_obj, &(buffer[write_index]), &transmit);

		write_index += transmit;
		remaining -= transmit;
	}
	return 0;
}
#endif

/*******************************************************************************
* Function Name: main
********************************************************************************
* Summary:
*  This is the main function for CM4 CPU. It initializes the USB Device block
*  and enumerates as a CDC device. It constantly checks for data received from
*  host and echos it back.
*
* Parameters:
*  void
*
* Return:
*  int
*
*******************************************************************************/
int main(void)
{
    cy_rslt_t result = CY_RSLT_SUCCESS;
    uint16_t * samples = NULL;
    int retval = 0;
    uint8_t sd_card_on = 0;

    /* Initialize the device and board peripherals */
    result = cybsp_init() ;
    if (result != CY_RSLT_SUCCESS)
    {
        CY_ASSERT(0);
    }

    /* Enable global interrupts */
    __enable_irq();

    /*Enable debug output via KitProg UART*/
	result = cy_retarget_io_init( KITPROG_TX, KITPROG_RX, CY_RETARGET_IO_BAUDRATE);
	if (result != CY_RSLT_SUCCESS)
	{
		handle_error();
	}

	printf("***********************************\r\n");
	printf("\tRDK2 - RAB3 - Level sensing with DataLogger r\n");
	printf("***********************************\r\n");

    /* init buttons */
    result = cyhal_gpio_init(USER_BTN1, CYHAL_GPIO_DIR_INPUT, CYHAL_GPIO_DRIVE_NONE, false);
	if (result != CY_RSLT_SUCCESS)
	{CY_ASSERT(0);}

    /*Initialize LEDs*/
    result = cyhal_gpio_init( LED1, CYHAL_GPIO_DIR_OUTPUT, CYHAL_GPIO_DRIVE_STRONG, CYBSP_LED_STATE_OFF);
    if (result != CY_RSLT_SUCCESS)
    {handle_error();}
    result = cyhal_gpio_init( LED2, CYHAL_GPIO_DIR_OUTPUT, CYHAL_GPIO_DRIVE_STRONG, CYBSP_LED_STATE_OFF);
    if (result != CY_RSLT_SUCCESS)
    {handle_error();}

#ifdef LOG_TO_SD_CARD
    if (init_sd_card() != 0)
    {
    	printf("Cannot init sd card\r\n");
    	cyhal_gpio_write(LED1, CYBSP_LED_STATE_OFF);
    	sd_card_on = 0;
    }
    else
	{
    	cyhal_gpio_write(LED1, CYBSP_LED_STATE_ON);
    	sd_card_on = 1;
	}

    if (sd_card_on)
    {
		if ( open_next_file() != 0)
		{
			printf("Cannot open log file\r\n");
			cyhal_gpio_write(LED1, CYBSP_LED_STATE_OFF);
			sd_card_on = 0;
		}
    }

#endif

    // Init samples
    samples = custom_malloc(bgt60tr13c_get_samples_per_frame() * sizeof(uint16_t));

    // Start frame generation
    if (bgt60tr13c_init() != 0)
    {
    	cyhal_gpio_write(LED1, CYBSP_LED_STATE_ON);
    	cyhal_gpio_write(LED2, CYBSP_LED_STATE_ON);
    	for(;;){}
    }

    cyhal_gpio_write(LED2, CYBSP_LED_STATE_OFF);


    for(;;)
    {
    	// Read and send over USB
    	if (bgt60tr13c_is_data_available())
    	{
    		retval = bgt60tr13c_get_data(samples);
    		if (retval == 0)
    		{
    			if (sd_card_on)
    			{
					uint16_t remaining_size = bgt60tr13c_get_samples_per_frame() * sizeof(uint16_t);
					uint8_t* write_buffer = (uint8_t*) samples;
#ifdef LOG_TO_SD_CARD
					uint16_t write_index = 0;
					for(;;)
					{
						UINT bwritten = 0;
						FRESULT fs_result = f_write(&SDFile, &write_buffer[write_index], remaining_size, &bwritten);
						if (fs_result != FR_OK)
						{
							f_close(&SDFile);
							cyhal_gpio_write(LED1, CYBSP_LED_STATE_OFF);
							sd_card_on = 0;
						}
						f_sync(&SDFile);

						write_index += bwritten;
						remaining_size -= bwritten;

						if (remaining_size == 0)
							break;
					}
    			}
#endif

#ifdef SEND_DATA_TO_GUI
    			uint16_t remaining_size = bgt60tr13c_get_samples_per_frame() * sizeof(uint16_t);
    			uint8_t* write_buffer = (uint8_t*) samples;

    			if (send_over_uart(write_buffer, remaining_size) != 0)
    			{
    				printf("Error while sending to GUI.\r\n");
    			}
#endif
    		}
    		cyhal_gpio_toggle(LED2);
    	}
    }
}

void handle_error(void)
{
     /* Disable all interrupts. */
    __disable_irq();
    cyhal_gpio_write(LED1, CYBSP_LED_STATE_OFF);
    cyhal_gpio_write(LED2, CYBSP_LED_STATE_ON);
    CY_ASSERT(0);
}

/* [] END OF FILE */
