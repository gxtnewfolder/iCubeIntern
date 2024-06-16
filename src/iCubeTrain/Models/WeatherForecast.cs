namespace iCubeTrain;

public class WeatherForecast
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public required string Summary { get; set; }

    // Additional property to calculate Temperature in Fahrenheit
    public decimal TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
