using System.Collections.Generic;
using System.IO;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Data.RiskyPick;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class StartRiskyPickCommandProcessor : CommandProcessorBase
    {
        private readonly int RiskyPickCost = 0;
        private readonly LineService _lineService;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly ISettingsAccessor _settingsAccessor;

        public StartRiskyPickCommandProcessor(LineService lineService, IPathsAccessor pathsAccessor, ISettingsAccessor settingsAccessor)
        {
            _lineService = lineService;
            _pathsAccessor = pathsAccessor;
            _settingsAccessor = settingsAccessor;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.StartRiskyPick);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.StartRiskyPick) && session.GameBoard == null;

        public override Result<Session> PerformCommand(Session session, string line)
        {
            return Result.Ok(session.Clone())
                .Ensure(s => !s.Domme.WasGreeted, "Risky Pick cannot be started from the Games window when there is a session in progress!")
                .Ensure(s => s.GameBoard == null, "A new Risky Pick game cannot be started until the current game is finished!")
                // .Ensure(s => s.GameBoard.EdgesOwed == 0, "You still owe edges from your previous game!")
                .Ensure(s => s.Sub.Purse[TokenDenomination.Bronze] >= RiskyPickCost, "It costs 100 Bronze Tokens to play Risky Pick!")
                .Ensure(s => File.Exists(_pathsAccessor.RiskyPickScript), _pathsAccessor.RiskyPickScript + " was not found.")
                .OnSuccess(s =>
                {
                    var smd = new ScriptMetaData
                    {
                        Key = _pathsAccessor.RiskyPickScript,
                        Info = "Risky Pick Game. Similar to Deal or No Deal",
                        IsEnabled = true,
                        Name = "Risky Pick Game"
                    };

                    var script = new Script(smd, File.ReadAllLines(_pathsAccessor.RiskyPickScript));
                    s.Sub.Purse[TokenDenomination.Bronze] = -RiskyPickCost;

                    s.GameBoard = RiskyPickGameBoard.Create(GetRiskyPickCaseValues());

                    OnCommandProcessed(s, script);

                    //RiskyState = True
                    //            mainWindow.ssh.RiskyDeal = True
                    //            SetupRiskyPick()

                    //            PlayRiskyPickButton.Text = ""
                    //            PlayRiskyPickButton.Enabled = False

                    //            mainWindow.ssh.StrokeTauntVal = -1

                    //            If Directory.Exists(My.Settings.DomImageDir) AndAlso mainWindow.ssh.SlideshowLoaded Then
                    //                mainWindow.LoadDommeImageFolder()
                    //            End If
                    //        s.BeginSession(script)
                    return s;
                });
        }

        private List<int> GetRiskyPickCaseValues() => new List<int> {
            1,
            2,
            3,
            4,
            5,
            7,
            10,
            12,
            15,
            20,
            25,
            30,
            40,
            50,
            55,
            60,
            65,
            70,
            75,
            80,
            85,
            90,
            95,
            100
        };
    }
}
