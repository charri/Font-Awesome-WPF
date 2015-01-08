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
    /// Interaction logic for SingleAnimation.xaml
    /// </summary>
    public partial class SingleAnimation : Window
    {
        public SingleAnimation()
        {
            InitializeComponent();

            Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.RotateRight, Brushes.BlueViolet);
        }
    }
}
