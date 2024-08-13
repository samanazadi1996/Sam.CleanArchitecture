using CleanArchitecture.Infrastructure.GRPC.Protos;

namespace CleanArchitecture.Infrastructure.GRPC.Common;

public static class MapperExtensions
{
    public static List<Error> ToProtobufErrors(this List<CleanArchitecture.Application.Wrappers.Error> errors)
    {
        return errors?.Select(e => new Error
        {
            Description = e.Description,
            ErrorCode = e.ErrorCode.ToString(), 
            FieldName = e.FieldName
        }).ToList() ?? [];
    }
}
