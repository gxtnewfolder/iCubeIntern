using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Data;
using iCubeTrain.Models;
using iCubeTrain.Repositories;
using iCubeTrain.Repositories.Generic;
using iCubeTrain.Repositories.Interface;
using iCubeTrain.Services.Interface;

namespace iCubeTrain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IExternalAPIService _externalAPIService;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<WeatherForecast> _weatherForecastRepository;
        private IMultiTagRepository _multiTagRepository;

        public UnitOfWork(AppDbContext context, IExternalAPIService externalAPIService)
        {
            _context = context;
            _externalAPIService = externalAPIService;
        }

        public IGenericRepository<Product> Products => _productRepository ??= new GenericRepository<Product>(_context);
        public IGenericRepository<WeatherForecast> WeatherForecasts => _weatherForecastRepository ??= new GenericRepository<WeatherForecast>(_context);
        public IMultiTagRepository MultiTagRepository
        {
            get
            {
                if (_multiTagRepository == null)
                {
                    _multiTagRepository = new MultiTagRepository(_externalAPIService);
                }
                return _multiTagRepository;
            }
        }
        

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}