using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class FlagAccessor : IFlagAccessor
    {
        private IConfigurationAccessor _configurationAccessor;

        public FlagAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public void DeleteFlag(DommePersonality domme, string flagName)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(DommePersonality domme, string flagName)
        {
            throw new NotImplementedException();
        }

        public void SetFlag(DommePersonality domme, string flagName, bool isTemp)
        {
            throw new NotImplementedException();
        }
    }
}
