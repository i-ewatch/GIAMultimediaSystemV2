using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    public class UploadSetting
    {
        /// <summary>
        /// 上傳類型
        /// </summary>
        public int UploadEnumType { get; set; }
        /// <summary>
        /// API上傳網址
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 卡版號上傳
        /// </summary>
        public List<SendX200> SendX200s { get; set; } = new List<SendX200>();

    }
    public class SendX200
    {
        /// <summary>
        /// 卡號
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 版號
        /// </summary>
        public string BoardNo { get; set; }
        /// <summary>
        /// 上傳電表列表
        /// </summary>
        public List<ElectricSendItem> ElectricSendItems { get; set; } = new List<ElectricSendItem>();
        /// <summary>
        /// 上傳感測器列表
        /// </summary>
        public List<SenserSendItem> SenserSendItems { get; set; } = new List<SenserSendItem>();
    }
    public class ElectricSendItem
    {
        /// <summary>
        /// Gateway編號
        /// </summary>
        public int GatewayIndex { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 電表類型
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
    }
    public class SenserSendItem
    {
        /// <summary>
        /// Gateway編號
        /// </summary>
        public int GatewayIndex { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 設備類型
        /// <para> 0 = BlackSenser </para>
        /// <para> 1 = WhiteSenser </para>
        /// <para> 2 = 氣象局API </para>
        /// </summary>
        public int SenserEnumType { get; set; }
    }
}
