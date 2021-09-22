Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text.RegularExpressions
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors

Public Enum ContactType
    [Nothing]
    Domme
    Contact1
    Contact2
    Contact3
End Enum

<Serializable>
Public Class ContactData

    Public Property Contact As ContactType = ContactType.Nothing

    ''' <summary>
    ''' List of images in the current slideshow
    ''' </summary>
    ''' <returns></returns>
    Public Property ImageList As New List(Of String)

    Public Property RecentFolders As New List(Of String)

    ''' <summary>
    ''' Index of the current image in <see cref="ImageList"/>
    ''' </summary>
    ''' <returns></returns>
    Public Property Index As Integer = -1
    Public ReadOnly Property SlideShowImages As List(Of ImageMetaData)

    <NonSerialized> <OptionalField>
    Dim ImageTagCache As New Dictionary(Of String, ImageTagCacheItem)(StringComparison.OrdinalIgnoreCase)
    Private ReadOnly mySettingsAccessor As ISettingsAccessor
    Private ReadOnly myImageMetaDataService As IImageAccessor
    Private ReadOnly myRandomNumberService As IRandomNumberService

    Sub New(newContactType As ContactType)
        Contact = newContactType
        mySettingsAccessor = ApplicationFactory.CreateSettingsAccessor()
        myImageMetaDataService = ApplicationFactory.CreateImageMetaDataService()
        myRandomNumberService = ApplicationFactory.CreateRandomNumberService()

        SlideShowImages = New List(Of ImageMetaData)()
    End Sub

    ''' <summary>
    ''' Fixes errors caused by missing optional fields after deserialisation.
    ''' </summary>
    ''' <param name="sc"></param>
    <OnDeserialized>
    Sub OnDeserialized(sc As StreamingContext)
        If ImageTagCache Is Nothing Then ImageTagCache = New Dictionary(Of String, ImageTagCacheItem)(StringComparison.OrdinalIgnoreCase)
    End Sub

    Private Function ValidateImageDirectory(contactType As ContactType) As Boolean
        Dim dommeSettings As DommeSettings = GetDommeSettings(contactType)
        Dim folder As String = dommeSettings.GlitterImageDirectory
        Dim name As String = dommeSettings.Name

        Return Directory.Exists(folder)
    End Function

    Friend Function GetRandom(tp As ContactType) As List(Of String)
        If ValidateImageDirectory(tp) Then
            Return LoadRandom(GetDommeSettings(tp).GlitterImageDirectory)
        Else
            Return New List(Of String)
        End If
    End Function

    Function LoadRandom(baseDirectory As String) As List(Of String)
        If Directory.Exists(baseDirectory) = False Then _
            Throw New DirectoryNotFoundException("The given slideshow base diretory """ & baseDirectory & """ was not found.")

        ' Read all subdirectories in base folder.
        Dim subDirs As List(Of String) = myDirectory.GetDirectories(baseDirectory).ToList
        Dim exclude As New List(Of String)
nextSubDir:
        ' Check if there are folders left.
        If subDirs.Count = 0 And exclude.Count > 0 Then
            Dim first As String = RecentFolders(0)
            RecentFolders.Remove(first)
            exclude.Remove(first)
            subDirs.Add(first)
        ElseIf subDirs.Count <= 0 And exclude.Count = 0 Then
            Throw New DirectoryNotFoundException("There are no subdirectories conataining images in """ & baseDirectory & """.")
        End If

        ' Get a random folder in base directory.
        Dim rndFolder As String = subDirs(New Random().Next(0, subDirs.Count))

        If RecentFolders.Contains(rndFolder) Then
            exclude.Add(rndFolder)
            subDirs.Remove(rndFolder)
            GoTo nextSubDir
        End If

        ' Read all imagefiles in random folder.
        Dim imageFiles As List(Of String)

        If My.Settings.CBSlideshowSubDir Then
            imageFiles = myDirectory.GetFilesImages(rndFolder, SearchOption.AllDirectories)
        Else
            imageFiles = myDirectory.GetFilesImages(rndFolder, SearchOption.TopDirectoryOnly)
        End If

        If imageFiles.Count <= 0 Then
            ' No imagefiles in subdirectory. Remove from list and try next folder
            subDirs.Remove(rndFolder)
            GoTo nextSubDir
        Else
            ' Imagefiles found -> Everything fine and done
            RecentFolders.Add(rndFolder)
            Return imageFiles
        End If

    End Function

    Function GetDommeSettings(contactType As ContactType) As DommeSettings
        Dim settings As Settings = mySettingsAccessor.GetSettings()
        Select Case contactType
            Case ContactType.Domme
                Return settings.Domme
            Case ContactType.Contact1
                Return settings.Apps.Glitter.Contact1
            Case ContactType.Contact2
                Return settings.Apps.Glitter.Contact2
            Case ContactType.Contact3
                Return settings.Apps.Glitter.Contact3
        End Select
        Throw New Exception("Unknown Contact Type")
    End Function

    Friend Function LoadNew() As Integer
        ImageList = GetRandom(Contact)
        Index = 0
        Return Index
    End Function

    Sub CheckInit()
        If Me.Index = -1 And Me.Contact <> ContactType.Nothing Then Me.LoadNew()
    End Sub

#Region "Navigation"
    Public Function GetNextImage() As ImageMetaData
        CheckInit()
        Dim newIndex = Index
        If My.Settings.CBSlideshowRandom Then
            newIndex = myRandomNumberService.Roll(0, ImageList.Count)
        ElseIf My.Settings.NextImageChance < myRandomNumberService.RollPercent() Then
            newIndex = Math.Max(newIndex - 1, 0)
        ElseIf newIndex >= ImageList.Count - 1 AndAlso My.Settings.CBNewSlideshow Then
            ' End of Slideshow start new
            newIndex = LoadNew()
        Else
            newIndex = Math.Min(newIndex + 1, ImageList.Count - 1)
        End If
        Index = newIndex

        Return GetImageMetaData(newIndex)
    End Function

    Public Function GetLastImage() As ImageMetaData
        Dim i As Integer = ImageList.Count
        Do
            i -= 1
        Loop Until File.Exists(ImageList(i))
        Index = i
        Return GetImageMetaData(i)
    End Function

    Public Function GetFirstImage() As ImageMetaData
        Dim i As Integer = -1
        Do
            i += 1
        Loop Until File.Exists(ImageList(i))
        Index = i
        Return GetImageMetaData(i)
    End Function

    Public Function GetCurrent() As ImageMetaData
        Return GetImageMetaData(Index)
    End Function

    Private Function GetImageMetaData(index As Integer) As ImageMetaData
        If ImageList.Count > 0 AndAlso index > -1 Then
            Dim file As String = ImageList(index)
            Dim imageMetaData = New ImageMetaData
            imageMetaData.Id = -1
            imageMetaData.FullFileName = file
            imageMetaData.ItemName = Path.GetFileName(file)
            imageMetaData.MediaContainerId = -1
            imageMetaData.GenreId = ImageGenre.Glitter
            imageMetaData.SourceId = ImageSource.Local
            Return imageMetaData
        End If

        Return Nothing
    End Function

    <Obsolete("migrate to GetCurrent")>
    Friend Function CurrentImage() As String
        If ImageList.Count > 0 And Index > -1 Then
            Return ImageList(Index)
        Else
            Return String.Empty
        End If
    End Function

    Friend Function NavigateFirst() As String
        CheckInit()
        If Me.ImageList.Count > 0 Then
            Index = 0
            Return CurrentImage()
        Else
            Return String.Empty
        End If
    End Function

    <Obsolete("migrate to GetNextImage")>
    Friend Function NavigateNextTease() As String
        CheckInit()

        If My.Settings.CBSlideshowRandom Then
            ' get Random Image
            Index = New Random().Next(0, ImageList.Count)
        ElseIf My.Settings.NextImageChance < New Random().Next(0, 101) Then
            ' Randomly backwards
            Index -= 1
            If Index < 0 Then Index = 0
        ElseIf Index >= ImageList.Count - 1 AndAlso My.Settings.CBNewSlideshow Then
            ' End of Slideshow start new
            LoadNew()
        ElseIf Index >= ImageList.Count - 1 Then
            ' End of Slideshow return last
            Index = ImageList.Count - 1
        Else
            ' Next image
            Index += 1
        End If

        Return CurrentImage()
    End Function

    Friend Function NavigateLast() As String
        CheckInit()
        If Me.ImageList.Count > 0 Then
            Index = ImageList.Count - 1
            Return CurrentImage()
        Else
            Return String.Empty
        End If
    End Function
#End Region

#Region "Tagged Images"

    ''' <summary>
    ''' Used for caching tagged image results.
    ''' </summary>
    Private Class ImageTagCacheItem
        Friend TagImageList As New List(Of String)
        Friend LastPicked As String = ""

        Sub New()
        End Sub
    End Class


    '    ''' <summary>
    '    ''' Find images in the current slideshow with tags in <paramref name="imageTagsString"/>
    '    ''' </summary>
    '    ''' <param name="imageTagsString">tags to search for as a comma seperated string.</param>
    '    ''' <returns>Returns a String representing the ImageLocation for the found image. If none was found it will 
    '    ''' return an empty string.</returns>
    '    Public Function GetTaggedImage(imageTagsString As String, Optional rememberResult As Boolean = False) As String
    '        GetTaggedImage = ""
    '        Dim imagePaths As ImageTagCacheItem = GetImageListByTag(imageTagsString)

    'tryNextImage:
    '        Dim currImgIndex As Integer = ImageList.IndexOf(CurrentImage)
    '        Dim rtnPath As String = ""
    '        Dim CurrDist As Integer = 999999

    '        For Each str As String In imagePaths.TagImageList
    '            Dim IndexInList As Integer = ImageList.IndexOf(str)
    '            ' Calculate the distance of ListIndex from the FoundFile to CurrentImage
    '            Dim FileDist As Integer = IndexInList - currImgIndex
    '            ' Convert negative values to positive by multipling (-) x (-) = (+) 
    '            If FileDist < 0 Then FileDist *= -1
    '            ' Check if the distance is bigger than the previous one
    '            If FileDist <= CurrDist Then
    '                ' Yes: We will set this file and save its distance
    'SetForwardImage:
    '                rtnPath = str
    '                CurrDist = FileDist
    '            ElseIf imagePaths.LastPicked = rtnPath AndAlso New Random().Next(0, 101) > 60 Then
    '                ' The last Picked image is the same as last time.
    '                GoTo SetForwardImage
    '            Else
    '                ' As the list is in the Same order as the Slideshow-List,
    '                ' we can stop searching, when the value is getting bigger.
    '                Exit For
    '            End If
    '        Next
    '        If String.IsNullOrWhiteSpace(rtnPath) Then
    '            Exit Function
    '        ElseIf Not File.Exists(rtnPath) Then
    '            ImageTagCache(imageTagsString).TagImageList.Remove(rtnPath)
    '            GoTo tryNextImage
    '        Else
    '            If rememberResult Then imagePaths.LastPicked = rtnPath
    '            Return rtnPath
    '        End If
    '    End Function

    Public Function GetTaggedImage(requestedTags As List(Of ItemTag)) As ImageMetaData
        Dim imageMetaDatas As List(Of ImageMetaData) = FilterImageListByTag((Path.GetDirectoryName(CurrentImage) & Path.DirectorySeparatorChar).ToLower(), requestedTags)
        Dim index As Integer = myRandomNumberService.Roll(0, imageMetaDatas.Count() - 1)
        Return imageMetaDatas(index)
    End Function

    ''' <summary>
    ''' Search images and return any that have been tagged with a tag in <paramref name="imageTags"/> and live in  <paramref name="slideShowFolder"/>
    ''' </summary>
    ''' <param name="slideShowFolder"></param>
    ''' <param name="imageTags"></param>
    ''' <returns></returns>
    Private Function FilterImageListByTag(slideShowFolder As String, imageTags As IEnumerable(Of ItemTag)) As List(Of ImageMetaData)
        Dim images As List(Of ImageMetaData) = New List(Of ImageMetaData)()

        For Each it As ItemTag In imageTags
            Dim taggedImages As List(Of ImageMetaData) = myImageMetaDataService.GetImagesWithTag(it.Id).Where(Function(imd) imd.FullFileName.ToLower().StartsWith(slideShowFolder)).ToList()
            images.AddRange(taggedImages)
        Next

        Return images
    End Function

    Private Function GetImageListByTag(imageTags As String) As ImageTagCacheItem
        Dim TargetFolder As String = Path.GetDirectoryName(CurrentImage) & Path.DirectorySeparatorChar
        Dim TagListFile As String = TargetFolder & "ImageTags.txt"

redo:
        If Not File.Exists(TagListFile) Then
            '===================================================================
            '							No Tag File
            Return New ImageTagCacheItem
        ElseIf ImageTagCache.Keys.Contains(imageTags) Then
            '===================================================================
            '						Previous cached result
            Dim rtnItem As ImageTagCacheItem = ImageTagCache(imageTags)

            If rtnItem.TagImageList.Count = 0 Then
                ' ´############## List was empty ################
                Return New ImageTagCacheItem
            ElseIf Not rtnItem.TagImageList(0).StartsWith(TargetFolder) Then
                ' ################ Wrong folder #################
                ImageTagCache.Remove(imageTags)
                GoTo redo
            Else
                ' ################# All fine ####################
                Return rtnItem
            End If
        Else
            '===================================================================
            '							 Read from File 
            Dim include As New List(Of String)
            Dim exclude As New List(Of String)
            Dim pathList As List(Of String) = File.ReadAllLines(TagListFile).ToList()
            Dim validExt As String() = Split(".jpg|.jpeg|.bmp|.png|.gif", "|")

            ' Replace case insensitive "not", to safely assign tags to their lists
            imageTags = Regex.Replace(imageTags, "\b(not)", "--", RegexOptions.IgnoreCase)

            ' Seperate Tags in given string.
            Dim S As String() = imageTags.Split({",", " "}, StringSplitOptions.RemoveEmptyEntries)

            ' Assign tags to lists.
            S.ToList.ForEach(Sub(x)
                                 If x.StartsWith("--") Then
                                     exclude.Add(x.Replace("--", ""))
                                 Else
                                     include.Add(x)
                                 End If
                             End Sub)

            ' Filter the List.
            pathList.RemoveAll(Function(x)
                                   ' Remove if given include tags are missing
                                   For Each tag As String In include
                                       If Not x.Contains(tag) Then Return True
                                   Next
                                   ' Remove if given exclude tags are present
                                   For Each notTag As String In exclude
                                       If x.Contains(notTag) Then Return True
                                   Next
                                   ' Remove all without valid extension
                                   Dim Ext As String = Path.GetExtension(Split(x)(0)).ToLower
                                   If Not validExt.Contains(Ext) Then Return True
                                   'Everything fine keep file
                                   Return False
                               End Function)

            '############################### Extract Filepaths ###############################
            ' Extract filepaths from list. An empty list doesn't matter here.
            Dim re As New Regex("(?:^.*(?:\.jpg|jpeg|png|bmp|gif)){1}",
                                RegexOptions.IgnoreCase Or RegexOptions.Multiline)
            ' Get the Matches. Since we can't search a generic list, we join it. 
            Dim mc As MatchCollection = re.Matches(String.Join(vbCrLf, pathList))

            ' Write the the ImagePaths back to list.
            pathList.Clear()
            For Each ma As Match In mc
                pathList.Add(TargetFolder & ma.Value)
            Next

            ' Add new item to cache and exit.
            GetImageListByTag = New ImageTagCacheItem() With {.TagImageList = pathList}
            ImageTagCache.Add(imageTags, GetImageListByTag)
            Return GetImageListByTag
        End If
    End Function

#End Region


End Class
