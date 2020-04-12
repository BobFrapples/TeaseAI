using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class ScriptAccessor : IScriptAccessor
    {
        private readonly IPathsAccessor _pathsAccessor;
        private readonly ICldAccessor _cldAccessor;

        public ScriptAccessor(IPathsAccessor pathsAccessor, ICldAccessor cldAccessor)
        {
            _pathsAccessor = pathsAccessor;
            _cldAccessor = cldAccessor;
        }

        public List<ScriptMetaData> GetAllScripts(string dommePersonalityName,  SessionPhase stage)
        {
            return GetAllScripts(dommePersonalityName)
                .OnSuccess(sl => sl.Where(s => s.SessionPhase == stage).ToList())
                .GetResultOrDefault();
        }

        public Result<List<ScriptMetaData>> GetAllScripts(string dommePersonalityName)
        {
            var checkList = new List<ScriptMetaData>();
            var personalityDir = _pathsAccessor.GetPersonalityFolder(dommePersonalityName);
            var scriptDatas = new List<Tuple<string, SessionPhase>>
            {
                Tuple.Create("stroke", SessionPhase.Start ),
                Tuple.Create("Modules", SessionPhase.Modules ),
                Tuple.Create("stroke", SessionPhase.Link ),
                Tuple.Create("stroke", SessionPhase.End ),
            };

            foreach (var scriptData in scriptDatas)
            {
                var cldFile = _pathsAccessor.GetScriptCld(dommePersonalityName, scriptData.Item2);
                var scriptDir = _pathsAccessor.GetScriptDir(dommePersonalityName, scriptData.Item1, scriptData.Item2);
                var getScripts = _cldAccessor.ReadCld(scriptDir, cldFile)
                    .OnSuccess(clData =>
                    {
                        clData.ForEach(cld =>
                        {
                            cld.SessionPhase = scriptData.Item2;
                            cld.Info = GetScriptInfo(cld.Key);
                        });
                        checkList.AddRange(clData);
                    });
                if (getScripts.IsFailure)
                    return getScripts;
            }
            return Result.Ok(checkList);
        }

        public Result<List<ScriptMetaData>> GetAvailableScripts(DommePersonality domme, SubPersonality submissive, string type, SessionPhase stage)
        {
            throw new NotImplementedException();
        }

        public Result<ScriptMetaData> GetFallbackMetaData(Session session, SessionPhase stage)
        {
            throw new NotImplementedException();
        }

        public Result<Script> GetScript(ScriptMetaData metaData)
        {
            try
            {
                return Result.Ok(new Script(metaData, File.ReadAllLines(metaData.Key).ToList()));
            }
            catch (Exception ex)
            {
                return Result.Fail<Script>(ex.Message);
            }
        }

        public Result Save(Script script)
        {
            try
            {
                File.WriteAllLines(script.MetaData.Key, script.Lines);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<Script> GetScript(DommePersonality domme, string fileName)
        {
            throw new NotImplementedException();
        }

        public Result<Script> GetScript(string id)
        {
            var metaData = CreateScriptMetaData(id)
                .OnSuccess(smd => new Script(smd, File.ReadAllLines(smd.Key)));

            return metaData;
        }

        public void Save(List<ScriptMetaData> scripts, string dommePersonalityName, string type, SessionPhase stage)
        {
            throw new NotImplementedException();
        }

        private Result<ScriptMetaData> CreateScriptMetaData(string id)
        {
            if (!File.Exists(id))
                return Result.Fail<ScriptMetaData>(id + " does not exist please try again.");

            var script = new ScriptMetaData
            {
                Name = Path.GetFileName(id).Replace(".txt", ""),
                Key = id,
                Info = GetScriptInfo(id)
            };

            return Result.Ok(script);
        }

        private string GetScriptInfo(string id)
        {
            var data = File.ReadAllLines(id).ToList();

            var info = data.FirstOrDefault(line => line.StartsWith("@Info"));
            if (!string.IsNullOrWhiteSpace(info))
                info = info.Replace("@Info", "");

            return info;
        }


    }
}
