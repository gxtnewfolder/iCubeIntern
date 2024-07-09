using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Configurations;
using iCubeTrain.Services.Interface;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Controllers
{
    [ApiController]
    [Route("api/multitag")]
    public class MultiTagController : ControllerBase
    {
        private readonly IExternalAPIService _externalAPIService;
        private readonly IOpenAIService _openAIService;

        static MultiTagController()
        {
            MappingConfig.RegisterMapping();
        }

        public MultiTagController(IExternalAPIService externalAPIService, IOpenAIService openAIService)
        {
            _externalAPIService = externalAPIService;
            _openAIService = openAIService;
        }
    }
}