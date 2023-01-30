using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Domain;
using mediatorCqrs.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace mediatorCqrs.Identity.Services
{
    public class AuthenticationeJWTservice : IAthenticationJWTServices
    {
        private readonly DataContext _data;
        private readonly IConfiguration _configuration;
        private readonly JWTsetting _jwtoption;

        public AuthenticationeJWTservice(DataContext data , IOptions<JWTsetting> jwtoption ,IConfiguration configuration )
        {
            _data = data;
            _configuration = configuration;
            _jwtoption = jwtoption.Value;
        }
        public void CreatePasswordhash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public void CreatePasswordhash(string password, byte[] passwordhash, out byte[] passwordsalt)
        {
            throw new NotImplementedException();
        }

        public string CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,customer.username)
            };
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _jwtoption.key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<Customer> CustomerExist(string username)
        {
            var customerExist =await _data.Customers.FirstOrDefaultAsync(r => r.username == username);
            if(customerExist != null)
            {
                return customerExist;
            }
            return null;

        }

        public async Task<string> Login(CustomersLoginDtos customersLogin)
        {
            var customerexist =await CustomerExist(customersLogin.username);
            if (customerexist != null)
            {
                if(VerifyPassword(customersLogin.password , customerexist.passwordhash , customerexist.passwordsalt))
                {
                   var t =   CreateToken(customerexist);
                    return  t;
                }
                var message = "not verify password";
                return message;
            }
            return null;
        }

        public bool VerifyPassword(string password, byte[]passwordhash, byte[]passwordsalt)
        {
           
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return ComputeHash.SequenceEqual(passwordhash);
            }
        }

        
    }
}
