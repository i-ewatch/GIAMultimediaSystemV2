using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    public class SqlDBSetting
    {
        /// <summary>
        /// 資料庫類型
        /// </summary>
        public int SQLEnumsType { get; set; }
        /// <summary>
        /// 資料庫IP
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        public string InitialCatalog { get; set; }
        /// <summary>
        /// 資料庫帳號
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 資料庫密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 電表收費旗標
        /// </summary>
        public bool ElectricMeterPriceFlag { get; set; }
    }
}
