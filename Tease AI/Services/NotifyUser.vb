Option Strict On
Option Infer Off
Imports TeaseAI.Common.Interfaces

Public Class NotifyUser
    Implements INotifyUser

    Public Sub ModalMessage(message As String) Implements INotifyUser.ModalMessage
        MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
