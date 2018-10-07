namespace TeaseAI.Services.CommandDetection
{
    /// <summary>
    /// Detect commands based around videos
    /// </summary>
    public class VideoDetection
    {
        public bool ShouldKeepLine(string inputString, bool isVideoTease, string videoType)
        {
            if (inputString.Contains("@VideoHardcore") && ((!isVideoTease) ||videoType != "Hardcore"))
                return false;

            if (inputString.Contains("@VideoSoftcore") && ((!isVideoTease) || videoType != "Softcore"))
                return false;

            if (inputString.Contains("@VideoLesbian") && ((!isVideoTease) || videoType != "Lesbian"))
                return false;

            if (inputString.Contains("@VideoBlowjob") && ((!isVideoTease) || videoType != "Blowjob"))
                return false;

            if (inputString.Contains("@VideoFemdom") && ((!isVideoTease) || videoType != "Femdom"))
                return false;

            if (inputString.Contains("@VideoFemsub") && ((!isVideoTease) || videoType != "Femsub"))
                return false;

            if (inputString.Contains("@VideoGeneral") && ((!isVideoTease) || videoType != "General"))
                return false;

            if (inputString.Contains("@VideoHardcoreDomme") && ((!isVideoTease) || videoType != "HardcoreD"))
                return false;

            if (inputString.Contains("@VideoSoftcoreDomme") && ((!isVideoTease) || videoType != "SoftcoreD"))
                return false;

            if (inputString.Contains("@VideoLesbianDomme") && ((!isVideoTease) || videoType != "LesbianD"))
                return false;

            if (inputString.Contains("@VideoBlowjobDomme") && ((!isVideoTease) || videoType != "BlowjobD"))
                return false;

            if (inputString.Contains("@VideoFemdomDomme") && ((!isVideoTease) || videoType != "FemdomD"))
                return false;

            if (inputString.Contains("@VideoFemsubDomme") && ((!isVideoTease) || videoType != "FemsubD"))
                return false;

            if (inputString.Contains("@VideoGeneralDomme") && ((!isVideoTease) || videoType != "GeneralD"))
                return false;

            return true;
        }
    }
}
