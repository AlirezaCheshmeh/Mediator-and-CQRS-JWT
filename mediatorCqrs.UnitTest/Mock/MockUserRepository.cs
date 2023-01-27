using mediatorCqrs.Application.Persistance.Contracts;
using mediatorCqrs.Domain;
using Moq;

namespace mediatorCqrs.UnitTest.Mock
{
    public class MockUserRepository
    {
        public static Mock<IUserRepository> GetUserRepository()
        {
            
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    DateTime= DateTime.Now,
                    email = " alich@",
                    isActive= true,
                    lastName = " ch",
                    name = " ali"
                },
                new User
                {
                    Id = 1,
                    DateTime= DateTime.Now,
                    email = " alich@",
                    isActive= true,
                    lastName = " ch",
                    name = " ali"
                }
            };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(users);

            mockRepo.Setup(r => r.Create(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    users.Add(user);
                    return Task.CompletedTask;
                });
            return mockRepo;
        }
    }
}
