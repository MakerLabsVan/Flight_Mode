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
    public partial class Gear : Form
    {
        public Gear()
        {
            InitializeComponent();
        }

        private void Gear_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Global.g_naxis; i++)
            {
                comboBox1.Items.Add("轴" + i.ToString());
                comboBox3.Items.Add("轴" + i.ToString());
                comboBox4.Items.Add("轴" + i.ToString());
            }
            comboBox1.SelectedItem = comboBox1.SelectedIndex = 0;
            comboBox2.SelectedItem = comboBox2.SelectedIndex = 0;
            comboBox3.SelectedItem = comboBox3.SelectedIndex = 0;
            comboBox4.SelectedItem = comboBox4.SelectedIndex = 1;
        }
        //设置手轮
        private void button1_Click(object sender, EventArgs e)
        {
            int st;
            string err;
            int axis = comboBox1.SelectedIndex;
            int ratesel = comboBox2.SelectedIndex;
            double acc = Convert.ToDouble(textBox1.Text);
            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }
            double rate = ratesel == 0 ? 1 : ratesel == 1 ? 10 : 100;
            //设置电子手轮的加减速度
            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
                return;
            }
            //设置跟随手轮运动
            st = IMC_Pkg.PKG_IMC_HandWheel(Global.g_handle, rate, axis);
            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }
        }
        //脱离手轮
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
            st = IMC_Pkg.PKG_IMC_ExitGear(Global.g_handle, axis);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
	        }
        }
        //设置齿轮
        private void button3_Click(object sender, EventArgs e)
        {
            int st;
            string err;
            int master = comboBox3.SelectedIndex;
            int axis = comboBox4.SelectedIndex;
            double acc = Convert.ToDouble(textBox3.Text);
            double rate = Convert.ToDouble(textBox2.Text);
            if (!Global.isOpen())
	        {
                MessageBox.Show(("设备没有打开！"));
		        return;
	        }
	        if(master == axis){
                MessageBox.Show(("主动轴和从动轴的轴号不能相同！"));
		        return;
	        }
	        //设置从动轴的加减速度
            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
		        return;
	        }
	        //使从动轴跟随主动轴运动
            st = IMC_Pkg.PKG_IMC_Gear1(Global.g_handle, rate, master, axis);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
	        }
        }
        //脱离齿轮
        private void button4_Click(object sender, EventArgs e)
        {
	        int st;
            string err;
            int axis = comboBox4.SelectedIndex;
	        if(!Global.isOpen())
	        {
		        MessageBox.Show(("设备没有打开！"));
		        return;
	        }
	        st = IMC_Pkg.PKG_IMC_ExitGear(Global.g_handle, axis);
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
        //主动轴相对运动
        private void button5_Click(object sender, EventArgs e)
        {
	        int st;
	        string err;
            int master = comboBox3.SelectedIndex;
            double acc = Convert.ToDouble(textBox3.Text);
            int dist = Convert.ToInt32(textBox5.Text);
            double tgvel = Convert.ToDouble(textBox4.Text);
	        if(!Global.isOpen())
	        {
		        MessageBox.Show(("设备没有打开！"));
		        return;
	        }

	        //设置主动轴的加减速度
	        st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, master);
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
		        return;
	        }
	        //主动轴进行相对运动
	        st = IMC_Pkg.PKG_IMC_MoveDist(Global.g_handle, dist, 0, tgvel, 0, master);
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
    }
}
