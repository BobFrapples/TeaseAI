using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    /// <summary>
    /// used to test the condition of conditional Objects
    /// </summary>
    public class ConditionalObjectLogic : IConditionalObjectLogic
    {
        public const string TypeMismatchError = "This line is comparing a word with a number incorrectly";
        public const string UnknownOperatorError = " is not a known comparison. Please report this bug to the script writer";

        public Result<bool> Evaluate(ConditionalObject conditionalObject)
        {
            var leftVar = 0m;
            var rightVar = 0m;
            var isLeftNumeric = decimal.TryParse(conditionalObject.LeftSide, out leftVar);
            var isRightNumeric = decimal.TryParse(conditionalObject.RightSide, out rightVar);

            if (isLeftNumeric != isRightNumeric)
                return Result.Fail<bool>(TypeMismatchError);

            if (isLeftNumeric && isRightNumeric)
                return TestNumerics(leftVar, conditionalObject.Operator, rightVar);
            return TestStrings(conditionalObject.LeftSide, conditionalObject.Operator, conditionalObject.RightSide);
        }


        private Result<bool> TestStrings(string leftVar, string conditionalOperator, string rightVar)
        {
            switch (conditionalOperator)
            {
                case ConditionalOperator.EqualTo:
                    return Result.Ok(leftVar.ToLower() == rightVar.ToLower());
                case ConditionalOperator.NotEqualTo:
                    return Result.Ok(leftVar.ToLower() != rightVar.ToLower());
                default:
                    return Result.Fail<bool>(conditionalOperator + UnknownOperatorError);
            }
        }

        private Result<bool> TestNumerics(decimal leftVar, string conditionalOperator, decimal rightVar)
        {
            switch (conditionalOperator)
            {
                case ConditionalOperator.EqualTo:
                    return Result.Ok(leftVar == rightVar);
                case ConditionalOperator.NotEqualTo:
                    return Result.Ok(leftVar != rightVar);
                case ConditionalOperator.GreaterThan:
                    return Result.Ok(leftVar > rightVar);
                case ConditionalOperator.GreaterThanOrEqualTo:
                    return Result.Ok(leftVar >= rightVar);
                case ConditionalOperator.LessThanOrEqualTo:
                    return Result.Ok(leftVar <= rightVar);
                case ConditionalOperator.LessThan:
                    return Result.Ok(leftVar < rightVar);
                default:
                    return Result.Fail<bool>(conditionalOperator + UnknownOperatorError);
            }
        }
    }
}
