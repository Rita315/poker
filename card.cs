using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Card
    {
        public string Suite { get; set; }
        public int Value { get; set; }

        public Card (string suite, int value)
        {
            Suite = suite;
            Value = value;
        }
    }
}
