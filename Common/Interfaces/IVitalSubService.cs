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
        Result<List<string>> GetVitalSubResponses(DommePersonality domme, bool didVitalSubFail);

    }
}
