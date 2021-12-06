using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserWpf.Model
{
    public class UserCollection : ObservableCollection<User>
    {
        public static UserCollection GetAllUsers()
        {
            UserCollection users = new UserCollection();
            User user = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Id, UserName, UserPass, DisplayName, IsAdmin FROM [User]", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = User.GetUserFromResultSet(reader);
                        users.Add(user);
                    }
                }

            }
            return users;
        }

        public static Boolean IsValidUser( string UserName, string UserPass)
        {
     
           
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Count(*) FROM [User] Where UserName = @u and UserPass = @p", conn);

                command.Parameters.AddWithValue("u", UserName.ToLower());
                command.Parameters.AddWithValue("p", UserPass);

                int c = int.Parse(command.ExecuteScalar().ToString());
                return c > 0;
            }
            
        }

        public static User GetUser(string UserName)
        {


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [User] Where UserName = @UserName", conn);

                command.Parameters.AddWithValue("@UserName", UserName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return User.GetUserFromResultSet(reader);
                }
            }

        }
    }
}
