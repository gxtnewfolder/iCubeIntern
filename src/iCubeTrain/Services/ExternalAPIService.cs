using System;
using System.Collections.Generic;
using System.Linq;
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