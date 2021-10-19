using Microsoft.VisualStudio.TestTools.UnitTesting;
using AxiomaWeatherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxiomaWeatherApp.Tests
{
    [TestClass()]
    public class LHMTApiClientTests
    {
        [TestMethod()]
        public async Task GetPlacesTest()
        {
            // setup
            var client = new LHMTApiClient();

            // act
            var places = await client.GetPlaces();

            // asert
            Assert.IsTrue(places is PlacesResponse);
            Assert.IsTrue(places.Count() > 0);
        }

        [TestMethod()]
        public async Task GetForecastTest()
        {
            // setup
            var client = new LHMTApiClient();

            // act
            var forecasts = await client.GetForecast("vilnius");

            // asert
            Assert.IsTrue(forecasts is ForecastResponse);
            Assert.IsTrue(forecasts.ForecastTimestamps.Count() > 0);
            Assert.IsTrue(forecasts.Place.Code == "vilnius");
        }
    }
}