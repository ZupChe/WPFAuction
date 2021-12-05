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

                SqlCommand command = new SqlCommand("SELECT Id, Name, Detail, Price, LastBidUser, IsClosed FROM [Auctions]", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var item = new Item((int)reader["Id"], (string)reader["Name"], (string)reader["Detail"], (int)reader["Price"], (int)reader["LastBidUser"], (bool)reader["IsClosed"]);
                        items.Add(item);
                    }
                }

            }
            return items;
        }

        
    }
}
