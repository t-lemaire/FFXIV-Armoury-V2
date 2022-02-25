using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
    }
}
