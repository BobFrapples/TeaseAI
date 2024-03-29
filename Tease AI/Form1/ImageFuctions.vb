﻿Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Events
Imports TeaseAI.Common.Interfaces.Accessors

Partial Class MainWindow

#Region "-------------------------------------------------- ImageDataContainer ------------------------------------------------"

    Friend Enum ImageSourceType
        Local
        Remote
    End Enum

    ''' <summary>
    ''' Represents a Object which can store all necessary Data related to genere-Images. 
    ''' This obejct is intended for managing Images. All Data and conditions can be stored in here
    ''' and retrieved from it.
    ''' </summary>
    Friend Class ImageDataContainer
        Private ReadOnly myPathsAccessor As PathsAccessor

        Public Sub New()
            myPathsAccessor = ApplicationFactory.CreatePathsAccessor()
        End Sub

        'TODO: ImageDataContainer Improve the usage of System Ressources.
        Public Name As ImageGenre

        ''' =========================================================================================================
        ''' <summary>
        ''' Stores the absolute or relative Url-File-Path.
        ''' <para>Do not access this direct. Use the Property instead!</para>
        ''' </summary>
        Private _URLFile As String = ""
        ''' <summary>
        ''' Gets or sets the Url-File-Path. Make sure you set this value with extension. Otherwise the value won't 
        ''' be accepted. The filepath can be a relatative or an absolute. A relative path will rooted with the 
        ''' standard URL-File directory
        ''' </summary>
        ''' <returns>The absolute Url-File-Path. </returns>
        Public Property UrlFile As String
            Get
                If _URLFile = "" Then Return _URLFile
                If _URLFile.ToLower.EndsWith(".txt") = False Then Return ""

                If Path.IsPathRooted(_URLFile) Then
                    ' Return an absolute FilePath
                    Return _URLFile
                Else
                    ' Return the relative path absolute.
                    Return myPathsAccessor.UrlsDirectory & _URLFile
                End If
            End Get
            Set(value As String)
                If value Is Nothing Then
                    _URLFile = ""
                ElseIf value.ToLower.EndsWith(".txt") = False Then
                    _URLFile = ""
                Else
                    _URLFile = value
                End If
            End Set
        End Property

        Public LocalDirectory As String = ""
        Public LocalSubDirectories As Boolean = False
        Public SYS_NoPornAllowed As Boolean = False

        Public Function CountImages() As Integer
            Return ToList().Count
        End Function

        Public Function CountImages(ByRef Type As ImageSourceType) As Integer
            Return ToList(Type).Count
        End Function

        ''' =========================================================================================================
        ''' <summary>
        ''' Checks if there are images available for the current Genre.
        ''' </summary>
        ''' <returns></returns>
        Public Function IsAvailable() As Boolean
            If CountImages() > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function IsAvailable(ByRef Type As ImageSourceType) As Boolean
            If CountImages(Type) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' =========================================================================================================
        ''' <summary>
        ''' Returns a List of FilePaths or URLs.
        ''' </summary>
        ''' <returns>Returns a List Containing all Found Links. If none are 
        ''' Found an empty List is returned</returns>
        Public Function ToList() As List(Of String)
            Dim rtnList As New List(Of String)
            Try
                ' If no Porn is allowed, then return Empty
                If SYS_NoPornAllowed Then Return rtnList

                rtnList.AddRange(ToList(ImageSourceType.Local))

                ' Load only LocalFiles if Oflline-Mode is acivated.
                Dim settingsAccessor As ISettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
                Dim settings As Settings = settingsAccessor.GetSettings()
                If Not settings.Misc.IsOffline Then _
                    rtnList.AddRange(ToList(ImageSourceType.Remote))
            Catch ex As Exception
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                '                                            All Errors
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Log.WriteError("Failed to fetch ImageList for genre." & Name.ToString & " : " & ex.Message, ex,
                               "Excetion at: " & Name.ToString & ".ToList()")
                Return New List(Of String)
            End Try
            Return rtnList
        End Function

        ''' =========================================================================================================
        ''' <summary>
        ''' Returns a List of FilePaths or URLs for the given Type.
        ''' </summary>
        ''' <param name="Type">The LoationType to Search for.</param>
        ''' <returns>Returns a List Containing all Found Links. If none are 
        ''' Found an empty List is returned</returns>
        Public Function ToList(ByRef Type As ImageSourceType) As List(Of String)
            Dim rtnList As New List(Of String)

            Try
                ' If no Porn is allowed, then return Empty
                If SYS_NoPornAllowed Then Return rtnList

                ' If offline mode is activated then search only for local files.
                Dim settingsAccessor As ISettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
                Dim settings As Settings = settingsAccessor.GetSettings()
                If settings.Misc.IsOffline Then Type = ImageSourceType.Local

                If Name = ImageGenre.Blog Then
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    '                                   Blog Images
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    If settings.Misc.IsOffline OrElse Type = ImageSourceType.Local Then GoTo exitEmpty
                    Dim tmpList As New List(Of String)

                    ' Load threadsafe all checked Items into List.
                    Dim temp As Object = FrmSettings.UIThread(Function()
                                                                  Return FrmSettings.RemoteMediaContainerList.CheckedItems.Cast(Of String).ToList
                                                              End Function)

                    tmpList.AddRange(DirectCast(temp, List(Of String)))

                    ' Remove Items where File does not exist.
                    tmpList.RemoveAll(Function(x) Not File.Exists(myPathsAccessor.UrlsDirectory & x & ".txt"))

                    ' Check Result if Files in List
                    If tmpList.Count < 1 Then GoTo exitEmpty

                    For Each fileName As String In tmpList
                        ' Read the URL-File
                        Dim addList As List(Of String) = Txt2List(myPathsAccessor.UrlsDirectory & fileName & ".txt")

                        ' add lines from file
                        rtnList.AddRange(addList)
                    Next
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                    ' Blog Images - End
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                ElseIf Name = ImageGenre.Liked Then
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    '                                  Liked Images
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    Try
                        Dim addlist As List(Of String) = Txt2List(myPathsAccessor.LikedImages)

                        ' Remove all URLs if Offline-Mode is activated
                        If settings.Misc.IsOffline OrElse Type = ImageSourceType.Local Then
                            addlist.RemoveAll(Function(x) IsUrl(x))
                        End If

                        rtnList.AddRange(addlist)
                    Catch ex As Exception
                        Log.WriteError(ex.Message, ex, "Error occured while loading Likelist")
                        GoTo exitEmpty
                    End Try
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                    ' Liked Images - End
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                ElseIf Name = ImageGenre.Disliked Then
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    '                                Disliked Images
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    Try
                        Dim addlist As List(Of String) = Txt2List(myPathsAccessor.DislikedImages)

                        ' Remove all URLs if Offline-Mode is activated
                        If settings.Misc.IsOffline OrElse Type = ImageSourceType.Local Then
                            addlist.RemoveAll(Function(x) IsUrl(x))
                        End If

                        rtnList.AddRange(addlist)
                    Catch ex As Exception
                        Log.WriteError(ex.Message, ex, "Error occured while loading Dislikelist")
                        GoTo exitEmpty
                    End Try
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                    ' Disliked Images - End
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                Else
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    '                                 Regular Genres
                    '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                    ' Load Local ImageList
                    If Type = ImageSourceType.Local AndAlso LocalDirectory <> "" AndAlso LocalDirectory IsNot Nothing Then
                        If LocalSubDirectories = False Then
                            rtnList.AddRange(myDirectory.GetFilesImages(LocalDirectory, SearchOption.TopDirectoryOnly))
                        Else
                            rtnList.AddRange(myDirectory.GetFilesImages(LocalDirectory, SearchOption.AllDirectories))
                        End If
                    End If

                    ' Load an URL-File
                    If Type = ImageSourceType.Remote And UrlFile <> "" And UrlFile IsNot Nothing Then
                        rtnList.AddRange(Txt2List(UrlFile))
                    End If
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                    ' Regular Genres - End
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                End If


                If Type = ImageSourceType.Local Then
                    If My.Application.CommandLineArgs.Contains("-forceLocalGif") Then
                        ' Removae all non-Gif images
                        rtnList.RemoveAll(Function(x) x.ToLower.EndsWith(".gif") = False)
                    ElseIf My.Application.CommandLineArgs.Contains("-disableLocalGif") Then
                        ' Remove all gif images
                        rtnList.RemoveAll(Function(x) x.ToLower.EndsWith(".gif"))
                    End If
                End If

                If Type = ImageSourceType.Remote Then
                    If My.Application.CommandLineArgs.Contains("-forceRemoteGif") Then
                        ' Removae all non-Gif images
                        rtnList.RemoveAll(Function(x) x.ToLower.EndsWith(".gif") = False)
                    ElseIf My.Application.CommandLineArgs.Contains("-disableRemoteGif") Then
                        ' Remove all gif images
                        rtnList.RemoveAll(Function(x) x.ToLower.EndsWith(".gif"))
                    End If
                End If
            Catch ex As Exception
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                '                                            All Errors
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Log.WriteError("Failed to fetch ImageList for genre." & Name.ToString & " and Source." & Type.ToString & " : " & ex.Message, ex,
                               "Excetion at: " & Name.ToString & ".ToList(" & Type.ToString & ")")
                Return New List(Of String)
            End Try
exitEmpty:
            Return rtnList
        End Function

        ''' =========================================================================================================
        ''' <summary>
        ''' Returns a random Image URL or Path from all Lists and directories.
        ''' </summary>
        ''' <returns>Returns a FilePath or URL. If there are no Images found the 
        ''' "NoLocalImagesFound-Image-Filepath is returned.</returns>
        Public Function Random() As String
            Try
                ' Get List of Files
                Dim tmpList As List(Of String) = ToList()

                ' Check if Links/Paths are present.
                If tmpList.Count <= 0 Then GoTo NoneFound

                ' Pick a Random Image-Path
                Return tmpList(New Random().Next(0, tmpList.Count))
            Catch ex As Exception
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                '						       All Errors
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Log.WriteError(ex.Message & vbCrLf & ToString(), ex, "Error while choosing a random Image.")
            End Try
NoneFound:
            ' Return the Error-Image FilePath
            Return Application.StartupPath & "\Images\System\NoLocalImagesFound.jpg"
        End Function

        ''' =========================================================================================================
        ''' <summary>
        ''' Returns a random Image for the given ImageGenre.
        ''' </summary>
        ''' <returns>Returns a FilePath or URL. If there are no Images found the 
        ''' "NoLocalImagesFound-Image-Filepath is returned.</returns>
        Public Function Random(ByRef Type As ImageSourceType) As String
            Try
                Dim settingsAccessor As ISettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
                Dim settings As Settings = settingsAccessor.GetSettings()
                ' If offline mode is activated then search only for local files.
                If settings.Misc.IsOffline Then Type = ImageSourceType.Local

                ' Get List of Files for Current Type
                Dim tmpList As List(Of String) = ToList(Type)

                ' Check if Links/Paths are present.
                If tmpList.Count <= 0 Then GoTo NoneFound

                ' Pick a Random Image-Path
                Return tmpList(New Random().Next(0, tmpList.Count))
            Catch ex As Exception
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                '						       All Errors
                '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Log.WriteError(ex.Message & vbCrLf & ToString(), ex, "Error while choosing a random Image.")
            End Try
NoneFound:
            ' Return an Error-Image FilePath
            If Type = ImageSourceType.Local _
            Then Return Application.StartupPath & "\Images\System\NoLocalImagesFound.jpg" _
            Else Return Application.StartupPath & "\Images\System\NoURLFilesSelected.jpg"
        End Function

        Public Overrides Function ToString() As String
            Dim settingsAccessor As ISettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
            Dim settings As Settings = settingsAccessor.GetSettings()
            Return "ImageDataContainer containing:" & vbCrLf &
                    "	GenreName: " & Name.ToString & vbCrLf &
                    "	URLFile: " & UrlFile.ToString & vbCrLf &
                    "	LocalDirectory: " & LocalDirectory.ToString & vbCrLf &
                    "	LocalSubDirectories: " & LocalSubDirectories.ToString & vbCrLf &
                    "	OfflineMode: " & settings.Misc.IsOffline.ToOnOff()
            'Return MyBase.ToString()
        End Function

    End Class

#End Region ' ImageDataContainer

    Friend Function GetImageData(genre As ImageGenre) As ImageDataContainer
        Dim tmpObj As Dictionary(Of ImageGenre, ImageDataContainer) = GetImageData()

        Return tmpObj(genre)
    End Function

    ''' =========================================================================================================
    ''' <summary>
    ''' Gets a dictionary which contains all nessecary data of genere-images.
    ''' </summary>
    ''' <returns>Returns a dictionary which contains all nessecary data of genere-images.</returns>
    Friend Function GetImageData() As Dictionary(Of ImageGenre, ImageDataContainer)
        Dim rtnDic As New Dictionary(Of ImageGenre, ImageDataContainer) '(StringComparer.OrdinalIgnoreCase)
        Dim sysNoPornAllowed As Boolean = myFlagAccessor.IsSet(CreateDommePersonality(), "SYS_NoPornAllowed")
        With rtnDic
            .Add(ImageGenre.Blog, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Blog,
                    .SYS_NoPornAllowed = sysNoPornAllowed
                 })
            .Add(ImageGenre.Liked, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Liked,
                    .SYS_NoPornAllowed = sysNoPornAllowed
                 })
            .Add(ImageGenre.Disliked, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Disliked,
                    .SYS_NoPornAllowed = sysNoPornAllowed
                 })

            .Add(ImageGenre.Butt, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Butt,
                    .LocalDirectory = If(My.Settings.CBIButts, My.Settings.LBLButtPath, ""),
                    .LocalSubDirectories = My.Settings.CBButtSubDir,
                    .UrlFile = If(My.Settings.UrlFileButtEnabled, My.Settings.UrlFileButt, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Boobs, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Boobs,
                    .LocalDirectory = If(My.Settings.CBIBoobs, My.Settings.LBLBoobPath, ""),
                    .LocalSubDirectories = My.Settings.CBButtSubDir,
                    .UrlFile = If(My.Settings.UrlFileBoobsEnabled, My.Settings.UrlFileBoobs, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Hardcore, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Hardcore,
                    .LocalDirectory = If(My.Settings.CBIHardcore, My.Settings.IHardcore, ""),
                    .LocalSubDirectories = My.Settings.CBHardcore,
                    .UrlFile = If(My.Settings.UrlFileHardcoreEnabled, My.Settings.UrlFileHardcore, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Softcore, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Softcore,
                    .LocalDirectory = If(My.Settings.CBISoftcore, My.Settings.ISoftcore, ""),
                    .LocalSubDirectories = My.Settings.CBSoftcore,
                    .UrlFile = If(My.Settings.UrlFileSoftcoreEnabled, My.Settings.UrlFileSoftcore, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Lesbian, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Lesbian,
                    .LocalDirectory = If(My.Settings.CBILesbian, My.Settings.ILesbian, ""),
                    .LocalSubDirectories = My.Settings.CBLesbian,
                    .UrlFile = If(My.Settings.UrlFileLesbianEnabled, My.Settings.UrlFileLesbian, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Blowjob, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Blowjob,
                    .LocalDirectory = If(My.Settings.CBIBlowjob, My.Settings.IBlowjob, ""),
                    .LocalSubDirectories = My.Settings.CBBlowjob,
                    .UrlFile = If(My.Settings.UrlFileBlowjobEnabled, My.Settings.UrlFileBlowjob, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Femdom, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Femdom,
                    .LocalDirectory = If(My.Settings.CBIFemdom, My.Settings.IFemdom, ""),
                    .LocalSubDirectories = My.Settings.CBFemdom,
                    .UrlFile = If(My.Settings.UrlFileFemdomEnabled, My.Settings.UrlFileFemdom, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Lezdom, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Lezdom,
                    .LocalDirectory = If(My.Settings.CBILezdom, My.Settings.ILezdom, ""),
                    .LocalSubDirectories = My.Settings.ILezdomSD,
                    .UrlFile = If(My.Settings.UrlFileLezdomEnabled, My.Settings.UrlFileLezdom, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Hentai, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Hentai,
                    .LocalDirectory = If(My.Settings.CBIHentai, My.Settings.IHentai, ""),
                    .LocalSubDirectories = My.Settings.IHentaiSD,
                    .UrlFile = If(My.Settings.UrlFileHentaiEnabled, My.Settings.UrlFileHentai, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Gay, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Gay,
                    .LocalDirectory = If(My.Settings.CBIGay, My.Settings.IGay, ""),
                    .LocalSubDirectories = My.Settings.IGaySD,
                    .UrlFile = If(My.Settings.UrlFileGayEnabled, My.Settings.UrlFileGay, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Maledom, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Maledom,
                    .LocalDirectory = If(My.Settings.CBIMaledom, My.Settings.IMaledom, ""),
                    .LocalSubDirectories = My.Settings.IMaledomSD,
                    .UrlFile = If(My.Settings.UrlFileMaledomEnabled, My.Settings.UrlFileMaledom, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.Captions, New ImageDataContainer With
                 {
                    .Name = ImageGenre.Captions,
                    .LocalDirectory = If(My.Settings.CBICaptions, My.Settings.ICaptions, ""),
                    .LocalSubDirectories = My.Settings.ICaptionsSD,
                    .UrlFile = If(My.Settings.UrlFileCaptionsEnabled, My.Settings.UrlFileCaptions, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })

            .Add(ImageGenre.General, New ImageDataContainer With
                 {
                    .Name = ImageGenre.General,
                    .LocalDirectory = If(My.Settings.CBIGeneral, My.Settings.IGeneral, ""),
                    .LocalSubDirectories = My.Settings.IGeneralSD,
                    .UrlFile = If(My.Settings.UrlFileGeneralEnabled, My.Settings.UrlFileGeneral, ""),
                    .SYS_NoPornAllowed = sysNoPornAllowed
                })
        End With

        Return rtnDic
    End Function

#Region "------------------------------------------------- GetRandom Image ----------------------------------------------------"

    ''' <summary>
    ''' Gets a random image URI from all available Sources
    ''' </summary>
    ''' <returns>The URI of a random Image.</returns>
    Friend Function GetRandomImage() As String
        ' Get all definitions for Images.
        Dim dicFilePaths As Dictionary(Of ImageGenre, ImageDataContainer) = GetImageData()
        Dim AllImages As New List(Of String)

        ' Fetch all available ImageLocations
        For Each genre As ImageGenre In dicFilePaths.Keys
            AllImages.AddRange(dicFilePaths(genre).ToList())
        Next

        ' Check if there are images
        If AllImages.Count = 0 Then Return myOldPathsAccessor.PathImageErrorNoLocalImages

        ' get an Random Image from the all available Locations
        Return AllImages(New Random().Next(0, AllImages.Count)).ToString
    End Function

    ''' <summary>
    ''' Gets a random image URI for given Genre from Local and URL-Files.
    ''' </summary>
    ''' <param name="genre">Determines the Genre to get a random image for.</param>
    ''' <returns>The URI of a random Image.</returns>
    Friend Function GetRandomImage(ByVal genre As ImageGenre) As String
        ' Get all definitions for Images.
        Dim dicFilePaths As Dictionary(Of ImageGenre, ImageDataContainer) = GetImageData()

        ' get an Random Image from the Random Genre.
        Return dicFilePaths(genre).Random()
    End Function

    ''' <summary>
    ''' Gets a random image URI for the given sourcetype (URL or Local).
    ''' </summary>
    ''' <param name="source">Determines the source to get a random image for.</param>
    ''' <returns>The URI of a random Image.</returns>
    Friend Function GetRandomImage(ByVal source As ImageSourceType) As String
        ' Get all definitions for Images.
        Dim dicFilePaths As Dictionary(Of ImageGenre, ImageDataContainer) = GetImageData()

        Dim allImages As New List(Of String)

        ' Fetch all available ImageLocations for sourceType
        For Each genre As String In dicFilePaths.Keys
            allImages.AddRange(dicFilePaths(genre).ToList(source))
        Next

        ' Check if Genres are present.
        If allImages.Count = 0 Then GoTo NoNeFound

        ' get an Random Image for the given SourceType
        Return allImages(New Random().Next(0, allImages.Count)).ToString
NoNeFound:
        ' Return an Error-Image FilePath
        If source = ImageSourceType.Local _
        Then Return myOldPathsAccessor.PathImageErrorNoLocalImages _
        Else Return myOldPathsAccessor.NoUrlFilesSelected
    End Function

    ''' <summary>
    ''' Gets a random image URI for the given genre and sourcetype (URL or Local)..
    ''' </summary>
    ''' <param name="genre">Determines the genre to get a random image for.</param>
    ''' <param name="source">Determines the source to get a random image for.</param>
    ''' <returns>The URI of a random Image.</returns>
    Friend Function GetRandomImage(ByVal genre As ImageGenre, ByVal source As ImageSourceType) As String
        ' Get all definitions for Images.
        Dim dicFilePaths As Dictionary(Of ImageGenre, ImageDataContainer) = GetImageData()

        ' Check if the given Genre is found.
        If dicFilePaths.Keys.Contains(genre) Then
            ' get an Random Image
            Return dicFilePaths(genre).Random(source)
        Else
            ' Return the Error-Image FilePath
            If source = ImageSourceType.Local _
            Then Return Application.StartupPath & "\Images\System\NoLocalImagesFound.jpg" _
            Else Return Application.StartupPath & "\Images\System\NoURLFilesSelected.jpg"
        End If
    End Function

#End Region ' Get Random Image

    ''' <summary>
    ''' Set the currently displayed image to eventArgs.ImageMetaData
    ''' </summary>
    ''' <param name="eventArgs"></param>
    Private Sub QueryImage(eventArgs As ShowImageEventArgs)
        eventArgs.ImageMetaData = myDisplayedImage
    End Sub

    ''' <summary>
    ''' Displays the image passed in as <paramref name="imageToShow"/> to the window
    ''' </summary>
    ''' <param name="imageToShow">The local path to the image to display.</param>
    <Obsolete("Use the version that takes ImageMetaData")>
    Public Sub ShowImage(imageToShow As String, ignored As Boolean)
        If FormLoading Then
            Return
        End If

        If String.IsNullOrWhiteSpace(imageToShow) Then
            Dim lazyText As String = "The given imagepath was NULL."
            Throw New ArgumentNullException(NameOf(imageToShow), "No image file was specified.")
        End If
        If Not File.Exists(imageToShow) Then
            Throw New ArgumentException(NameOf(imageToShow), imageToShow + " does not exist.")
        End If
        mainPictureBox.Image = Image.FromFile(imageToShow)
    End Sub

    Public Sub ShowImage(imageToShow As ImageMetaData)
        If FormLoading Then
            Return
        End If

        Dim imageData As Image = Nothing
        If imageToShow.SourceId = ImageSource.Local AndAlso Not File.Exists(imageToShow.FullFileName) Then
            Throw New ArgumentException(NameOf(imageToShow), imageToShow.ItemName + " does not exist.")
        End If

        If imageToShow.SourceId = ImageSource.Local Then
            imageData = Image.FromFile(imageToShow.FullFileName)
        ElseIf imageToShow.SourceId = ImageSource.Remote Then
            Dim webClient As Net.WebClient = New Net.WebClient()
            imageData = New Bitmap(New MemoryStream(webClient.DownloadData(imageToShow.FullFileName)))
        End If

        myDisplayedImage = imageToShow
        mainPictureBox.Image = imageData
    End Sub
#Region "---------------------------------------------------- BWimageSync -----------------------------------------------------"

#Region "---------------------------------------------------- Declarations ----------------------------------------------------"

    ''' <summary>
    ''' Modified Backgroundworker to load and save images on a different thread, with tth possibility to trigger 
    ''' the RunWorkerCompleted-Event manually
    ''' </summary>
    Private WithEvents BWimageFetcher As New Tease_AI.BackgroundWorkerSyncable With {.WorkerReportsProgress = True}

    ''' <summary>
    ''' Object to pass data to a differnt thread.
    ''' </summary>
    Private Class ImageFetchObject
        Public ImageLocation As String = ""

        Public Source As ImageSourceType

        Private _StoreDirectory As String = ""

        Public Property StoreDirectory As String
            Get
                Return _StoreDirectory
            End Get
            Set(value As String)
                If value Is Nothing Then _StoreDirectory = ""
                If Not value.EndsWith("\") And value <> "" Then
                    _StoreDirectory = value & "\"
                Else
                    _StoreDirectory = value

                End If
            End Set
        End Property

        Public FetchedImage As Image = Nothing
    End Class

#End Region ' Declarations

    ''' <summary>
    ''' Downloads or loads local or remote Images and displays them afterwards.
    ''' </summary>
    Private Sub BWimageFetcher_DoWork(sender As Object, e As DoWorkEventArgs) Handles BWimageFetcher.DoWork
        mreImageanimator.Set()

        Dim errorImagePath As String = myOldPathsAccessor.PathImageErrorOnLoading


        With CType(e.Argument, ImageFetchObject)
            Try
retryLocal: ' If an exception occures the function is restarted and the Errorimage is loaded.

                If .ImageLocation = "" Or .ImageLocation = Nothing Then
                    Throw New ArgumentException("The given filepath was empty.")
                ElseIf .ImageLocation = "" Then
                    Throw New ArgumentNullException("The given filepath was NULL.")
                ElseIf .ImageLocation.Contains("/") And .ImageLocation.Contains("://") Then
                    Dim s As String = ""

                    If .StoreDirectory <> "" Then
                        s = .StoreDirectory & Path.GetFileName(.ImageLocation)

                        If Not Directory.Exists(s) AndAlso File.Exists(s) Then
                            s = ""
                        End If
                    End If

                    ' Download the image
                    .FetchedImage = Common.DownloadRemoteImageFile(.ImageLocation, s, BWimageFetcher)

                    If .FetchedImage Is Nothing Then _
                  Throw New NullReferenceException("The result downloading """ &
                                           .ImageLocation.ToString & """ was empty.")
                Else
                    .FetchedImage = Image.FromFile(.ImageLocation)
                End If
                Debug.Print("ImageFetch - DoWork - Done")
            Catch ex As ThreadAbortException
                Debug.Print("ImageFetch - DoWork - Thread aborted.")
                e.Result = e.Argument
            Catch ex As Exception When .ImageLocation <> errorImagePath
                Debug.Print("ImageFetch - DoWork - 1st Exception perfomaing fallback")
                Log.WriteError("Error loading Image: """ & .ImageLocation & """", ex,
                        "Error loading image. Performing fallback to errorimage.")
                .ImageLocation = errorImagePath
                GoTo retryLocal
            Catch ex As Exception
                Debug.Print("ImageFetch - DoWork - 2nd Exception - fallback failed.")
                Log.WriteError("Fallback to errorimage """ & .ImageLocation & """ failed ",
                        ex, "Error loading image")
            End Try
        End With

        ' Stop ImageAnimations 
        mreImageanimator.Reset()
        ' Return the fetched data to the UI-Thread
        e.Result = e.Argument

        ' Exit sub, when the function was called on a BackgroundWorker Thread.
        If InvokeRequired Then Exit Sub
        BWimageFetcher_RunWorkerCompleted(Nothing, New System.ComponentModel.RunWorkerCompletedEventArgs(e.Argument, Nothing, False))
        '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°° END of Thread °°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
    End Sub

    ''' <summary>
    ''' Reports the download progress of an onlineimage.
    ''' </summary>
    Private Sub BWimageFetcher_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BWimageFetcher.ProgressChanged
        ' Unhide Progressbar 
        If e.ProgressPercentage > 0 Then ProgressBar_BGW_Images.Visible = True

        ' Set Maximum
        ProgressBar_BGW_Images.Maximum = 100

        ' Verify value
        If e.ProgressPercentage > ProgressBar_BGW_Images.Maximum Then Exit Sub

        ' Display Value
        ProgressBar_BGW_Images.Value = e.ProgressPercentage

        If e.ProgressPercentage >= 100 Then ProgressBar_BGW_Images.Visible = False
    End Sub

    Private Sub BWimageFetcher_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BWimageFetcher.RunWorkerCompleted
        Try
            ' Change Cursor back to original
            Me.BeginInvoke(Sub() mainPictureBox.Cursor = Cursors.Arrow)

            ' Hide ProgressBar 
            ProgressBar_BGW_Images.Visible = False

            If TypeOf e.Error Is TimeoutException Then Debug.Print(e.Error.Message)
            If e.Error IsNot Nothing Then Exit Sub

            If e.Cancelled Then
                MainPictureboxSetImage(New Bitmap(Image.FromFile(myOldPathsAccessor.PathImageErrorOnLoading)), "")
                Exit Sub
            End If

            If TypeOf e.Result Is ImageFetchObject Then

                Dim FetchResult As ImageFetchObject = e.Result

                ' Set the fetched image and release thte fetched one.
                MainPictureboxSetImage(FetchResult.FetchedImage,
                                       FetchResult.ImageLocation)

                If FetchResult.FetchedImage IsNot Nothing Then _
                    FetchResult.FetchedImage.Dispose()

                Debug.Print("ImageFetch - RunWorkerCompleted - Done" & vbCrLf &
                        "	ImageLocation: " & FetchResult.ImageLocation)
            End If
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                     All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Log.WriteError("An Exception occurred while displaying image: " & vbCrLf & ex.Message,
                     ex, "Error Displaying image.")
        End Try
    End Sub

#End Region ' BWimageSync

    ''' <summary>
    ''' Clears the MainPictureBox and disposes the current image.
    ''' </summary>
    Public Sub ClearMainPictureBox()

        'MainPictureboxSetImage(Nothing, "")
        MainPictureboxSetImage(New Bitmap(Image.FromFile(Application.StartupPath & "\Images\System\Black.jpg")), "")

    End Sub
    ''' <summary>
    ''' Sets the current Image in MainPicturebox without causing a permanent deadlock. The 
    ''' Resaouces of the current image are released.
    ''' </summary>
    ''' <param name="newImage">The Image to set. It is safe to dispose the given image afterwards.
    ''' <para>If the given images is Nothing/NULL the Mainbicturebox will be cleared.</para></param>
    ''' <param name="ImagePath"></param>
    Sub MainPictureboxSetImage(ByVal newImage As Image, ByVal ImagePath As String)
        Try
            ' This starts the ImageAnimator-Thread again. 
            ' Assigning a new image to the Picturebox, while the ImageAnimator is 
            ' currently stopped, will result in a deadlock!
            mreImageanimator.Set()

            ' Release all ressources 
            If mainPictureBox.Image IsNot Nothing Then
                Dim OldImage As Image = mainPictureBox.Image
                OldImage.Dispose()
                GC.Collect()
            End If

            'Set the new image and redraw the control
            If newImage IsNot Nothing Then
                mainPictureBox.Image = newImage.Clone

                If My.Settings.CBStretchLandscape Then

                    If mainPictureBox.Image.Width > mainPictureBox.Image.Height Then
                        mainPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                    Else
                        mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom
                    End If
                Else
                    mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom
                End If

                mainPictureBox.Invalidate()
                mainPictureBox.Refresh()
            Else
                ImagePath = ""
            End If

            ' Updeate the pathimformations.
            If ImagePath <> myOldPathsAccessor.PathImageErrorOnLoading Then
                ssh.ImageLocation = ImagePath
                LBLImageInfo.Text = ImagePath
                mainPictureBox.ImageLocation = ImagePath
            Else
                ssh.ImageLocation = ""
                LBLImageInfo.Text = ""
                mainPictureBox.ImageLocation = ""
            End If

            ' Update the DommeTag-App.
            CheckDommeTags()

            ' Update the the picturestrip, when it's opened.
            If PictureStrip.Visible Then
                PictureStrip_Opening(Nothing, Nothing)
            End If

        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                            All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Log.WriteError("Unable to set image in MainPictureBox: " & ex.Message,
                                ex, "MainPictureboxSetImage")
        End Try
    End Sub

#Region "-------------------------------------------ImageAnimator----------------------------------------"
    '*******************************************************************************
    '
    ' This Code Section is to fix a bug when displaying some GIFs. 
    ' Sometimes the imageanimator Class is blocking the UI-Thread while animating
    ' those images. This error can be reproduced with single images. 
    ' To Avoid this a WatchDog-Timer has been added. This timer  has to  be 
    ' resetted within a certain time, otherwise it triggers an Event to force
    ' animation to stop.
    '*******************************************************************************
    ''' ============================================================================
    ''' <summary>
    ''' WatchDegTimer to detect if a gif-image is blocking the UI-Thread.
    ''' </summary>
    Private WithEvents WatchDogImageAnimator As New My.WatchDog("WatchDog-ImageAnimator")

    ''' <summary>
    ''' Eventhandler to force-stop the ImageAnimator. This Method adds a FrameChanged-Eventhandler to
    ''' to the ImageAnimator. This Handler forces the ImageAnimator to stop until 
    ''' <see cref="mreImageanimator"/> is set again. <para>Make sure you don't assing a new image, 
    ''' until <see cref="mreImageanimator"/> is not set. This would cause a permanent deadlock! </para>
    ''' </summary>
    <DebuggerStepThrough>
    Sub WatchDogImageAnimator_WatchDogReset(sender As Object,
                                            ByVal e As EventArgs) Handles WatchDogImageAnimator.WatchDogReset
        Debug.Print("ImageAnimator-WatchDogReset on Image: " & ssh.ImageLocation)
        ' Prevent the ImageAnimator to stop
        mreImageanimator.Reset()

        ' Add a custom eventhandler to the imageanimator, if an
        ' animatable Image is displayed.
        If mainPictureBox.Image IsNot Nothing _
            AndAlso ImageAnimator_OnFrameChangedAdded = False _
            AndAlso ImageAnimator.CanAnimate(mainPictureBox.Image) Then
            ImageAnimator.Animate(mainPictureBox.Image, AddressOf ImageAnimator_OnFrameChanged)

            ImageAnimator_OnFrameChangedAdded = True
        End If
    End Sub

    ''' ============================================================================
    ''' <summary>
    ''' Indicates if the Eventhandler  ImageAnimator_OnFrameChanged has been added to ImageAnimatorThread.
    ''' </summary>
    Dim ImageAnimator_OnFrameChangedAdded As Boolean
    ''' <summary>
    ''' Signals the ImageAnimatorThread to suspend when the signal is set.
    ''' </summary>
    Shared mreImageanimator As New ManualResetEvent(True)

    ''' <summary>
    ''' This Method will stop the ImageAnimatorThread
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub ImageAnimator_OnFrameChanged(sender As Object, e As EventArgs)
        '×××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××
        '						ImageAnimator Thread - Beyond this Line: INVOKE IS REQUIRED...
        ' If the manual reset event is not set, the Thread will wait until it is set.
        ' The Timeout is used to prevent permanent DeadLocks.
        If mreImageanimator.WaitOne(New TimeSpan(0, 0, 0, 0, 10000)) Then
            'Debug.Print("No image animation stop")
        Else
            'Debug.Print("------------------------------------------Image animation stopped")
        End If
        '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°° END of Thread °°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
    End Sub
#End Region ' ImageAnimator

#Region "-------------------------------------- Image Administration ------------------------------------"
    ''' =========================================================================================================
    ''' <summary>
    ''' disposes the resources from the Main Picturebox and deletes the current image from Filesystem including 
    ''' TagList, LikedList and DislikedList.Applies only to Images not loacted in DommeImageDir or ContactImageDirs.
    ''' <para>Rethrows all Exceptions!</para>
    ''' <param name="restrictToLocal">Set TRUE to delete only LocalImages. Set to False to delete local 
    ''' images as well as remote image links</param>
    ''' </summary>
    Private Sub DeleteCurrentImage(ByVal restrictToLocal As Boolean)
        If ssh.ImageLocation Is Nothing Then Throw New ArgumentException("The given path was empty.")
        If ssh.ImageLocation = "" Then Throw New ArgumentException("The given path was empty.")
        Try
            If IsUrl(ssh.ImageLocation) Then
                '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '									Online Images
                '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                Debug.Print("××××××××××××××××××××××× Deleting remote Image: " & ssh.ImageLocation & "×××××××××××××××××××××××")

                If restrictToLocal Then Throw New ArgumentException("Can't delete remote files!")
                Dim rtnInt As Integer = 0
                ' #################### Remove from LikeList ####################
                rtnInt += RemoveFromLikeList(ssh.ImageLocation)
                ' ################## Remove from DislikeList ###################
                rtnInt += RemoveFromDislikeList(ssh.ImageLocation)
                ' ################## Remove from LocalTagList ##################
                rtnInt += RemoveFromLocalTagList(ssh.ImageLocation)
                ' #################### Remove from URL-Lists ###################
                rtnInt += RemoveFromUrlFiles(ssh.ImageLocation)

                ' Save the Path temporary -> CLearMainPictureBox will flush ImageLocation
                Dim tmpPath As String = ssh.ImageLocation
                ' Dispose the Image from RAM
                ClearMainPictureBox()
                If rtnInt < 1 Then Throw New Exception("The URL was not successfully deleted.")
#If TRACE Then
                Trace.WriteLine("Deleted image-link: " & tmpPath)
#End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                ' Online Images - End
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Else
                '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '									Local Images
                '▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                Debug.Print("××××××××××××××××××××××× Deleting local Image: " & ssh.ImageLocation & "×××××××××××××××××××××××")

                ' Check if all requirements are met.
                Dim tmpstring As String = Path.GetDirectoryName(ssh.ImageLocation)
                If myDirectory.Exists(tmpstring) = False Then
                    Throw New DirectoryNotFoundException("The given directory was not found: """ &
                                                         Path.GetDirectoryName(tmpstring) & """")
                End If

                If File.Exists(ssh.ImageLocation) = False Then
                    Throw New FileNotFoundException("The given File was not found: """ &
                                                    ssh.ImageLocation & """")
                End If

                If ssh.ImageLocation.ToLower.StartsWith(Application.StartupPath.ToLower & "\Images\System\".ToLower) Then _
                    Throw New ArgumentException("System iamges are not allowed to delete.")
                If ssh.SlideshowMain.ImageList.Contains(ssh.ImageLocation) Then _
                    Throw New ArgumentException("Domme-Slideshow images are not allowed to delete!")
                If ssh.SlideshowContact1.ImageList.Contains(ssh.ImageLocation) Then _
                    Throw New ArgumentException("Contact1-Slideshow images are not allowed to delete!")
                If ssh.SlideshowContact2.ImageList.Contains(ssh.ImageLocation) Then _
                    Throw New ArgumentException("Contact2-Slideshow images are not allowed to delete!")
                If ssh.SlideshowContact3.ImageList.Contains(ssh.ImageLocation) Then _
                    Throw New ArgumentException("Contact3-Slideshow images are not allowed to delete!")

                Dim settings As Settings = mySettingsAccessor.GetSettings()
                Dim glitterContact As DommeSettings = GetContactFromLocation(ssh.ImageLocation, settings)

                If ssh.ImageLocation.ToLower().StartsWith(settings.Domme.GlitterImageDirectory.ToLower()) Then _
                    Throw New Exception("Images in Domme-Image-Dir are not allowed to delete!")
                If ssh.ImageLocation.ToLower.StartsWith(settings.Apps.Glitter.Contact1.GlitterImageDirectory.ToLower()) Then _
                    Throw New Exception("Images in Contact1-Image-Dir are not allowed to delete!")
                If ssh.ImageLocation.ToLower.StartsWith(settings.Apps.Glitter.Contact2.GlitterImageDirectory.ToLower()) Then _
                    Throw New Exception("Images in Contact2-Image-Dir are not allowed to delete!")
                If ssh.ImageLocation.ToLower.StartsWith(settings.Apps.Glitter.Contact2.GlitterImageDirectory.ToLower()) Then _
                    Throw New Exception("Images in contact3-Image-Dir are not allowed to delete!")


                ' #################### Remove from ####################
                RemoveFromLikeList(ssh.ImageLocation)
                ' ################## Remove from DislikeList ###################
                RemoveFromDislikeList(ssh.ImageLocation)
                ' ################## Remove from LocalTagList ##################
                RemoveFromLocalTagList(ssh.ImageLocation)

                ' Save the Path temporary -> CLearMainPictureBox will flush ImageLocation
                Dim tmpPath As String = ssh.ImageLocation
                ' Dispose the Image from RAM
                ClearMainPictureBox()
                ' Delete the File from disk
                My.Computer.FileSystem.DeleteFile(tmpPath)

#If TRACE Then
                Trace.WriteLine("Deleted local File: " & tmpPath)
#End If

                If File.Exists(tmpPath) Then Throw New Exception("The image was not successfully deleted.")
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                ' Local Images - End
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            End If
        Catch ex As Exception
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            '                                         All Errors
            '▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Determine which glitter contact is sending a message.
    ''' </summary>
    ''' <param name="sentMessage"></param>
    ''' <param name="settings"></param>
    ''' <returns></returns>
    Private Shared Function GetContactFromLocation(sentMessage As String, settings As Settings) As DommeSettings
        If sentMessage.Contains("@Contact1") Then
            Return settings.Apps.Glitter.Contact1
        End If

        If sentMessage.Contains("@Contact2") Then
            Return settings.Apps.Glitter.Contact2
        End If

        If sentMessage.Contains("@Contact3") Then
            Return settings.Apps.Glitter.Contact3
        End If
        Return settings.Domme
    End Function

    ''' =========================================================================================================
    ''' <summary>
    ''' Removes the given path from the likedImage file.
    ''' </summary>
    ''' <param name="path">The Path/Url to remove from the file.</param>
    ''' <return>The number of lines deleted.</return>
    Friend Function RemoveFromLikeList(ByVal path As String) As Integer
        Return TxtRemoveLine(myOldPathsAccessor.LikedImages, path)
    End Function

    ''' =========================================================================================================
    ''' <summary>
    ''' Removes the given path from the DislikedImage file.
    ''' </summary>
    ''' <param name="path">The Path/Url to remove from the file.</param>
    ''' <return>The number of lines deleted.</return>
    Friend Function RemoveFromDislikeList(ByVal path As String) As Integer
        Return TxtRemoveLine(myOldPathsAccessor.DislikedImages, path)
    End Function

    ''' =========================================================================================================
    ''' <summary>
    ''' Removes the given path from the LocalImageTag file.
    ''' </summary>
    ''' <param name="path">The Path/Url to remove from the file.</param>
    ''' <return>The number of lines deleted.</return>
    Friend Function RemoveFromLocalTagList(ByVal path As String) As Integer
        Return TxtRemoveLine(myOldPathsAccessor.LocalImageTags, path)
    End Function

    ''' =========================================================================================================
    ''' <summary>
    ''' Removes the given path from the LocalImageTag file.
    ''' </summary>
    ''' <param name="path">The Path/Url to remove from the file.</param>
    Friend Function RemoveFromUrlFiles(ByVal path As String)
        Dim rtnInt As Integer = 0

        Dim CustomURLFileList As New List(Of String)

        ' Get all URL-Files associated with image-Genres 
        ' Their location can be outside of \Images\System\URL Files\
        For Each imgDC As ImageDataContainer In GetImageData.Values
            If imgDC.UrlFile IsNot Nothing AndAlso imgDC.UrlFile <> "" Then
                CustomURLFileList.Add(imgDC.UrlFile)
            End If
        Next

        ' Remove all, where the directory does not exists.
        CustomURLFileList.RemoveAll(Function(x) Not Directory.Exists(IO.Path.GetDirectoryName(x)))
        ' Remove all, where the file itself does not exists.
        CustomURLFileList.RemoveAll(Function(x) Not File.Exists(x))

        ' Find the URL in all URLFiles located in Standard Directory
        If Directory.Exists(Application.StartupPath & "\Images\System\URL Files\") Then
            Dim foundFiles As ObjectModel.ReadOnlyCollection(Of String) =
            FileIO.FileSystem.FindInFiles(Application.StartupPath & "\Images\System\URL Files\",
                                          path, True, FileIO.SearchOption.SearchTopLevelOnly)

            ' Distinct join the 2 Lists 
            CustomURLFileList = CustomURLFileList.Union(foundFiles).ToList
        End If

        ' Delete the URL from all Files 
        For Each filePath As String In CustomURLFileList
            rtnInt += TxtRemoveLine(filePath, path)
        Next

        Return rtnInt
    End Function


#End Region ' Image administration

End Class