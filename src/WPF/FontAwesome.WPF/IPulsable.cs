namespace FontAwesome.WPF
{
    /// <summary>
    /// Represents a control that can have a pulse animation
    /// </summary>
    public interface IPulsable
    {
        /// <summary>
        /// Gets or sets the state of the pulse animation
        /// </summary>
        bool Pulse { get; set; }

        /// <summary>
        /// Gets or set the length of the pulse animation in seconds
        /// </summary>
        double PulseDuration { get; set; }
    }
}
