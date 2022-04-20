using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yahtzee.BL.Models;
using Yahtzee.PL;

namespace Yahtzee.BL
{
    public static class LobbyManager
    {
        #region "CRUD"

        public async static Task<List<Lobby>> Load()
        {
            using (var dc = new YahtzeeEntities())
            {
                List<Lobby> lobbies = new List<Lobby>();

                dc.tblLobbies.ToList().ForEach(row => lobbies.Add(new Lobby()
                {
                    Id = row.Id,
                    LobbyName = row.LobbyName,
                    MaxPlayers = row.MaxPlayer
                }));

                return lobbies;
            }
        }

        public async static Task<int> Insert(Lobby lobby, bool rollback = false)
        {
            try
            {
                using (var dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblLobby newrow = new tblLobby();
                    newrow.Id = lobby.Id;
                    newrow.LobbyName = lobby.LobbyName;
                    newrow.MaxPlayer = lobby.MaxPlayers;

                    dc.tblLobbies.Add(newrow);
                    result = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async static Task<int> Update(Lobby lobby, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblLobby updaterow = dc.tblLobbies.FirstOrDefault(row => row.Id == lobby.Id);

                    if(updaterow != null)
                    {
                        updaterow.Id = lobby.Id;
                        updaterow.LobbyName = lobby.LobbyName;
                        updaterow.MaxPlayer = lobby.MaxPlayers;

                        result = dc.SaveChanges();

                    }
                    else
                    {
                        throw new Exception("Row could not be found!");
                    }

                    if (rollback) transaction.Rollback();

                    return result;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
