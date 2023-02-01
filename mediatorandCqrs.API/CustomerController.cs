using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Domain;
using mediatorCqrs.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mediatorandCqrs.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAthenticationJWTServices _athentication;
        

        public CustomerController(IAthenticationJWTServices athentication)
        {
            _athentication = athentication;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<Customer>> Register(CustomersLoginDtos loginDtos)
        {
            var result = await _athentication.register(loginDtos);
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> login(CustomersLoginDtos loginDtos)
        {
            var s = await _athentication.Login(loginDtos);
            var refershToekn = _athentication.GenerateRefreshToken();
            SetRefreshToken(refershToekn);



            return Ok(s);


        }

        [HttpPost("RefershToken")]
        public async Task<ActionResult<string>> Refersh()
        {

            var refresh = Request.Cookies["refreshToken"];
            var cus =await _athentication.GetCustomerFromRefreshToken(refresh);
            

            if(cus == null)
            {
                return Unauthorized("Token not valid");
            }
            else if ( cus.TokenExpieres < DateTime.Now)
            {
                return Unauthorized("Token expierd");
            }
            string token = _athentication.CreateToken(cus);
            var newrefersh = _athentication.GenerateRefreshToken();
            SetRefreshToken(newrefersh);
            Customer c = new Customer
            {
                RefreshToken = newrefersh.Token ,
                DateCreate = newrefersh.Create ,
                TokenExpieres = newrefersh.Expires
            };
            

            return Ok(token);
        }



        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var CookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, CookieOption);
        }

    }
}
