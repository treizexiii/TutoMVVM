using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService dataService, IPasswordHasher passwordHasher)
        {
            _accountService = dataService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _accountService.GetByUsername(username);
            if (storedAccount == null)
            {
                throw new UserNotFoundException(username);
            }

            var passwordSuccess = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);
            if (passwordSuccess != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmParssword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmParssword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }
            Account emailUser = await _accountService.GetByEmail(email);
            if (emailUser != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }
            Account usernameAccount = await _accountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                User user = new User()
                {
                    Username = username,
                    Email = email,
                    PasswordHash = _passwordHasher.HashPassword(password),
                    DateJoined = DateTime.Now
                };

                Account account = new Account()
                {
                    AccountHolder = user
                };

                await _accountService.Create(account);
            }

            return result;
        }
    }
}
