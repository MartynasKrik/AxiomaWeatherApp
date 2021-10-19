using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxiomaWeatherApp.Interfaces
{
    /// <summary>
    /// Cache
    /// </summary>
    interface ICache
    {
        /// <summary>
        /// Vietovės
        /// </summary>
        /// <returns></returns>
        System.Threading.Tasks.Task<List<Place>> GetPlaces();

        /// <summary>
        /// Dabartinis oras
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<CurrentWeather> GetCurrentWeather(string placeCode);

        /// <summary>
        /// Valandinės prognozės
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<List<HourlyForecast>> GetHourlyForecast(string placeCode);

        /// <summary>
        /// Savaitinės prognozės
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<List<WeeklyForecast>> GetWeeklyForecast(string placeCode);
    }
}
