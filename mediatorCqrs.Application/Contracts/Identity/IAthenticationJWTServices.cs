using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Domain;

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
        RefreshToken GenerateRefreshToken();


        Task<Customer> GetCustomerFromRefreshToken(string refto);
    }

}
