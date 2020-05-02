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
using System.Diagnostics;

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
            , ISystemVocabularyAccessor systemVocabularyAccessor
            , IVocabularyAccessor vocabularyAccessor
            , ILineCollectionFilter lineCollectionFilter
            , IRandomNumberService randomNumberService
            , IConfigurationAccessor configurationAccessor
            , INotifyUser notifyUser
            , IPathsAccessor pathsAccessor
            , IGetCommandProcessorsService getCommandProcessorsService
            )
        {
            CommandProcessors = getCommandProcessorsService.CreateCommandProcessors();

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

            MessageProcessors = CreateMessageProcessors(settingsAccessor, stringService, new LineService(), systemVocabularyAccessor, variableAccessor, new RandomNumberService());

            MessageProcessors[MessageProcessor.ScriptResponse].MessageProcessed += ScriptResponse_MessageProcessed;
            MessageProcessors[MessageProcessor.EdgeDetection].MessageProcessed += EdgeDetection_MessageProcessed;

            CommandProcessors[Keyword.RiskyPickStart].CommandProcessed += RiskyPickStartCommandProcessed;

            CommandProcessors[Keyword.LikeImage].BeforeCommandProcessed += LikeImageCommandProcessed;

            _scriptAccessor = scriptAccessor;
            _variableAccessor = variableAccessor;

            _scriptTimer = timerFactory.Create();
            _scriptTimer.Elapsed += _scriptTimer_Elapsed;

            _tauntTimer = timerFactory.Create();
            _tauntTimer.Elapsed += _tauntTimer_Elapsed;

            _teaseCountDown = timerFactory.Create();
            _teaseCountDown.Elapsed += _teaseCountDown_Elapsed;

            _vocabularyProcesser = new VocabularyProcessor(lineCollectionFilter, new LineService(), vocabularyAccessor, imageAccessor, randomNumberService);
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

        /// <summary>
        /// Event used to ask the UI what image is currently being displayed
        /// </summary>
        public event EventHandler<ShowImageEventArgs> QueryImage;
        private void OnQueryImage(ShowImageEventArgs queryImageEventArgs)
        {
            QueryImage?.Invoke(this, queryImageEventArgs);
        }

        public event EventHandler<PlayVideoEventArgs> PlayVideo;
        private void OnPlayVideo(PlayVideoEventArgs eventArgs)
        {
            PlayVideo?.Invoke(this, eventArgs);
        }
        #endregion

        public void BeginSession()
        {
            Debug.Print("Begin Session");
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
                .OnSuccess(smd => _scriptAccessor.GetScript(smd))
                .OnSuccess(s => BeginSession(s));
            if (selectScript.IsFailure)
            {
                OnDommeSaid(Session.Domme, "Error: " + selectScript.Error);
                return;
            }
        }

        public void BeginSession(Script script)
        {
            Debug.Print("Script: " + script.MetaData.Key);

            var setStrokeRound = _variableAccessor.SetVariable(Session.Domme, SystemVariable.StrokeRound, "0");
            var updateSubLeftEarly = _variableAccessor.GetVariable(Session.Domme, SystemVariable.SubLeftEarly)
                .OnSuccess(val => _variableAccessor.SetVariable(Session.Domme, SystemVariable.SubLeftEarly, (int.Parse(val) + 1).ToString()));

            Session.Scripts.Push(script);
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

        /// <summary>
        /// Process the command passed in. This is expected to be only the command on the line
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Result SendCommand(string command)
        {
            lock (_sessionLock)
            {
                var workingSession = Session.Clone();

                foreach (var processor in CommandProcessors.Values)
                {
                    if (processor.IsRelevant(workingSession, command))
                    {
                        var doCommand = processor.PerformCommand(workingSession, command)
                            .OnSuccess(ws => workingSession = ws)
                            .OnSuccess(ws => command = processor.DeleteCommandFrom(command));
                        if (doCommand.IsFailure)
                            return doCommand.Map();
                    }
                }
                Session = workingSession;
                return Result.Ok();
            }
        }

        #region private methods
        private Dictionary<MessageProcessor, IMessageProcessor> CreateMessageProcessors(ISettingsAccessor settingsService
            , IStringService stringService
            , LineService lineService
            , ISystemVocabularyAccessor systemVocabularyAccessor
            , IVariableAccessor variableAccessor
            , IRandomNumberService randomPercentService)
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

            if (session.IsScriptPaused)
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

        private void RiskyPickStartCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            lock (_sessionLock)
            {
                Session = e.Session;
            }
            BeginSession((Script)e.Parameter);
        }

        private void LikeImageCommandProcessed(object sender, CommandProcessedEventArgs e)
        {
            var eventArgs = (ShowImageEventArgs)e.Parameter;
            OnQueryImage(eventArgs);
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

                if (doWork.IsFailure)
                    OnDommeSaid(Session.Domme, doWork.Error.Message);
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