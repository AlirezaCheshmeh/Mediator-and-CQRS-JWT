using mediatorandCqrs.API;
using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Domain;
using mediatorCqrs.Identity.Services;
using mediatorCqrs.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.UnitTest.CustoemrsTest
{
    public class JWTAuthenticationtTest
    {
        [Fact]
        public  async Task LoginTeset()
        {
            CustomersLoginDtos loginDtos = new CustomersLoginDtos()
            {
                username = "alich",
                password = "12345" ,
            };
            Customer us = new Customer()
            {
             
               
                    username = "alich",
                passwordhash = Encoding.UTF8.GetBytes("sada"),
                passwordsalt = Encoding.UTF8.GetBytes("sada"),
               
            };
            var mock =new Mock<IAthenticationJWTServices>();
            mock.Setup(x => x.Login(loginDtos)).ReturnsAsync((string u) =>
            {
                mock.Setup(x=>x.CustomerExist("alich")).ReturnsAsync(us);
                mock.Setup(x => x.CreateToken(us)).Returns(Guid.NewGuid().ToString());
                mock.Setup(x => x
                .VerifyPassword(loginDtos.password, us.passwordhash, us.passwordsalt))
                .Returns(true);
                u = Guid.NewGuid().ToString();
                return u;
            });


            var sut = new CustomerController(mock.Object);
            var result = sut.login(loginDtos);
            Assert.NotNull(result);

        }
    }
}
