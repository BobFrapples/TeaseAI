using System;

namespace TeaseAI.Services.CommandDetection
{
    public class HolidayDetection
    {
        public bool ShouldKeepLine(string inputString, DateTime today)
        {
            if (inputString.ToLower().Contains("@valentinesday") && (today.Month != 2 || today.Day != 14))
                return false;
            if (inputString.ToLower().Contains("@christmaseve") && (today.Month != 12 || today.Day != 24))
                return false;
            if (inputString.ToLower().Contains("@christmasday") && (today.Month != 12 || today.Day != 25))
                return false;
            if (inputString.ToLower().Contains("@newyearseve") && (today.Month != 12 || today.Day != 31))
                return false;
            if (inputString.ToLower().Contains("@newyearsday") && (today.Month != 1 || today.Day != 1))
                return false;

            return true;
        }
    }
}
