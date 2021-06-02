using System.Threading.Tasks;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services.TransationServices
{
    public interface IBuyStockService
    {
        Task<Account> BuyStock(Account buyer, string stock, int shares);
    }
}
