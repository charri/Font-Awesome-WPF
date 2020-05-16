﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Represents a control that draws an FontAwesome icon as an image.
    /// </summary>
    public class ImageAwesome
        : Image, ISpinable, IRotatable, IFlippable, IPulsable
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
            DependencyProperty.Register("Spin", typeof(bool), typeof(ImageAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.Register("SpinDuration", typeof(double), typeof(ImageAwesome), new PropertyMetadata(1d, SpinDurationChanged, SpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(ImageAwesome), new PropertyMetadata(0d, RotationChanged, RotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty =
            DependencyProperty.Register("FlipOrientation", typeof(FlipOrientation), typeof(ImageAwesome), new PropertyMetadata(FlipOrientation.Normal, FlipOrientationChanged));

        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Pulse dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseProperty =
            DependencyProperty.Register("Pulse", typeof(bool), typeof(ImageAwesome), new PropertyMetadata(false, OnPulsePropertyChanged, PulseCoerceValue));

        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.PulseDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseDurationProperty =
            DependencyProperty.Register("PulseDuration", typeof(double), typeof(ImageAwesome), new PropertyMetadata(0d, PulseDurationChanged, PulseDurationCoerceValue));

        static ImageAwesome()
        {
            OpacityProperty.OverrideMetadata(typeof(ImageAwesome), new UIPropertyMetadata(1.0, OpacityChanged));
        }

        public ImageAwesome()
        {
            IsVisibleChanged += (s, a) => CoerceValue(SpinProperty);
        }

        private static void OpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(SpinProperty);
        }

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

            if ((bool)e.NewValue)
                imageAwesome.BeginSpin();
            else
            {
                imageAwesome.StopSpin();
                imageAwesome.SetRotation();
            }
        }

        private static object SpinCoerceValue(DependencyObject d, object basevalue)
        {
            var imageAwesome = (ImageAwesome)d;

            if (!imageAwesome.IsVisible || imageAwesome.Opacity == 0.0 || imageAwesome.SpinDuration == 0.0)
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
            var imageAwesome = d as ImageAwesome;

            if (null == imageAwesome || !imageAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue)) return;

            imageAwesome.StopSpin();
            imageAwesome.BeginSpin();
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
            var imageAwesome = d as ImageAwesome;

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
            var imageAwesome = (ImageAwesome)d;

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
            var imageAwesome = d as ImageAwesome;

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
            var imageAwesome = d as ImageAwesome;

            if (null == imageAwesome || imageAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue)) return;

            imageAwesome.SetRotation();
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
            var imageAwesome = d as ImageAwesome;

            if (null == imageAwesome || !(e.NewValue is FlipOrientation) || e.NewValue.Equals(e.OldValue)) return;
            
            imageAwesome.SetFlipOrientation();
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as ImageAwesome;

            if (imageAwesome == null) return;

            imageAwesome.SetValue(SourceProperty, CreateImageSource(imageAwesome.Icon, imageAwesome.Foreground));
        }


        /// <summary>
        /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
        /// <returns>A new System.Windows.Media.ImageSource</returns>
        public static ImageSource CreateImageSource(FontAwesomeIcon icon, Brush foregroundBrush, double emSize = 100)
        {
            var charIcon = char.ConvertFromUtf32((int)icon);

            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawText(
                    new FormattedText(charIcon, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        FontAwesomeTypeface, emSize, foregroundBrush) { TextAlignment = TextAlignment.Center }, new Point(0, 0));
            }
            return new DrawingImage(visual.Drawing);
        }

    }
}
