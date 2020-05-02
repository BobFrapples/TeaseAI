using System.Threading.Tasks;
using System.Windows;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.PersonalityEditor.Services
{
    public class NotifyUser : INotifyUser
    {
        public async Task ErrorMessageAsync(Error error)
        {
            MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            await Task.Delay(1);
        }

        public void ModalMessage(string message)
        {
            MessageBox.Show(message);
        }

        public async Task ModalMessageAsync(string message)
        {
            MessageBox.Show(message);
            await Task.Delay(1);
        }
    }
}
