using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Services.Interface
{
    public interface IOpenAIService
    {
        Task<string> GetOpenAIResponse(List<string> analysis, string prompt = "");
    }
}