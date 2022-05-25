using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    public class GIA_DistricsSetting
    {
            public Data[] data { get; set; }
            public string ret { get; set; }
            public string retDescription { get; set; }
    }
    public class Data
    {
        public string type { get; set; }
        public string parent_id { get; set; }
        public string uuid { get; set; }
        public string alias { get; set; }
        public string status { get; set; }
        public string dataDatetime { get; set; }
        public string County { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Township { get; set; }
    }
}
