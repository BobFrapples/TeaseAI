using System;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// How much the dom cares about the sub
    /// </summary>
    public struct ApathyLevel : IEquatable<ApathyLevel>
    {

        public static Result<ApathyLevel> Create(string value)
        {
            switch (value)
            {
                case "Cautious":
                    return Result.Ok(Cautious);
                case "Caring":
                    return Result.Ok(Caring);
                case "Moderate":
                    return Result.Ok(Moderate);
                case "Cruel":
                    return Result.Ok(Cruel);
                case "Merciless":
                    return Result.Ok(Merciless);
                default:
                    return Result.Fail<ApathyLevel>("value must be one of Cautious, Caring, Moderate, Cruel, Merciless");
            }
        }

        public static Result<ApathyLevel> Create(int value)
        {
            switch (value)
            {
                case 1:
                    return Result.Ok(Cautious);
                case 2:
                    return Result.Ok(Caring);
                case 3:
                    return Result.Ok(Moderate);
                case 4:
                    return Result.Ok(Cruel);
                case 5:
                    return Result.Ok(Merciless);
                default:
                    return Result.Fail<ApathyLevel>("value must be from 1 to 5");
            }
        }

        private ApathyLevel(int value)
        {
            _value = value;
        }

        #region definitions
        public static ApathyLevel Cautious => new ApathyLevel(1);

        public static ApathyLevel Caring => new ApathyLevel(2);

        public static ApathyLevel Moderate => new ApathyLevel(3);

        public static ApathyLevel Cruel => new ApathyLevel(4);

        public static ApathyLevel Merciless => new ApathyLevel(5);
        #endregion

        public bool Equals(ApathyLevel other) => _value == other._value;

        public override bool Equals(object other) => Equals((ApathyLevel)other);

        public override int GetHashCode() => _value.GetHashCode();

        #region casting
        public static implicit operator int(ApathyLevel value)
        {
            return value._value;
        }

        public static explicit operator ApathyLevel(int value) => Create(value).Value;

        public static explicit operator ApathyLevel(string value) => Create(value).Value;
        #endregion

        public override string ToString()
        {
            switch (_value)
            {
                case 1:
                    return nameof(Cautious);
                case 2:
                    return nameof(Caring);
                case 3:
                    return nameof(Moderate);
                case 4:
                    return nameof(Cruel);
                case 5:
                    return nameof(Merciless);
            }
            throw new Exception("_value was invalid. " + _value.ToString());
        }


        #region math operations
        public static ApathyLevel operator -(ApathyLevel value, int change) => new ApathyLevel(Math.Max(value._value - change, Cautious));
        public static ApathyLevel operator --(ApathyLevel value) => new ApathyLevel(Math.Max(value._value - 1, Cautious));

        public static ApathyLevel operator +(ApathyLevel value, int change) => new ApathyLevel(Math.Min(value._value + change, Merciless));
        public static ApathyLevel operator ++(ApathyLevel value) => new ApathyLevel(Math.Min(value._value + 1, Merciless));

        public static bool operator ==(ApathyLevel value, ApathyLevel other) => value.Equals(other);
        public static bool operator ==(ApathyLevel value, int other) => value.Equals(new ApathyLevel(other));

        public static bool operator !=(ApathyLevel value, ApathyLevel other) => !value.Equals(other);
        public static bool operator !=(ApathyLevel value, int other) => !value.Equals(new ApathyLevel(other));
        #endregion

        private int _value;
    }
}
