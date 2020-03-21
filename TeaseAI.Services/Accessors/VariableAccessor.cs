using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class VariableAccessor : IVariableAccessor
    {
        public VariableAccessor(IConfigurationAccessor configurationAccessor)
        {
        }

        public Result AddToVariable(DommePersonality domme, string variableName, int add)
        {
            throw new NotImplementedException();
        }

        public bool DoesExist(DommePersonality domme, string variableName)
        {
            throw new NotImplementedException();
        }

        public Result<string> GetVariable(DommePersonality domme, string variableName)
        {
            throw new NotImplementedException();
        }

        public Result SetVariable(DommePersonality domme, string variableName, string value)
        {
            throw new NotImplementedException();
        }
    }
}
