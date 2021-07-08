using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Modules
{
    /// <summary>
    /// 圓餅圖
    /// </summary>
    public class PieModule
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Argument { get; set; }
        /// <summary>
        /// 通訊編號
        /// </summary>
        public int GatewayIndex { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 數值
        /// </summary>
        public double Value { get; set; }
    }
}
