using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schwimmbad_Release
{
    class MySQL
    {

        public const string ConnectionString = "Server=localhost;Database=deinedatenbank;User ID=root;Password=deinpasswort;";

        public bool tryConnection()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return userCount > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der Verbindung zur Datenbank: {ex.Message}", "Datenbankfehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
