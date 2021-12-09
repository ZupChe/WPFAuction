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

        public bool IsOpened
        {
            get
            {
                return !_isClosed;
            }
        }

        public string Active
        {
            get
            {
                return this._isClosed ? "Closed" : "Active";
            }
        }



        private DispatcherTimer _bidCouter;
        private TimeSpan _bidTime;
        private DateTime _bidStartDate;



        public string BidTime
        {
            get { return _bidTime > default (TimeSpan) ? _bidTime.ToString(@"mm\:ss") : string.Empty; }
        }



        public Item(int Id, string Name, string Detail, int Price, string LastBidUser, bool IsClosed)
        {
            this.Name = Name;
            this.Detail = Detail;
            this.Price = Price;
            this.LastBidUser = LastBidUser;
            this.IsClosed = IsClosed;
            this.Id = Id;



            if (!IsClosed)
            {
                _bidStartDate = DateTime.Now;
                _bidCouter = new DispatcherTimer();
                _bidCouter.Interval = TimeSpan.FromMilliseconds(300);
                _bidCouter.Tick += (s, a) =>
                {
                    _bidTime = TimeSpan.FromMinutes(2) - (DateTime.Now - _bidStartDate);
                    if (_bidTime <= default (TimeSpan))
                    {
                        this.Close();
                    }

                    OnPropertyChanged(new PropertyChangedEventArgs("BidTime"));


                };

                _bidCouter.Start();
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

        public void Close()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE [Auctions] SET IsClosed=1 WHERE Id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;
              
                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();
                if(rows > 0)
                {
                    this.IsClosed = true;
                    this._bidCouter.Stop();
                }

            }
        }

        private void ResetTime()
        {
            this._bidStartDate = DateTime.Now;
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

       
        public void UpdatePrice(User LoginUser)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE [Auctions] SET LastBidUser=@lbu, Price=(select Price from Auctions where Id=@Id)+1 WHERE Id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;


               
                command.Parameters.Add(myParam);

                command.Parameters.AddWithValue("@lbu", LoginUser.Id);

                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    this.Price++;
                    this.ResetTime();
                    this.LastBidUser = LoginUser.DisplayName;
                }

            }
        }



    }
}