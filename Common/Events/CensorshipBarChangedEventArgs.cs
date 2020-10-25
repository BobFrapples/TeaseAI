using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Events
{
    /// <summary>
    /// Arguments telling the UI how to update the censorship bar
    /// </summary>
    public class CensorshipBarChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Show the bar be shown or hidden
        /// </summary>
        public bool IsVisible { get; set; }
        public Result Result { get; set; }
    }
}
