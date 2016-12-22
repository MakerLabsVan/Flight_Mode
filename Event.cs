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
    public partial class Event : Form
    {
        ushort[] m_gout = new ushort[48];
        ushort[] m_gin = new ushort[32];
        ushort[] m_gout_s = new ushort[48];
        ushort[] m_gin_s = new ushort[32];

        public Event()
        {
            InitializeComponent();
        }

        private void Event_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Global.g_naxis; i++)
            {
                comboBox5.Items.Add("轴" + i.ToString());
            }
            for (i = 0; i < 32; i++)
            {
                comboBox1.Items.Add((i+1).ToString());
            }
            for (i = 0; i < 48; i++)
            {
                comboBox2.Items.Add((i + 1).ToString());
            }
            comboBox1.SelectedItem = comboBox1.SelectedIndex = 0;
            comboBox2.SelectedItem = comboBox2.SelectedIndex = 0;
            comboBox3.SelectedItem = comboBox3.SelectedIndex = 0;
            comboBox4.SelectedItem = comboBox4.SelectedIndex = 1;
            comboBox5.SelectedItem = comboBox5.SelectedIndex = 0;

            listView1.Columns.Add("Input Port No:", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Value", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("Input Port No:", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Value", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("Input Port No:", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Value", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("Input Port No:", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Value", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("Input Port No:", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Value", 40, HorizontalAlignment.Left);
            for (i = 0; i < 16; i++)
            {
                ListViewItem lvitem = listView1.Items.Add((i + 1).ToString("d2"));
                lvitem.SubItems.Add(("0"));
                lvitem.SubItems.Add((i + 17).ToString("d2"));
                lvitem.SubItems.Add(("0"));
                lvitem.SubItems.Add((i + 33).ToString("d2"));
                lvitem.SubItems.Add(("0"));
                lvitem.SubItems.Add((i + 1).ToString("d2"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add((i + 17).ToString("d2"));
                lvitem.SubItems.Add(("1"));
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i;
            if (!Global.isOpen())
                return;

            if (IMC_Pkg.PKG_IMC_GetGout(Global.g_handle, m_gout) != 0)
            {
                for (i = 0; i < 48; i++)
                {
                    if (m_gout[i] != m_gout_s[i])
                    {
                        listView1.Items[i % 16].SubItems[i / 16 * 2 + 1].Text = m_gout[i].ToString();
                        m_gout_s[i] = m_gout[i];
                    }
                }
            }
            if (IMC_Pkg.PKG_IMC_GetGin(Global.g_handle, m_gin) != 0)
            {
                for (i = 0; i < 32; i++)
                {
                    if (m_gin[i] != m_gin_s[i])
                    {
                        listView1.Items[i % 16].SubItems[i / 16 * 2 + 7].Text = m_gin[i].ToString();
                        m_gin_s[i] = m_gin[i];
                    }
                }
            }
        }
        //安装事件1
        private void button1_Click(object sender, EventArgs e)
        {
	        int insel, outsel, outst1, outst2;
	        EventInfo[] info = new EventInfo[3];
	        short ginLoc, goutLoc, inbit, outbit;
	        int st;
	        string err;

	        if(!Global.isOpen())
	        {
		        MessageBox.Show("Device Not Connected！");
		        return;
	        }
            insel = comboBox1.SelectedIndex;
            outsel = comboBox2.SelectedIndex;
            outst1 = comboBox3.SelectedIndex;
            outst2 = comboBox4.SelectedIndex;

	        if(insel < 16)//输入端口1-16对应寄存器gin1的每一位
                ginLoc = ParamDef.gin1Loc;
	        else//输入端口17-32对应寄存器gin2的每一位
                ginLoc = ParamDef.gin2Loc;

	        if(outsel < 16)//输出端口1-16对应寄存器gout1的每一位
                goutLoc = ParamDef.gout1Loc;
	        else if(outsel < 32)//输出端口17-32对应寄存器gout2的每一位
                goutLoc = ParamDef.gout2Loc;
	        else//输出端口33-48对应寄存器gout3的每一位
                goutLoc = ParamDef.gout3Loc;

	        inbit = (short)(insel % 16);
	        outbit = (short)(outsel % 16);

            //IMC_Allways类型的事件指令用于给后续非IMC_Allways事件作为条件判断，ginLoc & (1 << inbit)
            IMC_Pkg.PKG_Fill_AND16i(ref info[0], (short)IMC_EventType.IMC_Allways, ginLoc, 0, (short)(1 << inbit), 0, 0);


	        //输入端闭合时,电平为低电平，即第一条事件指令中ginLoc & (1 << inbit) == 0
	        //因此使用边沿事件类型IMC_Edge_Zero
	        if(outst1 == 0){//输出端选择输出高电平
		        //goutLoc = goutLoc | (1 << outbit)
                IMC_Pkg.PKG_Fill_OR16i(ref info[1], (short)IMC_EventType.IMC_Edge_Zero, goutLoc, 0, (short)(1 << outbit), goutLoc, 0);
	        }else{//输出端选择输出低电平
		        //goutLoc = goutLoc & (~(1 << outbit))
                IMC_Pkg.PKG_Fill_AND16i(ref info[1], (short)IMC_EventType.IMC_Edge_Zero, goutLoc, 0, (short)(~(1 << outbit)), goutLoc, 0);
	        }

	        //输入端断开时,电平为高电平，即第一条事件指令中ginLoc & (1 << inbit) != 0
	        //因此使用边沿事件类型IMC_Edge_NotZero
	        if(outst2 == 0){//输出端选择输出高电平
		        //goutLoc = goutLoc | (1 << outbit)
                IMC_Pkg.PKG_Fill_OR16i(ref info[2], (short)IMC_EventType.IMC_Edge_NotZero, goutLoc, 0, (short)(1 << outbit), goutLoc, 0);
	        }else{//输出端选择输出低电平
		        //goutLoc = goutLoc & (~(1 << outbit))
                IMC_Pkg.PKG_Fill_AND16i(ref info[2], (short)IMC_EventType.IMC_Edge_NotZero, goutLoc, 0, (short)(~(1 << outbit)), goutLoc, 0);
	        }

            st = IMC_Pkg.PKG_IMC_InstallEvent(Global.g_handle, info, 3);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
        //安装事件2
        private void button2_Click(object sender, EventArgs e)
        {
	        int st;
	        string err;
	        short axis = (short)comboBox5.SelectedIndex;
	        int pos = Convert.ToInt32(textBox1.Text);
	        EventInfo[] info = new EventInfo[4];

            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }

	        //IMC_Allways类型的事件指令用于给后续非IMC_Allways事件作为条件判断，
            IMC_Pkg.PKG_Fill_CMP32i(ref info[0], (short)IMC_EventType.IMC_Allways, ParamDef.curposLoc, axis, pos, 0, 0);

	        //当上一条IMC_Allways事件中curpos > pos时，即使用边沿事件类型IMC_Edge_Great
	        //停止匀速运动，
            IMC_Pkg.PKG_Fill_SET32(ref info[1], (short)IMC_EventType.IMC_Edge_Great, 0, ParamDef.mcstgvelLoc, axis);

	        //停止时设置位置为0(需要设置2个寄存器)//
	        //注意，这是开始停止时设置0，即将pos位置设为0（不会很精确）
            IMC_Pkg.PKG_Fill_SET32(ref info[2], (short)IMC_EventType.IMC_Edge_Great, 0, ParamDef.homeposLoc, axis);
            IMC_Pkg.PKG_Fill_SET16(ref info[3], (short)IMC_EventType.IMC_Edge_Great, -1, ParamDef.sethomeLoc, axis);

            st = IMC_Pkg.PKG_IMC_InstallEvent(Global.g_handle, info, 4);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }

        }
        //启动匀速运动
        private void button3_Click(object sender, EventArgs e)
        {
	        int st;
            string err;
            short axis = (short)comboBox5.SelectedIndex;
            double acc = Convert.ToDouble(textBox3.Text);
            double tgvel = Convert.ToDouble(textBox2.Text);
            if (!Global.isOpen())
	        {
		        MessageBox.Show(("设备没有打开！"));
		        return ;
	        }
            st = IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, axis);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
		        return;
	        }
            st = IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, tgvel, axis);
	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
        //同时安装事件1和事件2
        private void button4_Click(object sender, EventArgs e)
        {
	        int st;
	        string err;
            int insel, outsel, outst1, outst2;
	        short axis = (short)comboBox5.SelectedIndex;
	        int pos = Convert.ToInt32(textBox1.Text);
	        EventInfo[] info = new EventInfo[7];
	        short ginLoc, goutLoc, inbit, outbit;

	        if(!Global.isOpen())
	        {
		        MessageBox.Show(("Device Not Connected！"));
		        return;
	        }
            insel = comboBox1.SelectedIndex;
            outsel = comboBox2.SelectedIndex;
            outst1 = comboBox3.SelectedIndex;
            outst2 = comboBox4.SelectedIndex;

	        if(insel < 16)//输入端口1-16对应寄存器gin1的每一位
                ginLoc = ParamDef.gin1Loc;
	        else//输入端口17-32对应寄存器gin2的每一位
                ginLoc = ParamDef.gin2Loc;

	        if(outsel < 16)//输出端口1-16对应寄存器gout1的每一位
                goutLoc = ParamDef.gout1Loc;
	        else if(outsel < 32)//输出端口17-32对应寄存器gout2的每一位
                goutLoc = ParamDef.gout2Loc;
	        else//输出端口33-48对应寄存器gout3的每一位
                goutLoc = ParamDef.gout3Loc;

	        inbit = (short)(insel % 16);
	        outbit = (short)(outsel % 16);

	        //IMC_Allways类型的事件指令用于给后续非IMC_Allways事件作为条件判断，
            IMC_Pkg.PKG_Fill_AND16i(ref info[0], (short)IMC_EventType.IMC_Allways, ginLoc, 0, (short)(1 << inbit), 0, 0);


	        //输入端闭合时,电平为低电平，即第一条事件指令中ginLoc & (1 << inbit) == 0
	        //因此使用边沿事件类型IMC_Edge_Zero
	        if(outst1 == 0){//输出端选择输出高电平
		        //goutLoc = goutLoc | (1 << outbit)
                IMC_Pkg.PKG_Fill_OR16i(ref info[1], (short)IMC_EventType.IMC_Edge_Zero, goutLoc, 0, (short)(1 << outbit), goutLoc, 0);
	        }else{//输出端选择输出低电平
		        //goutLoc = goutLoc & (~(1 << outbit))
		        IMC_Pkg.PKG_Fill_AND16i(ref info[1], (short)IMC_EventType.IMC_Edge_Zero, goutLoc, 0, (short)(~(1 << outbit)), goutLoc, 0);
	        }

	        //输入端断开时,电平为高电平，即第一条事件指令中ginLoc & (1 << inbit) != 0
	        //因此使用边沿事件类型IMC_Edge_NotZero
	        if(outst2 == 0){//输出端选择输出高电平
		        //goutLoc = goutLoc | (1 << outbit)
		        IMC_Pkg.PKG_Fill_OR16i(ref info[2], (short)IMC_EventType.IMC_Edge_NotZero, goutLoc, 0, (short)(1 << outbit), goutLoc, 0);
	        }else{//输出端选择输出低电平
		        //goutLoc = goutLoc & (~(1 << outbit))
		        IMC_Pkg.PKG_Fill_AND16i(ref info[2], (short)IMC_EventType.IMC_Edge_NotZero, goutLoc, 0, (short)(~(1 << outbit)), goutLoc, 0);
	        }
	
	        //IMC_Allways类型的事件指令用于给后续非IMC_Allways事件作为条件判断，
            IMC_Pkg.PKG_Fill_CMP32i(ref info[3], (short)IMC_EventType.IMC_Allways, ParamDef.curposLoc, axis, pos, 0, 0);

	        //当上一条IMC_Allways事件中curpos > pos时，即使用边沿事件类型IMC_Edge_Great
	        //停止匀速运动，
            IMC_Pkg.PKG_Fill_SET32(ref info[4], (short)IMC_EventType.IMC_Edge_Great, 0, ParamDef.mcstgvelLoc, axis);
	        //停止时设置位置为0(需要设置2个寄存器)//
	        //注意，这是开始停止时设置0，即将pos位置设为0（不会很精确）
            IMC_Pkg.PKG_Fill_SET32(ref info[5], (short)IMC_EventType.IMC_Edge_Great, 0, ParamDef.homeposLoc, axis);
            IMC_Pkg.PKG_Fill_SET16(ref info[6], (short)IMC_EventType.IMC_Edge_Great, -1, ParamDef.sethomeLoc, axis);


	        st = IMC_Pkg.PKG_IMC_InstallEvent(Global.g_handle, info, 7);
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }

        }
        //卸载事件
        private void button5_Click(object sender, EventArgs e)
        {
	        int st;
	        string err;
	        st = IMC_Pkg.PKG_IMC_StopEvent(Global.g_handle);
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
    }
}
