using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Services;
using TutoMVVM.FinancialModelingPrepAPI.Results;

namespace TutoMVVM.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpClient _client;

        public StockPriceService(FinancialModelingPrepHttpClient client)
        {
            _client = client;
        }

        public async Task<double> GetPrice(string symbol)
        {
            StockPriceResult response = await _client.GetAsync<StockPriceResult>("stock/real-time-price/" + symbol);

            if (response.Price == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            return response.Price;
        }
    }
}
