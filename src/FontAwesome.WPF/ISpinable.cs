using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Represents a spinable control
    /// </summary>
    public interface ISpinable
    {
        /// <summary>
        /// Gets or sets the current spin (angle) animation of the icon.
        /// </summary>
        bool Spin { get; set; }
    }
}
