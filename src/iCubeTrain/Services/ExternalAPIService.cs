using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using iCubeTrain.Models;
using iCubeTrain.Services.Interface;

namespace iCubeTrain.Services
{
    public class ExternalAPIService : IExternalAPIService
    {
        private readonly HttpClient _httpClient;

        public ExternalAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhY2Nlc3NfdG9rZW4iLCJ1c2VySWQiOiIzNTYiLCJmYWN0b3J5SWQiOiI5IiwiZXhwIjoxNzk2MzgzNDg5LCJpc3MiOiJpQ3ViZVNlcnZlciIsImF1ZCI6ImlDdWJlUHJvdmlkZXIifQ.fsj0Krulf4R7WA9pmIZpQUy0vMAVgcvFD04HtNs0q-c");
        }

        public async Task<List<TagValueData>> GetAllDataAsync(string tagName, string starttime, string endtime)
        {
            var data = new List<TagValueData>();

            var url = $"https://api.icube.co.th/integration/webapi/tag/multiple/values?tagname={tagName}&starttime={starttime}&endtime={endtime}";
            var response = await _httpClient.GetFromJsonAsync<IncomingData>(url);
            data.AddRange(response.Data);

            return data;
        }

    }
}