using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Example.FontAwesome.WPF.ViewModel;
using FontAwesome.WPF;

namespace Example.FontAwesome.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
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
