using System;
using System.Collections.Generic;

namespace Poker
{
    class Program
    {
        private static HashSet<int> indexSet;
        static void Main(string[] args)
        {
            //Card suit: Spade, Heart, Club, Diamond, Joker1, Joker2
            //Card value: 2-14
            string[] suits = { "Spade", "Heart", "Club", "Diamond" };

            //Create the deck
            Card[] deck = new Card[54];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    deck[i * 13 + j] = new Card(suits[i], j + 2);
                }
            }
            deck[52] = new Card("Joker", -1);
            deck[53] = new Card("Joker", -1);

            //Get 4 hands
            Card[,] hands = new Card[4,5];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    hands[i, j] = deck[getNextIndex()];
                }
            }

            //Get the rank
            int[] ranks = new int[4];
            for (int i = 0; i < 4; i++)
            {
                Card[] hand = new Card[5];
                for (int j = 0; j < 5; j++)
                {
                    hand[j] = hands[i, j];
                }
                ranks[i] = getRank(hand);
            }

            //Get the max rank hand
            int max = 0;
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                if (ranks[i] > max)
                {
                    max = ranks[i];
                    index = i;
                }
            }

            Console.WriteLine("The highest rank is " + (index + 1).ToString());
        }

        static int getNextIndex()
        {
            if (indexSet == null)
            {
                indexSet = new HashSet<int>();
            }

            int index = -1;
            Random rand = new Random(DateTime.Now.Millisecond);
            do
            {
                index = rand.Next(0, 53);
                if (!indexSet.Contains(index))
                {
                    indexSet.Add(index);
                    break;
                }
            } while (true);

            return index;
        }

        static int getRank(Card[] hand)
        {
            int rank = -1;
            ///Five of a kind
            if (isContainCard(hand, 14, "Spade") && isContainCard(hand, 14, "Heart") && isContainCard(hand, 14, "Club")
                && isContainCard(hand, 14, "Diamond") && isContainCard(hand, -1, "Joker"))
            {
                rank = 10;
            }
            ///Straight flush
            if (isContainCard(hand, 7, "Club") && isContainCard(hand, 8, "Club") && isContainCard(hand, 9, "Club")
                && isContainCard(hand, 10, "Club") && isContainCard(hand, 11, "Club")) 
            {
                rank = 9;
            }
            ///Four of a kind
            if (isContainCard(hand, 5, "Spade") && isContainCard(hand, 5, "Heart") && isContainCard(hand, 5, "Club")
                && isContainCard(hand, 5, "Diamond") ) 
            {
                rank = 8;
            }
            ///Full house
            if (isContainCard(hand, 6, "Spade") && isContainCard(hand, 6, "Heart") && isContainCard(hand, 6, "Club")
                && isContainCard(hand, 5, "Diamond") && isContainCard(hand, 5, "Heart"))
            {
                rank = 7;
            }
            /// Three of a kind
            if (isContainCard(hand, 12, "Spade") && isContainCard(hand, 12, "Heart") && isContainCard(hand, 12, "Club"))
            {
                rank = 6;
            }
            /// Two pair
            if (isContainCard(hand, 11, "Club") && isContainCard(hand, 11, "Heart") && isContainCard(hand, 2, "Spade") && isContainCard(hand, 2, "Diamond"))
            {
                rank = 5;
            }
            /// One pair
            if (isContainCard(hand, 10, "Club") && isContainCard(hand, 10, "Heart"))
            {
                rank = 4;
            }
            /// High card
            if (isContainCard(hand, 13, "Diamond") && isContainCard(hand, 12, "Diamond"))
            {
                rank = 3;
            }
            return rank;
        }

        static bool isContainCard(Card[] hand, int value, string suite)
        {
            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i].Value == value && hand[i].Suite == suite)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
