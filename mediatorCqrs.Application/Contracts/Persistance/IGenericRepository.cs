using mediatorCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Persistance.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetbyID(Expression<Func<T, bool>> expression);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Create(T entity);
        bool Update(T entity);
        Task<bool> Delete(T entity);

    }
}
