using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    public class CurrentCharacterViewModel
    {
        public Character CurrentCharacter { get; set; }

        public CurrentCharacterViewModel()
        {

        }

        private async Task LoadCurrentCharacter(int lodestoneId)
        {
            var character = await XivApiProcessor.LoadCharacter(lodestoneId);

            CurrentCharacter = character;
        }
    }
}
