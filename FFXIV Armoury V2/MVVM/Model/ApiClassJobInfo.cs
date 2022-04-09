using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class ApiClassJobInfo
    {
        public int Id { get; set; }
        /*public string Name { 
            get
            {
                if (Name == null)
                {
                    return "";
                } else
                {
                    return char.ToUpper(Name.ToLower()[0]).ToString();
                }
            }

            set { Name = value; }
        }*/

        public string Name { get; set; }
    }
}
