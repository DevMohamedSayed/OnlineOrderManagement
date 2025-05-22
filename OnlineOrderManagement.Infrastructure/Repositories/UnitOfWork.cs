using OnlineOrderManagement.Domain.Entities;
using OnlineOrderManagement.Domain.Repositories;
using OnlineOrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Customer> Customers { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Order> Orders { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new Repository<Customer>(context);
            Products = new Repository<Product>(context);
            Orders = new Repository<Order>(context);
        }
        public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
