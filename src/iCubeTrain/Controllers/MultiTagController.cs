using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Configurations;
using iCubeTrain.Data;
using iCubeTrain.Models;
using iCubeTrain.Services;
using iCubeTrain.Services.Interface;
using iCubeTrain.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Controllers
{
    [ApiController]
    [Route("api/multitag")]
    public class MultiTagController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OpenAIService _openAIService;
        private readonly TokenCountService _tokenCountService;

        private readonly AppDbContext _context;

        static MultiTagController()
        {
            MappingConfig.RegisterMapping();
        }

        public MultiTagController(IUnitOfWork unitOfWork, OpenAIService openAIService, TokenCountService tokenCountService, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _openAIService = openAIService;
            _tokenCountService = tokenCountService;
            _context = context;
        }

        [HttpGet("getalldata")]
        public async Task<IActionResult> GetAllDataAsync(string tagName, string starttime, string endtime)
        {
            var data = await _unitOfWork.MultiTagRepository.GetAllDataAsync(tagName, starttime, endtime);
            var incomingData = new IncomingData { Data = (List<TagValueData>)data };
            var transformedData = incomingData.Adapt<TransformedData>();
            return Ok(transformedData);
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeDataAsync([FromQuery] string tagName, string starttime, string endtime, string prompt)
        {
            try
            {
                // Fetch and transform data
                var data = await _unitOfWork.MultiTagRepository.GetAllDataAsync(tagName, starttime, endtime);
                var incomingData = new IncomingData { Data = (List<TagValueData>)data };
                var transformedData = incomingData.Adapt<TransformedData>();
                var transformedDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(transformedData);

                // Calculate token count for the prompt and transformed data
                var tokenCountPrompt = await _tokenCountService.GetTokenCountAsync(prompt);
                var tokenCountTransformedData = await _tokenCountService.GetTokenCountAsync(transformedDataJson);
                var tokenCountInput = tokenCountPrompt + tokenCountTransformedData;

                // Perform analysis using the OpenAI service
                var tagDataList = data.Select(data => data.Adapt<TagValueData>()).ToList();
                var result = AnalyzeJsonData(tagDataList);
                var analysisSummary = await _openAIService.GetOpenAIResponse(result, prompt);

                // Save chatbot analysis summary to the database
                var chatHistory = new ChatHistory
                {
                    UserQuery = prompt,
                    Response = analysisSummary,
                    Timestamp = DateTime.Now
                };
                _context.ChatHistories.Add(chatHistory);
                await _context.SaveChangesAsync();

                // Calculate the token count of the analysis summary
                var tokenCountAnalysisSummary = await _tokenCountService.GetTokenCountAsync(analysisSummary);

                return Ok(new { analysisSummary, tokenCountInput, tokenCountAnalysisSummary });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
        }

        private List<string> AnalyzeJsonData(List<TagValueData> dataItems)
        {
            var timeDifferences = new List<string>();
        
            // Group the data by TagId
            var groupedData = dataItems.GroupBy(item => item.Tagname);
        
            // Iterate over each group of data with the same TagId
            foreach (var group in groupedData)
            {
                var timestamps = group.Select(item => item.TimeStamp).OrderBy(ts => ts).ToList();
        
                for (int i = 1; i < timestamps.Count; i++)
                {
                    var diff = (timestamps[i] - timestamps[i - 1]).TotalSeconds;
                    timeDifferences.Add($"Time difference for Tagname {group.Key} between {timestamps[i - 1]} and {timestamps[i]} is {diff} seconds.");
                }
            }
        
            return timeDifferences;
        }
    }
}