using System;

namespace TeaseAI.Common.Interfaces
{
    /// <summary>
    /// service dealing with time
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// Get the general time of day based on when the sub wakes up: morning, afternoon, evening,  etc
        /// </summary>
        /// <returns></returns>
        string GetGeneralTime();

        /// <summary>
        /// Get the current actual time
        /// </summary>
        /// <returns></returns>
        DateTime GetTime();
    }
}
