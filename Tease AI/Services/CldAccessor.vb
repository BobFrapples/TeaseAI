Option Strict On
Option Explicit On
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces.Accessors

Public Class CldAccessor
    Implements ICldAccessor

    Public Sub WriteCld(cldData As List(Of ScriptMetaData), filePath As String) Implements ICldAccessor.WriteCld
        If Not Directory.Exists(Path.GetDirectoryName(filePath)) Then
            Directory.CreateDirectory(Path.GetDirectoryName(filePath))
        End If

        Using fs As New FileStream(filePath, IO.FileMode.Create), BinWrite As New BinaryWriter(fs)
            For i = 0 To cldData.Count - 1
                BinWrite.Write(cldData(i).Name)
                BinWrite.Write(cldData(i).IsEnabled)
            Next
        End Using
    End Sub

    Public Function ReadCld(scriptHomeDir As String, fileName As String) As Result(Of List(Of ScriptMetaData)) Implements ICldAccessor.ReadCld
        If Not File.Exists(fileName) Then
            Return Result.Fail(Of List(Of ScriptMetaData))("Checklist file " + fileName + " does not exist")
        End If
        Dim returnValue As List(Of ScriptMetaData) = New List(Of ScriptMetaData)()
        Using fs As New FileStream(fileName, FileMode.Open), binRead As New BinaryReader(fs)
            Do While fs.Position < fs.Length
                Dim name As String = binRead.ReadString()
                Dim cldData As ScriptMetaData = New ScriptMetaData With {
                    .Key = scriptHomeDir & Path.DirectorySeparatorChar & name,
                    .Name = name,
                    .IsEnabled = binRead.ReadBoolean(),
                    .Info = String.Empty
                }
                returnValue.Add(cldData)
            Loop
        End Using
        Return Result.Ok(returnValue)
    End Function

End Class
