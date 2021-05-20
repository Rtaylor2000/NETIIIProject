/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class file is used to connect crud the functions related to the 
/// pokemon object to the project.
/// </summary>
/// 
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
    public class PokemonAccessor : IPokemonAccessor
    {

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to delete an evolution object.
        /// </summary>
        /// <param name="reactant">the name of the pokemon be evolved</param>
        /// <param name="evolutionCondition">how the pokemon evolves</param>
        /// <param name="evolvesInto">the name of the pokemon it evolves into</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Deletion Failed. Evolution data was not deleted.)</exception>
        /// <returns>true or false if the removel worked</returns>
        public int DeleteEvolution(string reactant, string evolutionCondition, string evolvesInto)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_delete_one_evolution", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@reactant", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@evolution_condition", SqlDbType.Text);
            cmd.Parameters.Add("@evolves_into", SqlDbType.NVarChar, 50);

            cmd.Parameters["@reactant"].Value = reactant;
            cmd.Parameters["@evolution_condition"].Value = evolutionCondition;
            cmd.Parameters["@evolves_into"].Value = evolvesInto;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1) 
                {
                    throw new ApplicationException("The evolution could not be removed.");
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

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to delete a pokemon object.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon to be removed</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Deletion Failed. Pokemon data was not deleted.)</exception>
        /// <returns>true or false if the removel worked</returns>
        public int DeletePokemon(string pokemonName)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_delete_pokemon_by_name", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters["@pokemon_name"].Value = pokemonName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException(pokemonName+" could not be removed.");
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

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to create an evolution object.
        /// </summary>
        /// <param name="evolution">the new evolution object to be added into the </param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Add evolution Failed New evolution was not added)</exception>
        /// <returns>true or false if the addition worked</returns>
        public int InsertNewEvolution(Evolution evolution)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_insert_new_evolution", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@reactant", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@evolution_condition", SqlDbType.Text);
            cmd.Parameters.Add("@evolves_into", SqlDbType.NVarChar, 50);

            cmd.Parameters["@reactant"].Value = evolution.Reactant;
            cmd.Parameters["@evolution_condition"].Value = evolution.EvolutionCondition;
            cmd.Parameters["@evolves_into"].Value = evolution.EvolvesInto;

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

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to create a pokemon object.
        /// </summary>
        /// <param name="pokemon">the new pokemon object to be added into the </param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Add pokemon Failed new pokemon was not added.)</exception>
        /// <returns>true or false if the addition worked</returns>
        public int InsertNewPokemon(Pokemon pokemon)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_insert_new_pokemon", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pokedex_number", SqlDbType.Int);
            cmd.Parameters.Add("@pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@type_one", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@type_two", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@catch_rate", SqlDbType.Int);
            cmd.Parameters.Add("@base_HP", SqlDbType.Int);
            cmd.Parameters.Add("@base_attack", SqlDbType.Int);
            cmd.Parameters.Add("@base_defense", SqlDbType.Int);
            cmd.Parameters.Add("@base_special_attack", SqlDbType.Int);
            cmd.Parameters.Add("@base_special_defense", SqlDbType.Int);
            cmd.Parameters.Add("@base_speed", SqlDbType.Int);
            cmd.Parameters.Add("@pokemon_description", SqlDbType.Text);

            cmd.Parameters["@pokedex_number"].Value = pokemon.PokedexNumber;
            cmd.Parameters["@pokemon_name"].Value = pokemon.PokemonName;
            cmd.Parameters["@type_one"].Value = pokemon.TypeOne;
            cmd.Parameters["@type_two"].Value = pokemon.TypeTwo;
            cmd.Parameters["@catch_rate"].Value = pokemon.CatchRate;
            cmd.Parameters["@base_HP"].Value = pokemon.BaseHP;
            cmd.Parameters["@base_attack"].Value = pokemon.BaseAttack;
            cmd.Parameters["@base_defense"].Value = pokemon.BaseDefense;
            cmd.Parameters["@base_special_attack"].Value = pokemon.BaseSpecialAttack;
            cmd.Parameters["@base_special_defense"].Value = pokemon.BaseSpecialDefense;
            cmd.Parameters["@base_speed"].Value = pokemon.BaseSpeed;
            cmd.Parameters["@pokemon_description"].Value = pokemon.PokemonDescription;

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

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to select all the pokemon objects in the database.
        /// </summary>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon list not available.)</exception>
        /// <returns>a list of pokemon</returns>
        public List<Pokemon> SelectAllPokemon()
        {
            List<Pokemon> manyPokemon = new List<Pokemon>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_all_pokemon", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read()) 
                    {
                        var pokemon = new Pokemon()
                        {
                            PokedexNumber = reader.GetInt32(0),
                            PokemonName = reader.GetString(1),
                            TypeOne = reader.GetString(2),
                            TypeTwo = reader.GetString(3),
                            CatchRate = reader.GetInt32(4),
                            BaseHP = reader.GetInt32(5),
                            BaseAttack = reader.GetInt32(6),
                            BaseDefense = reader.GetInt32(7),
                            BaseSpecialAttack = reader.GetInt32(8),
                            BaseSpecialDefense = reader.GetInt32(9),
                            BaseSpeed = reader.GetInt32(10),
                            PokemonDescription = reader.GetString(11)
                        };
                        manyPokemon.Add(pokemon);
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

            return manyPokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to select all the evolution objects in the database 
        /// based on the name of the pokemon being evolved into.
        /// </summary>
        /// <param name="evolvesInto">the name of the pokemon being evolved into</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Evolutions list not available.)</exception>
        /// <returns>a list of evolutions</returns>
        public List<Evolution> SelectEvolutionByEvolvesInto(string evolvesInto)
        {
            List<Evolution> evolutions = new List<Evolution>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_evolution_by_evolves_into", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@evolves_into", SqlDbType.NVarChar, 50);
            cmd.Parameters["@evolves_into"].Value = evolvesInto;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var evolution = new Evolution()
                        {
                            Reactant = reader.GetString(0),
                            EvolutionCondition = reader.GetString(1),
                            EvolvesInto = reader.GetString(2),
                        };
                        evolutions.Add(evolution);
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

            return evolutions;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to select all the evolution objects in the database 
        /// based on the name of the pokemon being evolved.
        /// </summary>
        /// <param name="reactant">the name of the pokemon being evolved</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Evolutions list not available.)</exception>
        /// <returns>a list of evolutions</returns>
        public List<Evolution> SelectEvolutionByReactant(string reactant)
        {
            List<Evolution> evolutions = new List<Evolution>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_evolution_by_reactant", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@reactant", SqlDbType.NVarChar, 50);
            cmd.Parameters["@reactant"].Value = reactant;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var evolution = new Evolution()
                        {
                            Reactant = reader.GetString(0),
                            EvolutionCondition = reader.GetString(1),
                            EvolvesInto = reader.GetString(2),
                        };
                        evolutions.Add(evolution);
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

            return evolutions;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive a pokemon objects by its name.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon not found.)</exception>
        /// <returns>a pokemon object</returns>
        public Pokemon SelectPokemonByName(string pokemonName)
        {
            Pokemon pokemon = null;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_pokemon_by_name", conn);
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
                        pokemon = new Pokemon()
                        {
                            PokedexNumber = reader.GetInt32(0),
                            PokemonName = reader.GetString(1),
                            TypeOne = reader.GetString(2),
                            TypeTwo = reader.GetString(3),
                            CatchRate = reader.GetInt32(4),
                            BaseHP = reader.GetInt32(5),
                            BaseAttack = reader.GetInt32(6),
                            BaseDefense = reader.GetInt32(7),
                            BaseSpecialAttack = reader.GetInt32(8),
                            BaseSpecialDefense = reader.GetInt32(9),
                            BaseSpeed = reader.GetInt32(10),
                            PokemonDescription = reader.GetString(11)
                        };
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

            return pokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to select a pokemon objects by its type.
        /// </summary>
        /// <param name="type">the elemental type of a pokemon</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon list not available.)</exception>
        /// <returns>a list of pokemon</returns>
        public List<Pokemon> SelectPokemonByType(string type)
        {
            List<Pokemon> manyPokemon = new List<Pokemon>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_pokemon_by_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pokemon_type", SqlDbType.NVarChar, 10);
            cmd.Parameters["@pokemon_type"].Value = type;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var pokemon = new Pokemon()
                        {
                            PokedexNumber = reader.GetInt32(0),
                            PokemonName = reader.GetString(1),
                            TypeOne = reader.GetString(2),
                            TypeTwo = reader.GetString(3),
                            CatchRate = reader.GetInt32(4),
                            BaseHP = reader.GetInt32(5),
                            BaseAttack = reader.GetInt32(6),
                            BaseDefense = reader.GetInt32(7),
                            BaseSpecialAttack = reader.GetInt32(8),
                            BaseSpecialDefense = reader.GetInt32(9),
                            BaseSpeed = reader.GetInt32(10),
                            PokemonDescription = reader.GetString(11)
                        };
                        manyPokemon.Add(pokemon);
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

            return manyPokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to select a pokemon objects by its pokedex number.
        /// </summary>
        /// <param name="pokedexNumber">the pokedex number of a pokemon</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon not found.)</exception>
        /// <returns>a pokemon object</returns>
        public Pokemon SelectPokmeonByNumber(int pokedexNumber)
        {
            Pokemon pokemon = null;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_pokemon_by_pokedex_number", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pokedex_number", SqlDbType.Int);
            cmd.Parameters["@pokedex_number"].Value = pokedexNumber;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pokemon = new Pokemon()
                        {
                            PokedexNumber = reader.GetInt32(0),
                            PokemonName = reader.GetString(1),
                            TypeOne = reader.GetString(2),
                            TypeTwo = reader.GetString(3),
                            CatchRate = reader.GetInt32(4),
                            BaseHP = reader.GetInt32(5),
                            BaseAttack = reader.GetInt32(6),
                            BaseDefense = reader.GetInt32(7),
                            BaseSpecialAttack = reader.GetInt32(8),
                            BaseSpecialDefense = reader.GetInt32(9),
                            BaseSpeed = reader.GetInt32(10),
                            PokemonDescription = reader.GetString(11)
                        };
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

            return pokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to update an evolution object.
        /// </summary>
        /// <param name="oldEvolution">the original evolution object</param>
        /// <param name="newEvolution">the new evolution object to relpace the old one</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Update Failed. Evolution data not changed.)</exception>
        /// <returns>true or false if the edit worked</returns>
        public int UpdateEvolution(Evolution oldEvolution, Evolution newEvolution)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_update_evolution", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Old_reactant", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_evolution_condition", SqlDbType.Text);
            cmd.Parameters.Add("@Old_evolves_into", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@New_reactant", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@New_evolution_condition", SqlDbType.Text);
            cmd.Parameters.Add("@New_evolves_into", SqlDbType.NVarChar, 50);

            cmd.Parameters["@Old_reactant"].Value = oldEvolution.Reactant;
            cmd.Parameters["@Old_evolution_condition"].Value = oldEvolution.EvolutionCondition;
            cmd.Parameters["@Old_evolves_into"].Value = oldEvolution.EvolvesInto;
            cmd.Parameters["@New_reactant"].Value = newEvolution.Reactant;
            cmd.Parameters["@New_evolution_condition"].Value = newEvolution.EvolutionCondition;
            cmd.Parameters["@New_evolves_into"].Value = newEvolution.EvolvesInto;

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

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to update a pokemon object.
        /// </summary>
        /// <param name="oldPokemon">the original pokemon object</param>
        /// <param name="newPokemon">the new pokemon object to relpace the old one</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Update Failed. Pokemon data not changed.)</exception>
        /// <returns>true or false if the edit worked</returns>
        public int UpdatePokemon(Pokemon oldPokemon, Pokemon newPokemon)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_update_pokemon", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pokedex_number", SqlDbType.Int);
            cmd.Parameters.Add("@pokemon_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_type_one", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Old_type_two", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Old_catch_rate", SqlDbType.Int);
            cmd.Parameters.Add("@Old_base_HP", SqlDbType.Int);
            cmd.Parameters.Add("@Old_base_attack", SqlDbType.Int);
            cmd.Parameters.Add("@Old_base_defense", SqlDbType.Int);
            cmd.Parameters.Add("@Old_base_special_attack", SqlDbType.Int);
            cmd.Parameters.Add("@Old_base_special_defense", SqlDbType.Int);
            cmd.Parameters.Add("@Old_base_speed", SqlDbType.Int);
            cmd.Parameters.Add("@Old_pokemon_description", SqlDbType.Text);
            cmd.Parameters.Add("@New_type_one", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@New_type_two", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@New_catch_rate", SqlDbType.Int);
            cmd.Parameters.Add("@New_base_HP", SqlDbType.Int);
            cmd.Parameters.Add("@New_base_attack", SqlDbType.Int);
            cmd.Parameters.Add("@New_base_defense", SqlDbType.Int);
            cmd.Parameters.Add("@New_base_special_attack", SqlDbType.Int);
            cmd.Parameters.Add("@New_base_special_defense", SqlDbType.Int);
            cmd.Parameters.Add("@New_base_speed", SqlDbType.Int);
            cmd.Parameters.Add("@New_pokemon_description", SqlDbType.Text);

            cmd.Parameters["@pokedex_number"].Value = oldPokemon.PokedexNumber;
            cmd.Parameters["@pokemon_name"].Value = oldPokemon.PokemonName;
            cmd.Parameters["@Old_type_one"].Value = oldPokemon.TypeOne;
            cmd.Parameters["@Old_type_two"].Value = oldPokemon.TypeTwo;
            cmd.Parameters["@Old_catch_rate"].Value = oldPokemon.CatchRate;
            cmd.Parameters["@Old_base_HP"].Value = oldPokemon.BaseHP;
            cmd.Parameters["@Old_base_attack"].Value = oldPokemon.BaseAttack;
            cmd.Parameters["@Old_base_defense"].Value = oldPokemon.BaseDefense;
            cmd.Parameters["@Old_base_special_attack"].Value = oldPokemon.BaseSpecialAttack;
            cmd.Parameters["@Old_base_special_defense"].Value = oldPokemon.BaseSpecialDefense;
            cmd.Parameters["@Old_base_speed"].Value = oldPokemon.BaseSpeed;
            cmd.Parameters["@Old_pokemon_description"].Value = oldPokemon.PokemonDescription;

            cmd.Parameters["@New_type_one"].Value = newPokemon.TypeOne;
            cmd.Parameters["@New_type_two"].Value = newPokemon.TypeTwo;
            cmd.Parameters["@New_catch_rate"].Value = newPokemon.CatchRate;
            cmd.Parameters["@New_base_HP"].Value = newPokemon.BaseHP;
            cmd.Parameters["@New_base_attack"].Value = newPokemon.BaseAttack;
            cmd.Parameters["@New_base_defense"].Value = newPokemon.BaseDefense;
            cmd.Parameters["@New_base_special_attack"].Value = newPokemon.BaseSpecialAttack;
            cmd.Parameters["@New_base_special_defense"].Value = newPokemon.BaseSpecialDefense;
            cmd.Parameters["@New_base_speed"].Value = newPokemon.BaseSpeed;
            cmd.Parameters["@New_pokemon_description"].Value = newPokemon.PokemonDescription;

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
