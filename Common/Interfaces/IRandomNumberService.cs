namespace TeaseAI.Common.Interfaces
{
    public interface IRandomNumberService
    {
        int RollPercent();
        int Roll(int minimum, int maximum);
    }
}
