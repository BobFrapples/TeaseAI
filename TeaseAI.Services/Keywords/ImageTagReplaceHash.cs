using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.Keywords
{
    /// <summary>
    /// Service to find and replace Hash keywords
    /// </summary>
    public class ImageTagReplaceHash
    {
        public string ReplaceImageTags(string messageString, TaggedItem slide)
        {
            try
            {
                // nothing to do
                if (slide == null)
                    return messageString;
                var newMessage = messageString.Replace("#TagGarment", GetReplacementString(slide.ItemTags, "TagGarment", ItemTag.GarmentCovering, "garment"));
                newMessage = newMessage.Replace("#TagUnderwear", GetReplacementString(slide.ItemTags, "TagUnderwear", ItemTag.Underwear, "underwear"));
                newMessage = newMessage.Replace("#TagTattoo", GetReplacementString(slide.ItemTags, "TagTattoo", ItemTag.Tattoo, "tattoo"));
                newMessage = newMessage.Replace("#TagSexToy", GetReplacementString(slide.ItemTags, "TagSexToy", ItemTag.SexToy, "sex toy"));
                newMessage = newMessage.Replace("#TagFurniture", GetReplacementString(slide.ItemTags, "TagFurniture", ItemTag.Furniture, "furniture"));

                return messageString;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// finds variable tags and gets the variable data
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="tagStart"></param>
        /// <param name="itemTag"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetReplacementString(List<ItemTag> tags, string tagStart, ItemTag itemTag, string defaultValue)
        {
            var replacementString = defaultValue;
            if (tags.Any(tag => tag.ToString().Contains(tagStart)))
            {
                var coveredByTag = tags.FirstOrDefault(tag => tag.ToString().StartsWith(tagStart) && tag != itemTag);
                if ((coveredByTag != null))
                    replacementString = coveredByTag.ToString().Replace(tagStart, "").Replace("-", " ").ToLower();
            }
            return replacementString;
        }
    }
}
