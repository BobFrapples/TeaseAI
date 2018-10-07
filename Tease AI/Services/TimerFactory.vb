Imports TeaseAI.Common.Interfaces.Timers

Public Class TimerFactory
    Implements ITimerFactory

    Public Function Create() As ITimer Implements ITimerFactory.Create
        Return New SystemTimer()
    End Function
End Class
