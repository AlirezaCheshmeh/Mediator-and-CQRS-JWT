using AutoMapper;
using mediatorCqrs.Application.DTOs;
using mediatorCqrs.Application.Features.User.Requests.Commands;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Application.Profiles;
using mediatorCqrs.UnitTest.Mock;
using Moq;
using Shouldly;

namespace mediatorCqrs.UnitTest.Users.Command
{

    public class CreateUserCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockrepo;
        private readonly CreateUserDtos _createuserDtos;

        public CreateUserCommandTest()
        {
            _mockrepo = MockUserRepository.GetUserRepository();

            var mapperConfig = new MapperConfiguration(r =>
            {
                r.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _createuserDtos = new CreateUserDtos()
            {
                Id = 1,
                DateTime = DateTime.Now,
                email = " alich@",
                isActive = true,
                lastName = " ch",
                name = " ali"

            };
        }

        [Fact]
        public async Task CreateUser()
        {
            var handler = new CreateUserCommandHandler(_mockrepo.Object, _mapper);
            var result = handler.Handle(new CreateUserCommand() { createUserDtos = _createuserDtos }
            , CancellationToken.None);
            var User = await _mockrepo.Object.GetAll();
            User.Count.ShouldBe(2);



        }




    }
}
