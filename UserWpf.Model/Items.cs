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
    public class Items : INotifyPropertyChanged
    {
        private int _id;
        private string _item;
        private string _detail;
        private int _price;



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

        public string Item
        {
            get { return _item; }
            set
            {
                if (_item == value)
                {
                    return;
                }
                _item = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Item"));
            }
        }

        public string Detail
        {
            get { return _detail; }
            set
            {
                if (_detail == value)
                {
                    return;
                }
                _detail = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Detail"));
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (_price == value)
                {
                    return;
                }
                _price = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
            }
        }


        public Items(string Item, string Detail, int Price)
        {
            this.Item = Item;
            this.Detail = Detail;
            this.Price = Price;
            this.Id = Id;
        }


        public Items()
        {
            Item = "";
            Detail = "";
        }

        public static Items GetItemsFromResultSet(SqlDataReader reader)
        {
            Items item = new Items((int)reader["Id"], (string)reader["Item"], (string)reader["Details"], (int)reader["Price"]);
            return item;
        }

        public void DeleteItems()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE [Auctions] WHERE id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }

        public void UpdateItem()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE [Auctions] SET Item=@Item, Detail=@Detail, Price=@Price WHERE Id=@Id", conn);

                SqlParameter itemParam = new SqlParameter("@Item", SqlDbType.NVarChar);
                itemParam.Value = this.Item;

                SqlParameter detailParam = new SqlParameter("@Detail", SqlDbType.NVarChar);
                detailParam.Value = this.Detail;

                SqlParameter priceParam = new SqlParameter("@Price", SqlDbType.NVarChar);
                priceParam.Value = this.Price;

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(itemParam);
                command.Parameters.Add(detailParam);
                command.Parameters.Add(priceParam);
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

                SqlCommand command = new SqlCommand("INSERT INTO [Auctions](Item, Detail, Price) VALUES(@Item, @Detail, @Price); SELECT IDENT_CURRENT('Item');", conn);

                SqlParameter itemParam = new SqlParameter("@Item", SqlDbType.NVarChar);
                itemParam.Value = this.Item;

                SqlParameter detailParam = new SqlParameter("@Detail", SqlDbType.NVarChar);
                detailParam.Value = this.Detail;

                SqlParameter priceParam = new SqlParameter("@Price", SqlDbType.NVarChar);
                priceParam.Value = this.Price;


                command.Parameters.Add(itemParam);
                command.Parameters.Add(detailParam);
                command.Parameters.Add(priceParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.Id = Convert.ToInt32(id);
                }

            }
        }

        public void Save()
        {
            if (Id == 0)
            {
                Insert();
            }

            else
            {
                UpdateItem();
            }
        }

    }
}