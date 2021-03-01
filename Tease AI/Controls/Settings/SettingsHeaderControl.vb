Public Class SettingsHeaderControl
    Public Property SettingsTitle As String
        Get
            Return HeaderTextLabel.Text
        End Get
        Set(value As String)
            HeaderTextLabel.Text = value
        End Set
    End Property
End Class
