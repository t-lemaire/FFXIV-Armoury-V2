using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.Core
{
    public static class ItemHelper
    {
        private static ObservableCollection<Item> _items;

        public static ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public static ObservableCollection<Item> FetchItems(Character selectedCharacter)
        {
            string inventoryFilePath = FileHelper.GetCharacterGearFilePath(selectedCharacter);
            string fileContents = FileHelper.ReadFile(FileHelper.GetFilePath(inventoryFilePath));

            if (String.IsNullOrEmpty(fileContents))
            {
                return new ObservableCollection<Item>();
            }

            return JsonSerializer.Deserialize<ObservableCollection<Item>>(fileContents);
        }

        public static void SaveItems(Character selectedCharacter)
        {
            string inventoryFilePath = FileHelper.GetCharacterGearFilePath(selectedCharacter);
            FileHelper.WriteFile(FileHelper.GetFilePath(inventoryFilePath), JsonSerializer.Serialize(Items));
        }
    }
}
