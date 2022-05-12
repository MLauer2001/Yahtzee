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
        User user = new User();
        List<Lobby> lobbies = new List<Lobby>();
        public JoinLobby(User user)
        {
            InitializeComponent();
            this.user = user;
            lobbies = LobbyManager.Load().Result;

            lbxLobbies.ItemsSource = lobbies;
            lbxLobbies.DisplayMemberPath = "LobbyName";
            lbxLobbies.SelectedValuePath = "Id";
            lbxLobbies.SelectedIndex = 0;
        }

        private void btnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby();
            lobby = lobbies[lbxLobbies.SelectedIndex];

            YahtzeeCard yahtzee = new YahtzeeCard(user, lobby);
            yahtzee.Show();
            Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lobbies.Clear();
            lbxLobbies.ItemsSource = null;
            lobbies = LobbyManager.Load().Result;

            lbxLobbies.DisplayMemberPath = "LobbyName";
            lbxLobbies.SelectedValuePath = "Id";
            lbxLobbies.SelectedIndex = 0;

        }
    }
}
