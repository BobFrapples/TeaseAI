Option Strict On
Option Infer Off
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class VocabularyAccessor
    Implements IVocabularyAccessor
'TODO: Implement filter
    Public Function GetVocabulary(domme As DommePersonality, keyword As String) As Result(Of List(Of String)) Implements IVocabularyAccessor.GetVocabulary

        Dim filepath As String = Application.StartupPath & "\Scripts\" & domme.PersonalityName & "\Vocabulary\" & keyword & ".txt"
        If (Not File.Exists(filepath)) Then
            Return Result.Fail(Of List(Of String))("Unable to find vocabulary for " + keyword)
        End If

        Return Result.Ok(File.ReadAllLines(filepath).ToList())
    End Function
End Class

