using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.Core
{
    public static class GithubHelper
    {
        public static async Task<GitHubRelease> GetLatestRelease()
        {
            string url = "https://api.github.com/repos/t-lemaire/FFXIV-Armoury-V2/releases/latest";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    GitHubRelease releaseInfo = await response.Content.ReadAsAsync<GitHubRelease>();

                    return releaseInfo;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
