using mediatorCqrs.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using mediatorCqrs.Application.Features.User.Requests.Queries;
using mediatorCqrs.Application.Features.User.Requests.Commands;

namespace mediatorandCqrs.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetUserList")]
        public async Task<ActionResult<UserDtos>> GetUserList()
        {
            var UserList =await _mediator.Send(new GetUserListRequest());
            return Ok(UserList);
        }



        [HttpPost("CraeteUser")]
        public async Task<ActionResult<int>> Create([FromBody]CreateUserDtos user)
        {
            var command = new CreateUserCommand{ createUserDtos = user};
            var response = await _mediator.Send(command);
            return Ok(response);

        }
    }
}
