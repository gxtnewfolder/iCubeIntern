using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Models;

namespace iCubeTrain.Services.Interface
{
    public interface IExternalAPIService
    {
        Task<List<TagValueData>> GetAllDataAsync(string tagName, string starttime, string endtime);
    }
}