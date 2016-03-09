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
                Player player = new Player("Player " + (i+1).ToString());
                players.Add(player);
            }
            Dealer dealer = new Dealer();

            dealTable(); //deal players first card
            dealer.addCard(drawCard()); //deal dealer first card
            dealTable(); //deal players second card
            dealer.addCard(drawCard()); //deal dealer second card

            foreach (Player p in players) //print out player hands
                p.getHand();

            Console.WriteLine();

            dealer.getTopCard(); //print out dealer top card
            Console.WriteLine();
            foreach (Player p in players) //play hands
                playHand(p);

            playDealerHand();
            Console.WriteLine("You played with {0} decks, {1} cards left in the deck", deckCount, deck.cards.Count);
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
            int valOfHand = p.valueOfHand();
            Console.WriteLine("Value of hand: {0}", valOfHand.ToString());
            bool stand = false;
            bool hasBusted = false;
            do
            {
                Console.WriteLine("1 for hit, 0 for stand");
                string input = Console.ReadLine();
                if(input == "1")
                {
                    p.hit(drawCard());
                }
                else if(input == "0")
                    stand = true;

                hasBusted = bustedHand(p);
                if (hasBusted)
                    Console.WriteLine("{0} has busted", p.name);
                
            } while (stand == false && hasBusted == false);
        }

        public static bool bustedHand(Player p)
        {
            if (p.valueOfHand() > 21)
                return true;
            else
                return false;
        }

        public static void playDealerHand()
        {

        }
    }
}
