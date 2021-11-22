using ShopDB_CRUD.Command;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopDB_CRUD.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainWindow MainView { get; set; }
        public RelayCommand InsertCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }



        SqlConnection connection;
        string connectionString = "";
        DataSet dataset;
        SqlDataAdapter sqlDataAdapter;
        DataRowView dataRowView;
        TextBox ProductnameTxt = new TextBox();
        TextBox ProductIdTxt = new TextBox();
        TextBox CustomernameTxt = new TextBox();
        TextBox CustomerIdTxt = new TextBox();
        TextBox DetailsofOrderIsCashTxt = new TextBox();
        TextBox DetailsofOrderDateOrderTxt = new TextBox();
        TextBox DetailsofOrderIdTxt = new TextBox();
        TextBox OrdersCustomerIdTxt = new TextBox();
        TextBox OrdersProductIdTxt = new TextBox();
        TextBox OrdersDetailsofOrderIdTxt = new TextBox();
        TextBox OrdersIdTxt = new TextBox();
        int MaxIdFromProducs = 0;
        int MaxIdFromCustomer = 0;
        int MaxIdFromDetailsofOrder = 0;
        int MaxIdFromOrders = 0;
        string message = " ";



        private List<string> _tablename;

        public List<string> Tablename
        {
            get { return _tablename; }
            set { _tablename = value; OnPropertyChanged(); }
        }






        public void RefreshTable()
        {

            using (connection = new SqlConnection())
            {
                MainView.ShopDatagrid.ItemsSource = null;
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dataset);

                MainView.ShopDatagrid.ItemsSource = dataset.Tables[0].DefaultView;

                sqlCommandBuilder.GetUpdateCommand();
            }
        }
        public MainViewModel()
        {
            Tablename = new List<string>();
            Tablename.Add("Products");
            Tablename.Add("Customers");
            Tablename.Add("DetailsofOrder");
            Tablename.Add("Orders");

            

            connection = new SqlConnection();
            connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;





            ToolTip t = new ToolTip();
            t.Content = "Select Table";
            //MainView.selecttablecombobox.ToolTip = t;

            InsertCommand = new RelayCommand(sender =>
            {
                try
                {

                    if (MainView.selecttablecombobox.SelectedIndex == 0)
                    {
                        using (connection = new SqlConnection())
                        {
                            connection.ConnectionString = connectionString;
                            connection.Open();

                            SqlCommand InsertsqlCommandbyStoredProcedure = new SqlCommand(@"sp_InsertProduct", connection);
                            InsertsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqlParameter1 = new SqlParameter();
                            sqlParameter1.SqlDbType = SqlDbType.NVarChar;
                            sqlParameter1.ParameterName = "@ProductName";
                            sqlParameter1.Value = ProductnameTxt.Text;
                            InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                            InsertsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            dataset = new DataSet();
                            sqlDataAdapter = new SqlDataAdapter(@"select * from Products", connection);



                            RefreshTable();
                        }
                    }

                    if (MainView.selecttablecombobox.SelectedIndex == 1)
                    {
                        using (connection = new SqlConnection())
                        {
                            connection.ConnectionString = connectionString;
                            connection.Open();

                            SqlCommand InsertsqlCommandbyStoredProcedure = new SqlCommand(@"sp_InsertCustomer", connection);
                            InsertsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqlParameter1 = new SqlParameter();
                            sqlParameter1.SqlDbType = SqlDbType.NVarChar;
                            sqlParameter1.ParameterName = "@CustomerName";
                            sqlParameter1.Value = CustomernameTxt.Text;
                            InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                            InsertsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            dataset = new DataSet();
                            sqlDataAdapter = new SqlDataAdapter(@"select * from Customers", connection);



                            RefreshTable();
                        }
                    }

                    if (MainView.selecttablecombobox.SelectedIndex == 2)
                    {
                        using (connection = new SqlConnection())
                        {
                            connection.ConnectionString = connectionString;
                            connection.Open();
                            if (message == "Datetime is correct.")
                            {
                                if (DetailsofOrderIsCashTxt.Text == "True" || DetailsofOrderIsCashTxt.Text == "False" || DetailsofOrderIsCashTxt.Text == "true" || DetailsofOrderIsCashTxt.Text == "false")
                                {

                                    SqlCommand InsertsqlCommandbyStoredProcedure = new SqlCommand(@"sp_InsertDetailsofOrder", connection);
                                    InsertsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                    SqlParameter sqlParameter1 = new SqlParameter();
                                    sqlParameter1.SqlDbType = SqlDbType.Bit;
                                    sqlParameter1.ParameterName = "@iscash";
                                    sqlParameter1.Value = DetailsofOrderIsCashTxt.Text;
                                    InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                                    SqlParameter sqlParameter2 = new SqlParameter();
                                    sqlParameter2.SqlDbType = SqlDbType.DateTime;
                                    sqlParameter2.ParameterName = "@dateorder";
                                    sqlParameter2.Value = DetailsofOrderDateOrderTxt.Text;
                                    InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter2);


                                    InsertsqlCommandbyStoredProcedure.ExecuteNonQuery();

                                    dataset = new DataSet();
                                    sqlDataAdapter = new SqlDataAdapter(@"select * from DetailsofOrder", connection);



                                    RefreshTable();
                                }
                                else
                                {
                                    MessageBox.Show("erroneous input");
                                }
                            }

                        }
                    }

                    if (MainView.selecttablecombobox.SelectedIndex == 3)
                    {
                        using (connection = new SqlConnection())
                        {
                            connection.ConnectionString = connectionString;
                            connection.Open();

                            SqlCommand InsertsqlCommandbyStoredProcedure = new SqlCommand(@"sp_InsertOrder", connection);
                            InsertsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqlParameter1 = new SqlParameter();
                            sqlParameter1.SqlDbType = SqlDbType.Int;
                            sqlParameter1.ParameterName = "@CustumerId";
                            sqlParameter1.Value = OrdersCustomerIdTxt.Text;
                            InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                            SqlParameter sqlParameter2 = new SqlParameter();
                            sqlParameter2.SqlDbType = SqlDbType.Int;
                            sqlParameter2.ParameterName = "@ProductId";
                            sqlParameter2.Value = OrdersProductIdTxt.Text;
                            InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter2);

                            SqlParameter sqlParameter3 = new SqlParameter();
                            sqlParameter3.SqlDbType = SqlDbType.Int;
                            sqlParameter3.ParameterName = "@DetailsofOrderId";
                            sqlParameter3.Value = OrdersDetailsofOrderIdTxt.Text;
                            InsertsqlCommandbyStoredProcedure.Parameters.Add(sqlParameter3);


                            InsertsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            dataset = new DataSet();
                            sqlDataAdapter = new SqlDataAdapter(@"select * from Orders", connection);



                            RefreshTable();
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}");
                }
            });
            UpdateCommand = new RelayCommand(sender =>
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    try
                    {
                        if (MainView.selecttablecombobox.SelectedIndex == 0)
                        {
                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdProducts", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = ProductIdTxt.Text;
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromProducs = Convert.ToInt32(sqloutputParameter.Value);


                            if (MaxIdFromProducs >= Convert.ToInt32(ProductIdTxt.Text))
                            {
                                SqlCommand UpdatesqlCommandbyStoredProcedure = new SqlCommand(@"sp_updateProduct", connection);
                                UpdatesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter1 = new SqlParameter();
                                sqlParameter1.SqlDbType = SqlDbType.NVarChar;
                                sqlParameter1.ParameterName = "@ProductName";
                                sqlParameter1.Value = ProductnameTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                                SqlParameter sqlParameter2 = new SqlParameter();
                                sqlParameter2.SqlDbType = SqlDbType.Int;
                                sqlParameter2.ParameterName = "@ProductId";
                                sqlParameter2.Value = ProductIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter2);

                                UpdatesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from Products", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromProducs <= Convert.ToInt32(ProductIdTxt.Text))
                            {
                                MessageBox.Show("You change ProductId that are not.");

                            }
                        }


                        if (MainView.selecttablecombobox.SelectedIndex == 1)
                        {

                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdCustomer", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = CustomerIdTxt.Text;
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromCustomer = Convert.ToInt32(sqloutputParameter.Value);


                            if (MaxIdFromCustomer >= Convert.ToInt32(CustomerIdTxt.Text))
                            {
                                SqlCommand UpdatesqlCommandbyStoredProcedure = new SqlCommand(@"sp_updateCustomer", connection);
                                UpdatesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter1 = new SqlParameter();
                                sqlParameter1.SqlDbType = SqlDbType.Int;
                                sqlParameter1.ParameterName = "@CustomerId";
                                sqlParameter1.Value = CustomerIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                                SqlParameter sqlParameter2 = new SqlParameter();
                                sqlParameter2.SqlDbType = SqlDbType.NVarChar;
                                sqlParameter2.ParameterName = "@CustomerName";
                                sqlParameter2.Value = CustomernameTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter2);


                                UpdatesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from Customers", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromCustomer <= Convert.ToInt32(CustomerIdTxt.Text))
                            {
                                MessageBox.Show("You change Customer that are not.");

                            }
                        }

                        if (MainView.selecttablecombobox.SelectedIndex == 2)
                        {

                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdDetailsofOrder", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = DetailsofOrderIdTxt.Text;
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromDetailsofOrder = Convert.ToInt32(sqloutputParameter.Value);


                            if (MaxIdFromDetailsofOrder >= Convert.ToInt32(DetailsofOrderIdTxt.Text))
                            {
                                SqlCommand UpdatesqlCommandbyStoredProcedure = new SqlCommand(@"sp_updateDetailsofOrder", connection);
                                UpdatesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter1 = new SqlParameter();
                                sqlParameter1.SqlDbType = SqlDbType.Bit;
                                sqlParameter1.ParameterName = "@iscash";
                                sqlParameter1.Value = DetailsofOrderIsCashTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                                SqlParameter sqlParameter2 = new SqlParameter();
                                sqlParameter2.SqlDbType = SqlDbType.Int;
                                sqlParameter2.ParameterName = "@DateorderId";
                                sqlParameter2.Value = DetailsofOrderIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter2);

                                SqlParameter sqlParameter3 = new SqlParameter();
                                sqlParameter3.SqlDbType = SqlDbType.DateTime;
                                sqlParameter3.ParameterName = "@dateorder";
                                sqlParameter3.Value = DetailsofOrderDateOrderTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter3);

                                UpdatesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from DetailsofOrder", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromDetailsofOrder <= Convert.ToInt32(DetailsofOrderIdTxt.Text))
                            {
                                MessageBox.Show("You change Customer that are not.");

                            }
                        }

                        if (MainView.selecttablecombobox.SelectedIndex == 3)
                        {


                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdOrder", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = DetailsofOrderIdTxt.Text;
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromOrders = Convert.ToInt32(sqloutputParameter.Value);


                            if (MaxIdFromOrders >= Convert.ToInt32(OrdersIdTxt.Text))
                            {
                                SqlCommand UpdatesqlCommandbyStoredProcedure = new SqlCommand(@"sp_updateOrder", connection);
                                UpdatesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter1 = new SqlParameter();
                                sqlParameter1.SqlDbType = SqlDbType.Int;
                                sqlParameter1.ParameterName = "@OrderId";
                                sqlParameter1.Value = OrdersIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter1);

                                SqlParameter sqlParameter2 = new SqlParameter();
                                sqlParameter2.SqlDbType = SqlDbType.Int;
                                sqlParameter2.ParameterName = "@CustumerId";
                                sqlParameter2.Value = OrdersCustomerIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter2);

                                SqlParameter sqlParameter3 = new SqlParameter();
                                sqlParameter3.SqlDbType = SqlDbType.Int;
                                sqlParameter3.ParameterName = "@ProductId";
                                sqlParameter3.Value = OrdersProductIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter3);

                                SqlParameter sqlParameter4 = new SqlParameter();
                                sqlParameter4.SqlDbType = SqlDbType.Int;
                                sqlParameter4.ParameterName = "@DetailsofOrderId";
                                sqlParameter4.Value = OrdersDetailsofOrderIdTxt.Text;
                                UpdatesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter4);

                                UpdatesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from Orders", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromOrders <= Convert.ToInt32(OrdersIdTxt.Text))
                            {
                                MessageBox.Show("You change Customer that are not.");

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message }");
                    }

                }

            });
            DeleteCommand = new RelayCommand(sender =>
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();


                    try
                    {
                        if (MainView.selecttablecombobox.SelectedIndex == 0)
                        {
                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdProducts", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = int.Parse(ProductIdTxt.Text);
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromProducs = Convert.ToInt32(sqloutputParameter.Value);

                            if (MaxIdFromProducs >= int.Parse(ProductIdTxt.Text))
                            {
                                SqlCommand DeletesqlCommandbyStoredProcedure = new SqlCommand(@"sp_deleteProduct", connection);
                                DeletesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter = new SqlParameter();
                                sqlParameter.SqlDbType = SqlDbType.Int;
                                sqlParameter.ParameterName = "@ProductId";
                                sqlParameter.Value = ProductIdTxt.Text;
                                DeletesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter);



                                DeletesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from Products", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromProducs <= Convert.ToInt32(ProductIdTxt.Text))
                            {
                                MessageBox.Show("You delete ProductId that are not or delete Last ProductId");

                            }
                        }

                        if (MainView.selecttablecombobox.SelectedIndex == 1)
                        {
                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdCustomer", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = int.Parse(CustomerIdTxt.Text);
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromCustomer = Convert.ToInt32(sqloutputParameter.Value);

                            if (MaxIdFromCustomer >= int.Parse(CustomerIdTxt.Text))
                            {
                                SqlCommand DeletesqlCommandbyStoredProcedure = new SqlCommand(@"sp_deleteCustomer", connection);
                                DeletesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter = new SqlParameter();
                                sqlParameter.SqlDbType = SqlDbType.Int;
                                sqlParameter.ParameterName = "@CustomerId";
                                sqlParameter.Value = CustomerIdTxt.Text;
                                DeletesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter);



                                DeletesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from Customers", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromCustomer <= Convert.ToInt32(CustomerIdTxt.Text))
                            {
                                MessageBox.Show("You delete CustomerId that are not or delete Last CustomerId");

                            }
                        }

                        if (MainView.selecttablecombobox.SelectedIndex == 2)
                        {
                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdDetailsofOrder", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = int.Parse(DetailsofOrderIdTxt.Text);
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromDetailsofOrder = Convert.ToInt32(sqloutputParameter.Value);

                            if (MaxIdFromDetailsofOrder >= int.Parse(DetailsofOrderIdTxt.Text))
                            {
                                SqlCommand DeletesqlCommandbyStoredProcedure = new SqlCommand(@"sp_deleteDetailsofOrder", connection);
                                DeletesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter = new SqlParameter();
                                sqlParameter.SqlDbType = SqlDbType.Int;
                                sqlParameter.ParameterName = "@DetailsofOrderId";
                                sqlParameter.Value = DetailsofOrderIdTxt.Text;
                                DeletesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter);



                                DeletesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from DetailsofOrder", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromDetailsofOrder <= Convert.ToInt32(DetailsofOrderIdTxt.Text))
                            {
                                MessageBox.Show("You delete DetailsofOrderId that are not or delete Last DetailsofOrderId");

                            }
                        }

                        if (MainView.selecttablecombobox.SelectedIndex == 3)
                        {
                            SqlCommand MaxsqlCommandbyStoredProcedure = new SqlCommand(@"sp_MaxIdOrder", connection);
                            MaxsqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                            SqlParameter sqloutputParameter = new SqlParameter();
                            sqloutputParameter.SqlDbType = SqlDbType.Int;
                            sqloutputParameter.ParameterName = "@MaxId";
                            sqloutputParameter.Value = int.Parse(OrdersIdTxt.Text);
                            sqloutputParameter.Direction = System.Data.ParameterDirection.Output;
                            MaxsqlCommandbyStoredProcedure.Parameters.Add(sqloutputParameter);

                            MaxsqlCommandbyStoredProcedure.ExecuteNonQuery();

                            MaxIdFromOrders = Convert.ToInt32(sqloutputParameter.Value);

                            if (MaxIdFromOrders >= int.Parse(OrdersIdTxt.Text))
                            {
                                SqlCommand DeletesqlCommandbyStoredProcedure = new SqlCommand(@"sp_deleteOrder", connection);
                                DeletesqlCommandbyStoredProcedure.CommandType = CommandType.StoredProcedure;

                                SqlParameter sqlParameter = new SqlParameter();
                                sqlParameter.SqlDbType = SqlDbType.Int;
                                sqlParameter.ParameterName = "@OrderId";
                                sqlParameter.Value = OrdersIdTxt.Text;
                                DeletesqlCommandbyStoredProcedure.Parameters.Add(sqlParameter);



                                DeletesqlCommandbyStoredProcedure.ExecuteNonQuery();

                                dataset = new DataSet();
                                sqlDataAdapter = new SqlDataAdapter(@"select * from Orders", connection);


                                RefreshTable();

                            }

                            if (MaxIdFromOrders <= Convert.ToInt32(OrdersIdTxt.Text))
                            {
                                MessageBox.Show("You delete OrderId that are not or delete Last OrderId");

                            }
                        }


                    }
                    catch (SqlException ex)
                    {

                        MessageBox.Show($"{ex.Message}");
                    }
                }

            });

        }
    }
}
