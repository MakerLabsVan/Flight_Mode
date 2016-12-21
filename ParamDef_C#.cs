using System;


namespace imcpkg
{

//***********************************************************************************************************/
//       ��ͷ�ļ�ΪiMC������ַ�ĺ궨��                                                                      */
//       ��ʽ��                                                                                             */
//       //����                                                                                             */
//       public const Int16   ������Loc =      ��ַ     // ���ȫ�ֲ���  ���ݸ�ʽ                           */
//                                                                                                          */
//       ���С�����������ָ��iMC�Ĳ�����������Loc��ʾ�ò����ĵ�ַ                                         */
//                                                                                                          */
//       ��ĸA��ʾ���������ĸG��ʾȫ�ֲ���                                                                 */
//                                                                                                          */
//       iMC�й������¼������ݸ�ʽ�����ͣ�                                                                  */
//       S32��32λ������λ����������С�����ֵ�λ��Ϊ0������ֵ�ķ�ΧΪ��[-2147483648,2147483647]��           */
//       U32��32λ�޷���λ����������С�����ֵ�λ��Ϊ0������ֵ�ķ�ΧΪ��[0, 4294967295]��                    */
//       S16��16λ������λ����������С�����ֵ�λ��Ϊ0������ֵ�ķ�ΧΪ��[-32768,32767]��                     */
//       U16��16λ�޷���λ����������С�����ֵ�λ��Ϊ0������ֵ�ķ�ΧΪ��[0, 65535]��                         */
//       F16��16λ��־��������ȡ����ֵ��0��FFFFh��                                                          */
//       R16��16λ�Ĵ�������λ����о�������壬����λ�������á�                                            */
//       S16.32��48λ������λ����16λΪ�������֣���32λΪС�����֣�����ֵ�ķ�Χ��[-32768.0,32767.999999999767] */
//       U16.32��48λ�޷���λ����16λΪ�������֣���32λΪС�����֣�����ֵ�ķ�Χ��[0.0,65535.999999999767]   */
//       S16.16��32λ������λ����16λΪ�������֣���16λΪС�����֣�����ֵ�ķ�Χ��[-32768.0,32767.999984741211]  */
//       U16.16��32λ�޷���λ����16λΪ�������֣���32λΪС�����֣�����ֵ�ķ�Χ��[0.0,65535.999984741211]�� */
//       S0.16��16λ�з���λ��16λΪС�����֡�                                                              */
//       ��ˣ�iMC�еĲ���ֵ����ʾ��ʵ��ֵΪ��                                                              */
//       ʵ��ֵ=����ֵ/2^n                                                                                  */
//       ����nΪС�����ֵ�λ����                                                                            */
//       ���磬����һ��S32��ʽ��������00018000h����h��ʾʮ�����Ʊ�ʾ����ʾ��ʮ���Ƶ�ֵΪ98304/2^0 = 98304�� */
//       ����һ��S16.16��ʽ��������00018000h����ʾ��ʮ���Ƶ�ֵΪ98304/2^16 = 1.5��                          */
//***********************************************************************************************************/

    class ParamDef
    {
        //********************************************************************************************************/

        #region ������ϵ��MCS���㵽�����

        //��ǰ�滮�ٶȣ�������ϵ��
        public const Int16 mcsvelLoc = 4;                                      //A              S16.16

        //�����ٶ��˶���Ŀ���ٶȣ�������ϵ��
        public const Int16 mcstgvelLoc = 6;                                    //A              S16.16

        //������ϵ�Ƿ����ٶ�б�������У����ٻ���٣���FFFFh�������ٶ�б�����̣�0������Ŀ���ٶ����С�
        //�����ӳ����˶�ʱ�������жϴӶ����Ƿ��Ѵﵽ�����ٶȣ����Ƿ�ﵽ�������ٶȳ��Դ������ʵ�ֵ��0���ﵽ��FFFFh��δ�ﵽ
        public const Int16 mcsslopeLoc = 8;                                    //A              F16

        //������ϵ��ָ��λ��
        public const Int16 mcsposLoc = 10;                                     //A              S32

        //����Ŀ��λ�ã�������ϵ��
        public const Int16 mcstgposLoc = 12;                                   //A              S32

        //Ŀ���ƶ����루������ϵ�㵽���˶���
        public const Int16 mcsdistLoc = 14;                                    //A              S32

        //�㵽���˶�������ٶȣ�������ϵ��
        public const Int16 mcsmaxvelLoc = 16;                                  //A              S16.16

        //���ٶȣ�������ϵ��
        public const Int16 mcsaccelLoc = 18;                                   //A              S16.16

        //���ٶȣ�������ϵ��
        public const Int16 mcsdecelLoc = 20;                                   //A              S16.16

        //�㵽���˶���ģʽ��0����ͨģʽ��FFFFh������ģʽ��������ϵ��
        public const Int16 mcstrackLoc = 22;                                   //A              F16

        //д��FFFFh�����㵽���˶���д���㣬ֹͣ��ǰ�ĵ㵽���˶���������ϵ��
        public const Int16 mcsgoLoc = 23;                                      //A              F16

        //��־������ϵ�Ƿ�滮�˶��У�0���滮�˶���ֹͣ��FFFFh���滮�˶���
        public const Int16 mcsmovingLoc = 25;                                  //A              F16

        #endregion

        //********************************************************************************************************/

        #region ������ϵ��PCS���㵽�����

        //��ǰ�滮�ٶȣ�������ϵ��
        public const Int16 pcsvelLoc = 27;                                     //A              S16.16

        //�����ٶ��˶���Ŀ���ٶȣ�������ϵ����
        public const Int16 pcstgvelLoc = 29;                                   //A              S16.16

        //��־������ϵ�Ƿ����ٶ�б�������У����ٻ���٣���FFFFh�������ٶ�б�����̣�0������Ŀ���ٶ�����
        public const Int16 pcsslopeLoc = 31;                                   //A              F16

        //ָ��λ�ã�������ϵ��
        public const Int16 pcsposLoc = 33;                                     //A              S32

        //����Ŀ��λ�ã�������ϵ��
        public const Int16 pcstgposLoc = 35;                                   //A              S32

        //Ŀ���ƶ����루������ϵ�㵽���˶���
        public const Int16 pcsdistLoc = 37;                                    //A              S32

        //�㵽���˶�������ٶȣ�������ϵ��
        public const Int16 pcsmaxvelLoc = 39;                                  //A              S16.16

        //���ٶȣ�������ϵ����
        public const Int16 pcsaccelLoc = 41;                                   //A              S16.16

        //���ٶȣ�������ϵ����
        public const Int16 pcsdecelLoc = 43;                                   //A              S16.16

        //�㵽���˶���ģʽ��0����ͨģʽ��1������ģʽ��������ϵ����
        public const Int16 pcstrackLoc = 45;                                   //A              F16

        //д��FFFFh�����㵽���˶���д���㣬ֹͣ��ǰ�ĵ㵽���˶���������ϵ����
        public const Int16 pcsgoLoc = 46;                                      //A              F16

        //��־������ϵ�Ƿ�滮�˶��У�0���滮�˶���ֹͣ��FFFFh���滮�˶��С�
        public const Int16 pcsmovingLoc = 48;                                  //A              F16


        //��ǰָ��λ�ã�������������ϵĿ��λ�õ��Ӳ������˶�ƽ�����ܵ�ָ��λ�á�
        public const Int16 curposLoc = 225;                                    //A              S32

        //��ǰ�ٶȣ�������������ϵ���ٶȵ��Ӳ������˶�ƽ�����ܵ��ٶȡ�
        public const Int16 curvelLoc = 227;                                    //A              S16.16

        //0��������ϵ�ƶ��ľ����ۼӵ�������ϵ��Ŀ��λ��mcstgpos��
        //���㣺������ϵ�ƶ��ľ��벻�ۼӵ�������ϵ��Ŀ��λ��mcstgpos
        public const Int16 shiftmasterLoc = 174;                               //A              F16

        #endregion

        //********************************************************************************************************/
        #region ��������

        //���������ı������ӣ�ӦΪ��ֵ��
        public const Int16 encpfactorLoc = 73;                                 //A              U16.16

        //���������ٶȣ���λ������ֵÿ�������ڡ�
        public const Int16 encpvelLoc = 75;                                    //A              S16.16

        //������������ֵ������ǰʵ��λ�á�
        public const Int16 encpLoc = 78;                                       //A              S32

        //�����������ƺ�״̬�Ĵ���
        public const Int16 encpctrLoc = 539;                                   //A              R16

        #endregion

        //********************************************************************************************************/
        #region ��������

        //���������ı������ӣ�ӦΪ��ֵ��
        public const Int16 encsfactorLoc = 81;                                 //A              U16.16

        //���������ٶȣ���λ������ֵÿ�������ڡ�
        public const Int16 encsvelLoc = 83;                                    //A              S16.16

        //������������ֵ�Ĵ�����
        public const Int16 encsLoc = 86;                                       //A              S32

        //�����������Ŀ��ƼĴ���
        public const Int16 encsctrLoc = 531;                                   //A              R16

        //д�üĴ������㸨�����������Ĵ���
        public const Int16 clrencsLoc = 532;                                   //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region ���������

        //���и��ᡣֻ�����и�����ܽ����˶��滮��������������˳����У�run���㣬���ᴦ��ֹͣ����״̬��
        public const Int16 runLoc = 128;                                       //A              F16

        //״̬�Ĵ���
        public const Int16 statusLoc = 129;                                    //A              R16

        //����Ĵ���
        public const Int16 errorLoc = 130;                                     //A              R16

        //λ�����(poserr)������ֵ��������ֵ������Ĵ�������Ӧ��λ����λ��ע�⣺����Ϊ��ֵ��
        public const Int16 poserrlimLoc = 131;                                 //A              U16

        //�˶�ƽ�����ӡ�
        public const Int16 smoothLoc = 132;                                    //A              U16

        //�������ڴ�С����λ��λ�Ƶ�λ������ֵ������㡣
        public const Int16 trackwinLoc = 133;                                  //A              U16

        //�����ֹ���ڴ�С����λ��λ�Ƶ�λ������ֵ������㡣
        public const Int16 settlewinLoc = 134;                                 //A              U16

        //���õ�����˶����������뾲ֹ״̬����ʱ����������ֹͣ�滮�˶���ƽ������(moving=0)��
        //       �ھ�ֹ�����ڵ�״̬(outsettle=0)����settlen�趨�Ŀ������ڸ���������motion��������־����Ѿ�ֹ��
        public const Int16 settlenLoc = 135;                                   //A              U16

        //ָ���������ִ���ʱֹͣ�滮�˶���stopfilt����λ��λ����ʹ��������Ĵ���error�ж�Ӧ��λ����λ��ֹͣ�˶���
        public const Int16 stopfiltLoc = 136;                                  //A              R16


        public const Int16 stopmodeLoc = 137;                                  //A              R16

        //ָ���������ִ���ʱֹͣ�������С�exitfilt����λ��λ����ʹ��������Ĵ���error�ж�Ӧ��λ����λ��ֹͣ����(����run)��
        public const Int16 exitfiltLoc = 138;                                  //A              R16

        //��������λ�ã�Ĭ��Ϊ���������ֵ��
        public const Int16 psoftlimLoc = 139;                                  //A              S32

        //��������λ�ã�Ĭ��Ϊ���������ֵ��
        public const Int16 nsoftlimLoc = 141;                                  //A              S32

        //λ�öϵ㡣��curpos<breakp����״̬�Ĵ���status��BREAKPλ�����㣻��curpos��breakp����status��BREAKPλ����1��
        public const Int16 breakpLoc = 143;                                    //A              S32

        //ԭ���ƫ��λ��ֵ�����û�еԭ��ʱ����ֵ��������ָ��λ�üĴ���curpos�ͱ������Ĵ���encp�С�
        public const Int16 homeposLoc = 145;                                   //A              S32

        //��Ѱԭ��ʱ�����ٶε��ٶ�Ԥ��ֵ��
        public const Int16 lowvelLoc = 147;                                    //A              S16.16

        //��Ѱԭ��ʱ�����ٶε��ٶ�Ԥ��ֵ��
        public const Int16 highvelLoc = 149;                                   //A              S16.16

        //������Ѱԭ����̵��˶����кͲ�����ʽ��
        public const Int16 homeseqLoc = 151;                                   //A              R16

        //д��FFFFh����ʼִ���Զ���Ѱ�����û�еԭ��Ĳ�����ִ�еĶ���������homeseq�������壬�������˳���
        public const Int16 gohomeLoc = 152;                                    //A              F16

        //�����趨ԭ��λ��ָ�������ò���д��FFFFh��iMC����ǰλ����Ϊԭ�㡣
        public const Int16 sethomeLoc = 153;                                   //A              F16

        //�����ʾ����Ѱ��ԭ��
        public const Int16 homedLoc = 154;                                     //A              F16


        //д�����ֵ�Ը���ʵʩ����������������ı�����������ָ��λ�á�Ŀ��λ�õȸ���λ�ò�����
        //       �Լ������˶�״̬��־��mcspos��mcstgpos��curpos��encp��pcspos��pcstgpos��status��
        //       error��emstop��hpause��events��encs��ticks��aiolat��
        public const Int16 clearLoc = 157;                                     //A              F16

        //���ø�����ٶȼ���ֵ�����ۺ����˶�ģʽ��ֻҪʵ���ٶȳ����˼���ֵ������λ����Ĵ���error�е�λVELLIM��
        //       �˴��󲻿����Σ����ֻҪ�����˴��������˳��������С�ע�⣺����Ϊ��ֵ��
        public const Int16 vellimLoc = 158;                                    //A              S16.16


        //���ø���ļ��ٶȼ���ֵ�����ۺ����˶�ģʽ��ֻҪʵ�ʼ��ٶȳ����˼���ֵ������λ����Ĵ���error�е�ACCLIMλ��
        //       �˴��󲻿����Σ����ֻҪ�����˴��������˳��������С�ע�⣺accellim����Ϊ��ֵ��
        public const Int16 accellimLoc = 160;                                  //A              S16.16


        //�����ٶȡ�
        public const Int16 fixvelLoc = 162;                                    //A              S0.16

        //��������Ѱ��ԭ����ƶ���ָ��λ��ѡ��ò�������ָ��λ�á�
        public const Int16 homestposLoc = 163;                                 //A              S32

        
        #endregion

        //********************************************************************************************************/
        
        #region ���ӳ��ֲ���

        //���ӳ����˶�ģʽ�����������š�
        public const Int16 masterLoc = 169;                                    //A              U16

        //ָ�룬ָ��Ӷ����������������Ĳ�����
        public const Int16 gearsrcLoc = 170;                                   //A              U16

        //д��FFFFh��ʼ�Ӻϣ��������������ϡ�
        public const Int16 engearLoc = 171;                                    //A              F16

        //�������Ϻ�Ӷ�����˶�ģʽ��0���������Ϻ�Ӷ����Դ����ٶ���ΪĿ���ٶȽ��������ٶ��˶�ģʽ��
        //�������ٶȡ�������������֮ʱ����������ٶȳ��Դ������ʡ�FFFFh���������Ϻ�Ӷ�������gearoutvel���������õ��ٶ���Ϊ
        //Ŀ���ٶȽ��������ٶ��˶�ģʽ�������������Ϻ�ֹͣ��Ӧ����gearoutvel����ֵΪ0��
        public const Int16 gearoutmodLoc = 172;                                //A              F16

        //�������ʣ�16.32��ʽ����16λΪ�������֣���32λΪС�����֡�
        public const Int16 gearratioLoc = 175;                                 //A              S16.32

        //�������Ϻ�������ٶȡ�
        public const Int16 gearoutvelLoc = 178;                                //A              S16.16

        #endregion

        //********************************************************************************************************/
        
        #region ������

        //��������Ϊ�����ᣬ�˲������û���������λ�á�
        public const Int16 cirposLoc = 184;                                    //A              S32

        //���ø���Ϊ����������ᡣ��ciraxisΪ0������Ϊ�����᣻��ΪFFFFh����������Ϊ�����ᡣ
        public const Int16 ciraxisLoc = 186;                                   //A              F16

        //�����˫���α�־
        public const Int16 biciraxisLoc = 187;                                 //A              F16

        //��¼ѭ�������������������curpos>=cirpos��һ�Σ��üĴ���ֵ��1��
        //       �����������curpos<=-cirpos��curpos<=0�����üĴ���ֵ��1��
        public const Int16 cirswapLoc = 214;                                   //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region ������ͬ�����Ʋ���

        //ʹ��ͬ�����ƣ�0����ֹ��FFFFh��ʹ�ܡ�ͬ���˶���ɺ�startsyn��enasyn���Զ����㡣
        public const Int16 enasynLoc = 188;                                    //A              F16

        //д��FFFFh��ʼִ��ͬ����д��0�˳�ͬ����ͬ���˶���ɺ�startsyn��enasyn���Զ����㡣
        public const Int16 startsynLoc = 189;                                  //A              F16

        //ͬ�����ͣ����ӳ��ֵ�ͬ���˶������ٶ�ͬ�������syntype�������㡣
        public const Int16 syntypeLoc = 190;                                   //A              F16

        //ָ��ͬ��Դ������ָ�룬д��ʱ��ȡͬ��Դ�����ĵ�ַ��
        public const Int16 synsrcLoc = 191;                                    //A              U16

        //ͬ��Դ�������ڵ���ţ��������ᡣ
        public const Int16 synaxisLoc = 192;                                   //A              U16

        //��������ͬ�������У�ͬ��Դ�仯��������ͬ��Դ��λ���������synsrcvar��ʾͬ��Դ���ƶ��ľ��룬
        //��ͬ��Դ��ticks��synsrcvar��ʾ�����Ŀ�����������ע�⣬�ò�������Ϊ��������
        public const Int16 synsrcvarLoc = 193;                                 //A              S32


        //��syntype���㣬�ò���������������ͬ�������дӶ�����ƶ�Ŀ�꣺
        //��slaveabs=0���ò�����ʾ����ƶ����룻��slaveab!=0���ò�����ʾ����λ�ã���syntypeΪ�㣬�ò����������á�
        public const Int16 slavedistLoc = 197;                                 //A              S32

        //��slaveabs��־����Ϊ0��slavedist��ʾ���ƶ�Ŀ��������ƶ����룻
        //��slaveabs��־�������㣬slavedist��ʾ���Ǿ���λ�á�
        public const Int16 slaveabsLoc = 199;                                  //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region �˶�״̬

        //��־�Ƿ�滮�˶��У�0���滮�˶���ֹͣ��FFFFh���滮�˶��У�����������ϵ�͸�����ϵ���Լ������˶�����
        public const Int16 profilingLoc = 215;                                 //A              F16

        //��־�Ƿ����ڽ��������˶��У�FFFFh�������˶��У�0�������˶��ѽ�����CFIFO�ѿա�
        public const Int16 contouringLoc = 217;                                //A              F16

        //��־�Ƿ�滮�˶�������������ϵ�͸�����ϵ���Լ��˶�ƽ�������У�
        //0��ֹͣ�滮�˶���ֹͣƽ������FFFFh���滮�˶�δ��ɻ��˶�ƽ����������С�
        public const Int16 movingLoc = 218;                                    //A              F16

        //����Ƿ�ֹ��0���滮�˶�����ɣ��ҵ���Ѿ�ֹ��FFFFh���滮�˶�δ��ɣ�����������˶��滮���������δ��ֹ��
        public const Int16 motionLoc = 219;                                    //A              F16

        //λ�����Խ����ֹ���ڱ�־����outsettle=FFFFh��������ǰλ�����poserr���ھ�ֹ���ڲ���settlewin��
        public const Int16 outsettleLoc = 220;                                 //A              F16

        //λ�����Խ�����洰�ڱ�־����outtrack=FFFFh��������ǰλ�����poserr���ڸ��洰�ڲ�����
        public const Int16 outtrackLoc = 221;                                  //A              F16

        //λ����ָ��λ����ʵ��λ�ã�����ֵ��֮�poserr=curpos-encp��
        public const Int16 poserrLoc = 223;                                    //A              S16

        #endregion
        
        //********************************************************************************************************/
        
        #region ָ��������ز���

        //�������ģʽ���źż������üĴ���
        public const Int16 stepmodLoc = 615;                                   //A              R16

        //���÷����źű仯���ӳ�ʱ�䣬��λ��ϵͳ��ʱ������
        public const Int16 dirtimeLoc = 618;                                   //A              U16

        //�趨������Ч��ƽ��ȣ���λ��ϵͳ��ʱ������
        public const Int16 steptimeLoc = 619;                                  //A              R16

        //��������ͣ������壬�������ּ�����������
        public const Int16 haltstepLoc = 166;                                  //A              R16

        //��ͣ�������ģʽ��0:����ֹͣ������:�������ָ�����壬ֱ��ֹͣ
        public const Int16 haltmodeLoc = 167;                                  //A              R16

        #endregion

        //********************************************************************************************************/
        
        #region λ�ò������

        //��ǰ��ָ��ָ���λ��ֵ
        public const Int16 capfifoLoc = 696;                                   //A              S32

        //д������capfifo�Ķ�ָ��������һ��
        public const Int16 popcapfifoLoc = 698;                                //A              R16

        //д���������λ�ò��񻺴���capfifo
        public const Int16 clrcapfifoLoc = 699;                                //A              R16

        //capfifo����ѹ���λ�����ݸ���
        public const Int16 capfifocntLoc = 699;                                //A              R16

        //���üĴ���д�����򴥷�����λ�ã���������cappos�����С�
        public const Int16 captureLoc = 543;                                   //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region ̽���index������ز���


        //̽���index�ļ���ֵ
        public const Int16 counterLoc = 541;                                   //A              F16

        //̽���index�ļ���ֵ
        public const Int16 clrcounterLoc = 541;                                //A              F16

        #endregion

        //********************************************************************************************************/
        
        #region �����˶�

        //��FFFFh��ʼִ�������˶�����������������˶���
        public const Int16 startgroupLoc = 256;                                //G              F16

        //�����˶��������У����������
        public const Int16 groupnumLoc = 257;                                  //G              U16

        //�����˶��������У�X���Ӧ����š�
        public const Int16 group_xLoc = 258;                                   //G              U16

        //�����˶��������У�Y���Ӧ����š�
        public const Int16 group_yLoc = 259;                                   //G              U16

        //�����˶��������У�Z���Ӧ����š�
        public const Int16 group_zLoc = 260;                                   //G              U16

        //�����˶��������У�A���Ӧ����š�
        public const Int16 group_aLoc = 261;                                   //G              U16

        //�����˶��������У�B���Ӧ����š�
        public const Int16 group_bLoc = 262;                                   //G              U16

        //�����˶��������У�C���Ӧ����š�
        public const Int16 group_cLoc = 263;                                   //G              U16

        //�����˶��������У�D���Ӧ����š�
        public const Int16 group_dLoc = 264;                                   //G              U16

        //�����˶��������У�E���Ӧ����š�
        public const Int16 group_eLoc = 265;                                   //G              U16

        //�����˶��������У�F���Ӧ����š�
        public const Int16 group_fLoc = 266;                                   //G              U16

        //�����˶��������У�G���Ӧ����š�
        public const Int16 group_gLoc = 267;                                   //G              U16

        //�����˶��������У�H���Ӧ����š�
        public const Int16 group_hLoc = 268;                                   //G              U16

        //�����˶��������У�I���Ӧ����š�
        public const Int16 group_iLoc = 269;                                   //G              U16

        //�����˶��������У�J���Ӧ����š�
        public const Int16 group_jLoc = 270;                                   //G              U16

        //�����˶��������У�K���Ӧ����š�
        public const Int16 group_kLoc = 271;                                   //G              U16

        //�����˶��������У�L���Ӧ����š�
        public const Int16 group_lLoc = 272;                                   //G              U16

        //�����˶��������У�M���Ӧ����š�
        public const Int16 group_mLoc = 273;                                   //G              U16

        //�����˶���ƽ�����ʱ�䣬��λΪ��������
        public const Int16 groupsmoothLoc = 274;                               //G              U16

        //*************************************************************************************************************/
        //�����˶�ר��CFIFO����������ز���

        //CFIFO�����ݣ�WORD���ĸ���
        public const Int16 cfifocntLoc = 519;                                  //G              R16

        //д�������CFIFO
        public const Int16 clrCFIFOLoc = 519;                                  //G              R16

        #endregion

        //********************************************************************************************************/
        
        #region IFIFO/QFIFO��������ز���

        //д�������IFIFO
        public const Int16 clrififoLoc = 513;                                  //G              F16

        //IFIFO�����ݣ�WORD���ĸ���
        public const Int16 ififocntLoc = 513;                                  //G              F16

        //д�������QFIFO
        public const Int16 clrqfifoLoc = 521;                                  //G              F16

        //QFIFO�����ݣ�WORD���ĸ���
        public const Int16 qfifocntLoc = 521;                                  //G              F16

        //QFIFO�ĵȴ�ָ��ĳ�ʱʱ��
        public const Int16 qwaittimeLoc = 492;                                 //G              S32

        #endregion

        //********************************************************************************************************/
        
        #region Ԥ�����û�����
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
        
        #region �岹�˶��������

        //����������öε����������Ծ���ֵ�������ֵ��ʾ
        public const Int16 pathabsLoc = 205;                                   //A              F16

        //��ǰִ�жε��յ�
        public const Int16 segendLoc = 202;                                    //A              S32

        //��ǰִ�жε���ʼ��
        public const Int16 segstartLoc = 200;                                  //A              S32

        //����������������Ƿ����·���˶��������˶���
        public const Int16 moveinpath1Loc = 204;                               //A              F16

        //����������������Ƿ����·���˶��������˶���
        public const Int16 moveinpath2Loc = 165;                               //A              F16

        //�岹�ռ�1�Ĳ���***********************************************************************************************************/

        //д����㿪ʼִ��·���˶�
        public const Int16 startpath1Loc = 352;                                //G              F16

        //��־�Ƿ�����ִ�в岹
        public const Int16 pathmoving1Loc = 354;                               //G              F16

        //��ǰִ��Բ���εķ���0��˳ʱ�룬���㣺��ʱ��
        public const Int16 arcdir1Loc = 355;                                   //G              F16

        //ָ���ٶȹ滮�Ƿ���ڸöκϳ�·���ĳ��ȣ���ĳ�����ڸöε��ƶ����롣
        //��asseglenΪ0���ٶȹ滮����X��Y��Z����ĺϳ�·�����ȣ���pathvel�Ǻϳ�·�����ٶȡ�
        //��Ȼ����pathaxisnumС��3ʱ����ֻ��X���X��Y��ϳ�·�����ȡ�
        //��asseglen���㣬asseglen����Ϊ1~pathaxisnum��Χ��һ��ֵ����ʾ����segmap_x��segmap_y����ӳ������
        //�ƶ���������ٶȹ滮����1��ʾ����X����ƶ�����滮�ٶȣ����pathvel��ΪX����ٶȡ�
        public const Int16 asseglen1Loc = 361;                                 //G              F16

        //��ǰ·���ٶȣ�ֻ����
        public const Int16 pathvel1Loc = 362;                                          //G              S16.16

        //�����㣺����·���岹Ϊ����ģʽ��ÿ����ĸ��������Ϊ16bit��������PFIFO�������ȫ������·���еĸ������������
        //       public const Int16 contourmod1Loc =                   365     //G              F16

        //·�����ٶ�
        public const Int16 pathacc1Loc = 366;                                  //G              S16.16
        //�������ʣ�����·���˶�������ʵʱ�ı�·���ٶ�
        public const Int16 feedrate1Loc = 368;                                 //G              S16.16
        //����·���˶�������
        public const Int16 pathaxisnum1Loc = 370;                              //G              F16
        //ӳ��ΪX������
        public const Int16 segmap_x1Loc = 371;                                 //G              F16
        //ӳ��ΪY������
        public const Int16 segmap_y1Loc = 372;                                 //G              F16
        //ӳ��ΪZ������
        public const Int16 segmap_z1Loc = 373;                                 //G              F16
        //ӳ��ΪA������
        public const Int16 segmap_a1Loc = 374;                                 //G              F16
        //ӳ��ΪB������
        public const Int16 segmap_b1Loc = 375;                                 //G              F16
        //ӳ��ΪC������
        public const Int16 segmap_c1Loc = 376;                                 //G              F16
        //ӳ��ΪD������
        public const Int16 segmap_d1Loc = 377;                                 //G              F16
        //ӳ��ΪE������
        public const Int16 segmap_e1Loc = 378;                                 //G              F16
        //ӳ��ΪF������
        public const Int16 segmap_f1Loc = 379;                                 //G              F16
        //ӳ��ΪG������
        public const Int16 segmap_g1Loc = 380;                                 //G              F16
        //ӳ��ΪH������
        public const Int16 segmap_h1Loc = 381;                                 //G              F16
        //ӳ��ΪI������
        public const Int16 segmap_i1Loc = 382;                                 //G              F16
        //ӳ��ΪJ������
        public const Int16 segmap_j1Loc = 383;                                 //G              F16
        //ӳ��ΪK������
        public const Int16 segmap_k1Loc = 384;                                 //G              F16
        //ӳ��ΪL������
        public const Int16 segmap_l1Loc = 385;                                 //G              F16
        //ӳ��ΪM������
        public const Int16 segmap_m1Loc = 386;                                 //G              F16
        //��ִ�е�Ŀ���ٶ�
        public const Int16 segtgvel1Loc = 387;                                 //G              S16.16
        //�ν���ʱ���ٶ�
        public const Int16 segendvel1Loc = 389;                                //G              S16.16
        //����ִ�еĶε�ID�ţ�ÿִ��һ�Σ���ID��1
        public const Int16 segID1Loc = 391;                                    //G              U32
        //��ǰִ�жεĳ���
        public const Int16 seglen1Loc = 393;                                   //G              U32
        //��ǰִ��Բ���εİ뾶
        public const Int16 radius1Loc = 395;                                   //G              U32


        //PFIFO1��������ز���

        //�Դ˼Ĵ���д�������PFIFO
        public const Int16 clrPFIFO1Loc = 565;                                //G              F16

        //PFIFO�����ݸ���
        public const Int16 PFIFOcnt1Loc = 565;                                 //G              F16

        //PFIFO1�ȴ�ָ�ʱʱ��
        public const Int16 pwaittime1Loc = 399;                                //G              S32


        //PFIFO2***********************************************************************************************************/

        //д����㿪ʼִ��·���˶�
        public const Int16 startpath2Loc = 405;                                //G              F16
        //��־�Ƿ�����ִ�в岹
        public const Int16 pathmoving2Loc = 407;                               //G              F16
        //��ǰִ��Բ���εķ���0��˳ʱ�룬���㣺��ʱ��
        public const Int16 arcdir2Loc = 408;                                   //G              F16

        //ָ���ٶȹ滮�Ƿ���ڸöκϳ�·���ĳ��ȣ���ĳ�����ڸöε��ƶ����롣
        //��asseglenΪ0���ٶȹ滮����X��Y��Z����ĺϳ�·�����ȣ���pathvel�Ǻϳ�·�����ٶȡ�
        //��Ȼ����pathaxisnumС��3ʱ����ֻ��X���X��Y��ϳ�·�����ȡ�
        //��asseglen���㣬asseglen����Ϊ1~pathaxisnum��Χ��һ��ֵ����ʾ����segmap_x��segmap_y����ӳ������
        //�ƶ���������ٶȹ滮����1��ʾ����X����ƶ�����滮�ٶȣ����pathvel��ΪX����ٶȡ�
        public const Int16 asseglen2Loc = 414;                                 //G              F16

        //��ǰ·���ٶȣ�ֻ����
        public const Int16 pathvel2Loc = 415;                                  //G              S16.16

        //�����㣺����·���岹Ϊ����ģʽ��ÿ����ĸ��������Ϊ16bit��������PFIFO�������ȫ������·���еĸ������������
        //       public const Int16 contourmod2Loc = 418                               //G              F16

        //·�����ٶ�
        public const Int16 pathacc2Loc = 419;                                  //G              S16.16
        //�������ʣ�����·���˶�������ʵʱ�ı�·���ٶ�
        public const Int16 feedrate2Loc = 421;                                 //G              S16.16
        //����·���˶�������
        public const Int16 pathaxisnum2Loc = 423;                              //G              F16
        //ӳ��ΪX������
        public const Int16 segmap_x2Loc = 424;                                 //G              F16
        //ӳ��ΪY������
        public const Int16 segmap_y2Loc = 425;                                 //G              F16
        //ӳ��ΪZ������
        public const Int16 segmap_z2Loc = 426;                                 //G              F16
        //ӳ��ΪA������
        public const Int16 segmap_a2Loc = 427;                                 //G              F16
        //ӳ��ΪB������
        public const Int16 segmap_b2Loc = 428;                                 //G              F16
        //ӳ��ΪC������
        public const Int16 segmap_c2Loc = 429;                                 //G              F16
        //ӳ��ΪD������
        public const Int16 segmap_d2Loc = 430;                                 //G              F16
        //ӳ��ΪE������
        public const Int16 segmap_e2Loc = 431;                                 //G              F16
        //ӳ��ΪF������
        public const Int16 segmap_f2Loc = 432;                                 //G              F16
        //ӳ��ΪG������
        public const Int16 segmap_g2Loc = 433;                                 //G              F16
        //ӳ��ΪH������
        public const Int16 segmap_h2Loc = 434;                                 //G              F16
        //ӳ��ΪI������
        public const Int16 segmap_i2Loc = 435;                                 //G              F16
        //ӳ��ΪJ������
        public const Int16 segmap_j2Loc = 436;                                 //G              F16
        //ӳ��ΪK������
        public const Int16 segmap_k2Loc = 437;                                 //G              F16
        //ӳ��ΪL������
        public const Int16 segmap_l2Loc = 438;                                 //G              F16
        //ӳ��ΪM������
        public const Int16 segmap_m2Loc = 439;                                 //G              F16
        //��ִ�е�Ŀ���ٶ�
        public const Int16 segtgvel2Loc = 440;                                 //G              S16.16
        //�ν���ʱ���ٶ�
        public const Int16 segendvel2Loc = 442;                                //G              S16.16
        //�ε�ID�����ڱ�ʶ����ִ�еڼ���
        public const Int16 segID2Loc = 444;                                    //G              U32


        //PFIFO2��������ز���

        //�Դ˼Ĵ���д�������PFIFO
        public const Int16 clrPFIFO2Loc = 685;                                 //G              F16

        //PFIFO�����ݸ���
        public const Int16 PFIFOcnt2Loc = 685;                                 //G              F16

        //PFIFO2�ȴ�ָ�ʱʱ��
        public const Int16 pwaittime2Loc = 452;                                //G              S32

        #endregion

        //********************************************************************************************************/
        
        #region ���������I/O����ز���


        //���������������ʹ�ܡ�д��FFFFh��ʹ�����������ʹ����������ע�⣺д��FFFFh��������ʹ���ź�Ϊ�͵�ƽ��
        //д��0��Ϊ�ߵ�ƽ�����轫����Ϊ������(���������������������)��������ena��
        //�����������ж�����������������Ƿ�ʹ�ܣ�0�������������������ֹ��FFFFh�����������������ʹ�ܡ�
        public const Int16 enaLoc = 550;                                       //A              F16

        //��I/O���ݼĴ�����ĳI/O��Ϊ����ʱ���������Ƕ�Ӧ�ܽŵ�ʵʱ�ź�ֵ��
        //����Ϊ�����д��0��1��ʹ�ùܽ�����ͻ�ߵ�ƽ������õ�д�뵽�üĴ�����ֵ��
        public const Int16 aioLoc = 562;                                       //A              R16

        //��I/O�Ŀ��ƼĴ���
        public const Int16 aioctrLoc = 680;                                    //A              R16

        //��I/O��Ϊ����ʱ�����źŵ���Ч���ء�
        public const Int16 aiolatLoc = 682;                                    //A              R16

        //��Ӧ��λ��д��1��������I/O������ֵ��
        public const Int16 clraiolatLoc = 682;                                 //A              R16

        //ȫ�ֿ��������gout1
        public const Int16 gout1Loc = 560;                                     //G              R16

        //ȫ�ֿ��������gout2
        public const Int16 gout2Loc = 561;                                     //G              R16

        //ȫ�ֿ��������gout3
        public const Int16 gout3Loc = 555;                                     //G              R16

        //ȫ�ֿ���������gin1
        public const Int16 gin1Loc = 706;                                      //G              R16

        //ȫ�ֿ���������gin2
        public const Int16 gin2Loc = 707;                                      //G              R16

        //����ȫ�ֿ���������gin1����Ч����
        public const Int16 gin1latLoc = 612;                                      //G              R16

        //����ȫ�ֿ���������gin2����Ч����
        public const Int16 gin2latLoc = 613;                                      //G              R16

        //���õ�ֹͣ���ص�״̬��д������ֹͣ���ص���Ч����
        public const Int16 stopinLoc = 563;                                    //G              R16

        //���뿪�����������˲����üĴ���
        public const Int16 swfilterLoc = 548;                                  //G              R16

        //�ŷ��������������
        public const Int16 srstLoc = 551;                                      //A              R16

        #endregion

        //********************************************************************************************************/
        
        #region ��ʱ��Ԫ��صĲ���

        //�����ʱ��д���������ÿ�����1
        public const Int16 delaymsLoc = 704;                                   //G              U32

        //delayms����ʱΪ0��ò���Ϊ0
        public const Int16 delayoutLoc = 704;                                  //G              U32

        //����ʱ��������д����㿪ʼÿ���ڼ�1
        public const Int16 timerLoc = 481;                                     //G              U16

        //�������ڼ�������ÿ�������ڼ�1
        public const Int16 ticksLoc = 502;                                     //G              U32

        #endregion

        //�¼�ָ����ز���************************************************************************
        //�¼�ָ���������������ֹ�����¼�ָ��
        public const Int16 eventsLoc = 489;                                    //G              U16


        
        #region ��ͣ/��ͣ��ز���

        //ĳЩλ������λ����ʹ�������error�Ĵ�����Ӧλ����λ
        public const Int16 emstopLoc = 500;                                    //G              F16

        //��������ͣ
        public const Int16 hpauseLoc = 501;                                    //G              F16

        #endregion

       
        #region ϵͳ���� Read only
        public const Int16 clkdivLoc = 509;                                    //G              U16             ���Ʒ�Ƶ
        public const Int16 fwversionLoc = 511;                                 //G              U16             ��������firmware�İ汾�š�
        public const Int16 sysclkLoc = 628;                                    //G              U32             ϵͳ��׼ʱ�ӡ�
        public const Int16 naxisLoc = 634;                                     //G              U16             ��������֧�ֵ����������
        public const Int16 hwversionLoc = 635;                                 //G              U16             Ӳ���汾������Ӳ���汾��ͬ��

        #endregion

        //�������FIFO�Ͳ����ĵȴ�ָ���
        public const Int16 clearimcLoc = 494;                                  //G              R16

    }

}