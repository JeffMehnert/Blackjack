/*
Made by Jeff Mehnert
Personal Project
March 2016
*/
using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer
    {
        public List<Card> hand;
        public bool busted;
        public bool blackjack;

        public Dealer() //constructor
        {
            hand = new List<Card>();
            busted = false;
            blackjack = false;
        }

        public void draw(Card c) //add card to dealer's hand
        {
            hand.Add(c);
        }

        public void getTopCard() //prints top card in dealer's hand (showing his top card, hiding his bottom card)
        {
            Console.WriteLine("Dealer shows:");
            Console.WriteLine("{0} of {1}", hand[hand.Count - 1].id, hand[hand.Count - 1].suit);
        }

        public void emptyHand() //prepares hand for next round
        {
            hand.Clear();
            busted = false;
            blackjack = false;
        }

        public void getHand() //prints hand's contents
        {
            foreach (Card c in hand)
                Console.WriteLine(c.id);
        }

        public int getHandVal() //returns value of hand
        {
            int val = 0;
            int aceCount = 0;
            foreach (Card c in hand)
            {
                val += c.value;
                if (c.id == "A")
                    aceCount++;
            }

            if (val > 21 && aceCount > 0) //if value is over 21 and we've seen aces, we can subtract 10 for each ace we've seen, as aces represent 1 or 11
            {
                for (int i = 0; i < aceCount; i++)
                    val -= 10;
            }

            if (val > 21)
                busted = true;

            return val;
        }
    }
}
