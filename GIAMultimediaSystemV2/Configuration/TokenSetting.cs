using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    public class TokenSetting
    {
        /// <summary>
        /// 設備ID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 權杖
        /// </summary>
        public string LicenseCode { get; set; }
        /// <summary>
        /// 系統號碼
        /// </summary>
        public string SystemNumber { get; set; }
        /// <summary>
        /// 授權開始時間
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 系統最後關機時間
        /// </summary>
        public string FromTime { get; set; }
        /// <summary>
        /// 授權截止時間
        /// </summary>
        public string EndTime { get; set; }
    }
}
