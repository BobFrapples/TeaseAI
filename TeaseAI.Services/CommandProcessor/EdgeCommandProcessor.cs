using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class EdgeCommandProcessor : CommandProcessorBase
    {
        public EdgeCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.Edge);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.Edge + "(") || line.Contains(Keyword.Edge);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (line.Contains("("))
            {
                //TODO: Implement flags for Edge
            }
            if (workingSession.Sub.IsStroking)
                workingSession.Sub.IsAlreadyStrokingEdge = true;

            var script = CreateTauntScript();
            workingSession.Scripts.Push(script);

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }

        private Script CreateTauntScript()
        {
            var lines = new List<string>
            {
                "(TauntSub)",
                "#EdgeTaunt",
                "@Goto(TauntSub)",
                "@End",
            };
            var metaData = new ScriptMetaData
            {
                Info = "Endless loop where Domme taunts the sub until the sub edges",
                Name = "Taunt(Edge)",
                Key = "Generated",
            };
            return new Script(metaData, lines);
        }

        private LineService _lineService;
    }
}
