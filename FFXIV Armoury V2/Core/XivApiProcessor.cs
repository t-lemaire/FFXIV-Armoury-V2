using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFXIV_Armoury_V2.Core
{
    public class XivApiProcessor
    {
        private static string _baseUrl { get; set; } = "https://xivapi.com";
        public static async Task<Character> LoadCharacter(int lodestoneId)
        {
            string url = $"{_baseUrl}/character/{lodestoneId}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ApiCharacterResult loadedCharacterResult = await response.Content.ReadAsAsync<ApiCharacterResult>();

                    return loadedCharacterResult.Character;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ApiCharacterSearchResult> SearchCharacters(string searchText, int page=1)
        {
            searchText = HttpUtility.UrlEncode(searchText.Trim());

            if (page < 1)
            {
                page = 1;
            }

            string url = $"{_baseUrl}/character/search?name={searchText}&page={page}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ApiCharacterSearchResult loadedCharacterResult = await response.Content.ReadAsAsync<ApiCharacterSearchResult>();

                    return loadedCharacterResult;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
