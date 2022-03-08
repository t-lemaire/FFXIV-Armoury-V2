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

    }
}
