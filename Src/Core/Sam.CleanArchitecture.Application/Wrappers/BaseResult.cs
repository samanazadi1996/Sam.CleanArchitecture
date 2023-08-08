using System.Collections.Generic;

namespace Sam.CleanArchitecture.Application.Wrappers
{
    public class BaseResult
    {
        public BaseResult()
        {
            Success = true;
            Errors = null;
        }
        public BaseResult(Error error)
        {
            Errors ??= new List<Error>();
            Errors.Add(error);
            Success = false;
        }

        public BaseResult(IEnumerable<Error> errors)
        {
            Errors ??= new List<Error>();
            Errors.AddRange(errors);
            Success = false;
        }

        public bool Success { get; set; }
        public List<Error> Errors { get; set; }
    }
}
