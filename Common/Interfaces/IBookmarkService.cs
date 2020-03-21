using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Interfaces
{
    public interface IBookmarkService
    {
        /// <summary>
        /// Get the index of <paramref name="bookmark"/> in <paramref name="script"/>
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bookmark"></param>
        /// <returns></returns>
        Result<int> FindBookmark(IEnumerable<string> script, string bookmark);
    }
}
