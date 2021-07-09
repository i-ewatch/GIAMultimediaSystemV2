using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    public class GateWaySetting
    {
        /// <summary>
        /// 模式選擇
        /// </summary>
        public int ModeIndex { get; set; }
        /// <summary>
        /// 通訊旗標
        /// <para>True = 使用通訊</para>
        /// <para>False = 不使用通訊</para>
        /// </summary>
        public bool ControlFlag { get; set; }
        /// <summary>
        /// 紀錄旗標
        /// <para>True = 使用紀錄</para>
        /// <para>False = 不使用紀錄</para>
        /// </summary>
        public bool RecordFlag { get; set; }
        /// <summary>
        /// 上傳旗標
        /// <para>True = 使用上傳</para>
        /// <para>False = 不使用上傳</para>
        /// </summary>
        public bool UploadFlag { get; set; }
        /// <summary>
        /// 通訊通道
        /// </summary>
        public List<GateWay> GateWays { get; set; } = new List<GateWay>();
    }
    /// <summary>
    /// 通訊通道
    /// </summary>
    public class GateWay
    {
        /// <summary>
        /// Gateway編號
        /// </summary>
        public int GatewayIndex { get; set; }
        /// <summary>
        /// RTU COM位址
        /// </summary>
        public string ModbusRTULocation { get; set; }
        /// <summary>
        /// RTU Rate
        /// </summary>
        public int ModbusRTURate { get; set; }
        /// <summary>
        /// TCP IP位址
        /// </summary>
        public string ModbusTCPLocation { get; set; }
        /// <summary>
        /// TCP Port號
        /// </summary>
        public int ModbusTCPRate { get; set; }
        /// <summary>
        /// API網址
        /// </summary>
        public string APILocation { get; set; }
        /// <summary>
        /// GIA API網址
        /// </summary>
        public string GIAAPILocation { get; set; }
        /// <summary>
        /// EMS IP位址
        /// </summary>
        public string EMSLocation { get; set; }
        /// <summary>
        /// EMS Port號
        /// </summary>
        public int EMSRate { get; set; }
        /// <summary>
        /// API網址
        /// </summary>
        public string WeatherAPILocation { get; set; }
        /// <summary>
        /// 氣象開放資料平台會員授權碼
        /// </summary>
        public string Authorization { get; set; }
        /// <summary>
        /// 臺灣各縣市名稱
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 臺灣各縣市代碼
        /// </summary>
        public int WeatherTypeEnum { get; set; }
        /// <summary>
        /// 地區名稱
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// 設備通訊類型
        /// <para>0 = Modbus RTU</para>
        /// <para>1 = Modbus TCP</para>
        /// <para>2 = API</para>
        /// <para>3 = EMS</para>
        /// </summary>
        public int GatewayEnumType { get; set; }
        /// <summary>
        /// 環境感測器ID
        /// </summary>
        public List<GateWaySenserID> GateWaySenserIDs { get; set; } = new List<GateWaySenserID>();
        /// <summary>
        /// 電表設備ID
        /// </summary>
        public List<GateWayElectricID> GateWayElectricIDs { get; set; } = new List<GateWayElectricID>();
        /// <summary>
        /// 通道名稱
        /// </summary>
        public string GatewayName { get; set; }
    }
    /// <summary>
    /// 環境感測器ID
    /// </summary>
    public class GateWaySenserID
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 設備ID
        /// </summary>
        public byte DeviceID { get; set; }
        /// <summary>
        /// 設備類型
        /// <para> 0 = BlackSenser </para>
        /// <para> 1 = WhiteSenser </para>
        /// <para> 2 = 氣象局API </para>
        /// <para> 3 = GAISenser API </para>
        /// <para> 4 = GAISenser  </para>
        /// </summary>
        public int SenserEnumType { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string DeviceName { get; set; }
    }
    /// <summary>
    /// 電表設備ID
    /// </summary>
    public class GateWayElectricID
    {
        /// <summary>
        /// 總表旗標
        /// </summary>
        public bool TotalMeterFlag { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 群組編號
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 設備ID
        /// </summary>
        public byte DeviceID { get; set; }
        /// <summary>
        /// 設備類型
        /// <para> 0 = PA310電表 </para>
        /// <para> 1 = HC6600電表 </para>
        /// <para> 2 = SPM6電表 </para>
        /// <para> 3 = PA60多迴路電表 </para>
        /// <para> 4 = ABBM2M電表 </para>
        /// </summary>
        public int ElectricEnumType { get; set; }
        /// <summary>
        /// 迴路
        /// <para> 0 = 第一迴路 </para>
        /// <para> 1 = 第二迴路 </para>
        /// <para> 2 = 第三迴路 </para>
        /// <para> 3 = 第四迴路 </para>
        /// </summary>
        public int LoopEnumType { get; set; }
        /// <summary>
        /// 電表相位
        /// <para> 0 = 單相 </para>
        /// <para> 1 = 三相 </para>
        /// </summary>
        public int PhaseEnumType { get; set; }
        /// <summary>
        /// 電表相位角
        /// <para> 0 = R </para>
        /// <para> 1 = S </para>
        /// <para> 2 = T </para>
        /// </summary>
        public int PhaseAngleEnumType { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string DeviceName { get; set; }
    }
}
