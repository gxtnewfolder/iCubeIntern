using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Models;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Repositories.Interface
{
    public interface IMultiTagRepository
    {
        Task<IEnumerable<TagValueData>> GetAllDataAsync(string tagName, string starttime, string endtime);
        // Task<TagValueData> AnalyzeDataAsync([FromQuery] string tagName, string starttime, string endtime, string prompt);
    }
}