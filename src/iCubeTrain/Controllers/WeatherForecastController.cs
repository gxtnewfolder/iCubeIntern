using iCubeTrain.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public WeatherForecastController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWeatherForecasts()
        {
            return Ok(await unitOfWork.WeatherForecasts.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherForecast(int id)
        {
            return Ok(await unitOfWork.WeatherForecasts.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddWeatherForecast(WeatherForecast weatherForecast)
        {
            await unitOfWork.WeatherForecasts.AddAsync(weatherForecast);
            await unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetAllWeatherForecasts), new { id = weatherForecast.Id }, weatherForecast);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeatherForecast(int id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }
            await unitOfWork.WeatherForecasts.UpdateAsync(weatherForecast);
            await unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await unitOfWork.WeatherForecasts.GetByIdAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            await unitOfWork.WeatherForecasts.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
