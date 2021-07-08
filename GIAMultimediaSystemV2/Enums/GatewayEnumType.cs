using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Enums
{
    public enum GatewayEnumType
    {
        /// <summary>
        /// RS485
        /// </summary>
        ModbusRTU,
        /// <summary>
        /// TCP/IP
        /// </summary>
        ModbusTCP,
        /// <summary>
        /// API網址
        /// </summary>
        API,
        /// <summary>
        /// EMS TCP
        /// </summary>
        EMS
    }
}
