using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Events
{
    public class SendFileEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
    }
}
