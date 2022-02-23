using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    class GearItem
    {
        public string Name { get; set; }
        public int ItemLevel { get; set; }
        public int? LodestoneId { get; set; }

        public GearItem(string name, int itemLevel = 1, int? lodestoneId = null)
        {
            Name = name;
            ItemLevel = itemLevel;
            LodestoneId = lodestoneId;
        }
    }
}
