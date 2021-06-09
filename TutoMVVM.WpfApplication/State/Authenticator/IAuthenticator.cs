using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Services.AuthenticationServices;

namespace TutoMVVM.WpfApplication.State.Authenticator
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);

        /// <summary>
        /// Login to the application
        /// </summary>
        /// <param name="username">the user's name</param>
        /// <param name="password">the user's password</param>
        /// <exception cref="UserNotFoundException">Throw if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Throw if the password is invalid.</exception>
        /// <exception cref="Exceptions">Throw if the login fails.</exception>
        Task Login(string username, string password);
        void Logout();
    }
}
