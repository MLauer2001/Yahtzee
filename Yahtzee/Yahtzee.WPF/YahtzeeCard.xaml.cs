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

        public YahtzeeCard()
        {
            InitializeComponent();
            dice[0] = new Die();
            dice[1] = new Die();
            dice[2] = new Die();
            dice[3] = new Die();
            dice[4] = new Die();
            foreach(var die in dice)
            {
                die.Value = 6;
                die.Hold = false;
            }
        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
        {
            //CALL TO WEB TO CHECK IF ITS MY TURN...
            
            if(rollsLeft > 0) 
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
                if(rollsLeft == 0)
                {
                    //User adds to their score when roll/turn is up, disable button until this happens
                    btnRollDice.IsEnabled = false;
                }
            }
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

        private void btnThree_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFive_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn3ofKind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFullHouse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
