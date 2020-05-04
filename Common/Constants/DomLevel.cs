using System;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// How rough will the dom be
    /// </summary>
    public struct DomLevel : IEquatable<DomLevel>
    {

        public static Result<DomLevel> Create(string value)
        {
            switch (value)
            {
                case "Gentle":
                    return Result.Ok(Gentle);
                case "Lenient":
                    return Result.Ok(Lenient);
                case "Tease":
                    return Result.Ok(Tease);
                case "Rough":
                    return Result.Ok(Rough);
                case "Sadistic":
                    return Result.Ok(Sadistic);
                default:
                    return Result.Fail<DomLevel>("value must be one of Gentle, Lenient, Tease, Rough, Sadistic");
            }
        }

        public static Result<DomLevel> Create(int value)
        {
            switch (value)
            {
                case 1:
                    return Result.Ok(Gentle);
                case 2:
                    return Result.Ok(Lenient);
                case 3:
                    return Result.Ok(Tease);
                case 4:
                    return Result.Ok(Rough);
                case 5:
                    return Result.Ok(Sadistic);
                default:
                    return Result.Fail<DomLevel>("value must be from 1 to 5");
            }
        }

        private DomLevel(int value)
        {
            _value = value;
        }

        #region definitions
        public static DomLevel Gentle => new DomLevel(1);

        public static DomLevel Lenient => new DomLevel(2);

        public static DomLevel Tease => new DomLevel(3);

        public static DomLevel Rough => new DomLevel(4);

        public static DomLevel Sadistic => new DomLevel(5);
        #endregion

        public bool Equals(DomLevel other)
        {
            return _value == other._value;
        }

        #region casting
        public static implicit operator int(DomLevel value)
        {
            return value._value;
        }

        public static explicit operator DomLevel(int value) => Create(value).Value;

        public static explicit operator DomLevel(string value) => Create(value).Value;
        #endregion

        public override string ToString()
        {
            switch (_value)
            {
                case 1:
                    return nameof(Gentle);
                case 2:
                    return nameof(Lenient);
                case 3:
                    return nameof(Tease);
                case 4:
                    return nameof(Rough);
                case 5:
                    return nameof(Sadistic);
            }
            throw new Exception("_value was invalid. " + _value.ToString());
        }


        #region math operations
        public static DomLevel operator -(DomLevel value, int change) => new DomLevel(Math.Max(value._value - change, Gentle));
        public static DomLevel operator --(DomLevel value) => new DomLevel(Math.Max(value._value - 1, Gentle));

        public static DomLevel operator +(DomLevel value, int change) => new DomLevel(Math.Min(value._value + change, Sadistic));
        public static DomLevel operator ++(DomLevel value) => new DomLevel(Math.Min(value._value + 1, Sadistic));

        public static bool operator ==(DomLevel value, DomLevel other) => value.Equals(other);
        public static bool operator ==(DomLevel value, int other) => value.Equals(new DomLevel(other));

        public static bool operator !=(DomLevel value, DomLevel other) => !value.Equals(other);
        public static bool operator !=(DomLevel value, int other) => !value.Equals(new DomLevel(other));
        #endregion

        private int _value;
    }
}
