using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.BL.Models;

namespace Yahtzee.WPF
{
    public class SignalRConnection
    {
        HubConnection _connection;

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://mryahtzeeapi.azurewebsites.net/GameHub")
                .Build();

            _connection.On<User, Scorecard>("RecieveTurn", (user, scorecard) => OnSendTurn(user, scorecard));

            _connection.StartAsync();
        }

        private void OnSendTurn(User user, Scorecard scorecard)
        {
            throw new NotImplementedException();
        }

        public void SendTurnToLobby(User user, Scorecard scorecard)
        {
            try
            {
                Console.WriteLine("Sending {user}'s scorecard", user.Username);

                _connection.InvokeAsync("RecieveTurn", user, scorecard);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
