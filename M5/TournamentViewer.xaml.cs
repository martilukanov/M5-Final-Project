using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace M5
{
    /// <summary>
    /// Interaction logic for TournamentViewer.xaml
    /// </summary>
    /// 
    public partial class TournamentViewer : Window
    {
        Tournament currentTournament = new Tournament();
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
        bool Oneleg = false;
        bool Twoleg = false;
        bool Threeleg = false;
        bool OnelegFinished = false;
        bool TwolegFinished = false;
        bool ThreelegFinished = false;
        public TournamentViewer()
        {
            InitializeComponent();

            
            
            SqlCon.Open();
            

            SqlCommand cmdleg1 = new SqlCommand($"Select legId,result from TournamentTracker where tournamentId = {currentTournament.TournamentID} AND legId = 1", SqlCon);
            SqlDataReader readerleg1;
            readerleg1 = cmdleg1.ExecuteReader();
            while (readerleg1.Read())
            {
                int teamsLegOneFinished = 0;  
                if (readerleg1["result"].ToString()=="1" || readerleg1["result"].ToString() == "2")
                {
                    teamsLegOneFinished++;
                }
                if (teamsLegOneFinished==8)
                {
                    OnelegFinished = true;
                    
                }
                
                
                Oneleg = true;
                Twoleg = false;
                Threeleg = false;

            }
            readerleg1 .Close();



            SqlCommand cmd5 = new SqlCommand($"Select TOP 1 tournamentName from Tournament where tournamentId = {currentTournament.TournamentID}", SqlCon);
            SqlDataReader reader5;
            reader5 = cmd5.ExecuteReader();
            while (reader5.Read())
            {
                TournamentName.Content = reader5["tournamentName"].ToString();
            }
            reader5.Close();

            //Operations before leg 1 has started
            if (Oneleg == false)
            {
                currentTournament.CurrentTournamentTeamList.Shuffle();

                double m = 1;
                for (int i = 0; i < 8; i++)
                {
                    
                    string query = $"INSERT INTO TournamentTracker(tournamentId,legId,matchId,teamId,buttonAllocation) VALUES({currentTournament.TournamentID},1,{Convert.ToInt32(m)},{currentTournament.CurrentTournamentTeamList[i]},'L1T{i+1}');";
                    SqlCommand cmd = new SqlCommand(query, SqlCon);
                    cmd.ExecuteNonQuery();
                    m = m + 0.499999;
                }
            }

            //Operations After leg 1 is finished
            

            SqlCommand cmdleg1buttons = new SqlCommand($"Select Team.TeamName,TournamentTracker.buttonAllocation,TournamentTracker.result from TournamentTracker Right Join Team on TournamentTracker.teamId = Team.teamId where TournamentTracker.tournamentId = {currentTournament.TournamentID} AND TournamentTracker.legId = 1", SqlCon);
            SqlDataReader readerleg1buttons;
            readerleg1buttons = cmdleg1buttons.ExecuteReader();
            while (readerleg1buttons.Read())
            {
                if (readerleg1buttons["buttonAllocation"].ToString() == "L1T1")
                {
                    L1T1.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T2")
                {
                    L1T2.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T3")
                {
                    L1T3.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T4")
                {
                    L1T4.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T5")
                {
                    L1T5.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T6")
                {
                    L1T6.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T7")
                {
                    L1T7.Content = $"{readerleg1buttons["TeamName"]}";
                }
                else if (readerleg1buttons["buttonAllocation"].ToString() == "L1T8")
                {
                    L1T8.Content = $"{readerleg1buttons["TeamName"]}";
                }



                if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T1") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T1.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T2") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T2.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T3") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T3.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T4") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T4.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T5") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T5.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T6") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T6.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T7") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T7.IsEnabled = true;
                }
                else if ((readerleg1buttons["buttonAllocation"].ToString() == "L1T8") && (readerleg1buttons["result"] == DBNull.Value))
                {
                    L1T8.IsEnabled = true;
                }

            }



            readerleg1buttons.Close();  


            SqlCommand cmdleg2 = new SqlCommand($"Select legId, result from TournamentTracker where tournamentId = '{currentTournament.TournamentID}' AND legId = 2", SqlCon);
            SqlDataReader readerleg2;
            readerleg2 = cmdleg2.ExecuteReader();
            while (readerleg2.Read())
            {
                int teamsLegTwoFinished = 0;
                if (readerleg2["result"].ToString() == "1" || readerleg2["result"].ToString() == "2")
                {
                    teamsLegTwoFinished++;
                }
                if (teamsLegTwoFinished == 4)
                {
                    TwolegFinished = true;
                }

                Oneleg = false;
                Twoleg = true;
                Threeleg = false;

            }
            readerleg2.Close();

            SqlCommand cmdleg3 = new SqlCommand($"Select legId, result from TournamentTracker where tournamentId = '{currentTournament.TournamentID}' AND legId = 3 AND result = 1" , SqlCon);
            SqlDataReader readerleg3;
            readerleg3 = cmdleg3.ExecuteReader();
            if (readerleg3.Read())
            {
                


                ThreelegFinished = true;
   

                Oneleg = false;
                Twoleg = false;
                Threeleg = true;

            }
            readerleg3.Close();

            


            if(ThreelegFinished == true)
            {
                using (var cmd = new SqlCommand($"Select teamName from Team right join TournamentTracker on Team.TeamId = TournamentTracker.TeamId where TournamentTracker.tournamentId = {currentTournament.TournamentID} and legId = 3 and result = 1", SqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            TournamentName.Content = "Winners: " + reader["teamName"].ToString();


                        }
                    }
                }
            }

            SqlCon.Close();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        string passedButtonContent;
        string eliminatedButtonContent;
        private void L1T1_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T1", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T1", 1);
            Extensions.UpdateTableWinner("L1T1",matchId, 1, teamId);

            L1T1.IsEnabled = false;
            L1T2.IsEnabled = false;

            //passedButtonContent = L1T1.Content.ToString();
            //eliminatedButtonContent = L1T2.Content.ToString();

            //L1T1.Content = passedButtonContent + " " + "(Progressed)";
            //L1T2.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }

        private void L1T2_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T2", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T2", 1);
            Extensions.UpdateTableWinner("L1T2",matchId, 1, teamId);

            L1T1.IsEnabled = false;
            L1T2.IsEnabled = false;

            //passedButtonContent = L1T2.Content.ToString();
            //eliminatedButtonContent = L1T1.Content.ToString();

            //L1T2.Content = passedButtonContent + " " + "(Progressed)";
            //L1T1.Content = eliminatedButtonContent + " " + "(Eliminated)";

            ButtonAllocation();
        }

        private void L1T3_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T3", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T3", 1);
            Extensions.UpdateTableWinner("L1T3", matchId, 1, teamId);

            L1T3.IsEnabled = false;
            L1T4.IsEnabled = false;

            //passedButtonContent = L1T3.Content.ToString();
            //eliminatedButtonContent = L1T4.Content.ToString();

            //L1T3.Content = passedButtonContent + " " + "(Progressed)";
            //L1T4.Content = eliminatedButtonContent + " " + "(Eliminated)";

            ButtonAllocation();
        }

        private void L1T4_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T4", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T4", 1);
            Extensions.UpdateTableWinner("L1T4", matchId, 1, teamId);

            L1T3.IsEnabled = false;
            L1T4.IsEnabled = false;


            //passedButtonContent = L1T4.Content.ToString();
            //eliminatedButtonContent = L1T3.Content.ToString();

            //L1T4.Content = passedButtonContent + " " + "(Progressed)";
            //L1T3.Content = eliminatedButtonContent + " " + "(Eliminated)";

            ButtonAllocation();
        }

        private void L1T5_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T5", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T5", 1);
            Extensions.UpdateTableWinner("L1T5", matchId, 1, teamId);

            L1T5.IsEnabled = false;
            L1T6.IsEnabled = false;

            //passedButtonContent = L1T5.Content.ToString();
            //eliminatedButtonContent = L1T6.Content.ToString();

            //L1T5.Content = passedButtonContent + " " + "(Progressed)";
            //L1T6.Content = eliminatedButtonContent + " " + "(Eliminated)";

            ButtonAllocation();
        }

        private void L1T6_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T6", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T6", 1);
            Extensions.UpdateTableWinner("L1T6", matchId, 1, teamId);

            L1T5.IsEnabled = false;
            L1T6.IsEnabled = false;


            //passedButtonContent = L1T6.Content.ToString();
            //eliminatedButtonContent = L1T5.Content.ToString();

            //L1T6.Content = passedButtonContent + " " + "(Progressed)";
            //L1T5.Content = eliminatedButtonContent + " " + "(Eliminated)";

            ButtonAllocation();
        }

        private void L1T7_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T7", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T7", 1);
            Extensions.UpdateTableWinner("L1T7", matchId, 1, teamId);

            L1T7.IsEnabled = false;
            L1T8.IsEnabled = false;


            //passedButtonContent = L1T7.Content.ToString();
            //eliminatedButtonContent = L1T8.Content.ToString();

            //L1T7.Content = passedButtonContent + " " + "(Progressed)";
            //L1T8.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }

        private void L1T8_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L1T8", 1);
            int teamId = Extensions.GetTeamIdFromButton("L1T8", 1);
            Extensions.UpdateTableWinner("L1T8", matchId, 1,teamId);

            L1T7.IsEnabled = false;
            L1T8.IsEnabled = false;


            //passedButtonContent = L1T8.Content.ToString();
            //eliminatedButtonContent = L1T7.Content.ToString();

            //L1T8.Content = passedButtonContent + " " + "(Progressed)";
            //L1T7.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }

        private void L2T1_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L2T1", 2);
            int teamId = Extensions.GetTeamIdFromButton("L2T1", 2);
            Extensions.UpdateTableWinner("L2T1", matchId, 2, teamId);

            L2T1.IsEnabled = false;
            L2T2.IsEnabled = false;


            //passedButtonContent = L2T1.Content.ToString();
            //eliminatedButtonContent = L2T2.Content.ToString();

            //L2T1.Content = passedButtonContent + " " + "(Progressed)";
            //L2T2.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }

        private void L2T2_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L2T2", 2);
            int teamId = Extensions.GetTeamIdFromButton("L2T2", 2);
            Extensions.UpdateTableWinner("L2T2", matchId, 2, teamId);

            L2T1.IsEnabled = false;
            L2T2.IsEnabled = false;


            //passedButtonContent = L2T2.Content.ToString();
            //eliminatedButtonContent = L2T1.Content.ToString();

            //L2T2.Content = passedButtonContent + " " + "(Progressed)";
            //L2T1.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }

        private void L2T3_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L2T3", 2);
            int teamId = Extensions.GetTeamIdFromButton("L2T3", 2);
            Extensions.UpdateTableWinner("L2T3", matchId, 2, teamId);

            L2T3.IsEnabled = false;
            L2T4.IsEnabled = false;


            //passedButtonContent = L2T3.Content.ToString();
            //eliminatedButtonContent = L2T4.Content.ToString();

            //L2T3.Content = passedButtonContent + " " + "(Progressed)";
            //L2T4.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }

        private void L2T4_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L2T4", 2);
            int teamId = Extensions.GetTeamIdFromButton("L2T4", 2);
            Extensions.UpdateTableWinner("L2T4", matchId, 2, teamId);

            L2T3.IsEnabled = false;
            L2T4.IsEnabled = false;


            //passedButtonContent = L2T4.Content.ToString();
            //eliminatedButtonContent = L2T3.Content.ToString();

            //L2T4.Content = passedButtonContent + " " + "(Progressed)";
            //L2T3.Content = eliminatedButtonContent + " " + "(Eliminated)";


            ButtonAllocation();
        }


        private void L3T1_Click(object sender, RoutedEventArgs e)
        {
            string matchId = Extensions.GetMatchIdFromButton("L3T1", 3);
            int teamId = Extensions.GetTeamIdFromButton("L3T1", 3);


            L3T1.IsEnabled = false;
            L3T2.IsEnabled = false;


            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            Tournament currentTournament = new Tournament();
            WinnerPopup popup = new WinnerPopup();  

            SqlCon.Open();
            string query = $"Update TournamentTracker Set result = 1 where tournamentId = {currentTournament.TournamentID} AND matchId = '{matchId}' and legId = {3} and buttonAllocation = 'L3T1'";
            SqlCommand cmd = new SqlCommand(query, SqlCon);
            cmd.ExecuteNonQuery();

            string query2 = $"Update TournamentTracker set result = 2 where tournamentId = {currentTournament.TournamentID} AND matchId = '{matchId}' and legId = {3} and buttonAllocation != 'L3T1'";
            SqlCommand cmd2 = new SqlCommand(query2, SqlCon);
            cmd2.ExecuteNonQuery();


            //passedButtonContent = L3T1.Content.ToString();
            //eliminatedButtonContent = L3T2.Content.ToString();


            //L3T1.Content = passedButtonContent + " " + "(Winner)"; ;
            //L3T2.Content = eliminatedButtonContent + " " + "(Eliminated)";
            currentTournament.WinnerTeam = teamId;

            using (var cmd3 = new SqlCommand($"Select teamName from Team right join TournamentTracker on Team.TeamId = TournamentTracker.TeamId where TournamentTracker.tournamentId = {currentTournament.TournamentID} and legId = 3 and result = 1", SqlCon))
            {
                using (var reader3 = cmd3.ExecuteReader())
                {
                    while (reader3.Read())
                    {

                        TournamentName.Content = "Winners: " + reader3["teamName"].ToString();


                    }
                }
            }

            popup.Show();



        }

        private void L3T2_Click(object sender, RoutedEventArgs e)
        {
            L3T1.IsEnabled = false;
            L3T2.IsEnabled = false;


            string matchId = Extensions.GetMatchIdFromButton("L3T2", 3);
            int teamId = Extensions.GetTeamIdFromButton("L3T2", 3);


            L3T1.IsEnabled = false;
            L3T2.IsEnabled = false;


            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            Tournament currentTournament = new Tournament();
            WinnerPopup popup = new WinnerPopup();

            SqlCon.Open();
            string query = $"Update TournamentTracker Set result = 1 where tournamentId = {currentTournament.TournamentID} AND matchId = '{matchId}' and legId = {3} and buttonAllocation = 'L3T2'";
            SqlCommand cmd = new SqlCommand(query, SqlCon);
            cmd.ExecuteNonQuery();

            string query2 = $"Update TournamentTracker set result = 2 where tournamentId = {currentTournament.TournamentID} AND matchId = '{matchId}' and legId = {3} and buttonAllocation != 'L3T2'";
            SqlCommand cmd2 = new SqlCommand(query2, SqlCon);
            cmd2.ExecuteNonQuery();

            //passedButtonContent = L3T2.Content.ToString();
            //eliminatedButtonContent = L3T1.Content.ToString();


            //L3T2.Content = passedButtonContent + " " + "(Winner)"; ;
            //L3T1.Content = eliminatedButtonContent + " " + "(Eliminated)";
            currentTournament.WinnerTeam = teamId;

            using (var cmd3 = new SqlCommand($"Select teamName from Team right join TournamentTracker on Team.TeamId = TournamentTracker.TeamId where TournamentTracker.tournamentId = {currentTournament.TournamentID} and legId = 3 and result = 1", SqlCon))
            {
                using (var reader3 = cmd3.ExecuteReader())
                {
                    while (reader3.Read())
                    {

                        TournamentName.Content = "Winners: " + reader3["teamName"].ToString();


                    }
                }
            }

            popup.Show();
        }

        public void ButtonAllocation()
        {

            TournamentViewer tournamentViewer = new TournamentViewer();
            Tournament currentTournament = new Tournament();

            SqlConnection SqlCon2 = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            SqlCon2.Open();


            using (var cmd = new SqlCommand($"Select Team.TeamName,TournamentTracker.buttonAllocation,TournamentTracker.result from TournamentTracker Right Join Team on TournamentTracker.teamId = Team.teamId where TournamentTracker.tournamentId = {currentTournament.TournamentID}", SqlCon2))
            {
                using (var readerButtonAll = cmd.ExecuteReader())
                {
                    while (readerButtonAll.Read())
                    {
                        


                        if (readerButtonAll["buttonAllocation"].ToString() == "L2T1")
                        {
                            L2T1.Content = $"{readerButtonAll["TeamName"]}";
                        }
                        else if (readerButtonAll["buttonAllocation"].ToString() == "L2T2")
                        {
                            L2T2.Content = $"{readerButtonAll["TeamName"]}";
                        }
                        else if (readerButtonAll["buttonAllocation"].ToString() == "L2T3")
                        {
                            L2T3.Content = $"{readerButtonAll["TeamName"]}";
                        }
                        else if (readerButtonAll["buttonAllocation"].ToString() == "L2T4")
                        {
                            L2T4.Content = $"{readerButtonAll["TeamName"]}";
                        }
                        else if (readerButtonAll["buttonAllocation"].ToString() == "L3T1")
                        {
                            L3T1.Content = $"{readerButtonAll["TeamName"]}";
                        }
                        else if (readerButtonAll["buttonAllocation"].ToString() == "L3T2")
                        {
                            L3T2.Content = $"{readerButtonAll["TeamName"]}";
                        }



                        if ((readerButtonAll["buttonAllocation"].ToString() == "L2T1") && (readerButtonAll["result"] == DBNull.Value))
                        {
                            L2T1.IsEnabled = true;
                        }
                        else if ((readerButtonAll["buttonAllocation"].ToString() == "L2T2") && (readerButtonAll["result"] == DBNull.Value))
                        {
                            L2T2.IsEnabled = true;
                        }
                        else if ((readerButtonAll["buttonAllocation"].ToString() == "L2T3") && (readerButtonAll["result"] == DBNull.Value))
                        {
                            L2T3.IsEnabled = true;
                        }
                        else if ((readerButtonAll["buttonAllocation"].ToString() == "L2T4") && (readerButtonAll["result"] == DBNull.Value))
                        {
                            L2T4.IsEnabled = true;
                        }
                        else if ((readerButtonAll["buttonAllocation"].ToString() == "L3T1") && (readerButtonAll["result"] == DBNull.Value))
                        {
                            L3T1.IsEnabled = true;
                        }
                        else if ((readerButtonAll["buttonAllocation"].ToString() == "L3T2") && (readerButtonAll["result"] == DBNull.Value))
                        {
                            L3T2.IsEnabled = true;
                        }


                    }
                }
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();
        }
    }
    public static class Extensions
    {
        private static Random rand = new Random();

        public static void Shuffle<T>(this IList<T> values)
        {
            for (int i = values.Count - 1; i > 0; i--)
            {
                int k = rand.Next(i + 1);
                T value = values[k];
                values[k] = values[i];
                values[i] = value;
            }
        }

        public static void UpdateTableWinner(string clickedButton, string matchId, int legId, int teamId)
        {
            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            Tournament currentTournament = new Tournament();
            int MatchIdNextRound = 0;

            SqlCon.Open();
            string query = $"Update TournamentTracker Set result = 1 where tournamentId = {currentTournament.TournamentID} AND matchId = '{matchId}' and legId = {legId} and buttonAllocation = '{clickedButton}'";
            SqlCommand cmd = new SqlCommand(query, SqlCon);
            cmd.ExecuteNonQuery();

            string query2 = $"Update TournamentTracker set result = 2 where tournamentId = {currentTournament.TournamentID} AND matchId = '{matchId}' and legId = {legId} and buttonAllocation != '{clickedButton}'";
            SqlCommand cmd2 = new SqlCommand(query2, SqlCon);
            cmd2.ExecuteNonQuery();



            //SqlCommand cmd4 = new SqlCommand($"Select TOP 1 matchId from TournamentTracker where tournamentId = {currentTournament.TournamentID} AND legId = {legId+1} Order by matchId desc", SqlCon);
            //SqlDataReader reader4;
            //reader4 = cmd4.ExecuteReader();
            //while (reader4.Read())
            //{
            //    highestMatchIdNextRound = Convert.ToInt32(reader4["matchId"]); 

            //}
            //reader4.Close();

            string nextButton = "";
            if(clickedButton == "L1T1" || clickedButton == "L1T2")
            {
                nextButton = "L2T1";
                MatchIdNextRound = 1;
                
            }
            else if(clickedButton == "L1T3" || clickedButton == "L1T4")
            {
                nextButton = "L2T2";
                MatchIdNextRound = 1;
            }
            else if (clickedButton == "L1T5" || clickedButton == "L1T6")
            {
                nextButton = "L2T3";
                MatchIdNextRound = 2;
            }
            else if (clickedButton == "L1T7" || clickedButton == "L1T8")
            {
                nextButton = "L2T4";
                MatchIdNextRound = 2;
            }
            else if (clickedButton == "L2T1" || clickedButton == "L2T2")
            {
                nextButton = "L3T1";
                MatchIdNextRound = 1;
            }
            else if (clickedButton == "L2T3" || clickedButton == "L2T4")
            {
                nextButton = "L3T2";
                MatchIdNextRound = 1;
            }
            



            string query3 = $"INSERT INTO TournamentTracker(tournamentId,legId,matchId,teamId,buttonAllocation) VALUES({currentTournament.TournamentID},{legId+1},{MatchIdNextRound},{teamId},'{nextButton}');";
            SqlCommand cmd3 = new SqlCommand(query3, SqlCon);
            cmd3.ExecuteNonQuery();



            SqlCon.Close();
        }

        public static string GetMatchIdFromButton(string clickedButton, int legId)
        {

            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            Tournament currentTournament = new Tournament();
            
            SqlCon.Open();
            string clickedButtonMatchId = "";
            SqlCommand cmd = new SqlCommand($"Select matchId from TournamentTracker where tournamentId = '{currentTournament.TournamentID}' AND legId = {legId} And buttonAllocation = '{clickedButton}'", SqlCon);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                clickedButtonMatchId = reader["matchId"].ToString();

            }
            reader.Close();

            SqlCon.Close();

            return clickedButtonMatchId;
            
            
        }

        public static int GetTeamIdFromButton(string clickedButton, int legId)
        {

            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
            Tournament currentTournament = new Tournament();

            SqlCon.Open();
            int clickedButtonTeamId = 0;
            SqlCommand cmd = new SqlCommand($"Select teamId from TournamentTracker where tournamentId = '{currentTournament.TournamentID}' AND legId = {legId} And buttonAllocation = '{clickedButton}'", SqlCon);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                clickedButtonTeamId = Convert.ToInt32(reader["teamId"].ToString());

            }

            reader.Close();

            SqlCon.Close();

            return clickedButtonTeamId;

            
        }
        
    }
}
