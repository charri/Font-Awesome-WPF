using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Represents a control that draws an FontAwesome icon as an image.
    /// </summary>
    public class ImageAwesome
        : Image
    {
        /// <summary>
        /// FontAwesome FontFamily.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamily = new FontFamily(new Uri("pack://application:,,,/FontAwesome.WPF;component/"), "./#FontAwesome");
        /// <summary>
        /// Typeface used to generate FontAwesome icon.
        /// </summary>
        private static readonly Typeface FontAwesomeTypeface = new Typeface(FontAwesomeFontFamily, FontStyles.Normal,
            FontWeights.Normal, FontStretches.Normal);
        /// <summary>
        /// The key used for storing the spinner Storyboard.
        /// </summary>
        private static readonly string StoryBoardName = String.Format("{0}-storyboard-spinner", typeof(ImageAwesome).Name);

        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Foreground dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(Brushes.Black, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(ImageAwesome), new PropertyMetadata(FontAwesomeIcon.None, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty =
            DependencyProperty.Register("Spin", typeof(bool), typeof(ImageAwesome), new PropertyMetadata(false, OnSpinPropertyChanged));

        /// <summary>
        /// Gets or sets the foreground brush of the icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        /// <summary>
        /// Gets or sets the FontAwesome icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        /// <summary>
        /// Gets or sets the current spin (angle) animation of the icon.
        /// </summary>
        public bool Spin
        {
            get { return (bool)GetValue(SpinProperty); }
            set { SetValue(SpinProperty, value); }
        }

        private static void OnSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as ImageAwesome;

            if (imageAwesome == null) return;

            if ((bool) e.NewValue)
            {
                var storyboard = new Storyboard();

                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 360,
                    AutoReverse = false,
                    RepeatBehavior = RepeatBehavior.Forever,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };
                storyboard.Children.Add(animation);

                Storyboard.SetTarget(animation, imageAwesome);
                Storyboard.SetTargetProperty(animation, new PropertyPath("(ImageAwesome.RenderTransform).(RotateTransform.Angle)"));

                storyboard.Begin();
                imageAwesome.Resources.Add(StoryBoardName, storyboard);
            }
            else
            {
                var storyboard = imageAwesome.Resources[StoryBoardName] as Storyboard;

                if (storyboard == null) return;

                storyboard.Stop();

                imageAwesome.Resources.Remove(StoryBoardName);
            }
        }
        
        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as ImageAwesome;

            if (imageAwesome == null) return;

            d.SetValue(Image.SourceProperty, CreateImageSource(imageAwesome.Icon, imageAwesome.Foreground));
        }

        /// <summary>
        /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
        /// <returns>A new System.Windows.Media.ImageSource</returns>
        public static ImageSource CreateImageSource(FontAwesomeIcon icon, Brush foregroundBrush)
        {
            var charIcon = char.ConvertFromUtf32((int)icon);

            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawText(
                    new FormattedText(charIcon, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        FontAwesomeTypeface, 100, foregroundBrush), new Point(0, 0));
            }
            return new DrawingImage(visual.Drawing);
        }
    }
}
