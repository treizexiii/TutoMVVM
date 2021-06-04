using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Success,
        PasswordDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmParssword);
        Task<Account> Login(string username, string password);
    }
}
