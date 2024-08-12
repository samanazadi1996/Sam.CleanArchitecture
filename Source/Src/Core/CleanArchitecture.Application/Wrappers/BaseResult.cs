using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Wrappers;

public class BaseResult
{
    public bool Success { get; set; }
    public List<Error> Errors { get; set; }

    public static BaseResult Ok()
        => new() { Success = true };

    public static BaseResult Failure()
        => new() { Success = false };

    public static BaseResult Failure(Error error)
        => new() { Success = false, Errors = [error] };

    public static BaseResult Failure(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };

    public static implicit operator BaseResult(Error error)
        => Failure(error);

    public BaseResult AddError(Error error)
    {
        Errors ??= [];
        Errors.Add(error);
        Success = false;
        return this;
    }
}

public class BaseResult<TData> : BaseResult
{
    public TData Data { get; set; }

    public static BaseResult<TData> Ok(TData data)
        => new() { Success = true, Data = data };

    public new static BaseResult<TData> Failure(Error error)
        => new() { Success = false, Errors = [error] };

    public new static BaseResult<TData> Failure(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };

    public static implicit operator BaseResult<TData>(Error error)
        => Failure(error);

    public static implicit operator BaseResult<TData>(TData data)
        => Ok(data);
}