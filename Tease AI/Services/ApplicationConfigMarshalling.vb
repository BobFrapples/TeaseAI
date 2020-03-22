Option Explicit On
Option Infer Off
Imports Tease_AI.My
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data

Public Class ApplicationConfigMarshalling
    Public Function GetApplicationConfiguration() As ApplicationConfiguration
        Dim appConfig As ApplicationConfiguration = New ApplicationConfiguration()
        appConfig.ImageContainers = CreateContainersFromOldConfig()
        Return appConfig
    End Function

    Private Function CreateContainersFromOldConfig() As List(Of ImageContainer)
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
            containers.Add(New ImageContainer With
                       {
                       .IsEnabled = inputData.Item1,
                       .Source = inputData.Item2,
                       .Genre = inputData.Item3,
                       .Path = inputData.Item4,
                       .UseSubFolders = inputData.Item5
                       })
        Next

        ' These are special "meta" containers. I hate them.
        containers.Add(New ImageContainer() With {
            .IsEnabled = True,
            .Source = ImageSource.Remote,
            .Genre = ImageGenre.Blog
            })

        containers.Add(New ImageContainer() With {
            .IsEnabled = True,
            .Source = ImageSource.Local,
            .Genre = ImageGenre.Liked,
            .Path = mySystemImageDIr + "LikedImageURLs.txt"
            })

        containers.Add(New ImageContainer() With {
            .IsEnabled = True,
            .Source = ImageSource.Remote,
            .Genre = ImageGenre.Liked,
            .Path = mySystemImageDIr + "LikedImageURLs.txt"
            })

        containers.Add(New ImageContainer() With {
            .IsEnabled = True,
            .Source = ImageSource.Local,
            .Genre = ImageGenre.Disliked,
            .Path = mySystemImageDIr + "DislikedImageURLs.txt"
            })

        containers.Add(New ImageContainer() With {
            .IsEnabled = True,
            .Source = ImageSource.Remote,
            .Genre = ImageGenre.Disliked,
            .Path = mySystemImageDIr + "DislikedImageURLs.txt"
            })
        Return containers
    End Function

    Private ReadOnly mySystemImageDIr As String = Windows.Forms.Application.StartupPath + "\Images\System\"
    Private myPathUrlFileDir As String = mySystemImageDIr + "URL Files\"
End Class
