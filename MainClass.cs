/*
Made by Jeff Mehnert
Personal Project
March 2016
*/
using System;
using System.Collections.Generic;
using System.Threading;

namespace Blackjack
{
    class MainClass
    {
        public static Deck deck;
        public static List<Player> players;

        public static void Main()
        {
            Console.WriteLine("Welcome to Jeff's blackjack!");
            Console.WriteLine("How many decks would you like to play with?");
            int deckCount = Convert.ToInt32(Console.ReadLine()); //amount of decks (2 deck, 6 deck, etc)
            deck = new Deck(deckCount);
            if (deckCount < 1) //a little error handling
            {
                Console.WriteLine("Enter at least one deck");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("How many players?");
            int playerCount = Convert.ToInt32(Console.ReadLine()); //amount of players
            if (playerCount < 1) //more error handling
            {
                Console.WriteLine("Please, sit at least one player at the table");
                Console.ReadKey();
                return;
            }
            players = new List<Player>();
            for (int i = 0; i < playerCount; i++)//create players, give them names in the form of Player 1, Player 2, ...
            {
                Player player = new Player("Player " + (i + 1).ToString());
                players.Add(player);
            }
            Dealer dealer = new Dealer();

            foreach (Player p in players)
                p.addMoney();

            bool done = false;
            do
            {
                foreach (Player p in players) //each player places bets
                    p.placeBet();

                dealer.dealTable(); //deal players first card
                dealer.draw(drawCard()); //deal dealer first card
                dealer.dealTable(); //deal players second card
                dealer.draw(drawCard()); //deal dealer second card
                Console.WriteLine();

                foreach (Player p in players) //print out player hands
                {
                    p.getHand();
                    if (p.getHandVal() == 21)
                    {
                        Console.WriteLine("{0} has blackjack!", p.name);
                        p.blackjack = true;
                    }
                }
                Console.WriteLine();

                Card dealerTopCard = dealer.getTopCard(); //print out dealer top card              
                if (dealerTopCard.id == "A") //if dealer shows ace, offer insurance
                {
                    foreach (Player p in players)
                        p.insurance();
                }

                Console.WriteLine();

                if (dealer.getHandVal() == 21) //check if dealer has blackjack
                {
                    Console.WriteLine("Dealer has: ");
                    dealer.getHand();
                    Console.WriteLine("Dealer has blackjack!");
                    dealer.blackjack = true;
                    foreach (Player p in players)
                    {
                        if (p.insuranceBet > 0)
                        {
                            p.bank += p.insuranceBet * 2;
                            Console.WriteLine("{0} won their insurance bet");
                        }
                        else if (p.blackjack == false)
                            Console.WriteLine("Dealer wins over {0}", p.name);
                        else
                            Console.WriteLine("{0} pushes", p.name);

                    }
                }
                else //play the hand out if dealer doesn't have blackjack
                {
                    if (dealerTopCard.id == "A") //if he doesn't have blackjack while showing an A, let them know
                        Console.WriteLine("Dealer does not have blackjack");

                    foreach (Player p in players) //play hands
                        p.playHand();

                    dealer.playDealerHand();
                    if (dealer.busted == true) //if dealer busts
                    {
                        Console.WriteLine("Dealer busts!");
                        Console.WriteLine();
                        foreach (Player p in players)
                        {
                            if (p.busted == false)
                            {
                                Console.WriteLine("{0} wins!", p.name);
                                p.payout();
                            }


                            Console.WriteLine("{0} now has ${1} in their bank.", p.name, p.bank.ToString("F"));

                        }
                    }
                    else //if dealer doesn't bust
                    {
                        foreach (Player p in players)
                        {

                            if ((p.getHandVal() > dealer.getHandVal() && p.busted == false) || p.blackjack == true) //if player wins
                            {
                                Console.WriteLine("{0} wins!", p.name);
                                p.payout();
                            }

                            else if (p.getHandVal() == dealer.getHandVal() && p.busted == false) //if a push, they get their bet back
                            {
                                Console.WriteLine("{0} pushes", p.name);
                                p.bank += p.bet;
                            }

                            else //if dealer wins, player loses bet, which happens in emptyHand() function
                                Console.WriteLine("Dealer wins over {0}!", p.name);

                            Console.WriteLine("{0} now has ${1} in their bank.", p.name, p.bank.ToString("F"));
                        }
                    }

                }

                if (deck.cards.Count < (8 * players.Count)) //stops the game when the amount of cards left in the deck is less than 8 times the number of players
                {
                    Console.WriteLine("Deck is running low on cards, it only has {0} cards left. Exiting...", deck.cards.Count.ToString());
                    done = true;
                    break;
                }


                Console.WriteLine("You are playing with {0} decks, {1} cards left in the deck", deckCount, deck.cards.Count);
                Console.WriteLine("Would you like to play another hand? 1 for yes, 0 for no");
                bool succ = false;
                int input;
                do
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input == 0 || input == 1)
                        succ = true;
                } while (succ == false);

                if (input == 0) //done playing
                    done = true;

                foreach (Player p in players) //empty hands
                    p.emptyHand();

                dealer.emptyHand();
            } while (done == false);
            Console.WriteLine("Thanks for playing! Press any key to exit. Made by jeffymeh");
            Console.ReadKey();
        }

        public static Card drawCard() //removes a card off the last index of the deck and returns it
        {
            Card card = deck.cards[deck.cards.Count - 1];
            deck.cards.RemoveAt(deck.cards.Count - 1);
            return card;
        }
    }
}
