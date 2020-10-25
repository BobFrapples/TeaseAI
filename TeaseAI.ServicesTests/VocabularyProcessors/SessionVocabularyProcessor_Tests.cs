using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Services.VocabularyProcessors;

namespace TeaseAI.ServicesTests.VocabularyProcessors
{
    [TestClass]
    public class SessionVocabularyProcessor_Tests
    {
        SessionVocabularyProcessor _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new SessionVocabularyProcessor();
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldReturnDommeAge()
        {
            var testStrings = new List<Tuple<string, string>>
            {
                Tuple.Create("#Session.Domme.Age", "30"),
                Tuple.Create(" #Session.Domme.Age ", " 30 "),
                Tuple.Create("I'm #Session.Domme.Age today", "I'm 30 today"),
            };

            var session = new Session(new DommePersonality(), new SubPersonality());
            session.Domme.Age = 30;

            foreach (var testString in testStrings)
            {
                var actual = _service.ReplaceVocabulary(session, testString.Item1);

                Assert.AreEqual(testString.Item2, actual, testString.Item1);
            }
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldReturnSubCockTortureLevel()
        {
            var testStrings = new List<Tuple<string, string>>
            {
                Tuple.Create("#Session.Sub.CockTortureLevel", "3"),
                Tuple.Create(" #Session.Sub.CockTortureLevel ", " 3 "),
                Tuple.Create("Torture #Session.Sub.CockTortureLevel Me", "Torture 3 Me"),
            };

            var session = new Session(new DommePersonality(), new SubPersonality());
            session.Sub.CockTortureLevel = TortureLevel.Create(3).Value; 

            foreach (var testString in testStrings)
            {
                var actual = _service.ReplaceVocabulary(session, testString.Item1);

                Assert.AreEqual(testString.Item2, actual, testString.Item1);
            }
        }
    }
}
