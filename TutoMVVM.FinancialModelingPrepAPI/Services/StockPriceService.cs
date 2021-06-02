using System.Net.Http.Json;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Services;
using TutoMVVM.FinancialModelingPrepAPI.Results;

namespace TutoMVVM.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public StockPriceService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<double> GetPrice(string symbol)
        {
            using (FinancialModelingPrepHttpClient client = _httpClientFactory.CreateHttpClient())
            {
                StockPriceResult response = await client.GetAsync<StockPriceResult>("stock/real-time-price/" + symbol);

                if (response.Price == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }

                return response.Price;
            }
        }
    }
}
