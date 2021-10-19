using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AxiomaWeatherApp
{
    #region Api responses
    /// <summary>
    /// Vietovės atsakas
    /// </summary>
    public class PlacesResponse : List<PlaceModel>
    { }

    /// <summary>
    /// Vietovė
    /// </summary>
    public class PlaceModel
    {
        /// <summary>
        /// Kodas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("code", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// Pavadinimas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Administracinis vienetas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("administrativeDivision", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AdministrativeDivision { get; set; }

        /// <summary>
        /// Šalis
        /// </summary>
        [Newtonsoft.Json.JsonProperty("country", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Country { get; set; }

        /// <summary>
        /// Šalies kodas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("countryCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Koordinatės
        /// </summary>
        [Newtonsoft.Json.JsonProperty("coordinates", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Coordinates Coordinates { get; set; }
    }

    /// <summary>
    /// Koordinatės
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Platuma
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float Latitude { get; set; }

        /// <summary>
        /// Ilguma
        /// </summary>
        [Newtonsoft.Json.JsonProperty("longitude", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float Longitude { get; set; }
    }

    /// <summary>
    /// Orų prognozės atsakas
    /// </summary>
    public class ForecastResponse
    {
        /// <summary>
        /// Vietovė
        /// </summary>
        [Newtonsoft.Json.JsonProperty("place", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public PlaceModel Place { get; set; }

        /// <summary>
        /// Prognozės rūšis
        /// </summary>
        [Newtonsoft.Json.JsonProperty("forecastType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ForecastType { get; set; }

        /// <summary>
        /// Prognozės atnaujinimo laikas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("forecastCreationTimeUtc", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DateTime ForecastCreationTimeUtc { get; set; }

        /// <summary>
        /// Valandinės prognozės
        /// </summary>
        [Newtonsoft.Json.JsonProperty("forecastTimestamps", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<ForecastTimestamp> ForecastTimestamps { get; set; }
    }

    /// <summary>
    /// Valandinė prognozė
    /// </summary>
    public class ForecastTimestamp
    {
        /// <summary>
        /// Laikas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("forecastTimeUtc", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DateTime ForecastTimeUtc { get; set; }

        /// <summary>
        /// Oro temperatūra
        /// </summary>
        [Newtonsoft.Json.JsonProperty("airTemperature", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float AirTemperature { get; set; }

        /// <summary>
        /// Vėjo greitis
        /// </summary>
        [Newtonsoft.Json.JsonProperty("windSpeed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int WindSpeed { get; set; }

        /// <summary>
        /// Vėjo gūsių greitis
        /// </summary>
        [Newtonsoft.Json.JsonProperty("windGust", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int WindGust { get; set; }

        /// <summary>
        /// Vėjo kryptis
        /// </summary>
        [Newtonsoft.Json.JsonProperty("windDirection", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int WindDirection { get; set; }

        /// <summary>
        /// Debesuotumas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cloudCover", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int CloudCover { get; set; }

        /// <summary>
        /// Slėgis jūros lygyje
        /// </summary>
        [Newtonsoft.Json.JsonProperty("seaLevelPressure", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int SeaLevelPressure { get; set; }

        /// <summary>
        /// Santykinė drėgmė
        /// </summary>
        [Newtonsoft.Json.JsonProperty("relativeHumidity", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int RelativeHumidity { get; set; }

        /// <summary>
        /// Krituliai
        /// </summary>
        [Newtonsoft.Json.JsonProperty("totalPrecipitation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float TotalPrecipitation { get; set; }

        /// <summary>
        /// Oro sąlygų kodas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("conditionCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ConditionCode { get; set; }
    }

    /// <summary>
    /// Nerastos vietovės atsakas
    /// </summary>
    public class LocationNotFoundResponse
    {
        /// <summary>
        /// Klaida
        /// </summary>
        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ErrorResponse Error { get; set; }
    }

    /// <summary>
    /// Klaidos atsakas
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Kodas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("code", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// Pranešimas
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    #endregion

    #region FrontEndModels

    /// <summary>
    /// Dabartinis oras
    /// </summary>
    public class CurrentWeather
    {
        /// <summary>
        /// Vietovė
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Oro temperatūras
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// Oro sąlygos
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Vėjas
        /// </summary>
        public string Wind { get; set; }

        /// <summary>
        /// Min ir max temperatūros šiandien
        /// </summary>
        public string MinMax { get; set; }

        /// <summary>
        /// Oro sąlygų paveiksliukas
        /// </summary>
        public string ImagePath { get; set; }
    }

    /// <summary>
    /// Valandinė prognozė
    /// </summary>
    public class HourlyForecast
    {
        /// <summary>
        /// Laikas
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Oro sąlygos
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Oro temperatūra
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// Vėjas
        /// </summary>
        public string Wind { get; set; }

        /// <summary>
        /// Oro sąlygų paveiksliukas
        /// </summary>
        public string ImagePath { get; set; }
    }

    /// <summary>
    /// Savaitinė prognozė
    /// </summary>
    public class WeeklyForecast
    {
        /// <summary>
        /// Savaitės diena
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Oro sąlygos
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Oro sąlygų paveiksliukas
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Oro temperatūra
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// Vėjas
        /// </summary>
        public string Wind { get; set; }
    }

    /// <summary>
    /// Vietovė
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Valstybės kodas
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Pavadinimas
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Kodas
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// Pilna orų prognozė
    /// </summary>
    public class FullWeather : INotifyPropertyChanged
    {
        /// <summary>
        /// Dabartinis oras
        /// </summary>
        private CurrentWeather currentWeather;

        /// <summary>
        /// Valandinė prognozė
        /// </summary>
        private List<HourlyForecast> hourlyForecasts;

        /// <summary>
        /// Savaitinė prognozė
        /// </summary>
        private List<WeeklyForecast> weeklyForecasts;

        /// <summary>
        /// Vietovių sąrašas
        /// </summary>
        private List<Place> places;

        /// <summary>
        /// Paieškos tekstas
        /// </summary>
        private string searchText;

        public CurrentWeather CurrentWeather
        {
            get { return currentWeather; }
            set
            {
                if (currentWeather != value)
                {
                    currentWeather = value;
                    OnPropertyChange("CurrentWeather");

                }
            }
        }

        public List<HourlyForecast> HourlyForecasts
        {
            get { return hourlyForecasts; }
            set
            {
                if (hourlyForecasts != value)
                {
                    hourlyForecasts = value;
                    OnPropertyChange("HourlyForecasts");

                }
            }
        }

        public List<WeeklyForecast> WeeklyForecasts
        {
            get { return weeklyForecasts; }
            set
            {
                if (weeklyForecasts != value)
                {
                    weeklyForecasts = value;
                    OnPropertyChange("WeeklyForecasts");

                }
            }
        }

        public List<Place> Places
        {
            get { return places; }
            set
            {
                if (places != value)
                {
                    places = value;
                    OnPropertyChange("Places");

                }
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChange("SearchText");
                }
            }
        }

        public FullWeather()
        {
            SearchText = "Paieška...";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    #endregion

    #region Value converters
    /// <summary>
    /// Teksto dydžio skaičiuoklė (65%)
    /// </summary>
    public class FontSizeConverterDailyTemp : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .65);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Teksto dydžio skaičiuoklė (11%)
    /// </summary>
    public class FontSizeConverterDailyText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .65 / 6);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Teksto dydžio skaičiuoklė (8%)
    /// </summary>
    public class FontSizeConverterOther : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .65 / 8);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Teksto dydžio skaičiuoklė (18.5%)
    /// </summary>
    public class FontSizeConverterOtherTemps : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .65 / 3.5);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }
    #endregion
}
