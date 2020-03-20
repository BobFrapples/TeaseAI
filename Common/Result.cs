using System;
using System.Diagnostics;

namespace TeaseAI.Common
{
    /// <summary>
    /// Success / Failure with error of an operation
    /// </summary>
    public struct Result
    {
        /// <summary>Was the operation successful</summary>
        public bool IsSuccess { get; }
        /// <summary>Was the operation a failure</summary>
        public bool IsFailure => !IsSuccess;
        private Error _error;
        /// <summary>Reason the operation failed.</summary>
        public Error Error
        {
            get
            {
                if (IsFailure && _error == null)
                    throw new InvalidOperationException("Result must have a error if it failed.");

                return _error;
            }
        }

        [DebuggerStepThrough]
        internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException(error.Message);
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Result must have a error if it failed.");

            IsSuccess = isSuccess;
            _error = error;
        }

        [DebuggerStepThrough]
        public static Result Fail(string message)
        {
            return new Result(false, new Error(message));
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, new Error(message));
        }

        [DebuggerStepThrough]
        public static Result Fail(Error error)
        {
            return new Result(false, error);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(default(T), false, error);
        }

        [DebuggerStepThrough]
        public static Result Ok()
        {
            return new Result(true, default(Error));
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, default(Error));
        }
    }

    public struct Result<T>
    {
        /// <summary>Was the operation successful</summary>
        public bool IsSuccess { get; }
        /// <summary>Was the operation a failure</summary>
        public bool IsFailure => !IsSuccess;

        private readonly T _Value;
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException(Error.Message);
                return _Value;
            }
        }

        private Error _error;
        /// <summary>Reason the operation failed.</summary>
        public Error Error
        {
            get
            {
                if (IsFailure && _error == null)
                    throw new InvalidOperationException("Result must have a error if it failed.");

                return _error;
            }
        }

        internal Result(T value, bool isSuccess, Error error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException(error.Message);
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Result must have a error if it failed.");

            _Value = value;
            IsSuccess = isSuccess;
            _error = error;
        }

    }

    public static class ResultExtensions
    {

        [DebuggerStepThrough]
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
                action();

            return result;
        }

        [DebuggerStepThrough]
        public static Result<K> OnSuccess<K>(this Result result, Func<Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func();
        }

        [DebuggerStepThrough]
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        [DebuggerStepThrough]
        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);
            return func(result.Value);
        }

        [DebuggerStepThrough]
        public static Result<TReturn> OnSuccess<T, TReturn>(this Result<T> result, Func<T, Result<TReturn>> func)
        {
            if (result.IsFailure)
                return Result.Fail<TReturn>(result.Error);
            return func(result.Value);
        }

        [DebuggerStepThrough]
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);
            return func();
        }

        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result<T> result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
                action(result.Value);

            return result;
        }

        [DebuggerStepThrough]
        /// <summary>
        /// Perform the task when Result is a failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, T> func)
        {
            if (result.IsSuccess)
                return result;

            return Result.Ok(func(result.Error));
        }

        [DebuggerStepThrough]
        /// <summary>
        /// perform action when result is a failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<Error> action)
        {
            if (result.IsFailure)
                action(result.Error);

            return result;
        }

        [DebuggerStepThrough]
        public static K OnBoth<T, K>(this Result<T> result, Func<Result<T>, K> func)
        {
            return func(result);
        }

        [DebuggerStepThrough]
        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.Fail(errorMessage);

            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return result;
        }

        [DebuggerStepThrough]
        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        [DebuggerStepThrough]
        public static Result Map<T>(this Result<T> result)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            return Result.Ok();
        }

        /// <summary>
        /// Returns the result, or the default value for that type if a failure
        /// </summary>
        /// <typeparam name="T">Data in the result</typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T GetResultOrDefault<T>(this Result<T> result) => result.IsSuccess ? result.Value : default(T);

        /// <summary>
        /// Returns the result, or the default value for that type
        /// </summary>
        /// <typeparam name="T">Data in the result</typeparam>
        /// <param name="result"></param>
        /// <param name="defaultValue">default value on a failure</param>
        /// <returns></returns>
        public static T GetResultOrDefault<T>(this Result<T> result, T defaultValue) => result.IsSuccess ? result.Value : defaultValue;

        /// <summary>
        /// Get the error message for a result, null if result is success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetErrorMessageOrDefault<T>(this Result<T> result) => result.IsSuccess ? null : result.Error.Message;

        /// <summary>
        /// Get the error message for a result, null if result is success
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetErrorMessageOrDefault(this Result result) => result.IsSuccess ? null : result.Error.Message;

        /// <summary>
        /// Get the error message for a result, null if result is success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetErrorMessageOrDefault<T>(this Result<T> result, string message) => result.IsSuccess ? message : result.Error.Message;

        /// <summary>
        /// Get the error message for a result, null if result is success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetErrorMessageOrDefault(this Result result, string message) => result.IsSuccess ? message : result.Error.Message;
    }
}