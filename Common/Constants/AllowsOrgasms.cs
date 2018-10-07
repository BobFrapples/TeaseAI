using System;


namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public struct AllowsOrgasms : IEquatable<AllowsOrgasms>
    {
        private AllowsOrgasms(int value)
        {
            _value = value;
        }

        #region definitions
        /// <summary>
        /// Sub has 0 percent change of an orgasm
        /// </summary>
        public static AllowsOrgasms Never => new AllowsOrgasms(0);

        /// <summary>
        /// Sub has 25 percent change of an orgasm
        /// </summary>
        public static AllowsOrgasms Rarely => new AllowsOrgasms(25);

        /// <summary>
        /// Sub has 50 percent change of an orgasm
        /// </summary>
        public static AllowsOrgasms Sometimes => new AllowsOrgasms(50);

        /// <summary>
        /// Sub has 75 percent change of an orgasm
        /// </summary>
        public static AllowsOrgasms Often => new AllowsOrgasms(75);

        /// <summary>
        /// Sub has 100 percent change of an orgasm
        /// </summary>
        public static AllowsOrgasms Always => new AllowsOrgasms(100);
        #endregion

        public bool Equals(AllowsOrgasms other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Implicity converts the orgasm value to a percent success [ 0 | 25 | 50 | 75 | 100 ]
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(AllowsOrgasms value)
        {
            return value._value;
        }

        public static explicit operator AllowsOrgasms(int value) => Create(value).Value;

        public static explicit operator AllowsOrgasms(string value) => Create(value).Value;

        private static Result<AllowsOrgasms> Create(string value)
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
                    return Result.Fail<AllowsOrgasms>("value must be one of Never, Rarely, Sometimes, Often, Always");
            }
        }

        /// <summary>
        /// Returns a string matching how often the orgasm is allowed.
        /// i.e. Never, Rarely, Sometimes, Often, or Always
        /// </summary>
        /// <returns></returns>
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

        public static Result<AllowsOrgasms> Create(int value)
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
                    return Result.Fail<AllowsOrgasms>("value must be one of 0, 25, 50, 75, 100");
            }
        }

        public static AllowsOrgasms operator ++(AllowsOrgasms value) => new AllowsOrgasms(Math.Min(value._value +1, Always));
        public static AllowsOrgasms operator --(AllowsOrgasms value) => new AllowsOrgasms(Math.Max(value._value -1, Always));
        public static bool operator ==(AllowsOrgasms value, AllowsOrgasms other) => value.Equals(other);
        public static bool operator ==(AllowsOrgasms value, int other) => value.Equals(new AllowsOrgasms(other));

        public static bool operator !=(AllowsOrgasms value, AllowsOrgasms other) => !value.Equals(other);
        public static bool operator !=(AllowsOrgasms value, int other) => !value.Equals(new AllowsOrgasms(other));

        private int _value;
    }
}
