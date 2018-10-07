Option Strict On
Option Infer Off
Imports System.IO
Imports Tease_AI.My
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces.Accessors

Public Class ImageAccessor
    Implements IImageAccessor

    Public Function GetImageMetaDataList(source As ImageSource?, genre As ImageGenre?) As Result(Of List(Of ImageMetaData)) Implements IImageAccessor.GetImageMetaDataList
        Dim returnValue As List(Of ImageMetaData) = New List(Of ImageMetaData)()

        Dim containers As List(Of ImageContainer) = GetContainers()
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

    Private Function GetContainers() As List(Of ImageContainer)
        Dim containers As List(Of ImageContainer) = New List(Of ImageContainer)()

        Dim inputs As List(Of Tuple(Of Boolean, ImageSource, ImageGenre, String, Boolean)) = New List(Of Tuple(Of Boolean, ImageSource, ImageGenre, String, Boolean))()

        inputs.Add(Tuple.Create(MySettings.Default.CBIBlowjob, ImageSource.Local, ImageGenre.Blowjob, MySettings.Default.IBlowjob, MySettings.Default.CBBlowjob))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileBlowjobEnabled, ImageSource.Remote, ImageGenre.Blowjob, MySettings.Default.UrlFileBlowjob, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIBoobs, ImageSource.Local, ImageGenre.Boobs, MySettings.Default.LBLBoobPath, MySettings.Default.CBBoobSubDir))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileBoobsEnabled, ImageSource.Remote, ImageGenre.Boobs, MySettings.Default.UrlFileBoobs, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIButts, ImageSource.Local, ImageGenre.Butt, MySettings.Default.LBLButtPath, MySettings.Default.CBButtSubDir))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileButtEnabled, ImageSource.Remote, ImageGenre.Butt, MySettings.Default.UrlFileButt, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBICaptions, ImageSource.Local, ImageGenre.Captions, MySettings.Default.ICaptions, MySettings.Default.ICaptionsSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileCaptionsEnabled, ImageSource.Remote, ImageGenre.Captions, MySettings.Default.UrlFileCaptions, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIFemdom, ImageSource.Local, ImageGenre.Femdom, MySettings.Default.IFemdom, MySettings.Default.IFemdomSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileFemdomEnabled, ImageSource.Remote, ImageGenre.Femdom, MySettings.Default.UrlFileFemdom, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIGay, ImageSource.Local, ImageGenre.Gay, MySettings.Default.IGay, MySettings.Default.IGaySD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileGayEnabled, ImageSource.Remote, ImageGenre.Gay, MySettings.Default.UrlFileGay, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIGeneral, ImageSource.Local, ImageGenre.General, MySettings.Default.IGeneral, MySettings.Default.IGeneralSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileGeneralEnabled, ImageSource.Remote, ImageGenre.General, MySettings.Default.UrlFileGeneral, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIHardcore, ImageSource.Local, ImageGenre.Hardcore, MySettings.Default.IHardcore, MySettings.Default.IHardcoreSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileHardcoreEnabled, ImageSource.Remote, ImageGenre.Hardcore, MySettings.Default.UrlFileHardcore, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIHentai, ImageSource.Local, ImageGenre.Hentai, MySettings.Default.IHentai, MySettings.Default.IHentaiSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileHentaiEnabled, ImageSource.Remote, ImageGenre.Hentai, MySettings.Default.UrlFileHentai, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBILesbian, ImageSource.Local, ImageGenre.Lesbian, MySettings.Default.ILesbian, MySettings.Default.ILesbianSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileLesbianEnabled, ImageSource.Remote, ImageGenre.Lesbian, MySettings.Default.UrlFileLesbian, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBILezdom, ImageSource.Local, ImageGenre.Lezdom, MySettings.Default.ILesbian, MySettings.Default.ILezdomSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileLezdomEnabled, ImageSource.Remote, ImageGenre.Lezdom, MySettings.Default.UrlFileLezdom, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBIMaledom, ImageSource.Local, ImageGenre.Maledom, MySettings.Default.IMaledom, MySettings.Default.IMaledomSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileMaledomEnabled, ImageSource.Remote, ImageGenre.Maledom, MySettings.Default.UrlFileMaledom, False))

        inputs.Add(Tuple.Create(MySettings.Default.CBISoftcore, ImageSource.Local, ImageGenre.Softcore, MySettings.Default.ISoftcore, MySettings.Default.ISoftcoreSD))
        inputs.Add(Tuple.Create(MySettings.Default.UrlFileSoftcoreEnabled, ImageSource.Remote, ImageGenre.Softcore, MySettings.Default.UrlFileSoftcore, False))

        For Each inputData As Tuple(Of Boolean, ImageSource, ImageGenre, String, Boolean) In inputs
            If (inputData.Item1) Then
                containers.Add(New ImageContainer With
                           {
                           .Source = inputData.Item2,
                           .Genre = inputData.Item3,
                           .Path = inputData.Item4,
                           .UseSubFolders = inputData.Item5
                           })
            End If
        Next

        ' These are special "meta" containers. I hate them.
        containers.Add(New ImageContainer() With {
            .Source = ImageSource.Remote,
            .Genre = ImageGenre.Blog
            })

        containers.Add(New ImageContainer() With {
            .Source = ImageSource.Local,
            .Genre = ImageGenre.Liked,
            .Path = mySystemImageDIr + "LikedImageURLs.txt"
            })

        containers.Add(New ImageContainer() With {
            .Source = ImageSource.Local,
            .Genre = ImageGenre.Liked,
            .Path = mySystemImageDIr + "LikedImageURLs.txt"
            })

        containers.Add(New ImageContainer() With {
            .Source = ImageSource.Remote,
            .Genre = ImageGenre.Disliked,
            .Path = mySystemImageDIr + "DislikedImageURLs.txt"
            })

        containers.Add(New ImageContainer() With {
            .Source = ImageSource.Remote,
            .Genre = ImageGenre.Disliked,
            .Path = mySystemImageDIr + "DislikedImageURLs.txt"
            })

        Return containers
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


    Private mySystemImageDIr As String = System.Windows.Forms.Application.StartupPath + "\Images\System\"
    Private myPathUrlFileDir As String = mySystemImageDIr + "URL Files\"
End Class
