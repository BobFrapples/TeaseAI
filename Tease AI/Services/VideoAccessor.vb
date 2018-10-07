Option Infer Off
Option Strict On
Imports Tease_AI.My
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces.Accessors

Public Class VideoAccessor
    Implements IVideoAccessor

    Public Function GetVideoData(Genre As VideoGenre?) As Result(Of List(Of VideoMetaData)) Implements IVideoAccessor.GetVideoData
        Dim rVal As List(Of VideoMetaData) = New List(Of VideoMetaData)()

        rVal.AddRange(GetGenreData(MySettings.Default.CBBlowjob, VideoGenre.Blowjob, MySettings.Default.VideoBlowjob, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBBlowjobD, VideoGenre.Blowjob, MySettings.Default.VideoBlowjobD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBCH, VideoGenre.CockHero, MySettings.Default.VideoCH, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBCHD, VideoGenre.CockHero, MySettings.Default.VideoCHD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBFemdom, VideoGenre.FemDom, MySettings.Default.VideoFemdom, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBFemdomD, VideoGenre.FemDom, MySettings.Default.VideoFemdomD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBFemsub, VideoGenre.FemSub, MySettings.Default.VideoFemsub, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBFemsubD, VideoGenre.FemSub, MySettings.Default.VideoFemsubD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBGeneral, VideoGenre.General, MySettings.Default.VideoGeneral, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBGeneralD, VideoGenre.General, MySettings.Default.VideoGeneralD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBHardcore, VideoGenre.Hardcore, MySettings.Default.VideoHardcore, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBHardcoreD, VideoGenre.Hardcore, MySettings.Default.VideoHardcoreD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBJOI, VideoGenre.Joi, MySettings.Default.VideoJOI, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBJOID, VideoGenre.Joi, MySettings.Default.VideoJOID, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBLesbian, VideoGenre.Lesbian, MySettings.Default.VideoLesbian, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBLesbianD, VideoGenre.Lesbian, MySettings.Default.VideoLesbianD, True))

        rVal.AddRange(GetGenreData(MySettings.Default.CBSoftcore, VideoGenre.Softcore, MySettings.Default.VideoSoftcore, False))
        rVal.AddRange(GetGenreData(MySettings.Default.CBSoftcoreD, VideoGenre.Softcore, MySettings.Default.VideoSoftcoreD, True))

        Return Result.Ok(rVal)
    End Function

    Private Function GetGenreData(isEnabled As Boolean, genre As VideoGenre, path As String, featuresDomme As Boolean) As List(Of VideoMetaData)
        Dim rVal As List(Of VideoMetaData) = New List(Of VideoMetaData)()
        If (isEnabled) Then
            For Each fileName As String In myDirectory.GetFilesVideo(path)
                Dim item As VideoMetaData = New VideoMetaData()
                item.Genre = genre
                item.Key = fileName
                item.FeaturesDomme = featuresDomme
                rVal.Add(item)
            Next
        End If
        Return rVal
    End Function
End Class
