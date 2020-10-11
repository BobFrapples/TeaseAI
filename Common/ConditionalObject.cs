using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// Used to build logic into scripts.
    /// </summary>
    public class ConditionalObject
    {
        /// <summary>
        /// This is the string before parsing
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// Left side of the condition
        /// </summary>
        public string LeftSide { get; set; }

        /// <summary>
        /// Comparison operator, <see cref="ConditionalOperator"/> has the full list
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Right side of the condition
        /// </summary>
        public string RightSide { get; set; }

        /// <summary>
        /// if condition is true, this is the bookmark to jump to
        /// </summary>
        public string Target { get; set; }
    }
}
