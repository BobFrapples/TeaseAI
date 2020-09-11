using System.Collections.Generic;

namespace TeaseAI.Common.Interfaces
{
    /// <summary>
    /// remove lines from a collection that aren't relevant to the session
    /// </summary>
    public interface ILineCollectionFilter
    {
        /// <summary>
        /// read all lines from <paramref name="file"/> that aren't relevent to the session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        List<string> FilterLines(Session session, string file);

        /// <summary>
        /// remove lines from a collection that aren't relevant to the session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        List<string> FilterLines(Session session, IEnumerable<string> lines);
    }
}
