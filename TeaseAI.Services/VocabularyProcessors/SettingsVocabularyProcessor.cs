using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.VocabularyProcessors
{
    /// <summary>
    /// Replace Vocabulary with session variables
    /// </summary>
    public class SettingsVocabularyProcessor : BaseVocabularyProcessor
    {
        protected override Dictionary<string, Func<Session, string, string>> Vocabulary { get; set; }

        private readonly ISettingsAccessor _settingsAccesor;

        public SettingsVocabularyProcessor(ISettingsAccessor settingsAccessor)
        {
            Vocabulary = new Dictionary<string, Func<Session, string, string>>
            {
                { @"#Settings\.(.*)+", ProcessLine }
            };
            _settingsAccesor = settingsAccessor;
        }

        private string ProcessLine(Session session, string line)
        {
            var settings = _settingsAccesor.GetSettings();
            var propertyNames = line.Split('.');
            object currentObject = settings;
            Type currentType = currentObject.GetType();
            for (var i = 1; i < propertyNames.Count(); i++)
            {
                var propertyInfo = currentType.GetProperty(propertyNames[i]);
                currentObject = propertyInfo.GetValue(currentObject);
                currentType = currentObject.GetType();
            }
            return currentObject.ToString();
        }
    }
}
