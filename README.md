#Virtual Flight Platform

Controls the 9DVR motion platfrom and performs single axis or multi-axis motions to imitate flying

#Hardware Required:
[9DVR platfrom](http://www.xd5d.com/9dvr/)

#Software Required:
.NetFramework

#Steps
##Start Procedure
1. Plug in the power cord
2. Push the green button
3. Push the power button for computer 1(It's above the green button)
![Power Button](Power_button.jpg)
4. Push the power button on the master computer(on the right side of the computer)
5. Wait for both two computers to start
6. Double-Click the VNC software
![VNC Program](VNC.jpg)
7. Set IP to 192.168.1.2 
![Set IP](Set_IP.jpg)
8. Connect
9. Open the Flight_Mode program on the slave computer
10. Click the "Connect device" button
11. Switch to the master computer, and open the Zhuoyuan Movie program
![Zhuoyuan Program](Movie_player.jpg)
12. Wait for approximate 7 seconds, until the motor controllers are turned on
11. Switch back to the slave computer, and click the "Flight Mode" button on the slave computer
16. Use keyboard or mouse to control the platform. Simultaneous key presses is supported.

##Exit Procedure
1. Close the flight mode window
2. Click the disconnect button
3. Close the program
4. Go back to the master computer
5. Close the Zhuoyuan Movie program (click the exit button, then close the window)


#FAQ
### What should I do if the direction get reversed?
* A: Close all programs and shutdown both computers. Push the red button(STOP) on the platform. Wait for at least 10s, then execute the turn on procedure.

###How can I home the platform?
* A: First, make sure the movement direction is not reversed. If not, see FAQ1 first. 
*    Then, click the Home button on the main menu. 
*    After that, select the axis that is not homed (only axis 0,1,2 needs to be homed).
*    Make sure the Searching Home Direction is negative. You can also adjust the homing speed there.
*    Click "Home" button and wait for the axis to go back to home position.
*    When the axis reaches zero, click the Stop Homing button.
*    Click the set pos button to set its current position to zero
*    Repeat for other axes

Note: You do not need to home the axis 3(the rotational axis)


