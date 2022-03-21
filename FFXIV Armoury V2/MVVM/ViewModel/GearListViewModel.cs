using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    class GearListViewModel: ObservableObject
    {
        private ObservableCollection<InventoryItem> _items;
        public ObservableCollection<InventoryItem> Items { 
            get { 
                if (_itemsCharacterId != CurrentCharacter.Id)
                {
                    _items = ItemHelper.FetchItems(CurrentCharacter);
                    _itemsCharacterId = CurrentCharacter.Id;
                }
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Item> _searchResults;
        public ObservableCollection<Item> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        private Character? _currentCharacter;

        public Character? CurrentCharacter
        {
            get { return _currentCharacter; }
            set
            {
                _currentCharacter = value;
                OnPropertyChanged();
            }
        }

        private int? _itemsCharacterId { get; set; }

        private int _currentPage;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _maxPage;

        public int MaxPage
        {
            get { return _maxPage; }
            set
            {
                _maxPage = value;
                OnPropertyChanged();
            }
        }


        private bool _isSearchEnabled;

        public bool IsSearchEnabled
        {
            get { return _isSearchEnabled; }
            set
            {
                _isSearchEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextPageEnabled;

        public bool IsNextPageEnabled
        {
            get { return _isNextPageEnabled; }
            set
            {
                _isNextPageEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isPrevPageEnabled;

        public bool IsPrevPageEnabled
        {
            get { return _isPrevPageEnabled; }
            set
            {
                _isPrevPageEnabled = value;
                OnPropertyChanged();
            }
        }

        private string? _searchTerm;

        public string? SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged();

                IsSearchEnabled = !String.IsNullOrEmpty(value);
            }
        }

        private ObservableCollection<Inventory> _retainers;

        public ObservableCollection<Inventory> Retainers
        {
            get { return _retainers; }
            set { 
                _retainers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Inventory> _currentCharacterInventories;

        public ObservableCollection<Inventory> CurrentCharacterInventories
        {
            get { 
                ObservableCollection<Inventory> ret = new ObservableCollection<Inventory>();

                Inventory armoury = new Inventory();
                armoury.Name = "Armoury";
                armoury.InvType = InventoryType.ArmouryChest;
                armoury.CharacterId = CurrentCharacter.Id;
                armoury.Id = "Armoury";
                ret.Add(armoury);

                Inventory armoire = new Inventory();
                armoire.Name = "Armoire";
                armoire.InvType = InventoryType.Armoire;
                armoury.CharacterId = CurrentCharacter.Id;
                armoury.Id = "Armoire";
                ret.Add(armoire);

                Inventory characterInventory = new Inventory();
                characterInventory.Name = "Inventory";
                characterInventory.InvType = InventoryType.CharacterInventory;
                characterInventory.CharacterId = CurrentCharacter.Id;
                characterInventory.Id = "Inventory";
                ret.Add(characterInventory);

                Inventory chocoboSaddlebag = new Inventory();
                chocoboSaddlebag.Name = "Chocobo Saddlebag";
                chocoboSaddlebag.InvType = InventoryType.ChocoboSaddlebag;
                chocoboSaddlebag.CharacterId = CurrentCharacter.Id;
                chocoboSaddlebag.Id = "Chocobo Saddlebag";
                ret.Add(chocoboSaddlebag);

                Inventory glamourDresser = new Inventory();
                glamourDresser.Name = "Glamour Dresser";
                glamourDresser.InvType = InventoryType.GlamourDresser;
                glamourDresser.CharacterId = CurrentCharacter.Id;
                glamourDresser.Id = "Glamour Dresser";
                ret.Add(glamourDresser);

                ObservableCollection<Inventory> retainers = CharacterHelper.FetchRetainers();

                if (retainers.Count > 0)
                {
                    foreach (Inventory item in retainers)
                    {
                        if (item.CharacterId == CurrentCharacter.Id)
                        {
                            ret.Add(item);
                        }
                    }
                }

                ret = new ObservableCollection<Inventory>(ret.OrderBy(x => x.Name));

                return ret;
            }
        }



        public RelayCommand SearchItemCmd { get; set; }
        public RelayCommand SearchItemNextCmd { get; set; }
        public RelayCommand SearchItemPrevCmd { get; set; }
        public RelayCommand AddItem { get; set; }

        public GearListViewModel()
        {
            //LoadGearItems();
            CurrentCharacter = CharacterHelper.CurrentCharacter;
            Items = ItemHelper.FetchItems(CurrentCharacter);
            _itemsCharacterId = CurrentCharacter.Id;
            SearchResults = new ObservableCollection<Item>();

            SearchItemCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                IsNextPageEnabled = false;
                IsPrevPageEnabled = false;
                CurrentPage = 1;
                await SearchItems(o.ToString());
                IsSearchEnabled = true;
            });

            SearchItemNextCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                IsNextPageEnabled = false;
                IsPrevPageEnabled = false;
                CurrentPage++;
                await SearchItems(o.ToString(), CurrentPage);
                IsSearchEnabled = true;
            });

            SearchItemPrevCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                IsNextPageEnabled = false;
                IsPrevPageEnabled = false;
                CurrentPage--;
                await SearchItems(o.ToString(), CurrentPage);
                IsSearchEnabled = true;
            });

            AddItem = new RelayCommand(async o =>
            {
                Item givenItem = (Item)o;
                Item selectedItem = await XivApiProcessor.LoadItem(givenItem.Id);
                //Items.Add(selectedItem);

                InventoryItem newInventoryItem = new InventoryItem();
                newInventoryItem.GearItem = selectedItem;
                newInventoryItem.GearInventory = new Inventory();

                ItemHelper.AddItem(newInventoryItem, CurrentCharacter);
                OnPropertyChanged("Items");

                //ItemHelper.SaveItems(CurrentCharacter);
            });
        }

        public async Task SearchItems(string searchTerm, int page = 1)
        {
            var searchResults = await XivApiProcessor.SearchItems(searchTerm, page);

            MaxPage = (int)searchResults.Pagination.PageTotal;
            IsNextPageEnabled = searchResults.Pagination.PageNext != null && page < MaxPage;
            IsPrevPageEnabled = searchResults.Pagination.PagePrev != null;

            SearchResults.Clear();
            foreach (var result in searchResults.Results)
            {
                SearchResults.Add(result);
            }
        }
    }
}
