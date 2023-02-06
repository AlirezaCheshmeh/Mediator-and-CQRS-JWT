using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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

            var cus = await _athentication.CustomerExist(loginDtos.username);
            var response = await _athentication.CheckRefrshToken(cus.Id);
            if (response.Rtoken == null)
            {
                var refreshToken = new Refreshtoken
                {
                    Rtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Expire = DateTime.Now.AddMinutes(2),
                    Create = DateTime.Now,
                    RefID = cus.Id
                };
                _athentication.GenerateRefreshToken(refreshToken);
                SetRefreshToken(refreshToken.Expire, refreshToken.Rtoken);

            }
            SetRefreshToken(response.Expire, response.Rtoken);
            //}
            //else if(response.Expire < DateTime.Now)
            //{
            //    var refreshToken = new Refreshtoken
            //    {
            //        Rtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            //        Expire = DateTime.Now.AddMinutes(2),
            //        Create = DateTime.Now,
            //        RefID = cus.Id
            //    };
            //    _athentication.updaterefrsh(refreshToken);
            //    SetRefreshToken(refreshToken.Expire, response.Rtoken);
            //}
            //else
            //{
            //    SetRefreshToken(response.Expire, response.Rtoken);
            //}


            return Ok(s);
        }

        [HttpPost("RefershToken")]
        public async Task<ActionResult<string>> Refersh()
        {

            var refresh = Request.Cookies["refreshToken"];
            var RefreshROW= _athentication.GetToken(refresh);
           var fullrefandCus =await _athentication.CheckRefrshToken(RefreshROW.cusId);
            if (RefreshROW.Expire < DateTime.Now)
            {
               _athentication.updatetoken(RefreshROW.cusId , refresh);
               var cus = await _athentication.CustomerExist(fullrefandCus.customerDTO.username);
               var token = _athentication.CreateToken(cus);
                return Unauthorized($"Token expierd/ newToken: {token}");
            }
            return Ok(refresh);
        }



        private void SetRefreshToken(DateTime ex , string refersh)
        {
            var CookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(2)
            };
            Response.Cookies.Append("refreshToken", refersh, CookieOption);
        }

    }
}
