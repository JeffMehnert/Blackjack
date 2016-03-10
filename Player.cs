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
    class Player
    {
        public string name;
        public List<Card> hand;
        public bool hasAce;
        public bool busted;
        public int rawHandValue;
        
        public Player()
        {
            name = "";
            hand = new List<Card>();
            hasAce = false;
            busted = false;
            rawHandValue = 0;
        }
        public Player(string name)
        {
            this.name = name;
            hand = new List<Card>();
            hasAce = false;
            busted = false;
            rawHandValue = 0;
        }

        public void hit(Card c)
        {
            hand.Add(c);
        }

        public void emptyHand()
        {
            hand.Clear();
        }
        
        public void getHand()
        {
            Console.WriteLine("{0} has:", name);
            foreach(Card c in hand)
            {
                rawHandValue += c.value;
                Console.WriteLine("{0} of {1}", c.id, c.suit);
            }
            Console.WriteLine();    
        }

        public int getHandVal()
        {
            int val = 0;
            int aceCount = 0;
            foreach (Card c in hand)
            {
                val += c.value;
                if (c.id == "A")
                    aceCount++;
            }

            for (int i = 0; i < aceCount; i++)
                val -= 10;

            return val;
        }
    }
}
