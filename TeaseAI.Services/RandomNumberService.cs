using System;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        public int Roll(int minimum, int maximum)
        {
            return new Random().Next(minimum, maximum);
        }

        public int RollPercent() => Roll(1, 101);
    }
}
