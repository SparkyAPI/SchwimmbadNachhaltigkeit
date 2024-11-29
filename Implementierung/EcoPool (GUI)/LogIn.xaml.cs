using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace SchwimmbadNachhaltigkeit
{
    public partial class Login : Window
    {

        Schwimmbad_Release.MySQL mySQL = new Schwimmbad_Release.MySQL();
        bool useSQL = true;

        public Login()
        {
            InitializeComponent();
            useSQL = mySQL.tryConnection();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Password;

            if (useSQL)
            {
                if (mySQL.AuthenticateUser(username, password))
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {

                }
            }
            else
            {
                if (username == "admin" && password == "12345") //Nur falls die Datenbank nicht funktioniert und man trotzdem rein möchte (LOCAL)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
        }



        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Benutzer erstellen Funktion noch nicht implementiert.", "Benutzer anlegen", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Passwort vergessen Funktion noch nicht implementiert.", "Passwort vergessen", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
