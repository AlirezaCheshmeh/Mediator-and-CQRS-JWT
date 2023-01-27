using AutoMapper;
using mediatorCqrs.Application.DTOs;
using mediatorCqrs.Application.Features.User.Handlers.Queries;
using mediatorCqrs.Application.Features.User.Requests.Queries;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Application.Profiles;
using mediatorCqrs.UnitTest.Mock;
using Moq;
using Shouldly;

namespace mediatorCqrs.UnitTest.Users.Queries
{
    public class GetUserListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockrepo;
        public GetUserListRequestHandlerTest()
        {
            _mockrepo = MockUserRepository.GetUserRepository();
            var mapperConfig = new MapperConfiguration(r =>
            {
                r.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task GetUserListTest()
        {
            var handler = new GetUserListRequestHandler(_mockrepo.Object, _mapper);
            var resault = await handler.Handle(new GetUserListRequest(), CancellationToken.None);
            resault.ShouldBeOfType<List<UserDtos>>();
            resault.Count.ShouldBe(2);


        }
    }
}
