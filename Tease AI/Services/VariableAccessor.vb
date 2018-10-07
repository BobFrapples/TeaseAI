Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class VariableAccessor
    Implements IVariableAccessor

    Public Function DoesExist(domme As DommePersonality, variableName As String) As Boolean Implements IVariableAccessor.DoesExist
        Return File.Exists(GetFileName(domme, variableName))
    End Function

    Public Function GetVariable(domme As DommePersonality, variableName As String) As Result(Of String) Implements IVariableAccessor.GetVariable
        If (DoesExist(domme, variableName)) Then
            Dim data As String = File.ReadAllText(GetFileName(domme, variableName))
            Return Result.Ok(data)
        End If
        Return Result.Fail(Of String)(variableName + " is not an existing variable")
    End Function

    Public Function SetVariable(domme As DommePersonality, variableName As String, value As String) As Result Implements IVariableAccessor.SetVariable
        Dim fileName = GetFileName(domme, variableName)
        File.WriteAllText(fileName, value)
        Return Result.Ok()
    End Function

    Private Function GetFileName(domme As DommePersonality, variableName As String) As String
        Return Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\System\Variables\" + variableName
    End Function
End Class
