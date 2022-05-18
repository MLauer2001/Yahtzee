using Microsoft.Extensions.Logging;
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
    /// Interaction logic for JoinLobby.xaml
    /// </summary>
    public partial class JoinLobby : Window
    {
        UserLobby userLobby = new UserLobby();
        List<Lobby> lobbies = new List<Lobby>();

        public JoinLobby(UserLobby userLobby)
        {
            InitializeComponent();
            this.userLobby = userLobby;
            lobbies = LobbyManager.Load().Result;


            lbxLobbies.ItemsSource = lobbies;
            lbxLobbies.DisplayMemberPath = "LobbyName";
            lbxLobbies.SelectedValuePath = "Id";
            lbxLobbies.SelectedIndex = 0;
        }

        private void btnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            userLobby.Id = Guid.NewGuid();
            userLobby.LobbyId = lobbies[lbxLobbies.SelectedIndex].Id;

            YahtzeeCard yahtzee = new YahtzeeCard(userLobby);
            yahtzee.Show();
            Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lobbies.Clear();
            lbxLobbies.ItemsSource = null;
            lobbies = LobbyManager.Load().Result;

            lbxLobbies.ItemsSource = lobbies;
            lbxLobbies.DisplayMemberPath = "LobbyName";
            lbxLobbies.SelectedValuePath = "Id";
            lbxLobbies.SelectedIndex = 0;

        }
    }
}
