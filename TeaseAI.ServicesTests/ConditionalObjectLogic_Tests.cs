using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Services;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class ConditionalObjectLogic_Tests
    {
        ConditionalObjectLogic _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new ConditionalObjectLogic();
        }

        [TestMethod]
        public void GetState_ShouldFail_WhenComparingStringAndNumeric()
        {
            foreach (var input in new List<Tuple<string, string>>()
            {
                Tuple.Create("1", "test"),
                Tuple.Create("foo", "3000")
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    LeftSide = input.Item1,
                    RightSide = input.Item2,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.IsTrue(actual.IsFailure, input.Item1 + " " + input.Item2);
                Assert.AreEqual(ConditionalObjectLogic.TypeMismatchError, actual.Error.Message, input.Item1 + " " + input.Item2);
            }
        }

        [TestMethod]
        public void GetState_ShouldEvaluateLessThanCorrectly()
        {
            foreach (var input in new List<Tuple<string, string, string, bool, bool>>()
            {
                Tuple.Create("1", ConditionalOperator.LessThan, "10", true, true),
                Tuple.Create("300", ConditionalOperator.LessThan, "30", true, false),
                Tuple.Create("bar", ConditionalOperator.LessThan, "bar", false, false)
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    String = input.Item1 + input.Item2 + input.Item3,
                    LeftSide = input.Item1,
                    Operator = input.Item2,
                    RightSide = input.Item3,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.AreEqual(input.Item4, actual.IsSuccess, conditionalObject.String);
                if (actual.IsSuccess)
                    Assert.AreEqual(input.Item5, actual.Value, conditionalObject.String);
                if (actual.IsFailure)
                    Assert.IsTrue(actual.Error.Message.EndsWith(ConditionalObjectLogic.UnknownOperatorError), conditionalObject.String);
            }
        }

        [TestMethod]
        public void GetState_ShouldEvaluateLessThanOrEqualToCorrectly()
        {
            foreach (var input in new List<Tuple<string, string, string, bool, bool>>()
            {
                Tuple.Create("1", ConditionalOperator.LessThanOrEqualTo, "10", true, true),
                Tuple.Create("20", ConditionalOperator.LessThanOrEqualTo, "20", true, true),
                Tuple.Create("300", ConditionalOperator.LessThanOrEqualTo, "30", true, false),
                Tuple.Create("bar", ConditionalOperator.LessThanOrEqualTo, "bar", false, false)
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    String = input.Item1 + input.Item2 + input.Item3,
                    LeftSide = input.Item1,
                    Operator = input.Item2,
                    RightSide = input.Item3,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.AreEqual(input.Item4, actual.IsSuccess, conditionalObject.String);
                if (actual.IsSuccess)
                    Assert.AreEqual(input.Item5, actual.Value, conditionalObject.String);
            }
        }


        [TestMethod]
        public void GetState_ShouldEvaluateEqualToCorrectly()
        {
            foreach (var input in new List<Tuple<string, string, string, bool, bool>>()
            {
                Tuple.Create("10", ConditionalOperator.EqualTo, "10", true, true),
                Tuple.Create("bar", ConditionalOperator.EqualTo, "bar", true, true),
                Tuple.Create("300", ConditionalOperator.EqualTo, "30", true, false),
                Tuple.Create("23", ConditionalOperator.EqualTo, "30", true, false),
                Tuple.Create("foo", ConditionalOperator.EqualTo, "bar", true, false),
                Tuple.Create("1", ConditionalOperator.EqualTo, "bar", false, false),
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    String = input.Item1 + input.Item2 + input.Item3,
                    LeftSide = input.Item1,
                    Operator = input.Item2,
                    RightSide = input.Item3,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.AreEqual(input.Item4, actual.IsSuccess, conditionalObject.String);
                if (actual.IsSuccess)
                    Assert.AreEqual(input.Item5, actual.Value, conditionalObject.String);
            }
        }

        [TestMethod]
        public void GetState_ShouldEvaluateNotEqualToCorrectly()
        {
            foreach (var input in new List<Tuple<string, string, string, bool, bool>>()
            {
                Tuple.Create("1", ConditionalOperator.NotEqualTo, "10", true, true),
                Tuple.Create("300", ConditionalOperator.NotEqualTo, "30", true, true),
                Tuple.Create("bar", ConditionalOperator.NotEqualTo, "bar", true, false),
                Tuple.Create("foo", ConditionalOperator.NotEqualTo, "bar", true, true),
                Tuple.Create("20", ConditionalOperator.NotEqualTo, "20", true, false),
                Tuple.Create("2", ConditionalOperator.NotEqualTo, "bar", false, false),
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    String = input.Item1 + input.Item2 + input.Item3,
                    LeftSide = input.Item1,
                    Operator = input.Item2,
                    RightSide = input.Item3,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.AreEqual(input.Item4, actual.IsSuccess, conditionalObject.String);
                if (actual.IsSuccess)
                    Assert.AreEqual(input.Item5, actual.Value, conditionalObject.String);
            }
        }


        [TestMethod]
        public void GetState_ShouldEvaluateGreaterThanCorrectly()
        {
            foreach (var input in new List<Tuple<string, string, string, bool, bool>>()
            {
                Tuple.Create("1", ConditionalOperator.GreaterThan, "10", true, false),
                Tuple.Create("300", ConditionalOperator.GreaterThan, "30", true, true),
                Tuple.Create("bar", ConditionalOperator.GreaterThan, "bar", false, false)
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    String = input.Item1 + input.Item2 + input.Item3,
                    LeftSide = input.Item1,
                    Operator = input.Item2,
                    RightSide = input.Item3,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.AreEqual(input.Item4, actual.IsSuccess, conditionalObject.String);
                if (actual.IsSuccess)
                    Assert.AreEqual(input.Item5, actual.Value, conditionalObject.String);
                if (actual.IsFailure)
                    Assert.IsTrue(actual.Error.Message.EndsWith(ConditionalObjectLogic.UnknownOperatorError), conditionalObject.String);
            }
        }

        [TestMethod]
        public void GetState_ShouldEvaluateGreaterThanOrEqualToCorrectly()
        {
            foreach (var input in new List<Tuple<string, string, string, bool, bool>>()
            {
                Tuple.Create("1", ConditionalOperator.GreaterThanOrEqualTo, "10", true, false),
                Tuple.Create("20", ConditionalOperator.GreaterThanOrEqualTo, "20", true, true),
                Tuple.Create("300", ConditionalOperator.GreaterThanOrEqualTo, "30", true, true),
                Tuple.Create("bar", ConditionalOperator.GreaterThanOrEqualTo, "bar", false, false)
            })
            {
                var conditionalObject = new ConditionalObject
                {
                    String = input.Item1 + input.Item2 + input.Item3,
                    LeftSide = input.Item1,
                    Operator = input.Item2,
                    RightSide = input.Item3,
                };

                var actual = _service.Evaluate(conditionalObject);

                Assert.AreEqual(input.Item4, actual.IsSuccess, conditionalObject.String);
                if (actual.IsSuccess)
                    Assert.AreEqual(input.Item5, actual.Value, conditionalObject.String);
            }
        }

    }
}
