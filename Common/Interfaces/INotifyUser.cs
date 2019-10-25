namespace TeaseAI.Common.Interfaces
{
    public interface INotifyUser
    {
        /// <summary>
        /// Write a modal message to the user
        /// </summary>
        /// <param name="message"></param>
        void ModalMessage(string message);
    }
}
