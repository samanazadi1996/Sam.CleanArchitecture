using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Models
{
    public class BaseResult
    {
        public BaseResult()
        {
            Success = true;
            Message = "Ok";
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ApiVersion { get; set; }
        public List<string> Error { get; set; }

        public BaseResult NotFound(params string[] errors)
        {
            Success = false;
            Message = "NotFound";
            Error = errors.ToList();
            return this;
        }
        public BaseResult BadRequest(params string[] errors)
        {
            Success = false;
            Message = "BadRequest";
            Error = errors.ToList();
            return this;
        }
        public BaseResult SetApiVersion(string version)
        {
            ApiVersion = version;
            return this;
        }
    }
    public class BaseResult<T> : BaseResult
    {
        public T Result { get; set; }

        public BaseResult<T> Ok(T result)
        {
            Result = result;
            return this;
        }
        public BaseResult<T> NotFound(T result, params string[] errors)
        {
            NotFound(errors);
            Result = result;
            return this;
        }
    }
}
