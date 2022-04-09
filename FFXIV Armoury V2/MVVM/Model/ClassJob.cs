using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ClassJob: ObservableObject
    {
        private int? _classId;

        public int? ClassId
        {
            get { return _classId; }
            set { 
                _classId = value;
                OnPropertyChanged();
            }
        }

        private int? _expLevel;

        public int? ExpLevel
        {
            get { return _expLevel; }
            set { 
                _expLevel = value;
                OnPropertyChanged();
            }
        }

        private int _expLevelMax;

        public int ExpLevelMax
        {
            get { return _expLevelMax; }
            set { 
                _expLevelMax = value;
                OnPropertyChanged();
            }
        }

        private int? _expLevelToGo;

        public int? ExpLevelToGo
        {
            get { return _expLevelToGo; }
            set {
                _expLevelToGo = value;
                OnPropertyChanged();
            }
        }

        private int? _jobId;

        public int? JobId
        {
            get { return _jobId; }
            set { 
                _jobId = value;
                OnPropertyChanged();
            }
        }

        private int? _level;

        public int? Level
        {
            get { return _level; }
            set { 
                _level = value;
                OnPropertyChanged();
            }
        }

        private string? _name;

        public string? Name
        {
            get { return _name; }
            set { 
                _name = value;
                OnPropertyChanged();
            }
        }

        private ClassJobUnlockState? _unlockedState;

        public ClassJobUnlockState? UnlockedState
        {
            get { return _unlockedState; }
            set { 
                _unlockedState = value;
                OnPropertyChanged();
            }
        }

        private int? _classJobId
        {
            get
            {
                int? classJobId = ClassId;

                if (UnlockedState != null)
                {
                    if (UnlockedState.Id == null)
                    {
                        classJobId = ClassId;
                    }
                    else
                    {
                        classJobId = UnlockedState.Id;
                    }
                }

                return classJobId;
            }
        }

        public string JobIcon
        {
            get
            {
                switch (_classJobId)
                {
                    case 1:
                        return "/Images/Icons/Tanks/Gladiator.png";
                    case 2:
                        return "/Images/Icons/DPS/Pugilist.png";
                    case 3:
                        return "/Images/Icons/Tanks/Marauder.png";
                    case 4:
                        return "/Images/Icons/DPS/Lancer.png";
                    case 5:
                        return "/Images/Icons/DPS/Archer.png";
                    case 6:
                        return "/Images/Icons/Healers/Conjurer.png";
                    case 7:
                        return "/Images/Icons/DPS/Thaumaturge.png";
                    case 8:
                        return "/Images/Icons/Crafters/Carpenter.png";
                    case 9:
                        return "/Images/Icons/Crafters/Blacksmith.png";
                    case 10:
                        return "/Images/Icons/Crafters/Armorer.png";
                    case 11:
                        return "/Images/Icons/Crafters/Goldsmith.png";
                    case 12:
                        return "/Images/Icons/Crafters/Leatherworker.png";
                    case 13:
                        return "/Images/Icons/Crafters/Weaver.png";
                    case 14:
                        return "/Images/Icons/Crafters/Alchemist.png";
                    case 15:
                        return "/Images/Icons/Crafters/Culinarian.png";
                    case 16:
                        return "/Images/Icons/Gatherers/Miner.png";
                    case 17:
                        return "/Images/Icons/Gatherers/Botanist.png";
                    case 18:
                        return "/Images/Icons/Gatherers/Fisher.png";
                    case 19:
                        return "/Images/Icons/Tanks/Paladin.png";
                    case 20:
                        return "/Images/Icons/DPS/Monk.png";
                    case 21:
                        return "/Images/Icons/Tanks/Warrior.png";
                    case 22:
                        return "/Images/Icons/DPS/Dragoon.png";
                    case 23:
                        return "/Images/Icons/DPS/Bard.png";
                    case 24:
                        return "/Images/Icons/Healers/WhiteMage.png";
                    case 25:
                        return "/Images/Icons/DPS/BlackMage.png";
                    case 26:
                        return "/Images/Icons/DPS/Arcanist.png";
                    case 27:
                        return "/Images/Icons/DPS/Summoner.png";
                    case 28:
                        return "/Images/Icons/Healers/Scholar.png";
                    case 29:
                        return "/Images/Icons/DPS/Rogue.png";
                    case 30:
                        return "/Images/Icons/DPS/Ninja.png";
                    case 31:
                        return "/Images/Icons/DPS/Machinist.png";
                    case 32:
                        return "/Images/Icons/Tanks/DarkKnight.png";
                    case 33:
                        return "/Images/Icons/Healers/Astrologian.png";
                    case 34:
                        return "/Images/Icons/DPS/Samurai.png";
                    case 35:
                        return "/Images/Icons/DPS/RedMage.png";
                    case 36:
                        return "/Images/Icons/DPS/BlueMage.png";
                    case 37:
                        return "/Images/Icons/Tanks/Gunbreaker.png";
                    case 38:
                        return "/Images/Icons/DPS/Dancer.png";
                    case 39:
                        return "/Images/Icons/DPS/Reaper.png";
                    case 40:
                        return "/Images/Icons/Healers/Sage.png";
                    default:
                        return "/Images/Icons/meteor_flat.png";
                }
            }
        }

        public string JobLogo
        {
            get
            {
                switch (_classJobId)
                {
                    case 1:
                        return "/Images/Logos/Gladiator.png";
                    case 2:
                        return "/Images/Logos/Pugilist.png";
                    case 3:
                        return "/Images/Logos/Marauder.png";
                    case 4:
                        return "/Images/Logos/Lancer.png";
                    case 5:
                        return "/Images/Logos/Archer.png";
                    case 6:
                        return "/Images/Logos/Conjurer.png";
                    case 7:
                        return "/Images/Logos/Thaumaturge.png";
                    case 8:
                        return "/Images/Logos/Carpenter.png";
                    case 9:
                        return "/Images/Logos/Blacksmith.png";
                    case 10:
                        return "/Images/Logos/Armorer.png";
                    case 11:
                        return "/Images/Logos/Goldsmith.png";
                    case 12:
                        return "/Images/Logos/Leatherworker.png";
                    case 13:
                        return "/Images/Logos/Weaver.png";
                    case 14:
                        return "/Images/Icons/Crafters/Alchemist.png";
                    case 15:
                        return "/Images/Icons/Crafters/Culinarian.png";
                    case 16:
                        return "/Images/Icons/Gatherers/Miner.png";
                    case 17:
                        return "/Images/Icons/Gatherers/Botanist.png";
                    case 18:
                        return "/Images/Icons/Gatherers/Fisher.png";
                    case 19:
                        return "/Images/Logos/Paladin.png";
                    case 20:
                        return "/Images/Logos/Monk.png";
                    case 21:
                        return "/Images/Logos/Warrior.png";
                    case 22:
                        return "/Images/Logos/Dragoon.png";
                    case 23:
                        return "/Images/Logos/Bard.png";
                    case 24:
                        return "/Images/Logos/WhiteMage.png";
                    case 25:
                        return "/Images/Logos/BlackMage.png";
                    case 26:
                        return "/Images/Logos/Arcanist.png";
                    case 27:
                        return "/Images/Logos/Summoner.png";
                    case 28:
                        return "/Images/Logos/Scholar.png";
                    case 29:
                        return "/Images/Logos/Rogue.png";
                    case 30:
                        return "/Images/Logos/Ninja.png";
                    case 31:
                        return "/Images/Logos/Machinist.png";
                    case 32:
                        return "/Images/Logos/DarkKnight.png";
                    case 33:
                        return "/Images/Logos/Astrologian.png";
                    case 34:
                        return "/Images/Logos/Samurai.png";
                    case 35:
                        return "/Images/Logos/RedMage.png";
                    case 36:
                        return "/Images/Logos/BlueMage.png";
                    case 37:
                        return "/Images/Logos/Gunbreaker.png";
                    case 38:
                        return "/Images/Logos/Dancer.png";
                    case 39:
                        return "/Images/Logos/Reaper.png";
                    case 40:
                        return "/Images/Logos/Sage.png";
                    default:
                        return "/Images/Icons/meteor_flat.png";
                }
            }
        }

        public bool IsTank
        {
            get
            {
                switch (_classJobId)
                {
                    case 1:
                    case 3:
                    case 19:
                    case 21:
                    case 32:
                    case 37:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsHealer
        {
            get
            {
                switch (_classJobId)
                {
                    case 6:
                    case 24:
                    case 28:
                    case 33:
                    case 40:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsCrafter
        {
            get
            {
                switch (_classJobId)
                {
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsGatherer
        {
            get
            {
                switch (_classJobId)
                {
                    case 16:
                    case 17:
                    case 18:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsMeeleeDps
        {
            get
            {
                switch (_classJobId)
                {
                    case 2:
                    case 4:
                    case 20:
                    case 22:
                    case 29:
                    case 30:
                    case 34:
                    case 39:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsPhysicalRangedDps
        {
            get
            {
                switch (_classJobId)
                {
                    case 5:
                    case 23:
                    case 31:
                    case 38:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsMagicalRangedDps
        {
            get
            {
                switch (_classJobId)
                {
                    case 7:
                    case 25:
                    case 26:
                    case 27:
                    case 35:
                    case 36:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsDps
        {
            get
            {
                return IsMeeleeDps || IsPhysicalRangedDps || IsMagicalRangedDps;
            }
        }

        public string Colour1
        {
            get
            {
                if (IsTank)
                {
                    return "#2e3c83";
                }
                else if (IsHealer)
                {
                    return "#2a661a";
                }
                else if (IsDps)
                {
                    return "#512727";
                }
                else if (IsCrafter)
                {
                    return "#37342f";
                }
                else if (IsGatherer)
                {
                    return "#37342f";
                }
                else
                {
                    return "#37342f";
                }
            }
        }

        public string Colour2
        {
            get
            {
                if (IsTank)
                {
                    return "#475dce";
                }
                else if (IsHealer)
                {
                    return "#468d32";
                }
                else if (IsDps)
                {
                    return "#813b3c";
                }
                else if (IsCrafter)
                {
                    return "#494949";
                }
                else if (IsGatherer)
                {
                    return "#494949";
                }
                else
                {
                    return "#494949";
                }
            }
        }

        public static int ClassJobIdFromAccronym(string accronym)
        {
            switch (accronym.ToUpper())
            {
                case "ACN":
                    return 26;
                case "ALC":
                    return 14;
                case "ARC":
                    return 5;
                case "ARM":
                    return 10;
                case "AST":
                    return 33;
                case "BLM":
                    return 25;
                case "BLU":
                    return 36;
                case "BRD":
                    return 23;
                case "BSM":
                    return 9;
                case "BTN":
                    return 17;
                case "CNJ":
                    return 6;
                case "CRP":
                    return 8;
                case "CUL":
                    return 15;
                case "DNC":
                    return 38;
                case "DRG":
                    return 22;
                case "DRK":
                    return 32;
                case "FSH":
                    return 18;
                case "GLA":
                    return 1;
                case "GNB":
                    return 37;
                case "GSM":
                    return 11;
                case "LNC":
                    return 4;
                case "LTW":
                    return 12;
                case "MCH":
                    return 31;
                case "MIN":
                    return 16;
                case "MNK":
                    return 20;
                case "MRD":
                    return 3;
                case "NIN":
                    return 30;
                case "PGL":
                    return 2;
                case "PLD":
                    return 19;
                case "RDM":
                    return 35;
                case "ROG":
                    return 29;
                case "RPR":
                    return 39;
                case "SAM":
                    return 34;
                case "SCH":
                    return 28;
                case "SGE":
                    return 40;
                case "SMN":
                    return 27;
                case "THM":
                    return 7;
                case "WAR":
                    return 21;
                case "WHM":
                    return 24;
                case "WVR":
                    return 13;
                default:
                    return 0;
            }
        }

        public static ClassJob InstanciateClassJob(string classJob)
        {
            ClassJob cj = new ClassJob();

            cj.ClassId = ClassJobIdFromAccronym(classJob);
            cj.Name = classJob;

            return cj;
        }

        public static ClassJob InstanciateClassJob(ApiClassJobInfo classJob)
        {
            ClassJob cj = new ClassJob();
            cj.ClassId = classJob.Id;
            cj.Name= classJob.Name;

            return cj;
        }
    }
}
