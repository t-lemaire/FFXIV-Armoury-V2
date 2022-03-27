using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    public class ManageRetainersViewModel: ObservableObject
    {
        private ObservableCollection<Inventory>? _retainersList;

        public ObservableCollection<Inventory>? Retainers
        {
            get { return _retainersList; }
            set { 
                _retainersList = value;
                OnPropertyChanged();
                OnPropertyChanged("FilteredRetainers");
            }
        }

        private ObservableCollection<Inventory>? _filteredRetainers;

        public ObservableCollection<Inventory>? FilteredRetainers
        {
            get { 
                if (Retainers.Count == 0 || CurrentCharacter == null)
                {
                    return new ObservableCollection<Inventory>();
                } else
                {
                    return new ObservableCollection<Inventory>(Retainers.Where(r => r.CharacterId == CurrentCharacter.Id));
                }
            }
        }


        private Character? _currentCharacter;

        public Character? CurrentCharacter
        {
            get { return _currentCharacter; }
            set { 
                _currentCharacter = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RemoveRetainerCmd { get; set; }
        public RelayCommand SaveRetainerCmd { get; set; }
        public RelayCommand AddRetainer { get; set; }

        public ManageRetainersViewModel()
        {
            CurrentCharacter = CharacterHelper.CurrentCharacter;
            Retainers = CharacterHelper.FetchRetainers();

            RemoveRetainerCmd = new RelayCommand(async o =>
            {
                if (!(o is Inventory))
                {
                    return;
                } else if (Retainers == null || Retainers.Count == 0) {
                    return;
                }

                Retainers.Remove((Inventory)o);
                CharacterHelper.RemoveRetainer((Inventory)o);
                CharacterHelper.SaveRetainersList();

                ObservableCollection<InventoryItem> items = ItemHelper.FetchItems(CurrentCharacter);
                if (items.Count > 0)
                {
                    foreach (InventoryItem item in items)
                    {
                        item.GearInventory = new Inventory();
                        item.GearInventory.CharacterId = CurrentCharacter.Id;
                    }

                    ItemHelper.SaveItems(CurrentCharacter);
                }

                OnPropertyChanged("FilteredRetainers");
            });

            SaveRetainerCmd = new RelayCommand(async o =>
            {
                if (!(o is Inventory))
                {
                    return;
                }

                Inventory newRetainer = (Inventory)o;
                newRetainer.Id = String.IsNullOrEmpty(newRetainer.Id) ? Guid.NewGuid().ToString() : newRetainer.Id;
                newRetainer.InvType = InventoryType.Retainer;
                newRetainer.CharacterId = newRetainer.CharacterId == null ? CurrentCharacter.Id : newRetainer.CharacterId;
                
                CharacterHelper.SaveRetainer(newRetainer);
                OnPropertyChanged("FilteredRetainers");
            });

            AddRetainer = new RelayCommand(async o =>
            {
                Inventory newRetainer = new Inventory();
                newRetainer.Id = Guid.NewGuid().ToString();
                newRetainer.InvType = InventoryType.Retainer;
                newRetainer.CharacterId = CurrentCharacter.Id;

                Retainers.Add(newRetainer);
                OnPropertyChanged("FilteredRetainers");
            });
        }
    }
}
