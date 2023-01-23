using mediatorCqrs.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using mediatorCqrs.Application.Features.User.Requests.Queries;

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
    }
}
