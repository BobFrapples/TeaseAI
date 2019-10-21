Option Infer Off
Option Strict On
Imports TeaseAI.Common.Interfaces.Accessors

Public Class PathsAccessor
    Private mySettingsAccessor As ISettingsAccessor
    Private myApplicationPath As String

    Sub New(applicationPath As String, settingsAccessor As ISettingsAccessor)
        mySettingsAccessor = settingsAccessor
        myApplicationPath = applicationPath
    End Sub

    ''' <summary>
    ''' Returns the Path for the selected personality. Ends with Backslash!
    ''' </summary>
    ''' <returns>The Path for the selected personality. Ends with Backslash!</returns>
    Public ReadOnly Property Personality As String
        Get
            Return String.Format("{0}\Scripts\{1}\", myApplicationPath, mySettingsAccessor.DommePersonality)
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

    Public Property NoUrlFilesSelected As String = myApplicationPath & "\Images\System\NoURLFilesSelected.jpg"
    Public ReadOnly LikedImages As String = myApplicationPath & "\Images\System\LikedImageURLs.txt"
    Public ReadOnly DislikedImages As String = myApplicationPath & "\Images\System\DislikedImageURLs.txt"
    Public ReadOnly LocalImageTags As String = myApplicationPath & "\Images\System\LocalImageTags.txt"

    ''' <summary>
    ''' The default directory URL-Files are located.
    ''' </summary>
    Public ReadOnly UrlsDirectory As String = myApplicationPath & "\Images\System\URL Files\"

    Public ReadOnly PathImageErrorOnLoading As String = myApplicationPath & "\Images\System\ErrorLoadingImage.jpg"
    Public ReadOnly PathImageErrorNoLocalImages As String = myApplicationPath & "\Images\System\NoLocalImagesFound.jpg"

    Public ReadOnly SavedSessionDefaultPath As String = myApplicationPath & "\System\SavedState.save"
End Class

