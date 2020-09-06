using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services
{
    public class TimeService : ITimeService
    {
        public TimeService(ISettingsAccessor settingsAccessor)
        {
            _settingsAccessor = settingsAccessor;
        }

        public string GetGeneralTime()
        {
            var settings = _settingsAccessor.GetSettings();
            var wakeUpTime = new TimeSpan(7, 0, 0);
            var timeDiff = DateTime.Now.TimeOfDay - wakeUpTime;
            if (timeDiff.Hours >= -2 && timeDiff.Hours < 5)
                return "Morning";

            if (timeDiff.Hours >= 5 && timeDiff.Hours < 10)
                return "Afternoon";

            if (timeDiff.Hours >= 10 && timeDiff.Hours < 20)
                return "Evening";

            return "Night";
        }

        public DateTime GetTime() => DateTime.Now;

        private readonly ISettingsAccessor _settingsAccessor;
    }
}
