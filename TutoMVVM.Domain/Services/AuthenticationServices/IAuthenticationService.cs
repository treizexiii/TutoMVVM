using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Exceptions;

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

        /// <summary>
        /// Login to the application.
        /// </summary>
        /// <param name="username">the user's name.</param>
        /// <param name="password">the user's password.</param>
        /// <returns>The account for the user.</returns>
        /// <exception cref="UserNotFoundException">Throw if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Throw if the password is invalid.</exception>
        /// <exception cref="Exceptions">Throw if the login fails.</exception>
        Task<Account> Login(string username, string password);
    }
}
