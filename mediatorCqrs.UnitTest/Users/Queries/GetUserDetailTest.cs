using AutoMapper;
using mediatorCqrs.Application.Features.User.Handlers.Queries;
using mediatorCqrs.Application.Features.User.Requests.Queries;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Application.Profiles;
using mediatorCqrs.Domain;
using mediatorCqrs.UnitTest.Mock;
using Moq;

namespace mediatorCqrs.UnitTest.Users.Queries
{
    public class GetUserDetailTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockrepo;
        private readonly GetUserDetailRequestHandler _handler;
        private readonly int _id;
        private readonly User _user;

        public GetUserDetailTest()
        {
            _mockrepo = MockUserRepository.GetUserRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetUserDetailRequestHandler(_mockrepo.Object, _mapper);
            _id = 1;
            _user = new User()
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
        public async Task GetUserByIdTest()
        {
            //Arange
            var handler = new GetUserDetailRequestHandler(_mockrepo.Object, _mapper);

            //Act
            var result = await handler.Handle(new GetUserDetailRequest() { Id = _id },
                CancellationToken.None);

            //Assert
            Assert.Equal(1, result.Id);
        }
    }
}
