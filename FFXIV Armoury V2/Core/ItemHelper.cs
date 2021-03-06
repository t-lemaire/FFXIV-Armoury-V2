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
                Items.Clear();
                return Items;
            }

            ObservableCollection<InventoryItem>? TmpItems = new ObservableCollection<InventoryItem>();

            try
            {
                TmpItems = JsonSerializer.Deserialize<ObservableCollection<InventoryItem>>(fileContents);

                if (TmpItems != null)
                {
                    Items.Clear();
                    
                    foreach (InventoryItem item in TmpItems)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(Log.LogEvent.Critical, $"An error occurred while loading character's inventory file's contents.{ex.Message} -> {ex.StackTrace}");
                return new ObservableCollection<InventoryItem>();
            }

            return Items;
        }

        public static void SaveItems(Character selectedCharacter)
        {
            string inventoryFilePath = FileHelper.GetCharacterGearFilePath(selectedCharacter);
            
            try
            {
                FileHelper.WriteFile(FileHelper.GetFilePath(inventoryFilePath), JsonSerializer.Serialize(Items));
            }
            catch (Exception ex)
            {
                Log.Write(Log.LogEvent.Critical, $"An error occurred while saving character's inventory file's contents.{ex.Message} -> {ex.StackTrace}");
            }
        }

        public static void AddItem(InventoryItem newItem, Character selectedCharacter)
        {
            if (Items.Count == 0 || !Items.Contains(newItem))
            {
                Items.Add(newItem);
            }

            SaveItems(selectedCharacter);
        }

        public static void RemoveItem(InventoryItem item, Character selectedCharacter)
        {
            if (Items.Count > 0 || Items.Contains(item))
            {
                Items.Remove(item);
            }

            SaveItems(selectedCharacter);
        }

        public static void ClearInventory(Character selectedCharacter)
        {
            if (CharacterHelper.CurrentCharacter != null && selectedCharacter.Id == CharacterHelper.CurrentCharacter.Id)
            {
                Items.Clear();
            }

            FileHelper.DeleteFile(FileHelper.GetFilePath(FileHelper.GetCharacterGearFilePath(selectedCharacter)));
        }
    }
}
