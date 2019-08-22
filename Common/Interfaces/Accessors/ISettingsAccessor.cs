using System.Collections.Generic;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface ISettingsAccessor
    {
        List<string> GetGreetings();
        string GetDommePersonality();
        string GetDommeAvatarImageName();
        string GetDommeName();
        string GetSubName();
    }
}
