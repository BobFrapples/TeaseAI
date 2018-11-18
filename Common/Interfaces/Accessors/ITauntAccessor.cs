using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface ITauntAccessor
    {
        Result<List<string>> GetTaunt(Session session, string keyword);
    }
}
