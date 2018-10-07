using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandDetection
{
    public class SubPersonalityDetection
    {
        public bool ShouldKeepLine(string inputString, SubPersonality sub)
        {
            if (inputString.ToLower().Contains("@subbirthday")
                && DateTime.Now.Month != sub.Birthday.Month
                && DateTime.Now.Day != sub.Birthday.Day)
                return false;

            if (inputString.Contains("@SubCircumcised") && !sub.IsCircumsized)
                return false;
            if (inputString.Contains("@SubNotCircumcised") && sub.IsCircumsized)
                return false;
            if (inputString.Contains("@SubPierced") && !sub.IsCockPierced)
                return false;
            if (inputString.Contains("@SubNotPierced") && sub.IsCockPierced)
                return false;


            if (inputString.Contains("@CockTorture") && !sub.Kinks.Contains(Kink.CockTorture))
                return false;
            if (inputString.Contains("@BallTorture") && !sub.Kinks.Contains(Kink.BallTorture))
                return false;

            if (inputString.Contains("@BallTorture0") && sub.BallsTortureLevel != 0)
                return false;
            if (inputString.Contains("@BallTorture1") && sub.BallsTortureLevel != 1)
                return false;
            if (inputString.Contains("@BallTorture2") && sub.BallsTortureLevel != 2)
                return false;
            if (inputString.Contains("@BallTorture3") && sub.BallsTortureLevel != 3)
                return false;
            if (inputString.Contains("@BallTorture4+") && sub.BallsTortureLevel < 4)
                return false;
            if (inputString.Contains("@CockTorture0") && sub.CockTortureLevel != 0)
                return false;
            if (inputString.Contains("@CockTorture1") && sub.CockTortureLevel != 1)
                return false;
            if (inputString.Contains("@CockTorture2") && sub.CockTortureLevel != 2)
                return false;
            if (inputString.Contains("@CockTorture3") && sub.CockTortureLevel != 3)
                return false;
            if (inputString.Contains("@CockTorture4+") && sub.CockTortureLevel < 4)
                return false;

            if (inputString.Contains("@HasChastity") && !sub.ToyBox.Contains(Toy.ChastityDevice))
                return false;
            if (inputString.Contains("@DoesNotHaveChastity") && sub.ToyBox.Contains(Toy.ChastityDevice))
                return false;
            if (inputString.Contains("@ChastityPA") && !sub.ToyBox.Contains(Toy.ChastityDeviceRequiresPiercing))
                return false;
            if (inputString.Contains("@ChastitySpikes") && !sub.ToyBox.Contains(Toy.ChastityDeviceWithSpikes))
                return false;

            if (inputString.Contains("@InChastity") && !sub.InChastity)
                return false;
            if (inputString.Contains("@NotInChastity") && sub.InChastity)
                return false;

            return true;
        }
    }
}
