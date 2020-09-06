using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaseAI.Common
{
    /// <summary>
    /// Class representing a task letter from your domme
    /// </summary>
    public class TaskLetter
    {
        /// <summary>
        /// Body of the letter
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Filename this letter should be written to
        /// </summary>
        public string FileName { get; set; }
        public string Sender { get; set; }
    }
}
