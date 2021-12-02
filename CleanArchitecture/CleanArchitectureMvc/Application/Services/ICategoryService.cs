using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public interface ICategoryService : IService<CategoryDTO,Category>
    {
    }
}
