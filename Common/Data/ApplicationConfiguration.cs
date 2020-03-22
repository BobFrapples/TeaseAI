using System.Collections.Generic;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// Configuration options for any application
    /// </summary>
    public class ApplicationConfiguration
    {
        private List<ImageContainer> _imageContainers;

        /// <summary>
        /// Directory where all personalities are stored
        /// </summary>
        public string BaseDataFolder { get; set; }

        /// <summary>
        /// Configuration options for image containers 
        /// </summary>
        public List<ImageContainer> ImageContainers
        {
            get => _imageContainers ?? (_imageContainers = new List<ImageContainer>());
            set => _imageContainers = value;
        }
    }
}
