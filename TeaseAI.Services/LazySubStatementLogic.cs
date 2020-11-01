using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class LazySubStatementLogic : ILazySubStatementLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetAffirmative(Settings settings)
        {
            return "Yes" + GetHonorific(settings.Domme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetGreeting(Settings settings)
        {
             return "Hi" + GetHonorific(settings.Domme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetLetMeCum(Settings settings)
        {
            return "Please let me cum";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetNegative(Settings settings)
        {
            return "No" + GetHonorific(settings.Domme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetOnTheEdge(Settings settings)
        {
            return "On the edge";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetSafeword(Settings settings)
        {
            return settings.Sub.Safeword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetSlowDown(Settings settings)
        {
            return "May I slow down";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetSpeedUp(Settings settings)
        {
            return "May I speed up";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetStop(Settings settings)
        {
            return "Let me stop";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string GetStroke(Settings settings)
        {
            return "May I start stroking";
        }

        private string GetHonorific(DommeSettings dommeSettings)
        {
            if (dommeSettings.RequiresHonorific && dommeSettings.RequiresHonorificCapitalized)
                return " " + dommeSettings.Honorific.Replace(dommeSettings.Honorific[0], dommeSettings.Honorific[0].ToString().ToUpper()[0]);
            if (dommeSettings.RequiresHonorific)
                return " " + dommeSettings.Honorific;
            return string.Empty;
        }
    }
}
