using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using imcpkg;

namespace Example
{
    public partial class P2P : Form
    {
        int m_thdoing1;
        int m_thdoing2;

        int m_axis;
        double m_acc, m_startvel, m_tgvel;
        int m_dist, m_pos;

        public P2P()
        {
            InitializeComponent();
        }
        private void P2P_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Global.g_naxis; i++)
            {
                comboBox1.Items.Add("轴" + i.ToString());
            }
            comboBox1.SelectedItem = comboBox1.SelectedIndex = 0;

            m_thdoing1 = 0;
            m_thdoing2 = 0;
        }
        //启动绝对运动
        private void button1_Click(object sender, EventArgs e)
        {
            int []curpos = new int[16];
            if (!Global.isOpen())
	        {
		        MessageBox.Show("设备没有打开！");
		        return ;
	        }
            if (m_thdoing1 != 0)
            {
                //这里只是退出了等待，并没有停止绝对运动
                //如果要退出等待的同时，也停止运动，需要调用PKG_IMC_P2Pstop函数
                IMC_Pkg.PKG_IMC_ExitWait();
                return;
            }
            m_axis = comboBox1.SelectedIndex;
            m_acc = Convert.ToDouble(textBox1.Text);
            m_startvel = Convert.ToDouble(textBox2.Text);
            m_tgvel = Convert.ToDouble(textBox3.Text);
            m_pos = Convert.ToInt32(textBox4.Text);


            Thread p1 = new Thread(AbsThread);
            p1.Start();
        }
        //绝对运动线程
        public void AbsThread()
        {
	        int st;
	        string err;
	        m_thdoing1 = 1;
            Button1Ch("退出等待");

            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, m_acc, m_acc, m_axis);
	        if(st != 0)
	        {
                st = IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, m_pos, m_startvel, m_tgvel, 1, m_axis);
	        }

	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
	        m_thdoing1 = 0;
            Button1Ch("启动绝对运动");
	        return ;
        }
        //启动相对运动
        private void button2_Click(object sender, EventArgs e)
        {
            if (!Global.isOpen())
            {
                MessageBox.Show("设备没有打开！");
                return;
            }
            if (m_thdoing2 != 0)
            {
                //这里只是退出了等待，并没有停止相对运动
                //如果要退出等待的同时，也停止运动，需要调用PKG_IMC_P2Pstop函数
                IMC_Pkg.PKG_IMC_ExitWait();
                return;
            }
            m_axis = comboBox1.SelectedIndex;
            m_acc = Convert.ToDouble(textBox1.Text);
            m_startvel = Convert.ToDouble(textBox2.Text);
            m_tgvel = Convert.ToDouble(textBox3.Text);
            m_dist = Convert.ToInt32(textBox5.Text);

            Thread p2 = new Thread(DistThread);
            p2.Start();
        }
        //相对运动线程
        public void DistThread()
        {
            int st;
            string err;

            m_thdoing2 = 1;
            Button2Ch("退出等待");

            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, m_acc, m_acc, m_axis);
            if (st != 0)
            {
                st = IMC_Pkg.PKG_IMC_MoveDist(Global.g_handle, m_dist, m_startvel, m_tgvel, 1, m_axis);
            }

            if (st == 0)
            {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                MessageBox.Show(err);
            }
            m_thdoing2 = 0;
            Button2Ch("启动相对运动");
            return;
        }

        private delegate void FlushButton(string str); //代理
        private void Button1Ch( string str)
        {
            if (this.button1.InvokeRequired)//等待异步
            {
                FlushButton fc = new FlushButton(Button1Ch);
                this.Invoke(fc, str); //通过代理调用刷新方法
            }
            else
            {
                this.button1.Text = str;
            }
        }
        private void Button2Ch(string str)
        {
            if (this.button2.InvokeRequired)//等待异步
            {
                FlushButton fc = new FlushButton(Button2Ch);
                this.Invoke(fc, str); //通过代理调用刷新方法
            }
            else
            {
                this.button2.Text = str;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {            
	        int st;
	        string err;
	        if(!Global.isOpen())
	        {
		        MessageBox.Show(("设备没有打开！"));
		        return;
	        }

            m_axis = comboBox1.SelectedIndex;
            m_tgvel = Convert.ToDouble(textBox3.Text);
            st = IMC_Pkg.PKG_IMC_P2Pvel(Global.g_handle, m_tgvel, m_axis);
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
    }
}
