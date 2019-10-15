Option Explicit On
Option Strict On

Public Class TranslationService
    Public Function GetString(languageCode As String, key As String) As String
        If languageCode = "en" Then
            Return CType(IIf(English.ContainsKey(key), English(key), String.Empty), String)
        ElseIf languageCode = "de" Then
            Return CType(IIf(German.ContainsKey(key), German(key), String.Empty), String)
        End If
        Throw New Exception(languageCode & " is unknown.")
    End Function

    Private English As Dictionary(Of String, String) = CreateEnglish()

    Private German As Dictionary(Of String, String) = CreateGerman()

    Private Function CreateEnglish() As Dictionary(Of String, String)
        Dim returnValue As Dictionary(Of String, String) = New Dictionary(Of String, String)()

        returnValue("TimeStampCheckBox") = "When this is selected, a timestamp will appear" & Environment.NewLine & "with each message you and the domme send."
        returnValue("ShowNamesCheckBox") = "When this is selected, the names of you and the" & Environment.NewLine & "domme will appear with every message you send." & Environment.NewLine & Environment.NewLine & "If it is unselected, names will only appear" & Environment.NewLine & "when you were not the last one to type."

        Return returnValue
    End Function

    Private Function CreateGerman() As Dictionary(Of String, String)
        Dim returnValue As Dictionary(Of String, String) = New Dictionary(Of String, String)()

        returnValue("TimeStampCheckBox") = "Wenn dies aktiviert ist, wird mit jeder Nachricht die" & Environment.NewLine & "du oder die Domina sendet ein Zeitstempel angezeigt"
        returnValue("ShowNamesCheckBox") = "Wenn dies aktiviert ist, wird mit jeder Nachricht" & Environment.NewLine & "die du oder die Domina sendet der Name angezeigt." & Environment.NewLine & Environment.NewLine & "Wenn dies deaktiviert ist, Namen werden nur erscheinen" & Environment.NewLine & "wenn du nicht der letzte warst, der geschrieben hat."

        Return returnValue
    End Function
End Class
