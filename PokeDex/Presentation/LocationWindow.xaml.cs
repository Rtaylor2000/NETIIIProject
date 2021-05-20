using DataObject;
using Logic;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        private IPokemonLocationManager _pokemonLocationManager =
            new PokemonLocationManager();
        private Location _location;
        public LocationWindow(Location location)
        {
            _location = location;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblLocationName.Content = _location.LocationName;
            lblDescription.Content = _location.Description;

            try
            {
                if (dgLocationList.ItemsSource == null)
                {
                    dgLocationList.ItemsSource = _pokemonLocationManager.RetrievePokemonLocationByLocationName(_location.LocationName);

                    dgLocationList.Columns[0].Header = "Location Name";
                    dgLocationList.Columns[1].Header = "Pokemon Name";
                    dgLocationList.Columns[2].Header = "Game Name";
                    dgLocationList.Columns[3].Header = "How Found";
                    dgLocationList.Columns[4].Header = "Level Found";
                    dgLocationList.Columns[5].Header = "Species Encounter Rate";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}
