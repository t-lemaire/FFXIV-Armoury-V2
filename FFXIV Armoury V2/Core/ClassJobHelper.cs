using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.Core
{
    public static class ClassJobHelper
    {
        private static ObservableCollection<ApiClassJobInfo> _classJobsInfo;

        public static ObservableCollection<ApiClassJobInfo> ClassJobsInfo
        {
            get { return _classJobsInfo; }
            set { _classJobsInfo = value; }
        }

        public static async void LoadClassJobs()
        {
            List<ApiClassJobInfo> cji = await XivApiProcessor.LoadClassJobs();

            if (ClassJobsInfo != null)
            {
                ClassJobsInfo.Clear();
            } else
            {
                ClassJobsInfo = new ObservableCollection<ApiClassJobInfo>();
            }

            if (cji != null && cji.Count > 0)
            {
                foreach (ApiClassJobInfo c in cji)
                {
                    ClassJobsInfo.Add(c);
                }
            }
        }
    }
}
