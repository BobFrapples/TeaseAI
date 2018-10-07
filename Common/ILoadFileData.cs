namespace TeaseAI.Common
{
    /// <summary>
    /// Load a string of data from a file.
    /// </summary>
    public interface ILoadFileData
    {
        /// <summary>
        /// Load the filename and return the contents
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Result<string> ReadData(string fileName);
    }
}
