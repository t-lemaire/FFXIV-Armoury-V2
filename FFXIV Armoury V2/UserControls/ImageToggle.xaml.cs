using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFXIV_Armoury_V2.UserControls
{
    /// <summary>
    /// Interaction logic for ImageToggle.xaml
    /// </summary>
    public partial class ImageToggle : UserControl
    {
        /*private string _toggledImage;

        public string ToggledImage
        {
            get { return _toggledImage; }
            set { _toggledImage = value; }
        }*/

        private static DependencyProperty _untoggledImage = DependencyProperty.Register("UntoggledImage", typeof(string), typeof(ImageToggle));

        public string UntoggledImage
        {
            get { 
                return (string)GetValue(_untoggledImage);
            }
            set { 
                SetValue(_untoggledImage, value);
            }
        }

        private static DependencyProperty _tooltip = DependencyProperty.Register("Tooltip", typeof(string), typeof(ImageToggle));

        public string Tooltip
        {
            get
            {
                return (string)GetValue(_tooltip);
            }
            set
            {
                SetValue(_tooltip, value);
            }
        }

        /*private bool _isToggled;

        public bool IsToggled
        {
            get { return _isToggled; }
            set { _isToggled = value; }
        }*/


        public ImageToggle()
        {
            InitializeComponent();
        }
    }
}
