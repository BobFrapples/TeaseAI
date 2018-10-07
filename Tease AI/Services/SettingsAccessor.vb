Imports Tease_AI.My
Imports TeaseAI.Common.Interfaces.Accessors

Public Class SettingsAccessor
    Implements ISettingsAccessor

    Public Function GetGreetings() As List(Of String) Implements ISettingsAccessor.GetGreetings
        Return MySettings.Default.SubGreeting.Split(",").Select(Function(str) str.Trim()).ToList()
    End Function
End Class
