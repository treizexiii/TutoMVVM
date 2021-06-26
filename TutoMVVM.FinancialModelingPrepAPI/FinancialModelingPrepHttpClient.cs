﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TutoMVVM.FinancialModelingPrepAPI.Model;

namespace TutoMVVM.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public FinancialModelingPrepHttpClient(HttpClient client, FinancialModelingPrepApiKey apiKey)
        {
            _client = client;
            _apiKey = apiKey.Key;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync($"{uri}?apikey={_apiKey}");
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
