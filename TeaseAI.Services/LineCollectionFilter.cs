using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class LineCollectionFilter : ILineCollectionFilter
    {
        public List<string> FilterLines(Session session, IEnumerable<string> lines)
        {
            var filteredLines = lines.ToList();

            filteredLines = filteredLines.Select(line => FilterLine(Keyword.LongEdgeFilter, line, session.IsLongEdge))
                .Select(line => FilterLine(Keyword.SmallCockFilter, line, session.Sub.CockSize < session.Domme.CockSmallLimit))
                .Select(line => FilterLine(Keyword.BigCockFilter, line, session.Sub.CockSize > session.Domme.CockBigLimit))
                .Select(line => FilterLine(Keyword.Crazy, line, session.Domme.IsCrazy))
                .Select(line => FilterLine(Keyword.Vulgar, line, session.Domme.IsVulgar))
                .Select(line => FilterLine(Keyword.Supremacist, line, session.Domme.IsSupremacist))
                .Select(line => FilterLine(Keyword.SubYoungFilter, line, session.Sub.Age < session.Domme.SubAgeYoungLimit))
                .Select(line => FilterLine(Keyword.SubOldFilter, line, session.Sub.Age > session.Domme.SubAgeOldLimit))
                .Select(line => FilterLine(Keyword.SubInChastity, line, session.Sub.InChastity))
                .Select(line => FilterLine(Keyword.SelfYoungFilter, line, session.Domme.Age < session.Domme.AgeYoungLimit))
                .Select(line => FilterLine(Keyword.SelfOldFilter, line, session.Domme.Age > session.Domme.AgeOldLimit))
                .Select(line => FilterLine(Keyword.CupSizeA, line, session.Domme.CupSize == CupSize.ACup))
                .Select(line => FilterLine(Keyword.CupSizeB, line, session.Domme.CupSize == CupSize.BCup))
                .Select(line => FilterLine(Keyword.CupSizeC, line, session.Domme.CupSize == CupSize.CCup))
                .Select(line => FilterLine(Keyword.CupSizeD, line, session.Domme.CupSize == CupSize.DCup))
                .Select(line => FilterLine(Keyword.CupSizeDD, line, session.Domme.CupSize == CupSize.DdCup))
                .Select(line => FilterLine(Keyword.CupSizeDDD, line, session.Domme.CupSize == CupSize.DddCup))
                .Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
            return filteredLines;
        }

        string FilterLine(string keyword, string line, bool isMatch)
        {
            if (!isMatch && line.Contains(keyword))
                return string.Empty;
            return line.Replace(keyword, "");
        }
    }
}
