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
using Example.Font_Awesome_WPF.ViewModel;

namespace Example.Font_Awesome_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void IconSource_OnFilter(object sender, FilterEventArgs e)
        {
            var icon = e.Item as IconDescription;

            if (icon == null) return;

            e.Accepted = icon.Description.ToLower().Contains(FilterText.Text.ToLower());
        }

        private void FilterText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(FaListView.ItemsSource).Refresh();
        }
    }
}
