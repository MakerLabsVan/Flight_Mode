using System;
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
        public Flight()
        {
            InitializeComponent();
        }

        private void Flight_KeyDown(object sender, KeyEventArgs e)
        {
            double acc, svel, tvel;
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.X))
            {
                IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
                acc = Convert.ToDouble(textBox1.Text);
                svel = Convert.ToDouble(textBox2.Text);
                tvel = Convert.ToDouble(textBox3.Text);
                switch (e.KeyCode)
                {
                    case Keys.W:
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
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
                        break;
                    case Keys.S:
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
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
                        break;
                    case Keys.A:
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
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
                        break;
                    case Keys.D:
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
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
                        break;
                    case Keys.Z:
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
                        /*if (IMC_Fun.m_encp[0] < 40000)
                        {
                            IMC_Fun.Jog(vel, 0);
                        }
                        else
                        {
                            IMC_Fun.Jog(0, 0);
                        }
                        if (IMC_Fun.m_encp[1] < 40000)
                        {
                            IMC_Fun.Jog(vel, 1);
                        }
                        else
                        {
                            IMC_Fun.Jog(0, 1);
                        }
                        if (IMC_Fun.m_encp[2] < 40000)
                        {
                            IMC_Fun.Jog(vel, 2);
                        }
                        else
                        {
                            IMC_Fun.Jog(0, 2);
                        }*/
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 140000, svel, tvel, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 140000, svel, tvel, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 140000, svel, tvel, 0, 2);
                        break;
                    case Keys.X:
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
                        /*if (IMC_Fun.m_encp[0] > 100)
                        {
                            IMC_Fun.Jog(-vel, 0);
                        }
                        else
                        {
                            IMC_Fun.Jog(0, 0);
                            if (IMC_Fun.m_encp[0] < 0)
                            {
                                IMC_Fun.SetParam16(ParamDef.sethomeLoc, -1, 0, FIFO_SEL.SEL_IFIFO);
                            }
                        }
                        if (IMC_Fun.m_encp[1] > 100)
                        {
                            IMC_Fun.Jog(-vel, 1);
                        }
                        else
                        {
                            IMC_Fun.Jog(0, 1);
                            if (IMC_Fun.m_encp[1] < 0)
                            {
                                IMC_Fun.SetParam16(ParamDef.sethomeLoc, -1, 1, FIFO_SEL.SEL_IFIFO);
                            }
                        }
                        if (IMC_Fun.m_encp[2] > 100)
                        {
                            IMC_Fun.Jog(-vel, 2);
                        }
                        else
                        {
                            IMC_Fun.Jog(0, 2);
                            if (IMC_Fun.m_encp[2] < 0)
                            {
                                IMC_Fun.SetParam16(ParamDef.sethomeLoc, -1, 2, FIFO_SEL.SEL_IFIFO);
                            }
                        }*/
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 0, svel, tvel, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 0, svel, tvel, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, 0, svel, tvel, 0, 2);
                        break;
                    case Keys.E:
                        Flightalert.Text += "Hello!";
                        break;

                }
            }
        }

        private void Flight_KeyUp(object sender, KeyEventArgs e)
        {
            double acc, svel, tvel;
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.X))
            {
                IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
                acc = Convert.ToDouble(textBox1.Text);
                svel = Convert.ToDouble(textBox2.Text);
                tvel = Convert.ToDouble(textBox3.Text);
                switch (e.KeyCode)
                {
                    case Keys.W:
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
                        break;
                    case Keys.A:
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
                        break;
                    case Keys.S:
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
                        break;
                    case Keys.D:
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, svel, tvel, 0, 2);
                        break;
                    case Keys.Z:
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        flight_level = m_encp.Max();
                        break;
                    case Keys.X:
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
                        IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        flight_level = m_encp.Max();
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
                if (m_encp[i] > 140000 && m_encp[i] > m1_encp[i])
                {
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, i);
                    Flightalert.Text += "Axis Max Limit Triggered!" + Environment.NewLine;
                }
                if (m_encp[i] < 100 && m_encp[i] < m1_encp[i])
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
    }
}

   