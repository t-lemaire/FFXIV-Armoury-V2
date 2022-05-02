using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class GitHubRelease
    {
        public string Html_Url { get; set; }
        public string Name { get; set; }
        public Version Tag_Name { get; set; }
        public DateTime Created_At { get; set; }
        public string Body { get; set; }
    }
}
