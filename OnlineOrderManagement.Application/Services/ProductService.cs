using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineOrderManagement.Application.Common;
using OnlineOrderManagement.Domain.Entities;
using OnlineOrderManagement.Domain.Repositories;
using OnlineOrderManagement.Infrastructure.Data;

namespace OnlineOrderManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedResult<ProductDto>> GetAllProductsAsync(int page, int pageSize)
        {
            var query = _context.Products.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<ProductDto>(
                _mapper.Map<IEnumerable<ProductDto>>(items),
                totalCount,
                page,
                pageSize
            );
        }





        public async Task<ProductDto> CreateProductAsync(Product dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var existing = await _unitOfWork.Products.GetByIdAsync(id);
            if (existing == null) return null;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return product;
        }
        public async Task<Product?> GetProductByIdAsync(int id) =>
    await _unitOfWork.Products.GetByIdAsync(id);

public async Task UpdateProductAsync(Product product)
{
    _unitOfWork.Products.Update(product);
    await _unitOfWork.SaveChangesAsync();
}


        public async Task<bool> DeleteProductAsync(int id)
        {
            var existing = await _unitOfWork.Products.GetByIdAsync(id);
            if (existing == null) return false;

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }



    }
}
