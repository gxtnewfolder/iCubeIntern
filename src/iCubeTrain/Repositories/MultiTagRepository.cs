using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Models;
using iCubeTrain.Repositories.Interface;
using iCubeTrain.Services.Interface;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Repositories
{
    public class MultiTagRepository : IMultiTagRepository
    {
        private readonly IExternalAPIService _externalAPIService;

        public MultiTagRepository(IExternalAPIService externalAPIService)
        {
            _externalAPIService = externalAPIService;
        }

        public async Task<IEnumerable<TagValueData>> GetAllDataAsync(string tagName, string starttime, string endtime)
        {
            return await _externalAPIService.GetAllDataAsync(tagName, starttime, endtime);
        }

        // public async Task<TagValueData> AnalyzeDataAsync([FromQuery] string tagName, string starttime, string endtime, string prompt)
        // {
        //     try
        //     {
        //         // Fetch and transform data
        //         var allData = await _externalAPIService.GetAllDataAsync(tagName, starttime, endtime);
        //         var incomingData = new IncomingData { Data = allData };
        //         var transformedData = incomingData.Adapt<TransformedData>();
        //         var transformedDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(transformedData);

        //         // Calculate token count for the prompt and transformed data
        //         var tokenCountPrompt = await _tokenCountService.GetTokenCountAsync(prompt);
        //         var tokenCountTransformedData = await _tokenCountService.GetTokenCountAsync(transformedDataJson);
        //         var tokenCountInput = tokenCountPrompt + tokenCountTransformedData;

        //         // Perform analysis using the OpenAI service
        //         var tagDataList = allData.Select(data => data.Adapt<TagValueData>()).ToList();
        //         var result = AnalyzeJsonData(tagDataList);
        //         var analysisSummary = await _openAIService.GetOpenAIResponse(result, prompt);

        //         // Calculate the token count of the analysis summary
        //         var tokenCountAnalysisSummary = await _tokenCountService.GetTokenCountAsync(analysisSummary);

        //         return Ok(new { analysisSummary, tokenCountInput, tokenCountAnalysisSummary });
        //     }
        //     catch (HttpRequestException ex)
        //     {
        //         return StatusCode(500, $"Error fetching data: {ex.Message}");
        //     }
        // }

        // private List<string> AnalyzeJsonData(List<TagValueData> dataItems)
        // {
        //     var timeDifferences = new List<string>();

        //     // Group the data by TagId
        //     var groupedData = dataItems.GroupBy(item => item.Tagname);

        //     // Iterate over each group of data with the same TagId
        //     foreach (var group in groupedData)
        //     {
        //         var timestamps = group.Select(item => item.TimeStamp).OrderBy(ts => ts).ToList();

        //         for (int i = 1; i < timestamps.Count; i++)
        //         {
        //             var diff = (timestamps[i] - timestamps[i - 1]).TotalSeconds;
        //             timeDifferences.Add($"Time difference for Tagname {group.Key} between {timestamps[i - 1]} and {timestamps[i]} is {diff} seconds.");
        //         }
        //     }

        //     return timeDifferences;
        // }
    }
}