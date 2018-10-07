using System;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// What is the current mood of the Domme, Higher is happier
    /// </summary>
    public struct MoodLevel : IEquatable<MoodLevel>
    {
        public const int MaxValue = 10;
        public const int MinValue = 0;

        public static Result<MoodLevel> Create(int value)
        {
            if (MinValue <= value && value <= MaxValue)
                return Result.Ok(new MoodLevel(value));
            return Result.Fail<MoodLevel>("value must be from " + MinValue.ToString() + " to " + MaxValue.ToString());
        }

        private MoodLevel(int value)
        {
            _value = value;
        }

        public bool Equals(MoodLevel other)
        {
            return _value == other._value;
        }
        public override bool Equals(object other) => Equals((MoodLevel)other);

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #region casting
        public static implicit operator int(MoodLevel value)
        {
            return value._value;
        }

        public static explicit operator MoodLevel(int value) => Create(value).Value;
        #endregion

        public override string ToString()
        {
            return _value.ToString();
        }

        #region math operations
        public static MoodLevel operator -(MoodLevel value, int change) => new MoodLevel(Math.Max(value._value - change, MinValue));
        public static MoodLevel operator --(MoodLevel value) => new MoodLevel(Math.Max(value._value - 1, MinValue));

        public static MoodLevel operator +(MoodLevel value, int change) => new MoodLevel(Math.Min(value._value + change, MaxValue));
        public static MoodLevel operator ++(MoodLevel value) => new MoodLevel(Math.Min(value._value + 1, MaxValue));

        public static bool operator ==(MoodLevel value, MoodLevel other) => value.Equals(other);
        public static bool operator ==(MoodLevel value, int other) => value.Equals(new MoodLevel(other));

        public static bool operator !=(MoodLevel value, MoodLevel other) => !value.Equals(other);
        public static bool operator !=(MoodLevel value, int other) => !value.Equals(new MoodLevel(other));
        #endregion

        private int _value;
    }
}
