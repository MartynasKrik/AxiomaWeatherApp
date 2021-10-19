using Microsoft.VisualStudio.TestTools.UnitTesting;
using AxiomaWeatherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AxiomaWeatherApp.Tests
{
    [TestClass()]
    public class LHMTCacheTests
    {
        [TestMethod()]
        public async Task GetCurrentWeatherTest()
        {
            // Setup
            var cache = new LHMTCache();

            // Act
            var response = await cache.GetCurrentWeather("vilnius");

            // Assert
            Assert.AreNotEqual(response, null);
            Assert.AreEqual(response.Place, "Vilnius");
            Assert.AreNotEqual(response.Condition, null);
            Assert.AreNotEqual(response.ImagePath, null);
            Assert.AreNotEqual(response.MinMax, null);
            Assert.AreNotEqual(response.Temp, null);
            Assert.AreNotEqual(response.Wind, null);
        }

        [TestMethod()]
        public async Task GetHourlyForecastTest()
        {
            // Setup
            var cache = new LHMTCache();

            // Act
            var response = await cache.GetHourlyForecast("vilnius");

            // Assert
            Assert.AreNotEqual(response, null);
            Assert.AreEqual(response.Count, 12);

            // Visi rezultatai turi visas reikšmmes
            #region basic values

            Assert.AreNotEqual(response[0].Condition, null);
            Assert.AreNotEqual(response[0].ImagePath, null);
            Assert.AreNotEqual(response[0].Temp, null);
            Assert.AreNotEqual(response[0].Time, null);
            Assert.AreNotEqual(response[0].Wind, null);

            Assert.AreNotEqual(response[1].Condition, null);
            Assert.AreNotEqual(response[1].ImagePath, null);
            Assert.AreNotEqual(response[1].Temp, null);
            Assert.AreNotEqual(response[1].Time, null);
            Assert.AreNotEqual(response[1].Wind, null);

            Assert.AreNotEqual(response[2].Condition, null);
            Assert.AreNotEqual(response[2].ImagePath, null);
            Assert.AreNotEqual(response[2].Temp, null);
            Assert.AreNotEqual(response[2].Time, null);
            Assert.AreNotEqual(response[2].Wind, null);

            Assert.AreNotEqual(response[3].Condition, null);
            Assert.AreNotEqual(response[3].ImagePath, null);
            Assert.AreNotEqual(response[3].Temp, null);
            Assert.AreNotEqual(response[3].Time, null);
            Assert.AreNotEqual(response[3].Wind, null);

            Assert.AreNotEqual(response[4].Condition, null);
            Assert.AreNotEqual(response[4].ImagePath, null);
            Assert.AreNotEqual(response[4].Temp, null);
            Assert.AreNotEqual(response[4].Time, null);
            Assert.AreNotEqual(response[4].Wind, null);

            Assert.AreNotEqual(response[5].Condition, null);
            Assert.AreNotEqual(response[5].ImagePath, null);
            Assert.AreNotEqual(response[5].Temp, null);
            Assert.AreNotEqual(response[5].Time, null);
            Assert.AreNotEqual(response[5].Wind, null);

            Assert.AreNotEqual(response[6].Condition, null);
            Assert.AreNotEqual(response[6].ImagePath, null);
            Assert.AreNotEqual(response[6].Temp, null);
            Assert.AreNotEqual(response[6].Time, null);
            Assert.AreNotEqual(response[6].Wind, null);

            Assert.AreNotEqual(response[7].Condition, null);
            Assert.AreNotEqual(response[7].ImagePath, null);
            Assert.AreNotEqual(response[7].Temp, null);
            Assert.AreNotEqual(response[7].Time, null);
            Assert.AreNotEqual(response[7].Wind, null);

            Assert.AreNotEqual(response[8].Condition, null);
            Assert.AreNotEqual(response[8].ImagePath, null);
            Assert.AreNotEqual(response[8].Temp, null);
            Assert.AreNotEqual(response[8].Time, null);
            Assert.AreNotEqual(response[8].Wind, null);

            Assert.AreNotEqual(response[9].Condition, null);
            Assert.AreNotEqual(response[9].ImagePath, null);
            Assert.AreNotEqual(response[9].Temp, null);
            Assert.AreNotEqual(response[9].Time, null);
            Assert.AreNotEqual(response[9].Wind, null);

            Assert.AreNotEqual(response[10].Condition, null);
            Assert.AreNotEqual(response[10].ImagePath, null);
            Assert.AreNotEqual(response[10].Temp, null);
            Assert.AreNotEqual(response[10].Time, null);
            Assert.AreNotEqual(response[10].Wind, null);

            Assert.AreNotEqual(response[11].Condition, null);
            Assert.AreNotEqual(response[11].ImagePath, null);
            Assert.AreNotEqual(response[11].Temp, null);
            Assert.AreNotEqual(response[11].Time, null);
            Assert.AreNotEqual(response[11].Wind, null);
            #endregion


        }

        [TestMethod()]
        public async Task GetWeeklyForecastTest()
        {
            // Setup
            var cache = new LHMTCache();

            // Act
            var response = await cache.GetWeeklyForecast("vilnius");

            // Assert
            Assert.AreNotEqual(response, null);
            Assert.AreEqual(response.Count, 6);

            // Visi rezultatai turi visas reikšmmes
            #region basic values

            Assert.AreNotEqual(response[0].Condition, null);
            Assert.AreNotEqual(response[0].ImagePath, null);
            Assert.AreNotEqual(response[0].Temp, null);
            Assert.AreNotEqual(response[0].Day, null);
            Assert.AreNotEqual(response[0].Wind, null);

            Assert.AreNotEqual(response[1].Condition, null);
            Assert.AreNotEqual(response[1].ImagePath, null);
            Assert.AreNotEqual(response[1].Temp, null);
            Assert.AreNotEqual(response[1].Day, null);
            Assert.AreNotEqual(response[1].Wind, null);

            Assert.AreNotEqual(response[2].Condition, null);
            Assert.AreNotEqual(response[2].ImagePath, null);
            Assert.AreNotEqual(response[2].Temp, null);
            Assert.AreNotEqual(response[2].Day, null);
            Assert.AreNotEqual(response[2].Wind, null);

            Assert.AreNotEqual(response[3].Condition, null);
            Assert.AreNotEqual(response[3].ImagePath, null);
            Assert.AreNotEqual(response[3].Temp, null);
            Assert.AreNotEqual(response[3].Day, null);
            Assert.AreNotEqual(response[3].Wind, null);

            Assert.AreNotEqual(response[4].Condition, null);
            Assert.AreNotEqual(response[4].ImagePath, null);
            Assert.AreNotEqual(response[4].Temp, null);
            Assert.AreNotEqual(response[4].Day, null);
            Assert.AreNotEqual(response[4].Wind, null);

            Assert.AreNotEqual(response[5].Condition, null);
            Assert.AreNotEqual(response[5].ImagePath, null);
            Assert.AreNotEqual(response[5].Temp, null);
            Assert.AreNotEqual(response[5].Day, null);
            Assert.AreNotEqual(response[5].Wind, null);
            #endregion


        }

        [TestMethod()]
        public async Task GetForecastTest()
        {
            // Setup
            var cache = new LHMTCache();

            // Act
            var response = await cache.GetForecast("vilnius");

            // Assert
            Assert.AreNotEqual(response, null);
            Assert.AreEqual(response.Place.Name, "Vilnius");

            Assert.AreNotEqual(response.ForecastTimestamps, null);
            Assert.IsTrue(response.ForecastTimestamps.Count > 0);
        }

        [TestMethod()]
        public async Task GetPlacesTest()
        {
            // Setup
            var cache = new LHMTCache();

            // Act
            var response = await cache.GetPlaces();

            // Assert
            Assert.AreNotEqual(response, null);
            Assert.IsTrue(response is List<Place>);
            Assert.IsTrue(response.Count > 0);
        }

        [TestMethod()]
        public async Task TestCacheDurations()
        {
            // Setup
            var cache = new LHMTCache();

            // Act
            var firstResponse = await cache.GetForecast("vilnius");
            var secondResponse = await cache.GetForecast("vilnius");

            // Assert
            if (firstResponse.ForecastCreationTimeUtc == secondResponse.ForecastCreationTimeUtc)
            {
                Assert.AreEqual(firstResponse, secondResponse);
            }

            //// Galima bandyt 3 valandas palaukt tarp atsakymų, kad atsinaujintu cache, bet labai nepraktiška
            //Thread.Sleep(11000000);
            //var thirdResponse = await cache.GetForecast("vilnius");
            //Assert.AreNotEqual(firstResponse, thirdResponse);
        }

    }
}