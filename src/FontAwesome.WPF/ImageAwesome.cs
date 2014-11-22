using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FontAwesome.WPF
{
    public class ImageAwesome
        : Image
    {
        private static readonly FontFamily FontAwesomeFontFamily = new FontFamily(new Uri("pack://application:,,,/Font-Awesome-WPF;component/"), "./#FontAwesome");

        private static readonly Typeface FontAweseomTypeface = new Typeface(FontAwesomeFontFamily, FontStyles.Normal,
            FontWeights.Normal, FontStretches.Normal);

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(Brushes.Black));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(ImageAwesome), new PropertyMetadata(FontAwesomeIcon.None, OnIconPropertyChanged));

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as ImageAwesome;

            if (imageAwesome == null) return;

            d.SetValue(Image.SourceProperty, CreateImageSource(imageAwesome.Icon, imageAwesome.Foreground));
        }

        private static ImageSource CreateImageSource(FontAwesomeIcon icon, Brush foregroundBrush)
        {
            var charIcon = char.ConvertFromUtf32((int)icon);

            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawText(
                    new FormattedText(charIcon, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        FontAweseomTypeface, 100, foregroundBrush), new Point(0, 0));
            }
            return new DrawingImage(visual.Drawing);
        }
    }
}
