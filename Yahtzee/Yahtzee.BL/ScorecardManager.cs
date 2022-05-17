using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.BL.Models;
using Yahtzee.PL;

namespace Yahtzee.BL
{
    public static class ScorecardManager
    {
        #region "CRUD"

        public async static Task<List<Scorecard>> Load(Guid? userId = null)
        {
            List<Scorecard> rows = new List<Scorecard>();

            using (YahtzeeEntities dc = new YahtzeeEntities())
            {
                var tblScorecards = (from s in dc.tblScorecards
                                     join u in dc.tblUsers on s.UserId equals u.Id
                                     where u.Id == userId || userId == null
                                     orderby s.GrandTotal descending
                                     select new
                                     {
                                         ScorecardId = s.Id,
                                         s.UserId,
                                         u.Username,
                                         s.Aces,
                                         s.Twos,
                                         s.Threes,
                                         s.Fours,
                                         s.Fives,
                                         s.Sixes,
                                         s.Bonus,
                                         s.ThreeofKind,
                                         s.FourofKind,
                                         s.FullHouse,
                                         s.SmStraight,
                                         s.LgStraight,
                                         s.Chance,
                                         s.Yahtzee,
                                         s.GrandTotal
                                     }).Distinct().ToList();

                foreach (var s in tblScorecards)
                {
                    rows.Add(new Scorecard
                    {
                        Id = s.ScorecardId,
                        UserId = s.UserId,
                        Username = s.Username,
                        Aces = s.Aces,
                        Twos = s.Twos,
                        Threes = s.Threes,
                        Fours = s.Fours,
                        Fives = s.Fives,
                        Sixes = s.Sixes,
                        Bonus = s.Bonus,
                        ThreeOfKind = s.ThreeofKind,
                        FourOfKind = s.FourofKind,
                        SmStraight = s.SmStraight,
                        LgStraight = s.LgStraight,
                        Chance = s.Chance,
                        FullHouse = s.FullHouse,
                        Yahtzee = s.Yahtzee,
                        GrandTotal = s.GrandTotal
                    });
                }
                return rows;
            }
        }

        public async static Task<Scorecard> LoadById(Guid id)
        {
            using (YahtzeeEntities dc = new YahtzeeEntities())
            {
                var row = (from s in dc.tblScorecards
                           join u in dc.tblUsers on s.UserId equals u.Id
                           where s.Id == id
                           orderby s.GrandTotal descending
                           select new
                           {
                               ScorecardId = s.Id,
                               s.UserId,
                               u.Username,
                               s.Aces,
                               s.Twos,
                               s.Threes,
                               s.Fours,
                               s.Fives,
                               s.Sixes,
                               s.Bonus,
                               s.ThreeofKind,
                               s.FourofKind,
                               s.FullHouse,
                               s.SmStraight,
                               s.LgStraight,
                               s.Chance,
                               s.Yahtzee,
                               s.GrandTotal
                           }).FirstOrDefault();

                if (row != null)
                {
                    Scorecard scorecard = new Scorecard
                    {
                        Id = row.ScorecardId,
                        UserId = row.UserId,
                        Username = row.Username,
                        Aces = row.Aces,
                        Twos = row.Twos,
                        Threes = row.Threes,
                        Fours = row.Fours,
                        Fives = row.Fives,
                        Sixes = row.Sixes,
                        Bonus = row.Bonus,
                        ThreeOfKind = row.ThreeofKind,
                        FourOfKind = row.FourofKind,
                        SmStraight = row.SmStraight,
                        LgStraight = row.LgStraight,
                        Chance = row.Chance,
                        FullHouse = row.FullHouse,
                        Yahtzee = row.Yahtzee,
                        GrandTotal = row.GrandTotal
                    };
                    return scorecard;
                }
                else
                {
                    throw new Exception("Row was not found.");
                }
            }
        }

        public async static Task<Scorecard> LoadByUserId(Guid userId)
        {
            using (YahtzeeEntities dc = new YahtzeeEntities())
            {
                var row = (from s in dc.tblScorecards
                           join u in dc.tblUsers on s.UserId equals u.Id
                           where u.Id == userId
                           orderby s.GrandTotal descending
                           select new
                           {
                               ScorecardId = s.Id,
                               s.UserId,
                               u.Username,
                               s.Aces,
                               s.Twos,
                               s.Threes,
                               s.Fours,
                               s.Fives,
                               s.Sixes,
                               s.Bonus,
                               s.ThreeofKind,
                               s.FourofKind,
                               s.FullHouse,
                               s.SmStraight,
                               s.LgStraight,
                               s.Chance,
                               s.Yahtzee,
                               s.GrandTotal
                           }).FirstOrDefault();

                if (row != null)
                {
                    Scorecard scorecard = new Scorecard
                    {
                        Id = row.ScorecardId,
                        UserId = row.UserId,
                        Username = row.Username,
                        Aces = row.Aces,
                        Twos = row.Twos,
                        Threes = row.Threes,
                        Fours = row.Fours,
                        Fives = row.Fives,
                        Sixes = row.Sixes,
                        Bonus = row.Bonus,
                        ThreeOfKind = row.ThreeofKind,
                        FourOfKind = row.FourofKind,
                        SmStraight = row.SmStraight,
                        LgStraight = row.LgStraight,
                        Chance = row.Chance,
                        FullHouse = row.FullHouse,
                        Yahtzee = row.Yahtzee,
                        GrandTotal = row.GrandTotal
                    };
                    return scorecard;
                }
                else
                {
                    throw new Exception("Row was not found.");
                }
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


        public async static Task<int> UpdateByUserId(Guid userId, Scorecard scorecard, bool rollback = false)
        {
            try
            {
                using (YahtzeeEntities dc = new YahtzeeEntities())
                {
                    int result = 0;
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblScorecard updaterow = dc.tblScorecards.FirstOrDefault(row => row.UserId == userId);

                    if (updaterow != null)
                    {
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


        //public static Task<Scorecard> Turn(User user, Scorecard scorecard)
        //{
        //    int rollsLeft = 3;
        //    Die[] dice = new Die[5]; 
            
        //    while (rollsLeft > 1)
        //    {
        //        if(user == null)
        //        {
        //            //CPU logic here
        //        }
        //        else
        //        {
        //            //Player logic here
        //            RollDice(dice);
        //        }


        //        rollsLeft--;
        //    }
        //}

        public static Die[] RollDice(Die[] dice)
        {
            //Check for dice held
            //Roll remaining dice that arent held
            foreach (var die in dice)
            {
                if (die.Hold == false)
                {
                    die.Value = RandomNumberGenerator.GetInt32(1, 7);
                }
            }

            return dice;
        }

    }
}
