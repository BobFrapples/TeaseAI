﻿Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Speech.Synthesis
Imports System.Speech.AudioFormat
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions
Imports TeaseAI.Common
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Constants
Imports TeaseAI.Services.TagData
Imports TeaseAI.Services
Imports TeaseAI.Services.CommandDetection
Imports System.Threading.Tasks
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Services.Processor
Imports TeaseAI.Services.Keywords
Imports TeaseAI.Common.Events
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Common.Data.RiskyPick

Public Class MainWindow
#Region "-------------------------------------- File Constants ------------------------------------------"
    ''' <summary>
    ''' The default directory URL-Files are located.
    ''' </summary>
    Friend Shared ReadOnly pathUrlFileDir As String = Application.StartupPath & "\Images\System\URL Files\"

#End Region ' File Constants.

    Friend FormLoading As Boolean = True
    Dim FormFinishedLoading As Boolean = False
    Dim myChatLog As List(Of ChatMessage) = New List(Of ChatMessage)()
    Dim myDommeMessages As Queue(Of ChatMessage) = New Queue(Of ChatMessage)()

    Dim parseTagDataService As ParseOldTagDataService = New ParseOldTagDataService()
    Dim myLineService As LineService = New LineService()
    Dim loadFileData As ILoadFileData = ServiceFactory.CreateLoadFileData()
    Dim myChatLogToHtmlService As IChatLogToHtmlService = New ChatLogToHtmlService()
    Dim myStringService As StringService = New StringService()
    Dim myGetScripts As IScriptAccessor = New ScriptAccessor(New CldAccessor())
    Dim myImageTagReplaceHash As ImageTagReplaceHash = New ImageTagReplaceHash()
    Dim myFlagService As FlagService = New FlagService(New FlagAccessor())
    Dim myFlagAccessor As FlagAccessor = New FlagAccessor()
    Dim myGotoProcessor As GotoProcessor = New GotoProcessor(New ScriptAccessor(New CldAccessor()))
    Dim mySettingsAccessor As ISettingsAccessor = ServiceFactory.CreateSettingsAccessor()
    Dim myRandomNumberService As IRandomNumberService = New RandomNumberService()
    Dim mySlideShowNavigationService As ISlideShowNavigationService = New SlideShowNavigationService()
    Dim myPathsAccessor As PathsAccessor = ServiceFactory.CreatePathsAccessor()
    Dim WithEvents mySession As SessionEngine

    'TODO: Use a custom class to pass data between ScriptParsing methods.
    <Obsolete("QND-Implementation of ContactData.GetTaggedImage. ")>
    Dim ContactToUse As ContactData

    Dim sshSyncLock As New Object
    ''' <summary>
    ''' Shorthand Property to access My.Application.Session
    ''' </summary>
    ''' <returns></returns>
    Public Property ssh As SessionState
        Get
            SyncLock sshSyncLock
                Return My.Application.Session
            End SyncLock
        End Get
        Set(value As SessionState)
            SyncLock sshSyncLock
                My.Application.Session = value
            End SyncLock
        End Set
    End Property

    Public MetroThread As Thread

#Region "-------------------------------------- StrokePace ----------------------------------------------"
    ''' <summary>
    ''' Synclock Object to prevent datacorruption of <see cref="ssh.StrokePace"/>.
    ''' </summary>
    Public _StrokePaceSyncLock As New Object
    ''' <summary>
    ''' Gets or sets the strokepace.
    ''' Changing this value will  delay the MetronomeThread, until all 
    ''' registers are written to RAM.
    ''' </summary>
    ''' <returns>The current StrokePace.</returns>
    Public Property StrokePace As Integer
        Get
            Return ssh.StrokePace
        End Get
        Set(value As Integer)
            If value <> ssh.StrokePace Then
                SyncLock _StrokePaceSyncLock
                    ssh.StrokePace = value
                End SyncLock
            End If
            If value <> _StrokePaceMetronomeUnsynced Then
                SyncLock _StrokePaceMetronomeSyncLock
                    _StrokePaceMetronomeUnsynced = value
                End SyncLock
            End If
        End Set
    End Property
    ''' <summary>
    ''' Synclock Object to prevent datacorruption of <see cref="_StrokePaceMetronomeUnsynced"/>.
    ''' <para>As long as this lock is hold, the metronome thread is blocked!</para>
    ''' </summary>
    Private _StrokePaceMetronomeSyncLock As New Object
    ''' <summary>
    '''	Stores the value of the current strokePace. 
    ''' <para>Synchronized MultiThread-Object!</para>
    ''' <para> Use <see cref="StrokePace"/> instead.</para>
    ''' </summary>
    Private _StrokePaceMetronomeUnsynced As Integer
    ''' <summary>
    ''' Gets the strokepace ThreadSafe. 
    ''' This property is restricted to Metronome-Thread.
    ''' </summary>
    ''' <returns>The current StrokePace.</returns>
    Friend ReadOnly Property StrokePaceMetronome As Integer
        Get
            If Thread.CurrentThread IsNot MetroThread Then _
                Throw New AccessViolationException("Reading StrokePaceMetronome is restricted to MetronomeThread.")

            SyncLock _StrokePaceMetronomeSyncLock
                Dim tmpInt As Integer = _StrokePaceMetronomeUnsynced
                Return tmpInt
            End SyncLock
        End Get
    End Property
#End Region ' StrokePace

    Public synth As New SpeechSynthesizer
    Public synth2 As New SpeechSynthesizer


    Public LazyEdit1 As Boolean
    Public LazyEdit2 As Boolean
    Public LazyEdit3 As Boolean
    Public LazyEdit4 As Boolean
    Public LazyEdit5 As Boolean
    Public ApplyingTheme As Boolean

    Private Const DISABLE_SOUNDS As Integer = 21
    Private Const SET_FEATURE_ON_PROCESS As Integer = 2

    Private Declare Function GetKeyState _
         Lib "user32" _
         (ByVal nVirtKey As Long) As Integer
    Private Const VK_LBUTTON = &H1

    <DllImport("urlmon.dll")>
    Public Shared Function CoInternetSetFeatureEnabled(
    ByVal FeatureEntry As Integer, <MarshalAs(UnmanagedType.U4)> ByVal dwFlags As Integer, ByVal fEnable As Boolean) As Integer

    End Function

    Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String,
ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

            mainPictureBox.Image = Nothing
            WatchDogImageAnimator.Dispose()

            TeaseTimer.Stop()
            TeaseAIClock.Stop()
            Timer1.Stop()
            UpdateStageTimer.Stop()
            UpdatesTimer.Stop()
            StrokeTimeTotalTimer.Stop()
            StopEverything()

            'If BeforeTease = False And My.Settings.Sys_SubLeftEarly <> 0 Then My.Settings.Sys_SubLeftEarlyTotal += 1

            If ssh.BeforeTease = False And Val(GetVariable("SYS_SubLeftEarly")) <> 0 Then SetVariable("SYS_SubLeftEarlyTotal", Val(GetVariable("SYS_SubLeftEarlyTotal")) + 1)



            'TempGif.Dispose()
            'original.Dispose()
            'resized.Dispose()

            Try
                GC.Collect()
            Catch
            End Try



            If File.Exists(Application.StartupPath & "\System\Metronome") Then
                File.SetAttributes(Application.StartupPath & "\System\Metronome", FileAttributes.Normal)
                My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\System\Metronome")
            End If

            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Temp\Temp.gif") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Temp\Temp.gif")
            End If

            'TODO-Next: Remove Legacy-Code.
            Try
                For Each prog As Process In Process.GetProcesses
                    If prog.ProcessName = "Tease AI Metronome" Then
                        prog.Kill()
                    End If
                Next
            Catch ex As Exception

            End Try


            Dim TempDate As String
            Dim TempDateNow As DateTime = DateTime.Now

            TempDate = TempDateNow.ToString("MM.dd.yyyy hhmm")

            'Github Patch Begin

            ' If FrmSettings.CBSaveChatlogExit.Checked = True Then

            'If (Not System.IO.Directory.Exists(Application.StartupPath & "\Chatlogs\")) Then
            'System.IO.Directory.CreateDirectory(Application.StartupPath & "\Chatlogs\")
            'End If

            'My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\" & TempDate & " chatlog.html", ChatText.DocumentText, False)

            'End If

            ' Github Patch End

            SaveChatLog(TempDate)

            Try
                FrmSettings.Close()
                FrmSettings.Dispose()
            Catch ex As Exception
            End Try

            Try
                GamesWindow.Close()
                GamesWindow.Dispose()
            Catch ex As Exception
            End Try



            TeaseAINotify.Visible = False
            TeaseAINotify.Icon = Nothing
            TeaseAINotify.Dispose()
            TeaseAINotify = Nothing


            System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception

        Finally
            My.Settings.Save()
        End Try
    End Sub

    Private Sub SaveChatLog(LogDate As String)
        If FrmSettings.CBSaveChatlogExit.Checked = True And ChatText.DocumentText.Length > 36 Then

            If (Not System.IO.Directory.Exists(Application.StartupPath & "\Chatlogs\")) Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Chatlogs\")
            End If

            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\" & LogDate & " chatlog.html", ChatText.DocumentText, False)

        End If
    End Sub

    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)

        TeaseAINotify.Visible = False

        TeaseAINotify.Dispose()

        MyBase.OnClosing(e)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If mySession Is Nothing Then
            mySession = New SessionEngine(ServiceFactory.CreateSettingsAccessor(), New StringService(), New ScriptAccessor(New CldAccessor()), New TimerFactory(), New FlagAccessor(), New ImageAccessor(), New VideoAccessor(), New VariableAccessor(), New TauntAccessor(), New SystemVocabularyAccessor(), New VocabularyAccessor(), New LineCollectionFilter(), New RandomNumberService(), ServiceFactory.CreateConfigurationAccessor(), New NotifyUser(), CType(ServiceFactory.CreatePathsAccessor(), IPathsAccessor))
            AddHandler mySession.DommeSaid, AddressOf mySession_DommeSaid
            AddHandler mySession.ShowImage, AddressOf mySession_ShowImage
            AddHandler mySession.PlayVideo, AddressOf mySession_PlayVideo
            AddHandler mySession.MessageProcessors(MessageProcessor.RequestTask).MessageProcessed, AddressOf Task_Requested
            AddHandler mySession.MessageProcessors(MessageProcessor.Greeting).MessageProcessed, AddressOf Greeting_Spoken
            AddHandler mySession.MessageProcessors(MessageProcessor.Safeword).MessageProcessed, AddressOf Safeword_Spoken
        End If
        Try
retryStart:

            Dim tv As Version = My.Application.Info.Version
            Me.Text = String.Format("Tease A.I. - PATCH {0}.{1}{2}",
                                tv.Minor,
                                tv.Build,
                                If(tv.MinorRevision > 0, "." & tv.MinorRevision, ""))
            FormLoading = True
            Dim splashScreen As FrmSplash = New FrmSplash()
            splashScreen.PBSplash.Value = 0
            splashScreen.Show()

            splashScreen.UpdateText("Clearing Metronome settings...")
            StrokePace = 0

            splashScreen.UpdateText("Checking terms and conditions...")
            If Not My.Settings.TC2Agreed Then
                Form7.Show()
                Do
                    Application.DoEvents()
                Loop Until My.Settings.TC2Agreed = True
            End If

            splashScreen.UpdateText("Checking installed personalities...")

            Dim personalities As List(Of String) = GetDommePersonalities(Application.StartupPath & "\Scripts\")
            DommePersonalityComboBox.Items.AddRange(personalities.ToArray())
            If Not personalities.Any() Then
                MessageBox.Show(Me, "No domme Personalities were found! Many aspects of this program will not work correctly until at least one Personality is installed.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim domme As String = mySettingsAccessor.DommePersonality()
                DommePersonalityComboBox.Text = If(personalities.Contains(domme), domme, personalities.First())
            End If

            FrmSettings.FrmSettingsLoading = True
            FrmSettings.FrmSettingStartUp()

            Do
                Application.DoEvents()
            Loop Until FrmSettings.FrmSettingsLoading = False

            ssh.StrokeTimeTotal = My.Settings.StrokeTimeTotal
            StrokeTimeTotalTimer.Start()

            splashScreen.UpdateText("Calculating total stroke time...")
            Dim STT As TimeSpan = TimeSpan.FromSeconds(ssh.StrokeTimeTotal)
            FrmSettings.LBLStrokeTimeTotal.Text = String.Format("{0:0000}:{1:00}:{2:00}:{3:00}", STT.Days, STT.Hours, STT.Minutes, STT.Seconds)


            ssh.DomTask = "Null"
            ssh.DomChat = "Null"

            ssh.CBTBallsFirst = True
            ssh.CBTCockFirst = True
            ssh.CBTBothFirst = True
            ssh.CustomTaskFirst = True

            CoInternetSetFeatureEnabled(DISABLE_SOUNDS, SET_FEATURE_ON_PROCESS, True)

            IsTypingTimer.Start()

            splashScreen.UpdateText("Loading Domme and Sub avatar images...")
            Dim avatar As String = mySettingsAccessor.DommeAvatarImageName
            If File.Exists(avatar) Then
                domAvatar.Image = Image.FromFile(avatar)
            End If
            'If File.Exists(My.Settings.SubAvatarSave) Then subAvatar.Image = Image.FromFile(My.Settings.SubAvatarSave)

            splashScreen.UpdateText("Checking recent slideshows...")
            For Each path As String In My.Settings.RecentSlideshows
                If Directory.Exists(path) Then ImageFolderComboBox.Items.Add(path)
            Next
            My.Settings.RecentSlideshows.Clear()

            For Each comboitem As String In ImageFolderComboBox.Items
                My.Settings.RecentSlideshows.Add(comboitem)
            Next

            splashScreen.UpdateText("Checking local videos...")
            ' Checks all folders and Sets the VideoCount as LabelText
            FrmSettings.Video_CheckAllFolders()

            ssh.VideoType = "General"

            splashScreen.UpdateText("Loading Glitter avatar images...")
            If File.Exists(My.Settings.GlitterAV) Then FrmSettings.GlitterAV.Image = Image.FromFile(My.Settings.GlitterAV)
            If File.Exists(My.Settings.GlitterAV1) Then FrmSettings.GlitterAV1.Image = Image.FromFile(My.Settings.GlitterAV1)
            If File.Exists(My.Settings.GlitterAV2) Then FrmSettings.GlitterAV2.Image = Image.FromFile(My.Settings.GlitterAV2)
            If File.Exists(My.Settings.GlitterAV3) Then FrmSettings.GlitterAV3.Image = Image.FromFile(My.Settings.GlitterAV3)

            splashScreen.UpdateText("Loading Glitter settings...")
            ssh.UpdatesTick = 120
            UpdatesTimer.Start()

            Me.ActiveControl = Me.chatBox

            '################## Validate RadioButtons #################
            If My.Settings.CBGlitterFeedOff Then
                My.Settings.CBGlitterFeed = False
                My.Settings.CBGlitterFeedScripts = False
            ElseIf My.Settings.CBGlitterFeed Then
                ' No need to unset My.Settings.CBGlitterFeedOff. 
                ' If it would be true, this branch Is unreachable
                My.Settings.CBGlitterFeedScripts = False
            ElseIf My.Settings.CBGlitterFeed = False _
        AndAlso My.Settings.CBGlitterFeedOff = False _
        AndAlso My.Settings.CBGlitterFeedScripts = False Then
                My.Settings.CBGlitterFeedOff = True
            End If

            splashScreen.UpdateText("Loading names...")
            domName.Text = mySettingsAccessor.DommeName.Trim()
            SubName.Text = mySettingsAccessor.SubName.Trim()

            FrmSettings.PetNameBox1.Text = My.Settings.pnSetting1
            FrmSettings.petnameBox2.Text = My.Settings.pnSetting2
            FrmSettings.petnameBox3.Text = My.Settings.pnSetting3
            FrmSettings.petnameBox4.Text = My.Settings.pnSetting4
            FrmSettings.petnameBox5.Text = My.Settings.pnSetting5
            FrmSettings.petnameBox6.Text = My.Settings.pnSetting6
            FrmSettings.petnameBox7.Text = My.Settings.pnSetting7
            FrmSettings.petnameBox8.Text = My.Settings.pnSetting8

            splashScreen.UpdateText("Loading General Settings...")
            FrmSettings.TimeStampCheckBox.Checked = mySettingsAccessor.IsTimeStampEnabled
            FrmSettings.ShowNamesCheckBox.Checked = mySettingsAccessor.ShowNames
            FrmSettings.TypeInstantlyCheckBox.Checked = mySettingsAccessor.DoesDommeTypeInstantly
            FrmSettings.WebTeaseMode.Checked = mySettingsAccessor.WebTeaseModeEnabled
            If FrmSettings.WebTeaseMode.Checked = True _
                Then WebteaseModeToolStripMenuItem.Checked = True


            FrmSettings.CBInputIcon.Checked = My.Settings.CBInputIcon
            FrmSettings.CBBlogImageWindow.Checked = My.Settings.CBBlogImageMain
            FrmSettings.CBSlideshowSubDir.Checked = My.Settings.CBSlideshowSubDir
            FrmSettings.CBSlideshowRandom.Checked = My.Settings.CBSlideshowRandom
            FrmSettings.LandscapeCheckBox.Checked = My.Settings.CBStretchLandscape
            FrmSettings.CBSettingsPause.Checked = My.Settings.CBSettingsPause
            FrmSettings.CBAutosaveChatlog.Checked = My.Settings.CBAutosaveChatlog
            FrmSettings.CBSaveChatlogExit.Checked = My.Settings.CBExitSaveChatlog

            FrmSettings.CBImageInfo.Checked = My.Settings.CBImageInfo

            splashScreen.PBSplash.Value += 1
            splashScreen.UpdateText("Loading Domme Settings...")
            FrmSettings.domageNumBox.Value = My.Settings.DomAge
            FrmSettings.DomLevelDescLabel.Text = mySettingsAccessor.DominationLevel.ToString()

            FrmSettings.NBDomBirthdayMonth.Value = My.Settings.DomBirthMonth
            FrmSettings.NBDomBirthdayDay.Value = My.Settings.DomBirthDay
            FrmSettings.TBDomHairColor.Text = My.Settings.DomHair
            FrmSettings.domhairlengthComboBox.Text = My.Settings.DomHairLength
            FrmSettings.TBDomEyeColor.Text = My.Settings.DomEyes
            FrmSettings.boobComboBox.Text = My.Settings.DomCup
            FrmSettings.dompubichairComboBox.Text = My.Settings.DomPubicHair
            FrmSettings.CBDomTattoos.Checked = My.Settings.DomTattoos
            FrmSettings.CBDomFreckles.Checked = My.Settings.DomFreckles
            FrmSettings.crazyCheckBox.Checked = My.Settings.DomCrazy
            FrmSettings.vulgarCheckBox.Checked = My.Settings.DomVulgar
            FrmSettings.supremacistCheckBox.Checked = My.Settings.DomSupremacist
            FrmSettings.LCaseCheckBox.Checked = My.Settings.DomLowercase
            FrmSettings.apostropheCheckBox.Checked = My.Settings.DomNoApostrophes
            FrmSettings.commaCheckBox.Checked = My.Settings.DomNoCommas
            FrmSettings.periodCheckBox.Checked = My.Settings.DomNoPeriods
            FrmSettings.CBMeMyMine.Checked = My.Settings.DomMeMyMine
            FrmSettings.TBEmote.Text = My.Settings.TBEmote
            FrmSettings.TBEmoteEnd.Text = My.Settings.TBEmoteEnd

            If FrmSettings.TBEmote.Text = "" Then FrmSettings.TBEmote.Text = "*"
            If FrmSettings.TBEmoteEnd.Text = "" Then FrmSettings.TBEmoteEnd.Text = "*"
            FrmSettings.alloworgasmComboBox.Text = My.Settings.OrgasmAllow
            FrmSettings.ruinorgasmComboBox.Text = My.Settings.OrgasmRuin
            FrmSettings.CBDomDenialEnds.Checked = My.Settings.DomDenialEnd
            FrmSettings.CBDomOrgasmEnds.Checked = My.Settings.DomOrgasmEnd
            FrmSettings.orgasmsPerNumBox.Value = My.Settings.DomOrgasmPer
            FrmSettings.orgasmsperComboBox.Text = My.Settings.DomPerMonth

            If My.Settings.DomLock Then
                FrmSettings.orgasmsperlockButton.Enabled = False
                FrmSettings.orgasmlockrandombutton.Enabled = False
                FrmSettings.limitcheckbox.Checked = True
                FrmSettings.limitcheckbox.Enabled = False
                FrmSettings.orgasmsPerNumBox.Enabled = False
                FrmSettings.orgasmsperComboBox.Enabled = False
            End If

            FrmSettings.NBDomMoodMin.Value = My.Settings.DomMoodMin
            FrmSettings.NBDomMoodMax.Value = My.Settings.DomMoodMax
            FrmSettings.NBAvgCockMin.Value = My.Settings.AvgCockMin
            FrmSettings.NBAvgCockMax.Value = My.Settings.AvgCockMax
            FrmSettings.NBSelfAgeMin.Value = My.Settings.SelfAgeMin
            FrmSettings.NBSelfAgeMax.Value = My.Settings.SelfAgeMax
            FrmSettings.NBSubAgeMin.Value = My.Settings.SubAgeMin
            FrmSettings.NBSubAgeMax.Value = My.Settings.SubAgeMax

            splashScreen.UpdateText("Checking Glitter scripts...")
            Try
                FrmSettings.LBLGlitModDomType.Text = DommePersonalityComboBox.Text
            Catch
                FrmSettings.LBLGlitModDomType.Text = "Error!"
            End Try

            Dim files As List(Of String) = myDirectory.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\" & FrmSettings.CBGlitModType.Text & "\").ToList()
            FrmSettings.LBGlitModScripts.Items.Clear()
            For Each file As String In files.Select(Function(f) Path.GetFileNameWithoutExtension(f))
                FrmSettings.LBGlitModScripts.Items.Add(file)
            Next
            FrmSettings.LBLGlitModScriptCount.Text = FrmSettings.CBGlitModType.Text & " Scripts Found (" & files.Count() & ")"

            FrmSettings.NBWritingTaskMin.Value = My.Settings.NBWritingTaskMin
            FrmSettings.NBWritingTaskMax.Value = My.Settings.NBWritingTaskMax

            splashScreen.UpdateText("Loading Sub settings...")
            FrmSettings.CockSizeNumBox.Value = My.Settings.SubCockSize
            FrmSettings.subAgeNumBox.Value = My.Settings.SubAge

            FrmSettings.TBGreeting.Text = My.Settings.SubGreeting
            FrmSettings.TBYes.Text = My.Settings.SubYes
            FrmSettings.TBNo.Text = My.Settings.SubNo

            FrmSettings.TBHonorific.Text = My.Settings.SubHonorific
            If String.IsNullOrWhiteSpace(FrmSettings.TBHonorific.Text) Then FrmSettings.TBHonorific.Text = "Mistress"

            FrmSettings.CBHonorificInclude.Checked = My.Settings.CBUseHonor
            FrmSettings.CBHonorificCapitalized.Checked = My.Settings.CBCapHonor

            FrmSettings.NBBirthdayMonth.Value = My.Settings.SubBirthMonth
            FrmSettings.NBBirthdayDay.Value = My.Settings.SubBirthDay
            FrmSettings.TBSubHairColor.Text = My.Settings.SubHair
            FrmSettings.TBSubEyeColor.Text = My.Settings.SubEyes

            splashScreen.UpdateText("Loading Range settings...")
            FrmSettings.SliderSTF.Value = My.Settings.TimerSTF
            If FrmSettings.SliderSTF.Value = 1 Then FrmSettings.LBLStf.Text = "Preoccupied"
            If FrmSettings.SliderSTF.Value = 2 Then FrmSettings.LBLStf.Text = "Distracted"
            If FrmSettings.SliderSTF.Value = 3 Then FrmSettings.LBLStf.Text = "Normal"
            If FrmSettings.SliderSTF.Value = 4 Then FrmSettings.LBLStf.Text = "Talkative"
            If FrmSettings.SliderSTF.Value = 5 Then FrmSettings.LBLStf.Text = "Verbose"

            FrmSettings.TauntSlider.Value = My.Settings.TimerVTF
            If FrmSettings.TauntSlider.Value = 1 Then FrmSettings.LBLVtf.Text = "Preoccupied"
            If FrmSettings.TauntSlider.Value = 2 Or FrmSettings.TauntSlider.Value = 3 Then FrmSettings.LBLVtf.Text = "Distracted"
            If FrmSettings.TauntSlider.Value = 4 Or FrmSettings.TauntSlider.Value = 5 Then FrmSettings.LBLVtf.Text = "Normal"
            If FrmSettings.TauntSlider.Value = 6 Or FrmSettings.TauntSlider.Value = 7 Or FrmSettings.TauntSlider.Value = 8 Then FrmSettings.LBLVtf.Text = "Talkative"
            If FrmSettings.TauntSlider.Value = 9 Or FrmSettings.TauntSlider.Value = 10 Then FrmSettings.LBLVtf.Text = "Verbose"

            FrmSettings.DommeMessageFontCB.Text = My.Settings.DomFont
            FrmSettings.NBFontSizeD.Text = My.Settings.DomFontSize
            FrmSettings.SubMessageFontCB.Text = My.Settings.SubFont
            FrmSettings.NBFontSize.Text = My.Settings.SubFontSize

            ssh.HoldEdgeTimeTotal = My.Settings.HoldEdgeTimeTotal

            splashScreen.UpdateText("Configuring media player...")
            DomWMP.Height = SplitContainer1.Panel1.Height + 60
            If Not My.Settings.DomAVStretch Then domAvatar.SizeMode = PictureBoxSizeMode.Zoom

            BTNFileTransferOpen.Visible = False
            BTNFIleTransferDismiss.Visible = False

            splashScreen.UpdateText("Initializing Games window...")
            RefreshCards()
            ssh.GoldTokens = My.Settings.GoldTokens
            ssh.SilverTokens = My.Settings.SilverTokens
            ssh.BronzeTokens = My.Settings.BronzeTokens

            If Not My.Settings.Patch45Tokens Then
                ssh.BronzeTokens += 100
                My.Settings.Patch45Tokens = True
                My.Settings.BronzeTokens = ssh.BronzeTokens
            End If

            ssh.BronzeTokens = Math.Max(ssh.BronzeTokens, 0)
            ssh.SilverTokens = Math.Max(ssh.SilverTokens, 0)
            ssh.GoldTokens = Math.Max(ssh.GoldTokens, 0)

            splashScreen.UpdateText("Checking previous orgasms...")
            If My.Settings.LastOrgasm = Nothing Then
                My.Settings.LastOrgasm = FormatDateTime(Now, DateFormat.ShortDate)
            End If
            FrmSettings.LBLLastOrgasm.Text = My.Settings.LastOrgasm.ToString()

            If My.Settings.LastRuined = Nothing Then
                My.Settings.LastRuined = FormatDateTime(Now, DateFormat.ShortDate)
            End If
            FrmSettings.LBLLastRuined.Text = My.Settings.LastRuined.ToString()

            splashScreen.UpdateText("Checking current date...")
            If CompareDates(My.Settings.DateStamp) <> 0 Then
                Dim loginChance As Integer = ssh.randomizer.Next(1, 101)
                Dim loginAmount As Integer

                If loginChance = 100 Then loginAmount = 100
                If loginChance < 100 Then loginAmount = 50
                If loginChance < 91 Then loginAmount = 25
                If loginChance < 76 Then loginAmount = 10
                If loginChance < 51 Then loginAmount = 5

                TeaseAINotify.BalloonTipText = "Daily login bonus: You've received " & loginAmount & " tokens!"
                TeaseAINotify.Text = "Tease AI"
                TeaseAINotify.ShowBalloonTip(5000)

                My.Settings.DateStamp = FormatDateTime(Now, DateFormat.ShortDate)
                ssh.BronzeTokens += loginAmount
                My.Settings.BronzeTokens = ssh.BronzeTokens
                My.Settings.SilverTokens = ssh.SilverTokens
                My.Settings.GoldTokens = ssh.GoldTokens
            End If

            If CompareDates(My.Settings.WishlistDate) <> 0 Then
                My.Settings.ClearWishlist = False
            End If

            splashScreen.UpdateText("Calculating average edge information...")
            ssh.AvgEdgeStroking = My.Settings.AvgEdgeStroking
            ssh.AvgEdgeNoTouch = My.Settings.AvgEdgeNoTouch
            ssh.AvgEdgeCount = My.Settings.AvgEdgeCount
            ssh.AvgEdgeCountRest = My.Settings.AvgEdgeCountRest

            If My.Settings.AvgEdgeCount > 4 Then
                Dim TS1 As TimeSpan = TimeSpan.FromSeconds(ssh.AvgEdgeStroking)
                FrmSettings.LBLAvgEdgeStroking.Text = String.Format("{0:00}:{1:00}", TS1.Minutes, TS1.Seconds)
            Else
                FrmSettings.LBLAvgEdgeStroking.Text = "WAIT"
            End If


            If My.Settings.AvgEdgeCountRest > 4 Then
                Dim TS2 As TimeSpan = TimeSpan.FromSeconds(ssh.AvgEdgeNoTouch)
                FrmSettings.LBLAvgEdgeNoTouch.Text = String.Format("{0:00}:{1:00}", TS2.Minutes, TS2.Seconds)
            Else
                FrmSettings.LBLAvgEdgeNoTouch.Text = "WAIT"
            End If

            splashScreen.UpdateText("Loading Domme Personality...")
            ssh.DomPersonality = DommePersonalityComboBox.Text
            mySettingsAccessor.DommePersonality = DommePersonalityComboBox.Text

            Dim CheckSpace As String = chatBox.Text
            If CheckSpace = "" Then
                CheckSpace = ChatBox2.Text
            End If

            Directory.CreateDirectory(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Flags\Temp\")

            splashScreen.UpdateText("Loading Glitter Contact image slideshows...")
            ssh.SlideshowMain = New ContactData(ContactType.Domme)
            ssh.SlideshowContact1 = New ContactData(ContactType.Contact1)
            ssh.SlideshowContact2 = New ContactData(ContactType.Contact2)
            ssh.SlideshowContact3 = New ContactData(ContactType.Contact3)

            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Contact_Descriptions.txt") Then
                Dim ContactList As New List(Of String)
                ContactList = Txt2List(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Contact_Descriptions.txt")
                FrmSettings.GBGlitter1.Text = PoundClean(ContactList(0))
                FrmSettings.GBGlitter2.Text = PoundClean(ContactList(1))
                FrmSettings.GBGlitter3.Text = PoundClean(ContactList(2))
            Else
                FrmSettings.GBGlitter1.Text = "Contact 1"
                FrmSettings.GBGlitter2.Text = "Contact 2"
                FrmSettings.GBGlitter3.Text = "Contact 3"
            End If

            WMPTimer.Start()

            splashScreen.UpdateText("Loading Shorthands...")
            CBShortcuts.Checked = My.Settings.Shortcuts
            CBHideShortcuts.Checked = My.Settings.ShowShortcuts
            SetShortcutsVisible()

            TBShortYes.Text = My.Settings.ShortYes
            TBShortNo.Text = My.Settings.ShortNo
            TBShortEdge.Text = My.Settings.ShortEdge
            TBShortSpeedUp.Text = My.Settings.ShortSpeedUp
            TBShortSlowDown.Text = My.Settings.ShortSlowDown
            TBShortStop.Text = My.Settings.ShortStop
            TBShortStroke.Text = My.Settings.ShortStroke
            TBShortCum.Text = My.Settings.ShortCum
            TBShortGreet.Text = My.Settings.ShortGreet
            TBShortSafeword.Text = My.Settings.ShortSafeword

            splashScreen.UpdateText("Checking saved dimensions...")
            ToggleAppVisibility(Nothing)

            RestoreFormPosition()

            Me.PnlLayoutForm.BackgroundImage = Nothing
            If File.Exists(My.Settings.BackgroundImage) Then
                Me.PnlLayoutForm.BackgroundImage = Image.FromFile(My.Settings.BackgroundImage)
            End If

            If My.Settings.BackgroundStretch Then PnlLayoutForm.BackgroundImageLayout = ImageLayout.Stretch
            If My.Settings.CBDateTransparent Then PNLDate.BackColor = Color.Transparent

            SwitchSidesToolStripMenuItem.Checked = My.Settings.MirrorWindows
            SidepanelToolStripMenuItem.Checked = My.Settings.DisplaySidePanel
            LazySubAVToolStripMenuItem.Checked = My.Settings.LazySubAV
            MaximizeImageToolStripMenuItem.Checked = My.Settings.MaximizeMediaWindow
            SideChatToolStripMenuItem1.Checked = My.Settings.SideChat

            TeaseAIClock.Start()

            splashScreen.UpdateText("Loading VitalSub...")
            LBLCalorie.Text = My.Settings.CaloriesConsumed

            Dim vsubDir As String = Application.StartupPath & "\System\VitalSub"

            If Not Directory.Exists(vsubDir) Then Directory.CreateDirectory(vsubDir)

            If File.Exists(Application.StartupPath & "\System\VitalSub\ExerciseList.cld") Then
                LoadExercise()
            End If

            ssh.CaloriesConsumed = My.Settings.CaloriesConsumed

            If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieList.txt") Then
                For Each str As String In Txt2List(Application.StartupPath & "\System\VitalSub\CalorieList.txt")
                    ComboBoxCalorie.Items.Add(str)
                Next
            End If

            If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then
                LBCalorie.Items.AddRange(Txt2List(Application.StartupPath & "\System\VitalSub\CalorieItems.txt").ToArray)
                LBLCalorie.Text = ssh.CaloriesConsumed
            Else
                ssh.CaloriesConsumed = 0
                My.Settings.CaloriesConsumed = 0
                LBLCalorie.Text = ssh.CaloriesConsumed
            End If

            CBVitalSub.Checked = My.Settings.VitalSub
            If CBVitalSub.Checked Then
                CBVitalSub.ForeColor = Color.LightGreen
                CBVitalSub.Text = "VitalSub Active"
            Else
                CBVitalSub.ForeColor = Color.Red
                CBVitalSub.Text = "VitalSub Inactive"
            End If

            CBVitalSubDomTask.Checked = My.Settings.VitalSubAssignments
            NBMinPace.Value = My.Settings.MinPace
            NBMaxPace.Value = My.Settings.MaxPace
            CBMetronome.Checked = My.Settings.MetroOn
            FrmSettings.CBMuteMedia.Checked = My.Settings.MuteMedia

            FormLoading = False

            Control.CheckForIllegalCrossThreadCalls = False

            MetroThread = New Thread(AddressOf MetronomeTick) With {.Name = "Metronome-Thread"}
            MetroThread.IsBackground = True
            MetroThread.Start()

            BTNLS1.Text = My.Settings.LS1
            BTNLS2.Text = My.Settings.LS2
            BTNLS3.Text = My.Settings.LS3
            BTNLS4.Text = My.Settings.LS4
            BTNLS5.Text = My.Settings.LS5

            splashScreen.Close()

            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_OrgasmRestricted") Then
                If CompareDatesWithTime(GetDate("SYS_OrgasmRestricted")) <> 1 Then
                    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_OrgasmRestricted")
                    ssh.OrgasmRestricted = False
                Else
                    ssh.OrgasmRestricted = True
                End If
            End If
            ssh.Activate(Me)

            FormFinishedLoading = True

            My.Settings.Save()
            Trace.WriteLine("Startup has been completed")
            mySession.Session = CreateSession()
        Catch ex As Exception
            Log.WriteError(ex.Message, ex, "Exception occurred on startup")

            Dim btn As MessageBoxButtons = If(Debugger.IsAttached, MessageBoxButtons.AbortRetryIgnore, MessageBoxButtons.RetryCancel)

            Dim b As MsgBoxResult =
                    MessageBox.Show("An exception occurred on startup. Tease-AI is unable to work correctly until this error is fixed." &
                                    vbCrLf & vbCrLf &
                                    ex.Message &
                                    vbCrLf & vbCrLf &
                                    "Further details were written to the error log.", "Startup failed",
                                    btn, MessageBoxIcon.Hand)

            If b = MsgBoxResult.Abort Or b = MsgBoxResult.Cancel Then
                Process.GetCurrentProcess().Kill()
            ElseIf b = MsgBoxResult.Retry Then
                GoTo retryStart
            End If

        End Try
    End Sub

    Private Sub SendButton_Click(sender As Object, e As EventArgs) Handles SendButton.Click
        Dim chatMessage As ChatMessage = New ChatMessage With {
            .TimeStamp = DateTime.Now,
            .Sender = SubName.Text.Trim(),
            .Message = IIf(Not String.IsNullOrWhiteSpace(chatBox.Text), chatBox.Text, ChatBox2.Text)
        }
        If String.IsNullOrWhiteSpace(chatMessage.Message) Then Return

        chatBox.Text = ""
        ChatBox2.Text = ""

        If DommePersonalityComboBox.Items.Count < 1 Then
            MessageBox.Show(Me, "No domme Personalities were found! Please install at least one Personality directory in the Scripts folder before using this part of the program.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        If FrmSettings.CBSettingsPause.Checked And FrmSettings.SettingsPanel.Visible Then
            MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu is Visible"" option first!", , "Warning!")
            Return
        End If

#Region "This is for editing teh lazy sub buttons"
        If LazyEdit1 Then
            BTNLS1.Text = chatMessage.Message
            BTNLS1.Visible = True
            BTNLS1Edit.BackColor = My.Settings.ButtonColor
            BTNLS1Edit.ForeColor = My.Settings.TextColor
            My.Settings.LS1 = BTNLS1.Text
            Return
        End If

        If LazyEdit2 Then
            BTNLS2.Text = chatMessage.Message
            BTNLS2.Visible = True
            BTNLS2Edit.BackColor = My.Settings.ButtonColor
            BTNLS2Edit.ForeColor = My.Settings.TextColor
            My.Settings.LS2 = BTNLS2.Text
            Return
        End If

        If LazyEdit3 Then
            BTNLS3.Text = chatMessage.Message
            BTNLS3.Visible = True
            BTNLS3Edit.BackColor = My.Settings.ButtonColor
            BTNLS3Edit.ForeColor = My.Settings.TextColor
            My.Settings.LS3 = BTNLS3.Text
            Return
        End If

        If LazyEdit4 Then
            BTNLS4.Text = chatMessage.Message
            BTNLS4.Visible = True
            BTNLS4Edit.BackColor = My.Settings.ButtonColor
            BTNLS4Edit.ForeColor = My.Settings.TextColor
            My.Settings.LS4 = BTNLS4.Text
            Return
        End If

        If LazyEdit5 Then
            BTNLS5.Text = chatMessage.Message
            BTNLS5.Visible = True
            BTNLS5Edit.BackColor = My.Settings.ButtonColor
            BTNLS5Edit.ForeColor = My.Settings.TextColor
            My.Settings.LS5 = BTNLS5.Text
            Return
        End If
#End Region

        If TimeoutTimer.Enabled Then TimeoutTimer.Stop()

        ssh.ChatString = chatMessage.Message

        If CBShortcuts.Checked Then
            If UCase(ssh.ChatString) = UCase(TBShortYes.Text) Then ssh.ChatString = "Yes " & FrmSettings.TBHonorific.Text
            If UCase(ssh.ChatString) = UCase(TBShortNo.Text) Then ssh.ChatString = "No " & FrmSettings.TBHonorific.Text
            If UCase(ssh.ChatString) = UCase(TBShortEdge.Text) Then ssh.ChatString = "On the edge"
            If UCase(ssh.ChatString) = UCase(TBShortSpeedUp.Text) Then ssh.ChatString = "Let me speed up"
            If UCase(ssh.ChatString) = UCase(TBShortSlowDown.Text) Then ssh.ChatString = "Let me slow down"
            If UCase(ssh.ChatString) = UCase(TBShortStop.Text) Then ssh.ChatString = "Let me stop"
            If UCase(ssh.ChatString) = UCase(TBShortStroke.Text) Then ssh.ChatString = "May I start stroking?"
            If UCase(ssh.ChatString) = UCase(TBShortCum.Text) Then ssh.ChatString = "Please let me cum!"
            If UCase(ssh.ChatString) = UCase(TBShortGreet.Text) Then ssh.ChatString = "Hello " & FrmSettings.TBHonorific.Text
            If UCase(ssh.ChatString) = UCase(TBShortSafeword.Text) Then ssh.ChatString = FrmSettings.TBSafeword.Text
        End If

        ' User command detection? right now it doesn't do much but spit out some data
        If chatMessage.Message.StartsWith("@") Then
            Dim message As String = "<font face=""Cambria"" size=""2"" color=""Green"">"
            message += IIf(chatMessage.Message = "@", "::: TYPO :::<br>", chatMessage.Message.Replace("@", ""))
            message += " ::: FileText = " + mySession.Session.CurrentScript.MetaData.Name + " ::: LineVal = " + mySession.Session.CurrentScript.CurrentLine.ToString() + "<br>"
            message += "::: TauntText = " + ssh.TauntText + " ::: LineVal = " + ssh.TauntTextCount.ToString() + "<br>"
            message += "::: ResponseFile = " + ssh.ResponseFile + " ::: LineVal = " + ssh.ResponseLine.ToString() + "<br></font>"
            Throw New Exception("debug command")
            Return
        End If

        UpdateChatWindow(chatMessage)
        mySession.Say(chatMessage)
        Return

        If ssh.WritingTaskFlag Then GoTo WritingTaskLine

        If Not mySession.Session.Domme.WasGreeted Then
            Return
        End If

        If UCase(ssh.ChatString).Contains(UCase("stop")) Then TnASlides.Stop()

WritingTaskLine:
#Region "Writing Task Line"
        If ssh.WritingTaskFlag = True Then
            If ssh.ChatString = LBLWritingTaskText.Text Then
                ssh.WritingTaskLinesWritten += 1
                ssh.WritingTaskLinesRemaining -= 1

                LBLLinesWritten.Text = ssh.WritingTaskLinesWritten
                LBLLinesRemaining.Text = ssh.WritingTaskLinesRemaining

                If ssh.SubWroteLast = True And FrmSettings.ShowNamesCheckBox.Checked = False Then
                    'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & ssh.Chat & "</body>"
                    If CBWritingProgress.Checked = True Then
                        'ssh.Chat = "<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#000000"">" & ssh.Chat & ssh.ChatString & "<br></font> " _
                        '& "<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#006400"">" & "Correct: " & ssh.WritingTaskLinesRemaining & " lines remaining<br></font>"
                        'If FrmSettings.TimedWriting.Checked = True And ssh.WritingTaskCurrentTime < 1 Then ssh.Chat = ssh.Chat.Replace("Correct: " & ssh.WritingTaskLinesRemaining & " lines remaining", "Time Expired")
                        'ssh.Chat = ssh.Chat.Replace(" 1 lines", " 1 line")
                        'ssh.Chat = ssh.Chat.Replace(" 0 lines remaining", " Task Completed")
                        'Else
                        'ssh.Chat = "<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#000000"">" & ssh.Chat & ssh.ChatString & "<br></font>"
                    End If
                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat

                Else
                    'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & ssh.Chat & "</body>"

                    If CBWritingProgress.Checked = True Then
                        'ssh.Chat = ssh.Chat & "<font face=""Cambria"" size=""3"" font color=""" &
                        'My.Settings.SubColor & """><b>" & subName.Text & ": </b></font><font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#000000"">" & ssh.ChatString & "<br></font>" _
                        '& "<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#006400"">" & "Correct: " & ssh.WritingTaskLinesRemaining & " lines remaining<br></font>"
                        'If FrmSettings.TimedWriting.Checked = True And ssh.WritingTaskCurrentTime < 1 Then ssh.Chat = ssh.Chat.Replace("Correct: " & ssh.WritingTaskLinesRemaining & " lines remaining", "Time Expired")
                        'ssh.Chat = ssh.Chat.Replace(" 1 lines", " 1 line")
                        'ssh.Chat = ssh.Chat.Replace(" 0 lines remaining", " Task Completed")
                    Else
                        'ssh.Chat = ssh.Chat & "<font face=""Cambria"" size=""3"" font color=""" &
                        'My.Settings.SubColor & """><b>" & subName.Text & ": </b></font><font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#000000"">" & ssh.ChatString & "<br></font>"
                    End If

                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat

                End If

                If FrmSettings.CBAutosaveChatlog.Checked = True Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\Autosave.html", ChatText.DocumentText, False)

                chatBox.Text = ""
                ChatBox2.Text = ""

                ssh.SubWroteLast = True

                If ssh.WritingTaskLinesRemaining = 0 Then
                    ClearWriteTask()
                    ssh.ScriptTick = 3
                    ScriptTimer.Start()
                End If

                If ssh.WritingTaskCurrentTime < 1 And My.Settings.TimedWriting = True And ssh.WritingTaskFlag = True Then
                    ClearWriteTask()
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = "Failed Writing Task"
                    GetGoto()
                    ssh.ScriptTick = 4
                    ScriptTimer.Start()
                End If
            Else

                If ssh.SubWroteLast = True And FrmSettings.ShowNamesCheckBox.Checked = False Then

                    If CBWritingProgress.Checked = True Then
                        'ssh.Chat = "<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#000000"">" & ssh.Chat & "</font><font color=""#FF0000"">" & ssh.ChatString & "<br></font>" &
                        '"<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#CD0000"">" & "Wrong: " & (ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade) - 1 &
                        '" mistakes remaining<br></font>"
                        'If FrmSettings.TimedWriting.Checked = True And ssh.WritingTaskCurrentTime < 1 Then ssh.Chat = ssh.Chat.Replace("Wrong: " & (ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade) - 1 & " mistakes remaining", "Time Expired")
                        'ssh.Chat = ssh.Chat.Replace(" 1 mistakes", " 1 mistake")
                        'ssh.Chat = ssh.Chat.Replace(" 0 mistakes remaining", " Task Failed")
                    Else
                        'ssh.Chat = "<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#000000"">" & ssh.Chat & "</font><font color=""#FF0000"">" & ssh.ChatString & "<br></font>"
                    End If

                    'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & ssh.Chat & "</body>"
                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat

                Else

                    If CBWritingProgress.Checked = True Then
                        'ssh.Chat = ssh.Chat & "<font face=""Cambria"" size=""3"" font color=""" &
                        'My.Settings.SubColor & """><b>" & subName.Text & ": </b></font><font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#FF0000"">" & ssh.ChatString & "<br></font>" &
                        '"<font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#CD0000"">" & "Wrong: " & (ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade) - 1 &
                        '" mistakes remaining<br></font>"
                        'If FrmSettings.TimedWriting.Checked = True And ssh.WritingTaskCurrentTime < 1 Then ssh.Chat = ssh.Chat.Replace("Wrong: " & (ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade) - 1 & " mistakes remaining", "Time Expired")
                        'ssh.Chat = ssh.Chat.Replace(" 1 mistakes", " 1 mistake")
                        'ssh.Chat = ssh.Chat.Replace(" 0 mistakes remaining", " Task Failed")
                    Else
                        'ssh.Chat = ssh.Chat & "<font face=""Cambria"" size=""3"" font color=""" &
                        'My.Settings.SubColor & """><b>" & subName.Text & ": </b></font><font face=""" & FrmSettings.FontComboBox.Text & """ size=""" & FrmSettings.NBFontSize.Value & """ color=""#FF0000"">" & ssh.ChatString & "<br></font>"
                    End If

                    'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & ssh.Chat & "</body>"
                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat
                End If

                If FrmSettings.CBAutosaveChatlog.Checked = True Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\Autosave.html", ChatText.DocumentText, False)

                If ssh.IsTyping = True Then

                    'ChatText.DocumentText = ssh.Chat & "<font color=""DimGray""><i>" & domName.Text & " is typing...</i></font>"
                    'ChatText2.DocumentText = ssh.Chat & "<font color=""DimGray""><i>" & domName.Text & " is typing...</i></font>"
                End If

                ssh.SubWroteLast = True

                ssh.WritingTaskMistakesMade += 1
                LBLMistakesMade.Text = ssh.WritingTaskMistakesMade

                If ssh.WritingTaskMistakesMade = ssh.WritingTaskMistakesAllowed Then
                    ClearWriteTask()
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = "Failed Writing Task"
                    GetGoto()
                    ssh.ScriptTick = 4
                    ScriptTimer.Start()
                End If

                If ssh.WritingTaskCurrentTime < 1 And My.Settings.TimedWriting = True And ssh.WritingTaskFlag = True Then
                    ClearWriteTask()
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = "Failed Writing Task"
                    GetGoto()
                    ssh.ScriptTick = 4
                    ScriptTimer.Start()
                End If

            End If

        End If
#End Region
        If ssh.AFK = True Then Return

        ' Previous Commas

        Dim EdgeList As New List(Of String)
        EdgeList = Txt2List(Application.StartupPath & "\Scripts\" + mySession.Session.Domme.PersonalityName + "\Vocabulary\Responses\System\EdgeKEY.txt")

        Dim EdgeCheck As String = ssh.ChatString

        Dim EdgeString As String

        For i As Integer = 0 To EdgeList.Count - 1
            EdgeString = EdgeList(i)
            EdgeString = EdgeString.Replace("'", "")
            EdgeString = EdgeString.Replace(".", "")
            EdgeString = EdgeString.Replace(",", "")
            EdgeString = EdgeString.Replace("!", "")
            If UCase(EdgeCheck).Contains("DONT") Or UCase(EdgeCheck).Contains("NEVER") Or UCase(EdgeCheck).Contains("NOT") Then
                If UCase(EdgeCheck).Contains(UCase(EdgeString)) Then
                    ssh.EdgeNOT = True
                    Exit For
                End If
            End If
            If UCase(EdgeString) = UCase(EdgeCheck) Then
                ssh.EdgeFound = True
                Exit For
            End If
        Next

DebugAwareness:



        If ssh.InputFlag = True And ssh.DomTypeCheck = False Then
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ssh.InputString, ssh.ChatString, False)
            ssh.InputFlag = False
        End If

        ' Remove commas and apostrophes from user's entered text
        ssh.ChatString = ssh.ChatString.Replace(",", "")
        ssh.ChatString = ssh.ChatString.Replace("'", "")
        ssh.ChatString = ssh.ChatString.Replace(".", "")


        If UCase(ssh.ChatString) = UCase("CAME") Or UCase(ssh.ChatString) = UCase("I CAME") Or UCase(ssh.ChatString) = UCase("JUST CAME") Or UCase(ssh.ChatString) = UCase("I JUST CAME") Then
            If ssh.CameMessage = True Then
                ssh.CameMessage = False
                ssh.ChatString = ssh.CameMessageText
            End If
        End If

        If UCase(ssh.ChatString) = UCase("RUINED") Or UCase(ssh.ChatString) = UCase("I RUINED") Or UCase(ssh.ChatString) = UCase("RUINED IT") Or UCase(ssh.ChatString) = UCase("I RUINED IT") Then
            If ssh.RuinedMessage = True Then
                ssh.RuinedMessage = False
                ssh.ChatString = ssh.RuinedMessageText
            End If
        End If


        '        ' If the domme is waiting for a response, go straight to this sub-routine instead
        '        If ssh.YesOrNo = True And ssh.SubEdging = True Then GoTo EdgeSkip
        '        If ssh.YesOrNo = True And ssh.SubHoldingEdge = True Then GoTo EdgeSkip

        '        If ssh.YesOrNo = True And ssh.OrgasmYesNo = False And ssh.DomTypeCheck = False Then
        '            Await YesOrNoQuestionsAsync(mySession.Session.Domme, mySession.Session.Sub, ssh.ChatString, ssh)
        '            Return
        '        End If

        'EdgeSkip:
        'DomResponse()

    End Sub

    Private Function GetTeaseTick(mySession As Session, settings As My.MySettings) As Integer
        If mySettingsAccessor.IsTeaseLengthDommeDetermined Then
            If mySession.Domme.DomLevel = DomLevel.Gentle Then
                Return ssh.randomizer.Next(10, 16) * 60
            End If
            If mySession.Domme.DomLevel = DomLevel.Lenient Then
                Return ssh.randomizer.Next(15, 21) * 60
            End If
            If mySession.Domme.DomLevel = DomLevel.Tease Then
                Return ssh.randomizer.Next(20, 31) * 60
            End If
            If mySession.Domme.DomLevel = DomLevel.Rough Then
                Return ssh.randomizer.Next(30, 46) * 60
            End If
            If mySession.Domme.DomLevel = 5 Then
                Return ssh.randomizer.Next(45, 61) * 60
            End If
        End If
        Return ssh.randomizer.Next(mySettingsAccessor.TeaseLengthMinimum * 60, mySettingsAccessor.TeaseLengthMaximum * 60)
    End Function

    Public Function ResponseClean(ByVal CleanResponse As String) As String

        'TODO: Add Errorhandling.
        Dim DomResponse As New StreamReader(ssh.ResponseFile)
        Dim DRLines As New List(Of String)
        Dim DRLineTotal As Integer
        Dim SubState As String

        DRLineTotal = -1
        DRLines.Clear()

        Dim AddResponse As Boolean
        AddResponse = False

        If My.Settings.Chastity = True Then
            SubState = "Chastity"
            GoTo FoundState
        End If

        If ssh.BeforeTease = True Then
            SubState = "Before Tease"
            GoTo FoundState
        End If

        If ssh.FirstRound = True Then
            SubState = "First Round"
            GoTo FoundState
        End If

        If ssh.EndTease = True Then
            SubState = "After Tease"
            GoTo FoundState
        End If

        If ssh.CBTCockFlag = True Then
            SubState = "CBT Cock"
            GoTo FoundState
        End If

        If ssh.CBTBallsFlag = True Or ssh.CBTBothFlag = True Then
            SubState = "CBT Balls"
            GoTo FoundState
        End If

        If ssh.SubHoldingEdge = True Then
            SubState = "Sub Holding Edge"
            GoTo FoundState
        End If

        If ssh.SubEdging = True Then
            SubState = "Sub Edging"
            GoTo FoundState
        End If

        If ssh.SubStroking = True Then
            SubState = "Sub Stroking"
            GoTo FoundState
        End If

        SubState = "Not Stroking"

FoundState:


        If SubState = "Before Tease" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[Before Tease]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[Before Tease End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[Before Tease]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If


        If SubState = "Chastity" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[Chastity]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[Chastity End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[Chastity]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "First Round" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[First Round]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[First Round End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[First Round]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "Sub Stroking" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[Stroking]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[Stroking End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[Stroking]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "Not Stroking" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[Not Stroking]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[Not Stroking End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[Not Stroking]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "Sub Edging" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[Edging]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[Edging End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[Edging]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "Sub Holding Edge" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[Holding The Edge]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[Holding The Edge End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[Holding The Edge]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "CBT Cock" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[CBT Cock]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[CBT Cock End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[CBT Cock]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "CBT Balls" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[CBT Balls]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[CBT Balls End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[CBT Balls]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState = "After Tease" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[After Tease]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[After Tease End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[After Tease]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While
        End If

        If SubState <> "After Tease" And SubState <> "Before Tease" Then

            While DomResponse.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponse.ReadLine())
                If DRLines(DRLineTotal) = "[All]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[All End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[All]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While

        End If



        DomResponse.Close()
        DomResponse.Dispose()


        Using DomResponseAll As New StreamReader(ssh.ResponseFile)

            While DomResponseAll.Peek <> -1
                DRLineTotal += 1
                DRLines.Add(DomResponseAll.ReadLine())
                If DRLines(DRLineTotal) = "[All]" Then
                    AddResponse = True
                End If
                If DRLines(DRLineTotal) = "[All End]" Then
                    AddResponse = False
                End If
                If AddResponse = False Or DRLines(DRLineTotal) = "[All]" Then
                    DRLines.Remove(DRLines(DRLineTotal))
                    DRLineTotal -= 1
                End If
            End While

        End Using

        ' ###########

        If DRLines.Count < 1 Then
            CleanResponse = "NULL"
            GoTo NullSkip
        End If



        Try
            DRLines = FilterList(DRLines)
            ssh.ResponseLine = ssh.randomizer.Next(0, DRLines.Count)
            CleanResponse = DRLines(ssh.ResponseLine)
        Catch ex As Exception
            Log.WriteError("Tease AI did not return a valid Response line from file: " &
                           ssh.ResponseFile, ex, "ReponseClean(String)")
            CleanResponse = "ERROR: Tease AI did not return a valid Response line"
        End Try


        ssh.Responding = True

NullSkip:


        Return CleanResponse


    End Function

    Public Sub ScriptTimer_Tick(sender As Object, e As EventArgs)
        ' Handles ScriptTimer.Tick
        FrmSettings.LBLDebugScriptTime.Text = ssh.ScriptTick

        If ssh.DomTyping = True Then Return
        If ssh.YesOrNo = True Then Return

        If WaitTimer.Enabled OrElse ssh.DomTypeCheck Then Return

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If IsSpeechModeOn() = True Then
            If ssh.ScriptTick < 4 Then Return
        End If


        If ssh.DomTypeCheck = True And ssh.ScriptTick < 4 Then Return
        If chatBox.Text <> "" And ssh.ScriptTick < 4 Then Return
        If ChatBox2.Text <> "" And ssh.ScriptTick < 4 Then Return


        ssh.ScriptTick -= 1
        If ssh.ScriptTick < 1 Then
            ssh.ScriptTick = ssh.randomizer.Next(4, 7)
            RunFileText()
        End If
    End Sub

    Public Sub CBTBalls()
        Dim File2Read As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBTBalls_First.txt"

        If ssh.CBTBallsFirst = False Then
            File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBTBalls.txt"
        Else
            ssh.CBTBallsCount += 1
        End If

        ' Read all Lines of the given File.
        Dim BallList As List(Of String) = Txt2List(File2Read)

        Try
            BallList = FilterList(BallList)
            ssh.DomTask = BallList(ssh.randomizer.Next(0, BallList.Count))
        Catch ex As Exception
            Log.WriteError("Tease AI did not return a valid @CBTBalls line from file: " &
                           File2Read, ex, "CBTBalls()")
            ssh.DomTask = "ERROR: Tease AI did not return a valid @CBTBalls line"
        End Try

        ssh.CBTBallsFirst = False

    End Sub

    Public Sub CBTBoth()

        Dim File2Read As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBTBalls_First.txt"

        If ssh.CBTBothFirst = False Then
            File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBTBalls.txt"
        Else
            ssh.CBTBallsCount += 1
            ssh.CBTCockCount += 1
        End If

        ' Read all Lines of the given File.
        Dim BothList As List(Of String) = Txt2List(File2Read)

        File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBTCock_First.txt"

        If ssh.CBTBothFirst = False Then
            File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBTCock.txt"
        Else
            ssh.CBTBallsCount += 1
            ssh.CBTCockCount += 1
        End If

        ' Read all Lines of the given file and append to List.
        BothList.AddRange(Txt2List(File2Read))

        Try
            BothList = FilterList(BothList)
            ssh.DomTask = BothList(ssh.randomizer.Next(0, BothList.Count))
        Catch ex As Exception
            Log.WriteError("Tease AI did not return a valid @CBT line from file: " &
                           File2Read, ex, "CBTBoth()")
            ssh.DomTask = "ERROR: Tease AI did not return a valid @CBT line"
        End Try

        ssh.CBTBothFirst = False
    End Sub

    Public Sub RunCustomTask()

        Dim File2Read As String = ssh.CustomTaskTextFirst

        If ssh.CustomTaskFirst = False Then
            File2Read = ssh.CustomTaskText
        End If

        ' Read all Lines of the given File.
        Dim CustomList As List(Of String) = Txt2List(File2Read)

        Try
            CustomList = FilterList(CustomList)
            ssh.DomTask = CustomList(ssh.randomizer.Next(0, CustomList.Count))
        Catch ex As Exception
            Log.WriteError("Tease AI did not return a valid Custom Taks line from file: " & File2Read, ex, "RunCustomTask()")
            ssh.DomTask = "ERROR: Tease AI did not return a valid Custom Task line"
        End Try

        ssh.CustomTaskFirst = False

    End Sub

    Public Function AdvanceOverBookmark(script As List(Of String), currentLine As Integer) As Integer
        Do While currentLine < script.Count AndAlso script(currentLine)(0) = "("
            currentLine += 1
        Loop
        Return Math.Min(currentLine, script.Count)
    End Function

    Public Sub RunFileText()
        ssh.TeaseVideo = False
        ' If we are doing anything else, then exit out
        If Not mySession.Session.Domme.WasGreeted Then Return
        If ssh.CBTCockFlag OrElse ssh.CBTBallsFlag OrElse ssh.CBTBothFlag OrElse ssh.CustomTask Then Return
        If ssh.WritingTaskFlag Then Return
        If ssh.TeaseVideo Then Return
        If ssh.RiskyDelay Then Return
        If ssh.InputFlag Then Return

        ' Miniscripts can interrupt another thing
        If ssh.MiniScript Then GoTo ReturnCalled

        If ssh.CensorshipGame OrElse ssh.RLGLGame OrElse ssh.AvoidTheEdgeStroking OrElse ssh.SubEdging OrElse ssh.SubHoldingEdge Then Return
        If ssh.MultipleEdges Then Return

ReturnCalled:

        Dim lines As New List(Of String)

        If ssh.MiniScript Then
            lines = Txt2List(ssh.MiniScriptText)
            ssh.MiniTauntVal = AdvanceOverBookmark(lines, ssh.MiniTauntVal + 1)
        Else
            lines = Txt2List(ssh.FileText)
            ssh.StrokeTauntVal = AdvanceOverBookmark(lines, ssh.StrokeTauntVal + 1)
        End If

        If Not ssh.RunningScript AndAlso Not ssh.AvoidTheEdgeGame AndAlso Not ssh.ReturnFlag Then
            If ssh.MiniScript = True Then
                If lines(ssh.MiniTauntVal) = Keyword.End Then
                    ssh.MiniScript = False
                    If ssh.MiniTimerCheck Then
                        ssh.ScriptTick = 3
                        ScriptTimer.Start()
                    Else
                        ScriptTimer.Stop()
                    End If
                    Return
                End If
            Else
                If (Not ssh.StrokeTauntVal > lines.Count - 1) AndAlso (lines(ssh.StrokeTauntVal) = Keyword.End) AndAlso ssh.ShowModule Then
                    ssh.ModuleEnd = True
                End If
            End If
        End If

        HandleScripts()
    End Sub

    Public Sub HandleScripts()
ModuleEnd:

        If ssh.ModuleEnd AndAlso Not ssh.AvoidTheEdgeGame Then
            ScriptTimer.Stop()
            ssh.ModuleEnd = False
            ssh.ShowModule = False

            'MOVE TO NEXT FILE
            If ssh.Playlist Then
                If ssh.PlaylistCurrent = ssh.PlaylistFile.Count - 1 Then
                    RunLastScript()
                Else
                    RunLinkScript()
                End If
            Else
                If ssh.TeaseTick < 1 And ssh.BookmarkModule = False Then
                    RunLastScript()
                Else
                    RunLinkScript()
                End If
            End If
            Return
        End If

        If StrokeTimer.Enabled AndAlso ssh.MiniScript Then Return


        Dim lines As List(Of String)
        If ssh.MiniScript = True Then
            lines = Txt2List(ssh.MiniScriptText)
        Else
            lines = mySession.Session.CurrentScript.Lines.ToList()
        End If

        'If File.Exists(HandleScriptText) Then
        'Dim ioFile As New StreamReader(HandleScriptText)
        Dim currentLine As Integer

        'line = ScriptLineVal

        If ssh.MiniScript = True Then
            currentLine = ssh.MiniTauntVal
        Else
            currentLine = ssh.StrokeTauntVal
        End If

        If currentLine = lines.Count Then
            If ssh.ShowModule = True Then
                ssh.ModuleEnd = True
                GoTo ModuleEnd
            Else
                GoTo NonModuleEnd
            End If
        End If

        If GetFilter(lines(currentLine), True) = False Then
            RunFileText()
            Return
        End If

        If lines(currentLine) = Keyword.End Then

NonModuleEnd:

            If ssh.RiskyEdges = True Then ssh.RiskyEdges = False
            If ssh.LastScript = True Then
                ssh.LastScript = False
                ssh.EndTease = True
            End If
            If ssh.HypnoGen = True Then
                If ssh.Induction = True Then
                    ssh.Induction = False
                    ssh.StrokeTauntVal = -1
                    ssh.FileText = ssh.TempHypno
                    ssh.ScriptTick = 1
                    ScriptTimer.Start()
                    Return
                End If
                ssh.HypnoGen = False
                ssh.AFK = False
                DomWMP.Ctlcontrols.stop()
                BTNHypnoGenStart.Text = "Guide Me!"
            End If
            Dim submissive As SubPersonality = CreateSubPersonality()
            If ssh.ReturnFlag Then
                ssh.ReturnFlag = False
                ssh.FileText = ssh.ReturnFileText
                ssh.StrokeTauntVal = ssh.ReturnStrokeTauntVal

                If ssh.ReturnSubState = "Stroking" Then
                    If My.Settings.Chastity = True Then
                        'DomTask = "Now as I was saying @StartTaunts"
                        ssh.DomTask = "#Return_Chastity"
                    Else
                        If ssh.SubStroking = False Then
                            'DomTask = "Get back to stroking @StartStroking"
                            ssh.DomTask = "#Return_Stroking"
                        Else
                            StrokeTimer.Start()
                            StrokeTauntTimer.Start()
                        End If
                    End If
                End If
                If ssh.ReturnSubState = "Edging" Then

                    If ssh.SubEdging = False Then
                        'DomTask = "Start getting yourself to the edge again @Edge"
                        ssh.DomTask = "#Return_Edging"
                    Else
                        EdgeTauntTimer.Start()
                        EdgeCountTimer.Start()
                    End If
                End If
                If ssh.ReturnSubState = "Holding The Edge" Then
                    If ssh.SubEdging = False Then
                        'DomTask = "Start getting yourself to the edge again @EdgeHold"
                        ssh.DomTask = "#Return_Holding"
                    Else
                        HoldEdgeTimer.Start()
                        HoldEdgeTauntTimer.Start()
                    End If
                End If
                If ssh.ReturnSubState = "CBTBalls" Then
                    'DomTask = "Now let's get back to busting those #Balls @CBTBalls"
                    ssh.DomTask = "#Return_CBTBalls"
                    ssh.CBTBallsFirst = False
                End If
                If ssh.ReturnSubState = "CBTCock" Then
                    'DomTask = "Now let's get back to abusing that #Cock @CBTCock"
                    ssh.DomTask = "#Return_CBTCock"
                    ssh.CBTCockFirst = False
                End If
                If ssh.ReturnSubState = "Rest" Then
                    ssh.DomTypeCheck = True
                    ssh.ScriptTick = 5
                    ScriptTimer.Start()
                    'DomTask = "Now as I was saying"
                    ssh.DomTask = "#Return_Rest"
                    Return
                End If
            End If
            ScriptTimer.Stop()
            Return
        End If

        If currentLine < lines.Count - 1 Then
            If lines(currentLine + 1).Substring(0, 1) = "[" Then
                ssh.YesOrNo = True
                ScriptTimer.Stop()
            End If
        End If

        ssh.DomTask = (lines(currentLine).Trim)





        ssh.StringLength = 1



        ssh.DomTask = ssh.DomTask.Replace("#SubName", SubName.Text)

        ssh.DomTask = ssh.DomTask.Replace("#VTLength", ssh.VTLength / 60)


        If InStr(ssh.DomTask, "@CockSizeSmall") <> 0 Then
            ssh.DivideText = True
        End If

        If ssh.DomTask.Contains("@ShowTaggedImage") Then ssh.JustShowedBlogImage = True

        If ssh.DomTask.Contains("@NullResponse") Then ssh.NullResponse = True

        If ssh.HypnoGen = True Then

            If CBHypnoGenSlideshow.Checked = True Then

                If LBHypnoGenSlideshow.SelectedItem = "Boobs" Then ssh.DomTask = ssh.DomTask & " @ShowBoobsImage"
                If LBHypnoGenSlideshow.SelectedItem = "Butts" Then ssh.DomTask = ssh.DomTask & " @ShowButtImage"
                If LBHypnoGenSlideshow.SelectedItem = "Hardcore" Then ssh.DomTask = ssh.DomTask & " @ShowHardcoreImage"
                If LBHypnoGenSlideshow.SelectedItem = "Softcore" Then ssh.DomTask = ssh.DomTask & " @ShowSoftcoreImage"
                If LBHypnoGenSlideshow.SelectedItem = "Lesbian" Then ssh.DomTask = ssh.DomTask & " @ShowLesbianImage"
                If LBHypnoGenSlideshow.SelectedItem = "Blowjob" Then ssh.DomTask = ssh.DomTask & " @ShowBlowjobImage"
                If LBHypnoGenSlideshow.SelectedItem = "Femdom" Then ssh.DomTask = ssh.DomTask & " @ShowFemdomImage"
                If LBHypnoGenSlideshow.SelectedItem = "Lezdom" Then ssh.DomTask = ssh.DomTask & " @ShowLezdomImage"
                If LBHypnoGenSlideshow.SelectedItem = "Hentai" Then ssh.DomTask = ssh.DomTask & " @ShowHentaiImage"
                If LBHypnoGenSlideshow.SelectedItem = "Gay" Then ssh.DomTask = ssh.DomTask & " @ShowGayImage"
                If LBHypnoGenSlideshow.SelectedItem = "Maledom" Then ssh.DomTask = ssh.DomTask & " @ShowMaledomImage"
                If LBHypnoGenSlideshow.SelectedItem = "Captions" Then ssh.DomTask = ssh.DomTask & " @ShowCaptionsImage"
                If LBHypnoGenSlideshow.SelectedItem = "General" Then ssh.DomTask = ssh.DomTask & " @ShowGeneralImage"
                If LBHypnoGenSlideshow.SelectedItem = "Tagged" Then ssh.DomTask = ssh.DomTask & " @ShowTaggedImage @Tag" & TBHypnoGenImageTag.Text



            End If

        End If


        If ssh.DomTask <> "" Then
        Else
            RunFileText()
        End If

    End Sub

    Public Sub GetGoto()
        'BUG: @Goto Command is sometimes searching in the wrong file. Description: https://milovana.com/forum/viewtopic.php?f=2&t=15776&p=219171#p219169
        'If (Not ssh.DomTask.Contains("@Goto")) Then
        '    Return
        'End If
        ssh.GotoFlag = True

        'WARNING BROKEN
        If (ssh.GotoDommeLevel) Then
            ssh.FileGoto = "(DommeLevel)"
        ElseIf Not ssh.GotoDommeLevel AndAlso Not ssh.SkipGotoLine Then
            'Dim getBookmark = myGotoProcessor.GetBookmark(ssh.DomTask)
            'ssh.FileGoto = getBookmark.Value
        End If

        Dim bookmark As String = ssh.FileGoto
        Dim fileName As String = IIf(ssh.MiniScript, ssh.MiniScriptText, ssh.FileText)
        Dim script As List(Of String) = Txt2List(fileName)

        'ssh.DomTask = myGotoProcessor.DeleteGoto(ssh.DomTask)

        'Dim gotoLine As Result(Of Integer) = myGotoProcessor.FindBookmark(script, ssh.FileGoto)
        '' Set the current line number on the script
        'If ssh.MiniScript = True Then
        '    ssh.MiniTauntVal = gotoLine.Value
        'Else
        '    ssh.StrokeTauntVal = gotoLine.GetResultOrDefault(script.Count)
        'End If

        ssh.GotoDommeLevel = False
        ssh.SkipGotoLine = False
    End Sub

    ''' <summary>
    ''' adds <paramref name="chatMessage"/> to <see cref="myChatLog"/>, then adds the log to the chat windows
    ''' </summary>
    ''' <param name="chatMessage"></param>
    Private Sub UpdateChatWindow(chatMessage As ChatMessage)
        Dim messagePreferences As Dictionary(Of String, ChatMessagePreferences) = New Dictionary(Of String, ChatMessagePreferences)()
        messagePreferences(mySession.Session.Domme.Name) = CreateDommeMessagePreferences()
        messagePreferences(mySession.Session.Sub.Name) = CreateSubMessagePreferences()
        myChatLog.Add(chatMessage)
        Dim chatlog As List(Of ChatMessage) = myChatLog.ToList()
        If myDommeMessages.Any() Then
            Dim typingMessage As ChatMessage = New ChatMessage()
            typingMessage.Sender = mySession.Session.Domme.Name
            typingMessage.TimeStamp = DateTime.Now
            typingMessage.Message = "..."
            chatlog.Add(typingMessage)
        End If
        Dim chatLogHtml As String = myChatLogToHtmlService.CreateHtml(myChatLog, messagePreferences)
        AppendChatMessage(chatLogHtml, True)

        Try
            ChatText.Document.Window.ScrollTo(Integer.MaxValue, Integer.MaxValue)
        Catch
        End Try

        Try
            ChatText2.Document.Window.ScrollTo(Integer.MaxValue, Integer.MaxValue)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Timer for domme sending messages
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If FrmSettings.CBSettingsPause.Checked AndAlso FrmSettings.SettingsPanel.Visible Then Return

        ssh.DomTyping = True
        Dim ShowPicture As Boolean = False
        ssh.DomTypeCheck = True

        If CBHypnoGenNoText.Checked AndAlso ssh.HypnoGen Then ssh.NullResponse = True
        If ssh.DomTask.Contains("@SlideshowOff") Then CustomSlideshowTimer.Stop()

        If ssh.DomTask.Contains("@NullResponse") Then
            ssh.NullResponse = True
        Else
            ssh.RapidCode = False
        End If


        If Not ssh.Group.Contains("D") And Not ssh.DomTask.Contains("@Contact1") And Not ssh.DomTask.Contains("@Contact2") And Not ssh.DomTask.Contains("@Contact3") Then
            Dim groupList As New List(Of String)
            If ssh.Group.Contains("1") Then groupList.Add(" @Contact1 ")
            If ssh.Group.Contains("2") Then groupList.Add(" @Contact2 ")
            If ssh.Group.Contains("3") Then groupList.Add(" @Contact3 ")
            ssh.DomTask = ssh.DomTask & groupList(ssh.randomizer.Next(0, groupList.Count))
        End If


        If ssh.NullResponse Then
            Timer1.Stop()
            GoTo NullResponse
        End If

        ' Toggle switch to let the program know when to display "Domme is typing..." and when to remove it and display what she wrote
        If ssh.TypeToggle = 0 Then
            If ssh.TypeDelay > 0 Then
                ssh.TypeDelay -= 1
            Else
                Timer1.Stop()
                If ssh.RiskyDeal Then GamesWindow.LblRiskType.Visible = True
                If Not ssh.NullResponse Then
                    ssh.IsTyping = True
                    Dim typingName As String = domName.Text
                    If ssh.DomTask.Contains("@Contact1") Then typingName = My.Settings.Glitter1
                    If ssh.DomTask.Contains("@Contact2") Then typingName = My.Settings.Glitter2
                    If ssh.DomTask.Contains("@Contact3") Then typingName = My.Settings.Glitter3
                    'If TypingName <> domName.Text Then JustShowedBlogImage = True

                    If ssh.DomTask.Contains("@EmoteMessage") Then ssh.EmoMes = True

                    If ssh.DomTask.Contains("@SystemMessage") Then
                        ssh.SysMes = True
                        ssh.TypeDelay = 0
                        GoTo SkipIsTyping
                    End If
SkipIsTyping:
                End If

                ssh.TypeToggle = 1
                ssh.StringLength = ssh.DomTask.Length
                If ssh.DivideText = True Then
                    ssh.StringLength /= 3
                    ssh.DivideText = False
                End If
                If ssh.RLGLGame = True Then ssh.StringLength = 0
                If FrmSettings.TypeInstantlyCheckBox.Checked = True Or ssh.RapidCode = True Then ssh.StringLength = 0
                If ssh.HypnoGen = True And CBHypnoGenNoText.Checked = True Then ssh.StringLength = 0
            End If
        Else

            If ssh.TypeDelay > 0 Then
                ssh.TypeDelay -= 1
                If ssh.DomTask.Contains("@SystemMessage") Then ssh.TypeDelay = 0

            Else
                ssh.TypeToggle = 0
                Timer1.Stop()
                ssh.IsTyping = False
                If ssh.RiskyDeal = True Then GamesWindow.LblRiskType.Visible = False

                ssh.ResponseYes = ""
                ssh.ResponseNo = ""

                ' If PreCleanString.Contains("#") Then GoTo PoundLoop

                ' DomTask = PreCleanString

                '################## Display a Slideimage? #################
                'TODO: Optimize Code. Since images loaded by the Backgroundworker are prioritized, this section can be shrinked down.
                If ssh.DomTask.Contains("@ImageTag") Then ssh.JustShowedBlogImage = True

                If ssh.DomTask.Contains("@ShowHardcoreImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowSoftcoreImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowLesbianImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowBlowjobImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowFemdomImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowLezdomImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowHentaiImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowGayImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowMaledomImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowCaptionsImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowGeneralImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowLocalImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@ShowBlogImage") Then ssh.JustShowedBlogImage = True
                If ssh.DomTask.Contains("@NewBlogImage") Then ssh.JustShowedBlogImage = True

                If ssh.DomTask.Contains("@SlideshowFirst") Then ssh.JustShowedSlideshowImage = True
                If ssh.DomTask.Contains("@SlideshowNext") Then ssh.JustShowedSlideshowImage = True
                If ssh.DomTask.Contains("@SlideshowPrevious") Then ssh.JustShowedSlideshowImage = True
                If ssh.DomTask.Contains("@SlideshowLast") Then ssh.JustShowedSlideshowImage = True


                If ssh.GlitterTease = True And ssh.JustShowedBlogImage = False And ssh.LockImage = False Then GoTo TryNextWithTease


                If FrmSettings.TeaseSlideShowRadio.Checked = True And ssh.JustShowedBlogImage = False And ssh.TeaseVideo = False And Not ssh.DomTask.Contains("@NewBlogImage") And ssh.NullResponse = False _
                     And ssh.SlideshowLoaded = True And Not ssh.DomTask.Contains("@ShowButtImage") And Not ssh.DomTask.Contains("@ShowBoobsImage") And Not ssh.DomTask.Contains("@ShowButtsImage") _
                     And Not ssh.DomTask.Contains("@ShowBoobsImage") And ssh.LockImage = False And ssh.CustomSlideEnabled = False And ssh.RapidFire = False _
                     And UCase(ssh.DomTask) <> "<B>TEASE AI HAS BEEN RESET</B>" And ssh.JustShowedSlideshowImage = False Then
                    If ssh.SubStroking = False Or ssh.SubEdging = True Or ssh.SubHoldingEdge = True Then
                        ' Begin Next Button

                        ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
TryNextWithTease:



                    End If
                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                    ShowPicture = True

                End If



NullResponse:


                If ssh.DomTask.Contains("@WritingTask(") Then
                    Dim WriteFlag As String = GetParentheses(ssh.DomTask, "@WritingTask(")
                    ssh.DomTask = ssh.DomTask.Replace(WriteFlag, PoundClean(WriteFlag))
                End If

                If ssh.DomTask.Contains("@Contact1") Or ssh.DomTask.Contains("@Contact2") Or ssh.DomTask.Contains("@Contact3") Then ssh.SubWroteLast = True

                '################### Gather Response Data #################
                'TODO-Next: Test Code
                ContactToUse = ssh.SlideshowMain

                If ssh.DomTask.Contains("@Contact1") Then _
                    ContactToUse = ssh.SlideshowContact1

                If ssh.DomTask.Contains("@Contact2") Then _
                    ContactToUse = ssh.SlideshowContact2

                If ssh.DomTask.Contains("@Contact3") Then _
                    ContactToUse = ssh.SlideshowContact3

                Dim TypeName As String = ContactToUse.TypeName
                Dim TypeColor As String = ContactToUse.TypeColorHtml
                Dim TypeFont As String = ContactToUse.TypeFont
                Dim TypeSize As String = ContactToUse.TypeSize

                Dim TTSVoice As String = FrmSettings.TTSComboBox.Text
                Dim TTSrate As Integer = ContactToUse.TTSrate
                Dim TTSvolume As String = ContactToUse.TTSvolume

                ' Set LineSpeaker for typo corrections.
                Dim LineSpeaker As String = ""

                If ContactToUse.Equals(ssh.SlideshowContact1) Then
                    LineSpeaker = "@Contact1 "
                ElseIf ContactToUse.Equals(ssh.SlideshowContact2) Then
                    LineSpeaker = "@Contact2 "
                ElseIf ContactToUse.Equals(ssh.SlideshowContact3) Then
                    LineSpeaker = "@Contact3 "
                End If


                If FrmSettings.TTSCheckBox.Checked = True And TTSVoice <> "No voices installed" Then
                    Dim EmoteArray() As String = Split(ssh.DomTask)
                    For i As Integer = 0 To EmoteArray.Length - 1
                        Try
                            If EmoteArray(i).Contains("#") And LCase(EmoteArray(i)).Contains("emote") Then
                                EmoteArray(i) = EmoteArray(i).Replace(EmoteArray(i), "")
                            End If
                        Catch
                        End Try
                    Next
                    ssh.DomTask = Join(EmoteArray)
                End If

                'SaveBlogImage.Text = ""

                'If RiskyDeal = True Then Me.Focus()

                Dim LoopBuffer As Integer = 0


#If TRACE Then
                Dim sw As New Stopwatch
                sw.Start()

                Trace.WriteLine("Timer1 Parse Line: " & ssh.DomTask)
                Trace.Indent()
#End If
                Do
                    LoopBuffer += 1

                    ssh.DomTask = ssh.DomTask.Replace("#Null", "")
                    ssh.DomTask = PoundClean(ssh.DomTask)
                    If ssh.DomTask.Contains("@EmoteMessage") Then ssh.EmoMes = True
                    ssh.DomTask = CommandClean(ssh.DomTask)
                    ssh.DomTask = StripCommands(ssh.DomTask)
                    ssh.DomTask = ssh.DomTask.Replace("#Null", "")
                    ssh.DomTask = PoundClean(ssh.DomTask)

                    If LoopBuffer > 4 Then Exit Do

                Loop Until Not ssh.DomTask.Contains("#") And Not ssh.DomTask.Contains("@")
#If TRACE Then
                Trace.Unindent()
                Trace.WriteLine("Timer1 finished - Duration: " & sw.ElapsedMilliseconds & "ms")
#End If



                If CBHypnoGenNoText.Checked = True And ssh.HypnoGen = True Then GoTo HypNoResponse
                If ssh.NullResponse = True Then GoTo NoResponse

                ' Dim AtArray() As String = Split(DomTask)
                ' For i As Integer = 0 To AtArray.Length - 1
                'If AtArray(i) = "" Then GoTo AtBreak
                'If AtArray(i) = "" Then GoTo AtNext
                ' If AtArray(i).Contains("@") Then
                'AtArray(i) = AtArray(i).Replace(AtArray(i), "")
                'End If
                'AtNext:

                ' Next

                'DomTask = Join(AtArray)

                'AtBreak:


                If ssh.DomTask.Contains("(") And ssh.DomTask.Contains(")") Then
                    Dim ParenReg As RegularExpressions.Regex
                    ParenReg = New RegularExpressions.Regex("\(([^\)]*)\)")
                    ssh.DomTask = ssh.DomTask.Replace(ParenReg.Match(ssh.DomTask).Value(), "")
                End If

                ' Github Patch If SysMes = False And EmoMes = False Then
                If ssh.SysMes = False And ssh.EmoMes = False And Not ssh.DomTask = "" Then

                    Try
                        Dim UCASELine As String = UCase(ssh.DomTask.Substring(0, 1))
                        ssh.DomTask = ssh.DomTask.Remove(0, 1).Insert(0, UCASELine)
                    Catch
                    End Try


                    If FrmSettings.LCaseCheckBox.Checked = True Then ssh.DomTask = LCase(ssh.DomTask)
                    If FrmSettings.CBMeMyMine.Checked = True Then
                        Dim MeArray() As String = Split(ssh.DomTask)
                        For i As Integer = MeArray.Length - 1 To 0 Step -1
                            If UCase(MeArray(i)) = "ME" Then MeArray(i) = "Me"
                            If UCase(MeArray(i)) = "MY" Then MeArray(i) = "My"
                            If UCase(MeArray(i)) = "MINE" Then MeArray(i) = "Mine"
                            If UCase(MeArray(i)) = "I" Then MeArray(i) = "I"
                            If UCase(MeArray(i)) = "I'D" Then MeArray(i) = "I'd"
                            If UCase(MeArray(i)) = "I'M" Then MeArray(i) = "I'm"
                            If UCase(MeArray(i)) = "I'LL" Then MeArray(i) = "I'll"
                            If UCase(MeArray(i)) = "YOU" Then MeArray(i) = "you"
                            If UCase(MeArray(i)) = "YOUR" Then MeArray(i) = "your"
                            If UCase(MeArray(i)) = "YOURS" Then MeArray(i) = "yours"
                            If UCase(MeArray(i)) = "YOU'RE" Then MeArray(i) = "you're"
                            If UCase(MeArray(i)) = "YOU'D" Then MeArray(i) = "you'd"
                            If UCase(MeArray(i)) = "YOU'LL" Then MeArray(i) = "you'll"
                        Next
                        ssh.DomTask = Join(MeArray)
                    End If
                    If FrmSettings.apostropheCheckBox.Checked = True Then ssh.DomTask = ssh.DomTask.Replace("'", "")
                    If FrmSettings.commaCheckBox.Checked = True Then ssh.DomTask = ssh.DomTask.Replace(",", "")
                    If FrmSettings.periodCheckBox.Checked = True Then ssh.DomTask = ssh.DomTask.Replace(".", "")

                    ' Try
                    'DomTask = DomTask.Replace("*", FrmSettings.domemoteComboBox.Text.Substring(0, 1))
                    'Catch
                    'End Try

                    Dim EmoToggle As Boolean = True
                    For i As Integer = ssh.DomTask.Length - 1 To 0 Step -1
                        If ssh.DomTask.Substring(i, 1) = "*" Then
                            If EmoToggle = False Then
                                EmoToggle = True
                                ssh.DomTask = ssh.DomTask.Remove(i, 1).Insert(i, FrmSettings.TBEmote.Text)
                            Else
                                EmoToggle = False
                                ssh.DomTask = ssh.DomTask.Remove(i, 1).Insert(i, FrmSettings.TBEmoteEnd.Text)
                            End If
                        End If
                    Next

                    ssh.DomTask = ssh.DomTask.Replace(":d", ":D")
                    ssh.DomTask = ssh.DomTask.Replace(": d", ": D")


                    'Typo Test

                    Try

                        Dim RestoreDomTask As String = ssh.DomTask

                        If Not ssh.DomTask.Substring(0, 1) = FrmSettings.TBEmote.Text.Substring(0, 1) And Not ssh.DomTask.Contains("<") And ssh.YesOrNo = False And ssh.TypoSwitch <> 0 And ssh.TyposDisabled = False _
                             And FrmSettings.TTSCheckBox.Checked = False Then

                            Dim TypoChance As Integer = ssh.randomizer.Next(0, 101)

                            If TypoChance < FrmSettings.NBTypoChance.Value Or ssh.TypoSwitch = 2 Then

                                Try

                                    Dim TypoString As String

                                    Dim TypoSplit As String() = ssh.DomTask.Split(" ")

                                    ssh.TempVal = ssh.randomizer.Next(0, TypoSplit.Count)

                                    ssh.CorrectedWord = TypoSplit(ssh.TempVal)

                                    ssh.CorrectedWord = ssh.CorrectedWord.Replace(",", "")
                                    ssh.CorrectedWord = ssh.CorrectedWord.Replace(".", "")
                                    ssh.CorrectedWord = ssh.CorrectedWord.Replace("!", "")
                                    ssh.CorrectedWord = ssh.CorrectedWord.Replace("?", "")

                                    TypoString = "w d s f x"


                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "a" Then TypoString = "q w s z x"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "b" Then TypoString = "f v g h n"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "c" Then TypoString = "x d f v b"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "d" Then TypoString = "s c f x e"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "e" Then TypoString = "s r w 3 d"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "f" Then TypoString = "d r g v c"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "g" Then TypoString = "f t b h y"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "h" Then TypoString = "g b n u j"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "i" Then TypoString = "o u j k l"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "j" Then TypoString = "k u i n h"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "k" Then TypoString = "j m , l i"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "l" Then TypoString = "; p . , i"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "m" Then TypoString = "n j k , l"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "n" Then TypoString = "b h j k m"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "o" Then TypoString = "p 0 i k ;"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "p" Then TypoString = "[ - o ; l"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "q" Then TypoString = "1 w s a 2"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "r" Then TypoString = "4 5 t f d"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "s" Then TypoString = "w d a z x"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "t" Then TypoString = "5 6 g y r"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "u" Then TypoString = "y 7 j i k"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "v" Then TypoString = "c f g h b"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "w" Then TypoString = "2 a e q s"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "x" Then TypoString = "z s d f c"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "y" Then TypoString = "t 7 h u g"
                                    If LCase(TypoSplit(ssh.TempVal).Substring(0, 1)) = "z" Then TypoString = "a s x d c"


                                    Dim UpperChance As Integer = ssh.randomizer.Next(0, 101)
                                    If UpperChance < 26 Then TypoString = UCase(TypoString)



                                    Dim GetTypo As String() = TypoString.Split(" ")

                                    Dim MadeTypo As String = GetTypo(ssh.randomizer.Next(0, GetTypo.Count))


                                    Dim DoubleChance As Integer = ssh.randomizer.Next(0, 101)
                                    If DoubleChance < 11 Then MadeTypo = MadeTypo & LCase(GetTypo(ssh.randomizer.Next(0, GetTypo.Count)))


                                    TypoSplit(ssh.TempVal) = TypoSplit(ssh.TempVal).Remove(0, 1)

                                    Dim SpaceChance As Integer = ssh.randomizer.Next(0, 101)
                                    If SpaceChance < 7 Then
                                        TypoSplit(ssh.TempVal) = MadeTypo & " " & TypoSplit(ssh.TempVal)
                                    Else
                                        TypoSplit(ssh.TempVal) = MadeTypo & TypoSplit(ssh.TempVal)

                                    End If

                                    ssh.DomTask = Join(TypoSplit)

                                    ssh.CorrectedTypo = True

                                Catch

                                    ssh.DomTask = RestoreDomTask
                                    ssh.CorrectedTypo = False
                                End Try

                            End If

                        End If

                        ssh.TypoSwitch = 1

                    Catch
                    End Try


                End If

                ssh.DomTask = ssh.DomTask.Replace("ATSYMBOL", "@")
                ssh.DomTask = ssh.DomTask.Replace("atsymbol", "@")

                If ssh.InputIcon = True Then
                    ' github patch DomTask = DomTask & " <img src=""file://" & Application.StartupPath & "/Images/System/input.png""/>"
                    ssh.DomTask = ssh.DomTask & " <img src=""file://" & Application.StartupPath & "/Images/System/input.png"" title=""This icon means your Domme will remember your answer!""/>"
                    ssh.InputIcon = False
                End If

                ssh.DomTask = ssh.DomTask.Replace(" a a", " an a")
                ssh.DomTask = ssh.DomTask.Replace(" a e", " an e")
                ssh.DomTask = ssh.DomTask.Replace(" a i", " an i")
                ssh.DomTask = ssh.DomTask.Replace(" a o", " an o")
                ssh.DomTask = ssh.DomTask.Replace(" a u", " an u")

                ssh.DomTask = ssh.DomTask.Replace(" an uni", " a uni")
                ssh.DomTask = ssh.DomTask.Replace(" an utensil", " a utensil")
                ssh.DomTask = ssh.DomTask.Replace(" an ukulele", " a ukulele")
                ssh.DomTask = ssh.DomTask.Replace(" an use", " a use")
                ssh.DomTask = ssh.DomTask.Replace(" an urethra", " a urethra")
                ssh.DomTask = ssh.DomTask.Replace(" an urine", " a urine")
                ssh.DomTask = ssh.DomTask.Replace(" an usual", " a usual")
                ssh.DomTask = ssh.DomTask.Replace(" an utility", " a utility")
                ssh.DomTask = ssh.DomTask.Replace(" an uterus", " a uterus")
                ssh.DomTask = ssh.DomTask.Replace(" an utopia", " a utopia")


                'SUGGESTION: (Stefaf) All Writing to the Chatbox and Wating for fetched Images shoud be in a separat Function. 


                Dim TextColor As String = Color2Html(My.Settings.ChatTextColor)

                If ssh.NullResponse = False And ssh.DomTask <> "" Then

                    If UCase(ssh.DomTask) = "<B>TEASE AI HAS BEEN RESET</B>" Then ssh.DomTask = "<b>Tease AI has been reset</b>"


                    If ssh.SysMes = True Then
                        'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & ssh.DomTask & "</b><br></font></body>"
                        'ssh.SysMes = False
                        'ChatText.DocumentText = ssh.Chat
                        'ChatText2.DocumentText = ssh.Chat
                        GoTo EndSysMes
                    End If

                    If ssh.EmoMes = True Then
                        'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""" &
                        'TypeColor & """><b><i>" & ssh.DomTask & "</i></b><br></font></body>"
                        'ssh.EmoMes = False
                        'ChatText.DocumentText = ssh.Chat
                        'ChatText2.DocumentText = ssh.Chat
                        GoTo EndSysMes
                    End If

                    ' Add timestamps to domme response if the option is checked in the menu
                    If FrmSettings.TimeStampCheckBox.Checked = True And FrmSettings.WebTeaseMode.Checked = False Then
                        'ssh.Chat = ssh.Chat & "<font face=""Cambria"" size=""2"" color=""DimGray"">" & (Date.Now.ToString("hh:mm tt ")) & "</font>"
                    End If



                    If ssh.SubWroteLast = False And FrmSettings.ShowNamesCheckBox.Checked = False Then


                        If FrmSettings.WebTeaseMode.Checked = True Then
                            'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & "</body><body style=""word-wrap:break-word;"">" & "<font face=""" & FrmSettings.FontComboBoxD.Text & """ size=""" & FrmSettings.NBFontSizeD.Value & """ color=""" &
                            'TextColor & """><center>" & ssh.DomTask & "</center><br></font></body>"
                        Else
                            'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & FrmSettings.FontComboBoxD.Text & """ size=""" & FrmSettings.NBFontSizeD.Value & """ color=""" &
                            'TextColor & """>" & ssh.Chat & ssh.DomTask & "<br></font></body>"
                        End If


                        'ChatText.DocumentText = ssh.Chat
                        'ChatText2.DocumentText = ssh.Chat

                        If ssh.RiskyDeal = True Then GamesWindow.WBRiskyChat.DocumentText = "<body style=""word-wrap:break-word;""><font face=""Cambria"" size=""3"" font color=""" &
                          TypeColor & """><b>" & TypeName & ": </b></font><font face=""" & TypeFont & """ size=""" & TypeSize & """ color=""" & TextColor & """>" & ssh.DomTask & "<br></font></body>"


                    Else


                        If FrmSettings.WebTeaseMode.Checked = True Then
                            'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & "</body><body style=""word-wrap:break-word;"">" & "<font face=""" & FrmSettings.FontComboBoxD.Text & """ size=""" & FrmSettings.NBFontSizeD.Value & """ color=""" &
                            'TextColor & """><center>" & ssh.DomTask & "</center><br></font></body>"
                        Else
                            'ssh.Chat = "<body style=""word-wrap:break-word;"">" & ssh.Chat & "<font face=""Cambria"" size=""3"" font color=""" &
                            'TypeColor & """><b>" & TypeName & ": </b></font><font face=""" & TypeFont & """ size=""" & TypeSize & """ color=""" & TextColor & """>" & ssh.DomTask & "<br></font></body>"
                        End If

                        ssh.TypeToggle = 0
                        'ChatText.DocumentText = ssh.Chat
                        'ChatText2.DocumentText = ssh.Chat

                        If ssh.RiskyDeal = True Then GamesWindow.WBRiskyChat.DocumentText = "<body style=""word-wrap:break-word;""><font face=""Cambria"" size=""3"" font color=""" &
                          TypeColor & """><b>" & TypeName & ": </b></font><font face=""" & TypeFont & """ size=""" & TypeSize & """ color=""" & TextColor & """>" & ssh.DomTask & "<br></font></body>"

                    End If

EndSysMes:



                    ScrollChatDown()

                    If FrmSettings.CBAutosaveChatlog.Checked = True Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\Autosave.html", ChatText.DocumentText, False)

                    ' Dsplay the next picture in the slideshow as the domme responds if "With Tease" radio button is checked



                    ssh.SubWroteLast = False

                End If

HypNoResponse:
NoResponse:
                Try
                    If BWimageFetcher.TriggerRequired AndAlso BWimageFetcher.WaitToFinish() Then
                        ' ################## Image already loading ####################
                        ' If Sync of results is activated, wait for the ImageFetcher to finish .
                        ' Do nothing else -> WaitToFinish has already displayed an image.

                    ElseIf ssh.RiskyDeal = True Then
                        ' ######################## Risky Pick #########################
                        GamesWindow.PBRiskyPic.Image = Image.FromFile(ContactToUse.NavigateNextTease)

                    ElseIf Not String.IsNullOrWhiteSpace(ssh.DommeImageSTR) Then
                        ' ######################## Domme Tags #########################
                        ShowImage(ssh.DommeImageSTR, True)

                    ElseIf ShowPicture = True AndAlso ContactToUse IsNot Nothing Then
                        ' ######################## Slideshow ##########################
                        ShowImage(ContactToUse.NavigateNextTease, True)

                    ElseIf ShowPicture = True Then
                        ' #################### Domme Slideshow ########################
DommeSlideshowFallback:
                        ShowImage(ssh.SlideshowMain.NavigateNextTease, True)
                    End If

                Catch ex As Exception When ContactToUse IsNot ssh.SlideshowMain
                    '@@@@@@@@@@@@@@ Exception - Try Fallback @@@@@@@@@@@@@@@@@@
                    ContactToUse = Nothing
                    Log.WriteError("Error occurred while displaying image. Performing Fallback.",
                                   ex, "Display Image")
                    GoTo DommeSlideshowFallback
                Catch ex As Exception
                    '@@@@@@@@@@@@@@@@@@@@@@@ Exception @@@@@@@@@@@@@@@@@@@@@@@@
                    Log.WriteError("Error occurred while displaying image. Fallback Failed.",
                                   ex, "Display Image")
                    ClearMainPictureBox()
                Finally
                    ssh.DommeImageSTR = ""
                    ssh.JustShowedBlogImage = False
                    ssh.JustShowedSlideshowImage = False
                    ShowPicture = False
                End Try




                If FrmSettings.TTSCheckBox.Checked = True _
                And TTSVoice <> "No voices installed" _
                And ssh.DomTask <> "" Then
                    ssh.DomTask = StripFormat(ssh.DomTask)

                    mciSendString("CLOSE Speech1", String.Empty, 0, 0)
                    mciSendString("CLOSE Echo1", String.Empty, 0, 0)

                    Dim SpeechDir As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Hypnotic Guide\TempWav.wav"

                    synth2.Volume = TTSvolume
                    synth2.Rate = TTSrate
                    synth2.SelectVoice(TTSVoice)
                    synth2.SetOutputToWaveFile(SpeechDir, New SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono))
                    synth2.Speak(ssh.DomTask)
                    synth2.SetOutputToNull()

                    SpeechDir = GetShortPathName(SpeechDir)

                    mciSendString("OPEN " & SpeechDir & " TYPE WAVEAUDIO ALIAS Speech1", String.Empty, 0, 0)
                    mciSendString("PLAY Speech1 FROM 0", String.Empty, 0, 0)



                    If CBHypnoGenPhase.Checked And ssh.HypnoGen = True Then
                        Delay(0.4)
                        mciSendString("OPEN " & SpeechDir & " TYPE WAVEAUDIO ALIAS Echo1", String.Empty, 0, 0)
                        mciSendString("PLAY Echo1 FROM 0", String.Empty, 0, 0)
                    End If

                End If



                If ssh.CorrectedTypo = True Then
                    ssh.CorrectedTypo = False
                    'DomTask = "*" & CorrectedWord
                    ssh.DomTask = LineSpeaker & "*" & ssh.CorrectedWord
                    Return
                End If

                StrokeSpeedCheck()

                If ssh.SubStroking = False Then
                    StrokePace = 0
                    If FrmSettings.TBWebStop.Text <> "" Then
                        Try
                            FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text)
                        Catch
                        End Try
                    End If
                End If

                If ssh.RLGLGame = True And ssh.RedLight = False Then
                    If (DomWMP.playState = WMPLib.WMPPlayState.wmppsPaused) Then
                        DomWMP.Ctlcontrols.play()


                        ssh.AskedToSpeedUp = False
                        ssh.AskedToSlowDown = False
                        ssh.SubStroking = True
                        ssh.SubEdging = False
                        ssh.SubHoldingEdge = False
                        StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
                        StrokePace = 50 * Math.Round(StrokePace / 50)
                        ssh.RLGLTauntTick = ssh.randomizer.Next(20, 31)
                        ' VideoTauntTick = randomizer.Next(20, 31)
                        RLGLTauntTimer.Start()

                    End If
                End If

                If ssh.RLGLGame = True And ssh.RedLight = True Then
                    If (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying) Then
                        DomWMP.Ctlcontrols.pause()
                        ssh.SubStroking = False
                        StrokePace = 0
                        'VideoTauntTimer.Stop()
                    End If
                End If



                ssh.NullResponse = False

                If ssh.FollowUp <> "" Then
                    ssh.DomTask = ssh.FollowUp
                    ssh.FollowUp = ""
                    Exit Sub
                End If

                ssh.DomTypeCheck = False
                ssh.DomTyping = False
                'StringLength = 20
                ssh.StringLength = ssh.randomizer.Next(8, 16)

                If ssh.SubHoldingEdge = True Then
                    StrokePace = 0
                End If
                'JustShowedBlogImage = False

                If ssh.TempScriptCount = 0 Then
                    ssh.JustShowedBlogImage = False
                    ssh.JustShowedSlideshowImage = False
                End If

                If ssh.CBTBallsActive Then
                    ssh.CBTBallsActive = False
                    CBTBalls()
                End If

                If ssh.CBTBothActive = True Then
                    ssh.CBTBothActive = False
                    CBTBoth()
                End If

                If ssh.CustomTaskActive = True Then
                    ssh.CustomTaskActive = False
                    RunCustomTask()
                End If

                If ssh.YesOrNo = False Then
                    If ssh.RapidCode = True Then
                        RunFileText()
                    Else
                        ssh.ScriptTick = ssh.randomizer.Next(4, 7)
                        If ssh.RapidFire = True Then ssh.ScriptTick = 1
                        If ssh.RiskyDeal = True Then ssh.ScriptTick = 2
                        ScriptTimer.Start()
                    End If
                End If

                If ssh.YesOrNo = True And ssh.RiskyDeal = True Then
                    GamesWindow.BTNPickIt.Visible = True
                    GamesWindow.BTNRiskIt.Visible = True
                    GamesWindow.HighlightCaseLabelsOffer()

                End If

                ssh.GotoFlag = False


                If ssh.SubGaveUp = True Then

                    ssh.SubGaveUp = False

                    ssh.AskedToGiveUpSection = False
                    If TnASlides.Enabled = True Then TnASlides.Stop()

                    Dim WasStroking As Boolean = ssh.SubStroking
                    Dim WasEdging As Boolean = ssh.SubEdging
                    Dim WasHolding As Boolean = ssh.SubHoldingEdge

                    StopEverything()
                    ssh.ModuleEnd = False
                    ssh.ShowModule = False

                    If ssh.ReturnFlag Then
                        ssh.ShowModule = True
                        ScriptTimer.Start()
                    ElseIf ssh.TeaseTick < 1 And ssh.Playlist = False Then
                        ssh.StrokeTauntVal = -1
                        RunLastScript()
                    ElseIf WasStroking And Not WasEdging And Not WasHolding Then
                        ssh.StrokeTauntVal = -1
                        RunModuleScript(False)
                    Else
                        ssh.StrokeTauntVal = -1
                        RunLinkScript()
                    End If



                End If

            End If
        End If

    End Sub

    Private Function IsSpeechModeOn() As Boolean
        Dim retval As Integer
        Dim returnData As String = Space(128)
        retval = mciSendString("status Speech1 mode", returnData, 128, 0)
        Return returnData.Substring(0, 7) = "playing"
    End Function

    Public Shared Function GetShortPathName(ByVal longPath As String) As String
        Const MaxPath As Int32 = 260
        Const SBStartSize As Int32 = MaxPath + 1
        Dim sb As New System.Text.StringBuilder(SBStartSize)
        Dim len As Int32 = GetShortPathName(longPath, sb, sb.Length - 1)
        Return sb.ToString()
    End Function

    <System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Ansi, EntryPoint:="GetShortPathNameA")>
    Public Shared Function GetShortPathName(ByVal lpszLongPath As String,
                                        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)> ByVal lpszShortPath As System.Text.StringBuilder,
                                        ByVal cchBuffer As Int32) As Int32
    End Function

    Private Async Sub SendTimer_Tick(sender As Object, e As EventArgs) Handles SendTimer.Tick
        SendTimer.Enabled = False
        Dim chatMessage As ChatMessage = myDommeMessages.Dequeue()
        chatMessage.Message = HashTagReplace(chatMessage.Message, mySession.Session.Domme)
        UpdateChatWindow(chatMessage)

        Dim nextMessage As ChatMessage = myDommeMessages.FirstOrDefault()
        If nextMessage IsNot Nothing Then
            SendTimer.Interval = GetTypingDelay(nextMessage, mySettingsAccessor.DoesDommeTypeInstantly)
            SendTimer.Enabled = True
        End If
    End Sub

    Private Sub SendTimer_Tick2(sender As Object, e As EventArgs) 'Handles SendTimer.Tick
        Dim domme As DommePersonality = mySession.Session.Domme
        Dim chatMessage = ssh.DomChat
        If chatMessage.Contains("@SlideshowOff") Then
            CustomSlideshowTimer.Stop()
        End If
        If chatMessage.Contains("@NullResponse") Then
            ssh.NullResponse = True
        Else
            ssh.RapidCode = False
        End If

        If Not ssh.Group.Contains("D") And Not chatMessage.Contains("@Contact1") And Not chatMessage.Contains("@Contact2") And Not chatMessage.Contains("@Contact3") Then
            Dim GroupList As New List(Of String)
            If ssh.Group.Contains("1") Then GroupList.Add(" @Contact1 ")
            If ssh.Group.Contains("2") Then GroupList.Add(" @Contact2 ")
            If ssh.Group.Contains("3") Then GroupList.Add(" @Contact3 ")
            chatMessage = chatMessage & GroupList(ssh.randomizer.Next(0, GroupList.Count))
        End If

        If ssh.NullResponse = True Then
            SendTimer.Stop()
            GoTo NullResponseLine
        End If

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        Dim ShowPicture As Boolean = False

        ' Let the program know that the domme is currently typing
        ssh.DomTypeCheck = True

        ' Toggle switch to let the program know when to display "Domme is typing..." and when to remove it and display what she wrote
        If Not ssh.TypeToggle Then
            If ssh.TypeDelay > 0 Then
                ssh.TypeDelay -= 1
            Else
                ' Stop the timer while we do stuff
                SendTimer.Stop()

                If ssh.RiskyDeal = True Then GamesWindow.LblRiskType.Visible = True
                ssh.IsTyping = True
                Dim TypingName As String = domName.Text
                If chatMessage.Contains("@Contact1") Then TypingName = My.Settings.Glitter1
                If chatMessage.Contains("@Contact2") Then TypingName = My.Settings.Glitter2
                If chatMessage.Contains("@Contact3") Then TypingName = My.Settings.Glitter3

                If chatMessage.Contains("@EmoteMessage") Then ssh.EmoMes = True

                If chatMessage.Contains("@SystemMessage") Then
                    ssh.SysMes = True
                    ssh.TypeDelay = 0
                    GoTo SkipIsTyping
                End If

SkipIsTyping:

                ssh.TypeToggle = 1
                ssh.StringLength = chatMessage.Length
                If ssh.DivideText = True Then
                    ssh.StringLength /= 3
                    ssh.DivideText = False
                End If
                If FrmSettings.TypeInstantlyCheckBox.Checked = True Or ssh.RapidCode = True Then ssh.StringLength = 0
            End If

        Else

            If ssh.TypeDelay > 0 Then
                ssh.TypeDelay -= 1
                If chatMessage.Contains("@SystemMessage") Then ssh.TypeDelay = 0
            Else
                ssh.TypeToggle = 0
                SendTimer.Stop()
                ssh.IsTyping = False

                ssh.ResponseYes = ""
                ssh.ResponseNo = ""

                If ssh.RiskyDeal = True Then GamesWindow.LblRiskType.Visible = False

NullResponseLine:
                '################## Display a Slideimage? #################
                'TODO: Optimize Code. Since images loaded by the Backgroundworker are prioritized, this section can be shrinked down.
                ssh.JustShowedBlogImage = GetJustShowedImage(chatMessage)

                If chatMessage.Contains("@SlideshowFirst") Then ssh.JustShowedSlideshowImage = True
                If chatMessage.Contains("@SlideshowNext") Then ssh.JustShowedSlideshowImage = True
                If chatMessage.Contains("@SlideshowPrevious") Then ssh.JustShowedSlideshowImage = True
                If chatMessage.Contains("@SlideshowLast") Then ssh.JustShowedSlideshowImage = True

                If ssh.GlitterTease = True And ssh.JustShowedBlogImage = False Then GoTo TryNextWithTease

                If FrmSettings.TeaseSlideShowRadio.Checked = True And ssh.JustShowedBlogImage = False And ssh.TeaseVideo = False And Not chatMessage.Contains("@NewBlogImage") And ssh.NullResponse = False _
                    And ssh.SlideshowLoaded = True And Not chatMessage.Contains("@ShowButtImage") And Not chatMessage.Contains("@ShowBoobsImage") And Not chatMessage.Contains("@ShowButtsImage") _
                    And Not chatMessage.Contains("@ShowBoobImage") And ssh.LockImage = False And ssh.CustomSlideEnabled = False And ssh.RapidFire = False _
                    And UCase(chatMessage) <> "<B>TEASE AI HAS BEEN RESET</B>" And ssh.JustShowedSlideshowImage = False Then
                    If ssh.SubStroking = False Or ssh.SubEdging = True Or ssh.SubHoldingEdge = True Then
TryNextWithTease:
                    End If

                    ShowPicture = True
                End If


                If chatMessage.Contains("@WritingTask(") Then
                    Dim WriteFlag As String = GetParentheses(chatMessage, "@WritingTask(")
                    chatMessage = chatMessage.Replace(WriteFlag, PoundClean(WriteFlag))
                End If

                If chatMessage.Contains("@Contact1") Or chatMessage.Contains("@Contact2") Or chatMessage.Contains("@Contact3") Then ssh.SubWroteLast = True

                ContactToUse = GetContactToUse(chatMessage, ssh)

                Dim TypeName As String = ContactToUse.TypeName
                Dim TypeColor As String = ContactToUse.TypeColorHtml
                Dim TypeFont As String = ContactToUse.TypeFont
                Dim TypeSize As String = ContactToUse.TypeSize

                Dim TTSVoice As String = FrmSettings.TTSComboBox.Text
                Dim TTSrate As Integer = ContactToUse.TTSrate
                Dim TTSvolume As String = ContactToUse.TTSvolume



                If FrmSettings.TTSCheckBox.Checked = True And TTSVoice <> "No voices installed" Then
                    Dim EmoteArray() As String = Split(chatMessage)
                    For i As Integer = 0 To EmoteArray.Length - 1
                        Try
                            If EmoteArray(i).Contains("#") And LCase(EmoteArray(i)).Contains("emote") Then
                                EmoteArray(i) = EmoteArray(i).Replace(EmoteArray(i), "")
                            End If
                        Catch
                        End Try
                    Next
                    chatMessage = Join(EmoteArray)
                End If


                Dim LoopBuffer As Integer = 0

                Do

                    LoopBuffer += 1

                    chatMessage = chatMessage.Replace("#Null", "")
                    chatMessage = HashTagReplace(chatMessage, domme)
                    chatMessage = PerformCommands(chatMessage, False)

                    chatMessage = StripCommands(chatMessage)
                    chatMessage = chatMessage.Replace("#Null", "")
                    chatMessage = PoundClean(chatMessage)

                    If LoopBuffer > 4 Then Exit Do

                Loop Until Not chatMessage.Contains("#") And Not chatMessage.Contains("@")

                If ssh.SysMes = False And ssh.EmoMes = False Then

                    Try
                        Dim UCASELine As String = UCase(chatMessage.Substring(0, 1))
                        chatMessage = chatMessage.Remove(0, 1).Insert(0, UCASELine)
                    Catch
                    End Try

                    If FrmSettings.LCaseCheckBox.Checked = True Then chatMessage = LCase(chatMessage)
                    If FrmSettings.CBMeMyMine.Checked = True Then
                        Dim MeArray() As String = Split(chatMessage)
                        For i As Integer = MeArray.Length - 1 To 0 Step -1
                            If UCase(MeArray(i)) = "ME" Then MeArray(i) = "Me"
                            If UCase(MeArray(i)) = "MY" Then MeArray(i) = "My"
                            If UCase(MeArray(i)) = "MINE" Then MeArray(i) = "Mine"
                            If UCase(MeArray(i)) = "I" Then MeArray(i) = "I"
                            If UCase(MeArray(i)) = "I'D" Then MeArray(i) = "I'd"
                            If UCase(MeArray(i)) = "I'M" Then MeArray(i) = "I'm"
                            If UCase(MeArray(i)) = "I'LL" Then MeArray(i) = "I'll"
                            If UCase(MeArray(i)) = "YOU" Then MeArray(i) = "you"
                            If UCase(MeArray(i)) = "YOUR" Then MeArray(i) = "your"
                            If UCase(MeArray(i)) = "YOURS" Then MeArray(i) = "yours"
                            If UCase(MeArray(i)) = "YOU'RE" Then MeArray(i) = "you're"
                            If UCase(MeArray(i)) = "YOU'D" Then MeArray(i) = "you'd"
                            If UCase(MeArray(i)) = "YOU'LL" Then MeArray(i) = "you'll"
                        Next
                        chatMessage = Join(MeArray)
                    End If
                    If FrmSettings.apostropheCheckBox.Checked = True Then chatMessage = chatMessage.Replace("'", "")
                    If FrmSettings.commaCheckBox.Checked = True Then chatMessage = chatMessage.Replace(",", "")
                    If FrmSettings.periodCheckBox.Checked = True Then chatMessage = chatMessage.Replace(".", "")

                    Dim EmoToggle As Boolean = True
                    For i As Integer = chatMessage.Length - 1 To 0 Step -1
                        If chatMessage.Substring(i, 1) = "*" Then
                            If EmoToggle = False Then
                                EmoToggle = True
                                chatMessage = chatMessage.Remove(i, 1).Insert(i, FrmSettings.TBEmote.Text)
                            Else
                                EmoToggle = False
                                chatMessage = chatMessage.Remove(i, 1).Insert(i, FrmSettings.TBEmoteEnd.Text)
                            End If
                        End If
                    Next

                    chatMessage = chatMessage.Replace(":d", ":D")
                    chatMessage = chatMessage.Replace(": d", ": D")

                End If

                chatMessage = chatMessage.Replace("ATSYMBOL", "@")
                chatMessage = chatMessage.Replace("atsymbol", "@")

                If ssh.InputIcon = True Then
                    chatMessage = chatMessage & " <img src=""file://" & Application.StartupPath & "/Images/System/input.png""/>"
                    ssh.InputIcon = False
                End If
                chatMessage = SendTimerTickReplaceWords(chatMessage)

                'SUGGESTION: (Stefaf) All Writing to the Chatbox and Wating for fetched Images shoud be in a separat Function. 

                If ssh.NullResponse = True Or chatMessage = "" Or chatMessage Is Nothing Then GoTo NullResponseLine2

                If UCase(chatMessage) = "<B>TEASE AI HAS BEEN RESET</B>" Then chatMessage = "<b>Tease AI has been reset</b>"

                Dim TextColor As String = Color2Html(My.Settings.ChatTextColor)

                If ssh.SysMes = True Then
                    'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & chatMessage & "</b><br></font></body>"
                    'ssh.SysMes = False
                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat
                    GoTo EndSysMes
                End If

                If ssh.EmoMes = True Then
                    'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""" &
                    'TypeColor & """><b><i>" & chatMessage & "</i></b><br></font></body>"
                    'ssh.EmoMes = False
                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat
                    GoTo EndSysMes
                End If

                ' Add timestamps to domme response if the option is checked in the menu
                If FrmSettings.TimeStampCheckBox.Checked = True And FrmSettings.WebTeaseMode.Checked = False Then
                    'ssh.Chat = ssh.Chat & "<font face=""Cambria"" size=""2"" color=""DimGray"">" & (Date.Now.ToString("hh:mm tt ")) & "</font>"
                End If


                If ssh.SubWroteLast = False And FrmSettings.ShowNamesCheckBox.Checked = False Then


                    If FrmSettings.WebTeaseMode.Checked = True Then
                        'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & "</body><body style=""word-wrap:break-word;"">" & "<font face=""" & FrmSettings.FontComboBoxD.Text & """ size=""" & FrmSettings.NBFontSizeD.Value & """ color=""" &
                        'TextColor & """><center>" & chatMessage & "</center><br></font></body>"
                    Else
                        'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & FrmSettings.FontComboBoxD.Text & """ size=""" & FrmSettings.NBFontSizeD.Value & """ color=""" &
                        'TextColor & """>" & ssh.Chat & chatMessage & "<br></font></body>"
                    End If


                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat

                    If ssh.RiskyDeal = True Then GamesWindow.WBRiskyChat.DocumentText = "<body style=""word-wrap:break-word;""><font face=""Cambria"" size=""3"" font color=""" &
              TypeColor & """><b>" & TypeName & ": </b></font><font face=""" & TypeFont & """ size=""" & TypeSize & """ color=""" & TextColor & """>" & chatMessage & "<br></font></body>"

                Else

                    If FrmSettings.WebTeaseMode.Checked = True Then
                        'ssh.Chat = "<body bgcolor=""" & Color2Html(My.Settings.ChatWindowColor) & """>" & "</body><body style=""word-wrap:break-word;"">" & "<font face=""" & FrmSettings.FontComboBoxD.Text & """ size=""" & FrmSettings.NBFontSizeD.Value & """ color=""" &
                        'TextColor & """><center>" & chatMessage & "</center><br></font></body>"
                    Else
                        'ssh.Chat = "<body style=""word-wrap:break-word;"">" & ssh.Chat & "<font face=""Cambria"" size=""3"" font color=""" &
                        'TypeColor & """><b>" & TypeName & ": </b></font><font face=""" & TypeFont & """ size=""" & TypeSize & """ color=""" & TextColor & """>" & chatMessage & "<br></font></body>"
                    End If

                    ssh.TypeToggle = 0
                    'ChatText.DocumentText = ssh.Chat
                    'ChatText2.DocumentText = ssh.Chat

                    If ssh.RiskyDeal = True Then GamesWindow.WBRiskyChat.DocumentText = "<body style=""word-wrap:break-word;""><font face=""Cambria"" size=""3"" font color=""" &
              TypeColor & """><b>" & TypeName & ": </b></font><font face=""" & TypeFont & """ size=""" & TypeSize & """ color=""" & TextColor & """>" & chatMessage & "<br></font></body>"

                End If

EndSysMes:



                ScrollChatDown()


                If FrmSettings.CBAutosaveChatlog.Checked = True Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\Autosave.html", ChatText.DocumentText, False)

                ssh.SubWroteLast = False

NullResponseLine2:

                Try
                    If BWimageFetcher.TriggerRequired AndAlso BWimageFetcher.WaitToFinish() Then
                        ' ################## Image already loading ####################
                        ' If Sync of results is activated, wait for the ImageFetcher to finish .
                        ' Do nothing else -> WaitToFinish has already displayed an image.

                    ElseIf ssh.RiskyDeal = True Then
                        ' ######################## Risky Pick #########################
                        GamesWindow.PBRiskyPic.Image = Image.FromFile(ContactToUse.NavigateNextTease)

                    ElseIf Not String.IsNullOrWhiteSpace(ssh.DommeImageSTR) Then
                        ' ######################## Domme Tags #########################
                        ShowImage(ssh.DommeImageSTR, True)

                    ElseIf ShowPicture = True AndAlso ContactToUse IsNot Nothing Then
                        ' ################### Variable Slideshow ######################
                        ShowImage(ContactToUse.NavigateNextTease, True)

                    ElseIf ShowPicture = True Then
                        ' #################### Domme Slideshow ########################
DommeSlideshowFallback:
                        ShowImage(ssh.SlideshowMain.NavigateNextTease, True)
                    End If

                Catch ex As Exception When ContactToUse IsNot ssh.SlideshowMain
                    '@@@@@@@@@@@@@@ Exception - Try Fallback @@@@@@@@@@@@@@@@@@
                    ContactToUse = Nothing
                    Log.WriteError("Error occurred while displaying image. Performing Fallback.",
                                    ex, "Display Image")
                    GoTo DommeSlideshowFallback
                Catch ex As Exception
                    '@@@@@@@@@@@@@@@@@@@@@@@ Exception @@@@@@@@@@@@@@@@@@@@@@@@
                    Log.WriteError("Error occurred while displaying image. Fallback Failed.",
                                    ex, "Display Image")
                    ClearMainPictureBox()
                Finally
                    ssh.DommeImageSTR = ""
                    ssh.JustShowedBlogImage = False
                    ssh.JustShowedSlideshowImage = False
                    ShowPicture = False
                End Try

                If FrmSettings.TTSCheckBox.Checked = True _
                And TTSVoice <> "No voices installed" _
                And chatMessage <> "" Then
                    chatMessage = StripFormat(chatMessage)
                    synth.Volume = TTSvolume
                    synth.Rate = TTSrate
                    synth.SelectVoice(TTSVoice)
                    synth.Speak(chatMessage)
                End If



                If ssh.MultipleEdgesMetronome = "STOP" Then
                    ssh.MultipleEdgesMetronome = ""
                    StrokePace = 0
                    ssh.SubStroking = False
                    ssh.SubEdging = False
                    DeactivateWebToy()
                End If

                If ssh.MultipleEdgesMetronome = "START" Then
                    ssh.MultipleEdgesMetronome = ""
                    EdgePace()
                    ssh.SubStroking = True
                    ssh.SubEdging = True
                    ActivateWebToy()
                    DisableContactStroke()
                End If

                StrokeSpeedCheck()

                If ssh.SubStroking = False Then
                    StrokePace = 0
                    If FrmSettings.TBWebStop.Text <> "" Then
                        Try
                            FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text)
                        Catch
                        End Try
                    End If
                End If

                If ssh.SubHoldingEdge = True Then
                    StrokePace = 0
                End If

                ssh.NullResponse = False

                If ssh.FollowUp <> "" Then
                    chatMessage = ssh.FollowUp
                    ssh.FollowUp = ""
                    Exit Sub
                End If

                ssh.DomTypeCheck = False
                'StringLength = 20
                ssh.StringLength = ssh.randomizer.Next(8, 16)

                If ssh.TempScriptCount = 0 Then
                    ssh.JustShowedBlogImage = False
                    ssh.JustShowedSlideshowImage = False
                End If

                If ssh.CBTBallsActive = True Then ssh.CBTBallsActive = False
                If ssh.CBTBothActive = True Then ssh.CBTBothActive = False

                If ssh.CBTCockFlag = True Or ssh.CBTBallsFlag = True Or ssh.CBTBothFlag = True Or ssh.CustomTask = True Then
                    ssh.TasksCount -= 1
                    If ssh.TasksCount < 1 Then
                        ssh.CBTCockFlag = False
                        ssh.CBTBallsFlag = False
                        ssh.CBTBothFlag = False
                        ssh.CustomTask = False
                        ssh.CBTBallsFirst = True
                        ssh.CBTCockFirst = True
                        ssh.CBTBothFirst = True
                        ssh.CustomTaskFirst = True
                    End If
                End If

                If ssh.CBTBallsFlag = True Then
                    CBTBalls()
                End If

                If ssh.CBTBothFlag = True Then
                    CBTBoth()
                End If

                If ssh.CustomTask = True Then
                    RunCustomTask()
                End If

                If ssh.YesOrNo = False And ssh.Responding = False Then
                    ssh.ScriptTick = ssh.randomizer.Next(4, 7)
                    If ssh.RiskyDeal = True Then ssh.ScriptTick = 2
                    ScriptTimer.Start()
                End If

                ssh.Responding = False

                If ssh.SubGaveUp = True Then

                    ssh.SubGaveUp = False

                    ssh.AskedToGiveUpSection = False
                    If TnASlides.Enabled = True Then TnASlides.Stop()

                    Dim WasStroking As Boolean = ssh.SubStroking
                    Dim WasEdging As Boolean = ssh.SubEdging
                    Dim WasHolding As Boolean = ssh.SubHoldingEdge

                    StopEverything()
                    ssh.ModuleEnd = False
                    ssh.ShowModule = False

                    ssh.LastScriptCountdown -= 1

                    If ssh.ReturnFlag Then
                        ssh.ShowModule = True
                        ScriptTimer.Start()
                    ElseIf ssh.TeaseTick < 1 And ssh.Playlist = False Then
                        ssh.StrokeTauntVal = -1
                        RunLastScript()
                    ElseIf WasStroking And Not WasEdging And Not WasHolding Then
                        ssh.StrokeTauntVal = -1
                        RunModuleScript(False)
                    Else
                        ssh.StrokeTauntVal = -1
                        RunLinkScript()
                    End If

                End If
            End If
        End If

    End Sub

    Private Function GetContactToUse(chatMessage As String, ssh As SessionState) As ContactData

        If chatMessage.Contains("@Contact1") Then
            Return ssh.SlideshowContact1
        ElseIf chatMessage.Contains("@Contact2") Then
            Return ssh.SlideshowContact2
        ElseIf chatMessage.Contains("@Contact3") Then
            Return ssh.SlideshowContact3
        End If
        Return ssh.SlideshowMain
    End Function

    Private Function GetJustShowedImage(chatMessage As String) As Boolean
        Return chatMessage.Contains("@ShowHardcoreImage") OrElse
            chatMessage.Contains("@ShowSoftcoreImage") OrElse
            chatMessage.Contains("@ShowLesbianImage") OrElse
            chatMessage.Contains("@ShowBlowjobImage") OrElse
            chatMessage.Contains("@ShowFemdomImage") OrElse
            chatMessage.Contains("@ShowLezdomImage") OrElse
            chatMessage.Contains("@ShowHentaiImage") OrElse
            chatMessage.Contains("@ShowGayImage") OrElse
            chatMessage.Contains("@ShowMaledomImage") OrElse
            chatMessage.Contains("@ShowCaptionsImage") OrElse
            chatMessage.Contains("@ShowGeneralImage") OrElse
            chatMessage.Contains("@ShowImage") OrElse
            chatMessage.Contains("@ShowLocalImage") OrElse
            chatMessage.Contains("@ShowBlogImage") OrElse
            chatMessage.Contains("@NewBlogImage")
    End Function

    Private Function SendTimerTickReplaceWords(chatMessage As String) As String
        chatMessage = chatMessage.Replace(" a a", " an a")
        chatMessage = chatMessage.Replace(" a e", " an e")
        chatMessage = chatMessage.Replace(" a i", " an i")
        chatMessage = chatMessage.Replace(" a o", " an o")
        chatMessage = chatMessage.Replace(" a u", " an u")

        chatMessage = chatMessage.Replace(" an uni", " a uni")
        chatMessage = chatMessage.Replace(" an utensil", " a utensil")
        chatMessage = chatMessage.Replace(" an ukulele", " a ukulele")
        chatMessage = chatMessage.Replace(" an use", " a use")
        chatMessage = chatMessage.Replace(" an urethra", " a urethra")
        chatMessage = chatMessage.Replace(" an urine", " a urine")
        chatMessage = chatMessage.Replace(" an usual", " a usual")
        chatMessage = chatMessage.Replace(" an utility", " a utility")
        chatMessage = chatMessage.Replace(" an uterus", " a uterus")
        chatMessage = chatMessage.Replace(" an utopia", " a utopia")
        Return chatMessage
    End Function

#Region "------------------------------------------ Images ----------------------------------------------"
    Private Sub LoadCustomizedSlideshow(sender As Object, e As EventArgs) Handles BrowseFolderButton.Click, ImageFolderComboBox.KeyDown, ImageFolderComboBox.SelectedIndexChanged
        If sender Is ImageFolderComboBox AndAlso TypeOf e Is KeyEventArgs Then
            Dim keyEventArgs As KeyEventArgs = DirectCast(e, KeyEventArgs)
            If keyEventArgs.KeyCode <> Keys.Enter Then
                Return
            End If
            keyEventArgs.Handled = True
        End If

        'TODO-Next-Stefaf: Implement enhanced RecentSlideshows.Item handling
        If FrmSettings.CBSettingsPause.Checked AndAlso FrmSettings.SettingsPanel.Visible Then
            MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu is Visible"" option first!", , "Warning!")
            Return
        End If
        Try
            BrowseFolderButton.Enabled = False
            ImageSlideShowNextButton.Enabled = False
            ImageSlideShowPreviousButton.Enabled = False
            PicStripTSMIdommeSlideshow.Enabled = False
            EnableSlideShowControls(False)
            Dim folderToLoad As String = GetImageLocation(sender Is BrowseFolderButton, sender Is ImageFolderComboBox)
            'TODO-Next: Move ImageNavigation-Lock to BWImageSync
            If sender Is BrowseFolderButton Then
                My.Settings.RecentSlideshows.Add(folderToLoad)
                Do Until My.Settings.RecentSlideshows.Count < 11
                    My.Settings.RecentSlideshows.Remove(My.Settings.RecentSlideshows(0))
                Loop

                ImageFolderComboBox.Items.Clear()

                For Each comboitem As String In My.Settings.RecentSlideshows
                    ImageFolderComboBox.Items.Add(comboitem)
                Next

                ImageFolderComboBox.Text = folderToLoad
            ElseIf sender Is ImageFolderComboBox Then
                If TypeOf e Is KeyEventArgs Then
                    Dim keyEventArgs As KeyEventArgs = DirectCast(e, KeyEventArgs)
                    If keyEventArgs.KeyCode <> Keys.Enter Then
                        Return
                    End If
                    keyEventArgs.Handled = True
                End If
            End If

            If String.IsNullOrWhiteSpace(folderToLoad) AndAlso Not IsUrl(folderToLoad) AndAlso Not Directory.Exists(folderToLoad) Then
                ImageFolderComboBox.Text = folderToLoad + "is not a valid directory or URL"
                Return
            End If

            ssh.SlideshowMain.ImageList = GetImageList(folderToLoad)
            FrmSettings.TimedSlideShowRadio.Enabled = True
            FrmSettings.TeaseSlideShowRadio.Enabled = True
            DomWMP.Visible = False
            DomWMP.Ctlcontrols.pause()
            mainPictureBox.Visible = True
            ssh.SlideshowLoaded = False
            ssh.SlideshowMain.Index = 0

            If Not IsUrl(folderToLoad) Then
                ImageFolderComboBox.Enabled = False
            End If

            If Not ssh.SlideshowMain.ImageList.Any() Then
                MessageBox.Show(Me, folderToLoad + " doesn't contain any images.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If
            ssh.SlideshowLoaded = True

            ShowImage(ssh.SlideshowMain.CurrentImage, True)
            ssh.JustShowedBlogImage = False

            If FrmSettings.TimedSlideShowRadio.Checked Then
                ssh.SlideshowTimerTick = FrmSettings.SlideShowNumBox.Value
                SlideshowTimer.Start()
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to load custom slideshow : " & vbCrLf & vbCrLf & ex.Message, "Open CustomSlideshow failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            EnableSlideShowControls(True)
        End Try
    End Sub

    Private Function CreateImageSlideShow(slideShow As ContactData) As ImageSlideShow
        Return New ImageSlideShow With {
            .IsRandom = My.Settings.CBSlideshowRandom,
            .ImageList = slideShow.ImageList,
            .ImageIndex = slideShow.Index
        }
    End Function

    Private Function UpdateFromSlideShow(slideshow As ImageSlideShow) As Result(Of ContactData)
        My.Settings.CBSlideshowRandom = slideshow.IsRandom
        ssh.SlideshowMain.ImageList = slideshow.ImageList
        ssh.SlideshowMain.Index = slideshow.ImageIndex
        Return Result.Ok(ssh.SlideshowMain)
    End Function

    Private Sub EnableSlideShowControls(isEnabled As Boolean)
        BrowseFolderButton.Enabled = isEnabled
        ImageFolderComboBox.Enabled = isEnabled
        ImageSlideShowNextButton.Enabled = isEnabled
        ImageSlideShowPreviousButton.Enabled = isEnabled
        PicStripTSMIdommeSlideshow.Enabled = isEnabled
    End Sub

    Private Sub SlideShowNavigation_Click(sender As Object, e As EventArgs) Handles ImageSlideShowNextButton.Click, ImageSlideShowPreviousButton.Click
        Try
            EnableSlideShowControls(False)
            Dim imageSlideShow As ImageSlideShow = CreateImageSlideShow(ssh.SlideshowMain)
            If My.Settings.CBSettingsPause AndAlso FrmSettings.SettingsPanel.Visible Then
                MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu is Visible"" option first!", , "Warning!")
                Exit Sub
            End If
            If Not ssh.SlideshowLoaded OrElse ssh.TeaseVideo Then Return

            Dim newSlideShow As Result = mySlideShowNavigationService.MoveSlideShow(imageSlideShow, sender Is ImageSlideShowNextButton) _
                .OnSuccess(Function(iss) UpdateFromSlideShow(iss)) _
                .Ensure(Function(ssm) File.Exists(ssm.CurrentImage) OrElse IsUrl(ssm.CurrentImage), ssh.SlideshowMain.CurrentImage + " is not found and not a URL") _
                .OnSuccess(Sub()
                               ShowImage(ssh.SlideshowMain.CurrentImage, True)
                               ssh.JustShowedBlogImage = False
                           End Sub) _
                .Map()
            If newSlideShow.IsFailure Then
                ClearMainPictureBox()
                MessageBox.Show(newSlideShow.Error.Message)
            End If
            ' This is kept for reference
            'Dim sh As ContactData = ssh.SlideshowMain
            'If My.Settings.CBSlideshowRandom Then
            '    sh.NavigateNextTease()
            'ElseIf sender Is ImageSlideShowNextButton Then
            '    sh.NavigateForward()
            'ElseIf sender Is ImageSlideShowPreviousButton Then
            '    sh.NavigateBackward()
            'Else
            '    Throw New NotImplementedException("Action for button not implemented.")
            'End If
        Finally
            EnableSlideShowControls(True)
        End Try
    End Sub

    Private Sub ImageFolderComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles ImageFolderComboBox.MouseWheel
        Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        mwe.Handled = True
    End Sub

#End Region ' Images

#Region " VLC "

    Private Sub BTNLoadVideo_Click(sender As Object, e As EventArgs) Handles BTNLoadVideo.Click
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then
            MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu is Visible"" option first!", , "Warning!")
            Return
        End If

        If (OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK) Then



            DomWMP.Visible = True
            DomWMP.stretchToFit = True

            ' domVLC.Visible = True
            'SlideshowLoaded = False

            'programsettingsPanel.Visible = False
            mainPictureBox.Visible = False

            ' domVLC.playlist.items.clear()
            ' domVLC.playlist.add("file:///" & OpenFileDialog2.FileName & "")
            ' domVLC.video.crop = domVLC.Width & ":" & domVLC.Height
            ' domVLC.playlist.play()
            ' If FrmSettings.VLCfillRadio.Checked = True Then
            'domVLC.video.crop = domVLC.Width & ":" & domVLC.Height
            'End If
            'If FrmSettings.VLC43Radio.Checked = True Then domVLC.video.crop = "4:3"
            'If FrmSettings.VLC1610Radio.Checked = True Then domVLC.video.crop = "16:10"
            ' If FrmSettings.VLC169Radio.Checked = True Then domVLC.video.crop = "16:9"

            DomWMP.URL = OpenFileDialog2.FileName

        End If
    End Sub

    Private Sub BTNVideoControls_Click(sender As Object, e As EventArgs) Handles BTNVideoControls.Click

        If DomWMP.Height = SplitContainer1.Panel1.Height Then
            DomWMP.Height = SplitContainer1.Panel1.Height + 60
            BTNVideoControls.Text = "Show Video Controls"
        Else
            DomWMP.Height = SplitContainer1.Panel1.Height
            BTNVideoControls.Text = "Hide Video Controls"
        End If

        DomWMP.stretchToFit = True

    End Sub

#End Region

    Private Sub StrokeTimer_Tick(sender As Object, e As EventArgs) Handles StrokeTimer.Tick
        If ssh.InputFlag = True Then Return
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return
        If ssh.DomTypeCheck = True And ssh.StrokeTick < 5 Then Return
        If chatBox.Text <> "" And ssh.StrokeTick < 5 Then Return
        If ChatBox2.Text <> "" And ssh.StrokeTick < 5 Then Return
        If ssh.MiniScript = True And ssh.StrokeTick < 5 Then Return
        If ssh.FollowUp <> "" And ssh.StrokeTick < 5 Then Return


        If FrmSettings.CBDebugTauntsEndless.Checked = True And ssh.StrokeTick < 5 Then Return

        ssh.StrokeTick -= 1
        FrmSettings.LBLCycleDebugCountdown.Text = ssh.StrokeTick

        FrmSettings.LBLDebugStrokeTime.Text = ssh.StrokeTick

        If ssh.StrokeTick < 4 And ssh.TempScriptCount > 0 Then ssh.StrokeTick += 1

        If ssh.StrokeTick < 1 Then

            ssh.FirstRound = False

            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()

            If ssh.RunningScript = True Then
                ssh.ScriptTick = 3
                ScriptTimer.Start()
            Else

                RunModuleScript(False)

            End If


        End If
    End Sub

    Private Sub StrokeTauntTimer_Tick(sender As Object, e As EventArgs) Handles StrokeTauntTimer.Tick

        If ssh.MiniScript = True Then Return
        If ssh.InputFlag = True Then Return

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.StrokeTauntTick < 6 Then Return
        If chatBox.Text <> "" And ssh.StrokeTauntTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.StrokeTauntTick < 6 Then Return








        ssh.StrokeTauntTick -= 1

        FrmSettings.LBLDebugStrokeTauntTime.Text = ssh.StrokeTauntTick

        If ssh.StrokeTauntTick = 0 Then

            ' TauntText = Application.StartupPath & "\Scripts\" & dompersonalityComboBox.Text & "\StrokeTaunts.txt"

            If ssh.TempScriptCount = 0 Then
                'BlankLineLoop:

                Dim TauntFile As String
                TauntFile = "StrokeTaunts"
                If My.Settings.Chastity = True Then TauntFile = "ChastityTaunts"
                If ssh.GlitterTease = True Then TauntFile = "GlitterTaunts"
                ' ### Debug
                'TauntFile = "StrokeTaunts"

                ssh.TauntTextCount = 0
                ssh.ScriptCount = 0
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\", FileIO.SearchOption.SearchTopLevelOnly, TauntFile & "_*.txt")
                    ssh.ScriptCount += 1
                Next

                'Dim LinScript As Integer
                ' LinSelected = False

                'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Stroke\Linear Taunts", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                'LinScript += 1
                'Next

                Dim TauntTempVal As Integer = ssh.randomizer.Next(1, 101)

                'If LinScript = 0 Then

                If TauntTempVal < 45 Then
                    TauntTempVal = 1
                Else
                    TauntTempVal = ssh.randomizer.Next(1, ssh.ScriptCount + 1)
                End If

                If FrmSettings.CBDebugTaunts.Checked = True Then
                    If FrmSettings.RBDebugTaunts1.Checked = True Then TauntTempVal = 1
                    If FrmSettings.RBDebugTaunts2.Checked = True Then TauntTempVal = 2
                    If FrmSettings.RBDebugTaunts3.Checked = True Then TauntTempVal = 3
                End If


                'Else

                'If TauntTempVal < 11 Then
                'LinSelected = True
                'End If

                'If TauntTempVal > 10 And TauntTempVal < 51 Then

                '### Debug - Why was this here? O.o
                'TauntTempVal = 1  <--- Why?



                'End If

                'If TauntTempVal > 50 Then
                'TauntTempVal = randomizer.Next(1, ScriptCount + 1)
                'End If


                ' End If

                '### Debug
                'TauntTempVal = 3

                ' If LinSelected = False Then
                ssh.StrokeTauntCount = TauntTempVal
                ssh.ScriptCount = 0
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\", FileIO.SearchOption.SearchTopLevelOnly, TauntFile & "_*.txt")
                    ssh.ScriptCount += 1
                    If TauntTempVal = ssh.ScriptCount Then ssh.TauntText = foundFile
                Next
                ssh.ScriptCount = TauntTempVal
                'End If

            End If

            If ssh.TempScriptCount = 0 Then 'And LinSelected = False Then

                ' Uneseccary for txt2List creates a new List(of ) instance.
                ssh.TauntLines.Clear()
                ' Read all lines of given File.
                ssh.TauntLines = Txt2List(ssh.TauntText)
                ssh.TauntTextTotal = ssh.TauntLines.Count

                ssh.TauntTextTotal -= 1

                ssh.StrokeFilter = True



                Try
                    ssh.TauntLines = FilterList(ssh.TauntLines)
                    Dim g As String = "BreakPoint"
                Catch ex As Exception
                    Log.WriteError("Tease AI did not return a valid Taunt from file: " &
                                   ssh.TauntText, ex, "StrokeTauntTimer.Tick")
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Taunt"
                End Try

                ssh.StrokeFilter = False

                ssh.TauntTextTotal = ssh.TauntLines.Count

            End If




            '##############################################################################################################



            If ssh.TempScriptCount = 0 Then ' And LinSelected = False Then
                ssh.TempScriptCount = ssh.ScriptCount
                ssh.TauntTextTotal /= ssh.ScriptCount
                ssh.TauntTextCount = ssh.randomizer.Next(0, ssh.TauntTextTotal) * ssh.ScriptCount
                If FrmSettings.CBDebugTaunts.Checked = True Then ssh.TauntTextCount = 0
            Else
                ssh.TauntTextCount += 1
            End If

            ' If TempScriptCount = 0 And LinSelected = True Then
            'Dim LinList As New List(Of String)

            '            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Stroke\Linear Taunts", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
            'LinList.Add(foundFile)
            'Next

            'FileText = LinList(randomizer.Next(0, LinList.Count))

            'LinList.Clear()

            'LinList = Txt2List(FileText)

            'TempScriptCount = LinList.Count
            'LinLine = TempScriptCount


            'End If

            ssh.TempScriptCount -= 1

            Try
                ssh.DomTask = ssh.TauntLines(ssh.TauntTextCount)
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Taunt from file: " &
                                   ssh.TauntText, ex, "StrokeTauntTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Taunt"
            End Try


            If FrmSettings.CBDebugTaunts.Checked = True Then
                ssh.DomTask = ""
                If ssh.TauntTextCount = 0 Then ssh.DomTask = FrmSettings.TBDebugTaunts1.Text
                If ssh.TauntTextCount = 1 Then ssh.DomTask = FrmSettings.TBDebugTaunts2.Text
                If ssh.TauntTextCount = 2 Then ssh.DomTask = FrmSettings.TBDebugTaunts3.Text
                If ssh.DomTask = "" Then ssh.DomTask = "@SystemMessage ERROR: Debug field is currently blank"
            End If

            If ssh.DomTask.Contains("@ShowTaggedImage") Then ssh.JustShowedBlogImage = True

            'If DomTask = "" Then GoTo BlankLineLoop

            If InStr(UCase(ssh.DomTask), UCase("@CBT")) <> 0 Then
                CBTScript()
            Else
            End If



            If ssh.TempScriptCount = 0 Then
                If FrmSettings.SliderSTF.Value = 1 Then ssh.StrokeTauntTick = ssh.randomizer.Next(120, 241)
                If FrmSettings.SliderSTF.Value = 2 Then ssh.StrokeTauntTick = ssh.randomizer.Next(75, 121)
                If FrmSettings.SliderSTF.Value = 3 Then ssh.StrokeTauntTick = ssh.randomizer.Next(45, 76)
                If FrmSettings.SliderSTF.Value = 4 Then ssh.StrokeTauntTick = ssh.randomizer.Next(25, 46)
                If FrmSettings.SliderSTF.Value = 5 Then ssh.StrokeTauntTick = ssh.randomizer.Next(15, 26)
                'StrokeTauntTick = randomizer.Next(11, 21)
            Else
                ssh.StrokeTauntTick = ssh.randomizer.Next(5, 9)
            End If






        End If





    End Sub

    Public Sub CBTScript()

        Dim CBTAmount As Integer

        ssh.CBT = True
        ssh.YesOrNo = True
        Dim CBTCount As Integer

        Dim lines As List(Of String) = Txt2List(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\CBT\CBT.txt")
        CBTCount += lines.Count

        CBTCount = ssh.randomizer.Next(0, CBTCount)

        ssh.DomTask = lines(CBTCount)

        CBTAmount = ssh.randomizer.Next(1, 6) * 2 * FrmSettings.DominationLevel.Value
        ssh.DomTask = ssh.DomTask.Replace("#CBTAmount", CBTAmount)
    End Sub

#Region "----------------------------------------------------- Video-Files ----------------------------------------------------"
    Public Sub StartRandomVideo()
        ' Reset retentive global variables
        ssh.DommeVideo = False
        Dim getVideoMetaData As Result(Of List(Of VideoMetaData)) = New VideoAccessor().GetVideoData(Nothing)
        Dim videoMetaDatas As List(Of VideoMetaData) = getVideoMetaData.GetResultOrDefault(New List(Of VideoMetaData)())
        Dim allFiles As New List(Of VideoMetaData)

        If My.Settings.CBHardcore Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Hardcore AndAlso Not vmd.FeaturesDomme))

        If My.Settings.CBSoftcore Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Softcore AndAlso Not vmd.FeaturesDomme))

        If My.Settings.CBLesbian Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Lesbian AndAlso Not vmd.FeaturesDomme))

        If My.Settings.CBBlowjob Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Blowjob AndAlso Not vmd.FeaturesDomme))

        If My.Settings.CBFemdom = True Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.FemDom AndAlso Not vmd.FeaturesDomme))

        If My.Settings.CBFemsub = True Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.FemSub AndAlso Not vmd.FeaturesDomme))

        If ssh.NoSpecialVideo Then GoTo SkipSpecial
        If ssh.ScriptVideoTeaseFlag Then
            If ssh.ScriptVideoTease = "Censorship Sucks" OrElse ssh.ScriptVideoTease = "Avoid The Edge" OrElse ssh.ScriptVideoTease = "RLGL" Then GoTo SkipSpecial
        End If

        If ssh.RandomizerVideo = True Then GoTo SkipSpecial

        If My.Settings.CBJOI = True Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Joi AndAlso Not vmd.FeaturesDomme))

        If My.Settings.CBCH = True Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.CockHero AndAlso Not vmd.FeaturesDomme))

SkipSpecial:
        If My.Settings.CBGeneral Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.General AndAlso Not vmd.FeaturesDomme))

        ' Domme Videos
        If My.Settings.CBHardcoreD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.General AndAlso vmd.FeaturesDomme))

        If My.Settings.CBSoftcoreD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Softcore AndAlso vmd.FeaturesDomme))

        If My.Settings.CBLesbianD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Lesbian AndAlso vmd.FeaturesDomme))

        If My.Settings.CBBlowjobD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Blowjob AndAlso vmd.FeaturesDomme))

        If My.Settings.CBFemdomD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.FemDom AndAlso vmd.FeaturesDomme))

        If My.Settings.CBFemsubD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.FemSub AndAlso vmd.FeaturesDomme))

        If ssh.NoSpecialVideo Then GoTo SkipSpecialD
        If ssh.ScriptVideoTeaseFlag Then
            If ssh.ScriptVideoTease = "Censorship Sucks" OrElse ssh.ScriptVideoTease = "Avoid The Edge" OrElse ssh.ScriptVideoTease = "RLGL" Then GoTo SkipSpecialD
        End If

        If ssh.RandomizerVideo = True Then GoTo SkipSpecialD

        ' Domme Special Videos
        If My.Settings.CBJOID Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.Joi AndAlso vmd.FeaturesDomme))

        If My.Settings.CBCHD = True Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.CockHero AndAlso vmd.FeaturesDomme))

SkipSpecialD:
        '	Domme  General Videos
        If My.Settings.CBGeneralD Then _
            allFiles.AddRange(videoMetaDatas.Where(Function(vmd) vmd.Genre = VideoGenre.General AndAlso vmd.FeaturesDomme))

        allFiles = allFiles.Distinct().ToList()
        If Not allFiles.Any() OrElse ssh.VideoCheck Then Exit Sub

        Dim videoMetaData As VideoMetaData = allFiles(myRandomNumberService.Roll(0, allFiles.Count))
        Dim genre As VideoGenre = videoMetaData.Genre
        Dim containsDomme As Boolean = videoMetaData.FeaturesDomme

        ssh.VideoType = genre.ToString() + IIf(containsDomme, "D", String.Empty)
        ssh.DommeVideo = containsDomme

        PlayVideo(videoMetaData, False)
    End Sub

    Public Sub RandomVideo()
        ' Reset retentive global variables
        ssh.NoVideo = False
        ssh.DommeVideo = False

        Dim __dom As Random = New Random()
        Dim __domVideo As String
        Dim __TotalFiles As New List(Of String)

        '======================================================================================
        '									Genre Videos
        '======================================================================================
        If My.Settings.CBHardcore = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoHardcore))

        If My.Settings.CBSoftcore = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoSoftcore))

        If My.Settings.CBLesbian = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoLesbian))

        If My.Settings.CBBlowjob = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoBlowjob))

        If My.Settings.CBFemdom = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoFemdom))

        If My.Settings.CBFemsub = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoFemsub))

        If ssh.NoSpecialVideo = True Then GoTo SkipSpecial

        If ssh.ScriptVideoTeaseFlag = True Then
            If ssh.ScriptVideoTease = "Censorship Sucks" Or ssh.ScriptVideoTease = "Avoid The Edge" Or ssh.ScriptVideoTease = "RLGL" Then GoTo SkipSpecial
        End If

        If ssh.RandomizerVideo Then GoTo SkipSpecial
        If My.Settings.CBJOI = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoJOI))

        If My.Settings.CBCH = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoCH))

SkipSpecial:
        '======================================================================================
        '									General Videos
        '======================================================================================
        If My.Settings.CBGeneral = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoGeneral))

        '======================================================================================
        '									Domme - Videos
        '======================================================================================
        If My.Settings.CBHardcoreD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoHardcoreD))

        If My.Settings.CBSoftcoreD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoSoftcoreD))

        If My.Settings.CBLesbianD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoLesbianD))

        If My.Settings.CBBlowjobD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoBlowjobD))

        If My.Settings.CBFemdomD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoFemdomD))

        If My.Settings.CBFemsubD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoFemsubD))

        If ssh.NoSpecialVideo = True Then GoTo SkipSpecialD
        If ssh.ScriptVideoTeaseFlag = True Then
            If ssh.ScriptVideoTease = "Censorship Sucks" Or ssh.ScriptVideoTease = "Avoid The Edge" Or ssh.ScriptVideoTease = "RLGL" Then GoTo SkipSpecialD
        End If

        If ssh.RandomizerVideo = True Then GoTo SkipSpecialD

        '======================================================================================
        '								Domme - Special - Videos
        '======================================================================================
        If My.Settings.CBJOID = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoJOID))

        If My.Settings.CBCHD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoCHD))

SkipSpecialD:
        '======================================================================================
        '								Domme - General Videos
        '======================================================================================
        If My.Settings.CBGeneralD = True Then _
            __TotalFiles.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoGeneralD))



        If __TotalFiles.Count = 0 Then Exit Sub

        If ssh.VideoCheck = True Then Exit Sub

GetAnotherRandomVideo:

        __domVideo = __TotalFiles(__dom.Next(0, __TotalFiles.Count))

        If __domVideo = "" Then GoTo GetAnotherRandomVideo

        Dim genre As VideoGenre = VideoGenre.General

        If My.Settings.CBHardcore AndAlso InStr(__domVideo, My.Settings.VideoHardcore) <> 0 Then genre = VideoGenre.Hardcore
        If My.Settings.CBSoftcore AndAlso InStr(__domVideo, My.Settings.VideoSoftcore) <> 0 Then genre = VideoGenre.Softcore
        If My.Settings.CBLesbian AndAlso InStr(__domVideo, My.Settings.VideoLesbian) <> 0 Then genre = VideoGenre.Lesbian
        If My.Settings.CBBlowjob AndAlso InStr(__domVideo, My.Settings.VideoBlowjob) <> 0 Then genre = VideoGenre.Blowjob
        If My.Settings.CBFemdom AndAlso InStr(__domVideo, My.Settings.VideoFemdom) <> 0 Then genre = VideoGenre.FemDom
        If My.Settings.CBFemsub AndAlso InStr(__domVideo, My.Settings.VideoFemsub) <> 0 Then genre = VideoGenre.FemSub
        If My.Settings.CBJOI AndAlso InStr(__domVideo, My.Settings.VideoJOI) <> 0 Then genre = VideoGenre.Joi
        If My.Settings.CBCH AndAlso InStr(__domVideo, My.Settings.VideoCH) <> 0 Then genre = VideoGenre.CockHero
        If My.Settings.CBGeneral AndAlso InStr(__domVideo, My.Settings.VideoGeneral) <> 0 Then genre = VideoGenre.General

        Dim containsDomme As Boolean = False
        If My.Settings.CBHardcoreD And InStr(__domVideo, My.Settings.VideoHardcoreD) <> 0 Then
            genre = VideoGenre.Hardcore
            containsDomme = True
        End If
        If My.Settings.CBSoftcoreD And InStr(__domVideo, My.Settings.VideoSoftcoreD) <> 0 Then
            genre = VideoGenre.Softcore
            containsDomme = True
        End If
        If My.Settings.CBLesbianD And InStr(__domVideo, My.Settings.VideoLesbianD) <> 0 Then
            genre = VideoGenre.Lesbian
            containsDomme = True
        End If

        If My.Settings.CBBlowjobD And InStr(__domVideo, My.Settings.VideoBlowjobD) <> 0 Then
            genre = VideoGenre.Blowjob
            containsDomme = True
        End If
        If My.Settings.CBFemdomD And InStr(__domVideo, My.Settings.VideoFemdomD) <> 0 Then
            genre = VideoGenre.FemDom
            containsDomme = True
        End If
        If My.Settings.CBFemsubD And InStr(__domVideo, My.Settings.VideoFemsubD) <> 0 Then
            genre = VideoGenre.FemSub
            containsDomme = True
        End If

        If My.Settings.CBJOID And InStr(__domVideo, My.Settings.VideoJOID) <> 0 Then
            genre = VideoGenre.Joi
            containsDomme = True
        End If

        If My.Settings.CBCHD = True And InStr(__domVideo, My.Settings.VideoCHD) <> 0 Then
            genre = VideoGenre.CockHero
            containsDomme = True
        End If

        If My.Settings.CBGeneralD = True And InStr(__domVideo, My.Settings.VideoGeneral) <> 0 Then
            genre = VideoGenre.General
            containsDomme = True
        End If
        ssh.VideoType = genre.ToString() + IIf(containsDomme, "D", String.Empty)
        ssh.DommeVideo = containsDomme

        Dim videoMetaData As VideoMetaData = New VideoMetaData()
        videoMetaData.Key = __domVideo
        videoMetaData.Genre = genre
        videoMetaData.FeaturesDomme = containsDomme
        PlayVideo(videoMetaData, False)
    End Sub

    ''' <summary>
    ''' Start the video passed in.
    ''' </summary>
    ''' <param name="videoMetaData"></param>
    Private Function PlayVideo(videoMetaData As VideoMetaData, makeRandom As Boolean) As Result
        '        domVLC.Visible = True
        DomWMP.Visible = True
        DomWMP.stretchToFit = True

        ' programsettingsPanel.Visible = False
        mainPictureBox.Visible = False
        ' domVLC.playlist.items.clear()
        ' domVLC.playlist.add("file:///" & RandomVideo & "")
        ' domVLC.video.crop = domVLC.Width & ":" & domVLC.Height
        ' domVLC.playlist.play()
        'If FrmSettings.VLCfillRadio.Checked = True Then
        ' domVLC.video.crop = domVLC.Width & ":" & domVLC.Height
        'End If
        'If FrmSettings.VLC43Radio.Checked = True Then domVLC.video.crop = "4:3"
        'If FrmSettings.VLC1610Radio.Checked = True Then domVLC.video.crop = "16:10"
        'If FrmSettings.VLC169Radio.Checked = True Then domVLC.video.crop = "16:9"

        DomWMP.URL = videoMetaData.Key

        If ssh.JumpVideo = True Then
            Do
                Application.DoEvents()
            Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)
            DomWMP.Ctlcontrols.currentPosition = GetStartPostion(makeRandom)
        End If

        ssh.JumpVideo = False
        Return Result.Ok()
    End Function

    Friend Sub PlayRandomJOI()
        'ISSUE: there is no control, if a Domme-Video or a Regular JOI is played.
        'ISSUE: Redundant Code
        Dim JOIVideos As New List(Of String)
        JOIVideos.Clear()

        If FrmSettings.LblVideoJOITotal.Text <> "0" And My.Settings.CBJOI = True Then

            JOIVideos.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoJOI,
                                                         System.IO.SearchOption.AllDirectories))
        End If

        If FrmSettings.LblVideoJOITotalD.Text <> "0" And My.Settings.CBJOID = True Then
            JOIVideos.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoJOI,
                                                         System.IO.SearchOption.AllDirectories))
        End If

        If JOIVideos.Count < 1 Then
            'ISSUE: This Message will occur during running Scripts!
            MessageBox.Show(Me, "No JOI Videos found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If ssh.TeaseVideo = True Then RunFileText()
            ssh.TeaseVideo = False
            Return
        End If

        Dim JOIVideoLine As Integer = ssh.randomizer.Next(0, JOIVideos.Count)

        DomWMP.Visible = True
        DomWMP.stretchToFit = True

        mainPictureBox.Visible = False

        DomWMP.URL = JOIVideos(JOIVideoLine)


    End Sub

    Friend Sub PlayRandomCH()
        'ISSUE: there is no control, if a Domme-Video or a Regular JOI is played.
        'ISSUE: Redundant Code
        Dim CHVideos As New List(Of String)
        CHVideos.Clear()

        If FrmSettings.LblVideoCHTotal.Text <> "0" And My.Settings.CBCH = True Then
            CHVideos.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoCH))
        End If
        If FrmSettings.LblVideoCHTotalD.Text <> "0" And My.Settings.CBCHD = True Then
            CHVideos.AddRange(myDirectory.GetFilesVideo(My.Settings.VideoCHD))
        End If

        If CHVideos.Count < 1 Then
            'ISSUE: This Message will occur during running Scripts!
            MessageBox.Show(Me, "No CH Videos found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If ssh.TeaseVideo = True Then RunFileText()
            ssh.TeaseVideo = False
            Return
        End If

        Dim CHVideoLine As Integer = ssh.randomizer.Next(0, CHVideos.Count)

        DomWMP.Visible = True
        DomWMP.stretchToFit = True

        mainPictureBox.Visible = False

        DomWMP.URL = CHVideos(CHVideoLine)


    End Sub

#End Region

    Private Sub SettingsButton_Click(sender As Object, e As EventArgs) Handles BtnToggleSettings.Click
        If FrmSettings.Visible = True Then
            FrmSettings.Visible = False
            BtnToggleSettings.Text = "Open Settings Menu"
        Else
            FrmSettings.Visible = True
            BtnToggleSettings.Text = "Close Settings Menu"
        End If
    End Sub

    Public Sub CensorshipTimer_Tick(sender As Object, e As EventArgs) Handles CensorshipTimer.Tick


        If ssh.MiniScript = True Then Return
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.CensorshipTick < 6 Then Return
        If chatBox.Text <> "" And ssh.CensorshipTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.CensorshipTick < 6 Then Return
        If ssh.FollowUp <> "" And ssh.CensorshipTick < 6 Then Return

        ssh.CensorshipTick -= 1


        If ssh.CensorshipTick < 1 Then


            Dim CensorLineTemp As Integer = ssh.randomizer.Next(1, 101)


            Dim CensorVideo As String

            If FrmSettings.CBCensorConstant.Checked = True Then GoTo CensorConstant

            If CensorshipBar.Visible = True Then
                CensorshipBar.Visible = False
                ssh.CensorshipTick = ssh.randomizer.Next(FrmSettings.NBCensorHideMin.Value, FrmSettings.NBCensorHideMax.Value + 1)

                If CensorLineTemp > FrmSettings.TauntSlider.Value * 5 Then
                    Return
                End If

                CensorVideo = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Censorship Sucks\CensorBarOff.txt"

            Else

CensorConstant:

                Dim CensorshipBarX As Integer
                Dim CensorshipBarY As Integer
                Dim CensorshipBarY2 As Integer

                Try
                    CensorshipBarY2 = ssh.randomizer.Next(200, DomWMP.Height / 2)
                Catch
                    CensorshipBarY2 = 100
                End Try

                CensorshipBar.Height = CensorshipBarY2
                CensorshipBar.Width = CensorshipBarY2 * 2.6

                'QnD-BUGFIX: if CensorshipBar.Width > DomWMP.Width then ArgumentOutOfRangeException 
                CensorshipBarX = ssh.randomizer.Next(5, If(CensorshipBar.Width > DomWMP.Width, DomWMP.Width, DomWMP.Width - CensorshipBar.Width + 1))
                CensorshipBarY = ssh.randomizer.Next(5, If(CensorshipBar.Height > DomWMP.Height, DomWMP.Height, DomWMP.Height - CensorshipBar.Height + 1))
                CensorshipBar.Location = New Point(CensorshipBarX, CensorshipBarY)



                CensorshipBar.Visible = False
                CensorshipBar.Visible = True
                CensorshipBar.BringToFront()

                ssh.CensorshipTick = ssh.randomizer.Next(FrmSettings.NBCensorShowMin.Value, FrmSettings.NBCensorShowMax.Value + 1)

                If CensorLineTemp > FrmSettings.TauntSlider.Value * 5 Then
                    Return
                End If

                CensorVideo = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Censorship Sucks\CensorBarOn.txt"

            End If

            ' Read all lines of the given file.
            Dim lines As List(Of String) = Txt2List(CensorVideo)

            Dim CensorLine As Integer

            Try
                lines = FilterList(lines)
                If lines.Count < 1 Then Return
                CensorLine = ssh.randomizer.Next(0, lines.Count)
                ssh.DomTask = lines(CensorLine)
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Censorship Sucks line from file: " &
                               CensorVideo, ex, "CensorshipTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Censorship Sucks line"
            End Try

        End If

    End Sub

    Public Sub RLGLTimer_Tick(sender As Object, e As EventArgs) Handles RLGLTimer.Tick
        ' Check all Conditions before starting scripts.
        If ssh.MiniScript = True Then Return
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.RLGLTick < 6 Then Return
        If chatBox.Text <> "" And ssh.RLGLTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.RLGLTick < 6 Then Return
        If ssh.FollowUp <> "" And ssh.RLGLTick < 6 Then Return

        ' Decrement TickCounter if Game is running.
        If ssh.RLGLGame = True Then ssh.RLGLTick -= 1

        ' Run scripts only if time is over.
        If ssh.RLGLTick < 1 Then
            ' Swap the BooleanValue
            ssh.RedLight = Not ssh.RedLight
            ' Turn off TauntTimer when State is red.
            If ssh.RedLight Then RLGLTauntTimer.Stop()

            ' Declare list to read
            Dim tempList As List(Of String)
            Dim file2read As String

            ' Read File according to state and set the next timer-tick-duration.
            If ssh.RedLight Then
                '################################## RED - Light ##################################
                file2read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Red Light Green Light\Red Light.txt"
                tempList = Txt2List(file2read)
                ssh.RLGLTick = ssh.randomizer.Next(FrmSettings.NBRedLightMin.Value, FrmSettings.NBRedLightMax.Value + 1)
            Else
                '################################## Green - Light ################################
                file2read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Red Light Green Light\Green Light.txt"
                tempList = Txt2List(file2read)
                ssh.RLGLTick = ssh.randomizer.Next(FrmSettings.NBGreenLightMin.Value, FrmSettings.NBGreenLightMax.Value + 1)
            End If

            Try
                tempList = FilterList(tempList)
                ssh.DomTask = tempList(ssh.randomizer.Next(0, tempList.Count))
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid RLGL line from file: " &
                               file2read, ex, "RLGLTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid RLGL line"
            End Try
        End If
    End Sub

    Private Sub domName_Leave(sender As Object, e As EventArgs) Handles domName.Leave
        mySettingsAccessor.DommeName = domName.Text
        My.Settings.Save()
        mySession.Session.Domme.Name = mySettingsAccessor.DommeName
    End Sub
    Private Sub SubName_Leave(sender As Object, e As EventArgs) Handles SubName.Leave
        mySettingsAccessor.SubName = SubName.Text.Trim()
        My.Settings.Save()
        mySession.Session.Sub.Name = mySettingsAccessor.SubName
    End Sub

    Public Sub StatusUpdatePost()
        ssh.UpdatingPost = True
        If ssh.UpdateStage > 0 Then GoTo StatusUpdateBegin
        ssh.StatusText = ssh.UpdateList(ssh.randomizer.Next(0, ssh.UpdateList.Count))

        ' Read all lines of the given File.
        Dim lines As List(Of String) = Txt2List(ssh.StatusText)


        For i As Integer = lines.Count - 1 To 0 Step -1
            If lines(i) = "" Or lines(i) Is Nothing Then
                lines.Remove(lines(i))
            End If
        Next

        ssh.StatusText = lines(0)
        ' Github Patch  StatusText = PoundClean(StatusText)

        Dim LoopBuffer As Integer = 0
        Do
            LoopBuffer += 1

            ssh.StatusText = PoundClean(ssh.StatusText)

            If LoopBuffer > 4 Then Exit Do

        Loop Until Not ssh.DomTask.Contains("#")


        Dim AtArray() As String = Split(ssh.StatusText)
        For i As Integer = AtArray.Length - 1 To 0 Step -1
            If AtArray(i).Contains("@") Then
                AtArray(i) = AtArray(i).Replace(AtArray(i), "")
            End If
            If FrmSettings.CBHimHer.Checked = True Then
                If AtArray(i) = "He" Then AtArray(i) = AtArray(i).Replace("He", "She")
                If AtArray(i) = "he" Then AtArray(i) = AtArray(i).Replace("he", "she")
                If AtArray(i) = "Him" Then AtArray(i) = AtArray(i).Replace("Him", "Her")
                If AtArray(i) = "him" Then AtArray(i) = AtArray(i).Replace("him", "her")
                If AtArray(i) = "His" Then AtArray(i) = AtArray(i).Replace("His", "Her")
                If AtArray(i) = "his" Then AtArray(i) = AtArray(i).Replace("his", "her")
            End If
        Next
        ssh.StatusText = Join(AtArray)

        Dim DPic As String = My.Settings.GlitterAV
        DPic = "file://" & DPic
        DPic = DPic.Replace("\", "/")

        Dim StatusName As String


        Dim TextColor As String = Color2Html(My.Settings.ChatTextColor)

        StatusName = StatusUpdates.DocumentText & "<img class=""floatright"" style="" float: left; width: 48; height: 48; border: 0;"" src=""" & DPic & """> <font face=""Cambria"" size=""3"" color=""" & Color2Html(My.Settings.GlitterNCDommeColor) & """><b>" & domName.Text & "</b></font> <br><font face=""Cambria"" size=""2"" color=""DarkGray"">" & Date.Today & "</font><br><br>"
        StatusUpdates.DocumentText = StatusName & "<font face=""Cambria"" size=""2"" color=""" & TextColor & """>" & ssh.StatusText & "</font><br><br>"

        Dim StatusLines1 As New List(Of String)
        For i As Integer = 1 To lines.Count - 1
            StatusLines1.Add(lines(i))
        Next
        ssh.ContactNumber = 1

        ' For i As Integer = StatusLines1.Count - 1 To 0 Step -1
        'If StatusLines1(i) = "" Or StatusLines1(i) Is Nothing Then
        'StatusLines1.Remove(StatusLines1(i))
        'End If
        'Next

        StatusLines1 = StatusClean(StatusLines1)



        ssh.StatusText1 = StatusLines1(ssh.randomizer.Next(0, StatusLines1.Count))

        Dim StatusLines2 As New List(Of String)
        For i As Integer = 1 To lines.Count - 1
            StatusLines2.Add(lines(i))
        Next
        ssh.ContactNumber = 2

        ' For i As Integer = StatusLines2.Count - 1 To 0 Step -1
        'If StatusLines2(i) = "" Or StatusLines2(i) Is Nothing Then
        'StatusLines2.Remove(StatusLines2(i))
        'End If
        'Next

        StatusLines2 = StatusClean(StatusLines2)




        Do
            ssh.StatusText2 = StatusLines2(ssh.randomizer.Next(0, StatusLines2.Count))
        Loop Until ssh.StatusText2 <> ssh.StatusText1


        Dim StatusLines3 As New List(Of String)
        For i As Integer = 1 To lines.Count - 1
            StatusLines3.Add(lines(i))
        Next
        ssh.ContactNumber = 3

        'For i As Integer = StatusLines3.Count - 1 To 0 Step -1
        'If StatusLines3(i) = "" Or StatusLines3(i) Is Nothing Then
        'StatusLines3.Remove(StatusLines3(i))
        'End If
        'Next

        StatusLines3 = StatusClean(StatusLines3)

        Do
            ssh.StatusText3 = StatusLines3(ssh.randomizer.Next(0, StatusLines3.Count))
        Loop Until ssh.StatusText3 <> ssh.StatusText2 And ssh.StatusText3 <> ssh.StatusText1

        ssh.StatusText1 = ssh.StatusText1.Replace("#ShortName", My.Settings.GlitterSN)
        ssh.StatusText2 = ssh.StatusText2.Replace("#ShortName", My.Settings.GlitterSN)
        ssh.StatusText3 = ssh.StatusText3.Replace("#ShortName", My.Settings.GlitterSN)

        ssh.StatusText1 = ssh.StatusText1.Replace("#SubName", SubName.Text)
        ssh.StatusText2 = ssh.StatusText2.Replace("#SubName", SubName.Text)
        ssh.StatusText3 = ssh.StatusText3.Replace("#SubName", SubName.Text)

        ssh.StatusText1 = PoundClean(ssh.StatusText1)
        ssh.StatusText2 = PoundClean(ssh.StatusText2)
        ssh.StatusText3 = PoundClean(ssh.StatusText3)

        'GoTo TestSkip

        Dim AtArray1() As String = Split(ssh.StatusText1)
        For i As Integer = AtArray1.Length - 1 To 0 Step -1
            If AtArray1(i).Contains("@") Then
                AtArray1(i) = AtArray1(i).Replace(AtArray1(i), "")
            End If
            If FrmSettings.CBHimHer.Checked = True Then
                If AtArray1(i) = "He" Then AtArray1(i) = AtArray1(i).Replace("He", "She")
                If AtArray1(i) = "he" Then AtArray1(i) = AtArray1(i).Replace("he", "she")
                If AtArray1(i) = "Him" Then AtArray1(i) = AtArray1(i).Replace("Him", "Her")
                If AtArray1(i) = "him" Then AtArray1(i) = AtArray1(i).Replace("him", "her")
                If AtArray1(i) = "His" Then AtArray1(i) = AtArray1(i).Replace("His", "Her")
                If AtArray1(i) = "his" Then AtArray1(i) = AtArray1(i).Replace("his", "her")
            End If
        Next
        ssh.StatusText1 = Join(AtArray1)

        Dim AtArray2() As String = Split(ssh.StatusText2)
        For i As Integer = AtArray2.Length - 1 To 0 Step -1
            If AtArray2(i).Contains("@") Then
                AtArray2(i) = AtArray2(i).Replace(AtArray2(i), "")
            End If
            If FrmSettings.CBHimHer.Checked = True Then
                If AtArray2(i) = "He" Then AtArray2(i) = AtArray2(i).Replace("He", "She")
                If AtArray2(i) = "he" Then AtArray2(i) = AtArray2(i).Replace("he", "she")
                If AtArray2(i) = "Him" Then AtArray2(i) = AtArray2(i).Replace("Him", "Her")
                If AtArray2(i) = "him" Then AtArray2(i) = AtArray2(i).Replace("him", "her")
                If AtArray2(i) = "His" Then AtArray2(i) = AtArray2(i).Replace("His", "Her")
                If AtArray2(i) = "his" Then AtArray2(i) = AtArray2(i).Replace("his", "her")
            End If
        Next
        ssh.StatusText2 = Join(AtArray2)

        Dim AtArray3() As String = Split(ssh.StatusText3)
        For i As Integer = AtArray3.Length - 1 To 0 Step -1
            If AtArray3(i).Contains("@") Then
                AtArray3(i) = AtArray3(i).Replace(AtArray3(i), "")
            End If
            If FrmSettings.CBHimHer.Checked = True Then
                If AtArray3(i) = "He" Then AtArray3(i) = AtArray(i).Replace("He", "She")
                If AtArray3(i) = "he" Then AtArray3(i) = AtArray(i).Replace("he", "she")
                If AtArray3(i) = "Him" Then AtArray3(i) = AtArray(i).Replace("Him", "Her")
                If AtArray3(i) = "him" Then AtArray3(i) = AtArray(i).Replace("him", "her")
                If AtArray3(i) = "His" Then AtArray3(i) = AtArray(i).Replace("His", "Her")
                If AtArray3(i) = "his" Then AtArray3(i) = AtArray(i).Replace("his", "her")
            End If
        Next
        ssh.StatusText3 = Join(AtArray3)

        'TestSkip:

        ssh.Update1 = False
        ssh.Update2 = False
        ssh.Update3 = False

        ssh.StatusChance1 = ssh.randomizer.Next(1, 101)
        ssh.StatusChance2 = ssh.randomizer.Next(1, 101)
        ssh.StatusChance3 = ssh.randomizer.Next(1, 101)

        ssh.UpdateStageTick = ssh.randomizer.Next(10, 21)
        UpdateStageTimer.Start()
        ssh.UpdateStage = 1
        Return


StatusUpdateBegin:

        If ssh.Update1 = True And ssh.Update2 = True And ssh.Update3 = True Then GoTo StatusUpdateEnd

        'ContactTick = randomizer.Next(10, 21)
        'ContactFlag = True
        'ContactTimer.Start()

        'Do
        'Application.DoEvents()
        'Loop Until ContactFlag = False

        'Delay(RandomDelay)

ReRoll:

        ssh.TempVal = ssh.randomizer.Next(1, 4)

        If ssh.TempVal = 1 Then
            If ssh.Update1 = False Then
                GoTo StatusUpdate1
            Else
                GoTo ReRoll
            End If
        End If

        If ssh.TempVal = 2 Then
            If ssh.Update2 = False Then
                GoTo StatusUpdate2
            Else
                GoTo ReRoll
            End If
        End If

        If ssh.TempVal = 3 Then
            If ssh.Update3 = False Then
                GoTo StatusUpdate3
            Else
                GoTo ReRoll
            End If
        End If

        GoTo ReRoll

StatusUpdate1:

        Dim S1Pic As String = My.Settings.GlitterAV1
        S1Pic = "file://" & S1Pic
        S1Pic = S1Pic.Replace("\", "/")

        TextColor = Color2Html(My.Settings.ChatTextColor)

        If ssh.StatusChance1 < My.Settings.Glitter1Slider * 10 And My.Settings.CBGlitter1 = True Then
            StatusName = StatusUpdates.DocumentText & "<img class=""floatright"" style="" float: left; width: 32; height: 32; border: 0;"" src=""" & S1Pic & """> <font face=""Cambria"" size=""3"" color=""" & Color2Html(My.Settings.GlitterNC1Color) & """><b>" & My.Settings.Glitter1 & "</b></font><br> <font face=""Cambria"" size=""2"" color=""DarkGray"">" & Date.Today & "</font><br>" ' & "<font face=""Cambria"" size=""2"" color=""DarkGray"">" & TimeOfDay & "</font><br>"
            StatusUpdates.DocumentText = StatusName & "<font face=""Cambria"" size=""2"" color=""" & TextColor & """>" & ssh.StatusText1 & "</font><br><br>"
        End If

        ssh.Update1 = True

        ssh.UpdateStageTick = ssh.randomizer.Next(10, 21)
        UpdateStageTimer.Start()
        Return
        'GoTo StatusUpdateBegin

StatusUpdate2:

        Dim S2Pic As String = My.Settings.GlitterAV2
        S2Pic = "file://" & S2Pic
        S2Pic = S2Pic.Replace("\", "/")

        TextColor = Color2Html(My.Settings.ChatTextColor)

        If ssh.StatusChance2 < My.Settings.Glitter2Slider * 10 And My.Settings.CBGlitter2 = True Then
            StatusName = StatusUpdates.DocumentText & "<img class=""floatright"" style="" float: left; width: 32; height: 32; border: 0;"" src=""" & S2Pic & """> <font face=""Cambria"" size=""3"" color=""" & Color2Html(My.Settings.GlitterNC2Color) & """><b>" & My.Settings.Glitter2 & "</b></font><br> <font face=""Cambria"" size=""2"" color=""DarkGray"">" & Date.Today & "</font><br>" ' & "<font face=""Cambria"" size=""2"" color=""DarkGray"">" & TimeOfDay & "</font><br>"
            StatusUpdates.DocumentText = StatusName & "<font face=""Cambria"" size=""2"" color=""" & TextColor & """>" & ssh.StatusText2 & "</font><br><br>"


        End If

        ssh.Update2 = True
        ssh.UpdateStageTick = ssh.randomizer.Next(10, 21)
        UpdateStageTimer.Start()
        Return

        'GoTo StatusUpdateBegin

StatusUpdate3:

        Dim S3Pic As String = My.Settings.GlitterAV3
        S3Pic = "file://" & S3Pic
        S3Pic = S3Pic.Replace("\", "/")

        TextColor = Color2Html(My.Settings.ChatTextColor)

        If ssh.StatusChance3 < My.Settings.Glitter3Slider * 10 And My.Settings.CBGlitter3 = True Then
            StatusName = StatusUpdates.DocumentText & "<img class=""floatright"" style="" float: left; width: 32; height: 32; border: 0;"" src=""" & S3Pic & """> <font face=""Cambria"" size=""3"" color=""" & Color2Html(My.Settings.GlitterNC3Color) & """><b>" & My.Settings.Glitter3 & "</b></font><br> <font face=""Cambria"" size=""2"" color=""DarkGray"">" & Date.Today & "</font><br>" ' & "<font face=""Cambria"" size=""2"" color=""DarkGray"">" & TimeOfDay & "</font><br>"
            StatusUpdates.DocumentText = StatusName & "<font face=""Cambria"" size=""2"" color=""" & TextColor & """>" & ssh.StatusText3 & "</font><br><br>"


        End If

        ssh.Update3 = True

        ssh.UpdateStageTick = ssh.randomizer.Next(10, 21)
        UpdateStageTimer.Start()
        Return
        'GoTo StatusUpdateBegin

StatusUpdateEnd:

        ssh.UpdateStage = 0

        ' Github Patch 'StatusText = "Null" & Environment.NewLine & "Null" & Environment.NewLine & "Null" & Environment.NewLine & "Null" & Environment.NewLine

        ssh.UpdatingPost = False


    End Sub

    Public Function StatusClean(ByVal ListClean As List(Of String)) As List(Of String)



        ListClean.Add("### BUFFER LINE ###")

        If ssh.ContactNumber = 1 Then
            For i As Integer = ListClean.Count - 1 To 0 Step -1
                If ListClean(i).Contains("@Bratty") Then
                    ListClean(i) = ListClean(i).Replace("@Bratty ", "")
                End If
                If ListClean(i).Contains("@Contact2") Then
                    ListClean.Remove(ListClean(i))
                End If
                If ListClean(i).Contains("@Contact3") Then
                    ListClean.Remove(ListClean(i))
                End If
            Next
        End If

        If ssh.ContactNumber = 2 Then
            For i As Integer = ListClean.Count - 1 To 0 Step -1
                If ListClean(i).Contains("@Caring") Then
                    ListClean(i) = ListClean(i).Replace("@Caring ", "")
                End If
                If ListClean(i).Contains("@Contact1") Then
                    ListClean.Remove(ListClean(i))
                End If
                If ListClean(i).Contains("@Contact3") Then
                    ListClean.Remove(ListClean(i))
                End If
            Next
        End If

        If ssh.ContactNumber = 3 Then
            For i As Integer = ListClean.Count - 1 To 0 Step -1
                If ListClean(i).Contains("@Cruel") Then
                    ListClean(i) = ListClean(i).Replace("@Cruel ", "")
                End If
                If ListClean(i).Contains("@Contact1") Then
                    ListClean.Remove(ListClean(i))
                End If
                If ListClean(i).Contains("@Contact2") Then
                    ListClean.Remove(ListClean(i))
                End If
            Next
        End If

        For i As Integer = ListClean.Count - 1 To 0 Step -1
            If ListClean(i).Contains("@Angry") Then
                ListClean.Remove(ListClean(i))
            End If
            If ListClean(i).Contains("@Custom1") Then
                ListClean.Remove(ListClean(i))
            End If
            If ListClean(i).Contains("@Custom2") Then
                ListClean.Remove(ListClean(i))
            End If
        Next



        Try
            ListClean.Remove(ListClean(ListClean.Count - 1))
        Catch
            If ssh.ContactNumber = 1 Then ssh.DomTask = "ERROR: Tease AI did not return a valid Glitter line for Contact 1"
            If ssh.ContactNumber = 2 Then ssh.DomTask = "ERROR: Tease AI did not return a valid Glitter line for Contact 2"
            If ssh.ContactNumber = 3 Then ssh.DomTask = "ERROR: Tease AI did not return a valid Glitter line for Contact 3"
        End Try

        Return ListClean

    End Function

    Private Sub Delay(ByVal Milliseconds As Integer)
        Dim Count As Integer
        Milliseconds *= 1000
        Do Until Count >= Milliseconds
            Count += 1
            Thread.Sleep(1)
            Application.DoEvents()
        Loop
    End Sub

    Private Sub domAvatar_Click(sender As Object, e As EventArgs) Handles domAvatar.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            Try
                domAvatar.Image.Dispose()
            Catch
            End Try
            domAvatar.Image = Nothing
            GC.Collect()


            domAvatar.Image = Image.FromFile(OpenFileDialog1.FileName)
            mySettingsAccessor.DommeAvatarImageName = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub UpdatesTimer_Tick(sender As Object, e As EventArgs) Handles UpdatesTimer.Tick
        ' GLITTER POSTING

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If My.Settings.CBGlitterFeed = True And ssh.UpdatingPost = False Then

            ssh.UpdatesTick -= 1

            If ssh.UpdatesTick < 1 Then

                ssh.UpdatesTick = 1080 / My.Settings.GlitterDSlider

                ssh.UpdateList.Clear()

                If My.Settings.CBTease = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Tease\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        ssh.UpdateList.Add(foundFile)
                    Next
                End If

                If My.Settings.CBEgotist = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Egotist\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        ssh.UpdateList.Add(foundFile)
                    Next
                End If

                If My.Settings.CBTrivia = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Trivia\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        ssh.UpdateList.Add(foundFile)
                    Next
                End If

                If My.Settings.CBDaily = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Daily\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        ssh.UpdateList.Add(foundFile)
                    Next
                End If

                If My.Settings.CBCustom1 = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Custom 1\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        ssh.UpdateList.Add(foundFile)
                    Next
                End If

                If My.Settings.CBCustom2 = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Custom 2\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        ssh.UpdateList.Add(foundFile)
                    Next
                End If

                If ssh.UpdateList.Count < 1 Then
                    My.Settings.CBGlitterFeed = False
                    'MessageBox.Show(Me, "Tease AI attempted to create a Glitter update, but no files were found! Please make sure at least one category containing Glitter txt files has been selected.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    MessageBox.Show(Me, "Tease AI attempted to create a Glitter update, but no files were found! Please make sure at least one category containing Glitter txt files has been selected." & Environment.NewLine _
                    & Environment.NewLine & "Glitter feed has been automatically disabled.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    Return
                End If



                StatusUpdatePost()



            End If

        End If
    End Sub

    'TODO-Next: Move to proper Region
    Private Sub MediaButton_Click(sender As Object, e As EventArgs) Handles BtnToggleMediaPanel.Click

        If SplitContainer1.Panel2.Height < 68 Then Return

        If PNLMediaBar.Visible = True Then
            PNLMediaBar.Visible = False
            BtnToggleMediaPanel.Text = "Show Media Panel"
        Else

            PNLMediaBar.Visible = True
            BtnToggleMediaPanel.Text = "Hide Media Panel"
        End If

        ScrollChatDown()

    End Sub

    <Obsolete("In Progress")>
    Public Function SysKeywordClean(ByVal StringClean As String) As String

        ' StringClean = StringClean.Replace("#SubWritingTaskRAND", randomizer.Next(NBWritingTaskMin.Value / 10, (NBWritingTaskMax.Value / 10) + 1)) * 10

        If StringClean.Contains("#DateDifference(") Then

            Dim DateFlag As String = GetParentheses(StringClean, "#DateDifference(")
            Dim OriginalFlag As String = DateFlag
            DateFlag = FixCommas(DateFlag)
            Dim DateArray() As String = DateFlag.Split(",")

            Dim DDiff As Integer

            If UCase(DateArray(1)).Contains("SECOND") Then DDiff = DateDiff(DateInterval.Second, GetDate(DateArray(0)), Now)
            If UCase(DateArray(1)).Contains("MINUTE") Then DDiff = DateDiff(DateInterval.Minute, GetDate(DateArray(0)), Now)
            If UCase(DateArray(1)).Contains("HOUR") Then DDiff = DateDiff(DateInterval.Hour, GetDate(DateArray(0)), Now)
            If UCase(DateArray(1)).Contains("DAY") Then DDiff = DateDiff(DateInterval.Day, GetDate(DateArray(0)), Now)
            If UCase(DateArray(1)).Contains("WEEK") Then DDiff = DateDiff(DateInterval.Day, GetDate(DateArray(0)), Now) * 7
            If UCase(DateArray(1)).Contains("MONTH") Then DDiff = DateDiff(DateInterval.Month, GetDate(DateArray(0)), Now)
            If UCase(DateArray(1)).Contains("YEAR") Then DDiff = DateDiff(DateInterval.Year, GetDate(DateArray(0)), Now)

            StringClean = StringClean.Replace("#DateDifference(" & OriginalFlag & ")", DDiff)

        End If


        If ssh.AssImage = True Then StringClean = StringClean.Replace("#TnAFastSlidesResult", "#BBnB_Ass")
        If ssh.BoobImage = True Then StringClean = StringClean.Replace("#TnAFastSlidesResult", "#BBnB_Boobs")



        StringClean = StringClean.Replace("#BronzeTokens", ssh.BronzeTokens)
        StringClean = StringClean.Replace("#SilverTokens", ssh.SilverTokens)
        StringClean = StringClean.Replace("#GoldTokens", ssh.GoldTokens)

        'StringClean = StringClean.Replace("#Sys_SubLeftEarly", My.Settings.Sys_SubLeftEarly)
        'StringClean = StringClean.Replace("#Sys_SubLeftEarlyTotal", My.Settings.Sys_SubLeftEarlyTotal)

        StringClean = StringClean.Replace("#SlideshowCount", ssh.CustomSlideshow.Count - 1)
        StringClean = StringClean.Replace("#SlideshowCurrent", ssh.CustomSlideshow.Index)
        StringClean = StringClean.Replace("#SlideshowRemaining", (ssh.CustomSlideshow.Count - 1) - ssh.CustomSlideshow.Index)

        If StringClean.Contains("#Var[") Then

            'Dim VarSplit As String() = StringClean.Split("]")
            'For i As Integer = 0 To VarSplit.Count - 1
            'If VarSplit(i).Contains("#Var[") Then
            'Dim VarString As String = VarSplit(i) & "]"
            'Dim VarFlag As String = GetParentheses(VarString, "#Var[")
            'Dim VarFlag2 As String = GetVariable(VarFlag)
            ' StringClean = StringClean.Replace("#Var[" & VarFlag & "]", VarFlag2)
            'StringClean = StringClean.Replace("#Var[" & VarFlag & "]", VarFlag2)
            'End If
            '    Next

            StringClean = StringClean.Replace("#Var[", "@ShowVar[")

        End If

        'BUG: #RandomSlideshowCategory does not work! The Variable RandomSlideshowCategory is never set.
        If StringClean.Contains("#RandomSlideshowCategory") Then StringClean = StringClean.Replace("#RandomSlideshowCategory", ssh.RandomSlideshowCategory)

        If StringClean.Contains("#EdgeHold") Then
            Dim i As Integer = FrmSettings.HoldEdgeMinimum.Value
            If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then i *= 60

            Dim x As Integer = FrmSettings.HoldEdgeMaximum.Value
            If FrmSettings.LBLMaxHold.Text = "minutes" Then x *= 60

            Dim t As Integer = ssh.randomizer.Next(i, x + 1)
            If t >= 5 Then t = 5 * Math.Round(t / 5)
            Dim TConvert As String = ConvertSeconds(t)
            StringClean = StringClean.Replace("#EdgeHold", TConvert)
        End If

        If StringClean.Contains("#LongHold") Then
            Dim i As Integer = FrmSettings.LongEdgeHoldMinimum.Value
            Dim x As Integer = FrmSettings.LongEdgeHoldMaximum.Value
            Dim t As Integer = ssh.randomizer.Next(i, x + 1)
            t *= 60
            If t >= 5 Then t = 5 * Math.Round(t / 5)
            Dim TConvert As String = ConvertSeconds(t)
            StringClean = StringClean.Replace("#LongHold", TConvert)
        End If

        If StringClean.Contains("#ExtremeHold") Then
            Dim i As Integer = FrmSettings.ExtremeEdgeHoldMinimum.Value
            Dim x As Integer = FrmSettings.ExtremeEdgeHoldMaximum.Value
            Dim t As Integer = ssh.randomizer.Next(i, x + 1)
            t *= 60
            If t >= 5 Then t = 5 * Math.Round(t / 5)
            Dim TConvert As String = ConvertSeconds(t)
            StringClean = StringClean.Replace("#ExtremeHold", TConvert)
        End If

        StringClean = StringClean.Replace("#CurrentImage", ssh.ImageLocation)

        Return StringClean


    End Function

    Public Function ConvertSeconds(ByVal Seconds As Integer) As String

        Dim RetVal As String

        Dim SecondsDifference As Integer = Seconds
        Dim HMS = TimeSpan.FromSeconds(SecondsDifference)
        Dim H = HMS.Hours.ToString
        Dim M = HMS.Minutes.ToString
        Dim S = HMS.Seconds.ToString

        If HMS.Hours = 1 Then
            H = "1 hour"
        Else
            H = H & " hours"
        End If

        If HMS.Minutes = 1 Then
            M = "1 minute"
        Else
            Dim t As Integer = HMS.Minutes
            If t >= 5 Then t = 5 * Math.Round(t / 5)
            M = t & " minutes"
        End If

        If HMS.Minutes > 4 Or HMS.Hours > 0 Or HMS.Seconds = 0 Then
            S = ""
        Else
            If HMS.Seconds = 1 Then
                S = "1 second"
            Else
                S = S & " seconds"
            End If
        End If

        RetVal = ""

        If HMS.Hours > 0 Then
            RetVal = RetVal & H
            If HMS.Minutes > 0 Then RetVal = RetVal & " and "
        End If

        If HMS.Minutes > 0 Then
            RetVal = RetVal & M
            If HMS.Seconds > 0 And HMS.Hours < 1 And HMS.Minutes < 4 Then RetVal = RetVal & " and "
        End If

        RetVal = RetVal & S

        Return RetVal


    End Function

    Public Function PoundClean(ByVal StringClean As String) As String
#If TRACE Then
        Dim TS As New TraceSwitch("PoundClean", "")

        If TS.TraceVerbose Then
            Trace.WriteLine("============= PoundClean(String) =============")
            Trace.Indent()
            Trace.WriteLine(String.Format("StartValue: ""{0}""", StringClean))
        ElseIf TS.TraceInfo Then
            Trace.WriteLine(String.Format("PoundClean(String) parsing: ""{0}""", StringClean))
            Trace.Indent()
        End If

        Dim sw As New Stopwatch
        Dim StartTime As Date = Now
        sw.Start()
#End If

        Dim OrgString As String = StringClean
        Dim Recurrence As Integer = 0

        Do While Recurrence < 5 AndAlso (StringClean.Contains("#") Or StringClean.Contains("@Tag"))
            Recurrence += 1

#If TRACE Then
            If TS.TraceVerbose Then
                Trace.WriteLine(String.Format("Starting scan run {0} on ""{1}""", Recurrence, StringClean))
                Trace.Indent()
            End If
#End If

            StringClean = SysKeywordClean(StringClean)
#If TRACE Then
            If TS.TraceVerbose Then Trace.WriteLine(String.Format("System keywords cleaned: ""{0}""", StringClean))
#End If


            'Bug: TextedTags have to be applied after the image is displayed.
            ssh.FoundTag = "NULL"
            Dim slide As ContactData = ssh.SlideshowMain
            If slide.CurrentImage = String.Empty Then GoTo SkipTextedTags

            Dim TagFilePath As String = Path.GetDirectoryName(slide.CurrentImage) & "\ImageTags.txt"

            If (ssh.SlideshowLoaded = True And mainPictureBox.Image IsNot Nothing And DomWMP.Visible = False) _
            AndAlso File.Exists(TagFilePath) Then
                ' Read all lines of the given file.
                Dim TagList As List(Of String) = Txt2List(TagFilePath)

                Try
                    For t As Integer = 0 To TagList.Count - 1
                        If TagList(t).Contains(Path.GetFileName(slide.CurrentImage)) Then
                            ssh.FoundTag = TagList(t)
                            Dim FoundTagSplit As String() = Split(ssh.FoundTag)
                            For j As Integer = 0 To FoundTagSplit.Length - 1
                                If FoundTagSplit(j).Contains("TagGarment") Then
                                    ssh.TagGarment = FoundTagSplit(j).Replace("TagGarment", "")
                                    ssh.TagGarment = ssh.TagGarment.Replace("-", " ")
                                End If

                                If FoundTagSplit(j).Contains("TagUnderwear") Then
                                    ssh.TagUnderwear = FoundTagSplit(j).Replace("TagUnderwear", "")
                                    ssh.TagUnderwear = ssh.TagUnderwear.Replace("-", " ")
                                End If

                                If FoundTagSplit(j).Contains("TagTattoo") Then
                                    ssh.TagTattoo = FoundTagSplit(j).Replace("TagTattoo", "")
                                    ssh.TagTattoo = ssh.TagTattoo.Replace("-", " ")
                                End If

                                If FoundTagSplit(j).Contains("TagSexToy") Then
                                    ssh.TagSexToy = FoundTagSplit(j).Replace("TagSexToy", "")
                                    ssh.TagSexToy = ssh.TagSexToy.Replace("-", " ")
                                End If

                                If FoundTagSplit(j).Contains("TagFurniture") Then
                                    ssh.TagFurniture = FoundTagSplit(j).Replace("TagFurniture", "")
                                    ssh.TagFurniture = ssh.TagFurniture.Replace("-", " ")
                                End If

                            Next
                            Exit For
                        End If
                    Next
                Catch
                End Try
            End If

            StringClean = StringClean.Replace("#TagGarment", ssh.TagGarment)
            StringClean = StringClean.Replace("#TagUnderwear", ssh.TagUnderwear)
            StringClean = StringClean.Replace("#TagTattoo", ssh.TagTattoo)
            StringClean = StringClean.Replace("#TagSexToy", ssh.TagSexToy)
            StringClean = StringClean.Replace("#TagFurniture", ssh.TagFurniture)
SkipTextedTags:

            If StringClean.Contains("#") Or StringClean.Contains("@Tag") Then

                Dim re As New Regex("#[#\w\d\+\-_]+", RegexOptions.IgnoreCase)
                Dim mc As MatchCollection = re.Matches(StringClean)

                For Each keyword As Match In mc
#If TRACE Then
                    If TS.TraceVerbose Then Trace.WriteLine(String.Format("Applying vocabulary: ""{0}""", keyword.Value))
#End If

                    Dim filepath As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\" & keyword.Value & ".txt"

                    If Directory.Exists(Path.GetDirectoryName(filepath)) AndAlso File.Exists(filepath) Then
                        Dim lines As List(Of String) = Txt2List(filepath)

                        Try
                            lines = FilterList(lines)
                            Dim PoundVal As Integer = ssh.randomizer.Next(0, lines.Count)
                            StringClean = StringClean.Replace(keyword.Value, lines(PoundVal))
                        Catch ex As Exception
                            Log.WriteError("Error Processing vocabulary file: " & filepath, ex,
                                            "Tease AI did not return a valid line while parsing vocabulary file.")
                            StringClean = "ERROR: Tease AI did not return a valid line while parsing vocabulary file: " & keyword.Value
                        End Try

                        StringClean = StringClean.Replace("TagFace", "")
                        StringClean = StringClean.Replace("TagBoobs", "")
                        StringClean = StringClean.Replace("TagPussy", "")
                        StringClean = StringClean.Replace("TagAss", "")
                        StringClean = StringClean.Replace("TagFeet", "")
                        StringClean = StringClean.Replace("TagFullyDressed", "")
                        StringClean = StringClean.Replace("TagHalfDressed", "")
                        StringClean = StringClean.Replace("TagNaked", "")
                        StringClean = StringClean.Replace("TagSideView", "")
                        StringClean = StringClean.Replace("TagCloseUp", "")
                        StringClean = StringClean.Replace("TagMasturbating", "")
                        StringClean = StringClean.Replace("TagSucking", "")
                        StringClean = StringClean.Replace("TagSmiling", "")
                        StringClean = StringClean.Replace("TagGlaring", "")
                        StringClean = StringClean.Replace("TagSeeThrough", "")
                        StringClean = StringClean.Replace("TagAllFours", "")

                    Else
                        StringClean = StringClean.Replace(keyword.Value, "<font color=""red"">" & keyword.Value & "</font>")

                        Dim lazytext As String = "Unable to locate vocabulary file: """ & keyword.Value & """"
                        Log.WriteError(lazytext, New Exception(lazytext), "PoundClean(String)")

                    End If


                Next

            End If
#If TRACE Then
            Trace.Unindent()
#End If
        Loop

        If StringClean.Contains("#") Then
#If TRACE Then
            If TS.TraceError Then
                Trace.WriteLine("PoundClean(String): Stopping scan, maximum allowed scan depth reached.")
                Trace.Indent()
                Trace.WriteLine(String.Format("StartValue: ""{0}""", OrgString))
                Trace.WriteLine(String.Format("EndValue:   ""{0}""", StringClean))
                Trace.Unindent()
            End If
#End If
            Log.WriteError("Maximum allowed Vocabulary depth reached for line:" & OrgString & vbCrLf &
                           "Aborted Cleaning at: " & StringClean,
                           New StackOverflowException("PoundClean infinite loop protection"), "PoundClean(String)")
        Else
#If TRACE Then
            If TS.TraceVerbose Then
                Trace.WriteLine(String.Format("EndValue: ""{0}""", StringClean))
                Trace.WriteLine(String.Format("Duration: {0}ms", (Now - StartTime).TotalMilliseconds.ToString))
            End If
#End If

        End If

#If TRACE Then
        Trace.Unindent()
#End If
        Return StringClean
    End Function

    Public Function CommandClean(ByVal StringClean As String, Optional ByVal TaskClean As Boolean = False) As String
        Dim domme As DommePersonality = CreateDommePersonality()

        If TaskClean = True Then
            GoTo TaskCleanSet
        End If

RinseLatherRepeat:

        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        '									ImageCommands
        ' - Make sure you call all Display ImageFunctions before executing @LockImages.
        '	If you don't, FilterList() will return a wrong list of lines =>
        '		=> The Domme is talking about an image, but she never showed one.
        '		=> She is talking about an new image, but never showed one.
        ' - Call @DeleteLocalImage before you start to display a new Image, because they 
        '	are loaded and displayed async. Otherwise it will delete the wrong image!
        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

        ' @DeleteLocalImage: Deletes the current displayed local image from filesystem, 
        ' LiskedList, DislikedList and LocalImageTagList,  if the  current Image is 
        ' not an image in the Domme- or Contacts-Image directory or their subdirectories.
        If StringClean.Contains("@DeleteLocalImage") Then
            If mySettingsAccessor.CanDommeDeleteFiles Then
                Try
                    DeleteCurrentImage(True)
                Catch ex As Exception
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    '                   All Errors
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    Log.WriteError("Command @DeleteLocalImage was unable to delete the image.",
                                   ex, "@DeleteLocalImage failed")
                End Try
            End If
            StringClean = StringClean.Replace("@DeleteLocalImage", "")
        End If

        ' @DeleteImage: Deletes the current displayed image from filesystem, LiskedList, 
        ' DislikedList, LocalImageTagList and URL-Files, if the  current Image is 
        ' not an image in the Domme- or Contacts-Image directory or their subdirectories.
        If StringClean.Contains("@DeleteImage") Then
            If mySettingsAccessor.CanDommeDeleteFiles Then
                Try
                    DeleteCurrentImage(False)
                Catch ex As Exception
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    '                   All Errors
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    Log.WriteError("Command @DeleteImage was unable to delete the image.",
                                   ex, "@DeleteImage failed")
                End Try
            End If
            StringClean = StringClean.Replace("@DeleteImage", "")
        End If

        ' The @UnlockImages Command allows the Domme Slideshow to resume functioning as normal.
        If StringClean.Contains("@UnlockImages") Then
            If ssh.SlideshowLoaded = True Then
                ImageSlideShowNextButton.Enabled = True
                ImageSlideShowPreviousButton.Enabled = True
                PicStripTSMIdommeSlideshow.Enabled = True
            End If
            ssh.LockImage = False
            StringClean = StringClean.Replace("@UnlockImages", "")
        End If

        If StringClean.Contains("@DommeTag(") Then
            Dim TagFlag As String = GetParentheses(StringClean, "@DommeTag(")
            'QND-Implemented: ContactData.GetTaggedImage
            If ContactToUse IsNot Nothing Then
                ssh.DommeImageSTR = ContactToUse.GetTaggedImage(TagFlag, True)
            Else
                ssh.DommeImageSTR = ""
            End If
            ' Clean the Text.
            StringClean = StringClean.Replace("@DommeTag(" & TagFlag & ")", "")
        End If

        If StringClean.Contains("@NewDommeSlideshow") Then
            'TODO: Add Support for contact slideshows.
            ssh.SlideshowMain.LoadNew()
            ssh.SlideshowMain.CurrentImage()
            StringClean = StringClean.Replace("@NewDommeSlideshow", "")
        End If

        If StringClean.Contains("@DomTag(") Then
            Dim TagFlag As String = GetParentheses(StringClean, "@DomTag(")
            ' Try to get a Domme Image for the given Tags.
            'QND-Implemented: ContactData.GetTaggedImage
            If ContactToUse IsNot Nothing Then
                ssh.DommeImageSTR = ContactToUse.GetTaggedImage(TagFlag, True)
            Else
                ssh.DommeImageSTR = ""
            End If

            StringClean = StringClean.Replace("@DomTag(" & TagFlag & ")", "")
        End If

        If StringClean.Contains("@ImageTag(") Then
            Dim TagFlag As String = GetParentheses(StringClean, "@ImageTag(")
            ShowImage(GetLocalImage(TagFlag), False)
            StringClean = StringClean.Replace("@ImageTag(" & TagFlag & ")", "")
        End If

        If StringClean.Contains("@ShowLocalImage") And Not StringClean.Contains("@ShowLocalImage(") Then
            ShowImage(GetRandomImage(ImageSourceType.Local), False)
            StringClean = StringClean.Replace("@ShowLocalImage", "")
        End If

        '===============================================================================
        '								@ShowLocalImage()
        '===============================================================================
        If StringClean.Contains("@ShowLocalImage(") Then
            Dim LocalFlag As String = GetParentheses(StringClean, "@ShowLocalImage(")
            LocalFlag = FixCommas(LocalFlag)

            Dim tmpListGenre As List(Of String) = LocalFlag.Split(",").ToList

            If LocalFlag.ToUpper.Contains("NOT") Then
                ' =============== Invert the Content in Brackets ===============
                ' Declare a String containing all available ImageGenres
                Dim CompareFlag As String = "Hardcore, Softcore, Lesbian, Blowjob, Femdom, Lezdom, Hentai, Gay, Maledom, Captions, General, Butts, Boobs"

                ' Remove Imagegenre, when there are no local Images available or it is in the inverting bracket
                For i As Integer = tmpListGenre.Count - 1 To 0 Step -1
                    If tmpListGenre(i).ToUpper.Contains("HARDCORE") Or Not GetImageData(ImageGenre.Hardcore).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Hardcore, ", "")
                    If tmpListGenre(i).ToUpper.Contains("SOFTCORE") Or Not GetImageData(ImageGenre.Softcore).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Softcore, ", "")
                    If tmpListGenre(i).ToUpper.Contains("LESBIAN") Or Not GetImageData(ImageGenre.Lesbian).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Lesbian, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BLOWJOB") Or Not GetImageData(ImageGenre.Blowjob).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Blowjob, ", "")
                    If tmpListGenre(i).ToUpper.Contains("FEMDOM") Or Not GetImageData(ImageGenre.Femdom).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Femdom, ", "")
                    If tmpListGenre(i).ToUpper.Contains("LEZDOM") Or Not GetImageData(ImageGenre.Lezdom).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Lezdom, ", "")
                    If tmpListGenre(i).ToUpper.Contains("HENTAI") Or Not GetImageData(ImageGenre.Hentai).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Hentai, ", "")
                    If tmpListGenre(i).ToUpper.Contains("GAY") Or Not GetImageData(ImageGenre.Gay).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Gay, ", "")
                    If tmpListGenre(i).ToUpper.Contains("MALEDOM") Or Not GetImageData(ImageGenre.Maledom).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Maledom, ", "")
                    If tmpListGenre(i).ToUpper.Contains("CAPTIONS") Or Not GetImageData(ImageGenre.Captions).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Captions, ", "")
                    If tmpListGenre(i).ToUpper.Contains("GENERAL") Or Not GetImageData(ImageGenre.General).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("General, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BUTT") Or Not GetImageData(ImageGenre.Butt).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Butts, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BUTTS") Then CompareFlag = CompareFlag.Replace("Butts, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BOOB") Or Not GetImageData(ImageGenre.Boobs).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Boobs", "")
                    If tmpListGenre(i).ToUpper.Contains("BOOBS") Then CompareFlag = CompareFlag.Replace("Boobs", "")
                Next

                ' Set the inverted Array.
                tmpListGenre = CompareFlag.Split(", ").ToList
            End If

            ' generate a list of all available Local Images. This way it is most 
            ' likely, to get an image.
            Dim tmpImageLocationList As New List(Of String)

            For Each tmpStr As String In tmpListGenre
                If tmpStr.ToUpper.Contains("HARDCORE") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Hardcore).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("SOFTCORE") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Softcore).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("LESBIAN") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Lesbian).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("BLOWJOB") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Blowjob).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("FEMDOM") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Femdom).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("LEZDOM") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Lezdom).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("HENTAI") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Hentai).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("GAY") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Gay).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("MALEDOM") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Maledom).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("CAPTION") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Captions).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("GENERAL") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.General).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("BUTT") Or tmpStr.ToUpper.Contains("BUTTS") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Butt).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("BOOB") Or tmpStr.ToUpper.Contains("BOOBS") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Boobs).ToList(ImageSourceType.Local))
                End If
            Next
            ' Declare a string for the Image to show - initialize with error Image
            Dim tmpImgToShow As String = Application.StartupPath & "\Images\System\NoLocalImagesFound.jpg"
            ' If there are images, overwrite the error image.
            If tmpImageLocationList.Count > 0 Then
                tmpImgToShow = tmpImageLocationList(New Random().Next(0, tmpImageLocationList.Count))
            Else
                Trace.WriteLine("failed to execute Command: @ShowLocalImage(" & LocalFlag & ") No images found.")
            End If

            ShowImage(tmpImgToShow, False)

            StringClean = StringClean.Replace("@ShowLocalImage(" & GetParentheses(StringClean, "@ShowLocalImage(") & ")", "")
        End If
        '----------------------------------------
        ' @ShowLocalImage()- End
        '----------------------------------------
        '===============================================================================
        '								@ShowTaggedImage
        '===============================================================================
        If StringClean.Contains("@ShowTaggedImage") Then
            Dim Tags As List(Of String) = StringClean.Split() _
                                    .Select(Function(s) s.Trim()) _
                                    .Where(Function(w) CType(w, String).StartsWith("@Tag")).ToList

            Dim FoundString As String = GetLocalImage(Tags, Nothing)

            'TODO: @ShowTaggedImage - Add a dedicated ErrorImage when there are no tagged images.
            If String.IsNullOrWhiteSpace(FoundString) Then FoundString = myPathsAccessor.PathImageErrorNoLocalImages

            ssh.JustShowedBlogImage = True
            ShowImage(FoundString, False)

            Tags.ForEach(Sub(x) StringClean = StringClean.Replace(x, ""))
            StringClean = StringClean.Replace("@ShowTaggedImage", "")
        End If
        '----------------------------------------
        ' @ShowTaggedImage - End
        '----------------------------------------
        '===============================================================================
        '									@ShowImage[]
        '===============================================================================
        If StringClean.Contains("@ShowImage[") Then
            Dim ImageToShow As String = GetParentheses(StringClean, "@ShowImage[")
            Try
                Dim tmpImgLoc As String = ""

                If IsUrl(ImageToShow) Then
                    '########################## ImageURL was given #########################
                    tmpImgLoc = ImageToShow
                    GoTo ShowedBlogImage
                End If

                ' Change evtl. wrong given Slashes
                ImageToShow = ImageToShow.Replace("/", "\")

                ImageToShow = Application.StartupPath & "\Images\" & ImageToShow
                ImageToShow = ImageToShow.Replace("\\", "\")

                If ImageToShow.Contains("*") Then
                    '######################### Directory was given #########################
                    Dim tmpFilter As String = Path.GetFileName(ImageToShow)
                    Dim tmpDir As String = Path.GetDirectoryName(ImageToShow)
                    Dim ImageList As List(Of String)

                    If Directory.Exists(tmpDir) = False Then
                        Throw New Exception(
                         "The given directory """ & tmpDir & """ does not exist." &
                         vbCrLf & vbCrLf &
                         "Please make sure the directory exists and it is spelled correctly in the script.")
                    End If

                    If tmpFilter = "*" Then
                        ImageList = myDirectory.GetFilesImages(tmpDir)
                    Else
                        ImageList = Directory.GetFiles(tmpDir, tmpFilter, SearchOption.TopDirectoryOnly).ToList
                    End If

                    If ImageList.Count = 0 Then
                        Throw New FileNotFoundException(
                         "No images matching the filter """ & tmpFilter &
                         """ were found in """ & tmpDir & """!" &
                         vbCrLf & vbCrLf &
                         "Please make sure that valid files exist and the wildcards are applied correctly in the script.")
                    End If

                    tmpImgLoc = ImageList(New Random().Next(0, ImageList.Count))
                Else
                    '############################# Single Image ############################
                    If File.Exists(ImageToShow) Then
                        tmpImgLoc = ImageToShow
                    Else
                        Throw New Exception(
                         """" & Path.GetFileName(ImageToShow) & """ was not found in """ & Path.GetDirectoryName(ImageToShow) & """!" &
                         vbCrLf & vbCrLf &
                         "Please make sure the file exists and it is spelled correctly in the script.")
                    End If
                End If
                '############### Display the Image ##################
ShowedBlogImage:
                ShowImage(tmpImgLoc, False)
            Catch ex As Exception
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                '                   All Errors
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Log.WriteError("Command @ShowImage[] was unable to display the image.",
                   ex, "Error at @ShowImage[]")
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error at @ShowImage[]")
            End Try
            StringClean = StringClean.Replace("@ShowImage[" & GetParentheses(StringClean, "@ShowImage[") & "]", "")
        End If
        '----------------------------------------
        ' @ShowImage[]- End
        '----------------------------------------
        '===============================================================================
        '								Legacy TnA-Slideshow
        '===============================================================================
        ' TODO: Rework TnA-Game to use CustomSlideshow instead of its own code.
        ' @TnAFastSlides starts a fast slideshow with Boobs and Butts. Use with local images, to avoid the download delay. otherwise the images will stutter.
        ' @TnASlides starts a slideshow with boobs and butts. the Speed is fixed at 1 image per second.
        ' @TnASlowSlides starts a slideshow with boobs and butts. the Speed is fixed at 1 image per 5 seconds.

        If StringClean.Contains("@TnAFastSlides") Or StringClean.Contains("@TnASlowSlides") Or StringClean.Contains("@TnASlides") Then
            If StringClean.Contains("@TnAFastSlides") Then TnASlides.Interval = 334
            If StringClean.Contains("@TnASlides") Then TnASlides.Interval = 1000
            If StringClean.Contains("@TnASlowSlides") Then TnASlides.Interval = 5000

            Try
                ssh.BoobList.Clear()
                ssh.AssList.Clear()

                If ssh.BoobList.Count < 1 Then ssh.BoobList = GetImageData(ImageGenre.Boobs).ToList
                If ssh.AssList.Count < 1 Then ssh.AssList = GetImageData(ImageGenre.Butt).ToList

                If ssh.BoobList.Count < 1 Then Throw New Exception("No Boobs-images found.")
                If ssh.AssList.Count < 1 Then Throw New Exception("No Butt-images found.")

                TnASlides.Start()
            Catch ex As Exception
                Log.WriteError("Unable to start TnA Slideshow: " & vbCrLf &
                      ex.Message, ex, "CommandClean()")
            End Try

            StringClean = StringClean.Replace("@TnAFastSlides", "")
            StringClean = StringClean.Replace("@TnASlowSlides", "")
            StringClean = StringClean.Replace("@TnASlides", "")
        End If

        If StringClean.Contains("@CheckTnA") Then
            TnASlides.Stop()

            If ssh.AssImage = True Then ssh.FileGoto = "(Butt)"
            If ssh.BoobImage = True Then ssh.FileGoto = "(Boobs)"
            ssh.SkipGotoLine = True
            GetGoto()
            StringClean = StringClean.Replace("@CheckTnA", "")
        End If

        If StringClean.Contains("@StopTnA") Then
            TnASlides.Stop()
            ssh.BoobList.Clear()
            ssh.BoobImage = False
            ssh.AssList.Clear()
            ssh.AssImage = False
            StringClean = StringClean.Replace("@StopTnA", "")
        End If
        '----------------------------------------
        ' TnA-Slideshow - End
        '----------------------------------------
        '===============================================================================
        '								Slideshow
        '===============================================================================
        If StringClean.Contains("@Slideshow(") Then
            Dim SlideFlag As String = StringClean

            Dim SlideStart As Integer

            SlideStart = SlideFlag.IndexOf("@Slideshow(") + 11
            SlideFlag = SlideFlag.Substring(SlideStart, SlideFlag.Length - SlideStart)
            SlideFlag = SlideFlag.Split(")")(0)
            SlideFlag = SlideFlag.Replace("@Slideshow(", "")

            ssh.CustomSlideshow.Clear()

            If SlideFlag.ToLower.Contains("hardcore") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Hardcore).ToList(), ImageGenre.Hardcore)
            End If

            If SlideFlag.ToLower.Contains("softcore") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Softcore).ToList(), ImageGenre.Softcore)
            End If

            If SlideFlag.ToLower.Contains("lesbian") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Lesbian).ToList(), ImageGenre.Lesbian)
            End If

            If SlideFlag.ToLower.Contains("blowjob") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Blowjob).ToList(), ImageGenre.Blowjob)
            End If

            If SlideFlag.ToLower.Contains("femdom") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Femdom).ToList(), ImageGenre.Femdom)
            End If

            If SlideFlag.ToLower.Contains("lezdom") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Lezdom).ToList(), ImageGenre.Lezdom)
            End If

            If SlideFlag.ToLower.Contains("hentai") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Hentai).ToList(), ImageGenre.Hentai)
            End If

            If SlideFlag.ToLower.Contains("gay") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Gay).ToList(), ImageGenre.Gay)
            End If

            If SlideFlag.ToLower.Contains("maledom") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Maledom).ToList(), ImageGenre.Maledom)
            End If

            If SlideFlag.ToLower.Contains("captions") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Captions).ToList(), ImageGenre.Captions)
            End If

            If SlideFlag.ToLower.Contains("general") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.General).ToList(), ImageGenre.General)
            End If

            If SlideFlag.ToLower.Contains("boob") Or LCase(SlideFlag).Contains("boobs") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Boobs).ToList(), ImageGenre.Boobs)
            End If

            If SlideFlag.ToLower.Contains("butt") Or LCase(SlideFlag).Contains("butts") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Butt).ToList(), ImageGenre.Butt)
            End If


            CustomSlideshowTimer.Interval = 1000
            If LCase(SlideFlag).Contains("slow") Then CustomSlideshowTimer.Interval = 5000
            If LCase(SlideFlag).Contains("fast") Then CustomSlideshowTimer.Interval = 500


            StringClean = StringClean.Replace("@Slideshow(" & SlideFlag & ")", "")
        End If

        If StringClean.Contains("@SlideshowOn") Then
            If ssh.CustomSlideshow.Count > 0 Then
                ssh.CustomSlideEnabled = True
                CustomSlideshowTimer.Start()
            End If
            StringClean = StringClean.Replace("@SlideshowOn", "")
        End If

        If StringClean.Contains("@SlideshowOff") Then
            ssh.CustomSlideEnabled = False
            CustomSlideshowTimer.Stop()
            StringClean = StringClean.Replace("@SlideshowOff", "")
        End If

        If StringClean.Contains("@SlideshowFirst") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.FirstImage, False)
            StringClean = StringClean.Replace("@SlideshowFirst", "")
        End If

        If StringClean.Contains("@SlideshowLast") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.LastImage, False)
            StringClean = StringClean.Replace("@SlideshowLast", "")
        End If

        If StringClean.Contains("@SlideshowNext") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.NextImage, False)
            StringClean = StringClean.Replace("@SlideshowNext", "")
        End If

        If StringClean.Contains("@SlideshowPrevious") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.PreviousImage, False)
            StringClean = StringClean.Replace("@SlideshowPrevious", "")
        End If

        If StringClean.Contains("@GotoSlideshow") Then
            Dim ImageString As String = ssh.CustomSlideshow.CurrentImage

            If ImageString IsNot Nothing OrElse ImageString = "" Then
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Hardcore Then ssh.FileGoto = "(Hardcore)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Softcore Then ssh.FileGoto = "(Softcore)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Lesbian Then ssh.FileGoto = "(Lesbian)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Blowjob Then ssh.FileGoto = "(Blowjob)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Femdom Then ssh.FileGoto = "(Femdom)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Lezdom Then ssh.FileGoto = "(Lezdom)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Hentai Then ssh.FileGoto = "(Hentai)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Gay Then ssh.FileGoto = "(Gay)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Maledom Then ssh.FileGoto = "(Maledom)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Captions Then ssh.FileGoto = "(Captions)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.General Then ssh.FileGoto = "(General)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Boobs Then ssh.FileGoto = "(Boobs)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Butt Then ssh.FileGoto = "(Butts)"

                ssh.SkipGotoLine = True
                GetGoto()
            Else
                Dim lazytext As String = "@GotoSlideshow can't determine the current CustomSlideshow image. Please make sure to start it before using @GotoSlideshow."
                Log.WriteError(lazytext, New NullReferenceException(lazytext), "@GotoSlideshow")
            End If

            StringClean = StringClean.Replace("@GotoSlideshow", "")
        End If
        '----------------------------------------
        ' Slideshow - End
        '----------------------------------------
        ' This Command will not work in the same line, because the Images are loaded async and not available yet.
        If StringClean.Contains("@CurrentImage") Then StringClean = StringClean.Replace("@CurrentImage", ssh.ImageLocation)

        ' The @LockImages Commnd prevents the Domme Slideshow from moving forward or back when set to "Tease" or "Timed". Manual operation of Domme Slideshow images is still allowed,
        ' and pictures displayed through other means will still work. Images are automatically unlocked whenever Tease AI moves into a Link script, an End script, any Interrupt occurs
        ' (including Long Edge and Start Stroking) or when the sub gives up.

        If StringClean.Contains("@LockImages") Then
            ssh.LockImage = True
            ImageSlideShowNextButton.Enabled = False
            ImageSlideShowPreviousButton.Enabled = False
            PicStripTSMIdommeSlideshow.Enabled = False
            StringClean = StringClean.Replace("@LockImages", "")
        End If
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        '			ImageCommands - End
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Dim gotoLocation = myFlagService.GetNextStep(domme, StringClean)
        'If (Not String.IsNullOrWhiteSpace(gotoLocation)) Then
        '    GetGoto(gotoLocation)
        'End If
        StringClean = myFlagService.DeleteGetCommand(StringClean)

TaskCleanSet:
        ' The @SetVar[] Command is used to set a Variable and store it in System\Variables. The syntax for using @SetVar[] is @SetVar[VariableName]=[Value].
        ' For example, @SetVar[MyNumber]=[12] would save the Variable "MyNumber" as a value of 12. You can also set string Variables this way, such as @SetVar[MyString]=[lasagna]
        ' Multiple @SetVar[] Commands may be used per line if you wish.
        ' Variable names CANNOT contain spaces or any character not supported by Windows file naming conventions \ / : * ? " < > |

        If StringClean.Contains("@SetVar[") Then

            Dim VarArray As String() = StringClean.Split

            For i As Integer = 0 To VarArray.Count - 1

                Dim SCGotVar As String = "NULL"

                If VarArray(i).Contains("@SetVar[") Then
                    SCGotVar = VarArray(i)
                    VarArray(i) = ""

                    SCGotVar = SCGotVar.Replace("@SetVar[", "")

                    Dim SCGotVarSplit As String() = Split(SCGotVar, "]")

                    Dim VarName As String = SCGotVarSplit(0)

                    SCGotVarSplit(0) = ""

                    SCGotVar = Join(SCGotVarSplit)

                    SCGotVar = SCGotVar.Replace("=[", "")
                    SCGotVar = SCGotVar.Replace(" ", "")

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName, SCGotVar, False)

                End If

            Next

            StringClean = Join(VarArray)

        End If

        ' The @SetDate() Command allows you to set a time and date that's a specified amount of time in the future from the current time and date. Correct format is @SetDate(VarName, TimeAmount) .
        ' For example, @SetDate(EdgingStop, 1 Hour) would set a Variable called "EdgingStop" whose value is 1 hour away from the current time and date. As another example, @SetDate(NextOrgasmChance, 2 Weeks)
        ' would create a Variable called "NextOrgasmChance" whose value is 2 weeks from the current date.
        ' The available time increments are - Seconds, Minutes, Hours, Days, Weeks, Months and Years. When designating an amount of time, capitalization and pluralization do not matter. If no increment is
        ' specified, "Days" will be used.

        If StringClean.Contains("@SetDate(") Then

            Dim CheckArray As String() = StringClean.Split(")")
            Dim OriginalCheck As String

            For i As Integer = 0 To CheckArray.Count - 1

                If CheckArray(i).Contains("@SetDate(") Then

                    'CheckArray(i) = CheckArray(i) & "]"

                    Dim CheckFlag As String = GetParentheses(CheckArray(i), "@SetDate(")
                    OriginalCheck = CheckFlag

                    CheckFlag = CheckFlag.Replace(", ", ",")
                    CheckFlag = CheckFlag.Replace(" ,", ",")

                    Dim FlagArray() As String = CheckFlag.Split(",")

                    Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)

                    If UCase(FlagArray(1)).Contains(UCase("SECOND")) Then SetDate = DateAdd(DateInterval.Second, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("MINUTE")) Then SetDate = DateAdd(DateInterval.Minute, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("HOUR")) Then SetDate = DateAdd(DateInterval.Hour, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("DAY")) Then SetDate = DateAdd(DateInterval.Day, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("WEEK")) Then SetDate = DateAdd(DateInterval.Day, Val(FlagArray(1)) * 7, SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("MONTH")) Then SetDate = DateAdd(DateInterval.Month, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Year, Val(FlagArray(1)), SetDate)

                    If Not UCase(FlagArray(1)).Contains(UCase("SECOND")) And Not UCase(FlagArray(1)).Contains(UCase("MINUTE")) And Not UCase(FlagArray(1)).Contains(UCase("HOUR")) _
                     And Not UCase(FlagArray(1)).Contains(UCase("DAY")) And Not UCase(FlagArray(1)).Contains(UCase("WEEK")) And Not UCase(FlagArray(1)).Contains(UCase("MONTH")) _
                     And Not UCase(FlagArray(1)).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Day, Val(FlagArray(1)), SetDate)

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & FlagArray(0), FormatDateTime(SetDate, DateFormat.GeneralDate), False)

                    ' CheckArray(i) = CheckArray(i).Replace("@SetDate(" & OriginalCheck, "")

                    StringClean = StringClean.Replace("@SetDate(" & OriginalCheck & ")", "")

                End If

            Next

            'StringClean = Join(CheckArray, Nothing)

        End If


        ' The @RoundVar Command is used to take an existing Variable and round it by the amount specified. The correct format is @Round[VarName]=[RoundAmount]
        ' For example, @RoundVar[StrokeTotal]=[10] wound round the Variable "StrokeTotal" by 10.
        ' @Round[] will only round the and save Variable, it will not display it. More than one @Round[] Command can be used per line


        If StringClean.Contains("@RoundVar[") Then

            Dim VarArray As String() = StringClean.Split

            For i As Integer = 0 To VarArray.Count - 1

                Dim SCGotVar As String = "NULL"

                If VarArray(i).Contains("@RoundVar[") Then
                    SCGotVar = VarArray(i)
                    VarArray(i) = ""
                End If

                SCGotVar = SCGotVar.Replace("@RoundVar[", "")

                Dim SCGotVarSplit As String() = Split(SCGotVar, "]")

                Dim VarName As String = SCGotVarSplit(0)
                Dim Val1 As Integer

                Dim VarCheck As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName


                'TODO: Remove unsecure IO.Access to file, for there is no DirectoryCheck.
                If File.Exists(VarCheck) Then
                    ' Read first line of the given file.
                    Val1 = CInt(TxtReadLine(VarCheck))

                    SCGotVarSplit(0) = ""

                    SCGotVar = Join(SCGotVarSplit)

                    SCGotVar = SCGotVar.Replace("=[", "")
                    SCGotVar = SCGotVar.Replace(" ", "")

                    Dim VarValue As Integer = Val(SCGotVar)

                    Val1 = VarValue * Math.Round(Val1 / VarValue)

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName, Val1, False)

                End If

                ' StringClean = StringClean.Replace("@RoundVar[" & OriginalCheck & ")", "")

            Next

            StringClean = Join(VarArray)

        End If

        ' The @ChangeVar[] Command is used to Value of a new or existing Variable and round it by the amount specified. The correct format is @ChangeVar[VarName]=[Value1]+[Value2]
        ' For example, @ChangeVar[StrokeTotal]=[StrokeTotal]+[100] would add 100 to the current value of "StrokeTotal" and save it. If "StrokeTotal" did not previously exist, then it would be created
        ' with a value of 100 in this case, since nothing + 100 equals 100. You can use @ChangeVar[] to add, subtract, multiply or divide with the operators +, -, * and /
        'More than one @ChangeVar[] Command can be used per line.

        If StringClean.Contains("@ChangeVar[") Then

            Dim ChangeArray As String() = StringClean.Split

            For i As Integer = 0 To ChangeArray.Count - 1

                If ChangeArray(i).Contains("@ChangeVar[") Then

                    Dim ChangeFlag As String = ChangeArray(i)
                    Dim ChangeStart As Integer = ChangeFlag.IndexOf("@ChangeVar[") + 11

                    Dim ChangeVar As String
                    Dim ChangeVal1 As String
                    Dim ChangeVal2 As String
                    Dim ChangeOperator As String

                    Dim Val1 As Integer
                    Dim Val2 As Integer

                    ChangeFlag = ChangeArray(i).Substring(ChangeStart, ChangeArray(i).Length - ChangeStart)
                    ChangeVar = ChangeFlag.Split("]")(0)
                    ChangeVal1 = ChangeFlag.Split("]")(1)
                    ChangeVal2 = ChangeFlag.Split("]")(2)
                    ChangeOperator = ChangeFlag.Split("]")(2)

                    ChangeArray(i) = ChangeArray(i).Replace("@ChangeVar[" & ChangeVar & "]" & ChangeVal1 & "]" & ChangeVal2 & "]", "")

                    ChangeVar = ChangeVar.Replace("@ChangeVar[", "")
                    ChangeVal1 = ChangeVal1.Replace("=[", "")
                    ChangeVal2 = ChangeVal2.Replace("+[", "")
                    ChangeVal2 = ChangeVal2.Replace("-[", "")
                    ChangeVal2 = ChangeVal2.Replace("*[", "")
                    ChangeVal2 = ChangeVal2.Replace("/[", "")

                    '@ChangeVar[TB_EdgeHoldingOwed   ]    =[TB_EdgeHoldingOwed    ]     -[1       ]

                    If IsNumeric(ChangeVal1) = False Then
                        'TODO: Remove unsecure IO.Access to file, for there is no DirectoryCheck.
                        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal1) Then
                            Val1 = TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal1)
                        Else
                            Val1 = 0
                        End If
                    Else
                        Val1 = Val(ChangeVal1)
                    End If

                    If IsNumeric(ChangeVal2) = False Then
                        'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
                        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal2) Then
                            Val2 = TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal2)
                        Else
                            Val2 = 0
                        End If
                    Else
                        Val2 = Val(ChangeVal2)
                    End If

                    ssh.ScriptOperator = "Null"
                    If ChangeOperator.Contains("+") Then ssh.ScriptOperator = "Add"
                    If ChangeOperator.Contains("-") Then ssh.ScriptOperator = "Subtract"
                    If ChangeOperator.Contains("*") Then ssh.ScriptOperator = "Multiply"
                    If ChangeOperator.Contains("/") Then ssh.ScriptOperator = "Divide"

                    Dim ChangeVal As Integer = 0

                    If ssh.ScriptOperator = "Add" Then ChangeVal = Val1 + Val2
                    If ssh.ScriptOperator = "Subtract" Then ChangeVal = Val1 - Val2
                    If ssh.ScriptOperator = "Multiply" Then ChangeVal = Val1 * Val2
                    If ssh.ScriptOperator = "Divide" Then ChangeVal = Val1 / Val2

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVar, ChangeVal, False)

                End If

            Next

        End If

        ' The @ShowVar[] Command is used to show the value of an existing Variable. The correct format is @ShowVar[VarName]
        ' More than one @ShowVar[] Commands can be used per line

        If StringClean.Contains("@ShowVar[") Then

            Dim VarSplit As String() = StringClean.Split("]")

            For i As Integer = 0 To VarSplit.Count - 1

                If VarSplit(i).Contains("@ShowVar[") Then

                    Dim VarString As String = VarSplit(i) & "]"

                    Dim VarFlag As String = GetParentheses(VarString, "@ShowVar[")
                    Dim VarFlag2 As String = GetVariable(VarFlag)
                    ' StringClean = StringClean.Replace("#Var[" & VarFlag & "]", VarFlag2)

                    StringClean = StringClean.Replace("@ShowVar[" & VarFlag & "]", VarFlag2)

                End If

            Next

        End If

        If StringClean.Contains("@RemoveTokens(") Then

            Dim TokenFlag As String = GetParentheses(StringClean, "@RemoveTokens(")
            TokenFlag = FixCommas(TokenFlag)
            Dim TokenRemove As Integer

            If TokenFlag.Contains(",") Then
                Dim TokenArray As String() = TokenFlag.Split(",")
                For i As Integer = 0 To TokenArray.Count - 1
                    TokenRemove = Val(TokenArray(i))
                    If UCase(TokenArray(i)).Contains("B") Then ssh.BronzeTokens -= TokenRemove
                    If UCase(TokenArray(i)).Contains("S") Then ssh.SilverTokens -= TokenRemove
                    If UCase(TokenArray(i)).Contains("G") Then ssh.GoldTokens -= TokenRemove
                Next
            Else
                TokenRemove = Val(TokenFlag)
                If UCase(TokenFlag).Contains("B") Then ssh.BronzeTokens -= TokenRemove
                If UCase(TokenFlag).Contains("S") Then ssh.SilverTokens -= TokenRemove
                If UCase(TokenFlag).Contains("G") Then ssh.GoldTokens -= TokenRemove
            End If

            If ssh.BronzeTokens < 0 Then ssh.BronzeTokens = 0
            If ssh.SilverTokens < 0 Then ssh.SilverTokens = 0
            If ssh.GoldTokens < 0 Then ssh.GoldTokens = 0

            My.Settings.BronzeTokens = ssh.BronzeTokens
            My.Settings.SilverTokens = ssh.SilverTokens
            My.Settings.GoldTokens = ssh.GoldTokens


            StringClean = StringClean.Replace("@RemoveTokens(" & TokenFlag & ")", "")

        End If

        If StringClean.Contains("@Add1Token") Then
            ssh.BronzeTokens += 1
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 1 Bronze token!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@Add1Token", "")
        End If

        If StringClean.Contains("@Add3Tokens") Then
            ssh.BronzeTokens += 3
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 3 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@Add3Tokens", "")
        End If

        If StringClean.Contains("@Add5Tokens") Then
            ssh.BronzeTokens += 5
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            StringClean = StringClean.Replace("@Add5Tokens", "")
            MessageBox.Show(Me, domName.Text & " has given you 5 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If StringClean.Contains("@Add10Tokens") Then
            ssh.BronzeTokens += 10
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 10 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@Add10Tokens", "")
        End If

        If StringClean.Contains("@Add25Tokens") Then
            ssh.BronzeTokens += 25
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 25 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@Add25Tokens", "")
        End If

        If StringClean.Contains("@Add50Tokens") Then
            ssh.BronzeTokens += 50
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 50 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@Add50Tokens", "")
        End If

        If StringClean.Contains("@Add100Tokens") Then
            ssh.BronzeTokens += 100
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 100 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@Add50Tokens", "")
        End If

        If StringClean.Contains("@Remove100Tokens") Then
            ssh.BronzeTokens -= 100
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has taken 100 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StringClean = StringClean.Replace("@@Remove100Tokens", "")
        End If


        If StringClean.Contains("@UpdateOrgasm") Then
            My.Settings.LastOrgasm = FormatDateTime(Now, DateFormat.ShortDate)

            'Github Patch
            If My.Settings.OrgasmsLocked = True Then My.Settings.OrgasmsRemaining -= 1

            FrmSettings.LBLLastOrgasm.Text = My.Settings.LastOrgasm
            StringClean = StringClean.Replace("@UpdateOrgasm", "")
        End If

        If StringClean.Contains("@UpdateRuined") Then
            My.Settings.LastRuined = FormatDateTime(Now, DateFormat.ShortDate)

            ' GithubPatch
            If My.Settings.OrgasmsLocked = True Then My.Settings.OrgasmsRemaining -= 1

            FrmSettings.LBLLastRuined.Text = My.Settings.LastRuined
            StringClean = StringClean.Replace("@UpdateRuined", "")
        End If

        If StringClean.Contains("@DeleteVar[") Then

            Dim DeleteArray As String() = StringClean.Split("]")

            For i As Integer = 0 To DeleteArray.Count - 1

                If DeleteArray(i).Contains("@DeleteVar[") Then

                    DeleteArray(i) = DeleteArray(i) & "]"

                    Dim DFlag As String = GetParentheses(DeleteArray(i), "@DeleteVar[")
                    Dim OriginalDelete As String = DFlag

                    If DFlag.Contains(",") Then

                        DFlag = FixCommas(DFlag)

                        Dim FlagArray() As String = DFlag.Split(",")

                        For x As Integer = 0 To FlagArray.Count - 1

                            DeleteVariable(FlagArray(x))

                        Next

                    Else

                        DeleteVariable(DFlag)

                    End If

                    'DeleteArray(i) = DeleteArray(i).Replace("@DeleteVar[" & OriginalDelete & "]", "")

                    StringClean = StringClean.Replace("@DeleteVar[" & OriginalDelete & "]", "")

                End If

            Next
            'StringClean = Join(DeleteArray, Nothing)
        End If


        If StringClean.Contains(Keyword.PornAllowedOff) Then
            myFlagAccessor.SetFlag(CreateDommePersonality(), "SYS_NoPornAllowed", False)
            StringClean = StringClean.Replace(Keyword.PornAllowedOff, "")
        End If

        If StringClean.Contains(Keyword.PornAllowedOn) Then
            myFlagAccessor.DeleteFlag(CreateDommePersonality(), "SYS_NoPornAllowed")
            StringClean = StringClean.Replace(Keyword.PornAllowedOn, "")
        End If

        If StringClean.Contains("@RestrictOrgasm(") Then

            Dim CheckFlag As String = GetParentheses(StringClean, "@RestrictOrgasm(")

            If CheckFlag.Contains(",") Then

                CheckFlag = CheckFlag.Replace(", ", ",")
                CheckFlag = CheckFlag.Replace(" ,", ",")

                Dim FlagArray() As String = CheckFlag.Split(",")

                Dim Seconds1 As Integer = Val(FlagArray(0))
                Dim Seconds2 As Integer = Val(FlagArray(1))

                If UCase(FlagArray(0)).Contains(UCase("MINUTE")) Then Seconds1 *= 60
                If UCase(FlagArray(0)).Contains(UCase("HOUR")) Then Seconds1 *= 3600
                If UCase(FlagArray(0)).Contains(UCase("DAY")) Then Seconds1 *= 86400
                If UCase(FlagArray(0)).Contains(UCase("WEEK")) Then Seconds1 *= 604800
                If UCase(FlagArray(0)).Contains(UCase("MONTH")) Then Seconds1 *= 2419200
                If UCase(FlagArray(0)).Contains(UCase("YEAR")) Then Seconds1 *= 29030400

                If UCase(FlagArray(1)).Contains(UCase("MINUTE")) Then Seconds2 *= 60
                If UCase(FlagArray(1)).Contains(UCase("HOUR")) Then Seconds2 *= 3600
                If UCase(FlagArray(1)).Contains(UCase("DAY")) Then Seconds2 *= 86400
                If UCase(FlagArray(1)).Contains(UCase("WEEK")) Then Seconds2 *= 604800
                If UCase(FlagArray(1)).Contains(UCase("MONTH")) Then Seconds2 *= 2419200
                If UCase(FlagArray(1)).Contains(UCase("YEAR")) Then Seconds2 *= 29030400

                Dim TotalSeconds As Integer = ssh.randomizer.Next(Seconds1, Seconds2 + 1)

                Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)

                SetDate = DateAdd(DateInterval.Second, TotalSeconds, SetDate)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_OrgasmRestricted", FormatDateTime(SetDate, DateFormat.GeneralDate), False)

            Else

                Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)

                If UCase(CheckFlag).Contains(UCase("SECOND")) Then SetDate = DateAdd(DateInterval.Second, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("MINUTE")) Then SetDate = DateAdd(DateInterval.Minute, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("HOUR")) Then SetDate = DateAdd(DateInterval.Hour, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("DAY")) Then SetDate = DateAdd(DateInterval.Day, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("WEEK")) Then SetDate = DateAdd(DateInterval.Day, Val(CheckFlag) * 7, SetDate)
                If UCase(CheckFlag).Contains(UCase("MONTH")) Then SetDate = DateAdd(DateInterval.Month, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Year, Val(CheckFlag), SetDate)

                If Not UCase(CheckFlag).Contains(UCase("SECOND")) And Not UCase(CheckFlag).Contains(UCase("MINUTE")) And Not UCase(CheckFlag).Contains(UCase("HOUR")) _
                 And Not UCase(CheckFlag).Contains(UCase("DAY")) And Not UCase(CheckFlag).Contains(UCase("WEEK")) And Not UCase(CheckFlag).Contains(UCase("MONTH")) _
                 And Not UCase(CheckFlag).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Day, Val(CheckFlag), SetDate)

                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_OrgasmRestricted", FormatDateTime(SetDate, DateFormat.GeneralDate), False)

            End If
            ssh.OrgasmRestricted = True
            StringClean = StringClean.Replace("@RestrictOrgasm(" & GetParentheses(StringClean, "@RestrictOrgasm(") & ")", "")
        End If

        If StringClean.Contains("@RestrictOrgasm") Then
            ssh.OrgasmRestricted = True
            StringClean = StringClean.Replace("@RestrictOrgasm", "")
        End If

        If StringClean.Contains("@DecreaseRuinChance") Then

            If FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Never Ruins"
            If FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins"
            If FrmSettings.ruinorgasmComboBox.Text = "Often Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins"
            If FrmSettings.ruinorgasmComboBox.Text = "Always Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Often Ruins"

            My.Settings.OrgasmRuin = FrmSettings.ruinorgasmComboBox.Text

            StringClean = StringClean.Replace("@DecreaseRuinChance", "")
        End If

        If StringClean.Contains("@IncreaseRuinChance") Then

            If FrmSettings.ruinorgasmComboBox.Text = "Often Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Always Ruins"
            If FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Often Ruins"
            If FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins"
            If FrmSettings.ruinorgasmComboBox.Text = "Never Ruins" Then FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins"

            My.Settings.OrgasmRuin = FrmSettings.ruinorgasmComboBox.Text

            StringClean = StringClean.Replace("@IncreaseRuinChance", "")
        End If

        '@@@@@@@@@@@@@@@@@@@@@@ TASKCLEAN END

        If TaskClean = True Then Return StringClean


        ' The @CheckDate() Command checks a previously saved Variable created with the @SetDate() Command and goes to the specified line if the current time and date is on or after the date in the Variable.
        ' Correct format is @CheckDate(VarName, Goto Line) . For example, @CheckDate(NoPorn, Look At Porn Again) will go to the line (Look At Porn Again) if the current time and date has passed the value set
        ' for the Variable "NoPorn" by @SetDate()

        If StringClean.Contains("@CheckDate(") Then

            Dim CheckArray As String() = StringClean.Split(")")

            For i As Integer = 0 To CheckArray.Count - 1

                If CheckArray(i).Contains("@CheckDate(") Then

                    If CheckDateList(CheckArray(i), True) = True Then
                        Dim DateFlag As String = GetParentheses(CheckArray(i), "@CheckDate(")
                        DateFlag = FixCommas(DateFlag)
                        Dim DateArray As String() = DateFlag.Split(",")
                        ssh.SkipGotoLine = True
                        ssh.FileGoto = DateArray(DateArray.Count - 1).Replace(")", "")
                        GetGoto()
                    End If

                    StringClean = StringClean.Replace("@CheckDate(" & GetParentheses(CheckArray(i), "@CheckDate(") & ")", "")

                End If

            Next

        End If

        ' The @If[] Command allows you to compare Variables and go to a specific line if the statement is true. The correct format is @If[VarName]>[varName2]Then(Goto Line)
        ' For example, If[StrokeTotal]>[1000]Then(Thousand Strokes) would check if the Variable "StrokeTotal" is greater than 1000, and go to (Thousand Strokes) if so. 
        ' The @If[] Command can compare any combination of Variables and numeric values with = (or ==), <>, >, <, >= and <= . String Variables can be compared with = (or ==) and <> 
        ' More than one @If[] Command can be used per line. Tease AI will move to the line specified by whichever true statement happened last in the line.

        If StringClean.Contains("@If[") Then

            Do

                Dim SCIfVar As String() = Split(StringClean)
                Dim SCGotVar As String = "Null"

                For i As Integer = 0 To SCIfVar.Length - 1
                    If SCIfVar(i).Contains("@If[") Then
                        Dim IFJoin As Integer = 0
                        If Not SCIfVar(i).Contains(")") Then
                            Do
                                IFJoin += 1
                                SCIfVar(i) = SCIfVar(i) & " " & SCIfVar(i + IFJoin)
                                SCIfVar(i + IFJoin) = ""
                            Loop Until SCIfVar(i).Contains(")")
                        End If
                        SCGotVar = SCIfVar(i)
                        SCIfVar(i) = ""
                        StringClean = Join(SCIfVar)
                        Do
                            StringClean = StringClean.Replace("  ", " ")
                        Loop Until Not StringClean.Contains("  ")
                        Exit For
                    End If
                Next

                If SCGotVar.Contains("]And[") Then

                    Dim AndCheck As Boolean = True

                    For x As Integer = 0 To SCGotVar.Replace("]And[", "").Count - 1
                        If GetIf("[" & GetParentheses(SCGotVar, "@If[", 2) & "]") = False Then
                            AndCheck = False
                            Exit For
                        End If
                        SCGotVar = SCGotVar.Replace("[" & GetParentheses(SCGotVar, "@If[", 2) & "]And", "")
                    Next

                    If AndCheck = True Then
                        ssh.FileGoto = GetParentheses(SCGotVar, "Then(")
                        ssh.SkipGotoLine = True
                        GetGoto()
                    End If

                ElseIf SCGotVar.Contains("]Or[") Then

                    Dim OrCheck As Boolean = False

                    For x As Integer = 0 To SCGotVar.Replace("]Or[", "").Count - 1
                        If GetIf("[" & GetParentheses(SCGotVar, "@If[", 2) & "]") = True Then
                            OrCheck = True
                            Exit For
                        End If
                        SCGotVar = SCGotVar.Replace("[" & GetParentheses(SCGotVar, "@If[", 2) & "]Or", "")
                    Next

                    If OrCheck = True Then
                        ssh.FileGoto = GetParentheses(SCGotVar, "Then(")
                        ssh.SkipGotoLine = True
                        GetGoto()
                    End If

                Else

                    If GetIf("[" & GetParentheses(SCGotVar, "@If[", 2) & "]") = True Then
                        ssh.FileGoto = GetParentheses(SCGotVar, "Then(")
                        ssh.SkipGotoLine = True
                        GetGoto()
                    End If

                End If

            Loop Until Not StringClean.Contains("@If")

        End If

        ' The @InputVar[] stops script progression and waits for the user to input his next message. Whatever the user types next will be saved as a Variable named whatever you specify in the brackets.
        ' For example, if the script's line was "What's your favorite food? @InputVar[FavoriteFood]", and the user typed "lo mein", then "lo mein" would be saved as the Variable "FavoriteFood". If the
        ' user has checked "Show Icon During Input Questions" in the General Settings tab, then the domme's question will be accompanied by a small question mark icon to let the user know that their next
        ' response will be saved verbatim. @InputVar[] will pause Linear Scripts, as well as countdowns and taunts for Stroking, Edging and Holding The Edge.

        If StringClean.Contains("@InputVar[") Then

            ssh.InputString = GetParentheses(StringClean, "@InputVar[").Replace("]", "")
            ssh.InputFlag = True
            If FrmSettings.CBInputIcon.Checked = True Then ssh.InputIcon = True

            StringClean = StringClean.Replace("@InputVar[" & ssh.InputString & "]", "")

        End If


        ' The @DislikeBlogImage Command takes the URL of the most recently viewed blog image and adds it to the "Disliked" list located in [Tease AI Root Directory]\Images\System\DislikedImageURLS.txt

        If StringClean.Contains("@DislikeBlogImage") Then

            If ssh.ImageLocation <> "" Then

                If File.Exists(Application.StartupPath & "\Images\System\DislikedImageURLs.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\DislikedImageURLs.txt", Environment.NewLine & ssh.ImageLocation, True)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\DislikedImageURLs.txt", ssh.ImageLocation, True)
                End If
                StringClean = StringClean.Replace("@DislikeBlogImage", "")
            End If

        End If

        ' The @LikeBlogImage Command takes the URL of the most recently viewed blog image and adds it to the "Liked" list located in [Tease AI Root Directory]\Images\System\LikedImageURLS.txt

        If StringClean.Contains("@LikeBlogImage") Then

            If ssh.ImageLocation <> "" Then

                If File.Exists(Application.StartupPath & "\Images\System\LikedImageURLs.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\LikedImageURLs.txt", Environment.NewLine & ssh.ImageLocation, True)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\LikedImageURLs.txt", ssh.ImageLocation, True)
                End If
                StringClean = StringClean.Replace("@LikeBlogImage", "")
            End If

        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┌─┐┌─┐┌┬┐┌─┐┬─┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╠╣ ├─┤└─┐ │ ├┤ ├┬┘
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚  ┴ ┴└─┘ ┴ └─┘┴└─

        If StringClean.Contains("@StrokeFaster") Then
            ssh.StrokeFaster = True
            StringClean = StringClean.Replace("@StrokeFaster", "")
        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┬  ┌─┐┬ ┬┌─┐┬─┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╚═╗│  │ ││││├┤ ├┬┘
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚═╝┴─┘└─┘└┴┘└─┘┴└─

        If StringClean.Contains("@StrokeSlower") Then
            ssh.StrokeSlower = True
            StringClean = StringClean.Replace("@StrokeSlower", "")
        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┌─┐┌─┐┌┬┐┌─┐┌─┐┌┬┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╠╣ ├─┤└─┐ │ ├┤ └─┐ │ 
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚  ┴ ┴└─┘ ┴ └─┘└─┘ ┴ 

        If StringClean.Contains("@StrokeFastest") Then
            ssh.StrokeFastest = True
            StringClean = StringClean.Replace("@StrokeFastest", "")
        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┬  ┌─┐┬ ┬┌─┐┌─┐┌┬┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╚═╗│  │ ││││├┤ └─┐ │ 
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚═╝┴─┘└─┘└┴┘└─┘└─┘ ┴ 

        If StringClean.Contains("@StrokeSlowest") Then
            ssh.StrokeSlowest = True
            StringClean = StringClean.Replace("@StrokeSlowest", "")
        End If


        If StringClean.Contains("@StartStroking") Then

            If Not File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_FirstRun") Then
                Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_FirstRun", SetDate, False)
            End If

            SetVariable("SYS_StrokeRound", Val(GetVariable("SYS_StrokeRound")) + 1)

            If FrmSettings.TBWebStart.Text <> "" Then
                Try
                    FrmSettings.WebToy.Navigate(FrmSettings.TBWebStart.Text)
                Catch
                End Try
            End If
            If StringClean.Contains("@Contact1") Then ssh.Contact1Stroke = True
            If StringClean.Contains("@Contact2") Then ssh.Contact2Stroke = True
            If StringClean.Contains("@Contact3") Then ssh.Contact3Stroke = True
            ssh.AskedToGiveUpSection = False
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.BeforeTease = False
            ssh.SubStroking = True
            ssh.ShowModule = False
            'StrokeCycle = -1
            If ssh.StartStrokingCount = 0 Then ssh.FirstRound = True
            'If FirstRound = True Then My.Settings.Sys_SubLeftEarly += 1
            If ssh.FirstRound = True Then SetVariable("SYS_SubLeftEarly", Val(GetVariable("SYS_SubLeftEarly")) + 1)
            ssh.StartStrokingCount += 1
            StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
            StrokePace = 50 * Math.Round(StrokePace / 50)

            If ssh.WorshipMode = True Then
                StrokePace = NBMinPace.Value
                ssh.StrokeSlowest = True
            End If

            ClearModes()

            If FrmSettings.CBTauntCycleDD.Checked = True Then
                If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick = ssh.randomizer.Next(1, 3) * 60
                If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick = ssh.randomizer.Next(1, 4) * 60
                If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick = ssh.randomizer.Next(3, 6) * 60
                If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick = ssh.randomizer.Next(4, 8) * 60
                If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick = ssh.randomizer.Next(5, 11) * 60

                If ssh.WorshipMode = True Then
                    If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick = 180
                    If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick = 240
                    If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick = 360
                    If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick = 480
                    If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick = 600
                End If

            Else
                ssh.StrokeTick = ssh.randomizer.Next(FrmSettings.NBTauntCycleMin.Value * 60, FrmSettings.NBTauntCycleMax.Value * 60)
                If ssh.WorshipMode = True Then ssh.StrokeTick = FrmSettings.NBTauntCycleMax.Value * 60
            End If



            ssh.StrokeTauntTick = ssh.randomizer.Next(11, 21)
            'StrokeThread = New Thread(AddressOf StrokeLoop)
            'StrokeThread.IsBackground = True
            'StrokeThread.SetApartmentState(ApartmentState.STA)
            'StrokeThread.Start()
            StrokeTimer.Start()
            StrokeTauntTimer.Start()
            StringClean = StringClean.Replace("@StartStroking", "")
        End If

        If StringClean.Contains("@StartTaunts") Then
            ssh.AskedToGiveUpSection = False
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.BeforeTease = False
            ssh.SubStroking = True
            ssh.ShowModule = False
            'StrokeCycle = -1
            If ssh.StartStrokingCount = 0 Then ssh.FirstRound = True
            ssh.StartStrokingCount += 1
            ' github patch StrokePace = 0
            ' github patch StrokePaceTimer.Interval = StrokePace

            ClearModes()

            If FrmSettings.CBTauntCycleDD.Checked = True Then
                If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick = ssh.randomizer.Next(1, 3) * 60
                If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick = ssh.randomizer.Next(1, 4) * 60
                If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick = ssh.randomizer.Next(3, 6) * 60
                If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick = ssh.randomizer.Next(4, 8) * 60
                If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick = ssh.randomizer.Next(5, 11) * 60
            Else
                ssh.StrokeTick = ssh.randomizer.Next(FrmSettings.NBTauntCycleMin.Value * 60, FrmSettings.NBTauntCycleMax.Value * 60)
            End If
            ssh.StrokeTauntTick = ssh.randomizer.Next(11, 21)
            StrokeTimer.Start()
            StrokeTauntTimer.Start()
            StringClean = StringClean.Replace("@StartTaunts", "")
        End If

        If StringClean.Contains("@StopStroking") Then
            If FrmSettings.TBWebStop.Text <> "" Then
                Try
                    FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text)
                Catch
                End Try
            End If
            If ssh.Contact1Stroke = True Then
                StringClean = StringClean & "@Contact1"
                ssh.Contact1Stroke = False
            End If
            If ssh.Contact2Stroke = True Then
                StringClean = StringClean & "@Contact2"
                ssh.Contact2Stroke = False
            End If
            If ssh.Contact3Stroke = True Then
                StringClean = StringClean & "@Contact3"
                ssh.Contact3Stroke = False
            End If
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.SubStroking = False
            ssh.SubEdging = False
            ssh.SubHoldingEdge = False
            ssh.WorshipMode = False
            ssh.WorshipTarget = ""
            ssh.LongHold = False
            ssh.ExtremeHold = False
            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            StrokePace = 0
            EdgeTauntTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            StringClean = StringClean.Replace("@StopStroking", "")
        End If

        If StringClean.Contains("@StopTaunts") Then
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.SubStroking = False
            ssh.SubEdging = False
            ssh.SubHoldingEdge = False
            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            EdgeTauntTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            StringClean = StringClean.Replace("@StopTaunts", "")
        End If


        If StringClean.Contains("@LongHold(") Then
            Dim HoldInt As Integer = Val(GetParentheses(StringClean, "@LongHold("))
            ssh.TempVal = ssh.randomizer.Next(0, 101)
            If ssh.TempVal <= HoldInt Then ssh.LongHold = True

            StringClean = StringClean.Replace("@LongHold(" & GetParentheses(StringClean, "@LongHold(") & ")", "")
        End If

        If StringClean.Contains("@ExtremeHold(") Then
            Dim HoldInt As Integer = Val(GetParentheses(StringClean, "@ExtremeHold("))
            ssh.TempVal = ssh.randomizer.Next(0, 101)
            If ssh.TempVal <= HoldInt Then ssh.ExtremeHold = True

            StringClean = StringClean.Replace("@ExtremeHold(" & GetParentheses(StringClean, "@ExtremeHold(") & ")", "")
        End If

        If StringClean.Contains("@LongHold") Then
            ssh.LongHold = True
            StringClean = StringClean.Replace("@LongHold", "")
        End If

        If StringClean.Contains("@ExtremeHold") Then
            ssh.ExtremeHold = True
            StringClean = StringClean.Replace("@ExtremeHold", "")
        End If

        If StringClean.Contains("@MultipleEdges(") Then

            If StringClean.Contains("@Edg") Then

                Dim EdgeFlag As String = GetParentheses(StringClean, "@MultipleEdges(")
                EdgeFlag = FixCommas(EdgeFlag)
                Dim EdgeArray As String() = EdgeFlag.Split(",")

                If EdgeArray.Count = 3 Then

                    If ssh.randomizer.Next(1, 101) < Val(EdgeArray(2)) Then
                        ssh.MultipleEdges = True
                        ssh.MultipleEdgesAmount = Val(EdgeArray(0))
                        ssh.MultipleEdgesInterval = Val(EdgeArray(1))
                    End If

                Else

                    ssh.MultipleEdges = True
                    ssh.MultipleEdgesAmount = Val(EdgeArray(0))
                    ssh.MultipleEdgesInterval = Val(EdgeArray(1))

                End If

            End If

            StringClean = StringClean.Replace("@MultipleEdges(" & GetParentheses(StringClean, "@MultipleEdges(") & ")", "")

        End If


        If StringClean.Contains("@Edge(") Then

            ContactEdgeCheck(StringClean)

            Edge()

            If GetMatch(StringClean, "@Edge(", "Hold") = True Then ssh.EdgeHold = True
            If GetMatch(StringClean, "@Edge(", "NoHold") = True Then ssh.EdgeNoHold = True
            If ssh.EdgeHold = True And ssh.EdgeNoHold = True Then ssh.EdgeHold = False

            If GetMatch(StringClean, "@Edge(", "Deny") = True Then ssh.OrgasmDenied = True

            If GetMatch(StringClean, "@Edge(", "Orgasm") = True Then ssh.OrgasmAllowed = True

            If GetMatch(StringClean, "@Edge(", "Ruin") = True Then ssh.OrgasmRuined = True

            If ssh.OrgasmAllowed = True And ssh.OrgasmRuined = True Then ssh.OrgasmRuined = False

            If GetMatch(StringClean, "@Edge(", "RuinTaunts") = True Then
                If ssh.EdgeToRuin = True Then ssh.EdgeToRuinSecret = False
            End If

            If GetMatch(StringClean, "@Edge(", "LongHold") = True Then
                ssh.EdgeHold = True
                ssh.LongHold = True
            End If
            If GetMatch(StringClean, "@Edge(", "ExtremeHold") = True Then
                ssh.EdgeHold = True
                ssh.ExtremeHold = True
            End If
            If GetMatch(StringClean, "@Edge(", "HoldTaunts") = True Then
                If ssh.LongHold = True Then ssh.LongTaunts = True
            End If

        End If



        If StringClean.Contains("@EdgeMode(") Then

            Dim EdgeFlag As String = GetParentheses(StringClean, "@EdgeMode(")
            EdgeFlag = FixCommas(EdgeFlag)
            Dim EdgeArray As String() = EdgeFlag.Split(",")

            If UCase(EdgeArray(0)).Contains("GOTO") Then
                ssh.EdgeGoto = True
                ssh.EdgeGotoLine = EdgeArray(1)
            End If

            If UCase(EdgeArray(0)).Contains("MESSAGE") Then
                ssh.EdgeMessage = True
                ssh.EdgeMessageText = EdgeArray(1)
            End If

            If UCase(EdgeArray(0)).Contains("VIDEO") Then
                ssh.EdgeVideo = True
                ssh.EdgeGotoLine = EdgeArray(1)
            End If

            If UCase(EdgeArray(0)).Contains("NORMAL") Then
                ssh.EdgeGoto = False
                ssh.EdgeMessage = False
                ssh.EdgeVideo = False
            End If

            StringClean = StringClean.Replace("@EdgeMode(" & GetParentheses(StringClean, "@EdgeMode(") & ")", "")
        End If

        If StringClean.Contains("@EdgeToRuinNoHoldNoSecret") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeToRuin = True
            ssh.EdgeToRuinSecret = False
            StringClean = StringClean.Replace("@EdgeToRuinNoHoldNoSecret", "")
        End If

        If StringClean.Contains("@EdgeToRuinHoldNoSecret(") Then

            Dim EdgeHoldFlag As String = GetParentheses(StringClean, "@EdgeToRuinHoldNoSecret(")

            If EdgeHoldFlag.Contains(",") Then

                EdgeHoldFlag = FixCommas(EdgeHoldFlag)

                Dim EdgeFlagArray As String() = EdgeHoldFlag.Split(",")

                Dim Edge1 As Integer = Val(EdgeFlagArray(0))
                Dim Edge2 As Integer = Val(EdgeFlagArray(1))

                If UCase(EdgeFlagArray(0)).Contains("M") Then Edge1 *= 60
                If UCase(EdgeFlagArray(1)).Contains("M") Then Edge2 *= 60

                If UCase(EdgeFlagArray(0)).Contains("H") Then Edge1 *= 3600
                If UCase(EdgeFlagArray(1)).Contains("H") Then Edge2 *= 3600

                ssh.EdgeHoldSeconds = ssh.randomizer.Next(Edge1, Edge2 + 1)

            Else

                ssh.EdgeHoldSeconds = Val(EdgeHoldFlag)
                If UCase(GetParentheses(StringClean, "@EdgeToRuinHoldNoSecret(")).Contains("M") Then ssh.EdgeHoldSeconds *= 60
                If UCase(GetParentheses(StringClean, "@EdgeToRuinHoldNoSecret(")).Contains("H") Then ssh.EdgeHoldSeconds *= 3600

            End If

            EdgeHoldFlag = True

            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True
            ssh.EdgeToRuinSecret = False
            StringClean = StringClean.Replace("@EdgeToRuinHoldNoSecret(" & GetParentheses(StringClean, "@EdgeToRuinHoldNoSecret(") & ")", "")
        End If



        If StringClean.Contains("@EdgeToRuinHoldNoSecret") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True
            ssh.EdgeToRuinSecret = False
            StringClean = StringClean.Replace("@EdgeToRuinHoldNoSecret", "")
        End If

        If StringClean.Contains("@EdgeToRuinNoSecret") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeToRuinSecret = False
            ssh.EdgeToRuin = True
            StringClean = StringClean.Replace("@EdgeToRuinNoSecret", "")
        End If

        If StringClean.Contains("@EdgeToRuinNoHold") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeNoHold = True
            ssh.EdgeToRuin = True
            StringClean = StringClean.Replace("@EdgeToRuinNoHold", "")
        End If

        If StringClean.Contains("@EdgeToRuinHold(") Then

            Dim EdgeHoldFlag As String = GetParentheses(StringClean, "@EdgeToRuinHold(")

            If EdgeHoldFlag.Contains(",") Then

                EdgeHoldFlag = FixCommas(EdgeHoldFlag)

                Dim EdgeFlagArray As String() = EdgeHoldFlag.Split(",")

                Dim Edge1 As Integer = Val(EdgeFlagArray(0))
                Dim Edge2 As Integer = Val(EdgeFlagArray(1))

                If UCase(EdgeFlagArray(0)).Contains("M") Then Edge1 *= 60
                If UCase(EdgeFlagArray(1)).Contains("M") Then Edge2 *= 60

                If UCase(EdgeFlagArray(0)).Contains("H") Then Edge1 *= 3600
                If UCase(EdgeFlagArray(1)).Contains("H") Then Edge2 *= 3600

                ssh.EdgeHoldSeconds = ssh.randomizer.Next(Edge1, Edge2 + 1)

            Else

                ssh.EdgeHoldSeconds = Val(EdgeHoldFlag)
                If UCase(GetParentheses(StringClean, "@EdgeToRuinHold(")).Contains("M") Then ssh.EdgeHoldSeconds *= 60
                If UCase(GetParentheses(StringClean, "@EdgeToRuinHold(")).Contains("H") Then ssh.EdgeHoldSeconds *= 3600

            End If

            EdgeHoldFlag = True

            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True

            StringClean = StringClean.Replace("@EdgeToRuinHold(" & GetParentheses(StringClean, "@EdgeToRuinHold(") & ")", "")
        End If

        If StringClean.Contains("@EdgeToRuinHold") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True
            StringClean = StringClean.Replace("@EdgeToRuinHold", "")
        End If

        If StringClean.Contains("@EdgeToRuin") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeToRuin = True
            StringClean = StringClean.Replace("@EdgeToRuin", "")
        End If

        If StringClean.Contains("@EdgeNoHold") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeNoHold = True
            StringClean = StringClean.Replace("@EdgeNoHold", "")
        End If


        ' The Commands @EdgeHold(), @EdgeToRuinHold() and @EdgeToRuinHoldNoSecret() allow you to specify the amount of time the edge is held. The defualt is in seconds, but you can use Minutes and Hours as well
        ' For example: @EdgeHold(60) would have the domme make you hold the edge for 60 seconds
        ' @EdgeHold(3 Minutes) or @EdgeHold(3 M) - Domme will make you hold the edge for three minutes
        ' @EdgeHold(2 Hours) - Domme will make you hold the edge for 2 hours. Good luck :D
        '
        'You can also set a time range using a comma. For example, @EdgeHold(2 Minutes, 5 Minutes) - the domme would make you hold it a random amount of time bwteen 2 and 5 minutes.

        If StringClean.Contains("@EdgeHold(") Then

            Dim EdgeHoldFlag As String = GetParentheses(StringClean, "@EdgeHold(")

            If EdgeHoldFlag.Contains(",") Then

                EdgeHoldFlag = FixCommas(EdgeHoldFlag)

                Dim EdgeFlagArray As String() = EdgeHoldFlag.Split(",")

                Dim Edge1 As Integer = Val(EdgeFlagArray(0))
                Dim Edge2 As Integer = Val(EdgeFlagArray(1))

                If UCase(EdgeFlagArray(0)).Contains("M") Then Edge1 *= 60
                If UCase(EdgeFlagArray(1)).Contains("M") Then Edge2 *= 60

                If UCase(EdgeFlagArray(0)).Contains("H") Then Edge1 *= 3600
                If UCase(EdgeFlagArray(1)).Contains("H") Then Edge2 *= 3600

                ssh.EdgeHoldSeconds = ssh.randomizer.Next(Edge1, Edge2 + 1)

            Else

                ssh.EdgeHoldSeconds = Val(EdgeHoldFlag)
                If UCase(GetParentheses(StringClean, "@EdgeHold(")).Contains("M") Then ssh.EdgeHoldSeconds *= 60
                If UCase(GetParentheses(StringClean, "@EdgeHold(")).Contains("H") Then ssh.EdgeHoldSeconds *= 3600

            End If

            EdgeHoldFlag = True


            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeHold = True
            StringClean = StringClean.Replace("@EdgeHold(" & GetParentheses(StringClean, "@EdgeHold(") & ")", "")

        End If


        If StringClean.Contains("@EdgeHold") Then
            ContactEdgeCheck(StringClean)
            Edge()
            ssh.EdgeHold = True
            StringClean = StringClean.Replace("@EdgeHold", "")
        End If

        If StringClean.Contains("@Edge") Then
            ContactEdgeCheck(StringClean)
            Edge()
            StringClean = StringClean.Replace("@Edge", "")
        End If

        If StringClean.Contains("@CBT") And Not StringClean.Contains("@CBTLevel") Then
            If FrmSettings.CockTortureEnabledCB.Checked = True And FrmSettings.BallTortureEnabledCB.Checked = True Then
                ssh.CBTBothActive = True
                ssh.CBTBothFlag = True
                ssh.TasksCount = ssh.randomizer.Next(FrmSettings.TaskWaitMinimum.Value, FrmSettings.TaskWaitMaximum.Value + 1)
            End If

            StringClean = StringClean.Replace("@CBT", "")
        End If

        If StringClean.Contains("@DecideOrgasm") Then

            ssh.OrgasmDenied = False
            ssh.OrgasmAllowed = False
            ssh.OrgasmRuined = False

            Dim AllowGoto As String = "Orgasm Allow"
            Dim RuinGoto As String = "Orgasm Ruin"
            Dim DenyGoto As String = "Orgasm Deny"

            If StringClean.Contains("@DecideOrgasm(") Then

                Dim OrgasmFlag As String = GetParentheses(StringClean, "@DecideOrgasm(")
                OrgasmFlag = FixCommas(OrgasmFlag)
                Dim OrgasmArray As String() = OrgasmFlag.Split(",")

                If OrgasmArray.Count = 3 Then
                    AllowGoto = OrgasmArray(0)
                    RuinGoto = OrgasmArray(1)
                    DenyGoto = OrgasmArray(2)
                End If

            End If


            If FrmSettings.alloworgasmComboBox.Text = "Always Allows" And FrmSettings.ruinorgasmComboBox.Text = "Always Ruins" Then
                ssh.FileGoto = RuinGoto
                ssh.OrgasmRuined = True
                GoTo OrgasmDecided
            End If

            Dim OrgasmInt As Integer = ssh.randomizer.Next(1, 101)
            Dim OrgasmThreshold As Integer

            If FrmSettings.alloworgasmComboBox.Text = "Never Allows" Then OrgasmThreshold = 0
            If FrmSettings.alloworgasmComboBox.Text = "Always Allows" Then OrgasmThreshold = 1000

            If FrmSettings.DommeDecideOrgasmCB.Checked = True Then
                If FrmSettings.alloworgasmComboBox.Text = "Rarely Allows" Then OrgasmThreshold = 20
                If FrmSettings.alloworgasmComboBox.Text = "Sometimes Allows" Then OrgasmThreshold = 50
                If FrmSettings.alloworgasmComboBox.Text = "Often Allows" Then OrgasmThreshold = 75
            Else
                If FrmSettings.alloworgasmComboBox.Text = "Rarely Allows" Then OrgasmThreshold = FrmSettings.NBAllowRarely.Value
                If FrmSettings.alloworgasmComboBox.Text = "Sometimes Allows" Then OrgasmThreshold = FrmSettings.NBAllowSometimes.Value
                If FrmSettings.alloworgasmComboBox.Text = "Often Allows" Then OrgasmThreshold = FrmSettings.AllowOrgasmOftenNB.Value
            End If


            If OrgasmInt > OrgasmThreshold Then
                ssh.FileGoto = DenyGoto
                ssh.OrgasmDenied = True
                GoTo OrgasmDecided
            End If

            Dim RuinInt As Integer = ssh.randomizer.Next(1, 101)
            Dim RuinThreshold As Integer

            If FrmSettings.ruinorgasmComboBox.Text = "Never Ruins" Then RuinThreshold = 0
            If FrmSettings.ruinorgasmComboBox.Text = "Always Ruins" Then RuinThreshold = 1000


            If FrmSettings.DommeDecideRuinCB.Checked = True Then
                If FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins" Then RuinThreshold = 20
                If FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins" Then RuinThreshold = 50
                If FrmSettings.ruinorgasmComboBox.Text = "Often Ruins" Then RuinThreshold = 75
            Else
                If FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins" Then RuinThreshold = FrmSettings.NBRuinRarely.Value
                If FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins" Then RuinThreshold = FrmSettings.NBRuinSometimes.Value
                If FrmSettings.ruinorgasmComboBox.Text = "Often Ruins" Then RuinThreshold = FrmSettings.NBRuinOften.Value
            End If


            If RuinInt > RuinThreshold Then
                ssh.FileGoto = AllowGoto
                ssh.OrgasmAllowed = True
            Else
                ssh.FileGoto = RuinGoto
                ssh.OrgasmRuined = True
            End If

OrgasmDecided:

            ssh.SkipGotoLine = True
            GetGoto()

            StringClean = StringClean.Replace("@DecideOrgasm", "")
        End If


        If StringClean.Contains(Keyword.OrgasmRuin) Then
            ssh.FileGoto = "Orgasm Ruin"
            ssh.OrgasmRuined = True
            ssh.SkipGotoLine = True
            GetGoto()
            StringClean = StringClean.Replace(Keyword.OrgasmRuin, "")
        End If

        ' The @Glitter Command allows to specify a specfic script from the domme's Apps\Glitter\Script directory, which will then immediately play out in the Glitter app. For example, @Glitter(About to Ruin)
        ' would run the Glitter script in Apps\Glitter\Script\About to Ruin.txt.

        If StringClean.Contains(Keyword.Glitter) Then

            ' GitHub Patch: Dim GlitterFlag As Integer = GetParentheses(StringClean, Keywords.Glitter)
            Dim GlitterFlag As String = GetParentheses(StringClean, Keyword.Glitter)

            If My.Settings.CBGlitterFeedOff = False And ssh.UpdatingPost = False Then
                If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Script\" & GlitterFlag & ".txt") And ssh.UpdatingPost = False Then
                    ssh.UpdateList.Clear()
                    ssh.UpdateList.Add(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Script\" & GlitterFlag & ".txt")
                    StatusUpdatePost()
                End If
            End If

            StringClean = StringClean.Replace(Keyword.Glitter & GlitterFlag & ")", "")

        End If



        If StringClean.Contains("@WritingTask(") Then

            ssh.WritingTaskFlag = True

            Dim WTTempString As String() = Split(StringClean, "@WritingTask(", 2)
            Dim WTTemp As String() = Split(WTTempString(1), ")")
            LBLWritingTaskText.Text = WTTemp(0)
            LBLWritingTaskText.Text = StripCommands(LBLWritingTaskText.Text)
            LBLWritingTaskText.Text = StripFormat(LBLWritingTaskText.Text)
            LBLWritingTaskText.Text = LBLWritingTaskText.Text.Replace("  ", " ")

            Dim WritingTaskVal As Integer = Val(LBLWritingTaskText.Text)

            If WritingTaskVal = 0 Then
                ssh.WritingTaskLinesAmount = ssh.randomizer.Next(FrmSettings.NBWritingTaskMin.Value, FrmSettings.NBWritingTaskMax.Value)
                ssh.WritingTaskLinesAmount = 5 * Math.Round(ssh.WritingTaskLinesAmount / 5)
            Else
                ssh.WritingTaskLinesAmount = WritingTaskVal
                LBLWritingTaskText.Text = LBLWritingTaskText.Text.Replace(WritingTaskVal, "")
            End If

            LBLLinesWritten.Text = "0"
            LBLLinesRemaining.Text = ssh.WritingTaskLinesAmount

            If PNLWritingTask.Visible = False Then
                ToggleAppVisibility(PNLWritingTask)
            End If

            'WritingTaskMistakesAllowed = randomizer.Next(3, 9)

            'determine error numbers based on numbers of lines to write
            ssh.WritingTaskMistakesAllowed = ssh.randomizer.Next(ssh.WritingTaskLinesAmount / 10, ssh.WritingTaskLinesAmount / 3)
            'clamps the value between 2 and 10 errors
            ssh.WritingTaskMistakesAllowed = Math.Max(2, ssh.WritingTaskMistakesAllowed)
            ssh.WritingTaskMistakesAllowed = Math.Min(ssh.WritingTaskMistakesAllowed, 10)

            LBLMistakesAllowed.Text = ssh.WritingTaskMistakesAllowed
            LBLMistakesMade.Text = "0"
            StringClean = StringClean.Replace("@WritingTask", "")
            'LBLWritingTask.Text = "Write the following line " & WritingTaskLinesAmount & " times."
            ssh.WritingTaskLinesRemaining = ssh.WritingTaskLinesAmount
            ssh.WritingTaskLinesWritten = 0
            ssh.WritingTaskMistakesMade = 0
            chatBox.ShortcutsEnabled = False
            ChatBox2.ShortcutsEnabled = False

            If My.Settings.TimedWriting = True Then

                Dim secs As Single

                'determines how many secs are given for writing each line, depending on line length and typespeed value selected by the user in the settings
                '(between 0,54 and 0,75 secs per character in the sentence at slowest typingspeed and between 0.18 and 0.25 at fastest typing speed)
                secs = (ssh.randomizer.Next(15, 25) / My.Settings.TypeSpeed) * LBLWritingTaskText.Text.Length
                'determines how much time is given (in seconds) to complete the @WritingTask() depending on how many lines you have to write and a small bonus to give some
                'more time for very short lines
                ssh.WritingTaskCurrentTime = 5 + secs * ssh.WritingTaskLinesAmount

                LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times" & vbCrLf & "In " & ConvertSeconds(ssh.WritingTaskCurrentTime)
                LBLWritingTask.Text = LBLWritingTask.Text.Replace("line 1 times", "line 1 time")
            Else
                LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times"
                LBLWritingTask.Text = LBLWritingTask.Text.Replace("line 1 times", "line 1 time")
            End If

        End If

        If StringClean.Contains("@CheckJOIVideo") Then

            If Directory.Exists(My.Settings.VideoJOI) Or Directory.Exists(My.Settings.VideoJOID) Then
                If FrmSettings.LblVideoJOITotal.Text <> "0" Or FrmSettings.LblVideoJOITotalD.Text <> "0" Then
                Else
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = "No JOI Found"
                    GetGoto()
                End If
            Else
                ssh.SkipGotoLine = True
                ssh.FileGoto = "No JOI Found"
                GetGoto()
            End If

            StringClean = StringClean.Replace("@CheckJOIVideo", "")

        End If


        If StringClean.Contains("@PlayJOIVideo") Then

            If Directory.Exists(My.Settings.VideoJOI) Or Directory.Exists(My.Settings.VideoJOID) Then

                ssh.TeaseVideo = True
                PlayRandomJOI()
            End If

            StringClean = StringClean.Replace("@PlayJOIVideo", "")

        End If

        If StringClean.Contains("@PlayCHVideo") Then

            If Directory.Exists(My.Settings.VideoCH) Or Directory.Exists(My.Settings.VideoCH) Then

                ssh.TeaseVideo = True
                PlayRandomCH()
            End If

            StringClean = StringClean.Replace("@PlayCHVideo", "")

        End If




        If StringClean.Contains("@GiveUpCheck") Then


            If ssh.AskedToGiveUpSection = True Then

                If ssh.SubGaveUp = True Then
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpREHASH.txt"
                Else
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpREPEAT.txt"
                End If
                'StringClean = ResponseClean(StringClean)

            Else

                ssh.AskedToGiveUpSection = True
                ssh.AskedToGiveUp = True

                Dim GiveUpCheck As Integer

                If FrmSettings.NBEmpathy.Value = 1 Then GiveUpCheck = 0
                If FrmSettings.NBEmpathy.Value = 2 Then GiveUpCheck = 25
                If FrmSettings.NBEmpathy.Value = 3 Then GiveUpCheck = 50
                If FrmSettings.NBEmpathy.Value = 4 Then GiveUpCheck = 75
                If FrmSettings.NBEmpathy.Value = 5 Then GiveUpCheck = 1000

                Dim GiveUpVal As Integer = ssh.randomizer.Next(1, 101)

                'If GiveUpVal > GiveUpCheck Then
                If GiveUpVal > GiveUpCheck And Not ssh.LastScript Then
                    ' you can give up
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpALLOWED.txt"
                    ssh.LockImage = False
                    If ssh.SlideshowLoaded = True Then
                        ImageSlideShowNextButton.Enabled = True
                        ImageSlideShowPreviousButton.Enabled = True
                        PicStripTSMIdommeSlideshow.Enabled = True
                    End If
                    ssh.SubGaveUp = True
                    ssh.FirstRound = False
                Else
                    ' you can't give up
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpDENIED.txt"
                End If



            End If

            StringClean = ResponseClean(StringClean)

        End If


        If StringClean.Contains(Keyword.EndTease) Then
            SetVariable("SYS_SubLeftEarly", 0)
            'My.Settings.Sys_SubLeftEarly = 0
            'StopEverything()
            'ResetButton()
            ssh.Reset()
            FrmSettings.LockOrgasmChances(False)
            ssh.DomTask = "@SystemMessage <b>Tease AI has been reset</b>"
            ssh.DomChat = "@SystemMessage <b>Tease AI has been reset</b>"
            StringClean = StringClean.Replace(Keyword.EndTease, "")
        End If

        If StringClean.Contains("@FinishTease") Then
            ssh.TeaseTick = 0
            StringClean = StringClean.Replace("@FinishTease", "")
        End If

        If StringClean.Contains("@DommeLevelDown") Then
            If FrmSettings.DominationLevel.Value > 1 Then
                FrmSettings.DominationLevel.Value -= 1
            End If
            StringClean = StringClean.Replace("@DommeLevelDown", "")
        End If

        If StringClean.Contains("@ApathyLevelDown") Then
            FrmSettings.NBEmpathy.Value = ApathyLevel.Create(Convert.ToInt32(FrmSettings.NBEmpathy.Value)).Value - 1
            StringClean = StringClean.Replace("@ApathyLevelDown", "")
        End If

        If StringClean.Contains("@DommeLevelUp") Then
            If FrmSettings.DominationLevel.Value < 5 Then
                FrmSettings.DominationLevel.Value += 1
            End If
            StringClean = StringClean.Replace("@DommeLevelUp", "")
        End If

        If StringClean.Contains("@ApathyLevelUp") Then
            FrmSettings.NBEmpathy.Value = ApathyLevel.Create(Convert.ToInt32(FrmSettings.NBEmpathy.Value)).Value + 1
            StringClean = StringClean.Replace("@ApathyLevelUp", "")
        End If

        If StringClean.Contains("@InterruptLongEdge") Then

            Dim EdgeList As New List(Of String)

            For Each EdgeFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Long Edge\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                EdgeList.Add(EdgeFile)
            Next


            If EdgeList.Count > 0 Then

                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
                EdgeTauntTimer.Stop()
                StrokeTimer.Stop()
                StrokeTauntTimer.Stop()
                ssh.FileText = EdgeList(ssh.randomizer.Next(0, EdgeList.Count))
                ssh.LockImage = False
                ssh.MiniScript = False
                If ssh.SlideshowLoaded = True Then
                    ImageSlideShowNextButton.Enabled = True
                    ImageSlideShowPreviousButton.Enabled = True
                    PicStripTSMIdommeSlideshow.Enabled = True
                End If
                ssh.StrokeTauntVal = -1
                ssh.ScriptTick = 3
                ScriptTimer.Start()
                ssh.ShowModule = True

            Else
                MessageBox.Show(Me, "No files were found in " & Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Long Edge!" & Environment.NewLine _
                 & Environment.NewLine & "Please make sure at lease one LongEdge_ file exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            StringClean = StringClean.Replace("@InterruptLongEdge", "")
            ssh.JustShowedBlogImage = True
        End If

        If StringClean.Contains("@InterruptStartStroking") Then

            If ssh.CensorshipGame = True Or ssh.AvoidTheEdgeGame = True Or ssh.RLGLGame = True Then
                StringClean = "Ask me later"
                GoTo VTSkip
            End If

            Dim StrokeList As New List(Of String)

            For Each StrokeFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Start Stroking\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                StrokeList.Add(StrokeFile)
            Next

            If StrokeList.Count > 0 Then

                ssh.CBTCockFlag = False
                ssh.CBTBallsFlag = False
                ssh.CBTBothFlag = False
                ssh.CustomTask = False
                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
                EdgeTauntTimer.Stop()
                StrokeTimer.Stop()
                StrokeTauntTimer.Stop()
                ssh.FileText = StrokeList(ssh.randomizer.Next(0, StrokeList.Count))
                ssh.LockImage = False
                If ssh.SlideshowLoaded = True Then
                    ImageSlideShowNextButton.Enabled = True
                    ImageSlideShowPreviousButton.Enabled = True
                    PicStripTSMIdommeSlideshow.Enabled = True
                End If
                ssh.StrokeTauntVal = -1
                ssh.ScriptTick = 3
                ScriptTimer.Start()
                ssh.ShowModule = True
                ssh.MiniScript = False

            Else
                MessageBox.Show(Me, "No files were found in " & Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Start Stroking!" & Environment.NewLine _
                 & Environment.NewLine & "Please make sure at lease one StartStroking_ file exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            StringClean = StringClean.Replace("@InterruptStartStroking", "")
            ssh.JustShowedBlogImage = True
        End If

        If StringClean.Contains("@Interrupt(") Then
            Dim InterruptClean As String = StringClean
            Dim StartIndex As Integer = InterruptClean.IndexOf("@Interrupt(") + 11
            For i As Integer = 1 To StartIndex
                InterruptClean = InterruptClean.Remove(0, 1)
            Next
            Dim InterruptS As String() = InterruptClean.Split(")")
            InterruptClean = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\" & InterruptS(0) & ".txt"

            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\" & InterruptS(0) & ".txt") Then

                ssh.FirstRound = False
                ssh.CBTCockFlag = False
                ssh.CBTBallsFlag = False
                ssh.CBTBothFlag = False
                ssh.CustomTask = False
                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
                StrokeTimer.Stop()
                StrokeTauntTimer.Stop()

                CensorshipTimer.Stop()
                RLGLTimer.Stop()
                TnASlides.Stop()
                AvoidTheEdge.Stop()
                EdgeTauntTimer.Stop()
                HoldEdgeTimer.Stop()
                HoldEdgeTauntTimer.Stop()
                AvoidTheEdgeTaunts.Stop()
                RLGLTauntTimer.Stop()
                VideoTauntTimer.Stop()
                EdgeCountTimer.Stop()

                ssh.FileText = InterruptClean
                ssh.LockImage = False
                If ssh.SlideshowLoaded = True Then
                    ImageSlideShowNextButton.Enabled = True
                    ImageSlideShowPreviousButton.Enabled = True
                    PicStripTSMIdommeSlideshow.Enabled = True
                End If
                ssh.StrokeTauntVal = -1
                ssh.ScriptTick = 3
                ScriptTimer.Start()
                ssh.ShowModule = True

                ssh.MiniScript = False

            Else
                MessageBox.Show(Me, InterruptS(0) & ".txt was not found in " & Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt!" & Environment.NewLine _
                 & Environment.NewLine & "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            StringClean = StringClean.Replace("@Interrupt(" & InterruptS(0) & ")", "")
            ssh.JustShowedBlogImage = True
        End If

        If StringClean.Contains("@BookmarkModule") Then
            ssh.BookmarkModule = True
            ssh.BookmarkModuleFile = ssh.FileText
            ssh.BookmarkModuleLine = ssh.StrokeTauntVal + 1
            StringClean = StringClean.Replace("@BookmarkModule", "")
        End If

        If StringClean.Contains("@BookmarkLink") Then
            ssh.BookmarkLink = True
            ssh.BookmarkLinkFile = ssh.FileText
            ssh.BookmarkLinkLine = ssh.StrokeTauntVal + 1
            StringClean = StringClean.Replace("@BookmarkLink", "")
        End If

        If StringClean.Contains("@AFKOn") Then
            ssh.AFK = True
            StringClean = StringClean.Replace("@AFKOn", "")
        End If

        If StringClean.Contains("@AFKOff") Then
            ssh.AFK = False
            StringClean = StringClean.Replace("@AFKOff", "")
        End If

        If StringClean.Contains("@Wait(") Then

            Dim WaitFlag As String = GetParentheses(StringClean, "Wait(")
            Dim WaitSeconds As Integer = Val(WaitFlag)

            If UCase(WaitFlag).Contains("M") Then WaitSeconds *= 60
            If UCase(WaitFlag).Contains("H") Then WaitSeconds *= 3600

            ssh.WaitTick = WaitSeconds
            WaitTimer.Start()

            StringClean = StringClean.Replace("@Wait(" & WaitFlag & ")", "")

        End If



        If StringClean.Contains("@SendDailyTasks") Then
            CreateTaskLetter()
            StringClean = StringClean.Replace("@SendDailyTasks", "")
        End If

        If StringClean.Contains("@EdgingHold") Then

            ssh.DomTypeCheck = True
            ssh.SubEdging = False
            ssh.SubStroking = False
            ssh.SubHoldingEdge = True
            EdgeTauntTimer.Stop()
            'DomChat = "#HoldTheEdge"
            'TypingDelay()

            ssh.HoldEdgeTick = ssh.HoldEdgeChance

            Dim HoldEdgeMin As Integer = FrmSettings.HoldEdgeMinimum.Value
            If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then HoldEdgeMin *= 60

            Dim HoldEdgeMax As Integer = FrmSettings.HoldEdgeMaximum.Value
            If FrmSettings.LBLMaxHold.Text = "minutes" Then HoldEdgeMax *= 60

            If ssh.ExtremeHold = True Then
                HoldEdgeMin = FrmSettings.ExtremeEdgeHoldMinimum.Value * 60
                HoldEdgeMax = FrmSettings.ExtremeEdgeHoldMaximum.Value * 60
            End If

            If ssh.LongHold = True Then
                HoldEdgeMin = FrmSettings.LongEdgeHoldMinimum.Value * 60
                HoldEdgeMax = FrmSettings.LongEdgeHoldMaximum.Value * 60
            End If


            If HoldEdgeMax < HoldEdgeMin Then HoldEdgeMax = HoldEdgeMin + 1

            ssh.HoldEdgeTick = ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1)
            If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10

            ssh.HoldEdgeTime = 0

            HoldEdgeTimer.Start()
            HoldEdgeTauntTimer.Start()

            Do
                Application.DoEvents()
            Loop Until ssh.DomTypeCheck = False


            StringClean = StringClean.Replace("@EdgingHold", "")
        End If

        If StringClean.Contains("@EdgingStop") Then

            ssh.DomTypeCheck = True
            ssh.SubEdging = False
            ssh.SubStroking = False
            EdgeTauntTimer.Stop()
            'DomChat = "#StopStrokingEdge"
            'TypingDelay()

            Do
                Application.DoEvents()
            Loop Until ssh.DomTypeCheck = False

            StringClean = StringClean.Replace("@EdgingStop", "")
        End If

        'Github Patch  If StringClean.Contains("@EdgingDecide") Then
        If StringClean.Contains("@DecideEdge") Then

            ssh.TempVal = ssh.randomizer.Next(0, 101)

            If ssh.TempVal < 51 Then

                ssh.DomTypeCheck = True
                ssh.SubEdging = False
                ssh.SubStroking = False
                ssh.SubHoldingEdge = True
                EdgeTauntTimer.Stop()
                StrokePace = 0
                ssh.DomChat = "#HoldTheEdge"
                If ssh.Contact1Stroke = True Then
                    ssh.DomChat = "@Contact1 #HoldTheEdge"
                    ' Github Patch Contact1Stroke = False
                End If
                If ssh.Contact2Stroke = True Then
                    ssh.DomChat = "@Contact2 #HoldTheEdge"
                    ' Github Patch Contact2Stroke = False
                End If
                If ssh.Contact3Stroke = True Then
                    ssh.DomChat = "@Contact3 #HoldTheEdge"
                    ' Github Patch Contact3Stroke = False
                End If

                ssh.HoldEdgeTick = ssh.HoldEdgeChance

                Dim HoldEdgeMin As Integer = FrmSettings.HoldEdgeMinimum.Value
                If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then HoldEdgeMin *= 60

                Dim HoldEdgeMax As Integer = FrmSettings.HoldEdgeMaximum.Value
                If FrmSettings.LBLMaxHold.Text = "minutes" Then HoldEdgeMax *= 60

                If HoldEdgeMax < HoldEdgeMin Then HoldEdgeMax = HoldEdgeMin + 1

                ssh.HoldEdgeTick = ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1)
                If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10

                ssh.HoldEdgeTime = 0

                HoldEdgeTimer.Start()
                HoldEdgeTauntTimer.Start()

            Else

                ssh.DomTypeCheck = True
                ssh.SubEdging = False
                ssh.SubStroking = False
                EdgeTauntTimer.Stop()
                ssh.DomChat = "#StopStrokingEdge"
                If ssh.Contact1Stroke = True Then
                    ssh.DomChat = "@Contact1 #StopStrokingEdge"
                    ssh.Contact1Stroke = False
                End If
                If ssh.Contact2Stroke = True Then
                    ssh.DomChat = "@Contact2 #StopStrokingEdge"
                    ssh.Contact2Stroke = False
                End If
                If ssh.Contact3Stroke = True Then
                    ssh.DomChat = "@Contact3 #StopStrokingEdge"
                    ssh.Contact3Stroke = False
                End If
            End If

            Do
                Application.DoEvents()
            Loop Until ssh.DomTypeCheck = False


            StringClean = StringClean.Replace("@DecideEdge", "")
        End If

        If StringClean.Contains("@CheckVideo") Then
            ssh.VideoCheck = True
            RandomVideo()
            If ssh.NoVideo = True Then
                ssh.FileGoto = "(No Videos Found)"
            Else
                ssh.FileGoto = "(Videos Found)"
            End If
            ssh.VideoCheck = False
            ssh.NoVideo = False
            ssh.SkipGotoLine = True
            GetGoto()
            StringClean = StringClean.Replace("@CheckVideo", "")
        End If

        If StringClean.Contains("@PlayCensorshipSucks") Then

            RandomVideo()

            If ssh.NoVideo = False Then
                ssh.ScriptVideoTease = "Censorship Sucks"
                ssh.ScriptVideoTeaseFlag = True
                ssh.ScriptVideoTeaseFlag = False
                ssh.CensorshipGame = True
                ssh.VideoTease = True
                ssh.CensorshipTick = ssh.randomizer.Next(FrmSettings.NBCensorHideMin.Value, FrmSettings.NBCensorHideMax.Value + 1)
                CensorshipTimer.Start()
            End If

            StringClean = StringClean.Replace("@PlayCensorshipSucks", "")
        End If

        If StringClean.Contains("@VitalSubAssignment") Then
            ' Read all lines of the given file.
            Dim AssignList As List(Of String) = Txt2List(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\VitalSub\Assignments.txt")

            Dim TempAssign As String

            Try
                AssignList = FilterList(AssignList)
                TempAssign = AssignList(ssh.randomizer.Next(0, AssignList.Count))
            Catch
                TempAssign = "ERROR: VitalSub Assign"
            End Try

            Dim TempArray As String() = Split(TempAssign)

            For i As Integer = TempArray.Count - 1 To 0 Step -1
                If TempArray(i).Contains("@") Then TempArray(i) = ""
            Next


            CLBExercise.Items.Add(TempAssign)
            SaveExercise()
            CBVitalSubDomTask.Checked = False
            My.Settings.VitalSubAssignments = False
            StringClean = StringClean.Replace("@VitalSubAssignment", "")
        End If

        If StringClean.Contains("@PlayAvoidTheEdge") Then
            ' #### Reboot

            RandomVideo()

            If ssh.NoVideo = False Then

                ScriptTimer.Stop()
                ssh.SubStroking = True
                ssh.TempStrokeTauntVal = ssh.StrokeTauntVal
                ssh.TempFileText = ssh.FileText
                ssh.ScriptVideoTease = "Avoid The Edge"
                ssh.ScriptVideoTeaseFlag = True
                ssh.AvoidTheEdgeStroking = True
                ssh.AvoidTheEdgeGame = True
                ssh.ScriptVideoTeaseFlag = False
                ssh.VideoTease = True
                ssh.StartStrokingCount += 1
                StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
                StrokePace = 50 * Math.Round(StrokePace / 50)
                ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value
                AvoidTheEdgeTaunts.Start()

            End If

            StringClean = StringClean.Replace("@PlayAvoidTheEdge", "")
        End If

        If StringClean.Contains("@ResumeAvoidTheEdge") Then
            DomWMP.Ctlcontrols.play()
            ScriptTimer.Stop()
            ssh.AvoidTheEdgeStroking = True
            ssh.SubStroking = True
            ssh.StartStrokingCount += 1
            ssh.VideoTease = True
            StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
            StrokePace = 50 * Math.Round(StrokePace / 50)
            ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value
            AvoidTheEdgeTaunts.Start()
            StringClean = StringClean.Replace("@ResumeAvoidTheEdge", "")
        End If

        If StringClean.Contains("@PlayRedLightGreenLight") Then
            ' #### Reboot

            RandomVideo()

            If ssh.NoVideo = False Then

                ScriptTimer.Stop()
                ssh.SubStroking = True
                ssh.TempStrokeTauntVal = ssh.StrokeTauntVal
                ssh.TempFileText = ssh.FileText
                ssh.ScriptVideoTease = "RLGL"
                ssh.ScriptVideoTeaseFlag = True
                'AvoidTheEdgeStroking = True
                ssh.RLGLGame = True

                ssh.ScriptVideoTeaseFlag = False
                ssh.VideoTease = True
                ssh.RLGLTick = ssh.randomizer.Next(FrmSettings.NBGreenLightMin.Value, FrmSettings.NBGreenLightMax.Value + 1)
                RLGLTimer.Start()
                ssh.StartStrokingCount += 1
                StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
                StrokePace = 50 * Math.Round(StrokePace / 50)
                'VideoTauntTick = randomizer.Next(20, 31)
                'VideoTauntTimer.Start()

            End If
            StringClean = StringClean.Replace("@PlayRedLightGreenLight", "")
        End If

        If StringClean.Contains("@PlayVideo[") Then

            Dim VideoFlag As String = GetParentheses(StringClean, "@PlayVideo[")
            Dim VideoClean As String

            If StringClean.Contains("@JumpVideo") Then
                ssh.JumpVideo = True
                StringClean = StringClean.Replace("@JumpVideo", "")
            End If

            If VideoFlag.Contains(":\") Then
                VideoClean = VideoFlag

                If File.Exists(VideoClean) Then
                    DomWMP.URL = VideoClean
                    DomWMP.Visible = True
                    mainPictureBox.Visible = False
                    ssh.TeaseVideo = True

                    If ssh.JumpVideo = True Then

                        Do
                            Application.DoEvents()
                        Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)

                        Dim VideoLength As Integer = DomWMP.currentMedia.duration
                        Dim VidLow As Integer = VideoLength * 0.4
                        Dim VidHigh As Integer = VideoLength * 0.9
                        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                        DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

                    End If

                    ssh.JumpVideo = False

                Else
                    MessageBox.Show(Me, Path.GetFileName(VideoClean) & " was not found in " & Path.GetDirectoryName(VideoClean) & "!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

                GoTo ExternalVideo

            Else
                VideoClean = Application.StartupPath & "\Video\" & VideoFlag
                VideoClean = VideoClean.Replace("\\", "\")
            End If

            If VideoClean.Contains("*") Then

                Dim VideoList As New List(Of String)

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Path.GetDirectoryName(VideoClean), FileIO.SearchOption.SearchTopLevelOnly, Path.GetFileName(VideoClean))
                    VideoList.Add(foundFile)
                Next

                If VideoList.Count > 0 Then
                    DomWMP.URL = VideoList(ssh.randomizer.Next(0, VideoList.Count))
                    DomWMP.Visible = True
                    mainPictureBox.Visible = False
                    ssh.TeaseVideo = True

                    If ssh.JumpVideo = True Then

                        Do
                            Application.DoEvents()
                        Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)

                        Dim VideoLength As Integer = DomWMP.currentMedia.duration
                        Dim VidLow As Integer = VideoLength * 0.4
                        Dim VidHigh As Integer = VideoLength * 0.9
                        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                        DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

                    End If

                    ssh.JumpVideo = False
                Else
                    MessageBox.Show(Me, "No videos matching " & Path.GetFileName(VideoClean) & " were found in " & Path.GetDirectoryName(VideoClean) & "!" & Environment.NewLine & Environment.NewLine &
                      "Please make sure that valid files exist and that the wildcards are applied correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            Else

                If File.Exists(VideoClean) Then
                    DomWMP.URL = VideoClean
                    DomWMP.Visible = True
                    mainPictureBox.Visible = False
                    ssh.TeaseVideo = True

                    If ssh.JumpVideo = True Then

                        Do
                            Application.DoEvents()
                        Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)

                        Dim VideoLength As Integer = DomWMP.currentMedia.duration
                        Dim VidLow As Integer = VideoLength * 0.4
                        Dim VidHigh As Integer = VideoLength * 0.9
                        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                        DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

                    End If

                    ssh.JumpVideo = False

                Else
                    MessageBox.Show(Me, Path.GetFileName(VideoClean) & " was not found in " & Application.StartupPath & "\Video!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            End If

ExternalVideo:

            StringClean = StringClean.Replace("@PlayVideo[" & VideoFlag & "]", "")
        End If

        If StringClean.Contains("@PlayAudio[") Then

            Dim AudioFlag As String = GetParentheses(StringClean, "@PlayAudio[")
            ' Github Patch Dim AudioClean As String = Application.StartupPath & "\Video\" & AudioFlag
            Dim AudioClean As String

            If AudioFlag.Contains(":\") And Not AudioFlag.Contains("*") Then
                AudioClean = AudioFlag

                If File.Exists(AudioClean) Then
                    DomWMP.URL = AudioClean
                Else
                    MessageBox.Show(Me, Path.GetFileName(AudioClean) & " was not found in " & Path.GetDirectoryName(AudioClean) & "!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

                GoTo ExternalAudio

            Else

                AudioClean = Application.StartupPath & "\Audio\" & AudioFlag
                AudioClean = AudioClean.Replace("\\", "\")
            End If



            If AudioClean.Contains("*") Then

                Dim AudioList As New List(Of String)

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Path.GetDirectoryName(AudioClean), FileIO.SearchOption.SearchTopLevelOnly, Path.GetFileName(AudioClean))
                    AudioList.Add(foundFile)
                Next

                If AudioList.Count > 0 Then
                    DomWMP.URL = AudioList(ssh.randomizer.Next(0, AudioList.Count))
                Else
                    MessageBox.Show(Me, "No audio files matching " & Path.GetFileName(AudioClean) & " were found in " & Path.GetDirectoryName(AudioClean) & "!" & Environment.NewLine & Environment.NewLine &
                      "Please make sure that valid files exist and that the wildcards are applied correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            Else


                If File.Exists(AudioClean) Then
                    DomWMP.URL = AudioClean
                Else
                    MessageBox.Show(Me, Path.GetFileName(AudioClean) & " was not found in " & Application.StartupPath & "\Audio!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            End If

ExternalAudio:

            StringClean = StringClean.Replace("@PlayAudio[" & AudioFlag & "]", "")

        End If


        If StringClean.Contains("@PlayVideo(") Then


            Dim VidFlag As String = GetParentheses(StringClean, "@PlayVideo(")
            Dim VidInt As Integer = Val(VidFlag)
            If UCase(VidFlag).Contains("M") Then VidInt *= 60

            If StringClean.Contains("@JumpVideo") Then
                ssh.JumpVideo = True
                StringClean = StringClean.Replace("@JumpVideo", "")
            End If

            ssh.RandomizerVideo = True
            RandomVideo()

            If ssh.NoVideo = False Then
                ssh.TeaseVideo = True
                ssh.VideoTick = VidInt
                VideoTimer.Start()
            Else
                MessageBox.Show(Me, "No videos were found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If

            ssh.RandomizerVideo = False
            StringClean = StringClean.Replace("@PlayVideo", "")
        End If

        If StringClean.Contains("@JumpVideo") Then

            If (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying) Then
                Dim VideoLength As Integer = DomWMP.currentMedia.duration
                Dim VidLow As Integer = VideoLength * 0.4
                Dim VidHigh As Integer = VideoLength * 0.9
                Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

            End If
            StringClean = StringClean.Replace("@JumpVideo", "")
        End If


        If StringClean.Contains("@AddStrokeTime(") Then

            Dim OriginalFlag As String = ""

            If StrokeTimer.Enabled = True Then

                Dim StrokeFlag As String = GetParentheses(StringClean, "@AddStrokeTime(")
                OriginalFlag = StrokeFlag
                Dim StrokeSeconds As Integer

                If StrokeFlag.Contains(",") Then
                    StrokeFlag = FixCommas(StrokeFlag)
                    Dim StrokeFlagArray As String() = StrokeFlag.Split(",")
                    Dim Stroke1 As Integer = Val(StrokeFlagArray(0))
                    Dim Stroke2 As Integer = Val(StrokeFlagArray(1))
                    If UCase(StrokeFlagArray(0)).Contains("M") Then Stroke1 *= 60
                    If UCase(StrokeFlagArray(1)).Contains("M") Then Stroke2 *= 60
                    If UCase(StrokeFlagArray(0)).Contains("H") Then Stroke1 *= 3600
                    If UCase(StrokeFlagArray(1)).Contains("H") Then Stroke2 *= 3600
                    StrokeSeconds = ssh.randomizer.Next(Stroke1, Stroke2 + 1)
                Else
                    StrokeSeconds = Val(StrokeFlag)
                    If UCase(GetParentheses(StringClean, "@AddStrokeTime(")).Contains("M") Then StrokeSeconds *= 60
                    If UCase(GetParentheses(StringClean, "@AddStrokeTime(")).Contains("H") Then StrokeSeconds *= 3600
                End If
                ssh.StrokeTick += StrokeSeconds
            End If
            StringClean = StringClean.Replace("@AddStrokeTime(" & OriginalFlag & ")", "")
        End If

        If StringClean.Contains("@RemoveStrokeTime(") Then

            Dim OriginalFlag As String = ""

            If StrokeTimer.Enabled = True Then

                Dim StrokeFlag As String = GetParentheses(StringClean, "@RemoveStrokeTime(")
                OriginalFlag = StrokeFlag
                Dim StrokeSeconds As Integer

                If StrokeFlag.Contains(",") Then
                    StrokeFlag = FixCommas(StrokeFlag)
                    Dim StrokeFlagArray As String() = StrokeFlag.Split(",")
                    Dim Stroke1 As Integer = Val(StrokeFlagArray(0))
                    Dim Stroke2 As Integer = Val(StrokeFlagArray(1))
                    If UCase(StrokeFlagArray(0)).Contains("M") Then Stroke1 *= 60
                    If UCase(StrokeFlagArray(1)).Contains("M") Then Stroke2 *= 60
                    If UCase(StrokeFlagArray(0)).Contains("H") Then Stroke1 *= 3600
                    If UCase(StrokeFlagArray(1)).Contains("H") Then Stroke2 *= 3600
                    StrokeSeconds = ssh.randomizer.Next(Stroke1, Stroke2 + 1)
                Else
                    StrokeSeconds = Val(StrokeFlag)
                    If UCase(GetParentheses(StringClean, "@RemoveStrokeTime(")).Contains("M") Then StrokeSeconds *= 60
                    If UCase(GetParentheses(StringClean, "@RemoveStrokeTime(")).Contains("H") Then StrokeSeconds *= 3600
                End If
                ssh.StrokeTick -= StrokeSeconds
                If ssh.StrokeTick < 0 Then ssh.StrokeTick = 5
            End If
            StringClean = StringClean.Replace("@RemoveStrokeTime(" & OriginalFlag & ")", "")
        End If



        If StringClean.Contains("@AddStrokeTime") Then
            If StrokeTimer.Enabled = True Then
                If FrmSettings.CBTauntCycleDD.Checked = True Then
                    If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick += ssh.randomizer.Next(1, 3) * 60
                    If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick += ssh.randomizer.Next(1, 4) * 60
                    If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick += ssh.randomizer.Next(3, 6) * 60
                    If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick += ssh.randomizer.Next(4, 8) * 60
                    If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick += ssh.randomizer.Next(5, 11) * 60
                Else
                    ssh.StrokeTick += ssh.randomizer.Next(FrmSettings.NBTauntCycleMin.Value * 60, FrmSettings.NBTauntCycleMax.Value * 60)
                End If
            End If
            StringClean = StringClean.Replace("@AddStrokeTime", "")
        End If

        If StringClean.Contains("@RemoveStrokeTime") Then
            If StrokeTimer.Enabled = True Then
                ssh.StrokeTick -= ssh.StrokeTick / 2
            End If
            StringClean = StringClean.Replace("@RemoveStrokeTime", "")
        End If


        If StringClean.Contains("@AddEdgeHoldTime(") Then

            Dim OriginalFlag As String = ""

            If HoldEdgeTimer.Enabled = True Then

                Dim HoldEdgeFlag As String = GetParentheses(StringClean, "@AddEdgeHoldTime(")
                OriginalFlag = HoldEdgeFlag
                Dim HoldEdgeSeconds As Integer

                If HoldEdgeFlag.Contains(",") Then
                    HoldEdgeFlag = FixCommas(HoldEdgeFlag)
                    Dim HoldEdgeFlagArray As String() = HoldEdgeFlag.Split(",")
                    Dim HoldEdge1 As Integer = Val(HoldEdgeFlagArray(0))
                    Dim HoldEdge2 As Integer = Val(HoldEdgeFlagArray(1))
                    If UCase(HoldEdgeFlagArray(0)).Contains("M") Then HoldEdge1 *= 60
                    If UCase(HoldEdgeFlagArray(1)).Contains("M") Then HoldEdge2 *= 60
                    If UCase(HoldEdgeFlagArray(0)).Contains("H") Then HoldEdge1 *= 3600
                    If UCase(HoldEdgeFlagArray(1)).Contains("H") Then HoldEdge2 *= 3600
                    HoldEdgeSeconds = ssh.randomizer.Next(HoldEdge1, HoldEdge2 + 1)
                Else
                    HoldEdgeSeconds = Val(HoldEdgeFlag)
                    If UCase(GetParentheses(StringClean, "@AddEdgeHoldTime(")).Contains("M") Then HoldEdgeSeconds *= 60
                    If UCase(GetParentheses(StringClean, "@AddEdgeHoldTime(")).Contains("H") Then HoldEdgeSeconds *= 3600
                End If
                ssh.HoldEdgeTick += HoldEdgeSeconds
            End If
            StringClean = StringClean.Replace("@AddEdgeHoldTime(" & OriginalFlag & ")", "")
        End If

        If StringClean.Contains("@RemoveEdgeHoldTime(") Then

            Dim OriginalFlag As String = ""

            If HoldEdgeTimer.Enabled = True Then

                Dim HoldEdgeFlag As String = GetParentheses(StringClean, "@RemoveEdgeHoldTime(")
                OriginalFlag = HoldEdgeFlag
                Dim HoldEdgeSeconds As Integer

                If HoldEdgeFlag.Contains(",") Then
                    HoldEdgeFlag = FixCommas(HoldEdgeFlag)
                    Dim HoldEdgeFlagArray As String() = HoldEdgeFlag.Split(",")
                    Dim HoldEdge1 As Integer = Val(HoldEdgeFlagArray(0))
                    Dim HoldEdge2 As Integer = Val(HoldEdgeFlagArray(1))
                    If UCase(HoldEdgeFlagArray(0)).Contains("M") Then HoldEdge1 *= 60
                    If UCase(HoldEdgeFlagArray(1)).Contains("M") Then HoldEdge2 *= 60
                    If UCase(HoldEdgeFlagArray(0)).Contains("H") Then HoldEdge1 *= 3600
                    If UCase(HoldEdgeFlagArray(1)).Contains("H") Then HoldEdge2 *= 3600
                    HoldEdgeSeconds = ssh.randomizer.Next(HoldEdge1, HoldEdge2 + 1)
                Else
                    HoldEdgeSeconds = Val(HoldEdgeFlag)
                    If UCase(GetParentheses(StringClean, "@RemoveEdgeHoldTime(")).Contains("M") Then HoldEdgeSeconds *= 60
                    If UCase(GetParentheses(StringClean, "@RemoveEdgeHoldTime(")).Contains("H") Then HoldEdgeSeconds *= 3600
                End If
                ssh.HoldEdgeTick -= HoldEdgeSeconds
                If ssh.HoldEdgeTick < 5 Then ssh.HoldEdgeTick = 5
            End If
            StringClean = StringClean.Replace("@RemoveEdgeHoldTime(" & OriginalFlag & ")", "")
        End If


        If StringClean.Contains("@AddEdgeHoldTime") Then

            If HoldEdgeTimer.Enabled = True Then
                Dim HoldEdgeMin As Integer = FrmSettings.HoldEdgeMinimum.Value
                If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then HoldEdgeMin *= 60

                Dim HoldEdgeMax As Integer = FrmSettings.HoldEdgeMaximum.Value
                If FrmSettings.LBLMaxHold.Text = "minutes" Then HoldEdgeMax *= 60

                If HoldEdgeMax < HoldEdgeMin Then HoldEdgeMax = HoldEdgeMin + 1

                ssh.HoldEdgeTick += ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1)
                If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10
            End If
            StringClean = StringClean.Replace("@AddEdgeHoldTime", "")
        End If

        If StringClean.Contains("@RemoveEdgeHoldTime") Then
            If HoldEdgeTimer.Enabled = True Then
                ssh.HoldEdgeTick = ssh.HoldEdgeTick / 2
                If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10
            End If
            StringClean = StringClean.Replace("@RemoveEdgeHoldTime", "")
        End If

        If StringClean.Contains("@AddTeaseTime(") Then

            Dim OriginalFlag As String = ""

            If TeaseTimer.Enabled = True Then

                Dim TeaseFlag As String = GetParentheses(StringClean, "@AddTeaseTime(")
                OriginalFlag = TeaseFlag
                Dim TeaseSeconds As Integer

                If TeaseFlag.Contains(",") Then
                    TeaseFlag = FixCommas(TeaseFlag)
                    Dim TeaseFlagArray As String() = TeaseFlag.Split(",")
                    Dim Tease1 As Integer = Val(TeaseFlagArray(0))
                    Dim Tease2 As Integer = Val(TeaseFlagArray(1))
                    If UCase(TeaseFlagArray(0)).Contains("M") Then Tease1 *= 60
                    If UCase(TeaseFlagArray(1)).Contains("M") Then Tease2 *= 60
                    If UCase(TeaseFlagArray(0)).Contains("H") Then Tease1 *= 3600
                    If UCase(TeaseFlagArray(1)).Contains("H") Then Tease2 *= 3600
                    TeaseSeconds = ssh.randomizer.Next(Tease1, Tease2 + 1)
                Else
                    TeaseSeconds = Val(TeaseFlag)
                    If UCase(GetParentheses(StringClean, "@AddTeaseTime(")).Contains("M") Then TeaseSeconds *= 60
                    If UCase(GetParentheses(StringClean, "@AddTeaseTime(")).Contains("H") Then TeaseSeconds *= 3600
                End If
                ssh.TeaseTick += TeaseSeconds
            End If
            StringClean = StringClean.Replace("@AddTeaseTime(" & OriginalFlag & ")", "")
        End If

        If StringClean.Contains("@RemoveTeaseTime(") Then

            Dim OriginalFlag As String = ""

            If TeaseTimer.Enabled = True Then

                Dim TeaseFlag As String = GetParentheses(StringClean, "@RemoveTeaseTime(")
                OriginalFlag = TeaseFlag
                Dim TeaseSeconds As Integer

                If TeaseFlag.Contains(",") Then
                    TeaseFlag = FixCommas(TeaseFlag)
                    Dim TeaseFlagArray As String() = TeaseFlag.Split(",")
                    Dim Tease1 As Integer = Val(TeaseFlagArray(0))
                    Dim Tease2 As Integer = Val(TeaseFlagArray(1))
                    If UCase(TeaseFlagArray(0)).Contains("M") Then Tease1 *= 60
                    If UCase(TeaseFlagArray(1)).Contains("M") Then Tease2 *= 60
                    If UCase(TeaseFlagArray(0)).Contains("H") Then Tease1 *= 3600
                    If UCase(TeaseFlagArray(1)).Contains("H") Then Tease2 *= 3600
                    TeaseSeconds = ssh.randomizer.Next(Tease1, Tease2 + 1)
                Else
                    TeaseSeconds = Val(TeaseFlag)
                    If UCase(GetParentheses(StringClean, "@RemoveTeaseTime(")).Contains("M") Then TeaseSeconds *= 60
                    If UCase(GetParentheses(StringClean, "@RemoveTeaseTime(")).Contains("H") Then TeaseSeconds *= 3600
                End If
                ssh.TeaseTick -= TeaseSeconds
                If ssh.TeaseTick < 5 Then ssh.TeaseTick = 5
            End If
            StringClean = StringClean.Replace("@RemoveTeaseTime(" & OriginalFlag & ")", "")
        End If

        If StringClean.Contains("@AddTeaseTime") Then
            If TeaseTimer.Enabled = True Then
                If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
                    If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick += ssh.randomizer.Next(10, 16) * 60
                    If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick += ssh.randomizer.Next(15, 21) * 60
                    If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick += ssh.randomizer.Next(20, 31) * 60
                    If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick += ssh.randomizer.Next(30, 46) * 60
                    If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick += ssh.randomizer.Next(45, 61) * 60
                Else
                    ssh.TeaseTick += ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
                End If
            End If
            StringClean = StringClean.Replace("@AddTeaseTime", "")
        End If

        If StringClean.Contains("@RemoveTeaseTime") Then
            If TeaseTimer.Enabled = True Then
                ssh.TeaseTick = ssh.TeaseTick / 2
            End If
            StringClean = StringClean.Replace("@RemoveTeaseTime", "")
        End If

        If StringClean.Contains("@PlaylistOff") Then
            ssh.Playlist = False
            StringClean = StringClean.Replace("@PlaylistOff", "")
        End If

        If StringClean.Contains("@RapidTextOn") Or StringClean.Contains("@RTOn") Then
            ssh.RapidFire = True
            StringClean = StringClean.Replace("@RapidTextOn", "")
            StringClean = StringClean.Replace("@RTOn", "")
        End If

        If StringClean.Contains("@RapidTextOff") Or StringClean.Contains("@RTOff") Then
            ssh.RapidFire = False
            StringClean = StringClean.Replace("@RapidTextOff", "")
            StringClean = StringClean.Replace("@RTOff", "")
        End If

        If StringClean.Contains("@AddContact1") Or StringClean.Contains("@RemoveContact1") Then
            ssh.AddContactTick = 2
            Contact1Timer.Start()
            StringClean = StringClean.Replace("@AddContact1", "")
            StringClean = StringClean.Replace("@RemoveContact1", "")
        End If

        If StringClean.Contains("@AddContact2") Or StringClean.Contains("@RemoveContact2") Then
            ssh.AddContactTick = 2
            Contact2Timer.Start()
            StringClean = StringClean.Replace("@AddContact2", "")
            StringClean = StringClean.Replace("@RemoveContact2", "")
        End If

        If StringClean.Contains("@AddContact3") Or StringClean.Contains("@RemoveContact3") Then
            ssh.AddContactTick = 2
            Contact3Timer.Start()
            StringClean = StringClean.Replace("@AddContact3", "")
            StringClean = StringClean.Replace("@RemoveContact3", "")
        End If

        If StringClean.Contains("@AddDomme") Or StringClean.Contains("@RemoveDomme") Then
            ssh.AddContactTick = 2
            DommeTimer.Start()
            StringClean = StringClean.Replace("@AddDomme", "")
            StringClean = StringClean.Replace("@RemoveDomme", "")
        End If


        If StringClean.Contains("@NullResponse") Then
            ssh.NullResponse = True
            StringClean = StringClean.Replace("@NullResponse", "")
        End If

VTSkip:

        If StringClean.Contains("@SpeedUpCheck") Then

            If ssh.AskedToSpeedUp = True Then
                ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpREPEAT.txt"
                StringClean = ResponseClean(StringClean)

            Else

                If StrokePace < 201 Then
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpMAX.txt"
                    StringClean = ResponseClean(StringClean)

                Else

                    Dim SpeedUpCheck As Integer

                    If FrmSettings.DominationLevel.Value = 1 Then SpeedUpCheck = 70
                    If FrmSettings.DominationLevel.Value = 2 Then SpeedUpCheck = 40
                    If FrmSettings.DominationLevel.Value = 3 Then SpeedUpCheck = 60
                    If FrmSettings.DominationLevel.Value = 4 Then SpeedUpCheck = 50
                    If FrmSettings.DominationLevel.Value = 5 Then SpeedUpCheck = 65

                    Dim SpeedUpVal As Integer = ssh.randomizer.Next(1, 101)

                    If SpeedUpVal > SpeedUpCheck Then

                        ' you can speed up
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpALLOWED.txt"

                    Else

                        ' you can't speed up
                        ssh.AskedToSpeedUp = True
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpDENIED.txt"

                    End If

                    StringClean = ResponseClean(StringClean)

                End If

            End If

            StringClean = StringClean.Replace("@SpeedUpCheck", "")
            GoTo RinseLatherRepeat
        End If


        If StringClean.Contains("@SlowDownCheck") Then

            If ssh.AskedToSpeedUp = True Then
                ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownREPEAT.txt"
                StringClean = ResponseClean(StringClean)

            Else

                If StrokePace > 999 Then
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownMIN.txt"
                    StringClean = ResponseClean(StringClean)

                Else

                    Dim SpeedUpCheck As Integer

                    If FrmSettings.DominationLevel.Value = 1 Then SpeedUpCheck = 70
                    If FrmSettings.DominationLevel.Value = 2 Then SpeedUpCheck = 40
                    If FrmSettings.DominationLevel.Value = 3 Then SpeedUpCheck = 60
                    If FrmSettings.DominationLevel.Value = 4 Then SpeedUpCheck = 50
                    If FrmSettings.DominationLevel.Value = 5 Then SpeedUpCheck = 65

                    Dim SpeedUpVal As Integer = ssh.randomizer.Next(1, 101)

                    If SpeedUpVal > SpeedUpCheck Then

                        ' you can speed up
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownALLOWED.txt"

                    Else

                        ' you can't speed up
                        ssh.AskedToSpeedUp = True
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownDENIED.txt"

                    End If

                    StringClean = ResponseClean(StringClean)

                End If

            End If

            StringClean = StringClean.Replace("@SlowDownCheck", "")
            GoTo RinseLatherRepeat

        End If


        If StringClean.Contains("@PlayRiskyPick") Then
            ssh.RiskyDeal = True
            'FrmCardList.RiskyRound += 1
            GamesWindow.TCGames.SelectTab(2)
            GamesWindow.Show()
            GamesWindow.Focus()
            GamesWindow.SetupRiskyPick()
            StringClean = StringClean.Replace("@PlayRiskyPick", "")
        End If

        If StringClean.Contains("@ChooseRiskyPick") Then
            ssh.RiskyDelay = True
            If GamesWindow.RiskyCase1Button.Text <> "" Then GamesWindow.RiskyCase1Button.Enabled = True
            If GamesWindow.RiskyCase2Button.Text <> "" Then GamesWindow.RiskyCase2Button.Enabled = True
            If GamesWindow.BTNRisk3.Text <> "" Then GamesWindow.BTNRisk3.Enabled = True
            If GamesWindow.BTNRisk4.Text <> "" Then GamesWindow.BTNRisk4.Enabled = True
            If GamesWindow.BTNRisk5.Text <> "" Then GamesWindow.BTNRisk5.Enabled = True
            If GamesWindow.BTNRisk6.Text <> "" Then GamesWindow.BTNRisk6.Enabled = True
            If GamesWindow.BTNRisk7.Text <> "" Then GamesWindow.BTNRisk7.Enabled = True
            If GamesWindow.BTNRisk8.Text <> "" Then GamesWindow.BTNRisk8.Enabled = True
            If GamesWindow.BTNRisk9.Text <> "" Then GamesWindow.BTNRisk9.Enabled = True
            If GamesWindow.BTNRisk10.Text <> "" Then GamesWindow.BTNRisk10.Enabled = True

            If GamesWindow.BTNRisk11.Text <> "" Then GamesWindow.BTNRisk11.Enabled = True
            If GamesWindow.BTNRisk12.Text <> "" Then GamesWindow.BTNRisk12.Enabled = True
            If GamesWindow.BTNRisk13.Text <> "" Then GamesWindow.BTNRisk13.Enabled = True
            If GamesWindow.BTNRisk14.Text <> "" Then GamesWindow.BTNRisk14.Enabled = True
            If GamesWindow.BTNRisk15.Text <> "" Then GamesWindow.BTNRisk15.Enabled = True
            If GamesWindow.BTNRisk16.Text <> "" Then GamesWindow.BTNRisk16.Enabled = True
            If GamesWindow.BTNRisk17.Text <> "" Then GamesWindow.BTNRisk17.Enabled = True
            If GamesWindow.BTNRisk18.Text <> "" Then GamesWindow.BTNRisk18.Enabled = True
            If GamesWindow.BTNRisk19.Text <> "" Then GamesWindow.BTNRisk19.Enabled = True
            If GamesWindow.BTNRisk20.Text <> "" Then GamesWindow.BTNRisk20.Enabled = True

            If GamesWindow.BTNRisk21.Text <> "" Then GamesWindow.BTNRisk21.Enabled = True
            If GamesWindow.BTNRisk22.Text <> "" Then GamesWindow.BTNRisk22.Enabled = True
            If GamesWindow.BTNRisk23.Text <> "" Then GamesWindow.BTNRisk23.Enabled = True
            If GamesWindow.BTNRisk24.Text <> "" Then GamesWindow.BTNRisk24.Enabled = True

            GamesWindow.RiskyChoiceCount = 0
            GamesWindow.RiskyRound += 1
            GamesWindow.RiskyPickCount = 0
            GamesWindow.RiskyChoices.Clear()
            GamesWindow.ClearCaseLabelsOffer()
            'FrmCardList.Show()
            'FrmCardList.TCGames.SelectTab(4)
            'FrmCardList.Focus()

            StringClean = StringClean.Replace("@ChooseRiskyPick", "")
        End If


        If StringClean.Contains("@CheckRiskyPick") Then
            'FrmCardList.Focus()
            GamesWindow.CheckRiskyPick()
            StringClean = StringClean.Replace("@CheckRiskyPick", "")
        End If

        If StringClean.Contains("@FinalRiskyPick") Then
            'FrmCardList.Focus()
            GamesWindow.BTNRiskIt.Text = "LAST CASE"
            GamesWindow.BTNPickIt.Text = "MY CASE"
            StringClean = StringClean.Replace("@FinalRiskyPick", "")
        End If

        If StringClean.Contains("@ClearRiskyLabels") Then
            'FrmCardList.Focus()
            GamesWindow.ClearCaseLabelsOffer()
            StringClean = StringClean.Replace("@ClearRiskyLabels", "")
        End If

        If StringClean.Contains("@RiskyPayout") Then
            If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutSmall.wav") Then
                GamesWindow.GameWMP.settings.setMode("loop", False)
                GamesWindow.GameWMP.settings.volume = 20
                GamesWindow.GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutSmall.wav"
            End If
            ssh.BronzeTokens += GamesWindow.TokensPaid
            GamesWindow.LBLRiskTokens.Text = ssh.BronzeTokens
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\RP_Edges", GamesWindow.EdgesOwed, False)
            StringClean = StringClean.Replace("@RiskyPayout", "")
        End If

        If StringClean.Contains("@CloseRiskyPick") Then
            GamesWindow.CloseRiskyPick()
            StringClean = StringClean.Replace("@CloseRiskyPick", "")
        End If

        If StringClean.Contains("@RevealLastCase") Then
            GamesWindow.RevealLastCase()
            StringClean = StringClean.Replace("@RevealLastCase", "")
        End If

        If StringClean.Contains("@RevealUserCase") Then
            GamesWindow.RevealUserCase()
            StringClean = StringClean.Replace("@RevealUserCase", "")
        End If

        If StringClean.Contains("@RiskyState") Then
            If GamesWindow.RiskyState = True Then
                ssh.FileGoto = "(Risky Game)"
            Else
                ssh.FileGoto = "(Risky Tease)"
            End If
            GamesWindow.RiskyState = False
            ssh.SkipGotoLine = True
            GetGoto()
            StringClean = StringClean.Replace("@RiskyState", "")
        End If

        If StringClean.Contains("@SystemMessage ") Then
            StringClean = StringClean.Replace("@SystemMessage ", "")
        End If

        If StringClean.Contains("@EmoteMessage ") Then
            StringClean = StringClean.Replace("@EmoteMessage ", "")
        End If

        If StringClean.Contains("@CallReturn(") Then


            ssh.ReturnFileText = ssh.FileText
            ssh.ReturnStrokeTauntVal = ssh.StrokeTauntVal
            GetSubState()

            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            CensorshipTimer.Stop()
            RLGLTimer.Stop()
            TnASlides.Stop()
            AvoidTheEdge.Stop()
            EdgeTauntTimer.Stop()
            HoldEdgeTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            AvoidTheEdgeTaunts.Stop()
            RLGLTauntTimer.Stop()
            VideoTauntTimer.Stop()
            EdgeCountTimer.Stop()

            ssh.CBTBallsActive = False
            ssh.CBTBallsFlag = False
            ssh.CBTCockFlag = False
            ssh.CBTBothActive = False
            ssh.CBTBothFlag = False
            ssh.CustomTaskActive = False

            If Not ssh.SubGaveUp Then
                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
            End If

            'StopEverything()
            ssh.ReturnFlag = True


            Dim CheckFlag As String = GetParentheses(StringClean, "@CallReturn(")
            Dim CallReplace As String = CheckFlag

            If CheckFlag.Contains(",") Then

                CheckFlag = FixCommas(CheckFlag)

                Dim CallSplit As String() = CheckFlag.Split(",")
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CallSplit(0)
                ssh.FileGoto = CallSplit(1)
                ssh.SkipGotoLine = True
                GetGoto()

            Else

                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag
                ssh.StrokeTauntVal = -1

            End If
            ssh.ScriptTick = 2
            ScriptTimer.Start()

            StringClean = StringClean.Replace("@CallReturn(" & CallReplace & ")", "")

        End If

        If StringClean.Contains("@Call(") Then

            Dim CheckFlag As String = GetParentheses(StringClean, "@Call(")
            Dim CallReplace As String = CheckFlag

            If CheckFlag.Contains(",") Then

                CheckFlag = FixCommas(CheckFlag)

                Dim CallSplit As String() = CheckFlag.Split(",")
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CallSplit(0)
                ssh.FileGoto = CallSplit(1)
                ssh.SkipGotoLine = True
                GetGoto()

            Else

                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag
                ssh.StrokeTauntVal = -1

            End If

            StringClean = StringClean.Replace("@Call(" & CallReplace & ")", "")

        End If


        If StringClean.Contains("@CallRandom(") Then

            Dim CheckFlag As String = GetParentheses(StringClean, "@CallRandom(")
            Dim CallReplace As String = CheckFlag

            If Not Directory.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag) Then
                MessageBox.Show(Me, "The current script attempted to @Call from a directory that does not exist!" & Environment.NewLine & Environment.NewLine &
                 Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim RandomFile As New List(Of String)
                RandomFile.Clear()
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag & "\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                    RandomFile.Add(foundFile)
                Next
                If RandomFile.Count < 1 Then
                    MessageBox.Show(Me, "The current script attempted to @Call from a directory that does not contain any scripts!" & Environment.NewLine & Environment.NewLine &
                      Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    ssh.FileText = RandomFile(ssh.randomizer.Next(0, RandomFile.Count))
                    ssh.StrokeTauntVal = -1
                End If
            End If
            StringClean = StringClean.Replace("@CallRandom(" & CallReplace & ")", "")
        End If

        If StringClean.Contains("@InterruptsOff") Then
            ssh.DoNotDisturb = True
            StringClean = StringClean.Replace("@InterruptsOff", "")
        End If

        If StringClean.Contains("@InterruptsOn") Then
            ssh.DoNotDisturb = False
            StringClean = StringClean.Replace("@InterruptsOn", "")
        End If


        If StringClean.Contains("@NoTypo") Then
            ssh.TypoSwitch = 0
            StringClean = StringClean.Replace("@NoTypo", "")
        End If

        If StringClean.Contains("@ForceTypo") Then
            ssh.TypoSwitch = 2
            StringClean = StringClean.Replace("@ForceTypo", "")
        End If

        If StringClean.Contains("@TyposOff") Then
            ssh.TyposDisabled = True
            StringClean = StringClean.Replace("@TyposOff", "")
        End If

        If StringClean.Contains("@TyposOn") Then
            ssh.TyposDisabled = False
            StringClean = StringClean.Replace("@TyposOn", "")
        End If

        If StringClean.Contains("@GoodMood(") Then

            Dim MoodFlag As String = GetParentheses(StringClean, "@GoodMood(")

            If ssh.DommeMood > FrmSettings.NBDomMoodMax.Value Then
                ssh.FileGoto = MoodFlag
                ssh.SkipGotoLine = True
                GetGoto()
            End If

            StringClean = StringClean.Replace("@GoodMood(" & MoodFlag & ")", "")
        End If

        If StringClean.Contains("@BadMood(") Then

            Dim MoodFlag As String = GetParentheses(StringClean, "@BadMood(")

            If ssh.DommeMood < FrmSettings.NBDomMoodMin.Value Then
                ssh.FileGoto = MoodFlag
                ssh.SkipGotoLine = True
                GetGoto()
            End If

            StringClean = StringClean.Replace("@BadMood(" & MoodFlag & ")", "")
        End If

        If StringClean.Contains("@NeutralMood(") Then

            Dim MoodFlag As String = GetParentheses(StringClean, "@NeutralMood(")

            If ssh.DommeMood >= FrmSettings.NBDomMoodMin.Value And ssh.DommeMood <= FrmSettings.NBDomMoodMax.Value Then
                ssh.FileGoto = MoodFlag
                ssh.SkipGotoLine = True
                GetGoto()
            End If

            StringClean = StringClean.Replace("@NeutralMood(" & MoodFlag & ")", "")
        End If

        If StringClean.Contains("@MoodUp") Then
            ssh.DommeMood += 1
            If ssh.DommeMood > 10 Then ssh.DommeMood = 10
            StringClean = StringClean.Replace("@MoodUp", "")
        End If

        If StringClean.Contains("@MoodDown") Then
            ssh.DommeMood -= 1
            If ssh.DommeMood < 1 Then ssh.DommeMood = 1
            StringClean = StringClean.Replace("@MoodDown", "")
        End If

        If StringClean.Contains("@MoodBest") Then
            ssh.DommeMood = 10
            StringClean = StringClean.Replace("@MoodBest", "")
        End If

        If StringClean.Contains("@MoodWorst") Then
            ssh.DommeMood = 1
            StringClean = StringClean.Replace("@MoodWorst", "")
        End If

        If StringClean.Contains("@Timeout(") Then

            Dim TimeFlag As String = GetParentheses(StringClean, "@Timeout(")
            Dim OriginalFlag As String = TimeFlag

            TimeFlag = FixCommas(TimeFlag)

            Dim TimeArray As String() = TimeFlag.Split(",")

            ssh.FileGoto = TimeArray(1)
            ssh.TimeoutTick = Val(TimeArray(0))
            TimeoutTimer.Start()

            StringClean = StringClean.Replace("@Timeout(" & OriginalFlag & ")", "")
        End If

        If StringClean.Contains("@BallTorture+1") Then
            ssh.CBTBallsCount += 1
            StringClean = StringClean.Replace("@BallTorture+1", "")
        End If

        If StringClean.Contains("@CockTorture+1") Then
            ssh.CBTCockCount += 1
            StringClean = StringClean.Replace("@CockTorture+1", "")
        End If


        If StringClean.Contains("@EndTaunts") Then
            ssh.StrokeTick = 0
            StringClean = StringClean.Replace("@EndTaunts", "")
        End If


        If StringClean.Contains("@ResponseYes(") Then
            ssh.ResponseYes = GetParentheses(StringClean, "@ResponseYes(")
            StringClean = StringClean.Replace("@ResponseYes(" & GetParentheses(StringClean, "@ResponseYes(") & ")", "")
        End If

        If StringClean.Contains("@ResponseNo(") Then
            ssh.ResponseNo = GetParentheses(StringClean, "@ResponseNo(")
            StringClean = StringClean.Replace("@ResponseNo(" & GetParentheses(StringClean, "@ResponseNo(") & ")", "")
        End If


        If StringClean.Contains("@SetModule(") Then
            Dim TempMod As String = GetParentheses(StringClean, "@SetModule(")

            If TempMod.Contains(",") Then
                TempMod = FixCommas(TempMod)
                Dim TempArray As String() = TempMod.Split(",")
                TempMod = TempArray(0)
                ssh.SetModuleGoto = TempArray(1)

            End If


            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Modules\" & TempMod & ".txt") Then
                ssh.SetModule = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Modules\" & TempMod & ".txt"
            End If
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Modules\" & TempMod & ".txt") Then
                ssh.SetModule = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Modules\" & TempMod & ".txt"
            End If

            If ssh.SetModule = "" Then ssh.SetModuleGoto = ""

            StringClean = StringClean.Replace("@SetModule(" & GetParentheses(StringClean, "@SetModule(") & ")", "")
        End If

        If StringClean.Contains("@SetLink(") Then
            Dim TempMod As String = GetParentheses(StringClean, "@SetLink(")
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Link\" & TempMod & ".txt") Then
                ssh.SetLink = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Link\" & TempMod & ".txt"
            End If
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Link\" & TempMod & ".txt") Then
                ssh.SetLink = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Link\" & TempMod & ".txt"
            End If
            StringClean = StringClean.Replace("@SetLink(" & GetParentheses(StringClean, "@SetLink(") & ")", "")
        End If


        If StringClean.Contains("@FollowUp(") And ssh.FollowUp = "" Then
            ssh.FollowUp = GetParentheses(StringClean, "@FollowUp(")
            StringClean = StringClean.Replace("@FollowUp(" & ssh.FollowUp & ")", "")
        End If


        If StringClean.Contains("@FollowUp") And ssh.FollowUp = "" Then

            Dim FollowTemp As String
            Dim TSStartIndex As Integer
            Dim TSEndIndex As Integer

            TSStartIndex = StringClean.IndexOf("@FollowUp") + 9
            TSEndIndex = StringClean.IndexOf("@FollowUp") + 11

            FollowTemp = StringClean.Substring(TSStartIndex, TSEndIndex - TSStartIndex).Trim

            Dim FollowVal As Integer

            FollowVal = Val(FollowTemp)

            ssh.TempVal = ssh.randomizer.Next(1, 101)

            Dim FollowLineTemp As String
            FollowLineTemp = GetParentheses(StringClean, "@FollowUp" & FollowTemp & "(")

            If ssh.TempVal <= FollowVal Then ssh.FollowUp = FollowLineTemp

            StringClean = StringClean.Replace("@FollowUp" & FollowTemp & "(" & FollowLineTemp & ")", "")

        End If

        If StringClean.Contains("@Worship(") Then
            Dim WorshipTemp As String = GetParentheses(StringClean, "@Worship(")
            If UCase(WorshipTemp) = "ASS" Then ssh.WorshipTarget = "Ass"
            If UCase(WorshipTemp) = "BOOBS" Then ssh.WorshipTarget = "Boobs"
            If UCase(WorshipTemp) = "PUSSY" Then ssh.WorshipTarget = "Pussy"
            ssh.WorshipMode = True
            StringClean = StringClean.Replace("@Worship(" & GetParentheses(StringClean, "@Worship(") & ")", "")
        End If

        If StringClean.Contains("@WorshipOn") Then
            ssh.WorshipMode = True
            StringClean = StringClean.Replace("@WorshipOn", "")
        End If

        If StringClean.Contains("@WorshipOff") Then
            ssh.WorshipMode = False
            ssh.WorshipTarget = ""
            StringClean = StringClean.Replace("@WorshipOff", "")
        End If

        ' If StringClean.Contains("@AssWorship") Then
        'WorshipTarget = "Ass"
        'StringClean = StringClean.Replace("@AssWorship", "")
        'End If

        'If StringClean.Contains("@BoobWorship") Then
        'WorshipTarget = "Boobs"
        'StringClean = StringClean.Replace("@BoobWorship", "")
        'End If

        'If StringClean.Contains("@PussyWorship") Then
        'WorshipTarget = "Pussy"
        'StringClean = StringClean.Replace("@PussyWorship", "")
        'End If

        If StringClean.Contains("@ClearWorship") Then
            ssh.WorshipTarget = ""
            StringClean = StringClean.Replace("@ClearWorship", "")
        End If








        If StringClean.Contains("@MiniScript(") Then

            Dim MiniTemp As String = GetParentheses(StringClean, "@MiniScript(")


            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\MiniScripts\" & MiniTemp & ".txt") Then ' And MiniScript = False Then
                ssh.MiniScript = True
                ssh.MiniScriptText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\MiniScripts\" & MiniTemp & ".txt"
                ssh.MiniTauntVal = -1
                ssh.MiniTimerCheck = ScriptTimer.Enabled
                ssh.ScriptTick = 2
                ScriptTimer.Start()
            End If

            StringClean = StringClean.Replace("@MiniScript(" & MiniTemp & ")", "")
        End If


        If StringClean.Contains("@CheckFile(") Then

            Dim FileFlag As String = GetParentheses(StringClean, "@CheckFile(")
            FileFlag = FixCommas(FileFlag)

            Dim FileArray As String() = FileFlag.Split(",")

            If FileArray.Count = 2 Or FileArray.Count = 3 Then

                If File.Exists(FileArray(0)) Then
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = FileArray(1)
                    GetGoto()
                End If

                If Not File.Exists(FileArray(0)) And FileArray.Count = 3 Then
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = FileArray(2)
                    GetGoto()
                End If

            End If

            StringClean = StringClean.Replace("@CheckFile(" & GetParentheses(StringClean, "@CheckFile(") & ")", "")
        End If


        If StringClean.Contains("@YesMode(") Then

            Dim YesFlag As String = GetParentheses(StringClean, "@YesMode(")
            YesFlag = FixCommas(YesFlag)
            Dim YesArray As String() = YesFlag.Split(",")

            If UCase(YesArray(0)).Contains("GOTO") Then
                ssh.YesGoto = True
                ssh.YesGotoLine = YesArray(1)
            End If

            If UCase(YesArray(0)).Contains("VIDEO") Then
                ssh.YesVideo = True
                ssh.YesGotoLine = YesArray(1)
            End If

            If UCase(YesArray(0)).Contains("NORMAL") Then
                ssh.YesGoto = False
                ssh.YesVideo = False
            End If

            StringClean = StringClean.Replace("@YesMode(" & GetParentheses(StringClean, "@YesMode(") & ")", "")
        End If

        If StringClean.Contains("@NoMode(") Then

            Dim NoFlag As String = GetParentheses(StringClean, "@NoMode(")
            NoFlag = FixCommas(NoFlag)
            Dim NoArray As String() = NoFlag.Split(",")

            If UCase(NoArray(0)).Contains("GOTO") Then
                ssh.NoGoto = True
                ssh.NoGotoLine = NoArray(1)
            End If

            If UCase(NoArray(0)).Contains("VIDEO") Then
                ssh.NoVideo_Mode = True
                ssh.NoGotoLine = NoArray(1)
            End If

            If UCase(NoArray(0)).Contains("NORMAL") Then
                ssh.NoGoto = False
                ssh.NoVideo_Mode = False
            End If

            StringClean = StringClean.Replace("@NoMode(" & GetParentheses(StringClean, "@NoMode(") & ")", "")
        End If

        If StringClean.Contains("@CameMode(") Then

            Dim CameFlag As String = GetParentheses(StringClean, "@CameMode(")
            CameFlag = FixCommas(CameFlag)
            Dim CameArray As String() = CameFlag.Split(",")

            If UCase(CameArray(0)).Contains("GOTO") Then
                ssh.CameGoto = True
                ssh.CameGotoLine = CameArray(1)
            End If

            If UCase(CameArray(0)).Contains("MESSAGE") Then
                ssh.CameMessage = True
                ssh.CameMessageText = CameArray(1)
            End If

            If UCase(CameArray(0)).Contains("VIDEO") Then
                ssh.CameVideo = True
                ssh.CameGotoLine = CameArray(1)
            End If

            If UCase(CameArray(0)).Contains("NORMAL") Then
                ssh.CameGoto = False
                ssh.CameMessage = False
                ssh.CameVideo = False
            End If

            StringClean = StringClean.Replace("@CameMode(" & GetParentheses(StringClean, "@CameMode(") & ")", "")
        End If

        If StringClean.Contains("@RuinedMode(") Then

            Dim RuinedFlag As String = GetParentheses(StringClean, "@RuinedMode(")
            RuinedFlag = FixCommas(RuinedFlag)
            Dim RuinedArray As String() = RuinedFlag.Split(",")

            If UCase(RuinedArray(0)).Contains("GOTO") Then
                ssh.RuinedGoto = True
                ssh.RuinedGotoLine = RuinedArray(1)
            End If

            If UCase(RuinedArray(0)).Contains("MESSAGE") Then
                ssh.RuinedMessage = True
                ssh.RuinedMessageText = RuinedArray(1)
            End If

            If UCase(RuinedArray(0)).Contains("VIDEO") Then
                ssh.RuinedVideo = True
                ssh.RuinedGotoLine = RuinedArray(1)
            End If

            If UCase(RuinedArray(0)).Contains("NORMAL") Then
                ssh.RuinedGoto = False
                ssh.RuinedMessage = False
                ssh.RuinedVideo = False
            End If

            StringClean = StringClean.Replace("@RuinedMode(" & GetParentheses(StringClean, "@RuinedMode(") & ")", "")
        End If

        If StringClean.Contains("@CustomMode(") Then

            Dim CustomFlag As String = GetParentheses(StringClean, "@CustomMode(")
            CustomFlag = FixCommas(CustomFlag)
            Dim CustomArray As String() = CustomFlag.Split(",")

            If CustomArray.Count = 3 Then

                If ssh.Modes.Keys.Contains(CustomArray(0)) Then ssh.Modes.Remove(CustomArray(0))

                Dim NewMode As New Mode
                NewMode.Keyword = CustomArray(0)
                NewMode.Type = CustomArray(1)
                NewMode.GotoLine = CustomArray(2)
                ssh.Modes.Add(CustomArray(0), NewMode)
            End If

            If CustomArray.Count = 2 Then
                If CustomArray(1).ToUpper.Contains("NORMAL") Then
                    If ssh.Modes.Keys.Contains(CustomArray(0)) Then
                        ssh.Modes.Remove(CustomArray(0))
                    End If
                End If
            End If

            StringClean = StringClean.Replace("@CustomMode(" & GetParentheses(StringClean, "@CustomMode(") & ")", "")

        End If


        If StringClean.Contains("@ClearModes") Then
            ClearModes()
            StringClean = StringClean.Replace("@ClearModes", "")
        End If


        If StringClean.Contains("@LockVideo") Then
            ssh.LockVideo = True
            StringClean = StringClean.Replace("@LockVideo", "")
        End If

        If StringClean.Contains("@UnlockVideo") Then
            ssh.LockVideo = False
            mainPictureBox.Visible = True
            DomWMP.Visible = False
            StringClean = StringClean.Replace("@UnlockVideo", "")
        End If

        If StringClean.Contains("@ClearChat") Then
            ClearChat()
            StringClean = StringClean.Replace("@ClearChat", "")
        End If

        If StringClean.Contains("@ChatImage[") Then
            Dim ImageDir As String = Application.StartupPath & "\Images\" & GetParentheses(StringClean, "@ChatImage[")
            ImageDir = ImageDir.Replace("/", "\")
            ImageDir = ImageDir.Replace("\\", "\")


            If File.Exists(ImageDir.Split(",")(0)) Then

                If GetCharCount(ImageDir, ",") = 2 Then

                    Dim PicAttributes As String() = GetArrayString(ImageDir)

                    StringClean = StringClean.Replace("@ChatImage[" & GetParentheses(StringClean, "@ChatImage[") & "]", "<img id=""ChatPic"" src=""" & PicAttributes(0) & """ width=" & PicAttributes(1) &
                     " height=" & PicAttributes(2) & """/>")

                Else
                    StringClean = StringClean.Replace("@ChatImage[" & GetParentheses(StringClean, "@ChatImage[") & "]", "<img id=""ChatPic"" src=""" & ImageDir & """/>")
                End If

            Else
                StringClean = StringClean.Replace("@ChatImage[" & GetParentheses(StringClean, "@ChatImage[") & "]", "")
            End If
        End If

        If StringClean.Contains("@Debug") Then

            'Dim wy As Long = DateDiff(DateInterval.Day, Val(GetVariable("TB_AFKSlideshow")), Date.Now)

            MsgBox(GetParentheses("Testing If - @If[42]>[7]Then(Go here) okay", "@If["))
            MsgBox(GetParentheses("Testing If2 - @If[42]>[7]Then(Go here) okay", "@If[", 2))
            MsgBox(GetParentheses("Testing If2 - @If(candle) okay", "@If("))
            MsgBox(GetParentheses("Testing If2 - @If(candle)and(wax) okay", "@If(", 2))


            'MsgBox(GetVariable("Sys_EndTotal") & " less than 30? " & CheckVariable("@Variable[Sys_EndTotal]<[30] blah blah blah"))
            StringClean = StringClean.Replace("@Debug", "")
        End If

        If StringClean.Contains("@CheckBnB") Then
            If Not GetImageData(ImageGenre.Boobs).IsAvailable Or Not GetImageData(ImageGenre.Butt).IsAvailable Then
                ssh.FileGoto = "(No BnB)"
                ssh.SkipGotoLine = True
                GetGoto()
            End If
            StringClean = StringClean.Replace("@CheckBnB", "")
        End If

        If StringClean.Contains("@CheckStrokingState") Then
            'If SubStroking = True Then
            If ssh.SubStroking = True Or ssh.SubEdging = True Or ssh.SubHoldingEdge = True Then
                ssh.FileGoto = "(Sub Stroking)"
            Else
                ssh.FileGoto = "(Sub Not Stroking)"
            End If
            ssh.SkipGotoLine = True
            GetGoto()
            StringClean = StringClean.Replace("@CheckStrokingState", "")
        End If

        'The @SetGroup Command is a defunct Command that was created when implementing new Glitter features. It has no use in the current build of Tease AI.

        If StringClean.Contains("@SetGroup(") Then

            Dim WF As String = UCase(GetParentheses(StringClean, "@SetGroup("))

            If WF.Contains("D") And Not WF.Contains("1") And Not WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D"
            If WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D1"
            If WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D12"
            If WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D13"
            If WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D23"
            If WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D123"

            If Not WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "1"
            If Not WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "12"
            If Not WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "123"

            If WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D2"
            If Not WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "2"
            If Not WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "23"

            If WF.Contains("D") And Not WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D3"
            If Not WF.Contains("D") And Not WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "3"
            If Not WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "13"

            StringClean = StringClean.Replace("@SetGroup(" & WF & ")", "")

        End If

        Return StringClean

    End Function

    Private Function ConvertToTokens(tokenData As String) As Token
        Throw New NotImplementedException()
    End Function
    Public Class Token
        Public Property Amount As Integer
        Public Property Denomination As Constants.TokenDenomination
    End Class
#Region "-------------------------------------------- Webtoy --------------------------------------------"

    Public Sub ActivateWebToy()

        If FrmSettings.TBWebStart.Text <> "" Then
            Try
                FrmSettings.WebToy.Navigate(FrmSettings.TBWebStart.Text)
            Catch
            End Try
        End If

    End Sub

    Public Sub DeactivateWebToy()

        If FrmSettings.TBWebStart.Text <> "" Then
            Try
                FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text)
            Catch
            End Try
        End If

    End Sub

#End Region ' WebToy

#Region "-------------------------------- Script: Flags/Dates/Variables ---------------------------------"
#Region "------------------------------------- Script-Variables -----------------------------------------"

    Public Function SetVariable(ByVal VarName As String, ByVal VarValue As String)

        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName, VarValue, False)

    End Function

    Public Function DeleteVariable(ByVal FlagDir As String)

        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & FlagDir) Then _
                    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & FlagDir)

    End Function

    Public Function ChangeVariable(ByVal ChangeVar As String, ByVal ChangeVal1 As String, ByVal ChangeOperator As String, ByVal ChangeVal2 As String)

        Dim Val1 As Integer
        Dim Val2 As Integer

        If IsNumeric(ChangeVal1) = False Then
            'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal1) Then
                Val1 = Val(TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal1))
            Else
                Val1 = 0
            End If
        Else
            Val1 = Val(ChangeVal1)
        End If

        If IsNumeric(ChangeVal2) = False Then
            'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal2) Then
                Val2 = Val(TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal2))
            Else
                Val2 = 0
            End If
        Else
            Val2 = Val(ChangeVal2)
        End If

        ssh.ScriptOperator = "Null"
        If ChangeOperator.Contains("+") Then ssh.ScriptOperator = "Add"
        If ChangeOperator.Contains("-") Then ssh.ScriptOperator = "Subtract"
        If ChangeOperator.Contains("*") Then ssh.ScriptOperator = "Multiply"
        If ChangeOperator.Contains("/") Then ssh.ScriptOperator = "Divide"

        Dim ChangeVal As Integer = 0

        If ssh.ScriptOperator = "Add" Then ChangeVal = Val1 + Val2
        If ssh.ScriptOperator = "Subtract" Then ChangeVal = Val1 - Val2
        If ssh.ScriptOperator = "Multiply" Then ChangeVal = Val1 * Val2
        If ssh.ScriptOperator = "Divide" Then ChangeVal = Val1 / Val2

        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVar, ChangeVal, False)

    End Function

    Public Function GetVariable(ByVal VarName As String) As String

        Dim VarGet As String
        'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName) Then
            '### DEBUG

            ' VarGet = Val(VarReader.ReadLine())

            VarGet = TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName)
        Else
            VarGet = 0
        End If

        Return VarGet


    End Function

    Public Function CheckVariable(ByVal StringCLean As String) As Boolean

        Do

            Dim SCIfVar As String() = Split(StringCLean)
            Dim SCGotVar As String = "Null"

            For i As Integer = 0 To SCIfVar.Length - 1
                If SCIfVar(i).Contains("@Variable[") Then
                    Dim IFJoin As Integer = 0
                    If Not SCIfVar(i).Contains("] ") Then
                        Do
                            IFJoin += 1
                            SCIfVar(i) = SCIfVar(i) & " " & SCIfVar(i + IFJoin)
                            SCIfVar(i + IFJoin) = ""
                        Loop Until SCIfVar(i).Contains("] ") Or SCIfVar(i).EndsWith("]")
                    End If
                    SCGotVar = SCIfVar(i).Trim
                    SCIfVar(i) = ""
                    StringCLean = Join(SCIfVar)
                    Do
                        StringCLean = StringCLean.Replace("  ", " ")
                    Loop Until Not StringCLean.Contains("  ")
                    Exit For
                End If
            Next

            If SCGotVar.Contains("]And[") Then

                Dim AndCheck As Boolean = True

                For x As Integer = 0 To SCGotVar.Replace("]And[", "").Count - 1
                    If GetIf("[" & GetParentheses(SCGotVar, "@Variable[", 2) & "]") = False Then
                        AndCheck = False
                        Exit For
                    End If
                    SCGotVar = SCGotVar.Replace("[" & GetParentheses(SCGotVar, "@Variable[", 2) & "]And", "")
                Next

                Return AndCheck

            ElseIf SCGotVar.Contains("]Or[") Then

                Dim OrCheck As Boolean = False

                For x As Integer = 0 To SCGotVar.Replace("]Or[", "").Count - 1
                    If GetIf("[" & GetParentheses(SCGotVar, "@Variable[", 2) & "]") = True Then
                        OrCheck = True
                        Exit For
                    End If
                    SCGotVar = SCGotVar.Replace("[" & GetParentheses(SCGotVar, "@Variable[", 2) & "]Or", "")
                Next

                Return OrCheck

            Else

                If GetIf("[" & GetParentheses(SCGotVar, "@Variable[", 2) & "]") = True Then

                    Return True

                Else

                    Return False

                End If

            End If

        Loop Until Not StringCLean.Contains("@Variable")


    End Function

#End Region ' Script-Variables

#Region "---------------------------------------- Script-Dates ------------------------------------------"

    Public Function GetDate(ByVal VarName As String) As Date

        Dim VarGet As String
        'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName) Then
            VarGet = CDate(TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName))
        Else
            VarGet = FormatDateTime(Now, DateFormat.GeneralDate)
        End If

        Return VarGet


    End Function

    Public Function GetTime(ByVal VarName As String) As Date

        Dim VarGet As String
        'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName) Then
            VarGet = CDate(TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName))
        Else
            VarGet = FormatDateTime(Now, DateFormat.LongTime)
        End If

        Return VarGet


    End Function

    Public Function CheckDateList(ByVal DateString As String, Optional ByVal Linear As Boolean = False) As Boolean

        Dim DateFlag As String = GetParentheses(DateString, "@CheckDate(")

        If DateFlag.Contains(",") Then

            DateFlag = FixCommas(DateFlag)

            Dim DateArray() As String = DateFlag.Split(",")
            Dim DDiff As Long = 18855881
            Dim DDiff2 As Long = 18855881

            Dim DCompare As Long
            Dim DCompare2 As Long

            If Linear = False Then

                If DateArray.Count = 2 Then
                    DDiff = GetDateDifference(DateArray(0), DateArray(1))
                    DCompare = GetDateCompare(DateArray(0), DateArray(1))
                    If DDiff >= DCompare Then Return True
                    Return False
                End If

                If DateArray.Count = 3 Then
                    DDiff = GetDateDifference(DateArray(0), DateArray(1))
                    DCompare = GetDateCompare(DateArray(0), DateArray(1))
                    DDiff2 = GetDateDifference(DateArray(0), DateArray(2))
                    DCompare2 = GetDateCompare(DateArray(0), DateArray(2))
                    If DDiff >= DCompare And DDiff2 <= DCompare2 Then Return True
                    Return False
                End If

            Else

                If DateArray.Count = 2 Then
                    If CompareDatesWithTime(GetDate(DateArray(0))) <> 1 Then Return True
                    Return False
                End If

                If DateArray.Count = 3 Then
                    DDiff = GetDateDifference(DateArray(0), DateArray(1))
                    DCompare = GetDateCompare(DateArray(0), DateArray(1))
                    If DDiff >= DCompare Then Return True
                    Return False
                End If

                If DateArray.Count = 4 Then
                    DDiff = GetDateDifference(DateArray(0), DateArray(1))
                    DCompare = GetDateCompare(DateArray(0), DateArray(1))
                    DDiff2 = GetDateDifference(DateArray(0), DateArray(2))
                    DCompare2 = GetDateCompare(DateArray(0), DateArray(2))
                    If DDiff >= DCompare And DDiff2 <= DCompare2 Then Return True
                    Return False
                End If

            End If

        Else
            If CompareDatesWithTime(GetDate(DateFlag)) <> 1 Then Return True
            Return False
        End If

        Return False

    End Function

    Public Function GetDateDifference(ByVal DateVar As String, ByVal DateString As String) As Long

        Dim DDiff As Long = 0

        If UCase(DateString).Contains("SECOND") Then DDiff = DateDiff(DateInterval.Second, GetDate(DateVar), Now)
        If UCase(DateString).Contains("MINUTE") Then DDiff = DateDiff(DateInterval.Minute, GetDate(DateVar), Now) * 60
        If UCase(DateString).Contains("HOUR") Then DDiff = DateDiff(DateInterval.Hour, GetDate(DateVar), Now) * 3600
        If UCase(DateString).Contains("DAY") Then DDiff = DateDiff(DateInterval.Day, GetDate(DateVar), Now) * 86400
        If UCase(DateString).Contains("WEEK") Then DDiff = DateDiff(DateInterval.Day, GetDate(DateVar), Now) * 604800
        If UCase(DateString).Contains("MONTH") Then DDiff = DateDiff(DateInterval.Month, GetDate(DateVar), Now) * 2629746
        If UCase(DateString).Contains("YEAR") Then DDiff = DateDiff(DateInterval.Year, GetDate(DateVar), Now) * 31536000

        Return DDiff

    End Function

    Public Function GetDateCompare(ByVal DateVar As String, ByVal DateString As String) As Long

        Dim DDiff As Long = 0
        Dim Amount As Long = Val(DateString)

        If UCase(DateString).Contains("SECOND") Then DDiff = Amount
        If UCase(DateString).Contains("MINUTE") Then DDiff = Amount * 60
        If UCase(DateString).Contains("HOUR") Then DDiff = Amount * 3600
        If UCase(DateString).Contains("DAY") Then DDiff = Amount * 86400
        If UCase(DateString).Contains("WEEK") Then DDiff = Amount * 604800
        If UCase(DateString).Contains("MONTH") Then DDiff = Amount * 2629746
        If UCase(DateString).Contains("YEAR") Then DDiff = Amount * 31536000

        Return DDiff

    End Function

#End Region ' Script-Dates

#End Region ' Flags/Dates/Variables

    Public Function GetParentheses(ByVal ParenCheck As String, ByVal CommandCheck As String, Optional ByVal Iterations As Integer = 1) As String



        Dim ParenFlag As String = ParenCheck
        Dim ParenStart As Integer = ParenFlag.IndexOf(CommandCheck) + CommandCheck.Length
        'githib patch Dim ParenType As String

        Dim ParenType As String = Nothing

        ' #### CHECK ALL GETPAREN!
        'If CommandCheck.Substring(CommandCheck.Length - 1, 1) = "(" Then ParenType = ")"
        'If CommandCheck.Substring(CommandCheck.Length - 1, 1) = "[" Then ParenType = "]"

        If CommandCheck.Substring(CommandCheck.Length - 1, 1) = "(" Then
            ParenType = ")"
        End If
        If CommandCheck.Substring(CommandCheck.Length - 1, 1) = "[" Then
            ParenType = "]"
        End If



        'ParenFlag = ParenFlag.Substring(ParenStart, ParenFlag.Length - ParenStart)

        'Dim ParenEnd As Integer = ParenFlag.IndexOf(ParenType, ParenStart)
        Dim ParenEnd As Integer = GetNthIndex(ParenFlag, ParenType, ParenStart, Iterations)

        If ParenEnd = -1 Then ParenEnd = ParenFlag.Length
        ParenFlag = ParenFlag.Substring(ParenStart, ParenEnd - ParenStart)

        'ParenFlag = ParenFlag.Split(")")(0)
        'ParenFlag = ParenFlag.Split(ParenType)(0)
        'ParenFlag = ParenFlag.Replace(ParenType, "")
        'ParenFlag = ParenFlag.Substring(0, ParenFlag.Length - 1)
        Return ParenFlag


    End Function

    Public Function GetNthIndex(searchString As String, charToFind As Char, startIndex As Integer, n As Integer) As Integer
        Dim charIndexPair = searchString.Select(Function(c, i) New With {.Character = c, .Index = i}) _
                                        .Where(Function(x) x.Character = charToFind And x.Index > startIndex) _
                                        .ElementAtOrDefault(n - 1)
        Return If(charIndexPair IsNot Nothing, charIndexPair.Index, -1)
    End Function

    Public Function FixCommas(ByVal CommaString) As String

        CommaString = CommaString.replace(", ", ",")
        CommaString = CommaString.replace(" ,", ",")

        Return CommaString

    End Function

    Public Function GetEdgeHoldMinutes(ByVal HoldTime As Integer) As Boolean

        Dim HoldEdgeCheck As Boolean = False

        If ssh.HoldEdgeTime >= HoldTime * 60 Then HoldEdgeCheck = True

        Return HoldEdgeCheck


    End Function

    Public Function GetLocalImage(Optional ByVal IncludeTags As List(Of String) = Nothing,
                                  Optional ByVal ExcludeTags As List(Of String) = Nothing) As String


        If File.Exists(Application.StartupPath & "\Images\System\LocalImageTags.txt") Then
            ' Read all lines of given file.
            ssh.LocalTagImageList = Txt2List(Application.StartupPath & "\Images\System\LocalImageTags.txt")


            Dim ValidExt As String() = Split(".jpg|.jpeg|.bmp|.png|.gif", "|")

            ssh.LocalTagImageList.RemoveAll(Function(x)
                                                ' Remove if given include tags are missing
                                                If IncludeTags IsNot Nothing Then
                                                    For Each tag As String In IncludeTags
                                                        If Not x.Contains(tag.Replace("@", "")) Then Return True
                                                    Next
                                                End If
                                                ' Remove if given exclude tags are present
                                                If ExcludeTags IsNot Nothing Then
                                                    For Each tag As String In ExcludeTags
                                                        If x.Contains(tag.Replace("@", "")) Then Return True
                                                    Next
                                                End If
                                                ' Remove all without valid extension
                                                Dim Ext As String = Path.GetExtension(Split(x)(0)).ToLower
                                                If Not ValidExt.Contains(Ext) Then Return True
                                                'Everything fine keep file
                                                Return False
                                            End Function)

            Do While ssh.LocalTagImageList.Count > 0
                Dim rndNumber As Integer = ssh.randomizer.Next(0, ssh.LocalTagImageList.Count)
                ' TODO: GetLocalImage: Add space char (0x20) support for filepaths.
                Dim Filepath As String = Split(ssh.LocalTagImageList(rndNumber))(0)

                If Directory.Exists(Path.GetDirectoryName(Filepath)) _
                AndAlso File.Exists(Filepath) Then
                    Return Filepath
                Else
                    ssh.LocalTagImageList.RemoveAt(rndNumber)
                End If
            Loop
        End If

        Return String.Empty
    End Function

    Public Function GetLocalImage(ByVal LocTag As String) As String
        'TODO-Next: @ImageTag() Implement optimized @ShowTaggedImage code.
        If File.Exists(Application.StartupPath & "\Images\System\LocalImageTags.txt") Then


            Dim TagList As New List(Of String)
            TagList = Txt2List(Application.StartupPath & "\Images\System\LocalImageTags.txt")

            LocTag = LocTag.Replace(" ,", ",")
            LocTag = LocTag.Replace(", ", ",")

            Dim LocTagArray As String() = LocTag.Split(",")

            Dim LocTag1 As String = " "
            Dim LocTag2 As String = " "
            Dim LocTag3 As String = " "

            For i As Integer = 0 To LocTagArray.Count - 1
                If i = 0 Then LocTag1 = "Tag" & LocTagArray(0)
                If i = 1 Then LocTag2 = "Tag" & LocTagArray(1)
                If i = 2 Then LocTag3 = "Tag" & LocTagArray(2)
            Next


            Dim TaggedList As New List(Of String)

            For i As Integer = 0 To TagList.Count - 1
                If TagList(i).Contains(LocTag1) And TagList(i).Contains(LocTag2) And TagList(i).Contains(LocTag3) Then
                    TaggedList.Add(TagList(i))
                End If
            Next

            If TaggedList.Count > 0 Then

                Dim PicArray As String() = TaggedList(ssh.randomizer.Next(0, TaggedList.Count)).Split
                Dim PicDir As String = ""

                For p As Integer = 0 To PicArray.Count - 1
                    PicDir = PicDir & PicArray(p) & " "
                    If UCase(PicDir).Contains(".JPG") Or UCase(PicDir).Contains(".JPEG") Or UCase(PicDir).Contains(".PNG") Or UCase(PicDir).Contains(".BMP") Or UCase(PicDir).Contains(".GIF") Then Exit For
                Next

                Return PicDir

            Else
                Return String.Empty

            End If

        End If
    End Function

    Friend Sub ContactEdgeCheck(ByVal EdgeCheck As String)
        If EdgeCheck.Contains("@Contact1") Then ssh.Contact1Edge = True
        If EdgeCheck.Contains("@Contact2") Then ssh.Contact2Edge = True
        If EdgeCheck.Contains("@Contact3") Then ssh.Contact3Edge = True
    End Sub

    Public Sub DisableContactStroke()
        ssh.Contact1Stroke = False
        ssh.Contact2Stroke = False
        ssh.Contact3Stroke = False
    End Sub

    Public Sub GetSubState()

        ssh.ReturnSubState = "Rest"
        If ssh.SubStroking = True Then ssh.ReturnSubState = "Stroking"
        If ssh.SubEdging = True Then ssh.ReturnSubState = "Edging"
        If ssh.SubHoldingEdge = True Then ssh.ReturnSubState = "Holding The Edge"
        If ssh.CBTBallsFlag = True Or ssh.CBTBothFlag = True Then ssh.ReturnSubState = "CBTBalls"
        If ssh.CBTCockFlag = True Then ssh.ReturnSubState = "CBTCock"
        If ssh.CensorshipGame = True Then ssh.ReturnSubState = "Censorship Sucks"
        If ssh.AvoidTheEdgeGame = True Then ssh.ReturnSubState = "Avoid The Edge"
        If ssh.RLGLGame = True Then ssh.ReturnSubState = "RLGL"



    End Sub

    Public Sub EdgePace()

        StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMaxPace.Value + 151)
        If StrokePace > NBMinPace.Value Then StrokePace = NBMinPace.Value
        StrokePace = 50 * Math.Round(StrokePace / 50)

    End Sub

    Public Function FilterList(ByVal ListClean As List(Of String)) As List(Of String)
        'TDOD: Optimze Code "TextedTags"
        ssh.FoundTag = "NULL"
        Dim slide As ContactData = ssh.SlideshowMain
        If slide.CurrentImage = String.Empty Then GoTo SkipTextedTags

        Dim TagFilePath As String = Path.GetDirectoryName(slide.CurrentImage) & "\ImageTags.txt"

        If (ssh.SlideshowLoaded = True And mainPictureBox.Image IsNot Nothing And DomWMP.Visible = False) _
        AndAlso File.Exists(TagFilePath) Then
            Try
                Dim TagList As List(Of String) = Txt2List(TagFilePath)

                For t As Integer = 0 To TagList.Count - 1
                    If TagList(t).Contains(Path.GetFileName(slide.CurrentImage)) Then
                        ssh.FoundTag = TagList(t)
                        Dim FoundTagSplit As String() = Split(ssh.FoundTag)
                        For j As Integer = 0 To FoundTagSplit.Length - 1
                            If FoundTagSplit(j).Contains("TagGarment") Then
                                ssh.TagGarment = FoundTagSplit(j).Replace("TagGarment", "")
                                ssh.TagGarment = ssh.TagGarment.Replace("-", " ")
                            End If

                            If FoundTagSplit(j).Contains("TagUnderwear") Then
                                ssh.TagUnderwear = FoundTagSplit(j).Replace("TagUnderwear", "")
                                ssh.TagUnderwear = ssh.TagUnderwear.Replace("-", " ")
                            End If

                            If FoundTagSplit(j).Contains("TagTattoo") Then
                                ssh.TagTattoo = FoundTagSplit(j).Replace("TagTattoo", "")
                                ssh.TagTattoo = ssh.TagTattoo.Replace("-", " ")
                            End If

                            If FoundTagSplit(j).Contains("TagSexToy") Then
                                ssh.TagSexToy = FoundTagSplit(j).Replace("TagSexToy", "")
                                ssh.TagSexToy = ssh.TagSexToy.Replace("-", " ")
                            End If

                            If FoundTagSplit(j).Contains("TagFurniture") Then
                                ssh.TagFurniture = FoundTagSplit(j).Replace("TagFurniture", "")
                                ssh.TagFurniture = ssh.TagFurniture.Replace("-", " ")
                            End If
                        Next
                        Exit For
                    End If
                Next
            Catch
            End Try
        End If
SkipTextedTags:

        Dim FilterPass As Boolean
        Dim ListIncrement As Integer = 1
        If ssh.StrokeFilter = True Then ListIncrement = ssh.StrokeTauntCount

        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        '		Check if Grouped-Lines-Files have the right amount of Lines
        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        ' No need to go further on an empty file.
        If ListClean.Count <= 0 Then
            Trace.WriteLine("FilterList started with empty List. Skipping filter.")
            Return ListClean
        End If

        ' To Avoid DivideByZeroException 
        If ListIncrement <= 0 Then
            Dim lazyText As String = "FilterList Started With LineGroupingValue """ & ListIncrement & """. "
            Log.WriteError(lazyText, New ArgumentOutOfRangeException(lazyText), "FilterList Cancelled")
            Return ListClean
        End If

        ' Divide List.Count by StrokeTauntSize and get the Remainder.
        Dim InvalidLineCount As Integer = ListClean.Count Mod ListIncrement

        ' If there is a Remainder, the file has not the desired Line.Count.
        If InvalidLineCount > 0 Then
            ' So delete the Lines of the last and hopefully uncomplete Group. 
            ListClean.RemoveRange(ListClean.Count - InvalidLineCount, InvalidLineCount)
        End If
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        '		Grouped-Lines-Check-END 
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        For i As Integer = 0 To ListClean.Count - 1 Step ListIncrement

            FilterPass = True

            For x As Integer = 0 To ListIncrement - 1
                If GetFilter(ListClean(i + x)) = False Then
                    FilterPass = False
                    Exit For
                End If
            Next

            If FilterPass = False Then
                For x As Integer = 0 To ListIncrement - 1
                    ListClean(i + x) = ListClean(i + x) & "###-INVALID-###"
                Next
            End If

        Next

        For i As Integer = ListClean.Count - 1 To 0 Step -1
            If ListClean(i).Contains("###-INVALID-###") Then ListClean.RemoveAt(i)
        Next

        'BUG: Texted Tags are not working.
        For x As Integer = 0 To ListClean.Count - 1
            ListClean(x) = ListClean(x).Replace("#TagGarment", ssh.TagGarment.Replace("-", " "))
            ListClean(x) = ListClean(x).Replace("#TagUnderwear", ssh.TagUnderwear.Replace("-", " "))
            ListClean(x) = ListClean(x).Replace("#TagTattoo", ssh.TagTattoo.Replace("-", " "))
            ListClean(x) = ListClean(x).Replace("#TagSexToy", ssh.TagSexToy.Replace("-", " "))
            ListClean(x) = ListClean(x).Replace("#TagFurniture", ssh.TagFurniture.Replace("-", " "))
        Next

        Dim FilteredList As New List(Of String)

        Return ListClean
    End Function

    Public Function GetFilter(ByVal FilterString As String, Optional ByVal Linear As Boolean = False) As Boolean
        Dim OrgFilterString As String = FilterString
        Try
            If Linear = False Then
                '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '							Commands to sort out
                ' This Section contains @Commands, which are able to disqualify vocabulary lines.
                '
                ' Example-line: "Whatever Text to display @DommeTag(Glaring)"
                '
                ' This line has to be sorted out, if there are no corresponding images tagged 
                ' with "glaring".
                '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                'ISSUE: @DomTag() is not filtered out 
                If FilterString.Contains("@DommeTag(") Then
                    'QND-Implemented: ContactData.GetTaggedImage
                    If ssh.LockImage = True Then
                        Return False
                    ElseIf FilterString.ToLower.Contains("@contact1") Then
                        If ssh.SlideshowContact1.GetTaggedImage(GetParentheses(FilterString, "@DommeTag(")) = "" Then Return False
                    ElseIf FilterString.ToLower.Contains("@contact2") Then
                        If ssh.SlideshowContact2.GetTaggedImage(GetParentheses(FilterString, "@DommeTag(")) = "" Then Return False
                    ElseIf FilterString.ToLower.Contains("@contact3") Then
                        If ssh.SlideshowContact3.GetTaggedImage(GetParentheses(FilterString, "@DommeTag(")) = "" Then Return False
                    ElseIf ContactToUse IsNot Nothing Then
                        If ContactToUse.GetTaggedImage(GetParentheses(FilterString, "@DommeTag(")) = "" Then Return False
                    Else
                        Return False
                    End If
                End If

                If FilterString.Contains("@ImageTag(") Then
                    If GetLocalImage(GetParentheses(FilterString, "@ImageTag(")) = String.Empty Then Return False
                End If

                ' ################## @Show-Category-Image #####################
                If FilterString.Contains("@ShowBlogImage") Or FilterString.Contains("@NewBlogImage") Then
                    If Not GetImageData(ImageGenre.Blog).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowBlowjobImage") Then
                    If Not GetImageData(ImageGenre.Blowjob).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowBoobsImage") Or FilterString.Contains("@ShowBoobImage") Then
                    If Not GetImageData(ImageGenre.Boobs).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowCaptionsImage") Then
                    If Not GetImageData(ImageGenre.Captions).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowDislikedImage") Then
                    If Not GetImageData(ImageGenre.Disliked).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowFemdomImage") Then
                    If Not GetImageData(ImageGenre.Femdom).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowGayImage") Then
                    If Not GetImageData(ImageGenre.Gay).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowGeneralImage") Then
                    If Not GetImageData(ImageGenre.General).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowHardcoreImage") Then
                    If Not GetImageData(ImageGenre.Hardcore).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowHentaiImage") Then
                    If Not GetImageData(ImageGenre.Hentai).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowLesbianImage") Then
                    If Not GetImageData(ImageGenre.Lesbian).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowLezdomImage") Then
                    If Not GetImageData(ImageGenre.Lezdom).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowLikedImage") Then
                    If Not GetImageData(ImageGenre.Liked).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowLocalImage") Then
                    If myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") = True Or ssh.LockImage = True Then Return False
                End If
                If FilterString.Contains("@ShowLocalImage") Or FilterString.Contains("@ShowButtImage") Or FilterString.Contains("@ShowBoobsImage") Or FilterString.Contains("@ShowButtsImage") Or FilterString.Contains("@ShowBoobsImage") Then
                    If ssh.CustomSlideEnabled = True Or ssh.LockImage = True Then Return False
                End If
                'TODO: Add ImageDataContainerUsage to filter @ShowLocalImage correct.
                If FilterString.Contains("@ShowLocalImage") And My.Settings.CBIHardcore = False And My.Settings.CBISoftcore = False And My.Settings.CBILesbian = False And
               My.Settings.CBIBlowjob = False And My.Settings.CBIFemdom = False And My.Settings.CBILezdom = False And My.Settings.CBIHentai = False And
                  My.Settings.CBIGay = False And My.Settings.CBIMaledom = False And My.Settings.CBICaptions = False And My.Settings.CBIGeneral = False Then Return False

                If FilterString.Contains("@ShowTaggedImage") Then
                    Dim Tags As List(Of String) = FilterString.Split() _
                                    .Select(Function(s) s.Trim()) _
                                    .Where(Function(w) CType(w, String).StartsWith("@Tag")).ToList

                    If GetLocalImage(Tags, Nothing) = String.Empty Then Return False
                End If

                If FilterString.Contains("@ShowMaledomImage") Then
                    If Not GetImageData(ImageGenre.Maledom).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                If FilterString.Contains("@ShowSoftcoreImage") Then
                    If Not GetImageData(ImageGenre.Softcore).IsAvailable Or ssh.LockImage = True Or ssh.CustomSlideEnabled = True Then Return False
                End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                ' Disqualifying @Commands - End
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            End If

            '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            '							Possible space Filters
            ' This Section Contains @CommandFilters which allow space chars (0x20).
            ' 
            ' Example: "@Cup(A, B) Whatever Text to display"
            ' Mostly all perametrized command filters allow space chars in parameters.
            '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            If FilterString.Contains("@Variable[") Then
                If CheckVariable(FilterString) = False Then Return False
            End If

            If FilterString.Contains("@Group(") Then
                Dim GroupCheck As String = GetParentheses(FilterString, "@Group(")
                If GroupCheck.Contains("D") Then
                    If ssh.GlitterTease = False Or Not ssh.Group.Contains("D") Then Return False
                End If
                If GroupCheck.Contains("1") Then
                    If ssh.GlitterTease = False Or Not ssh.Group.Contains("1") Then Return False
                End If
                If GroupCheck.Contains("2") Then
                    If ssh.GlitterTease = False Or Not ssh.Group.Contains("2") Then Return False
                End If
                If GroupCheck.Contains("3") Then
                    If ssh.GlitterTease = False Or Not ssh.Group.Contains("3") Then Return False
                End If
            End If

            If FilterString.Contains("@Flag(") Or FilterString.Contains("@NotFlag(") Then
                Dim result As Boolean = True
                Dim writeFlag As String
                Dim splitFlag As String()

                If FilterString.Contains("@Flag(") Then
                    writeFlag = myLineService.GetParenData(FilterString, "@Flag(").Value(0)
                    writeFlag = FixCommas(writeFlag)
                    splitFlag = writeFlag.Split({","}, StringSplitOptions.RemoveEmptyEntries)

                    For Each s In splitFlag
                        If Not myFlagAccessor.IsSet(CreateDommePersonality(), s) Then
                            result = False
                            Exit For
                        End If
                    Next
                End If
                If result = False Then Return result

                If FilterString.Contains("@NotFlag(") Then
                    writeFlag = myLineService.GetParenData(FilterString, "@NotFlag(").Value(0)
                    writeFlag = FixCommas(writeFlag)
                    splitFlag = writeFlag.Split({","}, StringSplitOptions.RemoveEmptyEntries)

                    For Each s In splitFlag
                        If myFlagAccessor.IsSet(CreateDommePersonality(), s) Then
                            result = False
                            Exit For
                        End If
                    Next
                End If
                Return result
            End If

            If FilterString.Contains("@CheckDate(") And Linear = False Then
                If CheckDateList(FilterString) = False Then Return False
            End If

            If FilterString.Contains("@Month(") Then
                If GetMatch(FilterString, "@Month(", DateAndTime.Now.Month) = False Then Return False
            End If

            If FilterString.Contains("@Day(") Then
                If GetMatch(FilterString, "@Day(", DateAndTime.Now.Day) = False Then Return False
            End If

            If FilterString.Contains("@SetModule(") Then
                If ssh.SetModule <> "" Or ssh.BookmarkModule = True Then Return False
            End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            ' Possible space Filters - End
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            '								Single word filters
            ' This section contains single word command filters. 
            ' Since there are some legacy commands, which are filters and also instructions, 
            ' this section will ignore all @Statements after @NullResponse or the first 
            ' word not starting with "@" (0x40)
            '
            ' Beware: destroys the original FilterString-Value!
            '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            Dim FilterList As String()

            FilterList = FilterString.Split(" ")
            FilterString = ""

            For f As Integer = 0 To FilterList.Count - 1
                If Not FilterList(f).StartsWith("@") Or FilterList(f).Contains("@NullResponse") Then
                    Exit For
                End If

                FilterString = FilterString & FilterList(f) & " "
            Next

            If FilterString = "" Then Return True

            If FilterString.ToLower.Contains("@selfyoung") Or FilterString.ToLower.Contains("@selfold") Then
                If ssh.VideoTease = True Or ssh.TeaseVideo = True Then Return False
            End If
            If FilterString.ToLower.Contains("@subyoung") And FrmSettings.subAgeNumBox.Value > FrmSettings.NBSubAgeMin.Value - 1 Then Return False
            If FilterString.ToLower.Contains("@subold") And FrmSettings.subAgeNumBox.Value < FrmSettings.NBSubAgeMax.Value + 1 Then Return False

            If Not New DommePersonalityDetection().ShouldKeepLine(FilterString, CreateDommePersonality()) Then Return False
            If Not New HolidayDetection().ShouldKeepLine(FilterString, DateTime.Now) Then Return False
            If Not New SubPersonalityDetection().ShouldKeepLine(FilterString, CreateSubPersonality()) Then Return False
            If Not New TagDetection().ShouldKeepLine(FilterString, ssh.FoundTag) Then Return False
            If Not New VideoDetection().ShouldKeepLine(FilterString, ssh.VideoTease, ssh.VideoType) Then Return False
            If Not New SessionDetection().ShouldKeepLine(FilterString, CreateSession()) Then Return False

            If FilterString.ToLower.Contains("@cocksmall") And FrmSettings.CockSizeNumBox.Value >= FrmSettings.NBAvgCockMin.Value Then Return False
            If FilterString.ToLower.Contains("@cockaverage") Then
                If FrmSettings.CockSizeNumBox.Value < FrmSettings.NBAvgCockMin.Value Or FrmSettings.CockSizeNumBox.Value > FrmSettings.NBAvgCockMax.Value Then Return False
            End If

            If FilterString.ToLower.Contains("@cocklarge") And FrmSettings.CockSizeNumBox.Value <= FrmSettings.NBAvgCockMax.Value Then Return False

            If FilterString.ToLower.Contains("@strokespeedmax") And StrokePace < NBMaxPace.Value Then Return False
            If FilterString.ToLower.Contains("@strokespeedmin") And StrokePace < NBMinPace.Value Then Return False
            If FilterString.ToLower.Contains("@strokefaster") Or FilterString.ToLower.Contains("@strokefastest") Then
                If StrokePace = NBMaxPace.Value Or ssh.WorshipMode = True Then Return False
            End If
            If FilterString.ToLower.Contains("@strokeslower") Or FilterString.ToLower.Contains("@strokeslowest") Then
                If StrokePace = NBMinPace.Value Or ssh.WorshipMode = True Then Return False
            End If

            If FilterString.Contains("@LongEdge") Then
                If ssh.LongEdge = False Or FrmSettings.AllowLongEdgeTauntCB.Checked = False Then Return False
            End If
            If FilterString.Contains("@InterruptLongEdge") Then
                If ssh.LongEdge = False Or FrmSettings.AllowLongEdgeInterruptCB.Checked = False Or ssh.TeaseTick < 1 Or ssh.RiskyEdges = True Then Return False
            End If

            If FilterString.Contains("@1MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 60 Or ssh.HoldEdgeTime > 119 Then Return False
            End If
            If FilterString.Contains("@2MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 120 Or ssh.HoldEdgeTime > 179 Then Return False
            End If
            If FilterString.Contains("@3MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 180 Or ssh.HoldEdgeTime > 239 Then Return False
            End If
            If FilterString.Contains("@4MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 240 Or ssh.HoldEdgeTime > 299 Then Return False
            End If
            If FilterString.Contains("@5MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 300 Or ssh.HoldEdgeTime > 599 Then Return False
            End If
            If FilterString.Contains("@10MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 600 Or ssh.HoldEdgeTime > 899 Then Return False
            End If
            If FilterString.Contains("@15MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 900 Or ssh.HoldEdgeTime > 1799 Then Return False
            End If
            If FilterString.Contains("@30MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 1800 Or ssh.HoldEdgeTime > 2699 Then Return False
            End If
            If FilterString.Contains("@45MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 2700 Or ssh.HoldEdgeTime > 3599 Then Return False
            End If
            If FilterString.Contains("@60MinuteHold") Then
                If ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 3600 Then Return False
            End If

            If FilterString.Contains("@CBTLevel1") And FrmSettings.CockAndBallTortureLevelSlider.Value <> 1 Then Return False
            If FilterString.Contains("@CBTLevel2") And FrmSettings.CockAndBallTortureLevelSlider.Value <> 2 Then Return False
            If FilterString.Contains("@CBTLevel3") And FrmSettings.CockAndBallTortureLevelSlider.Value <> 3 Then Return False
            If FilterString.Contains("@CBTLevel4") And FrmSettings.CockAndBallTortureLevelSlider.Value <> 4 Then Return False
            If FilterString.Contains("@CBTLevel5") And FrmSettings.CockAndBallTortureLevelSlider.Value <> 5 Then Return False
            If FilterString.Contains("@BeforeTease") And ssh.BeforeTease = False Then Return False

            If FilterString.Contains("@VitalSub") And CBVitalSub.Checked = False Then Return False
            If FilterString.Contains("@VitalSubAssignment") Then
                If CBVitalSub.Checked = False Or CBVitalSubDomTask.Checked = False Then Return False
            End If

            If FilterString.Contains("@RuinTaunt") Then
                If ssh.EdgeToRuin = False Or ssh.EdgeToRuinSecret = True Then Return False
            End If

            If FilterString.Contains("@Morning") And ssh.GeneralTime <> "Morning" Then Return False
            If FilterString.Contains("@Afternoon") And ssh.GeneralTime <> "Afternoon" Then Return False
            If FilterString.Contains("@Night") And ssh.GeneralTime <> "Night" Then Return False

            If FilterString.Contains("@OrgasmRestricted") And ssh.OrgasmRestricted = False Then Return False
            If FilterString.Contains("@OrgasmNotRestricted") And ssh.OrgasmRestricted = True Then Return False
            If FilterString.Contains("@SubWorshipping") And ssh.WorshipMode = False Then Return False
            If FilterString.Contains("@SubNotWorshipping") And ssh.WorshipMode = True Then Return False
            If FilterString.Contains("@LongHold") Then
                If ssh.LongHold = False Or ssh.SubHoldingEdge = False Then Return False
            End If

            If FilterString.Contains("@ExtremeHold") Then
                If ssh.ExtremeHold = False Or ssh.SubHoldingEdge = False Then Return False
            End If

            If FilterString.Contains("@AssWorship") Then
                If ssh.WorshipTarget <> "Ass" Or ssh.WorshipMode = False Then Return False
            End If

            If FilterString.Contains("@BoobWorship") Then
                If ssh.WorshipTarget <> "Boobs" Or ssh.WorshipMode = False Then Return False
            End If

            If FilterString.Contains("@PussyWorship") Then
                If ssh.WorshipTarget <> "Pussy" Or ssh.WorshipMode = False Then Return False
            End If

            If FilterString.Contains("@Contact1") Then
                If ssh.GlitterTease = False Or Not ssh.Group.Contains("1") Then Return False
            End If

            If FilterString.Contains("@Contact2") Then
                If ssh.GlitterTease = False Or Not ssh.Group.Contains("2") Then Return False
            End If

            If FilterString.Contains("@Contact3") Then
                If ssh.GlitterTease = False Or Not ssh.Group.Contains("3") Then Return False
            End If

            If FilterString.Contains("@Info") Then Return False
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            ' Single word filters - End
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Return True
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Log.WriteError(String.Format("Exceoption occured while checking line ""{0}"".", OrgFilterString),
                                         ex, "GetFilter(String, Boolean)")
            Return False
        End Try
    End Function

    Public Function GetFilter2(ByVal FilterString As String) As Boolean


        Dim __ConditionDic As New Dictionary(Of String, Boolean)(System.StringComparer.OrdinalIgnoreCase)
        Try
            '===============================================================================
            '							Dictionary Setup Description
            ' 1st Parameter: "Key" this is the Command as String preceded with @
            ' 2nd Parameter: "Value" These are the conditions that must be met to EXCLUDE a line.
            '
            '		 If "Value" is "True", all lines containing "Key" will be excuded.
            '
            '===============================================================================
            With __ConditionDic
                .Add(Keyword.Crazy, FrmSettings.crazyCheckBox.Checked = False)
                .Add("@Vulgar", FrmSettings.vulgarCheckBox.Checked = False)
                .Add("@Supremacist", FrmSettings.supremacistCheckBox.Checked = False)
                .Add("@Sadistic", FrmSettings.sadisticCheckBox.Checked = False)
                .Add("@Degrading", FrmSettings.degradingCheckBox.Checked = False)
                .Add("@DommeLevel1", FrmSettings.DominationLevel.Value <> 1)
                .Add("@DommeLevel2", FrmSettings.DominationLevel.Value <> 2)
                .Add("@DommeLevel3", FrmSettings.DominationLevel.Value <> 3)
                .Add("@DommeLevel4", FrmSettings.DominationLevel.Value <> 4)
                .Add("@DommeLevel5", FrmSettings.DominationLevel.Value <> 5)
                .Add("@SelfYoung", FrmSettings.domageNumBox.Value > FrmSettings.NBSelfAgeMin.Value - 1)
                .Add("@SelfOld", FrmSettings.domageNumBox.Value < FrmSettings.NBSelfAgeMax.Value + 1)
                .Add("@ACup", FrmSettings.boobComboBox.Text <> "A" Or ssh.JustShowedBlogImage = True)
                .Add("@BCup", FrmSettings.boobComboBox.Text <> "B" Or ssh.JustShowedBlogImage = True)
                .Add("@CCup", FrmSettings.boobComboBox.Text <> "C" Or ssh.JustShowedBlogImage = True)
                .Add("@DCup", FrmSettings.boobComboBox.Text <> "D" Or ssh.JustShowedBlogImage = True)
                .Add("@DDCup", FrmSettings.boobComboBox.Text <> "DD" Or ssh.JustShowedBlogImage = True)
                .Add("@DDD+Cup", FrmSettings.boobComboBox.Text <> "DDD+" Or ssh.JustShowedBlogImage = True)
                .Add("@CockSmall", FrmSettings.CockSizeNumBox.Value >= FrmSettings.NBAvgCockMin.Value)
                .Add("@CockLarge", FrmSettings.CockSizeNumBox.Value <= FrmSettings.NBAvgCockMax.Value)
                .Add("@CockAverage", FrmSettings.CockSizeNumBox.Value < FrmSettings.NBAvgCockMin.Value Or FrmSettings.CockSizeNumBox.Value > FrmSettings.NBAvgCockMax.Value)
                .Add("@SubYoung", FrmSettings.subAgeNumBox.Value >= FrmSettings.NBSubAgeMin.Value)
                .Add("@SubOld", FrmSettings.subAgeNumBox.Value <= FrmSettings.NBSubAgeMax.Value)
                .Add("@SubBirthday", FrmSettings.NBBirthdayMonth.Value <> Month(Date.Now) And FrmSettings.NBBirthdayDay.Value <> DateAndTime.Day(Date.Now))
                .Add("@ValentinesDay", Month(Date.Now) <> 2 And DateAndTime.Day(Date.Now) <> 14)
                .Add("@ChristmasEve", Month(Date.Now) <> 12 And DateAndTime.Day(Date.Now) <> 24)
                .Add("@ChristmasDay", Month(Date.Now) <> 12 And DateAndTime.Day(Date.Now) <> 25)
                .Add("@NewYearsEve", Month(Date.Now) <> 12 And DateAndTime.Day(Date.Now) <> 31)
                .Add("@NewYearsDay", Month(Date.Now) <> 12 And DateAndTime.Day(Date.Now) <> 25)
                .Add("@TagFace", Not ssh.FoundTag.Contains("TagFace"))
                .Add("@TagBoobs", Not ssh.FoundTag.Contains("TagBoobs"))
                .Add("@TagPussy", Not ssh.FoundTag.Contains("TagPussy"))
                .Add("@TagAss", Not ssh.FoundTag.Contains("TagAss"))
                .Add("@TagFeet", Not ssh.FoundTag.Contains("TagFeet"))
                .Add("@TagLegs", Not ssh.FoundTag.Contains("TagLegs"))
                .Add("@TagMasturbating", Not ssh.FoundTag.Contains("TagMasturbating"))
                .Add("@TagSucking", Not ssh.FoundTag.Contains("TagSucking"))
                .Add("@TagFullyDressed", Not ssh.FoundTag.Contains("TagFullyDressed"))
                .Add("@TagHalfDressed", Not ssh.FoundTag.Contains("TagHalfDressed"))
                .Add("@TagGarmentCovering", Not ssh.FoundTag.Contains("TagGarmentCovering"))
                .Add("@TagHandsCovering", Not ssh.FoundTag.Contains("TagHandsCovering"))
                .Add("@TagNaked", Not ssh.FoundTag.Contains("TagNaked"))
                .Add("@TagSideView", Not ssh.FoundTag.Contains("TagSideView"))
                .Add("@TagCloseUp", Not ssh.FoundTag.Contains("TagCloseUp"))
                .Add("@TagPiercing", Not ssh.FoundTag.Contains("TagPiercing"))
                .Add("@TagSmiling", Not ssh.FoundTag.Contains("TagSmiling"))
                .Add("@TagGlaring", Not ssh.FoundTag.Contains("TagGlaring"))
                .Add("@TagGarment", Not ssh.FoundTag.Contains("TagGarment"))
                .Add("@TagUnderwear", Not ssh.FoundTag.Contains("TagUnderwear"))
                .Add("@TagTattoo", Not ssh.FoundTag.Contains("TagTattoo"))
                .Add("@TagSexToy", Not ssh.FoundTag.Contains("TagSexToy"))
                .Add("@TagFurniture", Not ssh.FoundTag.Contains("TagFurniture"))
                .Add("@FirstRound", ssh.FirstRound = False)
                .Add("@NotFirstRound", ssh.FirstRound = True)
                .Add("@StrokeSpeedMax", StrokePace < NBMaxPace.Value)
                .Add("@StrokeSpeedMin", StrokePace > NBMinPace.Value)
                .Add("@StrokeFaster", StrokePace = NBMaxPace.Value Or ssh.WorshipMode = True)
                .Add("@StrokeFastest", StrokePace = NBMaxPace.Value Or ssh.WorshipMode = True)
                .Add("@StrokeSlower", StrokePace = NBMinPace.Value Or ssh.WorshipMode = True)
                .Add("@StrokeSlowest", StrokePace = NBMinPace.Value Or ssh.WorshipMode = True)
                .Add("@AlwaysAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text <> "Always Allows")
                .Add("@OftenAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text <> "Often Allows")
                .Add("@SometimesAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text <> "Sometimes Allows")
                .Add("@RarelyAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text <> "Rarely Allows")
                .Add("@NeverAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text <> "Never Allows")
                .Add("@AlwaysRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text <> "Always Ruins")
                .Add("@OftenRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text <> "Often Ruins")
                .Add("@SometimesRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text <> "Sometimes Ruins")
                .Add("@RarelyRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text <> "Rarely Ruins")
                .Add("@NeverRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text <> "Never Ruins")
                .Add("@NotAlwaysAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text = "Always Allows")
                .Add("@NotNeverAllowsOrgasm", FrmSettings.alloworgasmComboBox.Text = "Never Allows")
                .Add("@NotAlwaysRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text = "Always Ruins")
                .Add("@NotNeverRuinsOrgasm", FrmSettings.ruinorgasmComboBox.Text = "Never Allows")
                .Add("@LongEdge", ssh.LongEdge = False Or FrmSettings.AllowLongEdgeTauntCB.Checked = False)
                .Add("@InterruptLongEdge", Not ssh.LongEdge OrElse Not FrmSettings.AllowLongEdgeInterruptCB.Checked OrElse ssh.TeaseTick < 1 OrElse ssh.RiskyEdges)
                .Add("@ShowHardcoreImage", Not Directory.Exists(My.Settings.IHardcore) OrElse Not My.Settings.CBIHardcore OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowSoftcoreImage", Not Directory.Exists(My.Settings.ISoftcore) OrElse My.Settings.CBISoftcore = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowLesbianImage", Not Directory.Exists(My.Settings.ILesbian) OrElse My.Settings.CBILesbian = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowBlowjobImage", Not Directory.Exists(My.Settings.IBlowjob) OrElse My.Settings.CBIBlowjob = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowFemdomImage", Not Directory.Exists(My.Settings.IFemdom) OrElse My.Settings.CBIFemdom = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowLezdomImage", Not Directory.Exists(My.Settings.ILezdom) OrElse My.Settings.CBILezdom = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowHentaiImage", Not Directory.Exists(My.Settings.IHentai) OrElse My.Settings.CBIHentai = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowGayImage", Not Directory.Exists(My.Settings.IGay) OrElse My.Settings.CBIGay = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowMaledomImage", Not Directory.Exists(My.Settings.IMaledom) OrElse My.Settings.CBIMaledom = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowCaptionsImage", Not Directory.Exists(My.Settings.ICaptions) OrElse My.Settings.CBICaptions = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowGeneralImage", Not Directory.Exists(My.Settings.IGeneral) OrElse My.Settings.CBIGeneral = False OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@ShowBlogImage", FrmSettings.URLFileList.CheckedItems.Count = 0 OrElse ssh.CustomSlideEnabled OrElse myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.LockImage)
                .Add("@NewBlogImage", __ConditionDic("@ShowBlogImage")) ' duplicate Command, lets get the Value af the other one.
                .Add("@ShowLocalImage", myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") OrElse ssh.CustomSlideEnabled Or ssh.LockImage = True _
                      Or (My.Settings.CBIHardcore = False And My.Settings.CBISoftcore = False And My.Settings.CBILesbian = False And My.Settings.CBIBlowjob = False _
                       And My.Settings.CBIFemdom = False And My.Settings.CBILezdom = False And My.Settings.CBIHentai = False And My.Settings.CBIGay = False _
                       And My.Settings.CBIMaledom = False And My.Settings.CBICaptions = False And My.Settings.CBIGeneral = False))
                '.Add("@ShowButtImage", Not Directory.Exists(FrmSettings.LBLButtPath.Text) And Not File.Exists(FrmSettings.LBLButtURL.Text) Or myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") = True Or CustomSlideshow = True Or LockImage = True)
                '.Add("@ShowButtsImage", __ConditionDic("@ShowButtImage")) ' duplicate Command, lets get the Value af the other one.
                '.Add("@ShowBoobImage", Not Directory.Exists(FrmSettings.LBLBoobPath.Text) And Not File.Exists(FrmSettings.LBLBoobURL.Text) Or myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed") = True Or CustomSlideshow = True Or LockImage = True)
                '.Add("@ShowBoobsImage", __ConditionDic("@ShowBoobImage")) ' duplicate Command, lets get the Value af the other one.
                .Add("@1MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 60 Or ssh.HoldEdgeTime > 119)
                .Add("@2MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 120 Or ssh.HoldEdgeTime > 179)
                .Add("@3MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 180 Or ssh.HoldEdgeTime > 239)
                .Add("@4MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 240 Or ssh.HoldEdgeTime > 299)
                .Add("@5MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 300 Or ssh.HoldEdgeTime > 599)
                .Add("@10MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 600 Or ssh.HoldEdgeTime > 899)
                .Add("@15MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 900 Or ssh.HoldEdgeTime > 1799)
                .Add("@30MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 1800 Or ssh.HoldEdgeTime > 2699)
                .Add("@45MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 2700 Or ssh.HoldEdgeTime > 3599)
                .Add("@60MinuteHold", ssh.SubHoldingEdge = False Or ssh.HoldEdgeTime < 3600)
                .Add("@CBTLevel1", FrmSettings.CockAndBallTortureLevelSlider.Value <> 1)
                .Add("@CBTLevel2", FrmSettings.CockAndBallTortureLevelSlider.Value <> 2)
                .Add("@CBTLevel3", FrmSettings.CockAndBallTortureLevelSlider.Value <> 3)
                .Add("@CBTLevel4", FrmSettings.CockAndBallTortureLevelSlider.Value <> 4)
                .Add("@CBTLevel5", FrmSettings.CockAndBallTortureLevelSlider.Value <> 5)
                .Add("@SubCircumcised", FrmSettings.CBSubCircumcised.Checked = False)
                .Add("@SubNotCircumcised", FrmSettings.CBSubCircumcised.Checked = True)
                .Add("@SubPierced", FrmSettings.CBSubPierced.Checked = False)
                .Add("@SubNotPierced", FrmSettings.CBSubPierced.Checked = True)
                .Add("@ShowTaggedImage", ssh.LocalTagImageList.Count = 0) '=>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> For this Condition the tags have be loaded before.
                .Add("@BeforeTease", ssh.BeforeTease = False)
                .Add("@OrgasmDenied", ssh.OrgasmDenied = False)
                .Add("@OrgasmAllowed", ssh.OrgasmAllowed = False)
                .Add("@OrgasmRuined", ssh.OrgasmRuined = False)
                .Add("@ApathyLevel1", FrmSettings.NBEmpathy.Value <> 1)
                .Add("@ApathyLevel2", FrmSettings.NBEmpathy.Value <> 2)
                .Add("@ApathyLevel3", FrmSettings.NBEmpathy.Value <> 3)
                .Add("@ApathyLevel4", FrmSettings.NBEmpathy.Value <> 4)
                .Add("@ApathyLevel5", FrmSettings.NBEmpathy.Value <> 5)
                .Add("@InChastity", My.Settings.Chastity = False)
                .Add("@NotInChastity", My.Settings.Chastity = True)
                .Add("@HasChastity", FrmSettings.CBOwnChastity.Checked = False)
                .Add("@DoesNotHaveChastity", FrmSettings.CBOwnChastity.Checked = True)
                .Add("@ChastityPA", FrmSettings.DoesChastityDeviceRequirePiercingCB.Checked = False)
                .Add("@ChastitySpikes", FrmSettings.ChastityDeviceContainsSpikesCB.Checked = False)
                .Add("@VitalSub", CBVitalSub.Checked = False)
                .Add("@VitalSubAssignment", CBVitalSub.Checked = False Or CBVitalSubDomTask.Checked = False)
                .Add("@RuinTaunt", ssh.EdgeToRuin = False Or ssh.EdgeToRuinSecret = True)
                .Add("@ShowLikedImage", Not File.Exists(Application.StartupPath & "\Images\System\LikedImageURLs.txt"))
                .Add("@ShowDislikedImage", Not File.Exists(Application.StartupPath & "\Images\System\DislikedImageURLs.txt"))
                .Add("@VideoHardcore", ssh.VideoTease = False Or ssh.VideoType <> "Hardcore")
                .Add("@VideoSoftcore", ssh.VideoTease = False Or ssh.VideoType <> "Softcore")
                .Add("@VideoLesbian", ssh.VideoTease = False Or ssh.VideoType <> "Lesbian")
                .Add("@VideoBlowjob", ssh.VideoTease = False Or ssh.VideoType <> "Blowjob")
                .Add("@VideoFemdom", ssh.VideoTease = False Or ssh.VideoType <> "Femdom")
                .Add("@VideoFemsub", ssh.VideoTease = False Or ssh.VideoType <> "Femsub")
                .Add("@VideoGeneral", ssh.VideoTease = False Or ssh.VideoType <> "General")
                .Add("@VideoHardcoreDomme", ssh.VideoTease = False Or ssh.VideoType <> "HardcoreD")
                .Add("@VideoSoftcoreDomme", ssh.VideoTease = False Or ssh.VideoType <> "SoftcoreD")
                .Add("@VideoLesbianDomme", ssh.VideoTease = False Or ssh.VideoType <> "LesbianD")
                .Add("@VideoBlowjobDomme", ssh.VideoTease = False Or ssh.VideoType <> "BlowjobD")
                .Add("@VideoFemdomDomme", ssh.VideoTease = False Or ssh.VideoType <> "FemdomD")
                .Add("@VideoFemsubDomme", ssh.VideoTease = False Or ssh.VideoType <> "FemsubD")
                .Add("@VideoGeneralDomme", ssh.VideoTease = False Or ssh.VideoType <> "GeneralD")
                .Add("@BallTorture0", ssh.CBTBallsCount <> 0)
                .Add("@BallTorture1", ssh.CBTBallsCount <> 1)
                .Add("@BallTorture2", ssh.CBTBallsCount <> 2)
                .Add("@BallTorture3", ssh.CBTBallsCount <> 3)
                .Add("@BallTorture4+", ssh.CBTBallsCount < 4)
                .Add("@CockTorture0", ssh.CBTCockCount <> 0)
                .Add("@CockTorture1", ssh.CBTCockCount <> 1)
                .Add("@CockTorture2", ssh.CBTCockCount <> 2)
                .Add("@CockTorture3", ssh.CBTCockCount <> 3)
                .Add("@CockTorture4+", ssh.CBTCockCount < 4)
                .Add("@Contact1", ssh.GlitterTease = False Or Not ssh.Group.Contains("1"))
                .Add("@Contact2", ssh.GlitterTease = False Or Not ssh.Group.Contains("2"))
                .Add("@Contact3", ssh.GlitterTease = False Or Not ssh.Group.Contains("3"))
                .Add("@Stroking", ssh.SubStroking = False)
                .Add("@SubStroking", ssh.SubStroking = False)
                .Add("@NotStroking", ssh.SubStroking = True)
                .Add("@SubNotStroking", ssh.SubStroking = True)
                .Add("@Edging", ssh.SubEdging = False)
                .Add("@SubEdging", ssh.SubEdging = False)
                .Add("@NotEdging", ssh.SubEdging = True)
                .Add("@SubNotEdging", ssh.SubEdging = True)
                .Add("@HoldingTheEdge", ssh.SubHoldingEdge = False)
                .Add("@SubHoldingTheEdge", ssh.SubHoldingEdge = False)
                .Add("@NotHoldingTheEdge", ssh.SubHoldingEdge = True)
                .Add("@SubNotHoldingTheEdge", ssh.SubHoldingEdge = True)
                .Add("@Morning", ssh.GeneralTime <> "Morning")
                .Add("@Afternoon", ssh.GeneralTime <> "Afternoon")
                .Add("@Night", ssh.GeneralTime <> "Night")
                .Add("@GoodMood", ssh.DommeMood <= FrmSettings.NBDomMoodMax.Value)
                .Add("@BadMood", ssh.DommeMood >= FrmSettings.NBDomMoodMin.Value)
                .Add("@NeutralMood", ssh.DommeMood > FrmSettings.NBDomMoodMax.Value Or ssh.DommeMood < FrmSettings.NBDomMoodMin.Value)
                .Add("@SetModule(", ssh.SetModule <> "" Or ssh.BookmarkModule = True) ' I wonder if this will work.
                .Add("@OrgasmRestricted", ssh.OrgasmRestricted = False)
                .Add("@OrgasmNotRestricted", ssh.OrgasmRestricted = True)
                .Add("@SubWorshipping", ssh.WorshipMode = False)
                .Add("@SubNotWorshipping", ssh.WorshipMode = True)
                .Add("@LongHold", ssh.LongHold = False Or ssh.SubHoldingEdge = False)
                .Add("@ExtremeHold", ssh.ExtremeHold = False Or ssh.SubHoldingEdge = False)
                .Add("@AssWorship", ssh.WorshipTarget <> "Ass" Or ssh.WorshipMode = False)
                .Add("@BoobWorship", ssh.WorshipTarget <> "Boobs" Or ssh.WorshipMode = False)
                .Add("@PussyWorship", ssh.WorshipTarget <> "Pussy" Or ssh.WorshipMode = False)
            End With
        Catch ex As ArgumentException
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '	                ArgumentException => Will occur everytime until you fix Source Code!
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            MsgBox("Error on initializing FilterList. This Error occurs, If you try to add a duplikace Key to the dictionary." &
                "This error is major issue in Code and will occur everytime until you fix this Error. For there is no point in " &
                "further executing the Code, the application will exit after closing this Message." & vbCrLf &
                ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical, "Source Code Error")
            Application.Exit()
        End Try


        Try
            ' Declare a new regex Instance, for detecting the parameters in a line.
            ' Allowed chars for Commands are:		 A-Z a-z 0-9 @ 
            ' Allowed Brackets are :				( [
            ' Minimum length is 3 Chars, maximum Command length has no restriction.
            Dim __re As Regex = New Regex("@[@\w\d+]{3,}[\(\[]*", RegexOptions.IgnoreCase)


            ' Execute regex on current line, to find all containing Commands
            Dim mc As MatchCollection = __re.Matches(FilterString)

            For Each m As Match In mc
                If __ConditionDic.Keys.Contains(m.Value) AndAlso __ConditionDic(m.Value) Then
                    '===============================================================================
                    '					Known Command - DELETE Condition = TRUE
                    '===============================================================================
                    ' The Command is known and his delete condition is True -> delete line
                    Return False

                ElseIf __ConditionDic.Keys.Contains(m.Value) = False Then
                    '===============================================================================
                    '						Unknown Command / BracketCommand
                    '===============================================================================
                    Dim Condition As Boolean = False

                    Select Case m.Value.ToUpper
                        Case "@DommeLevel(".ToUpper : Condition = FilterCheck(GetParentheses(FilterString, "@DommeLevel("), FrmSettings.DominationLevel)
                        Case "@Cup(".ToUpper : Condition = FilterCheck(GetParentheses(FilterString, "@Cup("), FrmSettings.boobComboBox)
                        Case Keyword.AllowsOrgasm.ToUpper : Condition = FilterCheck(GetParentheses(FilterString, Keyword.AllowsOrgasm), FrmSettings.alloworgasmComboBox)
                        Case Keyword.RuinsOrgasm.ToUpper : Condition = FilterCheck(GetParentheses(FilterString, Keyword.RuinsOrgasm), FrmSettings.ruinorgasmComboBox)
                        Case Keyword.ApathyLevel.ToUpper : Condition = FilterCheck(GetParentheses(FilterString, Keyword.ApathyLevel), FrmSettings.NBEmpathy)
                        Case "@Variable[".ToUpper : Condition = CheckVariable(FilterString)
                        Case "@CheckDate(".ToUpper : Condition = CheckDateList(FilterString)
                        'QND-Implemented: ContactData.GetTaggedImage
                        'Case "@DommeTag(".ToUpper : Condition = GetDommeImage(GetParentheses(FilterString, "@DommeTag(")) = False Or ssh.LockImage = True
                        Case "@ImageTag(".ToUpper : Condition = GetLocalImage(FilterString)
                        Case Else
                            '<= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <=
                            '					Unknown Command => goto next Match
                            '<= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <= <=
                            Dim f As String = "" ' Debug line to add the ability to set a breakpoint.
                            Exit For
                    End Select
                    ' The Command is known and his delete condition is True -> delete line
                    If Condition Then Return False
                End If
            Next ' of Regex matches (Commands)
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            'TODO: Once implemented rethrow all exceptions.
            'Throw
        End Try

        Return True




    End Function

#Region "---------------------------------------------------- Chatbox ---------------------------------------------------------"

    Private Sub ChatText_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles ChatText.DocumentCompleted
        ScrollChatDown()
    End Sub

    Private Sub ChatText2_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles ChatText2.DocumentCompleted
        Try
            ChatText2.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue)
        Catch
        End Try
    End Sub

    Private Sub chatBox_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles chatBox.DragDrop
        chatBox.Text = CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString
        SendButton.PerformClick()
    End Sub

    Private Sub chatBox2_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles ChatBox2.DragDrop
        chatBox.Text = ""
        ChatBox2.Text = CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString
        SendButton.PerformClick()
    End Sub

    Private Sub chatBox_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles chatBox.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub chatBox2_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles ChatBox2.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub chatbox_KeyDown(sender As Object, e As KeyEventArgs) Handles chatBox.KeyDown
        If e.KeyCode = Keys.Return Then
            SendButton.PerformClick()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub chatbox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ChatBox2.KeyDown
        If e.KeyCode = Keys.Return Then
            SendButton.PerformClick()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub chatBox_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles chatBox.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            ' sendButton.PerformClick()
            e.KeyChar = Chr(0)
        End If
    End Sub

    Private Sub chatBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles ChatBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            ' sendButton.PerformClick()
            e.KeyChar = Chr(0)
        End If
    End Sub

#End Region ' Chatbox

#Region "------------------------------------ Avoid the Edge --------------------------------------------"

    Private Sub AvoidTheEdge_Tick(sender As Object, e As EventArgs) Handles AvoidTheEdge.Tick

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.AvoidTheEdgeTick < 6 Then Return
        If chatBox.Text <> "" And ssh.AvoidTheEdgeTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.AvoidTheEdgeTick < 6 Then Return
        If ssh.FollowUp <> "" And ssh.AvoidTheEdgeTick < 6 Then Return

        ssh.AvoidTheEdgeTick -= 1

        If ssh.AvoidTheEdgeTick < 1 Then



            Dim AvoidTheEdgeVideo As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\AvoidTheEdge.txt"
            If ssh.DommeVideo = True Then AvoidTheEdgeVideo = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\AvoidTheEdgeD.txt"

            Dim AvoidTheEdgeLineStart As Integer
            Dim AvoidTheEdgeLineEnd As Integer


            If File.Exists(AvoidTheEdgeVideo) Then
            Else
                If ssh.DommeVideo = True Then
                    MsgBox("AvoidTheEdgeD.txt is missing!", , "Error!")
                Else
                    MsgBox("AvoidTheEdge.txt is missing!", , "Error!")
                End If
                Return
            End If



            If ssh.AvoidTheEdgeStroking = False Then


                'CensorshipTick = randomizer.Next(NBCensorHideMin.Value, NBCensorHideMax.Value + 1)

                ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value

                ' If AvoidTheEdgeLineTemp > TauntSlider.Value * 5 Then
                'Return
                'End If

                Using ioFileA As New StreamReader(AvoidTheEdgeVideo)
                    Dim linesA As New List(Of String)
                    Dim TempAvoidTheEdgeLine As Integer

                    TempAvoidTheEdgeLine = -1
                    While ioFileA.Peek <> -1
                        TempAvoidTheEdgeLine += 1
                        linesA.Add(ioFileA.ReadLine())
                        If ssh.VideoType = "Hardcore" And linesA(TempAvoidTheEdgeLine) = "[HardcoreStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Hardcore" And linesA(TempAvoidTheEdgeLine) = "[SoftcoreStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Softcore" And linesA(TempAvoidTheEdgeLine) = "[SoftcoreStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Softcore" And linesA(TempAvoidTheEdgeLine) = "[LesbianStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Lesbian" And linesA(TempAvoidTheEdgeLine) = "[LesbianStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Lesbian" And linesA(TempAvoidTheEdgeLine) = "[BlowjobStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Blowjob" And linesA(TempAvoidTheEdgeLine) = "[BlowjobStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Blowjob" And linesA(TempAvoidTheEdgeLine) = "[FemdomStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femdom" And linesA(TempAvoidTheEdgeLine) = "[FemdomStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femdom" And linesA(TempAvoidTheEdgeLine) = "[FemsubStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femsub" And linesA(TempAvoidTheEdgeLine) = "[FemsubStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femsub" And linesA(TempAvoidTheEdgeLine) = "[JOIStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "JOI" And linesA(TempAvoidTheEdgeLine) = "[JOIStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "JOI" And linesA(TempAvoidTheEdgeLine) = "[CHStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "CH" And linesA(TempAvoidTheEdgeLine) = "[CHStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "CH" And linesA(TempAvoidTheEdgeLine) = "[GeneralStrokingOn]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "General" And linesA(TempAvoidTheEdgeLine) = "[GeneralStrokingOff]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "General" And linesA(TempAvoidTheEdgeLine) = "[StrokingEnd]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                    End While

                End Using

            Else






                ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value

                Using ioFileB As New StreamReader(AvoidTheEdgeVideo)
                    Dim linesB As New List(Of String)
                    Dim TempAvoidTheEdgeLine As Integer

                    TempAvoidTheEdgeLine = -1
                    While ioFileB.Peek <> -1
                        TempAvoidTheEdgeLine += 1
                        linesB.Add(ioFileB.ReadLine())
                        If ssh.VideoType = "Hardcore" And linesB(TempAvoidTheEdgeLine) = "[HardcoreStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Hardcore" And linesB(TempAvoidTheEdgeLine) = "[HardcoreStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Softcore" And linesB(TempAvoidTheEdgeLine) = "[SoftcoreStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Softcore" And linesB(TempAvoidTheEdgeLine) = "[SoftcoreStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Lesbian" And linesB(TempAvoidTheEdgeLine) = "[LesbianStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Lesbian" And linesB(TempAvoidTheEdgeLine) = "[LesbianStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Blowjob" And linesB(TempAvoidTheEdgeLine) = "[BlowjobStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Blowjob" And linesB(TempAvoidTheEdgeLine) = "[BlowjobStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femdom" And linesB(TempAvoidTheEdgeLine) = "[FemdomStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femdom" And linesB(TempAvoidTheEdgeLine) = "[FemdomStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femsub" And linesB(TempAvoidTheEdgeLine) = "[FemsubStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "Femsub" And linesB(TempAvoidTheEdgeLine) = "[FemsubStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "JOI" And linesB(TempAvoidTheEdgeLine) = "[JOIStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "JOI" And linesB(TempAvoidTheEdgeLine) = "[JOIStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "CH" And linesB(TempAvoidTheEdgeLine) = "[CHStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "CH" And linesB(TempAvoidTheEdgeLine) = "[CHStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                        If ssh.VideoType = "General" And linesB(TempAvoidTheEdgeLine) = "[GeneralStrokingOn]" Then AvoidTheEdgeLineStart = TempAvoidTheEdgeLine
                        If ssh.VideoType = "General" And linesB(TempAvoidTheEdgeLine) = "[GeneralStrokingOff]" Then AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine
                    End While

                End Using

            End If

            Dim ioFile As New StreamReader(AvoidTheEdgeVideo)
            Dim lines As New List(Of String)
            While ioFile.Peek <> -1
                lines.Add(ioFile.ReadLine())
            End While

            Dim AvoidTheEdgeLine As Integer

            AvoidTheEdgeLine = ssh.randomizer.Next(AvoidTheEdgeLineStart + 1, AvoidTheEdgeLineEnd)

            ssh.DomTask = lines(AvoidTheEdgeLine)
        End If

    End Sub

    Private Sub AvoidTheEdgeResume_Tick(sender As Object, e As EventArgs) Handles AvoidTheEdgeResume.Tick

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.AtECountdown < 6 Then Return
        If chatBox.Text <> "" And ssh.AtECountdown < 6 Then Return
        If ChatBox2.Text <> "" And ssh.AtECountdown < 6 Then Return

        ssh.AtECountdown -= 1
        If ssh.AtECountdown < 1 Then
            AvoidTheEdgeResume.Stop()

            ssh.FileGoto = "NoAvoidTheEdgeInstructions"
            ssh.SkipGotoLine = True
            GetGoto()
            'domVLC.playlist.play()
            DomWMP.Ctlcontrols.play()
            HandleScripts()
            ScriptTimer.Start()


        End If


    End Sub

#End Region ' Avoid the Edge

    Private Sub BtnToggleImageVideo_Click(sender As Object, e As EventArgs) Handles BtnToggleImageVideo.Click


        If mainPictureBox.Visible = True Then
            DomWMP.Visible = True
            mainPictureBox.Visible = False
        Else
            mainPictureBox.Visible = True
            DomWMP.Visible = False
        End If

    End Sub

    Public Sub RunModuleScript(IsEdging As Boolean)

        ssh.ShowModule = True

        ssh.TauntEdging = False

        ssh.AskedToGiveUpSection = False
        Dim ModuleList As New List(Of String)
        ModuleList.Clear()

        Dim ChastityModuleCheck As String = "*.txt"
        If My.Settings.Chastity = True And Not IsEdging Then
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.SubStroking = False
            ssh.SubEdging = False
            ssh.SubHoldingEdge = False
            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            EdgeTauntTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            ChastityModuleCheck = "*_CHASTITY.txt"
        End If

        If ssh.PlaylistFile.Count = 0 Then GoTo NoPlaylistModuleFile

        If ssh.Playlist = False Or ssh.PlaylistFile(ssh.PlaylistCurrent).Contains("Random Module") Then


NoPlaylistModuleFile:

            If ssh.SetModule <> "" Then

                ssh.FileText = ssh.SetModule
            Else

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Modules\", FileIO.SearchOption.SearchTopLevelOnly, ChastityModuleCheck)
                    Dim TempModule As String = foundFile
                    TempModule = Path.GetFileName(TempModule).Replace(".txt", "")

                    If IsEdging Then

                        Do Until Not TempModule.Contains("\")
                            TempModule = TempModule.Remove(0, 1)
                        Loop
                    End If

                    For x As Integer = 0 To FrmSettings.ModuleScripts.Items.Count - 1
                        If My.Settings.Chastity = True Then
                            If FrmSettings.ModuleScripts.Items(x) = TempModule And FrmSettings.ModuleScripts.GetItemChecked(x) = True And Not foundFile.Contains("_EDGING") Then
                                ModuleList.Add(foundFile)
                            End If
                        ElseIf IsEdging Then
                            If FrmSettings.ModuleScripts.Items(x) = TempModule And FrmSettings.ModuleScripts.GetItemChecked(x) = True And foundFile.Contains("_EDGING") Then
                                ModuleList.Add(foundFile)
                            End If
                        Else
                            If FrmSettings.ModuleScripts.Items(x) = TempModule And FrmSettings.ModuleScripts.GetItemChecked(x) = True And Not foundFile.Contains("_EDGING") And Not foundFile.Contains("_CHASTITY") Then
                                ModuleList.Add(foundFile)
                            End If
                        End If
                    Next
                Next

                If ModuleList.Count < 1 Then
                    If My.Settings.Chastity = True Then
                        ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\Module_CHASTITY.txt"
                    ElseIf IsEdging Then
                        ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\Module_EDGING.txt"
                    Else
                        ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\Module.txt"
                    End If
                Else
                    ssh.FileText = ModuleList(ssh.randomizer.Next(0, ModuleList.Count))
                End If
            End If

        Else
            If ssh.PlaylistFile(ssh.PlaylistCurrent).Contains("Regular-TeaseAI-Script") Then
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Modules\" & ssh.PlaylistFile(ssh.PlaylistCurrent)
                ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "")
                ssh.FileText = ssh.FileText & ".txt"
            Else
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Playlist\Modules\" & ssh.PlaylistFile(ssh.PlaylistCurrent) & ".txt"
            End If

        End If

        ssh.SetModule = ""

        ssh.DomTask = ssh.DomTask.Replace("@Module", "")


        If ssh.SetModuleGoto <> "" Then
            ssh.FileGoto = ssh.SetModuleGoto
            ssh.SkipGotoLine = True
            GetGoto()
            ssh.SetModuleGoto = ""
        Else
            ssh.StrokeTauntVal = -1
        End If

        If ssh.Playlist = True Then ssh.PlaylistCurrent += 1

        If Not IsEdging Then

            If ssh.Playlist = True Then ssh.BookmarkModule = False

            If ssh.BookmarkModule = True Then
                ssh.BookmarkModule = False
                ssh.FileText = ssh.BookmarkModuleFile
                ssh.StrokeTauntVal = ssh.BookmarkModuleLine
            End If

            ssh.ScriptTick = 3

        Else
            ssh.ScriptTick = 4
        End If

        ScriptTimer.Start()
    End Sub

    Public Sub RunLinkScript()

        ClearModes()

        If ssh.PlaylistFile.Count = 0 Then GoTo NoPlaylistLinkFile

        If ssh.Playlist = False Or ssh.PlaylistFile(ssh.PlaylistCurrent).Contains("Random Link") Then


NoPlaylistLinkFile:
            If ssh.SetLink <> "" Then
                ssh.FileText = ssh.SetLink
            Else


                Dim LinkList As New List(Of String)
                LinkList.Clear()


                Dim ChastityLinkCheck As String
                If My.Settings.Chastity = True Then
                    ChastityLinkCheck = "*_CHASTITY.txt"
                Else
                    ChastityLinkCheck = "*.txt"
                End If

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\Link\", FileIO.SearchOption.SearchTopLevelOnly, ChastityLinkCheck)
                    Dim TempLink As String = foundFile
                    TempLink = TempLink.Replace(".txt", "")
                    Do Until Not TempLink.Contains("\")
                        TempLink = TempLink.Remove(0, 1)
                    Loop
                    For x As Integer = 0 To FrmSettings.LinkScripts.Items.Count - 1
                        If My.Settings.Chastity = True Then
                            If FrmSettings.LinkScripts.Items(x) = TempLink And FrmSettings.LinkScripts.GetItemChecked(x) = True Then
                                LinkList.Add(foundFile)
                            End If
                        Else
                            If FrmSettings.LinkScripts.Items(x) = TempLink And FrmSettings.LinkScripts.GetItemChecked(x) = True And Not TempLink.Contains("_CHASTITY") Then
                                LinkList.Add(foundFile)
                            End If
                        End If

                    Next
                Next

                If LinkList.Count < 1 Then
                    If My.Settings.Chastity = True Then
                        ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\Link_CHASTITY.txt"
                    Else
                        ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\Link.txt"
                    End If
                Else
                    ssh.FileText = LinkList(ssh.randomizer.Next(0, LinkList.Count))
                End If

            End If

        Else
            If ssh.PlaylistFile(ssh.PlaylistCurrent).Contains("Regular-TeaseAI-Script") Then
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\Link\" & ssh.PlaylistFile(ssh.PlaylistCurrent)
                ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "")
                ssh.FileText = ssh.FileText & ".txt"
            Else
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Playlist\Link\" & ssh.PlaylistFile(ssh.PlaylistCurrent) & ".txt"
            End If

        End If

        ssh.SetLink = ""
        If ssh.WorshipMode = False Then
            ssh.LockImage = False
            If ssh.SlideshowLoaded = True Then
                ImageSlideShowNextButton.Enabled = True
                ImageSlideShowPreviousButton.Enabled = True
                PicStripTSMIdommeSlideshow.Enabled = True
            End If
        End If


        If ssh.SetLinkGoto <> "" Then
            ssh.FileGoto = ssh.SetLinkGoto
            ssh.SkipGotoLine = True
            GetGoto()
            ssh.SetLinkGoto = ""
        Else
            ssh.StrokeTauntVal = -1
        End If


        If ssh.Playlist = True Then ssh.PlaylistCurrent += 1
        If ssh.Playlist = True Then ssh.BookmarkLink = False

        If ssh.BookmarkLink = True Then
            ssh.BookmarkLink = False
            ssh.FileText = ssh.BookmarkLinkFile
            ssh.StrokeTauntVal = ssh.BookmarkLinkLine
        End If

        ssh.ScriptTick = 3
        ScriptTimer.Start()


    End Sub

    ''' <summary>
    ''' This is the Last or ending script
    ''' </summary>
    Public Sub RunLastScript()

        ClearModes()

        SetVariable("SYS_SubLeftEarly", 0)

        SetVariable("SYS_EndTotal", Val(GetVariable("SYS_EndTotal")) + 1)

        If ssh.Playlist AndAlso Not ssh.PlaylistFile(ssh.PlaylistCurrent).Contains("Random End") Then
            If ssh.PlaylistFile(ssh.PlaylistCurrent).Contains("Regular-TeaseAI-Script") Then
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\End\" & ssh.PlaylistFile(ssh.PlaylistCurrent)
                ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "")
                ssh.FileText = ssh.FileText & ".txt"
            Else
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Playlist\End\" & ssh.PlaylistFile(ssh.PlaylistCurrent) & ".txt"
            End If
        End If

        If ssh.WorshipMode = False Then
            If ssh.SlideshowLoaded = True Then
                ImageSlideShowNextButton.Enabled = True
                ImageSlideShowPreviousButton.Enabled = True
                PicStripTSMIdommeSlideshow.Enabled = True
            End If
            ssh.LockImage = False
        End If


        ssh.StrokeTauntVal = -1

        ssh.LastScript = True


        ssh.ScriptTick = 3
        ScriptTimer.Start()

    End Sub

    Public Sub RunLastBegScript()

        ClearModes()

        Dim EndList As New List(Of String)
        EndList.Clear()

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\End\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
            Dim TempEnd As String = foundFile
            TempEnd = TempEnd.Replace(".txt", "")
            Do Until Not TempEnd.Contains("\")
                TempEnd = TempEnd.Remove(0, 1)
            Loop
            For x As Integer = 0 To FrmSettings.EndScripts.Items.Count - 1

                If ssh.OrgasmRestricted = False Then

                    If FrmSettings.EndScripts.Items(x) = TempEnd And FrmSettings.EndScripts.GetItemChecked(x) = True And TempEnd.Contains("_BEG") Then
                        EndList.Add(foundFile)
                    End If
                Else
                    If FrmSettings.EndScripts.Items(x) = TempEnd And FrmSettings.EndScripts.GetItemChecked(x) = True And TempEnd.Contains("_RESTRICTED") Then
                        EndList.Add(foundFile)
                    End If

                End If

            Next
        Next


        If EndList.Count < 1 Then

            If ssh.OrgasmRestricted = False Then
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\End_BEG.txt"
            Else
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Scripts\End_RESTRICTED.txt"
            End If
        Else
            ssh.FileText = EndList(ssh.randomizer.Next(0, EndList.Count))
        End If

        ssh.LockImage = False
        If ssh.SlideshowLoaded = True Then
            ImageSlideShowNextButton.Enabled = True
            ImageSlideShowPreviousButton.Enabled = True
            PicStripTSMIdommeSlideshow.Enabled = True
        End If

        ssh.StrokeTauntVal = -1
        ssh.ScriptTick = 4
        ScriptTimer.Start()
        ssh.LastScript = True

        'RunFileText()

    End Sub

    Public Sub StopEverything()

        ScriptTimer.Stop()
        StrokeTimer.Stop()
        StrokeTauntTimer.Stop()
        CensorshipTimer.Stop()
        RLGLTimer.Stop()
        TnASlides.Stop()
        AvoidTheEdge.Stop()
        EdgeTauntTimer.Stop()
        HoldEdgeTimer.Stop()
        HoldEdgeTauntTimer.Stop()
        AvoidTheEdgeTaunts.Stop()
        RLGLTauntTimer.Stop()
        VideoTauntTimer.Stop()
        EdgeCountTimer.Stop()

        ssh.SubStroking = False
        ssh.SubEdging = False
        ssh.SubHoldingEdge = False
        ssh.AskedToSpeedUp = False
        ssh.AskedToSlowDown = False

        ssh.WorshipMode = False
        ssh.WorshipTarget = ""
        ssh.LongHold = False
        ssh.ExtremeHold = False

        ssh.MiniScript = False

        ' Unlock OrgasmChances
        FrmSettings.LockOrgasmChances(False)

        ClearModes()


        If FrmSettings.TBWebStop.Text <> "" Then
            Try
                FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text)
            Catch
            End Try
        End If

        StrokePace = 0

    End Sub

    Private Sub EdgeTauntTimer_Tick(sender As Object, e As EventArgs) Handles EdgeTauntTimer.Tick

        If MultipleEdgesTimer.Enabled = True Then Return
        If ssh.MiniScript = True Then Return
        If ssh.InputFlag = True Then Return

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.EdgeTauntInt < 6 Then Return
        If chatBox.Text <> "" And ssh.EdgeTauntInt < 6 Then Return
        If ChatBox2.Text <> "" And ssh.EdgeTauntInt < 6 Then Return

        FrmSettings.LBLDebugEdgeTauntTime.Text = ssh.EdgeTauntInt

        ssh.EdgeTauntInt -= 1

        If ssh.EdgeTauntInt < 1 Then

            Dim File2Read As String = ""

            If ssh.GlitterTease = False Then
                File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\Edge\Edge.txt"
            Else
                File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\Edge\GroupEdge.txt"
            End If

            'Read all lines of the given file.
            Dim ETLines As List(Of String) = Txt2List(File2Read)

            Try
                ETLines = FilterList(ETLines)
                ssh.DomTask = ETLines(ssh.randomizer.Next(0, ETLines.Count))
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Edge Taunt from file: " &
                               File2Read, ex, "EdgeTauntTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Edge Taunt"
            End Try

            ssh.EdgeTauntInt = ssh.randomizer.Next(30, 46)

        End If

    End Sub

#Region "--------------------------------------- Hold the Edge ------------------------------------------"

    Private Sub HoldEdgeTimer_Tick(sender As Object, e As EventArgs) Handles HoldEdgeTimer.Tick

        If ssh.MiniScript = True Then Return

        ssh.HoldEdgeTime += 1
        ssh.HoldEdgeTimeTotal += 1

        My.Settings.HoldEdgeTimeTotal = ssh.HoldEdgeTimeTotal

        If ssh.InputFlag = True Then Return

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return


        'If DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.HoldEdgeTick < 4 Then Return
        If chatBox.Text <> "" And ssh.HoldEdgeTick < 4 Then Return
        If ChatBox2.Text <> "" And ssh.HoldEdgeTick < 4 Then Return
        If ssh.FollowUp <> "" And ssh.HoldEdgeTick < 4 Then Return

        ssh.HoldEdgeTick -= 1

        FrmSettings.LBLDebugHoldEdgeTime.Text = ssh.HoldEdgeTick

        If ssh.HoldEdgeTick < 1 Then

            'stop
            ssh.LongHold = False
            ssh.ExtremeHold = False
            ssh.WorshipMode = False
            ssh.WorshipTarget = ""

            'If OrgasmAllowed = True Then GoTo AllowedOrgasm
            'If EdgeToRuin = True Or OrgasmRuined = True Then GoTo RuinedOrgasm

            If ssh.EdgeToRuin = True Or ssh.OrgasmRuined = True Then
                ssh.LastOrgasmType = "RUINED"
                ssh.OrgasmRuined = False
                GoTo RuinedOrgasm
            End If

            If ssh.OrgasmAllowed = True Then
                ssh.LastOrgasmType = "ALLOWED"
                ssh.OrgasmAllowed = False
                GoTo AllowedOrgasm
            End If

            If ssh.OrgasmDenied = True Then

                ssh.LastOrgasmType = "DENIED"

                If FrmSettings.CBDomDenialEnds.Checked = False And ssh.TeaseTick < 1 Then

                    Dim RepeatChance As Integer = ssh.randomizer.Next(0, 101)

                    If RepeatChance < 10 * FrmSettings.DominationLevel.Value Then
                        ssh.SubEdging = False
                        ssh.SubStroking = False
                        EdgeTauntTimer.Stop()

                        Dim RepeatList As New List(Of String)

                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Denial Continue\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                            RepeatList.Add(foundFile)
                        Next

                        If RepeatList.Count < 1 Then GoTo NoRepeatFiles

                        If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
                            If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60
                            If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60
                            If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60
                            If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60
                            If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60
                        Else
                            ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
                        End If

                        TeaseTimer.Start()

                        'ShowModule = True
                        ssh.StrokeTauntVal = -1
                        ssh.FileText = RepeatList(ssh.randomizer.Next(0, RepeatList.Count))
                        ssh.ScriptTick = 2
                        ScriptTimer.Start()
                        ssh.OrgasmDenied = False
                        ssh.OrgasmYesNo = False
                        ssh.EndTease = False
                        Return
                    End If

                End If


            End If

NoRepeatFiles:

            HoldEdgeTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            ssh.SubHoldingEdge = False
            ssh.SubStroking = False
            ssh.OrgasmYesNo = False
            ssh.DomTask = "#StopStroking"
            If ssh.Contact1Edge = True Then
                ssh.DomTask = "@Contact1 #StopStroking"
                ssh.Contact1Edge = False
            End If
            If ssh.Contact2Edge = True Then
                ssh.DomTask = "@Contact2 #StopStroking"
                ssh.Contact2Edge = False
            End If
            If ssh.Contact3Edge = True Then
                ssh.DomTask = "@Contact3 #StopStroking"
                ssh.Contact3Edge = False
            End If
            Return

RuinedOrgasm:

            My.Settings.LastRuined = FormatDateTime(Now, DateFormat.ShortDate)
            FrmSettings.LBLLastRuined.Text = My.Settings.LastRuined

            If FrmSettings.CBDomOrgasmEnds.Checked = False And ssh.OrgasmRuined = True And ssh.TeaseTick < 1 Then

                Dim RepeatChance As Integer = ssh.randomizer.Next(0, 101)

                If RepeatChance < 8 * FrmSettings.DominationLevel.Value Then

                    EdgeTauntTimer.Stop()
                    HoldEdgeTimer.Stop()
                    HoldEdgeTauntTimer.Stop()
                    ssh.SubHoldingEdge = False
                    ssh.SubStroking = False
                    ssh.EdgeToRuin = False
                    ssh.EdgeToRuinSecret = True

                    Dim RepeatList As New List(Of String)

                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Ruin Continue\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        RepeatList.Add(foundFile)
                    Next

                    If RepeatList.Count < 1 Then GoTo NoRepeatRFiles


                    If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
                        If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60
                        If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60
                        If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60
                        If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60
                        If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60
                    Else
                        ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
                    End If
                    TeaseTimer.Start()

                    'ShowModule = True
                    ssh.StrokeTauntVal = -1
                    ssh.FileText = RepeatList(ssh.randomizer.Next(0, RepeatList.Count))
                    ssh.ScriptTick = 2
                    ScriptTimer.Start()
                    ssh.OrgasmRuined = False
                    ssh.OrgasmYesNo = False
                    ssh.EndTease = False
                    Return
                End If

            End If



NoRepeatRFiles:



            ssh.DomTypeCheck = True
            HoldEdgeTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            ssh.SubHoldingEdge = False
            ssh.SubStroking = False
            ssh.EdgeToRuin = False
            ssh.EdgeToRuinSecret = True
            ssh.OrgasmYesNo = False
            ssh.DomChat = "#RuinYourOrgasm"
            If ssh.Contact1Edge = True Then
                ssh.DomChat = "@Contact1 #RuinYourOrgasm"
                ssh.Contact1Edge = False
            End If
            If ssh.Contact2Edge = True Then
                ssh.DomChat = "@Contact2 #RuinYourOrgasm"
                ssh.Contact2Edge = False
            End If
            If ssh.Contact3Edge = True Then
                ssh.DomChat = "@Contact3 #RuinYourOrgasm"
                ssh.Contact3Edge = False
            End If
            Return

AllowedOrgasm:

            If My.Settings.OrgasmsLocked = True Then

                If My.Settings.OrgasmsRemaining < 1 Then

                    Dim NoCumList As New List(Of String)

                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Out of Orgasms\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        NoCumList.Add(foundFile)
                    Next

                    If NoCumList.Count < 1 Then GoTo NoNoCumFiles


                    HoldEdgeTimer.Stop()
                    HoldEdgeTauntTimer.Stop()
                    ssh.SubHoldingEdge = False
                    ssh.SubStroking = False
                    ssh.OrgasmYesNo = False
                    'ShowModule = True
                    ssh.StrokeTauntVal = -1
                    ssh.FileText = NoCumList(ssh.randomizer.Next(0, NoCumList.Count))
                    ssh.ScriptTick = 2
                    ScriptTimer.Start()
                    Return
                End If


                My.Settings.OrgasmsRemaining -= 1


            End If

NoNoCumFiles:


            My.Settings.LastOrgasm = FormatDateTime(Now, DateFormat.ShortDate)
            FrmSettings.LBLLastOrgasm.Text = My.Settings.LastOrgasm

            If FrmSettings.CBDomOrgasmEnds.Checked = False And ssh.TeaseTick < 1 Then

                Dim RepeatChance As Integer = ssh.randomizer.Next(0, 101)

                If RepeatChance < 4 * FrmSettings.DominationLevel.Value Then

                    HoldEdgeTimer.Stop()
                    HoldEdgeTauntTimer.Stop()
                    ssh.SubHoldingEdge = False
                    ssh.SubStroking = False
                    ssh.EdgeToRuin = False
                    ssh.EdgeToRuinSecret = True
                    EdgeTauntTimer.Stop()

                    Dim RepeatList As New List(Of String)

                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Orgasm Continue\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                        RepeatList.Add(foundFile)
                    Next

                    If RepeatList.Count < 1 Then GoTo NoRepeatOFiles


                    If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
                        If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60
                        If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60
                        If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60
                        If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60
                        If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60
                    Else
                        ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
                    End If
                    TeaseTimer.Start()

                    'ShowModule = True
                    ssh.StrokeTauntVal = -1
                    ssh.FileText = RepeatList(ssh.randomizer.Next(0, RepeatList.Count))
                    ssh.ScriptTick = 2
                    ScriptTimer.Start()
                    ssh.OrgasmAllowed = False
                    ssh.OrgasmYesNo = False
                    ssh.EndTease = False
                    Return
                End If

            End If



NoRepeatOFiles:


            ssh.DomTypeCheck = True
            HoldEdgeTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            ssh.SubHoldingEdge = False
            ssh.SubStroking = False
            ssh.OrgasmYesNo = False
            'OrgasmAllowed = False
            ssh.DomChat = "#CumForMe"
            If ssh.Contact1Edge = True Then
                ssh.DomChat = "@Contact1 #CumForMe"
                ssh.Contact1Edge = False
            End If
            If ssh.Contact2Edge = True Then
                ssh.DomChat = "@Contact2 #CumForMe"
                ssh.Contact2Edge = False
            End If
            If ssh.Contact3Edge = True Then
                ssh.DomChat = "@Contact3 #CumForMe"
                ssh.Contact3Edge = False
            End If
            Return

        End If

    End Sub

    Private Sub HoldEdgeTauntTimer_Tick(sender As Object, e As EventArgs) Handles HoldEdgeTauntTimer.Tick

        If ssh.MiniScript = True Then Return
        If ssh.InputFlag = True Then Return

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.EdgeTauntInt < 6 Then Return
        If chatBox.Text <> "" And ssh.EdgeTauntInt < 6 Then Return
        If ChatBox2.Text <> "" And ssh.EdgeTauntInt < 6 Then Return

        ssh.EdgeTauntInt -= 1

        If ssh.EdgeTauntInt < 1 Then

            Dim File2Read As String = ""

            If ssh.GlitterTease = False Then
                File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\HoldTheEdge\HoldTheEdge.txt"
            Else
                File2Read = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Stroke\HoldTheEdge\GroupHoldTheEdge.txt"
            End If

            ' Read all lines of given file.
            Dim ETLines As List(Of String) = Txt2List(File2Read)

            Try
                ETLines = FilterList(ETLines)
                ssh.DomTask = ETLines(ssh.randomizer.Next(0, ETLines.Count))
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Hold the Edge Taunt from file: " &
                               File2Read, ex, "HoldEdgeTauntTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Hold the Edge Taunt"
            End Try

            ssh.EdgeTauntInt = ssh.randomizer.Next(15, 31)
        End If

    End Sub

#End Region ' Hold the Edge

    Public Sub CreateTaskLetter()
        ' This doesn't matter in this method, but it used to set the value, so blanking it for compatibility
        ssh.TaskText = String.Empty
        Dim domme As DommePersonality = CreateDommePersonality()
        Dim submissive As SubPersonality = CreateSubPersonality()

        Dim signedBy As String = domme.Name
        If FrmSettings.CBHonorificInclude.Checked = True Then
            signedBy = domme.Honorific + " " + domme.Name
        End If

        Dim taskLetter As TaskLetter = CreateTaskLetter(Application.StartupPath, domme, submissive, ssh.GeneralTime, signedBy)

        ssh.TaskTextDir = taskLetter.FileName

        My.Computer.FileSystem.WriteAllText(ssh.TaskTextDir, taskLetter.Body, False)


        LBLFileTransfer.Text = signedBy + " is sending you a file!"
        PNLFileTransfer.Visible = True
        PNLFileTransfer.BringToFront()

        StupidTimer.Start()

    End Sub

    Public Function CreateTaskLetter(basePath As String, personality As DommePersonality, submissive As SubPersonality, generalTime As String, signedBy As String) As TaskLetter
        Dim returnValue As TaskLetter = New TaskLetter()
        Dim filePath As String = basePath + "\Scripts\" + personality.PersonalityName

        returnValue.Body = CleanTaskLines(filePath + "\Tasks\Greeting.txt") + " " + Environment.NewLine + Environment.NewLine
        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Intro.txt") + " " + Environment.NewLine + Environment.NewLine

        If generalTime = "Afternoon" Then
            GoTo Afternoon
        End If
        If generalTime = "Night" Then
            GoTo Night
        End If

        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Task_1.txt") + " " + Environment.NewLine + Environment.NewLine
        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Link_1-2.txt") + " " + Environment.NewLine + Environment.NewLine

Afternoon:
        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Task_2.txt") + " " + Environment.NewLine + Environment.NewLine
        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Link_2-3.txt") + " " + Environment.NewLine + Environment.NewLine

Night:
        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Task_3.txt") + " " + Environment.NewLine + Environment.NewLine
        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Outro.txt") + " " + Environment.NewLine + Environment.NewLine

        returnValue.Body += CleanTaskLines(filePath + "\Tasks\Signature.txt") + " " + Environment.NewLine
        returnValue.Body += signedBy

        returnValue.Body = Regex.Replace(returnValue.Body, "[ ]{2,}", " ")

        Dim TempDate As String
        Dim TempDateNow As DateTime = DateTime.Now

        TempDate = TempDateNow.ToString("M dd")

        returnValue.FileName = filePath + "\Received Files\Tasks for " + TempDate + ".txt"

        Return returnValue
    End Function

    Public Function CleanTaskLines(ByVal dir As String) As String
        Try
            Dim taskLines As List(Of String) = Txt2List(dir)
            Dim taskArray As String()
            Dim taskList As New List(Of String)
            Dim randomizer As Random = New Random()

            taskLines = FilterList(taskLines)
            If taskLines.Count = 0 Then Throw New ArgumentException("The given file: """ & dir & """ was returned empty.")

            Dim taskEntry As String
            taskEntry = taskLines(randomizer.Next(0, taskLines.Count))

            Dim loopBuffer As Integer

            taskArray = taskEntry.Split(" ")
            For i As Integer = 0 To taskArray.Count - 1
                taskList.Add(taskArray(i))
            Next
            taskEntry = ""
            For i As Integer = 0 To taskList.Count - 1
                Try
                    loopBuffer = 0

PoundLoop:
                    loopBuffer += 1

                    taskList(i) = taskList(i).Replace(". #Emote", " #Emote")
                    taskList(i) = taskList(i).Replace(". #Grin", " #Grin")
                    taskList(i) = taskList(i).Replace(". #Lol", " #Lol.")

                    taskList(i) = PoundClean(taskList(i))
                    If taskEntry.Contains("#") And loopBuffer < 6 Then GoTo PoundLoop

                    taskEntry = taskEntry & taskList(i) & " "
                Catch
                End Try
            Next

            Dim int As Integer

            If taskEntry.Contains("#TaskEdges") Then
                Do
                    int = randomizer.Next(FrmSettings.NBTaskEdgesMin.Value, FrmSettings.NBTaskEdgesMax.Value + 1)
                    If int > 5 Then int = 5 * Math.Round(int / 5)
                    taskEntry = taskEntry.Replace("#TaskEdges", int)
                Loop Until Not taskEntry.Contains("#TaskEdges")
            End If

            If taskEntry.Contains("#TaskStrokes") Then
                Do
                    int = randomizer.Next(FrmSettings.NBTaskStrokesMin.Value, FrmSettings.NBTaskStrokesMax.Value + 1)
                    If int > 10 Then int = 10 * Math.Round(int / 10)
                    taskEntry = taskEntry.Replace("#TaskStrokes", int)
                Loop Until Not taskEntry.Contains("#TaskStrokes")
            End If

            If taskEntry.Contains("#TaskHours") Then
                Do
                    int = randomizer.Next(1, FrmSettings.DominationLevel.Value + 1) + FrmSettings.DominationLevel.Value
                    taskEntry = taskEntry.Replace("#TaskHours", int)
                Loop Until Not taskEntry.Contains("#TaskHours")
            End If

            If taskEntry.Contains("#TaskMinutes") Then
                Do
                    int = randomizer.Next(5, 13) * FrmSettings.DominationLevel.Value
                    taskEntry = taskEntry.Replace("#TaskMinutes", int)
                Loop Until Not taskEntry.Contains("#TaskMinutes")
            End If

            If taskEntry.Contains("#TaskSeconds") Then
                Do
                    int = randomizer.Next(10, 30) * FrmSettings.DominationLevel.Value * randomizer.Next(1, FrmSettings.DominationLevel.Value + 1)
                    taskEntry = taskEntry.Replace("#TaskSeconds", int)
                Loop Until Not taskEntry.Contains("#TaskSeconds")
            End If

            If taskEntry.Contains("#TaskAmountLarge") Then
                Do
                    int = (randomizer.Next(15, 26) * FrmSettings.DominationLevel.Value) * 2
                    If int > 5 Then int = 5 * Math.Round(int / 5)
                    taskEntry = taskEntry.Replace("#TaskAmountLarge", int)
                Loop Until Not taskEntry.Contains("#TaskAmountLarge")
            End If

            If taskEntry.Contains("#TaskAmountSmall") Then
                Do
                    int = (randomizer.Next(5, 11) * FrmSettings.DominationLevel.Value) / 2
                    If int > 5 Then int = 5 * Math.Round(int / 5)
                    taskEntry = taskEntry.Replace("#TaskAmountSmall", int)
                Loop Until Not taskEntry.Contains("#TaskAmountSmall")
            End If

            If taskEntry.Contains("#TaskAmount") Then
                Do
                    int = randomizer.Next(15, 26) * FrmSettings.DominationLevel.Value
                    If int > 5 Then int = 5 * Math.Round(int / 5)
                    taskEntry = taskEntry.Replace("#TaskAmount", int)
                Loop Until Not taskEntry.Contains("#TaskAmount")
            End If

            If taskEntry.Contains("#TaskStrokingTime") Then
                Do
                    int = randomizer.Next(FrmSettings.NBTaskStrokingTimeMin.Value, FrmSettings.NBTaskStrokingTimeMax.Value + 1)
                    int *= 60
                    Dim TConvert As String = ConvertSeconds(int)
                    taskEntry = taskEntry.Replace("#TaskStrokingTime", TConvert)
                Loop Until Not taskEntry.Contains("#TaskStrokingTime")
            End If

            If taskEntry.Contains("#TaskHoldTheEdgeTime") Then
                Do
                    int = randomizer.Next(FrmSettings.NBTaskEdgeHoldTimeMin.Value, FrmSettings.NBTaskEdgeHoldTimeMax.Value + 1)
                    int *= 60
                    Dim TConvert As String = ConvertSeconds(int)
                    taskEntry = taskEntry.Replace("#TaskHoldTheEdgeTime", TConvert)
                Loop Until Not taskEntry.Contains("#TaskHoldTheEdgeTime")
            End If

            If taskEntry.Contains("#TaskCBTTime") Then
                Do
                    int = randomizer.Next(FrmSettings.NBTaskCBTTimeMin.Value, FrmSettings.NBTaskCBTTimeMax.Value + 1)
                    int *= 60
                    Dim TConvert As String = ConvertSeconds(int)
                    taskEntry = taskEntry.Replace("#TaskCBTTime", TConvert)
                Loop Until Not taskEntry.Contains("#TaskCBTTime")
            End If

            taskEntry = taskEntry.Replace("<font color=""red"">", "")
            taskEntry = taskEntry.Replace("</font>", "")
            taskEntry = taskEntry.Replace("#Null", "")

            loopBuffer = 0

            Do
                loopBuffer += 1
                If loopBuffer > 4 Then Exit Do
                taskEntry = PoundClean(taskEntry)
            Loop Until Not taskEntry.Contains("#")


            taskEntry = CommandClean(taskEntry, True)

            taskEntry = StripCommands(taskEntry)

            taskEntry = Trim(taskEntry)

            If taskEntry.Contains("*") Then
                taskEntry = taskEntry.Replace(". *", " *")
                Dim emoToggle As Boolean = True
                For i As Integer = taskEntry.Length - 1 To 0 Step -1
                    If taskEntry.Substring(i, 1) = "*" Then
                        If emoToggle = False Then
                            emoToggle = True
                            taskEntry = taskEntry.Remove(i, 1).Insert(i, FrmSettings.TBEmote.Text)
                        Else
                            emoToggle = False
                            taskEntry = taskEntry.Remove(i, 1).Insert(i, FrmSettings.TBEmoteEnd.Text)
                        End If
                    End If
                Next
            End If

            Return taskEntry
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Log.WriteError("Error occurred during file processing:  """ & dir & """", ex, "CleanTaskLines(String)")
            Return "ERROR: Tease AI did not return a valid Task line"
        End Try
    End Function

    Private Sub BTNFIleTransferDismiss_Click(sender As Object, e As EventArgs) Handles BTNFIleTransferDismiss.Click

        PNLFileTransfer.Visible = False
        BTNFileTransferOpen.Visible = False
        BTNFIleTransferDismiss.Visible = False
        LBLFileTransfer.Text = domName.Text & " is sending you a file!"
        PBFileTransfer.Value = 0

    End Sub

    Public Function ShellExecute(ByVal File As String) As Boolean
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = File
        myProcess.StartInfo.UseShellExecute = True
        myProcess.StartInfo.RedirectStandardOutput = False
        myProcess.Start()
        myProcess.Dispose()
    End Function

    Public Sub BTNFileTransferOpen_Click(sender As Object, e As EventArgs) Handles BTNFileTransferOpen.Click

        ShellExecute(ssh.TaskTextDir)

        PNLFileTransfer.Visible = False
        BTNFileTransferOpen.Visible = False
        BTNFIleTransferDismiss.Visible = False
        LBLFileTransfer.Text = domName.Text & " is sending you a file!"
        PBFileTransfer.Value = 0

    End Sub

    Private Sub SlideshowTimer_Tick(sender As Object, e As EventArgs) Handles SlideshowTimer.Tick
        'TODO: Remove CrossForm data access
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.SlideshowLoaded = False Or FrmSettings.TimedSlideShowRadio.Checked = False Or ssh.TeaseVideo = True Or ssh.LockImage = True Or ssh.JustShowedBlogImage = True Or ssh.CustomSlideEnabled = True Then Return

        ssh.SlideshowTimerTick -= 1

        If ssh.SlideshowTimerTick < 1 Then

TryNext:
            If My.Settings.CBSlideshowRandom Then
                ssh.SlideshowMain.NavigateNextTease()
            Else
                ssh.SlideshowMain.NavigateForward()
            End If


            If Not (File.Exists(ssh.SlideshowMain.CurrentImage) _
                    Or IsUrl(ssh.SlideshowMain.CurrentImage)) Then
                ClearMainPictureBox()
                Exit Sub
            End If

            Try
                ShowImage(ssh.SlideshowMain.CurrentImage, True)
                ssh.JustShowedBlogImage = False
                ssh.JustShowedSlideshowImage = True

            Catch
                GoTo TryNext
            End Try


            ssh.SlideshowTimerTick = FrmSettings.SlideShowNumBox.Value
        End If

    End Sub

    Public Sub GetEdgeTickCheck()

        If ssh.AlreadyStrokingEdge = True Then

            If ssh.AvgEdgeCount < 5 Then
                ssh.EdgeTickCheck = 60
            Else
                ssh.EdgeTickCheck = ssh.AvgEdgeStroking
            End If

        Else

            If ssh.AvgEdgeCountRest < 5 Then
                ssh.EdgeTickCheck = 300
            Else
                ssh.EdgeTickCheck = ssh.AvgEdgeNoTouch
            End If

        End If

    End Sub

    Private Sub EdgeCountTimer_Tick(sender As Object, e As EventArgs) Handles EdgeCountTimer.Tick

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        ssh.EdgeCountTick += 1

        If FrmSettings.UseAverageEdgeThresholdCB.Checked = True Then
            If ssh.EdgeCountTick > ssh.EdgeTickCheck Then ssh.LongEdge = True
        Else
            If ssh.EdgeCountTick > FrmSettings.NBLongEdge.Value * 60 Then ssh.LongEdge = True
        End If


        Dim m As Integer = TimeSpan.FromSeconds(ssh.EdgeCountTick).Minutes
        Dim s As Integer = TimeSpan.FromSeconds(ssh.EdgeCountTick).Seconds


        Dim TST As TimeSpan = TimeSpan.FromSeconds(ssh.EdgeCountTick)
    End Sub

    Private Sub StrokeTimeTotalTimer_Tick(sender As Object, e As EventArgs) Handles StrokeTimeTotalTimer.Tick

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.SubStroking = False Then Return

        ssh.StrokeTimeTotal += 1

        My.Settings.StrokeTimeTotal = ssh.StrokeTimeTotal

        Dim STT As TimeSpan = TimeSpan.FromSeconds(ssh.StrokeTimeTotal)

        'LBLStrokeTimeTotal.Text = String.Format("{0:000} D {1:00} H {2:00} M {3:00} S", STT.Days, STT.Hours, STT.Minutes, STT.Seconds)
        FrmSettings.LBLStrokeTimeTotal.Text = String.Format("{0:0000}:{1:00}:{2:00}:{3:00}", STT.Days, STT.Hours, STT.Minutes, STT.Seconds)


    End Sub

    Private Sub TnAFastSlides_Tick(sender As Object, e As EventArgs) Handles TnASlides.Tick
        Dim tmpSw As New Stopwatch

RestartFunction:
        tmpSw.Restart()
        Try
            If ssh.BoobList.Count < 1 Then Throw New Exception("No Boobs-images loaded.")
            If ssh.AssList.Count < 1 Then Throw New Exception("No Butt-images loaded.")

            Dim tmpImageToShow As String = ""
            Dim tmpLateSet As Boolean

            If New Random().Next(0, 101) < 51 Then
                tmpImageToShow = ssh.BoobList(ssh.randomizer.Next(0, ssh.BoobList.Count))
                tmpLateSet = True
            Else
                tmpImageToShow = ssh.AssList(ssh.randomizer.Next(0, ssh.AssList.Count))
                tmpLateSet = False
            End If

            Try
                ShowImage(tmpImageToShow, True)

                If tmpLateSet Then
                    ssh.BoobImage = True
                    ssh.AssImage = False
                Else
                    ssh.BoobImage = False
                    ssh.AssImage = True
                End If

                ' If the elapsed time to load an image was longer as the Timer.Interval
                ' we restart the function instantly, to avoid an unnecessary delay.
                ' If it took way too long and the Timer was stopped during imagedownload, 
                ' we dont want the timer to restart.
                If tmpSw.ElapsedMilliseconds > TnASlides.Interval And TnASlides.Enabled Then
                    GoTo RestartFunction
                End If
            Catch ex As Exception
                ' @@@@@@@@@@@@@@@@ Exception while loading image @@@@@@@@@@@@@@@@@
                ' Remove the ImagePath and retry.
                ssh.BoobList.RemoveAll(Function(x) x.Contains(tmpImageToShow))
                ssh.AssList.RemoveAll(Function(x) x.Contains(tmpImageToShow))
                GoTo RestartFunction
            End Try
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            TnASlides.Stop()
            Log.WriteError(ex.Message & vbCrLf & "TnA Slideshow will stop.", ex, "Exception in TnASlieds.Tick occured")
        End Try
    End Sub

#Region "---------------------------------------------------- Domme-WMP -------------------------------------------------------"

    Private Sub DomWMP_PlayStateChange(sender As Object, e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles DomWMP.PlayStateChange


        If (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying) Then
            If FrmSettings.CBMuteMedia.Checked = True Then
                DomWMP.settings.mute = True
            End If
        End If

        If (DomWMP.playState = WMPLib.WMPPlayState.wmppsStopped AndAlso mySession IsNot Nothing) Then
            mySession.VideoStopped()
        End If

        If (DomWMP.playState = WMPLib.WMPPlayState.wmppsStopped) Then

            VideoTimer.Stop()

            ssh.EdgeVideo = False
            ssh.YesVideo = False
            ssh.NoVideo_Mode = False
            ssh.CameVideo = False
            ssh.RuinedVideo = False

            DomWMP.currentPlaylist.clear()


            If ssh.CensorshipGame = True Then
                CensorshipTimer.Stop()
                CensorshipBar.Visible = False
                ssh.CensorshipGame = False
                ssh.VideoTease = False

                If ssh.RandomizerVideoTease = True Then
                    ScriptTimer.Stop()
                    mySession.Session.Domme.WasGreeted = False
                    ssh.RandomizerVideoTease = False
                    StopEverything()
                    Return
                End If

                RunFileText()
            End If

            If ssh.AvoidTheEdgeGame = True Then

                ssh.TeaseVideo = False
                ssh.AvoidTheEdgeGame = False
                ssh.AvoidTheEdgeStroking = False
                AvoidTheEdgeTaunts.Stop()
                ssh.VideoTease = False
                ssh.SubStroking = False

                If ssh.RandomizerVideoTease = True Then
                    ScriptTimer.Stop()
                    mySession.Session.Domme.WasGreeted = False
                    ssh.RandomizerVideoTease = False
                    StopEverything()
                    Return
                End If

                ssh.StrokeTauntVal = ssh.TempStrokeTauntVal
                ssh.FileText = ssh.TempFileText

                ssh.ScriptTick = 2
                ScriptTimer.Start()

                'RunFileText()




                'AvoidTheEdge.Stop()
                'AvoidTheEdgeGame = False
                'SkipGotoLine = True
                'If AvoidTheEdgeTimerTick < 1 Then
                'FileGoto = "(AvoidTheEdge Video Stop)"
                'Else
                '   FileGoto = "(AvoidTheEdge Video Continue)"
                'End If
                'GetGoto()
                'RunFileText()
            End If

            If ssh.RLGLGame = True Then
                RLGLTimer.Stop()
                RLGLTauntTimer.Stop()
                ssh.RLGLGame = False
                ssh.VideoTease = False
                ssh.SubStroking = False


                If ssh.RandomizerVideoTease = True Then
                    ScriptTimer.Stop()
                    mySession.Session.Domme.WasGreeted = False
                    ssh.RandomizerVideoTease = False
                    StopEverything()
                    Return
                End If

                ssh.ScriptTick = 1
                ScriptTimer.Start()

                Return
            End If


            If ssh.TeaseVideo = True Then
                ssh.TeaseVideo = False
                DomWMP.Ctlcontrols.pause()
                RunFileText()
            End If


            If ssh.LockVideo = False Then
                mainPictureBox.Visible = True
                DomWMP.Visible = False
            End If


        End If

    End Sub

    Private Sub WMPTimer_Tick(sender As Object, e As EventArgs) Handles WMPTimer.Tick
        If (DomWMP.playState = WMPLib.WMPPlayState.wmppsStopped AndAlso mySession IsNot Nothing) Then
            mySession.VideoStopped()
        End If


        If DomWMP.currentPlaylist.count <> 0 Then
            Try
                Dim VideoLength As Integer = DomWMP.currentMedia.duration
                Dim VideoRemaining As Integer = Math.Floor(DomWMP.currentMedia.duration - DomWMP.Ctlcontrols.currentPosition)
            Catch
            End Try
        End If

        If ssh.DomTypeCheck = True Or DomWMP.playState = WMPLib.WMPPlayState.wmppsStopped Or DomWMP.playState = WMPLib.WMPPlayState.wmppsPaused Then Return

        ssh.VidFile = Path.GetFileName(DomWMP.URL.ToString)

        Dim VidSplit As String() = ssh.VidFile.Split(".")
        ssh.VidFile = ""
        For i As Integer = 0 To VidSplit.Count - 2
            ssh.VidFile = ssh.VidFile + VidSplit(i)
        Next
        If ssh.VidFile = "" Then Exit Sub
        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Scripts\" & ssh.VidFile & ".txt") Then
            Dim SubCheck As String()
            Dim PlayPos As Integer
            Dim WMPPos As Integer = Math.Ceiling(DomWMP.Ctlcontrols.currentPosition)

            Dim SubList As New List(Of String)
            SubList = Txt2List(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Scripts\" & ssh.VidFile & ".txt")

            If Not SubList Is Nothing Then
                For i As Integer = 0 To SubList.Count - 1
                    SubCheck = SubList(i).Split("]")
                    SubCheck(0) = SubCheck(0).Replace("[", "")
                    Dim SubCheck2 As String() = SubCheck(0).Split(":")

                    PlayPos = SubCheck2(0) * 3600
                    PlayPos += SubCheck2(1) * 60
                    PlayPos += SubCheck2(2)

                    If WMPPos = PlayPos Then
                        ssh.DomTask = SubCheck(1)
                    End If
                Next
            End If
        End If


    End Sub

#End Region 'Domme-WMP

    Private Sub domAvatar_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles domAvatar.MouseEnter
        If FrmSettings.Visible = False And GamesWindow.Visible = False Then domAvatar.Focus()
    End Sub

    Private Sub domAvatar_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles domAvatar.MouseWheel



        If domAvatar.SizeMode = PictureBoxSizeMode.StretchImage Then
            domAvatar.SizeMode = PictureBoxSizeMode.Zoom
            My.Settings.DomAVStretch = False
        Else
            domAvatar.SizeMode = PictureBoxSizeMode.StretchImage
            My.Settings.DomAVStretch = True
        End If

    End Sub

    Private Sub WaitTimer_Tick(sender As Object, e As EventArgs) Handles WaitTimer.Tick
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTypeCheck = True Or ssh.YesOrNo = True Then Return

        ssh.WaitTick -= 1

        If ssh.WaitTick < 1 Then
            WaitTimer.Stop()
            ssh.ScriptTick = 1
        End If


    End Sub

    Private Sub StupidTimer_Tick(sender As Object, e As EventArgs) Handles StupidTimer.Tick

        If PBFileTransfer.Value = PBFileTransfer.Maximum Then
            StupidTimer.Enabled = False

            LBLFileTransfer.Text = "Download complete!"
            BTNFileTransferOpen.Visible = True
            BTNFIleTransferDismiss.Visible = True
            Exit Sub
        End If

        PBFileTransfer.Value += 1

    End Sub

    Public Sub SaveExercise()
        If FormLoading Then Return
        Dim fileStream As New FileStream(Application.StartupPath & "\System\VitalSub\ExerciseList.cld", FileMode.Create)
        Dim binaryWriter As New BinaryWriter(fileStream)
        For i = 0 To CLBExercise.Items.Count - 1
            binaryWriter.Write(CLBExercise.Items(i).ToString())
            binaryWriter.Write(CLBExercise.GetItemChecked(i))
        Next
        binaryWriter.Close()
        fileStream.Dispose()
    End Sub

    Public Sub LoadExercise()
        CLBExercise.Items.Clear()
        Dim myCldAccessor As ICldAccessor = New CldAccessor()
        Dim fileStream As New FileStream(Application.StartupPath & "\System\VitalSub\ExerciseList.cld", FileMode.Open)
        Dim binaryReader As New BinaryReader(fileStream)
        CLBExercise.BeginUpdate()
        Do While fileStream.Position < fileStream.Length
            CLBExercise.Items.Add(binaryReader.ReadString)
            CLBExercise.SetItemChecked(CLBExercise.Items.Count - 1, binaryReader.ReadBoolean)
        Loop
        CLBExercise.EndUpdate()
        binaryReader.Close()
        fileStream.Dispose()
    End Sub

    Public Sub RefreshCards()
        Try
            GamesWindow.GoldN1.Text = FrmSettings.GN1.Text
            GamesWindow.GoldN2.Text = FrmSettings.GN2.Text
            GamesWindow.GoldN3.Text = FrmSettings.GN3.Text
            GamesWindow.GoldN4.Text = FrmSettings.GN4.Text
            GamesWindow.GoldN5.Text = FrmSettings.GN5.Text
            GamesWindow.GoldN6.Text = FrmSettings.GN6.Text

            GamesWindow.GoldP1.Image = Image.FromFile(My.Settings.GP1)
            GamesWindow.GoldP2.Image = Image.FromFile(My.Settings.GP2)
            GamesWindow.GoldP3.Image = Image.FromFile(My.Settings.GP3)
            GamesWindow.GoldP4.Image = Image.FromFile(My.Settings.GP4)
            GamesWindow.GoldP5.Image = Image.FromFile(My.Settings.GP5)
            GamesWindow.GoldP6.Image = Image.FromFile(My.Settings.GP6)

            GamesWindow.SilverN1.Text = FrmSettings.SN1.Text
            GamesWindow.SilverN2.Text = FrmSettings.SN2.Text
            GamesWindow.SilverN3.Text = FrmSettings.SN3.Text
            GamesWindow.SilverN4.Text = FrmSettings.SN4.Text
            GamesWindow.SilverN5.Text = FrmSettings.SN5.Text
            GamesWindow.SilverN6.Text = FrmSettings.SN6.Text

            GamesWindow.SilverP1.Image = Image.FromFile(My.Settings.SP1)
            GamesWindow.SilverP2.Image = Image.FromFile(My.Settings.SP2)
            GamesWindow.SilverP3.Image = Image.FromFile(My.Settings.SP3)
            GamesWindow.SilverP4.Image = Image.FromFile(My.Settings.SP4)
            GamesWindow.SilverP5.Image = Image.FromFile(My.Settings.SP5)
            GamesWindow.SilverP6.Image = Image.FromFile(My.Settings.SP6)

            GamesWindow.BronzeN1.Text = FrmSettings.BN1.Text
            GamesWindow.BronzeN2.Text = FrmSettings.BN2.Text
            GamesWindow.BronzeN3.Text = FrmSettings.BN3.Text
            GamesWindow.BronzeN4.Text = FrmSettings.BN4.Text
            GamesWindow.BronzeN5.Text = FrmSettings.BN5.Text
            GamesWindow.BronzeN6.Text = FrmSettings.BN6.Text

            GamesWindow.BronzeP1.Image = Image.FromFile(My.Settings.BP1)
            GamesWindow.BronzeP2.Image = Image.FromFile(My.Settings.BP2)
            GamesWindow.BronzeP3.Image = Image.FromFile(My.Settings.BP3)
            GamesWindow.BronzeP4.Image = Image.FromFile(My.Settings.BP4)
            GamesWindow.BronzeP5.Image = Image.FromFile(My.Settings.BP5)
            GamesWindow.BronzeP6.Image = Image.FromFile(My.Settings.BP6)
        Catch
            ' This is intentionally suppressed because RefreshCards is poorly written and throws exceptions a lot
        End Try
    End Sub

    Private Sub VideoTauntTimer_Tick(sender As Object, e As EventArgs) Handles VideoTauntTimer.Tick

        'TODO: Merge redundant code: VideoTauntTimer_Tick, RLGLTauntTimer_Tick, AvoidTheEdgeTaunts_Tick
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return
        If ssh.MiniScript = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.VideoTauntTick < 6 Then Return
        If chatBox.Text <> "" And ssh.VideoTauntTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.VideoTauntTick < 6 Then Return

        ssh.VideoTauntTick -= 1


        If ssh.VideoTauntTick < 1 Then

            Dim FrequencyTemp As Integer = ssh.randomizer.Next(1, 101)
            If FrequencyTemp > FrmSettings.TauntSlider.Value * 5 Then
                ssh.VideoTauntTick = ssh.randomizer.Next(20, 31)
                Return
            End If

            Dim VTDir As String

            If ssh.RLGLGame = True Then VTDir = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Red Light Green Light\Taunts.txt"
            'TODO: Prevent File.Exits() with String.Empty
            If Not File.Exists(VTDir) Then Return

            ' Read all lines of the given file.
            Dim VTList As List(Of String) = Txt2List(VTDir)

            Try
                VTList = FilterList(VTList)
                If VTList.Count < 1 Then Return
                ssh.DomTask = VTList(ssh.randomizer.Next(0, VTList.Count))
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Video Taunt from file: " &
                               VTDir, ex, "VideoTaunTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Video Taunt"
            End Try

            ssh.VideoTauntTick = ssh.randomizer.Next(20, 31)
        End If
    End Sub

    Private Sub TeaseTimer_Tick(sender As Object, e As EventArgs) Handles TeaseTimer.Tick


        FrmSettings.LBLDebugTeaseTime.Text = ssh.TeaseTick
        ssh.TeaseTick -= 1

        If ssh.TeaseTick < 1 Then TeaseTimer.Stop()



    End Sub

    Public Sub RLGLTauntTimer_Tick(sender As Object, e As EventArgs) Handles RLGLTauntTimer.Tick
        'TODO: Merge redundant code: VideoTauntTimer_Tick, RLGLTauntTimer_Tick, AvoidTheEdgeTaunts_Tick
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return
        If ssh.MiniScript = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.RLGLTauntTick < 6 Then Return
        If chatBox.Text <> "" And ssh.RLGLTauntTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.RLGLTauntTick < 6 Then Return

        ssh.RLGLTauntTick -= 1


        If ssh.RLGLTauntTick < 1 Then

            Dim FrequencyTemp As Integer = ssh.randomizer.Next(1, 101)
            If FrequencyTemp > FrmSettings.TauntSlider.Value * 5 Then
                ssh.RLGLTauntTick = ssh.randomizer.Next(20, 31)
                Return
            End If

            Dim VTDir As String

            VTDir = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Red Light Green Light\Taunts.txt"

            If Not File.Exists(VTDir) Then Return

            ' Read all lines of the given file.
            Dim VTList As List(Of String) = Txt2List(VTDir)

            Try
                VTList = FilterList(VTList)
                If VTList.Count < 1 Then Return
                ssh.DomTask = VTList(ssh.randomizer.Next(0, VTList.Count))
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Video Taunt from file: " &
                               VTDir, ex, "RLGLTauntTimer.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Video Taunt"
            End Try

            ssh.RLGLTauntTick = ssh.randomizer.Next(20, 31)

        End If
    End Sub

    Private Sub AvoidTheEdgeTaunts_Tick(sender As Object, e As EventArgs) Handles AvoidTheEdgeTaunts.Tick
        'TODO: Merge redundant code: VideoTauntTimer_Tick, RLGLTauntTimer_Tick, AvoidTheEdgeTaunts_Tick
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If ssh.DomTyping = True Then Return
        If ssh.DomTypeCheck = True And ssh.AvoidTheEdgeTick < 6 Then Return
        If chatBox.Text <> "" And ssh.AvoidTheEdgeTick < 6 Then Return
        If ChatBox2.Text <> "" And ssh.AvoidTheEdgeTick < 6 Then Return



        ssh.AvoidTheEdgeTick -= 1


        If ssh.AvoidTheEdgeTick < 1 Then

            Dim FrequencyTemp As Integer = ssh.randomizer.Next(1, 101)
            If FrequencyTemp > FrmSettings.TauntSlider.Value * 5 Then
                ssh.AvoidTheEdgeTick = ssh.randomizer.Next(20, 31)
                Return
            End If

            Dim VTDir As String

            VTDir = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Video\Avoid The Edge\Taunts.txt"

            If Not File.Exists(VTDir) Then Return

            ' Read all lines of the given file.
            Dim VTList As List(Of String) = Txt2List(VTDir)

            Try
                VTList = FilterList(VTList)
                If VTList.Count < 1 Then Return
                ssh.DomTask = VTList(ssh.randomizer.Next(0, VTList.Count))
            Catch ex As Exception
                Log.WriteError("Tease AI did not return a valid Video Taunt from file: " &
                               VTDir, ex, "AvoidTheEdgeTaunts.Tick")
                ssh.DomTask = "ERROR: Tease AI did not return a valid Video Taunt"
            End Try

            ssh.AvoidTheEdgeTick = ssh.randomizer.Next(20, 31)
        End If
    End Sub

#Region "-------------------------------------------------- MainPictureBox ----------------------------------------------------"

    Private Sub mainPictureBox_LoadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles mainPictureBox.LoadCompleted
        ssh.ImageLocation = mainPictureBox.ImageLocation
        CheckDommeTags()
    End Sub

    Private Sub mainPictureBox_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainPictureBox.MouseDown
        If e.Button = MouseButtons.Right Then
            PictureStrip.Show(CType(sender, Control), e.Location)
        End If
    End Sub



#End Region ' MainPictureBox

#Region "-------------------------------------------------- PictureStrip ------------------------------------------------------"

    Private Sub PictureStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PictureStrip.Opening

        If mainPictureBox.Image Is Nothing Then
            Return
        End If
        Dim sh As ContactData = ssh.SlideshowMain

        PicStripTmsiDisableAnimation.Enabled = ImageAnimator.CanAnimate(mainPictureBox.Image) AndAlso ImageAnimator_OnFrameChangedAdded
        PicStripTmsiDisableAnimation.Checked = ImageAnimator.CanAnimate(mainPictureBox.Image) AndAlso
            ImageAnimator_OnFrameChangedAdded AndAlso
            Not mreImageanimator.WaitOne(0)

        If IsUrl(ssh.ImageLocation) Then
            PicStripTSMIsaveImage.Enabled = True
            PicStripTSMISaveImageTo.Enabled = True
            PicStripTSMIremoveFromURL.Enabled = True
        ElseIf ssh.ImageLocation = "" OrElse sh.ImageList.Contains(ssh.ImageLocation) Then
            PicStripTSMIcopyImageLocation.Enabled = False
            PicStripTSMIsaveImage.Enabled = False
            PicStripTSMISaveImageTo.Enabled = False
            PicStripTSMIlikeImage.Enabled = False
            PicStripTSMIlikeImage.Checked = False
            PicStripTSMIdislikeImage.Enabled = False
            PicStripTSMIdislikeImage.Checked = False
            PicStripTSMIremoveFromURL.Enabled = False

            If sh.ImageList.Contains(ssh.ImageLocation) Then
                PicStripTSMIdommeSlideshow.Enabled = True
                PicStripTSMIcopyImageLocation.Enabled = True
            End If
            Return
        End If

        PicStripTSMIcopyImageLocation.Enabled = True
        PicStripTSMIlikeImage.Enabled = True
        PicStripTSMIlikeImage.Checked = False
        PicStripTSMIdislikeImage.Enabled = True
        PicStripTSMIdislikeImage.Checked = False

        Dim tmp As List(Of String) = Txt2List(myPathsAccessor.LikedImages)
        If tmp.Contains(ssh.ImageLocation) Then PicStripTSMIlikeImage.Checked = True

        tmp = Txt2List(myPathsAccessor.DislikedImages)
        If tmp.Contains(ssh.ImageLocation) Then PicStripTSMIdislikeImage.Checked = True
    End Sub

    Private Sub PictureStripTmsiDisableAnimation_Click(sender As Object, e As EventArgs) Handles PicStripTmsiDisableAnimation.Click
        If mreImageanimator.WaitOne(0) Then
            mreImageanimator.Reset()
        Else
            mreImageanimator.Set()
        End If
    End Sub

    Private Sub PicStripTSMIcopyImageLocation_Click(sender As Object, e As EventArgs) Handles PicStripTSMIcopyImageLocation.Click
        My.Computer.Clipboard.SetText(ssh.ImageLocation)
    End Sub

    Public Function GetBlogPath(sender As ToolStripMenuItem, fileName As String) As String
        If sender Is PicStripTSMIsaveHardcore Then : Return My.Settings.IHardcore
        ElseIf sender Is PicStripTSMIsaveSoftcore Then : Return My.Settings.ISoftcore
        ElseIf sender Is PicStripTSMIsaveLesbian Then : Return My.Settings.ILesbian
        ElseIf sender Is PicStripTSMIsaveBlowjob Then : Return My.Settings.IBlowjob
        ElseIf sender Is PicStripTSMIsaveFemdom Then : Return My.Settings.IFemdom
        ElseIf sender Is PicStripTSMIsaveLezdom Then : Return My.Settings.ILezdom
        ElseIf sender Is PicStripTSMIsaveHentai Then : Return My.Settings.IHentai
        ElseIf sender Is PicStripTSMIsaveGay Then : Return My.Settings.IGay
        ElseIf sender Is PicStripTSMIsaveMaledom Then : Return My.Settings.IMaledom
        ElseIf sender Is PicStripTSMIsaveCaptions Then : Return My.Settings.ICaptions
        ElseIf sender Is PicStripTSMIsaveGeneral Then : Return My.Settings.IGeneral
        ElseIf sender Is PicStripTSMIsaveBoobs Then : Return My.Settings.LBLBoobPath
        ElseIf sender Is PicStripTSMIsaveButts Then : Return My.Settings.LBLButtPath
        ElseIf sender Is PicStripTSMIsaveImage Then
            SaveFileDialog1.Filter = "jpegs|*.jpg|gifs|*.gif|pngs|*.png|Bitmaps|*.bmp"
            SaveFileDialog1.FilterIndex = 1
            SaveFileDialog1.RestoreDirectory = True
            SaveFileDialog1.FileName = fileName
            SaveFileDialog1.CheckFileExists = False

            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                fileName = Path.GetFileName(SaveFileDialog1.FileName)
            End If

            Return fileName
        ElseIf sender Is PicStripTSMIlikeImage Then : Return myPathsAccessor.LikedImages
        ElseIf sender Is PicStripTSMIdislikeImage Then : Return myPathsAccessor.DislikedImages
        Else : Throw New NotImplementedException("Action for this button is not implemented.")
        End If

    End Function

    Public Sub PicStripTSMI_SaveImage(sender As Object, e As EventArgs) Handles PicStripTSMIsaveHardcore.Click,
        PicStripTSMIsaveSoftcore.Click, PicStripTSMIsaveLesbian.Click, PicStripTSMIsaveBlowjob.Click, PicStripTSMIsaveFemdom.Click,
        PicStripTSMIsaveLezdom.Click, PicStripTSMIsaveHentai.Click, PicStripTSMIsaveGay.Click, PicStripTSMIsaveMaledom.Click,
        PicStripTSMIsaveCaptions.Click, PicStripTSMIsaveGeneral.Click, PicStripTSMIsaveBoobs.Click, PicStripTSMIsaveButts.Click, PicStripTSMIsaveImage.Click

        Dim fileName As String = Path.GetFileName(mainPictureBox.ImageLocation)
        Dim blogPath As String = GetBlogPath(CType(sender, ToolStripMenuItem), fileName)
        Dim findFullPath As Result(Of String) = Result.Ok(blogPath) _
                .Ensure(Function(p) Directory.Exists(p), "Unable to find the directroy """ & blogPath & """" & Environment.NewLine & Environment.NewLine & "Please check your image settings.") _
                .OnSuccess(Function(p) p + Path.DirectorySeparatorChar + fileName)

        Dim fullPath = findFullPath.GetResultOrDefault()
        If File.Exists(fullPath) AndAlso Not SaveFileDialog1.CheckFileExists Then
            Dim dialogResult = MessageBox.Show(Me, fileName & " already exists in this directory!" & Environment.NewLine & Environment.NewLine & "Do you wish to overwrite?", "Caution!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If dialogResult = DialogResult.No Then
                Return
            End If
            My.Computer.FileSystem.DeleteFile(fullPath)
        End If

        mainPictureBox.Image.Save(fullPath)
    End Sub

    Private Sub PicStripTSMIlikeImage_Click(sender As Object, e As EventArgs) Handles PicStripTSMIlikeImage.Click,
                                                                                      PicStripTSMIdislikeImage.Click
        If String.IsNullOrWhiteSpace(ssh.ImageLocation = "") Then Exit Sub

        Dim lazytext As String = ""
        Try
            Dim tmpTsmi As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
            Dim tmpFilePath As String = GetBlogPath(tmpTsmi, String.Empty)

            If tmpTsmi.Checked Then
                lazytext = "remove path from file :" & tmpFilePath
                TxtRemoveLine(tmpFilePath, ssh.ImageLocation)
                tmpTsmi.Checked = False
            ElseIf File.Exists(tmpFilePath) Then
                lazytext = "append path  to file :" & tmpFilePath
                My.Computer.FileSystem.WriteAllText(tmpFilePath, Environment.NewLine & ssh.ImageLocation, True)
            Else
                lazytext = "add path to new file :" & tmpFilePath
                ' create a new file
                My.Computer.FileSystem.WriteAllText(tmpFilePath, ssh.ImageLocation, True)
            End If
            tmpTsmi.Checked = True
        Catch ex As Exception
            lazytext = "Unable to " & lazytext
            MessageBox.Show(Me, lazytext & vbCrLf _
                            & ex.Message, MsgBoxStyle.Exclamation, "Error updating list.")
        End Try
    End Sub

    Private Sub PicStripTSMIremoveFromURL_Click(sender As Object, e As EventArgs) Handles PicStripTSMIremoveFromURL.Click
        Try
            ' Lock Control
            PicStripTSMIremoveFromURL.Enabled = False

            ' Remove from URL-Files.
            RemoveFromUrlFiles(ssh.ImageLocation)
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '						       All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Log.WriteError(ex.Message & vbCrLf & ToString(), ex, "Error while deleting URL-From files.")
            MsgBox("An Exception Occured while deleting the URL from Files." & vbCrLf _
                   & ex.Message, MsgBoxStyle.Exclamation, "Delete URL from Files")
        End Try
    End Sub

#Region "-------------------------------------------------- DommeSlideshow ----------------------------------------------------"

    Private Sub PicStripTSMIdommeSlideshowGoToLast_Click(sender As Object, e As EventArgs) Handles PicStripTSMIdommeSlideshowGoToLast.Click

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then
            MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu Is Visible"" option first!", , "Warning!")
            Return
        End If

        If ssh.SlideshowLoaded = False Or ssh.TeaseVideo = True Or ssh.LockImage = True Then Return

        Try
            ShowImage(ssh.SlideshowMain.NavigateLast, True)
            ssh.JustShowedBlogImage = False
        Catch

        End Try
    End Sub

    Private Sub PicStripTSMIdommeSlideshow_GoToFirst_Click(sender As Object, e As EventArgs) Handles PicStripTSMIdommeSlideshow_GoToFirst.Click

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then
            MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu Is Visible"" option first!", , "Warning!")
            Return
        End If

        If ssh.SlideshowLoaded = False Or ssh.TeaseVideo = True Or ssh.LockImage = True Then Return

        Try
            ShowImage(ssh.SlideshowMain.NavigateFirst, True)
            ssh.JustShowedBlogImage = False
        Catch

        End Try
    End Sub

    Private Sub PicStripTSMIdommeSlideshowLoadNewSlideshow_Click(sender As Object, e As EventArgs) Handles PicStripTSMIdommeSlideshowLoadNewSlideshow.Click

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then
            MsgBox("Please close the settings menu or disable ""Pause Program When Settings Menu Is Visible"" option first!", , "Warning!")
            Return
        End If

        If ssh.SlideshowLoaded = False Or ssh.TeaseVideo = True Or ssh.LockImage = True Then Return

        Try
            ssh.SlideshowMain.LoadNew()
            ShowImage(ssh.SlideshowMain.NavigateFirst, True)
        Catch ex As Exception
            Dim i As Int16 = 0
        End Try
    End Sub

#End Region ' DommeSlideshow

#End Region ' PictureStrip

    Public Sub LoadDommeImageFolder()
        ssh.SlideshowMain.LoadNew()
        ShowImage(ssh.SlideshowMain.CurrentImage, False)
        ssh.SlideshowLoaded = True
        ssh.JustShowedBlogImage = False

        ImageSlideShowNextButton.Enabled = True
        ImageSlideShowPreviousButton.Enabled = True
        PicStripTSMIdommeSlideshow.Enabled = True

        If ssh.RiskyDeal = True Then GamesWindow.PBRiskyPic.Image = Image.FromFile(ssh.SlideshowMain.CurrentImage)

        If FrmSettings.TimedSlideShowRadio.Checked = True Then
            ssh.SlideshowTimerTick = FrmSettings.SlideShowNumBox.Value
            SlideshowTimer.Start()
        End If


    End Sub

    ''' <summary>
    ''' Push the chat to the bottom
    ''' </summary>
    Public Sub ScrollChatDown()

        Try
            ChatText.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue)
        Catch
        End Try

        Try
            ChatText2.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue)
        Catch
        End Try

    End Sub

    Private Sub StatusUpdates_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles StatusUpdates.DocumentCompleted
        Try
            StatusUpdates.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue)
        Catch
        End Try
    End Sub

    Public Function StripBlankLines(ByVal SpaceClean As List(Of String)) As List(Of String)
        For i As Integer = SpaceClean.Count - 1 To 0 Step -1
            If SpaceClean(i) = "" Then SpaceClean.Remove(SpaceClean(i))
        Next
        Return SpaceClean
    End Function

    Public Function StripCommands(ByVal CFClean As String) As String

        ' This works as a solution to avoid all the crap I'm having to do underneath it, but I couldn't figuure out how to keep it from eating
        ' words after @CommandFilters in #Keywords

        'Dim CleanReg As RegularExpressions.Regex
        'CleanReg = New RegularExpressions.Regex("(@[^)]+)\)")

        'Dim StripArray As String() = CFClean.Split(")")

        'For i As Integer = 0 To StripArray.Count - 1
        'If StripArray(i).Contains("@") Then
        'StripArray(i) = StripArray(i) & ")"
        'StripArray(i) = StripArray(i).Replace(CleanReg.Match(StripArray(i)).Value(), "")
        'End If
        'Next

        'CFClean = Join(StripArray)


        '===============================================================================
        '							Clean leftover @Commands(
        '===============================================================================
        If CFClean.Contains("@Variable[") Then
            CFClean = CFClean.Replace("@Variable[" & GetParentheses(CFClean, "@Variable[", 2) & "]", "")
            If CFClean.Contains("And[") Then CFClean = CFClean.Replace("And[" & GetParentheses(CFClean, "And[", 2) & "]", "")
            If CFClean.Contains("Or[") Then CFClean = CFClean.Replace("Or[" & GetParentheses(CFClean, "Or[", 2) & "]", "")
        End If

        For Each com As String In New List(Of String) From
                {"@Cup(", Keyword.AllowsOrgasm, Keyword.RuinsOrgasm, "@DommeLevel(",
                Keyword.ApathyLevel, "@Month(", "@Day(", "@Flag(", "@NotFlag("}
            If CFClean.Contains(com) Then CFClean = CFClean.Replace(com & GetParentheses(CFClean, com) & ")", "")
        Next

        '===============================================================================
        '					  Clean all other remaining @Commands
        '===============================================================================
        Dim AtArray() As String = Split(CFClean)
        For i As Integer = 0 To AtArray.Length - 1
            Try
                If AtArray(i).Contains("@") Then
                    AtArray(i) = AtArray(i).Replace(AtArray(i), "")
                End If
            Catch
            End Try
        Next
        CFClean = Join(AtArray)
        Return CFClean

    End Function

    Public Function StripFormat(ByVal FormatClean As String) As String
        FormatClean = FormatClean.Replace("<i>", "")
        FormatClean = FormatClean.Replace("</i>", "")
        FormatClean = FormatClean.Replace("<b>", "")
        FormatClean = FormatClean.Replace("</b>", "")
        FormatClean = FormatClean.Replace("<u>", "")
        FormatClean = FormatClean.Replace("</u>", "")
        FormatClean = FormatClean.Replace(FrmSettings.TBEmote.Text, "")
        FormatClean = FormatClean.Replace(FrmSettings.TBEmoteEnd.Text, "")

        Return FormatClean
    End Function

    Private Sub CustomSlideshowTimer_Tick(sender As Object, e As EventArgs) Handles CustomSlideshowTimer.Tick
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return
        Try
            Dim sw As New Stopwatch
restartInstantly:
            sw.Restart()

            ' Check if the timer is supposed to be running 
            If ssh.CustomSlideEnabled = False Then
                CustomSlideshowTimer.Stop()
                Exit Sub
            End If

            ' Determine if local images are preferred .
            Dim PreferOffline As Boolean = If(CustomSlideshowTimer.Interval < 1000, True, False)

            ' Display a random image.
            ShowImage(ssh.CustomSlideshow.GetRandom(PreferOffline), True)

            ' If displaying the image took longer as the interval, restart instantly.
            If sw.ElapsedMilliseconds > CustomSlideshowTimer.Interval Then GoTo restartInstantly
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Shared Function ResizeImage(ByVal image As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image

        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If

        Dim newImage As Image = New Bitmap(newWidth, newHeight)

        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage

    End Function

#Region "-------------------------------------------------- Contact 1-3 -------------------------------------------------------"



    Private Sub Contact1Timer_Tick(sender As Object, e As EventArgs) Handles Contact1Timer.Tick

        ssh.AddContactTick -= 1

        If ssh.AddContactTick < 1 Then
            Contact1Timer.Stop()
            If Not ssh.Group.Contains("1") Then
                ssh.Group = ssh.Group & "1"
                ssh.GlitterTease = True
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & My.Settings.Glitter1 & " has joined the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            Else
                ssh.Group = ssh.Group.Replace("1", "")
                If ssh.Group = "D" Then ssh.GlitterTease = False
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & My.Settings.Glitter1 & " has left the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            End If
        End If

    End Sub

    Private Sub Contact2Timer_Tick(sender As Object, e As EventArgs) Handles Contact2Timer.Tick

        ssh.AddContactTick -= 1

        If ssh.AddContactTick < 1 Then
            Contact2Timer.Stop()
            If Not ssh.Group.Contains("2") Then
                ssh.Group = ssh.Group & "2"
                ssh.GlitterTease = True
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & My.Settings.Glitter2 & " has joined the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            Else
                ssh.Group = ssh.Group.Replace("2", "")
                If ssh.Group = "D" Then ssh.GlitterTease = False
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & My.Settings.Glitter2 & " has left the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            End If
        End If

    End Sub

    Private Sub Contact3Timer_Tick(sender As Object, e As EventArgs) Handles Contact3Timer.Tick

        ssh.AddContactTick -= 1

        If ssh.AddContactTick < 1 Then
            Contact3Timer.Stop()
            If Not ssh.Group.Contains("3") Then
                ssh.Group = ssh.Group & "3"
                ssh.GlitterTease = True
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & My.Settings.Glitter3 & " has joined the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            Else
                ssh.Group = ssh.Group.Replace("3", "")
                If ssh.Group = "D" Then ssh.GlitterTease = False
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & My.Settings.Glitter3 & " has left the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            End If
        End If

    End Sub

#End Region

    Private Sub DommeTimer_Tick(sender As Object, e As EventArgs) Handles DommeTimer.Tick

        ssh.AddContactTick -= 1

        If ssh.AddContactTick < 1 Then
            DommeTimer.Stop()
            If Not ssh.Group.Contains("D") Then
                ssh.Group = ssh.Group & "D"
                If ssh.Group = "D" Then ssh.GlitterTease = False
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & domName.Text & " has joined the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            Else
                ssh.Group = ssh.Group.Replace("D", "")
                ssh.GlitterTease = True
                'ssh.Chat = "<body style=""word-wrap:break-word;"">" & "<font face=""" & "Cambria" & """ size=""" & "3" & """ color=""#000000"">" & ssh.Chat & "<font color=""SteelBlue""><b>" & domName.Text & " has left the Chat room</b>" & "<br></font></body>"
                'ChatText.DocumentText = ssh.Chat
                'ChatText2.DocumentText = ssh.Chat
            End If
        End If

    End Sub

    Private Sub UpdateStageTimer_Tick(sender As Object, e As EventArgs) Handles UpdateStageTimer.Tick
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return
        ssh.UpdateStageTick -= 1
        If ssh.UpdateStageTick < 1 Then
            UpdateStageTimer.Stop()
            StatusUpdatePost()
        End If
    End Sub


    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd, Me.Resize
        If Me.IsHandleCreated = False Then Exit Sub
        If Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None Then
            Exit Sub
        ElseIf Me.WindowState = FormWindowState.Maximized Then
            My.Settings.WindowHeight = 0
            My.Settings.WindowWidth = 0
        Else
            My.Settings.WindowHeight = Me.Height
            My.Settings.WindowWidth = Me.Width
        End If

    End Sub

    Private Sub TeaseAIClock_Tick(sender As Object, e As EventArgs) Handles TeaseAIClock.Tick
        ' Reset the WatchdogTimer Clock. 
        WatchDogImageAnimator.Reset(TeaseAIClock.Interval * 3)


        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then
            LBLTime.Text = Format(Now, "h:mm")
            LBLAMPM.Text = Format(Now, "tt")
            LBLDate.Text = Format(Now, "Long Date")
            Return
        End If

        If ssh.WritingTaskFlag = False Or (ssh.WritingTaskFlag = True And My.Settings.TimedWriting = False) Then
            LBLTime.Text = Format(Now, "h:mm")
            LBLAMPM.Text = Format(Now, "tt")
            LBLDate.Text = Format(Now, "Long Date")
        Else
            If ssh.WritingTaskCurrentTime > 0 Then
                If My.Settings.TimedWriting = True Then
                    LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times" & vbCrLf & "You have " & ConvertSeconds(ssh.WritingTaskCurrentTime)
                    LBLTime.Text = Convert.ToInt16(ssh.WritingTaskCurrentTime)
                Else
                    LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times"
                End If
            Else
                If My.Settings.TimedWriting = True Then
                    LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times" & vbCrLf & "YOUR TIME IS UP"
                    LBLTime.Text = "Time's Up"
                    'immediately ends the writing task if time is up without waiting for next user input
                    ClearWriteTask()
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = "Failed Writing Task"
                    GetGoto()
                    ssh.ScriptTick = 4
                    ScriptTimer.Start()
                Else
                    LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times"
                End If

            End If
            ssh.WritingTaskCurrentTime -= 1

            LBLAMPM.Text = ""
        End If




        'If WritingTaskFlag = False Then
        'LBLTime.Text = Format(Now, "h:mm")
        'LBLAMPM.Text = Format(Now, "tt")
        'LBLDate.Text = Format(Now, "Long Date")
        'Else
        'If WritingTaskCurrentTime > 0 Then
        'LBLWritingTask.Text = "Write the following line " & WritingTaskLinesAmount & " times" & vbCrLf & "You have " & ConvertSeconds(WritingTaskCurrentTime)
        'LBLTime.Text = Convert.ToInt16(WritingTaskCurrentTime)
        'Else
        'LBLWritingTask.Text = "Write the following line " & WritingTaskLinesAmount & " times" & vbCrLf & "YOUR TIME IS UP"
        'LBLTime.Text = "Time's Up"
        'End If
        'WritingTaskCurrentTime -= 1
        'LBLAMPM.Text = ""
        'End If


        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_WakeUp") Then

            Dim Dates As String
            'Dates = FormatDateTime(Now, DateFormat.ShortDate) & " " & GetTime("SYS_WakeUp")
            Dates = FormatDateTime(Now, DateFormat.ShortDate) & " " & FormatDateTime(FrmSettings.TimeBoxWakeUp.Value, DateFormat.LongTime)

            Dim DDiff As Integer
            DDiff = DateDiff(DateInterval.Hour, CDate(Dates), Now)

            Dim TimeCounter As Integer = -3

            ssh.GeneralTime = "Night"
            If DDiff < -20 Then ssh.GeneralTime = "Morning"
            If DDiff > -2 And DDiff < 5 Then ssh.GeneralTime = "Morning"
            If DDiff > 4 And DDiff < 12 Then ssh.GeneralTime = "Afternoon"
            If DDiff > -21 And DDiff < -11 Then ssh.GeneralTime = "Afternoon"

        End If

        ' #DEBUG

    End Sub

    Public Sub StrokeSpeedCheck()

        If ssh.StrokeFaster = True Then
            If ssh.SubStroking = True And ssh.SubEdging = False And ssh.SubHoldingEdge = False Then
                Dim Stroke123 As Integer = ssh.randomizer.Next(1, 4)
                Stroke123 = Stroke123 * 50
                StrokePace = StrokePace - Stroke123
                If StrokePace < NBMaxPace.Value Then StrokePace = NBMaxPace.Value

            End If
            ssh.StrokeFaster = False
        End If

        If ssh.StrokeSlower = True Then
            If ssh.SubStroking = True And ssh.SubEdging = False And ssh.SubHoldingEdge = False Then
                Dim Stroke123 As Integer = ssh.randomizer.Next(1, 4)
                Stroke123 = Stroke123 * 50
                StrokePace = StrokePace + Stroke123
                If StrokePace > NBMinPace.Value Then StrokePace = NBMinPace.Value

            End If
            ssh.StrokeSlower = False
        End If

        If ssh.StrokeFastest = True Then
            If ssh.SubStroking = True And ssh.SubEdging = False And ssh.SubHoldingEdge = False Then
                StrokePace = NBMaxPace.Value

            End If
            ssh.StrokeFastest = False
        End If

        If ssh.StrokeSlowest = True Then
            If ssh.SubStroking = True And ssh.SubEdging = False And ssh.SubHoldingEdge = False Then
                StrokePace = NBMinPace.Value

            End If
            ssh.StrokeSlowest = False
        End If

    End Sub

    Public Sub ToggleAppVisibility(ByVal appToOpen As Panel)
        If appToOpen Is Nothing AndAlso My.Settings.SideChat Then
            appToOpen = PnlSidechat
        End If

        For Each pnl As Control In PNLTabs.Controls
            pnl.Visible = appToOpen IsNot Nothing AndAlso pnl Is appToOpen
        Next

        PNLTabs.Visible = appToOpen IsNot Nothing
        PnlTabsLayout.Visible = appToOpen IsNot Nothing

        PnlChatBoxLayout.Visible = Not (MaximizeImageToolStripMenuItem.Checked AndAlso SidepanelToolStripMenuItem.Checked AndAlso PnlSidechat.Visible)
    End Sub

#Region "------------------------------------------------------- Apps ---------------------------------------------------------"

#Region "--------------------------------------------------- DommeTag APP -----------------------------------------------------"

#Region "------------------------------------------------- Regular Buttons-----------------------------------------------------"

    Private Sub Face_Click(sender As Object, e As EventArgs) Handles Face.Click
        If ssh.SlideshowLoaded = False Then Return
        If Face.BackColor = Color.White Then
            AddDommeTag("Face", "Nothing")
            Face.BackColor = Color.ForestGreen
            Face.ForeColor = Color.White
        Else
            RemoveDommeTag("Face", "Nothing")
            Face.BackColor = Color.White
            Face.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Boobs_Click(sender As Object, e As EventArgs) Handles Boobs.Click
        If ssh.SlideshowLoaded = False Then Return
        If Boobs.BackColor = Color.White Then
            AddDommeTag("Boobs", "Nothing")
            Boobs.BackColor = Color.ForestGreen
            Boobs.ForeColor = Color.White
        Else
            RemoveDommeTag("Boobs", "Nothing")
            Boobs.BackColor = Color.White
            Boobs.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Pussy_Click(sender As Object, e As EventArgs) Handles Pussy.Click
        If ssh.SlideshowLoaded = False Then Return
        If Pussy.BackColor = Color.White Then
            AddDommeTag("Pussy", "Nothing")
            Pussy.BackColor = Color.ForestGreen
            Pussy.ForeColor = Color.White
        Else
            RemoveDommeTag("Pussy", "Nothing")
            Pussy.BackColor = Color.White
            Pussy.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Ass_Click(sender As Object, e As EventArgs) Handles Ass.Click
        If ssh.SlideshowLoaded = False Then Return
        If Ass.BackColor = Color.White Then
            AddDommeTag("Ass", "Nothing")
            Ass.BackColor = Color.ForestGreen
            Ass.ForeColor = Color.White
        Else
            RemoveDommeTag("Ass", "Nothing")
            Ass.BackColor = Color.White
            Ass.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Legs_Click(sender As Object, e As EventArgs) Handles Legs.Click
        If ssh.SlideshowLoaded = False Then Return
        If Legs.BackColor = Color.White Then
            AddDommeTag("Legs", "Nothing")
            Legs.BackColor = Color.ForestGreen
            Legs.ForeColor = Color.White
        Else
            RemoveDommeTag("Legs", "Nothing")
            Legs.BackColor = Color.White
            Legs.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Feet_Click(sender As Object, e As EventArgs) Handles Feet.Click
        If ssh.SlideshowLoaded = False Then Return
        If Feet.BackColor = Color.White Then
            AddDommeTag("Feet", "Nothing")
            Feet.BackColor = Color.ForestGreen
            Feet.ForeColor = Color.White
        Else
            RemoveDommeTag("Feet", "Nothing")
            Feet.BackColor = Color.White
            Feet.ForeColor = Color.Black
        End If
    End Sub

    Private Sub FullyDressed_Click(sender As Object, e As EventArgs) Handles FullyDressed.Click
        If ssh.SlideshowLoaded = False Then Return
        If FullyDressed.BackColor = Color.White Then
            AddDommeTag("FullyDressed", "Nothing")
            FullyDressed.BackColor = Color.ForestGreen
            FullyDressed.ForeColor = Color.White
        Else
            RemoveDommeTag("FullyDressed", "Nothing")
            FullyDressed.BackColor = Color.White
            FullyDressed.ForeColor = Color.Black
        End If
    End Sub

    Private Sub HalfDressed_Click(sender As Object, e As EventArgs) Handles HalfDressed.Click
        If ssh.SlideshowLoaded = False Then Return
        If HalfDressed.BackColor = Color.White Then
            AddDommeTag("HalfDressed", "Nothing")
            HalfDressed.BackColor = Color.ForestGreen
            HalfDressed.ForeColor = Color.White
        Else
            RemoveDommeTag("HalfDressed", "Nothing")
            HalfDressed.BackColor = Color.White
            HalfDressed.ForeColor = Color.Black
        End If
    End Sub

    Private Sub GarmentCovering_Click(sender As Object, e As EventArgs) Handles GarmentCovering.Click
        If ssh.SlideshowLoaded = False Then Return
        If GarmentCovering.BackColor = Color.White Then
            AddDommeTag("GarmentCovering", "Nothing")
            GarmentCovering.BackColor = Color.ForestGreen
            GarmentCovering.ForeColor = Color.White
        Else
            RemoveDommeTag("GarmentCovering", "Nothing")
            GarmentCovering.BackColor = Color.White
            GarmentCovering.ForeColor = Color.Black
        End If
    End Sub

    Private Sub HandsCovering_Click(sender As Object, e As EventArgs) Handles HandsCovering.Click
        If ssh.SlideshowLoaded = False Then Return
        If HandsCovering.BackColor = Color.White Then
            AddDommeTag("HandsCovering", "Nothing")
            HandsCovering.BackColor = Color.ForestGreen
            HandsCovering.ForeColor = Color.White
        Else
            RemoveDommeTag("HandsCovering", "Nothing")
            HandsCovering.BackColor = Color.White
            HandsCovering.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Naked_Click(sender As Object, e As EventArgs) Handles Naked.Click
        If ssh.SlideshowLoaded = False Then Return
        If Naked.BackColor = Color.White Then
            AddDommeTag("Naked", "Nothing")
            Naked.BackColor = Color.ForestGreen
            Naked.ForeColor = Color.White
        Else
            RemoveDommeTag("Naked", "Nothing")
            Naked.BackColor = Color.White
            Naked.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Masturbating_Click(sender As Object, e As EventArgs) Handles Masturbating.Click
        If ssh.SlideshowLoaded = False Then Return
        If Masturbating.BackColor = Color.White Then
            AddDommeTag("Masturbating", "Nothing")
            Masturbating.BackColor = Color.ForestGreen
            Masturbating.ForeColor = Color.White
        Else
            RemoveDommeTag("Masturbating", "Nothing")
            Masturbating.BackColor = Color.White
            Masturbating.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Sucking_Click(sender As Object, e As EventArgs) Handles Sucking.Click
        If ssh.SlideshowLoaded = False Then Return
        If Sucking.BackColor = Color.White Then
            AddDommeTag("Sucking", "Nothing")
            Sucking.BackColor = Color.ForestGreen
            Sucking.ForeColor = Color.White
        Else
            RemoveDommeTag("Sucking", "Nothing")
            Sucking.BackColor = Color.White
            Sucking.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Smiling_Click(sender As Object, e As EventArgs) Handles Smiling.Click
        If ssh.SlideshowLoaded = False Then Return
        If Smiling.BackColor = Color.White Then
            AddDommeTag("Smiling", "Nothing")
            Smiling.BackColor = Color.ForestGreen
            Smiling.ForeColor = Color.White
        Else
            RemoveDommeTag("Smiling", "Nothing")
            Smiling.BackColor = Color.White
            Smiling.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Glaring_Click(sender As Object, e As EventArgs) Handles Glaring.Click
        If ssh.SlideshowLoaded = False Then Return
        If Glaring.BackColor = Color.White Then
            AddDommeTag("Glaring", "Nothing")
            Glaring.BackColor = Color.ForestGreen
            Glaring.ForeColor = Color.White
        Else
            RemoveDommeTag("Glaring", "Nothing")
            Glaring.BackColor = Color.White
            Glaring.ForeColor = Color.Black
        End If
    End Sub

    Private Sub SideView_Click(sender As Object, e As EventArgs) Handles SideView.Click
        If ssh.SlideshowLoaded = False Then Return
        If SideView.BackColor = Color.White Then
            AddDommeTag("SideView", "Nothing")
            SideView.BackColor = Color.ForestGreen
            SideView.ForeColor = Color.White
        Else
            RemoveDommeTag("SideView", "Nothing")
            SideView.BackColor = Color.White
            SideView.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CloseUp_Click(sender As Object, e As EventArgs) Handles CloseUp.Click
        If ssh.SlideshowLoaded = False Then Return
        If CloseUp.BackColor = Color.White Then
            AddDommeTag("CloseUp", "Nothing")
            CloseUp.BackColor = Color.ForestGreen
            CloseUp.ForeColor = Color.White
        Else
            RemoveDommeTag("CloseUp", "Nothing")
            CloseUp.BackColor = Color.White
            CloseUp.ForeColor = Color.Black
        End If
    End Sub

    Private Sub SeeThrough_Click(sender As Object, e As EventArgs) Handles SeeThrough.Click
        If ssh.SlideshowLoaded = False Then Return
        If SeeThrough.BackColor = Color.White Then
            AddDommeTag("SeeThrough", "Nothing")
            SeeThrough.BackColor = Color.ForestGreen
            SeeThrough.ForeColor = Color.White
        Else
            RemoveDommeTag("SeeThrough", "Nothing")
            SeeThrough.BackColor = Color.White
            SeeThrough.ForeColor = Color.Black
        End If
    End Sub

    Private Sub AllFours_Click(sender As Object, e As EventArgs) Handles AllFours.Click
        If ssh.SlideshowLoaded = False Then Return
        If AllFours.BackColor = Color.White Then
            AddDommeTag("AllFours", "Nothing")
            AllFours.BackColor = Color.ForestGreen
            AllFours.ForeColor = Color.White
        Else
            RemoveDommeTag("AllFours", "Nothing")
            AllFours.BackColor = Color.White
            AllFours.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Piercing_Click(sender As Object, e As EventArgs) Handles Piercing.Click
        If ssh.SlideshowLoaded = False Then Return
        If Piercing.BackColor = Color.White Then
            AddDommeTag("Piercing", "Nothing")
            Piercing.BackColor = Color.ForestGreen
            Piercing.ForeColor = Color.White
        Else
            RemoveDommeTag("Piercing", "Nothing")
            Piercing.BackColor = Color.White
            Piercing.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TBGarment_TextChanged(sender As Object, e As EventArgs) Handles TBGarment.TextChanged
        If ssh.SlideshowLoaded = False Or TBGarment.Focused = False Then Return
        If TBGarment.Text = "" Then
            Garment.BackColor = Color.White
            Garment.ForeColor = Color.Black
            RemoveDommeTag("Garment", "Nothing")
        Else
            Garment.BackColor = Color.ForestGreen
            Garment.ForeColor = Color.White
            AddDommeTag("Garment", TBGarment.Text)
        End If
    End Sub

    Private Sub TBUnderwear_TextChanged(sender As Object, e As EventArgs) Handles TBUnderwear.TextChanged
        If ssh.SlideshowLoaded = False Or TBUnderwear.Focused = False Then Return
        If TBUnderwear.Text = "" Then
            Underwear.BackColor = Color.White
            Underwear.ForeColor = Color.Black
            RemoveDommeTag("Underwear", "Nothing")
        Else
            Underwear.BackColor = Color.ForestGreen
            Underwear.ForeColor = Color.White
            AddDommeTag("Underwear", TBUnderwear.Text)
        End If
    End Sub

    Private Sub TBTattoo_TextChanged(sender As Object, e As EventArgs) Handles TBTattoo.TextChanged
        If ssh.SlideshowLoaded = False Or TBTattoo.Focused = False Then Return
        If TBTattoo.Text = "" Then
            Tattoo.BackColor = Color.White
            Tattoo.ForeColor = Color.Black
            RemoveDommeTag("Tattoo", "Nothing")
        Else
            Tattoo.BackColor = Color.ForestGreen
            Tattoo.ForeColor = Color.White
            AddDommeTag("Tattoo", TBTattoo.Text)
        End If
    End Sub

    Private Sub TBSexToy_TextChanged(sender As Object, e As EventArgs) Handles TBSexToy.TextChanged
        If ssh.SlideshowLoaded = False Or TBSexToy.Focused = False Then Return
        If TBSexToy.Text = "" Then
            SexToy.BackColor = Color.White
            SexToy.ForeColor = Color.Black
            RemoveDommeTag("SexToy", "Nothing")
        Else
            SexToy.BackColor = Color.ForestGreen
            SexToy.ForeColor = Color.White
            AddDommeTag("SexToy", TBSexToy.Text)
        End If
    End Sub


    Private Sub TBFurniture_TextChanged(sender As Object, e As EventArgs) Handles TBFurniture.TextChanged
        If ssh.SlideshowLoaded = False Or TBFurniture.Focused = False Then Return
        If TBFurniture.Text = "" Then
            Furniture.BackColor = Color.White
            Furniture.ForeColor = Color.Black
            RemoveDommeTag("Furniture", "Nothing")
        Else
            Furniture.BackColor = Color.ForestGreen
            Furniture.ForeColor = Color.White
            AddDommeTag("Furniture", TBFurniture.Text)
        End If
    End Sub

#End Region ' Regular Buttons


    Public Function AddDommeTag(ByVal AddDomTag As String, ByVal AddCustomDomTag As String)
        Dim DomTag As String = "Tag" & AddDomTag
        Dim Custom As String
        If AddCustomDomTag = "Nothing" Then
            Custom = ""
        Else
            Custom = AddCustomDomTag
        End If

        'Dim TagFile As String = Path.GetDirectoryName(_ImageFileNames(FileCount)) & "\ImageTags.txt"
        Dim TagFile As String = Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt"

        If File.Exists(TagFile) Then

            Dim TagList As New List(Of String)
            TagList = Txt2List(TagFile)

            Dim FoundFile As Boolean = False

            For i As Integer = 0 To TagList.Count - 1
                If TagList(i).Contains(Path.GetFileName(ssh.ImageLocation)) Then
                    FoundFile = True
                    If Not TagList(i).Contains(DomTag) Then
                        TagList(i) = TagList(i) & " " & DomTag & Custom

                    Else

                        If DomTag = "TagGarment" Or DomTag = "TagUnderwear" Or DomTag = "TagTattoo" Or DomTag = "TagSexToy" Or DomTag = "TagFurniture" Then

                            Dim CustomArray As String() = TagList(i).Split

                            For x As Integer = 0 To CustomArray.Count - 1
                                If CustomArray(x).Contains(DomTag) Then
                                    If DomTag = "TagGarment" And CustomArray(x).Contains("TagGarment") And Not CustomArray(x).Contains("TagGarmentCovering") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagUnderwear" And CustomArray(x).Contains("TagUnderwear") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagTattoo" And CustomArray(x).Contains("TagTattoo") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagSexToy" And CustomArray(x).Contains("TagSexToy") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagFurniture" And CustomArray(x).Contains("TagFurniture") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                End If
                            Next

                            TagList(i) = TagList(i) & " " & DomTag & Custom
                            TagList(i) = TagList(i).Replace("  ", " ")

                        End If




                    End If
                End If
            Next

            If FoundFile = False Then TagList.Add(Path.GetFileName(ssh.ImageLocation) & " " & DomTag & Custom)

            If TagList.Count > 0 Then
                Dim SettingsString As String = ""
                For i As Integer = 0 To TagList.Count - 1
                    SettingsString = SettingsString & TagList(i)
                    If i <> TagList.Count - 1 Then SettingsString = SettingsString & Environment.NewLine
                Next
                My.Computer.FileSystem.WriteAllText(Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt", SettingsString, False)
            End If

        ElseIf Path.GetDirectoryName(TagFile) = Path.GetDirectoryName(ssh.ImageLocation) Then
            ' Only Create new file for the loaded Slidshow. This Prevents URL-Image-Tagging.
            My.Computer.FileSystem.WriteAllText(Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt", Path.GetFileName(ssh.ImageLocation) & " " & DomTag & Custom, True)

        End If

    End Function

    Public Function RemoveDommeTag(ByVal RemoveDomTag As String, ByVal RemoveCustomDomTag As String)
        Dim DomTag As String = "Tag" & RemoveDomTag
        Dim Custom As String
        If RemoveCustomDomTag = "Nothing" Then
            Custom = ""
        Else
            Custom = RemoveCustomDomTag
        End If

        Dim SettingsString As String
        'Dim TagFile As String = Path.GetDirectoryName(_ImageFileNames(FileCount)) & "\ImageTags.txt"
        Dim TagFile As String = Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt"

        If File.Exists(TagFile) Then

            Dim TagList As New List(Of String)
            TagList = Txt2List(TagFile)

            For i As Integer = TagList.Count - 1 To 0 Step -1
                If TagList(i).Contains(Path.GetFileName(ssh.ImageLocation)) Then
                    If TagList(i).Contains(DomTag & Custom) Then

                        If DomTag = "TagGarment" Or DomTag = "TagUnderwear" Or DomTag = "TagTattoo" Or DomTag = "TagSexToy" Or DomTag = "TagFurniture" Then

                            Dim CustomArray As String() = TagList(i).Split

                            For x As Integer = 0 To CustomArray.Count - 1
                                If CustomArray(x).Contains(DomTag) Then
                                    If DomTag = "TagGarment" And CustomArray(x).Contains("TagGarment") And Not CustomArray(x).Contains("TagGarmentCovering") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagUnderwear" And CustomArray(x).Contains("TagUnderwear") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagTattoo" And CustomArray(x).Contains("TagTattoo") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagSexToy" And CustomArray(x).Contains("TagSexToy") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                    If DomTag = "TagFurniture" And CustomArray(x).Contains("TagFurniture") Then TagList(i) = TagList(i).Replace(CustomArray(x), "")
                                End If
                            Next
                        Else

                            TagList(i) = TagList(i).Replace(" " & DomTag & Custom, "")
                        End If


                        If Not TagList(i).Contains(" Tag") Then TagList.Remove(TagList(i))
                    End If
                End If
            Next

            If TagList.Count > 0 Then
                SettingsString = ""
                For i As Integer = 0 To TagList.Count - 1
                    SettingsString = SettingsString & TagList(i)
                    If i <> TagList.Count - 1 Then SettingsString = SettingsString & Environment.NewLine
                Next
                My.Computer.FileSystem.WriteAllText(Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt", SettingsString, False)
            Else
                If File.Exists(Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt") Then My.Computer.FileSystem.DeleteFile(Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt")
            End If

        End If




    End Function

    Private Sub BtnDommeTagNextImage_Click(sender As Object, e As EventArgs) Handles DommeTagBtnNextImage.Click
        ImageSlideShowNextButton.PerformClick()
    End Sub

    Private Sub BtnDommeTagLastImage_Click(sender As Object, e As EventArgs) Handles DommeTagBtnLastImage.Click
        ImageSlideShowPreviousButton.PerformClick()
    End Sub

    ''' <summary>
    ''' <para>In Order to Work, this function has to be called AFTER an Image has been loaded into the 
    ''' <see cref="MainWindow.mainPictureBox">PictureBox</see>. Everthing else doesn't work properly.</para>
    ''' <para>Right now there are only two working non-UI-Freezing posibilities: The Imagebox 
    ''' LoadCompleted-Event and PBImageThread. In PBImageThread an Invoke is required!</para>
    ''' This Function uses the <see cref="MainWindow.ssh.ImageLocation">ImageLocation</see> Variable to get the
    ''' current ImageFilePath. 
    ''' </summary>
    ''' <remarks>
    ''' To Raise the PictureBoxCompleted-Event, you have to load an Image via LoadAsync(URL).
    ''' </remarks>
    Public Sub CheckDommeTags()

        Face.BackColor = Color.White
        Face.ForeColor = Color.Black

        Boobs.BackColor = Color.White
        Boobs.ForeColor = Color.Black

        Pussy.BackColor = Color.White
        Pussy.ForeColor = Color.Black

        Ass.BackColor = Color.White
        Ass.ForeColor = Color.Black

        Legs.BackColor = Color.White
        Legs.ForeColor = Color.Black

        Feet.BackColor = Color.White
        Feet.ForeColor = Color.Black

        FullyDressed.BackColor = Color.White
        FullyDressed.ForeColor = Color.Black

        HalfDressed.BackColor = Color.White
        HalfDressed.ForeColor = Color.Black

        GarmentCovering.BackColor = Color.White
        GarmentCovering.ForeColor = Color.Black

        HandsCovering.BackColor = Color.White
        HandsCovering.ForeColor = Color.Black

        Naked.BackColor = Color.White
        Naked.ForeColor = Color.Black

        Masturbating.BackColor = Color.White
        Masturbating.ForeColor = Color.Black

        Sucking.BackColor = Color.White
        Sucking.ForeColor = Color.Black

        Smiling.BackColor = Color.White
        Smiling.ForeColor = Color.Black

        Glaring.BackColor = Color.White
        Glaring.ForeColor = Color.Black

        SideView.BackColor = Color.White
        SideView.ForeColor = Color.Black

        CloseUp.BackColor = Color.White
        CloseUp.ForeColor = Color.Black

        SeeThrough.BackColor = Color.White
        SeeThrough.ForeColor = Color.Black

        AllFours.BackColor = Color.White
        AllFours.ForeColor = Color.Black

        Piercing.BackColor = Color.White
        Piercing.ForeColor = Color.Black

        Garment.BackColor = Color.White
        Garment.ForeColor = Color.Black

        Underwear.BackColor = Color.White
        Underwear.ForeColor = Color.Black

        Tattoo.BackColor = Color.White
        Tattoo.ForeColor = Color.Black

        SexToy.BackColor = Color.White
        SexToy.ForeColor = Color.Black

        Furniture.BackColor = Color.White
        Furniture.ForeColor = Color.Black

        TBGarment.Text = ""
        TBUnderwear.Text = ""
        TBTattoo.Text = ""
        TBSexToy.Text = ""
        TBFurniture.Text = ""

        If ssh.ImageLocation = "" Then Exit Sub

        Dim tmpFileName As String = Path.GetFileName(ssh.ImageLocation)

        Dim TagFile As String = Path.GetDirectoryName(ssh.ImageLocation) & "\ImageTags.txt"

        If File.Exists(TagFile) Then

            Dim TagList As New List(Of String)
            TagList = Txt2List(TagFile)

            For i As Integer = TagList.Count - 1 To 0 Step -1

                If TagList(i).Contains(tmpFileName) Then
                    If TagList(i).Contains("TagFace") Then
                        Face.BackColor = Color.ForestGreen
                        Face.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagBoobs") Then
                        Boobs.BackColor = Color.ForestGreen
                        Boobs.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagPussy") Then
                        Pussy.BackColor = Color.ForestGreen
                        Pussy.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagAss") Then
                        Ass.BackColor = Color.ForestGreen
                        Ass.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagLegs") Then
                        Legs.BackColor = Color.ForestGreen
                        Legs.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagFeet") Then
                        Feet.BackColor = Color.ForestGreen
                        Feet.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagFullyDressed") Then
                        FullyDressed.BackColor = Color.ForestGreen
                        FullyDressed.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagHalfDressed") Then
                        HalfDressed.BackColor = Color.ForestGreen
                        HalfDressed.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagGarmentCovering") Then
                        GarmentCovering.BackColor = Color.ForestGreen
                        GarmentCovering.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagHandsCovering") Then
                        HandsCovering.BackColor = Color.ForestGreen
                        HandsCovering.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagNaked") Then
                        Naked.BackColor = Color.ForestGreen
                        Naked.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagMasturbating") Then
                        Masturbating.BackColor = Color.ForestGreen
                        Masturbating.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagSucking") Then
                        Sucking.BackColor = Color.ForestGreen
                        Sucking.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagSmiling") Then
                        Smiling.BackColor = Color.ForestGreen
                        Smiling.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagGlaring") Then
                        Glaring.BackColor = Color.ForestGreen
                        Glaring.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagSideView") Then
                        SideView.BackColor = Color.ForestGreen
                        SideView.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagSeeThrough") Then
                        SeeThrough.BackColor = Color.ForestGreen
                        SeeThrough.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagAllFours") Then
                        AllFours.BackColor = Color.ForestGreen
                        AllFours.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagCloseUp") Then
                        CloseUp.BackColor = Color.ForestGreen
                        CloseUp.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagPiercing") Then
                        Piercing.BackColor = Color.ForestGreen
                        Piercing.ForeColor = Color.White
                    End If

                    If TagList(i).Contains("TagGarment") Then
                        Dim GarmentArray As String() = TagList(i).Split
                        For x As Integer = 0 To GarmentArray.Count - 1
                            If GarmentArray(x).Contains("TagGarment") And Not GarmentArray(x).Contains("TagGarmentCovering") Then
                                Garment.BackColor = Color.ForestGreen
                                Garment.ForeColor = Color.White
                                TBGarment.Text = GarmentArray(x).Replace("TagGarment", "")
                            End If
                        Next

                    End If

                    If TagList(i).Contains("TagUnderwear") Then

                        Dim UnderwearArray As String() = TagList(i).Split
                        For x As Integer = 0 To UnderwearArray.Count - 1
                            If UnderwearArray(x).Contains("TagUnderwear") Then
                                Underwear.BackColor = Color.ForestGreen
                                Underwear.ForeColor = Color.White
                                TBUnderwear.Text = UnderwearArray(x).Replace("TagUnderwear", "")
                            End If
                        Next

                    End If

                    If TagList(i).Contains("TagTattoo") Then
                        Dim TattooArray As String() = TagList(i).Split
                        For x As Integer = 0 To TattooArray.Count - 1
                            If TattooArray(x).Contains("TagTattoo") Then
                                Tattoo.BackColor = Color.ForestGreen
                                Tattoo.ForeColor = Color.White
                                TBTattoo.Text = TattooArray(x).Replace("TagTattoo", "")
                            End If
                        Next

                    End If

                    If TagList(i).Contains("TagSexToy") Then
                        Dim SexToyArray As String() = TagList(i).Split
                        For x As Integer = 0 To SexToyArray.Count - 1
                            If SexToyArray(x).Contains("TagSexToy") Then
                                SexToy.BackColor = Color.ForestGreen
                                SexToy.ForeColor = Color.White
                                TBSexToy.Text = SexToyArray(x).Replace("TagSexToy", "")
                            End If
                        Next

                    End If

                    If TagList(i).Contains("TagFurniture") Then
                        Dim FurnitureArray As String() = TagList(i).Split
                        For x As Integer = 0 To FurnitureArray.Count - 1
                            If FurnitureArray(x).Contains("TagFurniture") Then
                                Furniture.BackColor = Color.ForestGreen
                                Furniture.ForeColor = Color.White
                                TBFurniture.Text = FurnitureArray(x).Replace("TagFurniture", "")
                            End If
                        Next
                    End If




                End If

            Next

        End If
    End Sub


#End Region ' DommeTag APP

#Region "------------------------------------------------------ Lazy-Sub ------------------------------------------------------"

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles BTNStop.Click, Button7.Click
        chatBox.Text = "Let me stop"
        SendButton.PerformClick()
    End Sub

    Private Sub BTNYes_Click(sender As Object, e As EventArgs) Handles BTNYes.Click, Button2.Click
        Try
            chatBox.Text = "Yes " & FrmSettings.TBHonorific.Text
        Catch
            chatBox.Text = "Yes"
        End Try

        SendButton.PerformClick()
    End Sub

    Private Sub BTNNo_Click(sender As Object, e As EventArgs) Handles BTNNo.Click, Button3.Click
        Try
            chatBox.Text = "No " & FrmSettings.TBHonorific.Text
        Catch
            chatBox.Text = "No"
        End Try

        SendButton.PerformClick()
    End Sub

    Private Sub BTNEdge_Click(sender As Object, e As EventArgs) Handles BTNEdge.Click, Button4.Click
        chatBox.Text = "On the edge"
        SendButton.PerformClick()
    End Sub

    Private Sub BTNSpeedUp_Click(sender As Object, e As EventArgs) Handles BTNSpeedUp.Click, Button8.Click
        chatBox.Text = "Let me speed up"
        SendButton.PerformClick()
    End Sub

    Private Sub BTNSlowDown_Click(sender As Object, e As EventArgs) Handles BTNSlowDown.Click, Button5.Click
        chatBox.Text = "Let me slow down"
        SendButton.PerformClick()
    End Sub

    Private Sub BTNStroke_Click(sender As Object, e As EventArgs) Handles BTNStroke.Click, Button6.Click
        chatBox.Text = "May I start stroking?"
        SendButton.PerformClick()
    End Sub

    Private Sub BTNAskToCum_Click(sender As Object, e As EventArgs) Handles BTNAskToCum.Click, Button9.Click
        chatBox.Text = "Please let me cum!"
        SendButton.PerformClick()
    End Sub

    Private Sub BTNGreeting_Click(sender As Object, e As EventArgs) Handles BTNGreeting.Click, Button10.Click

        If mySession.Session.Domme.WasGreeted = True Then
            ssh.LockImage = False
            If ssh.SlideshowLoaded = True Then
                ImageSlideShowNextButton.Enabled = True
                ImageSlideShowPreviousButton.Enabled = True
                PicStripTSMIdommeSlideshow.Enabled = True
            End If
            ssh.RapidFire = False
            Return
        End If

        Try
            chatBox.Text = "Hello " & FrmSettings.TBHonorific.Text
        Catch
            chatBox.Text = "Hello"
        End Try

        SendButton.PerformClick()
    End Sub

    Private Sub BTNSafeword_Click(sender As Object, e As EventArgs) Handles BTNSafeword.Click, Button11.Click
        Try
            chatBox.Text = FrmSettings.TBSafeword.Text
        Catch
            chatBox.Text = "@Error"
        End Try

        SendButton.PerformClick()
    End Sub

    Private Sub CBHideShortcuts_CheckedChanged(sender As Object, e As EventArgs) Handles CBHideShortcuts.CheckedChanged
        If FormLoading = False Then
            SetShortcutsVisible()
            My.Settings.ShowShortcuts = CBHideShortcuts.Checked
        End If
    End Sub

    Public Sub SetShortcutsVisible()
        Dim isHidden As Boolean = CBHideShortcuts.Checked
        TBShortYes.Visible = Not isHidden
        TBShortNo.Visible = Not isHidden
        TBShortEdge.Visible = Not isHidden
        TBShortSpeedUp.Visible = Not isHidden
        TBShortSlowDown.Visible = Not isHidden
        TBShortStop.Visible = Not isHidden
        TBShortStroke.Visible = Not isHidden
        TBShortCum.Visible = Not isHidden
        TBShortGreet.Visible = Not isHidden
        TBShortSafeword.Visible = Not isHidden
        BTNLS1Edit.Visible = Not isHidden
        BTNLS2Edit.Visible = Not isHidden
        BTNLS3Edit.Visible = Not isHidden
        BTNLS4Edit.Visible = Not isHidden
        BTNLS5Edit.Visible = Not isHidden

        Dim width As Integer = 163
        If isHidden Then width = 214
        BTNLS1.Width = width
        BTNLS2.Width = width
        BTNLS3.Width = width
        BTNLS4.Width = width
        BTNLS5.Width = width
    End Sub

    Private Sub CBShortcuts_CheckedChanged(sender As Object, e As EventArgs) Handles CBShortcuts.CheckedChanged
        If FormLoading = False Then
            My.Settings.Shortcuts = CBShortcuts.Checked
        End If
    End Sub

    Private Sub TBShortYes_LostFocus(sender As Object, e As EventArgs) Handles TBShortYes.LostFocus
        My.Settings.ShortYes = TBShortYes.Text
    End Sub

    Private Sub TBShortNo_LostFocus(sender As Object, e As EventArgs) Handles TBShortNo.LostFocus
        My.Settings.ShortNo = TBShortNo.Text
    End Sub

    Private Sub TBShortEdge_LostFocus(sender As Object, e As EventArgs) Handles TBShortEdge.LostFocus
        My.Settings.ShortEdge = TBShortEdge.Text
    End Sub

    Private Sub TBShortSpeedUp_LostFocus(sender As Object, e As EventArgs) Handles TBShortSpeedUp.LostFocus
        My.Settings.ShortSpeedUp = TBShortSpeedUp.Text
    End Sub

    Private Sub TBShortSlowDown_LostFocus(sender As Object, e As EventArgs) Handles TBShortSlowDown.LostFocus
        My.Settings.ShortSlowDown = TBShortSlowDown.Text
    End Sub

    Private Sub TBShortStop_LostFocus(sender As Object, e As EventArgs) Handles TBShortStop.LostFocus
        My.Settings.ShortStop = TBShortStop.Text
    End Sub

    Private Sub TBShortStroke_LostFocus(sender As Object, e As EventArgs) Handles TBShortStroke.LostFocus
        My.Settings.ShortStroke = TBShortStroke.Text
    End Sub

    Private Sub TBShortCum_LostFocus(sender As Object, e As EventArgs) Handles TBShortCum.LostFocus
        My.Settings.ShortCum = TBShortCum.Text
    End Sub

    Private Sub TBShortGreet_LostFocus(sender As Object, e As EventArgs) Handles TBShortGreet.LostFocus
        My.Settings.ShortGreet = TBShortGreet.Text
    End Sub

    Private Sub TBShortSafeword_LostFocus(sender As Object, e As EventArgs) Handles TBShortSafeword.LostFocus
        My.Settings.ShortSafeword = TBShortSafeword.Text
    End Sub

    Public Sub CheatCheck()

        If chatBox.Text = LBLWritingTaskText.Text Then
            chatBox.Text = "I'm a dirty cheater"
        End If

    End Sub

    Private Sub BTNLS1_Click(sender As Object, e As EventArgs) Handles BTNLS1.Click


        If BTNLS1.Text <> "" Then
            chatBox.Text = BTNLS1.Text
            If ssh.WritingTaskFlag = True Then CheatCheck()
            SendButton.PerformClick()
        End If

    End Sub

    Private Sub BTNLS1Edit_Click(sender As Object, e As EventArgs) Handles BTNLS1Edit.Click


        LazyEdit2 = False
        LazyEdit3 = False
        LazyEdit4 = False
        LazyEdit5 = False

        BTNLS2Edit.BackColor = My.Settings.ButtonColor
        BTNLS2Edit.ForeColor = My.Settings.TextColor
        BTNLS3Edit.BackColor = My.Settings.ButtonColor
        BTNLS3Edit.ForeColor = My.Settings.TextColor
        BTNLS4Edit.BackColor = My.Settings.ButtonColor
        BTNLS4Edit.ForeColor = My.Settings.TextColor
        BTNLS5Edit.BackColor = My.Settings.ButtonColor
        BTNLS5Edit.ForeColor = My.Settings.TextColor


        If LazyEdit1 = False Then
            BTNLS1Edit.BackColor = Color.ForestGreen
            BTNLS1Edit.ForeColor = Color.White
            LazyEdit1 = True
        Else
            BTNLS1Edit.BackColor = My.Settings.ButtonColor
            BTNLS1Edit.ForeColor = My.Settings.TextColor
            LazyEdit1 = False
        End If

    End Sub

    Private Sub BTNLS2_Click(sender As Object, e As EventArgs) Handles BTNLS2.Click


        If BTNLS2.Text <> "" Then
            chatBox.Text = BTNLS2.Text
            If ssh.WritingTaskFlag = True Then CheatCheck()
            SendButton.PerformClick()
        End If

    End Sub

    Private Sub BTNLS2Edit_Click(sender As Object, e As EventArgs) Handles BTNLS2Edit.Click


        LazyEdit1 = False
        LazyEdit3 = False
        LazyEdit4 = False
        LazyEdit5 = False

        BTNLS1Edit.BackColor = My.Settings.ButtonColor
        BTNLS1Edit.ForeColor = My.Settings.TextColor
        BTNLS3Edit.BackColor = My.Settings.ButtonColor
        BTNLS3Edit.ForeColor = My.Settings.TextColor
        BTNLS4Edit.BackColor = My.Settings.ButtonColor
        BTNLS4Edit.ForeColor = My.Settings.TextColor
        BTNLS5Edit.BackColor = My.Settings.ButtonColor
        BTNLS5Edit.ForeColor = My.Settings.TextColor

        If LazyEdit2 = False Then
            BTNLS2Edit.BackColor = Color.ForestGreen
            BTNLS2Edit.ForeColor = Color.White
            LazyEdit2 = True
        Else
            BTNLS2Edit.BackColor = My.Settings.ButtonColor
            BTNLS2Edit.ForeColor = My.Settings.TextColor
            LazyEdit2 = False
        End If

    End Sub

    Private Sub BTNLS3_Click(sender As Object, e As EventArgs) Handles BTNLS3.Click


        If BTNLS3.Text <> "" Then
            chatBox.Text = BTNLS3.Text
            If ssh.WritingTaskFlag = True Then CheatCheck()
            SendButton.PerformClick()
        End If

    End Sub

    Private Sub BTNLS3Edit_Click(sender As Object, e As EventArgs) Handles BTNLS3Edit.Click


        LazyEdit2 = False
        LazyEdit1 = False
        LazyEdit4 = False
        LazyEdit5 = False

        BTNLS2Edit.BackColor = My.Settings.ButtonColor
        BTNLS2Edit.ForeColor = My.Settings.TextColor
        BTNLS1Edit.BackColor = My.Settings.ButtonColor
        BTNLS1Edit.ForeColor = My.Settings.TextColor
        BTNLS4Edit.BackColor = My.Settings.ButtonColor
        BTNLS4Edit.ForeColor = My.Settings.TextColor
        BTNLS5Edit.BackColor = My.Settings.ButtonColor
        BTNLS5Edit.ForeColor = My.Settings.TextColor


        If LazyEdit3 = False Then
            BTNLS3Edit.BackColor = Color.ForestGreen
            BTNLS3Edit.ForeColor = Color.White
            LazyEdit3 = True
        Else
            BTNLS3Edit.BackColor = My.Settings.ButtonColor
            BTNLS3Edit.ForeColor = My.Settings.TextColor
            LazyEdit3 = False
        End If

    End Sub

    Private Sub BTNLS4_Click(sender As Object, e As EventArgs) Handles BTNLS4.Click


        If BTNLS4.Text <> "" Then
            chatBox.Text = BTNLS4.Text
            If ssh.WritingTaskFlag = True Then CheatCheck()
            SendButton.PerformClick()
        End If

    End Sub

    Private Sub BTNLS4Edit_Click(sender As Object, e As EventArgs) Handles BTNLS4Edit.Click


        LazyEdit2 = False
        LazyEdit3 = False
        LazyEdit1 = False
        LazyEdit5 = False

        BTNLS2Edit.BackColor = My.Settings.ButtonColor
        BTNLS2Edit.ForeColor = My.Settings.TextColor
        BTNLS3Edit.BackColor = My.Settings.ButtonColor
        BTNLS3Edit.ForeColor = My.Settings.TextColor
        BTNLS1Edit.BackColor = My.Settings.ButtonColor
        BTNLS1Edit.ForeColor = My.Settings.TextColor
        BTNLS5Edit.BackColor = My.Settings.ButtonColor
        BTNLS5Edit.ForeColor = My.Settings.TextColor


        If LazyEdit4 = False Then
            BTNLS4Edit.BackColor = Color.ForestGreen
            BTNLS4Edit.ForeColor = Color.White
            LazyEdit4 = True
        Else
            BTNLS4Edit.BackColor = My.Settings.ButtonColor
            BTNLS4Edit.ForeColor = My.Settings.TextColor
            LazyEdit4 = False
        End If

    End Sub

    Private Sub BTNLS5_Click(sender As Object, e As EventArgs) Handles BTNLS5.Click


        If BTNLS5.Text <> "" Then
            chatBox.Text = BTNLS5.Text
            If ssh.WritingTaskFlag = True Then CheatCheck()
            SendButton.PerformClick()
        End If

    End Sub

    Private Sub BTNLS5Edit_Click(sender As Object, e As EventArgs) Handles BTNLS5Edit.Click


        LazyEdit2 = False
        LazyEdit3 = False
        LazyEdit4 = False
        LazyEdit1 = False

        BTNLS2Edit.BackColor = My.Settings.ButtonColor
        BTNLS2Edit.ForeColor = My.Settings.TextColor
        BTNLS3Edit.BackColor = My.Settings.ButtonColor
        BTNLS3Edit.ForeColor = My.Settings.TextColor
        BTNLS4Edit.BackColor = My.Settings.ButtonColor
        BTNLS4Edit.ForeColor = My.Settings.TextColor
        BTNLS1Edit.BackColor = My.Settings.ButtonColor
        BTNLS1Edit.ForeColor = My.Settings.TextColor

        If LazyEdit5 = False Then
            BTNLS5Edit.BackColor = Color.ForestGreen
            BTNLS5Edit.ForeColor = Color.White
            LazyEdit5 = True
        Else
            BTNLS5Edit.BackColor = My.Settings.ButtonColor
            BTNLS5Edit.ForeColor = My.Settings.TextColor
            LazyEdit5 = False
        End If

    End Sub

#End Region ' Lazy-Sub

#Region "-------------------------------------------------- Randomizer-App ----------------------------------------------------"

    Private Sub BTNRandomBlog_Click(sender As Object, e As EventArgs) Handles BTNRandomBlog.Click
        BTNRandomBlog.Enabled = False

        ShowImage(GetRandomImage(ImageGenre.Blog), True)
        ssh.JustShowedBlogImage = True

        BTNRandomBlog.Enabled = True
    End Sub

    Private Sub BTNRandomLocal_Click(sender As Object, e As EventArgs) Handles BTNRandomLocal.Click
        BTNRandomLocal.Enabled = False

        ShowImage(GetRandomImage(ImageSourceType.Local), True)
        ssh.JustShowedBlogImage = True

        BTNRandomLocal.Enabled = True
    End Sub

    Private Sub BTNRandomVideo_Click(sender As Object, e As EventArgs) Handles BTNRandomVideo.Click
        ssh.RandomizerVideo = True
        StartRandomVideo()
        ssh.RandomizerVideo = False
    End Sub

    Private Sub BTNRandomJOI_Click(sender As Object, e As EventArgs) Handles BTNRandomJOI.Click
        PlayRandomJOI()
    End Sub

    Private Sub BTNRandomCS_Click(sender As Object, e As EventArgs) Handles BTNRandomCS.Click
        mySession.Session.Domme.WasGreeted = True
        ssh.RandomizerVideoTease = True

        ssh.ScriptVideoTease = "Censorship Sucks"
        ssh.ScriptVideoTeaseFlag = True
        RandomVideo()
        ssh.ScriptVideoTeaseFlag = False
        ssh.CensorshipGame = True
        ssh.VideoTease = True
        ssh.CensorshipTick = ssh.randomizer.Next(FrmSettings.NBCensorHideMin.Value, FrmSettings.NBCensorHideMax.Value + 1)
        CensorshipTimer.Start()
    End Sub

    Private Sub BTNRandomAtE_Click(sender As Object, e As EventArgs) Handles BTNRandomAtE.Click

        mySession.Session.Domme.WasGreeted = True
        ssh.RandomizerVideoTease = True

        ScriptTimer.Stop()
        ssh.SubStroking = True
        ssh.TempStrokeTauntVal = ssh.StrokeTauntVal
        ssh.TempFileText = ssh.FileText
        ssh.ScriptVideoTease = "Avoid The Edge"
        ssh.ScriptVideoTeaseFlag = True
        ssh.AvoidTheEdgeStroking = True
        ssh.AvoidTheEdgeGame = True
        RandomVideo()
        ssh.ScriptVideoTeaseFlag = False
        ssh.VideoTease = True
        ssh.StartStrokingCount += 1
        StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
        StrokePace = 50 * Math.Round(StrokePace / 50)
        ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value
        AvoidTheEdgeTaunts.Start()

    End Sub

    Private Sub BTNRandomRLGL_Click(sender As Object, e As EventArgs) Handles BTNRandomRLGL.Click

        mySession.Session.Domme.WasGreeted = True
        ssh.RandomizerVideoTease = True

        ScriptTimer.Stop()
        ssh.SubStroking = True
        ssh.ScriptVideoTease = "RLGL"
        ssh.ScriptVideoTeaseFlag = True
        'AvoidTheEdgeStroking = True
        ssh.RLGLGame = True
        RandomVideo()
        ssh.ScriptVideoTeaseFlag = False
        ssh.VideoTease = True
        ssh.RLGLTick = ssh.randomizer.Next(FrmSettings.NBGreenLightMin.Value, FrmSettings.NBGreenLightMax.Value + 1)
        RLGLTimer.Start()
        ssh.StartStrokingCount += 1
        StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
        StrokePace = 50 * Math.Round(StrokePace / 50)
        'VideoTauntTick = randomizer.Next(20, 31)
        'VideoTauntTimer.Start()

    End Sub

    Private Sub BTNRandomCH_Click_1(sender As Object, e As EventArgs) Handles BTNRandomCH.Click
        PlayRandomCH()
    End Sub

    ''' =========================================================================================================
    ''' <summary>
    ''' Jumps to random videoposition in current video.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <ramarks>There is no need for parameter Sender and e. 
    ''' Only for Designer Compatiblity with Butten Clicks.</ramarks>
    ''' <exception cref="exception">Rethrows all exceptions to catcher, as long sender is nothing.</exception>
    Private Sub VideoJump2Random(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            If DomWMP.currentPlaylist.count = 0 Then Throw New Exception("No Video playing - can't jump.")

            Dim VideoLength As Integer = DomWMP.currentMedia.duration
            Dim VidLow As Integer = VideoLength * 0.4
            Dim VidHigh As Integer = VideoLength * 0.9
            Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

            DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint
        Catch ex As Exception
            If sender IsNot Nothing Then
                MsgBox("Error on jumping to Random Position in Video!" & vbCrLf & ex.Message,
                  vbExclamation, "Jump to random Position")
            Else
                Throw
            End If
        End Try
    End Sub

#End Region

#Region "--------------------------------------------------- Wishlist APP -----------------------------------------------------"

    Private Sub BTNPlaylist_Click(sender As Object, e As EventArgs) Handles BTNPlaylist.Click
        Dim dommePersonality As DommePersonality = CreateDommePersonality()
        If LBPlaylist.SelectedItems.Count = 0 Then
            MessageBox.Show(Me, "Please select a Playlist first!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If mySession.Session.Domme.WasGreeted Then
            MessageBox.Show(Me, "Please wait until you are not engaged with the domme to begin a Playlist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ssh.Playlist = True

        ssh.PlaylistFile = Txt2List(Application.StartupPath & "\Scripts\" & dommePersonality.PersonalityName & "\Playlist\" & LBPlaylist.SelectedItem & ".txt")
        ssh.PlaylistFile = StripBlankLines(ssh.PlaylistFile)
        ssh.PlaylistCurrent = 0
        chatBox.Text = "Hello " & dommePersonality.Honorific

        SendButton.PerformClick()

        BTNPlaylist.Enabled = False
    End Sub

    Private Sub BTNWishlist_Click(sender As Object, e As EventArgs) Handles BTNWishlist.Click
        If mySession.Session.Domme.WasGreeted Then
            MessageBox.Show(Me, "Please wait until you are not engaged with your domme to use this feature!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If WishlistCostSilver.Visible AndAlso ssh.SilverTokens >= Val(LBLWishlistCost.Text) Then
            ssh.SilverTokens -= Val(LBLWishlistCost.Text)
            My.Settings.SilverTokens = ssh.SilverTokens
            My.Settings.ClearWishlist = True

            WishlistCostGold.Visible = False
            WishlistCostSilver.Visible = False
            LBLWishlistBronze.Text = ssh.BronzeTokens
            LBLWishlistSilver.Text = ssh.SilverTokens
            LBLWishlistGold.Text = ssh.GoldTokens
            LBLWishListName.Text = ""
            WishlistPreview.Visible = False
            LBLWishlistCost.Text = ""
            LBLWishListText.Text = "Thank you for your purchase! " & mySettingsAccessor.DommeName & " has been notified of your generous gift. Please check back again tomorrow for a new item!"
            BTNWishlist.Enabled = False
            BTNWishlist.Text = ""

            Dim rewardsDir As String = Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Wishlist\Silver Rewards\"
            Dim silverList As List(Of String) = My.Computer.FileSystem.GetFiles(rewardsDir, FileIO.SearchOption.SearchTopLevelOnly, "*.txt").ToList()
            If Not silverList.Any() Then
                MessageBox.Show(Me, "No Silver Reward scripts were found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If

            mySession.Session.Domme.WasGreeted = True
            ssh.ShowModule = True
            ssh.FileText = silverList(ssh.randomizer.Next(0, silverList.Count))

            If Directory.Exists(My.Settings.DomImageDir) And ssh.SlideshowLoaded = False Then
                LoadDommeImageFolder()
            End If

            ssh.StrokeTauntVal = -1
            ssh.ScriptTick = 2
            ScriptTimer.Start()
            Return
        End If

        If WishlistCostGold.Visible AndAlso ssh.GoldTokens >= Val(LBLWishlistCost.Text) Then
            ssh.GoldTokens -= Val(LBLWishlistCost.Text)
            My.Settings.GoldTokens = ssh.GoldTokens
            My.Settings.ClearWishlist = True

            Dim rewardsDir As String = Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Wishlist\Gold Rewards\"
            Dim goldList As List(Of String) = My.Computer.FileSystem.GetFiles(rewardsDir, FileIO.SearchOption.SearchTopLevelOnly, "*.txt").ToList()
            If Not goldList.Any() Then
                MessageBox.Show(Me, "No Gold Reward scripts were found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If

            mySession.Session.Domme.WasGreeted = True
            ssh.ShowModule = True

            ssh.FileText = goldList(ssh.randomizer.Next(0, goldList.Count))

            If Directory.Exists(My.Settings.DomImageDir) And ssh.SlideshowLoaded = False Then
                LoadDommeImageFolder()
            End If

            ssh.StrokeTauntVal = -1
            ssh.ScriptTick = 2
            ScriptTimer.Start()
        End If
    End Sub

#End Region

#Region "------------------------------------------------- Hypno-Guide App ----------------------------------------------------"

    Private Sub BTNHypnoGenStart_Click(sender As Object, e As EventArgs) Handles BTNHypnoGenStart.Click
        If Not ssh.HypnoGen Then
            If CBHypnoGenInduction.Checked Then
                If File.Exists(Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Hypnotic Guide\Inductions\" & LBHypnoGenInduction.SelectedItem & ".txt") Then
                    ssh.Induction = True
                    ssh.FileText = Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Hypnotic Guide\Inductions\" & LBHypnoGenInduction.SelectedItem & ".txt"
                Else
                    MessageBox.Show(Me, "Please select a valid Hypno Induction File or deselect the Induction option!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    Return
                End If
            End If

            If File.Exists(Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Hypnotic Guide\Hypno Files\" & LBHypnoGen.SelectedItem & ".txt") Then
                If ssh.Induction = False Then
                    ssh.FileText = Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Hypnotic Guide\Hypno Files\" & LBHypnoGen.SelectedItem & ".txt"
                Else
                    ssh.TempHypno = Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Apps\Hypnotic Guide\Hypno Files\" & LBHypnoGen.SelectedItem & ".txt"
                End If
            Else
                MessageBox.Show(Me, "Please select a valid Hypno File!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If

            ssh.StrokeTauntVal = -1
            ssh.ScriptTick = 1
            ScriptTimer.Start()
            Dim HypnoTrack As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Hypnotic Guide\" & ComboBoxHypnoGenTrack.SelectedItem
            If File.Exists(HypnoTrack) Then DomWMP.URL = HypnoTrack
            ssh.HypnoGen = True
            ssh.AFK = True
            mySession.Session.Domme.WasGreeted = True

            BTNHypnoGenStart.Text = "End Session"
        Else
            mciSendString("CLOSE Speech1", String.Empty, 0, 0)
            mciSendString("CLOSE Echo1", String.Empty, 0, 0)
            DomWMP.Ctlcontrols.stop()

            ScriptTimer.Stop()
            ssh.StrokeTauntVal = -1
            ssh.HypnoGen = False
            ssh.Induction = False
            ssh.AFK = False
            mySession.Session.Domme.WasGreeted = False

            BTNHypnoGenStart.Text = "Guide Me!"
        End If
    End Sub

    Private Sub CBHypnoGenSlideshow_CheckedChanged(sender As Object, e As EventArgs) Handles CBHypnoGenSlideshow.CheckedChanged
        If Not FormLoading Then
            LBHypnoGenSlideshow.Enabled = CBHypnoGenSlideshow.Checked
        End If
    End Sub

    Private Sub CBHypnoGenInduction_CheckedChanged(sender As Object, e As EventArgs) Handles CBHypnoGenInduction.CheckedChanged
        If Not FormLoading Then
            LBHypnoGenInduction.Enabled = CBHypnoGenInduction.Checked
        End If
    End Sub
#End Region

#Region "--------------------------------------------------- VitalSub APP -----------------------------------------------------"

    Private Sub BTNExercise_Click(sender As Object, e As EventArgs) Handles BTNExercise.Click
        If TBExercise.Text <> "" Then
            CLBExercise.Items.Add(TBExercise.Text)
            TBExercise.Text = ""
            SaveExercise()
        End If
    End Sub

    Private Sub BTNCalorie_Click(sender As Object, e As EventArgs) Handles BTNCalorie.Click
        If TBCalorieItem.Text <> "" And TBCalorieAmount.Text <> "" Then
            Dim CalorieString As String
            CalorieString = TBCalorieItem.Text & " " & TBCalorieAmount.Text & " Calories"
            Dim Dupecheck As Boolean = False
            For i As Integer = 0 To ComboBoxCalorie.Items.Count - 1
                If CalorieString = ComboBoxCalorie.Items(i) Then Dupecheck = True
            Next
            ComboBoxCalorie.Items.Add(CalorieString)
            LBCalorie.Items.Add(CalorieString)

            If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\System\VitalSub\CalorieItems.txt")
            For i As Integer = 0 To LBCalorie.Items.Count - 1
                If Not File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieItems.txt", LBCalorie.Items(i), False)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieItems.txt", Environment.NewLine & LBCalorie.Items(i), True)
                End If
            Next

            If Dupecheck = False Then
                If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieList.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieList.txt", Environment.NewLine & CalorieString, True)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieList.txt", CalorieString, False)
                End If
            End If
            Dupecheck = False
            ssh.CaloriesConsumed += TBCalorieAmount.Text
            LBLCalorie.Text = ssh.CaloriesConsumed
            My.Settings.CaloriesConsumed = ssh.CaloriesConsumed
            TBCalorieItem.Text = ""
            TBCalorieAmount.Text = ""
        End If
    End Sub

    Private Sub ComboBoxCalorie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCalorie.SelectedIndexChanged

    End Sub

    Private Sub ComboBoxCalorie_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxCalorie.SelectionChangeCommitted
        If Not ComboBoxCalorie.SelectedItem Is Nothing Then
            Dim CalorieString As String = ComboBoxCalorie.SelectedItem
            LBCalorie.Items.Add(CalorieString)
            CalorieString = CalorieString.Replace(" Calories", "")
            Dim CalorieSplit As String() = Split(CalorieString)
            Dim TempCal As Integer = Val(CalorieSplit(CalorieSplit.Count - 1))
            ssh.CaloriesConsumed += TempCal
            LBLCalorie.Text = ssh.CaloriesConsumed
            My.Settings.CaloriesConsumed = ssh.CaloriesConsumed
            If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\System\VitalSub\CalorieItems.txt")
            For i As Integer = 0 To LBCalorie.Items.Count - 1
                If Not File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieItems.txt", LBCalorie.Items(i), False)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieItems.txt", Environment.NewLine & LBCalorie.Items(i), True)
                End If
            Next
        End If
    End Sub

    Private Sub CLBExercise_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CLBExercise.SelectedIndexChanged, CLBExercise.LostFocus
        SaveExercise()
    End Sub

    Private Sub CBVitalSub_CheckedChanged(sender As Object, e As EventArgs) Handles CBVitalSub.CheckedChanged
        If CBVitalSub.Checked = True Then
            CBVitalSub.ForeColor = Color.LightGreen
            CBVitalSub.Text = "VitalSub Active"
        Else
            CBVitalSub.ForeColor = Color.Red
            CBVitalSub.Text = "VitalSub Inactive"
        End If
    End Sub

    Private Sub CBVitalSub_LostFocus(sender As Object, e As EventArgs) Handles CBVitalSub.LostFocus
        If CBVitalSub.Checked = True Then
            My.Settings.VitalSub = True
        Else
            My.Settings.VitalSub = False
        End If
    End Sub

    Private Sub LBCalorie_DoubleClick(sender As Object, e As EventArgs) Handles LBCalorie.DoubleClick


        Dim CalorieString As String = LBCalorie.SelectedItem
        CalorieString = CalorieString.Replace(" Calories", "")
        Dim CalorieSplit As String() = Split(CalorieString)
        Dim TempCal As Integer = Val(CalorieSplit(CalorieSplit.Count - 1))
        ssh.CaloriesConsumed -= TempCal
        LBLCalorie.Text = ssh.CaloriesConsumed
        My.Settings.CaloriesConsumed = ssh.CaloriesConsumed
        LBCalorie.Items.Remove(LBCalorie.SelectedItem)
        If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\System\VitalSub\CalorieItems.txt")
        If LBCalorie.Items.Count > 0 Then
            For i As Integer = 0 To LBCalorie.Items.Count - 1
                If Not File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieItems.txt", LBCalorie.Items(i), False)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\System\VitalSub\CalorieItems.txt", Environment.NewLine & LBCalorie.Items(i), True)
                End If
            Next
        End If
    End Sub

    Private Sub BTNVitalSub_Click(sender As Object, e As EventArgs) Handles BTNVitalSub.Click
        If mySession.Session.Domme.WasGreeted = True Then
            MessageBox.Show(Me, "Please wait until you are not engaged with the domme to make VitalSub reports!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        mySession.Session.Domme.WasGreeted = True

        Dim vitalSubFail As Boolean = False

        If CLBExercise.Items.Count > 0 Then
            For i As Integer = 0 To CLBExercise.Items.Count - 1
                If Not CLBExercise.GetItemChecked(i) Then vitalSubFail = True
            Next
        End If

        If Val(LBLCalorie.Text) > Val(TBCalorie.Text) Then vitalSubFail = True

        Dim vitalSubState As String

        If vitalSubFail Then
            vitalSubState = "Punishments"
        Else
            vitalSubState = "Rewards"
        End If
        vitalSubFail = False
        Dim VitalList As New List(Of String)

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\VitalSub\" & vitalSubState & "\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
            VitalList.Add(foundFile)
        Next

        If VitalList.Count > 0 Then

            ' github patch begin
            ' For i As Integer = 0 To CLBExercise.Items.Count - 1
            'CLBExercise.SetItemChecked(i, False)
            'Next
            'SaveExercise()
            ' github patch end

            CLBExercise.Items.Clear()
            SaveExercise()


            LBCalorie.Items.Clear()
            If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieItems.txt") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\System\VitalSub\CalorieItems.txt")
            LBLCalorie.Text = 0
            ssh.CaloriesConsumed = 0
            My.Settings.CaloriesConsumed = 0

            ssh.FileText = VitalList(ssh.randomizer.Next(0, VitalList.Count))

            If Directory.Exists(My.Settings.DomImageDir) And ssh.SlideshowLoaded = False Then
                LoadDommeImageFolder()
            End If

            ssh.StrokeTauntVal = -1
            ssh.ScriptTick = 3
            ScriptTimer.Start()

        Else

            MessageBox.Show(Me, "No " & vitalSubState & " were found! Please make sure you have files in the VitaSub directory for this personality type!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
    End Sub

    Private Sub CLBExercise_DragLeave(sender As Object, e As EventArgs) Handles CLBExercise.DragLeave

        CLBExercise.Items.Remove(CLBExercise.SelectedItem)
    End Sub

    Private Sub CBVitalSubDomTask_CheckedChanged(sender As Object, e As EventArgs) Handles CBVitalSubDomTask.CheckedChanged
        If FormLoading = False Then
            My.Settings.VitalSubAssignments = CBVitalSubDomTask.Checked
        End If
    End Sub

#End Region ' Vital Sub

    Public Sub MetronomeTick()
        Dim wavFilepath As String = Application.StartupPath & "\Audio\System\metronome.wav"
        Dim MetroSoundPlayer As Media.SoundPlayer = Nothing
        Dim wavStream As MemoryStream
        Dim Errorcounter As Integer = 0

restartNoFile:
        Try
            If Directory.Exists(Path.GetDirectoryName(wavFilepath)) AndAlso File.Exists(wavFilepath) Then

                wavStream = New MemoryStream(File.ReadAllBytes(wavFilepath))
                MetroSoundPlayer = New Media.SoundPlayer(wavStream)
                MetroSoundPlayer.Load()
playLoop:
                ' copy variable to avoid needless locking delays
                Dim tmpStrokePace As Integer = StrokePaceMetronome

                If tmpStrokePace > 0 AndAlso CBMetronome.Checked Then
                    MetroSoundPlayer.Stop()
                    MetroSoundPlayer.Play()
                    Thread.Sleep(tmpStrokePace)
                Else
                    Thread.Sleep(1000)
                End If

                GoTo playLoop
            Else
                Thread.Sleep(10 * 1000)
                GoTo restartNoFile
            End If
        Catch ex As Exception When Errorcounter < 120
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                  All Errors until 119 Errors occured
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            'TODO: MetronomeExceptions: Log error - but first add synclock to logging.
            Errorcounter += 1
            Thread.Sleep(1000)
            GoTo restartNoFile
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            'TODO: MetronomeExceptions: Add possibility to restart the thread.
        End Try
    End Sub

#Region "-------------------------------------------------- Metronome-App -----------------------------------------------------"

    Private Sub BTNMetroPreview1_Click(sender As Object, e As EventArgs) Handles BTNMetroPreview1.Click
        If ssh.SubStroking = False Then StrokePace = NBMaxPace.Value
    End Sub

    Private Sub BTNMetroPreview2_Click(sender As Object, e As EventArgs) Handles BTNMetroPreview2.Click
        If ssh.SubStroking = False Then StrokePace = NBMinPace.Value
    End Sub

    Private Sub BTNMetroStop1_Click(sender As Object, e As EventArgs) Handles BTNMetroStop1.Click
        If ssh.SubStroking = False Then StrokePace = 0
    End Sub

    Private Sub BTNMetroStop2_Click(sender As Object, e As EventArgs) Handles BTNMetroStop2.Click
        If ssh.SubStroking = False Then StrokePace = 0
    End Sub

    Private Sub NBMaxPace_ValueChanged(sender As Object, e As EventArgs) Handles NBMaxPace.ValueChanged
        If FormLoading = False Then
            If NBMaxPace.Value > NBMinPace.Value - 50 Then NBMaxPace.Value = NBMinPace.Value - 50
            If ssh.SubStroking = False Then StrokePace = NBMaxPace.Value
            My.Settings.MaxPace = NBMaxPace.Value
        End If
    End Sub

    Private Sub NBMinPace_ValueChanged(sender As Object, e As EventArgs) Handles NBMinPace.ValueChanged
        If FormLoading = False Then
            If NBMinPace.Value < NBMaxPace.Value + 50 Then NBMinPace.Value = NBMaxPace.Value + 50
            If ssh.SubStroking = False Then StrokePace = NBMinPace.Value
            My.Settings.MinPace = NBMinPace.Value
        End If
    End Sub

    Private Sub TimeoutTimer_Tick(sender As Object, e As EventArgs) Handles TimeoutTimer.Tick

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        If chatBox.Text <> "" And ssh.TimeoutTick < 3 Then Return
        If ChatBox2.Text <> "" And ssh.TimeoutTick < 3 Then Return

        ssh.TimeoutTick -= 1

        If ssh.TimeoutTick < 1 Then

            TimeoutTimer.Stop()
            ssh.YesOrNo = False
            ssh.InputFlag = False

            ssh.SkipGotoLine = True
            GetGoto()

            RunFileText()

        End If

    End Sub

    Private Sub CBMetronome_LostFocus(sender As Object, e As EventArgs) Handles CBMetronome.LostFocus
        My.Settings.MetroOn = CBMetronome.Checked
    End Sub

#End Region ' Metronome App

#End Region ' Apps

    Private Sub VideoTimer_Tick(sender As Object, e As EventArgs) Handles VideoTimer.Tick

        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        ssh.VideoTick -= 1

        If ssh.VideoTick < 1 Then
            VideoTimer.Stop()
            DomWMP.Ctlcontrols.stop()
        End If


    End Sub

    Private Sub MultipleEdgesTimer_Tick(sender As Object, e As EventArgs) Handles MultipleEdgesTimer.Tick

        If ssh.DomTypeCheck = True Then Return
        If FrmSettings.CBSettingsPause.Checked = True And FrmSettings.SettingsPanel.Visible = True Then Return

        ssh.MultipleEdgesTick -= 1

        If ssh.MultipleEdgesTick < 1 Then

            MultipleEdgesTimer.Stop()

            ssh.DomChat = "#SYS_MultipleEdgesStart"
            If ssh.Contact1Edge = True Then ssh.DomChat = "@Contact1 #SYS_MultipleEdgesStart"
            If ssh.Contact2Edge = True Then ssh.DomChat = "@Contact2 #SYS_MultipleEdgesStart"
            If ssh.Contact3Edge = True Then ssh.DomChat = "@Contact3 #SYS_MultipleEdgesStart"

            ssh.MultipleEdgesMetronome = "START"

            ssh.EdgeCountTick = 0
            EdgeCountTimer.Start()

        End If

    End Sub

    Public Function OfflineConversion(ByVal OffString As String) As String

        ' Ixnay on the Wordplay

        OffString = OffString.Replace("@ShowBlogImage", "@ShowLocalImage")

        If My.Settings.CBIButts = False Then
            OffString = OffString.Replace("@ShowButtImage", "")
            OffString = OffString.Replace("@ShowButtsImage", "")
        End If

        If My.Settings.CBIBoobs = False Then
            OffString = OffString.Replace("@ShowBoobImage", "")
            OffString = OffString.Replace("@ShowBoobsImage", "")
        End If

        If OffString.Contains("@ShowImage[") Then
            Dim CheckImage As String = GetParentheses(OffString, "@ShowImage[")
            If CheckImage.Contains("://") Then
                OffString = OffString.Replace("@ShowImage[" & CheckImage & "]", "")
            End If
        End If

        Return OffString

        ' You gotta keep 'em numerated

    End Function

    Private Function FilterCheck(ByVal Input As String, ByVal ConditionControl As Control) As Boolean
        Dim TextCondition As String
        ' Cast the Type of the Control to access it's visible TextValue
        If TypeOf ConditionControl Is NumericUpDown Then
            TextCondition = DirectCast(ConditionControl, NumericUpDown).Value
        ElseIf TypeOf ConditionControl Is ComboBox Then
            TextCondition = DirectCast(ConditionControl, ComboBox).Text
        ElseIf TypeOf ConditionControl Is CheckBox Then
            TextCondition = DirectCast(ConditionControl, CheckBox).Checked
        Else
            Throw New Exception("Type of control not implemented in Function.")
        End If

        TextCondition = UCase(TextCondition)

        If TextCondition = "ALWAYS ALLOWS" Or TextCondition = "ALWAYS RUINS" Then TextCondition = "ALWAYS"
        If TextCondition = "OFTEN ALLOWS" Or TextCondition = "OFTEN RUINS" Then TextCondition = "OFTEN"
        If TextCondition = "SOMETIMES ALLOWS" Or TextCondition = "SOMETIMES RUINS" Then TextCondition = "SOMETIMES"
        If TextCondition = "RARELY ALLOWS" Or TextCondition = "RARELY RUINS" Then TextCondition = "RARELY"
        If TextCondition = "NEVER ALLOWS" Or TextCondition = "NEVER RUINS" Then TextCondition = "NEVER"


        Input = UCase(Input)
        'Input = Input.Replace(" ", "")

        If Input.Contains(",") Then
            Input = FixCommas(Input)
            Dim SplitArray() As String = Input.Split(",")

            If Input.Contains("NOT") Then
                For i As Integer = 0 To SplitArray.Count - 1
                    If SplitArray(i) = TextCondition Then Return False
                Next
                Return True
            Else
                For i As Integer = 0 To SplitArray.Count - 1
                    If SplitArray(i) = TextCondition Then Return True
                Next
            End If
        Else
            If Input = TextCondition Then Return True
        End If

        Return False

    End Function

    Public Sub ClearModes()

        ssh.EdgeGoto = False
        ssh.YesGoto = False
        ssh.NoGoto = False
        ssh.CameGoto = False
        ssh.RuinedGoto = False
        ssh.EdgeVideo = False
        ssh.YesVideo = False
        ssh.NoVideo_Mode = False
        ssh.CameVideo = False
        ssh.RuinedVideo = False
        ssh.EdgeMessage = False
        ssh.CameMessage = False
        ssh.RuinedMessage = False
        ssh.Modes.Clear()


    End Sub

    Public Function GetMatch(ByVal Line As String, ByVal Command As String, Match As String) As Boolean

        Dim CommandFlag As String = GetParentheses(Line, Command)

        If CommandFlag.Contains(",") Then

            CommandFlag = FixCommas(CommandFlag)

            Dim CommandArray As String() = CommandFlag.Split(",")

            Dim NotFlag As Boolean = False

            For i As Integer = 0 To CommandArray.Count - 1
                If CommandArray(i).ToUpper = "NOT" Then NotFlag = True
            Next

            If NotFlag = True Then

                For i As Integer = 0 To CommandArray.Count - 1
                    If CommandArray(i) = Match Then Return False
                Next

                Return True

            Else

                For i As Integer = 0 To CommandArray.Count - 1
                    If CommandArray(i) = Match Then Return True
                Next

            End If

        Else

            If CommandFlag = Match Then Return True

        End If

        Return False

    End Function

    Public Sub Edge()

        If ssh.SubStroking = True Then ssh.AlreadyStrokingEdge = True
        GetEdgeTickCheck()
        ssh.SubStroking = True
        ssh.LongEdge = False
        ssh.AskedToSpeedUp = False
        ssh.AskedToSlowDown = False
        ssh.EdgeCountTick = 0
        EdgeCountTimer.Start()
        ssh.SubEdging = True
        ssh.EdgeTauntInt = ssh.randomizer.Next(15, 31)
        EdgeTauntTimer.Start()
        If ssh.OrgasmAllowed = True Or ssh.OrgasmDenied = True Or ssh.OrgasmRuined = True Then ssh.OrgasmYesNo = True
        EdgePace()
        ActivateWebToy()
        DisableContactStroke()
        ssh.SessionEdges += 1

    End Sub

    Public Sub ClearWriteTask()
        ssh.WritingTaskCurrentTime = 0
        ssh.WritingTaskFlag = False
        chatBox.ShortcutsEnabled = True
        ChatBox2.ShortcutsEnabled = True
        ToggleAppVisibility(Nothing)
    End Sub

    Public Sub ClearChat()
        Throw New Exception("ClearChat")
    End Sub

    Public Function GetIf(ByVal CompareString As String) As Boolean

        ' CompareString = [x]operator[y]

        Dim ReturnVal As Boolean = False

        Dim CompareArray As String() = CompareString.Split("]")
        Dim C_Operator As String = CompareArray(1).Split("[")(0)
        Dim Val1 As String = CompareArray(0).Replace("[", "")
        Dim Val2 As String = CompareArray(1).Replace(C_Operator & "[", "")

        If Val1.StartsWith("#") Then Val1 = PoundClean(Val1)
        If Val2.StartsWith("#") Then Val2 = PoundClean(Val2)

        If Not IsNumeric(Val1) Then
            Dim VarCheck As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & Val1
            If File.Exists(VarCheck) Then Val1 = TxtReadLine(VarCheck)
        End If

        If Not IsNumeric(Val2) Then
            Dim VarCheck As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & Val2
            If File.Exists(VarCheck) Then Val2 = TxtReadLine(VarCheck)
        End If

        If C_Operator = "=" Or C_Operator = "==" Then
            If UCase(Val1) = UCase(Val2) Then Return True
        End If

        If C_Operator = "<>" Then
            If UCase(Val1) <> UCase(Val2) Then Return True
        End If

        If Not IsNumeric(Val1) And Not IsNumeric(Val2) Then Return False

        If C_Operator = ">" Then
            If Val(Val1) > Val(Val2) Then Return True
        End If

        If C_Operator = "<" Then
            If Val(Val1) < Val(Val2) Then Return True
        End If

        If C_Operator = ">=" Then
            If Val(Val1) >= Val(Val2) Then Return True
        End If

        If C_Operator = "<=" Then
            If Val(Val1) <= Val(Val2) Then Return True
        End If

        Return ReturnVal

    End Function

    Public Function GetArrayString(ByVal StringToSplit As String) As String()
        StringToSplit = FixCommas(StringToSplit)
        Dim ArrayString As String() = StringToSplit.Split(",")
        Return ArrayString
    End Function

    Public Function GetCharCount(ByVal StringClean As String, ByVal Character As String) As Integer
        Return Len(StringClean) - Len(Replace(StringClean, Character, ""))
    End Function


#Region "data marshalling For services"
    Private Function CreateDommePersonality() As DommePersonality
        Dim returnValue As DommePersonality = New DommePersonality()

        returnValue.PersonalityName = mySettingsAccessor.DommePersonality

        returnValue.IsCrazy = FrmSettings.crazyCheckBox.Checked
        returnValue.IsDegrading = FrmSettings.degradingCheckBox.Checked
        returnValue.IsSadistic = FrmSettings.sadisticCheckBox.Checked
        returnValue.IsSupremacist = FrmSettings.supremacistCheckBox.Checked
        returnValue.IsVulgar = FrmSettings.vulgarCheckBox.Checked

        returnValue.Age = Convert.ToUInt16(FrmSettings.domageNumBox.Value)
        returnValue.AgeOldLimit = Convert.ToUInt16(FrmSettings.NBSelfAgeMin.Value)
        returnValue.AgeYoungLimit = Convert.ToUInt16(FrmSettings.NBSelfAgeMax.Value)
        returnValue.Name = domName.Text
        returnValue.Honorific = FrmSettings.TBHonorific.Text

        returnValue.SubAgeOldLimit = Convert.ToUInt16(FrmSettings.NBSubAgeMin.Value)
        returnValue.SubAgeYoungLimit = Convert.ToUInt16(FrmSettings.NBSubAgeMax.Value)

        returnValue.CockBigLimit = Convert.ToUInt16(FrmSettings.NBAvgCockMin.Value)
        returnValue.CockSmallLimit = Convert.ToUInt16(FrmSettings.NBAvgCockMin.Value)

        returnValue.AllowsOrgasms = MapToAllowsOrgasms(FrmSettings.alloworgasmComboBox.SelectedItem.ToString())
        returnValue.RuinsOrgasms = MapToRuinsOrgasms(FrmSettings.ruinorgasmComboBox.SelectedItem.ToString())

        returnValue.ApathyLevel = ApathyLevel.Create(Convert.ToInt32(FrmSettings.NBEmpathy.Value)).Value
        returnValue.DomLevel = DomLevel.Create(Convert.ToInt32(FrmSettings.DominationLevel.Value)).Value

        returnValue.CupSize = CupSize.Create(FrmSettings.boobComboBox.SelectedItem.ToString()).Value
        returnValue.BirthDay = New DateTime(DateTime.Now.Year, FrmSettings.NBDomBirthdayMonth.Value, FrmSettings.NBDomBirthdayDay.Value)

        returnValue.MoodLevel = MoodLevel.Create(Convert.ToInt32(ssh.DommeMood)).Value
        returnValue.MoodAngry = MoodLevel.Create(Convert.ToInt32(FrmSettings.NBDomMoodMin.Value)).Value
        returnValue.MoodHappy = MoodLevel.Create(Convert.ToInt32(FrmSettings.NBDomMoodMax.Value)).Value

        returnValue.RequiresHonorific = FrmSettings.CBHonorificInclude.Checked
        returnValue.RequiresHonorificCapitalized = FrmSettings.CBHonorificCapitalized.Checked

        Return returnValue
    End Function

    Private Function CreateSubPersonality() As SubPersonality
        Dim returnValue As SubPersonality = New SubPersonality()

        returnValue.Age = Convert.ToUInt16(FrmSettings.subAgeNumBox.Value)
        returnValue.Birthday = New DateTime(DateTime.Now.Year, My.Settings.SubBirthMonth, My.Settings.SubBirthDay)
        returnValue.CockSize = Convert.ToInt32(FrmSettings.CockSizeNumBox.Value)
        returnValue.Name = SubName.Text
        returnValue.IsCircumsized = FrmSettings.CBSubCircumcised.Checked
        returnValue.IsCockPierced = FrmSettings.CBSubPierced.Checked


        ' I don't know what these do, in theory, they should replace the slider setting
        'returnValue.BallsTortureLevel = TortureLevel.Create(ssh.CBTBallsCount).Value()
        'returnValue.CockTortureLevel = TortureLevel.Create(ssh.CBTCockCount).Value()
        returnValue.BallsTortureLevel = TortureLevel.Create(FrmSettings.CockAndBallTortureLevelSlider.Value).Value
        returnValue.CockTortureLevel = TortureLevel.Create(FrmSettings.CockAndBallTortureLevelSlider.Value).Value
        returnValue.Safeword = mySettingsAccessor.SafeWord

#Region "Setup Kinks"
        If FrmSettings.CockTortureEnabledCB.Checked Then
            returnValue.Kinks.Add(Kink.CockTorture)
        Else
            returnValue.Kinks.Remove(Kink.CockTorture)
        End If

        If FrmSettings.BallTortureEnabledCB.Checked Then
            returnValue.Kinks.Add(Kink.BallTorture)
        Else
            returnValue.Kinks.Remove(Kink.BallTorture)
        End If
#End Region

#Region "Setup Toybox"
        If FrmSettings.CBOwnChastity.Checked Then
            returnValue.ToyBox.Add(Toy.ChastityDevice)
        Else
            returnValue.ToyBox.Remove(Toy.ChastityDevice)
        End If

        If FrmSettings.DoesChastityDeviceRequirePiercingCB.Checked Then
            returnValue.ToyBox.Add(Toy.ChastityDeviceRequiresPiercing)
        Else
            returnValue.ToyBox.Remove(Toy.ChastityDeviceRequiresPiercing)
        End If

        If FrmSettings.ChastityDeviceContainsSpikesCB.Checked Then
            returnValue.ToyBox.Add(Toy.ChastityDeviceWithSpikes)
        Else
            returnValue.ToyBox.Remove(Toy.ChastityDeviceWithSpikes)
        End If
#End Region

#Region "Setup pet names"

        ' Happy mood
        If Not String.IsNullOrWhiteSpace(FrmSettings.PetNameBox1.Text) Then
            returnValue.PetNames.Add(FrmSettings.PetNameBox1.Text)
        End If
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox2.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox2.Text)
        End If

        ' Normal mood
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox3.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox3.Text)
        End If
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox4.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox4.Text)
        End If
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox5.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox5.Text)
        End If
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox6.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox6.Text)
        End If

        ' Angry mood
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox7.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox7.Text)
        End If
        If Not String.IsNullOrWhiteSpace(FrmSettings.petnameBox8.Text) Then
            returnValue.PetNames.Add(FrmSettings.petnameBox8.Text)
        End If
#End Region
        returnValue.InChastity = My.Settings.SubInChastity

        Return returnValue
    End Function

    Public Function CreateSession() As Session
        Dim returnValue As Session = New Session(CreateDommePersonality(), CreateSubPersonality())
        returnValue.Sub.IsStroking = ssh.SubStroking
        returnValue.Sub.IsEdging = ssh.SubEdging
        returnValue.IsFirstRound = ssh.FirstRound
        returnValue.IsOrgasmAllowed = ssh.OrgasmAllowed
        returnValue.IsOrgasmRuined = ssh.OrgasmRuined
        returnValue.IsBeforeTease = ssh.BeforeTease
        returnValue.MinimumTaskTime = FrmSettings.TaskWaitMinimum.Value
        returnValue.MaximumTaskTime = FrmSettings.TaskWaitMaximum.Value

        Return returnValue
    End Function

    Private Function CreateDommeMessagePreferences() As ChatMessagePreferences
        Dim messagePreferences As ChatMessagePreferences = New ChatMessagePreferences()
        messagePreferences.ShowTimeStamp = FrmSettings.TimeStampCheckBox.Checked
        messagePreferences.FontName = FrmSettings.SubMessageFontCB.Text
        messagePreferences.FontSize = Convert.ToInt32(FrmSettings.NBFontSize.Value)
        messagePreferences.FontColor = Color2Html(My.Settings.ChatTextColor)
        messagePreferences.ShowSenderName = FrmSettings.ShowNamesCheckBox.Checked
        messagePreferences.SenderColor = My.Settings.DomColor
        messagePreferences.BackgroundColor = Color2Html(My.Settings.ChatWindowColor)
        Return messagePreferences
    End Function

    Private Function CreateSubMessagePreferences() As ChatMessagePreferences
        Dim messagePreferences As ChatMessagePreferences = New ChatMessagePreferences()
        messagePreferences.ShowTimeStamp = FrmSettings.TimeStampCheckBox.Checked
        messagePreferences.FontName = FrmSettings.SubMessageFontCB.Text
        messagePreferences.FontSize = Convert.ToInt32(FrmSettings.NBFontSize.Value)
        messagePreferences.FontColor = Color2Html(My.Settings.ChatTextColor)
        messagePreferences.ShowSenderName = FrmSettings.ShowNamesCheckBox.Checked
        messagePreferences.SenderColor = My.Settings.SubColor
        messagePreferences.BackgroundColor = Color2Html(My.Settings.ChatWindowColor)
        Return messagePreferences
    End Function
#End Region

#Region "junk methods that should go away"
    Private Function MapToAllowsOrgasms(data As String) As AllowsOrgasms
        If data.ToLower().Contains("never") Then
            Return AllowsOrgasms.Never
        ElseIf data.ToLower().Contains("rarely") Then
            Return AllowsOrgasms.Rarely
        ElseIf data.ToLower().Contains("sometimes") Then
            Return AllowsOrgasms.Sometimes
        ElseIf data.ToLower().Contains("often") Then
            Return AllowsOrgasms.Often
        ElseIf data.ToLower().Contains("always") Then
            Return AllowsOrgasms.Always
        End If
        Throw New Exception(data + " is an unknown frequency")
    End Function

    Private Function MapToRuinsOrgasms(data As String) As RuinsOrgasms
        If data.ToLower().Contains("never") Then
            Return RuinsOrgasms.Never
        ElseIf data.ToLower().Contains("rarely") Then
            Return RuinsOrgasms.Rarely
        ElseIf data.ToLower().Contains("sometimes") Then
            Return RuinsOrgasms.Sometimes
        ElseIf data.ToLower().Contains("usually") Then
            Return RuinsOrgasms.Often
        ElseIf data.ToLower().Contains("always") Then
            Return RuinsOrgasms.Always
        End If
        Throw New Exception(data + " is an unknown frequency")
    End Function
#End Region

#Region "Session Engine Events"
    Public Sub mySession_DommeSaid(sender As Object, e As DommeSaidEventArgs)
        Dim session As SessionEngine = CType(sender, SessionEngine)
        If InvokeRequired Then
            BeginInvoke(New MethodInvoker(Sub() mySession_DommeSaid(sender, e)))
        Else
            e.ChatMessage.Message = ToBeMigrated(CreateDommePersonality(), e.ChatMessage.Message)
            If String.IsNullOrWhiteSpace(e.ChatMessage.Message) Then Return
            myDommeMessages.Enqueue(e.ChatMessage)
            If myDommeMessages.Any() Then
                SendTimer.Enabled = True
                SendTimer.Interval = GetTypingDelay(myDommeMessages.Peek(), mySettingsAccessor.DoesDommeTypeInstantly)
            End If
        End If
    End Sub

    Private Sub mySession_ShowImage(sender As Object, e As ShowImageEventArgs)
        If (InvokeRequired) Then
            BeginInvoke(New MethodInvoker(Sub() ShowImage(e.ImageMetaData.ItemName, False)))
        End If
    End Sub

    ''' <summary>
    ''' Handles the PlayVideo event. If the event happens on not the UI thread, then we pass it to the UI thread so everything is happy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mySession_PlayVideo(sender As Object, e As PlayVideoEventArgs)
        If (InvokeRequired) Then
            Invoke(New MethodInvoker(Sub() mySession_PlayVideo(sender, e)))
            'Dim foo = Invoke(New MethodInvoker(Function() PlayVideo(e.VideoMetaData, e.ShouldRandomizeStart)))
        Else
            e.Result = PlayVideo(e.VideoMetaData, e.ShouldRandomizeStart)
        End If
    End Sub

    Private Sub Safeword_Spoken(sender As Object, e As MessageProcessedEventArgs)

        Dim safeWord = myGetScripts.GetAvailableScripts(mySession.Session.Domme, mySession.Session.Sub, "Interrupt", "SafeWord") _
                .Ensure(Function(scripts) scripts.Count > 0, "No scripts were found for safeword") _
                .OnSuccess(Sub(scripts)
                               StopEverything()
                               ssh.FileText = scripts(ssh.randomizer.Next(0, scripts.Count)).Key
                               ssh.ShowModule = True
                               ssh.StrokeTauntVal = -1
                               ssh.ScriptTick = 2
                               ScriptTimer.Start()
                           End Sub)

        If safeWord.IsFailure Then
            MessageBox.Show(Me, safeWord.Error.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End If
        Return
    End Sub

    Private Sub Greeting_Spoken(sender As Object, e As MessageProcessedEventArgs)
        ssh.BeforeTease = True
        Dim sesh As Session = mySession.Session
        sesh.IsBeforeTease = True
        mySession.Session = sesh

        If My.Settings.LockOrgasmChances Then FrmSettings.LockOrgasmChances(True)

        If ssh.PlaylistFile.Count = 0 Then GoTo NoPlaylistStartFile

        If ssh.Playlist = False Or ssh.PlaylistFile(0).Contains("Random Start") Then

NoPlaylistStartFile:
            mySession.BeginSession()
        Else
            If ssh.PlaylistFile(0).Contains("Regular-TeaseAI-Script") Then
                ssh.FileText = Application.StartupPath & "\Scripts\" & e.Session.Domme.PersonalityName & "\Stroke\Start\" & ssh.PlaylistFile(0)
                ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "")
                ssh.FileText = ssh.FileText & ".txt"
            Else
                ssh.FileText = Application.StartupPath & "\Scripts\" & e.Session.Domme.PersonalityName & "\Playlist\Start\" & ssh.PlaylistFile(0) & ".txt"
            End If

            If ssh.Playlist = True Then ssh.PlaylistCurrent += 1
            ssh.LastScriptCountdown = ssh.randomizer.Next(3, 5 * Convert.ToInt32(e.Session.Domme.DomLevel))

            If Directory.Exists(My.Settings.DomImageDir) And ssh.SlideshowLoaded = False Then
                LoadDommeImageFolder()
            End If
        End If

        ssh.StrokeTauntVal = -1
    End Sub

    Private Sub Task_Requested(sender As Object, e As MessageProcessedEventArgs)
        Dim taskList As New List(Of String)
        For Each TaskFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" + e.Session.Domme.PersonalityName + "\Interrupt\Start Tasks\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
            taskList.Add(TaskFile)
        Next

        If taskList.Count > 0 Then
            If Directory.Exists(My.Settings.DomImageDir) And ssh.SlideshowLoaded = False Then
                LoadDommeImageFolder()
            End If
            ssh.BeforeTease = True
            mySession.Session.Domme.WasGreeted = True
            ssh.SubEdging = False
            ssh.SubHoldingEdge = False
            ssh.FileText = taskList(ssh.randomizer.Next(0, taskList.Count))
            ssh.LockImage = False
            If ssh.SlideshowLoaded = True Then
                ImageSlideShowNextButton.Enabled = True
                ImageSlideShowPreviousButton.Enabled = True
                PicStripTSMIdommeSlideshow.Enabled = True
            End If
            ssh.StrokeTauntVal = -1
            ssh.ScriptTick = 3
            ScriptTimer.Start()
            ssh.ShowModule = False
        Else
            MessageBox.Show(Me, "No files were found in " & Application.StartupPath & "\Scripts\" + e.Session.Domme.PersonalityName + "\Interrupt\Start Tasks!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End If
    End Sub
#End Region

#Region "UI Events"
    Private Sub Form1_PreviewKeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = (Keys.F Or Keys.Control) Then
            FullscreenToolStripMenuItem_Click(Nothing, Nothing)
        ElseIf e.Alt AndAlso MainMenuStrip.Visible = False Then
            MainMenuStrip.Visible = True
            MainMenuStrip.Focus()
        ElseIf e.Alt AndAlso FormBorderStyle = Windows.Forms.FormBorderStyle.None Then
            MainMenuStrip.Visible = False
        End If
    End Sub

    Private Sub MenuStrip2_Leave(sender As Object, e As EventArgs) Handles MenuStrip2.Leave
        If FormBorderStyle = Windows.Forms.FormBorderStyle.None Then
            MainMenuStrip.Visible = False
        End If
    End Sub


    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        My.Settings.SideChat = True
        ToggleAppVisibility(PNLChatBox2)
    End Sub

#Region "------------------------------------------------------ MenuStuff -----------------------------------------------------"

#Region "-------------------------------------------------------- File --------------------------------------------------------"

    Private Sub dompersonalitycombobox_LostFocus(sender As Object, e As EventArgs) Handles DommePersonalityComboBox.LostFocus
        mySettingsAccessor.DommePersonality = DommePersonalityComboBox.Text
    End Sub

    Private Sub dompersonalitycombobox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DommePersonalityComboBox.SelectedIndexChanged
        If FormLoading = True Then Exit Sub

        Try
            mySettingsAccessor.DommePersonality = DommePersonalityComboBox.Text
            FrmSettings.LBLGlitModDomType.Text = DommePersonalityComboBox.Text

            ssh.DomPersonality = DommePersonalityComboBox.Text

            FrmSettings.FrmSettingStartUp()

            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Contact_Descriptions.txt") Then
                Dim ContactList As New List(Of String)
                ContactList = Txt2List(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Contact_Descriptions.txt")
                FrmSettings.GBGlitter1.Text = PoundClean(ContactList(0))
                FrmSettings.GBGlitter2.Text = PoundClean(ContactList(1))
                FrmSettings.GBGlitter3.Text = PoundClean(ContactList(2))
            Else
                FrmSettings.GBGlitter1.Text = "Contact 1"
                FrmSettings.GBGlitter2.Text = "Contact 2"
                FrmSettings.GBGlitter3.Text = "Contact 3"
            End If

            Form9.LBLPersonality.Text = DommePersonalityComboBox.Text

        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Log.WriteError(ex.Message, ex, "Error on changing Personality")
            MessageBox.Show(ex.Message, "Error on changing Personality", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Private Sub SuspendSessionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuspendSessionToolStripMenuItem.Click
        Try

            If Not mySession.Session.Domme.WasGreeted Then
                MessageBox.Show(Me, "Tease AI is not currently running a session!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If

            Dim filename As String = myPathsAccessor.SavedSessionDefaultPath
            '	 ===============================================================================
            '						 Custom Location if Control-Key pressed
            '	 ===============================================================================
            If My.Computer.Keyboard.CtrlKeyDown Then
                Dim fsd As New SaveFileDialog With {.Filter = "Saved Session|*" & Path.GetExtension(myPathsAccessor.SavedSessionDefaultPath) & "",
                                                    .InitialDirectory = Path.GetDirectoryName(myPathsAccessor.SavedSessionDefaultPath),
                                                    .Title = "Select a destination to safe the sessin to.",
                                                    .FileName = Now.ToString("yy-MM-dd_HH-mm-ss") & "_" & DommePersonalityComboBox.Text,
                                                    .AddExtension = True,
                                                    .CheckPathExists = True,
                                                    .OverwritePrompt = True,
                                                    .ValidateNames = True}
                If fsd.ShowDialog() = DialogResult.Cancel Then Exit Sub

                filename = fsd.FileName
                '===============================================================================
                '						Check if default-File exists
                '===============================================================================
            ElseIf File.Exists(filename) _
            AndAlso MessageBox.Show(Me, "A previous saved state already exists!" &
                                    vbCrLf & vbCrLf &
                                    "Do you wish to overwrite it?", "Warning!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If

            ' Store Session to disk
            ssh.Save(filename)

            MessageBox.Show(Me, "Session state has been saved successfully!", "Success!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            MessageBox.Show(Me, "An error occurred and the state did not save correctly!" &
                            vbCrLf & vbCrLf & ex.Message,
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Private Sub ResumeSessionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResumeSessionToolStripMenuItem.Click
        Try
            Dim filename As String = myPathsAccessor.SavedSessionDefaultPath

            '						 Custom Location if Control-Key pressed
            If My.Computer.Keyboard.CtrlKeyDown Then
                Dim fsd As New OpenFileDialog With {.Filter = "Saved Session|*" & Path.GetExtension(myPathsAccessor.SavedSessionDefaultPath) & "",
                                                    .InitialDirectory = Path.GetDirectoryName(myPathsAccessor.SavedSessionDefaultPath),
                                                    .Title = "Select a saved session to resume.",
                                                    .CheckPathExists = True,
                                                    .CheckFileExists = True,
                                                    .AddExtension = True,
                                                    .ValidateNames = True}
                If fsd.ShowDialog() = DialogResult.Cancel Then Exit Sub

                filename = fsd.FileName
                '===============================================================================
                '						Check if default-File exists
                '===============================================================================
            ElseIf Not File.Exists(filename) Then
                MessageBox.Show(Me, Path.GetFileName(myPathsAccessor.SavedSessionDefaultPath) & " could not be found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Exit Sub
            End If

            If mySession.Session.Domme.WasGreeted = True _
            AndAlso MessageBox.Show(Me, "Resuming a previous state will cause you to lose your progress in this session!" &
                                    vbCrLf & vbCrLf &
                                    "Do you wish to proceed?", "Warning!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If

            ssh.Load(filename, True)

            If mySession.Session.Domme.WasGreeted And My.Settings.LockOrgasmChances Then _
                FrmSettings.LockOrgasmChances(True)

        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            MessageBox.Show(Me, "An error occurred and the state was not loaded correctly!" &
                            vbCrLf & vbCrLf & ex.Message,
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Private Sub ResetSessionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetSessionToolStripMenuItem.Click
        If mySession.Session.Domme.WasGreeted = False Then
            MessageBox.Show(Me, "Tease AI is not currently running a session!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        mySession.EndSession()

        SaveChatLog(DateTime.Now.ToString("MM.dd.yyyy hhmm"))

        If ssh.DomTypeCheck = False Then
            ssh.DomTask = "<b>Tease AI has been reset</b>"
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click,
                                                                                                    ExitToolStripMenuItem1.Click
        Me.Close()
        Me.Dispose()
    End Sub

#End Region ' File

#Region "------------------------------------------------------ Settings ------------------------------------------------------"

    Private Sub GeneralSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeneralSettingsToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(0)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub DommeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DommeToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(1)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub SubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(2)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub ScriptsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(3)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub ImagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImagesToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(4)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub TaggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TaggingToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(5)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub URLFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles URLFilesToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(6)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub VideoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VideoToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(7)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub AppsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AppsToolStripMenuItem1.Click
        FrmSettings.SettingsTabs.SelectTab(8)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub RangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RangesToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(10)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub ModdingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModdingToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(11)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub MiscToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MiscToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(12)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

#End Region ' Settings

#Region "-------------------------------------------------------- APPs --------------------------------------------------------"

    Private Sub CloseAppPanelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseAppPanelToolStripMenuItem.Click
        ToggleAppVisibility(Nothing)
    End Sub

    Private Sub MetronomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MetronomeToolStripMenuItem.Click
        ToggleAppVisibility(PNLMetronome)
    End Sub

    Private Sub GlitterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GlitterToolStripMenuItem.Click
        ToggleAppVisibility(PnlGlitter)
    End Sub

    Private Sub DommeTagsToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DommeTagsToolStripMenuItem2.Click
        ToggleAppVisibility(PNLDomTagBTN)
    End Sub

    Private Sub LazySubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LazySubToolStripMenuItem.Click
        ToggleAppVisibility(PNLLazySub)
    End Sub

    Private Sub RandomizerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RandomizerToolStripMenuItem.Click
        ToggleAppVisibility(PNLAppRandomizer)
    End Sub

    Private Sub PlaylistToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlaylistToolStripMenuItem.Click
        If PNLPlaylist.Visible = False Then
            ToggleAppVisibility(PNLPlaylist)
            LBPlaylist.Items.Clear()
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Playlist\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                LBPlaylist.Items.Add(Path.GetFileName(foundFile).Replace(".txt", ""))
            Next
        End If
    End Sub

    Private Sub WritingTasksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WritingTasksToolStripMenuItem.Click
        ToggleAppVisibility(PNLWritingTask)
    End Sub

    Private Sub WishlistToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WishlistToolStripMenuItem.Click
        If PNLWishList.Visible Then
            Return
        End If
        If My.Settings.ClearWishlist Then
            MessageBox.Show(Me, "You have already purchased " & mySettingsAccessor.DommeName & "'s Wishlist item for today!" & Environment.NewLine & Environment.NewLine &
                                "Please check back again tomorrow!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        LBLWishlistDom.Text = mySettingsAccessor.DommeName & "'s Wishlist"
        LBLWishlistDate.Text = Now.ToShortDateString()
        LBLWishlistBronze.Text = ssh.BronzeTokens
        LBLWishlistSilver.Text = ssh.SilverTokens
        LBLWishlistGold.Text = ssh.GoldTokens

        If Date.Compare(My.Settings.WishlistDate.Date, Now.Date) Then
            Dim itemsPath As String = Application.StartupPath + "\Scripts\" + mySettingsAccessor.DommePersonality + "\Apps\Wishlist\Items"
            Dim wishList As List(Of String) = My.Computer.FileSystem.GetFiles(itemsPath, FileIO.SearchOption.SearchTopLevelOnly, "*.txt").ToList()

            If Not wishList.Any() Then
                MessageBox.Show(Me, "No Wishlist items found!" & Environment.NewLine & Environment.NewLine &
                                "Please make sure you have item scripts located in Apps\Wishlist\Items.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If

            WishlistCostGold.Visible = False
            WishlistCostSilver.Visible = False
            LBLWishListText.Text = ""

            Dim wishFile As String = wishList(myRandomNumberService.Roll(0, wishList.Count))
            Dim wishItem As List(Of String) = Txt2List(wishFile)

            LBLWishListName.Text = wishItem(0)
            My.Settings.WishlistName = LBLWishListName.Text
            WishlistPreview.Load(wishItem(1))
            WishlistPreview.Visible = True
            My.Settings.WishlistPreview = wishItem(1)

            LBLWishlistCost.Text = wishItem(2)
            Dim token = GetTokenDenomination(wishItem(2))
            WishlistCostSilver.Visible = token = TokenDenomination.Silver
            My.Settings.WishlistTokenType = token.ToString()

            My.Settings.WishlistCost = Val(LBLWishlistCost.Text)

            LBLWishListText.Text = wishItem(3)
            My.Settings.WishlistNote = wishItem(3)

            If token = TokenDenomination.Gold AndAlso ssh.GoldTokens >= Val(LBLWishlistCost.Text) Then
                BTNWishlist.Enabled = True
                BTNWishlist.Text = "Purchase for " & domName.Text
            ElseIf token = TokenDenomination.Silver AndAlso ssh.SilverTokens >= Val(LBLWishlistCost.Text) Then
                BTNWishlist.Enabled = True
                BTNWishlist.Text = "Purchase for " & domName.Text
            Else
                BTNWishlist.Enabled = False
                BTNWishlist.Text = "Not Enough Tokens!"
            End If

            My.Settings.WishlistDate = FormatDateTime(Now, DateFormat.ShortDate)
        Else
            LBLWishListName.Text = My.Settings.WishlistName
            Try
                WishlistPreview.Load(My.Settings.WishlistPreview)
            Catch
                WishlistPreview.Load(Application.StartupPath & "\Images\System\NoPreview.png")
            End Try

            If My.Settings.WishlistTokenType = "Silver" Then WishlistCostSilver.Visible = True
            If My.Settings.WishlistTokenType = "Gold" Then WishlistCostGold.Visible = True
            LBLWishlistCost.Text = My.Settings.WishlistCost
            LBLWishListText.Text = My.Settings.WishlistNote

            If WishlistCostGold.Visible AndAlso ssh.GoldTokens >= Val(LBLWishlistCost.Text) Then
                BTNWishlist.Text = "????? Gold"
                BTNWishlist.Enabled = True
            ElseIf WishlistCostSilver.Visible AndAlso ssh.SilverTokens >= Val(LBLWishlistCost.Text) Then
                BTNWishlist.Text = "???? Silver"
                BTNWishlist.Enabled = True
            Else
                BTNWishlist.Text = "Not Enough Tokens!"
                BTNWishlist.Enabled = False
            End If

        End If

        If WishlistCostGold.Visible AndAlso ssh.GoldTokens >= Val(LBLWishlistCost.Text) Then
            BTNWishlist.Text = "Purchase for " & domName.Text
            BTNWishlist.Enabled = True
        ElseIf WishlistCostSilver.Visible AndAlso ssh.SilverTokens >= Val(LBLWishlistCost.Text) Then
            BTNWishlist.Text = "Purchase for " & domName.Text
            BTNWishlist.Enabled = True
        Else
            BTNWishlist.Text = "Not Enough Tokens!"
            BTNWishlist.Enabled = False
        End If

        ToggleAppVisibility(PNLWishList)
    End Sub

    Private Sub HypnoticGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HypnoticGuideToolStripMenuItem.Click
        ToggleAppVisibility(PNLHypnoGen)
        If PNLHypnoGen.Visible = False Then



            LBHypnoGenInduction.Items.Clear()

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Hypnotic Guide\Inductions\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")

                Dim TempUrl As String = foundFile
                TempUrl = TempUrl.Replace(".txt", "")
                Do Until Not TempUrl.Contains("\")
                    TempUrl = TempUrl.Remove(0, 1)
                Loop
                LBHypnoGenInduction.Items.Add(TempUrl)

            Next

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Hypnotic Guide\", FileIO.SearchOption.SearchTopLevelOnly, "*.mp3")
                Dim TempUrl As String = foundFile
                Do Until Not TempUrl.Contains("\")
                    TempUrl = TempUrl.Remove(0, 1)
                Loop
                ComboBoxHypnoGenTrack.Items.Add(TempUrl)
            Next



            LBHypnoGen.Items.Clear()

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Hypnotic Guide\Hypno Files\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")

                Dim TempUrl As String = foundFile
                TempUrl = TempUrl.Replace(".txt", "")
                Do Until Not TempUrl.Contains("\")
                    TempUrl = TempUrl.Remove(0, 1)
                Loop
                LBHypnoGen.Items.Add(TempUrl)

            Next



        End If


    End Sub

    Private Sub VitalSubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VitalSubToolStripMenuItem.Click
        ToggleAppVisibility(AppPanelVitalSub)
        If AppPanelVitalSub.Visible = False Then

            If File.Exists(Application.StartupPath & "\System\VitalSub\CalorieList.txt") And ComboBoxCalorie.Items.Count = 0 Then
                'Read all lines of the given file.
                Dim CalList As List(Of String) = Txt2List(Application.StartupPath & "\System\VitalSub\CalorieList.txt")

                For i As Integer = 0 To CalList.Count - 1
                    ComboBoxCalorie.Items.Add(CalList(i))
                Next
            End If

        End If

    End Sub

#End Region ' APPs

#Region "-------------------------------------------------------- Games -------------------------------------------------------"

    Private Sub SlotsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SlotsToolStripMenuItem1.Click,
                                                                                                    SlotsToolStripMenuItem.Click
        GamesWindow.TCGames.SelectTab(0)
        GamesWindow.Show()
        GamesWindow.Focus()
    End Sub

    Private Sub MatchGameToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MatchGameToolStripMenuItem1.Click,
                                                                                                        MatchGameToolStripMenuItem.Click
        GamesWindow.TCGames.SelectTab(1)
        GamesWindow.Show()
        GamesWindow.Focus()
    End Sub

    Private Sub RiskyPickToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RiskyPickToolStripMenuItem1.Click,
                                                                                                        RiskyPickToolStripMenuItem.Click
        GamesWindow.TCGames.SelectTab(2)
        GamesWindow.Show()
        GamesWindow.Focus()
    End Sub

    Private Sub ExchangeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExchangeToolStripMenuItem1.Click,
                                                                                                        ExchangeToolStripMenuItem.Click
        GamesWindow.TCGames.SelectTab(3)
        GamesWindow.Show()
        GamesWindow.Focus()
    End Sub

    Private Sub CollectionToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CollectionToolStripMenuItem1.Click,
                                                                                                        CollectionToolStripMenuItem.Click
        GamesWindow.TCGames.SelectTab(4)
        GamesWindow.Show()
        GamesWindow.Focus()
    End Sub

#End Region ' Games

#Region "----------------------------------------------------- Interface ------------------------------------------------------"

    Private Sub SwitchSidesToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles SwitchSidesToolStripMenuItem.CheckedChanged
        ' Prevent further execution during Form's InitializeComponent()-Method.
        If IsHandleCreated = False Then Exit Sub

        With PnlSidepanelLayout
            If SwitchSidesToolStripMenuItem.Checked Then
                My.Settings.MirrorWindows = True
                PnlSidepanelLayout.Dock = DockStyle.Right
            Else
                My.Settings.MirrorWindows = False
                PnlSidepanelLayout.Dock = DockStyle.Left
            End If

            .Padding = New Padding(.Padding.Right, .Padding.Top, .Padding.Left, .Padding.Bottom)
            PnlLayoutForm.Padding = New Padding(PnlLayoutForm.Padding.Right,
                     PnlLayoutForm.Padding.Top,
                     PnlLayoutForm.Padding.Left,
                     PnlLayoutForm.Padding.Bottom)
        End With
    End Sub

    Private Sub SideChatToolStripMenuItem1_CheckedChanged(sender As Object, e As EventArgs) Handles SideChatToolStripMenuItem1.CheckedChanged
        ' Prevent further execution during Form's InitializeComponent()-Method.
        If IsHandleCreated = False Then Exit Sub

        If SideChatToolStripMenuItem1.Checked = False Then
            My.Settings.SideChat = False
            ToggleAppVisibility(Nothing)
        Else
            My.Settings.SideChat = True
            ToggleAppVisibility(PnlSidechat)
        End If
    End Sub

    Private Sub LazySubAVToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles LazySubAVToolStripMenuItem.CheckedChanged
        ' Prevent further execution during Form's InitializeComponent()-Method.
        If IsHandleCreated = False Then Exit Sub

        If LazySubAVToolStripMenuItem.Checked = True Then
            '#################### Display LazySubAv ###################
            My.Settings.LazySubAV = True
            PNLLazySubAV.BringToFront()
            PnlAvatarInner.SendToBack()
        Else
            '##################### Hide LazySubAv #####################
            My.Settings.LazySubAV = False
            PNLLazySubAV.SendToBack()
            PnlAvatarInner.BringToFront()
        End If
    End Sub

    Private Sub ThemesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ThemesToolStripMenuItem1.Click
        FrmSettings.SettingsTabs.SelectTab(9)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub MaximizeImageToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles MaximizeImageToolStripMenuItem.CheckedChanged
        ' Prevent further execution during Form's InitializeComponent()-Method.
        If IsHandleCreated = False Then Exit Sub

        If MaximizeImageToolStripMenuItem.Checked = False Then
            '########################## Normal ########################
            My.Settings.MaximizeMediaWindow = False
            SplitContainer1.Panel2Collapsed = False
            PnlChatBoxLayout.Visible = True
            SplitContainer1.SplitterDistance = SplitContainer1.Height * 0.75
        Else
            '######################### Maximize #######################
            My.Settings.MaximizeMediaWindow = True
            SplitContainer1.Panel2Collapsed = True
            If PnlSidechat.Visible AndAlso PnlSidepanelLayout.Visible Then PnlChatBoxLayout.Visible = False
        End If

        'SplitContainer1.SplitterDistance = SplitContainer1.Height
    End Sub

    Private Sub SidepanelToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles SidepanelToolStripMenuItem.CheckedChanged
        ' Prevent further execution during Form's InitializeComponent()-Method.
        If IsHandleCreated = False Then Exit Sub

        If SidepanelToolStripMenuItem.Checked Then
            '########################## Display #######################
            PnlSidepanelLayout.Visible = True
            My.Settings.DisplaySidePanel = True

            If PnlSidepanelLayout.Dock = DockStyle.Left Then
                PnlLayoutForm.Padding = New Padding(0,
                                                     PnlLayoutForm.Padding.Top,
                                                     PnlLayoutForm.Padding.Right,
                                                     PnlLayoutForm.Padding.Bottom)
            Else
                PnlLayoutForm.Padding = New Padding(PnlLayoutForm.Padding.Left,
                                                     PnlLayoutForm.Padding.Top,
                                                    0,
                                                     PnlLayoutForm.Padding.Bottom)
            End If
        Else
            '########################### Hide #########################
            PnlSidepanelLayout.Visible = False
            My.Settings.DisplaySidePanel = False

            If PnlSidepanelLayout.Dock = DockStyle.Left Then
                PnlLayoutForm.Padding = New Padding(PnlLayoutForm.Padding.Right,
                                                     PnlLayoutForm.Padding.Top,
                                                     PnlLayoutForm.Padding.Right,
                                                     PnlLayoutForm.Padding.Bottom)
            Else
                PnlLayoutForm.Padding = New Padding(PnlLayoutForm.Padding.Left,
                                                     PnlLayoutForm.Padding.Top,
                                                    PnlLayoutForm.Padding.Left,
                                                     PnlLayoutForm.Padding.Bottom)
            End If

            If MaximizeImageToolStripMenuItem.Checked Then PnlChatBoxLayout.Visible = True
        End If
    End Sub

    Private Sub WebteaseModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebteaseModeToolStripMenuItem.Click
        FrmSettings.WebTeaseMode.Checked = Not FrmSettings.WebTeaseMode.Checked
        WebteaseModeToolStripMenuItem.Checked = Not FrmSettings.WebTeaseMode.Checked
        mySettingsAccessor.WebTeaseModeEnabled = FrmSettings.WebTeaseMode.Checked
    End Sub

    Private Sub DefaultImageSizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultImageSizeToolStripMenuItem.Click
        If SplitContainer1.Height > 430 Then SplitContainer1.SplitterDistance = SplitContainer1.Height - 252
    End Sub

    Private Sub FullscreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullscreenToolStripMenuItem.Click
        If Me.FormBorderStyle <> Windows.Forms.FormBorderStyle.None Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

            Dim WA As Rectangle = Screen.GetBounds(Me)

            Me.Location = New Point(WA.Location.X, WA.Location.Y)
            Me.Size = New Size(WA.Width, WA.Height)

            Me.WindowState = FormWindowState.Normal
            Me.MainMenuStrip.Visible = False
        Else
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            Me.Location = New Point(0, 0)
            Me.Size = New Size(My.Settings.WindowWidth, My.Settings.WindowHeight)
            Me.MainMenuStrip.Visible = True

            RestoreFormPosition()
        End If
    End Sub

    Private Sub RestoreFormPosition()
        Dim WA As Rectangle = Screen.GetWorkingArea(Cursor.Position)

        If My.Settings.WindowWidth = 0 Or My.Settings.WindowHeight = 0 Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.Width = If(WA.Width > Me.Width, My.Settings.WindowWidth, WA.Width)
            Me.Height = If(WA.Height > Me.Height, My.Settings.WindowHeight, WA.Height)
        End If

        Me.Left = WA.Location.X + (WA.Width - Me.Width) / 2
        Me.Top = WA.Location.Y + (WA.Height - Me.Height) / 2
    End Sub

#End Region ' Interface

#Region "------------------------------------------------------- Tools --------------------------------------------------------"

    Private Sub CommandGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommandGuideToolStripMenuItem.Click
        If Form10.Visible = False Then Form10.Show()
        Form10.Focus()
    End Sub

    Private Sub AIBoxesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AIBoxesToolStripMenuItem.Click
        If Form9.Visible = False Then Form9.Show()
        Form9.Focus()
    End Sub

    Private Sub OldDommeTagsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OldDommeTagsToolStripMenuItem.Click
        Form8.Show()
    End Sub

#End Region ' Tools

#Region "------------------------------------------------------ Milovana ------------------------------------------------------"

    Private Sub OpenBetaThreadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenBetaThreadToolStripMenuItem.Click,
                                                                                                            OpenBetaThreadToolStripMenuItem1.Click
        Process.Start("https://milovana.com/forum/viewtopic.php?f=2&t=15776")
    End Sub

    Private Sub BugReportThreadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BugReportThreadToolStripMenuItem.Click,
                                                                                                            BugReportThreadToolStripMenuItem1.Click
        Process.Start("https://milovana.com/forum/viewtopic.php?f=2&t=16203")
    End Sub

    Private Sub WebteasesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebteasesToolStripMenuItem.Click,
                                                                                                        WebteasesToolStripMenuItem1.Click
        Process.Start("https://milovana.com/webteases/")
    End Sub

    Private Sub AllAndEverythingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllAndEverythingToolStripMenuItem.Click,
                                                                                                                ForumToolStripMenuItem.Click
        Process.Start("https://milovana.com/forum/")
    End Sub

#End Region ' Milovana

    Private Sub StartTimer1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartTimer1ToolStripMenuItem.Click
        Timer1.Start()
    End Sub

    Private Sub RunScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunScriptToolStripMenuItem.Click

        If OpenScriptDialog.ShowDialog() = DialogResult.OK Then

            ssh.StrokeTauntVal = -1

            If Directory.Exists(My.Settings.DomImageDir) And ssh.SlideshowLoaded = False Then
                LoadDommeImageFolder()
            End If

            ssh.FileText = OpenScriptDialog.FileName
            ssh.BeforeTease = False
            ssh.ShowModule = True
            mySession.Session.Domme.WasGreeted = True
            ssh.ScriptTick = 1
            ScriptTimer.Start()
        End If
    End Sub

    Private Sub DebugSessionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugSessionWindowToolStripMenuItem.Click
        dbgSessionForm.Show()
    End Sub

    Private Sub DebugMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugMenuToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(13)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

    Private Sub DebugToolStripMenuItem_DropDownOpening(sender As Object, e As EventArgs) Handles DebugToolStripMenuItem.DropDownOpening
        StartTimer1ToolStripMenuItem.Enabled = Not Timer1.Enabled
    End Sub

    Private Sub RefreshRandomizerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshRandomizerToolStripMenuItem.Click
        ssh.randomizer = New Random(DateTime.Now.Ticks Mod Int32.MaxValue)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmSettings.SettingsTabs.SelectTab(14)
        FrmSettings.Show()
        FrmSettings.Focus()
    End Sub

#End Region ' Menu

#End Region

    Public Sub UnpauseScripts()
        mySession.Session.IsScriptPaused = False
    End Sub

    Public Sub PauseScripts()
        mySession.Session.IsScriptPaused = True
    End Sub

#Region "Prepare for extraction to service"
    ''' <summary>
    ''' Updates the chat window with <paramref name="messageString"/>, then optionally saves the log
    ''' </summary>
    ''' <param name="messageString"></param>
    ''' <param name="shouldSave"></param>
    Private Sub AppendChatMessage(messageString As String, shouldSave As Boolean)
        ChatText.DocumentText = messageString
        ChatText2.DocumentText = messageString

        If shouldSave = True Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Chatlogs\Autosave.html", messageString, False)
    End Sub

    Private Function GetStartPostion(makeRandom As Boolean) As Int32
        Dim VideoLength As Integer = DomWMP.currentMedia.duration
        Dim VidLow As Integer = VideoLength * 0.4
        Dim VidHigh As Integer = VideoLength * 0.9
        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

        Return VideoLength - VidPoint
    End Function

    Public Function ReplaceImageTags(messageString As String) As String
        Dim slide As ContactData = ssh.SlideshowMain
        If Not String.IsNullOrWhiteSpace(slide.CurrentImage) Then
            Dim fileName As String = Path.GetDirectoryName(slide.CurrentImage) + "\ImageTags.txt"

            ' This loads tag data from the file, because some tags have options, such as Garment
            If (ssh.SlideshowLoaded And mainPictureBox.Image IsNot Nothing And Not DomWMP.Visible) Then
                Dim tagList = loadFileData.ReadData(fileName) _
                        .OnSuccess(Function(data) parseTagDataService.ParseTagData(data)).GetResultOrDefault(New List(Of TaggedItem))
                Dim imageData = tagList.FirstOrDefault(Function(ti) ti.ItemName = Path.GetFileName(slide.CurrentImage))
                messageString = myImageTagReplaceHash.ReplaceImageTags(messageString, imageData)
            End If
        End If

        Return messageString
    End Function

    ''' <summary>
    ''' This performs find and replace on hashtag strings.
    ''' </summary>
    ''' <param name="messageString"></param>
    ''' <returns></returns>
    Public Function HashTagReplace(messageString As String, domme As DommePersonality) As String
        Dim initialString As String = messageString
        messageString = SysKeywordClean(messageString)

        'Bug: TextedTags have to be applied after the image is displayed.
        ssh.FoundTag = "NULL"
        messageString = ReplaceImageTags(messageString)

        If messageString.Contains("#") Then
            Dim mc As MatchCollection = New Regex("#[#\w\d\+\-_]+", RegexOptions.IgnoreCase).Matches(messageString)
            For Each keyword As Match In mc
                Dim phrases = New VocabularyAccessor().GetVocabulary(domme, keyword.Value) _
                    .Ensure(Function(data) data.Any(), "No vocabulary for " + keyword.Value) _
                    .OnSuccess(Function(data) FilterList(data)) _
                    .OnSuccess(Function(data) messageString.Replace(keyword.Value, data(ssh.randomizer.Next(0, data.Count)))) _
                    .OnFailure(Function(err) messageString = err.Message)
            Next
        End If

        Return messageString
    End Function

    Private Function ToBeMigrated(domme As DommePersonality, message As String) As String
        Dim inputString As String = message
#Region "Risky Pick Game"
        If inputString.Contains(Keyword.ChooseRiskyPick) Then
            mySession.Session.IsScriptPaused = True
            GamesWindow.EnableCases()
            GamesWindow.ClearCaseLabelsOffer()

            inputString = inputString.Replace("@ChooseRiskyPick", "")
        End If

        If inputString.Contains("@CheckRiskyPick") Then
            'FrmCardList.Focus()
            GamesWindow.CheckRiskyPick()
            inputString = inputString.Replace("@CheckRiskyPick", "")
            mySession.Session.IsScriptPaused = True
        End If

        If inputString.Contains("@FinalRiskyPick") Then
            'FrmCardList.Focus()
            GamesWindow.BTNRiskIt.Text = "LAST CASE"
            GamesWindow.BTNPickIt.Text = "MY CASE"
            inputString = inputString.Replace("@FinalRiskyPick", "")
        End If

        If inputString.Contains("@ClearRiskyLabels") Then
            'FrmCardList.Focus()
            GamesWindow.ClearCaseLabelsOffer()
            inputString = inputString.Replace("@ClearRiskyLabels", "")
        End If

        If inputString.Contains("@RiskyPayout") Then
            If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutSmall.wav") Then
                GamesWindow.GameWMP.settings.setMode("loop", False)
                GamesWindow.GameWMP.settings.volume = 20
                GamesWindow.GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutSmall.wav"
            End If
            ssh.BronzeTokens += GamesWindow.TokensPaid
            GamesWindow.LBLRiskTokens.Text = ssh.BronzeTokens
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\RP_Edges", GamesWindow.EdgesOwed, False)
            inputString = inputString.Replace("@RiskyPayout", "")
        End If

        If inputString.Contains("@CloseRiskyPick") Then
            GamesWindow.CloseRiskyPick()
            inputString = inputString.Replace("@CloseRiskyPick", "")
        End If

        If inputString.Contains("@RevealLastCase") Then
            GamesWindow.RevealLastCase()
            inputString = inputString.Replace("@RevealLastCase", "")
        End If

        If inputString.Contains("@RevealUserCase") Then
            GamesWindow.RevealUserCase()
            inputString = inputString.Replace("@RevealUserCase", "")
        End If

        If inputString.Contains("@RiskyState") Then
            If GamesWindow.RiskyState = True Then
                ssh.FileGoto = "(Risky Game)"
            Else
                ssh.FileGoto = "(Risky Tease)"
            End If
            GamesWindow.RiskyState = False
            ssh.SkipGotoLine = True
            inputString = inputString.Replace("@RiskyState", "")
        End If

        inputString = inputString.Replace("#RP_ChosenCase", GamesWindow.RiskyPickChosenCaseNumber)
        inputString = inputString.Replace("#RP_RespondCase", GamesWindow.RiskyPickChosenCaseEdges)
        'inputstring = inputstring.Replace("#RP_CaseNumber", FrmCardList.RiskyCase)
        If GamesWindow.RiskyPickCount = 0 Then inputString = inputString.Replace("#RP_CaseNumber", GamesWindow.SelectedCase1Label.Text)
        If GamesWindow.RiskyPickCount = 1 Then inputString = inputString.Replace("#RP_CaseNumber", GamesWindow.SelectedCase2Label.Text)
        If GamesWindow.RiskyPickCount = 2 Then inputString = inputString.Replace("#RP_CaseNumber", GamesWindow.SelectedCase3Label.Text)
        If GamesWindow.RiskyPickCount = 3 Then inputString = inputString.Replace("#RP_CaseNumber", GamesWindow.SelectedCase4Label.Text)
        If GamesWindow.RiskyPickCount = 4 Then inputString = inputString.Replace("#RP_CaseNumber", GamesWindow.SelectedCase5Label.Text)
        If GamesWindow.RiskyPickCount > 4 Then inputString = inputString.Replace("#RP_CaseNumber", GamesWindow.SelectedCase6Label.Text)
        inputString = inputString.Replace("#RP_EdgeOffer", GamesWindow.RiskyPickOffer.Edges)
        inputString = inputString.Replace("#RP_TokenOffer", GamesWindow.RiskyPickOffer.Tokens)
        inputString = inputString.Replace("#RP_EdgesOwed", GamesWindow.EdgesOwed)
        inputString = inputString.Replace("#RP_TokensPaid", GamesWindow.TokensPaid)
#End Region

        Return inputString
    End Function

    Public Function PerformCommands(inputString As String, shouldTaskClean As Boolean) As String
#Region "Ignore this"
        'ssh.FileGoto = "(Sub Not Stroking)"
        'ssh.SkipGotoLine = True
        'GetGoto("(Sub Not Stroking)")

        If shouldTaskClean Then
            GoTo TaskCleanSet
        End If

RinseLatherRepeat:

        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        '									ImageCommands
        ' - Make sure you call all Display ImageFunctions before executing @LockImages.
        '	If you don't, FilterList() will return a wrong list of lines =>
        '		=> The Domme is talking about an image, but she never showed one.
        '		=> She is talking about an new image, but never showed one.
        ' - Call @DeleteLocalImage before you start to display a new Image, because they 
        '	are loaded and displayed async. Otherwise it will delete the wrong image!
        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

        ' @DeleteLocalImage: Deletes the current displayed local image from filesystem, 
        ' LiskedList, DislikedList and LocalImageTagList,  if the  current Image is 
        ' not an image in the Domme- or Contacts-Image directory or their subdirectories.
        If inputString.Contains("@DeleteLocalImage") Then
            If mySettingsAccessor.CanDommeDeleteFiles Then
                Try
                    DeleteCurrentImage(True)
                Catch ex As Exception
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    '                   All Errors
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    Log.WriteError("Command @DeleteLocalImage was unable to delete the image.",
                                   ex, "@DeleteLocalImage failed")
                End Try
            End If
            inputString = inputString.Replace("@DeleteLocalImage", "")
        End If

        ' @DeleteImage: Deletes the current displayed image from filesystem, LiskedList, 
        ' DislikedList, LocalImageTagList and URL-Files, if the  current Image is 
        ' not an image in the Domme- or Contacts-Image directory or their subdirectories.
        If inputString.Contains("@DeleteImage") Then
            If mySettingsAccessor.CanDommeDeleteFiles Then
                Try
                    DeleteCurrentImage(False)
                Catch ex As Exception
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    '                   All Errors
                    '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                    Log.WriteError("Command @DeleteImage was unable to delete the image.",
                                   ex, "@DeleteImage failed")
                End Try
            End If
            inputString = inputString.Replace("@DeleteImage", "")
        End If

        ' The @UnlockImages Command allows the Domme Slideshow to resume functioning as normal.
        If inputString.Contains("@UnlockImages") Then
            If ssh.SlideshowLoaded = True Then
                ImageSlideShowNextButton.Enabled = True
                ImageSlideShowPreviousButton.Enabled = True
                PicStripTSMIdommeSlideshow.Enabled = True
            End If
            ssh.LockImage = False
            inputString = inputString.Replace("@UnlockImages", "")
        End If

        If inputString.Contains("@DommeTag(") Then
            Dim TagFlag As String = GetParentheses(inputString, "@DommeTag(")
            'QND-Implemented: ContactData.GetTaggedImage
            If ContactToUse IsNot Nothing Then
                ssh.DommeImageSTR = ContactToUse.GetTaggedImage(TagFlag, True)
            Else
                ssh.DommeImageSTR = ""
            End If
            ' Clean the Text.
            inputString = inputString.Replace("@DommeTag(" & TagFlag & ")", "")
        End If

        If inputString.Contains("@NewDommeSlideshow") Then
            'TODO: Add Support for contact slideshows.
            ssh.SlideshowMain.LoadNew()
            ssh.SlideshowMain.CurrentImage()
            inputString = inputString.Replace("@NewDommeSlideshow", "")
        End If

        If inputString.Contains("@DomTag(") Then
            Dim TagFlag As String = GetParentheses(inputString, "@DomTag(")
            ' Try to get a Domme Image for the given Tags.
            'QND-Implemented: ContactData.GetTaggedImage
            If ContactToUse IsNot Nothing Then
                ssh.DommeImageSTR = ContactToUse.GetTaggedImage(TagFlag, True)
            Else
                ssh.DommeImageSTR = ""
            End If

            inputString = inputString.Replace("@DomTag(" & TagFlag & ")", "")
        End If

        If inputString.Contains("@ImageTag(") Then
            Dim TagFlag As String = GetParentheses(inputString, "@ImageTag(")
            ShowImage(GetLocalImage(TagFlag), False)
            inputString = inputString.Replace("@ImageTag(" & TagFlag & ")", "")
        End If

        If inputString.Contains("@ShowLocalImage") And Not inputString.Contains("@ShowLocalImage(") Then
            ShowImage(GetRandomImage(ImageSourceType.Local), False)
            inputString = inputString.Replace("@ShowLocalImage", "")
        End If

        '===============================================================================
        '								@ShowLocalImage()
        '===============================================================================
        If inputString.Contains("@ShowLocalImage(") Then
            Dim LocalFlag As String = GetParentheses(inputString, "@ShowLocalImage(")
            LocalFlag = FixCommas(LocalFlag)

            Dim tmpListGenre As List(Of String) = LocalFlag.Split(",").ToList

            If LocalFlag.ToUpper.Contains("NOT") Then
                ' =============== Invert the Content in Brackets ===============
                ' Declare a String containing all available ImageGenres
                Dim CompareFlag As String = "Hardcore, Softcore, Lesbian, Blowjob, Femdom, Lezdom, Hentai, Gay, Maledom, Captions, General, Butts, Boobs"

                ' Remove Imagegenre, when there are no local Images available or it is in the inverting bracket
                For i As Integer = tmpListGenre.Count - 1 To 0 Step -1
                    If tmpListGenre(i).ToUpper.Contains("HARDCORE") Or Not GetImageData(ImageGenre.Hardcore).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Hardcore, ", "")
                    If tmpListGenre(i).ToUpper.Contains("SOFTCORE") Or Not GetImageData(ImageGenre.Softcore).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Softcore, ", "")
                    If tmpListGenre(i).ToUpper.Contains("LESBIAN") Or Not GetImageData(ImageGenre.Lesbian).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Lesbian, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BLOWJOB") Or Not GetImageData(ImageGenre.Blowjob).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Blowjob, ", "")
                    If tmpListGenre(i).ToUpper.Contains("FEMDOM") Or Not GetImageData(ImageGenre.Femdom).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Femdom, ", "")
                    If tmpListGenre(i).ToUpper.Contains("LEZDOM") Or Not GetImageData(ImageGenre.Lezdom).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Lezdom, ", "")
                    If tmpListGenre(i).ToUpper.Contains("HENTAI") Or Not GetImageData(ImageGenre.Hentai).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Hentai, ", "")
                    If tmpListGenre(i).ToUpper.Contains("GAY") Or Not GetImageData(ImageGenre.Gay).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Gay, ", "")
                    If tmpListGenre(i).ToUpper.Contains("MALEDOM") Or Not GetImageData(ImageGenre.Maledom).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Maledom, ", "")
                    If tmpListGenre(i).ToUpper.Contains("CAPTIONS") Or Not GetImageData(ImageGenre.Captions).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Captions, ", "")
                    If tmpListGenre(i).ToUpper.Contains("GENERAL") Or Not GetImageData(ImageGenre.General).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("General, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BUTT") Or Not GetImageData(ImageGenre.Butt).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Butts, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BUTTS") Then CompareFlag = CompareFlag.Replace("Butts, ", "")
                    If tmpListGenre(i).ToUpper.Contains("BOOB") Or Not GetImageData(ImageGenre.Boobs).IsAvailable(ImageSourceType.Local) Then CompareFlag = CompareFlag.Replace("Boobs", "")
                    If tmpListGenre(i).ToUpper.Contains("BOOBS") Then CompareFlag = CompareFlag.Replace("Boobs", "")
                Next

                ' Set the inverted Array.
                tmpListGenre = CompareFlag.Split(", ").ToList
            End If

            ' generate a list of all available Local Images. This way it is most 
            ' likely, to get an image.
            Dim tmpImageLocationList As New List(Of String)

            For Each tmpStr As String In tmpListGenre
                If tmpStr.ToUpper.Contains("HARDCORE") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Hardcore).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("SOFTCORE") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Softcore).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("LESBIAN") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Lesbian).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("BLOWJOB") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Blowjob).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("FEMDOM") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Femdom).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("LEZDOM") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Lezdom).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("HENTAI") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Hentai).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("GAY") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Gay).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("MALEDOM") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Maledom).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("CAPTION") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Captions).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("GENERAL") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.General).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("BUTT") Or tmpStr.ToUpper.Contains("BUTTS") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Butt).ToList(ImageSourceType.Local))
                ElseIf tmpStr.ToUpper.Contains("BOOB") Or tmpStr.ToUpper.Contains("BOOBS") Then
                    tmpImageLocationList.AddRange(GetImageData(ImageGenre.Boobs).ToList(ImageSourceType.Local))
                End If
            Next
            ' Declare a string for the Image to show - initialize with error Image
            Dim tmpImgToShow As String = Application.StartupPath & "\Images\System\NoLocalImagesFound.jpg"
            ' If there are images, overwrite the error image.
            If tmpImageLocationList.Count > 0 Then
                tmpImgToShow = tmpImageLocationList(New Random().Next(0, tmpImageLocationList.Count))
            Else
                Trace.WriteLine("failed to execute Command: @ShowLocalImage(" & LocalFlag & ") No images found.")
            End If

            ShowImage(tmpImgToShow, False)

            inputString = inputString.Replace("@ShowLocalImage(" & GetParentheses(inputString, "@ShowLocalImage(") & ")", "")
        End If
        '----------------------------------------
        ' @ShowLocalImage()- End
        '----------------------------------------
        '===============================================================================
        '								@ShowTaggedImage
        '===============================================================================
        If inputString.Contains("@ShowTaggedImage") Then
            Dim Tags As List(Of String) = inputString.Split() _
                                    .Select(Function(s) s.Trim()) _
                                    .Where(Function(w) CType(w, String).StartsWith("@Tag")).ToList

            Dim foundString As String = GetLocalImage(Tags, Nothing)

            'TODO: @ShowTaggedImage - Add a dedicated ErrorImage when there are no tagged images.
            If String.IsNullOrWhiteSpace(foundString) Then foundString = myPathsAccessor.PathImageErrorNoLocalImages

            ssh.JustShowedBlogImage = True
            ShowImage(foundString, False)

            Tags.ForEach(Sub(x) inputString = inputString.Replace(x, ""))
            inputString = inputString.Replace("@ShowTaggedImage", "")
        End If
        '----------------------------------------
        ' @ShowTaggedImage - End
        '----------------------------------------
        '===============================================================================
        '									@ShowImage[]
        '===============================================================================
        If inputString.Contains("@ShowImage[") Then
            Dim ImageToShow As String = GetParentheses(inputString, "@ShowImage[")
            Try
                Dim tmpImgLoc As String = ""

                If IsUrl(ImageToShow) Then
                    '########################## ImageURL was given #########################
                    tmpImgLoc = ImageToShow
                    GoTo ShowedBlogImage
                End If

                ' Change evtl. wrong given Slashes
                ImageToShow = ImageToShow.Replace("/", "\")

                ImageToShow = Application.StartupPath & "\Images\" & ImageToShow
                ImageToShow = ImageToShow.Replace("\\", "\")

                If ImageToShow.Contains("*") Then
                    '######################### Directory was given #########################
                    Dim tmpFilter As String = Path.GetFileName(ImageToShow)
                    Dim tmpDir As String = Path.GetDirectoryName(ImageToShow)
                    Dim ImageList As List(Of String)

                    If Directory.Exists(tmpDir) = False Then
                        Throw New Exception(
                         "The given directory """ & tmpDir & """ does not exist." &
                         vbCrLf & vbCrLf &
                         "Please make sure the directory exists and it is spelled correctly in the script.")
                    End If

                    If tmpFilter = "*" Then
                        ImageList = myDirectory.GetFilesImages(tmpDir)
                    Else
                        ImageList = Directory.GetFiles(tmpDir, tmpFilter, SearchOption.TopDirectoryOnly).ToList
                    End If

                    If ImageList.Count = 0 Then
                        Throw New FileNotFoundException(
                         "No images matching the filter """ & tmpFilter &
                         """ were found in """ & tmpDir & """!" &
                         vbCrLf & vbCrLf &
                         "Please make sure that valid files exist and the wildcards are applied correctly in the script.")
                    End If

                    tmpImgLoc = ImageList(New Random().Next(0, ImageList.Count))
                Else
                    '############################# Single Image ############################
                    If File.Exists(ImageToShow) Then
                        tmpImgLoc = ImageToShow
                    Else
                        Throw New Exception(
                         """" & Path.GetFileName(ImageToShow) & """ was not found in """ & Path.GetDirectoryName(ImageToShow) & """!" &
                         vbCrLf & vbCrLf &
                         "Please make sure the file exists and it is spelled correctly in the script.")
                    End If
                End If
                '############### Display the Image ##################
ShowedBlogImage:
                ShowImage(tmpImgLoc, False)
            Catch ex As Exception
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                '                   All Errors
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Log.WriteError("Command @ShowImage[] was unable to display the image.",
                   ex, "Error at @ShowImage[]")
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error at @ShowImage[]")
            End Try
            inputString = inputString.Replace("@ShowImage[" & GetParentheses(inputString, "@ShowImage[") & "]", "")
        End If
        '----------------------------------------
        ' @ShowImage[]- End
        '----------------------------------------
        '===============================================================================
        '								Legacy TnA-Slideshow
        '===============================================================================
        ' TODO: Rework TnA-Game to use CustomSlideshow instead of its own code.
        ' @TnAFastSlides starts a fast slideshow with Boobs and Butts. Use with local images, to avoid the download delay. otherwise the images will stutter.
        ' @TnASlides starts a slideshow with boobs and butts. the Speed is fixed at 1 image per second.
        ' @TnASlowSlides starts a slideshow with boobs and butts. the Speed is fixed at 1 image per 5 seconds.

        If inputString.Contains("@TnAFastSlides") Or inputString.Contains("@TnASlowSlides") Or inputString.Contains("@TnASlides") Then
            If inputString.Contains("@TnAFastSlides") Then TnASlides.Interval = 334
            If inputString.Contains("@TnASlides") Then TnASlides.Interval = 1000
            If inputString.Contains("@TnASlowSlides") Then TnASlides.Interval = 5000

            Try
                ssh.BoobList.Clear()
                ssh.AssList.Clear()

                If ssh.BoobList.Count < 1 Then ssh.BoobList = GetImageData(ImageGenre.Boobs).ToList
                If ssh.AssList.Count < 1 Then ssh.AssList = GetImageData(ImageGenre.Butt).ToList

                If ssh.BoobList.Count < 1 Then Throw New Exception("No Boobs-images found.")
                If ssh.AssList.Count < 1 Then Throw New Exception("No Butt-images found.")

                TnASlides.Start()
            Catch ex As Exception
                Log.WriteError("Unable to start TnA Slideshow: " & vbCrLf &
                      ex.Message, ex, "CommandClean()")
            End Try

            inputString = inputString.Replace("@TnAFastSlides", "")
            inputString = inputString.Replace("@TnASlowSlides", "")
            inputString = inputString.Replace("@TnASlides", "")
        End If

        If inputString.Contains("@CheckTnA") Then
            TnASlides.Stop()

            If ssh.AssImage = True Then ssh.FileGoto = "(Butt)"
            If ssh.BoobImage = True Then ssh.FileGoto = "(Boobs)"
            ssh.SkipGotoLine = True
            GetGoto()
            inputString = inputString.Replace("@CheckTnA", "")
        End If

        If inputString.Contains("@StopTnA") Then
            TnASlides.Stop()
            ssh.BoobList.Clear()
            ssh.BoobImage = False
            ssh.AssList.Clear()
            ssh.AssImage = False
            inputString = inputString.Replace("@StopTnA", "")
        End If
        '----------------------------------------
        ' TnA-Slideshow - End
        '----------------------------------------
        '===============================================================================
        '								Slideshow
        '===============================================================================
        If inputString.Contains("@Slideshow(") Then
            Dim SlideFlag As String = inputString

            Dim SlideStart As Integer

            SlideStart = SlideFlag.IndexOf("@Slideshow(") + 11
            SlideFlag = SlideFlag.Substring(SlideStart, SlideFlag.Length - SlideStart)
            SlideFlag = SlideFlag.Split(")")(0)
            SlideFlag = SlideFlag.Replace("@Slideshow(", "")

            ssh.CustomSlideshow.Clear()

            If SlideFlag.ToLower.Contains("hardcore") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Hardcore).ToList(), ImageGenre.Hardcore)
            End If

            If SlideFlag.ToLower.Contains("softcore") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Softcore).ToList(), ImageGenre.Softcore)
            End If

            If SlideFlag.ToLower.Contains("lesbian") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Lesbian).ToList(), ImageGenre.Lesbian)
            End If

            If SlideFlag.ToLower.Contains("blowjob") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Blowjob).ToList(), ImageGenre.Blowjob)
            End If

            If SlideFlag.ToLower.Contains("femdom") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Femdom).ToList(), ImageGenre.Femdom)
            End If

            If SlideFlag.ToLower.Contains("lezdom") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Lezdom).ToList(), ImageGenre.Lezdom)
            End If

            If SlideFlag.ToLower.Contains("hentai") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Hentai).ToList(), ImageGenre.Hentai)
            End If

            If SlideFlag.ToLower.Contains("gay") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Gay).ToList(), ImageGenre.Gay)
            End If

            If SlideFlag.ToLower.Contains("maledom") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Maledom).ToList(), ImageGenre.Maledom)
            End If

            If SlideFlag.ToLower.Contains("captions") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Captions).ToList(), ImageGenre.Captions)
            End If

            If SlideFlag.ToLower.Contains("general") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.General).ToList(), ImageGenre.General)
            End If

            If SlideFlag.ToLower.Contains("boob") Or LCase(SlideFlag).Contains("boobs") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Boobs).ToList(), ImageGenre.Boobs)
            End If

            If SlideFlag.ToLower.Contains("butt") Or LCase(SlideFlag).Contains("butts") Then
                ssh.CustomSlideshow.AddRange(GetImageData(ImageGenre.Butt).ToList(), ImageGenre.Butt)
            End If


            CustomSlideshowTimer.Interval = 1000
            If LCase(SlideFlag).Contains("slow") Then CustomSlideshowTimer.Interval = 5000
            If LCase(SlideFlag).Contains("fast") Then CustomSlideshowTimer.Interval = 500


            inputString = inputString.Replace("@Slideshow(" & SlideFlag & ")", "")
        End If

        If inputString.Contains("@SlideshowOn") Then
            If ssh.CustomSlideshow.Count > 0 Then
                ssh.CustomSlideEnabled = True
                CustomSlideshowTimer.Start()
            End If
            inputString = inputString.Replace("@SlideshowOn", "")
        End If

        If inputString.Contains("@SlideshowOff") Then
            ssh.CustomSlideEnabled = False
            CustomSlideshowTimer.Stop()
            inputString = inputString.Replace("@SlideshowOff", "")
        End If

        If inputString.Contains("@SlideshowFirst") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.FirstImage, False)
            inputString = inputString.Replace("@SlideshowFirst", "")
        End If

        If inputString.Contains("@SlideshowLast") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.LastImage, False)
            inputString = inputString.Replace("@SlideshowLast", "")
        End If

        If inputString.Contains("@SlideshowNext") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.NextImage, False)
            inputString = inputString.Replace("@SlideshowNext", "")
        End If

        If inputString.Contains("@SlideshowPrevious") Then
            ssh.CustomSlideEnabled = True
            ShowImage(ssh.CustomSlideshow.PreviousImage, False)
            inputString = inputString.Replace("@SlideshowPrevious", "")
        End If

        If inputString.Contains("@GotoSlideshow") Then
            Dim ImageString As String = ssh.CustomSlideshow.CurrentImage

            If ImageString IsNot Nothing OrElse ImageString = "" Then
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Hardcore Then ssh.FileGoto = "(Hardcore)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Softcore Then ssh.FileGoto = "(Softcore)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Lesbian Then ssh.FileGoto = "(Lesbian)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Blowjob Then ssh.FileGoto = "(Blowjob)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Femdom Then ssh.FileGoto = "(Femdom)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Lezdom Then ssh.FileGoto = "(Lezdom)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Hentai Then ssh.FileGoto = "(Hentai)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Gay Then ssh.FileGoto = "(Gay)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Maledom Then ssh.FileGoto = "(Maledom)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Captions Then ssh.FileGoto = "(Captions)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.General Then ssh.FileGoto = "(General)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Boobs Then ssh.FileGoto = "(Boobs)"
                If ssh.CustomSlideshow(ImageString) = ImageGenre.Butt Then ssh.FileGoto = "(Butts)"


                ssh.SkipGotoLine = True
                GetGoto()
            Else
                Dim lazytext As String = "@GotoSlideshow can't determine the current CustomSlideshow image. Please make sure to start it before using @GotoSlideshow."
                Log.WriteError(lazytext, New NullReferenceException(lazytext), "@GotoSlideshow")
            End If

            inputString = inputString.Replace("@GotoSlideshow", "")
        End If
        '----------------------------------------
        ' Slideshow - End
        '----------------------------------------
        ' This Command will not work in the same line, because the Images are loaded async and not available yet.
        If inputString.Contains("@CurrentImage") Then inputString = inputString.Replace("@CurrentImage", ssh.ImageLocation)

        ' The @LockImages Commnd prevents the Domme Slideshow from moving forward or back when set to "Tease" or "Timed". Manual operation of Domme Slideshow images is still allowed,
        ' and pictures displayed through other means will still work. Images are automatically unlocked whenever Tease AI moves into a Link script, an End script, any Interrupt occurs
        ' (including Long Edge and Start Stroking) or when the sub gives up.

        If inputString.Contains("@LockImages") Then
            ssh.LockImage = True
            ImageSlideShowNextButton.Enabled = False
            ImageSlideShowPreviousButton.Enabled = False
            PicStripTSMIdommeSlideshow.Enabled = False
            inputString = inputString.Replace("@LockImages", "")
        End If
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        '			ImageCommands - End
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


TaskCleanSet:


        ' The @SetVar[] Command is used to set a Variable and store it in System\Variables. The syntax for using @SetVar[] is @SetVar[VariableName]=[Value].
        ' For example, @SetVar[MyNumber]=[12] would save the Variable "MyNumber" as a value of 12. You can also set string Variables this way, such as @SetVar[MyString]=[lasagna]
        ' Multiple @SetVar[] Commands may be used per line if you wish.
        ' Variable names CANNOT contain spaces or any character not supported by Windows file naming conventions \ / : * ? " < > |

        If inputString.Contains("@SetVar[") Then

            Dim VarArray As String() = inputString.Split

            For i As Integer = 0 To VarArray.Count - 1

                Dim SCGotVar As String = "NULL"

                If VarArray(i).Contains("@SetVar[") Then
                    SCGotVar = VarArray(i)
                    VarArray(i) = ""

                    SCGotVar = SCGotVar.Replace("@SetVar[", "")

                    Dim SCGotVarSplit As String() = Split(SCGotVar, "]")

                    Dim VarName As String = SCGotVarSplit(0)

                    SCGotVarSplit(0) = ""

                    SCGotVar = Join(SCGotVarSplit)

                    SCGotVar = SCGotVar.Replace("=[", "")
                    SCGotVar = SCGotVar.Replace(" ", "")

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName, SCGotVar, False)

                End If

            Next

            inputString = Join(VarArray)

        End If

        ' The @SetDate() Command allows you to set a time and date that's a specified amount of time in the future from the current time and date. Correct format is @SetDate(VarName, TimeAmount) .
        ' For example, @SetDate(EdgingStop, 1 Hour) would set a Variable called "EdgingStop" whose value is 1 hour away from the current time and date. As another example, @SetDate(NextOrgasmChance, 2 Weeks)
        ' would create a Variable called "NextOrgasmChance" whose value is 2 weeks from the current date.
        ' The available time increments are - Seconds, Minutes, Hours, Days, Weeks, Months and Years. When designating an amount of time, capitalization and pluralization do not matter. If no increment is
        ' specified, "Days" will be used.

        If inputString.Contains("@SetDate(") Then

            Dim CheckArray As String() = inputString.Split(")")
            Dim OriginalCheck As String

            For i As Integer = 0 To CheckArray.Count - 1

                If CheckArray(i).Contains("@SetDate(") Then

                    'CheckArray(i) = CheckArray(i) & "]"

                    Dim CheckFlag As String = GetParentheses(CheckArray(i), "@SetDate(")
                    OriginalCheck = CheckFlag

                    CheckFlag = CheckFlag.Replace(", ", ",")
                    CheckFlag = CheckFlag.Replace(" ,", ",")

                    Dim FlagArray() As String = CheckFlag.Split(",")

                    Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)

                    If UCase(FlagArray(1)).Contains(UCase("SECOND")) Then SetDate = DateAdd(DateInterval.Second, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("MINUTE")) Then SetDate = DateAdd(DateInterval.Minute, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("HOUR")) Then SetDate = DateAdd(DateInterval.Hour, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("DAY")) Then SetDate = DateAdd(DateInterval.Day, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("WEEK")) Then SetDate = DateAdd(DateInterval.Day, Val(FlagArray(1)) * 7, SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("MONTH")) Then SetDate = DateAdd(DateInterval.Month, Val(FlagArray(1)), SetDate)
                    If UCase(FlagArray(1)).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Year, Val(FlagArray(1)), SetDate)

                    If Not UCase(FlagArray(1)).Contains(UCase("SECOND")) And Not UCase(FlagArray(1)).Contains(UCase("MINUTE")) And Not UCase(FlagArray(1)).Contains(UCase("HOUR")) _
                     And Not UCase(FlagArray(1)).Contains(UCase("DAY")) And Not UCase(FlagArray(1)).Contains(UCase("WEEK")) And Not UCase(FlagArray(1)).Contains(UCase("MONTH")) _
                     And Not UCase(FlagArray(1)).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Day, Val(FlagArray(1)), SetDate)

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & FlagArray(0), FormatDateTime(SetDate, DateFormat.GeneralDate), False)

                    ' CheckArray(i) = CheckArray(i).Replace("@SetDate(" & OriginalCheck, "")

                    inputString = inputString.Replace("@SetDate(" & OriginalCheck & ")", "")

                End If

            Next

            'StringClean = Join(CheckArray, Nothing)

        End If


        ' The @RoundVar Command is used to take an existing Variable and round it by the amount specified. The correct format is @Round[VarName]=[RoundAmount]
        ' For example, @RoundVar[StrokeTotal]=[10] wound round the Variable "StrokeTotal" by 10.
        ' @Round[] will only round the and save Variable, it will not display it. More than one @Round[] Command can be used per line


        If inputString.Contains("@RoundVar[") Then

            Dim VarArray As String() = inputString.Split

            For i As Integer = 0 To VarArray.Count - 1

                Dim SCGotVar As String = "NULL"

                If VarArray(i).Contains("@RoundVar[") Then
                    SCGotVar = VarArray(i)
                    VarArray(i) = ""
                End If

                SCGotVar = SCGotVar.Replace("@RoundVar[", "")

                Dim SCGotVarSplit As String() = Split(SCGotVar, "]")

                Dim VarName As String = SCGotVarSplit(0)
                Dim Val1 As Integer

                Dim VarCheck As String = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName


                'TODO: Remove unsecure IO.Access to file, for there is no DirectoryCheck.
                If File.Exists(VarCheck) Then
                    ' Read first line of the given file.
                    Val1 = CInt(TxtReadLine(VarCheck))

                    SCGotVarSplit(0) = ""

                    SCGotVar = Join(SCGotVarSplit)

                    SCGotVar = SCGotVar.Replace("=[", "")
                    SCGotVar = SCGotVar.Replace(" ", "")

                    Dim VarValue As Integer = Val(SCGotVar)

                    Val1 = VarValue * Math.Round(Val1 / VarValue)

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & VarName, Val1, False)

                End If

                ' StringClean = StringClean.Replace("@RoundVar[" & OriginalCheck & ")", "")

            Next

            inputString = Join(VarArray)

        End If

        ' The @ChangeVar[] Command is used to Value of a new or existing Variable and round it by the amount specified. The correct format is @ChangeVar[VarName]=[Value1]+[Value2]
        ' For example, @ChangeVar[StrokeTotal]=[StrokeTotal]+[100] would add 100 to the current value of "StrokeTotal" and save it. If "StrokeTotal" did not previously exist, then it would be created
        ' with a value of 100 in this case, since nothing + 100 equals 100. You can use @ChangeVar[] to add, subtract, multiply or divide with the operators +, -, * and /
        'More than one @ChangeVar[] Command can be used per line.

        If inputString.Contains("@ChangeVar[") Then

            Dim ChangeArray As String() = inputString.Split

            For i As Integer = 0 To ChangeArray.Count - 1

                If ChangeArray(i).Contains("@ChangeVar[") Then

                    Dim ChangeFlag As String = ChangeArray(i)
                    Dim ChangeStart As Integer = ChangeFlag.IndexOf("@ChangeVar[") + 11

                    Dim ChangeVar As String
                    Dim ChangeVal1 As String
                    Dim ChangeVal2 As String
                    Dim ChangeOperator As String

                    Dim Val1 As Integer
                    Dim Val2 As Integer

                    ChangeFlag = ChangeArray(i).Substring(ChangeStart, ChangeArray(i).Length - ChangeStart)
                    ChangeVar = ChangeFlag.Split("]")(0)
                    ChangeVal1 = ChangeFlag.Split("]")(1)
                    ChangeVal2 = ChangeFlag.Split("]")(2)
                    ChangeOperator = ChangeFlag.Split("]")(2)

                    ChangeArray(i) = ChangeArray(i).Replace("@ChangeVar[" & ChangeVar & "]" & ChangeVal1 & "]" & ChangeVal2 & "]", "")

                    ChangeVar = ChangeVar.Replace("@ChangeVar[", "")
                    ChangeVal1 = ChangeVal1.Replace("=[", "")
                    ChangeVal2 = ChangeVal2.Replace("+[", "")
                    ChangeVal2 = ChangeVal2.Replace("-[", "")
                    ChangeVal2 = ChangeVal2.Replace("*[", "")
                    ChangeVal2 = ChangeVal2.Replace("/[", "")

                    '@ChangeVar[TB_EdgeHoldingOwed   ]    =[TB_EdgeHoldingOwed    ]     -[1       ]

                    If IsNumeric(ChangeVal1) = False Then
                        'TODO: Remove unsecure IO.Access to file, for there is no DirectoryCheck.
                        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal1) Then
                            Val1 = TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal1)
                        Else
                            Val1 = 0
                        End If
                    Else
                        Val1 = Val(ChangeVal1)
                    End If

                    If IsNumeric(ChangeVal2) = False Then
                        'TODO: Remove unsecure IO.Access To file, for there is no DirectoryCheck.
                        If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal2) Then
                            Val2 = TxtReadLine(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVal2)
                        Else
                            Val2 = 0
                        End If
                    Else
                        Val2 = Val(ChangeVal2)
                    End If

                    ssh.ScriptOperator = "Null"
                    If ChangeOperator.Contains("+") Then ssh.ScriptOperator = "Add"
                    If ChangeOperator.Contains("-") Then ssh.ScriptOperator = "Subtract"
                    If ChangeOperator.Contains("*") Then ssh.ScriptOperator = "Multiply"
                    If ChangeOperator.Contains("/") Then ssh.ScriptOperator = "Divide"

                    Dim ChangeVal As Integer = 0

                    If ssh.ScriptOperator = "Add" Then ChangeVal = Val1 + Val2
                    If ssh.ScriptOperator = "Subtract" Then ChangeVal = Val1 - Val2
                    If ssh.ScriptOperator = "Multiply" Then ChangeVal = Val1 * Val2
                    If ssh.ScriptOperator = "Divide" Then ChangeVal = Val1 / Val2

                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\" & ChangeVar, ChangeVal, False)

                End If

            Next

        End If

        ' The @ShowVar[] Command is used to show the value of an existing Variable. The correct format is @ShowVar[VarName]
        ' More than one @ShowVar[] Commands can be used per line

        If inputString.Contains("@ShowVar[") Then

            Dim VarSplit As String() = inputString.Split("]")

            For i As Integer = 0 To VarSplit.Count - 1

                If VarSplit(i).Contains("@ShowVar[") Then

                    Dim VarString As String = VarSplit(i) & "]"

                    Dim VarFlag As String = GetParentheses(VarString, "@ShowVar[")
                    Dim VarFlag2 As String = GetVariable(VarFlag)
                    inputString = inputString.Replace("@ShowVar[" & VarFlag & "]", VarFlag2)

                End If

            Next

        End If

        If inputString.Contains("@RemoveTokens(") Then

            Dim TokenFlag As String = GetParentheses(inputString, "@RemoveTokens(")
            TokenFlag = FixCommas(TokenFlag)
            Dim TokenRemove As Integer

            If TokenFlag.Contains(",") Then
                Dim TokenArray As String() = TokenFlag.Split(",")
                For i As Integer = 0 To TokenArray.Count - 1
                    TokenRemove = Val(TokenArray(i))
                    If UCase(TokenArray(i)).Contains("B") Then ssh.BronzeTokens -= TokenRemove
                    If UCase(TokenArray(i)).Contains("S") Then ssh.SilverTokens -= TokenRemove
                    If UCase(TokenArray(i)).Contains("G") Then ssh.GoldTokens -= TokenRemove
                Next
            Else
                TokenRemove = Val(TokenFlag)
                If UCase(TokenFlag).Contains("B") Then ssh.BronzeTokens -= TokenRemove
                If UCase(TokenFlag).Contains("S") Then ssh.SilverTokens -= TokenRemove
                If UCase(TokenFlag).Contains("G") Then ssh.GoldTokens -= TokenRemove
            End If

            If ssh.BronzeTokens < 0 Then ssh.BronzeTokens = 0
            If ssh.SilverTokens < 0 Then ssh.SilverTokens = 0
            If ssh.GoldTokens < 0 Then ssh.GoldTokens = 0

            My.Settings.BronzeTokens = ssh.BronzeTokens
            My.Settings.SilverTokens = ssh.SilverTokens
            My.Settings.GoldTokens = ssh.GoldTokens


            inputString = inputString.Replace("@RemoveTokens(" & TokenFlag & ")", "")

        End If

        If inputString.Contains("@Add1Token") Then
            ssh.BronzeTokens += 1
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 1 Bronze token!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@Add1Token", "")
        End If

        If inputString.Contains("@Add3Tokens") Then
            ssh.BronzeTokens += 3
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 3 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@Add3Tokens", "")
        End If

        If inputString.Contains("@Add5Tokens") Then
            ssh.BronzeTokens += 5
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            inputString = inputString.Replace("@Add5Tokens", "")
            MessageBox.Show(Me, domName.Text & " has given you 5 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If inputString.Contains("@Add10Tokens") Then
            ssh.BronzeTokens += 10
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 10 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@Add10Tokens", "")
        End If

        If inputString.Contains("@Add25Tokens") Then
            ssh.BronzeTokens += 25
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 25 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@Add25Tokens", "")
        End If

        If inputString.Contains("@Add50Tokens") Then
            ssh.BronzeTokens += 50
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 50 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@Add50Tokens", "")
        End If

        If inputString.Contains("@Add100Tokens") Then
            ssh.BronzeTokens += 100
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has given you 100 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@Add50Tokens", "")
        End If

        If inputString.Contains("@Remove100Tokens") Then
            ssh.BronzeTokens -= 100
            My.Settings.BronzeTokens = ssh.BronzeTokens
            GamesWindow.UpdateBronzeTokens()
            MessageBox.Show(Me, domName.Text & " has taken 100 Bronze tokens!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputString = inputString.Replace("@@Remove100Tokens", "")
        End If


        If inputString.Contains("@UpdateOrgasm") Then
            My.Settings.LastOrgasm = FormatDateTime(Now, DateFormat.ShortDate)

            'Github Patch
            If My.Settings.OrgasmsLocked = True Then My.Settings.OrgasmsRemaining -= 1

            FrmSettings.LBLLastOrgasm.Text = My.Settings.LastOrgasm
            inputString = inputString.Replace("@UpdateOrgasm", "")
        End If

        If inputString.Contains("@UpdateRuined") Then
            My.Settings.LastRuined = FormatDateTime(Now, DateFormat.ShortDate)

            ' GithubPatch
            If My.Settings.OrgasmsLocked = True Then My.Settings.OrgasmsRemaining -= 1

            FrmSettings.LBLLastRuined.Text = My.Settings.LastRuined
            inputString = inputString.Replace("@UpdateRuined", "")
        End If

        If inputString.Contains("@DeleteVar[") Then

            Dim DeleteArray As String() = inputString.Split("]")

            For i As Integer = 0 To DeleteArray.Count - 1

                If DeleteArray(i).Contains("@DeleteVar[") Then

                    DeleteArray(i) = DeleteArray(i) & "]"

                    Dim DFlag As String = GetParentheses(DeleteArray(i), "@DeleteVar[")
                    Dim OriginalDelete As String = DFlag

                    If DFlag.Contains(",") Then

                        DFlag = FixCommas(DFlag)

                        Dim FlagArray() As String = DFlag.Split(",")

                        For x As Integer = 0 To FlagArray.Count - 1

                            DeleteVariable(FlagArray(x))

                        Next

                    Else

                        DeleteVariable(DFlag)

                    End If

                    'DeleteArray(i) = DeleteArray(i).Replace("@DeleteVar[" & OriginalDelete & "]", "")

                    inputString = inputString.Replace("@DeleteVar[" & OriginalDelete & "]", "")

                End If

            Next

            'StringClean = Join(DeleteArray, Nothing)

        End If

        If inputString.Contains(Keyword.PornAllowedOff) Then
            myFlagAccessor.SetFlag(CreateDommePersonality(), "SYS_NoPornAllowed", False)
            inputString = inputString.Replace(Keyword.PornAllowedOff, "")
        End If

        If inputString.Contains(Keyword.PornAllowedOn) Then
            myFlagAccessor.DeleteFlag(CreateDommePersonality(), "SYS_NoPornAllowed")
            inputString = inputString.Replace(Keyword.PornAllowedOn, "")
        End If

        If inputString.Contains("@RestrictOrgasm(") Then

            Dim CheckFlag As String = GetParentheses(inputString, "@RestrictOrgasm(")

            If CheckFlag.Contains(",") Then

                CheckFlag = CheckFlag.Replace(", ", ",")
                CheckFlag = CheckFlag.Replace(" ,", ",")

                Dim FlagArray() As String = CheckFlag.Split(",")

                Dim Seconds1 As Integer = Val(FlagArray(0))
                Dim Seconds2 As Integer = Val(FlagArray(1))

                If UCase(FlagArray(0)).Contains(UCase("MINUTE")) Then Seconds1 *= 60
                If UCase(FlagArray(0)).Contains(UCase("HOUR")) Then Seconds1 *= 3600
                If UCase(FlagArray(0)).Contains(UCase("DAY")) Then Seconds1 *= 86400
                If UCase(FlagArray(0)).Contains(UCase("WEEK")) Then Seconds1 *= 604800
                If UCase(FlagArray(0)).Contains(UCase("MONTH")) Then Seconds1 *= 2419200
                If UCase(FlagArray(0)).Contains(UCase("YEAR")) Then Seconds1 *= 29030400

                If UCase(FlagArray(1)).Contains(UCase("MINUTE")) Then Seconds2 *= 60
                If UCase(FlagArray(1)).Contains(UCase("HOUR")) Then Seconds2 *= 3600
                If UCase(FlagArray(1)).Contains(UCase("DAY")) Then Seconds2 *= 86400
                If UCase(FlagArray(1)).Contains(UCase("WEEK")) Then Seconds2 *= 604800
                If UCase(FlagArray(1)).Contains(UCase("MONTH")) Then Seconds2 *= 2419200
                If UCase(FlagArray(1)).Contains(UCase("YEAR")) Then Seconds2 *= 29030400

                Dim TotalSeconds As Integer = ssh.randomizer.Next(Seconds1, Seconds2 + 1)

                Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)

                SetDate = DateAdd(DateInterval.Second, TotalSeconds, SetDate)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_OrgasmRestricted", FormatDateTime(SetDate, DateFormat.GeneralDate), False)

            Else

                Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)

                If UCase(CheckFlag).Contains(UCase("SECOND")) Then SetDate = DateAdd(DateInterval.Second, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("MINUTE")) Then SetDate = DateAdd(DateInterval.Minute, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("HOUR")) Then SetDate = DateAdd(DateInterval.Hour, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("DAY")) Then SetDate = DateAdd(DateInterval.Day, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("WEEK")) Then SetDate = DateAdd(DateInterval.Day, Val(CheckFlag) * 7, SetDate)
                If UCase(CheckFlag).Contains(UCase("MONTH")) Then SetDate = DateAdd(DateInterval.Month, Val(CheckFlag), SetDate)
                If UCase(CheckFlag).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Year, Val(CheckFlag), SetDate)

                If Not UCase(CheckFlag).Contains(UCase("SECOND")) And Not UCase(CheckFlag).Contains(UCase("MINUTE")) And Not UCase(CheckFlag).Contains(UCase("HOUR")) _
                 And Not UCase(CheckFlag).Contains(UCase("DAY")) And Not UCase(CheckFlag).Contains(UCase("WEEK")) And Not UCase(CheckFlag).Contains(UCase("MONTH")) _
                 And Not UCase(CheckFlag).Contains(UCase("YEAR")) Then SetDate = DateAdd(DateInterval.Day, Val(CheckFlag), SetDate)

                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_OrgasmRestricted", FormatDateTime(SetDate, DateFormat.GeneralDate), False)

            End If
            ssh.OrgasmRestricted = True
            inputString = inputString.Replace("@RestrictOrgasm(" & GetParentheses(inputString, "@RestrictOrgasm(") & ")", "")
        End If

        If inputString.Contains("@RestrictOrgasm") Then
            ssh.OrgasmRestricted = True
            inputString = inputString.Replace("@RestrictOrgasm", "")
        End If

        '@@@@@@@@@@@@@@@@@@@@@@ TASKCLEAN END

        If shouldTaskClean = True Then Return inputString


        ' The @CheckDate() Command checks a previously saved Variable created with the @SetDate() Command and goes to the specified line if the current time and date is on or after the date in the Variable.
        ' Correct format is @CheckDate(VarName, Goto Line) . For example, @CheckDate(NoPorn, Look At Porn Again) will go to the line (Look At Porn Again) if the current time and date has passed the value set
        ' for the Variable "NoPorn" by @SetDate()

        If inputString.Contains("@CheckDate(") Then

            Dim CheckArray As String() = inputString.Split(")")

            For i As Integer = 0 To CheckArray.Count - 1

                If CheckArray(i).Contains("@CheckDate(") Then

                    If CheckDateList(CheckArray(i), True) = True Then
                        Dim DateFlag As String = GetParentheses(CheckArray(i), "@CheckDate(")
                        DateFlag = FixCommas(DateFlag)
                        Dim DateArray As String() = DateFlag.Split(",")
                        ssh.SkipGotoLine = True
                        ssh.FileGoto = DateArray(DateArray.Count - 1).Replace(")", "")
                        GetGoto()
                    End If

                    inputString = inputString.Replace("@CheckDate(" & GetParentheses(CheckArray(i), "@CheckDate(") & ")", "")

                End If

            Next

        End If

        ' The @If[] Command allows you to compare Variables and go to a specific line if the statement is true. The correct format is @If[VarName]>[varName2]Then(Goto Line)
        ' For example, If[StrokeTotal]>[1000]Then(Thousand Strokes) would check if the Variable "StrokeTotal" is greater than 1000, and go to (Thousand Strokes) if so. 
        ' The @If[] Command can compare any combination of Variables and numeric values with = (or ==), <>, >, <, >= and <= . String Variables can be compared with = (or ==) and <> 
        ' More than one @If[] Command can be used per line. Tease AI will move to the line specified by whichever true statement happened last in the line.

        If inputString.Contains("@If[") Then

            Do

                Dim SCIfVar As String() = Split(inputString)
                Dim SCGotVar As String = "Null"

                For i As Integer = 0 To SCIfVar.Length - 1
                    If SCIfVar(i).Contains("@If[") Then
                        Dim IFJoin As Integer = 0
                        If Not SCIfVar(i).Contains(")") Then
                            Do
                                IFJoin += 1
                                SCIfVar(i) = SCIfVar(i) & " " & SCIfVar(i + IFJoin)
                                SCIfVar(i + IFJoin) = ""
                            Loop Until SCIfVar(i).Contains(")")
                        End If
                        SCGotVar = SCIfVar(i)
                        SCIfVar(i) = ""
                        inputString = Join(SCIfVar)
                        Do
                            inputString = inputString.Replace("  ", " ")
                        Loop Until Not inputString.Contains("  ")
                        Exit For
                    End If
                Next

                If SCGotVar.Contains("]And[") Then

                    Dim AndCheck As Boolean = True

                    For x As Integer = 0 To SCGotVar.Replace("]And[", "").Count - 1
                        If GetIf("[" & GetParentheses(SCGotVar, "@If[", 2) & "]") = False Then
                            AndCheck = False
                            Exit For
                        End If
                        SCGotVar = SCGotVar.Replace("[" & GetParentheses(SCGotVar, "@If[", 2) & "]And", "")
                    Next

                    If AndCheck = True Then
                        ssh.FileGoto = GetParentheses(SCGotVar, "Then(")
                        ssh.SkipGotoLine = True
                        GetGoto()
                    End If

                ElseIf SCGotVar.Contains("]Or[") Then

                    Dim OrCheck As Boolean = False

                    For x As Integer = 0 To SCGotVar.Replace("]Or[", "").Count - 1
                        If GetIf("[" & GetParentheses(SCGotVar, "@If[", 2) & "]") = True Then
                            OrCheck = True
                            Exit For
                        End If
                        SCGotVar = SCGotVar.Replace("[" & GetParentheses(SCGotVar, "@If[", 2) & "]Or", "")
                    Next

                    If OrCheck = True Then
                        ssh.FileGoto = GetParentheses(SCGotVar, "Then(")
                        ssh.SkipGotoLine = True
                        GetGoto()
                    End If

                Else

                    If GetIf("[" & GetParentheses(SCGotVar, "@If[", 2) & "]") = True Then
                        ssh.FileGoto = GetParentheses(SCGotVar, "Then(")
                        ssh.SkipGotoLine = True
                        GetGoto()
                    End If

                End If

            Loop Until Not inputString.Contains("@If")

        End If

        ' The @InputVar[] stops script progression and waits for the user to input his next message. Whatever the user types next will be saved as a Variable named whatever you specify in the brackets.
        ' For example, if the script's line was "What's your favorite food? @InputVar[FavoriteFood]", and the user typed "lo mein", then "lo mein" would be saved as the Variable "FavoriteFood". If the
        ' user has checked "Show Icon During Input Questions" in the General Settings tab, then the domme's question will be accompanied by a small question mark icon to let the user know that their next
        ' response will be saved verbatim. @InputVar[] will pause Linear Scripts, as well as countdowns and taunts for Stroking, Edging and Holding The Edge.

        If inputString.Contains("@InputVar[") Then

            ssh.InputString = GetParentheses(inputString, "@InputVar[").Replace("]", "")
            ssh.InputFlag = True
            If FrmSettings.CBInputIcon.Checked = True Then ssh.InputIcon = True

            inputString = inputString.Replace("@InputVar[" & ssh.InputString & "]", "")

        End If


        ' The @DislikeBlogImage Command takes the URL of the most recently viewed blog image and adds it to the "Disliked" list located in [Tease AI Root Directory]\Images\System\DislikedImageURLS.txt

        If inputString.Contains("@DislikeBlogImage") Then

            If ssh.ImageLocation <> "" Then

                If File.Exists(Application.StartupPath & "\Images\System\DislikedImageURLs.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\DislikedImageURLs.txt", Environment.NewLine & ssh.ImageLocation, True)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\DislikedImageURLs.txt", ssh.ImageLocation, True)
                End If
                inputString = inputString.Replace("@DislikeBlogImage", "")
            End If

        End If

        ' The @LikeBlogImage Command takes the URL of the most recently viewed blog image and adds it to the "Liked" list located in [Tease AI Root Directory]\Images\System\LikedImageURLS.txt

        If inputString.Contains("@LikeBlogImage") Then

            If ssh.ImageLocation <> "" Then

                If File.Exists(Application.StartupPath & "\Images\System\LikedImageURLs.txt") Then
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\LikedImageURLs.txt", Environment.NewLine & ssh.ImageLocation, True)
                Else
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\LikedImageURLs.txt", ssh.ImageLocation, True)
                End If
                inputString = inputString.Replace("@LikeBlogImage", "")
            End If

        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┌─┐┌─┐┌┬┐┌─┐┬─┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╠╣ ├─┤└─┐ │ ├┤ ├┬┘
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚  ┴ ┴└─┘ ┴ └─┘┴└─

        If inputString.Contains("@StrokeFaster") Then
            ssh.StrokeFaster = True
            inputString = inputString.Replace("@StrokeFaster", "")
        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┬  ┌─┐┬ ┬┌─┐┬─┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╚═╗│  │ ││││├┤ ├┬┘
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚═╝┴─┘└─┘└┴┘└─┘┴└─

        If inputString.Contains("@StrokeSlower") Then
            ssh.StrokeSlower = True
            inputString = inputString.Replace("@StrokeSlower", "")
        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┌─┐┌─┐┌┬┐┌─┐┌─┐┌┬┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╠╣ ├─┤└─┐ │ ├┤ └─┐ │ 
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚  ┴ ┴└─┘ ┴ └─┘└─┘ ┴ 

        If inputString.Contains("@StrokeFastest") Then
            ssh.StrokeFastest = True
            inputString = inputString.Replace("@StrokeFastest", "")
        End If

        '  ╔═╗┌┬┐┬─┐┌─┐┬┌─┌─┐╔═╗┬  ┌─┐┬ ┬┌─┐┌─┐┌┬┐
        '  ╚═╗ │ ├┬┘│ │├┴┐├┤ ╚═╗│  │ ││││├┤ └─┐ │ 
        '  ╚═╝ ┴ ┴└─└─┘┴ ┴└─┘╚═╝┴─┘└─┘└┴┘└─┘└─┘ ┴ 

        If inputString.Contains("@StrokeSlowest") Then
            ssh.StrokeSlowest = True
            inputString = inputString.Replace("@StrokeSlowest", "")
        End If


        If inputString.Contains("@StartStroking") Then

            If Not File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_FirstRun") Then
                Dim SetDate As Date = FormatDateTime(Now, DateFormat.GeneralDate)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\System\Variables\SYS_FirstRun", SetDate, False)
            End If

            ssh.AskedToGiveUpSection = False
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.BeforeTease = False
            ssh.SubStroking = True
            ssh.ShowModule = False
            'StrokeCycle = -1

            ssh.StrokeTauntTick = ssh.randomizer.Next(11, 21)
            'StrokeThread = New Thread(AddressOf StrokeLoop)
            'StrokeThread.IsBackground = True
            'StrokeThread.SetApartmentState(ApartmentState.STA)
            'StrokeThread.Start()

            inputString = inputString.Replace("@StartStroking", "")
        End If

        If inputString.Contains("@StartTaunts") Then
            ssh.AskedToGiveUpSection = False
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.BeforeTease = False
            ssh.SubStroking = True
            ssh.ShowModule = False
            'StrokeCycle = -1
            If ssh.StartStrokingCount = 0 Then ssh.FirstRound = True
            ssh.StartStrokingCount += 1
            ' github patch StrokePace = 0
            ' github patch StrokePaceTimer.Interval = StrokePace

            ClearModes()

            If FrmSettings.CBTauntCycleDD.Checked = True Then
                If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick = ssh.randomizer.Next(1, 3) * 60
                If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick = ssh.randomizer.Next(1, 4) * 60
                If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick = ssh.randomizer.Next(3, 6) * 60
                If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick = ssh.randomizer.Next(4, 8) * 60
                If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick = ssh.randomizer.Next(5, 11) * 60
            Else
                ssh.StrokeTick = ssh.randomizer.Next(FrmSettings.NBTauntCycleMin.Value * 60, FrmSettings.NBTauntCycleMax.Value * 60)
            End If
            ssh.StrokeTauntTick = ssh.randomizer.Next(11, 21)
            StrokeTimer.Start()
            StrokeTauntTimer.Start()
            inputString = inputString.Replace("@StartTaunts", "")
        End If

        If inputString.Contains("@StopStroking") Then
            If FrmSettings.TBWebStop.Text <> "" Then
                Try
                    FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text)
                Catch
                End Try
            End If
            If ssh.Contact1Stroke = True Then
                inputString = inputString & "@Contact1"
                ssh.Contact1Stroke = False
            End If
            If ssh.Contact2Stroke = True Then
                inputString = inputString & "@Contact2"
                ssh.Contact2Stroke = False
            End If
            If ssh.Contact3Stroke = True Then
                inputString = inputString & "@Contact3"
                ssh.Contact3Stroke = False
            End If
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.SubStroking = False
            ssh.SubEdging = False
            ssh.SubHoldingEdge = False
            ssh.WorshipMode = False
            ssh.WorshipTarget = ""
            ssh.LongHold = False
            ssh.ExtremeHold = False
            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            StrokePace = 0
            EdgeTauntTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            inputString = inputString.Replace("@StopStroking", "")
        End If

        If inputString.Contains("@StopTaunts") Then
            ssh.AskedToSpeedUp = False
            ssh.AskedToSlowDown = False
            ssh.SubStroking = False
            ssh.SubEdging = False
            ssh.SubHoldingEdge = False
            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            EdgeTauntTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            inputString = inputString.Replace("@StopTaunts", "")
        End If


        If inputString.Contains("@LongHold(") Then
            Dim HoldInt As Integer = Val(GetParentheses(inputString, "@LongHold("))
            ssh.TempVal = ssh.randomizer.Next(0, 101)
            If ssh.TempVal <= HoldInt Then ssh.LongHold = True

            inputString = inputString.Replace("@LongHold(" & GetParentheses(inputString, "@LongHold(") & ")", "")
        End If

        If inputString.Contains("@ExtremeHold(") Then
            Dim HoldInt As Integer = Val(GetParentheses(inputString, "@ExtremeHold("))
            ssh.TempVal = ssh.randomizer.Next(0, 101)
            If ssh.TempVal <= HoldInt Then ssh.ExtremeHold = True

            inputString = inputString.Replace("@ExtremeHold(" & GetParentheses(inputString, "@ExtremeHold(") & ")", "")
        End If

        If inputString.Contains("@LongHold") Then
            ssh.LongHold = True
            inputString = inputString.Replace("@LongHold", "")
        End If

        If inputString.Contains("@ExtremeHold") Then
            ssh.ExtremeHold = True
            inputString = inputString.Replace("@ExtremeHold", "")
        End If

        If inputString.Contains("@MultipleEdges(") Then

            If inputString.Contains("@Edg") Then

                Dim EdgeFlag As String = GetParentheses(inputString, "@MultipleEdges(")
                EdgeFlag = FixCommas(EdgeFlag)
                Dim EdgeArray As String() = EdgeFlag.Split(",")

                If EdgeArray.Count = 3 Then

                    If ssh.randomizer.Next(1, 101) < Val(EdgeArray(2)) Then
                        ssh.MultipleEdges = True
                        ssh.MultipleEdgesAmount = Val(EdgeArray(0))
                        ssh.MultipleEdgesInterval = Val(EdgeArray(1))
                    End If

                Else

                    ssh.MultipleEdges = True
                    ssh.MultipleEdgesAmount = Val(EdgeArray(0))
                    ssh.MultipleEdgesInterval = Val(EdgeArray(1))

                End If

            End If

            inputString = inputString.Replace("@MultipleEdges(" & GetParentheses(inputString, "@MultipleEdges(") & ")", "")

        End If


        If inputString.Contains("@Edge(") Then

            ContactEdgeCheck(inputString)

            Edge()

            If GetMatch(inputString, "@Edge(", "Hold") = True Then ssh.EdgeHold = True
            If GetMatch(inputString, "@Edge(", "NoHold") = True Then ssh.EdgeNoHold = True
            If ssh.EdgeHold = True And ssh.EdgeNoHold = True Then ssh.EdgeHold = False

            If GetMatch(inputString, "@Edge(", "Deny") = True Then ssh.OrgasmDenied = True

            If GetMatch(inputString, "@Edge(", "Orgasm") = True Then ssh.OrgasmAllowed = True

            If GetMatch(inputString, "@Edge(", "Ruin") = True Then ssh.OrgasmRuined = True

            If ssh.OrgasmAllowed = True And ssh.OrgasmRuined = True Then ssh.OrgasmRuined = False

            If GetMatch(inputString, "@Edge(", "RuinTaunts") = True Then
                If ssh.EdgeToRuin = True Then ssh.EdgeToRuinSecret = False
            End If

            If GetMatch(inputString, "@Edge(", "LongHold") = True Then
                ssh.EdgeHold = True
                ssh.LongHold = True
            End If
            If GetMatch(inputString, "@Edge(", "ExtremeHold") = True Then
                ssh.EdgeHold = True
                ssh.ExtremeHold = True
            End If
            If GetMatch(inputString, "@Edge(", "HoldTaunts") = True Then
                If ssh.LongHold = True Then ssh.LongTaunts = True
            End If

        End If



        If inputString.Contains("@EdgeMode(") Then

            Dim EdgeFlag As String = GetParentheses(inputString, "@EdgeMode(")
            EdgeFlag = FixCommas(EdgeFlag)
            Dim EdgeArray As String() = EdgeFlag.Split(",")

            If UCase(EdgeArray(0)).Contains("GOTO") Then
                ssh.EdgeGoto = True
                ssh.EdgeGotoLine = EdgeArray(1)
            End If

            If UCase(EdgeArray(0)).Contains("MESSAGE") Then
                ssh.EdgeMessage = True
                ssh.EdgeMessageText = EdgeArray(1)
            End If

            If UCase(EdgeArray(0)).Contains("VIDEO") Then
                ssh.EdgeVideo = True
                ssh.EdgeGotoLine = EdgeArray(1)
            End If

            If UCase(EdgeArray(0)).Contains("NORMAL") Then
                ssh.EdgeGoto = False
                ssh.EdgeMessage = False
                ssh.EdgeVideo = False
            End If

            inputString = inputString.Replace("@EdgeMode(" & GetParentheses(inputString, "@EdgeMode(") & ")", "")
        End If

        If inputString.Contains("@EdgeToRuinNoHoldNoSecret") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeToRuin = True
            ssh.EdgeToRuinSecret = False
            inputString = inputString.Replace("@EdgeToRuinNoHoldNoSecret", "")
        End If

        If inputString.Contains("@EdgeToRuinHoldNoSecret(") Then

            Dim EdgeHoldFlag As String = GetParentheses(inputString, "@EdgeToRuinHoldNoSecret(")

            If EdgeHoldFlag.Contains(",") Then

                EdgeHoldFlag = FixCommas(EdgeHoldFlag)

                Dim EdgeFlagArray As String() = EdgeHoldFlag.Split(",")

                Dim Edge1 As Integer = Val(EdgeFlagArray(0))
                Dim Edge2 As Integer = Val(EdgeFlagArray(1))

                If UCase(EdgeFlagArray(0)).Contains("M") Then Edge1 *= 60
                If UCase(EdgeFlagArray(1)).Contains("M") Then Edge2 *= 60

                If UCase(EdgeFlagArray(0)).Contains("H") Then Edge1 *= 3600
                If UCase(EdgeFlagArray(1)).Contains("H") Then Edge2 *= 3600

                ssh.EdgeHoldSeconds = ssh.randomizer.Next(Edge1, Edge2 + 1)

            Else

                ssh.EdgeHoldSeconds = Val(EdgeHoldFlag)
                If UCase(GetParentheses(inputString, "@EdgeToRuinHoldNoSecret(")).Contains("M") Then ssh.EdgeHoldSeconds *= 60
                If UCase(GetParentheses(inputString, "@EdgeToRuinHoldNoSecret(")).Contains("H") Then ssh.EdgeHoldSeconds *= 3600

            End If

            EdgeHoldFlag = True

            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True
            ssh.EdgeToRuinSecret = False
            inputString = inputString.Replace("@EdgeToRuinHoldNoSecret(" & GetParentheses(inputString, "@EdgeToRuinHoldNoSecret(") & ")", "")
        End If



        If inputString.Contains("@EdgeToRuinHoldNoSecret") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True
            ssh.EdgeToRuinSecret = False
            inputString = inputString.Replace("@EdgeToRuinHoldNoSecret", "")
        End If

        If inputString.Contains("@EdgeToRuinNoSecret") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeToRuinSecret = False
            ssh.EdgeToRuin = True
            inputString = inputString.Replace("@EdgeToRuinNoSecret", "")
        End If

        If inputString.Contains("@EdgeToRuinNoHold") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeNoHold = True
            ssh.EdgeToRuin = True
            inputString = inputString.Replace("@EdgeToRuinNoHold", "")
        End If

        If inputString.Contains("@EdgeToRuinHold(") Then

            Dim EdgeHoldFlag As String = GetParentheses(inputString, "@EdgeToRuinHold(")

            If EdgeHoldFlag.Contains(",") Then

                EdgeHoldFlag = FixCommas(EdgeHoldFlag)

                Dim EdgeFlagArray As String() = EdgeHoldFlag.Split(",")

                Dim Edge1 As Integer = Val(EdgeFlagArray(0))
                Dim Edge2 As Integer = Val(EdgeFlagArray(1))

                If UCase(EdgeFlagArray(0)).Contains("M") Then Edge1 *= 60
                If UCase(EdgeFlagArray(1)).Contains("M") Then Edge2 *= 60

                If UCase(EdgeFlagArray(0)).Contains("H") Then Edge1 *= 3600
                If UCase(EdgeFlagArray(1)).Contains("H") Then Edge2 *= 3600

                ssh.EdgeHoldSeconds = ssh.randomizer.Next(Edge1, Edge2 + 1)

            Else

                ssh.EdgeHoldSeconds = Val(EdgeHoldFlag)
                If UCase(GetParentheses(inputString, "@EdgeToRuinHold(")).Contains("M") Then ssh.EdgeHoldSeconds *= 60
                If UCase(GetParentheses(inputString, "@EdgeToRuinHold(")).Contains("H") Then ssh.EdgeHoldSeconds *= 3600

            End If

            EdgeHoldFlag = True

            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True

            inputString = inputString.Replace("@EdgeToRuinHold(" & GetParentheses(inputString, "@EdgeToRuinHold(") & ")", "")
        End If

        If inputString.Contains("@EdgeToRuinHold") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeHold = True
            ssh.EdgeToRuin = True
            inputString = inputString.Replace("@EdgeToRuinHold", "")
        End If

        If inputString.Contains("@EdgeToRuin") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeToRuin = True
            inputString = inputString.Replace("@EdgeToRuin", "")
        End If

        If inputString.Contains("@EdgeNoHold") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeNoHold = True
            inputString = inputString.Replace("@EdgeNoHold", "")
        End If


        ' The Commands @EdgeHold(), @EdgeToRuinHold() and @EdgeToRuinHoldNoSecret() allow you to specify the amount of time the edge is held. The defualt is in seconds, but you can use Minutes and Hours as well
        ' For example: @EdgeHold(60) would have the domme make you hold the edge for 60 seconds
        ' @EdgeHold(3 Minutes) or @EdgeHold(3 M) - Domme will make you hold the edge for three minutes
        ' @EdgeHold(2 Hours) - Domme will make you hold the edge for 2 hours. Good luck :D
        '
        'You can also set a time range using a comma. For example, @EdgeHold(2 Minutes, 5 Minutes) - the domme would make you hold it a random amount of time bwteen 2 and 5 minutes.

        If inputString.Contains("@EdgeHold(") Then

            Dim EdgeHoldFlag As String = GetParentheses(inputString, "@EdgeHold(")

            If EdgeHoldFlag.Contains(",") Then

                EdgeHoldFlag = FixCommas(EdgeHoldFlag)

                Dim EdgeFlagArray As String() = EdgeHoldFlag.Split(",")

                Dim Edge1 As Integer = Val(EdgeFlagArray(0))
                Dim Edge2 As Integer = Val(EdgeFlagArray(1))

                If UCase(EdgeFlagArray(0)).Contains("M") Then Edge1 *= 60
                If UCase(EdgeFlagArray(1)).Contains("M") Then Edge2 *= 60

                If UCase(EdgeFlagArray(0)).Contains("H") Then Edge1 *= 3600
                If UCase(EdgeFlagArray(1)).Contains("H") Then Edge2 *= 3600

                ssh.EdgeHoldSeconds = ssh.randomizer.Next(Edge1, Edge2 + 1)

            Else

                ssh.EdgeHoldSeconds = Val(EdgeHoldFlag)
                If UCase(GetParentheses(inputString, "@EdgeHold(")).Contains("M") Then ssh.EdgeHoldSeconds *= 60
                If UCase(GetParentheses(inputString, "@EdgeHold(")).Contains("H") Then ssh.EdgeHoldSeconds *= 3600

            End If

            EdgeHoldFlag = True


            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeHold = True
            inputString = inputString.Replace("@EdgeHold(" & GetParentheses(inputString, "@EdgeHold(") & ")", "")

        End If


        If inputString.Contains("@EdgeHold") Then
            ContactEdgeCheck(inputString)
            Edge()
            ssh.EdgeHold = True
            inputString = inputString.Replace("@EdgeHold", "")
        End If

        If inputString.Contains("@Edge") Then
            ContactEdgeCheck(inputString)
            Edge()
            inputString = inputString.Replace("@Edge", "")
        End If

        If inputString.Contains("@CBT") And Not inputString.Contains("@CBTLevel") Then

            If FrmSettings.CockTortureEnabledCB.Checked = True And FrmSettings.BallTortureEnabledCB.Checked = True Then
                ssh.CBTBothActive = True
                ssh.CBTBothFlag = True
                ssh.TasksCount = ssh.randomizer.Next(FrmSettings.TaskWaitMinimum.Value, FrmSettings.TaskWaitMaximum.Value + 1)
            End If

            inputString = inputString.Replace("@CBT", "")
        End If

        If inputString.Contains("@DecideOrgasm") Then

            ssh.OrgasmDenied = False
            ssh.OrgasmAllowed = False
            ssh.OrgasmRuined = False

            Dim AllowGoto As String = "Orgasm Allow"
            Dim RuinGoto As String = "Orgasm Ruin"
            Dim DenyGoto As String = "Orgasm Deny"

            If inputString.Contains("@DecideOrgasm(") Then

                Dim OrgasmFlag As String = GetParentheses(inputString, "@DecideOrgasm(")
                OrgasmFlag = FixCommas(OrgasmFlag)
                Dim OrgasmArray As String() = OrgasmFlag.Split(",")

                If OrgasmArray.Count = 3 Then
                    AllowGoto = OrgasmArray(0)
                    RuinGoto = OrgasmArray(1)
                    DenyGoto = OrgasmArray(2)
                End If

            End If


            If FrmSettings.alloworgasmComboBox.Text = "Always Allows" And FrmSettings.ruinorgasmComboBox.Text = "Always Ruins" Then
                ssh.FileGoto = RuinGoto
                ssh.OrgasmRuined = True
                GoTo OrgasmDecided
            End If

            Dim OrgasmInt As Integer = ssh.randomizer.Next(1, 101)
            Dim OrgasmThreshold As Integer

            If FrmSettings.alloworgasmComboBox.Text = "Never Allows" Then OrgasmThreshold = 0
            If FrmSettings.alloworgasmComboBox.Text = "Always Allows" Then OrgasmThreshold = 1000

            If FrmSettings.DommeDecideOrgasmCB.Checked = True Then
                If FrmSettings.alloworgasmComboBox.Text = "Rarely Allows" Then OrgasmThreshold = 20
                If FrmSettings.alloworgasmComboBox.Text = "Sometimes Allows" Then OrgasmThreshold = 50
                If FrmSettings.alloworgasmComboBox.Text = "Often Allows" Then OrgasmThreshold = 75
            Else
                If FrmSettings.alloworgasmComboBox.Text = "Rarely Allows" Then OrgasmThreshold = FrmSettings.NBAllowRarely.Value
                If FrmSettings.alloworgasmComboBox.Text = "Sometimes Allows" Then OrgasmThreshold = FrmSettings.NBAllowSometimes.Value
                If FrmSettings.alloworgasmComboBox.Text = "Often Allows" Then OrgasmThreshold = FrmSettings.AllowOrgasmOftenNB.Value
            End If


            If OrgasmInt > OrgasmThreshold Then
                ssh.FileGoto = DenyGoto
                ssh.OrgasmDenied = True
                GoTo OrgasmDecided
            End If

            Dim RuinInt As Integer = ssh.randomizer.Next(1, 101)
            Dim RuinThreshold As Integer

            If FrmSettings.ruinorgasmComboBox.Text = "Never Ruins" Then RuinThreshold = 0
            If FrmSettings.ruinorgasmComboBox.Text = "Always Ruins" Then RuinThreshold = 1000


            If FrmSettings.DommeDecideRuinCB.Checked = True Then
                If FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins" Then RuinThreshold = 20
                If FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins" Then RuinThreshold = 50
                If FrmSettings.ruinorgasmComboBox.Text = "Often Ruins" Then RuinThreshold = 75
            Else
                If FrmSettings.ruinorgasmComboBox.Text = "Rarely Ruins" Then RuinThreshold = FrmSettings.NBRuinRarely.Value
                If FrmSettings.ruinorgasmComboBox.Text = "Sometimes Ruins" Then RuinThreshold = FrmSettings.NBRuinSometimes.Value
                If FrmSettings.ruinorgasmComboBox.Text = "Often Ruins" Then RuinThreshold = FrmSettings.NBRuinOften.Value
            End If


            If RuinInt > RuinThreshold Then
                ssh.FileGoto = AllowGoto
                ssh.OrgasmAllowed = True
            Else
                ssh.FileGoto = RuinGoto
                ssh.OrgasmRuined = True
            End If

OrgasmDecided:

            ssh.SkipGotoLine = True
            GetGoto()

            inputString = inputString.Replace("@DecideOrgasm", "")
        End If

        If inputString.Contains(Keyword.OrgasmRuin) Then
            ssh.FileGoto = "Orgasm Ruin"
            ssh.OrgasmRuined = True
            ssh.SkipGotoLine = True
            GetGoto()
            inputString = inputString.Replace(Keyword.OrgasmRuin, "")
        End If

        ' The @Glitter Command allows to specify a specfic script from the domme's Apps\Glitter\Script directory, which will then immediately play out in the Glitter app. For example, @Glitter(About to Ruin)
        ' would run the Glitter script in Apps\Glitter\Script\About to Ruin.txt.

        If inputString.Contains(Keyword.Glitter) Then

            ' GitHub Patch: Dim GlitterFlag As Integer = GetParentheses(StringClean, Keywords.Glitter)
            Dim GlitterFlag As String = GetParentheses(inputString, Keyword.Glitter)

            If My.Settings.CBGlitterFeedOff = False And ssh.UpdatingPost = False Then
                If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Script\" & GlitterFlag & ".txt") And ssh.UpdatingPost = False Then
                    ssh.UpdateList.Clear()
                    ssh.UpdateList.Add(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\Glitter\Script\" & GlitterFlag & ".txt")
                    StatusUpdatePost()
                End If
            End If

            inputString = inputString.Replace(Keyword.Glitter & GlitterFlag & ")", "")

        End If

        If inputString.Contains("@WritingTask(") Then

            ssh.WritingTaskFlag = True

            Dim WTTempString As String() = Split(inputString, "@WritingTask(", 2)
            Dim WTTemp As String() = Split(WTTempString(1), ")")
            LBLWritingTaskText.Text = WTTemp(0)
            LBLWritingTaskText.Text = StripCommands(LBLWritingTaskText.Text)
            LBLWritingTaskText.Text = StripFormat(LBLWritingTaskText.Text)
            LBLWritingTaskText.Text = LBLWritingTaskText.Text.Replace("  ", " ")

            Dim WritingTaskVal As Integer = Val(LBLWritingTaskText.Text)

            If WritingTaskVal = 0 Then
                ssh.WritingTaskLinesAmount = ssh.randomizer.Next(FrmSettings.NBWritingTaskMin.Value, FrmSettings.NBWritingTaskMax.Value)
                ssh.WritingTaskLinesAmount = 5 * Math.Round(ssh.WritingTaskLinesAmount / 5)
            Else
                ssh.WritingTaskLinesAmount = WritingTaskVal
                LBLWritingTaskText.Text = LBLWritingTaskText.Text.Replace(WritingTaskVal, "")
            End If

            LBLLinesWritten.Text = "0"
            LBLLinesRemaining.Text = ssh.WritingTaskLinesAmount

            If PNLWritingTask.Visible = False Then
                ToggleAppVisibility(PNLWritingTask)
            End If

            'WritingTaskMistakesAllowed = randomizer.Next(3, 9)

            'determine error numbers based on numbers of lines to write
            ssh.WritingTaskMistakesAllowed = ssh.randomizer.Next(ssh.WritingTaskLinesAmount / 10, ssh.WritingTaskLinesAmount / 3)
            'clamps the value between 2 and 10 errors
            ssh.WritingTaskMistakesAllowed = Math.Max(2, ssh.WritingTaskMistakesAllowed)
            ssh.WritingTaskMistakesAllowed = Math.Min(ssh.WritingTaskMistakesAllowed, 10)

            LBLMistakesAllowed.Text = ssh.WritingTaskMistakesAllowed
            LBLMistakesMade.Text = "0"
            inputString = inputString.Replace("@WritingTask", "")
            'LBLWritingTask.Text = "Write the following line " & WritingTaskLinesAmount & " times."
            ssh.WritingTaskLinesRemaining = ssh.WritingTaskLinesAmount
            ssh.WritingTaskLinesWritten = 0
            ssh.WritingTaskMistakesMade = 0
            chatBox.ShortcutsEnabled = False
            ChatBox2.ShortcutsEnabled = False

            If My.Settings.TimedWriting = True Then

                Dim secs As Single

                'determines how many secs are given for writing each line, depending on line length and typespeed value selected by the user in the settings
                '(between 0,54 and 0,75 secs per character in the sentence at slowest typingspeed and between 0.18 and 0.25 at fastest typing speed)
                secs = (ssh.randomizer.Next(15, 25) / My.Settings.TypeSpeed) * LBLWritingTaskText.Text.Length
                'determines how much time is given (in seconds) to complete the @WritingTask() depending on how many lines you have to write and a small bonus to give some
                'more time for very short lines
                ssh.WritingTaskCurrentTime = 5 + secs * ssh.WritingTaskLinesAmount

                LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times" & vbCrLf & "In " & ConvertSeconds(ssh.WritingTaskCurrentTime)
                LBLWritingTask.Text = LBLWritingTask.Text.Replace("line 1 times", "line 1 time")
            Else
                LBLWritingTask.Text = "Write the following line " & ssh.WritingTaskLinesAmount & " times"
                LBLWritingTask.Text = LBLWritingTask.Text.Replace("line 1 times", "line 1 time")
            End If

        End If

        If inputString.Contains("@CheckJOIVideo") Then

            If Directory.Exists(My.Settings.VideoJOI) Or Directory.Exists(My.Settings.VideoJOID) Then
                If FrmSettings.LblVideoJOITotal.Text <> "0" Or FrmSettings.LblVideoJOITotalD.Text <> "0" Then
                Else
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = "No JOI Found"
                    GetGoto()
                End If
            Else
                ssh.SkipGotoLine = True
                ssh.FileGoto = "No JOI Found"
                GetGoto()
            End If

            inputString = inputString.Replace("@CheckJOIVideo", "")

        End If

        If inputString.Contains("@PlayJOIVideo") Then

            If Directory.Exists(My.Settings.VideoJOI) Or Directory.Exists(My.Settings.VideoJOID) Then

                ssh.TeaseVideo = True
                PlayRandomJOI()
            End If

            inputString = inputString.Replace("@PlayJOIVideo", "")

        End If

        If inputString.Contains("@PlayCHVideo") Then

            If Directory.Exists(My.Settings.VideoCH) Or Directory.Exists(My.Settings.VideoCH) Then

                ssh.TeaseVideo = True
                PlayRandomCH()
            End If

            inputString = inputString.Replace("@PlayCHVideo", "")

        End If

        If inputString.Contains("@GiveUpCheck") Then


            If ssh.AskedToGiveUpSection = True Then

                If ssh.SubGaveUp = True Then
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpREHASH.txt"
                Else
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpREPEAT.txt"
                End If
                'StringClean = ResponseClean(StringClean)

            Else

                ssh.AskedToGiveUpSection = True
                ssh.AskedToGiveUp = True

                Dim GiveUpCheck As Integer

                If FrmSettings.NBEmpathy.Value = 1 Then GiveUpCheck = 0
                If FrmSettings.NBEmpathy.Value = 2 Then GiveUpCheck = 25
                If FrmSettings.NBEmpathy.Value = 3 Then GiveUpCheck = 50
                If FrmSettings.NBEmpathy.Value = 4 Then GiveUpCheck = 75
                If FrmSettings.NBEmpathy.Value = 5 Then GiveUpCheck = 1000

                Dim GiveUpVal As Integer = ssh.randomizer.Next(1, 101)

                'If GiveUpVal > GiveUpCheck Then
                If GiveUpVal > GiveUpCheck And Not ssh.LastScript Then
                    ' you can give up
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpALLOWED.txt"
                    ssh.LockImage = False
                    If ssh.SlideshowLoaded = True Then
                        ImageSlideShowNextButton.Enabled = True
                        ImageSlideShowPreviousButton.Enabled = True
                        PicStripTSMIdommeSlideshow.Enabled = True
                    End If
                    ssh.SubGaveUp = True
                    ssh.FirstRound = False
                Else
                    ' you can't give up
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\GiveUpDENIED.txt"
                End If



            End If

            inputString = ResponseClean(inputString)

        End If

        If inputString.Contains(Keyword.EndTease) Then
            SetVariable("SYS_SubLeftEarly", 0)
            'My.Settings.Sys_SubLeftEarly = 0
            'StopEverything()
            'ResetButton()
            ssh.Reset()
            FrmSettings.LockOrgasmChances(False)
            ssh.DomTask = "@SystemMessage <b>Tease AI has been reset</b>"
            ssh.DomChat = "@SystemMessage <b>Tease AI has been reset</b>"
            inputString = inputString.Replace(Keyword.EndTease, "")
        End If

        If inputString.Contains("@FinishTease") Then
            ssh.TeaseTick = 0
            inputString = inputString.Replace("@FinishTease", "")
        End If

        If inputString.Contains("@DommeLevelDown") Then
            If FrmSettings.DominationLevel.Value > 1 Then
                FrmSettings.DominationLevel.Value -= 1
            End If
            inputString = inputString.Replace("@DommeLevelDown", "")
        End If

        If inputString.Contains("@ApathyLevelDown") Then
            FrmSettings.NBEmpathy.Value = ApathyLevel.Create(Convert.ToInt32(FrmSettings.NBEmpathy.Value)).Value - 1
            inputString = inputString.Replace("@ApathyLevelDown", "")
        End If

        If inputString.Contains("@DommeLevelUp") Then
            If FrmSettings.DominationLevel.Value < 5 Then
                FrmSettings.DominationLevel.Value += 1
            End If
            inputString = inputString.Replace("@DommeLevelUp", "")
        End If

        If inputString.Contains("@ApathyLevelUp") Then
            FrmSettings.NBEmpathy.Value = ApathyLevel.Create(Convert.ToInt32(FrmSettings.NBEmpathy.Value)).Value + 1
            inputString = inputString.Replace("@ApathyLevelUp", "")
        End If

        If inputString.Contains("@InterruptLongEdge") Then

            Dim EdgeList As New List(Of String)

            For Each EdgeFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Long Edge\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                EdgeList.Add(EdgeFile)
            Next


            If EdgeList.Count > 0 Then

                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
                EdgeTauntTimer.Stop()
                StrokeTimer.Stop()
                StrokeTauntTimer.Stop()
                ssh.FileText = EdgeList(ssh.randomizer.Next(0, EdgeList.Count))
                ssh.LockImage = False
                ssh.MiniScript = False
                If ssh.SlideshowLoaded = True Then
                    ImageSlideShowNextButton.Enabled = True
                    ImageSlideShowPreviousButton.Enabled = True
                    PicStripTSMIdommeSlideshow.Enabled = True
                End If
                ssh.StrokeTauntVal = -1
                ssh.ScriptTick = 3
                ScriptTimer.Start()
                ssh.ShowModule = True

            Else
                MessageBox.Show(Me, "No files were found in " & Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Long Edge!" & Environment.NewLine _
                 & Environment.NewLine & "Please make sure at lease one LongEdge_ file exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            inputString = inputString.Replace("@InterruptLongEdge", "")
            ssh.JustShowedBlogImage = True
        End If

        If inputString.Contains("@InterruptStartStroking") Then

            If ssh.CensorshipGame = True Or ssh.AvoidTheEdgeGame = True Or ssh.RLGLGame = True Then
                inputString = "Ask me later"
                GoTo VTSkip
            End If

            Dim StrokeList As New List(Of String)

            For Each StrokeFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Start Stroking\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                StrokeList.Add(StrokeFile)
            Next


            If StrokeList.Count > 0 Then

                ssh.CBTCockFlag = False
                ssh.CBTBallsFlag = False
                ssh.CBTBothFlag = False
                ssh.CustomTask = False
                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
                EdgeTauntTimer.Stop()
                StrokeTimer.Stop()
                StrokeTauntTimer.Stop()
                ssh.FileText = StrokeList(ssh.randomizer.Next(0, StrokeList.Count))
                ssh.LockImage = False
                If ssh.SlideshowLoaded = True Then
                    ImageSlideShowNextButton.Enabled = True
                    ImageSlideShowPreviousButton.Enabled = True
                    PicStripTSMIdommeSlideshow.Enabled = True
                End If
                ssh.StrokeTauntVal = -1
                ssh.ScriptTick = 3
                ScriptTimer.Start()
                ssh.ShowModule = True
                ssh.MiniScript = False

            Else
                MessageBox.Show(Me, "No files were found in " & Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\Start Stroking!" & Environment.NewLine _
                 & Environment.NewLine & "Please make sure at lease one StartStroking_ file exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            inputString = inputString.Replace("@InterruptStartStroking", "")
            ssh.JustShowedBlogImage = True
        End If

        If inputString.Contains("@Interrupt(") Then
            Dim InterruptClean As String = inputString
            Dim StartIndex As Integer = InterruptClean.IndexOf("@Interrupt(") + 11
            For i As Integer = 1 To StartIndex
                InterruptClean = InterruptClean.Remove(0, 1)
            Next
            Dim InterruptS As String() = InterruptClean.Split(")")
            InterruptClean = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\" & InterruptS(0) & ".txt"

            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt\" & InterruptS(0) & ".txt") Then

                ssh.FirstRound = False
                ssh.CBTCockFlag = False
                ssh.CBTBallsFlag = False
                ssh.CBTBothFlag = False
                ssh.CustomTask = False
                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
                StrokeTimer.Stop()
                StrokeTauntTimer.Stop()

                CensorshipTimer.Stop()
                RLGLTimer.Stop()
                TnASlides.Stop()
                AvoidTheEdge.Stop()
                EdgeTauntTimer.Stop()
                HoldEdgeTimer.Stop()
                HoldEdgeTauntTimer.Stop()
                AvoidTheEdgeTaunts.Stop()
                RLGLTauntTimer.Stop()
                VideoTauntTimer.Stop()
                EdgeCountTimer.Stop()

                ssh.FileText = InterruptClean
                ssh.LockImage = False
                If ssh.SlideshowLoaded = True Then
                    ImageSlideShowNextButton.Enabled = True
                    ImageSlideShowPreviousButton.Enabled = True
                    PicStripTSMIdommeSlideshow.Enabled = True
                End If
                ssh.StrokeTauntVal = -1
                ssh.ScriptTick = 3
                ScriptTimer.Start()
                ssh.ShowModule = True

                ssh.MiniScript = False

            Else
                MessageBox.Show(Me, InterruptS(0) & ".txt was not found in " & Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Interrupt!" & Environment.NewLine _
                 & Environment.NewLine & "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            inputString = inputString.Replace("@Interrupt(" & InterruptS(0) & ")", "")
            ssh.JustShowedBlogImage = True
        End If

        If inputString.Contains("@BookmarkModule") Then
            ssh.BookmarkModule = True
            ssh.BookmarkModuleFile = ssh.FileText
            ssh.BookmarkModuleLine = ssh.StrokeTauntVal + 1
            inputString = inputString.Replace("@BookmarkModule", "")
        End If

        If inputString.Contains("@BookmarkLink") Then
            ssh.BookmarkLink = True
            ssh.BookmarkLinkFile = ssh.FileText
            ssh.BookmarkLinkLine = ssh.StrokeTauntVal + 1
            inputString = inputString.Replace("@BookmarkLink", "")
        End If

        If inputString.Contains("@SendDailyTasks") Then
            CreateTaskLetter()
            inputString = inputString.Replace("@SendDailyTasks", "")
        End If

        If inputString.Contains("@EdgingHold") Then

            ssh.DomTypeCheck = True
            ssh.SubEdging = False
            ssh.SubStroking = False
            ssh.SubHoldingEdge = True
            EdgeTauntTimer.Stop()
            'DomChat = "#HoldTheEdge"

            ssh.HoldEdgeTick = ssh.HoldEdgeChance

            Dim HoldEdgeMin As Integer = FrmSettings.HoldEdgeMinimum.Value
            If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then HoldEdgeMin *= 60

            Dim HoldEdgeMax As Integer = FrmSettings.HoldEdgeMaximum.Value
            If FrmSettings.LBLMaxHold.Text = "minutes" Then HoldEdgeMax *= 60

            If ssh.ExtremeHold = True Then
                HoldEdgeMin = FrmSettings.ExtremeEdgeHoldMinimum.Value * 60
                HoldEdgeMax = FrmSettings.ExtremeEdgeHoldMaximum.Value * 60
            End If

            If ssh.LongHold = True Then
                HoldEdgeMin = FrmSettings.LongEdgeHoldMinimum.Value * 60
                HoldEdgeMax = FrmSettings.LongEdgeHoldMaximum.Value * 60
            End If


            If HoldEdgeMax < HoldEdgeMin Then HoldEdgeMax = HoldEdgeMin + 1

            ssh.HoldEdgeTick = ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1)
            If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10

            ssh.HoldEdgeTime = 0

            HoldEdgeTimer.Start()
            HoldEdgeTauntTimer.Start()

            Do
                Application.DoEvents()
            Loop Until ssh.DomTypeCheck = False


            inputString = inputString.Replace("@EdgingHold", "")
        End If

        If inputString.Contains("@EdgingStop") Then

            ssh.DomTypeCheck = True
            ssh.SubEdging = False
            ssh.SubStroking = False
            EdgeTauntTimer.Stop()
            'DomChat = "#StopStrokingEdge"

            Do
                Application.DoEvents()
            Loop Until ssh.DomTypeCheck = False

            inputString = inputString.Replace("@EdgingStop", "")
        End If

        'Github Patch  If StringClean.Contains("@EdgingDecide") Then
        If inputString.Contains("@DecideEdge") Then

            ssh.TempVal = ssh.randomizer.Next(0, 101)

            If ssh.TempVal < 51 Then

                ssh.DomTypeCheck = True
                ssh.SubEdging = False
                ssh.SubStroking = False
                ssh.SubHoldingEdge = True
                EdgeTauntTimer.Stop()
                StrokePace = 0
                ssh.DomChat = "#HoldTheEdge"
                If ssh.Contact1Stroke = True Then
                    ssh.DomChat = "@Contact1 #HoldTheEdge"
                    ' Github Patch Contact1Stroke = False
                End If
                If ssh.Contact2Stroke = True Then
                    ssh.DomChat = "@Contact2 #HoldTheEdge"
                    ' Github Patch Contact2Stroke = False
                End If
                If ssh.Contact3Stroke = True Then
                    ssh.DomChat = "@Contact3 #HoldTheEdge"
                    ' Github Patch Contact3Stroke = False
                End If

                ssh.HoldEdgeTick = ssh.HoldEdgeChance

                Dim HoldEdgeMin As Integer = FrmSettings.HoldEdgeMinimum.Value
                If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then HoldEdgeMin *= 60

                Dim HoldEdgeMax As Integer = FrmSettings.HoldEdgeMaximum.Value
                If FrmSettings.LBLMaxHold.Text = "minutes" Then HoldEdgeMax *= 60

                If HoldEdgeMax < HoldEdgeMin Then HoldEdgeMax = HoldEdgeMin + 1

                ssh.HoldEdgeTick = ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1)
                If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10

                ssh.HoldEdgeTime = 0

                HoldEdgeTimer.Start()
                HoldEdgeTauntTimer.Start()

            Else

                ssh.DomTypeCheck = True
                ssh.SubEdging = False
                ssh.SubStroking = False
                EdgeTauntTimer.Stop()
                ssh.DomChat = "#StopStrokingEdge"
                If ssh.Contact1Stroke = True Then
                    ssh.DomChat = "@Contact1 #StopStrokingEdge"
                    ssh.Contact1Stroke = False
                End If
                If ssh.Contact2Stroke = True Then
                    ssh.DomChat = "@Contact2 #StopStrokingEdge"
                    ssh.Contact2Stroke = False
                End If
                If ssh.Contact3Stroke = True Then
                    ssh.DomChat = "@Contact3 #StopStrokingEdge"
                    ssh.Contact3Stroke = False
                End If

            End If

            Do
                Application.DoEvents()
            Loop Until ssh.DomTypeCheck = False


            inputString = inputString.Replace("@DecideEdge", "")
        End If

        If inputString.Contains("@CheckVideo") Then
            ssh.VideoCheck = True
            RandomVideo()
            If ssh.NoVideo = True Then
                ssh.FileGoto = "(No Videos Found)"
            Else
                ssh.FileGoto = "(Videos Found)"
            End If
            ssh.VideoCheck = False
            ssh.NoVideo = False
            ssh.SkipGotoLine = True
            GetGoto()
            inputString = inputString.Replace("@CheckVideo", "")
        End If

        If inputString.Contains("@PlayCensorshipSucks") Then

            RandomVideo()

            If ssh.NoVideo = False Then
                ssh.ScriptVideoTease = "Censorship Sucks"
                ssh.ScriptVideoTeaseFlag = True
                ssh.ScriptVideoTeaseFlag = False
                ssh.CensorshipGame = True
                ssh.VideoTease = True
                ssh.CensorshipTick = ssh.randomizer.Next(FrmSettings.NBCensorHideMin.Value, FrmSettings.NBCensorHideMax.Value + 1)
                CensorshipTimer.Start()
            End If

            inputString = inputString.Replace("@PlayCensorshipSucks", "")
        End If

        If inputString.Contains("@VitalSubAssignment") Then
            ' Read all lines of the given file.
            Dim AssignList As List(Of String) = Txt2List(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Apps\VitalSub\Assignments.txt")

            Dim TempAssign As String

            Try
                AssignList = FilterList(AssignList)
                TempAssign = AssignList(ssh.randomizer.Next(0, AssignList.Count))
            Catch
                TempAssign = "ERROR: VitalSub Assign"
            End Try

            Dim TempArray As String() = Split(TempAssign)

            For i As Integer = TempArray.Count - 1 To 0 Step -1
                If TempArray(i).Contains("@") Then TempArray(i) = ""
            Next


            CLBExercise.Items.Add(TempAssign)
            SaveExercise()
            CBVitalSubDomTask.Checked = False
            My.Settings.VitalSubAssignments = False
            inputString = inputString.Replace("@VitalSubAssignment", "")
        End If

        If inputString.Contains("@PlayAvoidTheEdge") Then
            ' #### Reboot

            RandomVideo()

            If ssh.NoVideo = False Then

                ScriptTimer.Stop()
                ssh.SubStroking = True
                ssh.TempStrokeTauntVal = ssh.StrokeTauntVal
                ssh.TempFileText = ssh.FileText
                ssh.ScriptVideoTease = "Avoid The Edge"
                ssh.ScriptVideoTeaseFlag = True
                ssh.AvoidTheEdgeStroking = True
                ssh.AvoidTheEdgeGame = True
                ssh.ScriptVideoTeaseFlag = False
                ssh.VideoTease = True
                ssh.StartStrokingCount += 1
                StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
                StrokePace = 50 * Math.Round(StrokePace / 50)
                ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value
                AvoidTheEdgeTaunts.Start()

            End If

            inputString = inputString.Replace("@PlayAvoidTheEdge", "")
        End If

        If inputString.Contains("@ResumeAvoidTheEdge") Then
            DomWMP.Ctlcontrols.play()
            ScriptTimer.Stop()
            ssh.AvoidTheEdgeStroking = True
            ssh.SubStroking = True
            ssh.StartStrokingCount += 1
            ssh.VideoTease = True
            StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
            StrokePace = 50 * Math.Round(StrokePace / 50)
            ssh.AvoidTheEdgeTick = 120 / FrmSettings.TauntSlider.Value
            AvoidTheEdgeTaunts.Start()
            inputString = inputString.Replace("@ResumeAvoidTheEdge", "")
        End If

        If inputString.Contains("@PlayRedLightGreenLight") Then
            ' #### Reboot

            RandomVideo()

            If ssh.NoVideo = False Then

                ScriptTimer.Stop()
                ssh.SubStroking = True
                ssh.TempStrokeTauntVal = ssh.StrokeTauntVal
                ssh.TempFileText = ssh.FileText
                ssh.ScriptVideoTease = "RLGL"
                ssh.ScriptVideoTeaseFlag = True
                'AvoidTheEdgeStroking = True
                ssh.RLGLGame = True

                ssh.ScriptVideoTeaseFlag = False
                ssh.VideoTease = True
                ssh.RLGLTick = ssh.randomizer.Next(FrmSettings.NBGreenLightMin.Value, FrmSettings.NBGreenLightMax.Value + 1)
                RLGLTimer.Start()
                ssh.StartStrokingCount += 1
                StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
                StrokePace = 50 * Math.Round(StrokePace / 50)
                'VideoTauntTick = randomizer.Next(20, 31)
                'VideoTauntTimer.Start()

            End If
            inputString = inputString.Replace("@PlayRedLightGreenLight", "")
        End If

        If inputString.Contains("@PlayVideo[") Then

            Dim VideoFlag As String = GetParentheses(inputString, "@PlayVideo[")
            Dim VideoClean As String

            If inputString.Contains("@JumpVideo") Then
                ssh.JumpVideo = True
                inputString = inputString.Replace("@JumpVideo", "")
            End If

            If VideoFlag.Contains(":\") Then
                VideoClean = VideoFlag

                If File.Exists(VideoClean) Then
                    DomWMP.URL = VideoClean
                    DomWMP.Visible = True
                    mainPictureBox.Visible = False
                    ssh.TeaseVideo = True

                    If ssh.JumpVideo = True Then

                        Do
                            Application.DoEvents()
                        Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)

                        Dim VideoLength As Integer = DomWMP.currentMedia.duration
                        Dim VidLow As Integer = VideoLength * 0.4
                        Dim VidHigh As Integer = VideoLength * 0.9
                        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                        DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

                    End If

                    ssh.JumpVideo = False

                Else
                    MessageBox.Show(Me, Path.GetFileName(VideoClean) & " was not found in " & Path.GetDirectoryName(VideoClean) & "!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

                GoTo ExternalVideo

            Else
                VideoClean = Application.StartupPath & "\Video\" & VideoFlag
                VideoClean = VideoClean.Replace("\\", "\")
            End If

            If VideoClean.Contains("*") Then

                Dim VideoList As New List(Of String)

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Path.GetDirectoryName(VideoClean), FileIO.SearchOption.SearchTopLevelOnly, Path.GetFileName(VideoClean))
                    VideoList.Add(foundFile)
                Next

                If VideoList.Count > 0 Then
                    DomWMP.URL = VideoList(ssh.randomizer.Next(0, VideoList.Count))
                    DomWMP.Visible = True
                    mainPictureBox.Visible = False
                    ssh.TeaseVideo = True

                    If ssh.JumpVideo = True Then

                        Do
                            Application.DoEvents()
                        Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)

                        Dim VideoLength As Integer = DomWMP.currentMedia.duration
                        Dim VidLow As Integer = VideoLength * 0.4
                        Dim VidHigh As Integer = VideoLength * 0.9
                        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                        DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

                    End If

                    ssh.JumpVideo = False
                Else
                    MessageBox.Show(Me, "No videos matching " & Path.GetFileName(VideoClean) & " were found in " & Path.GetDirectoryName(VideoClean) & "!" & Environment.NewLine & Environment.NewLine &
                      "Please make sure that valid files exist and that the wildcards are applied correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            Else

                If File.Exists(VideoClean) Then
                    DomWMP.URL = VideoClean
                    DomWMP.Visible = True
                    mainPictureBox.Visible = False
                    ssh.TeaseVideo = True

                    If ssh.JumpVideo = True Then

                        Do
                            Application.DoEvents()
                        Loop Until (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying)

                        Dim VideoLength As Integer = DomWMP.currentMedia.duration
                        Dim VidLow As Integer = VideoLength * 0.4
                        Dim VidHigh As Integer = VideoLength * 0.9
                        Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                        DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

                    End If

                    ssh.JumpVideo = False

                Else
                    MessageBox.Show(Me, Path.GetFileName(VideoClean) & " was not found in " & Application.StartupPath & "\Video!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            End If

ExternalVideo:

            inputString = inputString.Replace("@PlayVideo[" & VideoFlag & "]", "")
        End If

        If inputString.Contains("@PlayAudio[") Then

            Dim AudioFlag As String = GetParentheses(inputString, "@PlayAudio[")
            ' Github Patch Dim AudioClean As String = Application.StartupPath & "\Video\" & AudioFlag
            Dim AudioClean As String

            If AudioFlag.Contains(":\") And Not AudioFlag.Contains("*") Then
                AudioClean = AudioFlag

                If File.Exists(AudioClean) Then
                    DomWMP.URL = AudioClean
                Else
                    MessageBox.Show(Me, Path.GetFileName(AudioClean) & " was not found in " & Path.GetDirectoryName(AudioClean) & "!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

                GoTo ExternalAudio

            Else

                AudioClean = Application.StartupPath & "\Audio\" & AudioFlag
                AudioClean = AudioClean.Replace("\\", "\")
            End If



            If AudioClean.Contains("*") Then

                Dim AudioList As New List(Of String)

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Path.GetDirectoryName(AudioClean), FileIO.SearchOption.SearchTopLevelOnly, Path.GetFileName(AudioClean))
                    AudioList.Add(foundFile)
                Next

                If AudioList.Count > 0 Then
                    DomWMP.URL = AudioList(ssh.randomizer.Next(0, AudioList.Count))
                Else
                    MessageBox.Show(Me, "No audio files matching " & Path.GetFileName(AudioClean) & " were found in " & Path.GetDirectoryName(AudioClean) & "!" & Environment.NewLine & Environment.NewLine &
                      "Please make sure that valid files exist and that the wildcards are applied correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            Else


                If File.Exists(AudioClean) Then
                    DomWMP.URL = AudioClean
                Else
                    MessageBox.Show(Me, Path.GetFileName(AudioClean) & " was not found in " & Application.StartupPath & "\Audio!" & Environment.NewLine & Environment.NewLine &
                     "Please make sure the file exists and that it is spelled correctly in the script.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If

            End If

ExternalAudio:

            inputString = inputString.Replace("@PlayAudio[" & AudioFlag & "]", "")

        End If

        If inputString.Contains("@PlayVideo(") Then


            Dim VidFlag As String = GetParentheses(inputString, "@PlayVideo(")
            Dim VidInt As Integer = Val(VidFlag)
            If UCase(VidFlag).Contains("M") Then VidInt *= 60

            If inputString.Contains("@JumpVideo") Then
                ssh.JumpVideo = True
                inputString = inputString.Replace("@JumpVideo", "")
            End If

            ssh.RandomizerVideo = True
            RandomVideo()

            If ssh.NoVideo = False Then
                ssh.TeaseVideo = True
                ssh.VideoTick = VidInt
                VideoTimer.Start()
            Else
                MessageBox.Show(Me, "No videos were found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If

            ssh.RandomizerVideo = False
            inputString = inputString.Replace("@PlayVideo", "")
        End If

        If inputString.Contains("@JumpVideo") Then

            If (DomWMP.playState = WMPLib.WMPPlayState.wmppsPlaying) Then
                Dim VideoLength As Integer = DomWMP.currentMedia.duration
                Dim VidLow As Integer = VideoLength * 0.4
                Dim VidHigh As Integer = VideoLength * 0.9
                Dim VidPoint As Integer = ssh.randomizer.Next(VidLow, VidHigh)

                DomWMP.Ctlcontrols.currentPosition = VideoLength - VidPoint

            End If
            inputString = inputString.Replace("@JumpVideo", "")
        End If

        If inputString.Contains("@AddStrokeTime(") Then

            Dim OriginalFlag As String = ""

            If StrokeTimer.Enabled = True Then

                Dim StrokeFlag As String = GetParentheses(inputString, "@AddStrokeTime(")
                OriginalFlag = StrokeFlag
                Dim StrokeSeconds As Integer

                If StrokeFlag.Contains(",") Then
                    StrokeFlag = FixCommas(StrokeFlag)
                    Dim StrokeFlagArray As String() = StrokeFlag.Split(",")
                    Dim Stroke1 As Integer = Val(StrokeFlagArray(0))
                    Dim Stroke2 As Integer = Val(StrokeFlagArray(1))
                    If UCase(StrokeFlagArray(0)).Contains("M") Then Stroke1 *= 60
                    If UCase(StrokeFlagArray(1)).Contains("M") Then Stroke2 *= 60
                    If UCase(StrokeFlagArray(0)).Contains("H") Then Stroke1 *= 3600
                    If UCase(StrokeFlagArray(1)).Contains("H") Then Stroke2 *= 3600
                    StrokeSeconds = ssh.randomizer.Next(Stroke1, Stroke2 + 1)
                Else
                    StrokeSeconds = Val(StrokeFlag)
                    If UCase(GetParentheses(inputString, "@AddStrokeTime(")).Contains("M") Then StrokeSeconds *= 60
                    If UCase(GetParentheses(inputString, "@AddStrokeTime(")).Contains("H") Then StrokeSeconds *= 3600
                End If
                ssh.StrokeTick += StrokeSeconds
            End If
            inputString = inputString.Replace("@AddStrokeTime(" & OriginalFlag & ")", "")
        End If

        If inputString.Contains("@RemoveStrokeTime(") Then

            Dim OriginalFlag As String = ""

            If StrokeTimer.Enabled = True Then

                Dim StrokeFlag As String = GetParentheses(inputString, "@RemoveStrokeTime(")
                OriginalFlag = StrokeFlag
                Dim StrokeSeconds As Integer

                If StrokeFlag.Contains(",") Then
                    StrokeFlag = FixCommas(StrokeFlag)
                    Dim StrokeFlagArray As String() = StrokeFlag.Split(",")
                    Dim Stroke1 As Integer = Val(StrokeFlagArray(0))
                    Dim Stroke2 As Integer = Val(StrokeFlagArray(1))
                    If UCase(StrokeFlagArray(0)).Contains("M") Then Stroke1 *= 60
                    If UCase(StrokeFlagArray(1)).Contains("M") Then Stroke2 *= 60
                    If UCase(StrokeFlagArray(0)).Contains("H") Then Stroke1 *= 3600
                    If UCase(StrokeFlagArray(1)).Contains("H") Then Stroke2 *= 3600
                    StrokeSeconds = ssh.randomizer.Next(Stroke1, Stroke2 + 1)
                Else
                    StrokeSeconds = Val(StrokeFlag)
                    If UCase(GetParentheses(inputString, "@RemoveStrokeTime(")).Contains("M") Then StrokeSeconds *= 60
                    If UCase(GetParentheses(inputString, "@RemoveStrokeTime(")).Contains("H") Then StrokeSeconds *= 3600
                End If
                ssh.StrokeTick -= StrokeSeconds
                If ssh.StrokeTick < 0 Then ssh.StrokeTick = 5
            End If
            inputString = inputString.Replace("@RemoveStrokeTime(" & OriginalFlag & ")", "")
        End If

        If inputString.Contains("@AddStrokeTime") Then
            If StrokeTimer.Enabled = True Then
                If FrmSettings.CBTauntCycleDD.Checked = True Then
                    If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick += ssh.randomizer.Next(1, 3) * 60
                    If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick += ssh.randomizer.Next(1, 4) * 60
                    If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick += ssh.randomizer.Next(3, 6) * 60
                    If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick += ssh.randomizer.Next(4, 8) * 60
                    If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick += ssh.randomizer.Next(5, 11) * 60
                Else
                    ssh.StrokeTick += ssh.randomizer.Next(FrmSettings.NBTauntCycleMin.Value * 60, FrmSettings.NBTauntCycleMax.Value * 60)
                End If
            End If
            inputString = inputString.Replace("@AddStrokeTime", "")
        End If

        If inputString.Contains("@RemoveStrokeTime") Then
            If StrokeTimer.Enabled = True Then
                ssh.StrokeTick -= ssh.StrokeTick / 2
            End If
            inputString = inputString.Replace("@RemoveStrokeTime", "")
        End If


        If inputString.Contains("@AddEdgeHoldTime(") Then

            Dim OriginalFlag As String = ""

            If HoldEdgeTimer.Enabled = True Then

                Dim HoldEdgeFlag As String = GetParentheses(inputString, "@AddEdgeHoldTime(")
                OriginalFlag = HoldEdgeFlag
                Dim HoldEdgeSeconds As Integer

                If HoldEdgeFlag.Contains(",") Then
                    HoldEdgeFlag = FixCommas(HoldEdgeFlag)
                    Dim HoldEdgeFlagArray As String() = HoldEdgeFlag.Split(",")
                    Dim HoldEdge1 As Integer = Val(HoldEdgeFlagArray(0))
                    Dim HoldEdge2 As Integer = Val(HoldEdgeFlagArray(1))
                    If UCase(HoldEdgeFlagArray(0)).Contains("M") Then HoldEdge1 *= 60
                    If UCase(HoldEdgeFlagArray(1)).Contains("M") Then HoldEdge2 *= 60
                    If UCase(HoldEdgeFlagArray(0)).Contains("H") Then HoldEdge1 *= 3600
                    If UCase(HoldEdgeFlagArray(1)).Contains("H") Then HoldEdge2 *= 3600
                    HoldEdgeSeconds = ssh.randomizer.Next(HoldEdge1, HoldEdge2 + 1)
                Else
                    HoldEdgeSeconds = Val(HoldEdgeFlag)
                    If UCase(GetParentheses(inputString, "@AddEdgeHoldTime(")).Contains("M") Then HoldEdgeSeconds *= 60
                    If UCase(GetParentheses(inputString, "@AddEdgeHoldTime(")).Contains("H") Then HoldEdgeSeconds *= 3600
                End If
                ssh.HoldEdgeTick += HoldEdgeSeconds
            End If
            inputString = inputString.Replace("@AddEdgeHoldTime(" & OriginalFlag & ")", "")
        End If

        If inputString.Contains("@RemoveEdgeHoldTime(") Then

            Dim OriginalFlag As String = ""

            If HoldEdgeTimer.Enabled = True Then

                Dim HoldEdgeFlag As String = GetParentheses(inputString, "@RemoveEdgeHoldTime(")
                OriginalFlag = HoldEdgeFlag
                Dim HoldEdgeSeconds As Integer

                If HoldEdgeFlag.Contains(",") Then
                    HoldEdgeFlag = FixCommas(HoldEdgeFlag)
                    Dim HoldEdgeFlagArray As String() = HoldEdgeFlag.Split(",")
                    Dim HoldEdge1 As Integer = Val(HoldEdgeFlagArray(0))
                    Dim HoldEdge2 As Integer = Val(HoldEdgeFlagArray(1))
                    If UCase(HoldEdgeFlagArray(0)).Contains("M") Then HoldEdge1 *= 60
                    If UCase(HoldEdgeFlagArray(1)).Contains("M") Then HoldEdge2 *= 60
                    If UCase(HoldEdgeFlagArray(0)).Contains("H") Then HoldEdge1 *= 3600
                    If UCase(HoldEdgeFlagArray(1)).Contains("H") Then HoldEdge2 *= 3600
                    HoldEdgeSeconds = ssh.randomizer.Next(HoldEdge1, HoldEdge2 + 1)
                Else
                    HoldEdgeSeconds = Val(HoldEdgeFlag)
                    If UCase(GetParentheses(inputString, "@RemoveEdgeHoldTime(")).Contains("M") Then HoldEdgeSeconds *= 60
                    If UCase(GetParentheses(inputString, "@RemoveEdgeHoldTime(")).Contains("H") Then HoldEdgeSeconds *= 3600
                End If
                ssh.HoldEdgeTick -= HoldEdgeSeconds
                If ssh.HoldEdgeTick < 5 Then ssh.HoldEdgeTick = 5
            End If
            inputString = inputString.Replace("@RemoveEdgeHoldTime(" & OriginalFlag & ")", "")
        End If


        If inputString.Contains("@AddEdgeHoldTime") Then

            If HoldEdgeTimer.Enabled = True Then
                Dim HoldEdgeMin As Integer = FrmSettings.HoldEdgeMinimum.Value
                If FrmSettings.HoldEdgeMinimumUnits.Text = "minutes" Then HoldEdgeMin *= 60

                Dim HoldEdgeMax As Integer = FrmSettings.HoldEdgeMaximum.Value
                If FrmSettings.LBLMaxHold.Text = "minutes" Then HoldEdgeMax *= 60

                If HoldEdgeMax < HoldEdgeMin Then HoldEdgeMax = HoldEdgeMin + 1

                ssh.HoldEdgeTick += ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1)
                If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10
            End If
            inputString = inputString.Replace("@AddEdgeHoldTime", "")
        End If

        If inputString.Contains("@RemoveEdgeHoldTime") Then
            If HoldEdgeTimer.Enabled = True Then
                ssh.HoldEdgeTick = ssh.HoldEdgeTick / 2
                If ssh.HoldEdgeTick < 10 Then ssh.HoldEdgeTick = 10
            End If
            inputString = inputString.Replace("@RemoveEdgeHoldTime", "")
        End If

        If inputString.Contains("@AddTeaseTime(") Then

            Dim OriginalFlag As String = ""

            If TeaseTimer.Enabled = True Then

                Dim TeaseFlag As String = GetParentheses(inputString, "@AddTeaseTime(")
                OriginalFlag = TeaseFlag
                Dim TeaseSeconds As Integer

                If TeaseFlag.Contains(",") Then
                    TeaseFlag = FixCommas(TeaseFlag)
                    Dim TeaseFlagArray As String() = TeaseFlag.Split(",")
                    Dim Tease1 As Integer = Val(TeaseFlagArray(0))
                    Dim Tease2 As Integer = Val(TeaseFlagArray(1))
                    If UCase(TeaseFlagArray(0)).Contains("M") Then Tease1 *= 60
                    If UCase(TeaseFlagArray(1)).Contains("M") Then Tease2 *= 60
                    If UCase(TeaseFlagArray(0)).Contains("H") Then Tease1 *= 3600
                    If UCase(TeaseFlagArray(1)).Contains("H") Then Tease2 *= 3600
                    TeaseSeconds = ssh.randomizer.Next(Tease1, Tease2 + 1)
                Else
                    TeaseSeconds = Val(TeaseFlag)
                    If UCase(GetParentheses(inputString, "@AddTeaseTime(")).Contains("M") Then TeaseSeconds *= 60
                    If UCase(GetParentheses(inputString, "@AddTeaseTime(")).Contains("H") Then TeaseSeconds *= 3600
                End If
                ssh.TeaseTick += TeaseSeconds
            End If
            inputString = inputString.Replace("@AddTeaseTime(" & OriginalFlag & ")", "")
        End If

        If inputString.Contains("@RemoveTeaseTime(") Then

            Dim OriginalFlag As String = ""

            If TeaseTimer.Enabled = True Then

                Dim TeaseFlag As String = GetParentheses(inputString, "@RemoveTeaseTime(")
                OriginalFlag = TeaseFlag
                Dim TeaseSeconds As Integer

                If TeaseFlag.Contains(",") Then
                    TeaseFlag = FixCommas(TeaseFlag)
                    Dim TeaseFlagArray As String() = TeaseFlag.Split(",")
                    Dim Tease1 As Integer = Val(TeaseFlagArray(0))
                    Dim Tease2 As Integer = Val(TeaseFlagArray(1))
                    If UCase(TeaseFlagArray(0)).Contains("M") Then Tease1 *= 60
                    If UCase(TeaseFlagArray(1)).Contains("M") Then Tease2 *= 60
                    If UCase(TeaseFlagArray(0)).Contains("H") Then Tease1 *= 3600
                    If UCase(TeaseFlagArray(1)).Contains("H") Then Tease2 *= 3600
                    TeaseSeconds = ssh.randomizer.Next(Tease1, Tease2 + 1)
                Else
                    TeaseSeconds = Val(TeaseFlag)
                    If UCase(GetParentheses(inputString, "@RemoveTeaseTime(")).Contains("M") Then TeaseSeconds *= 60
                    If UCase(GetParentheses(inputString, "@RemoveTeaseTime(")).Contains("H") Then TeaseSeconds *= 3600
                End If
                ssh.TeaseTick -= TeaseSeconds
                If ssh.TeaseTick < 5 Then ssh.TeaseTick = 5
            End If
            inputString = inputString.Replace("@RemoveTeaseTime(" & OriginalFlag & ")", "")
        End If

        If inputString.Contains("@AddTeaseTime") Then
            If TeaseTimer.Enabled = True Then
                If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
                    If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick += ssh.randomizer.Next(10, 16) * 60
                    If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick += ssh.randomizer.Next(15, 21) * 60
                    If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick += ssh.randomizer.Next(20, 31) * 60
                    If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick += ssh.randomizer.Next(30, 46) * 60
                    If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick += ssh.randomizer.Next(45, 61) * 60
                Else
                    ssh.TeaseTick += ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
                End If
            End If
            inputString = inputString.Replace("@AddTeaseTime", "")
        End If

        If inputString.Contains("@RemoveTeaseTime") Then
            If TeaseTimer.Enabled = True Then
                ssh.TeaseTick = ssh.TeaseTick / 2
            End If
            inputString = inputString.Replace("@RemoveTeaseTime", "")
        End If

        If inputString.Contains("@PlaylistOff") Then
            ssh.Playlist = False
            inputString = inputString.Replace("@PlaylistOff", "")
        End If

        If inputString.Contains("@RapidTextOn") Or inputString.Contains("@RTOn") Then
            ssh.RapidFire = True
            inputString = inputString.Replace("@RapidTextOn", "")
            inputString = inputString.Replace("@RTOn", "")
        End If

        If inputString.Contains("@RapidTextOff") Or inputString.Contains("@RTOff") Then
            ssh.RapidFire = False
            inputString = inputString.Replace("@RapidTextOff", "")
            inputString = inputString.Replace("@RTOff", "")
        End If

        If inputString.Contains("@AddContact1") Or inputString.Contains("@RemoveContact1") Then
            ssh.AddContactTick = 2
            Contact1Timer.Start()
            inputString = inputString.Replace("@AddContact1", "")
            inputString = inputString.Replace("@RemoveContact1", "")
        End If

        If inputString.Contains("@AddContact2") Or inputString.Contains("@RemoveContact2") Then
            ssh.AddContactTick = 2
            Contact2Timer.Start()
            inputString = inputString.Replace("@AddContact2", "")
            inputString = inputString.Replace("@RemoveContact2", "")
        End If

        If inputString.Contains("@AddContact3") Or inputString.Contains("@RemoveContact3") Then
            ssh.AddContactTick = 2
            Contact3Timer.Start()
            inputString = inputString.Replace("@AddContact3", "")
            inputString = inputString.Replace("@RemoveContact3", "")
        End If

        If inputString.Contains("@AddDomme") Or inputString.Contains("@RemoveDomme") Then
            ssh.AddContactTick = 2
            DommeTimer.Start()
            inputString = inputString.Replace("@AddDomme", "")
            inputString = inputString.Replace("@RemoveDomme", "")
        End If


        If inputString.Contains("@NullResponse") Then
            ssh.NullResponse = True
            inputString = inputString.Replace("@NullResponse", "")
        End If

VTSkip:

        If inputString.Contains("@SpeedUpCheck") Then

            If ssh.AskedToSpeedUp = True Then
                ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpREPEAT.txt"
                inputString = ResponseClean(inputString)

            Else

                If StrokePace < 201 Then
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpMAX.txt"
                    inputString = ResponseClean(inputString)

                Else

                    Dim SpeedUpCheck As Integer

                    If FrmSettings.DominationLevel.Value = 1 Then SpeedUpCheck = 70
                    If FrmSettings.DominationLevel.Value = 2 Then SpeedUpCheck = 40
                    If FrmSettings.DominationLevel.Value = 3 Then SpeedUpCheck = 60
                    If FrmSettings.DominationLevel.Value = 4 Then SpeedUpCheck = 50
                    If FrmSettings.DominationLevel.Value = 5 Then SpeedUpCheck = 65

                    Dim SpeedUpVal As Integer = ssh.randomizer.Next(1, 101)

                    If SpeedUpVal > SpeedUpCheck Then

                        ' you can speed up
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpALLOWED.txt"

                    Else

                        ' you can't speed up
                        ssh.AskedToSpeedUp = True
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SpeedUpDENIED.txt"

                    End If

                    inputString = ResponseClean(inputString)

                End If

            End If

            inputString = inputString.Replace("@SpeedUpCheck", "")
            GoTo RinseLatherRepeat
        End If


        If inputString.Contains("@SlowDownCheck") Then

            If ssh.AskedToSpeedUp = True Then
                ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownREPEAT.txt"
                inputString = ResponseClean(inputString)

            Else

                If StrokePace > 999 Then
                    ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownMIN.txt"
                    inputString = ResponseClean(inputString)

                Else

                    Dim SpeedUpCheck As Integer

                    If FrmSettings.DominationLevel.Value = 1 Then SpeedUpCheck = 70
                    If FrmSettings.DominationLevel.Value = 2 Then SpeedUpCheck = 40
                    If FrmSettings.DominationLevel.Value = 3 Then SpeedUpCheck = 60
                    If FrmSettings.DominationLevel.Value = 4 Then SpeedUpCheck = 50
                    If FrmSettings.DominationLevel.Value = 5 Then SpeedUpCheck = 65

                    Dim SpeedUpVal As Integer = ssh.randomizer.Next(1, 101)

                    If SpeedUpVal > SpeedUpCheck Then

                        ' you can speed up
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownALLOWED.txt"

                    Else

                        ' you can't speed up
                        ssh.AskedToSpeedUp = True
                        ssh.ResponseFile = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Vocabulary\Responses\System\SlowDownDENIED.txt"

                    End If

                    inputString = ResponseClean(inputString)

                End If

            End If

            inputString = inputString.Replace("@SlowDownCheck", "")
            GoTo RinseLatherRepeat

        End If

#End Region
        If inputString.Contains("@PlayRiskyPick") Then
            ssh.RiskyDeal = True
            'FrmCardList.RiskyRound += 1
            GamesWindow.TCGames.SelectTab(2)
            GamesWindow.Show()
            GamesWindow.Focus()
            GamesWindow.SetupRiskyPick()
            inputString = inputString.Replace("@PlayRiskyPick", "")
        End If

        If inputString.Contains("@SystemMessage ") Then
            inputString = inputString.Replace("@SystemMessage ", "")
        End If

        If inputString.Contains("@EmoteMessage ") Then
            inputString = inputString.Replace("@EmoteMessage ", "")
        End If

        If inputString.Contains("@CallReturn(") Then
            ssh.ReturnFileText = ssh.FileText
            ssh.ReturnStrokeTauntVal = ssh.StrokeTauntVal
            GetSubState()

            StrokeTimer.Stop()
            StrokeTauntTimer.Stop()
            CensorshipTimer.Stop()
            RLGLTimer.Stop()
            TnASlides.Stop()
            AvoidTheEdge.Stop()
            EdgeTauntTimer.Stop()
            HoldEdgeTimer.Stop()
            HoldEdgeTauntTimer.Stop()
            AvoidTheEdgeTaunts.Stop()
            RLGLTauntTimer.Stop()
            VideoTauntTimer.Stop()
            EdgeCountTimer.Stop()

            ssh.CBTBallsActive = False
            ssh.CBTBallsFlag = False
            ssh.CBTCockFlag = False
            ssh.CBTBothActive = False
            ssh.CBTBothFlag = False
            ssh.CustomTaskActive = False

            If Not ssh.SubGaveUp Then
                ssh.SubEdging = False
                ssh.SubHoldingEdge = False
            End If

            'StopEverything()
            ssh.ReturnFlag = True


            Dim CheckFlag As String = GetParentheses(inputString, "@CallReturn(")
            Dim CallReplace As String = CheckFlag

            If CheckFlag.Contains(",") Then

                CheckFlag = FixCommas(CheckFlag)

                Dim CallSplit As String() = CheckFlag.Split(",")
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CallSplit(0)
                ssh.FileGoto = CallSplit(1)
                ssh.SkipGotoLine = True
                GetGoto()

            Else
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag
                ssh.StrokeTauntVal = -1
            End If
            ssh.ScriptTick = 2
            ScriptTimer.Start()

            inputString = inputString.Replace("@CallReturn(" & CallReplace & ")", "")

        End If

        If inputString.Contains("@Call(") Then

            Dim CheckFlag As String = GetParentheses(inputString, "@Call(")
            Dim CallReplace As String = CheckFlag

            If CheckFlag.Contains(",") Then

                CheckFlag = FixCommas(CheckFlag)

                Dim CallSplit As String() = CheckFlag.Split(",")
                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CallSplit(0)
                ssh.FileGoto = CallSplit(1)
                ssh.SkipGotoLine = True
                GetGoto()

            Else

                ssh.FileText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag
                ssh.StrokeTauntVal = -1

            End If

            inputString = inputString.Replace("@Call(" & CallReplace & ")", "")

        End If


        If inputString.Contains("@CallRandom(") Then

            Dim CheckFlag As String = GetParentheses(inputString, "@CallRandom(")
            Dim CallReplace As String = CheckFlag

            If Not Directory.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag) Then
                MessageBox.Show(Me, "The current script attempted to @Call from a directory that does not exist!" & Environment.NewLine & Environment.NewLine &
                 Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim RandomFile As New List(Of String)
                RandomFile.Clear()
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag & "\", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                    RandomFile.Add(foundFile)
                Next
                If RandomFile.Count < 1 Then
                    MessageBox.Show(Me, "The current script attempted to @Call from a directory that does not contain any scripts!" & Environment.NewLine & Environment.NewLine &
                      Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\" & CheckFlag, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    ssh.FileText = RandomFile(ssh.randomizer.Next(0, RandomFile.Count))
                    ssh.StrokeTauntVal = -1
                End If
            End If
            inputString = inputString.Replace("@CallRandom(" & CallReplace & ")", "")
        End If

        If inputString.Contains("@InterruptsOff") Then
            ssh.DoNotDisturb = True
            inputString = inputString.Replace("@InterruptsOff", "")
        End If

        If inputString.Contains("@InterruptsOn") Then
            ssh.DoNotDisturb = False
            inputString = inputString.Replace("@InterruptsOn", "")
        End If


        If inputString.Contains("@NoTypo") Then
            ssh.TypoSwitch = 0
            inputString = inputString.Replace("@NoTypo", "")
        End If

        If inputString.Contains("@ForceTypo") Then
            ssh.TypoSwitch = 2
            inputString = inputString.Replace("@ForceTypo", "")
        End If

        If inputString.Contains("@TyposOff") Then
            ssh.TyposDisabled = True
            inputString = inputString.Replace("@TyposOff", "")
        End If

        If inputString.Contains("@TyposOn") Then
            ssh.TyposDisabled = False
            inputString = inputString.Replace("@TyposOn", "")
        End If

        If inputString.Contains("@GoodMood(") Then

            Dim MoodFlag As String = GetParentheses(inputString, "@GoodMood(")

            If ssh.DommeMood > FrmSettings.NBDomMoodMax.Value Then
                ssh.FileGoto = MoodFlag
                ssh.SkipGotoLine = True
                GetGoto()
            End If

            inputString = inputString.Replace("@GoodMood(" & MoodFlag & ")", "")
        End If

        If inputString.Contains("@BadMood(") Then

            Dim MoodFlag As String = GetParentheses(inputString, "@BadMood(")

            If ssh.DommeMood < FrmSettings.NBDomMoodMin.Value Then
                ssh.FileGoto = MoodFlag
                ssh.SkipGotoLine = True
                GetGoto()
            End If

            inputString = inputString.Replace("@BadMood(" & MoodFlag & ")", "")
        End If

        If inputString.Contains("@NeutralMood(") Then

            Dim MoodFlag As String = GetParentheses(inputString, "@NeutralMood(")

            If ssh.DommeMood >= FrmSettings.NBDomMoodMin.Value And ssh.DommeMood <= FrmSettings.NBDomMoodMax.Value Then
                ssh.FileGoto = MoodFlag
                ssh.SkipGotoLine = True
                GetGoto()
            End If

            inputString = inputString.Replace("@NeutralMood(" & MoodFlag & ")", "")
        End If

        If inputString.Contains("@MoodUp") Then
            ssh.DommeMood += 1
            If ssh.DommeMood > 10 Then ssh.DommeMood = 10
            inputString = inputString.Replace("@MoodUp", "")
        End If

        If inputString.Contains("@MoodDown") Then
            ssh.DommeMood -= 1
            If ssh.DommeMood < 1 Then ssh.DommeMood = 1
            inputString = inputString.Replace("@MoodDown", "")
        End If

        If inputString.Contains("@MoodBest") Then
            ssh.DommeMood = 10
            inputString = inputString.Replace("@MoodBest", "")
        End If

        If inputString.Contains("@MoodWorst") Then
            ssh.DommeMood = 1
            inputString = inputString.Replace("@MoodWorst", "")
        End If

        If inputString.Contains("@Timeout(") Then

            Dim TimeFlag As String = GetParentheses(inputString, "@Timeout(")
            Dim OriginalFlag As String = TimeFlag

            TimeFlag = FixCommas(TimeFlag)

            Dim TimeArray As String() = TimeFlag.Split(",")

            ssh.FileGoto = TimeArray(1)
            ssh.TimeoutTick = Val(TimeArray(0))
            TimeoutTimer.Start()

            inputString = inputString.Replace("@Timeout(" & OriginalFlag & ")", "")
        End If

        If inputString.Contains("@BallTorture+1") Then
            ssh.CBTBallsCount += 1
            inputString = inputString.Replace("@BallTorture+1", "")
        End If

        If inputString.Contains("@CockTorture+1") Then
            ssh.CBTCockCount += 1
            inputString = inputString.Replace("@CockTorture+1", "")
        End If


        If inputString.Contains("@EndTaunts") Then
            ssh.StrokeTick = 0
            inputString = inputString.Replace("@EndTaunts", "")
        End If


        If inputString.Contains("@ResponseYes(") Then
            ssh.ResponseYes = GetParentheses(inputString, "@ResponseYes(")
            inputString = inputString.Replace("@ResponseYes(" & GetParentheses(inputString, "@ResponseYes(") & ")", "")
        End If

        If inputString.Contains("@ResponseNo(") Then
            ssh.ResponseNo = GetParentheses(inputString, "@ResponseNo(")
            inputString = inputString.Replace("@ResponseNo(" & GetParentheses(inputString, "@ResponseNo(") & ")", "")
        End If


        If inputString.Contains("@SetModule(") Then
            Dim TempMod As String = GetParentheses(inputString, "@SetModule(")

            If TempMod.Contains(",") Then
                TempMod = FixCommas(TempMod)
                Dim TempArray As String() = TempMod.Split(",")
                TempMod = TempArray(0)
                ssh.SetModuleGoto = TempArray(1)

            End If


            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Modules\" & TempMod & ".txt") Then
                ssh.SetModule = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Modules\" & TempMod & ".txt"
            End If
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Modules\" & TempMod & ".txt") Then
                ssh.SetModule = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Modules\" & TempMod & ".txt"
            End If

            If ssh.SetModule = "" Then ssh.SetModuleGoto = ""

            inputString = inputString.Replace("@SetModule(" & GetParentheses(inputString, "@SetModule(") & ")", "")
        End If

        If inputString.Contains("@SetLink(") Then
            Dim TempMod As String = GetParentheses(inputString, "@SetLink(")
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Link\" & TempMod & ".txt") Then
                ssh.SetLink = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\Link\" & TempMod & ".txt"
            End If
            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Link\" & TempMod & ".txt") Then
                ssh.SetLink = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Link\" & TempMod & ".txt"
            End If
            inputString = inputString.Replace("@SetLink(" & GetParentheses(inputString, "@SetLink(") & ")", "")
        End If

        If inputString.Contains("@FollowUp(") And ssh.FollowUp = "" Then
            ssh.FollowUp = GetParentheses(inputString, "@FollowUp(")
            inputString = inputString.Replace("@FollowUp(" & ssh.FollowUp & ")", "")
        End If


        If inputString.Contains("@FollowUp") And ssh.FollowUp = "" Then

            Dim FollowTemp As String
            Dim TSStartIndex As Integer
            Dim TSEndIndex As Integer

            TSStartIndex = inputString.IndexOf("@FollowUp") + 9
            TSEndIndex = inputString.IndexOf("@FollowUp") + 11

            FollowTemp = inputString.Substring(TSStartIndex, TSEndIndex - TSStartIndex).Trim

            Dim FollowVal As Integer

            FollowVal = Val(FollowTemp)

            ssh.TempVal = ssh.randomizer.Next(1, 101)

            Dim FollowLineTemp As String
            FollowLineTemp = GetParentheses(inputString, "@FollowUp" & FollowTemp & "(")

            If ssh.TempVal <= FollowVal Then ssh.FollowUp = FollowLineTemp

            inputString = inputString.Replace("@FollowUp" & FollowTemp & "(" & FollowLineTemp & ")", "")

        End If

        If inputString.Contains("@Worship(") Then
            Dim WorshipTemp As String = GetParentheses(inputString, "@Worship(")
            If UCase(WorshipTemp) = "ASS" Then ssh.WorshipTarget = "Ass"
            If UCase(WorshipTemp) = "BOOBS" Then ssh.WorshipTarget = "Boobs"
            If UCase(WorshipTemp) = "PUSSY" Then ssh.WorshipTarget = "Pussy"
            ssh.WorshipMode = True
            inputString = inputString.Replace("@Worship(" & GetParentheses(inputString, "@Worship(") & ")", "")
        End If

        If inputString.Contains("@WorshipOn") Then
            ssh.WorshipMode = True
            inputString = inputString.Replace("@WorshipOn", "")
        End If

        If inputString.Contains("@WorshipOff") Then
            ssh.WorshipMode = False
            ssh.WorshipTarget = ""
            inputString = inputString.Replace("@WorshipOff", "")
        End If

        If inputString.Contains("@ClearWorship") Then
            ssh.WorshipTarget = ""
            inputString = inputString.Replace("@ClearWorship", "")
        End If

        If inputString.Contains("@MiniScript(") Then

            Dim MiniTemp As String = GetParentheses(inputString, "@MiniScript(")


            If File.Exists(Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\MiniScripts\" & MiniTemp & ".txt") Then ' And MiniScript = False Then
                ssh.MiniScript = True
                ssh.MiniScriptText = Application.StartupPath & "\Scripts\" & DommePersonalityComboBox.Text & "\Custom\MiniScripts\" & MiniTemp & ".txt"
                ssh.MiniTauntVal = -1
                ssh.MiniTimerCheck = ScriptTimer.Enabled
                ssh.ScriptTick = 2
                ScriptTimer.Start()
            End If

            inputString = inputString.Replace("@MiniScript(" & MiniTemp & ")", "")
        End If


        If inputString.Contains("@CheckFile(") Then

            Dim FileFlag As String = GetParentheses(inputString, "@CheckFile(")
            FileFlag = FixCommas(FileFlag)

            Dim FileArray As String() = FileFlag.Split(",")

            If FileArray.Count = 2 Or FileArray.Count = 3 Then

                If File.Exists(FileArray(0)) Then
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = FileArray(1)
                    GetGoto()
                End If

                If Not File.Exists(FileArray(0)) And FileArray.Count = 3 Then
                    ssh.SkipGotoLine = True
                    ssh.FileGoto = FileArray(2)
                    GetGoto()
                End If

            End If

            inputString = inputString.Replace("@CheckFile(" & GetParentheses(inputString, "@CheckFile(") & ")", "")
        End If


        If inputString.Contains("@YesMode(") Then

            Dim YesFlag As String = GetParentheses(inputString, "@YesMode(")
            YesFlag = FixCommas(YesFlag)
            Dim YesArray As String() = YesFlag.Split(",")

            If UCase(YesArray(0)).Contains("GOTO") Then
                ssh.YesGoto = True
                ssh.YesGotoLine = YesArray(1)
            End If

            If UCase(YesArray(0)).Contains("VIDEO") Then
                ssh.YesVideo = True
                ssh.YesGotoLine = YesArray(1)
            End If

            If UCase(YesArray(0)).Contains("NORMAL") Then
                ssh.YesGoto = False
                ssh.YesVideo = False
            End If

            inputString = inputString.Replace("@YesMode(" & GetParentheses(inputString, "@YesMode(") & ")", "")
        End If

        If inputString.Contains("@NoMode(") Then

            Dim NoFlag As String = GetParentheses(inputString, "@NoMode(")
            NoFlag = FixCommas(NoFlag)
            Dim NoArray As String() = NoFlag.Split(",")

            If UCase(NoArray(0)).Contains("GOTO") Then
                ssh.NoGoto = True
                ssh.NoGotoLine = NoArray(1)
            End If

            If UCase(NoArray(0)).Contains("VIDEO") Then
                ssh.NoVideo_Mode = True
                ssh.NoGotoLine = NoArray(1)
            End If

            If UCase(NoArray(0)).Contains("NORMAL") Then
                ssh.NoGoto = False
                ssh.NoVideo_Mode = False
            End If

            inputString = inputString.Replace("@NoMode(" & GetParentheses(inputString, "@NoMode(") & ")", "")
        End If

        If inputString.Contains("@CameMode(") Then

            Dim CameFlag As String = GetParentheses(inputString, "@CameMode(")
            CameFlag = FixCommas(CameFlag)
            Dim CameArray As String() = CameFlag.Split(",")

            If UCase(CameArray(0)).Contains("GOTO") Then
                ssh.CameGoto = True
                ssh.CameGotoLine = CameArray(1)
            End If

            If UCase(CameArray(0)).Contains("MESSAGE") Then
                ssh.CameMessage = True
                ssh.CameMessageText = CameArray(1)
            End If

            If UCase(CameArray(0)).Contains("VIDEO") Then
                ssh.CameVideo = True
                ssh.CameGotoLine = CameArray(1)
            End If

            If UCase(CameArray(0)).Contains("NORMAL") Then
                ssh.CameGoto = False
                ssh.CameMessage = False
                ssh.CameVideo = False
            End If

            inputString = inputString.Replace("@CameMode(" & GetParentheses(inputString, "@CameMode(") & ")", "")
        End If

        If inputString.Contains("@RuinedMode(") Then

            Dim RuinedFlag As String = GetParentheses(inputString, "@RuinedMode(")
            RuinedFlag = FixCommas(RuinedFlag)
            Dim RuinedArray As String() = RuinedFlag.Split(",")

            If UCase(RuinedArray(0)).Contains("GOTO") Then
                ssh.RuinedGoto = True
                ssh.RuinedGotoLine = RuinedArray(1)
            End If

            If UCase(RuinedArray(0)).Contains("MESSAGE") Then
                ssh.RuinedMessage = True
                ssh.RuinedMessageText = RuinedArray(1)
            End If

            If UCase(RuinedArray(0)).Contains("VIDEO") Then
                ssh.RuinedVideo = True
                ssh.RuinedGotoLine = RuinedArray(1)
            End If

            If UCase(RuinedArray(0)).Contains("NORMAL") Then
                ssh.RuinedGoto = False
                ssh.RuinedMessage = False
                ssh.RuinedVideo = False
            End If

            inputString = inputString.Replace("@RuinedMode(" & GetParentheses(inputString, "@RuinedMode(") & ")", "")
        End If

        If inputString.Contains("@CustomMode(") Then

            Dim CustomFlag As String = GetParentheses(inputString, "@CustomMode(")
            CustomFlag = FixCommas(CustomFlag)
            Dim CustomArray As String() = CustomFlag.Split(",")

            If CustomArray.Count = 3 Then

                If ssh.Modes.Keys.Contains(CustomArray(0)) Then ssh.Modes.Remove(CustomArray(0))

                Dim NewMode As New Mode
                NewMode.Keyword = CustomArray(0)
                NewMode.Type = CustomArray(1)
                NewMode.GotoLine = CustomArray(2)
                ssh.Modes.Add(CustomArray(0), NewMode)
            End If

            If CustomArray.Count = 2 Then
                If CustomArray(1).ToUpper.Contains("NORMAL") Then
                    If ssh.Modes.Keys.Contains(CustomArray(0)) Then
                        ssh.Modes.Remove(CustomArray(0))
                    End If
                End If
            End If

            inputString = inputString.Replace("@CustomMode(" & GetParentheses(inputString, "@CustomMode(") & ")", "")

        End If


        If inputString.Contains("@ClearModes") Then
            ClearModes()
            inputString = inputString.Replace("@ClearModes", "")
        End If


        If inputString.Contains("@LockVideo") Then
            ssh.LockVideo = True
            inputString = inputString.Replace("@LockVideo", "")
        End If

        If inputString.Contains("@UnlockVideo") Then
            ssh.LockVideo = False
            mainPictureBox.Visible = True
            DomWMP.Visible = False
            inputString = inputString.Replace("@UnlockVideo", "")
        End If

        If inputString.Contains("@ClearChat") Then
            ClearChat()
            inputString = inputString.Replace("@ClearChat", "")
        End If

        If inputString.Contains("@ChatImage[") Then
            Dim ImageDir As String = Application.StartupPath & "\Images\" & GetParentheses(inputString, "@ChatImage[")
            ImageDir = ImageDir.Replace("/", "\")
            ImageDir = ImageDir.Replace("\\", "\")


            If File.Exists(ImageDir.Split(",")(0)) Then

                If GetCharCount(ImageDir, ",") = 2 Then

                    Dim PicAttributes As String() = GetArrayString(ImageDir)

                    inputString = inputString.Replace("@ChatImage[" & GetParentheses(inputString, "@ChatImage[") & "]", "<img id=""ChatPic"" src=""" & PicAttributes(0) & """ width=" & PicAttributes(1) &
                     " height=" & PicAttributes(2) & """/>")

                Else
                    inputString = inputString.Replace("@ChatImage[" & GetParentheses(inputString, "@ChatImage[") & "]", "<img id=""ChatPic"" src=""" & ImageDir & """/>")
                End If

            Else
                inputString = inputString.Replace("@ChatImage[" & GetParentheses(inputString, "@ChatImage[") & "]", "")
            End If
        End If

        If inputString.Contains("@Debug") Then

            'Dim wy As Long = DateDiff(DateInterval.Day, Val(GetVariable("TB_AFKSlideshow")), Date.Now)

            MsgBox(GetParentheses("Testing If - @If[42]>[7]Then(Go here) okay", "@If["))
            MsgBox(GetParentheses("Testing If2 - @If[42]>[7]Then(Go here) okay", "@If[", 2))
            MsgBox(GetParentheses("Testing If2 - @If(candle) okay", "@If("))
            MsgBox(GetParentheses("Testing If2 - @If(candle)and(wax) okay", "@If(", 2))


            'MsgBox(GetVariable("Sys_EndTotal") & " less than 30? " & CheckVariable("@Variable[Sys_EndTotal]<[30] blah blah blah"))
            inputString = inputString.Replace("@Debug", "")
        End If

        If inputString.Contains("@CheckBnB") Then
            If Not GetImageData(ImageGenre.Boobs).IsAvailable Or Not GetImageData(ImageGenre.Butt).IsAvailable Then
                ssh.FileGoto = "(No BnB)"
                ssh.SkipGotoLine = True
                GetGoto()
            End If
            inputString = inputString.Replace("@CheckBnB", "")
        End If

        If inputString.Contains("@CheckStrokingState") Then
            'If SubStroking = True Then
            If ssh.SubStroking = True Or ssh.SubEdging = True Or ssh.SubHoldingEdge = True Then
                ssh.FileGoto = "(Sub Stroking)"
            Else
                ssh.FileGoto = "(Sub Not Stroking)"
            End If
            ssh.SkipGotoLine = True
            GetGoto()
            inputString = inputString.Replace("@CheckStrokingState", "")
        End If

        'The @SetGroup Command is a defunct Command that was created when implementing new Glitter features. It has no use in the current build of Tease AI.

        If inputString.Contains("@SetGroup(") Then

            Dim WF As String = UCase(GetParentheses(inputString, "@SetGroup("))

            If WF.Contains("D") And Not WF.Contains("1") And Not WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D"
            If WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D1"
            If WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D12"
            If WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D13"
            If WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D23"
            If WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D123"

            If Not WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "1"
            If Not WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "12"
            If Not WF.Contains("D") And WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "123"

            If WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "D2"
            If Not WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And Not WF.Contains("3") Then ssh.Group = "2"
            If Not WF.Contains("D") And Not WF.Contains("1") And WF.Contains("2") And WF.Contains("3") Then ssh.Group = "23"

            If WF.Contains("D") And Not WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "D3"
            If Not WF.Contains("D") And Not WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "3"
            If Not WF.Contains("D") And WF.Contains("1") And Not WF.Contains("2") And WF.Contains("3") Then ssh.Group = "13"

            inputString = inputString.Replace("@SetGroup(" & WF & ")", "")

        End If

        Return inputString

    End Function

    Private Function GetTypingDelay(chatMessage As ChatMessage, isInstantType As Boolean) As Integer
        Dim typeDelay As Integer = 1
        If Not isInstantType Then
            typeDelay = Math.Min(chatMessage.Message.Length, 60)
        End If

        Return typeDelay
    End Function

    ''' <summary>
    ''' awaitable sleep. <paramref name="sleepTime"/> is ms
    ''' </summary>
    ''' <param name="sleepTime"></param>
    ''' <returns></returns>
    Private Async Function Sleep(sleepTime As Integer) As Task
        Await Task.Run(Sub() Thread.Sleep(sleepTime))
    End Function

    Private Function GetTokenDenomination(input As String) As TokenDenomination
        If input.ToLower().Contains("gold") Then
            Return TokenDenomination.Gold
        ElseIf input.ToLower().Contains("silver") Then
            Return TokenDenomination.Silver
        Else
            Return TokenDenomination.Bronze
        End If
    End Function

    Private Function GetDommePersonalities(basePath As String) As List(Of String)
        Dim returnValue As List(Of String) = New List(Of String)()
        For Each personalityDir As String In myDirectory.GetDirectories(basePath)
            Dim personalityName As String = Path.GetFileName(personalityDir)
            returnValue.Add(personalityName)
        Next
        Return returnValue
    End Function

    Private Function GetImageList(imageLocation As String) As List(Of String)
        If IsUrl(imageLocation) Then
            Dim webRequest As Net.HttpWebRequest = Net.HttpWebRequest.Create(imageLocation & "api/read?start=" & 1 & "&num=5000")
            Using webResponse As Net.HttpWebResponse = webRequest.GetResponse()
                Using reader As New Xml.XmlTextReader(webResponse.GetResponseStream())
                    Dim tmpDoc As New Xml.XmlDocument()
                    tmpDoc.Load(reader)

                    ' I don't think this is required
                    'webRequest.Abort()
                    webResponse.Close()
                    Dim imageList As List(Of String) = New List(Of String)()
                    For Each photoNode As Xml.XmlNode In tmpDoc.DocumentElement.SelectNodes("//photo-url")
                        If CInt(photoNode.Attributes.ItemOf("max-width").InnerText) = 1280 Then
                            imageList.Add(photoNode.InnerXml)
                        End If
                    Next
                    Return imageList
                End Using
            End Using
        End If

        Dim searchOption As SearchOption = If(FrmSettings.CBSlideshowSubDir.Checked, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly)
        Return myDirectory.GetFilesImages(imageLocation, searchOption)
    End Function

    Private Function GetImageLocation(useDialog As Boolean, fromDropDown As Boolean) As String
        Dim folderBrowswer As FolderBrowserDialog = New FolderBrowserDialog()
        If useDialog Then
            Return If(folderBrowswer.ShowDialog() = DialogResult.OK, folderBrowswer.SelectedPath, String.Empty)
        ElseIf fromDropDown Then
            Return ImageFolderComboBox.Text
        End If
        Throw New InvalidDataException("Neither use dialog nor from dropdown")
    End Function

    Private Function BooleanToOnOffColor(isOn As Boolean) As Color
        Return If(isOn, Color.Green, Color.Red)
    End Function

    Private Function BooleanToOnOff(isOn As Boolean) As String
        Return If(isOn, "ON", "OFF")
    End Function

    ''' <summary>
    ''' Compare <paramref name="checkDate"/> with today
    ''' </summary>
    ''' <param name="checkDate"></param>
    ''' <returns></returns>
    Public Function CompareDates(ByVal checkDate As Date) As Integer
        Return Date.Compare(checkDate.Date, Now.Date)
    End Function

    Public Function CompareDatesWithTime(ByVal CheckDate As Date) As Integer
        Dim result As Integer = DateTime.Compare(FormatDateTime(CheckDate, DateFormat.GeneralDate), FormatDateTime(Now, DateFormat.GeneralDate))
        Return result
    End Function

    Public Function SendCommand(command As String) As Result
        Return mySession.SendCommand(command)
    End Function

    Public Function GetGameBoard() As RiskyPickGameBoard
        Return mySession.Session.GameBoard
    End Function
#End Region
End Class