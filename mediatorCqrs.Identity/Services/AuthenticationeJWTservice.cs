using AutoMapper;
using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.DTOs.Referesh;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Domain;
using mediatorCqrs.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace mediatorCqrs.Identity.Services
{
    public class AuthenticationeJWTservice : IAthenticationJWTServices
    {
        private readonly DataContext _data;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationeJWTservice(DataContext data, IConfiguration configuration , IMapper mapper)
        {
            _data = data;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<RefreshTokenDTO> CheckRefrshToken(int id)
        {
            var refershAndcus = await _data.refreshtokens
                .AsNoTracking()
                .Include(x => x.customer)
                .FirstOrDefaultAsync(x => x.customer.Id == id);
            var refreshDto = _mapper.Map<RefreshTokenDTO>(refershAndcus);
            return refreshDto;
        }

        public void CreatePasswordhash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }



        public string CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , customer.Id.ToString()),
                new Claim(ClaimTypes.Name ,customer.username),
                new Claim(ClaimTypes.Role , "admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("JWTsetting:key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<Customer> CustomerExist(string username)
        {
            var customerExist = await _data.Customers.FirstOrDefaultAsync(r => r.username == username);
            if (customerExist != null)
            {
                return customerExist;
            }
            return null;

        }

        public void GenerateRefreshToken(Refreshtoken refreshtoken)
        {

            _data.refreshtokens.Add(refreshtoken);
            _data.SaveChanges();

        }

        public Refreshtoken GetToken(string r)
        {
           var result =  _data.refreshtokens.FirstOrDefault(x=>x.Rtoken == r);
            return result;
        }

        public async Task<string> Login(CustomersLoginDtos customersLogin)
        {
            var customerexist = await CustomerExist(customersLogin.username);
            if (customerexist != null)
            {
                if (VerifyPassword(customersLogin.password, customerexist.passwordhash, customerexist.passwordsalt) == true)
                {
                    var t = CreateToken(customerexist);

                    return t;

                }
                string message = "not verify password";
                return message;
            }
            return null;
        }

        public async Task<Customer> register(CustomersLoginDtos customersLogin)
        {
            CreatePasswordhash(customersLogin.password, out byte[] passhash, out byte[] passsalt);

            Customer cus = new Customer()
            {
                passwordhash = passhash,
                passwordsalt = passsalt,
                username = customersLogin.username,

            };

            _data.Customers.Add(cus);
            await _data.SaveChangesAsync();

            return cus;
        }

        public void updatetoken(int cusId,string r)
        {
            var result = _data.refreshtokens.FirstOrDefault(x => x.Rtoken == r);
            result.Create = DateTime.Now;
            result.Expire = DateTime.Now.AddMinutes(2);
            result.Rtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            result.cusId = cusId;
             _data.SaveChanges();
        }

        public bool VerifyPassword(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return ComputeHash.SequenceEqual(passwordhash);
            }
        }


    }
}
