using Application.DTOs;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProductService : IService<ProductDTO, Product>
    {
        Task<ProductDTO> FindProductWithCategoriesByIdAsync(int? id);
    }
}
