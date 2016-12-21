using System;


namespace imcpkg
{

//***********************************************************************************************************/
//       此头文件为iMC参数地址的宏定义                                                                      */
//       格式：                                                                                             */
//       //描述                                                                                             */
//       public const Int16   参数名Loc =      地址     // 轴或全局参数  数据格式                           */
//                                                                                                          */
//       其中“参数名”是指在iMC的参数名，其后加Loc表示该参数的地址                                         */
//                                                                                                          */
//       字母A表示轴参数，字母G表示全局参数                                                                 */
//                                                                                                          */
//       iMC中共有以下几种数据格式的类型：                                                                  */
//       S32：32位带符号位的整数，即小数部分的位数为0，参数值的范围为：[-2147483648,2147483647]。           */
//       U32：32位无符号位的整数，即小数部分的位数为0，参数值的范围为：[0, 4294967295]。                    */
//       S16：16位带符号位的整数，即小数部分的位数为0，参数值的范围为：[-32768,32767]。                     */
//       U16：16位无符号位的整数，即小数部分的位数为0，参数值的范围为：[0, 65535]。                         */
//       F16：16位标志参数，仅取两个值：0或FFFFh。                                                          */
//       R16：16位寄存器，各位域具有具体的意义，部分位域需设置。                                            */
//       S16.32：48位带符号位，高16位为整数部分，低32位为小数部分，参数值的范围：[-32768.0,32767.999999999767] */
//       U16.32：48位无符号位，高16位为整数部分，低32位为小数部分，参数值的范围：[0.0,65535.999999999767]   */
//       S16.16：32位带符号位，高16位为整数部分，低16位为小数部分，参数值的范围：[-32768.0,32767.999984741211]  */
//       U16.16：32位无符号位，高16位为整数部分，低32位为小数部分，参数值的范围：[0.0,65535.999984741211]。 */
//       S0.16：16位有符号位，16位为小数部分。                                                              */
//       因此，iMC中的参数值所表示的实际值为：                                                              */
//       实际值=参数值/2^n                                                                                  */
//       其中n为小数部分的位数。                                                                            */
//       例如，对于一个S32格式的数，“00018000h”（h表示十六进制表示）表示的十进制的值为98304/2^0 = 98304； */
//       对于一个S16.16格式的数，“00018000h”表示的十进制的值为98304/2^16 = 1.5。                          */
//***********************************************************************************************************/

    class ParamDef
    {
        //********************************************************************************************************/

        #region 主坐标系（MCS）点到点参数

        //当前规划速度（主坐标系）
        public const Int16 mcsvelLoc = 4;                                      //A              S16.16

        //连续速度运动的目标速度（主坐标系）
        public const Int16 mcstgvelLoc = 6;                                    //A              S16.16

        //主坐标系是否处于速度斜升过程中（加速或减速）。FFFFh：处于速度斜升过程，0：等于目标速度运行。
        //另：电子齿轮运动时，可以判断从动轴是否已达到传动速度，即是否达到主动轴速度乘以传动比率的值。0：达到，FFFFh：未达到
        public const Int16 mcsslopeLoc = 8;                                    //A              F16

        //主坐标系的指令位置
        public const Int16 mcsposLoc = 10;                                     //A              S32

        //绝对目标位置（主坐标系）
        public const Int16 mcstgposLoc = 12;                                   //A              S32

        //目标移动距离（主坐标系点到点运动）
        public const Int16 mcsdistLoc = 14;                                    //A              S32

        //点到点运动的最大速度（主坐标系）
        public const Int16 mcsmaxvelLoc = 16;                                  //A              S16.16

        //加速度（主坐标系）
        public const Int16 mcsaccelLoc = 18;                                   //A              S16.16

        //减速度（主坐标系）
        public const Int16 mcsdecelLoc = 20;                                   //A              S16.16

        //点到点运动的模式，0：普通模式，FFFFh：跟踪模式（主坐标系）
        public const Int16 mcstrackLoc = 22;                                   //A              F16

        //写入FFFFh启动点到点运动，写入零，停止当前的点到点运动（主坐标系）
        public const Int16 mcsgoLoc = 23;                                      //A              F16

        //标志主坐标系是否规划运动中，0：规划运动已停止，FFFFh：规划运动中
        public const Int16 mcsmovingLoc = 25;                                  //A              F16

        #endregion

        //********************************************************************************************************/

        #region 辅坐标系（PCS）点到点参数

        //当前规划速度（辅坐标系）
        public const Int16 pcsvelLoc = 27;                                     //A              S16.16

        //连续速度运动的目标速度（辅坐标系）。
        public const Int16 pcstgvelLoc = 29;                                   //A              S16.16

        //标志辅坐标系是否处于速度斜升过程中（加速或减速）。FFFFh：处于速度斜升过程，0：等于目标速度运行
        public const Int16 pcsslopeLoc = 31;                                   //A              F16

        //指令位置（辅坐标系）
        public const Int16 pcsposLoc = 33;                                     //A              S32

        //绝对目标位置（辅坐标系）
        public const Int16 pcstgposLoc = 35;                                   //A              S32

        //目标移动距离（辅坐标系点到点运动）
        public const Int16 pcsdistLoc = 37;                                    //A              S32

        //点到点运动的最大速度（辅坐标系）
        public const Int16 pcsmaxvelLoc = 39;                                  //A              S16.16

        //加速度（辅坐标系）。
        public const Int16 pcsaccelLoc = 41;                                   //A              S16.16

        //减速度（辅坐标系）。
        public const Int16 pcsdecelLoc = 43;                                   //A              S16.16

        //点到点运动的模式，0：普通模式，1：跟踪模式（辅坐标系）。
        public const Int16 pcstrackLoc = 45;                                   //A              F16

        //写入FFFFh启动点到点运动，写入零，停止当前的点到点运动（辅坐标系）。
        public const Int16 pcsgoLoc = 46;                                      //A              F16

        //标志辅坐标系是否规划运动中，0：规划运动已停止，FFFFh：规划运动中。
        public const Int16 pcsmovingLoc = 48;                                  //A              F16


        //当前指令位置，是主、辅坐标系目标位置叠加并经过运动平滑后总的指令位置。
        public const Int16 curposLoc = 225;                                    //A              S32

        //当前速度，是主、辅坐标系的速度叠加并经过运动平滑后总的速度。
        public const Int16 curvelLoc = 227;                                    //A              S16.16

        //0：辅坐标系移动的距离累加到主坐标系的目标位置mcstgpos，
        //非零：辅坐标系移动的距离不累加到主坐标系的目标位置mcstgpos
        public const Int16 shiftmasterLoc = 174;                               //A              F16

        #endregion

        //********************************************************************************************************/
        #region 主编码器

        //主编码器的倍率因子，应为正值。
        public const Int16 encpfactorLoc = 73;                                 //A              U16.16

        //主编码器速度，单位：计数值每控制周期。
        public const Int16 encpvelLoc = 75;                                    //A              S16.16

        //主编码器计数值，即当前实际位置。
        public const Int16 encpLoc = 78;                                       //A              S32

        //主编码器控制和状态寄存器
        public const Int16 encpctrLoc = 539;                                   //A              R16

        #endregion

        //********************************************************************************************************/
        #region 辅编码器

        //辅编码器的倍率因子，应为正值。
        public const Int16 encsfactorLoc = 81;                                 //A              U16.16

        //辅编码器速度，单位：计数值每控制周期。
        public const Int16 encsvelLoc = 83;                                    //A              S16.16

        //辅编码器计数值寄存器。
        public const Int16 encsLoc = 86;                                       //A              S32

        //辅助编码器的控制寄存器
        public const Int16 encsctrLoc = 531;                                   //A              R16

        //写该寄存器清零辅编码器计数寄存器
        public const Int16 clrencsLoc = 532;                                   //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region 其它轴参数

        //运行该轴。只有运行该轴才能进行运动规划。若该轴因错误退出运行，run清零，该轴处于停止运行状态。
        public const Int16 runLoc = 128;                                       //A              F16

        //状态寄存器
        public const Int16 statusLoc = 129;                                    //A              R16

        //错误寄存器
        public const Int16 errorLoc = 130;                                     //A              R16

        //位置误差(poserr)的限制值，超出该值，错误寄存器中相应的位域置位。注意：必须为正值。
        public const Int16 poserrlimLoc = 131;                                 //A              U16

        //运动平滑因子。
        public const Int16 smoothLoc = 132;                                    //A              U16

        //跟随误差窗口大小，单位：位移单位，参数值须大于零。
        public const Int16 trackwinLoc = 133;                                  //A              U16

        //电机静止窗口大小，单位：位移单位，参数值须大于零。
        public const Int16 settlewinLoc = 134;                                 //A              U16

        //设置电机自运动结束到进入静止状态的延时。若该轴已停止规划运动和平滑处理(moving=0)，
        //       在静止窗口内的状态(outsettle=0)保持settlen设定的控制周期个数后，清零motion参数，标志电机已静止。
        public const Int16 settlenLoc = 135;                                   //A              U16

        //指定发生何种错误时停止规划运动。stopfilt中置位的位域将致使：若错误寄存器error中对应的位域置位则停止运动。
        public const Int16 stopfiltLoc = 136;                                  //A              R16


        public const Int16 stopmodeLoc = 137;                                  //A              R16

        //指定发生何种错误时停止该轴运行。exitfilt中置位的位域将致使：若错误寄存器error中对应的位域置位则停止运行(清零run)。
        public const Int16 exitfiltLoc = 138;                                  //A              R16

        //正向软极限位置，默认为正方向最大值。
        public const Int16 psoftlimLoc = 139;                                  //A              S32

        //负向软极限位置，默认为负方向最大值。
        public const Int16 nsoftlimLoc = 141;                                  //A              S32

        //位置断点。若curpos<breakp，则状态寄存器status的BREAKP位域清零；若curpos≥breakp，则status的BREAKP位域置1。
        public const Int16 breakpLoc = 143;                                    //A              S32

        //原点的偏移位置值，设置机械原点时，该值被拷贝到指令位置寄存器curpos和编码器寄存器encp中。
        public const Int16 homeposLoc = 145;                                   //A              S32

        //搜寻原点时，低速段的速度预设值。
        public const Int16 lowvelLoc = 147;                                    //A              S16.16

        //搜寻原点时，高速段的速度预设值。
        public const Int16 highvelLoc = 149;                                   //A              S16.16

        //设置搜寻原点过程的运动序列和操作方式。
        public const Int16 homeseqLoc = 151;                                   //A              R16

        //写入FFFFh，开始执行自动搜寻和设置机械原点的操作，执行的动作过程由homeseq参数定义，清零则退出。
        public const Int16 gohomeLoc = 152;                                    //A              F16

        //主机设定原点位置指令，主机向该参数写入FFFFh，iMC将当前位置设为原点。
        public const Int16 sethomeLoc = 153;                                   //A              F16

        //非零表示已搜寻到原点
        public const Int16 homedLoc = 154;                                     //A              F16


        //写入非零值对该轴实施清零操作。清零该轴的编码器计数、指令位置、目标位置等各种位置参数，
        //       以及各种运动状态标志：mcspos、mcstgpos、curpos、encp、pcspos、pcstgpos、status、
        //       error、emstop、hpause、events、encs、ticks、aiolat。
        public const Int16 clearLoc = 157;                                     //A              F16

        //设置该轴的速度极限值。无论何种运动模式，只要实际速度超出此极限值，将置位错误寄存器error中的位VELLIM域，
        //       此错误不可屏蔽，因此只要发生此错误，立刻退出该轴运行。注意：必须为正值。
        public const Int16 vellimLoc = 158;                                    //A              S16.16


        //设置该轴的加速度极限值。无论何种运动模式，只要实际加速度超出此极限值，将置位错误寄存器error中的ACCLIM位域，
        //       此错误不可屏蔽，因此只要发生此错误，立刻退出该轴运行。注意：accellim必须为正值。
        public const Int16 accellimLoc = 160;                                  //A              S16.16


        //误差补偿速度。
        public const Int16 fixvelLoc = 162;                                    //A              S0.16

        //若设置搜寻到原点后移动到指定位置选项，该参数设置指定位置。
        public const Int16 homestposLoc = 163;                                 //A              S32

        
        #endregion

        //********************************************************************************************************/
        
        #region 电子齿轮参数

        //电子齿轮运动模式中主动轴的轴号。
        public const Int16 masterLoc = 169;                                    //A              U16

        //指针，指向从动轴所跟随的主动轴的参数。
        public const Int16 gearsrcLoc = 170;                                   //A              U16

        //写入FFFFh开始接合，清零则脱离啮合。
        public const Int16 engearLoc = 171;                                    //A              F16

        //脱离啮合后从动轴的运动模式。0：脱离啮合后从动轴以传动速度作为目标速度进入连续速度运动模式，
        //“传动速度”等于脱离啮合之时刻主动轴的速度乘以传动比率。FFFFh：脱离啮合后从动轴以由gearoutvel参数所设置的速度作为
        //目标速度进入连续速度运动模式。若需脱离啮合后停止，应设置gearoutvel参数值为0。
        public const Int16 gearoutmodLoc = 172;                                //A              F16

        //传动比率，16.32格式，高16位为整数部分，低32位为小数部分。
        public const Int16 gearratioLoc = 175;                                 //A              S16.32

        //脱离啮合后的运行速度。
        public const Int16 gearoutvelLoc = 178;                                //A              S16.16

        #endregion

        //********************************************************************************************************/
        
        #region 环形轴

        //若该轴设为环形轴，此参数设置环形轴的最大位置。
        public const Int16 cirposLoc = 184;                                    //A              S32

        //设置该轴为线性轴或环形轴。若ciraxis为0，该轴为线性轴；若为FFFFh，该轴设置为环形轴。
        public const Int16 ciraxisLoc = 186;                                   //A              F16

        //单向或双向环形标志
        public const Int16 biciraxisLoc = 187;                                 //A              F16

        //记录循环次数，向上溢出（即curpos>=cirpos）一次；该寄存器值加1，
        //       向下溢出（即curpos<=-cirpos或curpos<=0），该寄存器值减1。
        public const Int16 cirswapLoc = 214;                                   //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region 非线性同步控制参数

        //使能同步控制，0：禁止，FFFFh：使能。同步运动完成后startsyn和enasyn都自动清零。
        public const Int16 enasynLoc = 188;                                    //A              F16

        //写入FFFFh开始执行同步，写入0退出同步。同步运动完成后startsyn和enasyn都自动清零。
        public const Int16 startsynLoc = 189;                                  //A              F16

        //同步类型，电子齿轮的同步运动都是速度同步，因此syntype必须清零。
        public const Int16 syntypeLoc = 190;                                   //A              F16

        //指向同步源参数的指针，写入时是取同步源参数的地址。
        public const Int16 synsrcLoc = 191;                                    //A              U16

        //同步源参数所在的轴号，即参照轴。
        public const Int16 synaxisLoc = 192;                                   //A              U16

        //设置整个同步过程中，同步源变化的量，若同步源是位移类参数，synsrcvar表示同步源轴移动的距离，
        //若同步源是ticks，synsrcvar表示历经的控制周期数。注意，该参数必须为正整数。
        public const Int16 synsrcvarLoc = 193;                                 //A              S32


        //若syntype非零，该参数用于设置整个同步过程中从动轴的移动目标：
        //若slaveabs=0，该参数表示相对移动距离；若slaveab!=0，该参数表示绝对位置；若syntype为零，该参数无需设置。
        public const Int16 slavedistLoc = 197;                                 //A              S32

        //若slaveabs标志参数为0，slavedist表示的移动目标是相对移动距离；
        //若slaveabs标志参数非零，slavedist表示的是绝对位置。
        public const Int16 slaveabsLoc = 199;                                  //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region 运动状态

        //标志是否规划运动中，0：规划运动已停止，FFFFh：规划运动中（包括主坐标系和辅坐标系，以及轮廓运动）。
        public const Int16 profilingLoc = 215;                                 //A              F16

        //标志是否正在进行轮廓运动中，FFFFh：轮廓运动中，0：轮廓运动已结束或CFIFO已空。
        public const Int16 contouringLoc = 217;                                //A              F16

        //标志是否规划运动（包括主坐标系和辅坐标系）以及运动平滑处理中，
        //0：停止规划运动且停止平滑处理，FFFFh：规划运动未完成或运动平滑处理进行中。
        public const Int16 movingLoc = 218;                                    //A              F16

        //电机是否静止，0：规划运动已完成，且电机已静止；FFFFh：规划运动未完成，或虽已完成运动规划，但电机尚未静止。
        public const Int16 motionLoc = 219;                                    //A              F16

        //位置误差越出静止窗口标志，若outsettle=FFFFh，表明当前位置误差poserr大于静止窗口参数settlewin。
        public const Int16 outsettleLoc = 220;                                 //A              F16

        //位置误差越出跟随窗口标志，若outtrack=FFFFh，表明当前位置误差poserr大于跟随窗口参数。
        public const Int16 outtrackLoc = 221;                                  //A              F16

        //位置误差，指令位置与实际位置（反馈值）之差：poserr=curpos-encp。
        public const Int16 poserrLoc = 223;                                    //A              S16

        #endregion
        
        //********************************************************************************************************/
        
        #region 指令脉冲相关参数

        //脉冲输出模式及信号极性设置寄存器
        public const Int16 stepmodLoc = 615;                                   //A              R16

        //设置方向信号变化的延迟时间，单位是系统的时钟周期
        public const Int16 dirtimeLoc = 618;                                   //A              U16

        //设定脉冲有效电平宽度，单位是系统的时钟周期
        public const Int16 steptimeLoc = 619;                                  //A              R16

        //非零则暂停输出脉冲，清零则又继续允许发脉冲
        public const Int16 haltstepLoc = 166;                                  //A              R16

        //暂停输出脉冲模式。0:立刻停止；非零:减速输出指令脉冲，直到停止
        public const Int16 haltmodeLoc = 167;                                  //A              R16

        #endregion

        //********************************************************************************************************/
        
        #region 位置捕获参数

        //当前读指针指向的位置值
        public const Int16 capfifoLoc = 696;                                   //A              S32

        //写操作则将capfifo的读指针往后移一个
        public const Int16 popcapfifoLoc = 698;                                //A              R16

        //写操作则清空位置捕获缓存器capfifo
        public const Int16 clrcapfifoLoc = 699;                                //A              R16

        //capfifo中已压入的位置数据个数
        public const Int16 capfifocntLoc = 699;                                //A              R16

        //往该寄存器写操作则触发捕获位置，并保存在cappos参数中。
        public const Int16 captureLoc = 543;                                   //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region 探针或index计数相关参数


        //探针或index的计数值
        public const Int16 counterLoc = 541;                                   //A              F16

        //探针或index的计数值
        public const Int16 clrcounterLoc = 541;                                //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region 轮廓运动

        //置FFFFh开始执行轮廓运动，清零则结束轮廓运动。
        public const Int16 startgroupLoc = 256;                                //G              F16

        //轮廓运动的轴组中，轴的数量。
        public const Int16 groupnumLoc = 257;                                  //G              U16

        //轮廓运动的轴组中，X轴对应的轴号。
        public const Int16 group_xLoc = 258;                                   //G              U16

        //轮廓运动的轴组中，Y轴对应的轴号。
        public const Int16 group_yLoc = 259;                                   //G              U16

        //轮廓运动的轴组中，Z轴对应的轴号。
        public const Int16 group_zLoc = 260;                                   //G              U16

        //轮廓运动的轴组中，A轴对应的轴号。
        public const Int16 group_aLoc = 261;                                   //G              U16

        //轮廓运动的轴组中，B轴对应的轴号。
        public const Int16 group_bLoc = 262;                                   //G              U16

        //轮廓运动的轴组中，C轴对应的轴号。
        public const Int16 group_cLoc = 263;                                   //G              U16

        //轮廓运动的轴组中，D轴对应的轴号。
        public const Int16 group_dLoc = 264;                                   //G              U16

        //轮廓运动的轴组中，E轴对应的轴号。
        public const Int16 group_eLoc = 265;                                   //G              U16

        //轮廓运动的轴组中，F轴对应的轴号。
        public const Int16 group_fLoc = 266;                                   //G              U16

        //轮廓运动的轴组中，G轴对应的轴号。
        public const Int16 group_gLoc = 267;                                   //G              U16

        //轮廓运动的轴组中，H轴对应的轴号。
        public const Int16 group_hLoc = 268;                                   //G              U16

        //轮廓运动的轴组中，I轴对应的轴号。
        public const Int16 group_iLoc = 269;                                   //G              U16

        //轮廓运动的轴组中，J轴对应的轴号。
        public const Int16 group_jLoc = 270;                                   //G              U16

        //轮廓运动的轴组中，K轴对应的轴号。
        public const Int16 group_kLoc = 271;                                   //G              U16

        //轮廓运动的轴组中，L轴对应的轴号。
        public const Int16 group_lLoc = 272;                                   //G              U16

        //轮廓运动的轴组中，M轴对应的轴号。
        public const Int16 group_mLoc = 273;                                   //G              U16

        //轮廓运动的平滑拟合时间，单位为控制周期
        public const Int16 groupsmoothLoc = 274;                               //G              U16

        //*************************************************************************************************************/
        //轮廓运动专用CFIFO缓存器的相关参数

        //CFIFO中数据（WORD）的个数
        public const Int16 cfifocntLoc = 519;                                  //G              R16

        //写操作清空CFIFO
        public const Int16 clrCFIFOLoc = 519;                                  //G              R16

        #endregion

        //********************************************************************************************************/
        
        #region IFIFO/QFIFO缓存器相关参数

        //写操作清空IFIFO
        public const Int16 clrififoLoc = 513;                                  //G              F16

        //IFIFO中数据（WORD）的个数
        public const Int16 ififocntLoc = 513;                                  //G              F16

        //写操作清空QFIFO
        public const Int16 clrqfifoLoc = 521;                                  //G              F16

        //QFIFO中数据（WORD）的个数
        public const Int16 qfifocntLoc = 521;                                  //G              F16

        //QFIFO的等待指令的超时时间
        public const Int16 qwaittimeLoc = 492;                                 //G              S32

        #endregion

        //********************************************************************************************************/
        
        #region 预定义用户参数
        public const Int16 user16b0Loc = 307;                                  //G              S16
        public const Int16 user16b1Loc = 308;                                  //G              S16
        public const Int16 user16b2Loc = 309;                                  //G              S16
        public const Int16 user16b3Loc = 310;                                  //G              S16
        public const Int16 user16b4Loc = 311;                                  //G              S16
        public const Int16 user16b5Loc = 312;                                  //G              S16
        public const Int16 user16b6Loc = 313;                                  //G              S16
        public const Int16 user16b7Loc = 314;                                  //G              S16
        public const Int16 user16b8Loc = 315;                                  //G              S16
        public const Int16 user16b9Loc = 316;                                  //G              S16

        public const Int16 user32b0Loc = 317;                                  //G              S32
        public const Int16 user32b1Loc = 319;                                  //G              S32
        public const Int16 user32b2Loc = 321;                                  //G              S32
        public const Int16 user32b3Loc = 323;                                  //G              S32
        public const Int16 user32b4Loc = 325;                                  //G              S32
        public const Int16 user32b5Loc = 327;                                  //G              S32
        public const Int16 user32b6Loc = 329;                                  //G              S32
        public const Int16 user32b7Loc = 331;                                  //G              S32
        public const Int16 user32b8Loc = 333;                                  //G              S32
        public const Int16 user32b9Loc = 335;                                  //G              S32

        public const Int16 user48b0Loc = 337;                                  //G              S48
        public const Int16 user48b1Loc = 340;                                  //G              S48
        public const Int16 user48b2Loc = 343;                                  //G              S48
        public const Int16 user48b3Loc = 346;                                  //G              S48
        public const Int16 user48b4Loc = 349;                                  //G              S48

        #endregion

        //********************************************************************************************************/
        
        #region 插补运动的轴参数

        //轴参数，设置段的坐标数据以绝对值还是相对值表示
        public const Int16 pathabsLoc = 205;                                   //A              F16

        //当前执行段的终点
        public const Int16 segendLoc = 202;                                    //A              S32

        //当前执行段的起始点
        public const Int16 segstartLoc = 200;                                  //A              S32

        //轴参数，表明该轴是否参与路径运动并正在运动中
        public const Int16 moveinpath1Loc = 204;                               //A              F16

        //轴参数，表明该轴是否参与路径运动并正在运动中
        public const Int16 moveinpath2Loc = 165;                               //A              F16

        //插补空间1的参数***********************************************************************************************************/

        //写入非零开始执行路径运动
        public const Int16 startpath1Loc = 352;                                //G              F16

        //标志是否正在执行插补
        public const Int16 pathmoving1Loc = 354;                               //G              F16

        //当前执行圆弧段的方向，0：顺时针，非零：逆时针
        public const Int16 arcdir1Loc = 355;                                   //G              F16

        //指定速度规划是否基于该段合成路径的长度，或某个轴在该段的移动距离。
        //若asseglen为0，速度规划基于X、Y、Z三轴的合成路径长度，即pathvel是合成路径的速度。
        //当然，当pathaxisnum小于3时，则只有X轴或X、Y轴合成路径长度。
        //若asseglen非零，asseglen必须为1~pathaxisnum范围的一个值，表示采用segmap_x、segmap_y…所映射的轴的
        //移动距离进行速度规划，如1表示采用X轴的移动距离规划速度，因此pathvel即为X轴的速度。
        public const Int16 asseglen1Loc = 361;                                 //G              F16

        //当前路径速度（只读）
        public const Int16 pathvel1Loc = 362;                                          //G              S16.16

        //若非零：连续路径插补为轮廓模式，每个点的各个轴分量为16bit的增量，PFIFO中送入的全是轮廓路径中的各点坐标的增量
        //       public const Int16 contourmod1Loc =                   365     //G              F16

        //路径加速度
        public const Int16 pathacc1Loc = 366;                                  //G              S16.16
        //进给倍率，供在路径运动过程中实时改变路径速度
        public const Int16 feedrate1Loc = 368;                                 //G              S16.16
        //参与路径运动的轴数
        public const Int16 pathaxisnum1Loc = 370;                              //G              F16
        //映射为X轴的轴号
        public const Int16 segmap_x1Loc = 371;                                 //G              F16
        //映射为Y轴的轴号
        public const Int16 segmap_y1Loc = 372;                                 //G              F16
        //映射为Z轴的轴号
        public const Int16 segmap_z1Loc = 373;                                 //G              F16
        //映射为A轴的轴号
        public const Int16 segmap_a1Loc = 374;                                 //G              F16
        //映射为B轴的轴号
        public const Int16 segmap_b1Loc = 375;                                 //G              F16
        //映射为C轴的轴号
        public const Int16 segmap_c1Loc = 376;                                 //G              F16
        //映射为D轴的轴号
        public const Int16 segmap_d1Loc = 377;                                 //G              F16
        //映射为E轴的轴号
        public const Int16 segmap_e1Loc = 378;                                 //G              F16
        //映射为F轴的轴号
        public const Int16 segmap_f1Loc = 379;                                 //G              F16
        //映射为G轴的轴号
        public const Int16 segmap_g1Loc = 380;                                 //G              F16
        //映射为H轴的轴号
        public const Int16 segmap_h1Loc = 381;                                 //G              F16
        //映射为I轴的轴号
        public const Int16 segmap_i1Loc = 382;                                 //G              F16
        //映射为J轴的轴号
        public const Int16 segmap_j1Loc = 383;                                 //G              F16
        //映射为K轴的轴号
        public const Int16 segmap_k1Loc = 384;                                 //G              F16
        //映射为L轴的轴号
        public const Int16 segmap_l1Loc = 385;                                 //G              F16
        //映射为M轴的轴号
        public const Int16 segmap_m1Loc = 386;                                 //G              F16
        //段执行的目标速度
        public const Int16 segtgvel1Loc = 387;                                 //G              S16.16
        //段结束时的速度
        public const Int16 segendvel1Loc = 389;                                //G              S16.16
        //正在执行的段的ID号，每执行一段，该ID加1
        public const Int16 segID1Loc = 391;                                    //G              U32
        //当前执行段的长度
        public const Int16 seglen1Loc = 393;                                   //G              U32
        //当前执行圆弧段的半径
        public const Int16 radius1Loc = 395;                                   //G              U32


        //PFIFO1缓存器相关参数

        //对此寄存器写操作清空PFIFO
        public const Int16 clrPFIFO1Loc = 565;                                //G              F16

        //PFIFO中数据个数
        public const Int16 PFIFOcnt1Loc = 565;                                 //G              F16

        //PFIFO1等待指令超时时间
        public const Int16 pwaittime1Loc = 399;                                //G              S32


        //PFIFO2***********************************************************************************************************/

        //写入非零开始执行路径运动
        public const Int16 startpath2Loc = 405;                                //G              F16
        //标志是否正在执行插补
        public const Int16 pathmoving2Loc = 407;                               //G              F16
        //当前执行圆弧段的方向，0：顺时针，非零：逆时针
        public const Int16 arcdir2Loc = 408;                                   //G              F16

        //指定速度规划是否基于该段合成路径的长度，或某个轴在该段的移动距离。
        //若asseglen为0，速度规划基于X、Y、Z三轴的合成路径长度，即pathvel是合成路径的速度。
        //当然，当pathaxisnum小于3时，则只有X轴或X、Y轴合成路径长度。
        //若asseglen非零，asseglen必须为1~pathaxisnum范围的一个值，表示采用segmap_x、segmap_y…所映射的轴的
        //移动距离进行速度规划，如1表示采用X轴的移动距离规划速度，因此pathvel即为X轴的速度。
        public const Int16 asseglen2Loc = 414;                                 //G              F16

        //当前路径速度（只读）
        public const Int16 pathvel2Loc = 415;                                  //G              S16.16

        //若非零：连续路径插补为轮廓模式，每个点的各个轴分量为16bit的增量，PFIFO中送入的全是轮廓路径中的各点坐标的增量
        //       public const Int16 contourmod2Loc = 418                               //G              F16

        //路径加速度
        public const Int16 pathacc2Loc = 419;                                  //G              S16.16
        //进给倍率，供在路径运动过程中实时改变路径速度
        public const Int16 feedrate2Loc = 421;                                 //G              S16.16
        //参与路径运动的轴数
        public const Int16 pathaxisnum2Loc = 423;                              //G              F16
        //映射为X轴的轴号
        public const Int16 segmap_x2Loc = 424;                                 //G              F16
        //映射为Y轴的轴号
        public const Int16 segmap_y2Loc = 425;                                 //G              F16
        //映射为Z轴的轴号
        public const Int16 segmap_z2Loc = 426;                                 //G              F16
        //映射为A轴的轴号
        public const Int16 segmap_a2Loc = 427;                                 //G              F16
        //映射为B轴的轴号
        public const Int16 segmap_b2Loc = 428;                                 //G              F16
        //映射为C轴的轴号
        public const Int16 segmap_c2Loc = 429;                                 //G              F16
        //映射为D轴的轴号
        public const Int16 segmap_d2Loc = 430;                                 //G              F16
        //映射为E轴的轴号
        public const Int16 segmap_e2Loc = 431;                                 //G              F16
        //映射为F轴的轴号
        public const Int16 segmap_f2Loc = 432;                                 //G              F16
        //映射为G轴的轴号
        public const Int16 segmap_g2Loc = 433;                                 //G              F16
        //映射为H轴的轴号
        public const Int16 segmap_h2Loc = 434;                                 //G              F16
        //映射为I轴的轴号
        public const Int16 segmap_i2Loc = 435;                                 //G              F16
        //映射为J轴的轴号
        public const Int16 segmap_j2Loc = 436;                                 //G              F16
        //映射为K轴的轴号
        public const Int16 segmap_k2Loc = 437;                                 //G              F16
        //映射为L轴的轴号
        public const Int16 segmap_l2Loc = 438;                                 //G              F16
        //映射为M轴的轴号
        public const Int16 segmap_m2Loc = 439;                                 //G              F16
        //段执行的目标速度
        public const Int16 segtgvel2Loc = 440;                                 //G              S16.16
        //段结束时的速度
        public const Int16 segendvel2Loc = 442;                                //G              S16.16
        //段的ID，用于标识正在执行第几段
        public const Int16 segID2Loc = 444;                                    //G              U32


        //PFIFO2缓存器相关参数

        //对此寄存器写操作清空PFIFO
        public const Int16 clrPFIFO2Loc = 685;                                 //G              F16

        //PFIFO中数据个数
        public const Int16 PFIFOcnt2Loc = 685;                                 //G              F16

        //PFIFO2等待指令超时时间
        public const Int16 pwaittime2Loc = 452;                                //G              S32

        #endregion

        //********************************************************************************************************/
        
        #region 输入输出（I/O）相关参数


        //脉冲输出及驱动器使能。写入FFFFh则使能脉冲输出及使能驱动器。注意：写入FFFFh，驱动器使能信号为低电平，
        //写入0则为高电平。若需将轴作为虚拟轴(运行正常，但不输出脉冲)，可清零ena。
        //另：可以用于判断脉冲输出和驱动器是否使能，0：脉冲输出和驱动器禁止，FFFFh：脉冲输出和驱动器使能。
        public const Int16 enaLoc = 550;                                       //A              F16

        //轴I/O数据寄存器。某I/O设为输入时，读出的是对应管脚的实时信号值；
        //若设为输出，写入0或1可使该管脚输出低或高电平，读则得到写入到该寄存器的值。
        public const Int16 aioLoc = 562;                                       //A              R16

        //轴I/O的控制寄存器
        public const Int16 aioctrLoc = 680;                                    //A              R16

        //轴I/O设为输入时锁存信号的有效边沿。
        public const Int16 aiolatLoc = 682;                                    //A              R16

        //相应的位域写入1则清零轴I/O的锁存值。
        public const Int16 clraiolatLoc = 682;                                 //A              R16

        //全局开关量输出gout1
        public const Int16 gout1Loc = 560;                                     //G              R16

        //全局开关量输出gout2
        public const Int16 gout2Loc = 561;                                     //G              R16

        //全局开关量输出gout3
        public const Int16 gout3Loc = 555;                                     //G              R16

        //全局开关量输入gin1
        public const Int16 gin1Loc = 706;                                      //G              R16

        //全局开关量输入gin2
        public const Int16 gin2Loc = 707;                                      //G              R16

        //锁存全局开关量输入gin1的有效边沿
        public const Int16 gin1latLoc = 612;                                      //G              R16

        //锁存全局开关量输入gin2的有效边沿
        public const Int16 gin2latLoc = 613;                                      //G              R16

        //读得到停止开关的状态，写则设置停止开关的有效极性
        public const Int16 stopinLoc = 563;                                    //G              R16

        //输入开关量的数字滤波设置寄存器
        public const Int16 swfilterLoc = 548;                                  //G              R16

        //伺服驱动器误差清零
        public const Int16 srstLoc = 551;                                      //A              R16

        #endregion

        //********************************************************************************************************/
        
        #region 计时单元相关的参数

        //毫秒计时，写入毫秒数，每毫秒减1
        public const Int16 delaymsLoc = 704;                                   //G              U32

        //delayms倒计时为0后该参数为0
        public const Int16 delayoutLoc = 704;                                  //G              U32

        //倒计时计数器，写入非零开始每周期减1
        public const Int16 timerLoc = 481;                                     //G              U16

        //控制周期计数器，每控制周期加1
        public const Int16 ticksLoc = 502;                                     //G              U32

        #endregion

        //事件指令相关参数************************************************************************
        //事件指令数量，清零则禁止所有事件指令
        public const Int16 eventsLoc = 489;                                    //G              U16


        
        #region 急停/暂停相关参数

        //某些位域若置位，则使所有轴的error寄存器相应位域置位
        public const Int16 emstopLoc = 500;                                    //G              F16

        //非零则暂停
        public const Int16 hpauseLoc = 501;                                    //G              F16

        #endregion

       
        #region 系统参数 Read only
        public const Int16 clkdivLoc = 509;                                    //G              U16             控制分频
        public const Int16 fwversionLoc = 511;                                 //G              U16             本控制器firmware的版本号。
        public const Int16 sysclkLoc = 628;                                    //G              U32             系统基准时钟。
        public const Int16 naxisLoc = 634;                                     //G              U16             本控制器支持的最大轴数。
        public const Int16 hwversionLoc = 635;                                 //G              U16             硬件版本（根据硬件版本不同）

        #endregion

        //清空所有FIFO和残留的等待指令等
        public const Int16 clearimcLoc = 494;                                  //G              R16

    }

}