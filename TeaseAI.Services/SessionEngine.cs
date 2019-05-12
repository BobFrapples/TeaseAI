using System;
using System.Linq;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Common.Interfaces.Timers;
using TeaseAI.Services.CommandProcessor;
using TeaseAI.Services.MessageProcessors;
using TeaseAI.Common.Data;

namespace TeaseAI.Services
{
    /// <summary>
    /// Main engine of the program. 
    /// </summary>
    public class SessionEngine
    {
        //TODO: Setup filtering to ignore lines like @Crazy, @SelfYoung, etc.
        public Session Session { get; set; }
        private object _sessionLock = new object();

        /// <summary>
        /// Index command processor by keyword it handles. useful for binding events
        /// </summary>
        public Dictionary<string, ICommandProcessor> CommandProcessors { get; private set; }
        public Dictionary<MessageProcessor, IMessageProcessor> MessageProcessors { get; private set; }

        public SessionEngine(ISettingsAccessor settingsAccessor
            , IStringService stringService
            , IScriptAccessor scriptAccessor
            , ITimerFactory timerFactory
            , IFlagAccessor flagAccessor
            , IImageAccessor imageAccessor
            , IVideoAccessor videoAccessor
            , IVariableAccessor variableAccessor
            , ITauntAccessor tauntAccessor
            , ISystemVocabularyAccessor systemVocabularyAccessor)
        {
            CommandProcessors = CreateCommandProcessors(scriptAccessor, flagAccessor, new LineService(), imageAccessor, videoAccessor, variableAccessor, tauntAccessor);

            CommandProcessors[Keyword.StartStroking].CommandProcessed += StartStrokingCommandProcessed;
            CommandProcessors[Keyword.Edge].CommandProcessed += EdgeCommandProcessed;
            CommandProcessors[Keyword.End].CommandProcessed += EndCommandProcessed;
            CommandProcessors[Keyword.ShowImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowButtImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowBoobsImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.SearchImageBlog].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowHardcoreImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowSoftcoreImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowLesbianImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowBlowjobImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowFemdomImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowLezdomImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowHentaiImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowGayImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowMaledomImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowCaptionsImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowGeneralImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowLikedImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowDislikedImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowBlogImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.NewBlogImage].CommandProcessed += ShowImageCommandProcessed;
            CommandProcessors[Keyword.ShowLocalImage].CommandProcessed += ShowImageCommandProcessed;

            CommandProcessors[Keyword.PlayVideo].CommandProcessed += PlayVideoCommandProcessed;
            CommandProcessors[Keyword.PlayJoiVideo].CommandProcessed += PlayVideoCommandProcessed;

            MessageProcessors = CreateMessageProcessors(settingsAccessor, stringService, new LineService(), systemVocabularyAccessor, variableAccessor, new RandomPercentService());

            MessageProcessors[MessageProcessor.ScriptResponse].MessageProcessed += ScriptResponse_MessageProcessed;
            MessageProcessors[MessageProcessor.EdgeDetection].MessageProcessed += EdgeDetection_MessageProcessed;

            _scriptAccessor = scriptAccessor;
            _variableAccessor = variableAccessor;

            _scriptTimer = timerFactory.Create();
            _scriptTimer.Elapsed += _scriptTimer_Elapsed;

            _tauntTimer = timerFactory.Create();
            _tauntTimer.Elapsed += _tauntTimer_Elapsed;

            _teaseCountDown = timerFactory.Create();
            _teaseCountDown.Elapsed += _teaseCountDown_Elapsed;

            _vocabularyProcesser = new VocabularyProcessor(new LineCollectionFilter(), new LineService());
        }

        private void EdgeCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            _teaseCountDown.Interval = 1000;
            _teaseCountDown.AutoReset = true;
            _teaseCountDown.Enabled = true;
        }

        #region events and OnEvent methods
        public event EventHandler<DommeSaidEventArgs> DommeSaid;
        /// <summary>
        /// Fire DommeSaid event, but *only* if message is not empty. 
        /// The Domme only speaks when she has something to say.
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="message"></param>
        private void OnDommeSaid(DommePersonality domme, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            DommeSaid?.Invoke(this, new DommeSaidEventArgs()
            {
                ChatMessage = new ChatMessage()
                {
                    Message = message,
                    Sender = domme.Name,
                }
            });
        }

        public event EventHandler<ShowImageEventArgs> ShowImage;
        private void OnShowImage(ImageMetaData imageMetaData)
        {
            ShowImage?.Invoke(this, new ShowImageEventArgs() { ImageMetaData = imageMetaData });
        }

        public event EventHandler<PlayVideoEventArgs> PlayVideo;
        private void OnPlayVideo(PlayVideoEventArgs eventArgs)
        {
            PlayVideo?.Invoke(this, eventArgs);
        }
        #endregion

        public void BeginSession()
        {
            if (Session.Phase != SessionPhase.BeforeSession)
                return;
            Session.Phase = GetNextPhase(Session.Phase);
            var selectScript = _scriptAccessor.GetAvailableScripts(Session.Domme, Session.Sub, "Stroke", Session.Phase)
                .OnSuccess(scripts =>
                {
                    if (scripts.Count == 0)
                        return _scriptAccessor.GetFallbackMetaData(Session, Session.Phase);
                    return Result.Ok(scripts[new Random().Next(scripts.Count)]);
                })
                .OnSuccess(smd => _scriptAccessor.GetScript(smd));

            if (selectScript.IsFailure)
            {
                OnDommeSaid(Session.Domme, "Error: " + selectScript.Error);
                return;
            }

            var setStrokeRound = _variableAccessor.SetVariable(Session.Domme, SystemVariable.StrokeRound, "0");
            var updateSubLeftEarly = _variableAccessor.GetVariable(Session.Domme, SystemVariable.SubLeftEarly)
                .OnSuccess(val => _variableAccessor.SetVariable(Session.Domme, SystemVariable.SubLeftEarly, (int.Parse(val) + 1).ToString()));

            Session.Scripts.Push(selectScript.Value);
            Session.TimeRemaining = GetTeaseDuration(Session);
            // fire off once a minute (60s * 1000ms)
            _teaseCountDown.Interval = 60000;
            _teaseCountDown.AutoReset = true;
            _teaseCountDown.Enabled = true;

            // interval between reading lines in the script
            _scriptTimer.Interval = Session.Domme.MessageTimer;
            _scriptTimer.Enabled = true;
            _scriptTimer.AutoReset = false;
        }

        /// <summary>
        /// sub says something to the Domme.
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public Result Say(ChatMessage chatMessage)
        {
            lock (_sessionLock)
            {
                if (Session.Domme.IsAfk)
                    return Result.Fail(Session.Domme.Name + " is AFK");
                return FindProcessor(Session, chatMessage)
                    .OnSuccess(proc => proc.ProcessMessage(Session, chatMessage))
                    .OnSuccess(reply =>
                    {
                        Session = reply.Session;
                        var workingLine = _vocabularyProcesser.ReplaceVocabulary(Session, reply.MessageBack);
                        OnDommeSaid(Session.Domme, workingLine);
                        return Result.Ok();
                    });
            }
        }

        /// <summary>
        /// Tell the engine that the video stopped playing
        /// </summary>
        public void VideoStopped()
        {
            if (!Session.IsVideoPlaying)
                return;
            var newSession = Session.Clone();
            newSession.IsVideoPlaying = false;
            Session = newSession;
        }

        #region private methods
        private Dictionary<string, ICommandProcessor> CreateCommandProcessors(IScriptAccessor scriptAccessor
            , IFlagAccessor flagAccessor
            , LineService lineService
            , IImageAccessor imageAccessor
            , IVideoAccessor videoAccessor
            , IVariableAccessor variableAccessor
            , ITauntAccessor tauntAccessor)
        {
            var rVal = new Dictionary<string, ICommandProcessor>();
            rVal.Add(Keyword.Wait, new WaitCommandProcessor(lineService));
            rVal.Add(Keyword.StartStroking, new StartStrokingCommandProcessor(variableAccessor));
            rVal.Add(Keyword.Edge, new EdgeCommandProcessor(lineService));

            // Image commands
            rVal.Add(Keyword.ShowImage, new ShowImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowButtImage, new ShowButtImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowBoobsImage, new ShowBoobsImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.SearchImageBlog, new SearchImageBlogCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowHardcoreImage, new ShowHardcoreImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowSoftcoreImage, new ShowSoftcoreImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowLesbianImage, new ShowLesbianImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowBlowjobImage, new ShowBlowjobImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowFemdomImage, new ShowFemdomImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowLezdomImage, new ShowLezdomImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowHentaiImage, new ShowHentaiImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowGayImage, new ShowGayImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowMaledomImage, new ShowMaledomImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowCaptionsImage, new ShowCaptionsImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowGeneralImage, new ShowGeneralImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowLikedImage, new ShowLikedImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowDislikedImage, new ShowDislikedImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowBlogImage, new ShowBlogImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.NewBlogImage, new NewBlogImageCommandProcessor(imageAccessor));
            rVal.Add(Keyword.ShowLocalImage, new ShowLocalImageCommandProcessor(imageAccessor, lineService));

            // Video commands
            rVal.Add(Keyword.PlayVideo, new PlayVideoCommandProcessor(videoAccessor));
            rVal.Add(Keyword.PlayJoiVideo, new PlayJoiVideoCommandProcessor(videoAccessor));

            rVal.Add(Keyword.RandomText, new SearchImageBlogCommandProcessor(imageAccessor));
            rVal.Add(Keyword.IncreaseOrgasmChance, new IncreaseOrgasmChanceCommand());
            rVal.Add(Keyword.DecreaseOrgasmChance, new DecreaseOrgasmChanceCommand());
            rVal.Add(Keyword.IncreaseRuinChance, new IncreaseRuinChanceCommand());
            rVal.Add(Keyword.DecreaseRuinChance, new DecreaseRuinChanceCommand());

            // Flag commands used for script logic
            rVal.Add(Keyword.SetFlag, new SetFlagCommandProcessor(new FlagService(flagAccessor), lineService));
            rVal.Add(Keyword.SetTempFlag, new TempFlagCommandProcessor(new FlagService(flagAccessor), lineService));
            rVal.Add(Keyword.DeleteFlag, new DeleteFlagCommandProcessor(flagAccessor, lineService));
            rVal.Add(Keyword.NotFlag, new NotFlagCommandProcessor(flagAccessor, lineService));
            rVal.Add(Keyword.Flag, new FlagCommandProcessor(flagAccessor, lineService));

            // Variable commands, similar to flags
            rVal.Add(Keyword.SetVar, new SetVarCommandProcessor(lineService, variableAccessor));

            // Commands that affect Domme Messaging
            rVal.Add(Keyword.RapidCodeOn, new RapidCodeOnCommandProcessor());
            rVal.Add(Keyword.RapidCodeOff, new RapidCodeOffCommandProcessor());
            rVal.Add(Keyword.AfkOn, new AfkOnCommandProcessor(lineService));
            rVal.Add(Keyword.AfkOff, new AfkOffCommandProcessor(lineService));

            // Commands that move you to another part of the script should be checked after commands that operate on the current line
            rVal.Add(Keyword.Goto, new GotoCommandProcessor(lineService));
            rVal.Add(Keyword.Chance, new ChanceCommandProcessor(lineService));
            rVal.Add(Keyword.CheckFlag, new CheckFlagCommandProcessor(flagAccessor, lineService));
            rVal.Add(Keyword.GotoDommeOrgasm, new GotoDommeOrgasmCommandProcessor(lineService));
            rVal.Add(Keyword.GotoDommeRuin, new GotoDommeRuinCommandProcessor(lineService));
            rVal.Add(Keyword.GotoDommeApathy, new GotoDommeApathyCommandProcessor(lineService));
            rVal.Add(Keyword.GotoDommeLevel, new GotoDommeLevelCommandProcessor(lineService));
            rVal.Add(Keyword.OrgasmAllow, new OrgasmAllowCommandProcessor(lineService));
            rVal.Add(Keyword.OrgasmDeny, new OrgasmDenyCommandProcessor(lineService));
            rVal.Add(Keyword.Call, new CallCommandProcessor(scriptAccessor, lineService));

            rVal.Add(Keyword.End, new EndCommandProcessor(lineService));
            rVal.Add(Keyword.NullResponse, new NullResponseCommandProcessor());


            return rVal;
        }

        private Dictionary<MessageProcessor, IMessageProcessor> CreateMessageProcessors(ISettingsAccessor settingsService
            , IStringService stringService
            , LineService lineService
            , ISystemVocabularyAccessor systemVocabularyAccessor
            , IVariableAccessor variableAccessor
            , IRandomPercentService randomPercentService)
        {
            var rval = new Dictionary<MessageProcessor, IMessageProcessor>();
            rval.Add(MessageProcessor.RequestTask, new RequestTaskMessageProcessor());
            rval.Add(MessageProcessor.Greeting, new GreetingMessageProcessor(settingsService, stringService));
            rval.Add(MessageProcessor.Safeword, new SafewordMessageProcessor());
            rval.Add(MessageProcessor.ScriptResponse, new ScriptResponseMessageProcessor(lineService));
            rval.Add(MessageProcessor.EdgeDetection, new EdgeMessageProcessor(systemVocabularyAccessor, variableAccessor, randomPercentService));
            return rval;
        }

        private Result<IMessageProcessor> FindProcessor(Session session, ChatMessage chatMessage)
        {
            foreach (var processor in MessageProcessors.Values)
            {
                if (processor.IsRelevant(session, chatMessage))
                    return Result.Ok(processor);
            }
            return Result.Fail<IMessageProcessor>("Unable to process this message");
        }

        private SessionPhase GetNextPhase(SessionPhase phase)
        {
            switch (phase)
            {
                case SessionPhase.BeforeSession:
                    return SessionPhase.Start;
                case SessionPhase.Start:
                    return SessionPhase.Modules;
                case SessionPhase.Modules:
                    return SessionPhase.Link;
                case SessionPhase.Link:
                    return SessionPhase.End;
                case SessionPhase.End:
                    return SessionPhase.AfterSession;
                default:
                    return SessionPhase.End;
            }
        }

        private int GetTeaseDuration(Session session)
        {
            if (session.Domme.DomLevel == DomLevel.Gentle)
                return new Random().Next(10, 16);
            if (session.Domme.DomLevel == DomLevel.Lenient)
                return new Random().Next(15, 21);
            if (session.Domme.DomLevel == DomLevel.Tease)
                return new Random().Next(20, 31);
            if (session.Domme.DomLevel == DomLevel.Rough)
                return new Random().Next(30, 46);
            if (session.Domme.DomLevel == DomLevel.Sadistic)
                return new Random().Next(45, 61);
            throw new Exception("Unable to determine tease time");
        }

        /// <summary>
        /// Check to see if the session is blocked(i.e. waiting) for something to happen.
        /// Examples are the script requires a response, or a video is playing. This can be anything happening that keeps the session from continuing
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private bool IsSessionBlocked(Session session)
        {
            if (session.CurrentScript == null)
                return true;

            // The script requires a response
            if (session.CurrentScript.CurrentLine[0] == '[')
                return true;

            if (session.IsVideoPlaying)
                return true;

            return false;
        }

        private Result<Session> ProcessCommands(Session session, string finalLine)
        {
            var currentLineNumber = session.CurrentScript.LineNumber;
            var workingSession = session.Clone();

            foreach (var processor in CommandProcessors.Values)
            {
                if (processor.IsRelevant(session, finalLine))
                {
                    var doCommand = processor.PerformCommand(workingSession, finalLine)
                        .OnSuccess(ws => workingSession = ws)
                        .OnSuccess(ws => finalLine = processor.DeleteCommandFrom(finalLine));
                    if (doCommand.IsFailure)
                        return Result.Fail<Session>(doCommand.Error);
                }

                // Immediately stop processing if we changed line numbers due to a command.
                // *OR* if we don't have a current script (i.e. @End command happened)
                if (workingSession.CurrentScript == null
                    || currentLineNumber != workingSession.CurrentScript.LineNumber)
                    return Result.Ok(workingSession);
            }

            return Result.Ok(workingSession);
        }

        private void ScriptResponse_MessageProcessed(object sender, MessageProcessedEventArgs e)
        {
            var response = e.Parameter as Response;

            var workingLine = response.Responses[Response.Script].Single();
            foreach (var p in CommandProcessors.Values)
            {
                workingLine = p.DeleteCommandFrom(workingLine).Trim();
            }

            workingLine = _vocabularyProcesser.ReplaceVocabulary(Session, workingLine);

            // if the engine needs to pause here, then it can.
            OnDommeSaid(e.Session.Domme, workingLine);

            var doWork = ProcessCommands(e.Session, response.Responses[Response.Script].Single())
                .OnSuccess(sesh =>
                {
                    // if none of the commands advanced the script, then do so now.
                    if (Session.CurrentScript.LineNumber == sesh.CurrentScript.LineNumber && !sesh.CurrentScript.CurrentLine.StartsWith("["))
                    {
                        sesh.CurrentScript.LineNumber++;
                    }
                    return sesh;
                })
                .OnSuccess(sesh => Session = sesh);
        }

        private void EdgeDetection_MessageProcessed(object sender, MessageProcessedEventArgs e)
        {
            //var response = e.Parameter as Response;

            //var workingLine = response.Responses[Response.Edging].Single();
            //foreach (var p in CommandProcessors.Values)
            //{
            //    workingLine = p.DeleteCommandFrom(workingLine).Trim();
            //}

            //workingLine = _vocabularyProcesser.ReplaceVocabulary(Session, workingLine);

            //OnBeforeDommeSaid(e.Session.Domme, workingLine);

            // if the engine needs to pause here, then it can.
            //OnDommeSaid(e.Session.Domme, workingLine);
            Session = e.Session;
            //var doWork = ProcessCommands(e.Session, response.Responses[Response.Script].Single())
            //    .OnSuccess(sesh =>
            //    {
            //        // if none of the commands advanced the script, then do so now.
            //        if (Session.CurrentScript.LineNumber == sesh.CurrentScript.LineNumber && !sesh.CurrentScript.CurrentLine.StartsWith("["))
            //        {
            //            sesh.CurrentScript.LineNumber++;
            //        }
            //        return sesh;
            //    })
            //    .OnSuccess(sesh => Session = sesh);
        }

        string Normalize(Session session, string input)
        {
            return input
                .Replace("  ", " ")
                .Replace(", ", ",")
                .ToLower()
                .Replace(session.Domme.Honorific.ToLower(), string.Empty);
        }

        private static int ComputeLevenshtein(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        #endregion

        #region Command event handlers
        private void EndCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            var sub = e.Session.Sub;
            e.Session.Phase = GetNextPhase(e.Session.Phase);
            if (e.Session.Phase == SessionPhase.AfterSession)
            {
                e.Session.Scripts.Clear();
                return;
            }
            var type = "Stroke";
            if (e.Session.Phase == SessionPhase.Modules)
                type = string.Empty;
            var selectScript = _scriptAccessor.GetAvailableScripts(e.Session.Domme, e.Session.Sub, type, e.Session.Phase)
                .OnSuccess(scripts =>
                {
                    if (scripts.Count == 0)
                        return _scriptAccessor.GetFallbackMetaData(e.Session, e.Session.Phase);
                    return Result.Ok(scripts[new Random().Next(scripts.Count)]);
                })
                .OnSuccess(smd => _scriptAccessor.GetScript(smd));

            if (selectScript.IsFailure)
            {
                OnDommeSaid(e.Session.Domme, "Error: " + selectScript.Error);
                return;
            }

            e.Session.Scripts.Push(selectScript.Value);
            _scriptTimer.Interval = 1000;
            _scriptTimer.Enabled = false;
        }

        private void PlayVideoCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            var playEventArgs = (PlayVideoEventArgs)e.Parameter;
            OnPlayVideo(playEventArgs);
            e.Session.IsVideoPlaying = playEventArgs.Result.IsSuccess;
        }

        private void ShowImageCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            var imd = (ImageMetaData)e.Parameter;
            OnShowImage(imd);
        }

        private void StartStrokingCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            //StrokeTimer.Start()
            //TauntTimer.Start();
        }
        #endregion

        #region Services
        private readonly IScriptAccessor _scriptAccessor;
        private readonly IVariableAccessor _variableAccessor;
        private readonly VocabularyProcessor _vocabularyProcesser;
        #endregion

        #region Timers
        ITimer _scriptTimer;
        private void _scriptTimer_Elapsed(object sender, EventArgs e)
        {
            lock (_sessionLock)
            {
                if (IsSessionBlocked(Session))
                {
                    _scriptTimer.Enabled = true;
                    return;
                }

                // If we are on a bookmark, skip ahead one line
                if (Session.CurrentScript.CurrentLine[0] == '(')
                    Session.CurrentScript.LineNumber++;

                // First we replace the vocabulary
                var workingLine = _vocabularyProcesser.ReplaceVocabulary(Session, Session.CurrentScript.CurrentLine);
                var commandLine = workingLine;

                // Strip out commands so the Domme can speak
                foreach (var p in CommandProcessors.Values)
                {
                    workingLine = p.DeleteCommandFrom(workingLine).Trim();
                }

                OnDommeSaid(Session.Domme, workingLine);

                // Then we actually process the commands for this line
                var doWork = ProcessCommands(Session, commandLine)
                    .OnSuccess(sesh =>
                    {
                        // If there is a script, and it didn't advance yet, then do so now.
                        if (sesh.CurrentScript != null && Session.CurrentScript.LineNumber == sesh.CurrentScript.LineNumber)
                        {
                            var script = sesh.Scripts.Pop();
                            script.LineNumber++;
                            sesh.Scripts.Push(script);
                        }
                        return sesh;
                    })
                    .OnSuccess(sesh => Session = sesh);

                // We are all done, so go ahead and schedule a new timer.
                _scriptTimer.Enabled = true;
            }
        }

        ITimer _tauntTimer;
        private void _tauntTimer_Elapsed(object sender, EventArgs e)
        {
        }

        ITimer _teaseCountDown;
        private void _teaseCountDown_Elapsed(object sender, EventArgs e)
        {
            Session.TimeRemaining = Math.Min(0, Session.TimeRemaining - 1);
        }

        public void EndSession()
        {
        //    StrokePace = 0

        //If Directory.Exists(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\System\Flags\Temp\") Then
        //    My.Computer.FileSystem.DeleteDirectory(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\System\Flags\Temp\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        //End If

        //System.IO.Directory.CreateDirectory(Application.StartupPath & "\Scripts\" & dompersonalitycombobox.Text & "\System\Flags\Temp\")

        //ssh.TauntEdging = False

        //ssh.CBTBallsFirst = True
        //ssh.CBTCockFirst = True
        //ssh.CBTBothFirst = True
        //ssh.CustomTaskFirst = True

        //ssh.VideoType = "General"

        //ssh.UpdatesTick = 120
        //UpdatesTimer.Start()

        //Me.ActiveControl = Me.chatBox

        //ssh.JustShowedBlogImage = False

        //mySession.Session.Domme.WasGreeted = False
        //ssh.SubWroteLast = False
        //ssh.WritingTaskFlag = False

        //ssh.OrgasmYesNo = False

        //FrmSettings.LockOrgasmChances(False)

        //ssh.ShowModule = False
        //ssh.BookmarkLink = False
        //ssh.BookmarkModule = False
        //ssh.YesOrNo = False

        //ssh.StartStrokingCount = 0


        //ssh.StrokeTauntVal = -1

        //ssh.EdgeToRuinSecret = True

        //TeaseTimer.Stop()

        //DeleteVariable("SYS_StrokeRound")

        //mainPictureBox.Image = Nothing
        //ssh.SlideshowLoaded = False

        //FrmSettings.DominationLevel.Value = My.Settings.DomLevel
        //FrmSettings.NBEmpathy.Value = My.Settings.DomEmpathy

        //' Github Patch
        //BTNPlaylist.Enabled = True

        //If PNLWritingTask.Visible Then CloseApp(PNLWritingTask)
        }
        #endregion
    }
}