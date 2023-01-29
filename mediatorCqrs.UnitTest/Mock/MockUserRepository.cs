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
                    Id = 2,
                    DateTime= DateTime.Now,
                    email = " alich@",
                    isActive= true,
                    lastName = " ch",
                    name = " ali"
                }
            };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(users);

            mockRepo.Setup(r=> r.Delete(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                 {
                     users.Remove(user);
                     return true;
                 });

            mockRepo.Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    users.Add(user);
                    
                    return user;
                });
            var id = 1;
            mockRepo.Setup(x => x.GetbyID(id)).ReturnsAsync(users[0]);



            return mockRepo;
        }
    }
}
