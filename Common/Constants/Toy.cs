using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// List of possible toys the system can work with
    /// </summary>
    public static class Toy
    {
        /// <summary>
        /// Sub owns a chastity device
        /// </summary>
        public static string ChastityDevice => nameof(ChastityDevice);

        /// <summary>
        /// Sub owns a chastity device with spikes
        /// </summary>
        public static string ChastityDeviceWithSpikes => nameof(ChastityDeviceWithSpikes);

        /// <summary>
        /// Sub owns a chastity device which requires a piercing
        /// </summary>
        public static string ChastityDeviceRequiresPiercing => nameof(ChastityDeviceRequiresPiercing);
    }
}
