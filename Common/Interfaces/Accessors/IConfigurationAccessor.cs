namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IConfigurationAccessor
    {
        /// <summary>
        /// The root folder for image / videos / all other data
        /// </summary>
         string GetBaseFolder();
    }
}
