using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Data.RiskyPick;

namespace TeaseAI.Common
{
    /// <summary>
    /// Main engine of the program. 
    /// </summary>
    public class Session
    {
        #region Properties
        public DommePersonality Domme { get; set; }
        public SubPersonality Sub { get; set; }
        public List<DommePersonality> Glitter { get; private set; }
        public Stack<Script> Scripts { get; private set; }
        public Script CurrentScript => (Scripts.Count == 0) ? default(Script) : Scripts.Peek();
        public SessionPhase Phase { get; set; }
        public int TimeRemaining { get; set; }
        public RiskyPickGameBoard GameBoard { get; set; }

        /// <summary>
        /// Used to pause reading the script. some commands do this instead of using @wait, etc.
        /// </summary>
        public bool IsScriptPaused { get; set; }
        #endregion

        public Session(DommePersonality domme, SubPersonality sub)
        {
            Domme = domme;
            Sub = sub;
            Scripts = new Stack<Script>();
            Glitter = new List<DommePersonality>();

            IsFirstRound = false;
            IsBeforeTease = false;

            IsOrgasmRuined = false;
        }

        /// <summary>
        /// Perform a deep copy of session object
        /// </summary>
        /// <returns></returns>
        public Session Clone()
        {
            var returnValue = new Session(Domme.Clone(), Sub.Clone())
            {

                IsFirstRound = IsFirstRound,
                IsScriptPaused = IsScriptPaused,
                IsOrgasmRuined = IsOrgasmRuined,
                IsBeforeTease = IsBeforeTease,
                Phase = Phase,
                IsVideoPlaying = IsVideoPlaying,
                IsLongEdge = IsLongEdge,
                TimeRemaining = TimeRemaining,
                MaximumTaskTime = MaximumTaskTime,
                MinimumTaskTime = MinimumTaskTime,
                GameBoard = GameBoard?.Clone(),
                Glitter = Glitter.Select(dp => dp.Clone()).ToList(),
            };
            var scripts = this.Scripts.ToArray().ToList();
            scripts.Reverse();
            foreach (var scr in scripts)
            {
                returnValue.Scripts.Push(scr.Clone());
            }
            return returnValue;
        }

        #region deprecated
        [Obsolete("Use Phase instead")]
        public bool IsFirstRound { get; set; }
        [Obsolete("Use Sub.WillBeAllowedToOrgasm instead")]
        public bool IsOrgasmAllowed => Sub.WillBeAllowedToOrgasm.GetValueOrDefault();

        public bool IsOrgasmRuined { get; set; }
        [Obsolete("Use Phase instead")]
        public bool IsBeforeTease { get; set; }

        /// <summary>
        /// This is for tracking to / from the UI. is true when the UI starts a video, is false when the video stops
        /// </summary>
        public bool IsVideoPlaying { get; set; }
        public bool IsLongEdge { get; set; }
        public int MaximumTaskTime { get; set; }
        public int MinimumTaskTime { get; set; }
        #endregion
    }
}
