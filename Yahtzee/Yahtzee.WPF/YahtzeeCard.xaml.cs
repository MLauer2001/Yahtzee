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

namespace Yahztee.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class YahtzeeCard : Window
    {
        int rollsLeft = 3;
        Die[] dice = new Die[5];
        int upperBonusCheck = 0;
        int upperSectionTotal = 0;
        int lowerSectionTotal = 0;
        int grandTotal = 0;

        public YahtzeeCard()
        {
            InitializeComponent();
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
            foreach(var die in dice)
            {
                die.Value = 6;
                die.Hold = false;
            }
        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
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

                if(rollsLeft < 3)
                {
                    //User adds to their score when roll/turn is up, disable button until this happens
                    //Enable buttons to update scorecard
                    UpdateScorecard();

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

            btnHold1.IsEnabled = true;
            btnHold2.IsEnabled = true;
            btnHold3.IsEnabled = true;
            btnHold4.IsEnabled = true;
            btnHold5.IsEnabled = true;
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
            //Show score for that turn
            return turnScore;
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
                btnHold1.Background = Brushes.Blue;
            }
        }

        private void btnHold2_Click(object sender, RoutedEventArgs e)
        {
            if(dice[1].Hold == false)
            {
                dice[1].Hold = true;
                btnHold2.Background = Brushes.Red;
            }
            else
            {
                dice[1].Hold = false;
                btnHold2.Background = Brushes.Blue;
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
                btnHold3.Background = Brushes.Blue;
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
                btnHold4.Background = Brushes.Blue;
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
                btnHold5.Background = Brushes.Blue;
            }
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            if (lblOne.Content == string.Empty)
            {
                lblOne.Content = UpdateUpperSection(1);
                ResetTurn();
            }
        }

        private void btnTwo_Click(object sender, RoutedEventArgs e)
        {
            if (lblTwo.Content == string.Empty)
            {
                lblTwo.Content = UpdateUpperSection(2);
                ResetTurn();
            }
        }
        private void btnThree_Click(object sender, RoutedEventArgs e)
        {
            if (lblThree.Content == string.Empty)
            {
                lblThree.Content = UpdateUpperSection(3);
                ResetTurn();
            }
        }
        private void btnFour_Click(object sender, RoutedEventArgs e)
        {
            if (lblFour.Content == string.Empty)
            {
                lblFour.Content = UpdateUpperSection(4);
                ResetTurn();
            }
        }

        private void btnFive_Click(object sender, RoutedEventArgs e)
        {
            if (lblFive.Content == string.Empty)
            {
                lblFive.Content = UpdateUpperSection(5);
                ResetTurn();
            }
        }

        private void btnSix_Click(object sender, RoutedEventArgs e)
        {
            if (lblSix.Content == string.Empty)
            {
                lblSix.Content = UpdateUpperSection(6);
                ResetTurn();
            }
        }
        #endregion

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

        private static bool OfKindCheck(int[] array, int num, int whatKind)
        {
            int[] numArray = new int[whatKind];
            for(int z = 0; z < whatKind; z++)
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

        private void btn3ofKind_Click(object sender, RoutedEventArgs e)
        {
            if(lbl3ofKind.Content == string.Empty)
            {
                int num = 1;
                int[] array = OrderDice(dice);
                int turnTotal = 0;
                while (num < 6)
                {
                    //3 of Kind check, hence passing in 3
                    if (OfKindCheck(array, num, 3))
                    {
                        turnTotal = num * 3;

                        lowerSectionTotal += turnTotal;
                        lbl3ofKind.Content = turnTotal.ToString();
                        ResetTurn();
                        break;
                    }
                    num++;
                }
                if(turnTotal == 0)
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
                while (num < 6)
                {
                    //4 of Kind check, hence passing in 4
                    if (OfKindCheck(array, num, 4))
                    {
                        turnTotal = num * 4;

                        lowerSectionTotal += turnTotal;
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

        }

        private void btnSmStraight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLgStraight_Click(object sender, RoutedEventArgs e)
        {

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

                        lowerSectionTotal += turnTotal;
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
            else if (lblYahtzee.Content == "50")
            {
                //Yahtzee BONUS logic here
            }
        }
        private void btnChance_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
