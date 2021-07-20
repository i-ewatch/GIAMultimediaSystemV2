using Dapper;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.EF_Modules;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Modules;
using GIAMultimediaSystemV2.Protocols.ElectricMeter;
using GIAMultimediaSystemV2.Protocols.Senser;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Methods
{
    public class SqlMethod
    {
        /// <summary>
        /// 目前執行狀態
        /// </summary>
        public Form1 Form1 { get; set; }
        /// <summary>
        /// 資料庫類型
        /// </summary>
        private SQLEnumType SQLEnumType { get; set; }
        /// <summary>
        /// 電表相位角
        /// <para> 0 = R </para>
        /// <para> 1 = S </para>
        /// <para> 2 = T </para>
        /// </summary>
        private PhaseAngleEnumType PhaseAngleEnumType { get; set; }
        /// <summary>
        /// server資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder Serverscsb { get; set; }
        /// <summary>
        /// sql資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder scsb;
        /// <summary>
        /// MariaDB資料庫連結資訊
        /// </summary>
        public MySqlConnectionStringBuilder myscbs;
        /// <summary>
        /// 資料庫JSON
        /// </summary>
        public SqlDBSetting setting { get; set; }

        #region 資料庫連結
        /// <summary>
        /// EF資料庫連結
        /// </summary>
        /// <param name="DataBaseType">資料庫類型</param>
        public void SQLConnect()
        {
            SQLEnumType = (SQLEnumType)setting.SQLEnumsType;
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        Serverscsb = new SqlConnectionStringBuilder()
                        {
                            DataSource = setting.DataSource,
                            InitialCatalog = "master",
                            UserID = setting.UserID,
                            Password = setting.Password,
                        };
                        scsb = new SqlConnectionStringBuilder()
                        {
                            DataSource = setting.DataSource,
                            InitialCatalog = setting.InitialCatalog,
                            UserID = setting.UserID,
                            Password = setting.Password,
                        };
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        Serverscsb = new SqlConnectionStringBuilder()
                        {
                            DataSource = setting.DataSource,
                            InitialCatalog = "mysql",
                            UserID = setting.UserID,
                            Password = setting.Password,
                        };
                        myscbs = new MySqlConnectionStringBuilder()
                        {
                            Database = setting.InitialCatalog,
                            Server = setting.DataSource,
                            UserID = setting.UserID,
                            Password = setting.Password,
                            CharacterSet = "utf8"
                        };
                    }
                    break;
            }

        }
        #endregion

        #region  檢查資料庫是否存在
        /// <summary>
        /// 檢查資料庫是否存在
        /// </summary>
        /// <returns></returns>
        public bool Check_Datebase()
        {
            bool ExistFlag = false;
            string sql = string.Empty;
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        try
                        {
                            using (var conn = new SqlConnection(Serverscsb.ConnectionString))
                            {
                                sql = $"SELECT * FROM dbo.sysdatabases WHERE name = '{setting.InitialCatalog}'";
                                var Exist = conn.Query(sql).ToList();
                                if (Exist.Count > 0) { ExistFlag = true; }
                                else { ExistFlag = false; }
                            }
                        }
                        catch (Exception ex) { Log.Error(ex, "無 MSSQL Server"); }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        try
                        {
                            using (var conn = new MySqlConnection(Serverscsb.ConnectionString))
                            {
                                sql = $"SHOW DATABASES LIKE '{setting.InitialCatalog}'";
                                var Exist = conn.Query(sql).ToList();
                                if (Exist.Count > 0) { ExistFlag = true; }
                                else { ExistFlag = false; }
                            }
                        }
                        catch (Exception ex) { Log.Error(ex, "無 MariaDB Server"); }
                    }
                    break;
            }
            return ExistFlag;
        }
        #endregion

        #region 電表基本資訊更新 
        /// <summary>
        /// 電表基本資訊更新 
        /// </summary>
        /// <param name="electricIDs"></param>
        public void Insert_ElectricConfig(List<GateWay> gateWays)
        {
            string Checksql = string.Empty;
            string Updatasql = string.Empty;
            string Insertsql = string.Empty;
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            foreach (var gateitem in gateWays)
                            {
                                foreach (var electricitem in gateitem.GateWayElectricIDs)
                                {
                                    Checksql = $"SELECT * FROM ElectricConfig WHERE GatewayIndex = {gateitem.GatewayIndex} AND DeviceIndex = {electricitem.DeviceIndex} ";
                                    var Exist = conn.Query<ElectricConfig>(Checksql).ToList();
                                    if (Exist.Count > 0)
                                    {
                                        Updatasql = $"UPDATE ElectricConfig SET " +
                                                    $"TotalMeterFlag = {electricitem.TotalMeterFlag}, " +
                                                    $"DeviceID = {electricitem.DeviceID}, " +
                                                    $"ElectricEnumType={electricitem.ElectricEnumType}, " +
                                                    $"LoopEnumType={electricitem.LoopEnumType}, " +
                                                    $"PhaseEnumType = {electricitem.PhaseEnumType}, " +
                                                    $"PhaseAngleEnumType = {electricitem.PhaseAngleEnumType}, " +
                                                    $"DeviceName = '{electricitem.DeviceName}'" +
                                                    $" WHERE GatewayIndex ={gateitem.GatewayIndex},DeviceIndex = {electricitem.DeviceIndex}";
                                        conn.Execute(Updatasql);
                                    }
                                    else
                                    {
                                        Insertsql = $"INSERT INTO {setting.InitialCatalog}.ElectricConfig (TotalMeterFlag,GatewayIndex,DeviceIndex,DeviceID,ElectricEnumType,LoopEnumType,PhaseEnumType,PhaseAngleEnumType,DeviceName)VALUES" +
                                                    $"({electricitem.TotalMeterFlag},{gateitem.GatewayIndex},{electricitem.DeviceIndex},{electricitem.DeviceID},{electricitem.ElectricEnumType},{electricitem.LoopEnumType},{electricitem.PhaseEnumType},{electricitem.PhaseAngleEnumType},{electricitem.DeviceName})";
                                        conn.Execute(Insertsql);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            foreach (var gateitem in gateWays)
                            {
                                foreach (var electricitem in gateitem.GateWayElectricIDs)
                                {
                                    Checksql = $"SELECT * FROM ElectricConfig WHERE GatewayIndex = {gateitem.GatewayIndex} AND DeviceIndex = {electricitem.DeviceIndex} ";
                                    var Exist = conn.Query<ElectricConfig>(Checksql).ToList();
                                    if (Exist.Count > 0)
                                    {
                                        Updatasql = $"UPDATE ElectricConfig SET " +
                                                    $"TotalMeterFlag = {electricitem.TotalMeterFlag}, " +
                                                    $"DeviceID = {electricitem.DeviceID}, " +
                                                    $"ElectricEnumType={electricitem.ElectricEnumType}, " +
                                                    $"LoopEnumType={electricitem.LoopEnumType}, " +
                                                    $"PhaseEnumType = {electricitem.PhaseEnumType}, " +
                                                    $"PhaseAngleEnumType = {electricitem.PhaseAngleEnumType}, " +
                                                    $"DeviceName = N'{electricitem.DeviceName}'" +
                                                    $" WHERE GatewayIndex ={gateitem.GatewayIndex} AND DeviceIndex = {electricitem.DeviceIndex}";
                                        conn.Execute(Updatasql);
                                    }
                                    else
                                    {
                                        Insertsql = $"INSERT INTO {setting.InitialCatalog}.ElectricConfig (TotalMeterFlag,GatewayIndex,DeviceIndex,DeviceID,ElectricEnumType,LoopEnumType,PhaseEnumType,PhaseAngleEnumType,DeviceName)VALUES" +
                                                    $"({electricitem.TotalMeterFlag},{gateitem.GatewayIndex},{electricitem.DeviceIndex},{electricitem.DeviceID},{electricitem.ElectricEnumType},{electricitem.LoopEnumType},{electricitem.PhaseEnumType},{electricitem.PhaseAngleEnumType},N'{electricitem.DeviceName}')";
                                        conn.Execute(Insertsql);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #region 感測器基本資訊更新 
        /// <summary>
        /// 感測器基本資訊更新 
        /// </summary>
        /// <param name="electricIDs"></param>
        public void Insert_SenserConfig(List<GateWay> gateWays)
        {
            string Checksql = string.Empty;
            string Updatasql = string.Empty;
            string Insertsql = string.Empty;
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            foreach (var gateitem in gateWays)
                            {
                                foreach (var senseritem in gateitem.GateWaySenserIDs)
                                {
                                    Checksql = $"SELECT * FROM SenserConfig WHERE GatewayIndex = {gateitem.GatewayIndex} AND DeviceIndex = {senseritem.DeviceIndex} ";
                                    var Exist = conn.Query<ElectricConfig>(Checksql).ToList();
                                    if (Exist.Count > 0)
                                    {
                                        Updatasql = $"UPDATE ElectricConfig SET " +
                                                    $"DeviceID = {senseritem.DeviceID}, " +
                                                    $"SenserEnumType = {senseritem.SenserEnumType}, " +
                                                    $"DeviceName = '{senseritem.DeviceName}'" +
                                                    $" WHERE GatewayIndex ={gateitem.GatewayIndex},DeviceIndex = {senseritem.DeviceIndex}";
                                        conn.Execute(Updatasql);
                                    }
                                    else
                                    {
                                        Insertsql = $"INSERT INTO {setting.InitialCatalog}.SenserConfig (GatewayIndex,DeviceIndex,DeviceID,SenserEnumType,DeviceName)VALUES" +
                                                    $"({gateitem.GatewayIndex},{senseritem.DeviceIndex},{senseritem.DeviceID},{senseritem.SenserEnumType},'{senseritem.DeviceName}')";
                                        conn.Execute(Insertsql);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            foreach (var gateitem in gateWays)
                            {
                                foreach (var senseritem in gateitem.GateWaySenserIDs)
                                {
                                    Checksql = $"SELECT * FROM SenserConfig WHERE GatewayIndex = {gateitem.GatewayIndex} AND DeviceIndex = {senseritem.DeviceIndex} ";
                                    var Exist = conn.Query<ElectricConfig>(Checksql).ToList();
                                    if (Exist.Count > 0)
                                    {
                                        Updatasql = $"UPDATE SenserConfig SET " +
                                                    $"DeviceID = {senseritem.DeviceID}, " +
                                                    $"SenserEnumType = {senseritem.SenserEnumType}, " +
                                                    $"DeviceName = N'{senseritem.DeviceName}'" +
                                                    $" WHERE GatewayIndex ={gateitem.GatewayIndex} AND DeviceIndex = {senseritem.DeviceIndex}";
                                        conn.Execute(Updatasql);
                                    }
                                    else
                                    {
                                        Insertsql = $"INSERT INTO {setting.InitialCatalog}.SenserConfig (GatewayIndex,DeviceIndex,DeviceID,SenserEnumType,DeviceName)VALUES" +
                                                    $"({gateitem.GatewayIndex},{senseritem.DeviceIndex},{senseritem.DeviceID},{senseritem.SenserEnumType},N'{senseritem.DeviceName}')";
                                        conn.Execute(Insertsql);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #region 更新三相電表 ForWeb、Log與預存程序
        /// <summary>
        /// 更新三相電表 ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_ThreePhaseElectricMeter(ElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            string Proceduresql1 = string.Empty;
            string Checksql_Log = string.Empty;
            Checksql = $"SELECT * FROM ThreePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM ThreePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            UpdataForwebsql = $"UPDATE ThreePhaseElectricMeter_ForWeb SET " +
                              $"ttime= '{ttime}'," +
                              $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                              $"rv = {data.Rv}," +
                              $"sv={data.Sv}," +
                              $"tv={data.Tv}," +
                              $"rsv = {data.RSv}," +
                              $"stv = {data.STv}," +
                              $"trv = {data.TRv}," +
                              $"ra={data.RA}," +
                              $"sa={data.SA}," +
                              $"ta={data.TA}," +
                              $"kw = {data.kW}," +
                              $"kwh = {data.kWh}," +
                              $"kvar={data.kVAR}," +
                              $"kvarh={data.kVARh}," +
                              $"kva={data.kVA}," +
                              $"kvah={data.kVAh}," +
                              $"pfe = {data.PF}," +
                              $"hz={data.HZ} " +
                              $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} ";
            InsertForweb = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                        $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.Sv},{data.Tv},{data.RSv},{data.STv},{data.TRv},{data.RA},{data.SA},{data.TA},{data.kW},{data.kWh},{data.kVAR},{data.kVARh},{data.PF},{data.kVA},{data.kVAh},{data.HZ})";
            InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                        $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.Sv},{data.Tv},{data.RSv},{data.STv},{data.TRv},{data.RA},{data.SA},{data.TA},{data.kW},{data.kWh},{data.kVAR},{data.kVARh},{data.PF},{data.kVA},{data.kVAh},{data.HZ})";
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh}";
                            var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh})";
                            Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh})";

                            var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                            conn.Execute(Proceduresql1);
                        }
                    }
                    break;
            }
        }
        #endregion

        #region 更新三相電表(單相使用) ForWeb、Log與預存程序
        /// <summary>
        /// 更新三相電表(單相使用) ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_SinglePhaseElectricMeter(ElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string Checksql_Log = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            string Proceduresql1 = string.Empty;
            Checksql = $"SELECT * FROM SinglePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM SinglePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;
            switch (PhaseAngleEnumType)
            {
                case PhaseAngleEnumType.R:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Rv}," +
                             $"a={data.RA}," +
                             $"kw = {data.kWh_A}," +
                             $"kwh = {data.kWh_A}," +
                             $"kvar={data.kVAR_A}," +
                             $"kvarh={data.kVARh_A}," +
                             $"kva={data.kVA_A}," +
                             $"kvah={data.kVAh_A}," +
                             $"pfe = {data.PF_A}," +
                             $"hz={data.HZ} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.RA},{data.kW_A},{data.kWh_A},{data.kVAR_A},{data.kVARh_A},{data.PF_A},{data.kVA_A},{data.kVAh_A},{data.HZ})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.RA},{data.kW_A},{data.kWh_A},{data.kVAR_A},{data.kVARh_A},{data.PF_A},{data.kVA_A},{data.kVAh_A},{data.HZ})";
                    }
                    break;
                case PhaseAngleEnumType.S:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Sv}," +
                             $"a={data.SA}," +
                             $"kw = {data.kWh_B}," +
                             $"kwh = {data.kWh_B}," +
                             $"kvar={data.kVAR_B}," +
                             $"kvarh={data.kVARh_B}," +
                             $"kva={data.kVA_B}," +
                             $"kvah={data.kVAh_B}," +
                             $"pfe = {data.PF_B}," +
                             $"hz={data.HZ} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv},{data.SA},{data.kW_B},{data.kWh_B},{data.kVAR_B},{data.kVARh_B},{data.PF_B},{data.kVA_B},{data.kVAh_B},{data.HZ})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv},{data.SA},{data.kW_B},{data.kWh_B},{data.kVAR_B},{data.kVARh_B},{data.PF_B},{data.kVA_B},{data.kVAh_B},{data.HZ})";
                    }
                    break;
                case PhaseAngleEnumType.T:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Tv}," +
                             $"a={data.TA}," +
                             $"kw = {data.kWh_C}," +
                             $"kwh = {data.kWh_C}," +
                             $"kvar={data.kVAR_C}," +
                             $"kvarh={data.kVARh_C}," +
                             $"kva={data.kVA_C}," +
                             $"kvah={data.kVAh_C}," +
                             $"pfe = {data.PF_C}," +
                             $"hz={data.HZ} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv},{data.TA},{data.kW_C},{data.kWh_C},{data.kVAR_C},{data.kVARh_C},{data.PF_C},{data.kVA_C},{data.kVAh_C},{data.HZ})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv},{data.TA},{data.kW_C},{data.kWh_C},{data.kVAR_C},{data.kVARh_C},{data.PF_C},{data.kVA_C},{data.kVAh_C},{data.HZ})";
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh_A}";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh_B}";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh_C}";
                                }
                                break;
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_A})";
                                    Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_A})";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_B})";
                                    Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_B})";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_C})";
                                    Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_C})";
                                }
                                break;
                        }
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                            conn.Execute(Proceduresql1);
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 更新多迴路電表(三相使用) ForWeb、Log與預存程序
        /// <summary>
        /// 更新多迴路電表(三相使用) ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_ThreePhaseElectricMeter(MultiCircuitElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            string Proceduresql1 = string.Empty;
            string Checksql_Log = string.Empty;
            Checksql = $"SELECT * FROM ThreePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM ThreePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            UpdataForwebsql = $"UPDATE ThreePhaseElectricMeter_ForWeb SET " +
                              $"ttime= '{ttime}'," +
                              $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                              $"rv = {data.Rv[data.LoopEnumType]}," +
                              $"sv={data.Sv[data.LoopEnumType]}," +
                              $"tv={data.Tv[data.LoopEnumType]}," +
                              $"rsv = {data.RSv[data.LoopEnumType]}," +
                              $"stv = {data.STv[data.LoopEnumType]}," +
                              $"trv = {data.TRv[data.LoopEnumType]}," +
                              $"ra={data.RA[data.LoopEnumType]}," +
                              $"sa={data.SA[data.LoopEnumType]}," +
                              $"ta={data.TA[data.LoopEnumType]}," +
                              $"kw = {data.kW[data.LoopEnumType]}," +
                              $"kwh = {data.kWh[data.LoopEnumType]}," +
                              $"kvar={data.kVAR[data.LoopEnumType]}," +
                              $"kvarh={data.kVARh[data.LoopEnumType]}," +
                              $"kva={data.kVA[data.LoopEnumType]}," +
                              $"kvah={data.kVAh[data.LoopEnumType]}," +
                              $"pfe = {data.PF[data.LoopEnumType]}," +
                              $"hz={data.HZ[data.LoopEnumType]} " +
                              $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            InsertForweb = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                        $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv[data.LoopEnumType]},{data.Sv[data.LoopEnumType]},{data.Tv[data.LoopEnumType]},{data.RSv[data.LoopEnumType]},{data.STv[data.LoopEnumType]},{data.TRv[data.LoopEnumType]},{data.RA[data.LoopEnumType]},{data.SA[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.kW[data.LoopEnumType]},{data.kWh[data.LoopEnumType]},{data.kVAR[data.LoopEnumType]},{data.kVARh[data.LoopEnumType]},{data.PF[data.LoopEnumType]},{data.kVA[data.LoopEnumType]},{data.kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
            InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                        $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv[data.LoopEnumType]},{data.Sv[data.LoopEnumType]},{data.Tv[data.LoopEnumType]},{data.RSv[data.LoopEnumType]},{data.STv[data.LoopEnumType]},{data.TRv[data.LoopEnumType]},{data.RA[data.LoopEnumType]},{data.SA[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.kW[data.LoopEnumType]},{data.kWh[data.LoopEnumType]},{data.kVAR[data.LoopEnumType]},{data.kVARh[data.LoopEnumType]},{data.PF[data.LoopEnumType]},{data.kVA[data.LoopEnumType]},{data.kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh[data.LoopEnumType]}";
                            var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh[data.LoopEnumType]})";
                            Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh[data.LoopEnumType]})";
                            var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                            conn.Execute(Proceduresql1);
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 更新多迴路電表(單相使用) ForWeb、Log與預存程序
        /// <summary>
        /// 更新三相電表(單相使用) ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_SinglePhaseElectricMeter(MultiCircuitElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string Checksql_Log = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            string Proceduresql1 = string.Empty;
            Checksql = $"SELECT * FROM SinglePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM SinglePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;
            switch (PhaseAngleEnumType)
            {
                case PhaseAngleEnumType.R:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Rv[data.LoopEnumType]}," +
                             $"a={data.RA[data.LoopEnumType]}," +
                             $"kw = {data.R_kW[data.LoopEnumType]}," +
                             $"kwh = {data.R_kWh[data.LoopEnumType]}," +
                             $"kvar={data.R_kVAR[data.LoopEnumType]}," +
                             $"kvarh={data.R_kVARh[data.LoopEnumType]}," +
                             $"kva={data.R_kVA[data.LoopEnumType]}," +
                             $"kvah={data.R_kVAh[data.LoopEnumType]}," +
                             $"pfe = {data.R_PF[data.LoopEnumType]}," +
                             $"hz={data.HZ[data.LoopEnumType]} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv[data.LoopEnumType]},{data.RA[data.LoopEnumType]},{data.R_kW[data.LoopEnumType]},{data.R_kWh[data.LoopEnumType]},{data.R_kVAR[data.LoopEnumType]},{data.R_kVARh[data.LoopEnumType]},{data.R_PF[data.LoopEnumType]},{data.R_kVA[data.LoopEnumType]},{data.R_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv[data.LoopEnumType]},{data.RA[data.LoopEnumType]},{data.R_kW[data.LoopEnumType]},{data.R_kWh[data.LoopEnumType]},{data.R_kVAR[data.LoopEnumType]},{data.R_kVARh[data.LoopEnumType]},{data.R_PF[data.LoopEnumType]},{data.R_kVA[data.LoopEnumType]},{data.R_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                    }
                    break;
                case PhaseAngleEnumType.S:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Sv[data.LoopEnumType]}," +
                             $"a={data.SA[data.LoopEnumType]}," +
                             $"kw = {data.S_kW[data.LoopEnumType]}," +
                             $"kwh = {data.S_kWh[data.LoopEnumType]}," +
                             $"kvar={data.S_kVAR[data.LoopEnumType]}," +
                             $"kvarh={data.S_kVARh[data.LoopEnumType]}," +
                             $"kva={data.S_kVA[data.LoopEnumType]}," +
                             $"kvah={data.S_kVAh[data.LoopEnumType]}," +
                             $"pfe = {data.S_PF[data.LoopEnumType]}," +
                             $"hz={data.HZ[data.LoopEnumType]} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv[data.LoopEnumType]},{data.SA[data.LoopEnumType]},{data.S_kW[data.LoopEnumType]},{data.S_kWh[data.LoopEnumType]},{data.S_kVAR[data.LoopEnumType]},{data.S_kVARh[data.LoopEnumType]},{data.S_PF[data.LoopEnumType]},{data.S_kVA[data.LoopEnumType]},{data.S_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv[data.LoopEnumType]},{data.SA[data.LoopEnumType]},{data.S_kW[data.LoopEnumType]},{data.S_kWh[data.LoopEnumType]},{data.S_kVAR[data.LoopEnumType]},{data.S_kVARh[data.LoopEnumType]},{data.S_PF[data.LoopEnumType]},{data.S_kVA[data.LoopEnumType]},{data.S_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                    }
                    break;
                case PhaseAngleEnumType.T:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Tv[data.LoopEnumType]}," +
                             $"a={data.TA[data.LoopEnumType]}," +
                             $"kw = {data.T_kW[data.LoopEnumType]}," +
                             $"kwh = {data.T_kWh[data.LoopEnumType]}," +
                             $"kvar={data.T_kVAR[data.LoopEnumType]}," +
                             $"kvarh={data.T_kVARh[data.LoopEnumType]}," +
                             $"kva={data.T_kVA[data.LoopEnumType]}," +
                             $"kvah={data.T_kVAh[data.LoopEnumType]}," +
                             $"pfe = {data.T_PF[data.LoopEnumType]}," +
                             $"hz={data.HZ[data.LoopEnumType]} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.T_kW[data.LoopEnumType]},{data.T_kWh[data.LoopEnumType]},{data.T_kVAR[data.LoopEnumType]},{data.T_kVARh[data.LoopEnumType]},{data.T_PF[data.LoopEnumType]},{data.T_kVA[data.LoopEnumType]},{data.T_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.T_kW[data.LoopEnumType]},{data.T_kWh[data.LoopEnumType]},{data.T_kVAR[data.LoopEnumType]},{data.T_kVARh[data.LoopEnumType]},{data.T_PF[data.LoopEnumType]},{data.T_kVA[data.LoopEnumType]},{data.T_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.R_kWh[data.LoopEnumType]}";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.S_kWh[data.LoopEnumType]}";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.T_kWh[data.LoopEnumType]}";
                                }
                                break;
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.R_kWh[data.LoopEnumType]})";
                                    Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.R_kWh[data.LoopEnumType]})";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.S_kWh[data.LoopEnumType]})";
                                    Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.S_kWh[data.LoopEnumType]})";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.T_kWh[data.LoopEnumType]})";
                                    Proceduresql1 = $"CALL ElectricGeneralkwhProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.T_kWh[data.LoopEnumType]})";
                                }
                                break;
                        }
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;

                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;

                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                            conn.Execute(Proceduresql1);
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 更新溫溼度感測器 ForWeb與Log
        /// <summary>
        /// 更新溫溼度感測器 ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_Senser(SenserData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string Checksql_log = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            Checksql = $"SELECT * FROM Senser_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_log = $"SELECT * FROM Senser_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            UpdataForwebsql = $"UPDATE Senser_ForWeb SET " +
                              $"ttime= '{ttime}'," +
                              $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                              $"Temperature = {data.Temperature}," +
                              $"Humidity={data.Humidity}," +
                              $"PM1={data.PM1}," +
                              $"PM25 = {data.PM25}," +
                              $"PM10 = {data.PM10}," +
                              $"CO2 = {data.CO2}," +
                              $"TVOC={data.TVOC}," +
                              $"HCHO={data.HCHO}," +
                              $"O3={data.O3}," +
                              $"CO = {data.CO}," +
                              $"Mold = {data.Mold}," +
                              $"IAQ={data.IAQ} " +
                              $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            InsertForweb = $"INSERT INTO {setting.InitialCatalog}.Senser_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,Temperature,Humidity,PM1,PM25,PM10,CO2,TVOC,HCHO,O3,CO,Mold,IAQ)VALUES(" +
                        $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Temperature},{data.Humidity },{data.PM1},{data.PM25},{data.PM10},{data.CO2},{data.TVOC},{data.HCHO},{data.O3},{data.CO},{data.Mold},{data.IAQ})";
            InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.Senser_Log (ttime,ttimen,GatewayIndex,DeviceIndex,Temperature,Humidity,PM1,PM25,PM10,CO2,TVOC,HCHO,O3,CO,Mold,IAQ)VALUES(" +
                          $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Temperature},{data.Humidity },{data.PM1},{data.PM25},{data.PM10},{data.CO2},{data.TVOC},{data.HCHO},{data.O3},{data.CO},{data.Mold},{data.IAQ})";
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            var Exist = conn.Query<SenserLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var LogExist = conn.Query<SenserLog>(Checksql_log).ToList();
                            if (LogExist.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            var Exist = conn.Query<SenserLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var LogExist = conn.Query<SenserLog>(Checksql_log).ToList();
                            if (LogExist.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 搜尋總表KWH Bar圖
        /// <summary>
        /// 搜尋總表KWH Bar圖
        /// </summary>
        /// <param name="gateWaySetting">通訊資訊</param>
        /// <param name="GroupIndex">群組編號</param>
        /// <param name="TimeIndex">時間類型</param>
        /// <param name="DateIndex">數值類型 0 = kW，1 = 錢</param>
        /// <returns></returns>
        public List<BarModule> Serch_TotalMeter_Bar(GateWaySetting gateWaySetting, int GroupIndex, int TimeIndex, int DateIndex)
        {
            List<BarModule> module = new List<BarModule>();
            string sql = string.Empty;
            List<string> Search_TotalMeterstr = new List<string>();
            string Search_Criteria = string.Empty; //搜尋條件

            foreach (var GateWaysitem in gateWaySetting.GateWays)
            {
                foreach (var Electricitem in GateWaysitem.GateWayElectricIDs)
                {
                    if (Electricitem.GroupIndex == GroupIndex)
                    {
                        Search_TotalMeterstr.Add($"(GatewayIndex = {GateWaysitem.GatewayIndex} AND DeviceIndex = {Electricitem.DeviceIndex})");
                    }
                }
            }

            if (Search_TotalMeterstr.Count > 0)
            {
                Search_Criteria = $"AND {Search_TotalMeterstr[0]}";
                for (int i = 1; i < Search_TotalMeterstr.Count; i++)
                {
                    Search_Criteria += $" OR {Search_TotalMeterstr[i]}";
                }
            }
            else
            {
                Search_Criteria = " ";
            }

            switch (TimeIndex)
            {
                case 0://天
                    {
                        if (setting.ElectricMeterPriceFlag)
                        {
                            if (DateIndex == 0)
                            {
                                sql = $"SELECT ttimen AS Argument,SUM(Total) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyyMMdd00}' AND ttime <='{DateTime.Now:yyyyMMdd23}' {Search_Criteria} GROUP BY ttime";
                            }
                            else
                            {
                                sql = $"SELECT ttimen AS Argument,SUM(MoneyTotal) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyyMMdd00}' AND ttime <='{DateTime.Now:yyyyMMdd23}' {Search_Criteria} GROUP BY ttime";
                            }
                        }
                        else
                        {
                            if (DateIndex == 0)
                            {
                                sql = $"SELECT  ttimen AS Argument,SUM(KwhTotal) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyyMMdd00}' AND ttime <='{DateTime.Now:yyyyMMdd23}' {Search_Criteria} GROUP BY ttime";
                            }
                            else
                            {
                                sql = $"SELECT  ttimen AS Argument,SUM(Price) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyyMMdd00}' AND ttime <='{DateTime.Now:yyyyMMdd23}' {Search_Criteria} GROUP BY ttime";
                            }
                        }
                    }
                    break;
                case 1://月
                    {
                        if (setting.ElectricMeterPriceFlag)
                        {
                            if (DateIndex == 0)
                            {
                                sql = $"SELECT ttimen AS Argument,SUM(Total) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyyMM0100}' AND ttime <='{DateTime.Now:yyyyMM3123}' {Search_Criteria} GROUP BY ttime";
                            }
                            else
                            {
                                sql = $"SELECT ttimen AS Argument,SUM(MoneyTotal) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyyMM0100}' AND ttime <='{DateTime.Now:yyyyMM3123}' {Search_Criteria} GROUP BY ttime";
                            }
                        }
                        else
                        {
                            if (DateIndex == 0)
                            {
                                sql = $"SELECT  ttimen AS Argument,SUM(KwhTotal) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyyMM0100}' AND ttime <='{DateTime.Now:yyyyMM3123}' {Search_Criteria} GROUP BY ttime";
                            }
                            else
                            {
                                sql = $"SELECT  ttimen AS Argument,SUM(Price) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyyMM0100}' AND ttime <='{DateTime.Now:yyyyMM3123}' {Search_Criteria} GROUP BY ttime";
                            }
                        }
                    }
                    break;
                case 2://年
                    {
                        if (setting.ElectricMeterPriceFlag)
                        {
                            if (DateIndex == 0)
                            {
                                sql = $"SELECT ttimen AS Argument,SUM(Total) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyy010100}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                            }
                            else
                            {
                                sql = $"SELECT ttimen AS Argument,SUM(MoneyTotal) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyy010100}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                            }
                        }
                        else
                        {
                            if (DateIndex == 0)
                            {
                                sql = $"SELECT  ttimen AS Argument,SUM(KwhTotal) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyy010100}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                            }
                            else
                            {
                                sql = $"SELECT  ttimen AS Argument,SUM(Price) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyy010100}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                            }
                        }
                    }
                    break;
            }


            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            module = conn.Query<BarModule>(sql).ToList();
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            module = conn.Query<BarModule>(sql).ToList();
                        }
                    }
                    break;
            }
            return module;
        }
        #endregion

        #region 搜尋總表KWH Pie圖
        /// <summary>
        /// 搜尋總表KWH Pie圖
        /// </summary>
        /// <param name="gateWaySetting">通訊資訊</param>
        /// <returns></returns>
        public PieModule Serch_TotalMeter_Pie(GateWaySetting gateWaySettings, int GroupIndex, string GroupName)
        {
            PieModule pieModule = new PieModule();
            List<PieModule> module = new List<PieModule>();
            string sql = string.Empty;
            List<string> Search_TotalMeterstr = new List<string>();
            string Search_Criteria = string.Empty; //搜尋條件

            foreach (var GateWaysitem in gateWaySettings.GateWays)
            {
                foreach (var Electricitem in GateWaysitem.GateWayElectricIDs)
                {
                    if (Electricitem.GroupIndex == GroupIndex)
                    {
                        Search_TotalMeterstr.Add($"(GatewayIndex = {GateWaysitem.GatewayIndex} AND DeviceIndex = {Electricitem.DeviceIndex})");
                    }
                }
            }

            if (Search_TotalMeterstr.Count > 0)
            {
                Search_Criteria = $"AND {Search_TotalMeterstr[0]}";
                for (int i = 1; i < Search_TotalMeterstr.Count; i++)
                {
                    Search_Criteria += $" OR {Search_TotalMeterstr[i]}";
                }
            }
            else
            {
                Search_Criteria = " ";
            }
            if (setting.ElectricMeterPriceFlag)
            {
                sql = $"SELECT ttimen AS Argument,SUM(Total) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyy010000}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
            }
            else
            {
                sql = $"SELECT  ttimen AS Argument,SUM(KwhTotal) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyy010000}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            module = conn.Query<PieModule>(sql).ToList();
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            module = conn.Query<PieModule>(sql).ToList();
                        }
                    }
                    break;
            }
            pieModule.Argument = GroupName;
            foreach (var item in module)
            {
                pieModule.Value += item.Value;
            }
            return pieModule;
        }
        #endregion

        #region 搜尋總表KWH Circel圖
        /// <summary>
        /// 搜尋總表KWH Pie圖
        /// </summary>
        /// <param name="gateWaySettings">通訊資訊</param>
        /// <param name="GroupIndex">群組編號</param>
        /// <param name="DateIndex">數值類型 0 = kW，1 = 錢</param>
        /// <returns></returns>
        public decimal Serch_TotalMeter_Circel(GateWaySetting gateWaySettings, int GroupIndex, int DateIndex)
        {
            decimal module = 0;
            string sql = string.Empty;
            List<string> Search_TotalMeterstr = new List<string>();
            string Search_Criteria = string.Empty; //搜尋條件

            foreach (var GateWaysitem in gateWaySettings.GateWays)
            {
                foreach (var Electricitem in GateWaysitem.GateWayElectricIDs)
                {
                    if (Electricitem.GroupIndex == GroupIndex)
                    {
                        Search_TotalMeterstr.Add($"(GatewayIndex = {GateWaysitem.GatewayIndex} AND DeviceIndex = {Electricitem.DeviceIndex})");
                    }
                }
            }

            if (Search_TotalMeterstr.Count > 0)
            {
                Search_Criteria = $"AND {Search_TotalMeterstr[0]}";
                for (int i = 1; i < Search_TotalMeterstr.Count; i++)
                {
                    Search_Criteria += $" OR {Search_TotalMeterstr[i]}";
                }
            }
            else
            {
                Search_Criteria = " ";
            }
            if (setting.ElectricMeterPriceFlag)
            {
                if (DateIndex == 0)
                {
                    sql = $"SELECT SUM(Total) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyy010000}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                }
                else
                {
                    sql = $"SELECT SUM(MoneyTotal) AS Value FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyy010000}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                }
            }
            else
            {
                if (DateIndex == 0)
                {
                    sql = $"SELECT SUM(KwhTotal) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyy010000}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                }
                else
                {
                    sql = $"SELECT SUM(Price) AS Value FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyy010000}' AND ttime <='{DateTime.Now:yyyy123123}' {Search_Criteria} GROUP BY ttime";
                }
               
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            var modules = conn.Query<decimal>(sql).ToList();
                            foreach (var item in modules)
                            {
                                module += item;
                            }
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            var modules = conn.Query<decimal>(sql).ToList();
                            foreach (var item in modules)
                            {
                                module += item;
                            }
                        }
                    }
                    break;
            }
            return module;
        }
        #endregion

        #region 查詢三段電價金額
        /// <summary>
        /// 查詢三段電價金額
        /// </summary>
        /// <param name="gateWaySettings"></param>
        /// <param name="GroupIndex"></param>
        /// <returns></returns>
        public ElectricDailykwh Serch_TotalMeter_ElectricDailykwh(GateWaySetting gateWaySettings, int GroupIndex)
        {
            string sql = string.Empty;
            List<string> Search_TotalMeterstr = new List<string>();
            string Search_Criteria = string.Empty; //搜尋條件
            foreach (var GateWaysitem in gateWaySettings.GateWays)
            {
                foreach (var Electricitem in GateWaysitem.GateWayElectricIDs)
                {
                    if (Electricitem.GroupIndex == GroupIndex)
                    {
                        Search_TotalMeterstr.Add($"(GatewayIndex = {GateWaysitem.GatewayIndex} AND DeviceIndex = {Electricitem.DeviceIndex})");
                    }
                }
            }

            if (Search_TotalMeterstr.Count > 0)
            {
                Search_Criteria = $"AND {Search_TotalMeterstr[0]}";
                for (int i = 1; i < Search_TotalMeterstr.Count; i++)
                {
                    Search_Criteria += $" OR {Search_TotalMeterstr[i]}";
                }
            }
            else
            {
                Search_Criteria = " ";
            }
            sql = $"SELECT SUM(MoneyTotal) AS MoneyTotal,SUM(Total) AS Total FROM electricdailykwh WHERE ttime >='{DateTime.Now:yyyyMM0000}' AND ttime <='{DateTime.Now:yyyyMM3123}' {Search_Criteria} GROUP BY ttime";
            using (var conn = new MySqlConnection(myscbs.ConnectionString))
            {
                var data = conn.Query<ElectricDailykwh>(sql).ToList();
                ElectricDailykwh electricDailykwh = new ElectricDailykwh();
                foreach (var item in data)
                {
                    electricDailykwh.Total += item.Total;
                    electricDailykwh.MoneyTotal += item.MoneyTotal;
                }
                return electricDailykwh;
            }
        }
        #endregion

        #region 查詢一般電費金額
        /// <summary>
        /// 查詢一般電費金額
        /// </summary>
        /// <param name="gateWaySettings"></param>
        /// <param name="GroupIndex"></param>
        /// <returns></returns>
        public ElectricTotalPrice Serch_TotalMeter_ElectricTotalPrice(GateWaySetting gateWaySettings, int GroupIndex)
        {
            string sql = string.Empty;
            List<string> Search_TotalMeterstr = new List<string>();
            string Search_Criteria = string.Empty; //搜尋條件
            foreach (var GateWaysitem in gateWaySettings.GateWays)
            {
                foreach (var Electricitem in GateWaysitem.GateWayElectricIDs)
                {
                    if (Electricitem.GroupIndex == GroupIndex)
                    {
                        Search_TotalMeterstr.Add($"(GatewayIndex = {GateWaysitem.GatewayIndex} AND DeviceIndex = {Electricitem.DeviceIndex})");
                    }
                }
            }

            if (Search_TotalMeterstr.Count > 0)
            {
                Search_Criteria = $"AND {Search_TotalMeterstr[0]}";
                for (int i = 1; i < Search_TotalMeterstr.Count; i++)
                {
                    Search_Criteria += $" OR {Search_TotalMeterstr[i]}";
                }
            }
            else
            {
                Search_Criteria = " ";
            }
            sql = $"SELECT  SUM(Price) AS Price,SUM(KwhTotal) AS KwhTotal FROM electrictotalprice WHERE ttime >='{DateTime.Now:yyyyMM0000}' AND ttime <='{DateTime.Now:yyyyMM3123}' {Search_Criteria} GROUP BY ttime";
            using (var conn = new MySqlConnection(myscbs.ConnectionString))
            {
                var data = conn.Query<ElectricTotalPrice>(sql).ToList();
                ElectricTotalPrice electricTotalPrice = new ElectricTotalPrice();
                foreach (var item in data)
                {
                    electricTotalPrice.Price += item.Price;
                    electricTotalPrice.KwhTotal += item.KwhTotal;
                }
                return electricTotalPrice;
            }
        }
        #endregion
    }
}