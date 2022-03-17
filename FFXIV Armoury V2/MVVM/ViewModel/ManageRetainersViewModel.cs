using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ManageRetainersViewModel()
        {
            CurrentCharacter = CharacterHelper.CurrentCharacter;
            Retainers = CharacterHelper.FetchRetainers();

            RemoveRetainerCmd = new RelayCommand(async o =>
            {
                if (!(o is Inventory))
                {
                    return;
                }

                RemoveRetainer((Inventory)o);
            });

            SaveRetainerCmd = new RelayCommand(async o =>
            {
                if (!(o is Inventory))
                {
                    return;
                }

                Inventory newRetainer = (Inventory)o;
                newRetainer.Id = String.IsNullOrEmpty(newRetainer.Id) ? Guid.NewGuid().ToString() : newRetainer.Id;
                newRetainer.InvType = Inventory.InventoryType.Retainer;
                CharacterHelper.SaveRetainer(newRetainer);
            });
        }

        public void RemoveRetainer(Inventory retainer)
        {
            CharacterHelper.RemoveRetainer(retainer);
        }
    }
}
