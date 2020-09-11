Option Explicit On
Option Strict On

Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Data
Imports TeaseAI.Data.Interfaces
Imports TeaseAI.Data.Repositories
Imports TeaseAI.Services
Imports TeaseAI.Services.Services

Public Class ApplicationFactory

    Public Shared Function CreatePathsAccessor() As IPathsAccessor
        Return New Accessors.PathsAccessor(CreateConfigurationAccessor())
    End Function

    Public Shared Function CreateConfigurationAccessor() As IConfigurationAccessor
        Return New Accessors.ConfigurationAccessor()
    End Function

    <Obsolete("switch to CreateSettingsAccessor as soon as possible")>
    Public Shared Function CreateOldSettingsAccessor() As SettingsAccessor
        Return New SettingsAccessor()
    End Function

    Public Shared Function CreateSettingsAccessor() As ISettingsAccessor
        Return New Accessors.SettingsAccessor(CreateConfigurationAccessor())
    End Function

    Friend Shared Function CreateLoadFileData() As ILoadFileData
        Return New LoadFileData()
    End Function

    Friend Shared Function CreateScriptAccessor() As IScriptAccessor
        Return New Accessors.ScriptAccessor(CreatePathsAccessor(), CreateCldAccessor())
    End Function

    Public Shared Function CreateCldAccessor() As ICldAccessor
        Return New Accessors.CldAccessor()
    End Function

    Public Shared Function CreateGetCommandProcessorsService() As IGetCommandProcessorsService
        Return New GetCommandProcessorsService(CreateScriptAccessor() _
            , New FlagAccessor() _
            , New LineService _
            , CreateImageMetaDataService() _
            , New VideoAccessor() _
            , New VariableAccessor _
            , New TauntAccessor() _
            , CreateConfigurationAccessor() _
            , CreateRandomNumberService() _
            , New NotifyUser() _
            , CreateOldSettingsAccessor() _
            , CreatePathsAccessor() _
            , New BookmarkService() _
            , CreateMediaContainerService() _
            , CreateTimeService() _
            , CreateLineCollectionFilter() _
            , CreateVitalSubService())
    End Function

    Private Shared Function CreateLineCollectionFilter() As ILineCollectionFilter
        Return New LineCollectionFilter()
    End Function

    Private Shared Function CreateTimeService() As ITimeService
        Return New TimeService(CreateSettingsAccessor())
    End Function

    Public Shared Function CreateImageMetaDataService() As IImageAccessor
        Return New Accessors.ImageAccessor(CreateConfigurationAccessor() _
                                           , CreatePathsAccessor() _
                                           , CreateImageMetaDataRepository() _
                                           , CreateMediaContainerService())
    End Function

    Public Shared Function CreateVitalSubService() As IVitalSubService
        Return New VitalSubService(CreatePathsAccessor(), CreateLineCollectionFilter(), CreateRandomNumberService())
    End Function

    Public Shared Function CreateRandomNumberService() As IRandomNumberService
        Return New RandomNumberService()
    End Function

    Public Shared Function CreateImageMetaDataRepository() As IImageMetaDataRepository
        Return New ImageMetaDataRepository(CreateConfigurationAccessor())
    End Function

    Public Shared Function CreateSessionEngine() As SessionEngine
        Return New SessionEngine(CreateSettingsAccessor(), New StringService(), CreateScriptAccessor(), New TimerFactory(), New FlagAccessor(), CreateImageMetaDataService(), New VideoAccessor(), New VariableAccessor(), New TauntAccessor(), New SystemVocabularyAccessor(), New VocabularyAccessor(), New LineCollectionFilter(), New RandomNumberService(), CreateConfigurationAccessor(), New NotifyUser(), CreatePathsAccessor(), CreateGetCommandProcessorsService())
    End Function

    Friend Shared Function CreateImageBlogDownloadService() As IImageBlogDownloadService
        Return New TumblrImageBlogRepository()
    End Function

    Friend Shared Function CreateNotifyUserService() As INotifyUser
        Return New NotifyUser()
    End Function

    Public Shared Function CreateGenreService() As IGenreService
        Return New GenreService(RepositoryFactory.CreateGenreRepository(CreateConfigurationAccessor()))
    End Function

    Public Shared Function CreateMediaContainerService() As IMediaContainerService
        Return New MediaContainerService(RepositoryFactory.CreateMediaContainerRepository(CreateConfigurationAccessor()), RepositoryFactory.CreateGenreRepository(CreateConfigurationAccessor()))
    End Function

    Friend Shared Function CreateItemTagService() As IItemTagService
        Return New ItemTagService(RepositoryFactory.CreateItemTagRepository(CreateConfigurationAccessor()))
    End Function

    Friend Shared Function CreateImageTagMapService() As IImageTagMapService
        Return New ImageTagMapService(RepositoryFactory.CreateItemTagMapRepository(CreateConfigurationAccessor()))
    End Function
End Class
