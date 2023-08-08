using FluentValidation.Results;
using System.Collections.Generic;

namespace Sam.CleanArchitecture.Application.Wrappers
{
    public class Result<TData> : BaseResult
    {
        public Result()
        {

        }
        public Result(TData data)
        {
            Success = true;
            Data = data;
            Errors = null;
        }
        public Result(Error error) : base(error)
        {
        }

        public Result(IEnumerable<Error> errors) : base(errors)
        {
        }
        public TData Data { get; set; }
    }
}
