﻿Option Strict On
Option Infer Off

Imports Tease_AI.My
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Interfaces.Accessors

Public Class SettingsAccessor
    Implements TeaseAI.Common.Interfaces.Accessors.ISettingsAccessor

    Public Property IsTeaseLengthDommeDetermined As Boolean Implements ISettingsAccessor.IsTeaseLengthDommeDetermined
        Get
            Return Settings.CBTeaseLengthDD
        End Get
        Set(value As Boolean)
            Settings.CBTeaseLengthDD = value
        End Set
    End Property

    Public Property IsTauntCycleDommeDetermined As Boolean Implements ISettingsAccessor.IsTauntCycleDommeDetermined
        Get
            Return Settings.CBTauntCycleDD
        End Get
        Set(value As Boolean)
            Settings.CBTauntCycleDD = value
        End Set
    End Property

    Public Property CanDommeDeleteFiles As Boolean Implements ISettingsAccessor.CanDommeDeleteFiles
        Get
            Return Settings.DomDeleteMedia
        End Get
        Set(value As Boolean)
            Settings.DomDeleteMedia = value
        End Set
    End Property

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

    Public Property DommeName As String Implements ISettingsAccessor.DommeName
        Get
            Return Settings.DomName
        End Get
        Set(value As String)
            Settings.DomName = value
        End Set
    End Property

    Public Property SubName As String Implements ISettingsAccessor.SubName
        Get
            Return Settings.SubName
        End Get
        Set(value As String)
            Settings.SubName = value
        End Set
    End Property

    Friend Property DommeAvatarImageName As String Implements ISettingsAccessor.DommeAvatarImageName
        Get
            Return Settings.DomAvatarSave
        End Get
        Set(value As String)
            Settings.DomAvatarSave = value
        End Set
    End Property

    Public Property TeaseLengthMinimum As Integer Implements ISettingsAccessor.TeaseLengthMinimum
        Get
            Return Settings.TeaseLengthMin
        End Get
        Set(value As Integer)
            Settings.TeaseLengthMin = value
        End Set
    End Property

    Public Property TeaseLengthMaximum As Integer Implements ISettingsAccessor.TeaseLengthMaximum
        Get
            Return Settings.TeaseLengthMax
        End Get
        Set(value As Integer)
            Settings.TeaseLengthMax = value
        End Set
    End Property

    Public Property TauntCycleMinimum As Integer Implements ISettingsAccessor.TauntCycleMinimum
        Get
            Return Settings.TauntCycleMin
        End Get
        Set(value As Integer)
            Settings.TauntCycleMin = value
        End Set
    End Property

    Public Property TauntCycleMaximum As Integer Implements ISettingsAccessor.TauntCycleMaximum
        Get
            Return Settings.TauntCycleMax
        End Get
        Set(value As Integer)
            Settings.TauntCycleMax = value
        End Set
    End Property

    Public ReadOnly Property IsImageGenreEnabled As Dictionary(Of ImageGenre, Boolean) Implements ISettingsAccessor.IsImageGenreEnabled
        Get
            Dim returnValue As Dictionary(Of ImageGenre, Boolean) = New Dictionary(Of ImageGenre, Boolean)()

            returnValue(ImageGenre.Blowjob) = Settings.CBIBlowjob
            returnValue(ImageGenre.Boobs) = Settings.CBIBoobs
            returnValue(ImageGenre.Butt) = Settings.CBIButts
            returnValue(ImageGenre.Captions) = Settings.CBICaptions
            returnValue(ImageGenre.Femdom) = Settings.CBIFemdom
            returnValue(ImageGenre.Gay) = Settings.CBIGay
            returnValue(ImageGenre.General) = Settings.CBIGeneral
            returnValue(ImageGenre.Hardcore) = Settings.CBIHardcore
            returnValue(ImageGenre.Hentai) = Settings.CBIHentai
            returnValue(ImageGenre.Softcore) = Settings.CBISoftcore
            returnValue(ImageGenre.Lesbian) = Settings.CBILesbian
            returnValue(ImageGenre.Lezdom) = Settings.CBILezdom
            returnValue(ImageGenre.Maledom) = Settings.CBIMaledom

            Return returnValue
        End Get
    End Property

    Public ReadOnly Property ImageGenreIncludeSubDirectory As Dictionary(Of ImageGenre, Boolean) Implements ISettingsAccessor.ImageGenreIncludeSubDirectory
        Get
            Dim returnValue As Dictionary(Of ImageGenre, Boolean) = New Dictionary(Of ImageGenre, Boolean)()

            returnValue(ImageGenre.Blowjob) = Settings.IBlowjobSD
            returnValue(ImageGenre.Boobs) = Settings.CBBoobSubDir
            returnValue(ImageGenre.Butt) = Settings.CBButtSubDir
            returnValue(ImageGenre.Captions) = Settings.ICaptionsSD
            returnValue(ImageGenre.Femdom) = Settings.IFemdomSD
            returnValue(ImageGenre.Gay) = Settings.IGaySD
            returnValue(ImageGenre.General) = Settings.IGeneralSD
            returnValue(ImageGenre.Hardcore) = Settings.IHardcoreSD
            returnValue(ImageGenre.Hentai) = Settings.IHentaiSD
            returnValue(ImageGenre.Softcore) = Settings.ISoftcoreSD
            returnValue(ImageGenre.Lesbian) = Settings.ILesbianSD
            returnValue(ImageGenre.Lezdom) = Settings.ILezdomSD
            returnValue(ImageGenre.Maledom) = Settings.IMaledomSD

            Return returnValue
        End Get
    End Property

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

    Public ReadOnly Property AreOrgasmsLocked As Boolean Implements ISettingsAccessor.AreOrgasmsLocked
        Get
            Return Settings.OrgasmLockDate <= DateTime.Now.Date
        End Get
    End Property

    Public Property OrgasmLockDate As Date Implements ISettingsAccessor.OrgasmLockDate
        Get
            Return Settings.OrgasmLockDate
        End Get
        Set(value As Date)
            Settings.OrgasmLockDate = value
        End Set
    End Property

    Public Property IsOffline As Boolean Implements ISettingsAccessor.IsOffline
        Get
            Return Settings.OfflineMode
        End Get
        Set(value As Boolean)
            Settings.OfflineMode = value
        End Set
    End Property

    Public Property DoesDommeTypeInstantly As Boolean Implements ISettingsAccessor.DoesDommeTypeInstantly
        Get
            Return Settings.CBInstantType
        End Get
        Set(value As Boolean)
            Settings.CBInstantType = value
        End Set
    End Property

    Public Property WebTeaseModeEnabled As Boolean Implements ISettingsAccessor.WebTeaseModeEnabled
        Get
            Return Settings.CBWebtease
        End Get
        Set(value As Boolean)
            Settings.CBWebtease = value
        End Set
    End Property

    Public Property InChastity As Boolean Implements ISettingsAccessor.InChastity
        Get
            Return Settings.Chastity
        End Get
        Set(value As Boolean)
            Settings.Chastity = value
        End Set
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
