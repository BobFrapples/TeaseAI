Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

<Designer(GetType(RandomizerAppDesigner), GetType(IRootDesigner))>
Public Class RandomizerApp

    Public Property LabelColor As Color
        Get
            Return MediaLabel.ForeColor
        End Get
        Set(value As Color)
            MediaLabel.ForeColor = value
            SpecialVideoLabel.ForeColor = value
            VideoTeaseLabel.ForeColor = value
        End Set
    End Property

    Public Property ButtonForegroundColor As Color
        Get
            Return AvoidTheEdgeRandomizerButton.ForeColor
        End Get
        Set(value As Color)
            BlogImageRandomizerButton.ForeColor = value
            LocalImageRandomizerButton.ForeColor = value
            VideoRandomizerButton.ForeColor = value

            JerkOffInstructionsRandomizerButton.ForeColor = value
            CockHeroRandomizerButton.ForeColor = value

            AvoidTheEdgeRandomizerButton.ForeColor = value
            CensorshipSucksRandomizerButton.ForeColor = value
            RedLightGreenLightRandomizerButton.ForeColor = value
        End Set
    End Property

    Public Property ButtonBackgroundColor As Color
        Get
            Return AvoidTheEdgeRandomizerButton.BackColor
        End Get
        Set(value As Color)
            BlogImageRandomizerButton.BackColor = value
            LocalImageRandomizerButton.BackColor = value
            VideoRandomizerButton.BackColor = value

            JerkOffInstructionsRandomizerButton.BackColor = value
            CockHeroRandomizerButton.BackColor = value

            AvoidTheEdgeRandomizerButton.BackColor = value
            CensorshipSucksRandomizerButton.BackColor = value
            RedLightGreenLightRandomizerButton.BackColor = value
        End Set
    End Property

    Public Event BlogImageRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event LocalImageRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event VideoRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event JerkOffInstructionsRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event CockHeroRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event AvoidTheEdgeRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event CensorshipSucksRandomizerButton_Clicked(sender As Object, e As EventArgs)
    Public Event RedLightGreenLightRandomizerButton_Clicked(sender As Object, e As EventArgs)

    Private Sub BlogImageRandomizerButton_Click(sender As Object, e As EventArgs) Handles BlogImageRandomizerButton.Click
        RaiseEvent BlogImageRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub LocalImageRandomizerButton_Click(sender As Object, e As EventArgs) Handles LocalImageRandomizerButton.Click
        RaiseEvent LocalImageRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub VideoRandomizerButton_Click(sender As Object, e As EventArgs) Handles VideoRandomizerButton.Click
        RaiseEvent VideoRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub JerkOffInstructionsRandomizerButton_Click(sender As Object, e As EventArgs) Handles JerkOffInstructionsRandomizerButton.Click
        RaiseEvent JerkOffInstructionsRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub CockHeroRandomizerButton_Click(sender As Object, e As EventArgs) Handles CockHeroRandomizerButton.Click
        RaiseEvent CockHeroRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub AvoidTheEdgeRandomizerButton_Click(sender As Object, e As EventArgs) Handles AvoidTheEdgeRandomizerButton.Click
        RaiseEvent AvoidTheEdgeRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub CensorshipSucksRandomizerButton_Click(sender As Object, e As EventArgs) Handles CensorshipSucksRandomizerButton.Click
        RaiseEvent CensorshipSucksRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

    Private Sub RedLightGreenLightRandomizerButton_Click(sender As Object, e As EventArgs) Handles RedLightGreenLightRandomizerButton.Click
        RaiseEvent RedLightGreenLightRandomizerButton_Clicked(Me, New EventArgs())
    End Sub

End Class
