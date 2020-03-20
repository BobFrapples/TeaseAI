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

        public List<ScriptMetaData> GetAllScripts(string dommePersonalityName, string type, SessionPhase stage, bool isEnabledDefault)
        {
            throw new NotImplementedException();
        }

        public Result<List<ScriptMetaData>> GetAllScripts(string dommePersonalityName)
        {
            var personalityDir = _pathsAccessor.GetPersonalityFolder(dommePersonalityName);

            var sessionPhase = SessionPhase.Start;
            var cldFile = _pathsAccessor.GetScriptCld(dommePersonalityName, sessionPhase);
            var scriptDir = _pathsAccessor.GetScriptDir(dommePersonalityName, "stroke", sessionPhase);
            var checkList = _cldAccessor.ReadCld(scriptDir, cldFile)
                .OnSuccess(clData =>
                {
                    clData.ForEach(cld =>
                    {
                        cld.SessionPhase = SessionPhase.Start;
                        cld.Info = GetScriptInfo(cld.Key);
                    });
                });
            return checkList;
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
