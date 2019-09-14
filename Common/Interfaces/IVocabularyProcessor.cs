namespace TeaseAI.Common.Interfaces
{
    public interface IVocabularyProcessor
    {
        string ReplaceVocabulary(Session session, string workingLine);
        bool IsRelevant(string workingLine);
    }
}
