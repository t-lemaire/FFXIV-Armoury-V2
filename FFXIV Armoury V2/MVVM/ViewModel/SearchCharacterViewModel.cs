using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    public class SearchCharacterViewModel: ObservableObject
    {
        public ObservableCollection<ApiCharacterSearchResultProfile> SearchResults{ get; set; } = new ObservableCollection<ApiCharacterSearchResultProfile>();

        public RelayCommand SearchCharacterCmd { get; set; }

        public SearchCharacterViewModel()
        {
            SearchCharacterCmd = new RelayCommand(async o =>
            {
                await SearchCharacters(o.ToString());
            });
        }

        public async Task SearchCharacters(string searchTerm)
        {
            SearchResults.Clear();
            var searchResults = await XivApiProcessor.SearchCharacters(searchTerm);

            foreach (var result in searchResults.Results)
            {
                SearchResults.Add(result);
            }
        }
    }
}
