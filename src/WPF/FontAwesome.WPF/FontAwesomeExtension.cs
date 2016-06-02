using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Markup extension for easy converting from icon enum to string
    /// </summary>
    public class FontAwesomeExtension : MarkupExtension
    {
        /// <summary>
        /// Icon.
        /// </summary>
        public FontAwesomeIcon Icon { get; set; }

        public FontAwesomeExtension()
        {
        }


        public FontAwesomeExtension(FontAwesomeIcon icon)
        {
            this.Icon = icon;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return char.ConvertFromUtf32((int)Icon);
        }
    }
}
