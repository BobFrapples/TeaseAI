using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class BookmarkService : IBookmarkService
    {
        /// <summary>
        /// Find the first instance of <paramref name="bookmark"/> in script. 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bookmark"></param>
        /// <returns></returns>
        public Result<int> FindBookmark(IEnumerable<string> script, string bookmark)
        {
            if (string.IsNullOrWhiteSpace(bookmark))
                return Result.Ok(0);
            if (bookmark[0] != '(')
                bookmark = "(" + bookmark + ")";
            for (var i = 0; i < script.Count(); i++)
            {
                if (script.ElementAt(i) == bookmark)
                    return Result.Ok(i);
            }
            return Result.Fail<int>("Bookmark " + bookmark + " is not in this script.");
        }
    }
}
