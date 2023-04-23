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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();


        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Equals(""))
            {
                MessageBox.Show("Username is empty");
            }
            if (password.Equals(""))
            {
                MessageBox.Show("Password is empty");
            }
            else if (!(username.Text.Equals("")) && !(password.Equals("")))
            {


                SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

                bool pass = false;
                bool user = false;
                try
                {


                    string queryPass = "Select pass from UserInfo where username = '" + username.Text + "'";
                    sqlCon.Open();
                    SqlCommand cmdPass = new SqlCommand(queryPass, sqlCon);
                    SqlDataReader readerPass;
                    readerPass = cmdPass.ExecuteReader();
                    if (readerPass.Read())
                    {

                        if (readerPass["pass"].ToString() == password.Password)
                        {

                            pass = true;
                        }

                    }


                    readerPass.Close();

                    string query = "Select username from UserInfo where username = '" + username.Text + "'";

                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    string Getusername = "";
                    if (reader.Read())
                    {

                        if (reader["username"].ToString() == username.Text)
                        {
                            user = true;
                        }

                    }
                    else
                    {

                    }

                    reader.Close();

                    if (pass && user)
                    {
                        MainWindow obj = new MainWindow();
                        obj.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username or Password Inncorrect");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }





            }
        }

        private void Sign_Up_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= username_GotFocus;
        }

        

    }
}
