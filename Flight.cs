﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using imcpkg;

namespace Example
{
    public partial class Flight : Form
    {
        int[] m_encp = new int[Global.MAX_NAXIS];
        int[] m1_encp = new int[Global.MAX_NAXIS];
        int flight_level;
        int max_height = 130000;
        int min_height = 100;
        public Flight()
        {
            InitializeComponent();
            //Flightalert.Text += "===========================" + Environment.NewLine;
            Flightalert.Text += "Welcome to the Flight Mode!" + Environment.NewLine;
            Flightalert.Text += "Press Z to raise the platform" + Environment.NewLine;
            Flightalert.Text += "Press X to lower the platform" + Environment.NewLine;
            Flightalert.Text += "Press W to push down" + Environment.NewLine;
            Flightalert.Text += "Press S to pull up" + Environment.NewLine;
            Flightalert.Text += "Press A to turn left" + Environment.NewLine;
            Flightalert.Text += "Press D to turn right" + Environment.NewLine;
            Flightalert.Text += "You can also use mouse to control" + Environment.NewLine + Environment.NewLine; 
            Flightalert.Text += "If the control is reversed" + Environment.NewLine;
            Flightalert.Text += "Please turn off both two computers" + Environment.NewLine;
            Flightalert.Text += "Then push the stop button" + Environment.NewLine;
            Flightalert.Text += "Wait for at least 10s" + Environment.NewLine;
            Flightalert.Text += "Then do the start steps again" + Environment.NewLine + Environment.NewLine;
            Flightalert.Text += "Hope you enjoy the flight!" + Environment.NewLine;
            Flightalert.Text += "===========================" + Environment.NewLine;

        }

        private bool In_Bounds(int pos){
            return pos > min_height && pos < max_height;
        }

        private void Stop_All_Motors(){
            IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
            IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
            IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
        }

        private void Set_All_Acceleration(double acc){
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
        }

        private void Move_Motor(int final_pos, double start_velocity, double target_velocity, int motor){
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, motor);
        }

        private void Move_All_Motors(int final_pos, double start_velocity, double target_velocity){
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, 2);
        }

        private void Stop_Motor(int motor){
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, motor);
        }

        private void Move_To_Flight_Level(double start_velocity, double target_velocity){
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, start_velocity, target_velocity, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, start_velocity, target_velocity, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, start_velocity, target_velocity, 0, 2);
        }

        private void Flight_KeyDown(object sender, KeyEventArgs e)
        {
            double acc, svel, tvel;
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.X) || (e.KeyCode == Keys.Q) || (e.KeyCode == Keys.E))
            {
                IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
                acc = Convert.ToDouble(textBox1.Text); //Acceleration
                svel = Convert.ToDouble(textBox2.Text); //Starting velocity
                tvel = Convert.ToDouble(textBox3.Text); //Target velocity
                switch (e.KeyCode)
                {
                    case Keys.W:
                        Stop_All_Motors();
                        Set_All_Acceleration(acc);
                        for (int i = 0; i < 3; i++){
                            if (In_Bounds(m_encp[i])){
                                if (i == 0){
                                    Move_Motor(min_height, svel, tvel, i);
                                }
                                else{
                                    Move_Motor(max_height, svel, tvel, i);
                                }
                            }
                            else{
                                Stop_Motor(i);
                            }
                        }
                        break;
                    case Keys.S:
                        Stop_All_Motors();
                        Set_All_Acceleration(acc);
                        for (int i = 0; i < 3; i++){
                            if (In_Bounds(m_encp[i])){
                                if (i == 1 || i == 2){
                                    Move_Motor(min_height, svel, tvel, i);
                                }
                                else{
                                    Move_Motor(max_height, svel, tvel, i);
                                }
                            }
                            else{
                                Stop_Motor(i);
                            }
                        }
                        break;
                    case Keys.A:
                        Stop_All_Motors();
                        Set_All_Acceleration(acc);
                        if (In_Bounds(m_encp[1])){
                            Move_Motor(min_height, svel, tvel, 1);
                        }
                        else{
                            Stop_Motor(1);
                        }
                        if (In_Bounds(m_encp[2])){
                            Move_Motor(max_height, svel, tvel, 2);
                        }
                        else{
                            Stop_Motor(2);
                        }
                        break;
                    case Keys.D:
                        Stop_All_Motors();
                        Set_All_Acceleration(acc);
                        if (In_Bounds(m_encp[1])){
                            Move_Motor(max_height, svel, tvel, 1);
                        }
                        else{
                            Stop_Motor(1);
                        }
                        if (In_Bounds(m_encp[2])){
                            Move_Motor(min_height, svel, tvel, 2);
                        }
                        else{
                            Stop_Motor(2);
                        }
                        break;
                    case Keys.Z:
                        Set_All_Acceleration(acc);
                        Move_All_Motors(max_height, svel, tvel);
                        break;
                    case Keys.X:
                        Set_All_Acceleration(acc);
                        Move_All_Motors(min_height, svel, tvel);
                        break;
                    case Keys.Q:
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 3);
                        break;
                    case Keys.E:
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 3);
                        break;

                }
            }
        }

        private void Flight_KeyUp(object sender, KeyEventArgs e)
        {
            double acc, svel, tvel;
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.X) || (e.KeyCode == Keys.Q) || (e.KeyCode == Keys.E))
            {
                IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
                acc = Convert.ToDouble(textBox1.Text);
                svel = Convert.ToDouble(textBox2.Text);
                tvel = Convert.ToDouble(textBox3.Text);
                switch (e.KeyCode)
                {
                    case Keys.W:
                        Stop_All_Motors();
                        Move_To_Flight_Level(svel, tvel);
                        break;
                    case Keys.A:
                        Stop_All_Motors();
                        Move_To_Flight_Level(svel, tvel);
                        break;
                    case Keys.S:
                        Stop_All_Motors();
                        Move_To_Flight_Level(svel, tvel);
                        break;
                    case Keys.D:
                        Stop_All_Motors();
                        Move_To_Flight_Level(svel, tvel);
                        break;
                    case Keys.Z:
                        Stop_All_Motors();
                        Stop_Motor(0);
                        Stop_Motor(1);
                        Stop_Motor(2);
                        flight_level = m_encp.Max();
                        break;
                    case Keys.X:
                        Stop_All_Motors();
                        Stop_Motor(0);
                        Stop_Motor(1);
                        Stop_Motor(2);
                        flight_level = m_encp.Max();
                        break;
                    case Keys.Q:
                        Stop_Motor(3);
                        break;
                    case Keys.E:
                        Stop_Motor(3);
                        break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                m1_encp[i] = m_encp[i];
                IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
                if (m_encp[i] >= 130000 && m_encp[i] > m1_encp[i])
                {
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, i);
                    Flightalert.Text += "Axis Max Limit Triggered!" + Environment.NewLine;
                }
                if (m_encp[i] <= 100 && m_encp[i] < m1_encp[i])
                {
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, i);
                    Flightalert.Text += "Axis Min Limit Triggered!" + Environment.NewLine;
                }

            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)||(e.KeyCode==Keys.Back)))
            {
                label1.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tString = textBox1.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Please enter a valid number");
                    textBox1.Text = "10";
                    return;
                }
                if (Convert.ToDouble(textBox1.Text) < 0 || Convert.ToDouble(textBox1.Text) > Convert.ToDouble(textBox3.Text))
                {
                    MessageBox.Show("Valve not valid, must be between 0 and target vel");
                    textBox1.Text = "10";
                    return;
                }

            }
            //If it get's here it's a valid number
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string tString = textBox2.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Please enter a valid number");
                    textBox2.Text = "10";
                    return;
                }
                if (Convert.ToDouble(textBox2.Text) < 0 || Convert.ToDouble(textBox2.Text) > Convert.ToDouble(textBox3.Text))
                {
                    MessageBox.Show("Valve not valid, must be between 0 and target vel");
                    textBox2.Text = "10";
                    return;
                }

            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode == Keys.Back)))
            {
                label1.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string tString = textBox3.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Please enter a valid number");
                    textBox3.Text = "20";
                    return;
                }
                if (Convert.ToDouble(textBox3.Text) > 80 )
                {
                    MessageBox.Show("Maximum speed is 80");
                    textBox3.Text = "20";
                    return;
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode == Keys.Back)))
            {
                label1.Focus();
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            if (m_encp[0] > 100)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 0);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            }
            if (m_encp[1] < 140000)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 1);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            }
            if (m_encp[2] < 140000)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 2);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            if (m_encp[0] < 140000)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 0);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            }
            if (m_encp[1] > 100)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 1);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            }
            if (m_encp[2] > 100)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 2);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            }
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            if (m_encp[1] > 100)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 1);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            }
            if (m_encp[2] < 140000)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 2);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            }
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            if (m_encp[1] < 140000)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 1);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            }
            if (m_encp[2] > 100)
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 2);
            }
            else
            {
                IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            }
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 140000, svel, tvel, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 140000, svel, tvel, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 140000, svel, tvel, 0, 2);
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 0, svel, tvel, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 0, svel, tvel, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 0, svel, tvel, 0, 2);
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
            IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            flight_level = m_encp.Max();
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
            flight_level = m_encp.Max();
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 3);
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 3);
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            double acc, svel, tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 3);
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 3);
        }
    }
}

   