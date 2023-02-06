using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.DTOs.Referesh;
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
        void GenerateRefreshToken(Refreshtoken refreshtoken);


        Task<RefreshTokenDTO> CheckRefrshToken(int id);

        Refreshtoken GetToken(string r);

        void updatetoken(int cusId ,string r );
        




    
    }

}
