namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// names of system variables. these will always exist
    /// </summary>
    public static class SystemVariable
    {
        /// <summary>
        /// The first time this personality was run
        /// </summary>
        public const string FirstRun = "SYS_FirstRun";
        /// <summary>
        /// How many times has the sub been told to stroke
        /// </summary>
        public const string StrokeRound = "SYS_StrokeRound";

        /// <summary>
        /// keeps track of how many times the sub left before the session was complete
        /// </summary>
        public const string SubLeftEarly = "SYS_SubLeftEarly";

        /// <summary>
        /// How many times this session has the sub edged.
        /// </summary>
        public const string EdgeTotal = "SYS_EdgeTotal";
    }
}
