namespace TeaseAI.Common.Interfaces.Timers
{
    public interface ITimerFactory
    {
        /// <summary>
        /// Create a new ITimer
        /// </summary>
        /// <returns></returns>
        ITimer Create();
    }
}
