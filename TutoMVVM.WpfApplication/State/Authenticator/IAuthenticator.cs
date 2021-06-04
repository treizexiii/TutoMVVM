using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services.AuthenticationServices;

namespace TutoMVVM.WpfApplication.State.Authenticator
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<bool> Login(string username, string password);
        void Logout();
    }
}
