using AutoMapper;
using OnlineOrderManagement.Domain.Entities;
using OnlineOrderManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagement.Application.Services
{

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

       

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public async Task<Customer?> UpdateCustomerAsync(Customer customer)
        {
            var existing = await _unitOfWork.Customers.GetByIdAsync(customer.Id);
            if (existing == null) return null;
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existing = await _unitOfWork.Customers.GetByIdAsync(id);
            if (existing == null) return false;
            await _unitOfWork.Customers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
