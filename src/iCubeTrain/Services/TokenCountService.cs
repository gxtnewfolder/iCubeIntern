using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using iCubeTrain.Services.Interface;

namespace iCubeTrain.Services
{
    public class TokenCountService
    {
        private readonly HttpClient _httpClient;

        public TokenCountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTokenCountAsync(string text)
    {
        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { text }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("http://localhost:8000/count_tokens", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<int>(responseContent);
    }
    }
}