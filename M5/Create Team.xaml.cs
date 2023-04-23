using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Create_Team.xaml
    /// </summary>
    public partial class Create_Team : Window
    {
        public Create_Team()
        {
            InitializeComponent();

            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

            SqlCon.Open();
            
            SqlCommand cmdAddToListPlayer = new SqlCommand("Select PlayerID,FirstName, LastName from Players", SqlCon);
            SqlDataReader readerAddToListPlayer;
            readerAddToListPlayer = cmdAddToListPlayer.ExecuteReader();
            while (readerAddToListPlayer.Read())
            {
                PlayersBox.Items.Add(readerAddToListPlayer["PlayerID"].ToString() +" "+ readerAddToListPlayer["FirstName"].ToString() +" "+ readerAddToListPlayer["LastName"].ToString());
            }

            readerAddToListPlayer.Close();


            SqlCommand cmdAddToListTeam = new SqlCommand("Select TeamId,TeamName from Team", SqlCon);
            SqlDataReader readerAddToListTeam;
            readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
            while (readerAddToListTeam.Read())
            {
                TeamsBox.Items.Add(readerAddToListTeam["TeamId"].ToString() + " " + readerAddToListTeam["TeamName"].ToString());
            }

            readerAddToListTeam.Close();
            SqlCon.Close();


        }

        private void PlayersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

            try
            {

                

                if (TeamsBox.SelectedIndex>-1 && PlayersBox.SelectedIndex>-1)
                {
                    string query = $"INSERT INTO TeamPlayers (TeamId,PlayerID) VALUES( {TeamsBox.SelectedItem.ToString().Substring(0, TeamsBox.SelectedItem.ToString().IndexOf(" "))}, {PlayersBox.SelectedItem.ToString().Substring(0, PlayersBox.SelectedItem.ToString().IndexOf(" "))});";
                    SqlCon.Open();
                    SqlCommand cmd = new SqlCommand(query, SqlCon);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Please slect a team before adding players!");
                }


                string showData = $"SELECT Players.FirstName,Players.LastName FROM Players left Join TeamPlayers on Players.PlayerID = TeamPlayers.PlayerID where TeamId = {TeamsBox.SelectedItem.ToString().Substring(0, TeamsBox.SelectedItem.ToString().IndexOf(" "))}";
                SqlCommand cmd2 = new SqlCommand(showData, SqlCon);
                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                
                TeamDataGrid.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Player already in team");
            }
            finally { SqlCon.Close(); } 
            

        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

            try
            {
                string query = $"INSERT INTO Team(TeamName) VALUES('{NewTeamName.Text}');";
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Team created. Add players!");
                
                TeamsBox.Items.Clear();
                
                SqlCommand cmdAddToListTeam = new SqlCommand("Select TeamId,TeamName from Team", SqlCon);
                SqlDataReader readerAddToListTeam;
                readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
                while (readerAddToListTeam.Read())
                {
                    TeamsBox.Items.Add(readerAddToListTeam["TeamId"].ToString() + " " + readerAddToListTeam["TeamName"].ToString());
                }

                readerAddToListTeam.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { SqlCon.Close(); }


           
        }

        private void TeamsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");

            string showData = $"SELECT Players.FirstName,Players.LastName FROM Players left Join TeamPlayers on Players.PlayerID = TeamPlayers.PlayerID where TeamId = {TeamsBox.SelectedItem.ToString().Substring(0, TeamsBox.SelectedItem.ToString().IndexOf(" "))}";
            SqlCommand cmd2 = new SqlCommand(showData, SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            TeamDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_player add_Player = new Add_player();
            add_Player.Show();
            this.Close();   
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Create_Tournament create_Tournament = new Create_Tournament();
            create_Tournament.Show();
            this.Close();
        }

        public void name_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= name_GotFocus;
        }
    }
}
