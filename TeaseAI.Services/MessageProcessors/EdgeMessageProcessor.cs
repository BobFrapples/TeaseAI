using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.MessageProcessors
{
    public class EdgeMessageProcessor : IMessageProcessor
    {

        public EdgeMessageProcessor(ISystemVocabularyAccessor systemVocabularyAccessor
            , IVariableAccessor variableAccessor
            , IRandomNumberService randomPercentService)
        {
            _systemVocabularyAccessor = systemVocabularyAccessor;
            _variableAccessor = variableAccessor;
            _randomPercentService = randomPercentService;
        }

        public event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        public bool IsRelevant(Session session, ChatMessage chatMessage)
        {
            var data = _systemVocabularyAccessor.GetData(session, "EdgeKEY")
                .OnSuccess(list => list.Any(line => line.ToLower() == Normalize(chatMessage.Message)));

            return data.Value;
        }

        public Result<MessageProcessedResult> ProcessMessage(Session session, ChatMessage chatMessage)
        {
            var workingSession = session.Clone();
            if (session.Sub.InChastity)
                throw new NotImplementedException();

            var addEdge = _variableAccessor.AddToVariable(session.Domme, SystemVariable.EdgeTotal, 1);
            if (addEdge.IsFailure)
                return Result.Fail<MessageProcessedResult>( addEdge.Error);

            if (workingSession.Sub.IsHoldingTheEdge)
                return Result.Ok(new MessageProcessedResult { Session = session, MessageBack = "#YoureAlreadySupposedToBeClose" });

            #region needs figured out
            //            If ssh.EdgeFound = True And My.Settings.Chastity = False Then
            //            SetVariable("SYS_EdgeTotal", Val(GetVariable("SYS_EdgeTotal") + 1))

            //            If ssh.TauntEdging = True And ssh.SubEdging = False And ssh.ShowModule = False Then
            //                ssh.DomChat = "#SYS_TauntEdgingAsked"
            //                TypingDelay()

            //                ' Recalculate TantEdging-Chance
            //                If ssh.randomizer.Next(1, 101) <= FrmSettings.NBTauntEdging.Value Then
            //                    ssh.TauntEdging = False
            //                End If
            //                Exit Sub
            //            End If
            //            If ssh.EdgeVideo = True Then
            //                ssh.SessionEdges += 1
            //                ssh.EdgeVideo = False
            //                ssh.TeaseVideo = False
            //                VideoTimer.Stop()
            //                DomWMP.Visible = False
            //                DomWMP.Ctlcontrols.stop()
            //                mainPictureBox.Visible = True
            //                ssh.FileGoto = ssh.EdgeGotoLine
            //                ssh.SkipGotoLine = True
            //                GetGoto()
            //                Return
            //            End If
            //            If ssh.EdgeGoto = True Then
            //                ssh.SessionEdges += 1
            //                ssh.EdgeGoto = False
            //                ssh.FileGoto = ssh.EdgeGotoLine
            //                ssh.SkipGotoLine = True
            //                GetGoto()
            //                Return
            //            End If
            //            If ssh.EdgeMessage = True Then
            //                ssh.SessionEdges += 1
            //                ssh.EdgeMessage = False
            //                ssh.ChatString = ssh.EdgeMessageText
            //                GoTo DebugAwareness
            //            End If
            //            If ssh.RLGLGame = True Then
            //                Debug.Print("EdgeFOund = RLGL")
            //                ssh.DomChat = "#TryToHoldIt"
            //                TypingDelay()
            //                Return
            //            End If
            #endregion

            #region avoid the edge
            //            If ssh.AvoidTheEdgeStroking = True Then
            //                Debug.Print("EdgeFOund = ATE")
            //                AvoidTheEdgeTaunts.Stop()
            //                ssh.AvoidTheEdgeStroking = False
            //                ssh.VideoTease = False
            //                Dim ATEList As New List(Of String)
            //                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Video\Avoid The Edge\Scripts\", FileIO.SearchOption.SearchTopLevelOnly, " *.txt")
            //                    ATEList.Add(foundFile)
            //                Next
            //                If ATEList.Count < 1 Then
            //                    MessageBox.Show(Me, "No Avoid The Edge scripts were found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            //                    Return
            //                End If
            //                DomWMP.Ctlcontrols.pause()
            //                ssh.StrokeTauntVal = -1
            //                ssh.FileText = ATEList(ssh.randomizer.Next(0, ATEList.Count))
            //                ssh.ScriptTick = 2
            //                ScriptTimer.Start()
            //                Return
            //            End If
            #endregion

            if (workingSession.Sub.IsEdging)
            {
                var message = "#StopStrokingEdge";
                if (workingSession.Domme.EdgesRequired > workingSession.Sub.EdgeCount)
                {
                    workingSession.Sub.EdgeCount++;
                    // If ssh.Contact1Edge = True Then ssh.DomChat = "@Contact1 #SYS_MultipleEdgesStop"
                    // If ssh.Contact2Edge = True Then ssh.DomChat = "@Contact2 #SYS_MultipleEdgesStop"
                    // If ssh.Contact3Edge = True Then ssh.DomChat = "@Contact3 #SYS_MultipleEdgesStop"
                    // TODO: work in average edge count per session, average edge time.
                    message = "#SYS_MultipleEdgesStop";
                }

                var holdEdgeChance = 20m;
                if (workingSession.Domme.DomLevel == DomLevel.Gentle)
                    holdEdgeChance = 20m;
                else if (workingSession.Domme.DomLevel == DomLevel.Lenient)
                    holdEdgeChance = 25m;
                else if (workingSession.Domme.DomLevel == DomLevel.Tease)
                    holdEdgeChance = 30m;
                else if (workingSession.Domme.DomLevel == DomLevel.Rough)
                    holdEdgeChance = 40m;
                else if (workingSession.Domme.DomLevel == DomLevel.Sadistic)
                    holdEdgeChance = 50m;

                workingSession.Sub.HoldEdgeSeconds = 0; //Math.Max(_randomPercentService.RollPercent() - holdEdgeChance, 0) * workingSession.Domme.DomLevel;
                if ( workingSession.Sub.IsHoldingTheEdge)
                {
                    message = "#HoldTheEdge";
                    //                    If ssh.Contact1Edge = True Then
                    //                        ssh.DomChat = "@Contact1 #HoldTheEdge"
                    //                        ' Github Patch Contact1Edge = False
                    //                    End If
                    //                    If ssh.Contact2Edge = True Then
                    //                        ssh.DomChat = "@Contact2 #HoldTheEdge"
                    //                        ' github Patch Contact2Edge = False
                    //                    End If
                    //                    If ssh.Contact3Edge = True Then
                    //                        ssh.DomChat = "@Contact3 #HoldTheEdge"
                    //                        ' github patch Contact3Edge = False
                    //                    End If
                    // figure out  these
                }
                workingSession.Sub.IsEdging = workingSession.Sub.IsHoldingTheEdge;
                workingSession.Sub.IsStroking = workingSession.Sub.IsHoldingTheEdge;
                if (!workingSession.Sub.IsEdging)
                {
                    workingSession.Scripts.Pop();
                    workingSession.CurrentScript.LineNumber++;
                }

                return Result.Ok(new MessageProcessedResult { Session = workingSession, MessageBack = message });
            }

            #region So much uknown
            //            If ssh.SubEdging = True Then
            //                If HoldEdgeInt < ssh.HoldEdgeChance Then
            //                Else

            //                    If ssh.EdgeToRuin = True Or ssh.OrgasmRuined = True Then
            //                        ssh.LastOrgasmType = "RUINED"
            //                        ssh.OrgasmRuined = False
            //                        GoTo RuinedOrgasm
            //                    End If

            //                    If ssh.OrgasmAllowed = True Then
            //                        ssh.LastOrgasmType = "ALLOWED"
            //                        ssh.OrgasmAllowed = False
            //                        GoTo AllowedOrgasm
            //                    End If


            //                    Debug.Print("Ruined Orgasm skipped")

            //                    If ssh.OrgasmDenied = True Then

            //                        ssh.LastOrgasmType = "DENIED"

            //                        If FrmSettings.CBDomDenialEnds.Checked = False And ssh.TeaseTick < 1 Then

            //                            Dim RepeatChance As Integer = ssh.randomizer.Next(0, 101)

            //                            If RepeatChance < 10 * FrmSettings.DominationLevel.Value Then
            //                                ssh.SubEdging = False
            //                                ssh.SubStroking = False
            //                                EdgeTauntTimer.Stop()

            //                                Dim RepeatList As New List(Of String)

            //                                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Interrupt\Denial Continue\", FileIO.SearchOption.SearchTopLevelOnly, " *.txt")
            //                                    RepeatList.Add(foundFile)
            //                                Next

            //                                If RepeatList.Count < 1 Then GoTo NoRepeatFiles


            //                                If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
            //                                    If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60
            //                                    If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60
            //                                    If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60
            //                                    If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60
            //                                    If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60
            //                                Else
            //                                    ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
            //                                End If
            //                                TeaseTimer.Start()

            //                                ssh.OrgasmYesNo = False

            //                                'Github Patch
            //                                ssh.YesOrNo = False

            //                                'ShowModule = True
            //                                ssh.StrokeTauntVal = -1
            //                                ssh.FileText = RepeatList(ssh.randomizer.Next(0, RepeatList.Count))
            //                                ssh.ScriptTick = 2
            //                                ScriptTimer.Start()
            //                                ssh.OrgasmDenied = False
            //                                ssh.OrgasmYesNo = False
            //                                ssh.EndTease = False
            //                                Return
            //                            End If

            //                        End If


            //                    End If

            //NoRepeatFiles:

            //                    ssh.DomTypeCheck = True
            //                    ssh.OrgasmYesNo = False
            //                    ssh.SubEdging = False
            //                    ssh.SubStroking = False
            //                    EdgeTauntTimer.Stop()
            //                    ssh.DomChat = "#StopStrokingEdge"
            //                    If ssh.Contact1Edge = True Then
            //                        ssh.DomChat = "@Contact1 #StopStrokingEdge"
            //                        ssh.Contact1Edge = False
            //                    End If
            //                    If ssh.Contact2Edge = True Then
            //                        ssh.DomChat = "@Contact2 #StopStrokingEdge"
            //                        ssh.Contact2Edge = False
            //                    End If
            //                    If ssh.Contact3Edge = True Then
            //                        ssh.DomChat = "@Contact3 #StopStrokingEdge"
            //                        ssh.Contact3Edge = False
            //                    End If
            //                    TypingDelay()
            //                    Return

            //                End If

            //RuinedOrgasm:

            //                My.Settings.LastRuined = FormatDateTime(Now, DateFormat.ShortDate)
            //                FrmSettings.LBLLastRuined.Text = My.Settings.LastRuined

            //                If FrmSettings.CBDomOrgasmEnds.Checked = False And ssh.OrgasmRuined = True And ssh.TeaseTick < 1 Then

            //                    Dim RepeatChance As Integer = ssh.randomizer.Next(0, 101)

            //                    If RepeatChance < 8 * FrmSettings.DominationLevel.Value Then

            //                        ssh.SubEdging = False
            //                        ssh.SubStroking = False
            //                        ssh.EdgeToRuin = False
            //                        ssh.EdgeToRuinSecret = True
            //                        EdgeTauntTimer.Stop()

            //                        Dim RepeatList As New List(Of String)

            //                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Interrupt\Ruin Continue\", FileIO.SearchOption.SearchTopLevelOnly, " *.txt")
            //                            RepeatList.Add(foundFile)
            //                        Next

            //                        If RepeatList.Count < 1 Then GoTo NoRepeatRFiles


            //                        If FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
            //                            If FrmSettings.DominationLevel.Value = 1 Then ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60
            //                            If FrmSettings.DominationLevel.Value = 2 Then ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60
            //                            If FrmSettings.DominationLevel.Value = 3 Then ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60
            //                            If FrmSettings.DominationLevel.Value = 4 Then ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60
            //                            If FrmSettings.DominationLevel.Value = 5 Then ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60
            //                        Else
            //                            ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
            //                        End If
            //                        TeaseTimer.Start()

            //                        ssh.OrgasmYesNo = False

            //                        'Github Patch
            //                        ssh.YesOrNo = False

            //                        'ShowModule = True
            //                        ssh.StrokeTauntVal = -1
            //                        ssh.FileText = RepeatList(ssh.randomizer.Next(0, RepeatList.Count))
            //                        ssh.ScriptTick = 2
            //                        ScriptTimer.Start()
            //                        ssh.OrgasmRuined = False
            //                        ssh.OrgasmYesNo = False
            //                        ssh.EndTease = False
            //                        Return
            //                    End If

            //                End If



            //NoRepeatRFiles:


            //                ssh.DomTypeCheck = True
            //                ssh.SubEdging = False
            //                ssh.SubStroking = False
            //                ssh.EdgeToRuin = False
            //                ssh.EdgeToRuinSecret = True
            //                EdgeTauntTimer.Stop()
            //                ssh.OrgasmYesNo = False
            //                ssh.DomChat = "#RuinYourOrgasm"
            //                If ssh.Contact1Edge = True Then
            //                    ssh.DomChat = "@Contact1 #RuinYourOrgasm"
            //                    ssh.Contact1Edge = False
            //                End If
            //                If ssh.Contact2Edge = True Then
            //                    ssh.DomChat = "@Contact2 #RuinYourOrgasm"
            //                    ssh.Contact2Edge = False
            //                End If
            //                If ssh.Contact3Edge = True Then
            //                    ssh.DomChat = "@Contact3 #RuinYourOrgasm"
            //                    ssh.Contact3Edge = False
            //                End If
            //                TypingDelay()
            //                Return

            //AllowedOrgasm:

            //                If My.Settings.OrgasmsLocked = True Then

            //                    If My.Settings.OrgasmsRemaining < 1 Then

            //                        Dim NoCumList As New List(Of String)

            //                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Interrupt\Out of Orgasms\", FileIO.SearchOption.SearchTopLevelOnly, " *.txt")
            //                            NoCumList.Add(foundFile)
            //                        Next

            //                        If NoCumList.Count < 1 Then GoTo NoNoCumFiles


            //                        ssh.SubEdging = False
            //                        ssh.SubStroking = False
            //                        EdgeTauntTimer.Stop()
            //                        ssh.OrgasmYesNo = False

            //                        'Github Patch
            //                        ssh.YesOrNo = False

            //                        'ShowModule = True
            //                        ssh.StrokeTauntVal = -1
            //                        ssh.FileText = NoCumList(ssh.randomizer.Next(0, NoCumList.Count))
            //                        ssh.ScriptTick = 2
            //                        ScriptTimer.Start()
            //                        Return
            //                    End If


            //                    My.Settings.OrgasmsRemaining -= 1


            //                End If

            //NoNoCumFiles:

            //                My.Settings.LastOrgasm = FormatDateTime(Now, DateFormat.ShortDate)
            //                FrmSettings.LBLLastOrgasm.Text = My.Settings.LastOrgasm

            //                If FrmSettings.CBDomOrgasmEnds.Checked = False And ssh.TeaseTick < 1 Then

            //                    Dim RepeatChance As Integer = ssh.randomizer.Next(0, 101)

            //                    If RepeatChance < 4 * FrmSettings.DominationLevel.Value Then
            //                        ssh.SubEdging = False
            //                        ssh.SubStroking = False
            //                        EdgeTauntTimer.Stop()

            //                        Dim RepeatList As New List(Of String)

            //                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\Interrupt\Orgasm Continue\", FileIO.SearchOption.SearchTopLevelOnly, " *.txt")
            //                            RepeatList.Add(foundFile)
            //                        Next

            //                        If RepeatList.Count < 1 Then GoTo NoRepeatOFiles


            //                        If Not FrmSettings.TeaseLengthDommeDetermined.Checked = True Then
            //                            ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60)
            //                            TeaseTimer.Start()
            //                        End If

            //                        ssh.OrgasmYesNo = False

            //                        'Github Patch
            //                        ssh.YesOrNo = False

            //                        'ShowModule = True
            //                        ssh.StrokeTauntVal = -1
            //                        ssh.FileText = RepeatList(ssh.randomizer.Next(0, RepeatList.Count))
            //                        ssh.ScriptTick = 2
            //                        ScriptTimer.Start()
            //                        ssh.OrgasmAllowed = False
            //                        ssh.OrgasmYesNo = False
            //                        ssh.EndTease = False
            //                        Return
            //                    End If

            //                End If



            //NoRepeatOFiles:
            //                ssh.DomTypeCheck = True
            //                ssh.SubEdging = False
            //                ssh.SubStroking = False
            //                'OrgasmAllowed = False
            //                EdgeTauntTimer.Stop()
            //                ssh.OrgasmYesNo = False
            //                ssh.DomChat = "#CumForMe"
            //                If ssh.Contact1Edge = True Then
            //                    ssh.DomChat = "@Contact1 #CumForMe"
            //                    ssh.Contact1Edge = False
            //                End If
            //                If ssh.Contact2Edge = True Then
            //                    ssh.DomChat = "@Contact2 #CumForMe"
            //                    ssh.Contact2Edge = False
            //                End If
            //                If ssh.Contact3Edge = True Then
            //                    ssh.DomChat = "@Contact3 #CumForMe"
            //                    ssh.Contact3Edge = False
            //                End If
            //                TypingDelay()
            //                Return


            //            End If



            //            If ssh.SubStroking = True Then

            //                Dim TauntStop As Integer = ssh.randomizer.Next(1, 101)

            //                If TauntStop <= FrmSettings.NBTauntEdging.Value Then

            //                    ssh.FirstRound = False
            //                    'ShowModule = True
            //                    StrokeTauntTimer.Stop()
            //                    StrokeTimer.Stop()


            //                    If ssh.BookmarkModule = True Then
            //                        ssh.DomTypeCheck = True
            //                        ssh.SubEdging = False
            //                        ssh.SubStroking = False
            //                        ssh.DomChat = "#StopStrokingEdge"
            //                        If ssh.Contact1Edge = True Then
            //                            ssh.DomChat = "@Contact1 #StopStrokingEdge"
            //                            ssh.Contact1Edge = False
            //                        End If
            //                        If ssh.Contact2Edge = True Then
            //                            ssh.DomChat = "@Contact2 #StopStrokingEdge"
            //                            ssh.Contact2Edge = False
            //                        End If
            //                        If ssh.Contact3Edge = True Then
            //                            ssh.DomChat = "@Contact3 #StopStrokingEdge"
            //                            ssh.Contact3Edge = False
            //                        End If
            //                        TypingDelay()

            //                        Do
            //                            Application.DoEvents()
            //                        Loop Until ssh.DomTypeCheck = False

            //                        ssh.BookmarkModule = False
            //                        ssh.FileText = ssh.BookmarkModuleFile
            //                        ssh.StrokeTauntVal = ssh.BookmarkModuleLine
            //                        RunFileText()
            //                        Return
            //                    End If

            //                    RunModuleScript(True)

            //                Else

            //                    ssh.TauntEdging = True
            //                    ssh.DomChat = "#SYS_TauntEdging"
            //                    TypingDelay()

            //                End If
            //            End If
            //            Return
            //        End If

            //        If ssh.EdgeFound = True And My.Settings.Chastity = True Then
            //            ssh.EdgeFound = False
            //            ssh.EdgeNOT = True
            //        End If
            #endregion

            workingSession.Sub.IsStroking = false;
            workingSession.Sub.HoldEdgeSeconds = 0;
                return Result.Ok(new MessageProcessedResult { Session = workingSession, MessageBack = "I think this is wrong" });
        }

        private void OnMessageProcessed(Session session, Response response)
        {
            MessageProcessed?.Invoke(this, new MessageProcessedEventArgs()
            {
                Session = session,
                Parameter = response,
            });
        }

        private string Normalize(string message)
        {
            return message
                .Replace("'", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("!", "")
                .Replace("  ", " ");
        }

        private readonly ISystemVocabularyAccessor _systemVocabularyAccessor;
        private readonly IVariableAccessor _variableAccessor;
        private readonly IRandomNumberService _randomPercentService;
    }
}
