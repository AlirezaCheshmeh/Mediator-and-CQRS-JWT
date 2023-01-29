using AutoMapper;
using mediatorCqrs.Application.Features.User.Requests.Commands;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
        var ChangedUser = _mapper.Map<User>(request.userDtos);
        var result =await _userRepository.Update(ChangedUser);
        return result;
        }
    }

