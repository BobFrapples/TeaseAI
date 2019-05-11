namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IVariableAccessor
    {
        bool DoesExist(DommePersonality domme, string variableName);
        Result<string> GetVariable(DommePersonality domme, string variableName);
        Result SetVariable(DommePersonality domme, string variableName, string value);
        /// <summary>
        /// Increase the value of <paramref name="variableName"/> by <paramref name="add"/>
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="variableName"></param>
        /// <returns></returns>
        Result AddToVariable(DommePersonality domme, string variableName, int add);
    }
}
