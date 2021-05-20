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
    public class UserAccessor : IUserAccessor
    {
        public User SelectUserByEmail(string email)
        {
            User user = null;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_user_by_email", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters["@email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    var userID = reader.GetInt32(0);
                    var firstName = reader.GetString(2);
                    var lastName = reader.GetString(3);
                    var active = reader.GetBoolean(4);
                    var role = reader.GetString(5);
                    reader.Close();
                    user = new User(userID, firstName, lastName, email, role);
                }
                else 
                {
                    throw new ApplicationException("User not found");
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

            return user;
        }

        public int UpdatePasswordHash(string email, string newPasswordHash, string oldPasswordHash)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_update_passwordhash", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Oldpassword_hash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Newpassword_hash", SqlDbType.NVarChar, 100);
            cmd.Parameters["@email"].Value = email;
            cmd.Parameters["@Oldpassword_hash"].Value = oldPasswordHash;
            cmd.Parameters["@Newpassword_hash"].Value = newPasswordHash;

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

        public int VerifyUserNameAndPassword(string email, string passwordHash)
        {
            int result = 0;
            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_authenticate_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@password_hash", SqlDbType.NVarChar, 100);
            cmd.Parameters["@email"].Value = email;
            cmd.Parameters["@password_hash"].Value = passwordHash;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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
