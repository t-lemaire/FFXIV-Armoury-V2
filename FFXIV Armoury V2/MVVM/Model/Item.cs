using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Icon { get; set; }
        public string IconHd { get; set; }
        public ApiEquipSlotCategory EquipSlotCategory { get; set; }

        public ApiClassJobCategory ClassJobCategory { get; set; }
    }
}
