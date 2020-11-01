namespace TeaseAI.Common
{
    /// <summary>
    /// Settings specific to the Lazy Sub App
    /// </summary>
    public class LazySubSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LazySubSettings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Custom text Sent when button one is pressed
        /// </summary>
        public string CustomTextOne { get; set; }

        /// <summary>
        /// Custom text Sent when button two is pressed
        /// </summary>
        public string CustomTextTwo { get; set; }

        /// <summary>
        /// Custom text Sent when button three is pressed
        /// </summary>
        public string CustomTextThree { get; set; }

        /// <summary>
        /// Custom text Sent when button four is pressed
        /// </summary>
        public string CustomTextFour { get; set; }

        /// <summary>
        /// Custom text Sent when button five is pressed
        /// </summary>
        public string CustomTextFive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YesShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NoShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OnTheEdgeShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SpeedUpShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SlowDownShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StrokeShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StopShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LetMeCumShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GreetingShortCut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SafewordShortCut { get; set; }

        /// <summary>
        /// Should the engine expand shortcuts
        /// </summary>
        public bool AreShortcutsEnabled { get; set; }
    }
}
