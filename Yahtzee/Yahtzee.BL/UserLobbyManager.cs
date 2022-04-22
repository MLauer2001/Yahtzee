using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.BL.Models;
using Yahtzee.PL;

namespace Yahtzee.BL
{
    public static class UserLobbyManager
    {
        #region "CRUD"

        public async static Task<List<UserLobby>> Load()
        {
            using (var dc = new YahtzeeEntities())
            {
                List<UserLobby> userLobbies = new List<UserLobby>();

                dc.tblUserLobbies.ToList().ForEach(row => userLobbies.Add(new UserLobby()
                {
                    Id = row.Id,
                    UserId = row.UserId,
                    LobbyId = row.LobbyId,
                    ScorecardId = row.ScorecardId

                }));

                return userLobbies;
            }
        }

        public async static Task<UserLobby> LoadById(Guid id)
        {
            using (var dc = new YahtzeeEntities())
            {
                tblUserLobby row = dc.tblUserLobbies.FirstOrDefault(row => row.Id == id);

                UserLobby userLobby = new UserLobby()
                {
                    Id = row.Id,
                    UserId = row.UserId,
                    LobbyId = row.LobbyId,
                    ScorecardId = row.ScorecardId

                };

                return userLobby;
            }
        }

        public async static Task<int> Insert(UserLobby userLobby, bool rollback = false)
        {
            try
            {
                using (var dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserLobby newrow = new tblUserLobby();
                    newrow.Id = userLobby.Id;
                    newrow.UserId = userLobby.UserId;
                    newrow.LobbyId = userLobby.LobbyId;
                    newrow.ScorecardId = userLobby.ScorecardId;
                    

                    dc.tblUserLobbies.Add(newrow);
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

        public async static Task<int> Update(UserLobby userLobby, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserLobby updaterow = dc.tblUserLobbies.FirstOrDefault(row => row.Id == userLobby.Id);

                    if (updaterow != null)
                    {
                        updaterow.Id = userLobby.Id;
                        updaterow.UserId = userLobby.UserId;
                        updaterow.LobbyId = userLobby.LobbyId;
                        updaterow.ScorecardId = userLobby.ScorecardId;


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

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserLobby deleterow = dc.tblUserLobbies.FirstOrDefault(row => row.Id == id);

                    if (deleterow != null)
                    {
                        dc.tblUserLobbies.Remove(deleterow);
                        result = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
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

        public static Task<int> Game()
        {
            throw new NotImplementedException();
        }
    }
}
