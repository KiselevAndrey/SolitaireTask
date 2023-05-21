using CodeBase.Game.Card;
using System;

namespace CodeBase.Game.Logic
{
    public static class RuleChecker
    {
        private static readonly string[] _suits = Enum.GetNames(typeof(Suit));

        public static bool CheckDownPlaceRule(IPlayingCard card1, IPlayingCard card2)
        {
            if (card1.Suit == card2.Suit || card1.Suit == (Suit)(((int)(card2.Suit) + 2) % _suits.Length))
                return false;

            if((int)card1.Rank - (int)card2.Rank != 1)
                return false;

            return true;
        }
    }
}