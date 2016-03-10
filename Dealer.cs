using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Dealer
    {
        public List<Card> hand;
        public bool busted;

        public Dealer()
        {
            hand = new List<Card>();
            busted = false;
        }

        public void draw(Card c)
        {
            hand.Add(c);
        }

        public void getTopCard()
        {
            Console.WriteLine("Dealer shows:");
            Console.WriteLine("{0} of {1}",hand[hand.Count - 1].id, hand[hand.Count - 1].suit);
        }

        public void getHand()
        {
            foreach (Card c in hand)
                Console.WriteLine(c.id);
        }

        public int getHandVal()
        {
            int val = 0;
            int aceCount = 0;
            foreach (Card c in hand)
            {
                val += c.value;
                if(c.id == "A")
                    aceCount++;
            }

            for (int i = 0; i < aceCount; i++)
                val -= 10;

            return val;
        }
    }
}
