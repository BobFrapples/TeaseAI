using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class TauntAccessor : ITauntAccessor
    {
        private IConfigurationAccessor _configurationAccessor;

        public TauntAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<List<string>> GetTaunt(Session session, string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
