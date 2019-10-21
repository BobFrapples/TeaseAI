using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface ISlideShowNavigationService
    {
        /// <summary>
        /// Advance the slide show to the next image. 
        /// </summary>
        /// <param name="imageSlideShow"></param>
        /// <returns></returns>
        Result<ImageSlideShow> AdvanceSlideShow(ImageSlideShow imageSlideShow);

        /// <summary>
        /// Move the slideshow to a random image
        /// </summary>
        /// <param name="imageSlideShow"></param>
        /// <returns></returns>
        Result<ImageSlideShow> MoveToRandomImage(ImageSlideShow imageSlideShow);

        /// <summary>
        /// Moves the slideshow either randomly based on the slideshow, then by <paramref name="moveForward"/> 
        /// </summary>
        /// <param name="imageSlideShow"></param>
        /// <returns></returns>
        Result<ImageSlideShow> MoveSlideShow(ImageSlideShow imageSlideShow, bool moveForward );

        /// <summary>
        /// rewind the slideshow to the previous image
        /// </summary>
        /// <param name="imageSlideShow"></param>
        /// <returns></returns>
        Result<ImageSlideShow> RewindSlideShow(ImageSlideShow imageSlideShow);
    }
}
