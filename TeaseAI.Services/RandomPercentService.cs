using System;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class RandomPercentService : IRandomPercentService
    {
        public int RollPercent()
        {
            return new Random().Next(1, 100);
        }
    }
}
