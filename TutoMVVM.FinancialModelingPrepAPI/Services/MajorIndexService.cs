using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;

namespace TutoMVVM.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public MajorIndexService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using (FinancialModelingPrepHttpClient client = _httpClientFactory.CreateHttpClient())
            {
                var response = await client.GetAsync<MajorIndex>("majors-indexes/" + GetUriSuffix(indexType));
                response.Type = indexType;
                return response;
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
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