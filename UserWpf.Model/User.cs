using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserWpf.Model
{
    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _userName;
        private string _userPass;
        private string _displayName;
        private bool _isAdmin;
       

        public event PropertyChangedEventHandler PropertyChanged;
       

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Id"));
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                {
                    return;
                }
                _userName = value;
             
                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }

        public string UserPass
        {
            get { return _userPass; }
            set
            {
                if (_userPass == value)
                {
                    return;
                }
                _userPass = value;
                
                OnPropertyChanged(new PropertyChangedEventArgs("UserPass"));
            }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (_displayName == value)
                {
                    return;
                }
                _displayName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin == value)
                {
                    return;
                }
                _isAdmin = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsAdmin"));
            }
        }

      
        public User(string UserName, string UserPass, string DisplayName, bool IsAdmin)
        {
            this.UserName = UserName;
            this.UserPass = UserPass;
            this.DisplayName = DisplayName;
            this.IsAdmin = IsAdmin;
        }


        public User(int Id, string UserName, string UserPass, string DisplayName, bool IsAdmin)
        {
            this.UserName = UserName;
            this.UserPass = UserPass;
            this.DisplayName = DisplayName;
            this.IsAdmin = IsAdmin;
            this.Id = Id;
        }

        public User()
        {
            UserName = "";
            UserPass = "";
            DisplayName = "";
            
        }

        public static User GetUserFromResultSet(SqlDataReader reader)
        {
            User user = new User((int)reader["Id"], (string)reader["UserName"], (string)reader["UserPass"], (string)reader["DisplayName"], (bool)reader["IsAdmin"]);
            return user;
        }

        public void DeletePerson()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE [User] WHERE id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }

        public void UpdatePerson()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE [User] SET UserName=@UserName, UserPass=@UserPass, DisplayName=@DisplayName, IsAdmin=@IsAdmin WHERE Id=@Id", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter userDisplayParam = new SqlParameter("@DisplayName", SqlDbType.NVarChar);
                userDisplayParam.Value = this.DisplayName;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.Bit);
                isAdminParam.Value = this.IsAdmin;

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(userDisplayParam);
                command.Parameters.Add(isAdminParam);
                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }

        public void Insert()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO [User](UserName, UserPass, DisplayName, IsAdmin) VALUES(@UserName, @UserPass, @DisplayName, @IsAdmin); SELECT IDENT_CURRENT('User');", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter userDisplayParam = new SqlParameter("@DisplayName", SqlDbType.NVarChar);
                userDisplayParam.Value = this.DisplayName;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.Bit);
                isAdminParam.Value = this.IsAdmin;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(userDisplayParam);
                command.Parameters.Add(isAdminParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.Id = Convert.ToInt32(id);
                }

            }
        }

        public void Save()
        {
            if(Id == 0)
            {
                Insert();
            }

            else
            {
                UpdatePerson();
            }
        }



   

    
}
}

