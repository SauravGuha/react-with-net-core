

namespace Application.Core
{
    public class Result<T>
    {
        public string? ErrorMessage { get; set; }

        public T? Value { get; set; }

        public bool Status { get; set; }

        public int? ErrorCode { get; set; }

        public static Result<T> SetSuccess(T value) => new Result<T> { Status = true, Value = value };

        public static Result<T> SetError(string message, int errorCode) => new Result<T> { ErrorMessage = message, Status = false, ErrorCode = errorCode };
    }
}