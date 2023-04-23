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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
        List<int> tournamentMembers = new List<int>();
        Tournament currentTournament = new Tournament();    
        
        public MainWindow()
        {
            InitializeComponent();

            SqlCon.Open();
            SqlCommand cmdAddToListTeam = new SqlCommand("Select tournamentID,tournamentName from Tournament", SqlCon);
            SqlDataReader readerAddToListTeam;
            readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
            while (readerAddToListTeam.Read())
            {
                TournamentBox.Items.Add(readerAddToListTeam["tournamentId"].ToString() + " " + readerAddToListTeam["tournamentName"].ToString());
            }

            readerAddToListTeam.Close();
            SqlCon.Close();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Create_Tournament create_Tournament = new Create_Tournament();  
            create_Tournament.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Open Tournament 

            
            //Get the id of the tournament that is chosen to be viewed and add it to the class
            SqlCon.Open();
            SqlCommand cmdAddToListTeam = new SqlCommand($"Select tournamentId from Tournament where tournamentId = '{TournamentBox.SelectedItem.ToString().Substring(0, TournamentBox.SelectedItem.ToString().IndexOf(" "))}'", SqlCon);
            SqlDataReader readerAddToListTeam;
            readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
            while (readerAddToListTeam.Read())
            {
                currentTournament.TournamentID= readerAddToListTeam["tournamentId"].ToString();
            }

            readerAddToListTeam.Close();
            SqlCon.Close();


            //Put the teams that are registered in the tournament that is chosen to be viewed into the list in the class
            SqlCon.Open();
            SqlCommand cmd= new SqlCommand($"Select teamId from TournamentTeams where tournamentId = '{TournamentBox.SelectedItem.ToString().Substring(0, TournamentBox.SelectedItem.ToString().IndexOf(" "))}'", SqlCon);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                currentTournament.AddTeamToList(reader["teamId"].ToString());
            }

            readerAddToListTeam.Close();
            SqlCon.Close();





            TournamentViewer viewer = new TournamentViewer();
            viewer.Show();
            this.Close();


            viewer.ButtonAllocation();
            

            //SqlCon.Open();
            //SqlCommand cmdAddToListTeam = new SqlCommand("Select teamId from tournamentTeams where ", SqlCon);
            //SqlDataReader readerAddToListTeam;
            //readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
            //while (readerAddToListTeam.Read())
            //{
            //    tournamentMembers.Add(Int32.Parse(readerAddToListTeam["TeamId"].ToString()));
            //}

            //readerAddToListTeam.Close();
            //SqlCon.Close();


        }

        private void TournamentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class Tournament
    {
        static string currentTournamentID;
        static int winnerTeam;
        static public List<string> currentTournamentTeamList = new List<string>();

        public string TournamentID
        {
            get { return currentTournamentID; }
            set { currentTournamentID = value; }
        }
        public int WinnerTeam
        {
            get { return winnerTeam; }

            set { winnerTeam = value; }
        }

        public List<string> CurrentTournamentTeamList
        {
            get { return currentTournamentTeamList; }
            
        }

        public void AddTeamToList(string value)
        {
            currentTournamentTeamList.Add(value);
        }

        public string GetElementFromList(int value)
        {
            return currentTournamentTeamList[value];
        }

        
    }
}
