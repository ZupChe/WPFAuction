﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UserWpf.Model
{
    public class Item : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _detail;
        private int _price;
        private string _lastBidUser;
        private bool _isClosed;



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

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
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
                OnPropertyChanged(new PropertyChangedEventArgs("Price"));
            }
        }

        public string LastBidUser
        {
            get { return _lastBidUser; }
            set
            {
                if (_lastBidUser == value)
                {
                    return;
                }
                _lastBidUser = value;

                OnPropertyChanged(new PropertyChangedEventArgs("LastBidUser"));
            }
        }

        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                if (_isClosed == value)
                {
                    return;
                }
                _isClosed = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsClosed"));
            }
        }



        private DispatcherTimer _bidCouter;
        private TimeSpan _bidTime;
        private DateTime _bidStartDate;



        public TimeSpan BidTime
        {
            get { return _bidTime; }
            set
            {
                if (_bidTime == value) return;
                _bidTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BidTime"));
            }
        }



        public Item(int Id, string Name, string Detail, int Price, string LastBidUser, bool IsClosed)
        {
            this.Name = Name;
            this.Detail = Detail;
            this.Price = Price;
            this.LastBidUser = LastBidUser;
            this._isClosed = IsClosed;
            this.Id = Id;



            if (!IsClosed)
            {
                _bidCouter = new DispatcherTimer();
                _bidCouter.Interval = TimeSpan.FromMilliseconds(300);
                _bidCouter.Tick += (s, a) =>
                {



                };
            }
        }


        public Item()
        {
            Name = "";
            Detail = "";
        }

        public void DeleteItem()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE FROM [Auctions] WHERE Id=@Id", conn);

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

                SqlCommand command = new SqlCommand("UPDATE [Auctions] SET Detail=@Detail, Price=@Price WHERE Id=@Id", conn);

                SqlParameter detailParam = new SqlParameter("@Detail", SqlDbType.NVarChar);
                detailParam.Value = this.Detail;

                SqlParameter priceParam = new SqlParameter("@Price", SqlDbType.NVarChar);
                priceParam.Value = this.Price;

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

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

                SqlCommand command = new SqlCommand("INSERT INTO [Auctions](Name, Detail, Price, IsClosed) VALUES(@Name, @Detail, @Price, 0); SELECT IDENT_CURRENT('Auctions');", conn);

                SqlParameter nameParam = new SqlParameter("@Name", SqlDbType.NVarChar);
                nameParam.Value = this.Name;

                SqlParameter detailParam = new SqlParameter("@Detail", SqlDbType.NVarChar);
                detailParam.Value = this.Detail;

                SqlParameter priceParam = new SqlParameter("@Price", SqlDbType.NVarChar);
                priceParam.Value = this.Price;


                command.Parameters.Add(nameParam);
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

        public void UpdatePrice(int LoginUser)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE [Auctions] SET LastBidUser=@lbu, Price=(select Price from Auctions where Id=@Id)+1 WHERE Id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;


               
                command.Parameters.Add(myParam);

                command.Parameters.AddWithValue("@lbu", LoginUser);

                int rows = command.ExecuteNonQuery();

            }
        }



    }
}