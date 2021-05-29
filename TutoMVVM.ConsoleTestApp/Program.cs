using System;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.EntityFramework;
using TutoMVVM.EntityFramework.Services;

namespace TutoMVVM.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> userService = new GenericDataService<User>(new TutoMVVMDbContextFactory());

            var newUser = userService.Create(new User
            {
                Username = "Jonathan",
                Email = "jonathan.rougier@hotmail.fr",
                DateJoined = DateTime.Now,
                Password = "password"
            });

            Console.WriteLine($"{newUser.Result.Username} {newUser.Result.Id} {newUser.Result.DateJoined.ToString("yyyy-MM-dd")}");
        }
    }
}
