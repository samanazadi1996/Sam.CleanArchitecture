using Microsoft.AspNetCore.Mvc;
using Sam.CleanArchitecture.Application.Features.OutBoxEventItems.Queries.GetPagedList;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Dtos;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class OutBoxEventItemController : BaseApiController
    {

        [HttpGet]
        public async Task<PagedResponse<OutBoxEventItemDto>> GetPagedList([FromQuery] GetPagedListQuery model)
            => await Mediator.Send(model);

    }

}