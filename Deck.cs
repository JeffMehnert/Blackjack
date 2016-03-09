using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Deck
    {
        public List<Card> cards;

        public Deck(int howManyDecks)
        {
            cards = new List<Card>();
            for (int i = 0; i < howManyDecks; i++)
                fillOneDeck();

            shuffle();
        }

        private void fillOneDeck()
        {
            for(int i = 2; i <= 10; i++) //create the 2-10 cards
            {
                for (int j = 0; j < 4; j++) //add four cards of each type 2-10
                {
                    cards.Add(new Card(i.ToString(), (Card.Suit)j));
                }
            }

            for(int i = 0; i < 4; i++)
            {
                cards.Add(new Card("J", (Card.Suit)i));
                cards.Add(new Card("Q", (Card.Suit)i));
                cards.Add(new Card("K", (Card.Suit)i));
                cards.Add(new Card("A", (Card.Suit)i));
            }
        }

        private void shuffle()
        {
            Random rand = new Random();
            int n = cards.Count;
            while(n > 1)
            {
                n--;
                int k = rand.Next(n+1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }
    }
}
