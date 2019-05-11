Imports TeaseAI.Common
Imports TeaseAI.Common.Interfaces.Accessors

Public Class SystemVocabularyAccessor
    Implements ISystemVocabularyAccessor

    Public Function GetData(session As Session, key As String) As Result(Of List(Of String)) Implements ISystemVocabularyAccessor.GetData
        Dim fileName = GetFileName(session.Domme, key)
        If (System.IO.File.Exists(fileName)) Then

            Return Result.Ok(System.IO.File.ReadAllLines(fileName).ToList())
        End If
        Result.Fail(Of List(Of String))("Unable to find the file for " + key)
    End Function

    Private Function GetFileName(domme As DommePersonality, variableName As String) As String
        'C:\source\TeaseAI\Tease AI\bin\Debug\Scripts\new-wicked-tease\Vocabulary\Responses\System
        Return Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\Vocabulary\Responses\System\" + variableName + ".txt"
    End Function
End Class
