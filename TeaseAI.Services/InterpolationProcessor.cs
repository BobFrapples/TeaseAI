using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services
{
    public class InterpolationProcessor : IInterpolationProcessor
    {
        public InterpolationProcessor(ISettingsAccessor settingsAccessor)
        {
            _settingsAccessor = settingsAccessor;
        }

        public Result<string> Interpolate(Session session, string line)
        {
            var workingLine = line;
            var objectNames = GetObjectNames(line);

            var errors = new List<string>();
            foreach (var objectName in objectNames)
            {
                var propertyNames = objectName.Split('.').ToList();
                var className = propertyNames[0];
                propertyNames.RemoveAt(0);

                var workingClass = GetWorkingClass(session, className).Value;
                var replaceString = FindProperty(workingClass, propertyNames)
                    .OnSuccess(p => p.ToString())
                    .OnSuccess(v => workingLine.Replace("{" + objectName + "}", v));
                if (replaceString.IsFailure)
                    errors.Add(replaceString.Error.Message);
                else
                    workingLine = replaceString.Value;
            }

            if (errors.Any())
                return Result.Fail<string>(string.Join(Environment.NewLine, errors));
            return Result.Ok(workingLine);
        }

        public Result Parse(Session session, string line)
        {
            var objectNames = GetObjectNames(line);
            var errors = new List<string>();
            foreach (var objectName in objectNames)
            {
                var propertyNames = objectName.Split('.').ToList();
                var className = propertyNames[0];
                propertyNames.RemoveAt(0);

                var workingClass = GetWorkingClass(session, className).Value;
                var validatePath = ValidatePath(workingClass, propertyNames);
                if (validatePath.IsFailure)
                    return validatePath;

                var obsoleteMessage = GetObsoleteMessage(workingClass, propertyNames);
                if (!string.IsNullOrWhiteSpace(obsoleteMessage))
                    errors.Add(obsoleteMessage);
            }

            if (errors.Any())
                return Result.Fail(string.Join(Environment.NewLine, errors));
            return Result.Ok();
        }

        private string GetObsoleteMessage(object startingPoint, IEnumerable<string> propertyPath)
        {
            object currentObject = startingPoint;
            Type currentType = currentObject.GetType();
            for (var i = 0; i < propertyPath.Count(); i++)
            {
                var propertyInfo = currentType.GetProperty(propertyPath.ElementAt(i));
                var attributes = propertyInfo.GetCustomAttributes(false);
                foreach (var attribute in attributes.OfType<ObsoleteAttribute>())
                {
                    return propertyInfo.Name + " is obsolete: " + attribute.Message;
                }
                currentObject = propertyInfo.GetValue(currentObject);
                currentType = currentObject.GetType();
            }
            return null;
        }

        private Result ValidatePath(object startingPoint, IEnumerable<string> propertyPath) => FindProperty(startingPoint, propertyPath).Map();

        private Result<object> GetWorkingClass(Session session, string className)
        {
            switch (className.ToLower())
            {
                case "session":
                    return Result.Ok((object)session);
                case "settings":
                    return Result.Ok((object)_settingsAccessor.GetSettings());
                default:
                    return Result.Fail<object>(className + " isn't a known classname");
            }
        }

        private List<string> GetObjectNames(string line)
        {
            var properties = new List<string>();

            var regex = new Regex(@"(?<=\{).*?(?=\})");
            foreach (Match testMatch in regex.Matches(line))
                properties.Add(testMatch.Value);

            return properties;
        }

        private Result<object> FindProperty(object startingPoint, IEnumerable<string> propertyPath)
        {
            object currentObject = startingPoint;
            Type currentType = currentObject.GetType();
            for (var i = 0; i < propertyPath.Count(); i++)
            {
                var propertyInfo = currentType.GetProperty(propertyPath.ElementAt(i));
                if (propertyInfo == null)
                    return Result.Fail<object>(propertyPath.ElementAt(i) + " was not a property found on object " + currentType.Name);
                currentObject = propertyInfo.GetValue(currentObject);
                currentType = currentObject.GetType();
            }
            return Result.Ok(currentObject);
        }

        private readonly ISettingsAccessor _settingsAccessor;
    }
}
