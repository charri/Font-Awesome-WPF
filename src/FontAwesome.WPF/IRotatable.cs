namespace FontAwesome.WPF
{
    /// <summary>
    /// Represents a rotatable control
    /// </summary>
    public interface IRotatable
    {
        /// <summary>
        /// Gets or sets the current rotation (angle) of the icon.
        /// </summary>
        double Rotation { get; set; }
    }
}