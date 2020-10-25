namespace TeaseAI.Common.Interfaces
{
    /// <summary>
    /// This is used to replace variables, settings, session information, etc *before* command or vocabulary processing.
    /// </summary>
    public interface IInterpolationProcessor
    {

        /// <summary>
        /// Attempt to parse and confirm an interpolation in the line is valid.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        Result Parse(Session session, string line );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        Result<string> Interpolate(Session session, string line );
    }
}
