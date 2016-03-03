using System;

namespace FontAwesome.UWP
{
    /// <summary>
    /// Specifies a description for a property or event.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class DescriptionAttribute
        : Attribute
    {
        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the DescriptionAttribute class with a description.
        /// </summary>
        /// <param name="description">The description text.</param>
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
        
    }

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

    /// <summary>
    /// Represents the id (css class name) of the icon.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class IconIdAttribute
        : Attribute
    {
        /// <summary>
        /// Gets or sets the id (css class name) of the icon.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the FontAwesome.WPF.IconIdAttribute class.
        /// </summary>
        /// <param name="id">The icon id (css class name).</param>
        public IconIdAttribute(string id)
        {
            Id = id;
        }
    }
}
