using System.Threading.Tasks;
using MobileWeatherApp.Models;

namespace MobileWeatherApp.Services
{
    public interface IDarkSkyService
    {
        Task<Weather> GetWeatherData(double latitude, double longitude);
    }
}