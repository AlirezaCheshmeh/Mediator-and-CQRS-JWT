using mediatorCqrs.Application.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _Dbset; 

        public GenericRepository(DataContext context)
        {
            _context = context;
            _Dbset = context.Set<T>();
        }
        public async  Task<T> Create(T entity)
        {
            _Dbset.AddAsync(entity);
             await _context.SaveChangesAsync();
            return entity;
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
           return await _Dbset.ToListAsync();
        }

        public Task<T> GetbyID(int ID)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
