﻿using System;
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
    /// Interaction logic for Create_Tournament.xaml
    /// </summary>
    public partial class Create_Tournament : Window
    {
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
        bool createdTournament = false;
        string createdTournamentID;
        public Create_Tournament()
        {
            InitializeComponent();

            SqlCon.Open();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            


            try
            {
                //Add new tournament
                string query = $"INSERT INTO Tournament(tournamentName) VALUES('{TournamentName.Text}');";
                SqlCon.Open();
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                cmd.ExecuteNonQuery();
                createdTournament=true;

                //Get Created TournamentID
                SqlCommand cmdAddToListTeam = new SqlCommand($"Select tournamentId from Tournament where tournamentName = '{TournamentName.Text}'", SqlCon);
                SqlDataReader readerAddToListTeam;
                readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
                while (readerAddToListTeam.Read())
                {
                    createdTournamentID = readerAddToListTeam["tournamentId"].ToString();
                }

                readerAddToListTeam.Close();

                //for(int i = 1;i<=8; i++)
                //{
                //    string query2 = $"INSERT INTO TournamentTracker(tournamentId,legId) VALUES('{TournamentName.Text}',1);";
                //    SqlCommand cmd2 = new SqlCommand(query2, SqlCon);
                //    cmd2.ExecuteNonQuery();
                //}


                MessageBox.Show($"{createdTournamentID}");
                TournamentBox.Items.Clear();




                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { SqlCon.Close(); }
        }

        private void AddTeamToTouranment(object sender, RoutedEventArgs e)
        {
            
            if (createdTournament == true)
            {
                try
                {
                    string query = $"INSERT INTO TournamentTeams(tournamentId,teamId, stillPlaying) VALUES('{createdTournamentID}','{TeamsBox.SelectedItem.ToString().Substring(0, TeamsBox.SelectedItem.ToString().IndexOf(" "))}',1);";
                    SqlCon.Open();
                    SqlCommand cmd = new SqlCommand(query, SqlCon);
                    cmd.ExecuteNonQuery();
                }
                catch 
                {
                    MessageBox.Show("Team already in tournament");
                }
                

                SqlCommand cmdAddToBoxTeam = new SqlCommand($"Select TeamName from Team Right Join TournamentTeams on Team.TeamId = TournamentTeams.teamId where TournamentTeams.tournamentId = {createdTournamentID}", SqlCon);
                SqlDataAdapter sda = new SqlDataAdapter(cmdAddToBoxTeam);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                TournamentBox.ItemsSource = dt.DefaultView;
                SqlCon.Close();

                

            }



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();  
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Create_Team create_Team = new Create_Team();    
            create_Team.Show();
            this.Close();
        }
    }
}
