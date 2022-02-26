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

        public GearListViewModel GearListVM { get; set; }
        public CharacterInfoViewModel CharacterInfoVM { get; set; }
        public CurrentCharacterViewModel CurrentCharacterVM { get; set; }
        public SearchCharacterViewModel SearchCharacterVM { get; set; }

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
            //Main view port
            GearListVM = new GearListViewModel();
            CharacterInfoVM = new CharacterInfoViewModel();
            SearchCharacterVM = new SearchCharacterViewModel();

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

            //Logged in section
            CurrentCharacterVM = new CurrentCharacterViewModel();

            LoginView = CurrentCharacterVM;

            CurrentCharacterViewCommand = new RelayCommand(o =>
            {
                LoginView = CurrentCharacterVM;
            });
        }
    }
}
