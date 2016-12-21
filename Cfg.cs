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
        public struct CFG_INFO
        {
            public int ena;		    //使能驱动器
            public UInt32 steptime;	//脉冲宽度
            public int pulpolar;	//脉冲电平
            public int dirpolar;	//方向电平
            public double vellim;	//速度极限
            public double acclim;	//加速度极限
            public int plimitena;	//硬件限位
            public int plimitpolar;	//限位电平
            public int nlimitena;	//硬件限位
            public int nlimitpolar;	//限位电平
            public int almena;	    //伺服报警使能
            public int almpolar;	//伺服报警电平
            public int INPena;	    //伺服到位使能
            public int INPpolar;	//伺服到位电平
            public double encpfactor;	//反馈倍率
            public int encpena;	    //反馈使能
            public int encpmode;	//反馈计数模式
            public int encpdir;	    //反馈计数方向
	        int res;		        //保留
        }
    public partial class Cfg : Form
    {
        int m_selRow;
        public CFG_INFO[] m_cfg = new CFG_INFO[Global.MAX_NAXIS];

        public Cfg()
        {
            InitializeComponent();
        }

        private void Cfg_Load(object sender, EventArgs e)
        {
            int i, st;
            string err;

            m_selRow = -1;

            for (i = 0; i < Global.g_naxis; i++)
            {
                st = IMC_Pkg.PKG_IMC_GetConfig(Global.g_handle, ref m_cfg[i].steptime, ref m_cfg[i].pulpolar, ref m_cfg[i].dirpolar,
                    ref m_cfg[i].encpena, ref m_cfg[i].encpmode, ref m_cfg[i].encpdir, ref m_cfg[i].encpfactor,
                    ref m_cfg[i].vellim, ref m_cfg[i].acclim, ref m_cfg[i].ena, ref m_cfg[i].plimitena, ref m_cfg[i].plimitpolar, 
                    ref m_cfg[i].nlimitena, ref m_cfg[i].nlimitpolar,
                    ref m_cfg[i].almena, ref m_cfg[i].almpolar, ref m_cfg[i].INPena, ref m_cfg[i].INPpolar, i);
                if (st == 0)
                {
                    err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                    MessageBox.Show(err);
                    break;
                }
            }
            listView1.Columns.Add("轴号", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("脉冲宽度", 65, HorizontalAlignment.Left);
            listView1.Columns.Add("脉冲电平", 65, HorizontalAlignment.Left);
            listView1.Columns.Add("方向电平", 65, HorizontalAlignment.Left);
            listView1.Columns.Add("使能驱动器", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("编码器反馈", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("计数模式", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("计数正方向", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("反馈倍率", 65, HorizontalAlignment.Left);
            listView1.Columns.Add("加速度极限", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("速度极限", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("硬件正限位", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("正限位电平", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("硬件负限位", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("正限位电平", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("伺服报警", 65, HorizontalAlignment.Left);
            listView1.Columns.Add("报警电平", 65, HorizontalAlignment.Left);
            for (i = 0; i < Global.g_naxis; i++)
            {
                ListViewItem lvitem = listView1.Items.Add("轴" + i.ToString());
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                UpdateList(i, m_cfg[i]);
            }

        }

        //更新修改后的信息到列表中
        public void UpdateList(int row, CFG_INFO info)
        {
            listView1.Items[row].SubItems[1].Text = info.steptime.ToString();
            listView1.Items[row].SubItems[2].Text = info.pulpolar != 0 ? ("高电平") : ("低电平");
            listView1.Items[row].SubItems[3].Text = info.dirpolar != 0 ? ("高电平") : ("低电平");
            listView1.Items[row].SubItems[4].Text = info.ena != 0 ? ("使能") : ("禁止");
            listView1.Items[row].SubItems[5].Text = info.encpena != 0 ? ("使能") : ("禁止");
            listView1.Items[row].SubItems[6].Text = info.encpmode != 0 ? ("脉冲+方向") : ("正交信号");
            if (info.encpmode != 0)
                listView1.Items[row].SubItems[7].Text = info.encpdir != 0 ? ("高电平") : ("低电平");
            else
                listView1.Items[row].SubItems[7].Text = info.encpdir != 0 ? ("A超前B") : ("B超前A");
            listView1.Items[row].SubItems[8].Text = info.encpfactor.ToString("f4");
            listView1.Items[row].SubItems[9].Text = info.acclim.ToString("f4");
            listView1.Items[row].SubItems[10].Text = info.vellim.ToString("f4");
            listView1.Items[row].SubItems[11].Text = info.plimitena != 0 ? ("使能") : ("禁止");
            listView1.Items[row].SubItems[12].Text = info.plimitpolar != 0 ? ("高电平") : ("低电平");
            listView1.Items[row].SubItems[13].Text = info.nlimitena != 0 ? ("使能") : ("禁止");
            listView1.Items[row].SubItems[14].Text = info.nlimitpolar != 0 ? ("高电平") : ("低电平");
            listView1.Items[row].SubItems[15].Text = info.almena != 0 ? ("使能") : ("禁止");
            listView1.Items[row].SubItems[16].Text = info.almpolar != 0 ? ("高电平") : ("低电平");
        }
        //获得修改后的信息
        public void GetCfgInfo()
        {
            m_cfg[m_selRow].steptime = Convert.ToUInt32(textBox1.Text);
            m_cfg[m_selRow].pulpolar = comboBox1.SelectedIndex;
            m_cfg[m_selRow].dirpolar = comboBox2.SelectedIndex;
            m_cfg[m_selRow].ena = checkBox1.Checked ? 1 : 0;
            m_cfg[m_selRow].encpena = checkBox3.Checked ? 1 : 0;
            m_cfg[m_selRow].encpmode = comboBox3.SelectedIndex;
            m_cfg[m_selRow].encpdir = comboBox4.SelectedIndex;
            m_cfg[m_selRow].encpfactor = Convert.ToDouble(textBox4.Text);
            m_cfg[m_selRow].acclim = Convert.ToDouble(textBox2.Text);
            m_cfg[m_selRow].vellim = Convert.ToDouble(textBox3.Text);
            m_cfg[m_selRow].plimitena = checkBox4.Checked ? 1 : 0;
            m_cfg[m_selRow].plimitpolar = comboBox5.SelectedIndex;
            m_cfg[m_selRow].nlimitena = checkBox5.Checked ? 1 : 0;
            m_cfg[m_selRow].nlimitpolar = comboBox7.SelectedIndex;
            m_cfg[m_selRow].almena = checkBox2.Checked ? 1 : 0;
            m_cfg[m_selRow].almpolar = comboBox6.SelectedIndex;
        }
        //将新的信息显示到控件
        public void UpdateCfgInfo()
        {
            groupBox1.Text = "轴" + m_selRow.ToString();
	        textBox1.Text =  m_cfg[m_selRow].steptime.ToString();
	        comboBox1.SelectedIndex = m_cfg[m_selRow].pulpolar;
	        comboBox2.SelectedIndex = m_cfg[m_selRow].dirpolar;
	        checkBox1.Checked = m_cfg[m_selRow].ena != 0 ? true : false;
	        checkBox3.Checked = m_cfg[m_selRow].encpena != 0 ? true : false;
	        comboBox3.SelectedIndex = m_cfg[m_selRow].encpmode;
	        ChangeEncpDir(m_cfg[m_selRow].encpmode);
	        comboBox4.SelectedIndex = m_cfg[m_selRow].encpdir;
	        textBox4.Text = m_cfg[m_selRow].encpfactor.ToString("F4");
            textBox2.Text = m_cfg[m_selRow].acclim.ToString("F4");
            textBox3.Text = m_cfg[m_selRow].vellim.ToString("F4");
            checkBox4.Checked = m_cfg[m_selRow].plimitena != 0 ? true : false;
	        comboBox5.SelectedIndex = m_cfg[m_selRow].plimitpolar;
            checkBox5.Checked = m_cfg[m_selRow].nlimitena != 0 ? true : false;
	        comboBox7.SelectedIndex = m_cfg[m_selRow].nlimitpolar;
            checkBox2.Checked = m_cfg[m_selRow].almena != 0 ? true : false;
	        comboBox6.SelectedIndex = m_cfg[m_selRow].almpolar;
        }
        //反馈计数方向与反馈模式有关，
        public void ChangeEncpDir(int mode)
        {
            comboBox4.Items.Clear();
	        if(mode == 0)
	        {
                comboBox4.Items.Add(("B超前A为正"));
                comboBox4.Items.Add(("A超前B为正"));
	        }else{
                comboBox4.Items.Add(("方向是低电平为正"));
                comboBox4.Items.Add(("方向是高电平为正"));
	        }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_selRow < 0)
                return;
            int mode = comboBox3.SelectedIndex;
            ChangeEncpDir(mode);
            comboBox4.SelectedIndex = m_cfg[m_selRow].encpdir;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_selRow < 0)
                return;
            GetCfgInfo();
            UpdateList(m_selRow, m_cfg[m_selRow]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
	        int i, st = 0;
	        string err;
            if (!Global.isOpen())
	        {
		        MessageBox.Show(("设备没有打开！"));
		        return;
	        }
            st = IMC_Pkg.PKG_IMC_ClearIMC(Global.g_handle);
            for (i = 0; i < Global.g_naxis; i++)
	        {
                st = IMC_Pkg.PKG_IMC_ClearAxis(Global.g_handle, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetStopfilt(Global.g_handle, 1, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetExitfilt(Global.g_handle, 0, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetPulWidth(Global.g_handle, m_cfg[i].steptime, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetPulPolar(Global.g_handle, m_cfg[i].pulpolar, m_cfg[i].dirpolar, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetEncpEna(Global.g_handle, m_cfg[i].encpena, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetEncpMode(Global.g_handle, m_cfg[i].encpmode, m_cfg[i].encpdir, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetEncpRate(Global.g_handle, m_cfg[i].encpfactor, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetVelAccLim(Global.g_handle, m_cfg[i].vellim, m_cfg[i].acclim, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_Setlimit(Global.g_handle, m_cfg[i].plimitena, m_cfg[i].plimitpolar, m_cfg[i].nlimitena, m_cfg[i].nlimitpolar, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetAlm(Global.g_handle, m_cfg[i].almena, m_cfg[i].almpolar, i);
		        if(st == 0)
			        break;
                st = IMC_Pkg.PKG_IMC_SetEna(Global.g_handle, m_cfg[i].ena, i);//使能驱动器放在最后
		        if(st == 0)
			        break;
	        }
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (m_selRow != listView1.SelectedItems[0].Index)
                {
                    m_selRow = listView1.SelectedItems[0].Index;
                    UpdateCfgInfo();
                }
            }
            
        }
    }
}
