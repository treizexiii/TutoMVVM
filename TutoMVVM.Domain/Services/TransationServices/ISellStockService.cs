using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.TransationServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock for an account.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="symbol">The solded symbol.</param>
        /// <param name="shares">The amount of shares to sell</param>
        /// <returns>The uploaded account.</returns>
        /// <exception cref="InsufficientShareException">Thrown if the seller has insufficient shares for the symbol.</exception>
        /// <exception cref="InvalidSymbolException">Thrown if the symbol does not exist.</exception>
        /// <exception cref="Exceptions">Throw if the transaction fails</exception>
        Task<Account> SellStock(Account seller, string symbol, int shares);
    }
}
