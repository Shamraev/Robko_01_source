EESchema Schematic File Version 4
LIBS:robko_PCB_arduino_shield-cache
EELAYER 29 0
EELAYER END
$Descr A3 16535 11693
encoding utf-8
Sheet 1 1
Title ""
Date ""
Rev ""
Comp ""
Comment1 ""
Comment2 ""
Comment3 ""
Comment4 ""
$EndDescr
$Comp
L Connector:Conn_01x13_Male J2
U 1 1 5D553446
P 12800 4850
F 0 "J2" H 12908 5631 50  0000 C CNN
F 1 "Conn_01x13_Male" H 12908 5540 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x13_P2.54mm_Vertical" H 12800 4850 50  0001 C CNN
F 3 "~" H 12800 4850 50  0001 C CNN
	1    12800 4850
	1    0    0    -1  
$EndComp
$Comp
L Connector:Conn_01x13_Male J4
U 1 1 5D556270
P 15000 4800
F 0 "J4" H 15108 5581 50  0000 C CNN
F 1 "Conn_01x13_Male" H 15108 5490 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x13_P2.54mm_Vertical" H 15000 4800 50  0001 C CNN
F 3 "~" H 15000 4800 50  0001 C CNN
	1    15000 4800
	1    0    0    -1  
$EndComp
$Comp
L sb-cnc-shield-rescue:POLOLU_STEPPER_DRIVER U1
U 1 1 5D5530B8
P 6550 2900
F 0 "U1" H 6550 3547 60  0000 C CNN
F 1 "POLOLU_STEPPER_DRIVER" H 6550 3441 60  0001 C CNN
F 2 "sb-cnc-shield-master:a4988_shield" H 6550 2900 60  0001 C CNN
F 3 "" H 6550 2900 60  0000 C CNN
	1    6550 2900
	1    0    0    -1  
$EndComp
$Comp
L Connector:Barrel_Jack_Switch J1
U 1 1 5D55AB20
P 2600 7900
F 0 "J1" H 2657 8217 50  0000 C CNN
F 1 "Barrel_Jack_Switch" H 2657 8126 50  0000 C CNN
F 2 "Connector_BarrelJack:BarrelJack_CUI_PJ-102AH_Horizontal" H 2650 7860 50  0001 C CNN
F 3 "~" H 2650 7860 50  0001 C CNN
	1    2600 7900
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR0101
U 1 1 5D55C595
P 3050 8000
F 0 "#PWR0101" H 3050 7750 50  0001 C CNN
F 1 "GNDA" H 3055 7827 50  0000 C CNN
F 2 "" H 3050 8000 50  0001 C CNN
F 3 "" H 3050 8000 50  0001 C CNN
	1    3050 8000
	1    0    0    -1  
$EndComp
$Comp
L sb-cnc-shield-rescue:VIN #PWR0102
U 1 1 5D55DE3C
P 3900 7800
F 0 "#PWR0102" H 3900 7650 50  0001 C CNN
F 1 "VIN" H 3915 7973 50  0000 C CNN
F 2 "" H 3900 7800 60  0000 C CNN
F 3 "" H 3900 7800 60  0000 C CNN
	1    3900 7800
	1    0    0    -1  
$EndComp
Wire Wire Line
	2900 8000 3050 8000
Wire Wire Line
	2900 7900 3050 7900
Wire Wire Line
	3050 7900 3050 8000
Connection ~ 3050 8000
$Comp
L Device:Jumper_NO_Small JP1
U 1 1 5D56626F
P 5600 2600
F 0 "JP1" H 5600 2785 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 2694 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 2600 50  0001 C CNN
F 3 "~" H 5600 2600 50  0001 C CNN
	1    5600 2600
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP2
U 1 1 5D566F49
P 5600 2700
F 0 "JP2" H 5600 2885 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 2794 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 2700 50  0001 C CNN
F 3 "~" H 5600 2700 50  0001 C CNN
	1    5600 2700
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP3
U 1 1 5D5673F9
P 5600 2800
F 0 "JP3" H 5600 2985 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 2894 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 2800 50  0001 C CNN
F 3 "~" H 5600 2800 50  0001 C CNN
	1    5600 2800
	1    0    0    -1  
$EndComp
$Comp
L power:GND #PWR0103
U 1 1 5D55B264
P 1750 2900
F 0 "#PWR0103" H 1750 2650 50  0001 C CNN
F 1 "GND" H 1755 2727 50  0000 C CNN
F 2 "" H 1750 2900 50  0001 C CNN
F 3 "" H 1750 2900 50  0001 C CNN
	1    1750 2900
	1    0    0    -1  
$EndComp
$Comp
L power:GND #PWR0104
U 1 1 5D55C21B
P 4200 2200
F 0 "#PWR0104" H 4200 1950 50  0001 C CNN
F 1 "GND" H 4205 2027 50  0000 C CNN
F 2 "" H 4200 2200 50  0001 C CNN
F 3 "" H 4200 2200 50  0001 C CNN
	1    4200 2200
	1    0    0    -1  
$EndComp
Wire Wire Line
	3900 2200 4200 2200
Wire Wire Line
	2000 2800 1750 2800
Wire Wire Line
	1750 2800 1750 2900
Wire Wire Line
	2000 2900 1750 2900
Connection ~ 1750 2900
$Comp
L power:+5V #PWR0105
U 1 1 5D55F6CF
P 1750 2700
F 0 "#PWR0105" H 1750 2550 50  0001 C CNN
F 1 "+5V" H 1765 2873 50  0000 C CNN
F 2 "" H 1750 2700 50  0001 C CNN
F 3 "" H 1750 2700 50  0001 C CNN
	1    1750 2700
	1    0    0    -1  
$EndComp
Wire Wire Line
	2000 2700 1750 2700
Wire Wire Line
	2900 7800 3150 7800
Wire Wire Line
	3750 7800 3900 7800
$Comp
L power:+5V #PWR0106
U 1 1 5D574656
P 5150 2450
F 0 "#PWR0106" H 5150 2300 50  0001 C CNN
F 1 "+5V" H 5165 2623 50  0000 C CNN
F 2 "" H 5150 2450 50  0001 C CNN
F 3 "" H 5150 2450 50  0001 C CNN
	1    5150 2450
	1    0    0    -1  
$EndComp
Wire Wire Line
	5150 2450 5150 2600
Wire Wire Line
	5150 2600 5500 2600
Wire Wire Line
	5150 2600 5150 2700
Wire Wire Line
	5150 2700 5500 2700
Connection ~ 5150 2600
Wire Wire Line
	5150 2700 5150 2800
Wire Wire Line
	5150 2800 5500 2800
Connection ~ 5150 2700
Wire Wire Line
	5700 2600 6100 2600
Wire Wire Line
	5700 2700 6100 2700
Wire Wire Line
	5700 2800 6100 2800
Text GLabel 5950 2500 0    50   Input ~ 0
notEn
$Comp
L power:GND #PWR0107
U 1 1 5D579072
P 1800 5150
F 0 "#PWR0107" H 1800 4900 50  0001 C CNN
F 1 "GND" H 1805 4977 50  0000 C CNN
F 2 "" H 1800 5150 50  0001 C CNN
F 3 "" H 1800 5150 50  0001 C CNN
	1    1800 5150
	1    0    0    -1  
$EndComp
Wire Wire Line
	2000 5050 1800 5050
Wire Wire Line
	1800 5050 1800 5150
Wire Wire Line
	2000 5150 1800 5150
Connection ~ 1800 5150
Text GLabel 4000 6050 2    50   Input ~ 0
notEn
$Comp
L sb-cnc-shield-rescue:VIN #PWR0108
U 1 1 5D57BFF0
P 8100 2300
F 0 "#PWR0108" H 8100 2150 50  0001 C CNN
F 1 "VIN" H 8115 2473 50  0000 C CNN
F 2 "" H 8100 2300 60  0000 C CNN
F 3 "" H 8100 2300 60  0000 C CNN
	1    8100 2300
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0109
U 1 1 5D580E50
P 7450 3100
F 0 "#PWR0109" H 7450 2950 50  0001 C CNN
F 1 "+5V" V 7465 3228 50  0000 L CNN
F 2 "" H 7450 3100 50  0001 C CNN
F 3 "" H 7450 3100 50  0001 C CNN
	1    7450 3100
	0    1    1    0   
$EndComp
Wire Wire Line
	7000 3100 7450 3100
$Comp
L power:GND #PWR0110
U 1 1 5D58418C
P 7150 3300
F 0 "#PWR0110" H 7150 3050 50  0001 C CNN
F 1 "GND" H 7155 3127 50  0000 C CNN
F 2 "" H 7150 3300 50  0001 C CNN
F 3 "" H 7150 3300 50  0001 C CNN
	1    7150 3300
	1    0    0    -1  
$EndComp
Wire Wire Line
	6100 2900 6000 2900
Wire Wire Line
	6000 2900 6000 3000
Wire Wire Line
	6000 3000 6100 3000
Text GLabel 6000 3100 0    50   Input ~ 0
M1_STEP
Text GLabel 6000 3200 0    50   Input ~ 0
M1_DIR
Wire Wire Line
	5950 2500 6100 2500
Wire Wire Line
	7000 3200 7150 3200
Wire Wire Line
	7150 3200 7150 3300
Wire Wire Line
	6000 3100 6100 3100
Wire Wire Line
	6000 3200 6100 3200
$Comp
L power:GNDA #PWR0111
U 1 1 5D57ED6D
P 8100 2600
F 0 "#PWR0111" H 8100 2350 50  0001 C CNN
F 1 "GNDA" H 8105 2427 50  0000 C CNN
F 2 "" H 8100 2600 50  0001 C CNN
F 3 "" H 8100 2600 50  0001 C CNN
	1    8100 2600
	1    0    0    1   
$EndComp
Wire Wire Line
	7000 2600 7500 2600
Text GLabel 7200 2700 2    50   Input ~ 0
M1_1
Text GLabel 7200 2800 2    50   Input ~ 0
M1_2
Text GLabel 7200 2900 2    50   Input ~ 0
M1_3
Text GLabel 7200 3000 2    50   Input ~ 0
M1_4
Wire Wire Line
	7000 2700 7200 2700
Wire Wire Line
	7000 2800 7200 2800
Wire Wire Line
	7000 2900 7200 2900
Wire Wire Line
	7000 3000 7200 3000
Text GLabel 2000 5300 0    50   Input ~ 0
M1_STEP
Text GLabel 2000 5500 0    50   Input ~ 0
M1_DIR
Wire Wire Line
	2000 5950 1900 5950
Wire Wire Line
	2000 5750 1900 5750
Text GLabel 13150 4250 2    50   Input ~ 0
M1_1
Text GLabel 13150 4350 2    50   Input ~ 0
M1_2
Text GLabel 13150 4450 2    50   Input ~ 0
M1_3
Text GLabel 13150 4550 2    50   Input ~ 0
M1_4
Wire Wire Line
	13000 4550 13150 4550
Wire Wire Line
	13000 4450 13150 4450
Wire Wire Line
	13000 4350 13150 4350
Wire Wire Line
	13000 4250 13150 4250
Text GLabel 13450 4650 2    50   Input ~ 0
M2_1
Text GLabel 13450 4750 2    50   Input ~ 0
M2_2
Text GLabel 13450 4850 2    50   Input ~ 0
M2_3
Text GLabel 13450 4950 2    50   Input ~ 0
M2_4
Wire Wire Line
	13000 4650 13450 4650
Wire Wire Line
	13000 4750 13450 4750
Wire Wire Line
	13000 4850 13450 4850
Wire Wire Line
	13000 4950 13450 4950
$Comp
L Device:CP C1
U 1 1 5D5AF314
P 7500 2450
F 0 "C1" H 7618 2496 50  0000 L CNN
F 1 "100uF" H 7618 2405 50  0000 L CNN
F 2 "Capacitor_THT:CP_Radial_D10.0mm_P3.50mm" H 7538 2300 50  0001 C CNN
F 3 "~" H 7500 2450 50  0001 C CNN
	1    7500 2450
	1    0    0    -1  
$EndComp
$Comp
L sb-cnc-shield-rescue:POLOLU_STEPPER_DRIVER U2
U 1 1 5D5E1DD8
P 6550 4550
F 0 "U2" H 6550 5197 60  0000 C CNN
F 1 "POLOLU_STEPPER_DRIVER" H 6550 5091 60  0001 C CNN
F 2 "sb-cnc-shield-master:a4988_shield" H 6550 4550 60  0001 C CNN
F 3 "" H 6550 4550 60  0000 C CNN
	1    6550 4550
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0113
U 1 1 5D5E1E09
P 7450 4750
F 0 "#PWR0113" H 7450 4600 50  0001 C CNN
F 1 "+5V" V 7465 4878 50  0000 L CNN
F 2 "" H 7450 4750 50  0001 C CNN
F 3 "" H 7450 4750 50  0001 C CNN
	1    7450 4750
	0    1    1    0   
$EndComp
Wire Wire Line
	7000 4750 7450 4750
$Comp
L power:GND #PWR0114
U 1 1 5D5E1E10
P 7150 4950
F 0 "#PWR0114" H 7150 4700 50  0001 C CNN
F 1 "GND" H 7155 4777 50  0000 C CNN
F 2 "" H 7150 4950 50  0001 C CNN
F 3 "" H 7150 4950 50  0001 C CNN
	1    7150 4950
	1    0    0    -1  
$EndComp
Wire Wire Line
	6100 4550 6000 4550
Wire Wire Line
	6000 4550 6000 4650
Wire Wire Line
	6000 4650 6100 4650
Text GLabel 6000 4750 0    50   Input ~ 0
M2_STEP
Text GLabel 6000 4850 0    50   Input ~ 0
M2_DIR
Wire Wire Line
	7000 4850 7150 4850
Wire Wire Line
	7150 4850 7150 4950
Wire Wire Line
	6000 4750 6100 4750
Wire Wire Line
	6000 4850 6100 4850
Text GLabel 7200 4350 2    50   Input ~ 0
M2_1
Text GLabel 7200 4450 2    50   Input ~ 0
M2_2
Text GLabel 7200 4550 2    50   Input ~ 0
M2_3
Text GLabel 7200 4650 2    50   Input ~ 0
M2_4
Wire Wire Line
	7000 4350 7200 4350
Wire Wire Line
	7000 4450 7200 4450
Wire Wire Line
	7000 4550 7200 4550
Wire Wire Line
	7000 4650 7200 4650
Text GLabel 4000 6250 2    50   Input ~ 0
M2_STEP
Text GLabel 3900 6450 2    50   Input ~ 0
M2_DIR
Wire Wire Line
	3900 5400 4000 5400
Wire Wire Line
	3900 5500 4000 5500
Text GLabel 13150 5050 2    50   Input ~ 0
M3_1
Text GLabel 13150 5150 2    50   Input ~ 0
M3_2
Text GLabel 13150 5250 2    50   Input ~ 0
M3_3
Text GLabel 13150 5350 2    50   Input ~ 0
M3_4
Wire Wire Line
	13000 5350 13150 5350
Wire Wire Line
	13000 5250 13150 5250
Wire Wire Line
	13000 5150 13150 5150
Wire Wire Line
	13000 5050 13150 5050
Wire Wire Line
	7000 2500 7150 2500
Wire Wire Line
	7150 2500 7150 2300
Wire Wire Line
	7150 2300 7500 2300
Wire Wire Line
	7500 2300 8100 2300
Connection ~ 7500 2300
Wire Wire Line
	7500 2600 8100 2600
Connection ~ 7500 2600
$Comp
L sb-cnc-shield-rescue:VIN #PWR0115
U 1 1 5D61E88D
P 8100 3950
F 0 "#PWR0115" H 8100 3800 50  0001 C CNN
F 1 "VIN" H 8115 4123 50  0000 C CNN
F 2 "" H 8100 3950 60  0000 C CNN
F 3 "" H 8100 3950 60  0000 C CNN
	1    8100 3950
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR0116
U 1 1 5D61E893
P 8100 4250
F 0 "#PWR0116" H 8100 4000 50  0001 C CNN
F 1 "GNDA" H 8105 4077 50  0000 C CNN
F 2 "" H 8100 4250 50  0001 C CNN
F 3 "" H 8100 4250 50  0001 C CNN
	1    8100 4250
	1    0    0    1   
$EndComp
Wire Wire Line
	7000 4250 7500 4250
$Comp
L Device:CP C2
U 1 1 5D61E89A
P 7500 4100
F 0 "C2" H 7618 4146 50  0000 L CNN
F 1 "100uF" H 7618 4055 50  0000 L CNN
F 2 "Capacitor_THT:CP_Radial_D10.0mm_P3.50mm" H 7538 3950 50  0001 C CNN
F 3 "~" H 7500 4100 50  0001 C CNN
	1    7500 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	7000 4150 7150 4150
Wire Wire Line
	7150 4150 7150 3950
Wire Wire Line
	7150 3950 7500 3950
Wire Wire Line
	7500 3950 8100 3950
Connection ~ 7500 3950
Wire Wire Line
	7500 4250 8100 4250
Connection ~ 7500 4250
$Comp
L sb-cnc-shield-rescue:POLOLU_STEPPER_DRIVER U3
U 1 1 5D6276C6
P 6550 6100
F 0 "U3" H 6550 6747 60  0000 C CNN
F 1 "POLOLU_STEPPER_DRIVER" H 6550 6641 60  0001 C CNN
F 2 "sb-cnc-shield-master:a4988_shield" H 6550 6100 60  0001 C CNN
F 3 "" H 6550 6100 60  0000 C CNN
	1    6550 6100
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0118
U 1 1 5D6276F0
P 7450 6300
F 0 "#PWR0118" H 7450 6150 50  0001 C CNN
F 1 "+5V" V 7465 6428 50  0000 L CNN
F 2 "" H 7450 6300 50  0001 C CNN
F 3 "" H 7450 6300 50  0001 C CNN
	1    7450 6300
	0    1    1    0   
$EndComp
Wire Wire Line
	7000 6300 7450 6300
$Comp
L power:GND #PWR0119
U 1 1 5D6276F7
P 7150 6500
F 0 "#PWR0119" H 7150 6250 50  0001 C CNN
F 1 "GND" H 7155 6327 50  0000 C CNN
F 2 "" H 7150 6500 50  0001 C CNN
F 3 "" H 7150 6500 50  0001 C CNN
	1    7150 6500
	1    0    0    -1  
$EndComp
Wire Wire Line
	6100 6100 6000 6100
Wire Wire Line
	6000 6100 6000 6200
Wire Wire Line
	6000 6200 6100 6200
Text GLabel 6000 6300 0    50   Input ~ 0
M3_STEP
Text GLabel 6000 6400 0    50   Input ~ 0
M3_DIR
Wire Wire Line
	7000 6400 7150 6400
Wire Wire Line
	7150 6400 7150 6500
Wire Wire Line
	6000 6300 6100 6300
Wire Wire Line
	6000 6400 6100 6400
Text GLabel 7200 5900 2    50   Input ~ 0
M3_1
Text GLabel 7200 6000 2    50   Input ~ 0
M3_2
Text GLabel 7200 6100 2    50   Input ~ 0
M3_3
Text GLabel 7200 6200 2    50   Input ~ 0
M3_4
Wire Wire Line
	7000 5900 7200 5900
Wire Wire Line
	7000 6000 7200 6000
Wire Wire Line
	7000 6100 7200 6100
Wire Wire Line
	7000 6200 7200 6200
$Comp
L sb-cnc-shield-rescue:VIN #PWR0120
U 1 1 5D62770F
P 8100 5500
F 0 "#PWR0120" H 8100 5350 50  0001 C CNN
F 1 "VIN" H 8115 5673 50  0000 C CNN
F 2 "" H 8100 5500 60  0000 C CNN
F 3 "" H 8100 5500 60  0000 C CNN
	1    8100 5500
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR0121
U 1 1 5D627715
P 8100 5800
F 0 "#PWR0121" H 8100 5550 50  0001 C CNN
F 1 "GNDA" H 8105 5627 50  0000 C CNN
F 2 "" H 8100 5800 50  0001 C CNN
F 3 "" H 8100 5800 50  0001 C CNN
	1    8100 5800
	1    0    0    1   
$EndComp
Wire Wire Line
	7000 5800 7500 5800
$Comp
L Device:CP C3
U 1 1 5D62771C
P 7500 5650
F 0 "C3" H 7618 5696 50  0000 L CNN
F 1 "100uF" H 7618 5605 50  0000 L CNN
F 2 "Capacitor_THT:CP_Radial_D10.0mm_P3.50mm" H 7538 5500 50  0001 C CNN
F 3 "~" H 7500 5650 50  0001 C CNN
	1    7500 5650
	1    0    0    -1  
$EndComp
Wire Wire Line
	7000 5700 7150 5700
Wire Wire Line
	7150 5700 7150 5500
Wire Wire Line
	7150 5500 7500 5500
Wire Wire Line
	7500 5500 8100 5500
Connection ~ 7500 5500
Wire Wire Line
	7500 5800 8100 5800
Connection ~ 7500 5800
Text GLabel 3900 4600 2    50   Input ~ 0
M3_STEP
Text GLabel 3900 4500 2    50   Input ~ 0
M3_DIR
Wire Wire Line
	3900 5600 4000 5600
Wire Wire Line
	3900 5700 4000 5700
Text GLabel 15350 4200 2    50   Input ~ 0
M4_1
Text GLabel 15350 4300 2    50   Input ~ 0
M4_2
Text GLabel 15350 4400 2    50   Input ~ 0
M4_3
Text GLabel 15350 4500 2    50   Input ~ 0
M4_4
Wire Wire Line
	15200 4500 15350 4500
Wire Wire Line
	15200 4400 15350 4400
Wire Wire Line
	15200 4300 15350 4300
Wire Wire Line
	15200 4200 15350 4200
Text GLabel 15650 4900 2    50   Input ~ 0
M5_1
Text GLabel 15650 4800 2    50   Input ~ 0
M5_2
Text GLabel 15650 4700 2    50   Input ~ 0
M5_3
Text GLabel 15650 4600 2    50   Input ~ 0
M5_4
Wire Wire Line
	15200 4900 15650 4900
Wire Wire Line
	15200 4800 15650 4800
Wire Wire Line
	15200 4700 15650 4700
Wire Wire Line
	15200 4600 15650 4600
Text GLabel 15350 5300 2    50   Input ~ 0
M6_1
Text GLabel 15350 5200 2    50   Input ~ 0
M6_2
Text GLabel 15350 5100 2    50   Input ~ 0
M6_3
Text GLabel 15350 5000 2    50   Input ~ 0
M6_4
Wire Wire Line
	15200 5000 15350 5000
Wire Wire Line
	15200 5100 15350 5100
Wire Wire Line
	15200 5200 15350 5200
Wire Wire Line
	15200 5300 15350 5300
$Comp
L sb-cnc-shield-rescue:POLOLU_STEPPER_DRIVER U4
U 1 1 5D662963
P 10350 2900
F 0 "U4" H 10350 3547 60  0000 C CNN
F 1 "POLOLU_STEPPER_DRIVER" H 10350 3441 60  0001 C CNN
F 2 "sb-cnc-shield-master:a4988_shield" H 10350 2900 60  0001 C CNN
F 3 "" H 10350 2900 60  0000 C CNN
	1    10350 2900
	1    0    0    -1  
$EndComp
$Comp
L sb-cnc-shield-rescue:VIN #PWR0123
U 1 1 5D66298D
P 11900 2300
F 0 "#PWR0123" H 11900 2150 50  0001 C CNN
F 1 "VIN" H 11915 2473 50  0000 C CNN
F 2 "" H 11900 2300 60  0000 C CNN
F 3 "" H 11900 2300 60  0000 C CNN
	1    11900 2300
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0124
U 1 1 5D662993
P 11250 3100
F 0 "#PWR0124" H 11250 2950 50  0001 C CNN
F 1 "+5V" V 11265 3228 50  0000 L CNN
F 2 "" H 11250 3100 50  0001 C CNN
F 3 "" H 11250 3100 50  0001 C CNN
	1    11250 3100
	0    1    1    0   
$EndComp
Wire Wire Line
	10800 3100 11250 3100
$Comp
L power:GND #PWR0125
U 1 1 5D66299A
P 10950 3300
F 0 "#PWR0125" H 10950 3050 50  0001 C CNN
F 1 "GND" H 10955 3127 50  0000 C CNN
F 2 "" H 10950 3300 50  0001 C CNN
F 3 "" H 10950 3300 50  0001 C CNN
	1    10950 3300
	1    0    0    -1  
$EndComp
Wire Wire Line
	9900 2900 9800 2900
Wire Wire Line
	9800 2900 9800 3000
Wire Wire Line
	9800 3000 9900 3000
Text GLabel 9800 3100 0    50   Input ~ 0
M4_STEP
Text GLabel 9800 3200 0    50   Input ~ 0
M4_DIR
Wire Wire Line
	10800 3200 10950 3200
Wire Wire Line
	10950 3200 10950 3300
Wire Wire Line
	9800 3100 9900 3100
Wire Wire Line
	9800 3200 9900 3200
$Comp
L power:GNDA #PWR0126
U 1 1 5D6629AA
P 11900 2600
F 0 "#PWR0126" H 11900 2350 50  0001 C CNN
F 1 "GNDA" H 11905 2427 50  0000 C CNN
F 2 "" H 11900 2600 50  0001 C CNN
F 3 "" H 11900 2600 50  0001 C CNN
	1    11900 2600
	1    0    0    1   
$EndComp
Wire Wire Line
	10800 2600 11300 2600
Text GLabel 11000 2700 2    50   Input ~ 0
M4_1
Text GLabel 11000 2800 2    50   Input ~ 0
M4_2
Text GLabel 11000 2900 2    50   Input ~ 0
M4_3
Text GLabel 11000 3000 2    50   Input ~ 0
M4_4
Wire Wire Line
	10800 2700 11000 2700
Wire Wire Line
	10800 2800 11000 2800
Wire Wire Line
	10800 2900 11000 2900
Wire Wire Line
	10800 3000 11000 3000
$Comp
L Device:CP C4
U 1 1 5D6629B9
P 11300 2450
F 0 "C4" H 11418 2496 50  0000 L CNN
F 1 "100uF" H 11418 2405 50  0000 L CNN
F 2 "Capacitor_THT:CP_Radial_D10.0mm_P3.50mm" H 11338 2300 50  0001 C CNN
F 3 "~" H 11300 2450 50  0001 C CNN
	1    11300 2450
	1    0    0    -1  
$EndComp
$Comp
L sb-cnc-shield-rescue:POLOLU_STEPPER_DRIVER U5
U 1 1 5D6629BF
P 10350 4550
F 0 "U5" H 10350 5197 60  0000 C CNN
F 1 "POLOLU_STEPPER_DRIVER" H 10350 5091 60  0001 C CNN
F 2 "sb-cnc-shield-master:a4988_shield" H 10350 4550 60  0001 C CNN
F 3 "" H 10350 4550 60  0000 C CNN
	1    10350 4550
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0128
U 1 1 5D6629E9
P 11250 4750
F 0 "#PWR0128" H 11250 4600 50  0001 C CNN
F 1 "+5V" V 11265 4878 50  0000 L CNN
F 2 "" H 11250 4750 50  0001 C CNN
F 3 "" H 11250 4750 50  0001 C CNN
	1    11250 4750
	0    1    1    0   
$EndComp
Wire Wire Line
	10800 4750 11250 4750
$Comp
L power:GND #PWR0129
U 1 1 5D6629F0
P 10950 4950
F 0 "#PWR0129" H 10950 4700 50  0001 C CNN
F 1 "GND" H 10955 4777 50  0000 C CNN
F 2 "" H 10950 4950 50  0001 C CNN
F 3 "" H 10950 4950 50  0001 C CNN
	1    10950 4950
	1    0    0    -1  
$EndComp
Wire Wire Line
	9900 4550 9800 4550
Wire Wire Line
	9800 4550 9800 4650
Wire Wire Line
	9800 4650 9900 4650
Text GLabel 9800 4750 0    50   Input ~ 0
M5_STEP
Text GLabel 9800 4850 0    50   Input ~ 0
M5_DIR
Wire Wire Line
	10800 4850 10950 4850
Wire Wire Line
	10950 4850 10950 4950
Wire Wire Line
	9800 4750 9900 4750
Wire Wire Line
	9800 4850 9900 4850
Text GLabel 11000 4350 2    50   Input ~ 0
M5_1
Text GLabel 11000 4450 2    50   Input ~ 0
M5_2
Text GLabel 11000 4550 2    50   Input ~ 0
M5_3
Text GLabel 11000 4650 2    50   Input ~ 0
M5_4
Wire Wire Line
	10800 4350 11000 4350
Wire Wire Line
	10800 4450 11000 4450
Wire Wire Line
	10800 4550 11000 4550
Wire Wire Line
	10800 4650 11000 4650
Wire Wire Line
	10800 2500 10950 2500
Wire Wire Line
	10950 2500 10950 2300
Wire Wire Line
	10950 2300 11300 2300
Wire Wire Line
	11300 2300 11900 2300
Connection ~ 11300 2300
Wire Wire Line
	11300 2600 11900 2600
Connection ~ 11300 2600
$Comp
L sb-cnc-shield-rescue:VIN #PWR0130
U 1 1 5D662A0F
P 11900 3950
F 0 "#PWR0130" H 11900 3800 50  0001 C CNN
F 1 "VIN" H 11915 4123 50  0000 C CNN
F 2 "" H 11900 3950 60  0000 C CNN
F 3 "" H 11900 3950 60  0000 C CNN
	1    11900 3950
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR0131
U 1 1 5D662A15
P 11900 4250
F 0 "#PWR0131" H 11900 4000 50  0001 C CNN
F 1 "GNDA" H 11905 4077 50  0000 C CNN
F 2 "" H 11900 4250 50  0001 C CNN
F 3 "" H 11900 4250 50  0001 C CNN
	1    11900 4250
	1    0    0    1   
$EndComp
Wire Wire Line
	10800 4250 11300 4250
$Comp
L Device:CP C5
U 1 1 5D662A1C
P 11300 4100
F 0 "C5" H 11418 4146 50  0000 L CNN
F 1 "100uF" H 11418 4055 50  0000 L CNN
F 2 "Capacitor_THT:CP_Radial_D10.0mm_P3.50mm" H 11338 3950 50  0001 C CNN
F 3 "~" H 11300 4100 50  0001 C CNN
	1    11300 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	10800 4150 10950 4150
Wire Wire Line
	10950 4150 10950 3950
Wire Wire Line
	10950 3950 11300 3950
Wire Wire Line
	11300 3950 11900 3950
Connection ~ 11300 3950
Wire Wire Line
	11300 4250 11900 4250
Connection ~ 11300 4250
$Comp
L sb-cnc-shield-rescue:POLOLU_STEPPER_DRIVER U6
U 1 1 5D662A29
P 10350 6100
F 0 "U6" H 10350 6747 60  0000 C CNN
F 1 "POLOLU_STEPPER_DRIVER" H 10350 6641 60  0001 C CNN
F 2 "sb-cnc-shield-master:a4988_shield" H 10350 6100 60  0001 C CNN
F 3 "" H 10350 6100 60  0000 C CNN
	1    10350 6100
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0133
U 1 1 5D662A53
P 11250 6300
F 0 "#PWR0133" H 11250 6150 50  0001 C CNN
F 1 "+5V" V 11265 6428 50  0000 L CNN
F 2 "" H 11250 6300 50  0001 C CNN
F 3 "" H 11250 6300 50  0001 C CNN
	1    11250 6300
	0    1    1    0   
$EndComp
Wire Wire Line
	10800 6300 11250 6300
$Comp
L power:GND #PWR0134
U 1 1 5D662A5A
P 10950 6500
F 0 "#PWR0134" H 10950 6250 50  0001 C CNN
F 1 "GND" H 10955 6327 50  0000 C CNN
F 2 "" H 10950 6500 50  0001 C CNN
F 3 "" H 10950 6500 50  0001 C CNN
	1    10950 6500
	1    0    0    -1  
$EndComp
Wire Wire Line
	9900 6100 9800 6100
Wire Wire Line
	9800 6100 9800 6200
Wire Wire Line
	9800 6200 9900 6200
Text GLabel 9800 6300 0    50   Input ~ 0
M6_STEP
Text GLabel 9800 6400 0    50   Input ~ 0
M6_DIR
Wire Wire Line
	10800 6400 10950 6400
Wire Wire Line
	10950 6400 10950 6500
Wire Wire Line
	9800 6300 9900 6300
Wire Wire Line
	9800 6400 9900 6400
Text GLabel 11000 5900 2    50   Input ~ 0
M6_1
Text GLabel 11000 6000 2    50   Input ~ 0
M6_2
Text GLabel 11000 6100 2    50   Input ~ 0
M6_3
Text GLabel 11000 6200 2    50   Input ~ 0
M6_4
Wire Wire Line
	10800 5900 11000 5900
Wire Wire Line
	10800 6000 11000 6000
Wire Wire Line
	10800 6100 11000 6100
Wire Wire Line
	10800 6200 11000 6200
$Comp
L sb-cnc-shield-rescue:VIN #PWR0135
U 1 1 5D662A72
P 11900 5500
F 0 "#PWR0135" H 11900 5350 50  0001 C CNN
F 1 "VIN" H 11915 5673 50  0000 C CNN
F 2 "" H 11900 5500 60  0000 C CNN
F 3 "" H 11900 5500 60  0000 C CNN
	1    11900 5500
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR0136
U 1 1 5D662A78
P 11900 5800
F 0 "#PWR0136" H 11900 5550 50  0001 C CNN
F 1 "GNDA" H 11905 5627 50  0000 C CNN
F 2 "" H 11900 5800 50  0001 C CNN
F 3 "" H 11900 5800 50  0001 C CNN
	1    11900 5800
	1    0    0    1   
$EndComp
Wire Wire Line
	10800 5800 11300 5800
$Comp
L Device:CP C6
U 1 1 5D662A7F
P 11300 5650
F 0 "C6" H 11418 5696 50  0000 L CNN
F 1 "100uF" H 11418 5605 50  0000 L CNN
F 2 "Capacitor_THT:CP_Radial_D10.0mm_P3.50mm" H 11338 5500 50  0001 C CNN
F 3 "~" H 11300 5650 50  0001 C CNN
	1    11300 5650
	1    0    0    -1  
$EndComp
Wire Wire Line
	10800 5700 10950 5700
Wire Wire Line
	10950 5700 10950 5500
Wire Wire Line
	10950 5500 11300 5500
Wire Wire Line
	11300 5500 11900 5500
Connection ~ 11300 5500
Wire Wire Line
	11300 5800 11900 5800
Connection ~ 11300 5800
Text GLabel 1900 5750 0    50   Input ~ 0
M4_STEP
Text GLabel 1900 5950 0    50   Input ~ 0
M4_DIR
Wire Wire Line
	3900 5950 4000 5950
Text GLabel 2000 6800 0    50   Input ~ 0
M5_DIR
Wire Wire Line
	3900 6050 4000 6050
Wire Wire Line
	3900 6150 4000 6150
Text GLabel 3900 6550 2    50   Input ~ 0
M6_STEP
Text GLabel 3900 6650 2    50   Input ~ 0
M6_DIR
Wire Wire Line
	3900 6250 4000 6250
Wire Wire Line
	3900 6350 4000 6350
Wire Wire Line
	3900 5800 4000 5800
NoConn ~ 2000 2500
NoConn ~ 2000 2600
NoConn ~ 2000 3000
NoConn ~ 2000 3200
NoConn ~ 2000 3300
NoConn ~ 2000 3400
NoConn ~ 2000 3500
NoConn ~ 2000 3600
NoConn ~ 2000 3700
NoConn ~ 2000 3800
NoConn ~ 2000 3900
NoConn ~ 2000 4100
NoConn ~ 2000 4200
NoConn ~ 2000 4300
NoConn ~ 2000 4400
NoConn ~ 2000 4500
NoConn ~ 2000 4600
NoConn ~ 2000 4700
NoConn ~ 2000 4800
NoConn ~ 2000 5400
NoConn ~ 2000 5600
NoConn ~ 2000 5850
NoConn ~ 3900 4400
NoConn ~ 3900 4300
NoConn ~ 3900 4200
NoConn ~ 3900 4100
NoConn ~ 3900 4000
NoConn ~ 3900 3900
NoConn ~ 3900 3700
NoConn ~ 3900 3600
NoConn ~ 3900 3500
NoConn ~ 3900 3400
NoConn ~ 3900 3300
NoConn ~ 3900 3200
NoConn ~ 3900 3100
NoConn ~ 3900 3000
NoConn ~ 3900 2800
NoConn ~ 3900 2700
NoConn ~ 3900 2600
NoConn ~ 3900 2500
NoConn ~ 3900 2400
NoConn ~ 3900 2300
NoConn ~ 3900 2100
$Comp
L power:+5V #PWR0137
U 1 1 5DB630B5
P 4550 4950
F 0 "#PWR0137" H 4550 4800 50  0001 C CNN
F 1 "+5V" H 4565 5123 50  0000 C CNN
F 2 "" H 4550 4950 50  0001 C CNN
F 3 "" H 4550 4950 50  0001 C CNN
	1    4550 4950
	1    0    0    -1  
$EndComp
Wire Wire Line
	3900 4950 4000 4950
Wire Wire Line
	3900 4850 4000 4850
Wire Wire Line
	4000 4850 4000 4950
NoConn ~ 13000 5450
NoConn ~ 14150 5450
NoConn ~ 15200 5400
$Comp
L Device:Jumper_NO_Small JP4
U 1 1 5DCBE1D1
P 5600 4250
F 0 "JP4" H 5600 4435 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 4344 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 4250 50  0001 C CNN
F 3 "~" H 5600 4250 50  0001 C CNN
	1    5600 4250
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP5
U 1 1 5DCBE1D7
P 5600 4350
F 0 "JP5" H 5600 4535 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 4444 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 4350 50  0001 C CNN
F 3 "~" H 5600 4350 50  0001 C CNN
	1    5600 4350
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP6
U 1 1 5DCBE1DD
P 5600 4450
F 0 "JP6" H 5600 4635 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 4544 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 4450 50  0001 C CNN
F 3 "~" H 5600 4450 50  0001 C CNN
	1    5600 4450
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0112
U 1 1 5DCBE1E3
P 5150 4100
F 0 "#PWR0112" H 5150 3950 50  0001 C CNN
F 1 "+5V" H 5165 4273 50  0000 C CNN
F 2 "" H 5150 4100 50  0001 C CNN
F 3 "" H 5150 4100 50  0001 C CNN
	1    5150 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	5150 4100 5150 4250
Wire Wire Line
	5150 4250 5500 4250
Wire Wire Line
	5150 4250 5150 4350
Wire Wire Line
	5150 4350 5500 4350
Connection ~ 5150 4250
Wire Wire Line
	5150 4350 5150 4450
Wire Wire Line
	5150 4450 5500 4450
Connection ~ 5150 4350
Wire Wire Line
	5700 4250 6100 4250
Wire Wire Line
	5700 4350 6100 4350
Wire Wire Line
	5700 4450 6100 4450
Text GLabel 5950 4150 0    50   Input ~ 0
notEn
Wire Wire Line
	5950 4150 6100 4150
$Comp
L Device:Jumper_NO_Small JP7
U 1 1 5DCE06CB
P 5600 5800
F 0 "JP7" H 5600 5985 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 5894 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 5800 50  0001 C CNN
F 3 "~" H 5600 5800 50  0001 C CNN
	1    5600 5800
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP8
U 1 1 5DCE06D1
P 5600 5900
F 0 "JP8" H 5600 6085 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 5994 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 5900 50  0001 C CNN
F 3 "~" H 5600 5900 50  0001 C CNN
	1    5600 5900
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP9
U 1 1 5DCE06D7
P 5600 6000
F 0 "JP9" H 5600 6185 50  0001 C CNN
F 1 "Jumper_NO_Small" H 5600 6094 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 5600 6000 50  0001 C CNN
F 3 "~" H 5600 6000 50  0001 C CNN
	1    5600 6000
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0117
U 1 1 5DCE06DD
P 5150 5650
F 0 "#PWR0117" H 5150 5500 50  0001 C CNN
F 1 "+5V" H 5165 5823 50  0000 C CNN
F 2 "" H 5150 5650 50  0001 C CNN
F 3 "" H 5150 5650 50  0001 C CNN
	1    5150 5650
	1    0    0    -1  
$EndComp
Wire Wire Line
	5150 5650 5150 5800
Wire Wire Line
	5150 5800 5500 5800
Wire Wire Line
	5150 5800 5150 5900
Wire Wire Line
	5150 5900 5500 5900
Connection ~ 5150 5800
Wire Wire Line
	5150 5900 5150 6000
Wire Wire Line
	5150 6000 5500 6000
Connection ~ 5150 5900
Wire Wire Line
	5700 5800 6100 5800
Wire Wire Line
	5700 5900 6100 5900
Wire Wire Line
	5700 6000 6100 6000
Text GLabel 5950 5700 0    50   Input ~ 0
notEn
Wire Wire Line
	5950 5700 6100 5700
$Comp
L Device:Jumper_NO_Small JP16
U 1 1 5DD03F6C
P 9400 5800
F 0 "JP16" H 9400 5985 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 5894 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 5800 50  0001 C CNN
F 3 "~" H 9400 5800 50  0001 C CNN
	1    9400 5800
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP17
U 1 1 5DD03F72
P 9400 5900
F 0 "JP17" H 9400 6085 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 5994 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 5900 50  0001 C CNN
F 3 "~" H 9400 5900 50  0001 C CNN
	1    9400 5900
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP18
U 1 1 5DD03F78
P 9400 6000
F 0 "JP18" H 9400 6185 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 6094 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 6000 50  0001 C CNN
F 3 "~" H 9400 6000 50  0001 C CNN
	1    9400 6000
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0122
U 1 1 5DD03F7E
P 8950 5650
F 0 "#PWR0122" H 8950 5500 50  0001 C CNN
F 1 "+5V" H 8965 5823 50  0000 C CNN
F 2 "" H 8950 5650 50  0001 C CNN
F 3 "" H 8950 5650 50  0001 C CNN
	1    8950 5650
	1    0    0    -1  
$EndComp
Wire Wire Line
	8950 5650 8950 5800
Wire Wire Line
	8950 5800 9300 5800
Wire Wire Line
	8950 5800 8950 5900
Wire Wire Line
	8950 5900 9300 5900
Connection ~ 8950 5800
Wire Wire Line
	8950 5900 8950 6000
Wire Wire Line
	8950 6000 9300 6000
Connection ~ 8950 5900
Wire Wire Line
	9500 5800 9900 5800
Wire Wire Line
	9500 5900 9900 5900
Wire Wire Line
	9500 6000 9900 6000
Text GLabel 9750 5700 0    50   Input ~ 0
notEn
Wire Wire Line
	9750 5700 9900 5700
$Comp
L Device:Jumper_NO_Small JP13
U 1 1 5DD279EE
P 9400 4250
F 0 "JP13" H 9400 4435 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 4344 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 4250 50  0001 C CNN
F 3 "~" H 9400 4250 50  0001 C CNN
	1    9400 4250
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP14
U 1 1 5DD279F4
P 9400 4350
F 0 "JP14" H 9400 4535 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 4444 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 4350 50  0001 C CNN
F 3 "~" H 9400 4350 50  0001 C CNN
	1    9400 4350
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP15
U 1 1 5DD279FA
P 9400 4450
F 0 "JP15" H 9400 4635 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 4544 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 4450 50  0001 C CNN
F 3 "~" H 9400 4450 50  0001 C CNN
	1    9400 4450
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0127
U 1 1 5DD27A00
P 8950 4100
F 0 "#PWR0127" H 8950 3950 50  0001 C CNN
F 1 "+5V" H 8965 4273 50  0000 C CNN
F 2 "" H 8950 4100 50  0001 C CNN
F 3 "" H 8950 4100 50  0001 C CNN
	1    8950 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	8950 4100 8950 4250
Wire Wire Line
	8950 4250 9300 4250
Wire Wire Line
	8950 4250 8950 4350
Wire Wire Line
	8950 4350 9300 4350
Connection ~ 8950 4250
Wire Wire Line
	8950 4350 8950 4450
Wire Wire Line
	8950 4450 9300 4450
Connection ~ 8950 4350
Wire Wire Line
	9500 4250 9900 4250
Wire Wire Line
	9500 4350 9900 4350
Wire Wire Line
	9500 4450 9900 4450
Text GLabel 9750 4150 0    50   Input ~ 0
notEn
Wire Wire Line
	9750 4150 9900 4150
$Comp
L Device:Jumper_NO_Small JP10
U 1 1 5DD4B51C
P 9400 2600
F 0 "JP10" H 9400 2785 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 2694 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 2600 50  0001 C CNN
F 3 "~" H 9400 2600 50  0001 C CNN
	1    9400 2600
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP11
U 1 1 5DD4B522
P 9400 2700
F 0 "JP11" H 9400 2885 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 2794 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 2700 50  0001 C CNN
F 3 "~" H 9400 2700 50  0001 C CNN
	1    9400 2700
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NO_Small JP12
U 1 1 5DD4B528
P 9400 2800
F 0 "JP12" H 9400 2985 50  0001 C CNN
F 1 "Jumper_NO_Small" H 9400 2894 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x02_P2.54mm_Vertical" H 9400 2800 50  0001 C CNN
F 3 "~" H 9400 2800 50  0001 C CNN
	1    9400 2800
	1    0    0    -1  
$EndComp
$Comp
L power:+5V #PWR0132
U 1 1 5DD4B52E
P 8950 2450
F 0 "#PWR0132" H 8950 2300 50  0001 C CNN
F 1 "+5V" H 8965 2623 50  0000 C CNN
F 2 "" H 8950 2450 50  0001 C CNN
F 3 "" H 8950 2450 50  0001 C CNN
	1    8950 2450
	1    0    0    -1  
$EndComp
Wire Wire Line
	8950 2450 8950 2600
Wire Wire Line
	8950 2600 9300 2600
Wire Wire Line
	8950 2600 8950 2700
Wire Wire Line
	8950 2700 9300 2700
Connection ~ 8950 2600
Wire Wire Line
	8950 2700 8950 2800
Wire Wire Line
	8950 2800 9300 2800
Connection ~ 8950 2700
Wire Wire Line
	9500 2600 9900 2600
Wire Wire Line
	9500 2700 9900 2700
Wire Wire Line
	9500 2800 9900 2800
Text GLabel 9750 2500 0    50   Input ~ 0
notEn
Wire Wire Line
	9750 2500 9900 2500
$Comp
L power:+5V #PWR0138
U 1 1 5D5AC2CB
P 14300 4050
F 0 "#PWR0138" H 14300 3900 50  0001 C CNN
F 1 "+5V" H 14315 4223 50  0000 C CNN
F 2 "" H 14300 4050 50  0001 C CNN
F 3 "" H 14300 4050 50  0001 C CNN
	1    14300 4050
	1    0    0    -1  
$EndComp
Wire Wire Line
	14150 4250 14300 4250
Wire Wire Line
	14300 4050 14300 4250
Wire Wire Line
	14150 4350 14300 4350
Wire Wire Line
	14300 4350 14300 4250
Connection ~ 14300 4250
$Comp
L Connector:Conn_01x13_Male J3
U 1 1 5D554968
P 13950 4850
F 0 "J3" H 14058 5631 50  0000 C CNN
F 1 "Conn_01x13_Male" H 14058 5540 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x13_P2.54mm_Vertical" H 13950 4850 50  0001 C CNN
F 3 "~" H 13950 4850 50  0001 C CNN
	1    13950 4850
	1    0    0    -1  
$EndComp
Text GLabel 14300 4450 2    50   Input ~ 0
M3_LIMIT
Wire Wire Line
	14150 4450 14300 4450
Text GLabel 14300 4550 2    50   Input ~ 0
M5_LIMIT
Wire Wire Line
	14150 4550 14300 4550
Text GLabel 14300 4650 2    50   Input ~ 0
M1_LIMIT
Wire Wire Line
	14150 4650 14300 4650
Text GLabel 14300 4750 2    50   Input ~ 0
M4_LIMIT
Wire Wire Line
	14150 4750 14300 4750
Text GLabel 14300 4850 2    50   Input ~ 0
M2_LIMIT
Wire Wire Line
	14150 4850 14300 4850
Text GLabel 14300 4950 2    50   Input ~ 0
M6_LIMIT
Wire Wire Line
	14150 4950 14300 4950
Text GLabel 350  6050 0    50   Input ~ 0
M3_LIMIT
Text GLabel 350  6150 0    50   Input ~ 0
M5_LIMIT
Text GLabel 350  6350 0    50   Input ~ 0
M4_LIMIT
Text GLabel 350  6450 0    50   Input ~ 0
M2_LIMIT
Text GLabel 350  6600 0    50   Input ~ 0
M6_LIMIT
Wire Wire Line
	4000 4950 4550 4950
Connection ~ 4000 4950
Wire Wire Line
	3900 5100 4450 5100
$Comp
L Device:R R4
U 1 1 5D80B410
P 1850 7250
F 0 "R4" H 1920 7296 50  0000 L CNN
F 1 "10k" H 1920 7205 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P7.62mm_Horizontal" V 1780 7250 50  0001 C CNN
F 3 "~" H 1850 7250 50  0001 C CNN
	1    1850 7250
	-1   0    0    -1  
$EndComp
$Comp
L Device:R R6
U 1 1 5D84DE77
P 1300 7250
F 0 "R6" H 1370 7296 50  0000 L CNN
F 1 "10k" H 1370 7205 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P7.62mm_Horizontal" V 1230 7250 50  0001 C CNN
F 3 "~" H 1300 7250 50  0001 C CNN
	1    1300 7250
	-1   0    0    -1  
$EndComp
$Comp
L power:GND #PWR0139
U 1 1 5D918486
P 1550 7550
F 0 "#PWR0139" H 1550 7300 50  0001 C CNN
F 1 "GND" H 1555 7377 50  0000 C CNN
F 2 "" H 1550 7550 50  0001 C CNN
F 3 "" H 1550 7550 50  0001 C CNN
	1    1550 7550
	-1   0    0    -1  
$EndComp
Wire Wire Line
	1850 7400 1850 7550
Wire Wire Line
	1850 7550 1550 7550
Wire Wire Line
	1550 7400 1550 7550
Connection ~ 1550 7550
Wire Wire Line
	800  7400 800  7550
$Comp
L Device:R R1
U 1 1 5D9906F3
P 1050 7250
F 0 "R1" H 1120 7296 50  0000 L CNN
F 1 "10k" H 1120 7205 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P7.62mm_Horizontal" V 980 7250 50  0001 C CNN
F 3 "~" H 1050 7250 50  0001 C CNN
	1    1050 7250
	-1   0    0    -1  
$EndComp
$Comp
L Analog_Switch:MAX323CSA U7
U 1 1 5D59BFBF
P 3450 7800
F 0 "U7" H 3450 8067 50  0000 C CNN
F 1 "MAX323CSA" H 3450 7976 50  0000 C CNN
F 2 "sb-cnc-shield-master:switch_13" H 3450 7700 50  0001 C CNN
F 3 "https://datasheets.maximintegrated.com/en/ds/MAX323-MAX325.pdf" H 3450 7800 50  0001 C CNN
	1    3450 7800
	1    0    0    -1  
$EndComp
NoConn ~ 3450 8000
NoConn ~ 3900 5200
NoConn ~ 3900 5300
NoConn ~ 14150 5050
NoConn ~ 14150 5150
NoConn ~ 14150 5250
NoConn ~ 14150 5350
$Comp
L Device:R R5
U 1 1 5D837E40
P 1550 7250
F 0 "R5" H 1620 7296 50  0000 L CNN
F 1 "10k" H 1620 7205 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P7.62mm_Horizontal" V 1480 7250 50  0001 C CNN
F 3 "~" H 1550 7250 50  0001 C CNN
	1    1550 7250
	-1   0    0    -1  
$EndComp
Wire Wire Line
	1050 7400 1050 7550
Wire Wire Line
	1050 7550 1300 7550
Wire Wire Line
	1300 7400 1300 7550
Connection ~ 1300 7550
Wire Wire Line
	1300 7550 1550 7550
$Comp
L Device:R R3
U 1 1 5D9906E6
P 800 7250
F 0 "R3" H 870 7296 50  0000 L CNN
F 1 "10k" H 870 7205 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P7.62mm_Horizontal" V 730 7250 50  0001 C CNN
F 3 "~" H 800 7250 50  0001 C CNN
	1    800  7250
	-1   0    0    -1  
$EndComp
$Comp
L Device:R R2
U 1 1 5D9906ED
P 550 7250
F 0 "R2" H 620 7296 50  0000 L CNN
F 1 "10k" H 620 7205 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P7.62mm_Horizontal" V 480 7250 50  0001 C CNN
F 3 "~" H 550 7250 50  0001 C CNN
	1    550  7250
	-1   0    0    -1  
$EndComp
Text GLabel 350  6250 0    50   Input ~ 0
M1_LIMIT
Wire Wire Line
	350  6450 1550 6450
Wire Wire Line
	1850 7100 1850 6600
Connection ~ 1850 6600
Wire Wire Line
	1850 6600 2000 6600
Wire Wire Line
	350  6600 1850 6600
Wire Wire Line
	1550 7100 1550 6450
Connection ~ 1550 6450
Wire Wire Line
	1550 6450 2000 6450
Wire Wire Line
	1300 7100 1300 6350
Wire Wire Line
	350  6350 1300 6350
Connection ~ 1300 6350
Wire Wire Line
	1300 6350 2000 6350
Wire Wire Line
	1050 7100 1050 6250
Wire Wire Line
	350  6250 1050 6250
Connection ~ 1050 6250
Wire Wire Line
	1050 6250 2000 6250
Wire Wire Line
	350  6150 800  6150
Connection ~ 800  6150
Wire Wire Line
	800  6150 2000 6150
Wire Wire Line
	550  7100 550  6050
Wire Wire Line
	350  6050 550  6050
Connection ~ 550  6050
Wire Wire Line
	550  6050 2000 6050
Wire Wire Line
	800  7550 1050 7550
Connection ~ 1050 7550
Wire Wire Line
	550  7550 800  7550
Connection ~ 800  7550
Wire Wire Line
	550  7400 550  7550
Text GLabel 2000 6900 0    50   Input ~ 0
M5_STEP
Wire Wire Line
	800  6150 800  7100
NoConn ~ 2000 6700
$Comp
L arduino_shieldsNCL:ARDUINO_MEGA_SHIELD SHIELD1
U 1 1 5D595715
P 3000 4450
F 0 "SHIELD1" H 2950 7087 60  0000 C CNN
F 1 "ARDUINO_MEGA_SHIELD" H 2950 6981 60  0000 C CNN
F 2 "sb-cnc-shield-master:ARDUINO_MEGA_SHIELD" H 3000 4450 50  0001 C CNN
F 3 "" H 3000 4450 50  0001 C CNN
	1    3000 4450
	1    0    0    -1  
$EndComp
$EndSCHEMATC
