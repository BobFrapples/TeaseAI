namespace TeaseAI.Common
{
    /// <summary>Error when a method that returns <see cref="Result"/> fails.</summary>
    public class Error
    {
        /// <summary>Error message indicating why the method failed.</summary>
        public string Message { get; set; }

        public Error(string message)
        {
            Message = message;
        }
        public override string ToString()
        {
            return Message;
        }
    }

    /// <summary>Error when a method has arguments which are incorrect</summary>
    public class ArgumentError : Error
    {
        public string Parameter { get; set; }

        public ArgumentError(string message, string parameter) : base(message)
        {
            Parameter = parameter;
        }
    }
}
