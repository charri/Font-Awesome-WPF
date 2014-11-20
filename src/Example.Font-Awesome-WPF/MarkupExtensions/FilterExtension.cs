using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Example.Font_Awesome_WPF.MarkupExtensions
{
    [ContentProperty("Filters")]
    public class FilterExtension : MarkupExtension
    {
        private readonly Collection<IFilter> _filters = new Collection<IFilter>();
        public ICollection<IFilter> Filters { get { return _filters; } }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new FilterEventHandler((s, e) =>
            {
                if (Filters.Select(filter => filter.Filter(e.Item)).Any(res => !res))
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = true;
            });
        }
    }

    public interface IFilter
    {
        bool Filter(object item);
    }
}
