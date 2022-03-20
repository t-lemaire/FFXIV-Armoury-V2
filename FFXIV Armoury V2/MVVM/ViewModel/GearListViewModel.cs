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
            get { return _items; }
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

        public RelayCommand SearchItemCmd { get; set; }
        public RelayCommand SearchItemNextCmd { get; set; }
        public RelayCommand SearchItemPrevCmd { get; set; }
        public RelayCommand AddItem { get; set; }

        public GearListViewModel()
        {
            //LoadGearItems();
            CurrentCharacter = CharacterHelper.CurrentCharacter;
            Items = ItemHelper.FetchItems(CurrentCharacter);
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
