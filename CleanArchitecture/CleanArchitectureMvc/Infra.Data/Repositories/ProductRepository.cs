using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> FindAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int? id)
        {
            return await _context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<Product> DeleteAsync(Product obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();

            return obj;
        }
    }
}
