Option Strict On
Option Infer Off

Imports Tease_AI.My
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Interfaces.Accessors

Public Class SettingsAccessor
    Implements TeaseAI.Common.Interfaces.Accessors.ISettingsAccessor

    Public Function GetGreetings() As List(Of String) Implements ISettingsAccessor.GetGreetings
        Return MySettings.Default.SubGreeting.Split(","(0)).Select(Function(str) str.Trim()).ToList()
    End Function

    Public Sub Save() Implements ISettingsAccessor.Save
        Settings.Save()
    End Sub

    Private Function ISettingsAccessor_GetSettings() As TeaseAI.Common.Settings Implements ISettingsAccessor.GetSettings
        Throw New NotImplementedException()
    End Function

    Public Function WriteSettings(settings As TeaseAI.Common.Settings) As TeaseAI.Common.Settings Implements ISettingsAccessor.WriteSettings
        Throw New NotImplementedException()
    End Function

    Public ReadOnly Property ImageGenreFolder As Dictionary(Of ImageGenre, String) Implements ISettingsAccessor.ImageGenreFolder
        Get
            Dim returnValue As Dictionary(Of ImageGenre, String) = New Dictionary(Of ImageGenre, String)()
            returnValue(ImageGenre.Blowjob) = Settings.IBlowjob
            returnValue(ImageGenre.Boobs) = Settings.LBLBoobPath
            returnValue(ImageGenre.Butt) = Settings.LBLButtPath
            returnValue(ImageGenre.Captions) = Settings.ICaptions
            returnValue(ImageGenre.Femdom) = Settings.IFemdom
            returnValue(ImageGenre.Gay) = Settings.IGay
            returnValue(ImageGenre.General) = Settings.IGeneral
            returnValue(ImageGenre.Hentai) = Settings.IHentai
            returnValue(ImageGenre.Hardcore) = Settings.IHardcore
            returnValue(ImageGenre.Softcore) = Settings.ISoftcore
            returnValue(ImageGenre.Lesbian) = Settings.ILesbian
            returnValue(ImageGenre.Lezdom) = Settings.ILezdom
            returnValue(ImageGenre.Maledom) = Settings.IMaledom
            Return returnValue
        End Get
    End Property

    Public Property BronzeTokens As Integer Implements ISettingsAccessor.BronzeTokens
        Get
            Return Settings.BronzeTokens
        End Get
        Set(value As Integer)
            Settings.BronzeTokens = value
        End Set
    End Property

    Public Property SilverTokens As Integer Implements ISettingsAccessor.SilverTokens
        Get
            Return Settings.SilverTokens
        End Get
        Set(value As Integer)
            Settings.SilverTokens = value
        End Set
    End Property

    Public Property GoldTokens As Integer Implements ISettingsAccessor.GoldTokens
        Get
            Return Settings.GoldTokens
        End Get
        Set(value As Integer)
            Settings.GoldTokens = value
        End Set
    End Property
End Class
