using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Methods;
using MathLibrary;
using NModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Protocols
{
    public abstract class AbsProtocol
    {
        /// <summary>
        /// 標註
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// 地區資訊
        /// </summary>
        public List<Taiwan_DistricsSetting> Taiwan_DistricsSettings { get; set; }
        /// <summary>
        /// 通訊資訊
        /// </summary>
        public GateWaySetting GateWaySetting { get; set; }
        /// <summary>
        /// GIA網址
        /// </summary>
        public string GIALocation { get; set; }
        /// <summary>
        /// 電表類型
        /// </summary>
        public int ElectricEnumType { get; set; } = -1;
        /// <summary>
        /// 感測器類型
        /// </summary>
        public int SenserEnumType { get; set; } = -1;
        /// <summary>
        /// 連線旗標
        /// </summary>
        public bool ConnectFlag { get; set; }
        /// <summary>
        /// 通訊編號
        /// </summary>
        public int GatewayIndex { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 群組編號
        /// </summary>
        public int GroupIndex { get; set; }
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
        /// 數學公式方法
        /// </summary>
        public MathClass MathClass { get; set; } = new MathClass();
        /// <summary>
        /// 設備ID
        /// </summary>
        public byte ID { get; set; }
        /// <summary>
        /// 資料讀取(Modbus)
        /// </summary>
        /// <param name="master"></param>
        public abstract void DataReader(IModbusMaster master);
        /// <summary>
        /// 資料讀取(API)
        /// </summary>
        public abstract void DataAPIReader();
    }
}
