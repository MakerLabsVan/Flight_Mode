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
    public partial class Jog : Form
    {
        int[] m_encp = new int[Global.MAX_NAXIS];
        int[] m1_encp = new int[Global.MAX_NAXIS];
        public Jog()
        {
            InitializeComponent();
        }

        private void Jog_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Global.g_naxis; i++)
            {
                comboBox1.Items.Add("轴" + i.ToString());
            }
            comboBox1.SelectedItem = comboBox1.SelectedIndex = 0;

        }
        //匀速运动
        private void button1_Click(object sender, EventArgs e)
        {
            int st;
            string err;
            int axis = comboBox1.SelectedIndex;
            double acc = Convert.ToDouble(textBox1.Text);
            double startvel = Convert.ToDouble(textBox2.Text);
            double tgvel = Convert.ToDouble(textBox3.Text);
            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }

            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);

            if (st != 0)
            {
                st = IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, startvel, tgvel, axis);
            }

            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }	

        }
        //停止匀速运动
        private void button2_Click(object sender, EventArgs e)
        {
            int st;
            string err;
            int axis = comboBox1.SelectedIndex;
            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }
            st = IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }	
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            int axis = comboBox1.SelectedIndex;
            double acc = Convert.ToDouble(textBox1.Text);
            double startvel = Convert.ToDouble(textBox2.Text);
            double tgvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, startvel, tgvel, axis);
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            int axis = comboBox1.SelectedIndex;
            double acc = Convert.ToDouble(textBox1.Text);
            double startvel = Convert.ToDouble(textBox2.Text);
            double tgvel = Convert.ToDouble(textBox3.Text);
            IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -startvel, -tgvel, axis);
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            int axis = comboBox1.SelectedIndex;
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, axis);
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            int axis = comboBox1.SelectedIndex;
            IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, axis);
        }

        private void Jog_KeyDown(object sender, KeyEventArgs e)
        {
            double acc,svel,tvel;
            IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
            acc = Convert.ToDouble(textBox1.Text);
            svel = Convert.ToDouble(textBox2.Text);
            tvel = Convert.ToDouble(textBox3.Text);
            switch (e.KeyCode)
            {
                case Keys.Q:
                    if (m_encp[0] <= 140000)
                    {
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 0);
                    }
                    else
                    {
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        Axisalert.Text += "Axis1 Max Limit Triggered!" + Environment.NewLine;
                    }
                    break;
                case Keys.A:
                    if (m_encp[0] >= 100)
                    {
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 0);
                    }
                    else
                    {
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                        IMC_Pkg.PKG_IMC_SetPos(Global.g_handle, 0, 0);
                        Axisalert.Text += "Axis1 Min Limit Triggered!" + Environment.NewLine;
                    }
                    break;
                case Keys.W:
                    if (m_encp[1] <= 140000)
                    {
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 1);
                    }
                    else
                    {
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        Axisalert.Text += "Axis2 Max Limit Triggered!" + Environment.NewLine;
                    }
                    break;
                case Keys.S:
                    if (m_encp[1] >= 100)
                    {
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 1);
                    }
                    else
                    {
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                        IMC_Pkg.PKG_IMC_SetPos(Global.g_handle, 0, 1);
                        Axisalert.Text += "Axis2 Min Limit Triggered!" + Environment.NewLine;
                    }
                    break;

                case Keys.E:
                     if (m_encp[2] <= 140000)
                    {
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 2);
                    }
                    else
                    {
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        Axisalert.Text += "Axis3 Max Limit Triggered!" + Environment.NewLine;
                    }
                    break;
                case Keys.D:
                     if (m_encp[2] >= 100)
                    {
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 2);
                    }
                    else
                    {
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                        IMC_Pkg.PKG_IMC_SetPos(Global.g_handle, 0, 2);
                        Axisalert.Text += "Axis3 Min Limit Triggered!" + Environment.NewLine;
                    }
                    break;
                case Keys.R:
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel, tvel, 3);
                    break;
                case Keys.F:
                        IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
                        IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel, -tvel, 3);
                    break;



            }
        }

        private void Jog_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                    break;
                case Keys.A:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 0);
                    break;
                case Keys.W:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                    break;
                case Keys.S:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 1);
                    break;
                case Keys.E:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                    break;
                case Keys.D:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 2);
                    break;
                case Keys.R:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 3);
                    break;
                case Keys.F:
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, 3);
                    break;
            }
        }
        private void Axisalert_TextChanged(object sender, EventArgs e)
        {
            Axisalert.SelectionStart = Axisalert.Text.Length;
            Axisalert.ScrollToCaret();
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
                    Axisalert.Text += "Axis Max Limit Triggered!" + Environment.NewLine;
                }
                if (m_encp[i] < 100 && m_encp[i] < m1_encp[i])
                {
                    IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, i);
                    Axisalert.Text += "Axis Min Limit Triggered!" + Environment.NewLine;
                }

            }
        }
    }
}
