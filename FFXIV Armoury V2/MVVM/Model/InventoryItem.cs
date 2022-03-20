using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class InventoryItem: ObservableObject
    {
        private Item? _item;

        public Item? GearItem
        {
            get { return _item; }
            set { 
                _item = value;
                OnPropertyChanged();
            }
        }

        private Inventory? _inventory;

        public Inventory? GearInventory
        {
            get { return _inventory; }
            set { 
                _inventory = value;
                OnPropertyChanged();
            }
        }

    }
}
