﻿using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// Various user configuration information
    /// </summary>
    public class Settings
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Settings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Name of the domme personality.
        /// </summary>
        public string DommePersonality { get; set; }

        /// <summary>
        /// Domme configuration ( Domme name, preferences, etc )
        /// </summary>
        public DommeSettings Domme
        {
            get { return _domme ?? (_domme = new DommeSettings()); }
            set { _domme = value; }
        }

        /// <summary>
        /// settings from the General tab.
        /// </summary>
        public GeneralSettings General
        {
            get { return _general ?? (_general = new GeneralSettings()); }
            set { _general = value; }
        }

        /// <summary>
        /// settings from the ranges tab.
        /// </summary>
        public RangeSettings Range
        {
            get { return _range ?? (_range = new RangeSettings()); }
            set { _range = value; }
        }

        /// <summary>
        /// Sub configuration
        /// </summary>
        public SubSettings Sub
        {
            get { return _sub ?? (_sub = new SubSettings()); }
            set { _sub = value; }
        }

        /// <summary>
        /// Misc configuration
        /// </summary>
        public MiscSettings Misc
        {
            get { return _misc ?? (_misc = new MiscSettings()); }
            set { _misc = value; }
        }

        /// <summary>
        /// Apps configs
        /// </summary>
        public AppSettings Apps
        {
            get { return _apps ?? (_apps = new AppSettings()); }
            set { _apps = value; }
        }

        private DommeSettings _domme;
        private SubSettings _sub;
        private RangeSettings _range;
        private GeneralSettings _general;
        private MiscSettings _misc;
        private AppSettings _apps;
    }
}
