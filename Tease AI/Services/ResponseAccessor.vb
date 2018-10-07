Option Strict On
Option Infer Off
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Services

Public Class ResponseAccessor
    Implements IResponseAccessor

    Private ReadOnly myLineService As LineService

    Public Function GetResponses(session As Session) As List(Of Response) Implements IResponseAccessor.GetResponses
        Dim returnValue As List(Of Response) = New List(Of Response)()
        Dim path As String = Application.StartupPath + "\Scripts\" + session.Domme.PersonalityName + "\Vocabulary\Responses\"

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
            returnValue.Add(CreateResponse(foundFile))
        Next
        Return returnValue
    End Function

    Private Function CreateResponse(foundFile As String) As Response
        Dim lines As List(Of String) = File.ReadAllLines(foundFile).ToList()
        Dim keywords As Result(Of List(Of String)) = myLineService.GetParenData(lines(0), "[")
        lines.RemoveAt(0)

        Dim returnValue As Response = New Response(foundFile)
        returnValue.Phrases.AddRange(keywords.GetResultOrDefault(New List(Of String)()))

        Dim bookmarkName As String = String.Empty
        For Each line As String In lines
            If (String.IsNullOrWhiteSpace(bookmarkName) AndAlso line(0) = "[") Then
                bookmarkName = line.Replace("[", "").Replace("]", "")
                If (Not returnValue.Responses.Keys.Contains(bookmarkName) AndAlso Not bookmarkName = Response.All) Then
                    returnValue.Responses(bookmarkName) = New List(Of String)
                End If
            ElseIf (line = "[" + bookmarkName + " End]") Then
                bookmarkName = String.Empty
            ElseIf Not String.IsNullOrWhiteSpace(bookmarkName) Then
                If (bookmarkName = Response.All) Then
                    returnValue.Responses(Response.AfterTease).Add(line)
                    returnValue.Responses(Response.BeforeTease).Add(line)
                    returnValue.Responses(Response.CbtBalls).Add(line)
                    returnValue.Responses(Response.CbtCock).Add(line)
                    returnValue.Responses(Response.Edging).Add(line)
                    returnValue.Responses(Response.FirstRound).Add(line)
                    returnValue.Responses(Response.HoldingTheEdge).Add(line)
                    returnValue.Responses(Response.NotStroking).Add(line)
                    returnValue.Responses(Response.Stroking).Add(line)
                Else
                    returnValue.Responses(bookmarkName).Add(line)
                End If
            Else
                Dim unknown As String = line
            End If
        Next
        Return returnValue
    End Function

    Public Sub New(lineService As LineService)
        myLineService = lineService
    End Sub

End Class
