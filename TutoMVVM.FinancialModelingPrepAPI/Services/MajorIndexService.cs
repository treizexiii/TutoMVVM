using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;

namespace TutoMVVM.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepHttpClient _client;

        public MajorIndexService(FinancialModelingPrepHttpClient client)
        {
            _client = client;
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            var response = await _client.GetAsync<MajorIndex>("majors-indexes/" + GetUriSuffix(indexType));
            response.Type = indexType;
            return response;
        }

        private static string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new Exception("MajorIndexType does not have a suffix defined.");
            }
        }
    }
}