using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
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

        public static Character CurrentCharacter
        {
            get { return character; }
            set { character = value; }
        }


        public static Character? FetchCurrentCharacter()
        {
            return JsonSerializer.Deserialize<Character>(FileHelper.ReadFile(FileHelper.GetFilePath(_currentCharacterFileName)));
        }

        public static async void SaveCurrentCharacter(ApiCharacterSearchResultProfile characterProfile)
        {
            Character character = await XivApiProcessor.LoadCharacter(characterProfile.Id);
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
        }

        public static void SaveCurrentCharacter(Character character)
        {
            FileHelper.WriteFile(FileHelper.GetFilePath(_currentCharacterFileName),JsonSerializer.Serialize(character));
        }
    }
}
