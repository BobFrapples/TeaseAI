using System;
using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface ICldAccessor
    {
        /// <summary>
        /// Read a checklist file that stores Script Meta Data
        /// </summary>
        /// <param name="scriptHomeDir"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Result<List<ScriptMetaData>> ReadCld(string scriptHomeDir, string fileName);
        void WriteCld(List<ScriptMetaData> finalCld, string loadPath);
    }
}
