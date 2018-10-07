namespace TeaseAI.Services.CommandDetection
{
    /// <summary>
    /// match tags used for images 
    /// </summary>
    public class TagDetection
    {
        public bool ShouldKeepLine(string inputString, string mediaTags)
        {
            if (inputString.ToLower().Contains("@tagface") && !mediaTags.ToLower().Contains("tagface"))
                return false;
            if (inputString.ToLower().Contains("@tagboobs") && !mediaTags.ToLower().Contains("tagboobs"))
                return false;
            if (inputString.ToLower().Contains("@tagpussy") && !mediaTags.ToLower().Contains("tagpussy"))
                return false;
            if (inputString.ToLower().Contains("@tagass") && !mediaTags.ToLower().Contains("tagass"))
                return false;
            if (inputString.ToLower().Contains("@tagfeet") && !mediaTags.ToLower().Contains("tagfeet"))
                return false;
            if (inputString.ToLower().Contains("@taglegs") && !mediaTags.ToLower().Contains("taglegs"))
                return false;

            if (inputString.ToLower().Contains("@tagmasturbating") && !mediaTags.ToLower().Contains("tagmasturbating"))
                return false;
            if (inputString.ToLower().Contains("@tagsucking") && !mediaTags.ToLower().Contains("tagsucking"))
                return false;
            if (inputString.ToLower().Contains("@tagfullydressed") && !mediaTags.ToLower().Contains("tagfullydressed"))
                return false;
            if (inputString.ToLower().Contains("@taghalfdressed") && !mediaTags.ToLower().Contains("taghalfdressed"))
                return false;
            if (inputString.ToLower().Contains("@taggarmentcovering") && !mediaTags.ToLower().Contains("taggarmentcovering"))
                return false;
            if (inputString.ToLower().Contains("@taghandscovering") && !mediaTags.ToLower().Contains("taghandscovering"))
                return false;
            if (inputString.ToLower().Contains("@tagnaked") && !mediaTags.ToLower().Contains("tagnaked"))
                return false;
            if (inputString.ToLower().Contains("@tagsideview") && !mediaTags.ToLower().Contains("tagsideview"))
                return false;
            if (inputString.ToLower().Contains("@tagcloseup") && !mediaTags.ToLower().Contains("tagcloseup"))
                return false;
            if (inputString.ToLower().Contains("@tagpiercing") && !mediaTags.ToLower().Contains("tagpiercing"))
                return false;
            if (inputString.ToLower().Contains("@tagsmiling") && !mediaTags.ToLower().Contains("tagsmiling"))
                return false;
            if (inputString.ToLower().Contains("@tagglaring") && !mediaTags.ToLower().Contains("tagglaring"))
                return false;
            if (inputString.ToLower().Contains("@taggarment") && !mediaTags.ToLower().Contains("taggarment"))
                return false;
            if (inputString.ToLower().Contains("@tagunderwear") && !mediaTags.ToLower().Contains("tagunderwear"))
                return false;
            if (inputString.ToLower().Contains("@tagtattoo") && !mediaTags.ToLower().Contains("tagtattoo"))
                return false;
            if (inputString.ToLower().Contains("@tagsextoy") && !mediaTags.ToLower().Contains("tagsextoy"))
                return false;
            if (inputString.ToLower().Contains("@tagfurniture") && !mediaTags.ToLower().Contains("tagfurniture"))
                return false;

            return true;
        }
    }
}
