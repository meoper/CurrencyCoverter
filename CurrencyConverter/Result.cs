namespace CurrencyConverter
{
    public sealed class Result<T>
    {
        internal Result()
        {

        }

        public T Value { get; private init; }

        public bool IsSuccess { get; private init; }

        public string Message { get; private init; }

        public static Result<T> CreateSuccess(T value)
        {
            return new Result<T> { IsSuccess = true, Value = value };
        }

        public static Result<T> CreateFailure(string error)
        {
            return new Result<T> { IsSuccess = false, Message = error };
        }
    }

    public static class ResultExtensions
    {
        public static Result<T> ToSuccess<T>(this T value)
        {
            return Result<T>.CreateSuccess(value);
        }

        public static Result<TTo> ToFailure<TFrom, TTo>(this Result<TFrom> result)
        {
            return Result<TTo>.CreateFailure(result.Message);
        }
    }
}
