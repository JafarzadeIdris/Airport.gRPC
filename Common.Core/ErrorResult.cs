namespace Common.Core
{
    public class ErrorResult
    {
        public string Code { get; }
        public string Message { get; }

        private ErrorResult(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static ErrorResult NotFound => new ("NotFound", "The requested resource was not found.");
        public static ErrorResult ServerError => new ("ServerError", "An internal server error occurred.");
    }
}
