using System;

namespace TeaseAI.Common.Constants
{
    public class CupSize : IEquatable<CupSize>
    {

        public static Result<CupSize> Create(string value)
        {
            switch (value)
            {
                case "A":
                case "ACup":
                    return Result.Ok(ACup);
                case "B":
                case "BCup":
                    return Result.Ok(BCup);
                case "C":
                case "CCup":
                    return Result.Ok(CCup);
                case "D":
                case "DCup":
                    return Result.Ok(DCup);
                case "DD":
                case "DDCup":
                    return Result.Ok(DdCup);
                case "DDD+":
                case "DDD+Cup":
                    return Result.Ok(DdCup);
                default:
                    return Result.Fail<CupSize>("value must be one of ACup, BCup, CCup, DCup, DDCup, DDD+Cup");
            }
        }

        public static Result<CupSize> Create(int value)
        {
            switch (value)
            {
                case 1:
                    return Result.Ok(ACup);
                case 2:
                    return Result.Ok(BCup);
                case 3:
                    return Result.Ok(CCup);
                case 4:
                    return Result.Ok(DCup);
                case 5:
                    return Result.Ok(DdCup);
                case 6:
                    return Result.Ok(DddCup);
                default:
                    return Result.Fail<CupSize>("value must be from 1 to 5");
            }
        }

        private CupSize()
        {
        }

        private CupSize(int value)
        {
            _value = value;
        }

        #region definitions
        public static CupSize ACup => new CupSize(1);

        public static CupSize BCup => new CupSize(2);

        public static CupSize CCup => new CupSize(3);

        public static CupSize DCup => new CupSize(4);

        public static CupSize DdCup => new CupSize(5);
        public static CupSize DddCup => new CupSize(6);
        #endregion

        public bool Equals(CupSize other)
        {
            return _value == other?._value;
        }

        public override bool Equals(object other)
        {
            return Equals(other as CupSize);
        }

        public override int GetHashCode()
        {
            return -1939223833 + _value.GetHashCode();
        }

        #region casting
        public static implicit operator int(CupSize value)
        {
            return value._value;
        }

        public static explicit operator CupSize(int value) => Create(value).Value;

        public static explicit operator CupSize(string value) => Create(value).Value;
        #endregion

        public override string ToString()
        {
            switch (_value)
            {
                case 1:
                    return nameof(ACup);
                case 2:
                    return nameof(BCup);
                case 3:
                    return nameof(CCup);
                case 4:
                    return nameof(DCup);
                case 5:
                    return nameof(DdCup);
                case 6:
                    return "DDD+Cup";
            }
            throw new Exception("_value was invalid. " + _value.ToString());
        }

        #region math operations
        public static CupSize operator -(CupSize value, int change) => new CupSize(Math.Max(value._value - change, ACup));
        public static CupSize operator --(CupSize value) => new CupSize(Math.Max(value._value - 1, ACup));

        public static CupSize operator +(CupSize value, int change) => new CupSize(Math.Min(value._value + change, DddCup));
        public static CupSize operator ++(CupSize value) => new CupSize(Math.Min(value._value + 1, DddCup));

        public static bool operator ==(CupSize value, CupSize other) => value.Equals(other);
        public static bool operator ==(CupSize value, int other) => value.Equals(new CupSize(other));

        public static bool operator !=(CupSize value, CupSize other) => !value.Equals(other);
        public static bool operator !=(CupSize value, int other) => !value.Equals(new CupSize(other));
        #endregion

        private int _value = 1;
    }
}
