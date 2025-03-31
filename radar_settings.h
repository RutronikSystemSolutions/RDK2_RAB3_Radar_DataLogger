#ifndef XENSIV_BGT60TRXX_CONF_H
#define XENSIV_BGT60TRXX_CONF_H


/**
 * Content of the JSON file
 *
 {
    "device_config": {
        "fmcw_single_shape": {
            "aaf_cutoff_Hz": 500000,
            "chirp_repetition_time_s": 0.0005911249900236726,
            "end_frequency_Hz": 61500000000,
            "frame_repetition_time_s": 5,
            "hp_cutoff_Hz": 80000,
            "if_gain_dB": 43,
            "mimo_mode": "off",
            "num_chirps_per_frame": 32,
            "num_samples_per_chirp": 256,
            "rx_antennas": [
                2
            ],
            "sample_rate_Hz": 2000000,
            "start_frequency_Hz": 60000000000,
            "tx_antennas": [
                1
            ],
            "tx_power_level": 31
        }
    },
    "device_info": {
        "baseboard_name": "Radar Baseboard MCU7 (MCU A)",
        "device_name": "BGT60TR13C",
        "firmware_version": "2.5.5"
    },
    "json_version": {
        "type": "BGT60TR13C_Standard",
        "version": "4.0.0"
    }
}
 */

#define XENSIV_BGT60TRXX_CONF_DEVICE (XENSIV_DEVICE_BGT60TR13C)
#define XENSIV_BGT60TRXX_CONF_START_FREQ_HZ (60000000000)
#define XENSIV_BGT60TRXX_CONF_END_FREQ_HZ (61500000000)
#define XENSIV_BGT60TRXX_CONF_NUM_SAMPLES_PER_CHIRP (256)
#define XENSIV_BGT60TRXX_CONF_NUM_CHIRPS_PER_FRAME (32)
#define XENSIV_BGT60TRXX_CONF_NUM_RX_ANTENNAS (1)
#define XENSIV_BGT60TRXX_CONF_NUM_TX_ANTENNAS (1)
#define XENSIV_BGT60TRXX_CONF_SAMPLE_RATE (2000000)
#define XENSIV_BGT60TRXX_CONF_CHIRP_REPETITION_TIME_S (0.000591112)
#define XENSIV_BGT60TRXX_CONF_FRAME_REPETITION_TIME_S (5.00025)
#define XENSIV_BGT60TRXX_CONF_NUM_REGS (38)

#if defined(XENSIV_BGT60TRXX_CONF_IMPL)
const uint32_t register_list[] = {
    0x11e8270UL,
    0x30a0210UL,
    0x9e967fdUL,
    0xb0805b4UL,
    0xd102fffUL,
    0xf010f00UL,
    0x11000000UL,
    0x13000000UL,
    0x15000000UL,
    0x17000be0UL,
    0x19000000UL,
    0x1b000000UL,
    0x1d000000UL,
    0x1f000b60UL,
    0x2110c851UL,
    0x232ff41fUL,
    0x25705ef7UL,
    0x2d000490UL,
    0x3b000480UL,
    0x49000480UL,
    0x57000480UL,
    0x5911be0eUL,
    0x5b95f40aUL,
    0x5d01f000UL,
    0x5f787e1eUL,
    0x61db76e4UL,
    0x630000eaUL,
    0x65000532UL,
    0x67000100UL,
    0x69000000UL,
    0x6b000000UL,
    0x6d000000UL,
    0x6f31fb10UL,
    0x7f000100UL,
    0x8f000100UL,
    0x9f000100UL,
    0xad000000UL,
    0xb7000000UL,
};

#endif /* XENSIV_BGT60TRXX_CONF_IMPL */

#endif /* XENSIV_BGT60TRXX_CONF_H */
