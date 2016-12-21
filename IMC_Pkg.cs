using System;
using System.Runtime.InteropServices;


namespace imcpkg
{
    //FIFO编号
    public enum FIFO_SEL
    {
        SEL_IFIFO,
        SEL_QFIFO,
        SEL_PFIFO1,
        SEL_PFIFO2,
        SEL_CFIFO,
    }
    //WR_MUL_DES, *pWR_MUL_DES 定义
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WR_MUL_DES
    {
        // WORD->UInt16
        public Int16 addr;

        // WORD->UInt16
        public UInt16 axis;

        // WORD->UInt16
        public UInt16 len;

        // WORD[4]
        public UInt16 data_0;
        public Int16 data_1;
        public Int16 data_2;
        public Int16 data_3;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct EventInfo
    {
        //操作码，即枚举类型IMC_EVENT_CMD中的值
        public short EventCMD;

        //执行类型，即枚举类型IMC_EventType中的值
        public short EventType;

        //指向操作数1的参数地址
        public short Src1_loc;

        //操作数1所属的轴号
        public short Src1_axis;

        //指向操作数2的参数地址
        public short Src2_loc;

        //操作数2所属的轴号
        public short Src2_axis;

        //保留
        public int reserve1;

        //指向存储目标的参数地址
        public short dest_loc;

        //目标参数所属的轴号
        public short dest_axis;

        //保留
        public int reserve2;
    }


    //事件类型定义
    public enum IMC_EventType
    {
        IMC_Allways,		//“无条件执行”

        IMC_Edge_Zero,		//“边沿型条件执行”——变为0时
        IMC_Edge_NotZero,	//“边沿型条件执行”——变为非0时
        IMC_Edge_Great, 	//“边沿型条件执行”——变为大于时
        IMC_Edge_GreatEqu, 	//“边沿型条件执行”——变为大于等于时
        IMC_Edge_Little,	//“边沿型条件执行”——变为小于时
        IMC_Edge_Carry,		//“边沿型条件执行”——变为溢出时
        IMC_Edge_NotCarry, 	//“边沿型条件执行”——变为无溢出时

        IMC_IF_Zero,		//“电平型条件执行”——若为0
        IMC_IF_NotZero, 	//“电平型条件执行”——若为非0
        IMC_IF_Great,		//“电平型条件执行”——若大于
        IMC_IF_GreatEqu, 	//“电平型条件执行”——若大于等于
        IMC_IF_Little, 		//“电平型条件执行”——若小于
        IMC_IF_Carry,		//“电平型条件执行”——若溢出
        IMC_IF_NotCarry		//“电平型条件执行”——若无溢出
    }


    class IMC_Pkg
    {
        [DllImport(("IMC_Pkg.dll"), EntryPoint = "PKG_IMC_FindNetCard")]
        //static extern int DllRegisterServer();
        public static extern int
	        PKG_IMC_FindNetCard (byte[]  info,	    //返回找到的网卡名称
						        ref int num) ;		//返回找到的网卡数量
        //打开控制卡设备，与设备建立通信连接
        [DllImport("IMC_Pkg.dll")]
        public static extern IntPtr  
	        PKG_IMC_Open (int netcardsel,			//网卡索引，由搜索网卡函数返回的结果决定
						        int imcid) ;		//IMC控制卡的id，由控制卡上的拨码开关设置决定
        //打开控制卡设备，与设备建立通信连接
        [DllImport("IMC_Pkg.dll")]
        public static extern IntPtr  
	        PKG_IMC_OpenX (int netcardsel, 			//网卡索引，由搜索网卡函数返回的结果决定
						        int imcid, 			//IMC控制卡的id，由控制卡上的拨码开关设置决定
						        int timeout,		//通信超时时间，单位毫秒
						        int openMode) ;		//打开模式；1：混杂模式， 0：非混杂模式
        //使用密码打开控制卡设备，与设备建立通信连接
        [DllImport("IMC_Pkg.dll")]
        public static extern IntPtr  
	        PKG_IMC_OpenUsePassword (int netcardsel, 	//网卡索引，由搜索网卡函数返回的结果决定
						        int imcid,  			//IMC控制卡的id，由控制卡上的拨码开关设置决定
                                ref char password);		//密码字符串
        //关闭打开的设备。
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Close (IntPtr Handle);

        //配置函数
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_IMC_InitCfg(IntPtr handle);
        //清空控制卡中所有的FIFO
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_ClearIMC(IntPtr handle); 	    //设备句柄
        //清空指定轴的所有状态
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_ClearAxis(IntPtr handle, 	    //设备句柄，
					        int axis);				//轴号			
        //设置指定轴的有效电平的脉冲宽度
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetPulWidth(IntPtr handle, 	    //设备句柄，
					        UInt32 ns,		        //脉冲宽度，单位为纳秒
					        int axis);				//需要设置脉冲宽度的轴号
        //设置指定轴的脉冲和方向的有效电平
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetPulPolar(IntPtr handle, 	    //设备句柄，
					        int pul,				//脉冲信号的有效电平。非零：高电平有效； 零：低电平有效。
					        int dir, 				//方向信号的有效电平。非零：高电平有效； 零：低电平有效。
					        int axis);				//需要设置有效电平的轴号。
        //使能/禁止控制卡接收编码器反馈
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetEncpEna(IntPtr handle, 	    //设备句柄，
					        int ena, 				//使能标志。非零：使能； 零：不使能。
					        int axis);				//需要能/禁止控制卡接收编码器反馈的轴号。
        //设置控制卡接收编码器反馈的计数模式和计数方向
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetEncpMode(IntPtr handle,  	//设备句柄，
					        int mode, 				//编码器的计数模式。零：正交信号模式； 非零：脉冲+方向模式
					        int dir, 				//编码器的计数方向。
					        int axis);				//需要设置的轴号。
        //设置编码器反馈倍率
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetEncpRate(IntPtr handle,      //设备句柄，
					        double rate,			//倍率
					        int axis); 				//需要设置的轴号。
        //设置指定轴的速度和加速度限制
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetVelAccLim(IntPtr handle,     //设备句柄，
					        double vellim, 			//速度极限，单位为脉冲每毫秒。
					        double acclim, 			//加速度极限，单位为脉冲每平方毫秒。
					        int axis);				//需要设置速度和加速度极限的轴号
        //使能/禁止指定轴的驱动器
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetEna(IntPtr handle, 		    //设备句柄，
					        int ena, 				//使能标志。非零：使能； 零：不使能。
					        int axis);				//需要使能/禁止驱动器的轴号。
        //使能/禁止硬件限位输入端口和设置其有效极性。
        [DllImport("IMC_Pkg.dll")]
        public static extern int
            PKG_IMC_Setlimit(IntPtr handle, 	    //设备句柄，
                            int plimEna, 			//是否使能硬件正限位功能。非零：使能； 零：不使能。
                            int plimPolar, 			//正限位极性；非零：有效； 零：低电平有效。
                            int nlimEna, 			//是否使能硬件负限位功能。非零：使能； 零：不使能。
                            int nlimPolar, 			//负限位极性；非零：有效； 零：低电平有效。
					        int axis);				//轴号。
        //使能伺服报警输入和设置其有效极性
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetAlm (IntPtr handle, 		    //设备句柄，
					        int ena, 				//是否使能伺服报警输入功能。非零：使能； 零：不使能。
					        int polar, 				//极性；非零：高电平有效； 零：低电平有效。
					        int axis);				//轴号。
        //使能伺服到位输入和设置其有效极性
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetINP (IntPtr handle, 		    //设备句柄，
					        int ena, 				//是否使能伺服到位输入功能。非零：使能； 零：不使能。
					        int polar, 				//极性；非零：高电平有效； 零：低电平有效。
					        int axis);				//轴号。
        //设置急停输入端的有效极性
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetEmstopPolar(IntPtr handle,   //设备句柄，
					        int polar, 				//极性；非零：高电平有效； 零：低电平有效。
					        int axis);				//轴号。
        //设置通用输入端的有效极性
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetInPolar(IntPtr handle, 	    //设备句柄，
					        int polar, 				//极性；非零：高电平有效； 零：低电平有效。
                            int inPort);			//输入端口，范围1 - 32。
        //设置发生错误时，电机是否停止运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int
            PKG_IMC_SetStopfilt(IntPtr handle,      //设备句柄，
                            int stop, 				//出错时是否停止运行；非零：停止； 零：不停止。
					        int axis);				//轴号。
        //设置发生错误时，电机是否退出运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int
            PKG_IMC_SetExitfilt(IntPtr handle,      //设备句柄，
                            int exit, 				//出错时是否退出运行；非零：退出； 零：不退出。
					        int axis);				//轴号。
        //设置静态补偿的范围
        [DllImport("IMC_Pkg.dll")]
        public static extern int
            PKG_IMC_SetRecoupRange(IntPtr handle,   //设备句柄，
                            int range, 				//误差补偿值；取值范围0 - 32767。
					        int axis);				//轴号。

        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_GetConfig(IntPtr handle,  		//设备句柄，
                            ref UInt32 steptime, 	//脉冲宽度，单位为纳秒
                            ref int pulpolar, 		//脉冲的有效电平；零：低电平有效； 非零：高电平有效
                            ref int dirpolar, 		//方向的有效电平；零：低电平有效； 非零：高电平有效
                            ref int encpena, 		//是否使用编码器反馈；零：禁用； 非零：使用
                            ref int encpmode, 		//编码器计数模式
                            ref int encpdir, 		//编码器计数方向
                            ref double encpfactor, 	//编码器倍率
                            ref double vellim, 		//速度极限，单位为脉冲/毫秒
                            ref double acclim, 		//加速度极限，单位为脉冲/平方毫秒
                            ref int drvena, 		//是否使能驱动器；零：不使能； 非零：使能
                            ref int plimitena, 		//是否使用硬件正限位；零：禁用； 非零：使用
                            ref int plimitpolar, 	//硬件正限位有效电平；零：低电平有效； 非零：高电平有效
                            ref int nlimitena, 		//是否使用硬件负限位；零：禁用； 非零：使用
                            ref int nlimitpolar, 	//硬件负限位有效电平；零：低电平有效； 非零：高电平有效
                            ref int almena, 		//是否使用伺服报警；零：禁用； 非零：使用
                            ref int almpolar, 		//伺服报警有效电平；零：低电平有效； 非零：高电平有效
                            ref int INPena, 		//是否使用伺服到位；零：禁用； 非零：使用
                            ref int INPpolar, 		//伺服到位有效电平；零：低电平有效； 非零：高电平有效
					        int axis);				//需要获取信息的轴号
        //点到点运动函数
        //使轴从当前位置移动到指定的目标位置
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MoveAbs(IntPtr handle, 		    //设备句柄，
					        int pos, 				//目标位置，单位为脉冲；
					        double startvel, 		//起始速度，单位为脉冲每毫秒；
					        double tgvel, 			//目标速度，单位为脉冲每毫秒；
					        int wait, 				//是否等待运动完成后，函数才返回。非零：等待运动完成；零：不等待。
					        int axis); 				//指定轴号
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MoveAbs_P(IntPtr handle, 		    //设备句柄，
					        int pos, 				//目标位置，单位为脉冲；
					        double startvel, 		//起始速度，单位为脉冲每毫秒；
					        double tgvel, 			//目标速度，单位为脉冲每毫秒；
					        int wait, 				//是否等待运动完成后，函数才返回。非零：等待运动完成；零：不等待。
					        int axis); 				//指定轴号

        //使轴从当前位置移动到指定的距离
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MoveDist(IntPtr handle, 	    //设备句柄，
					        int dist, 				//移动距离，单位为脉冲；
					        double startvel, 		//起始速度，单位为脉冲每毫秒；
					        double tgvel, 			//目标速度，单位为脉冲每毫秒；
					        int wait, 				//是否等待运动完成后，函数才返回。非零：等待运动完成；零：不等待。
					        int axis); 				//指定轴号；
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MoveDist_P(IntPtr handle, 	    //设备句柄，
					        int dist, 				//移动距离，单位为脉冲；
					        double startvel, 		//起始速度，单位为脉冲每毫秒；
					        double tgvel, 			//目标速度，单位为脉冲每毫秒；
					        int wait, 				//是否等待运动完成后，函数才返回。非零：等待运动完成；零：不等待。
					        int axis); 				//指定轴号；
        //立即改变当前正在执行的点到点运动的运动速度
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2Pvel(IntPtr handle,		    //设备句柄，
					        double tgvel, 			//目标速度，单位为脉冲每毫秒；
					        int axis);				//轴号；
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2Pvel_P(IntPtr handle,		    //设备句柄，
					        double tgvel, 			//目标速度，单位为脉冲每毫秒；
					        int axis);				//轴号；
        //设置当前点到点运动的加速度和减速度
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetAccel(IntPtr handle, 	    //设备句柄，
					        double accel, 			//加速度，单位为脉冲每平方毫秒；
					        double decel, 			//减速度；
					        int axis);				//轴号；
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetAccel_P(IntPtr handle, 	    //设备句柄，
					        double accel, 			//加速度，单位为脉冲每平方毫秒；
					        double decel, 			//减速度；
					        int axis);				//轴号；
        //设置点到点运动模式
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2Pmode(IntPtr handle,  	    //设备句柄，
					        int mode, 				//运动模式；零：普通模式； 非零：跟踪模式
					        int axis);				//轴号。
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2Pmode_P(IntPtr handle,  	    //设备句柄，
					        int mode, 				//运动模式；零：普通模式； 非零：跟踪模式
					        int axis);				//轴号。
        //改变点到点运动的目标位置
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2PnewPos(IntPtr handle,  	    //设备句柄，
					        int tgpos, 			    //新的目标位置，单位为脉冲；
					        int axis);				//轴号。
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2PnewPos_P(IntPtr handle,  	    //设备句柄，
					        int tgpos, 			    //新的目标位置，单位为脉冲；
					        int axis);				//轴号。
        //减速停止点到点运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2Pstop(IntPtr handle, 		    //设备句柄，
					        int axis);				//轴号。
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_P2Pstop_P(IntPtr handle, 		    //设备句柄，
					        int axis);				//轴号。
        //使轴立即按指定的速度一直运动，直到速度被改变为止
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MoveVel(IntPtr handle,  	    //设备句柄，
					        double startvel, 		//起始速度，单位为脉冲每平方毫秒； 
					        double tgvel, 			//指定轴的运动速度，单位为脉冲每平方毫秒；
					        int axis);				//轴号。
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MoveVel_P(IntPtr handle,  	    //设备句柄，
					        double startvel, 		//起始速度，单位为脉冲每平方毫秒； 
					        double tgvel, 			//指定轴的运动速度，单位为脉冲每平方毫秒；
					        int axis);				//轴号。
        //插补函数
        //立即将参与插补运动的轴号映射到X、Y、Z、A、B、…、等对应的标识上
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_AxisMap(IntPtr handle, 		    //设备句柄，
					        int[] axis, 			//需要映射的轴号的数组
					        int num, 				//需要映射的轴的数量
					        int fifo);				//对哪个插补空间进行映射，可选SEL_PFIFO1和SEL_PFIFO2。
        //立即启用插补空间
        [DllImport("IMC_Pkg.dll")]
        public static extern int
	        PKG_IMC_PFIFOrun (IntPtr handle, 	    //设备句柄，
					        int fifo);				//启用哪个插补空间，可选SEL_PFIFO1或SEL_PFIFO2。
        //立即改变插补的加速度
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetPFIFOaccel (IntPtr handle,   //设备句柄，
					        double accel, 			//插补的加速度，单位为脉冲每平方毫秒； 
					        int fifo);				//设置哪个插补空间的插补的加速度，可选SEL_PFIFO1或SEL_PFIFO2。
        //立即改变插补的加速度
        [DllImport("IMC_Pkg.dll")]
        public static extern int
            PKG_IMC_SetPFIFOvelMode(IntPtr handle,   //设备句柄，
                            int mode, 			    //速度规划模式 
                            int fifo);				//设置哪个插补空间的速度规划模式，可选SEL_PFIFO1或SEL_PFIFO2。
        //单段直线插补（绝对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Line_Pos(IntPtr handle, 	    //设备句柄，
					        int[] pos, 			    //参与插补运动的轴的位置，单位为脉冲；
					        int axisNum, 			//参与插补运动的轴数；
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的末端速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //单段直线插补（相对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Line_Dist(IntPtr handle, 	    //设备句柄，
					        int[] dist, 			//参与插补运动的轴的移动距离，单位为脉冲；
					        int axisNum, 			//参与插补运动的轴数；
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel,			//插补运动的末端速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //多段连续直线插补（绝对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MulLine_Pos (IntPtr handle,     //设备句柄，
					        int[] pos, 			//多段参与插补运动的轴的位置，单位为脉冲；
					        int axisNum, 			//参与插补运动的轴数；
					        int segNum, 			//插补运动的段数；
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的最后一段的结束速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //多段连续直线插补（相对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_MulLine_Dist (IntPtr handle,    //设备句柄，
					        int[] dist, 			//多段参与插补运动的轴的移动距离，单位为脉冲； 
					        int axisNum, 			//参与插补运动的轴数；
					        int segNum, 			//插补运动的段数；
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的最后一段的结束速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //圆弧插补（绝对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Arc_Pos(IntPtr handle, 		    //设备句柄，
					        int endx, 				//参与圆弧插补的X轴的终点的位置，单位为脉冲；
					        int endy, 				//参与圆弧插补的Y轴的终点的位置，单位为脉冲；
					        int cx, 				//参与圆弧插补的X轴的圆心，单位为脉冲；
					        int cy, 				//参与圆弧插补的Y轴的圆心，单位为脉冲； 
					        int dir, 				//圆弧运动的方向
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的结束速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo) ;				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //圆弧插补（相对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Arc_Dist(IntPtr handle, 	    //设备句柄，
					        int dist_x, 			//参与圆弧插补的X轴的终点相对于起点的距离，单位为脉冲；
					        int dist_y, 			//参与圆弧插补的Y轴的终点相对于起点的距离，单位为脉冲；
					        int dist_cx, 			//参与圆弧插补的X轴的圆心相对于起点的距离，单位为脉冲；
					        int dist_cy, 			//参与圆弧插补的Y轴的圆心相对于起点的距离，单位为脉冲；
					        int dir, 				//圆弧运动的方向
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的结束速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //圆弧直线插补（绝对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_ArcLine_Pos(IntPtr handle,  	//设备句柄，
					        int endx, 				//参与圆弧插补的X轴的终点的位置，单位为脉冲；
					        int endy, 				//参与圆弧插补的Y轴的终点的位置，单位为脉冲；
					        int cx, 				//参与圆弧插补的X轴的圆心，单位为脉冲；
					        int cy, 				//参与圆弧插补的Y轴的圆心，单位为脉冲；
					        int dir, 				//圆弧运动的方向； 
					        int[] pos, 			    //跟随圆弧插补运动的轴的位置，单位为脉冲； 
					        int axisNum, 			//跟随圆弧插补运动的轴数；
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的结束速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //圆弧直线插补（相对运动）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_ArcLine_Dist(IntPtr handle,     //设备句柄，
					        int dist_x, 			//参与圆弧插补的X轴的终点相对于起点的距离，单位为脉冲；
					        int dist_y, 			//参与圆弧插补的Y轴的终点相对于起点的距离，单位为脉冲；
					        int dist_cx, 			//参与圆弧插补的X轴的圆心相对于起点的距离，单位为脉冲；
					        int dist_cy, 			//参与圆弧插补的Y轴的圆心相对于起点的距离，单位为脉冲；
					        int dir, 				//圆弧运动的方向； 
					        int[] dist, 			//跟随圆弧插补运动的轴的移动距离，单位为脉冲；
					        int axisNum, 			//跟随圆弧插补运动的轴数；
					        double tgvel, 			//插补运动的速度，单位为脉冲每平方毫秒；
					        double endvel, 			//插补运动的结束速度，单位为脉冲每平方毫秒；
					        int wait, 				//是否等待插补运动完成，函数才返回。非零：等待运动完成；零：不等待。
					        int fifo);				//指定将此运动指令发送到哪个FIFO中执行，可选SEL_PFIFO1或SEL_PFIFO2。
        //立即改变插补的进给倍率。当进给倍率设为0时，可实现插补的暂停，再次设为非零则解除暂停
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetFeedrate (IntPtr handle,     //设备句柄，
					        double rate, 			//进给倍率； 
					        int fifo);				//设置哪个插补空间的插补的进给倍率，可选SEL_PFIFO1或SEL_PFIFO2。
        //立即停止插补运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_PFIFOstop (IntPtr handle, 	    //设备句柄，
					        int fifo);				//停止哪个插补空间的插补，可选SEL_PFIFO1或SEL_PFIFO2。
		//判断插补运动是否停止
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
			PKG_IMC_isPstop (IntPtr handle, 	    //设备句柄，
							int fifo);             //哪个插补空间的插补停止，可选SEL_PFIFO1或SEL_PFIFO2。
        //立即清空发到插补空间中未被执行的所有指令
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_PFIFOclear (IntPtr handle, 	    //设备句柄，
					        int fifo);				//清空哪个插补空间的指令，可选SEL_PFIFO1或SEL_PFIFO2。

        //齿轮函数
        //设置指定轴跟随电子手轮运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int  
	        PKG_IMC_HandWheel(IntPtr Handle, 	    //设备句柄，
					        double rate, 			//电子手轮倍率；
					        int axis);				//跟随手轮运动的轴号；
        //设置从动轴跟随主动轴运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int  
	        PKG_IMC_Gear1 (IntPtr Handle, 		    //设备句柄，
					        double rate, 			//齿轮倍率；
					        int master, 			//主动轴号
					        int axis);				//从动轴的轴号。
        //设置从动轴跟随主动轴运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int  
	        PKG_IMC_Gear2 (IntPtr Handle, 	        //设备句柄，
					        double rate, 			//齿轮倍率；
					        int master, 			//主动轴号
					        int axis);				//从动轴的轴号。
        //立即脱离电子手轮或齿轮的啮合
        [DllImport("IMC_Pkg.dll")]
        public static extern int  
	        PKG_IMC_ExitGear (IntPtr Handle, 	    //设备句柄，
					        int axis);				//从动轴的轴号。


        //IO设置函数
        //对输出端口进行控制
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetOut(IntPtr handle, 		    //设备句柄，
					        int outPort, 			//输出端口；范围是1 – 48；
					        int data, 				//控制输出端口的状态； 零：断开输出端口； 非零：连通输出端口。
					        int fifo);				//指定将此指令发送到哪个FIFO中执行。

         //搜零函数
        //设置当前搜零过程中使用的高速度和低速度
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetHomeVel(IntPtr handle, 	    //设备句柄，
					        double hight, 			//搜零过程中使用的高速度，单位为脉冲每毫秒；
					        double low, 			//搜零过程中使用的低速度，单位为脉冲每毫秒；
					        int axis);				//轴号；
		//设置编码器索引信号的极性
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
			PKG_IMC_SetHomeIndexPolar(IntPtr handle, //设备句柄，
							int polar,				//索引信号的极性， 非零：上升沿有效， 0：下降沿有效
							int axis);				//轴号；
        //使用零点开关搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeSwitch1 (IntPtr Handle,     //设备句柄，
					        int dir, 				//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int riseEdge, 			//指定检测原点开关的边沿；零：下降沿； 非零：上升沿
					        int pos, 				//设置零点时刻零点开关的位置值；
					        int stpos, 			    //搜零结束时，电机停止的位置；
					        double movVel,			//设置零点后移动到停止位置时的速度
					        int wait,				//是否等待搜零结束
					        int axis);				//轴号。
        //使用零点开关搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeSwitch2 (IntPtr Handle,     //设备句柄，
					        int dir, 				//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int riseEdge, 			//指定检测原点开关的边沿；零：下降沿； 非零：上升沿
					        int pos, 				//设置零点时刻零点开关的位置值；
					        int stpos, 			    //搜零结束时，电机停止的位置；
					        double movVel,			//设置零点后移动到停止位置时的速度
					        int wait,				//是否等待搜零结束
					        int axis) ;				//轴号。
        //使用零点开关搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeSwitch3 (IntPtr Handle,     //设备句柄，
					        int dir, 				//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int riseEdge, 			//指定检测原点开关的边沿；零：下降沿； 非零：上升沿
					        int pos, 				//设置零点时刻零点开关的位置值；
					        int stpos, 			    //搜零结束时，电机停止的位置；
					        double movVel,			//设置零点后移动到停止位置时的速度
					        int wait,				//是否等待搜零结束
					        int axis);				//轴号。
        //使用零点开关和索引信号搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeSwitchIndex1 (IntPtr Handle,    //设备句柄，
					        int dir, 					//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int riseEdge, 				//指定检测原点开关的边沿；零：下降沿； 非零：上升沿
					        int pos, 					//设置零点时刻零点开关的位置值；
					        int stpos, 			    	//搜零结束时，电机停止的位置；
					        double movVel,				//设置零点后移动到停止位置时的速度
					        int wait,					//是否等待搜零结束
					        int axis);					//轴号。
        //使用零点开关和索引信号搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeSwitchIndex2 (IntPtr Handle, //设备句柄，
					        int dir, 				//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int riseEdge, 			//指定检测原点开关的边沿；零：下降沿； 非零：上升沿
					        int pos, 				//设置零点时刻零点开关的位置值；
					        int stpos, 			    //搜零结束时，电机停止的位置；
					        double movVel,			//设置零点后移动到停止位置时的速度
					        int wait,				//是否等待搜零结束
					        int axis) ;				//轴号。
        //使用零点开关和索引信号搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeSwitchIndex3 (IntPtr Handle, 	//设备句柄，
					        int dir, 				//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int riseEdge, 			//指定检测原点开关的边沿；零：下降沿； 非零：上升沿
					        int pos, 				//设置零点时刻零点开关的位置值；
					        int stpos, 			    //搜零结束时，电机停止的位置；
					        double movVel,			//设置零点后移动到停止位置时的速度
					        int wait,				//是否等待搜零结束
					        int axis);				//轴号。
        //使用索引信号搜索零点
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeIndex (IntPtr Handle, 	    //设备句柄，
					        int dir, 				//搜零方向。零：正方向搜零；非零：负方向搜零；
					        int pos, 				//设置零点时刻零点开关的位置值；
					        int stpos, 			    //搜零结束时，电机停止的位置；
					        double movVel,			//设置零点后移动到停止位置时的速度
					        int wait,				//是否等待搜零结束
					        int axis);				//轴号。
        //把该轴的当前位置设定为指定值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetPos (IntPtr Handle, 		    //设备句柄，
					        int pos, 				//设置的指定值，单位为脉冲；
					        int axis);				//轴号。
        //立即停止搜零运动
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_HomeStop (IntPtr Handle, 	    //设备句柄，
					        int axis);				//需要停止搜零运动的轴号。

        //获取状态函数
        //获取轴数
        [DllImport("IMC_Pkg.dll")]
         public static extern int 
         PKG_IMC_GetNaxis(IntPtr Handle); 		    //设备句柄，

         //获得所有轴的机械位置。
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetEncp(IntPtr Handle, 	    //设备句柄，
	 				        int[] pos, 			    //用于保存机械位置的数组；单位为脉冲；
					        int axisnum);			//控制卡的轴数。
         //获得所有轴的指令位置
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetCurpos(IntPtr Handle,   	//设备句柄，
	 				        int[] pos, 			    //用于保存指令位置的数组；单位为脉冲；
					        int axisnum);			//控制卡的轴数。
         //获得所有轴的错误状态
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetErrorReg(IntPtr Handle,     //设备句柄，
	 				        UInt16[] err, 	        //用于保存所有轴的错误状态；有错误则相应的位会置1。
					        int axisnum);			//控制卡的轴数。
         //获得所有轴的运动状态
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetMoving(IntPtr Handle, 	    //设备句柄，
					        UInt16[] moving,        //用于保存所有轴的运动状态，零：已停止运动； 非零：正在运动中
					        int axisnum);			//控制卡的轴数。
         //获得所有轴的功能输入端口状态
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetAin(IntPtr Handle,  	    //设备句柄，
					        UInt16[,] ain,	        //用于保存所有轴功能输入端的状态
					        int axisnum);			//控制卡的轴数。
         //获得所有通用输入端口的状态
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetGin(IntPtr Handle, 	        //设备句柄，
					        UInt16[] gin);          //用于保存所有通用输入端的状态。
         //获得所有输出端口的状态
         [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_GetGout(IntPtr Handle,     	//设备句柄，
					        UInt16[] gout);         //用于保存所有输出端的状态

        [DllImport("IMC_Pkg.dll")]
         public static extern int 
	         PKG_IMC_ClearError(IntPtr Handle,      //设备句柄，
					        int axis);				//需要清除错误的轴号
        //其他功能函数
         //所有轴立即急停或解除急停状态
         [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Emstop(IntPtr Handle, 		    //设备句柄，
					        int isStop);			//急停还是解除急停；非零：急停； 零：解除急停。
        //对所有轴立即暂停或解除暂停状态
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_Pause(IntPtr Handle, 		    //设备句柄，
					        int pause);				//暂停还是解除暂停状态；非零：暂停； 零：解除暂停。
        //设置每个轴的平滑度
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetSmooth(IntPtr handle, 	    //设备句柄，
						        short smooth, 		//平滑度，值越大则越平滑，但运动轨迹的误差就越大；
						        int aixs); 			//轴号；	

        //退出等待运动完成
        [DllImport("IMC_Pkg.dll")]
        public static extern void  
	        PKG_IMC_ExitWait();
        //当函数返回错误是，使用此函数获得错误提示
        [DllImport("IMC_Pkg.dll")]
        public static extern   string
	        PKG_IMC_GetFunErrStr();
        //获得错误寄存器是字符串说明
        [DllImport("IMC_Pkg.dll")]
        public static extern  string
	        PKG_IMC_GetRegErrorStr(UInt16 err);

        //ADD事件
        //将某个寄存器的值与另一个寄存器的值进行相加，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_ADD32(ref EventInfo info,	//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis,		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis, 		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定数值进行相加，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_ADD32i(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            int data, 							//32位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与另一个寄存器的值进行相加，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_ADD48(ref EventInfo info,	//事件结构体指针，事件指令将填充事件到此指针指向的地址中 
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定数值进行相加，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_ADD48i(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            Int64 data, 						//64位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号

        //CMP事件
        //将某个寄存器的值与另一个寄存器的值进行相减，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_CMP32(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定数值进行相减，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_CMP32i(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            int data, 							//32位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与另一个寄存器的值进行相减，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_CMP48(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定数值进行相减，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_CMP48i(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            Int64 data, 						//64位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号

        //SCA事件
        //将某个寄存器的值乘以另一个寄存器指定的倍率，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SCA32(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值乘以指定的倍率，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SCA32i(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            int data, 							//32位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值乘以另一个寄存器指定的倍率，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SCA48(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值乘以乘以指定的倍率，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SCA48i(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            Int64 data, 						//64位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //MUL事件
        //
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_MUL32L(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_MUL32iL(ref EventInfo info,//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            int data, 							//32位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_MUL32A(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_MUL32iA(ref EventInfo info,//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            int data, 							//32位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号

        //COP事件
        //将某个16位寄存器的值赋值给目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_COP16(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个32位寄存器的值赋值给目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_COP32(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个48位寄存器的值赋值给目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_COP48(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号

        //SET事件
        //将指定的数值赋值给16位目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SET16(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short data, 						//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将指定的数值赋值给32位目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SET32(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        int data, 							//32位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将指定的数值赋值给48位目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_SET48(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        Int64 data, 						//64位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号


        //OR事件
        //将某个寄存器的值与另一个寄存器的值进行或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_OR16(ref EventInfo info,	//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与另一个寄存器的值进行或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_OR16B(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定的数值进行或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_OR16i(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
					        short data, 						//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定的数值进行或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_OR16iB(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
					        short data, 						//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号

        //AND事件
        //将某个寄存器的值与另一个寄存器的值进行与运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_AND16(ref EventInfo info,  //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与另一个寄存器的值进行与运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_AND16B(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定的数值进行与运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_AND16i(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
					        short data, 						//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定的数值进行与运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_AND16iB(ref EventInfo info, //事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
					        short data, 						//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号


        //XOR事件
        //将某个寄存器的值与另一个寄存器的值进行异或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_XOR16(ref EventInfo info,	//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与另一个寄存器的值进行异或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_XOR16B(ref EventInfo info,	//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
                            short Src2, short Src2_axis,  		//寄存器2的地址和其对应的轴号
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定的数值进行异或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_XOR16i(ref EventInfo info,	//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType, 					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
					        short data, 						//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号
        //将某个寄存器的值与指定的数值进行异或运算，将结果保存到目标寄存器
        [DllImport("IMC_Pkg.dll")]
        public static extern int PKG_Fill_XOR16iB(ref EventInfo info,//事件结构体指针，事件指令将填充事件到此指针指向的地址中
					        short  EventType,					//事件类型，由枚举IMC_EventType中的成员赋值
					        short Src1, short Src1_axis, 		//寄存器1的地址和其对应的轴号
					        short data,							//16位整数
                            short dest, short dest_axis);		//目标寄存器的地址和其对应的轴号


        //底层函数封装
        //设置多个寄存器为指定值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetMulParam(IntPtr Handle,	    //设备句柄，
						        WR_MUL_DES[] pdes,	//WR_MUL_DES结构体数组；
						        int ArrNum,			//pdes数组中有效数据的个数；
						        int fifo);			//指定将此指令发送到哪个FIFO中
        //设置16位寄存器为指定值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetParam16(IntPtr Handle, 	    //设备句柄，
						        short paramloc,		//寄存器地址；
						        short data,			//16位整型数据
						        int axis,			//轴号；
						        int fifo);			//指定将此指令发送到哪个FIFO中
        //设置32位寄存器为指定值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetParam32(IntPtr Handle, 	    //设备句柄，
						        short paramloc, 	//寄存器地址；
						        int data, 			//32位整型数据
						        int axis,			//轴号；
						        int fifo);			//指定将此指令发送到哪个FIFO中
        //设置48位寄存器为指定值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetParam48(IntPtr Handle, 	    //设备句柄，
						        short paramloc, 	//寄存器地址；
						        Int64 data, 		//64位整型数据
						        int axis,			//轴号；
						        int fifo);			//指定将此指令发送到哪个FIFO中
        //将寄存器某个位的值设为指定值（1或者0）
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_SetParamBit(IntPtr Handle, 	    //设备句柄，
						        short paramloc,    	//寄存器地址；
						        short bit,			//寄存器的某个位，范围 0 – 15；
						        short val, 			//指定的值, 1或者0；
						        int axis,			//轴号；
						        int fifo);			//指定将此指令发送到哪个FIFO中
        //将寄存器指定的位的值由1变为0或者由0变为1
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_TurnParamBit(IntPtr Handle,     //设备句柄，
					        short paramloc, 		//寄存器地址；
					        short bit, 				//寄存器的某个位，范围 0 – 15；
					        int axis,				//轴号；
					        int fifo);				//指定将此指令发送到哪个FIFO中
        //置位或清零寄存器的某些位
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_ORXORParam(IntPtr Handle, 	    //设备句柄，
					        short paramloc, 		//寄存器地址；
					        short ORdata, 			//与寄存器进行相或的值；
					        short XORdata, 			//与寄存器进行相异或的值；
					        int axis,				//轴号；
					        int fifo);				//指定将此指令发送到哪个FIFO中
        //阻塞FIFO执行后续指令，直到寄存器的某个位变为指定值或超时为止
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_WaitParamBit(IntPtr Handle,	    //设备句柄，
					        short paramloc, 		//寄存器地址；
					        short bit, 				//寄存器的某个位，范围 0 – 15；
					        short val, 				//指定值，1或0；
					        int timeout, 			//超时时间，单位为毫秒；
					        int axis, 				//轴号；
					        int fifo);				//指定将此指令发送到哪个FIFO中
        //阻塞FIFO执行后续指令，直到超过设定的时间为止
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_WaitTime(IntPtr Handle,		    //设备句柄，
					        int time, 				//等待时间，单位为毫秒
					        int fifo);				//指定将此指令发送到哪个FIFO中
        //阻塞FIFO执行后续指令，直到寄存器的值变为指定值或超时为止
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_WaitParam(IntPtr Handle, 	    //设备句柄，
					        short paramloc, 		//寄存器地址；
					        short data, 			//指定的值；
					        int timeout, 			//超时时间，单位为毫秒；
					        int axis, 				//轴号；
					        int fifo);				//指定将此指令发送到哪个FIFO中
        //阻塞FIFO执行后续指令，直到寄存器的值与mask进行相与后的值与data值相等或超时为止
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_WaitParamMask(IntPtr Handle,    //设备句柄，
					        short paramloc, 		//寄存器地址；
					        short mask, 			//与寄存器进行相与的值
					        short data, 			//用于比较的值；
					        int timeout, 			//超时时间，单位为毫秒；
					        int axis, 				//轴号；
					        int fifo);				//指定将此指令发送到哪个FIFO中
        //读取多个寄存器的值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_GetMulParam(IntPtr Handle, 	    //设备句柄，
					        WR_MUL_DES[] pdes, 		//WR_MUL_DES结构体数组；
					        int ArrNum);			//WR_MUL_DES结构体数组的有效成员个数。
        //读取16位寄存器的值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_GetParam16(IntPtr Handle, 		//设备句柄，
					        short paramloc, 		//寄存器地址；
                            ref short data, 		//16位整型变量的地址，用于保存16位寄存器的值；
					        int axis);				//轴号；
        //读取32位寄存器的值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_GetParam32(IntPtr Handle, 		//设备句柄，
					        short paramloc, 		//寄存器地址；
                            ref int data, 			//32位整型变量的地址，用于保存32位寄存器的值；
					        int axis);				//轴号；
        //读取48位寄存器的值
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_GetParam48(IntPtr Handle, 		//设备句柄，
					        short paramloc, 		//寄存器地址；
					        ref Int64 data, 		//64位整型变量的地址，用于保存48位寄存器的值；
					        int axis);				//轴号；

        //将设置的事件安装到控制卡中
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_InstallEvent(IntPtr Handle,     //设备句柄，
					        EventInfo[] pEvent, 	//事件结构体数组，由事件填充函数填充；
					        UInt16 EventNum	        //事件指令的数量；
					        );
        //停止安装的事件运行
        [DllImport("IMC_Pkg.dll")]
        public static extern int 
	        PKG_IMC_StopEvent(IntPtr Handle); //设备句柄，
    }
}
