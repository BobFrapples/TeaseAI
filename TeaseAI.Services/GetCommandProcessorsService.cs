using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services.CommandProcessor;

namespace TeaseAI.Services
{
    public class GetCommandProcessorsService : IGetCommandProcessorsService
    {
        private readonly IScriptAccessor _scriptAccessor;
        private readonly IFlagAccessor _flagAccessor;
        private readonly LineService _lineService;
        private readonly IImageAccessor _imageAccessor;
        private readonly IVideoAccessor _videoAccessor;
        private readonly IVariableAccessor _variableAccessor;
        private readonly ITauntAccessor _tauntAccessor;
        private readonly IConfigurationAccessor _configurationAccessor;
        private readonly IRandomNumberService _randomNumberService;
        private readonly INotifyUser _notifyUser;
        private readonly ISettingsAccessor _settingsAccessor;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly IBookmarkService _bookmarkService;
        private readonly IMediaContainerService _mediaContainerService;

        public GetCommandProcessorsService(IScriptAccessor scriptAccessor
            , IFlagAccessor flagAccessor
            , LineService lineService
            , IImageAccessor imageAccessor
            , IVideoAccessor videoAccessor
            , IVariableAccessor variableAccessor
            , ITauntAccessor tauntAccessor
            , IConfigurationAccessor configurationAccessor
            , IRandomNumberService randomNumberService
            , INotifyUser notifyUser
            , ISettingsAccessor settingsAccessor
            , IPathsAccessor pathsAccessor
            , IBookmarkService bookmarkService
            , IMediaContainerService mediaContainerService
            )
        {
            _scriptAccessor = scriptAccessor;
            _flagAccessor = flagAccessor;
            _lineService = lineService;
            _imageAccessor = imageAccessor;
            _videoAccessor = videoAccessor;
            _variableAccessor = variableAccessor;
            _tauntAccessor = tauntAccessor;
            _configurationAccessor = configurationAccessor;
            _randomNumberService = randomNumberService;
            _notifyUser = notifyUser;
            _settingsAccessor = settingsAccessor;
            _pathsAccessor = pathsAccessor;
            _bookmarkService = bookmarkService;
            _mediaContainerService = mediaContainerService;
        }

        public Dictionary<string, ICommandProcessor> CreateCommandProcessors()
        {
            var rVal = new Dictionary<string, ICommandProcessor>();
            rVal.Add(Keyword.Wait, new WaitCommandProcessor(_lineService));
            rVal.Add(Keyword.StartStroking, new StartStrokingCommandProcessor(_variableAccessor, _lineService));
            rVal.Add(Keyword.Edge, new EdgeCommandProcessor(_lineService));

            // Image commands
            rVal.Add(Keyword.ShowImage, new ShowImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowButtImage, new ShowButtImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowBoobsImage, new ShowBoobsImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.SearchImageBlog, new SearchImageBlogCommandProcessor(_imageAccessor));
            rVal.Add(Keyword.ShowHardcoreImage, new ShowHardcoreImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowSoftcoreImage, new ShowSoftcoreImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowLesbianImage, new ShowLesbianImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowBlowjobImage, new ShowBlowjobImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowFemdomImage, new ShowFemdomImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowLezdomImage, new ShowLezdomImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowHentaiImage, new ShowHentaiImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowGayImage, new ShowGayImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowMaledomImage, new ShowMaledomImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowCaptionsImage, new ShowCaptionsImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowGeneralImage, new ShowGeneralImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowLikedImage, new ShowLikedImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowDislikedImage, new ShowDislikedImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowBlogImage, new ShowBlogImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.NewBlogImage, new NewBlogImageCommandProcessor(_imageAccessor, _lineService, _randomNumberService));
            rVal.Add(Keyword.ShowLocalImage, new ShowLocalImageCommandProcessor(_imageAccessor, _lineService));

            // Video commands
            rVal.Add(Keyword.PlayVideo, new PlayVideoCommandProcessor(_videoAccessor));
            rVal.Add(Keyword.PlayJoiVideo, new PlayJoiVideoCommandProcessor(_lineService, _videoAccessor, _randomNumberService));

            rVal.Add(Keyword.RandomText, new SearchImageBlogCommandProcessor(_imageAccessor));
            rVal.Add(Keyword.IncreaseOrgasmChance, new IncreaseOrgasmChanceCommand(_lineService));
            rVal.Add(Keyword.DecreaseOrgasmChance, new DecreaseOrgasmChanceCommand(_lineService));
            rVal.Add(Keyword.IncreaseRuinChance, new IncreaseRuinChanceCommand(_lineService));
            rVal.Add(Keyword.DecreaseRuinChance, new DecreaseRuinChanceCommand(_lineService));

            // Flag commands used for script logic
            rVal.Add(Keyword.SetFlag, new SetFlagCommandProcessor(new FlagService(_flagAccessor), _lineService));
            rVal.Add(Keyword.SetTempFlag, new TempFlagCommandProcessor(new FlagService(_flagAccessor), _lineService));
            rVal.Add(Keyword.DeleteFlag, new DeleteFlagCommandProcessor(_flagAccessor, _lineService));
            rVal.Add(Keyword.NotFlag, new NotFlagCommandProcessor(_flagAccessor, _lineService));
            rVal.Add(Keyword.Flag, new FlagCommandProcessor(_flagAccessor, _lineService));

            // Variable commands, similar to flags
            rVal.Add(Keyword.SetVar, new SetVarCommandProcessor(_lineService, _variableAccessor));

            // Commands that affect Domme Messaging
            rVal.Add(Keyword.RapidCodeOn, new RapidTextOnCommandProcessor(_lineService));
            rVal.Add(Keyword.RapidCodeOff, new RapidTextOffCommandProcessor(_lineService));
            rVal.Add(Keyword.AfkOn, new AfkOnCommandProcessor(_lineService));
            rVal.Add(Keyword.AfkOff, new AfkOffCommandProcessor(_lineService));

            // Commands that move you to another part of the script should be checked after commands that operate on the current line
            rVal.Add(Keyword.Goto, new GotoCommandProcessor(_lineService, _bookmarkService, _randomNumberService));
            rVal.Add(Keyword.Chance, new ChanceCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.CheckFlag, new CheckFlagCommandProcessor(_flagAccessor, _lineService, _bookmarkService));
            rVal.Add(Keyword.GotoDommeOrgasm, new GotoDommeOrgasmCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.GotoDommeRuin, new GotoDommeRuinCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.GotoDommeApathy, new GotoDommeApathyCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.GotoDommeLevel, new GotoDommeLevelCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.OrgasmAllow, new OrgasmAllowCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.OrgasmDeny, new OrgasmDenyCommandProcessor(_lineService, _bookmarkService));
            rVal.Add(Keyword.Call, new CallCommandProcessor(_scriptAccessor, _lineService, _pathsAccessor, _bookmarkService));
            rVal.Add(Keyword.Unpause, new UnpauseCommandProcessor(_lineService));

            rVal.Add(Keyword.CockTorture, new CockTortureCommandProcessor(_lineService, _configurationAccessor, _randomNumberService, _pathsAccessor));
            rVal.Add(Keyword.BallTorture, new BallTortureCommandProcessor(_lineService, _configurationAccessor, _randomNumberService, _pathsAccessor));
            rVal.Add(Keyword.CustomTask, new CustomTaskCommandProcessor(_lineService, _configurationAccessor, _randomNumberService, _variableAccessor, _pathsAccessor));

            rVal.Add(Keyword.AddTokens, new AddTokensCommandProcessor(_lineService, _settingsAccessor, _notifyUser));

            rVal.Add(Keyword.RiskyPickStart, new RiskyPickStartCommandProcessor(_lineService, _pathsAccessor, _settingsAccessor));
            rVal.Add(Keyword.RiskyPickWaitForCase, new RiskyPickWaitForCaseCommandProcessor(_lineService));
            rVal.Add(Keyword.RiskyPickSelectCase, new RiskyPickSelectCaseCommandProcessor(_lineService));
            rVal.Add(Keyword.RiskyPickCheck, new RiskyPickCheckCommandProcessor(_lineService));

            rVal.Add(Keyword.End, new EndCommandProcessor(_lineService));
            rVal.Add(Keyword.NullResponse, new NullResponseCommandProcessor(_lineService));
            rVal.Add(Keyword.Info, new InfoCommandProcessor(_lineService));
            rVal.Add(Keyword.DifferentAnswer, new DifferentAnswerCommandProcessor(_lineService));
            rVal.Add(Keyword.AcceptAnswer, new AcceptAnswerCommandProcessor(_lineService));
            rVal.Add(Keyword.LikeImage, new LikeImageCommandProcessor(_lineService, _imageAccessor, _mediaContainerService));
            rVal.Add(Keyword.DislikeImage, new DislikeImageCommandProcessor(_lineService, _imageAccessor, _mediaContainerService));
            rVal.Add(Keyword.DecideOrgasm, new DecideOrgasmCommandProcessor(_lineService, _randomNumberService, _bookmarkService));

            return rVal;
        }
    }
}
