using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Data;
using iCubeTrain.Models;
using iCubeTrain.Repositories.Generic;

namespace iCubeTrain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<WeatherForecast> _weatherForecastRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> Products => _productRepository ??= new GenericRepository<Product>(_context);
        public IGenericRepository<WeatherForecast> WeatherForecasts => _weatherForecastRepository ??= new GenericRepository<WeatherForecast>(_context);

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