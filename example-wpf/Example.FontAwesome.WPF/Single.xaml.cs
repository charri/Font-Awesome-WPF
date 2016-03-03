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
using System.Windows.Shapes;
using FontAwesome.WPF;

namespace Example.FontAwesome.WPF
{
    /// <summary>
    /// Interaction logic for Single.xaml
    /// </summary>
    public partial class Single : Window
    {
        public Single()
        {
            InitializeComponent();

            Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.BlueViolet);
        }

        public FontAwesomeIcon FontAwesomeIcon { get { return FontAwesomeIcon.Flag;} }
    }
}
