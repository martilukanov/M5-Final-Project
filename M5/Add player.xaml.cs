using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace M5
{
    /// <summary>
    /// Interaction logic for Add_player.xaml
    /// </summary>
    public partial class Add_player : Window
    {
        public Add_player()
        {
            InitializeComponent();

            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

            string showData = "SELECT [FirstName],[LastName] ,[Age],[email],[Country],[Height],[Weight]  FROM Players";
            SqlCommand cmd2 = new SqlCommand(showData, sqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            PDataGrid.ItemsSource = dt.DefaultView;

            sqlCon.Close();
        }

        private void PAdd_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

            try
            {
                string query = $"INSERT INTO Players(FirstName,LastName,Age,email,Country, Height, Weight) VALUES('{PFirstName.Text}', '{PLastName.Text}', {PAge.Text}, '{PEmail.Text}', '{PCountry.Text}', '{PHeight.Text}', '{PWeight.Text}');";
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.ExecuteNonQuery();


                string showData = "SELECT [FirstName],[LastName] ,[Age],[email],[Country],[Height],[Weight]  FROM Players";
                SqlCommand cmd2 = new SqlCommand(showData, sqlCon);
                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                PDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Create_Team create_Team = new Create_Team();
            create_Team.Show();
            this.Close();   
        }

        private void PWeight_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PWeight_GotFocus;
        }

        private void PHeight_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PHeight_GotFocus;
        }

        private void PCountry_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PCountry_GotFocus;
        }

        private void PEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PEmail_GotFocus;
        }

        private void PAge_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PAge_GotFocus;
        }

        private void PLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PLastName_GotFocus;
        }

        private void PFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PFirstName_GotFocus;
        }
    }
}
