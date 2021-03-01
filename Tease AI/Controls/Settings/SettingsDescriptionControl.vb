Public Class SettingsDescriptionControl
    Public Property DescriptionText As String
        Get
            Return AppsSettingsDescriptionLabel.Text
        End Get
        Set(ByVal value As String)
            AppsSettingsDescriptionLabel.Text = value
        End Set
    End Property
End Class
