using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.TagData
{
    /// <summary>
    /// used for tagging files with content information
    /// </summary>
    public class ParseOldTagDataService
    {
        /// <summary>
        /// Read the tag string and return the tagged items.
        /// This uses the old format stored in .txt files
        /// </summary>
        /// <param name="data">data from the file, *NOT* the filename</param>
        /// <returns></returns>
        public Result<List<Common.TaggedItem>> ParseTagData(string data)
        {
            try
            {
                var result = new List<Common.TaggedItem>();
                var lines = data.Split(Environment.NewLine.ToCharArray());
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    var item = new Common.TaggedItem();
                    var tokens = line.Split(' ').ToList();
                    for (var i = tokens.Count - 1; i >= 0; i--)
                    {
                        // this is unfortunate, but how we have to do it.
                        // If we can't map the tag, assume it's filename
                        var parseTag = FindTag(tokens[i]);
                        if (parseTag.IsFailure)
                            break;

                        item.ItemTags.Add(parseTag.Value);
                        tokens.RemoveAt(i);
                    }
                    item.ItemName = string.Join(" ", tokens);
                    result.Add(item);
                }

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<Common.TaggedItem>>(new Error(ex.Message));
            }
        }

        /// <summary>
        /// This attempts to parse <paramref name="token"/> into an <see cref="Common.Constants.TaggedItem"/>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private Result<Common.Constants.TaggedItem> FindTag(string token)
        {
            var tag = Common.Constants.TaggedItem.Create(token.Replace("Tag", ""));
            if (tag.IsFailure && token.StartsWith("Tag"))
                return Common.Constants.TaggedItem.Create(token);
            else
                return tag;
        }
    }
}
