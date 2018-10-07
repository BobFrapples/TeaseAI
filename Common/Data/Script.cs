using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// This is a full script, including the Domme's location in it
    /// </summary>
    public class Script
    {
        public ScriptMetaData MetaData { get; private set; }
        public ReadOnlyCollection<string> Lines { get; private set; }
        private int _lineNumber;
        public int LineNumber
        {
            get
            {
                return _lineNumber;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Line number should start at 0");
                _lineNumber = value;
            }
        }
        public string CurrentLine => Lines[LineNumber];

        public Script(ScriptMetaData metaData, IList<string> lines)
        {
            MetaData = metaData;
            Lines = new ReadOnlyCollection<string>(lines);
            LineNumber = 0;
        }

        /// <summary>
        /// performs a deep copy of Script
        /// </summary>
        /// <returns></returns>
        public Script Clone()
        {
            return new Script(MetaData.Clone(), Lines.ToList())
            {
                LineNumber = LineNumber,
            };
        }
    }
}
