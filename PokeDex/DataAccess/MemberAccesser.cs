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
    public class MemberAccesser : IMemberAccesser
    {
        public int DeactivateMember(int memberID)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_safely_deactivate_member", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", memberID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("Employee could not be deactivated.");
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

        public int InsertNewMember(Member member)
        {
            int memberID = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_insert_new_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@first_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@last_name", SqlDbType.NVarChar, 50);

            cmd.Parameters["@email"].Value = member.Email;
            cmd.Parameters["@first_name"].Value = member.FirstName;
            cmd.Parameters["@last_name"].Value = member.LastName;

            try
            {
                conn.Open();
                memberID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                conn.Close();
            }
            return memberID;
        }

        public int ReactivateMember(int memberID)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_reactivate_member", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", memberID);

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

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();
            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_all_roles", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
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
            return roles;
        }

        public List<Member> SelectMemberByActive(bool active = true)
        {
            List<Member> members = new List<Member>();

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_select_members_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        var member = new Member()
                        {
                            MemberID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            Role = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        members.Add(member);
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

            return members;
        }

        public int UpdateMemberProfile(Member oldMember, Member newMember)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_update_member_profile_data", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@member_id", SqlDbType.Int);
            cmd.Parameters.Add("@Newemail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Newfirst_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Newlast_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Oldemail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Oldfirst_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Oldlast_name", SqlDbType.NVarChar, 50);

            cmd.Parameters["@member_id"].Value = oldMember.MemberID;
            cmd.Parameters["@Newemail"].Value = newMember.Email;
            cmd.Parameters["@Newfirst_name"].Value = newMember.FirstName;
            cmd.Parameters["@Newlast_name"].Value = newMember.LastName;
            cmd.Parameters["@Oldemail"].Value = oldMember.Email;
            cmd.Parameters["@Oldfirst_name"].Value = oldMember.FirstName;
            cmd.Parameters["@Oldlast_name"].Value = oldMember.LastName;

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

        public int UpdateMemberRole(int memberID, string oldRole, string newRole)
        {
            int result = 0;

            var conn = DBConnection.GetSqlConnection();
            var cmd = new SqlCommand("sp_safely_change_member_role", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@member_id", SqlDbType.Int);
            cmd.Parameters.Add("@Oldrole", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Newrole", SqlDbType.NVarChar, 10);

            cmd.Parameters["@member_id"].Value = memberID;
            cmd.Parameters["@Oldrole"].Value = oldRole;
            cmd.Parameters["@Newrole"].Value = newRole;

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
