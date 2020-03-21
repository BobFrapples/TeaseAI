Imports Tease_AI
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Services

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

    Public Shared Function CreateGetCommandProcessorsService() As IGetCommandProcessorsService
        Return New GetCommandProcessorsService(CreateScriptAccessor(), New FlagAccessor(), New LineService, New ImageAccessor(), New VideoAccessor(), New VariableAccessor, New TauntAccessor(), New ConfigurationAccessor(), New RandomNumberService(), New NotifyUser(), CreateSettingsAccessor(), CreatePathsAccessor(), New BookmarkService())
    End Function

    Public Shared Function CreateSessionEngine() As SessionEngine
        Return New SessionEngine(CreateSettingsAccessor(), New StringService(), CreateScriptAccessor(), New TimerFactory(), New FlagAccessor(), New ImageAccessor(), New VideoAccessor(), New VariableAccessor(), New TauntAccessor(), New SystemVocabularyAccessor(), New VocabularyAccessor(), New LineCollectionFilter(), New RandomNumberService(), CreateConfigurationAccessor(), New NotifyUser(), CreatePathsAccessor(), CreateGetCommandProcessorsService())
    End Function
End Class
