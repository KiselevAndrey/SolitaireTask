using Assets.CodeBase.Game.Place;
using CodeBase.Game.Card;
using CodeBase.Game.Logic;
using CodeBase.Utility.Extension;
using UnityEngine;

namespace CodeBase.Game.Place
{
    public class PlaceDown : PlayingPlace, IDownPlace
    {
        [SerializeField, Range(1, 7)] private int _startCardCount = 1;

        private readonly Rank _emptyRank = Rank._K;

        public bool StartFilling(IPlayingCard card, ref int cardIndex)
        {
            if(cardIndex >= _startCardCount) 
                return false;

            card.MoveToNewPlace(this, cardIndex, OnCardEndMovingWhenFilling);
            AddCard(card);

            return ++cardIndex < _startCardCount;
        }

        public override bool CheckRulesToJoin(IPlayingCard card)
        {
            if (Cards.IsNotEmpty())
                return RuleChecker.CheckDownPlaceRule(LastCard, card);
            else
                return card.Rank == _emptyRank;
        }

        private void OnCardEndMovingWhenFilling(IPlayingCard card, int cardIndex)
        {
            card.IsOpen = cardIndex >= _startCardCount - 1;
        }
    }
}