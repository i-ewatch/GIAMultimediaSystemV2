using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    public class GroupSetting
    {
        public List<Group> Groups { get; set; } = new List<Group>();
    }
    public class Group
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GroupName { get; set; }
    }
}
