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
    public static class ActivationManager
    {
        #region "CRUD"

        public async static Task<List<Activation>> Load()
        {
            using (var dc = new YahtzeeEntities())
            {
                List<Activation> activations = new List<Activation>();

                dc.tblActivations.ToList().ForEach(row => activations.Add(new Activation()
                {
                    Id = row.Id,
                    LobbyId = row.LobbyId,
                    KeyCode = row.KeyCode,
                    StartDate = row.StartDate,
                    EndDate = row.EndDate

                }));

                return activations;
            }
        }

        public async static Task<Activation> LoadById(Guid id)
        {
            using (var dc = new YahtzeeEntities())
            {
                tblActivation row = dc.tblActivations.FirstOrDefault(row => row.Id == id);

                Activation activation = new Activation()
                {
                    Id = row.Id,
                    LobbyId = row.LobbyId,
                    KeyCode = row.KeyCode,
                    StartDate = row.StartDate,
                    EndDate = row.EndDate
                };

                return activation;
            }
        }

        public async static Task<int> Insert(Activation activation, bool rollback = false)
        {
            try
            {
                using (var dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblActivation newrow = new tblActivation();

                    newrow.Id = activation.Id;
                    newrow.LobbyId = activation.LobbyId;
                    newrow.KeyCode = activation.KeyCode;
                    newrow.StartDate = activation.StartDate;
                    newrow.EndDate = activation.EndDate;
                    

                    dc.tblActivations.Add(newrow);
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

        public async static Task<int> Update(Activation activation, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblActivation updaterow = dc.tblActivations.FirstOrDefault(row => row.Id == activation.Id);

                    if (updaterow != null)
                    {
                        updaterow.Id = activation.Id;
                        updaterow.LobbyId = activation.LobbyId;
                        updaterow.KeyCode = activation.KeyCode;
                        updaterow.StartDate = activation.StartDate;
                        updaterow.EndDate = activation.EndDate;

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

                    tblActivation deleterow = dc.tblActivations.FirstOrDefault(row => row.Id == id);

                    if (deleterow != null)
                    {
                        dc.tblActivations.Remove(deleterow);
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
    }
}
