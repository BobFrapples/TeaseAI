using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IVitalSubService
    {
        List<string> GetEatenFood();
        void SaveEatenFood(IEnumerable<string> foodknown);

        void SaveKnownFoodItems(IEnumerable<string> foodknown);
        List<string> GetKnownFoodItems();

        void SaveAssignedExercises(List<ExerciseAssignment> exerciseAssignments);
        List<ExerciseAssignment> GetAssignedExercises();

        ExerciseAssignment GetExerciseFromDomme(Session session, string file);

        /// <summary>
        /// Submit the daily report, get back a response.
        /// On success, will clear daily exercises / calories
        /// </summary>
        /// <param name="domme"></param>
        /// <returns></returns>
        Result<Script> SubmitData(DommePersonality domme);

        Result<List<string>> GetVitalSubResponses(DommePersonality domme, bool didVitalSubFail);

    }
}
