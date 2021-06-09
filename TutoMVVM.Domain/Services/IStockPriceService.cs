using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;

namespace TutoMVVM.Domain.Services
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the share price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price of.</param>
        /// <returns>The price of the symbol.</returns>
        /// <exception cref="InvalidSymbolException">Thrown if the symbol does not exist.</exception>
        /// /// <exception cref="Exceptions">Thrown if getting the symbol fails.</exception>
        Task<double> GetPrice(string symbol);
    }
}
