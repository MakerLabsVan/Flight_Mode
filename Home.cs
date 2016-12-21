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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Global.g_naxis; i++)
            {
                comboBox1.Items.Add("Axis" + i.ToString());
            }
            comboBox1.SelectedItem = comboBox1.SelectedIndex = 0;
            comboBox2.SelectedItem = comboBox2.SelectedIndex = 0;
            comboBox3.SelectedItem = comboBox3.SelectedIndex = 0;
            comboBox4.SelectedItem = comboBox4.SelectedIndex = 0;

        }
        //搜零
        private void button1_Click(object sender, EventArgs e)
        {
            int st;
            string err;
            int axis = comboBox1.SelectedIndex;
            int mode = comboBox2.SelectedIndex;
            int dir = comboBox3.SelectedIndex;
            int riseEdge = comboBox4.SelectedIndex;
            double high = Convert.ToDouble(textBox1.Text);
            double low = Convert.ToDouble(textBox2.Text);
            double vel = Convert.ToDouble(textBox4.Text);
            int pos = Convert.ToInt32(textBox5.Text);
            int stpos = Convert.ToInt32(textBox6.Text);
            double acc = Convert.ToDouble(textBox3.Text);

            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }
            //设置搜零轴的加减速度
            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
                return;
            }
            //设置搜零轴的搜零速度
            st = IMC_Pkg.PKG_IMC_SetHomeVel(Global.g_handle, high, low, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
                return;
            }
            switch (mode)
            {
                case 0:
                    st = IMC_Pkg.PKG_IMC_HomeSwitch1(Global.g_handle, dir, riseEdge, pos, stpos, vel, 0, axis);
                    break;
                case 1:
                    st = IMC_Pkg.PKG_IMC_HomeSwitch2(Global.g_handle, dir, riseEdge, pos, stpos, vel, 0, axis);
                    break;
                case 2:
                    st = IMC_Pkg.PKG_IMC_HomeSwitch3(Global.g_handle, dir, riseEdge, pos, stpos, vel, 0, axis);
                    break;
                case 3:
                    st = IMC_Pkg.PKG_IMC_HomeSwitchIndex1(Global.g_handle, dir, riseEdge, pos, stpos, vel, 0, axis);
                    break;
                case 4:
                    st = IMC_Pkg.PKG_IMC_HomeSwitchIndex2(Global.g_handle, dir, riseEdge, pos, stpos, vel, 0, axis);
                    break;
                case 5:
                    st = IMC_Pkg.PKG_IMC_HomeSwitchIndex3(Global.g_handle, dir, riseEdge, pos, stpos, vel, 0, axis);
                    break;
                case 6:
                    st = IMC_Pkg.PKG_IMC_HomeIndex(Global.g_handle, dir, pos, stpos, vel, 0, axis);
                    break;
            }
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }

        }
        //停止搜零
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
            st = IMC_Pkg.PKG_IMC_HomeStop(Global.g_handle, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }
        }
        //设置位置
        private void button3_Click(object sender, EventArgs e)
        {
            int st;
            string err;
            int axis = comboBox1.SelectedIndex;
            int pos = Convert.ToInt32(textBox7.Text);
            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }
            //设置当前位置为指定值
            st = IMC_Pkg.PKG_IMC_SetPos(Global.g_handle, pos, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }
        }
    }
}
