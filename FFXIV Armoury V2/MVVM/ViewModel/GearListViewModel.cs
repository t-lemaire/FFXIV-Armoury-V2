using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIV_Armoury_V2.MVVM.Model;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    class GearListViewModel
    {
        public ObservableCollection<GearItem> GearItems { get; set; }

        public GearListViewModel()
        {
            LoadGearItems();
        }

        private void LoadGearItems()
        {
            GearItems = new ObservableCollection<GearItem>();
            //ObservableCollection<GearItem> gearItems = new ObservableCollection<GearItem>();

            GearItem gearItem1 = new GearItem("Staff", 50);
            GearItem gearItem2 = new GearItem("Helmet", 1);
            GearItem gearItem3 = new GearItem("Ring", 35);

            GearItems.Add(gearItem1);
            GearItems.Add(gearItem2);
            GearItems.Add(gearItem3);
        }
    }
}
