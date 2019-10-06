using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface ICldAccessor
    {
        Result<List<ScriptMetaData>> ReadCld(string fileName);
        void WriteCld(List<ScriptMetaData> finalCld, string loadPath);
    }
}
