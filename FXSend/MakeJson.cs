using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSend
{
    public class MakeJson

    {
        /// <summary>
        /// 智能靜电压监控器设置值
        /// 参数：监控器id
        /// 返回结果：智能静电压监控器设置值json
        /// </summary>
        /// <returns></returns>
        public string ElectrostaticVoltageSettingData(string id)
        {
            string sql = "select IDNumber as Mid, Field8 as CH1, Field3 as  CH1ValLowerLimmit, Field4 as  CH1ValUpperLimmit,Field9 as  CH2 ,Field2 as  CH2ValLowerLimmit, Field6 as  CH2ValUpperLimmit";
            sql += " from InstrumentJKQ where instruName = '智能静电压监控器'";
            sql += "and idnumber = '" + id + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Vals.Append("{");
                    Vals.Append("\"MId\":\"" + row["Mid"] + "\",");
                    Vals.Append("\"CH1\":\"" + row["CH1"] + "\",");
                    Vals.Append("\"CH1ValLowerLimmit\":\"" + row["CH1ValLowerLimmit"] + "\",");
                    Vals.Append("\"CH1ValUpperLimmit\":\"" + row["CH1ValUpperLimmit"] + "\",");
                    Vals.Append("\"CH2\":\"" + row["CH2"] + "\",");
                    Vals.Append("\"CH2ValLowerLimmit\":\"" + row["CH2ValLowerLimmit"] + "\",");
                    Vals.Append("\"CH2ValUpperLimmit\":\"" + row["CH2ValUpperLimmit"] + "\"");
                    Vals.Append("}");
                }
                var json = "{ \"DataType\": \"String\", \"DataLabel\": \"ElectrostaticVoltageSettingData\", \"Datas\":" + Vals + "}";
                return json;
            }
            else
            {
                string strmsg = "Fail,参数设置不正确,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                return "{\"status\":\"" + strmsg + "\"}";

            }
        }
        /// <summary>
        ///智能监控器（带漏电压）设置值
        /// 参数：监控器id
        /// 返回结果：智能监控器（带漏电压）设置值json
        /// </summary>
        /// <returns></returns>
        public string GroundingResistanceSettingData(string id)
        {
            string sql = "select b.IDNumber,a.Status, CH,a.InstrumentID as InstrumentID,AlarmValueLow,AlarmValueHigh  , a.Field7 as 'DValUpperLimmit',a.Field8 as 'LValUpperLimmit'";
            sql += " from ControlData as a ,InstrumentJKQ as b ";
            sql += "where idnumber = '" + id + "'";
            sql += " and a.InstrumentID=b.InstrumentID";
            sql += " and b.field2='1'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        Vals.Append("{");
                        Vals.Append("\"MId\":\"" + dt.Rows[i]["IDNumber"] + "\",");
                        Vals.Append("\"CH" + (i + 1) + "Type\":\"" + dt.Rows[i]["Status"] + "\",");
                        Vals.Append("\"CH" + (i + 1) + "ResistanceValLowerLimmit\":\"" + dt.Rows[i]["AlarmValueLow"] + "\",");//电阻下限
                        Vals.Append("\"CH" + (i + 1) + "ResistanceValUpperLimmit\":\"" + dt.Rows[i]["AlarmValueHigh"] + "\",");//电阻上限
                        Vals.Append("\"LV" + (i + 1) + "ValUpperLimmit\":\"" + dt.Rows[i]["LValUpperLimmit"] + "\",");//直流上限
                        Vals.Append("\"DV" + (i + 1) + "ValUpperLimmit\":\"" + dt.Rows[i]["DValUpperLimmit"] + "\",");//交流上限

                    }
                    else
                    {

                        Vals.Append("\"CH" + (i + 1) + "Type\":\"" + dt.Rows[i]["Status"] + "\",");//通道类型
                        Vals.Append("\"CH" + (i + 1) + "ResistanceValLowerLimmit\":\"" + dt.Rows[i]["AlarmValueLow"] + "\",");//电阻下限
                        Vals.Append("\"CH" + (i + 1) + "ResistanceValUpperLimmit\":\"" + dt.Rows[i]["AlarmValueHigh"] + "\",");//电阻上限
                        Vals.Append("\"LV" + (i + 1) + "ValUpperLimmit\":\"" + dt.Rows[i]["LValUpperLimmit"] + "\",");
                        Vals.Append("\"DV" + (i + 1) + "ValUpperLimmit\":\"" + dt.Rows[i]["DValUpperLimmit"] + "\",");

                    }

                }
                Vals = Vals.Remove(Vals.Length - 1, 1);
                Vals.Append("}");
                var json1 = "{ \"DataType\": \"String\", \"DataLabel\": \"GroundingResistanceSettingData\", \"Datas\":" + Vals + "}";
                return json1;
            }
            else
            {
                string strmsg = "Fail,参数设置不正确,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                return "{\"status\":\"" + strmsg + "\"}";

            }






            //var json = "{\"Datatype\":\"String\",\"DataLabel\":\" ElectrostaticVoltageSettingData \",\"Datas\":{\"MId\":\"\",\"CH1Type\":\"ws\",\"CH2Type\":\"ws\",\"CH3Type\":\"mat\",\"CH4Type\":\"gnd\",\"CH1Status\":\"OK\",\"CH2Status\":\"OK\",\"CH3Status\":\"OK\",\"CH4Status\":\"OK\",\"CH1Resistance\":\"\",\"CH2Resistance\":\"\",\"CH3Resistance\":\"\",\"CH4Resistance\":\"\",\"LV1Status\":\"OK\",\"LV2Status\":\"OK\",\"LV3Status\":\"OK\",\"LV4Status\":\"OK\",\"LV1ValLowerLimmit\":\"\",\"LV1ValUpperLimmit\":\"\",\"LV2ValLowerLimmit\":\"\",\"LV2ValUpperLimmit\":\"\",\"LV3ValLowerLimmit\":\"\",\"LV3ValUpperLimmit\":\"\",\"LV4ValLowerLimmit\":\"\",\"LV4ValUpperLimmit\":\"\",\"DV1Status\":\"OK\",\"DV2Status\":\"OK\",\"DV3Status\":\"OK\",\"DV4Status\":\"OK\",\"DV1ValLowerLimmit\":\"\",\"DV1ValUpperLimmit\":\"\",\"DV2ValLowerLimmit\":\"\",\"DV2ValUpperLimmit\":\"\",\"DV3ValLowerLimmit\":\"\",\"DV3ValUpperLimmit\":\"\",\"DV4ValLowerLimmit\":\"\",\"DV4ValUpperLimmit\":\"\"}}";

        }
        /// <summary>
        /// 智能静电压监控器扫描数据
        /// 参数：开始时间，结束时间
        /// 返回结果：智能静电压监控器扫描数据json
        /// </summary>
        /// <returns></returns> json pass
        public string IntellectElectrostaticVoltageMonitorData(DateTime datetimeStart, DateTime dateTimeEnd)
        {
            string sql = "SELECT Id,MId,CH1,CH1Val,CH2,CH2Val,Result,AddTime,CreateTime";
            sql += " from MonitorDataZhiNengJDY ";
            sql += "where AddTime between  '" + datetimeStart + "' and '" + dateTimeEnd + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                Vals.Append("[");
                foreach (DataRow row in dt.Rows)
                {
                    Vals.Append("{");
                    Vals.Append("\"id\":\"" + row["id"] + "\",");
                    Vals.Append("\"Mid\":\"" + row["Mid"] + "\",");
                    Vals.Append("\"CH1\":\"" + row["CH1"] + "\",");
                    Vals.Append("\"CH1Val\":\"" + row["CH1Val"] + "\",");
                    Vals.Append("\"CH2\":\"" + row["CH2"] + "\",");
                    Vals.Append("\"CH2Val\":\"" + row["CH2Val"] + "\",");
                    Vals.Append("\"Result\":\"" + row["Result"] + "\",");
                    Vals.Append("\"AddTime\":\"" + Convert.ToDateTime(row["AddTime"]).ToString("yyyy-MM-dd HH-mm-ss") + "\",");
                    Vals.Append("\"CreateTime\":\"" + Convert.ToDateTime(row["CreateTime"]).ToString("yyyy-MM-dd HH-mm-ss") + "\"");
                    Vals.Append("},");
                }
                Vals = Vals.Remove(Vals.Length - 1, 1);
                Vals.Append("]");

                var json = "{ \"DataType\": \"String\", \"DataLabel\": \"IntellectElectrostaticVoltageMonitorData\", \"Datas\":" + Vals + "}";
                return json;
            }
            else
            {
                string strmsg = "Fail,参数设置不正确,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                return "{\"status\":\"" + strmsg + "\"}";

            }




        }
        /// <summary>
        ///智能监控器扫描数据
        /// 参数：开始时间，结束时间
        /// 返回结果：智能监控器扫描数据json
        /// </summary>
        /// <returns></returns>
        public string IntellectMonitorData(DateTime datetimeStart, DateTime dateTimeEnd)//估计用不上

        {
            string sql = "SELECT  Id,MId,AddTime,CH1Type,CH2Type,CH3Type,CH4Type,CH1Status,CH2Status,CH3Status,CH4Status ,IsValue,CreateTime,IsSleep";
            sql += "   FROM MonitorDataZhiNeng";
            sql += " where AddTime between  '" + datetimeStart + "' and '" + dateTimeEnd + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                Vals.Append("[");
                foreach (DataRow row in dt.Rows)
                {
                    Vals.Append("{");
                    Vals.Append("\"Mid\":\"" + row["Mid"] + "\",");
                    Vals.Append("\"AddTime\":\"" + row["AddTime"] + "\",");
                    Vals.Append("\"CH1Type\":\"" + row["CH1Type"] + "\",");
                    Vals.Append("\"CH2Type\":\"" + row["CH2Type"] + "\",");
                    Vals.Append("\"CH3Type\":\"" + row["CH3Type"] + "\",");
                    Vals.Append("\"CH4Type\":\"" + row["CH4Type"] + "\",");
                    Vals.Append("\"CH1Status\":\"" + row["CH1Status"] + "\",");
                    Vals.Append("\"CH2Status\":\"" + row["CH2Status"] + "\",");
                    Vals.Append("\"CH3Status\":\"" + row["CH3Status"] + "\",");
                    Vals.Append("\"CH4Status\":\"" + row["CH4Status"] + "\",");
                    Vals.Append("\"IsValue\":\"" + row["IsValue"] + "\",");
                    Vals.Append("\"CreateTime\":\"" + row["CreateTime"] + "\",");
                    Vals.Append("\"IsSleep\":\"" + row["IsSleep"] + "\",");
                    Vals.Append("\"AddTime\":\"" + row["AddTime"] + "\"");
                    Vals.Append("},");
                }
                Vals = Vals.Remove(Vals.Length - 1, 1);
                Vals.Append("]");
                var json = "{ \"DataType\": \"String\", \"DataLabel\": \"IntellectMonitorData\", \"Datas\":" + Vals + "}";
                return json;
            }
            else
            {
                return "{\"status\":Fail,参数设置不正确}";

            }



        }
        /// <summary>
        /// 智能监控器（带漏电压）扫描数据
        /// 参数：开始时间，结束时间
        /// 返回结果：智能监控器（带漏电压）扫描数据json
        /// </summary>
        /// <returns></returns>
        public string IntellectMonitorDivulgeVoltageData(DateTime datetimeStart, DateTime dateTimeEnd)
        {
            string sql = "SELECT MId,CH1Type,CH2Type,CH3Type,CH4Type,CH1Status,CH2Status ,CH3Status,CH4Status,IsValue,LV1Status,LV2Status ,LV3Status,LV4Status ,LV1Val,LV2Val,LV3Val,LV4Val,DV1Status,DV2Status,DV3Status,DV4Status,DV1Val,DV2Val ,DV3Val,DV4Val,AddTime,CreateTime";
            sql += " from MonitorDataZhiNengLVoltAge";
            sql += " where CreateTime between  '" + datetimeStart + "' and '" + dateTimeEnd + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                Vals.Append("[");
                foreach (DataRow row in dt.Rows)
                {

                    Vals.Append("{");
                    Vals.Append("\"Mid\":\"" + row["Mid"] + "\",");
                    Vals.Append("\"CH1Type\":\"" + row["CH1Type"] + "\",");
                    Vals.Append("\"CH2Type\":\"" + row["CH2Type"] + "\",");
                    Vals.Append("\"CH3Type\":\"" + row["CH3Type"] + "\",");
                    Vals.Append("\"CH4Type\":\"" + row["CH4Type"] + "\",");
                    Vals.Append("\"IsValue\":\"" + row["IsValue"] + "\",");
                    Vals.Append("\"CreateTime\":\"" + row["CreateTime"] + "\",");
                    Vals.Append("\"CH1Status\":\"" + row["CH1Status"] + "\",");
                    Vals.Append("\"CH2Status\":\"" + row["CH2Status"] + "\",");
                    Vals.Append("\"CH3Status\":\"" + row["CH3Status"] + "\",");
                    Vals.Append("\"CH4Status\":\"" + row["CH4Status"] + "\",");
                    Vals.Append("\"LV1Status\":\"" + row["LV1Status"] + "\",");
                    Vals.Append("\"LV2Status\":\"" + row["LV2Status"] + "\",");
                    Vals.Append("\"LV3Status\":\"" + row["LV3Status"] + "\",");
                    Vals.Append("\"LV4Status\":\"" + row["LV4Status"] + "\",");
                    Vals.Append("\"LV1Val\":\"" + row["LV1Val"] + "\",");
                    Vals.Append("\"LV2Val\":\"" + row["LV2Val"] + "\",");
                    Vals.Append("\"LV3Val\":\"" + row["LV3Val"] + "\",");
                    Vals.Append("\"LV4Val\":\"" + row["LV4Val"] + "\",");
                    Vals.Append("\"DV1Status\":\"" + row["DV1Status"] + "\",");
                    Vals.Append("\"DV2Status\":\"" + row["DV2Status"] + "\",");
                    Vals.Append("\"DV3Status\":\"" + row["DV3Status"] + "\",");
                    Vals.Append("\"DV4Status\":\"" + row["DV4Status"] + "\",");
                    Vals.Append("\"DV1Val\":\"" + row["DV1Val"] + "\",");
                    Vals.Append("\"DV2Val\":\"" + row["DV2Val"] + "\",");
                    Vals.Append("\"DV3Val\":\"" + row["DV3Val"] + "\",");
                    Vals.Append("\"DV4Val\":\"" + row["DV4Val"] + "\",");


                    Vals.Append("\"AddTime\":\"" + row["AddTime"] + "\"");
                    Vals.Append("},");
                }
                Vals = Vals.Remove(Vals.Length - 1, 1);
                Vals.Append("]");
                var json = "{ \"DataType\": \"String\", \"DataLabel\": \"IntellectMonitorDivulgeVoltageData\", \"Datas\":" + Vals + "}";
                return json;
            }
            else
            {
                string strmsg = "Fail,参数设置不正确,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                return "{\"status\":\"" + strmsg + "\"}";

            }



        }
        /// <summary>
        /// 监控器属性数据
        ///参数：监控器id
        ///返回结果：监控器属性json
        /// </summary>
        /// <returns></returns>
        public string MonitorPropertyData(string id)
        {
            string sql = "select Instrumentid,IDNumber,InstruName,DeptID,AlarmValue,Void_flag,Remark,CreateUser,CreateTime,DeptName";
            sql += " from InstrumentJKQ ";
            sql += "where idnumber = '" + id + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Vals.Append("{");
                    Vals.Append("\"Instrumentid\":\"" + row["Instrumentid"] + "\",");
                    Vals.Append("\"IDNumber\":\"" + row["IDNumber"] + "\",");
                    Vals.Append("\"InstruName\":\"" + row["InstruName"] + "\",");
                    Vals.Append("\"DeptID\":\"" + row["DeptID"] + "\",");
                    Vals.Append("\"AlarmValue\":\"" + row["AlarmValue"] + "\",");
                    Vals.Append("\"Void_flag\":\"" + row["Void_flag"] + "\",");
                    Vals.Append("\"Remark\":\"" + row["Remark"] + "\",");
                    Vals.Append("\"CreateUser\":\"" + row["CreateUser"] + "\",");
                    Vals.Append("\"CreateTime\":\"" + row["CreateTime"] + "\",");
                    Vals.Append("\"DeptName\":\"" + row["DeptName"] + "\"");
                    Vals.Append("}");
                }
                var json = "{ \"DataType\": \"String\", \"DataLabel\": \"MonitorPropertyData\", \"Datas\":" + Vals + "}";
                return json;
            }
            else
            {
                string strmsg = "Fail,参数设置不正确,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                return "{\"status\":\"" + strmsg + "\"}";

            }
        }
        /// <summary>
        /// 智能监控器属性数据
        /// 参数：监控器id
        /// 返回结果：智能监控器各通道设置值json
        /// </summary>
        /// <returns></returns>
        public string ControlData(string id)
        {
            string sql = "select  ControlDataID,CH,a.InstrumentID as InstrumentID,AlarmValueLow,AlarmValueHIgh ,a.Void_flag as Void_flag ,a.Remark as Remark";
            sql += " from ControlData as a ,InstrumentJKQ as b ";
            sql += "where idnumber = '" + id + "'";
            sql += " and a.InstrumentID=b.InstrumentID";
            DataSet ds = DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            StringBuilder Vals = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                Vals.Append("[");
                foreach (DataRow row in dt.Rows)
                {
                    Vals.Append("{");
                    Vals.Append("\"ControlDataID\":\"" + row["ControlDataID"] + "\",");
                    Vals.Append("\"CH\":\"" + row["CH"] + "\",");
                    Vals.Append("\"InstrumentID\":\"" + row["InstrumentID"] + "\",");
                    Vals.Append("\"AlarmValueLow\":\"" + row["AlarmValueLow"] + "\",");
                    Vals.Append("\"AlarmValueHIgh\":\"" + row["AlarmValueHigh"] + "\",");
                    Vals.Append("\"Void_flag\":\"" + row["Void_flag"] + "\",");
                    Vals.Append("\"Remark\":\"" + row["Remark"] + "\"");
                    Vals.Append("},");
                }
                Vals = Vals.Remove(Vals.Length - 1, 1);
                Vals.Append("]");
                var json = "{ \"DataType\": \"String\", \"DataLabel\": \"ControlData\", \"Datas\":" + Vals + "}";
                return json;
            }
            else
            {
                string strmsg = "Fail,参数设置不正确,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                return "{\"status\":\"" + strmsg + "\"}";

            }
        }


    }

}
