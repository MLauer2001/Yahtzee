using Castle.Core.Logging;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yahtzee.BL;
using Yahtzee.BL.Models;
using Yahtzee.WPF;

namespace Yahztee.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class YahtzeeCard : Window
    {
        int rollsLeft = 3;
        int turnsLeft = 13;
        Die[] dice = new Die[5];
        int upperBonusCheck = 0;
        int upperSectionTotal = 0;
        int lowerSectionTotal = 0;
        int grandTotal = 0;
        HubConnection _connection;

        MySettings mySettings;
        string APIAddress = "https://mryahtzeeapi.azurewebsites.net/GameHub";

        private int LowerSectionTotal
        {
            get { return lowerSectionTotal; }
            set 
            { 
                lowerSectionTotal = value;
                PropertyChanged();
            }
        }

        private void PropertyChanged()
        {
            lblLowerSection.Content = LowerSectionTotal.ToString();
        }

        UserLobby userLobby = new UserLobby();
        User user = new User();
        Lobby lobby = new Lobby();
        Scorecard scorecard = new Scorecard();
        SignalRConnection signalR = new SignalRConnection();


        private readonly ILogger<User> _logger;

       
        public YahtzeeCard(UserLobby userLobby)
        {
            InitializeComponent();
            this.userLobby = userLobby;
            this.user = UserManager.LoadById(userLobby.UserId).Result;
            this.lobby = LobbyManager.LoadById(userLobby.LobbyId).Result;

            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.File("logs\\Log_UserTurn.txt", rollingInterval: RollingInterval.Day).CreateLogger();

            if (userLobby.ScorecardId != Guid.Empty)
            {
                this.scorecard = ScorecardManager.LoadById(userLobby.ScorecardId).Result;
                userLobby.ScorecardId = scorecard.Id;
                scorecard.UserId = user.Id;
                scorecard.Username = user.Username;
            }
            else
            {
                scorecard.Id = Guid.NewGuid();
                userLobby.ScorecardId = scorecard.Id;
                scorecard.UserId = user.Id;
                scorecard.Username = user.Username;
                ScorecardManager.Insert(scorecard);
            }
                
            lblUsername.Content = user.Username + "'s Card";
            signalR.Start();


            dice[0] = new Die();
            dice[1] = new Die();
            dice[2] = new Die();
            dice[3] = new Die();
            dice[4] = new Die();
            btnHold1.IsEnabled = false;
            btnHold2.IsEnabled = false;
            btnHold3.IsEnabled = false;
            btnHold4.IsEnabled = false;
            btnHold5.IsEnabled = false;
            foreach (var die in dice)
            {
                die.Value = 6;
                die.Hold = false;
            }
        }

        private async void RollDice_Click(object sender, RoutedEventArgs e)
        {
            //CALL TO WEB TO CHECK IF ITS MY TURN...

            btnHold1.IsEnabled = true;
            btnHold2.IsEnabled = true;
            btnHold3.IsEnabled = true;
            btnHold4.IsEnabled = true;
            btnHold5.IsEnabled = true;

            if (rollsLeft > 0)
            {
                //Check for Dice held and roll remaining -> return die array
                dice = ScorecardManager.RollDice(dice);

                //Change die images to dice values

                Die1.Source = new BitmapImage(new Uri(String.Format(@"pack://application:,,,/DiceImg/{0}.png", dice[0].Value)));
                Die2.Source = new BitmapImage(new Uri(String.Format(@"pack://application:,,,/DiceImg/{0}.png", dice[1].Value)));
                Die3.Source = new BitmapImage(new Uri(String.Format(@"pack://application:,,,/DiceImg/{0}.png", dice[2].Value)));
                Die4.Source = new BitmapImage(new Uri(String.Format(@"pack://application:,,,/DiceImg/{0}.png", dice[3].Value)));
                Die5.Source = new BitmapImage(new Uri(String.Format(@"pack://application:,,,/DiceImg/{0}.png", dice[4].Value)));

                rollsLeft--;
                lblRollsLeft.Content = rollsLeft.ToString();

                //_logger.LogInformation("{Username} has {rolls} rolls left", user.Username, rollsLeft);
                

                if (rollsLeft < 3)
                {
                    //User adds to their score when roll/turn is up, disable button until this happens
                    //Enable buttons to update scorecard
                    UpdateScorecard();

                }
                if (lblYahtzee.Content == "50")
                {
                    //User already has yahtzee...
                    btnYahtzeeBonus.IsEnabled = true;
                }

                if (turnsLeft == 0)
                {
                    grandTotal = upperSectionTotal + LowerSectionTotal;
                    scorecard.GrandTotal = grandTotal;
                    lblTotal.Content = grandTotal.ToString();
                    try
                    {
                        ScorecardManager.Update(scorecard);
                        Log.Information(user.Username + " has finished their game with a total score of " + scorecard.GrandTotal);
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   
                }
            }
        }

        private void UpdateScorecard()
        {
            if (rollsLeft == 0)
            {
                btnRollDice.IsEnabled = false;
                btnHold1.IsEnabled = false;
                btnHold2.IsEnabled = false;
                btnHold3.IsEnabled = false;
                btnHold4.IsEnabled = false;
                btnHold5.IsEnabled = false;

                Log.Information("{Username} updated their scorecard", user.Username);
            }

            btnOne.IsEnabled = true;
            btnTwo.IsEnabled = true;
            btnThree.IsEnabled = true;
            btnFour.IsEnabled = true;
            btnFive.IsEnabled = true;
            btnSix.IsEnabled = true;
            btn3ofKind.IsEnabled = true;
            btn4ofKind.IsEnabled = true;
            btnFullHouse.IsEnabled = true;
            btnLgStraight.IsEnabled = true;
            btnSmStraight.IsEnabled = true;
            btnYahtzee.IsEnabled = true;
            btnChance.IsEnabled = true;
            btnYahtzeeBonus.IsEnabled = true;
        }

        private void ResetTurn()
        {
            //Disable the buttons, rolls back at 3
            rollsLeft = 3;
            turnsLeft--;

            scorecard.GrandTotal = grandTotal;
            ScorecardManager.Update(scorecard);
            signalR.SendTurnToLobby(userLobby);


            btnHold1.IsEnabled = true;
            btnHold2.IsEnabled = true;
            btnHold3.IsEnabled = true;
            btnHold4.IsEnabled = true;
            btnHold5.IsEnabled = true;
            foreach(var die in dice)
                die.Hold = false;

            Brush brush = new SolidColorBrush(Color.FromRgb(59, 64, 184));
            btnHold1.Background = brush;
            btnHold2.Background = brush;
            btnHold3.Background = brush;
            btnHold4.Background = brush;
            btnHold5.Background = brush;

            btnRollDice.IsEnabled = true;

            btnOne.IsEnabled = false;
            btnTwo.IsEnabled = false;
            btnThree.IsEnabled = false;
            btnFour.IsEnabled = false;
            btnFive.IsEnabled = false;
            btnSix.IsEnabled = false;
            btn3ofKind.IsEnabled = false;
            btn4ofKind.IsEnabled = false;
            btnFullHouse.IsEnabled = false;
            btnLgStraight.IsEnabled = false;
            btnSmStraight.IsEnabled = false;
            btnYahtzee.IsEnabled = false;
            btnChance.IsEnabled = false;
            btnYahtzeeBonus.IsEnabled = false;

            lblUpperSection.Content = upperSectionTotal.ToString();
        }

        private static int[] OrderDice(Die[] dice)
        {
            int[] array = new int[dice.Length];
            for (int i = 0; i < dice.Length; i++)
            {
                array[i] = dice[i].Value;
            }

            int[] array2 = array.OrderBy(i => i).ToArray();

            return array2;
        }

        #region UPPERSECTION
        private int UpdateUpperSection(int value)
        {
            int turnScore = 0;
            foreach (var die in dice)
            {
                if (die.Value == value)
                {
                    turnScore += value;
                }
            }

            //Update UppersectionTotal
            upperSectionTotal += turnScore;


            switch (value)
            {
                case 1:
                    scorecard.Aces = turnScore;
                    break;
                case 2:
                    scorecard.Twos = turnScore;
                    break;
                case 3:
                    scorecard.Threes = turnScore;
                    break;
                case 4:
                    scorecard.Fours = turnScore;
                    break;
                case 5:
                    scorecard.Fives = turnScore;
                    break;
                case 6:
                    scorecard.Sixes = turnScore;
                    break;
                default:
                    break;
            }
            //Show score for that turn
            return turnScore;
        }

        private bool UpperBonusCheck()
        {
            if (lblOne.Content != "" && lblTwo.Content != "" &&
               lblThree.Content != "" && lblFour.Content != "" &&
               lblFive.Content != "" && lblSix.Content != "" && upperBonusCheck == 0)
            {
                //Check for upperSection Bonus
                if (upperSectionTotal >= 63)
                {
                    upperSectionTotal += 35;
                    scorecard.Bonus = 35;
                    lblBonus.Content = "35";
                    lblUpperSection.Content = upperSectionTotal.ToString();
                    return true;
                }
                else
                {
                    scorecard.Bonus = 0;
                    lblBonus.Content = "0";
                    return false;
                }
            }
            else
                return false;
        }


        private void btnHold1_Click(object sender, RoutedEventArgs e)
        {
            if (dice[0].Hold == false)
            {
                dice[0].Hold = true;
                btnHold1.Background = Brushes.Red;
            }
            else
            {
                dice[0].Hold = false;
                Brush brush = new SolidColorBrush(Color.FromRgb(59, 64, 184));
                btnHold1.Background = brush;
            }
        }

        private void btnHold2_Click(object sender, RoutedEventArgs e)
        {
            if (dice[1].Hold == false)
            {
                dice[1].Hold = true;
                btnHold2.Background = Brushes.Red;
            }
            else
            {
                dice[1].Hold = false; 
                Brush brush = new SolidColorBrush(Color.FromRgb(59, 64, 184));
                btnHold2.Background = brush;
            }
        }

        private void btnHold3_Click(object sender, RoutedEventArgs e)
        {
            if (dice[2].Hold == false)
            {
                dice[2].Hold = true;
                btnHold3.Background = Brushes.Red;
            }
            else
            {
                dice[2].Hold = false;
                Brush brush = new SolidColorBrush(Color.FromRgb(59, 64, 184));
                btnHold3.Background = brush;
            }
        }

        private void btnHold4_Click(object sender, RoutedEventArgs e)
        {
            if (dice[3].Hold == false)
            {
                dice[3].Hold = true;
                btnHold4.Background = Brushes.Red;
            }
            else
            {
                dice[3].Hold = false;
                Brush brush = new SolidColorBrush(Color.FromRgb(59, 64, 184));
                btnHold4.Background = brush;
            }
        }

        private void btnHold5_Click(object sender, RoutedEventArgs e)
        {
            if (dice[4].Hold == false)
            {
                dice[4].Hold = true;
                btnHold5.Background = Brushes.Red;
            }
            else
            {
                dice[4].Hold = false;
                Brush brush = new SolidColorBrush(Color.FromRgb(59, 64, 184));
                btnHold5.Background = brush;
            }
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            if (lblOne.Content == string.Empty)
            {
                lblOne.Content = UpdateUpperSection(1);
                ResetTurn();
                UpperBonusCheck();
                
            }
        }

        private void btnTwo_Click(object sender, RoutedEventArgs e)
        {
            if (lblTwo.Content == string.Empty)
            {
                lblTwo.Content = UpdateUpperSection(2);
                ResetTurn();
                UpperBonusCheck();
                
            }
        }
        private void btnThree_Click(object sender, RoutedEventArgs e)
        {
            if (lblThree.Content == string.Empty)
            {
                lblThree.Content = UpdateUpperSection(3);
                ResetTurn();
                UpperBonusCheck();
                
            }
        }
        private void btnFour_Click(object sender, RoutedEventArgs e)
        {
            if (lblFour.Content == string.Empty)
            {
                lblFour.Content = UpdateUpperSection(4);
                ResetTurn();
                UpperBonusCheck();
                
            }
        }

        private void btnFive_Click(object sender, RoutedEventArgs e)
        {
            if (lblFive.Content == string.Empty)
            {
                lblFive.Content = UpdateUpperSection(5);
                ResetTurn();
                UpperBonusCheck();
                
            }
        }

        private void btnSix_Click(object sender, RoutedEventArgs e)
        {
            if (lblSix.Content == string.Empty)
            {
                lblSix.Content = UpdateUpperSection(6);
                ResetTurn();
                UpperBonusCheck();
                
            }
        }
        #endregion


        #region LOWERSECTION
        private static bool OfKindCheck(int[] array, int num, int whatKind)
        {
            int[] numArray = new int[whatKind];
            for (int z = 0; z < whatKind; z++)
            {
                numArray[z] = num;
            }

            int n = array.Length;
            int m = numArray.Length;
            // Two pointers to traverse the arrays
            int i = 0, j = 0;

            // Traverse both arrays simultaneously
            while (i < n && j < m)
            {

                // If element matches
                // increment both pointers
                if (array[i] == numArray[j])
                {

                    i++;
                    j++;

                    // If array B is completely
                    // traversed
                    if (j == m)
                        return true;
                }

                // If not,
                // increment i and reset j
                else
                {
                    i = i - j + 1;
                    j = 0;
                }
            }
            return false;
        }

        private static bool StraightCheck(int[] array, int whatKind)
        {
            int[] checkArray = new int[whatKind];
            int num = 1;
            for (int z = 0; z < whatKind; z++)
            {
                checkArray[z] = num;
                num++;
            }

            //Must remove duplicates in array
            array = array.Distinct().ToArray();

            //If its a small straight, only compare first four numbers in array
            if (whatKind == 4)
            {
                //Check if array is 1,2,3,4 || 2,3,4,5 || 3,4,5,6
                

                if (array[0] == 1)
                {
                    checkArray[0] = 1;
                    checkArray[1] = 2;
                    checkArray[2] = 3;
                    checkArray[3] = 4;

                    if (Enumerable.SequenceEqual(checkArray, array))
                        return true;
                    else
                        return false;
                }
                else if (array[0] == 2)
                {
                    checkArray[0] = 2;
                    checkArray[1] = 3;
                    checkArray[2] = 4;
                    checkArray[3] = 5;

                    if (Enumerable.SequenceEqual(checkArray, array))
                        return true;
                    else
                        return false;
                }
                else if (array[0] == 3)
                {
                    checkArray[0] = 3;
                    checkArray[1] = 4;
                    checkArray[2] = 5;
                    checkArray[3] = 6;

                    if (Enumerable.SequenceEqual(checkArray, array))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            {
                //Large straight

                //Check if Lg Straight is 1,2,3,4,5 || 2,3,4,5,6

                if (array[0] == 1)
                {
                    checkArray[0] = 1;
                    checkArray[1] = 2;
                    checkArray[2] = 3;
                    checkArray[3] = 4;
                    checkArray[4] = 5;

                    if (Enumerable.SequenceEqual(checkArray, array))
                        return true;
                    else
                        return false;
                }
                else
                {
                    checkArray[0] = 2;
                    checkArray[1] = 3;
                    checkArray[2] = 4;
                    checkArray[3] = 5;
                    checkArray[4] = 6;

                    if (Enumerable.SequenceEqual(checkArray, array))
                        return true;
                    else
                        return false;
                }
            }
        }
        private void btn3ofKind_Click(object sender, RoutedEventArgs e)
        {
            if (lbl3ofKind.Content == string.Empty)
            {
                int num = 1;
                int[] array = OrderDice(dice);
                int turnTotal = 0;
                while (num < 7)
                {
                    //3 of Kind check, hence passing in 3
                    if (OfKindCheck(array, num, 3))
                    {
                        turnTotal = num * 3;

                        LowerSectionTotal += turnTotal;
                        scorecard.ThreeOfKind = turnTotal;
                        lbl3ofKind.Content = turnTotal.ToString();
                        ResetTurn();
                        
                        break;
                    }
                    num++;
                }
                if (turnTotal == 0)
                {
                    lbl3ofKind.Content = "0";
                    ResetTurn();
                    
                }

            }
        }

        private void btn4ofKind_Click(object sender, RoutedEventArgs e)
        {
            if (lblFourofKind.Content == string.Empty)
            {
                int num = 1;
                int[] array = OrderDice(dice);
                int turnTotal = 0;
                while (num < 7)
                {
                    //4 of Kind check, hence passing in 4
                    if (OfKindCheck(array, num, 4))
                    {
                        turnTotal = num * 4;

                        LowerSectionTotal += turnTotal;
                        scorecard.FourOfKind = turnTotal;
                        lblFourofKind.Content = turnTotal.ToString();
                        ResetTurn();
                        break;
                    }
                    num++;
                }
                if (turnTotal == 0)
                {
                    lblFourofKind.Content = "0";
                    ResetTurn();
                }

            }
        }

        private void btnFullHouse_Click(object sender, RoutedEventArgs e)
        {
            //Check for two of a kind and 3 of a kind...
            if (lblFullHouse.Content == string.Empty)
            {
                int num = 1;
                int num2 = 1;
                int[] array = OrderDice(dice);
                int turnTotal = 0;
                while (num < 7)
                {
                    //2 of kind check, hence passing in 2
                    if (OfKindCheck(array, num, 2))
                    {
                        //If num2 is not num for 2 of kind, then check for 3 of kind
                        while (num2 < 7)
                        {
                            if (num2 != num)
                            {
                                if (OfKindCheck(array, num2, 3))
                                {
                                    turnTotal = (num * 2) + (num2 * 3);

                                    LowerSectionTotal += turnTotal;
                                    scorecard.FullHouse = turnTotal;
                                    lblFullHouse.Content = turnTotal.ToString();
                                    ResetTurn();
                                    break;
                                }
                            }
                            num2++;
                        }
                    }
                    num++;
                }
            }
        }
        private void btnSmStraight_Click(object sender, RoutedEventArgs e)
        {
            if (lblSmStraight.Content == string.Empty)
            {
                int[] array = OrderDice(dice);
                int turnTotal = 0;

                if (StraightCheck(array, 4))
                {
                    array = array.Distinct().ToArray();

                    for (int i = 0; i < array.Length; i++)
                    {
                        turnTotal += array[i];
                    }
                    LowerSectionTotal += turnTotal;
                    scorecard.SmStraight = turnTotal;
                    lblSmStraight.Content = turnTotal.ToString();
                    ResetTurn();
                    
                }
                else
                {
                    lblSmStraight.Content = "0";
                    ResetTurn();
                    
                }
            }
        }
        private void btnLgStraight_Click(object sender, RoutedEventArgs e)
        {
            if (lblLgStraight.Content == string.Empty)
            {
                int[] array = OrderDice(dice);
                int turnTotal = 0;

                if (StraightCheck(array, 5))
                {
                    array = array.Distinct().ToArray();

                    for (int i = 0; i < array.Length; i++)
                    {
                        turnTotal += array[i];
                    }
                    LowerSectionTotal += turnTotal;
                    scorecard.LgStraight = turnTotal;
                    lblLgStraight.Content = turnTotal.ToString();
                    ResetTurn();
                    
                }
                else
                {
                    lblLgStraight.Content = "0";
                    ResetTurn();
                    
                }
            }
        }

        private void btnYahtzee_Click(object sender, RoutedEventArgs e)
        {
            if (lblYahtzee.Content == string.Empty)
            {
                int num = 1;
                int[] array = OrderDice(dice);
                int turnTotal = 0;
                while (num < 6)
                {
                    //Yahtzee check, hence passing in 5
                    if (OfKindCheck(array, num, 5))
                    {
                        turnTotal = 50;

                        LowerSectionTotal += turnTotal;
                        scorecard.Yahtzee = turnTotal;
                        lblYahtzee.Content = turnTotal.ToString();
                        ResetTurn();
                        
                        break;
                    }
                    num++;
                }
                if (turnTotal == 0)
                {
                    lblYahtzee.Content = "0";
                    ResetTurn();
                    
                }

            }
        }
        private void btnChance_Click(object sender, RoutedEventArgs e)
        {
            if (lblChance.Content == string.Empty)
            {
                int[] array = OrderDice(dice);
                int turnTotal = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    turnTotal += array[i];
                }

                LowerSectionTotal += turnTotal;
                scorecard.Chance = turnTotal;
                lblChance.Content = turnTotal.ToString();
                ResetTurn();
                
            }
        }

        private void btnYahtzeeBonus_Click(object sender, RoutedEventArgs e)
        {
            if (lblYahtzee.Content == string.Empty)
            {
                int num = 1;
                int[] array = OrderDice(dice);
                int turnTotal = 0;
                while (num < 6)
                {
                    //Yahtzee check, hence passing in 5
                    if (OfKindCheck(array, num, 5))
                    {
                        turnTotal = 50;

                        LowerSectionTotal += turnTotal;
                        lblYahtzeeBonus.Content = turnTotal.ToString();
                        ResetTurn();
                        
                        break;
                    }
                    num++;
                }
                if (turnTotal == 0)
                {
                    lblYahtzee.Content = "0";
                    ResetTurn();
                    
                }
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mySettings = App.Configuration.GetSection("MySettings").Get<MySettings>();
            APIAddress = "https://mryahtzeeapi.azurewebsites.net/GameHub";
            ConnectToChannel();

        }

        void ConnectToChannel()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://mryahtzeeapi.azurewebsites.net/GameHub")
                .Build();

            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));

            _connection.StartAsync();

            string message = DateTime.Now.ToString() + ": Connected...";
            this.Title = "Connected...";

            try
            {
                _connection.InvokeAsync("SendMessage", "UI", message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void SendMessageToChannel(string message)
        {
            try
            {
                _connection.InvokeAsync("SendMessage", _connection.ConnectionId, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnSend(string name, string message)
        {
            this.Title = message;
            int number;

            if(int.TryParse(message, out number))
            {
                this.Title = "Testing";
                SendMessageToChannel("Test!");
            }
        }
    }
}
