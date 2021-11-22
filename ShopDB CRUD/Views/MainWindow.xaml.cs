using ShopDB_CRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using ToolTip = System.Windows.Controls.ToolTip;

namespace ShopDB_CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        string connectionString = "";
        DataSet dataset; //müvəqqqəti istifadə
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
        public MainWindow()
        {
            InitializeComponent();


            connection = new SqlConnection();
            connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            List<string> tablename = new List<string>();

            tablename.Add("Products");
            tablename.Add("Customers");
            tablename.Add("DetailsofOrder");
            tablename.Add("Orders");

            selecttablecombobox.ItemsSource = tablename;

            ToolTip t = new ToolTip();
            t.Content = "Select Table";
            selecttablecombobox.ToolTip = t;



        }

        public void RefreshTable()
        {

            using (connection = new SqlConnection())
            {
                ShopDatagrid.ItemsSource = null;
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dataset);

                ShopDatagrid.ItemsSource = dataset.Tables[0].DefaultView;

                sqlCommandBuilder.GetUpdateCommand();
            }
        }
        private void selecttablecombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selecttablecombobox.SelectedIndex != -1)
            {



                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    dataset = new DataSet();
                    sqlDataAdapter = new SqlDataAdapter($@"select *from {selecttablecombobox.SelectedItem.ToString()}", connection);


                    RefreshTable();

                }
            }


            if (selecttablecombobox.SelectedIndex == 0)
            {
                ShopDatagrid.Visibility = Visibility.Visible;
                ShopDatagrid.FontSize = 30;
                ShopDatagrid.Width = 800;

                ProductnameTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                ProductnameTxt.FontWeight = FontWeights.Bold;
                ProductnameTxt.IsUndoEnabled = false;
                ProductnameTxt.AcceptsReturn = true;
                ProductnameTxt.AcceptsTab = true;
                ProductnameTxt.FontSize = 20;
                ProductnameTxt.VerticalContentAlignment = VerticalAlignment.Center;
                ProductnameTxt.MaxLength = 40;
                ProductnameTxt.MaxLines = 40;
                ProductnameTxt.Focusable = true;
                ProductnameTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                ProductnameTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ProductnameTxt.Padding = new Thickness(5, 5, 5, 5);
                ProductnameTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t = new ToolTip();
                t.Content = "Add Product Name";
                ProductnameTxt.ToolTip = t;




                Grid.SetRow(ProductnameTxt, 0);
                CrudTextgrid.Children.Add(ProductnameTxt);


                ProductIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                ProductIdTxt.FontWeight = FontWeights.Bold;
                ProductIdTxt.IsUndoEnabled = false;
                ProductIdTxt.AcceptsReturn = true;
                ProductIdTxt.AcceptsTab = true;
                ProductIdTxt.FontSize = 20;
                ProductIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                ProductIdTxt.MaxLength = 25;
                ProductIdTxt.MaxLines = 25;
                ProductIdTxt.Focusable = true;
                ProductIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                ProductIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ProductIdTxt.Padding = new Thickness(5, 5, 5, 5);
                ProductIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t2 = new ToolTip();
                t2.Content = "Add Product Id";
                ProductIdTxt.ToolTip = t2;
                ProductIdTxt.TextChanged += ProductIdTxt_TextChanged;



                Grid.SetRow(ProductIdTxt, 1);
                CrudTextgrid.Children.Add(ProductIdTxt);
            }

            if (selecttablecombobox.SelectedIndex != 0)
            {
                CrudTextgrid.Children.Remove(ProductnameTxt);
                CrudTextgrid.Children.Remove(ProductIdTxt);
            }

            if (selecttablecombobox.SelectedIndex == 1)
            {
                ShopDatagrid.Visibility = Visibility.Visible;
                ShopDatagrid.FontSize = 30;
                ShopDatagrid.Width = 800;

                CustomernameTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                CustomernameTxt.FontWeight = FontWeights.Bold;
                CustomernameTxt.IsUndoEnabled = false;
                CustomernameTxt.AcceptsReturn = true;
                CustomernameTxt.AcceptsTab = true;
                CustomernameTxt.FontSize = 20;
                CustomernameTxt.VerticalContentAlignment = VerticalAlignment.Center;
                CustomernameTxt.MaxLength = 40;
                CustomernameTxt.MaxLines = 40;
                CustomernameTxt.Focusable = true;
                CustomernameTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                CustomernameTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                CustomernameTxt.Padding = new Thickness(5, 5, 5, 5);
                CustomernameTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t = new ToolTip();
                t.Content = "Add Customer Name";
                CustomernameTxt.ToolTip = t;




                Grid.SetRow(CustomernameTxt, 0);
                CrudTextgrid.Children.Add(CustomernameTxt);


                CustomerIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                CustomerIdTxt.FontWeight = FontWeights.Bold;
                CustomerIdTxt.IsUndoEnabled = false;
                CustomerIdTxt.AcceptsReturn = true;
                CustomerIdTxt.AcceptsTab = true;
                CustomerIdTxt.FontSize = 20;
                CustomerIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                CustomerIdTxt.MaxLength = 25;
                CustomerIdTxt.MaxLines = 25;
                CustomerIdTxt.Focusable = true;
                CustomerIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                CustomerIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                CustomerIdTxt.Padding = new Thickness(5, 5, 5, 5);
                CustomerIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t2 = new ToolTip();
                t2.Content = "Add Customer Id";
                CustomerIdTxt.ToolTip = t2;
                CustomerIdTxt.TextChanged += CustomerIdTxt_TextChanged;



                Grid.SetRow(CustomerIdTxt, 1);
                CrudTextgrid.Children.Add(CustomerIdTxt);
            }

            if (selecttablecombobox.SelectedIndex != 1)
            {
                CrudTextgrid.Children.Remove(CustomernameTxt);
                CrudTextgrid.Children.Remove(CustomerIdTxt);
            }


            if (selecttablecombobox.SelectedIndex == 2)
            {
                ShopDatagrid.Visibility = Visibility.Visible;
                ShopDatagrid.FontSize = 30;
                ShopDatagrid.Width = 800;

                DetailsofOrderIsCashTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                DetailsofOrderIsCashTxt.FontWeight = FontWeights.Bold;
                DetailsofOrderIsCashTxt.IsUndoEnabled = false;
                DetailsofOrderIsCashTxt.AcceptsReturn = true;
                DetailsofOrderIsCashTxt.AcceptsTab = true;
                DetailsofOrderIsCashTxt.FontSize = 20;
                DetailsofOrderIsCashTxt.VerticalContentAlignment = VerticalAlignment.Center;
                DetailsofOrderIsCashTxt.MaxLength = 40;
                DetailsofOrderIsCashTxt.MaxLines = 40;
                DetailsofOrderIsCashTxt.Focusable = true;
                DetailsofOrderIsCashTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                DetailsofOrderIsCashTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DetailsofOrderIsCashTxt.Padding = new Thickness(5, 5, 5, 5);
                DetailsofOrderIsCashTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t = new ToolTip();
                t.Content = "Add DetailsofOrder cash";
                DetailsofOrderIsCashTxt.ToolTip = t;
                DetailsofOrderIsCashTxt.TextChanged += DetailsofOrderIsCashTxt_TextChanged;




                Grid.SetRow(DetailsofOrderIsCashTxt, 0);
                CrudTextgrid.Children.Add(DetailsofOrderIsCashTxt);


                DetailsofOrderDateOrderTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                DetailsofOrderDateOrderTxt.FontWeight = FontWeights.Bold;
                DetailsofOrderDateOrderTxt.IsUndoEnabled = false;
                DetailsofOrderDateOrderTxt.AcceptsReturn = true;
                DetailsofOrderDateOrderTxt.AcceptsTab = true;
                DetailsofOrderDateOrderTxt.FontSize = 20;
                DetailsofOrderDateOrderTxt.VerticalContentAlignment = VerticalAlignment.Center;
                DetailsofOrderDateOrderTxt.MaxLength = 25;
                DetailsofOrderDateOrderTxt.MaxLines = 25;
                DetailsofOrderDateOrderTxt.Focusable = true;
                DetailsofOrderDateOrderTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                DetailsofOrderDateOrderTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DetailsofOrderDateOrderTxt.Padding = new Thickness(5, 5, 5, 5);
                DetailsofOrderDateOrderTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t2 = new ToolTip();
                t2.Content = "Add DetailsofOrder Date Order dd/MM/yyyy";
                DetailsofOrderDateOrderTxt.ToolTip = t2;
                DetailsofOrderDateOrderTxt.TextChanged += DetailsofOrderDateOrderTxt_TextChanged;



                Grid.SetRow(DetailsofOrderDateOrderTxt, 1);
                CrudTextgrid.Children.Add(DetailsofOrderDateOrderTxt);



                DetailsofOrderIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                DetailsofOrderIdTxt.FontWeight = FontWeights.Bold;
                DetailsofOrderIdTxt.IsUndoEnabled = false;
                DetailsofOrderIdTxt.AcceptsReturn = true;
                DetailsofOrderIdTxt.AcceptsTab = true;
                DetailsofOrderIdTxt.FontSize = 20;
                DetailsofOrderIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                DetailsofOrderIdTxt.MaxLength = 25;
                DetailsofOrderIdTxt.MaxLines = 25;
                DetailsofOrderIdTxt.Focusable = true;
                DetailsofOrderIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                DetailsofOrderIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DetailsofOrderIdTxt.Padding = new Thickness(5, 5, 5, 5);
                DetailsofOrderIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t3 = new ToolTip();
                t3.Content = "Add DetailsofOrder Id";
                DetailsofOrderIdTxt.ToolTip = t3;
                DetailsofOrderIdTxt.TextChanged += DetailsofOrderIdTxt_TextChanged;



                Grid.SetRow(DetailsofOrderIdTxt, 2);
                CrudTextgrid.Children.Add(DetailsofOrderIdTxt);
            }

            if (selecttablecombobox.SelectedIndex != 2)
            {
                CrudTextgrid.Children.Remove(DetailsofOrderIsCashTxt);
                CrudTextgrid.Children.Remove(DetailsofOrderDateOrderTxt);
                CrudTextgrid.Children.Remove(DetailsofOrderIdTxt);
            }


            if (selecttablecombobox.SelectedIndex == 3)
            {
                ShopDatagrid.Visibility = Visibility.Visible;
                ShopDatagrid.FontSize = 30;
                ShopDatagrid.Width = 800;

                OrdersCustomerIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                OrdersCustomerIdTxt.FontWeight = FontWeights.Bold;
                OrdersCustomerIdTxt.IsUndoEnabled = false;
                OrdersCustomerIdTxt.AcceptsReturn = true;
                OrdersCustomerIdTxt.AcceptsTab = true;
                OrdersCustomerIdTxt.FontSize = 20;
                OrdersCustomerIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                OrdersCustomerIdTxt.MaxLength = 40;
                OrdersCustomerIdTxt.MaxLines = 40;
                OrdersCustomerIdTxt.Focusable = true;
                OrdersCustomerIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                OrdersCustomerIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                OrdersCustomerIdTxt.Padding = new Thickness(5, 5, 5, 5);
                OrdersCustomerIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t = new ToolTip();
                t.Content = "Add OrdersCustomerId";
                OrdersCustomerIdTxt.ToolTip = t;
                OrdersCustomerIdTxt.TextChanged += OrdersCustomerIdTxt_TextChanged;




                Grid.SetRow(OrdersCustomerIdTxt, 0);
                CrudTextgrid.Children.Add(OrdersCustomerIdTxt);


                OrdersProductIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                OrdersProductIdTxt.FontWeight = FontWeights.Bold;
                OrdersProductIdTxt.IsUndoEnabled = false;
                OrdersProductIdTxt.AcceptsReturn = true;
                OrdersProductIdTxt.AcceptsTab = true;
                OrdersProductIdTxt.FontSize = 20;
                OrdersProductIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                OrdersProductIdTxt.MaxLength = 25;
                OrdersProductIdTxt.MaxLines = 25;
                OrdersProductIdTxt.Focusable = true;
                OrdersProductIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                OrdersProductIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                OrdersProductIdTxt.Padding = new Thickness(5, 5, 5, 5);
                OrdersProductIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t2 = new ToolTip();
                t2.Content = "Add OrdersProductId";
                OrdersProductIdTxt.ToolTip = t2;
                OrdersProductIdTxt.TextChanged += OrdersProductIdTxt_TextChanged;



                Grid.SetRow(OrdersProductIdTxt, 1);
                CrudTextgrid.Children.Add(OrdersProductIdTxt);



                OrdersDetailsofOrderIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                OrdersDetailsofOrderIdTxt.FontWeight = FontWeights.Bold;
                OrdersDetailsofOrderIdTxt.IsUndoEnabled = false;
                OrdersDetailsofOrderIdTxt.AcceptsReturn = true;
                OrdersDetailsofOrderIdTxt.AcceptsTab = true;
                OrdersDetailsofOrderIdTxt.FontSize = 20;
                OrdersDetailsofOrderIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                OrdersDetailsofOrderIdTxt.MaxLength = 25;
                OrdersDetailsofOrderIdTxt.MaxLines = 25;
                OrdersDetailsofOrderIdTxt.Focusable = true;
                OrdersDetailsofOrderIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                OrdersDetailsofOrderIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                OrdersDetailsofOrderIdTxt.Padding = new Thickness(5, 5, 5, 5);
                OrdersDetailsofOrderIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t3 = new ToolTip();
                t3.Content = "Add OrdersDetailsofOrderId";
                OrdersDetailsofOrderIdTxt.ToolTip = t3;
                OrdersDetailsofOrderIdTxt.TextChanged += OrdersDetailsofOrderIdTxt_TextChanged;



                Grid.SetRow(OrdersDetailsofOrderIdTxt, 2);
                CrudTextgrid.Children.Add(OrdersDetailsofOrderIdTxt);


                OrdersIdTxt.Foreground = new SolidColorBrush(Colors.SkyBlue);
                OrdersIdTxt.FontWeight = FontWeights.Bold;
                OrdersIdTxt.IsUndoEnabled = false;
                OrdersIdTxt.AcceptsReturn = true;
                OrdersIdTxt.AcceptsTab = true;
                OrdersIdTxt.FontSize = 20;
                OrdersIdTxt.VerticalContentAlignment = VerticalAlignment.Center;
                OrdersIdTxt.MaxLength = 25;
                OrdersIdTxt.MaxLines = 25;
                OrdersIdTxt.Focusable = true;
                OrdersIdTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                OrdersIdTxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                OrdersIdTxt.Padding = new Thickness(5, 5, 5, 5);
                OrdersIdTxt.Margin = new Thickness(5, 5, 5, 5);
                ToolTip t4 = new ToolTip();
                t4.Content = "Add OrdersId";
                OrdersIdTxt.ToolTip = t4;
                OrdersIdTxt.TextChanged += OrdersDetailsofOrderIdTxt_TextChanged;



                Grid.SetRow(OrdersIdTxt, 3);
                CrudTextgrid.Children.Add(OrdersIdTxt);
            }

            if (selecttablecombobox.SelectedIndex != 3)
            {
                CrudTextgrid.Children.Remove(OrdersCustomerIdTxt);
                CrudTextgrid.Children.Remove(OrdersProductIdTxt);
                CrudTextgrid.Children.Remove(OrdersDetailsofOrderIdTxt);
                CrudTextgrid.Children.Remove(OrdersIdTxt);
            }


        }

        private void DetailsofOrderIsCashTxt_TextChanged(object sender, TextChangedEventArgs e)
        {




            if (System.Text.RegularExpressions.Regex.IsMatch(DetailsofOrderIsCashTxt.Text, "^(?i)(True|False)e$"))
            {
                MessageBox.Show("Please enter only True or False.");
                DetailsofOrderIsCashTxt.Text = DetailsofOrderIsCashTxt.Text.Remove(DetailsofOrderIsCashTxt.Text.Length - 1);
            }



        }

        private void OrdersDetailsofOrderIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(OrdersDetailsofOrderIdTxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                OrdersDetailsofOrderIdTxt.Text = OrdersDetailsofOrderIdTxt.Text.Remove(OrdersDetailsofOrderIdTxt.Text.Length - 1);
            }
        }

        private void OrdersProductIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(OrdersProductIdTxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                OrdersProductIdTxt.Text = OrdersProductIdTxt.Text.Remove(OrdersProductIdTxt.Text.Length - 1);
            }
        }

        private void OrdersCustomerIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(OrdersCustomerIdTxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                OrdersCustomerIdTxt.Text = OrdersCustomerIdTxt.Text.Remove(OrdersCustomerIdTxt.Text.Length - 1);
            }
        }

        private void DetailsofOrderIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(DetailsofOrderIdTxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                DetailsofOrderIdTxt.Text = DetailsofOrderIdTxt.Text.Remove(DetailsofOrderIdTxt.Text.Length - 1);
            }
        }

        private void DetailsofOrderDateOrderTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");


            bool isValid = regex.IsMatch(DetailsofOrderDateOrderTxt.Text);


            DateTime dt;
            isValid = DateTime.TryParseExact(DetailsofOrderDateOrderTxt.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            if (isValid)
            {
                message = "Datetime is correct.";

            }


        }

        private void CustomerIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(CustomerIdTxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                CustomerIdTxt.Text = CustomerIdTxt.Text.Remove(CustomerIdTxt.Text.Length - 1);
            }
        }

        private void ProductIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ProductIdTxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                ProductIdTxt.Text = ProductIdTxt.Text.Remove(ProductIdTxt.Text.Length - 1);
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (selecttablecombobox.SelectedIndex == 0)
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

                if (selecttablecombobox.SelectedIndex == 1)
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

                if (selecttablecombobox.SelectedIndex == 2)
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

                if (selecttablecombobox.SelectedIndex == 3)
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
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            using (connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();


                try
                {
                    if (selecttablecombobox.SelectedIndex == 0)
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


                    if (selecttablecombobox.SelectedIndex == 1)
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

                    if (selecttablecombobox.SelectedIndex == 2)
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

                    if (selecttablecombobox.SelectedIndex == 3)
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
        }

        private void ShopDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selecttablecombobox.SelectedIndex == 0)
            {

                dataRowView = ShopDatagrid.SelectedItem as DataRowView;

                if (dataRowView != null)
                {
                    ProductIdTxt.Text = dataRowView["Id"].ToString();
                    ProductnameTxt.Text = dataRowView["Name of Products"].ToString();
                }
            }

            if (selecttablecombobox.SelectedIndex == 1)
            {

                dataRowView = ShopDatagrid.SelectedItem as DataRowView;

                if (dataRowView != null)
                {
                    CustomerIdTxt.Text = dataRowView["Id"].ToString();
                    CustomernameTxt.Text = dataRowView["Name of Customers"].ToString();
                }
            }


            if (selecttablecombobox.SelectedIndex == 2)
            {

                dataRowView = ShopDatagrid.SelectedItem as DataRowView;

                if (dataRowView != null)
                {
                    DetailsofOrderIdTxt.Text = dataRowView["Id"].ToString();
                    DetailsofOrderIsCashTxt.Text = dataRowView["isCash"].ToString();
                    DetailsofOrderDateOrderTxt.Text = dataRowView["DateOrder"].ToString();


                }
            }

            if (selecttablecombobox.SelectedIndex == 3)
            {


                dataRowView = ShopDatagrid.SelectedItem as DataRowView;

                if (dataRowView != null)
                {
                    OrdersIdTxt.Text = dataRowView["Id"].ToString();
                    OrdersCustomerIdTxt.Text = dataRowView["CustomersId for Orders"].ToString();
                    OrdersProductIdTxt.Text = dataRowView["ProductsId for Orders"].ToString();
                    OrdersDetailsofOrderIdTxt.Text = dataRowView["DetailsofOrder for Orders"].ToString();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();


                try
                {
                    if (selecttablecombobox.SelectedIndex == 0)
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

                    if (selecttablecombobox.SelectedIndex == 1)
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

                    if (selecttablecombobox.SelectedIndex == 2)
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

                    if (selecttablecombobox.SelectedIndex == 3)
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
        }


    }
}
