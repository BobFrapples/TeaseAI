using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandDetection
{
    public class DommePersonalityDetection
    {
        public bool ShouldKeepLine(string inputString, DommePersonality domme)
        {
            var lineService = new LineService();

            if (inputString.Contains(Keyword.AllowsOrgasm) &&
                !lineService.GetParenData(inputString, Keyword.AllowsOrgasm).GetResultOrDefault(new List<string>()).Contains(domme.AllowsOrgasms.ToString()))
                return false;
            if (inputString.ToLower().Contains("@alwaysallowsorgasm") && domme.AllowsOrgasms != AllowsOrgasms.Always) return false;
            if (inputString.ToLower().Contains("@oftenallowsorgasm") && domme.AllowsOrgasms != AllowsOrgasms.Often) return false;
            if (inputString.ToLower().Contains("@sometimesallowsorgasm") && domme.AllowsOrgasms != AllowsOrgasms.Sometimes) return false;
            if (inputString.ToLower().Contains("@rarelyallowsorgasm") && domme.AllowsOrgasms != AllowsOrgasms.Rarely) return false;
            if (inputString.ToLower().Contains("@neverallowsorgasm") && domme.AllowsOrgasms != AllowsOrgasms.Never) return false;
            if (inputString.ToLower().Contains("@notalwaysallowsorgasm") && domme.AllowsOrgasms == AllowsOrgasms.Always) return false;
            if (inputString.ToLower().Contains("@notneverallowsorgasm") && domme.AllowsOrgasms == AllowsOrgasms.Never) return false;


            if (inputString.Contains(Keyword.RuinsOrgasm) &&
                !lineService.GetParenData(inputString, Keyword.RuinsOrgasm).GetResultOrDefault(new List<string>()).Contains(domme.RuinsOrgasms.ToString()))
                return false;
            if (inputString.ToLower().Contains("@alwaysruinsorgasm") && domme.RuinsOrgasms != RuinsOrgasms.Always) return false;
            if (inputString.ToLower().Contains("@oftenruinsorgasm") && domme.RuinsOrgasms != RuinsOrgasms.Often) return false;
            if (inputString.ToLower().Contains("@sometimesruinsorgasm") && domme.RuinsOrgasms != RuinsOrgasms.Sometimes) return false;
            if (inputString.ToLower().Contains("@rarelyruinsorgasm") && domme.RuinsOrgasms != RuinsOrgasms.Rarely) return false;
            if (inputString.ToLower().Contains("@neverruinsorgasm") && domme.RuinsOrgasms != RuinsOrgasms.Never) return false;
            if (inputString.ToLower().Contains("@notalwaysruinsorgasm") && domme.RuinsOrgasms == RuinsOrgasms.Always) return false;
            if (inputString.ToLower().Contains("@notneverruinsorgasm") && domme.RuinsOrgasms == RuinsOrgasms.Never) return false;


            // Filter based on apathy level
            if (inputString.Contains(Keyword.ApathyLevel) &&
                !lineService.GetParenData(inputString, Keyword.ApathyLevel).GetResultOrDefault(new List<string>()).Contains(domme.ApathyLevel.ToString()))
                return false;
            if (inputString.Contains(Keyword.ApathyLevelNum + "1") && domme.ApathyLevel != ApathyLevel.Cautious) return false;
            if (inputString.Contains(Keyword.ApathyLevelNum + "2") && domme.ApathyLevel != ApathyLevel.Caring) return false;
            if (inputString.Contains(Keyword.ApathyLevelNum + "3") && domme.ApathyLevel != ApathyLevel.Moderate) return false;
            if (inputString.Contains(Keyword.ApathyLevelNum + "4") && domme.ApathyLevel != ApathyLevel.Cruel) return false;
            if (inputString.Contains(Keyword.ApathyLevelNum + "5") && domme.ApathyLevel != ApathyLevel.Merciless) return false;

            // Domme Level
            if (inputString.ToLower().Contains("@DommeLevel(") &&
                !lineService.GetParenData(inputString, "@DommeLevel(").GetResultOrDefault(new List<string>()).Contains(domme.DomLevel.ToString()))
                return false;
            if (inputString.ToLower().Contains("@dommelevel1") && domme.DomLevel != DomLevel.Gentle) return false;
            if (inputString.ToLower().Contains("@dommelevel2") && domme.DomLevel != DomLevel.Lenient) return false;
            if (inputString.ToLower().Contains("@dommelevel3") && domme.DomLevel != DomLevel.Tease) return false;
            if (inputString.ToLower().Contains("@dommelevel4") && domme.DomLevel != DomLevel.Rough) return false;
            if (inputString.ToLower().Contains("@dommelevel5") && domme.DomLevel != DomLevel.Sadistic) return false;

            // Cup size
            if (inputString.Contains("@Cup(") &&
                !lineService.GetParenData(inputString, "@Cup(").GetResultOrDefault(new List<string>()).Contains(domme.CupSize.ToString()))
                return false;
            if (inputString.ToLower().Contains("@acup") && domme.CupSize != CupSize.ACup) return false;
            if (inputString.ToLower().Contains("@bcup") && domme.CupSize != CupSize.BCup) return false;
            if (inputString.ToLower().Contains("@ccup") && domme.CupSize != CupSize.CCup) return false;
            if (inputString.ToLower().Contains("@dcup") && domme.CupSize != CupSize.DCup) return false;
            if (inputString.ToLower().Contains("@ddcup") && domme.CupSize != CupSize.DdCup) return false;
            if (inputString.ToLower().Contains("@ddd+cup") && domme.CupSize != CupSize.DddCup) return false;

            // Personality flags
            if (inputString.ToLower().Contains(Keyword.Crazy.ToLower()) && !domme.IsCrazy) return false;
            if (inputString.ToLower().Contains(Keyword.Vulgar.ToLower()) && !domme.IsVulgar) return false;
            if (inputString.ToLower().Contains(Keyword.Degrading.ToLower()) && !domme.IsDegrading) return false;
            if (inputString.ToLower().Contains(Keyword.Supremacist.ToLower()) && !domme.IsSupremacist) return false;
            if (inputString.ToLower().Contains(Keyword.Sadistic.ToLower()) && !domme.IsSadistic) return false;

            // Domme Age
            if (inputString.ToLower().Contains("@selfyoung") && domme.Age > domme.AgeYoungLimit - 1) return false;
            if (inputString.ToLower().Contains("@selfold") && domme.Age > domme.AgeOldLimit + 1) return false;

            // Domme mood
            if (inputString.Contains("@GoodMood") && domme.MoodLevel <= domme.MoodHappy) return false;
            if (inputString.Contains("@BadMood") && domme.MoodLevel >= domme.MoodAngry) return false;
            if (inputString.Contains("@NeutralMood"))
                if (domme.MoodLevel > domme.MoodHappy || domme.MoodLevel < domme.MoodAngry) return false;


            if (inputString.ToLower().Contains("@dombirthday")
                && DateTime.Now.Month != domme.BirthDay.Month
                && DateTime.Now.Day != domme.BirthDay.Day)
                return false;

            return true;
        }
    }
}
