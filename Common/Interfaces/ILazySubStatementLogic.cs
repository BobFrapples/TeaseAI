using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Interfaces
{
    /// <summary>
    /// Build statements used by lazy sub apps
    /// </summary>
    public interface ILazySubStatementLogic
    {
        /// <summary>
        /// Get a statement indicating yes
        /// </summary>
        /// <returns></returns>
        string GetAffirmative(Settings settings);

        /// <summary>
        /// Get a statement indicating no
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetNegative(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetOnTheEdge(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetSafeword(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetGreeting(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetLetMeCum(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetStop(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetStroke(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetSlowDown(Settings settings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        string GetSpeedUp(Settings settings);
    }
}
