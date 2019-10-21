Imports TeaseAI.Common.Interfaces.Accessors

Public Class ServiceFactory

    Public Shared Function CreatePathsAccessor(baseFolder As String) As PathsAccessor
        Return New PathsAccessor(baseFolder, CreateSettingsAccessor())
    End Function

    Public Shared Function CreateSettingsAccessor() As ISettingsAccessor
        Return New SettingsAccessor()
    End Function
End Class
