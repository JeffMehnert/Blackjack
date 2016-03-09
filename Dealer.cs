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

        public Dealer()
        {
            hand = new List<Card>();
        }

        public void addCard(Card c)
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
    }
}
