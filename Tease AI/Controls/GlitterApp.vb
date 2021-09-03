Option Strict On
Option Infer Off
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class GlitterApp
    Public Sub New()
        InitializeComponent()
        mySettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
        myGlitterMessages = New List(Of String)
        GlitterBaseHtml = "<html>
<head>
  <style>
    div {{ margin-bottom:2px; border: 1px solid black }}
  </style>
</head>
<body>{0}</body>
<html>"
        GlitterHistory.Navigate("about:blank")
    End Sub

    Public Sub AddMessage(sender As DommeSettings, statusText As String)
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        Dim imageUrl As String = ("file:///" & sender.AvatarImageFile).Replace("\", "/")
        myGlitterMessages.Add(BuildGlitterMessage(sender, statusText, "black"))
        Dim bodyHtml As String = String.Join(Environment.NewLine, myGlitterMessages)
        Me.GlitterHistory.DocumentText = String.Format(GlitterBaseHtml, bodyHtml)
    End Sub

    Private Shared Function BuildGlitterMessage(sender As DommeSettings, messageText As String, messageColor As String) As String
        Dim glitterImage As String = ("file:///" & sender.AvatarImageFile).Replace("\", "/")

        Dim message As String = "<div><img class=""floatright"" style="" float: left; width: 32; height: 32; border: 0;"" src=""" _
            & glitterImage & """> <font face=""Cambria"" size=""3"" color=""" & sender.ChatColor & """><b>" & sender.GlitterContactName _
            & "</b></font><br> <font face=""Cambria"" size=""2"" color=""DarkGray"">" & Date.Today & "</font><br>" & "<font face=""Cambria"" size=""2"" color=""" _
            & messageColor & """>" & messageText & "</font></div>" & Environment.NewLine
        Return message
    End Function

    Private Sub StatusUpdates_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles GlitterHistory.DocumentCompleted
        Try
            GlitterHistory.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue)
        Catch ex As Exception
            Dim i As Integer = 3
        End Try
    End Sub

    Dim GlitterBaseHtml As String
    Dim myGlitterMessages As List(Of String)
    Private ReadOnly mySettingsAccessor As ISettingsAccessor
End Class
