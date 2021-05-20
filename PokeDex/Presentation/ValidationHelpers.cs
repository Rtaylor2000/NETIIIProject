using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public static class ValidationHelpers
    {
        public static bool IsValidEmail(this string email)
        {
            bool result = false;

            if (email.Contains("@") && email.Length >= 7 &&
                email.Length <= 100)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidPassword(this string password)
        {
            bool result = false;

            if (password.Length >= 7)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidFirstName(this string firstName)
        {
            bool result = false;

            if (firstName.Length >= 1 && firstName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidLastName(this string lastName)
        {
            bool result = false;

            if (lastName.Length >= 1 && lastName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidPokemonName(this string pokemonName) 
        {
            bool result = false;

            if (pokemonName.Length >= 1 && pokemonName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidTypeOne(this string type)
        {
            bool result = false;

            switch (type.ToLower())
            {
                case "bug":
                    result = true;
                    break;
                case "dragon":
                    result = true;
                    break;
                case "electric":
                    result = true;
                    break;
                case "fighting":
                    result = true;
                    break;
                case "fire":
                    result = true;
                    break;
                case "flying":
                    result = true;
                    break;
                case "ghost":
                    result = true;
                    break;
                case "grass":
                    result = true;
                    break;
                case "ground":
                    result = true;
                    break;
                case "ice":
                    result = true;
                    break;
                case "normal":
                    result = true;
                    break;
                case "poison":
                    result = true;
                    break;
                case "psychic":
                    result = true;
                    break;
                case "rock":
                    result = true;
                    break;
                case "water":
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public static bool isValidTypeTwo(this string type)
        {
            bool result = false;

            switch (type.ToLower())
            {
                case "bug":
                    result = true;
                    break;
                case "dragon":
                    result = true;
                    break;
                case "electric":
                    result = true;
                    break;
                case "fighting":
                    result = true;
                    break;
                case "fire":
                    result = true;
                    break;
                case "flying":
                    result = true;
                    break;
                case "ghost":
                    result = true;
                    break;
                case "grass":
                    result = true;
                    break;
                case "ground":
                    result = true;
                    break;
                case "ice":
                    result = true;
                    break;
                case "normal":
                    result = true;
                    break;
                case "poison":
                    result = true;
                    break;
                case "psychic":
                    result = true;
                    break;
                case "rock":
                    result = true;
                    break;
                case "water":
                    result = true;
                    break;
                case "none":
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public static bool isValidLocationName(this string locationName)
        {
            bool result = false;

            if (locationName.Length >= 1 && locationName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidGameName(this string gameName)
        {
            bool result = false;

            if (gameName.Length >= 1 && gameName.Length <= 6)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidLevelFound(this string levelFound)
        {
            bool result = false;

            if (levelFound.Length >= 1 && levelFound.Length <= 300)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidRole(this string role)
        {
            bool result = false;

            switch (role.ToLower())
            {
                case "admin":
                    result = true;
                    break;
                case "researcher":
                    result = true;
                    break;
                case "user":
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }
    }
}
