using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface ISystemVocabularyAccessor
    {
        Result<List<string>> GetData(Session session, string key);
    }
}
