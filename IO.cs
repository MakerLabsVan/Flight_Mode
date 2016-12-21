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
    public partial class IO : Form
    {
	    ushort[] m_gin = new ushort[32];
	    ushort[] m_gin_s = new ushort[32];
	    ushort[] m_gout = new ushort[48];
	    ushort[] m_gout_s = new ushort[48];
        ushort[,] m_aio = new ushort[Global.MAX_NAXIS, 6];
        ushort[,] m_aio_s = new ushort[Global.MAX_NAXIS, 6];

        public IO()
        {
            InitializeComponent();
        }

        private void IO_Load(object sender, EventArgs e)
        {
            int i;
            listView1.Columns.Add("轴号", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("正限位", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("负限位", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("原点", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("探针", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("报警", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("到位", 60, HorizontalAlignment.Left);

            listView2.Columns.Add("输出端口号", 80, HorizontalAlignment.Right);
            listView2.Columns.Add("值", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("输出端口号", 80, HorizontalAlignment.Right);
            listView2.Columns.Add("值", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("输出端口号", 80, HorizontalAlignment.Right);
            listView2.Columns.Add("值", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("输入端口号", 80, HorizontalAlignment.Right);
            listView2.Columns.Add("值", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("输入端口号", 80, HorizontalAlignment.Right);
            listView2.Columns.Add("值", 40, HorizontalAlignment.Left);

            for (i = 0; i < Global.g_naxis; i++)
            {
                ListViewItem lvitem = listView1.Items.Add("轴" + i.ToString());
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
                lvitem.SubItems.Add(("1"));
            }
            for (i = 0; i < 16; i++)
            {
                ListViewItem lvitem = listView2.Items.Add((i + 1).ToString("d2"));
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

            switch (Global.g_naxis)
            {
                case 4:
                case 8:
                case 12:
                case 16:
                    button41.Enabled = true;
                    button42.Enabled = true;
                    button43.Enabled = true;
                    button44.Enabled = true;
                    button45.Enabled = true;
                    button46.Enabled = true;
                    button47.Enabled = true;
                    button48.Enabled = true;
                    break;
                case 6:
                case 10:
                case 14:
                    button41.Enabled = false;
                    button42.Enabled = false;
                    button43.Enabled = false;
                    button44.Enabled = false;
                    button45.Enabled = false;
                    button46.Enabled = false;
                    button47.Enabled = false;
                    button48.Enabled = false;
                    break;
            }
	
            timer1.Enabled = true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            int i;
            int outdata;
            int st;
            string err;
            Button btn = (Button)sender;

            if (!Global.isOpen())
            {
                MessageBox.Show(("设备没有打开！"));
                return;
            }

            for (i = 0; i < 48; i++)
            {
                if (btn.Text == (i + 1).ToString())
                {
                    outdata = m_gout_s[i];
                    outdata = (outdata == 0) ? 1 : 0;
                    st = IMC_Pkg.PKG_IMC_SetOut(Global.g_handle, i + 1, outdata, (int)FIFO_SEL.SEL_IFIFO);
                    if (st == 0)
                    {
                        err = IMC_Pkg.PKG_IMC_GetFunErrStr();
                        MessageBox.Show(err);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i, k;
            if (!Global.isOpen())
                return;
            if (IMC_Pkg.PKG_IMC_GetAin(Global.g_handle, m_aio, Global.g_naxis) != 0)
            {
                for (i = 0; i < Global.g_naxis; i++)
                {
                    for (k = 0; k < 6; k++)
                    {
                        if (m_aio[i,k] != m_aio_s[i,k])
                        {
                            if (m_aio[i, k] != 0)
                            {
                                listView1.Items[i].SubItems[k + 1].Text = "1";
                            }
                            else
                                listView1.Items[i].SubItems[k + 1].Text = "0";
                            m_aio_s[i, k] = m_aio[i, k];
                        }
                    }
                }
            }
            if (IMC_Pkg.PKG_IMC_GetGout(Global.g_handle, m_gout) != 0)
            {
                for (i = 0; i < 48; i++)
                {
                    if (m_gout[i] != m_gout_s[i])
                    {
                        listView2.Items[i % 16].SubItems[i / 16 * 2 + 1].Text = m_gout[i].ToString();
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
                        listView2.Items[i % 16].SubItems[i / 16 * 2 + 7].Text = m_gin[i].ToString();
                        m_gin_s[i] = m_gin[i];
                    }
                }
            }
        }
    }
}
