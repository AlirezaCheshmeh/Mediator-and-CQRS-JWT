﻿//using mediatorCqrs.Application.Persistance.Contracts;
//using mediatorCqrs.Domain;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace mediatorCqrs.UnitTest.Mock
//{
//    public class MockUserRepository
//    {
//        public static Mock<IUserRepository> GetUserRepository()
//        {
//            var user = new List<User>
//            {
//                new User
//                {
//                    Id = 1,
//                    DateTime= DateTime.Now,
//                    email = " alich@",
//                    isActive= true,
//                    lastName = " ch",
//                    name = " ali"
//                }
//            };
//            var mockRepo = new Mock<IUserRepository>();
//            mockRepo.Setup( r => r.GetAll()).ReturnsAsync(user);

//            mockRepo.Setup(r => r.Create(It.IsAny<User>())).(User user) =>{
//                user.Add(user);
//                return user;
//            };
//            return mockRepo;
//        }
//    }
//}