using TeaseAI.Common;

namespace TeaseAI.Services.CommandDetection
{
    public class SessionDetection
    {
        public bool ShouldKeepLine(string inputString, Session ssh)
        {
            // stroking / not stroking
            if ((inputString.Contains("@Stroking") || inputString.Contains("@SubStroking"))
                && !ssh.Sub.IsStroking)
                return false;

            if ((inputString.Contains("@NotStroking") || inputString.Contains("@SubNotStroking"))
                && ssh.Sub.IsStroking)
                return false;

            // Edging / not edging
            if ((inputString.Contains("@Edging") || inputString.Contains("@Edging"))
                && !ssh.IsEdging)
                return false;

            if ((inputString.Contains("@NotEdging") || inputString.Contains("@SubNotEdging"))
                && ssh.IsEdging)
                return false;

            // holding edge / not holding edge
            if ((inputString.Contains("@HoldingTheEdge") || inputString.Contains("@SubHoldingTheEdge"))
                && !ssh.IsHoldingTheEdge)
                return false;

            if ((inputString.Contains("@NotHoldingTheEdge") || inputString.Contains("@SubNotHoldingTheEdge"))
                && ssh.IsHoldingTheEdge)
                return false;

            if (inputString.ToLower().Contains("@firstround") &&
                !ssh.IsFirstRound)
                return false;

            if (inputString.ToLower().Contains("@notfirstround") &&
                ssh.IsFirstRound)
                return false;

            if (inputString.Contains("@OrgasmDenied") && ssh.IsOrgasmAllowed)
                return false;

            if (inputString.Contains("@OrgasmAllowed") && !ssh.IsOrgasmAllowed)
                return false;

            if (inputString.Contains("@OrgasmRuined") && !ssh.IsOrgasmRuined)
                return false;

            if (inputString.Contains("@BeforeTease") && !ssh.IsBeforeTease)
                return false;

            return true;
        }
    }
}
