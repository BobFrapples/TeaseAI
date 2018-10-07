using System;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public struct RuinsOrgasms : IEquatable<RuinsOrgasms>
    {
        private RuinsOrgasms(int value)
        {
            _value = value;
        }

        #region definitions
        /// <summary>
        /// Sub has 0 percent change of an orgasm
        /// </summary>
        public static RuinsOrgasms Never => new RuinsOrgasms(0);

        /// <summary>
        /// Sub has 25 percent change of an orgasm
        /// </summary>
        public static RuinsOrgasms Rarely => new RuinsOrgasms(25);

        /// <summary>
        /// Sub has 50 percent change of an orgasm
        /// </summary>
        public static RuinsOrgasms Sometimes => new RuinsOrgasms(50);

        /// <summary>
        /// Sub has 75 percent change of an orgasm
        /// </summary>
        public static RuinsOrgasms Often => new RuinsOrgasms(75);

        /// <summary>
        /// Sub has 100 percent change of an orgasm
        /// </summary>
        public static RuinsOrgasms Always => new RuinsOrgasms(100);
        #endregion

        public bool Equals(RuinsOrgasms other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Implicity converts the orgasm value to a percent success [ 0 | 25 | 50 | 75 | 100 ]
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(RuinsOrgasms value)
        {
            return value._value;
        }

        public static explicit operator RuinsOrgasms(int value) => Create(value).Value;

        public static explicit operator RuinsOrgasms(string value) => Create(value).Value;

        private static Result<RuinsOrgasms> Create(string value)
        {
            switch (value)
            {
                case "Never":
                    return Result.Ok(Never);
                case "Rarely":
                    return Result.Ok(Rarely);
                case "Sometimes":
                    return Result.Ok(Sometimes);
                case "Often":
                    return Result.Ok(Often);
                case "Always":
                    return Result.Ok(Always);
                default:
                    return Result.Fail<RuinsOrgasms>("value must be one of Never, Rarely, Sometimes, Often, Always");
            }
        }

        public override string ToString()
        {
            switch (_value)
            {
                case 0:
                    return nameof(Never);
                case 25:
                    return nameof(Rarely);
                case 50:
                    return nameof(Sometimes);
                case 75:
                    return nameof(Often);
                case 100:
                    return nameof(Always);
            }
            throw new Exception("_value was invalid. " + _value.ToString());
        }

        public static Result<RuinsOrgasms> Create(int value)
        {
            switch (value)
            {
                case 0:
                    return Result.Ok(Never);
                case 25:
                    return Result.Ok(Rarely);
                case 50:
                    return Result.Ok(Sometimes);
                case 75:
                    return Result.Ok(Often);
                case 100:
                    return Result.Ok(Always);
                default:
                    return Result.Fail<RuinsOrgasms>("value must be one of 0, 25, 50, 75, 100");
            }
        }

        public static RuinsOrgasms operator ++(RuinsOrgasms value) => new RuinsOrgasms(Math.Min(value._value + 1, Always));
        public static RuinsOrgasms operator --(RuinsOrgasms value) => new RuinsOrgasms(Math.Max(value._value - 1, Always));
        public static bool operator ==(RuinsOrgasms value, RuinsOrgasms other) => value.Equals(other);
        public static bool operator ==(RuinsOrgasms value, int other) => value.Equals(new RuinsOrgasms(other));

        public static bool operator !=(RuinsOrgasms value, RuinsOrgasms other) => !value.Equals(other);
        public static bool operator !=(RuinsOrgasms value, int other) => !value.Equals(new RuinsOrgasms(other));

        private int _value;
    }
}
