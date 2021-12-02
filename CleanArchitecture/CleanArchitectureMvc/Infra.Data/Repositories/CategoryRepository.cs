using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> FindAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> FindByIdAsync(int? id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<Category> DeleteAsync(Category obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();

            return obj;
        }
    }
}
