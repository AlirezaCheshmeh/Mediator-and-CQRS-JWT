using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Contracts.Identity
{
    public interface IAthenticationJWTServices
    {
        Task<string> Login(CustomersLoginDtos customersLogin);
        Task<Customer> register(CustomersLoginDtos customersLogin);

        Task<Customer> CustomerExist(string username);

        void CreatePasswordhash(string password,out byte[] passwordhash, out byte[] passwordsalt);
        bool VerifyPassword(string password, byte[] passwordhash, byte[] passwordsalt);
        string CreateToken(Customer customer);
    }

}
