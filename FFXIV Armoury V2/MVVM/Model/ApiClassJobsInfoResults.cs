using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ApiClassJobsInfoResults
    {
        public ApiPagination Pagination { get; set; }
        public List<ApiClassJobInfo> Results { get; set; }
    }
}
