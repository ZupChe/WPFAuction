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
    public class ItemCollection : ObservableCollection<Item>
    {
        public static ItemCollection GetAllItem()
        {
            ItemCollection items = new ItemCollection();
           

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT a.*, u.DisplayName FROM [Auctions] as a LEFT JOIN [User] as u on u.Id=a.LastBidUser", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        var item = new Item((int)reader["Id"], (string)reader["Name"], (string)reader["Detail"], (int)reader["Price"], reader["DisplayName"].ToString(), (bool)reader["IsClosed"]);
                        items.Add(item);
                    }
                }

            }
            return items;
        }

        
    }
}
