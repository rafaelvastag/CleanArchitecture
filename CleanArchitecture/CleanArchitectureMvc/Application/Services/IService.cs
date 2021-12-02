using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IService<T,E> 
        where T : class 
        where E : class
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> FindByIdAsync(int? id);
        Task<T> AddAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task<T> DeleteAsync(T obj);
    }
}
