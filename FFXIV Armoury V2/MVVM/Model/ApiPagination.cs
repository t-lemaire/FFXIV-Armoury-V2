using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ApiPagination
    {
        public int? Page { get; set; }
        public int? PageNext { get; set; }
        public int? PagePrev { get; set; }
        public int? PageTotal { get; set; }
        public int? Results { get; set; }
        public int? ResultsPerPage { get; set; }
        public int? ResultsTotal { get; set; }
    }
}
