using mediatorCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Persistance.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
