using FFXIV_Armoury_V2.Core;
using System;
using System.Collections.Generic;
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
            }
        }

        private string? _dc;

        public string? Dc
        {
            get { return _dc; }
            set { 
                _dc = value;
                OnPropertyChanged();
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

        public string? DcServer
        {
            get
            {
                if (!String.IsNullOrEmpty(Dc) && !String.IsNullOrEmpty(Server))
                {
                    return $"{Server} ({Dc})";
                }
                else if (!String.IsNullOrEmpty(Server) && String.IsNullOrEmpty(Dc))
                {
                    return Server;
                }
                else if (String.IsNullOrEmpty(Server) && !String.IsNullOrEmpty(Dc))
                {
                    return Dc;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
