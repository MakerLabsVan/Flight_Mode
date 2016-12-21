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
    public partial class Form1 : Form
    {

        int[] m_encp = new int[Global.MAX_NAXIS];
        int[] m_curpos = new int[Global.MAX_NAXIS];
        ushort[] m_error = new ushort[Global.MAX_NAXIS];
        int[] m_encp_t = new int[Global.MAX_NAXIS];
        int[] m_curpos_t = new int[Global.MAX_NAXIS];
        ushort[] m_error_t = new ushort[Global.MAX_NAXIS];


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            byte[] info = new byte[16*256];
	        int num = 0, i;
            string str;

            
	        //程序初始化时搜索PC中的网卡
	        if(IMC_Pkg.PKG_IMC_FindNetCard (info, ref num) != 0)
	        {
                for (i = 0; i < num; i++)
                {
                    str = System.Text.Encoding.Default.GetString(info, i * 256, 256);
			        comboBox1.Items.Add(str);
		        }
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedItem = comboBox1.SelectedIndex = 0; 
	        }
		    for(i=0; i<64; i++)
            {
                str = i.ToString();
                comboBox2.Items.Add(str);
            }
            comboBox2.SelectedItem = comboBox2.SelectedIndex = 0;

            listView1.Columns.Add("轴号", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("指令位置（脉冲）", 140, HorizontalAlignment.Left);
            listView1.Columns.Add("机械位置（脉冲）", 140, HorizontalAlignment.Left);
            listView1.Columns.Add("错误寄存器", 140, HorizontalAlignment.Left);
        }

        //打开设备
        private void button1_Click(object sender, EventArgs e)
        {
	        int netid, imcid, i;
	        string err;
	        netid = comboBox1.SelectedIndex;
	        imcid = comboBox2.SelectedIndex;
            if (Global.isOpen())//设备已打开则关闭
	        {
		        IMC_Pkg.PKG_IMC_Close(Global.g_handle);
                Global.g_handle = IntPtr.Zero;
		        button1.Text = "打开设备";
		        timer1.Enabled = false;
	        }else{
                Global.g_handle = IMC_Pkg.PKG_IMC_Open(netid, imcid);
                if (Global.isOpen())
		        {
			        button1.Text = ("关闭设备");
                    i = IMC_Pkg.PKG_IMC_InitCfg(Global.g_handle);
			        if(i == 0)
			        {
				        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
				        MessageBox.Show(err);
			        }
		            timer1.Enabled = true;	//启动定时器，定时读取位置信息

                    Global.g_naxis = IMC_Pkg.PKG_IMC_GetNaxis(Global.g_handle);//获得控制卡最大轴数
                    if (Global.g_naxis > 0)
			        {
				        listView1.Items.Clear();
                        for (i = 0; i < Global.g_naxis; i++)
				        {		
					        ListViewItem lvitem = listView1.Items.Add("轴" + i.ToString());
                            lvitem.SubItems.Add(("0"));
                            lvitem.SubItems.Add(("0"));
                            lvitem.SubItems.Add(("0x0000"));
				        }
			        }else{
				        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
				        MessageBox.Show(err);
			        }

                }
                else
                {
			        MessageBox.Show(("无法打开设备！\r\n\r\n请检查网卡和控制卡ID是否选择正确！"));
		        }
	        }

        }
        //配置
        private void button2_Click(object sender, EventArgs e)
        {
            Cfg cfgForm = new Cfg();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            cfgForm.ShowDialog(); 
        }
        //点到点运动
        private void button3_Click(object sender, EventArgs e)
        {
            P2P posForm = new P2P();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            posForm.ShowDialog(); 
        }
        //匀速运动或点动
        private void button4_Click(object sender, EventArgs e)
        {
            Jog jogForm = new Jog();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            jogForm.ShowDialog(); 
        }
        //插补运动
        private void button5_Click(object sender, EventArgs e)
        {
            InterP interpForm = new InterP();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            interpForm.ShowDialog(); 
        }
        //搜索零点
        private void button6_Click(object sender, EventArgs e)
        {
            Home homeForm = new Home();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            homeForm.ShowDialog(); 
        }
        //输入输出操作
        private void button7_Click(object sender, EventArgs e)
        {
            IO ioForm = new IO();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            ioForm.ShowDialog(); 
        }
        //齿轮
        private void button8_Click(object sender, EventArgs e)
        {
            Gear gearForm = new Gear();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            gearForm.ShowDialog(); 
        }
        //事件
        private void button9_Click(object sender, EventArgs e)
        {
            Event eventForm = new Event();
            if (!Global.isOpen())
            {
                MessageBox.Show("请先打开设备!");
                return;
            }
            eventForm.ShowDialog(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i;
            if (!Global.isOpen())
                return;
         //   listView1.BeginUpdate();

            if (IMC_Pkg.PKG_IMC_GetCurpos(Global.g_handle, m_curpos, Global.g_naxis) != 0)//获得指令位置
            {
                for (i = 0; i < Global.g_naxis; i++)
                {
                    if (m_curpos[i] != m_curpos_t[i])
                    {
                        m_curpos_t[i] = m_curpos[i];
                        listView1.Items[i].SubItems[1].Text = m_curpos[i].ToString();
                    }
                }
            }
            if (IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis) != 0)//获得反馈编码器位置
            {
                for (i = 0; i < Global.g_naxis; i++)
                {
                    if (m_encp[i] != m_encp_t[i])
                    {
                        m_encp_t[i] = m_encp[i];
                        listView1.Items[i].SubItems[2].Text = m_encp[i].ToString("D");
                    }
                }
            }
            if (IMC_Pkg.PKG_IMC_GetErrorReg(Global.g_handle, m_error, Global.g_naxis) != 0)//获得错误寄存器值
            {
                for (i = 0; i < Global.g_naxis; i++)
                {
                    if (m_error[i] != m_error_t[i])
                    {
                        m_error_t[i] = m_error[i];
                        listView1.Items[i].SubItems[3].Text = "0x" + m_error[i].ToString("X4");
                    }
                }
            }
      //      listView1.EndUpdate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Flight Flightform = new Flight();
            Flightform.ShowDialog();
        }
    }
}
