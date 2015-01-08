using System;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Represents the category of a fontawesome icon.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class IconCategoryAttribute
        : Attribute
    {
        /// <summary>
        /// Gets or sets the category of the icon.
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Initializes a new instance of the FontAwesome.WPF.IconCategoryAttribute class.
        /// </summary>
        /// <param name="category">The icon category.</param>
        public IconCategoryAttribute(string category)
        {
            Category = category;
        }
    }
    /// <summary>
    /// Represents the field is an alias of another icon.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class IconAliasAttribute
        : Attribute
    { }
    
}
