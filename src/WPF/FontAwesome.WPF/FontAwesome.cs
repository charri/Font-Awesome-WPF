using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Provides a lightweight control for displaying a FontAwesome icon as text.
    /// </summary>
    public class FontAwesome
        : TextBlock, ISpinable, IRotatable, IFlippable, IPulsable
    {
        /// <summary>
        /// FontAwesome FontFamily.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamily = new FontFamily(new Uri("pack://application:,,,/FontAwesome.WPF;component/"), "./#FontAwesome");
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(FontAwesome), new PropertyMetadata(FontAwesomeIcon.None, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty =
            DependencyProperty.Register("Spin", typeof(bool), typeof(FontAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.Register("SpinDuration", typeof(double), typeof(FontAwesome), new PropertyMetadata(1d, SpinDurationChanged, SpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(FontAwesome), new PropertyMetadata(0d, RotationChanged, RotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty =
            DependencyProperty.Register("FlipOrientation", typeof(FlipOrientation), typeof(FontAwesome), new PropertyMetadata(FlipOrientation.Normal, FlipOrientationChanged));

        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Pulse dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseProperty =
            DependencyProperty.Register("Pulse", typeof(bool), typeof(FontAwesome), new PropertyMetadata(false, OnPulsePropertyChanged, PulseCoerceValue));

        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.PulseDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseDurationProperty =
            DependencyProperty.Register("PulseDuration", typeof(double), typeof(FontAwesome), new PropertyMetadata(0d, PulseDurationChanged, PulseDurationCoerceValue));

        static FontAwesome()
        {
            OpacityProperty.OverrideMetadata(typeof(FontAwesome), new UIPropertyMetadata(1.0, OpacityChanged));
        }

        public FontAwesome()
        {
            IsVisibleChanged += (s, a) => CoerceValue(SpinProperty);
        }

        private static void OpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(SpinProperty);
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

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
#if NET40
            d.SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.ClearType);
#endif
            d.SetValue(FontFamilyProperty, FontAwesomeFontFamily);
            d.SetValue(TextAlignmentProperty, TextAlignment.Center);
            d.SetValue(TextProperty, char.ConvertFromUtf32((int)e.NewValue));
        }

        private static void OnSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fontAwesome = d as FontAwesome;

            if (fontAwesome == null) return;

            if ((bool)e.NewValue)
                fontAwesome.BeginSpin();
            else
            {
                fontAwesome.StopSpin();
                fontAwesome.SetRotation();
            }
        }

        private static object SpinCoerceValue(DependencyObject d, object basevalue)
        {
            var fontAwesome = (FontAwesome)d;

            if (!fontAwesome.IsVisible || fontAwesome.Opacity == 0.0 || fontAwesome.SpinDuration == 0.0)
                return false;

            return basevalue;
        }

        /// <summary>
        /// Gets or sets the duration of the spinning animation (in seconds). This will stop and start the spin animation.
        /// </summary>
        public double SpinDuration
        {
            get { return (double)GetValue(SpinDurationProperty); }
            set { SetValue(SpinDurationProperty, value); }
        }

        private static void SpinDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fontAwesome = d as FontAwesome;

            if (null == fontAwesome || !fontAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue)) return;

            fontAwesome.StopSpin();
            fontAwesome.BeginSpin();
        }

        private static object SpinDurationCoerceValue(DependencyObject d, object value)
        {
            double val = (double)value;
            return val < 0 ? 0d : value;
        }

        /// <summary>
        /// Gets or sets the state of the Pulse animation
        /// </summary>
        public bool Pulse
        {
            get { return (bool)GetValue(PulseProperty); }
            set { SetValue(PulseProperty, value); }
        }

        private static void OnPulsePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as FontAwesome;

            if (imageAwesome == null) return;

            if ((bool)e.NewValue)
                imageAwesome.BeginPulse();
            else
            {
                imageAwesome.StopPulse();
                imageAwesome.SetRotation();
            }
        }

        private static object PulseCoerceValue(DependencyObject d, object basevalue)
        {
            var imageAwesome = (FontAwesome)d;

            if (!imageAwesome.IsVisible || imageAwesome.Opacity == 0.0 || imageAwesome.PulseDuration == 0.0)
                return false;

            return basevalue;
        }

        /// <summary>
        /// Gets or sets the length of the Pulse animation in seconds
        /// </summary>
        public double PulseDuration
        {
            get { return (double)GetValue(PulseDurationProperty); }
            set { SetValue(PulseDurationProperty, value); }
        }

        private static void PulseDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as FontAwesome;

            if (null == imageAwesome || !imageAwesome.Pulse || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue)) return;

            imageAwesome.StopPulse();
            imageAwesome.BeginPulse();
        }

        private static object PulseDurationCoerceValue(DependencyObject d, object value)
        {
            double val = (double)value;
            return val < 0 ? 0d : value;
        }

        /// <summary>
        /// Gets or sets the current rotation (angle).
        /// </summary>
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        private static void RotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fontAwesome = d as FontAwesome;

            if (null == fontAwesome || fontAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue)) return;

            fontAwesome.SetRotation();
        }

        private static object RotationCoerceValue(DependencyObject d, object value)
        {
            double val = (double)value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
        }

        /// <summary>
        /// Gets or sets the current orientation (horizontal, vertical).
        /// </summary>
        public FlipOrientation FlipOrientation
        {
            get { return (FlipOrientation)GetValue(FlipOrientationProperty); }
            set { SetValue(FlipOrientationProperty, value); }
        }

        private static void FlipOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fontAwesome = d as FontAwesome;

            if (null == fontAwesome || !(e.NewValue is FlipOrientation) || e.NewValue.Equals(e.OldValue)) return;

            fontAwesome.SetFlipOrientation();
        }
    }
}
