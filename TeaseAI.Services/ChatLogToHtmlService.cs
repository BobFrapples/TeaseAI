using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class ChatLogToHtmlService : IChatLogToHtmlService
    {
        public string CreateHtml(List<ChatMessage> chatLog, Dictionary<string, ChatMessagePreferences> messagePreferences)
        {
            var showTimeStamp = true;
            var htmlOut = "<body>";
            var previousSender = string.Empty;
            foreach (var chatMessage in chatLog)
            {
                var timeTag = showTimeStamp ? string.Format("<font face='Cambria' size='2' color='DimGray'>{0}</font>", chatMessage.TimeStamp.ToString("hh:mm tt ")) : "";
                var senderTag = (previousSender == chatMessage.Sender)
                    ? string.Empty
                    : string.Format("<font face='Cambria' size='3' font color='{0}'><b>{1}: </b></font>", messagePreferences[chatMessage.Sender].SenderColor, chatMessage.Sender);

                var messageTag = string.Format("<font face='{0}' size='{1}' color='{2}'>{3}</font>"
                        , messagePreferences[chatMessage.Sender].FontName
                        , messagePreferences[chatMessage.Sender].FontSize.ToString()
                        , messagePreferences[chatMessage.Sender].FontColor
                        , chatMessage.Message);

                htmlOut += string.Format("<div style='word-wrap:break-word;' bgcolor='{0}'>{1}{2}{3}</div>"
                    , messagePreferences[chatMessage.Sender].BackgroundColor
                    , timeTag
                    , senderTag
                    , messageTag
                    );
                previousSender = chatMessage.Sender;
            }
            htmlOut += "</body>";
            return htmlOut;
        }
    }
}
