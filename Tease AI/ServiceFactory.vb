Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Services

Public Class ServiceFactory

    Public Shared Function CreatePathsAccessor() As IPathsAccessor
        Return New PathsAccessor(CreateConfigurationAccessor(), CreateSettingsAccessor())
    End Function

    Public Shared Function CreateConfigurationAccessor() As IConfigurationAccessor
        Return New Accessors.ConfigurationAccessor()
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
        Return New GetCommandProcessorsService(CreateScriptAccessor() _
            , New FlagAccessor() _
            , New LineService _
            , CreateImageAccessor() _
            , New VideoAccessor() _
            , New VariableAccessor _
            , New TauntAccessor() _
            , CreateConfigurationAccessor() _
            , New RandomNumberService() _
            , New NotifyUser() _
            , CreateSettingsAccessor() _
            , CreatePathsAccessor() _
            , New BookmarkService())
    End Function

    Public Shared Function CreateImageAccessor() As IImageAccessor
        Return New Accessors.ImageAccessor(CreateConfigurationAccessor() _
                                           , CreatePathsAccessor())
    End Function

    Public Shared Function CreateSessionEngine() As SessionEngine
        Return New SessionEngine(CreateSettingsAccessor(), New StringService(), CreateScriptAccessor(), New TimerFactory(), New FlagAccessor(), CreateImageAccessor(), New VideoAccessor(), New VariableAccessor(), New TauntAccessor(), New SystemVocabularyAccessor(), New VocabularyAccessor(), New LineCollectionFilter(), New RandomNumberService(), CreateConfigurationAccessor(), New NotifyUser(), CreatePathsAccessor(), CreateGetCommandProcessorsService())
    End Function
End Class
