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

        public static Character? FetchCurrentCharacter()
        {
            string fileContents = FileHelper.ReadFile(FileHelper.GetFilePath(_currentCharacterFileName));

            if (String.IsNullOrEmpty(fileContents))
            {
                return new Character();
            }

            return JsonSerializer.Deserialize<Character>(fileContents);
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
            }
        }

        public static async void RemoveCharacterToList(Character character)
        {
            var nbCharactersInList = charactersList.Count(i => i.Id == character.Id);

            if (nbCharactersInList != null && nbCharactersInList > 0)
            {
                Character selectedCharacter = charactersList.Where(i => i.Id == character.Id).First();
                charactersList.Remove(selectedCharacter);
                SaveCharactersList();
            }
        }

        public static void SaveCharactersList()
        {
            string filePath = FileHelper.GetCharactersListFilePath();

            FileHelper.WriteFile(FileHelper.GetFilePath(filePath), JsonSerializer.Serialize(charactersList));
        }
    }
}
