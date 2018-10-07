using System;

namespace TeaseAI.Common.Interfaces.Timers
{
    /// <summary>
    /// This is an interface to a timer, that implementation must be provided by the end application
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// if true, reset the timer after the elapse event, if false, only fire once
        /// </summary>
        bool AutoReset { get; set; }
        /// <summary>
        /// will the timer raise the Elapsed event or not
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// Interval time in ms
        /// </summary>
        double Interval { get; set; }

        /// <summary>
        /// Event raised when the interval has passed
        /// </summary>
        event EventHandler<EventArgs> Elapsed;
    }
}
