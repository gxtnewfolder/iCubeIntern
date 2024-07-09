using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using iCubeTrain.Services.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iCubeTrain.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<string> GetOpenAIResponse(List<string> analysis, string prompt = "")
        {
            prompt += string.Join("\n", analysis);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = prompt }
                },
                max_tokens = 500,
                temperature = 1.0
            };

            var requestJson = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            _httpClient.DefaultRequestHeaders.Add("OpenAI-Data-Usage-Opt-Out", "true");

            try
            {
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                response.EnsureSuccessStatusCode();
                
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

                return responseObject["choices"][0]["message"]["content"].ToString();
            }
            catch (HttpRequestException e)
            {
                return $"Request error: {e.Message}";
            }
            catch (System.Text.Json.JsonException e)
            {
                return $"Serialization error: {e.Message}";
            }
            catch (Exception e)
            {
                return $"Unexpected error: {e.Message}";
            }
        }
    }
}