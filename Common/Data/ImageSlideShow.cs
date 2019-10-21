using System.Collections.Generic;
using System.Linq;

namespace TeaseAI.Common.Data
{
    public class ImageSlideShow
    {
        public bool IsRandom {get;set;}
        public List<string> ImageList { get; set; } = new List<string>();
        public int ImageIndex { get; set; }

        public ImageSlideShow Clone()
        {
            return new ImageSlideShow
            {
                IsRandom = IsRandom,
                ImageList = ImageList.ToList(),
                ImageIndex = ImageIndex,
            };
        }
    }
}
