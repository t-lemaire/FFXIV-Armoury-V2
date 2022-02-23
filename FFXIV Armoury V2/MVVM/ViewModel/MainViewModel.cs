using FFXIV_Armoury_V2.Core;
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

        public GearListViewModel GearListVM { get; set; }
        public CharacterInfoViewModel CharacterInfoVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            GearListVM = new GearListViewModel();
            CharacterInfoVM = new CharacterInfoViewModel();

            CurrentView = GearListVM;

            GearListVewCommand = new RelayCommand(o =>
            {
                CurrentView = GearListVM;
            });

            CharacterInfoVewCommand = new RelayCommand(o =>
            {
                CurrentView = CharacterInfoVM;
            });
        }
    }
}
