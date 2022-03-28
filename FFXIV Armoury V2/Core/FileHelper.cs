using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.Core
{
    public static class FileHelper
    {
        private static string GetBasePath()
        {
            return Path.GetDirectoryName(Path.Combine(@Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)), "FFXIV Armory Manager", "test.json"));
        }

        public static string GetFilePath(string filename)
        {
            return Path.Combine(GetBasePath(), filename);
        }

        public static string GetCurrentCharacterFilePath()
        {
            return GetFilePath("CurrentCharacter.json");
        }

        public static string GetCharactersListFilePath()
        {
            return GetFilePath("CharactersList.json");
        }

        public static string GetRetainersListFilePath()
        {
            return GetFilePath("Retainers.json");
        }
        
        public static string GetCharacterGearFilePath(Character selectedCharacter)
        {
            return GetFilePath($"GearList_{selectedCharacter.Id}.json");
        }

        public static string GetCharacterGearFilePath(int characterId)
        {
            return GetFilePath($"GearList_{characterId}.json");
        }

        public async static Task WriteFile(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }

        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "";
            }

            return File.ReadAllText(filePath);
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
