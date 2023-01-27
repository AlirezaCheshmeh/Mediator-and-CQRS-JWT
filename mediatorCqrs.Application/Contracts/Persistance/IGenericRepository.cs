using mediatorCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Persistance.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetbyID(int ID);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Create(T entity);
        Task Update(T entity);
        Task<bool> Delete(T entity);

    }
}
