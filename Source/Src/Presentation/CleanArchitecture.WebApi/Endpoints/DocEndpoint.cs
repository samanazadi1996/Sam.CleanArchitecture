using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.WebApi.Endpoints;

public class DocEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder
            .MapGet(GetErrorCodes);
    }

    public Dictionary<string, string> GetErrorCodes()
    {
        return Enum.GetValues<ErrorCode>().ToDictionary(t => ((int)t).ToString(), t => t.ToString());
    }
}
