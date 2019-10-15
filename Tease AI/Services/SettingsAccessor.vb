Option Strict On
Option Infer Off

Imports Tease_AI.My
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Interfaces.Accessors

Public Class SettingsAccessor
    Implements ISettingsAccessor

    Public Property IsBallTortureEnabled As Boolean Implements ISettingsAccessor.IsBallTortureEnabled
        Get
            Return Settings.CBTCock
        End Get
        Set(value As Boolean)
            Settings.CBTCock = value
        End Set
    End Property

    Public Property IsCockTortureEnabled As Boolean Implements ISettingsAccessor.IsCockTortureEnabled
        Get
            Return Settings.CBTCock
        End Get
        Set(value As Boolean)
            Settings.CBTCock = value
        End Set
    End Property

    Public Property DoesChastityDeviceRequirePiercing As Boolean Implements ISettingsAccessor.DoesChastityDeviceRequirePiercing
        Get
            Return Settings.ChastityPA
        End Get
        Set(value As Boolean)
            Settings.ChastityPA = value
        End Set
    End Property

    Public Property DoesChastityDeviceContainSpikes As Boolean Implements ISettingsAccessor.DoesChastityDeviceContainSpikes
        Get
            Return Settings.ChastitySpikes
        End Get
        Set(value As Boolean)
            Settings.ChastitySpikes = value
        End Set
    End Property

    Public Property CanInterruptLongEdge As Boolean Implements ISettingsAccessor.CanInterruptLongEdge
        Get
            Return Settings.CBLongEdgeInterrupt
        End Get
        Set(value As Boolean)
            Settings.CBLongEdgeInterrupt = value
        End Set
    End Property

    Public Property HoldEdgeMinimum As Integer Implements ISettingsAccessor.HoldEdgeMinimum
        Get
            Return Settings.HoldTheEdgeMin
        End Get
        Set(value As Integer)
            Settings.HoldTheEdgeMin = value
        End Set
    End Property

    Public Property LongHoldEdgeMinimum As Integer Implements ISettingsAccessor.LongHoldEdgeMinimum
        Get
            Return Settings.LongHoldMax
        End Get
        Set(value As Integer)
            Settings.LongHoldMax = value
        End Set
    End Property

    Public Property HoldEdgeMaximum As Integer Implements ISettingsAccessor.HoldEdgeMaximum
        Get
            Return Settings.HoldTheEdgeMax
        End Get
        Set(value As Integer)
            Settings.HoldTheEdgeMax = value
        End Set
    End Property

    Public Property LongHoldEdgeMaximum As Integer Implements ISettingsAccessor.LongHoldEdgeMaximum
        Get
            Return Settings.LongHoldMin
        End Get
        Set(value As Integer)
            Settings.LongHoldMin = value
        End Set
    End Property

    Public Property ExtremeHoldEdgeMaximum As Integer Implements ISettingsAccessor.ExtremeHoldEdgeMaximum
        Get
            Return Settings.ExtremeHoldMax
        End Get
        Set(value As Integer)
            Settings.ExtremeHoldMax = value
        End Set
    End Property

    Public Property ExtremeHoldEdgeMinimum As Integer Implements ISettingsAccessor.ExtremeHoldEdgeMinimum
        Get
            Return Settings.ExtremeHoldMin
        End Get
        Set(value As Integer)
            Settings.ExtremeHoldMin = value
        End Set
    End Property

    Public Property CockAndBallTortureLevel As TortureLevel Implements ISettingsAccessor.CockAndBallTortureLevel
        Get
            Return TortureLevel.Create(Settings.CBTSlider).Value
        End Get
        Set(value As TortureLevel)
            Settings.CBTSlider = value
        End Set
    End Property

    Public Property IsSubCircumcised As Boolean Implements ISettingsAccessor.IsSubCircumcised
        Get
            Return Settings.SubCircumcised
        End Get
        Set(value As Boolean)
            Settings.SubCircumcised = value
        End Set
    End Property

    Public Property IsSubPierced As Boolean Implements ISettingsAccessor.IsSubPierced
        Get
            Return Settings.SubPierced
        End Get
        Set(value As Boolean)
            Settings.SubPierced = value
        End Set
    End Property

    Public Property DominationLevel As DomLevel Implements ISettingsAccessor.DominationLevel
        Get
            Return DomLevel.Create(Settings.DomLevel).Value
        End Get
        Set(value As DomLevel)
            Settings.DomLevel = value
        End Set
    End Property

    Public Property ApathyLevel As ApathyLevel Implements ISettingsAccessor.ApathyLevel
        Get
            Return ApathyLevel.Create(Settings.DomEmpathy).Value
        End Get
        Set(value As ApathyLevel)
            Settings.DomEmpathy = value
        End Set
    End Property

    Public Property DoesDommeDecideOrgasmRange As Boolean Implements ISettingsAccessor.DoesDommeDecideOrgasmRange
        Get
            Return Settings.RangeOrgasm
        End Get
        Set(value As Boolean)
            Settings.RangeOrgasm = value
        End Set
    End Property

    Public Property DoesDommeDecideRuinRange As Boolean Implements ISettingsAccessor.DoesDommeDecideRuinRange
        Get
            Return Settings.RangeRuin
        End Get
        Set(value As Boolean)
            Settings.RangeRuin = value
        End Set
    End Property

    Public Property AllowOrgasmOftenPercent As Integer Implements ISettingsAccessor.AllowOrgasmOftenPercent
        Get
            Return Settings.AllowOften
        End Get
        Set(value As Integer)
            Settings.AllowOften = value
        End Set
    End Property

    Public Property AllowOrgasmSometimesPercent As Integer Implements ISettingsAccessor.AllowOrgasmSometimesPercent
        Get
            Return Settings.AllowSometimes
        End Get
        Set(value As Integer)
            Settings.AllowSometimes = value
        End Set
    End Property

    Public Property AllowOrgasmRarelyPercent As Integer Implements ISettingsAccessor.AllowOrgasmRarelyPercent
        Get
            Return Settings.AllowRarely
        End Get
        Set(value As Integer)
            Settings.AllowRarely = value
        End Set
    End Property

    Public Property RuinOrgasmOftenPercent As Integer Implements ISettingsAccessor.RuinOrgasmOftenPercent
        Get
            Return Settings.RuinOften
        End Get
        Set(value As Integer)
            Settings.RuinOften = value
        End Set
    End Property

    Public Property RuinOrgasmSometimesPercent As Integer Implements ISettingsAccessor.RuinOrgasmSometimesPercent
        Get
            Return Settings.RuinSometimes
        End Get
        Set(value As Integer)
            Settings.RuinSometimes = value
        End Set
    End Property

    Public Property RuinOrgasmRarelyPercent As Integer Implements ISettingsAccessor.RuinOrgasmRarelyPercent
        Get
            Return Settings.RuinRarely
        End Get
        Set(value As Integer)
            Settings.RuinRarely = value
        End Set
    End Property

    Public Property SafeWord As String Implements ISettingsAccessor.SafeWord
        Get
            Return Settings.Safeword
        End Get
        Set(value As String)
            Settings.Safeword = value
        End Set
    End Property

    Public Property UseAverageEdgeTimeAsThreshold As Boolean Implements ISettingsAccessor.UseAverageEdgeTimeAsThreshold
        Get
            Return Settings.CBEdgeUseAvg
        End Get
        Set(value As Boolean)
            Settings.CBEdgeUseAvg = value
        End Set
    End Property

    Public Property AllowsLongEdgeTaunts As Boolean Implements ISettingsAccessor.AllowsLongEdgeTaunts
        Get
            Return Settings.CBLongEdgeTaunts
        End Get
        Set(value As Boolean)
            Settings.CBLongEdgeTaunts = value
        End Set
    End Property

    Public Property AllowsLongEdgeInterrupts As Boolean Implements ISettingsAccessor.AllowsLongEdgeInterrupts
        Get
            Return Settings.CBLongEdgeInterrupts
        End Get
        Set(value As Boolean)
            Settings.CBLongEdgeInterrupts = value
        End Set
    End Property

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

    Public Property HasChastityDevice As Boolean Implements ISettingsAccessor.HasChastityDevice
        Get
            Return Settings.CBOwnChastity
        End Get
        Set(value As Boolean)
            Settings.CBOwnChastity = value
        End Set
    End Property

    Public Property CallCockAClit As Boolean Implements ISettingsAccessor.CallCockAClit
        Get
            Return Settings.CockToClit
        End Get
        Set(value As Boolean)
            Settings.CockToClit = value
        End Set
    End Property

    Public Property CallBallsPussy As Boolean Implements ISettingsAccessor.CallBallsPussy
        Get
            Return Settings.BallsToPussy
        End Get
        Set(value As Boolean)
            Settings.BallsToPussy = value
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

    Public Property IsSubFemale As Boolean Implements ISettingsAccessor.IsSubFemale
        Get
            Return Settings.CBHimHer
        End Get
        Set(value As Boolean)
            Settings.CBHimHer = value
        End Set
    End Property

    Public Function GetGreetings() As List(Of String) Implements ISettingsAccessor.GetGreetings
        Return MySettings.Default.SubGreeting.Split(","(0)).Select(Function(str) str.Trim()).ToList()
    End Function

    Public Property DommePersonality As String Implements ISettingsAccessor.DommePersonality
        Get
            Return Settings.DomPersonality
        End Get
        Set(value As String)
            Settings.DomPersonality = value
        End Set
    End Property

    Public Property DommeName As String Implements ISettingsAccessor.DommeName
        Get
            Return Settings.DomName
        End Get
        Set(value As String)
            Settings.DomName = value
        End Set
    End Property

    Public Function GetSubName() As String Implements ISettingsAccessor.GetSubName
        Return MySettings.Default.SubName
    End Function

    Friend Property tDommeAvatarImageName As String Implements ISettingsAccessor.DommeAvatarImageName
        Get
            Return Settings.DomPersonality
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

    Public Property IsOnline As Boolean Implements ISettingsAccessor.IsOnline
        Get
            Return Settings.OfflineMode
        End Get
        Set(value As Boolean)
            Settings.OfflineMode = value
        End Set
    End Property

    Public Property IsTimeStampEnabled As Boolean Implements ISettingsAccessor.IsTimeStampEnabled
        Get
            Return Settings.CBTimeStamps
        End Get
        Set(value As Boolean)
            Settings.CBTimeStamps = value
        End Set
    End Property

    Public Property ShowNames As Boolean Implements ISettingsAccessor.ShowNames
        Get
            Return Settings.CBShowNames
        End Get
        Set(value As Boolean)
            Settings.CBShowNames = value
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
End Class
