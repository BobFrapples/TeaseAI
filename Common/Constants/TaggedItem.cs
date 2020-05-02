using System;
using System.Diagnostics;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// Possible tags for a TaggedItem
    /// </summary>
    [DebuggerDisplay("{_value}")]
    public struct TaggedItem : IEquatable<TaggedItem>
    {
        /// <summary>
        /// private constructure ensures correct data
        /// </summary>
        /// <param name="name"></param>
        private TaggedItem(string name)
        {
            _value = name;
        }

        #region Available tags
        /// <summary>
        /// This item has Face
        /// </summary>
        public static TaggedItem Face => new TaggedItem("Tag" + nameof(Face));

        /// <summary>
        /// This item has Furniture
        /// </summary>
        public static TaggedItem Furniture => new TaggedItem("Tag" + nameof(Furniture));

        /// <summary>
        /// This item has a garment
        /// </summary>
        public static TaggedItem GarmentCovering => new TaggedItem("Tag" + nameof(GarmentCovering));

        /// <summary>
        /// This item has Naked
        /// </summary>
        public static TaggedItem Naked => new TaggedItem("Tag" + nameof(Naked));

        /// <summary>
        /// This item has SexToy
        /// </summary>
        public static TaggedItem SexToy => new TaggedItem("Tag" + nameof(SexToy));

        /// <summary>
        /// This item has Tattoo
        /// </summary>
        public static TaggedItem Tattoo => new TaggedItem("Tag" + nameof(Tattoo));

        /// <summary>
        /// This item has underwear
        /// </summary>
        public static TaggedItem Underwear => new TaggedItem("Tag" + nameof(Underwear));

        /// <summary>
        /// This item has Smiling
        /// </summary>
        public static TaggedItem Smiling => new TaggedItem("Tag" + nameof(Smiling));

        /// <summary>
        /// This item has CloseUp
        /// </summary>
        public static TaggedItem CloseUp => new TaggedItem("Tag" + nameof(CloseUp));

        public static string FullyDressed => "FullyDressed";
        public static string SideView => "SideView";
        public static string Boobs => "Boobs";
        public static string Glaring => "Glaring";
        public static string HalfDressed => "HalfDressed";
        #endregion

        public bool Equals(TaggedItem other)
        {
            return _value == other._value;
        }

        public static implicit operator string(TaggedItem value)
        {
            return value._value;
        }

        public static explicit operator TaggedItem(string value) => Create(value).Value;

        public override string ToString() => _value;

        public static Result<TaggedItem> Create(string value)
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
                return Result.Ok(new TaggedItem("TagFullyDressed"));
            else if (value == "SideView")
                return Result.Ok(new TaggedItem("TagSideView"));
            else if (value == "Boobs")
                return Result.Ok(new TaggedItem("TagBoobs"));
            else if (value == "GarmentCovering")
                return Result.Ok(new TaggedItem("TagGarmentCovering"));
            else if (value == "Glaring")
                return Result.Ok(new TaggedItem("TagGlaring"));
            else if (value == "HalfDressed")
                return Result.Ok(new TaggedItem("TagHalfDressed"));
            else if  (value.StartsWith("Tag"))
                return Result.Ok(new TaggedItem(value));

            return Result.Fail<TaggedItem>(value + " is an unknown tag.");
        }

        private string _value;
    }
}
