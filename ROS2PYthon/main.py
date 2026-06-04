import socket
import time
from Key import Key
from Led import LED
from USBDevice import *
from Buzzer import Buzzer
from espmax import ESPMax
from PWMServo import PWMServo
from micropython import const
from machine import Pin, ADC, Timer
from RobotControl import RobotControl
from SuctionNozzle import SuctionNozzle
from BusServo import BusServo, have_got_servo_pos


from connectToWifiEsp32 import esp32Wifi
from esp32MqttHandler import esp32MQTTHandler
#from esp32MqttPublisher import esp32MQTTPublisher


print("Please wait...")
led = LED()
buzzer = Buzzer()
pwm = PWMServo()
pwm.work_with_time()
bus_servo = BusServo()
robot = RobotControl()
arm = ESPMax(bus_servo)
nozzle = SuctionNozzle()
pos_servo_1 = BusServo()
pos_servo_2 = BusServo()
pos_servo_3 = BusServo()


connectToWifi = esp32Wifi()
MQTTEsp= esp32MQTTHandler()
#MQTTEspPub = esp32MQTTPublisher()

connectToWifi.connect("Redmi Note 12 Pro 5G","12345678")


MQTTEsp.identificationInfo("MaxArm",' 10.104.183.112',1883)
MQTTEsp.connectToBroker()
MQTTEsp.sub("rotativeBase/topic")
MQTTEsp.sub("verticalArm/topic")
MQTTEsp.sub("upDownSegment/topic")
MQTTEsp.sub("armHome/topic")
MQTTEsp.sub("noozle/topic")
#/////////////////////////



#/////////////////////////
arm.go_home()
print_en = True
nozzle_st = False
nozzle_angle = 0
buzzer.setBuzzer(80)
(x,y,z) = arm.ORIGIN
move_sleep = time.ticks_ms()
threshold = 1.0


print("Start")

arm.go_home()
(x, y, z) = arm.ORIGIN

noozleVal = MQTTEsp.noozleValues

lastX = x
lastY = y
lastZ = z

status = -1
while True:
    currX = x
    currY = y
    currZ = z

   
    if (abs(currX - lastX) >= threshold or abs(currY - lastY) >= threshold or  abs(currZ - lastZ) >= threshold):
    
        msg = "X:{:.1f}, Y:{:.1f}, Z:{:.1f}".format(x, y, z)
        MQTTEsp.pub("feedback/topic", msg)
        
        lastX = currX
        lastY = currY
        lastZ = currZ
   

    if MQTTEsp.client:
        try:
            MQTTEsp.client.check_msg()
        except OSErrror as e:
            pass
        
   
    xDelta = MQTTEsp.fbaseAngle
    yDelta = MQTTEsp.verticalArmAngle
    zDelta = MQTTEsp.upDownAngle
    armInitPos = MQTTEsp.armIniPos
    noozleVal = MQTTEsp.noozleValues
    
   
    
    if time.ticks_ms() >= move_sleep:
            
        if(noozleVal>0.5):
            nozzle.on()
            if status != 1:
                 MQTTEsp.pub("f_noozle/topic", "On")
                 status = 1
                
        else:
            nozzle.off()
            if status != 0:
                MQTTEsp.pub("f_noozle/topic", "Off")
                status = 0
       
        
        
       
        if(armInitPos>0.5):
            arm.go_home(1000)
            (x, y, z) = arm.ORIGIN 
            MQTTEsp.armIniPos = 0.0
            reset_msg =  " Reseted:{:.1f}".format(armInitPos)
            MQTTEsp.pub("reset/topic", reset_msg)

        if xDelta != 0 or yDelta != 0 or zDelta != 0:  
           
            target_x = x + xDelta
            if arm.set_position((target_x, y, z), 30):
                x = target_x   
                MQTTEsp.fbaseAngle = 0
            else:     
                MQTTEsp.fbaseAngle = 0
            move_sleep = time.ticks_ms() + 30

            target_y = y + yDelta
            MQTTEsp.verticalArmAngle = 0
            if arm.set_position((x, target_y, z), 30):
                y = target_y
            else:
               MQTTEsp.verticalArmAngle = 0
            move_sleep = time.ticks_ms() + 30
            target_z = z + zDelta
            
            MQTTEsp.upDownAngle = 0
            if arm.set_position((x, y, target_z), 30):
                 z = target_z
            else:
                MQTTEsp.upDownAngle = 0
            
           

            move_sleep = time.ticks_ms() + 30
     

#         print("Robot mutat la -> X:{:.1f}, Y:{:.1f}, Z:{:.1f}".format(x, y, z))
#         print("ArmInitPos:{:.1f}".format(armInitPos))
#         print("NoozleStatus:{:.1f}".format(noozleVal))
          
          
     
        move_sleep = time.ticks_ms() + 30

    time.sleep(0.005)



























