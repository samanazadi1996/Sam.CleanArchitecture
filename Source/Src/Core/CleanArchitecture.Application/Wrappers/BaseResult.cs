using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Wrappers;

public class BaseResult
{
    public bool Success { get; set; }
    public List<Error> Errors { get; set; }

    public static BaseResult Ok()
        => new() { Success = true };

    public static BaseResult Fail()
        => new() { Success = false };

    public static BaseResult Fail(Error error)
        => new() { Success = false, Errors = [error] };

    public static BaseResult Fail(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };

    public BaseResult AddError(Error error)
    {
        Errors ??= [];
        Errors.Add(error);
        Success = false;
        return this;
    }
    public BaseResult AddErrors(IEnumerable<Error> errors)
    {
        Errors ??= [];
        Errors.AddRange(errors);
        Success = false;
        return this;
    }

}

public class BaseResult<TData> : BaseResult
{

    public TData Data { get; set; }

    public static BaseResult<TData> Ok(TData data)
        => new() { Success = true, Data = data };

    public static BaseResult<TData> Fail(TData data)
        => new() { Success = false, Data = data };

    public new static BaseResult<TData> Fail(Error error)
        => new() { Success = false, Errors = [error] };

    public new static BaseResult<TData> Fail(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };


}