Option Strict On
Option Infer Off
Public Class GlitterSettingsControl

    Public Event ShowDescription(sender As Object, e As ShowDescriptionEventArgs)

    Public Sub OnShowDescription(name As String, description As String)
        Dim showDescriptionEventArgs As ShowDescriptionEventArgs = New ShowDescriptionEventArgs
        showDescriptionEventArgs.Name = name
        showDescriptionEventArgs.Description = description

        RaiseEvent ShowDescription(Me, showDescriptionEventArgs)
    End Sub

    Public Event GlitterChanged(sender As Object, e As EventArgs)

    Public Sub OnGlitterChanged()
        Dim eventArgs As EventArgs = New EventArgs

        RaiseEvent GlitterChanged(Me, eventArgs)
    End Sub

#Region "Properties"
    ''' <summary>
    ''' Set the label for this Control
    ''' </summary>
    ''' <returns></returns>
    Public Property GlitterLabel As String
        Get
            Return GlitterSettingsGroupBox.Text
        End Get
        Set(ByVal value As String)
            GlitterSettingsGroupBox.Text = value
        End Set
    End Property

    Public Property ChatColor As String
        Get
            Return ColorTranslator.ToHtml(GlitterChatPreview.ForeColor)
        End Get
        Set(value As String)
            GlitterChatPreview.ForeColor = ColorTranslator.FromHtml(value)
        End Set
    End Property

    Public Property GlitterContactName As String
        Get
            Return GlitterContactNameTextBox.Text
        End Get
        Set(ByVal value As String)
            GlitterContactNameTextBox.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Set the label for the enabled GroupBox
    ''' </summary>
    ''' <returns></returns>
    Public Property EnabledLabel As String
        Get
            Return GlitterFeedGroupBox.Text
        End Get
        Set(value As String)
            GlitterFeedGroupBox.Text = value
        End Set
    End Property
    Public Property IsGlitterEnabled As Boolean
        Get
            Return GlitterFeedOn.Checked
        End Get
        Set(ByVal value As Boolean)
            GlitterFeedOn.Checked = value
        End Set
    End Property

    Public Property IsScriptsOptionEnabled As Boolean
        Get
            Return GlitterFeedScripts.Visible
        End Get
        Set(ByVal value As Boolean)
            GlitterFeedScripts.Visible = value
        End Set
    End Property

    Public Property PostFrequency As Integer
        Get
            Return PostFrequencySlider.Value
        End Get
        Set(ByVal value As Integer)
            PostFrequencySlider.Value = value
        End Set
    End Property

    Public Property ResponseFrequency As Integer
        Get
            Return ResponseFrequencySlider.Value
        End Get
        Set(ByVal value As Integer)
            ResponseFrequencySlider.Value = value
        End Set
    End Property

    Public Property IsAngry As Boolean
        Get
            Return AngryCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            AngryCheckBox.Checked = value
        End Set
    End Property

    Public Property IsBratty As Boolean
        Get
            Return BrattyCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            BrattyCheckBox.Checked = value
        End Set
    End Property

    Public Property IsCaring As Boolean
        Get
            Return CaringCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            CaringCheckBox.Checked = value
        End Set
    End Property

    Public Property IsCondescending As Boolean
        Get
            Return CondescendingCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            CondescendingCheckBox.Checked = value
        End Set
    End Property

    Public Property IsCrazy As Boolean
        Get
            Return CrazyCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            CrazyCheckBox.Checked = value
        End Set
    End Property

    Public Property IsCruel As Boolean
        Get
            Return CruelCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            CruelCheckBox.Checked = value
        End Set
    End Property

    Public Property IsDegrading As Boolean
        Get
            Return DegradingCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            DegradingCheckBox.Checked = value
        End Set
    End Property

    Public Property IsSadistic As Boolean
        Get
            Return SadisticCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            SadisticCheckBox.Checked = value
        End Set
    End Property

    Public Property IsSupremacist As Boolean
        Get
            Return SupremicistCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            SupremicistCheckBox.Checked = value
        End Set
    End Property

    Public Property IsVulgar As Boolean
        Get
            Return VulgarCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            VulgarCheckBox.Checked = value
        End Set
    End Property

    Public Property AvatarImageFile As String
        Get
            Return myAvatarImageFile
        End Get
        Set(ByVal value As String)
            myAvatarImageFile = value
            If Not String.IsNullOrWhiteSpace(value) Then
                GlitterAvatarImage.Image = Image.FromFile(myAvatarImageFile)
            End If
        End Set
    End Property

    Public Property IsTeaseModuleEnabled As Boolean
        Get
            Return TeaseModuleCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            TeaseModuleCheckBox.Checked = value
        End Set
    End Property

    Public Property IsEgotistModuleEnabled As Boolean

        Get
            Return EgotistModuleCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            EgotistModuleCheckBox.Checked = value
        End Set
    End Property

    Public Property IsTriviaModuleEnabled As Boolean
        Get
            Return TriviaModuleCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            TriviaModuleCheckBox.Checked = value
        End Set
    End Property

    Public Property IsDailyModuleEnabled As Boolean
        Get
            Return DailyModuleCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            DailyModuleCheckBox.Checked = value
        End Set
    End Property

    Public Property IsCustom1ModuleEnabled As Boolean
        Get
            Return CustomOneModuleCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            CustomOneModuleCheckBox.Checked = value
        End Set
    End Property

    Public Property IsCustom2ModuleEnabled As Boolean
        Get
            Return CustomTwoModuleCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            CustomTwoModuleCheckBox.Checked = value
        End Set
    End Property

    Public Property GlitterImageDirectory As String
        Get
            Return GlitterImageDirectoryTextBox.Text
        End Get
        Set(ByVal value As String)
            GlitterImageDirectoryTextBox.Text = value
        End Set
    End Property

#End Region

#Region "Event handling"
    Private Sub GlitterAvatarImage_Click(sender As Object, e As EventArgs) Handles GlitterAvatarImage.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            myAvatarImageFile = openFileDialog.FileName
            GlitterAvatarImage.Image = Image.FromFile(myAvatarImageFile)
            OnGlitterChanged()
        End If
    End Sub

    Private Sub GlitterNameColorButton_Click(sender As Object, e As EventArgs) Handles GlitterNameColorButton.Click
        Dim getColor As ColorDialog = New ColorDialog
        If getColor.ShowDialog() = DialogResult.OK Then
            GlitterChatPreview.ForeColor = getColor.Color
            OnGlitterChanged()
        End If
    End Sub

    Private Sub Controls_FireShowDescription(sender As Object, e As EventArgs) Handles GlitterAvatarImage.MouseEnter _
        , GlitterChatPreview.MouseEnter _
        , GlitterContactNameTextBox.MouseEnter _
        , GlitterFeedOffRadio.MouseEnter _
        , GlitterFeedOn.MouseEnter _
        , GlitterFeedScripts.MouseEnter _
        , GlitterImageDirectoryTextBox.MouseEnter _
        , GlitterNameColorButton.MouseEnter _
        , AngryCheckBox.MouseEnter _
        , BrattyCheckBox.MouseEnter _
        , CaringCheckBox.MouseEnter _
        , CondescendingCheckBox.MouseEnter _
        , CrazyCheckBox.MouseEnter _
        , CruelCheckBox.MouseEnter _
        , CustomOneModuleCheckBox.MouseEnter _
        , CustomTwoModuleCheckBox.MouseEnter _
        , DailyModuleCheckBox.MouseEnter _
        , VulgarCheckBox.MouseEnter
        OnShowDescription("Name", ToolTipData.GetToolTip(CType(sender, Control)))
    End Sub

    Private Sub ClearImageDirectoryButton_Click(sender As Object, e As EventArgs) Handles ClearImageDirectoryButton.Click
        GlitterImageDirectoryTextBox.Text = String.Empty
        OnGlitterChanged()
    End Sub
#End Region

    Private Property myAvatarImageFile As String

End Class
