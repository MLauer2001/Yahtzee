using System;
using System.Collections.Generic;
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
using Yahtzee.BL;
using Yahtzee.BL.Models;
using Yahztee.WPF;

namespace Yahtzee.WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;

            UserLobby userLobby = new UserLobby();

            try
            {
                UserManager.Login(user.Username, user.Password);
                user = UserManager.LoadByUsername(user.Username).Result;
                userLobby.UserId = user.Id;

                JoinLobby join = new JoinLobby(userLobby);
                join.Show();
                Close();
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;
            }
            

        }
    }
}
