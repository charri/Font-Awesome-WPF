using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace FontAwesome.WPF.Converters
{
    /// <summary>
    /// Can convert the CSS class name to a FontAwesomIcon and vice-versa.
    /// </summary>
    public class CssClassNameConverter
        : IValueConverter
    {
        private static readonly IDictionary<string, FontAwesomeIcon> ClassNameLookup;
        private static readonly IDictionary<FontAwesomeIcon, string> IconLookup;
 
        static CssClassNameConverter()
        {
            ClassNameLookup = new Dictionary<string, FontAwesomeIcon>();
            IconLookup = new Dictionary<FontAwesomeIcon, string>();

            foreach (var value in Enum.GetValues(typeof (FontAwesomeIcon)))
            {
                var memInfo = typeof(FontAwesomeIcon).GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(IconIdAttribute), false);

                if (attributes.Length == 0) continue; // alias

                var id = ((IconIdAttribute)attributes[0]).Id;

                ClassNameLookup.Add(id, (FontAwesomeIcon)value);
                IconLookup.Add((FontAwesomeIcon)value, id);
            }
        }

        /// <summary>
        /// Gets or sets the mode of the converter
        /// </summary>
        public CassClassConverterMode Mode { get; set; }

        private static FontAwesomeIcon FromStringToIcon(object value)
        {
            var icon = value as string;

            if (string.IsNullOrEmpty(icon)) return FontAwesomeIcon.None;

            FontAwesomeIcon rValue;

            if (!ClassNameLookup.TryGetValue(icon, out rValue))
            {
                rValue = FontAwesomeIcon.None;
            }

            return rValue;
        }

        private static string FromIconToString(object value)
        {
            if (!(value is FontAwesomeIcon)) throw new ArgumentException("Value must be of type: FontAwesomeIcon", "value");

            string rValue = null;

            if (!IconLookup.TryGetValue((FontAwesomeIcon)value, out rValue))
            {
                throw new Exception("Icon not found. Was an alias entered?");
            }

            return rValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Mode == CassClassConverterMode.FromStringToIcon)
                return FromStringToIcon(value);
            
            return FromIconToString(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Mode == CassClassConverterMode.FromStringToIcon)
                return FromIconToString(value);

            return FromStringToIcon(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CassClassConverterMode
    {
        /// <summary>
        /// 
        /// </summary>
        FromStringToIcon = 0,
        /// <summary>
        /// 
        /// </summary>
        FromIconToString
    }
}
