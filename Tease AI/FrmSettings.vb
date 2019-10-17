Imports System.ComponentModel
Imports System.IO
Imports System.Speech.Synthesis
Imports Tease_AI.URL_Files
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Services.TagData

Public Class FrmSettings

    Private Property Ssh As SessionState
        Get
            Return My.Application.Session
        End Get
        Set(value As SessionState)
            My.Application.Session = value
        End Set
    End Property

    ''' <summary>
    ''' Location of the local image tag file.
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property LocalImageTagFile As String
        Get
            Return Application.StartupPath & "\Images\System\LocalImageTags.txt"
        End Get
    End Property

    Public URLFileIncludeList As New List(Of String)
    Public FrmSettingsLoading As Boolean
    Dim LocalImageDir As New List(Of String)

    Dim ImageTagDir As New List(Of String)
    ''' <summary>
    ''' List of images in the current genre
    ''' </summary>
    Dim LocalImageTagDir As New List(Of String)
    Dim ImageTagCount As Integer
    Dim CurrentImageTagImage As String
    Dim CurrentLocalImageTagImage As String
    Dim TagCount As Integer

    ''' <summary>
    ''' Index + 1 of the displayed local image in LocalImageTagDir
    ''' </summary>
    Private LocalTagCount As Integer

    Public WebImage As String
    Public WebImageFile As StreamReader
    Public WebImageLines As New List(Of String)
    Public WebImageLine As Integer
    Public WebImageLineTotal As Integer
    Public WebImagePath As String
    Public ApproveImage As Integer = 0

    Dim CheckImgDir As New List(Of String)

    Public Sub New()
        mySettingsAccessor = New SettingsAccessor()
        myBlogAccessor = New BlogImageAccessor()
        myCldAccessor = New CldAccessor()
        myScriptAccessor = New ScriptAccessor(New CldAccessor())
        myLoadFileData = New LoadFileData()
        myParseTagDataService = New ParseOldTagDataService()

        InitializeComponent()
    End Sub

    Private Sub frmProgramma_FormClosing(ByVal sender As Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Visible = False
        MainWindow.BtnToggleSettings.Text = "Open Settings Menu"
        e.Cancel = True
    End Sub

    Private Sub FrmSettings_LostFocus(sender As Object, e As EventArgs) Handles Me.Deactivate
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Called when we want to validate everything
    ''' </summary>
    Public Sub FrmSettingStartUp()
        FrmSettingsLoading = True

        FrmSplash.UpdateText("Checking installed voices...")

        Dim oSpeech As New SpeechSynthesizer()
        Dim installedVoices As ObjectModel.ReadOnlyCollection(Of InstalledVoice) = oSpeech.GetInstalledVoices

        Dim names(installedVoices.Count - 1) As String
        For i As Integer = 0 To installedVoices.Count - 1
            names(i) = installedVoices(i).VoiceInfo.Name
            Debug.Print("Name = " & names(i))
        Next

        oSpeech.Dispose()

        FrmSplash.UpdateText("Checking URL Files...")
        Dim blogMetaDatas As List(Of BlogMetaData) = myBlogAccessor.GetBlogMetaData()

        URLFileList.Items.Clear()
        For Each blogMetaData In blogMetaDatas
            URLFileList.Items.Add(blogMetaData.FileName, blogMetaData.IsEnabled)
        Next
        URLFileList.Refresh()

        FrmSplash.UpdateText("Checking Local Image settings...")

        ' Check image Folders
        CheckImageFolder(ImageGenre.Blowjob)
        CheckImageFolder(ImageGenre.Boobs)
        CheckImageFolder(ImageGenre.Butt)
        CheckImageFolder(ImageGenre.Captions)
        CheckImageFolder(ImageGenre.Femdom)
        CheckImageFolder(ImageGenre.Gay)
        CheckImageFolder(ImageGenre.General)
        CheckImageFolder(ImageGenre.Hardcore)
        CheckImageFolder(ImageGenre.Hentai)
        CheckImageFolder(ImageGenre.Softcore)
        CheckImageFolder(ImageGenre.Lesbian)
        CheckImageFolder(ImageGenre.Lezdom)
        CheckImageFolder(ImageGenre.Maledom)

        FrmSplash.UpdateText("Checking installed fonts...")

        SubMessageFontCB.Items.AddRange(New Text.InstalledFontCollection().Families)

        DommeMessageFontCB.Items.AddRange(New Text.InstalledFontCollection().Families)

        FrmSplash.UpdateText("Checking available scripts...")

        Dim scriptType As String = "Stroke"
        Dim scripts As List(Of ScriptMetaData) = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, scriptType, SessionPhase.Start, True)
        myScriptAccessor.Save(scripts, mySettingsAccessor.DommePersonality, scriptType, SessionPhase.Start)

        scripts = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, "Modules", SessionPhase.Modules, True)
        myScriptAccessor.Save(scripts, mySettingsAccessor.DommePersonality, "Modules", SessionPhase.Modules)

        scripts = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, scriptType, SessionPhase.Link, True)
        myScriptAccessor.Save(scripts, mySettingsAccessor.DommePersonality, scriptType, SessionPhase.Link)

        scripts = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, scriptType, SessionPhase.End, True)
        myScriptAccessor.Save(scripts, mySettingsAccessor.DommePersonality, scriptType, SessionPhase.End)

        FrmSplash.UpdateText("Populating available voices...")

        Dim voicecheck As Integer
        'Dim voices = Fringe.GetInstalledVoices()
        For Each v As InstalledVoice In installedVoices
            voicecheck += 1
            TTSComboBox.Items.Add(v.VoiceInfo.Name)
            TTSComboBox.Text = v.VoiceInfo.Name
        Next

        If voicecheck = 0 Then
            TTSComboBox.Text = "No voices installed"
            TTSComboBox.Enabled = False
            TTSCheckBox.Checked = False
            TTSCheckBox.Enabled = False
        End If

        FrmSplash.UpdateText("Loading Sub settings...")

        CockTortureEnabledCB.Checked = mySettingsAccessor.IsCockTortureEnabled
        BallTortureEnabledCB.Checked = mySettingsAccessor.IsBallTortureEnabled

        NBLongEdge.Value = My.Settings.LongEdge

        AllowLongEdgeInterruptCB.Checked = mySettingsAccessor.CanInterruptLongEdge

        HoldEdgeMaximum.Value = ConvertHoldTime(mySettingsAccessor.HoldEdgeMaximum)
        LBLMaxHold.Text = ConvertHoldTimeUnits(mySettingsAccessor.HoldEdgeMaximum)
        HoldEdgeMinimum.Value = ConvertHoldTime(mySettingsAccessor.HoldEdgeMinimum)
        HoldEdgeMinimumUnits.Text = ConvertHoldTime(mySettingsAccessor.HoldEdgeMinimum)

        LongEdgeHoldMaximum.Value = mySettingsAccessor.LongHoldEdgeMaximum
        LongEdgeHoldMinimum.Value = mySettingsAccessor.LongHoldEdgeMinimum

        ExtremeEdgeHoldMaximum.Value = mySettingsAccessor.ExtremeHoldEdgeMaximum
        ExtremeEdgeHoldMinimum.Value = mySettingsAccessor.ExtremeHoldEdgeMinimum

        CockAndBallTortureLevelSlider.Value = mySettingsAccessor.CockAndBallTortureLevel
        CockAndBallTortureLevelLbl.Text = "CBT Level:  " & CockAndBallTortureLevelSlider.Value

        CBSubCircumcised.Checked = mySettingsAccessor.IsSubCircumcised
        CBSubPierced.Checked = mySettingsAccessor.IsSubPierced

        FrmSplash.UpdateText("Loading Domme settings...")
        Dim domLevel As DomLevel = mySettingsAccessor.DominationLevel
        DominationLevel.Value = domLevel
        DomLevelDescLabel.Text = domLevel.ToString()

        Dim apathyLevel As ApathyLevel = mySettingsAccessor.ApathyLevel
        NBEmpathy.Value = apathyLevel
        LBLEmpathy.Text = apathyLevel.ToString()

        DommeDecideOrgasmCB.Checked = mySettingsAccessor.DoesDommeDecideOrgasmRange
        AllowOrgasmOftenNB.Enabled = Not DommeDecideOrgasmCB.Checked
        AllowOrgasmOftenNB.Value = mySettingsAccessor.AllowOrgasmOftenPercent
        NBAllowSometimes.Enabled = Not DommeDecideOrgasmCB.Checked
        NBAllowSometimes.Value = mySettingsAccessor.AllowOrgasmSometimesPercent
        NBAllowRarely.Enabled = Not DommeDecideOrgasmCB.Checked
        NBAllowRarely.Value = mySettingsAccessor.AllowOrgasmRarelyPercent

        DommeDecideRuinCB.Checked = mySettingsAccessor.DoesDommeDecideRuinRange
        NBRuinOften.Enabled = Not DommeDecideRuinCB.Checked
        NBRuinOften.Value = mySettingsAccessor.RuinOrgasmOftenPercent
        NBRuinSometimes.Enabled = Not DommeDecideRuinCB.Checked
        NBRuinSometimes.Value = mySettingsAccessor.RuinOrgasmSometimesPercent
        NBRuinRarely.Enabled = Not DommeDecideRuinCB.Checked
        NBRuinRarely.Value = mySettingsAccessor.RuinOrgasmRarelyPercent

        TBSafeword.Text = mySettingsAccessor.SafeWord

        FrmSplash.UpdateText("Loading card images...")

        MainWindow.GamesToolStripMenuItem1.Enabled = CardGameCheck()

        FrmSplash.UpdateText("Checking user settings...")

        CBOwnChastity.Checked = mySettingsAccessor.HasChastityDevice

        DoesChastityDeviceRequirePiercingCB.Checked = mySettingsAccessor.DoesChastityDeviceRequirePiercing
        DoesChastityDeviceRequirePiercingCB.Enabled = CBOwnChastity.Checked
        ChastityDeviceContainsSpikesCB.Checked = mySettingsAccessor.DoesChastityDeviceContainSpikes
        ChastityDeviceContainsSpikesCB.Enabled = CBOwnChastity.Checked

        UseAverageEdgeThresholdCB.Checked = mySettingsAccessor.UseAverageEdgeTimeAsThreshold
        AllowLongEdgeTauntCB.Checked = mySettingsAccessor.AllowsLongEdgeTaunts
        AllowLongEdgeInterruptCB.Checked = mySettingsAccessor.AllowsLongEdgeInterrupts

        TeaseLengthDommeDetermined.Checked = mySettingsAccessor.IsTeaseLengthDommeDetermined
        CBTauntCycleDD.Checked = mySettingsAccessor.IsTauntCycleDommeDetermined

        ' If orgasms are locked, then check the lock until date and possibly unlock them
        If Not mySettingsAccessor.AreOrgasmsLocked Then
            limitcheckbox.Checked = True
            limitcheckbox.Enabled = False
            orgasmsPerNumBox.Enabled = False
            orgasmsperComboBox.Enabled = False
            orgasmsperlockButton.Enabled = False
            orgasmlockrandombutton.Enabled = False
        End If

        CBHimHer.Checked = mySettingsAccessor.IsSubFemale
        CBCockToClit.Checked = mySettingsAccessor.CallCockAClit
        CBBallsToPussy.Checked = mySettingsAccessor.CallBallsPussy

        NBNextImageChance.Value = My.Settings.NextImageChance
        CBDomDel.Checked = mySettingsAccessor.CanDommeDeleteFiles

        NBTeaseLengthMin.Value = mySettingsAccessor.TeaseLengthMinimum
        NBTeaseLengthMax.Value = mySettingsAccessor.TeaseLengthMaximum

        NBTauntCycleMin.Value = mySettingsAccessor.TauntCycleMinimum
        NBTauntCycleMax.Value = mySettingsAccessor.TauntCycleMaximum

        NBRedLightMin.Value = My.Settings.RedLightMin
        NBRedLightMax.Value = My.Settings.RedLightMax

        NBGreenLightMin.Value = My.Settings.GreenLightMin
        NBGreenLightMax.Value = My.Settings.GreenLightMax

        FrmSplash.UpdateText("Auditing scripts...")

        TBWebStart.Text = My.Settings.WebToyStart
        TBWebStop.Text = My.Settings.WebToyStop


        FrmSplash.UpdateText("Calculating space of saved session images...")

        Dim imageInfo As Tuple(Of Integer, Long) = GetImageCountSize(Application.StartupPath & "\Images\Session Images\")
        LBLSesFiles.Text = imageInfo.Item1.ToString()
        LBLSesSpace.Text = FormatBytes(imageInfo.Item2)

        FrmSplash.UpdateText("Loading Settings Menu options...")
        SaveSettingsDialog.InitialDirectory = Application.StartupPath & "\System"
        OpenSettingsDialog.InitialDirectory = Application.StartupPath & "\System"

        WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & mySettingsAccessor.DommePersonality & "\Playlist\Start\")

        For Each tmptbx As TextBox In New List(Of TextBox) From {TbxContact1ImageDir, TbxContact2ImageDir, TbxContact3ImageDir, TbxDomImageDir}
            If tmptbx.DataBindings("Text") Is Nothing Then
                Throw New Exception("There is no databinding set on """ & tmptbx.Name & """'s text-property. Set the databinding and recompile!")
            End If
        Next

        For Each tmptbx As CheckBox In New List(Of CheckBox) From {CBGlitter1, CBGlitter2, CBGlitter3}
            If tmptbx.DataBindings("Checked") Is Nothing Then
                Throw New Exception("There is no databinding set on """ & tmptbx.Name & """'s checked-property. Set the databinding and recompile!")
            End If
        Next

        If My.Settings.TeaseAILanguage = "English" Then EnglishMenu()
        If My.Settings.TeaseAILanguage = "German" Then GermanMenu()

        Try
            TimeBoxWakeUp.Value = My.Settings.WakeUp
        Catch
        End Try

        NBTypoChance.Value = My.Settings.TypoChance

        SliderVVolume.Value = My.Settings.VVolume
        SliderVRate.Value = My.Settings.VRate

        LBLVVolume.Text = SliderVVolume.Value
        LBLVRate.Text = SliderVRate.Value

        If mySettingsAccessor.IsOnline Then
            LBLOfflineMode.Text = "OFF"
            LBLOfflineMode.ForeColor = Color.Red
        Else
            LBLOfflineMode.Text = "ON"
            LBLOfflineMode.ForeColor = Color.Green
        End If

        CBNewSlideshow.Checked = My.Settings.CBNewSlideshow

        NBTauntEdging.Value = My.Settings.TauntEdging

        CBURLPreview.Checked = My.Settings.CBURLPreview

        TypesSpeedVal.Text = TypeSpeedSlider.Value

        FrmSettingsLoading = False
        Visible = False
        Dim languageCode As String = "en"
        If RBGerman.Checked Then
            languageCode = "de"
        End If
        SetToolTips(languageCode)
    End Sub

    Private Sub SettingsTabs_TabIndexChanged(sender As Object, e As EventArgs) Handles SettingsTabs.SelectedIndexChanged
        If SettingsTabs.SelectedTab Is TabPage16 Then TCScripts_TabIndexChanged(TCScripts, Nothing)
    End Sub

#Region "set tooltips"
    Private Sub SetToolTips(languageCode As String)
        Dim langData As TranslationService = New TranslationService()
        TTDir.SetToolTip(TimeStampCheckBox, langData.GetString(languageCode, NameOf(TimeStampCheckBox)))
        TTDir.SetToolTip(ShowNamesCheckBox, langData.GetString(languageCode, NameOf(ShowNamesCheckBox)))
    End Sub

    Private Sub TypeSpeedSlider_MouseHover(sender As Object, e As EventArgs) Handles TypeSpeedSlider.MouseHover
        TTDir.SetToolTip(TypeSpeedSlider, "Adjust your typing speed. It determines how much time you will have during Writing Tasks to accomplish them." & vbCrLf & "(There is a 3-fold difference in time granted between slowest and fastest typing speed")
    End Sub

    Private Sub TimedWriting_CheckedChanged_1(sender As Object, e As EventArgs) Handles TimedWriting.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(TimedWriting, "When selected, you will need to complete Writing Tasks in a certain amount of time, based on sentence length and Typing Speed value" & Environment.NewLine &
      "When unselected, Writing Tasks failure will only be based on errors made")
        If RBGerman.Checked Then TTDir.SetToolTip(TimedWriting, "Wenn diese Option aktiviert , müssen Sie Schreibaufgaben in einer bestimmten Zeit zu vervollständigen, basierend auf Satzlängeund Typing Speed ​​Wert" & Environment.NewLine &
         "Wenn diese Option deaktiviert, Schreibaufgaben Fehler wird nur auf Fehler beruhen gemacht")

    End Sub

    Private Sub TypeInstantlyCheckBox_MouseHover(sender As Object, e As EventArgs) Handles TypeInstantlyCheckBox.MouseHover
        If RBEnglish.Checked Then TTDir.SetToolTip(TypeInstantlyCheckBox, "This program simulates a chat environment, so a brief delay appears before each post the domme makes." & Environment.NewLine &
                                                                                 "This delay is determined by the length of what she is saying and will be accompanied by the text ""[Dom Name] is typing...""" & Environment.NewLine & Environment.NewLine &
                                                                                 "When this is selected, the typing delay is removed and the domme's messages become instantaneous.")
        If RBGerman.Checked Then TTDir.SetToolTip(TypeInstantlyCheckBox, "Dieses Programm simuliert eine Chat Umgebung, daher erscheint eine kurze Verzögerung vor jedem Beitrag den die Domina macht." & Environment.NewLine &
                                                                                "Diese Verzögerung hängt von der Länge ab, was sie schreibt und wird begleitet mit dem text „[Dom Name] is typing…"" für einen besseren Effekt." & Environment.NewLine & Environment.NewLine &
                                                                                "Wenn dies deaktiviert ist, ist die „Tippen"" Verzögerung entfernt und die Domina Beiträge erschein sofort")
    End Sub

    Private Sub CBInputIcon_MouseHover(sender As Object, e As EventArgs) Handles CBInputIcon.MouseHover
        TTDir.SetToolTip(CBInputIcon, "When this is selected, a small question mark icon will appear next to the" & Environment.NewLine &
                                      "domme's question when your exact response will be saved to a variable.")
        'If RBGerman.Checked Then TTDir.SetToolTip(CBInputIcon, "Wenn dies aktiviert ist, wird mit jeder Nachricht die" & Environment.NewLine & _
        ' "du oder die Domina sendet ein Zeitstempel angezeigt")

        'LBLGeneralSettingsDescription.Text = "When this is selected, a small question mark icon will appear next to domme's question when your exact response will be saved to a variable."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, kann der Teilungsbalken zwischen Chat Fenster und Bildfenster nicht verstellt werden."
    End Sub

    Private Sub CBBlogImageWindow_MouseHover(sender As Object, e As EventArgs) Handles CBBlogImageWindow.MouseHover
        If RBEnglish.Checked Then TTDir.SetToolTip(CBBlogImageWindow, "When this is selected, any blog images the domme shows you will" & Environment.NewLine &
                                                                             "automatically be saved to ""[root folder]\Images\Session Images\"".")
        If RBGerman.Checked Then TTDir.SetToolTip(CBBlogImageWindow, "Wenn dies aktiviert ist, wird jedes Blog Bild, welches die Domina dir zeigt" & Environment.NewLine &
                                                                            "automatisch gespeichert in „…\Tease AI Open Beta\Images\Session Images\""")
        'LBLGeneralSettingsDescription.Text = "When this is selected, any blog images the domme shows you will automatically be saved to ""[root folder]\Images\Session Images\""."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird jedes Blog Bild, welches die Domina dir zeigt automatisch gespeichert in „…\Tease AI Open Beta\Images\Session Images\"""
    End Sub

    Private Sub LandscapeCheckBox_MouseHover(sender As Object, e As EventArgs) Handles LandscapeCheckBox.MouseHover
        If RBEnglish.Checked Then TTDir.SetToolTip(LandscapeCheckBox, "When this is selected, images that appear in the main window will be" & Environment.NewLine &
                                                                             "stretched to fit the screen if their width is greater than their height.")
        If RBGerman.Checked Then TTDir.SetToolTip(LandscapeCheckBox, "Wenn dies aktiviert ist, werden die Bilder(welche Angezeigt" & Environment.NewLine &
                                                                            "werden) gestreckt, wenn ihre Breite größer als ihre Höhe ist.")
        'LBLGeneralSettingsDescription.Text = "When this is selected, images that appear in the main window will be stretched to fit the screen if their width is greater than their height."
        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, werden die Bilder(welche Angezeigt werden) gestreckt, wenn ihre Breite größer als ihre Höhe ist"
    End Sub

    Private Sub CBImageInfo_MouseHover(sender As Object, e As EventArgs) Handles CBImageInfo.MouseHover
        If RBEnglish.Checked Then TTDir.SetToolTip(CBImageInfo, "When this is selected, the local filepath or URL address of each image displayed" & Environment.NewLine &
                                                                       "in the main window will appear in the upper left hand corner of the screen.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBImageInfo, "Wenn dies aktiviert ist, wird der Lokale Dateipfad oder die URL-Adresse" & Environment.NewLine &
                                                                      "von jedem Bild in der oberen linken Ecke des Bildschirms angezeigt.")

        'LBLGeneralSettingsDescription.Text = "When this is selected, the local filepath or URL address of each image displayed in the main window will appear in the upper left hand corner of the screen."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird der Lokale Dateipfad oder die URL-Adresse von jedem Bild in der oberen linken Ecke des Bildschirms angezeigt."
    End Sub

    Private Sub BTNDomImageDir_MouseHover(sender As Object, e As EventArgs) Handles BTNDomImageDir.MouseHover
        If RBEnglish.Checked Then TTDir.SetToolTip(BTNDomImageDir, "Use this button to select a directory containing several image" & Environment.NewLine &
                                                                             "set folders of the same model you're using as your domme." & Environment.NewLine & Environment.NewLine &
                                                                             "Once a valid directory has been set, any time you say hello to the domme, one of" & Environment.NewLine &
                                                                             "those folders will automatically be selected at random and used for the slideshow.")
        If RBGerman.Checked Then TTDir.SetToolTip(BTNDomImageDir, "Benutze diese Schaltfläche um einen Ordner zu wählen, welcher mehre" & Environment.NewLine &
                                                                            "Bildersets von dem selben Model enthält, die du als Domina benutzt." & Environment.NewLine & Environment.NewLine &
                                                                            "Nachdem einmal ein gültiges Verzeichnis gesetzt wurde, wird nachdem du Hello" & Environment.NewLine &
                                                                            "zu der Domina gesagt hast, automatisch zufällig eine Diashow ausgewählt.")


        'LBLGeneralSettingsDescription.Text = "Use this button to select a directory containing several image set folders of the same model you're using as your domme. Once a valid directory has been set, any time" _
        '& " you say hello to the domme, one of those folders will automatically be selected at random and used for the slideshow."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Benutze diese Schaltfläche um einen Ordner zu wählen, welcher mehre Bildersets von dem selben Model enthält,"
        ' die du als Domina benutzt. Nachdem einmal ein gültiges Verzeichnis gesetzt wurde, wird nachdem du Hello zu der Domina gesagt hast, automatisch zufällig eine Diashow ausgewählt."
    End Sub

    Private Sub offRadio_MouseHover(sender As Object, e As EventArgs) Handles ManualSlideShowRadio.MouseHover
        If RBEnglish.Checked Then TTDir.SetToolTip(ManualSlideShowRadio, "When this is set, any domme slideshow you have selected will not advance during the" & Environment.NewLine &
                                                                    "tease. Use the Previous and Next buttons on the Media Bar to change the images.")
        If RBGerman.Checked Then TTDir.SetToolTip(ManualSlideShowRadio, "Wenn dies aktiviert ist, wird jede Diashow nicht automatisch die Bilder wechseln." & Environment.NewLine &
                                                                   "Nutze die Vor- und Zurückschaltflächen in der media bar um die Bilder zu wechseln.")

        'LBLGeneralSettingsDescription.Text = "When this is set, any slideshow you have selected will not advance during the tease. Use the Previous and Next buttons on the Media Bar to change the images."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird jede Diashow nicht automatisch die Bilder wechseln. Nutze die Vor- und Zurückschaltflächen in der media bar um die Bilder zu wechseln"
    End Sub

    Private Sub TimedRadio_MouseHover(sender As Object, e As EventArgs) Handles TimedSlideShowRadio.MouseHover

        TTDir.SetToolTip(TimedSlideShowRadio, "When this is set, any slideshow you have selected will advance the image" & Environment.NewLine &
                                     "every number of seconds displayed in the box to the right of this option.")

        'LBLGeneralSettingsDescription.Text = "When this is set, any slideshow you have selected will advance the image every number of seconds displayed in the box to the right of this option."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = ""
    End Sub

    Private Sub SlideShowNumBox_MouseHover(sender As Object, e As EventArgs) Handles SlideShowNumBox.MouseHover

        TTDir.SetToolTip(SlideShowNumBox, "The number of seconds between image changes" & Environment.NewLine &
                                          "when the ""Timed"" slideshow option is checked.")

        'LBLGeneralSettingsDescription.Text = "The number of seconds between image changes when the ""Timed"" slideshow option is checked."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = ""
    End Sub

    Private Sub TeaseSlideShowRadio_MouseHover(sender As Object, e As EventArgs) Handles TeaseSlideShowRadio.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(TeaseSlideShowRadio, "When this is set, any slideshow you have selected will advance automatically when the domme " & Environment.NewLine &
                                                                      "types. The slideshow may move forward or backward, but will not loop either direction." & Environment.NewLine & Environment.NewLine &
                                                                      "You can change the odds of which way the slideshow will move in" & Environment.NewLine &
                                                                      "the Ranges tab. This is the default slideshow mode for Tease AI.")
        If RBGerman.Checked Then TTDir.SetToolTip(TeaseSlideShowRadio, "Wenn dies aktiviert ist, wird die Diashow automatisch die Bilder wechseln wenn die Domina schreibt." & Environment.NewLine &
                                                                     "Die Diashow kann vorwärts oder rückwärts laufen, aber wird keine Richtung wiederholen." & Environment.NewLine & Environment.NewLine &
                                                                     "Du kannst die Wahrscheinlichkeit in welche Richtung die Diashow läuft im Wertebereichs" & Environment.NewLine &
                                                                     "„Reiter"" ändern. Dies ist der Standart Diashow modus in Tease AI.")


        'LBLGeneralSettingsDescription.Text = "When this is set, any slideshow you have selected will advance automatically when the domme types. The slideshow may move forward or backward, but will not loop either" _
        '   & " direction. You can change the odds of which way the slideshow will move in the Ranges tab. This is the default slideshow mode for Tease AI."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird die Diashow automatisch die Bilder wechseln wenn die Domina schreibt. 
        'Die Diashow kann vorwärts oder rückwärts laufen, aber wird keine Richtung wiederholen. Du kannst die Wahrscheinlichkeit in welche Richtung die 
        'Diashow läuft im Wertebereichs „Reiter"" ändern. Dies ist der Standart Diashow modus in Tease AI "
    End Sub

    Private Sub CBSettingsPause_MouseHover(sender As Object, e As EventArgs) Handles CBSettingsPause.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBSettingsPause, "When this is selected, the program will pause any time" & Environment.NewLine &
                                                                           "the settings menu is open and resume once it is closed.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBSettingsPause, "Wenn dies aktiviert ist, wird das Programm immer in Pause" & Environment.NewLine &
                                                                          "springen solange das Einstellungsmenü geöffnet ist.")

        'LBLGeneralSettingsDescription.Text = "When this is selected, the program will pause any time the settings menu is open and resume once it is closed."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird das Programm immer in Pause springen solange das Einstellungsmenü geöffnet ist."
    End Sub

    Private Sub BTNDomColor_MouseHover(sender As Object, e As EventArgs) Handles BTNDomColor.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(BTNDomColor, "This button allows you to change the color of the" & Environment.NewLine &
                                                                       "domme's name as it appears in the chat window." & Environment.NewLine & Environment.NewLine &
                                                                       "A preview will appear in the text box next to this" & Environment.NewLine &
                                                                       "button once a color has been selected.")
        If RBGerman.Checked Then TTDir.SetToolTip(BTNDomColor, "Diese Schaltfläche erlaubt dir die Farbe des Domina Namens" & Environment.NewLine &
                                                                      "zu ändern in der er im Chat Fenster angezeigt wird." & Environment.NewLine & Environment.NewLine &
                                                                      "Eine Vorschau wird in der Textbox neben dieser Schaltfläche" & Environment.NewLine &
                                                                      "angezeigt, nachdem eine Farbe ausgewählt wurde.")


        'LBLGeneralSettingsDescription.Text = "This button allows you to change the color of the domme's name as it appears in the chat window. A preview will appear in the text box next to this button once a color has been selected."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Diese Schaltfläche erlaubt dir die Farbe des Domina Namens zu ändern in der er im Chat Fenster angezeigt wird. Eine Vorschau wird in der Textbox neben dieser Schaltfläche angezeigt, nachdem eine Farbe ausgewählt wurde."
    End Sub

    Private Sub BTNSubColor_MouseHover(sender As Object, e As EventArgs) Handles BTNSubColor.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(BTNSubColor, "This button allows you to change the color of" & Environment.NewLine &
                                                                       "your name as it appears in the chat window." & Environment.NewLine & Environment.NewLine &
                                                                       "A preview will appear in the text box next to this" & Environment.NewLine &
                                                                       "button once a color has been selected.")
        If RBGerman.Checked Then TTDir.SetToolTip(BTNSubColor, "Diese Schaltfläche erlaubt dir die Farbe des Sklaven Namens" & Environment.NewLine &
                                                                      "zu ändern in der er im Chat Fenster angezeigt wird." & Environment.NewLine & Environment.NewLine &
                                                                      "Eine Vorschau wird in der Textbox neben dieser Schaltfläche" & Environment.NewLine &
                                                                      "angezeigt, nachdem eine Farbe ausgewählt wurde.")

        'LBLGeneralSettingsDescription.Text = "This button allows you to change the color of your name as it appears in the chat window. A preview will appear in the text box next to this button once a color has been selected."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Diese Schaltfläche erlaubt dir die Farbe  des Sklaven Namens zu ändern in der er im Chat Fenster angezeigt wird. Eine Vorschau wird in der Textbox neben dieser Schaltfläche angezeigt, nachdem eine Farbe ausgewählt wurde."
    End Sub

    Private Sub LBLDomColor_MouseHover(sender As Object, e As EventArgs) Handles LBLDomColor.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(LBLDomColor, "After clicking the ""Domme Name Color"" button to the" & Environment.NewLine &
                                                                       "left, a preview of the selected color will appear here.")
        If RBGerman.Checked Then TTDir.SetToolTip(LBLDomColor, "Nachdem Klicken der Schaltfläche ""Domina Farbe für Namen"" zur" & Environment.NewLine &
                                                                      "linken, eine Vorschau der ausgewählten Farbe erscheint hier.")

        'LBLGeneralSettingsDescription.Text = "After clicking the ""Domme Name Color"" button to the left, a preview of the selected color will appear here."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Nachdem Klicken der Schaltfläche ""Domina Farbe für Namen"" zur linken, eine Vorschau der ausgewählten Farbe erscheint hier"
    End Sub

    Private Sub LBLSubColor_MouseHover(sender As Object, e As EventArgs) Handles LBLSubColor.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(LBLSubColor, "After clicking the ""Sub Name Color"" button to the" & Environment.NewLine &
                                                                      "left, a preview of the selected color will appear here.")
        If RBGerman.Checked Then TTDir.SetToolTip(LBLSubColor, "Nachdem Klicken der Schaltfläche ""Sklaven Farbe für Namen"" zur" & Environment.NewLine &
                                                                      "linken, eine Vorschau der ausgewählten Farbe erscheint hier.")


        'LBLGeneralSettingsDescription.Text = "After clicking the ""Sub Name Color"" button to the left, a preview of the selected color will appear here."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Nachdem Klicken der Schaltfläche ""Sklaven Farbe für Namen"" zur linken, eine Vorschau der ausgewählten Farbe erscheint hier"
    End Sub

    Private Sub CBDomDel_MouseHover(sender As Object, e As EventArgs) Handles CBDomDel.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBDomDel, "When this box is checked, the domme will be able to permanently delete" & Environment.NewLine &
                                                                    "media from your hard drive when such Commands are used in scripts." & Environment.NewLine & Environment.NewLine &
                                                                    "When this box is NOT checked, media will not actually be deleted. Images will still" & Environment.NewLine &
                                                                    "disappear from the window, but they will not be deleted from the hard drive.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBDomDel, "Wenn dies aktiviert ist, ist die Domina dazu in der Lage Medien permanent von" & Environment.NewLine &
                                                                   "deiner Festplatte zu löschen, wenn solche Kommandos in dem Script genutzt werden." & Environment.NewLine & Environment.NewLine &
                                                                   "Wenn dies deaktiviert ist, werden Bilder vom Bildschirm" & Environment.NewLine &
                                                                   "verschwinden, aber nicht von der Festplatte gelöscht.")



        'LBLGeneralSettingsDescription.Text = "When this box is checked, the domme will be able to permanently delete media from your hard drive when such Commands are used in scripts." & Environment.NewLine & _
        '   Environment.NewLine & "When this box is NOT checked, media will not actually be deleted. Images will still disappear from the window, but they will not be deleted from the hard drive."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, ist die Domina dazu in der Lage Medien permanent von deiner Festplatte zu löschen, wenn solche "
        'Kommandos in dem Script genutzt werden. Wenn dies deaktiviert ist, werden Bilder vom Bildschirm verschwinden, aber nicht von der Festplatte gelöscht."
    End Sub

    Private Sub CBSlideshowSubDir_MouseHover(sender As Object, e As EventArgs) Handles CBSlideshowSubDir.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBSlideshowSubDir, "When this is selected, the program will include all subdirectories" & Environment.NewLine &
                                                                             "when you select a folder for domme slideshow images" & Environment.NewLine & Environment.NewLine &
                                                                             "When it is unselected, only the images in the top" & Environment.NewLine &
                                                                             "level of the folder will be used.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBSlideshowSubDir, "Wenn dies aktiviert ist, wird das Programm alle Unterordner mit" & Environment.NewLine &
                                                                            "einbeziehn wenn du ein Ordner für Diashow bilder gewählt hast." & Environment.NewLine & Environment.NewLine &
                                                                            "Wenn dies deaktiviert ist. Werden nur Bilder" & Environment.NewLine &
                                                                            "des ausgewählten Ordners benutzt.")

        'LBLGeneralSettingsDescription.Text = "When this is selected, the program will include all subdirectories when you select a folder for slideshow images. When it is unselected, only the images in the top " & _
        '   "level of the folder will be used."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird das Programm alle Unterordner mit einbeziehn wenn du ein Ordner für Diashow bilder gewählt hast. "
        'Wenn dies deaktiviert ist. Werden nur Bilder des ausgewählten Ordners benutzt"
    End Sub

    Private Sub CBSlideshowRandom_MouseHover(sender As Object, e As EventArgs) Handles CBSlideshowRandom.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBSlideshowRandom, "When this is selected, the slideshow will display images randomly." & Environment.NewLine &
                                                                             "When it is unselected, it will display images in order of their filename.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBSlideshowRandom, "Wenn dies aktiviert ist, werden Diashow Bilder zufällig angezeigt." & Environment.NewLine &
                                                                            " Wenn dies deaktiviert ist, werden die Bilder in Reihenfolge ihrer Dateinamen gezeigt.")


        'LBLGeneralSettingsDescription.Text = "When this is selected, the slideshow will display images randomly. When it is unselected, it will display images in order of their filename."
        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, werden Diashow Bilder zufällig angezeigt. Wenn dies deaktiviert ist, werden die Bilder in Reihenfolge ihrer Dateinamen gezeigt."

    End Sub

    Private Sub CBAutosaveChatlog_MouseHover(sender As Object, e As EventArgs) Handles CBAutosaveChatlog.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBAutosaveChatlog, "When this is selected, the program will save a chatlog called" & Environment.NewLine &
                                                                             """Autosave.html"" any time you or the domme post a message." & Environment.NewLine & Environment.NewLine &
                                                                             "This log is overwritten each time, so it will only display a record of the current session." & Environment.NewLine &
                                                                             "This log can be found in the ""Chatlogs"" directory in the root folder of the program.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBAutosaveChatlog, "Wenn dies aktiviert ist, speichert das Programm einen Chatlog" & Environment.NewLine &
                                                                            "(„Autosave.html"") immer wenn du oder die Domina eine Nachricht senden." & Environment.NewLine & Environment.NewLine &
                                                                            "Dieses Log wird jedes Mal überschrieben, so das es nur die Aktuelle Session aufnimmt/anzeigt." & Environment.NewLine &
                                                                            "Dieses Log befindet sich im Ordner „Chatlogs"" in dem Tease AI Ordner.")


        'LBLGeneralSettingsDescription.Text = "When this is selected, the program will save a chatlog called ""Autosave.html"" any time you or the domme post a message. This log is overwritten each time, so it will only display " & _
        ' "a record of the current session. This log can be found in the ""Chatlogs"" directory in the root folder of the program."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, speichert das Programm einen Chatlog („Autosave.html"") immer wenn du oder die Domina eine Nachricht senden."
        ' Dieses Log wird jedes Mal überschrieben, so das es nur die Aktuelle Session aufnimmt/anzeigt. Dieses Log befindet sich im Ordner „Chatlogs"" in dem Tease AI Ordner."

    End Sub

    Private Sub CBSaveChatlogExit_MouseHover(sender As Object, e As EventArgs) Handles CBSaveChatlogExit.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBSaveChatlogExit, "When this is selected, a unique chatlog that includes the" & Environment.NewLine &
                                                                             "date and time will be created whenever you exit the program." & Environment.NewLine & Environment.NewLine &
                                                                             "This log can be found in the ""Chatlogs"" directory in the root folder of the program.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBSaveChatlogExit, "Wenn dies aktiviert ist, speichert das Programm einen einzigartigen Chatlog," & Environment.NewLine &
                                                                            "der Datum und Zeit beinhaltet, immer dann wenn du das Programm beendest." & Environment.NewLine & Environment.NewLine &
                                                                            "Dieses Log befindet sich im Ordner „Chatlogs"" in dem Tease AI Ordner.")

        ' LBLGeneralSettingsDescription.Text = "When this is selected, a unique chatlog that includes the date and time will be created whenever you exit the program. This log can be found in the ""Chatlogs"" directory in " & _
        '    "the root folder of the program."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, speichert das Programm einen einzigartigen Chatlog, der Datum und Zeit beinhaltet, immer dann wenn du das "
        'Programm beendest. Dieses Log befindet sich im Ordner „Chatlogs"" in dem Tease AI Ordner."
    End Sub

    Private Sub CBJackInTheBox_MouseHover(sender As Object, e As EventArgs) Handles CBAuditStartup.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(CBAuditStartup, "When this is checked, the program will automatically audit all" & Environment.NewLine &
                                                                          "scripts in the current domme's directory and fix common errors.")
        If RBGerman.Checked Then TTDir.SetToolTip(CBAuditStartup, "Wenn dies aktiviert ist, wird das Programm automatisch alle" & Environment.NewLine &
                                                                         "Scripts im domina Ordner prüfen und häufige Fehler beheben.")


        'LBLGeneralSettingsDescription.Text = "When this is checked, the program will automatically audit all scripts in the current domme's directory and fix common errors."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies aktiviert ist, wird das Programm automatisch alle Scripts im domina Ordner prüfen und häufige Fehler beheben"
    End Sub

    Private Sub TBSafeword_MouseHover(sender As Object, e As EventArgs) Handles TBSafeword.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(TBSafeword, "Use this to set the word you would like to use as your safeword." & Environment.NewLine & Environment.NewLine &
                                                                      "When used by itself during interaction with the domme, it will stop all activity" & Environment.NewLine &
                                                                      "and begin an Interrupt script where the domme makes sure you're okay to continue.")
        If RBGerman.Checked Then TTDir.SetToolTip(TBSafeword, "Gebe hier dein Safeword ein, welches alle Aktivitäten der Domina stopt," & Environment.NewLine &
                                                                     "bis sie sicher ist, das du weiter machen kannst.")

        'LBLGeneralSettingsDescription.Text = "Use this to set the word you would like to use as your safeword. When used by itself during interaction with the domme, it will stop all activity and begin an Interrupt" _
        '   & " script where the domme makes sure you're okay to continue."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Gebe hier dein Safeword ein, welches alle Aktivitäten der Domina stopt, bis sie sicher ist, das du weiter machen kannst."
    End Sub

    Private Sub TTSCheckbox_MouseHover(sender As Object, e As EventArgs) Handles TTSCheckBox.MouseHover


        If RBEnglish.Checked Then TTDir.SetToolTip(TTSCheckBox, "When this is selected, the domme will ""speak"" her lines using whichever TTS voice you have selected." & Environment.NewLine &
                                                                       "This setting must be manually checked to make the most out of the Hypnotic Guide app." & Environment.NewLine & Environment.NewLine &
                                                                       "For privacy reasons, this setting will not be saved through multiple uses of the program." & Environment.NewLine &
                                                                       "It must be selected each time you start Tease AI and wish to use it.")
        If RBGerman.Checked Then TTDir.SetToolTip(TTSCheckBox, "Wenn dies Aktiviert ist, wird die Domina ihre Zeilen ""sprechen"" mit welcher TTS stimme du gewählt hast." & Environment.NewLine &
                                                                      "Diese Einstellung muss Manuel gewählt werden um das meiste aus der Hypnotic Guide app zu machen." & Environment.NewLine & Environment.NewLine &
                                                                      "Wegen der Privatsphäre wird diese Einstellung nicht gespeichert," & Environment.NewLine &
                                                                      "sondern muss bei jedem Start von Tease AI gesondert gewählt werden.")


        'LBLGeneralSettingsDescription.Text = "When this is selected, the domme will ""speak"" her lines using whichever TTS voice you have selected. This setting must be manually checked to make the most out of the" _
        '   & " Hypnotic Guide app. For privacy reasons, this setting will not be saved through multiple uses of the program. It must be selected each time you start Tease AI and wish to use it."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = "Wenn dies Aktiviert ist, wird die Domina ihre Zeilen ""sprechen"" mit welcher TTS stimme du gewählt hast."
        'Diese Einstellung muss Manuel gewählt werden um das meiste aus der Hypnotic Guide app zu machen. Wegen der Privatsphäre wird diese Einstellung nicht gespeichert,
        '   sondern muss bei jedem Start von Tease AI gesondert gewählt werden."
    End Sub

    Private Sub TTSComboBox_MouseHover(sender As Object, e As EventArgs) Handles TTSComboBox.MouseHover

        TTDir.SetToolTip(TTSComboBox, "Make a selection from the Text-to-Speech voices installed on your computer.")

        ' LBLGeneralSettingsDescription.Text = "Make a selection from the Text-to-Speech voices installed on your computer."

        'If RBGerman.Checked Then LBLGeneralSettingsDescription.Text = ""
    End Sub

    Private Sub DominationLevel_MouseHover(sender As Object, e As EventArgs) Handles DominationLevel.MouseHover

        TTDir.SetToolTip(DominationLevel, "Sets the Domme's level (1-5)." & Environment.NewLine & Environment.NewLine &
                         "This setting affects the difficulty of the tasks the domme will subject you to.")

        'LblDommeSettingsDescription.Text = "Sets the Domme's level (1-5)." & Environment.NewLine & Environment.NewLine & "This setting affects the difficulty of the tasks the domme will subject you to. For example, a domme with a higher level may make you hold edges for " _
        '   & "longer periods of time, while a domme with a lower level may not make you edge that often. The domme's level is a general guideline of how easy-going or sadistic she can be, not necessarily what she will " _
        '  & "choose for you every time."
    End Sub

    Private Sub NBEmpathy_MouseHover(sender As Object, e As EventArgs) Handles NBEmpathy.MouseHover

        TTDir.SetToolTip(NBEmpathy, "Sets the Domme's Apathy level (1-5)." & Environment.NewLine & Environment.NewLine &
                       "This setting affects how merciless the domme is likely to be with you")

        'LblDommeSettingsDescription.Text = "Sets the Domme's Apathy level (1-5)." & Environment.NewLine & Environment.NewLine & "This setting affects how lenient the domme is likely to be with you. For example, a domme with a higher level may rarely take mercy on you or let " _
        '   & "you stop a task, while a domme with a lower level may never attempt to push your limits."
    End Sub

    Private Sub NBDomBirthdayMonth_MouseHover(sender As Object, e As EventArgs) Handles NBDomBirthdayMonth.MouseHover

        TTDir.SetToolTip(NBDomBirthdayMonth, "Sets the month the domme was born.")

        'LblDommeSettingsDescription.Text = "Sets the month the domme was born."
    End Sub

    Private Sub NBDomBirthdayDay_MouseHover(sender As Object, e As EventArgs) Handles NBDomBirthdayDay.MouseHover

        TTDir.SetToolTip(NBDomBirthdayDay, "Sets the day the domme was born.")

        'LblDommeSettingsDescription.Text = "Sets the day the domme was born."
    End Sub

    Private Sub domageNumBox_MouseHover(sender As Object, e As EventArgs) Handles domageNumBox.MouseHover

        TTDir.SetToolTip(domageNumBox, "Sets the Domme's age (18-99 years old).")

        'LblDommeSettingsDescription.Text = "Sets the Domme's age (18-99 years old)." & Environment.NewLine & Environment.NewLine & "This setting mainly affects how the domme describes herself in random conversation. For example, a younger domme might refer to her skin " _
        ' & "as tight or smooth, while an older domme might choose words like sensuous. Scripts may also contain keywords and variables that will limit certain paths to certain age groups."
    End Sub

    Private Sub domhairComboBox_MouseHover(sender As Object, e As EventArgs) Handles TBDomHairColor.MouseHover

        TTDir.SetToolTip(TBDomHairColor, "Sets the domme's hair color.")

        'LblDommeSettingsDescription.Text = "Sets the Domme's hair color." & Environment.NewLine & Environment.NewLine & "The domme may sometimes refer to her hair color over the course of the tease. Set this value to the color " & _
        ' "of the slideshow model's hair to enhance immersion."
    End Sub

    Private Sub boobComboBox_MouseHover(sender As Object, e As EventArgs) Handles boobComboBox.MouseHover

        TTDir.SetToolTip(boobComboBox, "Sets the Domme's cup size.")

        'LblDommeSettingsDescription.Text = "Sets the Domme's cup size." & Environment.NewLine & Environment.NewLine & "The domme may sometimes refer to the size of her breasts over the course of the tease. Set this value to the " & _
        '   "slideshow model's cup size to enhance immersion."
    End Sub

    Private Sub domhairlengthComboBox_MouseHover(sender As Object, e As EventArgs) Handles domhairlengthComboBox.MouseHover

        TTDir.SetToolTip(domhairlengthComboBox, "Sets the domme's hair length.")

        'LblDommeSettingsDescription.Text = "Sets the Domme's hair length." & Environment.NewLine & Environment.NewLine & "The domme may sometimes refer to her hair length over the course of the tease. Set this value to the length " & _
        '   "of the slideshow model's hair to enhance immersion."
    End Sub

    Private Sub domeyesComboBox_MouseHover(sender As Object, e As EventArgs) Handles TBDomEyeColor.MouseHover

        TTDir.SetToolTip(TBDomEyeColor, "Sets the domme's eye color.")

        'LblDommeSettingsDescription.Text = "Sets the Domme's eye color." & Environment.NewLine & Environment.NewLine & "The domme may sometimes refer to her eye color over the course of the tease. Set this value to the color " & _
        '   "of the slideshow model's eyes to enhance immersion."
    End Sub

    Private Sub dompubichairComboBox_MouseHover(sender As Object, e As EventArgs) Handles dompubichairComboBox.MouseHover

        TTDir.SetToolTip(dompubichairComboBox, "Sets description of the Domme's pubic hair.")

        'LblDommeSettingsDescription.Text = "Sets description of the Domme's pubic hair." & Environment.NewLine & Environment.NewLine & "The domme may sometimes refer to her pubic hair over the course of the tease. Set this value to a description " & _
        '  "of the slideshow model's pubic hair to enhance immersion."
    End Sub

    Private Sub crazyCheckBox_MouseHover(sender As Object, e As EventArgs) Handles crazyCheckBox.MouseHover

        TTDir.SetToolTip(crazyCheckBox, "Gives the Domme the Crazy trait." & Environment.NewLine & Environment.NewLine &
                     "This will open up dialogue options that suggest the domme is a little unhinged.")

        'LblDommeSettingsDescription.Text = "Gives the Domme the Crazy trait." & Environment.NewLine & Environment.NewLine & "This will open up dialogue options that suggest the domme is a little unhinged. " & _
        '   "Scripts may also contain keywords and variables that will limit certain paths to this trait."
    End Sub

    Private Sub CBDomTattoos_MouseHover(sender As Object, e As EventArgs) Handles CBDomTattoos.MouseHover

        TTDir.SetToolTip(CBDomTattoos, "Sets whether the domme has tattoos.")

        'LblDommeSettingsDescription.Text = "Sets whether the domme has tattoos." & Environment.NewLine & Environment.NewLine & "This will open up dialogue options that involve the domme being tattooed. " & _
        ' "Scripts may also contain keywords and variables that will limit certain paths to this trait."
    End Sub

    Private Sub CBDomFreckles_MouseHover(sender As Object, e As EventArgs) Handles CBDomFreckles.MouseHover

        TTDir.SetToolTip(CBDomTattoos, "Sets whether the domme has freckles.")

        'LblDommeSettingsDescription.Text = "Sets whether the domme has freckles." & Environment.NewLine & Environment.NewLine & "This will open up dialogue options that involve the domme having freckles. " & _
        '   "Scripts may also contain keywords and variables that will limit certain paths to this trait."
    End Sub

    Private Sub vulgarCheckBox_MouseHover(sender As Object, e As EventArgs) Handles vulgarCheckBox.MouseHover

        TTDir.SetToolTip(vulgarCheckBox, "Gives the Domme the Vulgar trait." & Environment.NewLine & Environment.NewLine &
                  "This will open up vulgar dialogue options for the domme.")

        'LblDommeSettingsDescription.Text = "Gives the Domme the Vulgar trait." & Environment.NewLine & Environment.NewLine & "This will open up vulgar dialogue options for the domme. She will include words like ""titties"" and " & _
        ' """gonads"" while a more reserved domme may limit herself to ""tits"" and ""balls"". Scripts may also contain keywords and variables that will limit certain paths to this trait."
    End Sub

    Private Sub supremacistCheckBox_MouseHover(sender As Object, e As EventArgs) Handles supremacistCheckBox.MouseHover

        TTDir.SetToolTip(supremacistCheckBox, "Gives the Domme the Supremacist trait." & Environment.NewLine & Environment.NewLine &
                                         "This will open up dialogue options that suggest the" & Environment.NewLine &
                                         "domme considers herself inherently superior to you.")

        ' LblDommeSettingsDescription.Text = "Gives the Domme the Supremacist trait." & Environment.NewLine & Environment.NewLine & "This will open up dialogue options that suggest the domme considers herself inherently superior " & _
        '    "to you. Scripts may also contain keywords and variables that will limit certain paths to this trait."
    End Sub

    Private Sub alloworgasmComboBox_MouseHover(sender As Object, e As EventArgs) Handles alloworgasmComboBox.MouseHover

        TTDir.SetToolTip(alloworgasmComboBox, "Sets how often the domme allows the user to have an orgasm during End scripts." & Environment.NewLine & Environment.NewLine &
                                              "To further define these parameters, use the options in the Ranges tab.")


        'LblDommeSettingsDescription.Text = "Sets how often the domme allows the user to have an orgasm during End scripts." & Environment.NewLine & Environment.NewLine & "To further define these parameters, use the options in the Ranges tab."
    End Sub

    Private Sub ruinorgasmComboBox_MouseHover(sender As Object, e As EventArgs) Handles ruinorgasmComboBox.MouseHover

        TTDir.SetToolTip(ruinorgasmComboBox, "Sets how often the domme will ruin the user's orgasm during End scripts." & Environment.NewLine & Environment.NewLine &
                                              "To further define these parameters, use the options in the Ranges tab.")

        'LblDommeSettingsDescription.Text = "Sets how often the domme will ruin the user's orgasm during End scripts." & Environment.NewLine & Environment.NewLine & "To further define these parameters, use the options in the Ranges tab."
    End Sub

    Private Sub LCaseCheckBox_MouseHover(sender As Object, e As EventArgs) Handles LCaseCheckBox.MouseHover

        TTDir.SetToolTip(LCaseCheckBox, "When this is checked, the domme won't use capital letters when she types." & Environment.NewLine & Environment.NewLine &
                                         "She will still capitalize Me/My/Mine if that box is checked.")


        'LblDommeSettingsDescription.Text = "When this is checked, the domme won't use capital letters when she types." & Environment.NewLine & Environment.NewLine & "She will still capitalize Me/My/Mine if that box is checked."
    End Sub

    Private Sub commaCheckBox_MouseHover(sender As Object, e As EventArgs) Handles commaCheckBox.MouseHover

        TTDir.SetToolTip(commaCheckBox, "When this is checked, the domme won't use commas when she types.")

        'LblDommeSettingsDescription.Text = "When this is checked, the domme won't use commas when she types."
    End Sub

    Private Sub periodCheckBox_MouseHover(sender As Object, e As EventArgs) Handles periodCheckBox.MouseHover

        TTDir.SetToolTip(periodCheckBox, "When this is checked, the domme won't use periods when she types.")

        'LblDommeSettingsDescription.Text = "When this is checked, the domme won't use periods when she types."
    End Sub

    Private Sub CBMeMyMine_MouseHover(sender As Object, e As EventArgs) Handles CBMeMyMine.MouseHover
        TTDir.SetToolTip(CBMeMyMine, "When this is checked, the domme will always capitalize ""Me, My and Mine""." & Environment.NewLine & Environment.NewLine &
           "If the lowercase typing option is checked, she will also capitalize ""I, I'm, I'd and I'll"".")
    End Sub

    Private Sub TBEmote_MouseHover(sender As Object, e As EventArgs) Handles TBEmote.MouseHover
        TTDir.SetToolTip(TBEmote, "This determines what symbol(s) the domme uses to begin an emote.")
    End Sub

    Private Sub TBEmoteEnd_MouseHover(sender As Object, e As EventArgs) Handles TBEmoteEnd.MouseHover
        TTDir.SetToolTip(TBEmoteEnd, "This determines what symbol(s) the domme uses to end an emote.")
    End Sub

    Private Sub LockOrgasmChances_MouseHover(sender As Object, e As EventArgs) Handles CBLockOrgasmChances.MouseHover
        TTDir.SetToolTip(CBLockOrgasmChances, "If checked the orgasm chances will be locked and unchangeable once you start the tease." & Environment.NewLine & Environment.NewLine &
            "Orgasm chances will be changeable and unlocked when out of a tease.")
    End Sub

    Private Sub CBDomDenialEnds_MouseHover(sender As Object, e As EventArgs) Handles CBDomDenialEnds.MouseHover
        TTDir.SetToolTip(CBDomDenialEnds, "Determines whether the domme will keep teasing you after you have been denied." & Environment.NewLine & Environment.NewLine &
            "If this box is checked, she will end the tease after she decides to deny your orgasm." & Environment.NewLine &
            "If it is unchecked, she may choose to start teasing you all over again.")
    End Sub

    Private Sub CBDomOrgasmEnds_MouseHover(sender As Object, e As EventArgs) Handles CBDomOrgasmEnds.MouseHover
        TTDir.SetToolTip(CBDomOrgasmEnds, "Determines whether the domme will keep teasing you after you have an orgasm." & Environment.NewLine & Environment.NewLine &
             "If this box is checked, she will end the tease after she allows you to cum." & Environment.NewLine &
             "If it is unchecked, she may choose to start teasing you all over again.")
    End Sub

    Private Sub LockOrgasm_MouseHover(sender As Object, e As EventArgs) Handles orgasmsperlockButton.MouseHover
        TTDir.SetToolTip(orgasmsperlockButton, "When this arrangement is selected, the domme will limit the number of" & Environment.NewLine &
                                                "orgasms she allows you to have according to the parameters you set." & Environment.NewLine & Environment.NewLine &
                                                "This will not be finalized until the Limit box is checked and you click ""Lock Selected""." & Environment.NewLine &
                                                "Once an orgasm limit has been finalized, it cannot be undone until the period of time is up!")
    End Sub

    Private Sub limitcheckbox_MouseHover(sender As Object, e As EventArgs) Handles limitcheckbox.MouseHover
        TTDir.SetToolTip(limitcheckbox, "When this arrangement is selected, the domme will limit the number of" & Environment.NewLine &
                                                "orgasms she allows you to have according to the parameters you set." & Environment.NewLine & Environment.NewLine &
                                                "This will not be finalized until the Limit box is checked and you click ""Lock Selected""." & Environment.NewLine &
                                                "Once an orgasm limit has been finalized, it cannot be undone until the period of time is up!")
    End Sub

    Private Sub orgasmsPerNumBox_MouseHover(sender As Object, e As EventArgs) Handles orgasmsPerNumBox.MouseHover
        TTDir.SetToolTip(orgasmsPerNumBox, "When this arrangement is selected, the domme will limit the number of" & Environment.NewLine &
                                                "orgasms she allows you to have according to the parameters you set." & Environment.NewLine & Environment.NewLine &
                                                "This will not be finalized until the Limit box is checked and you click ""Lock Selected""." & Environment.NewLine &
                                                "Once an orgasm limit has been finalized, it cannot be undone until the period of time is up!")
    End Sub

    Private Sub orgasmsperComboBox_MouseHover(sender As Object, e As EventArgs) Handles orgasmsperComboBox.MouseHover
        TTDir.SetToolTip(orgasmsperComboBox, "When this arrangement is selected, the domme will limit the number of" & Environment.NewLine &
                                                "orgasms she allows you to have according to the parameters you set." & Environment.NewLine & Environment.NewLine &
                                                "This will not be finalized until the Limit box is checked and you click ""Lock Selected""." & Environment.NewLine &
                                                "Once an orgasm limit has been finalized, it cannot be undone until the period of time is up!")
    End Sub

    Private Sub LockRandomOrgasm_MouseHover(sender As Object, e As EventArgs) Handles orgasmlockrandombutton.MouseHover

        TTDir.SetToolTip(orgasmsperComboBox, "When this arrangement is selected, the domme will randomly limit the" & Environment.NewLine &
                                              "number of orgasms she allows you to have for a random period of time." & Environment.NewLine & Environment.NewLine &
                                              "Once you confirm this choice, it cannot be undone until the period of time is up!")

        'LblDommeSettingsDescription.Text = "When this button is clicked, the domme will randomly limit the number of orgasms she allows you to have for a random period of time." & Environment.NewLine & Environment.NewLine & _
        '   "Her choice will be based on her level, so be careful. A higher level domme could limit the amount of orgasms you have for up to a year! Once you confirm this choice, it cannot be undone until the period of time is up!"
    End Sub

    Private Sub NBDomMoodMin_MouseHover(sender As Object, e As EventArgs) Handles NBDomMoodMin.MouseHover

        TTDir.SetToolTip(NBDomMoodMin, "Determines the low range of the domme's mood index." & Environment.NewLine &
                                       "The domme's mood may affect certain dialogue choices or outcomes." & Environment.NewLine & Environment.NewLine &
                                       "The higher this number is, the easier it is to put her in a bad mood." & Environment.NewLine &
                                       "Setting this value to ""1"" will prevent the domme from ever being in a bad mood.")



        'LblDommeSettingsDescription.Text = "Determines the low range of the domme's mood index. The domme's mood may affect certain dialogue choices or outcomes." & Environment.NewLine & Environment.NewLine & _
        '   "The higher this number is, the easier it is to put her in a bad mood. Setting this value to ""1"" will prevent the domme from ever being in a bad mood."
    End Sub

    Private Sub NBAvgCockMin_MouseHover(sender As Object, e As EventArgs) Handles NBAvgCockMin.MouseHover
        TTDir.SetToolTip(NBAvgCockMin, "Determines the lowest range of what the domme considers an average cock size." & Environment.NewLine & Environment.NewLine &
                                       "If your cock size is lower then this, the domme will consider it small.")
    End Sub

    Private Sub NBSelfAgeMin_Enter(sender As Object, e As EventArgs) Handles NBSelfAgeMin.MouseHover

        TTDir.SetToolTip(NBSelfAgeMin, "This is the age range that the domme considers ""not that young, but not that old""." & Environment.NewLine & Environment.NewLine &
                                       "If the domme's age is below this number, she will use dialogue options that suggest" & Environment.NewLine &
                                       "having the maturity and body of a girl in her early twenties.")
    End Sub

    Private Sub NBSelfAgeMax_Enter(sender As Object, e As EventArgs) Handles NBSelfAgeMax.MouseHover

        TTDir.SetToolTip(NBSelfAgeMax, "This is the age range that the domme considers ""not that young, but not that old""." & Environment.NewLine & Environment.NewLine &
               "If the domme's age is above this number, she will use dialogue options that suggest" & Environment.NewLine &
               "an exceptional amount of maturity, or having an aging body.")
    End Sub

    Private Sub apostropheCheckBox_MouseHover(sender As Object, e As EventArgs) Handles apostropheCheckBox.MouseHover

        TTDir.SetToolTip(apostropheCheckBox, "When this is checked, the domme won't use apostrophes when she types.")

        'LblDommeSettingsDescription.Text = "When this is checked, the domme won't use apostrophes when she types."
    End Sub

    Private Sub NBDomMoodMax_MouseHover(sender As Object, e As EventArgs) Handles NBDomMoodMax.MouseHover

        TTDir.SetToolTip(NBDomMoodMax, "Determines the high range of the domme's mood index." & Environment.NewLine &
                                    "The domme's mood may affect certain dialogue choices or outcomes." & Environment.NewLine & Environment.NewLine &
                                    "The lower this number is, the easier it is to put her in a good mood." & Environment.NewLine &
                                    "Setting this value to ""10"" will prevent the domme from ever being in an especially great mood.")



        'LblDommeSettingsDescription.Text = "Determines the high range of the domme's mood index. The domme's mood may affect certain dialogue choices or outcomes." & Environment.NewLine & Environment.NewLine & _
        '   "The lower this number is, the easier it is to put her in an especially great mood. Setting this value to ""10"" will prevent the domme from ever being in an especially great mood."
    End Sub

    Private Sub NBAvgCockMax_MouseHover(sender As Object, e As EventArgs) Handles NBAvgCockMax.MouseHover
        TTDir.SetToolTip(NBAvgCockMin, "Determines the highest range of what the domme considers an average cock size." & Environment.NewLine & Environment.NewLine &
           "If your cock size is higher than this, the domme will consider it big.")
    End Sub

    Private Sub NBSubAgeMin_Enter(sender As Object, e As EventArgs) Handles NBSubAgeMin.MouseHover
        TTDir.SetToolTip(NBSubAgeMin, "This is the age range that the domme considers ""not that young, but not that old""." & Environment.NewLine & Environment.NewLine &
            "If your age is below this number, the domme will use dialogue options that suggest" & Environment.NewLine &
            "you have the virility and body of a male in his early twenties.")
    End Sub

    Private Sub NBSubAgeMax_Enter(sender As Object, e As EventArgs) Handles NBSubAgeMax.MouseHover
        TTDir.SetToolTip(NBSubAgeMax, "This is the age range that the domme considers ""not that young, but not that old""." & Environment.NewLine & Environment.NewLine &
                                      "If your age is above this number, the domme will use dialogue options that suggest" & Environment.NewLine &
                                      "you're over the hill.")
    End Sub

    Private Sub PetNameBox1_Enter(sender As Object, e As EventArgs) Handles PetNameBox1.MouseHover
        TTDir.SetToolTip(PetNameBox1, "Enter a pet name that the domme will call you when she's in a great mood." & Environment.NewLine & Environment.NewLine &
                                      "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox2_Enter(sender As Object, e As EventArgs) Handles petnameBox2.MouseHover
        TTDir.SetToolTip(petnameBox2, "Enter a pet name that the domme will call you when she's in a great mood." & Environment.NewLine & Environment.NewLine &
                                        "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox3_Enter(sender As Object, e As EventArgs) Handles petnameBox3.MouseHover
        TTDir.SetToolTip(petnameBox3, "Enter a pet name that the domme will call you when she's in a neutral mood." & Environment.NewLine & Environment.NewLine &
                                       "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox4_Enter(sender As Object, e As EventArgs) Handles petnameBox4.MouseHover
        TTDir.SetToolTip(petnameBox4, "Enter a pet name that the domme will call you when she's in a neutral mood." & Environment.NewLine & Environment.NewLine &
                                      "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox5_Enter(sender As Object, e As EventArgs) Handles petnameBox5.MouseHover
        TTDir.SetToolTip(petnameBox5, "Enter a pet name that the domme will call you when she's in a neutral mood." & Environment.NewLine & Environment.NewLine &
                                             "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox6_Enter(sender As Object, e As EventArgs) Handles petnameBox6.MouseHover
        TTDir.SetToolTip(petnameBox6, "Enter a pet name that the domme will call you when she's in a neutral mood." & Environment.NewLine & Environment.NewLine &
                                     "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox7_Enter(sender As Object, e As EventArgs) Handles petnameBox7.MouseHover
        TTDir.SetToolTip(petnameBox7, "Enter a pet name that the domme will call you when she's in a bad mood." & Environment.NewLine & Environment.NewLine &
                                     "All pet name boxes must be filled in.")
    End Sub

    Private Sub PetNameBox8_Enter(sender As Object, e As EventArgs) Handles petnameBox8.MouseHover
        TTDir.SetToolTip(petnameBox8, "Enter a pet name that the domme will call you when she's in a bad mood." & Environment.NewLine & Environment.NewLine &
                                     "All pet name boxes must be filled in.")
    End Sub

    Private Sub BTNSaveDomSet_MouseHover(sender As Object, e As EventArgs) Handles BTNSaveDomSet.MouseHover
        TTDir.SetToolTip(BTNSaveDomSet, "Click to save this configuration of Domme Settings to a file that you can load at any time.")
    End Sub

    Private Sub BTNLoadDomSet_MouseHover(sender As Object, e As EventArgs) Handles BTNLoadDomSet.MouseHover
        TTDir.SetToolTip(BTNLoadDomSet, "Click to load a custom Domme Settings file you have previously created.")
    End Sub


    Private Sub BTNGlitterD_MouseHover(sender As Object, e As EventArgs) Handles BTNGlitterD.MouseHover
        TTDir.SetToolTip(BTNGlitterD, "This button allows you to change the color of the domme's name as it appears in the Glitter app." & Environment.NewLine &
                                      "A preview will appear in the text box below this button once a color has been selected.")
    End Sub
    Private Sub GlitterAV_MouseHover(sender As Object, e As EventArgs) Handles GlitterAV.MouseHover
        TTDir.SetToolTip(GlitterAV, "Click here to set the image the domme will use as her Glitter avatar.")
    End Sub
    Private Sub LBLGlitterNCDomme_Click(sender As Object, e As EventArgs) Handles LBLGlitterNCDomme.MouseHover, LBLGlitterNC1.MouseHover, LBLGlitterNC2.MouseHover, LBLGlitterNC3.MouseHover
        TTDir.SetToolTip(sender, "After clicking the ""Choose Name Color"" button above, a preview of the selected color will appear here.")
    End Sub
    Private Sub TBGlitterShortName_MouseHover(sender As Object, e As EventArgs) Handles TBGlitterShortName.MouseHover
        TTDir.SetToolTip(TBGlitterShortName, "This is the name that the domme's contacts will refer to her as in the Glitter feed.")
    End Sub
    Private Sub CBTease_MouseHover(sender As Object, e As EventArgs) Handles CBTease.MouseHover
        TTDir.SetToolTip(CBTease, "When this box is checked, the domme will make posts referencing your ongoing teasing and denial.")
    End Sub
    Private Sub CBEgotist_MouseHover(sender As Object, e As EventArgs) Handles CBEgotist.MouseHover
        TTDir.SetToolTip(CBEgotist, "When this box is checked, the domme will make self-centered posts stating how amazing she is.")
    End Sub
    Private Sub CBTrivia_MouseHover(sender As Object, e As EventArgs) Handles CBTrivia.MouseHover
        TTDir.SetToolTip(CBTrivia, "When this box is checked, the domme will make posts containing quotes or general trivia.")
    End Sub
    Private Sub CBDaily_MouseHover(sender As Object, e As EventArgs) Handles CBDaily.MouseHover
        TTDir.SetToolTip(CBDaily, "When this box is checked, the domme will make mundane posts about her day.")
    End Sub
    Private Sub CBCustom1_MouseHover(sender As Object, e As EventArgs) Handles CBCustom1.MouseHover
        TTDir.SetToolTip(CBCustom1, "When this box is checked, the domme will make posts taken from Custom 1" & Environment.NewLine &
                                  "folder in the Glitter scripts directory for her personality style.")
    End Sub
    Private Sub CBCustom2_MouseHover(sender As Object, e As EventArgs) Handles CBCustom2.MouseHover
        TTDir.SetToolTip(CBCustom2, "When this box is checked, the domme will make posts taken from Custom 2" & Environment.NewLine &
                                  "folder in the Glitter scripts directory for her personality style.")
    End Sub
    Private Sub GlitterSlider_MouseHover(sender As Object, e As EventArgs) Handles GlitterSlider.MouseHover
        TTDir.SetToolTip(GlitterSlider, "This slider determines how often the domme makes Glitter posts on her own." & Environment.NewLine &
                                             "The further to the right the slider is, the more often she posts.")
    End Sub
    Private Sub LBLGlitterSlider_MouseHover(sender As Object, e As EventArgs) Handles LBLGlitterSlider.MouseHover
        TTDir.SetToolTip(LBLGlitterSlider, "This slider determines how often the domme makes Glitter posts on her own." & Environment.NewLine &
                                             "The further to the right the slider is, the more often she posts.")
    End Sub

    Private Sub TBGlitter1_MouseHover(sender As Object, e As EventArgs) Handles TBGlitter1.MouseHover, TBGlitter2.MouseHover, TBGlitter3.MouseHover
        TTDir.SetToolTip(sender, "This will be the name of this contact as it appears in the Glitter feed.")
    End Sub
    Private Sub GlitterSlider1_MouseHover(sender As Object, e As EventArgs) Handles GlitterSlider1.MouseHover, GlitterSlider2.MouseHover, GlitterSlider3.MouseHover, LBLGlitterSlider1.MouseHover, LBLGlitterSlider2.MouseHover, LBLGlitterSlider3.MouseHover
        TTDir.SetToolTip(sender, "This slider determines how often this contact responds to the domme's Glitter posts." & Environment.NewLine &
                                         "The further to the right the slider is, the more often she responds.")
    End Sub
    Private Sub GlitterAV1_MouseHover(sender As Object, e As EventArgs) Handles GlitterAV1.MouseHover, GlitterAV2.MouseHover, GlitterAV3.MouseHover
        TTDir.SetToolTip(sender, "Click here to set the image that this contact will use as her Glitter avatar.")
    End Sub
    Private Sub CBGlitter1_MouseHover(sender As Object, e As EventArgs) Handles CBGlitter1.MouseHover, CBGlitter2.MouseHover, CBGlitter3.MouseHover
        TTDir.SetToolTip(sender, "This check box enables this contact's participation in the Glitter feed.")
    End Sub
    Private Sub BTNGlitter1_MouseHover(sender As Object, e As EventArgs) Handles BTNGlitter1.MouseHover, BTNGlitter2.MouseHover, BTNGlitter3.MouseHover
        TTDir.SetToolTip(sender, "This button allows you to change the color of this contact's name as it appears in the Glitter app.")
    End Sub

    Private Sub LBLContact1ImageDir_MouseHover(sender As Object, e As EventArgs) Handles TbxContact1ImageDir.MouseHover, TbxContact2ImageDir.MouseHover, TbxContact3ImageDir.MouseHover, TbxDomImageDir.MouseHover
        TTDir.SetToolTip(sender, CType(sender, TextBox).Text)
    End Sub

    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles BtnContact1ImageDir.MouseHover, BtnContact2ImageDir.MouseHover, BtnContact3ImageDir.MouseHover

        If RBEnglish.Checked Then TTDir.SetToolTip(sender, "Use this button to select a directory containing several image" & Environment.NewLine &
"set folders of the same model you're using as your contact.")
        If RBGerman.Checked Then TTDir.SetToolTip(sender, "Benutze diese Schaltfläche um einen Ordner zu wählen, welcher mehre" & Environment.NewLine &
"Bildersets von dem selben Model enthält, die du als Kontakt benutzt.")
    End Sub

    Private Sub LBLIHardcore_MouseHover(sender As Object, e As EventArgs) Handles TbxIHardcore.MouseHover
        TTDir.SetToolTip(TbxIHardcore, TbxIHardcore.Text)
    End Sub
    Private Sub LBLISoftcore_MouseHover(sender As Object, e As EventArgs) Handles TbxISoftcore.MouseHover
        TTDir.SetToolTip(TbxISoftcore, TbxISoftcore.Text)
    End Sub
    Private Sub LBLILesbian_MouseHover(sender As Object, e As EventArgs) Handles TbxILesbian.MouseHover
        TTDir.SetToolTip(TbxILesbian, TbxILesbian.Text)
    End Sub
    Private Sub LBLIBlowjob_MouseHover(sender As Object, e As EventArgs) Handles TbxIBlowjob.MouseHover
        TTDir.SetToolTip(TbxIBlowjob, TbxIBlowjob.Text)
    End Sub
    Private Sub LBLIFemdom_MouseHover(sender As Object, e As EventArgs) Handles TbxIFemdom.MouseHover
        TTDir.SetToolTip(TbxIFemdom, TbxIFemdom.Text)
    End Sub
    Private Sub LBLILezdom_MouseHover(sender As Object, e As EventArgs) Handles TbxILezdom.MouseHover
        TTDir.SetToolTip(TbxILezdom, TbxILezdom.Text)
    End Sub
    Private Sub LBLIHentai_MouseHover(sender As Object, e As EventArgs) Handles TbxIHentai.MouseHover
        TTDir.SetToolTip(TbxIHentai, TbxIHentai.Text)
    End Sub
    Private Sub LBLIGay_MouseHover(sender As Object, e As EventArgs) Handles TbxIGay.MouseHover
        TTDir.SetToolTip(TbxIGay, TbxIGay.Text)
    End Sub
    Private Sub LBLIMaledom_MouseHover(sender As Object, e As EventArgs) Handles TbxIMaledom.MouseHover
        TTDir.SetToolTip(TbxIMaledom, TbxIMaledom.Text)
    End Sub
    Private Sub LBLICaptions_MouseHover(sender As Object, e As EventArgs) Handles TbxICaptions.MouseHover
        TTDir.SetToolTip(TbxICaptions, TbxICaptions.Text)
    End Sub
    Private Sub LBLIGeneral_MouseHover(sender As Object, e As EventArgs) Handles TbxIGeneral.MouseHover
        TTDir.SetToolTip(TbxIGeneral, TbxIGeneral.Text)
    End Sub

    Private Sub LBLBoobPath_MouseHover(sender As Object, e As EventArgs) Handles TbxIBoobs.MouseHover
        TTDir.SetToolTip(TbxIBoobs, TbxIBoobs.Text)
    End Sub

    Private Sub LBLButtPath_MouseHover(sender As Object, e As EventArgs) Handles TbxIButts.MouseHover
        TTDir.SetToolTip(TbxIButts, TbxIButts.Text)
    End Sub

    Private Sub TxbVideoFolder_MouseHover(sender As Object, e As EventArgs) Handles TxbVideoHardCore.MouseHover,
                TxbVideoHardCoreD.MouseHover, TxbVideoSoftCore.MouseHover, TxbVideoSoftCoreD.MouseHover, TxbVideoLesbian.MouseHover,
                TxbVideoLesbianD.MouseHover, TxbVideoBlowjob.MouseHover, TxbVideoBlowjobD.MouseHover, TxbVideoFemdom.MouseHover,
                TxbVideoFemdomD.MouseHover, TxbVideoFemsub.MouseHover, TxbVideoFemsubD.MouseHover, TxbVideoJOI.MouseHover,
                TxbVideoJOID.MouseHover, TxbVideoCH.MouseHover, TxbVideoCHD.MouseHover, TxbVideoGeneral.MouseHover, TxbVideoGeneralD.MouseHover

        TTDir.SetToolTip(sender, CType(sender, TextBox).Text)
    End Sub

    Private Sub BTNRefreshVideos_MouseHover(sender As Object, e As EventArgs) Handles BTNRefreshVideos.MouseHover
        TTDir.SetToolTip(BTNRefreshVideos, "Use this button to refresh video paths.")
    End Sub

    Private Sub NBBirthdayMonth_MouseHover(sender As Object, e As EventArgs) Handles NBBirthdayMonth.MouseEnter
        TTDir.SetToolTip(NBBirthdayMonth, "Set the month you were born.")
        'LBLSubSettingsDescription.Text = "Set the month you were born."
    End Sub

    Private Sub Birthday_MouseHover(sender As Object, e As EventArgs) Handles LBLSubBirthday.MouseHover
        TTDir.SetToolTip(LBLSubBirthday, "Set your birthday with the format mm/dd.")
        'LBLSubSettingsDescription.Text = "Set the day you were born."
    End Sub

    Private Sub NBBirthdayDay_MouseHover(sender As Object, e As EventArgs) Handles NBBirthdayDay.MouseEnter
        TTDir.SetToolTip(NBBirthdayDay, "Set the day you were born.")
        'LBLSubSettingsDescription.Text = "Set the day you were born."
    End Sub

    Private Sub TBSubHairColor_MouseHover(sender As Object, e As EventArgs) Handles TBSubHairColor.MouseHover
        TTDir.SetToolTip(TBSubHairColor, "Enter your hair color using all lower case letters.")
        'LBLSubSettingsDescription.Text = "Enter your hair color using all lower case letters."
    End Sub

    Private Sub TBSubEyeColor_MouseHover(sender As Object, e As EventArgs) Handles TBSubEyeColor.MouseHover
        TTDir.SetToolTip(TBSubEyeColor, "Enter your eye color using all lower case letters.")
        'LBLSubSettingsDescription.Text = "Enter your eye color using all lower case letters."
    End Sub

    Private Sub CockSizeNumBox_MouseHover(sender As Object, e As EventArgs) Handles CockSizeNumBox.MouseEnter
        TTDir.SetToolTip(CockSizeNumBox, "Set your cock size in inches.")
        'LBLSubSettingsDescription.Text = "Set your cock size in inches."
    End Sub

    Private Sub CBSubCircumcised_MouseHover(sender As Object, e As EventArgs) Handles CBSubCircumcised.MouseHover
        TTDir.SetToolTip(CBSubCircumcised, "Check this box if your cock is circumcised.")
        'LBLSubSettingsDescription.Text = "Check this box if your cock is circumcised."
    End Sub

    Private Sub CBSubPierced_MouseHover(sender As Object, e As EventArgs) Handles CBSubPierced.MouseHover
        TTDir.SetToolTip(CBSubPierced, "Check this box if your cock is pierced.")
        'LBLSubSettingsDescription.Text = "Check this box if your cock is pierced."
    End Sub

    Private Sub CBCBTCock_MouseHover(sender As Object, e As EventArgs) Handles CockTortureEnabledCB.MouseHover

        TTDir.SetToolTip(CockTortureEnabledCB, "Check this box to enable cock torture." & Environment.NewLine & Environment.NewLine &
                                     "If this box is unchecked, the domme may still state that you're about to endure cock torture," & Environment.NewLine &
                                     "but the program will simply move to the next line instead of making you perform it.")



        'LBLSubSettingsDescription.Text = "Check this box to enabled cock torture." & Environment.NewLine & Environment.NewLine & "If this box is unchecked, the domme may still state that you're about to endure" _
        ' & " cock torture, but the program will simply move to the next line instead of making you perform it."
    End Sub

    Private Sub CBCBTBall_MouseHover(sender As Object, e As EventArgs) Handles BallTortureEnabledCB.MouseHover

        TTDir.SetToolTip(BallTortureEnabledCB, "Check this box to enable ball torture." & Environment.NewLine & Environment.NewLine &
                                  "If this box is unchecked, the domme may still state that you're about to endure ball torture," & Environment.NewLine &
                                  "but the program will simply move to the next line instead of making you perform it.")

        'LBLSubSettingsDescription.Text = "Check this box to enabled ball torture." & Environment.NewLine & Environment.NewLine & "If this box is unchecked, the domme may still state that you're about to endure" _
        '   & " ball torture, but the program will simply move to the next line instead of making you perform it."
    End Sub

    Private Sub CBTSlider_MouseHover(sender As Object, e As EventArgs) Handles CockAndBallTortureLevelSlider.MouseHover

        TTDir.SetToolTip(CockAndBallTortureLevelSlider, "This affects the severity of the CBT tasks you will be asked to perform." & Environment.NewLine & Environment.NewLine &
                                  "The higher this slider, the more severe the tasks will be.")

        'LBLSubSettingsDescription.Text = "This affects the severity of the CBT tasks you will be asked to perform. The higher this slider, the more severe the tasks will be."
    End Sub

    Private Sub GBPerformance_MouseHover(sender As Object, e As EventArgs)
        LBLSubSettingsDescription.Text = "This area keeps track of several statistics related to your time with the program."
    End Sub

    Private Sub CBOwnChastity_MouseHover(sender As Object, e As EventArgs) Handles CBOwnChastity.MouseHover

        TTDir.SetToolTip(CBOwnChastity, "Check this box if you own a chastity device and wish to run scripts" & Environment.NewLine &
                                        "where the domme places you in chastity.")
        'LBLSubSettingsDescription.Text = "Check this box if you own a chastity device. This allows the program to use that fact in various scripts."
    End Sub

    Private Sub CBChastityPA_MouseHover(sender As Object, e As EventArgs) Handles DoesChastityDeviceRequirePiercingCB.MouseHover
        TTDir.SetToolTip(DoesChastityDeviceRequirePiercingCB, "Check this box if your chastity device requires a piercing.")
        'LBLSubSettingsDescription.Text = "Check this box if your chastity device requires a piercing."
    End Sub

    Private Sub CBChastitySpikes_MouseHover(sender As Object, e As EventArgs) Handles ChastityDeviceContainsSpikesCB.MouseHover

        TTDir.SetToolTip(ChastityDeviceContainsSpikesCB, "Check this box if your chastity device contains spikes.")
        'LBLSubSettingsDescription.Text = "Check this box if your chastity device contains spikes."

    End Sub

    Private Sub TBGreeting_MouseHover(sender As Object, e As EventArgs) Handles TBGreeting.MouseHover

        TTDir.SetToolTip(TBGreeting, "Enter any number of words or phrases, separated by commas." & Environment.NewLine & Environment.NewLine &
                                  "When you use any of these words/phrases by themselves after starting the" & Environment.NewLine &
                                  "program, the domme will recognize it as a greeting and begin the tease.")
    End Sub

    Private Sub TBYes_MouseHover(sender As Object, e As EventArgs) Handles TBYes.MouseHover

        TTDir.SetToolTip(TBYes, "Enter any number of words or phrases, separated by commas." & Environment.NewLine & Environment.NewLine &
                                    "The domme will recognize these as ""yes"" answers to Multiple Choice sections.")

        ' LBLSubSettingsDescription.Text = "Enter any number of words or phrases, separated by commas. The domme will recognize these as ""yes"" answers to Multiple Choice sections."
    End Sub

    Private Sub TBNo_MouseHover(sender As Object, e As EventArgs) Handles TBNo.MouseHover

        TTDir.SetToolTip(TBNo, "Enter any number of words or phrases, separated by commas." & Environment.NewLine & Environment.NewLine &
                                 "The domme will recognize these as ""no"" answers to Multiple Choice sections.")

        'LBLSubSettingsDescription.Text = "Enter any number of words or phrases, separated by commas. The domme will recognize these as ""no"" answers to Multiple Choice sections."
    End Sub

    Private Sub TBHonorific_MouseHover(sender As Object, e As EventArgs) Handles TBHonorific.MouseHover

        TTDir.SetToolTip(TBHonorific, "Enter an honorific to use for the domme, such as Mistress, Goddess, Princess, etc.")

        'LBLSubSettingsDescription.Text = "Enter an honorific to use for the domme, such as Mistress, Goddess, Princess, etc."
    End Sub

    Private Sub CBHonorificInclude_MouseHover(sender As Object, e As EventArgs) Handles CBHonorificInclude.MouseHover

        TTDir.SetToolTip(CBHonorificInclude, "When this box is checked, the domme's honorific must be included with" & Environment.NewLine &
                                             "greetings and yes or no responses used during multiple choice segments.")

        'LBLSubSettingsDescription.Text = "When this box is checked, the domme's honorific must be included with greetings and yes or no responses used during multiple choice segments."
    End Sub
    Private Sub CBHonorificCapitalized_MouseHover(sender As Object, e As EventArgs) Handles CBHonorificCapitalized.MouseHover

        TTDir.SetToolTip(CBHonorificCapitalized, "When this box is checked, the domme's honorific must be capitalized where it is required.")
        'LBLSubSettingsDescription.Text = "When this box is checked, the domme's honorific must be capitalized where it is required."
    End Sub

    Private Sub NBLongEdge_MouseHover(sender As Object, e As EventArgs) Handles NBLongEdge.MouseEnter
        LBLSubSettingsDescription.Text = "Sets how long (in seconds) it will take after being told to edge for the domme to believe you have been trying to reach the edge for too long."
    End Sub

    Private Sub BTNWICreateURL_MouseHover(sender As Object, e As EventArgs) Handles BTNWICreateURL.MouseHover
        TTDir.SetToolTip(BTNWICreateURL, "Click here to create a new URL File." & Environment.NewLine & Environment.NewLine &
                                         "URL Files create a txt file containing the URL address" & Environment.NewLine &
                                         "of every image posted at the image blog you specify.")
    End Sub

    Private Sub CBWIreview_MouseHover(sender As Object, e As EventArgs) Handles CBWIReview.MouseHover
        TTDir.SetToolTip(CBWIReview, "When this is checked, you'll need to review" & Environment.NewLine &
                                     "each image before it's added to the URL File.")
    End Sub

    Private Sub CBWISavetoDisk_MouseHover(sender As Object, e As EventArgs) Handles CBWISaveToDisk.MouseHover
        TTDir.SetToolTip(CBWISaveToDisk, "When this is checked, images will also be saved" & Environment.NewLine &
                                         "to the specified HDD directory as they are added.")
    End Sub

    Private Sub BTNWIAddandContinue_MouseHover(sender As Object, e As EventArgs) Handles BTNWIAddandContinue.MouseHover
        TTDir.SetToolTip(BTNWIAddandContinue, "When reviewing images, click this button to add the" & Environment.NewLine &
                                              "current image to the URL File and continue to the next.")
    End Sub

    Private Sub BTNWIContinue_MouseHover(sender As Object, e As EventArgs) Handles BTNWIContinue.MouseHover
        TTDir.SetToolTip(BTNWIContinue, "When reviewing images, click this button to skip the" & Environment.NewLine &
                                        "current image without adding it to the URL File.")
    End Sub

    Private Sub BTNWICancel_MouseHover(sender As Object, e As EventArgs) Handles BTNWICancel.MouseHover
        TTDir.SetToolTip(BTNWICancel, "Use this button to cancel URL File creation.")
    End Sub

    Private Sub BTNWIOpenURL_MouseHover(sender As Object, e As EventArgs) Handles BTNWIOpenURL.MouseHover
        TTDir.SetToolTip(BTNWIOpenURL, "Use this button to view a URL File you have previously created.")
    End Sub

    Private Sub BTNWIPrevious_MouseHover(sender As Object, e As EventArgs) Handles BTNWIPrevious.MouseHover
        TTDir.SetToolTip(BTNWIPrevious, "Use this button to view the previous image of a URL File.")
    End Sub

    Private Sub BTNWINext_MouseHover(sender As Object, e As EventArgs) Handles BTNWINext.MouseHover
        TTDir.SetToolTip(BTNWINext, "Use this button to view the next image of a URL File.")
    End Sub

    Private Sub BTNWIRemove_MouseHover(sender As Object, e As EventArgs) Handles BTNWIRemove.MouseHover
        TTDir.SetToolTip(BTNWIRemove, "Use this button to remove an image from a URL File.")
    End Sub

    Private Sub BTNWILiked_MouseHover(sender As Object, e As EventArgs) Handles BTNWILiked.MouseHover
        TTDir.SetToolTip(BTNWILiked, "Use this button to add the current image to your Liked URL Files list.")
    End Sub

    Private Sub BTNWIDisliked_MouseHover(sender As Object, e As EventArgs) Handles BTNWIDisliked.MouseHover
        TTDir.SetToolTip(BTNWIDisliked, "Use this button to add the current image to your Disliked URL Files list.")
    End Sub

    Private Sub BTNWISave_MouseHover(sender As Object, e As EventArgs) Handles BTNWISave.MouseHover
        TTDir.SetToolTip(BTNWISave, "Use this button to save the current image to your hard drive.")
    End Sub

    Private Sub TBWIDirectory_MouseHover(sender As Object, e As EventArgs) Handles TBWIDirectory.MouseHover
        TTDir.SetToolTip(TBWIDirectory, "This is where images will be saved if ""Save Images to Disk"" is checked.")
    End Sub

    Private Sub BTNWIBrowse_MouseHover(sender As Object, e As EventArgs) Handles BTNWIBrowse.MouseHover
        TTDir.SetToolTip(BTNWIBrowse, "Select the directory where images will be saved to disk.")
    End Sub

    Private Sub SliderVVolume_MouseHover(sender As Object, e As EventArgs) Handles SliderVVolume.MouseHover
        TTDir.SetToolTip(SliderVVolume, "Adusts the volume of the domme's TTS voice.")
    End Sub

    Private Sub SliderVRate_MouseHover(sender As Object, e As EventArgs) Handles SliderVRate.MouseHover
        TTDir.SetToolTip(SliderVRate, "Adusts the speed of the domme's TTS voice.")
    End Sub

#End Region

#Region "------------------------------------- GeneralTab -----------------------------------------------"

    Private Sub BtnImportSettings_Click(sender As Object, e As EventArgs) Handles BtnImportSettings.Click
        My.MySettings.importOnRestart()
    End Sub

    Private Sub TimeStampCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TimeStampCheckBox.CheckedChanged
        mySettingsAccessor.IsTimeStampEnabled = TimeStampCheckBox.Checked
    End Sub

    Private Sub ShowNamesCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ShowNamesCheckBox.CheckedChanged
        mySettingsAccessor.ShowNames = ShowNamesCheckBox.Checked
    End Sub

    Private Sub TypeInstantlyCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TypeInstantlyCheckBox.CheckedChanged
        mySettingsAccessor.DoesDommeTypeInstantly = TypeInstantlyCheckBox.Checked
    End Sub

    Private Sub CBWebtease_CheckedChanged(sender As Object, e As EventArgs) Handles WebTeaseMode.CheckedChanged
        mySettingsAccessor.WebTeaseModeEnabled = WebTeaseMode.Checked
    End Sub

    Private Sub CBBlogImageWindow_CheckedChanged(sender As Object, e As EventArgs) Handles CBBlogImageWindow.CheckedChanged
        My.Settings.CBBlogImageMain = CBBlogImageWindow.Checked
    End Sub

    Private Sub LandscapeCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LandscapeCheckBox.CheckedChanged
        My.Settings.CBStretchLandscape = LandscapeCheckBox.Checked
    End Sub

    Private Sub CBSettingsPause_CheckedChanged(sender As Object, e As EventArgs) Handles CBSettingsPause.CheckedChanged
        My.Settings.CBSettingsPause = CBSettingsPause.Checked
    End Sub

    Private Sub Radio_LostFocus(sender As Object, e As EventArgs) Handles TeaseSlideShowRadio.LostFocus, ManualSlideShowRadio.LostFocus, TimedSlideShowRadio.LostFocus
        If TeaseSlideShowRadio.Checked Then My.Settings.SlideshowMode = "Tease"
        If TimedSlideShowRadio.Checked Then My.Settings.SlideshowMode = "Timed"
        If ManualSlideShowRadio.Checked Then My.Settings.SlideshowMode = "Manual"
    End Sub

    Private Sub CBAutosaveChatlog_CheckedChanged(sender As Object, e As EventArgs) Handles CBAutosaveChatlog.CheckedChanged
        My.Settings.CBAutosaveChatlog = CBAutosaveChatlog.Checked
    End Sub

    Private Sub CBSaveChatlogExit_CheckedChanged(sender As Object, e As EventArgs) Handles CBSaveChatlogExit.CheckedChanged
        My.Settings.CBExitSaveChatlog = CBSaveChatlogExit.Checked
    End Sub

    Private Sub CBSlideshowSubDir_CheckedChanged(sender As Object, e As EventArgs) Handles CBSlideshowSubDir.CheckedChanged
        My.Settings.CBSlideshowSubDir = CBSlideshowSubDir.Checked
    End Sub

    Private Sub CBSlideshowRandom_CheckedChanged(sender As Object, e As EventArgs) Handles CBSlideshowRandom.CheckedChanged
        My.Settings.CBSlideshowRandom = CBSlideshowRandom.Checked
    End Sub

    Private Sub CBLockWindow_CheckedChanged(sender As Object, e As EventArgs) Handles CBInputIcon.CheckedChanged
        My.Settings.CBInputIcon = CBInputIcon.Checked
    End Sub

    Private Sub BTNDomColor_Click(sender As Object, e As EventArgs) Handles BTNDomColor.Click
        If GetColor.ShowDialog() = DialogResult.OK Then
            My.Settings.DomColorColor = GetColor.Color
            LBLDomColor.ForeColor = GetColor.Color
            My.Settings.DomColor = Color2Html(GetColor.Color)
        End If
    End Sub

    Private Sub BTNSubColor_Click(sender As Object, e As EventArgs) Handles BTNSubColor.Click
        If GetColor.ShowDialog() = DialogResult.OK Then
            My.Settings.SubColorColor = GetColor.Color
            LBLSubColor.ForeColor = GetColor.Color
            My.Settings.SubColor = Color2Html(GetColor.Color)
        End If
    End Sub

    Private Sub TimedSlideShowRadio_CheckedChanged(sender As Object, e As EventArgs) Handles TimedSlideShowRadio.CheckedChanged
        If MainWindow.ssh.SlideshowLoaded AndAlso TimedSlideShowRadio.Checked Then
            MainWindow.ssh.SlideshowTimerTick = SlideShowNumBox.Value
            MainWindow.SlideshowTimer.Start()
        End If
    End Sub

    Private Sub TeaseSlideShowRadio_CheckedChanged(sender As Object, e As EventArgs) Handles TeaseSlideShowRadio.CheckedChanged
        If Not TimedSlideShowRadio.Checked AndAlso Not MainWindow.FormLoading Then
            MainWindow.SlideshowTimer.Stop()
        End If
    End Sub

    Private Sub ManualSlideShowRadio_CheckedChanged(sender As Object, e As EventArgs) Handles ManualSlideShowRadio.CheckedChanged
        If Not TimedSlideShowRadio.Checked Then
            MainWindow.SlideshowTimer.Stop()
        End If
    End Sub

    Private Sub DommeMessageFontCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles DommeMessageFontCB.SelectedValueChanged
        My.Settings.DomFont = DommeMessageFontCB.Text
    End Sub

    Private Sub SubMessageFontCB_LostFocus(sender As Object, e As EventArgs) Handles SubMessageFontCB.LostFocus
        My.Settings.SubFont = SubMessageFontCB.Text
    End Sub

    Private Sub NBFontSizeD_LostFocus(sender As Object, e As EventArgs) Handles NBFontSizeD.LostFocus
        My.Settings.DomFontSize = NBFontSizeD.Value
    End Sub

    Private Sub NBFontSize_LostFocus(sender As Object, e As EventArgs) Handles NBFontSize.LostFocus
        My.Settings.SubFontSize = NBFontSize.Value
    End Sub

    Private Sub CBImageInfo_CheckedChanged(sender As Object, e As EventArgs) Handles CBImageInfo.CheckedChanged
        MainWindow.LBLImageInfo.Visible = CBImageInfo.Checked
        My.Settings.CBImageInfo = Not CBImageInfo.Checked
    End Sub

#End Region ' General

#Region "-------------------------------------- Domme Tab -----------------------------------------------"
    Private Sub PetNameBox1_LostFocus(sender As Object, e As EventArgs) Handles PetNameBox1.LostFocus, petnameBox2.LostFocus, petnameBox3.LostFocus, petnameBox4.LostFocus, petnameBox5.LostFocus, petnameBox6.LostFocus, petnameBox7.LostFocus, petnameBox8.LostFocus
        Dim defaultPetNames As List(Of String) = New List(Of String)
        defaultPetNames.Add("stroker")
        defaultPetNames.Add("loser")
        defaultPetNames.Add("slave")
        defaultPetNames.Add("bitch boy")
        defaultPetNames.Add("wanker")

        Dim workingPetNameBox As TextBox
        workingPetNameBox = CType(sender, TextBox)

        If String.IsNullOrWhiteSpace(workingPetNameBox.Text) Then workingPetNameBox.Text = defaultPetNames(MainWindow.ssh.randomizer.Next(0, 5))

        My.Settings.pnSetting1 = PetNameBox1.Text
        My.Settings.pnSetting2 = petnameBox2.Text
        My.Settings.pnSetting3 = petnameBox3.Text
        My.Settings.pnSetting4 = petnameBox4.Text
        My.Settings.pnSetting5 = petnameBox5.Text
        My.Settings.pnSetting6 = petnameBox6.Text
        My.Settings.pnSetting7 = petnameBox7.Text
        My.Settings.pnSetting8 = petnameBox8.Text
    End Sub

    ''' <summary>
    ''' Locks the Orgasm Chances.
    ''' </summary>
    ''' <param name="lock">If True the Controls regarding orgasms are locked.</param>
    Friend Sub LockOrgasmChances(ByVal lock As Boolean)

        alloworgasmComboBox.Enabled = Not lock
        ruinorgasmComboBox.Enabled = Not lock

        GBRangeOrgasmChance.Enabled = Not lock
        GBRangeRuinChance.Enabled = Not lock

    End Sub

    Private Sub DominationLevel_ValueChanged(sender As Object, e As EventArgs) Handles DominationLevel.ValueChanged
        mySettingsAccessor.DominationLevel = DomLevel.Create(Convert.ToInt32(DominationLevel.Value)).Value
    End Sub

    Private Sub alloworgasmComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles alloworgasmComboBox.SelectedIndexChanged
        My.Settings.OrgasmAllow = alloworgasmComboBox.Text
    End Sub

    Private Sub ruinorgasmComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ruinorgasmComboBox.SelectedIndexChanged
        My.Settings.OrgasmRuin = ruinorgasmComboBox.Text
    End Sub

    Private Sub DomAgeNumBox_ValueChanged(sender As Object, e As EventArgs) Handles domageNumBox.ValueChanged
        My.Settings.DomAge = domageNumBox.Value
    End Sub

    Private Sub TBDomHairColor_LostFocus(sender As Object, e As EventArgs) Handles TBDomHairColor.LostFocus
        My.Settings.DomHair = TBDomHairColor.Text
    End Sub

    Private Sub domhairlengthComboBox_LostFocus(sender As Object, e As EventArgs) Handles domhairlengthComboBox.LostFocus
        My.Settings.DomHairLength = domhairlengthComboBox.Text
    End Sub

    Private Sub TBDomEyeColor_LostFocus(sender As Object, e As EventArgs) Handles TBDomEyeColor.LostFocus
        My.Settings.DomEyes = TBDomEyeColor.Text
    End Sub

    Private Sub boobComboBox_LostFocus(sender As Object, e As EventArgs) Handles boobComboBox.LostFocus
        My.Settings.DomCup = boobComboBox.Text
    End Sub

    Private Sub dompubichairComboBox_LostFocus(sender As Object, e As EventArgs) Handles dompubichairComboBox.LostFocus
        My.Settings.DomPubicHair = dompubichairComboBox.Text
    End Sub

    Private Sub crazyCheckBox_LostFocus(sender As Object, e As EventArgs) Handles crazyCheckBox.LostFocus
        My.Settings.DomCrazy = crazyCheckBox.Checked
    End Sub

    Private Sub CBDomTattoos_LostFocus(sender As Object, e As EventArgs) Handles CBDomTattoos.LostFocus
        My.Settings.DomTattoos = CBDomTattoos.Checked
    End Sub

    Private Sub CBDomFreckles_LostFocus(sender As Object, e As EventArgs) Handles CBDomFreckles.LostFocus
        My.Settings.DomFreckles = CBDomFreckles.Checked
    End Sub

    Private Sub vulgarCheckBox_LostFocus(sender As Object, e As EventArgs) Handles vulgarCheckBox.LostFocus
        My.Settings.DomVulgar = vulgarCheckBox.Checked
    End Sub

    Private Sub supremacistCheckBox_LostFocus(sender As Object, e As EventArgs) Handles supremacistCheckBox.LostFocus
        My.Settings.DomSupremacist = supremacistCheckBox.Checked
    End Sub

    Private Sub LCaseCheckBoxCheckBox_LostFocus(sender As Object, e As EventArgs) Handles LCaseCheckBox.LostFocus
        My.Settings.DomLowercase = LCaseCheckBox.Checked
    End Sub

    Private Sub apostropheCheckBox_LostFocus(sender As Object, e As EventArgs) Handles apostropheCheckBox.LostFocus
        My.Settings.DomNoApostrophes = apostropheCheckBox.Checked
    End Sub

    Private Sub commaCheckBox_LostFocus(sender As Object, e As EventArgs) Handles commaCheckBox.LostFocus
        My.Settings.DomNoCommas = commaCheckBox.Checked
    End Sub

    Private Sub periodCheckBox_LostFocus(sender As Object, e As EventArgs) Handles periodCheckBox.LostFocus
        My.Settings.DomNoPeriods = periodCheckBox.Checked
    End Sub

    Private Sub TBEmote_LostFocus(sender As Object, e As EventArgs) Handles TBEmote.LostFocus
        My.Settings.TBEmote = TBEmote.Text
    End Sub

    Private Sub TBEmoteEnd_LostFocus(sender As Object, e As EventArgs) Handles TBEmoteEnd.LostFocus
        My.Settings.TBEmoteEnd = TBEmoteEnd.Text
    End Sub

    Private Sub CBMeMyMine_LostFocus(sender As Object, e As EventArgs) Handles CBMeMyMine.LostFocus
        My.Settings.DomMeMyMine = CBMeMyMine.Checked
    End Sub

    Private Sub CBDomDenialEnds_LostFocus(sender As Object, e As EventArgs) Handles CBDomDenialEnds.LostFocus
        My.Settings.DomDenialEnd = CBDomDenialEnds.Checked
    End Sub

    Private Sub CBDomOrgasmEnds_LostFocus(sender As Object, e As EventArgs) Handles CBDomOrgasmEnds.LostFocus
        My.Settings.DomOrgasmEnd = CBDomOrgasmEnds.Checked
    End Sub

    Private Sub NBDomMoodMin_LostFocus(sender As Object, e As EventArgs) Handles NBDomMoodMin.LostFocus
        My.Settings.DomMoodMin = NBDomMoodMin.Value
    End Sub

    Private Sub NBDomMoodMax_LostFocus(sender As Object, e As EventArgs) Handles NBDomMoodMax.LostFocus
        My.Settings.DomMoodMax = NBDomMoodMax.Value
    End Sub

    Private Sub NBDomMoodMin_ValueChanged(sender As Object, e As EventArgs) Handles NBDomMoodMin.ValueChanged
        If NBDomMoodMin.Value > NBDomMoodMax.Value Then NBDomMoodMin.Value = NBDomMoodMax.Value
    End Sub

    Private Sub NBDomMoodMax_ValueChanged(sender As Object, e As EventArgs) Handles NBDomMoodMax.ValueChanged
        If NBDomMoodMax.Value < NBDomMoodMin.Value Then NBDomMoodMax.Value = NBDomMoodMin.Value
    End Sub

    Private Sub NBAvgCockMin_LostFocus(sender As Object, e As EventArgs) Handles NBAvgCockMin.LostFocus
        My.Settings.AvgCockMin = NBAvgCockMin.Value
    End Sub

    Private Sub NBAvgCockMax_LostFocus(sender As Object, e As EventArgs) Handles NBAvgCockMax.LostFocus
        My.Settings.AvgCockMax = NBAvgCockMax.Value
    End Sub

    Private Sub NBAvgCockMin_ValueChanged(sender As Object, e As EventArgs) Handles NBAvgCockMin.ValueChanged
        If NBAvgCockMin.Value > NBAvgCockMax.Value Then NBAvgCockMin.Value = NBAvgCockMax.Value
    End Sub

    Private Sub NBAvgCockMax_ValueChanged(sender As Object, e As EventArgs) Handles NBAvgCockMax.ValueChanged
        If NBAvgCockMax.Value < NBAvgCockMin.Value Then NBAvgCockMax.Value = NBAvgCockMin.Value
    End Sub

    Private Sub NBSelfAgeMin_LostFocus(sender As Object, e As EventArgs) Handles NBSelfAgeMin.LostFocus
        My.Settings.SelfAgeMin = NBSelfAgeMin.Value
    End Sub

    Private Sub NBSelfAgeMax_LostFocus(sender As Object, e As EventArgs) Handles NBSelfAgeMax.LostFocus
        My.Settings.SelfAgeMax = NBSelfAgeMax.Value
    End Sub

    Private Sub NBSelfAgeMin_ValueChanged(sender As Object, e As EventArgs) Handles NBSelfAgeMin.ValueChanged
        If NBSelfAgeMin.Value > NBSelfAgeMax.Value Then NBSelfAgeMin.Value = NBSelfAgeMax.Value
    End Sub

    Private Sub NBSelfAgeMax_ValueChanged(sender As Object, e As EventArgs) Handles NBSelfAgeMax.ValueChanged
        If NBSelfAgeMax.Value < NBSelfAgeMin.Value Then NBSelfAgeMax.Value = NBSelfAgeMin.Value
    End Sub

    Private Sub NBSubAgeMin_LostFocus(sender As Object, e As EventArgs) Handles NBSubAgeMin.LostFocus
        My.Settings.SubAgeMin = NBSubAgeMin.Value
    End Sub

    Private Sub NBSubAgeMax_LostFocus(sender As Object, e As EventArgs) Handles NBSubAgeMax.LostFocus
        My.Settings.SubAgeMax = NBSubAgeMax.Value
    End Sub

    Private Sub NBSubAgeMin_ValueChanged(sender As Object, e As EventArgs) Handles NBSubAgeMin.ValueChanged
        If NBSubAgeMin.Value > NBSubAgeMax.Value Then NBSubAgeMin.Value = NBSubAgeMax.Value
    End Sub

    Private Sub NBSubAgeMax_ValueChanged(sender As Object, e As EventArgs) Handles NBSubAgeMax.ValueChanged
        If NBSubAgeMax.Value < NBSubAgeMin.Value Then NBSubAgeMax.Value = NBSubAgeMin.Value
    End Sub

    Private Sub NBEmpathy_LostFocus(sender As Object, e As EventArgs) Handles NBEmpathy.LostFocus
        mySettingsAccessor.ApathyLevel = ApathyLevel.Create(CType(NBEmpathy.Value, Integer)).Value
    End Sub

    Private Sub domlevelNumBox_ValueChanged(sender As Object, e As EventArgs) Handles DominationLevel.ValueChanged
        DomLevelDescLabel.Text = mySettingsAccessor.DominationLevel.ToString()
    End Sub

    Private Sub NBEmpathy_ValueChanged(sender As Object, e As EventArgs) Handles NBEmpathy.ValueChanged
        LBLEmpathy.Text = mySettingsAccessor.ApathyLevel.ToString()
    End Sub

#End Region ' Domme

#Region "-------------------------------------- Scripts -------------------------------------------------"

    ''' <summary>
    ''' Forces the Focus onto the CheckedListBoxes in Tabcontrol.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TCScripts_TabIndexChanged(sender As Object, e As EventArgs) Handles TCScripts.SelectedIndexChanged
        If TCScripts.SelectedTab Is ScriptsStartTab Then
            StartScripts.Focus()
        ElseIf TCScripts.SelectedTab Is ScriptsModuleTab Then
            ModuleScripts.Focus()
        ElseIf TCScripts.SelectedTab Is ScriptsLinkTab Then
            LinkScripts.Focus()
        ElseIf TCScripts.SelectedTab Is ScriptsEndTab Then
            EndScripts.Focus()
        End If
    End Sub

    Public Shared Sub saveCheckedListBox(target As CheckedListBox, filePath As String)
        If Not Directory.Exists(Path.GetDirectoryName(filePath)) Then _
            Directory.CreateDirectory(Path.GetDirectoryName(filePath))

        Using fs As New FileStream(filePath, IO.FileMode.Create), BinWrite As New BinaryWriter(fs)
            For i = 0 To target.Items.Count - 1
                BinWrite.Write(CStr(target.Items(i)))
                BinWrite.Write(CBool(target.GetItemChecked(i)))
            Next
        End Using
    End Sub

    Private Sub SaveStartScripts() Handles StartScripts.LostFocus
        saveCheckedListBox(StartScripts, Ssh.Files.StartChecklist)
    End Sub

    Private Sub SaveModuleScripts() Handles ModuleScripts.LostFocus
        saveCheckedListBox(ModuleScripts, Ssh.Files.ModuleChecklist)
    End Sub

    Private Sub SaveLinkScripts() Handles LinkScripts.LostFocus
        saveCheckedListBox(LinkScripts, Ssh.Files.LinkChecklist)
    End Sub

    Private Sub SaveEndScripts() Handles EndScripts.LostFocus
        saveCheckedListBox(EndScripts, Ssh.Files.EndChecklist)
    End Sub

    Public Sub ScriptsStartTab_VisibleChanged() Handles ScriptsStartTab.VisibleChanged
        If (Not ScriptsStartTab.Visible) Then
            Return
        End If
        LoadScriptMetaData(StartScripts, mySettingsAccessor.DommePersonality, "Stroke", SessionPhase.Start, False)
    End Sub

    Public Sub ScriptsModuleTab_VisibleChanged() Handles ScriptsModuleTab.VisibleChanged
        If (Not ScriptsModuleTab.Visible) Then
            Return
        End If
        LoadScriptMetaData(ModuleScripts, mySettingsAccessor.DommePersonality, "Modules", SessionPhase.Modules, False)
    End Sub

    Public Sub ScriptsLinkTab_VisibleChanged() Handles ScriptsLinkTab.VisibleChanged
        If (Not ScriptsLinkTab.Visible) Then
            Return
        End If
        LoadScriptMetaData(LinkScripts, mySettingsAccessor.DommePersonality, "Stroke", Constants.SessionPhase.Link, False)
    End Sub

    Public Sub ScriptsEndTab_VisibleChanged() Handles ScriptsEndTab.VisibleChanged
        If (Not ScriptsEndTab.Visible) Then
            Return
        End If
        LoadScriptMetaData(EndScripts, mySettingsAccessor.DommePersonality, "Stroke", Constants.SessionPhase.End, False)
    End Sub

    Private Sub Scripts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles StartScripts.SelectedIndexChanged, ModuleScripts.SelectedIndexChanged, LinkScripts.SelectedIndexChanged, EndScripts.SelectedIndexChanged
        Dim sessionPhase As SessionPhase = GetSessionPhase(TCScripts.SelectedTab)
        Dim target As CheckedListBox = GetScriptsCheckedListBox(sessionPhase)
        If target.SelectedIndex = -1 Then ScriptStatusUnlock(False) : Exit Sub


        Dim scriptType As String = "Stroke"
        If target Is StartScripts Then
            sessionPhase = SessionPhase.Start
        ElseIf target Is ModuleScripts Then
            scriptType = "Modules"
            sessionPhase = SessionPhase.Modules
        ElseIf target Is LinkScripts Then
            sessionPhase = SessionPhase.Link
        ElseIf target Is EndScripts Then
            sessionPhase = SessionPhase.End
        Else
            Throw New NotImplementedException("The starting control is not implemented in this method.")
        End If

        Dim scripts As List(Of ScriptMetaData) = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, If(sessionPhase = SessionPhase.Modules, "Modules", "Stroke"), sessionPhase, True)
        Dim script = scripts.First(Function(smd) smd.Name = target.SelectedItem.ToString())
        ScriptInfoTextArea.Text = script.Info
        GetScriptStatus(script)
    End Sub

    ''' <summary>
    ''' This Procedure enables or disables the Controls to view a script status.
    ''' On disabling it will clear the textbox.text as well.
    ''' </summary>
    ''' <param name="state">False disables the Controls. True enables them.</param>
    Private Sub ScriptStatusUnlock(state As Boolean)
        BTNScriptOpen.Enabled = state
        ScriptInfoTextArea.Enabled = state
        RTBScriptReq.Enabled = state
        LBLScriptReq.Enabled = state
        If Not state Then
            ScriptInfoTextArea.Text = ""
            RTBScriptReq.Text = ""
            LBLScriptReq.Text = ""
        End If
    End Sub

    Private Sub BtnScriptsOpen_Click(sender As Object, e As EventArgs) Handles BTNScriptOpen.Click
        Dim sessionPhase As SessionPhase = GetSessionPhase(TCScripts.SelectedTab)
        Dim checkedListBox As CheckedListBox = GetScriptsCheckedListBox(sessionPhase)
        Dim scripts As List(Of ScriptMetaData) = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, IIf(sessionPhase = SessionPhase.Modules, "Modules", "Stroke"), sessionPhase, True)

        Dim clickedScript = scripts.First(Function(smd) smd.Name = checkedListBox.SelectedItem.ToString())
        MainWindow.ShellExecute(clickedScript.Key)
    End Sub

    Private Sub BtnScriptsSelectAutomated_Click(sender As Object, e As EventArgs) Handles SelectAvailableScriptsButton.Click, SelectNoScriptsButton.Click, SelectAllScriptsButton.Click
        ' Lock Buttons to prevent double trigger
        SelectAvailableScriptsButton.Enabled = False
        SelectNoScriptsButton.Enabled = False
        SelectAllScriptsButton.Enabled = False

        Dim sessionPhase As SessionPhase = GetSessionPhase(TCScripts.SelectedTab)
        Dim target As CheckedListBox = GetScriptsCheckedListBox(sessionPhase)
        Dim saveAction As Action = GetSaveAction(sessionPhase, target)
        Try
            target.BeginUpdate()

            If sender Is SelectNoScriptsButton Then
                For i As Integer = 0 To target.Items.Count - 1
                    target.SetItemChecked(i, False)
                Next
            ElseIf sender Is SelectAllScriptsButton Then
                For i As Integer = 0 To target.Items.Count - 1
                    target.SetItemChecked(i, True)
                Next

            ElseIf sender Is SelectAvailableScriptsButton Then
                Dim scriptFolder As String
                If sessionPhase = SessionPhase.Start Then
                    scriptFolder = Ssh.Folders.StartScripts
                ElseIf sessionPhase = SessionPhase.Modules Then
                    scriptFolder = Ssh.Folders.ModuleScripts
                ElseIf sessionPhase = SessionPhase.Link Then
                    scriptFolder = Ssh.Folders.LinkScripts
                ElseIf sessionPhase = SessionPhase.End Then
                    scriptFolder = Ssh.Folders.EndScripts
                End If

                Dim scriptType As String = "Stroke"
                Dim scripts As List(Of ScriptMetaData) = myScriptAccessor.GetAllScripts(mySettingsAccessor.DommePersonality, IIf(sessionPhase = SessionPhase.Modules, "Modules", "Stroke"), sessionPhase, True)

                For i As Integer = 0 To target.Items.Count - 1
                    Dim item = target.Items(i)
                    Dim scriptMetaData = scripts.First(Function(smd) smd.Name = item.ToString())
                    Dim requirements = GetScriptRequirements(scriptMetaData)
                    target.SetItemChecked(i, requirements.Item1)
                Next
            End If

            saveAction()
        Finally
            target.EndUpdate()
            target.Focus()
        End Try

        SelectAvailableScriptsButton.Enabled = True
        SelectNoScriptsButton.Enabled = True
        SelectAllScriptsButton.Enabled = True
    End Sub

#End Region ' Scripts

#Region "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Apps ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"

#Region "----------------------------------------- Glitter ----------------------------------------------"

    Private Sub GlitterAV_Click(sender As Object, e As EventArgs) Handles GlitterAV.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            GlitterAV.Image = Image.FromFile(OpenFileDialog1.FileName)
            My.Settings.GlitterAV = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub GlitterAV1_Click(sender As Object, e As EventArgs) Handles GlitterAV1.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            GlitterAV1.Image = Image.FromFile(OpenFileDialog1.FileName)
            My.Settings.GlitterAV1 = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub GlitterAV2_Click(sender As Object, e As EventArgs) Handles GlitterAV2.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            GlitterAV2.Image = Image.FromFile(OpenFileDialog1.FileName)
            My.Settings.GlitterAV2 = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub GlitterAV3_Click(sender As Object, e As EventArgs) Handles GlitterAV3.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            GlitterAV3.Image = Image.FromFile(OpenFileDialog1.FileName)
            My.Settings.GlitterAV3 = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub BTNDomImageDir_Click(sender As Object, e As EventArgs) Handles BTNDomImageDir.Click
        Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            My.Settings.DomImageDir = FolderBrowserDialog1.SelectedPath
            My.Application.Session.SlideshowMain = New ContactData(ContactType.Domme)
        End If
    End Sub

    Private Sub BtnContact1ImageDir_Click(sender As Object, e As EventArgs) Handles BtnContact1ImageDir.Click
        Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            My.Settings.Contact1ImageDir = FolderBrowserDialog1.SelectedPath
            My.Application.Session.SlideshowContact1 = New ContactData(ContactType.Contact1)
        End If
    End Sub

    Private Sub BtnContact2ImageDir_Click(sender As Object, e As EventArgs) Handles BtnContact2ImageDir.Click
        Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            My.Settings.Contact2ImageDir = FolderBrowserDialog1.SelectedPath
            My.Application.Session.SlideshowContact2 = New ContactData(ContactType.Contact2)
        End If
    End Sub

    Private Sub BtnContact3ImageDir_Click(sender As Object, e As EventArgs) Handles BtnContact3ImageDir.Click
        Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            My.Settings.Contact3ImageDir = FolderBrowserDialog1.SelectedPath
            My.Application.Session.SlideshowContact3 = New ContactData(ContactType.Contact3)
        End If
    End Sub

    Private Sub BTNGlitterD_Click(sender As Object, e As EventArgs) Handles BTNGlitterD.Click
        SetColor(LBLGlitterNCDomme)
    End Sub

    Private Sub BTNGlitter1_Click(sender As Object, e As EventArgs) Handles BTNGlitter1.Click
        SetColor(LBLGlitterNC1)
    End Sub

    Private Sub BTNGlitter2_click(sender As Object, e As EventArgs) Handles BTNGlitter2.Click
        SetColor(LBLGlitterNC2)
    End Sub

    Private Sub BTNGlitter3_Click(sender As Object, e As EventArgs) Handles BTNGlitter3.Click
        SetColor(LBLGlitterNC3)
    End Sub

    Private Sub CBGlitterFeed_CheckedChanged(sender As Object, e As EventArgs) Handles CBGlitterFeed.Click, CBGlitterFeedScripts.Click, CBGlitterFeedOff.Click
        If MainWindow.FormLoading Then
            Return
        End If
        ' In order to prevent wrong values, we have to change the DataSourceUpdateMode.
        ' Since the Designer will reset this value, we have to undo this changes.
        For Each ob As RadioButton In {CBGlitterFeed, CBGlitterFeedOff, CBGlitterFeedScripts}
            ob.DataBindings("Checked").DataSourceUpdateMode = DataSourceUpdateMode.OnValidation
        Next

        ' Set the desired Value manually - Didn't know it is that much of a problem to databind RadioButtons.
        ' This Solution ensures the ui to display the current value, whenever and whatever thread changed in and
        ' it saves correctly. The only issue could be, when setting a value, while forgetting to disable the others.
        Dim checked As Boolean = CType(sender, RadioButton).Checked
        My.Settings.CBGlitterFeed = If(sender Is CBGlitterFeed, checked, False)
        My.Settings.CBGlitterFeedOff = If(sender Is CBGlitterFeedOff, checked, False)
        My.Settings.CBGlitterFeedScripts = If(sender Is CBGlitterFeedScripts, checked, False)
    End Sub

    Private Sub BtnContact1ImageDirClear_Click(sender As Object, e As EventArgs) Handles BtnContact1ImageDirClear.Click
        My.Settings.ResetField(TbxContact1ImageDir, "Text")
        My.Application.Session.SlideshowContact1 = New ContactData()
    End Sub

    Private Sub BtnContact2ImageDirClear_Click(sender As Object, e As EventArgs) Handles BtnContact2ImageDirClear.Click
        My.Settings.ResetField(TbxContact2ImageDir, "Text")
        My.Application.Session.SlideshowContact2 = New ContactData()
    End Sub

    Private Sub BtnContact3ImageDirClear_Click(sender As Object, e As EventArgs) Handles BtnContact3ImageDirClear.Click
        My.Settings.ResetField(TbxContact3ImageDir, "Text")
        My.Application.Session.SlideshowContact3 = New ContactData()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim saveSettingsDialog = New SaveFileDialog()
        saveSettingsDialog.Title = "Select a location to save current Glitter settings"
        saveSettingsDialog.InitialDirectory = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\System\"

        saveSettingsDialog.FileName = MainWindow.dompersonalitycombobox.Text & " Glitter Settings"

        If saveSettingsDialog.ShowDialog() = DialogResult.OK Then
            Dim settingsPath As String = saveSettingsDialog.FileName
            Dim settingsList As New List(Of String)
            settingsList.Clear()


            If My.Settings.CBGlitterFeed Then settingsList.Add("Glitter Feed: On")
            If My.Settings.CBGlitterFeedScripts Then settingsList.Add("Glitter Feed: Scripts")
            If My.Settings.CBGlitterFeedOff Then settingsList.Add("Glitter Feed: Off")

            settingsList.Add("Short Name: " & My.Settings.GlitterSN)
            settingsList.Add("Domme Color: " & My.Settings.GlitterNCDommeColor.ToArgb.ToString)
            settingsList.Add("Tease: " & My.Settings.CBTease)
            settingsList.Add("Egotist: " & My.Settings.CBEgotist)
            settingsList.Add("Trivia: " & My.Settings.CBTrivia)
            settingsList.Add("Daily: " & My.Settings.CBDaily)
            settingsList.Add("Custom 1: " & My.Settings.CBCustom1)
            settingsList.Add("Custom 2: " & My.Settings.CBCustom2)
            settingsList.Add("Domme Post Frequency: " & My.Settings.GlitterDSlider)

            settingsList.Add("Contact 1 Enabled: " & My.Settings.CBGlitter1)
            settingsList.Add("Contact 1 Name: " & My.Settings.Glitter1)
            settingsList.Add("Contact 1 Color: " & My.Settings.GlitterNC1Color.ToArgb.ToString)
            settingsList.Add("Contact 1 Image Directory: " & My.Settings.Contact1ImageDir)
            settingsList.Add("Contact 1 Post Frequency: " & My.Settings.Glitter1Slider)

            settingsList.Add("Contact 2 Enabled: " & My.Settings.CBGlitter2)
            settingsList.Add("Contact 2 Name: " & My.Settings.Glitter2)
            settingsList.Add("Contact 2 Color: " & My.Settings.GlitterNC2Color.ToArgb.ToString)
            settingsList.Add("Contact 2 Image Directory: " & My.Settings.Contact2ImageDir)
            settingsList.Add("Contact 2 Post Frequency: " & My.Settings.Glitter2Slider)

            settingsList.Add("Contact 3 Enabled: " & My.Settings.CBGlitter3)
            settingsList.Add("Contact 3 Name: " & My.Settings.Glitter3)
            settingsList.Add("Contact 3 Color: " & My.Settings.GlitterNC3Color.ToArgb.ToString)
            settingsList.Add("Contact 3 Image Directory: " & My.Settings.Contact3ImageDir)
            settingsList.Add("Contact 3 Post Frequency: " & My.Settings.Glitter3Slider)

            settingsList.Add("Domme AV: " & My.Settings.GlitterAV)
            settingsList.Add("Contact 1 AV: " & My.Settings.GlitterAV1)
            settingsList.Add("Contact 2 AV: " & My.Settings.GlitterAV2)
            settingsList.Add("Contact 3 AV: " & My.Settings.GlitterAV3)



            Dim SettingsString As String = ""

            For i As Integer = 0 To settingsList.Count - 1
                SettingsString = SettingsString & settingsList(i)
                If i <> settingsList.Count - 1 Then SettingsString = SettingsString & Environment.NewLine
            Next

            My.Computer.FileSystem.WriteAllText(settingsPath, SettingsString, False)
        End If


    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        'ISSUE: Loading a corrupted textfile results in half loaded Glitter settings.
        OpenSettingsDialog.Title = "Select a Glitter settings file"
        OpenSettingsDialog.InitialDirectory = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\System\"

        If OpenSettingsDialog.ShowDialog() = DialogResult.OK Then

            Dim SettingsList As New List(Of String)

            Try
                Dim SettingsReader As New StreamReader(OpenSettingsDialog.FileName)
                While SettingsReader.Peek <> -1
                    SettingsList.Add(SettingsReader.ReadLine())
                End While
                SettingsReader.Close()
                SettingsReader.Dispose()
            Catch ex As Exception
                MessageBox.Show(Me, "This file could not be opened!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End Try

            Try

                Dim CheckState As String = SettingsList(0).Replace("Glitter Feed: ", "")
                If CheckState = "On" Then My.Settings.CBGlitterFeed = True
                If CheckState = "Scripts" Then My.Settings.CBGlitterFeedScripts = True
                If CheckState = "Off" Then My.Settings.CBGlitterFeedOff = True

                My.Settings.GlitterSN = SettingsList(1).Replace("Short Name: ", "")

                Dim GlitterColor As Color = Color.FromArgb(SettingsList(2).Replace("Domme Color: ", ""))
                My.Settings.GlitterNCDommeColor = GlitterColor

                My.Settings.CBTease = SettingsList(3).Replace("Tease: ", "")
                My.Settings.CBEgotist = SettingsList(4).Replace("Egotist: ", "")
                My.Settings.CBTrivia = SettingsList(5).Replace("Trivia: ", "")
                My.Settings.CBDaily = SettingsList(6).Replace("Daily: ", "")
                My.Settings.CBCustom1 = SettingsList(7).Replace("Custom 1: ", "")
                My.Settings.CBCustom2 = SettingsList(8).Replace("Custom 2: ", "")
                My.Settings.GlitterDSlider = CInt(SettingsList(9).Replace("Domme Post Frequency: ", ""))


                My.Settings.CBGlitter1 = SettingsList(10).Replace("Contact 1 Enabled: ", "")
                My.Settings.Glitter1 = SettingsList(11).Replace("Contact 1 Name: ", "")
                GlitterColor = Color.FromArgb(SettingsList(12).Replace("Contact 1 Color: ", ""))
                My.Settings.GlitterNC1Color = GlitterColor
                My.Settings.Contact1ImageDir = SettingsList(13).Replace("Contact 1 Image Directory: ", "")
                My.Settings.Glitter1Slider = SettingsList(14).Replace("Contact 1 Post Frequency: ", "")

                My.Settings.CBGlitter2 = SettingsList(15).Replace("Contact 2 Enabled: ", "")
                My.Settings.Glitter2 = SettingsList(16).Replace("Contact 2 Name: ", "")
                GlitterColor = Color.FromArgb(SettingsList(17).Replace("Contact 2 Color: ", ""))
                My.Settings.GlitterNC2Color = GlitterColor
                My.Settings.Contact2ImageDir = SettingsList(18).Replace("Contact 2 Image Directory: ", "")
                My.Settings.Glitter2Slider = SettingsList(19).Replace("Contact 2 Post Frequency: ", "")

                My.Settings.CBGlitter3 = SettingsList(20).Replace("Contact 3 Enabled: ", "")
                My.Settings.Glitter3 = SettingsList(21).Replace("Contact 3 Name: ", "")
                GlitterColor = Color.FromArgb(SettingsList(22).Replace("Contact 3 Color: ", ""))
                My.Settings.GlitterNC3Color = GlitterColor
                My.Settings.Contact3ImageDir = SettingsList(23).Replace("Contact 3 Image Directory: ", "")
                My.Settings.Glitter3Slider = SettingsList(24).Replace("Contact 3 Post Frequency: ", "")

                Try
                    GlitterAV.Image = Image.FromFile(SettingsList(25).Replace("Domme AV: ", ""))
                    My.Settings.GlitterAV = SettingsList(25).Replace("Domme AV: ", "")
                Catch
                End Try

                Try
                    GlitterAV1.Image = Image.FromFile(SettingsList(26).Replace("Contact 1 AV: ", ""))
                    My.Settings.GlitterAV1 = SettingsList(26).Replace("Contact 1 AV: ", "")
                Catch
                End Try

                Try
                    GlitterAV2.Image = Image.FromFile(SettingsList(27).Replace("Contact 2 AV: ", ""))
                    My.Settings.GlitterAV2 = SettingsList(27).Replace("Contact 2 AV: ", "")
                Catch
                End Try

                Try
                    GlitterAV3.Image = Image.FromFile(SettingsList(28).Replace("Contact 3 AV: ", ""))
                    My.Settings.GlitterAV3 = SettingsList(28).Replace("Contact 3 AV: ", "")
                Catch
                End Try


            Catch
                MessageBox.Show(Me, "This Glitter settings file is invalid or has been edited incorrectly!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End Try

        End If
    End Sub

#End Region ' Glitter

#Region "----------------------------------------- Games ------------------------------------------------"

    ''' =========================================================================================================
    ''' <summary>
    ''' Checks if all the conditions for card games are met.
    ''' </summary>
    ''' <returns>Returns true if all conditions are met.</returns>
    Friend Function CardGameCheck() As Boolean
        Dim rtnVal As Boolean = True

        For Each tmpPicBox As PictureBox In New List(Of PictureBox) From
        {BP1, BP2, BP3, BP4, BP5, BP6, SP1, SP2, SP3, SP4, SP5, SP6,
        GP1, GP2, GP3, GP4, GP5, GP6, CardBack}

            If tmpPicBox.DataBindings.Item("ImageLocation") Is Nothing Then
                Throw New Exception("There is no databinding set on """ & tmpPicBox.Name &
                                    """'s image location. Set the databinding and recompile!")
            End If

            tmpPicBox.AllowDrop = True

            If tmpPicBox.ImageLocation = My.Settings.GetDefault(tmpPicBox, "ImageLocation") Then
                rtnVal = False
            ElseIf File.Exists(tmpPicBox.ImageLocation) Then
                tmpPicBox.Image = Image.FromFile(tmpPicBox.ImageLocation)
            Else
                rtnVal = False
                My.Settings.ResetField(tmpPicBox, "ImageLocation")
            End If
        Next

        For Each tmpTbx As TextBox In New List(Of TextBox) From
                {BN1, BN2, BN3, BN4, BN5, BN6,
                SN1, SN2, SN3, SN4, SN5, SN6,
                GN1, GN2, GN3, GN4, GN5, GN6}

            If tmpTbx.DataBindings.Item("Text") Is Nothing Then
                Throw New Exception("There is no databinding set on """ & tmpTbx.Name &
                                    """'s text property. Set the databinding and recompile!")
            End If

            If tmpTbx.Text.Length < 1 Then My.Settings.ResetField(tmpTbx, "Text")
        Next
        Return rtnVal
    End Function

    ''' =========================================================================================================
    ''' <summary>
    ''' Sets a Cardimage for the given picturebox.
    ''' </summary>
    ''' <param name="sender">The PictureBox to set the Image.</param>
    ''' <param name="filepath">The image filepath to set.</param>
    ''' <remarks>The PictureBox must have a databinding between the 
    ''' ImageLoaction-Property and My.Settings.</remarks>
    Private Sub CardImageSet(sender As PictureBox, filepath As String)
        Try
            Dim target As PictureBox = CType(sender, PictureBox)
            Dim savePath As String = String.Format("{0}\Images\Cards\Card{1}.bmp",
                                                   Application.StartupPath,
                                                   target.Name)

            savePath = savePath.Replace("CardCard", "Card")

            ' Close Games form and end file access.
            If FrmCardList.Visible Then FrmCardList.Dispose()
            FrmCardList.ClearAllCards()

            ' Release all ressources.
            If target.Image IsNot Nothing Then target.Image.Dispose()
            target.Image = Nothing
            target.ImageLocation = ""

            GC.Collect()
            Application.DoEvents()

            ' Check if the file is locked. Sometimes the GC needs some time
            ' to finally release the file lock after the image was disposed.
            Dim retrycounter As Integer = 5
            Do While IsFileLocked(savePath) AndAlso retrycounter > 0
                retrycounter -= 1
                GC.Collect()
                Application.DoEvents()
            Loop

            If retrycounter <= 0 Then Throw New IO.IOException(
                String.Format("The file """"{0}"" is already in use."), savePath)

            ' Check if the Databinding is properly set.
            If target.DataBindings.Item("ImageLocation") Is Nothing Then
                Throw New Exception("There is no databinding set on """ & target.Name &
                                    """'s image location. Set the databinding and recompile!")
            End If

            ' Set the resized image as picturebox image and write it to disk
            target.Image = ResizeImage(filepath, New Size(138, 179))
            target.Image.Save(savePath)

            ' Set the image Location-Property. Property has to be databound with My.Settings!
            target.ImageLocation = savePath

            ' Writing to databound Imagelocation doesn't update My.Settings!
            ' Now we write the value directly using the binding to get the My.Settings.Member to write to.
            My.Settings(target.DataBindings.Item("ImageLocation").BindingMemberInfo.BindingField) = savePath


            MainWindow.GamesToolStripMenuItem1.Enabled = CardGameCheck()
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Unable to set Card Image")
        End Try
    End Sub

    Private Sub CardPictureboxes_DragEnter(ByVal sender As Object, ByVal e As Windows.Forms.DragEventArgs) Handles _
                                        BP1.DragEnter, BP2.DragEnter, BP3.DragEnter, BP4.DragEnter, BP5.DragEnter, BP6.DragEnter,
                                        SP1.DragEnter, SP2.DragEnter, SP3.DragEnter, SP4.DragEnter, SP5.DragEnter, SP6.DragEnter,
                                        GP1.DragEnter, GP2.DragEnter, GP3.DragEnter, GP4.DragEnter, GP5.DragEnter, GP6.DragEnter,
                                        CardBack.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub CardPictureboxes_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) Handles _
                                        BP1.DragDrop, BP2.DragDrop, BP3.DragDrop, BP4.DragDrop, BP5.DragDrop, BP6.DragDrop,
                                        SP1.DragDrop, SP2.DragDrop, SP3.DragDrop, SP4.DragDrop, SP5.DragDrop, SP6.DragDrop,
                                        GP1.DragDrop, GP2.DragDrop, GP3.DragDrop, GP4.DragDrop, GP5.DragDrop, GP6.DragDrop,
                                        CardBack.DragDrop
        CardImageSet(CType(sender, PictureBox), CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0))
    End Sub

    Private Sub CardPictureboxes_Click(sender As Object, e As EventArgs) Handles _
                                        BP1.Click, BP2.Click, BP3.Click, BP4.Click, BP5.Click, BP6.Click,
                                        SP1.Click, SP2.Click, SP3.Click, SP4.Click, SP5.Click, SP6.Click,
                                        GP1.Click, GP2.Click, GP3.Click, GP4.Click, GP5.Click, GP6.Click,
                                        CardBack.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            CardImageSet(CType(sender, PictureBox), OpenFileDialog1.FileName)
        End If
    End Sub

    ''' <summary>
    ''' Resets the databinding source of a TextBox to its initial value, if there is no Text entered.
    ''' </summary>
    Private Sub CardTextboxes_Validating(sender As Object, e As CancelEventArgs) Handles _
                                        BN1.Validating, BN2.Validating, BN3.Validating, BN4.Validating, BN5.Validating, BN6.Validating,
                                        SN1.Validating, SN2.Validating, SN3.Validating, SN4.Validating, SN5.Validating, SN6.Validating,
                                        GN1.Validating, GN2.Validating, GN3.Validating, GN4.Validating, GN5.Validating, GN6.Validating
        Dim tmpTbx As TextBox = CType(sender, TextBox)

        If tmpTbx.Text = "" AndAlso tmpTbx.DataBindings("Text") IsNot Nothing Then
            My.Settings.ResetField(tmpTbx, "Text")
            e.Cancel = False
        End If
    End Sub

#End Region ' Games

#End Region ' Apps

#Region "-------------------------------------- URL Files -----------------------------------------------"

    Private Sub Button57_Click(sender As Object, e As EventArgs) Handles BTNWIBrowse.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            TBWIDirectory.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub CBWISaveToDisk_CheckedChanged(sender As Object, e As EventArgs) Handles CBWISaveToDisk.CheckedChanged

        If CBWISaveToDisk.Checked Then
            If Not Directory.Exists(TBWIDirectory.Text) Then
                MessageBox.Show(Me, "Please enter or browse for a valid Saved Image Directory first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                CBWISaveToDisk.Checked = False
            End If
        End If
    End Sub

    Private Sub BTNWIAddandContinue_Click(sender As Object, e As EventArgs) Handles BTNWIAddandContinue.Click
        ApproveImage = 1
    End Sub

    Private Sub BTNWIContinue_Click(sender As Object, e As EventArgs) Handles BTNWIContinue.Click
        ApproveImage = 2
    End Sub

    Private Sub BTNCancel_Click(sender As Object, e As EventArgs) Handles BTNWICancel.Click
        If BWURLFiles.IsBusy Then BWURLFiles.CancelAsync()
    End Sub

    Private Sub BTNWIRemove_Click(sender As Object, e As EventArgs) Handles BTNWIRemove.Click


        WebImageLines.Remove(WebImageLines(WebImageLine))


        If WebImageLine = WebImageLines.Count Then WebImageLine = 0
        '
        'Else
        'WebImageLine += 1
        'End If

        Try
            WebPictureBox.Image.Dispose()
        Catch
        End Try

        WebPictureBox.Image = Nothing
        GC.Collect()

        WebPictureBox.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(WebImageLines(WebImageLine))))

        Debug.Print(WebImageLines(WebImageLine))

        My.Computer.FileSystem.DeleteFile(WebImagePath)

        If File.Exists(WebImagePath) Then
            Debug.Print("File Exists")
        Else
            Debug.Print("Nope")
        End If

        My.Computer.FileSystem.WriteAllText(WebImagePath, String.Join(Environment.NewLine, WebImageLines), False)

    End Sub

    Private Sub BTNWILiked_Click(sender As Object, e As EventArgs) Handles BTNWILiked.Click


        If File.Exists(Application.StartupPath & "\Images\System\LikedImageURLs.txt") Then
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\LikedImageURLs.txt", Environment.NewLine & WebImageLines(WebImageLine), True)
        Else
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\LikedImageURLs.txt", WebImageLines(WebImageLine), True)
        End If


    End Sub

    Private Sub BTNWIDisliked_Click(sender As Object, e As EventArgs) Handles BTNWIDisliked.Click

        If File.Exists(Application.StartupPath & "\Images\System\DislikedImageURLs.txt") Then
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\DislikedImageURLs.txt", Environment.NewLine & WebImageLines(WebImageLine), True)
        Else
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Images\System\DislikedImageURLs.txt", WebImageLines(WebImageLine), True)
        End If

    End Sub

    Private Sub WebPictureBox_MouseWheel(sender As Object, e As Windows.Forms.MouseEventArgs) Handles WebPictureBox.MouseWheel

        Select Case e.Delta
            Case -120 'Scrolling down
                WebImageLine += 1

                If WebImageLine > WebImageLineTotal - 1 Then
                    WebImageLine = WebImageLineTotal
                    MsgBox("No more images to display!", , "Warning!")
                    Return
                End If

                Try
                    WebPictureBox.Image.Dispose()
                Catch
                End Try
                WebPictureBox.Image = Nothing
                GC.Collect()

                WebPictureBox.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(WebImageLines(WebImageLine))))
                LBLWebImageCount.Text = WebImageLine + 1 & "/" & WebImageLineTotal
            Case 120 'Scrolling up
                WebImageLine -= 1

                If WebImageLine < 0 Then
                    WebImageLine = 0
                    MsgBox("No more images to display!", , "Warning!")
                    Return
                End If

                Try
                    WebPictureBox.Image.Dispose()
                Catch
                End Try
                WebPictureBox.Image = Nothing
                GC.Collect()
                WebPictureBox.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(WebImageLines(WebImageLine))))
                LBLWebImageCount.Text = WebImageLine + 1 & "/" & WebImageLineTotal
        End Select


    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles WebPictureBox.MouseEnter
        WebPictureBox.Focus()
    End Sub


    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles BTNWIOpenURL.Click

        WebImageFileDialog.InitialDirectory = Application.StartupPath & "\Images\System\URL Files"

        If (WebImageFileDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then

            WebImageFile = New IO.StreamReader(WebImageFileDialog.FileName)
            WebImagePath = WebImageFileDialog.FileName

            WebImageLines.Clear()

            WebImageLine = 0
            WebImageLineTotal = 0

            While WebImageFile.Peek <> -1
                WebImageLineTotal += 1
                WebImageLines.Add(WebImageFile.ReadLine())
            End While

            Try
                WebPictureBox.Image.Dispose()
            Catch
            End Try
            WebPictureBox.Image = Nothing
            GC.Collect()

            Try
                WebPictureBox.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(WebImageLines(0))))
            Catch
                MessageBox.Show(Me, "Failed to load URL File image!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

            WebImageFile.Close()
            WebImageFile.Dispose()

            LBLWebImageCount.Text = WebImageLine + 1 & "/" & WebImageLineTotal


            BTNWINext.Enabled = True
            BTNWIPrevious.Enabled = True
            BTNWIRemove.Enabled = True
            BTNWILiked.Enabled = True
            BTNWIDisliked.Enabled = True
            BTNWISave.Enabled = True


        End If




    End Sub


    Private Sub Button35_Click_2(sender As Object, e As EventArgs) Handles BTNWINext.Click

TryNextImage:

        WebImageLine += 1

        If WebImageLine > WebImageLineTotal - 1 Then
            WebImageLine = WebImageLineTotal
            MsgBox("No more images to display!", , "Warning!")
            Return
        End If

        Try
            WebPictureBox.Image.Dispose()
        Catch
        End Try

        WebPictureBox.Image = Nothing
        GC.Collect()
        Try
            WebPictureBox.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(WebImageLines(WebImageLine))))
            LBLWebImageCount.Text = WebImageLine + 1 & "/" & WebImageLineTotal
        Catch ex As Exception
            GoTo TryNextImage
        End Try



    End Sub

    Private Sub Button18_Click_2(sender As Object, e As EventArgs) Handles BTNWIPrevious.Click

trypreviousimage:

        WebImageLine -= 1

        If WebImageLine < 0 Then
            WebImageLine = 0
            MsgBox("No more images to display!", , "Warning!")
            Return
        End If

        Try
            WebPictureBox.Image.Dispose()
        Catch
        End Try

        WebPictureBox.Image = Nothing
        GC.Collect()
        Try
            WebPictureBox.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(WebImageLines(WebImageLine))))
            LBLWebImageCount.Text = WebImageLine + 1 & "/" & WebImageLineTotal
        Catch ex As Exception
            GoTo trypreviousimage
        End Try

    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles BTNWISave.Click

        If WebPictureBox.Image Is Nothing Then
            MsgBox("Nothing to save!", , "Error!")
            Return
        End If


        SaveFileDialog1.Filter = "jpegs|*.jpg|gifs|*.gif|pngs|*.png|Bitmaps|*.bmp"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.RestoreDirectory = True


        Try

            WebImage = WebImageLines(WebImageLine)

            Dim DirSplit As String() = WebImage.Split("/")
            WebImage = DirSplit(DirSplit.Length - 1)

            ' ### Clean Code
            'Do Until Not Form1.WebImage.Contains("/")
            'Form1.WebImage = Form1.WebImage.Remove(0, 1)
            'Loop

            SaveFileDialog1.FileName = WebImage

        Catch ex As Exception

            SaveFileDialog1.FileName = "image.jpg"

        End Try





        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then

            WebPictureBox.Image.Save(SaveFileDialog1.FileName)

        End If

    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles BTNWICreateURL.Click,
                                                                                       BTNMaintenanceRefresh.Click,
                                                                                       BTNMaintenanceRebuild.Click
        ' Group Buttons by inital-State.
        Dim __PreEnabled As New List(Of Control) From
            {BTNWIOpenURL, BTNWICreateURL, BTNMaintenanceRefresh,
            BTNMaintenanceRebuild, BTNMaintenanceScripts}
        Dim __PreDisabled As New List(Of Control) From
            {BTNWICancel, BTNMaintenanceCancel}

        Try
            ' Set their new State, so the User can't disturb.
            __PreEnabled.ForEach(Sub(x) x.Enabled = False)
            __PreDisabled.ForEach(Sub(x) x.Enabled = True)

            Select Case sender.name
                Case BTNWICreateURL.Name
                    '**************************************************************************************************************
                    '                                                Create URL-File
                    '**************************************************************************************************************
                    Dim __BtnLocalURL As New List(Of Control) From {
                        BTNWINext, BTNWIPrevious, BTNWIRemove, BTNWILiked, BTNWIDisliked, BTNWISave}
                    Try
                        ' Disable Buttons for Opening-URL-Files
                        __BtnLocalURL.ForEach(Sub(x) x.Enabled = False)

                        ' Run Backgroundworker
                        Dim __tmpResult As URL_File_BGW.CreateUrlFileResult = BWURLFiles.CreateURLFileAsync()

                        ' Activate the created URL-File
                        URL_File_Set(__tmpResult.Filename)

                        ' UserInfo
                        If __tmpResult._Error Is Nothing Then
                            MsgBox("URL File has been saved to:" &
                               vbCrLf & vbCrLf & Application.StartupPath & "\Images\System\URL Files\" & __tmpResult.Filename & ".txt" &
                               vbCrLf & vbCrLf & "Use the ""Open URL File"" button to load and view your collections.",  , "Success!")
                        Else
                            MsgBox("It is encountered an error during URL-File-Creation." & vbCrLf &
                                   __tmpResult._Error.Message & vbCrLf &
                                       "URL File has been saved to:" &
                                    vbCrLf & vbCrLf & Application.StartupPath & "\Images\System\URL Files\" & __tmpResult.Filename & ".txt" &
                                    vbCrLf & vbCrLf & "Use the ""Open URL File"" button to load and view your collections.",  , "Successful despite errors!")
                        End If
                    Catch
                        '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                        '                                            All Errors
                        '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                        Throw
                    Finally
                        '⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑ Finally ⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑
                        __BtnLocalURL.ForEach(Sub(x) x.Enabled = True)
                    End Try
                Case BTNMaintenanceRefresh.Name
                    '**************************************************************************************************************
                    '                                             Refresh URL-Files
                    '**************************************************************************************************************
                    Try

                        ' Run Backgroundworker
                        Dim __tmpResult As URL_File_BGW.MaintainUrlResult = BWURLFiles.RefreshURLFilesAsync()

                        ' Activate the URL-Files
                        __tmpResult.MaintainedUrlFiles.ForEach(AddressOf URL_File_Set)

                        If __tmpResult.Cancelled Then
                            MessageBox.Show(Me, "Refreshing URL-File has been aborted after " & __tmpResult.MaintainedUrlFiles.Count & " URL-Files." &
                                            vbCrLf & __tmpResult.ModifiedLinkCount & " new URLs have been added.",
                                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf __tmpResult.ErrorText.Capacity > 0 Then
                            MessageBox.Show(Me, "URL Files have been refreshed with errors!" &
                                            vbCrLf & vbCrLf & __tmpResult.ModifiedLinkCount & " new URLs have been added." &
                                            vbCrLf & vbCrLf & String.Join(vbCrLf, __tmpResult.ErrorText),
                                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show(Me, "All URL Files have been refreshed!" &
                                            vbCrLf & vbCrLf & __tmpResult.ModifiedLinkCount & " new URLs have been added.",
                                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch
                        '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                        '                                            All Errors
                        '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                        Throw
                    Finally
                        '⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑ Finally ⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑
                        LBLMaintenance.Text = String.Empty
                        PBCurrent.Value = 0
                        PBMaintenance.Value = 0
                    End Try
                Case BTNMaintenanceRebuild.Name
                    '**************************************************************************************************************
                    '                                             Rebuild URL-Files
                    '**************************************************************************************************************
                    Try
                        ' Run Backgroundworker
                        Dim __tmpResult As URL_File_BGW.MaintainUrlResult = BWURLFiles.RebuildURLFilesAsync()

                        ' Activate the URL-Files
                        __tmpResult.MaintainedUrlFiles.ForEach(AddressOf URL_File_Set)

                        If __tmpResult.Cancelled Then
                            MessageBox.Show(Me, "Rebuilding URL-File has been aborted after " & __tmpResult.MaintainedUrlFiles.Count & " URL-Files." &
                                            vbCrLf & __tmpResult.ModifiedLinkCount & " dead URLs have been removed.",
                                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf __tmpResult.ErrorText.Capacity > 0 Then
                            MessageBox.Show(Me, "URL Files have been rebuilded with errors!" &
                                            vbCrLf & vbCrLf & __tmpResult.ModifiedLinkCount & " dead URLs have been removed." &
                                            vbCrLf & vbCrLf & __tmpResult.LinkCountTotal & " URLs in total." &
                                            vbCrLf & vbCrLf & String.Join(vbCrLf, __tmpResult.ErrorText),
                                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show(Me, "All URL Files have been rebuilded!" &
                                            vbCrLf & vbCrLf & __tmpResult.ModifiedLinkCount & " dead URLs have been removed." &
                                            vbCrLf & vbCrLf & __tmpResult.LinkCountTotal & " URLs in total.",
                                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch
                        '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                        '                                            All Errors
                        '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                        Throw
                    Finally
                        '⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑ Finally ⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑
                        LBLMaintenance.Text = String.Empty
                        PBCurrent.Value = 0
                        PBMaintenance.Value = 0
                    End Try
            End Select
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            If ex.InnerException IsNot Nothing Then
                ' If an Error ocurred in the other Thread, initial Exception is innner one.
                MsgBox(ex.InnerException.Message, MsgBoxStyle.Critical, "Error Creating URL-File")
            Else
                ' Otherwise show it normal.
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Creating URL-File")
            End If
        Finally
            '⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑ Finally ⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑⚑
            ' Restore the initial State of the Buttons
            __PreEnabled.ForEach(Sub(x) x.Enabled = True)
            __PreDisabled.ForEach(Sub(x) x.Enabled = False)
        End Try
    End Sub

#Region "-------------------------------------- Backgroundworker URL Files --------------------------------------"

    ''' =========================================================================================================
    ''' <summary>
    ''' This Event is used, to gather Variables, for the BackgroundThread, the User can change during runtime.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BWURLFiles_URLCreate_User_Interactions(ByVal sender As URL_File_BGW, ByRef e As URL_File_BGW.UserActions) Handles BWURLFiles.URL_FileCreate_UserInteractions
        If Me.InvokeRequired Then
            Dim Callbak As New URL_File_BGW.URL_FileCreate_UserInteractions_Delegate(AddressOf BWURLFiles_URLCreate_User_Interactions)
            Me.Invoke(Callbak, sender, e)
        Else
            e.ReviewImages = CBWIReview.Checked
            e.ApproveImage = ApproveImage
            e.SaveImages = CBWISaveToDisk.Checked
            e.ImgSaveDir = TBWIDirectory.Text
        End If
    End Sub

    ''' =========================================================================================================
    ''' <summary>
    ''' This Event will be triggered, when the Working Stage of the BGW changes
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub BWURLFiles_ProgressChanged(ByVal Sender As Object, ByRef e As URL_File_BGW.URL_File_ProgressChangedEventArgs) Handles BWURLFiles.URL_File_ProgressChanged
        If Me.InvokeRequired Then
            ' Beware: Event is fired in Worker Thread, so you need to do a Function Callback.
            Dim CallBack As New URL_File_BGW.URL_File_ProgressChanged_Delegate(AddressOf BWURLFiles_ProgressChanged)
            Me.Invoke(CallBack, Sender, e)
        Else
            ' Reset remanent Marker for Image Approval
            If ApproveImage <> 0 Then ApproveImage = 0
            Select Case e.CurrentTask
                Case URL_File_Tasks.CreateURLFile
                    '===============================================================================
                    '                           Create URL-File
                    '===============================================================================
                    Select Case e.ActStage
                        ' ------------------------ Image Approval -------------------------------
                        Case WorkingStages.ImageApproval
                            If e.ImageToReview IsNot Nothing Then
                                ' Dispose old Image & Set new Image
                                Try : WebPictureBox.Image.Dispose() : Catch : End Try
                                WebPictureBox.Image = e.ImageToReview
                                ' Enabled UI Elements
                                BTNWIContinue.Enabled = True
                                BTNWIAddandContinue.Enabled = True
                            End If
                        Case WorkingStages.Writing_File
                            ' ---------------------- Write to File -------------------------------
                            'State info to User
                            LBLWebImageCount.Text = "Writing"
                            ' At this state no cnancel possible
                            BTNWICancel.Enabled = False
                            WebPictureBox.Image = Nothing
                        Case Else
                            ' ---------------------- Everthing else ------------------------------
                            ' Refresh Progressbars
                            WebImageProgressBar.Maximum = e.BlogPageTotal + 1
                            WebImageProgressBar.Value = e.BlogPage
                            ' Disable Image Approval-UI
                            BTNWIContinue.Enabled = False
                            BTNWIAddandContinue.Enabled = False
                            ' Inform User about BGW-State
                            LBLWebImageCount.Text = String.Format("{0}/{1} ({2})", e.BlogPage, e.BlogPageTotal, e.ImageCount)
                    End Select
                Case Else
                    '===============================================================================
                    '                           Refresh URL-File
                    '===============================================================================
                    LBLMaintenance.Text = e.InfoText
                    PBCurrent.Maximum = e.BlogPageTotal
                    PBCurrent.Value = e.BlogPage
                    PBMaintenance.Maximum = e.OverallProgressTotal
                    PBMaintenance.Value = e.OverallProgress
            End Select
        End If
    End Sub

#End Region

#End Region ' Url Files

#Region "--------------------------------------- Images -------------------------------------------------"

    Friend Shared Function Image_FolderCheck(ByVal directoryDescription As String,
                                             ByVal directoryPath As String,
                                             ByVal defaultPath As String,
                                             ByRef subDirectories As Boolean) As String
        Dim rtnPath As String

        ' Exit if default value.
        If directoryPath = defaultPath Then subDirectories = False : Return defaultPath

        ' check it if the directory exists.
        If Directory.Exists(directoryPath) Then rtnPath = directoryPath : GoTo checkFolder

        ' Tell User, the dir. wasn't found. Ask to search manually for the folder.
        If MessageBox.Show(ActiveForm,
                           "The directory """ & directoryPath & """ was not found." & vbCrLf & "Do you want to search for it?",
                           directoryDescription & " image directory not found.",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) <> DialogResult.Yes Then
set_default:
            Return defaultPath
        Else
            '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            '								Set new Folder
            '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
set_newFolder:
            ' Find the first available parent-directory. 
            ' This way the user hasn't to browse through his hole IO-System.
            Dim __tmp_dir As String = directoryPath
            Do Until Directory.Exists(__tmp_dir) Or __tmp_dir Is Nothing
                __tmp_dir = Path.GetDirectoryName(__tmp_dir)
            Loop

            ' Initialize new Dialog-Form
            Dim FolSel As New FolderBrowserDialog With {.SelectedPath = __tmp_dir,
                                                            .Description = "Select " & directoryDescription & " image folder."}
            ' Display the Dialog -> Now the user has to set the new dir.
            If FolSel.ShowDialog(ActiveForm) = DialogResult.OK Then
                rtnPath = FolSel.SelectedPath
            Else
                GoTo set_default
            End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            ' Set new Folder - End
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        End If ' END IF - Messagebox.

        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        '							   Check folder content
        '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
checkFolder:
        Dim count_top As Integer = myDirectory.GetFilesImages(rtnPath, SearchOption.TopDirectoryOnly).Count
        Dim count_all As Integer = myDirectory.GetFilesImages(rtnPath, SearchOption.AllDirectories).Count

        If count_top = 0 And count_all = 0 Then
            ' ================================= No images in folder ===============================
            If MessageBox.Show(ActiveForm,
               "The directory """ & directoryPath & """ doesn't contain images." & vbCrLf & "Do you want to set a new folder?",
               directoryDescription & " image folder empty",
               MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                GoTo set_newFolder
            Else
                GoTo set_default
            End If
        ElseIf count_top = 0 And count_all > count_top And subDirectories = False Then
            ' ======================== none in top, but in sub ->enable sub? ======================
            If MessageBox.Show(ActiveForm,
               "The directory """ & directoryPath & """ doesn't contain images, but it's " &
               "subdirectories. Do you want to include subdirectories? If you click no the " &
               "default value will be set.",
               directoryDescription & " image folder empty",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                subDirectories = True
                Return rtnPath
            Else
                GoTo set_default
            End If
        Else
            '================================= everything fine ====================================
            Return rtnPath
        End If
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        ' Check folder content - End
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
    End Function

    Private Function CheckFolderSettings(directoryDescription As String, directoryPath As String, defaultPath As String, includeSubDirectories As Boolean) As Tuple(Of Boolean, String)
        Dim rtnPath As String

        If directoryPath = defaultPath Then
            Return Tuple.Create(False, defaultPath)
        End If

        If Directory.Exists(directoryPath) Then
            rtnPath = directoryPath
        ElseIf MessageBox.Show(ActiveForm,
                           "The directory """ & directoryPath & """ was not found." & vbCrLf & "Do you want to search for it?",
                           directoryDescription & " image directory not found.",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) <> DialogResult.Yes Then
            Return Tuple.Create(includeSubDirectories, defaultPath)
        Else
setNewFolder:
            Dim parentDirectory As String = directoryPath
            Do Until Directory.Exists(parentDirectory) OrElse parentDirectory Is Nothing
                parentDirectory = Path.GetDirectoryName(parentDirectory)
            Loop

            Dim folderBrowser As New FolderBrowserDialog With {.SelectedPath = parentDirectory, .Description = "Select " & directoryDescription & " image folder."}
            If folderBrowser.ShowDialog(ActiveForm) = DialogResult.OK Then
                rtnPath = folderBrowser.SelectedPath
            Else
                Return Tuple.Create(False, defaultPath)
            End If
        End If

checkFolder:
        Dim count_top As Integer = myDirectory.GetFilesImages(rtnPath, SearchOption.TopDirectoryOnly).Count
        Dim count_all As Integer = myDirectory.GetFilesImages(rtnPath, SearchOption.AllDirectories).Count

        If count_top > 0 AndAlso count_all > 0 Then
            Return Tuple.Create(includeSubDirectories, rtnPath)
        End If
        If count_top = 0 AndAlso count_all = 0 Then
            If MessageBox.Show(ActiveForm,
                   "The directory """ & directoryPath & """ doesn't contain images." & Environment.NewLine & "Do you want to set a new folder?",
                   directoryDescription & " image folder empty",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                GoTo setNewFolder
            End If
        ElseIf count_top = 0 AndAlso count_all > count_top AndAlso Not includeSubDirectories Then
            If MessageBox.Show(ActiveForm,
                   "The directory """ & directoryPath & """ doesn't contain images, but it's " &
                   "subdirectories. Do you want to include subdirectories? If you click no the " &
                   "default value will be set.",
                   directoryDescription & " image folder empty",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Return Tuple.Create(True, rtnPath)
            End If
        End If

        Return Tuple.Create(includeSubDirectories, defaultPath)
    End Function

    Private Function CheckImageFolder(imageGenre As ImageGenre) As Boolean
        Dim isSubdir As Boolean = mySettingsAccessor.ImageGenreIncludeSubDirectory(imageGenre)
        Dim imageFolder As String = mySettingsAccessor.ImageGenreFolder(imageGenre)

        Dim def As String = String.Empty

        Dim folderCheck As Tuple(Of Boolean, String) = CheckFolderSettings(imageGenre.ToString(), imageFolder, def, isSubdir)

        mySettingsAccessor.ImageGenreFolder(imageGenre) = folderCheck.Item2
        mySettingsAccessor.IsImageGenreEnabled(imageGenre) = Not (folderCheck.Item2 = def)
        mySettingsAccessor.ImageGenreIncludeSubDirectory(imageGenre) = folderCheck.Item1

        Return mySettingsAccessor.IsImageGenreEnabled(imageGenre)
    End Function

#Region "------------------------------------- Hardcore Images -------------------------------------------"

    Private Sub BTNIHardcore_Click(sender As Object, e As EventArgs) Handles BTNIHardcore.Click
        SetFolderFromDialog(ImageGenre.Hardcore)
    End Sub

    Private Sub BTNISoftcore_Click(sender As Object, e As EventArgs) Handles BTNISoftcore.Click
        SetFolderFromDialog(ImageGenre.Softcore)
    End Sub

    Private Sub BTNILesbian_Click(sender As Object, e As EventArgs) Handles BTNILesbian.Click
        SetFolderFromDialog(ImageGenre.Lesbian)
    End Sub

    Private Sub BTNIBlowjob_Click(sender As Object, e As EventArgs) Handles BTNIBlowjob.Click
        SetFolderFromDialog(ImageGenre.Blowjob)
    End Sub

    Private Sub BTNIFemdom_Click(sender As Object, e As EventArgs) Handles BTNIFemdom.Click
        SetFolderFromDialog(ImageGenre.Femdom)
    End Sub

    Private Sub BTNILezdom_Click(sender As Object, e As EventArgs) Handles BTNILezdom.Click
        SetFolderFromDialog(ImageGenre.Lezdom)
    End Sub

    Private Sub BTNIHentai_Click(sender As Object, e As EventArgs) Handles BTNIHentai.Click
        SetFolderFromDialog(ImageGenre.Hentai)
    End Sub

    Private Sub BTNIGay_Click(sender As Object, e As EventArgs) Handles BTNIGay.Click
        SetFolderFromDialog(ImageGenre.Gay)
    End Sub

    Private Sub BTNIMaledom_Click(sender As Object, e As EventArgs) Handles BTNIMaledom.Click
        SetFolderFromDialog(ImageGenre.Maledom)
    End Sub

    Private Sub BTNIGeneral_Click(sender As Object, e As EventArgs) Handles BTNIGeneral.Click
        SetFolderFromDialog(ImageGenre.General)
    End Sub

    Private Sub BTNICaptions_Click(sender As Object, e As EventArgs) Handles BTNICaptions.Click
        SetFolderFromDialog(ImageGenre.Captions)
    End Sub

    Private Sub BTNBoobPath_Click(sender As Object, e As EventArgs) Handles BTNBoobPath.Click
        SetFolderFromDialog(ImageGenre.Boobs)
    End Sub

    Private Sub BTNButtPath_Click(sender As Object, e As EventArgs) Handles BTNButtPath.Click
        SetFolderFromDialog(ImageGenre.Butt)
    End Sub

#End Region ' Butt

    Private Sub ImagePathTextBox_DoubleClick(sender As Object, e As EventArgs) Handles TbxIHardcore.DoubleClick, TbxISoftcore.DoubleClick
        CType(sender, TextBox).Text = "No path selected"
    End Sub

    Private Sub LBLILesbian_Click(sender As Object, e As EventArgs) Handles TbxILesbian.DoubleClick
        TbxILesbian.Text = "No path selected"
    End Sub

    Private Sub LBLIBlowjob_Click(sender As Object, e As EventArgs) Handles TbxIBlowjob.DoubleClick
        TbxIBlowjob.Text = "No path selected"
    End Sub

    Private Sub LBLIFemdom_Click(sender As Object, e As EventArgs) Handles TbxIFemdom.DoubleClick
        TbxIFemdom.Text = "No path selected"
    End Sub

    Private Sub LBLILezdom_Click(sender As Object, e As EventArgs) Handles TbxILezdom.DoubleClick
        TbxILezdom.Text = "No path selected"
    End Sub

    Private Sub LBLIHentai_Click(sender As Object, e As EventArgs) Handles TbxIHentai.DoubleClick
        TbxIHentai.Text = "No path selected"
    End Sub

    Private Sub LBLIGay_Click(sender As Object, e As EventArgs) Handles TbxIGay.DoubleClick
        TbxIGay.Text = "No path selected"
    End Sub

    Private Sub LBLIMaledom_Click(sender As Object, e As EventArgs) Handles TbxIMaledom.DoubleClick
        TbxIMaledom.Text = "No path selected"
    End Sub

    Private Sub LBLICaptions_Click(sender As Object, e As EventArgs) Handles TbxICaptions.DoubleClick
        TbxICaptions.Text = "No path selected"
    End Sub

    Private Sub LBLIGeneral_Click(sender As Object, e As EventArgs) Handles TbxIGeneral.DoubleClick
        TbxIGeneral.Text = "No path selected"
    End Sub

#Region "----------------------------------- GenreImages-Url-Files --------------------------------------"

    Private Sub BtnImageUrlSetFile_Click(sender As Object, e As EventArgs) Handles BtnImageUrlHardcore.Click,
                    BtnImageUrlSoftcore.Click, BtnImageUrlMaledom.Click, BtnImageUrlLezdom.Click, BtnImageUrlLesbian.Click,
                    BtnImageUrlHentai.Click, BtnImageUrlGeneral.Click, BtnImageUrlGay.Click, BtnImageUrlFemdom.Click,
                    BtnImageUrlCaptions.Click, BtnImageUrlButt.Click, BtnImageUrlBoobs.Click, BtnImageUrlBlowjob.Click
        Try
            ' Read the Row of the current Button
            Dim tmpTlpRow As Integer = TlpImageUrls.GetRow(sender)

            ' Check if the Button is in the TableLayoutPanel.
            If tmpTlpRow = -1 Then Throw New Exception("Can't find control in TableLayoutPanel. " &
                                                       "This is a major Design issue has to be fixed in code.")

            ' Get the Checkbox for the current button
            Dim tmpCheckbox As CheckBox = TlpImageUrls.GetControlFromPosition(0, tmpTlpRow)

            ' Check if the Text-Property has an active Databinding.
            If tmpCheckbox.DataBindings.Item("Checked") Is Nothing Then _
                Throw New InvalidDataException("Databinding """" Checked """" was not found in Checkbox." &
                                                "This is a major design issue and has to be fixed in code.")

            ' Get the TExtBox for the Current Button
            Dim tmpTextbox As TextBox = TlpImageUrls.GetControlFromPosition(2, tmpTlpRow)

            ' Check if the Text-Property has an active Databinding.
            If tmpTextbox.DataBindings.Item("Text") Is Nothing Then _
                Throw New InvalidDataException("This function is only availabe with a Databound Textbox. " &
                                                "This is a major design issue and has to be fixed in code.")

            'Declare a new instance of An OpenFileDialog. Use the URL-FilePat as initial
            Dim tmpFS As New OpenFileDialog With {
                .Filter = "Textfiles|*.txt",
                .Multiselect = False,
                .CheckFileExists = True,
                .Title = "Select an " & tmpCheckbox.Text & " URL-File",
                .InitialDirectory = MainWindow.pathUrlFileDir}

            ' Check if the URL-FilePath exits -> Otherwise create it.
            If Not Directory.Exists(tmpFS.InitialDirectory) Then _
            Directory.CreateDirectory(tmpFS.InitialDirectory)

            Dim tmpPath As String = tmpTextbox.Text
            If tmpPath.ToLower.EndsWith(".txt") Then
                If Path.IsPathRooted(tmpPath) AndAlso Directory.Exists(Path.GetDirectoryName(tmpPath)) Then
                    ' Set an alternate Initial directory if filepath is absolute 
                    tmpFS.InitialDirectory = Path.GetDirectoryName(tmpPath)
                    tmpFS.FileName = Path.GetFileName(tmpPath)
                Else
                    ' Set the given Filename
                    tmpFS.FileName = tmpPath
                End If
            End If

            If tmpFS.ShowDialog() = DialogResult.OK Then
                If Path.GetDirectoryName(tmpFS.FileName).ToLower = Path.GetDirectoryName(MainWindow.pathUrlFileDir).ToLower Then
                    ' If the file is located standarddirectory st only the filename
                    tmpTextbox.Text = tmpFS.SafeFileName
                Else
                    ' Otherwise set the absoulte filepath
                    tmpTextbox.Text = tmpFS.FileName
                End If

                ' This will force the Settings to save.
                tmpCheckbox.Checked = True
            End If
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '						       All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            MsgBox(ex.Message & vbCrLf & "Please report this error at the Milovana Forum.",
                   MsgBoxStyle.Critical, "Cant Set URl-File")
            Log.WriteError(ex.Message, ex, "Error Set Url-File")
        End Try
    End Sub

#End Region 'GenreImages-Url-Files

#End Region ' Images

#Region "--------------------------------------- Videos -------------------------------------------------"

    Friend Shared Function Video_FolderCheck(ByVal directoryDescription As String, ByVal directoryPath As String, ByVal defaultPath As String) As String
        ' Exit if the directory exists.
        If Directory.Exists(directoryPath) Then Return directoryPath
        ' Exit if default value.
        If directoryPath = defaultPath Then Return defaultPath

        ' Tell User, the dir. wasn't found. Ask to search manually for the folder.
        If MessageBox.Show(ActiveForm,
                           "The directory """ & directoryPath & """ was not found." & vbCrLf & "Do you want to search for it?",
                           directoryDescription & " directory not found.",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            ' Find the first available parent-directory. 
            ' This way the user hasn't to browse through his hole IO-System.
            Dim __tmp_dir As String = directoryPath
            Do Until Directory.Exists(__tmp_dir) Or __tmp_dir Is Nothing
                __tmp_dir = Path.GetDirectoryName(__tmp_dir)
            Loop

            ' Initialize new Dialog-Form
            Dim FolSel As New FolderBrowserDialog With {.SelectedPath = __tmp_dir,
                                                        .Description = "Select " & directoryDescription & " folder."}
            ' Display the Dialog -> Now the user has to set the new dir.
            If FolSel.ShowDialog(ActiveForm) = DialogResult.OK Then
                Return FolSel.SelectedPath
            End If

        End If ' END IF - Messagebox.
        Return defaultPath
    End Function

    Friend Function Video_CheckAllFolders() As Integer
        Dim t As Integer = 0

        LblVideoHardCoreTotal.Text = VideoHardcore_Count() : t += CInt(LblVideoHardCoreTotal.Text)
        LblVideoSoftCoreTotal.Text = VideoSoftcore_Count() : t += CInt(LblVideoSoftCoreTotal.Text)
        LblVideoLesbianTotal.Text = VideoLesbian_Count() : t += CInt(LblVideoLesbianTotal.Text)
        LblVideoBlowjobTotal.Text = VideoBlowjob_Count() : t += CInt(LblVideoBlowjobTotal.Text)
        LblVideoFemdomTotal.Text = VideoFemdom_Count() : t += CInt(LblVideoFemdomTotal.Text)
        LblVideoFemsubTotal.Text = VideoFemsub_Count() : t += CInt(LblVideoFemsubTotal.Text)
        LblVideoJOITotal.Text = VideoJOI_Count() : t += CInt(LblVideoJOITotal.Text)
        LblVideoCHTotal.Text = VideoCH_Count() : t += CInt(LblVideoCHTotal.Text)
        LblVideoGeneralTotal.Text = VideoGeneral_Count() : t += CInt(LblVideoGeneralTotal.Text)

        LblVideoHardCoreTotalD.Text = VideoHardcoreD_Count() : t += CInt(LblVideoHardCoreTotalD.Text)
        LblVideoSoftCoreTotalD.Text = VideoSoftcoreD_Count() : t += CInt(LblVideoSoftCoreTotalD.Text)
        LblVideoLesbianTotalD.Text = VideoLesbianD_Count() : t += CInt(LblVideoLesbianTotalD.Text)
        LblVideoBlowjobTotalD.Text = VideoBlowjobD_Count() : t += CInt(LblVideoBlowjobTotalD.Text)
        LblVideoFemdomTotalD.Text = VideoFemdomD_Count() : t += CInt(LblVideoFemdomTotalD.Text)
        LblVideoFemsubTotalD.Text = VideoFemsubD_Count() : t += CInt(LblVideoFemsubTotalD.Text)
        LblVideoJOITotalD.Text = VideoJOID_Count() : t += CInt(LblVideoJOITotalD.Text)
        LblVideoCHTotalD.Text = VideoCHD_Count() : t += CInt(LblVideoCHTotalD.Text)
        LblVideoGeneralTotalD.Text = VideoGeneralD_Count() : t += CInt(LblVideoGeneralTotalD.Text)

        Return t
    End Function
#Region "----------------------------------------- Regular -----------------------------------------------"

#Region "------------------------------------- Hardcore Videos -------------------------------------------"

    Private Sub BTNVideoHardCore_Click(sender As Object, e As EventArgs) Handles BTNVideoHardCore.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoHardcore = FolderBrowserDialog1.SelectedPath
            My.Settings.CBHardcore = True
            LblVideoHardCoreTotal.Text = VideoHardcore_Count(False)
        End If
    End Sub

    Friend Shared Function VideoHardcore_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoHardcore").Property.DefaultValue

        My.Settings.VideoHardcore =
            Video_FolderCheck("Hardcore Video", My.Settings.VideoHardcore, def)

        If My.Settings.VideoHardcore = def Then My.Settings.CBHardcore = False

        Return My.Settings.CBHardcore
    End Function

    Friend Shared Function VideoHardcore_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoHardcore_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoHardcore).Count
    End Function

#End Region ' Hardcore

#Region "------------------------------------- Softcore Videos -------------------------------------------"

    Private Sub BTNVideoSoftCore_Click(sender As Object, e As EventArgs) Handles BTNVideoSoftCore.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoSoftcore = FolderBrowserDialog1.SelectedPath
            My.Settings.CBSoftcore = True
            LblVideoSoftCoreTotal.Text = VideoSoftcore_Count(False)
        End If
    End Sub

    Friend Shared Function VideoSoftcore_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoSoftcore").Property.DefaultValue

        My.Settings.VideoSoftcore =
            Video_FolderCheck("Softcore Video", My.Settings.VideoSoftcore, def)

        If My.Settings.VideoSoftcore = def Then My.Settings.CBSoftcore = False

        Return My.Settings.CBSoftcore
    End Function

    Friend Shared Function VideoSoftcore_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoSoftcore_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoSoftcore).Count
    End Function

#End Region ' Softcore

#Region "------------------------------------- Lesbian Videos --------------------------------------------"

    Private Sub BTNVideoLesbian_Click(sender As Object, e As EventArgs) Handles BTNVideoLesbian.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoLesbian = FolderBrowserDialog1.SelectedPath
            My.Settings.CBLesbian = True
            LblVideoLesbianTotal.Text = VideoLesbian_Count(False)
        End If
    End Sub

    Friend Shared Function VideoLesbian_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoLesbian").Property.DefaultValue

        My.Settings.VideoLesbian =
            Video_FolderCheck("Lesbian Video", My.Settings.VideoLesbian, def)

        If My.Settings.VideoLesbian = def Then My.Settings.CBLesbian = False

        Return My.Settings.CBLesbian
    End Function

    Friend Shared Function VideoLesbian_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoLesbian_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoLesbian).Count
    End Function

#End Region ' Lesbian

#Region "------------------------------------- Blowjob Videos --------------------------------------------"

    Private Sub BTNVideoBlowjob_Click(sender As Object, e As EventArgs) Handles BTNVideoBlowjob.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoBlowjob = FolderBrowserDialog1.SelectedPath
            My.Settings.CBBlowjob = True
            LblVideoBlowjobTotal.Text = VideoBlowjob_Count(False)
        End If
    End Sub

    Friend Shared Function VideoBlowjob_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoBlowjob").Property.DefaultValue

        My.Settings.VideoBlowjob =
            Video_FolderCheck("Blowjob Video", My.Settings.VideoBlowjob, def)

        If My.Settings.VideoBlowjob = def Then My.Settings.CBBlowjob = False

        Return My.Settings.CBBlowjob
    End Function

    Friend Shared Function VideoBlowjob_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoBlowjob_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoBlowjob).Count
    End Function

#End Region ' Blowjob

#Region "---------------------------------------- Femdom -------------------------------------------------"

    Private Sub BTNVideoFemDom_Click(sender As Object, e As EventArgs) Handles BTNVideoFemDom.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoFemdom = FolderBrowserDialog1.SelectedPath
            My.Settings.CBFemdom = True
            LblVideoFemdomTotal.Text = VideoFemdom_Count(False)
        End If
    End Sub

    Friend Shared Function VideoFemdom_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoFemdom").Property.DefaultValue

        My.Settings.VideoFemdom =
            Video_FolderCheck("Femdom Video", My.Settings.VideoFemdom, def)

        If My.Settings.VideoFemdom = def Then My.Settings.CBFemdom = False

        Return My.Settings.CBFemdom
    End Function

    Friend Shared Function VideoFemdom_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoFemdom_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoFemdom).Count
    End Function

#End Region ' Femdom

#Region "------------------------------------- Femsub Videos ---------------------------------------------"

    Private Sub BTNVideoFemSub_Click(sender As Object, e As EventArgs) Handles BTNVideoFemSub.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoFemsub = FolderBrowserDialog1.SelectedPath
            My.Settings.CBFemsub = True
            LblVideoFemsubTotal.Text = VideoFemsub_Count(False)
        End If
    End Sub

    Friend Shared Function VideoFemsub_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoFemsub").Property.DefaultValue

        My.Settings.VideoFemsub =
            Video_FolderCheck("Femsub Video", My.Settings.VideoFemsub, def)

        If My.Settings.VideoFemsub = def Then My.Settings.CBFemsub = False

        Return My.Settings.CBFemsub
    End Function

    Friend Shared Function VideoFemsub_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoFemsub_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoFemsub).Count
    End Function

#End Region ' Femsub

#Region "------------------------------------- JOI Videos ------------------------------------------------"

    Private Sub BTNVideoJOI_Click(sender As Object, e As EventArgs) Handles BTNVideoJOI.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoJOI = FolderBrowserDialog1.SelectedPath
            My.Settings.CBJOI = True
            LblVideoJOITotal.Text = VideoJOI_Count(False)
        End If
    End Sub

    Friend Shared Function VideoJOI_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoJOI").Property.DefaultValue

        My.Settings.VideoJOI =
            Video_FolderCheck("JOI Video", My.Settings.VideoJOI, def)

        If My.Settings.VideoJOI = def Then My.Settings.CBJOI = False

        Return My.Settings.CBJOI
    End Function

    Friend Shared Function VideoJOI_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoJOI_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoJOI).Count
    End Function

#End Region ' JOI

#Region "------------------------------------- CH Videos -------------------------------------------------"

    Private Sub BTNVideoCH_Click(sender As Object, e As EventArgs) Handles BTNVideoCH.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoCH = FolderBrowserDialog1.SelectedPath
            My.Settings.CBCH = True
            LblVideoCHTotal.Text = VideoCH_Count(False)
        End If
    End Sub

    Friend Shared Function VideoCH_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoCH").Property.DefaultValue

        My.Settings.VideoCH =
            Video_FolderCheck("CH Video", My.Settings.VideoCH, def)

        If My.Settings.VideoCH = def Then My.Settings.CBCH = False

        Return My.Settings.CBCH
    End Function

    Friend Shared Function VideoCH_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoCH_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoCH).Count
    End Function

#End Region ' CH

#Region "------------------------------------- General Videos --------------------------------------------"

    Private Sub BTNVideoGeneral_Click(sender As Object, e As EventArgs) Handles BTNVideoGeneral.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoGeneral = FolderBrowserDialog1.SelectedPath
            My.Settings.CBGeneral = True
            LblVideoGeneralTotal.Text = VideoGeneral_Count(False)
        End If
    End Sub

    Friend Shared Function VideoGeneral_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoGeneral").Property.DefaultValue

        My.Settings.VideoGeneral =
            Video_FolderCheck("General Video", My.Settings.VideoGeneral, def)

        If My.Settings.VideoGeneral = def Then My.Settings.CBGeneral = False

        Return My.Settings.CBGeneral
    End Function

    Friend Shared Function VideoGeneral_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoGeneral_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoGeneral).Count
    End Function

#End Region ' General

#End Region ' Regular

#Region "------------------------------------------ Domme ------------------------------------------------"

#Region "---------------------------------------- HardcoreD ----------------------------------------------"

    Private Sub BTNVideoHardcoreD_Click(sender As Object, e As EventArgs) Handles BTNVideoHardCoreD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoHardcoreD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBHardcoreD = True
            LblVideoHardCoreTotalD.Text = VideoHardcoreD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoHardcoreD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoHardcoreD").Property.DefaultValue

        My.Settings.VideoHardcoreD =
            Video_FolderCheck("HardcoreD Video", My.Settings.VideoHardcoreD, def)

        If My.Settings.VideoHardcoreD = def Then My.Settings.CBHardcoreD = False

        Return My.Settings.CBHardcoreD
    End Function

    Friend Shared Function VideoHardcoreD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoHardcoreD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoHardcoreD).Count
    End Function

#End Region ' HardcoreD

#Region "---------------------------------------- SoftcoreD ----------------------------------------------"

    Private Sub BTNVideoSoftcoreD_Click(sender As Object, e As EventArgs) Handles BTNVideoSoftCoreD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoSoftcoreD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBSoftcoreD = True
            LblVideoSoftCoreTotalD.Text = VideoSoftcoreD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoSoftcoreD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoSoftcoreD").Property.DefaultValue

        My.Settings.VideoSoftcoreD =
            Video_FolderCheck("SoftcoreD Video", My.Settings.VideoSoftcoreD, def)

        If My.Settings.VideoSoftcoreD = def Then My.Settings.CBSoftcoreD = False

        Return My.Settings.CBSoftcoreD
    End Function

    Friend Shared Function VideoSoftcoreD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoSoftcoreD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoSoftcoreD).Count
    End Function

#End Region ' SoftcoreD

#Region "---------------------------------------- LesbianD -----------------------------------------------"

    Private Sub BTNVideoLesbianD_Click(sender As Object, e As EventArgs) Handles BTNVideoLesbianD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoLesbianD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBLesbianD = True
            LblVideoLesbianTotalD.Text = VideoLesbianD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoLesbianD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoLesbianD").Property.DefaultValue

        My.Settings.VideoLesbianD =
            Video_FolderCheck("LesbianD Video", My.Settings.VideoLesbianD, def)

        If My.Settings.VideoLesbianD = def Then My.Settings.CBLesbianD = False

        Return My.Settings.CBLesbianD
    End Function

    Friend Shared Function VideoLesbianD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoLesbianD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoLesbianD).Count
    End Function

#End Region ' LesbianD

#Region "---------------------------------------- BlowjobD -----------------------------------------------"

    Private Sub BTNVideoBlowjobD_Click(sender As Object, e As EventArgs) Handles BTNVideoBlowjobD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoBlowjobD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBBlowjobD = True
            LblVideoBlowjobTotalD.Text = VideoBlowjobD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoBlowjobD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoBlowjobD").Property.DefaultValue

        My.Settings.VideoBlowjobD =
            Video_FolderCheck("BlowjobD Video", My.Settings.VideoBlowjobD, def)

        If My.Settings.VideoBlowjobD = def Then My.Settings.CBBlowjobD = False

        Return My.Settings.CBBlowjobD
    End Function

    Friend Shared Function VideoBlowjobD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoBlowjobD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoBlowjobD).Count
    End Function

#End Region ' BlowjobD

#Region "---------------------------------------- FemdomD ------------------------------------------------"

    Private Sub BTNVideoFemdomD_Click(sender As Object, e As EventArgs) Handles BTNVideoFemDomD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoFemdomD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBFemdomD = True
            LblVideoFemdomTotalD.Text = VideoFemdomD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoFemdomD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoFemdomD").Property.DefaultValue

        My.Settings.VideoFemdomD =
            Video_FolderCheck("FemdomD Video", My.Settings.VideoFemdomD, def)

        If My.Settings.VideoFemdomD = def Then My.Settings.CBFemdomD = False

        Return My.Settings.CBFemdomD
    End Function

    Friend Shared Function VideoFemdomD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoFemdomD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoFemdomD).Count
    End Function

#End Region ' FemdomD

#Region "---------------------------------------- FemsubD ------------------------------------------------"

    Private Sub BTNVideoFemsubD_Click(sender As Object, e As EventArgs) Handles BTNVideoFemSubD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoFemsubD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBFemsubD = True
            LblVideoFemsubTotalD.Text = VideoFemsubD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoFemsubD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoFemsubD").Property.DefaultValue

        My.Settings.VideoFemsubD =
            Video_FolderCheck("FemsubD Video", My.Settings.VideoFemsubD, def)

        If My.Settings.VideoFemsubD = def Then My.Settings.CBFemsubD = False

        Return My.Settings.CBFemsubD
    End Function

    Friend Shared Function VideoFemsubD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoFemsubD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoFemsubD).Count
    End Function

#End Region ' FemsubD

#Region "---------------------------------------- JOI-D --------------------------------------------------"

    Private Sub BTNVideoJOID_Click(sender As Object, e As EventArgs) Handles BTNVideoJOID.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoJOID = FolderBrowserDialog1.SelectedPath
            My.Settings.CBJOID = True
            LblVideoJOITotalD.Text = VideoJOID_Count(False)
        End If
    End Sub

    Friend Shared Function VideoJOID_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoJOID").Property.DefaultValue

        My.Settings.VideoJOID =
            Video_FolderCheck("JOID Video", My.Settings.VideoJOID, def)

        If My.Settings.VideoJOID = def Then My.Settings.CBJOID = False

        Return My.Settings.CBJOID
    End Function

    Friend Shared Function VideoJOID_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoJOID_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoJOID).Count
    End Function

#End Region ' JOI-D

#Region "---------------------------------------- CH-D ---------------------------------------------------"

    Private Sub BTNVideoCHD_Click(sender As Object, e As EventArgs) Handles BTNVideoCHD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoCHD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBCHD = True
            LblVideoCHTotalD.Text = VideoCHD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoCHD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoCHD").Property.DefaultValue

        My.Settings.VideoCHD =
            Video_FolderCheck("CHD Video", My.Settings.VideoCHD, def)

        If My.Settings.VideoCHD = def Then My.Settings.CBCHD = False

        Return My.Settings.CBCHD
    End Function

    Friend Shared Function VideoCHD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoCHD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoCHD).Count
    End Function

#End Region ' CH-D

#Region "---------------------------------------- GeneralD -----------------------------------------------"

    Private Sub BTNVideoGeneralD_Click(sender As Object, e As EventArgs) Handles BTNVideoGeneralD.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.VideoGeneralD = FolderBrowserDialog1.SelectedPath
            My.Settings.CBGeneralD = True
            LblVideoGeneralTotalD.Text = VideoGeneralD_Count(False)
        End If
    End Sub

    Friend Shared Function VideoGeneralD_CheckFolder() As Boolean
        Dim def As String =
            My.Settings.PropertyValues("VideoGeneralD").Property.DefaultValue

        My.Settings.VideoGeneralD =
            Video_FolderCheck("GeneralD Video", My.Settings.VideoGeneralD, def)

        If My.Settings.VideoGeneralD = def Then My.Settings.CBGeneralD = False

        Return My.Settings.CBGeneralD
    End Function

    Friend Shared Function VideoGeneralD_Count(Optional ByVal checkfolder As Boolean = True) As Integer
        If checkfolder Then VideoGeneralD_CheckFolder()
        Return myDirectory.GetFilesVideo(My.Settings.VideoGeneralD).Count
    End Function

#End Region ' GeneralD

#End Region ' Domme

    Private Sub BTNRefreshVideos_Click(sender As Object, e As EventArgs) Handles BTNRefreshVideos.Click
        VideoDescriptionLabel.Text = "Refresh complete: " & Video_CheckAllFolders() & " videos found!"
        VideoDescriptionLabel.Text = VideoDescriptionLabel.Text.Replace(": 1 videos", ": 1 video")
    End Sub

#End Region ' Videos


    Private Sub ComboBox1_DrawItem(ByVal sender As Object, ByVal e As Windows.Forms.DrawItemEventArgs) Handles SubMessageFontCB.DrawItem
        e.DrawBackground()
        If (e.State And DrawItemState.Focus) <> 0 Then
            e.DrawFocusRectangle()
        End If
        Dim objBrush As Brush = Nothing
        Try
            objBrush = New SolidBrush(e.ForeColor)
            Dim _FontName As String = SubMessageFontCB.Items(e.Index)
            Dim _font As Font
            Dim _fontfamily = New FontFamily(_FontName)
            If _fontfamily.IsStyleAvailable(FontStyle.Regular) Then
                _font = New Font(_fontfamily, 14, FontStyle.Regular)
            ElseIf _fontfamily.IsStyleAvailable(FontStyle.Bold) Then
                _font = New Font(_fontfamily, 14, FontStyle.Bold)
            ElseIf _fontfamily.IsStyleAvailable(FontStyle.Italic) Then
                _font = New Font(_fontfamily, 14, FontStyle.Italic)
            End If
            e.Graphics.DrawString(_FontName, _font, objBrush, e.Bounds)
        Finally
            If objBrush IsNot Nothing Then
                objBrush.Dispose()
            End If
            objBrush = Nothing
        End Try
    End Sub

    Private Sub ComboBox1D_DrawItem(ByVal sender As Object, ByVal e As Windows.Forms.DrawItemEventArgs) Handles DommeMessageFontCB.DrawItem
        e.DrawBackground()
        If (e.State And DrawItemState.Focus) <> 0 Then
            e.DrawFocusRectangle()
        End If
        Using objBrush As Brush = New SolidBrush(e.ForeColor)
            Dim _FontName As String = DommeMessageFontCB.Items(e.Index)
            Dim _font As Font
            Dim _fontfamily = New FontFamily(_FontName)
            If _fontfamily.IsStyleAvailable(FontStyle.Regular) Then
                _font = New Font(_fontfamily, 14, FontStyle.Regular)
            ElseIf _fontfamily.IsStyleAvailable(FontStyle.Bold) Then
                _font = New Font(_fontfamily, 14, FontStyle.Bold)
            ElseIf _fontfamily.IsStyleAvailable(FontStyle.Italic) Then
                _font = New Font(_fontfamily, 14, FontStyle.Italic)
            End If
            e.Graphics.DrawString(_FontName, _font, objBrush, e.Bounds)
        End Using
    End Sub

    Private Sub CockSizeNumBox_ValueChanged(sender As Object, e As EventArgs) Handles CockSizeNumBox.ValueChanged
        My.Settings.SubCockSize = CockSizeNumBox.Value
    End Sub

    Private Sub NBCensorShowMin_Leave(sender As Object, e As EventArgs) Handles NBCensorShowMin.Leave
        My.Settings.NBCensorShowMin = NBCensorShowMin.Value
    End Sub

    Private Sub NBCensorShowMax_Leave(sender As Object, e As EventArgs) Handles NBCensorShowMax.Leave
        My.Settings.NBCensorShowMax = NBCensorShowMax.Value
    End Sub

    Private Sub NBCensorHideMin_Leave(sender As Object, e As EventArgs) Handles NBCensorHideMin.Leave
        My.Settings.NBCensorHideMin = NBCensorHideMin.Value
    End Sub

    Private Sub NBCensorHideMax_Leave(sender As Object, e As EventArgs) Handles NBCensorHideMax.Leave
        My.Settings.NBCensorHideMax = NBCensorHideMax.Value
    End Sub

    Private Sub CBCensorConstant_CheckedChanged(sender As Object, e As EventArgs) Handles CBCensorConstant.CheckedChanged
        My.Settings.CBCensorConstant = CBCensorConstant.Checked
    End Sub

    Private Sub NBCensorShowMin_ValueChanged(sender As Object, e As EventArgs) Handles NBCensorShowMin.ValueChanged
        If NBCensorShowMin.Value > NBCensorShowMax.Value Then NBCensorShowMin.Value = NBCensorShowMax.Value
    End Sub

    Private Sub NBCensorShowMax_ValueChanged(sender As Object, e As EventArgs) Handles NBCensorShowMax.ValueChanged
        If NBCensorShowMax.Value < NBCensorShowMin.Value Then NBCensorShowMax.Value = NBCensorShowMin.Value
    End Sub

    Private Sub NBTeaseLengthMin_LostFocus(sender As Object, e As EventArgs) Handles NBTeaseLengthMin.LostFocus
        mySettingsAccessor.TeaseLengthMinimum = NBTeaseLengthMin.Value
    End Sub

    Private Sub NBTeaseLengthMax_LostFocus(sender As Object, e As EventArgs) Handles NBTeaseLengthMax.LostFocus
        mySettingsAccessor.TeaseLengthMaximum = NBTeaseLengthMax.Value
    End Sub

    Private Sub NBTauntCycleMin_LostFocus(sender As Object, e As EventArgs) Handles NBTauntCycleMin.LostFocus
        mySettingsAccessor.TauntCycleMinimum = NBTauntCycleMin.Value
    End Sub

    Private Sub NBTauntCycleMax_LostFocus(sender As Object, e As EventArgs) Handles NBTauntCycleMax.LostFocus
        mySettingsAccessor.TauntCycleMaximum = NBTauntCycleMax.Value
    End Sub

    Private Sub NBRedLightMin_LostFocus(sender As Object, e As EventArgs) Handles NBRedLightMin.LostFocus
        My.Settings.RedLightMin = NBRedLightMin.Value
    End Sub

    Private Sub NBRedLightMax_LostFocus(sender As Object, e As EventArgs) Handles NBRedLightMax.LostFocus
        My.Settings.RedLightMax = NBRedLightMax.Value
    End Sub
    Private Sub NBGreenLightMin_LostFocus(sender As Object, e As EventArgs) Handles NBGreenLightMin.LostFocus
        My.Settings.GreenLightMin = NBGreenLightMin.Value
    End Sub

    Private Sub NBGreenLightMax_LostFocus(sender As Object, e As EventArgs) Handles NBGreenLightMax.LostFocus
        My.Settings.GreenLightMax = NBGreenLightMax.Value
    End Sub

    Private Sub NBTeaseLengthMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTeaseLengthMin.ValueChanged
        If NBTeaseLengthMin.Value > NBTeaseLengthMax.Value Then NBTeaseLengthMin.Value = NBTeaseLengthMax.Value
    End Sub

    Private Sub NBTeaseLengthMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTeaseLengthMax.ValueChanged
        If NBTeaseLengthMax.Value < NBTeaseLengthMin.Value Then NBTeaseLengthMax.Value = NBTeaseLengthMin.Value
    End Sub

    Private Sub NBTauntCycleMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTauntCycleMin.ValueChanged
        If NBTauntCycleMin.Value > NBTauntCycleMax.Value Then NBTauntCycleMin.Value = NBTauntCycleMax.Value
    End Sub

    Private Sub NBTauntCycleMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTauntCycleMax.ValueChanged
        If NBTauntCycleMax.Value < NBTauntCycleMin.Value Then NBTauntCycleMax.Value = NBTauntCycleMin.Value
    End Sub

    Private Sub NBCensorHideMin_ValueChanged(sender As Object, e As EventArgs) Handles NBCensorHideMin.ValueChanged
        If NBCensorHideMin.Value > NBCensorHideMax.Value Then NBCensorHideMin.Value = NBCensorHideMax.Value
    End Sub

    Private Sub NBCensorHideMax_ValueChanged(sender As Object, e As EventArgs) Handles NBCensorHideMax.ValueChanged
        If NBCensorHideMax.Value < NBCensorHideMin.Value Then NBCensorHideMax.Value = NBCensorHideMin.Value
    End Sub

    Private Sub Button26_Click_1(sender As Object, e As EventArgs) Handles BTNVideoModLoad.Click

        Dim CensorText As String = "NULL"

        If CBVTType.Text = "Censorship Sucks" Then
            If LBVidScript.SelectedItem = "CensorBarOff" Then CensorText = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Video\Censorship Sucks\CensorBarOff.txt"
            If LBVidScript.SelectedItem = "CensorBarOn" Then CensorText = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Video\Censorship Sucks\CensorBarOn.txt"
        End If

        If CBVTType.Text = "Avoid The Edge" Then
            If LBVidScript.SelectedItem = "Taunts" Then CensorText = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Video\Avoid The Edge\Taunts.txt"
        End If

        If CBVTType.Text = "Red Light Green Light" Then
            If LBVidScript.SelectedItem = "Green Light" Then CensorText = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Video\Red Light Green Light\Green Light.txt"
            If LBVidScript.SelectedItem = "Red Light" Then CensorText = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Video\Red Light Green Light\Red Light.txt"
            If LBVidScript.SelectedItem = "Taunts" Then CensorText = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Video\Red Light Green Light\Taunts.txt"
        End If

        MainWindow.ssh.VTPath = CensorText

        Dim VidReader As New StreamReader(CensorText)
        Dim VidList As New List(Of String)

        While VidReader.Peek <> -1
            VidList.Add(VidReader.ReadLine())
        End While

        VidReader.Close()
        VidReader.Dispose()

        Dim VidString As String = String.Empty

        For i As Integer = 0 To VidList.Count - 1
            If i <> VidList.Count - 1 Then
                VidString = VidString & VidList(i) & Environment.NewLine
            Else
                VidString = VidString & VidList(i)
            End If
        Next

        RTBVideoMod.Text = VidString

        LBVidScript.Enabled = False
        CBVTType.Enabled = False
        BTNVideoModClear.Enabled = True
        BTNVideoModLoad.Enabled = False
        RTBVideoMod.Enabled = True
        BTNVideoModSave.Enabled = False
    End Sub

    Private Sub RTBVideoMod_TextChanged(sender As Object, e As EventArgs) Handles RTBVideoMod.TextChanged
        BTNVideoModSave.Enabled = True
    End Sub

    Private Sub BTNVideoModClear_Click(sender As Object, e As EventArgs) Handles BTNVideoModClear.Click
        BTNVideoModClear.Enabled = False
        BTNVideoModLoad.Enabled = True
        CBVTType.Enabled = True
        RTBVideoMod.Text = ""
        RTBVideoMod.Enabled = False
        BTNVideoModSave.Enabled = False
        LBVidScript.Enabled = True
    End Sub

    Private Sub BTNVideoModSave_Click(sender As Object, e As EventArgs) Handles BTNVideoModSave.Click

        If MsgBox("This will overwrite the current " & CBVTType.Text & " script!" & Environment.NewLine & Environment.NewLine & "Are you sure?", vbYesNo, "Warning!") = MsgBoxResult.Yes Then
            Debug.Print("Worked?")
        Else
            Debug.Print("Did not work")
            Return
        End If

        My.Computer.FileSystem.DeleteFile(MainWindow.ssh.VTPath)

        Dim WriteList As New List(Of String)

        WriteList.Clear()

        For i As Integer = 0 To RTBVideoMod.Lines.Count - 1
            If i <> RTBVideoMod.Lines.Count - 1 Then
                WriteList.Add(RTBVideoMod.Lines(i) & Environment.NewLine)
            Else
                WriteList.Add(RTBVideoMod.Lines(i))
            End If
        Next

        For i As Integer = 0 To WriteList.Count - 1
            If i <> WriteList.Count - 1 Then
                My.Computer.FileSystem.WriteAllText(MainWindow.ssh.VTPath, WriteList(i), True)
            Else
                My.Computer.FileSystem.WriteAllText(MainWindow.ssh.VTPath, WriteList(i), True)
            End If
        Next

        MessageBox.Show(Me, "File saved successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)

        BTNVideoModSave.Enabled = False

    End Sub

    Private Sub Button26_Click_2(sender As Object, e As EventArgs) Handles Button26.Click
        TBGlitModFileName.Text = ""
        RTBGlitModDommePost.Text = ""
        RTBGlitModResponses.Text = ""
        LBGlitModScripts.ClearSelected()
    End Sub

    Private Sub CBGlitModType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBGlitModType.SelectedIndexChanged

        If MainWindow.FormLoading Then
            Return
        End If
        Dim files() As String = myDirectory.GetFiles(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Apps\Glitter\" & CBGlitModType.Text & "\")
        Dim GlitterScriptCount As Integer

        LBGlitModScripts.Items.Clear()

        For Each file As String In files

            GlitterScriptCount += 1
            LBGlitModScripts.Items.Add(Path.GetFileName(file).Replace(".txt", ""))

        Next

        LBLGlitModScriptCount.Text = CBGlitModType.Text & " Scripts Found (" & GlitterScriptCount & ")"
    End Sub

    Private Sub LBGlitModScripts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBGlitModScripts.SelectedIndexChanged

        Dim GlitPath As String = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Apps\Glitter\" & CBGlitModType.Text & "\" & LBGlitModScripts.SelectedItem & ".txt"

        If Not File.Exists(GlitPath) Then Return

        If GlitPath = MainWindow.ssh.StatusText Then
            MsgBox("This file is currently in use by the program. Saving changes may be slow until the Glitter process has finished.", , "Warning!")
        End If

        TBGlitModFileName.Text = LBGlitModScripts.SelectedItem

        RTBGlitModDommePost.Text = ""
        RTBGlitModResponses.Text = ""

        Dim ioFile As New StreamReader(GlitPath)
        Dim lines As New List(Of String)

        Dim GlitCount As Integer
        Dim GlitEnd As Integer

        GlitCount = -1

        While ioFile.Peek <> -1
            GlitCount += 1
            lines.Add(ioFile.ReadLine())
        End While


        GlitEnd = GlitCount
        GlitCount = 1

        RTBGlitModDommePost.Text = lines(0)


        Do
            RTBGlitModResponses.Text = RTBGlitModResponses.Text & lines(GlitCount) & Environment.NewLine
            GlitCount += 1
        Loop Until GlitCount = GlitEnd + 1

        ioFile.Close()
        ioFile.Dispose()

        Debug.Print(RTBGlitModResponses.Lines.Count)


    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click

        If TBGlitModFileName.Text = "" Or RTBGlitModDommePost.Text = "" Or RTBGlitModResponses.Text = "" Then
            MsgBox("Please make sure all fields have been filled out!", , "Error!")
            Return
        End If

        If RTBGlitModResponses.Lines.Count < 3 Then
            MsgBox("Please make sure the Responses text box has at least three responses!", , "Error!")
            Return
        End If

        Dim GlitPath As String = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Apps\Glitter\" & CBGlitModType.Text & "\" & TBGlitModFileName.Text & ".txt"

        If Not LBGlitModScripts.Items.Contains(TBGlitModFileName.Text) Then
            LBGlitModScripts.Items.Add(TBGlitModFileName.Text)
            My.Computer.FileSystem.WriteAllText(GlitPath, RTBGlitModDommePost.Text & Environment.NewLine & RTBGlitModResponses.Text, False)
            File.WriteAllLines(GlitPath, File.ReadAllLines(GlitPath).Where(Function(s) s <> String.Empty))
        Else
            If MsgBox(TBGlitModFileName.Text & ".txt already exists! Overwrite?", vbYesNo, "Warning!") = MsgBoxResult.Yes Then
                My.Computer.FileSystem.WriteAllText(GlitPath, RTBGlitModDommePost.Text & Environment.NewLine & RTBGlitModResponses.Text, False)
                File.WriteAllLines(GlitPath, File.ReadAllLines(GlitPath).Where(Function(s) s <> String.Empty))
            Else
                Debug.Print("Did not work")
                Return
            End If
        End If



    End Sub

    ''' =========================================================================================================
    ''' <summary>
    ''' Activates the specified is activaed in Listbox and saves the Listboxstate.
    ''' </summary>
    ''' <param name="URL_FileName"></param>
    Private Sub URL_File_Set(ByVal URL_FileName As String)
        ' Set the new URL-File
        If Not URLFileList.Items.Contains(URL_FileName) Then
            URLFileList.Items.Add(URL_FileName)
            For i As Integer = 0 To URLFileList.Items.Count - 1
                If URLFileList.Items(i) = URL_FileName Then URLFileList.SetItemChecked(i, True)
            Next
        End If
        ' Save ListState
        SaveURLFileSelection()
    End Sub

    Private Sub SliderSTF_Scroll(sender As Object, e As EventArgs) Handles SliderSTF.Scroll
        If SliderSTF.Value = 1 Then LBLStf.Text = "Preoccupied"
        If SliderSTF.Value = 2 Then LBLStf.Text = "Distracted"
        If SliderSTF.Value = 3 Then LBLStf.Text = "Normal"
        If SliderSTF.Value = 4 Then LBLStf.Text = "Talkative"
        If SliderSTF.Value = 5 Then LBLStf.Text = "Verbose"

    End Sub

    Private Sub TauntSlider_Scroll(sender As Object, e As EventArgs) Handles TauntSlider.Scroll
        If TauntSlider.Value = 1 Then LBLVtf.Text = "Preoccupied"
        If TauntSlider.Value = 2 Or TauntSlider.Value = 3 Then LBLVtf.Text = "Distracted"
        If TauntSlider.Value = 4 Or TauntSlider.Value = 5 Then LBLVtf.Text = "Normal"
        If TauntSlider.Value = 6 Or TauntSlider.Value = 7 Or TauntSlider.Value = 8 Then LBLVtf.Text = "Talkative"
        If TauntSlider.Value = 9 Or TauntSlider.Value = 10 Then LBLVtf.Text = "Verbose"

    End Sub

#Region "Lost focus / save values"
    Private Sub TauntSlider_LostFocus(sender As Object, e As EventArgs) Handles TauntSlider.LostFocus
        My.Settings.TimerVTF = TauntSlider.Value

    End Sub

    Private Sub SliderSTF_LostFocus(sender As Object, e As EventArgs) Handles SliderSTF.LostFocus
        My.Settings.TimerSTF = SliderSTF.Value

    End Sub

    Private Sub NBWritingTaskMin_LostFocus(sender As Object, e As EventArgs) Handles NBWritingTaskMin.LostFocus
        My.Settings.NBWritingTaskMin = NBWritingTaskMin.Value
    End Sub

    Private Sub NBWritingTaskMin_ValueChanged(sender As Object, e As EventArgs) Handles NBWritingTaskMin.ValueChanged
        If NBWritingTaskMin.Value > NBWritingTaskMax.Value Then NBWritingTaskMin.Value = NBWritingTaskMax.Value
    End Sub

    Private Sub NBWritingTaskMax_LostFocus(sender As Object, e As EventArgs) Handles NBWritingTaskMax.LostFocus
        My.Settings.NBWritingTaskMax = NBWritingTaskMax.Value
    End Sub

    Private Sub NBWritingTaskMax_ValueChanged(sender As Object, e As EventArgs) Handles NBWritingTaskMax.ValueChanged
        If NBWritingTaskMax.Value < NBWritingTaskMin.Value Then NBWritingTaskMax.Value = NBWritingTaskMin.Value
    End Sub

    Private Sub BTNTagDir_Click(sender As Object, e As EventArgs) Handles BTNTagDir.Click

        Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            ImageTagDir.Clear()

            TBTagDir.Text = folderBrowserDialog.SelectedPath

            Dim supportedExtensions As String = "*.png,*.jpg,*.gif,*.bmp,*.jpeg"
            Dim files As String() = myDirectory.GetFiles(TBTagDir.Text, "*.*")

            Array.Sort(files)

            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    ImageTagDir.Add(fi)
                End If
            Next

            If ImageTagDir.Count < 1 Then
                MessageBox.Show(Me, "There are no images in the specified folder.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            ImageTagPictureBox.Image = Image.FromFile(ImageTagDir(0))
            MainWindow.mainPictureBox.LoadAsync(ImageTagDir(0))
            CurrentImageTagImage = ImageTagDir(0)

            Dim taggedItem As TaggedItem = GetTaggedItem(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)
            SetDommeTagCheckboxes(taggedItem.ItemTags)

            TagCount = 1
            LBLTagCount.Text = TagCount & "/" & ImageTagDir.Count

            'If ImageTagDir.Count = 1 Then BTNTagSave.Text = "Save and Finish"

            ImageTagCount = 0

            BTNTagSave.Enabled = True
            BTNTagNext.Enabled = True
            BTNTagPrevious.Enabled = False
            BTNTagDir.Enabled = False
            TBTagDir.Enabled = False

            SetDommeTagCheckboxesEnabled(True)
            LBLTagCount.Enabled = True
        End If
    End Sub

    Private Sub TBTagDir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBTagDir.KeyPress
        If e.KeyChar <> Convert.ToChar(13) Then
            Return
        End If

        e.Handled = True
        e.KeyChar = Chr(0)

        Dim getImages As Result(Of List(Of String)) = GetImagesInFolder(TBTagDir.Text) _
            .Ensure(Function(data) data.Any(), TBTagDir.Text & " does not have any images in it.")

        LocalImageTagDir = getImages.GetResultOrDefault(New List(Of String))

        If Not getImages.IsFailure() Then
            MessageBox.Show(Me, getImages.GetErrorMessageOrDefault(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If


        CurrentImageTagImage = ImageTagDir.First()
        ImageTagPictureBox.Image = Image.FromFile(CurrentImageTagImage)
        MainWindow.mainPictureBox.LoadAsync(CurrentImageTagImage)

        Dim taggedItem As TaggedItem = GetTaggedItem(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)
        SetDommeTagCheckboxes(taggedItem.ItemTags)

        TagCount = 1
        LBLTagCount.Text = TagCount & "/" & ImageTagDir.Count

        ImageTagCount = 0
        BTNTagSave.Enabled = True
        BTNTagNext.Enabled = TagCount < ImageTagDir.Count
        BTNTagPrevious.Enabled = TagCount > 1
        BTNTagDir.Enabled = False
        TBTagDir.Enabled = False
        SetDommeTagCheckboxesEnabled(True)
    End Sub

    Private Sub BTNTagSave_Click(sender As Object, e As EventArgs) Handles BTNTagSave.Click

        SaveDommeTags(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)

        BTNTagDir.Enabled = True
        TBTagDir.Enabled = True
        BTNTagSave.Enabled = False
        BTNTagNext.Enabled = False
        BTNTagPrevious.Enabled = False




        ' If BTNTagSave.Text = "Save and Finish" Then
        'BTNTagSave.Text = "Save and Display Next Image"
        'BTNTagSave.Enabled = False
        'MessageBox.Show(Me, "All images in this folder have been successfully tagged.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'ImageTagPictureBox.Image = Nothing
        'Return
        'End If

        SetDommeTagCheckboxes(New List(Of ItemTag)())
        SetDommeTagCheckboxesEnabled(False)

        LBLTagCount.Text = "0/0"
        LBLTagCount.Enabled = False

        ImageTagPictureBox.Image = Nothing
    End Sub

    Private Sub BTNTagNext_Click(sender As Object, e As EventArgs) Handles BTNTagNext.Click
        TagCount += 1
        LBLTagCount.Text = TagCount & "/" & ImageTagDir.Count

        SaveDommeTags(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)

        ImageTagCount += 1

        CurrentImageTagImage = ImageTagDir(ImageTagCount - 1)
        ImageTagPictureBox.Image = Image.FromFile(CurrentImageTagImage)
        MainWindow.mainPictureBox.LoadAsync(CurrentImageTagImage)

        BTNTagNext.Enabled = TagCount < ImageTagDir.Count
        BTNTagPrevious.Enabled = TagCount > 1

        Dim taggedItem As TaggedItem = GetTaggedItem(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)
        SetDommeTagCheckboxes(taggedItem.ItemTags)
    End Sub

    Private Sub BTNTagPrevious_Click(sender As Object, e As EventArgs) Handles BTNTagPrevious.Click

        TagCount -= 1
        LBLTagCount.Text = TagCount & "/" & ImageTagDir.Count
        BTNTagNext.Enabled = True


        SaveDommeTags(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)

        ImageTagCount -= 1

        Try
            ImageTagPictureBox.Image.Dispose()
        Catch
        End Try

        ImageTagPictureBox.Image = Nothing
        GC.Collect()

        ImageTagPictureBox.Image = Image.FromFile(ImageTagDir(ImageTagCount))
        MainWindow.mainPictureBox.LoadAsync(ImageTagDir(ImageTagCount))
        CurrentImageTagImage = ImageTagDir(ImageTagCount)

        BTNTagPrevious.Enabled = ImageTagCount > 0
        Dim taggedItem As TaggedItem = GetTaggedItem(TBTagDir.Text & "\ImageTags.txt", CurrentImageTagImage)
        SetDommeTagCheckboxes(taggedItem.ItemTags)
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click

        TBKeyWords.Text = ""
        RTBKeyWords.Text = ""

        Dim files() As String = myDirectory.GetFiles(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\")

        LBKeyWords.Items.Clear()

        For Each file As String In files
            LBKeyWords.Items.Add(Path.GetFileName(file).Replace(".txt", ""))
        Next

    End Sub

    Private Sub LBKeyWords_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBKeyWords.SelectedIndexChanged

        Dim KeyWordPath As String = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & LBKeyWords.SelectedItem & ".txt"

        If Not File.Exists(KeyWordPath) Then Return

        ' If GlitPath = StatusText Then
        'MsgBox("This file is currently in use by the program. Saving changes may be slow until the Glitter process has finished.", , "Warning!")
        'End If


        TBKeyWords.Text = LBKeyWords.SelectedItem

        RTBKeyWords.Text = ""


        Dim ioFile As New StreamReader(KeyWordPath)
        Dim lines As New List(Of String)

        Dim KeyWordCount As Integer
        Dim KeyWordEnd As Integer

        KeyWordCount = -1

        While ioFile.Peek <> -1
            KeyWordCount += 1
            lines.Add(ioFile.ReadLine())
        End While


        KeyWordEnd = KeyWordCount
        KeyWordCount = 0



        Do
            RTBKeyWords.Text = RTBKeyWords.Text & lines(KeyWordCount) & Environment.NewLine
            KeyWordCount += 1
        Loop Until KeyWordCount = KeyWordEnd + 1

        ioFile.Close()
        ioFile.Dispose()

        Debug.Print(RTBKeyWords.Lines.Count)

    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click

        Try
            If TBKeyWords.Text = "" Or InStr(TBKeyWords.Text, "#") <> 1 Or Not TBKeyWords.Text.Substring(0, 1) = "#" Then
                MessageBox.Show(Me, "Please enter a correct file name for this Keyword script!" & Environment.NewLine & Environment.NewLine & "Keyword file names must contain one ""#"" sign, " &
                                "placed at the beginning of the word or phrase.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If
        Catch
            MessageBox.Show(Me, "Please enter a file name for this Keyword script!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End Try


        If RTBKeyWords.Text = "" Then
            MessageBox.Show(Me, "The Keyword file you are attempting to save is blank!" & Environment.NewLine & Environment.NewLine & "Please add some lines before saving.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        Dim KeyWordSaveDir As String = TBKeyWords.Text
        KeyWordSaveDir = KeyWordSaveDir.Replace(".txt", "")

        If Not LBKeyWords.Items.Contains(KeyWordSaveDir) Then
            LBKeyWords.Items.Add(KeyWordSaveDir)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & KeyWordSaveDir & ".txt", RTBKeyWords.Text, False)
            File.WriteAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & KeyWordSaveDir & ".txt", File.ReadAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & KeyWordSaveDir & ".txt").Where(Function(s) s <> String.Empty))
        Else
            Dim Result As Integer = MessageBox.Show(Me, KeyWordSaveDir & " already exists!" & Environment.NewLine & Environment.NewLine & "Do you wish to overwrite?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If Result = DialogResult.Yes Then
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & KeyWordSaveDir & ".txt", RTBKeyWords.Text, False)
                File.WriteAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & KeyWordSaveDir & ".txt", File.ReadAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\" & KeyWordSaveDir & ".txt").Where(Function(s) s <> String.Empty))
            Else
                Debug.Print("Did not work")
                Return
            End If
        End If

        MessageBox.Show(Me, "Keyword file has been saved!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub TBGreeting_LostFocus(sender As Object, e As EventArgs) Handles TBGreeting.LostFocus
        My.Settings.SubGreeting = TBGreeting.Text
    End Sub

    Private Sub TBYes_LostFocus(sender As Object, e As EventArgs) Handles TBYes.LostFocus
        My.Settings.SubYes = TBYes.Text
    End Sub

    Private Sub TBNo_LostFocus(sender As Object, e As EventArgs) Handles TBNo.LostFocus
        My.Settings.SubNo = TBNo.Text
    End Sub

    Private Sub TBHonorific_LostFocus(sender As Object, e As EventArgs) Handles TBHonorific.LostFocus
        If TBHonorific.Text = "" Or TBHonorific.Text Is Nothing Then TBHonorific.Text = "Mistress"
        My.Settings.SubHonorific = TBHonorific.Text
    End Sub

    Private Sub CBHonorificInclude_LostFocus(sender As Object, e As EventArgs) Handles CBHonorificInclude.LostFocus
        If CBHonorificInclude.Checked Then
            My.Settings.CBUseHonor = True
        Else
            My.Settings.CBUseHonor = False
        End If
    End Sub

    Private Sub CBHonorificCapitalized_LostFocus(sender As Object, e As EventArgs) Handles CBHonorificCapitalized.LostFocus
        If CBHonorificCapitalized.Checked Then
            My.Settings.CBCapHonor = True
        Else
            My.Settings.CBCapHonor = False
        End If
    End Sub

    Private Sub subAgeNumBox_LostFocus(sender As Object, e As EventArgs) Handles subAgeNumBox.LostFocus
        My.Settings.SubAge = subAgeNumBox.Value
    End Sub

    Private Sub NBDomBirthdayMonth_LostFocus(sender As Object, e As EventArgs) Handles NBDomBirthdayMonth.LostFocus
        My.Settings.DomBirthMonth = NBDomBirthdayMonth.Value
    End Sub

    Private Sub NBDomBirthdayDay_LostFocus(sender As Object, e As EventArgs) Handles NBDomBirthdayDay.LostFocus
        My.Settings.DomBirthDay = NBDomBirthdayDay.Value
    End Sub

    Private Sub NBBirthdayMonth_LostFocus(sender As Object, e As EventArgs) Handles NBBirthdayMonth.LostFocus
        My.Settings.SubBirthMonth = NBBirthdayMonth.Value
    End Sub

    Private Sub NBBirthdayDay_LostFocus(sender As Object, e As EventArgs) Handles NBBirthdayDay.LostFocus
        My.Settings.SubBirthDay = NBBirthdayDay.Value
    End Sub

    Private Sub TBSubHairColor_LostFocus(sender As Object, e As EventArgs) Handles TBSubHairColor.LostFocus
        My.Settings.SubHair = TBSubHairColor.Text
    End Sub

    Private Sub TBSubEyeColor_LostFocus(sender As Object, e As EventArgs) Handles TBSubEyeColor.LostFocus
        My.Settings.SubEyes = TBSubEyeColor.Text
    End Sub
#End Region

    Private Sub Button37_Click_1(sender As Object, e As EventArgs) Handles Button37.Click
        If TBKeywordPreview.Text = "" Then Return

        LBLKeywordPreview.Text = MainWindow.PoundClean(TBKeywordPreview.Text)
    End Sub

    Private Sub NBBirthdayMonth_ValueChanged(sender As Object, e As EventArgs) Handles NBBirthdayMonth.MouseLeave

        If NBBirthdayMonth.Value = 2 And NBBirthdayDay.Value > 28 Then
            NBBirthdayDay.Value = 28
        End If

        If NBBirthdayMonth.Value = 4 Or NBBirthdayMonth.Value = 6 Or NBBirthdayMonth.Value = 9 Or NBBirthdayMonth.Value = 11 Then
            If NBBirthdayDay.Value > 30 Then
                NBBirthdayDay.Value = 30
            End If
            NBBirthdayDay.Maximum = 30
        Else
            NBBirthdayDay.Maximum = 31
        End If

        If NBBirthdayMonth.Value = 2 Then
            NBBirthdayDay.Maximum = 28
        End If

    End Sub

    Private Sub NBDomBirthdayMonth_ValueChanged(sender As Object, e As EventArgs) Handles NBDomBirthdayMonth.MouseLeave

        If NBDomBirthdayMonth.Value = 2 And NBDomBirthdayDay.Value > 28 Then
            NBDomBirthdayDay.Value = 28
        End If

        If NBDomBirthdayMonth.Value = 4 Or NBDomBirthdayMonth.Value = 6 Or NBDomBirthdayMonth.Value = 9 Or NBDomBirthdayMonth.Value = 11 Then
            If NBDomBirthdayDay.Value > 30 Then
                NBDomBirthdayDay.Value = 30
            End If
            NBDomBirthdayDay.Maximum = 30
        Else
            NBDomBirthdayDay.Maximum = 31
        End If

        If NBDomBirthdayMonth.Value = 2 Then
            NBDomBirthdayDay.Maximum = 28
        End If

    End Sub

    Function InstrCount(StringToSearch As String,
           StringToFind As String) As Long

        If Len(StringToFind) Then
            InstrCount = UBound(Split(StringToSearch, StringToFind))
        End If

        Return InstrCount
    End Function

    Private Sub TBTagDir_MouseClick(sender As Object, e As Windows.Forms.MouseEventArgs) Handles TBTagDir.MouseClick
        TBTagDir.SelectionStart = 0
        TBTagDir.SelectionLength = Len(TBTagDir.Text)
    End Sub

    Private Sub TBWIDirectory_MouseClick(sender As Object, e As Windows.Forms.MouseEventArgs) Handles TBWIDirectory.MouseClick
        TBWIDirectory.SelectionStart = 0
        TBWIDirectory.SelectionLength = Len(TBWIDirectory.Text)
    End Sub

    Private Sub SaveURLFileSelection()
        If FrmSettingsLoading Then Return

        Dim blogMetaData As List(Of BlogMetaData) = New List(Of BlogMetaData)()
        For i = 0 To URLFileList.Items.Count - 1
            blogMetaData.Add(New BlogMetaData With {
                             .FileName = CStr(URLFileList.Items(i)),
                             .IsEnabled = URLFileList.GetItemChecked(i)
                             })
        Next
        myBlogAccessor.SaveBlogMetaData(blogMetaData)
    End Sub

    Private Sub URLFileList_LostFocus(sender As Object, e As EventArgs) Handles URLFileList.LostFocus
        SaveURLFileSelection()
    End Sub

    Private Sub URLFileList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles URLFileList.SelectedIndexChanged
        If CBURLPreview.Checked Then
            Dim blogMetaData = myBlogAccessor.GetBlogMetaData().First(Function(x) x.FileName.Equals(URLFileList.SelectedItem.ToString()))
            Dim imageMetaDatas = myBlogAccessor.GetImageMetaData(blogMetaData)
            Dim imageUrl As ImageMetaData = imageMetaDatas(MainWindow.ssh.randomizer.Next(0, imageMetaDatas.Count))
            PBURLPreview.Image = New Bitmap(New MemoryStream(New Net.WebClient().DownloadData(imageUrl.ItemName)))
        End If

        SaveURLFileSelection()
    End Sub

    Private Sub BTNURLFilesAll_Click(sender As Object, e As EventArgs) Handles BTNURLFilesAll.Click
        For i As Integer = 0 To URLFileList.Items.Count - 1
            URLFileList.SetItemChecked(i, True)
        Next
        SaveURLFileSelection()
    End Sub

    Private Sub BTNURLFilesNone_Click(sender As Object, e As EventArgs) Handles BTNURLFilesNone.Click
        For i As Integer = 0 To URLFileList.Items.Count - 1
            URLFileList.SetItemChecked(i, False)
        Next
        SaveURLFileSelection()
    End Sub

    Private Sub CBCBTCock_CheckedChanged(sender As Object, e As EventArgs) Handles CockTortureEnabledCB.LostFocus
        mySettingsAccessor.IsCockTortureEnabled = CockTortureEnabledCB.Checked
    End Sub

    Private Sub CBCBTBalls_CheckedChanged(sender As Object, e As EventArgs) Handles BallTortureEnabledCB.LostFocus
        mySettingsAccessor.IsBallTortureEnabled = BallTortureEnabledCB.Checked
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles BTNLocalTagDir.Click
        Dim folderBrowser As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowser.ShowDialog() = DialogResult.OK) Then
            LocalImageTagDir.Clear()

            Dim tagLocalImageFolder As String = folderBrowser.SelectedPath
            Dim supportedExtensions As String = "*.png,*.jpg,*.gif,*.bmp,*.jpeg"
            Dim files As List(Of String) = myDirectory.GetFiles(tagLocalImageFolder, "*.*").ToList()
            files.Sort()

            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(fi.ToLower())) Then
                    LocalImageTagDir.Add(fi)
                End If
            Next

            If Not LocalImageTagDir.Any() Then
                MessageBox.Show(Me, "There are no images in the specified folder.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            CurrentLocalImageTagImage = LocalImageTagDir(0)
            MainWindow.mainPictureBox.LoadAsync(CurrentLocalImageTagImage)

            Dim taggedItem As TaggedItem = GetTaggedItem(LocalImageTagFile, CurrentLocalImageTagImage)
            SetLocalTagCheckboxes(taggedItem.ItemTags)

            LocalTagCount = 1
            LBLLocalTagCount.Text = LocalTagCount & "/" & LocalImageTagDir.Count

            BTNLocalTagSave.Enabled = True
            BTNLocalTagNext.Enabled = LocalImageTagDir.Count < LocalTagCount
            BTNLocalTagPrevious.Enabled = False
            BTNLocalTagDir.Enabled = False
            TBLocalTagDir.Enabled = False

            SetLocalImageEnabled(True)

            LBLLocalTagCount.Enabled = True
        End If
    End Sub

    Private Sub BTNLocalTagNext_Click(sender As Object, e As EventArgs) Handles BTNLocalTagNext.Click

        LocalTagCount += 1
        LBLLocalTagCount.Text = LocalTagCount & "/" & LocalImageTagDir.Count
        BTNLocalTagPrevious.Enabled = LocalTagCount > 1

        SaveLocalImageTags(CurrentLocalImageTagImage)

        MainWindow.mainPictureBox.LoadAsync(LocalImageTagDir(LocalTagCount - 1))
        CurrentLocalImageTagImage = LocalImageTagDir(LocalTagCount - 1)

        BTNLocalTagNext.Enabled = LocalImageTagDir.Count < LocalTagCount

        Dim taggedItem As TaggedItem = GetTaggedItem(LocalImageTagFile, CurrentLocalImageTagImage)
        SetLocalTagCheckboxes(taggedItem.ItemTags)

    End Sub

    Private Sub NBLongEdge_ValueChanged(sender As Object, e As EventArgs) Handles NBLongEdge.LostFocus
        My.Settings.LongEdge = NBLongEdge.Value
    End Sub

    Private Sub NBHoldTheEdgeMax_LostFocus(sender As Object, e As EventArgs) Handles HoldEdgeMaximum.LostFocus
        mySettingsAccessor.HoldEdgeMaximum = ConvertHoldTime(HoldEdgeMaximum.Value, LBLMaxHold.Text)
    End Sub

    Private Sub NBHoldTheEdgeMin_LostFocus(sender As Object, e As EventArgs) Handles HoldEdgeMinimum.LostFocus
        mySettingsAccessor.HoldEdgeMaximum = ConvertHoldTime(HoldEdgeMinimum.Value, HoldEdgeMinimumUnits.Text)
    End Sub

    Private Sub NBHoldTheEdgeMax_ValueChanged(sender As Object, e As EventArgs) Handles HoldEdgeMaximum.ValueChanged
        If FrmSettingsLoading = False Then

            If HoldEdgeMaximum.Value = 0 And LBLMaxHold.Text = "minutes" Then
                HoldEdgeMaximum.Value = 59
                LBLMaxHold.Text = "seconds"
                Return
            End If

            If HoldEdgeMaximum.Value = 60 And LBLMaxHold.Text = "seconds" Then
                HoldEdgeMaximum.Value = 1
                LBLMaxHold.Text = "minutes"
                Return
            End If
        End If
    End Sub

    Private Sub NBHoldTheEdgeMin_ValueChanged(sender As Object, e As EventArgs) Handles HoldEdgeMinimum.ValueChanged
        If FrmSettingsLoading = False Then

            If HoldEdgeMinimum.Value = 0 And HoldEdgeMinimumUnits.Text = "minutes" Then
                HoldEdgeMinimum.Value = 59
                HoldEdgeMinimumUnits.Text = "seconds"
                Return
            End If

            If HoldEdgeMinimum.Value = 60 And HoldEdgeMinimumUnits.Text = "seconds" Then
                HoldEdgeMinimum.Value = 1
                HoldEdgeMinimumUnits.Text = "minutes"
                Return
            End If
        End If
    End Sub

    Private Sub NBLongHoldMax_LostFocus(sender As Object, e As EventArgs) Handles LongEdgeHoldMaximum.LostFocus
        mySettingsAccessor.LongHoldEdgeMaximum = LongEdgeHoldMaximum.Value
    End Sub

    Private Sub NBLongHoldMin_LostFocus(sender As Object, e As EventArgs) Handles LongEdgeHoldMinimum.LostFocus
        mySettingsAccessor.LongHoldEdgeMinimum = LongEdgeHoldMinimum.Value
    End Sub

    Private Sub NBLongHoldMax_ValueChanged(sender As Object, e As EventArgs) Handles LongEdgeHoldMaximum.ValueChanged
        If FrmSettingsLoading = False Then
            If LongEdgeHoldMaximum.Value = 1 Then
                LBLMaxLongHold.Text = "minute"
            Else
                LBLMaxLongHold.Text = "minutes"
            End If
        End If
    End Sub

    Private Sub NBLongHoldMin_ValueChanged(sender As Object, e As EventArgs) Handles LongEdgeHoldMinimum.ValueChanged
        If FrmSettingsLoading = False Then
            If LongEdgeHoldMinimum.Value = 1 Then
                LBLMinLongHold.Text = "minute"
            Else
                LBLMinLongHold.Text = "minutes"
            End If
        End If
    End Sub

    Private Sub NBExtremeHoldMax_LostFocus(sender As Object, e As EventArgs) Handles ExtremeEdgeHoldMaximum.LostFocus
        mySettingsAccessor.ExtremeHoldEdgeMaximum = ExtremeEdgeHoldMaximum.Value
    End Sub

    Private Sub NBExtremeHoldMin_LostFocus(sender As Object, e As EventArgs) Handles ExtremeEdgeHoldMinimum.LostFocus
        mySettingsAccessor.ExtremeHoldEdgeMinimum = ExtremeEdgeHoldMinimum.Value
    End Sub

    Private Sub NBExtremeHoldMax_ValueChanged(sender As Object, e As EventArgs) Handles ExtremeEdgeHoldMaximum.ValueChanged
        If FrmSettingsLoading = False Then
            If ExtremeEdgeHoldMaximum.Value = 1 Then
                LBLMaxExtremeHold.Text = "minute"
            Else
                LBLMaxExtremeHold.Text = "minutes"
            End If
        End If
    End Sub

    Private Sub NBExtremeHoldMin_ValueChanged(sender As Object, e As EventArgs) Handles ExtremeEdgeHoldMinimum.ValueChanged
        If FrmSettingsLoading = False Then
            If ExtremeEdgeHoldMinimum.Value = 1 Then
                LBLMinExtremeHold.Text = "minute"
            Else
                LBLMinExtremeHold.Text = "minutes"
            End If
        End If
    End Sub

    Private Sub CBTSlider_Scroll(sender As Object, e As EventArgs) Handles CockAndBallTortureLevelSlider.Scroll
        mySettingsAccessor.CockAndBallTortureLevel = TortureLevel.Create(CockAndBallTortureLevelSlider.Value).Value
        CockAndBallTortureLevelLbl.Text = "CBT Level: " & CockAndBallTortureLevelSlider.Value.ToString()
    End Sub

    Private Sub CBSubCircumcised_CheckedChanged(sender As Object, e As EventArgs) Handles CBSubCircumcised.CheckedChanged
        mySettingsAccessor.IsSubCircumcised = CBSubCircumcised.Checked
    End Sub

    Private Sub CBSubPierced_CheckedChanged(sender As Object, e As EventArgs) Handles CBSubPierced.CheckedChanged
        mySettingsAccessor.IsSubPierced = CBSubPierced.Checked
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles BTNSaveDomSet.Click

        SaveSettingsDialog.Title = "Select a location to save current Domme settings"
        SaveSettingsDialog.InitialDirectory = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\System\"
        SaveSettingsDialog.FileName = MainWindow.dompersonalitycombobox.Text & " Domme Settings"

        If SaveSettingsDialog.ShowDialog() = DialogResult.OK Then
            Dim SettingsPath As String = SaveSettingsDialog.FileName
            Dim SettingsList As New List(Of String)
            SettingsList.Clear()

            SettingsList.Add("Level: " & DominationLevel.Value)
            SettingsList.Add("Empathy: " & NBEmpathy.Value)
            SettingsList.Add("Age: " & domageNumBox.Value)
            SettingsList.Add("Birth Month: " & NBDomBirthdayMonth.Value)
            SettingsList.Add("Birth Day: " & NBDomBirthdayDay.Value)
            SettingsList.Add("Hair Color: " & TBDomHairColor.Text)
            SettingsList.Add("Hair Length: " & domhairlengthComboBox.Text)
            SettingsList.Add("Eye Color: " & TBDomEyeColor.Text)
            SettingsList.Add("Cup Size: " & boobComboBox.Text)
            SettingsList.Add("Pubic Hair: " & dompubichairComboBox.Text)
            SettingsList.Add("Tattoos: " & CBDomTattoos.Checked)
            SettingsList.Add("Freckles: " & CBDomFreckles.Checked)

            SettingsList.Add("Personality: " & MainWindow.dompersonalitycombobox.Text)
            SettingsList.Add("Crazy: " & crazyCheckBox.Checked)
            SettingsList.Add("Vulgar: " & vulgarCheckBox.Checked)
            SettingsList.Add("Supremacist: " & supremacistCheckBox.Checked)
            SettingsList.Add("Pet Name 1: " & PetNameBox1.Text)
            SettingsList.Add("Pet Name 2: " & petnameBox2.Text)
            SettingsList.Add("Pet Name 3: " & petnameBox3.Text)
            SettingsList.Add("Pet Name 4: " & petnameBox4.Text)
            SettingsList.Add("Pet Name 5: " & petnameBox5.Text)
            SettingsList.Add("Pet Name 6: " & petnameBox6.Text)
            SettingsList.Add("Pet Name 7: " & petnameBox7.Text)
            SettingsList.Add("Pet Name 8: " & petnameBox8.Text)

            SettingsList.Add("Allows Orgasms: " & alloworgasmComboBox.Text)
            SettingsList.Add("Ruins Orgasms: " & ruinorgasmComboBox.Text)
            SettingsList.Add("Denial Ends: " & CBDomDenialEnds.Checked)
            SettingsList.Add("Orgasm Ends: " & CBDomOrgasmEnds.Checked)
            SettingsList.Add("P.O.T.: NULL")
            SettingsList.Add("All Lowercase: " & LCaseCheckBox.Checked)
            SettingsList.Add("No Apostrophes: " & apostropheCheckBox.Checked)
            SettingsList.Add("No Commas: " & commaCheckBox.Checked)
            SettingsList.Add("No Periods: " & periodCheckBox.Checked)
            SettingsList.Add("Me/My/Mine: " & CBMeMyMine.Checked)
            SettingsList.Add("Emotes: " & "NULL")

            SettingsList.Add("DommeMoodMin: " & NBDomMoodMin.Value)
            SettingsList.Add("DommeMoodMax: " & NBDomMoodMax.Value)
            SettingsList.Add("AvgCockSizeMin: " & NBAvgCockMin.Value)
            SettingsList.Add("AvgCockSizeMax: " & NBAvgCockMax.Value)
            SettingsList.Add("SelfAgeMin: " & NBSelfAgeMin.Value)
            SettingsList.Add("SelfAgeMax: " & NBSelfAgeMax.Value)
            SettingsList.Add("SubAgeMin: " & NBSubAgeMin.Value)
            SettingsList.Add("SubAgeMax: " & NBSubAgeMax.Value)

            SettingsList.Add("Emote Start: " & TBEmote.Text)
            SettingsList.Add("Emote End: " & TBEmoteEnd.Text)

            SettingsList.Add("Sadistic: " & sadisticCheckBox.Checked)
            SettingsList.Add("Degrading: " & degradingCheckBox.Checked)

            SettingsList.Add("Typo Chance: " & NBTypoChance.Value)

            Dim SettingsString As String = ""

            For i As Integer = 0 To SettingsList.Count - 1
                SettingsString = SettingsString & SettingsList(i)
                If i <> SettingsList.Count - 1 Then SettingsString = SettingsString & Environment.NewLine
            Next

            My.Computer.FileSystem.WriteAllText(SettingsPath, SettingsString, False)
        End If

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles BTNLoadDomSet.Click

        OpenSettingsDialog.Title = "Select a Domme settings file"
        OpenSettingsDialog.InitialDirectory = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\System\"

        If OpenSettingsDialog.ShowDialog() = DialogResult.OK Then

            Dim SettingsList As New List(Of String)

            Try
                Dim SettingsReader As New StreamReader(OpenSettingsDialog.FileName)
                While SettingsReader.Peek <> -1
                    SettingsList.Add(SettingsReader.ReadLine())
                End While
                SettingsReader.Close()
                SettingsReader.Dispose()
            Catch ex As Exception
                MessageBox.Show(Me, "This file could not be opened!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End Try

            Try
                DominationLevel.Value = SettingsList(0).Replace("Level: ", "")
                NBEmpathy.Value = SettingsList(1).Replace("Empathy: ", "")
                domageNumBox.Value = SettingsList(2).Replace("Age: ", "")
                NBDomBirthdayMonth.Value = SettingsList(3).Replace("Birth Month: ", "")
                NBDomBirthdayDay.Value = SettingsList(4).Replace("Birth Day: ", "")
                TBDomHairColor.Text = SettingsList(5).Replace("Hair Color: ", "")
                domhairlengthComboBox.Text = SettingsList(6).Replace("Hair Length: ", "")
                TBDomEyeColor.Text = SettingsList(7).Replace("Eye Color: ", "")
                boobComboBox.Text = SettingsList(8).Replace("Cup Size: ", "")
                dompubichairComboBox.Text = SettingsList(9).Replace("Pubic Hair: ", "")
                CBDomTattoos.Checked = SettingsList(10).Replace("Tattoos: ", "")
                CBDomFreckles.Checked = SettingsList(11).Replace("Freckles: ", "")

                MainWindow.dompersonalitycombobox.Text = SettingsList(12).Replace("Personality: ", "")
                crazyCheckBox.Checked = SettingsList(13).Replace("Crazy: ", "")
                vulgarCheckBox.Checked = SettingsList(14).Replace("Vulgar: ", "")
                supremacistCheckBox.Checked = SettingsList(15).Replace("Supremacist: ", "")
                PetNameBox1.Text = SettingsList(16).Replace("Pet Name 1: ", "")
                petnameBox2.Text = SettingsList(17).Replace("Pet Name 2: ", "")
                petnameBox3.Text = SettingsList(18).Replace("Pet Name 3: ", "")
                petnameBox4.Text = SettingsList(19).Replace("Pet Name 4: ", "")
                petnameBox5.Text = SettingsList(20).Replace("Pet Name 5: ", "")
                petnameBox6.Text = SettingsList(21).Replace("Pet Name 6: ", "")
                petnameBox7.Text = SettingsList(22).Replace("Pet Name 7: ", "")
                petnameBox8.Text = SettingsList(23).Replace("Pet Name 8: ", "")

                alloworgasmComboBox.Text = SettingsList(24).Replace("Allows Orgasms: ", "")
                ruinorgasmComboBox.Text = SettingsList(25).Replace("Ruins Orgasms: ", "")
                CBDomDenialEnds.Checked = SettingsList(26).Replace("Denial Ends: ", "")
                CBDomOrgasmEnds.Checked = SettingsList(27).Replace("Orgasm Ends: ", "")
                'CBDomPOT.Checked = SettingsList(28).Replace("P.O.T.: NULL", "")
                LCaseCheckBox.Checked = SettingsList(29).Replace("All Lowercase: ", "")
                apostropheCheckBox.Checked = SettingsList(30).Replace("No Apostrophes: ", "")
                commaCheckBox.Checked = SettingsList(31).Replace("No Commas: ", "")
                periodCheckBox.Checked = SettingsList(32).Replace("No Periods: ", "")
                CBMeMyMine.Checked = SettingsList(33).Replace("Me/My/Mine: ", "")
                'domemoteComboBox.Text = SettingsList(34).Replace("Emotes: ", "")

                NBDomMoodMin.Value = SettingsList(35).Replace("DommeMoodMin: ", "")
                NBDomMoodMax.Value = SettingsList(36).Replace("DommeMoodMax: ", "")
                NBAvgCockMin.Value = SettingsList(37).Replace("AvgCockSizeMin: ", "")
                NBAvgCockMax.Value = SettingsList(38).Replace("AvgCockSizeMax: ", "")
                NBSelfAgeMin.Value = SettingsList(39).Replace("SelfAgeMin: ", "")
                NBSelfAgeMax.Value = SettingsList(40).Replace("SelfAgeMax: ", "")
                NBSubAgeMin.Value = SettingsList(41).Replace("SubAgeMin: ", "")
                NBSubAgeMax.Value = SettingsList(42).Replace("SubAgeMax: ", "")


                TBEmote.Text = SettingsList(43).Replace("Emote Start: ", "")
                TBEmoteEnd.Text = SettingsList(44).Replace("Emote End: ", "")

                sadisticCheckBox.Checked = SettingsList(45).Replace("Sadistic: ", "")
                degradingCheckBox.Checked = SettingsList(46).Replace("Degrading: ", "")

                NBTypoChance.Value = SettingsList(47).Replace("Typo Chance: ", "")


                SaveDommeSettings()
            Catch
                MessageBox.Show(Me, "This settings file is invalid or has been edited incorrectly!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                LoadDommeSettings()
            End Try




        End If
    End Sub

    Public Sub SaveDommeSettings()
        mySettingsAccessor.DominationLevel = DomLevel.Create(Convert.ToInt32(DominationLevel.Value)).Value
        mySettingsAccessor.ApathyLevel = ApathyLevel.Create(Convert.ToInt32(NBEmpathy.Value)).Value
        My.Settings.DomAge = domageNumBox.Value
        My.Settings.DomBirthMonth = NBDomBirthdayMonth.Value
        My.Settings.DomBirthDay = NBDomBirthdayDay.Value
        My.Settings.DomHair = TBDomHairColor.Text
        My.Settings.DomHairLength = domhairlengthComboBox.Text
        My.Settings.DomEyes = TBDomEyeColor.Text
        My.Settings.DomCup = boobComboBox.Text
        My.Settings.DomPubicHair = dompubichairComboBox.Text
        My.Settings.DomTattoos = CBDomTattoos.Checked
        My.Settings.DomFreckles = CBDomFreckles.Checked

        mySettingsAccessor.DommePersonality = MainWindow.dompersonalitycombobox.Text
        My.Settings.DomCrazy = crazyCheckBox.Checked
        My.Settings.DomVulgar = vulgarCheckBox.Checked
        My.Settings.DomSupremacist = supremacistCheckBox.Checked
        My.Settings.DomSadistic = sadisticCheckBox.Checked
        My.Settings.DomDegrading = degradingCheckBox.Checked
        My.Settings.pnSetting1 = PetNameBox1.Text
        My.Settings.pnSetting2 = petnameBox2.Text
        My.Settings.pnSetting3 = petnameBox3.Text
        My.Settings.pnSetting4 = petnameBox4.Text
        My.Settings.pnSetting5 = petnameBox5.Text
        My.Settings.pnSetting6 = petnameBox6.Text
        My.Settings.pnSetting7 = petnameBox7.Text
        My.Settings.pnSetting8 = petnameBox8.Text

        My.Settings.OrgasmAllow = alloworgasmComboBox.Text
        My.Settings.OrgasmRuin = ruinorgasmComboBox.Text
        My.Settings.LockOrgasmChances = CBLockOrgasmChances.Checked
        My.Settings.DomDenialEnd = CBDomDenialEnds.Checked
        My.Settings.DomOrgasmEnd = CBDomOrgasmEnds.Checked
        ' My.Settings.DomPOT = CBDomPOT.Checked
        My.Settings.DomLowercase = LCaseCheckBox.Checked
        My.Settings.DomNoApostrophes = apostropheCheckBox.Checked
        My.Settings.DomNoCommas = commaCheckBox.Checked
        My.Settings.DomNoPeriods = periodCheckBox.Checked
        My.Settings.DomMeMyMine = CBMeMyMine.Checked
        My.Settings.DomEmotes = "NULL"

        My.Settings.DomMoodMin = NBDomMoodMin.Value
        My.Settings.DomMoodMax = NBDomMoodMax.Value
        My.Settings.AvgCockMin = NBAvgCockMin.Value
        My.Settings.AvgCockMax = NBAvgCockMax.Value
        My.Settings.SelfAgeMin = NBSelfAgeMin.Value
        My.Settings.SelfAgeMax = NBSelfAgeMax.Value
        My.Settings.SubAgeMin = NBSubAgeMin.Value
        My.Settings.SubAgeMax = NBSubAgeMax.Value
    End Sub

    Public Sub LoadDommeSettings()

        DominationLevel.Value = mySettingsAccessor.DominationLevel
        NBEmpathy.Value = mySettingsAccessor.ApathyLevel
        domageNumBox.Value = My.Settings.DomAge
        NBDomBirthdayMonth.Value = My.Settings.DomBirthMonth
        NBDomBirthdayDay.Value = My.Settings.DomBirthDay
        TBDomHairColor.Text = My.Settings.DomHair
        domhairlengthComboBox.Text = My.Settings.DomHairLength
        TBDomEyeColor.Text = My.Settings.DomEyes
        boobComboBox.Text = My.Settings.DomCup
        dompubichairComboBox.Text = My.Settings.DomPubicHair
        CBDomTattoos.Checked = My.Settings.DomTattoos
        CBDomFreckles.Checked = My.Settings.DomFreckles

        MainWindow.dompersonalitycombobox.Text = mySettingsAccessor.DommePersonality
        crazyCheckBox.Checked = My.Settings.DomCrazy
        vulgarCheckBox.Checked = My.Settings.DomVulgar
        supremacistCheckBox.Checked = My.Settings.DomSupremacist
        sadisticCheckBox.Checked = My.Settings.DomSadistic
        degradingCheckBox.Checked = My.Settings.DomDegrading
        PetNameBox1.Text = My.Settings.pnSetting1
        petnameBox2.Text = My.Settings.pnSetting2
        petnameBox3.Text = My.Settings.pnSetting3
        petnameBox4.Text = My.Settings.pnSetting4
        petnameBox5.Text = My.Settings.pnSetting5
        petnameBox6.Text = My.Settings.pnSetting6
        petnameBox7.Text = My.Settings.pnSetting7
        petnameBox8.Text = My.Settings.pnSetting8

        alloworgasmComboBox.Text = My.Settings.OrgasmAllow
        ruinorgasmComboBox.Text = My.Settings.OrgasmRuin
        CBLockOrgasmChances.Checked = My.Settings.LockOrgasmChances
        CBDomDenialEnds.Checked = My.Settings.DomDenialEnd
        CBDomOrgasmEnds.Checked = My.Settings.DomOrgasmEnd
        'CBDomPOT.Checked = My.Settings.DomPOT
        LCaseCheckBox.Checked = My.Settings.DomLowercase
        apostropheCheckBox.Checked = My.Settings.DomNoApostrophes
        commaCheckBox.Checked = My.Settings.DomNoCommas
        periodCheckBox.Checked = My.Settings.DomNoPeriods
        CBMeMyMine.Checked = My.Settings.DomMeMyMine
        'domemoteComboBox.Text = My.Settings.DomEmotes

        NBDomMoodMin.Value = My.Settings.DomMoodMin
        NBDomMoodMax.Value = My.Settings.DomMoodMax
        NBAvgCockMin.Value = My.Settings.AvgCockMin
        NBAvgCockMax.Value = My.Settings.AvgCockMax
        NBSelfAgeMin.Value = My.Settings.SelfAgeMin
        NBSelfAgeMax.Value = My.Settings.SelfAgeMax
        NBSubAgeMin.Value = My.Settings.SubAgeMin
        NBSubAgeMax.Value = My.Settings.SubAgeMax

    End Sub

    Private Sub BTNLocalTagPrevious_Click(sender As Object, e As EventArgs) Handles BTNLocalTagPrevious.Click

        LocalTagCount -= 1
        LBLLocalTagCount.Text = LocalTagCount & "/" & LocalImageTagDir.Count
        BTNLocalTagNext.Enabled = LocalImageTagDir.Count < LocalTagCount

        SaveLocalImageTags(CurrentLocalImageTagImage)

        MainWindow.mainPictureBox.LoadAsync(LocalImageTagDir(LocalTagCount - 1))
        CurrentLocalImageTagImage = LocalImageTagDir(LocalTagCount - 1)

        BTNLocalTagPrevious.Enabled = LocalTagCount > 1

        Dim taggedItem As TaggedItem = GetTaggedItem(LocalImageTagFile, CurrentLocalImageTagImage)
        SetLocalTagCheckboxes(taggedItem.ItemTags)
    End Sub

    Private Sub BTNLocalTagSave_Click(sender As Object, e As EventArgs) Handles BTNLocalTagSave.Click

        SaveLocalImageTags(CurrentLocalImageTagImage)

        BTNLocalTagDir.Enabled = True
        TBLocalTagDir.Enabled = True
        BTNLocalTagSave.Enabled = False
        BTNLocalTagNext.Enabled = False
        BTNLocalTagPrevious.Enabled = False

        SetLocalImageEnabled(False)

        LBLLocalTagCount.Text = "0/0"
        LBLLocalTagCount.Enabled = False

        MainWindow.mainPictureBox.Image = Nothing
    End Sub

    Private Sub TBLocalTagDir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBLocalTagDir.KeyPress
        If e.KeyChar <> Convert.ToChar(13) Then
            Return
        End If

        e.Handled = True
        e.KeyChar = Chr(0)

        Dim getImages As Result(Of List(Of String)) = GetImagesInFolder(TBLocalTagDir.Text) _
            .Ensure(Function(data) data.Any(), TBLocalTagDir.Text & " does not have any images in it.")

        LocalImageTagDir = getImages.GetResultOrDefault(New List(Of String))

        If getImages.IsFailure Then
            MessageBox.Show(Me, getImages.GetErrorMessageOrDefault(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        CurrentLocalImageTagImage = LocalImageTagDir.First()
        MainWindow.mainPictureBox.LoadAsync(CurrentLocalImageTagImage)

        Dim taggedItem As TaggedItem = GetTaggedItem(LocalImageTagFile, CurrentLocalImageTagImage)
        SetLocalTagCheckboxes(taggedItem.ItemTags)

        LocalTagCount = 1
        LBLLocalTagCount.Text = LocalTagCount.ToString() & "/" & LocalImageTagDir.Count.ToString()

        BTNLocalTagSave.Enabled = True
        BTNLocalTagNext.Enabled = LocalImageTagDir.Count < LocalTagCount
        BTNLocalTagPrevious.Enabled = False
        BTNLocalTagDir.Enabled = False
        TBLocalTagDir.Enabled = False

        SetLocalImageEnabled(True)

        LBLLocalTagCount.Enabled = True
    End Sub

    Private Sub CBRangeOrgasm_CheckedChanged(sender As Object, e As EventArgs) Handles DommeDecideOrgasmCB.CheckedChanged
        AllowOrgasmOftenNB.Enabled = Not DommeDecideOrgasmCB.Checked
        NBAllowSometimes.Enabled = Not DommeDecideOrgasmCB.Checked
        NBAllowRarely.Enabled = Not DommeDecideOrgasmCB.Checked
    End Sub

    Private Sub CBRangeRuin_CheckedChanged(sender As Object, e As EventArgs) Handles DommeDecideRuinCB.CheckedChanged
        If DommeDecideRuinCB.Checked = False Then
            NBRuinOften.Enabled = True
            NBRuinSometimes.Enabled = True
            NBRuinRarely.Enabled = True
        Else
            NBRuinOften.Enabled = False
            NBRuinSometimes.Enabled = False
            NBRuinRarely.Enabled = False
        End If
    End Sub

    Private Sub CBRangeOrgasm_LostFocus(sender As Object, e As EventArgs) Handles DommeDecideOrgasmCB.LostFocus
        mySettingsAccessor.DoesDommeDecideOrgasmRange = DommeDecideOrgasmCB.Checked
    End Sub

    Private Sub CBRangeRuin_LostFocus(sender As Object, e As EventArgs) Handles DommeDecideRuinCB.LostFocus
        mySettingsAccessor.DoesDommeDecideRuinRange = DommeDecideRuinCB.Checked
    End Sub

    Private Sub NBAllowOften_ValueChanged(sender As Object, e As EventArgs) Handles AllowOrgasmOftenNB.LostFocus
        mySettingsAccessor.AllowOrgasmOftenPercent = AllowOrgasmOftenNB.Value
    End Sub

    Private Sub NBAllowSometimes_ValueChanged(sender As Object, e As EventArgs) Handles NBAllowSometimes.LostFocus
        mySettingsAccessor.AllowOrgasmSometimesPercent = NBAllowSometimes.Value
    End Sub

    Private Sub NBAllowRarely_ValueChanged(sender As Object, e As EventArgs) Handles NBAllowRarely.LostFocus
        mySettingsAccessor.AllowOrgasmRarelyPercent = NBAllowRarely.Value
    End Sub

    Private Sub NBRuinOften_ValueChanged(sender As Object, e As EventArgs) Handles NBRuinOften.LostFocus
        mySettingsAccessor.RuinOrgasmOftenPercent = NBRuinOften.Value
    End Sub

    Private Sub NBRuinSometimes_ValueChanged(sender As Object, e As EventArgs) Handles NBRuinSometimes.LostFocus
        mySettingsAccessor.RuinOrgasmSometimesPercent = NBRuinSometimes.Value
    End Sub

    Private Sub NBRuinRarely_ValueChanged(sender As Object, e As EventArgs) Handles NBRuinRarely.LostFocus
        mySettingsAccessor.RuinOrgasmRarelyPercent = NBRuinRarely.Value
    End Sub

    Private Sub TBSafeword_LostFocus(sender As Object, e As EventArgs) Handles TBSafeword.LostFocus
        mySettingsAccessor.SafeWord = TBSafeword.Text
    End Sub

    Private Sub Button4_Click_5(sender As Object, e As EventArgs) Handles Button4.Click

        TBResponses.Text = ""
        RTBResponses.Text = ""
        RTBResponsesKEY.Text = ""

        Dim files() As String = myDirectory.GetFiles(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\")

        LBResponses.Items.Clear()

        For Each file As String In files
            LBResponses.Items.Add(Path.GetFileName(file).Replace(".txt", ""))
        Next




    End Sub

    Private Sub LBResponses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LBResponses.SelectedIndexChanged

        Dim ResponsePath As String = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & LBResponses.SelectedItem & ".txt"

        If Not File.Exists(ResponsePath) Then Return



        TBResponses.Text = LBResponses.SelectedItem

        RTBResponses.Text = ""


        Dim ioFile As New StreamReader(ResponsePath)
        Dim lines As New List(Of String)

        ' Dim ResponseCount As Integer
        'Dim ResponseEnd As Integer

        'ResponseCount = -1

        While ioFile.Peek <> -1
            '   ResponseCount += 1
            lines.Add(ioFile.ReadLine())
        End While

        ioFile.Close()
        ioFile.Dispose()


        'ResponseEnd = ResponseCount
        'ResponseCount = 0

        RTBResponsesKEY.Text = lines(0)

        For i As Integer = 1 To lines.Count - 1
            RTBResponses.Text = RTBResponses.Text & lines(i) & Environment.NewLine
        Next

        ' Array.ForEach(Enumerable.Range(0, RTBResponses.Lines.Length).Where(Function(x) RTBResponses.Lines(x).StartsWith("[")).ToArray, Sub(x)
        'RTBResponses.SelectionStart = RTBResponses.GetFirstCharIndexFromLine(x)
        'RTBResponses.SelectionLength = RTBResponses.Lines(x).Length
        'RTBResponses.SelectionFont = New Font(RTBResponses.SelectionFont, FontStyle.Bold)
        '                                                                                                                              End Sub)

        For i As Integer = 0 To RTBResponses.Lines.Count - 1
            Try
                If RTBResponses.Lines(i).Substring(0, 1) = "[" Then
                    RTBResponses.SelectionStart = RTBResponses.Text.IndexOf(RTBResponses.Lines(i))
                    RTBResponses.SelectionLength = RTBResponses.Lines(i).Length
                    'RTBResponses.Select(RTBResponses.GetFirstCharIndexFromLine(i), RTBResponses.Lines(i).Length)
                    RTBResponses.SelectionFont = New Font(RTBResponses.SelectionFont, FontStyle.Bold)
                End If
            Catch
            End Try
        Next




        'Do
        'RTBResponses.Text = RTBResponses.Text & lines(ResponseCount) & Environment.NewLine
        'ResponseCount += 1
        'Loop Until ResponseCount = ResponseEnd + 1






    End Sub

    Private Sub Button5_Click_2(sender As Object, e As EventArgs) Handles Button5.Click



        If TBResponses.Text = "" Then
            MessageBox.Show(Me, "Please enter a file name for this Response script!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        If RTBResponsesKEY.Text = "" Then
            MessageBox.Show(Me, "You have not entered any keywords for the program to find!" & Environment.NewLine & Environment.NewLine & "Please add at least one keyword between brackets in the top window.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        If RTBResponses.Text = "" Then
            MessageBox.Show(Me, "The Response file you are attempting to save is blank!" & Environment.NewLine & Environment.NewLine & "Please add some lines before saving.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        Dim ResponsesaveDir As String = TBResponses.Text
        ResponsesaveDir = ResponsesaveDir.Replace(".txt", "")

        If Not LBResponses.Items.Contains(ResponsesaveDir) Then
            LBResponses.Items.Add(ResponsesaveDir)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt", RTBResponsesKEY.Text & Environment.NewLine, False)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt", RTBResponses.Text, True)
            File.WriteAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt", File.ReadAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt").Where(Function(s) s <> String.Empty))
        Else
            Dim Result As Integer = MessageBox.Show(Me, ResponsesaveDir & " already exists!" & Environment.NewLine & Environment.NewLine & "Do you wish to overwrite?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If Result = DialogResult.Yes Then
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt", RTBResponsesKEY.Text & Environment.NewLine, False)
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt", RTBResponses.Text, True)
                File.WriteAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt", File.ReadAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\" & ResponsesaveDir & ".txt").Where(Function(s) s <> String.Empty))
            Else
                Debug.Print("Did not work")
                Return
            End If
        End If

        MessageBox.Show(Me, "Response file has been saved!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)






    End Sub

    Private Sub Button9_Click_2(sender As Object, e As EventArgs) Handles Button9.Click

        If RTBResponses.Text <> "" Then
            MessageBox.Show(Me, "Template cannot be generated while there is text in the main Response window!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim TemplateDir As String = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Vocabulary\Responses\System\Template\Template.txt"

        If File.Exists(TemplateDir) Then

            Dim TempReader As New StreamReader(TemplateDir)
            Dim TempList As New List(Of String)

            While TempReader.Peek <> -1
                TempList.Add(TempReader.ReadLine())
            End While

            TempReader.Close()
            TempReader.Dispose()

            For i As Integer = 0 To TempList.Count - 1
                RTBResponses.Text = RTBResponses.Text & TempList(i) & Environment.NewLine
            Next

            For i As Integer = 0 To RTBResponses.Lines.Count - 1
                ' If RTBResponses.Lines(i).Substring(0, 1) = "[" Then
                RTBResponses.SelectionStart = RTBResponses.Text.IndexOf(RTBResponses.Lines(i))
                RTBResponses.SelectionLength = RTBResponses.Lines(i).Length
                RTBResponses.SelectionFont = New Font(RTBResponses.SelectionFont, FontStyle.Bold)
                'End If
            Next

        Else
            MessageBox.Show(Me, "Template file was not found!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If


    End Sub

    Private Sub CBEdgeUseAvg_LostFocus(sender As Object, e As EventArgs) Handles UseAverageEdgeThresholdCB.LostFocus
        mySettingsAccessor.UseAverageEdgeTimeAsThreshold = UseAverageEdgeThresholdCB.Checked
    End Sub
    Private Sub CBLongEdgeTaunts_LostFocus(sender As Object, e As EventArgs) Handles AllowLongEdgeTauntCB.LostFocus
        mySettingsAccessor.AllowsLongEdgeTaunts = AllowLongEdgeTauntCB.Checked
    End Sub

    Private Sub CBLongEdgeInterrupts_LostFocus(sender As Object, e As EventArgs) Handles AllowLongEdgeInterruptCB.LostFocus
        mySettingsAccessor.AllowsLongEdgeInterrupts = AllowLongEdgeInterruptCB.Checked
    End Sub

    Private Sub CBLongEdgeInterrupts_MouseHover(sender As Object, e As EventArgs) Handles AllowLongEdgeInterruptCB.MouseEnter
        LBLSubSettingsDescription.Text = "When this box is checked, the domme will include edge taunts that call special Interrupt scripts when the Long Edge threshold has been passed."
    End Sub

    Private Sub CBEdgeUseAvg_MouseHover(sender As Object, e As EventArgs) Handles UseAverageEdgeThresholdCB.MouseEnter
        LBLSubSettingsDescription.Text = "When this is checked, the domme will use the average amount of time it has historically taken you to reach the edge to decide when you have been trying to edge for too long."
    End Sub

    Private Sub CBLongEdgeTaunts_MouseHover(sender As Object, e As EventArgs) Handles AllowLongEdgeTauntCB.MouseEnter
        LBLSubSettingsDescription.Text = "When this box is checked, the domme will include edge taunts that are reserved for when the Long Edge threshold has been passed." & Environment.NewLine & Environment.NewLine &
            "This will allow the domme to tease you about the fact that you have been trying to edge for longer than she expected."
    End Sub

    Private Sub NBHOldTheEdgeMax_MouseHover(sender As Object, e As EventArgs) Handles HoldEdgeMaximum.MouseEnter
        LBLSubSettingsDescription.Text = "Sets the maximum time (in seconds) that the domme will make you hold the edge. If you enter 0 as an amount, then the domme will decide based on her level."
    End Sub

    Private Sub NBWritinGTaskMin_MouseHover(sender As Object, e As EventArgs) Handles NBWritingTaskMin.MouseEnter
        LBLSubSettingsDescription.Text = "Sets the minimum amount of lines the domme will assign you for writing tasks."
    End Sub
    Private Sub NBWritinGTaskMax_MouseHover(sender As Object, e As EventArgs) Handles NBWritingTaskMax.MouseEnter
        LBLSubSettingsDescription.Text = "Sets the maximum amount of lines the domme will assign you for writing tasks."
    End Sub
    'Private Sub SubDescText_MouseHover(sender As Object, e As EventArgs) Handles Panel2.MouseEnter, GroupBox32.MouseEnter, GroupBox45.MouseEnter, GroupBox35.MouseEnter, GroupBox7.MouseEnter, GroupBox12.MouseEnter
    '   LBLSubSettingsDescription.Text = "Hover over any setting in the menu for a more detailed description of its function."
    'End Sub
    Private Sub CBHimHer_MouseHover(sender As Object, e As EventArgs) Handles CBHimHer.MouseEnter
        LBLSubSettingsDescription.Text = "When this is checked, Glitter will automatically replace any instance of He/Him/His with She/Her/Her."
    End Sub

    Private Sub NBNextImageChance_LostFocus(sender As Object, e As EventArgs) Handles NBNextImageChance.LostFocus
        My.Settings.NextImageChance = NBNextImageChance.Value
    End Sub

    Private Sub orgasmsperlockButton_Click(sender As Object, e As EventArgs) Handles orgasmsperlockButton.Click

        If Not limitcheckbox.Checked Then
            MessageBox.Show(Me, "The Limit box must be checked before clicking this button!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim result As DialogResult

        If orgasmsPerNumBox.Value = 1 Then
            result = MessageBox.Show("This will limit you to 1 orgasm for the next " & LCase(orgasmsperComboBox.Text) & "." & Environment.NewLine & Environment.NewLine &
                                               "Are you absolutely sure you wish to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        Else
            result = MessageBox.Show("This will limit you to " & orgasmsPerNumBox.Value & " orgasms for the next " & LCase(orgasmsperComboBox.Text) & "." & Environment.NewLine & Environment.NewLine &
                                                           "Are you absolutely sure you wish to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        End If

        If result = DialogResult.No Then
            Return
        End If

        My.Settings.OrgasmsRemaining = orgasmsPerNumBox.Value
        My.Settings.DomOrgasmPer = orgasmsPerNumBox.Value

        My.Settings.DomPerMonth = orgasmsperComboBox.Text

        Dim releaseDate As Date = GetOrgasmReleaseDate(orgasmsperComboBox.Text, DateTime.Now.Date).Date
        mySettingsAccessor.OrgasmLockDate = releaseDate

        limitcheckbox.Enabled = False
        orgasmsPerNumBox.Enabled = False
        orgasmsperComboBox.Enabled = False
        orgasmsperlockButton.Enabled = False
        orgasmlockrandombutton.Enabled = False
    End Sub

    Private Sub OrgasmLockRandomButton_Click(sender As Object, e As EventArgs) Handles orgasmlockrandombutton.Click
        If limitcheckbox.Checked = False Then
            MessageBox.Show(Me, "The Limit box must be checked before clicking this button!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("This will allow the domme to limit you to a random number of orgasms for a random amount of time. High level dommes could restrict you to a very low amount for up to a year!" & Environment.NewLine & Environment.NewLine &
                                           "Are you absolutely sure you wish to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)

        If result = DialogResult.No Then
            Return
        End If

        Dim randomOrgasms As Integer = MainWindow.ssh.randomizer.Next(1, 6)

        My.Settings.OrgasmsRemaining = randomOrgasms
        My.Settings.DomOrgasmPer = randomOrgasms

        orgasmsPerNumBox.Value = randomOrgasms

        Dim orgasmInterval As String = GetOrgasmInterval(mySettingsAccessor.DominationLevel)
        My.Settings.DomPerMonth = orgasmInterval
        orgasmsperComboBox.Text = My.Settings.DomPerMonth

        Dim releaseDate As Date = GetOrgasmReleaseDate(orgasmsperComboBox.Text, DateTime.Now.Date).Date
        mySettingsAccessor.OrgasmLockDate = releaseDate

        limitcheckbox.Enabled = False
        orgasmsPerNumBox.Enabled = False
        orgasmsperComboBox.Enabled = False
        orgasmsperlockButton.Enabled = False
        orgasmlockrandombutton.Enabled = False
    End Sub

    Private Sub CBVTType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBVTType.SelectedIndexChanged
        If CBVTType.Text = "Censorship Sucks" Then
            LBVidScript.Items.Clear()
            LBVidScript.Items.Add("CensorBarOff")
            LBVidScript.Items.Add("CensorBarOn")
        End If

        If CBVTType.Text = "Avoid The Edge" Then
            LBVidScript.Items.Clear()
            LBVidScript.Items.Add("Taunts")
        End If


        If CBVTType.Text = "Red Light Green Light" Then
            LBVidScript.Items.Clear()
            LBVidScript.Items.Add("Green Light")
            LBVidScript.Items.Add("Red Light")
            LBVidScript.Items.Add("Taunts")
        End If


    End Sub

    Private Sub NBTeaseLengthMin_MouseHover(sender As Object, e As EventArgs) Handles NBTeaseLengthMin.MouseEnter
        LBLRangeSettingsDescription.Text = "Set the minimum amount of time the program will run before the domme decides if you can have an orgasm." & Environment.NewLine & Environment.NewLine &
            "The domme will not move to an End script until the first @End point of a Module that occurs after tease time expires." & Environment.NewLine & Environment.NewLine &
            "If the domme decides to tease you again, the tease time will be reset to a new amount based Tease Length settings."
    End Sub

    Private Sub NBTeaseLengthMax_MouseHover(sender As Object, e As EventArgs) Handles NBTeaseLengthMax.MouseEnter
        LBLRangeSettingsDescription.Text = "Set the maximum amount of time the program will run before the domme decides if you can have an orgasm." & Environment.NewLine & Environment.NewLine &
            "The domme will not move to an End script until the first @End point of a Module that occurs after tease time expires." & Environment.NewLine & Environment.NewLine &
            "If the domme decides to tease you again, the tease time will be reset to a new amount based Tease Length settings."
    End Sub


    Private Sub CBTeaseLengthDD_LostFocus(sender As Object, e As EventArgs) Handles TeaseLengthDommeDetermined.LostFocus
        mySettingsAccessor.IsTeaseLengthDommeDetermined = TeaseLengthDommeDetermined.Checked
    End Sub

    Private Sub CBTauntCycleDD_LostFocus(sender As Object, e As EventArgs) Handles CBTauntCycleDD.LostFocus
        mySettingsAccessor.IsTauntCycleDommeDetermined = CBTauntCycleDD.Checked
    End Sub

    Private Sub CBTeaseLengthDD_MouseHover(sender As Object, e As EventArgs) Handles TeaseLengthDommeDetermined.MouseEnter
        LBLRangeSettingsDescription.Text = "This allows the domme to decide the length of the tease based on her level." & Environment.NewLine & Environment.NewLine &
            "A level 1 domme may tease you for 15-20 minutes, while a level 5 domme may tease you as long as an hour." & Environment.NewLine & Environment.NewLine &
            "The domme will not move to an End script until the first @End point of a Module that occurs after tease time expires."
    End Sub

    Private Sub NBTauntCycleMin_MouseHover(sender As Object, e As EventArgs) Handles NBTauntCycleMin.MouseEnter
        LBLRangeSettingsDescription.Text = "Set the minimum amount of time the domme will make you stroke during Taunt cycles."
    End Sub

    Private Sub NBTauntCycleMax_MouseHover(sender As Object, e As EventArgs) Handles NBTauntCycleMax.MouseEnter
        LBLRangeSettingsDescription.Text = "Set the maximum amount of time the domme will make you stroke during Taunt cycles"
    End Sub

    Private Sub CBTauntCycleDD_MouseHover(sender As Object, e As EventArgs) Handles CBTauntCycleDD.MouseEnter
        LBLRangeSettingsDescription.Text = "This allows the domme to decide how long she makes you stroke during Taunt cycles based on her level." & Environment.NewLine & Environment.NewLine &
            "A level 1 domme may have you stroke for a couple minutes at a time, while a level 5 domme may have you stroke up to 10 minutes during each Taunt cycle."
    End Sub

    Private Sub SliderSTF_MouseHover(sender As Object, e As EventArgs) Handles SliderSTF.MouseEnter
        LBLRangeSettingsDescription.Text = "This allows you to set the frequency of the domme's Stroke Taunts." & Environment.NewLine & Environment.NewLine &
            "A middle value tries to emulate an online experience as closely as possible. Use a higher value to increase the frequency of Taunts to something you would expect in a webtease. Use a lower value to simulate the domme being preoccupied or not that interested in engaging you."
    End Sub

    Private Sub TauntSlider_MouseHover(sender As Object, e As EventArgs) Handles TauntSlider.MouseEnter
        LBLRangeSettingsDescription.Text = "This allows you to set the frequency of the domme's Taunts during Video Teases." & Environment.NewLine & Environment.NewLine &
            "A middle value creates a fairly common use of Taunts. Use a higher value to make the domme extremely engaged. Use a lower value to focus on the Video Tease with minimal interaction from the domme."
    End Sub

    Private Sub CBRangeOrgasm_MouseHover(sender As Object, e As EventArgs) Handles DommeDecideOrgasmCB.MouseEnter
        LBLRangeSettingsDescription.Text = "This allows the domme to decide what chance she will allow an orgasm based on her settings." & Environment.NewLine & Environment.NewLine &
            "Default settings are: Often Allows: 75% - Sometimes Allows: 50% - Rarely Allows: 20%"
    End Sub

    Private Sub NBAllowOften_MouseHover(sender As Object, e As EventArgs) Handles AllowOrgasmOftenNB.MouseEnter
        LBLRangeSettingsDescription.Text = "When ""Domme Decide"" is not checked, this allows you to set what chance the domme will allow orgasm when she is set to ""Often Allows""."
    End Sub

    Private Sub NBAllowSometimes_MouseHover(sender As Object, e As EventArgs) Handles NBAllowSometimes.MouseEnter
        LBLRangeSettingsDescription.Text = "When ""Domme Decide"" is not checked, this allows you to set what chance the domme will allow orgasm when she is set to ""Sometimes Allows""."
    End Sub

    Private Sub NBAllowRarely_MouseHover(sender As Object, e As EventArgs) Handles NBAllowRarely.MouseEnter
        LBLRangeSettingsDescription.Text = "When ""Domme Decide"" is not checked, this allows you to set what chance the domme will allow orgasm when she is set to ""Rarely Allows""."
    End Sub

    Private Sub CBRangeRuin_MouseHover(sender As Object, e As EventArgs) Handles DommeDecideRuinCB.MouseEnter
        LBLRangeSettingsDescription.Text = "This allows the domme to decide what chance she will ruin an orgasm based on her settings." & Environment.NewLine & Environment.NewLine &
            "Default settings are: Often Ruins: 75% - Sometimes Ruins: 50% - Rarely Ruins: 20%"
    End Sub

    Private Sub NBRuinOften_MouseHover(sender As Object, e As EventArgs) Handles NBRuinOften.MouseEnter
        LBLRangeSettingsDescription.Text = "When ""Domme Decide"" is not checked, this allows you to set what chance the domme will ruin an orgasm when she is set to ""Often Ruins""."
    End Sub

    Private Sub NBRuinSometimes_MouseHover(sender As Object, e As EventArgs) Handles NBRuinSometimes.MouseEnter
        LBLRangeSettingsDescription.Text = "When ""Domme Decide"" is not checked, this allows you to set what chance the domme will ruin an orgasm when she is set to ""Sometimes Ruins""."
    End Sub

    Private Sub NBRuinRarely_MouseHover(sender As Object, e As EventArgs) Handles NBRuinRarely.MouseEnter
        LBLRangeSettingsDescription.Text = "When ""Domme Decide"" is not checked, this allows you to set what chance the domme will ruin an orgasm when she is set to ""Rarely Ruins""."
    End Sub


    Private Sub NBNextImageChance_MouseHover(sender As Object, e As EventArgs) Handles NBNextImageChance.MouseEnter
        LBLRangeSettingsDescription.Text = "When running a slideshow with the ""Tease"" option selected, this value determines what chance the slideshow will move forward instead of backward."
    End Sub

    Private Sub nbcensorshowmin_MouseHover(sender As Object, e As EventArgs) Handles NBCensorShowMin.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the minimum amount of time the censor bar will be on the screen at a time while playing Censorship Sucks."
    End Sub

    Private Sub nbcensorshowmax_MouseHover(sender As Object, e As EventArgs) Handles NBCensorShowMax.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the maximum amount of time the censor bar will be on the screen at a time while playing Censorship Sucks."
    End Sub

    Private Sub nbcensorhidemin_MouseHover(sender As Object, e As EventArgs) Handles NBCensorHideMin.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the minimum amount of time the censor bar will be invisible while playing Censorship Sucks."
    End Sub

    Private Sub nbcensorhidemax_MouseHover(sender As Object, e As EventArgs) Handles NBCensorHideMax.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the maximum amount of time the censor bar will be invisible while playing Censorship Sucks."
    End Sub

    Private Sub cbcensorconstant_MouseHover(sender As Object, e As EventArgs) Handles CBCensorConstant.MouseEnter
        LBLRangeSettingsDescription.Text = "When this is checked, the censor bar will always be visible while playing Censorship Sucks. Its position on the screen will still change in time with Show Censor Bar settings."
    End Sub

    Private Sub nbredlightmin_MouseHover(sender As Object, e As EventArgs) Handles NBRedLightMin.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the minimum amount of time the domme will keep the video paused while playing Red Light Green Light."
    End Sub

    Private Sub nbredlightmax_MouseHover(sender As Object, e As EventArgs) Handles NBRedLightMax.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the maximum amount of time the domme will keep the video paused while playing Red Light Green Light."
    End Sub

    Private Sub nbgreenlightmin_MouseHover(sender As Object, e As EventArgs) Handles NBGreenLightMin.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the minimum amount of time the domme will keep the video playing while playing Red Light Green Light."
    End Sub

    Private Sub nbgreenlightmax_MouseHover(sender As Object, e As EventArgs) Handles NBGreenLightMax.MouseEnter
        LBLRangeSettingsDescription.Text = "This determines the maximum amount of time the domme will keep the video playing while playing Red Light Green Light."
    End Sub

    Private Sub RangeSet_MouseHover(sender As Object, e As EventArgs) Handles Panel6.MouseEnter, GroupBox21.MouseEnter, GroupBox18.MouseEnter, GroupBox19.MouseEnter, GroupBox10.MouseEnter, GBRangeRuinChance.MouseEnter, GBRangeOrgasmChance.MouseEnter, GroupBox57.MouseEnter
        LBLRangeSettingsDescription.Text = "Hover over any setting in the menu for a more detailed description of its function."
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TBWishlistURL.TextChanged
        Try
            WishlistPreview.Image.Dispose()
        Catch
        End Try
        WishlistPreview.Image = Nothing
        GC.Collect()
        Try
            WishlistPreview.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(TBWishlistURL.Text)))
        Catch
            MessageBox.Show(Me, "Failed to load Wishlist preview image!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub radioGold_CheckedChanged(sender As Object, e As EventArgs) Handles radioGold.CheckedChanged
        WishlistCostGold.Visible = True
        WishlistCostSilver.Visible = False
    End Sub

    Private Sub radioSilver_CheckedChanged(sender As Object, e As EventArgs) Handles radioSilver.CheckedChanged
        WishlistCostGold.Visible = False
        WishlistCostSilver.Visible = True
    End Sub

    Private Sub TBWishlistItem_TextChanged(sender As Object, e As EventArgs) Handles TBWishlistItem.TextChanged
        LBLWishListName.Text = TBWishlistItem.Text
    End Sub

    Private Sub NBWishlistCost_ValueChanged(sender As Object, e As EventArgs) Handles NBWishlistCost.ValueChanged
        LBLWishlistCost.Text = NBWishlistCost.Value
    End Sub

    Private Sub TBWishlistComment_TextChanged(sender As Object, e As EventArgs) Handles TBWishlistComment.TextChanged
        LBLWishListText.Text = TBWishlistComment.Text
    End Sub

    Private Sub BTNWishlistCreate_Click(sender As Object, e As EventArgs) Handles BTNWishlistCreate.Click

        If TBWishlistItem.Text = "" Then
            MessageBox.Show(Me, "Please enter a name for this item!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        Try
            WishlistPreview.Image.Dispose()
        Catch
        End Try

        WishlistPreview.Image = Nothing
        GC.Collect()



        Try
            WishlistPreview.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(TBWishlistURL.Text)))
        Catch ex As Exception
            MessageBox.Show(Me, "Tease AI cannot locate the image URL provided! Please make sure it is a valid address and you are connected to the internet!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End Try

        If TBWishlistComment.Text = "" Then
            MessageBox.Show(Me, "Please enter a comment for this item!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        Try
            Dim WishDir As String = Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Apps\Wishlist\Items\" & TBWishlistItem.Text & ".txt"

            If File.Exists(WishDir) Then My.Computer.FileSystem.DeleteFile(WishDir)

            My.Computer.FileSystem.WriteAllText(WishDir, TBWishlistItem.Text, True)
            My.Computer.FileSystem.WriteAllText(WishDir, Environment.NewLine, True)
            My.Computer.FileSystem.WriteAllText(WishDir, TBWishlistURL.Text, True)
            My.Computer.FileSystem.WriteAllText(WishDir, Environment.NewLine, True)
            Dim WishCost As String
            If radioSilver.Checked Then
                WishCost = NBWishlistCost.Value & " Silver"
            Else
                WishCost = NBWishlistCost.Value & " Gold"
            End If
            My.Computer.FileSystem.WriteAllText(WishDir, WishCost, True)
            My.Computer.FileSystem.WriteAllText(WishDir, Environment.NewLine, True)
            My.Computer.FileSystem.WriteAllText(WishDir, TBWishlistComment.Text, True)

            MessageBox.Show(Me, "Wishlist file saved successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
            MessageBox.Show(Me, "There was a problem saving this file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try

    End Sub

    Private Sub CBOwnChastity_CheckedChanged(sender As Object, e As EventArgs) Handles CBOwnChastity.CheckedChanged

        DoesChastityDeviceRequirePiercingCB.Enabled = CBOwnChastity.Checked
        ChastityDeviceContainsSpikesCB.Enabled = CBOwnChastity.Checked

    End Sub

    Private Sub CBOwnChastity_LostFocus(sender As Object, e As EventArgs) Handles CBOwnChastity.LostFocus
        mySettingsAccessor.HasChastityDevice = CBOwnChastity.Checked
    End Sub

    Private Sub CBChastityPA_LostFocus(sender As Object, e As EventArgs) Handles DoesChastityDeviceRequirePiercingCB.LostFocus
        mySettingsAccessor.DoesChastityDeviceRequirePiercing = DoesChastityDeviceRequirePiercingCB.Checked
    End Sub

    Private Sub CBChastitySpikes_LostFocus(sender As Object, e As EventArgs) Handles ChastityDeviceContainsSpikesCB.LostFocus
        mySettingsAccessor.DoesChastityDeviceContainSpikes = ChastityDeviceContainsSpikesCB.Checked
    End Sub

    Private Sub CBHimHer_LostFocus(sender As Object, e As EventArgs) Handles CBHimHer.LostFocus
        mySettingsAccessor.IsSubFemale = CBHimHer.Checked
    End Sub

    Private Sub CBDomDel_LostFocus(sender As Object, e As EventArgs) Handles CBDomDel.LostFocus
        mySettingsAccessor.CanDommeDeleteFiles = CBDomDel.Checked
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BTNMaintenanceCancel.Click
        If BWURLFiles.IsBusy Then BWURLFiles.CancelAsync()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles BTNMaintenanceScripts.Click

        PBMaintenance.Maximum = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text, FileIO.SearchOption.SearchAllSubDirectories, "*.txt").Count +
             My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Images\System\", FileIO.SearchOption.SearchAllSubDirectories, "*.txt").Count
        PBMaintenance.Value = 0
        Dim BlankAudit As Integer = 0
        Dim ErrorAudit As Integer = 0

        BTNMaintenanceRebuild.Enabled = False
        BTNMaintenanceRefresh.Enabled = False

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text, FileIO.SearchOption.SearchAllSubDirectories, "*.txt")

            LBLMaintenance.Text = "Checking " & Path.GetFileName(foundFile) & "..."
            PBMaintenance.Value += 1
            Dim CheckFiles As String() = File.ReadAllLines(foundFile)

            Dim GoodLines As New List(Of String)

            For Each line As String In CheckFiles
                If Not line = "" Then
                    GoodLines.Add(line)
                Else
                    BlankAudit += 1
                End If
            Next

            For i As Integer = 0 To GoodLines.Count - 1
                If GoodLines(i).Contains(" ]") Then
                    ErrorAudit += 1
                    Do
                        GoodLines(i) = GoodLines(i).Replace(" ]", "]")
                    Loop Until Not GoodLines(i).Contains(" ]")
                End If
                If GoodLines(i).Contains("[ ") Then
                    ErrorAudit += 1
                    Do
                        GoodLines(i) = GoodLines(i).Replace("[ ", "[")
                    Loop Until Not GoodLines(i).Contains("[ ")
                End If
                If GoodLines(i).Contains(",,") Then
                    ErrorAudit += 1
                    Do

                        GoodLines(i) = GoodLines(i).Replace(",,", ",")
                    Loop Until Not GoodLines(i).Contains(",,")
                End If
                If GoodLines(i).Contains(",]") Then
                    ErrorAudit += 1
                    Do

                        GoodLines(i) = GoodLines(i).Replace(",]", "]")
                    Loop Until Not GoodLines(i).Contains(",]")
                End If
                If GoodLines(i).Contains(" ,") Then
                    ErrorAudit += 1
                    Do

                        GoodLines(i) = GoodLines(i).Replace(" ,", ",")
                    Loop Until Not GoodLines(i).Contains(" ,")
                End If
                If foundFile.Contains("Suffering") Then Debug.Print(GoodLines(i))

                If GoodLines(i).Contains("@ShowBoobImage") Then
                    ErrorAudit += 1
                    GoodLines(i) = GoodLines(i).Replace("@ShowBoobImage", "@ShowBoobsImage")
                End If

            Next


            If Not foundFile.Contains("Received Files") Then

                Dim fs As New FileStream(foundFile, FileMode.Create)
                Dim sw As New StreamWriter(fs)


                For i As Integer = 0 To GoodLines.Count - 1
                    If i <> GoodLines.Count - 1 Then
                        sw.WriteLine(GoodLines(i))
                    Else
                        sw.Write(GoodLines(i))
                    End If
                Next


                sw.Close()
                sw.Dispose()

                fs.Close()
                fs.Dispose()

            End If

        Next




        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Images\System\", FileIO.SearchOption.SearchAllSubDirectories, "*.txt")

            LBLMaintenance.Text = "Checking " & Path.GetFileName(foundFile) & "..."
            PBMaintenance.Value += 1
            Dim CheckFiles As String() = File.ReadAllLines(foundFile)

            Dim GoodLines As New List(Of String)

            For Each line As String In CheckFiles
                If Not line = "" Then
                    GoodLines.Add(line)
                Else
                    BlankAudit += 1
                End If
            Next

            Dim fs As New FileStream(foundFile, FileMode.Create)
            Dim sw As New StreamWriter(fs)


            For i As Integer = 0 To GoodLines.Count - 1
                If i <> GoodLines.Count - 1 Then
                    sw.WriteLine(GoodLines(i))
                Else
                    sw.Write(GoodLines(i))
                End If
            Next

            sw.Close()
            sw.Dispose()

            fs.Close()
            fs.Dispose()

        Next


        Debug.Print("done")

        ' Github Patch
        MessageBox.Show(If(Me.Visible, Me, FrmSplash), PBMaintenance.Maximum & " scripts have been audited." & Environment.NewLine & Environment.NewLine &
                        "Blank lines cleared: " & BlankAudit & Environment.NewLine & Environment.NewLine &
                        "Script errors corrected: " & ErrorAudit, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)

        PBMaintenance.Value = 0

        LBLMaintenance.Text = ""

        BTNMaintenanceRebuild.Enabled = True
        BTNMaintenanceRefresh.Enabled = True


    End Sub

    Private Sub TBWebStart_LostFocus(sender As Object, e As EventArgs) Handles TBWebStart.LostFocus
        My.Settings.WebToyStart = TBWebStart.Text
    End Sub

    Private Sub TBWebStop_LostFocus(sender As Object, e As EventArgs) Handles TBWebStop.LostFocus
        My.Settings.WebToyStop = TBWebStop.Text
    End Sub

    Private Sub Button3_Click_2(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(Application.StartupPath & "\Images\Session Images\")
    End Sub

    ''' <summary>
    ''' Returns a tuple (Count, SpaceBytes)
    ''' </summary>
    ''' <param name="folder"></param>
    ''' <returns></returns>
    Public Function GetImageCountSize(folder As String) As Tuple(Of Int32, Int64)
        Dim imageCount As Integer = 0
        Dim imageSpace As Long = 0

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(folder, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
            imageCount += 1
            imageSpace += My.Computer.FileSystem.GetFileInfo(foundFile).Length
        Next

        Return Tuple.Create(imageCount, imageSpace)
    End Function

    ''' <summary>
    ''' Format a value in bytes in human readable format (KB, MB, etc)
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <returns></returns>
    Private Function FormatBytes(ByVal bytes As Long) As String
        Try
            Dim dblBytes As Double = bytes
            Dim Size As String() = {"bytes", "KB", "MB", "GB", "TB", "PB"}
            Dim SizeCounter As Integer = 0
            While dblBytes > 768
                dblBytes = dblBytes / 1024
                SizeCounter += 1
            End While
            dblBytes = CType(CType(dblBytes * 100, Integer), Double) * 0.01
            Return dblBytes.ToString & " " & Size(SizeCounter)
        Catch ex As Exception
            Return bytes.ToString()
        End Try
    End Function

    Private Sub Button6_Click_2(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("This will permanently delete all files in the Session Images folder." & Environment.NewLine & Environment.NewLine &
                                                "Are you sure you wish to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Images\Session Images\", FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                My.Computer.FileSystem.DeleteFile(foundFile)
            Next

            Dim imageInfo As Tuple(Of Int32, Int64) = GetImageCountSize(Application.StartupPath & "\Images\Session Images\")
            LBLSesFiles.Text = imageInfo.Item1.ToString()
            LBLSesSpace.Text = FormatBytes(imageInfo.Item2)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim result As DialogResult = MessageBox.Show("This will permanently reset all saved Tease AI settings back to their default value!" & Environment.NewLine & Environment.NewLine &
                                              "Are you sure you wish to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            My.Settings.Reset()
            MessageBox.Show(Me, "Tease AI settings have been reverted back to default values.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub CBCockToClit_LostFocus(sender As Object, e As EventArgs) Handles CBCockToClit.LostFocus
        mySettingsAccessor.CallCockAClit = CBCockToClit.Checked
    End Sub

    Private Sub CBBallsToPussy_LostFocus(sender As Object, e As EventArgs) Handles CBBallsToPussy.LostFocus
        mySettingsAccessor.CallBallsPussy = CBBallsToPussy.Checked
    End Sub

    Private Sub CBCockToClit_MouseHover(sender As Object, e As EventArgs) Handles CBCockToClit.MouseEnter
        LBLSubSettingsDescription.Text = "When this box is checked, the domme will replace #Cock with a Keyword for ""clit"" when it appears in a script" & Environment.NewLine & Environment.NewLine &
            "She will also replace the word ""stroking"" with words like ""rubbing, fingering, teasing"" etc."
    End Sub

    Private Sub CBBallsToPussy_MouseHover(sender As Object, e As EventArgs) Handles CBBallsToPussy.MouseEnter
        LBLSubSettingsDescription.Text = "When this box is checked, the domme will replace #Balls with a Keyword for ""pussy"" when it appears in a script" & Environment.NewLine & Environment.NewLine &
            "She will also replace ""those #Balls"" with ""that pussy"" to make the exchange more natural."
    End Sub

    Private Sub LBPlaylist_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) Handles LBPlaylist.DragDrop

        Debug.Print("Playlist DragDrop called? called?")
        If FrmSettingsLoading Then Return

        Dim LBPlaylistString As String = CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString
        LBPlaylistString = Path.GetFileName(LBPlaylistString).Replace(".txt", "")
        If RadioPlaylistRegScripts.Checked Then
            LBPlaylist.Items.Add(LBPlaylistString & " Regular-TeaseAI-Script")
        Else
            LBPlaylist.Items.Add(LBPlaylistString)
        End If

        ProcessPlaylist()


    End Sub

    Private Sub LBPlaylist_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs) Handles LBPlaylist.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Function StripBlankLines(ByVal SpaceClean As List(Of String)) As List(Of String)
        For i As Integer = SpaceClean.Count - 1 To 0 Step -1
            If SpaceClean(i) = "" Then SpaceClean.Remove(SpaceClean(i))
        Next
        Return SpaceClean
    End Function

    Private Sub BTNPlaylistEnd_Click(sender As Object, e As EventArgs) Handles BTNPlaylistEnd.Click

        Debug.Print("BTNPLaylistENd called?")
        If FrmSettingsLoading = True Or BTNPlaylistEnd.BackColor = Color.Blue Then Return

        If RadioPlaylistRegScripts.Checked Then
            WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\End")
        Else
            WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\End\")
        End If
        LBLPlaylIstLink.Enabled = False
        LBLPlaylIstLink.ForeColor = Color.Black
        BTNPlaylistEnd.ForeColor = Color.White
        LBLPlaylIstLink.BackColor = Color.LightGray
        BTNPlaylistEnd.BackColor = Color.Blue
        Return
    End Sub

    Private Sub RadioPlaylistScripts_CheckedChanged(sender As Object, e As EventArgs) Handles RadioPlaylistScripts.CheckedChanged, RadioPlaylistRegScripts.CheckedChanged
        Debug.Print("Radio CHanged called?")
        If FrmSettingsLoading = True Or MainWindow.FormLoading Then Return
        Debug.Print("Radio CHanged accepted??")
        If LBLPLaylistStart.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Start\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Start\")
            End If
            Return
        End If

        If BTNPlaylistEnd.BackColor = Color.Blue Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\End\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\End\")
            End If
            Return
        End If

        If LBLPlaylistModule.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Modules\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Modules\")
            End If
            Return
        End If

        If LBLPlaylIstLink.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Link\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Link\")
            End If
            Return
        End If


    End Sub

    Private Sub BTNPlaylistSave_Click(sender As Object, e As EventArgs) Handles BTNPlaylistSave.Click

        If TBPlaylistSave.Text = "" Or TBPlaylistSave.Text Is Nothing Then
            MessageBox.Show(Me, "Please enter a name for this Playlist!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        If File.Exists(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\" & TBPlaylistSave.Text & ".txt") Then
            Dim result As Integer
            result = MessageBox.Show(TBPlaylistSave.Text & ".txt already exists!" & Environment.NewLine & Environment.NewLine & "Do you wish to overwrite this file?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.No Then
                Return
            End If
        End If

        Dim PlaylistList As New List(Of String)
        For i As Integer = 0 To LBPlaylist.Items.Count - 1
            PlaylistList.Add(LBPlaylist.Items(i))
        Next

        Try
            System.IO.File.WriteAllLines(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\" & TBPlaylistSave.Text & ".txt", PlaylistList)
            MessageBox.Show(Me, "Playlist file has been saved successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            BTNPlaylistSave.Enabled = False
            LBPlaylist.Items.Clear()
            LBLPLaylistStart.Enabled = True
            LBLPLaylistStart.ForeColor = Color.White
            LBLPLaylistStart.BackColor = Color.Green
            BTNPlaylistCtrlZ.Enabled = False
            BTNPlaylistClearAll.Enabled = False
            If LBLPLaylistStart.Enabled Then
                If RadioPlaylistRegScripts.Checked Then
                    WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Start\")
                Else
                    WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Start\")
                End If
            End If
        Catch
            MessageBox.Show(Me, "Something went wrong and the Playlist file was not saved successfully.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Private Sub BTNPlaylistClearAll_Click(sender As Object, e As EventArgs) Handles BTNPlaylistClearAll.Click

        LBPlaylist.Items.Clear()
        BTNPlaylistSave.Enabled = False
        TBPlaylistSave.Text = ""
        LBLPLaylistStart.Enabled = True
        LBLPLaylistStart.ForeColor = Color.White
        LBLPLaylistStart.BackColor = Color.Green
        LBLPlaylistModule.Enabled = False
        LBLPlaylistModule.ForeColor = Color.Black
        LBLPlaylistModule.BackColor = Color.LightGray
        LBLPlaylIstLink.Enabled = False
        LBLPlaylIstLink.ForeColor = Color.Black
        LBLPlaylIstLink.BackColor = Color.LightGray
        BTNPlaylistEnd.Enabled = False
        BTNPlaylistEnd.ForeColor = Color.Black
        BTNPlaylistEnd.BackColor = Color.LightGray
        BTNPlaylistCtrlZ.Enabled = False
        BTNPlaylistClearAll.Enabled = False
        If LBLPLaylistStart.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Start\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Start\")
            End If
        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If BTNPlaylistSave.Enabled Then Return

        If LBLPLaylistStart.Enabled Then
            LBPlaylist.Items.Add("Random Start")
            ProcessPlaylist()
            Return
        End If

        If LBLPlaylistModule.Enabled Then
            LBPlaylist.Items.Add("Random Module")
            ProcessPlaylist()
            Return
        End If

        If LBLPlaylIstLink.Enabled Then
            LBPlaylist.Items.Add("Random Link")
            ProcessPlaylist()
            Return
        End If

        If BTNPlaylistEnd.BackColor = Color.Blue Then
            LBPlaylist.Items.Add("Random End")
            ProcessPlaylist()
            Return
        End If

    End Sub

    Public Sub ProcessPlaylist()

        If BTNPlaylistEnd.BackColor = Color.Blue Then
            WBPlaylist.Navigate("about:blank")
            BTNPlaylistEnd.Enabled = False
            BTNPlaylistEnd.ForeColor = Color.Black
            BTNPlaylistEnd.BackColor = Color.LightGray
            BTNPlaylistSave.Enabled = True
            Return
        End If

        If LBLPLaylistStart.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Modules\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Modules\")
            End If

            LBLPLaylistStart.Enabled = False
            LBLPlaylistModule.ForeColor = Color.White
            LBLPLaylistStart.ForeColor = Color.Black
            LBLPlaylistModule.BackColor = Color.Green
            LBLPLaylistStart.BackColor = Color.LightGray
            LBLPlaylistModule.Enabled = True
            BTNPlaylistCtrlZ.Enabled = True
            BTNPlaylistClearAll.Enabled = True
            Return
        End If

        If LBLPlaylistModule.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Link")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Link\")
            End If
            LBLPlaylistModule.Enabled = False
            LBLPlaylIstLink.Enabled = True
            BTNPlaylistEnd.Enabled = True
            LBLPlaylistModule.ForeColor = Color.Black
            LBLPlaylIstLink.ForeColor = Color.White
            BTNPlaylistEnd.ForeColor = Color.White
            LBLPlaylistModule.BackColor = Color.LightGray
            LBLPlaylIstLink.BackColor = Color.Green
            BTNPlaylistEnd.BackColor = Color.Green
            Return
        End If

        If LBLPlaylIstLink.Enabled Then
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Modules\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Modules\")
            End If
            LBLPlaylIstLink.Enabled = False
            LBLPlaylistModule.Enabled = True
            BTNPlaylistEnd.Enabled = False
            LBLPlaylistModule.ForeColor = Color.White
            LBLPlaylIstLink.ForeColor = Color.Black
            BTNPlaylistEnd.ForeColor = Color.Black
            LBLPlaylistModule.BackColor = Color.Green
            LBLPlaylIstLink.BackColor = Color.LightGray
            BTNPlaylistEnd.BackColor = Color.LightGray
            Return
        End If

    End Sub

    Private Sub BTNPlaylistCtrlZ_Click(sender As Object, e As EventArgs) Handles BTNPlaylistCtrlZ.Click

        If BTNPlaylistEnd.BackColor = Color.Blue Then
            If BTNPlaylistSave.Enabled Then
                LBPlaylist.Items.RemoveAt(LBPlaylist.Items.Count - 1)
                BTNPlaylistSave.Enabled = False
            End If
        End If

        If BTNPlaylistEnd.BackColor = Color.Blue Then
            LBLPlaylIstLink.Enabled = True
            LBLPlaylIstLink.ForeColor = Color.White
            LBLPlaylIstLink.BackColor = Color.Green
            BTNPlaylistEnd.Enabled = True
            BTNPlaylistEnd.ForeColor = Color.White
            BTNPlaylistEnd.BackColor = Color.Green
            LBLPlaylistModule.Enabled = False
            LBLPlaylistModule.ForeColor = Color.Black
            LBLPlaylistModule.BackColor = Color.LightGray
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Link\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Link\")
            End If
            Return
        End If

        LBPlaylist.Items.RemoveAt(LBPlaylist.Items.Count - 1)

        If LBPlaylist.Items.Count = 0 Then
            LBLPLaylistStart.Enabled = True
            LBLPLaylistStart.ForeColor = Color.White
            LBLPLaylistStart.BackColor = Color.Green
            LBLPlaylistModule.Enabled = False
            LBLPlaylistModule.ForeColor = Color.Black
            LBLPlaylistModule.BackColor = Color.LightGray
            BTNPlaylistCtrlZ.Enabled = False
            BTNPlaylistClearAll.Enabled = False
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Start\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Start\")
            End If
            Return
        End If

        If LBLPlaylistModule.Enabled Then
            LBLPlaylIstLink.Enabled = True
            LBLPlaylIstLink.ForeColor = Color.White
            LBLPlaylIstLink.BackColor = Color.Green
            BTNPlaylistEnd.Enabled = True
            BTNPlaylistEnd.ForeColor = Color.White
            BTNPlaylistEnd.BackColor = Color.Green
            LBLPlaylistModule.Enabled = False
            LBLPlaylistModule.ForeColor = Color.Black
            LBLPlaylistModule.BackColor = Color.LightGray
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Stroke\Link\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Link\")
            End If
            Return
        End If

        If LBLPlaylIstLink.Enabled Then
            LBLPlaylistModule.Enabled = True
            LBLPlaylistModule.ForeColor = Color.White
            LBLPlaylistModule.BackColor = Color.Green
            BTNPlaylistEnd.Enabled = False
            BTNPlaylistEnd.ForeColor = Color.Black
            BTNPlaylistEnd.BackColor = Color.LightGray
            LBLPlaylIstLink.Enabled = False
            LBLPlaylIstLink.ForeColor = Color.Black
            LBLPlaylIstLink.BackColor = Color.LightGray
            If RadioPlaylistRegScripts.Checked Then
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Modules\")
            Else
                WBPlaylist.Navigate(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\Playlist\Modules\")
            End If
            Return
        End If



    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles Button11.Click

        If MsgBox("This will change the Chastity state of Tease AI. Depending on the Personality or Scripts used so far, this could cause unexpected behavior or break certain scripts." & Environment.NewLine _
                  & Environment.NewLine & "It is recommended to only change this state if you are otherwise stuck. Are you sure you wish to change the Chastity state?", vbYesNo, "Warning!") = MsgBoxResult.Yes Then
            If My.Settings.Chastity Then
                My.Settings.Chastity = False
                LBLChastityState.Text = "OFF"
                LBLChastityState.ForeColor = Color.Red
            Else
                My.Settings.Chastity = True
                LBLChastityState.Text = "ON"
                LBLChastityState.ForeColor = Color.Green
            End If
        Else
            Return
        End If


    End Sub

    Public Sub EnglishMenu()

        LBLGeneralSettings.Text = "General Settings"

        GBGeneralSettings.Text = "Chat Window"

        TimeStampCheckBox.Text = "Show Timestamps"
        ShowNamesCheckBox.Text = "Always Show Names"
        TypeInstantlyCheckBox.Text = "Domme Types Instantly"
        CBInputIcon.Text = "Show Icon During Input Questions"

        GBDommeFont.Text = "Domme Font Settings"
        BTNDomColor.Text = "Domme Name Color"
        LBLDomColor.Text = "Preview"

        GBSubFont.Text = "Sub Font Settings"
        BTNSubColor.Text = "Sub Name Color"
        LBLSubColor.Text = "Preview"

        GBGeneralImages.Text = "Images"

        CBBlogImageWindow.Text = "Save Blog Images From Session"
        CBSlideshowSubDir.Text = "Slideshow Includes Subdirectories"
        CBSlideshowRandom.Text = "Display Slideshow Pictures Randomly"
        LandscapeCheckBox.Text = "Stretch Landscape Images"
        CBImageInfo.Text = "Display Image Information"

        GBDommeImages.Text = "Slideshow Options"
        BTNDomImageDir.Text = "Set Domme Images Directory"

        'GBSlideshowOptions.Text = "Slideshow Options"
        ManualSlideShowRadio.Text = "Manual"
        TeaseSlideShowRadio.Text = "Tease"

        GBGeneralSystem.Text = "System"
        CBAuditStartup.Text = "Audit Scripts on Startup"
        CBSettingsPause.Text = "Pause Program When Settings Menu is Visible"
        CBAutosaveChatlog.Text = "Autosave Chatlog"
        CBSaveChatlogExit.Text = "Save Unique Chatlog on Exit"
        CBDomDel.Text = "Allow Domme to Delete Local Media"

        GBSafeword.Text = "Safeword"
        LBLSafeword.Text = "Enter a safeword that will stop all activity until the domme is sure you're able to continue."

        GBGeneralTextToSpeech.Text = "Text to Speech"
        TTSCheckBox.Text = "Enable"

        ' GBGeneralDesc.Text = "Description"
        'LBLGeneralSettingsDescription.Text = "Hover over any setting in the menu for a more detailed description of its function."

    End Sub

    Public Sub GermanMenu()
        LBLGeneralSettings.Text = "Allgemeine Einstellung"

        GBGeneralSettings.Text = "Chat Fenster"

        TimeStampCheckBox.Text = "Zeige Zeitstempel"
        ShowNamesCheckBox.Text = "Zeige immer die Namen"
        TypeInstantlyCheckBox.Text = "Domina Schreibt sofort"
        'CBInputIcon.Text = "Deaktiviere Chat Fenster Verstellung"

        GBDommeFont.Text = "Domina Schrift Einstellungen"
        BTNDomColor.Text = "Domina Farbe"
        LBLDomColor.Text = "Vorschau"

        GBSubFont.Text = "Sklaven Schrift Einstellungen"
        BTNSubColor.Text = "Sklaven Farbe"
        LBLSubColor.Text = "Vorschau"

        GBGeneralImages.Text = "Bilder"

        CBBlogImageWindow.Text = "Speichere Blog Bilder von Sitzung"
        CBSlideshowSubDir.Text = "Diashows enthalten Unterordner"
        CBSlideshowRandom.Text = "Zeige Diashow Bilder zufällig"
        LandscapeCheckBox.Text = "Strecke „Landschaftsbilder"""
        CBImageInfo.Text = "Zeige Bild Informationen"

        GBDommeImages.Text = "Diashow Einstellungen"
        BTNDomImageDir.Text = "Wähle Domina Bilder Speicherpfad"

        ManualSlideShowRadio.Text = "Manual"
        TeaseSlideShowRadio.Text = "Tease"

        GBGeneralSystem.Text = "System"
        CBAuditStartup.Text = "Prüfen der Scripts beim Starten"
        CBSettingsPause.Text = "Pause wenn Einstellungsmenü geöffnet ist"
        CBAutosaveChatlog.Text = "Autospeichern des Chatverlaufs"
        CBSaveChatlogExit.Text = "Speichert beim beenden einen Chatverlauf"
        CBDomDel.Text = "Erlaube Domina Lokale Medien zu löschen"

        GBSafeword.Text = "Safeword"
        LBLSafeword.Text = "Gebe hier dein Safeword ein, welches alle Aktivitäten der Domina stopt, bis sie sicher ist, das du weiter machen kannst."

        GBGeneralTextToSpeech.Text = "Text zu Sprache"
        TTSCheckBox.Text = "Aktiv"
    End Sub

    Private Sub RBGerman_CheckedChanged(sender As Object, e As EventArgs) Handles RBGerman.CheckedChanged, RBEnglish.CheckedChanged
        If FrmSettingsLoading = False Then

            If RBGerman.Checked Then
                GermanMenu()
                My.Settings.TeaseAILanguage = "German"
            End If

            If RBEnglish.Checked Then
                EnglishMenu()
                My.Settings.TeaseAILanguage = "English"
            End If
        End If
    End Sub

    Private Sub ThemeLbl_Click(sender As Object, e As EventArgs) Handles LBLBackColor2.Click, LBLButtonColor2.Click, LBLTextColor2.Click, LBLDateTimeColor2.Click, LBLDateBackColor2.Click, LBLChatWindowColor2.Click, LBLChatTextColor2.Click
        If GetColor.ShowDialog() = DialogResult.OK Then
            MainWindow.SuspendLayout()
            CType(sender, Label).BackColor = GetColor.Color
            MainWindow.ResumeLayout()
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                MainWindow.PnlLayoutForm.BackgroundImage.Dispose()
            Catch
            End Try
            MainWindow.PnlLayoutForm.BackgroundImage = Nothing
            GC.Collect()
            MainWindow.PnlLayoutForm.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
            My.Settings.BackgroundImage = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Try
            MainWindow.PnlLayoutForm.BackgroundImage.Dispose()
            PBBackgroundPreview.Image.Dispose()
        Catch
        End Try
        MainWindow.PnlLayoutForm.BackgroundImage = Nothing
        PBBackgroundPreview.Image = Nothing
        GC.Collect()
        My.Settings.BackgroundImage = ""
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        OpenScriptDialog.Title = "Select a Theme settings file"
        OpenScriptDialog.InitialDirectory = Application.StartupPath & "\System\"

        If OpenScriptDialog.ShowDialog() = DialogResult.OK Then

            Dim SettingsList As New List(Of String)

            Try
                Dim SettingsReader As New StreamReader(OpenScriptDialog.FileName)
                While SettingsReader.Peek <> -1
                    SettingsList.Add(SettingsReader.ReadLine())
                End While
                SettingsReader.Close()
                SettingsReader.Dispose()
            Catch ex As Exception
                MessageBox.Show(Me, "This file could not be opened!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End Try

            Try
                If File.Exists(SettingsList(0).Replace("Background Image: ", "")) Then
                    PBBackgroundPreview.Image = Image.FromFile(SettingsList(0).Replace("Background Image: ", ""))
                    My.Settings.BackgroundImage = SettingsList(0).Replace("Background Image: ", "")
                End If

                CBStretchBack.Checked = SettingsList(1).Replace("Stretch Image: ", "")

                My.Settings.BackgroundColor = Color.FromArgb(SettingsList(2).Replace("Background Color: ", ""))
                My.Settings.ButtonColor = Color.FromArgb(SettingsList(3).Replace("Button Color: ", ""))
                My.Settings.TextColor = Color.FromArgb(SettingsList(4).Replace("Text Color: ", ""))
                My.Settings.ChatWindowColor = Color.FromArgb(SettingsList(5).Replace("Chat Window Color: ", ""))
                My.Settings.ChatTextColor = Color.FromArgb(SettingsList(6).Replace("Chat Text Color: ", ""))

                My.Settings.DateTextColor = Color.FromArgb(SettingsList(7).Replace("Date Text Color: ", ""))
                My.Settings.DateBackColor = Color.FromArgb(SettingsList(8).Replace("Date Back Color: ", ""))
                CBTransparentTime.Checked = SettingsList(9).Replace("Transparent Date: ", "")

                CBFlipBack.Checked = SettingsList(10).Replace("FlipImage: ", "")



            Catch
                MessageBox.Show(Me, "This Theme settings file is invalid or has been edited incorrectly!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End Try

            MainWindow.ApplyThemeColor()


        End If
    End Sub

    Private Sub CBStretchBack_CheckedChanged(sender As Object, e As EventArgs) Handles CBStretchBack.CheckedChanged
        My.Settings.BackgroundStretch = CBStretchBack.Checked
    End Sub

    Private Sub CBTransparentTime_CheckedChanged(sender As Object, e As EventArgs) Handles CBTransparentTime.CheckedChanged
        My.Settings.CBDateTransparent = CBTransparentTime.Checked
    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CBFlipBack.CheckedChanged

        Try
            If MainWindow.FormLoading = False And MainWindow.ApplyingTheme = False Then MainWindow.ApplyThemeColor()
        Catch
        End Try

    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        SaveFileDialog1.Title = "Select a location to save current Theme"
        SaveFileDialog1.InitialDirectory = Application.StartupPath & "\System\"


        SaveFileDialog1.FileName = "Theme.txt"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim SettingsPath As String = SaveFileDialog1.FileName
            Dim SettingsList As New List(Of String)
            SettingsList.Clear()

            SettingsList.Add("Background Image: " & My.Settings.BackgroundImage)
            SettingsList.Add("Stretch Image: " & CBStretchBack.Checked)

            SettingsList.Add("Background Color: " & My.Settings.BackgroundColor.ToArgb.ToString)
            SettingsList.Add("Button Color: " & My.Settings.ButtonColor.ToArgb.ToString)
            SettingsList.Add("Text Color: " & My.Settings.TextColor.ToArgb.ToString)
            SettingsList.Add("Chat Window Color: " & My.Settings.ChatWindowColor.ToArgb.ToString)
            SettingsList.Add("Chat Text Color: " & My.Settings.ChatTextColor.ToArgb.ToString)
            SettingsList.Add("Date Text Color: " & My.Settings.DateTextColor.ToArgb.ToString)
            SettingsList.Add("Date Back Color: " & My.Settings.DateBackColor.ToArgb.ToString)
            SettingsList.Add("Transparent Date: " & CBTransparentTime.Checked)

            SettingsList.Add("FlipImage: " & CBFlipBack.Checked)

            Dim SettingsString As String = ""

            For i As Integer = 0 To SettingsList.Count - 1
                SettingsString = SettingsString & SettingsList(i)
                If i <> SettingsList.Count - 1 Then SettingsString = SettingsString & Environment.NewLine
            Next

            My.Computer.FileSystem.WriteAllText(SettingsPath, SettingsString, False)
        End If

    End Sub

    Private Sub TimeBoxWakeUp_ValueChanged(sender As Object, e As EventArgs) Handles TimeBoxWakeUp.ValueChanged
        If MainWindow.FormLoading = False Then


            Dim SetDate As Date = FormatDateTime(TimeBoxWakeUp.Value, DateFormat.LongTime)

            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Scripts\" & MainWindow.dompersonalitycombobox.Text & "\System\Variables\SYS_WakeUp", FormatDateTime(SetDate, DateFormat.LongTime), False)



            'Debug.Print("Dates = " & Dates)

            ' Github Patch My.Settings.WakeUp = Form1.GetTime("SYS_WakeUp")
            My.Settings.WakeUp = FormatDateTime(Now, DateFormat.ShortDate) & " " & MainWindow.GetTime("SYS_WakeUp")



            Debug.Print(MainWindow.ssh.GeneralTime)




        End If
    End Sub

    Private Sub NBTypoChance_LostFocus(sender As Object, e As EventArgs) Handles NBTypoChance.LostFocus
        My.Settings.TypoChance = NBTypoChance.Value
    End Sub

    Private Sub SliderVVolume_LostFocus(sender As Object, e As EventArgs) Handles SliderVVolume.LostFocus
        My.Settings.VVolume = SliderVVolume.Value
    End Sub

    Private Sub SliderVRate_LostFocus(sender As Object, e As EventArgs) Handles SliderVRate.LostFocus
        My.Settings.VRate = SliderVRate.Value
    End Sub

    Private Sub SliderVVolume_Scroll(sender As Object, e As EventArgs) Handles SliderVVolume.Scroll
        MainWindow.synth.Volume = SliderVVolume.Value
        MainWindow.synth2.Volume = SliderVVolume.Value
        LBLVVolume.Text = SliderVVolume.Value
    End Sub

    Private Sub SliderVRate_Scroll(sender As Object, e As EventArgs) Handles SliderVRate.Scroll
        MainWindow.synth.Rate = SliderVRate.Value
        MainWindow.synth2.Rate = SliderVRate.Value
        LBLVRate.Text = SliderVRate.Value
    End Sub

    Private Sub sadisticCheckBox_LostFocus(sender As Object, e As EventArgs) Handles sadisticCheckBox.LostFocus
        My.Settings.DomSadistic = sadisticCheckBox.Checked
    End Sub

    Private Sub degradingCheckBox_LostFocus(sender As Object, e As EventArgs) Handles degradingCheckBox.LostFocus
        My.Settings.DomDegrading = degradingCheckBox.Checked
    End Sub

    Private Sub CBWebtease_CheckedChanged_1(sender As Object, e As EventArgs) Handles WebTeaseMode.CheckedChanged
        If WebTeaseMode.Checked Then
            MainWindow.WebteaseModeToolStripMenuItem.Checked = True
            MainWindow.ChatText.ScrollBarsEnabled = False
            MainWindow.ChatText2.ScrollBarsEnabled = False
        Else
            MainWindow.WebteaseModeToolStripMenuItem.Checked = False
            MainWindow.ChatText.ScrollBarsEnabled = True
            MainWindow.ChatText2.ScrollBarsEnabled = True

        End If
    End Sub

    Private Sub BTNDebugTauntsClear_Click(sender As Object, e As EventArgs) Handles BTNDebugTauntsClear.Click
        TBDebugTaunts1.Text = ""
        TBDebugTaunts2.Text = ""
        TBDebugTaunts3.Text = ""
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        MainWindow.ssh.StrokeTick = 5
    End Sub

    Private Sub CBMuteMedia_CheckedChanged(sender As Object, e As EventArgs) Handles CBMuteMedia.CheckedChanged

        MainWindow.DomWMP.settings.mute = CBMuteMedia.Checked

    End Sub

    Private Sub CBMuteMedia_LostFocus(sender As Object, e As EventArgs) Handles CBMuteMedia.LostFocus
        My.Settings.MuteMedia = CBMuteMedia.Checked
    End Sub

    Private Sub BTNOfflineMode_Click(sender As Object, e As EventArgs) Handles BTNOfflineMode.Click
        mySettingsAccessor.IsOnline = Not mySettingsAccessor.IsOnline
        If mySettingsAccessor.IsOnline Then
            LBLOfflineMode.Text = "ON"
            LBLOfflineMode.ForeColor = Color.Green
        Else
            LBLOfflineMode.Text = "OFF"
            LBLOfflineMode.ForeColor = Color.Red
        End If
    End Sub

    Private Sub CBNewSlideshow_LostFocus(sender As Object, e As EventArgs) Handles CBNewSlideshow.LostFocus
        If CBNewSlideshow.Checked Then
            My.Settings.CBNewSlideshow = True
        Else
            My.Settings.CBNewSlideshow = False
        End If
    End Sub

    Private Sub NBTauntEdging_LostFocus(sender As Object, e As EventArgs) Handles NBTauntEdging.LostFocus
        My.Settings.TauntEdging = NBTauntEdging.Value
    End Sub

    Private Sub BTNDebugTeaseTimer_Click(sender As Object, e As EventArgs) Handles BTNDebugTeaseTimer.Click
        MainWindow.ssh.TeaseTick = 5
    End Sub

    Private Sub BTNDebugStrokeTime_Click(sender As Object, e As EventArgs) Handles BTNDebugStrokeTime.Click
        MainWindow.ssh.StrokeTick = 5
    End Sub

    Private Sub BTNDebugStrokeTauntTimer_Click(sender As Object, e As EventArgs) Handles BTNDebugStrokeTauntTimer.Click
        MainWindow.ssh.StrokeTauntTick = 5
    End Sub

    Private Sub BTNDebugEdgeTauntTimer_Click(sender As Object, e As EventArgs) Handles BTNDebugEdgeTauntTimer.Click
        MainWindow.ssh.EdgeTauntInt = 5
    End Sub

    Private Sub BTNDebugHoldEdgeTimer_Click(sender As Object, e As EventArgs) Handles BTNDebugHoldEdgeTimer.Click
        MainWindow.ssh.HoldEdgeTick = 5
    End Sub

    Private Sub CBURLPreview_LostFocus(sender As Object, e As EventArgs) Handles CBURLPreview.LostFocus
        My.Settings.CBURLPreview = CBURLPreview.Checked
    End Sub

    Private Sub NBTaskStrokesMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskStrokesMin.ValueChanged
        If NBTaskStrokesMin.Value > NBTaskStrokesMax.Value Then NBTaskStrokesMin.Value = NBTaskStrokesMax.Value
    End Sub
    Private Sub NBTaskStrokesMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskStrokesMax.ValueChanged
        If NBTaskStrokesMax.Value < NBTaskStrokesMin.Value Then NBTaskStrokesMax.Value = NBTaskStrokesMin.Value
    End Sub

    Private Sub NBTaskStrokingTimeMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskStrokingTimeMin.ValueChanged
        If NBTaskStrokingTimeMin.Value > NBTaskStrokingTimeMax.Value Then NBTaskStrokingTimeMin.Value = NBTaskStrokingTimeMax.Value
    End Sub
    Private Sub NBTaskStrokingTimeMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskStrokingTimeMax.ValueChanged
        If NBTaskStrokingTimeMax.Value < NBTaskStrokingTimeMin.Value Then NBTaskStrokingTimeMax.Value = NBTaskStrokingTimeMin.Value
    End Sub

    Private Sub NBTaskEdgesMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskEdgesMin.ValueChanged
        If NBTaskEdgesMin.Value > NBTaskEdgesMax.Value Then NBTaskEdgesMin.Value = NBTaskEdgesMax.Value
    End Sub
    Private Sub NBTaskEdgesMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskEdgesMax.ValueChanged
        If NBTaskEdgesMax.Value < NBTaskEdgesMin.Value Then NBTaskEdgesMax.Value = NBTaskEdgesMin.Value
    End Sub

    Private Sub NBTaskEdgeHoldTimeMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskEdgeHoldTimeMin.ValueChanged
        If NBTaskEdgeHoldTimeMin.Value > NBTaskEdgeHoldTimeMax.Value Then NBTaskEdgeHoldTimeMin.Value = NBTaskEdgeHoldTimeMax.Value
    End Sub
    Private Sub NBTaskEdgeHoldTimeMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskEdgeHoldTimeMax.ValueChanged
        If NBTaskEdgeHoldTimeMax.Value < NBTaskEdgeHoldTimeMin.Value Then NBTaskEdgeHoldTimeMax.Value = NBTaskEdgeHoldTimeMin.Value
    End Sub

    Private Sub NBTaskCBTTimeMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskCBTTimeMin.ValueChanged
        If NBTaskCBTTimeMin.Value > NBTaskCBTTimeMax.Value Then NBTaskCBTTimeMin.Value = NBTaskCBTTimeMax.Value
    End Sub
    Private Sub NBTaskCBTTimeMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTaskCBTTimeMax.ValueChanged
        If NBTaskCBTTimeMax.Value < NBTaskCBTTimeMin.Value Then NBTaskCBTTimeMax.Value = NBTaskCBTTimeMin.Value
    End Sub

    Private Sub NBTasksMin_ValueChanged(sender As Object, e As EventArgs) Handles NBTasksMin.ValueChanged
        If NBTasksMin.Value > NBTasksMax.Value Then NBTasksMin.Value = NBTasksMax.Value
    End Sub

    Private Sub NBTasksMax_ValueChanged(sender As Object, e As EventArgs) Handles NBTasksMax.ValueChanged
        If NBTasksMax.Value < NBTasksMin.Value Then NBTasksMax.Value = NBTasksMin.Value
    End Sub

    Private Sub TypeSpeedSlider_Scroll(sender As Object, e As EventArgs) Handles TypeSpeedSlider.Scroll
        TypesSpeedVal.Text = TypeSpeedSlider.Value
    End Sub

    Private Function ConvertHoldTime(holdTimeSeconds As Int16) As Int16
        If (ConvertHoldTimeUnits(holdTimeSeconds) = "minutes") Then
            Return holdTimeSeconds / 60
        End If
        Return holdTimeSeconds
    End Function

    Private Function ConvertHoldTime(holdTime As Int16, holdTimeUnits As String) As Int16
        If (holdTimeUnits = "minutes") Then
            Return holdTime * 60
        End If
        Return holdTime
    End Function

    Private Function ConvertHoldTimeUnits(holdTimeSeconds As Int16) As String
        If (holdTimeSeconds > 60) Then
            Return "minutes"
        End If
        Return "seconds"
    End Function

    Delegate Function GetScriptsDelegate(stage As String) As List(Of String)

    Public Function GetAvailableScripts(stage As String) As List(Of String)
        Dim getScripts As New GetScriptsDelegate(AddressOf DoGetScripts)
        If (stage = "Start") Then
            Return Invoke(getScripts, stage)
        End If
        'If FrmSettings.StartScripts.Items(x) = scriptName AndAlso FrmSettings.StartScripts.GetItemChecked(x) Then
    End Function

    Private Function DoGetScripts(stage As String) As List(Of String)
        Dim scriptList As List(Of String) = New List(Of String)
        For x As Integer = 0 To StartScripts.Items.Count - 1
            If StartScripts.GetItemChecked(x) Then
                scriptList.Add(StartScripts.Items(x))
            End If
        Next
        Return scriptList
    End Function

    ''' <summary>
    ''' Determine requirements for <paramref name="scriptMetaData"/>
    ''' </summary>
    ''' <param name="scriptMetaData"></param>
    Public Function GetScriptRequirements(scriptMetaData As ScriptMetaData) As Tuple(Of Boolean, String)
        Dim requirements As List(Of String) = New List(Of String)()
        Dim getScript As Result(Of Script) = myScriptAccessor.GetScript(scriptMetaData)
        If (getScript.IsFailure) Then
            Throw New ApplicationException(getScript.Error.Message)
        End If
        Dim script As Script = getScript.Value
        Dim areScriptRequirementsMet As Boolean = True
        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowBlogImage) OrElse l.Contains(Keyword.NewBlogImage) OrElse l.Contains(Keyword.ShowImage)) Then
            requirements.Add("* At least one URL File Selected *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso URLFileList.CheckedItems.Count > 0
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowLocalImage) OrElse l.Contains(Keyword.ShowImage)) Then
            requirements.Add("* At least one Local Image path selected with a valid directory *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.IsImageGenreEnabled.Values.Any(Function(isEn) isEn)
        End If

        If script.Lines.Any(Function(l) l.Contains("@CBTBalls")) Then
            requirements.Add("* Ball Torture must be enabled *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.IsBallTortureEnabled
        End If

        If script.Lines.Any(Function(l) l.Contains("@CBTCock")) Then
            requirements.Add("* Cock Torture must be enabled *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.IsCockTortureEnabled
        End If

        If script.Lines.Any(Function(l) Not l.Contains("@CBTCock") AndAlso Not l.Contains("@CBTBalls") AndAlso l.Contains("@CBT")) Then
            requirements.Add("* Cock Torture must be enabled *")
            requirements.Add("* Ball Torture must be enabled *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.IsCockTortureEnabled AndAlso mySettingsAccessor.IsBallTortureEnabled
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.PlayJoiVideo)) Then
            requirements.Add("* JOI or JOI Domme Video path selected with a valid directory *")
            areScriptRequirementsMet = areScriptRequirementsMet _
                    AndAlso ((CBVideoJOI.Checked AndAlso Convert.ToInt32(LblVideoJOITotal.Text) > 0) _
                        OrElse (CBVideoJOID.Checked AndAlso Convert.ToInt32(LblVideoJOITotalD.Text) > 0)
                    )
        End If

        If script.Lines.Any(Function(l) l.Contains("PlayCHVideo")) Then
            requirements.Add("* CH or CH Domme Video path selected with a valid directory *")
            areScriptRequirementsMet = areScriptRequirementsMet _
                    AndAlso ((CBVideoCH.Checked AndAlso Convert.ToInt32(LblVideoCHTotal.Text) > 0) _
                        OrElse (CBVideoCHD.Checked AndAlso Convert.ToInt32(LblVideoCHTotalD.Text) > 0)
                    )
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowButtImage) OrElse l.Contains(Keyword.ShowButtsImage)) Then
            requirements.Add("* BnB Butt path must be set to a valid directory or URL File *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                    ((mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Butt) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Butt))) OrElse
                    (ChbImageUrlButts.Checked AndAlso File.Exists(My.Settings.UrlFileButt)))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowBoobImage) OrElse l.Contains(Keyword.ShowBoobsImage)) Then
            requirements.Add("* BnB Boobs path must be set to a valid directory or URL File *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                ((mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Boobs) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Boobs))) OrElse
                (ChbImageUrlButts.Checked AndAlso File.Exists(My.Settings.UrlFileButt)))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowHardcoreImage)) Then
            requirements.Add("* Local Hardcore images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Hardcore) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Hardcore))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowLesbianImage)) Then
            requirements.Add("* Local Lesbian images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Lesbian) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Lesbian))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowBlowjobImage)) Then
            requirements.Add("* Local Blowjob images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Blowjob) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Blowjob))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowFemdomImage)) Then
            requirements.Add("* Local Femdom images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Femdom) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Femdom))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowLezdomImage)) Then
            requirements.Add("* Local Lezdom images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Lezdom) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Lezdom))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowHentaiImage)) Then
            requirements.Add("* Local Hentai images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Hentai) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Hentai))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowGayImage)) Then
            requirements.Add("* Local gay images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Gay) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Gay))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowMaledomImage)) Then
            requirements.Add("* Local maledom images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Maledom) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Maledom))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowCaptionsImage)) Then
            requirements.Add("* Local caption images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.Captions) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.Captions))
        End If

        If script.Lines.Any(Function(l) l.Contains(Keyword.ShowGeneralImage)) Then
            requirements.Add("* Local general images must be enabled and set to a valid directory  *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso
                mySettingsAccessor.IsImageGenreEnabled(ImageGenre.General) AndAlso File.Exists(mySettingsAccessor.ImageGenreFolder(ImageGenre.General))
        End If

        If script.Lines.Any(Function(l) l.Contains("@ShowTaggedImage") AndAlso l.Contains("@Tag")) Then
            Dim tagImageList As List(Of String) = New List(Of String)()

            Dim tagFile = LocalImageTagFile
            If File.Exists(tagFile) Then
                tagImageList = File.ReadAllText(tagFile) _
                    .Split(" ") _
                    .Where(Function(l) l.StartsWith("Tag")) _
                    .Distinct() _
                    .ToList()
            End If

            Dim scriptTags As List(Of String) = String.Join(" ", script.Lines.All(Function(l) l.Contains("@Tag"))) _
                .Split(" ") _
                .Where(Function(l) l.StartsWith("@Tag")) _
                .Select(Function(l) l.Replace("@Tag", "Tag")) _
                .Distinct() _
                .ToList()

            Dim missingTags As List(Of String) = scriptTags.Where(Function(l) Not tagFile.Contains(l)).ToList()
            requirements.Add("* Images in LocalImageTags.txt tagged with: " & String.Join(" ", missingTags))
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso Not missingTags.Any()
        End If

        If script.Lines.Any(Function(l) l.Contains("@ShowTaggedImage") AndAlso Not l.Contains("@Tag")) Then
            Dim tagFile = LocalImageTagFile
            requirements.Add("* LocalImageTags.txt must exist in \Images\System\ *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso File.Exists(tagFile)
        End If

        If script.Lines.Any(Function(l) l.Contains("@CheckVideo")) Then
            MainWindow.ssh.VideoCheck = True
            MainWindow.RandomVideo()
            requirements.Add("* At least one Genre or Domme Video path set and selected *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso Not MainWindow.ssh.NoVideo
            MainWindow.ssh.VideoCheck = False
            MainWindow.ssh.NoVideo = False
        End If

        ' Need to find out if we have videos for the next few
        MainWindow.ssh.VideoCheck = True
        MainWindow.RandomVideo()
        Dim hasVideos = Not MainWindow.ssh.NoVideo
        MainWindow.ssh.VideoCheck = False
        MainWindow.ssh.NoVideo = False

        If script.Lines.Any(Function(l) l.Contains("@CheckVideo")) Then
            requirements.Add("* At least one Genre or Domme Video path set and selected *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso hasVideos
        End If

        If script.Lines.Any(Function(l) l.Contains("@PlayCensorShipSucks") OrElse l.Contains("@PlayAvoidTheEdge") OrElse l.Contains("@PlayRedLightGreenLight")) Then
            requirements.Add("* At least one non-Special Genre or Domme Video path set and selected *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso hasVideos
        End If

        If script.Lines.Any(Function(l) l.Contains("@ChastityOn") OrElse l.Contains("@ChastityOff")) Then
            requirements.Add("* You must indicate you own a chastity device *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.HasChastityDevice
        End If

        If script.Lines.Any(Function(l) l.Contains("@DeleteLocalImage")) Then
            requirements.Add("* ""Allow Domme to Delete Local Media"" muct be checked *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.CanDommeDeleteFiles
        End If

        If script.Lines.Any(Function(l) l.Contains("@DeleteLocalImage")) Then
            requirements.Add("* ""Allow Domme to Delete Local Media"" muct be checked *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso mySettingsAccessor.CanDommeDeleteFiles
        End If

        If script.Lines.Any(Function(l) l.Contains("@VitalSubAssignment")) Then
            requirements.Add("* ""Allow Domme to Delete Local Media"" muct be checked *")
            requirements.Add("* VitalSub must be enabled *")
            requirements.Add("* ""Domme Assignments"" must be checked in the VitalSub app *")
            areScriptRequirementsMet = areScriptRequirementsMet AndAlso MainWindow.CBVitalSub.Checked AndAlso MainWindow.CBVitalSubDomTask.Checked
        End If
        Return Tuple.Create(areScriptRequirementsMet, String.Join(Environment.NewLine, requirements).Replace("**", "* *"))
    End Function

    ''' <summary>
    ''' Load script metadata into the checked list box
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="dommePersonalityName"></param>
    ''' <param name="type"></param>
    ''' <param name="stage"></param>
    ''' <param name="isEnabledByDefault"></param>
    Private Sub LoadScriptMetaData(target As CheckedListBox, dommePersonalityName As String, type As String, stage As SessionPhase, isEnabledByDefault As Boolean)
        Dim scripts As List(Of ScriptMetaData) = myScriptAccessor.GetAllScripts(dommePersonalityName, type, stage, isEnabledByDefault)

        Dim lastIndex As Integer = target.SelectedIndex
        Dim lastItem As String = target.SelectedItem

        target.BeginUpdate()
        target.Items.Clear()
        For Each cldFile In scripts
            target.Items.Add(cldFile.Name, cldFile.IsEnabled)
        Next

        If lastIndex = -1 Then
            ScriptStatusUnlock(False)
        ElseIf target.Items.Count >= lastIndex Then
            target.SelectedIndex = lastIndex
        ElseIf target.Items.Contains(lastItem) Then
            target.SelectedItem = lastItem
        End If
        target.EndUpdate()
    End Sub

    ''' <summary>
    ''' Determine requirements for <paramref name="scriptMetaData"/>
    ''' </summary>
    ''' <param name="scriptMetaData"></param>
    Public Sub GetScriptStatus(scriptMetaData As ScriptMetaData)
        Dim scriptRequirements = GetScriptRequirements(scriptMetaData)

        Try
            ScriptStatusUnlock(True)
            RTBScriptReq.Text = scriptRequirements.Item2

            If Not scriptRequirements.Item1 Then
                LBLScriptReq.ForeColor = Color.Red
                LBLScriptReq.Text = "All requirements not met!"
            Else
                LBLScriptReq.ForeColor = Color.Green
                LBLScriptReq.Text = "All requirements met!"
            End If
        Catch ex As Exception
            ScriptStatusUnlock(False)
            MessageBox.Show(ex.Message, "Error Checking ScriptStatus", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Private Function GetScriptsCheckedListBox(sessionPhase As SessionPhase) As CheckedListBox
        If sessionPhase = SessionPhase.Start Then
            Return StartScripts
        ElseIf sessionPhase = SessionPhase.Modules Then
            Return ModuleScripts
        ElseIf sessionPhase = SessionPhase.Link Then
            Return LinkScripts
        ElseIf sessionPhase = SessionPhase.End Then
            Return EndScripts
        End If
        Throw New Exception("Unable to determine CheckedListBox.")
    End Function

    Private Function GetSessionPhase(selectedTab As TabPage) As SessionPhase
        If selectedTab Is ScriptsStartTab Then
            Return SessionPhase.Start
        ElseIf selectedTab Is ScriptsModuleTab Then
            Return SessionPhase.Modules
        ElseIf selectedTab Is ScriptsLinkTab Then
            Return SessionPhase.Link
        ElseIf selectedTab Is ScriptsEndTab Then
            Return SessionPhase.End
        End If
        Throw New Exception("Unable to determine CheckedListBox.")
    End Function

    Private Function GetSaveAction(sessionPhase As SessionPhase, target As CheckedListBox) As Action
        If sessionPhase = SessionPhase.Start Then
            Return Sub() saveCheckedListBox(target, Ssh.Files.StartChecklist)
        ElseIf sessionPhase = SessionPhase.Modules Then
            Return Sub() saveCheckedListBox(target, Ssh.Files.ModuleChecklist)
        ElseIf sessionPhase = SessionPhase.Link Then
            Return Sub() saveCheckedListBox(target, Ssh.Files.LinkChecklist)
        ElseIf sessionPhase = SessionPhase.End Then
            Return Sub() saveCheckedListBox(target, Ssh.Files.EndChecklist)
        End If
    End Function

    Private Sub SetColor(label As Label)
        Dim getColor As ColorDialog = New ColorDialog
        If getColor.ShowDialog() = DialogResult.OK Then
            label.ForeColor = getColor.Color
        End If
    End Sub

    Private Sub SetFolderFromDialog(imageGenre As ImageGenre)
        Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
        If (folderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            mySettingsAccessor.ImageGenreFolder(imageGenre) = FolderBrowserDialog1.SelectedPath
            CheckImageFolder(imageGenre)
        End If
    End Sub

    Public Function Color2Html(color As Color) As String
        Return "#" & color.ToArgb().ToString("x").Substring(2).ToUpper
    End Function

    ''' <summary>
    ''' Set the checkboxes based on whether or not <paramref name="itemTags"/> contains a given tag
    ''' </summary>
    ''' <param name="itemTags"></param>
    Private Sub SetLocalTagCheckboxes(itemTags As List(Of ItemTag))
        CBTagHardcore.Checked = itemTags.Contains(ItemTag.Create("TagHardcore").Value)
        CBTagLesbian.Checked = itemTags.Contains(ItemTag.Create("TagLesbian").Value)
        CBTagGay.Checked = itemTags.Contains(ItemTag.Create("TagGay").Value)
        CBTagBisexual.Checked = itemTags.Contains(ItemTag.Create("TagBisexual").Value)
        CBTagSoloF.Checked = itemTags.Contains(ItemTag.Create("TagSoloF").Value)
        CBTagSoloM.Checked = itemTags.Contains(ItemTag.Create("TagSoloM").Value)
        CBTagSoloFuta.Checked = itemTags.Contains(ItemTag.Create("TagSoloFuta").Value)
        CBTagPOV.Checked = itemTags.Contains(ItemTag.Create("TagPOV").Value)
        CBTagBondage.Checked = itemTags.Contains(ItemTag.Create("TagBondage").Value)
        CBTagSM.Checked = itemTags.Contains(ItemTag.Create("TagSM").Value)
        CBTagTD.Checked = itemTags.Contains(ItemTag.Create("TagTD").Value)
        CBTagChastity.Checked = itemTags.Contains(ItemTag.Create("TagChastity").Value)
        CBTagCFNM.Checked = itemTags.Contains(ItemTag.Create("TagCFNM").Value)
        CBTagBath.Checked = itemTags.Contains(ItemTag.Create("TagBath").Value)
        CBTagShower.Checked = itemTags.Contains(ItemTag.Create("TagShower").Value)
        CBTagOutdoors.Checked = itemTags.Contains(ItemTag.Create("TagOutdoors").Value)
        CBTagArtwork.Checked = itemTags.Contains(ItemTag.Create("TagArtwork").Value)

        CBTagMasturbation.Checked = itemTags.Contains(ItemTag.Create("TagMasturbation").Value)
        CBTagHandjob.Checked = itemTags.Contains(ItemTag.Create("TagHandjob").Value)
        CBTagFingering.Checked = itemTags.Contains(ItemTag.Create("TagFingering").Value)
        CBTagBlowjob.Checked = itemTags.Contains(ItemTag.Create("TagBlowjob").Value)
        CBTagCunnilingus.Checked = itemTags.Contains(ItemTag.Create("TagCunnilingus").Value)
        CBTagTitjob.Checked = itemTags.Contains(ItemTag.Create("TagTitjob").Value)
        CBTagFootjob.Checked = itemTags.Contains(ItemTag.Create("TagFootjob").Value)
        CBTagFacesitting.Checked = itemTags.Contains(ItemTag.Create("TagFacesitting").Value)
        CBTagRimming.Checked = itemTags.Contains(ItemTag.Create("TagRimming").Value)
        CBTagMissionary.Checked = itemTags.Contains(ItemTag.Create("TagMissionary").Value)
        CBTagDoggyStyle.Checked = itemTags.Contains(ItemTag.Create("TagDoggyStyle").Value)
        CBTagCowgirl.Checked = itemTags.Contains(ItemTag.Create("TagCowgirl").Value)
        CBTagRCowgirl.Checked = itemTags.Contains(ItemTag.Create("TagRCowgirl").Value)
        CBTagStanding.Checked = itemTags.Contains(ItemTag.Create("TagStanding").Value)
        CBTagAnalSex.Checked = itemTags.Contains(ItemTag.Create("TagAnalSex").Value)
        CBTagDP.Checked = itemTags.Contains(ItemTag.Create("TagDP").Value)
        CBTagGangbang.Checked = itemTags.Contains(ItemTag.Create("TagGangbang").Value)

        CBTag1F.Checked = itemTags.Contains(ItemTag.Create("Tag1F").Value)
        CBTag2F.Checked = itemTags.Contains(ItemTag.Create("Tag2F").Value)
        CBTag3F.Checked = itemTags.Contains(ItemTag.Create("Tag3F").Value)
        CBTag1M.Checked = itemTags.Contains(ItemTag.Create("Tag1M").Value)
        CBTag2M.Checked = itemTags.Contains(ItemTag.Create("Tag2M").Value)
        CBTag3M.Checked = itemTags.Contains(ItemTag.Create("Tag3M").Value)
        CBTag1Futa.Checked = itemTags.Contains(ItemTag.Create("Tag1Futa").Value)
        CBTag2Futa.Checked = itemTags.Contains(ItemTag.Create("Tag2Futa").Value)
        CBTag3Futa.Checked = itemTags.Contains(ItemTag.Create("Tag3Futa").Value)
        CBTagFemdom.Checked = itemTags.Contains(ItemTag.Create("TagFemdom").Value)
        CBTagMaledom.Checked = itemTags.Contains(ItemTag.Create("TagMaledom").Value)
        CBTagFutadom.Checked = itemTags.Contains(ItemTag.Create("TagFutadom").Value)
        CBTagFemsub.Checked = itemTags.Contains(ItemTag.Create("TagFemsub").Value)
        CBTagMalesub.Checked = itemTags.Contains(ItemTag.Create("TagMalesub").Value)
        CBTagFutasub.Checked = itemTags.Contains(ItemTag.Create("TagFutasub").Value)
        CBTagMultiDom.Checked = itemTags.Contains(ItemTag.Create("TagMultiDom").Value)
        CBTagMultiSub.Checked = itemTags.Contains(ItemTag.Create("TagMultiSub").Value)

        CBTagBodyTits.Checked = itemTags.Contains(ItemTag.Create("TagBodyTits").Value)
        CBTagBodyFace.Checked = itemTags.Contains(ItemTag.Create("TagBodyFace").Value)
        CBTagBodyFingers.Checked = itemTags.Contains(ItemTag.Create("TagBodyFingers").Value)
        CBTagBodyMouth.Checked = itemTags.Contains(ItemTag.Create("TagBodyMouth").Value)
        CBTagBodyNipples.Checked = itemTags.Contains(ItemTag.Create("TagBodyNipples").Value)
        CBTagBodyPussy.Checked = itemTags.Contains(ItemTag.Create("TagBodyPussy").Value)
        CBTagBodyAss.Checked = itemTags.Contains(ItemTag.Create("TagBodyAss").Value)
        CBTagBodyLegs.Checked = itemTags.Contains(ItemTag.Create("TagBodyLegs").Value)
        CBTagBodyFeet.Checked = itemTags.Contains(ItemTag.Create("TagBodyFeet").Value)
        CBTagBodyCock.Checked = itemTags.Contains(ItemTag.Create("TagBodyCock").Value)
        CBTagBodyBalls.Checked = itemTags.Contains(ItemTag.Create("TagBodyBalls").Value)

        CBTagNurse.Checked = itemTags.Contains(ItemTag.Create("TagNurse").Value)
        CBTagTeacher.Checked = itemTags.Contains(ItemTag.Create("TagTeacher").Value)
        CBTagSchoolgirl.Checked = itemTags.Contains(ItemTag.Create("TagSchoolgirl").Value)
        CBTagMaid.Checked = itemTags.Contains(ItemTag.Create("TagMaid").Value)
        CBTagSuperhero.Checked = itemTags.Contains(ItemTag.Create("TagSuperhero").Value)

        CBTagWhipping.Checked = itemTags.Contains(ItemTag.Create("TagWhipping").Value)
        CBTagSpanking.Checked = itemTags.Contains(ItemTag.Create("TagSpanking").Value)
        CBTagCockTorture.Checked = itemTags.Contains(ItemTag.Create("TagCockTorture").Value)
        CBTagBallTorture.Checked = itemTags.Contains(ItemTag.Create("TagBallTorture").Value)
        CBTagStrapon.Checked = itemTags.Contains(ItemTag.Create("TagStrapon").Value)
        CBTagBlindfold.Checked = itemTags.Contains(ItemTag.Create("TagBlindfold").Value)
        CBTagGag.Checked = itemTags.Contains(ItemTag.Create("TagGag").Value)
        CBTagClamps.Checked = itemTags.Contains(ItemTag.Create("TagClamps").Value)
        CBTagHotWax.Checked = itemTags.Contains(ItemTag.Create("TagHotWax").Value)
        CBTagNeedles.Checked = itemTags.Contains(ItemTag.Create("TagNeedles").Value)
        CBTagElectro.Checked = itemTags.Contains(ItemTag.Create("TagElectro").Value)

        CBTagDomme.Checked = itemTags.Contains(ItemTag.Create("TagDomme").Value)
        CBTagCumshot.Checked = itemTags.Contains(ItemTag.Create("TagCumshot").Value)
        CBTagCumEating.Checked = itemTags.Contains(ItemTag.Create("TagCumEating").Value)
        CBTagKissing.Checked = itemTags.Contains(ItemTag.Create("TagKissing").Value)
        CBTagTattoos.Checked = itemTags.Contains(ItemTag.Create("TagTattoos").Value)
        CBTagStockings.Checked = itemTags.Contains(ItemTag.Create("TagStockings").Value)
        CBTagVibrator.Checked = itemTags.Contains(ItemTag.Create("TagVibrator").Value)
        CBTagDildo.Checked = itemTags.Contains(ItemTag.Create("TagDildo").Value)
        CBTagPocketPussy.Checked = itemTags.Contains(ItemTag.Create("TagPocketPussy").Value)
        CBTagAnalToy.Checked = itemTags.Contains(ItemTag.Create("TagAnalToy").Value)
        CBTagWatersports.Checked = itemTags.Contains(ItemTag.Create("TagWaterSports").Value)

        CBTagShibari.Checked = itemTags.Contains(ItemTag.Create("TagShibari").Value)
        CBTagTentacles.Checked = itemTags.Contains(ItemTag.Create("TagTentacles").Value)
        CBTagBukkake.Checked = itemTags.Contains(ItemTag.Create("TagBukkake").Value)
        CBTagBakunyuu.Checked = itemTags.Contains(ItemTag.Create("TagBakunyuu").Value)
        CBTagAhegao.Checked = itemTags.Contains(ItemTag.Create("TagAhegao").Value)
        CBTagBodyWriting.Checked = itemTags.Contains(ItemTag.Create("TagBodyWriting").Value)
        CBTagTrap.Checked = itemTags.Contains(ItemTag.Create("TagTrap").Value)
        CBTagGanguro.Checked = itemTags.Contains(ItemTag.Create("TagGanguro").Value)
        CBTagMahouShoujo.Checked = itemTags.Contains(ItemTag.Create("TagMahouShoujo").Value)
        CBTagMonsterGirl.Checked = itemTags.Contains(ItemTag.Create("TagMonsterGirl").Value)
    End Sub

    ''' <summary>
    ''' Look at the checkboxes and build a list of item tags
    ''' </summary>
    Private Function GetItemTagsFromCheckboxes() As List(Of ItemTag)
        Dim tags As List(Of ItemTag) = New List(Of ItemTag)
        If CBTagHardcore.Checked Then tags.Add(ItemTag.Create("TagHardcore").Value)
        If CBTagLesbian.Checked Then tags.Add(ItemTag.Create("TagLesbian").Value)
        If CBTagLesbian.Checked Then tags.Add(ItemTag.Create("TagLesbian").Value)
        If CBTagGay.Checked Then tags.Add(ItemTag.Create("TagGay").Value)
        If CBTagBisexual.Checked Then tags.Add(ItemTag.Create("TagBisexual").Value)
        If CBTagSoloF.Checked Then tags.Add(ItemTag.Create("TagSoloF").Value)
        If CBTagSoloM.Checked Then tags.Add(ItemTag.Create("TagSoloM").Value)
        If CBTagSoloFuta.Checked Then tags.Add(ItemTag.Create("TagSoloFuta").Value)
        If CBTagPOV.Checked Then tags.Add(ItemTag.Create("TagPOV").Value)
        If CBTagBondage.Checked Then tags.Add(ItemTag.Create("TagBondage").Value)
        If CBTagSM.Checked Then tags.Add(ItemTag.Create("TagSM").Value)
        If CBTagTD.Checked Then tags.Add(ItemTag.Create("TagTD").Value)
        If CBTagChastity.Checked Then tags.Add(ItemTag.Create("TagChastity").Value)
        If CBTagCFNM.Checked Then tags.Add(ItemTag.Create("TagCFNM").Value)
        If CBTagBath.Checked Then tags.Add(ItemTag.Create("TagBath").Value)
        If CBTagShower.Checked Then tags.Add(ItemTag.Create("TagShower").Value)
        If CBTagOutdoors.Checked Then tags.Add(ItemTag.Create("TagOutdoors").Value)
        If CBTagArtwork.Checked Then tags.Add(ItemTag.Create("TagArtwork").Value)

        If CBTagMasturbation.Checked Then tags.Add(ItemTag.Create("TagMasturbation").Value)
        If CBTagHandjob.Checked Then tags.Add(ItemTag.Create("TagHandjob").Value)
        If CBTagFingering.Checked Then tags.Add(ItemTag.Create("TagFingering").Value)
        If CBTagBlowjob.Checked Then tags.Add(ItemTag.Create("TagBlowjob").Value)
        If CBTagCunnilingus.Checked Then tags.Add(ItemTag.Create("TagCunnilingus").Value)
        If CBTagTitjob.Checked Then tags.Add(ItemTag.Create("TagTitjob").Value)
        If CBTagFootjob.Checked Then tags.Add(ItemTag.Create("TagFootjob").Value)
        If CBTagFacesitting.Checked Then tags.Add(ItemTag.Create("TagFacesitting").Value)
        If CBTagRimming.Checked Then tags.Add(ItemTag.Create("TagRimming").Value)
        If CBTagMissionary.Checked Then tags.Add(ItemTag.Create("TagMissionary").Value)
        If CBTagDoggyStyle.Checked Then tags.Add(ItemTag.Create("TagDoggyStyle").Value)
        If CBTagCowgirl.Checked Then tags.Add(ItemTag.Create("TagCowgirl").Value)
        If CBTagRCowgirl.Checked Then tags.Add(ItemTag.Create("TagRCowgirl").Value)
        If CBTagStanding.Checked Then tags.Add(ItemTag.Create("TagStanding").Value)
        If CBTagAnalSex.Checked Then tags.Add(ItemTag.Create("TagAnalSex").Value)
        If CBTagDP.Checked Then tags.Add(ItemTag.Create("TagDP").Value)
        If CBTagGangbang.Checked Then tags.Add(ItemTag.Create("TagGangbang").Value)

        If CBTag1F.Checked Then tags.Add(ItemTag.Create("Tag1F").Value)
        If CBTag2F.Checked Then tags.Add(ItemTag.Create("Tag2F").Value)
        If CBTag3F.Checked Then tags.Add(ItemTag.Create("Tag3F").Value)
        If CBTag1M.Checked Then tags.Add(ItemTag.Create("Tag1M").Value)
        If CBTag2M.Checked Then tags.Add(ItemTag.Create("Tag2M").Value)
        If CBTag3M.Checked Then tags.Add(ItemTag.Create("Tag3M").Value)
        If CBTag1Futa.Checked Then tags.Add(ItemTag.Create("Tag1Futa").Value)
        If CBTag2Futa.Checked Then tags.Add(ItemTag.Create("Tag2Futa").Value)
        If CBTag3Futa.Checked Then tags.Add(ItemTag.Create("Tag3Futa").Value)
        If CBTagFemdom.Checked Then tags.Add(ItemTag.Create("TagFemdom").Value)
        If CBTagMaledom.Checked Then tags.Add(ItemTag.Create("TagMaledom").Value)
        If CBTagFutadom.Checked Then tags.Add(ItemTag.Create("TagFutadom").Value)
        If CBTagFemsub.Checked Then tags.Add(ItemTag.Create("TagFemsub").Value)
        If CBTagMalesub.Checked Then tags.Add(ItemTag.Create("TagMalesub").Value)
        If CBTagFutasub.Checked Then tags.Add(ItemTag.Create("TagFutasub").Value)
        If CBTagMultiDom.Checked Then tags.Add(ItemTag.Create("TagMultiDom").Value)
        If CBTagMultiSub.Checked Then tags.Add(ItemTag.Create("TagMultiSub").Value)

        If CBTagBodyFace.Checked Then tags.Add(ItemTag.Create("TagBodyFace").Value)
        If CBTagBodyFingers.Checked Then tags.Add(ItemTag.Create("TagBodyFingers").Value)
        If CBTagBodyMouth.Checked Then tags.Add(ItemTag.Create("TagBodyMouth").Value)
        If CBTagBodyTits.Checked Then tags.Add(ItemTag.Create("TagBodyTits").Value)
        If CBTagBodyNipples.Checked Then tags.Add(ItemTag.Create("TagBodyNipples").Value)
        If CBTagBodyPussy.Checked Then tags.Add(ItemTag.Create("TagBodyPussy").Value)
        If CBTagBodyAss.Checked Then tags.Add(ItemTag.Create("TagBodyAss").Value)
        If CBTagBodyLegs.Checked Then tags.Add(ItemTag.Create("TagBodyLegs").Value)
        If CBTagBodyFeet.Checked Then tags.Add(ItemTag.Create("TagBodyFeet").Value)
        If CBTagBodyCock.Checked Then tags.Add(ItemTag.Create("TagBodyCock").Value)
        If CBTagBodyBalls.Checked Then tags.Add(ItemTag.Create("TagBodyBalls").Value)

        If CBTagNurse.Checked Then tags.Add(ItemTag.Create("TagNurse").Value)
        If CBTagTeacher.Checked Then tags.Add(ItemTag.Create("TagTeacher").Value)
        If CBTagSchoolgirl.Checked Then tags.Add(ItemTag.Create("TagSchoolgirl").Value)
        If CBTagMaid.Checked Then tags.Add(ItemTag.Create("TagMaid").Value)
        If CBTagSuperhero.Checked Then tags.Add(ItemTag.Create("TagSuperhero").Value)

        If CBTagWhipping.Checked Then tags.Add(ItemTag.Create("TagWhipping").Value)
        If CBTagSpanking.Checked Then tags.Add(ItemTag.Create("TagSpanking").Value)
        If CBTagCockTorture.Checked Then tags.Add(ItemTag.Create("TagCockTorture").Value)
        If CBTagBallTorture.Checked Then tags.Add(ItemTag.Create("TagBallTorture").Value)
        If CBTagStrapon.Checked Then tags.Add(ItemTag.Create("TagStrapon").Value)
        If CBTagBlindfold.Checked Then tags.Add(ItemTag.Create("TagBlindfold").Value)
        If CBTagGag.Checked Then tags.Add(ItemTag.Create("TagGag").Value)
        If CBTagClamps.Checked Then tags.Add(ItemTag.Create("TagClamps").Value)
        If CBTagHotWax.Checked Then tags.Add(ItemTag.Create("TagHotWax").Value)
        If CBTagNeedles.Checked Then tags.Add(ItemTag.Create("TagNeedles").Value)
        If CBTagElectro.Checked Then tags.Add(ItemTag.Create("TagElectro").Value)

        If CBTagDomme.Checked Then tags.Add(ItemTag.Create("TagDomme").Value)
        If CBTagCumshot.Checked Then tags.Add(ItemTag.Create("TagCumshot").Value)
        If CBTagCumEating.Checked Then tags.Add(ItemTag.Create("TagCumEating").Value)
        If CBTagKissing.Checked Then tags.Add(ItemTag.Create("TagKissing").Value)
        If CBTagTattoos.Checked Then tags.Add(ItemTag.Create("TagTattoos").Value)
        If CBTagStockings.Checked Then tags.Add(ItemTag.Create("TagStockings").Value)
        If CBTagVibrator.Checked Then tags.Add(ItemTag.Create("TagVibrator").Value)
        If CBTagDildo.Checked Then tags.Add(ItemTag.Create("TagDildo").Value)
        If CBTagPocketPussy.Checked Then tags.Add(ItemTag.Create("TagPocketPussy").Value)
        If CBTagAnalToy.Checked Then tags.Add(ItemTag.Create("TagAnalToy").Value)
        If CBTagWatersports.Checked Then tags.Add(ItemTag.Create("TagWatersports").Value)

        If CBTagShibari.Checked Then tags.Add(ItemTag.Create("TagShibari").Value)
        If CBTagTentacles.Checked Then tags.Add(ItemTag.Create("TagTentacles").Value)
        If CBTagBukkake.Checked Then tags.Add(ItemTag.Create("TagBukkake").Value)
        If CBTagBakunyuu.Checked Then tags.Add(ItemTag.Create("TagBakunyuu").Value)
        If CBTagAhegao.Checked Then tags.Add(ItemTag.Create("TagAhegao").Value)
        If CBTagBodyWriting.Checked Then tags.Add(ItemTag.Create("TagBodyWriting").Value)
        If CBTagTrap.Checked Then tags.Add(ItemTag.Create("TagTrap").Value)
        If CBTagGanguro.Checked Then tags.Add(ItemTag.Create("TagGanguro").Value)
        If CBTagMahouShoujo.Checked Then tags.Add(ItemTag.Create("TagMahouShoujo").Value)
        If CBTagMonsterGirl.Checked Then tags.Add(ItemTag.Create("TagMonsterGirl").Value)
        Return tags
    End Function

    ''' <summary>
    ''' Get a list of image files in <paramref name="folderName"/>
    ''' </summary>
    ''' <param name="folderName"></param>
    ''' <returns></returns>
    Private Function GetImagesInFolder(folderName As String) As Result(Of List(Of String))
        If (Not Directory.Exists(folderName)) Then
            Return Result.Fail(Of List(Of String))(folderName + " does not exist.")
        End If

        Dim imageExtensions As List(Of String) = New List(Of String)()
        imageExtensions.Add(".png")
        imageExtensions.Add(".jpg")
        imageExtensions.Add(".jpeg")
        imageExtensions.Add(".gif")
        imageExtensions.Add(".bmp")

        Dim images As List(Of String) = Directory.GetFiles(folderName) _
            .Where(Function(f) imageExtensions.Contains(Path.GetExtension(f).ToLower())) _
            .OrderBy(Function(f) f) _
            .ToList()

        Return Result.Ok(images)
    End Function

    Private Function GetTaggedItem(tagFile As String, imageFile As String) As TaggedItem
        Dim taggedItems As List(Of TaggedItem) = ReadTagFile(tagFile)
        Return taggedItems.FirstOrDefault(Function(ti) ti.ItemName.ToLower() = imageFile.ToLower())
    End Function

    Private Sub SetDommeTagCheckboxesEnabled(isEnabled As Boolean)
        CBTagFace.Enabled = isEnabled
        CBTagBoobs.Enabled = isEnabled
        CBTagPussy.Enabled = isEnabled
        CBTagAss.Enabled = isEnabled
        CBTagLegs.Enabled = isEnabled
        CBTagFeet.Enabled = isEnabled
        CBTagFullyDressed.Enabled = isEnabled
        CBTagHalfDressed.Enabled = isEnabled
        CBTagGarmentCovering.Enabled = isEnabled
        CBTagHandsCovering.Enabled = isEnabled
        CBTagNaked.Enabled = isEnabled
        CBTagSideView.Enabled = isEnabled
        CBTagCloseUp.Enabled = isEnabled
        CBTagMasturbating.Enabled = isEnabled
        CBTagSucking.Enabled = isEnabled
        CBTagPiercing.Enabled = isEnabled
        CBTagSmiling.Enabled = isEnabled
        CBTagGlaring.Enabled = isEnabled
        CBTagSeeThrough.Enabled = isEnabled
        CBTagAllFours.Enabled = isEnabled

        CBTagGarment.Enabled = isEnabled
        CBTagUnderwear.Enabled = isEnabled
        CBTagTattoo.Enabled = isEnabled
        CBTagSexToy.Enabled = isEnabled
        CBTagFurniture.Enabled = isEnabled

        TBTagGarment.Enabled = isEnabled
        TBTagUnderwear.Enabled = isEnabled
        TBTagTattoo.Enabled = isEnabled
        TBTagSexToy.Enabled = isEnabled
        TBTagFurniture.Enabled = isEnabled

        LBLTagCount.Enabled = isEnabled
    End Sub

    ''' <summary>
    ''' Set the domme tag checkboxes
    ''' </summary>
    ''' <param name="itemTags"></param>
    Private Sub SetDommeTagCheckboxes(itemTags As List(Of ItemTag))
        CBTagFace.Checked = itemTags.Contains(ItemTag.Create("TagFace").Value)
        CBTagBoobs.Checked = itemTags.Contains(ItemTag.Create("TagBoobs").Value)
        CBTagPussy.Checked = itemTags.Contains(ItemTag.Create("TagPussy").Value)
        CBTagAss.Checked = itemTags.Contains(ItemTag.Create("TagAss").Value)
        CBTagLegs.Checked = itemTags.Contains(ItemTag.Create("TagLegs").Value)
        CBTagFeet.Checked = itemTags.Contains(ItemTag.Create("TagFeet").Value)
        CBTagFullyDressed.Checked = itemTags.Contains(ItemTag.Create("TagFullyDressed").Value)
        CBTagHalfDressed.Checked = itemTags.Contains(ItemTag.Create("TagHalfDressed").Value)
        CBTagGarmentCovering.Checked = itemTags.Contains(ItemTag.Create("TagGarmentCovering").Value)
        CBTagHandsCovering.Checked = itemTags.Contains(ItemTag.Create("TagHandsCovering").Value)
        CBTagNaked.Checked = itemTags.Contains(ItemTag.Create("TagNaked").Value)
        CBTagSideView.Checked = itemTags.Contains(ItemTag.Create("TagSideView").Value)
        CBTagCloseUp.Checked = itemTags.Contains(ItemTag.Create("TagCloseUp").Value)
        CBTagMasturbating.Checked = itemTags.Contains(ItemTag.Create("TagMasturbating").Value)
        CBTagSucking.Checked = itemTags.Contains(ItemTag.Create("TagSucking").Value)
        CBTagPiercing.Checked = itemTags.Contains(ItemTag.Create("TagPiercing").Value)
        CBTagSmiling.Checked = itemTags.Contains(ItemTag.Create("TagSmiling").Value)
        CBTagGlaring.Checked = itemTags.Contains(ItemTag.Create("TagGlaring").Value)
        CBTagSeeThrough.Checked = itemTags.Contains(ItemTag.Create("TagSeeThrough").Value)
        CBTagAllFours.Checked = itemTags.Contains(ItemTag.Create("TagAllFours").Value)

        Dim garmentTag As ItemTag? = itemTags.FirstOrDefault(Function(it) it.Equals(ItemTag.Create("TagGarment").Value))
        CBTagGarment.Checked = garmentTag.HasValue
        TBTagGarment.Text = If(garmentTag.HasValue, String.Empty, garmentTag.Value.ToString().Replace("TagGarment", ""))

        Dim underwearTag As ItemTag? = itemTags.FirstOrDefault(Function(it) it.Equals(ItemTag.Create("TagUnderwear").Value))
        CBTagUnderwear.Checked = underwearTag.HasValue
        TBTagUnderwear.Text = If(underwearTag.HasValue, String.Empty, underwearTag.Value.ToString().Replace("TagUnderwear", ""))

        Dim tattooTag As ItemTag? = itemTags.FirstOrDefault(Function(it) it.Equals(ItemTag.Create("TagTattoo").Value))
        CBTagTattoo.Checked = tattooTag.HasValue
        TBTagTattoo.Text = If(tattooTag.HasValue, String.Empty, tattooTag.Value.ToString().Replace("TagTattoo", ""))

        Dim sexToyTag As ItemTag? = itemTags.FirstOrDefault(Function(it) it.Equals(ItemTag.Create("TagSexToy").Value))
        CBTagSexToy.Checked = sexToyTag.HasValue
        TBTagSexToy.Text = If(sexToyTag.HasValue, String.Empty, sexToyTag.Value.ToString().Replace("TagSexToy", ""))

        Dim furnitureTag As ItemTag? = itemTags.FirstOrDefault(Function(it) it.Equals(ItemTag.Create("TagFurniture").Value))
        CBTagFurniture.Checked = furnitureTag.HasValue
        TBTagFurniture.Text = If(furnitureTag.HasValue, String.Empty, furnitureTag.Value.ToString().Replace("TagFurniture", ""))
    End Sub

    Public Sub SetLocalImageEnabled(isEnabled As Boolean)
        CBTagHardcore.Enabled = isEnabled
        CBTagLesbian.Enabled = isEnabled
        CBTagGay.Enabled = isEnabled
        CBTagBisexual.Enabled = isEnabled
        CBTagSoloF.Enabled = isEnabled
        CBTagSoloM.Enabled = isEnabled
        CBTagSoloFuta.Enabled = isEnabled
        CBTagPOV.Enabled = isEnabled
        CBTagBondage.Enabled = isEnabled
        CBTagSM.Enabled = isEnabled
        CBTagTD.Enabled = isEnabled
        CBTagChastity.Enabled = isEnabled
        CBTagCFNM.Enabled = isEnabled
        CBTagBath.Enabled = isEnabled
        CBTagShower.Enabled = isEnabled
        CBTagOutdoors.Enabled = isEnabled
        CBTagArtwork.Enabled = isEnabled

        CBTagMasturbation.Enabled = isEnabled
        CBTagHandjob.Enabled = isEnabled
        CBTagFingering.Enabled = isEnabled
        CBTagBlowjob.Enabled = isEnabled
        CBTagCunnilingus.Enabled = isEnabled
        CBTagTitjob.Enabled = isEnabled
        CBTagFootjob.Enabled = isEnabled
        CBTagFacesitting.Enabled = isEnabled
        CBTagRimming.Enabled = isEnabled
        CBTagMissionary.Enabled = isEnabled
        CBTagDoggyStyle.Enabled = isEnabled
        CBTagCowgirl.Enabled = isEnabled
        CBTagRCowgirl.Enabled = isEnabled
        CBTagStanding.Enabled = isEnabled
        CBTagAnalSex.Enabled = isEnabled
        CBTagDP.Enabled = isEnabled
        CBTagGangbang.Enabled = isEnabled

        CBTag1F.Enabled = isEnabled
        CBTag2F.Enabled = isEnabled
        CBTag3F.Enabled = isEnabled
        CBTag1M.Enabled = isEnabled
        CBTag2M.Enabled = isEnabled
        CBTag3M.Enabled = isEnabled
        CBTag1Futa.Enabled = isEnabled
        CBTag2Futa.Enabled = isEnabled
        CBTag3Futa.Enabled = isEnabled
        CBTagFemdom.Enabled = isEnabled
        CBTagMaledom.Enabled = isEnabled
        CBTagFutadom.Enabled = isEnabled
        CBTagFemsub.Enabled = isEnabled
        CBTagMalesub.Enabled = isEnabled
        CBTagFutasub.Enabled = isEnabled
        CBTagMultiDom.Enabled = isEnabled
        CBTagMultiSub.Enabled = isEnabled

        CBTagBodyFace.Enabled = isEnabled
        CBTagBodyFingers.Enabled = isEnabled
        CBTagBodyMouth.Enabled = isEnabled
        CBTagBodyTits.Enabled = isEnabled
        CBTagBodyNipples.Enabled = isEnabled
        CBTagBodyPussy.Enabled = isEnabled
        CBTagBodyAss.Enabled = isEnabled
        CBTagBodyLegs.Enabled = isEnabled
        CBTagBodyFeet.Enabled = isEnabled
        CBTagBodyCock.Enabled = isEnabled
        CBTagBodyBalls.Enabled = isEnabled

        CBTagNurse.Enabled = isEnabled
        CBTagTeacher.Enabled = isEnabled
        CBTagSchoolgirl.Enabled = isEnabled
        CBTagMaid.Enabled = isEnabled
        CBTagSuperhero.Enabled = isEnabled

        CBTagWhipping.Enabled = isEnabled
        CBTagSpanking.Enabled = isEnabled
        CBTagCockTorture.Enabled = isEnabled
        CBTagBallTorture.Enabled = isEnabled
        CBTagStrapon.Enabled = isEnabled
        CBTagBlindfold.Enabled = isEnabled
        CBTagGag.Enabled = isEnabled
        CBTagClamps.Enabled = isEnabled
        CBTagHotWax.Enabled = isEnabled
        CBTagNeedles.Enabled = isEnabled
        CBTagElectro.Enabled = isEnabled

        CBTagDomme.Enabled = isEnabled
        CBTagCumshot.Enabled = isEnabled
        CBTagCumEating.Enabled = isEnabled
        CBTagKissing.Enabled = isEnabled
        CBTagTattoos.Enabled = isEnabled
        CBTagStockings.Enabled = isEnabled
        CBTagVibrator.Enabled = isEnabled
        CBTagDildo.Enabled = isEnabled
        CBTagPocketPussy.Enabled = isEnabled
        CBTagAnalToy.Enabled = isEnabled
        CBTagWatersports.Enabled = isEnabled

        CBTagShibari.Enabled = isEnabled
        CBTagTentacles.Enabled = isEnabled
        CBTagBukkake.Enabled = isEnabled
        CBTagBakunyuu.Enabled = isEnabled
        CBTagAhegao.Enabled = isEnabled
        CBTagBodyWriting.Enabled = isEnabled
        CBTagTrap.Enabled = isEnabled
        CBTagGanguro.Enabled = isEnabled
        CBTagMahouShoujo.Enabled = isEnabled
        CBTagMonsterGirl.Enabled = isEnabled


    End Sub

    Public Sub SaveLocalImageTags(imageFileName As String)
        Dim tags As List(Of ItemTag) = GetItemTagsFromCheckboxes()
        Dim imageLine As String = imageFileName & " " & String.Join(" ", tags.Select(Function(t) t.ToString()))

        If File.Exists(LocalImageTagFile) Then
            Dim tagCheckList As List(Of String) = File.ReadAllLines(LocalImageTagFile).ToList()

            Dim lineExists As Boolean
            lineExists = False

            For i As Integer = 0 To tagCheckList.Count - 1
                If tagCheckList(i).Contains(imageFileName) Then
                    lineExists = True
                    tagCheckList(i) = imageLine
                End If
            Next

            If Not lineExists Then
                tagCheckList.Add(imageLine)
                lineExists = False
            End If

            File.WriteAllLines(LocalImageTagFile, tagCheckList)
        Else
            File.AppendAllText(LocalImageTagFile, imageLine)
        End If
    End Sub

    Public Sub SaveDommeTags(tagFile As String, imageFile As String)

        Dim imageTagLine As String = Path.GetFileName(imageFile)

        If CBTagFace.Checked Then imageTagLine = imageTagLine & " " & "TagFace"
        If CBTagBoobs.Checked Then imageTagLine = imageTagLine & " " & "TagBoobs"
        If CBTagPussy.Checked Then imageTagLine = imageTagLine & " " & "TagPussy"
        If CBTagAss.Checked Then imageTagLine = imageTagLine & " " & "TagAss"
        If CBTagLegs.Checked Then imageTagLine = imageTagLine & " " & "TagLegs"
        If CBTagFeet.Checked Then imageTagLine = imageTagLine & " " & "TagFeet"
        If CBTagFullyDressed.Checked Then imageTagLine = imageTagLine & " " & "TagFullyDressed"
        If CBTagHalfDressed.Checked Then imageTagLine = imageTagLine & " " & "TagHalfDressed"
        If CBTagGarmentCovering.Checked Then imageTagLine = imageTagLine & " " & "TagGarmentCovering"
        If CBTagHandsCovering.Checked Then imageTagLine = imageTagLine & " " & "TagHandsCovering"
        If CBTagNaked.Checked Then imageTagLine = imageTagLine & " " & "TagNaked"
        If CBTagSideView.Checked Then imageTagLine = imageTagLine & " " & "TagSideView"
        If CBTagCloseUp.Checked Then imageTagLine = imageTagLine & " " & "TagCloseUp"
        If CBTagMasturbating.Checked Then imageTagLine = imageTagLine & " " & "TagMasturbating"
        If CBTagSucking.Checked Then imageTagLine = imageTagLine & " " & "TagSucking"
        If CBTagPiercing.Checked Then imageTagLine = imageTagLine & " " & "TagPiercing"
        If CBTagSmiling.Checked Then imageTagLine = imageTagLine & " " & "TagSmiling"
        If CBTagGlaring.Checked Then imageTagLine = imageTagLine & " " & "TagGlaring"
        If CBTagSeeThrough.Checked Then imageTagLine = imageTagLine & " " & "TagSeeThrough"
        If CBTagAllFours.Checked Then imageTagLine = imageTagLine & " " & "TagAllFours"

        If CBTagGarment.Checked Then
            If TBTagGarment.Text = "" Then
                MessageBox.Show(Me, "Please enter a description in the Garment field!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            Else
                imageTagLine = imageTagLine & " " & "TagGarment" & TBTagGarment.Text
            End If
        End If

        If CBTagUnderwear.Checked Then
            If TBTagUnderwear.Text = "" Then
                MessageBox.Show(Me, "Please enter a description in the Underwear field!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            Else
                imageTagLine = imageTagLine & " " & "TagUnderwear" & TBTagUnderwear.Text
            End If
        End If

        If CBTagTattoo.Checked Then
            If TBTagTattoo.Text = "" Then
                MessageBox.Show(Me, "Please enter a description in the Tattoo field!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            Else
                imageTagLine = imageTagLine & " " & "TagTattoo" & TBTagTattoo.Text
            End If
        End If

        If CBTagSexToy.Checked Then
            If TBTagSexToy.Text = "" Then
                MessageBox.Show(Me, "Please enter a description in the Room field!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            Else
                imageTagLine = imageTagLine & " " & "TagSexToy" & TBTagSexToy.Text
            End If
        End If

        If CBTagFurniture.Checked Then
            If TBTagFurniture.Text = "" Then
                MessageBox.Show(Me, "Please enter a description in the Furniture field!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            Else
                imageTagLine = imageTagLine & " " & "TagFurniture" & TBTagFurniture.Text
            End If
        End If


        If File.Exists(tagFile) Then
            Dim TagCheckList As List(Of TaggedItem) = ReadTagFile(tagFile)
            Dim LineExists As Boolean
            LineExists = False
            ' FINISH ME
            'For i As Integer = 0 To TagCheckList.Count - 1
            '    If TagCheckList(i).Contains(Path.GetFileName(CurrentImageTagImage)) Then
            '        TagCheckList(i) = TempImageDir
            '        LineExists = True
            '        System.IO.File.WriteAllLines(TBTagDir.Text & "\ImageTags.txt", TagCheckList)
            '    End If
            'Next

            If Not LineExists Then
                My.Computer.FileSystem.WriteAllText(tagFile, Environment.NewLine & imageTagLine, True)
                LineExists = False
            End If

        Else
            My.Computer.FileSystem.WriteAllText(tagFile, imageTagLine, True)
        End If
    End Sub

    ''' <summary>
    ''' Reads the tag data from a file as a collection of strings
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Private Function ReadTagFile(fileName As String) As List(Of TaggedItem)
        Return myLoadFileData.ReadData(fileName) _
                    .OnSuccess(Function(data) myParseTagDataService.ParseTagData(data)).GetResultOrDefault(New List(Of TaggedItem))
    End Function

    Private Function GetOrgasmReleaseDate(lockTimeDuration As String, startDate As Date) As Date
        If lockTimeDuration = "Week" Then startDate = DateAdd(DateInterval.Day, 7, startDate)
        If lockTimeDuration = "2 Weeks" Then startDate = DateAdd(DateInterval.Day, 14, startDate)
        If lockTimeDuration = "Month" Then startDate = DateAdd(DateInterval.Month, 1, startDate)
        If lockTimeDuration = "2 Months" Then startDate = DateAdd(DateInterval.Month, 2, startDate)
        If lockTimeDuration = "3 Months" Then startDate = DateAdd(DateInterval.Month, 3, startDate)
        If lockTimeDuration = "6 Months" Then startDate = DateAdd(DateInterval.Month, 6, startDate)
        If lockTimeDuration = "9 Months" Then startDate = DateAdd(DateInterval.Month, 9, startDate)
        If lockTimeDuration = "Year" Then startDate = DateAdd(DateInterval.Year, 1, startDate)
        If lockTimeDuration = "2 Years" Then startDate = DateAdd(DateInterval.Year, 2, startDate)
        If lockTimeDuration = "3 Years" Then startDate = DateAdd(DateInterval.Year, 3, startDate)
        If lockTimeDuration = "5 Years" Then startDate = DateAdd(DateInterval.Year, 5, startDate)
        If lockTimeDuration = "10 Years" Then startDate = DateAdd(DateInterval.Year, 10, startDate)
        If lockTimeDuration = "25 Years" Then startDate = DateAdd(DateInterval.Year, 25, startDate)
        If lockTimeDuration = "Lifetime" Then startDate = DateAdd(DateInterval.Year, 100, startDate)

        Return startDate
    End Function

    Private Function GetOrgasmInterval(dominationLevel As DomLevel) As String
        Dim randomTime As Integer = MainWindow.ssh.randomizer.Next(1, 4)

        If dominationLevel = DomLevel.Gentle Then
            Return If(randomTime = 2, "2 Weeks", "Week")
        End If

        If dominationLevel = DomLevel.Lenient Then
            Return If(randomTime = 3, "Month", "2 Weeks")
        End If

        If dominationLevel = DomLevel.Tease Then
            Return If(randomTime > 2, "Month", "2 Weeks")
        End If

        If dominationLevel = DomLevel.Rough Then
            Dim time = "3 months"
            If randomTime = 1 Then time = "2 Months"
            If randomTime = 2 Then time = "3 Months"
            If randomTime = 2 Then time = "6 Months"
            Return time
        End If

        If dominationLevel = DomLevel.Sadistic Then
            Dim time = "6 months"
            If randomTime = 2 Then time = "9 Months"
            If randomTime = 3 Then time = "Year"
            Return time
        End If
        Throw New Exception("Unkonown DomLevel")
    End Function

    Private mySettingsAccessor As ISettingsAccessor
    Private myBlogAccessor As BlogImageAccessor
    Private myCldAccessor As ICldAccessor
    Private myScriptAccessor As IScriptAccessor
    Private myLoadFileData As ILoadFileData
    Private myParseTagDataService As ParseOldTagDataService
End Class
