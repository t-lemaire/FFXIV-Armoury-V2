using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
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

        public static async Task<ApiItemSearchResult> SearchItems(string searchText, int page = 1)
        {
            searchText = HttpUtility.UrlEncode(searchText.Trim());

            if (page < 1)
            {
                page = 1;
            }

            int perPage = 100;
            int from = (page - 1) * perPage;

            string url = $"{_baseUrl}/search";
            string content = $"{{\"indexes\":\"item\",\"columns\":\"\",\"page\":{page},\"body\":{{\"query\":{{\"bool\":{{\"must\":[{{\"wildcard\":{{\"NameCombined_en\":\"*{searchText}*\"}}}}],\"filter\":[{{\"bool\":{{\"should\":[{{\"term\":{{\"EquipSlotCategory.Body\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Ears\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Feet\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.FingerL\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.FingerR\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Gloves\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Head\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Legs\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.MainHand\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Neck\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.OffHand\":{{\"value\":\"1\"}}}}}},{{\"term\":{{\"EquipSlotCategory.Wrists\":{{\"value\":\"1\"}}}}}}]}}}}]}}}},\"from\":{from},\"size\":{perPage}}}}}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var responses = await ApiHelper.ApiClient.SendAsync(request).ConfigureAwait(false);
            responses.EnsureSuccessStatusCode();

            return await responses.Content.ReadAsAsync<ApiItemSearchResult>();
        }

        public static async Task<Item> LoadItem(int lodestoneId)
        {
            string url = $"{_baseUrl}/item/{lodestoneId}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Item selectedItem = await response.Content.ReadAsAsync<Item>();

                    return selectedItem;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ApiClassJobInfo>> LoadClassJobs(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            string url = $"{_baseUrl}/classjob?page={page}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ApiClassJobsInfoResults loadedClassJobsInfo = await response.Content.ReadAsAsync<ApiClassJobsInfoResults>();

                    return loadedClassJobsInfo.Results;

                    //return new List<ApiClassJobInfo>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
