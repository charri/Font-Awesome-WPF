using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Draws a FontAwesome icon
    /// </summary>
    public abstract partial class FontAwesomeControl
        : Shape
    {
        protected FontAwesomeControl()
        {
            SetValue(StretchProperty, Stretch.Uniform);
            SetValue(RenderTransformProperty, new ScaleTransform(1.0, -1.0));
        }

        #region Dynamic Properties 

        /// <summary>
        /// Path data.
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data",
            typeof(Geometry),
            typeof(FontAwesomeControl),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender),
            null);


        /// <summary>
        /// Path data
        /// </summary>
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            private set { SetValue(DataProperty, value); }
        }
        #endregion

        #region Protected Methods and Properties 

        /// <summary>
        /// Gets a value that represents the Geometry of the Shape.
        /// </summary>
        protected override Geometry DefiningGeometry => Data ?? Geometry.Empty;

        #endregion
    }
}
