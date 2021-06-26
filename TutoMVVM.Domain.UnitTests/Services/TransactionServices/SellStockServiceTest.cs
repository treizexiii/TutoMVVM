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
    public class SellStockServiceTest
    {
        private SellStockService _sellStockService;

        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _sellStockService = new SellStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void SellStock_WithInsufficientShares_ThrowsInsufficientShareException()
        {
            string expectedSymbol = "T";
            int expectedAccountShares = 0;
            int expectedRequiredShares = 10;
            Account seller = CreateAccount(expectedSymbol, expectedAccountShares);

            InsufficientShareException exception = Assert.ThrowsAsync<InsufficientShareException>(
                () => _sellStockService.SellStock(seller, expectedSymbol, expectedRequiredShares));
            string actualSymbol = exception.Symbol;
            double actualAccountBalance = exception.AccountShares;
            double ActualRequiredBalance = exception.RequiredShares;

            Assert.AreEqual(expectedSymbol, actualSymbol);
            Assert.AreEqual(expectedAccountShares, actualAccountBalance);
            Assert.AreEqual(expectedRequiredShares, ActualRequiredBalance);
        }

        [Test]
        public void SellStock_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            string expectedInvaildSymbol = "Bad_symbol";
            Account seller = CreateAccount(expectedInvaildSymbol, 10);
            _mockStockPriceService.Setup(s => s.GetPrice(expectedInvaildSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvaildSymbol));

            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(() => _sellStockService.SellStock(seller, expectedInvaildSymbol, 5));
            string actualSymbolException = exception.Symbol;

            Assert.AreEqual(expectedInvaildSymbol, actualSymbolException);
        }

        [Test]
        public void SellStock_WithGetPriceFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _sellStockService.SellStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public void SellStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _sellStockService.SellStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public async Task SellStock_WithSuccessfullSell_ReturnAccountWithNewTransaction()
        {
            int expectedTransactionCount = 2;
            Account seller = CreateAccount(It.IsAny<string>(), 10);

            seller = await _sellStockService.SellStock(seller, It.IsAny<string>(), 5);
            int actualTransactionCount = seller.AssetTransactions.Count();

            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task SellStock_WithSuccessfullSell_ReturnAccountWithNewBalance()
        {
            int expectedBalance = 100;
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            seller = await _sellStockService.SellStock(seller, It.IsAny<string>(), 2);
            double actualBalance = seller.Balance;

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
