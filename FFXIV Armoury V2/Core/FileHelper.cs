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
    }
}
