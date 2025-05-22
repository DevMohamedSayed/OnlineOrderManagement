using Microsoft.AspNetCore.Mvc;
using OnlineOrderManagement.Application.Services;
using OnlineOrderManagement.Application;
using OnlineOrderManagement.Domain.Entities;
using AutoMapper;

namespace OnlineOrderManagement.Api.Controller
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerCreateDto dto)
        {
            var customer = _mapper.Map<Domain.Entities.Customer>(dto);
            var created = await _customerService.CreateCustomerAsync(customer);
            var result = _mapper.Map<CustomerDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var list = await _customerService.GetAllCustomersAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(list));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDto dto)
        {
            var existing = await _customerService.GetCustomerByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _customerService.UpdateCustomerAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _customerService.DeleteCustomerAsync(id))
                return NotFound();
            return NoContent();
        }



    }
}
