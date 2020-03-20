using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class BookmarkService : IBookmarkService
    {
        public Result<int> FindBookmark(IEnumerable<string> script, string bookmark)
        {
            for (var i = 0; i < script.Count(); i++)
            {
                if (script.ElementAt(i) == bookmark)
                    return Result.Ok(i);
            }
            return Result.Fail<int>("Bookmark " + bookmark + " is not in this script.");
        }
    }
}
