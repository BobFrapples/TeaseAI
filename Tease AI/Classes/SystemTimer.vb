Imports TeaseAI.Common.Interfaces.Timers

Public Class SystemTimer
    Implements ITimer
    Public Sub New()
        myTimer = New Timers.Timer()
    End Sub

    Public Property AutoReset As Boolean Implements ITimer.AutoReset
        Get
            Return myTimer.AutoReset
        End Get
        Set(value As Boolean)
            myTimer.AutoReset = value
        End Set
    End Property

    Public Property Interval As Double Implements ITimer.Interval
        Get
            Return myTimer.Interval
        End Get
        Set(value As Double)
            myTimer.Interval = value
        End Set
    End Property

    Public Property Enabled As Boolean Implements ITimer.Enabled
        Get
            Return myTimer.Enabled
        End Get
        Set(value As Boolean)
            myTimer.Enabled = value
        End Set
    End Property

    Public Event Elapsed As EventHandler(Of EventArgs) Implements ITimer.Elapsed

    Private Sub myTimer_Elapsed(s As Object, e As EventArgs) Handles myTimer.Elapsed
        RaiseEvent Elapsed(Me, e)
    End Sub

    Dim WithEvents myTimer As System.Timers.Timer
End Class
