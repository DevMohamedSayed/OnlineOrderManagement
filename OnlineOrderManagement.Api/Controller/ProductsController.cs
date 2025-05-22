using Microsoft.AspNetCore.Mvc;
using OnlineOrderManagement.Application.Services;
using OnlineOrderManagement.Application;
using OnlineOrderManagement.Domain.Entities;
using AutoMapper;
using OnlineOrderManagement.Application.Common;

namespace OnlineOrderManagement.Api.Controller
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService svc, IMapper mapper)
        {
            _productService = svc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductCreateDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Product>(dto);
            var created = await _productService.CreateProductAsync(entity);
            var result = _mapper.Map<ProductDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            var list = await _productService.GetAllProductsAsync(page, pageSize);
            return Ok(_mapper.Map<PagedResult<ProductDto>>(list));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
        {
            var existing = await _productService.GetProductByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _productService.UpdateProductAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _productService.DeleteProductAsync(id))
                return NotFound();
            return NoContent();
        }


    }
}
