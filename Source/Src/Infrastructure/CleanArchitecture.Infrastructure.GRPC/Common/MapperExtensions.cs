using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Infrastructure.GRPC.Protos;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Infrastructure.GRPC.Common;

public static class MapperExtensions
{
    public static IEnumerable<GrpcError> ToProtobufErrors(this List<Error> errors)
    {
        return errors?.Select(e => new GrpcError
        {
            Description = e.Description,
            ErrorCode = e.ErrorCode.ToString(),
            FieldName = e.FieldName
        }) ?? [];
    }
    public static GrpcBaseResultWithIntData ToGrpcBaseResultWithIntData(this BaseResult<long> result)
    {
        return new GrpcBaseResultWithIntData
        {
            Success = result.Success,
            Errors = { result.Errors.ToProtobufErrors() },
            Data = result.Data
        };
    }
    public static GrpcBaseResult ToGrpcBaseResult(this BaseResult result)
    {
        return new GrpcBaseResult
        {
            Success = result.Success,
            Errors = { result.Errors.ToProtobufErrors() }
        };
    }
}
