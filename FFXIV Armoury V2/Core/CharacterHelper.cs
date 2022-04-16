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
    public static class CharacterHelper
    {
        private const string _currentCharacterFileName = "CurrentCharacter.json";
        private static Character character = new Character();
        private static ObservableCollection<Character> charactersList = new ObservableCollection<Character>();
        private static ObservableCollection<Inventory> retainersList = new ObservableCollection<Inventory>();

        public static Character CurrentCharacter
        {
            get { return character; }
            set { character = value; }
        }

        public static ObservableCollection<Character> CharactersList
        {
            get { return charactersList; }
            set { charactersList = value; }
        }

        public static ObservableCollection<Inventory> RetainersList
        {
            get { return retainersList; }
            set { retainersList = value; }
        }

        public static Character? FetchCurrentCharacter()
        {
            string fileContents = FileHelper.ReadFile(FileHelper.GetFilePath(_currentCharacterFileName));

            if (String.IsNullOrEmpty(fileContents))
            {
                return new Character();
            }

            try
            {
                return JsonSerializer.Deserialize<Character>(fileContents);
            } catch (Exception ex)
            {
                Log.Write(Log.LogEvent.Critical, $"An error occurred while loading current character file from its contents.{ex.Message} -> {ex.StackTrace}");
                return null;
            }
        }

        public static ObservableCollection<Character>? FetchCharactersList()
        {
            string filePath = FileHelper.GetCharactersListFilePath();

            string fileContents = FileHelper.ReadFile(FileHelper.GetFilePath(filePath));

            if (String.IsNullOrEmpty(fileContents))
            {
                return new ObservableCollection<Character>();
            }

            return JsonSerializer.Deserialize<ObservableCollection<Character>>(fileContents);
        }

        public static async void SaveCurrentCharacter(ApiCharacterSearchResultProfile characterProfile)
        {
            Character character = await XivApiProcessor.LoadCharacter(characterProfile.Id);
            
            SaveCurrentCharacter(character);
        }

        public static async void SaveCurrentCharacter(Character character)
        {
            CurrentCharacter.Avatar = character.Avatar;
            CurrentCharacter.Name = character.Name;
            CurrentCharacter.Server = character.Server;
            CurrentCharacter.Dc = character.Dc;
            CurrentCharacter.Id = character.Id;
            CurrentCharacter.FreeCompanyName = character.FreeCompanyName;
            CurrentCharacter.Portrait = character.Portrait;
            CurrentCharacter.ClassJobs = character.ClassJobs;
            CurrentCharacter.ActiveClassJob = character.ActiveClassJob;

            FileHelper.WriteFile(FileHelper.GetFilePath(_currentCharacterFileName), JsonSerializer.Serialize(character));

            AddCharacterToList(character);
        }

        public static async void AddCharacterToList(Character character)
        {
            var nbCharactersInList = charactersList.Count(i => i.Id == character.Id);

            if (nbCharactersInList != null && nbCharactersInList == 0)
            {
                charactersList.Add(character);
                SaveCharactersList();
            } else
            {
                Character foundCharacter = charactersList.FirstOrDefault(i => i.Id == character.Id);

                if (foundCharacter != null)
                {
                    foundCharacter.Avatar = character.Avatar;
                    foundCharacter.Name = character.Name;
                    foundCharacter.Server = character.Server;
                    foundCharacter.Dc = character.Dc;
                    foundCharacter.Id = character.Id;
                    foundCharacter.FreeCompanyName = character.FreeCompanyName;
                    foundCharacter.Portrait = character.Portrait;
                    foundCharacter.ClassJobs = character.ClassJobs;
                    foundCharacter.ActiveClassJob = character.ActiveClassJob;

                    SaveCharactersList();
                }
            }
        }

        public static async void RemoveCharacterFromList(Character character)
        {
            var nbCharactersInList = charactersList.Count(i => i.Id == character.Id);

            if (nbCharactersInList != null && nbCharactersInList > 0)
            {
                Character selectedCharacter = charactersList.Where(i => i.Id == character.Id).First();
                charactersList.Remove(selectedCharacter);

                if (CurrentCharacter != null && character.Id == CurrentCharacter.Id)
                {
                    CurrentCharacter.Avatar = null;
                    CurrentCharacter.Name = null;
                    CurrentCharacter.Server = null;
                    CurrentCharacter.Dc = null;
                    CurrentCharacter.Id = null;
                    CurrentCharacter.FreeCompanyName = null;
                    CurrentCharacter.Portrait = null;
                    CurrentCharacter.ClassJobs = null;
                    CurrentCharacter.ActiveClassJob = null;
                    FileHelper.DeleteFile(FileHelper.GetFilePath(_currentCharacterFileName));
                }

                SaveCharactersList();
            }
        }

        public static void SaveCharactersList()
        {
            string filePath = FileHelper.GetCharactersListFilePath();

            if (charactersList.Count > 0)
            {
                FileHelper.WriteFile(FileHelper.GetFilePath(filePath), JsonSerializer.Serialize(charactersList));
            } else
            {
                FileHelper.DeleteFile(filePath);
            }
        }

        public static ObservableCollection<Inventory> FetchRetainers()
        {
            string filePath = FileHelper.GetRetainersListFilePath();
            string fileContents = FileHelper.ReadFile(FileHelper.GetFilePath(filePath));

            if (String.IsNullOrEmpty(fileContents))
            {
                return new ObservableCollection<Inventory>();
            }

            return JsonSerializer.Deserialize<ObservableCollection<Inventory>>(fileContents);
        }

        public static void SaveRetainersList()
        {
            string filePath = FileHelper.GetRetainersListFilePath();

            FileHelper.WriteFile(FileHelper.GetFilePath(filePath), JsonSerializer.Serialize(retainersList));
        }

        public static void SaveRetainer(Inventory retainer)
        {
            if (retainer.InvType != InventoryType.Retainer)
            {
                throw new ArgumentException("Inventory type must be a retainer.");
            }

            Inventory existingRetainer = retainersList.Where(o => o.InvType == InventoryType.Retainer && o.Id == retainer.Id).FirstOrDefault();
            if (existingRetainer != null)
            {
                existingRetainer = retainer;
            } else
            {
                retainersList.Add(retainer);
            }

            Task.Run(() =>
            {
                SaveRetainersList();
            });
        }

        public static void RemoveRetainer(Inventory retainer)
        {
            int nbRetainers = retainersList.Count(i => i.Id == retainer.Id && i.InvType == retainer.InvType);
            if (nbRetainers != null && nbRetainers > 0)
            {
                Inventory selectedRetainer = retainersList.Where(i => i.Id == retainer.Id && i.InvType == retainer.InvType).First();
                retainersList.Remove(selectedRetainer);
                SaveRetainersList();
            }
        }
    }
}
