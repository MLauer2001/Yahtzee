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
    public static class ScorecardManager
    {
        #region "CRUD"

        public async static Task<List<Scorecard>> Load()
        {
            using (var dc = new YahtzeeEntities())
            {
                List<Scorecard> scorecards = new List<Scorecard>();

                dc.tblScorecards.ToList().ForEach(row => scorecards.Add(new Scorecard()
                {
                    Id = row.Id,
                    UserId = row.UserId,
                    Aces = row.Aces,
                    Twos = row.Twos,
                    Threes = row.Threes,
                    Fours = row.Fours,
                    Fives = row.Fives,
                    Sixes = row.Sixes,
                    Bonus = row.Bonus,
                    ThreeOfKind = row.ThreeofKind,
                    FourOfKind = row.FourofKind,
                    FullHouse = row.FullHouse,
                    SmStraight = row.SmStraight,
                    LgStraight = row.LgStraight,
                    Yahtzee = row.Yahtzee,
                    Chance = row.Chance,
                    GrandTotal = row.GrandTotal
                    
                }));

                return scorecards;
            }
        }

        public async static Task<Scorecard> LoadById(Guid id)
        {
            using (var dc = new YahtzeeEntities())
            {
                tblScorecard row = dc.tblScorecards.FirstOrDefault(row => row.Id == id);

                Scorecard scorecard = new Scorecard()
                {
                    Id = row.Id,
                    UserId = row.UserId,
                    Aces = row.Aces,
                    Twos = row.Twos,
                    Threes = row.Threes,
                    Fours = row.Fours,
                    Fives = row.Fives,
                    Sixes = row.Sixes,
                    Bonus = row.Bonus,
                    ThreeOfKind = row.ThreeofKind,
                    FourOfKind = row.FourofKind,
                    FullHouse = row.FullHouse,
                    SmStraight = row.SmStraight,
                    LgStraight = row.LgStraight,
                    Yahtzee = row.Yahtzee,
                    Chance = row.Chance,
                    GrandTotal = row.GrandTotal
                };

                return scorecard;
            }
        }

        public async static Task<int> Insert(Scorecard scorecard, bool rollback = false)
        {
            try
            {
                using (var dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblScorecard newrow = new tblScorecard();
                    newrow.Id = scorecard.Id;
                    newrow.UserId = scorecard.UserId;
                    newrow.Aces = scorecard.Aces;
                    newrow.Twos = scorecard.Twos;
                    newrow.Threes = scorecard.Threes;
                    newrow.Fours = scorecard.Fours;
                    newrow.Fives = scorecard.Fives;
                    newrow.Sixes = scorecard.Sixes;
                    newrow.Bonus = scorecard.Bonus;
                    newrow.ThreeofKind = scorecard.ThreeOfKind;
                    newrow.FourofKind = scorecard.FourOfKind;
                    newrow.FullHouse = scorecard.FullHouse;
                    newrow.SmStraight = scorecard.SmStraight;
                    newrow.LgStraight = scorecard.LgStraight;
                    newrow.Yahtzee = scorecard.Yahtzee;
                    newrow.Chance = scorecard.Chance;
                    newrow.GrandTotal = scorecard.GrandTotal;

                    dc.tblScorecards.Add(newrow);
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

        public async static Task<int> Update(Scorecard scorecard, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblScorecard updaterow = dc.tblScorecards.FirstOrDefault(row => row.Id == scorecard.Id);

                    if (updaterow != null)
                    {
                        updaterow.Id = scorecard.Id;
                        updaterow.Id = scorecard.Id;
                        updaterow.UserId = scorecard.UserId;
                        updaterow.Aces = scorecard.Aces;
                        updaterow.Twos = scorecard.Twos;
                        updaterow.Threes = scorecard.Threes;
                        updaterow.Fours = scorecard.Fours;
                        updaterow.Fives = scorecard.Fives;
                        updaterow.Sixes = scorecard.Sixes;
                        updaterow.Bonus = scorecard.Bonus;
                        updaterow.ThreeofKind = scorecard.ThreeOfKind;
                        updaterow.FourofKind = scorecard.FourOfKind;
                        updaterow.FullHouse = scorecard.FullHouse;
                        updaterow.SmStraight = scorecard.SmStraight;
                        updaterow.LgStraight = scorecard.LgStraight;
                        updaterow.Yahtzee = scorecard.Yahtzee;
                        updaterow.Chance = scorecard.Chance;
                        updaterow.GrandTotal = scorecard.GrandTotal;

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

                    tblScorecard deleterow = dc.tblScorecards.FirstOrDefault(row => row.Id == id);

                    if (deleterow != null)
                    {
                        dc.tblScorecards.Remove(deleterow);
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

        public static Task<int> RollDice()
        {
            throw new NotImplementedException();
        }

        public static Task<int> HoldDice()
        {
            throw new NotImplementedException();
        }
        
        public static Task<int> PickMove()
        {
            throw new NotImplementedException();
        }
    }
}
