namespace TeaseAI.Common.Interfaces
{
    public interface IRandomNumberService
    {
        /// <summary>
        /// Roll a number between 1 and 100
        /// </summary>
        /// <returns></returns>
        int RollPercent();

        /// <summary>
        /// Generate a random number starting at <paramref name="minimum"/> (inclusive) and ending at <paramref name="maximum"/> (exclusive)
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        int Roll(int minimum, int maximum);
    }
}
