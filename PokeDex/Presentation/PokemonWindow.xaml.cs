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
    /// Interaction logic for PokemonWindow.xaml
    /// </summary>
    public partial class PokemonWindow : Window
    {
        private IPokemonLocationManager _pokemonLocationManager =
            new PokemonLocationManager();
        private IPokemonManager _pokemonManager = new PokemonManager();
        private Pokemon _pokemon;
        
        public PokemonWindow(Pokemon pokemon)
        {
            _pokemon = pokemon;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblDexNumber.Content = _pokemon.PokedexNumber;
            lblPokemonName.Content = _pokemon.PokemonName;
            lblDescription.Content = _pokemon.PokemonDescription;
            lblChatchRate.Content = "Chatch Rate: " + _pokemon.CatchRate;
            lblHP.Content = "Base HP: " + _pokemon.BaseHP;
            lblAttack.Content = "Base Attack: " + _pokemon.BaseAttack;
            lblDeffense.Content = "Base Defense: " + _pokemon.BaseDefense;
            lblSAttack.Content = "Base Special Attack: " + _pokemon.BaseSpecialAttack;
            lblSDeffense.Content = "Base Special Defense: " + _pokemon.BaseSpecialDefense;
            lblSpeed.Content = "Base Speed: " + _pokemon.BaseSpeed;

            try
            {
                if (dgPokemonList.ItemsSource == null)
                {
                    dgPokemonList.ItemsSource = _pokemonLocationManager.RetrievePokemonLocationByPokemon(_pokemon.PokemonName);

                    dgPokemonList.Columns[0].Header = "Location Name";
                    dgPokemonList.Columns[1].Header = "Pokemon Name";
                    dgPokemonList.Columns[2].Header = "Game Name";
                    dgPokemonList.Columns[3].Header = "How Found";
                    dgPokemonList.Columns[4].Header = "Level Found";
                    dgPokemonList.Columns[5].Header = "Species Encounter Rate";
                }
                if (dgEvolutionList.ItemsSource == null)
                {
                    dgEvolutionList.ItemsSource = _pokemonManager.RetrieveEvolutionByReactant(_pokemon.PokemonName);

                    dgEvolutionList.Columns[0].Header = "Reactant";
                    dgEvolutionList.Columns[1].Header = "Evolution Condition";
                    dgEvolutionList.Columns[2].Header = "Evolves Into";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        }
    }
}
