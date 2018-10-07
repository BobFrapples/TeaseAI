namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// Interface defining how to get / set flag state
    /// </summary>
    public interface IFlagAccessor
    {
        /// <summary>
        /// Returns whether <paramref name="flagName"/>is set or not
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="flagName"></param>
        /// <returns></returns>
        bool IsSet(DommePersonality domme, string flagName);
        void SetFlag(DommePersonality domme, string flagName, bool isTemp);
        void DeleteFlag(DommePersonality domme, string flagName);
    }
}
