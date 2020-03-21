using System.Threading.Tasks;

namespace TeaseAI.Common.Interfaces
{
    public interface INotifyUser
    {
        /// <summary>
        /// Write a modal message to the user
        /// </summary>
        /// <param name="message"></param>
        void ModalMessage(string message);

        /// <summary>
        /// Async version of Modal message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ModalMessageAsync(string message);
    }
}
