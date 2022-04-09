using FFXIV_Armoury_V2.Core;
using FFXIV_Armoury_V2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public RelayCommand GearListVewCommand { get; set; }
        public RelayCommand CharacterInfoVewCommand { get; set; }
        public RelayCommand CurrentCharacterViewCommand { get; set; }
        public RelayCommand SearchCharacterViewCommand { get; set; }
        public RelayCommand ManageRetainersViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }

        public GearListViewModel GearListVM { get; set; }
        public CharacterInfoViewModel CharacterInfoVM { get; set; }
        public CurrentCharacterViewModel CurrentCharacterVM { get; set; }
        public SearchCharacterViewModel SearchCharacterVM { get; set; }
        public ManageRetainersViewModel ManageRetainersVM { get; set; }
        public AboutViewModel AboutVM { get; set; }

        private object _currentView;
        private object _loginView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public object LoginView
        {
            get { return _loginView; }
            set
            {
                _loginView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CharacterHelper.CurrentCharacter = CharacterHelper.FetchCurrentCharacter();
            CharacterHelper.CharactersList = CharacterHelper.FetchCharactersList();
            CharacterHelper.RetainersList = CharacterHelper.FetchRetainers();

            //Main view port
            GearListVM = new GearListViewModel();
            CharacterInfoVM = new CharacterInfoViewModel();
            SearchCharacterVM = new SearchCharacterViewModel();
            ManageRetainersVM = new ManageRetainersViewModel();
            AboutVM = new AboutViewModel();

            CurrentView = SearchCharacterVM;
            

            GearListVewCommand = new RelayCommand(o =>
            {
                CurrentView = GearListVM;
            });

            CharacterInfoVewCommand = new RelayCommand(o =>
            {
                CurrentView = CharacterInfoVM;
            });
            SearchCharacterViewCommand = new RelayCommand(o =>
            {
                CurrentView = SearchCharacterVM;
            });
            ManageRetainersViewCommand = new RelayCommand(o =>
            {
                CurrentView = ManageRetainersVM;
            });
            AboutViewCommand = new RelayCommand(o =>
            {
                CurrentView = AboutVM;
            });

            //Logged in section
            CurrentCharacterVM = new CurrentCharacterViewModel();

            LoginView = CurrentCharacterVM;

            CurrentCharacterViewCommand = new RelayCommand(o =>
            {
                LoginView = CurrentCharacterVM;
            });

            Task.Run(async () =>
            {
                if (CharacterHelper.CurrentCharacter != null)
                {
                    Character character = await XivApiProcessor.LoadCharacter((int)CharacterHelper.CurrentCharacter.Id);

                    if (character.Name != null)
                    {
                        CharacterHelper.SaveCurrentCharacter(character);

                        OnPropertyChanged("CurrentCharacter");
                    }
                }
            });

            Task.Run(async () =>
            {
                if (CharacterHelper.CharactersList.Count > 0)
                {
                    foreach (var character in CharacterHelper.CharactersList)
                    {
                        Character foundCharacter = await XivApiProcessor.LoadCharacter((int)character.Id);

                        
                        if (foundCharacter.Name != null)
                        {
                            CharacterHelper.AddCharacterToList(foundCharacter);
                        }
                    }
                }
            });

            Task.Run(async () =>
            {
                ClassJobHelper.LoadClassJobs();
            });
        }
    }
}
