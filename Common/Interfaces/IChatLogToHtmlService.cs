using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Interfaces
{
    public interface IChatLogToHtmlService
    {
        string CreateHtml(List<ChatMessage> chatLog, Dictionary<string, ChatMessagePreferences> messagePreferences);
    }
}
