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
    class Card
    {
        public string id;
        public int value;
        public int altValue;
        public enum Suit
        {
            Spades = 0,
            Clubs,
            Diamonds,
            Hearts
        };
        public Suit suit; 

        public Card(string id)
        {
            this.id = id;
            altValue = 0;
            setValue(id);
        }

        public Card(string id, Suit suit)
        {
            this.id = id;
            this.suit = suit;
            altValue = 0;
            setValue(id);
        }

        public void setValue(string id)
        {
            if(id == "2" || id == "3" || id == "4" || id == "5" || id == "6" || id == "7" || id == "8" || id == "9" || id == "10")
                value = Convert.ToInt32(id);
            else if(id == "J" || id == "Q" || id == "K")
                value = 10;
            else if(id == "A")
            {
                value = 11;
                altValue = 1;
            }
        }
    }
}
