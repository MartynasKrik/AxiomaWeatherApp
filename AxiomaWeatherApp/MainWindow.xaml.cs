using AxiomaWeatherApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AxiomaWeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICache dataClient;
        public FullWeather fullWeather;
        private string selectedPlaceCode = Properties.Settings.Default.DefaultLocationCode;

        public MainWindow()
        {
            InitializeComponent();
            dataClient = new LHMTCache();

            fullWeather = new FullWeather();
            this.DataContext = fullWeather;

            LoadPlaces();
            LoadWeather(selectedPlaceCode);
        }

        /// <summary>
        /// Atnaujiną prognozes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RefreshBtnClick(object sender, RoutedEventArgs e)
        {
            await LoadWeather(selectedPlaceCode);
        }

        /// <summary>
        /// Nustato naujai pasirinktą vietą ir atnaujina prognozes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SelectBtnClick(object sender, RoutedEventArgs e)
        {
            if (PlacesDataGrid.SelectedItem is Place)
            {
                var selectedItem = (Place)PlacesDataGrid.SelectedItem;
                selectedPlaceCode = selectedItem.Code;
                await LoadWeather(selectedPlaceCode);
            }
        }

        /// <summary>
        /// Atnaujina orų prognozes
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        private async Task LoadWeather(string place)
        {
            fullWeather.CurrentWeather = await dataClient.GetCurrentWeather(place);
            fullWeather.HourlyForecasts = await dataClient.GetHourlyForecast(place);
            fullWeather.WeeklyForecasts = await dataClient.GetWeeklyForecast(place);
        }

        /// <summary>
        /// Vietovių užpildymas / filtravimas
        /// </summary>
        /// <returns></returns>
        private async Task LoadPlaces()
        {
            // Jeigu vietovės dar neužpildytos jas užpildo
            if (fullWeather.Places == null || fullWeather.Places.Count < 1)
            {
                fullWeather.Places = await dataClient.GetPlaces();
                PlacesDataGrid.ItemsSource = fullWeather.Places;
            }
            // Jeigu vietovės jau užpildytos tada filtruoja pagal paiešką
            else
            {
                var filteredPlaces = new List<Place>();
                filteredPlaces = fullWeather.Places.Where(f => f.Name.ToLower().Contains(fullWeather.SearchText.ToLower())).ToList();
                PlacesDataGrid.ItemsSource = filteredPlaces;
            }

            // Nustato vietovių sąrašo išvaizdą
            PlacesDataGrid.Columns[0].Width = new DataGridLength(10);
            PlacesDataGrid.Columns[1].Width = new DataGridLength(10, DataGridLengthUnitType.Star);
            PlacesDataGrid.Columns[2].Visibility = Visibility.Collapsed;
            PlacesDataGrid.Columns[0].Header = "";
            PlacesDataGrid.Columns[1].Header = "Pavadinimas";
        }

        /// <summary>
        /// Išvalo paieškos lauką jeigu užpildytas pradine reikšme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (fullWeather.SearchText == "Paieška...")
            {
                fullWeather.SearchText = "";
            }
        }

        /// <summary>
        /// Paieška
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchBtnClick(object sender, RoutedEventArgs e)
        {
            await LoadPlaces();
        }
    }
}
