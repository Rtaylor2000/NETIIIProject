/// <summary>
/// Ryan Taylor
/// Created: 2021/04/19
/// 
/// this class file is used to process inofrmation realated to 
/// the current user that the database is able to find
/// </summary>
using DataAccess;
using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserManager : IUserManager
    {
        private IUserAccessor userAccessor;
        public UserManager() 
        {
            userAccessor = new UserAccessor();
        }
        public UserManager(IUserAccessor suppliedUserAccessor) 
        {
            userAccessor = suppliedUserAccessor;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// this method is used to see if the user trying to login is 
        /// regesterd within the database
        /// </summary>
        /// 
        /// <param name="email">the email of the user trying to log in</param>
        /// <param name="password">the password of the user trying to login</param>
        /// <exception cref="ApplicationException">bad username or 
        /// password, login failed</exception>
        /// <returns>the user object of the currently logged in user</returns>
        public User AuthenticateUser(string email, string password)
        {
            User user = null;

            password = hashSHA256(password);
            try
            {
                if (1 == userAccessor.VerifyUserNameAndPassword(email, password))
                {
                    user = userAccessor.SelectUserByEmail(email);
                }
                else
                {
                    throw new ApplicationException("Bad Username or Password");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Login Failed.", ex);
            }

            return user;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// this method is used to update the password of the user who is currently logged in
        /// </summary>
        /// 
        /// <param name="user">the user object of the person currently logged in</param>
        /// <param name="oldPassword">the original password</param>
        /// <param name="newPassword">the new password</param>
        /// <exception cref="ApplicationException">bad username or 
        /// password, Update failed</exception>
        /// <returns>the user object of the currently logged in user</returns>
        public bool UpdatePassword(User user, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = oldPassword.SHA256Value();
            newPassword = newPassword.SHA256Value();

            try
            {
                result = (1 == userAccessor.UpdatePasswordHash(
                    user.Email, newPassword, oldPassword));
                if (result == false)
                {
                    throw new ApplicationException("Update Failed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Bad username or password.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// this method is used to find a user by their email
        /// </summary>
        /// 
        /// <param name="email">the users email</param>
        /// <returns>true or false if the user was found or not</returns>
        public bool FindUserByEmail(string email)
        {
            bool result = false;

            try
            {
                userAccessor.SelectUserByEmail(email);
                result = true;
            }
            catch (Exception)
            {
                result = false; // user not found
            }

            return result;
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// this method is used update the password of the user who is currently logged in
        /// </summary>
        /// 
        /// <param name="source">the password sorce</param>
        /// <returns>unreadable password</returns>
        private string hashSHA256(string source)
        {
            string result = "";

            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();

            return result;
        }
    }
}
