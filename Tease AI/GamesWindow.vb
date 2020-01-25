Imports System.IO
Imports TeaseAI.Common.Interfaces.Accessors
Imports TeaseAI.Common
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Data.RiskyPick
Imports TeaseAI.Common.Constants

Public Class GamesWindow

#Region "properties"
    Private ReadOnly Property mySelectedCase As Color = Color.Khaki
    Private ReadOnly Property RiskyPickCost As Integer = 0
    Dim randomizer As New Random
    Dim TempVal As Integer


    Dim MatchPhase As Integer = 0
    Dim Match1 As String
    Dim Match2 As String
    Dim MatchTemp As String
    Dim MatchChance As Integer
    Dim MatchesMade As Integer
    Dim GameOn As Boolean
    Dim MatchPot As Integer
    Dim ShuffleTick As Integer

    Dim SlotBetTemp As Integer

    Dim MatchList As New List(Of String)
    Dim CompareList As New List(Of String)

    Dim Pair1 As String
    Dim Pair2 As String
    Dim Pair3 As String
    Dim Pair4 As String
    Dim Pair5 As String
    Dim Pair6 As String
    Dim Pair7 As String
    Dim Pair8 As String
    Dim Pair9 As String

    Dim Match1A As String
    Dim Match2A As String
    Dim Match3A As String
    Dim Match4A As String
    Dim Match5A As String
    Dim Match6A As String

    Dim Match1B As String
    Dim Match2B As String
    Dim Match3B As String
    Dim Match4B As String
    Dim Match5B As String
    Dim Match6B As String

    Dim Match1C As String
    Dim Match2C As String
    Dim Match3C As String
    Dim Match4C As String
    Dim Match5C As String
    Dim Match6C As String

    Dim CardTick As Integer
    Dim CardBackImage As String

    Dim RevealCards As Boolean
    Dim RevealTick As Integer

    Dim CardSetup As Boolean

    Dim SlotTick1 As Integer
    Dim SlotTick2 As Integer
    Dim SlotTick3 As Integer

    Dim SlotList As New List(Of String)

    Dim Slot1Val As Integer
    Dim Slot2Val As Integer
    Dim Slot3Val As Integer

    Dim SlotBet As Integer
    Dim Payout As Integer

    Dim BoosterTick As Integer
    Dim BoosterListBronze As New List(Of String)
    Dim BoosterListSilver As New List(Of String)
    Dim BoosterListGold As New List(Of String)

    Public B1 As Integer
    Public B2 As Integer
    Public B3 As Integer
    Public B4 As Integer
    Public B5 As Integer
    Public B6 As Integer

    Public S1 As Integer
    Public S2 As Integer
    Public S3 As Integer
    Public S4 As Integer
    Public S5 As Integer
    Public S6 As Integer

    Public G1 As Integer
    Public G2 As Integer
    Public G3 As Integer
    Public G4 As Integer
    Public G5 As Integer
    Public G6 As Integer

    Dim CardVal As Integer

    Dim fileName1 As String

    Public RiskyState As Boolean
    Public RiskyPickOffer As RiskyPickOffer = New RiskyPickOffer()
    Public RiskyChoices As New List(Of String)

    Public RiskyRound As Integer
    Public RiskyChoiceCount As Integer


    Public RiskyPickChosenCaseNumber As Integer
    Public RiskyPickCount As Integer
    Public RiskyPickChosenCaseEdges As String
    Public RiskyInt As Integer
    Public RiskyTick As Integer

    Public HighestRisk As Integer
    Public LowestRisk As Integer

    Public EdgesOwed As Integer
    Public TokensPaid As Integer

    Public CardImage1 As Image
    Public CardImage2 As Image
    Public CardImage3 As Image
    Public CardImage4 As Image
    Public CardImage5 As Image
    Public CardImage6 As Image
    Public CardImage7 As Image
    Public CardImage8 As Image
    Public CardImage9 As Image
    Private ReadOnly mySettingsAccessor As ISettingsAccessor
    Private ReadOnly myPathsAccessor As PathsAccessor
#End Region

    Public Sub New()
        mySettingsAccessor = ServiceFactory.CreateSettingsAccessor()
        myPathsAccessor = ServiceFactory.CreatePathsAccessor()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" _
  (ByVal lpstrCommand As String, ByVal lpstrReturnString As String,
  ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer

    Public Sub DealMatchCards()

        If File.Exists(My.Settings.CardBack) Then
            CardBackImage = My.Settings.CardBack
        Else
            CardBackImage = Application.StartupPath & "\Scripts\" & MainWindow.DommePersonalityComboBox.Text & "\Apps\Games\_CardBackPicture.png"
        End If

        ClearMatchCache()

        M1A.Image = Image.FromFile(CardBackImage)
        M2A.Image = Image.FromFile(CardBackImage)
        M3A.Image = Image.FromFile(CardBackImage)
        M4A.Image = Image.FromFile(CardBackImage)
        M5A.Image = Image.FromFile(CardBackImage)
        M6A.Image = Image.FromFile(CardBackImage)

        M1B.Image = Image.FromFile(CardBackImage)
        M2B.Image = Image.FromFile(CardBackImage)
        M3B.Image = Image.FromFile(CardBackImage)
        M4B.Image = Image.FromFile(CardBackImage)
        M5B.Image = Image.FromFile(CardBackImage)
        M6B.Image = Image.FromFile(CardBackImage)

        M1C.Image = Image.FromFile(CardBackImage)
        M2C.Image = Image.FromFile(CardBackImage)
        M3C.Image = Image.FromFile(CardBackImage)
        M4C.Image = Image.FromFile(CardBackImage)
        M5C.Image = Image.FromFile(CardBackImage)
        M6C.Image = Image.FromFile(CardBackImage)

    End Sub

    Public Sub InitializeCards()


        'M1A.Enabled = True
        'M2A.Enabled = True
        'M3A.Enabled = True
        'M4A.Enabled = True
        'M5A.Enabled = True
        'M6A.Enabled = True

        'M1B.Enabled = True
        'M2B.Enabled = True
        'M3B.Enabled = True
        'M4B.Enabled = True
        'M5B.Enabled = True
        'M6B.Enabled = True

        'M1C.Enabled = True
        'M2C.Enabled = True
        'M3C.Enabled = True
        'M4C.Enabled = True
        'M5C.Enabled = True
        'M6C.Enabled = True

        DealMatchCards()

        MatchList.Clear()


        For i As Integer = 0 To FrmSettings.URLFileList.Items.Count - 1


            If File.Exists(Application.StartupPath & "\Images\System\URL Files\" & FrmSettings.URLFileList.Items(i) & ".txt") Then

                If FrmSettings.URLFileList.GetItemCheckState(i) = CheckState.Checked Then

                    Dim URLString As String = Application.StartupPath & "\Images\System\URL Files\" & FrmSettings.URLFileList.Items(i) & ".txt"
                    Dim CardReader As New System.IO.StreamReader(URLString)

                    While CardReader.Peek <> -1
                        MatchList.Add(CardReader.ReadLine())
                    End While


                    CardReader.Close()
                    CardReader.Dispose()

                End If

            End If
        Next

        If My.Settings.CBIncludeGifs = False Then
            For i As Integer = MatchList.Count - 1 To 0 Step -1
                If MatchList(i).Contains(".gif") Then MatchList.Remove(MatchList(i))
            Next
        End If


        Dim supportedExtensions As String

        If My.Settings.CBIncludeGifs = True Then
            supportedExtensions = "*.png,*.jpg,*.gif,*.bmp,*.jpeg"
        Else
            supportedExtensions = "*.png,*.jpg,*.bmp,*.jpeg"
        End If

        Dim files As String()


        If My.Settings.CBIHardcore = True And Directory.Exists(My.Settings.IHardcore) Then
            If FrmSettings.CBIHardcoreSD.Checked = True Then
                files = myDirectory.GetFiles(My.Settings.IHardcore, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IHardcore, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBISoftcore = True And Directory.Exists(My.Settings.ISoftcore) Then
            If My.Settings.ISoftcoreSD = True Then
                files = myDirectory.GetFiles(My.Settings.ISoftcore, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ISoftcore, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBILesbian = True And Directory.Exists(My.Settings.ILesbian) Then
            If My.Settings.ILesbianSD = True Then
                files = myDirectory.GetFiles(My.Settings.ILesbian, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ILesbian, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIBlowjob = True And Directory.Exists(My.Settings.IBlowjob) Then
            If My.Settings.IBlowjobSD = True Then
                files = myDirectory.GetFiles(My.Settings.IBlowjob, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IBlowjob, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIFemdom = True And Directory.Exists(My.Settings.IFemdom) Then
            If My.Settings.IFemdomSD = True Then
                files = myDirectory.GetFiles(My.Settings.IFemdom, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IFemdom, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBILezdom = True And Directory.Exists(My.Settings.ILezdom) Then
            If My.Settings.ILezdomSD = True Then
                files = myDirectory.GetFiles(My.Settings.ILezdom, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ILezdom, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIHentai = True And Directory.Exists(My.Settings.IHentai) Then
            If My.Settings.IHentaiSD = True Then
                files = myDirectory.GetFiles(My.Settings.IHentai, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IHentai, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIGay = True And Directory.Exists(My.Settings.IGay) Then
            If My.Settings.IGaySD = True Then
                files = myDirectory.GetFiles(My.Settings.IGay, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IGay, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIMaledom = True And Directory.Exists(My.Settings.IMaledom) Then
            If My.Settings.IMaledomSD = True Then
                files = myDirectory.GetFiles(My.Settings.IMaledom, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IMaledom, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBICaptions = True And Directory.Exists(My.Settings.ICaptions) Then
            If My.Settings.ICaptionsSD = True Then
                files = myDirectory.GetFiles(My.Settings.ICaptions, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ICaptions, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIGeneral = True And Directory.Exists(My.Settings.IGeneral) Then
            If My.Settings.IGeneralSD = True Then
                files = myDirectory.GetFiles(My.Settings.IGeneral, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IGeneral, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If





    End Sub

    Public Sub MatchGameStart()

        'M1A.Enabled = True
        'M2A.Enabled = True
        'M3A.Enabled = True
        'M4A.Enabled = True
        'M5A.Enabled = True
        'M6A.Enabled = True

        'M1B.Enabled = True
        'M2B.Enabled = True
        'M3B.Enabled = True
        'M4B.Enabled = True
        'M5B.Enabled = True
        'M6B.Enabled = True

        'M1C.Enabled = True
        'M2C.Enabled = True
        'M3C.Enabled = True
        'M4C.Enabled = True
        'M5C.Enabled = True
        'M6C.Enabled = True




        MatchList.Clear()


        For i As Integer = 0 To FrmSettings.URLFileList.Items.Count - 1


            If File.Exists(Application.StartupPath & "\Images\System\URL Files\" & FrmSettings.URLFileList.Items(i) & ".txt") Then

                If FrmSettings.URLFileList.GetItemCheckState(i) = CheckState.Checked Then

                    Dim URLString As String = Application.StartupPath & "\Images\System\URL Files\" & FrmSettings.URLFileList.Items(i) & ".txt"
                    Dim CardReader As New System.IO.StreamReader(URLString)

                    While CardReader.Peek <> -1
                        MatchList.Add(CardReader.ReadLine())
                    End While


                    CardReader.Close()
                    CardReader.Dispose()

                End If

            End If
        Next

        If My.Settings.CBIncludeGifs = False Then
            For i As Integer = MatchList.Count - 1 To 0 Step -1
                If MatchList(i).Contains(".gif") Then MatchList.Remove(MatchList(i))
            Next
        End If


        Dim supportedExtensions As String

        If My.Settings.CBIncludeGifs = True Then
            supportedExtensions = "*.png,*.jpg,*.gif,*.bmp,*.jpeg"
        Else
            supportedExtensions = "*.png,*.jpg,*.bmp,*.jpeg"
        End If

        Dim files As String()


        If My.Settings.CBIHardcore = True And Directory.Exists(My.Settings.IHardcore) Then
            If FrmSettings.CBIHardcoreSD.Checked = True Then
                files = myDirectory.GetFiles(My.Settings.IHardcore, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IHardcore, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBISoftcore = True And Directory.Exists(My.Settings.ISoftcore) Then
            If My.Settings.ISoftcoreSD = True Then
                files = myDirectory.GetFiles(My.Settings.ISoftcore, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ISoftcore, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBILesbian = True And Directory.Exists(My.Settings.ILesbian) Then
            If My.Settings.ILesbianSD = True Then
                files = myDirectory.GetFiles(My.Settings.ILesbian, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ILesbian, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIBlowjob = True And Directory.Exists(My.Settings.IBlowjob) Then
            If My.Settings.IBlowjobSD = True Then
                files = myDirectory.GetFiles(My.Settings.IBlowjob, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IBlowjob, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIFemdom = True And Directory.Exists(My.Settings.IFemdom) Then
            If My.Settings.IFemdomSD = True Then
                files = myDirectory.GetFiles(My.Settings.IFemdom, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IFemdom, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBILezdom = True And Directory.Exists(My.Settings.ILezdom) Then
            If My.Settings.ILezdomSD = True Then
                files = myDirectory.GetFiles(My.Settings.ILezdom, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ILezdom, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIHentai = True And Directory.Exists(My.Settings.IHentai) Then
            If My.Settings.IHentaiSD = True Then
                files = myDirectory.GetFiles(My.Settings.IHentai, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IHentai, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIGay = True And Directory.Exists(My.Settings.IGay) Then
            If My.Settings.IGaySD = True Then
                files = myDirectory.GetFiles(My.Settings.IGay, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IGay, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIMaledom = True And Directory.Exists(My.Settings.IMaledom) Then
            If My.Settings.IMaledomSD = True Then
                files = myDirectory.GetFiles(My.Settings.IMaledom, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IMaledom, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBICaptions = True And Directory.Exists(My.Settings.ICaptions) Then
            If My.Settings.ICaptionsSD = True Then
                files = myDirectory.GetFiles(My.Settings.ICaptions, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.ICaptions, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If

        If My.Settings.CBIGeneral = True And Directory.Exists(My.Settings.IGeneral) Then
            If My.Settings.IGeneralSD = True Then
                files = myDirectory.GetFiles(My.Settings.IGeneral, "*.*", SearchOption.AllDirectories)
            Else
                files = myDirectory.GetFiles(My.Settings.IGeneral, "*.*")
            End If
            Array.Sort(files)
            For Each fi As String In files
                If supportedExtensions.Contains(Path.GetExtension(LCase(fi))) Then
                    MatchList.Add(fi)
                End If
            Next
        End If




        If MatchList.Count < 1 Then Return


        Debug.Print("Prepare Card1")

        ShowCard1()


    End Sub

    Public Sub ShowCard1()
        Debug.Print("ShowCard1 Called")
Card1:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair1 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try

            CardImage1 = CardImage(Pair1)
            M1A.Image = CardImage1
            If M1A.Image Is Nothing Then GoTo Card1
            M2A.Image = CardImage1
            M2A_LoadCompleted()
        Catch ex As Exception
            Debug.Print("Problems")
            GoTo Card1
        End Try
    End Sub

    Private Sub M2A_LoadCompleted()
        'Private Sub M2A_LoadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles M2A.LoadCompleted
        Debug.Print("M2A Loaded")
        Debug.Print(CardSetup)
        If CardSetup = True Then
            M1A.Visible = True
            M2A.Visible = True
            ShowCard2()
        End If
    End Sub

    Public Sub ShowCard2()
        Debug.Print("ShowCard 2")
Card2:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair2 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M3A.Load(Pair2)
            'M4A.LoadAsync(Pair2)

            CardImage2 = CardImage(Pair2)
            M3A.Image = CardImage2
            If M3A.Image Is Nothing Then GoTo Card2
            M4A.Image = CardImage2
            M4A_LoadCompleted()

        Catch ex As Exception
            GoTo Card2
        End Try

    End Sub

    Private Sub M4A_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M3A.Visible = True
            M4A.Visible = True
            ShowCard3()
        End If
    End Sub

    Public Sub ShowCard3()
Card3:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair3 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M5A.Load(Pair3)
            'M6A.LoadAsync(Pair3)


            CardImage3 = CardImage(Pair3)
            M5A.Image = CardImage3
            If M5A.Image Is Nothing Then GoTo Card3
            M6A.Image = CardImage3
            M6A_LoadCompleted()

        Catch ex As Exception
            GoTo Card3
        End Try

    End Sub

    Private Sub M6A_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M5A.Visible = True
            M6A.Visible = True
            ShowCard4()
        End If
    End Sub


    Public Sub ShowCard4()

Card4:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair4 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M1B.Load(Pair4)
            'M2B.LoadAsync(Pair4)


            CardImage4 = CardImage(Pair4)
            M1B.Image = CardImage4
            If M1B.Image Is Nothing Then GoTo Card4
            M2B.Image = CardImage4
            M2B_LoadCompleted()
        Catch ex As Exception
            GoTo Card4
        End Try


    End Sub


    Private Sub M2B_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M1B.Visible = True
            M2B.Visible = True
            ShowCard5()
        End If
    End Sub


    Public Sub ShowCard5()
Card5:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair5 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M3B.Load(Pair5)
            'M4B.LoadAsync(Pair5)

            CardImage5 = CardImage(Pair5)
            M3B.Image = CardImage5
            If M3B.Image Is Nothing Then GoTo Card5
            M4B.Image = CardImage5
            M4B_LoadCompleted()

        Catch ex As Exception
            GoTo Card5
        End Try

    End Sub

    Private Sub M4B_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M3B.Visible = True
            M4B.Visible = True
            ShowCard6()
        End If
    End Sub


    Public Sub ShowCard6()

Card6:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair6 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M5B.Load(Pair6)
            'M6B.LoadAsync(Pair6)

            CardImage6 = CardImage(Pair6)
            M5B.Image = CardImage6
            If M5B.Image Is Nothing Then GoTo Card6
            M6B.Image = CardImage6
            M6B_LoadCompleted()

        Catch ex As Exception
            GoTo Card6
        End Try

    End Sub


    Private Sub M6B_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M5B.Visible = True
            M6B.Visible = True
            ShowCard7()
        End If
    End Sub

    Public Sub ShowCard7()

Card7:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair7 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M1C.Load(Pair7)
            'M2C.LoadAsync(Pair7)

            CardImage7 = CardImage(Pair7)
            M1C.Image = CardImage7
            If M1C.Image Is Nothing Then GoTo Card7
            M2C.Image = CardImage7
            M2C_LoadCompleted()

        Catch ex As Exception
            GoTo Card7
        End Try


    End Sub

    Private Sub M2C_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M1C.Visible = True
            M2C.Visible = True
            ShowCard8()
        End If
    End Sub

    Public Sub ShowCard8()
Card8:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair8 = MatchList(CardVal)
        MatchList.Remove(MatchList(CardVal))
        Try
            'M3C.Load(Pair8)
            'M4C.LoadAsync(Pair8)

            CardImage8 = CardImage(Pair8)
            M3C.Image = CardImage8
            If M3C.Image Is Nothing Then GoTo Card8
            M4C.Image = CardImage8
            M4C_LoadCompleted()

        Catch ex As Exception
            GoTo Card8
        End Try
    End Sub

    Private Sub M4C_LoadCompleted()
        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False
            M3C.Visible = True
            M4C.Visible = True
            ShowCard9()
        End If
    End Sub

    Public Sub ShowCard9()

Card9:
        CardVal = MainWindow.ssh.randomizer.Next(0, MatchList.Count)
        Pair9 = MatchList(CardVal)
        Try
            'M5C.Load(Pair9)
            'M6C.LoadAsync(Pair9)

            CardImage9 = CardImage(Pair9)
            M5C.Image = CardImage9
            If M5C.Image Is Nothing Then GoTo Card9
            M6C.Image = CardImage9
            M6C_LoadCompleted()

        Catch ex As Exception
            GoTo Card9
        End Try
    End Sub

    Private Sub M6C_LoadCompleted()


        If CardSetup = True Then
            RevealTick = 1
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False

            M5C.Visible = True
            M6C.Visible = True

            CardSetup = False

            'MatchList.Remove(MatchList(CardVal))


            'MatchList.Remove(MatchList(CardVal))

            MatchList.Clear()
            CompareList.Clear()

            MatchList.Add(Pair1)
            MatchList.Add(Pair1)
            MatchList.Add(Pair2)
            MatchList.Add(Pair2)
            MatchList.Add(Pair3)
            MatchList.Add(Pair3)
            MatchList.Add(Pair4)
            MatchList.Add(Pair4)
            MatchList.Add(Pair5)
            MatchList.Add(Pair5)
            MatchList.Add(Pair6)
            MatchList.Add(Pair6)
            MatchList.Add(Pair7)
            MatchList.Add(Pair7)
            MatchList.Add(Pair8)
            MatchList.Add(Pair8)
            MatchList.Add(Pair9)
            MatchList.Add(Pair9)

            CompareList.Add(Pair1)
            CompareList.Add(Pair1)
            CompareList.Add(Pair2)
            CompareList.Add(Pair2)
            CompareList.Add(Pair3)
            CompareList.Add(Pair3)
            CompareList.Add(Pair4)
            CompareList.Add(Pair4)
            CompareList.Add(Pair5)
            CompareList.Add(Pair5)
            CompareList.Add(Pair6)
            CompareList.Add(Pair6)
            CompareList.Add(Pair7)
            CompareList.Add(Pair7)
            CompareList.Add(Pair8)
            CompareList.Add(Pair8)
            CompareList.Add(Pair9)
            CompareList.Add(Pair9)

            'CardImage1 = CardImage(Pair1)
            'CardImage2 = CardImage(Pair2)
            'CardImage3 = CardImage(Pair3)
            'CardImage4 = CardImage(Pair4)
            'CardImage5 = CardImage(Pair5)
            'CardImage6 = CardImage(Pair6)
            'CardImage7 = CardImage(Pair7)
            'CardImage8 = CardImage(Pair8)
            'CardImage9 = CardImage(Pair9)




            For I As Integer = 0 To MatchList.Count - 1
                Debug.Print(MatchList(I))
            Next

            Match1A = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match1A)
            Match2A = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match2A)
            Match3A = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match3A)
            Match4A = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match4A)
            Match5A = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match5A)
            Match6A = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match6A)

            Match1B = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match1B)
            Match2B = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match2B)
            Match3B = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match3B)
            Match4B = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match4B)
            Match5B = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match5B)
            Match6B = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match6B)

            Match1C = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match1C)
            Match2C = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match2C)
            Match3C = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match3C)
            Match4C = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match4C)
            Match5C = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            MatchList.Remove(Match5C)
            Match6C = MatchList(MainWindow.ssh.randomizer.Next(0, MatchList.Count))
            'MatchList.Remove(Match6C)

            RevealTick = 3
            RevealCards = False
            CardRevealTimer.Start()
            Do
                Application.DoEvents()

            Loop Until RevealCards = True
            RevealCards = False

            EraseCards()

            ClearMatchCache()




            M1A.Image = Image.FromFile(CardBackImage)
            M2A.Image = Image.FromFile(CardBackImage)
            M3A.Image = Image.FromFile(CardBackImage)
            M4A.Image = Image.FromFile(CardBackImage)
            M5A.Image = Image.FromFile(CardBackImage)
            M6A.Image = Image.FromFile(CardBackImage)

            M1B.Image = Image.FromFile(CardBackImage)
            M2B.Image = Image.FromFile(CardBackImage)
            M3B.Image = Image.FromFile(CardBackImage)
            M4B.Image = Image.FromFile(CardBackImage)
            M5B.Image = Image.FromFile(CardBackImage)
            M6B.Image = Image.FromFile(CardBackImage)

            M1C.Image = Image.FromFile(CardBackImage)
            M2C.Image = Image.FromFile(CardBackImage)
            M3C.Image = Image.FromFile(CardBackImage)
            M4C.Image = Image.FromFile(CardBackImage)
            M5C.Image = Image.FromFile(CardBackImage)
            M6C.Image = Image.FromFile(CardBackImage)


            LBLMatchChance.Text = MatchChance & " Chances Left"

            BTNMatchEasy.Enabled = False
            BTNMatchNormal.Enabled = False
            BTNMatchHard.Enabled = False

            DealMatchCards()

            If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\CardShuffle.wav") Then
                GameWMP.settings.setMode("loop", False)
                GameWMP.settings.volume = 20
                GameWMP.URL = Application.StartupPath & "\Audio\System\CardShuffle.wav"
            End If
            'My.Computer.Audio.Play(Application.StartupPath & "\Audio\System\CardShuffle.wav")

            ShuffleTick = 19
            ShuffleTimer.Start()


        End If

    End Sub


    Public Sub CheckMatchTemp()

        'If MatchTemp = "M1A" Then M1A.Enabled = False
        'If MatchTemp = "M2A" Then M2A.Enabled = False
        'If MatchTemp = "M3A" Then M3A.Enabled = False
        'If MatchTemp = "M4A" Then M4A.Enabled = False
        'If MatchTemp = "M5A" Then M5A.Enabled = False
        'If MatchTemp = "M6A" Then M6A.Enabled = False

        'If MatchTemp = "M1B" Then M1B.Enabled = False
        'If MatchTemp = "M2B" Then M2B.Enabled = False
        'If MatchTemp = "M3B" Then M3B.Enabled = False
        'If MatchTemp = "M4B" Then M4B.Enabled = False
        'If MatchTemp = "M5B" Then M5B.Enabled = False
        'If MatchTemp = "M6B" Then M6B.Enabled = False

        'If MatchTemp = "M1C" Then M1C.Enabled = False
        'If MatchTemp = "M2C" Then M2C.Enabled = False
        'If MatchTemp = "M3C" Then M3C.Enabled = False
        'If MatchTemp = "M4C" Then M4C.Enabled = False
        'If MatchTemp = "M5C" Then M5C.Enabled = False
        'If MatchTemp = "M6C" Then M6C.Enabled = False

        If Match1 <> Match2 Then

            MatchChance -= 1
            LBLMatchChance.Text = MatchChance & " Chances Left"
            If MatchChance = 1 Then LBLMatchChance.Text = MatchChance & " Chance Left"

            CardTick = 2
            CardTimer.Start()

            Do
                Application.DoEvents()
            Loop Until CardTimer.Enabled = False

        Else

            MatchesMade += 1

        End If


        If M1A.Enabled = True Then
            Try
                ' M1A.Image.Dispose()
            Catch
            End Try
            M1A.Image = Nothing
            M1A.Image = Image.FromFile(CardBackImage)
        End If
        If M2A.Enabled = True Then
            Try
                'M2A.Image.Dispose()
            Catch
            End Try
            M2A.Image = Nothing
            M2A.Image = Image.FromFile(CardBackImage)
        End If
        If M3A.Enabled = True Then
            Try
                'M3A.Image.Dispose()
            Catch
            End Try
            M3A.Image = Nothing
            M3A.Image = Image.FromFile(CardBackImage)
        End If
        If M4A.Enabled = True Then
            Try
                'M4A.Image.Dispose()
            Catch
            End Try
            M4A.Image = Nothing
            M4A.Image = Image.FromFile(CardBackImage)
        End If
        If M5A.Enabled = True Then
            Try
                ' M5A.Image.Dispose()
            Catch
            End Try
            M5A.Image = Nothing
            M5A.Image = Image.FromFile(CardBackImage)
        End If
        If M6A.Enabled = True Then
            Try
                ' M6A.Image.Dispose()
            Catch
            End Try
            M6A.Image = Nothing
            M6A.Image = Image.FromFile(CardBackImage)
        End If

        If M1B.Enabled = True Then
            Try
                ' M1B.Image.Dispose()
            Catch
            End Try
            M1B.Image = Nothing
            M1B.Image = Image.FromFile(CardBackImage)
        End If
        If M2B.Enabled = True Then
            Try
                'M2B.Image.Dispose()
            Catch
            End Try
            M2B.Image = Nothing
            M2B.Image = Image.FromFile(CardBackImage)
        End If
        If M3B.Enabled = True Then
            Try
                'M3B.Image.Dispose()
            Catch
            End Try
            M3B.Image = Nothing
            M3B.Image = Image.FromFile(CardBackImage)
        End If
        If M4B.Enabled = True Then
            Try
                ' M4B.Image.Dispose()
            Catch
            End Try
            M4B.Image = Nothing
            M4B.Image = Image.FromFile(CardBackImage)
        End If
        If M5B.Enabled = True Then
            Try
                'M5B.Image.Dispose()
            Catch
            End Try
            M5B.Image = Nothing
            M5B.Image = Image.FromFile(CardBackImage)
        End If
        If M6B.Enabled = True Then
            Try
                'M6B.Image.Dispose()
            Catch
            End Try
            M6B.Image = Nothing
            M6B.Image = Image.FromFile(CardBackImage)
        End If

        If M1C.Enabled = True Then
            Try
                'M1C.Image.Dispose()
            Catch
            End Try
            M1C.Image = Nothing
            M1C.Image = Image.FromFile(CardBackImage)
        End If
        If M2C.Enabled = True Then
            Try
                ' M2C.Image.Dispose()
            Catch
            End Try
            M2C.Image = Nothing
            M2C.Image = Image.FromFile(CardBackImage)
        End If
        If M3C.Enabled = True Then
            Try
                'M3C.Image.Dispose()
            Catch
            End Try
            M3C.Image = Nothing
            M3C.Image = Image.FromFile(CardBackImage)
        End If
        If M4C.Enabled = True Then
            Try
                ' M4C.Image.Dispose()
            Catch
            End Try
            M4C.Image = Nothing
            M4C.Image = Image.FromFile(CardBackImage)
        End If
        If M5C.Enabled = True Then
            Try
                'M5C.Image.Dispose()
            Catch
            End Try
            M5C.Image = Nothing
            M5C.Image = Image.FromFile(CardBackImage)
        End If
        If M6C.Enabled = True Then
            Try
                'M6C.Image.Dispose()
            Catch
            End Try
            M6C.Image = Nothing
            M6C.Image = Image.FromFile(CardBackImage)
        End If

        'Try
        'GC.Collect()
        'Catch
        'End Try

        If MatchChance = 0 Then

            LBLMatchChance.Text = "Game Over"
            GameOn = False
            BTNMatchEasy.Enabled = True
            BTNMatchNormal.Enabled = True
            BTNMatchHard.Enabled = True

            If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\NoPayout.wav") Then


                GameWMP.settings.setMode("loop", False)
                GameWMP.settings.volume = 20
                GameWMP.URL = Application.StartupPath & "\Audio\System\NoPayout.wav"


            End If
            'My.Computer.Audio.Play(Application.StartupPath & "\Audio\System\NoPayout.wav")



        End If

        If MatchesMade = 9 Then


            LBLMatchChance.Text = "You Win!"
            GameOn = False
            MainWindow.ssh.BronzeTokens = MainWindow.ssh.BronzeTokens + MatchPot
            My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
            My.Settings.Save()
            LBLMatchTokens.Text = MainWindow.ssh.BronzeTokens


            If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutSmall.wav") Then


                GameWMP.settings.setMode("loop", False)
                GameWMP.settings.volume = 20
                GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutSmall.wav"


            End If

            'If FrmSettings.CBGameSounds.Checked = True Then My.Computer.Audio.Play(Application.StartupPath & "\Audio\System\PayoutSmall.wav")

            BTNMatchEasy.Enabled = True
            BTNMatchNormal.Enabled = True
            BTNMatchHard.Enabled = True

        End If




    End Sub

    Public Sub FlipCards()

        If MatchTemp = "M1A" Then M1A.Enabled = True
        If MatchTemp = "M2A" Then M2A.Enabled = True
        If MatchTemp = "M3A" Then M3A.Enabled = True
        If MatchTemp = "M4A" Then M4A.Enabled = True
        If MatchTemp = "M5A" Then M5A.Enabled = True
        If MatchTemp = "M6A" Then M6A.Enabled = True

        If MatchTemp = "M1B" Then M1B.Enabled = True
        If MatchTemp = "M2B" Then M2B.Enabled = True
        If MatchTemp = "M3B" Then M3B.Enabled = True
        If MatchTemp = "M4B" Then M4B.Enabled = True
        If MatchTemp = "M5B" Then M5B.Enabled = True
        If MatchTemp = "M6B" Then M6B.Enabled = True

        If MatchTemp = "M1C" Then M1C.Enabled = True
        If MatchTemp = "M2C" Then M2C.Enabled = True
        If MatchTemp = "M3C" Then M3C.Enabled = True
        If MatchTemp = "M4C" Then M4C.Enabled = True
        If MatchTemp = "M5C" Then M5C.Enabled = True
        If MatchTemp = "M6C" Then M6C.Enabled = True






    End Sub

    Public Sub RemindCards()

        CardImage1 = CardImage(Pair1)
        CardImage2 = CardImage(Pair2)
        CardImage3 = CardImage(Pair3)
        CardImage4 = CardImage(Pair4)
        CardImage5 = CardImage(Pair5)
        CardImage6 = CardImage(Pair6)
        CardImage7 = CardImage(Pair7)
        CardImage8 = CardImage(Pair8)
        CardImage9 = CardImage(Pair9)

    End Sub

    Private Sub M1A_Click(sender As Object, e As EventArgs) Handles M1A.Click


        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()

        'RemindCards()

        If CardImage1 Is Nothing Or CardImage2 Is Nothing Or CardImage3 Is Nothing Or CardImage4 Is Nothing Or CardImage5 Is Nothing Or CardImage6 Is Nothing Or CardImage7 Is Nothing Or
            CardImage8 Is Nothing Or CardImage9 Is Nothing Then
            MsgBox("Nothing here!")
        End If
        If Match1A = CompareList(0) Or Match1A = CompareList(1) Then M1A.Image = CardImage1
        If Match1A = CompareList(2) Or Match1A = CompareList(3) Then M1A.Image = CardImage2
        If Match1A = CompareList(4) Or Match1A = CompareList(5) Then M1A.Image = CardImage3
        If Match1A = CompareList(6) Or Match1A = CompareList(7) Then M1A.Image = CardImage4
        If Match1A = CompareList(8) Or Match1A = CompareList(9) Then M1A.Image = CardImage5
        If Match1A = CompareList(10) Or Match1A = CompareList(11) Then M1A.Image = CardImage6
        If Match1A = CompareList(12) Or Match1A = CompareList(13) Then M1A.Image = CardImage7
        If Match1A = CompareList(14) Or Match1A = CompareList(15) Then M1A.Image = CardImage8
        If Match1A = CompareList(16) Or Match1A = CompareList(17) Then M1A.Image = CardImage9


        If MatchPhase = 0 Then
            MatchPhase = 1
            'M1A.Load(Match1A)
            Match1 = Match1A
            M1A.Enabled = False
            MatchTemp = "M1A"
        Else
            MatchPhase = 0
            'M1A.Load(Match1A)
            Match2 = Match1A
            If Match1 = Match2 Then
                M1A.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M2A_Click(sender As Object, e As EventArgs) Handles M2A.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()

        'RemindCards()


        If Match2A = CompareList(0) Or Match2A = CompareList(1) Then M2A.Image = CardImage1
        If Match2A = CompareList(2) Or Match2A = CompareList(3) Then M2A.Image = CardImage2
        If Match2A = CompareList(4) Or Match2A = CompareList(5) Then M2A.Image = CardImage3
        If Match2A = CompareList(6) Or Match2A = CompareList(7) Then M2A.Image = CardImage4
        If Match2A = CompareList(8) Or Match2A = CompareList(9) Then M2A.Image = CardImage5
        If Match2A = CompareList(10) Or Match2A = CompareList(11) Then M2A.Image = CardImage6
        If Match2A = CompareList(12) Or Match2A = CompareList(13) Then M2A.Image = CardImage7
        If Match2A = CompareList(14) Or Match2A = CompareList(15) Then M2A.Image = CardImage8
        If Match2A = CompareList(16) Or Match2A = CompareList(17) Then M2A.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match2A
            M2A.Enabled = False
            MatchTemp = "M2A"
        Else
            MatchPhase = 0
            Match2 = Match2A
            If Match1 = Match2 Then
                M2A.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M3A_Click(sender As Object, e As EventArgs) Handles M3A.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match3A = CompareList(0) Or Match3A = CompareList(1) Then M3A.Image = CardImage1
        If Match3A = CompareList(2) Or Match3A = CompareList(3) Then M3A.Image = CardImage2
        If Match3A = CompareList(4) Or Match3A = CompareList(5) Then M3A.Image = CardImage3
        If Match3A = CompareList(6) Or Match3A = CompareList(7) Then M3A.Image = CardImage4
        If Match3A = CompareList(8) Or Match3A = CompareList(9) Then M3A.Image = CardImage5
        If Match3A = CompareList(10) Or Match3A = CompareList(11) Then M3A.Image = CardImage6
        If Match3A = CompareList(12) Or Match3A = CompareList(13) Then M3A.Image = CardImage7
        If Match3A = CompareList(14) Or Match3A = CompareList(15) Then M3A.Image = CardImage8
        If Match3A = CompareList(16) Or Match3A = CompareList(17) Then M3A.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match3A
            M3A.Enabled = False
            MatchTemp = "M3A"
        Else
            MatchPhase = 0
            Match2 = Match3A
            If Match1 = Match2 Then
                M3A.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M4A_Click(sender As Object, e As EventArgs) Handles M4A.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match4A = CompareList(0) Or Match4A = CompareList(1) Then M4A.Image = CardImage1
        If Match4A = CompareList(2) Or Match4A = CompareList(3) Then M4A.Image = CardImage2
        If Match4A = CompareList(4) Or Match4A = CompareList(5) Then M4A.Image = CardImage3
        If Match4A = CompareList(6) Or Match4A = CompareList(7) Then M4A.Image = CardImage4
        If Match4A = CompareList(8) Or Match4A = CompareList(9) Then M4A.Image = CardImage5
        If Match4A = CompareList(10) Or Match4A = CompareList(11) Then M4A.Image = CardImage6
        If Match4A = CompareList(12) Or Match4A = CompareList(13) Then M4A.Image = CardImage7
        If Match4A = CompareList(14) Or Match4A = CompareList(15) Then M4A.Image = CardImage8
        If Match4A = CompareList(16) Or Match4A = CompareList(17) Then M4A.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match4A
            M4A.Enabled = False
            MatchTemp = "M4A"
        Else
            MatchPhase = 0
            Match2 = Match4A
            If Match1 = Match2 Then
                M4A.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M5A_Click(sender As Object, e As EventArgs) Handles M5A.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match5A = CompareList(0) Or Match5A = CompareList(1) Then M5A.Image = CardImage1
        If Match5A = CompareList(2) Or Match5A = CompareList(3) Then M5A.Image = CardImage2
        If Match5A = CompareList(4) Or Match5A = CompareList(5) Then M5A.Image = CardImage3
        If Match5A = CompareList(6) Or Match5A = CompareList(7) Then M5A.Image = CardImage4
        If Match5A = CompareList(8) Or Match5A = CompareList(9) Then M5A.Image = CardImage5
        If Match5A = CompareList(10) Or Match5A = CompareList(11) Then M5A.Image = CardImage6
        If Match5A = CompareList(12) Or Match5A = CompareList(13) Then M5A.Image = CardImage7
        If Match5A = CompareList(14) Or Match5A = CompareList(15) Then M5A.Image = CardImage8
        If Match5A = CompareList(16) Or Match5A = CompareList(17) Then M5A.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match5A
            M5A.Enabled = False
            MatchTemp = "M5A"
        Else
            MatchPhase = 0
            Match2 = Match5A
            If Match1 = Match2 Then
                M5A.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M6A_Click(sender As Object, e As EventArgs) Handles M6A.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match6A = CompareList(0) Or Match6A = CompareList(1) Then M6A.Image = CardImage1
        If Match6A = CompareList(2) Or Match6A = CompareList(3) Then M6A.Image = CardImage2
        If Match6A = CompareList(4) Or Match6A = CompareList(5) Then M6A.Image = CardImage3
        If Match6A = CompareList(6) Or Match6A = CompareList(7) Then M6A.Image = CardImage4
        If Match6A = CompareList(8) Or Match6A = CompareList(9) Then M6A.Image = CardImage5
        If Match6A = CompareList(10) Or Match6A = CompareList(11) Then M6A.Image = CardImage6
        If Match6A = CompareList(12) Or Match6A = CompareList(13) Then M6A.Image = CardImage7
        If Match6A = CompareList(14) Or Match6A = CompareList(15) Then M6A.Image = CardImage8
        If Match6A = CompareList(16) Or Match6A = CompareList(17) Then M6A.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match6A
            M6A.Enabled = False
            MatchTemp = "M6A"
        Else
            MatchPhase = 0
            Match2 = Match6A
            If Match1 = Match2 Then
                M6A.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub

    Private Sub M1B_Click(sender As Object, e As EventArgs) Handles M1B.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match1B = CompareList(0) Or Match1B = CompareList(1) Then M1B.Image = CardImage1
        If Match1B = CompareList(2) Or Match1B = CompareList(3) Then M1B.Image = CardImage2
        If Match1B = CompareList(4) Or Match1B = CompareList(5) Then M1B.Image = CardImage3
        If Match1B = CompareList(6) Or Match1B = CompareList(7) Then M1B.Image = CardImage4
        If Match1B = CompareList(8) Or Match1B = CompareList(9) Then M1B.Image = CardImage5
        If Match1B = CompareList(10) Or Match1B = CompareList(11) Then M1B.Image = CardImage6
        If Match1B = CompareList(12) Or Match1B = CompareList(13) Then M1B.Image = CardImage7
        If Match1B = CompareList(14) Or Match1B = CompareList(15) Then M1B.Image = CardImage8
        If Match1B = CompareList(16) Or Match1B = CompareList(17) Then M1B.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match1B
            M1B.Enabled = False
            MatchTemp = "M1B"
        Else
            MatchPhase = 0
            Match2 = Match1B
            If Match1 = Match2 Then
                M1B.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M2B_Click(sender As Object, e As EventArgs) Handles M2B.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match2B = CompareList(0) Or Match2B = CompareList(1) Then M2B.Image = CardImage1
        If Match2B = CompareList(2) Or Match2B = CompareList(3) Then M2B.Image = CardImage2
        If Match2B = CompareList(4) Or Match2B = CompareList(5) Then M2B.Image = CardImage3
        If Match2B = CompareList(6) Or Match2B = CompareList(7) Then M2B.Image = CardImage4
        If Match2B = CompareList(8) Or Match2B = CompareList(9) Then M2B.Image = CardImage5
        If Match2B = CompareList(10) Or Match2B = CompareList(11) Then M2B.Image = CardImage6
        If Match2B = CompareList(12) Or Match2B = CompareList(13) Then M2B.Image = CardImage7
        If Match2B = CompareList(14) Or Match2B = CompareList(15) Then M2B.Image = CardImage8
        If Match2B = CompareList(16) Or Match2B = CompareList(17) Then M2B.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match2B
            M2B.Enabled = False
            MatchTemp = "M2B"
        Else
            MatchPhase = 0
            Match2 = Match2B
            If Match1 = Match2 Then
                M2B.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M3B_Click(sender As Object, e As EventArgs) Handles M3B.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match3B = CompareList(0) Or Match3B = CompareList(1) Then M3B.Image = CardImage1
        If Match3B = CompareList(2) Or Match3B = CompareList(3) Then M3B.Image = CardImage2
        If Match3B = CompareList(4) Or Match3B = CompareList(5) Then M3B.Image = CardImage3
        If Match3B = CompareList(6) Or Match3B = CompareList(7) Then M3B.Image = CardImage4
        If Match3B = CompareList(8) Or Match3B = CompareList(9) Then M3B.Image = CardImage5
        If Match3B = CompareList(10) Or Match3B = CompareList(11) Then M3B.Image = CardImage6
        If Match3B = CompareList(12) Or Match3B = CompareList(13) Then M3B.Image = CardImage7
        If Match3B = CompareList(14) Or Match3B = CompareList(15) Then M3B.Image = CardImage8
        If Match3B = CompareList(16) Or Match3B = CompareList(17) Then M3B.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match3B
            M3B.Enabled = False
            MatchTemp = "M3B"
        Else
            MatchPhase = 0
            Match2 = Match3B
            If Match1 = Match2 Then
                M3B.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M4B_Click(sender As Object, e As EventArgs) Handles M4B.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match4B = CompareList(0) Or Match4B = CompareList(1) Then M4B.Image = CardImage1
        If Match4B = CompareList(2) Or Match4B = CompareList(3) Then M4B.Image = CardImage2
        If Match4B = CompareList(4) Or Match4B = CompareList(5) Then M4B.Image = CardImage3
        If Match4B = CompareList(6) Or Match4B = CompareList(7) Then M4B.Image = CardImage4
        If Match4B = CompareList(8) Or Match4B = CompareList(9) Then M4B.Image = CardImage5
        If Match4B = CompareList(10) Or Match4B = CompareList(11) Then M4B.Image = CardImage6
        If Match4B = CompareList(12) Or Match4B = CompareList(13) Then M4B.Image = CardImage7
        If Match4B = CompareList(14) Or Match4B = CompareList(15) Then M4B.Image = CardImage8
        If Match4B = CompareList(16) Or Match4B = CompareList(17) Then M4B.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match4B
            M4B.Enabled = False
            MatchTemp = "M4B"
        Else
            MatchPhase = 0
            Match2 = Match4B
            If Match1 = Match2 Then
                M4B.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M5B_Click(sender As Object, e As EventArgs) Handles M5B.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match5B = CompareList(0) Or Match5B = CompareList(1) Then M5B.Image = CardImage1
        If Match5B = CompareList(2) Or Match5B = CompareList(3) Then M5B.Image = CardImage2
        If Match5B = CompareList(4) Or Match5B = CompareList(5) Then M5B.Image = CardImage3
        If Match5B = CompareList(6) Or Match5B = CompareList(7) Then M5B.Image = CardImage4
        If Match5B = CompareList(8) Or Match5B = CompareList(9) Then M5B.Image = CardImage5
        If Match5B = CompareList(10) Or Match5B = CompareList(11) Then M5B.Image = CardImage6
        If Match5B = CompareList(12) Or Match5B = CompareList(13) Then M5B.Image = CardImage7
        If Match5B = CompareList(14) Or Match5B = CompareList(15) Then M5B.Image = CardImage8
        If Match5B = CompareList(16) Or Match5B = CompareList(17) Then M5B.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match5B
            M5B.Enabled = False
            MatchTemp = "M5B"
        Else
            MatchPhase = 0
            Match2 = Match5B
            If Match1 = Match2 Then
                M5B.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M6B_Click(sender As Object, e As EventArgs) Handles M6B.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match6B = CompareList(0) Or Match6B = CompareList(1) Then M6B.Image = CardImage1
        If Match6B = CompareList(2) Or Match6B = CompareList(3) Then M6B.Image = CardImage2
        If Match6B = CompareList(4) Or Match6B = CompareList(5) Then M6B.Image = CardImage3
        If Match6B = CompareList(6) Or Match6B = CompareList(7) Then M6B.Image = CardImage4
        If Match6B = CompareList(8) Or Match6B = CompareList(9) Then M6B.Image = CardImage5
        If Match6B = CompareList(10) Or Match6B = CompareList(11) Then M6B.Image = CardImage6
        If Match6B = CompareList(12) Or Match6B = CompareList(13) Then M6B.Image = CardImage7
        If Match6B = CompareList(14) Or Match6B = CompareList(15) Then M6B.Image = CardImage8
        If Match6B = CompareList(16) Or Match6B = CompareList(17) Then M6B.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match6B
            M6B.Enabled = False
            MatchTemp = "M6B"
        Else
            MatchPhase = 0
            Match2 = Match6B
            If Match1 = Match2 Then
                M6B.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub

    Private Sub M1C_Click(sender As Object, e As EventArgs) Handles M1C.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match1C = CompareList(0) Or Match1C = CompareList(1) Then M1C.Image = CardImage1
        If Match1C = CompareList(2) Or Match1C = CompareList(3) Then M1C.Image = CardImage2
        If Match1C = CompareList(4) Or Match1C = CompareList(5) Then M1C.Image = CardImage3
        If Match1C = CompareList(6) Or Match1C = CompareList(7) Then M1C.Image = CardImage4
        If Match1C = CompareList(8) Or Match1C = CompareList(9) Then M1C.Image = CardImage5
        If Match1C = CompareList(10) Or Match1C = CompareList(11) Then M1C.Image = CardImage6
        If Match1C = CompareList(12) Or Match1C = CompareList(13) Then M1C.Image = CardImage7
        If Match1C = CompareList(14) Or Match1C = CompareList(15) Then M1C.Image = CardImage8
        If Match1C = CompareList(16) Or Match1C = CompareList(17) Then M1C.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match1C
            M1C.Enabled = False
            MatchTemp = "M1C"
        Else
            MatchPhase = 0
            Match2 = Match1C
            If Match1 = Match2 Then
                M1C.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M2C_Click(sender As Object, e As EventArgs) Handles M2C.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match2C = CompareList(0) Or Match2C = CompareList(1) Then M2C.Image = CardImage1
        If Match2C = CompareList(2) Or Match2C = CompareList(3) Then M2C.Image = CardImage2
        If Match2C = CompareList(4) Or Match2C = CompareList(5) Then M2C.Image = CardImage3
        If Match2C = CompareList(6) Or Match2C = CompareList(7) Then M2C.Image = CardImage4
        If Match2C = CompareList(8) Or Match2C = CompareList(9) Then M2C.Image = CardImage5
        If Match2C = CompareList(10) Or Match2C = CompareList(11) Then M2C.Image = CardImage6
        If Match2C = CompareList(12) Or Match2C = CompareList(13) Then M2C.Image = CardImage7
        If Match2C = CompareList(14) Or Match2C = CompareList(15) Then M2C.Image = CardImage8
        If Match2C = CompareList(16) Or Match2C = CompareList(17) Then M2C.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match2C
            M2C.Enabled = False
            MatchTemp = "M2C"
        Else
            MatchPhase = 0
            Match2 = Match2C
            If Match1 = Match2 Then
                M2C.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M3C_Click(sender As Object, e As EventArgs) Handles M3C.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match3C = CompareList(0) Or Match3C = CompareList(1) Then M3C.Image = CardImage1
        If Match3C = CompareList(2) Or Match3C = CompareList(3) Then M3C.Image = CardImage2
        If Match3C = CompareList(4) Or Match3C = CompareList(5) Then M3C.Image = CardImage3
        If Match3C = CompareList(6) Or Match3C = CompareList(7) Then M3C.Image = CardImage4
        If Match3C = CompareList(8) Or Match3C = CompareList(9) Then M3C.Image = CardImage5
        If Match3C = CompareList(10) Or Match3C = CompareList(11) Then M3C.Image = CardImage6
        If Match3C = CompareList(12) Or Match3C = CompareList(13) Then M3C.Image = CardImage7
        If Match3C = CompareList(14) Or Match3C = CompareList(15) Then M3C.Image = CardImage8
        If Match3C = CompareList(16) Or Match3C = CompareList(17) Then M3C.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match3C
            M3C.Enabled = False
            MatchTemp = "M3C"
        Else
            MatchPhase = 0
            Match2 = Match3C
            If Match1 = Match2 Then
                M3C.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M4C_Click(sender As Object, e As EventArgs) Handles M4C.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match4C = CompareList(0) Or Match4C = CompareList(1) Then M4C.Image = CardImage1
        If Match4C = CompareList(2) Or Match4C = CompareList(3) Then M4C.Image = CardImage2
        If Match4C = CompareList(4) Or Match4C = CompareList(5) Then M4C.Image = CardImage3
        If Match4C = CompareList(6) Or Match4C = CompareList(7) Then M4C.Image = CardImage4
        If Match4C = CompareList(8) Or Match4C = CompareList(9) Then M4C.Image = CardImage5
        If Match4C = CompareList(10) Or Match4C = CompareList(11) Then M4C.Image = CardImage6
        If Match4C = CompareList(12) Or Match4C = CompareList(13) Then M4C.Image = CardImage7
        If Match4C = CompareList(14) Or Match4C = CompareList(15) Then M4C.Image = CardImage8
        If Match4C = CompareList(16) Or Match4C = CompareList(17) Then M4C.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match4C
            M4C.Enabled = False
            MatchTemp = "M4C"
        Else
            MatchPhase = 0
            Match2 = Match4C
            If Match1 = Match2 Then
                M4C.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M5C_Click(sender As Object, e As EventArgs) Handles M5C.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match5C = CompareList(0) Or Match5C = CompareList(1) Then M5C.Image = CardImage1
        If Match5C = CompareList(2) Or Match5C = CompareList(3) Then M5C.Image = CardImage2
        If Match5C = CompareList(4) Or Match5C = CompareList(5) Then M5C.Image = CardImage3
        If Match5C = CompareList(6) Or Match5C = CompareList(7) Then M5C.Image = CardImage4
        If Match5C = CompareList(8) Or Match5C = CompareList(9) Then M5C.Image = CardImage5
        If Match5C = CompareList(10) Or Match5C = CompareList(11) Then M5C.Image = CardImage6
        If Match5C = CompareList(12) Or Match5C = CompareList(13) Then M5C.Image = CardImage7
        If Match5C = CompareList(14) Or Match5C = CompareList(15) Then M5C.Image = CardImage8
        If Match5C = CompareList(16) Or Match5C = CompareList(17) Then M5C.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match5C
            M5C.Enabled = False
            MatchTemp = "M5C"
        Else
            MatchPhase = 0
            Match2 = Match5C
            If Match1 = Match2 Then
                M5C.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub
    Private Sub M6C_Click(sender As Object, e As EventArgs) Handles M6C.Click

        If CardTimer.Enabled = True Or GameOn = False Then Return

        PlayCardFlip()



        If Match6C = CompareList(0) Or Match6C = CompareList(1) Then M6C.Image = CardImage1
        If Match6C = CompareList(2) Or Match6C = CompareList(3) Then M6C.Image = CardImage2
        If Match6C = CompareList(4) Or Match6C = CompareList(5) Then M6C.Image = CardImage3
        If Match6C = CompareList(6) Or Match6C = CompareList(7) Then M6C.Image = CardImage4
        If Match6C = CompareList(8) Or Match6C = CompareList(9) Then M6C.Image = CardImage5
        If Match6C = CompareList(10) Or Match6C = CompareList(11) Then M6C.Image = CardImage6
        If Match6C = CompareList(12) Or Match6C = CompareList(13) Then M6C.Image = CardImage7
        If Match6C = CompareList(14) Or Match6C = CompareList(15) Then M6C.Image = CardImage8
        If Match6C = CompareList(16) Or Match6C = CompareList(17) Then M6C.Image = CardImage9

        If MatchPhase = 0 Then
            MatchPhase = 1
            Match1 = Match6C
            M6C.Enabled = False
            MatchTemp = "M6C"
        Else
            MatchPhase = 0
            Match2 = Match6C
            If Match1 = Match2 Then
                M6C.Enabled = False
            Else
                FlipCards()
            End If
            CheckMatchTemp()
        End If

    End Sub

    Private Sub CardTimer_Tick(sender As Object, e As EventArgs) Handles CardTimer.Tick

        CardTick -= 1

        If CardTick < 1 Then
            CardTimer.Stop()
        End If

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If SlotTimer3.Enabled = True Or LBLSlotBet.Text = 0 Then Return


        SlotTick1 = 4
        SlotTick2 = 12
        SlotTick3 = 36



        SlotList.Clear()

        SlotList.Add(My.Settings.BP1)
        SlotList.Add(My.Settings.BP2)
        SlotList.Add(My.Settings.BP3)
        SlotList.Add(My.Settings.BP4)
        SlotList.Add(My.Settings.BP5)
        SlotList.Add(My.Settings.BP6)

        SlotList.Add(My.Settings.SP1)
        SlotList.Add(My.Settings.SP2)
        SlotList.Add(My.Settings.SP3)
        SlotList.Add(My.Settings.SP4)
        SlotList.Add(My.Settings.SP5)
        SlotList.Add(My.Settings.SP6)

        SlotList.Add(My.Settings.GP1)
        SlotList.Add(My.Settings.GP2)
        SlotList.Add(My.Settings.GP3)
        SlotList.Add(My.Settings.GP4)
        SlotList.Add(My.Settings.GP5)
        SlotList.Add(My.Settings.GP6)

        SlotBack1.BackColor = Color.Gainsboro
        SlotBack2.BackColor = Color.Gainsboro
        SlotBack3.BackColor = Color.Gainsboro

        LBLSlotPayout.Text = "---"

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\Slots.wav") Then
            ' mciSendString("close myWAV", Nothing, 0, 0)

            fileName1 = Application.StartupPath & "\Audio\System\Slots.wav"
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = fileName1


            'fileName1 = Form1.GetShortPathName(fileName1)

            'Dim Volume As Integer = 50
            'mciSendString("setaudio myWAV volume to " & Volume, Nothing, 0, 0)
            'waveoutsetvolume(-1, 5)

            'mciSendString("open " & fileName1 & " type WAVEAUDIO alias myWAV", Nothing, 0, 0)
            'mciSendString("play myWAV", Nothing, 0, 0)
        End If
        'My.Computer.Audio.Play(Application.StartupPath & "\Audio\System\Slots.wav", AudioPlayMode.Background)


        MainWindow.ssh.BronzeTokens -= LBLSlotBet.Text
        LBLSlotTokens.Text = MainWindow.ssh.BronzeTokens

        If Val(LBLSlotBet.Text) > Val(LBLSlotTokens.Text) Then
            LBLSlotBet.Text = LBLSlotTokens.Text
            SlotBet = Val(LBLSlotBet.Text)
            SlotBetTemp = Val(LBLSlotBet.Text)
        End If

        Try
            Slot1.Image.Dispose()
        Catch
        End Try
        Slot1.Image = Nothing

        Try
            Slot2.Image.Dispose()
        Catch
        End Try
        Slot2.Image = Nothing

        Try
            Slot3.Image.Dispose()
        Catch
        End Try
        Slot3.Image = Nothing

        GC.Collect()

        SlotTimer1.Start()
        SlotTImer2.Start()
        SlotTimer3.Start()




    End Sub

    Private Sub SlotTimer_Tick(sender As Object, e As EventArgs) Handles SlotTimer1.Tick

        SlotTick1 -= 1

        Slot1Val = randomizer.Next(0, 18)
        Try
            Slot1.Image.Dispose()
        Catch
        End Try
        Slot1.Image = Nothing
        GC.Collect()
        Slot1.Image = Image.FromFile(SlotList(Slot1Val))

        If SlotTick1 < 1 Then
            SlotTimer1.Stop()
            SlotBack1.BackColor = Color.Silver
            If Slot1Val < 6 Then SlotBack1.BackColor = Color.Peru
            If Slot1Val > 11 Then SlotBack1.BackColor = Color.Gold
        End If







    End Sub

    Private Sub SlotTImer2_Tick(sender As Object, e As EventArgs) Handles SlotTImer2.Tick


        SlotTick2 -= 1

        Slot2Val = randomizer.Next(0, 18)
        Try
            Slot2.Image.Dispose()
        Catch
        End Try
        Slot2.Image = Nothing
        GC.Collect()
        Slot2.Image = Image.FromFile(SlotList(Slot2Val))

        If SlotTick2 < 1 Then
            SlotTImer2.Stop()
            SlotBack2.BackColor = Color.Silver
            If Slot2Val < 6 Then SlotBack2.BackColor = Color.Peru
            If Slot2Val > 11 Then SlotBack2.BackColor = Color.Gold
        End If




    End Sub

    Private Sub SlotTimer3_Tick(sender As Object, e As EventArgs) Handles SlotTimer3.Tick

        SlotTick3 -= 1

        Slot3Val = randomizer.Next(0, 18)
        Try
            Slot3.Image.Dispose()
        Catch
        End Try
        Slot3.Image = Nothing
        GC.Collect()
        Slot3.Image = Image.FromFile(SlotList(Slot3Val))

        If SlotTick3 < 1 Then
            SlotTimer3.Stop()
            SlotBack3.BackColor = Color.Silver
            If Slot3Val < 6 Then SlotBack3.BackColor = Color.Peru
            If Slot3Val > 11 Then SlotBack3.BackColor = Color.Gold


            Payout = 0

            If SlotBack1.BackColor = Color.Gold Then Payout = 1
            If SlotBack1.BackColor = Color.Gold And SlotBack2.BackColor = Color.Gold Then Payout = 2

            If SlotBack1.BackColor = Color.Peru And SlotBack2.BackColor = Color.Peru And SlotBack3.BackColor = Color.Peru Then Payout = 3
            If SlotBack1.BackColor = Color.Silver And SlotBack2.BackColor = Color.Silver And SlotBack3.BackColor = Color.Silver Then Payout = 5
            If SlotBack1.BackColor = Color.Gold And SlotBack2.BackColor = Color.Gold And SlotBack3.BackColor = Color.Gold Then Payout = 7

            If SlotBack1.BackColor = Color.Peru And SlotBack2.BackColor = Color.Peru And Slot1Val = Slot2Val Then Payout = 10
            If SlotBack1.BackColor = Color.Silver And SlotBack2.BackColor = Color.Silver And Slot1Val = Slot2Val Then Payout = 15
            If SlotBack1.BackColor = Color.Gold And SlotBack2.BackColor = Color.Gold And Slot1Val = Slot2Val Then Payout = 20

            If SlotBack1.BackColor = Color.Peru And SlotBack2.BackColor = Color.Peru And SlotBack3.BackColor = Color.Peru And Slot1Val = Slot2Val And Slot2Val = Slot3Val Then Payout = 30
            If SlotBack1.BackColor = Color.Silver And SlotBack2.BackColor = Color.Silver And SlotBack3.BackColor = Color.Silver And Slot1Val = Slot2Val And Slot2Val = Slot3Val Then Payout = 40
            If SlotBack1.BackColor = Color.Gold And SlotBack2.BackColor = Color.Gold And SlotBack3.BackColor = Color.Gold And Slot1Val = Slot2Val And Slot2Val = Slot3Val Then Payout = 50

            Payout *= SlotBet
            LBLSlotPayout.Text = Payout

            If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutOne.wav") Then
                ' Dim SlotSound As String

                'mciSendString("close myWAV", Nothing, 0, 0)


                If Payout < 4 Then
                    'SlotSound = Application.StartupPath & "\Audio\System\PayoutOne.wav"


                    fileName1 = Application.StartupPath & "\Audio\System\PayoutOne.wav"
                    'fileName1 = Form1.GetShortPathName(fileName1)




                    'min Volume is 1, max Volume is 1000



                End If

                If Payout > 3 And Payout < 26 And File.Exists(Application.StartupPath & "\Audio\System\PayoutSmall.wav") Then
                    'SlotSound = Application.StartupPath & "\Audio\System\PayoutSmall.wav"

                    fileName1 = Application.StartupPath & "\Audio\System\PayoutSmall.wav"
                    '   fileName1 = Form1.GetShortPathName(fileName1)
                End If

                If Payout > 25 And File.Exists(Application.StartupPath & "\Audio\System\PayoutBig.wav") Then
                    'SlotSound = Application.StartupPath & "\Audio\System\PayoutBig.wav"

                    fileName1 = Application.StartupPath & "\Audio\System\PayoutBig.wav"
                    '  fileName1 = Form1.GetShortPathName(fileName1)
                End If

                If Payout = 0 And File.Exists(Application.StartupPath & "\Audio\System\NoPayout.wav") Then
                    'SlotSound = Application.StartupPath & "\Audio\System\NoPayout.wav"

                    fileName1 = Application.StartupPath & "\Audio\System\NoPayout.wav"
                    ' fileName1 = Form1.GetShortPathName(fileName1)
                End If

                GameWMP.settings.setMode("loop", False)
                GameWMP.settings.volume = 20
                GameWMP.URL = fileName1


                'Dim Volume As Integer = 50
                'mciSendString("setaudio myWAV volume to " & Volume, Nothing, 0, 0)

                '                mciSendString("open " & fileName1 & " type WAVEAUDIO alias myWAV", Nothing, 0, 0)
                '               mciSendString("play myWAV", Nothing, 0, 0)

                'My.Computer.Audio.Play(SlotSound)
            End If


            MainWindow.ssh.BronzeTokens += Payout

            LBLSlotTokens.Text = MainWindow.ssh.BronzeTokens

            My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
            My.Settings.Save()



        End If








    End Sub

    Private Sub Panel5_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub FrmCardList_Load(sender As Object, e As EventArgs) Handles Me.Load

        InitializeSlots()

    End Sub

    Public Sub InitializeSlots()

        SlotList.Clear()

        SlotList.Add(My.Settings.BP1)
        SlotList.Add(My.Settings.BP2)
        SlotList.Add(My.Settings.BP3)
        SlotList.Add(My.Settings.BP4)
        SlotList.Add(My.Settings.BP5)
        SlotList.Add(My.Settings.BP6)

        SlotList.Add(My.Settings.SP1)
        SlotList.Add(My.Settings.SP2)
        SlotList.Add(My.Settings.SP3)
        SlotList.Add(My.Settings.SP4)
        SlotList.Add(My.Settings.SP5)
        SlotList.Add(My.Settings.SP6)

        SlotList.Add(My.Settings.GP1)
        SlotList.Add(My.Settings.GP2)
        SlotList.Add(My.Settings.GP3)
        SlotList.Add(My.Settings.GP4)
        SlotList.Add(My.Settings.GP5)
        SlotList.Add(My.Settings.GP6)



        Slot1.Image = Image.FromFile(SlotList(randomizer.Next(0, SlotList.Count)))
        Slot2.Image = Image.FromFile(SlotList(randomizer.Next(0, SlotList.Count)))
        Slot3.Image = Image.FromFile(SlotList(randomizer.Next(0, SlotList.Count)))



        Dim SlotImage As String

        If File.Exists(My.Settings.CardBack) Then
            SlotImage = My.Settings.CardBack
        Else
            SlotImage = Application.StartupPath & "\Scripts\" & MainWindow.DommePersonalityComboBox.Text & "\Apps\Games\_CardBackPicture.png"
        End If

        SlotLeft2.Image = Image.FromFile(SlotImage)
        SlotLeft1.Image = Image.FromFile(SlotImage)
        SlotRight1.Image = Image.FromFile(SlotImage)
        SlotRight2.Image = Image.FromFile(SlotImage)

        LBLSlotTokens.Text = MainWindow.ssh.BronzeTokens

    End Sub

    Public Sub ClearSlots()

        For Each tmp As PictureBox In New List(Of PictureBox) From
                {Slot1, Slot2, Slot3, SlotLeft1, SlotLeft2, SlotRight1, SlotRight2}
            If tmp.Image IsNot Nothing Then tmp.Image.Dispose()
            tmp.Image = Nothing
        Next

        Try
            GC.Collect()
        Catch
        End Try

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If SlotTimer3.Enabled = True Then Return

        If SlotBet < 3 Then
            SlotBet += 1
            If SlotBet > LBLSlotTokens.Text Then SlotBet -= 1
            LBLSlotBet.Text = SlotBet
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If SlotTimer3.Enabled = True Then Return

        If SlotBet > 0 Then
            SlotBet -= 1
            LBLSlotBet.Text = SlotBet
        End If


    End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs)

        MainWindow.ssh.BronzeTokens += 5
        LBLSlotTokens.Text = MainWindow.ssh.BronzeTokens
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles BTNMatchEasy.Click

        If MainWindow.ssh.BronzeTokens < 1 Then Return


        InitializeCards()

        If MatchList.Count < 1 Then
            MessageBox.Show(Me, "You will need to select at least 1 local image folder or URL File before you can play the Match Game!", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        BTNMatchEasy.Enabled = False
        BTNMatchNormal.Enabled = False
        BTNMatchHard.Enabled = False

        EraseCards()

        MainWindow.ssh.BronzeTokens -= 1
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
        My.Settings.Save()
        LBLMatchTokens.Text = MainWindow.ssh.BronzeTokens

        CardSetup = True

        MatchChance = 15
        MatchesMade = 0
        MatchPot = 3

        MatchGameStart()

    End Sub

    Private Sub BTNMatchNormal_Click(sender As Object, e As EventArgs) Handles BTNMatchNormal.Click

        If MainWindow.ssh.BronzeTokens < 1 Then Return

        InitializeCards()

        If MatchList.Count < 1 Then
            MessageBox.Show(Me, "You will need to select at least 1 local image folder or URL File before you can play the Match Game!", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        BTNMatchEasy.Enabled = False
        BTNMatchNormal.Enabled = False
        BTNMatchHard.Enabled = False

        EraseCards()

        MainWindow.ssh.BronzeTokens -= 1
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
        My.Settings.Save()
        LBLMatchTokens.Text = MainWindow.ssh.BronzeTokens

        CardSetup = True

        MatchChance = 10
        MatchesMade = 0
        MatchPot = 10

        MatchGameStart()

    End Sub

    Private Sub BTNMatchHard_Click(sender As Object, e As EventArgs) Handles BTNMatchHard.Click

        If MainWindow.ssh.BronzeTokens < 1 Then Return

        InitializeCards()

        If MatchList.Count < 1 Then
            MessageBox.Show(Me, "You will need to select at least 1 local image folder or URL File before you can play the Match Game!", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        BTNMatchEasy.Enabled = False
        BTNMatchNormal.Enabled = False
        BTNMatchHard.Enabled = False

        EraseCards()

        MainWindow.ssh.BronzeTokens -= 1
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
        My.Settings.Save()
        LBLMatchTokens.Text = MainWindow.ssh.BronzeTokens

        CardSetup = True

        MatchChance = 7
        MatchesMade = 0
        MatchPot = 25

        MatchGameStart()


    End Sub

    Public Sub ClearMatchCache()



        Try
            M1A.Image.Dispose()
        Catch
        End Try
        Try
            M2A.Image.Dispose()
        Catch
        End Try
        Try
            M3A.Image.Dispose()
        Catch
        End Try
        Try
            M4A.Image.Dispose()
        Catch
        End Try
        Try
            M5A.Image.Dispose()
        Catch
        End Try
        Try
            M6A.Image.Dispose()
        Catch
        End Try

        Try
            M1B.Image.Dispose()
        Catch
        End Try
        Try
            M2B.Image.Dispose()
        Catch
        End Try
        Try
            M3B.Image.Dispose()
        Catch
        End Try
        Try
            M4B.Image.Dispose()
        Catch
        End Try
        Try
            M5B.Image.Dispose()
        Catch
        End Try
        Try
            M6B.Image.Dispose()
        Catch
        End Try

        Try
            M1C.Image.Dispose()
        Catch
        End Try
        Try
            M2C.Image.Dispose()
        Catch
        End Try
        Try
            M3C.Image.Dispose()
        Catch
        End Try
        Try
            M4C.Image.Dispose()
        Catch
        End Try
        Try
            M5C.Image.Dispose()
        Catch
        End Try
        Try
            M6C.Image.Dispose()
        Catch
        End Try


        M1A.Image = Nothing
        M2A.Image = Nothing
        M3A.Image = Nothing
        M4A.Image = Nothing
        M5A.Image = Nothing
        M6A.Image = Nothing



        M1B.Image = Nothing
        M2B.Image = Nothing
        M3B.Image = Nothing
        M4B.Image = Nothing
        M5B.Image = Nothing
        M6B.Image = Nothing



        M1C.Image = Nothing
        M2C.Image = Nothing
        M3C.Image = Nothing
        M4C.Image = Nothing
        M5C.Image = Nothing
        M6C.Image = Nothing




        Try
            GC.Collect()
        Catch
        End Try

    End Sub

    Public Sub EraseCards()

        M1A.Visible = False
        M2A.Visible = False
        M3A.Visible = False
        M4A.Visible = False
        M5A.Visible = False
        M6A.Visible = False

        M1B.Visible = False
        M2B.Visible = False
        M3B.Visible = False
        M4B.Visible = False
        M5B.Visible = False
        M6B.Visible = False

        M1C.Visible = False
        M2C.Visible = False
        M3C.Visible = False
        M4C.Visible = False
        M5C.Visible = False
        M6C.Visible = False



    End Sub

    Private Sub TCGames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TCGames.SelectedIndexChanged

        ClearAllCards()

        If TCGames.SelectedIndex <> 0 Then
            ClearSlots()
            SlotBet = 0
            LBLSlotBet.Text = SlotBet
        End If

        If TCGames.SelectedIndex = 0 Then
            InitializeSlots()
            LBLSlotTokens.Text = MainWindow.ssh.BronzeTokens
        End If

        If TCGames.SelectedIndex <> 1 Then
            ClearMatchCache()
        End If

        If TCGames.SelectedIndex = 1 Then
            InitializeCards()
            LBLMatchTokens.Text = MainWindow.ssh.BronzeTokens
        End If

        If TCGames.SelectedIndex = 2 Then

            LBLRiskTokens.Text = MainWindow.ssh.BronzeTokens
        End If

        If TCGames.SelectedIndex = 3 Then

            If MainWindow.CompareDates(My.Settings.TokenTasks) <> 0 Then
                BTNTokenRequest.Enabled = True
            Else
                BTNTokenRequest.Enabled = False
            End If

            ClearExchange()
            CheckExchange()

            If File.Exists(My.Settings.CardBack) Then
                Try
                    BoosterBack.Image.Dispose()
                Catch
                End Try

                BoosterBack.Image = Nothing
                GC.Collect()

                BoosterBack.Image = Image.FromFile(My.Settings.CardBack)
            Else
                BoosterBack.Image = Image.FromFile(Application.StartupPath & "\Scripts\" & MainWindow.DommePersonalityComboBox.Text & "\Apps\Games\_CardBackPicture.png")
            End If
            LBLExchangeBronze.Text = MainWindow.ssh.BronzeTokens
            LBLExchangeSilver.Text = MainWindow.ssh.SilverTokens
            LBLExchangeGold.Text = MainWindow.ssh.GoldTokens
        End If

        If TCGames.SelectedIndex = 4 Then



            BronzeQ1.Text = My.Settings.B1
            BronzeQ2.Text = My.Settings.B2
            BronzeQ3.Text = My.Settings.B3
            BronzeQ4.Text = My.Settings.B4
            BronzeQ5.Text = My.Settings.B5
            BronzeQ6.Text = My.Settings.B6

            SilverQ1.Text = My.Settings.S1
            SilverQ2.Text = My.Settings.S2
            SilverQ3.Text = My.Settings.S3
            SilverQ4.Text = My.Settings.S4
            SilverQ5.Text = My.Settings.S5
            SilverQ6.Text = My.Settings.S6

            GoldQ1.Text = My.Settings.G1
            GoldQ2.Text = My.Settings.G2
            GoldQ3.Text = My.Settings.G3
            GoldQ4.Text = My.Settings.G4
            GoldQ5.Text = My.Settings.G5
            GoldQ6.Text = My.Settings.G6

            If GoldQ1.Text <> 0 Then
                GoldN1.Text = FrmSettings.GN1.Text
                GoldP1.Image = Image.FromFile(My.Settings.GP1)
                GoldP1.Visible = True
            Else
                GoldP1.Visible = False
                GoldN1.Text = ""
            End If

            If GoldQ2.Text <> 0 Then
                GoldN2.Text = FrmSettings.GN2.Text
                GoldP2.Image = Image.FromFile(My.Settings.GP2)
                GoldP2.Visible = True
            Else
                GoldP2.Visible = False
                GoldN2.Text = ""
            End If

            If GoldQ3.Text <> 0 Then
                GoldN3.Text = FrmSettings.GN3.Text
                GoldP3.Image = Image.FromFile(My.Settings.GP3)
                GoldP3.Visible = True
            Else
                GoldP3.Visible = False
                GoldN3.Text = ""
            End If

            If GoldQ4.Text <> 0 Then
                GoldN4.Text = FrmSettings.GN4.Text
                GoldP4.Image = Image.FromFile(My.Settings.GP4)
                GoldP4.Visible = True
            Else
                GoldP4.Visible = False
                GoldN4.Text = ""
            End If

            If GoldQ5.Text <> 0 Then
                GoldN5.Text = FrmSettings.GN5.Text
                GoldP5.Image = Image.FromFile(My.Settings.GP5)
                GoldP5.Visible = True
            Else
                GoldP5.Visible = False
                GoldN5.Text = ""
            End If

            If GoldQ6.Text <> 0 Then
                GoldN6.Text = FrmSettings.GN6.Text
                GoldP6.Image = Image.FromFile(My.Settings.GP6)
                GoldP6.Visible = True
            Else
                GoldP6.Visible = False
                GoldN6.Text = ""
            End If


            If SilverQ1.Text <> 0 Then
                SilverN1.Text = FrmSettings.SN1.Text
                SilverP1.Image = Image.FromFile(My.Settings.SP1)
                SilverP1.Visible = True
            Else
                SilverP1.Visible = False
                SilverN1.Text = ""
            End If

            If SilverQ2.Text <> 0 Then
                SilverN2.Text = FrmSettings.SN2.Text
                SilverP2.Image = Image.FromFile(My.Settings.SP2)
                SilverP2.Visible = True
            Else
                SilverP2.Visible = False
                SilverN2.Text = ""
            End If

            If SilverQ3.Text <> 0 Then
                SilverN3.Text = FrmSettings.SN3.Text
                SilverP3.Image = Image.FromFile(My.Settings.SP3)
                SilverP3.Visible = True
            Else
                SilverP3.Visible = False
                SilverN3.Text = ""
            End If

            If SilverQ4.Text <> 0 Then
                SilverN4.Text = FrmSettings.SN4.Text
                SilverP4.Image = Image.FromFile(My.Settings.SP4)
                SilverP4.Visible = True
            Else
                SilverP4.Visible = False
                SilverN4.Text = ""
            End If

            If SilverQ5.Text <> 0 Then
                SilverN5.Text = FrmSettings.SN5.Text
                SilverP5.Image = Image.FromFile(My.Settings.SP5)
                SilverP5.Visible = True
            Else
                SilverP5.Visible = False
                SilverN5.Text = ""
            End If

            If SilverQ6.Text <> 0 Then
                SilverN6.Text = FrmSettings.SN6.Text
                SilverP6.Image = Image.FromFile(My.Settings.SP6)
                SilverP6.Visible = True
            Else
                SilverP6.Visible = False
                SilverN6.Text = ""
            End If


            If BronzeQ1.Text <> 0 Then
                BronzeN1.Text = FrmSettings.BN1.Text
                BronzeP1.Image = Image.FromFile(My.Settings.BP1)
                BronzeP1.Visible = True
            Else
                BronzeP1.Visible = False
                BronzeN1.Text = ""
            End If

            If BronzeQ2.Text <> 0 Then
                BronzeN2.Text = FrmSettings.BN2.Text
                BronzeP2.Image = Image.FromFile(My.Settings.BP2)
                BronzeP2.Visible = True
            Else
                BronzeP2.Visible = False
                BronzeN2.Text = ""
            End If

            If BronzeQ3.Text <> 0 Then
                BronzeN3.Text = FrmSettings.BN3.Text
                BronzeP3.Image = Image.FromFile(My.Settings.BP3)
                BronzeP3.Visible = True
            Else
                BronzeP3.Visible = False
                BronzeN3.Text = ""
            End If

            If BronzeQ4.Text <> 0 Then
                BronzeN4.Text = FrmSettings.BN4.Text
                BronzeP4.Image = Image.FromFile(My.Settings.BP4)
                BronzeP4.Visible = True
            Else
                BronzeP4.Visible = False
                BronzeN4.Text = ""
            End If

            If BronzeQ5.Text <> 0 Then
                BronzeN5.Text = FrmSettings.BN5.Text
                BronzeP5.Image = Image.FromFile(My.Settings.BP5)
                BronzeP5.Visible = True
            Else
                BronzeP5.Visible = False
                BronzeN5.Text = ""
            End If

            If BronzeQ6.Text <> 0 Then
                BronzeN6.Text = FrmSettings.BN6.Text
                BronzeP6.Image = Image.FromFile(My.Settings.BP6)
                BronzeP6.Visible = True
            Else
                BronzeP6.Visible = False
                BronzeN6.Text = ""
            End If







        End If



    End Sub

    Private Sub ShuffleTimer_Tick(sender As Object, e As EventArgs) Handles ShuffleTimer.Tick

        ShuffleTick -= 1

        If ShuffleTick = 18 Then M1A.Visible = True
        If ShuffleTick = 17 Then M2A.Visible = True
        If ShuffleTick = 16 Then M3A.Visible = True
        If ShuffleTick = 15 Then M4A.Visible = True
        If ShuffleTick = 14 Then M5A.Visible = True
        If ShuffleTick = 13 Then M6A.Visible = True

        If ShuffleTick = 12 Then M1B.Visible = True
        If ShuffleTick = 11 Then M2B.Visible = True
        If ShuffleTick = 10 Then M3B.Visible = True
        If ShuffleTick = 9 Then M4B.Visible = True
        If ShuffleTick = 8 Then M5B.Visible = True
        If ShuffleTick = 7 Then M6B.Visible = True

        If ShuffleTick = 6 Then M1C.Visible = True
        If ShuffleTick = 5 Then M2C.Visible = True
        If ShuffleTick = 4 Then M3C.Visible = True
        If ShuffleTick = 3 Then M4C.Visible = True
        If ShuffleTick = 2 Then M5C.Visible = True
        If ShuffleTick = 1 Then M6C.Visible = True

        If ShuffleTick = 0 Then
            GameOn = True
            ShuffleTimer.Stop()

            M1A.Enabled = True
            M2A.Enabled = True
            M3A.Enabled = True
            M4A.Enabled = True
            M5A.Enabled = True
            M6A.Enabled = True

            M1B.Enabled = True
            M2B.Enabled = True
            M3B.Enabled = True
            M4B.Enabled = True
            M5B.Enabled = True
            M6B.Enabled = True

            M1C.Enabled = True
            M2C.Enabled = True
            M3C.Enabled = True
            M4C.Enabled = True
            M5C.Enabled = True
            M6C.Enabled = True

            RemindCards()
        End If




    End Sub

    Private Sub BuyBoosterButton_Click(sender As Object, e As EventArgs) Handles BTNBoosterBuy.Click

        If MainWindow.ssh.BronzeTokens < 25 Then Return

        MainWindow.ssh.BronzeTokens -= 25
        LBLExchangeBronze.Text = MainWindow.ssh.BronzeTokens
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
        My.Settings.Save()

        BoosterListBronze.Clear()
        BoosterListSilver.Clear()
        BoosterListGold.Clear()

        BoosterListBronze.Add(My.Settings.BP1)
        BoosterListBronze.Add(My.Settings.BP2)
        BoosterListBronze.Add(My.Settings.BP3)
        BoosterListBronze.Add(My.Settings.BP4)
        BoosterListBronze.Add(My.Settings.BP5)
        BoosterListBronze.Add(My.Settings.BP6)

        BoosterListSilver.Add(My.Settings.SP1)
        BoosterListSilver.Add(My.Settings.SP2)
        BoosterListSilver.Add(My.Settings.SP3)
        BoosterListSilver.Add(My.Settings.SP4)
        BoosterListSilver.Add(My.Settings.SP5)
        BoosterListSilver.Add(My.Settings.SP6)

        BoosterListGold.Add(My.Settings.GP1)
        BoosterListGold.Add(My.Settings.GP2)
        BoosterListGold.Add(My.Settings.GP3)
        BoosterListGold.Add(My.Settings.GP4)
        BoosterListGold.Add(My.Settings.GP5)
        BoosterListGold.Add(My.Settings.GP6)

        BoosterTick = 6
        BTNBoosterBuy.Enabled = False

        Booster1.Visible = False
        Booster2.Visible = False
        Booster3.Visible = False
        Booster4.Visible = False
        Booster5.Visible = False



        Try
            Booster1.Image.Dispose()
        Catch
        End Try
        Booster1.Image = Nothing
        Try
            Booster2.Image.Dispose()
        Catch
        End Try
        Booster2.Image = Nothing
        Try
            Booster3.Image.Dispose()
        Catch
        End Try
        Booster3.Image = Nothing
        Try
            Booster4.Image.Dispose()
        Catch
        End Try
        Booster4.Image = Nothing
        Try
            Booster5.Image.Dispose()
        Catch
        End Try
        Booster5.Image = Nothing
        Try
            GC.Collect()
        Catch
        End Try

        Booster1Frame.BackColor = Color.DimGray
        Booster1Plate.BackColor = Color.DimGray
        Booster2Frame.BackColor = Color.DimGray
        Booster2Plate.BackColor = Color.DimGray
        Booster3Frame.BackColor = Color.DimGray
        Booster3Plate.BackColor = Color.DimGray
        Booster4Frame.BackColor = Color.DimGray
        Booster4Plate.BackColor = Color.DimGray
        Booster5Frame.BackColor = Color.DimGray
        Booster5Plate.BackColor = Color.DimGray

        Booster1Name.Text = ""
        Booster2Name.Text = ""
        Booster3Name.Text = ""
        Booster4Name.Text = ""
        Booster5Name.Text = ""



        BoosterTimer.Start()
        CheckExchange()



    End Sub

    Private Sub BoosterTimer_Tick(sender As Object, e As EventArgs) Handles BoosterTimer.Tick

        BoosterTick -= 1
        Dim ColorVal As Integer



        If BoosterTick = 5 Then

            TempVal = randomizer.Next(1, 101)

            If TempVal > 20 Then
                Booster1Frame.BackColor = Color.Peru
                Booster1Plate.BackColor = Color.Peru
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster1.Image = Image.FromFile(BoosterListBronze(0))
                    Booster1Name.Text = FrmSettings.BN1.Text
                    My.Settings.B1 += 1
                End If
                If ColorVal = 2 Then
                    Booster1.Image = Image.FromFile(BoosterListBronze(1))
                    Booster1Name.Text = FrmSettings.BN2.Text
                    My.Settings.B2 += 1
                End If
                If ColorVal = 3 Then
                    Booster1.Image = Image.FromFile(BoosterListBronze(2))
                    Booster1Name.Text = FrmSettings.BN3.Text
                    My.Settings.B3 += 1
                End If
                If ColorVal = 4 Then
                    Booster1.Image = Image.FromFile(BoosterListBronze(3))
                    Booster1Name.Text = FrmSettings.BN4.Text
                    My.Settings.B4 += 1
                End If
                If ColorVal = 5 Then
                    Booster1.Image = Image.FromFile(BoosterListBronze(4))
                    Booster1Name.Text = FrmSettings.BN5.Text
                    My.Settings.B5 += 1
                End If
                If ColorVal = 6 Then
                    Booster1.Image = Image.FromFile(BoosterListBronze(5))
                    Booster1Name.Text = FrmSettings.BN6.Text
                    My.Settings.B6 += 1
                End If
            End If

            If TempVal > 5 And TempVal < 21 Then
                Booster1Frame.BackColor = Color.Silver
                Booster1Plate.BackColor = Color.Silver
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster1.Image = Image.FromFile(BoosterListSilver(0))
                    Booster1Name.Text = FrmSettings.SN1.Text
                    My.Settings.S1 += 1
                End If
                If ColorVal = 2 Then
                    Booster1.Image = Image.FromFile(BoosterListSilver(1))
                    Booster1Name.Text = FrmSettings.SN2.Text
                    My.Settings.S2 += 1
                End If
                If ColorVal = 3 Then
                    Booster1.Image = Image.FromFile(BoosterListSilver(2))
                    Booster1Name.Text = FrmSettings.SN3.Text
                    My.Settings.S3 += 1
                End If
                If ColorVal = 4 Then
                    Booster1.Image = Image.FromFile(BoosterListSilver(3))
                    Booster1Name.Text = FrmSettings.SN4.Text
                    My.Settings.S4 += 1
                End If
                If ColorVal = 5 Then
                    Booster1.Image = Image.FromFile(BoosterListSilver(4))
                    Booster1Name.Text = FrmSettings.SN5.Text
                    My.Settings.S5 += 1
                End If
                If ColorVal = 6 Then
                    Booster1.Image = Image.FromFile(BoosterListSilver(5))
                    Booster1Name.Text = FrmSettings.SN6.Text
                    My.Settings.S6 += 1
                End If
            End If

            If TempVal < 6 Then
                Booster1Frame.BackColor = Color.Gold
                Booster1Plate.BackColor = Color.Gold
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster1.Image = Image.FromFile(BoosterListGold(0))
                    Booster1Name.Text = FrmSettings.GN1.Text
                    My.Settings.G1 += 1
                End If
                If ColorVal = 2 Then
                    Booster1.Image = Image.FromFile(BoosterListGold(1))
                    Booster1Name.Text = FrmSettings.GN2.Text
                    My.Settings.G2 += 1
                End If
                If ColorVal = 3 Then
                    Booster1.Image = Image.FromFile(BoosterListGold(2))
                    Booster1Name.Text = FrmSettings.GN3.Text
                    My.Settings.G3 += 1
                End If
                If ColorVal = 4 Then
                    Booster1.Image = Image.FromFile(BoosterListGold(3))
                    Booster1Name.Text = FrmSettings.GN4.Text
                    My.Settings.G4 += 1
                End If
                If ColorVal = 5 Then
                    Booster1.Image = Image.FromFile(BoosterListGold(4))
                    Booster1Name.Text = FrmSettings.GN5.Text
                    My.Settings.G5 += 1
                End If
                If ColorVal = 6 Then
                    Booster1.Image = Image.FromFile(BoosterListGold(5))
                    Booster1Name.Text = FrmSettings.GN6.Text
                    My.Settings.G6 += 1
                End If
            End If
            Booster1.Visible = True
        End If


        If BoosterTick = 4 Then

            TempVal = randomizer.Next(1, 101)

            If TempVal > 20 Then
                Booster2Frame.BackColor = Color.Peru
                Booster2Plate.BackColor = Color.Peru
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster2.Image = Image.FromFile(BoosterListBronze(0))
                    Booster2Name.Text = FrmSettings.BN1.Text
                    My.Settings.B1 += 1
                End If
                If ColorVal = 2 Then
                    Booster2.Image = Image.FromFile(BoosterListBronze(1))
                    Booster2Name.Text = FrmSettings.BN2.Text
                    My.Settings.B2 += 1
                End If
                If ColorVal = 3 Then
                    Booster2.Image = Image.FromFile(BoosterListBronze(2))
                    Booster2Name.Text = FrmSettings.BN3.Text
                    My.Settings.B3 += 1
                End If
                If ColorVal = 4 Then
                    Booster2.Image = Image.FromFile(BoosterListBronze(3))
                    Booster2Name.Text = FrmSettings.BN4.Text
                    My.Settings.B4 += 1
                End If
                If ColorVal = 5 Then
                    Booster2.Image = Image.FromFile(BoosterListBronze(4))
                    Booster2Name.Text = FrmSettings.BN5.Text
                    My.Settings.B5 += 1
                End If
                If ColorVal = 6 Then
                    Booster2.Image = Image.FromFile(BoosterListBronze(5))
                    Booster2Name.Text = FrmSettings.BN6.Text
                    My.Settings.B6 += 1
                End If
            End If

            If TempVal > 5 And TempVal < 21 Then
                Booster2Frame.BackColor = Color.Silver
                Booster2Plate.BackColor = Color.Silver
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster2.Image = Image.FromFile(BoosterListSilver(0))
                    Booster2Name.Text = FrmSettings.SN1.Text
                    My.Settings.S1 += 1
                End If
                If ColorVal = 2 Then
                    Booster2.Image = Image.FromFile(BoosterListSilver(1))
                    Booster2Name.Text = FrmSettings.SN2.Text
                    My.Settings.S2 += 1
                End If
                If ColorVal = 3 Then
                    Booster2.Image = Image.FromFile(BoosterListSilver(2))
                    Booster2Name.Text = FrmSettings.SN3.Text
                    My.Settings.S3 += 1
                End If
                If ColorVal = 4 Then
                    Booster2.Image = Image.FromFile(BoosterListSilver(3))
                    Booster2Name.Text = FrmSettings.SN4.Text
                    My.Settings.S4 += 1
                End If
                If ColorVal = 5 Then
                    Booster2.Image = Image.FromFile(BoosterListSilver(4))
                    Booster2Name.Text = FrmSettings.SN5.Text
                    My.Settings.S5 += 1
                End If
                If ColorVal = 6 Then
                    Booster2.Image = Image.FromFile(BoosterListSilver(5))
                    Booster2Name.Text = FrmSettings.SN6.Text
                    My.Settings.S6 += 1
                End If
            End If

            If TempVal < 6 Then
                Booster2Frame.BackColor = Color.Gold
                Booster2Plate.BackColor = Color.Gold
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster2.Image = Image.FromFile(BoosterListGold(0))
                    Booster2Name.Text = FrmSettings.GN1.Text
                    My.Settings.G1 += 1
                End If
                If ColorVal = 2 Then
                    Booster2.Image = Image.FromFile(BoosterListGold(1))
                    Booster2Name.Text = FrmSettings.GN2.Text
                    My.Settings.G2 += 1
                End If
                If ColorVal = 3 Then
                    Booster2.Image = Image.FromFile(BoosterListGold(2))
                    Booster2Name.Text = FrmSettings.GN3.Text
                    My.Settings.G3 += 1
                End If
                If ColorVal = 4 Then
                    Booster2.Image = Image.FromFile(BoosterListGold(3))
                    Booster2Name.Text = FrmSettings.GN4.Text
                    My.Settings.G4 += 1
                End If
                If ColorVal = 5 Then
                    Booster2.Image = Image.FromFile(BoosterListGold(4))
                    Booster2Name.Text = FrmSettings.GN5.Text
                    My.Settings.G5 += 1
                End If
                If ColorVal = 6 Then
                    Booster2.Image = Image.FromFile(BoosterListGold(5))
                    Booster2Name.Text = FrmSettings.GN6.Text
                    My.Settings.G6 += 1
                End If
            End If
            Booster2.Visible = True
        End If

        If BoosterTick = 3 Then

            TempVal = randomizer.Next(1, 101)

            If TempVal > 20 Then
                Booster3Frame.BackColor = Color.Peru
                Booster3Plate.BackColor = Color.Peru
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster3.Image = Image.FromFile(BoosterListBronze(0))
                    Booster3Name.Text = FrmSettings.BN1.Text
                    My.Settings.B1 += 1
                End If
                If ColorVal = 2 Then
                    Booster3.Image = Image.FromFile(BoosterListBronze(1))
                    Booster3Name.Text = FrmSettings.BN2.Text
                    My.Settings.B2 += 1
                End If
                If ColorVal = 3 Then
                    Booster3.Image = Image.FromFile(BoosterListBronze(2))
                    Booster3Name.Text = FrmSettings.BN3.Text
                    My.Settings.B3 += 1
                End If
                If ColorVal = 4 Then
                    Booster3.Image = Image.FromFile(BoosterListBronze(3))
                    Booster3Name.Text = FrmSettings.BN4.Text
                    My.Settings.B4 += 1
                End If
                If ColorVal = 5 Then
                    Booster3.Image = Image.FromFile(BoosterListBronze(4))
                    Booster3Name.Text = FrmSettings.BN5.Text
                    My.Settings.B5 += 1
                End If
                If ColorVal = 6 Then
                    Booster3.Image = Image.FromFile(BoosterListBronze(5))
                    Booster3Name.Text = FrmSettings.BN6.Text
                    My.Settings.B6 += 1
                End If
            End If

            If TempVal > 5 And TempVal < 21 Then
                Booster3Frame.BackColor = Color.Silver
                Booster3Plate.BackColor = Color.Silver
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster3.Image = Image.FromFile(BoosterListSilver(0))
                    Booster3Name.Text = FrmSettings.SN1.Text
                    My.Settings.S1 += 1
                End If
                If ColorVal = 2 Then
                    Booster3.Image = Image.FromFile(BoosterListSilver(1))
                    Booster3Name.Text = FrmSettings.SN2.Text
                    My.Settings.S2 += 1
                End If
                If ColorVal = 3 Then
                    Booster3.Image = Image.FromFile(BoosterListSilver(2))
                    Booster3Name.Text = FrmSettings.SN3.Text
                    My.Settings.S3 += 1
                End If
                If ColorVal = 4 Then
                    Booster3.Image = Image.FromFile(BoosterListSilver(3))
                    Booster3Name.Text = FrmSettings.SN4.Text
                    My.Settings.S4 += 1
                End If
                If ColorVal = 5 Then
                    Booster3.Image = Image.FromFile(BoosterListSilver(4))
                    Booster3Name.Text = FrmSettings.SN5.Text
                    My.Settings.S5 += 1
                End If
                If ColorVal = 6 Then
                    Booster3.Image = Image.FromFile(BoosterListSilver(5))
                    Booster3Name.Text = FrmSettings.SN6.Text
                    My.Settings.S6 += 1
                End If
            End If

            If TempVal < 6 Then
                Booster3Frame.BackColor = Color.Gold
                Booster3Plate.BackColor = Color.Gold
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster3.Image = Image.FromFile(BoosterListGold(0))
                    Booster3Name.Text = FrmSettings.GN1.Text
                    My.Settings.G1 += 1
                End If
                If ColorVal = 2 Then
                    Booster3.Image = Image.FromFile(BoosterListGold(1))
                    Booster3Name.Text = FrmSettings.GN2.Text
                    My.Settings.G2 += 1
                End If
                If ColorVal = 3 Then
                    Booster3.Image = Image.FromFile(BoosterListGold(2))
                    Booster3Name.Text = FrmSettings.GN3.Text
                    My.Settings.G3 += 1
                End If
                If ColorVal = 4 Then
                    Booster3.Image = Image.FromFile(BoosterListGold(3))
                    Booster3Name.Text = FrmSettings.GN4.Text
                    My.Settings.G4 += 1
                End If
                If ColorVal = 5 Then
                    Booster3.Image = Image.FromFile(BoosterListGold(4))
                    Booster3Name.Text = FrmSettings.GN5.Text
                    My.Settings.G5 += 1
                End If
                If ColorVal = 6 Then
                    Booster3.Image = Image.FromFile(BoosterListGold(5))
                    Booster3Name.Text = FrmSettings.GN6.Text
                    My.Settings.G6 += 1
                End If
            End If
            Booster3.Visible = True
        End If

        If BoosterTick = 2 Then

            TempVal = randomizer.Next(1, 101)

            If TempVal > 20 Then
                Booster4Frame.BackColor = Color.Peru
                Booster4Plate.BackColor = Color.Peru
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster4.Image = Image.FromFile(BoosterListBronze(0))
                    Booster4Name.Text = FrmSettings.BN1.Text
                    My.Settings.B1 += 1
                End If
                If ColorVal = 2 Then
                    Booster4.Image = Image.FromFile(BoosterListBronze(1))
                    Booster4Name.Text = FrmSettings.BN2.Text
                    My.Settings.B2 += 1
                End If
                If ColorVal = 3 Then
                    Booster4.Image = Image.FromFile(BoosterListBronze(2))
                    Booster4Name.Text = FrmSettings.BN3.Text
                    My.Settings.B3 += 1
                End If
                If ColorVal = 4 Then
                    Booster4.Image = Image.FromFile(BoosterListBronze(3))
                    Booster4Name.Text = FrmSettings.BN4.Text
                    My.Settings.B4 += 1
                End If
                If ColorVal = 5 Then
                    Booster4.Image = Image.FromFile(BoosterListBronze(4))
                    Booster4Name.Text = FrmSettings.BN5.Text
                    My.Settings.B5 += 1
                End If
                If ColorVal = 6 Then
                    Booster4.Image = Image.FromFile(BoosterListBronze(5))
                    Booster4Name.Text = FrmSettings.BN6.Text
                    My.Settings.B6 += 1
                End If
            End If

            If TempVal > 5 And TempVal < 21 Then
                Booster4Frame.BackColor = Color.Silver
                Booster4Plate.BackColor = Color.Silver
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster4.Image = Image.FromFile(BoosterListSilver(0))
                    Booster4Name.Text = FrmSettings.SN1.Text
                    My.Settings.S1 += 1
                End If
                If ColorVal = 2 Then
                    Booster4.Image = Image.FromFile(BoosterListSilver(1))
                    Booster4Name.Text = FrmSettings.SN2.Text
                    My.Settings.S2 += 1
                End If
                If ColorVal = 3 Then
                    Booster4.Image = Image.FromFile(BoosterListSilver(2))
                    Booster4Name.Text = FrmSettings.SN3.Text
                    My.Settings.S3 += 1
                End If
                If ColorVal = 4 Then
                    Booster4.Image = Image.FromFile(BoosterListSilver(3))
                    Booster4Name.Text = FrmSettings.SN4.Text
                    My.Settings.S4 += 1
                End If
                If ColorVal = 5 Then
                    Booster4.Image = Image.FromFile(BoosterListSilver(4))
                    Booster4Name.Text = FrmSettings.SN5.Text
                    My.Settings.S5 += 1
                End If
                If ColorVal = 6 Then
                    Booster4.Image = Image.FromFile(BoosterListSilver(5))
                    Booster4Name.Text = FrmSettings.SN6.Text
                    My.Settings.S6 += 1
                End If
            End If

            If TempVal < 6 Then
                Booster4Frame.BackColor = Color.Gold
                Booster4Plate.BackColor = Color.Gold
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster4.Image = Image.FromFile(BoosterListGold(0))
                    Booster4Name.Text = FrmSettings.GN1.Text
                    My.Settings.G1 += 1
                End If
                If ColorVal = 2 Then
                    Booster4.Image = Image.FromFile(BoosterListGold(1))
                    Booster4Name.Text = FrmSettings.GN2.Text
                    My.Settings.G2 += 1
                End If
                If ColorVal = 3 Then
                    Booster4.Image = Image.FromFile(BoosterListGold(2))
                    Booster4Name.Text = FrmSettings.GN3.Text
                    My.Settings.G3 += 1
                End If
                If ColorVal = 4 Then
                    Booster4.Image = Image.FromFile(BoosterListGold(3))
                    Booster4Name.Text = FrmSettings.GN4.Text
                    My.Settings.G4 += 1
                End If
                If ColorVal = 5 Then
                    Booster4.Image = Image.FromFile(BoosterListGold(4))
                    Booster4Name.Text = FrmSettings.GN5.Text
                    My.Settings.G5 += 1
                End If
                If ColorVal = 6 Then
                    Booster4.Image = Image.FromFile(BoosterListGold(5))
                    Booster4Name.Text = FrmSettings.GN6.Text
                    My.Settings.G6 += 1
                End If
            End If
            Booster4.Visible = True
        End If

        If BoosterTick = 1 Then

            TempVal = randomizer.Next(1, 101)

            If TempVal > 20 Then
                Booster5Frame.BackColor = Color.Peru
                Booster5Plate.BackColor = Color.Peru
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster5.Image = Image.FromFile(BoosterListBronze(0))
                    Booster5Name.Text = FrmSettings.BN1.Text
                    My.Settings.B1 += 1
                End If
                If ColorVal = 2 Then
                    Booster5.Image = Image.FromFile(BoosterListBronze(1))
                    Booster5Name.Text = FrmSettings.BN2.Text
                    My.Settings.B2 += 1
                End If
                If ColorVal = 3 Then
                    Booster5.Image = Image.FromFile(BoosterListBronze(2))
                    Booster5Name.Text = FrmSettings.BN3.Text
                    My.Settings.B3 += 1
                End If
                If ColorVal = 4 Then
                    Booster5.Image = Image.FromFile(BoosterListBronze(3))
                    Booster5Name.Text = FrmSettings.BN4.Text
                    My.Settings.B4 += 1
                End If
                If ColorVal = 5 Then
                    Booster5.Image = Image.FromFile(BoosterListBronze(4))
                    Booster5Name.Text = FrmSettings.BN5.Text
                    My.Settings.B5 += 1
                End If
                If ColorVal = 6 Then
                    Booster5.Image = Image.FromFile(BoosterListBronze(5))
                    Booster5Name.Text = FrmSettings.BN6.Text
                    My.Settings.B6 += 1
                End If
            End If

            If TempVal > 5 And TempVal < 21 Then
                Booster5Frame.BackColor = Color.Silver
                Booster5Plate.BackColor = Color.Silver
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster5.Image = Image.FromFile(BoosterListSilver(0))
                    Booster5Name.Text = FrmSettings.SN1.Text
                    My.Settings.S1 += 1
                End If
                If ColorVal = 2 Then
                    Booster5.Image = Image.FromFile(BoosterListSilver(1))
                    Booster5Name.Text = FrmSettings.SN2.Text
                    My.Settings.S2 += 1
                End If
                If ColorVal = 3 Then
                    Booster5.Image = Image.FromFile(BoosterListSilver(2))
                    Booster5Name.Text = FrmSettings.SN3.Text
                    My.Settings.S3 += 1
                End If
                If ColorVal = 4 Then
                    Booster5.Image = Image.FromFile(BoosterListSilver(3))
                    Booster5Name.Text = FrmSettings.SN4.Text
                    My.Settings.S4 += 1
                End If
                If ColorVal = 5 Then
                    Booster5.Image = Image.FromFile(BoosterListSilver(4))
                    Booster5Name.Text = FrmSettings.SN5.Text
                    My.Settings.S5 += 1
                End If
                If ColorVal = 6 Then
                    Booster5.Image = Image.FromFile(BoosterListSilver(5))
                    Booster5Name.Text = FrmSettings.SN6.Text
                    My.Settings.S6 += 1
                End If
            End If

            If TempVal < 6 Then
                Booster5Frame.BackColor = Color.Gold
                Booster5Plate.BackColor = Color.Gold
                ColorVal = randomizer.Next(1, 7)
                If ColorVal = 1 Then
                    Booster5.Image = Image.FromFile(BoosterListGold(0))
                    Booster5Name.Text = FrmSettings.GN1.Text
                    My.Settings.G1 += 1
                End If
                If ColorVal = 2 Then
                    Booster5.Image = Image.FromFile(BoosterListGold(1))
                    Booster5Name.Text = FrmSettings.GN2.Text
                    My.Settings.G2 += 1
                End If
                If ColorVal = 3 Then
                    Booster5.Image = Image.FromFile(BoosterListGold(2))
                    Booster5Name.Text = FrmSettings.GN3.Text
                    My.Settings.G3 += 1
                End If
                If ColorVal = 4 Then
                    Booster5.Image = Image.FromFile(BoosterListGold(3))
                    Booster5Name.Text = FrmSettings.GN4.Text
                    My.Settings.G4 += 1
                End If
                If ColorVal = 5 Then
                    Booster5.Image = Image.FromFile(BoosterListGold(4))
                    Booster5Name.Text = FrmSettings.GN5.Text
                    My.Settings.G5 += 1
                End If
                If ColorVal = 6 Then
                    Booster5.Image = Image.FromFile(BoosterListGold(5))
                    Booster5Name.Text = FrmSettings.GN6.Text
                    My.Settings.G6 += 1
                End If
            End If
            Booster5.Visible = True
        End If





        My.Settings.Save()
        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\CardFlip.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\CardFlip.wav"
        End If
        'My.Computer.Audio.Play(Application.StartupPath & "\Audio\System\CardFlip.wav")

        If BoosterTick = 1 Then
            BoosterTimer.Stop()
            CheckExchange()
        End If


    End Sub

    Public Sub UpdateBronzeTokens()

        LBLSlotTokens.Text = MainWindow.ssh.BronzeTokens
        LBLMatchTokens.Text = MainWindow.ssh.BronzeTokens
        LBLExchangeBronze.Text = MainWindow.ssh.BronzeTokens

    End Sub

    Public Sub CheckExchange()

        If My.Settings.B1 > 0 And My.Settings.B2 > 0 And My.Settings.B3 > 0 And My.Settings.B4 > 0 And My.Settings.B5 > 0 And My.Settings.B6 > 0 Then
            BTNExchange1.Enabled = True
            BTNExchange2.Enabled = True
        Else
            BTNExchange1.Enabled = False
            BTNExchange2.Enabled = False
        End If

        If My.Settings.S1 > 0 And My.Settings.S2 > 0 And My.Settings.S3 > 0 And My.Settings.S4 > 0 And My.Settings.S5 > 0 And My.Settings.S6 > 0 Then
            BTNExchange3.Enabled = True
            BTNExchange4.Enabled = True
        Else
            BTNExchange3.Enabled = False
            BTNExchange4.Enabled = False
        End If

        If My.Settings.G1 > 0 And My.Settings.G2 > 0 And My.Settings.G3 > 0 And My.Settings.G4 > 0 And My.Settings.G5 > 0 And My.Settings.G6 > 0 Then
            BTNExchange5.Enabled = True
        Else
            BTNExchange5.Enabled = False
        End If

        If MainWindow.ssh.SilverTokens > 0 Then
            BTNExchange6.Enabled = True
        Else
            BTNExchange6.Enabled = False
        End If

        If MainWindow.ssh.BronzeTokens > 24 And BoosterTimer.Enabled = False Then
            BTNBoosterBuy.Enabled = True
        Else
            BTNBoosterBuy.Enabled = False
        End If

    End Sub

    Public Sub ClearExchange()

        ExchangeCard.Visible = False
        ExchangeFrame.BackColor = Color.DimGray
        ExchangePlate.BackColor = Color.DimGray
        LBLExchange.Text = ""
        ExchangeName.Text = ""


    End Sub

    Private Sub BTNExchange1_Click(sender As Object, e As EventArgs) Handles BTNExchange1.Click


        My.Settings.B1 -= 1
        My.Settings.B2 -= 1
        My.Settings.B3 -= 1
        My.Settings.B4 -= 1
        My.Settings.B5 -= 1
        My.Settings.B6 -= 1

        Dim SilverDraw As New List(Of String)
        SilverDraw.Clear()

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\CardFlip.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\CardFlip.wav"
        End If

        ExchangeCard.Visible = True
        ExchangeFrame.BackColor = Color.Silver
        ExchangePlate.BackColor = Color.Silver
        LBLExchange.Text = "You've received a Silver card!"

        SilverDraw.Add(My.Settings.SP1)
        SilverDraw.Add(My.Settings.SP2)
        SilverDraw.Add(My.Settings.SP3)
        SilverDraw.Add(My.Settings.SP4)
        SilverDraw.Add(My.Settings.SP5)
        SilverDraw.Add(My.Settings.SP6)

        TempVal = randomizer.Next(1, 7)

        If TempVal = 1 Then
            My.Settings.S1 += 1
            ExchangeCard.Image = Image.FromFile(SilverDraw(0))
            ExchangeName.Text = FrmSettings.SN1.Text
        End If

        If TempVal = 2 Then
            My.Settings.S2 += 1
            ExchangeCard.Image = Image.FromFile(SilverDraw(1))
            ExchangeName.Text = FrmSettings.SN2.Text
        End If

        If TempVal = 3 Then
            My.Settings.S3 += 1
            ExchangeCard.Image = Image.FromFile(SilverDraw(2))
            ExchangeName.Text = FrmSettings.SN3.Text
        End If

        If TempVal = 4 Then
            My.Settings.S4 += 1
            ExchangeCard.Image = Image.FromFile(SilverDraw(3))
            ExchangeName.Text = FrmSettings.SN4.Text
        End If

        If TempVal = 5 Then
            My.Settings.S5 += 1
            ExchangeCard.Image = Image.FromFile(SilverDraw(4))
            ExchangeName.Text = FrmSettings.SN5.Text
        End If

        If TempVal = 6 Then
            My.Settings.S6 += 1
            ExchangeCard.Image = Image.FromFile(SilverDraw(5))
            ExchangeName.Text = FrmSettings.SN6.Text
        End If

        My.Settings.Save()

        CheckExchange()


    End Sub

    Private Sub BTNExchange2_Click(sender As Object, e As EventArgs) Handles BTNExchange2.Click


        My.Settings.B1 -= 1
        My.Settings.B2 -= 1
        My.Settings.B3 -= 1
        My.Settings.B4 -= 1
        My.Settings.B5 -= 1
        My.Settings.B6 -= 1

        ClearExchange()

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutSmall.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutSmall.wav"
        End If

        MainWindow.ssh.BronzeTokens += 12
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
        LBLExchangeBronze.Text = MainWindow.ssh.BronzeTokens



        My.Settings.Save()

        CheckExchange()


    End Sub

    Private Sub BTNExchange3_Click(sender As Object, e As EventArgs) Handles BTNExchange3.Click

        My.Settings.S1 -= 1
        My.Settings.S2 -= 1
        My.Settings.S3 -= 1
        My.Settings.S4 -= 1
        My.Settings.S5 -= 1
        My.Settings.S6 -= 1

        Dim GoldDraw As New List(Of String)
        GoldDraw.Clear()

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\CardFlip.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\CardFlip.wav"
        End If

        ExchangeCard.Visible = True
        ExchangeFrame.BackColor = Color.Gold
        ExchangePlate.BackColor = Color.Gold
        LBLExchange.Text = "You've received a Gold card!"

        GoldDraw.Add(My.Settings.GP1)
        GoldDraw.Add(My.Settings.GP2)
        GoldDraw.Add(My.Settings.GP3)
        GoldDraw.Add(My.Settings.GP4)
        GoldDraw.Add(My.Settings.GP5)
        GoldDraw.Add(My.Settings.GP6)

        TempVal = randomizer.Next(1, 7)

        If TempVal = 1 Then
            My.Settings.G1 += 1
            ExchangeCard.Image = Image.FromFile(GoldDraw(0))
            ExchangeName.Text = FrmSettings.GN1.Text
        End If

        If TempVal = 2 Then
            My.Settings.G2 += 1
            ExchangeCard.Image = Image.FromFile(GoldDraw(1))
            ExchangeName.Text = FrmSettings.GN2.Text
        End If

        If TempVal = 3 Then
            My.Settings.G3 += 1
            ExchangeCard.Image = Image.FromFile(GoldDraw(2))
            ExchangeName.Text = FrmSettings.GN3.Text
        End If

        If TempVal = 4 Then
            My.Settings.G4 += 1
            ExchangeCard.Image = Image.FromFile(GoldDraw(3))
            ExchangeName.Text = FrmSettings.GN4.Text
        End If

        If TempVal = 5 Then
            My.Settings.G5 += 1
            ExchangeCard.Image = Image.FromFile(GoldDraw(4))
            ExchangeName.Text = FrmSettings.GN5.Text
        End If

        If TempVal = 6 Then
            My.Settings.G6 += 1
            ExchangeCard.Image = Image.FromFile(GoldDraw(5))
            ExchangeName.Text = FrmSettings.GN6.Text
        End If

        My.Settings.Save()

        CheckExchange()



    End Sub

    Private Sub BTNExchange4_Click(sender As Object, e As EventArgs) Handles BTNExchange4.Click

        My.Settings.S1 -= 1
        My.Settings.S2 -= 1
        My.Settings.S3 -= 1
        My.Settings.S4 -= 1
        My.Settings.S5 -= 1
        My.Settings.S6 -= 1

        ClearExchange()

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutOne.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutOne.wav"
        End If

        MainWindow.ssh.SilverTokens += 1
        My.Settings.SilverTokens = MainWindow.ssh.SilverTokens
        LBLExchangeSilver.Text = MainWindow.ssh.SilverTokens



        My.Settings.Save()

        CheckExchange()


    End Sub

    Private Sub BTNExchange5_Click(sender As Object, e As EventArgs) Handles BTNExchange5.Click

        My.Settings.G1 -= 1
        My.Settings.G2 -= 1
        My.Settings.G3 -= 1
        My.Settings.G4 -= 1
        My.Settings.G5 -= 1
        My.Settings.G6 -= 1

        ClearExchange()

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutOne.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutOne.wav"
        End If

        MainWindow.ssh.GoldTokens += 1
        My.Settings.GoldTokens = MainWindow.ssh.GoldTokens
        LBLExchangeGold.Text = MainWindow.ssh.GoldTokens



        My.Settings.Save()

        CheckExchange()

    End Sub

    Private Sub BTNExchange6_Click(sender As Object, e As EventArgs) Handles BTNExchange6.Click

        ClearExchange()

        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\PayoutSmall.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\PayoutSmall.wav"
        End If

        MainWindow.ssh.SilverTokens -= 1
        MainWindow.ssh.BronzeTokens += 50
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens

        LBLExchangeBronze.Text = MainWindow.ssh.BronzeTokens
        LBLExchangeSilver.Text = MainWindow.ssh.SilverTokens

        My.Settings.Save()

        CheckExchange()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BTNTokenRequest.Click



        Dim TokenList As New List(Of String)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & MainWindow.DommePersonalityComboBox.Text & "\Apps\Games\Token Tasks\", FileIO.SearchOption.SearchAllSubDirectories, "*.txt")
            TokenList.Add(foundFile)
        Next
        If TokenList.Count > 0 Then

            MainWindow.ssh.SaidHello = True
            MainWindow.ssh.ShowModule = True
            MainWindow.ssh.FileText = TokenList(randomizer.Next(0, TokenList.Count))
            MainWindow.ssh.StrokeTauntVal = -1
            MainWindow.ssh.ScriptTick = 2
            MainWindow.ScriptTimer.Start()

            My.Settings.TokenTasks = FormatDateTime(Now, DateFormat.ShortDate)
            My.Settings.Save()
            BTNTokenRequest.Enabled = False
        Else
            MessageBox.Show(Me, "No tasks found in Token Tasks folder!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If


    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)

        MainWindow.ssh.BronzeTokens += 50
        My.Settings.BronzeTokens = MainWindow.ssh.BronzeTokens

        My.Settings.Save()

        LBLExchangeBronze.Text = MainWindow.ssh.BronzeTokens
        BTNBoosterBuy.Enabled = True

    End Sub

    Private Sub CardRevealTimer_Tick(sender As Object, e As EventArgs) Handles CardRevealTimer.Tick

        RevealTick -= 1

        If RevealTick < 1 Then
            CardRevealTimer.Stop()
            RevealCards = True
        End If

    End Sub

    Public Function CardImage(ByVal ProtoImage As String) As Image

        Dim original As Image


        If ProtoImage.Contains("\") Then
            original = Image.FromFile(ProtoImage)
        Else
            original = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(ProtoImage)))
        End If

        Dim resized As Image = MainWindow.ResizeImage(original, New Size(M1A.Width, M1A.Height))
        original.Dispose()

        Return resized

    End Function

    Private Sub PlayRiskyPickButton_Click(sender As Object, e As EventArgs) Handles PlayRiskyPickButton.Click
        Dim startRP As Result = StartRiskyPick(MainWindow)
        If startRP.IsFailure Then
            MessageBox.Show(Me, startRP.Error.Message, "Unable to play Risky Pick!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End If
    End Sub

    Private Sub RiskyCaseButton_Click(sender As Object, e As EventArgs) Handles RiskyCase1Button.Click, RiskyCase2Button.Click, BTNRisk3.Click,
        BTNRisk4.Click, BTNRisk5.Click, BTNRisk6.Click, BTNRisk7.Click, BTNRisk8.Click, BTNRisk9.Click, BTNRisk10.Click,
        BTNRisk11.Click, BTNRisk12.Click, BTNRisk13.Click, BTNRisk14.Click, BTNRisk15.Click, BTNRisk16.Click, BTNRisk17.Click, BTNRisk18.Click,
        BTNRisk19.Click, BTNRisk20.Click, BTNRisk21.Click, BTNRisk22.Click, BTNRisk23.Click, BTNRisk24.Click
        Dim caseButton = CType(sender, Button)
        If caseButton.BackColor = mySelectedCase Then Return
        RiskyPickChosenCaseNumber = GetCaseNumber(caseButton)
        Dim command As String = Keyword.RiskyPickSelectCase + RiskyPickChosenCaseNumber.ToString() + ")"
        Dim selectCase As Result = MainWindow.SendCommand(command) _
            .OnSuccess(Sub()
                           Dim gameBoard = MainWindow.GetGameBoard()
                           RiskyPickChosenCaseEdges = gameBoard.Cases(RiskyPickChosenCaseNumber).ToString()
                           UpdateUiFromBoard(gameBoard)
                           caseButton.BackColor = mySelectedCase

                           MainWindow.SendCommand(Keyword.Unpause)
                       End Sub)
    End Sub

    ''' <summary>
    ''' Disable all risky pick cases
    ''' </summary>
    Public Sub DisableCases()
        RiskyCase1Button.Enabled = False
        RiskyCase2Button.Enabled = False
        BTNRisk3.Enabled = False
        BTNRisk4.Enabled = False
        BTNRisk5.Enabled = False
        BTNRisk6.Enabled = False
        BTNRisk7.Enabled = False
        BTNRisk8.Enabled = False
        BTNRisk9.Enabled = False
        BTNRisk10.Enabled = False
        BTNRisk11.Enabled = False
        BTNRisk12.Enabled = False
        BTNRisk13.Enabled = False
        BTNRisk14.Enabled = False
        BTNRisk15.Enabled = False
        BTNRisk16.Enabled = False
        BTNRisk17.Enabled = False
        BTNRisk18.Enabled = False
        BTNRisk19.Enabled = False
        BTNRisk20.Enabled = False
        BTNRisk21.Enabled = False
        BTNRisk22.Enabled = False
        BTNRisk23.Enabled = False
        BTNRisk24.Enabled = False
    End Sub

    Public Sub RevealUserCase()

        'RiskyCase1Button.Text = ""
        'RiskyCase2Button.Text = ""
        'BTNRisk3.Text = ""
        'BTNRisk4.Text = ""
        'BTNRisk5.Text = ""
        'BTNRisk6.Text = ""
        'BTNRisk7.Text = ""
        'BTNRisk8.Text = ""
        'BTNRisk9.Text = ""
        'BTNRisk10.Text = ""
        'BTNRisk11.Text = ""
        'BTNRisk12.Text = ""
        'BTNRisk13.Text = ""
        'BTNRisk14.Text = ""
        'BTNRisk15.Text = ""
        'BTNRisk16.Text = ""
        'BTNRisk17.Text = ""
        'BTNRisk18.Text = ""
        'BTNRisk19.Text = ""
        'BTNRisk20.Text = ""
        'BTNRisk21.Text = ""
        'BTNRisk22.Text = ""
        'BTNRisk23.Text = ""
        'BTNRisk24.Text = ""

        'If PlayRiskyPickButton.Text = "1" Then RiskyCase1Button.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "2" Then RiskyCase2Button.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "3" Then BTNRisk3.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "4" Then BTNRisk4.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "5" Then BTNRisk5.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "6" Then BTNRisk6.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "7" Then BTNRisk7.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "8" Then BTNRisk8.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "9" Then BTNRisk9.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "10" Then BTNRisk10.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "11" Then BTNRisk11.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "12" Then BTNRisk12.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "13" Then BTNRisk13.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "14" Then BTNRisk14.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "15" Then BTNRisk15.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "16" Then BTNRisk16.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "17" Then BTNRisk17.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "18" Then BTNRisk18.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "19" Then BTNRisk19.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "20" Then BTNRisk20.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "21" Then BTNRisk21.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "22" Then BTNRisk22.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "23" Then BTNRisk23.Text = PlayRiskyPickButton.Text
        'If PlayRiskyPickButton.Text = "24" Then BTNRisk24.Text = PlayRiskyPickButton.Text

        'SelectedCase1Label.ForeColor = Color.Black
        'SelectedCase2Label.ForeColor = Color.Black
        'SelectedCase3Label.ForeColor = Color.Black
        'SelectedCase4Label.ForeColor = Color.Black
        'SelectedCase5Label.ForeColor = Color.Black
        'SelectedCase6Label.ForeColor = Color.Black

        'SelectedCase1Label.Text = PlayRiskyPickButton.Text
        'SelectedCase2Label.Text = PlayRiskyPickButton.Text
        'SelectedCase3Label.Text = PlayRiskyPickButton.Text
        'SelectedCase4Label.Text = PlayRiskyPickButton.Text
        'SelectedCase5Label.Text = PlayRiskyPickButton.Text
        'SelectedCase6Label.Text = PlayRiskyPickButton.Text


        'ClearCaseLabelsOffer()

        'If RiskyCase1Button.Text <> "" Then
        '    RiskyCase1Contents.ForeColor = Color.Black
        '    RiskyCase1Contents.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If

        'If RiskyCase2Button.Text <> "" Then
        '    RiskyCase2Contents.ForeColor = Color.Black
        '    RiskyCase2Contents.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk3.Text <> "" Then
        '    RiskyCase3Contents.ForeColor = Color.Black
        '    RiskyCase3Contents.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk4.Text <> "" Then
        '    RiskyCase4Contents.ForeColor = Color.Black
        '    RiskyCase4Contents.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk5.Text <> "" Then
        '    Risk5.ForeColor = Color.Black
        '    Risk5.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk6.Text <> "" Then
        '    Risk6.ForeColor = Color.Black
        '    Risk6.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk7.Text <> "" Then
        '    Risk7.ForeColor = Color.Black
        '    Risk7.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk8.Text <> "" Then
        '    Risk8.ForeColor = Color.Black
        '    Risk8.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk9.Text <> "" Then
        '    Risk9.ForeColor = Color.Black
        '    Risk9.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk10.Text <> "" Then
        '    Risk10.ForeColor = Color.Black
        '    Risk10.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk11.Text <> "" Then
        '    Risk11.ForeColor = Color.Black
        '    Risk11.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk12.Text <> "" Then
        '    Risk12.ForeColor = Color.Black
        '    Risk12.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk13.Text <> "" Then
        '    Risk13.ForeColor = Color.Black
        '    Risk13.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk14.Text <> "" Then
        '    Risk14.ForeColor = Color.Black
        '    Risk14.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk15.Text <> "" Then
        '    Risk15.ForeColor = Color.Black
        '    Risk15.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk16.Text <> "" Then
        '    Risk16.ForeColor = Color.Black
        '    Risk16.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk17.Text <> "" Then
        '    Risk17.ForeColor = Color.Black
        '    Risk17.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk18.Text <> "" Then
        '    Risk18.ForeColor = Color.Black
        '    Risk18.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk19.Text <> "" Then
        '    Risk19.ForeColor = Color.Black
        '    Risk19.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk20.Text <> "" Then
        '    Risk20.ForeColor = Color.Black
        '    Risk20.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk21.Text <> "" Then
        '    Risk21.ForeColor = Color.Black
        '    Risk21.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk22.Text <> "" Then
        '    Risk22.ForeColor = Color.Black
        '    Risk22.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk23.Text <> "" Then
        '    Risk23.ForeColor = Color.Black
        '    Risk23.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If
        'If BTNRisk24.Text <> "" Then
        '    Risk24.ForeColor = Color.Black
        '    Risk24.Text = RiskyShuffled(RiskyPickNumber - 1)
        'End If

        EdgesOwed = MainWindow.GetGameBoard().PlayersCase.Edges
        TokensPaid = 1000 / EdgesOwed
        TokensPaid = Math.Ceiling(TokensPaid)
    End Sub

    Public Sub RevealLastCase()
        SelectedCase1Label.ForeColor = Color.Black
        SelectedCase2Label.ForeColor = Color.Black
        SelectedCase3Label.ForeColor = Color.Black
        SelectedCase4Label.ForeColor = Color.Black
        SelectedCase5Label.ForeColor = Color.Black
        SelectedCase6Label.ForeColor = Color.Black

        ClearCaseLabelsOffer()

        'If RiskyCase1Button.Text <> "" Then
        '    RiskyCase1Contents.ForeColor = Color.Black
        '    RiskyCase1Contents.Text = RiskyShuffled(0)
        '    EdgesOwed = Val(RiskyShuffled(0))
        'End If

        'If RiskyCase2Button.Text <> "" Then
        '    RiskyCase2Contents.ForeColor = Color.Black
        '    RiskyCase2Contents.Text = RiskyShuffled(1)
        '    EdgesOwed = Val(RiskyShuffled(1))
        'End If
        'If BTNRisk3.Text <> "" Then
        '    RiskyCase3Contents.ForeColor = Color.Black
        '    RiskyCase3Contents.Text = RiskyShuffled(2)
        '    EdgesOwed = Val(RiskyShuffled(2))
        'End If
        'If BTNRisk4.Text <> "" Then
        '    RiskyCase4Contents.ForeColor = Color.Black
        '    RiskyCase4Contents.Text = RiskyShuffled(3)
        '    EdgesOwed = Val(RiskyShuffled(3))
        'End If
        'If BTNRisk5.Text <> "" Then
        '    Risk5.ForeColor = Color.Black
        '    Risk5.Text = RiskyShuffled(4)
        '    EdgesOwed = Val(RiskyShuffled(4))
        'End If
        'If BTNRisk6.Text <> "" Then
        '    Risk6.ForeColor = Color.Black
        '    Risk6.Text = RiskyShuffled(5)
        '    EdgesOwed = Val(RiskyShuffled(5))
        'End If
        'If BTNRisk7.Text <> "" Then
        '    Risk7.ForeColor = Color.Black
        '    Risk7.Text = RiskyShuffled(6)
        '    EdgesOwed = Val(RiskyShuffled(6))
        'End If
        'If BTNRisk8.Text <> "" Then
        '    Risk8.ForeColor = Color.Black
        '    Risk8.Text = RiskyShuffled(7)
        '    EdgesOwed = Val(RiskyShuffled(7))
        'End If
        'If BTNRisk9.Text <> "" Then
        '    Risk9.ForeColor = Color.Black
        '    Risk9.Text = RiskyShuffled(8)
        '    EdgesOwed = Val(RiskyShuffled(8))
        'End If
        'If BTNRisk10.Text <> "" Then
        '    Risk10.ForeColor = Color.Black
        '    Risk10.Text = RiskyShuffled(9)
        '    EdgesOwed = Val(RiskyShuffled(9))
        'End If
        'If BTNRisk11.Text <> "" Then
        '    Risk11.ForeColor = Color.Black
        '    Risk11.Text = RiskyShuffled(10)
        '    EdgesOwed = Val(RiskyShuffled(10))
        'End If
        'If BTNRisk12.Text <> "" Then
        '    Risk12.ForeColor = Color.Black
        '    Risk12.Text = RiskyShuffled(11)
        '    EdgesOwed = Val(RiskyShuffled(11))
        'End If
        'If BTNRisk13.Text <> "" Then
        '    Risk13.ForeColor = Color.Black
        '    Risk13.Text = RiskyShuffled(12)
        '    EdgesOwed = Val(RiskyShuffled(12))
        'End If
        'If BTNRisk14.Text <> "" Then
        '    Risk14.ForeColor = Color.Black
        '    Risk14.Text = RiskyShuffled(13)
        '    EdgesOwed = Val(Risk14.Text)
        'End If
        'If BTNRisk15.Text <> "" Then
        '    Risk15.ForeColor = Color.Black
        '    Risk15.Text = RiskyShuffled(14)
        '    EdgesOwed = Val(RiskyShuffled(14))
        'End If
        'If BTNRisk16.Text <> "" Then
        '    Risk16.ForeColor = Color.Black
        '    Risk16.Text = RiskyShuffled(15)
        '    EdgesOwed = Val(RiskyShuffled(15))
        'End If
        'If BTNRisk17.Text <> "" Then
        '    Risk17.ForeColor = Color.Black
        '    Risk17.Text = RiskyShuffled(16)
        '    EdgesOwed = Val(RiskyShuffled(16))
        'End If
        'If BTNRisk18.Text <> "" Then
        '    Risk18.ForeColor = Color.Black
        '    Risk18.Text = RiskyShuffled(17)
        '    EdgesOwed = Val(RiskyShuffled(17))
        'End If
        'If BTNRisk19.Text <> "" Then
        '    Risk19.ForeColor = Color.Black
        '    Risk19.Text = RiskyShuffled(18)
        '    EdgesOwed = Val(RiskyShuffled(18))
        'End If
        'If BTNRisk20.Text <> "" Then
        '    Risk20.ForeColor = Color.Black
        '    Risk20.Text = RiskyShuffled(19)
        '    EdgesOwed = Val(RiskyShuffled(19))
        'End If
        'If BTNRisk21.Text <> "" Then
        '    Risk21.ForeColor = Color.Black
        '    Risk21.Text = RiskyShuffled(20)
        '    EdgesOwed = Val(RiskyShuffled(20))
        'End If
        'If BTNRisk22.Text <> "" Then
        '    Risk22.ForeColor = Color.Black
        '    Risk22.Text = RiskyShuffled(21)
        '    EdgesOwed = Val(RiskyShuffled(21))
        'End If
        'If BTNRisk23.Text <> "" Then
        '    Risk23.ForeColor = Color.Black
        '    Risk23.Text = RiskyShuffled(22)
        '    EdgesOwed = Val(RiskyShuffled(22))
        'End If
        'If BTNRisk24.Text <> "" Then
        '    Risk24.ForeColor = Color.Black
        '    Risk24.Text = RiskyShuffled(23)
        '    EdgesOwed = Val(RiskyShuffled(23))
        'End If

        'If LBLRisk100.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 100 Then EdgesOwed = 100
        'If LBLRisk95.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 95 Then EdgesOwed = 95
        'If LBLRisk90.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 90 Then EdgesOwed = 90
        'If LBLRisk85.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 85 Then EdgesOwed = 85
        'If LBLRisk80.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 80 Then EdgesOwed = 80
        'If LBLRisk75.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 75 Then EdgesOwed = 75

        'If LBLRisk70.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 70 Then EdgesOwed = 70
        'If LBLRisk65.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 65 Then EdgesOwed = 65
        'If LBLRisk60.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 60 Then EdgesOwed = 60
        'If LBLRisk55.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 55 Then EdgesOwed = 55
        'If LBLRisk50.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 50 Then EdgesOwed = 50
        'If LBLRisk40.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 40 Then EdgesOwed = 40

        'If LBLRisk30.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 30 Then EdgesOwed = 30
        'If LBLRisk25.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 25 Then EdgesOwed = 25
        'If LBLRisk20.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 20 Then EdgesOwed = 20
        'If LBLRisk15.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 15 Then EdgesOwed = 15
        'If LBLRisk12.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 12 Then EdgesOwed = 12
        'If LBLRisk10.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 10 Then EdgesOwed = 10

        'If LBLRisk7.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 7 Then EdgesOwed = 7
        'If LBLRisk5.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 5 Then EdgesOwed = 5
        'If LBLRisk4.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 4 Then EdgesOwed = 4
        'If LBLRisk3.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 3 Then EdgesOwed = 3
        'If LBLRisk2.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 2 Then EdgesOwed = 2
        'If LBLRisk1.ForeColor = Color.White And Val(RiskyShuffled(RiskyPickNumber - 1)) <> 1 Then EdgesOwed = 1


        TokensPaid = 1000 / EdgesOwed
        TokensPaid = Math.Ceiling(TokensPaid)



    End Sub

    Private Sub TimerRiskyFlash_Tick(sender As Object, e As EventArgs) Handles TimerRiskyFlash.Tick

        RiskyTick -= 1

        If RiskyTick = 2 Then
            If RiskyCase1Button.BackColor = Color.Yellow Then RiskyCase1Button.BackColor = Color.AliceBlue
            If RiskyCase2Button.BackColor = Color.Yellow Then RiskyCase2Button.BackColor = Color.AliceBlue
            If BTNRisk3.BackColor = Color.Yellow Then BTNRisk3.BackColor = Color.AliceBlue
            If BTNRisk4.BackColor = Color.Yellow Then BTNRisk4.BackColor = Color.AliceBlue
            If BTNRisk5.BackColor = Color.Yellow Then BTNRisk5.BackColor = Color.AliceBlue
            If BTNRisk6.BackColor = Color.Yellow Then BTNRisk6.BackColor = Color.AliceBlue
            If BTNRisk7.BackColor = Color.Yellow Then BTNRisk7.BackColor = Color.AliceBlue
            If BTNRisk8.BackColor = Color.Yellow Then BTNRisk8.BackColor = Color.AliceBlue
            If BTNRisk9.BackColor = Color.Yellow Then BTNRisk9.BackColor = Color.AliceBlue
            If BTNRisk10.BackColor = Color.Yellow Then BTNRisk10.BackColor = Color.AliceBlue
            If BTNRisk11.BackColor = Color.Yellow Then BTNRisk11.BackColor = Color.AliceBlue
            If BTNRisk12.BackColor = Color.Yellow Then BTNRisk12.BackColor = Color.AliceBlue
            If BTNRisk13.BackColor = Color.Yellow Then BTNRisk13.BackColor = Color.AliceBlue
            If BTNRisk14.BackColor = Color.Yellow Then BTNRisk14.BackColor = Color.AliceBlue
            If BTNRisk15.BackColor = Color.Yellow Then BTNRisk15.BackColor = Color.AliceBlue
            If BTNRisk16.BackColor = Color.Yellow Then BTNRisk16.BackColor = Color.AliceBlue
            If BTNRisk17.BackColor = Color.Yellow Then BTNRisk17.BackColor = Color.AliceBlue
            If BTNRisk18.BackColor = Color.Yellow Then BTNRisk18.BackColor = Color.AliceBlue
            If BTNRisk19.BackColor = Color.Yellow Then BTNRisk19.BackColor = Color.AliceBlue
            If BTNRisk20.BackColor = Color.Yellow Then BTNRisk20.BackColor = Color.AliceBlue
            If BTNRisk21.BackColor = Color.Yellow Then BTNRisk21.BackColor = Color.AliceBlue
            If BTNRisk22.BackColor = Color.Yellow Then BTNRisk22.BackColor = Color.AliceBlue
            If BTNRisk23.BackColor = Color.Yellow Then BTNRisk23.BackColor = Color.AliceBlue
            If BTNRisk24.BackColor = Color.Yellow Then BTNRisk24.BackColor = Color.AliceBlue
        End If

        If RiskyTick = 1 Then
            If RiskyCase1Button.BackColor = Color.AliceBlue Then RiskyCase1Button.BackColor = Color.Yellow
            If RiskyCase2Button.BackColor = Color.AliceBlue Then RiskyCase2Button.BackColor = Color.Yellow
            If BTNRisk3.BackColor = Color.AliceBlue Then BTNRisk3.BackColor = Color.Yellow
            If BTNRisk4.BackColor = Color.AliceBlue Then BTNRisk4.BackColor = Color.Yellow
            If BTNRisk5.BackColor = Color.AliceBlue Then BTNRisk5.BackColor = Color.Yellow
            If BTNRisk6.BackColor = Color.AliceBlue Then BTNRisk6.BackColor = Color.Yellow
            If BTNRisk7.BackColor = Color.AliceBlue Then BTNRisk7.BackColor = Color.Yellow
            If BTNRisk8.BackColor = Color.AliceBlue Then BTNRisk8.BackColor = Color.Yellow
            If BTNRisk9.BackColor = Color.AliceBlue Then BTNRisk9.BackColor = Color.Yellow
            If BTNRisk10.BackColor = Color.AliceBlue Then BTNRisk10.BackColor = Color.Yellow
            If BTNRisk11.BackColor = Color.AliceBlue Then BTNRisk11.BackColor = Color.Yellow
            If BTNRisk12.BackColor = Color.AliceBlue Then BTNRisk12.BackColor = Color.Yellow
            If BTNRisk13.BackColor = Color.AliceBlue Then BTNRisk13.BackColor = Color.Yellow
            If BTNRisk14.BackColor = Color.AliceBlue Then BTNRisk14.BackColor = Color.Yellow
            If BTNRisk15.BackColor = Color.AliceBlue Then BTNRisk15.BackColor = Color.Yellow
            If BTNRisk16.BackColor = Color.AliceBlue Then BTNRisk16.BackColor = Color.Yellow
            If BTNRisk17.BackColor = Color.AliceBlue Then BTNRisk17.BackColor = Color.Yellow
            If BTNRisk18.BackColor = Color.AliceBlue Then BTNRisk18.BackColor = Color.Yellow
            If BTNRisk19.BackColor = Color.AliceBlue Then BTNRisk19.BackColor = Color.Yellow
            If BTNRisk20.BackColor = Color.AliceBlue Then BTNRisk20.BackColor = Color.Yellow
            If BTNRisk21.BackColor = Color.AliceBlue Then BTNRisk21.BackColor = Color.Yellow
            If BTNRisk22.BackColor = Color.AliceBlue Then BTNRisk22.BackColor = Color.Yellow
            If BTNRisk23.BackColor = Color.AliceBlue Then BTNRisk23.BackColor = Color.Yellow
            If BTNRisk24.BackColor = Color.AliceBlue Then BTNRisk24.BackColor = Color.Yellow
        End If

        If RiskyTick = 0 Then
            If RiskyCase1Button.BackColor = Color.Yellow Then RiskyCase1Button.BackColor = Color.Transparent
            If RiskyCase2Button.BackColor = Color.Yellow Then RiskyCase2Button.BackColor = Color.Transparent
            If BTNRisk3.BackColor = Color.Yellow Then BTNRisk3.BackColor = Color.Transparent
            If BTNRisk4.BackColor = Color.Yellow Then BTNRisk4.BackColor = Color.Transparent
            If BTNRisk5.BackColor = Color.Yellow Then BTNRisk5.BackColor = Color.Transparent
            If BTNRisk6.BackColor = Color.Yellow Then BTNRisk6.BackColor = Color.Transparent
            If BTNRisk7.BackColor = Color.Yellow Then BTNRisk7.BackColor = Color.Transparent
            If BTNRisk8.BackColor = Color.Yellow Then BTNRisk8.BackColor = Color.Transparent
            If BTNRisk9.BackColor = Color.Yellow Then BTNRisk9.BackColor = Color.Transparent
            If BTNRisk10.BackColor = Color.Yellow Then BTNRisk10.BackColor = Color.Transparent
            If BTNRisk11.BackColor = Color.Yellow Then BTNRisk11.BackColor = Color.Transparent
            If BTNRisk12.BackColor = Color.Yellow Then BTNRisk12.BackColor = Color.Transparent
            If BTNRisk13.BackColor = Color.Yellow Then BTNRisk13.BackColor = Color.Transparent
            If BTNRisk14.BackColor = Color.Yellow Then BTNRisk14.BackColor = Color.Transparent
            If BTNRisk15.BackColor = Color.Yellow Then BTNRisk15.BackColor = Color.Transparent
            If BTNRisk16.BackColor = Color.Yellow Then BTNRisk16.BackColor = Color.Transparent
            If BTNRisk17.BackColor = Color.Yellow Then BTNRisk17.BackColor = Color.Transparent
            If BTNRisk18.BackColor = Color.Yellow Then BTNRisk18.BackColor = Color.Transparent
            If BTNRisk19.BackColor = Color.Yellow Then BTNRisk19.BackColor = Color.Transparent
            If BTNRisk20.BackColor = Color.Yellow Then BTNRisk20.BackColor = Color.Transparent
            If BTNRisk21.BackColor = Color.Yellow Then BTNRisk21.BackColor = Color.Transparent
            If BTNRisk22.BackColor = Color.Yellow Then BTNRisk22.BackColor = Color.Transparent
            If BTNRisk23.BackColor = Color.Yellow Then BTNRisk23.BackColor = Color.Transparent
            If BTNRisk24.BackColor = Color.Yellow Then BTNRisk24.BackColor = Color.Transparent
            TimerRiskyFlash.Stop()
            RiskyPickOffer = GetRiskyOffer(MainWindow.GetGameBoard())
            LBLRiskMaxPot.Text = Math.Ceiling(1000 / LowestRisk)
            LblRiskMinPot.Text = Math.Ceiling(1000 / HighestRisk)
        End If
    End Sub

    Private Sub BTNRiskIt_Click(sender As Object, e As EventArgs) Handles BTNRiskIt.Click
        MainWindow.chatBox.Text = "Risk it"
        MainWindow.SendButton.PerformClick()
        If BTNRiskIt.Text = "LAST CASE" Then
            BTNRiskIt.Visible = True
            BTNPickIt.Visible = False
        Else
            BTNRiskIt.Visible = False
            BTNPickIt.Visible = False
        End If

    End Sub

    Private Sub BTNPickIt_Click(sender As Object, e As EventArgs) Handles BTNPickIt.Click
        MainWindow.chatBox.Text = "Pick it"
        MainWindow.SendButton.PerformClick()
        If BTNPickIt.Text <> "MY CASE" Then
            BTNRiskIt.Visible = False
            BTNPickIt.Visible = True
        Else
            BTNRiskIt.Visible = False
            BTNPickIt.Visible = False
            EdgesOwed = RiskyPickOffer.Edges
            TokensPaid = RiskyPickOffer.Tokens
        End If
    End Sub

    Public Sub ClearCaseLabels()

        If RiskyChoices(RiskyPickCount - 1) = LBLRisk100.Text Then LBLRisk100.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk95.Text Then LBLRisk95.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk90.Text Then LBLRisk90.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk85.Text Then LBLRisk85.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk80.Text Then LBLRisk80.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk75.Text Then LBLRisk75.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk70.Text Then LBLRisk70.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk60.Text Then LBLRisk60.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk50.Text Then LBLRisk50.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk40.Text Then LBLRisk40.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk30.Text Then LBLRisk30.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk25.Text Then LBLRisk25.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk20.Text Then LBLRisk20.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk15.Text Then LBLRisk15.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk10.Text Then LBLRisk10.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk5.Text Then LBLRisk5.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk3.Text Then LBLRisk3.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk1.Text Then LBLRisk1.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk12.Text Then LBLRisk12.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk55.Text Then LBLRisk55.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk65.Text Then LBLRisk65.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk4.Text Then LBLRisk4.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk2.Text Then LBLRisk2.ForeColor = Color.DimGray
        If RiskyChoices(RiskyPickCount - 1) = LBLRisk7.Text Then LBLRisk7.ForeColor = Color.DimGray

    End Sub

    Public Sub HighlightCaseLabelsOffer()

        RiskyCase1Contents.ForeColor = Color.DimGray
        RiskyCase2Contents.ForeColor = Color.DimGray
        RiskyCase3Contents.ForeColor = Color.DimGray
        RiskyCase4Contents.ForeColor = Color.DimGray
        Risk5.ForeColor = Color.DimGray
        Risk6.ForeColor = Color.DimGray
        Risk7.ForeColor = Color.DimGray
        Risk8.ForeColor = Color.DimGray
        Risk9.ForeColor = Color.DimGray
        Risk10.ForeColor = Color.DimGray
        Risk11.ForeColor = Color.DimGray
        Risk12.ForeColor = Color.DimGray
        Risk13.ForeColor = Color.DimGray
        Risk14.ForeColor = Color.DimGray
        Risk15.ForeColor = Color.DimGray
        Risk16.ForeColor = Color.DimGray
        Risk17.ForeColor = Color.DimGray
        Risk18.ForeColor = Color.DimGray
        Risk19.ForeColor = Color.DimGray
        Risk20.ForeColor = Color.DimGray
        Risk21.ForeColor = Color.DimGray
        Risk22.ForeColor = Color.DimGray
        Risk23.ForeColor = Color.DimGray
        Risk24.ForeColor = Color.DimGray


    End Sub

    ''' <summary>
    ''' Just sets the foreground color to DarkGay in the boxes under the button
    ''' </summary>
    Public Sub ClearCaseLabelsOffer()
        RiskyCase1Contents.ForeColor = Color.DarkGray
        RiskyCase2Contents.ForeColor = Color.DarkGray
        RiskyCase3Contents.ForeColor = Color.DarkGray
        RiskyCase4Contents.ForeColor = Color.DarkGray
        Risk5.ForeColor = Color.DarkGray
        Risk6.ForeColor = Color.DarkGray
        Risk7.ForeColor = Color.DarkGray
        Risk8.ForeColor = Color.DarkGray
        Risk9.ForeColor = Color.DarkGray
        Risk10.ForeColor = Color.DarkGray
        Risk11.ForeColor = Color.DarkGray
        Risk12.ForeColor = Color.DarkGray
        Risk13.ForeColor = Color.DarkGray
        Risk14.ForeColor = Color.DarkGray
        Risk15.ForeColor = Color.DarkGray
        Risk16.ForeColor = Color.DarkGray
        Risk17.ForeColor = Color.DarkGray
        Risk18.ForeColor = Color.DarkGray
        Risk19.ForeColor = Color.DarkGray
        Risk20.ForeColor = Color.DarkGray
        Risk21.ForeColor = Color.DarkGray
        Risk22.ForeColor = Color.DarkGray
        Risk23.ForeColor = Color.DarkGray
        Risk24.ForeColor = Color.DarkGray
    End Sub

    Public Sub ClearAllCards()

        For Each tmp As PictureBox In New List(Of PictureBox) From
                {Slot1, Slot2, Slot3, SlotLeft1, SlotLeft2, SlotRight1, SlotRight2,
                BronzeP1, BronzeP2, BronzeP3, BronzeP4, BronzeP5, BronzeP6,
                SilverP1, SilverP2, SilverP3, SilverP4, SilverP5, SilverP6,
                GoldP1, GoldP2, GoldP3, GoldP3, GoldP4, GoldP5, GoldP6}
            If tmp.Image IsNot Nothing Then tmp.Image.Dispose()
            tmp.Image = Nothing
        Next

        Try
            GC.Collect()
        Catch
        End Try

    End Sub

    Public Sub CloseRiskyPick()

        ClearCaseLabelsOffer()

        RiskyCase1Button.Enabled = False
        RiskyCase2Button.Enabled = False
        BTNRisk3.Enabled = False
        BTNRisk4.Enabled = False
        BTNRisk5.Enabled = False
        BTNRisk6.Enabled = False
        BTNRisk7.Enabled = False
        BTNRisk8.Enabled = False
        BTNRisk9.Enabled = False
        BTNRisk10.Enabled = False
        BTNRisk11.Enabled = False
        BTNRisk12.Enabled = False
        BTNRisk13.Enabled = False
        BTNRisk14.Enabled = False
        BTNRisk15.Enabled = False
        BTNRisk16.Enabled = False
        BTNRisk17.Enabled = False
        BTNRisk18.Enabled = False
        BTNRisk19.Enabled = False
        BTNRisk20.Enabled = False
        BTNRisk21.Enabled = False
        BTNRisk22.Enabled = False
        BTNRisk23.Enabled = False
        BTNRisk24.Enabled = False

        RiskyCase1Button.Text = "1"
        RiskyCase2Button.Text = "2"
        BTNRisk3.Text = "3"
        BTNRisk4.Text = "4"
        BTNRisk5.Text = "5"
        BTNRisk6.Text = "6"
        BTNRisk7.Text = "7"
        BTNRisk8.Text = "8"
        BTNRisk9.Text = "9"
        BTNRisk10.Text = "10"
        BTNRisk11.Text = "11"
        BTNRisk12.Text = "12"
        BTNRisk13.Text = "13"
        BTNRisk14.Text = "14"
        BTNRisk15.Text = "15"
        BTNRisk16.Text = "16"
        BTNRisk17.Text = "17"
        BTNRisk18.Text = "18"
        BTNRisk19.Text = "19"
        BTNRisk20.Text = "20"
        BTNRisk21.Text = "21"
        BTNRisk22.Text = "22"
        BTNRisk23.Text = "23"
        BTNRisk24.Text = "24"

        LBLRisk100.ForeColor = Color.White
        LBLRisk95.ForeColor = Color.White
        LBLRisk90.ForeColor = Color.White
        LBLRisk85.ForeColor = Color.White
        LBLRisk80.ForeColor = Color.White
        LBLRisk75.ForeColor = Color.White
        LBLRisk70.ForeColor = Color.White
        LBLRisk65.ForeColor = Color.White
        LBLRisk60.ForeColor = Color.White
        LBLRisk55.ForeColor = Color.White
        LBLRisk50.ForeColor = Color.White
        LBLRisk40.ForeColor = Color.White
        LBLRisk30.ForeColor = Color.White
        LBLRisk25.ForeColor = Color.White
        LBLRisk20.ForeColor = Color.White
        LBLRisk15.ForeColor = Color.White
        LBLRisk12.ForeColor = Color.White
        LBLRisk10.ForeColor = Color.White
        LBLRisk7.ForeColor = Color.White
        LBLRisk5.ForeColor = Color.White
        LBLRisk4.ForeColor = Color.White
        LBLRisk3.ForeColor = Color.White
        LBLRisk2.ForeColor = Color.White
        LBLRisk1.ForeColor = Color.White

        RiskyPickChat.DocumentText = ""
        LBLRiskMaxPot.Text = "N/A"
        LblRiskMinPot.Text = "N/A"
        BTNRiskIt.Visible = False
        BTNPickIt.Visible = False
        BTNRiskIt.Text = "DECLINE OFFER"
        BTNPickIt.Text = "ACCEPT OFFER"

        MainWindow.ssh.RiskyDeal = False
        MainWindow.ssh.RiskyEdges = True

        Me.Close()
    End Sub

    'Public Sub PickedMyCase()

    'EdgesOwed = Val(RiskyCase)
    'TokensPaid = 1000 / EdgesOwed
    'TokensPaid = Math.Ceiling(TokensPaid)



    'End Sub

    'Public Sub PickedLastCase()



    'EdgesOwed = Val(RiskyCase)
    'TokensPaid = 1000 / EdgesOwed
    'TokensPaid = Math.Ceiling(TokensPaid)



    'End Sub

    ''' <summary>
    ''' Resets Risky Pick to a new Game
    ''' </summary>
    Public Sub SetupRiskyPick()
        SelectedCase1Label.Text = ""
        SelectedCase2Label.Text = ""
        SelectedCase3Label.Text = ""
        SelectedCase4Label.Text = ""
        SelectedCase5Label.Text = ""
        SelectedCase6Label.Text = ""
        RiskyRound = -1
        RiskyChoiceCount = 0
    End Sub

    ''' <summary>
    ''' This will Conditionally enable cases based on ifthe risk button has text or not
    ''' </summary>
    Public Sub EnableCases()
        If RiskyCase1Button.Text <> "" Then RiskyCase1Button.Enabled = True
        If RiskyCase2Button.Text <> "" Then RiskyCase2Button.Enabled = True
        If BTNRisk3.Text <> "" Then BTNRisk3.Enabled = True
        If BTNRisk4.Text <> "" Then BTNRisk4.Enabled = True
        If BTNRisk5.Text <> "" Then BTNRisk5.Enabled = True
        If BTNRisk6.Text <> "" Then BTNRisk6.Enabled = True
        If BTNRisk7.Text <> "" Then BTNRisk7.Enabled = True
        If BTNRisk8.Text <> "" Then BTNRisk8.Enabled = True
        If BTNRisk9.Text <> "" Then BTNRisk9.Enabled = True
        If BTNRisk10.Text <> "" Then BTNRisk10.Enabled = True

        If BTNRisk11.Text <> "" Then BTNRisk11.Enabled = True
        If BTNRisk12.Text <> "" Then BTNRisk12.Enabled = True
        If BTNRisk13.Text <> "" Then BTNRisk13.Enabled = True
        If BTNRisk14.Text <> "" Then BTNRisk14.Enabled = True
        If BTNRisk15.Text <> "" Then BTNRisk15.Enabled = True
        If BTNRisk16.Text <> "" Then BTNRisk16.Enabled = True
        If BTNRisk17.Text <> "" Then BTNRisk17.Enabled = True
        If BTNRisk18.Text <> "" Then BTNRisk18.Enabled = True
        If BTNRisk19.Text <> "" Then BTNRisk19.Enabled = True
        If BTNRisk20.Text <> "" Then BTNRisk20.Enabled = True

        If BTNRisk21.Text <> "" Then BTNRisk21.Enabled = True
        If BTNRisk22.Text <> "" Then BTNRisk22.Enabled = True
        If BTNRisk23.Text <> "" Then BTNRisk23.Enabled = True
        If BTNRisk24.Text <> "" Then BTNRisk24.Enabled = True
    End Sub

#Region "RiskyPickService"
    ''' <summary>
    ''' Start a new game of Risky Pick.
    ''' Risky pick is a game similar to Deal or no deal. 
    ''' </summary>
    ''' <param name="mainWindow"></param>
    ''' <returns></returns>
    Private Function StartRiskyPick(mainWindow As MainWindow) As Result
        DisableCases()
        Return mainWindow.SendCommand(Keyword.RiskyPickStart) _
            .OnSuccess(Sub()
                           SetupRiskyPick()

                           PlayRiskyPickButton.Text = ""
                           PlayRiskyPickButton.Enabled = False

                           If Directory.Exists(My.Settings.DomImageDir) AndAlso mainWindow.ssh.SlideshowLoaded Then
                               mainWindow.LoadDommeImageFolder()
                           End If
                       End Sub)
    End Function

    ''' <summary>
    ''' Update the UI based on the new gameboard
    ''' </summary>
    ''' <param name="newGameBoard">Which choice in the round is this</param>
    Public Sub UpdateUiFromBoard(newGameBoard As RiskyPickGameBoard)
        'PlayCardFlip()

        If newGameBoard.CurrentRound > 0 Then
            SelectedCase1Label.Text = If(newGameBoard.SelectedCases.Count >= 1, newGameBoard.SelectedCases(0).ToString(), String.Empty)
            SelectedCase2Label.Text = If(newGameBoard.SelectedCases.Count >= 2, newGameBoard.SelectedCases(1).ToString(), String.Empty)
            SelectedCase3Label.Text = If(newGameBoard.SelectedCases.Count >= 3, newGameBoard.SelectedCases(2).ToString(), String.Empty)
            SelectedCase4Label.Text = If(newGameBoard.SelectedCases.Count >= 4, newGameBoard.SelectedCases(3).ToString(), String.Empty)
            SelectedCase5Label.Text = If(newGameBoard.SelectedCases.Count >= 5, newGameBoard.SelectedCases(4).ToString(), String.Empty)
            SelectedCase6Label.Text = If(newGameBoard.SelectedCases.Count >= 6, newGameBoard.SelectedCases(5).ToString(), String.Empty)
        End If

        For Each caseNumber In newGameBoard.Cases.Keys
            Dim caseButton As Button = GetCaseButton(caseNumber)
            caseButton.Enabled = Not ((newGameBoard.PlayersCase?.CaseNumber).GetValueOrDefault() = caseNumber) AndAlso Not (newGameBoard.Cases(caseNumber).IsOpened) AndAlso Not (newGameBoard.SelectedCases.Contains(caseNumber))

            Dim caseLabel As Label = GetCaseLabel(caseNumber)
            caseLabel.Text = If(newGameBoard.Cases(caseNumber).IsOpened, newGameBoard.Cases(caseNumber).ToString(), String.Empty)
        Next

        PlayRiskyPickButton.Text = newGameBoard.PlayersCase?.CaseNumber.ToString()
        If (newGameBoard.CurrentRound = 0 AndAlso newGameBoard.PlayersCase IsNot Nothing) OrElse newGameBoard.CasesToPick(newGameBoard.CurrentRound) = newGameBoard.SelectedCases.Count Then
            DisableCases()
            newGameBoard.CurrentRound += 1
        End If
    End Sub

    ''' <summary>
    ''' Go find the button for a given case
    ''' </summary>
    ''' <param name="caseNumber"></param>
    ''' <returns></returns>
    Private Function GetCaseButton(caseNumber As Integer) As Button
        If caseNumber = 1 Then
            Return RiskyCase1Button
        ElseIf caseNumber = 2 Then
            Return RiskyCase2Button
        ElseIf caseNumber = 3 Then
            Return BTNRisk3
        ElseIf caseNumber = 4 Then
            Return BTNRisk4
        ElseIf caseNumber = 5 Then
            Return BTNRisk5
        ElseIf caseNumber = 6 Then
            Return BTNRisk6
        ElseIf caseNumber = 7 Then
            Return BTNRisk7
        ElseIf caseNumber = 8 Then
            Return BTNRisk8
        ElseIf caseNumber = 9 Then
            Return BTNRisk9
        ElseIf caseNumber = 10 Then
            Return BTNRisk10
        ElseIf caseNumber = 11 Then
            Return BTNRisk11
        ElseIf caseNumber = 12 Then
            Return BTNRisk12
        ElseIf caseNumber = 13 Then
            Return BTNRisk13
        ElseIf caseNumber = 14 Then
            Return BTNRisk14
        ElseIf caseNumber = 15 Then
            Return BTNRisk15
        ElseIf caseNumber = 16 Then
            Return BTNRisk16
        ElseIf caseNumber = 17 Then
            Return BTNRisk17
        ElseIf caseNumber = 18 Then
            Return BTNRisk18
        ElseIf caseNumber = 19 Then
            Return BTNRisk19
        ElseIf caseNumber = 20 Then
            Return BTNRisk20
        ElseIf caseNumber = 21 Then
            Return BTNRisk21
        ElseIf caseNumber = 22 Then
            Return BTNRisk22
        ElseIf caseNumber = 23 Then
            Return BTNRisk23
        ElseIf caseNumber = 24 Then
            Return BTNRisk24
        End If
        Throw New ArgumentOutOfRangeException()
    End Function

    ''' <summary>
    ''' Go find the contents label for a given case
    ''' </summary>
    ''' <param name="caseNumber"></param>
    ''' <returns></returns>
    Private Function GetCaseLabel(caseNumber As Integer) As Label
        If caseNumber = 1 Then
            Return RiskyCase1Contents
        ElseIf caseNumber = 2 Then
            Return RiskyCase2Contents
        ElseIf caseNumber = 3 Then
            Return RiskyCase3Contents
        ElseIf caseNumber = 4 Then
            Return RiskyCase4Contents
        ElseIf caseNumber = 5 Then
            Return Risk5
        ElseIf caseNumber = 6 Then
            Return Risk6
        ElseIf caseNumber = 7 Then
            Return Risk7
        ElseIf caseNumber = 8 Then
            Return Risk8
        ElseIf caseNumber = 9 Then
            Return Risk9
        ElseIf caseNumber = 10 Then
            Return Risk10
        ElseIf caseNumber = 11 Then
            Return Risk11
        ElseIf caseNumber = 12 Then
            Return Risk12
        ElseIf caseNumber = 13 Then
            Return Risk13
        ElseIf caseNumber = 14 Then
            Return Risk14
        ElseIf caseNumber = 15 Then
            Return Risk15
        ElseIf caseNumber = 16 Then
            Return Risk16
        ElseIf caseNumber = 17 Then
            Return Risk17
        ElseIf caseNumber = 18 Then
            Return Risk18
        ElseIf caseNumber = 19 Then
            Return Risk19
        ElseIf caseNumber = 20 Then
            Return Risk20
        ElseIf caseNumber = 21 Then
            Return Risk21
        ElseIf caseNumber = 22 Then
            Return Risk22
        ElseIf caseNumber = 23 Then
            Return Risk23
        ElseIf caseNumber = 24 Then
            Return Risk24
        End If
        Throw New ArgumentOutOfRangeException()
    End Function

    Private Function GetCaseNumber(button As Button) As Integer
        For i As Integer = 1 To 24
            If GetCaseButton(i) Is button Then
                Return i
            End If
        Next
        Throw New ArgumentOutOfRangeException()
    End Function

    Public Function GetRiskyOffer(gameBoard As RiskyPickGameBoard) As RiskyPickOffer

        Dim offer As RiskyPickOffer = New RiskyPickOffer()
        Dim edgeCount = 0
        Dim tokenCount = 0
        Dim offerAverage = 0
        Dim tokenAverage = 0

        ' This for each loops replaces a mess of if statements checking this
        'If LBLRisk1.ForeColor = Color.White Then
        For Each riskyCase As GameCase In gameBoard.Cases.Values
            If Not riskyCase.IsOpened Then
                edgeCount += riskyCase.Edges
                tokenCount += (1000 / riskyCase.Edges)
                offerAverage += 1
            End If
        Next

        HighestRisk = gameBoard.Cases.Values.Where(Function(c) Not c.IsOpened).Max(Function(c) c.Edges)
        LowestRisk = gameBoard.Cases.Values.Where(Function(c) Not c.IsOpened).Min(Function(c) c.Edges)

        offer.Tokens = Math.Ceiling(tokenCount / offerAverage)
        offer.Edges = Math.Ceiling(edgeCount / offerAverage)

        Return offer

    End Function

    Public Sub CheckRiskyPick()
        CheckRiskyPick(MainWindow.GetGameBoard())
    End Sub

    Private Sub CheckRiskyPick(gameBoard As RiskyPickGameBoard)
        If gameBoard.PlayersCase Is Nothing Then Return

        If gameBoard.CurrentRound = 0 Then
            Dim caseButton As Button = GetCaseButton(gameBoard.PlayersCase.CaseNumber)
            If caseButton.BackColor = mySelectedCase Then
                caseButton.BackColor = Color.Transparent
                caseButton.Text = String.Empty
            End If
            Return
        End If

        SelectedCase1Label.Text = If(gameBoard.SelectedCases.Count >= 1, gameBoard.SelectedCases(0).ToString(), String.Empty)
        SelectedCase2Label.Text = If(gameBoard.SelectedCases.Count >= 2, gameBoard.SelectedCases(1).ToString(), String.Empty)
        SelectedCase3Label.Text = If(gameBoard.SelectedCases.Count >= 3, gameBoard.SelectedCases(2).ToString(), String.Empty)
        SelectedCase4Label.Text = If(gameBoard.SelectedCases.Count >= 4, gameBoard.SelectedCases(3).ToString(), String.Empty)
        SelectedCase5Label.Text = If(gameBoard.SelectedCases.Count >= 5, gameBoard.SelectedCases(4).ToString(), String.Empty)
        SelectedCase6Label.Text = If(gameBoard.SelectedCases.Count >= 6, gameBoard.SelectedCases(5).ToString(), String.Empty)

        For caseNumber As Integer = 1 To 24
            GetCaseButton(caseNumber).ForeColor = If(gameBoard.Cases(caseNumber).IsOpened, Color.Black, Color.DarkGray)
        Next
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase1Contents.Text Then RiskyCase1Button.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase2Contents.Text Then RiskyCase2Button.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase3Contents.Text Then BTNRisk3.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase4Contents.Text Then BTNRisk4.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk5.Text Then BTNRisk5.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk6.Text Then BTNRisk6.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk7.Text Then BTNRisk7.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk8.Text Then BTNRisk8.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk9.Text Then BTNRisk9.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk10.Text Then BTNRisk10.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk11.Text Then BTNRisk11.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk12.Text Then BTNRisk12.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk13.Text Then BTNRisk13.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk14.Text Then BTNRisk14.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk15.Text Then BTNRisk15.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk16.Text Then BTNRisk16.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk17.Text Then BTNRisk17.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk18.Text Then BTNRisk18.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk19.Text Then BTNRisk19.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk20.Text Then BTNRisk20.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk21.Text Then BTNRisk21.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk22.Text Then BTNRisk22.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk23.Text Then BTNRisk23.BackColor = Color.Yellow
        'If RiskyChoices(RiskyPickCount - 1) = Risk24.Text Then BTNRisk24.BackColor = Color.Yellow

        'RiskyTick = 3
        'TimerRiskyFlash.Start()

        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase1Contents.Text Then RiskyCase1Button.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase2Contents.Text Then RiskyCase2Button.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase3Contents.Text Then BTNRisk3.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = RiskyCase4Contents.Text Then BTNRisk4.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk5.Text Then BTNRisk5.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk6.Text Then BTNRisk6.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk7.Text Then BTNRisk7.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk8.Text Then BTNRisk8.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk9.Text Then BTNRisk9.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk10.Text Then BTNRisk10.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk11.Text Then BTNRisk11.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk12.Text Then BTNRisk12.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk13.Text Then BTNRisk13.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk14.Text Then BTNRisk14.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk15.Text Then BTNRisk15.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk16.Text Then BTNRisk16.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk17.Text Then BTNRisk17.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk18.Text Then BTNRisk18.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk19.Text Then BTNRisk19.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk20.Text Then BTNRisk20.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk21.Text Then BTNRisk21.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk22.Text Then BTNRisk22.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk23.Text Then BTNRisk23.Text = ""
        'If RiskyChoices(RiskyPickCount - 1) = Risk24.Text Then BTNRisk24.Text = ""

        'ClearCaseLabels()

        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk100.Text Then RiskyResponse = "#RP_100Edge"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk95.Text Then RiskyResponse = "#RP_FirstRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk90.Text Then RiskyResponse = "#RP_FirstRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk85.Text Then RiskyResponse = "#RP_FirstRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk80.Text Then RiskyResponse = "#RP_FirstRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk75.Text Then RiskyResponse = "#RP_FirstRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk70.Text Then RiskyResponse = "#RP_SecondRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk60.Text Then RiskyResponse = "#RP_SecondRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk50.Text Then RiskyResponse = "#RP_SecondRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk40.Text Then RiskyResponse = "#RP_SecondRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk30.Text Then RiskyResponse = "#RP_ThirdRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk25.Text Then RiskyResponse = "#RP_ThirdRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk20.Text Then RiskyResponse = "#RP_ThirdRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk15.Text Then RiskyResponse = "#RP_ThirdRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk10.Text Then RiskyResponse = "#RP_ThirdRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk5.Text Then RiskyResponse = "#RP_FourthRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk3.Text Then RiskyResponse = "#RP_FourthRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk1.Text Then RiskyResponse = "#RP_1Edge"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk12.Text Then RiskyResponse = "#RP_ThirdRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk55.Text Then RiskyResponse = "#RP_SecondRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk65.Text Then RiskyResponse = "#RP_SecondRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk4.Text Then RiskyResponse = "#RP_FourthRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk2.Text Then RiskyResponse = "#RP_FourthRow"
        'If RiskyChoices(RiskyPickCount - 1) = LBLRisk7.Text Then RiskyResponse = "#RP_FourthRow"


        If gameBoard.CasesToPick(gameBoard.CurrentRound) = gameBoard.SelectedCases.Count Then
            RiskyPickOffer = GetRiskyOffer(gameBoard)
        End If

    End Sub
#End Region

#Region "Media related"
    ''' <summary>
    ''' Just play the sound of a card flipping if it is available
    ''' </summary>
    Public Sub PlayCardFlip()
        If FrmSettings.CBGameSounds.Checked = True And File.Exists(Application.StartupPath & "\Audio\System\CardFlip.wav") Then
            GameWMP.settings.setMode("loop", False)
            GameWMP.settings.volume = 20
            GameWMP.URL = Application.StartupPath & "\Audio\System\CardFlip.wav"
        End If
    End Sub

    Friend Sub UpdateRiskyChat(messageString As String, gameBoard As RiskyPickGameBoard)
        RiskyPickChat.DocumentText = messageString
        UpdateUiFromBoard(gameBoard)
    End Sub
#End Region
End Class