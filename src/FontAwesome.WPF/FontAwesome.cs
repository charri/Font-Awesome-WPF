using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome.WPF
{
    public class FontAwesome
        : TextBlock
    {
        private static readonly FontFamily FontAwesomeFontFamily = new FontFamily(new Uri("pack://application:,,,/Font-Awesome-WPF;component/"), "./#FontAwesome");

        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(FontAwesome), new PropertyMetadata(FontAwesomeIcon.None, OnIconPropertyChanged));

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextBlock.TextProperty, char.ConvertFromUtf32((int)e.NewValue));
        }

        public FontAwesome()
        {
            FontFamily = FontAwesomeFontFamily;
            TextAlignment = System.Windows.TextAlignment.Center;
        }


    }
}
