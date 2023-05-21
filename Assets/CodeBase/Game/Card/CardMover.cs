using CodeBase.Game.Place;
using DG.Tweening;
using System;
using UnityEngine;

namespace CodeBase.Game.Card
{
    public class CardMover
    {
        private IPlayingCard _card;
        private Vector3 _preClickPosition;

        public CardMover(IPlayingCard card)
        {
            _card = card;
        }

        public void MoveToNewPlace(IPlace place, int cardIndex, Action<IPlayingCard, int> onEndMove)
        {
            _card.Transform.parent = place.Last;
            _card.Transform.DOLocalMove(place.LocalPositionToAdding, 0.3f)
                .OnComplete(() => onEndMove(_card, cardIndex));
        }

        internal void MoveToNewPlaceShort(IPlace place)
        {
            _card.Transform.parent = place.Last;
            _card.Transform.DOLocalMove(place.LocalPositionToAdding, Time.deltaTime);
        }

        public void MoveFromMouse(Vector3 mousePos)
        {
            _card.Transform.position = mousePos;

        }

        internal void MoveToPreClickPosition()
        {
            _card.Transform.DOLocalMove(_preClickPosition, 0.1f);
        }

        internal void SavePreClickPosition()
        {
            _preClickPosition = _card.Transform.localPosition;
        }
    }
}
