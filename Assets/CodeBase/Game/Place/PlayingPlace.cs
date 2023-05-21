using CodeBase.Game.Card;
using CodeBase.Game.Logic;
using CodeBase.Game.Place;
using CodeBase.Utility.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Game.Place
{
    public abstract class PlayingPlace : Selectable, IPlace
    {
        [field: SerializeField] protected Vector3 Offset = new(0, 0, -0.01f);

        protected List<IPlayingCard> Cards = new();

        public Transform Last => Cards.IsEmpty() ? Transform : LastCard.Transform;
        public Vector3 LocalPositionToAdding => LastCard == null 
            ? new Vector3(0, 0, Offset.z) 
            : Offset;

        public IPlayingCard LastCard => Cards.IsEmpty() ? null : Cards[^1];

        public void AddCard(IPlayingCard card)
        {
            card.SetPlace(this);
            Cards.Add(card);
        }

        #region ITempPlace
        public virtual bool CheckRulesToJoin(IPlayingCard card)
        {
            return false;
        }

        public void SayMeGoobye(IPlayingCard card)
        {
            var index = Cards.IndexOf(card);
            Cards.RemoveRange(index, Cards.Count - index);
            card.Transform.parent = null;

            if (LastCard != null)
                LastCard.IsOpen = true;
        }

        public void SayMeHello(List<IPlayingCard> cards)
        {
            cards[0].MoveToNewPlaceShort(this);

            foreach (var card in cards)
                card.EnableCollider(true);

            Cards.AddRange(cards);
        }

        public List<IPlayingCard> SelectCardsToHand(IPlayingCard card)
        {
            var index = Cards.IndexOf(card);
            return Cards.GetRange(index, Cards.Count - index);
        }
        #endregion ITempPlace
    }
}
