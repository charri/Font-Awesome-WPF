using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Font_Awesome.WPF
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class IconCategoryAttribute
        : Attribute
    {
        public string Category { get; set; }

        public IconCategoryAttribute(string category)
        {
            Category = category;
        }
    }
}
