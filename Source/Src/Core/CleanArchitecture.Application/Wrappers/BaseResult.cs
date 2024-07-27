using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Wrappers;

public class BaseResult
{
    public BaseResult()
    {
        Success = true;
        Errors = null;
    }
    public BaseResult(Error error)
    {
        Errors = [error];
        Success = false;
    }

    public BaseResult(IEnumerable<Error> errors)
    {
        Errors = errors.ToList();
        Success = false;
    }

    public bool Success { get; set; }
    public List<Error> Errors { get; set; }
}
public class BaseResult<TData> : BaseResult
{
    public BaseResult()
    {

    }
    public BaseResult(TData data)
    {
        Success = true;
        Data = data;
        Errors = null;
    }
    public BaseResult(Error error) : base(error)
    {
    }

    public BaseResult(IEnumerable<Error> errors) : base(errors)
    {
    }
    public TData Data { get; set; }
}
