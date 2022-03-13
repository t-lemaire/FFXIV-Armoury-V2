using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    public class SearchCharacterViewModel: ObservableObject
    {
        public ObservableCollection<ApiCharacterSearchResultProfile> SearchResults{ get; set; } = new ObservableCollection<ApiCharacterSearchResultProfile>();
        private int _currentPage;

        public int CurrentPage
        {
            get { return _currentPage; }
            set { 
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _maxPage;

        public int MaxPage
        {
            get { return _maxPage; }
            set { 
                _maxPage = value;
                OnPropertyChanged();
            }
        }


        private bool _isSearchEnabled;

        public bool IsSearchEnabled
        {
            get { return _isSearchEnabled; }
            set { 
                _isSearchEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextPageEnabled;

        public bool IsNextPageEnabled
        {
            get { return _isNextPageEnabled; }
            set { 
                _isNextPageEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isPrevPageEnabled;

        public bool IsPrevPageEnabled
        {
            get { return _isPrevPageEnabled; }
            set { 
                _isPrevPageEnabled = value;
                OnPropertyChanged();
            }
        }



        public RelayCommand SearchCharacterCmd { get; set; }
        public RelayCommand SearchCharacterNextCmd { get; set; }
        public RelayCommand SearchCharacterPrevCmd { get; set; }
        public RelayCommand SelectCurrentCharacter { get; set; }

        public SearchCharacterViewModel()
        {
            CurrentPage = 1;
            IsSearchEnabled = true;
            IsNextPageEnabled = false;

            SearchCharacterCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                CurrentPage = 1;
                await SearchCharacters(o.ToString());
                IsSearchEnabled = true;
            });

            SearchCharacterNextCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                CurrentPage++;
                await SearchCharacters(o.ToString(), CurrentPage);
                IsSearchEnabled = true;
            });

            SearchCharacterPrevCmd = new RelayCommand(async o =>
            {
                IsSearchEnabled = false;
                CurrentPage--;
                await SearchCharacters(o.ToString(), CurrentPage);
                IsSearchEnabled = true;
            });

            SelectCurrentCharacter = new RelayCommand(o =>
            {
                SelectCharacter((ApiCharacterSearchResultProfile)o);
            });
        }

        public async Task SearchCharacters(string searchTerm, int page=1)
        {
            var searchResults = await XivApiProcessor.SearchCharacters(searchTerm, page);

            IsNextPageEnabled = searchResults.Pagination.PageNext != null;
            IsPrevPageEnabled = searchResults.Pagination.PagePrev != null;
            MaxPage = (int)searchResults.Pagination.PageTotal;

            SearchResults.Clear();
            foreach (var result in searchResults.Results)
            {
                SearchResults.Add(result);
            }
        }

        private async void SelectCharacter(ApiCharacterSearchResultProfile character)
        {
            /*string json = JsonConvert.SerializeObject(character);

            string filePath = FileHelper.GetCurrentCharacterFilePath();
            await FileHelper.WriteFile(filePath, json);*/
            CharacterHelper.SaveCurrentCharacter(character);
        }
    }
}
