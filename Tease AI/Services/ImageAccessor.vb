Option Strict On
Option Infer Off
Imports System.IO
Imports Tease_AI
Imports Tease_AI.My
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces.Accessors

Public Class ImageAccessor
    Implements IImageAccessor

    Public Sub New(configurationAccessor As IConfigurationAccessor)
        myConfigurationAccessor = configurationAccessor
    End Sub

    Public Function GetImageMetaDataList(source As ImageSource?, genre As ImageGenre?) As Result(Of List(Of ImageMetaData)) Implements IImageAccessor.GetImageMetaDataList
        Dim returnValue As List(Of ImageMetaData) = New List(Of ImageMetaData)()

        Dim containers As List(Of ImageContainer) = myConfigurationAccessor.GetApplicationConfiguration().ImageContainers

        For Each container As ImageContainer In containers
            Dim searchLevel As FileIO.SearchOption = If(container.UseSubFolders, FileIO.SearchOption.SearchAllSubDirectories, FileIO.SearchOption.SearchTopLevelOnly)

            If (genre.GetValueOrDefault(container.Genre) = container.Genre) AndAlso source.GetValueOrDefault(container.Source) = container.Source Then
                If (container.Source = ImageSource.Local) Then
                    If (container.Genre = ImageGenre.Liked) OrElse (container.Genre = ImageGenre.Disliked) Then
                        For Each foundFile As String In File.ReadAllLines(container.Path)
                            If (Not foundFile.ToLower().StartsWith("http")) Then
                                returnValue.Add(New ImageMetaData() With
                                            {
                                            .Genre = container.Genre,
                                            .ItemName = foundFile,
                                            .Source = container.Source
                                            })
                            End If
                        Next
                    Else
                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(container.Path, searchLevel, "*.jpg")
                            returnValue.Add(New ImageMetaData() With
                            {
                            .Genre = container.Genre,
                            .ItemName = foundFile,
                            .Source = container.Source
                            })
                        Next
                    End If
                ElseIf container.Source = ImageSource.Remote Then
                    If (container.Genre = ImageGenre.Liked) OrElse (container.Genre = ImageGenre.Disliked) Then
                        For Each foundFile As String In File.ReadAllLines(container.Path)
                            If (foundFile.ToLower().StartsWith("http")) Then
                                returnValue.Add(New ImageMetaData() With
                                            {
                                            .Genre = container.Genre,
                                            .ItemName = foundFile,
                                            .Source = container.Source
                                            })
                            End If
                        Next
                    ElseIf (container.Genre = ImageGenre.Blog) Then
                        For Each blogFile As String In GetAvailableBlogFiles()
                            For Each foundFile As String In File.ReadAllLines(blogFile)
                                returnValue.Add(New ImageMetaData() With
                                            {
                                            .Genre = ImageGenre.Blog,
                                            .ItemName = foundFile,
                                            .Source = container.Source
                                            })
                            Next
                        Next
                    Else
                        Dim fileName As String = myPathUrlFileDir + container.Path
                        For Each foundFile As String In File.ReadAllLines(fileName)
                            returnValue.Add(New ImageMetaData() With
                                            {
                                            .Genre = container.Genre,
                                            .ItemName = foundFile,
                                            .Source = container.Source
                                            })
                        Next
                    End If
                End If
            End If
        Next

        Return Result.Ok(returnValue)
    End Function

    Private Function GetAvailableBlogFiles() As List(Of String)
        Dim checkList As String = myPathUrlFileDir + "..\URLFileCheckList.cld"

        Dim returnValue As List(Of String) = New List(Of String)()
        Using fs As New FileStream(checkList, FileMode.Open), binRead As New BinaryReader(fs)
            Do While fs.Position < fs.Length
                Dim fileName As String = binRead.ReadString()
                Dim enabled As Boolean = binRead.ReadBoolean()
                Dim fullFilePath As String = myPathUrlFileDir + fileName + ".txt"

                If File.Exists(fullFilePath) AndAlso enabled Then
                    returnValue.Add(fullFilePath)
                End If
            Loop
        End Using
        Return returnValue.Distinct().ToList()
    End Function

    Public Function SaveImageMetaData(imageMetaDatas As List(Of ImageMetaData)) As Result Implements IImageAccessor.SaveImageMetaData
        Throw New NotImplementedException()
    End Function

    Private mySystemImageDIr As String = System.Windows.Forms.Application.StartupPath + "\Images\System\"
    Private myPathUrlFileDir As String = mySystemImageDIr + "URL Files\"
    Private ReadOnly myConfigurationAccessor As IConfigurationAccessor
End Class
