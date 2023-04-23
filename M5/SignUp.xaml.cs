using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace M5
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Sign_Up_Click(object sender, RoutedEventArgs e)
        {


            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            sqlCon.Open();

            bool allTestspassed = true;
            string queryPass = "Select username from UserInfo where username = '" + username.Text + "'";

            SqlCommand cmdPass = new SqlCommand(queryPass, sqlCon);
            SqlDataReader readerPass;
            readerPass = cmdPass.ExecuteReader();
            if (readerPass.Read())
            {
                allTestspassed = false;
                MessageBox.Show("Username already exists");

            }

            readerPass.Close();


            
            if (username.Text.Equals(""))
            {
                MessageBox.Show("Username is empty");
                allTestspassed = false;
            }
            if (password.Equals(""))
            {
                MessageBox.Show("Password is empty");
                allTestspassed = false;

            }
            if (password.Password != passwordCheck.Password)
            {
                MessageBox.Show("Passwords don't match");
                allTestspassed = false;

            }
            if (allTestspassed == true)
            {


                

                try
                {
                    string query = "INSERT INTO UserInfo( username, pass) VALUES('" + username.Text + "', '" + password.Password + "');";
                    
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.ExecuteNonQuery();
                    LogIn obj = new LogIn();
                    obj.Show();
                    this.Close();
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
            
        }
        public void username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= username_GotFocus;
        }
    }
}
