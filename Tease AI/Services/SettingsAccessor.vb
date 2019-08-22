Imports Tease_AI.My
Imports TeaseAI.Common.Interfaces.Accessors

Public Class SettingsAccessor
    Implements ISettingsAccessor

    Public Function GetGreetings() As List(Of String) Implements ISettingsAccessor.GetGreetings
        Return MySettings.Default.SubGreeting.Split(",").Select(Function(str) str.Trim()).ToList()
    End Function

    Public Function GetDommePersonality() As String Implements ISettingsAccessor.GetDommePersonality
        Return MySettings.Default.DomPersonality
    End Function

    Public Function GetDommeName() As String Implements ISettingsAccessor.GetDommeName
        Return MySettings.Default.DomName
    End Function

    Public Function GetSubName() As String Implements ISettingsAccessor.GetSubName
        Return MySettings.Default.SubName
    End Function

    Friend Function GetDommeAvatarImageName() As String Implements ISettingsAccessor.GetDommeAvatarImageName
        Return MySettings.Default.DomAvatarSave
    End Function
End Class
