using AutoMapper;
using mediatorCqrs.Application.Features.User.Requests.Commands;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Domain;
using MediatR;


public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userForDelete = _mapper.Map<User>(request.userDtos);
        var result = await _userRepository.Delete(userForDelete);
        //var resultBoolean = (result == true) ? true : false;
        return result;   
    }
}

