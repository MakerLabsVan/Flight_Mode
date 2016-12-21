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
    public partial class InterP : Form
    {
        int m_thdoing;

        int[] m_axis = new int[Global.MAX_NAXIS];
	    double m_acc, m_tgvel, m_endvel, m_rate;
        int m_endx, m_endy, m_cx, m_cy, m_dir, m_segnum, m_axisCnt;
        int[] m_startpos = new int[2];
	    int[] m_pos = new int[Global.MAX_NAXIS];
        bool m_isAbs;

        TextBox[] m_posBox;
        CheckBox[] m_selBox;

        public InterP()
        {
            InitializeComponent();
        }

        private void InterP_Load(object sender, EventArgs e)
        {
            int i;

            m_thdoing = 0;

            for (i = 0; i < Global.g_naxis; i++)
            {
                comboBox1.Items.Add("轴" + i.ToString());
                comboBox2.Items.Add("轴" + i.ToString());
            }
            comboBox1.SelectedItem = comboBox1.SelectedIndex = 0;
            comboBox2.SelectedItem = comboBox2.SelectedIndex = 1;
            comboBox3.SelectedItem = comboBox3.SelectedIndex = 0;

            m_posBox = new TextBox[16];
            m_selBox = new CheckBox[16];
            m_posBox[0] = textBox10;
            m_posBox[1] = textBox11;
            m_posBox[2] = textBox12;
            m_posBox[3] = textBox13;
            m_posBox[4] = textBox14;
            m_posBox[5] = textBox15;
            m_posBox[6] = textBox16;
            m_posBox[7] = textBox17;
            m_posBox[8] = textBox18;
            m_posBox[9] = textBox19;
            m_posBox[10] = textBox20;
            m_posBox[11] = textBox21;
            m_posBox[12] = textBox22;
            m_posBox[13] = textBox23;
            m_posBox[14] = textBox24;
            m_posBox[15] = textBox25;
            m_selBox[0] = checkBox2;
            m_selBox[1] = checkBox3;
            m_selBox[2] = checkBox4;
            m_selBox[3] = checkBox5;
            m_selBox[4] = checkBox6;
            m_selBox[5] = checkBox7;
            m_selBox[6] = checkBox8;
            m_selBox[7] = checkBox9;
            m_selBox[8] = checkBox10;
            m_selBox[9] = checkBox11;
            m_selBox[10] = checkBox12;
            m_selBox[11] = checkBox13;
            m_selBox[12] = checkBox14;
            m_selBox[13] = checkBox15;
            m_selBox[14] = checkBox16;
            m_selBox[15] = checkBox17;
            for (i = 4; i < Global.g_naxis; i++)
            {
                m_posBox[i].Visible = true;
                m_selBox[i].Visible = true;
            }
            for (; i < 16; i++)
            {//隐藏多余的控件
                m_posBox[i].Visible = false;
                m_selBox[i].Visible = false;
            }
        }

        int SetPFIFO(IntPtr handle, double acc, double rate, int fifo)
        {
            int st;
            st = IMC_Pkg.PKG_IMC_PFIFOrun(handle, fifo);			//启用插补空间
            if (st == 0)
                return st;
            st = IMC_Pkg.PKG_IMC_SetPFIFOaccel(handle, acc, fifo);	//设置加速度
            if (st == 0)
                return st;
            st = IMC_Pkg.PKG_IMC_SetFeedrate(handle, rate, fifo);	//设置进给倍率
            return st;
        }
        //启动圆弧插补
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Global.isOpen())
            {
                MessageBox.Show("设备没有打开！");
                return;
            }
            if (m_thdoing == 0)
            {
                m_axisCnt = 2;
	            m_axis[0] = comboBox1.SelectedIndex;
	            m_axis[1] = comboBox2.SelectedIndex;
	            m_dir = comboBox3.SelectedIndex;
	            if(m_axis[0] == m_axis[1])
	            {
		            MessageBox.Show(("X轴和Y轴不能选择相同的轴号"));
		            return;
	            }
	            m_startpos[0] = Convert.ToInt32(textBox1.Text);
	            m_startpos[1] = Convert.ToInt32(textBox2.Text);
	            m_endx = Convert.ToInt32(textBox3.Text);
	            m_endy = Convert.ToInt32(textBox4.Text);
	            m_cx = Convert.ToInt32(textBox5.Text);
	            m_cy = Convert.ToInt32(textBox6.Text);

                m_tgvel = Convert.ToDouble(textBox8.Text);
                m_acc = Convert.ToDouble(textBox7.Text);
                m_rate = Convert.ToDouble(textBox9.Text);
	            m_endvel = 0;
                //需要等待圆弧插补运动完成，这会阻塞主线程使程序看起来像无响应状态
                //因此需要使用线程
                Thread a = new Thread(ArcThread);
                a.Start();
            }
            else
            {
                IMC_Pkg.PKG_IMC_ExitWait();
                IMC_Pkg.PKG_IMC_PFIFOstop(Global.g_handle, (int)FIFO_SEL.SEL_PFIFO1);
            }
        }
        //圆弧插补线程
        public void  ArcThread()
        {
	        int st, fifo;
	        string err;
            
	        m_thdoing = 1;
	        Button1Ch("停止");

            fifo = (int)FIFO_SEL.SEL_PFIFO1;    //使用插补空间1

            st = IMC_Pkg.PKG_IMC_PFIFOclear(Global.g_handle, fifo);	//清空PFIFO

	        if(st != 0){
                st = SetPFIFO(Global.g_handle, m_acc, m_rate, fifo);
	        }

	        if(st != 0){
                st = IMC_Pkg.PKG_IMC_AxisMap(Global.g_handle, m_axis, m_axisCnt, fifo);//映射轴
	        }

	        if(st != 0){
                //先移动到起点位置（不等待完成，使函数立即返回）
                st = IMC_Pkg.PKG_IMC_Line_Pos(Global.g_handle, m_startpos, m_axisCnt, m_tgvel, m_tgvel, 0, fifo);
	        }

	        if(st != 0){//函数等待运动完成后才返回
                st = IMC_Pkg.PKG_IMC_Arc_Pos(Global.g_handle, m_endx, m_endy, m_cx, m_cy, m_dir, m_tgvel, m_endvel, 1, fifo);
	        }

	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }

	        Button1Ch("启动圆弧插补");
	        m_thdoing = 0;
        }
        //启动圆弧直线插补
        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            if (!Global.isOpen())
            {
                MessageBox.Show("设备没有打开！");
                return;
            }
            if (m_thdoing == 0)
            {
                m_axis[0] = comboBox1.SelectedIndex;
                m_axis[1] = comboBox2.SelectedIndex;
                m_dir = comboBox3.SelectedIndex;
                if (m_axis[0] == m_axis[1])
                {
                    MessageBox.Show(("X轴和Y轴不能选择相同的轴号"));
                    return;
                }
                m_axisCnt = 2;

                for (i = 0; i < Global.g_naxis; i++)
                {//获得直线的位置
                    if (m_selBox[i].Checked)
                    {
                        m_axis[m_axisCnt] = i;
                        m_pos[m_axisCnt - 2] = Convert.ToInt32(m_posBox[i].Text);
                        m_axisCnt++;
                    }
                }
                for (i = 2; i < m_axisCnt; i++)
                {
                    if (m_axis[i] == m_axis[0] || m_axis[i] == m_axis[1])
                    {
                        MessageBox.Show(("参与直线运动的轴不能与参与圆弧插补选择相同的轴号"));
                        return;
                    }
                }
                m_startpos[0] = Convert.ToInt32(textBox1.Text);
                m_startpos[1] = Convert.ToInt32(textBox2.Text);
                m_endx = Convert.ToInt32(textBox3.Text);
                m_endy = Convert.ToInt32(textBox4.Text);
                m_cx = Convert.ToInt32(textBox5.Text);
                m_cy = Convert.ToInt32(textBox6.Text);

                m_tgvel = Convert.ToDouble(textBox8.Text);
                m_acc = Convert.ToDouble(textBox7.Text);
                m_rate = Convert.ToDouble(textBox9.Text);
                m_endvel = 0;
                //需要等待圆弧插补运动完成，这会阻塞主线程使程序看起来像无响应状态
                //因此需要使用线程
                Thread a = new Thread(ArcLineThread);
                a.Start();
            }
            else
            {
                IMC_Pkg.PKG_IMC_ExitWait();
                IMC_Pkg.PKG_IMC_PFIFOstop(Global.g_handle, (int)FIFO_SEL.SEL_PFIFO1);
            }
        }
        //圆弧直线线程
        private void ArcLineThread()
        {
	        int st, fifo;
	        string err;


            fifo = (int)FIFO_SEL.SEL_PFIFO1;//使用插补空间 1

	        m_thdoing = 1;
	        Button2Ch("停止");

            st = IMC_Pkg.PKG_IMC_PFIFOclear(Global.g_handle, fifo);	//清空PFIFO

	        if(st != 0){
                st = SetPFIFO(Global.g_handle, m_acc, m_rate, fifo);
	        }

	        if(st != 0){
                st = IMC_Pkg.PKG_IMC_AxisMap(Global.g_handle, m_axis, m_axisCnt, fifo);//映射轴
	        }

	        if(st != 0){
		        //先移动到圆弧起点位置（不等待完成，使函数立即返回）
                st = IMC_Pkg.PKG_IMC_Line_Pos(Global.g_handle, m_startpos, 2, m_tgvel, m_tgvel, 0, fifo);
	        }

	        if(st != 0)
            {//函数等待运动完成后才返回
                st = IMC_Pkg.PKG_IMC_ArcLine_Pos(Global.g_handle, m_endx, m_endy, m_cx, m_cy, m_dir, m_pos, m_axisCnt - 2, m_tgvel, 0, 1, fifo);
	        }

	        if(st == 0)
	        {
                err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
	        Button2Ch("启动圆弧直线插补");
	        m_thdoing = 0;
        }
        //启动直线插补
        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            if (!Global.isOpen())
            {
                MessageBox.Show("设备没有打开！");
                return;
            }
            if (m_thdoing == 0)
            {
                m_axisCnt = 0;

                for (i = 0; i < Global.g_naxis; i++)
                {//获得直线的位置
                    if (m_selBox[i].Checked)
                    {
                        m_axis[m_axisCnt] = i;
                        m_pos[m_axisCnt] = Convert.ToInt32(m_posBox[i].Text);
                        m_axisCnt++;
                    }
                }
                if (m_axisCnt == 0)
                {
                    MessageBox.Show("请先选择参与直线插补的轴");
                    return ;
                }

                m_segnum = Convert.ToInt32(textBox26.Text);
                if (m_segnum < 1)
                    m_segnum = 1;
                m_isAbs = checkBox1.Checked;

                m_tgvel = Convert.ToDouble(textBox8.Text);
                m_acc = Convert.ToDouble(textBox7.Text);
                m_rate = Convert.ToDouble(textBox9.Text);
                m_endvel = 0;
                //需要等待圆弧插补运动完成，这会阻塞主线程使程序看起来像无响应状态
                //因此需要使用线程
                Thread l = new Thread(LineThread);
                l.Start();
            }
            else
            {
                IMC_Pkg.PKG_IMC_ExitWait();
                IMC_Pkg.PKG_IMC_PFIFOstop(Global.g_handle, (int)FIFO_SEL.SEL_PFIFO1);
            }
        }
        //直线插补线程
        private void LineThread()
        {
	        int st, i, n, fifo;
	        string err;
            int[] segpos;

            segpos = new int[m_segnum * m_axisCnt];
            for (i = 0; i < m_segnum; i++)
            {
                for (n = 0; n < m_axisCnt; n++)
                    segpos[i * m_axisCnt + n] = m_pos[n];
            }
            
            fifo = (int)FIFO_SEL.SEL_PFIFO1;//使用插补空间 1

	        m_thdoing = 1;
	        Button3Ch("停止");

	        st = IMC_Pkg.PKG_IMC_PFIFOclear(Global.g_handle, fifo);	//清空PFIFO

	        if(st != 0){
		        st = SetPFIFO(Global.g_handle, m_acc, m_rate, fifo);
	        }
	        if(st != 0){
		        st = IMC_Pkg.PKG_IMC_AxisMap(Global.g_handle, m_axis, m_axisCnt, fifo);//映射轴
	        }
	        if(st != 0){
		        if(m_isAbs)//使用绝对坐标
		        {
			        if(m_segnum > 1)
				        st = IMC_Pkg.PKG_IMC_MulLine_Pos(Global.g_handle, segpos, m_axisCnt, m_segnum, m_tgvel, 0, 1, fifo);
			        else
				        st = IMC_Pkg.PKG_IMC_Line_Pos(Global.g_handle, m_pos, m_axisCnt, m_tgvel, 0, 1, fifo);
		        }else{//使用相对坐标
			        if(m_segnum > 1)
				        st = IMC_Pkg.PKG_IMC_MulLine_Dist(Global.g_handle, segpos, m_axisCnt, m_segnum, m_tgvel, 0, 1, fifo);
			        else
				        st = IMC_Pkg.PKG_IMC_Line_Dist(Global.g_handle, m_pos, m_axisCnt, m_tgvel, 0, 1, fifo);
		        }
	        }
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
	        Button3Ch("启动直线插补");
	        m_thdoing = 0;
	        return;
        }

        private delegate void FlushButton(string str); //代理
        private void Button1Ch(string str)
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
        private void Button3Ch(string str)
        {
            if (this.button3.InvokeRequired)//等待异步
            {
                FlushButton fc = new FlushButton(Button3Ch);
                this.Invoke(fc, str); //通过代理调用刷新方法
            }
            else
            {
                this.button3.Text = str;
            }
        }
        //改变进给倍率（在进行插补的过程中改变才有效）
        private void button4_Click(object sender, EventArgs e)
        {            
	        int st;
	        string err;
	        if(!Global.isOpen())
	        {
		        MessageBox.Show(("设备没有打开！"));
		        return;
	        }
	
            m_rate = Convert.ToDouble(textBox9.Text);
	        st = IMC_Pkg.PKG_IMC_SetFeedrate(Global.g_handle, m_rate, (int)FIFO_SEL.SEL_PFIFO1);	//设置进给倍率
	        if(st == 0)
	        {
		        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
		        MessageBox.Show(err);
	        }
        }
    }
}
