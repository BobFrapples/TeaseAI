Option Strict On
Option Infer Off
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class GlitterApp
    Public Sub New()
        mySettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
        InitializeComponent()

    End Sub

    Public Sub AddMessage(sender As DommeSettings, statusText As String)
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        Dim imageUrl As String = ("file://" & sender.AvatarImageFile).Replace("\", "/")
        Dim statusHtml As String = BuildGlitterMessage(sender, statusText, "black")

        Me.GlitterHistory.DocumentText &= statusHtml
    End Sub

    Private Shared Function BuildGlitterMessage(sender As DommeSettings, messageText As String, messageColor As String) As String
        Dim glitterImage As String = ("file://" & sender.AvatarImageFile).Replace("\", "/")

        Dim message As String = "<img class=""floatright"" style="" float: left; width: 32; height: 32; border: 0;"" src=""" _
            & glitterImage & """> <font face=""Cambria"" size=""3"" color=""" & sender.ChatColor & """><b>" & sender.GlitterContactName _
            & "</b></font><br> <font face=""Cambria"" size=""2"" color=""DarkGray"">" & Date.Today & "</font><br>" & "<font face=""Cambria"" size=""2"" color=""" _
            & messageColor & """>" & messageText & "</font><br><br>"
        Return message
    End Function

    Private ReadOnly mySettingsAccessor As ISettingsAccessor

    'Private Sub StatusUpdates_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles GlitterWindow.DocumentCompleted
    '    Try
    '        'GlitterWindow.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue)
    '    Catch
    '    End Try
    'End Sub
End Class
