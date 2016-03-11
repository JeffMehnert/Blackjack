/*
Made by Jeff Mehnert
Personal Project
March 2016
*/
using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Player
    {
        public string name;
        public List<Card> hand; //player's hand
        public bool busted;
        public bool blackjack;
        public double bank; //total amount of money player has at table
        public double bet; //amount player bets on current hand
        public double insuranceBet; //amount player bets on insurance if applicable

        public Player(string name) //constructor
        {
            this.name = name;
            hand = new List<Card>();
            busted = false;
            blackjack = false;
            bank = 0;
            bet = 0;
            insuranceBet = 0;
        }

        public void addMoney()
        {
            Console.WriteLine("{0}, how much money would you like to play with?", name);
            double money = Convert.ToDouble(Console.ReadLine());
            bank += money;
            Console.WriteLine();
        }


        public void placeBet() //place bets. makes the user enter a valid bet
        {
            bool success = false;
            Console.WriteLine("{0}, please place your bet. You have ${1} left in your bank", name, bank.ToString("F"));
            do{
                int bet = Convert.ToInt32(Console.ReadLine());
                if (bet <= 0)
                    Console.WriteLine("Enter a bet greater than 0");
                else if (bank < bet)
                    Console.WriteLine("Bet exceeds amount in the bank. Enter a smaller bet");
                else
                {
                    this.bet = bet;
                    bank -= bet;
                    success = true;
                }
                    
            }while(success == false);
            Console.WriteLine();
        }

        public void payout() //pays player on a win
        {
            const double THREEHALVES = 3 / 2;
            if (blackjack == true)      //if player won with blackjack, pay 3 to 2
                bank += bet * THREEHALVES;
            else                        //if not, pay 2 to 1
                bank += bet * 2;
        }

        public void insurance() //when dealer shows an A, player has option to take insurance
        {
            Console.WriteLine("Dealer is showing an A. {0}, would you like to buy insurance? 1 for yes, 0 for no.");
            bool success = false;
            int input;
            do
            {
                input = Convert.ToInt32(Console.ReadLine());
                if (input == 0 || input == 1)
                    success = true;
            } while (success == false);
            
            if (input == 1) //if they want to take insurance
            {
                Console.WriteLine("How much would you like to bet for insurance? Bet can be up to half the amount of your original bet.");
                bool succ = false;
                do
                {
                    insuranceBet = Convert.ToDouble(Console.ReadLine());
                    if (insuranceBet <= 0) //if bet not greater than 0
                        Console.WriteLine("Enter a bet greater than 0");
                    else if (insuranceBet > (bet / 2)) //if bet larger than half the original bet
                        Console.WriteLine("Insurance bet too large. Enter a bet at most half your original bet.");
                    else //if bet was good, break from the loop
                        succ = true;
                } while (succ == false);

                Console.WriteLine("Insurance bet placed for {0}", name);
                bank -= insuranceBet;
            }
            else //if they don't want insurance
                return;
        }

        public void hit(Card c) //hit me! add one card to player's hand
        {
            hand.Add(c);
        }

        public void emptyHand() //prepares hands for the next round
        {
            hand.Clear();
            busted = false;
            blackjack = false;
            bet = 0;
            insuranceBet = 0;
        }

        public void getHand() //prints hand's contents
        {
            Console.WriteLine("{0} has:", name);
            foreach (Card c in hand)
                Console.WriteLine("{0} of {1}", c.id, c.suit);

            Console.WriteLine();
        }

        public int getHandVal() //returns value of the cards in hand
        {
            int val = 0;
            int aceCount = 0;
            foreach (Card c in hand)
            {
                val += c.value;
                if (c.id == "A")
                    aceCount++;
            }

            if (val > 21 && aceCount >= 1) //if value is over 21 and we've seen aces, we can subtract 10 for each ace we've seen, as aces represent 1 or 11
            {
                for (int i = 0; i < aceCount; i++)
                    val -= 10;
            }
            return val;
        }
    }
}
