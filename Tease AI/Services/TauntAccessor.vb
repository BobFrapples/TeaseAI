Option Strict On
Option Infer Off
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class TauntAccessor
    Implements ITauntAccessor

    Public Function GetVocabulary(session As Session, keyword As String) As Result(Of List(Of String)) Implements ITauntAccessor.GetTaunt

        Dim filePath As String = Application.StartupPath & "\Scripts\" & session.Domme.PersonalityName & "\Stroke\Edge\Edge.txt"
        'If session.GlitterTease Then
        'filePath = Application.StartupPath & "\Scripts\" & session.Domme.PersonalityName & "\Stroke\Edge\GroupEdge.txt"
        'End If
        If (Not File.Exists(filePath)) Then
            Return Result.Fail(Of List(Of String))("Unable to find vocabulary for " + keyword)
        End If

        Return Result.Ok(File.ReadAllLines(filePath).ToList())
    End Function
End Class

