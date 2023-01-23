using mediatorCqrs.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Persistence.Configuration.Entities
{
    public class UserSeedingData
    {
      

       
        public async static void Seed(IApplicationBuilder application)
        {
            using (var servicescope = application.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();

                var user = new User()
                {
                    DateTime = DateTime.Now,
                    email = "alichesh@",
                    isActive = true,
                    name = "alireza",
                    lastName = " cheshmeh"
                };
                if (!context.User.Any())
                {
                    context.User.Add(user);
                    await context.SaveChangesAsync();
                }
            }


        }
        

    }
}
