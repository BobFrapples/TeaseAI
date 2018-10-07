Option Strict On
Option Infer Off
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class FlagAccessor
    Implements IFlagAccessor

    Public Sub SetFlag(domme As DommePersonality, flagName As String, isTemp As Boolean) Implements IFlagAccessor.SetFlag
        If (isTemp) Then
            Using fs As New FileStream(Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\System\Flags\Temp\" + flagName, FileMode.Create) : End Using
        Else
            Using fs As New FileStream(Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\System\Flags\" + flagName, FileMode.Create) : End Using
        End If
    End Sub

    Public Function IsSet(domme As DommePersonality, flagName As String) As Boolean Implements IFlagAccessor.IsSet
        Return File.Exists(Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\System\Flags\" + flagName) OrElse
            File.Exists(Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\System\Flags\Temp\" + flagName)
    End Function

    Public Sub DeleteFlag(domme As DommePersonality, flagName As String) Implements IFlagAccessor.DeleteFlag
        Dim myBaseDir As String = Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\System\Flags\"
        If File.Exists(myBaseDir + flagName) Then _
            File.Delete(myBaseDir + flagName)

        If File.Exists(myBaseDir + "Temp\" + flagName) Then _
            File.Delete(myBaseDir + "Temp\" + flagName)
    End Sub
End Class
