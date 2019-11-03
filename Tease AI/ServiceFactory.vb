Imports Tease_AI
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces
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

    Friend Shared Function CreateScriptAccessor() As IScriptAccessor
        Return New ScriptAccessor(CreateCldAccessor())
    End Function

    Public Shared Function CreateCldAccessor() As CldAccessor
        Return New CldAccessor()
    End Function
End Class
