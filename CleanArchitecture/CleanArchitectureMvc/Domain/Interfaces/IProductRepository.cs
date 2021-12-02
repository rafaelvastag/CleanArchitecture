﻿using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> FindProductWithCategoriesByIdAsync(int? id);
    }
}
