using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Models;
using iCubeTrain.Repositories.Generic;

namespace iCubeTrain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<WeatherForecast> WeatherForecasts { get; }
        Task<int> CompleteAsync();
    }
}