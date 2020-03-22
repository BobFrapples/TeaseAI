Option Infer Off
Option Strict On
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Interfaces.Accessors

Public Class PathsAccessor
    Implements IPathsAccessor
    Sub New(configurationAccessor As IConfigurationAccessor, settingsAccessor As ISettingsAccessor)
        mySettingsAccessor = settingsAccessor
        myConfigurationAccessor = configurationAccessor
    End Sub

    ''' <summary>
    ''' Returns the Path for the selected personality. Ends with Backslash!
    ''' </summary>
    ''' <returns>The Path for the selected personality. Ends with Backslash!</returns>
    Public ReadOnly Property Personality As String
        Get
            Return String.Format("{0}\Scripts\{1}\", myConfigurationAccessor.GetBaseFolder(), mySettingsAccessor.DommePersonality)
        End Get
    End Property

    Public ReadOnly Property PersonalitySystem As String
        Get
            Return String.Format("{0}System\", Personality)
        End Get
    End Property

    ''' <summary>
    ''' Returns the Path for the selected personalities flags. Ends with Backslash!
    ''' </summary>
    ''' <returns>The Path for the selected personalities flags. Ends with Backslash!</returns>
    Public ReadOnly Property Flags As String
        Get
            Return String.Format("{0}System\Flags\", Personality)
        End Get
    End Property

    ''' <summary>
    ''' Returns the Path for the selected personalities temporary flags. Ends with Backslash!
    ''' </summary>
    ''' <returns>The Path for the selected personalities temporary flags. Ends with Backslash!</returns>
    Public ReadOnly Property TempFlags As String
        Get
            Return String.Format("{0}Temp\", Flags)
        End Get
    End Property

    ''' <summary>
    ''' Returns the Path for the selected personalities variables. Ends with Backslash!
    ''' </summary>
    ''' <returns>The Path for the selected personalities variables. Ends with Backslash!</returns>
    Public ReadOnly Property Variables As String
        Get
            Return String.Format("{0}System\Variables\", Personality)
        End Get
    End Property

    Public ReadOnly Property StartScripts As String
        Get
            Return String.Format("{0}Stroke\Start\", Personality)
        End Get
    End Property

    Public ReadOnly Property LinkScripts As String
        Get
            Return String.Format("{0}Stroke\Link\", Personality)
        End Get
    End Property

    Public ReadOnly Property ModuleScripts As String
        Get
            Return String.Format("{0}Modules\", Personality)
        End Get
    End Property

    Public ReadOnly Property EndScripts As String
        Get
            Return String.Format("{0}Stroke\End\", Personality)
        End Get
    End Property

    Public ReadOnly Property NoUrlFilesSelected As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\NoURLFilesSelected.jpg"
        End Get
    End Property

    Public ReadOnly Property LikedImages As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\LikedImageURLs.txt"
        End Get
    End Property

    Public ReadOnly Property DislikedImages As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\DislikedImageURLs.txt"
        End Get
    End Property

    Public ReadOnly Property LocalImageTags As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\LocalImageTags.txt"
        End Get
    End Property

    ''' <summary>
    ''' The default directory URL-Files are located.
    ''' </summary>
    Public ReadOnly Property UrlsDirectory As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\URL Files\"
        End Get
    End Property

    Public ReadOnly Property PathImageErrorOnLoading As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\ErrorLoadingImage.jpg"
        End Get
    End Property

    Public ReadOnly Property PathImageErrorNoLocalImages As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\Images\System\NoLocalImagesFound.jpg"
        End Get
    End Property

    Public ReadOnly Property SavedSessionDefaultPath As String
        Get
            Return myConfigurationAccessor.GetBaseFolder() & "\System\SavedState.save"
        End Get
    End Property

    Public ReadOnly Property RiskyPickScript As String Implements IPathsAccessor.RiskyPickScript
        Get
            Return Personality & "Apps\Games\Risky Pick\Risky Pick.txt"
        End Get
    End Property

    Public Function GetPersonalityFolder() As String Implements IPathsAccessor.GetPersonalitiesFolder
        Throw New NotImplementedException()
    End Function

    Public Function GetPersonalityFolder(dommePersonalityName As String) As String Implements IPathsAccessor.GetPersonalityFolder
        Throw New NotImplementedException()
    End Function

    Public Function GetScriptDir(dommePersonalityName As String, type As String, sessionPhase As SessionPhase) As String Implements IPathsAccessor.GetScriptDir
        Throw New NotImplementedException()
    End Function

    Public Function GetScriptCld(dommePersonalityName As String, sessionPhase As SessionPhase) As String Implements IPathsAccessor.GetScriptCld
        Throw New NotImplementedException()
    End Function

    Public Function GetSystemImages() As String Implements IPathsAccessor.GetSystemImages
        Return myConfigurationAccessor.GetBaseFolder() + IO.Path.DirectorySeparatorChar + "Images" + IO.Path.DirectorySeparatorChar + "System"
    End Function

    Private mySettingsAccessor As ISettingsAccessor
    Private myConfigurationAccessor As IConfigurationAccessor
End Class

