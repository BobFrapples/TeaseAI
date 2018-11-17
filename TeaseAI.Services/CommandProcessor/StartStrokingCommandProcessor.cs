using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class StartStrokingCommandProcessor : CommandProcessorBase
    {
        public StartStrokingCommandProcessor(IVariableAccessor variableAccessor)
        {
            _variableAccessor = variableAccessor;
        }

        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.StartStroking, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.StartStroking);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();

            // deliberately ignore the return value from this set variable, if it happens, great
            // if it fails, I'm not losing sleep
            if (!_variableAccessor.DoesExist(workingSession.Domme, SystemVariable.FirstRun))
                _variableAccessor.SetVariable(workingSession.Domme, SystemVariable.FirstRun, DateTime.Now.ToString());

            var updateRound = _variableAccessor.GetVariable(workingSession.Domme, SystemVariable.StrokeRound)
                .OnSuccess(rnd => _variableAccessor.SetVariable(workingSession.Domme, SystemVariable.StrokeRound, (int.Parse(rnd) + 1).ToString()))
                .OnSuccess(() => _variableAccessor.GetVariable(workingSession.Domme, SystemVariable.StrokeRound));
            if (updateRound.IsFailure)
                return Result.Fail<Session>(updateRound.Error);

            workingSession.IsFirstRound = updateRound.Value == "1";

            // This is web toy and glitter contact stuff
            //        If FrmSettings.TBWebStart.Text <> "" Then
            //Try
            //                FrmSettings.WebToy.Navigate(FrmSettings.TBWebStart.Text)
            //            Catch
            //            End Try
            //        End If
            //        If inputString.Contains("@Contact1") Then ssh.Contact1Stroke = True
            //        If inputString.Contains("@Contact2") Then ssh.Contact2Stroke = True
            //        If inputString.Contains("@Contact3") Then ssh.Contact3Stroke = True
            // sub left early stuff went to session start
            // I'm not sure what this is for yet.
            //StrokePace = ssh.randomizer.Next(NBMaxPace.Value, NBMinPace.Value + 1)
            //StrokePace = 50 * Math.Round(StrokePace / 50)

            //If ssh.WorshipMode = True Then
            //    StrokePace = NBMinPace.Value
            //    ssh.StrokeSlowest = True
            //End If
            workingSession.IsBeforeTease = false;
            workingSession.Sub.IsStroking = true;
            workingSession.Sub.StrokePace = GetStrokePace();

            workingSession.TimeRemaining = GetStrokeTime(workingSession);

            OnCommandProcessed(workingSession);

            return Result.Ok(workingSession);
        }

        /// <summary>
        /// Get the Speed at which the sub should be stroking.
        /// </summary>
        /// <returns>value between 200 and 1000 in multiples of 50, stroke faster for lower numbers</returns>
        private int GetStrokePace()
        {
            var pace = new Random().Next(200, 1000);
            // This just
            return 50 * (int)Math.Round(pace / 50m);
        }

        private int GetStrokeTime(Session workingSession)
        {
            // TODO: Add user ranges and worship mode.
            if (workingSession.Domme.DomLevel == DomLevel.Gentle)
                return new Random().Next(1, 3) * 60;
            if (workingSession.Domme.DomLevel == DomLevel.Lenient)
                return new Random().Next(1, 4) * 60;
            if (workingSession.Domme.DomLevel == DomLevel.Tease)
                return new Random().Next(3, 6) * 60;
            if (workingSession.Domme.DomLevel == DomLevel.Rough)
                return new Random().Next(4, 8) * 60;
            if (workingSession.Domme.DomLevel == DomLevel.Sadistic)
                return new Random().Next(5, 11) * 60;
            throw new ArgumentOutOfRangeException(nameof(workingSession.Domme.DomLevel));
            //    If FrmSettings.CBTauntCycleDD.Checked = True Then
            //    If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick = ssh.randomizer.Next(1, 3) * 60
            //    If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick = ssh.randomizer.Next(1, 4) * 60
            //    If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick = ssh.randomizer.Next(3, 6) * 60
            //    If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick = ssh.randomizer.Next(4, 8) * 60
            //    If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick = ssh.randomizer.Next(5, 11) * 60

            //    If ssh.WorshipMode = True Then
            //        If FrmSettings.DominationLevel.Value = 1 Then ssh.StrokeTick = 180
            //        If FrmSettings.DominationLevel.Value = 2 Then ssh.StrokeTick = 240
            //        If FrmSettings.DominationLevel.Value = 3 Then ssh.StrokeTick = 360
            //        If FrmSettings.DominationLevel.Value = 4 Then ssh.StrokeTick = 480
            //        If FrmSettings.DominationLevel.Value = 5 Then ssh.StrokeTick = 600
            //    End If

            //Else
            //    ssh.StrokeTick = ssh.randomizer.Next(FrmSettings.NBTauntCycleMin.Value * 60, FrmSettings.NBTauntCycleMax.Value * 60)
            //    If ssh.WorshipMode = True Then ssh.StrokeTick = FrmSettings.NBTauntCycleMax.Value * 60
            //End If
        }

        private readonly IVariableAccessor _variableAccessor;
    }
}
