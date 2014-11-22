using System;

namespace FontAwesome.WPF
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class IconCategoryAttribute
        : Attribute
    {
        /// <summary>
        /// Icon's Category
        /// </summary>
        public string Category { get; set; }

        public IconCategoryAttribute(string category)
        {
            Category = category;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class IconAliasAttribute
        : Attribute
    { }
    
}
