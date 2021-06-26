using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.TransationServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// Purchase a stock for an account.
        /// </summary>
        /// <param name="buyer">The account of the buyer.</param>
        /// <param name="stock">The symbol purchased.</param>
        /// <param name="shares">The amount of shares.</param>
        /// <returns>The updated account.</returns>
        /// <exception cref="InsufficientFundsException">Throw if the account has an insufficient balance</exception>
        /// <exception cref="InvalidSymbolException">Thrown if the symbol does not exist.</exception>
        /// <exception cref="Exceptions">Throw if the transaction fails</exception>
        Task<Account> BuyStock(Account buyer, string stock, int shares);
    }
}
