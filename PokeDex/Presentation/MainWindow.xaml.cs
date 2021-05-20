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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserManager _userManager = new UserManager();
        private IPokemonLocationManager _pokemonLocationManager =
            new PokemonLocationManager();
        private IPokemonManager _pokemonManager = new PokemonManager();
        private User _user = null;
        private bool _pokemonUpdated = false;
        private bool _locationUpdated = false;
        private bool _memberUpdated = false;

        PokemonLocation _oldPokemonLocation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mnuItemUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                var updatePassword = new UpdatePassword(_user, _userManager);
                updatePassword.ShowDialog();
            }
            else 
            {
                MessageBox.Show("You are not logged in.");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnLogin.Content == "Login") 
            {
                try
                {
                    _user = _userManager.AuthenticateUser(txtUserName.Text, 
                        pwdPassword.Password);
                    //MessageBox.Show(_user.FirstName + " logged in successfully");
                    btnLogin.Content = "Logout";
                    txtUserName.Text = "";

                    if (pwdPassword.Password == "newuser")
                    {
                        var updatePassword = new UpdatePassword(_user, _userManager, true);
                        if (!updatePassword.ShowDialog() == true)
                        {
                            resetWindow();
                            _user = null;
                            return;
                        }
                    }

                    btnLogin.IsDefault = false; // stops the enter button from loging people out
                    pwdPassword.Password = "";
                    txtUserName.Visibility = Visibility.Hidden;
                    lblUserName.Visibility = Visibility.Hidden;
                    pwdPassword.Visibility = Visibility.Hidden;
                    lblPassword.Visibility = Visibility.Hidden;
                    sbarItemMessage.Content = "";


                    mnuMain.IsEnabled = true;
                    showUserTabls();

                    lblGreeting.Content = "Welcome back, " + _user.FirstName + " " + _user.LastName;

                    lblRoles.Content = "You are logged in as: " + _user.Role;


                }
                catch (Exception ex)
                {
                    pwdPassword.Clear();
                    txtUserName.Clear();
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    txtUserName.Focus();
                }
            }
            else 
            {
                _user = null;

                resetWindow();
            }
        }

        private void hideAllTabs()
        {
            foreach (TabItem t in tabSetMain.Items)
            {
                t.Visibility = Visibility.Collapsed;
            }
            grdAdd.Visibility = Visibility.Hidden;
            grdLocation.Visibility = Visibility.Hidden;
            grdPokemon.Visibility = Visibility.Hidden;
            grdUpdate.Visibility = Visibility.Hidden;
            grdUserAdmin.Visibility = Visibility.Hidden;
            grdDelete.Visibility = Visibility.Hidden;
        }

        private void showUserTabls()
        {
            switch (_user.Role)
            {
                case "admin":

                    tabAdd.Visibility = Visibility.Visible;
                    tabLocation.Visibility = Visibility.Visible;
                    tabPokemon.Visibility = Visibility.Visible;
                    tabUpdate.Visibility = Visibility.Visible;
                    tabUserEditor.Visibility = Visibility.Visible;
                    tabDelete.Visibility = Visibility.Visible;

                    grdAdd.Visibility = Visibility.Visible;
                    grdLocation.Visibility = Visibility.Visible;
                    grdPokemon.Visibility = Visibility.Visible;
                    grdUpdate.Visibility = Visibility.Visible;
                    grdDelete.Visibility = Visibility.Visible;
                    grdUserAdmin.Visibility = Visibility.Visible;

                    tabUserEditor.IsSelected = true;
                    break;
                case "researcher":

                    tabAdd.Visibility = Visibility.Visible;
                    tabLocation.Visibility = Visibility.Visible;
                    tabPokemon.Visibility = Visibility.Visible;

                    grdAdd.Visibility = Visibility.Visible;
                    grdLocation.Visibility = Visibility.Visible;
                    grdPokemon.Visibility = Visibility.Visible;

                    tabAdd.IsSelected = true;
                    break;
                case "user":

                    tabLocation.Visibility = Visibility.Visible;
                    tabPokemon.Visibility = Visibility.Visible;

                    grdLocation.Visibility = Visibility.Visible;
                    grdPokemon.Visibility = Visibility.Visible;

                    tabPokemon.IsSelected = true;
                    break;
                default:
                    break;
            }
        }

        private void resetWindow()
        {
            btnLogin.IsDefault = true;
            hideAllTabs();
            mnuMain.IsEnabled = false;
            txtUserName.Text = "";
            pwdPassword.Password = "";
            btnLogin.Content = "Login";
            lblGreeting.Content = "Greetings.";
            lblRoles.Content = "You are not logged in";
            txtUserName.Visibility = Visibility.Visible;
            lblUserName.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            sbarItemMessage.Content = "Please login to continue.";

            //new close 
            dgUserList.ItemsSource = null;

            txtUserName.Focus();
        }

        private void btnCreatAccount_Click(object sender, RoutedEventArgs e)
        {
            var editProfile = new MemberAddEdit();
            editProfile.ShowDialog();
            _memberUpdated = true;
        }

        private void mnuItemUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                var editProfile = new MemberAddEdit(_user);
                editProfile.ShowDialog();
                lblGreeting.Content = "Welcome back, " + _user.FirstName + " " + _user.LastName;
                _memberUpdated = true;
            }
            else
            {
                MessageBox.Show("You are not logged in.");
            }
        }

        private void tabPokemon_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    if (dgPokemonList.ItemsSource == null || _pokemonUpdated == true)
                    {
                        dgPokemonList.ItemsSource = _pokemonManager.RetrieveAllPokemon();

                        dgPokemonList.Columns[0].Header = "Pokedex Number";
                        dgPokemonList.Columns[1].Header = "Pokemon Name";
                        dgPokemonList.Columns[2].Header = "Type One";
                        dgPokemonList.Columns[3].Header = "Type Two";
                        dgPokemonList.Columns[4].Header = "Catch Rate";
                        dgPokemonList.Columns[5].Header = "Base HP";
                        dgPokemonList.Columns[6].Header = "Base Attack";
                        dgPokemonList.Columns[7].Header = "Base Defense";
                        dgPokemonList.Columns[8].Header = "Base Special Attack";
                        dgPokemonList.Columns[9].Header = "Base Special Defense";
                        dgPokemonList.Columns[10].Header = "Base Speed";
                        dgPokemonList.Columns[11].Header = "Pokedex Entry";

                        _pokemonUpdated = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnPNameSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> pokemonNames = getPokemonNames();
                if (txtPokemonName.Text.isValidPokemonName() && pokemonNames.Contains(txtPokemonName.Text.Trim()))
                {
                    var pokemonWindow = new PokemonWindow(
                        _pokemonManager.RetrievePokemonByName(txtPokemonName.Text));
                    pokemonWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Pokemon " + txtPokemonName.Text +" does not exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnPDexNumberSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<int> allPokedexNumbers = getPokedexNumbers();
                if (allPokedexNumbers.Contains(int.Parse(txtPokemonDexNumber.Text.Trim())))
                {
                    var pokemonWindow = new PokemonWindow(
                        _pokemonManager.RetrievePokmeonByNumber(int.Parse(txtPokemonDexNumber.Text)));
                    pokemonWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Pokedex number " + txtPokemonDexNumber.Text + " Is Not Registerd");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnPTypeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtPokemonType.Text.isValidTypeOne())
            {
                dgPokemonList.ItemsSource = _pokemonManager.RetrievePokemonByType(
                    txtPokemonType.Text);
            }
            else 
            {
                MessageBox.Show("Incorrect Pokemon Type" + "\n\n" +
                    txtPokemonType.Text+ " is not a pokemon type");
            }
        }

        private void dgPokemonList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Pokemon)dgPokemonList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            var pokemonWindow = new PokemonWindow(selectedItem);
            pokemonWindow.ShowDialog();
        }

        private void tabLocation_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    if (dgLocationList.ItemsSource == null || _locationUpdated == true)
                    {
                        dgLocationList.ItemsSource = 
                            _pokemonLocationManager.RetrieveAllLocation();

                        dgLocationList.Columns[0].Header = "Location Name";
                        dgLocationList.Columns[1].Header = "Location Description";
                        
                        _locationUpdated = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void dgLocationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Location)dgLocationList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            var locationWindow = new LocationWindow(selectedItem);
            locationWindow.ShowDialog();
        }

        private void btnAddPokemon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number = -1;
                List<string> pokemonNames = getPokemonNames();
                List<int> pokedexNumbers = getPokedexNumbers();
                if (!txtNewPokemonName.Text.isValidPokemonName() ||
                    pokemonNames.Contains(txtNewPokemonName.Text)) 
                {
                    MessageBox.Show("Invalid Pokemon Name.");
                    txtNewPokemonName.Focus();
                    txtNewPokemonName.SelectAll();
                    return;
                }
                if (!txtNewTypeOne.Text.isValidTypeOne())
                {
                    MessageBox.Show("Invalid Type One.");
                    txtNewTypeOne.Focus();
                    txtNewTypeOne.SelectAll();
                    return;
                }
                if (!txtNewTypeTwo.Text.isValidTypeTwo())
                {
                    MessageBox.Show("Invalid Type Two.(None is valid)");
                    txtNewTypeTwo.Focus();
                    txtNewTypeTwo.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewDexNumber.Text.Trim(), out number)|| 
                    pokedexNumbers.Contains(int.Parse(txtNewDexNumber.Text.Trim())))
                {
                    MessageBox.Show("Invalid Pokedex Number.");
                    txtNewDexNumber.Focus();
                    txtNewDexNumber.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewCatchRate.Text.Trim(), out number))
                {
                    MessageBox.Show("The Catch Rate must be a number.");
                    txtNewCatchRate.Focus();
                    txtNewCatchRate.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewHP.Text.Trim(), out number))
                {
                    MessageBox.Show("The Base HP must be a number.");
                    txtNewHP.Focus();
                    txtNewHP.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewAtt.Text.Trim(), out number))
                {
                    MessageBox.Show("The Base Attack must be a number.");
                    txtNewAtt.Focus();
                    txtNewAtt.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewDef.Text.Trim(), out number))
                {
                    MessageBox.Show("The Base Deffense must be a number.");
                    txtNewDef.Focus();
                    txtNewDef.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewSAtt.Text.Trim(), out number))
                {
                    MessageBox.Show("The Base Special Attack must be a number.");
                    txtNewSAtt.Focus();
                    txtNewSAtt.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewSDef.Text.Trim(), out number))
                {
                    MessageBox.Show("The Base Special Deffense must be a number.");
                    txtNewSDef.Focus();
                    txtNewSDef.SelectAll();
                    return;
                }
                if (!int.TryParse(txtNewSpeed.Text.Trim(), out number))
                {
                    MessageBox.Show("The Base Speed must be a number.");
                    txtNewSpeed.Focus();
                    txtNewSpeed.SelectAll();
                    return;
                }
                if (txtNewPokemonDescription.Text.Trim() == "")
                {
                    MessageBox.Show("The Pokemon Description can not be empty.");
                    txtNewPokemonDescription.Focus();
                    txtNewPokemonDescription.SelectAll();
                    return;
                }
                var newPokemon = new Pokemon()
                {
                    PokedexNumber = int.Parse(txtNewDexNumber.Text.Trim()),
                    PokemonName = txtNewPokemonName.Text.Trim(),
                    TypeOne = txtNewTypeOne.Text.ToUpper().Trim(),
                    TypeTwo = txtNewTypeTwo.Text.Trim(),
                    CatchRate = int.Parse(txtNewCatchRate.Text.Trim()),
                    BaseHP = int.Parse(txtNewHP.Text.Trim()),
                    BaseAttack = int.Parse(txtNewAtt.Text.Trim()),
                    BaseDefense = int.Parse(txtNewDef.Text.Trim()),
                    BaseSpecialAttack = int.Parse(txtNewSAtt.Text.Trim()),
                    BaseSpecialDefense = int.Parse(txtNewSDef.Text.Trim()),
                    BaseSpeed = int.Parse(txtNewSpeed.Text.Trim()),
                    PokemonDescription = txtNewPokemonDescription.Text.Trim()
                };
                _pokemonManager.AddNewPokemon(newPokemon);
                _pokemonUpdated = true;
                txtNewDexNumber.Text = "";
                txtNewPokemonName.Text = "";
                txtNewTypeOne.Text = "";
                txtNewTypeTwo.Text = "";
                txtNewCatchRate.Text = "";
                txtNewHP.Text = "";
                txtNewAtt.Text = "";
                txtNewDef.Text = "";
                txtNewSAtt.Text = "";
                txtNewSDef.Text = "";
                txtNewSpeed.Text = "";
                txtNewPokemonDescription.Text = "";
                MessageBox.Show("Pokemon "+newPokemon.PokemonName+" was added.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnAddEvolution_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> allPokemonNames = getPokemonNames();
                if (!txtNewReactant.Text.isValidPokemonName() 
                    || !allPokemonNames.Contains(txtNewReactant.Text))
                {
                    MessageBox.Show("Pokemon " + txtNewReactant.Text + " Dose Not Exist.");
                    txtNewReactant.Focus();
                    txtNewReactant.SelectAll();
                    return;
                }
                if (!txtNewEvolvesInto.Text.isValidPokemonName() 
                    || !allPokemonNames.Contains(txtNewEvolvesInto.Text))
                {
                    MessageBox.Show("Pokemon "+txtNewEvolvesInto.Text+" Dose Not Exist.");
                    txtNewEvolvesInto.Focus();
                    txtNewEvolvesInto.SelectAll();
                    return;
                }
                if (txtNewEvolutionCondition.Text.Trim() == "")
                {
                    MessageBox.Show("The Evolution Condition can not be empty.");
                    txtNewEvolutionCondition.Focus();
                    txtNewEvolutionCondition.SelectAll();
                    return;
                }
                var newEvolution = new Evolution()
                {
                    Reactant = txtNewReactant.Text.Trim(),
                    EvolvesInto = txtNewEvolvesInto.Text.Trim(),
                    EvolutionCondition = txtNewEvolutionCondition.Text.Trim()
                };
                _pokemonManager.AddNewEvolution(newEvolution);
                txtNewReactant.Text = "";
                txtNewEvolvesInto.Text = "";
                txtNewEvolutionCondition.Text = "";
                MessageBox.Show("Evolution between " + newEvolution.Reactant + " and "
                    + newEvolution.EvolvesInto + " was added.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnAddPokemonLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> allPokemonNames = getPokemonNames();
                List<string> allLocationNames = getLocationNames();
                if (!txtNewPokemonLocationName.Text.isValidLocationName() 
                    || !allLocationNames.Contains(txtNewPokemonLocationName.Text))
                {
                    MessageBox.Show("Location "+txtNewPokemonLocationName.Text+ " Does Not Exist");
                    txtNewPokemonLocationName.Focus();
                    txtNewPokemonLocationName.SelectAll();
                    return;
                }
                if (!txtNewPokemon.Text.isValidPokemonName()
                    || !allPokemonNames.Contains(txtNewPokemon.Text))
                {
                    MessageBox.Show("Pokemon " + txtNewPokemon.Text + " not found.");
                    txtNewPokemon.Focus();
                    txtNewPokemon.SelectAll();
                    return;
                }
                if (!txtNewGame.Text.isValidGameName())
                {
                    MessageBox.Show("Invalid Game Name.");
                    txtNewGame.Focus();
                    txtNewGame.SelectAll();
                    return;
                }
                if (!txtNewLevel.Text.isValidLevelFound())
                {
                    MessageBox.Show("Invalid Level Found.");
                    txtNewLevel.Focus();
                    txtNewLevel.SelectAll();
                    return;
                }
                if (txtNewFound.Text.Trim() == "")
                {
                    MessageBox.Show("How Found Can Not Be Blank");
                    txtNewFound.Focus();
                    txtNewFound.SelectAll();
                    return;
                }
                if (txtNewEncounter.Text.Trim() == "")
                {
                    MessageBox.Show("Encounter Rate Can Not Be Blank");
                    txtNewEncounter.Focus();
                    txtNewEncounter.SelectAll();
                    return;
                }
                var newPokemonLocation = new PokemonLocation()
                {
                    LocationName = txtNewPokemonLocationName.Text.Trim(),
                    PokemonName = txtNewPokemon.Text.Trim(),
                    GameName = txtNewGame.Text.Trim(),
                    LevelFound = txtNewLevel.Text.Trim(),
                    HowFound = txtNewFound.Text.Trim(),
                    SpeciesEncounterRate = txtNewEncounter.Text.Trim()
                };
                _pokemonLocationManager.AddNewPokemonLocation(newPokemonLocation);
                txtNewPokemonLocationName.Text = "";
                txtNewPokemon.Text = "";
                txtNewGame.Text = "";
                txtNewLevel.Text = "";
                txtNewFound.Text = "";
                txtNewEncounter.Text = "";
                MessageBox.Show("Pokemon Location " + newPokemonLocation.LocationName + " containing "
                    + newPokemonLocation.PokemonName + " was added.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private List<string> getPokemonNames()
        {
            List<string> allPokemonNames = new List<string>();
            try
            {
                List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
                foreach (Pokemon pokemon in allPokemon)
                {
                    allPokemonNames.Add(pokemon.PokemonName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            return allPokemonNames;
        }

        private List<string> getLocationNames()
        {
            List<string> allLocationNames = new List<string>();
            try
            {
                List<Location> allLocations = _pokemonLocationManager.RetrieveAllLocation();
                foreach (Location location in allLocations)
                {
                    allLocationNames.Add(location.LocationName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            return allLocationNames;
        }

        private List<int> getPokedexNumbers() 
        {
            List<int> allPokedexNumbers = new List<int>();
            try
            {
                List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
                foreach (Pokemon pokemon in allPokemon)
                {
                    allPokedexNumbers.Add(pokemon.PokedexNumber);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            return allPokedexNumbers;
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!txtNewLocationName.Text.Trim().isValidLocationName())
                {
                    MessageBox.Show("Invalid Location Name.");
                    txtNewLocationName.Focus();
                    txtNewLocationName.SelectAll();
                    return;
                }
                if (txtNewLocationDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Location Description Can Not Be Blank.");
                    txtNewLocationDescription.Focus();
                    txtNewLocationDescription.SelectAll();
                    return;
                }
                var newLocation = new Location()
                {
                    LocationName = txtNewLocationName.Text.Trim(),
                    Description = txtNewLocationDescription.Text.Trim()
                };
                _pokemonLocationManager.AddNewLocation(newLocation);
                txtNewLocationName.Text = "";
                txtNewLocationDescription.Text = "";
                MessageBox.Show("Location "+newLocation.LocationName+" was added");
                _locationUpdated = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
        private void btnUpdatePokemon_Click(object sender, RoutedEventArgs e)
        {
            Pokemon pokemon = new Pokemon();
            if ((string)btnUpdatePokemon.Content == "Find Pokemon")
            {
                txtUpdatePokemonName.BorderBrush = Brushes.Black;
                txtUpdatePokemonName.IsReadOnly = false;
                try
                {
                    List<string> allPokemonNames = getPokemonNames();
                    if (!txtUpdatePokemonName.Text.isValidPokemonName()
                        || !allPokemonNames.Contains(txtUpdatePokemonName.Text))
                    {
                        MessageBox.Show("Pokemon " + txtUpdatePokemonName.Text +
                            " Dose Not Exist.");
                        txtUpdatePokemonName.Focus();
                        txtUpdatePokemonName.SelectAll();
                        return;
                    }
                    pokemon = _pokemonManager.RetrievePokemonByName(
                        txtUpdatePokemonName.Text.Trim());
                    txtUpdatePokemonName.IsReadOnly = true;
                    txtUpdateDexNumber.IsReadOnly = false;
                    txtUpdateTypeOne.IsReadOnly = false;
                    txtUpdateTypeTwo.IsReadOnly = false;
                    txtUpdateCatchRate.IsReadOnly = false;
                    txtUpdateHP.IsReadOnly = false;
                    txtUpdateAtt.IsReadOnly = false;
                    txtUpdateDef.IsReadOnly = false;
                    txtUpdateSAtt.IsReadOnly = false;
                    txtUpdateSDef.IsReadOnly = false;
                    txtUpdateSpeed.IsReadOnly = false;
                    txtUpdateDesc.IsReadOnly = false;

                    txtUpdatePokemonName.BorderBrush = Brushes.White;
                    txtUpdateDexNumber.BorderBrush = Brushes.Black;
                    txtUpdateTypeOne.BorderBrush = Brushes.Black;
                    txtUpdateTypeTwo.BorderBrush = Brushes.Black;
                    txtUpdateCatchRate.BorderBrush = Brushes.Black;
                    txtUpdateHP.BorderBrush = Brushes.Black;
                    txtUpdateAtt.BorderBrush = Brushes.Black;
                    txtUpdateDef.BorderBrush = Brushes.Black;
                    txtUpdateSAtt.BorderBrush = Brushes.Black;
                    txtUpdateSDef.BorderBrush = Brushes.Black;
                    txtUpdateSpeed.BorderBrush = Brushes.Black;
                    txtUpdateDesc.BorderBrush = Brushes.Black;

                    txtUpdateDexNumber.Text = pokemon.PokedexNumber.ToString();
                    txtUpdateTypeOne.Text = pokemon.TypeOne;
                    txtUpdateTypeTwo.Text = pokemon.TypeTwo;
                    txtUpdateCatchRate.Text = pokemon.CatchRate.ToString();
                    txtUpdateHP.Text = pokemon.BaseHP.ToString();
                    txtUpdateAtt.Text = pokemon.BaseAttack.ToString();
                    txtUpdateDef.Text = pokemon.BaseDefense.ToString();
                    txtUpdateSAtt.Text = pokemon.BaseSpecialAttack.ToString();
                    txtUpdateSDef.Text = pokemon.BaseSpecialDefense.ToString();
                    txtUpdateSpeed.Text = pokemon.BaseSpeed.ToString();
                    txtUpdateDesc.Text = pokemon.PokemonDescription;

                    btnUpdatePokemon.Content = "Save Pokemon";
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else 
            {
                try
                {
                    int number = -1;
                    pokemon = _pokemonManager.RetrievePokemonByName(
                        txtUpdatePokemonName.Text.Trim());
                    List<int> pokedexNumbers = getPokedexNumbers();
                    if (!int.TryParse(txtUpdateDexNumber.Text.Trim(), out number) ||
                        (pokedexNumbers.Contains(int.Parse(txtUpdateDexNumber.Text.Trim()))
                        && !(int.Parse(txtUpdateDexNumber.Text.Trim()) == pokemon.PokedexNumber)))
                    {
                        MessageBox.Show("Invalid Pokedex Number.");
                        txtUpdateDexNumber.Focus();
                        txtUpdateDexNumber.SelectAll();
                        return;
                    }
                    if (!txtUpdateTypeOne.Text.isValidTypeOne())
                    {
                        MessageBox.Show("Invalid Type One.");
                        txtUpdateTypeOne.Focus();
                        txtUpdateTypeOne.SelectAll();
                        return;
                    }
                    if (!txtUpdateTypeTwo.Text.isValidTypeTwo())
                    {
                        MessageBox.Show("Invalid Type Two.(None is valid)");
                        txtUpdateTypeTwo.Focus();
                        txtUpdateTypeTwo.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateCatchRate.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Catch Rate must be a number.");
                        txtUpdateCatchRate.Focus();
                        txtUpdateCatchRate.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateHP.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Base HP must be a number.");
                        txtUpdateHP.Focus();
                        txtUpdateHP.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateAtt.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Base Attack must be anumber.");
                        txtUpdateAtt.Focus();
                        txtUpdateAtt.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateDef.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Base Deffense must be a number.");
                        txtUpdateDef.Focus();
                        txtUpdateDef.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateSAtt.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Base Special Attack must be a number.");
                        txtUpdateSAtt.Focus();
                        txtUpdateSAtt.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateSDef.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Base Special Deffense must be a number.");
                        txtUpdateSDef.Focus();
                        txtUpdateSDef.SelectAll();
                        return;
                    }
                    if (!int.TryParse(txtUpdateSpeed.Text.Trim(), out number))
                    {
                        MessageBox.Show("The Base Speed must be a number.");
                        txtUpdateSpeed.Focus();
                        txtUpdateSpeed.SelectAll();
                        return;
                    }
                    if (txtUpdateDesc.Text.Trim() == "")
                    {
                        MessageBox.Show("The Pokemon Description can not be empty.");
                        txtUpdateDesc.Focus();
                        txtUpdateDesc.SelectAll();
                        return;
                    }
                    var newPokemon = new Pokemon()
                    {
                        PokedexNumber = int.Parse(txtUpdateDexNumber.Text.Trim()),
                        PokemonName = txtUpdatePokemonName.Text.Trim(),
                        TypeOne = txtUpdateTypeOne.Text.ToUpper().Trim(),
                        TypeTwo = txtUpdateTypeTwo.Text.Trim(),
                        CatchRate = int.Parse(txtUpdateCatchRate.Text.Trim()),
                        BaseHP = int.Parse(txtUpdateHP.Text.Trim()),
                        BaseAttack = int.Parse(txtUpdateAtt.Text.Trim()),
                        BaseDefense = int.Parse(txtUpdateDef.Text.Trim()),
                        BaseSpecialAttack = int.Parse(txtUpdateSAtt.Text.Trim()),
                        BaseSpecialDefense = int.Parse(txtUpdateSDef.Text.Trim()),
                        BaseSpeed = int.Parse(txtUpdateSpeed.Text.Trim()),
                        PokemonDescription = txtUpdateDesc.Text.Trim()
                    };
                    txtUpdatePokemonName.BorderBrush = Brushes.Black;
                    txtUpdateDexNumber.BorderBrush = Brushes.White;
                    txtUpdateTypeOne.BorderBrush = Brushes.White;
                    txtUpdateTypeTwo.BorderBrush = Brushes.White;
                    txtUpdateCatchRate.BorderBrush = Brushes.White;
                    txtUpdateHP.BorderBrush = Brushes.White;
                    txtUpdateAtt.BorderBrush = Brushes.White;
                    txtUpdateDef.BorderBrush = Brushes.White;
                    txtUpdateSAtt.BorderBrush = Brushes.White;
                    txtUpdateSDef.BorderBrush = Brushes.White;
                    txtUpdateSpeed.BorderBrush = Brushes.White;
                    txtUpdateDesc.BorderBrush = Brushes.White;
                    _pokemonManager.EditPokemon(pokemon, newPokemon);
                    _pokemonUpdated = true;

                    txtUpdatePokemonName.Text = "";
                    txtUpdateDexNumber.Text = "";
                    txtUpdateTypeOne.Text = "";
                    txtUpdateTypeTwo.Text = "";
                    txtUpdateCatchRate.Text = "";
                    txtUpdateHP.Text = "";
                    txtUpdateAtt.Text = "";
                    txtUpdateDef.Text = "";
                    txtUpdateSAtt.Text = "";
                    txtUpdateSDef.Text = "";
                    txtUpdateSpeed.Text = "";
                    txtUpdateDesc.Text = "";

                    txtUpdatePokemonName.IsReadOnly = false;
                    txtUpdateDexNumber.IsReadOnly = true;
                    txtUpdateTypeOne.IsReadOnly = true;
                    txtUpdateTypeTwo.IsReadOnly = true;
                    txtUpdateCatchRate.IsReadOnly = true;
                    txtUpdateHP.IsReadOnly = true;
                    txtUpdateAtt.IsReadOnly = true;
                    txtUpdateDef.IsReadOnly = true;
                    txtUpdateSAtt.IsReadOnly = true;
                    txtUpdateSDef.IsReadOnly = true;
                    txtUpdateSpeed.IsReadOnly = true;
                    txtUpdateDesc.IsReadOnly = true;

                    btnUpdatePokemon.Content = "Find Pokemon";
                    MessageBox.Show("Pokemon " + newPokemon.PokemonName + " was updated.");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnCanclePokemon_Click(object sender, RoutedEventArgs e)
        {
            txtUpdatePokemonName.BorderBrush = Brushes.Black;
            txtUpdateDexNumber.BorderBrush = Brushes.White;
            txtUpdateTypeOne.BorderBrush = Brushes.White;
            txtUpdateTypeTwo.BorderBrush = Brushes.White;
            txtUpdateCatchRate.BorderBrush = Brushes.White;
            txtUpdateHP.BorderBrush = Brushes.White;
            txtUpdateAtt.BorderBrush = Brushes.White;
            txtUpdateDef.BorderBrush = Brushes.White;
            txtUpdateSAtt.BorderBrush = Brushes.White;
            txtUpdateSDef.BorderBrush = Brushes.White;
            txtUpdateSpeed.BorderBrush = Brushes.White;
            txtUpdateDesc.BorderBrush = Brushes.White;

            txtUpdatePokemonName.Text = "";
            txtUpdateDexNumber.Text = "";
            txtUpdateTypeOne.Text = "";
            txtUpdateTypeTwo.Text = "";
            txtUpdateCatchRate.Text = "";
            txtUpdateHP.Text = "";
            txtUpdateAtt.Text = "";
            txtUpdateDef.Text = "";
            txtUpdateSAtt.Text = "";
            txtUpdateSDef.Text = "";
            txtUpdateSpeed.Text = "";
            txtUpdateDesc.Text = "";

            txtUpdatePokemonName.IsReadOnly = false;
            txtUpdateDexNumber.IsReadOnly = true;
            txtUpdateTypeOne.IsReadOnly = true;
            txtUpdateTypeTwo.IsReadOnly = true;
            txtUpdateCatchRate.IsReadOnly = true;
            txtUpdateHP.IsReadOnly = true;
            txtUpdateAtt.IsReadOnly = true;
            txtUpdateDef.IsReadOnly = true;
            txtUpdateSAtt.IsReadOnly = true;
            txtUpdateSDef.IsReadOnly = true;
            txtUpdateSpeed.IsReadOnly = true;
            txtUpdateDesc.IsReadOnly = true;
            btnUpdatePokemon.Content = "Find Pokemon";
        }

        private void btnUpdateEvolution_Click(object sender, RoutedEventArgs e)
        {
            List<Evolution> evolutions = new List<Evolution>();
            Evolution oldEvolution = new Evolution();
            List<string> allPokemonNames = getPokemonNames();
            if ((string)btnUpdateEvolution.Content == "Find Evolution")
            {
                try
                {
                    if (!txtUpdateEvolvesInto.Text.isValidPokemonName()
                        || !allPokemonNames.Contains(txtUpdateEvolvesInto.Text))
                    {
                        MessageBox.Show("Pokemon " + txtUpdateEvolvesInto.Text +
                            " Dose Not Exist.");
                        txtUpdateEvolvesInto.Focus();
                        txtUpdateEvolvesInto.SelectAll();
                        return;
                    }
                    evolutions = _pokemonManager.RetrieveEvolutionByEvolvesInto(
                        txtUpdateEvolvesInto.Text.Trim());
                    oldEvolution = evolutions[0];

                    txtUpdateReactant.Text = oldEvolution.Reactant;
                    txtUpdateEvolutionCondition.Text = oldEvolution.EvolutionCondition;

                    txtUpdateEvolvesInto.BorderBrush = Brushes.White;
                    txtUpdateReactant.BorderBrush = Brushes.Black;
                    txtUpdateEvolutionCondition.BorderBrush = Brushes.Black;

                    txtUpdateEvolvesInto.IsReadOnly = true;
                    txtUpdateReactant.IsReadOnly = false;
                    txtUpdateEvolutionCondition.IsReadOnly = false;

                    btnUpdateEvolution.Content = "Update Evolution";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                try
                {
                    List<Evolution> manyEvolutions = 
                        _pokemonManager.RetrieveEvolutionByEvolvesInto(
                        txtUpdateEvolvesInto.Text.Trim());
                    Evolution evolution = manyEvolutions[0];
                    if (!txtUpdateReactant.Text.isValidPokemonName()
                    || !allPokemonNames.Contains(txtUpdateReactant.Text))
                    {
                        MessageBox.Show("Pokemon " + txtUpdateReactant.Text 
                            + " Dose Not Exist.");
                        txtUpdateReactant.Focus();
                        txtUpdateReactant.SelectAll();
                        return;
                    }
                    if (!txtUpdateEvolvesInto.Text.isValidPokemonName()
                        || !allPokemonNames.Contains(txtUpdateEvolvesInto.Text))
                    {
                        MessageBox.Show("Pokemon " + txtUpdateEvolvesInto.Text 
                            + " Dose Not Exist.");
                        txtUpdateEvolvesInto.Focus();
                        txtUpdateEvolvesInto.SelectAll();
                        return;
                    }
                    if (txtUpdateEvolutionCondition.Text.Trim() == "")
                    {
                        MessageBox.Show("The Evolution Condition can not be empty.");
                        txtUpdateEvolutionCondition.Focus();
                        txtUpdateEvolutionCondition.SelectAll();
                        return;
                    }
                    Evolution newEvolution = new Evolution()
                    {
                        EvolutionCondition = txtUpdateEvolutionCondition.Text.Trim(),
                        EvolvesInto = txtUpdateEvolvesInto.Text.Trim(),
                        Reactant = txtUpdateReactant.Text.Trim()
                    };
                    _pokemonManager.EditEvolution(evolution, newEvolution);
                    btnUpdateEvolution.Content = "Find Evolution"; 
                    txtUpdateEvolvesInto.Text = "";
                    txtUpdateReactant.Text = "";
                    txtUpdateEvolutionCondition.Text = "";

                    txtUpdateEvolvesInto.BorderBrush = Brushes.Black;
                    txtUpdateReactant.BorderBrush = Brushes.White;
                    txtUpdateEvolutionCondition.BorderBrush = Brushes.White;

                    txtUpdateEvolvesInto.IsReadOnly = false;
                    txtUpdateReactant.IsReadOnly = true;
                    txtUpdateEvolutionCondition.IsReadOnly = true;
                    MessageBox.Show("Evolution has been updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnCancleEvolution_Click(object sender, RoutedEventArgs e)
        {
            txtUpdateEvolvesInto.BorderBrush = Brushes.Black;
            txtUpdateReactant.BorderBrush = Brushes.White;
            txtUpdateEvolutionCondition.BorderBrush = Brushes.White;

            txtUpdateEvolvesInto.Text = "";
            txtUpdateReactant.Text = "";
            txtUpdateEvolutionCondition.Text = "";

            txtUpdateEvolvesInto.IsReadOnly = false;
            txtUpdateReactant.IsReadOnly = true;
            txtUpdateEvolutionCondition.IsReadOnly = true;
            btnUpdateEvolution.Content = "Find Evolution";
        }

        private void btnUpdatePokemonLocation_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnUpdatePokemonLocation.Content == "Find Pokemon Location")
            {
                try
                {
                    List<string> allPokemonNames = getPokemonNames();
                    List<string> allLocationNames = getLocationNames();

                    if (!txtUpdateLocation.Text.isValidLocationName()
                    || !allLocationNames.Contains(txtUpdateLocation.Text))
                    {
                        MessageBox.Show("Location " + txtUpdateLocation.Text 
                            + " Does Not Exist");
                        txtUpdateLocation.Focus();
                        txtUpdateLocation.SelectAll();
                        return;
                    }
                    if (!txtUpdatePokemon.Text.isValidPokemonName()
                        || !allPokemonNames.Contains(txtUpdatePokemon.Text))
                    {
                        MessageBox.Show("Pokemon " + txtUpdatePokemon.Text + " not found.");
                        txtUpdatePokemon.Focus();
                        txtUpdatePokemon.SelectAll();
                        return;
                    }
                    if (!txtUpdateGame.Text.isValidGameName())
                    {
                        MessageBox.Show("Invalid Game Name.");
                        txtUpdateGame.Focus();
                        txtUpdateGame.SelectAll();
                        return;
                    }
                    if (!txtUpdateLevel.Text.isValidLevelFound())
                    {
                        MessageBox.Show("Invalid Level Found.");
                        txtUpdateLevel.Focus();
                        txtUpdateLevel.SelectAll();
                        return;
                    }
                    if (txtUpdateFound.Text.Trim() == "")
                    {
                        MessageBox.Show("How Found Can Not Be Blank");
                        txtUpdateFound.Focus();
                        txtUpdateFound.SelectAll();
                        return;
                    }
                    if (txtUpdateEncounter.Text.Trim() == "")
                    {
                        MessageBox.Show("Encounter Rate Can Not Be Blank");
                        txtUpdateEncounter.Focus();
                        txtUpdateEncounter.SelectAll();
                        return;
                    }

                    List<PokemonLocation> pokemonLocations =
                        _pokemonLocationManager.RetrievePokemonLocationByLocationName(
                            txtUpdateLocation.Text);
                    _oldPokemonLocation = new PokemonLocation()
                    {
                        LocationName = txtUpdateLocation.Text.Trim(),
                        PokemonName = txtUpdatePokemon.Text.Trim(),
                        GameName = txtUpdateGame.Text.Trim(),
                        LevelFound = txtUpdateLevel.Text.Trim(),
                        HowFound = txtUpdateFound.Text.Trim(),
                        SpeciesEncounterRate = txtUpdateEncounter.Text.Trim(),
                    };
                    bool found = false;
                    foreach (PokemonLocation onePokmeonLocation in pokemonLocations)
                    {
                        if (onePokmeonLocation.LocationName ==
                            _oldPokemonLocation.LocationName 
                            && onePokmeonLocation.PokemonName ==
                            _oldPokemonLocation.PokemonName
                            && onePokmeonLocation.GameName == 
                            _oldPokemonLocation.GameName
                            && onePokmeonLocation.LevelFound == 
                            _oldPokemonLocation.LevelFound) 
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        MessageBox.Show("Pokemon Location Found and Ready to be Updated");
                        btnUpdatePokemonLocation.Content = "Update Pokemon Location";
                    }
                    else
                    {
                        MessageBox.Show("That Pokemon Location was not Found. " +
                            "Please try again");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else 
            {
                try
                {
                    List<string> allPokemonNames = getPokemonNames();
                    List<string> allLocationNames = getLocationNames();

                    if (!txtUpdateLocation.Text.isValidLocationName()
                    || !allLocationNames.Contains(txtUpdateLocation.Text))
                    {
                        MessageBox.Show("Location " + txtUpdateLocation.Text
                            + " Does Not Exist");
                        txtUpdateLocation.Focus();
                        txtUpdateLocation.SelectAll();
                        return;
                    }
                    if (!txtUpdatePokemon.Text.isValidPokemonName()
                        || !allPokemonNames.Contains(txtUpdatePokemon.Text))
                    {
                        MessageBox.Show("Pokemon " + txtUpdatePokemon.Text + " not found.");
                        txtUpdatePokemon.Focus();
                        txtUpdatePokemon.SelectAll();
                        return;
                    }
                    if (!txtUpdateGame.Text.isValidGameName())
                    {
                        MessageBox.Show("Invalid Game Name.");
                        txtUpdateGame.Focus();
                        txtUpdateGame.SelectAll();
                        return;
                    }
                    if (!txtUpdateLevel.Text.isValidLevelFound())
                    {
                        MessageBox.Show("Invalid Level Found.");
                        txtUpdateLevel.Focus();
                        txtUpdateLevel.SelectAll();
                        return;
                    }
                    if (txtUpdateFound.Text.Trim() == "")
                    {
                        MessageBox.Show("How Found Can Not Be Blank");
                        txtUpdateFound.Focus();
                        txtUpdateFound.SelectAll();
                        return;
                    }
                    if (txtUpdateEncounter.Text.Trim() == "")
                    {
                        MessageBox.Show("Encounter Rate Can Not Be Blank");
                        txtUpdateEncounter.Focus();
                        txtUpdateEncounter.SelectAll();
                        return;
                    }

                    PokemonLocation newPokemonLocation = new PokemonLocation()
                    {
                        LocationName = txtUpdateLocation.Text.Trim(),
                        PokemonName = txtUpdatePokemon.Text.Trim(),
                        GameName = txtUpdateGame.Text.Trim(),
                        LevelFound = txtUpdateLevel.Text.Trim(),
                        HowFound = txtUpdateFound.Text.Trim(),
                        SpeciesEncounterRate = txtUpdateEncounter.Text.Trim(),
                    };

                    _pokemonLocationManager.EditPokemonLocation(
                            _oldPokemonLocation, newPokemonLocation);

                    txtUpdateLocation.Text = "";
                    txtUpdatePokemon.Text = "";
                    txtUpdateGame.Text = "";
                    txtUpdateLevel.Text = "";
                    txtUpdateFound.Text = "";
                    txtUpdateEncounter.Text = "";

                    btnUpdatePokemonLocation.Content = "Find Pokemon Location";

                    MessageBox.Show("Pokemon Location was Updated");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnCanclePokemonLocation_Click(object sender, RoutedEventArgs e)
        {
            txtUpdateLocation.Text = "";
            txtUpdatePokemon.Text = "";
            txtUpdateGame.Text = "";
            txtUpdateLevel.Text = "";
            txtUpdateFound.Text = "";
            txtUpdateEncounter.Text = "";

            btnUpdatePokemonLocation.Content = "Find Pokemon Location";
        }

        private void btnUpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnUpdateLocation.Content == "Find Location")
            {
                try
                {
                    List<string> allLocationNames = getLocationNames();

                    if (!txtUpdateLocationName.Text.isValidLocationName()
                    || !allLocationNames.Contains(txtUpdateLocationName.Text))
                    {
                        MessageBox.Show("Location " + txtUpdateLocationName.Text
                            + " Does Not Exist");
                        txtUpdateLocationName.Focus();
                        txtUpdateLocationName.SelectAll();
                        return;
                    }

                    List<Location> locations = _pokemonLocationManager.RetrieveAllLocation();
                    int space = allLocationNames.IndexOf(txtUpdateLocationName.Text);
                    Location location = locations[space];

                    txtUpdateDescription.Text = location.Description;

                    txtUpdateLocationName.BorderBrush = Brushes.White;
                    txtUpdateDescription.BorderBrush = Brushes.Black;

                    txtUpdateLocationName.IsReadOnly = true;
                    txtUpdateDescription.IsReadOnly = false;

                    btnUpdateLocation.Content = "Update Location";
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else 
            {
                try
                {
                    List<string> allLocationNames = getLocationNames();

                    if (txtUpdateDescription.Text.Trim() == "")
                    {
                        MessageBox.Show("Location Description Can Not Be Blank.");
                        txtUpdateDescription.Focus();
                        txtUpdateDescription.SelectAll();
                        return;
                    }

                    List<Location> locations = _pokemonLocationManager.RetrieveAllLocation();
                    int space = allLocationNames.IndexOf(txtUpdateLocationName.Text);
                    Location oldLocation = locations[space];

                    Location newLocation = new Location()
                    {
                        LocationName = txtUpdateLocationName.Text.Trim(),
                        Description = txtUpdateDescription.Text.Trim()
                    };

                    _pokemonLocationManager.EditLocation(oldLocation, newLocation);

                    txtUpdateLocationName.BorderBrush = Brushes.Black;
                    txtUpdateDescription.BorderBrush = Brushes.White;

                    txtUpdateLocationName.IsReadOnly = false;
                    txtUpdateDescription.IsReadOnly = true;

                    txtUpdateLocationName.Text = "";
                    txtUpdateDescription.Text = "";

                    MessageBox.Show("Location was updated.");
                    btnUpdateLocation.Content = "Find Location";

                    _locationUpdated = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnCancleLocation_Click(object sender, RoutedEventArgs e)
        {
            txtUpdateLocationName.BorderBrush = Brushes.Black;
            txtUpdateDescription.BorderBrush = Brushes.White;

            txtUpdateLocationName.IsReadOnly = false;
            txtUpdateDescription.IsReadOnly = true;

            txtUpdateLocationName.Text = "";
            txtUpdateDescription.Text = "";
            btnUpdateLocation.Content = "Find Location";

        }

        private void tabUserEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    var memberManager = new MemberManager();
                    if (dgUserList.ItemsSource == null || _memberUpdated == true)
                    {
                        dgUserList.ItemsSource =
                            memberManager.RetrieveMemberByActive();

                        dgUserList.Columns[0].Header = "Member ID";
                        dgUserList.Columns[1].Header = "Email";
                        dgUserList.Columns[2].Header = "First Name";
                        dgUserList.Columns[3].Header = "Last Name";
                        dgUserList.Columns[4].Header = "Active";
                        dgUserList.Columns[5].Header = "Role";

                        _memberUpdated = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void dgUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Member)dgUserList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select an employee to edit!", 
                    "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            editMember();
        }

        private void editMember()
        {
            var selectedItem = (Member)dgUserList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            var editRoleActiveWindow = new MemberRoleActive(selectedItem);
            if (editRoleActiveWindow.ShowDialog() == true)
            {
                var memberManager = new MemberManager();
                dgUserList.ItemsSource =
                    memberManager.RetrieveMemberByActive((bool)chkShowActive.IsChecked);

                dgUserList.Columns[0].Header = "Member ID";
                dgUserList.Columns[1].Header = "Email";
                dgUserList.Columns[2].Header = "First Name";
                dgUserList.Columns[3].Header = "Last Name";
                dgUserList.Columns[4].Header = "Active";
                dgUserList.Columns[5].Header = "Role";
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Member)dgUserList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select a member to edit!", 
                    "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            editMember();
        }

        private void chkShowActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var memberManager = new MemberManager();
                dgUserList.ItemsSource =
                    memberManager.RetrieveMemberByActive((bool)chkShowActive.IsChecked);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            resetWindow();
        }

        private void btnDeletePokemon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> allPokemonNames = getPokemonNames();
                if (!txtDeletePokemonName.Text.isValidPokemonName()
                        || !allPokemonNames.Contains(txtDeletePokemonName.Text.Trim()))
                {
                    MessageBox.Show("Pokemon " + txtDeletePokemonName.Text +
                        " Dose Not Exist.");
                    txtDeletePokemonName.Focus();
                    txtDeletePokemonName.SelectAll();
                    return;
                }

                _pokemonManager.RemovePokemon(txtDeletePokemonName.Text.Trim());

                MessageBox.Show("Pokemon "+ txtDeletePokemonName.Text + " was deleted");

                txtDeletePokemonName.Text = "";

                _pokemonUpdated = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnDeleteEvolution_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> allPokemonNames = getPokemonNames();
                if (!txtDeleteReactant.Text.isValidPokemonName()
                    || !allPokemonNames.Contains(txtDeleteReactant.Text))
                {
                    MessageBox.Show("Pokemon " + txtDeleteReactant.Text + " Dose Not Exist.");
                    txtDeleteReactant.Focus();
                    txtDeleteReactant.SelectAll();
                    return;
                }
                if (!txtDeleteEvolvesInto.Text.isValidPokemonName()
                    || !allPokemonNames.Contains(txtDeleteEvolvesInto.Text))
                {
                    MessageBox.Show("Pokemon " + txtDeleteEvolvesInto.Text + " Dose Not Exist.");
                    txtDeleteEvolvesInto.Focus();
                    txtDeleteEvolvesInto.SelectAll();
                    return;
                }
                if (txtDeleteEvolutionCondition.Text.Trim() == "")
                {
                    MessageBox.Show("The Evolution Condition can not be empty.");
                    txtDeleteEvolutionCondition.Focus();
                    txtDeleteEvolutionCondition.SelectAll();
                    return;
                }

                List<Evolution> manyEvolutions =
                        _pokemonManager.RetrieveEvolutionByEvolvesInto(txtDeleteEvolvesInto.Text.Trim()); ;
                bool found = false;
                foreach (Evolution oneEvolution in manyEvolutions)
                {
                    if (oneEvolution.EvolutionCondition ==
                        txtDeleteEvolutionCondition.Text.Trim()
                        && oneEvolution.EvolvesInto ==
                        txtDeleteEvolvesInto.Text.Trim()
                        && oneEvolution.Reactant ==
                        txtDeleteReactant.Text.Trim())
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    _pokemonManager.RemoveEvolution(txtDeleteReactant.Text.Trim()
                        , txtDeleteEvolutionCondition.Text.Trim()
                        , txtDeleteEvolvesInto.Text.Trim());

                    MessageBox.Show("Evolution bewteen "+ txtDeleteReactant.Text 
                        + " and "+ txtDeleteEvolvesInto.Text + " was deleated");

                    txtDeleteReactant.Text = "";
                    txtDeleteEvolvesInto.Text = "";
                    txtDeleteEvolutionCondition.Text = "";
                }
                else 
                {
                    MessageBox.Show("Evolution not found");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnDeletePokemonLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> allLocationNames = getLocationNames();
                List<string> allPokemonNames = getPokemonNames();
                if (!txtDeleteLocation.Text.isValidLocationName()
                    || !allLocationNames.Contains(txtDeleteLocation.Text))
                {
                    MessageBox.Show("Location " + txtDeleteLocation.Text
                        + " Does Not Exist");
                    txtDeleteLocation.Focus();
                    txtDeleteLocation.SelectAll();
                    return;
                }
                if (!txtDeletePokemon.Text.isValidPokemonName()
                    || !allPokemonNames.Contains(txtDeletePokemon.Text))
                {
                    MessageBox.Show("Pokemon " + txtDeletePokemon.Text + " not found.");
                    txtDeletePokemon.Focus();
                    txtDeletePokemon.SelectAll();
                    return;
                }
                if (!txtDeleteGame.Text.isValidGameName())
                {
                    MessageBox.Show("Invalid Game Name.");
                    txtDeleteGame.Focus();
                    txtDeleteGame.SelectAll();
                    return;
                }
                if (!txtDeleteLevel.Text.isValidLevelFound())
                {
                    MessageBox.Show("Invalid Level Found.");
                    txtDeleteLevel.Focus();
                    txtDeleteLevel.SelectAll();
                    return;
                }

                List<PokemonLocation> pokemonLocations =
                        _pokemonLocationManager.RetrievePokemonLocationByLocationName(
                            txtDeleteLocation.Text);
                bool found = false;
                foreach (PokemonLocation onePokmeonLocation in pokemonLocations)
                {
                    if (onePokmeonLocation.LocationName ==
                        txtDeleteLocation.Text.Trim()
                        && onePokmeonLocation.PokemonName ==
                        txtDeletePokemon.Text.Trim()
                        && onePokmeonLocation.GameName ==
                        txtDeleteGame.Text.Trim()
                        && onePokmeonLocation.LevelFound ==
                        txtDeleteLevel.Text.Trim())
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    _pokemonLocationManager.RemovePokemonLocation(
                        txtDeleteLocation.Text.Trim(), txtDeletePokemon.Text.Trim(),
                        txtDeleteLevel.Text.Trim(), txtDeleteGame.Text.Trim());

                    MessageBox.Show("Pokemon Location was deleted.");

                    txtDeleteLocation.Text = "";
                    txtDeletePokemon.Text = "";
                    txtDeleteGame.Text = "";
                    txtDeleteLevel.Text = "";
                }
                else
                {
                    MessageBox.Show("Pokemon Location was not Found.");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> allLocationNames = getLocationNames();
                if (!txtDeleteLocationName.Text.isValidLocationName()
                    || !allLocationNames.Contains(txtDeleteLocationName.Text))
                {
                    MessageBox.Show("Location " + txtDeleteLocationName.Text
                        + " Does Not Exist");
                    txtDeleteLocationName.Focus();
                    txtDeleteLocationName.SelectAll();
                    return;
                }

                _pokemonLocationManager.RemoveLocationByName(txtDeleteLocationName.Text.Trim());

                MessageBox.Show("Location "+ txtDeleteLocationName.Text + " was deleted");

                txtDeleteLocationName.Text = "";

                _locationUpdated = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnLocationSearch_Click(object sender, RoutedEventArgs e)
        {
            List<string> allLocationNames = getLocationNames();
            try
            {
                if (txtLocationName.Text.isValidLocationName() && allLocationNames.Contains(txtLocationName.Text))
                {
                    List<Location> locations = _pokemonLocationManager.RetrieveAllLocation();
                    int space = allLocationNames.IndexOf(txtLocationName.Text.Trim());
                    Location location = locations[space];

                    var locationWindow = new LocationWindow(location);
                    locationWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Location " + txtLocationName.Text + " does not exist");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}


/*
 * MessageBoxResult result = MessageBox.Show(message, "Update Status",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _cabinManager.EditCabinStatus(cabin.CabinID, oldStatus, newStatus);
                    _cabinsUpdated = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
 */