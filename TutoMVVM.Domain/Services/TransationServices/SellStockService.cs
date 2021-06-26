using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.TransationServices
{
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            int accountShares = GetAccountSharesForSymbol(seller, symbol);
            if (accountShares < shares)
            {
                throw new InsufficientShareException(symbol, accountShares, shares);
            }

            double stockPrice = await _stockPriceService.GetPrice(symbol);

            // Add an assetTransaction for the sale to the user account.
            seller.AssetTransactions.Add(new AssetTransaction()
            {
                Account = seller,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                IsPurchase = false,
                Shares = shares
            });

            // Update the account.
            seller.Balance += stockPrice * shares;

            await _accountService.Update(seller.Id, seller);

            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            IEnumerable<AssetTransaction> accountTransactionForSymbol = seller.AssetTransactions.Where(a => a.Asset.Symbol == symbol);
            return accountTransactionForSymbol.Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
        }
    }
}
