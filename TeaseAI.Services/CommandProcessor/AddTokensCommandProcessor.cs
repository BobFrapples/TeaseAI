using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class AddTokensCommandProcessor : CommandProcessorBase
    {
        public AddTokensCommandProcessor(LineService lineService, ISettingsAccessor settingsAccessor, INotifyUser notifyUser):base(Keyword.AddTokens,lineService)
        {
            _lineService = lineService;
            _settingsAccessor = settingsAccessor;
            _notifyUser = notifyUser;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            return _lineService.GetParenData(line, Keyword.AddTokens)
                .OnSuccess(lineData =>
                {
                    var workingSession = session.Clone();
                    foreach (var tokenData in lineData)
                    {
                        var createToken = Token.Create(tokenData)
                            .OnSuccess(tokens => workingSession.Sub.Purse[tokens.Denomination] += tokens.Amount);
                        if (createToken.IsFailure)
                            return Result.Fail<Session>(createToken.Error);
                    }
                    _settingsAccessor.BronzeTokens = workingSession.Sub.Purse[TokenDenomination.Bronze];
                    _settingsAccessor.SilverTokens = workingSession.Sub.Purse[TokenDenomination.Silver];
                    _settingsAccessor.GoldTokens = workingSession.Sub.Purse[TokenDenomination.Gold];

                    OnCommandProcessed(workingSession, null);
                    _notifyUser.ModalMessage(workingSession.Domme.Name + " has given you some tokens!");

                    return Result.Ok(workingSession);
                });
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _lineService.GetParenData(line, Keyword.AddTokens)
                .OnSuccess(lineData =>
                {
                    foreach (var tokenData in lineData)
                    {
                        var createToken = Token.Create(tokenData).Map();
                        if (createToken.IsFailure)
                            return createToken;
                    }
                    return Result.Ok();
                });
        }

        private readonly LineService _lineService;
        private readonly ISettingsAccessor _settingsAccessor;
        private readonly INotifyUser _notifyUser;
    }
}
