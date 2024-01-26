using Microsoft.AspNetCore.Mvc;
using TestTemplate.Application.Features.Products.Commands.CreateProduct;
using TestTemplate.Application.Features.Products.Commands.DeleteProduct;
using TestTemplate.Application.Features.Products.Commands.UpdateProduct;
using TestTemplate.Application.Features.Products.Queries.GetPagedListProduct;
using TestTemplate.Application.Features.Products.Queries.GetProductById;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Dtos;
using TestTemplate.WebUi.Infrastracture.Helper;

namespace TestTemplate.WebUi.Controllers
{
    public class ProductController : BaseController
    {
        public IActionResult Index([FromQuery] GetPagedListProductQuery model) => View(model);

        public async Task<IActionResult> _Index([FromQuery] GetPagedListProductQuery model)
            => PartialView(new PagentaionResult<GetPagedListProductQuery, PagedResponse<ProductDto>>(model,await Mediator.Send(model)));
        public IActionResult _Create() => PartialView();

        public async Task<IActionResult> _Update(long id)
        {
            var product = await Mediator.Send(new GetProductByIdQuery() { Id = id });
            return PartialView(new UpdateProductCommand()
            {
                Id = product.Data.Id,
                Name = product.Data.Name,
                Price = product.Data.Price,
                BarCode = product.Data.BarCode
            });
        }


        [HttpPost]
        public async Task<BaseResult<long>> CreateProduct(CreateProductCommand model) => await Mediator.Send(model);

        [HttpPost]
        public async Task<BaseResult> UpdateProduct(UpdateProductCommand model) => await Mediator.Send(model);

        [HttpPost]
        public async Task<BaseResult> DeleteProduct(DeleteProductCommand model) => await Mediator.Send(model);

    }
}
