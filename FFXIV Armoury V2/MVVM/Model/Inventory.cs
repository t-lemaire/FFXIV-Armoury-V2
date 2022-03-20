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

        public string NameLabel
        {
            get
            {
                if (InvType == InventoryType.Retainer)
                {
                    return $"{Name} (Retainer)";
                } else
                {
                    switch (InvType)
                    {
                        case InventoryType.GlamourDresser:
                            return "Glamour Dresser";
                            break;
                        case InventoryType.Armoire:
                            return "Armoire";
                            break;
                        case InventoryType.CharacterInventory:
                            return "Personal Inventory";
                            break;
                        case InventoryType.ChocoboSaddleBag:
                            return "Chocobo Saddlebag";
                            break;
                        case InventoryType.ArmouryChest:
                            return "Armoury Chest";
                            break;
                        default:
                            return "Unknown Inventory";
                            break;
                    }
                }
            }
        }
    }
}
