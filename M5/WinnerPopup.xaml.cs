using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Data.SqlClient;

namespace M5
{
    /// <summary>
    /// Interaction logic for WinnerPopup.xaml
    /// </summary>
    public partial class WinnerPopup : Window
    {
        Tournament currentTournament = new Tournament();
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=M5Tournament; Integrated Security=True");
        public WinnerPopup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
            
            SqlCon.Open();

            int playersInTeam = 0;
            using (var cmd1 = new SqlCommand($"Select count(PlayerID) as [amountOfPlayersInWinnigTeam] from TeamPlayers where TeamId = {currentTournament.WinnerTeam}", SqlCon))
            {
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {

                        playersInTeam = Convert.ToInt32(reader1["amountOfPlayersInWinnigTeam"]);


                    }
                }
            }

            int prize = 0;
            string tournamentName = "";
            using (var cmd2 = new SqlCommand($"Select prize, tournamentName from Tournament where tournamentId = {currentTournament.TournamentID}", SqlCon))
            {
                using (var reader2 = cmd2.ExecuteReader())
                {
                    while (reader2.Read())
                    {

                        prize = Convert.ToInt32(reader2["prize"]);
                        tournamentName = reader2["tournamentName"].ToString();

                    }
                }
            }

            bool error = false;
            using (var cmd = new SqlCommand($"Select * from Players right join TeamPlayers on Players.PlayerID = TeamPlayers.PlayerID where TeamId = {currentTournament.WinnerTeam}", SqlCon))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            string to = reader["email"].ToString();
                            string from = "smurnament@gmail.com";
                            string subject = "Tournament Winner";
                            string body = $"Congratulations your team has won the {tournamentName} tournament. Your prize is {(double)prize/ (double)playersInTeam} lv ";
                            MailMessage message = new MailMessage(from, to, subject, body);
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                            smtp.Port = 587;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential("smurnament@gmail.com", "jzdawugaxrdojbzy");
                            smtp.EnableSsl = true;
                            smtp.Send(message);
                        }
                        catch(Exception ex) 
                        {
                            error = true;   
                        }
                        


                    }
                }
            }

            if(error == true)
            {
                MessageBox.Show("Some emails might not be correct");
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();

            string to = "martin.lukanov1000@gmail.com";
            string from = "martin.lukanov1000@gmail.com";
            string subject = "Using the new SMTP client.";
            string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("martin.lukanov1000@gmail.com", "spalxkzmjinffhvf");
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}
