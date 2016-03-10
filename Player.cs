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

        public Player(string name) //constructor
        {
            this.name = name;
            hand = new List<Card>();
            busted = false;
            blackjack = false;
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
