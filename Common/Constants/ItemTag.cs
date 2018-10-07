using System;
using System.Diagnostics;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// Possible tags for a TaggedItem
    /// </summary>
    [DebuggerDisplay("{_value}")]
    public struct ItemTag : IEquatable<ItemTag>
    {
        /// <summary>
        /// private constructure ensures correct data
        /// </summary>
        /// <param name="name"></param>
        private ItemTag(string name)
        {
            _value = name;
        }

        #region Available tags
        /// <summary>
        /// This item has Face
        /// </summary>
        public static ItemTag Face => new ItemTag("Tag" + nameof(Face));

        /// <summary>
        /// This item has Furniture
        /// </summary>
        public static ItemTag Furniture => new ItemTag("Tag" + nameof(Furniture));

        /// <summary>
        /// This item has a garment
        /// </summary>
        public static ItemTag GarmentCovering => new ItemTag("Tag" + nameof(GarmentCovering));

        /// <summary>
        /// This item has Naked
        /// </summary>
        public static ItemTag Naked => new ItemTag("Tag" + nameof(Naked));

        /// <summary>
        /// This item has SexToy
        /// </summary>
        public static ItemTag SexToy => new ItemTag("Tag" + nameof(SexToy));

        /// <summary>
        /// This item has Tattoo
        /// </summary>
        public static ItemTag Tattoo => new ItemTag("Tag" + nameof(Tattoo));

        /// <summary>
        /// This item has underwear
        /// </summary>
        public static ItemTag Underwear => new ItemTag("Tag" + nameof(Underwear));

        /// <summary>
        /// This item has Smiling
        /// </summary>
        public static ItemTag Smiling => new ItemTag("Tag" + nameof(Smiling));

        /// <summary>
        /// This item has CloseUp
        /// </summary>
        public static ItemTag CloseUp => new ItemTag("Tag" + nameof(CloseUp));
        #endregion

        public bool Equals(ItemTag other)
        {
            return _value == other._value;
        }

        public static implicit operator string(ItemTag value)
        {
            return value._value;
        }

        public static explicit operator ItemTag(string value) => Create(value).Value;

        public override string ToString() => _value;

        public static Result<ItemTag> Create(string value)
        {
            int i = 0;
            if (value == nameof(Face))
                return Result.Ok(Face);
            else if (value == nameof(Furniture))
                return Result.Ok(Furniture);
            else if (value == nameof(GarmentCovering))
                return Result.Ok(GarmentCovering);
            else if (value == nameof(Naked))
                return Result.Ok(Naked);
            else if (value == nameof(SexToy))
                return Result.Ok(SexToy);
            else if (value == nameof(Tattoo))
                return Result.Ok(Tattoo);
            else if (value == nameof(Smiling))
                return Result.Ok(Smiling);
            else if (value == nameof(CloseUp))
                return Result.Ok(CloseUp);
            else if (value == "FullyDressed")
                return Result.Ok(new ItemTag("TagFullyDressed"));
            else if (value == "SideView")
                return Result.Ok(new ItemTag("TagSideView"));
            else if (value == "Boobs")
                return Result.Ok(new ItemTag("TagBoobs"));
            else if (value == "GarmentCovering")
                return Result.Ok(new ItemTag("TagGarmentCovering"));
            else if (value == "Glaring")
                return Result.Ok(new ItemTag("TagGlaring"));
            else if (value == "HalfDressed")
                return Result.Ok(new ItemTag("TagHalfDressed"));
            else if  (value.StartsWith("Tag"))
                return Result.Ok(new ItemTag(value));

            return Result.Fail<ItemTag>(value + " is an unknown tag.");
        }

        private string _value;
    }
}
