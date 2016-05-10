using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Example.FontAwesome.WIN81.Annotations;
using FontAwesome.UWP;

namespace Example.FontAwesome.WIN81.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IconDescription _currentIcon;
        private IEnumerable<IGrouping<string, IconDescription>> _icons;

        public IEnumerable<IGrouping<string, IconDescription>> Icons
        {
            get { return _icons; }
            private set
            {
                if (Equals(value, _icons))
                {
                    return;
                }
                _icons = value;
                OnPropertyChanged();
            }
        }

        public IconDescription CurrentIcon
        {
            get { return _currentIcon; }
            set
            {
                if (Equals(value, _currentIcon)) return;
                _currentIcon = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<IconDescription> ToIconDescription(FontAwesomeIcon icon, string filter)
        {
            var memberInfo = typeof(FontAwesomeIcon).GetTypeInfo().GetDeclaredField(icon.ToString());

            if (memberInfo == null)
                yield break;

            foreach (var cat in memberInfo.GetCustomAttributes(typeof(IconCategoryAttribute), false).Cast<IconCategoryAttribute>())
            {
                var desc = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().First();
                var id = memberInfo.GetCustomAttributes(typeof(IconIdAttribute), false).Cast<IconIdAttribute>().FirstOrDefault();

                if (!string.IsNullOrEmpty(filter) &&
                    !(
                    desc.Description.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                    icon.ToString().IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    )
                    continue;

                yield return new IconDescription { Category = cat.Category, Description = desc.Description, Icon = icon, Id = id?.Id };
            }
        }

        public MainViewModel()
        {
            LoadData(null);
        }

        private void LoadData(string filter)
        {
            Icons = Enum.GetValues(typeof(FontAwesomeIcon)).Cast<FontAwesomeIcon>()
                .Where(t => t != FontAwesomeIcon.None)
                .OrderBy(t => t, new IconComparer()) // order the icons
                .SelectMany(t => ToIconDescription(t, filter))
                .GroupBy(t => t.Category)
                .OrderBy(t => t.Key)
                .ToArray();  // order the groups
        }

        public class IconComparer
            : IComparer<FontAwesomeIcon>
        {
            public int Compare(FontAwesomeIcon x, FontAwesomeIcon y)
            {
                return string.Compare(x.ToString(), y.ToString(), System.StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Filter(string text)
        {
            LoadData(text);
        }
    }

    public class IconDescription
    {
        public string Description { get; set; }

        public FontAwesomeIcon Icon { get; set; }

        public string Category { get; set; }

        public string Id { get; set; }

    }
}

