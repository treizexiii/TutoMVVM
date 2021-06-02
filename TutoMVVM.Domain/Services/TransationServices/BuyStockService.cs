using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.TransationServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            double stockPrice = await _stockPriceService.GetPrice(symbol);
            double transactionAmount = stockPrice * shares;

            if (transactionAmount > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionAmount);
            }

            AssetTransaction transaction = new AssetTransaction()
            {
                Account = buyer,
                Asset = new Asset()
                {
                    Symbol = symbol,
                    PricePerShare = stockPrice
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = true
            };

            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionAmount;

            await _accountService.Update(buyer.Id, buyer);

            return buyer;
        }
    }
}
