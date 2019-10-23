Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class ServiceFactory

    Public Shared Function CreatePathsAccessor() As PathsAccessor
        Return New PathsAccessor(CreateConfigurationAccessor(), CreateSettingsAccessor())
    End Function

    Public Shared Function CreateConfigurationAccessor() As IConfigurationAccessor
        Return New ConfigurationAccessor()
    End Function

    Public Shared Function CreateSettingsAccessor() As ISettingsAccessor
        Return New SettingsAccessor()
    End Function

    Friend Shared Function CreateLoadFileData() As ILoadFileData
        Return New LoadFileData()
    End Function
End Class
