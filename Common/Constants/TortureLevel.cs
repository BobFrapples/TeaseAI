using System;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// Acceptable level of physical pain for the sub
    /// </summary>
    public struct TortureLevel : IEquatable<TortureLevel>
    {
        public const int MaxValue = 5;
        public const int MinValue = 0;

        public static Result<TortureLevel> Create(int value)
        {
            if (MinValue <= value && value <= MaxValue)
                return Result.Ok(new TortureLevel(value));
            return Result.Fail<TortureLevel>("value must be from " + MinValue.ToString() + " to " + MaxValue.ToString());
        }

        private TortureLevel(int value)
        {
            _value = value;
        }

        public bool Equals(TortureLevel other)
        {
            return _value == other._value;
        }
        public override bool Equals(object other) => Equals((TortureLevel)other);

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #region casting
        public static implicit operator int(TortureLevel value)
        {
            return value._value;
        }

        public static explicit operator TortureLevel(int value) => Create(value).Value;
        #endregion

        public override string ToString()
        {
            return _value.ToString();
        }

        #region  operators
        public static TortureLevel operator -(TortureLevel value, int change) => new TortureLevel(Math.Max(value._value - change, MinValue));
        public static TortureLevel operator --(TortureLevel value) => new TortureLevel(Math.Max(value._value - 1, MinValue));

        public static TortureLevel operator +(TortureLevel value, int change) => new TortureLevel(Math.Min(value._value + change, MaxValue));
        public static TortureLevel operator ++(TortureLevel value) => new TortureLevel(Math.Min(value._value + 1, MaxValue));

        public static bool operator ==(TortureLevel value, TortureLevel other) => value.Equals(other);
        public static bool operator ==(TortureLevel value, int other) => value.Equals(new TortureLevel(other));

        public static bool operator !=(TortureLevel value, TortureLevel other) => !value.Equals(other);
        public static bool operator !=(TortureLevel value, int other) => !value.Equals(new TortureLevel(other));
        #endregion

        private int _value;
    }
}
