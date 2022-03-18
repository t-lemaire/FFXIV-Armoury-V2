using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class Inventory: ObservableObject
    {
        public enum InventoryType
        {
            None = 0,
            CharacterInventory = 1,
            ChocoboSaddleBag = 2,
            Retainer = 3,
            GlamourDresser = 4,
            Armoire = 5,
            ArmouryChest = 6
        }

        private InventoryType _invType;

        public InventoryType InvType
        {
            get { return _invType; }
            set { _invType = value; }
        }

        private string? _id;

        public string? Id
        {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { 
                _name = value;
                OnPropertyChanged();
            }
        }

        private int? _characterId;

        public int? CharacterId
        {
            get { return _characterId; }
            set { 
                _characterId = value;
                OnPropertyChanged();
            }
        }


    }
}
