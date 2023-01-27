using AutoMapper;
using mediatorCqrs.Application.DTOs;
using mediatorCqrs.Application.Features.User.Requests.Commands;
using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Application.Profiles;
using mediatorCqrs.UnitTest.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.UnitTest.Users.Command
{
    public class DeleteUserCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockrepo;
        private readonly UserDtos _userDtos;

        public DeleteUserCommandTest()
        {
            _mockrepo = MockUserRepository.GetUserRepository();
            var mappinConfig = new MapperConfiguration(r =>
            {
                r.AddProfile<MappingProfile>();
            });
            _mapper = mappinConfig.CreateMapper();
            _userDtos = new UserDtos()
            {
                Id= 1,  
                DateTime = DateTime.Now,
                email = " alich@",
                isActive = true,
                lastName = " ch",
                name = " ali"
            };
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            var handler = new DeleteUserCommandHandler(_mockrepo.Object,_mapper);
            var result =await handler.Handle(new DeleteUserCommand { userDtos = _userDtos} ,
                CancellationToken.None);
            Assert.Equal(true, result);
        }
    }
}
