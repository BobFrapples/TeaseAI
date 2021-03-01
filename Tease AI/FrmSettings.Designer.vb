<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSettings
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSettings))
        Me.SettingsTabs = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.PNLGeneralSettings = New System.Windows.Forms.Panel()
        Me.BtnImportSettings = New System.Windows.Forms.Button()
        Me.LblImportSettings = New System.Windows.Forms.Label()
        Me.GroupBox64 = New System.Windows.Forms.GroupBox()
        Me.CBMuteMedia = New System.Windows.Forms.CheckBox()
        Me.GBDommeImages = New System.Windows.Forms.GroupBox()
        Me.SlideShowNumBox = New System.Windows.Forms.NumericUpDown()
        Me.TeaseSlideShowRadio = New System.Windows.Forms.RadioButton()
        Me.CBNewSlideshow = New System.Windows.Forms.CheckBox()
        Me.ManualSlideShowRadio = New System.Windows.Forms.RadioButton()
        Me.BTNDomImageDir = New System.Windows.Forms.Button()
        Me.TimedSlideShowRadio = New System.Windows.Forms.RadioButton()
        Me.TbxDomImageDir = New System.Windows.Forms.TextBox()
        Me.GBGeneralTextToSpeech = New System.Windows.Forms.GroupBox()
        Me.LBLVRate = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.LBLVVolume = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.SliderVRate = New System.Windows.Forms.TrackBar()
        Me.SliderVVolume = New System.Windows.Forms.TrackBar()
        Me.TTSCheckBox = New System.Windows.Forms.CheckBox()
        Me.TTSComboBox = New System.Windows.Forms.ComboBox()
        Me.GBSafeword = New System.Windows.Forms.GroupBox()
        Me.LBLSafeword = New System.Windows.Forms.Label()
        Me.TBSafeword = New System.Windows.Forms.TextBox()
        Me.GBGeneralSystem = New System.Windows.Forms.GroupBox()
        Me.CBAuditStartup = New System.Windows.Forms.CheckBox()
        Me.CBDomDel = New System.Windows.Forms.CheckBox()
        Me.CBSettingsPause = New System.Windows.Forms.CheckBox()
        Me.CBSaveChatlogExit = New System.Windows.Forms.CheckBox()
        Me.CBAutosaveChatlog = New System.Windows.Forms.CheckBox()
        Me.GBGeneralImages = New System.Windows.Forms.GroupBox()
        Me.CBImageInfo = New System.Windows.Forms.CheckBox()
        Me.CBSlideshowRandom = New System.Windows.Forms.CheckBox()
        Me.LandscapeCheckBox = New System.Windows.Forms.CheckBox()
        Me.CBBlogImageWindow = New System.Windows.Forms.CheckBox()
        Me.CBSlideshowSubDir = New System.Windows.Forms.CheckBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.GBGeneralSettings = New System.Windows.Forms.GroupBox()
        Me.WebTeaseMode = New System.Windows.Forms.CheckBox()
        Me.GBSubFont = New System.Windows.Forms.GroupBox()
        Me.BTNSubColor = New System.Windows.Forms.Button()
        Me.LBLSubColor = New System.Windows.Forms.Label()
        Me.NBFontSize = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SubMessageFontCB = New System.Windows.Forms.ComboBox()
        Me.GBDommeFont = New System.Windows.Forms.GroupBox()
        Me.BTNDomColor = New System.Windows.Forms.Button()
        Me.LBLDomColor = New System.Windows.Forms.Label()
        Me.DommeMessageFontCB = New System.Windows.Forms.ComboBox()
        Me.NBFontSizeD = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CBInputIcon = New System.Windows.Forms.CheckBox()
        Me.TypeInstantlyCheckBox = New System.Windows.Forms.CheckBox()
        Me.TimeStampCheckBox = New System.Windows.Forms.CheckBox()
        Me.ShowNamesCheckBox = New System.Windows.Forms.CheckBox()
        Me.LBLGeneralSettings = New System.Windows.Forms.Label()
        Me.DommeSettingsTabPage = New System.Windows.Forms.TabPage()
        Me.DommeSettingsBodyPanel = New System.Windows.Forms.Panel()
        Me.GroupBox39 = New System.Windows.Forms.GroupBox()
        Me.CBHonorificInclude = New System.Windows.Forms.CheckBox()
        Me.CBHonorificCapitalized = New System.Windows.Forms.CheckBox()
        Me.TBHonorific = New System.Windows.Forms.TextBox()
        Me.DommeStatsGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.LBLEmpathy = New System.Windows.Forms.Label()
        Me.NBEmpathy = New System.Windows.Forms.NumericUpDown()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.NBDomBirthdayDay = New System.Windows.Forms.NumericUpDown()
        Me.TBDomEyeColor = New System.Windows.Forms.TextBox()
        Me.TBDomHairColor = New System.Windows.Forms.TextBox()
        Me.DomAgeNumberBox = New System.Windows.Forms.NumericUpDown()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.NBDomBirthdayMonth = New System.Windows.Forms.NumericUpDown()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.CBDomTattoos = New System.Windows.Forms.CheckBox()
        Me.CBDomFreckles = New System.Windows.Forms.CheckBox()
        Me.domhairlengthComboBox = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DommePubicHairComboBox = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.boobComboBox = New System.Windows.Forms.ComboBox()
        Me.DomLevelDescLabel = New System.Windows.Forms.Label()
        Me.DominationLevel = New System.Windows.Forms.NumericUpDown()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.GBDomPetNames = New System.Windows.Forms.GroupBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.petnameBox7 = New System.Windows.Forms.TextBox()
        Me.petnameBox8 = New System.Windows.Forms.TextBox()
        Me.PetNameBox1 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.petnameBox4 = New System.Windows.Forms.TextBox()
        Me.petnameBox6 = New System.Windows.Forms.TextBox()
        Me.petnameBox2 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.petnameBox5 = New System.Windows.Forms.TextBox()
        Me.petnameBox3 = New System.Windows.Forms.TextBox()
        Me.GBDomOrgasms = New System.Windows.Forms.GroupBox()
        Me.CBLockOrgasmChances = New System.Windows.Forms.CheckBox()
        Me.orgasmlockrandombutton = New System.Windows.Forms.Button()
        Me.CBDomOrgasmEnds = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.orgasmsperlockButton = New System.Windows.Forms.Button()
        Me.OrgasmsPerComboBox = New System.Windows.Forms.ComboBox()
        Me.orgasmsperLabel = New System.Windows.Forms.Label()
        Me.limitcheckbox = New System.Windows.Forms.CheckBox()
        Me.OrgasmsPerNumBox = New System.Windows.Forms.NumericUpDown()
        Me.CBDomDenialEnds = New System.Windows.Forms.CheckBox()
        Me.AllowsOrgasmComboBox = New System.Windows.Forms.ComboBox()
        Me.RuinsOrgasmsComboBox = New System.Windows.Forms.ComboBox()
        Me.GBDomPersonality = New System.Windows.Forms.GroupBox()
        Me.degradingCheckBox = New System.Windows.Forms.CheckBox()
        Me.sadisticCheckBox = New System.Windows.Forms.CheckBox()
        Me.supremacistCheckBox = New System.Windows.Forms.CheckBox()
        Me.vulgarCheckBox = New System.Windows.Forms.CheckBox()
        Me.crazyCheckBox = New System.Windows.Forms.CheckBox()
        Me.CondescendingCheckBox = New System.Windows.Forms.CheckBox()
        Me.GBDomRanges = New System.Windows.Forms.GroupBox()
        Me.NBDomMoodMax = New System.Windows.Forms.NumericUpDown()
        Me.NBDomMoodMin = New System.Windows.Forms.NumericUpDown()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.NBSubAgeMax = New System.Windows.Forms.NumericUpDown()
        Me.NBSubAgeMin = New System.Windows.Forms.NumericUpDown()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.NBSelfAgeMax = New System.Windows.Forms.NumericUpDown()
        Me.NBSelfAgeMin = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.NBAvgCockMax = New System.Windows.Forms.NumericUpDown()
        Me.NBAvgCockMin = New System.Windows.Forms.NumericUpDown()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.GBDomTypingStyle = New System.Windows.Forms.GroupBox()
        Me.TBEmoteEnd = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.TBEmote = New System.Windows.Forms.TextBox()
        Me.NBTypoChance = New System.Windows.Forms.NumericUpDown()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.CBMeMyMine = New System.Windows.Forms.CheckBox()
        Me.GroupBox63 = New System.Windows.Forms.GroupBox()
        Me.LCaseCheckBox = New System.Windows.Forms.CheckBox()
        Me.apostropheCheckBox = New System.Windows.Forms.CheckBox()
        Me.periodCheckBox = New System.Windows.Forms.CheckBox()
        Me.commaCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.DommeSettingsHeaderPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BTNSaveDomSet = New System.Windows.Forms.Button()
        Me.DommeSettingsSaveButton = New System.Windows.Forms.Button()
        Me.DommeSettingsLogo = New System.Windows.Forms.PictureBox()
        Me.DommeSettingsHeaderLabel = New System.Windows.Forms.Label()
        Me.DommeSettingsDescriptionGroupBox = New System.Windows.Forms.GroupBox()
        Me.DommeSettingsDescriptionLabel = New System.Windows.Forms.Label()
        Me.TabPage10 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox22 = New System.Windows.Forms.GroupBox()
        Me.NBWritingTaskMax = New System.Windows.Forms.NumericUpDown()
        Me.NBWritingTaskMin = New System.Windows.Forms.NumericUpDown()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.GroupBox45 = New System.Windows.Forms.GroupBox()
        Me.CockAndBallTortureLevelLbl = New System.Windows.Forms.Label()
        Me.BallTortureEnabledCB = New System.Windows.Forms.CheckBox()
        Me.CockTortureEnabledCB = New System.Windows.Forms.CheckBox()
        Me.CockAndBallTortureLevelSlider = New System.Windows.Forms.TrackBar()
        Me.GroupBox35 = New System.Windows.Forms.GroupBox()
        Me.GroupBox38 = New System.Windows.Forms.GroupBox()
        Me.TBNo = New System.Windows.Forms.TextBox()
        Me.GroupBox37 = New System.Windows.Forms.GroupBox()
        Me.TBYes = New System.Windows.Forms.TextBox()
        Me.GroupBox36 = New System.Windows.Forms.GroupBox()
        Me.TBGreeting = New System.Windows.Forms.TextBox()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.TimeBoxWakeUp = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.LBLSubSettingsDescription = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.LBLMaxExtremeHold = New System.Windows.Forms.Label()
        Me.LBLMinExtremeHold = New System.Windows.Forms.Label()
        Me.ExtremeEdgeHoldMinimum = New System.Windows.Forms.NumericUpDown()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.ExtremeEdgeHoldMaximum = New System.Windows.Forms.NumericUpDown()
        Me.LBLMaxLongHold = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.LBLMinLongHold = New System.Windows.Forms.Label()
        Me.LongEdgeHoldMinimum = New System.Windows.Forms.NumericUpDown()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.LongEdgeHoldMaximum = New System.Windows.Forms.NumericUpDown()
        Me.LBLMaxHold = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.NBLongEdge = New System.Windows.Forms.NumericUpDown()
        Me.HoldEdgeMinimumUnits = New System.Windows.Forms.Label()
        Me.UseAverageEdgeThresholdCB = New System.Windows.Forms.CheckBox()
        Me.AllowLongEdgeInterruptCB = New System.Windows.Forms.CheckBox()
        Me.HoldEdgeMinimum = New System.Windows.Forms.NumericUpDown()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.HoldEdgeMaximum = New System.Windows.Forms.NumericUpDown()
        Me.AllowLongEdgeTauntCB = New System.Windows.Forms.CheckBox()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.GroupBox32 = New System.Windows.Forms.GroupBox()
        Me.LBLSubBdayFormat = New System.Windows.Forms.Label()
        Me.ChastityDeviceContainsSpikesCB = New System.Windows.Forms.CheckBox()
        Me.CBOwnChastity = New System.Windows.Forms.CheckBox()
        Me.DoesChastityDeviceRequirePiercingCB = New System.Windows.Forms.CheckBox()
        Me.CBHimHer = New System.Windows.Forms.CheckBox()
        Me.CBBallsToPussy = New System.Windows.Forms.CheckBox()
        Me.CBCockToClit = New System.Windows.Forms.CheckBox()
        Me.NBBirthdayDay = New System.Windows.Forms.NumericUpDown()
        Me.CBSubCircumcised = New System.Windows.Forms.CheckBox()
        Me.CBSubPierced = New System.Windows.Forms.CheckBox()
        Me.TBSubEyeColor = New System.Windows.Forms.TextBox()
        Me.TBSubHairColor = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.LBLSubInches = New System.Windows.Forms.Label()
        Me.subAgeNumBox = New System.Windows.Forms.NumericUpDown()
        Me.NBBirthdayMonth = New System.Windows.Forms.NumericUpDown()
        Me.LBLSubCockSize = New System.Windows.Forms.Label()
        Me.CockSizeNumBox = New System.Windows.Forms.NumericUpDown()
        Me.LBLSubEye = New System.Windows.Forms.Label()
        Me.LBLSubHair = New System.Windows.Forms.Label()
        Me.LBLSubBirthday = New System.Windows.Forms.Label()
        Me.LBLSubAge = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.TabPage16 = New System.Windows.Forms.TabPage()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.ScriptNavPanel = New System.Windows.Forms.Panel()
        Me.SelectAllScriptsButton = New System.Windows.Forms.Button()
        Me.TCScripts = New System.Windows.Forms.TabControl()
        Me.ScriptsStartTab = New System.Windows.Forms.TabPage()
        Me.StartScripts = New System.Windows.Forms.CheckedListBox()
        Me.ScriptsModuleTab = New System.Windows.Forms.TabPage()
        Me.ModuleScripts = New System.Windows.Forms.CheckedListBox()
        Me.ScriptsLinkTab = New System.Windows.Forms.TabPage()
        Me.LinkScripts = New System.Windows.Forms.CheckedListBox()
        Me.ScriptsEndTab = New System.Windows.Forms.TabPage()
        Me.EndScripts = New System.Windows.Forms.CheckedListBox()
        Me.SelectAvailableScriptsButton = New System.Windows.Forms.Button()
        Me.ScriptTitle = New System.Windows.Forms.Label()
        Me.BTNScriptOpen = New System.Windows.Forms.Button()
        Me.SelectNoScriptsButton = New System.Windows.Forms.Button()
        Me.ScriptInfoPanel = New System.Windows.Forms.Panel()
        Me.ScriptsDescriptionGroup = New System.Windows.Forms.GroupBox()
        Me.ScriptInfoTextArea = New System.Windows.Forms.RichTextBox()
        Me.LBLScriptReq = New System.Windows.Forms.Label()
        Me.ScriptsRequirementsGroup = New System.Windows.Forms.GroupBox()
        Me.ScriptRequirements = New System.Windows.Forms.RichTextBox()
        Me.GroupBox43 = New System.Windows.Forms.GroupBox()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.GernreImagesTab = New System.Windows.Forms.TabControl()
        Me.TpImagesUrlFiles = New System.Windows.Forms.TabPage()
        Me.PreviewRemoteImagesCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox66 = New System.Windows.Forms.GroupBox()
        Me.PBURLPreview = New System.Windows.Forms.PictureBox()
        Me.BTNURLFilesAll = New System.Windows.Forms.Button()
        Me.BTNURLFilesNone = New System.Windows.Forms.Button()
        Me.RemoteMediaContainerList = New System.Windows.Forms.CheckedListBox()
        Me.TpImagesGenre = New System.Windows.Forms.TabPage()
        Me.GrbImageUrlFiles = New System.Windows.Forms.GroupBox()
        Me.TlpImageUrls = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnImageUrlButt = New System.Windows.Forms.Button()
        Me.BtnImageUrlBoobs = New System.Windows.Forms.Button()
        Me.BtnImageUrlBlowjob = New System.Windows.Forms.Button()
        Me.BtnImageUrlCaptions = New System.Windows.Forms.Button()
        Me.BtnImageUrlHentai = New System.Windows.Forms.Button()
        Me.BtnImageUrlGay = New System.Windows.Forms.Button()
        Me.BtnImageUrlGeneral = New System.Windows.Forms.Button()
        Me.BtnImageUrlHardcore = New System.Windows.Forms.Button()
        Me.BtnImageUrlLesbian = New System.Windows.Forms.Button()
        Me.BtnImageUrlLezdom = New System.Windows.Forms.Button()
        Me.BtnImageUrlMaledom = New System.Windows.Forms.Button()
        Me.BtnImageUrlFemdom = New System.Windows.Forms.Button()
        Me.BtnImageUrlSoftcore = New System.Windows.Forms.Button()
        Me.ChbImageUrlHardcore = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlButts = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlMaledom = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlGay = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlSoftcore = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlBoobs = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlLesbian = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlBlowjob = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlCaptions = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlGeneral = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlFemdom = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlHentai = New System.Windows.Forms.CheckBox()
        Me.ChbImageUrlLezdom = New System.Windows.Forms.CheckBox()
        Me.TxbImageUrlBlowjob = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlSoftcore = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlLezdom = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlFemdom = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlHardcore = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlHentai = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlGay = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlLesbian = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlMaledom = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlCaptions = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlGeneral = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlBoobs = New System.Windows.Forms.TextBox()
        Me.TxbImageUrlButts = New System.Windows.Forms.TextBox()
        Me.GbxImagesGenre = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LocalHardcoreDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalHardcoreDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalHardcoreSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalHardcoreEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalSoftcoreEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalSoftcoreDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalButtSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalSoftcoreSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalBoobsSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalLezdomSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalGeneralSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalLesbianSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalCaptionsSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalLesbianEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalMaledomSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalBlowjobEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalGaySubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalHentaiSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalBlowjobSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalFemdomSubdirectoryCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalButtDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalFemdomEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalLesbianDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalSoftcoreDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalLezdomEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalBoobsDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalHentaiEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalBlowjobDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalGayEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalGeneralDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalMaledomEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalFemdomDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalLesbianDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalCaptionsDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalCaptionsEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalLezdomDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalMaledomDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalButtDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalHentaiDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalGeneralEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalGayDirectoryTextBox = New System.Windows.Forms.TextBox()
        Me.LocalBoobsEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalButtEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalBlowjobDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalFemdomDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalBoobsDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalLezdomDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalHentaiDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalGayDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalMaledomDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalCaptionsDirectoryButton = New System.Windows.Forms.Button()
        Me.LocalGeneralDirectoryButton = New System.Windows.Forms.Button()
        Me.TabPage33 = New System.Windows.Forms.TabPage()
        Me.LocalTagsTab = New System.Windows.Forms.TabControl()
        Me.TabPage34 = New System.Windows.Forms.TabPage()
        Me.CBTagSeeThrough = New System.Windows.Forms.RadioButton()
        Me.CBTagAllFours = New System.Windows.Forms.CheckBox()
        Me.CBTagGlaring = New System.Windows.Forms.CheckBox()
        Me.CBTagSmiling = New System.Windows.Forms.CheckBox()
        Me.DommeTagDirInput = New System.Windows.Forms.TextBox()
        Me.CBTagPiercing = New System.Windows.Forms.CheckBox()
        Me.CBTagLegs = New System.Windows.Forms.CheckBox()
        Me.TBTagFurniture = New System.Windows.Forms.TextBox()
        Me.CBTagFurniture = New System.Windows.Forms.CheckBox()
        Me.TBTagSexToy = New System.Windows.Forms.TextBox()
        Me.CBTagSexToy = New System.Windows.Forms.CheckBox()
        Me.TBTagTattoo = New System.Windows.Forms.TextBox()
        Me.CBTagTattoo = New System.Windows.Forms.CheckBox()
        Me.TBTagUnderwear = New System.Windows.Forms.TextBox()
        Me.CBTagUnderwear = New System.Windows.Forms.CheckBox()
        Me.TBTagGarment = New System.Windows.Forms.TextBox()
        Me.CBTagGarment = New System.Windows.Forms.CheckBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.CBTagHandsCovering = New System.Windows.Forms.RadioButton()
        Me.CBTagGarmentCovering = New System.Windows.Forms.RadioButton()
        Me.CBTagCloseUp = New System.Windows.Forms.CheckBox()
        Me.CBTagNaked = New System.Windows.Forms.RadioButton()
        Me.CBTagSideView = New System.Windows.Forms.CheckBox()
        Me.BTNTagPrevious = New System.Windows.Forms.Button()
        Me.CBTagHalfDressed = New System.Windows.Forms.RadioButton()
        Me.BTNTagNext = New System.Windows.Forms.Button()
        Me.CBTagFullyDressed = New System.Windows.Forms.RadioButton()
        Me.LBLTagCount = New System.Windows.Forms.Label()
        Me.CBTagSucking = New System.Windows.Forms.CheckBox()
        Me.CBTagMasturbating = New System.Windows.Forms.CheckBox()
        Me.CBTagFeet = New System.Windows.Forms.CheckBox()
        Me.CBTagBoobs = New System.Windows.Forms.CheckBox()
        Me.CBTagAss = New System.Windows.Forms.CheckBox()
        Me.CBTagPussy = New System.Windows.Forms.CheckBox()
        Me.BTNTagSave = New System.Windows.Forms.Button()
        Me.DommeTagDirectoryButton = New System.Windows.Forms.Button()
        Me.ImageTagPictureBox = New System.Windows.Forms.PictureBox()
        Me.CBTagFace = New System.Windows.Forms.CheckBox()
        Me.FileDropDownLabel = New System.Windows.Forms.TabPage()
        Me.LocalTagImageNavGroup = New System.Windows.Forms.GroupBox()
        Me.FileTagCombo = New System.Windows.Forms.ComboBox()
        Me.LBLLocalTagCount = New System.Windows.Forms.Label()
        Me.FileTagNextButton = New System.Windows.Forms.Button()
        Me.FileTagPreviousButton = New System.Windows.Forms.Button()
        Me.GenreDropDownLabel = New System.Windows.Forms.Label()
        Me.GenreCombo = New System.Windows.Forms.ComboBox()
        Me.GroupBox55 = New System.Windows.Forms.GroupBox()
        Me.CBTagNurse = New System.Windows.Forms.CheckBox()
        Me.CBTagSchoolgirl = New System.Windows.Forms.CheckBox()
        Me.CBTagMaid = New System.Windows.Forms.CheckBox()
        Me.CBTagTeacher = New System.Windows.Forms.CheckBox()
        Me.CBTagSuperhero = New System.Windows.Forms.CheckBox()
        Me.GroupBox53 = New System.Windows.Forms.GroupBox()
        Me.CBTagTrap = New System.Windows.Forms.CheckBox()
        Me.CBTagTentacles = New System.Windows.Forms.CheckBox()
        Me.CBTagMonsterGirl = New System.Windows.Forms.CheckBox()
        Me.CBTagBukkake = New System.Windows.Forms.CheckBox()
        Me.CBTagGanguro = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyWriting = New System.Windows.Forms.CheckBox()
        Me.CBTagMahouShoujo = New System.Windows.Forms.CheckBox()
        Me.CBTagBakunyuu = New System.Windows.Forms.CheckBox()
        Me.CBTagAhegao = New System.Windows.Forms.CheckBox()
        Me.CBTagShibari = New System.Windows.Forms.CheckBox()
        Me.GroupBox49 = New System.Windows.Forms.GroupBox()
        Me.CBTagBodyMouth = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyAss = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyFace = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyLegs = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyBalls = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyCock = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyFeet = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyNipples = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyPussy = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyTits = New System.Windows.Forms.CheckBox()
        Me.CBTagBodyFingers = New System.Windows.Forms.CheckBox()
        Me.GroupBox46 = New System.Windows.Forms.GroupBox()
        Me.CBTagMultiSub = New System.Windows.Forms.CheckBox()
        Me.CBTagMultiDom = New System.Windows.Forms.CheckBox()
        Me.CBTagFemdom = New System.Windows.Forms.CheckBox()
        Me.CBTag2M = New System.Windows.Forms.CheckBox()
        Me.CBTagFutadom = New System.Windows.Forms.CheckBox()
        Me.CBTagFemsub = New System.Windows.Forms.CheckBox()
        Me.CBTag2Futa = New System.Windows.Forms.CheckBox()
        Me.CBTagMaledom = New System.Windows.Forms.CheckBox()
        Me.CBTag3M = New System.Windows.Forms.CheckBox()
        Me.CBTagFutasub = New System.Windows.Forms.CheckBox()
        Me.CBTag3Futa = New System.Windows.Forms.CheckBox()
        Me.CBTagMalesub = New System.Windows.Forms.CheckBox()
        Me.CBTag2F = New System.Windows.Forms.CheckBox()
        Me.CBTag1Futa = New System.Windows.Forms.CheckBox()
        Me.CBTag1M = New System.Windows.Forms.CheckBox()
        Me.CBTag1F = New System.Windows.Forms.CheckBox()
        Me.CBTag3F = New System.Windows.Forms.CheckBox()
        Me.GroupBox54 = New System.Windows.Forms.GroupBox()
        Me.CBTagTattoos = New System.Windows.Forms.CheckBox()
        Me.CBTagAnalToy = New System.Windows.Forms.CheckBox()
        Me.CBTagDomme = New System.Windows.Forms.CheckBox()
        Me.CBTagPocketPussy = New System.Windows.Forms.CheckBox()
        Me.CBTagWatersports = New System.Windows.Forms.CheckBox()
        Me.CBTagStockings = New System.Windows.Forms.CheckBox()
        Me.CBTagCumshot = New System.Windows.Forms.CheckBox()
        Me.CBTagCumEating = New System.Windows.Forms.CheckBox()
        Me.CBTagVibrator = New System.Windows.Forms.CheckBox()
        Me.CBTagDildo = New System.Windows.Forms.CheckBox()
        Me.CBTagKissing = New System.Windows.Forms.CheckBox()
        Me.BdsmTagGroup = New System.Windows.Forms.GroupBox()
        Me.CBTagBallTorture = New System.Windows.Forms.CheckBox()
        Me.CBTagGag = New System.Windows.Forms.CheckBox()
        Me.CBTagBlindfold = New System.Windows.Forms.CheckBox()
        Me.CBTagWhipping = New System.Windows.Forms.CheckBox()
        Me.CBTagCockTorture = New System.Windows.Forms.CheckBox()
        Me.CBTagElectro = New System.Windows.Forms.CheckBox()
        Me.CBTagHotWax = New System.Windows.Forms.CheckBox()
        Me.CBTagClamps = New System.Windows.Forms.CheckBox()
        Me.CBTagStrapon = New System.Windows.Forms.CheckBox()
        Me.CBTagSpanking = New System.Windows.Forms.CheckBox()
        Me.CBTagNeedles = New System.Windows.Forms.CheckBox()
        Me.GroupBox50 = New System.Windows.Forms.GroupBox()
        Me.CBTagRimming = New System.Windows.Forms.CheckBox()
        Me.CBTagFacesitting = New System.Windows.Forms.CheckBox()
        Me.CBTagMissionary = New System.Windows.Forms.CheckBox()
        Me.CBTagMasturbation = New System.Windows.Forms.CheckBox()
        Me.CBTagRCowgirl = New System.Windows.Forms.CheckBox()
        Me.CBTagFingering = New System.Windows.Forms.CheckBox()
        Me.CBTagGangbang = New System.Windows.Forms.CheckBox()
        Me.CBTagBlowjob = New System.Windows.Forms.CheckBox()
        Me.CBTagDP = New System.Windows.Forms.CheckBox()
        Me.CBTagHandjob = New System.Windows.Forms.CheckBox()
        Me.CBTagStanding = New System.Windows.Forms.CheckBox()
        Me.CBTagFootjob = New System.Windows.Forms.CheckBox()
        Me.CBTagCowgirl = New System.Windows.Forms.CheckBox()
        Me.CBTagDoggyStyle = New System.Windows.Forms.CheckBox()
        Me.CBTagTitjob = New System.Windows.Forms.CheckBox()
        Me.CBTagCunnilingus = New System.Windows.Forms.CheckBox()
        Me.CBTagAnalSex = New System.Windows.Forms.CheckBox()
        Me.GroupBox48 = New System.Windows.Forms.GroupBox()
        Me.CBTagArtwork = New System.Windows.Forms.CheckBox()
        Me.CBTagOutdoors = New System.Windows.Forms.CheckBox()
        Me.CBTagPOV = New System.Windows.Forms.CheckBox()
        Me.CBTagHardcore = New System.Windows.Forms.CheckBox()
        Me.CBTagTD = New System.Windows.Forms.CheckBox()
        Me.CBTagGay = New System.Windows.Forms.CheckBox()
        Me.CBTagBath = New System.Windows.Forms.CheckBox()
        Me.CBTagBisexual = New System.Windows.Forms.CheckBox()
        Me.CBTagCFNM = New System.Windows.Forms.CheckBox()
        Me.CBTagLesbian = New System.Windows.Forms.CheckBox()
        Me.CBTagSoloFuta = New System.Windows.Forms.CheckBox()
        Me.CBTagSM = New System.Windows.Forms.CheckBox()
        Me.CBTagBondage = New System.Windows.Forms.CheckBox()
        Me.CBTagSoloM = New System.Windows.Forms.CheckBox()
        Me.CBTagSoloF = New System.Windows.Forms.CheckBox()
        Me.CBTagChastity = New System.Windows.Forms.CheckBox()
        Me.CBTagShower = New System.Windows.Forms.CheckBox()
        Me.SaveTagButton = New System.Windows.Forms.Button()
        Me.LocalTagPictureBox = New System.Windows.Forms.PictureBox()
        Me.UrlFilesTab = New System.Windows.Forms.TabPage()
        Me.UrlFilesPanel = New System.Windows.Forms.Panel()
        Me.SelectBlogDropDown = New System.Windows.Forms.ComboBox()
        Me.UrlImageContinueButton = New System.Windows.Forms.Button()
        Me.UrlImageAddAndContinue = New System.Windows.Forms.Button()
        Me.BTNWICancel = New System.Windows.Forms.Button()
        Me.CBWIReview = New System.Windows.Forms.CheckBox()
        Me.BTNWIBrowse = New System.Windows.Forms.Button()
        Me.TBWIDirectory = New System.Windows.Forms.TextBox()
        Me.BTNWIDisliked = New System.Windows.Forms.Button()
        Me.BTNWILiked = New System.Windows.Forms.Button()
        Me.UrlImageRemoveButton = New System.Windows.Forms.Button()
        Me.CBWISaveToDisk = New System.Windows.Forms.CheckBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.WebImageProgressBar = New System.Windows.Forms.ProgressBar()
        Me.CreateBlogContainerButton = New System.Windows.Forms.Button()
        Me.LBLWebImageCount = New System.Windows.Forms.Label()
        Me.BTNWISave = New System.Windows.Forms.Button()
        Me.UrlFilesPreviousImageButton = New System.Windows.Forms.Button()
        Me.UrlFilesNextImageButton = New System.Windows.Forms.Button()
        Me.WebPictureBox = New System.Windows.Forms.PictureBox()
        Me.ImageBlogs = New System.Windows.Forms.Label()
        Me.TpVideoSettings = New System.Windows.Forms.TabPage()
        Me.VideoSettingsPanel = New System.Windows.Forms.Panel()
        Me.VideoLayoutTable = New System.Windows.Forms.TableLayoutPanel()
        Me.VideoGeneralPanel = New System.Windows.Forms.Panel()
        Me.VideoGeneralGroupBox = New System.Windows.Forms.GroupBox()
        Me.LblVideoGeneralTotal = New System.Windows.Forms.Label()
        Me.TxbVideoGeneral = New System.Windows.Forms.TextBox()
        Me.BTNVideoGeneral = New System.Windows.Forms.Button()
        Me.CBVideoGeneral = New System.Windows.Forms.CheckBox()
        Me.VideoSpecialGroupBox = New System.Windows.Forms.GroupBox()
        Me.LblVideoCHTotal = New System.Windows.Forms.Label()
        Me.LblVideoJOITotal = New System.Windows.Forms.Label()
        Me.TxbVideoCH = New System.Windows.Forms.TextBox()
        Me.TxbVideoJOI = New System.Windows.Forms.TextBox()
        Me.BTNVideoCH = New System.Windows.Forms.Button()
        Me.BTNVideoJOI = New System.Windows.Forms.Button()
        Me.CBVideoJOI = New System.Windows.Forms.CheckBox()
        Me.CBVideoCH = New System.Windows.Forms.CheckBox()
        Me.VideoGenreGroupBox = New System.Windows.Forms.GroupBox()
        Me.LblVideoFemsubTotal = New System.Windows.Forms.Label()
        Me.TxbVideoFemsub = New System.Windows.Forms.TextBox()
        Me.LblVideoFemdomTotal = New System.Windows.Forms.Label()
        Me.TxbVideoFemdom = New System.Windows.Forms.TextBox()
        Me.TxbVideoBlowjob = New System.Windows.Forms.TextBox()
        Me.LblVideoBlowjobTotal = New System.Windows.Forms.Label()
        Me.TxbVideoLesbian = New System.Windows.Forms.TextBox()
        Me.TxbVideoSoftCore = New System.Windows.Forms.TextBox()
        Me.LblVideoLesbianTotal = New System.Windows.Forms.Label()
        Me.VideoHardCorePathTextBox = New System.Windows.Forms.TextBox()
        Me.BTNVideoFemSub = New System.Windows.Forms.Button()
        Me.LblVideoSoftCoreTotal = New System.Windows.Forms.Label()
        Me.BTNVideoFemDom = New System.Windows.Forms.Button()
        Me.BTNVideoBlowjob = New System.Windows.Forms.Button()
        Me.LblVideoHardCoreTotal = New System.Windows.Forms.Label()
        Me.BTNVideoLesbian = New System.Windows.Forms.Button()
        Me.BTNVideoSoftCore = New System.Windows.Forms.Button()
        Me.VideoSetHardcorePathButton = New System.Windows.Forms.Button()
        Me.VideoEnableHardcoreCheckBox = New System.Windows.Forms.CheckBox()
        Me.VideoEnableSoftcoreCheckBox = New System.Windows.Forms.CheckBox()
        Me.CBVideoLesbian = New System.Windows.Forms.CheckBox()
        Me.CBVideoBlowjob = New System.Windows.Forms.CheckBox()
        Me.CBVideoFemsub = New System.Windows.Forms.CheckBox()
        Me.CBVideoFemdom = New System.Windows.Forms.CheckBox()
        Me.VideoDommePanel = New System.Windows.Forms.Panel()
        Me.VideoDommeGeneralGroupBox = New System.Windows.Forms.GroupBox()
        Me.VideoTotalDommeGeneral = New System.Windows.Forms.Label()
        Me.VideoDommeGeneralPathTextBox = New System.Windows.Forms.TextBox()
        Me.BTNVideoGeneralD = New System.Windows.Forms.Button()
        Me.CBVideoGeneralD = New System.Windows.Forms.CheckBox()
        Me.GbxVideoSpecialD = New System.Windows.Forms.GroupBox()
        Me.LblVideoCHTotalD = New System.Windows.Forms.Label()
        Me.LblVideoJOITotalD = New System.Windows.Forms.Label()
        Me.TxbVideoCHD = New System.Windows.Forms.TextBox()
        Me.TxbVideoJOID = New System.Windows.Forms.TextBox()
        Me.BTNVideoCHD = New System.Windows.Forms.Button()
        Me.BTNVideoJOID = New System.Windows.Forms.Button()
        Me.CBVideoJOID = New System.Windows.Forms.CheckBox()
        Me.CBVideoCHD = New System.Windows.Forms.CheckBox()
        Me.GbxVideoGenreD = New System.Windows.Forms.GroupBox()
        Me.LblVideoFemsubTotalD = New System.Windows.Forms.Label()
        Me.TxbVideoFemsubD = New System.Windows.Forms.TextBox()
        Me.LblVideoFemdomTotalD = New System.Windows.Forms.Label()
        Me.TxbVideoFemdomD = New System.Windows.Forms.TextBox()
        Me.TxbVideoBlowjobD = New System.Windows.Forms.TextBox()
        Me.LblVideoBlowjobTotalD = New System.Windows.Forms.Label()
        Me.TxbVideoLesbianD = New System.Windows.Forms.TextBox()
        Me.TxbVideoSoftCoreD = New System.Windows.Forms.TextBox()
        Me.LblVideoLesbianTotalD = New System.Windows.Forms.Label()
        Me.TxbVideoHardCoreD = New System.Windows.Forms.TextBox()
        Me.BTNVideoFemSubD = New System.Windows.Forms.Button()
        Me.LblVideoSoftCoreTotalD = New System.Windows.Forms.Label()
        Me.BTNVideoFemDomD = New System.Windows.Forms.Button()
        Me.BTNVideoBlowjobD = New System.Windows.Forms.Button()
        Me.LblVideoHardCoreTotalD = New System.Windows.Forms.Label()
        Me.BTNVideoLesbianD = New System.Windows.Forms.Button()
        Me.BTNVideoSoftCoreD = New System.Windows.Forms.Button()
        Me.BTNVideoHardCoreD = New System.Windows.Forms.Button()
        Me.CBVideoHardcoreD = New System.Windows.Forms.CheckBox()
        Me.CBVideoSoftCoreD = New System.Windows.Forms.CheckBox()
        Me.CBVideoLesbianD = New System.Windows.Forms.CheckBox()
        Me.CBVideoBlowjobD = New System.Windows.Forms.CheckBox()
        Me.CBVideoFemsubD = New System.Windows.Forms.CheckBox()
        Me.CBVideoFemdomD = New System.Windows.Forms.CheckBox()
        Me.VideoHeaderPanel = New System.Windows.Forms.Panel()
        Me.VideoHeaderLabel = New System.Windows.Forms.Label()
        Me.VideoRefreshButton = New System.Windows.Forms.Button()
        Me.VideoLogo = New System.Windows.Forms.PictureBox()
        Me.VideoDescriptionGroupBox = New System.Windows.Forms.GroupBox()
        Me.VideoDescriptionLabel = New System.Windows.Forms.Label()
        Me.AppsTabPage = New System.Windows.Forms.TabPage()
        Me.AppsSettingsTabList = New System.Windows.Forms.TabControl()
        Me.GlitterAppTabPage = New System.Windows.Forms.TabPage()
        Me.DommeGlitterSettings = New Tease_AI.GlitterSettingsControl()
        Me.GBGlitter1 = New System.Windows.Forms.GroupBox()
        Me.BtnContact1ImageDirClear = New System.Windows.Forms.Button()
        Me.BtnContact1ImageDir = New System.Windows.Forms.Button()
        Me.TbxContact1ImageDir = New System.Windows.Forms.TextBox()
        Me.BTNGlitter1 = New System.Windows.Forms.Button()
        Me.LBLGlitterNC1 = New System.Windows.Forms.Label()
        Me.LBLGlitterSlider1 = New System.Windows.Forms.Label()
        Me.GlitterSlider1 = New System.Windows.Forms.TrackBar()
        Me.CBGlitter1 = New System.Windows.Forms.CheckBox()
        Me.TBGlitter1 = New System.Windows.Forms.TextBox()
        Me.GlitterAV1 = New System.Windows.Forms.PictureBox()
        Me.GBGlitter3 = New System.Windows.Forms.GroupBox()
        Me.BtnContact3ImageDirClear = New System.Windows.Forms.Button()
        Me.BtnContact3ImageDir = New System.Windows.Forms.Button()
        Me.TbxContact3ImageDir = New System.Windows.Forms.TextBox()
        Me.BTNGlitter3 = New System.Windows.Forms.Button()
        Me.LBLGlitterNC3 = New System.Windows.Forms.Label()
        Me.LBLGlitterSlider3 = New System.Windows.Forms.Label()
        Me.GlitterSlider3 = New System.Windows.Forms.TrackBar()
        Me.CBGlitter3 = New System.Windows.Forms.CheckBox()
        Me.TBGlitter3 = New System.Windows.Forms.TextBox()
        Me.GlitterAV3 = New System.Windows.Forms.PictureBox()
        Me.GBGlitter2 = New System.Windows.Forms.GroupBox()
        Me.BtnContact2ImageDirClear = New System.Windows.Forms.Button()
        Me.BtnContact2ImageDir = New System.Windows.Forms.Button()
        Me.TbxContact2ImageDir = New System.Windows.Forms.TextBox()
        Me.BTNGlitter2 = New System.Windows.Forms.Button()
        Me.LBLGlitterNC2 = New System.Windows.Forms.Label()
        Me.LBLGlitterSlider2 = New System.Windows.Forms.Label()
        Me.GlitterSlider2 = New System.Windows.Forms.TrackBar()
        Me.CBGlitter2 = New System.Windows.Forms.CheckBox()
        Me.TBGlitter2 = New System.Windows.Forms.TextBox()
        Me.GlitterAV2 = New System.Windows.Forms.PictureBox()
        Me.TpGames = New System.Windows.Forms.TabPage()
        Me.CBIncludeGifs = New System.Windows.Forms.CheckBox()
        Me.LblCardsSetupNote = New System.Windows.Forms.Label()
        Me.CBGameSounds = New System.Windows.Forms.CheckBox()
        Me.GbxCardsGold = New System.Windows.Forms.GroupBox()
        Me.GN6 = New System.Windows.Forms.TextBox()
        Me.GP6 = New System.Windows.Forms.PictureBox()
        Me.GN2 = New System.Windows.Forms.TextBox()
        Me.GP2 = New System.Windows.Forms.PictureBox()
        Me.GP5 = New System.Windows.Forms.PictureBox()
        Me.GN1 = New System.Windows.Forms.TextBox()
        Me.GP1 = New System.Windows.Forms.PictureBox()
        Me.GN5 = New System.Windows.Forms.TextBox()
        Me.GN3 = New System.Windows.Forms.TextBox()
        Me.GP3 = New System.Windows.Forms.PictureBox()
        Me.GP4 = New System.Windows.Forms.PictureBox()
        Me.GN4 = New System.Windows.Forms.TextBox()
        Me.GbxCardsBackground = New System.Windows.Forms.GroupBox()
        Me.CardBack = New System.Windows.Forms.PictureBox()
        Me.GbxCardsBronze = New System.Windows.Forms.GroupBox()
        Me.BN6 = New System.Windows.Forms.TextBox()
        Me.BN3 = New System.Windows.Forms.TextBox()
        Me.BP3 = New System.Windows.Forms.PictureBox()
        Me.BP6 = New System.Windows.Forms.PictureBox()
        Me.BN2 = New System.Windows.Forms.TextBox()
        Me.BN5 = New System.Windows.Forms.TextBox()
        Me.BP5 = New System.Windows.Forms.PictureBox()
        Me.BP2 = New System.Windows.Forms.PictureBox()
        Me.BN1 = New System.Windows.Forms.TextBox()
        Me.BN4 = New System.Windows.Forms.TextBox()
        Me.BP4 = New System.Windows.Forms.PictureBox()
        Me.BP1 = New System.Windows.Forms.PictureBox()
        Me.GbxCardsSilver = New System.Windows.Forms.GroupBox()
        Me.SN6 = New System.Windows.Forms.TextBox()
        Me.SP6 = New System.Windows.Forms.PictureBox()
        Me.SN2 = New System.Windows.Forms.TextBox()
        Me.SP2 = New System.Windows.Forms.PictureBox()
        Me.SN1 = New System.Windows.Forms.TextBox()
        Me.SP5 = New System.Windows.Forms.PictureBox()
        Me.SP1 = New System.Windows.Forms.PictureBox()
        Me.SN5 = New System.Windows.Forms.TextBox()
        Me.SN3 = New System.Windows.Forms.TextBox()
        Me.SN4 = New System.Windows.Forms.TextBox()
        Me.SP3 = New System.Windows.Forms.PictureBox()
        Me.SP4 = New System.Windows.Forms.PictureBox()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.TBWishlistComment = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.TBWishlistItem = New System.Windows.Forms.TextBox()
        Me.radioGold = New System.Windows.Forms.RadioButton()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.radioSilver = New System.Windows.Forms.RadioButton()
        Me.TBWishlistURL = New System.Windows.Forms.TextBox()
        Me.NBWishlistCost = New System.Windows.Forms.NumericUpDown()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.BTNWishlistCreate = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.PNLWishList = New System.Windows.Forms.Panel()
        Me.WishlistCostSilver = New System.Windows.Forms.PictureBox()
        Me.LBLWishListText = New System.Windows.Forms.Label()
        Me.LBLWishlistCost = New System.Windows.Forms.Label()
        Me.WishlistCostGold = New System.Windows.Forms.PictureBox()
        Me.LBLWishListName = New System.Windows.Forms.Label()
        Me.WishlistPreview = New System.Windows.Forms.PictureBox()
        Me.AppsSettingsHeaderPanel = New System.Windows.Forms.Panel()
        Me.AppsSettingsLoad = New System.Windows.Forms.Button()
        Me.AppsSettingsSave = New System.Windows.Forms.Button()
        Me.AppsSettingsLogo = New System.Windows.Forms.PictureBox()
        Me.AppsSettingsHeaderLabel = New System.Windows.Forms.Label()
        Me.TabPage26 = New System.Windows.Forms.TabPage()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Button32 = New System.Windows.Forms.Button()
        Me.Button31 = New System.Windows.Forms.Button()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.CBTransparentTime = New System.Windows.Forms.CheckBox()
        Me.LBLDateTimeColor2 = New System.Windows.Forms.Label()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.LBLDateBackColor2 = New System.Windows.Forms.Label()
        Me.LBLTextColor = New System.Windows.Forms.Label()
        Me.LBLChatWindowColor2 = New System.Windows.Forms.Label()
        Me.LBLTextColor2 = New System.Windows.Forms.Label()
        Me.LBLChatTextColor = New System.Windows.Forms.Label()
        Me.LBLBackColor2 = New System.Windows.Forms.Label()
        Me.LBLButtonColor = New System.Windows.Forms.Label()
        Me.LBLChatWindowColor = New System.Windows.Forms.Label()
        Me.LBLBackColor = New System.Windows.Forms.Label()
        Me.LBLChatTextColor2 = New System.Windows.Forms.Label()
        Me.LBLButtonColor2 = New System.Windows.Forms.Label()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CBFlipBack = New System.Windows.Forms.CheckBox()
        Me.PBBackgroundPreview = New System.Windows.Forms.PictureBox()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.CBStretchBack = New System.Windows.Forms.CheckBox()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.RangeSettingsTabPage = New System.Windows.Forms.TabPage()
        Me.RangeSettingsBody = New System.Windows.Forms.Panel()
        Me.RangeSettingsBodyTablePanel = New System.Windows.Forms.TableLayoutPanel()
        Me.RangeSettingsBodyRightColumnPanel = New System.Windows.Forms.Panel()
        Me.RangeSettingsTeaseSlideshowGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.NBNextImageChance = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RangeSettingsGlitterTasksGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.NBTaskCBTTimeMax = New System.Windows.Forms.NumericUpDown()
        Me.NBTaskCBTTimeMin = New System.Windows.Forms.NumericUpDown()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.NBTaskEdgeHoldTimeMax = New System.Windows.Forms.NumericUpDown()
        Me.NBTaskEdgeHoldTimeMin = New System.Windows.Forms.NumericUpDown()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.NBTaskEdgesMax = New System.Windows.Forms.NumericUpDown()
        Me.NBTaskEdgesMin = New System.Windows.Forms.NumericUpDown()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.NBTaskStrokingTimeMax = New System.Windows.Forms.NumericUpDown()
        Me.NBTaskStrokingTimeMin = New System.Windows.Forms.NumericUpDown()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.NBTaskStrokesMax = New System.Windows.Forms.NumericUpDown()
        Me.NBTaskStrokesMin = New System.Windows.Forms.NumericUpDown()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.RangeSettingsVideoTeaseGroupBox = New System.Windows.Forms.GroupBox()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.GreenLightMaximumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.GreenLightMinimumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.RedLightMaximumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.RedLightMinimumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.RangeSettingsCensorshipSucksGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.ShowCensorshipBarMinimumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.HideCensorshipBarMaximumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.HideCensorshipBarMinimumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.CensorshipBarDuringVideoTease = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ShowCensorshipBarMaximumSeconds = New System.Windows.Forms.NumericUpDown()
        Me.RangeSettingsBodyMiddleColumnPanel = New System.Windows.Forms.Panel()
        Me.GBRangeOrgasmChance = New System.Windows.Forms.GroupBox()
        Me.RarelyAllowsPercentLabel = New System.Windows.Forms.Label()
        Me.SometimesAllowsPercentNumberBox = New System.Windows.Forms.NumericUpDown()
        Me.SometimesAllowsPercentLabel = New System.Windows.Forms.Label()
        Me.OftenAllowsPercentLabel = New System.Windows.Forms.Label()
        Me.RarelyAllowsPercentNumberBox = New System.Windows.Forms.NumericUpDown()
        Me.OftenAllowsPercentNumberBox = New System.Windows.Forms.NumericUpDown()
        Me.DommeDecideOrgasmCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox69 = New System.Windows.Forms.GroupBox()
        Me.TypesSpeedVal = New System.Windows.Forms.Label()
        Me.TypeSpeedLabel = New System.Windows.Forms.Label()
        Me.TimedWriting = New System.Windows.Forms.CheckBox()
        Me.TypeSpeedSlider = New System.Windows.Forms.TrackBar()
        Me.GBRangeRuinChance = New System.Windows.Forms.GroupBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.NBRuinSometimes = New System.Windows.Forms.NumericUpDown()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.NBRuinRarely = New System.Windows.Forms.NumericUpDown()
        Me.NBRuinOften = New System.Windows.Forms.NumericUpDown()
        Me.DommeDecideRuinCheckBox = New System.Windows.Forms.CheckBox()
        Me.RangeSettingsBodyLeftColumnPanel = New System.Windows.Forms.Panel()
        Me.RangeSettingsTeaseGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.NBTauntEdging = New System.Windows.Forms.NumericUpDown()
        Me.VideoTauntDescriptionLabel = New System.Windows.Forms.Label()
        Me.LBLStf = New System.Windows.Forms.Label()
        Me.SliderSTF = New System.Windows.Forms.TrackBar()
        Me.VideoTauntSlider = New System.Windows.Forms.TrackBar()
        Me.VideoTauntSliderLabel = New System.Windows.Forms.Label()
        Me.CBTauntCycleDD = New System.Windows.Forms.CheckBox()
        Me.TeaseLengthDommeDetermined = New System.Windows.Forms.CheckBox()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.NBTauntCycleMax = New System.Windows.Forms.NumericUpDown()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.NBTauntCycleMin = New System.Windows.Forms.NumericUpDown()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.NBTeaseLengthMax = New System.Windows.Forms.NumericUpDown()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.NBTeaseLengthMin = New System.Windows.Forms.NumericUpDown()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.RangeSettingsSessionTasksGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaskWaitMaximum = New System.Windows.Forms.NumericUpDown()
        Me.TaskWaitMinimum = New System.Windows.Forms.NumericUpDown()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.RangeSettingsHeaderPanel = New System.Windows.Forms.Panel()
        Me.RangeSettingsLogo = New System.Windows.Forms.PictureBox()
        Me.RangeSettingsHeaderLabel = New System.Windows.Forms.Label()
        Me.RangeSettingsDescriptionGroupBox = New System.Windows.Forms.GroupBox()
        Me.RangeSettingsDescriptionLabel = New System.Windows.Forms.Label()
        Me.TabPage13 = New System.Windows.Forms.TabPage()
        Me.ModSubTab = New System.Windows.Forms.TabControl()
        Me.ModPlaylistTabPage = New System.Windows.Forms.TabPage()
        Me.TBPlaylistSave = New System.Windows.Forms.TextBox()
        Me.BTNPlaylistCtrlZ = New System.Windows.Forms.Button()
        Me.RadioPlaylistRegScripts = New System.Windows.Forms.RadioButton()
        Me.RadioPlaylistScripts = New System.Windows.Forms.RadioButton()
        Me.BTNPlaylistEnd = New System.Windows.Forms.Button()
        Me.BTNPlaylistClearAll = New System.Windows.Forms.Button()
        Me.BTNPlaylistSave = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.ScriptPlayList = New System.Windows.Forms.WebBrowser()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.LBLPlaylIstLink = New System.Windows.Forms.Label()
        Me.LBLPlaylistModule = New System.Windows.Forms.Label()
        Me.LBLPLaylistStart = New System.Windows.Forms.Label()
        Me.LBPlaylist = New System.Windows.Forms.ListBox()
        Me.TabPage14 = New System.Windows.Forms.TabPage()
        Me.LBLKeywordPreview = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.TBKeywordPreview = New System.Windows.Forms.TextBox()
        Me.Button37 = New System.Windows.Forms.Button()
        Me.Button50 = New System.Windows.Forms.Button()
        Me.Button22 = New System.Windows.Forms.Button()
        Me.TBKeyWords = New System.Windows.Forms.TextBox()
        Me.LBKeyWords = New System.Windows.Forms.ListBox()
        Me.RTBKeyWords = New System.Windows.Forms.RichTextBox()
        Me.TabPage24 = New System.Windows.Forms.TabPage()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.RTBResponsesKEY = New System.Windows.Forms.RichTextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TBResponses = New System.Windows.Forms.TextBox()
        Me.LBResponses = New System.Windows.Forms.ListBox()
        Me.RTBResponses = New System.Windows.Forms.RichTextBox()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.RTBVideoMod = New System.Windows.Forms.RichTextBox()
        Me.GroupBox29 = New System.Windows.Forms.GroupBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.BTNVideoModClear = New System.Windows.Forms.Button()
        Me.GroupBox28 = New System.Windows.Forms.GroupBox()
        Me.CBVTType = New System.Windows.Forms.ComboBox()
        Me.BTNVideoModLoad = New System.Windows.Forms.Button()
        Me.GroupBox30 = New System.Windows.Forms.GroupBox()
        Me.LBVidScript = New System.Windows.Forms.ListBox()
        Me.BTNVideoModSave = New System.Windows.Forms.Button()
        Me.TabPage15 = New System.Windows.Forms.TabPage()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.TBGlitModFileName = New System.Windows.Forms.TextBox()
        Me.GroupBox34 = New System.Windows.Forms.GroupBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.RTBGlitModDommePost = New System.Windows.Forms.RichTextBox()
        Me.Button26 = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.RTBGlitModResponses = New System.Windows.Forms.RichTextBox()
        Me.LBGlitModScripts = New System.Windows.Forms.ListBox()
        Me.LBLGlitModScriptCount = New System.Windows.Forms.Label()
        Me.LBLGlitModDomType = New System.Windows.Forms.Label()
        Me.Button29 = New System.Windows.Forms.Button()
        Me.CBGlitModType = New System.Windows.Forms.ComboBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.TabPage25 = New System.Windows.Forms.TabPage()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.GroupBox62 = New System.Windows.Forms.GroupBox()
        Me.RBGerman = New System.Windows.Forms.RadioButton()
        Me.RBEnglish = New System.Windows.Forms.RadioButton()
        Me.GroupBox33 = New System.Windows.Forms.GroupBox()
        Me.BTNOfflineMode = New System.Windows.Forms.Button()
        Me.LBLOfflineMode = New System.Windows.Forms.Label()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.ChastityModeButton = New System.Windows.Forms.Button()
        Me.InChastityLabel = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.GroupBox27 = New System.Windows.Forms.GroupBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.LBLSesSpace = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.LBLSesFiles = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BTNMaintenanceScripts = New System.Windows.Forms.Button()
        Me.BTNMaintenanceRefresh = New System.Windows.Forms.Button()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.PBCurrent = New System.Windows.Forms.ProgressBar()
        Me.BTNMaintenanceCancel = New System.Windows.Forms.Button()
        Me.PBMaintenance = New System.Windows.Forms.ProgressBar()
        Me.LBLMaintenance = New System.Windows.Forms.Label()
        Me.BTNMaintenanceRebuild = New System.Windows.Forms.Button()
        Me.WebToy = New System.Windows.Forms.WebBrowser()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.TBWebStop = New System.Windows.Forms.TextBox()
        Me.TBWebStart = New System.Windows.Forms.TextBox()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.TabPage28 = New System.Windows.Forms.TabPage()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPage29 = New System.Windows.Forms.TabPage()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.LBLDebugScriptTime = New System.Windows.Forms.Label()
        Me.BTNDebugHoldEdgeTimer = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LBLAvgEdgeStroking = New System.Windows.Forms.Label()
        Me.LBLStrokeTimeTotal = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.LBLLastRuined = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.LBLAvgEdgeNoTouch = New System.Windows.Forms.Label()
        Me.LBLLastOrgasm = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox26 = New System.Windows.Forms.GroupBox()
        Me.LBLCycleDebugCountdown = New System.Windows.Forms.Label()
        Me.Button19 = New System.Windows.Forms.Button()
        Me.BTNDebugTauntsClear = New System.Windows.Forms.Button()
        Me.TBDebugTaunts3 = New System.Windows.Forms.TextBox()
        Me.TBDebugTaunts2 = New System.Windows.Forms.TextBox()
        Me.TBDebugTaunts1 = New System.Windows.Forms.TextBox()
        Me.RBDebugTaunts3 = New System.Windows.Forms.RadioButton()
        Me.RBDebugTaunts2 = New System.Windows.Forms.RadioButton()
        Me.RBDebugTaunts1 = New System.Windows.Forms.RadioButton()
        Me.CBDebugTauntsEndless = New System.Windows.Forms.CheckBox()
        Me.CBDebugTaunts = New System.Windows.Forms.CheckBox()
        Me.BTNDebugStrokeTauntTimer = New System.Windows.Forms.Button()
        Me.LBLDebugHoldEdgeTime = New System.Windows.Forms.Label()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.BTNDebugStrokeTime = New System.Windows.Forms.Button()
        Me.BTNDebugEdgeTauntTimer = New System.Windows.Forms.Button()
        Me.LBLDebugTeaseTime = New System.Windows.Forms.Label()
        Me.LBLDebugStrokeTime = New System.Windows.Forms.Label()
        Me.LBLDebugEdgeTauntTime = New System.Windows.Forms.Label()
        Me.BTNDebugTeaseTimer = New System.Windows.Forms.Button()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.LBLDebugStrokeTauntTime = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.TabPage30 = New System.Windows.Forms.TabPage()
        Me.Button33 = New System.Windows.Forms.Button()
        Me.Button24 = New System.Windows.Forms.Button()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GetColor = New System.Windows.Forms.ColorDialog()
        Me.WebImageFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenScriptDialog = New System.Windows.Forms.OpenFileDialog()
        Me.OpenSettingsDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveSettingsDialog = New System.Windows.Forms.SaveFileDialog()
        Me.TTDir = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxbImgUrlHardcore = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.SettingsHeader = New Tease_AI.SettingsHeaderControl()
        Me.SettingsDescriptionControl = New Tease_AI.SettingsDescriptionControl()
        Me.BWURLFiles = New Tease_AI.URL_Files.URL_File_BGW()
        Me.SettingsTabs.SuspendLayout
        Me.TabPage1.SuspendLayout
        Me.PNLGeneralSettings.SuspendLayout
        Me.GroupBox64.SuspendLayout
        Me.GBDommeImages.SuspendLayout
        CType(Me.SlideShowNumBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBGeneralTextToSpeech.SuspendLayout
        CType(Me.SliderVRate, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SliderVVolume, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBSafeword.SuspendLayout
        Me.GBGeneralSystem.SuspendLayout
        Me.GBGeneralImages.SuspendLayout
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBGeneralSettings.SuspendLayout
        Me.GBSubFont.SuspendLayout
        CType(Me.NBFontSize, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBDommeFont.SuspendLayout
        CType(Me.NBFontSizeD, System.ComponentModel.ISupportInitialize).BeginInit
        Me.DommeSettingsTabPage.SuspendLayout
        Me.DommeSettingsBodyPanel.SuspendLayout
        Me.GroupBox39.SuspendLayout
        Me.DommeStatsGroupBox.SuspendLayout
        CType(Me.NBEmpathy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBDomBirthdayDay, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DomAgeNumberBox, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBDomBirthdayMonth, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DominationLevel, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBDomPetNames.SuspendLayout
        Me.GBDomOrgasms.SuspendLayout
        CType(Me.OrgasmsPerNumBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBDomPersonality.SuspendLayout
        Me.GBDomRanges.SuspendLayout
        CType(Me.NBDomMoodMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBDomMoodMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBSubAgeMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBSubAgeMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBSelfAgeMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBSelfAgeMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBAvgCockMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBAvgCockMin, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBDomTypingStyle.SuspendLayout
        CType(Me.NBTypoChance, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox63.SuspendLayout
        Me.DommeSettingsHeaderPanel.SuspendLayout
        Me.Panel1.SuspendLayout
        CType(Me.DommeSettingsLogo, System.ComponentModel.ISupportInitialize).BeginInit
        Me.DommeSettingsDescriptionGroupBox.SuspendLayout
        Me.TabPage10.SuspendLayout
        Me.Panel2.SuspendLayout
        Me.GroupBox22.SuspendLayout
        CType(Me.NBWritingTaskMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBWritingTaskMin, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox45.SuspendLayout
        CType(Me.CockAndBallTortureLevelSlider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox35.SuspendLayout
        Me.GroupBox38.SuspendLayout
        Me.GroupBox37.SuspendLayout
        Me.GroupBox36.SuspendLayout
        Me.GroupBox13.SuspendLayout
        Me.GroupBox12.SuspendLayout
        Me.GroupBox7.SuspendLayout
        CType(Me.ExtremeEdgeHoldMinimum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ExtremeEdgeHoldMaximum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LongEdgeHoldMinimum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LongEdgeHoldMaximum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBLongEdge, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.HoldEdgeMinimum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.HoldEdgeMaximum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox32.SuspendLayout
        CType(Me.NBBirthdayDay, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.subAgeNumBox, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBBirthdayMonth, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CockSizeNumBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabPage16.SuspendLayout
        Me.Panel9.SuspendLayout
        Me.ScriptNavPanel.SuspendLayout
        Me.TCScripts.SuspendLayout
        Me.ScriptsStartTab.SuspendLayout
        Me.ScriptsModuleTab.SuspendLayout
        Me.ScriptsLinkTab.SuspendLayout
        Me.ScriptsEndTab.SuspendLayout
        Me.ScriptInfoPanel.SuspendLayout
        Me.ScriptsDescriptionGroup.SuspendLayout
        Me.ScriptsRequirementsGroup.SuspendLayout
        Me.GroupBox43.SuspendLayout
        Me.TabPage7.SuspendLayout
        Me.GernreImagesTab.SuspendLayout
        Me.TpImagesUrlFiles.SuspendLayout
        Me.GroupBox66.SuspendLayout
        CType(Me.PBURLPreview, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TpImagesGenre.SuspendLayout
        Me.GrbImageUrlFiles.SuspendLayout
        Me.TlpImageUrls.SuspendLayout
        Me.GbxImagesGenre.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.TabPage33.SuspendLayout
        Me.LocalTagsTab.SuspendLayout
        Me.TabPage34.SuspendLayout
        CType(Me.ImageTagPictureBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.FileDropDownLabel.SuspendLayout
        Me.LocalTagImageNavGroup.SuspendLayout
        Me.GroupBox55.SuspendLayout
        Me.GroupBox53.SuspendLayout
        Me.GroupBox49.SuspendLayout
        Me.GroupBox46.SuspendLayout
        Me.GroupBox54.SuspendLayout
        Me.BdsmTagGroup.SuspendLayout
        Me.GroupBox50.SuspendLayout
        Me.GroupBox48.SuspendLayout
        CType(Me.LocalTagPictureBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.UrlFilesTab.SuspendLayout
        Me.UrlFilesPanel.SuspendLayout
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.WebPictureBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TpVideoSettings.SuspendLayout
        Me.VideoSettingsPanel.SuspendLayout
        Me.VideoLayoutTable.SuspendLayout
        Me.VideoGeneralPanel.SuspendLayout
        Me.VideoGeneralGroupBox.SuspendLayout
        Me.VideoSpecialGroupBox.SuspendLayout
        Me.VideoGenreGroupBox.SuspendLayout
        Me.VideoDommePanel.SuspendLayout
        Me.VideoDommeGeneralGroupBox.SuspendLayout
        Me.GbxVideoSpecialD.SuspendLayout
        Me.GbxVideoGenreD.SuspendLayout
        Me.VideoHeaderPanel.SuspendLayout
        CType(Me.VideoLogo, System.ComponentModel.ISupportInitialize).BeginInit
        Me.VideoDescriptionGroupBox.SuspendLayout
        Me.AppsTabPage.SuspendLayout
        Me.AppsSettingsTabList.SuspendLayout
        Me.GlitterAppTabPage.SuspendLayout
        Me.GBGlitter1.SuspendLayout
        CType(Me.GlitterSlider1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GlitterAV1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBGlitter3.SuspendLayout
        CType(Me.GlitterSlider3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GlitterAV3, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBGlitter2.SuspendLayout
        CType(Me.GlitterSlider2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GlitterAV2, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TpGames.SuspendLayout
        Me.GbxCardsGold.SuspendLayout
        CType(Me.GP6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GP2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GP5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GP1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GP3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GP4, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GbxCardsBackground.SuspendLayout
        CType(Me.CardBack, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GbxCardsBronze.SuspendLayout
        CType(Me.BP3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BP6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BP5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BP2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BP4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BP1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GbxCardsSilver.SuspendLayout
        CType(Me.SP6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SP2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SP5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SP1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SP3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SP4, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabPage6.SuspendLayout
        Me.Panel10.SuspendLayout
        CType(Me.NBWishlistCost, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PNLWishList.SuspendLayout
        CType(Me.WishlistCostSilver, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.WishlistCostGold, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.WishlistPreview, System.ComponentModel.ISupportInitialize).BeginInit
        Me.AppsSettingsHeaderPanel.SuspendLayout
        CType(Me.AppsSettingsLogo, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabPage26.SuspendLayout
        Me.Panel12.SuspendLayout
        Me.GroupBox9.SuspendLayout
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox5.SuspendLayout
        Me.GroupBox11.SuspendLayout
        Me.GroupBox1.SuspendLayout
        CType(Me.PBBackgroundPreview, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsTabPage.SuspendLayout
        Me.RangeSettingsBody.SuspendLayout
        Me.RangeSettingsBodyTablePanel.SuspendLayout
        Me.RangeSettingsBodyRightColumnPanel.SuspendLayout
        Me.RangeSettingsTeaseSlideshowGroupBox.SuspendLayout
        CType(Me.NBNextImageChance, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsGlitterTasksGroupBox.SuspendLayout
        CType(Me.NBTaskCBTTimeMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskCBTTimeMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskEdgeHoldTimeMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskEdgeHoldTimeMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskEdgesMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskEdgesMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskStrokingTimeMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskStrokingTimeMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskStrokesMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTaskStrokesMin, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsVideoTeaseGroupBox.SuspendLayout
        Me.GroupBox19.SuspendLayout
        CType(Me.GreenLightMaximumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GreenLightMinimumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RedLightMaximumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RedLightMinimumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsCensorshipSucksGroupBox.SuspendLayout
        CType(Me.ShowCensorshipBarMinimumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.HideCensorshipBarMaximumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.HideCensorshipBarMinimumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ShowCensorshipBarMaximumSeconds, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsBodyMiddleColumnPanel.SuspendLayout
        Me.GBRangeOrgasmChance.SuspendLayout
        CType(Me.SometimesAllowsPercentNumberBox, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RarelyAllowsPercentNumberBox, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.OftenAllowsPercentNumberBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox69.SuspendLayout
        CType(Me.TypeSpeedSlider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GBRangeRuinChance.SuspendLayout
        CType(Me.NBRuinSometimes, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBRuinRarely, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBRuinOften, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsBodyLeftColumnPanel.SuspendLayout
        Me.RangeSettingsTeaseGroupBox.SuspendLayout
        CType(Me.NBTauntEdging, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SliderSTF, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.VideoTauntSlider, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTauntCycleMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTauntCycleMin, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTeaseLengthMax, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NBTeaseLengthMin, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsSessionTasksGroupBox.SuspendLayout
        CType(Me.TaskWaitMaximum, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TaskWaitMinimum, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsHeaderPanel.SuspendLayout
        CType(Me.RangeSettingsLogo, System.ComponentModel.ISupportInitialize).BeginInit
        Me.RangeSettingsDescriptionGroupBox.SuspendLayout
        Me.TabPage13.SuspendLayout
        Me.ModSubTab.SuspendLayout
        Me.ModPlaylistTabPage.SuspendLayout
        Me.TabPage14.SuspendLayout
        Me.TabPage24.SuspendLayout
        Me.TabPage8.SuspendLayout
        Me.GroupBox29.SuspendLayout
        Me.GroupBox28.SuspendLayout
        Me.GroupBox30.SuspendLayout
        Me.TabPage15.SuspendLayout
        Me.GroupBox34.SuspendLayout
        Me.TabPage25.SuspendLayout
        Me.Panel11.SuspendLayout
        Me.GroupBox62.SuspendLayout
        Me.GroupBox33.SuspendLayout
        Me.GroupBox27.SuspendLayout
        Me.GroupBox20.SuspendLayout
        Me.GroupBox15.SuspendLayout
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabPage28.SuspendLayout
        Me.TabControl3.SuspendLayout
        Me.TabPage29.SuspendLayout
        Me.GroupBox6.SuspendLayout
        Me.GroupBox26.SuspendLayout
        Me.TabPage30.SuspendLayout
        Me.TabPage5.SuspendLayout
        Me.Panel5.SuspendLayout
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'SettingsTabs
        '
        Me.SettingsTabs.Controls.Add(Me.TabPage1)
        Me.SettingsTabs.Controls.Add(Me.DommeSettingsTabPage)
        Me.SettingsTabs.Controls.Add(Me.TabPage10)
        Me.SettingsTabs.Controls.Add(Me.TabPage16)
        Me.SettingsTabs.Controls.Add(Me.TabPage7)
        Me.SettingsTabs.Controls.Add(Me.TabPage33)
        Me.SettingsTabs.Controls.Add(Me.UrlFilesTab)
        Me.SettingsTabs.Controls.Add(Me.TpVideoSettings)
        Me.SettingsTabs.Controls.Add(Me.AppsTabPage)
        Me.SettingsTabs.Controls.Add(Me.TabPage26)
        Me.SettingsTabs.Controls.Add(Me.RangeSettingsTabPage)
        Me.SettingsTabs.Controls.Add(Me.TabPage13)
        Me.SettingsTabs.Controls.Add(Me.TabPage25)
        Me.SettingsTabs.Controls.Add(Me.TabPage28)
        Me.SettingsTabs.Controls.Add(Me.TabPage5)
        Me.SettingsTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SettingsTabs.Location = New System.Drawing.Point(0, 60)
        Me.SettingsTabs.Name = "SettingsTabs"
        Me.SettingsTabs.SelectedIndex = 0
        Me.SettingsTabs.Size = New System.Drawing.Size(980, 482)
        Me.SettingsTabs.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Silver
        Me.TabPage1.Controls.Add(Me.PNLGeneralSettings)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(972, 456)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        '
        'PNLGeneralSettings
        '
        Me.PNLGeneralSettings.BackColor = System.Drawing.Color.LightGray
        Me.PNLGeneralSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PNLGeneralSettings.Controls.Add(Me.BtnImportSettings)
        Me.PNLGeneralSettings.Controls.Add(Me.LblImportSettings)
        Me.PNLGeneralSettings.Controls.Add(Me.GroupBox64)
        Me.PNLGeneralSettings.Controls.Add(Me.GBDommeImages)
        Me.PNLGeneralSettings.Controls.Add(Me.GBGeneralTextToSpeech)
        Me.PNLGeneralSettings.Controls.Add(Me.GBSafeword)
        Me.PNLGeneralSettings.Controls.Add(Me.GBGeneralSystem)
        Me.PNLGeneralSettings.Controls.Add(Me.GBGeneralImages)
        Me.PNLGeneralSettings.Controls.Add(Me.PictureBox2)
        Me.PNLGeneralSettings.Controls.Add(Me.GBGeneralSettings)
        Me.PNLGeneralSettings.Controls.Add(Me.LBLGeneralSettings)
        Me.PNLGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PNLGeneralSettings.Location = New System.Drawing.Point(3, 3)
        Me.PNLGeneralSettings.Name = "PNLGeneralSettings"
        Me.PNLGeneralSettings.Size = New System.Drawing.Size(966, 450)
        Me.PNLGeneralSettings.TabIndex = 1
        '
        'BtnImportSettings
        '
        Me.BtnImportSettings.BackColor = System.Drawing.Color.Transparent
        Me.BtnImportSettings.BackgroundImage = Global.Tease_AI.My.Resources.Resources.Button_Export
        Me.BtnImportSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnImportSettings.FlatAppearance.BorderSize = 0
        Me.BtnImportSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.BtnImportSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.BtnImportSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImportSettings.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImportSettings.ForeColor = System.Drawing.Color.Black
        Me.BtnImportSettings.Location = New System.Drawing.Point(669, 14)
        Me.BtnImportSettings.Name = "BtnImportSettings"
        Me.BtnImportSettings.Size = New System.Drawing.Size(30, 26)
        Me.BtnImportSettings.TabIndex = 158
        Me.BtnImportSettings.UseVisualStyleBackColor = False
        '
        'LblImportSettings
        '
        Me.LblImportSettings.AutoSize = True
        Me.LblImportSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblImportSettings.ForeColor = System.Drawing.Color.Black
        Me.LblImportSettings.Location = New System.Drawing.Point(664, 0)
        Me.LblImportSettings.Name = "LblImportSettings"
        Me.LblImportSettings.Size = New System.Drawing.Size(35, 13)
        Me.LblImportSettings.TabIndex = 159
        Me.LblImportSettings.Text = "import"
        Me.LblImportSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox64
        '
        Me.GroupBox64.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox64.Controls.Add(Me.CBMuteMedia)
        Me.GroupBox64.ForeColor = System.Drawing.Color.Black
        Me.GroupBox64.Location = New System.Drawing.Point(440, 258)
        Me.GroupBox64.Name = "GroupBox64"
        Me.GroupBox64.Size = New System.Drawing.Size(259, 49)
        Me.GroupBox64.TabIndex = 157
        Me.GroupBox64.TabStop = False
        Me.GroupBox64.Text = "Media Options"
        '
        'CBMuteMedia
        '
        Me.CBMuteMedia.AutoSize = True
        Me.CBMuteMedia.ForeColor = System.Drawing.Color.Black
        Me.CBMuteMedia.Location = New System.Drawing.Point(7, 21)
        Me.CBMuteMedia.Name = "CBMuteMedia"
        Me.CBMuteMedia.Size = New System.Drawing.Size(241, 17)
        Me.CBMuteMedia.TabIndex = 6
        Me.CBMuteMedia.TabStop = False
        Me.CBMuteMedia.Text = "Mute Video and Audio Played in Media Player"
        Me.CBMuteMedia.UseVisualStyleBackColor = True
        '
        'GBDommeImages
        '
        Me.GBDommeImages.BackColor = System.Drawing.Color.LightGray
        Me.GBDommeImages.Controls.Add(Me.SlideShowNumBox)
        Me.GBDommeImages.Controls.Add(Me.TeaseSlideShowRadio)
        Me.GBDommeImages.Controls.Add(Me.CBNewSlideshow)
        Me.GBDommeImages.Controls.Add(Me.ManualSlideShowRadio)
        Me.GBDommeImages.Controls.Add(Me.BTNDomImageDir)
        Me.GBDommeImages.Controls.Add(Me.TimedSlideShowRadio)
        Me.GBDommeImages.Controls.Add(Me.TbxDomImageDir)
        Me.GBDommeImages.ForeColor = System.Drawing.Color.Black
        Me.GBDommeImages.Location = New System.Drawing.Point(224, 179)
        Me.GBDommeImages.Name = "GBDommeImages"
        Me.GBDommeImages.Size = New System.Drawing.Size(210, 128)
        Me.GBDommeImages.TabIndex = 156
        Me.GBDommeImages.TabStop = False
        Me.GBDommeImages.Text = "Slideshow Options"
        '
        'SlideShowNumBox
        '
        Me.SlideShowNumBox.BackColor = System.Drawing.Color.White
        Me.SlideShowNumBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SlideShowNumBox.ForeColor = System.Drawing.Color.Black
        Me.SlideShowNumBox.Location = New System.Drawing.Point(93, 20)
        Me.SlideShowNumBox.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.SlideShowNumBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.SlideShowNumBox.Name = "SlideShowNumBox"
        Me.SlideShowNumBox.Size = New System.Drawing.Size(47, 20)
        Me.SlideShowNumBox.TabIndex = 20
        Me.SlideShowNumBox.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'TeaseSlideShowRadio
        '
        Me.TeaseSlideShowRadio.AutoSize = True
        Me.TeaseSlideShowRadio.Checked = True
        Me.TeaseSlideShowRadio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TeaseSlideShowRadio.ForeColor = System.Drawing.Color.Black
        Me.TeaseSlideShowRadio.Location = New System.Drawing.Point(149, 21)
        Me.TeaseSlideShowRadio.Name = "TeaseSlideShowRadio"
        Me.TeaseSlideShowRadio.Size = New System.Drawing.Size(55, 17)
        Me.TeaseSlideShowRadio.TabIndex = 21
        Me.TeaseSlideShowRadio.TabStop = True
        Me.TeaseSlideShowRadio.Text = "Tease"
        Me.TeaseSlideShowRadio.UseVisualStyleBackColor = True
        '
        'CBNewSlideshow
        '
        Me.CBNewSlideshow.AutoSize = True
        Me.CBNewSlideshow.Checked = True
        Me.CBNewSlideshow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBNewSlideshow.ForeColor = System.Drawing.Color.Black
        Me.CBNewSlideshow.Location = New System.Drawing.Point(6, 100)
        Me.CBNewSlideshow.Name = "CBNewSlideshow"
        Me.CBNewSlideshow.Size = New System.Drawing.Size(200, 17)
        Me.CBNewSlideshow.TabIndex = 18
        Me.CBNewSlideshow.TabStop = False
        Me.CBNewSlideshow.Text = "Load New Slideshow When Finished"
        Me.CBNewSlideshow.UseVisualStyleBackColor = True
        '
        'ManualSlideShowRadio
        '
        Me.ManualSlideShowRadio.AutoSize = True
        Me.ManualSlideShowRadio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ManualSlideShowRadio.ForeColor = System.Drawing.Color.Black
        Me.ManualSlideShowRadio.Location = New System.Drawing.Point(6, 21)
        Me.ManualSlideShowRadio.Name = "ManualSlideShowRadio"
        Me.ManualSlideShowRadio.Size = New System.Drawing.Size(60, 17)
        Me.ManualSlideShowRadio.TabIndex = 18
        Me.ManualSlideShowRadio.Text = "Manual"
        Me.ManualSlideShowRadio.UseVisualStyleBackColor = True
        '
        'BTNDomImageDir
        '
        Me.BTNDomImageDir.BackColor = System.Drawing.Color.LightGray
        Me.BTNDomImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNDomImageDir.ForeColor = System.Drawing.Color.Black
        Me.BTNDomImageDir.Location = New System.Drawing.Point(6, 45)
        Me.BTNDomImageDir.Name = "BTNDomImageDir"
        Me.BTNDomImageDir.Size = New System.Drawing.Size(198, 22)
        Me.BTNDomImageDir.TabIndex = 17
        Me.BTNDomImageDir.Text = "Set Domme Images Directory"
        Me.BTNDomImageDir.UseVisualStyleBackColor = False
        '
        'TimedSlideShowRadio
        '
        Me.TimedSlideShowRadio.AutoSize = True
        Me.TimedSlideShowRadio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimedSlideShowRadio.ForeColor = System.Drawing.Color.Black
        Me.TimedSlideShowRadio.Location = New System.Drawing.Point(72, 23)
        Me.TimedSlideShowRadio.Name = "TimedSlideShowRadio"
        Me.TimedSlideShowRadio.Size = New System.Drawing.Size(14, 13)
        Me.TimedSlideShowRadio.TabIndex = 19
        Me.TimedSlideShowRadio.UseVisualStyleBackColor = True
        '
        'TbxDomImageDir
        '
        Me.TbxDomImageDir.BackColor = System.Drawing.Color.LightGray
        Me.TbxDomImageDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbxDomImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbxDomImageDir.ForeColor = System.Drawing.Color.Black
        Me.TbxDomImageDir.Location = New System.Drawing.Point(6, 73)
        Me.TbxDomImageDir.Name = "TbxDomImageDir"
        Me.TbxDomImageDir.ReadOnly = True
        Me.TbxDomImageDir.Size = New System.Drawing.Size(198, 20)
        Me.TbxDomImageDir.TabIndex = 0
        '
        'GBGeneralTextToSpeech
        '
        Me.GBGeneralTextToSpeech.BackColor = System.Drawing.Color.LightGray
        Me.GBGeneralTextToSpeech.Controls.Add(Me.LBLVRate)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.Label93)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.LBLVVolume)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.Label68)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.SliderVRate)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.SliderVVolume)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.TTSCheckBox)
        Me.GBGeneralTextToSpeech.Controls.Add(Me.TTSComboBox)
        Me.GBGeneralTextToSpeech.ForeColor = System.Drawing.Color.Black
        Me.GBGeneralTextToSpeech.Location = New System.Drawing.Point(440, 313)
        Me.GBGeneralTextToSpeech.Name = "GBGeneralTextToSpeech"
        Me.GBGeneralTextToSpeech.Size = New System.Drawing.Size(259, 117)
        Me.GBGeneralTextToSpeech.TabIndex = 0
        Me.GBGeneralTextToSpeech.TabStop = False
        Me.GBGeneralTextToSpeech.Text = "Text to Speech"
        '
        'LBLVRate
        '
        Me.LBLVRate.Location = New System.Drawing.Point(202, 52)
        Me.LBLVRate.Name = "LBLVRate"
        Me.LBLVRate.Size = New System.Drawing.Size(45, 13)
        Me.LBLVRate.TabIndex = 158
        Me.LBLVRate.Text = "100"
        Me.LBLVRate.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(141, 52)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(33, 13)
        Me.Label93.TabIndex = 157
        Me.Label93.Text = "Rate:"
        '
        'LBLVVolume
        '
        Me.LBLVVolume.Location = New System.Drawing.Point(75, 52)
        Me.LBLVVolume.Name = "LBLVVolume"
        Me.LBLVVolume.Size = New System.Drawing.Size(45, 13)
        Me.LBLVVolume.TabIndex = 33
        Me.LBLVVolume.Text = "100"
        Me.LBLVVolume.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(14, 52)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(45, 13)
        Me.Label68.TabIndex = 32
        Me.Label68.Text = "Volume:"
        '
        'SliderVRate
        '
        Me.SliderVRate.Location = New System.Drawing.Point(133, 68)
        Me.SliderVRate.Minimum = -10
        Me.SliderVRate.Name = "SliderVRate"
        Me.SliderVRate.Size = New System.Drawing.Size(120, 45)
        Me.SliderVRate.TabIndex = 31
        '
        'SliderVVolume
        '
        Me.SliderVVolume.Location = New System.Drawing.Point(6, 68)
        Me.SliderVVolume.Maximum = 100
        Me.SliderVVolume.Name = "SliderVVolume"
        Me.SliderVVolume.Size = New System.Drawing.Size(120, 45)
        Me.SliderVVolume.TabIndex = 30
        Me.SliderVVolume.Value = 50
        '
        'TTSCheckBox
        '
        Me.TTSCheckBox.AutoSize = True
        Me.TTSCheckBox.ForeColor = System.Drawing.Color.Black
        Me.TTSCheckBox.Location = New System.Drawing.Point(10, 21)
        Me.TTSCheckBox.Name = "TTSCheckBox"
        Me.TTSCheckBox.Size = New System.Drawing.Size(59, 17)
        Me.TTSCheckBox.TabIndex = 28
        Me.TTSCheckBox.TabStop = False
        Me.TTSCheckBox.Text = "Enable"
        Me.TTSCheckBox.UseVisualStyleBackColor = True
        '
        'TTSComboBox
        '
        Me.TTSComboBox.BackColor = System.Drawing.SystemColors.Window
        Me.TTSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TTSComboBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TTSComboBox.FormattingEnabled = True
        Me.TTSComboBox.Location = New System.Drawing.Point(71, 19)
        Me.TTSComboBox.Name = "TTSComboBox"
        Me.TTSComboBox.Size = New System.Drawing.Size(178, 21)
        Me.TTSComboBox.TabIndex = 29
        Me.TTSComboBox.TabStop = False
        '
        'GBSafeword
        '
        Me.GBSafeword.BackColor = System.Drawing.Color.LightGray
        Me.GBSafeword.Controls.Add(Me.LBLSafeword)
        Me.GBSafeword.Controls.Add(Me.TBSafeword)
        Me.GBSafeword.ForeColor = System.Drawing.Color.Black
        Me.GBSafeword.Location = New System.Drawing.Point(440, 179)
        Me.GBSafeword.Name = "GBSafeword"
        Me.GBSafeword.Size = New System.Drawing.Size(259, 74)
        Me.GBSafeword.TabIndex = 0
        Me.GBSafeword.TabStop = False
        Me.GBSafeword.Text = "Safeword"
        '
        'LBLSafeword
        '
        Me.LBLSafeword.Location = New System.Drawing.Point(17, 42)
        Me.LBLSafeword.Name = "LBLSafeword"
        Me.LBLSafeword.Size = New System.Drawing.Size(225, 29)
        Me.LBLSafeword.TabIndex = 0
        Me.LBLSafeword.Text = "Enter a safeword that will stop all activity until the domme is sure you're able " &
    "to continue."
        Me.LBLSafeword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TBSafeword
        '
        Me.TBSafeword.Location = New System.Drawing.Point(17, 19)
        Me.TBSafeword.Name = "TBSafeword"
        Me.TBSafeword.Size = New System.Drawing.Size(225, 20)
        Me.TBSafeword.TabIndex = 27
        Me.TBSafeword.Text = "red"
        Me.TBSafeword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GBGeneralSystem
        '
        Me.GBGeneralSystem.Controls.Add(Me.CBAuditStartup)
        Me.GBGeneralSystem.Controls.Add(Me.CBDomDel)
        Me.GBGeneralSystem.Controls.Add(Me.CBSettingsPause)
        Me.GBGeneralSystem.Controls.Add(Me.CBSaveChatlogExit)
        Me.GBGeneralSystem.Controls.Add(Me.CBAutosaveChatlog)
        Me.GBGeneralSystem.Location = New System.Drawing.Point(440, 33)
        Me.GBGeneralSystem.Name = "GBGeneralSystem"
        Me.GBGeneralSystem.Size = New System.Drawing.Size(259, 140)
        Me.GBGeneralSystem.TabIndex = 0
        Me.GBGeneralSystem.TabStop = False
        Me.GBGeneralSystem.Text = "System"
        '
        'CBAuditStartup
        '
        Me.CBAuditStartup.AutoSize = True
        Me.CBAuditStartup.ForeColor = System.Drawing.Color.Black
        Me.CBAuditStartup.Location = New System.Drawing.Point(7, 19)
        Me.CBAuditStartup.Name = "CBAuditStartup"
        Me.CBAuditStartup.Size = New System.Drawing.Size(137, 17)
        Me.CBAuditStartup.TabIndex = 26
        Me.CBAuditStartup.TabStop = False
        Me.CBAuditStartup.Text = "Audit Scripts on Startup"
        Me.CBAuditStartup.UseVisualStyleBackColor = True
        '
        'CBDomDel
        '
        Me.CBDomDel.AutoSize = True
        Me.CBDomDel.ForeColor = System.Drawing.Color.Black
        Me.CBDomDel.Location = New System.Drawing.Point(7, 110)
        Me.CBDomDel.Name = "CBDomDel"
        Me.CBDomDel.Size = New System.Drawing.Size(197, 17)
        Me.CBDomDel.TabIndex = 27
        Me.CBDomDel.TabStop = False
        Me.CBDomDel.Text = "Allow Domme to Delete Local Media"
        Me.CBDomDel.UseVisualStyleBackColor = True
        '
        'CBSettingsPause
        '
        Me.CBSettingsPause.AutoSize = True
        Me.CBSettingsPause.ForeColor = System.Drawing.Color.Black
        Me.CBSettingsPause.Location = New System.Drawing.Point(7, 41)
        Me.CBSettingsPause.Name = "CBSettingsPause"
        Me.CBSettingsPause.Size = New System.Drawing.Size(244, 17)
        Me.CBSettingsPause.TabIndex = 22
        Me.CBSettingsPause.TabStop = False
        Me.CBSettingsPause.Text = "Pause Program When Settings Menu is Visible"
        Me.CBSettingsPause.UseVisualStyleBackColor = True
        '
        'CBSaveChatlogExit
        '
        Me.CBSaveChatlogExit.AutoSize = True
        Me.CBSaveChatlogExit.Checked = True
        Me.CBSaveChatlogExit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBSaveChatlogExit.ForeColor = System.Drawing.Color.Black
        Me.CBSaveChatlogExit.Location = New System.Drawing.Point(7, 87)
        Me.CBSaveChatlogExit.Name = "CBSaveChatlogExit"
        Me.CBSaveChatlogExit.Size = New System.Drawing.Size(162, 17)
        Me.CBSaveChatlogExit.TabIndex = 25
        Me.CBSaveChatlogExit.TabStop = False
        Me.CBSaveChatlogExit.Text = "Save Unique Chatlog on Exit"
        Me.CBSaveChatlogExit.UseVisualStyleBackColor = True
        '
        'CBAutosaveChatlog
        '
        Me.CBAutosaveChatlog.AutoSize = True
        Me.CBAutosaveChatlog.Checked = True
        Me.CBAutosaveChatlog.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBAutosaveChatlog.ForeColor = System.Drawing.Color.Black
        Me.CBAutosaveChatlog.Location = New System.Drawing.Point(7, 64)
        Me.CBAutosaveChatlog.Name = "CBAutosaveChatlog"
        Me.CBAutosaveChatlog.Size = New System.Drawing.Size(110, 17)
        Me.CBAutosaveChatlog.TabIndex = 24
        Me.CBAutosaveChatlog.TabStop = False
        Me.CBAutosaveChatlog.Text = "Autosave Chatlog"
        Me.CBAutosaveChatlog.UseVisualStyleBackColor = True
        '
        'GBGeneralImages
        '
        Me.GBGeneralImages.Controls.Add(Me.CBImageInfo)
        Me.GBGeneralImages.Controls.Add(Me.CBSlideshowRandom)
        Me.GBGeneralImages.Controls.Add(Me.LandscapeCheckBox)
        Me.GBGeneralImages.Controls.Add(Me.CBBlogImageWindow)
        Me.GBGeneralImages.Controls.Add(Me.CBSlideshowSubDir)
        Me.GBGeneralImages.Location = New System.Drawing.Point(224, 33)
        Me.GBGeneralImages.Name = "GBGeneralImages"
        Me.GBGeneralImages.Size = New System.Drawing.Size(210, 140)
        Me.GBGeneralImages.TabIndex = 0
        Me.GBGeneralImages.TabStop = False
        Me.GBGeneralImages.Text = "Images"
        '
        'CBImageInfo
        '
        Me.CBImageInfo.AutoSize = True
        Me.CBImageInfo.ForeColor = System.Drawing.Color.Black
        Me.CBImageInfo.Location = New System.Drawing.Point(6, 110)
        Me.CBImageInfo.Name = "CBImageInfo"
        Me.CBImageInfo.Size = New System.Drawing.Size(147, 17)
        Me.CBImageInfo.TabIndex = 16
        Me.CBImageInfo.TabStop = False
        Me.CBImageInfo.Text = "Display Image Information"
        Me.CBImageInfo.UseVisualStyleBackColor = True
        '
        'CBSlideshowRandom
        '
        Me.CBSlideshowRandom.AutoSize = True
        Me.CBSlideshowRandom.ForeColor = System.Drawing.Color.Black
        Me.CBSlideshowRandom.Location = New System.Drawing.Point(6, 64)
        Me.CBSlideshowRandom.Name = "CBSlideshowRandom"
        Me.CBSlideshowRandom.Size = New System.Drawing.Size(202, 17)
        Me.CBSlideshowRandom.TabIndex = 14
        Me.CBSlideshowRandom.TabStop = False
        Me.CBSlideshowRandom.Text = "Display Slideshow Pictures Randomly"
        Me.CBSlideshowRandom.UseVisualStyleBackColor = True
        '
        'LandscapeCheckBox
        '
        Me.LandscapeCheckBox.AutoSize = True
        Me.LandscapeCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LandscapeCheckBox.Location = New System.Drawing.Point(6, 87)
        Me.LandscapeCheckBox.Name = "LandscapeCheckBox"
        Me.LandscapeCheckBox.Size = New System.Drawing.Size(153, 17)
        Me.LandscapeCheckBox.TabIndex = 15
        Me.LandscapeCheckBox.TabStop = False
        Me.LandscapeCheckBox.Text = "Stretch Landscape Images"
        Me.LandscapeCheckBox.UseVisualStyleBackColor = True
        '
        'CBBlogImageWindow
        '
        Me.CBBlogImageWindow.AutoSize = True
        Me.CBBlogImageWindow.Checked = True
        Me.CBBlogImageWindow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBBlogImageWindow.ForeColor = System.Drawing.Color.Black
        Me.CBBlogImageWindow.Location = New System.Drawing.Point(6, 18)
        Me.CBBlogImageWindow.Name = "CBBlogImageWindow"
        Me.CBBlogImageWindow.Size = New System.Drawing.Size(178, 17)
        Me.CBBlogImageWindow.TabIndex = 12
        Me.CBBlogImageWindow.TabStop = False
        Me.CBBlogImageWindow.Text = "Save Blog Images From Session"
        Me.CBBlogImageWindow.UseVisualStyleBackColor = True
        '
        'CBSlideshowSubDir
        '
        Me.CBSlideshowSubDir.AutoSize = True
        Me.CBSlideshowSubDir.Checked = True
        Me.CBSlideshowSubDir.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBSlideshowSubDir.ForeColor = System.Drawing.Color.Black
        Me.CBSlideshowSubDir.Location = New System.Drawing.Point(6, 41)
        Me.CBSlideshowSubDir.Name = "CBSlideshowSubDir"
        Me.CBSlideshowSubDir.Size = New System.Drawing.Size(187, 17)
        Me.CBSlideshowSubDir.TabIndex = 13
        Me.CBSlideshowSubDir.TabStop = False
        Me.CBSlideshowSubDir.Text = "Slideshow Includes Subdirectories"
        Me.CBSlideshowSubDir.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox2.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.PictureBox2.Location = New System.Drawing.Point(9, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(160, 19)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 148
        Me.PictureBox2.TabStop = False
        '
        'GBGeneralSettings
        '
        Me.GBGeneralSettings.BackColor = System.Drawing.Color.LightGray
        Me.GBGeneralSettings.Controls.Add(Me.WebTeaseMode)
        Me.GBGeneralSettings.Controls.Add(Me.GBSubFont)
        Me.GBGeneralSettings.Controls.Add(Me.GBDommeFont)
        Me.GBGeneralSettings.Controls.Add(Me.CBInputIcon)
        Me.GBGeneralSettings.Controls.Add(Me.TypeInstantlyCheckBox)
        Me.GBGeneralSettings.Controls.Add(Me.TimeStampCheckBox)
        Me.GBGeneralSettings.Controls.Add(Me.ShowNamesCheckBox)
        Me.GBGeneralSettings.ForeColor = System.Drawing.Color.Black
        Me.GBGeneralSettings.Location = New System.Drawing.Point(7, 33)
        Me.GBGeneralSettings.Name = "GBGeneralSettings"
        Me.GBGeneralSettings.Size = New System.Drawing.Size(211, 326)
        Me.GBGeneralSettings.TabIndex = 0
        Me.GBGeneralSettings.TabStop = False
        Me.GBGeneralSettings.Text = "Chat Window"
        '
        'WebTeaseMode
        '
        Me.WebTeaseMode.AutoSize = True
        Me.WebTeaseMode.ForeColor = System.Drawing.Color.Black
        Me.WebTeaseMode.Location = New System.Drawing.Point(6, 110)
        Me.WebTeaseMode.Name = "WebTeaseMode"
        Me.WebTeaseMode.Size = New System.Drawing.Size(105, 17)
        Me.WebTeaseMode.TabIndex = 5
        Me.WebTeaseMode.TabStop = False
        Me.WebTeaseMode.Text = "Webtease Mode"
        Me.WebTeaseMode.UseVisualStyleBackColor = True
        '
        'GBSubFont
        '
        Me.GBSubFont.Controls.Add(Me.BTNSubColor)
        Me.GBSubFont.Controls.Add(Me.LBLSubColor)
        Me.GBSubFont.Controls.Add(Me.NBFontSize)
        Me.GBSubFont.Controls.Add(Me.Label2)
        Me.GBSubFont.Controls.Add(Me.SubMessageFontCB)
        Me.GBSubFont.Location = New System.Drawing.Point(6, 219)
        Me.GBSubFont.Name = "GBSubFont"
        Me.GBSubFont.Size = New System.Drawing.Size(200, 86)
        Me.GBSubFont.TabIndex = 0
        Me.GBSubFont.TabStop = False
        Me.GBSubFont.Text = "Sub Font Settings"
        '
        'BTNSubColor
        '
        Me.BTNSubColor.BackColor = System.Drawing.Color.LightGray
        Me.BTNSubColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNSubColor.ForeColor = System.Drawing.Color.Black
        Me.BTNSubColor.Location = New System.Drawing.Point(6, 19)
        Me.BTNSubColor.Name = "BTNSubColor"
        Me.BTNSubColor.Size = New System.Drawing.Size(110, 25)
        Me.BTNSubColor.TabIndex = 8
        Me.BTNSubColor.Text = "Sub Name Color"
        Me.BTNSubColor.UseVisualStyleBackColor = False
        '
        'LBLSubColor
        '
        Me.LBLSubColor.BackColor = System.Drawing.Color.White
        Me.LBLSubColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLSubColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubColor.Location = New System.Drawing.Point(120, 20)
        Me.LBLSubColor.Name = "LBLSubColor"
        Me.LBLSubColor.Size = New System.Drawing.Size(72, 23)
        Me.LBLSubColor.TabIndex = 0
        Me.LBLSubColor.Text = "Preview"
        Me.LBLSubColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NBFontSize
        '
        Me.NBFontSize.BackColor = System.Drawing.Color.White
        Me.NBFontSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBFontSize.ForeColor = System.Drawing.Color.Black
        Me.NBFontSize.Location = New System.Drawing.Point(147, 47)
        Me.NBFontSize.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBFontSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBFontSize.Name = "NBFontSize"
        Me.NBFontSize.Size = New System.Drawing.Size(45, 20)
        Me.NBFontSize.TabIndex = 11
        Me.NBFontSize.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(117, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 20)
        Me.Label2.TabIndex = 63
        Me.Label2.Text = "Size:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SubMessageFontCB
        '
        Me.SubMessageFontCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.SubMessageFontCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubMessageFontCB.FormattingEnabled = True
        Me.SubMessageFontCB.ItemHeight = 20
        Me.SubMessageFontCB.Location = New System.Drawing.Point(6, 46)
        Me.SubMessageFontCB.Name = "SubMessageFontCB"
        Me.SubMessageFontCB.Size = New System.Drawing.Size(110, 26)
        Me.SubMessageFontCB.TabIndex = 9
        '
        'GBDommeFont
        '
        Me.GBDommeFont.Controls.Add(Me.BTNDomColor)
        Me.GBDommeFont.Controls.Add(Me.LBLDomColor)
        Me.GBDommeFont.Controls.Add(Me.DommeMessageFontCB)
        Me.GBDommeFont.Controls.Add(Me.NBFontSizeD)
        Me.GBDommeFont.Controls.Add(Me.Label7)
        Me.GBDommeFont.Location = New System.Drawing.Point(6, 142)
        Me.GBDommeFont.Name = "GBDommeFont"
        Me.GBDommeFont.Size = New System.Drawing.Size(200, 77)
        Me.GBDommeFont.TabIndex = 0
        Me.GBDommeFont.TabStop = False
        Me.GBDommeFont.Text = "Domme Font Settings"
        '
        'BTNDomColor
        '
        Me.BTNDomColor.BackColor = System.Drawing.Color.LightGray
        Me.BTNDomColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNDomColor.ForeColor = System.Drawing.Color.Black
        Me.BTNDomColor.Location = New System.Drawing.Point(6, 19)
        Me.BTNDomColor.Name = "BTNDomColor"
        Me.BTNDomColor.Size = New System.Drawing.Size(110, 25)
        Me.BTNDomColor.TabIndex = 5
        Me.BTNDomColor.Text = "Domme Name Color"
        Me.BTNDomColor.UseVisualStyleBackColor = False
        '
        'LBLDomColor
        '
        Me.LBLDomColor.BackColor = System.Drawing.Color.White
        Me.LBLDomColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLDomColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLDomColor.Location = New System.Drawing.Point(120, 20)
        Me.LBLDomColor.Name = "LBLDomColor"
        Me.LBLDomColor.Size = New System.Drawing.Size(72, 23)
        Me.LBLDomColor.TabIndex = 0
        Me.LBLDomColor.Text = "Preview"
        Me.LBLDomColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DommeMessageFontCB
        '
        Me.DommeMessageFontCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.DommeMessageFontCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DommeMessageFontCB.FormattingEnabled = True
        Me.DommeMessageFontCB.ItemHeight = 20
        Me.DommeMessageFontCB.Location = New System.Drawing.Point(6, 46)
        Me.DommeMessageFontCB.Name = "DommeMessageFontCB"
        Me.DommeMessageFontCB.Size = New System.Drawing.Size(110, 26)
        Me.DommeMessageFontCB.TabIndex = 6
        '
        'NBFontSizeD
        '
        Me.NBFontSizeD.BackColor = System.Drawing.Color.White
        Me.NBFontSizeD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBFontSizeD.ForeColor = System.Drawing.Color.Black
        Me.NBFontSizeD.Location = New System.Drawing.Point(147, 47)
        Me.NBFontSizeD.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBFontSizeD.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBFontSizeD.Name = "NBFontSizeD"
        Me.NBFontSizeD.Size = New System.Drawing.Size(45, 20)
        Me.NBFontSizeD.TabIndex = 7
        Me.NBFontSizeD.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(117, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 20)
        Me.Label7.TabIndex = 172
        Me.Label7.Text = "Size:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBInputIcon
        '
        Me.CBInputIcon.AutoSize = True
        Me.CBInputIcon.Checked = True
        Me.CBInputIcon.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBInputIcon.ForeColor = System.Drawing.Color.Black
        Me.CBInputIcon.Location = New System.Drawing.Point(6, 87)
        Me.CBInputIcon.Name = "CBInputIcon"
        Me.CBInputIcon.Size = New System.Drawing.Size(188, 17)
        Me.CBInputIcon.TabIndex = 4
        Me.CBInputIcon.TabStop = False
        Me.CBInputIcon.Text = "Show Icon During Input Questions"
        Me.CBInputIcon.UseVisualStyleBackColor = True
        '
        'TypeInstantlyCheckBox
        '
        Me.TypeInstantlyCheckBox.AutoSize = True
        Me.TypeInstantlyCheckBox.ForeColor = System.Drawing.Color.Black
        Me.TypeInstantlyCheckBox.Location = New System.Drawing.Point(6, 64)
        Me.TypeInstantlyCheckBox.Name = "TypeInstantlyCheckBox"
        Me.TypeInstantlyCheckBox.Size = New System.Drawing.Size(136, 17)
        Me.TypeInstantlyCheckBox.TabIndex = 3
        Me.TypeInstantlyCheckBox.TabStop = False
        Me.TypeInstantlyCheckBox.Text = "Domme Types Instantly"
        Me.TypeInstantlyCheckBox.UseVisualStyleBackColor = True
        '
        'TimeStampCheckBox
        '
        Me.TimeStampCheckBox.AutoSize = True
        Me.TimeStampCheckBox.Checked = True
        Me.TimeStampCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TimeStampCheckBox.ForeColor = System.Drawing.Color.Black
        Me.TimeStampCheckBox.Location = New System.Drawing.Point(6, 18)
        Me.TimeStampCheckBox.Name = "TimeStampCheckBox"
        Me.TimeStampCheckBox.Size = New System.Drawing.Size(112, 17)
        Me.TimeStampCheckBox.TabIndex = 1
        Me.TimeStampCheckBox.TabStop = False
        Me.TimeStampCheckBox.Text = "Show Timestamps"
        Me.TimeStampCheckBox.UseVisualStyleBackColor = True
        '
        'ShowNamesCheckBox
        '
        Me.ShowNamesCheckBox.AutoSize = True
        Me.ShowNamesCheckBox.Checked = True
        Me.ShowNamesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowNamesCheckBox.ForeColor = System.Drawing.Color.Black
        Me.ShowNamesCheckBox.Location = New System.Drawing.Point(6, 41)
        Me.ShowNamesCheckBox.Name = "ShowNamesCheckBox"
        Me.ShowNamesCheckBox.Size = New System.Drawing.Size(125, 17)
        Me.ShowNamesCheckBox.TabIndex = 2
        Me.ShowNamesCheckBox.TabStop = False
        Me.ShowNamesCheckBox.Text = "Always Show Names"
        Me.ShowNamesCheckBox.UseVisualStyleBackColor = True
        '
        'LBLGeneralSettings
        '
        Me.LBLGeneralSettings.BackColor = System.Drawing.Color.Transparent
        Me.LBLGeneralSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGeneralSettings.ForeColor = System.Drawing.Color.Black
        Me.LBLGeneralSettings.Location = New System.Drawing.Point(7, 6)
        Me.LBLGeneralSettings.Name = "LBLGeneralSettings"
        Me.LBLGeneralSettings.Size = New System.Drawing.Size(692, 21)
        Me.LBLGeneralSettings.TabIndex = 0
        Me.LBLGeneralSettings.Text = "General Settings"
        Me.LBLGeneralSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DommeSettingsTabPage
        '
        Me.DommeSettingsTabPage.BackColor = System.Drawing.Color.Silver
        Me.DommeSettingsTabPage.Controls.Add(Me.DommeSettingsBodyPanel)
        Me.DommeSettingsTabPage.Controls.Add(Me.DommeSettingsHeaderPanel)
        Me.DommeSettingsTabPage.Controls.Add(Me.DommeSettingsDescriptionGroupBox)
        Me.DommeSettingsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DommeSettingsTabPage.Name = "DommeSettingsTabPage"
        Me.DommeSettingsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DommeSettingsTabPage.Size = New System.Drawing.Size(972, 456)
        Me.DommeSettingsTabPage.TabIndex = 1
        Me.DommeSettingsTabPage.Text = "Domme"
        '
        'DommeSettingsBodyPanel
        '
        Me.DommeSettingsBodyPanel.Controls.Add(Me.GroupBox39)
        Me.DommeSettingsBodyPanel.Controls.Add(Me.DommeStatsGroupBox)
        Me.DommeSettingsBodyPanel.Controls.Add(Me.GBDomPetNames)
        Me.DommeSettingsBodyPanel.Controls.Add(Me.GBDomOrgasms)
        Me.DommeSettingsBodyPanel.Controls.Add(Me.GBDomPersonality)
        Me.DommeSettingsBodyPanel.Controls.Add(Me.GBDomRanges)
        Me.DommeSettingsBodyPanel.Controls.Add(Me.GBDomTypingStyle)
        Me.DommeSettingsBodyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DommeSettingsBodyPanel.Location = New System.Drawing.Point(3, 63)
        Me.DommeSettingsBodyPanel.Name = "DommeSettingsBodyPanel"
        Me.DommeSettingsBodyPanel.Size = New System.Drawing.Size(966, 290)
        Me.DommeSettingsBodyPanel.TabIndex = 154
        '
        'GroupBox39
        '
        Me.GroupBox39.Controls.Add(Me.CBHonorificInclude)
        Me.GroupBox39.Controls.Add(Me.CBHonorificCapitalized)
        Me.GroupBox39.Controls.Add(Me.TBHonorific)
        Me.GroupBox39.Location = New System.Drawing.Point(489, 341)
        Me.GroupBox39.Name = "GroupBox39"
        Me.GroupBox39.Size = New System.Drawing.Size(247, 89)
        Me.GroupBox39.TabIndex = 149
        Me.GroupBox39.TabStop = False
        Me.GroupBox39.Tag = ""
        Me.GroupBox39.Text = "Honorific"
        '
        'CBHonorificInclude
        '
        Me.CBHonorificInclude.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBHonorificInclude.ForeColor = System.Drawing.Color.Black
        Me.CBHonorificInclude.Location = New System.Drawing.Point(9, 44)
        Me.CBHonorificInclude.Name = "CBHonorificInclude"
        Me.CBHonorificInclude.Size = New System.Drawing.Size(234, 21)
        Me.CBHonorificInclude.TabIndex = 40
        Me.CBHonorificInclude.Text = "Honorific Must Be Included w/ Key Phrases"
        Me.CBHonorificInclude.UseVisualStyleBackColor = True
        '
        'CBHonorificCapitalized
        '
        Me.CBHonorificCapitalized.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBHonorificCapitalized.ForeColor = System.Drawing.Color.Black
        Me.CBHonorificCapitalized.Location = New System.Drawing.Point(9, 66)
        Me.CBHonorificCapitalized.Name = "CBHonorificCapitalized"
        Me.CBHonorificCapitalized.Size = New System.Drawing.Size(179, 21)
        Me.CBHonorificCapitalized.TabIndex = 39
        Me.CBHonorificCapitalized.Text = "Honorific Must Be Capitalized"
        Me.CBHonorificCapitalized.UseVisualStyleBackColor = True
        '
        'TBHonorific
        '
        Me.TBHonorific.Location = New System.Drawing.Point(9, 16)
        Me.TBHonorific.Name = "TBHonorific"
        Me.TBHonorific.Size = New System.Drawing.Size(229, 20)
        Me.TBHonorific.TabIndex = 0
        Me.TBHonorific.Text = "Mistress"
        '
        'DommeStatsGroupBox
        '
        Me.DommeStatsGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.DommeStatsGroupBox.Controls.Add(Me.Label128)
        Me.DommeStatsGroupBox.Controls.Add(Me.LBLEmpathy)
        Me.DommeStatsGroupBox.Controls.Add(Me.NBEmpathy)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label83)
        Me.DommeStatsGroupBox.Controls.Add(Me.NBDomBirthdayDay)
        Me.DommeStatsGroupBox.Controls.Add(Me.TBDomEyeColor)
        Me.DommeStatsGroupBox.Controls.Add(Me.TBDomHairColor)
        Me.DommeStatsGroupBox.Controls.Add(Me.DomAgeNumberBox)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label47)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label76)
        Me.DommeStatsGroupBox.Controls.Add(Me.NBDomBirthdayMonth)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label84)
        Me.DommeStatsGroupBox.Controls.Add(Me.CBDomTattoos)
        Me.DommeStatsGroupBox.Controls.Add(Me.CBDomFreckles)
        Me.DommeStatsGroupBox.Controls.Add(Me.domhairlengthComboBox)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label10)
        Me.DommeStatsGroupBox.Controls.Add(Me.DommePubicHairComboBox)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label9)
        Me.DommeStatsGroupBox.Controls.Add(Me.boobComboBox)
        Me.DommeStatsGroupBox.Controls.Add(Me.DomLevelDescLabel)
        Me.DommeStatsGroupBox.Controls.Add(Me.DominationLevel)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label43)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label44)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label45)
        Me.DommeStatsGroupBox.Controls.Add(Me.Label46)
        Me.DommeStatsGroupBox.ForeColor = System.Drawing.Color.Black
        Me.DommeStatsGroupBox.Location = New System.Drawing.Point(56, 37)
        Me.DommeStatsGroupBox.Name = "DommeStatsGroupBox"
        Me.DommeStatsGroupBox.Size = New System.Drawing.Size(171, 263)
        Me.DommeStatsGroupBox.TabIndex = 62
        Me.DommeStatsGroupBox.TabStop = False
        Me.DommeStatsGroupBox.Text = "Stats/Appearance"
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label128.Location = New System.Drawing.Point(125, 68)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(38, 13)
        Me.Label128.TabIndex = 156
        Me.Label128.Text = "mm/dd"
        Me.Label128.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLEmpathy
        '
        Me.LBLEmpathy.Location = New System.Drawing.Point(113, 41)
        Me.LBLEmpathy.Name = "LBLEmpathy"
        Me.LBLEmpathy.Size = New System.Drawing.Size(55, 13)
        Me.LBLEmpathy.TabIndex = 157
        Me.LBLEmpathy.Text = "Moderate"
        Me.LBLEmpathy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NBEmpathy
        '
        Me.NBEmpathy.BackColor = System.Drawing.Color.White
        Me.NBEmpathy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBEmpathy.ForeColor = System.Drawing.Color.Black
        Me.NBEmpathy.Location = New System.Drawing.Point(73, 38)
        Me.NBEmpathy.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBEmpathy.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBEmpathy.Name = "NBEmpathy"
        Me.NBEmpathy.Size = New System.Drawing.Size(38, 20)
        Me.NBEmpathy.TabIndex = 156
        Me.NBEmpathy.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.Transparent
        Me.Label83.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.Black
        Me.Label83.Location = New System.Drawing.Point(6, 37)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(60, 17)
        Me.Label83.TabIndex = 158
        Me.Label83.Text = "Apathy:"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBDomBirthdayDay
        '
        Me.NBDomBirthdayDay.BackColor = System.Drawing.Color.White
        Me.NBDomBirthdayDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBDomBirthdayDay.ForeColor = System.Drawing.Color.Black
        Me.NBDomBirthdayDay.Location = New System.Drawing.Point(125, 83)
        Me.NBDomBirthdayDay.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.NBDomBirthdayDay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBDomBirthdayDay.Name = "NBDomBirthdayDay"
        Me.NBDomBirthdayDay.Size = New System.Drawing.Size(38, 20)
        Me.NBDomBirthdayDay.TabIndex = 152
        Me.NBDomBirthdayDay.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'TBDomEyeColor
        '
        Me.TBDomEyeColor.BackColor = System.Drawing.Color.White
        Me.TBDomEyeColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBDomEyeColor.ForeColor = System.Drawing.Color.Black
        Me.TBDomEyeColor.Location = New System.Drawing.Point(73, 155)
        Me.TBDomEyeColor.Name = "TBDomEyeColor"
        Me.TBDomEyeColor.Size = New System.Drawing.Size(89, 23)
        Me.TBDomEyeColor.TabIndex = 154
        Me.TBDomEyeColor.Text = "green"
        '
        'TBDomHairColor
        '
        Me.TBDomHairColor.BackColor = System.Drawing.Color.White
        Me.TBDomHairColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBDomHairColor.ForeColor = System.Drawing.Color.Black
        Me.TBDomHairColor.Location = New System.Drawing.Point(73, 105)
        Me.TBDomHairColor.Name = "TBDomHairColor"
        Me.TBDomHairColor.Size = New System.Drawing.Size(89, 23)
        Me.TBDomHairColor.TabIndex = 153
        Me.TBDomHairColor.Text = "brown"
        '
        'DomAgeNumberBox
        '
        Me.DomAgeNumberBox.BackColor = System.Drawing.Color.White
        Me.DomAgeNumberBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DomAgeNumberBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DomAgeNumberBox.ForeColor = System.Drawing.Color.Black
        Me.DomAgeNumberBox.Location = New System.Drawing.Point(73, 61)
        Me.DomAgeNumberBox.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.DomAgeNumberBox.Minimum = New Decimal(New Integer() {18, 0, 0, 0})
        Me.DomAgeNumberBox.Name = "DomAgeNumberBox"
        Me.DomAgeNumberBox.Size = New System.Drawing.Size(38, 20)
        Me.DomAgeNumberBox.TabIndex = 27
        Me.DomAgeNumberBox.Value = New Decimal(New Integer() {21, 0, 0, 0})
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(6, 60)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(63, 17)
        Me.Label47.TabIndex = 138
        Me.Label47.Text = "Age:"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.Color.Black
        Me.Label76.Location = New System.Drawing.Point(113, 87)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(12, 13)
        Me.Label76.TabIndex = 151
        Me.Label76.Text = "/"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NBDomBirthdayMonth
        '
        Me.NBDomBirthdayMonth.BackColor = System.Drawing.Color.White
        Me.NBDomBirthdayMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBDomBirthdayMonth.ForeColor = System.Drawing.Color.Black
        Me.NBDomBirthdayMonth.Location = New System.Drawing.Point(73, 83)
        Me.NBDomBirthdayMonth.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.NBDomBirthdayMonth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBDomBirthdayMonth.Name = "NBDomBirthdayMonth"
        Me.NBDomBirthdayMonth.Size = New System.Drawing.Size(38, 20)
        Me.NBDomBirthdayMonth.TabIndex = 149
        Me.NBDomBirthdayMonth.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.Transparent
        Me.Label84.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.ForeColor = System.Drawing.Color.Black
        Me.Label84.Location = New System.Drawing.Point(6, 84)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(60, 17)
        Me.Label84.TabIndex = 150
        Me.Label84.Text = "Birthday:"
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBDomTattoos
        '
        Me.CBDomTattoos.AutoSize = True
        Me.CBDomTattoos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDomTattoos.ForeColor = System.Drawing.Color.Black
        Me.CBDomTattoos.Location = New System.Drawing.Point(13, 237)
        Me.CBDomTattoos.Name = "CBDomTattoos"
        Me.CBDomTattoos.Size = New System.Drawing.Size(62, 17)
        Me.CBDomTattoos.TabIndex = 148
        Me.CBDomTattoos.Text = "Tattoos"
        Me.CBDomTattoos.UseVisualStyleBackColor = True
        '
        'CBDomFreckles
        '
        Me.CBDomFreckles.AutoSize = True
        Me.CBDomFreckles.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDomFreckles.ForeColor = System.Drawing.Color.Black
        Me.CBDomFreckles.Location = New System.Drawing.Point(88, 237)
        Me.CBDomFreckles.Name = "CBDomFreckles"
        Me.CBDomFreckles.Size = New System.Drawing.Size(66, 17)
        Me.CBDomFreckles.TabIndex = 147
        Me.CBDomFreckles.Text = "Freckles"
        Me.CBDomFreckles.UseVisualStyleBackColor = True
        '
        'domhairlengthComboBox
        '
        Me.domhairlengthComboBox.BackColor = System.Drawing.Color.White
        Me.domhairlengthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.domhairlengthComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.domhairlengthComboBox.ForeColor = System.Drawing.Color.Black
        Me.domhairlengthComboBox.FormattingEnabled = True
        Me.domhairlengthComboBox.Items.AddRange(New Object() {"Shaved", "Buzz cut", "Short", "Medium", "Long", "Very Long"})
        Me.domhairlengthComboBox.Location = New System.Drawing.Point(73, 132)
        Me.domhairlengthComboBox.Name = "domhairlengthComboBox"
        Me.domhairlengthComboBox.Size = New System.Drawing.Size(89, 21)
        Me.domhairlengthComboBox.TabIndex = 145
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(6, 133)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 17)
        Me.Label10.TabIndex = 146
        Me.Label10.Text = "Hair Length:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DommePubicHairComboBox
        '
        Me.DommePubicHairComboBox.BackColor = System.Drawing.Color.White
        Me.DommePubicHairComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DommePubicHairComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DommePubicHairComboBox.ForeColor = System.Drawing.Color.Black
        Me.DommePubicHairComboBox.FormattingEnabled = True
        Me.DommePubicHairComboBox.Items.AddRange(New Object() {"Shaved", "Sparse", "Trimmed", "Natural", "Hairy"})
        Me.DommePubicHairComboBox.Location = New System.Drawing.Point(73, 208)
        Me.DommePubicHairComboBox.Name = "DommePubicHairComboBox"
        Me.DommePubicHairComboBox.Size = New System.Drawing.Size(89, 21)
        Me.DommePubicHairComboBox.TabIndex = 143
        Me.TTDir.SetToolTip(Me.DommePubicHairComboBox, "Sets description of the Domme's pubic hair.")
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(6, 209)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 17)
        Me.Label9.TabIndex = 144
        Me.Label9.Text = "Pubic Hair:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'boobComboBox
        '
        Me.boobComboBox.BackColor = System.Drawing.Color.White
        Me.boobComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.boobComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boobComboBox.ForeColor = System.Drawing.Color.Black
        Me.boobComboBox.FormattingEnabled = True
        Me.boobComboBox.Items.AddRange(New Object() {"A", "B", "C", "D", "DD", "DDD+"})
        Me.boobComboBox.Location = New System.Drawing.Point(73, 182)
        Me.boobComboBox.Name = "boobComboBox"
        Me.boobComboBox.Size = New System.Drawing.Size(89, 21)
        Me.boobComboBox.TabIndex = 2
        '
        'DomLevelDescLabel
        '
        Me.DomLevelDescLabel.Location = New System.Drawing.Point(112, 18)
        Me.DomLevelDescLabel.Name = "DomLevelDescLabel"
        Me.DomLevelDescLabel.Size = New System.Drawing.Size(55, 13)
        Me.DomLevelDescLabel.TabIndex = 42
        Me.DomLevelDescLabel.Text = "Tease"
        Me.DomLevelDescLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DominationLevel
        '
        Me.DominationLevel.BackColor = System.Drawing.Color.White
        Me.DominationLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DominationLevel.ForeColor = System.Drawing.Color.Black
        Me.DominationLevel.Location = New System.Drawing.Point(73, 15)
        Me.DominationLevel.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.DominationLevel.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.DominationLevel.Name = "DominationLevel"
        Me.DominationLevel.Size = New System.Drawing.Size(38, 20)
        Me.DominationLevel.TabIndex = 41
        Me.DominationLevel.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(6, 183)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(63, 17)
        Me.Label43.TabIndex = 142
        Me.Label43.Text = "Cup Size:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(6, 158)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(63, 17)
        Me.Label44.TabIndex = 141
        Me.Label44.Text = "Eye Color:"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.Black
        Me.Label45.Location = New System.Drawing.Point(6, 108)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(78, 17)
        Me.Label45.TabIndex = 140
        Me.Label45.Text = "Hair Color:"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Black
        Me.Label46.Location = New System.Drawing.Point(6, 15)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(46, 17)
        Me.Label46.TabIndex = 139
        Me.Label46.Text = "Level:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GBDomPetNames
        '
        Me.GBDomPetNames.BackColor = System.Drawing.Color.Transparent
        Me.GBDomPetNames.Controls.Add(Me.Label74)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox7)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox8)
        Me.GBDomPetNames.Controls.Add(Me.PetNameBox1)
        Me.GBDomPetNames.Controls.Add(Me.Label15)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox4)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox6)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox2)
        Me.GBDomPetNames.Controls.Add(Me.Label11)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox5)
        Me.GBDomPetNames.Controls.Add(Me.petnameBox3)
        Me.GBDomPetNames.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBDomPetNames.ForeColor = System.Drawing.Color.Black
        Me.GBDomPetNames.Location = New System.Drawing.Point(233, 110)
        Me.GBDomPetNames.Name = "GBDomPetNames"
        Me.GBDomPetNames.Size = New System.Drawing.Size(250, 190)
        Me.GBDomPetNames.TabIndex = 134
        Me.GBDomPetNames.TabStop = False
        Me.GBDomPetNames.Text = "Pet Names"
        '
        'Label74
        '
        Me.Label74.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.Color.Black
        Me.Label74.Location = New System.Drawing.Point(8, 14)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(233, 13)
        Me.Label74.TabIndex = 45
        Me.Label74.Text = "Great Mood"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'petnameBox7
        '
        Me.petnameBox7.BackColor = System.Drawing.Color.White
        Me.petnameBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox7.ForeColor = System.Drawing.Color.Black
        Me.petnameBox7.Location = New System.Drawing.Point(8, 154)
        Me.petnameBox7.Name = "petnameBox7"
        Me.petnameBox7.Size = New System.Drawing.Size(114, 23)
        Me.petnameBox7.TabIndex = 13
        Me.petnameBox7.Text = "bitch boy"
        Me.petnameBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'petnameBox8
        '
        Me.petnameBox8.BackColor = System.Drawing.Color.White
        Me.petnameBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox8.ForeColor = System.Drawing.Color.Black
        Me.petnameBox8.Location = New System.Drawing.Point(128, 154)
        Me.petnameBox8.Name = "petnameBox8"
        Me.petnameBox8.Size = New System.Drawing.Size(113, 22)
        Me.petnameBox8.TabIndex = 14
        Me.petnameBox8.Text = "slut"
        Me.petnameBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PetNameBox1
        '
        Me.PetNameBox1.BackColor = System.Drawing.Color.White
        Me.PetNameBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PetNameBox1.ForeColor = System.Drawing.Color.Black
        Me.PetNameBox1.Location = New System.Drawing.Point(8, 32)
        Me.PetNameBox1.Name = "PetNameBox1"
        Me.PetNameBox1.Size = New System.Drawing.Size(114, 23)
        Me.PetNameBox1.TabIndex = 7
        Me.PetNameBox1.Text = "stroker"
        Me.PetNameBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(8, 136)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(233, 13)
        Me.Label15.TabIndex = 44
        Me.Label15.Text = "Bad Mood"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'petnameBox4
        '
        Me.petnameBox4.BackColor = System.Drawing.Color.White
        Me.petnameBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox4.ForeColor = System.Drawing.Color.Black
        Me.petnameBox4.Location = New System.Drawing.Point(128, 81)
        Me.petnameBox4.Name = "petnameBox4"
        Me.petnameBox4.Size = New System.Drawing.Size(113, 23)
        Me.petnameBox4.TabIndex = 10
        Me.petnameBox4.Text = "loser"
        Me.petnameBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'petnameBox6
        '
        Me.petnameBox6.BackColor = System.Drawing.Color.White
        Me.petnameBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox6.ForeColor = System.Drawing.Color.Black
        Me.petnameBox6.Location = New System.Drawing.Point(128, 107)
        Me.petnameBox6.Name = "petnameBox6"
        Me.petnameBox6.Size = New System.Drawing.Size(113, 23)
        Me.petnameBox6.TabIndex = 12
        Me.petnameBox6.Text = "pet"
        Me.petnameBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'petnameBox2
        '
        Me.petnameBox2.BackColor = System.Drawing.Color.White
        Me.petnameBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox2.ForeColor = System.Drawing.Color.Black
        Me.petnameBox2.Location = New System.Drawing.Point(128, 32)
        Me.petnameBox2.Name = "petnameBox2"
        Me.petnameBox2.Size = New System.Drawing.Size(114, 23)
        Me.petnameBox2.TabIndex = 8
        Me.petnameBox2.Text = "wanker"
        Me.petnameBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(5, 63)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(239, 13)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Neutral Mood"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'petnameBox5
        '
        Me.petnameBox5.BackColor = System.Drawing.Color.White
        Me.petnameBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox5.ForeColor = System.Drawing.Color.Black
        Me.petnameBox5.Location = New System.Drawing.Point(8, 107)
        Me.petnameBox5.Name = "petnameBox5"
        Me.petnameBox5.Size = New System.Drawing.Size(114, 23)
        Me.petnameBox5.TabIndex = 11
        Me.petnameBox5.Text = "baby"
        Me.petnameBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'petnameBox3
        '
        Me.petnameBox3.BackColor = System.Drawing.Color.White
        Me.petnameBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.petnameBox3.ForeColor = System.Drawing.Color.Black
        Me.petnameBox3.Location = New System.Drawing.Point(8, 81)
        Me.petnameBox3.Name = "petnameBox3"
        Me.petnameBox3.Size = New System.Drawing.Size(114, 23)
        Me.petnameBox3.TabIndex = 9
        Me.petnameBox3.Text = "slave"
        Me.petnameBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GBDomOrgasms
        '
        Me.GBDomOrgasms.Controls.Add(Me.CBLockOrgasmChances)
        Me.GBDomOrgasms.Controls.Add(Me.orgasmlockrandombutton)
        Me.GBDomOrgasms.Controls.Add(Me.CBDomOrgasmEnds)
        Me.GBDomOrgasms.Controls.Add(Me.Label16)
        Me.GBDomOrgasms.Controls.Add(Me.Label12)
        Me.GBDomOrgasms.Controls.Add(Me.orgasmsperlockButton)
        Me.GBDomOrgasms.Controls.Add(Me.OrgasmsPerComboBox)
        Me.GBDomOrgasms.Controls.Add(Me.orgasmsperLabel)
        Me.GBDomOrgasms.Controls.Add(Me.limitcheckbox)
        Me.GBDomOrgasms.Controls.Add(Me.OrgasmsPerNumBox)
        Me.GBDomOrgasms.Controls.Add(Me.CBDomDenialEnds)
        Me.GBDomOrgasms.Controls.Add(Me.AllowsOrgasmComboBox)
        Me.GBDomOrgasms.Controls.Add(Me.RuinsOrgasmsComboBox)
        Me.GBDomOrgasms.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBDomOrgasms.ForeColor = System.Drawing.Color.Black
        Me.GBDomOrgasms.Location = New System.Drawing.Point(489, 37)
        Me.GBDomOrgasms.Name = "GBDomOrgasms"
        Me.GBDomOrgasms.Size = New System.Drawing.Size(259, 194)
        Me.GBDomOrgasms.TabIndex = 132
        Me.GBDomOrgasms.TabStop = False
        Me.GBDomOrgasms.Text = "Orgasms"
        '
        'CBLockOrgasmChances
        '
        Me.CBLockOrgasmChances.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLockOrgasmChances.ForeColor = System.Drawing.Color.Black
        Me.CBLockOrgasmChances.Location = New System.Drawing.Point(15, 73)
        Me.CBLockOrgasmChances.Name = "CBLockOrgasmChances"
        Me.CBLockOrgasmChances.Size = New System.Drawing.Size(237, 24)
        Me.CBLockOrgasmChances.TabIndex = 146
        Me.CBLockOrgasmChances.Text = "Orgasm Chance Locked when Tease Starts"
        Me.CBLockOrgasmChances.UseVisualStyleBackColor = True
        '
        'orgasmlockrandombutton
        '
        Me.orgasmlockrandombutton.BackColor = System.Drawing.Color.LightGray
        Me.orgasmlockrandombutton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orgasmlockrandombutton.ForeColor = System.Drawing.Color.Black
        Me.orgasmlockrandombutton.Location = New System.Drawing.Point(134, 161)
        Me.orgasmlockrandombutton.Name = "orgasmlockrandombutton"
        Me.orgasmlockrandombutton.Size = New System.Drawing.Size(110, 21)
        Me.orgasmlockrandombutton.TabIndex = 145
        Me.orgasmlockrandombutton.Text = "Lock Random"
        Me.orgasmlockrandombutton.UseVisualStyleBackColor = False
        '
        'CBDomOrgasmEnds
        '
        Me.CBDomOrgasmEnds.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDomOrgasmEnds.ForeColor = System.Drawing.Color.Black
        Me.CBDomOrgasmEnds.Location = New System.Drawing.Point(145, 95)
        Me.CBDomOrgasmEnds.Name = "CBDomOrgasmEnds"
        Me.CBDomOrgasmEnds.Size = New System.Drawing.Size(104, 37)
        Me.CBDomOrgasmEnds.TabIndex = 144
        Me.CBDomOrgasmEnds.Text = "Orgasm Always Ends Tease"
        Me.CBDomOrgasmEnds.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(12, 47)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(87, 17)
        Me.Label16.TabIndex = 142
        Me.Label16.Text = "Ruins Orgasms:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(12, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(87, 17)
        Me.Label12.TabIndex = 141
        Me.Label12.Text = "Allows Orgasms:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'orgasmsperlockButton
        '
        Me.orgasmsperlockButton.BackColor = System.Drawing.Color.LightGray
        Me.orgasmsperlockButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orgasmsperlockButton.ForeColor = System.Drawing.Color.Black
        Me.orgasmsperlockButton.Location = New System.Drawing.Point(15, 161)
        Me.orgasmsperlockButton.Name = "orgasmsperlockButton"
        Me.orgasmsperlockButton.Size = New System.Drawing.Size(110, 21)
        Me.orgasmsperlockButton.TabIndex = 97
        Me.orgasmsperlockButton.Text = "Lock Selected"
        Me.orgasmsperlockButton.UseVisualStyleBackColor = False
        '
        'OrgasmsPerComboBox
        '
        Me.OrgasmsPerComboBox.BackColor = System.Drawing.Color.White
        Me.OrgasmsPerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OrgasmsPerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OrgasmsPerComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrgasmsPerComboBox.ForeColor = System.Drawing.Color.Black
        Me.OrgasmsPerComboBox.FormattingEnabled = True
        Me.OrgasmsPerComboBox.Items.AddRange(New Object() {"Week", "2 Weeks", "Month", "2 Months", "3 Months", "6 Months", "9 Months", "Year", "2 Years", "3 Years", "5 Years", "10 Years", "25 Years", "Lifetime"})
        Me.OrgasmsPerComboBox.Location = New System.Drawing.Point(143, 133)
        Me.OrgasmsPerComboBox.Name = "OrgasmsPerComboBox"
        Me.OrgasmsPerComboBox.Size = New System.Drawing.Size(101, 21)
        Me.OrgasmsPerComboBox.TabIndex = 43
        '
        'orgasmsperLabel
        '
        Me.orgasmsperLabel.AutoSize = True
        Me.orgasmsperLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orgasmsperLabel.ForeColor = System.Drawing.Color.Black
        Me.orgasmsperLabel.Location = New System.Drawing.Point(115, 137)
        Me.orgasmsperLabel.Name = "orgasmsperLabel"
        Me.orgasmsperLabel.Size = New System.Drawing.Size(22, 13)
        Me.orgasmsperLabel.TabIndex = 42
        Me.orgasmsperLabel.Text = "per"
        Me.orgasmsperLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'limitcheckbox
        '
        Me.limitcheckbox.AutoSize = True
        Me.limitcheckbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.limitcheckbox.ForeColor = System.Drawing.Color.Black
        Me.limitcheckbox.Location = New System.Drawing.Point(15, 135)
        Me.limitcheckbox.Name = "limitcheckbox"
        Me.limitcheckbox.Size = New System.Drawing.Size(47, 17)
        Me.limitcheckbox.TabIndex = 39
        Me.limitcheckbox.Text = "Limit"
        Me.limitcheckbox.UseVisualStyleBackColor = True
        '
        'OrgasmsPerNumBox
        '
        Me.OrgasmsPerNumBox.BackColor = System.Drawing.Color.White
        Me.OrgasmsPerNumBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrgasmsPerNumBox.ForeColor = System.Drawing.Color.Black
        Me.OrgasmsPerNumBox.Location = New System.Drawing.Point(68, 134)
        Me.OrgasmsPerNumBox.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.OrgasmsPerNumBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.OrgasmsPerNumBox.Name = "OrgasmsPerNumBox"
        Me.OrgasmsPerNumBox.Size = New System.Drawing.Size(41, 20)
        Me.OrgasmsPerNumBox.TabIndex = 41
        Me.OrgasmsPerNumBox.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'CBDomDenialEnds
        '
        Me.CBDomDenialEnds.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDomDenialEnds.ForeColor = System.Drawing.Color.Black
        Me.CBDomDenialEnds.Location = New System.Drawing.Point(15, 95)
        Me.CBDomDenialEnds.Name = "CBDomDenialEnds"
        Me.CBDomDenialEnds.Size = New System.Drawing.Size(94, 37)
        Me.CBDomDenialEnds.TabIndex = 38
        Me.CBDomDenialEnds.Text = "Denial Always Ends Tease"
        Me.CBDomDenialEnds.UseVisualStyleBackColor = True
        '
        'AllowsOrgasmComboBox
        '
        Me.AllowsOrgasmComboBox.BackColor = System.Drawing.Color.White
        Me.AllowsOrgasmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AllowsOrgasmComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AllowsOrgasmComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AllowsOrgasmComboBox.ForeColor = System.Drawing.Color.Black
        Me.AllowsOrgasmComboBox.FormattingEnabled = True
        Me.AllowsOrgasmComboBox.Items.AddRange(New Object() {"Never Allows", "Rarely Allows", "Sometimes Allows", "Often Allows", "Always Allows"})
        Me.AllowsOrgasmComboBox.Location = New System.Drawing.Point(98, 18)
        Me.AllowsOrgasmComboBox.Name = "AllowsOrgasmComboBox"
        Me.AllowsOrgasmComboBox.Size = New System.Drawing.Size(146, 21)
        Me.AllowsOrgasmComboBox.TabIndex = 1
        '
        'RuinsOrgasmsComboBox
        '
        Me.RuinsOrgasmsComboBox.BackColor = System.Drawing.Color.White
        Me.RuinsOrgasmsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RuinsOrgasmsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RuinsOrgasmsComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RuinsOrgasmsComboBox.ForeColor = System.Drawing.Color.Black
        Me.RuinsOrgasmsComboBox.FormattingEnabled = True
        Me.RuinsOrgasmsComboBox.Items.AddRange(New Object() {"Never Ruins", "Rarely Ruins", "Sometimes Ruins", "Often Ruins", "Always Ruins"})
        Me.RuinsOrgasmsComboBox.Location = New System.Drawing.Point(98, 46)
        Me.RuinsOrgasmsComboBox.Name = "RuinsOrgasmsComboBox"
        Me.RuinsOrgasmsComboBox.Size = New System.Drawing.Size(146, 21)
        Me.RuinsOrgasmsComboBox.TabIndex = 2
        '
        'GBDomPersonality
        '
        Me.GBDomPersonality.Controls.Add(Me.degradingCheckBox)
        Me.GBDomPersonality.Controls.Add(Me.sadisticCheckBox)
        Me.GBDomPersonality.Controls.Add(Me.supremacistCheckBox)
        Me.GBDomPersonality.Controls.Add(Me.vulgarCheckBox)
        Me.GBDomPersonality.Controls.Add(Me.crazyCheckBox)
        Me.GBDomPersonality.Controls.Add(Me.CondescendingCheckBox)
        Me.GBDomPersonality.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBDomPersonality.ForeColor = System.Drawing.Color.Black
        Me.GBDomPersonality.Location = New System.Drawing.Point(233, 37)
        Me.GBDomPersonality.Name = "GBDomPersonality"
        Me.GBDomPersonality.Size = New System.Drawing.Size(250, 67)
        Me.GBDomPersonality.TabIndex = 131
        Me.GBDomPersonality.TabStop = False
        Me.GBDomPersonality.Text = "Personality"
        '
        'degradingCheckBox
        '
        Me.degradingCheckBox.AutoSize = True
        Me.degradingCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.degradingCheckBox.ForeColor = System.Drawing.Color.Black
        Me.degradingCheckBox.Location = New System.Drawing.Point(73, 43)
        Me.degradingCheckBox.Name = "degradingCheckBox"
        Me.degradingCheckBox.Size = New System.Drawing.Size(75, 17)
        Me.degradingCheckBox.TabIndex = 40
        Me.degradingCheckBox.Text = "Degrading"
        Me.degradingCheckBox.UseVisualStyleBackColor = True
        '
        'sadisticCheckBox
        '
        Me.sadisticCheckBox.AutoSize = True
        Me.sadisticCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sadisticCheckBox.ForeColor = System.Drawing.Color.Black
        Me.sadisticCheckBox.Location = New System.Drawing.Point(11, 43)
        Me.sadisticCheckBox.Name = "sadisticCheckBox"
        Me.sadisticCheckBox.Size = New System.Drawing.Size(63, 17)
        Me.sadisticCheckBox.TabIndex = 39
        Me.sadisticCheckBox.Text = "Sadistic"
        Me.sadisticCheckBox.UseVisualStyleBackColor = True
        '
        'supremacistCheckBox
        '
        Me.supremacistCheckBox.AutoSize = True
        Me.supremacistCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.supremacistCheckBox.ForeColor = System.Drawing.Color.Black
        Me.supremacistCheckBox.Location = New System.Drawing.Point(148, 20)
        Me.supremacistCheckBox.Name = "supremacistCheckBox"
        Me.supremacistCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.supremacistCheckBox.TabIndex = 38
        Me.supremacistCheckBox.Text = "Supremacist"
        Me.supremacistCheckBox.UseVisualStyleBackColor = True
        '
        'vulgarCheckBox
        '
        Me.vulgarCheckBox.AutoSize = True
        Me.vulgarCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vulgarCheckBox.ForeColor = System.Drawing.Color.Black
        Me.vulgarCheckBox.Location = New System.Drawing.Point(73, 20)
        Me.vulgarCheckBox.Name = "vulgarCheckBox"
        Me.vulgarCheckBox.Size = New System.Drawing.Size(56, 17)
        Me.vulgarCheckBox.TabIndex = 37
        Me.vulgarCheckBox.Text = "Vulgar"
        Me.vulgarCheckBox.UseVisualStyleBackColor = True
        '
        'crazyCheckBox
        '
        Me.crazyCheckBox.AutoSize = True
        Me.crazyCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.crazyCheckBox.ForeColor = System.Drawing.Color.Black
        Me.crazyCheckBox.Location = New System.Drawing.Point(11, 20)
        Me.crazyCheckBox.Name = "crazyCheckBox"
        Me.crazyCheckBox.Size = New System.Drawing.Size(52, 17)
        Me.crazyCheckBox.TabIndex = 36
        Me.crazyCheckBox.Text = "Crazy"
        Me.crazyCheckBox.UseVisualStyleBackColor = True
        '
        'CondescendingCheckBox
        '
        Me.CondescendingCheckBox.AutoSize = True
        Me.CondescendingCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CondescendingCheckBox.ForeColor = System.Drawing.Color.Black
        Me.CondescendingCheckBox.Location = New System.Drawing.Point(148, 43)
        Me.CondescendingCheckBox.Name = "CondescendingCheckBox"
        Me.CondescendingCheckBox.Size = New System.Drawing.Size(100, 17)
        Me.CondescendingCheckBox.TabIndex = 41
        Me.CondescendingCheckBox.Text = "Condescending"
        Me.CondescendingCheckBox.UseVisualStyleBackColor = True
        '
        'GBDomRanges
        '
        Me.GBDomRanges.Controls.Add(Me.NBDomMoodMax)
        Me.GBDomRanges.Controls.Add(Me.NBDomMoodMin)
        Me.GBDomRanges.Controls.Add(Me.Label37)
        Me.GBDomRanges.Controls.Add(Me.Label39)
        Me.GBDomRanges.Controls.Add(Me.NBSubAgeMax)
        Me.GBDomRanges.Controls.Add(Me.NBSubAgeMin)
        Me.GBDomRanges.Controls.Add(Me.Label31)
        Me.GBDomRanges.Controls.Add(Me.Label36)
        Me.GBDomRanges.Controls.Add(Me.NBSelfAgeMax)
        Me.GBDomRanges.Controls.Add(Me.NBSelfAgeMin)
        Me.GBDomRanges.Controls.Add(Me.Label21)
        Me.GBDomRanges.Controls.Add(Me.Label22)
        Me.GBDomRanges.Controls.Add(Me.NBAvgCockMax)
        Me.GBDomRanges.Controls.Add(Me.NBAvgCockMin)
        Me.GBDomRanges.Controls.Add(Me.Label23)
        Me.GBDomRanges.Controls.Add(Me.Label30)
        Me.GBDomRanges.Location = New System.Drawing.Point(489, 237)
        Me.GBDomRanges.Name = "GBDomRanges"
        Me.GBDomRanges.Size = New System.Drawing.Size(259, 94)
        Me.GBDomRanges.TabIndex = 148
        Me.GBDomRanges.TabStop = False
        Me.GBDomRanges.Text = "Ranges"
        '
        'NBDomMoodMax
        '
        Me.NBDomMoodMax.Location = New System.Drawing.Point(200, 11)
        Me.NBDomMoodMax.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NBDomMoodMax.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NBDomMoodMax.Name = "NBDomMoodMax"
        Me.NBDomMoodMax.Size = New System.Drawing.Size(44, 20)
        Me.NBDomMoodMax.TabIndex = 168
        Me.NBDomMoodMax.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'NBDomMoodMin
        '
        Me.NBDomMoodMin.Location = New System.Drawing.Point(134, 11)
        Me.NBDomMoodMin.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NBDomMoodMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBDomMoodMin.Name = "NBDomMoodMin"
        Me.NBDomMoodMin.Size = New System.Drawing.Size(44, 20)
        Me.NBDomMoodMin.TabIndex = 167
        Me.NBDomMoodMin.Value = New Decimal(New Integer() {6, 0, 0, 0})
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(184, 11)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(10, 17)
        Me.Label37.TabIndex = 166
        Me.Label37.Text = "-"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(12, 11)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(116, 17)
        Me.Label39.TabIndex = 165
        Me.Label39.Text = "Domme Mood Index:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBSubAgeMax
        '
        Me.NBSubAgeMax.Location = New System.Drawing.Point(200, 68)
        Me.NBSubAgeMax.Maximum = New Decimal(New Integer() {98, 0, 0, 0})
        Me.NBSubAgeMax.Minimum = New Decimal(New Integer() {29, 0, 0, 0})
        Me.NBSubAgeMax.Name = "NBSubAgeMax"
        Me.NBSubAgeMax.Size = New System.Drawing.Size(44, 20)
        Me.NBSubAgeMax.TabIndex = 164
        Me.NBSubAgeMax.Value = New Decimal(New Integer() {49, 0, 0, 0})
        '
        'NBSubAgeMin
        '
        Me.NBSubAgeMin.Location = New System.Drawing.Point(134, 68)
        Me.NBSubAgeMin.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NBSubAgeMin.Minimum = New Decimal(New Integer() {19, 0, 0, 0})
        Me.NBSubAgeMin.Name = "NBSubAgeMin"
        Me.NBSubAgeMin.Size = New System.Drawing.Size(44, 20)
        Me.NBSubAgeMin.TabIndex = 163
        Me.NBSubAgeMin.Value = New Decimal(New Integer() {28, 0, 0, 0})
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(184, 68)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(10, 17)
        Me.Label31.TabIndex = 162
        Me.Label31.Text = "-"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(12, 68)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(113, 17)
        Me.Label36.TabIndex = 161
        Me.Label36.Text = "Sub Age Perception:"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBSelfAgeMax
        '
        Me.NBSelfAgeMax.Location = New System.Drawing.Point(200, 49)
        Me.NBSelfAgeMax.Maximum = New Decimal(New Integer() {98, 0, 0, 0})
        Me.NBSelfAgeMax.Minimum = New Decimal(New Integer() {29, 0, 0, 0})
        Me.NBSelfAgeMax.Name = "NBSelfAgeMax"
        Me.NBSelfAgeMax.Size = New System.Drawing.Size(44, 20)
        Me.NBSelfAgeMax.TabIndex = 156
        Me.NBSelfAgeMax.Value = New Decimal(New Integer() {49, 0, 0, 0})
        '
        'NBSelfAgeMin
        '
        Me.NBSelfAgeMin.Location = New System.Drawing.Point(134, 49)
        Me.NBSelfAgeMin.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NBSelfAgeMin.Minimum = New Decimal(New Integer() {19, 0, 0, 0})
        Me.NBSelfAgeMin.Name = "NBSelfAgeMin"
        Me.NBSelfAgeMin.Size = New System.Drawing.Size(44, 20)
        Me.NBSelfAgeMin.TabIndex = 155
        Me.NBSelfAgeMin.Value = New Decimal(New Integer() {28, 0, 0, 0})
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(184, 49)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(10, 17)
        Me.Label21.TabIndex = 154
        Me.Label21.Text = "-"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(12, 49)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(116, 17)
        Me.Label22.TabIndex = 153
        Me.Label22.Text = "Self Age Perception:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBAvgCockMax
        '
        Me.NBAvgCockMax.Location = New System.Drawing.Point(200, 30)
        Me.NBAvgCockMax.Maximum = New Decimal(New Integer() {14, 0, 0, 0})
        Me.NBAvgCockMax.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NBAvgCockMax.Name = "NBAvgCockMax"
        Me.NBAvgCockMax.Size = New System.Drawing.Size(44, 20)
        Me.NBAvgCockMax.TabIndex = 152
        Me.NBAvgCockMax.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'NBAvgCockMin
        '
        Me.NBAvgCockMin.Location = New System.Drawing.Point(134, 30)
        Me.NBAvgCockMin.Maximum = New Decimal(New Integer() {13, 0, 0, 0})
        Me.NBAvgCockMin.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NBAvgCockMin.Name = "NBAvgCockMin"
        Me.NBAvgCockMin.Size = New System.Drawing.Size(44, 20)
        Me.NBAvgCockMin.TabIndex = 151
        Me.NBAvgCockMin.Value = New Decimal(New Integer() {6, 0, 0, 0})
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(184, 30)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(10, 17)
        Me.Label23.TabIndex = 150
        Me.Label23.Text = "-"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(12, 30)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(116, 17)
        Me.Label30.TabIndex = 149
        Me.Label30.Text = "Average Dick Size:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GBDomTypingStyle
        '
        Me.GBDomTypingStyle.Controls.Add(Me.TBEmoteEnd)
        Me.GBDomTypingStyle.Controls.Add(Me.Label67)
        Me.GBDomTypingStyle.Controls.Add(Me.TBEmote)
        Me.GBDomTypingStyle.Controls.Add(Me.NBTypoChance)
        Me.GBDomTypingStyle.Controls.Add(Me.Label66)
        Me.GBDomTypingStyle.Controls.Add(Me.CBMeMyMine)
        Me.GBDomTypingStyle.Controls.Add(Me.GroupBox63)
        Me.GBDomTypingStyle.Controls.Add(Me.Label64)
        Me.GBDomTypingStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBDomTypingStyle.ForeColor = System.Drawing.Color.Black
        Me.GBDomTypingStyle.Location = New System.Drawing.Point(56, 306)
        Me.GBDomTypingStyle.Name = "GBDomTypingStyle"
        Me.GBDomTypingStyle.Size = New System.Drawing.Size(427, 124)
        Me.GBDomTypingStyle.TabIndex = 138
        Me.GBDomTypingStyle.TabStop = False
        Me.GBDomTypingStyle.Text = "Typing Style"
        '
        'TBEmoteEnd
        '
        Me.TBEmoteEnd.BackColor = System.Drawing.Color.White
        Me.TBEmoteEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBEmoteEnd.ForeColor = System.Drawing.Color.Black
        Me.TBEmoteEnd.Location = New System.Drawing.Point(115, 91)
        Me.TBEmoteEnd.Name = "TBEmoteEnd"
        Me.TBEmoteEnd.Size = New System.Drawing.Size(84, 23)
        Me.TBEmoteEnd.TabIndex = 155
        Me.TBEmoteEnd.Text = "*"
        Me.TBEmoteEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.Color.Black
        Me.Label67.Location = New System.Drawing.Point(237, 77)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(42, 13)
        Me.Label67.TabIndex = 169
        Me.Label67.Text = "Typo %"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TBEmote
        '
        Me.TBEmote.BackColor = System.Drawing.Color.White
        Me.TBEmote.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBEmote.ForeColor = System.Drawing.Color.Black
        Me.TBEmote.Location = New System.Drawing.Point(9, 91)
        Me.TBEmote.Name = "TBEmote"
        Me.TBEmote.Size = New System.Drawing.Size(85, 23)
        Me.TBEmote.TabIndex = 154
        Me.TBEmote.Text = "*"
        Me.TBEmote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NBTypoChance
        '
        Me.NBTypoChance.Location = New System.Drawing.Point(238, 94)
        Me.NBTypoChance.Name = "NBTypoChance"
        Me.NBTypoChance.Size = New System.Drawing.Size(44, 20)
        Me.NBTypoChance.TabIndex = 168
        Me.NBTypoChance.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.Black
        Me.Label66.Location = New System.Drawing.Point(322, 77)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(52, 13)
        Me.Label66.TabIndex = 44
        Me.Label66.Text = "Pronouns"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CBMeMyMine
        '
        Me.CBMeMyMine.AutoSize = True
        Me.CBMeMyMine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBMeMyMine.ForeColor = System.Drawing.Color.Black
        Me.CBMeMyMine.Location = New System.Drawing.Point(325, 97)
        Me.CBMeMyMine.Name = "CBMeMyMine"
        Me.CBMeMyMine.Size = New System.Drawing.Size(88, 17)
        Me.CBMeMyMine.TabIndex = 40
        Me.CBMeMyMine.Text = "Me/My/Mine"
        Me.CBMeMyMine.UseVisualStyleBackColor = True
        '
        'GroupBox63
        '
        Me.GroupBox63.Controls.Add(Me.LCaseCheckBox)
        Me.GroupBox63.Controls.Add(Me.apostropheCheckBox)
        Me.GroupBox63.Controls.Add(Me.periodCheckBox)
        Me.GroupBox63.Controls.Add(Me.commaCheckBox)
        Me.GroupBox63.Location = New System.Drawing.Point(9, 15)
        Me.GroupBox63.Name = "GroupBox63"
        Me.GroupBox63.Size = New System.Drawing.Size(407, 48)
        Me.GroupBox63.TabIndex = 41
        Me.GroupBox63.TabStop = False
        Me.GroupBox63.Text = "Remove"
        '
        'LCaseCheckBox
        '
        Me.LCaseCheckBox.AutoSize = True
        Me.LCaseCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LCaseCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LCaseCheckBox.Location = New System.Drawing.Point(16, 19)
        Me.LCaseCheckBox.Name = "LCaseCheckBox"
        Me.LCaseCheckBox.Size = New System.Drawing.Size(88, 17)
        Me.LCaseCheckBox.TabIndex = 38
        Me.LCaseCheckBox.Text = "Capitalization"
        Me.LCaseCheckBox.UseVisualStyleBackColor = True
        '
        'apostropheCheckBox
        '
        Me.apostropheCheckBox.AutoSize = True
        Me.apostropheCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.apostropheCheckBox.ForeColor = System.Drawing.Color.Black
        Me.apostropheCheckBox.Location = New System.Drawing.Point(116, 19)
        Me.apostropheCheckBox.Name = "apostropheCheckBox"
        Me.apostropheCheckBox.Size = New System.Drawing.Size(85, 17)
        Me.apostropheCheckBox.TabIndex = 39
        Me.apostropheCheckBox.Text = "Apostrophes"
        Me.apostropheCheckBox.UseVisualStyleBackColor = True
        '
        'periodCheckBox
        '
        Me.periodCheckBox.AutoSize = True
        Me.periodCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.periodCheckBox.ForeColor = System.Drawing.Color.Black
        Me.periodCheckBox.Location = New System.Drawing.Point(316, 19)
        Me.periodCheckBox.Name = "periodCheckBox"
        Me.periodCheckBox.Size = New System.Drawing.Size(61, 17)
        Me.periodCheckBox.TabIndex = 37
        Me.periodCheckBox.Text = "Periods"
        Me.periodCheckBox.UseVisualStyleBackColor = True
        '
        'commaCheckBox
        '
        Me.commaCheckBox.AutoSize = True
        Me.commaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commaCheckBox.ForeColor = System.Drawing.Color.Black
        Me.commaCheckBox.Location = New System.Drawing.Point(216, 19)
        Me.commaCheckBox.Name = "commaCheckBox"
        Me.commaCheckBox.Size = New System.Drawing.Size(66, 17)
        Me.commaCheckBox.TabIndex = 36
        Me.commaCheckBox.Text = "Commas"
        Me.commaCheckBox.UseVisualStyleBackColor = True
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.Color.Black
        Me.Label64.Location = New System.Drawing.Point(8, 77)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(79, 13)
        Me.Label64.TabIndex = 43
        Me.Label64.Text = "Emote Symbols"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DommeSettingsHeaderPanel
        '
        Me.DommeSettingsHeaderPanel.Controls.Add(Me.Panel1)
        Me.DommeSettingsHeaderPanel.Controls.Add(Me.DommeSettingsLogo)
        Me.DommeSettingsHeaderPanel.Controls.Add(Me.DommeSettingsHeaderLabel)
        Me.DommeSettingsHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.DommeSettingsHeaderPanel.Location = New System.Drawing.Point(3, 3)
        Me.DommeSettingsHeaderPanel.Name = "DommeSettingsHeaderPanel"
        Me.DommeSettingsHeaderPanel.Size = New System.Drawing.Size(966, 60)
        Me.DommeSettingsHeaderPanel.TabIndex = 155
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.BTNSaveDomSet)
        Me.Panel1.Controls.Add(Me.DommeSettingsSaveButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(844, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(122, 60)
        Me.Panel1.TabIndex = 152
        '
        'BTNSaveDomSet
        '
        Me.BTNSaveDomSet.BackColor = System.Drawing.Color.Transparent
        Me.BTNSaveDomSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTNSaveDomSet.Dock = System.Windows.Forms.DockStyle.Right
        Me.BTNSaveDomSet.FlatAppearance.BorderSize = 0
        Me.BTNSaveDomSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.BTNSaveDomSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.BTNSaveDomSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTNSaveDomSet.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNSaveDomSet.ForeColor = System.Drawing.Color.Black
        Me.BTNSaveDomSet.Image = Global.Tease_AI.My.Resources.Resources.Button_Save
        Me.BTNSaveDomSet.Location = New System.Drawing.Point(-2, 0)
        Me.BTNSaveDomSet.Margin = New System.Windows.Forms.Padding(0)
        Me.BTNSaveDomSet.Name = "BTNSaveDomSet"
        Me.BTNSaveDomSet.Size = New System.Drawing.Size(62, 60)
        Me.BTNSaveDomSet.TabIndex = 151
        Me.BTNSaveDomSet.Text = "Load"
        Me.BTNSaveDomSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BTNSaveDomSet.UseVisualStyleBackColor = False
        '
        'DommeSettingsSaveButton
        '
        Me.DommeSettingsSaveButton.BackColor = System.Drawing.Color.Transparent
        Me.DommeSettingsSaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DommeSettingsSaveButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.DommeSettingsSaveButton.FlatAppearance.BorderSize = 0
        Me.DommeSettingsSaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.DommeSettingsSaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.DommeSettingsSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DommeSettingsSaveButton.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DommeSettingsSaveButton.ForeColor = System.Drawing.Color.Black
        Me.DommeSettingsSaveButton.Image = Global.Tease_AI.My.Resources.Resources.Button_Export
        Me.DommeSettingsSaveButton.Location = New System.Drawing.Point(60, 0)
        Me.DommeSettingsSaveButton.Margin = New System.Windows.Forms.Padding(0)
        Me.DommeSettingsSaveButton.Name = "DommeSettingsSaveButton"
        Me.DommeSettingsSaveButton.Size = New System.Drawing.Size(62, 60)
        Me.DommeSettingsSaveButton.TabIndex = 150
        Me.DommeSettingsSaveButton.Text = "Save"
        Me.DommeSettingsSaveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.DommeSettingsSaveButton.UseVisualStyleBackColor = False
        '
        'DommeSettingsLogo
        '
        Me.DommeSettingsLogo.BackColor = System.Drawing.Color.Transparent
        Me.DommeSettingsLogo.Dock = System.Windows.Forms.DockStyle.Left
        Me.DommeSettingsLogo.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.DommeSettingsLogo.Location = New System.Drawing.Point(0, 0)
        Me.DommeSettingsLogo.Name = "DommeSettingsLogo"
        Me.DommeSettingsLogo.Size = New System.Drawing.Size(160, 60)
        Me.DommeSettingsLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.DommeSettingsLogo.TabIndex = 149
        Me.DommeSettingsLogo.TabStop = False
        '
        'DommeSettingsHeaderLabel
        '
        Me.DommeSettingsHeaderLabel.BackColor = System.Drawing.Color.Transparent
        Me.DommeSettingsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DommeSettingsHeaderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DommeSettingsHeaderLabel.ForeColor = System.Drawing.Color.Black
        Me.DommeSettingsHeaderLabel.Location = New System.Drawing.Point(0, 0)
        Me.DommeSettingsHeaderLabel.Name = "DommeSettingsHeaderLabel"
        Me.DommeSettingsHeaderLabel.Size = New System.Drawing.Size(966, 60)
        Me.DommeSettingsHeaderLabel.TabIndex = 49
        Me.DommeSettingsHeaderLabel.Text = "Domme Settings"
        Me.DommeSettingsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DommeSettingsDescriptionGroupBox
        '
        Me.DommeSettingsDescriptionGroupBox.Controls.Add(Me.DommeSettingsDescriptionLabel)
        Me.DommeSettingsDescriptionGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DommeSettingsDescriptionGroupBox.Location = New System.Drawing.Point(3, 353)
        Me.DommeSettingsDescriptionGroupBox.Name = "DommeSettingsDescriptionGroupBox"
        Me.DommeSettingsDescriptionGroupBox.Size = New System.Drawing.Size(966, 100)
        Me.DommeSettingsDescriptionGroupBox.TabIndex = 156
        Me.DommeSettingsDescriptionGroupBox.TabStop = False
        Me.DommeSettingsDescriptionGroupBox.Text = "Description"
        '
        'DommeSettingsDescriptionLabel
        '
        Me.DommeSettingsDescriptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.DommeSettingsDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DommeSettingsDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DommeSettingsDescriptionLabel.ForeColor = System.Drawing.Color.Black
        Me.DommeSettingsDescriptionLabel.Location = New System.Drawing.Point(3, 16)
        Me.DommeSettingsDescriptionLabel.Name = "DommeSettingsDescriptionLabel"
        Me.DommeSettingsDescriptionLabel.Size = New System.Drawing.Size(960, 81)
        Me.DommeSettingsDescriptionLabel.TabIndex = 63
        Me.DommeSettingsDescriptionLabel.Text = "Hover over any setting in the menu for a more detailed description of its functio" &
    "n."
        Me.DommeSettingsDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage10
        '
        Me.TabPage10.BackColor = System.Drawing.Color.Silver
        Me.TabPage10.Controls.Add(Me.Panel2)
        Me.TabPage10.Location = New System.Drawing.Point(4, 22)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage10.Size = New System.Drawing.Size(972, 456)
        Me.TabPage10.TabIndex = 9
        Me.TabPage10.Text = "Sub"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightGray
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.GroupBox22)
        Me.Panel2.Controls.Add(Me.GroupBox45)
        Me.Panel2.Controls.Add(Me.GroupBox35)
        Me.Panel2.Controls.Add(Me.GroupBox13)
        Me.Panel2.Controls.Add(Me.GroupBox12)
        Me.Panel2.Controls.Add(Me.GroupBox7)
        Me.Panel2.Controls.Add(Me.PictureBox12)
        Me.Panel2.Controls.Add(Me.GroupBox32)
        Me.Panel2.Controls.Add(Me.Label70)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(966, 450)
        Me.Panel2.TabIndex = 94
        '
        'GroupBox22
        '
        Me.GroupBox22.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox22.Controls.Add(Me.NBWritingTaskMax)
        Me.GroupBox22.Controls.Add(Me.NBWritingTaskMin)
        Me.GroupBox22.Controls.Add(Me.Label75)
        Me.GroupBox22.Controls.Add(Me.Label77)
        Me.GroupBox22.ForeColor = System.Drawing.Color.Black
        Me.GroupBox22.Location = New System.Drawing.Point(440, 388)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Size = New System.Drawing.Size(259, 39)
        Me.GroupBox22.TabIndex = 158
        Me.GroupBox22.TabStop = False
        Me.GroupBox22.Text = "Writing Tasks"
        '
        'NBWritingTaskMax
        '
        Me.NBWritingTaskMax.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBWritingTaskMax.Location = New System.Drawing.Point(200, 13)
        Me.NBWritingTaskMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NBWritingTaskMax.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBWritingTaskMax.Name = "NBWritingTaskMax"
        Me.NBWritingTaskMax.Size = New System.Drawing.Size(44, 20)
        Me.NBWritingTaskMax.TabIndex = 168
        Me.NBWritingTaskMax.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'NBWritingTaskMin
        '
        Me.NBWritingTaskMin.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBWritingTaskMin.Location = New System.Drawing.Point(134, 13)
        Me.NBWritingTaskMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NBWritingTaskMin.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBWritingTaskMin.Name = "NBWritingTaskMin"
        Me.NBWritingTaskMin.Size = New System.Drawing.Size(44, 20)
        Me.NBWritingTaskMin.TabIndex = 167
        Me.NBWritingTaskMin.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.Transparent
        Me.Label75.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.Color.Black
        Me.Label75.Location = New System.Drawing.Point(184, 13)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(10, 17)
        Me.Label75.TabIndex = 166
        Me.Label75.Text = "-"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.Transparent
        Me.Label77.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.Color.Black
        Me.Label77.Location = New System.Drawing.Point(12, 15)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(126, 17)
        Me.Label77.TabIndex = 165
        Me.Label77.Tag = ""
        Me.Label77.Text = "Line Amount Range:"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox45
        '
        Me.GroupBox45.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox45.Controls.Add(Me.CockAndBallTortureLevelLbl)
        Me.GroupBox45.Controls.Add(Me.BallTortureEnabledCB)
        Me.GroupBox45.Controls.Add(Me.CockTortureEnabledCB)
        Me.GroupBox45.Controls.Add(Me.CockAndBallTortureLevelSlider)
        Me.GroupBox45.ForeColor = System.Drawing.Color.Black
        Me.GroupBox45.Location = New System.Drawing.Point(440, 294)
        Me.GroupBox45.Name = "GroupBox45"
        Me.GroupBox45.Size = New System.Drawing.Size(259, 50)
        Me.GroupBox45.TabIndex = 155
        Me.GroupBox45.TabStop = False
        Me.GroupBox45.Text = "CBT"
        '
        'CockAndBallTortureLevelLbl
        '
        Me.CockAndBallTortureLevelLbl.BackColor = System.Drawing.Color.Transparent
        Me.CockAndBallTortureLevelLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CockAndBallTortureLevelLbl.ForeColor = System.Drawing.Color.Black
        Me.CockAndBallTortureLevelLbl.Location = New System.Drawing.Point(134, 30)
        Me.CockAndBallTortureLevelLbl.Name = "CockAndBallTortureLevelLbl"
        Me.CockAndBallTortureLevelLbl.Size = New System.Drawing.Size(110, 17)
        Me.CockAndBallTortureLevelLbl.TabIndex = 168
        Me.CockAndBallTortureLevelLbl.Text = "CBT Level: 3"
        Me.CockAndBallTortureLevelLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BallTortureEnabledCB
        '
        Me.BallTortureEnabledCB.Location = New System.Drawing.Point(69, 13)
        Me.BallTortureEnabledCB.Name = "BallTortureEnabledCB"
        Me.BallTortureEnabledCB.Size = New System.Drawing.Size(66, 31)
        Me.BallTortureEnabledCB.TabIndex = 1
        Me.BallTortureEnabledCB.Text = "Ball Torture"
        Me.BallTortureEnabledCB.UseVisualStyleBackColor = True
        '
        'CockTortureEnabledCB
        '
        Me.CockTortureEnabledCB.Location = New System.Drawing.Point(9, 13)
        Me.CockTortureEnabledCB.Name = "CockTortureEnabledCB"
        Me.CockTortureEnabledCB.Size = New System.Drawing.Size(68, 31)
        Me.CockTortureEnabledCB.TabIndex = 0
        Me.CockTortureEnabledCB.Text = "Cock Torture"
        Me.CockTortureEnabledCB.UseVisualStyleBackColor = True
        '
        'CockAndBallTortureLevelSlider
        '
        Me.CockAndBallTortureLevelSlider.AutoSize = False
        Me.CockAndBallTortureLevelSlider.LargeChange = 1
        Me.CockAndBallTortureLevelSlider.Location = New System.Drawing.Point(134, 13)
        Me.CockAndBallTortureLevelSlider.Maximum = 5
        Me.CockAndBallTortureLevelSlider.Minimum = 1
        Me.CockAndBallTortureLevelSlider.Name = "CockAndBallTortureLevelSlider"
        Me.CockAndBallTortureLevelSlider.Size = New System.Drawing.Size(110, 25)
        Me.CockAndBallTortureLevelSlider.TabIndex = 166
        Me.CockAndBallTortureLevelSlider.Value = 3
        '
        'GroupBox35
        '
        Me.GroupBox35.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox35.Controls.Add(Me.GroupBox38)
        Me.GroupBox35.Controls.Add(Me.GroupBox37)
        Me.GroupBox35.Controls.Add(Me.GroupBox36)
        Me.GroupBox35.ForeColor = System.Drawing.Color.Black
        Me.GroupBox35.Location = New System.Drawing.Point(440, 30)
        Me.GroupBox35.Name = "GroupBox35"
        Me.GroupBox35.Size = New System.Drawing.Size(259, 263)
        Me.GroupBox35.TabIndex = 154
        Me.GroupBox35.TabStop = False
        Me.GroupBox35.Text = "Key Phrases"
        '
        'GroupBox38
        '
        Me.GroupBox38.Controls.Add(Me.TBNo)
        Me.GroupBox38.Location = New System.Drawing.Point(6, 116)
        Me.GroupBox38.Name = "GroupBox38"
        Me.GroupBox38.Size = New System.Drawing.Size(247, 46)
        Me.GroupBox38.TabIndex = 2
        Me.GroupBox38.TabStop = False
        Me.GroupBox38.Tag = ""
        Me.GroupBox38.Text = "No"
        '
        'TBNo
        '
        Me.TBNo.Location = New System.Drawing.Point(9, 16)
        Me.TBNo.Name = "TBNo"
        Me.TBNo.Size = New System.Drawing.Size(229, 20)
        Me.TBNo.TabIndex = 0
        Me.TBNo.Text = "no, nah, nope, not"
        '
        'GroupBox37
        '
        Me.GroupBox37.Controls.Add(Me.TBYes)
        Me.GroupBox37.Location = New System.Drawing.Point(6, 64)
        Me.GroupBox37.Name = "GroupBox37"
        Me.GroupBox37.Size = New System.Drawing.Size(247, 46)
        Me.GroupBox37.TabIndex = 1
        Me.GroupBox37.TabStop = False
        Me.GroupBox37.Tag = ""
        Me.GroupBox37.Text = "Yes"
        '
        'TBYes
        '
        Me.TBYes.Location = New System.Drawing.Point(9, 16)
        Me.TBYes.Name = "TBYes"
        Me.TBYes.Size = New System.Drawing.Size(229, 20)
        Me.TBYes.TabIndex = 0
        Me.TBYes.Text = "yes, yeah, yep, yup, sure, of course, absolutely, you know it"
        '
        'GroupBox36
        '
        Me.GroupBox36.Controls.Add(Me.TBGreeting)
        Me.GroupBox36.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox36.Name = "GroupBox36"
        Me.GroupBox36.Size = New System.Drawing.Size(247, 46)
        Me.GroupBox36.TabIndex = 0
        Me.GroupBox36.TabStop = False
        Me.GroupBox36.Tag = ""
        Me.GroupBox36.Text = "Greeting"
        '
        'TBGreeting
        '
        Me.TBGreeting.Location = New System.Drawing.Point(9, 16)
        Me.TBGreeting.Name = "TBGreeting"
        Me.TBGreeting.Size = New System.Drawing.Size(229, 20)
        Me.TBGreeting.TabIndex = 0
        Me.TBGreeting.Text = "hello, hi, hey, heya, good morning, good afternoon, good evening"
        '
        'GroupBox13
        '
        Me.GroupBox13.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox13.Controls.Add(Me.Label34)
        Me.GroupBox13.Controls.Add(Me.TimeBoxWakeUp)
        Me.GroupBox13.ForeColor = System.Drawing.Color.Black
        Me.GroupBox13.Location = New System.Drawing.Point(440, 346)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(259, 39)
        Me.GroupBox13.TabIndex = 157
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Routine"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(12, 15)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(116, 17)
        Me.Label34.TabIndex = 140
        Me.Label34.Text = "Daily Wake Up Time:"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TimeBoxWakeUp
        '
        Me.TimeBoxWakeUp.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.TimeBoxWakeUp.Location = New System.Drawing.Point(134, 12)
        Me.TimeBoxWakeUp.Name = "TimeBoxWakeUp"
        Me.TimeBoxWakeUp.ShowUpDown = True
        Me.TimeBoxWakeUp.Size = New System.Drawing.Size(110, 20)
        Me.TimeBoxWakeUp.TabIndex = 0
        '
        'GroupBox12
        '
        Me.GroupBox12.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox12.Controls.Add(Me.LBLSubSettingsDescription)
        Me.GroupBox12.ForeColor = System.Drawing.Color.Black
        Me.GroupBox12.Location = New System.Drawing.Point(239, 201)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(195, 231)
        Me.GroupBox12.TabIndex = 65
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Description"
        '
        'LBLSubSettingsDescription
        '
        Me.LBLSubSettingsDescription.BackColor = System.Drawing.Color.Transparent
        Me.LBLSubSettingsDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubSettingsDescription.ForeColor = System.Drawing.Color.Black
        Me.LBLSubSettingsDescription.Location = New System.Drawing.Point(10, 19)
        Me.LBLSubSettingsDescription.Name = "LBLSubSettingsDescription"
        Me.LBLSubSettingsDescription.Size = New System.Drawing.Size(179, 206)
        Me.LBLSubSettingsDescription.TabIndex = 62
        Me.LBLSubSettingsDescription.Text = "Hover over any setting in the menu for a more detailed description of its functio" &
    "n."
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.LBLMaxExtremeHold)
        Me.GroupBox7.Controls.Add(Me.LBLMinExtremeHold)
        Me.GroupBox7.Controls.Add(Me.ExtremeEdgeHoldMinimum)
        Me.GroupBox7.Controls.Add(Me.Label133)
        Me.GroupBox7.Controls.Add(Me.ExtremeEdgeHoldMaximum)
        Me.GroupBox7.Controls.Add(Me.LBLMaxLongHold)
        Me.GroupBox7.Controls.Add(Me.Label78)
        Me.GroupBox7.Controls.Add(Me.LBLMinLongHold)
        Me.GroupBox7.Controls.Add(Me.LongEdgeHoldMinimum)
        Me.GroupBox7.Controls.Add(Me.Label129)
        Me.GroupBox7.Controls.Add(Me.LongEdgeHoldMaximum)
        Me.GroupBox7.Controls.Add(Me.LBLMaxHold)
        Me.GroupBox7.Controls.Add(Me.Label79)
        Me.GroupBox7.Controls.Add(Me.NBLongEdge)
        Me.GroupBox7.Controls.Add(Me.HoldEdgeMinimumUnits)
        Me.GroupBox7.Controls.Add(Me.UseAverageEdgeThresholdCB)
        Me.GroupBox7.Controls.Add(Me.AllowLongEdgeInterruptCB)
        Me.GroupBox7.Controls.Add(Me.HoldEdgeMinimum)
        Me.GroupBox7.Controls.Add(Me.Label55)
        Me.GroupBox7.Controls.Add(Me.Label81)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Controls.Add(Me.HoldEdgeMaximum)
        Me.GroupBox7.Controls.Add(Me.AllowLongEdgeTauntCB)
        Me.GroupBox7.Controls.Add(Me.Label131)
        Me.GroupBox7.Location = New System.Drawing.Point(7, 201)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(226, 226)
        Me.GroupBox7.TabIndex = 152
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Edging"
        '
        'LBLMaxExtremeHold
        '
        Me.LBLMaxExtremeHold.AutoSize = True
        Me.LBLMaxExtremeHold.Location = New System.Drawing.Point(173, 128)
        Me.LBLMaxExtremeHold.Name = "LBLMaxExtremeHold"
        Me.LBLMaxExtremeHold.Size = New System.Drawing.Size(43, 13)
        Me.LBLMaxExtremeHold.TabIndex = 192
        Me.LBLMaxExtremeHold.Text = "minutes"
        Me.LBLMaxExtremeHold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLMinExtremeHold
        '
        Me.LBLMinExtremeHold.AutoSize = True
        Me.LBLMinExtremeHold.Location = New System.Drawing.Point(173, 106)
        Me.LBLMinExtremeHold.Name = "LBLMinExtremeHold"
        Me.LBLMinExtremeHold.Size = New System.Drawing.Size(43, 13)
        Me.LBLMinExtremeHold.TabIndex = 190
        Me.LBLMinExtremeHold.Text = "minutes"
        Me.LBLMinExtremeHold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExtremeEdgeHoldMinimum
        '
        Me.ExtremeEdgeHoldMinimum.Location = New System.Drawing.Point(128, 104)
        Me.ExtremeEdgeHoldMinimum.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.ExtremeEdgeHoldMinimum.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ExtremeEdgeHoldMinimum.Name = "ExtremeEdgeHoldMinimum"
        Me.ExtremeEdgeHoldMinimum.Size = New System.Drawing.Size(44, 20)
        Me.ExtremeEdgeHoldMinimum.TabIndex = 189
        Me.ExtremeEdgeHoldMinimum.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.Transparent
        Me.Label133.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.ForeColor = System.Drawing.Color.Black
        Me.Label133.Location = New System.Drawing.Point(6, 105)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(119, 17)
        Me.Label133.TabIndex = 187
        Me.Label133.Text = "Min Extreme Hold Time:"
        Me.Label133.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ExtremeEdgeHoldMaximum
        '
        Me.ExtremeEdgeHoldMaximum.Location = New System.Drawing.Point(128, 126)
        Me.ExtremeEdgeHoldMaximum.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.ExtremeEdgeHoldMaximum.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.ExtremeEdgeHoldMaximum.Name = "ExtremeEdgeHoldMaximum"
        Me.ExtremeEdgeHoldMaximum.Size = New System.Drawing.Size(44, 20)
        Me.ExtremeEdgeHoldMaximum.TabIndex = 188
        Me.ExtremeEdgeHoldMaximum.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'LBLMaxLongHold
        '
        Me.LBLMaxLongHold.AutoSize = True
        Me.LBLMaxLongHold.Location = New System.Drawing.Point(173, 84)
        Me.LBLMaxLongHold.Name = "LBLMaxLongHold"
        Me.LBLMaxLongHold.Size = New System.Drawing.Size(43, 13)
        Me.LBLMaxLongHold.TabIndex = 186
        Me.LBLMaxLongHold.Text = "minutes"
        Me.LBLMaxLongHold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.Transparent
        Me.Label78.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.Black
        Me.Label78.Location = New System.Drawing.Point(6, 83)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(113, 17)
        Me.Label78.TabIndex = 185
        Me.Label78.Text = "Max Long Hold Time:"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLMinLongHold
        '
        Me.LBLMinLongHold.AutoSize = True
        Me.LBLMinLongHold.Location = New System.Drawing.Point(173, 62)
        Me.LBLMinLongHold.Name = "LBLMinLongHold"
        Me.LBLMinLongHold.Size = New System.Drawing.Size(43, 13)
        Me.LBLMinLongHold.TabIndex = 184
        Me.LBLMinLongHold.Text = "minutes"
        Me.LBLMinLongHold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LongEdgeHoldMinimum
        '
        Me.LongEdgeHoldMinimum.Location = New System.Drawing.Point(128, 60)
        Me.LongEdgeHoldMinimum.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.LongEdgeHoldMinimum.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.LongEdgeHoldMinimum.Name = "LongEdgeHoldMinimum"
        Me.LongEdgeHoldMinimum.Size = New System.Drawing.Size(44, 20)
        Me.LongEdgeHoldMinimum.TabIndex = 183
        Me.LongEdgeHoldMinimum.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.Transparent
        Me.Label129.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label129.ForeColor = System.Drawing.Color.Black
        Me.Label129.Location = New System.Drawing.Point(6, 61)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(113, 17)
        Me.Label129.TabIndex = 181
        Me.Label129.Text = "Min Long Hold Time:"
        Me.Label129.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LongEdgeHoldMaximum
        '
        Me.LongEdgeHoldMaximum.Location = New System.Drawing.Point(128, 82)
        Me.LongEdgeHoldMaximum.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.LongEdgeHoldMaximum.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.LongEdgeHoldMaximum.Name = "LongEdgeHoldMaximum"
        Me.LongEdgeHoldMaximum.Size = New System.Drawing.Size(44, 20)
        Me.LongEdgeHoldMaximum.TabIndex = 182
        Me.LongEdgeHoldMaximum.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'LBLMaxHold
        '
        Me.LBLMaxHold.AutoSize = True
        Me.LBLMaxHold.Location = New System.Drawing.Point(173, 40)
        Me.LBLMaxHold.Name = "LBLMaxHold"
        Me.LBLMaxHold.Size = New System.Drawing.Size(43, 13)
        Me.LBLMaxHold.TabIndex = 180
        Me.LBLMaxHold.Text = "minutes"
        Me.LBLMaxHold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.Transparent
        Me.Label79.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.ForeColor = System.Drawing.Color.Black
        Me.Label79.Location = New System.Drawing.Point(6, 39)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(113, 17)
        Me.Label79.TabIndex = 178
        Me.Label79.Text = "Max Hold Edge Time:"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBLongEdge
        '
        Me.NBLongEdge.Location = New System.Drawing.Point(128, 148)
        Me.NBLongEdge.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.NBLongEdge.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBLongEdge.Name = "NBLongEdge"
        Me.NBLongEdge.Size = New System.Drawing.Size(44, 20)
        Me.NBLongEdge.TabIndex = 152
        Me.NBLongEdge.Value = New Decimal(New Integer() {120, 0, 0, 0})
        '
        'HoldEdgeMinimumUnits
        '
        Me.HoldEdgeMinimumUnits.AutoSize = True
        Me.HoldEdgeMinimumUnits.Location = New System.Drawing.Point(173, 18)
        Me.HoldEdgeMinimumUnits.Name = "HoldEdgeMinimumUnits"
        Me.HoldEdgeMinimumUnits.Size = New System.Drawing.Size(47, 13)
        Me.HoldEdgeMinimumUnits.TabIndex = 177
        Me.HoldEdgeMinimumUnits.Text = "seconds"
        Me.HoldEdgeMinimumUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UseAverageEdgeThresholdCB
        '
        Me.UseAverageEdgeThresholdCB.AutoSize = True
        Me.UseAverageEdgeThresholdCB.Enabled = False
        Me.UseAverageEdgeThresholdCB.Location = New System.Drawing.Point(9, 170)
        Me.UseAverageEdgeThresholdCB.Name = "UseAverageEdgeThresholdCB"
        Me.UseAverageEdgeThresholdCB.Size = New System.Drawing.Size(185, 17)
        Me.UseAverageEdgeThresholdCB.TabIndex = 174
        Me.UseAverageEdgeThresholdCB.Text = "Use Avg Edge Time as Threshold"
        Me.UseAverageEdgeThresholdCB.UseVisualStyleBackColor = True
        '
        'AllowLongEdgeInterruptCB
        '
        Me.AllowLongEdgeInterruptCB.Checked = True
        Me.AllowLongEdgeInterruptCB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AllowLongEdgeInterruptCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AllowLongEdgeInterruptCB.ForeColor = System.Drawing.Color.Black
        Me.AllowLongEdgeInterruptCB.Location = New System.Drawing.Point(9, 204)
        Me.AllowLongEdgeInterruptCB.Name = "AllowLongEdgeInterruptCB"
        Me.AllowLongEdgeInterruptCB.Size = New System.Drawing.Size(177, 21)
        Me.AllowLongEdgeInterruptCB.TabIndex = 169
        Me.AllowLongEdgeInterruptCB.Text = "Allow Long Edge Interrupts"
        Me.AllowLongEdgeInterruptCB.UseVisualStyleBackColor = True
        '
        'HoldEdgeMinimum
        '
        Me.HoldEdgeMinimum.Location = New System.Drawing.Point(128, 16)
        Me.HoldEdgeMinimum.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.HoldEdgeMinimum.Name = "HoldEdgeMinimum"
        Me.HoldEdgeMinimum.Size = New System.Drawing.Size(44, 20)
        Me.HoldEdgeMinimum.TabIndex = 176
        Me.HoldEdgeMinimum.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.Transparent
        Me.Label55.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.Black
        Me.Label55.Location = New System.Drawing.Point(7, 149)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(116, 17)
        Me.Label55.TabIndex = 170
        Me.Label55.Text = "Long Edge Threshold:"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.Transparent
        Me.Label81.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.Black
        Me.Label81.Location = New System.Drawing.Point(6, 17)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(113, 17)
        Me.Label81.TabIndex = 153
        Me.Label81.Text = "Min Hold Edge Time:"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(174, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 175
        Me.Label5.Text = "minutes"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HoldEdgeMaximum
        '
        Me.HoldEdgeMaximum.Location = New System.Drawing.Point(128, 38)
        Me.HoldEdgeMaximum.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.HoldEdgeMaximum.Name = "HoldEdgeMaximum"
        Me.HoldEdgeMaximum.Size = New System.Drawing.Size(44, 20)
        Me.HoldEdgeMaximum.TabIndex = 155
        Me.HoldEdgeMaximum.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'AllowLongEdgeTauntCB
        '
        Me.AllowLongEdgeTauntCB.Checked = True
        Me.AllowLongEdgeTauntCB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AllowLongEdgeTauntCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AllowLongEdgeTauntCB.ForeColor = System.Drawing.Color.Black
        Me.AllowLongEdgeTauntCB.Location = New System.Drawing.Point(9, 186)
        Me.AllowLongEdgeTauntCB.Name = "AllowLongEdgeTauntCB"
        Me.AllowLongEdgeTauntCB.Size = New System.Drawing.Size(163, 21)
        Me.AllowLongEdgeTauntCB.TabIndex = 172
        Me.AllowLongEdgeTauntCB.Text = "Allow Long Edge Taunts"
        Me.AllowLongEdgeTauntCB.UseVisualStyleBackColor = True
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.Transparent
        Me.Label131.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label131.ForeColor = System.Drawing.Color.Black
        Me.Label131.Location = New System.Drawing.Point(6, 127)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(128, 17)
        Me.Label131.TabIndex = 191
        Me.Label131.Text = "Max Extreme Hold Time:"
        Me.Label131.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox12
        '
        Me.PictureBox12.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox12.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.PictureBox12.Location = New System.Drawing.Point(9, 6)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(160, 19)
        Me.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox12.TabIndex = 149
        Me.PictureBox12.TabStop = False
        '
        'GroupBox32
        '
        Me.GroupBox32.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox32.Controls.Add(Me.LBLSubBdayFormat)
        Me.GroupBox32.Controls.Add(Me.ChastityDeviceContainsSpikesCB)
        Me.GroupBox32.Controls.Add(Me.CBOwnChastity)
        Me.GroupBox32.Controls.Add(Me.DoesChastityDeviceRequirePiercingCB)
        Me.GroupBox32.Controls.Add(Me.CBHimHer)
        Me.GroupBox32.Controls.Add(Me.CBBallsToPussy)
        Me.GroupBox32.Controls.Add(Me.CBCockToClit)
        Me.GroupBox32.Controls.Add(Me.NBBirthdayDay)
        Me.GroupBox32.Controls.Add(Me.CBSubCircumcised)
        Me.GroupBox32.Controls.Add(Me.CBSubPierced)
        Me.GroupBox32.Controls.Add(Me.TBSubEyeColor)
        Me.GroupBox32.Controls.Add(Me.TBSubHairColor)
        Me.GroupBox32.Controls.Add(Me.Label63)
        Me.GroupBox32.Controls.Add(Me.LBLSubInches)
        Me.GroupBox32.Controls.Add(Me.subAgeNumBox)
        Me.GroupBox32.Controls.Add(Me.NBBirthdayMonth)
        Me.GroupBox32.Controls.Add(Me.LBLSubCockSize)
        Me.GroupBox32.Controls.Add(Me.CockSizeNumBox)
        Me.GroupBox32.Controls.Add(Me.LBLSubEye)
        Me.GroupBox32.Controls.Add(Me.LBLSubHair)
        Me.GroupBox32.Controls.Add(Me.LBLSubBirthday)
        Me.GroupBox32.Controls.Add(Me.LBLSubAge)
        Me.GroupBox32.ForeColor = System.Drawing.Color.Black
        Me.GroupBox32.Location = New System.Drawing.Point(7, 30)
        Me.GroupBox32.Name = "GroupBox32"
        Me.GroupBox32.Size = New System.Drawing.Size(427, 165)
        Me.GroupBox32.TabIndex = 62
        Me.GroupBox32.TabStop = False
        Me.GroupBox32.Text = "Stats && Information"
        '
        'LBLSubBdayFormat
        '
        Me.LBLSubBdayFormat.AutoSize = True
        Me.LBLSubBdayFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubBdayFormat.Location = New System.Drawing.Point(125, 22)
        Me.LBLSubBdayFormat.Name = "LBLSubBdayFormat"
        Me.LBLSubBdayFormat.Size = New System.Drawing.Size(38, 13)
        Me.LBLSubBdayFormat.TabIndex = 161
        Me.LBLSubBdayFormat.Text = "mm/dd"
        Me.LBLSubBdayFormat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChastityDeviceContainsSpikesCB
        '
        Me.ChastityDeviceContainsSpikesCB.AutoSize = True
        Me.ChastityDeviceContainsSpikesCB.Enabled = False
        Me.ChastityDeviceContainsSpikesCB.Location = New System.Drawing.Point(191, 140)
        Me.ChastityDeviceContainsSpikesCB.Name = "ChastityDeviceContainsSpikesCB"
        Me.ChastityDeviceContainsSpikesCB.Size = New System.Drawing.Size(179, 17)
        Me.ChastityDeviceContainsSpikesCB.TabIndex = 4
        Me.ChastityDeviceContainsSpikesCB.Text = "Chastity Device Contains Spikes"
        Me.ChastityDeviceContainsSpikesCB.UseVisualStyleBackColor = True
        '
        'CBOwnChastity
        '
        Me.CBOwnChastity.AutoSize = True
        Me.CBOwnChastity.Location = New System.Drawing.Point(191, 90)
        Me.CBOwnChastity.Name = "CBOwnChastity"
        Me.CBOwnChastity.Size = New System.Drawing.Size(171, 17)
        Me.CBOwnChastity.TabIndex = 5
        Me.CBOwnChastity.Text = "Own Device a Chastity Device"
        Me.CBOwnChastity.UseVisualStyleBackColor = True
        '
        'DoesChastityDeviceRequirePiercingCB
        '
        Me.DoesChastityDeviceRequirePiercingCB.AutoSize = True
        Me.DoesChastityDeviceRequirePiercingCB.Enabled = False
        Me.DoesChastityDeviceRequirePiercingCB.Location = New System.Drawing.Point(191, 115)
        Me.DoesChastityDeviceRequirePiercingCB.Name = "DoesChastityDeviceRequirePiercingCB"
        Me.DoesChastityDeviceRequirePiercingCB.Size = New System.Drawing.Size(195, 17)
        Me.DoesChastityDeviceRequirePiercingCB.TabIndex = 3
        Me.DoesChastityDeviceRequirePiercingCB.Text = "Chastity Device Requires a Piercing"
        Me.DoesChastityDeviceRequirePiercingCB.UseVisualStyleBackColor = True
        '
        'CBHimHer
        '
        Me.CBHimHer.AutoSize = True
        Me.CBHimHer.Location = New System.Drawing.Point(191, 65)
        Me.CBHimHer.Name = "CBHimHer"
        Me.CBHimHer.Size = New System.Drawing.Size(219, 17)
        Me.CBHimHer.TabIndex = 3
        Me.CBHimHer.Text = "Replace Male Glitter Pronouns to Female"
        Me.CBHimHer.UseVisualStyleBackColor = True
        '
        'CBBallsToPussy
        '
        Me.CBBallsToPussy.AutoSize = True
        Me.CBBallsToPussy.Location = New System.Drawing.Point(191, 40)
        Me.CBBallsToPussy.Name = "CBBallsToPussy"
        Me.CBBallsToPussy.Size = New System.Drawing.Size(193, 17)
        Me.CBBallsToPussy.TabIndex = 160
        Me.CBBallsToPussy.Text = "Replace #Balls with #BallsToPussy"
        Me.CBBallsToPussy.UseVisualStyleBackColor = True
        '
        'CBCockToClit
        '
        Me.CBCockToClit.AutoSize = True
        Me.CBCockToClit.Location = New System.Drawing.Point(191, 15)
        Me.CBCockToClit.Name = "CBCockToClit"
        Me.CBCockToClit.Size = New System.Drawing.Size(185, 17)
        Me.CBCockToClit.TabIndex = 159
        Me.CBCockToClit.Text = "Replace #Cock with #CockToClit"
        Me.CBCockToClit.UseVisualStyleBackColor = True
        '
        'NBBirthdayDay
        '
        Me.NBBirthdayDay.BackColor = System.Drawing.Color.White
        Me.NBBirthdayDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBBirthdayDay.ForeColor = System.Drawing.Color.Black
        Me.NBBirthdayDay.Location = New System.Drawing.Point(125, 37)
        Me.NBBirthdayDay.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.NBBirthdayDay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBBirthdayDay.Name = "NBBirthdayDay"
        Me.NBBirthdayDay.Size = New System.Drawing.Size(38, 20)
        Me.NBBirthdayDay.TabIndex = 144
        Me.NBBirthdayDay.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'CBSubCircumcised
        '
        Me.CBSubCircumcised.AutoSize = True
        Me.CBSubCircumcised.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBSubCircumcised.ForeColor = System.Drawing.Color.Black
        Me.CBSubCircumcised.Location = New System.Drawing.Point(9, 138)
        Me.CBSubCircumcised.Name = "CBSubCircumcised"
        Me.CBSubCircumcised.Size = New System.Drawing.Size(83, 17)
        Me.CBSubCircumcised.TabIndex = 157
        Me.CBSubCircumcised.Text = "Circumcised"
        Me.CBSubCircumcised.UseVisualStyleBackColor = True
        '
        'CBSubPierced
        '
        Me.CBSubPierced.AutoSize = True
        Me.CBSubPierced.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBSubPierced.ForeColor = System.Drawing.Color.Black
        Me.CBSubPierced.Location = New System.Drawing.Point(98, 138)
        Me.CBSubPierced.Name = "CBSubPierced"
        Me.CBSubPierced.Size = New System.Drawing.Size(62, 17)
        Me.CBSubPierced.TabIndex = 156
        Me.CBSubPierced.Text = "Pierced"
        Me.CBSubPierced.UseVisualStyleBackColor = True
        '
        'TBSubEyeColor
        '
        Me.TBSubEyeColor.BackColor = System.Drawing.Color.White
        Me.TBSubEyeColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBSubEyeColor.ForeColor = System.Drawing.Color.Black
        Me.TBSubEyeColor.Location = New System.Drawing.Point(73, 87)
        Me.TBSubEyeColor.Name = "TBSubEyeColor"
        Me.TBSubEyeColor.Size = New System.Drawing.Size(89, 23)
        Me.TBSubEyeColor.TabIndex = 155
        Me.TBSubEyeColor.Text = "brown"
        '
        'TBSubHairColor
        '
        Me.TBSubHairColor.BackColor = System.Drawing.Color.White
        Me.TBSubHairColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBSubHairColor.ForeColor = System.Drawing.Color.Black
        Me.TBSubHairColor.Location = New System.Drawing.Point(73, 60)
        Me.TBSubHairColor.Name = "TBSubHairColor"
        Me.TBSubHairColor.Size = New System.Drawing.Size(89, 23)
        Me.TBSubHairColor.TabIndex = 154
        Me.TBSubHairColor.Text = "brown"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.Black
        Me.Label63.Location = New System.Drawing.Point(113, 41)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(12, 13)
        Me.Label63.TabIndex = 143
        Me.Label63.Text = "/"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLSubInches
        '
        Me.LBLSubInches.AutoSize = True
        Me.LBLSubInches.Location = New System.Drawing.Point(118, 118)
        Me.LBLSubInches.Name = "LBLSubInches"
        Me.LBLSubInches.Size = New System.Drawing.Size(38, 13)
        Me.LBLSubInches.TabIndex = 124
        Me.LBLSubInches.Text = "inches"
        Me.LBLSubInches.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'subAgeNumBox
        '
        Me.subAgeNumBox.BackColor = System.Drawing.Color.White
        Me.subAgeNumBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.subAgeNumBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.subAgeNumBox.ForeColor = System.Drawing.Color.Black
        Me.subAgeNumBox.Location = New System.Drawing.Point(73, 14)
        Me.subAgeNumBox.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.subAgeNumBox.Minimum = New Decimal(New Integer() {18, 0, 0, 0})
        Me.subAgeNumBox.Name = "subAgeNumBox"
        Me.subAgeNumBox.Size = New System.Drawing.Size(38, 20)
        Me.subAgeNumBox.TabIndex = 27
        Me.subAgeNumBox.Value = New Decimal(New Integer() {21, 0, 0, 0})
        '
        'NBBirthdayMonth
        '
        Me.NBBirthdayMonth.BackColor = System.Drawing.Color.White
        Me.NBBirthdayMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NBBirthdayMonth.ForeColor = System.Drawing.Color.Black
        Me.NBBirthdayMonth.Location = New System.Drawing.Point(73, 37)
        Me.NBBirthdayMonth.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.NBBirthdayMonth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBBirthdayMonth.Name = "NBBirthdayMonth"
        Me.NBBirthdayMonth.Size = New System.Drawing.Size(38, 20)
        Me.NBBirthdayMonth.TabIndex = 41
        Me.NBBirthdayMonth.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'LBLSubCockSize
        '
        Me.LBLSubCockSize.BackColor = System.Drawing.Color.Transparent
        Me.LBLSubCockSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubCockSize.ForeColor = System.Drawing.Color.Black
        Me.LBLSubCockSize.Location = New System.Drawing.Point(6, 114)
        Me.LBLSubCockSize.Name = "LBLSubCockSize"
        Me.LBLSubCockSize.Size = New System.Drawing.Size(63, 17)
        Me.LBLSubCockSize.TabIndex = 142
        Me.LBLSubCockSize.Text = "Cock Size:"
        Me.LBLSubCockSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CockSizeNumBox
        '
        Me.CockSizeNumBox.BackColor = System.Drawing.Color.White
        Me.CockSizeNumBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CockSizeNumBox.ForeColor = System.Drawing.Color.Black
        Me.CockSizeNumBox.Location = New System.Drawing.Point(73, 114)
        Me.CockSizeNumBox.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.CockSizeNumBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.CockSizeNumBox.Name = "CockSizeNumBox"
        Me.CockSizeNumBox.Size = New System.Drawing.Size(38, 20)
        Me.CockSizeNumBox.TabIndex = 123
        Me.CockSizeNumBox.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'LBLSubEye
        '
        Me.LBLSubEye.BackColor = System.Drawing.Color.Transparent
        Me.LBLSubEye.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubEye.ForeColor = System.Drawing.Color.Black
        Me.LBLSubEye.Location = New System.Drawing.Point(6, 90)
        Me.LBLSubEye.Name = "LBLSubEye"
        Me.LBLSubEye.Size = New System.Drawing.Size(63, 17)
        Me.LBLSubEye.TabIndex = 141
        Me.LBLSubEye.Text = "Eye Color"
        Me.LBLSubEye.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLSubHair
        '
        Me.LBLSubHair.BackColor = System.Drawing.Color.Transparent
        Me.LBLSubHair.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubHair.ForeColor = System.Drawing.Color.Black
        Me.LBLSubHair.Location = New System.Drawing.Point(6, 63)
        Me.LBLSubHair.Name = "LBLSubHair"
        Me.LBLSubHair.Size = New System.Drawing.Size(78, 17)
        Me.LBLSubHair.TabIndex = 140
        Me.LBLSubHair.Text = "Hair Color"
        Me.LBLSubHair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLSubBirthday
        '
        Me.LBLSubBirthday.BackColor = System.Drawing.Color.Transparent
        Me.LBLSubBirthday.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubBirthday.ForeColor = System.Drawing.Color.Black
        Me.LBLSubBirthday.Location = New System.Drawing.Point(6, 37)
        Me.LBLSubBirthday.Name = "LBLSubBirthday"
        Me.LBLSubBirthday.Size = New System.Drawing.Size(60, 17)
        Me.LBLSubBirthday.TabIndex = 139
        Me.LBLSubBirthday.Text = "Birthday:"
        Me.LBLSubBirthday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLSubAge
        '
        Me.LBLSubAge.BackColor = System.Drawing.Color.Transparent
        Me.LBLSubAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSubAge.ForeColor = System.Drawing.Color.Black
        Me.LBLSubAge.Location = New System.Drawing.Point(6, 15)
        Me.LBLSubAge.Name = "LBLSubAge"
        Me.LBLSubAge.Size = New System.Drawing.Size(63, 17)
        Me.LBLSubAge.TabIndex = 138
        Me.LBLSubAge.Text = "Age:"
        Me.LBLSubAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.Transparent
        Me.Label70.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.Color.Black
        Me.Label70.Location = New System.Drawing.Point(7, 6)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(692, 21)
        Me.Label70.TabIndex = 49
        Me.Label70.Text = "Sub Settings"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage16
        '
        Me.TabPage16.BackColor = System.Drawing.Color.Silver
        Me.TabPage16.Controls.Add(Me.Panel9)
        Me.TabPage16.Location = New System.Drawing.Point(4, 22)
        Me.TabPage16.Name = "TabPage16"
        Me.TabPage16.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage16.Size = New System.Drawing.Size(972, 456)
        Me.TabPage16.TabIndex = 14
        Me.TabPage16.Text = "Scripts"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightGray
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.ScriptNavPanel)
        Me.Panel9.Controls.Add(Me.ScriptInfoPanel)
        Me.Panel9.Controls.Add(Me.GroupBox43)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(3, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(966, 450)
        Me.Panel9.TabIndex = 94
        '
        'ScriptNavPanel
        '
        Me.ScriptNavPanel.Controls.Add(Me.SelectAllScriptsButton)
        Me.ScriptNavPanel.Controls.Add(Me.TCScripts)
        Me.ScriptNavPanel.Controls.Add(Me.SelectAvailableScriptsButton)
        Me.ScriptNavPanel.Controls.Add(Me.ScriptTitle)
        Me.ScriptNavPanel.Controls.Add(Me.BTNScriptOpen)
        Me.ScriptNavPanel.Controls.Add(Me.SelectNoScriptsButton)
        Me.ScriptNavPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.ScriptNavPanel.Location = New System.Drawing.Point(0, 0)
        Me.ScriptNavPanel.Name = "ScriptNavPanel"
        Me.ScriptNavPanel.Size = New System.Drawing.Size(443, 357)
        Me.ScriptNavPanel.TabIndex = 162
        '
        'SelectAllScriptsButton
        '
        Me.SelectAllScriptsButton.Location = New System.Drawing.Point(117, 409)
        Me.SelectAllScriptsButton.Name = "SelectAllScriptsButton"
        Me.SelectAllScriptsButton.Size = New System.Drawing.Size(75, 23)
        Me.SelectAllScriptsButton.TabIndex = 158
        Me.SelectAllScriptsButton.Text = "Select All"
        Me.SelectAllScriptsButton.UseVisualStyleBackColor = True
        '
        'TCScripts
        '
        Me.TCScripts.Controls.Add(Me.ScriptsStartTab)
        Me.TCScripts.Controls.Add(Me.ScriptsModuleTab)
        Me.TCScripts.Controls.Add(Me.ScriptsLinkTab)
        Me.TCScripts.Controls.Add(Me.ScriptsEndTab)
        Me.TCScripts.Dock = System.Windows.Forms.DockStyle.Top
        Me.TCScripts.Location = New System.Drawing.Point(0, 21)
        Me.TCScripts.Name = "TCScripts"
        Me.TCScripts.SelectedIndex = 0
        Me.TCScripts.Size = New System.Drawing.Size(443, 382)
        Me.TCScripts.TabIndex = 154
        '
        'ScriptsStartTab
        '
        Me.ScriptsStartTab.BackColor = System.Drawing.Color.Silver
        Me.ScriptsStartTab.Controls.Add(Me.StartScripts)
        Me.ScriptsStartTab.Location = New System.Drawing.Point(4, 22)
        Me.ScriptsStartTab.Name = "ScriptsStartTab"
        Me.ScriptsStartTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ScriptsStartTab.Size = New System.Drawing.Size(435, 356)
        Me.ScriptsStartTab.TabIndex = 4
        Me.ScriptsStartTab.Text = "Start"
        '
        'StartScripts
        '
        Me.StartScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StartScripts.FormattingEnabled = True
        Me.StartScripts.Location = New System.Drawing.Point(3, 3)
        Me.StartScripts.Name = "StartScripts"
        Me.StartScripts.Size = New System.Drawing.Size(429, 350)
        Me.StartScripts.Sorted = True
        Me.StartScripts.TabIndex = 155
        '
        'ScriptsModuleTab
        '
        Me.ScriptsModuleTab.BackColor = System.Drawing.Color.Silver
        Me.ScriptsModuleTab.Controls.Add(Me.ModuleScripts)
        Me.ScriptsModuleTab.Location = New System.Drawing.Point(4, 22)
        Me.ScriptsModuleTab.Name = "ScriptsModuleTab"
        Me.ScriptsModuleTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ScriptsModuleTab.Size = New System.Drawing.Size(435, 356)
        Me.ScriptsModuleTab.TabIndex = 5
        Me.ScriptsModuleTab.Text = "Modules"
        '
        'ModuleScripts
        '
        Me.ModuleScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModuleScripts.FormattingEnabled = True
        Me.ModuleScripts.Location = New System.Drawing.Point(3, 3)
        Me.ModuleScripts.Name = "ModuleScripts"
        Me.ModuleScripts.Size = New System.Drawing.Size(429, 350)
        Me.ModuleScripts.Sorted = True
        Me.ModuleScripts.TabIndex = 156
        '
        'ScriptsLinkTab
        '
        Me.ScriptsLinkTab.BackColor = System.Drawing.Color.Silver
        Me.ScriptsLinkTab.Controls.Add(Me.LinkScripts)
        Me.ScriptsLinkTab.Location = New System.Drawing.Point(4, 22)
        Me.ScriptsLinkTab.Name = "ScriptsLinkTab"
        Me.ScriptsLinkTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ScriptsLinkTab.Size = New System.Drawing.Size(435, 356)
        Me.ScriptsLinkTab.TabIndex = 6
        Me.ScriptsLinkTab.Text = "Link"
        '
        'LinkScripts
        '
        Me.LinkScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LinkScripts.FormattingEnabled = True
        Me.LinkScripts.Location = New System.Drawing.Point(3, 3)
        Me.LinkScripts.Name = "LinkScripts"
        Me.LinkScripts.Size = New System.Drawing.Size(429, 350)
        Me.LinkScripts.Sorted = True
        Me.LinkScripts.TabIndex = 156
        '
        'ScriptsEndTab
        '
        Me.ScriptsEndTab.BackColor = System.Drawing.Color.Silver
        Me.ScriptsEndTab.Controls.Add(Me.EndScripts)
        Me.ScriptsEndTab.Location = New System.Drawing.Point(4, 22)
        Me.ScriptsEndTab.Name = "ScriptsEndTab"
        Me.ScriptsEndTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ScriptsEndTab.Size = New System.Drawing.Size(435, 356)
        Me.ScriptsEndTab.TabIndex = 7
        Me.ScriptsEndTab.Text = "End"
        '
        'EndScripts
        '
        Me.EndScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EndScripts.FormattingEnabled = True
        Me.EndScripts.Location = New System.Drawing.Point(3, 3)
        Me.EndScripts.Name = "EndScripts"
        Me.EndScripts.Size = New System.Drawing.Size(429, 350)
        Me.EndScripts.Sorted = True
        Me.EndScripts.TabIndex = 156
        '
        'SelectAvailableScriptsButton
        '
        Me.SelectAvailableScriptsButton.Location = New System.Drawing.Point(315, 409)
        Me.SelectAvailableScriptsButton.Name = "SelectAvailableScriptsButton"
        Me.SelectAvailableScriptsButton.Size = New System.Drawing.Size(100, 23)
        Me.SelectAvailableScriptsButton.TabIndex = 160
        Me.SelectAvailableScriptsButton.Text = "Select Available"
        Me.SelectAvailableScriptsButton.UseVisualStyleBackColor = True
        '
        'ScriptTitle
        '
        Me.ScriptTitle.BackColor = System.Drawing.Color.Transparent
        Me.ScriptTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.ScriptTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScriptTitle.ForeColor = System.Drawing.Color.Black
        Me.ScriptTitle.Location = New System.Drawing.Point(0, 0)
        Me.ScriptTitle.Name = "ScriptTitle"
        Me.ScriptTitle.Size = New System.Drawing.Size(443, 21)
        Me.ScriptTitle.TabIndex = 49
        Me.ScriptTitle.Text = "Script Selection"
        Me.ScriptTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNScriptOpen
        '
        Me.BTNScriptOpen.Location = New System.Drawing.Point(15, 409)
        Me.BTNScriptOpen.Name = "BTNScriptOpen"
        Me.BTNScriptOpen.Size = New System.Drawing.Size(75, 23)
        Me.BTNScriptOpen.TabIndex = 157
        Me.BTNScriptOpen.Text = "Open Script"
        Me.BTNScriptOpen.UseVisualStyleBackColor = True
        '
        'SelectNoScriptsButton
        '
        Me.SelectNoScriptsButton.Location = New System.Drawing.Point(219, 409)
        Me.SelectNoScriptsButton.Name = "SelectNoScriptsButton"
        Me.SelectNoScriptsButton.Size = New System.Drawing.Size(75, 23)
        Me.SelectNoScriptsButton.TabIndex = 159
        Me.SelectNoScriptsButton.Text = "Select None"
        Me.SelectNoScriptsButton.UseVisualStyleBackColor = True
        '
        'ScriptInfoPanel
        '
        Me.ScriptInfoPanel.Controls.Add(Me.ScriptsDescriptionGroup)
        Me.ScriptInfoPanel.Controls.Add(Me.LBLScriptReq)
        Me.ScriptInfoPanel.Controls.Add(Me.ScriptsRequirementsGroup)
        Me.ScriptInfoPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ScriptInfoPanel.Location = New System.Drawing.Point(449, 0)
        Me.ScriptInfoPanel.Name = "ScriptInfoPanel"
        Me.ScriptInfoPanel.Size = New System.Drawing.Size(515, 357)
        Me.ScriptInfoPanel.TabIndex = 161
        '
        'ScriptsDescriptionGroup
        '
        Me.ScriptsDescriptionGroup.Controls.Add(Me.ScriptInfoTextArea)
        Me.ScriptsDescriptionGroup.Dock = System.Windows.Forms.DockStyle.Top
        Me.ScriptsDescriptionGroup.Location = New System.Drawing.Point(0, 0)
        Me.ScriptsDescriptionGroup.Name = "ScriptsDescriptionGroup"
        Me.ScriptsDescriptionGroup.Size = New System.Drawing.Size(515, 277)
        Me.ScriptsDescriptionGroup.TabIndex = 153
        Me.ScriptsDescriptionGroup.TabStop = False
        Me.ScriptsDescriptionGroup.Text = "Description"
        '
        'ScriptInfoTextArea
        '
        Me.ScriptInfoTextArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScriptInfoTextArea.Location = New System.Drawing.Point(3, 16)
        Me.ScriptInfoTextArea.Name = "ScriptInfoTextArea"
        Me.ScriptInfoTextArea.ReadOnly = True
        Me.ScriptInfoTextArea.Size = New System.Drawing.Size(509, 258)
        Me.ScriptInfoTextArea.TabIndex = 0
        Me.ScriptInfoTextArea.Text = ""
        '
        'LBLScriptReq
        '
        Me.LBLScriptReq.BackColor = System.Drawing.Color.LightGray
        Me.LBLScriptReq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLScriptReq.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LBLScriptReq.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLScriptReq.ForeColor = System.Drawing.Color.Green
        Me.LBLScriptReq.Location = New System.Drawing.Point(0, 108)
        Me.LBLScriptReq.Name = "LBLScriptReq"
        Me.LBLScriptReq.Size = New System.Drawing.Size(515, 27)
        Me.LBLScriptReq.TabIndex = 156
        Me.LBLScriptReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ScriptsRequirementsGroup
        '
        Me.ScriptsRequirementsGroup.Controls.Add(Me.ScriptRequirements)
        Me.ScriptsRequirementsGroup.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ScriptsRequirementsGroup.Location = New System.Drawing.Point(0, 135)
        Me.ScriptsRequirementsGroup.Name = "ScriptsRequirementsGroup"
        Me.ScriptsRequirementsGroup.Size = New System.Drawing.Size(515, 222)
        Me.ScriptsRequirementsGroup.TabIndex = 155
        Me.ScriptsRequirementsGroup.TabStop = False
        Me.ScriptsRequirementsGroup.Text = "Requirements"
        '
        'ScriptRequirements
        '
        Me.ScriptRequirements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScriptRequirements.Location = New System.Drawing.Point(3, 16)
        Me.ScriptRequirements.Name = "ScriptRequirements"
        Me.ScriptRequirements.ReadOnly = True
        Me.ScriptRequirements.Size = New System.Drawing.Size(509, 203)
        Me.ScriptRequirements.TabIndex = 0
        Me.ScriptRequirements.Text = ""
        '
        'GroupBox43
        '
        Me.GroupBox43.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox43.Controls.Add(Me.Label98)
        Me.GroupBox43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox43.ForeColor = System.Drawing.Color.Black
        Me.GroupBox43.Location = New System.Drawing.Point(0, 357)
        Me.GroupBox43.Name = "GroupBox43"
        Me.GroupBox43.Size = New System.Drawing.Size(964, 91)
        Me.GroupBox43.TabIndex = 65
        Me.GroupBox43.TabStop = False
        Me.GroupBox43.Text = "Description"
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.Transparent
        Me.Label98.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.Black
        Me.Label98.Location = New System.Drawing.Point(6, 16)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(680, 73)
        Me.Label98.TabIndex = 62
        Me.Label98.Text = resources.GetString("Label98.Text")
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.Silver
        Me.TabPage7.Controls.Add(Me.GernreImagesTab)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(972, 456)
        Me.TabPage7.TabIndex = 11
        Me.TabPage7.Text = "Images"
        '
        'GernreImagesTab
        '
        Me.GernreImagesTab.Controls.Add(Me.TpImagesUrlFiles)
        Me.GernreImagesTab.Controls.Add(Me.TpImagesGenre)
        Me.GernreImagesTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GernreImagesTab.Location = New System.Drawing.Point(3, 3)
        Me.GernreImagesTab.Name = "GernreImagesTab"
        Me.GernreImagesTab.SelectedIndex = 0
        Me.GernreImagesTab.Size = New System.Drawing.Size(966, 450)
        Me.GernreImagesTab.TabIndex = 154
        '
        'TpImagesUrlFiles
        '
        Me.TpImagesUrlFiles.BackColor = System.Drawing.Color.LightGray
        Me.TpImagesUrlFiles.Controls.Add(Me.PreviewRemoteImagesCheckBox)
        Me.TpImagesUrlFiles.Controls.Add(Me.GroupBox66)
        Me.TpImagesUrlFiles.Controls.Add(Me.BTNURLFilesAll)
        Me.TpImagesUrlFiles.Controls.Add(Me.BTNURLFilesNone)
        Me.TpImagesUrlFiles.Controls.Add(Me.RemoteMediaContainerList)
        Me.TpImagesUrlFiles.Location = New System.Drawing.Point(4, 22)
        Me.TpImagesUrlFiles.Name = "TpImagesUrlFiles"
        Me.TpImagesUrlFiles.Padding = New System.Windows.Forms.Padding(3)
        Me.TpImagesUrlFiles.Size = New System.Drawing.Size(958, 424)
        Me.TpImagesUrlFiles.TabIndex = 0
        Me.TpImagesUrlFiles.Text = "URL Files"
        '
        'PreviewRemoteImagesCheckBox
        '
        Me.PreviewRemoteImagesCheckBox.AutoSize = True
        Me.PreviewRemoteImagesCheckBox.Checked = True
        Me.PreviewRemoteImagesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PreviewRemoteImagesCheckBox.Location = New System.Drawing.Point(463, 500)
        Me.PreviewRemoteImagesCheckBox.Name = "PreviewRemoteImagesCheckBox"
        Me.PreviewRemoteImagesCheckBox.Size = New System.Drawing.Size(240, 17)
        Me.PreviewRemoteImagesCheckBox.TabIndex = 163
        Me.PreviewRemoteImagesCheckBox.Text = "Show Previews When A URL File is Selected"
        Me.PreviewRemoteImagesCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox66
        '
        Me.GroupBox66.Controls.Add(Me.PBURLPreview)
        Me.GroupBox66.Location = New System.Drawing.Point(460, 9)
        Me.GroupBox66.Name = "GroupBox66"
        Me.GroupBox66.Size = New System.Drawing.Size(492, 485)
        Me.GroupBox66.TabIndex = 162
        Me.GroupBox66.TabStop = False
        Me.GroupBox66.Text = "Example Preview"
        '
        'PBURLPreview
        '
        Me.PBURLPreview.BackColor = System.Drawing.Color.Black
        Me.PBURLPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PBURLPreview.Location = New System.Drawing.Point(3, 16)
        Me.PBURLPreview.Name = "PBURLPreview"
        Me.PBURLPreview.Size = New System.Drawing.Size(486, 466)
        Me.PBURLPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PBURLPreview.TabIndex = 0
        Me.PBURLPreview.TabStop = False
        '
        'BTNURLFilesAll
        '
        Me.BTNURLFilesAll.Location = New System.Drawing.Point(463, 525)
        Me.BTNURLFilesAll.Name = "BTNURLFilesAll"
        Me.BTNURLFilesAll.Size = New System.Drawing.Size(75, 23)
        Me.BTNURLFilesAll.TabIndex = 160
        Me.BTNURLFilesAll.Text = "Select All"
        Me.BTNURLFilesAll.UseVisualStyleBackColor = True
        '
        'BTNURLFilesNone
        '
        Me.BTNURLFilesNone.Location = New System.Drawing.Point(463, 557)
        Me.BTNURLFilesNone.Name = "BTNURLFilesNone"
        Me.BTNURLFilesNone.Size = New System.Drawing.Size(75, 23)
        Me.BTNURLFilesNone.TabIndex = 161
        Me.BTNURLFilesNone.Text = "Select None"
        Me.BTNURLFilesNone.UseVisualStyleBackColor = True
        '
        'RemoteMediaContainerList
        '
        Me.RemoteMediaContainerList.CheckOnClick = True
        Me.RemoteMediaContainerList.FormattingEnabled = True
        Me.RemoteMediaContainerList.Location = New System.Drawing.Point(6, 9)
        Me.RemoteMediaContainerList.Name = "RemoteMediaContainerList"
        Me.RemoteMediaContainerList.Size = New System.Drawing.Size(448, 589)
        Me.RemoteMediaContainerList.Sorted = True
        Me.RemoteMediaContainerList.TabIndex = 154
        '
        'TpImagesGenre
        '
        Me.TpImagesGenre.BackColor = System.Drawing.Color.LightGray
        Me.TpImagesGenre.Controls.Add(Me.GrbImageUrlFiles)
        Me.TpImagesGenre.Controls.Add(Me.GbxImagesGenre)
        Me.TpImagesGenre.Location = New System.Drawing.Point(4, 22)
        Me.TpImagesGenre.Name = "TpImagesGenre"
        Me.TpImagesGenre.Padding = New System.Windows.Forms.Padding(3)
        Me.TpImagesGenre.Size = New System.Drawing.Size(958, 424)
        Me.TpImagesGenre.TabIndex = 1
        Me.TpImagesGenre.Text = "Genre Images"
        '
        'GrbImageUrlFiles
        '
        Me.GrbImageUrlFiles.Controls.Add(Me.TlpImageUrls)
        Me.GrbImageUrlFiles.Location = New System.Drawing.Point(512, 8)
        Me.GrbImageUrlFiles.Name = "GrbImageUrlFiles"
        Me.GrbImageUrlFiles.Size = New System.Drawing.Size(440, 400)
        Me.GrbImageUrlFiles.TabIndex = 1
        Me.GrbImageUrlFiles.TabStop = False
        Me.GrbImageUrlFiles.Text = "URL Files"
        '
        'TlpImageUrls
        '
        Me.TlpImageUrls.BackColor = System.Drawing.Color.LightGray
        Me.TlpImageUrls.ColumnCount = 3
        Me.TlpImageUrls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpImageUrls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpImageUrls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlButt, 1, 13)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlBoobs, 1, 12)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlBlowjob, 1, 4)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlCaptions, 1, 10)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlHentai, 1, 7)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlGay, 1, 8)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlGeneral, 1, 11)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlHardcore, 1, 0)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlLesbian, 1, 2)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlLezdom, 1, 6)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlMaledom, 1, 9)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlFemdom, 1, 5)
        Me.TlpImageUrls.Controls.Add(Me.BtnImageUrlSoftcore, 1, 1)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlHardcore, 0, 0)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlButts, 0, 13)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlMaledom, 0, 9)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlGay, 0, 8)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlSoftcore, 0, 1)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlBoobs, 0, 12)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlLesbian, 0, 2)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlBlowjob, 0, 4)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlCaptions, 0, 10)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlGeneral, 0, 11)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlFemdom, 0, 5)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlHentai, 0, 7)
        Me.TlpImageUrls.Controls.Add(Me.ChbImageUrlLezdom, 0, 6)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlBlowjob, 2, 4)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlSoftcore, 2, 1)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlLezdom, 2, 6)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlFemdom, 2, 5)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlHardcore, 2, 0)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlHentai, 2, 7)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlGay, 2, 8)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlLesbian, 2, 2)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlMaledom, 2, 9)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlCaptions, 2, 10)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlGeneral, 2, 11)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlBoobs, 2, 12)
        Me.TlpImageUrls.Controls.Add(Me.TxbImageUrlButts, 2, 13)
        Me.TlpImageUrls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TlpImageUrls.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TlpImageUrls.Location = New System.Drawing.Point(3, 16)
        Me.TlpImageUrls.Name = "TlpImageUrls"
        Me.TlpImageUrls.RowCount = 14
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpImageUrls.Size = New System.Drawing.Size(434, 381)
        Me.TlpImageUrls.TabIndex = 0
        '
        'BtnImageUrlButt
        '
        Me.BtnImageUrlButt.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlButt.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BtnImageUrlButt.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlButt.Location = New System.Drawing.Point(76, 348)
        Me.BtnImageUrlButt.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlButt.Name = "BtnImageUrlButt"
        Me.BtnImageUrlButt.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlButt.TabIndex = 38
        Me.BtnImageUrlButt.Text = "1"
        Me.BtnImageUrlButt.UseVisualStyleBackColor = False
        '
        'BtnImageUrlBoobs
        '
        Me.BtnImageUrlBoobs.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlBoobs.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BtnImageUrlBoobs.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlBoobs.Location = New System.Drawing.Point(76, 319)
        Me.BtnImageUrlBoobs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlBoobs.Name = "BtnImageUrlBoobs"
        Me.BtnImageUrlBoobs.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlBoobs.TabIndex = 35
        Me.BtnImageUrlBoobs.Text = "1"
        Me.BtnImageUrlBoobs.UseVisualStyleBackColor = False
        '
        'BtnImageUrlBlowjob
        '
        Me.BtnImageUrlBlowjob.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlBlowjob.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlBlowjob.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlBlowjob.Location = New System.Drawing.Point(76, 87)
        Me.BtnImageUrlBlowjob.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlBlowjob.Name = "BtnImageUrlBlowjob"
        Me.BtnImageUrlBlowjob.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlBlowjob.TabIndex = 11
        Me.BtnImageUrlBlowjob.Text = "1"
        Me.BtnImageUrlBlowjob.UseVisualStyleBackColor = False
        '
        'BtnImageUrlCaptions
        '
        Me.BtnImageUrlCaptions.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlCaptions.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlCaptions.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlCaptions.Location = New System.Drawing.Point(76, 261)
        Me.BtnImageUrlCaptions.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlCaptions.Name = "BtnImageUrlCaptions"
        Me.BtnImageUrlCaptions.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlCaptions.TabIndex = 29
        Me.BtnImageUrlCaptions.Text = "1"
        Me.BtnImageUrlCaptions.UseVisualStyleBackColor = False
        '
        'BtnImageUrlHentai
        '
        Me.BtnImageUrlHentai.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlHentai.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlHentai.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlHentai.Location = New System.Drawing.Point(76, 174)
        Me.BtnImageUrlHentai.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlHentai.Name = "BtnImageUrlHentai"
        Me.BtnImageUrlHentai.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlHentai.TabIndex = 20
        Me.BtnImageUrlHentai.Text = "1"
        Me.BtnImageUrlHentai.UseVisualStyleBackColor = False
        '
        'BtnImageUrlGay
        '
        Me.BtnImageUrlGay.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlGay.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlGay.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlGay.Location = New System.Drawing.Point(76, 203)
        Me.BtnImageUrlGay.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlGay.Name = "BtnImageUrlGay"
        Me.BtnImageUrlGay.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlGay.TabIndex = 23
        Me.BtnImageUrlGay.Text = "1"
        Me.BtnImageUrlGay.UseVisualStyleBackColor = False
        '
        'BtnImageUrlGeneral
        '
        Me.BtnImageUrlGeneral.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlGeneral.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlGeneral.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlGeneral.Location = New System.Drawing.Point(76, 290)
        Me.BtnImageUrlGeneral.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlGeneral.Name = "BtnImageUrlGeneral"
        Me.BtnImageUrlGeneral.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlGeneral.TabIndex = 32
        Me.BtnImageUrlGeneral.Text = "1"
        Me.BtnImageUrlGeneral.UseVisualStyleBackColor = False
        '
        'BtnImageUrlHardcore
        '
        Me.BtnImageUrlHardcore.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlHardcore.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlHardcore.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlHardcore.Location = New System.Drawing.Point(76, 0)
        Me.BtnImageUrlHardcore.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlHardcore.Name = "BtnImageUrlHardcore"
        Me.BtnImageUrlHardcore.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlHardcore.TabIndex = 1
        Me.BtnImageUrlHardcore.Text = "1"
        Me.BtnImageUrlHardcore.UseVisualStyleBackColor = False
        '
        'BtnImageUrlLesbian
        '
        Me.BtnImageUrlLesbian.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlLesbian.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlLesbian.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlLesbian.Location = New System.Drawing.Point(76, 58)
        Me.BtnImageUrlLesbian.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlLesbian.Name = "BtnImageUrlLesbian"
        Me.BtnImageUrlLesbian.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlLesbian.TabIndex = 8
        Me.BtnImageUrlLesbian.Text = "1"
        Me.BtnImageUrlLesbian.UseVisualStyleBackColor = False
        '
        'BtnImageUrlLezdom
        '
        Me.BtnImageUrlLezdom.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlLezdom.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlLezdom.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlLezdom.Location = New System.Drawing.Point(76, 145)
        Me.BtnImageUrlLezdom.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlLezdom.Name = "BtnImageUrlLezdom"
        Me.BtnImageUrlLezdom.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlLezdom.TabIndex = 17
        Me.BtnImageUrlLezdom.Text = "1"
        Me.BtnImageUrlLezdom.UseVisualStyleBackColor = False
        '
        'BtnImageUrlMaledom
        '
        Me.BtnImageUrlMaledom.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlMaledom.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlMaledom.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlMaledom.Location = New System.Drawing.Point(76, 232)
        Me.BtnImageUrlMaledom.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlMaledom.Name = "BtnImageUrlMaledom"
        Me.BtnImageUrlMaledom.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlMaledom.TabIndex = 26
        Me.BtnImageUrlMaledom.Text = "1"
        Me.BtnImageUrlMaledom.UseVisualStyleBackColor = False
        '
        'BtnImageUrlFemdom
        '
        Me.BtnImageUrlFemdom.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlFemdom.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlFemdom.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlFemdom.Location = New System.Drawing.Point(76, 116)
        Me.BtnImageUrlFemdom.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlFemdom.Name = "BtnImageUrlFemdom"
        Me.BtnImageUrlFemdom.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlFemdom.TabIndex = 14
        Me.BtnImageUrlFemdom.Text = "1"
        Me.BtnImageUrlFemdom.UseVisualStyleBackColor = False
        '
        'BtnImageUrlSoftcore
        '
        Me.BtnImageUrlSoftcore.BackColor = System.Drawing.Color.LightGray
        Me.BtnImageUrlSoftcore.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnImageUrlSoftcore.ForeColor = System.Drawing.Color.Black
        Me.BtnImageUrlSoftcore.Location = New System.Drawing.Point(76, 29)
        Me.BtnImageUrlSoftcore.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.BtnImageUrlSoftcore.Name = "BtnImageUrlSoftcore"
        Me.BtnImageUrlSoftcore.Size = New System.Drawing.Size(34, 28)
        Me.BtnImageUrlSoftcore.TabIndex = 5
        Me.BtnImageUrlSoftcore.Text = "1"
        Me.BtnImageUrlSoftcore.UseVisualStyleBackColor = False
        '
        'ChbImageUrlHardcore
        '
        Me.ChbImageUrlHardcore.AutoSize = True
        Me.ChbImageUrlHardcore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlHardcore.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlHardcore.Location = New System.Drawing.Point(3, 3)
        Me.ChbImageUrlHardcore.Name = "ChbImageUrlHardcore"
        Me.ChbImageUrlHardcore.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlHardcore.TabIndex = 0
        Me.ChbImageUrlHardcore.Text = "Hardcore"
        Me.ChbImageUrlHardcore.UseVisualStyleBackColor = True
        '
        'ChbImageUrlButts
        '
        Me.ChbImageUrlButts.AutoSize = True
        Me.ChbImageUrlButts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlButts.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlButts.Location = New System.Drawing.Point(3, 351)
        Me.ChbImageUrlButts.Name = "ChbImageUrlButts"
        Me.ChbImageUrlButts.Size = New System.Drawing.Size(70, 27)
        Me.ChbImageUrlButts.TabIndex = 37
        Me.ChbImageUrlButts.Text = "Butts"
        Me.ChbImageUrlButts.UseVisualStyleBackColor = True
        '
        'ChbImageUrlMaledom
        '
        Me.ChbImageUrlMaledom.AutoSize = True
        Me.ChbImageUrlMaledom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlMaledom.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlMaledom.Location = New System.Drawing.Point(3, 235)
        Me.ChbImageUrlMaledom.Name = "ChbImageUrlMaledom"
        Me.ChbImageUrlMaledom.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlMaledom.TabIndex = 25
        Me.ChbImageUrlMaledom.Text = "Maledom"
        Me.ChbImageUrlMaledom.UseVisualStyleBackColor = True
        '
        'ChbImageUrlGay
        '
        Me.ChbImageUrlGay.AutoSize = True
        Me.ChbImageUrlGay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlGay.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlGay.Location = New System.Drawing.Point(3, 206)
        Me.ChbImageUrlGay.Name = "ChbImageUrlGay"
        Me.ChbImageUrlGay.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlGay.TabIndex = 22
        Me.ChbImageUrlGay.Text = "Gay"
        Me.ChbImageUrlGay.UseVisualStyleBackColor = True
        '
        'ChbImageUrlSoftcore
        '
        Me.ChbImageUrlSoftcore.AutoSize = True
        Me.ChbImageUrlSoftcore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlSoftcore.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlSoftcore.Location = New System.Drawing.Point(3, 32)
        Me.ChbImageUrlSoftcore.Name = "ChbImageUrlSoftcore"
        Me.ChbImageUrlSoftcore.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlSoftcore.TabIndex = 4
        Me.ChbImageUrlSoftcore.Text = "Softcore"
        Me.ChbImageUrlSoftcore.UseVisualStyleBackColor = True
        '
        'ChbImageUrlBoobs
        '
        Me.ChbImageUrlBoobs.AutoSize = True
        Me.ChbImageUrlBoobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlBoobs.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlBoobs.Location = New System.Drawing.Point(3, 322)
        Me.ChbImageUrlBoobs.Name = "ChbImageUrlBoobs"
        Me.ChbImageUrlBoobs.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlBoobs.TabIndex = 34
        Me.ChbImageUrlBoobs.Text = "Boobs"
        Me.ChbImageUrlBoobs.UseVisualStyleBackColor = True
        '
        'ChbImageUrlLesbian
        '
        Me.ChbImageUrlLesbian.AutoSize = True
        Me.ChbImageUrlLesbian.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlLesbian.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlLesbian.Location = New System.Drawing.Point(3, 61)
        Me.ChbImageUrlLesbian.Name = "ChbImageUrlLesbian"
        Me.ChbImageUrlLesbian.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlLesbian.TabIndex = 7
        Me.ChbImageUrlLesbian.Text = "Lesbian"
        Me.ChbImageUrlLesbian.UseVisualStyleBackColor = True
        '
        'ChbImageUrlBlowjob
        '
        Me.ChbImageUrlBlowjob.AutoSize = True
        Me.ChbImageUrlBlowjob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlBlowjob.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlBlowjob.Location = New System.Drawing.Point(3, 90)
        Me.ChbImageUrlBlowjob.Name = "ChbImageUrlBlowjob"
        Me.ChbImageUrlBlowjob.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlBlowjob.TabIndex = 10
        Me.ChbImageUrlBlowjob.Text = "Blowjob"
        Me.ChbImageUrlBlowjob.UseVisualStyleBackColor = True
        '
        'ChbImageUrlCaptions
        '
        Me.ChbImageUrlCaptions.AutoSize = True
        Me.ChbImageUrlCaptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlCaptions.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlCaptions.Location = New System.Drawing.Point(3, 264)
        Me.ChbImageUrlCaptions.Name = "ChbImageUrlCaptions"
        Me.ChbImageUrlCaptions.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlCaptions.TabIndex = 28
        Me.ChbImageUrlCaptions.Text = "Captions"
        Me.ChbImageUrlCaptions.UseVisualStyleBackColor = True
        '
        'ChbImageUrlGeneral
        '
        Me.ChbImageUrlGeneral.AutoSize = True
        Me.ChbImageUrlGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlGeneral.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlGeneral.Location = New System.Drawing.Point(3, 293)
        Me.ChbImageUrlGeneral.Name = "ChbImageUrlGeneral"
        Me.ChbImageUrlGeneral.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlGeneral.TabIndex = 31
        Me.ChbImageUrlGeneral.Text = "General"
        Me.ChbImageUrlGeneral.UseVisualStyleBackColor = True
        '
        'ChbImageUrlFemdom
        '
        Me.ChbImageUrlFemdom.AutoSize = True
        Me.ChbImageUrlFemdom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlFemdom.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlFemdom.Location = New System.Drawing.Point(3, 119)
        Me.ChbImageUrlFemdom.Name = "ChbImageUrlFemdom"
        Me.ChbImageUrlFemdom.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlFemdom.TabIndex = 13
        Me.ChbImageUrlFemdom.Text = "Femdom"
        Me.ChbImageUrlFemdom.UseVisualStyleBackColor = True
        '
        'ChbImageUrlHentai
        '
        Me.ChbImageUrlHentai.AutoSize = True
        Me.ChbImageUrlHentai.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlHentai.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlHentai.Location = New System.Drawing.Point(3, 177)
        Me.ChbImageUrlHentai.Name = "ChbImageUrlHentai"
        Me.ChbImageUrlHentai.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlHentai.TabIndex = 19
        Me.ChbImageUrlHentai.Text = "Hentai"
        Me.ChbImageUrlHentai.UseVisualStyleBackColor = True
        '
        'ChbImageUrlLezdom
        '
        Me.ChbImageUrlLezdom.AutoSize = True
        Me.ChbImageUrlLezdom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChbImageUrlLezdom.ForeColor = System.Drawing.Color.Black
        Me.ChbImageUrlLezdom.Location = New System.Drawing.Point(3, 148)
        Me.ChbImageUrlLezdom.Name = "ChbImageUrlLezdom"
        Me.ChbImageUrlLezdom.Size = New System.Drawing.Size(70, 23)
        Me.ChbImageUrlLezdom.TabIndex = 16
        Me.ChbImageUrlLezdom.Text = "Lezdom"
        Me.ChbImageUrlLezdom.UseVisualStyleBackColor = True
        '
        'TxbImageUrlBlowjob
        '
        Me.TxbImageUrlBlowjob.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlBlowjob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlBlowjob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlBlowjob.Location = New System.Drawing.Point(115, 92)
        Me.TxbImageUrlBlowjob.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlBlowjob.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlBlowjob.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlBlowjob.Name = "TxbImageUrlBlowjob"
        Me.TxbImageUrlBlowjob.ReadOnly = True
        Me.TxbImageUrlBlowjob.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlBlowjob.TabIndex = 12
        '
        'TxbImageUrlSoftcore
        '
        Me.TxbImageUrlSoftcore.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlSoftcore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlSoftcore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlSoftcore.Location = New System.Drawing.Point(115, 34)
        Me.TxbImageUrlSoftcore.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlSoftcore.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlSoftcore.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlSoftcore.Name = "TxbImageUrlSoftcore"
        Me.TxbImageUrlSoftcore.ReadOnly = True
        Me.TxbImageUrlSoftcore.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlSoftcore.TabIndex = 6
        '
        'TxbImageUrlLezdom
        '
        Me.TxbImageUrlLezdom.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlLezdom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlLezdom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlLezdom.Location = New System.Drawing.Point(115, 150)
        Me.TxbImageUrlLezdom.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlLezdom.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlLezdom.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlLezdom.Name = "TxbImageUrlLezdom"
        Me.TxbImageUrlLezdom.ReadOnly = True
        Me.TxbImageUrlLezdom.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlLezdom.TabIndex = 18
        '
        'TxbImageUrlFemdom
        '
        Me.TxbImageUrlFemdom.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlFemdom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlFemdom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlFemdom.Location = New System.Drawing.Point(115, 121)
        Me.TxbImageUrlFemdom.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlFemdom.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlFemdom.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlFemdom.Name = "TxbImageUrlFemdom"
        Me.TxbImageUrlFemdom.ReadOnly = True
        Me.TxbImageUrlFemdom.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlFemdom.TabIndex = 15
        '
        'TxbImageUrlHardcore
        '
        Me.TxbImageUrlHardcore.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlHardcore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlHardcore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlHardcore.Location = New System.Drawing.Point(115, 5)
        Me.TxbImageUrlHardcore.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlHardcore.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlHardcore.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlHardcore.Name = "TxbImageUrlHardcore"
        Me.TxbImageUrlHardcore.ReadOnly = True
        Me.TxbImageUrlHardcore.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlHardcore.TabIndex = 3
        '
        'TxbImageUrlHentai
        '
        Me.TxbImageUrlHentai.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlHentai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlHentai.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlHentai.Location = New System.Drawing.Point(115, 179)
        Me.TxbImageUrlHentai.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlHentai.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlHentai.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlHentai.Name = "TxbImageUrlHentai"
        Me.TxbImageUrlHentai.ReadOnly = True
        Me.TxbImageUrlHentai.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlHentai.TabIndex = 21
        '
        'TxbImageUrlGay
        '
        Me.TxbImageUrlGay.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlGay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlGay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlGay.Location = New System.Drawing.Point(115, 208)
        Me.TxbImageUrlGay.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlGay.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlGay.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlGay.Name = "TxbImageUrlGay"
        Me.TxbImageUrlGay.ReadOnly = True
        Me.TxbImageUrlGay.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlGay.TabIndex = 24
        '
        'TxbImageUrlLesbian
        '
        Me.TxbImageUrlLesbian.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlLesbian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlLesbian.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlLesbian.Location = New System.Drawing.Point(115, 63)
        Me.TxbImageUrlLesbian.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlLesbian.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlLesbian.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlLesbian.Name = "TxbImageUrlLesbian"
        Me.TxbImageUrlLesbian.ReadOnly = True
        Me.TxbImageUrlLesbian.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlLesbian.TabIndex = 9
        '
        'TxbImageUrlMaledom
        '
        Me.TxbImageUrlMaledom.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlMaledom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlMaledom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlMaledom.Location = New System.Drawing.Point(115, 237)
        Me.TxbImageUrlMaledom.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlMaledom.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlMaledom.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlMaledom.Name = "TxbImageUrlMaledom"
        Me.TxbImageUrlMaledom.ReadOnly = True
        Me.TxbImageUrlMaledom.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlMaledom.TabIndex = 27
        '
        'TxbImageUrlCaptions
        '
        Me.TxbImageUrlCaptions.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlCaptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlCaptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlCaptions.Location = New System.Drawing.Point(115, 266)
        Me.TxbImageUrlCaptions.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlCaptions.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlCaptions.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlCaptions.Name = "TxbImageUrlCaptions"
        Me.TxbImageUrlCaptions.ReadOnly = True
        Me.TxbImageUrlCaptions.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlCaptions.TabIndex = 30
        '
        'TxbImageUrlGeneral
        '
        Me.TxbImageUrlGeneral.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlGeneral.Location = New System.Drawing.Point(115, 295)
        Me.TxbImageUrlGeneral.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlGeneral.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlGeneral.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlGeneral.Name = "TxbImageUrlGeneral"
        Me.TxbImageUrlGeneral.ReadOnly = True
        Me.TxbImageUrlGeneral.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlGeneral.TabIndex = 33
        '
        'TxbImageUrlBoobs
        '
        Me.TxbImageUrlBoobs.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlBoobs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlBoobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlBoobs.Location = New System.Drawing.Point(115, 324)
        Me.TxbImageUrlBoobs.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlBoobs.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlBoobs.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlBoobs.Name = "TxbImageUrlBoobs"
        Me.TxbImageUrlBoobs.ReadOnly = True
        Me.TxbImageUrlBoobs.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlBoobs.TabIndex = 36
        '
        'TxbImageUrlButts
        '
        Me.TxbImageUrlButts.BackColor = System.Drawing.Color.LightGray
        Me.TxbImageUrlButts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImageUrlButts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImageUrlButts.Location = New System.Drawing.Point(115, 353)
        Me.TxbImageUrlButts.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.TxbImageUrlButts.MaximumSize = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlButts.MinimumSize = New System.Drawing.Size(182, 17)
        Me.TxbImageUrlButts.Name = "TxbImageUrlButts"
        Me.TxbImageUrlButts.ReadOnly = True
        Me.TxbImageUrlButts.Size = New System.Drawing.Size(300, 17)
        Me.TxbImageUrlButts.TabIndex = 39
        '
        'GbxImagesGenre
        '
        Me.GbxImagesGenre.Controls.Add(Me.TableLayoutPanel1)
        Me.GbxImagesGenre.Location = New System.Drawing.Point(6, 8)
        Me.GbxImagesGenre.Name = "GbxImagesGenre"
        Me.GbxImagesGenre.Size = New System.Drawing.Size(500, 400)
        Me.GbxImagesGenre.TabIndex = 0
        Me.GbxImagesGenre.TabStop = False
        Me.GbxImagesGenre.Text = "Local Images"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHardcoreDirectoryButton, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHardcoreDirectoryTextBox, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHardcoreSubdirectoryCheckBox, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHardcoreEnabledCheckBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalSoftcoreEnabledCheckBox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalSoftcoreDirectoryTextBox, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalButtSubdirectoryCheckBox, 3, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalSoftcoreSubdirectoryCheckBox, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBoobsSubdirectoryCheckBox, 3, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLezdomSubdirectoryCheckBox, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGeneralSubdirectoryCheckBox, 3, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLesbianSubdirectoryCheckBox, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalCaptionsSubdirectoryCheckBox, 3, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLesbianEnabledCheckBox, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalMaledomSubdirectoryCheckBox, 3, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBlowjobEnabledCheckBox, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGaySubdirectoryCheckBox, 3, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHentaiSubdirectoryCheckBox, 3, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBlowjobSubdirectoryCheckBox, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalFemdomSubdirectoryCheckBox, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalButtDirectoryTextBox, 2, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalFemdomEnabledCheckBox, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLesbianDirectoryTextBox, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalSoftcoreDirectoryButton, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLezdomEnabledCheckBox, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBoobsDirectoryTextBox, 2, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHentaiEnabledCheckBox, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBlowjobDirectoryTextBox, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGayEnabledCheckBox, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGeneralDirectoryTextBox, 2, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalMaledomEnabledCheckBox, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalFemdomDirectoryTextBox, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLesbianDirectoryButton, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalCaptionsDirectoryTextBox, 2, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalCaptionsEnabledCheckBox, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLezdomDirectoryTextBox, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalMaledomDirectoryTextBox, 2, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalButtDirectoryButton, 1, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHentaiDirectoryTextBox, 2, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGeneralEnabledCheckBox, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGayDirectoryTextBox, 2, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBoobsEnabledCheckBox, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalButtEnabledCheckBox, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBlowjobDirectoryButton, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalFemdomDirectoryButton, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalBoobsDirectoryButton, 1, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalLezdomDirectoryButton, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalHentaiDirectoryButton, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGayDirectoryButton, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalMaledomDirectoryButton, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalCaptionsDirectoryButton, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.LocalGeneralDirectoryButton, 1, 10)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 13
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(494, 381)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LocalHardcoreDirectoryButton
        '
        Me.LocalHardcoreDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalHardcoreDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalHardcoreDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalHardcoreDirectoryButton.Location = New System.Drawing.Point(76, 0)
        Me.LocalHardcoreDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalHardcoreDirectoryButton.Name = "LocalHardcoreDirectoryButton"
        Me.LocalHardcoreDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalHardcoreDirectoryButton.TabIndex = 1
        Me.LocalHardcoreDirectoryButton.Text = "1"
        Me.LocalHardcoreDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalHardcoreDirectoryTextBox
        '
        Me.LocalHardcoreDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalHardcoreDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalHardcoreDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalHardcoreDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalHardcoreDirectoryTextBox.Location = New System.Drawing.Point(115, 5)
        Me.LocalHardcoreDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalHardcoreDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalHardcoreDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalHardcoreDirectoryTextBox.Name = "LocalHardcoreDirectoryTextBox"
        Me.LocalHardcoreDirectoryTextBox.ReadOnly = True
        Me.LocalHardcoreDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalHardcoreDirectoryTextBox.TabIndex = 2
        '
        'LocalHardcoreSubdirectoryCheckBox
        '
        Me.LocalHardcoreSubdirectoryCheckBox.AutoSize = True
        Me.LocalHardcoreSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalHardcoreSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalHardcoreSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 3)
        Me.LocalHardcoreSubdirectoryCheckBox.Name = "LocalHardcoreSubdirectoryCheckBox"
        Me.LocalHardcoreSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalHardcoreSubdirectoryCheckBox.TabIndex = 3
        Me.LocalHardcoreSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalHardcoreSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalHardcoreEnabledCheckBox
        '
        Me.LocalHardcoreEnabledCheckBox.AutoSize = True
        Me.LocalHardcoreEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalHardcoreEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalHardcoreEnabledCheckBox.Location = New System.Drawing.Point(3, 3)
        Me.LocalHardcoreEnabledCheckBox.Name = "LocalHardcoreEnabledCheckBox"
        Me.LocalHardcoreEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalHardcoreEnabledCheckBox.TabIndex = 0
        Me.LocalHardcoreEnabledCheckBox.Text = "Hardcore"
        Me.LocalHardcoreEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalSoftcoreEnabledCheckBox
        '
        Me.LocalSoftcoreEnabledCheckBox.AutoSize = True
        Me.LocalSoftcoreEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalSoftcoreEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalSoftcoreEnabledCheckBox.Location = New System.Drawing.Point(3, 32)
        Me.LocalSoftcoreEnabledCheckBox.Name = "LocalSoftcoreEnabledCheckBox"
        Me.LocalSoftcoreEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalSoftcoreEnabledCheckBox.TabIndex = 4
        Me.LocalSoftcoreEnabledCheckBox.Text = "Softcore"
        Me.LocalSoftcoreEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalSoftcoreDirectoryTextBox
        '
        Me.LocalSoftcoreDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalSoftcoreDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalSoftcoreDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalSoftcoreDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalSoftcoreDirectoryTextBox.Location = New System.Drawing.Point(115, 34)
        Me.LocalSoftcoreDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalSoftcoreDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalSoftcoreDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalSoftcoreDirectoryTextBox.Name = "LocalSoftcoreDirectoryTextBox"
        Me.LocalSoftcoreDirectoryTextBox.ReadOnly = True
        Me.LocalSoftcoreDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalSoftcoreDirectoryTextBox.TabIndex = 6
        '
        'LocalButtSubdirectoryCheckBox
        '
        Me.LocalButtSubdirectoryCheckBox.AutoSize = True
        Me.LocalButtSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalButtSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalButtSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 351)
        Me.LocalButtSubdirectoryCheckBox.Name = "LocalButtSubdirectoryCheckBox"
        Me.LocalButtSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 27)
        Me.LocalButtSubdirectoryCheckBox.TabIndex = 51
        Me.LocalButtSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalButtSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalSoftcoreSubdirectoryCheckBox
        '
        Me.LocalSoftcoreSubdirectoryCheckBox.AutoSize = True
        Me.LocalSoftcoreSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalSoftcoreSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalSoftcoreSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 32)
        Me.LocalSoftcoreSubdirectoryCheckBox.Name = "LocalSoftcoreSubdirectoryCheckBox"
        Me.LocalSoftcoreSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalSoftcoreSubdirectoryCheckBox.TabIndex = 7
        Me.LocalSoftcoreSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalSoftcoreSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalBoobsSubdirectoryCheckBox
        '
        Me.LocalBoobsSubdirectoryCheckBox.AutoSize = True
        Me.LocalBoobsSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalBoobsSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalBoobsSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 322)
        Me.LocalBoobsSubdirectoryCheckBox.Name = "LocalBoobsSubdirectoryCheckBox"
        Me.LocalBoobsSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalBoobsSubdirectoryCheckBox.TabIndex = 47
        Me.LocalBoobsSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalBoobsSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalLezdomSubdirectoryCheckBox
        '
        Me.LocalLezdomSubdirectoryCheckBox.AutoSize = True
        Me.LocalLezdomSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalLezdomSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalLezdomSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 148)
        Me.LocalLezdomSubdirectoryCheckBox.Name = "LocalLezdomSubdirectoryCheckBox"
        Me.LocalLezdomSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalLezdomSubdirectoryCheckBox.TabIndex = 23
        Me.LocalLezdomSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalLezdomSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalGeneralSubdirectoryCheckBox
        '
        Me.LocalGeneralSubdirectoryCheckBox.AutoSize = True
        Me.LocalGeneralSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalGeneralSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalGeneralSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 293)
        Me.LocalGeneralSubdirectoryCheckBox.Name = "LocalGeneralSubdirectoryCheckBox"
        Me.LocalGeneralSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalGeneralSubdirectoryCheckBox.TabIndex = 43
        Me.LocalGeneralSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalGeneralSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalLesbianSubdirectoryCheckBox
        '
        Me.LocalLesbianSubdirectoryCheckBox.AutoSize = True
        Me.LocalLesbianSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalLesbianSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalLesbianSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 61)
        Me.LocalLesbianSubdirectoryCheckBox.Name = "LocalLesbianSubdirectoryCheckBox"
        Me.LocalLesbianSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalLesbianSubdirectoryCheckBox.TabIndex = 11
        Me.LocalLesbianSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalLesbianSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalCaptionsSubdirectoryCheckBox
        '
        Me.LocalCaptionsSubdirectoryCheckBox.AutoSize = True
        Me.LocalCaptionsSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalCaptionsSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalCaptionsSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 264)
        Me.LocalCaptionsSubdirectoryCheckBox.Name = "LocalCaptionsSubdirectoryCheckBox"
        Me.LocalCaptionsSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalCaptionsSubdirectoryCheckBox.TabIndex = 39
        Me.LocalCaptionsSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalCaptionsSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalLesbianEnabledCheckBox
        '
        Me.LocalLesbianEnabledCheckBox.AutoSize = True
        Me.LocalLesbianEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalLesbianEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalLesbianEnabledCheckBox.Location = New System.Drawing.Point(3, 61)
        Me.LocalLesbianEnabledCheckBox.Name = "LocalLesbianEnabledCheckBox"
        Me.LocalLesbianEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalLesbianEnabledCheckBox.TabIndex = 8
        Me.LocalLesbianEnabledCheckBox.Text = "Lesbian"
        Me.LocalLesbianEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalMaledomSubdirectoryCheckBox
        '
        Me.LocalMaledomSubdirectoryCheckBox.AutoSize = True
        Me.LocalMaledomSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalMaledomSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalMaledomSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 235)
        Me.LocalMaledomSubdirectoryCheckBox.Name = "LocalMaledomSubdirectoryCheckBox"
        Me.LocalMaledomSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalMaledomSubdirectoryCheckBox.TabIndex = 35
        Me.LocalMaledomSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalMaledomSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalBlowjobEnabledCheckBox
        '
        Me.LocalBlowjobEnabledCheckBox.AutoSize = True
        Me.LocalBlowjobEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalBlowjobEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalBlowjobEnabledCheckBox.Location = New System.Drawing.Point(3, 90)
        Me.LocalBlowjobEnabledCheckBox.Name = "LocalBlowjobEnabledCheckBox"
        Me.LocalBlowjobEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalBlowjobEnabledCheckBox.TabIndex = 12
        Me.LocalBlowjobEnabledCheckBox.Text = "Blowjob"
        Me.LocalBlowjobEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalGaySubdirectoryCheckBox
        '
        Me.LocalGaySubdirectoryCheckBox.AutoSize = True
        Me.LocalGaySubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalGaySubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalGaySubdirectoryCheckBox.Location = New System.Drawing.Point(343, 206)
        Me.LocalGaySubdirectoryCheckBox.Name = "LocalGaySubdirectoryCheckBox"
        Me.LocalGaySubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalGaySubdirectoryCheckBox.TabIndex = 31
        Me.LocalGaySubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalGaySubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalHentaiSubdirectoryCheckBox
        '
        Me.LocalHentaiSubdirectoryCheckBox.AutoSize = True
        Me.LocalHentaiSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalHentaiSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalHentaiSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 177)
        Me.LocalHentaiSubdirectoryCheckBox.Name = "LocalHentaiSubdirectoryCheckBox"
        Me.LocalHentaiSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalHentaiSubdirectoryCheckBox.TabIndex = 27
        Me.LocalHentaiSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalHentaiSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalBlowjobSubdirectoryCheckBox
        '
        Me.LocalBlowjobSubdirectoryCheckBox.AutoSize = True
        Me.LocalBlowjobSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalBlowjobSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalBlowjobSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 90)
        Me.LocalBlowjobSubdirectoryCheckBox.Name = "LocalBlowjobSubdirectoryCheckBox"
        Me.LocalBlowjobSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalBlowjobSubdirectoryCheckBox.TabIndex = 15
        Me.LocalBlowjobSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalBlowjobSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalFemdomSubdirectoryCheckBox
        '
        Me.LocalFemdomSubdirectoryCheckBox.AutoSize = True
        Me.LocalFemdomSubdirectoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalFemdomSubdirectoryCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalFemdomSubdirectoryCheckBox.Location = New System.Drawing.Point(343, 119)
        Me.LocalFemdomSubdirectoryCheckBox.Name = "LocalFemdomSubdirectoryCheckBox"
        Me.LocalFemdomSubdirectoryCheckBox.Size = New System.Drawing.Size(148, 23)
        Me.LocalFemdomSubdirectoryCheckBox.TabIndex = 19
        Me.LocalFemdomSubdirectoryCheckBox.Text = "Include Subdirectories"
        Me.LocalFemdomSubdirectoryCheckBox.UseVisualStyleBackColor = True
        '
        'LocalButtDirectoryTextBox
        '
        Me.LocalButtDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalButtDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalButtDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalButtDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalButtDirectoryTextBox.Location = New System.Drawing.Point(115, 353)
        Me.LocalButtDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalButtDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalButtDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalButtDirectoryTextBox.Name = "LocalButtDirectoryTextBox"
        Me.LocalButtDirectoryTextBox.ReadOnly = True
        Me.LocalButtDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalButtDirectoryTextBox.TabIndex = 50
        '
        'LocalFemdomEnabledCheckBox
        '
        Me.LocalFemdomEnabledCheckBox.AutoSize = True
        Me.LocalFemdomEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalFemdomEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalFemdomEnabledCheckBox.Location = New System.Drawing.Point(3, 119)
        Me.LocalFemdomEnabledCheckBox.Name = "LocalFemdomEnabledCheckBox"
        Me.LocalFemdomEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalFemdomEnabledCheckBox.TabIndex = 16
        Me.LocalFemdomEnabledCheckBox.Text = "Femdom"
        Me.LocalFemdomEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalLesbianDirectoryTextBox
        '
        Me.LocalLesbianDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalLesbianDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalLesbianDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalLesbianDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalLesbianDirectoryTextBox.Location = New System.Drawing.Point(115, 63)
        Me.LocalLesbianDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalLesbianDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalLesbianDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalLesbianDirectoryTextBox.Name = "LocalLesbianDirectoryTextBox"
        Me.LocalLesbianDirectoryTextBox.ReadOnly = True
        Me.LocalLesbianDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalLesbianDirectoryTextBox.TabIndex = 10
        '
        'LocalSoftcoreDirectoryButton
        '
        Me.LocalSoftcoreDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalSoftcoreDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalSoftcoreDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalSoftcoreDirectoryButton.Location = New System.Drawing.Point(76, 29)
        Me.LocalSoftcoreDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalSoftcoreDirectoryButton.Name = "LocalSoftcoreDirectoryButton"
        Me.LocalSoftcoreDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalSoftcoreDirectoryButton.TabIndex = 5
        Me.LocalSoftcoreDirectoryButton.Text = "1"
        Me.LocalSoftcoreDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalLezdomEnabledCheckBox
        '
        Me.LocalLezdomEnabledCheckBox.AutoSize = True
        Me.LocalLezdomEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalLezdomEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalLezdomEnabledCheckBox.Location = New System.Drawing.Point(3, 148)
        Me.LocalLezdomEnabledCheckBox.Name = "LocalLezdomEnabledCheckBox"
        Me.LocalLezdomEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalLezdomEnabledCheckBox.TabIndex = 20
        Me.LocalLezdomEnabledCheckBox.Text = "Lezdom"
        Me.LocalLezdomEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalBoobsDirectoryTextBox
        '
        Me.LocalBoobsDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalBoobsDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalBoobsDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalBoobsDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalBoobsDirectoryTextBox.Location = New System.Drawing.Point(115, 324)
        Me.LocalBoobsDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalBoobsDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalBoobsDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalBoobsDirectoryTextBox.Name = "LocalBoobsDirectoryTextBox"
        Me.LocalBoobsDirectoryTextBox.ReadOnly = True
        Me.LocalBoobsDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalBoobsDirectoryTextBox.TabIndex = 46
        '
        'LocalHentaiEnabledCheckBox
        '
        Me.LocalHentaiEnabledCheckBox.AutoSize = True
        Me.LocalHentaiEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalHentaiEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalHentaiEnabledCheckBox.Location = New System.Drawing.Point(3, 177)
        Me.LocalHentaiEnabledCheckBox.Name = "LocalHentaiEnabledCheckBox"
        Me.LocalHentaiEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalHentaiEnabledCheckBox.TabIndex = 24
        Me.LocalHentaiEnabledCheckBox.Text = "Hentai"
        Me.LocalHentaiEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalBlowjobDirectoryTextBox
        '
        Me.LocalBlowjobDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalBlowjobDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalBlowjobDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalBlowjobDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalBlowjobDirectoryTextBox.Location = New System.Drawing.Point(115, 92)
        Me.LocalBlowjobDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalBlowjobDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalBlowjobDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalBlowjobDirectoryTextBox.Name = "LocalBlowjobDirectoryTextBox"
        Me.LocalBlowjobDirectoryTextBox.ReadOnly = True
        Me.LocalBlowjobDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalBlowjobDirectoryTextBox.TabIndex = 14
        '
        'LocalGayEnabledCheckBox
        '
        Me.LocalGayEnabledCheckBox.AutoSize = True
        Me.LocalGayEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalGayEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalGayEnabledCheckBox.Location = New System.Drawing.Point(3, 206)
        Me.LocalGayEnabledCheckBox.Name = "LocalGayEnabledCheckBox"
        Me.LocalGayEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalGayEnabledCheckBox.TabIndex = 28
        Me.LocalGayEnabledCheckBox.Text = "Gay"
        Me.LocalGayEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalGeneralDirectoryTextBox
        '
        Me.LocalGeneralDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalGeneralDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalGeneralDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalGeneralDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalGeneralDirectoryTextBox.Location = New System.Drawing.Point(115, 295)
        Me.LocalGeneralDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalGeneralDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalGeneralDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalGeneralDirectoryTextBox.Name = "LocalGeneralDirectoryTextBox"
        Me.LocalGeneralDirectoryTextBox.ReadOnly = True
        Me.LocalGeneralDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalGeneralDirectoryTextBox.TabIndex = 42
        '
        'LocalMaledomEnabledCheckBox
        '
        Me.LocalMaledomEnabledCheckBox.AutoSize = True
        Me.LocalMaledomEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalMaledomEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalMaledomEnabledCheckBox.Location = New System.Drawing.Point(3, 235)
        Me.LocalMaledomEnabledCheckBox.Name = "LocalMaledomEnabledCheckBox"
        Me.LocalMaledomEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalMaledomEnabledCheckBox.TabIndex = 32
        Me.LocalMaledomEnabledCheckBox.Text = "Maledom"
        Me.LocalMaledomEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalFemdomDirectoryTextBox
        '
        Me.LocalFemdomDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalFemdomDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalFemdomDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalFemdomDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalFemdomDirectoryTextBox.Location = New System.Drawing.Point(115, 121)
        Me.LocalFemdomDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalFemdomDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalFemdomDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalFemdomDirectoryTextBox.Name = "LocalFemdomDirectoryTextBox"
        Me.LocalFemdomDirectoryTextBox.ReadOnly = True
        Me.LocalFemdomDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalFemdomDirectoryTextBox.TabIndex = 18
        '
        'LocalLesbianDirectoryButton
        '
        Me.LocalLesbianDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalLesbianDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalLesbianDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalLesbianDirectoryButton.Location = New System.Drawing.Point(76, 58)
        Me.LocalLesbianDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalLesbianDirectoryButton.Name = "LocalLesbianDirectoryButton"
        Me.LocalLesbianDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalLesbianDirectoryButton.TabIndex = 9
        Me.LocalLesbianDirectoryButton.Text = "1"
        Me.LocalLesbianDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalCaptionsDirectoryTextBox
        '
        Me.LocalCaptionsDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalCaptionsDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalCaptionsDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalCaptionsDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalCaptionsDirectoryTextBox.Location = New System.Drawing.Point(115, 266)
        Me.LocalCaptionsDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalCaptionsDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalCaptionsDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalCaptionsDirectoryTextBox.Name = "LocalCaptionsDirectoryTextBox"
        Me.LocalCaptionsDirectoryTextBox.ReadOnly = True
        Me.LocalCaptionsDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalCaptionsDirectoryTextBox.TabIndex = 38
        '
        'LocalCaptionsEnabledCheckBox
        '
        Me.LocalCaptionsEnabledCheckBox.AutoSize = True
        Me.LocalCaptionsEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalCaptionsEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalCaptionsEnabledCheckBox.Location = New System.Drawing.Point(3, 264)
        Me.LocalCaptionsEnabledCheckBox.Name = "LocalCaptionsEnabledCheckBox"
        Me.LocalCaptionsEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalCaptionsEnabledCheckBox.TabIndex = 36
        Me.LocalCaptionsEnabledCheckBox.Text = "Captions"
        Me.LocalCaptionsEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalLezdomDirectoryTextBox
        '
        Me.LocalLezdomDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalLezdomDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalLezdomDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalLezdomDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalLezdomDirectoryTextBox.Location = New System.Drawing.Point(115, 150)
        Me.LocalLezdomDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalLezdomDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalLezdomDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalLezdomDirectoryTextBox.Name = "LocalLezdomDirectoryTextBox"
        Me.LocalLezdomDirectoryTextBox.ReadOnly = True
        Me.LocalLezdomDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalLezdomDirectoryTextBox.TabIndex = 22
        '
        'LocalMaledomDirectoryTextBox
        '
        Me.LocalMaledomDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalMaledomDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalMaledomDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalMaledomDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalMaledomDirectoryTextBox.Location = New System.Drawing.Point(115, 237)
        Me.LocalMaledomDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalMaledomDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalMaledomDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalMaledomDirectoryTextBox.Name = "LocalMaledomDirectoryTextBox"
        Me.LocalMaledomDirectoryTextBox.ReadOnly = True
        Me.LocalMaledomDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalMaledomDirectoryTextBox.TabIndex = 34
        '
        'LocalButtDirectoryButton
        '
        Me.LocalButtDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalButtDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalButtDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalButtDirectoryButton.Location = New System.Drawing.Point(76, 348)
        Me.LocalButtDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalButtDirectoryButton.Name = "LocalButtDirectoryButton"
        Me.LocalButtDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalButtDirectoryButton.TabIndex = 49
        Me.LocalButtDirectoryButton.Text = "1"
        Me.LocalButtDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalHentaiDirectoryTextBox
        '
        Me.LocalHentaiDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalHentaiDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalHentaiDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalHentaiDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalHentaiDirectoryTextBox.Location = New System.Drawing.Point(115, 179)
        Me.LocalHentaiDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalHentaiDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalHentaiDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalHentaiDirectoryTextBox.Name = "LocalHentaiDirectoryTextBox"
        Me.LocalHentaiDirectoryTextBox.ReadOnly = True
        Me.LocalHentaiDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalHentaiDirectoryTextBox.TabIndex = 26
        '
        'LocalGeneralEnabledCheckBox
        '
        Me.LocalGeneralEnabledCheckBox.AutoSize = True
        Me.LocalGeneralEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalGeneralEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalGeneralEnabledCheckBox.Location = New System.Drawing.Point(3, 293)
        Me.LocalGeneralEnabledCheckBox.Name = "LocalGeneralEnabledCheckBox"
        Me.LocalGeneralEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalGeneralEnabledCheckBox.TabIndex = 40
        Me.LocalGeneralEnabledCheckBox.Text = "General"
        Me.LocalGeneralEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalGayDirectoryTextBox
        '
        Me.LocalGayDirectoryTextBox.BackColor = System.Drawing.Color.LightGray
        Me.LocalGayDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LocalGayDirectoryTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocalGayDirectoryTextBox.ForeColor = System.Drawing.Color.Black
        Me.LocalGayDirectoryTextBox.Location = New System.Drawing.Point(115, 208)
        Me.LocalGayDirectoryTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 8, 3)
        Me.LocalGayDirectoryTextBox.MaximumSize = New System.Drawing.Size(2, 17)
        Me.LocalGayDirectoryTextBox.MinimumSize = New System.Drawing.Size(217, 17)
        Me.LocalGayDirectoryTextBox.Name = "LocalGayDirectoryTextBox"
        Me.LocalGayDirectoryTextBox.ReadOnly = True
        Me.LocalGayDirectoryTextBox.Size = New System.Drawing.Size(217, 17)
        Me.LocalGayDirectoryTextBox.TabIndex = 30
        '
        'LocalBoobsEnabledCheckBox
        '
        Me.LocalBoobsEnabledCheckBox.AutoSize = True
        Me.LocalBoobsEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalBoobsEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalBoobsEnabledCheckBox.Location = New System.Drawing.Point(3, 322)
        Me.LocalBoobsEnabledCheckBox.Name = "LocalBoobsEnabledCheckBox"
        Me.LocalBoobsEnabledCheckBox.Size = New System.Drawing.Size(70, 23)
        Me.LocalBoobsEnabledCheckBox.TabIndex = 44
        Me.LocalBoobsEnabledCheckBox.Text = "Boobs"
        Me.LocalBoobsEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalButtEnabledCheckBox
        '
        Me.LocalButtEnabledCheckBox.AutoSize = True
        Me.LocalButtEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalButtEnabledCheckBox.ForeColor = System.Drawing.Color.Black
        Me.LocalButtEnabledCheckBox.Location = New System.Drawing.Point(3, 351)
        Me.LocalButtEnabledCheckBox.Name = "LocalButtEnabledCheckBox"
        Me.LocalButtEnabledCheckBox.Size = New System.Drawing.Size(70, 27)
        Me.LocalButtEnabledCheckBox.TabIndex = 48
        Me.LocalButtEnabledCheckBox.Text = "Butts"
        Me.LocalButtEnabledCheckBox.UseVisualStyleBackColor = True
        '
        'LocalBlowjobDirectoryButton
        '
        Me.LocalBlowjobDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalBlowjobDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalBlowjobDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalBlowjobDirectoryButton.Location = New System.Drawing.Point(76, 87)
        Me.LocalBlowjobDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalBlowjobDirectoryButton.Name = "LocalBlowjobDirectoryButton"
        Me.LocalBlowjobDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalBlowjobDirectoryButton.TabIndex = 13
        Me.LocalBlowjobDirectoryButton.Text = "1"
        Me.LocalBlowjobDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalFemdomDirectoryButton
        '
        Me.LocalFemdomDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalFemdomDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalFemdomDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalFemdomDirectoryButton.Location = New System.Drawing.Point(76, 116)
        Me.LocalFemdomDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalFemdomDirectoryButton.Name = "LocalFemdomDirectoryButton"
        Me.LocalFemdomDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalFemdomDirectoryButton.TabIndex = 17
        Me.LocalFemdomDirectoryButton.Text = "1"
        Me.LocalFemdomDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalBoobsDirectoryButton
        '
        Me.LocalBoobsDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalBoobsDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalBoobsDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalBoobsDirectoryButton.Location = New System.Drawing.Point(76, 319)
        Me.LocalBoobsDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalBoobsDirectoryButton.Name = "LocalBoobsDirectoryButton"
        Me.LocalBoobsDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalBoobsDirectoryButton.TabIndex = 45
        Me.LocalBoobsDirectoryButton.Text = "1"
        Me.LocalBoobsDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalLezdomDirectoryButton
        '
        Me.LocalLezdomDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalLezdomDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalLezdomDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalLezdomDirectoryButton.Location = New System.Drawing.Point(76, 145)
        Me.LocalLezdomDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalLezdomDirectoryButton.Name = "LocalLezdomDirectoryButton"
        Me.LocalLezdomDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalLezdomDirectoryButton.TabIndex = 21
        Me.LocalLezdomDirectoryButton.Text = "1"
        Me.LocalLezdomDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalHentaiDirectoryButton
        '
        Me.LocalHentaiDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalHentaiDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalHentaiDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalHentaiDirectoryButton.Location = New System.Drawing.Point(76, 174)
        Me.LocalHentaiDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalHentaiDirectoryButton.Name = "LocalHentaiDirectoryButton"
        Me.LocalHentaiDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalHentaiDirectoryButton.TabIndex = 25
        Me.LocalHentaiDirectoryButton.Text = "1"
        Me.LocalHentaiDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalGayDirectoryButton
        '
        Me.LocalGayDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalGayDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalGayDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalGayDirectoryButton.Location = New System.Drawing.Point(76, 203)
        Me.LocalGayDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalGayDirectoryButton.Name = "LocalGayDirectoryButton"
        Me.LocalGayDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalGayDirectoryButton.TabIndex = 29
        Me.LocalGayDirectoryButton.Text = "1"
        Me.LocalGayDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalMaledomDirectoryButton
        '
        Me.LocalMaledomDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalMaledomDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalMaledomDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalMaledomDirectoryButton.Location = New System.Drawing.Point(76, 232)
        Me.LocalMaledomDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalMaledomDirectoryButton.Name = "LocalMaledomDirectoryButton"
        Me.LocalMaledomDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalMaledomDirectoryButton.TabIndex = 33
        Me.LocalMaledomDirectoryButton.Text = "1"
        Me.LocalMaledomDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalCaptionsDirectoryButton
        '
        Me.LocalCaptionsDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalCaptionsDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalCaptionsDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalCaptionsDirectoryButton.Location = New System.Drawing.Point(76, 261)
        Me.LocalCaptionsDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalCaptionsDirectoryButton.Name = "LocalCaptionsDirectoryButton"
        Me.LocalCaptionsDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalCaptionsDirectoryButton.TabIndex = 37
        Me.LocalCaptionsDirectoryButton.Text = "1"
        Me.LocalCaptionsDirectoryButton.UseVisualStyleBackColor = False
        '
        'LocalGeneralDirectoryButton
        '
        Me.LocalGeneralDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.LocalGeneralDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LocalGeneralDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.LocalGeneralDirectoryButton.Location = New System.Drawing.Point(76, 290)
        Me.LocalGeneralDirectoryButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.LocalGeneralDirectoryButton.Name = "LocalGeneralDirectoryButton"
        Me.LocalGeneralDirectoryButton.Size = New System.Drawing.Size(34, 28)
        Me.LocalGeneralDirectoryButton.TabIndex = 41
        Me.LocalGeneralDirectoryButton.Text = "1"
        Me.LocalGeneralDirectoryButton.UseVisualStyleBackColor = False
        '
        'TabPage33
        '
        Me.TabPage33.BackColor = System.Drawing.Color.Silver
        Me.TabPage33.Controls.Add(Me.LocalTagsTab)
        Me.TabPage33.Location = New System.Drawing.Point(4, 22)
        Me.TabPage33.Name = "TabPage33"
        Me.TabPage33.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage33.Size = New System.Drawing.Size(972, 456)
        Me.TabPage33.TabIndex = 21
        Me.TabPage33.Text = "Tagging"
        '
        'LocalTagsTab
        '
        Me.LocalTagsTab.Controls.Add(Me.TabPage34)
        Me.LocalTagsTab.Controls.Add(Me.FileDropDownLabel)
        Me.LocalTagsTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LocalTagsTab.Location = New System.Drawing.Point(3, 3)
        Me.LocalTagsTab.Name = "LocalTagsTab"
        Me.LocalTagsTab.SelectedIndex = 0
        Me.LocalTagsTab.Size = New System.Drawing.Size(966, 450)
        Me.LocalTagsTab.TabIndex = 0
        '
        'TabPage34
        '
        Me.TabPage34.BackColor = System.Drawing.Color.LightGray
        Me.TabPage34.Controls.Add(Me.CBTagSeeThrough)
        Me.TabPage34.Controls.Add(Me.CBTagAllFours)
        Me.TabPage34.Controls.Add(Me.CBTagGlaring)
        Me.TabPage34.Controls.Add(Me.CBTagSmiling)
        Me.TabPage34.Controls.Add(Me.DommeTagDirInput)
        Me.TabPage34.Controls.Add(Me.CBTagPiercing)
        Me.TabPage34.Controls.Add(Me.CBTagLegs)
        Me.TabPage34.Controls.Add(Me.TBTagFurniture)
        Me.TabPage34.Controls.Add(Me.CBTagFurniture)
        Me.TabPage34.Controls.Add(Me.TBTagSexToy)
        Me.TabPage34.Controls.Add(Me.CBTagSexToy)
        Me.TabPage34.Controls.Add(Me.TBTagTattoo)
        Me.TabPage34.Controls.Add(Me.CBTagTattoo)
        Me.TabPage34.Controls.Add(Me.TBTagUnderwear)
        Me.TabPage34.Controls.Add(Me.CBTagUnderwear)
        Me.TabPage34.Controls.Add(Me.TBTagGarment)
        Me.TabPage34.Controls.Add(Me.CBTagGarment)
        Me.TabPage34.Controls.Add(Me.Label72)
        Me.TabPage34.Controls.Add(Me.CBTagHandsCovering)
        Me.TabPage34.Controls.Add(Me.CBTagGarmentCovering)
        Me.TabPage34.Controls.Add(Me.CBTagCloseUp)
        Me.TabPage34.Controls.Add(Me.CBTagNaked)
        Me.TabPage34.Controls.Add(Me.CBTagSideView)
        Me.TabPage34.Controls.Add(Me.BTNTagPrevious)
        Me.TabPage34.Controls.Add(Me.CBTagHalfDressed)
        Me.TabPage34.Controls.Add(Me.BTNTagNext)
        Me.TabPage34.Controls.Add(Me.CBTagFullyDressed)
        Me.TabPage34.Controls.Add(Me.LBLTagCount)
        Me.TabPage34.Controls.Add(Me.CBTagSucking)
        Me.TabPage34.Controls.Add(Me.CBTagMasturbating)
        Me.TabPage34.Controls.Add(Me.CBTagFeet)
        Me.TabPage34.Controls.Add(Me.CBTagBoobs)
        Me.TabPage34.Controls.Add(Me.CBTagAss)
        Me.TabPage34.Controls.Add(Me.CBTagPussy)
        Me.TabPage34.Controls.Add(Me.BTNTagSave)
        Me.TabPage34.Controls.Add(Me.DommeTagDirectoryButton)
        Me.TabPage34.Controls.Add(Me.ImageTagPictureBox)
        Me.TabPage34.Controls.Add(Me.CBTagFace)
        Me.TabPage34.Location = New System.Drawing.Point(4, 22)
        Me.TabPage34.Name = "TabPage34"
        Me.TabPage34.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage34.Size = New System.Drawing.Size(958, 424)
        Me.TabPage34.TabIndex = 0
        Me.TabPage34.Text = "Domme Tags"
        '
        'CBTagSeeThrough
        '
        Me.CBTagSeeThrough.AutoSize = True
        Me.CBTagSeeThrough.Enabled = False
        Me.CBTagSeeThrough.Location = New System.Drawing.Point(577, 117)
        Me.CBTagSeeThrough.Name = "CBTagSeeThrough"
        Me.CBTagSeeThrough.Size = New System.Drawing.Size(87, 17)
        Me.CBTagSeeThrough.TabIndex = 226
        Me.CBTagSeeThrough.Text = "See Through"
        Me.CBTagSeeThrough.UseVisualStyleBackColor = True
        '
        'CBTagAllFours
        '
        Me.CBTagAllFours.AutoSize = True
        Me.CBTagAllFours.Enabled = False
        Me.CBTagAllFours.ForeColor = System.Drawing.Color.Black
        Me.CBTagAllFours.Location = New System.Drawing.Point(577, 207)
        Me.CBTagAllFours.Name = "CBTagAllFours"
        Me.CBTagAllFours.Size = New System.Drawing.Size(66, 17)
        Me.CBTagAllFours.TabIndex = 225
        Me.CBTagAllFours.Text = "All Fours"
        Me.CBTagAllFours.UseVisualStyleBackColor = True
        '
        'CBTagGlaring
        '
        Me.CBTagGlaring.AutoSize = True
        Me.CBTagGlaring.Enabled = False
        Me.CBTagGlaring.ForeColor = System.Drawing.Color.Black
        Me.CBTagGlaring.Location = New System.Drawing.Point(484, 227)
        Me.CBTagGlaring.Name = "CBTagGlaring"
        Me.CBTagGlaring.Size = New System.Drawing.Size(59, 17)
        Me.CBTagGlaring.TabIndex = 224
        Me.CBTagGlaring.Text = "Glaring"
        Me.CBTagGlaring.UseVisualStyleBackColor = True
        '
        'CBTagSmiling
        '
        Me.CBTagSmiling.AutoSize = True
        Me.CBTagSmiling.Enabled = False
        Me.CBTagSmiling.ForeColor = System.Drawing.Color.Black
        Me.CBTagSmiling.Location = New System.Drawing.Point(484, 207)
        Me.CBTagSmiling.Name = "CBTagSmiling"
        Me.CBTagSmiling.Size = New System.Drawing.Size(59, 17)
        Me.CBTagSmiling.TabIndex = 223
        Me.CBTagSmiling.Text = "Smiling"
        Me.CBTagSmiling.UseVisualStyleBackColor = True
        '
        'DommeTagDirInput
        '
        Me.DommeTagDirInput.Location = New System.Drawing.Point(55, 9)
        Me.DommeTagDirInput.Name = "DommeTagDirInput"
        Me.DommeTagDirInput.Size = New System.Drawing.Size(330, 20)
        Me.DommeTagDirInput.TabIndex = 222
        Me.DommeTagDirInput.Text = "Enter Image Directory"
        '
        'CBTagPiercing
        '
        Me.CBTagPiercing.AutoSize = True
        Me.CBTagPiercing.Enabled = False
        Me.CBTagPiercing.ForeColor = System.Drawing.Color.Black
        Me.CBTagPiercing.Location = New System.Drawing.Point(577, 227)
        Me.CBTagPiercing.Name = "CBTagPiercing"
        Me.CBTagPiercing.Size = New System.Drawing.Size(64, 17)
        Me.CBTagPiercing.TabIndex = 221
        Me.CBTagPiercing.Text = "Piercing"
        Me.CBTagPiercing.UseVisualStyleBackColor = True
        '
        'CBTagLegs
        '
        Me.CBTagLegs.AutoSize = True
        Me.CBTagLegs.Enabled = False
        Me.CBTagLegs.ForeColor = System.Drawing.Color.Black
        Me.CBTagLegs.Location = New System.Drawing.Point(484, 117)
        Me.CBTagLegs.Name = "CBTagLegs"
        Me.CBTagLegs.Size = New System.Drawing.Size(49, 17)
        Me.CBTagLegs.TabIndex = 220
        Me.CBTagLegs.Text = "Legs"
        Me.CBTagLegs.UseVisualStyleBackColor = True
        '
        'TBTagFurniture
        '
        Me.TBTagFurniture.Enabled = False
        Me.TBTagFurniture.Location = New System.Drawing.Point(559, 370)
        Me.TBTagFurniture.Name = "TBTagFurniture"
        Me.TBTagFurniture.Size = New System.Drawing.Size(108, 20)
        Me.TBTagFurniture.TabIndex = 219
        '
        'CBTagFurniture
        '
        Me.CBTagFurniture.AutoSize = True
        Me.CBTagFurniture.Enabled = False
        Me.CBTagFurniture.ForeColor = System.Drawing.Color.Black
        Me.CBTagFurniture.Location = New System.Drawing.Point(476, 372)
        Me.CBTagFurniture.Name = "CBTagFurniture"
        Me.CBTagFurniture.Size = New System.Drawing.Size(67, 17)
        Me.CBTagFurniture.TabIndex = 218
        Me.CBTagFurniture.Text = "Furniture"
        Me.CBTagFurniture.UseVisualStyleBackColor = True
        '
        'TBTagSexToy
        '
        Me.TBTagSexToy.Enabled = False
        Me.TBTagSexToy.Location = New System.Drawing.Point(560, 346)
        Me.TBTagSexToy.Name = "TBTagSexToy"
        Me.TBTagSexToy.Size = New System.Drawing.Size(108, 20)
        Me.TBTagSexToy.TabIndex = 217
        '
        'CBTagSexToy
        '
        Me.CBTagSexToy.AutoSize = True
        Me.CBTagSexToy.Enabled = False
        Me.CBTagSexToy.ForeColor = System.Drawing.Color.Black
        Me.CBTagSexToy.Location = New System.Drawing.Point(476, 348)
        Me.CBTagSexToy.Name = "CBTagSexToy"
        Me.CBTagSexToy.Size = New System.Drawing.Size(65, 17)
        Me.CBTagSexToy.TabIndex = 216
        Me.CBTagSexToy.Text = "Sex Toy"
        Me.CBTagSexToy.UseVisualStyleBackColor = True
        '
        'TBTagTattoo
        '
        Me.TBTagTattoo.Enabled = False
        Me.TBTagTattoo.Location = New System.Drawing.Point(560, 322)
        Me.TBTagTattoo.Name = "TBTagTattoo"
        Me.TBTagTattoo.Size = New System.Drawing.Size(108, 20)
        Me.TBTagTattoo.TabIndex = 215
        '
        'CBTagTattoo
        '
        Me.CBTagTattoo.AutoSize = True
        Me.CBTagTattoo.Enabled = False
        Me.CBTagTattoo.ForeColor = System.Drawing.Color.Black
        Me.CBTagTattoo.Location = New System.Drawing.Point(476, 324)
        Me.CBTagTattoo.Name = "CBTagTattoo"
        Me.CBTagTattoo.Size = New System.Drawing.Size(57, 17)
        Me.CBTagTattoo.TabIndex = 214
        Me.CBTagTattoo.Text = "Tattoo"
        Me.CBTagTattoo.UseVisualStyleBackColor = True
        '
        'TBTagUnderwear
        '
        Me.TBTagUnderwear.Enabled = False
        Me.TBTagUnderwear.Location = New System.Drawing.Point(560, 298)
        Me.TBTagUnderwear.Name = "TBTagUnderwear"
        Me.TBTagUnderwear.Size = New System.Drawing.Size(108, 20)
        Me.TBTagUnderwear.TabIndex = 213
        '
        'CBTagUnderwear
        '
        Me.CBTagUnderwear.AutoSize = True
        Me.CBTagUnderwear.Enabled = False
        Me.CBTagUnderwear.ForeColor = System.Drawing.Color.Black
        Me.CBTagUnderwear.Location = New System.Drawing.Point(476, 300)
        Me.CBTagUnderwear.Name = "CBTagUnderwear"
        Me.CBTagUnderwear.Size = New System.Drawing.Size(78, 17)
        Me.CBTagUnderwear.TabIndex = 212
        Me.CBTagUnderwear.Text = "Underwear"
        Me.CBTagUnderwear.UseVisualStyleBackColor = True
        '
        'TBTagGarment
        '
        Me.TBTagGarment.Enabled = False
        Me.TBTagGarment.Location = New System.Drawing.Point(560, 274)
        Me.TBTagGarment.Name = "TBTagGarment"
        Me.TBTagGarment.Size = New System.Drawing.Size(108, 20)
        Me.TBTagGarment.TabIndex = 211
        '
        'CBTagGarment
        '
        Me.CBTagGarment.AutoSize = True
        Me.CBTagGarment.Enabled = False
        Me.CBTagGarment.ForeColor = System.Drawing.Color.Black
        Me.CBTagGarment.Location = New System.Drawing.Point(476, 276)
        Me.CBTagGarment.Name = "CBTagGarment"
        Me.CBTagGarment.Size = New System.Drawing.Size(66, 17)
        Me.CBTagGarment.TabIndex = 210
        Me.CBTagGarment.Text = "Garment"
        Me.CBTagGarment.UseVisualStyleBackColor = True
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.Transparent
        Me.Label72.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.Black
        Me.Label72.Location = New System.Drawing.Point(5, 368)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(451, 35)
        Me.Label72.TabIndex = 189
        Me.Label72.Text = "Open a directory containing images. Check all tags that apply to each image displ" &
    "ayed, and enter one-word tag descriptions in the text fields when appropriate. (" &
    "e.g. Garment: dress)"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBTagHandsCovering
        '
        Me.CBTagHandsCovering.AutoSize = True
        Me.CBTagHandsCovering.Enabled = False
        Me.CBTagHandsCovering.Location = New System.Drawing.Point(577, 97)
        Me.CBTagHandsCovering.Name = "CBTagHandsCovering"
        Me.CBTagHandsCovering.Size = New System.Drawing.Size(101, 17)
        Me.CBTagHandsCovering.TabIndex = 209
        Me.CBTagHandsCovering.Text = "Hands Covering"
        Me.CBTagHandsCovering.UseVisualStyleBackColor = True
        '
        'CBTagGarmentCovering
        '
        Me.CBTagGarmentCovering.AutoSize = True
        Me.CBTagGarmentCovering.Enabled = False
        Me.CBTagGarmentCovering.Location = New System.Drawing.Point(577, 77)
        Me.CBTagGarmentCovering.Name = "CBTagGarmentCovering"
        Me.CBTagGarmentCovering.Size = New System.Drawing.Size(110, 17)
        Me.CBTagGarmentCovering.TabIndex = 208
        Me.CBTagGarmentCovering.Text = "Garment Covering"
        Me.CBTagGarmentCovering.UseVisualStyleBackColor = True
        '
        'CBTagCloseUp
        '
        Me.CBTagCloseUp.AutoSize = True
        Me.CBTagCloseUp.Enabled = False
        Me.CBTagCloseUp.ForeColor = System.Drawing.Color.Black
        Me.CBTagCloseUp.Location = New System.Drawing.Point(577, 187)
        Me.CBTagCloseUp.Name = "CBTagCloseUp"
        Me.CBTagCloseUp.Size = New System.Drawing.Size(69, 17)
        Me.CBTagCloseUp.TabIndex = 205
        Me.CBTagCloseUp.Text = "Close Up"
        Me.CBTagCloseUp.UseVisualStyleBackColor = True
        '
        'CBTagNaked
        '
        Me.CBTagNaked.AutoSize = True
        Me.CBTagNaked.Enabled = False
        Me.CBTagNaked.Location = New System.Drawing.Point(577, 136)
        Me.CBTagNaked.Name = "CBTagNaked"
        Me.CBTagNaked.Size = New System.Drawing.Size(57, 17)
        Me.CBTagNaked.TabIndex = 199
        Me.CBTagNaked.Text = "Naked"
        Me.CBTagNaked.UseVisualStyleBackColor = True
        '
        'CBTagSideView
        '
        Me.CBTagSideView.AutoSize = True
        Me.CBTagSideView.Enabled = False
        Me.CBTagSideView.ForeColor = System.Drawing.Color.Black
        Me.CBTagSideView.Location = New System.Drawing.Point(577, 167)
        Me.CBTagSideView.Name = "CBTagSideView"
        Me.CBTagSideView.Size = New System.Drawing.Size(73, 17)
        Me.CBTagSideView.TabIndex = 204
        Me.CBTagSideView.Text = "Side View"
        Me.CBTagSideView.UseVisualStyleBackColor = True
        '
        'BTNTagPrevious
        '
        Me.BTNTagPrevious.BackColor = System.Drawing.Color.LightGray
        Me.BTNTagPrevious.Enabled = False
        Me.BTNTagPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNTagPrevious.ForeColor = System.Drawing.Color.Black
        Me.BTNTagPrevious.Location = New System.Drawing.Point(391, 8)
        Me.BTNTagPrevious.Name = "BTNTagPrevious"
        Me.BTNTagPrevious.Size = New System.Drawing.Size(47, 24)
        Me.BTNTagPrevious.TabIndex = 207
        Me.BTNTagPrevious.Text = "<<"
        Me.BTNTagPrevious.UseVisualStyleBackColor = False
        '
        'CBTagHalfDressed
        '
        Me.CBTagHalfDressed.AutoSize = True
        Me.CBTagHalfDressed.Enabled = False
        Me.CBTagHalfDressed.Location = New System.Drawing.Point(577, 57)
        Me.CBTagHalfDressed.Name = "CBTagHalfDressed"
        Me.CBTagHalfDressed.Size = New System.Drawing.Size(86, 17)
        Me.CBTagHalfDressed.TabIndex = 198
        Me.CBTagHalfDressed.Text = "Half Dressed"
        Me.CBTagHalfDressed.UseVisualStyleBackColor = True
        '
        'BTNTagNext
        '
        Me.BTNTagNext.BackColor = System.Drawing.Color.LightGray
        Me.BTNTagNext.Enabled = False
        Me.BTNTagNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNTagNext.ForeColor = System.Drawing.Color.Black
        Me.BTNTagNext.Location = New System.Drawing.Point(560, 8)
        Me.BTNTagNext.Name = "BTNTagNext"
        Me.BTNTagNext.Size = New System.Drawing.Size(47, 24)
        Me.BTNTagNext.TabIndex = 206
        Me.BTNTagNext.Text = ">>"
        Me.BTNTagNext.UseVisualStyleBackColor = False
        '
        'CBTagFullyDressed
        '
        Me.CBTagFullyDressed.AutoSize = True
        Me.CBTagFullyDressed.Enabled = False
        Me.CBTagFullyDressed.Location = New System.Drawing.Point(577, 37)
        Me.CBTagFullyDressed.Name = "CBTagFullyDressed"
        Me.CBTagFullyDressed.Size = New System.Drawing.Size(88, 17)
        Me.CBTagFullyDressed.TabIndex = 197
        Me.CBTagFullyDressed.Text = "Fully Dressed"
        Me.CBTagFullyDressed.UseVisualStyleBackColor = True
        '
        'LBLTagCount
        '
        Me.LBLTagCount.BackColor = System.Drawing.Color.Transparent
        Me.LBLTagCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLTagCount.Enabled = False
        Me.LBLTagCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTagCount.ForeColor = System.Drawing.Color.Black
        Me.LBLTagCount.Location = New System.Drawing.Point(444, 10)
        Me.LBLTagCount.Name = "LBLTagCount"
        Me.LBLTagCount.Size = New System.Drawing.Size(110, 20)
        Me.LBLTagCount.TabIndex = 203
        Me.LBLTagCount.Text = "0/0"
        Me.LBLTagCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CBTagSucking
        '
        Me.CBTagSucking.AutoSize = True
        Me.CBTagSucking.Enabled = False
        Me.CBTagSucking.ForeColor = System.Drawing.Color.Black
        Me.CBTagSucking.Location = New System.Drawing.Point(484, 187)
        Me.CBTagSucking.Name = "CBTagSucking"
        Me.CBTagSucking.Size = New System.Drawing.Size(65, 17)
        Me.CBTagSucking.TabIndex = 202
        Me.CBTagSucking.Text = "Sucking"
        Me.CBTagSucking.UseVisualStyleBackColor = True
        '
        'CBTagMasturbating
        '
        Me.CBTagMasturbating.AutoSize = True
        Me.CBTagMasturbating.Enabled = False
        Me.CBTagMasturbating.ForeColor = System.Drawing.Color.Black
        Me.CBTagMasturbating.Location = New System.Drawing.Point(484, 167)
        Me.CBTagMasturbating.Name = "CBTagMasturbating"
        Me.CBTagMasturbating.Size = New System.Drawing.Size(87, 17)
        Me.CBTagMasturbating.TabIndex = 201
        Me.CBTagMasturbating.Text = "Masturbating"
        Me.CBTagMasturbating.UseVisualStyleBackColor = True
        '
        'CBTagFeet
        '
        Me.CBTagFeet.AutoSize = True
        Me.CBTagFeet.Enabled = False
        Me.CBTagFeet.ForeColor = System.Drawing.Color.Black
        Me.CBTagFeet.Location = New System.Drawing.Point(484, 137)
        Me.CBTagFeet.Name = "CBTagFeet"
        Me.CBTagFeet.Size = New System.Drawing.Size(47, 17)
        Me.CBTagFeet.TabIndex = 200
        Me.CBTagFeet.Text = "Feet"
        Me.CBTagFeet.UseVisualStyleBackColor = True
        '
        'CBTagBoobs
        '
        Me.CBTagBoobs.AutoSize = True
        Me.CBTagBoobs.Enabled = False
        Me.CBTagBoobs.ForeColor = System.Drawing.Color.Black
        Me.CBTagBoobs.Location = New System.Drawing.Point(484, 57)
        Me.CBTagBoobs.Name = "CBTagBoobs"
        Me.CBTagBoobs.Size = New System.Drawing.Size(56, 17)
        Me.CBTagBoobs.TabIndex = 196
        Me.CBTagBoobs.Text = "Boobs"
        Me.CBTagBoobs.UseVisualStyleBackColor = True
        '
        'CBTagAss
        '
        Me.CBTagAss.AutoSize = True
        Me.CBTagAss.Enabled = False
        Me.CBTagAss.ForeColor = System.Drawing.Color.Black
        Me.CBTagAss.Location = New System.Drawing.Point(484, 97)
        Me.CBTagAss.Name = "CBTagAss"
        Me.CBTagAss.Size = New System.Drawing.Size(43, 17)
        Me.CBTagAss.TabIndex = 195
        Me.CBTagAss.Text = "Ass"
        Me.CBTagAss.UseVisualStyleBackColor = True
        '
        'CBTagPussy
        '
        Me.CBTagPussy.AutoSize = True
        Me.CBTagPussy.Enabled = False
        Me.CBTagPussy.ForeColor = System.Drawing.Color.Black
        Me.CBTagPussy.Location = New System.Drawing.Point(484, 77)
        Me.CBTagPussy.Name = "CBTagPussy"
        Me.CBTagPussy.Size = New System.Drawing.Size(54, 17)
        Me.CBTagPussy.TabIndex = 194
        Me.CBTagPussy.Text = "Pussy"
        Me.CBTagPussy.UseVisualStyleBackColor = True
        '
        'BTNTagSave
        '
        Me.BTNTagSave.Enabled = False
        Me.BTNTagSave.Location = New System.Drawing.Point(613, 9)
        Me.BTNTagSave.Name = "BTNTagSave"
        Me.BTNTagSave.Size = New System.Drawing.Size(83, 23)
        Me.BTNTagSave.TabIndex = 193
        Me.BTNTagSave.Text = "Finished"
        Me.BTNTagSave.UseVisualStyleBackColor = True
        '
        'DommeTagDirectoryButton
        '
        Me.DommeTagDirectoryButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.DommeTagDirectoryButton.Location = New System.Drawing.Point(6, 8)
        Me.DommeTagDirectoryButton.Name = "DommeTagDirectoryButton"
        Me.DommeTagDirectoryButton.Size = New System.Drawing.Size(43, 23)
        Me.DommeTagDirectoryButton.TabIndex = 192
        Me.DommeTagDirectoryButton.Text = "1"
        Me.DommeTagDirectoryButton.UseVisualStyleBackColor = True
        '
        'ImageTagPictureBox
        '
        Me.ImageTagPictureBox.BackColor = System.Drawing.Color.Black
        Me.ImageTagPictureBox.Location = New System.Drawing.Point(5, 37)
        Me.ImageTagPictureBox.Name = "ImageTagPictureBox"
        Me.ImageTagPictureBox.Size = New System.Drawing.Size(451, 328)
        Me.ImageTagPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ImageTagPictureBox.TabIndex = 191
        Me.ImageTagPictureBox.TabStop = False
        '
        'CBTagFace
        '
        Me.CBTagFace.AutoSize = True
        Me.CBTagFace.Enabled = False
        Me.CBTagFace.ForeColor = System.Drawing.Color.Black
        Me.CBTagFace.Location = New System.Drawing.Point(484, 37)
        Me.CBTagFace.Name = "CBTagFace"
        Me.CBTagFace.Size = New System.Drawing.Size(50, 17)
        Me.CBTagFace.TabIndex = 190
        Me.CBTagFace.Text = "Face"
        Me.CBTagFace.UseVisualStyleBackColor = True
        '
        'FileDropDownLabel
        '
        Me.FileDropDownLabel.BackColor = System.Drawing.Color.LightGray
        Me.FileDropDownLabel.Controls.Add(Me.LocalTagImageNavGroup)
        Me.FileDropDownLabel.Controls.Add(Me.GenreDropDownLabel)
        Me.FileDropDownLabel.Controls.Add(Me.GenreCombo)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox55)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox53)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox49)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox46)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox54)
        Me.FileDropDownLabel.Controls.Add(Me.BdsmTagGroup)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox50)
        Me.FileDropDownLabel.Controls.Add(Me.GroupBox48)
        Me.FileDropDownLabel.Controls.Add(Me.SaveTagButton)
        Me.FileDropDownLabel.Controls.Add(Me.LocalTagPictureBox)
        Me.FileDropDownLabel.Location = New System.Drawing.Point(4, 22)
        Me.FileDropDownLabel.Name = "FileDropDownLabel"
        Me.FileDropDownLabel.Padding = New System.Windows.Forms.Padding(3)
        Me.FileDropDownLabel.Size = New System.Drawing.Size(958, 424)
        Me.FileDropDownLabel.TabIndex = 1
        Me.FileDropDownLabel.Text = "Local Tags"
        '
        'LocalTagImageNavGroup
        '
        Me.LocalTagImageNavGroup.Controls.Add(Me.FileTagCombo)
        Me.LocalTagImageNavGroup.Controls.Add(Me.LBLLocalTagCount)
        Me.LocalTagImageNavGroup.Controls.Add(Me.FileTagNextButton)
        Me.LocalTagImageNavGroup.Controls.Add(Me.FileTagPreviousButton)
        Me.LocalTagImageNavGroup.Location = New System.Drawing.Point(6, 38)
        Me.LocalTagImageNavGroup.Name = "LocalTagImageNavGroup"
        Me.LocalTagImageNavGroup.Size = New System.Drawing.Size(436, 71)
        Me.LocalTagImageNavGroup.TabIndex = 247
        Me.LocalTagImageNavGroup.TabStop = False
        Me.LocalTagImageNavGroup.Text = "File Navigation"
        '
        'FileTagCombo
        '
        Me.FileTagCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FileTagCombo.FormattingEnabled = True
        Me.FileTagCombo.Location = New System.Drawing.Point(61, 13)
        Me.FileTagCombo.Name = "FileTagCombo"
        Me.FileTagCombo.Size = New System.Drawing.Size(343, 21)
        Me.FileTagCombo.TabIndex = 2
        '
        'LBLLocalTagCount
        '
        Me.LBLLocalTagCount.BackColor = System.Drawing.Color.Transparent
        Me.LBLLocalTagCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLLocalTagCount.Enabled = False
        Me.LBLLocalTagCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLLocalTagCount.ForeColor = System.Drawing.Color.Black
        Me.LBLLocalTagCount.Location = New System.Drawing.Point(177, 37)
        Me.LBLLocalTagCount.Name = "LBLLocalTagCount"
        Me.LBLLocalTagCount.Size = New System.Drawing.Size(110, 20)
        Me.LBLLocalTagCount.TabIndex = 230
        Me.LBLLocalTagCount.Text = "0/0"
        Me.LBLLocalTagCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FileTagNextButton
        '
        Me.FileTagNextButton.BackColor = System.Drawing.Color.LightGray
        Me.FileTagNextButton.Enabled = False
        Me.FileTagNextButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileTagNextButton.ForeColor = System.Drawing.Color.Black
        Me.FileTagNextButton.Location = New System.Drawing.Point(293, 35)
        Me.FileTagNextButton.Name = "FileTagNextButton"
        Me.FileTagNextButton.Size = New System.Drawing.Size(47, 24)
        Me.FileTagNextButton.TabIndex = 4
        Me.FileTagNextButton.Text = ">>"
        Me.FileTagNextButton.UseVisualStyleBackColor = False
        '
        'FileTagPreviousButton
        '
        Me.FileTagPreviousButton.BackColor = System.Drawing.Color.LightGray
        Me.FileTagPreviousButton.Enabled = False
        Me.FileTagPreviousButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileTagPreviousButton.ForeColor = System.Drawing.Color.Black
        Me.FileTagPreviousButton.Location = New System.Drawing.Point(124, 35)
        Me.FileTagPreviousButton.Name = "FileTagPreviousButton"
        Me.FileTagPreviousButton.Size = New System.Drawing.Size(47, 24)
        Me.FileTagPreviousButton.TabIndex = 3
        Me.FileTagPreviousButton.Text = "<<"
        Me.FileTagPreviousButton.UseVisualStyleBackColor = False
        '
        'GenreDropDownLabel
        '
        Me.GenreDropDownLabel.AutoSize = True
        Me.GenreDropDownLabel.Location = New System.Drawing.Point(20, 14)
        Me.GenreDropDownLabel.Name = "GenreDropDownLabel"
        Me.GenreDropDownLabel.Size = New System.Drawing.Size(68, 13)
        Me.GenreDropDownLabel.TabIndex = 243
        Me.GenreDropDownLabel.Text = "Image Genre"
        '
        'GenreCombo
        '
        Me.GenreCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GenreCombo.FormattingEnabled = True
        Me.GenreCombo.Location = New System.Drawing.Point(119, 10)
        Me.GenreCombo.Name = "GenreCombo"
        Me.GenreCombo.Size = New System.Drawing.Size(266, 21)
        Me.GenreCombo.TabIndex = 1
        '
        'GroupBox55
        '
        Me.GroupBox55.Controls.Add(Me.CBTagNurse)
        Me.GroupBox55.Controls.Add(Me.CBTagSchoolgirl)
        Me.GroupBox55.Controls.Add(Me.CBTagMaid)
        Me.GroupBox55.Controls.Add(Me.CBTagTeacher)
        Me.GroupBox55.Controls.Add(Me.CBTagSuperhero)
        Me.GroupBox55.Location = New System.Drawing.Point(8, 479)
        Me.GroupBox55.Name = "GroupBox55"
        Me.GroupBox55.Size = New System.Drawing.Size(105, 118)
        Me.GroupBox55.TabIndex = 241
        Me.GroupBox55.TabStop = False
        Me.GroupBox55.Text = "Outfit"
        '
        'CBTagNurse
        '
        Me.CBTagNurse.AutoSize = True
        Me.CBTagNurse.Enabled = False
        Me.CBTagNurse.ForeColor = System.Drawing.Color.Black
        Me.CBTagNurse.Location = New System.Drawing.Point(15, 17)
        Me.CBTagNurse.Name = "CBTagNurse"
        Me.CBTagNurse.Size = New System.Drawing.Size(54, 17)
        Me.CBTagNurse.TabIndex = 203
        Me.CBTagNurse.Text = "Nurse"
        Me.CBTagNurse.UseVisualStyleBackColor = True
        '
        'CBTagSchoolgirl
        '
        Me.CBTagSchoolgirl.AutoSize = True
        Me.CBTagSchoolgirl.Enabled = False
        Me.CBTagSchoolgirl.ForeColor = System.Drawing.Color.Black
        Me.CBTagSchoolgirl.Location = New System.Drawing.Point(15, 57)
        Me.CBTagSchoolgirl.Name = "CBTagSchoolgirl"
        Me.CBTagSchoolgirl.Size = New System.Drawing.Size(72, 17)
        Me.CBTagSchoolgirl.TabIndex = 204
        Me.CBTagSchoolgirl.Text = "Schoolgirl"
        Me.CBTagSchoolgirl.UseVisualStyleBackColor = True
        '
        'CBTagMaid
        '
        Me.CBTagMaid.AutoSize = True
        Me.CBTagMaid.Enabled = False
        Me.CBTagMaid.ForeColor = System.Drawing.Color.Black
        Me.CBTagMaid.Location = New System.Drawing.Point(15, 77)
        Me.CBTagMaid.Name = "CBTagMaid"
        Me.CBTagMaid.Size = New System.Drawing.Size(49, 17)
        Me.CBTagMaid.TabIndex = 205
        Me.CBTagMaid.Text = "Maid"
        Me.CBTagMaid.UseVisualStyleBackColor = True
        '
        'CBTagTeacher
        '
        Me.CBTagTeacher.AutoSize = True
        Me.CBTagTeacher.Enabled = False
        Me.CBTagTeacher.ForeColor = System.Drawing.Color.Black
        Me.CBTagTeacher.Location = New System.Drawing.Point(15, 37)
        Me.CBTagTeacher.Name = "CBTagTeacher"
        Me.CBTagTeacher.Size = New System.Drawing.Size(66, 17)
        Me.CBTagTeacher.TabIndex = 206
        Me.CBTagTeacher.Text = "Teacher"
        Me.CBTagTeacher.UseVisualStyleBackColor = True
        '
        'CBTagSuperhero
        '
        Me.CBTagSuperhero.AutoSize = True
        Me.CBTagSuperhero.Enabled = False
        Me.CBTagSuperhero.ForeColor = System.Drawing.Color.Black
        Me.CBTagSuperhero.Location = New System.Drawing.Point(15, 97)
        Me.CBTagSuperhero.Name = "CBTagSuperhero"
        Me.CBTagSuperhero.Size = New System.Drawing.Size(75, 17)
        Me.CBTagSuperhero.TabIndex = 213
        Me.CBTagSuperhero.Text = "Superhero"
        Me.CBTagSuperhero.UseVisualStyleBackColor = True
        '
        'GroupBox53
        '
        Me.GroupBox53.Controls.Add(Me.CBTagTrap)
        Me.GroupBox53.Controls.Add(Me.CBTagTentacles)
        Me.GroupBox53.Controls.Add(Me.CBTagMonsterGirl)
        Me.GroupBox53.Controls.Add(Me.CBTagBukkake)
        Me.GroupBox53.Controls.Add(Me.CBTagGanguro)
        Me.GroupBox53.Controls.Add(Me.CBTagBodyWriting)
        Me.GroupBox53.Controls.Add(Me.CBTagMahouShoujo)
        Me.GroupBox53.Controls.Add(Me.CBTagBakunyuu)
        Me.GroupBox53.Controls.Add(Me.CBTagAhegao)
        Me.GroupBox53.Controls.Add(Me.CBTagShibari)
        Me.GroupBox53.Location = New System.Drawing.Point(119, 479)
        Me.GroupBox53.Name = "GroupBox53"
        Me.GroupBox53.Size = New System.Drawing.Size(216, 118)
        Me.GroupBox53.TabIndex = 240
        Me.GroupBox53.TabStop = False
        Me.GroupBox53.Text = "Hentai/JAV Themes"
        '
        'CBTagTrap
        '
        Me.CBTagTrap.AutoSize = True
        Me.CBTagTrap.Enabled = False
        Me.CBTagTrap.ForeColor = System.Drawing.Color.Black
        Me.CBTagTrap.Location = New System.Drawing.Point(126, 37)
        Me.CBTagTrap.Name = "CBTagTrap"
        Me.CBTagTrap.Size = New System.Drawing.Size(48, 17)
        Me.CBTagTrap.TabIndex = 226
        Me.CBTagTrap.Text = "Trap"
        Me.CBTagTrap.UseVisualStyleBackColor = True
        '
        'CBTagTentacles
        '
        Me.CBTagTentacles.AutoSize = True
        Me.CBTagTentacles.Enabled = False
        Me.CBTagTentacles.ForeColor = System.Drawing.Color.Black
        Me.CBTagTentacles.Location = New System.Drawing.Point(15, 37)
        Me.CBTagTentacles.Name = "CBTagTentacles"
        Me.CBTagTentacles.Size = New System.Drawing.Size(73, 17)
        Me.CBTagTentacles.TabIndex = 204
        Me.CBTagTentacles.Text = "Tentacles"
        Me.CBTagTentacles.UseVisualStyleBackColor = True
        '
        'CBTagMonsterGirl
        '
        Me.CBTagMonsterGirl.AutoSize = True
        Me.CBTagMonsterGirl.Enabled = False
        Me.CBTagMonsterGirl.ForeColor = System.Drawing.Color.Black
        Me.CBTagMonsterGirl.Location = New System.Drawing.Point(126, 97)
        Me.CBTagMonsterGirl.Name = "CBTagMonsterGirl"
        Me.CBTagMonsterGirl.Size = New System.Drawing.Size(82, 17)
        Me.CBTagMonsterGirl.TabIndex = 214
        Me.CBTagMonsterGirl.Text = "Monster Girl"
        Me.CBTagMonsterGirl.UseVisualStyleBackColor = True
        '
        'CBTagBukkake
        '
        Me.CBTagBukkake.AutoSize = True
        Me.CBTagBukkake.Enabled = False
        Me.CBTagBukkake.ForeColor = System.Drawing.Color.Black
        Me.CBTagBukkake.Location = New System.Drawing.Point(15, 57)
        Me.CBTagBukkake.Name = "CBTagBukkake"
        Me.CBTagBukkake.Size = New System.Drawing.Size(69, 17)
        Me.CBTagBukkake.TabIndex = 210
        Me.CBTagBukkake.Text = "Bukkake"
        Me.CBTagBukkake.UseVisualStyleBackColor = True
        '
        'CBTagGanguro
        '
        Me.CBTagGanguro.AutoSize = True
        Me.CBTagGanguro.Enabled = False
        Me.CBTagGanguro.ForeColor = System.Drawing.Color.Black
        Me.CBTagGanguro.Location = New System.Drawing.Point(126, 57)
        Me.CBTagGanguro.Name = "CBTagGanguro"
        Me.CBTagGanguro.Size = New System.Drawing.Size(67, 17)
        Me.CBTagGanguro.TabIndex = 205
        Me.CBTagGanguro.Text = "Ganguro"
        Me.CBTagGanguro.UseVisualStyleBackColor = True
        '
        'CBTagBodyWriting
        '
        Me.CBTagBodyWriting.AutoSize = True
        Me.CBTagBodyWriting.Enabled = False
        Me.CBTagBodyWriting.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyWriting.Location = New System.Drawing.Point(126, 17)
        Me.CBTagBodyWriting.Name = "CBTagBodyWriting"
        Me.CBTagBodyWriting.Size = New System.Drawing.Size(86, 17)
        Me.CBTagBodyWriting.TabIndex = 208
        Me.CBTagBodyWriting.Text = "Body Writing"
        Me.CBTagBodyWriting.UseVisualStyleBackColor = True
        '
        'CBTagMahouShoujo
        '
        Me.CBTagMahouShoujo.AutoSize = True
        Me.CBTagMahouShoujo.Enabled = False
        Me.CBTagMahouShoujo.ForeColor = System.Drawing.Color.Black
        Me.CBTagMahouShoujo.Location = New System.Drawing.Point(126, 77)
        Me.CBTagMahouShoujo.Name = "CBTagMahouShoujo"
        Me.CBTagMahouShoujo.Size = New System.Drawing.Size(95, 17)
        Me.CBTagMahouShoujo.TabIndex = 209
        Me.CBTagMahouShoujo.Text = "Mahou Shoujo"
        Me.CBTagMahouShoujo.UseVisualStyleBackColor = True
        '
        'CBTagBakunyuu
        '
        Me.CBTagBakunyuu.AutoSize = True
        Me.CBTagBakunyuu.Enabled = False
        Me.CBTagBakunyuu.ForeColor = System.Drawing.Color.Black
        Me.CBTagBakunyuu.Location = New System.Drawing.Point(15, 77)
        Me.CBTagBakunyuu.Name = "CBTagBakunyuu"
        Me.CBTagBakunyuu.Size = New System.Drawing.Size(74, 17)
        Me.CBTagBakunyuu.TabIndex = 213
        Me.CBTagBakunyuu.Text = "Bakunyuu"
        Me.CBTagBakunyuu.UseVisualStyleBackColor = True
        '
        'CBTagAhegao
        '
        Me.CBTagAhegao.AutoSize = True
        Me.CBTagAhegao.Enabled = False
        Me.CBTagAhegao.ForeColor = System.Drawing.Color.Black
        Me.CBTagAhegao.Location = New System.Drawing.Point(15, 97)
        Me.CBTagAhegao.Name = "CBTagAhegao"
        Me.CBTagAhegao.Size = New System.Drawing.Size(63, 17)
        Me.CBTagAhegao.TabIndex = 207
        Me.CBTagAhegao.Text = "Ahegao"
        Me.CBTagAhegao.UseVisualStyleBackColor = True
        '
        'CBTagShibari
        '
        Me.CBTagShibari.AutoSize = True
        Me.CBTagShibari.Enabled = False
        Me.CBTagShibari.ForeColor = System.Drawing.Color.Black
        Me.CBTagShibari.Location = New System.Drawing.Point(15, 17)
        Me.CBTagShibari.Name = "CBTagShibari"
        Me.CBTagShibari.Size = New System.Drawing.Size(58, 17)
        Me.CBTagShibari.TabIndex = 203
        Me.CBTagShibari.Text = "Shibari"
        Me.CBTagShibari.UseVisualStyleBackColor = True
        '
        'GroupBox49
        '
        Me.GroupBox49.Controls.Add(Me.CBTagBodyMouth)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyAss)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyFace)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyLegs)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyBalls)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyCock)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyFeet)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyNipples)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyPussy)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyTits)
        Me.GroupBox49.Controls.Add(Me.CBTagBodyFingers)
        Me.GroupBox49.Location = New System.Drawing.Point(341, 115)
        Me.GroupBox49.Name = "GroupBox49"
        Me.GroupBox49.Size = New System.Drawing.Size(103, 238)
        Me.GroupBox49.TabIndex = 236
        Me.GroupBox49.TabStop = False
        Me.GroupBox49.Text = "Body Part"
        '
        'CBTagBodyMouth
        '
        Me.CBTagBodyMouth.AutoSize = True
        Me.CBTagBodyMouth.Enabled = False
        Me.CBTagBodyMouth.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyMouth.Location = New System.Drawing.Point(14, 57)
        Me.CBTagBodyMouth.Name = "CBTagBodyMouth"
        Me.CBTagBodyMouth.Size = New System.Drawing.Size(56, 17)
        Me.CBTagBodyMouth.TabIndex = 220
        Me.CBTagBodyMouth.Text = "Mouth"
        Me.CBTagBodyMouth.UseVisualStyleBackColor = True
        '
        'CBTagBodyAss
        '
        Me.CBTagBodyAss.AutoSize = True
        Me.CBTagBodyAss.Enabled = False
        Me.CBTagBodyAss.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyAss.Location = New System.Drawing.Point(15, 137)
        Me.CBTagBodyAss.Name = "CBTagBodyAss"
        Me.CBTagBodyAss.Size = New System.Drawing.Size(43, 17)
        Me.CBTagBodyAss.TabIndex = 219
        Me.CBTagBodyAss.Text = "Ass"
        Me.CBTagBodyAss.UseVisualStyleBackColor = True
        '
        'CBTagBodyFace
        '
        Me.CBTagBodyFace.AutoSize = True
        Me.CBTagBodyFace.Enabled = False
        Me.CBTagBodyFace.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyFace.Location = New System.Drawing.Point(15, 17)
        Me.CBTagBodyFace.Name = "CBTagBodyFace"
        Me.CBTagBodyFace.Size = New System.Drawing.Size(50, 17)
        Me.CBTagBodyFace.TabIndex = 203
        Me.CBTagBodyFace.Text = "Face"
        Me.CBTagBodyFace.UseVisualStyleBackColor = True
        '
        'CBTagBodyLegs
        '
        Me.CBTagBodyLegs.AutoSize = True
        Me.CBTagBodyLegs.Enabled = False
        Me.CBTagBodyLegs.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyLegs.Location = New System.Drawing.Point(15, 157)
        Me.CBTagBodyLegs.Name = "CBTagBodyLegs"
        Me.CBTagBodyLegs.Size = New System.Drawing.Size(49, 17)
        Me.CBTagBodyLegs.TabIndex = 218
        Me.CBTagBodyLegs.Text = "Legs"
        Me.CBTagBodyLegs.UseVisualStyleBackColor = True
        '
        'CBTagBodyBalls
        '
        Me.CBTagBodyBalls.AutoSize = True
        Me.CBTagBodyBalls.Enabled = False
        Me.CBTagBodyBalls.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyBalls.Location = New System.Drawing.Point(15, 217)
        Me.CBTagBodyBalls.Name = "CBTagBodyBalls"
        Me.CBTagBodyBalls.Size = New System.Drawing.Size(48, 17)
        Me.CBTagBodyBalls.TabIndex = 217
        Me.CBTagBodyBalls.Text = "Balls"
        Me.CBTagBodyBalls.UseVisualStyleBackColor = True
        '
        'CBTagBodyCock
        '
        Me.CBTagBodyCock.AutoSize = True
        Me.CBTagBodyCock.Enabled = False
        Me.CBTagBodyCock.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyCock.Location = New System.Drawing.Point(15, 197)
        Me.CBTagBodyCock.Name = "CBTagBodyCock"
        Me.CBTagBodyCock.Size = New System.Drawing.Size(51, 17)
        Me.CBTagBodyCock.TabIndex = 216
        Me.CBTagBodyCock.Text = "Cock"
        Me.CBTagBodyCock.UseVisualStyleBackColor = True
        '
        'CBTagBodyFeet
        '
        Me.CBTagBodyFeet.AutoSize = True
        Me.CBTagBodyFeet.Enabled = False
        Me.CBTagBodyFeet.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyFeet.Location = New System.Drawing.Point(15, 177)
        Me.CBTagBodyFeet.Name = "CBTagBodyFeet"
        Me.CBTagBodyFeet.Size = New System.Drawing.Size(47, 17)
        Me.CBTagBodyFeet.TabIndex = 215
        Me.CBTagBodyFeet.Text = "Feet"
        Me.CBTagBodyFeet.UseVisualStyleBackColor = True
        '
        'CBTagBodyNipples
        '
        Me.CBTagBodyNipples.AutoSize = True
        Me.CBTagBodyNipples.Enabled = False
        Me.CBTagBodyNipples.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyNipples.Location = New System.Drawing.Point(15, 97)
        Me.CBTagBodyNipples.Name = "CBTagBodyNipples"
        Me.CBTagBodyNipples.Size = New System.Drawing.Size(61, 17)
        Me.CBTagBodyNipples.TabIndex = 207
        Me.CBTagBodyNipples.Text = "Nipples"
        Me.CBTagBodyNipples.UseVisualStyleBackColor = True
        '
        'CBTagBodyPussy
        '
        Me.CBTagBodyPussy.AutoSize = True
        Me.CBTagBodyPussy.Enabled = False
        Me.CBTagBodyPussy.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyPussy.Location = New System.Drawing.Point(15, 117)
        Me.CBTagBodyPussy.Name = "CBTagBodyPussy"
        Me.CBTagBodyPussy.Size = New System.Drawing.Size(54, 17)
        Me.CBTagBodyPussy.TabIndex = 209
        Me.CBTagBodyPussy.Text = "Pussy"
        Me.CBTagBodyPussy.UseVisualStyleBackColor = True
        '
        'CBTagBodyTits
        '
        Me.CBTagBodyTits.AutoSize = True
        Me.CBTagBodyTits.Enabled = False
        Me.CBTagBodyTits.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyTits.Location = New System.Drawing.Point(15, 77)
        Me.CBTagBodyTits.Name = "CBTagBodyTits"
        Me.CBTagBodyTits.Size = New System.Drawing.Size(43, 17)
        Me.CBTagBodyTits.TabIndex = 213
        Me.CBTagBodyTits.Text = "Tits"
        Me.CBTagBodyTits.UseVisualStyleBackColor = True
        '
        'CBTagBodyFingers
        '
        Me.CBTagBodyFingers.AutoSize = True
        Me.CBTagBodyFingers.Enabled = False
        Me.CBTagBodyFingers.ForeColor = System.Drawing.Color.Black
        Me.CBTagBodyFingers.Location = New System.Drawing.Point(15, 37)
        Me.CBTagBodyFingers.Name = "CBTagBodyFingers"
        Me.CBTagBodyFingers.Size = New System.Drawing.Size(60, 17)
        Me.CBTagBodyFingers.TabIndex = 210
        Me.CBTagBodyFingers.Text = "Fingers"
        Me.CBTagBodyFingers.UseVisualStyleBackColor = True
        '
        'GroupBox46
        '
        Me.GroupBox46.Controls.Add(Me.CBTagMultiSub)
        Me.GroupBox46.Controls.Add(Me.CBTagMultiDom)
        Me.GroupBox46.Controls.Add(Me.CBTagFemdom)
        Me.GroupBox46.Controls.Add(Me.CBTag2M)
        Me.GroupBox46.Controls.Add(Me.CBTagFutadom)
        Me.GroupBox46.Controls.Add(Me.CBTagFemsub)
        Me.GroupBox46.Controls.Add(Me.CBTag2Futa)
        Me.GroupBox46.Controls.Add(Me.CBTagMaledom)
        Me.GroupBox46.Controls.Add(Me.CBTag3M)
        Me.GroupBox46.Controls.Add(Me.CBTagFutasub)
        Me.GroupBox46.Controls.Add(Me.CBTag3Futa)
        Me.GroupBox46.Controls.Add(Me.CBTagMalesub)
        Me.GroupBox46.Controls.Add(Me.CBTag2F)
        Me.GroupBox46.Controls.Add(Me.CBTag1Futa)
        Me.GroupBox46.Controls.Add(Me.CBTag1M)
        Me.GroupBox46.Controls.Add(Me.CBTag1F)
        Me.GroupBox46.Controls.Add(Me.CBTag3F)
        Me.GroupBox46.Location = New System.Drawing.Point(230, 115)
        Me.GroupBox46.Name = "GroupBox46"
        Me.GroupBox46.Size = New System.Drawing.Size(105, 358)
        Me.GroupBox46.TabIndex = 234
        Me.GroupBox46.TabStop = False
        Me.GroupBox46.Text = "Genders && Roles"
        '
        'CBTagMultiSub
        '
        Me.CBTagMultiSub.AutoSize = True
        Me.CBTagMultiSub.Enabled = False
        Me.CBTagMultiSub.ForeColor = System.Drawing.Color.Black
        Me.CBTagMultiSub.Location = New System.Drawing.Point(15, 337)
        Me.CBTagMultiSub.Name = "CBTagMultiSub"
        Me.CBTagMultiSub.Size = New System.Drawing.Size(70, 17)
        Me.CBTagMultiSub.TabIndex = 207
        Me.CBTagMultiSub.Text = "Multi-Sub"
        Me.CBTagMultiSub.UseVisualStyleBackColor = True
        '
        'CBTagMultiDom
        '
        Me.CBTagMultiDom.AutoSize = True
        Me.CBTagMultiDom.Enabled = False
        Me.CBTagMultiDom.ForeColor = System.Drawing.Color.Black
        Me.CBTagMultiDom.Location = New System.Drawing.Point(15, 317)
        Me.CBTagMultiDom.Name = "CBTagMultiDom"
        Me.CBTagMultiDom.Size = New System.Drawing.Size(73, 17)
        Me.CBTagMultiDom.TabIndex = 230
        Me.CBTagMultiDom.Text = "Multi-Dom"
        Me.CBTagMultiDom.UseVisualStyleBackColor = True
        '
        'CBTagFemdom
        '
        Me.CBTagFemdom.AutoSize = True
        Me.CBTagFemdom.Enabled = False
        Me.CBTagFemdom.ForeColor = System.Drawing.Color.Black
        Me.CBTagFemdom.Location = New System.Drawing.Point(15, 197)
        Me.CBTagFemdom.Name = "CBTagFemdom"
        Me.CBTagFemdom.Size = New System.Drawing.Size(66, 17)
        Me.CBTagFemdom.TabIndex = 229
        Me.CBTagFemdom.Text = "Femdom"
        Me.CBTagFemdom.UseVisualStyleBackColor = True
        '
        'CBTag2M
        '
        Me.CBTag2M.AutoSize = True
        Me.CBTag2M.Enabled = False
        Me.CBTag2M.ForeColor = System.Drawing.Color.Black
        Me.CBTag2M.Location = New System.Drawing.Point(15, 97)
        Me.CBTag2M.Name = "CBTag2M"
        Me.CBTag2M.Size = New System.Drawing.Size(56, 17)
        Me.CBTag2M.TabIndex = 206
        Me.CBTag2M.Text = "2 Men"
        Me.CBTag2M.UseVisualStyleBackColor = True
        '
        'CBTagFutadom
        '
        Me.CBTagFutadom.AutoSize = True
        Me.CBTagFutadom.Enabled = False
        Me.CBTagFutadom.ForeColor = System.Drawing.Color.Black
        Me.CBTagFutadom.Location = New System.Drawing.Point(15, 237)
        Me.CBTagFutadom.Name = "CBTagFutadom"
        Me.CBTagFutadom.Size = New System.Drawing.Size(67, 17)
        Me.CBTagFutadom.TabIndex = 204
        Me.CBTagFutadom.Text = "Futadom"
        Me.CBTagFutadom.UseVisualStyleBackColor = True
        '
        'CBTagFemsub
        '
        Me.CBTagFemsub.AutoSize = True
        Me.CBTagFemsub.Enabled = False
        Me.CBTagFemsub.ForeColor = System.Drawing.Color.Black
        Me.CBTagFemsub.Location = New System.Drawing.Point(15, 257)
        Me.CBTagFemsub.Name = "CBTagFemsub"
        Me.CBTagFemsub.Size = New System.Drawing.Size(63, 17)
        Me.CBTagFemsub.TabIndex = 205
        Me.CBTagFemsub.Text = "Femsub"
        Me.CBTagFemsub.UseVisualStyleBackColor = True
        '
        'CBTag2Futa
        '
        Me.CBTag2Futa.AutoSize = True
        Me.CBTag2Futa.Enabled = False
        Me.CBTag2Futa.ForeColor = System.Drawing.Color.Black
        Me.CBTag2Futa.Location = New System.Drawing.Point(15, 157)
        Me.CBTag2Futa.Name = "CBTag2Futa"
        Me.CBTag2Futa.Size = New System.Drawing.Size(56, 17)
        Me.CBTag2Futa.TabIndex = 186
        Me.CBTag2Futa.Text = "2 Futa"
        Me.CBTag2Futa.UseVisualStyleBackColor = True
        '
        'CBTagMaledom
        '
        Me.CBTagMaledom.AutoSize = True
        Me.CBTagMaledom.Enabled = False
        Me.CBTagMaledom.ForeColor = System.Drawing.Color.Black
        Me.CBTagMaledom.Location = New System.Drawing.Point(15, 217)
        Me.CBTagMaledom.Name = "CBTagMaledom"
        Me.CBTagMaledom.Size = New System.Drawing.Size(69, 17)
        Me.CBTagMaledom.TabIndex = 206
        Me.CBTagMaledom.Text = "Maledom"
        Me.CBTagMaledom.UseVisualStyleBackColor = True
        '
        'CBTag3M
        '
        Me.CBTag3M.AutoSize = True
        Me.CBTag3M.Enabled = False
        Me.CBTag3M.ForeColor = System.Drawing.Color.Black
        Me.CBTag3M.Location = New System.Drawing.Point(15, 117)
        Me.CBTag3M.Name = "CBTag3M"
        Me.CBTag3M.Size = New System.Drawing.Size(56, 17)
        Me.CBTag3M.TabIndex = 190
        Me.CBTag3M.Text = "3 Men"
        Me.CBTag3M.UseVisualStyleBackColor = True
        '
        'CBTagFutasub
        '
        Me.CBTagFutasub.AutoSize = True
        Me.CBTagFutasub.Enabled = False
        Me.CBTagFutasub.ForeColor = System.Drawing.Color.Black
        Me.CBTagFutasub.Location = New System.Drawing.Point(15, 297)
        Me.CBTagFutasub.Name = "CBTagFutasub"
        Me.CBTagFutasub.Size = New System.Drawing.Size(64, 17)
        Me.CBTagFutasub.TabIndex = 213
        Me.CBTagFutasub.Text = "Futasub"
        Me.CBTagFutasub.UseVisualStyleBackColor = True
        '
        'CBTag3Futa
        '
        Me.CBTag3Futa.AutoSize = True
        Me.CBTag3Futa.Enabled = False
        Me.CBTag3Futa.ForeColor = System.Drawing.Color.Black
        Me.CBTag3Futa.Location = New System.Drawing.Point(15, 177)
        Me.CBTag3Futa.Name = "CBTag3Futa"
        Me.CBTag3Futa.Size = New System.Drawing.Size(56, 17)
        Me.CBTag3Futa.TabIndex = 197
        Me.CBTag3Futa.Text = "3 Futa"
        Me.CBTag3Futa.UseVisualStyleBackColor = True
        '
        'CBTagMalesub
        '
        Me.CBTagMalesub.AutoSize = True
        Me.CBTagMalesub.Enabled = False
        Me.CBTagMalesub.ForeColor = System.Drawing.Color.Black
        Me.CBTagMalesub.Location = New System.Drawing.Point(15, 277)
        Me.CBTagMalesub.Name = "CBTagMalesub"
        Me.CBTagMalesub.Size = New System.Drawing.Size(66, 17)
        Me.CBTagMalesub.TabIndex = 210
        Me.CBTagMalesub.Text = "Malesub"
        Me.CBTagMalesub.UseVisualStyleBackColor = True
        '
        'CBTag2F
        '
        Me.CBTag2F.AutoSize = True
        Me.CBTag2F.Enabled = False
        Me.CBTag2F.ForeColor = System.Drawing.Color.Black
        Me.CBTag2F.Location = New System.Drawing.Point(15, 37)
        Me.CBTag2F.Name = "CBTag2F"
        Me.CBTag2F.Size = New System.Drawing.Size(72, 17)
        Me.CBTag2F.TabIndex = 188
        Me.CBTag2F.Text = "2 Women"
        Me.CBTag2F.UseVisualStyleBackColor = True
        '
        'CBTag1Futa
        '
        Me.CBTag1Futa.AutoSize = True
        Me.CBTag1Futa.Enabled = False
        Me.CBTag1Futa.ForeColor = System.Drawing.Color.Black
        Me.CBTag1Futa.Location = New System.Drawing.Point(15, 137)
        Me.CBTag1Futa.Name = "CBTag1Futa"
        Me.CBTag1Futa.Size = New System.Drawing.Size(56, 17)
        Me.CBTag1Futa.TabIndex = 191
        Me.CBTag1Futa.Text = "1 Futa"
        Me.CBTag1Futa.UseVisualStyleBackColor = True
        '
        'CBTag1M
        '
        Me.CBTag1M.AutoSize = True
        Me.CBTag1M.Enabled = False
        Me.CBTag1M.ForeColor = System.Drawing.Color.Black
        Me.CBTag1M.Location = New System.Drawing.Point(15, 77)
        Me.CBTag1M.Name = "CBTag1M"
        Me.CBTag1M.Size = New System.Drawing.Size(56, 17)
        Me.CBTag1M.TabIndex = 189
        Me.CBTag1M.Text = "1 Man"
        Me.CBTag1M.UseVisualStyleBackColor = True
        '
        'CBTag1F
        '
        Me.CBTag1F.AutoSize = True
        Me.CBTag1F.Enabled = False
        Me.CBTag1F.ForeColor = System.Drawing.Color.Black
        Me.CBTag1F.Location = New System.Drawing.Point(15, 17)
        Me.CBTag1F.Name = "CBTag1F"
        Me.CBTag1F.Size = New System.Drawing.Size(72, 17)
        Me.CBTag1F.TabIndex = 185
        Me.CBTag1F.Text = "1 Woman"
        Me.CBTag1F.UseVisualStyleBackColor = True
        '
        'CBTag3F
        '
        Me.CBTag3F.AutoSize = True
        Me.CBTag3F.Enabled = False
        Me.CBTag3F.ForeColor = System.Drawing.Color.Black
        Me.CBTag3F.Location = New System.Drawing.Point(15, 57)
        Me.CBTag3F.Name = "CBTag3F"
        Me.CBTag3F.Size = New System.Drawing.Size(72, 17)
        Me.CBTag3F.TabIndex = 192
        Me.CBTag3F.Text = "3 Women"
        Me.CBTag3F.UseVisualStyleBackColor = True
        '
        'GroupBox54
        '
        Me.GroupBox54.Controls.Add(Me.CBTagTattoos)
        Me.GroupBox54.Controls.Add(Me.CBTagAnalToy)
        Me.GroupBox54.Controls.Add(Me.CBTagDomme)
        Me.GroupBox54.Controls.Add(Me.CBTagPocketPussy)
        Me.GroupBox54.Controls.Add(Me.CBTagWatersports)
        Me.GroupBox54.Controls.Add(Me.CBTagStockings)
        Me.GroupBox54.Controls.Add(Me.CBTagCumshot)
        Me.GroupBox54.Controls.Add(Me.CBTagCumEating)
        Me.GroupBox54.Controls.Add(Me.CBTagVibrator)
        Me.GroupBox54.Controls.Add(Me.CBTagDildo)
        Me.GroupBox54.Controls.Add(Me.CBTagKissing)
        Me.GroupBox54.Location = New System.Drawing.Point(450, 38)
        Me.GroupBox54.Name = "GroupBox54"
        Me.GroupBox54.Size = New System.Drawing.Size(135, 238)
        Me.GroupBox54.TabIndex = 239
        Me.GroupBox54.TabStop = False
        Me.GroupBox54.Text = "Misc"
        '
        'CBTagTattoos
        '
        Me.CBTagTattoos.AutoSize = True
        Me.CBTagTattoos.Enabled = False
        Me.CBTagTattoos.ForeColor = System.Drawing.Color.Black
        Me.CBTagTattoos.Location = New System.Drawing.Point(15, 97)
        Me.CBTagTattoos.Name = "CBTagTattoos"
        Me.CBTagTattoos.Size = New System.Drawing.Size(62, 17)
        Me.CBTagTattoos.TabIndex = 214
        Me.CBTagTattoos.Text = "Tattoos"
        Me.CBTagTattoos.UseVisualStyleBackColor = True
        '
        'CBTagAnalToy
        '
        Me.CBTagAnalToy.AutoSize = True
        Me.CBTagAnalToy.Enabled = False
        Me.CBTagAnalToy.ForeColor = System.Drawing.Color.Black
        Me.CBTagAnalToy.Location = New System.Drawing.Point(15, 197)
        Me.CBTagAnalToy.Name = "CBTagAnalToy"
        Me.CBTagAnalToy.Size = New System.Drawing.Size(68, 17)
        Me.CBTagAnalToy.TabIndex = 215
        Me.CBTagAnalToy.Text = "Anal Toy"
        Me.CBTagAnalToy.UseVisualStyleBackColor = True
        '
        'CBTagDomme
        '
        Me.CBTagDomme.AutoSize = True
        Me.CBTagDomme.Enabled = False
        Me.CBTagDomme.ForeColor = System.Drawing.Color.Black
        Me.CBTagDomme.Location = New System.Drawing.Point(15, 17)
        Me.CBTagDomme.Name = "CBTagDomme"
        Me.CBTagDomme.Size = New System.Drawing.Size(114, 17)
        Me.CBTagDomme.TabIndex = 219
        Me.CBTagDomme.Text = "Tease A.I. Domme"
        Me.CBTagDomme.UseVisualStyleBackColor = True
        '
        'CBTagPocketPussy
        '
        Me.CBTagPocketPussy.AutoSize = True
        Me.CBTagPocketPussy.Enabled = False
        Me.CBTagPocketPussy.ForeColor = System.Drawing.Color.Black
        Me.CBTagPocketPussy.Location = New System.Drawing.Point(15, 177)
        Me.CBTagPocketPussy.Name = "CBTagPocketPussy"
        Me.CBTagPocketPussy.Size = New System.Drawing.Size(91, 17)
        Me.CBTagPocketPussy.TabIndex = 205
        Me.CBTagPocketPussy.Text = "Pocket Pussy"
        Me.CBTagPocketPussy.UseVisualStyleBackColor = True
        '
        'CBTagWatersports
        '
        Me.CBTagWatersports.AutoSize = True
        Me.CBTagWatersports.Enabled = False
        Me.CBTagWatersports.ForeColor = System.Drawing.Color.Black
        Me.CBTagWatersports.Location = New System.Drawing.Point(15, 217)
        Me.CBTagWatersports.Name = "CBTagWatersports"
        Me.CBTagWatersports.Size = New System.Drawing.Size(83, 17)
        Me.CBTagWatersports.TabIndex = 218
        Me.CBTagWatersports.Text = "Watersports"
        Me.CBTagWatersports.UseVisualStyleBackColor = True
        '
        'CBTagStockings
        '
        Me.CBTagStockings.AutoSize = True
        Me.CBTagStockings.Enabled = False
        Me.CBTagStockings.ForeColor = System.Drawing.Color.Black
        Me.CBTagStockings.Location = New System.Drawing.Point(15, 117)
        Me.CBTagStockings.Name = "CBTagStockings"
        Me.CBTagStockings.Size = New System.Drawing.Size(73, 17)
        Me.CBTagStockings.TabIndex = 217
        Me.CBTagStockings.Text = "Stockings"
        Me.CBTagStockings.UseVisualStyleBackColor = True
        '
        'CBTagCumshot
        '
        Me.CBTagCumshot.AutoSize = True
        Me.CBTagCumshot.Enabled = False
        Me.CBTagCumshot.ForeColor = System.Drawing.Color.Black
        Me.CBTagCumshot.Location = New System.Drawing.Point(15, 37)
        Me.CBTagCumshot.Name = "CBTagCumshot"
        Me.CBTagCumshot.Size = New System.Drawing.Size(67, 17)
        Me.CBTagCumshot.TabIndex = 206
        Me.CBTagCumshot.Text = "Cumshot"
        Me.CBTagCumshot.UseVisualStyleBackColor = True
        '
        'CBTagCumEating
        '
        Me.CBTagCumEating.AutoSize = True
        Me.CBTagCumEating.Enabled = False
        Me.CBTagCumEating.ForeColor = System.Drawing.Color.Black
        Me.CBTagCumEating.Location = New System.Drawing.Point(15, 57)
        Me.CBTagCumEating.Name = "CBTagCumEating"
        Me.CBTagCumEating.Size = New System.Drawing.Size(80, 17)
        Me.CBTagCumEating.TabIndex = 204
        Me.CBTagCumEating.Text = "Cum Eating"
        Me.CBTagCumEating.UseVisualStyleBackColor = True
        '
        'CBTagVibrator
        '
        Me.CBTagVibrator.AutoSize = True
        Me.CBTagVibrator.Enabled = False
        Me.CBTagVibrator.ForeColor = System.Drawing.Color.Black
        Me.CBTagVibrator.Location = New System.Drawing.Point(15, 137)
        Me.CBTagVibrator.Name = "CBTagVibrator"
        Me.CBTagVibrator.Size = New System.Drawing.Size(62, 17)
        Me.CBTagVibrator.TabIndex = 210
        Me.CBTagVibrator.Text = "Vibrator"
        Me.CBTagVibrator.UseVisualStyleBackColor = True
        '
        'CBTagDildo
        '
        Me.CBTagDildo.AutoSize = True
        Me.CBTagDildo.Enabled = False
        Me.CBTagDildo.ForeColor = System.Drawing.Color.Black
        Me.CBTagDildo.Location = New System.Drawing.Point(15, 157)
        Me.CBTagDildo.Name = "CBTagDildo"
        Me.CBTagDildo.Size = New System.Drawing.Size(50, 17)
        Me.CBTagDildo.TabIndex = 213
        Me.CBTagDildo.Text = "Dildo"
        Me.CBTagDildo.UseVisualStyleBackColor = True
        '
        'CBTagKissing
        '
        Me.CBTagKissing.AutoSize = True
        Me.CBTagKissing.Enabled = False
        Me.CBTagKissing.ForeColor = System.Drawing.Color.Black
        Me.CBTagKissing.Location = New System.Drawing.Point(15, 77)
        Me.CBTagKissing.Name = "CBTagKissing"
        Me.CBTagKissing.Size = New System.Drawing.Size(59, 17)
        Me.CBTagKissing.TabIndex = 203
        Me.CBTagKissing.Text = "Kissing"
        Me.CBTagKissing.UseVisualStyleBackColor = True
        '
        'BdsmTagGroup
        '
        Me.BdsmTagGroup.Controls.Add(Me.CBTagBallTorture)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagGag)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagBlindfold)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagWhipping)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagCockTorture)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagElectro)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagHotWax)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagClamps)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagStrapon)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagSpanking)
        Me.BdsmTagGroup.Controls.Add(Me.CBTagNeedles)
        Me.BdsmTagGroup.Location = New System.Drawing.Point(341, 359)
        Me.BdsmTagGroup.Name = "BdsmTagGroup"
        Me.BdsmTagGroup.Size = New System.Drawing.Size(103, 238)
        Me.BdsmTagGroup.TabIndex = 238
        Me.BdsmTagGroup.TabStop = False
        Me.BdsmTagGroup.Text = "BDSM"
        '
        'CBTagBallTorture
        '
        Me.CBTagBallTorture.AutoSize = True
        Me.CBTagBallTorture.Enabled = False
        Me.CBTagBallTorture.ForeColor = System.Drawing.Color.Black
        Me.CBTagBallTorture.Location = New System.Drawing.Point(15, 77)
        Me.CBTagBallTorture.Name = "CBTagBallTorture"
        Me.CBTagBallTorture.Size = New System.Drawing.Size(80, 17)
        Me.CBTagBallTorture.TabIndex = 220
        Me.CBTagBallTorture.Text = "Ball Torture"
        Me.CBTagBallTorture.UseVisualStyleBackColor = True
        '
        'CBTagGag
        '
        Me.CBTagGag.AutoSize = True
        Me.CBTagGag.Enabled = False
        Me.CBTagGag.ForeColor = System.Drawing.Color.Black
        Me.CBTagGag.Location = New System.Drawing.Point(15, 137)
        Me.CBTagGag.Name = "CBTagGag"
        Me.CBTagGag.Size = New System.Drawing.Size(46, 17)
        Me.CBTagGag.TabIndex = 214
        Me.CBTagGag.Text = "Gag"
        Me.CBTagGag.UseVisualStyleBackColor = True
        '
        'CBTagBlindfold
        '
        Me.CBTagBlindfold.AutoSize = True
        Me.CBTagBlindfold.Enabled = False
        Me.CBTagBlindfold.ForeColor = System.Drawing.Color.Black
        Me.CBTagBlindfold.Location = New System.Drawing.Point(15, 117)
        Me.CBTagBlindfold.Name = "CBTagBlindfold"
        Me.CBTagBlindfold.Size = New System.Drawing.Size(66, 17)
        Me.CBTagBlindfold.TabIndex = 208
        Me.CBTagBlindfold.Text = "Blindfold"
        Me.CBTagBlindfold.UseVisualStyleBackColor = True
        '
        'CBTagWhipping
        '
        Me.CBTagWhipping.AutoSize = True
        Me.CBTagWhipping.Enabled = False
        Me.CBTagWhipping.ForeColor = System.Drawing.Color.Black
        Me.CBTagWhipping.Location = New System.Drawing.Point(15, 17)
        Me.CBTagWhipping.Name = "CBTagWhipping"
        Me.CBTagWhipping.Size = New System.Drawing.Size(71, 17)
        Me.CBTagWhipping.TabIndex = 203
        Me.CBTagWhipping.Text = "Whipping"
        Me.CBTagWhipping.UseVisualStyleBackColor = True
        '
        'CBTagCockTorture
        '
        Me.CBTagCockTorture.AutoSize = True
        Me.CBTagCockTorture.Enabled = False
        Me.CBTagCockTorture.ForeColor = System.Drawing.Color.Black
        Me.CBTagCockTorture.Location = New System.Drawing.Point(15, 57)
        Me.CBTagCockTorture.Name = "CBTagCockTorture"
        Me.CBTagCockTorture.Size = New System.Drawing.Size(88, 17)
        Me.CBTagCockTorture.TabIndex = 204
        Me.CBTagCockTorture.Text = "Cock Torture"
        Me.CBTagCockTorture.UseVisualStyleBackColor = True
        '
        'CBTagElectro
        '
        Me.CBTagElectro.AutoSize = True
        Me.CBTagElectro.Enabled = False
        Me.CBTagElectro.ForeColor = System.Drawing.Color.Black
        Me.CBTagElectro.Location = New System.Drawing.Point(15, 217)
        Me.CBTagElectro.Name = "CBTagElectro"
        Me.CBTagElectro.Size = New System.Drawing.Size(59, 17)
        Me.CBTagElectro.TabIndex = 207
        Me.CBTagElectro.Text = "Electro"
        Me.CBTagElectro.UseVisualStyleBackColor = True
        '
        'CBTagHotWax
        '
        Me.CBTagHotWax.AutoSize = True
        Me.CBTagHotWax.Enabled = False
        Me.CBTagHotWax.ForeColor = System.Drawing.Color.Black
        Me.CBTagHotWax.Location = New System.Drawing.Point(15, 177)
        Me.CBTagHotWax.Name = "CBTagHotWax"
        Me.CBTagHotWax.Size = New System.Drawing.Size(68, 17)
        Me.CBTagHotWax.TabIndex = 213
        Me.CBTagHotWax.Text = "Hot Wax"
        Me.CBTagHotWax.UseVisualStyleBackColor = True
        '
        'CBTagClamps
        '
        Me.CBTagClamps.AutoSize = True
        Me.CBTagClamps.Enabled = False
        Me.CBTagClamps.ForeColor = System.Drawing.Color.Black
        Me.CBTagClamps.Location = New System.Drawing.Point(15, 157)
        Me.CBTagClamps.Name = "CBTagClamps"
        Me.CBTagClamps.Size = New System.Drawing.Size(60, 17)
        Me.CBTagClamps.TabIndex = 210
        Me.CBTagClamps.Text = "Clamps"
        Me.CBTagClamps.UseVisualStyleBackColor = True
        '
        'CBTagStrapon
        '
        Me.CBTagStrapon.AutoSize = True
        Me.CBTagStrapon.Enabled = False
        Me.CBTagStrapon.ForeColor = System.Drawing.Color.Black
        Me.CBTagStrapon.Location = New System.Drawing.Point(15, 97)
        Me.CBTagStrapon.Name = "CBTagStrapon"
        Me.CBTagStrapon.Size = New System.Drawing.Size(66, 17)
        Me.CBTagStrapon.TabIndex = 205
        Me.CBTagStrapon.Text = "Strap-on"
        Me.CBTagStrapon.UseVisualStyleBackColor = True
        '
        'CBTagSpanking
        '
        Me.CBTagSpanking.AutoSize = True
        Me.CBTagSpanking.Enabled = False
        Me.CBTagSpanking.ForeColor = System.Drawing.Color.Black
        Me.CBTagSpanking.Location = New System.Drawing.Point(15, 37)
        Me.CBTagSpanking.Name = "CBTagSpanking"
        Me.CBTagSpanking.Size = New System.Drawing.Size(71, 17)
        Me.CBTagSpanking.TabIndex = 206
        Me.CBTagSpanking.Text = "Spanking"
        Me.CBTagSpanking.UseVisualStyleBackColor = True
        '
        'CBTagNeedles
        '
        Me.CBTagNeedles.AutoSize = True
        Me.CBTagNeedles.Enabled = False
        Me.CBTagNeedles.ForeColor = System.Drawing.Color.Black
        Me.CBTagNeedles.Location = New System.Drawing.Point(15, 197)
        Me.CBTagNeedles.Name = "CBTagNeedles"
        Me.CBTagNeedles.Size = New System.Drawing.Size(65, 17)
        Me.CBTagNeedles.TabIndex = 209
        Me.CBTagNeedles.Text = "Needles"
        Me.CBTagNeedles.UseVisualStyleBackColor = True
        '
        'GroupBox50
        '
        Me.GroupBox50.Controls.Add(Me.CBTagRimming)
        Me.GroupBox50.Controls.Add(Me.CBTagFacesitting)
        Me.GroupBox50.Controls.Add(Me.CBTagMissionary)
        Me.GroupBox50.Controls.Add(Me.CBTagMasturbation)
        Me.GroupBox50.Controls.Add(Me.CBTagRCowgirl)
        Me.GroupBox50.Controls.Add(Me.CBTagFingering)
        Me.GroupBox50.Controls.Add(Me.CBTagGangbang)
        Me.GroupBox50.Controls.Add(Me.CBTagBlowjob)
        Me.GroupBox50.Controls.Add(Me.CBTagDP)
        Me.GroupBox50.Controls.Add(Me.CBTagHandjob)
        Me.GroupBox50.Controls.Add(Me.CBTagStanding)
        Me.GroupBox50.Controls.Add(Me.CBTagFootjob)
        Me.GroupBox50.Controls.Add(Me.CBTagCowgirl)
        Me.GroupBox50.Controls.Add(Me.CBTagDoggyStyle)
        Me.GroupBox50.Controls.Add(Me.CBTagTitjob)
        Me.GroupBox50.Controls.Add(Me.CBTagCunnilingus)
        Me.GroupBox50.Controls.Add(Me.CBTagAnalSex)
        Me.GroupBox50.Location = New System.Drawing.Point(119, 115)
        Me.GroupBox50.Name = "GroupBox50"
        Me.GroupBox50.Size = New System.Drawing.Size(105, 358)
        Me.GroupBox50.TabIndex = 237
        Me.GroupBox50.TabStop = False
        Me.GroupBox50.Text = "Sex"
        '
        'CBTagRimming
        '
        Me.CBTagRimming.AutoSize = True
        Me.CBTagRimming.Enabled = False
        Me.CBTagRimming.ForeColor = System.Drawing.Color.Black
        Me.CBTagRimming.Location = New System.Drawing.Point(15, 177)
        Me.CBTagRimming.Name = "CBTagRimming"
        Me.CBTagRimming.Size = New System.Drawing.Size(66, 17)
        Me.CBTagRimming.TabIndex = 219
        Me.CBTagRimming.Text = "Rimming"
        Me.CBTagRimming.UseVisualStyleBackColor = True
        '
        'CBTagFacesitting
        '
        Me.CBTagFacesitting.AutoSize = True
        Me.CBTagFacesitting.Enabled = False
        Me.CBTagFacesitting.ForeColor = System.Drawing.Color.Black
        Me.CBTagFacesitting.Location = New System.Drawing.Point(15, 157)
        Me.CBTagFacesitting.Name = "CBTagFacesitting"
        Me.CBTagFacesitting.Size = New System.Drawing.Size(77, 17)
        Me.CBTagFacesitting.TabIndex = 226
        Me.CBTagFacesitting.Text = "Facesitting"
        Me.CBTagFacesitting.UseVisualStyleBackColor = True
        '
        'CBTagMissionary
        '
        Me.CBTagMissionary.AutoSize = True
        Me.CBTagMissionary.Enabled = False
        Me.CBTagMissionary.ForeColor = System.Drawing.Color.Black
        Me.CBTagMissionary.Location = New System.Drawing.Point(15, 197)
        Me.CBTagMissionary.Name = "CBTagMissionary"
        Me.CBTagMissionary.Size = New System.Drawing.Size(75, 17)
        Me.CBTagMissionary.TabIndex = 208
        Me.CBTagMissionary.Text = "Missionary"
        Me.CBTagMissionary.UseVisualStyleBackColor = True
        '
        'CBTagMasturbation
        '
        Me.CBTagMasturbation.AutoSize = True
        Me.CBTagMasturbation.Enabled = False
        Me.CBTagMasturbation.ForeColor = System.Drawing.Color.Black
        Me.CBTagMasturbation.Location = New System.Drawing.Point(15, 17)
        Me.CBTagMasturbation.Name = "CBTagMasturbation"
        Me.CBTagMasturbation.Size = New System.Drawing.Size(87, 17)
        Me.CBTagMasturbation.TabIndex = 203
        Me.CBTagMasturbation.Text = "Masturbation"
        Me.CBTagMasturbation.UseVisualStyleBackColor = True
        '
        'CBTagRCowgirl
        '
        Me.CBTagRCowgirl.AutoSize = True
        Me.CBTagRCowgirl.Enabled = False
        Me.CBTagRCowgirl.ForeColor = System.Drawing.Color.Black
        Me.CBTagRCowgirl.Location = New System.Drawing.Point(15, 257)
        Me.CBTagRCowgirl.Name = "CBTagRCowgirl"
        Me.CBTagRCowgirl.Size = New System.Drawing.Size(74, 17)
        Me.CBTagRCowgirl.TabIndex = 218
        Me.CBTagRCowgirl.Text = "R. Cowgirl"
        Me.CBTagRCowgirl.UseVisualStyleBackColor = True
        '
        'CBTagFingering
        '
        Me.CBTagFingering.AutoSize = True
        Me.CBTagFingering.Enabled = False
        Me.CBTagFingering.ForeColor = System.Drawing.Color.Black
        Me.CBTagFingering.Location = New System.Drawing.Point(15, 57)
        Me.CBTagFingering.Name = "CBTagFingering"
        Me.CBTagFingering.Size = New System.Drawing.Size(69, 17)
        Me.CBTagFingering.TabIndex = 204
        Me.CBTagFingering.Text = "Fingering"
        Me.CBTagFingering.UseVisualStyleBackColor = True
        '
        'CBTagGangbang
        '
        Me.CBTagGangbang.AutoSize = True
        Me.CBTagGangbang.Enabled = False
        Me.CBTagGangbang.ForeColor = System.Drawing.Color.Black
        Me.CBTagGangbang.Location = New System.Drawing.Point(15, 337)
        Me.CBTagGangbang.Name = "CBTagGangbang"
        Me.CBTagGangbang.Size = New System.Drawing.Size(76, 17)
        Me.CBTagGangbang.TabIndex = 217
        Me.CBTagGangbang.Text = "Gangbang"
        Me.CBTagGangbang.UseVisualStyleBackColor = True
        '
        'CBTagBlowjob
        '
        Me.CBTagBlowjob.AutoSize = True
        Me.CBTagBlowjob.Enabled = False
        Me.CBTagBlowjob.ForeColor = System.Drawing.Color.Black
        Me.CBTagBlowjob.Location = New System.Drawing.Point(15, 77)
        Me.CBTagBlowjob.Name = "CBTagBlowjob"
        Me.CBTagBlowjob.Size = New System.Drawing.Size(63, 17)
        Me.CBTagBlowjob.TabIndex = 205
        Me.CBTagBlowjob.Text = "Blowjob"
        Me.CBTagBlowjob.UseVisualStyleBackColor = True
        '
        'CBTagDP
        '
        Me.CBTagDP.AutoSize = True
        Me.CBTagDP.Enabled = False
        Me.CBTagDP.ForeColor = System.Drawing.Color.Black
        Me.CBTagDP.Location = New System.Drawing.Point(15, 317)
        Me.CBTagDP.Name = "CBTagDP"
        Me.CBTagDP.Size = New System.Drawing.Size(41, 17)
        Me.CBTagDP.TabIndex = 216
        Me.CBTagDP.Text = "DP"
        Me.CBTagDP.UseVisualStyleBackColor = True
        '
        'CBTagHandjob
        '
        Me.CBTagHandjob.AutoSize = True
        Me.CBTagHandjob.Enabled = False
        Me.CBTagHandjob.ForeColor = System.Drawing.Color.Black
        Me.CBTagHandjob.Location = New System.Drawing.Point(15, 37)
        Me.CBTagHandjob.Name = "CBTagHandjob"
        Me.CBTagHandjob.Size = New System.Drawing.Size(66, 17)
        Me.CBTagHandjob.TabIndex = 206
        Me.CBTagHandjob.Text = "Handjob"
        Me.CBTagHandjob.UseVisualStyleBackColor = True
        '
        'CBTagStanding
        '
        Me.CBTagStanding.AutoSize = True
        Me.CBTagStanding.Enabled = False
        Me.CBTagStanding.ForeColor = System.Drawing.Color.Black
        Me.CBTagStanding.Location = New System.Drawing.Point(15, 277)
        Me.CBTagStanding.Name = "CBTagStanding"
        Me.CBTagStanding.Size = New System.Drawing.Size(68, 17)
        Me.CBTagStanding.TabIndex = 215
        Me.CBTagStanding.Text = "Standing"
        Me.CBTagStanding.UseVisualStyleBackColor = True
        '
        'CBTagFootjob
        '
        Me.CBTagFootjob.AutoSize = True
        Me.CBTagFootjob.Enabled = False
        Me.CBTagFootjob.ForeColor = System.Drawing.Color.Black
        Me.CBTagFootjob.Location = New System.Drawing.Point(15, 137)
        Me.CBTagFootjob.Name = "CBTagFootjob"
        Me.CBTagFootjob.Size = New System.Drawing.Size(61, 17)
        Me.CBTagFootjob.TabIndex = 207
        Me.CBTagFootjob.Text = "Footjob"
        Me.CBTagFootjob.UseVisualStyleBackColor = True
        '
        'CBTagCowgirl
        '
        Me.CBTagCowgirl.AutoSize = True
        Me.CBTagCowgirl.Enabled = False
        Me.CBTagCowgirl.ForeColor = System.Drawing.Color.Black
        Me.CBTagCowgirl.Location = New System.Drawing.Point(15, 237)
        Me.CBTagCowgirl.Name = "CBTagCowgirl"
        Me.CBTagCowgirl.Size = New System.Drawing.Size(60, 17)
        Me.CBTagCowgirl.TabIndex = 214
        Me.CBTagCowgirl.Text = "Cowgirl"
        Me.CBTagCowgirl.UseVisualStyleBackColor = True
        '
        'CBTagDoggyStyle
        '
        Me.CBTagDoggyStyle.AutoSize = True
        Me.CBTagDoggyStyle.Enabled = False
        Me.CBTagDoggyStyle.ForeColor = System.Drawing.Color.Black
        Me.CBTagDoggyStyle.Location = New System.Drawing.Point(15, 217)
        Me.CBTagDoggyStyle.Name = "CBTagDoggyStyle"
        Me.CBTagDoggyStyle.Size = New System.Drawing.Size(83, 17)
        Me.CBTagDoggyStyle.TabIndex = 209
        Me.CBTagDoggyStyle.Text = "Doggy Style"
        Me.CBTagDoggyStyle.UseVisualStyleBackColor = True
        '
        'CBTagTitjob
        '
        Me.CBTagTitjob.AutoSize = True
        Me.CBTagTitjob.Enabled = False
        Me.CBTagTitjob.ForeColor = System.Drawing.Color.Black
        Me.CBTagTitjob.Location = New System.Drawing.Point(15, 117)
        Me.CBTagTitjob.Name = "CBTagTitjob"
        Me.CBTagTitjob.Size = New System.Drawing.Size(52, 17)
        Me.CBTagTitjob.TabIndex = 213
        Me.CBTagTitjob.Text = "Titjob"
        Me.CBTagTitjob.UseVisualStyleBackColor = True
        '
        'CBTagCunnilingus
        '
        Me.CBTagCunnilingus.AutoSize = True
        Me.CBTagCunnilingus.Enabled = False
        Me.CBTagCunnilingus.ForeColor = System.Drawing.Color.Black
        Me.CBTagCunnilingus.Location = New System.Drawing.Point(15, 97)
        Me.CBTagCunnilingus.Name = "CBTagCunnilingus"
        Me.CBTagCunnilingus.Size = New System.Drawing.Size(80, 17)
        Me.CBTagCunnilingus.TabIndex = 210
        Me.CBTagCunnilingus.Text = "Cunnilingus"
        Me.CBTagCunnilingus.UseVisualStyleBackColor = True
        '
        'CBTagAnalSex
        '
        Me.CBTagAnalSex.AutoSize = True
        Me.CBTagAnalSex.Enabled = False
        Me.CBTagAnalSex.ForeColor = System.Drawing.Color.Black
        Me.CBTagAnalSex.Location = New System.Drawing.Point(15, 297)
        Me.CBTagAnalSex.Name = "CBTagAnalSex"
        Me.CBTagAnalSex.Size = New System.Drawing.Size(68, 17)
        Me.CBTagAnalSex.TabIndex = 212
        Me.CBTagAnalSex.Text = "Anal Sex"
        Me.CBTagAnalSex.UseVisualStyleBackColor = True
        '
        'GroupBox48
        '
        Me.GroupBox48.Controls.Add(Me.CBTagArtwork)
        Me.GroupBox48.Controls.Add(Me.CBTagOutdoors)
        Me.GroupBox48.Controls.Add(Me.CBTagPOV)
        Me.GroupBox48.Controls.Add(Me.CBTagHardcore)
        Me.GroupBox48.Controls.Add(Me.CBTagTD)
        Me.GroupBox48.Controls.Add(Me.CBTagGay)
        Me.GroupBox48.Controls.Add(Me.CBTagBath)
        Me.GroupBox48.Controls.Add(Me.CBTagBisexual)
        Me.GroupBox48.Controls.Add(Me.CBTagCFNM)
        Me.GroupBox48.Controls.Add(Me.CBTagLesbian)
        Me.GroupBox48.Controls.Add(Me.CBTagSoloFuta)
        Me.GroupBox48.Controls.Add(Me.CBTagSM)
        Me.GroupBox48.Controls.Add(Me.CBTagBondage)
        Me.GroupBox48.Controls.Add(Me.CBTagSoloM)
        Me.GroupBox48.Controls.Add(Me.CBTagSoloF)
        Me.GroupBox48.Controls.Add(Me.CBTagChastity)
        Me.GroupBox48.Controls.Add(Me.CBTagShower)
        Me.GroupBox48.Location = New System.Drawing.Point(8, 115)
        Me.GroupBox48.Name = "GroupBox48"
        Me.GroupBox48.Size = New System.Drawing.Size(105, 358)
        Me.GroupBox48.TabIndex = 235
        Me.GroupBox48.TabStop = False
        Me.GroupBox48.Text = "Category"
        '
        'CBTagArtwork
        '
        Me.CBTagArtwork.AutoSize = True
        Me.CBTagArtwork.Enabled = False
        Me.CBTagArtwork.ForeColor = System.Drawing.Color.Black
        Me.CBTagArtwork.Location = New System.Drawing.Point(15, 337)
        Me.CBTagArtwork.Name = "CBTagArtwork"
        Me.CBTagArtwork.Size = New System.Drawing.Size(62, 17)
        Me.CBTagArtwork.TabIndex = 225
        Me.CBTagArtwork.Text = "Artwork"
        Me.CBTagArtwork.UseVisualStyleBackColor = True
        '
        'CBTagOutdoors
        '
        Me.CBTagOutdoors.AutoSize = True
        Me.CBTagOutdoors.Enabled = False
        Me.CBTagOutdoors.ForeColor = System.Drawing.Color.Black
        Me.CBTagOutdoors.Location = New System.Drawing.Point(15, 317)
        Me.CBTagOutdoors.Name = "CBTagOutdoors"
        Me.CBTagOutdoors.Size = New System.Drawing.Size(69, 17)
        Me.CBTagOutdoors.TabIndex = 219
        Me.CBTagOutdoors.Text = "Outdoors"
        Me.CBTagOutdoors.UseVisualStyleBackColor = True
        '
        'CBTagPOV
        '
        Me.CBTagPOV.AutoSize = True
        Me.CBTagPOV.Enabled = False
        Me.CBTagPOV.ForeColor = System.Drawing.Color.Black
        Me.CBTagPOV.Location = New System.Drawing.Point(15, 157)
        Me.CBTagPOV.Name = "CBTagPOV"
        Me.CBTagPOV.Size = New System.Drawing.Size(48, 17)
        Me.CBTagPOV.TabIndex = 208
        Me.CBTagPOV.Text = "POV"
        Me.CBTagPOV.UseVisualStyleBackColor = True
        '
        'CBTagHardcore
        '
        Me.CBTagHardcore.AutoSize = True
        Me.CBTagHardcore.Enabled = False
        Me.CBTagHardcore.ForeColor = System.Drawing.Color.Black
        Me.CBTagHardcore.Location = New System.Drawing.Point(15, 17)
        Me.CBTagHardcore.Name = "CBTagHardcore"
        Me.CBTagHardcore.Size = New System.Drawing.Size(70, 17)
        Me.CBTagHardcore.TabIndex = 5
        Me.CBTagHardcore.Text = "Hardcore"
        Me.CBTagHardcore.UseVisualStyleBackColor = True
        '
        'CBTagTD
        '
        Me.CBTagTD.AutoSize = True
        Me.CBTagTD.Enabled = False
        Me.CBTagTD.ForeColor = System.Drawing.Color.Black
        Me.CBTagTD.Location = New System.Drawing.Point(15, 217)
        Me.CBTagTD.Name = "CBTagTD"
        Me.CBTagTD.Size = New System.Drawing.Size(47, 17)
        Me.CBTagTD.TabIndex = 218
        Me.CBTagTD.Text = "T&&D"
        Me.CBTagTD.UseVisualStyleBackColor = True
        '
        'CBTagGay
        '
        Me.CBTagGay.AutoSize = True
        Me.CBTagGay.Enabled = False
        Me.CBTagGay.ForeColor = System.Drawing.Color.Black
        Me.CBTagGay.Location = New System.Drawing.Point(15, 57)
        Me.CBTagGay.Name = "CBTagGay"
        Me.CBTagGay.Size = New System.Drawing.Size(45, 17)
        Me.CBTagGay.TabIndex = 7
        Me.CBTagGay.Text = "Gay"
        Me.CBTagGay.UseVisualStyleBackColor = True
        '
        'CBTagBath
        '
        Me.CBTagBath.AutoSize = True
        Me.CBTagBath.Enabled = False
        Me.CBTagBath.ForeColor = System.Drawing.Color.Black
        Me.CBTagBath.Location = New System.Drawing.Point(15, 277)
        Me.CBTagBath.Name = "CBTagBath"
        Me.CBTagBath.Size = New System.Drawing.Size(48, 17)
        Me.CBTagBath.TabIndex = 217
        Me.CBTagBath.Text = "Bath"
        Me.CBTagBath.UseVisualStyleBackColor = True
        '
        'CBTagBisexual
        '
        Me.CBTagBisexual.AutoSize = True
        Me.CBTagBisexual.Enabled = False
        Me.CBTagBisexual.ForeColor = System.Drawing.Color.Black
        Me.CBTagBisexual.Location = New System.Drawing.Point(15, 77)
        Me.CBTagBisexual.Name = "CBTagBisexual"
        Me.CBTagBisexual.Size = New System.Drawing.Size(65, 17)
        Me.CBTagBisexual.TabIndex = 7
        Me.CBTagBisexual.Text = "Bisexual"
        Me.CBTagBisexual.UseVisualStyleBackColor = True
        '
        'CBTagCFNM
        '
        Me.CBTagCFNM.AutoSize = True
        Me.CBTagCFNM.Enabled = False
        Me.CBTagCFNM.ForeColor = System.Drawing.Color.Black
        Me.CBTagCFNM.Location = New System.Drawing.Point(15, 257)
        Me.CBTagCFNM.Name = "CBTagCFNM"
        Me.CBTagCFNM.Size = New System.Drawing.Size(56, 17)
        Me.CBTagCFNM.TabIndex = 216
        Me.CBTagCFNM.Text = "CFNM"
        Me.CBTagCFNM.UseVisualStyleBackColor = True
        '
        'CBTagLesbian
        '
        Me.CBTagLesbian.AutoSize = True
        Me.CBTagLesbian.Enabled = False
        Me.CBTagLesbian.ForeColor = System.Drawing.Color.Black
        Me.CBTagLesbian.Location = New System.Drawing.Point(15, 37)
        Me.CBTagLesbian.Name = "CBTagLesbian"
        Me.CBTagLesbian.Size = New System.Drawing.Size(63, 17)
        Me.CBTagLesbian.TabIndex = 6
        Me.CBTagLesbian.Text = "Lesbian"
        Me.CBTagLesbian.UseVisualStyleBackColor = True
        '
        'CBTagSoloFuta
        '
        Me.CBTagSoloFuta.AutoSize = True
        Me.CBTagSoloFuta.Enabled = False
        Me.CBTagSoloFuta.ForeColor = System.Drawing.Color.Black
        Me.CBTagSoloFuta.Location = New System.Drawing.Point(15, 137)
        Me.CBTagSoloFuta.Name = "CBTagSoloFuta"
        Me.CBTagSoloFuta.Size = New System.Drawing.Size(71, 17)
        Me.CBTagSoloFuta.TabIndex = 207
        Me.CBTagSoloFuta.Text = "Solo Futa"
        Me.CBTagSoloFuta.UseVisualStyleBackColor = True
        '
        'CBTagSM
        '
        Me.CBTagSM.AutoSize = True
        Me.CBTagSM.Enabled = False
        Me.CBTagSM.ForeColor = System.Drawing.Color.Black
        Me.CBTagSM.Location = New System.Drawing.Point(15, 197)
        Me.CBTagSM.Name = "CBTagSM"
        Me.CBTagSM.Size = New System.Drawing.Size(48, 17)
        Me.CBTagSM.TabIndex = 214
        Me.CBTagSM.Text = "S&&M"
        Me.CBTagSM.UseVisualStyleBackColor = True
        '
        'CBTagBondage
        '
        Me.CBTagBondage.AutoSize = True
        Me.CBTagBondage.Enabled = False
        Me.CBTagBondage.ForeColor = System.Drawing.Color.Black
        Me.CBTagBondage.Location = New System.Drawing.Point(15, 177)
        Me.CBTagBondage.Name = "CBTagBondage"
        Me.CBTagBondage.Size = New System.Drawing.Size(69, 17)
        Me.CBTagBondage.TabIndex = 209
        Me.CBTagBondage.Text = "Bondage"
        Me.CBTagBondage.UseVisualStyleBackColor = True
        '
        'CBTagSoloM
        '
        Me.CBTagSoloM.AutoSize = True
        Me.CBTagSoloM.Enabled = False
        Me.CBTagSoloM.ForeColor = System.Drawing.Color.Black
        Me.CBTagSoloM.Location = New System.Drawing.Point(15, 117)
        Me.CBTagSoloM.Name = "CBTagSoloM"
        Me.CBTagSoloM.Size = New System.Drawing.Size(59, 17)
        Me.CBTagSoloM.TabIndex = 213
        Me.CBTagSoloM.Text = "Solo M"
        Me.CBTagSoloM.UseVisualStyleBackColor = True
        '
        'CBTagSoloF
        '
        Me.CBTagSoloF.AutoSize = True
        Me.CBTagSoloF.Enabled = False
        Me.CBTagSoloF.ForeColor = System.Drawing.Color.Black
        Me.CBTagSoloF.Location = New System.Drawing.Point(15, 97)
        Me.CBTagSoloF.Name = "CBTagSoloF"
        Me.CBTagSoloF.Size = New System.Drawing.Size(56, 17)
        Me.CBTagSoloF.TabIndex = 210
        Me.CBTagSoloF.Text = "Solo F"
        Me.CBTagSoloF.UseVisualStyleBackColor = True
        '
        'CBTagChastity
        '
        Me.CBTagChastity.AutoSize = True
        Me.CBTagChastity.Enabled = False
        Me.CBTagChastity.ForeColor = System.Drawing.Color.Black
        Me.CBTagChastity.Location = New System.Drawing.Point(15, 237)
        Me.CBTagChastity.Name = "CBTagChastity"
        Me.CBTagChastity.Size = New System.Drawing.Size(63, 17)
        Me.CBTagChastity.TabIndex = 212
        Me.CBTagChastity.Text = "Chastity"
        Me.CBTagChastity.UseVisualStyleBackColor = True
        '
        'CBTagShower
        '
        Me.CBTagShower.AutoSize = True
        Me.CBTagShower.Enabled = False
        Me.CBTagShower.ForeColor = System.Drawing.Color.Black
        Me.CBTagShower.Location = New System.Drawing.Point(15, 297)
        Me.CBTagShower.Name = "CBTagShower"
        Me.CBTagShower.Size = New System.Drawing.Size(62, 17)
        Me.CBTagShower.TabIndex = 211
        Me.CBTagShower.Text = "Shower"
        Me.CBTagShower.UseVisualStyleBackColor = True
        '
        'SaveTagButton
        '
        Me.SaveTagButton.Enabled = False
        Me.SaveTagButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveTagButton.Location = New System.Drawing.Point(832, 4)
        Me.SaveTagButton.Name = "SaveTagButton"
        Me.SaveTagButton.Size = New System.Drawing.Size(120, 49)
        Me.SaveTagButton.TabIndex = 229
        Me.SaveTagButton.Text = "Save Tags"
        Me.SaveTagButton.UseVisualStyleBackColor = True
        '
        'LocalTagPictureBox
        '
        Me.LocalTagPictureBox.BackColor = System.Drawing.Color.Black
        Me.LocalTagPictureBox.Location = New System.Drawing.Point(450, 282)
        Me.LocalTagPictureBox.Name = "LocalTagPictureBox"
        Me.LocalTagPictureBox.Size = New System.Drawing.Size(502, 311)
        Me.LocalTagPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.LocalTagPictureBox.TabIndex = 246
        Me.LocalTagPictureBox.TabStop = False
        '
        'UrlFilesTab
        '
        Me.UrlFilesTab.BackColor = System.Drawing.Color.Silver
        Me.UrlFilesTab.Controls.Add(Me.UrlFilesPanel)
        Me.UrlFilesTab.Location = New System.Drawing.Point(4, 22)
        Me.UrlFilesTab.Name = "UrlFilesTab"
        Me.UrlFilesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.UrlFilesTab.Size = New System.Drawing.Size(972, 456)
        Me.UrlFilesTab.TabIndex = 10
        Me.UrlFilesTab.Text = "URL Files"
        '
        'UrlFilesPanel
        '
        Me.UrlFilesPanel.BackColor = System.Drawing.Color.LightGray
        Me.UrlFilesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UrlFilesPanel.Controls.Add(Me.SelectBlogDropDown)
        Me.UrlFilesPanel.Controls.Add(Me.UrlImageContinueButton)
        Me.UrlFilesPanel.Controls.Add(Me.UrlImageAddAndContinue)
        Me.UrlFilesPanel.Controls.Add(Me.BTNWICancel)
        Me.UrlFilesPanel.Controls.Add(Me.CBWIReview)
        Me.UrlFilesPanel.Controls.Add(Me.BTNWIBrowse)
        Me.UrlFilesPanel.Controls.Add(Me.TBWIDirectory)
        Me.UrlFilesPanel.Controls.Add(Me.BTNWIDisliked)
        Me.UrlFilesPanel.Controls.Add(Me.BTNWILiked)
        Me.UrlFilesPanel.Controls.Add(Me.UrlImageRemoveButton)
        Me.UrlFilesPanel.Controls.Add(Me.CBWISaveToDisk)
        Me.UrlFilesPanel.Controls.Add(Me.PictureBox5)
        Me.UrlFilesPanel.Controls.Add(Me.WebImageProgressBar)
        Me.UrlFilesPanel.Controls.Add(Me.CreateBlogContainerButton)
        Me.UrlFilesPanel.Controls.Add(Me.LBLWebImageCount)
        Me.UrlFilesPanel.Controls.Add(Me.BTNWISave)
        Me.UrlFilesPanel.Controls.Add(Me.UrlFilesPreviousImageButton)
        Me.UrlFilesPanel.Controls.Add(Me.UrlFilesNextImageButton)
        Me.UrlFilesPanel.Controls.Add(Me.WebPictureBox)
        Me.UrlFilesPanel.Controls.Add(Me.ImageBlogs)
        Me.UrlFilesPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UrlFilesPanel.Location = New System.Drawing.Point(3, 3)
        Me.UrlFilesPanel.Name = "UrlFilesPanel"
        Me.UrlFilesPanel.Size = New System.Drawing.Size(966, 450)
        Me.UrlFilesPanel.TabIndex = 91
        '
        'SelectBlogDropDown
        '
        Me.SelectBlogDropDown.FormattingEnabled = True
        Me.SelectBlogDropDown.Location = New System.Drawing.Point(828, 247)
        Me.SelectBlogDropDown.Name = "SelectBlogDropDown"
        Me.SelectBlogDropDown.Size = New System.Drawing.Size(130, 21)
        Me.SelectBlogDropDown.TabIndex = 169
        '
        'UrlImageContinueButton
        '
        Me.UrlImageContinueButton.BackColor = System.Drawing.Color.LightGray
        Me.UrlImageContinueButton.Enabled = False
        Me.UrlImageContinueButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UrlImageContinueButton.ForeColor = System.Drawing.Color.Black
        Me.UrlImageContinueButton.Location = New System.Drawing.Point(827, 157)
        Me.UrlImageContinueButton.Name = "UrlImageContinueButton"
        Me.UrlImageContinueButton.Size = New System.Drawing.Size(131, 24)
        Me.UrlImageContinueButton.TabIndex = 168
        Me.UrlImageContinueButton.Text = "Continue"
        Me.UrlImageContinueButton.UseVisualStyleBackColor = False
        '
        'UrlImageAddAndContinue
        '
        Me.UrlImageAddAndContinue.BackColor = System.Drawing.Color.LightGray
        Me.UrlImageAddAndContinue.Enabled = False
        Me.UrlImageAddAndContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UrlImageAddAndContinue.ForeColor = System.Drawing.Color.Black
        Me.UrlImageAddAndContinue.Location = New System.Drawing.Point(827, 127)
        Me.UrlImageAddAndContinue.Name = "UrlImageAddAndContinue"
        Me.UrlImageAddAndContinue.Size = New System.Drawing.Size(131, 24)
        Me.UrlImageAddAndContinue.TabIndex = 167
        Me.UrlImageAddAndContinue.Text = "Add and Continue"
        Me.UrlImageAddAndContinue.UseVisualStyleBackColor = False
        '
        'BTNWICancel
        '
        Me.BTNWICancel.BackColor = System.Drawing.Color.LightGray
        Me.BTNWICancel.Enabled = False
        Me.BTNWICancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNWICancel.ForeColor = System.Drawing.Color.Black
        Me.BTNWICancel.Location = New System.Drawing.Point(827, 187)
        Me.BTNWICancel.Name = "BTNWICancel"
        Me.BTNWICancel.Size = New System.Drawing.Size(131, 24)
        Me.BTNWICancel.TabIndex = 166
        Me.BTNWICancel.Text = "Cancel"
        Me.BTNWICancel.UseVisualStyleBackColor = False
        '
        'CBWIReview
        '
        Me.CBWIReview.Location = New System.Drawing.Point(828, 68)
        Me.CBWIReview.Name = "CBWIReview"
        Me.CBWIReview.Size = New System.Drawing.Size(124, 30)
        Me.CBWIReview.TabIndex = 165
        Me.CBWIReview.Text = "Review Each Image"
        Me.CBWIReview.UseVisualStyleBackColor = True
        '
        'BTNWIBrowse
        '
        Me.BTNWIBrowse.BackColor = System.Drawing.Color.LightGray
        Me.BTNWIBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNWIBrowse.ForeColor = System.Drawing.Color.Black
        Me.BTNWIBrowse.Location = New System.Drawing.Point(105, 597)
        Me.BTNWIBrowse.Name = "BTNWIBrowse"
        Me.BTNWIBrowse.Size = New System.Drawing.Size(50, 24)
        Me.BTNWIBrowse.TabIndex = 163
        Me.BTNWIBrowse.Text = "Browse"
        Me.BTNWIBrowse.UseVisualStyleBackColor = False
        '
        'TBWIDirectory
        '
        Me.TBWIDirectory.BackColor = System.Drawing.Color.White
        Me.TBWIDirectory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBWIDirectory.ForeColor = System.Drawing.Color.Black
        Me.TBWIDirectory.Location = New System.Drawing.Point(161, 599)
        Me.TBWIDirectory.Name = "TBWIDirectory"
        Me.TBWIDirectory.Size = New System.Drawing.Size(400, 20)
        Me.TBWIDirectory.TabIndex = 164
        Me.TBWIDirectory.Text = "Saved Image Directory"
        '
        'BTNWIDisliked
        '
        Me.BTNWIDisliked.BackColor = System.Drawing.Color.LightGray
        Me.BTNWIDisliked.Enabled = False
        Me.BTNWIDisliked.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNWIDisliked.ForeColor = System.Drawing.Color.Black
        Me.BTNWIDisliked.Location = New System.Drawing.Point(828, 371)
        Me.BTNWIDisliked.Name = "BTNWIDisliked"
        Me.BTNWIDisliked.Size = New System.Drawing.Size(131, 24)
        Me.BTNWIDisliked.TabIndex = 162
        Me.BTNWIDisliked.Text = "Add to Disliked Images"
        Me.BTNWIDisliked.UseVisualStyleBackColor = False
        '
        'BTNWILiked
        '
        Me.BTNWILiked.BackColor = System.Drawing.Color.LightGray
        Me.BTNWILiked.Enabled = False
        Me.BTNWILiked.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNWILiked.ForeColor = System.Drawing.Color.Black
        Me.BTNWILiked.Location = New System.Drawing.Point(828, 341)
        Me.BTNWILiked.Name = "BTNWILiked"
        Me.BTNWILiked.Size = New System.Drawing.Size(131, 24)
        Me.BTNWILiked.TabIndex = 161
        Me.BTNWILiked.Text = "Add to Liked Images"
        Me.BTNWILiked.UseVisualStyleBackColor = False
        '
        'UrlImageRemoveButton
        '
        Me.UrlImageRemoveButton.BackColor = System.Drawing.Color.LightGray
        Me.UrlImageRemoveButton.Enabled = False
        Me.UrlImageRemoveButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UrlImageRemoveButton.ForeColor = System.Drawing.Color.Black
        Me.UrlImageRemoveButton.Location = New System.Drawing.Point(828, 311)
        Me.UrlImageRemoveButton.Name = "UrlImageRemoveButton"
        Me.UrlImageRemoveButton.Size = New System.Drawing.Size(131, 24)
        Me.UrlImageRemoveButton.TabIndex = 160
        Me.UrlImageRemoveButton.Text = "Remove From URL File"
        Me.UrlImageRemoveButton.UseVisualStyleBackColor = False
        '
        'CBWISaveToDisk
        '
        Me.CBWISaveToDisk.Location = New System.Drawing.Point(828, 94)
        Me.CBWISaveToDisk.Name = "CBWISaveToDisk"
        Me.CBWISaveToDisk.Size = New System.Drawing.Size(124, 30)
        Me.CBWISaveToDisk.TabIndex = 157
        Me.CBWISaveToDisk.Text = "Save Images to Disk"
        Me.CBWISaveToDisk.UseVisualStyleBackColor = True
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox5.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.PictureBox5.Location = New System.Drawing.Point(9, 6)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(160, 19)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 156
        Me.PictureBox5.TabStop = False
        '
        'WebImageProgressBar
        '
        Me.WebImageProgressBar.Location = New System.Drawing.Point(828, 217)
        Me.WebImageProgressBar.Maximum = 2500
        Me.WebImageProgressBar.Name = "WebImageProgressBar"
        Me.WebImageProgressBar.Size = New System.Drawing.Size(131, 23)
        Me.WebImageProgressBar.TabIndex = 155
        '
        'CreateBlogContainerButton
        '
        Me.CreateBlogContainerButton.BackColor = System.Drawing.Color.LightGray
        Me.CreateBlogContainerButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateBlogContainerButton.ForeColor = System.Drawing.Color.Black
        Me.CreateBlogContainerButton.Location = New System.Drawing.Point(828, 38)
        Me.CreateBlogContainerButton.Name = "CreateBlogContainerButton"
        Me.CreateBlogContainerButton.Size = New System.Drawing.Size(132, 24)
        Me.CreateBlogContainerButton.TabIndex = 154
        Me.CreateBlogContainerButton.Text = "Add a Blog"
        Me.CreateBlogContainerButton.UseVisualStyleBackColor = False
        '
        'LBLWebImageCount
        '
        Me.LBLWebImageCount.BackColor = System.Drawing.Color.Transparent
        Me.LBLWebImageCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLWebImageCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLWebImageCount.ForeColor = System.Drawing.Color.Black
        Me.LBLWebImageCount.Location = New System.Drawing.Point(6, 599)
        Me.LBLWebImageCount.Name = "LBLWebImageCount"
        Me.LBLWebImageCount.Size = New System.Drawing.Size(93, 21)
        Me.LBLWebImageCount.TabIndex = 153
        Me.LBLWebImageCount.Text = "0/0"
        Me.LBLWebImageCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNWISave
        '
        Me.BTNWISave.BackColor = System.Drawing.Color.LightGray
        Me.BTNWISave.Enabled = False
        Me.BTNWISave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNWISave.ForeColor = System.Drawing.Color.Black
        Me.BTNWISave.Location = New System.Drawing.Point(828, 400)
        Me.BTNWISave.Name = "BTNWISave"
        Me.BTNWISave.Size = New System.Drawing.Size(131, 24)
        Me.BTNWISave.TabIndex = 152
        Me.BTNWISave.Text = "Save Image to Disk"
        Me.BTNWISave.UseVisualStyleBackColor = False
        '
        'UrlFilesPreviousImageButton
        '
        Me.UrlFilesPreviousImageButton.BackColor = System.Drawing.Color.LightGray
        Me.UrlFilesPreviousImageButton.Enabled = False
        Me.UrlFilesPreviousImageButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UrlFilesPreviousImageButton.ForeColor = System.Drawing.Color.Black
        Me.UrlFilesPreviousImageButton.Location = New System.Drawing.Point(828, 281)
        Me.UrlFilesPreviousImageButton.Name = "UrlFilesPreviousImageButton"
        Me.UrlFilesPreviousImageButton.Size = New System.Drawing.Size(47, 24)
        Me.UrlFilesPreviousImageButton.TabIndex = 149
        Me.UrlFilesPreviousImageButton.Text = "<<"
        Me.UrlFilesPreviousImageButton.UseVisualStyleBackColor = False
        '
        'UrlFilesNextImageButton
        '
        Me.UrlFilesNextImageButton.BackColor = System.Drawing.Color.LightGray
        Me.UrlFilesNextImageButton.Enabled = False
        Me.UrlFilesNextImageButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UrlFilesNextImageButton.ForeColor = System.Drawing.Color.Black
        Me.UrlFilesNextImageButton.Location = New System.Drawing.Point(912, 281)
        Me.UrlFilesNextImageButton.Name = "UrlFilesNextImageButton"
        Me.UrlFilesNextImageButton.Size = New System.Drawing.Size(47, 24)
        Me.UrlFilesNextImageButton.TabIndex = 150
        Me.UrlFilesNextImageButton.Text = ">>"
        Me.UrlFilesNextImageButton.UseVisualStyleBackColor = False
        '
        'WebPictureBox
        '
        Me.WebPictureBox.BackColor = System.Drawing.Color.Black
        Me.WebPictureBox.Location = New System.Drawing.Point(6, 38)
        Me.WebPictureBox.Name = "WebPictureBox"
        Me.WebPictureBox.Size = New System.Drawing.Size(815, 553)
        Me.WebPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.WebPictureBox.TabIndex = 148
        Me.WebPictureBox.TabStop = False
        '
        'ImageBlogs
        '
        Me.ImageBlogs.BackColor = System.Drawing.Color.Transparent
        Me.ImageBlogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImageBlogs.ForeColor = System.Drawing.Color.Black
        Me.ImageBlogs.Location = New System.Drawing.Point(175, 6)
        Me.ImageBlogs.Name = "ImageBlogs"
        Me.ImageBlogs.Size = New System.Drawing.Size(646, 21)
        Me.ImageBlogs.TabIndex = 48
        Me.ImageBlogs.Text = "URL Files"
        Me.ImageBlogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TpVideoSettings
        '
        Me.TpVideoSettings.BackColor = System.Drawing.Color.Silver
        Me.TpVideoSettings.Controls.Add(Me.VideoSettingsPanel)
        Me.TpVideoSettings.Location = New System.Drawing.Point(4, 22)
        Me.TpVideoSettings.Name = "TpVideoSettings"
        Me.TpVideoSettings.Padding = New System.Windows.Forms.Padding(6)
        Me.TpVideoSettings.Size = New System.Drawing.Size(972, 456)
        Me.TpVideoSettings.TabIndex = 2
        Me.TpVideoSettings.Text = "Video"
        '
        'VideoSettingsPanel
        '
        Me.VideoSettingsPanel.BackColor = System.Drawing.Color.LightGray
        Me.VideoSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VideoSettingsPanel.Controls.Add(Me.VideoLayoutTable)
        Me.VideoSettingsPanel.Controls.Add(Me.VideoHeaderPanel)
        Me.VideoSettingsPanel.Controls.Add(Me.VideoDescriptionGroupBox)
        Me.VideoSettingsPanel.Location = New System.Drawing.Point(6, 6)
        Me.VideoSettingsPanel.Margin = New System.Windows.Forms.Padding(6)
        Me.VideoSettingsPanel.Name = "VideoSettingsPanel"
        Me.VideoSettingsPanel.Size = New System.Drawing.Size(960, 619)
        Me.VideoSettingsPanel.TabIndex = 92
        '
        'VideoLayoutTable
        '
        Me.VideoLayoutTable.BackColor = System.Drawing.Color.Transparent
        Me.VideoLayoutTable.ColumnCount = 2
        Me.VideoLayoutTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.VideoLayoutTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.VideoLayoutTable.Controls.Add(Me.VideoGeneralPanel, 0, 0)
        Me.VideoLayoutTable.Controls.Add(Me.VideoDommePanel, 1, 0)
        Me.VideoLayoutTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoLayoutTable.Location = New System.Drawing.Point(0, 46)
        Me.VideoLayoutTable.Name = "VideoLayoutTable"
        Me.VideoLayoutTable.RowCount = 1
        Me.VideoLayoutTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.VideoLayoutTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.VideoLayoutTable.Size = New System.Drawing.Size(958, 479)
        Me.VideoLayoutTable.TabIndex = 153
        '
        'VideoGeneralPanel
        '
        Me.VideoGeneralPanel.BackColor = System.Drawing.Color.Transparent
        Me.VideoGeneralPanel.Controls.Add(Me.VideoGeneralGroupBox)
        Me.VideoGeneralPanel.Controls.Add(Me.VideoSpecialGroupBox)
        Me.VideoGeneralPanel.Controls.Add(Me.VideoGenreGroupBox)
        Me.VideoGeneralPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.VideoGeneralPanel.Location = New System.Drawing.Point(3, 3)
        Me.VideoGeneralPanel.Name = "VideoGeneralPanel"
        Me.VideoGeneralPanel.Size = New System.Drawing.Size(473, 307)
        Me.VideoGeneralPanel.TabIndex = 0
        '
        'VideoGeneralGroupBox
        '
        Me.VideoGeneralGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.VideoGeneralGroupBox.Controls.Add(Me.LblVideoGeneralTotal)
        Me.VideoGeneralGroupBox.Controls.Add(Me.TxbVideoGeneral)
        Me.VideoGeneralGroupBox.Controls.Add(Me.BTNVideoGeneral)
        Me.VideoGeneralGroupBox.Controls.Add(Me.CBVideoGeneral)
        Me.VideoGeneralGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.VideoGeneralGroupBox.ForeColor = System.Drawing.Color.Black
        Me.VideoGeneralGroupBox.Location = New System.Drawing.Point(0, 235)
        Me.VideoGeneralGroupBox.Name = "VideoGeneralGroupBox"
        Me.VideoGeneralGroupBox.Size = New System.Drawing.Size(473, 72)
        Me.VideoGeneralGroupBox.TabIndex = 2
        Me.VideoGeneralGroupBox.TabStop = False
        Me.VideoGeneralGroupBox.Text = "General"
        '
        'LblVideoGeneralTotal
        '
        Me.LblVideoGeneralTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoGeneralTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoGeneralTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoGeneralTotal.Location = New System.Drawing.Point(424, 22)
        Me.LblVideoGeneralTotal.Name = "LblVideoGeneralTotal"
        Me.LblVideoGeneralTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoGeneralTotal.TabIndex = 3
        Me.LblVideoGeneralTotal.Text = "0"
        Me.LblVideoGeneralTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoGeneral
        '
        Me.TxbVideoGeneral.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoGeneral.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoGeneral.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoGeneral.Location = New System.Drawing.Point(113, 19)
        Me.TxbVideoGeneral.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoGeneral.Name = "TxbVideoGeneral"
        Me.TxbVideoGeneral.ReadOnly = True
        Me.TxbVideoGeneral.Size = New System.Drawing.Size(305, 20)
        Me.TxbVideoGeneral.TabIndex = 2
        '
        'BTNVideoGeneral
        '
        Me.BTNVideoGeneral.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoGeneral.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoGeneral.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoGeneral.Location = New System.Drawing.Point(73, 13)
        Me.BTNVideoGeneral.Name = "BTNVideoGeneral"
        Me.BTNVideoGeneral.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoGeneral.TabIndex = 1
        Me.BTNVideoGeneral.Text = "1"
        Me.BTNVideoGeneral.UseVisualStyleBackColor = False
        '
        'CBVideoGeneral
        '
        Me.CBVideoGeneral.AutoSize = True
        Me.CBVideoGeneral.ForeColor = System.Drawing.Color.Black
        Me.CBVideoGeneral.Location = New System.Drawing.Point(6, 19)
        Me.CBVideoGeneral.Name = "CBVideoGeneral"
        Me.CBVideoGeneral.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoGeneral.TabIndex = 0
        Me.CBVideoGeneral.Text = "General"
        Me.CBVideoGeneral.UseVisualStyleBackColor = True
        '
        'VideoSpecialGroupBox
        '
        Me.VideoSpecialGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.VideoSpecialGroupBox.Controls.Add(Me.LblVideoCHTotal)
        Me.VideoSpecialGroupBox.Controls.Add(Me.LblVideoJOITotal)
        Me.VideoSpecialGroupBox.Controls.Add(Me.TxbVideoCH)
        Me.VideoSpecialGroupBox.Controls.Add(Me.TxbVideoJOI)
        Me.VideoSpecialGroupBox.Controls.Add(Me.BTNVideoCH)
        Me.VideoSpecialGroupBox.Controls.Add(Me.BTNVideoJOI)
        Me.VideoSpecialGroupBox.Controls.Add(Me.CBVideoJOI)
        Me.VideoSpecialGroupBox.Controls.Add(Me.CBVideoCH)
        Me.VideoSpecialGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoSpecialGroupBox.ForeColor = System.Drawing.Color.Black
        Me.VideoSpecialGroupBox.Location = New System.Drawing.Point(0, 165)
        Me.VideoSpecialGroupBox.Name = "VideoSpecialGroupBox"
        Me.VideoSpecialGroupBox.Size = New System.Drawing.Size(473, 142)
        Me.VideoSpecialGroupBox.TabIndex = 1
        Me.VideoSpecialGroupBox.TabStop = False
        Me.VideoSpecialGroupBox.Text = "Special"
        '
        'LblVideoCHTotal
        '
        Me.LblVideoCHTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblVideoCHTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoCHTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoCHTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoCHTotal.Location = New System.Drawing.Point(424, 35)
        Me.LblVideoCHTotal.Name = "LblVideoCHTotal"
        Me.LblVideoCHTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoCHTotal.TabIndex = 7
        Me.LblVideoCHTotal.Text = "0"
        Me.LblVideoCHTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblVideoJOITotal
        '
        Me.LblVideoJOITotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblVideoJOITotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoJOITotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoJOITotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoJOITotal.Location = New System.Drawing.Point(424, 12)
        Me.LblVideoJOITotal.Name = "LblVideoJOITotal"
        Me.LblVideoJOITotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoJOITotal.TabIndex = 3
        Me.LblVideoJOITotal.Text = "0"
        Me.LblVideoJOITotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoCH
        '
        Me.TxbVideoCH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoCH.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoCH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoCH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoCH.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoCH.Location = New System.Drawing.Point(113, 32)
        Me.TxbVideoCH.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoCH.Name = "TxbVideoCH"
        Me.TxbVideoCH.ReadOnly = True
        Me.TxbVideoCH.Size = New System.Drawing.Size(307, 20)
        Me.TxbVideoCH.TabIndex = 6
        '
        'TxbVideoJOI
        '
        Me.TxbVideoJOI.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoJOI.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoJOI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoJOI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoJOI.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoJOI.Location = New System.Drawing.Point(113, 9)
        Me.TxbVideoJOI.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoJOI.Name = "TxbVideoJOI"
        Me.TxbVideoJOI.ReadOnly = True
        Me.TxbVideoJOI.Size = New System.Drawing.Size(307, 20)
        Me.TxbVideoJOI.TabIndex = 2
        '
        'BTNVideoCH
        '
        Me.BTNVideoCH.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoCH.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoCH.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoCH.Location = New System.Drawing.Point(67, 29)
        Me.BTNVideoCH.Name = "BTNVideoCH"
        Me.BTNVideoCH.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoCH.TabIndex = 5
        Me.BTNVideoCH.Text = "1"
        Me.BTNVideoCH.UseVisualStyleBackColor = False
        '
        'BTNVideoJOI
        '
        Me.BTNVideoJOI.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoJOI.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoJOI.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoJOI.Location = New System.Drawing.Point(67, 6)
        Me.BTNVideoJOI.Name = "BTNVideoJOI"
        Me.BTNVideoJOI.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoJOI.TabIndex = 1
        Me.BTNVideoJOI.Text = "1"
        Me.BTNVideoJOI.UseVisualStyleBackColor = False
        '
        'CBVideoJOI
        '
        Me.CBVideoJOI.AutoSize = True
        Me.CBVideoJOI.ForeColor = System.Drawing.Color.Black
        Me.CBVideoJOI.Location = New System.Drawing.Point(6, 13)
        Me.CBVideoJOI.Name = "CBVideoJOI"
        Me.CBVideoJOI.Size = New System.Drawing.Size(42, 17)
        Me.CBVideoJOI.TabIndex = 0
        Me.CBVideoJOI.Text = "JOI"
        Me.TTDir.SetToolTip(Me.CBVideoJOI, "Jerk Off Instructions")
        Me.CBVideoJOI.UseVisualStyleBackColor = True
        '
        'CBVideoCH
        '
        Me.CBVideoCH.AutoSize = True
        Me.CBVideoCH.ForeColor = System.Drawing.Color.Black
        Me.CBVideoCH.Location = New System.Drawing.Point(6, 37)
        Me.CBVideoCH.Name = "CBVideoCH"
        Me.CBVideoCH.Size = New System.Drawing.Size(41, 17)
        Me.CBVideoCH.TabIndex = 4
        Me.CBVideoCH.Text = "CH"
        Me.TTDir.SetToolTip(Me.CBVideoCH, "Cock Hero")
        Me.CBVideoCH.UseVisualStyleBackColor = True
        '
        'VideoGenreGroupBox
        '
        Me.VideoGenreGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.VideoGenreGroupBox.Controls.Add(Me.LblVideoFemsubTotal)
        Me.VideoGenreGroupBox.Controls.Add(Me.TxbVideoFemsub)
        Me.VideoGenreGroupBox.Controls.Add(Me.LblVideoFemdomTotal)
        Me.VideoGenreGroupBox.Controls.Add(Me.TxbVideoFemdom)
        Me.VideoGenreGroupBox.Controls.Add(Me.TxbVideoBlowjob)
        Me.VideoGenreGroupBox.Controls.Add(Me.LblVideoBlowjobTotal)
        Me.VideoGenreGroupBox.Controls.Add(Me.TxbVideoLesbian)
        Me.VideoGenreGroupBox.Controls.Add(Me.TxbVideoSoftCore)
        Me.VideoGenreGroupBox.Controls.Add(Me.LblVideoLesbianTotal)
        Me.VideoGenreGroupBox.Controls.Add(Me.VideoHardCorePathTextBox)
        Me.VideoGenreGroupBox.Controls.Add(Me.BTNVideoFemSub)
        Me.VideoGenreGroupBox.Controls.Add(Me.LblVideoSoftCoreTotal)
        Me.VideoGenreGroupBox.Controls.Add(Me.BTNVideoFemDom)
        Me.VideoGenreGroupBox.Controls.Add(Me.BTNVideoBlowjob)
        Me.VideoGenreGroupBox.Controls.Add(Me.LblVideoHardCoreTotal)
        Me.VideoGenreGroupBox.Controls.Add(Me.BTNVideoLesbian)
        Me.VideoGenreGroupBox.Controls.Add(Me.BTNVideoSoftCore)
        Me.VideoGenreGroupBox.Controls.Add(Me.VideoSetHardcorePathButton)
        Me.VideoGenreGroupBox.Controls.Add(Me.VideoEnableHardcoreCheckBox)
        Me.VideoGenreGroupBox.Controls.Add(Me.VideoEnableSoftcoreCheckBox)
        Me.VideoGenreGroupBox.Controls.Add(Me.CBVideoLesbian)
        Me.VideoGenreGroupBox.Controls.Add(Me.CBVideoBlowjob)
        Me.VideoGenreGroupBox.Controls.Add(Me.CBVideoFemsub)
        Me.VideoGenreGroupBox.Controls.Add(Me.CBVideoFemdom)
        Me.VideoGenreGroupBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.VideoGenreGroupBox.ForeColor = System.Drawing.Color.Black
        Me.VideoGenreGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.VideoGenreGroupBox.Name = "VideoGenreGroupBox"
        Me.VideoGenreGroupBox.Size = New System.Drawing.Size(473, 165)
        Me.VideoGenreGroupBox.TabIndex = 0
        Me.VideoGenreGroupBox.TabStop = False
        Me.VideoGenreGroupBox.Text = "Genre"
        '
        'LblVideoFemsubTotal
        '
        Me.LblVideoFemsubTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoFemsubTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoFemsubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoFemsubTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoFemsubTotal.Location = New System.Drawing.Point(424, 136)
        Me.LblVideoFemsubTotal.Name = "LblVideoFemsubTotal"
        Me.LblVideoFemsubTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoFemsubTotal.TabIndex = 23
        Me.LblVideoFemsubTotal.Text = "0"
        Me.LblVideoFemsubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoFemsub
        '
        Me.TxbVideoFemsub.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoFemsub.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoFemsub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoFemsub.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoFemsub.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoFemsub.Location = New System.Drawing.Point(107, 134)
        Me.TxbVideoFemsub.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoFemsub.Name = "TxbVideoFemsub"
        Me.TxbVideoFemsub.ReadOnly = True
        Me.TxbVideoFemsub.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoFemsub.TabIndex = 22
        '
        'LblVideoFemdomTotal
        '
        Me.LblVideoFemdomTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoFemdomTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoFemdomTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoFemdomTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoFemdomTotal.Location = New System.Drawing.Point(424, 112)
        Me.LblVideoFemdomTotal.Name = "LblVideoFemdomTotal"
        Me.LblVideoFemdomTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoFemdomTotal.TabIndex = 19
        Me.LblVideoFemdomTotal.Text = "0"
        Me.LblVideoFemdomTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoFemdom
        '
        Me.TxbVideoFemdom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoFemdom.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoFemdom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoFemdom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoFemdom.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoFemdom.Location = New System.Drawing.Point(107, 110)
        Me.TxbVideoFemdom.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoFemdom.Name = "TxbVideoFemdom"
        Me.TxbVideoFemdom.ReadOnly = True
        Me.TxbVideoFemdom.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoFemdom.TabIndex = 18
        '
        'TxbVideoBlowjob
        '
        Me.TxbVideoBlowjob.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoBlowjob.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoBlowjob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoBlowjob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoBlowjob.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoBlowjob.Location = New System.Drawing.Point(107, 86)
        Me.TxbVideoBlowjob.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoBlowjob.Name = "TxbVideoBlowjob"
        Me.TxbVideoBlowjob.ReadOnly = True
        Me.TxbVideoBlowjob.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoBlowjob.TabIndex = 14
        '
        'LblVideoBlowjobTotal
        '
        Me.LblVideoBlowjobTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoBlowjobTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoBlowjobTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoBlowjobTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoBlowjobTotal.Location = New System.Drawing.Point(424, 88)
        Me.LblVideoBlowjobTotal.Name = "LblVideoBlowjobTotal"
        Me.LblVideoBlowjobTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoBlowjobTotal.TabIndex = 15
        Me.LblVideoBlowjobTotal.Text = "0"
        Me.LblVideoBlowjobTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoLesbian
        '
        Me.TxbVideoLesbian.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoLesbian.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoLesbian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoLesbian.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoLesbian.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoLesbian.Location = New System.Drawing.Point(107, 63)
        Me.TxbVideoLesbian.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoLesbian.Name = "TxbVideoLesbian"
        Me.TxbVideoLesbian.ReadOnly = True
        Me.TxbVideoLesbian.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoLesbian.TabIndex = 10
        '
        'TxbVideoSoftCore
        '
        Me.TxbVideoSoftCore.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoSoftCore.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoSoftCore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoSoftCore.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoSoftCore.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoSoftCore.Location = New System.Drawing.Point(107, 40)
        Me.TxbVideoSoftCore.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoSoftCore.Name = "TxbVideoSoftCore"
        Me.TxbVideoSoftCore.ReadOnly = True
        Me.TxbVideoSoftCore.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoSoftCore.TabIndex = 6
        '
        'LblVideoLesbianTotal
        '
        Me.LblVideoLesbianTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoLesbianTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoLesbianTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoLesbianTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoLesbianTotal.Location = New System.Drawing.Point(424, 66)
        Me.LblVideoLesbianTotal.Name = "LblVideoLesbianTotal"
        Me.LblVideoLesbianTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoLesbianTotal.TabIndex = 11
        Me.LblVideoLesbianTotal.Text = "0"
        Me.LblVideoLesbianTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'VideoHardCorePathTextBox
        '
        Me.VideoHardCorePathTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VideoHardCorePathTextBox.BackColor = System.Drawing.Color.LightGray
        Me.VideoHardCorePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VideoHardCorePathTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoHardCorePathTextBox.ForeColor = System.Drawing.Color.Black
        Me.VideoHardCorePathTextBox.Location = New System.Drawing.Point(107, 17)
        Me.VideoHardCorePathTextBox.MinimumSize = New System.Drawing.Size(180, 17)
        Me.VideoHardCorePathTextBox.Name = "VideoHardCorePathTextBox"
        Me.VideoHardCorePathTextBox.ReadOnly = True
        Me.VideoHardCorePathTextBox.Size = New System.Drawing.Size(313, 20)
        Me.VideoHardCorePathTextBox.TabIndex = 2
        '
        'BTNVideoFemSub
        '
        Me.BTNVideoFemSub.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoFemSub.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoFemSub.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoFemSub.Location = New System.Drawing.Point(73, 130)
        Me.BTNVideoFemSub.Name = "BTNVideoFemSub"
        Me.BTNVideoFemSub.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoFemSub.TabIndex = 21
        Me.BTNVideoFemSub.Text = "1"
        Me.BTNVideoFemSub.UseVisualStyleBackColor = False
        '
        'LblVideoSoftCoreTotal
        '
        Me.LblVideoSoftCoreTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoSoftCoreTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoSoftCoreTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoSoftCoreTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoSoftCoreTotal.Location = New System.Drawing.Point(424, 43)
        Me.LblVideoSoftCoreTotal.Name = "LblVideoSoftCoreTotal"
        Me.LblVideoSoftCoreTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoSoftCoreTotal.TabIndex = 7
        Me.LblVideoSoftCoreTotal.Text = "0"
        Me.LblVideoSoftCoreTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNVideoFemDom
        '
        Me.BTNVideoFemDom.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoFemDom.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoFemDom.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoFemDom.Location = New System.Drawing.Point(73, 106)
        Me.BTNVideoFemDom.Name = "BTNVideoFemDom"
        Me.BTNVideoFemDom.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoFemDom.TabIndex = 17
        Me.BTNVideoFemDom.Text = "1"
        Me.BTNVideoFemDom.UseVisualStyleBackColor = False
        '
        'BTNVideoBlowjob
        '
        Me.BTNVideoBlowjob.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoBlowjob.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoBlowjob.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoBlowjob.Location = New System.Drawing.Point(73, 82)
        Me.BTNVideoBlowjob.Name = "BTNVideoBlowjob"
        Me.BTNVideoBlowjob.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoBlowjob.TabIndex = 13
        Me.BTNVideoBlowjob.Text = "1"
        Me.BTNVideoBlowjob.UseVisualStyleBackColor = False
        '
        'LblVideoHardCoreTotal
        '
        Me.LblVideoHardCoreTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoHardCoreTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoHardCoreTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoHardCoreTotal.ForeColor = System.Drawing.Color.Black
        Me.LblVideoHardCoreTotal.Location = New System.Drawing.Point(424, 19)
        Me.LblVideoHardCoreTotal.Name = "LblVideoHardCoreTotal"
        Me.LblVideoHardCoreTotal.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoHardCoreTotal.TabIndex = 3
        Me.LblVideoHardCoreTotal.Text = "0"
        Me.LblVideoHardCoreTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNVideoLesbian
        '
        Me.BTNVideoLesbian.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoLesbian.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoLesbian.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoLesbian.Location = New System.Drawing.Point(73, 59)
        Me.BTNVideoLesbian.Name = "BTNVideoLesbian"
        Me.BTNVideoLesbian.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoLesbian.TabIndex = 9
        Me.BTNVideoLesbian.Text = "1"
        Me.BTNVideoLesbian.UseVisualStyleBackColor = False
        '
        'BTNVideoSoftCore
        '
        Me.BTNVideoSoftCore.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoSoftCore.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoSoftCore.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoSoftCore.Location = New System.Drawing.Point(73, 36)
        Me.BTNVideoSoftCore.Name = "BTNVideoSoftCore"
        Me.BTNVideoSoftCore.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoSoftCore.TabIndex = 5
        Me.BTNVideoSoftCore.Text = "1"
        Me.BTNVideoSoftCore.UseVisualStyleBackColor = False
        '
        'VideoSetHardcorePathButton
        '
        Me.VideoSetHardcorePathButton.BackColor = System.Drawing.Color.LightGray
        Me.VideoSetHardcorePathButton.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.VideoSetHardcorePathButton.ForeColor = System.Drawing.Color.Black
        Me.VideoSetHardcorePathButton.Location = New System.Drawing.Point(73, 12)
        Me.VideoSetHardcorePathButton.Name = "VideoSetHardcorePathButton"
        Me.VideoSetHardcorePathButton.Size = New System.Drawing.Size(34, 28)
        Me.VideoSetHardcorePathButton.TabIndex = 1
        Me.VideoSetHardcorePathButton.Text = "1"
        Me.VideoSetHardcorePathButton.UseVisualStyleBackColor = False
        '
        'VideoEnableHardcoreCheckBox
        '
        Me.VideoEnableHardcoreCheckBox.AutoSize = True
        Me.VideoEnableHardcoreCheckBox.ForeColor = System.Drawing.Color.Black
        Me.VideoEnableHardcoreCheckBox.Location = New System.Drawing.Point(6, 19)
        Me.VideoEnableHardcoreCheckBox.Name = "VideoEnableHardcoreCheckBox"
        Me.VideoEnableHardcoreCheckBox.Size = New System.Drawing.Size(70, 17)
        Me.VideoEnableHardcoreCheckBox.TabIndex = 0
        Me.VideoEnableHardcoreCheckBox.Text = "Hardcore"
        Me.VideoEnableHardcoreCheckBox.UseVisualStyleBackColor = True
        '
        'VideoEnableSoftcoreCheckBox
        '
        Me.VideoEnableSoftcoreCheckBox.AutoSize = True
        Me.VideoEnableSoftcoreCheckBox.ForeColor = System.Drawing.Color.Black
        Me.VideoEnableSoftcoreCheckBox.Location = New System.Drawing.Point(6, 43)
        Me.VideoEnableSoftcoreCheckBox.Name = "VideoEnableSoftcoreCheckBox"
        Me.VideoEnableSoftcoreCheckBox.Size = New System.Drawing.Size(66, 17)
        Me.VideoEnableSoftcoreCheckBox.TabIndex = 4
        Me.VideoEnableSoftcoreCheckBox.Text = "Softcore"
        Me.VideoEnableSoftcoreCheckBox.UseVisualStyleBackColor = True
        '
        'CBVideoLesbian
        '
        Me.CBVideoLesbian.AutoSize = True
        Me.CBVideoLesbian.ForeColor = System.Drawing.Color.Black
        Me.CBVideoLesbian.Location = New System.Drawing.Point(6, 66)
        Me.CBVideoLesbian.Name = "CBVideoLesbian"
        Me.CBVideoLesbian.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoLesbian.TabIndex = 8
        Me.CBVideoLesbian.Text = "Lesbian"
        Me.CBVideoLesbian.UseVisualStyleBackColor = True
        '
        'CBVideoBlowjob
        '
        Me.CBVideoBlowjob.AutoSize = True
        Me.CBVideoBlowjob.ForeColor = System.Drawing.Color.Black
        Me.CBVideoBlowjob.Location = New System.Drawing.Point(6, 89)
        Me.CBVideoBlowjob.Name = "CBVideoBlowjob"
        Me.CBVideoBlowjob.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoBlowjob.TabIndex = 12
        Me.CBVideoBlowjob.Text = "Blowjob"
        Me.CBVideoBlowjob.UseVisualStyleBackColor = True
        '
        'CBVideoFemsub
        '
        Me.CBVideoFemsub.AutoSize = True
        Me.CBVideoFemsub.ForeColor = System.Drawing.Color.Black
        Me.CBVideoFemsub.Location = New System.Drawing.Point(6, 137)
        Me.CBVideoFemsub.Name = "CBVideoFemsub"
        Me.CBVideoFemsub.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoFemsub.TabIndex = 20
        Me.CBVideoFemsub.Text = "Femsub"
        Me.CBVideoFemsub.UseVisualStyleBackColor = True
        '
        'CBVideoFemdom
        '
        Me.CBVideoFemdom.AutoSize = True
        Me.CBVideoFemdom.ForeColor = System.Drawing.Color.Black
        Me.CBVideoFemdom.Location = New System.Drawing.Point(6, 113)
        Me.CBVideoFemdom.Name = "CBVideoFemdom"
        Me.CBVideoFemdom.Size = New System.Drawing.Size(66, 17)
        Me.CBVideoFemdom.TabIndex = 16
        Me.CBVideoFemdom.Text = "Femdom"
        Me.CBVideoFemdom.UseVisualStyleBackColor = True
        '
        'VideoDommePanel
        '
        Me.VideoDommePanel.BackColor = System.Drawing.Color.Transparent
        Me.VideoDommePanel.Controls.Add(Me.VideoDommeGeneralGroupBox)
        Me.VideoDommePanel.Controls.Add(Me.GbxVideoSpecialD)
        Me.VideoDommePanel.Controls.Add(Me.GbxVideoGenreD)
        Me.VideoDommePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoDommePanel.Location = New System.Drawing.Point(482, 3)
        Me.VideoDommePanel.Name = "VideoDommePanel"
        Me.VideoDommePanel.Size = New System.Drawing.Size(473, 473)
        Me.VideoDommePanel.TabIndex = 1
        '
        'VideoDommeGeneralGroupBox
        '
        Me.VideoDommeGeneralGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VideoDommeGeneralGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.VideoDommeGeneralGroupBox.Controls.Add(Me.VideoTotalDommeGeneral)
        Me.VideoDommeGeneralGroupBox.Controls.Add(Me.VideoDommeGeneralPathTextBox)
        Me.VideoDommeGeneralGroupBox.Controls.Add(Me.BTNVideoGeneralD)
        Me.VideoDommeGeneralGroupBox.Controls.Add(Me.CBVideoGeneralD)
        Me.VideoDommeGeneralGroupBox.ForeColor = System.Drawing.Color.Black
        Me.VideoDommeGeneralGroupBox.Location = New System.Drawing.Point(0, 235)
        Me.VideoDommeGeneralGroupBox.Name = "VideoDommeGeneralGroupBox"
        Me.VideoDommeGeneralGroupBox.Size = New System.Drawing.Size(473, 72)
        Me.VideoDommeGeneralGroupBox.TabIndex = 5
        Me.VideoDommeGeneralGroupBox.TabStop = False
        Me.VideoDommeGeneralGroupBox.Text = "Domme General"
        '
        'VideoTotalDommeGeneral
        '
        Me.VideoTotalDommeGeneral.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VideoTotalDommeGeneral.BackColor = System.Drawing.Color.Transparent
        Me.VideoTotalDommeGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoTotalDommeGeneral.ForeColor = System.Drawing.Color.Black
        Me.VideoTotalDommeGeneral.Location = New System.Drawing.Point(432, 19)
        Me.VideoTotalDommeGeneral.Name = "VideoTotalDommeGeneral"
        Me.VideoTotalDommeGeneral.Size = New System.Drawing.Size(34, 17)
        Me.VideoTotalDommeGeneral.TabIndex = 3
        Me.VideoTotalDommeGeneral.Text = "0"
        Me.VideoTotalDommeGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'VideoDommeGeneralPathTextBox
        '
        Me.VideoDommeGeneralPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VideoDommeGeneralPathTextBox.BackColor = System.Drawing.Color.LightGray
        Me.VideoDommeGeneralPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VideoDommeGeneralPathTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoDommeGeneralPathTextBox.ForeColor = System.Drawing.Color.Black
        Me.VideoDommeGeneralPathTextBox.Location = New System.Drawing.Point(113, 18)
        Me.VideoDommeGeneralPathTextBox.MinimumSize = New System.Drawing.Size(180, 17)
        Me.VideoDommeGeneralPathTextBox.Name = "VideoDommeGeneralPathTextBox"
        Me.VideoDommeGeneralPathTextBox.ReadOnly = True
        Me.VideoDommeGeneralPathTextBox.Size = New System.Drawing.Size(313, 20)
        Me.VideoDommeGeneralPathTextBox.TabIndex = 2
        '
        'BTNVideoGeneralD
        '
        Me.BTNVideoGeneralD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoGeneralD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoGeneralD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoGeneralD.Location = New System.Drawing.Point(73, 13)
        Me.BTNVideoGeneralD.Name = "BTNVideoGeneralD"
        Me.BTNVideoGeneralD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoGeneralD.TabIndex = 1
        Me.BTNVideoGeneralD.Text = "1"
        Me.BTNVideoGeneralD.UseVisualStyleBackColor = False
        '
        'CBVideoGeneralD
        '
        Me.CBVideoGeneralD.AutoSize = True
        Me.CBVideoGeneralD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoGeneralD.Location = New System.Drawing.Point(6, 19)
        Me.CBVideoGeneralD.Name = "CBVideoGeneralD"
        Me.CBVideoGeneralD.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoGeneralD.TabIndex = 0
        Me.CBVideoGeneralD.Text = "General"
        Me.CBVideoGeneralD.UseVisualStyleBackColor = True
        '
        'GbxVideoSpecialD
        '
        Me.GbxVideoSpecialD.BackColor = System.Drawing.Color.LightGray
        Me.GbxVideoSpecialD.Controls.Add(Me.LblVideoCHTotalD)
        Me.GbxVideoSpecialD.Controls.Add(Me.LblVideoJOITotalD)
        Me.GbxVideoSpecialD.Controls.Add(Me.TxbVideoCHD)
        Me.GbxVideoSpecialD.Controls.Add(Me.TxbVideoJOID)
        Me.GbxVideoSpecialD.Controls.Add(Me.BTNVideoCHD)
        Me.GbxVideoSpecialD.Controls.Add(Me.BTNVideoJOID)
        Me.GbxVideoSpecialD.Controls.Add(Me.CBVideoJOID)
        Me.GbxVideoSpecialD.Controls.Add(Me.CBVideoCHD)
        Me.GbxVideoSpecialD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GbxVideoSpecialD.ForeColor = System.Drawing.Color.Black
        Me.GbxVideoSpecialD.Location = New System.Drawing.Point(0, 165)
        Me.GbxVideoSpecialD.Name = "GbxVideoSpecialD"
        Me.GbxVideoSpecialD.Size = New System.Drawing.Size(473, 308)
        Me.GbxVideoSpecialD.TabIndex = 4
        Me.GbxVideoSpecialD.TabStop = False
        Me.GbxVideoSpecialD.Text = "Domme Special"
        '
        'LblVideoCHTotalD
        '
        Me.LblVideoCHTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoCHTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoCHTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoCHTotalD.Location = New System.Drawing.Point(432, 42)
        Me.LblVideoCHTotalD.Name = "LblVideoCHTotalD"
        Me.LblVideoCHTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoCHTotalD.TabIndex = 7
        Me.LblVideoCHTotalD.Text = "0"
        Me.LblVideoCHTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblVideoJOITotalD
        '
        Me.LblVideoJOITotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoJOITotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoJOITotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoJOITotalD.Location = New System.Drawing.Point(432, 19)
        Me.LblVideoJOITotalD.Name = "LblVideoJOITotalD"
        Me.LblVideoJOITotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoJOITotalD.TabIndex = 3
        Me.LblVideoJOITotalD.Text = "0"
        Me.LblVideoJOITotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoCHD
        '
        Me.TxbVideoCHD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoCHD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoCHD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoCHD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoCHD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoCHD.Location = New System.Drawing.Point(113, 41)
        Me.TxbVideoCHD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoCHD.Name = "TxbVideoCHD"
        Me.TxbVideoCHD.ReadOnly = True
        Me.TxbVideoCHD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoCHD.TabIndex = 6
        '
        'TxbVideoJOID
        '
        Me.TxbVideoJOID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoJOID.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoJOID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoJOID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoJOID.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoJOID.Location = New System.Drawing.Point(113, 18)
        Me.TxbVideoJOID.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoJOID.Name = "TxbVideoJOID"
        Me.TxbVideoJOID.ReadOnly = True
        Me.TxbVideoJOID.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoJOID.TabIndex = 2
        '
        'BTNVideoCHD
        '
        Me.BTNVideoCHD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoCHD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoCHD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoCHD.Location = New System.Drawing.Point(73, 36)
        Me.BTNVideoCHD.Name = "BTNVideoCHD"
        Me.BTNVideoCHD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoCHD.TabIndex = 5
        Me.BTNVideoCHD.Text = "1"
        Me.BTNVideoCHD.UseVisualStyleBackColor = False
        '
        'BTNVideoJOID
        '
        Me.BTNVideoJOID.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoJOID.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoJOID.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoJOID.Location = New System.Drawing.Point(73, 13)
        Me.BTNVideoJOID.Name = "BTNVideoJOID"
        Me.BTNVideoJOID.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoJOID.TabIndex = 1
        Me.BTNVideoJOID.Text = "1"
        Me.BTNVideoJOID.UseVisualStyleBackColor = False
        '
        'CBVideoJOID
        '
        Me.CBVideoJOID.AutoSize = True
        Me.CBVideoJOID.ForeColor = System.Drawing.Color.Black
        Me.CBVideoJOID.Location = New System.Drawing.Point(6, 19)
        Me.CBVideoJOID.Name = "CBVideoJOID"
        Me.CBVideoJOID.Size = New System.Drawing.Size(42, 17)
        Me.CBVideoJOID.TabIndex = 0
        Me.CBVideoJOID.Text = "JOI"
        Me.CBVideoJOID.UseVisualStyleBackColor = True
        '
        'CBVideoCHD
        '
        Me.CBVideoCHD.AutoSize = True
        Me.CBVideoCHD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoCHD.Location = New System.Drawing.Point(6, 43)
        Me.CBVideoCHD.Name = "CBVideoCHD"
        Me.CBVideoCHD.Size = New System.Drawing.Size(41, 17)
        Me.CBVideoCHD.TabIndex = 4
        Me.CBVideoCHD.Text = "CH"
        Me.CBVideoCHD.UseVisualStyleBackColor = True
        '
        'GbxVideoGenreD
        '
        Me.GbxVideoGenreD.BackColor = System.Drawing.Color.LightGray
        Me.GbxVideoGenreD.Controls.Add(Me.LblVideoFemsubTotalD)
        Me.GbxVideoGenreD.Controls.Add(Me.TxbVideoFemsubD)
        Me.GbxVideoGenreD.Controls.Add(Me.LblVideoFemdomTotalD)
        Me.GbxVideoGenreD.Controls.Add(Me.TxbVideoFemdomD)
        Me.GbxVideoGenreD.Controls.Add(Me.TxbVideoBlowjobD)
        Me.GbxVideoGenreD.Controls.Add(Me.LblVideoBlowjobTotalD)
        Me.GbxVideoGenreD.Controls.Add(Me.TxbVideoLesbianD)
        Me.GbxVideoGenreD.Controls.Add(Me.TxbVideoSoftCoreD)
        Me.GbxVideoGenreD.Controls.Add(Me.LblVideoLesbianTotalD)
        Me.GbxVideoGenreD.Controls.Add(Me.TxbVideoHardCoreD)
        Me.GbxVideoGenreD.Controls.Add(Me.BTNVideoFemSubD)
        Me.GbxVideoGenreD.Controls.Add(Me.LblVideoSoftCoreTotalD)
        Me.GbxVideoGenreD.Controls.Add(Me.BTNVideoFemDomD)
        Me.GbxVideoGenreD.Controls.Add(Me.BTNVideoBlowjobD)
        Me.GbxVideoGenreD.Controls.Add(Me.LblVideoHardCoreTotalD)
        Me.GbxVideoGenreD.Controls.Add(Me.BTNVideoLesbianD)
        Me.GbxVideoGenreD.Controls.Add(Me.BTNVideoSoftCoreD)
        Me.GbxVideoGenreD.Controls.Add(Me.BTNVideoHardCoreD)
        Me.GbxVideoGenreD.Controls.Add(Me.CBVideoHardcoreD)
        Me.GbxVideoGenreD.Controls.Add(Me.CBVideoSoftCoreD)
        Me.GbxVideoGenreD.Controls.Add(Me.CBVideoLesbianD)
        Me.GbxVideoGenreD.Controls.Add(Me.CBVideoBlowjobD)
        Me.GbxVideoGenreD.Controls.Add(Me.CBVideoFemsubD)
        Me.GbxVideoGenreD.Controls.Add(Me.CBVideoFemdomD)
        Me.GbxVideoGenreD.Dock = System.Windows.Forms.DockStyle.Top
        Me.GbxVideoGenreD.ForeColor = System.Drawing.Color.Black
        Me.GbxVideoGenreD.Location = New System.Drawing.Point(0, 0)
        Me.GbxVideoGenreD.Name = "GbxVideoGenreD"
        Me.GbxVideoGenreD.Size = New System.Drawing.Size(473, 165)
        Me.GbxVideoGenreD.TabIndex = 3
        Me.GbxVideoGenreD.TabStop = False
        Me.GbxVideoGenreD.Text = "Domme Genre"
        '
        'LblVideoFemsubTotalD
        '
        Me.LblVideoFemsubTotalD.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoFemsubTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoFemsubTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoFemsubTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoFemsubTotalD.Location = New System.Drawing.Point(432, 129)
        Me.LblVideoFemsubTotalD.Name = "LblVideoFemsubTotalD"
        Me.LblVideoFemsubTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoFemsubTotalD.TabIndex = 23
        Me.LblVideoFemsubTotalD.Text = "0"
        Me.LblVideoFemsubTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoFemsubD
        '
        Me.TxbVideoFemsubD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoFemsubD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoFemsubD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoFemsubD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoFemsubD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoFemsubD.Location = New System.Drawing.Point(113, 136)
        Me.TxbVideoFemsubD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoFemsubD.Name = "TxbVideoFemsubD"
        Me.TxbVideoFemsubD.ReadOnly = True
        Me.TxbVideoFemsubD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoFemsubD.TabIndex = 22
        '
        'LblVideoFemdomTotalD
        '
        Me.LblVideoFemdomTotalD.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoFemdomTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoFemdomTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoFemdomTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoFemdomTotalD.Location = New System.Drawing.Point(432, 105)
        Me.LblVideoFemdomTotalD.Name = "LblVideoFemdomTotalD"
        Me.LblVideoFemdomTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoFemdomTotalD.TabIndex = 19
        Me.LblVideoFemdomTotalD.Text = "0"
        Me.LblVideoFemdomTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoFemdomD
        '
        Me.TxbVideoFemdomD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoFemdomD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoFemdomD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoFemdomD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoFemdomD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoFemdomD.Location = New System.Drawing.Point(113, 112)
        Me.TxbVideoFemdomD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoFemdomD.Name = "TxbVideoFemdomD"
        Me.TxbVideoFemdomD.ReadOnly = True
        Me.TxbVideoFemdomD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoFemdomD.TabIndex = 18
        '
        'TxbVideoBlowjobD
        '
        Me.TxbVideoBlowjobD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoBlowjobD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoBlowjobD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoBlowjobD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoBlowjobD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoBlowjobD.Location = New System.Drawing.Point(113, 88)
        Me.TxbVideoBlowjobD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoBlowjobD.Name = "TxbVideoBlowjobD"
        Me.TxbVideoBlowjobD.ReadOnly = True
        Me.TxbVideoBlowjobD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoBlowjobD.TabIndex = 14
        '
        'LblVideoBlowjobTotalD
        '
        Me.LblVideoBlowjobTotalD.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoBlowjobTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoBlowjobTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoBlowjobTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoBlowjobTotalD.Location = New System.Drawing.Point(432, 81)
        Me.LblVideoBlowjobTotalD.Name = "LblVideoBlowjobTotalD"
        Me.LblVideoBlowjobTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoBlowjobTotalD.TabIndex = 15
        Me.LblVideoBlowjobTotalD.Text = "0"
        Me.LblVideoBlowjobTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoLesbianD
        '
        Me.TxbVideoLesbianD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoLesbianD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoLesbianD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoLesbianD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoLesbianD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoLesbianD.Location = New System.Drawing.Point(113, 65)
        Me.TxbVideoLesbianD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoLesbianD.Name = "TxbVideoLesbianD"
        Me.TxbVideoLesbianD.ReadOnly = True
        Me.TxbVideoLesbianD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoLesbianD.TabIndex = 10
        '
        'TxbVideoSoftCoreD
        '
        Me.TxbVideoSoftCoreD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoSoftCoreD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoSoftCoreD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoSoftCoreD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoSoftCoreD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoSoftCoreD.Location = New System.Drawing.Point(113, 42)
        Me.TxbVideoSoftCoreD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoSoftCoreD.Name = "TxbVideoSoftCoreD"
        Me.TxbVideoSoftCoreD.ReadOnly = True
        Me.TxbVideoSoftCoreD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoSoftCoreD.TabIndex = 6
        '
        'LblVideoLesbianTotalD
        '
        Me.LblVideoLesbianTotalD.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoLesbianTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoLesbianTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoLesbianTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoLesbianTotalD.Location = New System.Drawing.Point(432, 59)
        Me.LblVideoLesbianTotalD.Name = "LblVideoLesbianTotalD"
        Me.LblVideoLesbianTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoLesbianTotalD.TabIndex = 11
        Me.LblVideoLesbianTotalD.Text = "0"
        Me.LblVideoLesbianTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxbVideoHardCoreD
        '
        Me.TxbVideoHardCoreD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxbVideoHardCoreD.BackColor = System.Drawing.Color.LightGray
        Me.TxbVideoHardCoreD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVideoHardCoreD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVideoHardCoreD.ForeColor = System.Drawing.Color.Black
        Me.TxbVideoHardCoreD.Location = New System.Drawing.Point(113, 19)
        Me.TxbVideoHardCoreD.MinimumSize = New System.Drawing.Size(180, 17)
        Me.TxbVideoHardCoreD.Name = "TxbVideoHardCoreD"
        Me.TxbVideoHardCoreD.ReadOnly = True
        Me.TxbVideoHardCoreD.Size = New System.Drawing.Size(313, 20)
        Me.TxbVideoHardCoreD.TabIndex = 2
        '
        'BTNVideoFemSubD
        '
        Me.BTNVideoFemSubD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoFemSubD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoFemSubD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoFemSubD.Location = New System.Drawing.Point(73, 130)
        Me.BTNVideoFemSubD.Name = "BTNVideoFemSubD"
        Me.BTNVideoFemSubD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoFemSubD.TabIndex = 21
        Me.BTNVideoFemSubD.Text = "1"
        Me.BTNVideoFemSubD.UseVisualStyleBackColor = False
        '
        'LblVideoSoftCoreTotalD
        '
        Me.LblVideoSoftCoreTotalD.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoSoftCoreTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoSoftCoreTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoSoftCoreTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoSoftCoreTotalD.Location = New System.Drawing.Point(432, 36)
        Me.LblVideoSoftCoreTotalD.Name = "LblVideoSoftCoreTotalD"
        Me.LblVideoSoftCoreTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoSoftCoreTotalD.TabIndex = 7
        Me.LblVideoSoftCoreTotalD.Text = "0"
        Me.LblVideoSoftCoreTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNVideoFemDomD
        '
        Me.BTNVideoFemDomD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoFemDomD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoFemDomD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoFemDomD.Location = New System.Drawing.Point(73, 106)
        Me.BTNVideoFemDomD.Name = "BTNVideoFemDomD"
        Me.BTNVideoFemDomD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoFemDomD.TabIndex = 17
        Me.BTNVideoFemDomD.Text = "1"
        Me.BTNVideoFemDomD.UseVisualStyleBackColor = False
        '
        'BTNVideoBlowjobD
        '
        Me.BTNVideoBlowjobD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoBlowjobD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoBlowjobD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoBlowjobD.Location = New System.Drawing.Point(73, 82)
        Me.BTNVideoBlowjobD.Name = "BTNVideoBlowjobD"
        Me.BTNVideoBlowjobD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoBlowjobD.TabIndex = 13
        Me.BTNVideoBlowjobD.Text = "1"
        Me.BTNVideoBlowjobD.UseVisualStyleBackColor = False
        '
        'LblVideoHardCoreTotalD
        '
        Me.LblVideoHardCoreTotalD.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblVideoHardCoreTotalD.BackColor = System.Drawing.Color.Transparent
        Me.LblVideoHardCoreTotalD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVideoHardCoreTotalD.ForeColor = System.Drawing.Color.Black
        Me.LblVideoHardCoreTotalD.Location = New System.Drawing.Point(432, 12)
        Me.LblVideoHardCoreTotalD.Name = "LblVideoHardCoreTotalD"
        Me.LblVideoHardCoreTotalD.Size = New System.Drawing.Size(34, 17)
        Me.LblVideoHardCoreTotalD.TabIndex = 3
        Me.LblVideoHardCoreTotalD.Text = "0"
        Me.LblVideoHardCoreTotalD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNVideoLesbianD
        '
        Me.BTNVideoLesbianD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoLesbianD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoLesbianD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoLesbianD.Location = New System.Drawing.Point(73, 59)
        Me.BTNVideoLesbianD.Name = "BTNVideoLesbianD"
        Me.BTNVideoLesbianD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoLesbianD.TabIndex = 9
        Me.BTNVideoLesbianD.Text = "1"
        Me.BTNVideoLesbianD.UseVisualStyleBackColor = False
        '
        'BTNVideoSoftCoreD
        '
        Me.BTNVideoSoftCoreD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoSoftCoreD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoSoftCoreD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoSoftCoreD.Location = New System.Drawing.Point(73, 36)
        Me.BTNVideoSoftCoreD.Name = "BTNVideoSoftCoreD"
        Me.BTNVideoSoftCoreD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoSoftCoreD.TabIndex = 5
        Me.BTNVideoSoftCoreD.Text = "1"
        Me.BTNVideoSoftCoreD.UseVisualStyleBackColor = False
        '
        'BTNVideoHardCoreD
        '
        Me.BTNVideoHardCoreD.BackColor = System.Drawing.Color.LightGray
        Me.BTNVideoHardCoreD.Font = New System.Drawing.Font("Wingdings", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BTNVideoHardCoreD.ForeColor = System.Drawing.Color.Black
        Me.BTNVideoHardCoreD.Location = New System.Drawing.Point(73, 12)
        Me.BTNVideoHardCoreD.Name = "BTNVideoHardCoreD"
        Me.BTNVideoHardCoreD.Size = New System.Drawing.Size(34, 28)
        Me.BTNVideoHardCoreD.TabIndex = 1
        Me.BTNVideoHardCoreD.Text = "1"
        Me.BTNVideoHardCoreD.UseVisualStyleBackColor = False
        '
        'CBVideoHardcoreD
        '
        Me.CBVideoHardcoreD.AutoSize = True
        Me.CBVideoHardcoreD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoHardcoreD.Location = New System.Drawing.Point(6, 19)
        Me.CBVideoHardcoreD.Name = "CBVideoHardcoreD"
        Me.CBVideoHardcoreD.Size = New System.Drawing.Size(70, 17)
        Me.CBVideoHardcoreD.TabIndex = 0
        Me.CBVideoHardcoreD.Text = "Hardcore"
        Me.CBVideoHardcoreD.UseVisualStyleBackColor = True
        '
        'CBVideoSoftCoreD
        '
        Me.CBVideoSoftCoreD.AutoSize = True
        Me.CBVideoSoftCoreD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoSoftCoreD.Location = New System.Drawing.Point(6, 43)
        Me.CBVideoSoftCoreD.Name = "CBVideoSoftCoreD"
        Me.CBVideoSoftCoreD.Size = New System.Drawing.Size(66, 17)
        Me.CBVideoSoftCoreD.TabIndex = 4
        Me.CBVideoSoftCoreD.Text = "Softcore"
        Me.CBVideoSoftCoreD.UseVisualStyleBackColor = True
        '
        'CBVideoLesbianD
        '
        Me.CBVideoLesbianD.AutoSize = True
        Me.CBVideoLesbianD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoLesbianD.Location = New System.Drawing.Point(6, 66)
        Me.CBVideoLesbianD.Name = "CBVideoLesbianD"
        Me.CBVideoLesbianD.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoLesbianD.TabIndex = 8
        Me.CBVideoLesbianD.Text = "Lesbian"
        Me.CBVideoLesbianD.UseVisualStyleBackColor = True
        '
        'CBVideoBlowjobD
        '
        Me.CBVideoBlowjobD.AutoSize = True
        Me.CBVideoBlowjobD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoBlowjobD.Location = New System.Drawing.Point(6, 89)
        Me.CBVideoBlowjobD.Name = "CBVideoBlowjobD"
        Me.CBVideoBlowjobD.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoBlowjobD.TabIndex = 12
        Me.CBVideoBlowjobD.Text = "Blowjob"
        Me.CBVideoBlowjobD.UseVisualStyleBackColor = True
        '
        'CBVideoFemsubD
        '
        Me.CBVideoFemsubD.AutoSize = True
        Me.CBVideoFemsubD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoFemsubD.Location = New System.Drawing.Point(6, 137)
        Me.CBVideoFemsubD.Name = "CBVideoFemsubD"
        Me.CBVideoFemsubD.Size = New System.Drawing.Size(63, 17)
        Me.CBVideoFemsubD.TabIndex = 20
        Me.CBVideoFemsubD.Text = "Femsub"
        Me.CBVideoFemsubD.UseVisualStyleBackColor = True
        '
        'CBVideoFemdomD
        '
        Me.CBVideoFemdomD.AutoSize = True
        Me.CBVideoFemdomD.ForeColor = System.Drawing.Color.Black
        Me.CBVideoFemdomD.Location = New System.Drawing.Point(6, 113)
        Me.CBVideoFemdomD.Name = "CBVideoFemdomD"
        Me.CBVideoFemdomD.Size = New System.Drawing.Size(66, 17)
        Me.CBVideoFemdomD.TabIndex = 16
        Me.CBVideoFemdomD.Text = "Femdom"
        Me.CBVideoFemdomD.UseVisualStyleBackColor = True
        '
        'VideoHeaderPanel
        '
        Me.VideoHeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.VideoHeaderPanel.Controls.Add(Me.VideoHeaderLabel)
        Me.VideoHeaderPanel.Controls.Add(Me.VideoRefreshButton)
        Me.VideoHeaderPanel.Controls.Add(Me.VideoLogo)
        Me.VideoHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.VideoHeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.VideoHeaderPanel.Name = "VideoHeaderPanel"
        Me.VideoHeaderPanel.Size = New System.Drawing.Size(958, 46)
        Me.VideoHeaderPanel.TabIndex = 152
        '
        'VideoHeaderLabel
        '
        Me.VideoHeaderLabel.BackColor = System.Drawing.Color.Transparent
        Me.VideoHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoHeaderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoHeaderLabel.ForeColor = System.Drawing.Color.Black
        Me.VideoHeaderLabel.Location = New System.Drawing.Point(160, 0)
        Me.VideoHeaderLabel.Name = "VideoHeaderLabel"
        Me.VideoHeaderLabel.Size = New System.Drawing.Size(638, 46)
        Me.VideoHeaderLabel.TabIndex = 49
        Me.VideoHeaderLabel.Text = "Video Settings"
        Me.VideoHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'VideoRefreshButton
        '
        Me.VideoRefreshButton.BackColor = System.Drawing.Color.Transparent
        Me.VideoRefreshButton.BackgroundImage = Global.Tease_AI.My.Resources.Resources.Button_Refresh
        Me.VideoRefreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.VideoRefreshButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.VideoRefreshButton.FlatAppearance.BorderSize = 0
        Me.VideoRefreshButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.VideoRefreshButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.VideoRefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VideoRefreshButton.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoRefreshButton.ForeColor = System.Drawing.Color.Black
        Me.VideoRefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VideoRefreshButton.Location = New System.Drawing.Point(798, 0)
        Me.VideoRefreshButton.Name = "VideoRefreshButton"
        Me.VideoRefreshButton.Size = New System.Drawing.Size(160, 46)
        Me.VideoRefreshButton.TabIndex = 149
        Me.VideoRefreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VideoRefreshButton.UseVisualStyleBackColor = False
        '
        'VideoLogo
        '
        Me.VideoLogo.BackColor = System.Drawing.Color.Transparent
        Me.VideoLogo.Dock = System.Windows.Forms.DockStyle.Left
        Me.VideoLogo.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.VideoLogo.Location = New System.Drawing.Point(0, 0)
        Me.VideoLogo.Name = "VideoLogo"
        Me.VideoLogo.Size = New System.Drawing.Size(160, 46)
        Me.VideoLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.VideoLogo.TabIndex = 151
        Me.VideoLogo.TabStop = False
        '
        'VideoDescriptionGroupBox
        '
        Me.VideoDescriptionGroupBox.BackColor = System.Drawing.Color.LightGray
        Me.VideoDescriptionGroupBox.Controls.Add(Me.VideoDescriptionLabel)
        Me.VideoDescriptionGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.VideoDescriptionGroupBox.ForeColor = System.Drawing.Color.Black
        Me.VideoDescriptionGroupBox.Location = New System.Drawing.Point(0, 525)
        Me.VideoDescriptionGroupBox.Name = "VideoDescriptionGroupBox"
        Me.VideoDescriptionGroupBox.Size = New System.Drawing.Size(958, 92)
        Me.VideoDescriptionGroupBox.TabIndex = 6
        Me.VideoDescriptionGroupBox.TabStop = False
        Me.VideoDescriptionGroupBox.Text = "Description"
        '
        'VideoDescriptionLabel
        '
        Me.VideoDescriptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.VideoDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoDescriptionLabel.ForeColor = System.Drawing.Color.Black
        Me.VideoDescriptionLabel.Location = New System.Drawing.Point(3, 16)
        Me.VideoDescriptionLabel.Name = "VideoDescriptionLabel"
        Me.VideoDescriptionLabel.Size = New System.Drawing.Size(952, 73)
        Me.VideoDescriptionLabel.TabIndex = 62
        Me.VideoDescriptionLabel.Text = "Use this page to select the videos you would like the program to use and set thei" &
    "r paths." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The Domme Genre paths are for videos that feature the model you are " &
    "using as your domme."
        Me.VideoDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AppsTabPage
        '
        Me.AppsTabPage.BackColor = System.Drawing.Color.Silver
        Me.AppsTabPage.Controls.Add(Me.AppsSettingsTabList)
        Me.AppsTabPage.Controls.Add(Me.AppsSettingsHeaderPanel)
        Me.AppsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.AppsTabPage.Name = "AppsTabPage"
        Me.AppsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AppsTabPage.Size = New System.Drawing.Size(972, 456)
        Me.AppsTabPage.TabIndex = 16
        Me.AppsTabPage.Text = "Apps"
        Me.AppsTabPage.ToolTipText = "App Settings"
        '
        'AppsSettingsTabList
        '
        Me.AppsSettingsTabList.Controls.Add(Me.GlitterAppTabPage)
        Me.AppsSettingsTabList.Controls.Add(Me.TpGames)
        Me.AppsSettingsTabList.Controls.Add(Me.TabPage6)
        Me.AppsSettingsTabList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AppsSettingsTabList.Location = New System.Drawing.Point(3, 3)
        Me.AppsSettingsTabList.Name = "AppsSettingsTabList"
        Me.AppsSettingsTabList.SelectedIndex = 0
        Me.AppsSettingsTabList.Size = New System.Drawing.Size(966, 450)
        Me.AppsSettingsTabList.TabIndex = 0
        '
        'GlitterAppTabPage
        '
        Me.GlitterAppTabPage.AutoScroll = True
        Me.GlitterAppTabPage.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GlitterAppTabPage.Controls.Add(Me.DommeGlitterSettings)
        Me.GlitterAppTabPage.Controls.Add(Me.GBGlitter1)
        Me.GlitterAppTabPage.Controls.Add(Me.GBGlitter3)
        Me.GlitterAppTabPage.Controls.Add(Me.GBGlitter2)
        Me.GlitterAppTabPage.Location = New System.Drawing.Point(4, 22)
        Me.GlitterAppTabPage.Name = "GlitterAppTabPage"
        Me.GlitterAppTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.GlitterAppTabPage.Size = New System.Drawing.Size(958, 424)
        Me.GlitterAppTabPage.TabIndex = 0
        Me.GlitterAppTabPage.Text = "Glitter"
        '
        'DommeGlitterSettings
        '
        Me.DommeGlitterSettings.AvatarImageFile = Nothing
        Me.DommeGlitterSettings.BackColor = System.Drawing.Color.Transparent
        Me.DommeGlitterSettings.ChatColor = "buttontext"
        Me.DommeGlitterSettings.EnabledLabel = "Glitter Feed"
        Me.DommeGlitterSettings.GlitterContactName = ""
        Me.DommeGlitterSettings.GlitterLabel = "Domme"
        Me.DommeGlitterSettings.IsAngry = False
        Me.DommeGlitterSettings.IsBratty = False
        Me.DommeGlitterSettings.IsCaring = False
        Me.DommeGlitterSettings.IsCondescending = False
        Me.DommeGlitterSettings.IsCrazy = False
        Me.DommeGlitterSettings.IsCruel = False
        Me.DommeGlitterSettings.IsCustom1ModuleEnabled = False
        Me.DommeGlitterSettings.IsCustom2ModuleEnabled = False
        Me.DommeGlitterSettings.IsDailyModuleEnabled = False
        Me.DommeGlitterSettings.IsDegrading = False
        Me.DommeGlitterSettings.IsEgotistModuleEnabled = False
        Me.DommeGlitterSettings.IsGlitterEnabled = False
        Me.DommeGlitterSettings.IsSadistic = False
        Me.DommeGlitterSettings.IsScriptsOptionEnabled = False
        Me.DommeGlitterSettings.IsSupremacist = False
        Me.DommeGlitterSettings.IsTeaseModuleEnabled = False
        Me.DommeGlitterSettings.IsTriviaModuleEnabled = False
        Me.DommeGlitterSettings.IsVulgar = False
        Me.DommeGlitterSettings.Location = New System.Drawing.Point(10, 6)
        Me.DommeGlitterSettings.MinimumSize = New System.Drawing.Size(350, 150)
        Me.DommeGlitterSettings.Name = "DommeGlitterSettings"
        Me.DommeGlitterSettings.PostFrequency = 1
        Me.DommeGlitterSettings.ResponseFrequency = 1
        Me.DommeGlitterSettings.Size = New System.Drawing.Size(421, 300)
        Me.DommeGlitterSettings.TabIndex = 163
        '
        'GBGlitter1
        '
        Me.GBGlitter1.BackColor = System.Drawing.Color.Transparent
        Me.GBGlitter1.Controls.Add(Me.BtnContact1ImageDirClear)
        Me.GBGlitter1.Controls.Add(Me.BtnContact1ImageDir)
        Me.GBGlitter1.Controls.Add(Me.TbxContact1ImageDir)
        Me.GBGlitter1.Controls.Add(Me.BTNGlitter1)
        Me.GBGlitter1.Controls.Add(Me.LBLGlitterNC1)
        Me.GBGlitter1.Controls.Add(Me.LBLGlitterSlider1)
        Me.GBGlitter1.Controls.Add(Me.GlitterSlider1)
        Me.GBGlitter1.Controls.Add(Me.CBGlitter1)
        Me.GBGlitter1.Controls.Add(Me.TBGlitter1)
        Me.GBGlitter1.Controls.Add(Me.GlitterAV1)
        Me.GBGlitter1.Location = New System.Drawing.Point(437, 6)
        Me.GBGlitter1.Name = "GBGlitter1"
        Me.GBGlitter1.Size = New System.Drawing.Size(344, 150)
        Me.GBGlitter1.TabIndex = 161
        Me.GBGlitter1.TabStop = False
        Me.GBGlitter1.Text = "Contact 1"
        '
        'BtnContact1ImageDirClear
        '
        Me.BtnContact1ImageDirClear.BackColor = System.Drawing.Color.LightGray
        Me.BtnContact1ImageDirClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnContact1ImageDirClear.ForeColor = System.Drawing.Color.Black
        Me.BtnContact1ImageDirClear.Location = New System.Drawing.Point(174, 93)
        Me.BtnContact1ImageDirClear.Name = "BtnContact1ImageDirClear"
        Me.BtnContact1ImageDirClear.Size = New System.Drawing.Size(39, 22)
        Me.BtnContact1ImageDirClear.TabIndex = 181
        Me.BtnContact1ImageDirClear.Text = "Clear"
        Me.BtnContact1ImageDirClear.UseVisualStyleBackColor = False
        '
        'BtnContact1ImageDir
        '
        Me.BtnContact1ImageDir.BackColor = System.Drawing.Color.LightGray
        Me.BtnContact1ImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnContact1ImageDir.ForeColor = System.Drawing.Color.Black
        Me.BtnContact1ImageDir.Location = New System.Drawing.Point(9, 93)
        Me.BtnContact1ImageDir.Name = "BtnContact1ImageDir"
        Me.BtnContact1ImageDir.Size = New System.Drawing.Size(160, 22)
        Me.BtnContact1ImageDir.TabIndex = 177
        Me.BtnContact1ImageDir.Text = "Set Contact1 Images Directory"
        Me.BtnContact1ImageDir.UseVisualStyleBackColor = False
        '
        'TbxContact1ImageDir
        '
        Me.TbxContact1ImageDir.BackColor = System.Drawing.Color.LightGray
        Me.TbxContact1ImageDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbxContact1ImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbxContact1ImageDir.ForeColor = System.Drawing.Color.Black
        Me.TbxContact1ImageDir.Location = New System.Drawing.Point(9, 121)
        Me.TbxContact1ImageDir.MaximumSize = New System.Drawing.Size(2, 17)
        Me.TbxContact1ImageDir.MinimumSize = New System.Drawing.Size(204, 17)
        Me.TbxContact1ImageDir.Name = "TbxContact1ImageDir"
        Me.TbxContact1ImageDir.ReadOnly = True
        Me.TbxContact1ImageDir.Size = New System.Drawing.Size(204, 17)
        Me.TbxContact1ImageDir.TabIndex = 176
        '
        'BTNGlitter1
        '
        Me.BTNGlitter1.BackColor = System.Drawing.Color.LightGray
        Me.BTNGlitter1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNGlitter1.ForeColor = System.Drawing.Color.Black
        Me.BTNGlitter1.Location = New System.Drawing.Point(220, 23)
        Me.BTNGlitter1.Name = "BTNGlitter1"
        Me.BTNGlitter1.Size = New System.Drawing.Size(115, 24)
        Me.BTNGlitter1.TabIndex = 175
        Me.BTNGlitter1.Text = "Choose Name Color"
        Me.BTNGlitter1.UseVisualStyleBackColor = False
        '
        'LBLGlitterNC1
        '
        Me.LBLGlitterNC1.BackColor = System.Drawing.Color.White
        Me.LBLGlitterNC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLGlitterNC1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitterNC1.Location = New System.Drawing.Point(220, 57)
        Me.LBLGlitterNC1.Name = "LBLGlitterNC1"
        Me.LBLGlitterNC1.Size = New System.Drawing.Size(115, 23)
        Me.LBLGlitterNC1.TabIndex = 166
        Me.LBLGlitterNC1.Text = "Preview"
        Me.LBLGlitterNC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLGlitterSlider1
        '
        Me.LBLGlitterSlider1.BackColor = System.Drawing.Color.Transparent
        Me.LBLGlitterSlider1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitterSlider1.ForeColor = System.Drawing.Color.Black
        Me.LBLGlitterSlider1.Location = New System.Drawing.Point(220, 96)
        Me.LBLGlitterSlider1.Name = "LBLGlitterSlider1"
        Me.LBLGlitterSlider1.Size = New System.Drawing.Size(115, 19)
        Me.LBLGlitterSlider1.TabIndex = 163
        Me.LBLGlitterSlider1.Text = "Response Frequency"
        Me.LBLGlitterSlider1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GlitterSlider1
        '
        Me.GlitterSlider1.AutoSize = False
        Me.GlitterSlider1.LargeChange = 1
        Me.GlitterSlider1.Location = New System.Drawing.Point(220, 118)
        Me.GlitterSlider1.Maximum = 9
        Me.GlitterSlider1.Minimum = 1
        Me.GlitterSlider1.Name = "GlitterSlider1"
        Me.GlitterSlider1.Size = New System.Drawing.Size(115, 25)
        Me.GlitterSlider1.TabIndex = 161
        Me.GlitterSlider1.Value = 1
        '
        'CBGlitter1
        '
        Me.CBGlitter1.AutoSize = True
        Me.CBGlitter1.Checked = True
        Me.CBGlitter1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBGlitter1.ForeColor = System.Drawing.Color.Black
        Me.CBGlitter1.Location = New System.Drawing.Point(79, 26)
        Me.CBGlitter1.Name = "CBGlitter1"
        Me.CBGlitter1.Size = New System.Drawing.Size(122, 17)
        Me.CBGlitter1.TabIndex = 151
        Me.CBGlitter1.Text = "Enable This Contact"
        Me.CBGlitter1.UseVisualStyleBackColor = True
        '
        'TBGlitter1
        '
        Me.TBGlitter1.BackColor = System.Drawing.Color.White
        Me.TBGlitter1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBGlitter1.ForeColor = System.Drawing.Color.Black
        Me.TBGlitter1.Location = New System.Drawing.Point(79, 57)
        Me.TBGlitter1.Name = "TBGlitter1"
        Me.TBGlitter1.Size = New System.Drawing.Size(134, 23)
        Me.TBGlitter1.TabIndex = 49
        Me.TBGlitter1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GlitterAV1
        '
        Me.GlitterAV1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GlitterAV1.Location = New System.Drawing.Point(9, 16)
        Me.GlitterAV1.Name = "GlitterAV1"
        Me.GlitterAV1.Size = New System.Drawing.Size(64, 64)
        Me.GlitterAV1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GlitterAV1.TabIndex = 149
        Me.GlitterAV1.TabStop = False
        '
        'GBGlitter3
        '
        Me.GBGlitter3.BackColor = System.Drawing.Color.Transparent
        Me.GBGlitter3.Controls.Add(Me.BtnContact3ImageDirClear)
        Me.GBGlitter3.Controls.Add(Me.BtnContact3ImageDir)
        Me.GBGlitter3.Controls.Add(Me.TbxContact3ImageDir)
        Me.GBGlitter3.Controls.Add(Me.BTNGlitter3)
        Me.GBGlitter3.Controls.Add(Me.LBLGlitterNC3)
        Me.GBGlitter3.Controls.Add(Me.LBLGlitterSlider3)
        Me.GBGlitter3.Controls.Add(Me.GlitterSlider3)
        Me.GBGlitter3.Controls.Add(Me.CBGlitter3)
        Me.GBGlitter3.Controls.Add(Me.TBGlitter3)
        Me.GBGlitter3.Controls.Add(Me.GlitterAV3)
        Me.GBGlitter3.Location = New System.Drawing.Point(26, 312)
        Me.GBGlitter3.Name = "GBGlitter3"
        Me.GBGlitter3.Size = New System.Drawing.Size(344, 150)
        Me.GBGlitter3.TabIndex = 160
        Me.GBGlitter3.TabStop = False
        Me.GBGlitter3.Text = "Contact 3"
        '
        'BtnContact3ImageDirClear
        '
        Me.BtnContact3ImageDirClear.BackColor = System.Drawing.Color.LightGray
        Me.BtnContact3ImageDirClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnContact3ImageDirClear.ForeColor = System.Drawing.Color.Black
        Me.BtnContact3ImageDirClear.Location = New System.Drawing.Point(174, 93)
        Me.BtnContact3ImageDirClear.Name = "BtnContact3ImageDirClear"
        Me.BtnContact3ImageDirClear.Size = New System.Drawing.Size(39, 22)
        Me.BtnContact3ImageDirClear.TabIndex = 180
        Me.BtnContact3ImageDirClear.Text = "Clear"
        Me.BtnContact3ImageDirClear.UseVisualStyleBackColor = False
        '
        'BtnContact3ImageDir
        '
        Me.BtnContact3ImageDir.BackColor = System.Drawing.Color.LightGray
        Me.BtnContact3ImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnContact3ImageDir.ForeColor = System.Drawing.Color.Black
        Me.BtnContact3ImageDir.Location = New System.Drawing.Point(9, 93)
        Me.BtnContact3ImageDir.Name = "BtnContact3ImageDir"
        Me.BtnContact3ImageDir.Size = New System.Drawing.Size(160, 22)
        Me.BtnContact3ImageDir.TabIndex = 179
        Me.BtnContact3ImageDir.Text = "Set Contact3 Images Directory"
        Me.BtnContact3ImageDir.UseVisualStyleBackColor = False
        '
        'TbxContact3ImageDir
        '
        Me.TbxContact3ImageDir.BackColor = System.Drawing.Color.LightGray
        Me.TbxContact3ImageDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbxContact3ImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbxContact3ImageDir.ForeColor = System.Drawing.Color.Black
        Me.TbxContact3ImageDir.Location = New System.Drawing.Point(9, 121)
        Me.TbxContact3ImageDir.MaximumSize = New System.Drawing.Size(2, 17)
        Me.TbxContact3ImageDir.MinimumSize = New System.Drawing.Size(204, 17)
        Me.TbxContact3ImageDir.Name = "TbxContact3ImageDir"
        Me.TbxContact3ImageDir.ReadOnly = True
        Me.TbxContact3ImageDir.Size = New System.Drawing.Size(204, 17)
        Me.TbxContact3ImageDir.TabIndex = 178
        '
        'BTNGlitter3
        '
        Me.BTNGlitter3.BackColor = System.Drawing.Color.LightGray
        Me.BTNGlitter3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNGlitter3.ForeColor = System.Drawing.Color.Black
        Me.BTNGlitter3.Location = New System.Drawing.Point(220, 23)
        Me.BTNGlitter3.Name = "BTNGlitter3"
        Me.BTNGlitter3.Size = New System.Drawing.Size(115, 24)
        Me.BTNGlitter3.TabIndex = 175
        Me.BTNGlitter3.Text = "Choose Name Color"
        Me.BTNGlitter3.UseVisualStyleBackColor = False
        '
        'LBLGlitterNC3
        '
        Me.LBLGlitterNC3.BackColor = System.Drawing.Color.White
        Me.LBLGlitterNC3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLGlitterNC3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitterNC3.Location = New System.Drawing.Point(220, 57)
        Me.LBLGlitterNC3.Name = "LBLGlitterNC3"
        Me.LBLGlitterNC3.Size = New System.Drawing.Size(115, 23)
        Me.LBLGlitterNC3.TabIndex = 166
        Me.LBLGlitterNC3.Text = "Preview"
        Me.LBLGlitterNC3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLGlitterSlider3
        '
        Me.LBLGlitterSlider3.BackColor = System.Drawing.Color.Transparent
        Me.LBLGlitterSlider3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitterSlider3.ForeColor = System.Drawing.Color.Black
        Me.LBLGlitterSlider3.Location = New System.Drawing.Point(220, 96)
        Me.LBLGlitterSlider3.Name = "LBLGlitterSlider3"
        Me.LBLGlitterSlider3.Size = New System.Drawing.Size(115, 19)
        Me.LBLGlitterSlider3.TabIndex = 163
        Me.LBLGlitterSlider3.Text = "Response Frequency"
        Me.LBLGlitterSlider3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GlitterSlider3
        '
        Me.GlitterSlider3.AutoSize = False
        Me.GlitterSlider3.LargeChange = 1
        Me.GlitterSlider3.Location = New System.Drawing.Point(220, 118)
        Me.GlitterSlider3.Maximum = 9
        Me.GlitterSlider3.Minimum = 1
        Me.GlitterSlider3.Name = "GlitterSlider3"
        Me.GlitterSlider3.Size = New System.Drawing.Size(115, 25)
        Me.GlitterSlider3.TabIndex = 161
        Me.GlitterSlider3.Value = 1
        '
        'CBGlitter3
        '
        Me.CBGlitter3.AutoSize = True
        Me.CBGlitter3.Checked = True
        Me.CBGlitter3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBGlitter3.ForeColor = System.Drawing.Color.Black
        Me.CBGlitter3.Location = New System.Drawing.Point(79, 26)
        Me.CBGlitter3.Name = "CBGlitter3"
        Me.CBGlitter3.Size = New System.Drawing.Size(122, 17)
        Me.CBGlitter3.TabIndex = 151
        Me.CBGlitter3.Text = "Enable This Contact"
        Me.CBGlitter3.UseVisualStyleBackColor = True
        '
        'TBGlitter3
        '
        Me.TBGlitter3.BackColor = System.Drawing.Color.White
        Me.TBGlitter3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBGlitter3.ForeColor = System.Drawing.Color.Black
        Me.TBGlitter3.Location = New System.Drawing.Point(79, 57)
        Me.TBGlitter3.Name = "TBGlitter3"
        Me.TBGlitter3.Size = New System.Drawing.Size(134, 23)
        Me.TBGlitter3.TabIndex = 49
        Me.TBGlitter3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GlitterAV3
        '
        Me.GlitterAV3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GlitterAV3.Location = New System.Drawing.Point(9, 16)
        Me.GlitterAV3.Name = "GlitterAV3"
        Me.GlitterAV3.Size = New System.Drawing.Size(64, 64)
        Me.GlitterAV3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GlitterAV3.TabIndex = 149
        Me.GlitterAV3.TabStop = False
        '
        'GBGlitter2
        '
        Me.GBGlitter2.BackColor = System.Drawing.Color.Transparent
        Me.GBGlitter2.Controls.Add(Me.BtnContact2ImageDirClear)
        Me.GBGlitter2.Controls.Add(Me.BtnContact2ImageDir)
        Me.GBGlitter2.Controls.Add(Me.TbxContact2ImageDir)
        Me.GBGlitter2.Controls.Add(Me.BTNGlitter2)
        Me.GBGlitter2.Controls.Add(Me.LBLGlitterNC2)
        Me.GBGlitter2.Controls.Add(Me.LBLGlitterSlider2)
        Me.GBGlitter2.Controls.Add(Me.GlitterSlider2)
        Me.GBGlitter2.Controls.Add(Me.CBGlitter2)
        Me.GBGlitter2.Controls.Add(Me.TBGlitter2)
        Me.GBGlitter2.Controls.Add(Me.GlitterAV2)
        Me.GBGlitter2.Location = New System.Drawing.Point(466, 328)
        Me.GBGlitter2.Name = "GBGlitter2"
        Me.GBGlitter2.Size = New System.Drawing.Size(344, 150)
        Me.GBGlitter2.TabIndex = 151
        Me.GBGlitter2.TabStop = False
        Me.GBGlitter2.Text = "Contact 2"
        '
        'BtnContact2ImageDirClear
        '
        Me.BtnContact2ImageDirClear.BackColor = System.Drawing.Color.LightGray
        Me.BtnContact2ImageDirClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnContact2ImageDirClear.ForeColor = System.Drawing.Color.Black
        Me.BtnContact2ImageDirClear.Location = New System.Drawing.Point(174, 93)
        Me.BtnContact2ImageDirClear.Name = "BtnContact2ImageDirClear"
        Me.BtnContact2ImageDirClear.Size = New System.Drawing.Size(39, 22)
        Me.BtnContact2ImageDirClear.TabIndex = 181
        Me.BtnContact2ImageDirClear.Text = "Clear"
        Me.BtnContact2ImageDirClear.UseVisualStyleBackColor = False
        '
        'BtnContact2ImageDir
        '
        Me.BtnContact2ImageDir.BackColor = System.Drawing.Color.LightGray
        Me.BtnContact2ImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnContact2ImageDir.ForeColor = System.Drawing.Color.Black
        Me.BtnContact2ImageDir.Location = New System.Drawing.Point(9, 93)
        Me.BtnContact2ImageDir.Name = "BtnContact2ImageDir"
        Me.BtnContact2ImageDir.Size = New System.Drawing.Size(160, 22)
        Me.BtnContact2ImageDir.TabIndex = 179
        Me.BtnContact2ImageDir.Text = "Set Contact2 Images Directory"
        Me.BtnContact2ImageDir.UseVisualStyleBackColor = False
        '
        'TbxContact2ImageDir
        '
        Me.TbxContact2ImageDir.BackColor = System.Drawing.Color.LightGray
        Me.TbxContact2ImageDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbxContact2ImageDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbxContact2ImageDir.ForeColor = System.Drawing.Color.Black
        Me.TbxContact2ImageDir.Location = New System.Drawing.Point(9, 121)
        Me.TbxContact2ImageDir.MaximumSize = New System.Drawing.Size(2, 17)
        Me.TbxContact2ImageDir.MinimumSize = New System.Drawing.Size(204, 17)
        Me.TbxContact2ImageDir.Name = "TbxContact2ImageDir"
        Me.TbxContact2ImageDir.ReadOnly = True
        Me.TbxContact2ImageDir.Size = New System.Drawing.Size(204, 17)
        Me.TbxContact2ImageDir.TabIndex = 178
        '
        'BTNGlitter2
        '
        Me.BTNGlitter2.BackColor = System.Drawing.Color.LightGray
        Me.BTNGlitter2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNGlitter2.ForeColor = System.Drawing.Color.Black
        Me.BTNGlitter2.Location = New System.Drawing.Point(220, 23)
        Me.BTNGlitter2.Name = "BTNGlitter2"
        Me.BTNGlitter2.Size = New System.Drawing.Size(115, 24)
        Me.BTNGlitter2.TabIndex = 167
        Me.BTNGlitter2.Text = "Choose Name Color"
        Me.BTNGlitter2.UseVisualStyleBackColor = False
        '
        'LBLGlitterNC2
        '
        Me.LBLGlitterNC2.BackColor = System.Drawing.Color.White
        Me.LBLGlitterNC2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLGlitterNC2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitterNC2.Location = New System.Drawing.Point(220, 57)
        Me.LBLGlitterNC2.Name = "LBLGlitterNC2"
        Me.LBLGlitterNC2.Size = New System.Drawing.Size(115, 23)
        Me.LBLGlitterNC2.TabIndex = 166
        Me.LBLGlitterNC2.Text = "Preview"
        Me.LBLGlitterNC2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLGlitterSlider2
        '
        Me.LBLGlitterSlider2.BackColor = System.Drawing.Color.Transparent
        Me.LBLGlitterSlider2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitterSlider2.ForeColor = System.Drawing.Color.Black
        Me.LBLGlitterSlider2.Location = New System.Drawing.Point(220, 96)
        Me.LBLGlitterSlider2.Name = "LBLGlitterSlider2"
        Me.LBLGlitterSlider2.Size = New System.Drawing.Size(115, 19)
        Me.LBLGlitterSlider2.TabIndex = 163
        Me.LBLGlitterSlider2.Text = "Response Frequency"
        Me.LBLGlitterSlider2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GlitterSlider2
        '
        Me.GlitterSlider2.AutoSize = False
        Me.GlitterSlider2.LargeChange = 1
        Me.GlitterSlider2.Location = New System.Drawing.Point(220, 118)
        Me.GlitterSlider2.Maximum = 9
        Me.GlitterSlider2.Minimum = 1
        Me.GlitterSlider2.Name = "GlitterSlider2"
        Me.GlitterSlider2.Size = New System.Drawing.Size(115, 25)
        Me.GlitterSlider2.TabIndex = 161
        Me.GlitterSlider2.Value = 1
        '
        'CBGlitter2
        '
        Me.CBGlitter2.AutoSize = True
        Me.CBGlitter2.Checked = True
        Me.CBGlitter2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBGlitter2.ForeColor = System.Drawing.Color.Black
        Me.CBGlitter2.Location = New System.Drawing.Point(79, 26)
        Me.CBGlitter2.Name = "CBGlitter2"
        Me.CBGlitter2.Size = New System.Drawing.Size(122, 17)
        Me.CBGlitter2.TabIndex = 151
        Me.CBGlitter2.Text = "Enable This Contact"
        Me.CBGlitter2.UseVisualStyleBackColor = True
        '
        'TBGlitter2
        '
        Me.TBGlitter2.BackColor = System.Drawing.Color.White
        Me.TBGlitter2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBGlitter2.ForeColor = System.Drawing.Color.Black
        Me.TBGlitter2.Location = New System.Drawing.Point(79, 57)
        Me.TBGlitter2.Name = "TBGlitter2"
        Me.TBGlitter2.Size = New System.Drawing.Size(134, 23)
        Me.TBGlitter2.TabIndex = 49
        Me.TBGlitter2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GlitterAV2
        '
        Me.GlitterAV2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GlitterAV2.Location = New System.Drawing.Point(9, 16)
        Me.GlitterAV2.Name = "GlitterAV2"
        Me.GlitterAV2.Size = New System.Drawing.Size(64, 64)
        Me.GlitterAV2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GlitterAV2.TabIndex = 149
        Me.GlitterAV2.TabStop = False
        '
        'TpGames
        '
        Me.TpGames.BackColor = System.Drawing.Color.LightGray
        Me.TpGames.Controls.Add(Me.CBIncludeGifs)
        Me.TpGames.Controls.Add(Me.LblCardsSetupNote)
        Me.TpGames.Controls.Add(Me.CBGameSounds)
        Me.TpGames.Controls.Add(Me.GbxCardsGold)
        Me.TpGames.Controls.Add(Me.GbxCardsBackground)
        Me.TpGames.Controls.Add(Me.GbxCardsBronze)
        Me.TpGames.Controls.Add(Me.GbxCardsSilver)
        Me.TpGames.Location = New System.Drawing.Point(4, 22)
        Me.TpGames.Name = "TpGames"
        Me.TpGames.Padding = New System.Windows.Forms.Padding(3)
        Me.TpGames.Size = New System.Drawing.Size(958, 424)
        Me.TpGames.TabIndex = 1
        Me.TpGames.Text = "Games"
        '
        'CBIncludeGifs
        '
        Me.CBIncludeGifs.AutoSize = True
        Me.CBIncludeGifs.Checked = True
        Me.CBIncludeGifs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBIncludeGifs.Location = New System.Drawing.Point(528, 351)
        Me.CBIncludeGifs.Name = "CBIncludeGifs"
        Me.CBIncludeGifs.Size = New System.Drawing.Size(154, 17)
        Me.CBIncludeGifs.TabIndex = 5
        Me.CBIncludeGifs.Text = "Match Game Includes Gifs "
        Me.CBIncludeGifs.UseVisualStyleBackColor = True
        '
        'LblCardsSetupNote
        '
        Me.LblCardsSetupNote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCardsSetupNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCardsSetupNote.Location = New System.Drawing.Point(523, 249)
        Me.LblCardsSetupNote.Name = "LblCardsSetupNote"
        Me.LblCardsSetupNote.Size = New System.Drawing.Size(171, 93)
        Me.LblCardsSetupNote.TabIndex = 4
        Me.LblCardsSetupNote.Text = "Each of the pictures in this tab MUST be set before the Games app can be selected" &
    "!"
        Me.LblCardsSetupNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CBGameSounds
        '
        Me.CBGameSounds.AutoSize = True
        Me.CBGameSounds.Checked = True
        Me.CBGameSounds.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBGameSounds.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBGameSounds.ForeColor = System.Drawing.Color.Black
        Me.CBGameSounds.Location = New System.Drawing.Point(528, 379)
        Me.CBGameSounds.Name = "CBGameSounds"
        Me.CBGameSounds.Size = New System.Drawing.Size(116, 17)
        Me.CBGameSounds.TabIndex = 6
        Me.CBGameSounds.Text = "Play Game Sounds"
        Me.CBGameSounds.UseVisualStyleBackColor = True
        '
        'GbxCardsGold
        '
        Me.GbxCardsGold.Controls.Add(Me.GN6)
        Me.GbxCardsGold.Controls.Add(Me.GP6)
        Me.GbxCardsGold.Controls.Add(Me.GN2)
        Me.GbxCardsGold.Controls.Add(Me.GP2)
        Me.GbxCardsGold.Controls.Add(Me.GP5)
        Me.GbxCardsGold.Controls.Add(Me.GN1)
        Me.GbxCardsGold.Controls.Add(Me.GP1)
        Me.GbxCardsGold.Controls.Add(Me.GN5)
        Me.GbxCardsGold.Controls.Add(Me.GN3)
        Me.GbxCardsGold.Controls.Add(Me.GP3)
        Me.GbxCardsGold.Controls.Add(Me.GP4)
        Me.GbxCardsGold.Controls.Add(Me.GN4)
        Me.GbxCardsGold.Location = New System.Drawing.Point(350, 7)
        Me.GbxCardsGold.Name = "GbxCardsGold"
        Me.GbxCardsGold.Size = New System.Drawing.Size(166, 398)
        Me.GbxCardsGold.TabIndex = 2
        Me.GbxCardsGold.TabStop = False
        Me.GbxCardsGold.Text = "Gold Cards"
        '
        'GN6
        '
        Me.GN6.Location = New System.Drawing.Point(86, 367)
        Me.GN6.Name = "GN6"
        Me.GN6.Size = New System.Drawing.Size(71, 20)
        Me.GN6.TabIndex = 5
        Me.GN6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GP6
        '
        Me.GP6.BackColor = System.Drawing.Color.Silver
        Me.GP6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GP6.InitialImage = Nothing
        Me.GP6.Location = New System.Drawing.Point(86, 268)
        Me.GP6.Name = "GP6"
        Me.GP6.Size = New System.Drawing.Size(71, 93)
        Me.GP6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GP6.TabIndex = 17
        Me.GP6.TabStop = False
        '
        'GN2
        '
        Me.GN2.Location = New System.Drawing.Point(86, 117)
        Me.GN2.Name = "GN2"
        Me.GN2.Size = New System.Drawing.Size(71, 20)
        Me.GN2.TabIndex = 1
        Me.GN2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GP2
        '
        Me.GP2.BackColor = System.Drawing.Color.Silver
        Me.GP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GP2.InitialImage = Nothing
        Me.GP2.Location = New System.Drawing.Point(86, 17)
        Me.GP2.Name = "GP2"
        Me.GP2.Size = New System.Drawing.Size(71, 94)
        Me.GP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GP2.TabIndex = 9
        Me.GP2.TabStop = False
        '
        'GP5
        '
        Me.GP5.BackColor = System.Drawing.Color.Silver
        Me.GP5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GP5.InitialImage = Nothing
        Me.GP5.Location = New System.Drawing.Point(9, 268)
        Me.GP5.Name = "GP5"
        Me.GP5.Size = New System.Drawing.Size(71, 93)
        Me.GP5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GP5.TabIndex = 15
        Me.GP5.TabStop = False
        '
        'GN1
        '
        Me.GN1.Location = New System.Drawing.Point(9, 117)
        Me.GN1.Name = "GN1"
        Me.GN1.Size = New System.Drawing.Size(71, 20)
        Me.GN1.TabIndex = 0
        Me.GN1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GP1
        '
        Me.GP1.BackColor = System.Drawing.Color.Silver
        Me.GP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GP1.InitialImage = Nothing
        Me.GP1.Location = New System.Drawing.Point(9, 17)
        Me.GP1.Name = "GP1"
        Me.GP1.Size = New System.Drawing.Size(71, 94)
        Me.GP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GP1.TabIndex = 0
        Me.GP1.TabStop = False
        '
        'GN5
        '
        Me.GN5.Location = New System.Drawing.Point(9, 367)
        Me.GN5.Name = "GN5"
        Me.GN5.Size = New System.Drawing.Size(71, 20)
        Me.GN5.TabIndex = 4
        Me.GN5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GN3
        '
        Me.GN3.Location = New System.Drawing.Point(9, 242)
        Me.GN3.Name = "GN3"
        Me.GN3.Size = New System.Drawing.Size(71, 20)
        Me.GN3.TabIndex = 2
        Me.GN3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GP3
        '
        Me.GP3.BackColor = System.Drawing.Color.Silver
        Me.GP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GP3.InitialImage = Nothing
        Me.GP3.Location = New System.Drawing.Point(9, 143)
        Me.GP3.Name = "GP3"
        Me.GP3.Size = New System.Drawing.Size(71, 93)
        Me.GP3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GP3.TabIndex = 11
        Me.GP3.TabStop = False
        '
        'GP4
        '
        Me.GP4.BackColor = System.Drawing.Color.Silver
        Me.GP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GP4.InitialImage = Nothing
        Me.GP4.Location = New System.Drawing.Point(86, 143)
        Me.GP4.Name = "GP4"
        Me.GP4.Size = New System.Drawing.Size(71, 93)
        Me.GP4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GP4.TabIndex = 13
        Me.GP4.TabStop = False
        '
        'GN4
        '
        Me.GN4.Location = New System.Drawing.Point(86, 242)
        Me.GN4.Name = "GN4"
        Me.GN4.Size = New System.Drawing.Size(71, 20)
        Me.GN4.TabIndex = 3
        Me.GN4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GbxCardsBackground
        '
        Me.GbxCardsBackground.Controls.Add(Me.CardBack)
        Me.GbxCardsBackground.Location = New System.Drawing.Point(522, 7)
        Me.GbxCardsBackground.Name = "GbxCardsBackground"
        Me.GbxCardsBackground.Size = New System.Drawing.Size(172, 236)
        Me.GbxCardsBackground.TabIndex = 3
        Me.GbxCardsBackground.TabStop = False
        Me.GbxCardsBackground.Text = "Card Background"
        '
        'CardBack
        '
        Me.CardBack.BackColor = System.Drawing.Color.Silver
        Me.CardBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardBack.InitialImage = Nothing
        Me.CardBack.Location = New System.Drawing.Point(17, 28)
        Me.CardBack.Name = "CardBack"
        Me.CardBack.Size = New System.Drawing.Size(138, 179)
        Me.CardBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.CardBack.TabIndex = 18
        Me.CardBack.TabStop = False
        '
        'GbxCardsBronze
        '
        Me.GbxCardsBronze.Controls.Add(Me.BN6)
        Me.GbxCardsBronze.Controls.Add(Me.BN3)
        Me.GbxCardsBronze.Controls.Add(Me.BP3)
        Me.GbxCardsBronze.Controls.Add(Me.BP6)
        Me.GbxCardsBronze.Controls.Add(Me.BN2)
        Me.GbxCardsBronze.Controls.Add(Me.BN5)
        Me.GbxCardsBronze.Controls.Add(Me.BP5)
        Me.GbxCardsBronze.Controls.Add(Me.BP2)
        Me.GbxCardsBronze.Controls.Add(Me.BN1)
        Me.GbxCardsBronze.Controls.Add(Me.BN4)
        Me.GbxCardsBronze.Controls.Add(Me.BP4)
        Me.GbxCardsBronze.Controls.Add(Me.BP1)
        Me.GbxCardsBronze.Location = New System.Drawing.Point(6, 6)
        Me.GbxCardsBronze.Name = "GbxCardsBronze"
        Me.GbxCardsBronze.Size = New System.Drawing.Size(166, 399)
        Me.GbxCardsBronze.TabIndex = 0
        Me.GbxCardsBronze.TabStop = False
        Me.GbxCardsBronze.Text = "Bronze Cards"
        '
        'BN6
        '
        Me.BN6.Location = New System.Drawing.Point(86, 368)
        Me.BN6.Name = "BN6"
        Me.BN6.Size = New System.Drawing.Size(71, 20)
        Me.BN6.TabIndex = 5
        Me.BN6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BN3
        '
        Me.BN3.Location = New System.Drawing.Point(9, 243)
        Me.BN3.Name = "BN3"
        Me.BN3.Size = New System.Drawing.Size(71, 20)
        Me.BN3.TabIndex = 2
        Me.BN3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BP3
        '
        Me.BP3.BackColor = System.Drawing.Color.Silver
        Me.BP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BP3.InitialImage = Nothing
        Me.BP3.Location = New System.Drawing.Point(9, 144)
        Me.BP3.Name = "BP3"
        Me.BP3.Size = New System.Drawing.Size(71, 93)
        Me.BP3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BP3.TabIndex = 11
        Me.BP3.TabStop = False
        '
        'BP6
        '
        Me.BP6.BackColor = System.Drawing.Color.Silver
        Me.BP6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BP6.InitialImage = Nothing
        Me.BP6.Location = New System.Drawing.Point(86, 269)
        Me.BP6.Name = "BP6"
        Me.BP6.Size = New System.Drawing.Size(71, 93)
        Me.BP6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BP6.TabIndex = 17
        Me.BP6.TabStop = False
        '
        'BN2
        '
        Me.BN2.Location = New System.Drawing.Point(86, 118)
        Me.BN2.Name = "BN2"
        Me.BN2.Size = New System.Drawing.Size(71, 20)
        Me.BN2.TabIndex = 1
        Me.BN2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BN5
        '
        Me.BN5.Location = New System.Drawing.Point(9, 368)
        Me.BN5.Name = "BN5"
        Me.BN5.Size = New System.Drawing.Size(71, 20)
        Me.BN5.TabIndex = 4
        Me.BN5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BP5
        '
        Me.BP5.BackColor = System.Drawing.Color.Silver
        Me.BP5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BP5.InitialImage = Nothing
        Me.BP5.Location = New System.Drawing.Point(9, 269)
        Me.BP5.Name = "BP5"
        Me.BP5.Size = New System.Drawing.Size(71, 93)
        Me.BP5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BP5.TabIndex = 15
        Me.BP5.TabStop = False
        '
        'BP2
        '
        Me.BP2.BackColor = System.Drawing.Color.Silver
        Me.BP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BP2.InitialImage = Nothing
        Me.BP2.Location = New System.Drawing.Point(86, 19)
        Me.BP2.Name = "BP2"
        Me.BP2.Size = New System.Drawing.Size(71, 93)
        Me.BP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BP2.TabIndex = 9
        Me.BP2.TabStop = False
        '
        'BN1
        '
        Me.BN1.Location = New System.Drawing.Point(9, 118)
        Me.BN1.Name = "BN1"
        Me.BN1.Size = New System.Drawing.Size(71, 20)
        Me.BN1.TabIndex = 0
        Me.BN1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BN4
        '
        Me.BN4.Location = New System.Drawing.Point(86, 243)
        Me.BN4.Name = "BN4"
        Me.BN4.Size = New System.Drawing.Size(71, 20)
        Me.BN4.TabIndex = 3
        Me.BN4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BP4
        '
        Me.BP4.BackColor = System.Drawing.Color.Silver
        Me.BP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BP4.InitialImage = Nothing
        Me.BP4.Location = New System.Drawing.Point(86, 144)
        Me.BP4.Name = "BP4"
        Me.BP4.Size = New System.Drawing.Size(71, 93)
        Me.BP4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BP4.TabIndex = 13
        Me.BP4.TabStop = False
        '
        'BP1
        '
        Me.BP1.BackColor = System.Drawing.Color.Silver
        Me.BP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BP1.InitialImage = Nothing
        Me.BP1.Location = New System.Drawing.Point(9, 19)
        Me.BP1.Name = "BP1"
        Me.BP1.Size = New System.Drawing.Size(71, 93)
        Me.BP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BP1.TabIndex = 0
        Me.BP1.TabStop = False
        '
        'GbxCardsSilver
        '
        Me.GbxCardsSilver.Controls.Add(Me.SN6)
        Me.GbxCardsSilver.Controls.Add(Me.SP6)
        Me.GbxCardsSilver.Controls.Add(Me.SN2)
        Me.GbxCardsSilver.Controls.Add(Me.SP2)
        Me.GbxCardsSilver.Controls.Add(Me.SN1)
        Me.GbxCardsSilver.Controls.Add(Me.SP5)
        Me.GbxCardsSilver.Controls.Add(Me.SP1)
        Me.GbxCardsSilver.Controls.Add(Me.SN5)
        Me.GbxCardsSilver.Controls.Add(Me.SN3)
        Me.GbxCardsSilver.Controls.Add(Me.SN4)
        Me.GbxCardsSilver.Controls.Add(Me.SP3)
        Me.GbxCardsSilver.Controls.Add(Me.SP4)
        Me.GbxCardsSilver.Location = New System.Drawing.Point(178, 6)
        Me.GbxCardsSilver.Name = "GbxCardsSilver"
        Me.GbxCardsSilver.Size = New System.Drawing.Size(166, 399)
        Me.GbxCardsSilver.TabIndex = 1
        Me.GbxCardsSilver.TabStop = False
        Me.GbxCardsSilver.Text = "Silver Cards"
        '
        'SN6
        '
        Me.SN6.Location = New System.Drawing.Point(86, 368)
        Me.SN6.Name = "SN6"
        Me.SN6.Size = New System.Drawing.Size(71, 20)
        Me.SN6.TabIndex = 5
        Me.SN6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SP6
        '
        Me.SP6.BackColor = System.Drawing.Color.Silver
        Me.SP6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SP6.InitialImage = Nothing
        Me.SP6.Location = New System.Drawing.Point(86, 269)
        Me.SP6.Name = "SP6"
        Me.SP6.Size = New System.Drawing.Size(71, 93)
        Me.SP6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SP6.TabIndex = 17
        Me.SP6.TabStop = False
        '
        'SN2
        '
        Me.SN2.Location = New System.Drawing.Point(86, 118)
        Me.SN2.Name = "SN2"
        Me.SN2.Size = New System.Drawing.Size(71, 20)
        Me.SN2.TabIndex = 1
        Me.SN2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SP2
        '
        Me.SP2.BackColor = System.Drawing.Color.Silver
        Me.SP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SP2.InitialImage = Nothing
        Me.SP2.Location = New System.Drawing.Point(86, 19)
        Me.SP2.Name = "SP2"
        Me.SP2.Size = New System.Drawing.Size(71, 93)
        Me.SP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SP2.TabIndex = 9
        Me.SP2.TabStop = False
        '
        'SN1
        '
        Me.SN1.Location = New System.Drawing.Point(9, 118)
        Me.SN1.Name = "SN1"
        Me.SN1.Size = New System.Drawing.Size(71, 20)
        Me.SN1.TabIndex = 0
        Me.SN1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SP5
        '
        Me.SP5.BackColor = System.Drawing.Color.Silver
        Me.SP5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SP5.InitialImage = Nothing
        Me.SP5.Location = New System.Drawing.Point(9, 269)
        Me.SP5.Name = "SP5"
        Me.SP5.Size = New System.Drawing.Size(71, 93)
        Me.SP5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SP5.TabIndex = 15
        Me.SP5.TabStop = False
        '
        'SP1
        '
        Me.SP1.BackColor = System.Drawing.Color.Silver
        Me.SP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SP1.InitialImage = Nothing
        Me.SP1.Location = New System.Drawing.Point(9, 19)
        Me.SP1.Name = "SP1"
        Me.SP1.Size = New System.Drawing.Size(71, 93)
        Me.SP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SP1.TabIndex = 0
        Me.SP1.TabStop = False
        '
        'SN5
        '
        Me.SN5.Location = New System.Drawing.Point(9, 368)
        Me.SN5.Name = "SN5"
        Me.SN5.Size = New System.Drawing.Size(71, 20)
        Me.SN5.TabIndex = 4
        Me.SN5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SN3
        '
        Me.SN3.Location = New System.Drawing.Point(9, 243)
        Me.SN3.Name = "SN3"
        Me.SN3.Size = New System.Drawing.Size(71, 20)
        Me.SN3.TabIndex = 2
        Me.SN3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SN4
        '
        Me.SN4.Location = New System.Drawing.Point(86, 243)
        Me.SN4.Name = "SN4"
        Me.SN4.Size = New System.Drawing.Size(71, 20)
        Me.SN4.TabIndex = 3
        Me.SN4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SP3
        '
        Me.SP3.BackColor = System.Drawing.Color.Silver
        Me.SP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SP3.InitialImage = Nothing
        Me.SP3.Location = New System.Drawing.Point(9, 144)
        Me.SP3.Name = "SP3"
        Me.SP3.Size = New System.Drawing.Size(71, 93)
        Me.SP3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SP3.TabIndex = 11
        Me.SP3.TabStop = False
        '
        'SP4
        '
        Me.SP4.BackColor = System.Drawing.Color.Silver
        Me.SP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SP4.InitialImage = Nothing
        Me.SP4.Location = New System.Drawing.Point(86, 144)
        Me.SP4.Name = "SP4"
        Me.SP4.Size = New System.Drawing.Size(71, 93)
        Me.SP4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SP4.TabIndex = 13
        Me.SP4.TabStop = False
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.LightGray
        Me.TabPage6.Controls.Add(Me.Panel10)
        Me.TabPage6.Controls.Add(Me.Label107)
        Me.TabPage6.Controls.Add(Me.BTNWishlistCreate)
        Me.TabPage6.Controls.Add(Me.Label18)
        Me.TabPage6.Controls.Add(Me.PNLWishList)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(958, 424)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "Wishlist"
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.TBWishlistComment)
        Me.Panel10.Controls.Add(Me.Label32)
        Me.Panel10.Controls.Add(Me.TBWishlistItem)
        Me.Panel10.Controls.Add(Me.radioGold)
        Me.Panel10.Controls.Add(Me.Label42)
        Me.Panel10.Controls.Add(Me.radioSilver)
        Me.Panel10.Controls.Add(Me.TBWishlistURL)
        Me.Panel10.Controls.Add(Me.NBWishlistCost)
        Me.Panel10.Controls.Add(Me.Label48)
        Me.Panel10.Controls.Add(Me.Label73)
        Me.Panel10.Location = New System.Drawing.Point(38, 47)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(252, 308)
        Me.Panel10.TabIndex = 179
        '
        'TBWishlistComment
        '
        Me.TBWishlistComment.Location = New System.Drawing.Point(16, 173)
        Me.TBWishlistComment.Multiline = True
        Me.TBWishlistComment.Name = "TBWishlistComment"
        Me.TBWishlistComment.Size = New System.Drawing.Size(217, 118)
        Me.TBWishlistComment.TabIndex = 172
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(13, 4)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(58, 13)
        Me.Label32.TabIndex = 167
        Me.Label32.Text = "Item Name"
        '
        'TBWishlistItem
        '
        Me.TBWishlistItem.Location = New System.Drawing.Point(16, 20)
        Me.TBWishlistItem.Name = "TBWishlistItem"
        Me.TBWishlistItem.Size = New System.Drawing.Size(217, 20)
        Me.TBWishlistItem.TabIndex = 168
        '
        'radioGold
        '
        Me.radioGold.AutoSize = True
        Me.radioGold.Location = New System.Drawing.Point(167, 125)
        Me.radioGold.Name = "radioGold"
        Me.radioGold.Size = New System.Drawing.Size(47, 17)
        Me.radioGold.TabIndex = 176
        Me.radioGold.Text = "Gold"
        Me.radioGold.UseVisualStyleBackColor = True
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(13, 56)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(75, 13)
        Me.Label42.TabIndex = 169
        Me.Label42.Text = "Item Image Url"
        '
        'radioSilver
        '
        Me.radioSilver.AutoSize = True
        Me.radioSilver.Checked = True
        Me.radioSilver.Location = New System.Drawing.Point(100, 125)
        Me.radioSilver.Name = "radioSilver"
        Me.radioSilver.Size = New System.Drawing.Size(51, 17)
        Me.radioSilver.TabIndex = 175
        Me.radioSilver.TabStop = True
        Me.radioSilver.Text = "Silver"
        Me.radioSilver.UseVisualStyleBackColor = True
        '
        'TBWishlistURL
        '
        Me.TBWishlistURL.Location = New System.Drawing.Point(16, 72)
        Me.TBWishlistURL.Name = "TBWishlistURL"
        Me.TBWishlistURL.Size = New System.Drawing.Size(217, 20)
        Me.TBWishlistURL.TabIndex = 170
        '
        'NBWishlistCost
        '
        Me.NBWishlistCost.Location = New System.Drawing.Point(16, 125)
        Me.NBWishlistCost.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NBWishlistCost.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBWishlistCost.Name = "NBWishlistCost"
        Me.NBWishlistCost.Size = New System.Drawing.Size(40, 20)
        Me.NBWishlistCost.TabIndex = 174
        Me.NBWishlistCost.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(13, 157)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(74, 13)
        Me.Label48.TabIndex = 171
        Me.Label48.Text = "Item Comment"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(13, 108)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(51, 13)
        Me.Label73.TabIndex = 173
        Me.Label73.Text = "Item Cost"
        '
        'Label107
        '
        Me.Label107.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.Location = New System.Drawing.Point(38, 5)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(252, 47)
        Me.Label107.TabIndex = 178
        Me.Label107.Text = "Use this page to create Wishlist files."
        Me.Label107.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNWishlistCreate
        '
        Me.BTNWishlistCreate.Location = New System.Drawing.Point(38, 365)
        Me.BTNWishlistCreate.Name = "BTNWishlistCreate"
        Me.BTNWishlistCreate.Size = New System.Drawing.Size(252, 33)
        Me.BTNWishlistCreate.TabIndex = 177
        Me.BTNWishlistCreate.Text = "Create Wishlist File"
        Me.BTNWishlistCreate.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(409, 5)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(250, 23)
        Me.Label18.TabIndex = 166
        Me.Label18.Text = "Preview"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PNLWishList
        '
        Me.PNLWishList.BackColor = System.Drawing.Color.White
        Me.PNLWishList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PNLWishList.Controls.Add(Me.WishlistCostSilver)
        Me.PNLWishList.Controls.Add(Me.LBLWishListText)
        Me.PNLWishList.Controls.Add(Me.LBLWishlistCost)
        Me.PNLWishList.Controls.Add(Me.WishlistCostGold)
        Me.PNLWishList.Controls.Add(Me.LBLWishListName)
        Me.PNLWishList.Controls.Add(Me.WishlistPreview)
        Me.PNLWishList.Location = New System.Drawing.Point(407, 31)
        Me.PNLWishList.Name = "PNLWishList"
        Me.PNLWishList.Size = New System.Drawing.Size(250, 367)
        Me.PNLWishList.TabIndex = 165
        '
        'WishlistCostSilver
        '
        Me.WishlistCostSilver.BackColor = System.Drawing.Color.Transparent
        Me.WishlistCostSilver.Image = CType(resources.GetObject("WishlistCostSilver.Image"), System.Drawing.Image)
        Me.WishlistCostSilver.Location = New System.Drawing.Point(107, 206)
        Me.WishlistCostSilver.Name = "WishlistCostSilver"
        Me.WishlistCostSilver.Size = New System.Drawing.Size(28, 28)
        Me.WishlistCostSilver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.WishlistCostSilver.TabIndex = 111
        Me.WishlistCostSilver.TabStop = False
        '
        'LBLWishListText
        '
        Me.LBLWishListText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLWishListText.Location = New System.Drawing.Point(14, 247)
        Me.LBLWishListText.Name = "LBLWishListText"
        Me.LBLWishListText.Size = New System.Drawing.Size(220, 109)
        Me.LBLWishListText.TabIndex = 108
        '
        'LBLWishlistCost
        '
        Me.LBLWishlistCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLWishlistCost.ForeColor = System.Drawing.Color.Black
        Me.LBLWishlistCost.Location = New System.Drawing.Point(139, 206)
        Me.LBLWishlistCost.Name = "LBLWishlistCost"
        Me.LBLWishlistCost.Size = New System.Drawing.Size(44, 28)
        Me.LBLWishlistCost.TabIndex = 107
        Me.LBLWishlistCost.Text = "3"
        Me.LBLWishlistCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WishlistCostGold
        '
        Me.WishlistCostGold.BackColor = System.Drawing.Color.Transparent
        Me.WishlistCostGold.Image = CType(resources.GetObject("WishlistCostGold.Image"), System.Drawing.Image)
        Me.WishlistCostGold.Location = New System.Drawing.Point(107, 206)
        Me.WishlistCostGold.Name = "WishlistCostGold"
        Me.WishlistCostGold.Size = New System.Drawing.Size(28, 28)
        Me.WishlistCostGold.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.WishlistCostGold.TabIndex = 106
        Me.WishlistCostGold.TabStop = False
        Me.WishlistCostGold.Visible = False
        '
        'LBLWishListName
        '
        Me.LBLWishListName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLWishListName.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.LBLWishListName.Location = New System.Drawing.Point(14, 22)
        Me.LBLWishListName.Name = "LBLWishListName"
        Me.LBLWishListName.Size = New System.Drawing.Size(220, 23)
        Me.LBLWishListName.TabIndex = 104
        Me.LBLWishListName.Text = "Item Name Goes Here"
        Me.LBLWishListName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'WishlistPreview
        '
        Me.WishlistPreview.ImageLocation = ""
        Me.WishlistPreview.Location = New System.Drawing.Point(50, 54)
        Me.WishlistPreview.Name = "WishlistPreview"
        Me.WishlistPreview.Size = New System.Drawing.Size(145, 143)
        Me.WishlistPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.WishlistPreview.TabIndex = 101
        Me.WishlistPreview.TabStop = False
        '
        'AppsSettingsHeaderPanel
        '
        Me.AppsSettingsHeaderPanel.Controls.Add(Me.AppsSettingsLoad)
        Me.AppsSettingsHeaderPanel.Controls.Add(Me.AppsSettingsSave)
        Me.AppsSettingsHeaderPanel.Controls.Add(Me.AppsSettingsLogo)
        Me.AppsSettingsHeaderPanel.Controls.Add(Me.AppsSettingsHeaderLabel)
        Me.AppsSettingsHeaderPanel.Location = New System.Drawing.Point(3, 3)
        Me.AppsSettingsHeaderPanel.Name = "AppsSettingsHeaderPanel"
        Me.AppsSettingsHeaderPanel.Size = New System.Drawing.Size(966, 60)
        Me.AppsSettingsHeaderPanel.TabIndex = 1
        '
        'AppsSettingsLoad
        '
        Me.AppsSettingsLoad.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.AppsSettingsLoad.Dock = System.Windows.Forms.DockStyle.Right
        Me.AppsSettingsLoad.FlatAppearance.BorderSize = 0
        Me.AppsSettingsLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.AppsSettingsLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.AppsSettingsLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AppsSettingsLoad.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppsSettingsLoad.ForeColor = System.Drawing.Color.Black
        Me.AppsSettingsLoad.Image = Global.Tease_AI.My.Resources.Resources.Button_Save
        Me.AppsSettingsLoad.Location = New System.Drawing.Point(842, 0)
        Me.AppsSettingsLoad.Margin = New System.Windows.Forms.Padding(0)
        Me.AppsSettingsLoad.Name = "AppsSettingsLoad"
        Me.AppsSettingsLoad.Size = New System.Drawing.Size(62, 60)
        Me.AppsSettingsLoad.TabIndex = 153
        Me.AppsSettingsLoad.Text = "Load"
        Me.AppsSettingsLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AppsSettingsLoad.UseVisualStyleBackColor = False
        '
        'AppsSettingsSave
        '
        Me.AppsSettingsSave.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AppsSettingsSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.AppsSettingsSave.FlatAppearance.BorderSize = 0
        Me.AppsSettingsSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.AppsSettingsSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.AppsSettingsSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AppsSettingsSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppsSettingsSave.ForeColor = System.Drawing.Color.Black
        Me.AppsSettingsSave.Image = Global.Tease_AI.My.Resources.Resources.Button_Export
        Me.AppsSettingsSave.Location = New System.Drawing.Point(904, 0)
        Me.AppsSettingsSave.Margin = New System.Windows.Forms.Padding(0)
        Me.AppsSettingsSave.Name = "AppsSettingsSave"
        Me.AppsSettingsSave.Size = New System.Drawing.Size(62, 60)
        Me.AppsSettingsSave.TabIndex = 152
        Me.AppsSettingsSave.Text = "Save"
        Me.AppsSettingsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AppsSettingsSave.UseVisualStyleBackColor = False
        '
        'AppsSettingsLogo
        '
        Me.AppsSettingsLogo.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsLogo.Dock = System.Windows.Forms.DockStyle.Left
        Me.AppsSettingsLogo.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.AppsSettingsLogo.Location = New System.Drawing.Point(0, 0)
        Me.AppsSettingsLogo.Name = "AppsSettingsLogo"
        Me.AppsSettingsLogo.Size = New System.Drawing.Size(160, 60)
        Me.AppsSettingsLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.AppsSettingsLogo.TabIndex = 150
        Me.AppsSettingsLogo.TabStop = False
        '
        'AppsSettingsHeaderLabel
        '
        Me.AppsSettingsHeaderLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AppsSettingsHeaderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppsSettingsHeaderLabel.ForeColor = System.Drawing.Color.Black
        Me.AppsSettingsHeaderLabel.Location = New System.Drawing.Point(0, 0)
        Me.AppsSettingsHeaderLabel.Name = "AppsSettingsHeaderLabel"
        Me.AppsSettingsHeaderLabel.Size = New System.Drawing.Size(966, 60)
        Me.AppsSettingsHeaderLabel.TabIndex = 151
        Me.AppsSettingsHeaderLabel.Text = "Apps Settings"
        Me.AppsSettingsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage26
        '
        Me.TabPage26.BackColor = System.Drawing.Color.Silver
        Me.TabPage26.Controls.Add(Me.Panel12)
        Me.TabPage26.Location = New System.Drawing.Point(4, 22)
        Me.TabPage26.Name = "TabPage26"
        Me.TabPage26.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage26.Size = New System.Drawing.Size(972, 456)
        Me.TabPage26.TabIndex = 19
        Me.TabPage26.Text = "Themes"
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.LightGray
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.GroupBox9)
        Me.Panel12.Controls.Add(Me.PictureBox10)
        Me.Panel12.Controls.Add(Me.GroupBox5)
        Me.Panel12.Controls.Add(Me.GroupBox11)
        Me.Panel12.Controls.Add(Me.GroupBox1)
        Me.Panel12.Controls.Add(Me.Label164)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(3, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(966, 450)
        Me.Panel12.TabIndex = 93
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Button32)
        Me.GroupBox9.Controls.Add(Me.Button31)
        Me.GroupBox9.Location = New System.Drawing.Point(351, 231)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(348, 94)
        Me.GroupBox9.TabIndex = 152
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "System"
        '
        'Button32
        '
        Me.Button32.BackColor = System.Drawing.Color.Transparent
        Me.Button32.Image = Global.Tease_AI.My.Resources.Resources.Button_Save_Big
        Me.Button32.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button32.Location = New System.Drawing.Point(196, 24)
        Me.Button32.Name = "Button32"
        Me.Button32.Size = New System.Drawing.Size(135, 55)
        Me.Button32.TabIndex = 55
        Me.Button32.Text = "  Save Theme"
        Me.Button32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button32.UseVisualStyleBackColor = False
        '
        'Button31
        '
        Me.Button31.BackColor = System.Drawing.Color.Transparent
        Me.Button31.Image = Global.Tease_AI.My.Resources.Resources.Button_Import_Big
        Me.Button31.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button31.Location = New System.Drawing.Point(17, 24)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(135, 55)
        Me.Button31.TabIndex = 54
        Me.Button31.Text = "  Open Theme"
        Me.Button31.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button31.UseVisualStyleBackColor = False
        '
        'PictureBox10
        '
        Me.PictureBox10.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox10.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.PictureBox10.Location = New System.Drawing.Point(9, 6)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(160, 19)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox10.TabIndex = 151
        Me.PictureBox10.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.CBTransparentTime)
        Me.GroupBox5.Controls.Add(Me.LBLDateTimeColor2)
        Me.GroupBox5.Controls.Add(Me.Label137)
        Me.GroupBox5.Controls.Add(Me.Label138)
        Me.GroupBox5.Controls.Add(Me.LBLDateBackColor2)
        Me.GroupBox5.Controls.Add(Me.LBLTextColor)
        Me.GroupBox5.Controls.Add(Me.LBLChatWindowColor2)
        Me.GroupBox5.Controls.Add(Me.LBLTextColor2)
        Me.GroupBox5.Controls.Add(Me.LBLChatTextColor)
        Me.GroupBox5.Controls.Add(Me.LBLBackColor2)
        Me.GroupBox5.Controls.Add(Me.LBLButtonColor)
        Me.GroupBox5.Controls.Add(Me.LBLChatWindowColor)
        Me.GroupBox5.Controls.Add(Me.LBLBackColor)
        Me.GroupBox5.Controls.Add(Me.LBLChatTextColor2)
        Me.GroupBox5.Controls.Add(Me.LBLButtonColor2)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 31)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(336, 294)
        Me.GroupBox5.TabIndex = 58
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "UI Colors"
        '
        'CBTransparentTime
        '
        Me.CBTransparentTime.AutoSize = True
        Me.CBTransparentTime.Location = New System.Drawing.Point(7, 262)
        Me.CBTransparentTime.Name = "CBTransparentTime"
        Me.CBTransparentTime.Size = New System.Drawing.Size(179, 17)
        Me.CBTransparentTime.TabIndex = 23
        Me.CBTransparentTime.Text = "Transparent Date/Time Window"
        Me.CBTransparentTime.UseVisualStyleBackColor = True
        '
        'LBLDateTimeColor2
        '
        Me.LBLDateTimeColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDateTimeColor2.Location = New System.Drawing.Point(187, 190)
        Me.LBLDateTimeColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLDateTimeColor2.Name = "LBLDateTimeColor2"
        Me.LBLDateTimeColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLDateTimeColor2.TabIndex = 19
        '
        'Label137
        '
        Me.Label137.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label137.Location = New System.Drawing.Point(6, 227)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(175, 20)
        Me.Label137.TabIndex = 20
        Me.Label137.Text = "Date/Time Window Color"
        Me.Label137.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label138
        '
        Me.Label138.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label138.Location = New System.Drawing.Point(6, 193)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(175, 20)
        Me.Label138.TabIndex = 17
        Me.Label138.Text = "Date/Time Text Color"
        Me.Label138.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLDateBackColor2
        '
        Me.LBLDateBackColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDateBackColor2.Location = New System.Drawing.Point(187, 224)
        Me.LBLDateBackColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLDateBackColor2.Name = "LBLDateBackColor2"
        Me.LBLDateBackColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLDateBackColor2.TabIndex = 22
        '
        'LBLTextColor
        '
        Me.LBLTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLTextColor.Location = New System.Drawing.Point(6, 91)
        Me.LBLTextColor.Name = "LBLTextColor"
        Me.LBLTextColor.Size = New System.Drawing.Size(175, 20)
        Me.LBLTextColor.TabIndex = 7
        Me.LBLTextColor.Text = "Text Color"
        Me.LBLTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLChatWindowColor2
        '
        Me.LBLChatWindowColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLChatWindowColor2.Location = New System.Drawing.Point(187, 122)
        Me.LBLChatWindowColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLChatWindowColor2.Name = "LBLChatWindowColor2"
        Me.LBLChatWindowColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLChatWindowColor2.TabIndex = 12
        '
        'LBLTextColor2
        '
        Me.LBLTextColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLTextColor2.Location = New System.Drawing.Point(187, 88)
        Me.LBLTextColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLTextColor2.Name = "LBLTextColor2"
        Me.LBLTextColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLTextColor2.TabIndex = 9
        '
        'LBLChatTextColor
        '
        Me.LBLChatTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLChatTextColor.Location = New System.Drawing.Point(6, 159)
        Me.LBLChatTextColor.Name = "LBLChatTextColor"
        Me.LBLChatTextColor.Size = New System.Drawing.Size(175, 20)
        Me.LBLChatTextColor.TabIndex = 14
        Me.LBLChatTextColor.Text = "Chat Text Color"
        Me.LBLChatTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLBackColor2
        '
        Me.LBLBackColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLBackColor2.Location = New System.Drawing.Point(187, 20)
        Me.LBLBackColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLBackColor2.Name = "LBLBackColor2"
        Me.LBLBackColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLBackColor2.TabIndex = 3
        '
        'LBLButtonColor
        '
        Me.LBLButtonColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLButtonColor.Location = New System.Drawing.Point(6, 57)
        Me.LBLButtonColor.Name = "LBLButtonColor"
        Me.LBLButtonColor.Size = New System.Drawing.Size(175, 20)
        Me.LBLButtonColor.TabIndex = 4
        Me.LBLButtonColor.Text = "Button Color"
        Me.LBLButtonColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLChatWindowColor
        '
        Me.LBLChatWindowColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLChatWindowColor.Location = New System.Drawing.Point(6, 125)
        Me.LBLChatWindowColor.Name = "LBLChatWindowColor"
        Me.LBLChatWindowColor.Size = New System.Drawing.Size(175, 20)
        Me.LBLChatWindowColor.TabIndex = 10
        Me.LBLChatWindowColor.Text = "Chat Window Color"
        Me.LBLChatWindowColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLBackColor
        '
        Me.LBLBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLBackColor.Location = New System.Drawing.Point(6, 23)
        Me.LBLBackColor.Name = "LBLBackColor"
        Me.LBLBackColor.Size = New System.Drawing.Size(175, 20)
        Me.LBLBackColor.TabIndex = 0
        Me.LBLBackColor.Text = "Background Color"
        Me.LBLBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBLChatTextColor2
        '
        Me.LBLChatTextColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLChatTextColor2.Location = New System.Drawing.Point(187, 156)
        Me.LBLChatTextColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLChatTextColor2.Name = "LBLChatTextColor2"
        Me.LBLChatTextColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLChatTextColor2.TabIndex = 16
        '
        'LBLButtonColor2
        '
        Me.LBLButtonColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLButtonColor2.Location = New System.Drawing.Point(187, 54)
        Me.LBLButtonColor2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LBLButtonColor2.Name = "LBLButtonColor2"
        Me.LBLButtonColor2.Size = New System.Drawing.Size(136, 28)
        Me.LBLButtonColor2.TabIndex = 6
        '
        'GroupBox11
        '
        Me.GroupBox11.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox11.Controls.Add(Me.Label144)
        Me.GroupBox11.ForeColor = System.Drawing.Color.Black
        Me.GroupBox11.Location = New System.Drawing.Point(7, 331)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(692, 92)
        Me.GroupBox11.TabIndex = 65
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Description"
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.Transparent
        Me.Label144.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label144.ForeColor = System.Drawing.Color.Black
        Me.Label144.Location = New System.Drawing.Point(6, 16)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(680, 73)
        Me.Label144.TabIndex = 62
        Me.Label144.Text = "Use this page to create custom themes for Tease AI." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Themes can then be saved a" &
    "nd opened from txt files."
        Me.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CBFlipBack)
        Me.GroupBox1.Controls.Add(Me.PBBackgroundPreview)
        Me.GroupBox1.Controls.Add(Me.Button17)
        Me.GroupBox1.Controls.Add(Me.CBStretchBack)
        Me.GroupBox1.Controls.Add(Me.Button18)
        Me.GroupBox1.Location = New System.Drawing.Point(351, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(348, 195)
        Me.GroupBox1.TabIndex = 57
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Background"
        '
        'CBFlipBack
        '
        Me.CBFlipBack.Enabled = False
        Me.CBFlipBack.Location = New System.Drawing.Point(6, 153)
        Me.CBFlipBack.Name = "CBFlipBack"
        Me.CBFlipBack.Size = New System.Drawing.Size(86, 41)
        Me.CBFlipBack.TabIndex = 4
        Me.CBFlipBack.Text = "Flip Background"
        Me.CBFlipBack.UseVisualStyleBackColor = True
        '
        'PBBackgroundPreview
        '
        Me.PBBackgroundPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBBackgroundPreview.Location = New System.Drawing.Point(6, 19)
        Me.PBBackgroundPreview.Name = "PBBackgroundPreview"
        Me.PBBackgroundPreview.Size = New System.Drawing.Size(202, 133)
        Me.PBBackgroundPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBBackgroundPreview.TabIndex = 0
        Me.PBBackgroundPreview.TabStop = False
        '
        'Button17
        '
        Me.Button17.BackColor = System.Drawing.Color.Transparent
        Me.Button17.BackgroundImage = Global.Tease_AI.My.Resources.Resources.Background_Load
        Me.Button17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button17.Location = New System.Drawing.Point(228, 36)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(103, 93)
        Me.Button17.TabIndex = 1
        Me.Button17.UseVisualStyleBackColor = False
        '
        'CBStretchBack
        '
        Me.CBStretchBack.Checked = True
        Me.CBStretchBack.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBStretchBack.Location = New System.Drawing.Point(122, 153)
        Me.CBStretchBack.Name = "CBStretchBack"
        Me.CBStretchBack.Size = New System.Drawing.Size(86, 41)
        Me.CBStretchBack.TabIndex = 2
        Me.CBStretchBack.Text = "Stretch Background"
        Me.CBStretchBack.UseVisualStyleBackColor = True
        '
        'Button18
        '
        Me.Button18.Location = New System.Drawing.Point(228, 155)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(103, 31)
        Me.Button18.TabIndex = 3
        Me.Button18.Text = "Clear"
        Me.Button18.UseVisualStyleBackColor = True
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.Transparent
        Me.Label164.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label164.ForeColor = System.Drawing.Color.Black
        Me.Label164.Location = New System.Drawing.Point(7, 6)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(692, 21)
        Me.Label164.TabIndex = 49
        Me.Label164.Text = "Theme Settings"
        Me.Label164.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RangeSettingsTabPage
        '
        Me.RangeSettingsTabPage.BackColor = System.Drawing.Color.Silver
        Me.RangeSettingsTabPage.Controls.Add(Me.RangeSettingsBody)
        Me.RangeSettingsTabPage.Controls.Add(Me.RangeSettingsHeaderPanel)
        Me.RangeSettingsTabPage.Controls.Add(Me.RangeSettingsDescriptionGroupBox)
        Me.RangeSettingsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.RangeSettingsTabPage.Name = "RangeSettingsTabPage"
        Me.RangeSettingsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.RangeSettingsTabPage.Size = New System.Drawing.Size(972, 456)
        Me.RangeSettingsTabPage.TabIndex = 3
        Me.RangeSettingsTabPage.Text = "Ranges"
        '
        'RangeSettingsBody
        '
        Me.RangeSettingsBody.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RangeSettingsBody.Controls.Add(Me.RangeSettingsBodyTablePanel)
        Me.RangeSettingsBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RangeSettingsBody.Location = New System.Drawing.Point(3, 63)
        Me.RangeSettingsBody.Name = "RangeSettingsBody"
        Me.RangeSettingsBody.Size = New System.Drawing.Size(966, 225)
        Me.RangeSettingsBody.TabIndex = 91
        '
        'RangeSettingsBodyTablePanel
        '
        Me.RangeSettingsBodyTablePanel.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsBodyTablePanel.ColumnCount = 3
        Me.RangeSettingsBodyTablePanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.RangeSettingsBodyTablePanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.RangeSettingsBodyTablePanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.RangeSettingsBodyTablePanel.Controls.Add(Me.RangeSettingsBodyRightColumnPanel, 2, 0)
        Me.RangeSettingsBodyTablePanel.Controls.Add(Me.RangeSettingsBodyMiddleColumnPanel, 1, 0)
        Me.RangeSettingsBodyTablePanel.Controls.Add(Me.RangeSettingsBodyLeftColumnPanel, 0, 0)
        Me.RangeSettingsBodyTablePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RangeSettingsBodyTablePanel.Location = New System.Drawing.Point(0, 0)
        Me.RangeSettingsBodyTablePanel.Name = "RangeSettingsBodyTablePanel"
        Me.RangeSettingsBodyTablePanel.RowCount = 1
        Me.RangeSettingsBodyTablePanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.RangeSettingsBodyTablePanel.Size = New System.Drawing.Size(964, 223)
        Me.RangeSettingsBodyTablePanel.TabIndex = 174
        '
        'RangeSettingsBodyRightColumnPanel
        '
        Me.RangeSettingsBodyRightColumnPanel.Controls.Add(Me.RangeSettingsTeaseSlideshowGroupBox)
        Me.RangeSettingsBodyRightColumnPanel.Controls.Add(Me.RangeSettingsGlitterTasksGroupBox)
        Me.RangeSettingsBodyRightColumnPanel.Controls.Add(Me.RangeSettingsVideoTeaseGroupBox)
        Me.RangeSettingsBodyRightColumnPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RangeSettingsBodyRightColumnPanel.Location = New System.Drawing.Point(648, 3)
        Me.RangeSettingsBodyRightColumnPanel.Name = "RangeSettingsBodyRightColumnPanel"
        Me.RangeSettingsBodyRightColumnPanel.Size = New System.Drawing.Size(313, 534)
        Me.RangeSettingsBodyRightColumnPanel.TabIndex = 0
        '
        'RangeSettingsTeaseSlideshowGroupBox
        '
        Me.RangeSettingsTeaseSlideshowGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RangeSettingsTeaseSlideshowGroupBox.Controls.Add(Me.Label112)
        Me.RangeSettingsTeaseSlideshowGroupBox.Controls.Add(Me.NBNextImageChance)
        Me.RangeSettingsTeaseSlideshowGroupBox.Controls.Add(Me.Label6)
        Me.RangeSettingsTeaseSlideshowGroupBox.Location = New System.Drawing.Point(3, 9)
        Me.RangeSettingsTeaseSlideshowGroupBox.Name = "RangeSettingsTeaseSlideshowGroupBox"
        Me.RangeSettingsTeaseSlideshowGroupBox.Size = New System.Drawing.Size(307, 54)
        Me.RangeSettingsTeaseSlideshowGroupBox.TabIndex = 170
        Me.RangeSettingsTeaseSlideshowGroupBox.TabStop = False
        Me.RangeSettingsTeaseSlideshowGroupBox.Text = "Tease Slideshow"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.Transparent
        Me.Label112.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.ForeColor = System.Drawing.Color.Black
        Me.Label112.Location = New System.Drawing.Point(233, 21)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(50, 17)
        Me.Label112.TabIndex = 180
        Me.Label112.Text = "percent"
        Me.Label112.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBNextImageChance
        '
        Me.NBNextImageChance.Location = New System.Drawing.Point(183, 20)
        Me.NBNextImageChance.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBNextImageChance.Name = "NBNextImageChance"
        Me.NBNextImageChance.Size = New System.Drawing.Size(44, 20)
        Me.NBNextImageChance.TabIndex = 179
        Me.NBNextImageChance.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(9, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(151, 17)
        Me.Label6.TabIndex = 154
        Me.Label6.Text = "Image Next/Previous Chance:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RangeSettingsGlitterTasksGroupBox
        '
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label161)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskCBTTimeMax)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskCBTTimeMin)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label162)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label163)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label158)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskEdgeHoldTimeMax)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskEdgeHoldTimeMin)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label159)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label160)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskEdgesMax)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskEdgesMin)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label119)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label157)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label151)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskStrokingTimeMax)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskStrokingTimeMin)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label154)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label155)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskStrokesMax)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.NBTaskStrokesMin)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label146)
        Me.RangeSettingsGlitterTasksGroupBox.Controls.Add(Me.Label149)
        Me.RangeSettingsGlitterTasksGroupBox.Location = New System.Drawing.Point(3, 265)
        Me.RangeSettingsGlitterTasksGroupBox.Name = "RangeSettingsGlitterTasksGroupBox"
        Me.RangeSettingsGlitterTasksGroupBox.Size = New System.Drawing.Size(307, 139)
        Me.RangeSettingsGlitterTasksGroupBox.TabIndex = 171
        Me.RangeSettingsGlitterTasksGroupBox.TabStop = False
        Me.RangeSettingsGlitterTasksGroupBox.Text = "Letter Tasks"
        '
        'Label161
        '
        Me.Label161.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label161.BackColor = System.Drawing.Color.Transparent
        Me.Label161.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label161.ForeColor = System.Drawing.Color.Black
        Me.Label161.Location = New System.Drawing.Point(249, 110)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(50, 17)
        Me.Label161.TabIndex = 204
        Me.Label161.Text = "minutes"
        Me.Label161.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTaskCBTTimeMax
        '
        Me.NBTaskCBTTimeMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskCBTTimeMax.Location = New System.Drawing.Point(199, 110)
        Me.NBTaskCBTTimeMax.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.NBTaskCBTTimeMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskCBTTimeMax.Name = "NBTaskCBTTimeMax"
        Me.NBTaskCBTTimeMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskCBTTimeMax.TabIndex = 203
        Me.NBTaskCBTTimeMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NBTaskCBTTimeMin
        '
        Me.NBTaskCBTTimeMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskCBTTimeMin.Location = New System.Drawing.Point(133, 111)
        Me.NBTaskCBTTimeMin.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskCBTTimeMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskCBTTimeMin.Name = "NBTaskCBTTimeMin"
        Me.NBTaskCBTTimeMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskCBTTimeMin.TabIndex = 202
        Me.NBTaskCBTTimeMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label162
        '
        Me.Label162.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label162.BackColor = System.Drawing.Color.Transparent
        Me.Label162.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label162.ForeColor = System.Drawing.Color.Black
        Me.Label162.Location = New System.Drawing.Point(183, 110)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(10, 17)
        Me.Label162.TabIndex = 201
        Me.Label162.Text = "-"
        Me.Label162.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.Transparent
        Me.Label163.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label163.ForeColor = System.Drawing.Color.Black
        Me.Label163.Location = New System.Drawing.Point(12, 111)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(151, 17)
        Me.Label163.TabIndex = 200
        Me.Label163.Text = "CBT Time:"
        Me.Label163.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label158
        '
        Me.Label158.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label158.BackColor = System.Drawing.Color.Transparent
        Me.Label158.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label158.ForeColor = System.Drawing.Color.Black
        Me.Label158.Location = New System.Drawing.Point(249, 87)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(50, 17)
        Me.Label158.TabIndex = 199
        Me.Label158.Text = "minutes"
        Me.Label158.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTaskEdgeHoldTimeMax
        '
        Me.NBTaskEdgeHoldTimeMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskEdgeHoldTimeMax.Location = New System.Drawing.Point(199, 87)
        Me.NBTaskEdgeHoldTimeMax.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.NBTaskEdgeHoldTimeMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskEdgeHoldTimeMax.Name = "NBTaskEdgeHoldTimeMax"
        Me.NBTaskEdgeHoldTimeMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskEdgeHoldTimeMax.TabIndex = 198
        Me.NBTaskEdgeHoldTimeMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NBTaskEdgeHoldTimeMin
        '
        Me.NBTaskEdgeHoldTimeMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskEdgeHoldTimeMin.Location = New System.Drawing.Point(133, 88)
        Me.NBTaskEdgeHoldTimeMin.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskEdgeHoldTimeMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskEdgeHoldTimeMin.Name = "NBTaskEdgeHoldTimeMin"
        Me.NBTaskEdgeHoldTimeMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskEdgeHoldTimeMin.TabIndex = 197
        Me.NBTaskEdgeHoldTimeMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label159
        '
        Me.Label159.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label159.BackColor = System.Drawing.Color.Transparent
        Me.Label159.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label159.ForeColor = System.Drawing.Color.Black
        Me.Label159.Location = New System.Drawing.Point(183, 87)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(10, 17)
        Me.Label159.TabIndex = 196
        Me.Label159.Text = "-"
        Me.Label159.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label160
        '
        Me.Label160.BackColor = System.Drawing.Color.Transparent
        Me.Label160.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label160.ForeColor = System.Drawing.Color.Black
        Me.Label160.Location = New System.Drawing.Point(12, 88)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(151, 17)
        Me.Label160.TabIndex = 195
        Me.Label160.Text = "Edge Hold Time:"
        Me.Label160.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTaskEdgesMax
        '
        Me.NBTaskEdgesMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskEdgesMax.Location = New System.Drawing.Point(199, 64)
        Me.NBTaskEdgesMax.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NBTaskEdgesMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskEdgesMax.Name = "NBTaskEdgesMax"
        Me.NBTaskEdgesMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskEdgesMax.TabIndex = 194
        Me.NBTaskEdgesMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NBTaskEdgesMin
        '
        Me.NBTaskEdgesMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskEdgesMin.Location = New System.Drawing.Point(133, 65)
        Me.NBTaskEdgesMin.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskEdgesMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskEdgesMin.Name = "NBTaskEdgesMin"
        Me.NBTaskEdgesMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskEdgesMin.TabIndex = 193
        Me.NBTaskEdgesMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label119
        '
        Me.Label119.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label119.BackColor = System.Drawing.Color.Transparent
        Me.Label119.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.ForeColor = System.Drawing.Color.Black
        Me.Label119.Location = New System.Drawing.Point(183, 64)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(10, 17)
        Me.Label119.TabIndex = 192
        Me.Label119.Text = "-"
        Me.Label119.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label157
        '
        Me.Label157.BackColor = System.Drawing.Color.Transparent
        Me.Label157.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label157.ForeColor = System.Drawing.Color.Black
        Me.Label157.Location = New System.Drawing.Point(12, 65)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(151, 17)
        Me.Label157.TabIndex = 191
        Me.Label157.Text = "Edges:"
        Me.Label157.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label151
        '
        Me.Label151.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label151.BackColor = System.Drawing.Color.Transparent
        Me.Label151.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.ForeColor = System.Drawing.Color.Black
        Me.Label151.Location = New System.Drawing.Point(249, 41)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(50, 17)
        Me.Label151.TabIndex = 190
        Me.Label151.Text = "minutes"
        Me.Label151.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTaskStrokingTimeMax
        '
        Me.NBTaskStrokingTimeMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskStrokingTimeMax.Location = New System.Drawing.Point(199, 41)
        Me.NBTaskStrokingTimeMax.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskStrokingTimeMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskStrokingTimeMax.Name = "NBTaskStrokingTimeMax"
        Me.NBTaskStrokingTimeMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskStrokingTimeMax.TabIndex = 189
        Me.NBTaskStrokingTimeMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NBTaskStrokingTimeMin
        '
        Me.NBTaskStrokingTimeMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskStrokingTimeMin.Location = New System.Drawing.Point(133, 42)
        Me.NBTaskStrokingTimeMin.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskStrokingTimeMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskStrokingTimeMin.Name = "NBTaskStrokingTimeMin"
        Me.NBTaskStrokingTimeMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskStrokingTimeMin.TabIndex = 188
        Me.NBTaskStrokingTimeMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label154
        '
        Me.Label154.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label154.BackColor = System.Drawing.Color.Transparent
        Me.Label154.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.ForeColor = System.Drawing.Color.Black
        Me.Label154.Location = New System.Drawing.Point(183, 41)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(10, 17)
        Me.Label154.TabIndex = 187
        Me.Label154.Text = "-"
        Me.Label154.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label155
        '
        Me.Label155.BackColor = System.Drawing.Color.Transparent
        Me.Label155.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.ForeColor = System.Drawing.Color.Black
        Me.Label155.Location = New System.Drawing.Point(12, 42)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(151, 17)
        Me.Label155.TabIndex = 186
        Me.Label155.Text = "Stroking Time:"
        Me.Label155.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTaskStrokesMax
        '
        Me.NBTaskStrokesMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskStrokesMax.Location = New System.Drawing.Point(199, 18)
        Me.NBTaskStrokesMax.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskStrokesMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskStrokesMax.Name = "NBTaskStrokesMax"
        Me.NBTaskStrokesMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskStrokesMax.TabIndex = 184
        Me.NBTaskStrokesMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NBTaskStrokesMin
        '
        Me.NBTaskStrokesMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTaskStrokesMin.Location = New System.Drawing.Point(133, 19)
        Me.NBTaskStrokesMin.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NBTaskStrokesMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTaskStrokesMin.Name = "NBTaskStrokesMin"
        Me.NBTaskStrokesMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTaskStrokesMin.TabIndex = 183
        Me.NBTaskStrokesMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label146
        '
        Me.Label146.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label146.BackColor = System.Drawing.Color.Transparent
        Me.Label146.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label146.ForeColor = System.Drawing.Color.Black
        Me.Label146.Location = New System.Drawing.Point(183, 18)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(10, 17)
        Me.Label146.TabIndex = 182
        Me.Label146.Text = "-"
        Me.Label146.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.Transparent
        Me.Label149.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label149.ForeColor = System.Drawing.Color.Black
        Me.Label149.Location = New System.Drawing.Point(12, 19)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(151, 17)
        Me.Label149.TabIndex = 181
        Me.Label149.Text = "Strokes:"
        Me.Label149.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RangeSettingsVideoTeaseGroupBox
        '
        Me.RangeSettingsVideoTeaseGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RangeSettingsVideoTeaseGroupBox.Controls.Add(Me.GroupBox19)
        Me.RangeSettingsVideoTeaseGroupBox.Controls.Add(Me.RangeSettingsCensorshipSucksGroupBox)
        Me.RangeSettingsVideoTeaseGroupBox.Location = New System.Drawing.Point(3, 69)
        Me.RangeSettingsVideoTeaseGroupBox.Name = "RangeSettingsVideoTeaseGroupBox"
        Me.RangeSettingsVideoTeaseGroupBox.Size = New System.Drawing.Size(307, 190)
        Me.RangeSettingsVideoTeaseGroupBox.TabIndex = 0
        Me.RangeSettingsVideoTeaseGroupBox.TabStop = False
        Me.RangeSettingsVideoTeaseGroupBox.Text = "Video Teases"
        '
        'GroupBox19
        '
        Me.GroupBox19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox19.Controls.Add(Me.Label110)
        Me.GroupBox19.Controls.Add(Me.Label111)
        Me.GroupBox19.Controls.Add(Me.GreenLightMaximumSeconds)
        Me.GroupBox19.Controls.Add(Me.GreenLightMinimumSeconds)
        Me.GroupBox19.Controls.Add(Me.RedLightMaximumSeconds)
        Me.GroupBox19.Controls.Add(Me.Label26)
        Me.GroupBox19.Controls.Add(Me.RedLightMinimumSeconds)
        Me.GroupBox19.Controls.Add(Me.Label28)
        Me.GroupBox19.Controls.Add(Me.Label27)
        Me.GroupBox19.Controls.Add(Me.Label29)
        Me.GroupBox19.Location = New System.Drawing.Point(6, 110)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(293, 66)
        Me.GroupBox19.TabIndex = 2
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Red Light, Green Light"
        '
        'Label110
        '
        Me.Label110.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label110.BackColor = System.Drawing.Color.Transparent
        Me.Label110.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.ForeColor = System.Drawing.Color.Black
        Me.Label110.Location = New System.Drawing.Point(241, 39)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(50, 17)
        Me.Label110.TabIndex = 181
        Me.Label110.Text = "seconds"
        Me.Label110.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label111
        '
        Me.Label111.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label111.BackColor = System.Drawing.Color.Transparent
        Me.Label111.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.ForeColor = System.Drawing.Color.Black
        Me.Label111.Location = New System.Drawing.Point(241, 16)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(50, 17)
        Me.Label111.TabIndex = 180
        Me.Label111.Text = "seconds"
        Me.Label111.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GreenLightMaximumSeconds
        '
        Me.GreenLightMaximumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GreenLightMaximumSeconds.Location = New System.Drawing.Point(191, 38)
        Me.GreenLightMaximumSeconds.Name = "GreenLightMaximumSeconds"
        Me.GreenLightMaximumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.GreenLightMaximumSeconds.TabIndex = 156
        Me.TTDir.SetToolTip(Me.GreenLightMaximumSeconds, "This determines the maximum amount of time the domme will keep the video playing " &
        "while playing Red Light Green Light.")
        Me.GreenLightMaximumSeconds.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'GreenLightMinimumSeconds
        '
        Me.GreenLightMinimumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GreenLightMinimumSeconds.Location = New System.Drawing.Point(125, 38)
        Me.GreenLightMinimumSeconds.Name = "GreenLightMinimumSeconds"
        Me.GreenLightMinimumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.GreenLightMinimumSeconds.TabIndex = 155
        Me.TTDir.SetToolTip(Me.GreenLightMinimumSeconds, "This determines the minimum amount of time the domme will keep the video playing " &
        "while playing Red Light Green Light.")
        Me.GreenLightMinimumSeconds.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'RedLightMaximumSeconds
        '
        Me.RedLightMaximumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RedLightMaximumSeconds.Location = New System.Drawing.Point(191, 15)
        Me.RedLightMaximumSeconds.Name = "RedLightMaximumSeconds"
        Me.RedLightMaximumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.RedLightMaximumSeconds.TabIndex = 152
        Me.TTDir.SetToolTip(Me.RedLightMaximumSeconds, "This determines the maximum amount of time the domme will keep the video paused w" &
        "hile playing Red Light Green Light.")
        Me.RedLightMaximumSeconds.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(175, 38)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(10, 17)
        Me.Label26.TabIndex = 154
        Me.Label26.Text = "-"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RedLightMinimumSeconds
        '
        Me.RedLightMinimumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RedLightMinimumSeconds.Location = New System.Drawing.Point(125, 15)
        Me.RedLightMinimumSeconds.Name = "RedLightMinimumSeconds"
        Me.RedLightMinimumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.RedLightMinimumSeconds.TabIndex = 151
        Me.TTDir.SetToolTip(Me.RedLightMinimumSeconds, "This determines the minimum amount of time the domme will keep the video paused w" &
        "hile playing Red Light Green Light.")
        Me.RedLightMinimumSeconds.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label28
        '
        Me.Label28.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(175, 15)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(10, 17)
        Me.Label28.TabIndex = 150
        Me.Label28.Text = "-"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(6, 39)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(151, 17)
        Me.Label27.TabIndex = 153
        Me.Label27.Text = "Green Light Time:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(6, 16)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(151, 17)
        Me.Label29.TabIndex = 149
        Me.Label29.Text = "Red Light Time:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RangeSettingsCensorshipSucksGroupBox
        '
        Me.RangeSettingsCensorshipSucksGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.Label108)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.Label109)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.ShowCensorshipBarMinimumSeconds)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.HideCensorshipBarMaximumSeconds)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.HideCensorshipBarMinimumSeconds)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.CensorshipBarDuringVideoTease)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.Label25)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.Label20)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.Label19)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.Label24)
        Me.RangeSettingsCensorshipSucksGroupBox.Controls.Add(Me.ShowCensorshipBarMaximumSeconds)
        Me.RangeSettingsCensorshipSucksGroupBox.Location = New System.Drawing.Point(6, 16)
        Me.RangeSettingsCensorshipSucksGroupBox.Name = "RangeSettingsCensorshipSucksGroupBox"
        Me.RangeSettingsCensorshipSucksGroupBox.Size = New System.Drawing.Size(295, 88)
        Me.RangeSettingsCensorshipSucksGroupBox.TabIndex = 1
        Me.RangeSettingsCensorshipSucksGroupBox.TabStop = False
        Me.RangeSettingsCensorshipSucksGroupBox.Text = "Censorship Sucks"
        '
        'Label108
        '
        Me.Label108.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label108.BackColor = System.Drawing.Color.Transparent
        Me.Label108.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.ForeColor = System.Drawing.Color.Black
        Me.Label108.Location = New System.Drawing.Point(243, 39)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(50, 17)
        Me.Label108.TabIndex = 179
        Me.Label108.Text = "seconds"
        Me.Label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label109
        '
        Me.Label109.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label109.BackColor = System.Drawing.Color.Transparent
        Me.Label109.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label109.ForeColor = System.Drawing.Color.Black
        Me.Label109.Location = New System.Drawing.Point(243, 16)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(50, 17)
        Me.Label109.TabIndex = 178
        Me.Label109.Text = "seconds"
        Me.Label109.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ShowCensorshipBarMinimumSeconds
        '
        Me.ShowCensorshipBarMinimumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowCensorshipBarMinimumSeconds.Location = New System.Drawing.Point(127, 15)
        Me.ShowCensorshipBarMinimumSeconds.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.ShowCensorshipBarMinimumSeconds.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ShowCensorshipBarMinimumSeconds.Name = "ShowCensorshipBarMinimumSeconds"
        Me.ShowCensorshipBarMinimumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.ShowCensorshipBarMinimumSeconds.TabIndex = 151
        Me.TTDir.SetToolTip(Me.ShowCensorshipBarMinimumSeconds, "This determines the minimum amount of time the censor bar will be on the screen a" &
        "t a time while playing Censorship Sucks.")
        Me.ShowCensorshipBarMinimumSeconds.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'HideCensorshipBarMaximumSeconds
        '
        Me.HideCensorshipBarMaximumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HideCensorshipBarMaximumSeconds.Location = New System.Drawing.Point(193, 38)
        Me.HideCensorshipBarMaximumSeconds.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.HideCensorshipBarMaximumSeconds.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.HideCensorshipBarMaximumSeconds.Name = "HideCensorshipBarMaximumSeconds"
        Me.HideCensorshipBarMaximumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.HideCensorshipBarMaximumSeconds.TabIndex = 156
        Me.TTDir.SetToolTip(Me.HideCensorshipBarMaximumSeconds, "This determines the maximum amount of time the censor bar will be invisible while" &
        " playing Censorship Sucks.")
        Me.HideCensorshipBarMaximumSeconds.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'HideCensorshipBarMinimumSeconds
        '
        Me.HideCensorshipBarMinimumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HideCensorshipBarMinimumSeconds.Location = New System.Drawing.Point(127, 38)
        Me.HideCensorshipBarMinimumSeconds.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.HideCensorshipBarMinimumSeconds.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.HideCensorshipBarMinimumSeconds.Name = "HideCensorshipBarMinimumSeconds"
        Me.HideCensorshipBarMinimumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.HideCensorshipBarMinimumSeconds.TabIndex = 155
        Me.TTDir.SetToolTip(Me.HideCensorshipBarMinimumSeconds, "This determines the minimum amount of time the censor bar will be invisible while" &
        " playing Censorship Sucks.")
        Me.HideCensorshipBarMinimumSeconds.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'CensorshipBarDuringVideoTease
        '
        Me.CensorshipBarDuringVideoTease.AutoSize = True
        Me.CensorshipBarDuringVideoTease.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CensorshipBarDuringVideoTease.ForeColor = System.Drawing.Color.Black
        Me.CensorshipBarDuringVideoTease.Location = New System.Drawing.Point(6, 65)
        Me.CensorshipBarDuringVideoTease.Name = "CensorshipBarDuringVideoTease"
        Me.CensorshipBarDuringVideoTease.Size = New System.Drawing.Size(263, 17)
        Me.CensorshipBarDuringVideoTease.TabIndex = 157
        Me.CensorshipBarDuringVideoTease.Text = "Censorship Bar Always Visible During Video Tease"
        Me.TTDir.SetToolTip(Me.CensorshipBarDuringVideoTease, "When this is checked, the censor bar will always be visible while playing Censors" &
        "hip Sucks. Its position on the screen will still change in time with Show Censor" &
        " Bar settings.")
        Me.CensorshipBarDuringVideoTease.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(177, 15)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(10, 17)
        Me.Label25.TabIndex = 150
        Me.Label25.Text = "-"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(6, 39)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(110, 17)
        Me.Label20.TabIndex = 153
        Me.Label20.Text = "Censor Bar Hidden:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(177, 38)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(10, 17)
        Me.Label19.TabIndex = 154
        Me.Label19.Text = "-"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(6, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(110, 17)
        Me.Label24.TabIndex = 149
        Me.Label24.Text = "Censor Bar Shown:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ShowCensorshipBarMaximumSeconds
        '
        Me.ShowCensorshipBarMaximumSeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowCensorshipBarMaximumSeconds.Location = New System.Drawing.Point(193, 15)
        Me.ShowCensorshipBarMaximumSeconds.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.ShowCensorshipBarMaximumSeconds.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ShowCensorshipBarMaximumSeconds.Name = "ShowCensorshipBarMaximumSeconds"
        Me.ShowCensorshipBarMaximumSeconds.Size = New System.Drawing.Size(44, 20)
        Me.ShowCensorshipBarMaximumSeconds.TabIndex = 152
        Me.TTDir.SetToolTip(Me.ShowCensorshipBarMaximumSeconds, "This determines the maximum amount of time the censor bar will be on the screen a" &
        "t a time while playing Censorship Sucks.")
        Me.ShowCensorshipBarMaximumSeconds.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'RangeSettingsBodyMiddleColumnPanel
        '
        Me.RangeSettingsBodyMiddleColumnPanel.Controls.Add(Me.GBRangeOrgasmChance)
        Me.RangeSettingsBodyMiddleColumnPanel.Controls.Add(Me.GroupBox69)
        Me.RangeSettingsBodyMiddleColumnPanel.Controls.Add(Me.GBRangeRuinChance)
        Me.RangeSettingsBodyMiddleColumnPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RangeSettingsBodyMiddleColumnPanel.Location = New System.Drawing.Point(321, 3)
        Me.RangeSettingsBodyMiddleColumnPanel.Name = "RangeSettingsBodyMiddleColumnPanel"
        Me.RangeSettingsBodyMiddleColumnPanel.Size = New System.Drawing.Size(321, 534)
        Me.RangeSettingsBodyMiddleColumnPanel.TabIndex = 170
        '
        'GBRangeOrgasmChance
        '
        Me.GBRangeOrgasmChance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBRangeOrgasmChance.BackColor = System.Drawing.Color.Transparent
        Me.GBRangeOrgasmChance.Controls.Add(Me.RarelyAllowsPercentLabel)
        Me.GBRangeOrgasmChance.Controls.Add(Me.SometimesAllowsPercentNumberBox)
        Me.GBRangeOrgasmChance.Controls.Add(Me.SometimesAllowsPercentLabel)
        Me.GBRangeOrgasmChance.Controls.Add(Me.OftenAllowsPercentLabel)
        Me.GBRangeOrgasmChance.Controls.Add(Me.RarelyAllowsPercentNumberBox)
        Me.GBRangeOrgasmChance.Controls.Add(Me.OftenAllowsPercentNumberBox)
        Me.GBRangeOrgasmChance.Controls.Add(Me.DommeDecideOrgasmCheckBox)
        Me.GBRangeOrgasmChance.Location = New System.Drawing.Point(3, 4)
        Me.GBRangeOrgasmChance.Name = "GBRangeOrgasmChance"
        Me.GBRangeOrgasmChance.Size = New System.Drawing.Size(315, 122)
        Me.GBRangeOrgasmChance.TabIndex = 167
        Me.GBRangeOrgasmChance.TabStop = False
        Me.GBRangeOrgasmChance.Text = "Orgasm Chance"
        '
        'RarelyAllowsPercentLabel
        '
        Me.RarelyAllowsPercentLabel.BackColor = System.Drawing.Color.Transparent
        Me.RarelyAllowsPercentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RarelyAllowsPercentLabel.ForeColor = System.Drawing.Color.Black
        Me.RarelyAllowsPercentLabel.Location = New System.Drawing.Point(6, 94)
        Me.RarelyAllowsPercentLabel.Name = "RarelyAllowsPercentLabel"
        Me.RarelyAllowsPercentLabel.Size = New System.Drawing.Size(83, 17)
        Me.RarelyAllowsPercentLabel.TabIndex = 173
        Me.RarelyAllowsPercentLabel.Text = "Rarely Allows:"
        Me.RarelyAllowsPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SometimesAllowsPercentNumberBox
        '
        Me.SometimesAllowsPercentNumberBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SometimesAllowsPercentNumberBox.Enabled = False
        Me.SometimesAllowsPercentNumberBox.Location = New System.Drawing.Point(265, 65)
        Me.SometimesAllowsPercentNumberBox.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.SometimesAllowsPercentNumberBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.SometimesAllowsPercentNumberBox.Name = "SometimesAllowsPercentNumberBox"
        Me.SometimesAllowsPercentNumberBox.Size = New System.Drawing.Size(44, 20)
        Me.SometimesAllowsPercentNumberBox.TabIndex = 169
        Me.SometimesAllowsPercentNumberBox.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'SometimesAllowsPercentLabel
        '
        Me.SometimesAllowsPercentLabel.BackColor = System.Drawing.Color.Transparent
        Me.SometimesAllowsPercentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SometimesAllowsPercentLabel.ForeColor = System.Drawing.Color.Black
        Me.SometimesAllowsPercentLabel.Location = New System.Drawing.Point(6, 68)
        Me.SometimesAllowsPercentLabel.Name = "SometimesAllowsPercentLabel"
        Me.SometimesAllowsPercentLabel.Size = New System.Drawing.Size(102, 17)
        Me.SometimesAllowsPercentLabel.TabIndex = 172
        Me.SometimesAllowsPercentLabel.Text = "Sometimes Allows:"
        Me.SometimesAllowsPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OftenAllowsPercentLabel
        '
        Me.OftenAllowsPercentLabel.BackColor = System.Drawing.Color.Transparent
        Me.OftenAllowsPercentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OftenAllowsPercentLabel.ForeColor = System.Drawing.Color.Black
        Me.OftenAllowsPercentLabel.Location = New System.Drawing.Point(6, 42)
        Me.OftenAllowsPercentLabel.Name = "OftenAllowsPercentLabel"
        Me.OftenAllowsPercentLabel.Size = New System.Drawing.Size(83, 17)
        Me.OftenAllowsPercentLabel.TabIndex = 171
        Me.OftenAllowsPercentLabel.Text = "Often Allows:"
        Me.OftenAllowsPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RarelyAllowsPercentNumberBox
        '
        Me.RarelyAllowsPercentNumberBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RarelyAllowsPercentNumberBox.Enabled = False
        Me.RarelyAllowsPercentNumberBox.Location = New System.Drawing.Point(265, 91)
        Me.RarelyAllowsPercentNumberBox.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.RarelyAllowsPercentNumberBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.RarelyAllowsPercentNumberBox.Name = "RarelyAllowsPercentNumberBox"
        Me.RarelyAllowsPercentNumberBox.Size = New System.Drawing.Size(44, 20)
        Me.RarelyAllowsPercentNumberBox.TabIndex = 170
        Me.RarelyAllowsPercentNumberBox.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'OftenAllowsPercentNumberBox
        '
        Me.OftenAllowsPercentNumberBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OftenAllowsPercentNumberBox.Enabled = False
        Me.OftenAllowsPercentNumberBox.Location = New System.Drawing.Point(265, 39)
        Me.OftenAllowsPercentNumberBox.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.OftenAllowsPercentNumberBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.OftenAllowsPercentNumberBox.Name = "OftenAllowsPercentNumberBox"
        Me.OftenAllowsPercentNumberBox.Size = New System.Drawing.Size(44, 20)
        Me.OftenAllowsPercentNumberBox.TabIndex = 168
        Me.OftenAllowsPercentNumberBox.Value = New Decimal(New Integer() {75, 0, 0, 0})
        '
        'DommeDecideOrgasmCheckBox
        '
        Me.DommeDecideOrgasmCheckBox.AutoSize = True
        Me.DommeDecideOrgasmCheckBox.ForeColor = System.Drawing.Color.Black
        Me.DommeDecideOrgasmCheckBox.Location = New System.Drawing.Point(9, 19)
        Me.DommeDecideOrgasmCheckBox.Name = "DommeDecideOrgasmCheckBox"
        Me.DommeDecideOrgasmCheckBox.Size = New System.Drawing.Size(99, 17)
        Me.DommeDecideOrgasmCheckBox.TabIndex = 159
        Me.DommeDecideOrgasmCheckBox.Text = "Domme Decide"
        Me.DommeDecideOrgasmCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox69
        '
        Me.GroupBox69.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox69.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox69.Controls.Add(Me.TypesSpeedVal)
        Me.GroupBox69.Controls.Add(Me.TypeSpeedLabel)
        Me.GroupBox69.Controls.Add(Me.TimedWriting)
        Me.GroupBox69.Controls.Add(Me.TypeSpeedSlider)
        Me.GroupBox69.Location = New System.Drawing.Point(3, 260)
        Me.GroupBox69.Name = "GroupBox69"
        Me.GroupBox69.Size = New System.Drawing.Size(315, 109)
        Me.GroupBox69.TabIndex = 173
        Me.GroupBox69.TabStop = False
        Me.GroupBox69.Text = "Writing Tasks"
        '
        'TypesSpeedVal
        '
        Me.TypesSpeedVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypesSpeedVal.AutoSize = True
        Me.TypesSpeedVal.Location = New System.Drawing.Point(290, 83)
        Me.TypesSpeedVal.Name = "TypesSpeedVal"
        Me.TypesSpeedVal.Size = New System.Drawing.Size(19, 13)
        Me.TypesSpeedVal.TabIndex = 0
        Me.TypesSpeedVal.Text = "10"
        Me.TypesSpeedVal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TypeSpeedLabel
        '
        Me.TypeSpeedLabel.AutoSize = True
        Me.TypeSpeedLabel.Location = New System.Drawing.Point(6, 83)
        Me.TypeSpeedLabel.Name = "TypeSpeedLabel"
        Me.TypeSpeedLabel.Size = New System.Drawing.Size(76, 13)
        Me.TypeSpeedLabel.TabIndex = 2
        Me.TypeSpeedLabel.Text = "Typing Speed:"
        '
        'TimedWriting
        '
        Me.TimedWriting.AutoSize = True
        Me.TimedWriting.Location = New System.Drawing.Point(9, 19)
        Me.TimedWriting.Name = "TimedWriting"
        Me.TimedWriting.Size = New System.Drawing.Size(123, 17)
        Me.TimedWriting.TabIndex = 1
        Me.TimedWriting.Text = "Timed Writing Tasks"
        Me.TimedWriting.UseVisualStyleBackColor = True
        '
        'TypeSpeedSlider
        '
        Me.TypeSpeedSlider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypeSpeedSlider.BackColor = System.Drawing.SystemColors.Control
        Me.TypeSpeedSlider.Location = New System.Drawing.Point(6, 35)
        Me.TypeSpeedSlider.Maximum = 100
        Me.TypeSpeedSlider.Minimum = 33
        Me.TypeSpeedSlider.Name = "TypeSpeedSlider"
        Me.TypeSpeedSlider.Size = New System.Drawing.Size(303, 45)
        Me.TypeSpeedSlider.TabIndex = 3
        Me.TypeSpeedSlider.Value = 33
        '
        'GBRangeRuinChance
        '
        Me.GBRangeRuinChance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBRangeRuinChance.BackColor = System.Drawing.Color.Transparent
        Me.GBRangeRuinChance.Controls.Add(Me.Label90)
        Me.GBRangeRuinChance.Controls.Add(Me.NBRuinSometimes)
        Me.GBRangeRuinChance.Controls.Add(Me.Label91)
        Me.GBRangeRuinChance.Controls.Add(Me.Label92)
        Me.GBRangeRuinChance.Controls.Add(Me.NBRuinRarely)
        Me.GBRangeRuinChance.Controls.Add(Me.NBRuinOften)
        Me.GBRangeRuinChance.Controls.Add(Me.DommeDecideRuinCheckBox)
        Me.GBRangeRuinChance.Location = New System.Drawing.Point(3, 132)
        Me.GBRangeRuinChance.Name = "GBRangeRuinChance"
        Me.GBRangeRuinChance.Size = New System.Drawing.Size(315, 122)
        Me.GBRangeRuinChance.TabIndex = 168
        Me.GBRangeRuinChance.TabStop = False
        Me.GBRangeRuinChance.Text = "Ruin Chance"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.Transparent
        Me.Label90.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.ForeColor = System.Drawing.Color.Black
        Me.Label90.Location = New System.Drawing.Point(6, 94)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(83, 17)
        Me.Label90.TabIndex = 173
        Me.Label90.Text = "Rarely Ruins:"
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBRuinSometimes
        '
        Me.NBRuinSometimes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBRuinSometimes.Enabled = False
        Me.NBRuinSometimes.Location = New System.Drawing.Point(265, 65)
        Me.NBRuinSometimes.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NBRuinSometimes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBRuinSometimes.Name = "NBRuinSometimes"
        Me.NBRuinSometimes.Size = New System.Drawing.Size(44, 20)
        Me.NBRuinSometimes.TabIndex = 169
        Me.NBRuinSometimes.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.Transparent
        Me.Label91.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label91.ForeColor = System.Drawing.Color.Black
        Me.Label91.Location = New System.Drawing.Point(6, 68)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(102, 17)
        Me.Label91.TabIndex = 172
        Me.Label91.Text = "Sometimes Ruins:"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.Transparent
        Me.Label92.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.ForeColor = System.Drawing.Color.Black
        Me.Label92.Location = New System.Drawing.Point(6, 42)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(72, 17)
        Me.Label92.TabIndex = 171
        Me.Label92.Text = "Often Ruins:"
        Me.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBRuinRarely
        '
        Me.NBRuinRarely.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBRuinRarely.Enabled = False
        Me.NBRuinRarely.Location = New System.Drawing.Point(265, 91)
        Me.NBRuinRarely.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NBRuinRarely.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBRuinRarely.Name = "NBRuinRarely"
        Me.NBRuinRarely.Size = New System.Drawing.Size(44, 20)
        Me.NBRuinRarely.TabIndex = 170
        Me.NBRuinRarely.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'NBRuinOften
        '
        Me.NBRuinOften.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBRuinOften.Enabled = False
        Me.NBRuinOften.Location = New System.Drawing.Point(265, 39)
        Me.NBRuinOften.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NBRuinOften.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBRuinOften.Name = "NBRuinOften"
        Me.NBRuinOften.Size = New System.Drawing.Size(44, 20)
        Me.NBRuinOften.TabIndex = 168
        Me.NBRuinOften.Value = New Decimal(New Integer() {75, 0, 0, 0})
        '
        'DommeDecideRuinCheckBox
        '
        Me.DommeDecideRuinCheckBox.AutoSize = True
        Me.DommeDecideRuinCheckBox.ForeColor = System.Drawing.Color.Black
        Me.DommeDecideRuinCheckBox.Location = New System.Drawing.Point(9, 19)
        Me.DommeDecideRuinCheckBox.Name = "DommeDecideRuinCheckBox"
        Me.DommeDecideRuinCheckBox.Size = New System.Drawing.Size(99, 17)
        Me.DommeDecideRuinCheckBox.TabIndex = 159
        Me.DommeDecideRuinCheckBox.Text = "Domme Decide"
        Me.DommeDecideRuinCheckBox.UseVisualStyleBackColor = True
        '
        'RangeSettingsBodyLeftColumnPanel
        '
        Me.RangeSettingsBodyLeftColumnPanel.Controls.Add(Me.RangeSettingsTeaseGroupBox)
        Me.RangeSettingsBodyLeftColumnPanel.Controls.Add(Me.RangeSettingsSessionTasksGroupBox)
        Me.RangeSettingsBodyLeftColumnPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RangeSettingsBodyLeftColumnPanel.Location = New System.Drawing.Point(3, 3)
        Me.RangeSettingsBodyLeftColumnPanel.Name = "RangeSettingsBodyLeftColumnPanel"
        Me.RangeSettingsBodyLeftColumnPanel.Size = New System.Drawing.Size(312, 534)
        Me.RangeSettingsBodyLeftColumnPanel.TabIndex = 171
        '
        'RangeSettingsTeaseGroupBox
        '
        Me.RangeSettingsTeaseGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RangeSettingsTeaseGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label139)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.NBTauntEdging)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.VideoTauntDescriptionLabel)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.LBLStf)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.SliderSTF)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.VideoTauntSlider)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.VideoTauntSliderLabel)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.CBTauntCycleDD)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.TeaseLengthDommeDetermined)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label103)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.NBTauntCycleMax)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label105)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label101)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.NBTauntCycleMin)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label102)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label97)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.NBTeaseLengthMax)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label99)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label96)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.NBTeaseLengthMin)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label95)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label49)
        Me.RangeSettingsTeaseGroupBox.Controls.Add(Me.Label141)
        Me.RangeSettingsTeaseGroupBox.Location = New System.Drawing.Point(3, 4)
        Me.RangeSettingsTeaseGroupBox.Name = "RangeSettingsTeaseGroupBox"
        Me.RangeSettingsTeaseGroupBox.Size = New System.Drawing.Size(306, 308)
        Me.RangeSettingsTeaseGroupBox.TabIndex = 169
        Me.RangeSettingsTeaseGroupBox.TabStop = False
        Me.RangeSettingsTeaseGroupBox.Text = "Tease"
        '
        'Label139
        '
        Me.Label139.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label139.BackColor = System.Drawing.Color.Transparent
        Me.Label139.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label139.ForeColor = System.Drawing.Color.Black
        Me.Label139.Location = New System.Drawing.Point(238, 172)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(50, 17)
        Me.Label139.TabIndex = 184
        Me.Label139.Text = "percent"
        Me.Label139.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTauntEdging
        '
        Me.NBTauntEdging.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTauntEdging.Location = New System.Drawing.Point(193, 172)
        Me.NBTauntEdging.Name = "NBTauntEdging"
        Me.NBTauntEdging.Size = New System.Drawing.Size(44, 20)
        Me.NBTauntEdging.TabIndex = 188
        Me.NBTauntEdging.Value = New Decimal(New Integer() {70, 0, 0, 0})
        '
        'VideoTauntDescriptionLabel
        '
        Me.VideoTauntDescriptionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VideoTauntDescriptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.VideoTauntDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoTauntDescriptionLabel.ForeColor = System.Drawing.Color.Black
        Me.VideoTauntDescriptionLabel.Location = New System.Drawing.Point(193, 279)
        Me.VideoTauntDescriptionLabel.Name = "VideoTauntDescriptionLabel"
        Me.VideoTauntDescriptionLabel.Size = New System.Drawing.Size(87, 17)
        Me.VideoTauntDescriptionLabel.TabIndex = 187
        Me.VideoTauntDescriptionLabel.Text = "Normal"
        Me.VideoTauntDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLStf
        '
        Me.LBLStf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LBLStf.BackColor = System.Drawing.Color.Transparent
        Me.LBLStf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLStf.ForeColor = System.Drawing.Color.Black
        Me.LBLStf.Location = New System.Drawing.Point(193, 233)
        Me.LBLStf.Name = "LBLStf"
        Me.LBLStf.Size = New System.Drawing.Size(87, 17)
        Me.LBLStf.TabIndex = 165
        Me.LBLStf.Text = "Normal"
        Me.LBLStf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SliderSTF
        '
        Me.SliderSTF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SliderSTF.AutoSize = False
        Me.SliderSTF.BackColor = System.Drawing.SystemColors.Control
        Me.SliderSTF.LargeChange = 1
        Me.SliderSTF.Location = New System.Drawing.Point(193, 200)
        Me.SliderSTF.Maximum = 5
        Me.SliderSTF.Minimum = 1
        Me.SliderSTF.Name = "SliderSTF"
        Me.SliderSTF.Size = New System.Drawing.Size(87, 20)
        Me.SliderSTF.TabIndex = 163
        Me.SliderSTF.Value = 3
        '
        'VideoTauntSlider
        '
        Me.VideoTauntSlider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VideoTauntSlider.AutoSize = False
        Me.VideoTauntSlider.BackColor = System.Drawing.SystemColors.Control
        Me.VideoTauntSlider.LargeChange = 1
        Me.VideoTauntSlider.Location = New System.Drawing.Point(193, 256)
        Me.VideoTauntSlider.Minimum = 1
        Me.VideoTauntSlider.Name = "VideoTauntSlider"
        Me.VideoTauntSlider.Size = New System.Drawing.Size(87, 20)
        Me.VideoTauntSlider.TabIndex = 161
        Me.VideoTauntSlider.TickFrequency = 2
        Me.TTDir.SetToolTip(Me.VideoTauntSlider, resources.GetString("VideoTauntSlider.ToolTip"))
        Me.VideoTauntSlider.Value = 4
        '
        'VideoTauntSliderLabel
        '
        Me.VideoTauntSliderLabel.BackColor = System.Drawing.Color.Transparent
        Me.VideoTauntSliderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoTauntSliderLabel.ForeColor = System.Drawing.Color.Black
        Me.VideoTauntSliderLabel.Location = New System.Drawing.Point(6, 256)
        Me.VideoTauntSliderLabel.Name = "VideoTauntSliderLabel"
        Me.VideoTauntSliderLabel.Size = New System.Drawing.Size(123, 17)
        Me.VideoTauntSliderLabel.TabIndex = 186
        Me.VideoTauntSliderLabel.Text = "Video Taunt Frequency:"
        Me.VideoTauntSliderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBTauntCycleDD
        '
        Me.CBTauntCycleDD.AutoSize = True
        Me.CBTauntCycleDD.ForeColor = System.Drawing.Color.Black
        Me.CBTauntCycleDD.Location = New System.Drawing.Point(9, 140)
        Me.CBTauntCycleDD.Name = "CBTauntCycleDD"
        Me.CBTauntCycleDD.Size = New System.Drawing.Size(176, 17)
        Me.CBTauntCycleDD.TabIndex = 185
        Me.CBTauntCycleDD.Text = "Domme Decide Based on Level"
        Me.CBTauntCycleDD.UseVisualStyleBackColor = True
        '
        'TeaseLengthDommeDetermined
        '
        Me.TeaseLengthDommeDetermined.AutoSize = True
        Me.TeaseLengthDommeDetermined.ForeColor = System.Drawing.Color.Black
        Me.TeaseLengthDommeDetermined.Location = New System.Drawing.Point(9, 69)
        Me.TeaseLengthDommeDetermined.Name = "TeaseLengthDommeDetermined"
        Me.TeaseLengthDommeDetermined.Size = New System.Drawing.Size(176, 17)
        Me.TeaseLengthDommeDetermined.TabIndex = 184
        Me.TeaseLengthDommeDetermined.Text = "Domme Decide Based on Level"
        Me.TTDir.SetToolTip(Me.TeaseLengthDommeDetermined, resources.GetString("TeaseLengthDommeDetermined.ToolTip"))
        Me.TeaseLengthDommeDetermined.UseVisualStyleBackColor = True
        '
        'Label103
        '
        Me.Label103.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label103.BackColor = System.Drawing.Color.Transparent
        Me.Label103.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.ForeColor = System.Drawing.Color.Black
        Me.Label103.Location = New System.Drawing.Point(238, 118)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(50, 17)
        Me.Label103.TabIndex = 183
        Me.Label103.Text = "minutes"
        Me.Label103.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTauntCycleMax
        '
        Me.NBTauntCycleMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTauntCycleMax.Location = New System.Drawing.Point(193, 117)
        Me.NBTauntCycleMax.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NBTauntCycleMax.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NBTauntCycleMax.Name = "NBTauntCycleMax"
        Me.NBTauntCycleMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTauntCycleMax.TabIndex = 182
        Me.NBTauntCycleMax.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.Transparent
        Me.Label105.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.ForeColor = System.Drawing.Color.Black
        Me.Label105.Location = New System.Drawing.Point(6, 117)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(123, 17)
        Me.Label105.TabIndex = 181
        Me.Label105.Text = "Taunt Cycle Maximum:"
        Me.Label105.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label101
        '
        Me.Label101.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label101.BackColor = System.Drawing.Color.Transparent
        Me.Label101.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.ForeColor = System.Drawing.Color.Black
        Me.Label101.Location = New System.Drawing.Point(238, 94)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(50, 17)
        Me.Label101.TabIndex = 180
        Me.Label101.Text = "minutes"
        Me.Label101.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTauntCycleMin
        '
        Me.NBTauntCycleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTauntCycleMin.Location = New System.Drawing.Point(193, 93)
        Me.NBTauntCycleMin.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NBTauntCycleMin.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NBTauntCycleMin.Name = "NBTauntCycleMin"
        Me.NBTauntCycleMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTauntCycleMin.TabIndex = 179
        Me.NBTauntCycleMin.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.Transparent
        Me.Label102.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.ForeColor = System.Drawing.Color.Black
        Me.Label102.Location = New System.Drawing.Point(6, 93)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(123, 17)
        Me.Label102.TabIndex = 178
        Me.Label102.Text = "Taunt Cycle Minimum:"
        Me.Label102.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label97
        '
        Me.Label97.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label97.BackColor = System.Drawing.Color.Transparent
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.ForeColor = System.Drawing.Color.Black
        Me.Label97.Location = New System.Drawing.Point(238, 47)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(50, 17)
        Me.Label97.TabIndex = 177
        Me.Label97.Text = "minutes"
        Me.Label97.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTeaseLengthMax
        '
        Me.NBTeaseLengthMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTeaseLengthMax.Location = New System.Drawing.Point(193, 46)
        Me.NBTeaseLengthMax.Maximum = New Decimal(New Integer() {720, 0, 0, 0})
        Me.NBTeaseLengthMax.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NBTeaseLengthMax.Name = "NBTeaseLengthMax"
        Me.NBTeaseLengthMax.Size = New System.Drawing.Size(44, 20)
        Me.NBTeaseLengthMax.TabIndex = 176
        Me.NBTeaseLengthMax.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.Transparent
        Me.Label99.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.ForeColor = System.Drawing.Color.Black
        Me.Label99.Location = New System.Drawing.Point(6, 46)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(123, 17)
        Me.Label99.TabIndex = 175
        Me.Label99.Text = "Tease Length Maximum:"
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label96
        '
        Me.Label96.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label96.BackColor = System.Drawing.Color.Transparent
        Me.Label96.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.ForeColor = System.Drawing.Color.Black
        Me.Label96.Location = New System.Drawing.Point(238, 21)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(50, 17)
        Me.Label96.TabIndex = 174
        Me.Label96.Text = "minutes"
        Me.Label96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NBTeaseLengthMin
        '
        Me.NBTeaseLengthMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NBTeaseLengthMin.Location = New System.Drawing.Point(193, 20)
        Me.NBTeaseLengthMin.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.NBTeaseLengthMin.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NBTeaseLengthMin.Name = "NBTeaseLengthMin"
        Me.NBTeaseLengthMin.Size = New System.Drawing.Size(44, 20)
        Me.NBTeaseLengthMin.TabIndex = 169
        Me.NBTeaseLengthMin.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.Transparent
        Me.Label95.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.ForeColor = System.Drawing.Color.Black
        Me.Label95.Location = New System.Drawing.Point(6, 20)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(123, 17)
        Me.Label95.TabIndex = 166
        Me.Label95.Text = "Tease Length Minimum:"
        Me.Label95.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.Black
        Me.Label49.Location = New System.Drawing.Point(6, 207)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(132, 17)
        Me.Label49.TabIndex = 164
        Me.Label49.Text = "Stroke Taunt Frequency:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.Transparent
        Me.Label141.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.ForeColor = System.Drawing.Color.Black
        Me.Label141.Location = New System.Drawing.Point(6, 163)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(141, 32)
        Me.Label141.TabIndex = 189
        Me.Label141.Text = "Edging Ends Taunts:"
        Me.Label141.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RangeSettingsSessionTasksGroupBox
        '
        Me.RangeSettingsSessionTasksGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RangeSettingsSessionTasksGroupBox.Controls.Add(Me.TaskWaitMaximum)
        Me.RangeSettingsSessionTasksGroupBox.Controls.Add(Me.TaskWaitMinimum)
        Me.RangeSettingsSessionTasksGroupBox.Controls.Add(Me.Label165)
        Me.RangeSettingsSessionTasksGroupBox.Controls.Add(Me.Label166)
        Me.RangeSettingsSessionTasksGroupBox.Location = New System.Drawing.Point(3, 318)
        Me.RangeSettingsSessionTasksGroupBox.Name = "RangeSettingsSessionTasksGroupBox"
        Me.RangeSettingsSessionTasksGroupBox.Size = New System.Drawing.Size(306, 51)
        Me.RangeSettingsSessionTasksGroupBox.TabIndex = 172
        Me.RangeSettingsSessionTasksGroupBox.TabStop = False
        Me.RangeSettingsSessionTasksGroupBox.Text = "Session Tasks"
        '
        'TaskWaitMaximum
        '
        Me.TaskWaitMaximum.Location = New System.Drawing.Point(113, 20)
        Me.TaskWaitMaximum.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.TaskWaitMaximum.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.TaskWaitMaximum.Name = "TaskWaitMaximum"
        Me.TaskWaitMaximum.Size = New System.Drawing.Size(44, 20)
        Me.TaskWaitMaximum.TabIndex = 187
        Me.TaskWaitMaximum.Value = New Decimal(New Integer() {6, 0, 0, 0})
        '
        'TaskWaitMinimum
        '
        Me.TaskWaitMinimum.Location = New System.Drawing.Point(54, 21)
        Me.TaskWaitMinimum.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.TaskWaitMinimum.Name = "TaskWaitMinimum"
        Me.TaskWaitMinimum.Size = New System.Drawing.Size(44, 20)
        Me.TaskWaitMinimum.TabIndex = 186
        Me.TaskWaitMinimum.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.Transparent
        Me.Label165.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label165.ForeColor = System.Drawing.Color.Black
        Me.Label165.Location = New System.Drawing.Point(100, 20)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(10, 17)
        Me.Label165.TabIndex = 185
        Me.Label165.Text = "-"
        Me.Label165.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.Transparent
        Me.Label166.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label166.ForeColor = System.Drawing.Color.Black
        Me.Label166.Location = New System.Drawing.Point(6, 21)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(49, 17)
        Me.Label166.TabIndex = 188
        Me.Label166.Text = "Amount:"
        Me.Label166.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RangeSettingsHeaderPanel
        '
        Me.RangeSettingsHeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsHeaderPanel.Controls.Add(Me.RangeSettingsLogo)
        Me.RangeSettingsHeaderPanel.Controls.Add(Me.RangeSettingsHeaderLabel)
        Me.RangeSettingsHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.RangeSettingsHeaderPanel.Location = New System.Drawing.Point(3, 3)
        Me.RangeSettingsHeaderPanel.Name = "RangeSettingsHeaderPanel"
        Me.RangeSettingsHeaderPanel.Size = New System.Drawing.Size(966, 60)
        Me.RangeSettingsHeaderPanel.TabIndex = 174
        '
        'RangeSettingsLogo
        '
        Me.RangeSettingsLogo.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsLogo.Dock = System.Windows.Forms.DockStyle.Left
        Me.RangeSettingsLogo.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.RangeSettingsLogo.Location = New System.Drawing.Point(0, 0)
        Me.RangeSettingsLogo.Name = "RangeSettingsLogo"
        Me.RangeSettingsLogo.Size = New System.Drawing.Size(160, 60)
        Me.RangeSettingsLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.RangeSettingsLogo.TabIndex = 166
        Me.RangeSettingsLogo.TabStop = False
        '
        'RangeSettingsHeaderLabel
        '
        Me.RangeSettingsHeaderLabel.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RangeSettingsHeaderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RangeSettingsHeaderLabel.ForeColor = System.Drawing.Color.Black
        Me.RangeSettingsHeaderLabel.Location = New System.Drawing.Point(0, 0)
        Me.RangeSettingsHeaderLabel.Name = "RangeSettingsHeaderLabel"
        Me.RangeSettingsHeaderLabel.Size = New System.Drawing.Size(966, 60)
        Me.RangeSettingsHeaderLabel.TabIndex = 48
        Me.RangeSettingsHeaderLabel.Text = "Range Settings"
        Me.RangeSettingsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RangeSettingsDescriptionGroupBox
        '
        Me.RangeSettingsDescriptionGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsDescriptionGroupBox.Controls.Add(Me.RangeSettingsDescriptionLabel)
        Me.RangeSettingsDescriptionGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RangeSettingsDescriptionGroupBox.ForeColor = System.Drawing.Color.Black
        Me.RangeSettingsDescriptionGroupBox.Location = New System.Drawing.Point(3, 288)
        Me.RangeSettingsDescriptionGroupBox.Name = "RangeSettingsDescriptionGroupBox"
        Me.RangeSettingsDescriptionGroupBox.Size = New System.Drawing.Size(966, 165)
        Me.RangeSettingsDescriptionGroupBox.TabIndex = 66
        Me.RangeSettingsDescriptionGroupBox.TabStop = False
        Me.RangeSettingsDescriptionGroupBox.Text = "Description"
        '
        'RangeSettingsDescriptionLabel
        '
        Me.RangeSettingsDescriptionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RangeSettingsDescriptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.RangeSettingsDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RangeSettingsDescriptionLabel.ForeColor = System.Drawing.Color.Black
        Me.RangeSettingsDescriptionLabel.Location = New System.Drawing.Point(9, 16)
        Me.RangeSettingsDescriptionLabel.Name = "RangeSettingsDescriptionLabel"
        Me.RangeSettingsDescriptionLabel.Size = New System.Drawing.Size(950, 144)
        Me.RangeSettingsDescriptionLabel.TabIndex = 62
        Me.RangeSettingsDescriptionLabel.Text = "Hover over any setting in the menu for a more detailed description of its functio" &
    "n."
        Me.RangeSettingsDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage13
        '
        Me.TabPage13.BackColor = System.Drawing.Color.Silver
        Me.TabPage13.Controls.Add(Me.ModSubTab)
        Me.TabPage13.Location = New System.Drawing.Point(4, 22)
        Me.TabPage13.Name = "TabPage13"
        Me.TabPage13.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage13.Size = New System.Drawing.Size(972, 456)
        Me.TabPage13.TabIndex = 13
        Me.TabPage13.Text = "Modding"
        '
        'ModSubTab
        '
        Me.ModSubTab.Controls.Add(Me.ModPlaylistTabPage)
        Me.ModSubTab.Controls.Add(Me.TabPage14)
        Me.ModSubTab.Controls.Add(Me.TabPage24)
        Me.ModSubTab.Controls.Add(Me.TabPage8)
        Me.ModSubTab.Controls.Add(Me.TabPage15)
        Me.ModSubTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModSubTab.Location = New System.Drawing.Point(3, 3)
        Me.ModSubTab.Name = "ModSubTab"
        Me.ModSubTab.SelectedIndex = 0
        Me.ModSubTab.Size = New System.Drawing.Size(966, 450)
        Me.ModSubTab.TabIndex = 0
        '
        'ModPlaylistTabPage
        '
        Me.ModPlaylistTabPage.BackColor = System.Drawing.Color.LightGray
        Me.ModPlaylistTabPage.Controls.Add(Me.TBPlaylistSave)
        Me.ModPlaylistTabPage.Controls.Add(Me.BTNPlaylistCtrlZ)
        Me.ModPlaylistTabPage.Controls.Add(Me.RadioPlaylistRegScripts)
        Me.ModPlaylistTabPage.Controls.Add(Me.RadioPlaylistScripts)
        Me.ModPlaylistTabPage.Controls.Add(Me.BTNPlaylistEnd)
        Me.ModPlaylistTabPage.Controls.Add(Me.BTNPlaylistClearAll)
        Me.ModPlaylistTabPage.Controls.Add(Me.BTNPlaylistSave)
        Me.ModPlaylistTabPage.Controls.Add(Me.Button7)
        Me.ModPlaylistTabPage.Controls.Add(Me.ScriptPlayList)
        Me.ModPlaylistTabPage.Controls.Add(Me.Label80)
        Me.ModPlaylistTabPage.Controls.Add(Me.LBLPlaylIstLink)
        Me.ModPlaylistTabPage.Controls.Add(Me.LBLPlaylistModule)
        Me.ModPlaylistTabPage.Controls.Add(Me.LBLPLaylistStart)
        Me.ModPlaylistTabPage.Controls.Add(Me.LBPlaylist)
        Me.ModPlaylistTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ModPlaylistTabPage.Name = "ModPlaylistTabPage"
        Me.ModPlaylistTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ModPlaylistTabPage.Size = New System.Drawing.Size(958, 424)
        Me.ModPlaylistTabPage.TabIndex = 5
        Me.ModPlaylistTabPage.Text = "Playlists"
        '
        'TBPlaylistSave
        '
        Me.TBPlaylistSave.Location = New System.Drawing.Point(413, 371)
        Me.TBPlaylistSave.Name = "TBPlaylistSave"
        Me.TBPlaylistSave.Size = New System.Drawing.Size(201, 20)
        Me.TBPlaylistSave.TabIndex = 203
        '
        'BTNPlaylistCtrlZ
        '
        Me.BTNPlaylistCtrlZ.Enabled = False
        Me.BTNPlaylistCtrlZ.Location = New System.Drawing.Point(621, 21)
        Me.BTNPlaylistCtrlZ.Name = "BTNPlaylistCtrlZ"
        Me.BTNPlaylistCtrlZ.Size = New System.Drawing.Size(44, 23)
        Me.BTNPlaylistCtrlZ.TabIndex = 202
        Me.BTNPlaylistCtrlZ.Text = "Undo"
        Me.BTNPlaylistCtrlZ.UseVisualStyleBackColor = True
        '
        'RadioPlaylistRegScripts
        '
        Me.RadioPlaylistRegScripts.AutoSize = True
        Me.RadioPlaylistRegScripts.Location = New System.Drawing.Point(228, 372)
        Me.RadioPlaylistRegScripts.Name = "RadioPlaylistRegScripts"
        Me.RadioPlaylistRegScripts.Size = New System.Drawing.Size(127, 17)
        Me.RadioPlaylistRegScripts.TabIndex = 201
        Me.RadioPlaylistRegScripts.Text = "Show Regular Scripts"
        Me.RadioPlaylistRegScripts.UseVisualStyleBackColor = True
        '
        'RadioPlaylistScripts
        '
        Me.RadioPlaylistScripts.AutoSize = True
        Me.RadioPlaylistScripts.Checked = True
        Me.RadioPlaylistScripts.Location = New System.Drawing.Point(62, 372)
        Me.RadioPlaylistScripts.Name = "RadioPlaylistScripts"
        Me.RadioPlaylistScripts.Size = New System.Drawing.Size(122, 17)
        Me.RadioPlaylistScripts.TabIndex = 200
        Me.RadioPlaylistScripts.TabStop = True
        Me.RadioPlaylistScripts.Text = "Show Playlist Scripts"
        Me.RadioPlaylistScripts.UseVisualStyleBackColor = True
        '
        'BTNPlaylistEnd
        '
        Me.BTNPlaylistEnd.BackColor = System.Drawing.Color.LightGray
        Me.BTNPlaylistEnd.Enabled = False
        Me.BTNPlaylistEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNPlaylistEnd.ForeColor = System.Drawing.Color.Black
        Me.BTNPlaylistEnd.Location = New System.Drawing.Point(165, 21)
        Me.BTNPlaylistEnd.Name = "BTNPlaylistEnd"
        Me.BTNPlaylistEnd.Size = New System.Drawing.Size(44, 23)
        Me.BTNPlaylistEnd.TabIndex = 199
        Me.BTNPlaylistEnd.Text = "End"
        Me.BTNPlaylistEnd.UseVisualStyleBackColor = False
        '
        'BTNPlaylistClearAll
        '
        Me.BTNPlaylistClearAll.Enabled = False
        Me.BTNPlaylistClearAll.Location = New System.Drawing.Point(296, 21)
        Me.BTNPlaylistClearAll.Name = "BTNPlaylistClearAll"
        Me.BTNPlaylistClearAll.Size = New System.Drawing.Size(78, 23)
        Me.BTNPlaylistClearAll.TabIndex = 198
        Me.BTNPlaylistClearAll.Text = "Clear All"
        Me.BTNPlaylistClearAll.UseVisualStyleBackColor = True
        '
        'BTNPlaylistSave
        '
        Me.BTNPlaylistSave.Enabled = False
        Me.BTNPlaylistSave.Location = New System.Drawing.Point(621, 369)
        Me.BTNPlaylistSave.Name = "BTNPlaylistSave"
        Me.BTNPlaylistSave.Size = New System.Drawing.Size(44, 23)
        Me.BTNPlaylistSave.TabIndex = 197
        Me.BTNPlaylistSave.Text = "Save"
        Me.BTNPlaylistSave.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(213, 21)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(78, 23)
        Me.Button7.TabIndex = 196
        Me.Button7.Text = "Add Random"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'ScriptPlayList
        '
        Me.ScriptPlayList.Location = New System.Drawing.Point(38, 54)
        Me.ScriptPlayList.MinimumSize = New System.Drawing.Size(20, 20)
        Me.ScriptPlayList.Name = "ScriptPlayList"
        Me.ScriptPlayList.Size = New System.Drawing.Size(336, 292)
        Me.ScriptPlayList.TabIndex = 195
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(410, 27)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(47, 13)
        Me.Label80.TabIndex = 194
        Me.Label80.Text = "Playlist"
        '
        'LBLPlaylIstLink
        '
        Me.LBLPlaylIstLink.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLPlaylIstLink.Enabled = False
        Me.LBLPlaylIstLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPlaylIstLink.Location = New System.Drawing.Point(128, 22)
        Me.LBLPlaylIstLink.Name = "LBLPlaylIstLink"
        Me.LBLPlaylIstLink.Size = New System.Drawing.Size(34, 21)
        Me.LBLPlaylIstLink.TabIndex = 193
        Me.LBLPlaylIstLink.Text = "Link"
        Me.LBLPlaylIstLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLPlaylistModule
        '
        Me.LBLPlaylistModule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLPlaylistModule.Enabled = False
        Me.LBLPlaylistModule.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPlaylistModule.Location = New System.Drawing.Point(76, 22)
        Me.LBLPlaylistModule.Name = "LBLPlaylistModule"
        Me.LBLPlaylistModule.Size = New System.Drawing.Size(50, 21)
        Me.LBLPlaylistModule.TabIndex = 192
        Me.LBLPlaylistModule.Text = "Module"
        Me.LBLPlaylistModule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLPLaylistStart
        '
        Me.LBLPLaylistStart.BackColor = System.Drawing.Color.Green
        Me.LBLPLaylistStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLPLaylistStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPLaylistStart.ForeColor = System.Drawing.Color.White
        Me.LBLPLaylistStart.Location = New System.Drawing.Point(38, 22)
        Me.LBLPLaylistStart.Name = "LBLPLaylistStart"
        Me.LBLPLaylistStart.Size = New System.Drawing.Size(36, 21)
        Me.LBLPLaylistStart.TabIndex = 190
        Me.LBLPLaylistStart.Text = "Start"
        Me.LBLPLaylistStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBPlaylist
        '
        Me.LBPlaylist.AllowDrop = True
        Me.LBPlaylist.FormattingEnabled = True
        Me.LBPlaylist.Location = New System.Drawing.Point(413, 56)
        Me.LBPlaylist.Name = "LBPlaylist"
        Me.LBPlaylist.Size = New System.Drawing.Size(252, 290)
        Me.LBPlaylist.TabIndex = 189
        '
        'TabPage14
        '
        Me.TabPage14.BackColor = System.Drawing.Color.LightGray
        Me.TabPage14.Controls.Add(Me.LBLKeywordPreview)
        Me.TabPage14.Controls.Add(Me.Label88)
        Me.TabPage14.Controls.Add(Me.TBKeywordPreview)
        Me.TabPage14.Controls.Add(Me.Button37)
        Me.TabPage14.Controls.Add(Me.Button50)
        Me.TabPage14.Controls.Add(Me.Button22)
        Me.TabPage14.Controls.Add(Me.TBKeyWords)
        Me.TabPage14.Controls.Add(Me.LBKeyWords)
        Me.TabPage14.Controls.Add(Me.RTBKeyWords)
        Me.TabPage14.Location = New System.Drawing.Point(4, 22)
        Me.TabPage14.Name = "TabPage14"
        Me.TabPage14.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage14.Size = New System.Drawing.Size(958, 424)
        Me.TabPage14.TabIndex = 0
        Me.TabPage14.Text = "Keywords"
        '
        'LBLKeywordPreview
        '
        Me.LBLKeywordPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLKeywordPreview.Location = New System.Drawing.Point(215, 385)
        Me.LBLKeywordPreview.Name = "LBLKeywordPreview"
        Me.LBLKeywordPreview.Size = New System.Drawing.Size(416, 23)
        Me.LBLKeywordPreview.TabIndex = 174
        Me.LBLKeywordPreview.Text = "Get Preview Here"
        Me.LBLKeywordPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label88
        '
        Me.Label88.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label88.Location = New System.Drawing.Point(3, 358)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(194, 53)
        Me.Label88.TabIndex = 173
        Me.Label88.Text = "Preview:  Enter any line with a Keyword and press # to generate a random sentence" &
    " the domme return."
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TBKeywordPreview
        '
        Me.TBKeywordPreview.Location = New System.Drawing.Point(215, 358)
        Me.TBKeywordPreview.Name = "TBKeywordPreview"
        Me.TBKeywordPreview.Size = New System.Drawing.Size(416, 20)
        Me.TBKeywordPreview.TabIndex = 172
        Me.TBKeywordPreview.Text = "Enter Line Here"
        Me.TBKeywordPreview.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button37
        '
        Me.Button37.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button37.Location = New System.Drawing.Point(638, 358)
        Me.Button37.Name = "Button37"
        Me.Button37.Size = New System.Drawing.Size(47, 50)
        Me.Button37.TabIndex = 171
        Me.Button37.Text = "#"
        Me.Button37.UseVisualStyleBackColor = True
        '
        'Button50
        '
        Me.Button50.Location = New System.Drawing.Point(6, 10)
        Me.Button50.Name = "Button50"
        Me.Button50.Size = New System.Drawing.Size(194, 23)
        Me.Button50.TabIndex = 169
        Me.Button50.Text = "Refresh and Clear Keyword List"
        Me.Button50.UseVisualStyleBackColor = True
        '
        'Button22
        '
        Me.Button22.Location = New System.Drawing.Point(638, 10)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(47, 23)
        Me.Button22.TabIndex = 167
        Me.Button22.Text = "Save"
        Me.Button22.UseVisualStyleBackColor = True
        '
        'TBKeyWords
        '
        Me.TBKeyWords.Location = New System.Drawing.Point(215, 10)
        Me.TBKeyWords.Name = "TBKeyWords"
        Me.TBKeyWords.Size = New System.Drawing.Size(416, 20)
        Me.TBKeyWords.TabIndex = 166
        '
        'LBKeyWords
        '
        Me.LBKeyWords.FormattingEnabled = True
        Me.LBKeyWords.Location = New System.Drawing.Point(6, 36)
        Me.LBKeyWords.Name = "LBKeyWords"
        Me.LBKeyWords.Size = New System.Drawing.Size(194, 316)
        Me.LBKeyWords.Sorted = True
        Me.LBKeyWords.TabIndex = 165
        '
        'RTBKeyWords
        '
        Me.RTBKeyWords.Location = New System.Drawing.Point(215, 39)
        Me.RTBKeyWords.Name = "RTBKeyWords"
        Me.RTBKeyWords.Size = New System.Drawing.Size(470, 313)
        Me.RTBKeyWords.TabIndex = 164
        Me.RTBKeyWords.Text = ""
        '
        'TabPage24
        '
        Me.TabPage24.BackColor = System.Drawing.Color.LightGray
        Me.TabPage24.Controls.Add(Me.Button9)
        Me.TabPage24.Controls.Add(Me.RTBResponsesKEY)
        Me.TabPage24.Controls.Add(Me.Button4)
        Me.TabPage24.Controls.Add(Me.Button5)
        Me.TabPage24.Controls.Add(Me.TBResponses)
        Me.TabPage24.Controls.Add(Me.LBResponses)
        Me.TabPage24.Controls.Add(Me.RTBResponses)
        Me.TabPage24.Location = New System.Drawing.Point(4, 22)
        Me.TabPage24.Name = "TabPage24"
        Me.TabPage24.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage24.Size = New System.Drawing.Size(958, 424)
        Me.TabPage24.TabIndex = 3
        Me.TabPage24.Text = "Responses"
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(217, 10)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(215, 23)
        Me.Button9.TabIndex = 176
        Me.Button9.Text = "Response Template"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'RTBResponsesKEY
        '
        Me.RTBResponsesKEY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RTBResponsesKEY.Location = New System.Drawing.Point(217, 36)
        Me.RTBResponsesKEY.Name = "RTBResponsesKEY"
        Me.RTBResponsesKEY.Size = New System.Drawing.Size(468, 40)
        Me.RTBResponsesKEY.TabIndex = 175
        Me.RTBResponsesKEY.Text = ""
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(6, 10)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(194, 23)
        Me.Button4.TabIndex = 174
        Me.Button4.Text = "Refresh and Clear Response List"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(638, 10)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(47, 23)
        Me.Button5.TabIndex = 173
        Me.Button5.Text = "Save"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TBResponses
        '
        Me.TBResponses.Location = New System.Drawing.Point(438, 10)
        Me.TBResponses.Name = "TBResponses"
        Me.TBResponses.Size = New System.Drawing.Size(194, 20)
        Me.TBResponses.TabIndex = 172
        '
        'LBResponses
        '
        Me.LBResponses.FormattingEnabled = True
        Me.LBResponses.Location = New System.Drawing.Point(6, 36)
        Me.LBResponses.Name = "LBResponses"
        Me.LBResponses.Size = New System.Drawing.Size(194, 355)
        Me.LBResponses.Sorted = True
        Me.LBResponses.TabIndex = 171
        '
        'RTBResponses
        '
        Me.RTBResponses.Location = New System.Drawing.Point(217, 82)
        Me.RTBResponses.Name = "RTBResponses"
        Me.RTBResponses.Size = New System.Drawing.Size(468, 309)
        Me.RTBResponses.TabIndex = 170
        Me.RTBResponses.Text = ""
        '
        'TabPage8
        '
        Me.TabPage8.BackColor = System.Drawing.Color.LightGray
        Me.TabPage8.Controls.Add(Me.RTBVideoMod)
        Me.TabPage8.Controls.Add(Me.GroupBox29)
        Me.TabPage8.Controls.Add(Me.BTNVideoModClear)
        Me.TabPage8.Controls.Add(Me.GroupBox28)
        Me.TabPage8.Controls.Add(Me.BTNVideoModLoad)
        Me.TabPage8.Controls.Add(Me.GroupBox30)
        Me.TabPage8.Controls.Add(Me.BTNVideoModSave)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(958, 424)
        Me.TabPage8.TabIndex = 2
        Me.TabPage8.Text = "Video"
        '
        'RTBVideoMod
        '
        Me.RTBVideoMod.Enabled = False
        Me.RTBVideoMod.Location = New System.Drawing.Point(167, 17)
        Me.RTBVideoMod.Name = "RTBVideoMod"
        Me.RTBVideoMod.Size = New System.Drawing.Size(525, 286)
        Me.RTBVideoMod.TabIndex = 150
        Me.RTBVideoMod.Text = ""
        '
        'GroupBox29
        '
        Me.GroupBox29.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox29.Controls.Add(Me.Label51)
        Me.GroupBox29.ForeColor = System.Drawing.Color.Black
        Me.GroupBox29.Location = New System.Drawing.Point(6, 309)
        Me.GroupBox29.Name = "GroupBox29"
        Me.GroupBox29.Size = New System.Drawing.Size(692, 92)
        Me.GroupBox29.TabIndex = 66
        Me.GroupBox29.TabStop = False
        Me.GroupBox29.Text = "Description"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.Black
        Me.Label51.Location = New System.Drawing.Point(6, 16)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(680, 73)
        Me.Label51.TabIndex = 62
        Me.Label51.Text = resources.GetString("Label51.Text")
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNVideoModClear
        '
        Me.BTNVideoModClear.Enabled = False
        Me.BTNVideoModClear.Location = New System.Drawing.Point(6, 227)
        Me.BTNVideoModClear.Name = "BTNVideoModClear"
        Me.BTNVideoModClear.Size = New System.Drawing.Size(155, 35)
        Me.BTNVideoModClear.TabIndex = 153
        Me.BTNVideoModClear.Text = "Clear Text and Select New Video Tease Type/Script"
        Me.BTNVideoModClear.UseVisualStyleBackColor = True
        '
        'GroupBox28
        '
        Me.GroupBox28.Controls.Add(Me.CBVTType)
        Me.GroupBox28.Location = New System.Drawing.Point(6, 8)
        Me.GroupBox28.Name = "GroupBox28"
        Me.GroupBox28.Size = New System.Drawing.Size(155, 46)
        Me.GroupBox28.TabIndex = 148
        Me.GroupBox28.TabStop = False
        Me.GroupBox28.Text = "Video Tease Type"
        '
        'CBVTType
        '
        Me.CBVTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBVTType.FormattingEnabled = True
        Me.CBVTType.Items.AddRange(New Object() {"Avoid The Edge", "Censorship Sucks", "Red Light Green Light"})
        Me.CBVTType.Location = New System.Drawing.Point(9, 15)
        Me.CBVTType.Name = "CBVTType"
        Me.CBVTType.Size = New System.Drawing.Size(137, 21)
        Me.CBVTType.TabIndex = 171
        '
        'BTNVideoModLoad
        '
        Me.BTNVideoModLoad.Location = New System.Drawing.Point(6, 176)
        Me.BTNVideoModLoad.Name = "BTNVideoModLoad"
        Me.BTNVideoModLoad.Size = New System.Drawing.Size(155, 35)
        Me.BTNVideoModLoad.TabIndex = 152
        Me.BTNVideoModLoad.Text = "Load Script"
        Me.BTNVideoModLoad.UseVisualStyleBackColor = True
        '
        'GroupBox30
        '
        Me.GroupBox30.Controls.Add(Me.LBVidScript)
        Me.GroupBox30.Location = New System.Drawing.Point(6, 60)
        Me.GroupBox30.Name = "GroupBox30"
        Me.GroupBox30.Size = New System.Drawing.Size(155, 100)
        Me.GroupBox30.TabIndex = 149
        Me.GroupBox30.TabStop = False
        Me.GroupBox30.Text = "Script"
        '
        'LBVidScript
        '
        Me.LBVidScript.FormattingEnabled = True
        Me.LBVidScript.Location = New System.Drawing.Point(9, 20)
        Me.LBVidScript.Name = "LBVidScript"
        Me.LBVidScript.Size = New System.Drawing.Size(137, 69)
        Me.LBVidScript.TabIndex = 0
        '
        'BTNVideoModSave
        '
        Me.BTNVideoModSave.Enabled = False
        Me.BTNVideoModSave.Location = New System.Drawing.Point(6, 268)
        Me.BTNVideoModSave.Name = "BTNVideoModSave"
        Me.BTNVideoModSave.Size = New System.Drawing.Size(155, 35)
        Me.BTNVideoModSave.TabIndex = 151
        Me.BTNVideoModSave.Text = "Save Changes"
        Me.BTNVideoModSave.UseVisualStyleBackColor = True
        '
        'TabPage15
        '
        Me.TabPage15.BackColor = System.Drawing.Color.LightGray
        Me.TabPage15.Controls.Add(Me.Label62)
        Me.TabPage15.Controls.Add(Me.Label61)
        Me.TabPage15.Controls.Add(Me.Label57)
        Me.TabPage15.Controls.Add(Me.Label58)
        Me.TabPage15.Controls.Add(Me.Label60)
        Me.TabPage15.Controls.Add(Me.TBGlitModFileName)
        Me.TabPage15.Controls.Add(Me.GroupBox34)
        Me.TabPage15.Controls.Add(Me.RTBGlitModDommePost)
        Me.TabPage15.Controls.Add(Me.Button26)
        Me.TabPage15.Controls.Add(Me.Label56)
        Me.TabPage15.Controls.Add(Me.RTBGlitModResponses)
        Me.TabPage15.Controls.Add(Me.LBGlitModScripts)
        Me.TabPage15.Controls.Add(Me.LBLGlitModScriptCount)
        Me.TabPage15.Controls.Add(Me.LBLGlitModDomType)
        Me.TabPage15.Controls.Add(Me.Button29)
        Me.TabPage15.Controls.Add(Me.CBGlitModType)
        Me.TabPage15.Controls.Add(Me.Label59)
        Me.TabPage15.Controls.Add(Me.Label50)
        Me.TabPage15.Location = New System.Drawing.Point(4, 22)
        Me.TabPage15.Name = "TabPage15"
        Me.TabPage15.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage15.Size = New System.Drawing.Size(958, 424)
        Me.TabPage15.TabIndex = 1
        Me.TabPage15.Text = "Glitter"
        '
        'Label62
        '
        Me.Label62.Location = New System.Drawing.Point(255, 169)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(59, 51)
        Me.Label62.TabIndex = 177
        Me.Label62.Text = "@Cruel @Angry @Custom2"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label61
        '
        Me.Label61.Location = New System.Drawing.Point(194, 169)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(59, 51)
        Me.Label61.TabIndex = 176
        Me.Label61.Text = "@Bratty @Caring @Custom1"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label57
        '
        Me.Label57.Location = New System.Drawing.Point(194, 11)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(120, 23)
        Me.Label57.TabIndex = 160
        Me.Label57.Text = "File Name"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label58
        '
        Me.Label58.Location = New System.Drawing.Point(350, 11)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(326, 23)
        Me.Label58.TabIndex = 161
        Me.Label58.Text = "Domme's Post"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label60
        '
        Me.Label60.Location = New System.Drawing.Point(194, 139)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(120, 30)
        Me.Label60.TabIndex = 175
        Me.Label60.Text = "Tease Responses Need 3 of Each:"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TBGlitModFileName
        '
        Me.TBGlitModFileName.Location = New System.Drawing.Point(194, 36)
        Me.TBGlitModFileName.Name = "TBGlitModFileName"
        Me.TBGlitModFileName.Size = New System.Drawing.Size(120, 20)
        Me.TBGlitModFileName.TabIndex = 158
        '
        'GroupBox34
        '
        Me.GroupBox34.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox34.Controls.Add(Me.Label52)
        Me.GroupBox34.ForeColor = System.Drawing.Color.Black
        Me.GroupBox34.Location = New System.Drawing.Point(8, 296)
        Me.GroupBox34.Name = "GroupBox34"
        Me.GroupBox34.Size = New System.Drawing.Size(683, 107)
        Me.GroupBox34.TabIndex = 66
        Me.GroupBox34.TabStop = False
        Me.GroupBox34.Text = "Description"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.Transparent
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(6, 16)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(670, 88)
        Me.Label52.TabIndex = 62
        Me.Label52.Text = resources.GetString("Label52.Text")
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RTBGlitModDommePost
        '
        Me.RTBGlitModDommePost.Location = New System.Drawing.Point(350, 36)
        Me.RTBGlitModDommePost.Name = "RTBGlitModDommePost"
        Me.RTBGlitModDommePost.Size = New System.Drawing.Size(326, 41)
        Me.RTBGlitModDommePost.TabIndex = 162
        Me.RTBGlitModDommePost.Text = ""
        '
        'Button26
        '
        Me.Button26.Location = New System.Drawing.Point(194, 239)
        Me.Button26.Name = "Button26"
        Me.Button26.Size = New System.Drawing.Size(120, 23)
        Me.Button26.TabIndex = 174
        Me.Button26.Text = "Clear Fields"
        Me.Button26.UseVisualStyleBackColor = True
        '
        'Label56
        '
        Me.Label56.Location = New System.Drawing.Point(350, 80)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(324, 23)
        Me.Label56.TabIndex = 156
        Me.Label56.Text = "Responses (Minimum 3)"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RTBGlitModResponses
        '
        Me.RTBGlitModResponses.Location = New System.Drawing.Point(350, 106)
        Me.RTBGlitModResponses.Name = "RTBGlitModResponses"
        Me.RTBGlitModResponses.Size = New System.Drawing.Size(326, 184)
        Me.RTBGlitModResponses.TabIndex = 150
        Me.RTBGlitModResponses.Text = ""
        '
        'LBGlitModScripts
        '
        Me.LBGlitModScripts.FormattingEnabled = True
        Me.LBGlitModScripts.Location = New System.Drawing.Point(27, 106)
        Me.LBGlitModScripts.Name = "LBGlitModScripts"
        Me.LBGlitModScripts.Size = New System.Drawing.Size(136, 186)
        Me.LBGlitModScripts.TabIndex = 163
        '
        'LBLGlitModScriptCount
        '
        Me.LBLGlitModScriptCount.Location = New System.Drawing.Point(27, 80)
        Me.LBLGlitModScriptCount.Name = "LBLGlitModScriptCount"
        Me.LBLGlitModScriptCount.Size = New System.Drawing.Size(136, 23)
        Me.LBLGlitModScriptCount.TabIndex = 173
        Me.LBLGlitModScriptCount.Text = "0 Trivia Glitter Scripts Found"
        Me.LBLGlitModScriptCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLGlitModDomType
        '
        Me.LBLGlitModDomType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGlitModDomType.Location = New System.Drawing.Point(191, 103)
        Me.LBLGlitModDomType.Name = "LBLGlitModDomType"
        Me.LBLGlitModDomType.Size = New System.Drawing.Size(123, 23)
        Me.LBLGlitModDomType.TabIndex = 155
        Me.LBLGlitModDomType.Text = "Total Brat"
        Me.LBLGlitModDomType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button29
        '
        Me.Button29.Location = New System.Drawing.Point(194, 268)
        Me.Button29.Name = "Button29"
        Me.Button29.Size = New System.Drawing.Size(120, 23)
        Me.Button29.TabIndex = 151
        Me.Button29.Text = "Save Glitter File"
        Me.Button29.UseVisualStyleBackColor = True
        '
        'CBGlitModType
        '
        Me.CBGlitModType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBGlitModType.FormattingEnabled = True
        Me.CBGlitModType.Items.AddRange(New Object() {"Tease", "Egotist", "Trivia", "Daily", "Custom 1", "Custom 2"})
        Me.CBGlitModType.Location = New System.Drawing.Point(27, 35)
        Me.CBGlitModType.Name = "CBGlitModType"
        Me.CBGlitModType.Size = New System.Drawing.Size(136, 21)
        Me.CBGlitModType.TabIndex = 171
        '
        'Label59
        '
        Me.Label59.Location = New System.Drawing.Point(33, 11)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(130, 23)
        Me.Label59.TabIndex = 172
        Me.Label59.Text = "Glitter Post Type"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.Location = New System.Drawing.Point(191, 80)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(123, 23)
        Me.Label50.TabIndex = 154
        Me.Label50.Text = "Current Domme Personality:"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage25
        '
        Me.TabPage25.BackColor = System.Drawing.Color.Silver
        Me.TabPage25.Controls.Add(Me.Panel11)
        Me.TabPage25.Location = New System.Drawing.Point(4, 22)
        Me.TabPage25.Name = "TabPage25"
        Me.TabPage25.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage25.Size = New System.Drawing.Size(972, 456)
        Me.TabPage25.TabIndex = 18
        Me.TabPage25.Text = "Misc"
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.LightGray
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Controls.Add(Me.GroupBox62)
        Me.Panel11.Controls.Add(Me.GroupBox33)
        Me.Panel11.Controls.Add(Me.GroupBox27)
        Me.Panel11.Controls.Add(Me.GroupBox20)
        Me.Panel11.Controls.Add(Me.WebToy)
        Me.Panel11.Controls.Add(Me.GroupBox15)
        Me.Panel11.Controls.Add(Me.PictureBox9)
        Me.Panel11.Controls.Add(Me.Label148)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(3, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(966, 450)
        Me.Panel11.TabIndex = 92
        '
        'GroupBox62
        '
        Me.GroupBox62.Controls.Add(Me.RBGerman)
        Me.GroupBox62.Controls.Add(Me.RBEnglish)
        Me.GroupBox62.Location = New System.Drawing.Point(420, 155)
        Me.GroupBox62.Name = "GroupBox62"
        Me.GroupBox62.Size = New System.Drawing.Size(277, 107)
        Me.GroupBox62.TabIndex = 178
        Me.GroupBox62.TabStop = False
        Me.GroupBox62.Text = "Language"
        '
        'RBGerman
        '
        Me.RBGerman.AutoSize = True
        Me.RBGerman.Location = New System.Drawing.Point(180, 20)
        Me.RBGerman.Name = "RBGerman"
        Me.RBGerman.Size = New System.Drawing.Size(65, 17)
        Me.RBGerman.TabIndex = 1
        Me.RBGerman.Text = "Deutsch"
        Me.RBGerman.UseVisualStyleBackColor = True
        '
        'RBEnglish
        '
        Me.RBEnglish.AutoSize = True
        Me.RBEnglish.Checked = True
        Me.RBEnglish.Location = New System.Drawing.Point(36, 19)
        Me.RBEnglish.Name = "RBEnglish"
        Me.RBEnglish.Size = New System.Drawing.Size(59, 17)
        Me.RBEnglish.TabIndex = 0
        Me.RBEnglish.TabStop = True
        Me.RBEnglish.Text = "English"
        Me.RBEnglish.UseVisualStyleBackColor = True
        '
        'GroupBox33
        '
        Me.GroupBox33.Controls.Add(Me.BTNOfflineMode)
        Me.GroupBox33.Controls.Add(Me.LBLOfflineMode)
        Me.GroupBox33.Controls.Add(Me.Label140)
        Me.GroupBox33.Controls.Add(Me.ChastityModeButton)
        Me.GroupBox33.Controls.Add(Me.InChastityLabel)
        Me.GroupBox33.Controls.Add(Me.Label120)
        Me.GroupBox33.Location = New System.Drawing.Point(420, 268)
        Me.GroupBox33.Name = "GroupBox33"
        Me.GroupBox33.Size = New System.Drawing.Size(277, 159)
        Me.GroupBox33.TabIndex = 177
        Me.GroupBox33.TabStop = False
        Me.GroupBox33.Text = "System States"
        '
        'BTNOfflineMode
        '
        Me.BTNOfflineMode.Location = New System.Drawing.Point(161, 70)
        Me.BTNOfflineMode.Name = "BTNOfflineMode"
        Me.BTNOfflineMode.Size = New System.Drawing.Size(99, 23)
        Me.BTNOfflineMode.TabIndex = 180
        Me.BTNOfflineMode.Text = "Toggle"
        Me.BTNOfflineMode.UseVisualStyleBackColor = True
        '
        'LBLOfflineMode
        '
        Me.LBLOfflineMode.BackColor = System.Drawing.Color.LightGray
        Me.LBLOfflineMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLOfflineMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLOfflineMode.ForeColor = System.Drawing.Color.Red
        Me.LBLOfflineMode.Location = New System.Drawing.Point(120, 70)
        Me.LBLOfflineMode.Name = "LBLOfflineMode"
        Me.LBLOfflineMode.Size = New System.Drawing.Size(37, 23)
        Me.LBLOfflineMode.TabIndex = 179
        Me.LBLOfflineMode.Text = "OFF"
        Me.LBLOfflineMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.LightGray
        Me.Label140.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label140.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.Location = New System.Drawing.Point(17, 70)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(98, 23)
        Me.Label140.TabIndex = 178
        Me.Label140.Text = "OFFLINE MODE"
        Me.Label140.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChastityModeButton
        '
        Me.ChastityModeButton.Location = New System.Drawing.Point(161, 33)
        Me.ChastityModeButton.Name = "ChastityModeButton"
        Me.ChastityModeButton.Size = New System.Drawing.Size(99, 23)
        Me.ChastityModeButton.TabIndex = 177
        Me.ChastityModeButton.Text = "Toggle"
        Me.ChastityModeButton.UseVisualStyleBackColor = True
        '
        'InChastityLabel
        '
        Me.InChastityLabel.BackColor = System.Drawing.Color.LightGray
        Me.InChastityLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.InChastityLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InChastityLabel.ForeColor = System.Drawing.Color.Red
        Me.InChastityLabel.Location = New System.Drawing.Point(120, 33)
        Me.InChastityLabel.Name = "InChastityLabel"
        Me.InChastityLabel.Size = New System.Drawing.Size(37, 23)
        Me.InChastityLabel.TabIndex = 3
        Me.InChastityLabel.Text = "OFF"
        Me.InChastityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.LightGray
        Me.Label120.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label120.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label120.Location = New System.Drawing.Point(17, 33)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(98, 23)
        Me.Label120.TabIndex = 2
        Me.Label120.Text = "CHASTITY"
        Me.Label120.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox27
        '
        Me.GroupBox27.Controls.Add(Me.Button6)
        Me.GroupBox27.Controls.Add(Me.LBLSesSpace)
        Me.GroupBox27.Controls.Add(Me.Button3)
        Me.GroupBox27.Controls.Add(Me.LBLSesFiles)
        Me.GroupBox27.Controls.Add(Me.Label125)
        Me.GroupBox27.Controls.Add(Me.Label124)
        Me.GroupBox27.Location = New System.Drawing.Point(420, 32)
        Me.GroupBox27.Name = "GroupBox27"
        Me.GroupBox27.Size = New System.Drawing.Size(279, 117)
        Me.GroupBox27.TabIndex = 176
        Me.GroupBox27.TabStop = False
        Me.GroupBox27.Text = "Session Images"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(143, 76)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(117, 23)
        Me.Button6.TabIndex = 176
        Me.Button6.Text = "Delete All Files"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'LBLSesSpace
        '
        Me.LBLSesSpace.Location = New System.Drawing.Point(149, 53)
        Me.LBLSesSpace.Name = "LBLSesSpace"
        Me.LBLSesSpace.Size = New System.Drawing.Size(124, 13)
        Me.LBLSesSpace.TabIndex = 3
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(20, 76)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(117, 23)
        Me.Button3.TabIndex = 175
        Me.Button3.Text = "Open Folder"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'LBLSesFiles
        '
        Me.LBLSesFiles.Location = New System.Drawing.Point(149, 24)
        Me.LBLSesFiles.Name = "LBLSesFiles"
        Me.LBLSesFiles.Size = New System.Drawing.Size(124, 13)
        Me.LBLSesFiles.TabIndex = 2
        '
        'Label125
        '
        Me.Label125.AutoSize = True
        Me.Label125.Location = New System.Drawing.Point(17, 53)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(120, 13)
        Me.Label125.TabIndex = 1
        Me.Label125.Text = "Total Disk Space Used:"
        '
        'Label124
        '
        Me.Label124.AutoSize = True
        Me.Label124.Location = New System.Drawing.Point(17, 24)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(126, 13)
        Me.Label124.TabIndex = 0
        Me.Label124.Text = "Number of Files in Folder:"
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.Button1)
        Me.GroupBox20.Controls.Add(Me.BTNMaintenanceScripts)
        Me.GroupBox20.Controls.Add(Me.BTNMaintenanceRefresh)
        Me.GroupBox20.Controls.Add(Me.Label117)
        Me.GroupBox20.Controls.Add(Me.Label116)
        Me.GroupBox20.Controls.Add(Me.PBCurrent)
        Me.GroupBox20.Controls.Add(Me.BTNMaintenanceCancel)
        Me.GroupBox20.Controls.Add(Me.PBMaintenance)
        Me.GroupBox20.Controls.Add(Me.LBLMaintenance)
        Me.GroupBox20.Controls.Add(Me.BTNMaintenanceRebuild)
        Me.GroupBox20.Location = New System.Drawing.Point(6, 32)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(408, 230)
        Me.GroupBox20.TabIndex = 174
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "Maintenance"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(270, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 23)
        Me.Button1.TabIndex = 176
        Me.Button1.Text = "Reset Settings"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BTNMaintenanceScripts
        '
        Me.BTNMaintenanceScripts.Location = New System.Drawing.Point(142, 19)
        Me.BTNMaintenanceScripts.Name = "BTNMaintenanceScripts"
        Me.BTNMaintenanceScripts.Size = New System.Drawing.Size(121, 23)
        Me.BTNMaintenanceScripts.TabIndex = 175
        Me.BTNMaintenanceScripts.Text = "Audit Scripts"
        Me.BTNMaintenanceScripts.UseVisualStyleBackColor = True
        '
        'BTNMaintenanceRefresh
        '
        Me.BTNMaintenanceRefresh.Location = New System.Drawing.Point(15, 19)
        Me.BTNMaintenanceRefresh.Name = "BTNMaintenanceRefresh"
        Me.BTNMaintenanceRefresh.Size = New System.Drawing.Size(121, 23)
        Me.BTNMaintenanceRefresh.TabIndex = 7
        Me.BTNMaintenanceRefresh.Text = "Refresh URL Files"
        Me.BTNMaintenanceRefresh.UseVisualStyleBackColor = True
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.Location = New System.Drawing.Point(15, 182)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(84, 13)
        Me.Label117.TabIndex = 6
        Me.Label117.Text = "Overall Progress"
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Location = New System.Drawing.Point(15, 140)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(85, 13)
        Me.Label116.TabIndex = 5
        Me.Label116.Text = "Current Progress"
        '
        'PBCurrent
        '
        Me.PBCurrent.Location = New System.Drawing.Point(15, 156)
        Me.PBCurrent.Name = "PBCurrent"
        Me.PBCurrent.Size = New System.Drawing.Size(376, 23)
        Me.PBCurrent.TabIndex = 4
        '
        'BTNMaintenanceCancel
        '
        Me.BTNMaintenanceCancel.Enabled = False
        Me.BTNMaintenanceCancel.Location = New System.Drawing.Point(270, 48)
        Me.BTNMaintenanceCancel.Name = "BTNMaintenanceCancel"
        Me.BTNMaintenanceCancel.Size = New System.Drawing.Size(121, 23)
        Me.BTNMaintenanceCancel.TabIndex = 3
        Me.BTNMaintenanceCancel.Text = "Cancel"
        Me.BTNMaintenanceCancel.UseVisualStyleBackColor = True
        '
        'PBMaintenance
        '
        Me.PBMaintenance.Location = New System.Drawing.Point(15, 197)
        Me.PBMaintenance.Name = "PBMaintenance"
        Me.PBMaintenance.Size = New System.Drawing.Size(376, 23)
        Me.PBMaintenance.TabIndex = 2
        '
        'LBLMaintenance
        '
        Me.LBLMaintenance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLMaintenance.Location = New System.Drawing.Point(15, 76)
        Me.LBLMaintenance.Name = "LBLMaintenance"
        Me.LBLMaintenance.Size = New System.Drawing.Size(376, 61)
        Me.LBLMaintenance.TabIndex = 1
        Me.LBLMaintenance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNMaintenanceRebuild
        '
        Me.BTNMaintenanceRebuild.Location = New System.Drawing.Point(15, 48)
        Me.BTNMaintenanceRebuild.Name = "BTNMaintenanceRebuild"
        Me.BTNMaintenanceRebuild.Size = New System.Drawing.Size(121, 23)
        Me.BTNMaintenanceRebuild.TabIndex = 0
        Me.BTNMaintenanceRebuild.Text = "Rebuild URL Files"
        Me.BTNMaintenanceRebuild.UseVisualStyleBackColor = True
        '
        'WebToy
        '
        Me.WebToy.Location = New System.Drawing.Point(16, 379)
        Me.WebToy.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebToy.Name = "WebToy"
        Me.WebToy.Size = New System.Drawing.Size(381, 36)
        Me.WebToy.TabIndex = 172
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.Label115)
        Me.GroupBox15.Controls.Add(Me.TBWebStop)
        Me.GroupBox15.Controls.Add(Me.TBWebStart)
        Me.GroupBox15.Controls.Add(Me.Label114)
        Me.GroupBox15.Location = New System.Drawing.Point(6, 268)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(408, 159)
        Me.GroupBox15.TabIndex = 173
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Web-Controlled Sex Toy"
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Location = New System.Drawing.Point(12, 58)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(54, 13)
        Me.Label115.TabIndex = 171
        Me.Label115.Text = "Stop URL"
        '
        'TBWebStop
        '
        Me.TBWebStop.Location = New System.Drawing.Point(10, 72)
        Me.TBWebStop.Name = "TBWebStop"
        Me.TBWebStop.Size = New System.Drawing.Size(381, 20)
        Me.TBWebStop.TabIndex = 170
        '
        'TBWebStart
        '
        Me.TBWebStart.Location = New System.Drawing.Point(10, 33)
        Me.TBWebStart.Name = "TBWebStart"
        Me.TBWebStart.Size = New System.Drawing.Size(381, 20)
        Me.TBWebStart.TabIndex = 167
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.Location = New System.Drawing.Point(12, 17)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(54, 13)
        Me.Label114.TabIndex = 168
        Me.Label114.Text = "Start URL"
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox9.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.PictureBox9.Location = New System.Drawing.Point(9, 6)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(160, 19)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox9.TabIndex = 166
        Me.PictureBox9.TabStop = False
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.Transparent
        Me.Label148.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.ForeColor = System.Drawing.Color.Black
        Me.Label148.Location = New System.Drawing.Point(7, 6)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(692, 21)
        Me.Label148.TabIndex = 48
        Me.Label148.Text = "Miscellaneous Settings"
        Me.Label148.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage28
        '
        Me.TabPage28.BackColor = System.Drawing.Color.Silver
        Me.TabPage28.Controls.Add(Me.TabControl3)
        Me.TabPage28.Location = New System.Drawing.Point(4, 22)
        Me.TabPage28.Name = "TabPage28"
        Me.TabPage28.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage28.Size = New System.Drawing.Size(972, 456)
        Me.TabPage28.TabIndex = 20
        Me.TabPage28.Text = "Debug"
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPage29)
        Me.TabControl3.Controls.Add(Me.TabPage30)
        Me.TabControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl3.Location = New System.Drawing.Point(3, 3)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(966, 450)
        Me.TabControl3.TabIndex = 0
        '
        'TabPage29
        '
        Me.TabPage29.BackColor = System.Drawing.Color.LightGray
        Me.TabPage29.Controls.Add(Me.Label143)
        Me.TabPage29.Controls.Add(Me.LBLDebugScriptTime)
        Me.TabPage29.Controls.Add(Me.BTNDebugHoldEdgeTimer)
        Me.TabPage29.Controls.Add(Me.GroupBox6)
        Me.TabPage29.Controls.Add(Me.GroupBox26)
        Me.TabPage29.Controls.Add(Me.BTNDebugStrokeTauntTimer)
        Me.TabPage29.Controls.Add(Me.LBLDebugHoldEdgeTime)
        Me.TabPage29.Controls.Add(Me.Label145)
        Me.TabPage29.Controls.Add(Me.BTNDebugStrokeTime)
        Me.TabPage29.Controls.Add(Me.BTNDebugEdgeTauntTimer)
        Me.TabPage29.Controls.Add(Me.LBLDebugTeaseTime)
        Me.TabPage29.Controls.Add(Me.LBLDebugStrokeTime)
        Me.TabPage29.Controls.Add(Me.LBLDebugEdgeTauntTime)
        Me.TabPage29.Controls.Add(Me.BTNDebugTeaseTimer)
        Me.TabPage29.Controls.Add(Me.Label142)
        Me.TabPage29.Controls.Add(Me.Label150)
        Me.TabPage29.Controls.Add(Me.Label152)
        Me.TabPage29.Controls.Add(Me.LBLDebugStrokeTauntTime)
        Me.TabPage29.Controls.Add(Me.Label147)
        Me.TabPage29.Location = New System.Drawing.Point(4, 22)
        Me.TabPage29.Name = "TabPage29"
        Me.TabPage29.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage29.Size = New System.Drawing.Size(958, 424)
        Me.TabPage29.TabIndex = 0
        Me.TabPage29.Text = "TabPage29"
        '
        'Label143
        '
        Me.Label143.Location = New System.Drawing.Point(402, 46)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(67, 23)
        Me.Label143.TabIndex = 15
        Me.Label143.Text = "Script Timer"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLDebugScriptTime
        '
        Me.LBLDebugScriptTime.BackColor = System.Drawing.Color.Gainsboro
        Me.LBLDebugScriptTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDebugScriptTime.Location = New System.Drawing.Point(475, 46)
        Me.LBLDebugScriptTime.Name = "LBLDebugScriptTime"
        Me.LBLDebugScriptTime.Size = New System.Drawing.Size(100, 23)
        Me.LBLDebugScriptTime.TabIndex = 16
        Me.LBLDebugScriptTime.Text = "0"
        Me.LBLDebugScriptTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNDebugHoldEdgeTimer
        '
        Me.BTNDebugHoldEdgeTimer.Location = New System.Drawing.Point(581, 248)
        Me.BTNDebugHoldEdgeTimer.Name = "BTNDebugHoldEdgeTimer"
        Me.BTNDebugHoldEdgeTimer.Size = New System.Drawing.Size(75, 23)
        Me.BTNDebugHoldEdgeTimer.TabIndex = 14
        Me.BTNDebugHoldEdgeTimer.Text = "Set to 5"
        Me.BTNDebugHoldEdgeTimer.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label4)
        Me.GroupBox6.Controls.Add(Me.LBLAvgEdgeStroking)
        Me.GroupBox6.Controls.Add(Me.LBLStrokeTimeTotal)
        Me.GroupBox6.Controls.Add(Me.Label94)
        Me.GroupBox6.Controls.Add(Me.LBLLastRuined)
        Me.GroupBox6.Controls.Add(Me.Label65)
        Me.GroupBox6.Controls.Add(Me.LBLAvgEdgeNoTouch)
        Me.GroupBox6.Controls.Add(Me.LBLLastOrgasm)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.Label1)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 189)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(283, 102)
        Me.GroupBox6.TabIndex = 156
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Performance"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 17)
        Me.Label4.TabIndex = 147
        Me.Label4.Text = "Stroking Time"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLAvgEdgeStroking
        '
        Me.LBLAvgEdgeStroking.AutoSize = True
        Me.LBLAvgEdgeStroking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLAvgEdgeStroking.Location = New System.Drawing.Point(113, 68)
        Me.LBLAvgEdgeStroking.Name = "LBLAvgEdgeStroking"
        Me.LBLAvgEdgeStroking.Size = New System.Drawing.Size(36, 15)
        Me.LBLAvgEdgeStroking.TabIndex = 144
        Me.LBLAvgEdgeStroking.Text = "00:00"
        Me.LBLAvgEdgeStroking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLStrokeTimeTotal
        '
        Me.LBLStrokeTimeTotal.Location = New System.Drawing.Point(26, 33)
        Me.LBLStrokeTimeTotal.Name = "LBLStrokeTimeTotal"
        Me.LBLStrokeTimeTotal.Size = New System.Drawing.Size(77, 17)
        Me.LBLStrokeTimeTotal.TabIndex = 148
        Me.LBLStrokeTimeTotal.Text = "0000:00:00:00"
        Me.LBLStrokeTimeTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.Transparent
        Me.Label94.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.ForeColor = System.Drawing.Color.Black
        Me.Label94.Location = New System.Drawing.Point(189, 16)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(65, 17)
        Me.Label94.TabIndex = 150
        Me.Label94.Text = "Last Ruined"
        Me.Label94.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLLastRuined
        '
        Me.LBLLastRuined.Location = New System.Drawing.Point(184, 31)
        Me.LBLLastRuined.Name = "LBLLastRuined"
        Me.LBLLastRuined.Size = New System.Drawing.Size(75, 17)
        Me.LBLLastRuined.TabIndex = 152
        Me.LBLLastRuined.Text = "04/28/2015"
        Me.LBLLastRuined.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.Transparent
        Me.Label65.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.Color.Black
        Me.Label65.Location = New System.Drawing.Point(103, 16)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(85, 17)
        Me.Label65.TabIndex = 149
        Me.Label65.Text = "Last Orgasm"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLAvgEdgeNoTouch
        '
        Me.LBLAvgEdgeNoTouch.AutoSize = True
        Me.LBLAvgEdgeNoTouch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLAvgEdgeNoTouch.Location = New System.Drawing.Point(215, 68)
        Me.LBLAvgEdgeNoTouch.Name = "LBLAvgEdgeNoTouch"
        Me.LBLAvgEdgeNoTouch.Size = New System.Drawing.Size(36, 15)
        Me.LBLAvgEdgeNoTouch.TabIndex = 146
        Me.LBLAvgEdgeNoTouch.Text = "00:00"
        Me.LBLAvgEdgeNoTouch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLLastOrgasm
        '
        Me.LBLLastOrgasm.Location = New System.Drawing.Point(107, 31)
        Me.LBLLastOrgasm.Name = "LBLLastOrgasm"
        Me.LBLLastOrgasm.Size = New System.Drawing.Size(75, 17)
        Me.LBLLastOrgasm.TabIndex = 151
        Me.LBLLastOrgasm.Text = "04/28/2015"
        Me.LBLLastOrgasm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(25, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(238, 17)
        Me.Label14.TabIndex = 138
        Me.Label14.Text = "Average Time to Edge"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(177, 68)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 13)
        Me.Label13.TabIndex = 145
        Me.Label13.Text = "Rest:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 143
        Me.Label1.Text = "While Stroking:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox26
        '
        Me.GroupBox26.Controls.Add(Me.LBLCycleDebugCountdown)
        Me.GroupBox26.Controls.Add(Me.Button19)
        Me.GroupBox26.Controls.Add(Me.BTNDebugTauntsClear)
        Me.GroupBox26.Controls.Add(Me.TBDebugTaunts3)
        Me.GroupBox26.Controls.Add(Me.TBDebugTaunts2)
        Me.GroupBox26.Controls.Add(Me.TBDebugTaunts1)
        Me.GroupBox26.Controls.Add(Me.RBDebugTaunts3)
        Me.GroupBox26.Controls.Add(Me.RBDebugTaunts2)
        Me.GroupBox26.Controls.Add(Me.RBDebugTaunts1)
        Me.GroupBox26.Controls.Add(Me.CBDebugTauntsEndless)
        Me.GroupBox26.Controls.Add(Me.CBDebugTaunts)
        Me.GroupBox26.Location = New System.Drawing.Point(6, 5)
        Me.GroupBox26.Name = "GroupBox26"
        Me.GroupBox26.Size = New System.Drawing.Size(346, 178)
        Me.GroupBox26.TabIndex = 0
        Me.GroupBox26.TabStop = False
        Me.GroupBox26.Text = "Taunt Cycle"
        '
        'LBLCycleDebugCountdown
        '
        Me.LBLCycleDebugCountdown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LBLCycleDebugCountdown.Location = New System.Drawing.Point(100, 146)
        Me.LBLCycleDebugCountdown.Name = "LBLCycleDebugCountdown"
        Me.LBLCycleDebugCountdown.Size = New System.Drawing.Size(81, 23)
        Me.LBLCycleDebugCountdown.TabIndex = 10
        Me.LBLCycleDebugCountdown.Text = "0"
        Me.LBLCycleDebugCountdown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button19
        '
        Me.Button19.Location = New System.Drawing.Point(191, 142)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(146, 30)
        Me.Button19.TabIndex = 9
        Me.Button19.Text = "Countdown to 5 Seconds"
        Me.Button19.UseVisualStyleBackColor = True
        '
        'BTNDebugTauntsClear
        '
        Me.BTNDebugTauntsClear.Location = New System.Drawing.Point(194, 28)
        Me.BTNDebugTauntsClear.Name = "BTNDebugTauntsClear"
        Me.BTNDebugTauntsClear.Size = New System.Drawing.Size(146, 30)
        Me.BTNDebugTauntsClear.TabIndex = 8
        Me.BTNDebugTauntsClear.Text = "Clear"
        Me.BTNDebugTauntsClear.UseVisualStyleBackColor = True
        '
        'TBDebugTaunts3
        '
        Me.TBDebugTaunts3.Location = New System.Drawing.Point(6, 116)
        Me.TBDebugTaunts3.Name = "TBDebugTaunts3"
        Me.TBDebugTaunts3.Size = New System.Drawing.Size(331, 20)
        Me.TBDebugTaunts3.TabIndex = 7
        '
        'TBDebugTaunts2
        '
        Me.TBDebugTaunts2.Location = New System.Drawing.Point(6, 90)
        Me.TBDebugTaunts2.Name = "TBDebugTaunts2"
        Me.TBDebugTaunts2.Size = New System.Drawing.Size(331, 20)
        Me.TBDebugTaunts2.TabIndex = 6
        '
        'TBDebugTaunts1
        '
        Me.TBDebugTaunts1.Location = New System.Drawing.Point(6, 64)
        Me.TBDebugTaunts1.Name = "TBDebugTaunts1"
        Me.TBDebugTaunts1.Size = New System.Drawing.Size(331, 20)
        Me.TBDebugTaunts1.TabIndex = 5
        '
        'RBDebugTaunts3
        '
        Me.RBDebugTaunts3.AutoSize = True
        Me.RBDebugTaunts3.Location = New System.Drawing.Point(127, 41)
        Me.RBDebugTaunts3.Name = "RBDebugTaunts3"
        Me.RBDebugTaunts3.Size = New System.Drawing.Size(59, 17)
        Me.RBDebugTaunts3.TabIndex = 4
        Me.RBDebugTaunts3.Text = "3 Lines"
        Me.RBDebugTaunts3.UseVisualStyleBackColor = True
        '
        'RBDebugTaunts2
        '
        Me.RBDebugTaunts2.AutoSize = True
        Me.RBDebugTaunts2.Location = New System.Drawing.Point(66, 41)
        Me.RBDebugTaunts2.Name = "RBDebugTaunts2"
        Me.RBDebugTaunts2.Size = New System.Drawing.Size(59, 17)
        Me.RBDebugTaunts2.TabIndex = 3
        Me.RBDebugTaunts2.Text = "2 Lines"
        Me.RBDebugTaunts2.UseVisualStyleBackColor = True
        '
        'RBDebugTaunts1
        '
        Me.RBDebugTaunts1.AutoSize = True
        Me.RBDebugTaunts1.Checked = True
        Me.RBDebugTaunts1.Location = New System.Drawing.Point(7, 41)
        Me.RBDebugTaunts1.Name = "RBDebugTaunts1"
        Me.RBDebugTaunts1.Size = New System.Drawing.Size(54, 17)
        Me.RBDebugTaunts1.TabIndex = 2
        Me.RBDebugTaunts1.TabStop = True
        Me.RBDebugTaunts1.Text = "1 Line"
        Me.RBDebugTaunts1.UseVisualStyleBackColor = True
        '
        'CBDebugTauntsEndless
        '
        Me.CBDebugTauntsEndless.AutoSize = True
        Me.CBDebugTauntsEndless.Location = New System.Drawing.Point(7, 150)
        Me.CBDebugTauntsEndless.Name = "CBDebugTauntsEndless"
        Me.CBDebugTauntsEndless.Size = New System.Drawing.Size(92, 17)
        Me.CBDebugTauntsEndless.TabIndex = 1
        Me.CBDebugTauntsEndless.Text = "Endless Cycle"
        Me.CBDebugTauntsEndless.UseVisualStyleBackColor = True
        '
        'CBDebugTaunts
        '
        Me.CBDebugTaunts.AutoSize = True
        Me.CBDebugTaunts.Location = New System.Drawing.Point(7, 20)
        Me.CBDebugTaunts.Name = "CBDebugTaunts"
        Me.CBDebugTaunts.Size = New System.Drawing.Size(174, 17)
        Me.CBDebugTaunts.TabIndex = 0
        Me.CBDebugTaunts.Text = "Enable Taunt Cycle Debugging"
        Me.CBDebugTaunts.UseVisualStyleBackColor = True
        '
        'BTNDebugStrokeTauntTimer
        '
        Me.BTNDebugStrokeTauntTimer.Location = New System.Drawing.Point(581, 150)
        Me.BTNDebugStrokeTauntTimer.Name = "BTNDebugStrokeTauntTimer"
        Me.BTNDebugStrokeTauntTimer.Size = New System.Drawing.Size(75, 23)
        Me.BTNDebugStrokeTauntTimer.TabIndex = 8
        Me.BTNDebugStrokeTauntTimer.Text = "Set to 5"
        Me.BTNDebugStrokeTauntTimer.UseVisualStyleBackColor = True
        '
        'LBLDebugHoldEdgeTime
        '
        Me.LBLDebugHoldEdgeTime.BackColor = System.Drawing.Color.Gainsboro
        Me.LBLDebugHoldEdgeTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDebugHoldEdgeTime.Location = New System.Drawing.Point(475, 248)
        Me.LBLDebugHoldEdgeTime.Name = "LBLDebugHoldEdgeTime"
        Me.LBLDebugHoldEdgeTime.Size = New System.Drawing.Size(100, 23)
        Me.LBLDebugHoldEdgeTime.TabIndex = 13
        Me.LBLDebugHoldEdgeTime.Text = "0"
        Me.LBLDebugHoldEdgeTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label145
        '
        Me.Label145.Location = New System.Drawing.Point(402, 11)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(67, 23)
        Me.Label145.TabIndex = 3
        Me.Label145.Text = "Tease Timer"
        Me.Label145.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNDebugStrokeTime
        '
        Me.BTNDebugStrokeTime.Location = New System.Drawing.Point(581, 121)
        Me.BTNDebugStrokeTime.Name = "BTNDebugStrokeTime"
        Me.BTNDebugStrokeTime.Size = New System.Drawing.Size(75, 23)
        Me.BTNDebugStrokeTime.TabIndex = 2
        Me.BTNDebugStrokeTime.Text = "Set to 5"
        Me.BTNDebugStrokeTime.UseVisualStyleBackColor = True
        '
        'BTNDebugEdgeTauntTimer
        '
        Me.BTNDebugEdgeTauntTimer.Location = New System.Drawing.Point(581, 209)
        Me.BTNDebugEdgeTauntTimer.Name = "BTNDebugEdgeTauntTimer"
        Me.BTNDebugEdgeTauntTimer.Size = New System.Drawing.Size(75, 23)
        Me.BTNDebugEdgeTauntTimer.TabIndex = 11
        Me.BTNDebugEdgeTauntTimer.Text = "Set to 5"
        Me.BTNDebugEdgeTauntTimer.UseVisualStyleBackColor = True
        '
        'LBLDebugTeaseTime
        '
        Me.LBLDebugTeaseTime.BackColor = System.Drawing.Color.Gainsboro
        Me.LBLDebugTeaseTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDebugTeaseTime.Location = New System.Drawing.Point(475, 11)
        Me.LBLDebugTeaseTime.Name = "LBLDebugTeaseTime"
        Me.LBLDebugTeaseTime.Size = New System.Drawing.Size(100, 23)
        Me.LBLDebugTeaseTime.TabIndex = 4
        Me.LBLDebugTeaseTime.Text = "0"
        Me.LBLDebugTeaseTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLDebugStrokeTime
        '
        Me.LBLDebugStrokeTime.BackColor = System.Drawing.Color.Gainsboro
        Me.LBLDebugStrokeTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDebugStrokeTime.Location = New System.Drawing.Point(475, 121)
        Me.LBLDebugStrokeTime.Name = "LBLDebugStrokeTime"
        Me.LBLDebugStrokeTime.Size = New System.Drawing.Size(100, 23)
        Me.LBLDebugStrokeTime.TabIndex = 1
        Me.LBLDebugStrokeTime.Text = "0"
        Me.LBLDebugStrokeTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLDebugEdgeTauntTime
        '
        Me.LBLDebugEdgeTauntTime.BackColor = System.Drawing.Color.Gainsboro
        Me.LBLDebugEdgeTauntTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDebugEdgeTauntTime.Location = New System.Drawing.Point(475, 209)
        Me.LBLDebugEdgeTauntTime.Name = "LBLDebugEdgeTauntTime"
        Me.LBLDebugEdgeTauntTime.Size = New System.Drawing.Size(100, 23)
        Me.LBLDebugEdgeTauntTime.TabIndex = 10
        Me.LBLDebugEdgeTauntTime.Text = "0"
        Me.LBLDebugEdgeTauntTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTNDebugTeaseTimer
        '
        Me.BTNDebugTeaseTimer.Location = New System.Drawing.Point(581, 11)
        Me.BTNDebugTeaseTimer.Name = "BTNDebugTeaseTimer"
        Me.BTNDebugTeaseTimer.Size = New System.Drawing.Size(75, 23)
        Me.BTNDebugTeaseTimer.TabIndex = 5
        Me.BTNDebugTeaseTimer.Text = "Set to 5"
        Me.BTNDebugTeaseTimer.UseVisualStyleBackColor = True
        '
        'Label142
        '
        Me.Label142.Location = New System.Drawing.Point(402, 121)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(67, 23)
        Me.Label142.TabIndex = 0
        Me.Label142.Text = "Stroke Timer"
        Me.Label142.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label150
        '
        Me.Label150.Location = New System.Drawing.Point(402, 206)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(67, 27)
        Me.Label150.TabIndex = 9
        Me.Label150.Text = "Edge Taunt Timer"
        Me.Label150.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label152
        '
        Me.Label152.Location = New System.Drawing.Point(402, 238)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(67, 40)
        Me.Label152.TabIndex = 12
        Me.Label152.Text = "Hold Edge Timer"
        Me.Label152.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LBLDebugStrokeTauntTime
        '
        Me.LBLDebugStrokeTauntTime.BackColor = System.Drawing.Color.Gainsboro
        Me.LBLDebugStrokeTauntTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBLDebugStrokeTauntTime.Location = New System.Drawing.Point(475, 150)
        Me.LBLDebugStrokeTauntTime.Name = "LBLDebugStrokeTauntTime"
        Me.LBLDebugStrokeTauntTime.Size = New System.Drawing.Size(100, 23)
        Me.LBLDebugStrokeTauntTime.TabIndex = 7
        Me.LBLDebugStrokeTauntTime.Text = "0"
        Me.LBLDebugStrokeTauntTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label147
        '
        Me.Label147.Location = New System.Drawing.Point(402, 143)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(67, 29)
        Me.Label147.TabIndex = 6
        Me.Label147.Text = "Stroke Taunt Timer"
        Me.Label147.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage30
        '
        Me.TabPage30.BackColor = System.Drawing.Color.LightGray
        Me.TabPage30.Controls.Add(Me.Button33)
        Me.TabPage30.Controls.Add(Me.Button24)
        Me.TabPage30.Location = New System.Drawing.Point(4, 22)
        Me.TabPage30.Name = "TabPage30"
        Me.TabPage30.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage30.Size = New System.Drawing.Size(958, 424)
        Me.TabPage30.TabIndex = 1
        Me.TabPage30.Text = "TabPage30"
        '
        'Button33
        '
        Me.Button33.Location = New System.Drawing.Point(6, 35)
        Me.Button33.Name = "Button33"
        Me.Button33.Size = New System.Drawing.Size(75, 23)
        Me.Button33.TabIndex = 1
        Me.Button33.Text = "LoadState"
        Me.Button33.UseVisualStyleBackColor = True
        '
        'Button24
        '
        Me.Button24.Location = New System.Drawing.Point(6, 6)
        Me.Button24.Name = "Button24"
        Me.Button24.Size = New System.Drawing.Size(75, 23)
        Me.Button24.TabIndex = 0
        Me.Button24.Text = "SaveState"
        Me.Button24.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.Silver
        Me.TabPage5.Controls.Add(Me.Panel5)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(972, 456)
        Me.TabPage5.TabIndex = 17
        Me.TabPage5.Text = "About"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.LightGray
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label130)
        Me.Panel5.Controls.Add(Me.Label123)
        Me.Panel5.Controls.Add(Me.Label69)
        Me.Panel5.Controls.Add(Me.Label113)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label35)
        Me.Panel5.Controls.Add(Me.Label33)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.PictureBox3)
        Me.Panel5.Controls.Add(Me.Label41)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(972, 456)
        Me.Panel5.TabIndex = 92
        '
        'Label130
        '
        Me.Label130.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label130.Location = New System.Drawing.Point(481, 372)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(254, 54)
        Me.Label130.TabIndex = 176
        Me.Label130.Text = "q55x8x" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Stefaf" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "OxiKlein" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label130.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label123
        '
        Me.Label123.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.Location = New System.Drawing.Point(201, 372)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(254, 54)
        Me.Label123.TabIndex = 175
        Me.Label123.Text = "pepsifreak" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Daragorn" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ambossli"
        Me.Label123.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label69
        '
        Me.Label69.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(155, 353)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(638, 22)
        Me.Label69.TabIndex = 174
        Me.Label69.Text = "Triple Alfa"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.Location = New System.Drawing.Point(124, 475)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(452, 13)
        Me.Label113.TabIndex = 173
        Me.Label113.Text = "All content contained in or viewed through this program are property of their res" &
    "pective owners."
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(155, 331)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(638, 24)
        Me.Label40.TabIndex = 171
        Me.Label40.Text = "Special Thanks"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(152, 165)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(641, 77)
        Me.Label35.TabIndex = 170
        Me.Label35.Text = "This program is freeware. It may be freely distributed." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Do not package or dist" &
    "ribute this program with any scripts or additional content." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please distribute a" &
    "dditional files separately."
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(152, 249)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(641, 77)
        Me.Label33.TabIndex = 169
        Me.Label33.Text = resources.GetString("Label33.Text")
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(642, 136)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(93, 13)
        Me.Label17.TabIndex = 168
        Me.Label17.Text = "Designed by 1885"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(609, 475)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(215, 13)
        Me.Label3.TabIndex = 167
        Me.Label3.Text = " Tease AI © 2015 1885 All Rights Reserved"
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox3.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_big
        Me.PictureBox3.Location = New System.Drawing.Point(204, 75)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(531, 58)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 166
        Me.PictureBox3.TabStop = False
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(155, 430)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(638, 39)
        Me.Label41.TabIndex = 172
        Me.Label41.Text = "Thank you to everyone who has provided help and feedback." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Thank you to the commu" &
    "nity who's been supportive of my work over the years. Tease AI exists because of" &
    " you." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" &
    "s (*.*)|*.*"
        Me.OpenFileDialog1.Title = "Select an image file"
        '
        'GetColor
        '
        Me.GetColor.Color = System.Drawing.Color.SteelBlue
        '
        'WebImageFileDialog
        '
        Me.WebImageFileDialog.Filter = "TXT Files (*.txt)|*.txt"
        Me.WebImageFileDialog.Title = "Please select a URL File"
        '
        'OpenScriptDialog
        '
        Me.OpenScriptDialog.Filter = "TXT Files (*.txt)|*.txt"
        Me.OpenScriptDialog.Title = "Please select a script"
        '
        'OpenSettingsDialog
        '
        Me.OpenSettingsDialog.Filter = "TXT Files (*.txt)|*.txt"
        Me.OpenSettingsDialog.Title = "Please select a settings file to open"
        '
        'SaveSettingsDialog
        '
        Me.SaveSettingsDialog.Filter = "TXT Files (*.txt)|*.txt"
        Me.SaveSettingsDialog.Title = "Select a location to save current Domme settings"
        '
        'TxbImgUrlHardcore
        '
        Me.TxbImgUrlHardcore.BackColor = System.Drawing.Color.LightGray
        Me.TxbImgUrlHardcore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbImgUrlHardcore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxbImgUrlHardcore.Location = New System.Drawing.Point(116, 6)
        Me.TxbImgUrlHardcore.Name = "TxbImgUrlHardcore"
        Me.TxbImgUrlHardcore.ReadOnly = True
        Me.TxbImgUrlHardcore.Size = New System.Drawing.Size(189, 20)
        Me.TxbImgUrlHardcore.TabIndex = 145
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.LightGray
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox2.Location = New System.Drawing.Point(116, 34)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(189, 20)
        Me.TextBox2.TabIndex = 145
        '
        'SettingsHeader
        '
        Me.SettingsHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.SettingsHeader.Location = New System.Drawing.Point(0, 0)
        Me.SettingsHeader.Name = "SettingsHeader"
        Me.SettingsHeader.SettingsTitle = "Header Text"
        Me.SettingsHeader.Size = New System.Drawing.Size(980, 60)
        Me.SettingsHeader.TabIndex = 0
        '
        'SettingsDescriptionControl
        '
        Me.SettingsDescriptionControl.BackColor = System.Drawing.Color.Transparent
        Me.SettingsDescriptionControl.DescriptionText = "Hover over any setting in the menu for a more detailed description of its functio" &
    "n."
        Me.SettingsDescriptionControl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SettingsDescriptionControl.Location = New System.Drawing.Point(0, 542)
        Me.SettingsDescriptionControl.Name = "SettingsDescriptionControl"
        Me.SettingsDescriptionControl.Size = New System.Drawing.Size(980, 165)
        Me.SettingsDescriptionControl.TabIndex = 2
        '
        'BWURLFiles
        '
        Me.BWURLFiles.DislikeListPath = "Images\System\DislikedImageURLs.txt"
        Me.BWURLFiles.ImageURLFileDir = "Images\System\URL Files\"
        Me.BWURLFiles.LikeListPath = "Images\System\LikedImageURLs.txt"
        Me.BWURLFiles.WorkerReportsProgress = True
        Me.BWURLFiles.WorkerSupportsCancellation = True
        '
        'FrmSettings
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 707)
        Me.Controls.Add(Me.SettingsTabs)
        Me.Controls.Add(Me.SettingsHeader)
        Me.Controls.Add(Me.SettingsDescriptionControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1000, 750)
        Me.Name = "FrmSettings"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tease AI Settings"
        Me.SettingsTabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.PNLGeneralSettings.ResumeLayout(False)
        Me.PNLGeneralSettings.PerformLayout
        Me.GroupBox64.ResumeLayout(False)
        Me.GroupBox64.PerformLayout
        Me.GBDommeImages.ResumeLayout(False)
        Me.GBDommeImages.PerformLayout
        CType(Me.SlideShowNumBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBGeneralTextToSpeech.ResumeLayout(False)
        Me.GBGeneralTextToSpeech.PerformLayout
        CType(Me.SliderVRate, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SliderVVolume, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBSafeword.ResumeLayout(False)
        Me.GBSafeword.PerformLayout
        Me.GBGeneralSystem.ResumeLayout(False)
        Me.GBGeneralSystem.PerformLayout
        Me.GBGeneralImages.ResumeLayout(False)
        Me.GBGeneralImages.PerformLayout
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBGeneralSettings.ResumeLayout(False)
        Me.GBGeneralSettings.PerformLayout
        Me.GBSubFont.ResumeLayout(False)
        CType(Me.NBFontSize, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBDommeFont.ResumeLayout(False)
        CType(Me.NBFontSizeD, System.ComponentModel.ISupportInitialize).EndInit
        Me.DommeSettingsTabPage.ResumeLayout(False)
        Me.DommeSettingsBodyPanel.ResumeLayout(False)
        Me.GroupBox39.ResumeLayout(False)
        Me.GroupBox39.PerformLayout
        Me.DommeStatsGroupBox.ResumeLayout(False)
        Me.DommeStatsGroupBox.PerformLayout
        CType(Me.NBEmpathy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBDomBirthdayDay, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DomAgeNumberBox, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBDomBirthdayMonth, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DominationLevel, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBDomPetNames.ResumeLayout(False)
        Me.GBDomPetNames.PerformLayout
        Me.GBDomOrgasms.ResumeLayout(False)
        Me.GBDomOrgasms.PerformLayout
        CType(Me.OrgasmsPerNumBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBDomPersonality.ResumeLayout(False)
        Me.GBDomPersonality.PerformLayout
        Me.GBDomRanges.ResumeLayout(False)
        CType(Me.NBDomMoodMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBDomMoodMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBSubAgeMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBSubAgeMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBSelfAgeMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBSelfAgeMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBAvgCockMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBAvgCockMin, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBDomTypingStyle.ResumeLayout(False)
        Me.GBDomTypingStyle.PerformLayout
        CType(Me.NBTypoChance, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox63.ResumeLayout(False)
        Me.GroupBox63.PerformLayout
        Me.DommeSettingsHeaderPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DommeSettingsLogo, System.ComponentModel.ISupportInitialize).EndInit
        Me.DommeSettingsDescriptionGroupBox.ResumeLayout(False)
        Me.TabPage10.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox22.ResumeLayout(False)
        CType(Me.NBWritingTaskMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBWritingTaskMin, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox45.ResumeLayout(False)
        CType(Me.CockAndBallTortureLevelSlider, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox35.ResumeLayout(False)
        Me.GroupBox38.ResumeLayout(False)
        Me.GroupBox38.PerformLayout
        Me.GroupBox37.ResumeLayout(False)
        Me.GroupBox37.PerformLayout
        Me.GroupBox36.ResumeLayout(False)
        Me.GroupBox36.PerformLayout
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout
        CType(Me.ExtremeEdgeHoldMinimum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ExtremeEdgeHoldMaximum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LongEdgeHoldMinimum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LongEdgeHoldMaximum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBLongEdge, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.HoldEdgeMinimum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.HoldEdgeMaximum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox32.ResumeLayout(False)
        Me.GroupBox32.PerformLayout
        CType(Me.NBBirthdayDay, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.subAgeNumBox, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBBirthdayMonth, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CockSizeNumBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage16.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.ScriptNavPanel.ResumeLayout(False)
        Me.TCScripts.ResumeLayout(False)
        Me.ScriptsStartTab.ResumeLayout(False)
        Me.ScriptsModuleTab.ResumeLayout(False)
        Me.ScriptsLinkTab.ResumeLayout(False)
        Me.ScriptsEndTab.ResumeLayout(False)
        Me.ScriptInfoPanel.ResumeLayout(False)
        Me.ScriptsDescriptionGroup.ResumeLayout(False)
        Me.ScriptsRequirementsGroup.ResumeLayout(False)
        Me.GroupBox43.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.GernreImagesTab.ResumeLayout(False)
        Me.TpImagesUrlFiles.ResumeLayout(False)
        Me.TpImagesUrlFiles.PerformLayout
        Me.GroupBox66.ResumeLayout(False)
        CType(Me.PBURLPreview, System.ComponentModel.ISupportInitialize).EndInit
        Me.TpImagesGenre.ResumeLayout(False)
        Me.GrbImageUrlFiles.ResumeLayout(False)
        Me.TlpImageUrls.ResumeLayout(False)
        Me.TlpImageUrls.PerformLayout
        Me.GbxImagesGenre.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout
        Me.TabPage33.ResumeLayout(False)
        Me.LocalTagsTab.ResumeLayout(False)
        Me.TabPage34.ResumeLayout(False)
        Me.TabPage34.PerformLayout
        CType(Me.ImageTagPictureBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.FileDropDownLabel.ResumeLayout(False)
        Me.FileDropDownLabel.PerformLayout
        Me.LocalTagImageNavGroup.ResumeLayout(False)
        Me.GroupBox55.ResumeLayout(False)
        Me.GroupBox55.PerformLayout
        Me.GroupBox53.ResumeLayout(False)
        Me.GroupBox53.PerformLayout
        Me.GroupBox49.ResumeLayout(False)
        Me.GroupBox49.PerformLayout
        Me.GroupBox46.ResumeLayout(False)
        Me.GroupBox46.PerformLayout
        Me.GroupBox54.ResumeLayout(False)
        Me.GroupBox54.PerformLayout
        Me.BdsmTagGroup.ResumeLayout(False)
        Me.BdsmTagGroup.PerformLayout
        Me.GroupBox50.ResumeLayout(False)
        Me.GroupBox50.PerformLayout
        Me.GroupBox48.ResumeLayout(False)
        Me.GroupBox48.PerformLayout
        CType(Me.LocalTagPictureBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.UrlFilesTab.ResumeLayout(False)
        Me.UrlFilesPanel.ResumeLayout(False)
        Me.UrlFilesPanel.PerformLayout
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.WebPictureBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.TpVideoSettings.ResumeLayout(False)
        Me.VideoSettingsPanel.ResumeLayout(False)
        Me.VideoLayoutTable.ResumeLayout(False)
        Me.VideoGeneralPanel.ResumeLayout(False)
        Me.VideoGeneralGroupBox.ResumeLayout(False)
        Me.VideoGeneralGroupBox.PerformLayout
        Me.VideoSpecialGroupBox.ResumeLayout(False)
        Me.VideoSpecialGroupBox.PerformLayout
        Me.VideoGenreGroupBox.ResumeLayout(False)
        Me.VideoGenreGroupBox.PerformLayout
        Me.VideoDommePanel.ResumeLayout(False)
        Me.VideoDommeGeneralGroupBox.ResumeLayout(False)
        Me.VideoDommeGeneralGroupBox.PerformLayout
        Me.GbxVideoSpecialD.ResumeLayout(False)
        Me.GbxVideoSpecialD.PerformLayout
        Me.GbxVideoGenreD.ResumeLayout(False)
        Me.GbxVideoGenreD.PerformLayout
        Me.VideoHeaderPanel.ResumeLayout(False)
        CType(Me.VideoLogo, System.ComponentModel.ISupportInitialize).EndInit
        Me.VideoDescriptionGroupBox.ResumeLayout(False)
        Me.AppsTabPage.ResumeLayout(False)
        Me.AppsSettingsTabList.ResumeLayout(False)
        Me.GlitterAppTabPage.ResumeLayout(False)
        Me.GBGlitter1.ResumeLayout(False)
        Me.GBGlitter1.PerformLayout
        CType(Me.GlitterSlider1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GlitterAV1, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBGlitter3.ResumeLayout(False)
        Me.GBGlitter3.PerformLayout
        CType(Me.GlitterSlider3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GlitterAV3, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBGlitter2.ResumeLayout(False)
        Me.GBGlitter2.PerformLayout
        CType(Me.GlitterSlider2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GlitterAV2, System.ComponentModel.ISupportInitialize).EndInit
        Me.TpGames.ResumeLayout(False)
        Me.TpGames.PerformLayout
        Me.GbxCardsGold.ResumeLayout(False)
        Me.GbxCardsGold.PerformLayout
        CType(Me.GP6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GP2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GP5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GP1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GP3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GP4, System.ComponentModel.ISupportInitialize).EndInit
        Me.GbxCardsBackground.ResumeLayout(False)
        CType(Me.CardBack, System.ComponentModel.ISupportInitialize).EndInit
        Me.GbxCardsBronze.ResumeLayout(False)
        Me.GbxCardsBronze.PerformLayout
        CType(Me.BP3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BP6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BP5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BP2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BP4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BP1, System.ComponentModel.ISupportInitialize).EndInit
        Me.GbxCardsSilver.ResumeLayout(False)
        Me.GbxCardsSilver.PerformLayout
        CType(Me.SP6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SP2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SP5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SP1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SP3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SP4, System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage6.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout
        CType(Me.NBWishlistCost, System.ComponentModel.ISupportInitialize).EndInit
        Me.PNLWishList.ResumeLayout(False)
        CType(Me.WishlistCostSilver, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.WishlistCostGold, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.WishlistPreview, System.ComponentModel.ISupportInitialize).EndInit
        Me.AppsSettingsHeaderPanel.ResumeLayout(False)
        CType(Me.AppsSettingsLogo, System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage26.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PBBackgroundPreview, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsTabPage.ResumeLayout(False)
        Me.RangeSettingsBody.ResumeLayout(False)
        Me.RangeSettingsBodyTablePanel.ResumeLayout(False)
        Me.RangeSettingsBodyRightColumnPanel.ResumeLayout(False)
        Me.RangeSettingsTeaseSlideshowGroupBox.ResumeLayout(False)
        CType(Me.NBNextImageChance, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsGlitterTasksGroupBox.ResumeLayout(False)
        CType(Me.NBTaskCBTTimeMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskCBTTimeMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskEdgeHoldTimeMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskEdgeHoldTimeMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskEdgesMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskEdgesMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskStrokingTimeMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskStrokingTimeMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskStrokesMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTaskStrokesMin, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsVideoTeaseGroupBox.ResumeLayout(False)
        Me.GroupBox19.ResumeLayout(False)
        CType(Me.GreenLightMaximumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GreenLightMinimumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RedLightMaximumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RedLightMinimumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsCensorshipSucksGroupBox.ResumeLayout(False)
        Me.RangeSettingsCensorshipSucksGroupBox.PerformLayout
        CType(Me.ShowCensorshipBarMinimumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.HideCensorshipBarMaximumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.HideCensorshipBarMinimumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ShowCensorshipBarMaximumSeconds, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsBodyMiddleColumnPanel.ResumeLayout(False)
        Me.GBRangeOrgasmChance.ResumeLayout(False)
        Me.GBRangeOrgasmChance.PerformLayout
        CType(Me.SometimesAllowsPercentNumberBox, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RarelyAllowsPercentNumberBox, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.OftenAllowsPercentNumberBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox69.ResumeLayout(False)
        Me.GroupBox69.PerformLayout
        CType(Me.TypeSpeedSlider, System.ComponentModel.ISupportInitialize).EndInit
        Me.GBRangeRuinChance.ResumeLayout(False)
        Me.GBRangeRuinChance.PerformLayout
        CType(Me.NBRuinSometimes, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBRuinRarely, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBRuinOften, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsBodyLeftColumnPanel.ResumeLayout(False)
        Me.RangeSettingsTeaseGroupBox.ResumeLayout(False)
        Me.RangeSettingsTeaseGroupBox.PerformLayout
        CType(Me.NBTauntEdging, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SliderSTF, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.VideoTauntSlider, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTauntCycleMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTauntCycleMin, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTeaseLengthMax, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NBTeaseLengthMin, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsSessionTasksGroupBox.ResumeLayout(False)
        CType(Me.TaskWaitMaximum, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TaskWaitMinimum, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsHeaderPanel.ResumeLayout(False)
        CType(Me.RangeSettingsLogo, System.ComponentModel.ISupportInitialize).EndInit
        Me.RangeSettingsDescriptionGroupBox.ResumeLayout(False)
        Me.TabPage13.ResumeLayout(False)
        Me.ModSubTab.ResumeLayout(False)
        Me.ModPlaylistTabPage.ResumeLayout(False)
        Me.ModPlaylistTabPage.PerformLayout
        Me.TabPage14.ResumeLayout(False)
        Me.TabPage14.PerformLayout
        Me.TabPage24.ResumeLayout(False)
        Me.TabPage24.PerformLayout
        Me.TabPage8.ResumeLayout(False)
        Me.GroupBox29.ResumeLayout(False)
        Me.GroupBox28.ResumeLayout(False)
        Me.GroupBox30.ResumeLayout(False)
        Me.TabPage15.ResumeLayout(False)
        Me.TabPage15.PerformLayout
        Me.GroupBox34.ResumeLayout(False)
        Me.TabPage25.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.GroupBox62.ResumeLayout(False)
        Me.GroupBox62.PerformLayout
        Me.GroupBox33.ResumeLayout(False)
        Me.GroupBox27.ResumeLayout(False)
        Me.GroupBox27.PerformLayout
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage28.ResumeLayout(False)
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage29.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout
        Me.GroupBox26.ResumeLayout(False)
        Me.GroupBox26.PerformLayout
        Me.TabPage30.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SettingsTabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents PNLGeneralSettings As System.Windows.Forms.Panel
    Friend WithEvents GBSafeword As System.Windows.Forms.GroupBox
    Friend WithEvents LBLSubColor As System.Windows.Forms.Label
    Friend WithEvents BTNSubColor As System.Windows.Forms.Button
    Friend WithEvents BTNDomColor As System.Windows.Forms.Button
    Friend WithEvents LBLDomColor As System.Windows.Forms.Label
    Friend WithEvents GBGeneralSystem As System.Windows.Forms.GroupBox
    Friend WithEvents CBSettingsPause As System.Windows.Forms.CheckBox
    Friend WithEvents CBSaveChatlogExit As System.Windows.Forms.CheckBox
    Friend WithEvents CBAutosaveChatlog As System.Windows.Forms.CheckBox
    Friend WithEvents GBGeneralImages As System.Windows.Forms.GroupBox
    Friend WithEvents CBSlideshowRandom As System.Windows.Forms.CheckBox
    Friend WithEvents LandscapeCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CBBlogImageWindow As System.Windows.Forms.CheckBox
    Friend WithEvents CBSlideshowSubDir As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents GBGeneralTextToSpeech As System.Windows.Forms.GroupBox
    Friend WithEvents TTSCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TTSComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents GBGeneralSettings As System.Windows.Forms.GroupBox
    Friend WithEvents CBInputIcon As System.Windows.Forms.CheckBox
    Friend WithEvents TypeInstantlyCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TimeStampCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShowNamesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LBLGeneralSettings As System.Windows.Forms.Label
    Friend WithEvents DommeSettingsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents DommeSettingsLogo As System.Windows.Forms.PictureBox
    Friend WithEvents GBDomRanges As System.Windows.Forms.GroupBox
    Friend WithEvents NBDomMoodMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBDomMoodMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents NBSubAgeMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBSubAgeMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents NBSelfAgeMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBSelfAgeMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents NBAvgCockMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBAvgCockMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents DommeStatsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents boobComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DomLevelDescLabel As System.Windows.Forms.Label
    Friend WithEvents DomAgeNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents DominationLevel As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents DommeSettingsHeaderLabel As System.Windows.Forms.Label
    Friend WithEvents GBDomPersonality As System.Windows.Forms.GroupBox
    Friend WithEvents supremacistCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents vulgarCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents crazyCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GBDomTypingStyle As System.Windows.Forms.GroupBox
    Friend WithEvents CBMeMyMine As System.Windows.Forms.CheckBox
    Friend WithEvents apostropheCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LCaseCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents periodCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents commaCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GBDomOrgasms As System.Windows.Forms.GroupBox
    Friend WithEvents orgasmlockrandombutton As System.Windows.Forms.Button
    Friend WithEvents CBDomOrgasmEnds As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents orgasmsperlockButton As System.Windows.Forms.Button
    Friend WithEvents OrgasmsPerComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents orgasmsperLabel As System.Windows.Forms.Label
    Friend WithEvents limitcheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents OrgasmsPerNumBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents CBDomDenialEnds As System.Windows.Forms.CheckBox
    Friend WithEvents AllowsOrgasmComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents RuinsOrgasmsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents GBDomPetNames As System.Windows.Forms.GroupBox
    Friend WithEvents petnameBox7 As System.Windows.Forms.TextBox
    Friend WithEvents PetNameBox1 As System.Windows.Forms.TextBox
    Friend WithEvents petnameBox4 As System.Windows.Forms.TextBox
    Friend WithEvents petnameBox8 As System.Windows.Forms.TextBox
    Friend WithEvents petnameBox2 As System.Windows.Forms.TextBox
    Friend WithEvents petnameBox6 As System.Windows.Forms.TextBox
    Friend WithEvents petnameBox5 As System.Windows.Forms.TextBox
    Friend WithEvents petnameBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TabPage10 As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox35 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox38 As System.Windows.Forms.GroupBox
    Friend WithEvents TBNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox37 As System.Windows.Forms.GroupBox
    Friend WithEvents TBYes As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox36 As System.Windows.Forms.GroupBox
    Friend WithEvents TBGreeting As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents NBWritingTaskMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBWritingTaskMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents HoldEdgeMaximum As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents NBLongEdge As System.Windows.Forms.NumericUpDown
    Friend WithEvents LBLStrokeTimeTotal As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LBLAvgEdgeNoTouch As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents LBLAvgEdgeStroking As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PictureBox12 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents LBLSubSettingsDescription As System.Windows.Forms.Label
    Friend WithEvents GroupBox32 As System.Windows.Forms.GroupBox
    Friend WithEvents NBBirthdayDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents LBLSubInches As System.Windows.Forms.Label
    Friend WithEvents subAgeNumBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBBirthdayMonth As System.Windows.Forms.NumericUpDown
    Friend WithEvents LBLSubCockSize As System.Windows.Forms.Label
    Friend WithEvents CockSizeNumBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents LBLSubEye As System.Windows.Forms.Label
    Friend WithEvents LBLSubHair As System.Windows.Forms.Label
    Friend WithEvents LBLSubBirthday As System.Windows.Forms.Label
    Friend WithEvents LBLSubAge As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents LocalButtSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalButtDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalButtDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents BtnImageUrlButt As System.Windows.Forms.Button
    Friend WithEvents LocalBoobsSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalBoobsDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalBoobsDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents BtnImageUrlBoobs As System.Windows.Forms.Button
    Friend WithEvents UrlFilesTab As System.Windows.Forms.TabPage
    Friend WithEvents UrlFilesPanel As System.Windows.Forms.Panel
    Friend WithEvents UrlImageContinueButton As System.Windows.Forms.Button
    Friend WithEvents UrlImageAddAndContinue As System.Windows.Forms.Button
    Friend WithEvents BTNWICancel As System.Windows.Forms.Button
    Friend WithEvents CBWIReview As System.Windows.Forms.CheckBox
    Friend WithEvents BTNWIBrowse As System.Windows.Forms.Button
    Friend WithEvents TBWIDirectory As System.Windows.Forms.TextBox
    Friend WithEvents BTNWIDisliked As System.Windows.Forms.Button
    Friend WithEvents BTNWILiked As System.Windows.Forms.Button
    Friend WithEvents UrlImageRemoveButton As System.Windows.Forms.Button
    Friend WithEvents CBWISaveToDisk As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents WebImageProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents CreateBlogContainerButton As System.Windows.Forms.Button
    Friend WithEvents LBLWebImageCount As System.Windows.Forms.Label
    Friend WithEvents BTNWISave As System.Windows.Forms.Button
    Friend WithEvents UrlFilesPreviousImageButton As System.Windows.Forms.Button
    Friend WithEvents UrlFilesNextImageButton As System.Windows.Forms.Button
    Friend WithEvents WebPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents ImageBlogs As System.Windows.Forms.Label
    Friend WithEvents TpVideoSettings As System.Windows.Forms.TabPage
    Friend WithEvents BTNVideoModClear As System.Windows.Forms.Button
    Friend WithEvents BTNVideoModLoad As System.Windows.Forms.Button
    Friend WithEvents BTNVideoModSave As System.Windows.Forms.Button
    Friend WithEvents RTBVideoMod As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox30 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox28 As System.Windows.Forms.GroupBox
    Friend WithEvents CBVTType As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox29 As System.Windows.Forms.GroupBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents RangeSettingsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents RangeSettingsBody As System.Windows.Forms.Panel
    Friend WithEvents RangeSettingsLogo As System.Windows.Forms.PictureBox
    Friend WithEvents LBLStf As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents SliderSTF As System.Windows.Forms.TrackBar
    Friend WithEvents RangeSettingsDescriptionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents RangeSettingsDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents RangeSettingsVideoTeaseGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents RangeSettingsCensorshipSucksGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CensorshipBarDuringVideoTease As System.Windows.Forms.CheckBox
    Friend WithEvents HideCensorshipBarMaximumSeconds As System.Windows.Forms.NumericUpDown
    Friend WithEvents HideCensorshipBarMinimumSeconds As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ShowCensorshipBarMaximumSeconds As System.Windows.Forms.NumericUpDown
    Friend WithEvents ShowCensorshipBarMinimumSeconds As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents RangeSettingsHeaderLabel As System.Windows.Forms.Label
    Friend WithEvents GBGlitter1 As System.Windows.Forms.GroupBox
    Friend WithEvents BTNGlitter1 As System.Windows.Forms.Button
    Friend WithEvents LBLGlitterNC1 As System.Windows.Forms.Label
    Friend WithEvents LBLGlitterSlider1 As System.Windows.Forms.Label
    Friend WithEvents GlitterSlider1 As System.Windows.Forms.TrackBar
    Friend WithEvents CBGlitter1 As System.Windows.Forms.CheckBox
    Friend WithEvents TBGlitter1 As System.Windows.Forms.TextBox
    Friend WithEvents GlitterAV1 As System.Windows.Forms.PictureBox
    Friend WithEvents GBGlitter3 As System.Windows.Forms.GroupBox
    Friend WithEvents BTNGlitter3 As System.Windows.Forms.Button
    Friend WithEvents LBLGlitterNC3 As System.Windows.Forms.Label
    Friend WithEvents LBLGlitterSlider3 As System.Windows.Forms.Label
    Friend WithEvents GlitterSlider3 As System.Windows.Forms.TrackBar
    Friend WithEvents CBGlitter3 As System.Windows.Forms.CheckBox
    Friend WithEvents TBGlitter3 As System.Windows.Forms.TextBox
    Friend WithEvents GlitterAV3 As System.Windows.Forms.PictureBox
    Friend WithEvents GBGlitter2 As System.Windows.Forms.GroupBox
    Friend WithEvents BTNGlitter2 As System.Windows.Forms.Button
    Friend WithEvents LBLGlitterNC2 As System.Windows.Forms.Label
    Friend WithEvents LBLGlitterSlider2 As System.Windows.Forms.Label
    Friend WithEvents GlitterSlider2 As System.Windows.Forms.TrackBar
    Friend WithEvents CBGlitter2 As System.Windows.Forms.CheckBox
    Friend WithEvents TBGlitter2 As System.Windows.Forms.TextBox
    Friend WithEvents GlitterAV2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Button26 As System.Windows.Forms.Button
    Friend WithEvents LBLGlitModScriptCount As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents CBGlitModType As System.Windows.Forms.ComboBox
    Friend WithEvents LBGlitModScripts As System.Windows.Forms.ListBox
    Friend WithEvents RTBGlitModDommePost As System.Windows.Forms.RichTextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents TBGlitModFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents LBLGlitModDomType As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Button29 As System.Windows.Forms.Button
    Friend WithEvents RTBGlitModResponses As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox34 As System.Windows.Forms.GroupBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents TabPage13 As System.Windows.Forms.TabPage
    Friend WithEvents ModSubTab As System.Windows.Forms.TabControl
    Friend WithEvents TabPage14 As System.Windows.Forms.TabPage
    Friend WithEvents LBLKeywordPreview As System.Windows.Forms.Label
    Friend WithEvents TBKeywordPreview As System.Windows.Forms.TextBox
    Friend WithEvents Button37 As System.Windows.Forms.Button
    Friend WithEvents Button50 As System.Windows.Forms.Button
    Friend WithEvents Button22 As System.Windows.Forms.Button
    Friend WithEvents TBKeyWords As System.Windows.Forms.TextBox
    Friend WithEvents LBKeyWords As System.Windows.Forms.ListBox
    Friend WithEvents RTBKeyWords As System.Windows.Forms.RichTextBox
    Friend WithEvents TabPage15 As System.Windows.Forms.TabPage
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GetColor As System.Windows.Forms.ColorDialog
    Friend WithEvents WebImageFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenScriptDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TeaseSlideShowRadio As System.Windows.Forms.RadioButton
    Friend WithEvents ManualSlideShowRadio As System.Windows.Forms.RadioButton
    Friend WithEvents SlideShowNumBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents TimedSlideShowRadio As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SubMessageFontCB As System.Windows.Forms.ComboBox
    Friend WithEvents NBFontSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DommeMessageFontCB As System.Windows.Forms.ComboBox
    Friend WithEvents NBFontSizeD As System.Windows.Forms.NumericUpDown
    Friend WithEvents GBDommeFont As System.Windows.Forms.GroupBox
    Friend WithEvents GBSubFont As System.Windows.Forms.GroupBox
    Friend WithEvents DommePubicHairComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents domhairlengthComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CBDomTattoos As System.Windows.Forms.CheckBox
    Friend WithEvents CBDomFreckles As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents NBDomBirthdayDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents NBDomBirthdayMonth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents CBImageInfo As System.Windows.Forms.CheckBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents RemoteMediaContainerList As System.Windows.Forms.CheckedListBox
    Friend WithEvents LocalMaledomDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalGayDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalHentaiDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalMaledomDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalGayDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalHentaiDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalHentaiEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalMaledomEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalGayEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalLezdomDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalFemdomDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalBlowjobDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalLesbianDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalSoftcoreDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalHardcoreDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalLezdomDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalFemdomDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalBlowjobDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalLesbianDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalSoftcoreDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalHardcoreDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalHardcoreEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalSoftcoreEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalLesbianEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalBlowjobEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalLezdomEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalFemdomEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalGeneralDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalGeneralDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalGeneralEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalHardcoreSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalGeneralSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalMaledomSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalGaySubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalHentaiSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalLezdomSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalFemdomSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalBlowjobSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalLesbianSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalSoftcoreSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalCaptionsSubdirectoryCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalCaptionsDirectoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LocalCaptionsDirectoryButton As System.Windows.Forms.Button
    Friend WithEvents LocalCaptionsEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GBDommeImages As System.Windows.Forms.GroupBox
    Friend WithEvents BTNDomImageDir As System.Windows.Forms.Button
    Friend WithEvents TbxDomImageDir As System.Windows.Forms.TextBox
    Friend WithEvents TabPage16 As System.Windows.Forms.TabPage
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents TCScripts As System.Windows.Forms.TabControl
    Friend WithEvents ScriptsStartTab As System.Windows.Forms.TabPage
    Friend WithEvents StartScripts As System.Windows.Forms.CheckedListBox
    Friend WithEvents ScriptsModuleTab As System.Windows.Forms.TabPage
    Friend WithEvents ScriptsLinkTab As System.Windows.Forms.TabPage
    Friend WithEvents ScriptsEndTab As System.Windows.Forms.TabPage
    Friend WithEvents ScriptsDescriptionGroup As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox43 As System.Windows.Forms.GroupBox
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents ScriptTitle As System.Windows.Forms.Label
    Friend WithEvents ScriptsRequirementsGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ScriptRequirements As System.Windows.Forms.RichTextBox
    Friend WithEvents ScriptInfoTextArea As System.Windows.Forms.RichTextBox
    Friend WithEvents LBLScriptReq As System.Windows.Forms.Label
    Friend WithEvents ModuleScripts As System.Windows.Forms.CheckedListBox
    Friend WithEvents LinkScripts As System.Windows.Forms.CheckedListBox
    Friend WithEvents EndScripts As System.Windows.Forms.CheckedListBox
    Friend WithEvents BTNScriptOpen As System.Windows.Forms.Button
    Friend WithEvents SelectAvailableScriptsButton As System.Windows.Forms.Button
    Friend WithEvents SelectNoScriptsButton As System.Windows.Forms.Button
    Friend WithEvents SelectAllScriptsButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox45 As System.Windows.Forms.GroupBox
    Friend WithEvents BallTortureEnabledCB As System.Windows.Forms.CheckBox
    Friend WithEvents CockTortureEnabledCB As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents OpenSettingsDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveSettingsDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ChastityDeviceContainsSpikesCB As System.Windows.Forms.CheckBox
    Friend WithEvents DoesChastityDeviceRequirePiercingCB As System.Windows.Forms.CheckBox
    Friend WithEvents AllowLongEdgeInterruptCB As System.Windows.Forms.CheckBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents AllowLongEdgeTauntCB As System.Windows.Forms.CheckBox
    Friend WithEvents CockAndBallTortureLevelLbl As System.Windows.Forms.Label
    Friend WithEvents CockAndBallTortureLevelSlider As System.Windows.Forms.TrackBar
    Friend WithEvents TBDomEyeColor As System.Windows.Forms.TextBox
    Friend WithEvents TBDomHairColor As System.Windows.Forms.TextBox
    Friend WithEvents TBSubEyeColor As System.Windows.Forms.TextBox
    Friend WithEvents TBSubHairColor As System.Windows.Forms.TextBox
    Friend WithEvents CBSubCircumcised As System.Windows.Forms.CheckBox
    Friend WithEvents CBSubPierced As System.Windows.Forms.CheckBox
    Friend WithEvents LBLEmpathy As System.Windows.Forms.Label
    Friend WithEvents NBEmpathy As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents BTNSaveDomSet As System.Windows.Forms.Button
    Friend WithEvents DommeSettingsSaveButton As System.Windows.Forms.Button
    Friend WithEvents CBAuditStartup As System.Windows.Forms.CheckBox
    Friend WithEvents GBRangeOrgasmChance As System.Windows.Forms.GroupBox
    Friend WithEvents RarelyAllowsPercentLabel As System.Windows.Forms.Label
    Friend WithEvents SometimesAllowsPercentNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents SometimesAllowsPercentLabel As System.Windows.Forms.Label
    Friend WithEvents OftenAllowsPercentLabel As System.Windows.Forms.Label
    Friend WithEvents RarelyAllowsPercentNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents OftenAllowsPercentNumberBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents DommeDecideOrgasmCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GBRangeRuinChance As System.Windows.Forms.GroupBox
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents NBRuinSometimes As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents NBRuinRarely As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBRuinOften As System.Windows.Forms.NumericUpDown
    Friend WithEvents DommeDecideRuinCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RangeSettingsTeaseGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents LBLSafeword As System.Windows.Forms.Label
    Friend WithEvents TBSafeword As System.Windows.Forms.TextBox
    Friend WithEvents AppsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents AppsSettingsTabList As System.Windows.Forms.TabControl
    Friend WithEvents GlitterAppTabPage As System.Windows.Forms.TabPage
    Friend WithEvents TpGames As System.Windows.Forms.TabPage
    Friend WithEvents GbxCardsGold As System.Windows.Forms.GroupBox
    Friend WithEvents GN6 As System.Windows.Forms.TextBox
    Friend WithEvents GP6 As System.Windows.Forms.PictureBox
    Friend WithEvents GN5 As System.Windows.Forms.TextBox
    Friend WithEvents GP5 As System.Windows.Forms.PictureBox
    Friend WithEvents GN4 As System.Windows.Forms.TextBox
    Friend WithEvents GP4 As System.Windows.Forms.PictureBox
    Friend WithEvents GN3 As System.Windows.Forms.TextBox
    Friend WithEvents GP3 As System.Windows.Forms.PictureBox
    Friend WithEvents GN2 As System.Windows.Forms.TextBox
    Friend WithEvents GP2 As System.Windows.Forms.PictureBox
    Friend WithEvents GN1 As System.Windows.Forms.TextBox
    Friend WithEvents GP1 As System.Windows.Forms.PictureBox
    Friend WithEvents GbxCardsSilver As System.Windows.Forms.GroupBox
    Friend WithEvents SN6 As System.Windows.Forms.TextBox
    Friend WithEvents SP6 As System.Windows.Forms.PictureBox
    Friend WithEvents SN5 As System.Windows.Forms.TextBox
    Friend WithEvents SP5 As System.Windows.Forms.PictureBox
    Friend WithEvents SN4 As System.Windows.Forms.TextBox
    Friend WithEvents SP4 As System.Windows.Forms.PictureBox
    Friend WithEvents SN3 As System.Windows.Forms.TextBox
    Friend WithEvents SP3 As System.Windows.Forms.PictureBox
    Friend WithEvents SN2 As System.Windows.Forms.TextBox
    Friend WithEvents SP2 As System.Windows.Forms.PictureBox
    Friend WithEvents SN1 As System.Windows.Forms.TextBox
    Friend WithEvents SP1 As System.Windows.Forms.PictureBox
    Friend WithEvents GbxCardsBackground As System.Windows.Forms.GroupBox
    Friend WithEvents GbxCardsBronze As System.Windows.Forms.GroupBox
    Friend WithEvents BN6 As System.Windows.Forms.TextBox
    Friend WithEvents BP6 As System.Windows.Forms.PictureBox
    Friend WithEvents BN5 As System.Windows.Forms.TextBox
    Friend WithEvents BP5 As System.Windows.Forms.PictureBox
    Friend WithEvents BN4 As System.Windows.Forms.TextBox
    Friend WithEvents BP4 As System.Windows.Forms.PictureBox
    Friend WithEvents BN3 As System.Windows.Forms.TextBox
    Friend WithEvents BP3 As System.Windows.Forms.PictureBox
    Friend WithEvents BN2 As System.Windows.Forms.TextBox
    Friend WithEvents BP2 As System.Windows.Forms.PictureBox
    Friend WithEvents BN1 As System.Windows.Forms.TextBox
    Friend WithEvents BP1 As System.Windows.Forms.PictureBox
    Friend WithEvents CardBack As System.Windows.Forms.PictureBox
    Friend WithEvents CBGameSounds As System.Windows.Forms.CheckBox
    Friend WithEvents LblCardsSetupNote As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents UseAverageEdgeThresholdCB As System.Windows.Forms.CheckBox
    Friend WithEvents LBLLastRuined As System.Windows.Forms.Label
    Friend WithEvents TabPage24 As System.Windows.Forms.TabPage
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TBResponses As System.Windows.Forms.TextBox
    Friend WithEvents LBResponses As System.Windows.Forms.ListBox
    Friend WithEvents RTBResponses As System.Windows.Forms.RichTextBox
    Friend WithEvents RTBResponsesKEY As System.Windows.Forms.RichTextBox
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents VideoTauntSlider As System.Windows.Forms.TrackBar
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents NBTeaseLengthMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents NBTeaseLengthMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents NBTauntCycleMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents NBTauntCycleMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents CBTauntCycleDD As System.Windows.Forms.CheckBox
    Friend WithEvents TeaseLengthDommeDetermined As System.Windows.Forms.CheckBox
    Friend WithEvents RangeSettingsTeaseSlideshowGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents VideoTauntSliderLabel As System.Windows.Forms.Label
    Friend WithEvents VideoTauntDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents NBNextImageChance As System.Windows.Forms.NumericUpDown
    Friend WithEvents LBVidScript As System.Windows.Forms.ListBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents BTNWishlistCreate As System.Windows.Forms.Button
    Friend WithEvents radioGold As System.Windows.Forms.RadioButton
    Friend WithEvents radioSilver As System.Windows.Forms.RadioButton
    Friend WithEvents NBWishlistCost As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents TBWishlistComment As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TBWishlistURL As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents TBWishlistItem As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PNLWishList As System.Windows.Forms.Panel
    Friend WithEvents WishlistCostSilver As System.Windows.Forms.PictureBox
    Friend WithEvents LBLWishListText As System.Windows.Forms.Label
    Friend WithEvents LBLWishlistCost As System.Windows.Forms.Label
    Friend WithEvents WishlistCostGold As System.Windows.Forms.PictureBox
    Friend WithEvents LBLWishListName As System.Windows.Forms.Label
    Friend WithEvents WishlistPreview As System.Windows.Forms.PictureBox
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents CBOwnChastity As System.Windows.Forms.CheckBox
    Friend WithEvents CBIncludeGifs As System.Windows.Forms.CheckBox
    Friend WithEvents CBHimHer As System.Windows.Forms.CheckBox
    Friend WithEvents CBDomDel As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage25 As System.Windows.Forms.TabPage
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents TBWebStop As System.Windows.Forms.TextBox
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents TBWebStart As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents WebToy As System.Windows.Forms.WebBrowser
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Friend WithEvents PBMaintenance As System.Windows.Forms.ProgressBar
    Friend WithEvents LBLMaintenance As System.Windows.Forms.Label
    Friend WithEvents BTNMaintenanceRebuild As System.Windows.Forms.Button
    Friend WithEvents BTNMaintenanceCancel As System.Windows.Forms.Button
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents PBCurrent As System.Windows.Forms.ProgressBar
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents BTNMaintenanceRefresh As System.Windows.Forms.Button
    Friend WithEvents ModPlaylistTabPage As System.Windows.Forms.TabPage
    Friend WithEvents BTNMaintenanceScripts As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox27 As System.Windows.Forms.GroupBox
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents LBLSesSpace As System.Windows.Forms.Label
    Friend WithEvents LBLSesFiles As System.Windows.Forms.Label
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents CBBallsToPussy As System.Windows.Forms.CheckBox
    Friend WithEvents CBCockToClit As System.Windows.Forms.CheckBox
    Friend WithEvents LBLLastOrgasm As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents LBLSubBdayFormat As System.Windows.Forms.Label
    Friend WithEvents ScriptPlayList As System.Windows.Forms.WebBrowser
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents LBLPlaylIstLink As System.Windows.Forms.Label
    Friend WithEvents LBLPlaylistModule As System.Windows.Forms.Label
    Friend WithEvents LBLPLaylistStart As System.Windows.Forms.Label
    Friend WithEvents LBPlaylist As System.Windows.Forms.ListBox
    Friend WithEvents BTNPlaylistEnd As System.Windows.Forms.Button
    Friend WithEvents BTNPlaylistClearAll As System.Windows.Forms.Button
    Friend WithEvents BTNPlaylistSave As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents TBPlaylistSave As System.Windows.Forms.TextBox
    Friend WithEvents BTNPlaylistCtrlZ As System.Windows.Forms.Button
    Friend WithEvents RadioPlaylistRegScripts As System.Windows.Forms.RadioButton
    Friend WithEvents RadioPlaylistScripts As System.Windows.Forms.RadioButton
    Friend WithEvents BtnContact1ImageDir As System.Windows.Forms.Button
    Friend WithEvents TbxContact1ImageDir As System.Windows.Forms.TextBox
    Friend WithEvents BtnContact3ImageDir As System.Windows.Forms.Button
    Friend WithEvents TbxContact3ImageDir As System.Windows.Forms.TextBox
    Friend WithEvents BtnContact2ImageDir As System.Windows.Forms.Button
    Friend WithEvents TbxContact2ImageDir As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox33 As System.Windows.Forms.GroupBox
    Friend WithEvents ChastityModeButton As System.Windows.Forms.Button
    Friend WithEvents InChastityLabel As System.Windows.Forms.Label
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents TTDir As System.Windows.Forms.ToolTip
    Friend WithEvents BtnContact3ImageDirClear As System.Windows.Forms.Button
    Friend WithEvents BtnContact1ImageDirClear As System.Windows.Forms.Button
    Friend WithEvents BtnContact2ImageDirClear As System.Windows.Forms.Button
    Friend WithEvents GroupBox62 As System.Windows.Forms.GroupBox
    Friend WithEvents RBGerman As System.Windows.Forms.RadioButton
    Friend WithEvents RBEnglish As System.Windows.Forms.RadioButton
    Friend WithEvents TabPage26 As System.Windows.Forms.TabPage
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PBBackgroundPreview As System.Windows.Forms.PictureBox
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents CBStretchBack As System.Windows.Forms.CheckBox
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents LBLTextColor As System.Windows.Forms.Label
    Friend WithEvents LBLChatWindowColor2 As System.Windows.Forms.Label
    Friend WithEvents LBLTextColor2 As System.Windows.Forms.Label
    Friend WithEvents LBLChatTextColor As System.Windows.Forms.Label
    Friend WithEvents LBLBackColor2 As System.Windows.Forms.Label
    Friend WithEvents LBLButtonColor As System.Windows.Forms.Label
    Friend WithEvents LBLChatWindowColor As System.Windows.Forms.Label
    Friend WithEvents LBLBackColor As System.Windows.Forms.Label
    Friend WithEvents LBLChatTextColor2 As System.Windows.Forms.Label
    Friend WithEvents LBLButtonColor2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents LBLDateTimeColor2 As System.Windows.Forms.Label
    Friend WithEvents Label137 As System.Windows.Forms.Label
    Friend WithEvents Label138 As System.Windows.Forms.Label
    Friend WithEvents LBLDateBackColor2 As System.Windows.Forms.Label
    Friend WithEvents CBTransparentTime As System.Windows.Forms.CheckBox
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents CBFlipBack As System.Windows.Forms.CheckBox
    Friend WithEvents Button32 As System.Windows.Forms.Button
    Friend WithEvents CondescendingCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents degradingCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents sadisticCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents TimeBoxWakeUp As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents HoldEdgeMinimumUnits As System.Windows.Forms.Label
    Friend WithEvents HoldEdgeMinimum As System.Windows.Forms.NumericUpDown
    Friend WithEvents LBLMaxHold As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents GroupBox22 As System.Windows.Forms.GroupBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents GroupBox63 As System.Windows.Forms.GroupBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents NBTypoChance As System.Windows.Forms.NumericUpDown
    Friend WithEvents TBEmote As System.Windows.Forms.TextBox
    Friend WithEvents TBEmoteEnd As System.Windows.Forms.TextBox
    Friend WithEvents SliderVRate As System.Windows.Forms.TrackBar
    Friend WithEvents SliderVVolume As System.Windows.Forms.TrackBar
    Friend WithEvents LBLVRate As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents LBLVVolume As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents LBLMaxExtremeHold As System.Windows.Forms.Label
    Friend WithEvents LBLMinExtremeHold As System.Windows.Forms.Label
    Friend WithEvents ExtremeEdgeHoldMinimum As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents ExtremeEdgeHoldMaximum As System.Windows.Forms.NumericUpDown
    Friend WithEvents LBLMaxLongHold As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents LBLMinLongHold As System.Windows.Forms.Label
    Friend WithEvents LongEdgeHoldMinimum As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents LongEdgeHoldMaximum As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents WebTeaseMode As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage28 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage29 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox26 As System.Windows.Forms.GroupBox
    Friend WithEvents RBDebugTaunts3 As System.Windows.Forms.RadioButton
    Friend WithEvents RBDebugTaunts2 As System.Windows.Forms.RadioButton
    Friend WithEvents RBDebugTaunts1 As System.Windows.Forms.RadioButton
    Friend WithEvents CBDebugTauntsEndless As System.Windows.Forms.CheckBox
    Friend WithEvents CBDebugTaunts As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage30 As System.Windows.Forms.TabPage
    Friend WithEvents TBDebugTaunts3 As System.Windows.Forms.TextBox
    Friend WithEvents TBDebugTaunts2 As System.Windows.Forms.TextBox
    Friend WithEvents TBDebugTaunts1 As System.Windows.Forms.TextBox
    Friend WithEvents BTNDebugTauntsClear As System.Windows.Forms.Button
    Friend WithEvents LBLCycleDebugCountdown As System.Windows.Forms.Label
    Friend WithEvents Button19 As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents GroupBox64 As System.Windows.Forms.GroupBox
    Friend WithEvents CBMuteMedia As System.Windows.Forms.CheckBox
    Friend WithEvents BTNOfflineMode As System.Windows.Forms.Button
    Friend WithEvents LBLOfflineMode As System.Windows.Forms.Label
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents CBNewSlideshow As System.Windows.Forms.CheckBox
    Friend WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents NBTauntEdging As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents BTNDebugHoldEdgeTimer As System.Windows.Forms.Button
    Friend WithEvents BTNDebugStrokeTauntTimer As System.Windows.Forms.Button
    Friend WithEvents LBLDebugHoldEdgeTime As System.Windows.Forms.Label
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents BTNDebugStrokeTime As System.Windows.Forms.Button
    Friend WithEvents BTNDebugEdgeTauntTimer As System.Windows.Forms.Button
    Friend WithEvents LBLDebugTeaseTime As System.Windows.Forms.Label
    Friend WithEvents LBLDebugStrokeTime As System.Windows.Forms.Label
    Friend WithEvents LBLDebugEdgeTauntTime As System.Windows.Forms.Label
    Friend WithEvents BTNDebugTeaseTimer As System.Windows.Forms.Button
    Friend WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents LBLDebugStrokeTauntTime As System.Windows.Forms.Label
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents LBLDebugScriptTime As System.Windows.Forms.Label
    Friend WithEvents GernreImagesTab As System.Windows.Forms.TabControl
    Friend WithEvents TpImagesUrlFiles As System.Windows.Forms.TabPage
    Friend WithEvents TpImagesGenre As System.Windows.Forms.TabPage
    Friend WithEvents BTNURLFilesNone As System.Windows.Forms.Button
    Friend WithEvents BTNURLFilesAll As System.Windows.Forms.Button
    Friend WithEvents GbxImagesGenre As System.Windows.Forms.GroupBox
    Friend WithEvents GrbImageUrlFiles As System.Windows.Forms.GroupBox
    Friend WithEvents ChbImageUrlButts As System.Windows.Forms.CheckBox
    Friend WithEvents ChbImageUrlBoobs As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlGeneral As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlLesbian As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlCaptions As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlBlowjob As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlMaledom As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlGay As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlGay As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlSoftcore As System.Windows.Forms.CheckBox
    Friend WithEvents ChbImageUrlHentai As System.Windows.Forms.CheckBox
    Friend WithEvents ChbImageUrlLezdom As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlHentai As System.Windows.Forms.Button
    Friend WithEvents BtnImageUrlLezdom As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlFemdom As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlFemdom As System.Windows.Forms.Button
    Friend WithEvents BtnImageUrlBlowjob As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlCaptions As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlLesbian As System.Windows.Forms.Button
    Friend WithEvents BtnImageUrlSoftcore As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlGeneral As System.Windows.Forms.CheckBox
    Friend WithEvents BtnImageUrlHardcore As System.Windows.Forms.Button
    Friend WithEvents ChbImageUrlHardcore As System.Windows.Forms.CheckBox
    Friend WithEvents ChbImageUrlMaledom As System.Windows.Forms.CheckBox
    Friend WithEvents LocalButtEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LocalBoobsEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox66 As System.Windows.Forms.GroupBox
    Friend WithEvents PBURLPreview As System.Windows.Forms.PictureBox
    Friend WithEvents PreviewRemoteImagesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RangeSettingsGlitterTasksGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents NBTaskStrokingTimeMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBTaskStrokingTimeMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label154 As System.Windows.Forms.Label
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents NBTaskStrokesMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBTaskStrokesMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents NBTaskEdgesMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBTaskEdgesMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents NBTaskCBTTimeMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBTaskCBTTimeMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents NBTaskEdgeHoldTimeMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBTaskEdgeHoldTimeMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents BtnImportSettings As Button
    Friend WithEvents LblImportSettings As Label
    Friend WithEvents RangeSettingsSessionTasksGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents TaskWaitMaximum As System.Windows.Forms.NumericUpDown
    Friend WithEvents TaskWaitMinimum As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents GroupBox69 As System.Windows.Forms.GroupBox
    Friend WithEvents TypeSpeedSlider As System.Windows.Forms.TrackBar
    Friend WithEvents TypeSpeedLabel As System.Windows.Forms.Label
    Friend WithEvents TimedWriting As System.Windows.Forms.CheckBox
    Friend WithEvents TypesSpeedVal As System.Windows.Forms.Label
    Friend WithEvents TlpImageUrls As TableLayoutPanel
    Friend WithEvents TxbImageUrlLesbian As TextBox
    Friend WithEvents TxbImageUrlHardcore As TextBox
    Friend WithEvents TxbImageUrlSoftcore As TextBox
    Friend WithEvents TxbImageUrlBlowjob As TextBox
    Friend WithEvents TxbImageUrlFemdom As TextBox
    Friend WithEvents TxbImageUrlLezdom As TextBox
    Friend WithEvents TxbImageUrlHentai As TextBox
    Friend WithEvents TxbImageUrlGay As TextBox
    Friend WithEvents TxbImageUrlMaledom As TextBox
    Friend WithEvents TxbImageUrlCaptions As TextBox
    Friend WithEvents TxbImageUrlGeneral As TextBox
    Friend WithEvents TxbImageUrlBoobs As TextBox
    Friend WithEvents TxbImageUrlButts As TextBox
    Friend WithEvents TxbImgUrlHardcore As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents BWURLFiles As URL_Files.URL_File_BGW
    Friend WithEvents Button24 As System.Windows.Forms.Button
    Friend WithEvents Button33 As System.Windows.Forms.Button
    Friend WithEvents TabPage33 As TabPage
    Friend WithEvents LocalTagsTab As TabControl
    Friend WithEvents TabPage34 As TabPage
    Friend WithEvents FileDropDownLabel As TabPage
    Friend WithEvents CBTagSeeThrough As RadioButton
    Friend WithEvents CBTagAllFours As CheckBox
    Friend WithEvents CBTagGlaring As CheckBox
    Friend WithEvents CBTagSmiling As CheckBox
    Friend WithEvents DommeTagDirInput As TextBox
    Friend WithEvents CBTagPiercing As CheckBox
    Friend WithEvents CBTagLegs As CheckBox
    Friend WithEvents TBTagFurniture As TextBox
    Friend WithEvents CBTagFurniture As CheckBox
    Friend WithEvents TBTagSexToy As TextBox
    Friend WithEvents CBTagSexToy As CheckBox
    Friend WithEvents TBTagTattoo As TextBox
    Friend WithEvents CBTagTattoo As CheckBox
    Friend WithEvents TBTagUnderwear As TextBox
    Friend WithEvents CBTagUnderwear As CheckBox
    Friend WithEvents TBTagGarment As TextBox
    Friend WithEvents CBTagGarment As CheckBox
    Friend WithEvents Label72 As Label
    Friend WithEvents CBTagHandsCovering As RadioButton
    Friend WithEvents CBTagGarmentCovering As RadioButton
    Friend WithEvents CBTagCloseUp As CheckBox
    Friend WithEvents CBTagNaked As RadioButton
    Friend WithEvents CBTagSideView As CheckBox
    Friend WithEvents BTNTagPrevious As Button
    Friend WithEvents CBTagHalfDressed As RadioButton
    Friend WithEvents BTNTagNext As Button
    Friend WithEvents CBTagFullyDressed As RadioButton
    Friend WithEvents LBLTagCount As Label
    Friend WithEvents CBTagSucking As CheckBox
    Friend WithEvents CBTagMasturbating As CheckBox
    Friend WithEvents CBTagFeet As CheckBox
    Friend WithEvents CBTagBoobs As CheckBox
    Friend WithEvents CBTagAss As CheckBox
    Friend WithEvents CBTagPussy As CheckBox
    Friend WithEvents BTNTagSave As Button
    Friend WithEvents DommeTagDirectoryButton As Button
    Friend WithEvents ImageTagPictureBox As PictureBox
    Friend WithEvents CBTagFace As CheckBox
    Friend WithEvents GroupBox55 As GroupBox
    Friend WithEvents CBTagNurse As CheckBox
    Friend WithEvents CBTagSchoolgirl As CheckBox
    Friend WithEvents CBTagMaid As CheckBox
    Friend WithEvents CBTagTeacher As CheckBox
    Friend WithEvents CBTagSuperhero As CheckBox
    Friend WithEvents GroupBox53 As GroupBox
    Friend WithEvents CBTagTrap As CheckBox
    Friend WithEvents CBTagTentacles As CheckBox
    Friend WithEvents CBTagMonsterGirl As CheckBox
    Friend WithEvents CBTagBukkake As CheckBox
    Friend WithEvents CBTagGanguro As CheckBox
    Friend WithEvents CBTagBodyWriting As CheckBox
    Friend WithEvents CBTagMahouShoujo As CheckBox
    Friend WithEvents CBTagBakunyuu As CheckBox
    Friend WithEvents CBTagAhegao As CheckBox
    Friend WithEvents CBTagShibari As CheckBox
    Friend WithEvents GroupBox49 As GroupBox
    Friend WithEvents CBTagBodyMouth As CheckBox
    Friend WithEvents CBTagBodyAss As CheckBox
    Friend WithEvents CBTagBodyFace As CheckBox
    Friend WithEvents CBTagBodyLegs As CheckBox
    Friend WithEvents CBTagBodyBalls As CheckBox
    Friend WithEvents CBTagBodyCock As CheckBox
    Friend WithEvents CBTagBodyFeet As CheckBox
    Friend WithEvents CBTagBodyNipples As CheckBox
    Friend WithEvents CBTagBodyPussy As CheckBox
    Friend WithEvents CBTagBodyTits As CheckBox
    Friend WithEvents CBTagBodyFingers As CheckBox
    Friend WithEvents GroupBox46 As GroupBox
    Friend WithEvents CBTagMultiSub As CheckBox
    Friend WithEvents CBTagMultiDom As CheckBox
    Friend WithEvents CBTagFemdom As CheckBox
    Friend WithEvents CBTag2M As CheckBox
    Friend WithEvents CBTagFutadom As CheckBox
    Friend WithEvents CBTagFemsub As CheckBox
    Friend WithEvents CBTag2Futa As CheckBox
    Friend WithEvents CBTagMaledom As CheckBox
    Friend WithEvents CBTag3M As CheckBox
    Friend WithEvents CBTagFutasub As CheckBox
    Friend WithEvents CBTag3Futa As CheckBox
    Friend WithEvents CBTagMalesub As CheckBox
    Friend WithEvents CBTag2F As CheckBox
    Friend WithEvents CBTag1Futa As CheckBox
    Friend WithEvents CBTag1M As CheckBox
    Friend WithEvents CBTag1F As CheckBox
    Friend WithEvents CBTag3F As CheckBox
    Friend WithEvents GroupBox54 As GroupBox
    Friend WithEvents CBTagTattoos As CheckBox
    Friend WithEvents CBTagAnalToy As CheckBox
    Friend WithEvents CBTagDomme As CheckBox
    Friend WithEvents CBTagPocketPussy As CheckBox
    Friend WithEvents CBTagWatersports As CheckBox
    Friend WithEvents CBTagStockings As CheckBox
    Friend WithEvents CBTagCumshot As CheckBox
    Friend WithEvents CBTagCumEating As CheckBox
    Friend WithEvents CBTagVibrator As CheckBox
    Friend WithEvents CBTagDildo As CheckBox
    Friend WithEvents CBTagKissing As CheckBox
    Friend WithEvents BdsmTagGroup As GroupBox
    Friend WithEvents CBTagBallTorture As CheckBox
    Friend WithEvents CBTagGag As CheckBox
    Friend WithEvents CBTagBlindfold As CheckBox
    Friend WithEvents CBTagWhipping As CheckBox
    Friend WithEvents CBTagCockTorture As CheckBox
    Friend WithEvents CBTagElectro As CheckBox
    Friend WithEvents CBTagHotWax As CheckBox
    Friend WithEvents CBTagClamps As CheckBox
    Friend WithEvents CBTagStrapon As CheckBox
    Friend WithEvents CBTagSpanking As CheckBox
    Friend WithEvents CBTagNeedles As CheckBox
    Friend WithEvents GroupBox50 As GroupBox
    Friend WithEvents CBTagRimming As CheckBox
    Friend WithEvents CBTagFacesitting As CheckBox
    Friend WithEvents CBTagMissionary As CheckBox
    Friend WithEvents CBTagMasturbation As CheckBox
    Friend WithEvents CBTagRCowgirl As CheckBox
    Friend WithEvents CBTagFingering As CheckBox
    Friend WithEvents CBTagGangbang As CheckBox
    Friend WithEvents CBTagBlowjob As CheckBox
    Friend WithEvents CBTagDP As CheckBox
    Friend WithEvents CBTagHandjob As CheckBox
    Friend WithEvents CBTagStanding As CheckBox
    Friend WithEvents CBTagFootjob As CheckBox
    Friend WithEvents CBTagCowgirl As CheckBox
    Friend WithEvents CBTagDoggyStyle As CheckBox
    Friend WithEvents CBTagTitjob As CheckBox
    Friend WithEvents CBTagCunnilingus As CheckBox
    Friend WithEvents CBTagAnalSex As CheckBox
    Friend WithEvents GroupBox48 As GroupBox
    Friend WithEvents CBTagArtwork As CheckBox
    Friend WithEvents CBTagOutdoors As CheckBox
    Friend WithEvents CBTagPOV As CheckBox
    Friend WithEvents CBTagHardcore As CheckBox
    Friend WithEvents CBTagTD As CheckBox
    Friend WithEvents CBTagGay As CheckBox
    Friend WithEvents CBTagBath As CheckBox
    Friend WithEvents CBTagBisexual As CheckBox
    Friend WithEvents CBTagCFNM As CheckBox
    Friend WithEvents CBTagLesbian As CheckBox
    Friend WithEvents CBTagSoloFuta As CheckBox
    Friend WithEvents CBTagSM As CheckBox
    Friend WithEvents CBTagBondage As CheckBox
    Friend WithEvents CBTagSoloM As CheckBox
    Friend WithEvents CBTagSoloF As CheckBox
    Friend WithEvents CBTagChastity As CheckBox
    Friend WithEvents CBTagShower As CheckBox
    Friend WithEvents FileTagPreviousButton As Button
    Friend WithEvents FileTagNextButton As Button
    Friend WithEvents LBLLocalTagCount As Label
    Friend WithEvents SaveTagButton As Button
    Friend WithEvents CBLockOrgasmChances As CheckBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GenreDropDownLabel As Label
    Friend WithEvents GenreCombo As ComboBox
    Friend WithEvents FileTagCombo As ComboBox
    Friend WithEvents LocalTagImageNavGroup As GroupBox
    Friend WithEvents LocalTagPictureBox As PictureBox
    Friend WithEvents ScriptInfoPanel As Panel
    Friend WithEvents ScriptNavPanel As Panel
    Friend WithEvents SelectBlogDropDown As ComboBox
    Friend WithEvents VideoSettingsPanel As Panel
    Friend WithEvents VideoHeaderPanel As Panel
    Friend WithEvents VideoHeaderLabel As Label
    Friend WithEvents VideoRefreshButton As Button
    Friend WithEvents VideoLogo As PictureBox
    Friend WithEvents VideoDommeGeneralGroupBox As GroupBox
    Friend WithEvents VideoTotalDommeGeneral As Label
    Friend WithEvents VideoDommeGeneralPathTextBox As TextBox
    Friend WithEvents BTNVideoGeneralD As Button
    Friend WithEvents CBVideoGeneralD As CheckBox
    Friend WithEvents GbxVideoSpecialD As GroupBox
    Friend WithEvents LblVideoCHTotalD As Label
    Friend WithEvents LblVideoJOITotalD As Label
    Friend WithEvents TxbVideoCHD As TextBox
    Friend WithEvents TxbVideoJOID As TextBox
    Friend WithEvents BTNVideoCHD As Button
    Friend WithEvents BTNVideoJOID As Button
    Friend WithEvents CBVideoJOID As CheckBox
    Friend WithEvents CBVideoCHD As CheckBox
    Friend WithEvents GbxVideoGenreD As GroupBox
    Friend WithEvents LblVideoFemsubTotalD As Label
    Friend WithEvents TxbVideoFemsubD As TextBox
    Friend WithEvents LblVideoFemdomTotalD As Label
    Friend WithEvents TxbVideoFemdomD As TextBox
    Friend WithEvents TxbVideoBlowjobD As TextBox
    Friend WithEvents LblVideoBlowjobTotalD As Label
    Friend WithEvents TxbVideoLesbianD As TextBox
    Friend WithEvents TxbVideoSoftCoreD As TextBox
    Friend WithEvents LblVideoLesbianTotalD As Label
    Friend WithEvents TxbVideoHardCoreD As TextBox
    Friend WithEvents BTNVideoFemSubD As Button
    Friend WithEvents LblVideoSoftCoreTotalD As Label
    Friend WithEvents BTNVideoFemDomD As Button
    Friend WithEvents BTNVideoBlowjobD As Button
    Friend WithEvents LblVideoHardCoreTotalD As Label
    Friend WithEvents BTNVideoLesbianD As Button
    Friend WithEvents BTNVideoSoftCoreD As Button
    Friend WithEvents BTNVideoHardCoreD As Button
    Friend WithEvents CBVideoHardcoreD As CheckBox
    Friend WithEvents CBVideoSoftCoreD As CheckBox
    Friend WithEvents CBVideoLesbianD As CheckBox
    Friend WithEvents CBVideoBlowjobD As CheckBox
    Friend WithEvents CBVideoFemsubD As CheckBox
    Friend WithEvents CBVideoFemdomD As CheckBox
    Friend WithEvents VideoDescriptionGroupBox As GroupBox
    Friend WithEvents VideoDescriptionLabel As Label
    Friend WithEvents VideoGeneralGroupBox As GroupBox
    Friend WithEvents LblVideoGeneralTotal As Label
    Friend WithEvents TxbVideoGeneral As TextBox
    Friend WithEvents BTNVideoGeneral As Button
    Friend WithEvents CBVideoGeneral As CheckBox
    Friend WithEvents VideoSpecialGroupBox As GroupBox
    Friend WithEvents LblVideoCHTotal As Label
    Friend WithEvents LblVideoJOITotal As Label
    Friend WithEvents TxbVideoCH As TextBox
    Friend WithEvents TxbVideoJOI As TextBox
    Friend WithEvents BTNVideoCH As Button
    Friend WithEvents BTNVideoJOI As Button
    Friend WithEvents CBVideoJOI As CheckBox
    Friend WithEvents CBVideoCH As CheckBox
    Friend WithEvents VideoGenreGroupBox As GroupBox
    Friend WithEvents LblVideoFemsubTotal As Label
    Friend WithEvents TxbVideoFemsub As TextBox
    Friend WithEvents LblVideoFemdomTotal As Label
    Friend WithEvents TxbVideoFemdom As TextBox
    Friend WithEvents TxbVideoBlowjob As TextBox
    Friend WithEvents LblVideoBlowjobTotal As Label
    Friend WithEvents TxbVideoLesbian As TextBox
    Friend WithEvents TxbVideoSoftCore As TextBox
    Friend WithEvents LblVideoLesbianTotal As Label
    Friend WithEvents VideoHardCorePathTextBox As TextBox
    Friend WithEvents BTNVideoFemSub As Button
    Friend WithEvents LblVideoSoftCoreTotal As Label
    Friend WithEvents BTNVideoFemDom As Button
    Friend WithEvents BTNVideoBlowjob As Button
    Friend WithEvents LblVideoHardCoreTotal As Label
    Friend WithEvents BTNVideoLesbian As Button
    Friend WithEvents BTNVideoSoftCore As Button
    Friend WithEvents VideoSetHardcorePathButton As Button
    Friend WithEvents VideoEnableHardcoreCheckBox As CheckBox
    Friend WithEvents VideoEnableSoftcoreCheckBox As CheckBox
    Friend WithEvents CBVideoLesbian As CheckBox
    Friend WithEvents CBVideoBlowjob As CheckBox
    Friend WithEvents CBVideoFemsub As CheckBox
    Friend WithEvents CBVideoFemdom As CheckBox
    Friend WithEvents VideoLayoutTable As TableLayoutPanel
    Friend WithEvents VideoGeneralPanel As Panel
    Friend WithEvents VideoDommePanel As Panel
    Friend WithEvents RangeSettingsHeaderPanel As Panel
    Friend WithEvents RangeSettingsBodyTablePanel As TableLayoutPanel
    Friend WithEvents RangeSettingsBodyRightColumnPanel As Panel
    Friend WithEvents RangeSettingsBodyMiddleColumnPanel As Panel
    Friend WithEvents RangeSettingsBodyLeftColumnPanel As Panel
    Friend WithEvents GreenLightMaximumSeconds As NumericUpDown
    Friend WithEvents GreenLightMinimumSeconds As NumericUpDown
    Friend WithEvents RedLightMaximumSeconds As NumericUpDown
    Friend WithEvents RedLightMinimumSeconds As NumericUpDown
    Friend WithEvents DommeSettingsDescriptionGroupBox As GroupBox
    Friend WithEvents DommeSettingsHeaderPanel As Panel
    Friend WithEvents DommeSettingsBodyPanel As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox39 As GroupBox
    Private WithEvents CBHonorificInclude As CheckBox
    Private WithEvents CBHonorificCapitalized As CheckBox
    Private WithEvents TBHonorific As TextBox
    Friend WithEvents DommeSettingsDescriptionLabel As Label
    Friend WithEvents AppsSettingsHeaderPanel As Panel
    Friend WithEvents AppsSettingsLogo As PictureBox
    Friend WithEvents AppsSettingsHeaderLabel As Label
    Friend WithEvents AppsSettingsLoad As Button
    Friend WithEvents AppsSettingsSave As Button
    Friend WithEvents DommeGlitterSettings As GlitterSettingsControl
    Friend WithEvents SettingsHeader As SettingsHeaderControl
    Friend WithEvents SettingsDescriptionControl As SettingsDescriptionControl
End Class
