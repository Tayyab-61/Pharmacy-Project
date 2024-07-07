using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace Pharmacy_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddInventory.Visibility = Visibility.Hidden;
            SellInventory.Visibility = Visibility.Hidden;
            RecInventory.Visibility = Visibility.Hidden;
            RecSeller.Visibility = Visibility.Hidden;
            Dashboard.Visibility = Visibility.Visible;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dashboard.Visibility = Visibility.Hidden;
            SellInventory.Visibility = Visibility.Hidden;
            RecInventory.Visibility = Visibility.Hidden;
            RecSeller.Visibility = Visibility.Hidden;
            AddInventory.Visibility = Visibility.Visible;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddInventory.Visibility = Visibility.Hidden;
            Dashboard.Visibility = Visibility.Hidden;
            RecInventory.Visibility = Visibility.Hidden;
            RecSeller.Visibility = Visibility.Hidden;
            SellInventory.Visibility = Visibility.Visible;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddInventory.Visibility = Visibility.Hidden;
            SellInventory.Visibility = Visibility.Hidden;
            Dashboard.Visibility = Visibility.Hidden;
            RecSeller.Visibility = Visibility.Hidden;
            RecInventory.Visibility = Visibility.Visible;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30");
            try
            {
                con.Open();

                string sqlquery = "select * from InventoryRecord";
                SqlCommand command = new SqlCommand(sqlquery, con);
                SqlDataReader dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                InventoryGrid.ItemsSource = dt.DefaultView;
                MessageBox.Show("Connenction established succesfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Connenction can't established.");
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddInventory.Visibility = Visibility.Hidden;
            SellInventory.Visibility = Visibility.Hidden;
            RecInventory.Visibility = Visibility.Hidden;
            Dashboard.Visibility = Visibility.Hidden;
            RecSeller.Visibility = Visibility.Visible;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30");
            try
            {
                con.Open();


                string sqlquery = "SELECT CustomerId,CustomerName,ProductId ,DrugName,Cost,Contact,Address FROM InventoryRecord INNER JOIN CustomerRec ON InventoryRecord.Stockid=CustomerRec.ProductId";
                SqlCommand command = new SqlCommand(sqlquery, con);
                SqlDataReader dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                SellerRecord.ItemsSource = dt.DefaultView;

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connenction can't established." + ex);
            }

        }

        public void Button_Click_5(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30");
            try
            {
                con.Open();


                string sqlquery = "SELECT CustomerId,CustomerName,ProductId,DrugName,Catagory,Company,Cost FROM InventoryRecord INNER JOIN CustomerRec ON InventoryRecord.Stockid=CustomerRec.ProductId";
                SqlCommand command = new SqlCommand(sqlquery, con);
                SqlDataReader dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                SellerGrid.ItemsSource = dt.DefaultView;

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connenction can't established." + ex);
            }





        }

        private void Checker_Click(object sender, RoutedEventArgs e)

        {
            ProdId.IsReadOnly = true;
            ProdId.Text = stockIdCheck.Text;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30");
            try
            {
                con.Open();
                string id = stockIdCheck.Text;

                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Please enter a Stock ID");
                    return;
                }
                int Convertid = Convert.ToInt16(id);
                string sqlquery = "select * from InventoryRecord where Stockid='" + Convertid + "'";

                SqlCommand command = new SqlCommand(sqlquery, con);
                SqlDataReader dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                CheckerGrid.ItemsSource = dt.DefaultView;

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connenction can't established.");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string CustomerId = CustId.Text;
            string CustomerName = CusName.Text;
            string Contact = contact.Text;
            string Address = address.Text;
            string StrProductId = ProdId.Text;
            if (string.IsNullOrEmpty(CustomerId) || string.IsNullOrEmpty(StrProductId))
            {
                MessageBox.Show("Enter Customer id and Product id");
                return;
            }
            int ProductId = Convert.ToInt16(StrProductId);
            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30");
            try
            {
                con1.Open();


                string sqlquery = "Insert into CustomerRec values('" + CustomerId + "','" + CustomerName + "','" + Contact + "','" + Address + "','" + ProductId + "')";
                SqlCommand command = new SqlCommand(sqlquery, con1);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Insert successfully");
                }
                else
                {
                    MessageBox.Show("Insert failed");
                }


            }
            catch (SqlException)
            {
                MessageBox.Show("Something went wrong");
            }


        }

        private void Bill_Click(object sender, RoutedEventArgs e)
        {
            string stock = stockIdCheck.Text;
            CustomerName.Content = CusName.Text;
            DrugName.Content = Drugtext.Text;
            payment.Content = Paymenttext.Text;

        }

        private void AddInventory1_Click(object sender, RoutedEventArgs e)
        {
            string strStock = stockId.Text;
            if (String.IsNullOrEmpty(strStock))
            {
                MessageBox.Show("Please enter a valid Stock id");
                return;
            }
            int Stock = Convert.ToInt16(strStock);
            string Cost = cost.Text;
            string DrugName = drugName.Text;
            string Catagory = catagory.Text;
            string Company = company.Text;

            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30");
            try
            {
                con1.Open();


                string sqlquery = "Insert into InventoryRecord values('" + Stock + "','" + Cost + "','" + DrugName + "','" + Catagory + "','" + Company + "')";
                SqlCommand command = new SqlCommand(sqlquery, con1);
                SqlDataReader dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                InventoryGrid.ItemsSource = dt.DefaultView;
                MessageBox.Show("Insert successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void CLEAR_Click(object sender, RoutedEventArgs e)
        {
            stockId.Text = null;
            cost.Text = null;
            drugName.Text = null;
            catagory.Text = null;
            company.Text = null;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have a TextBox named stockId to get the Stock ID to delete.
            string strStock = stockId.Text;
            if (String.IsNullOrEmpty(strStock))
            {
                MessageBox.Show("Please enter a valid Stock id");
                return;
            }
            int Stock = Convert.ToInt16(strStock);

            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
            try
            {
                con1.Open();

                // Use parameterized query to prevent SQL injection
                string sqlquery = "DELETE FROM InventoryRecord WHERE StockId = @Stock";
                SqlCommand command = new SqlCommand(sqlquery, con1);
                command.Parameters.AddWithValue("@Stock", Stock);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Delete successfully");
                }
                else
                {
                    MessageBox.Show("No record found with the given Stock ID");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }
            finally
            {
                if (con1.State == System.Data.ConnectionState.Open)
                {
                    con1.Close();
                }
            }

        }

        private void delete_Click_1(object sender, RoutedEventArgs e)
        {
            string CustomerId = CustId.Text;

            if (string.IsNullOrEmpty(CustomerId))
            {
                MessageBox.Show("Please enter a Customer ID");
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                try
                {
                    con1.Open();

                    string sqlquery = "DELETE FROM CustomerRec WHERE CustomerId = @CustomerId";
                    using (SqlCommand command = new SqlCommand(sqlquery, con1))
                    {
                        command.Parameters.AddWithValue("@CustomerId", CustomerId);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Delete successfully");
                        }
                        else
                        {
                            MessageBox.Show("No record found with the given Customer ID");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            string CustomerId = CustId.Text;
            string CustomerName = CusName.Text;
            string Contact = contact.Text;
            string Address = address.Text;
            string strProductId = ProdId.Text;

            if (string.IsNullOrEmpty(CustomerId) || string.IsNullOrEmpty(strProductId))
            {
                MessageBox.Show("Please enter valid Customer ID and Product ID");
                return;
            }
            int ProductId = Convert.ToInt16(strProductId);

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                try
                {
                    con1.Open();

                    string sqlquery = "UPDATE CustomerRec SET CustomerName = @CustomerName, Contact = @Contact, Address = @Address, ProductId = @ProductId WHERE CustomerId = @CustomerId";
                    using (SqlCommand command = new SqlCommand(sqlquery, con1))
                    {
                        command.Parameters.AddWithValue("@CustomerId", CustomerId);
                        command.Parameters.AddWithValue("@CustomerName", CustomerName);
                        command.Parameters.AddWithValue("@Contact", Contact);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@ProductId", ProductId);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Update successfully");
                        }
                        else
                        {
                            MessageBox.Show("No record found with the given Customer ID");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message);
                }
            }
        }

        private void Update_Click_1(object sender, RoutedEventArgs e)
        {
            string strStock = stockId.Text;
            if (string.IsNullOrEmpty(strStock))
            {
                MessageBox.Show("Please enter a valid Stock ID");
                return;
            }
            int Stock = Convert.ToInt16(strStock);
            string Cost = cost.Text;
            string DrugName = drugName.Text;
            string Category = catagory.Text; // Corrected spelling from "Catagory" to "Category"
            string Company = company.Text;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tayyab Ali\Documents\Pharmay System.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                try
                {
                    con1.Open();

                    string sqlquery = "UPDATE InventoryRecord SET Cost = @Cost, DrugName = @DrugName, Catagory = @Category, Company = @Company WHERE StockId = @Stock";
                    using (SqlCommand command = new SqlCommand(sqlquery, con1))
                    {
                        command.Parameters.AddWithValue("@Stock", Stock);
                        command.Parameters.AddWithValue("@Cost", Cost);
                        command.Parameters.AddWithValue("@DrugName", DrugName);
                        command.Parameters.AddWithValue("@Category", Category);
                        command.Parameters.AddWithValue("@Company", Company);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Update successfully");
                        }
                        else
                        {
                            MessageBox.Show("No record found with the given Stock ID");
                        }
                    }
                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }

            }
        }
    }
}


