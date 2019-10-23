using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

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
        public string PointInSession
        {
            get
            {
                if (IsFirstRound)
                    return Response.FirstRound;
                return Response.BeforeTease;
            }
        }
        public int TimeRemaining { get; set; }
        #endregion

        public Session(DommePersonality domme, SubPersonality sub)
        {
            Domme = domme;
            Sub = sub;
            Scripts = new Stack<Script>();
            Glitter = new List<DommePersonality>();

            IsFirstRound = false;
            IsBeforeTease = false;

            IsOrgasmAllowed = false;
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

                IsFirstRound = this.IsFirstRound,
                IsOrgasmAllowed = this.IsOrgasmAllowed,
                IsOrgasmRuined = this.IsOrgasmRuined,
                IsBeforeTease = this.IsBeforeTease,
                Phase = this.Phase,
                IsVideoPlaying = this.IsVideoPlaying,
                IsLongEdge = this.IsLongEdge,
                TimeRemaining = this.TimeRemaining,
                MaximumTaskTime = MaximumTaskTime,
                MinimumTaskTime = MinimumTaskTime,
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
        public bool IsFirstRound { get; set; }
        public bool IsOrgasmAllowed { get; set; }

        public bool IsOrgasmRuined { get; set; }
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
