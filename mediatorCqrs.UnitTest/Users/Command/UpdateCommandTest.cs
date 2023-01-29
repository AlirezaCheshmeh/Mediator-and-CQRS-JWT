using AutoMapper;
using mediatorCqrs.Application.DTOs;
using mediatorCqrs.Application.Features.User.Requests.Commands;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Application.Profiles;
using mediatorCqrs.UnitTest.Mock;
using Moq;

namespace mediatorCqrs.UnitTest.Users.Command
{
    public class UpdateCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mocrepo;
        private readonly UserDtos _userDtos;

        public UpdateCommandTest()
        {
            _mocrepo = MockUserRepository.GetUserRepository();
            var mappConfig =new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();

            });
            _mapper = mappConfig.CreateMapper();
            _userDtos= new UserDtos()
            {

                Id = 1,
                DateTime = DateTime.Now,
                email = " alich@",
                isActive = false,
                lastName = " ch",
                name = " ali"
            };
        }

        [Fact]
        public async Task UpdatedCommandTest()
        {
            var handler = new UpdateUserCommandHandler(_mocrepo.Object,_mapper);
            var result =await handler.Handle(new UpdateUserCommand() { userDtos = _userDtos  },
               CancellationToken.None);

            Assert.Equal(true, result);
        }
    }
}
