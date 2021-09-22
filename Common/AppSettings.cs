using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common
{
    /// <summary>
    /// Settings from the apps tab
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Settings for the Lazy Sub app
        /// </summary>
        public LazySubSettings LazySub
        {
            get { return _lazySubSettings ?? (_lazySubSettings = new LazySubSettings()); }
            set { _lazySubSettings = value; }
        }

        private LazySubSettings _lazySubSettings;
        
        /// <summary>
        /// Settings for the Lazy Sub app
        /// </summary>
        public GlitterSettings Glitter
        {
            get { return _glitterSettings ?? (_glitterSettings = new GlitterSettings()); }
            set { _glitterSettings = value; }
        }

        private GlitterSettings _glitterSettings;
    }
}
