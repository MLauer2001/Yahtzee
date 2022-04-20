﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.BL.Models;
using Yahtzee.PL;

namespace Yahtzee.BL
{
    public static class UserManager
    {
        #region "CRUD"

        public async static Task<List<User>> Load()
        {
            using (var dc = new YahtzeeEntities())
            {
                List<User> lobbies = new List<User>();

                dc.tblUsers.ToList().ForEach(row => lobbies.Add(new User()
                {
                    Id = row.Id,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Email = row.Email,
                    Username = row.Username,
                    Password = row.Password
                }));

                return lobbies;
            }
        }

        public async static Task<User> LoadById(Guid id)
        {
            using (var dc = new YahtzeeEntities())
            {
                tblUser row = dc.tblUsers.FirstOrDefault(row => row.Id == id);

                User user = new User()
                {
                    Id = row.Id,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Email = row.Email,
                    Username = row.Username,
                    Password = row.Password
                };

                return user;
            }
        }

        public async static Task<int> Insert(User user, bool rollback = false)
        {
            try
            {
                using (var dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser newrow = new tblUser();
                    newrow.Id = user.Id;
                    newrow.FirstName = user.FirstName;
                    newrow.LastName = user.LastName;
                    newrow.Email = user.Email;
                    newrow.Username = user.Username;
                    newrow.Password = user.Password;

                    dc.tblUsers.Add(newrow);
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


        public async static Task<int> Update(User user, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser updaterow = dc.tblUsers.FirstOrDefault(row => row.Id == user.Id);

                    if (updaterow != null)
                    {
                        updaterow.Id = user.Id;
                        updaterow.FirstName = user.FirstName;
                        updaterow.LastName = user.LastName;
                        updaterow.Email = user.Email;
                        updaterow.Username = user.Username;
                        updaterow.Password = user.Password;

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

                    tblUser deleterow = dc.tblUsers.FirstOrDefault(row => row.Id == id);

                    if (deleterow != null)
                    {
                        dc.tblUsers.Remove(deleterow);
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