using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.TransationServices;

namespace TutoMVVM.Domain.UnitTests.Services.TransactionServices
{
    [TestFixture]
    public class BuyStockServiceTest
    {
        private BuyStockService _buyStockService;

        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _buyStockService = new BuyStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void BuyStock_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            string expectedSymbol = "Bad_symbol";
            Account buyer = CreateAccount(expectedSymbol, 10);
            _mockStockPriceService.Setup(s => s.GetPrice(expectedSymbol)).ThrowsAsync(new InvalidSymbolException(expectedSymbol));

            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(() => _buyStockService.BuyStock(buyer, expectedSymbol, 5));
            string actualSymbolException = exception.Symbol;

            Assert.AreEqual(expectedSymbol, actualSymbolException);
        }

        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {
            Account buyer = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 5));
        }

        [Test]
        public void BuyStock_WithInsufficientFunds_ThrowsInsufficientFundsExceptionForBalances()
        {
            double expectedBalance = 0;
            double expectedRequiredBalance = 100;
            Account buyer = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            InsufficientFundsException exception = Assert.ThrowsAsync<InsufficientFundsException>(
                () => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 2));
            double actualBalance = exception.AccountBalance;
            double actualRequiredBalance = exception.RequiredBalance;

            Assert.AreEqual(expectedBalance, actualBalance);
            Assert.AreEqual(expectedRequiredBalance, actualRequiredBalance);
        }

        [Test]
        public void BuyStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _buyStockService.BuyStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public async Task BuyStock_WithSuccessfullPurchase_ReturnAccountWithNewTransaction()
        {
            int expectedTransationCount = 2;
            Account buyer = CreateAccount(It.IsAny<string>(), 10);

            buyer = await _buyStockService.BuyStock(buyer, It.IsAny<string>(), 5);
            int actualTransactionCount = buyer.AssetTransactions.Count();

            Assert.AreEqual(expectedTransationCount, actualTransactionCount);
        }

        [Test]
        public async Task BuyStock_WithSuccessfullPurchase_ReturnAccountWithNewBallance()
        {
            double expectedBalance = 0;
            Account buyer = CreateAccount(It.IsAny<string>(), 10);
            buyer.Balance = 100;
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            buyer = await _buyStockService.BuyStock(buyer, It.IsAny<string>(), 2);
            double actualBalance = buyer.Balance;

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private static Account CreateAccount(string symbol, int shares)
        {
            return new Account()
            {
                AssetTransactions = new List<AssetTransaction>()
                {
                    new AssetTransaction()
                    {
                        Asset = new Asset()
                        {
                            Symbol = symbol,
                        },
                        IsPurchase = true,
                        Shares = shares
                    }
                }
            };
        }
    }
}
