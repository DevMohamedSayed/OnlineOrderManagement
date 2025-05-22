using OnlineOrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagement.Application.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> UpdateCustomerAsync(Customer customer);   
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);   
    }
}
