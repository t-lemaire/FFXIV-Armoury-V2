using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ApiItemSearchResult
    {
        public List<Item> Results { get; set; }
        public ApiPagination Pagination { get; set; }
    }
}
