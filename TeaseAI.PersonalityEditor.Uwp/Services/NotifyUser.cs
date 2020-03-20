using System;
using System.Threading.Tasks;
using TeaseAI.Common.Interfaces;
using Windows.UI.Popups;

namespace TeaseAI.PersonalityEditor.Services
{
    public class NotifyUser : INotifyUser
    {
        public void ModalMessage(string message)
        {
            throw new NotImplementedException();
        }

        public async Task ModalMessageAsync(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
