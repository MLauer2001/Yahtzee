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

            _connection.On<UserLobby>("SendTurn", (userlobby) => SendTurnToLobby(userlobby));

            _connection.StartAsync();
        }

        private void OnSendTurn(UserLobby userLobby)
        {
            Console.WriteLine("Sending user information to web...");
        }

        public void SendTurnToLobby(UserLobby userlobby)
        {
            try
            {
                Console.WriteLine("Sending scorecard...");

                _connection.InvokeAsync("SendTurn", userlobby);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ConnectToChannel(string user)
        {
            Start();

            string message = user + " Connected...";
            try
            {
                _connection.InvokeAsync("SendMessage", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
