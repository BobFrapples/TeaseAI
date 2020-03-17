using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class CldAccessor : ICldAccessor
    {
        public Result<List<ScriptMetaData>> ReadCld(string fileName)
        {
            var data = Result.Ok()
                .Ensure(() => File.Exists(fileName), "Checklist file " + fileName + " does not exist")
                .OnSuccess(() =>
                {
                    using (var fs = new FileStream(fileName, FileMode.Open))
                    using (var binRead = new BinaryReader(fs))
                    {
                        var returnValue = new List<ScriptMetaData>();
                        while (fs.Position < fs.Length)
                        {
                            var cldData = new ScriptMetaData
                            {
                                Key = String.Empty,
                                Name = binRead.ReadString(),
                                IsEnabled = binRead.ReadBoolean(),
                                Info = String.Empty
                            };
                            returnValue.Add(cldData);
                        }
                        return Result.Ok(returnValue);
                    }
                });
            return data;
        }

        public Result<List<ScriptMetaData>> ReadCld(string scriptHomeDir, string fileName)
        {
            var data = Result.Ok()
                .Ensure(() => File.Exists(fileName), "Checklist file " + fileName + " does not exist")
                .OnSuccess(() =>
                {
                    using (var fs = new FileStream(fileName, FileMode.Open))
                    using (var binRead = new BinaryReader(fs))
                    {
                        var returnValue = new List<ScriptMetaData>();
                        while (fs.Position < fs.Length)
                        {
                            var name = binRead.ReadString();
                            var cldData = new ScriptMetaData
                            {
                                Key = scriptHomeDir + Path.DirectorySeparatorChar + name + ".txt",
                                Name = name,
                                IsEnabled = binRead.ReadBoolean(),
                                Info = String.Empty
                            };
                            returnValue.Add(cldData);
                        }
                        return Result.Ok(returnValue);
                    }
                });
            return data;
        }

        public void WriteCld(List<ScriptMetaData> cldData, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (var fs = new FileStream(filePath, FileMode.Create))
            using (var binWrite = new BinaryWriter(fs))
            {
                foreach (var cld in cldData)
                {
                    binWrite.Write(cld.Name);
                    binWrite.Write(cld.IsEnabled);
                }
            }
        }
    }
}
