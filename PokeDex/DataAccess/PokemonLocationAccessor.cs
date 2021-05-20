using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PokemonLocationAccessor : IPokemonLocationAccessor
    {
        public int DeleteLocationByName(string locationName)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_delete_location_by_name", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters["@location_name"].Value = locationName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1) 
                {
                    throw new ApplicationException(locationName
                        +" location could not be removed.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                conn.Close();
            }

            return result;
        }

        public int DeletePokemonLocation(string locationName, string pokemonName
            , string levelFound, string gameName)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_delete_one_pokemon_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@level_found", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@game_name", SqlDbType.NVarChar, 6);

            cmd.Parameters["@location_name"].Value = locationName;
            cmd.Parameters["@pokemon_name"].Value = pokemonName;
            cmd.Parameters["@level_found"].Value = levelFound;
            cmd.Parameters["@game_name"].Value = gameName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("Pokemon Location could not be removed.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int InsertNewLocation(Location location)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_insert_new_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@description", SqlDbType.Text);

            cmd.Parameters["@location_name"].Value = location.LocationName;
            cmd.Parameters["@description"].Value = location.Description;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int InsertNewPokemonLocation(PokemonLocation pokemonLocation)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_insert_new_pokemon_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@game_name", SqlDbType.NVarChar, 6);
            cmd.Parameters.Add("@how_found", SqlDbType.Text);
            cmd.Parameters.Add("@level_found", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@species_encounter_rate", SqlDbType.Text);

            cmd.Parameters["@location_name"].Value = pokemonLocation.LocationName;
            cmd.Parameters["@pokemon_name"].Value = pokemonLocation.PokemonName;
            cmd.Parameters["@game_name"].Value = pokemonLocation.GameName;
            cmd.Parameters["@how_found"].Value = pokemonLocation.HowFound;
            cmd.Parameters["@level_found"].Value = pokemonLocation.LevelFound;
            cmd.Parameters["@species_encounter_rate"].Value = pokemonLocation.SpeciesEncounterRate;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<Location> SelectAllLocation()
        {
            List<Location> locations = new List<Location>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_all_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                        var location = new Location()
                        {
                            LocationName = reader.GetString(0),
                            Description = reader.GetString(1)
                        };
                        locations.Add(location);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return locations;
        }

        public List<PokemonLocation> SelectPokemonLocationByLocationName(string locationName)
        {
            List<PokemonLocation> pokemonLocations = new List<PokemonLocation>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_pokemon_location_by_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@location_name", SqlDbType.NVarChar, 50);

            cmd.Parameters["@location_name"].Value = locationName;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var pokemonLocation = new PokemonLocation()
                        {
                            LocationName = reader.GetString(0),
                            PokemonName = reader.GetString(1),
                            GameName = reader.GetString(2),
                            HowFound = reader.GetString(3),
                            LevelFound = reader.GetString(4),
                            SpeciesEncounterRate = reader.GetString(5)
                        };
                        pokemonLocations.Add(pokemonLocation);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return pokemonLocations;
        }

        public List<PokemonLocation> SelectPokemonLocationByPokemon(string pokemonName)
        {
            List<PokemonLocation> pokemonLocations = new List<PokemonLocation>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_pokemon_location_by_pokemon_name", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pokemon_name", SqlDbType.NVarChar, 50);

            cmd.Parameters["@pokemon_name"].Value = pokemonName;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var pokemonLocation = new PokemonLocation()
                        {
                            LocationName = reader.GetString(0),
                            PokemonName = reader.GetString(1),
                            GameName = reader.GetString(2),
                            HowFound = reader.GetString(3),
                            LevelFound = reader.GetString(4),
                            SpeciesEncounterRate = reader.GetString(5)
                        };
                        pokemonLocations.Add(pokemonLocation);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return pokemonLocations;
        }

        public int UpdateLocation(Location oldLocation, Location newLocation)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_update_location_by_name", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_description", SqlDbType.Text);
            cmd.Parameters.Add("@New_description", SqlDbType.Text);

            cmd.Parameters["@location_name"].Value = oldLocation.LocationName;
            cmd.Parameters["@Old_description"].Value = oldLocation.Description;
            cmd.Parameters["@New_description"].Value = newLocation.Description;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int UpdatePokemonLocation(PokemonLocation oldPokemonLocation
            , PokemonLocation newPokemonLocation)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_update_pokemon_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Old_location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_game_name", SqlDbType.NVarChar, 6);
            cmd.Parameters.Add("@Old_how_found", SqlDbType.Text);
            cmd.Parameters.Add("@Old_level_found", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@Old_species_encounter_rate", SqlDbType.Text);

            cmd.Parameters.Add("@New_location_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@New_pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@New_game_name", SqlDbType.NVarChar, 6);
            cmd.Parameters.Add("@New_how_found", SqlDbType.Text);
            cmd.Parameters.Add("@New_level_found", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@New_species_encounter_rate", SqlDbType.Text);


            cmd.Parameters["@Old_location_name"].Value = oldPokemonLocation.LocationName;
            cmd.Parameters["@Old_pokemon_name"].Value = oldPokemonLocation.PokemonName;
            cmd.Parameters["@Old_game_name"].Value = oldPokemonLocation.GameName;
            cmd.Parameters["@Old_how_found"].Value = oldPokemonLocation.HowFound;
            cmd.Parameters["@Old_level_found"].Value = oldPokemonLocation.LevelFound;
            cmd.Parameters["@Old_species_encounter_rate"].Value = oldPokemonLocation.SpeciesEncounterRate;

            cmd.Parameters["@New_location_name"].Value = newPokemonLocation.LocationName;
            cmd.Parameters["@New_pokemon_name"].Value = newPokemonLocation.PokemonName;
            cmd.Parameters["@New_game_name"].Value = newPokemonLocation.GameName;
            cmd.Parameters["@New_how_found"].Value = newPokemonLocation.HowFound;
            cmd.Parameters["@New_level_found"].Value = newPokemonLocation.LevelFound;
            cmd.Parameters["@New_species_encounter_rate"].Value = newPokemonLocation.SpeciesEncounterRate;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
