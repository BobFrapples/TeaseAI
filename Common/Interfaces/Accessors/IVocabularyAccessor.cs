using System.Collections.Generic;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IVocabularyAccessor
    {
        Result<List<string>> GetVocabulary(DommePersonality domme, string keyword);

    }
}
