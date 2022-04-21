using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int? LevelEquip { get; set; }
        public int? LevelItem { get; set; }
        public ApiEquipSlotCategory EquipSlotCategory { get; set; }

        public ApiClassJobCategory ClassJobCategory { get; set; }

        public string IconUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(IconHd))
                {
                    return $"https://xivapi.com/{IconHd}";
                }
                else if (!string.IsNullOrEmpty(Icon))
                {
                    return $"https://xivapi.com/{Icon}";
                } else
                {
                    return "";
                }
            }
        }

        public ObservableCollection<ClassJob> AvailableJobs
        {
            get
            {
                return ClassJobCategory.AvailableClassJobs();
            }
        }

        public bool IsLowLevel
        {
            get
            {
                if (AvailableJobs.Count > 0)
                {
                    bool hasHigherLevel = false;
                    Character? character = CharacterHelper.CurrentCharacter;

                    if (character == null)
                    {
                        return false;
                    }

                    foreach (ClassJob itemJob in AvailableJobs)
                    {
                        foreach (ClassJob characterJob in character.ClassJobs)
                        {
                            if (itemJob.ClassId == characterJob.ClassId && LevelEquip >= characterJob.Level)
                            {
                                hasHigherLevel = true;
                                break;
                            }
                        }

                        if (hasHigherLevel)
                        {
                            break;
                        }
                    }

                    return !hasHigherLevel;
                }

                return false;
            }
        }
    }
}
