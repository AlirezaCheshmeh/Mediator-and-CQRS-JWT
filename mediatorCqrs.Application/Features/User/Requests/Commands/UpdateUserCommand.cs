using mediatorCqrs.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Features.User.Requests.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserDtos  userDtos{ get; set; }
    }
}
