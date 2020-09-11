namespace TeaseAI.Common
{
    /// <summary>
    /// Configuration for "VitalSub" feature
    /// </summary>
    public class HealthGoals
    {
        public HealthGoals()
        {
            Version = 1;
            CaloriesGoal = 2000;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set;}

        /// <summary>
        /// Is VitalSub active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Calories eaten since the last reporting
        /// </summary>
        public int CaloriesConsumed { get; set; } 

        /// <summary>
        /// Target number of calories per day
        /// </summary>
        public int CaloriesGoal { get; set; }

        public bool CanDommeAddAssignments { get; set; }
    }
}