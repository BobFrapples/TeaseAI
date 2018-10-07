using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IResponseAccessor
    {
        List<Response> GetResponses(Session session);
    }
}
