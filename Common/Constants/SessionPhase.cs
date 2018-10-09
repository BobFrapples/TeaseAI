namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// The phases of a session in order. 
    /// 1. BeforeSession
    /// 2. Start
    /// 3. Modules
    /// 4. Link
    /// 5. End
    /// 6. AfterSession
    /// </summary>
    public enum SessionPhase
    {
        BeforeSession,
        Start,
        Modules,
        Link,
        End,
        AfterSession,
    }
}
