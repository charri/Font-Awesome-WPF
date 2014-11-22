using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FontAwesome.WPF;

namespace Example.FontAwesome.WPF.ViewModel
{
    public class MainViewModel
    {

        public ObservableCollection<IconDescription> Icons { get; set; }

        public MainViewModel()
        {
            Icons = new ObservableCollection<IconDescription>();

            var icons = Enum.GetValues(typeof (FontAwesomeIcon)).Cast<FontAwesomeIcon>()
                .Where(t => t != FontAwesomeIcon.None)
                .OrderBy(t => t, new IconComparer());

            foreach (var icon in icons)
            {
                var memberInfo = typeof(FontAwesomeIcon).GetMember(icon.ToString()).FirstOrDefault();

                if(memberInfo == null) continue; // alias

                foreach (var cat in memberInfo.GetCustomAttributes(typeof(IconCategoryAttribute), false).Cast<IconCategoryAttribute>())
                {
                    var desc = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().First();
                    Icons.Add(new IconDescription { Category = cat.Category, Description = desc.Description, Icon = icon });
                }
            }
            
        }

        public class IconComparer
            : IComparer<FontAwesomeIcon>
        {
            public int Compare(FontAwesomeIcon x, FontAwesomeIcon y)
            {
                return String.Compare(x.ToString(), y.ToString(), System.StringComparison.InvariantCultureIgnoreCase);
            }
        }

    }

    public class IconDescription
    {
        public string Description { get; set; }

        public FontAwesomeIcon Icon { get; set; }

        public string Category { get; set; }

    }
}
