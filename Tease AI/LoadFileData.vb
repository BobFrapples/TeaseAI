Imports System.IO
Imports TeaseAI.Common

Public Class LoadFileData
    Implements ILoadFileData

    Public Function ReadData(fileName As String) As Result(Of String) Implements ILoadFileData.ReadData
        Try
            If (Not File.Exists(fileName)) Then
                Return Result.Fail(Of String)(fileName + " was not found.")
            End If
            Return Result.Ok(File.ReadAllText(fileName))
        Catch ex As Exception
            Return Result.Fail(Of String)(ex.Message)
        End Try
    End Function
End Class