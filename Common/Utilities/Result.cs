using System;

namespace Common.Utilities
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }

        protected Result(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result Success(string message)
        {
            return new Result(true, message);
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }
    }

    /// <summary>
    /// Represents the result of an operation that returns a value.
    /// </summary>
    public class Result<T> : Result
    {
        public T Value { get; private set; }

        private Result(bool isSuccess, T value, string errorMessage = null)
            : base(isSuccess, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value);
        }

        public static Result<T> Success(T value, string message)
        {
            return new Result<T>(true, value, message);
        }

        public static new Result<T> Failure(string errorMessage)
        {
            return new Result<T>(false, default(T), errorMessage);
        }
    }
}