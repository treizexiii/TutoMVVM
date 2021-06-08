using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.AuthenticationServices;

namespace TutoMVVM.Domain.UnitTests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        private Mock<IAccountService> _mockAccountService;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);

        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            // Arrange
            string expectedUsername = "testUser";
            string password = "testPassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            // Act
            Account account = await _authenticationService.Login(expectedUsername, password);

            // Assert
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithInCorrectPasswordForExistingUsername_ThrowInvalidPasswordForUsername()
        {
            // Arrange
            string expectedUsername = "testUser";
            string password = "testPassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowInvalidPasswordForUsername()
        {
            // Arrange
            string expectedUsername = "testUser";
            string password = "testPassword";
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Regiter_WithPassordNotMatching_ReturnsPasswordDoNotMatch()
        {
            // Arrange
            string password = "testPassword";
            string confirmPassword = "confirmPassword";

            // Act
            RegistrationResult expected = RegistrationResult.PasswordDoNotMatch;
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Regiter_WithAlreadyExistingEmail_ReturnsEmailAlreadyExist()
        {
            // Arrange
            string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());

            // Act
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;
            RegistrationResult actual = await _authenticationService.Register(email, It.IsAny<string>(), "test", "test");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Regiter_WithAlreadyExistingUsernamel_ReturnsUsernameAlreadyExist()
        {
            // Arrange
            string username = "test";
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());

            // Act
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), username, "test", "test");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Regiter_WithNonExistingUserAndPasswordsMatching_ReturnsSuccess()
        {
            // Arrange

            // Act
            RegistrationResult expected = RegistrationResult.Success;
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), "test", "test");

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
