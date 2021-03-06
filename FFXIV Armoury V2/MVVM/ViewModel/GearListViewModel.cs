using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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

        private bool _showLowLevelItems;

        public bool ExcludeHighLevelGearItems
        {
            get { return _showLowLevelItems; }
            set { 
                _showLowLevelItems = value;
                OnPropertyChanged();
            }
        }

        private bool _showOnlyEquipable;

        public bool ShowOnlyEquipable
        {
            get { return _showOnlyEquipable; }
            set { 
                _showOnlyEquipable = value;
                OnPropertyChanged();
            }
        }



        private Visibility _searchProgressBarVisibility;

        public Visibility SearchProgressBarVisibility
        {
            get { return _searchProgressBarVisibility; }
            set
            {
                _searchProgressBarVisibility = value;
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

        private Inventory? _defaultInventory;

        public Inventory? DefaultInventory
        {
            get { return _defaultInventory; }
            set { 
                _defaultInventory = value;
                OnPropertyChanged();
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

        public int GearItemNumberJobRows
        {
            get
            {
                if (CurrentCharacter == null)
                {
                    return 1;
                } else
                {
                    return (int)Math.Floor((double)CurrentCharacter.ClassJobs.Count());
                }
            }
        }

        public int GearItemNumberJobColumns
        {
            get
            {
                if (CurrentCharacter == null)
                {
                    return 1;
                }
                else
                {
                    return (int)Math.Ceiling((double)(CurrentCharacter.ClassJobs.Count() / GearItemNumberJobRows));
                }
            }
        }

        private string _gearSearchString;

        public string GearSearchString
        {
            get { return _gearSearchString; }
            set { 
                _gearSearchString = value;
                OnPropertyChanged();
                FilteredItemsList.Filter = FilterGearList;
            }
        }


        private ObservableCollection<ApiClassJobInfo> _apiClassJobs;

        public ObservableCollection<ApiClassJobInfo> ApiClassJobs
        {
            get { return _apiClassJobs; }
            set { 
                _apiClassJobs = value;
                OnPropertyChanged();
                OnPropertyChanged("ClassJobsInfo");
            }
        }


        private ObservableCollection<ClassJob> _classJobsInfo;

        public ObservableCollection<ClassJob> ClassJobsInfo
        {
            get {
                ObservableCollection <ClassJob> cji = new ObservableCollection<ClassJob>();
                if (_apiClassJobs != null && _apiClassJobs.Count > 0)
                {
                    foreach (ApiClassJobInfo classJobInfo in _apiClassJobs)
                    {
                        cji.Add(ClassJob.InstanciateClassJob(classJobInfo));
                    }
                }

                return cji;
            }
            set { 
                _classJobsInfo = value;
                OnPropertyChanged();
                OnPropertyChanged("TankClassJobs");
                OnPropertyChanged("HealerClassJobs");
                OnPropertyChanged("MeeleeDpsClassJobs");
                OnPropertyChanged("RangedDpsClassJobs");
                OnPropertyChanged("CasterDpsClassJobs");
                OnPropertyChanged("CrafterClassJobs");
                OnPropertyChanged("GathererClassJobs");
            }
        }

        public ObservableCollection<ClassJob> TankClassJobs {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                } else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsTank));
                }
            }
        }

        public ObservableCollection<ClassJob> HealerClassJobs
        {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                }
                else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsHealer));
                }
            }
        }

        public ObservableCollection<ClassJob> MeeleeDpsClassJobs
        {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                }
                else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsMeeleeDps));
                }
            }
        }

        public ObservableCollection<ClassJob> RangedDpsClassJobs
        {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                }
                else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsPhysicalRangedDps));
                }
            }
        }

        public ObservableCollection<ClassJob> CasterDpsClassJobs
        {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                }
                else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsMagicalRangedDps));
                }
            }
        }

        public ObservableCollection<ClassJob> CrafterClassJobs
        {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                }
                else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsCrafter));
                }
            }
        }

        public ObservableCollection<ClassJob> GathererClassJobs
        {
            get
            {
                if (ClassJobsInfo == null || ClassJobsInfo.Count == 0)
                {
                    return new ObservableCollection<ClassJob>();
                }
                else
                {
                    return new ObservableCollection<ClassJob>(ClassJobsInfo.Where(c => c.IsGatherer));
                }
            }
        }

        private ICollectionView _filteredItemsList;

        public ICollectionView FilteredItemsList
        {
            get { return _filteredItemsList; }
            set { 
                _filteredItemsList = value;
                OnPropertyChanged();
            }
        }



        public RelayCommand SearchItemCmd { get; set; }
        public RelayCommand SearchItemNextCmd { get; set; }
        public RelayCommand SearchItemPrevCmd { get; set; }
        public RelayCommand AddItem { get; set; }
        public RelayCommand RemoveItem { get; set; }
        public RelayCommand SaveItems { get; set; }
        public RelayCommand ToggleLowLevelGear { get; set; }
        public RelayCommand ToggleEquipableGear { get; set; }

        public RelayCommand ToggleJobFilterCommand { get; set; }
        public ObservableCollection<int> JobFilter { get; set; } = new ObservableCollection<int>();

        public GearListViewModel()
        {
            //LoadGearItems();
            CurrentCharacter = CharacterHelper.CurrentCharacter;
            Items = ItemHelper.FetchItems(CurrentCharacter);
            _itemsCharacterId = CurrentCharacter.Id;
            SearchResults = new ObservableCollection<Item>();
            SearchProgressBarVisibility = Visibility.Collapsed;
            ExcludeHighLevelGearItems = false;
            ShowOnlyEquipable = false;

            SearchItemCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                IsNextPageEnabled = false;
                IsPrevPageEnabled = false;
                CurrentPage = 1;
                SearchProgressBarVisibility = Visibility.Visible;
                await SearchItems(o.ToString());
                SearchProgressBarVisibility = Visibility.Collapsed;
                IsSearchEnabled = true;
            });

            SearchItemNextCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                IsNextPageEnabled = false;
                IsPrevPageEnabled = false;
                CurrentPage++;
                SearchProgressBarVisibility = Visibility.Visible;
                await SearchItems(o.ToString(), CurrentPage);
                SearchProgressBarVisibility = Visibility.Collapsed;
                IsSearchEnabled = true;
            });

            SearchItemPrevCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                IsNextPageEnabled = false;
                IsPrevPageEnabled = false;
                CurrentPage--;
                SearchProgressBarVisibility = Visibility.Visible;
                await SearchItems(o.ToString(), CurrentPage);
                SearchProgressBarVisibility = Visibility.Collapsed;
                IsSearchEnabled = true;
            });

            AddItem = new RelayCommand(async o =>
            {
                Item givenItem = (Item)o;
                Item selectedItem = await XivApiProcessor.LoadItem(givenItem.Id);

                InventoryItem newInventoryItem = new InventoryItem();
                newInventoryItem.GearItem = selectedItem;
                newInventoryItem.GearInventory = new Inventory();

                if (DefaultInventory != null)
                {
                    newInventoryItem.GearInventory = DefaultInventory;
                }

                ItemHelper.AddItem(newInventoryItem, CurrentCharacter);
                OnPropertyChanged("Items");
            });

            RemoveItem = new RelayCommand(async o =>
            {
                InventoryItem item = (InventoryItem)o;

                ItemHelper.RemoveItem(item, CurrentCharacter);
                OnPropertyChanged("Items");
            });

            SaveItems = new RelayCommand(async o =>
            {
                ItemHelper.SaveItems(CurrentCharacter);
                OnPropertyChanged("Items");
            });

            ApiClassJobs = ClassJobHelper.ClassJobsInfo;

            ToggleJobFilterCommand = new RelayCommand(async o =>
            {
                int jobId = Convert.ToInt32(o);
                if (JobFilter.Contains(jobId))
                {
                    JobFilter.Remove(jobId);
                } else
                {
                    JobFilter.Add(jobId);
                }

                FilteredItemsList.Filter = FilterGearList;
            });

            ToggleLowLevelGear = new RelayCommand(async o =>
            {
                ExcludeHighLevelGearItems = !ExcludeHighLevelGearItems;

                FilteredItemsList.Filter = FilterGearList;
            });

            ToggleEquipableGear = new RelayCommand(async o =>
            {
                ShowOnlyEquipable = !ShowOnlyEquipable;

                FilteredItemsList.Filter = FilterGearList;
            });

            FilteredItemsList = CollectionViewSource.GetDefaultView(Items);
            FilteredItemsList.Filter = FilterGearList;
            FilteredItemsList.SortDescriptions.Add(new SortDescription("GearItem.EquipSlotCategory.SlotName", ListSortDirection.Ascending));
            FilteredItemsList.SortDescriptions.Add(new SortDescription("GearItem.Name", ListSortDirection.Ascending));
            FilteredItemsList.SortDescriptions.Add(new SortDescription("GearInventory.NameLabel", ListSortDirection.Ascending));

            using (FilteredItemsList.DeferRefresh())
            {
                FilteredItemsList.GroupDescriptions.Clear();
                FilteredItemsList.GroupDescriptions.Add(new PropertyGroupDescription("GearItem.EquipSlotCategory.SlotName"));
            }
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

        private bool FilterGearList(object gItem)
        {
            InventoryItem? gearItem = gItem as InventoryItem;

            if (gearItem == null)
            {
                return true;
            }

            bool jobFilterFound = false;
            if (JobFilter != null && JobFilter.Count > 0)
            {
                foreach (ClassJob cj in gearItem.GearItem.AvailableJobs)
                {
                    if (JobFilter.Contains((int)cj.ClassId))
                    {
                        jobFilterFound = true;
                        break;
                    }
                }
            } else
            {
                jobFilterFound = true;
            }

            if (!jobFilterFound)
            {
                return false;
            }

            bool nameMatchesSearchString = false;
            if (String.IsNullOrEmpty(GearSearchString))
            {
                nameMatchesSearchString = true;
            } else
            {
                nameMatchesSearchString = gearItem.GearItem.Name.Contains(GearSearchString, StringComparison.OrdinalIgnoreCase);
            }

            if (!nameMatchesSearchString)
            {
                return false;
            }

            bool showBasedOnLevel = true;
            if (ExcludeHighLevelGearItems && gearItem.GearItem != null && CurrentCharacter.ClassJobs != null)
            {
                if (gearItem.GearItem.AvailableJobs.Count > 0)
                {
                    bool hasHigherLevel = false;
                    
                    foreach (ClassJob itemJob in gearItem.GearItem.AvailableJobs)
                    {
                        foreach (ClassJob characterJob in CurrentCharacter.ClassJobs)
                        {
                            if (itemJob.ClassId == characterJob.ClassId && gearItem.GearItem.LevelEquip >= characterJob.Level)
                            {
                                hasHigherLevel = true;
                                showBasedOnLevel = false;
                                break;
                            }
                        }

                        if (hasHigherLevel)
                        {
                            break;
                        }
                    }
                }
            }

            if (!showBasedOnLevel)
            {
                return false;
            }

            bool showBasedOnEquipable = true;
            if (ShowOnlyEquipable)
            {
                showBasedOnEquipable = false;
                foreach (ClassJob cj in gearItem.GearItem.AvailableJobs)
                {
                    if (JobFilter != null && JobFilter.Count > 0 && !JobFilter.Contains((int)cj.ClassId))
                    {
                        continue;
                    }

                    foreach (ClassJob characterJob in CurrentCharacter.ClassJobs)
                    {
                        if (cj.ClassId == characterJob.ClassId && gearItem.GearItem.LevelEquip <= characterJob.Level)
                        {
                            showBasedOnEquipable = true;
                            break;
                        }
                    }
                }
            }

            if (!showBasedOnEquipable)
            {
                return false;
            }

            return true;
        }
    }
}
