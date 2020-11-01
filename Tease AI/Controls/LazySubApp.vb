Option Explicit On
Option Strict On
Imports TeaseAI.Common
Imports TeaseAI.Common.Events
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors

Public Class LazySubApp
    Public Property LabelColor As Color
        Get
            Return EnableShortcutsCheckbox.ForeColor
        End Get
        Set(value As Color)
            EnableShortcutsCheckbox.ForeColor = value
            HideShortcutsCheckBox.ForeColor = value
        End Set
    End Property

    Public Property ButtonForegroundColor As Color
        Get
            Return YesButton.ForeColor
        End Get
        Set(value As Color)
            YesButton.ForeColor = value
            YesButton.FlatAppearance.BorderColor = value

            NoButton.ForeColor = value
            NoButton.FlatAppearance.BorderColor = value

            OnTheEdgeButton.ForeColor = value
            OnTheEdgeButton.FlatAppearance.BorderColor = value

            SpeedUpButton.ForeColor = value
            SpeedUpButton.FlatAppearance.BorderColor = value

            SlowDownButton.ForeColor = value
            SlowDownButton.FlatAppearance.BorderColor = value

            StopButton.ForeColor = value
            StopButton.FlatAppearance.BorderColor = value

            StrokeButton.ForeColor = value
            StrokeButton.FlatAppearance.BorderColor = value

            LetMeCumButton.ForeColor = value
            LetMeCumButton.FlatAppearance.BorderColor = value

            GreetingButton.ForeColor = value
            GreetingButton.FlatAppearance.BorderColor = value

            SafewordButton.ForeColor = value
            SafewordButton.FlatAppearance.BorderColor = value

            CustomOneButton.ForeColor = value
            CustomOneButton.FlatAppearance.BorderColor = value

            CustomOneEditButton.ForeColor = value
            CustomOneEditButton.FlatAppearance.BorderColor = value

            CustomTwoButton.ForeColor = value
            CustomTwoButton.FlatAppearance.BorderColor = value

            CustomTwoEditButton.ForeColor = value
            CustomTwoEditButton.FlatAppearance.BorderColor = value

            CustomThreeButton.ForeColor = value
            CustomThreeButton.FlatAppearance.BorderColor = value

            CustomThreeEditButton.ForeColor = value
            CustomThreeEditButton.FlatAppearance.BorderColor = value

            CustomFourButton.ForeColor = value
            CustomFourButton.FlatAppearance.BorderColor = value

            CustomFourEditButton.ForeColor = value
            CustomFourEditButton.FlatAppearance.BorderColor = value

            CustomFiveButton.ForeColor = value
            CustomFiveButton.FlatAppearance.BorderColor = value

            CustomFiveEditButton.ForeColor = value
            CustomFiveEditButton.FlatAppearance.BorderColor = value
        End Set
    End Property

    Public Property ButtonBackgroundColor As Color
        Get
            Return YesButton.BackColor
        End Get
        Set(value As Color)
            YesButton.BackColor = value
            NoButton.BackColor = value
            OnTheEdgeButton.BackColor = value
            SpeedUpButton.BackColor = value
            SlowDownButton.BackColor = value
            StopButton.BackColor = value
            StrokeButton.BackColor = value
            LetMeCumButton.BackColor = value
            GreetingButton.BackColor = value
            SafewordButton.BackColor = value
            CustomOneButton.BackColor = value
            CustomOneEditButton.BackColor = value
            CustomTwoButton.BackColor = value
            CustomTwoEditButton.BackColor = value
            CustomThreeButton.BackColor = value
            CustomThreeEditButton.BackColor = value
            CustomFourButton.BackColor = value
            CustomFourEditButton.BackColor = value
            CustomFiveButton.BackColor = value
            CustomFiveEditButton.BackColor = value
        End Set
    End Property

    Public Event SendMessage(sender As Object, e As SendMessageEventArgs)

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mySettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
        myLazySubStatementLogic = ApplicationFactory.CreateLazySubStatementsService()


        ' TODO: Need migrated
        'Me.PNLLazySub.Controls.Add(Me.TBShortSafeword)
        'Me.PNLLazySub.Controls.Add(Me.TBShortGreet)
        'Me.PNLLazySub.Controls.Add(Me.TBShortCum)
        'Me.PNLLazySub.Controls.Add(Me.TBShortStroke)
        'Me.PNLLazySub.Controls.Add(Me.TBShortStop)
        'Me.PNLLazySub.Controls.Add(Me.TBShortSlowDown)
        'Me.PNLLazySub.Controls.Add(Me.TBShortSpeedUp)
        'Me.PNLLazySub.Controls.Add(Me.TBShortEdge)
        'Me.PNLLazySub.Controls.Add(Me.TBShortNo)
        'Me.PNLLazySub.Controls.Add(Me.TBShortYes)
    End Sub

    Protected Sub OnSendMessage(messageString As String, sender As String)

        Dim chatMessage As ChatMessage = New ChatMessage With {
            .Message = messageString,
            .Sender = sender
        }

        Dim EventArgs As SendMessageEventArgs = New SendMessageEventArgs With {
            .ChatMessage = chatMessage
        }

        RaiseEvent SendMessage(Me, EventArgs)
    End Sub

    Protected Function GetHonorific(dommeSettings As DommeSettings) As String
        If dommeSettings.RequiresHonorific AndAlso dommeSettings.RequiresHonorificCapitalized Then
            Return " " + dommeSettings.Honorific.Replace(dommeSettings.Honorific(0), dommeSettings.Honorific(0).ToString().ToUpper()(0))
        ElseIf dommeSettings.RequiresHonorific Then
            Return " " + dommeSettings.Honorific
        End If
        Return String.Empty
    End Function

    Private ReadOnly mySettingsAccessor As ISettingsAccessor
    Private ReadOnly myLazySubStatementLogic As ILazySubStatementLogic

#Region "Event handlers"
    Private Sub LazySubApp_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Dim settings As LazySubSettings = mySettingsAccessor.GetSettings().Apps.LazySub
        CustomOneButton.Text = settings.CustomTextOne
        CustomTwoButton.Text = settings.CustomTextTwo
        CustomThreeButton.Text = settings.CustomTextThree
        CustomFourButton.Text = settings.CustomTextFour
        CustomFiveButton.Text = settings.CustomTextFive
        YesShortcutBox.Text = settings.YesShortCut
        NoShortcutBox.Text = settings.NoShortCut
        OnTheEdgeShortcutBox.Text = settings.OnTheEdgeShortCut
        SpeedUpShortcutBox.Text = settings.SpeedUpShortCut
        SlowDownShortcutBox.Text = settings.SlowDownShortCut
        StrokeShortcutBox.Text = settings.StrokeShortCut
        StopShortcutBox.Text = settings.StopShortCut
        LetMeCumShortcutBox.Text = settings.LetMeCumShortCut
        GreetingShortcutBox.Text = settings.GreetingShortCut
        SafewordShortcutBox.Text = settings.SafewordShortCut
    End Sub

    Private Sub HideShortcutsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles HideShortcutsCheckBox.CheckedChanged
        Dim areShortcutsVisible As Boolean = Not HideShortcutsCheckBox.Checked
        Dim buttonWidth As Integer = Size.Width - CustomOneButton.Margin.Left - CustomOneButton.Margin.Right
        If areShortcutsVisible Then
            buttonWidth = buttonWidth - CustomOneEditButton.Size.Width - CustomOneEditButton.Margin.Left - CustomOneEditButton.Margin.Right
        End If

        YesShortcutBox.Visible = areShortcutsVisible
        NoShortcutBox.Visible = areShortcutsVisible
        OnTheEdgeShortcutBox.Visible = areShortcutsVisible
        SpeedUpShortcutBox.Visible = areShortcutsVisible
        SlowDownShortcutBox.Visible = areShortcutsVisible
        StrokeShortcutBox.Visible = areShortcutsVisible
        StopShortcutBox.Visible = areShortcutsVisible
        LetMeCumShortcutBox.Visible = areShortcutsVisible
        GreetingShortcutBox.Visible = areShortcutsVisible
        SafewordShortcutBox.Visible = areShortcutsVisible

        CustomOneEditButton.Visible = areShortcutsVisible
        CustomOneButton.Width = buttonWidth
        CustomTwoEditButton.Visible = areShortcutsVisible
        CustomTwoButton.Width = buttonWidth
        CustomThreeEditButton.Visible = areShortcutsVisible
        CustomThreeButton.Width = buttonWidth
        CustomFourEditButton.Visible = areShortcutsVisible
        CustomFourButton.Width = buttonWidth
        CustomFiveEditButton.Visible = areShortcutsVisible
        CustomFiveButton.Width = buttonWidth
    End Sub

    Private Sub EnableShortcutsCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles EnableShortcutsCheckbox.CheckedChanged
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.AreShortcutsEnabled = EnableShortcutsCheckbox.Checked
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub YesButton_Click(sender As Object, e As EventArgs) Handles YesButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        Dim messageString As String = myLazySubStatementLogic.GetAffirmative(settings)
        OnSendMessage(messageString, settings.Sub.Name)
    End Sub

    Private Sub YesShortcutBox_Leave(sender As Object, e As EventArgs) Handles YesShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.YesShortCut = YesShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub NoButton_Click(sender As Object, e As EventArgs) Handles NoButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        Dim messageString As String = myLazySubStatementLogic.GetNegative(settings)
        OnSendMessage(messageString, settings.Sub.Name)
    End Sub

    Private Sub NoShortcutBox_Leave(sender As Object, e As EventArgs) Handles NoShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.NoShortCut = NoShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub OnTheEdgeButton_Click(sender As Object, e As EventArgs) Handles OnTheEdgeButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetOnTheEdge(settings), settings.Sub.Name)
    End Sub

    Private Sub OnTheEdgeShortcutBox_Leave(sender As Object, e As EventArgs) Handles OnTheEdgeShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.OnTheEdgeShortCut = OnTheEdgeShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub SpeedUpButton_Click(sender As Object, e As EventArgs) Handles SpeedUpButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetSpeedUp(settings), settings.Sub.Name)
    End Sub

    Private Sub SpeedUpShortcutBox_Leave(sender As Object, e As EventArgs) Handles SpeedUpShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.SpeedUpShortCut = SpeedUpShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub SlowDownButton_Click(sender As Object, e As EventArgs) Handles SlowDownButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetSlowDown(settings), settings.Sub.Name)
    End Sub

    Private Sub SlowDownShortcutBox_Leave(sender As Object, e As EventArgs) Handles SlowDownShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.SlowDownShortCut = SlowDownShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub StrokeButton_Click(sender As Object, e As EventArgs) Handles StrokeButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetStroke(settings), settings.Sub.Name)
    End Sub

    Private Sub StrokeShortcutBox_Leave(sender As Object, e As EventArgs) Handles StrokeShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.StrokeShortCut = StrokeShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetStop(settings), settings.Sub.Name)
    End Sub

    Private Sub StopShortcutBox_Leave(sender As Object, e As EventArgs) Handles StopShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.StopShortCut = StopShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub LetMeCumButton_Click(sender As Object, e As EventArgs) Handles LetMeCumButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetLetMeCum(settings), settings.Sub.Name)
    End Sub

    Private Sub LetMeCumShortcutBox_Leave(sender As Object, e As EventArgs) Handles LetMeCumShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.LetMeCumShortCut = LetMeCumShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub GreetingButton_Click(sender As Object, e As EventArgs) Handles GreetingButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        Dim messageString As String = myLazySubStatementLogic.GetGreeting(settings)

        OnSendMessage(messageString, settings.Sub.Name)
    End Sub

    Private Sub GreetingShortcutBox_Leave(sender As Object, e As EventArgs) Handles GreetingShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.GreetingShortCut = GreetingShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub

    Private Sub SafewordButton_Click(sender As Object, e As EventArgs) Handles SafewordButton.Click
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        OnSendMessage(myLazySubStatementLogic.GetSafeword(settings), settings.Sub.Name)
    End Sub

    Private Sub SafewordShortcutBox_Leave(sender As Object, e As EventArgs) Handles SafewordShortcutBox.Leave
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        settings.Apps.LazySub.SafewordShortCut = SafewordShortcutBox.Text
        mySettingsAccessor.WriteSettings(settings)
    End Sub
#End Region
End Class
