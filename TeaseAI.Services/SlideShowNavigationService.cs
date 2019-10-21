using System;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class SlideShowNavigationService : ISlideShowNavigationService
    {
        private RandomNumberService _randomNumberService;

        public SlideShowNavigationService()
        {
            _randomNumberService = new RandomNumberService();
        }

        public Result<ImageSlideShow> MoveToRandomImage(ImageSlideShow imageSlideShow)
        {
            var workingSlideShow = imageSlideShow.Clone();
            workingSlideShow.ImageIndex = _randomNumberService.Roll(0, imageSlideShow.ImageList.Count - 1);
            return Result.Ok(workingSlideShow);
        }

        public Result<ImageSlideShow> AdvanceSlideShow(ImageSlideShow imageSlideShow)
        {
            return MoveSlideShow(imageSlideShow, 1);
        }

        public Result<ImageSlideShow> MoveSlideShow(ImageSlideShow imageSlideShow, bool moveForward)
        {
            if (imageSlideShow.IsRandom)
                return MoveToRandomImage(imageSlideShow);
            if (moveForward)
                return MoveSlideShow(imageSlideShow, 1);
            return MoveSlideShow(imageSlideShow, -1);
        }

        private Result<ImageSlideShow> MoveSlideShow(ImageSlideShow imageSlideShow, int moveVector)
        {
            var workingSlideShow = imageSlideShow.Clone();

            var newIndex = workingSlideShow.ImageIndex + moveVector;
            if (newIndex < 0)
                return Result.Fail<ImageSlideShow>("At the start of this slideshow");
            if (newIndex == imageSlideShow.ImageList.Count)
                return Result.Fail<ImageSlideShow>("At the end of this slideshow");
            workingSlideShow.ImageIndex = newIndex;

            return Result.Ok(workingSlideShow);
        }

        public Result<ImageSlideShow> RewindSlideShow(ImageSlideShow imageSlideShow)
        {
            return MoveSlideShow(imageSlideShow, -1);
        }
    }
}
