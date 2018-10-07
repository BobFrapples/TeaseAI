using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;

namespace TeaseAI.Services
{
    public class LineService
    {
        public LineService()
        {
            _stringService = new StringService();
        }

        /// <summary>
        /// Get the data inside the parentheses of a command string.
        /// i.e. calling GetParentheseData("foo(BAR)", "foo(") succeeds with a value of BAR
        /// NOTE: This method will only match the first instance of <paramref name="command"/>
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public Result<List<string>> GetParenData(string input, string command)
        {
            var startIndex = input.IndexOf(command) + command.Length;

            var getFlag = GetParenType(command)
                .OnSuccess(parenType => input.IndexOf(parenType, startIndex))
                .Ensure(idx => idx >= 0, "Unable to find the closing paren in " + input)
                .OnSuccess(endIndex => input.Substring(startIndex, endIndex - startIndex))
                .OnSuccess(data => data.Split(',').Select(item => item.Trim()).ToList());

            return getFlag;
        }

        public string ReplaceParenData(string input, string command, string newText)
        {
            if (!input.Contains(command))
                return input;

            var cmd = GetParenType(command)
                .OnSuccess(parenType =>
                {
                    var startIndex = input.IndexOf(command);
                    var closeIndex = input.IndexOf(parenType, startIndex) + 1;
                    return input.Substring(startIndex, closeIndex - startIndex);
                });
            if (cmd.IsSuccess)
                return input.Replace(cmd.Value, newText);

            return input.Replace(command, newText);
        }

        public Result<string> GetAnswerResponse(DommePersonality domme, string answerString, List<string> scriptSection)
        {
            foreach (var currentLine in scriptSection)
            {
                var shouldLookForYes = currentLine.ToUpper().Contains("[YES]");
                var shouldLookForNo = currentLine.ToUpper().Contains("[NO]");

                var responses = GetParenData(currentLine, "[").GetResultOrDefault(new List<string>());
                var chatMessage = currentLine.Trim();
                if (chatMessage.Contains("]"))
                    chatMessage = currentLine.Substring(currentLine.IndexOf("]") + 1).Trim();

                for (var i = 0; i < responses.Count; i++)
                {
                    if (answerString.ToUpper().ToString().Contains(responses[i].ToUpper().ToString()))
                    {
                        if (shouldLookForYes || shouldLookForNo)
                        {
                            if (domme.RequiresHonorific)
                            {
                                if (!_stringService.WordExists(answerString.ToUpper(), domme.Honorific.ToUpper()))
                                    return Result.Ok(_stringService.Capitalize(responses[i] + " what?"));

                                if (domme.RequiresHonorificCapitalized && !_stringService.WordExists(answerString, _stringService.Capitalize(domme.Honorific.ToLower())))
                                    return Result.Ok("#CapitalizeHonorific");
                            }
                        }

                        return Result.Ok(chatMessage);
                    }
                }
            }

            return Result.Fail<string>("No response found");
        }

        public string DeleteCommand(string input, string command)
        {
            return ReplaceParenData(input, command, string.Empty);
        }

        private Result<string> GetParenType(string command)
        {
            var data = command.ToCharArray();
            if (command.Substring(command.Length - 1, 1) == "(")
                return Result.Ok(")");

            if (data.Last() == '[')
                return Result.Ok("]");

            return Result.Fail<string>("Unknown paren type of " + data.Last());
        }

        private StringService _stringService;
    }
}
