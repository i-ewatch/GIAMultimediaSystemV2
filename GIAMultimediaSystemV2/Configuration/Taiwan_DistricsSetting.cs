using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    /// <summary>
    /// 台灣縣市區設定
    /// </summary>
    public class Taiwan_DistricsSetting
    {
        public string CityName { get; set; }
        public string CityEngName { get; set; }
        public Arealist[] AreaList { get; set; }
    }

    public class Arealist
    {
        public string ZipCode { get; set; }
        public string AreaName { get; set; }
        public string AreaEngName { get; set; }
        public Roadlist[] RoadList { get; set; }
    }

    public class Roadlist
    {
        public string RoadName { get; set; }
        public string RoadEngName { get; set; }
    }
}
