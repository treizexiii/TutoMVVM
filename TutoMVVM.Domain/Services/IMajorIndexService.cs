using System.Threading.Tasks;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.Domain.Services
{
    public interface IMajorIndexService
    {
        Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
    }
}