using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    public  class AboutViewModel
    {
        public string AppVersion { get; set; }

        public AboutViewModel()
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
