using DemoActions.Controllers;

namespace DemoActions.UnitTests.Controllers;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsExactlyFiveForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_AllForecastsHaveValidDates()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = controller.Get().ToArray();

        // Assert
        for (int i = 0; i < result.Length; i++)
        {
            Assert.True(result[i].Date > today);
            Assert.True(result[i].Date <= today.AddDays(5));
        }
    }

    [Fact]
    public void Get_AllTemperaturesAreInValidRange()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.InRange(forecast.TemperatureC, -20, 54);
        });
    }

    [Fact]
    public void Get_AllSummariesAreValid()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var validSummaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        // Act
        var result = controller.Get();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.NotNull(forecast.Summary);
            Assert.Contains(forecast.Summary, validSummaries);
        });
    }

    [Fact]
    public void Get_ReturnsNewInstancesOnEachCall()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result1 = controller.Get().ToArray();
        var result2 = controller.Get().ToArray();

        // Assert
        Assert.NotSame(result1, result2);
    }
}
