/*
Made by Jeff Mehnert
Personal Project
March 2016
*/
using System;
using System.Collections.Generic;
using System.Threading;

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

        public Card getTopCard() //prints first card in dealer's hand (showing his top card, hiding his bottom card)
        {
            Console.WriteLine("Dealer shows:");
            Card topCard = hand[0];
            Console.WriteLine("{0} of {1}", topCard.id, topCard.suit);
            return topCard;
        }

        public void dealTable() //deals one card to each player at the table
        {
            foreach (Player p in MainClass.players)
            {
                p.hit(MainClass.drawCard());
            }
        }

        public void playDealerHand() //executes the dealer playing their cards
        {
            Console.WriteLine("Dealer's hand: ");
            getHand();
            int dealerHandVal = getHandVal();
            Console.WriteLine("Dealer hand value: {0}", dealerHandVal.ToString());
            while (dealerHandVal < 17) //dealer must draw until the value of his hand is at least 17
            {
                Thread.Sleep(2000); //pause for 2 seconds while dealer takes a card (helps visualize the game in the console view)
                Card drawnCard = MainClass.drawCard();
                Console.WriteLine("Dealer draws {0}", drawnCard.id);
                draw(drawnCard);
                dealerHandVal = getHandVal();
                Console.WriteLine("Dealer hand value: {0}", dealerHandVal.ToString());
            }
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
