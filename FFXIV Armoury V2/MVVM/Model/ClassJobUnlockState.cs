using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ClassJobUnlockState: ObservableObject
    {
        private int? _id;

        public int? Id
        {
            get { return _id; }
            set {
                _id = value;
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

    }
}
