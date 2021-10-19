using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxiomaWeatherApp
{
    /// <summary>
    /// Api klientas
    /// </summary>
    interface IApiClient
    {
        /// <summary>
        /// Vietovės
        /// </summary>
        /// <returns></returns>
        System.Threading.Tasks.Task<PlacesResponse> GetPlaces();

        /// <summary>
        /// Prognozės
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<ForecastResponse> GetForecast(string placeCode);
    }
}
