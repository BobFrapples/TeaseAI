using System;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Events
{
    public class ShowImageEventArgs : EventArgs
    {
        public ImageMetaData ImageMetaData { get; set; }
    }
}
