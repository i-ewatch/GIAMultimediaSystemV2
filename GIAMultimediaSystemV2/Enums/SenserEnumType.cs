using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Enums
{
    public  enum SenserEnumType
    {
        /// <summary>
        /// 溫濕度感測器(黑色485)
        /// </summary>
        BlackSenser,
        /// <summary>
        /// 溫濕度感測器(白色485)
        /// </summary>
        WhiteSenser,
        /// <summary>
        /// 氣象局API
        /// </summary>
        WeatherAPI,
        /// <summary>
        /// GIA API網址
        /// </summary>
        GIAAPI,
        /// <summary>
        /// GIA Modbus RTU
        /// </summary>
        GIA,
    }
}
