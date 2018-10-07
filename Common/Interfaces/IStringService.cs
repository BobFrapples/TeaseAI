namespace TeaseAI.Common.Interfaces
{
    public interface IStringService
    {
        string Capitalize(string input);

        bool WordExists(string haystack, string needle);
    }
}
