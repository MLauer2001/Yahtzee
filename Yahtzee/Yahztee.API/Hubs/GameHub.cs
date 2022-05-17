using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Yahtzee.BL;
using Yahtzee.BL.Models;

namespace Yahztee.API.Hubs
{
    public class GameHub : Hub
    {
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(string user, string score, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, score, message);
        }

        public Task SendMessageToGroup(string group, string user, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", user, message);
        }

        public Task SendTurnToGroup(string group, string user, string score)
        {
            return Clients.Group(group).SendAsync("ReceiveTurn", user, score);
        }

        public async Task SendTurn(UserLobby userLobby, string group)
        {
            User user = await UserManager.LoadById(userLobby.UserId);
            Lobby lobby = await LobbyManager.LoadById(userLobby.LobbyId);
            Scorecard scorecard = await ScorecardManager.LoadById(userLobby.ScorecardId);

            await Clients.Group(group).SendAsync("ReceiveTurn", user.Username + " has sent their turn", user, lobby, scorecard);
        }
    }
}
