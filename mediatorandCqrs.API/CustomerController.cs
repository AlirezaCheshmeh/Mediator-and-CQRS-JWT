using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mediatorandCqrs.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAthenticationJWTServices _athentication;

        public CustomerController(IAthenticationJWTServices athentication )
        {
            _athentication = athentication;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<Customer>> Register(CustomersLoginDtos loginDtos)
        {
           var result =await _athentication.register(loginDtos);
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> login(CustomersLoginDtos loginDtos)
        {
          var s=await  _athentication.Login(loginDtos);
            return Ok(s);
            
        }
    }
}
