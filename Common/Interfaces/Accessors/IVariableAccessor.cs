namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IVariableAccessor
    {
        bool DoesExist(DommePersonality domme, string variableName);
        Result<string> GetVariable(DommePersonality domme, string variableName);
        Result SetVariable(DommePersonality domme, string variableName, string value);
    }
}
