using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.MVVM.Model
{
    public class Character: ObservableObject
    {


        private string? _avatar;

        public string? Avatar
        {
            get { return _avatar; }
            set { 
                _avatar = value;
                OnPropertyChanged();
            }
        }

        private string? _server;

        public string? Server
        {
            get { return _server; }
            set { 
                _server = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ServerDc));
            }
        }

        private string? _dc;

        public string? Dc
        {
            get { return _dc; }
            set { 
                _dc = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ServerDc));
            }
        }

        private string? _freeCompanyName;

        public string? FreeCompanyName
        {
            get { return _freeCompanyName; }
            set { 
                _freeCompanyName = value;
                OnPropertyChanged();
            }
        }

        private int? _id;

        public int? Id
        {
            get { return _id; }
            set { 
                _id = value;
                OnPropertyChanged();
            }
        }

        private string? _name;

        public string? Name
        {
            get { return _name; }
            set { 
                _name = value;
                OnPropertyChanged();
            }
        }

        private int? _gender;

        public int? Gender
        {
            get { return _gender; }
            set { 
                _gender = value;
                OnPropertyChanged();
            }
        }

        private string? _portrait;

        public string? Portrait
        {
            get { return _portrait; }
            set { 
                _portrait = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ClassJob>? _classJobs;

        public ObservableCollection<ClassJob>? ClassJobs
        {
            get { return _classJobs; }
            set {
                _classJobs = value;
                OnPropertyChanged();
            }
        }

        private ClassJob? _activeClassJob;

        public ClassJob? ActiveClassJob
        {
            get { return _activeClassJob; }
            set {
                _activeClassJob = value;
                OnPropertyChanged();
            }
        }


        public string ServerDc
        {
            get
            {
                if (!String.IsNullOrEmpty(Server) && !String.IsNullOrEmpty(Dc))
                {
                    return $"{Server} ({Dc})";
                }
                else if (!String.IsNullOrEmpty(Server))
                {
                    return Server;
                }
                else if (!String.IsNullOrEmpty(Dc))
                {
                    return Dc;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
