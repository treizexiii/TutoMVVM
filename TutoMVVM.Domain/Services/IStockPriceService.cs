using System.Threading.Tasks;

namespace TutoMVVM.Domain.Services
{
    public interface IStockPriceService
    {
        Task<double> GetPrice(string symbol);
    }
}
