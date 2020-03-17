Option Strict On
Option Infer Off
Imports System.Threading.Tasks
Imports TeaseAI.Common.Interfaces

Public Class NotifyUser
    Implements INotifyUser

    Public Sub ModalMessage(message As String) Implements INotifyUser.ModalMessage
        MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function ModalMessageAsync(message As String) As Task Implements INotifyUser.ModalMessageAsync
        Throw New NotImplementedException()
    End Function
End Class
