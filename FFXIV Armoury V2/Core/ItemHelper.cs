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
        public static ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

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

        public static void AddItem(Item newItem, Character selectedCharacter)
        {
            if (Items.Count == 0 || !Items.Contains(newItem))
            {
                Items.Add(newItem);
            }

            SaveItems(selectedCharacter);
        }
    }
}
