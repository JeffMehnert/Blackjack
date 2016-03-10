/*
Made by Jeff Mehnert
Personal Project
March 2016
*/
using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Card
    {
        public string id; //what you see on the card
        public int value; //the value the card represents in blackjack
        public enum Suit  //card suit (doesn't really matter in blackjack unless special side bets exist that could involve card suit (perfect pair))
        {
            Spades = 0,
            Clubs,
            Diamonds,
            Hearts
        };
        public Suit suit;

        public Card(string id, Suit suit) //constructor
        {
            this.id = id;
            this.suit = suit;
            setValue(id);
        }

        public void setValue(string id) //sets value based on card's ID
        {
            if (id == "2" || id == "3" || id == "4" || id == "5" || id == "6" || id == "7" || id == "8" || id == "9" || id == "10")
                value = Convert.ToInt32(id);
            else if (id == "J" || id == "Q" || id == "K")
                value = 10;
            else if (id == "A") //ace can be 1 as well but this case is handled in player/dealer classes inside of getHandVal() functions
                value = 11;
        }
    }
}
