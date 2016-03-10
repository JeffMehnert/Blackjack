/*
Made by Jeff Mehnert
Personal Project
March 2016
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int deckCount = Convert.ToInt32(Console.ReadLine());
            deck = new Deck(deckCount);
            if (deckCount < 1)
            {
                Console.WriteLine("Enter at least one deck");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("How many players?");
            int playerCount = Convert.ToInt32(Console.ReadLine());
            players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                Player player = new Player("Player " + (i + 1).ToString());
                players.Add(player);
            }
            Dealer dealer = new Dealer();
            bool done = false;
            do
            {
                dealTable(); //deal players first card
                dealer.draw(drawCard()); //deal dealer first card
                dealTable(); //deal players second card
                dealer.draw(drawCard()); //deal dealer second card

                foreach (Player p in players) //print out player hands
                    p.getHand();

                Console.WriteLine();

                dealer.getTopCard(); //print out dealer top card
                Console.WriteLine();
                foreach (Player p in players) //play hands
                    playHand(p);

                playDealerHand(dealer);
                if (dealer.busted == true)
                {
                    Console.WriteLine("Dealer busts!");
                    Console.WriteLine();
                    foreach (Player p in players)
                    {
                        if (p.busted == false)
                            Console.WriteLine("{0} wins!", p.name);
                    }
                }
                else
                {
                    foreach (Player p in players)
                    {
                        if (p.getHandVal() > dealer.getHandVal() && p.busted == false)
                            Console.WriteLine("{0} wins!", p.name);
                        else if (p.getHandVal() == dealer.getHandVal() && p.busted == false)
                            Console.WriteLine("{0} pushes", p.name);
                        else
                            Console.WriteLine("Dealer wins over {0}!", p.name);
                    }
                }

                if (deck.cards.Count < (8*players.Count))
                {
                    Console.WriteLine("Deck is running low on cards, it only has {0} cards left. Exiting...", deck.cards.Count.ToString());
                    done = true;
                    break;
                }


                Console.WriteLine("You played with {0} decks, {1} cards left in the deck", deckCount, deck.cards.Count);
                Console.WriteLine("Would you like to play another hand? 1 for yes, 0 for no");
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 0)
                    done = true;

                foreach (Player p in players)
                    p.emptyHand();

                dealer.emptyHand();
            } while (done == false);
            Console.WriteLine("Thanks for playing! Press any key to exit. Made by jeffymeh");
            Console.ReadKey();
        }

        public static Card drawCard()
        {
            Card card = deck.cards[deck.cards.Count - 1];
            deck.cards.RemoveAt(deck.cards.Count - 1);
            return card;
        }

        public static void dealTable()
        {
            foreach (Player p in players)
            {
                p.hit(drawCard());
            }
        }

        public static void playHand(Player p)
        {
            p.getHand();
            int valOfHand = p.getHandVal();
            Console.WriteLine("Value of hand: {0}", valOfHand.ToString());
            bool stand = false;
            bool hasBusted = false;
            do
            {
                Console.WriteLine("1 for hit, 0 for stand");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    p.hit(drawCard());
                    Console.WriteLine("You receive a {0}", p.hand[p.hand.Count - 1].id);
                    Console.WriteLine("Value of hand: {0}", p.getHandVal().ToString());
                }
                else if (input == "0")
                {
                    stand = true;
                    Console.WriteLine("{0} stood at {1}", p.name, p.getHandVal().ToString());
                    Console.WriteLine();
                }


                hasBusted = bustedHand(p);
                if (hasBusted)
                {
                    Console.WriteLine("{0} has busted", p.name);
                    p.busted = true;
                }


            } while (stand == false && hasBusted == false);
        }

        public static bool bustedHand(Player p)
        {
            if (p.getHandVal() > 21)
                return true;
            else
                return false;
        }

        public static void playDealerHand(Dealer d)
        {
            Console.WriteLine("Dealer's hand: ");
            d.getHand();
            int dealerHandVal = d.getHandVal();
            Console.WriteLine("Dealer hand value: {0}", dealerHandVal.ToString());
            while (dealerHandVal < 17)
            {
                Card drawnCard = drawCard();
                Console.WriteLine("Dealer draws {0}", drawnCard.id);
                d.draw(drawnCard);
                dealerHandVal = d.getHandVal();
                Console.WriteLine("Dealer hand value: {0}", dealerHandVal.ToString());
            }
        }
    }
}
