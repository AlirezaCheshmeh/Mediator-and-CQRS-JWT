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
            var UserList = await _mediator.Send(new GetUserListRequest());
            return Ok(UserList);
        }



        [HttpPost("CraeteUser")]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserDtos user)
        {
            var command = new CreateUserCommand { createUserDtos = user };
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<bool>> Delete([FromBody] UserDtos user)
        {
            var command = new DeleteUserCommand { userDtos = user };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet("GetUserByID{id}")]
        public async Task<ActionResult<UserDtos>> GetByID(int id)
        {
            var Query = new GetUserDetailRequest { Id = id };
            var respponse = await _mediator.Send(Query);
            if(respponse != null)
            {
                return Ok(respponse);
            }
            return BadRequest("user not found");
            
        }


        [HttpPut("UpdateUser")]
        public async Task<ActionResult<bool>> UpdateUser(UserDtos user )
        {
            var command = new UpdateUserCommand { userDtos = user };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
