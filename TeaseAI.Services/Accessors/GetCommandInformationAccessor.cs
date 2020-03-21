using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class GetCommandInformationAccessor : IGetCommandInformationAccessor
    {
        public GetCommandInformationAccessor()
        {
        }

        public Result<ScriptCommandInformation> GetCommandInformation(string command)
        {
            var data = File.ReadAllText("ScriptCommands.json");
            var commands = JsonConvert.DeserializeObject<List<ScriptCommandInformation>>(data);
            var item = commands.FirstOrDefault(c => c.Command == command);

            return item == null ? Result.Fail<ScriptCommandInformation>("Unable to find this command's documentation") : Result.Ok(item); 
        }

    }
}
