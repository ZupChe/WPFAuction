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
    public class ItemCollection : ObservableCollection<User>
    {
        public static UserCollection GetAllItem()
        {
            ItemCollection items = new ItemCollection();
            Items item = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Id, Item, Detail, Price, FROM [Auctions]", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        item = Items.GetItemFromResultSet(reader);
                        items.Add(item);
                    }
                }

            }
            return item;
        }

        
    }
}
