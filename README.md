# RDK2 RAB3-Radar DataLogger

This code example demonstrates how to implement a data-logger on a PSoC62 for use with a BGT60TR13C radar sensor from Infineon.

The data are logged on the SD-card.

<img src="pictures/rdk2_rab3.png" style="zoom:25%;" />

## Requirements

- [ModusToolbox® software](https://www.infineon.com/cms/en/design-support/tools/sdk/modustoolbox-software/) **v3.x** [built with **v3.3**]
- [RAB3-Radar](https://www.rutronik24.com/product/rutronik/rab3radar/23169671.html)
- [RDK2](https://www.rutronik24.fr/produit/rutronik/rdk2/16440182.html)
- A SD card formatted in FATFS

## Supported toolchains (make variable 'TOOLCHAIN')

- GNU Arm&reg; Embedded Compiler v11.3.1 (`GCC_ARM`) - Default value of `TOOLCHAIN`

## Using the code example with a ModusToolbox™ IDE:

The example can be directly imported inside Modus Toolbox by doing:
1) File -> New -> Modus Toolbox Application
2) PSoC 6 BSPs -> RDK2
3) Sensing -> RDK2 RAB3 Radar Data Logger

A new project will be created inside your workspace.

## Operation

Plug a USB cable into the Kit Prog3 USB connector. The sensor values will be stored on a SD card in binary format.

At each start, the software will create a new file named "radar_XX.dat", with XX a number that is incremented at each start.

You can also visualize the data by using the visualisation gui (see the gui directory).

## Change the radar configuration
You can change the radar configuration used to measure by generating a new "radar_settings.h" configuration.

Use the Infineon “Radar Fusion GUI” tool to generate a new version of the file.

## Status of the LED

Red LED: will be toggled after each radar acquisition

Green LED: turned OFF if the SD card is not detected, turned ON if SD card has been detected

## Libraries

The project contains a local copy of the sensor-xensiv-bgt60trxx.
Modifications have been made inside the file xensiv_bgt60trxx_mtb.c to detect timeout during SPI transfers.

## Legal Disclaimer

The evaluation board including the software is for testing purposes only and, because it has limited functions and limited resilience, is not suitable for permanent use under real conditions. If the evaluation board is nevertheless used under real conditions, this is done at one’s responsibility; any liability of Rutronik is insofar excluded. 

<img src="pictures/rutronik.png" style="zoom:50%;" />



