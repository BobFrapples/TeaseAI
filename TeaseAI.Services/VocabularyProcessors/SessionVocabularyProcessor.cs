using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;

namespace TeaseAI.Services.VocabularyProcessors
{
    /// <summary>
    /// Replace Vocabulary with session variables
    /// </summary>
    public class SessionVocabularyProcessor : BaseVocabularyProcessor
    {
        protected override Dictionary<string, Func<Session, string, string>> Vocabulary { get; set; }

        public SessionVocabularyProcessor()
        {
            Vocabulary = new Dictionary<string, Func<Session, string, string>>
            {
                { @"#Session\.(.*)+", ProcessLine }
            };
        }

        private string ProcessLine(Session session, string line)
        {
            var propertyNames = line.Split('.');
            object currentObject = session;
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
