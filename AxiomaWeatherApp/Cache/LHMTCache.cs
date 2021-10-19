using AxiomaWeatherApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxiomaWeatherApp
{
    public class LHMTCache : ICache
    {
        private IApiClient client = new LHMTApiClient();

        //Vietovės
        private PlacesResponse places;

        //Prognozės
        private List<ForecastWrapper> forecasts = new List<ForecastWrapper>();

        /// <summary>
        /// Dabartinis oras
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public async Task<CurrentWeather> GetCurrentWeather(string placeCode)
        {
            var placeForecast = await GetForecast(placeCode);
            var currentForecast = placeForecast.ForecastTimestamps.Where(f => f.ForecastTimeUtc.Date == DateTime.UtcNow.Date
                                                                && f.ForecastTimeUtc.Hour == DateTime.UtcNow.Hour).FirstOrDefault();

            var currentWeather = new CurrentWeather
            {
                Place = placeForecast.Place.Name,
                Temp = Convert.ToInt32(currentForecast.AirTemperature) + "°C",
                Condition = TranslateConditionCode(currentForecast.ConditionCode),
                Wind = GetWindString(currentForecast.WindSpeed, currentForecast.WindDirection, true),
                ImagePath = GetImagePath(currentForecast.ConditionCode, currentForecast.ForecastTimeUtc.Hour),
                MinMax = GetDaysMinMaxTempValues(placeForecast.ForecastTimestamps.Where(f => f.ForecastTimeUtc.Date == DateTime.UtcNow.Date).ToList())
            };

            return currentWeather;
        }

        /// <summary>
        /// Valandinė prognozė
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public async Task<List<HourlyForecast>> GetHourlyForecast(string placeCode)
        {
            var hourlyForecasts = new List<HourlyForecast>();
            var placeForecast = await GetForecast(placeCode);
            var date = DateTime.UtcNow.Date;
            var time = DateTime.UtcNow.Date.AddHours(DateTime.UtcNow.Hour);

            for (int i = 0; i < 12; i++)
            {
                var forecastToAdd = placeForecast.ForecastTimestamps.Where(f => (f.ForecastTimeUtc.Date == date || f.ForecastTimeUtc.Date == date.AddDays(1)) && f.ForecastTimeUtc == time.AddHours(i*2)).FirstOrDefault();

                if (forecastToAdd != null)
                {
                    hourlyForecasts.Add(new HourlyForecast
                    {
                        Time = forecastToAdd.ForecastTimeUtc.Hour.ToString() + ":00",
                        Condition = TranslateConditionCode(forecastToAdd.ConditionCode),
                        ImagePath = GetImagePath(forecastToAdd.ConditionCode, time.AddHours(i * 2).Hour),
                        Temp = Convert.ToInt32(forecastToAdd.AirTemperature) + "°C",
                        Wind = GetWindString(forecastToAdd.WindSpeed, forecastToAdd.WindDirection)
                    });
                }
            }

            return hourlyForecasts;
        }

        /// <summary>
        /// Savaitės prognozė
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public async Task<List<WeeklyForecast>> GetWeeklyForecast(string placeCode)
        {
            var weeklyForecast = new List<WeeklyForecast>();
            var placeForecast = await GetForecast(placeCode);
            var date = DateTime.UtcNow.Date;

            for (int i = 0; i < 6; i++)
            {
                var daysForecasts = placeForecast.ForecastTimestamps.Where(f => f.ForecastTimeUtc.Date == date.AddDays(i)).ToList();

                if (daysForecasts.Count > 0)
                {
                    weeklyForecast.Add(new WeeklyForecast
                    {
                        Day = TranslateWeekdays(date.AddDays(i).DayOfWeek.ToString()),
                        Condition = TranslateConditionCode(GetMostCommonCondition(daysForecasts)),
                        ImagePath = GetImagePath(GetMostCommonCondition(daysForecasts), 12),
                        Temp = GetDaysMinMaxTempValues(daysForecasts),
                        Wind = GetAverageWind(daysForecasts)
                    });
                }
            }

            return weeklyForecast;
        }

        /// <summary>
        /// Orų prognozė
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public async Task<ForecastResponse> GetForecast(string placeCode)
        {
            try
            {
                // Prgonozė atitinkanti užklausos kodą
                var requestedForecast = forecasts.Where(f => f.forecast.Place.Code == placeCode).First();

                // Jeigu prognozė pasenusi atnaujinama cache ir grąžinama
                if (requestedForecast.forecast.ForecastCreationTimeUtc < DateTime.UtcNow.AddHours(-3) &&
                    requestedForecast.AddedToCacheUTC < DateTime.UtcNow.AddMinutes(-10))
                {
                    forecasts.Remove(requestedForecast);

                    requestedForecast = new ForecastWrapper { forecast = await client.GetForecast(placeCode), AddedToCacheUTC = DateTime.UtcNow };

                    forecasts.Add(requestedForecast);

                    return (requestedForecast.forecast);

                }
                // Jeigu nepasenusi grąžinama
                else
                {
                    return requestedForecast.forecast;
                }
            }
            catch (Exception ex)
            {
                // Nėra prognozės su tokiu kodu todėl gaunama nauja
                if (ex.Message == "Sequence contains no elements")
                {
                    var requestedForecast = new ForecastWrapper { forecast = await client.GetForecast(placeCode), AddedToCacheUTC = DateTime.UtcNow };

                    forecasts.Add(requestedForecast);

                    return (requestedForecast.forecast);
                }
                // Kita klaida
                else
                    throw ex;
            }
        }

        /// <summary>
        /// Vietovių sąrašas
        /// </summary>
        /// <returns></returns>
        public async Task<List<Place>> GetPlaces()
        {
            // Jeigu vietovės jau buvo gautos daugiau nebeatnaujinama
            if (places != null)
            {
                var converted = places.Select(f => new Place { CountryCode = f.CountryCode, Name = f.Name, Code = f.Code }).ToList();

                return converted;
            }
            else
            {
                places = await client.GetPlaces();
                var converted = places.Select(f => new Place { CountryCode = f.CountryCode, Name = f.Name, Code = f.Code }).ToList();

                return converted;
            }
        }

        /// <summary>
        /// Išverčia oro sąlygų kodą
        /// </summary>
        /// <param name="conditionCode"></param>
        /// <returns></returns>
        public string TranslateConditionCode(string conditionCode)
        {
            /*  clear - giedra;
                isolated-clouds - mažai debesuota;
                scattered-clouds - debesuota su pragiedruliais;
                overcast - debesuota;
                light-rain - nedidelis lietus;
                moderate-rain - lietus;
                heavy-rain - smarkus lietus;
                sleet - šlapdriba;
                light-snow - nedidelis sniegas;
                moderate-snow - sniegas;
                heavy-snow - smarkus sniegas;
                fog - rūkas;
                na - oro sąlygos nenustatytos.
            */

            switch (conditionCode)
            {
                case "clear":
                    return "Giedra";

                case "isolated-clouds":
                    return "Mažai debesuota";

                case "scattered-clouds":
                    return "Debesuota su pragiedruliais";

                case "overcast":
                    return "Debesuota";

                case "light-rain":
                    return "Nedidelis lietus";

                case "moderate-rain":
                    return "Lietus";

                case "heavy-rain":
                    return "Smarkus lietus";

                case "sleet":
                    return "Šlapdriba";

                case "light-snow":
                    return "Nedidelis sniegas";

                case "moderate-snow":
                    return "Sniegas";

                case "heavy-snow":
                    return "Smarkus sniegas";

                case "fog":
                    return "Rūkas";

                default:
                    return "Oro sąlygos nenustatytos";
            }
        }

        /// <summary>
        /// Sukuria vėjo greičio ir krypties tekstą
        /// </summary>
        /// <param name="windSpeed"></param>
        /// <param name="windDirection"></param>
        /// <param name="fullText"></param>
        /// <returns></returns>
        public string GetWindString(int windSpeed, int windDirection, bool fullText = false)
        {
            var windString = new StringBuilder("");

            windString.Append(windSpeed.ToString() + "m/s ");
            windString.Append(ConvertWindDirectionTostring(windDirection, fullText));

            return windString.ToString();
        }

        /// <summary>
        /// Išverčia vėjo kryptį į tekstą
        /// </summary>
        /// <param name="windDirection"></param>
        /// <param name="fullText"></param>
        /// <returns></returns>
        public string ConvertWindDirectionTostring(int windDirection, bool fullText)
        {
            switch (windDirection)
            {
                case int n when (n > 337 || n <= 22):
                    return fullText ? "šiaurės" : "š";

                case int n when (n > 22 && n <= 67):
                    return fullText ? "šiaurės vakarų" : "šv";

                case int n when (n > 67 && n <= 112):
                    return fullText ? "vakarų" : "v";

                case int n when (n > 112 && n <= 157):
                    return fullText ? "pietvakarių" : "pv";

                case int n when (n > 157 && n <= 202):
                    return fullText ? "pietų" : "p";

                case int n when (n > 202 && n <= 247):
                    return fullText ? "pietryčių" : "pr";

                case int n when (n > 247 && n <= 292):
                    return fullText ? "rytų" : "r";

                case int n when (n > 292 && n <= 337):
                    return fullText ? "šiaurės rytų" : "šr";

                default:
                    return fullText ? "Vėjo kryptis nenustatyta" : "na";
            }
        }

        /// <summary>
        /// Grąžina nuotraukos pavadinimą pagal oro sąlygas
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public string GetImagePath(string condition, int hour)
        {
            var dayNightString = hour > 8 && hour < 22 ? "/day_" : "/night_";
            switch (condition)
            {
                case "clear":
                    return "Resources/Icons" + dayNightString + "clear.png";

                case "isolated-clouds":
                    return "Resources/Icons" + dayNightString + "cloudy.png";

                case "scattered-clouds":
                    return "Resources/Icons" + dayNightString + "cloudy.png";

                case "overcast":
                    return "/Resources/Icons/cloudy.png";

                case "light-rain":
                    return "/Resources/Icons/light_rain.png";

                case "moderate-rain":
                    return "/Resources/Icons/medium_rain.png";

                case "heavy-rain":
                    return "/Resources/Icons/heavy_rain.png";

                case "sleet":
                    return "/Resources/Icons/sleet.png";

                case "light-snow":
                    return "/Resources/Icons/light_snow.png";

                case "moderate-snow":
                    return "/Resources/Icons/medium_snow.png";

                case "heavy-snow":
                    return "/Resources/Icons/heavy_snow.png";

                case "fog":
                    return "/Resources/Icons/fog.png";

                default:
                    return "/Resources/Icons/blank.png";
            }
        }

        /// <summary>
        /// Grąžina dienos min ir max temperatūras
        /// </summary>
        /// <param name="forecasts"></param>
        /// <returns></returns>
        public string GetDaysMinMaxTempValues(List<ForecastTimestamp> forecasts)
        {
            var min = Convert.ToInt32(forecasts.Min(f => f.AirTemperature));
            var max = Convert.ToInt32(forecasts.Max(f => f.AirTemperature));

            return min.ToString() + "°C / " + max.ToString() + "°C";
        }

        /// <summary>
        /// Išverčia savaitės dienų pavadinimus
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string TranslateWeekdays(string name)
        {
            switch (name)
            {
                case "Monday":
                    return "Pirmadienis";

                case "Tuesday":
                    return "Antradienis";

                case "Wednesday":
                    return "Trečiadienis";

                case "Thursday":
                    return "Ketvirtadienis";

                case "Friday":
                    return "Penktadienis";

                case "Saturday":
                    return "Šeštadienis";

                case "Sunday":
                    return "Sekmadienis";

                default:
                    return "";
                    break;
            }
        }

        /// <summary>
        /// Grąžina dažniausiai pasikartojančias oro sąlygas
        /// </summary>
        /// <param name="forecasts"></param>
        /// <returns></returns>
        public string GetMostCommonCondition(List<ForecastTimestamp> forecasts)
        {
            return forecasts.GroupBy(f => f.ConditionCode)
                .OrderByDescending(f => f.Count())
                .Select(f => f.Key)
                .FirstOrDefault().ToString();
        }

        /// <summary>
        /// Grąžina vidutinį vėjo greitį ir kryptį
        /// </summary>
        /// <param name="forecasts"></param>
        /// <returns></returns>
        public string GetAverageWind(List<ForecastTimestamp> forecasts)
        {
            return GetWindString(Convert.ToInt32(forecasts.Average(f => f.WindSpeed)), Convert.ToInt32(forecasts.Average(f => f.WindDirection)));
        }

    }

    public class ForecastWrapper
    {
        // LMHT neatnaujina prognozių lygiai kas 3 valandas dėl to naudojamas papildomas laikas
        public ForecastResponse forecast { get; set; }

        public DateTime AddedToCacheUTC { get; set; }
    }
}
