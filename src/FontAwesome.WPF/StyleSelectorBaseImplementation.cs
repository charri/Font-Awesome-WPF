using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Needed to accomodate the change form fontawesome 4.7 to 5.12, as there are now 3 distinct font families which need to be selectable. Instead of making 3 different controls (FontAwesome, Awesome,ImageAwesome) one uses the <see cref="StyleSelector"/> enum to select the wanted fontfamily.
    /// </summary>
    internal static class StyleSelectorBaseImplementation
    {
        /// <summary>
        /// FontAwesome FontFamily for Regular icons.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamilyRegular = new FontFamily(new Uri("pack://application:,,,/FontAwesome.WPF;component/"), "./#Font Awesome 5 Free Regular"); //Last one is the actual name of the font family inside the otf file, not the filename!
        /// <summary>
        /// FontAwesome FontFamily for Bold icons.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamilySolid = new FontFamily(new Uri("pack://application:,,,/FontAwesome.WPF;component/"), "./#Font Awesome 5 Free Solid"); //Last one is the actual name of the font family inside the otf file, not the filename!
        /// <summary>
        /// FontAwesome FontFamily for icons belonging to brands.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamilyBrands = new FontFamily(new Uri("pack://application:,,,/FontAwesome.WPF;component/"), "./#Font Awesome 5 Brands Regular"); //Last one is the actual name of the font family inside the otf file, not the filename!


        #region Helper

        internal static FontFamily GetSelectedFontFamily(object selector)
        {
            FontFamily family = null;
            if (selector is StyleSelector s)
                family = GetSelectedFontFamily(s);

            return family;
        }

        internal static FontFamily GetSelectedFontFamily(StyleSelector selector)
        {
            FontFamily selectedStyle = null;
            switch (selector)
            {
                case StyleSelector.Regular:
                    selectedStyle = FontAwesomeFontFamilyRegular;
                    break;
                case StyleSelector.Solid:
                    selectedStyle = FontAwesomeFontFamilySolid;
                    break;
                case StyleSelector.Brands:
                    selectedStyle = FontAwesomeFontFamilyBrands;
                    break;
                default:
                    selectedStyle = FontAwesomeFontFamilyRegular;
                    break;
            }
            return selectedStyle;
        }


        #endregion
    }
}
