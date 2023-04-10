using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FXCDI3DCAPI;
using Microsoft.Win32;
using MyTools;
using System.Configuration;
using System.Reflection;
using FXSend;
using System.Web.Script.Serialization;

namespace FXPro
{
    public partial class FXSend : Form
    {
        public FXSend()
        {
            InitializeComponent();
            Intent intent = new Intent("1", "2", "3");
            intent.ReceiveDataCallBackFunc += new Action<string, string>(Send);
        }
        System.Timers.Timer SentMsg;  // 定时推送数据
        System.Timers.Timer SentScanMsg;  // 定时推送扫描数据
        System.Timers.Timer CloseT;  // 定时推送数据


        Universal universal = new Universal();
        string remsg = "";
        string ScanDataIn = "";
        string SiteDataIn = "";

        private void FXSend_Load(object sender, EventArgs e)
        {
            //获取当前应用程序路径
            string path = Application.ExecutablePath;
            //true开启启动应用程序，false关闭开机启动应用程序
            SelfRunning(true, "FXSend.exe", path);
            DataBaseName.Text = ConfigurationManager.AppSettings["DataBaseName"].ToString();
            IP.Text = ConfigurationManager.AppSettings["IP"].ToString();
            UserName.Text = ConfigurationManager.AppSettings["UserName"].ToString();
            Password.Text = ConfigurationManager.AppSettings["Password"].ToString();
            IsSuccessConnect.Text = "连接中....";
            GetConnect();
            ContorlEdit(false);
            new Intent("", "", "");
            SentMsg = new System.Timers.Timer(10 * 1000);  //
           // SentMsg = new System.Timers.Timer(1000);  //定时周期1h
            SentMsg.Elapsed += timerSendMsg_Elapsed;  //到10秒了做的事件
            SentMsg.AutoReset = true;  //是否不断重复定时器操作
            SentMsg.Enabled = true;
            SentScanMsg = new System.Timers.Timer(10 * 1000);  //扫描数据：定时周期,5min
           // SentScanMsg = new System.Timers.Timer(1000);  //定时周期,5min
            SentScanMsg.Elapsed += timerSendScanMsg_Elapsed;  //推送扫描数据
            SentScanMsg.AutoReset = true;  //是否不断重复定时器操作
            SentScanMsg.Enabled = true;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            SaveConfig("appSettings", DataBaseName.Text, "DataBaseName");
            SaveConfig("appSettings", IP.Text, "IP");
            SaveConfig("appSettings", UserName.Text, "UserName");
            SaveConfig("appSettings", Password.Text, "Password");
            BtnSubmit.Text = "提交成功";        
            CloseT = new System.Timers.Timer(500);  //定时周期,5min
            CloseT.Elapsed += timerCloseT_Elapsed;  //推送扫描数据
           // CloseT.AutoReset = true;  //是否不断重复定时器操作
            CloseT.Enabled = true;   
        }

        static void Send(string ip,string description)
        {
            string id;
            string idmsg= description.Split('|')[1];
            if (ip == "模拟ip")//测试
            {
                id = description;
            }
            else
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                id = jss.Deserialize<Description>(idmsg).ID;
            }
            FileHelper.AddLog("客户端:"+ip+"下发了指令，即将推送监控器:"+ id + "的数据");
            string json="";
            MakeJson test = new MakeJson();          
            string sql = "select InstruName from InstrumentJKQ where idNumber = '" + id + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["InstruName"].ToString()=="智能静电压监控器")
                {
                     json = test.ElectrostaticVoltageSettingData(id);
                }
                else
                    if(dt.Rows[0]["InstruName"].ToString() == "智能监控器")
                {
                    json = test.GroundingResistanceSettingData(id);
                }
                json = "GetData|" + id + json;
            }
            else
            {
                json = "GetData| Fail+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            }
            
            Intent.SendDataToFoxconnClinet(json);
            FileHelper.AddLog("推送完毕");
        }
    
        private void SaveConfig(string sectionName, string value, string strKey)
        {
            //读取程序集的配置文件
            string assemblyConfigFile = Assembly.GetEntryAssembly().Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(assemblyConfigFile);
            //获取appSettings节点
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection(sectionName);
            //删除name，然后添加新值
            appSettings.Settings.Remove(strKey);
            appSettings.Settings.Add(strKey, value);
            //保存配置文件
            config.Save();
        }
        private string[] GetElectrostaticVoltageId(string type)
        {
            string sql;
            if (type=="")
            {
                sql = "select idnumber from InstrumentJKQ";

            }
            else 
                if(type == "智能监控器")
            {
                sql = "select idnumber from InstrumentJKQ where instruName = '" + type + "' and field2= '1'";//1为带漏电压的智能监控器

            }
            else
            {
                sql = "select idnumber from InstrumentJKQ where instruName = '" + type + "'";
            }        
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            string[] Ids = new string[dt.Rows.Count];
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Ids[i] = dt.Rows[i]["idnumber"].ToString();
                }
            }
            return Ids;
        }
        void timerCloseT_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
         
            Environment.Exit(0);

        }
        void timerSendScanMsg_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ScanDataIn = ConfigurationManager.AppSettings["ScanData"].ToString();
            if (ScanDataIn == "")
            {
                SiteDataIn = "5";
            }
            FileHelper.AddLog("开始推送扫描数据");
            SentScanMsg.Interval = Convert.ToDouble(ScanDataIn) * 60 * 1000;//5min
            MakeJson test = new MakeJson();
            DateTime dateStart = DateTime.Now.AddSeconds(-300);
            DateTime dateEnd = DateTime.Now;
            
            //智能静电压监控器扫描数据
            remsg = universal.UniversalDataCollectionFunc(test.IntellectElectrostaticVoltageMonitorData(dateStart, dateEnd));
            FileHelper.AddLog("智能静电压监控器扫描数据-返回结果：" + remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            //智能监控器（带漏漏电压电流）扫描数据
            remsg = universal.UniversalDataCollectionFunc(test.IntellectMonitorDivulgeVoltageData(dateStart, dateEnd));
            FileHelper.AddLog("智能监控器（带漏漏电压电流）扫描数据-返回结果：" + remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");

            //监控器扫描数据
            remsg =  universal.UniversalDataCollectionFunc(test.IntellectMonitorData(dateStart, dateEnd));
            FileHelper.AddLog("监控器扫描数据-返回结果：" + remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            FileHelper.AddLog("推送监控器扫描数据一轮结束");

        }
        void timerSendMsg_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SiteDataIn = ConfigurationManager.AppSettings["SiteData"].ToString();
            if (SiteDataIn=="")
            {
                SiteDataIn = "24";

            }
            FileHelper.AddLog("开始推送监控器设置值");
            SentMsg.Interval = Convert.ToDouble(SiteDataIn) * 60 * 60 * 1000;
            MakeJson test = new MakeJson();
            string[] AllIds = GetElectrostaticVoltageId("");//全部监控器
            string[] ElectrostaticVoltageIds = GetElectrostaticVoltageId("智能静电压监控器");
            string[] GroundingResistanceIds = GetElectrostaticVoltageId("智能监控器");
          
            // 智能靜电压监控器设置值
            foreach (string ElectrostaticVoltageId in ElectrostaticVoltageIds)
            {
                 remsg = universal.UniversalDataCollectionFunc(test.ElectrostaticVoltageSettingData(ElectrostaticVoltageId));
                FileHelper.AddLog(ElectrostaticVoltageId+"(智能靜电压监控器设置值返回值)" +remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            }
            // 智能监控器（带漏漏电压电流）设置值、智能监控器上下限设置值
            foreach (string GroundingResistanceId in GroundingResistanceIds)
            {
                //智能监控器（带漏漏电压电流）设置值
                remsg = universal.UniversalDataCollectionFunc(test.GroundingResistanceSettingData(GroundingResistanceId));
                FileHelper.AddLog(GroundingResistanceId+ "智能监控器（带漏漏电压电流）设置值返回值:" + remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");


                // 智能监控器上下限设置值
                remsg = universal.UniversalDataCollectionFunc(test.ControlData(GroundingResistanceId));
                FileHelper.AddLog(GroundingResistanceId+ "(智能监控器上下限设置值)返回值:" + remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");

            }
            // 监控器属性数据
            foreach (string AllId in AllIds)
            {
                remsg= universal.UniversalDataCollectionFunc(test.MonitorPropertyData(AllId));
                FileHelper.AddLog(AllId+ "(监控器属性数据)返值" + remsg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            }
            FileHelper.AddLog("推送监控器设置值一轮结束");
        }
        void GetConnect()
        {

            bool connectionState = false;

            connectionState = DbHelperSQL.IsConnectionSql();

            if (connectionState)
            {
                IsSuccessConnect.Text = "研成数据库连接成功";
                IsSuccessConnect.ForeColor = Color.Green;
                FileHelper.AddLog("数据库连接成功");

            }
            else
            {
                IsSuccessConnect.Text = "研成数据库连接失败";
                IsSuccessConnect.ForeColor = Color.Red;
                FileHelper.AddLog("研成数据库连接失败");

            }

        }

        /// <summary>
        /// 写入或删除注册表键值对,即设为开机启动或开机不启动
        /// </summary>
        /// <param name="isStart">是否开机启动</param>
        /// <param name="exeName">应用程序名</param>
        /// <param name="path">应用程序路径带程序名</param>
        /// <returns></returns>
        private static bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                //若开机自启动则添加键值对
                if (isStart)
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return false;
                //throw;
            }

            return true;
        }

        private void ClickSend_Click(object sender, EventArgs e)
        {
            Send("模拟ip", TestId.Text);
           // Send("ip", "GetData|{\"ID\":\"100\" }");

        }
        private void Modify_Click(object sender, EventArgs e)
        {
            if (Modify.Text == "启用修改框")
            {
                ContorlEdit(true);
                Modify.Text = "禁用修改框";
            }
            else
            {
                ContorlEdit(false);
                Modify.Text = "启用修改框";
            }
        }
        private void ContorlEdit(bool edit)
        {
            if (edit)
            {
                DataBaseName.Enabled = true;
                IP.Enabled = true;
                UserName.Enabled = true;
                Password.Enabled = true;
                BtnSubmit.Enabled = true;


            }
            else
            {
                DataBaseName.Enabled = false;
                IP.Enabled = false;
                UserName.Enabled = false;
                Password.Enabled = false;
                BtnSubmit.Enabled = false;

            }

        }
    }
}
