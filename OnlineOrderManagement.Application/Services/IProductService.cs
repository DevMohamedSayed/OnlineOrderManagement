using OnlineOrderManagement.Application.Common;
using OnlineOrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagement.Application.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetAllProductsAsync(int page, int pageSize);
        Task<Product?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(Product dto);
        Task UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
       
    }
}
