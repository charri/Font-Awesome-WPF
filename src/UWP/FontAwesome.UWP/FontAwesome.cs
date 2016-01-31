using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FontAwesome.UWP
{
    public class FontAwesome
        : FontIcon
    {
        private static readonly FontFamily FontAwesomeFontFamily = new FontFamily("/FontAwesome.UWP;Component/FontAwesome.otf#FontAwesome");

        public FontAwesome()
        {
            FontFamily = FontAwesomeFontFamily;
        }

        public FontAwesomeIcon Icon
        {
            get
            {
                var value = GetValue(GlyphProperty);
                if (value == null) return FontAwesomeIcon.None;

                var glyph = char.ConvertToUtf32(value.ToString(), 0);

                return (FontAwesomeIcon)glyph;
            }
            set
            {
                SetValue(FontFamilyProperty, FontAwesomeFontFamily);
                SetValue(GlyphProperty, char.ConvertFromUtf32((int)value));
            }
        }
    }
}
