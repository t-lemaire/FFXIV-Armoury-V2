using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    public class CurrentCharacterViewModel: ObservableObject
    {
        private Character? _currentCharacter;

        public Character? CurrentCharacter
        {
            get { return _currentCharacter; }
            set { 
                _currentCharacter = value;
                OnPropertyChanged();
            }
        }

        public CurrentCharacterViewModel()
        {
            CurrentCharacter = CharacterHelper.CurrentCharacter;
        }
    }
}
