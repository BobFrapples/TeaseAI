using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Interfaces
{
    public interface IConditionalObjectLogic
    {
        Result<bool> Evaluate(ConditionalObject conditionalObject);
    }
}
