Imports System.IO
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data

Public Class BlogImageAccessor

    Function GetBlogMetaData() As List(Of BlogMetaData)
        'Private myPathUrlFileDir As String = mySystemImageDIr + "URL Files\"
        Dim returnValue As List(Of BlogMetaData) = New List(Of BlogMetaData)()
        If File.Exists(myUrlMasterFile) Then
            Using fs As New FileStream(myUrlMasterFile, FileMode.Open), binRead As New BinaryReader(fs)
                Do While fs.Position < fs.Length
                    Dim fileName As String = binRead.ReadString()
                    Dim enabled As Boolean = binRead.ReadBoolean()
                    If File.Exists(mySystemImageDir & "/Url Files/" & fileName) Then
                        returnValue.Add(New BlogMetaData() With
                                            {
                                            .Genre = ImageGenre.Blog,
                                            .FileName = fileName,
                                            .IsEnabled = enabled
                                            })
                    End If
                Loop
            End Using
        Else
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(mySystemImageDir & "/URL Files", FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                returnValue.Add(New BlogMetaData() With
                                            {
                                            .Genre = ImageGenre.Blog,
                                            .FileName = Path.GetFileName(foundFile).Replace(".txt", ""),
                                            .IsEnabled = True
                                            })
            Next
        End If
        Return returnValue.Distinct().ToList()
    End Function

    Function GetImageMetaData(blogMetaData As BlogMetaData) As List(Of ImageMetaData)
        Dim returnValue As List(Of ImageMetaData) = New List(Of ImageMetaData)()
        Dim blogImageList = mySystemImageDir & "/URL Files\" & blogMetaData.FileName & ".txt"
        For Each imageUrl As String In File.ReadAllLines(blogImageList)
            returnValue.Add(New ImageMetaData() With
                                {
                                .GenreId = ImageGenre.Blog,
                                .SourceId = ImageSource.Remote,
                                .ItemName = imageUrl
                                })
        Next
        Return returnValue
    End Function

    Sub SaveBlogMetaData(newMetaData As List(Of BlogMetaData))
        Using fs As New FileStream(myUrlMasterFile, FileMode.Open), writer As New BinaryWriter(fs)
            Dim blogMetaData As List(Of BlogMetaData) = New List(Of BlogMetaData)()
            For Each metaData In newMetaData
                writer.Write(metaData.FileName)
                writer.Write(metaData.IsEnabled)
            Next
        End Using
    End Sub

    Private mySystemImageDir As String = Application.StartupPath + "/Images/System"
    Private myUrlMasterFile As String = mySystemImageDir + "/URLFileCheckList.cld"
End Class
