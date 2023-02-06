using AutoMapper;
using mediatorCqrs.Application.DTOs.Referesh;
using mediatorCqrs.Domain;
using mediatorCqrs.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace mediatorandCqrs.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ValuesController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Refreshtoken>> Add()
        {
            Refreshtoken refresh = new Refreshtoken()
            {
                Rtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expire = DateTime.Now.AddMinutes(2),
                Create = DateTime.Now,
                cusId = 1

            };
            _dataContext.refreshtokens.Add(refresh);
            await _dataContext.SaveChangesAsync();

            ;

            return refresh;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<RefreshTokenDTO>> Get()
        {

            var cus = await _dataContext.refreshtokens
                   .AsNoTracking()
                   .Include(x => x.customer)
                   .FirstOrDefaultAsync(r => r.customer.Id == 1);
            var r = _mapper.Map<RefreshTokenDTO>(cus);
           
          


            return r ;

        }
        [HttpPut("up{r}")]
        public async Task<ActionResult<Refreshtoken>> update(string r)
        {
            var refreshToken = new Refreshtoken
            {
                Rtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expire = DateTime.Now.AddMinutes(2),
                Create = DateTime.Now,
                
                cusId = 1

            };
            var result = _dataContext.refreshtokens.FirstOrDefault(x => x.Rtoken == r);
            result.Create = DateTime.Now;
            result.Expire = DateTime.Now.AddMinutes(2);
            result.Rtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            result.cusId = 1;
            await _dataContext.SaveChangesAsync();
            return Ok(result);
        }








    }
}
