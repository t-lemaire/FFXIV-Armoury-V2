using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ApiEquipSlotCategory
    {
        public int Body { get; set; }
        public int Ears { get; set; }
        public int Feet { get; set; }
        public int FingerL { get; set; }
        public int FingerR { get; set; }
        public int Gloves { get; set; }
        public int Head { get; set; }
        public int Legs { get; set; }
        public int MainHand { get; set; }
        public int Neck { get; set; }
        public int OffHand { get; set; }
        public int Wrists { get; set; }

        public string SlotName
        {
            get
            {
                if (Body == 1)
                {
                    return "Body";
                } else if (Ears == 1)
                {
                    return "Earring";
                }
                else if (Feet == 1)
                {
                    return "Feet";
                }
                else if (FingerL == 1 || FingerR == 1)
                {
                    return "Ring";
                }
                else if (Gloves == 1)
                {
                    return "Gloves";
                }
                else if (Head == 1)
                {
                    return "Head";
                }
                else if (Legs == 1)
                {
                    return "Legs";
                }
                else if (MainHand == 1)
                {
                    return "Main Hand";
                }
                else if (Neck == 1)
                {
                    return "Necklace";
                }
                else if (OffHand == 1)
                {
                    return "Offhand";
                }
                else if (Wrists == 1)
                {
                    return "Bracelets";
                } else
                {
                    return "Unknown";
                }
            }
        }
    }
}
