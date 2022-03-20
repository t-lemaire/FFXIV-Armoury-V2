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
        public static ObservableCollection<InventoryItem> Items { get; set; } = new ObservableCollection<InventoryItem>();

        public static ObservableCollection<InventoryItem> FetchItems(Character selectedCharacter)
        {
            string inventoryFilePath = FileHelper.GetCharacterGearFilePath(selectedCharacter);
            string fileContents = FileHelper.ReadFile(FileHelper.GetFilePath(inventoryFilePath));

            if (String.IsNullOrEmpty(fileContents))
            {
                return new ObservableCollection<InventoryItem>();
            }

            return JsonSerializer.Deserialize<ObservableCollection<InventoryItem>>(fileContents);
        }

        public static void SaveItems(Character selectedCharacter)
        {
            string inventoryFilePath = FileHelper.GetCharacterGearFilePath(selectedCharacter);
            FileHelper.WriteFile(FileHelper.GetFilePath(inventoryFilePath), JsonSerializer.Serialize(Items));
        }

        public static void AddItem(InventoryItem newItem, Character selectedCharacter)
        {
            if (Items.Count == 0 || !Items.Contains(newItem))
            {
                Items.Add(newItem);
            }

            SaveItems(selectedCharacter);
        }
    }
}
