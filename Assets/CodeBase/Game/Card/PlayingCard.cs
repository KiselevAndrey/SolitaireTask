using CodeBase.Game.Logic;
using CodeBase.Game.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Game.Card
{
    public class PlayingCard : Selectable, IPlayingCard
    {
        [SerializeField] private CardVisual _visual;
        [field: SerializeField] public BoxCollider2D Collider { get; private set; }

        private CardMover _mover;

        private bool _isOpen = false;

        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
        public IPlace Place { get; private set; }
        public bool IsOpen
        {
            get => _isOpen; 
            set 
            { 
                if (_isOpen != value)
                {
                    _isOpen = value;
                    _visual.Show(value);
                    Collider.enabled = value;
                }
            }
        }

        public void Init(Suit suit, Rank rank, bool isOpen, Sprite showedSide, Sprite reverseSide)
        {
            _visual.Init(showedSide, reverseSide);

            Suit = suit;
            Rank = rank;
            IsOpen = isOpen;

            gameObject.name = $"Card " + ToString();
        }

        #region IPlayingCard
        public void SetPlace(IPlace place)
        {
            Place = place;
            Transform.parent = place.Last;
        }

        public List<IPlayingCard> PickUpMe()
        {
            _mover.SavePreClickPosition();
            var cards = Place.SelectCardsToHand(this);
            foreach (var card in cards) 
                card.Collider.enabled = false;

            return cards;
        }

        public override string ToString() { return $"{Suit}{Rank}"; }

        public void ChangePlace(List<IPlayingCard> cards, IPlace newPlace)
        {
            Place.SayMeGoobye(this);

            foreach (PlayingCard card in cards.Cast<PlayingCard>())
                card.Place = newPlace;

            Place.SayMeHello(cards);
        }
        #endregion IPlayingCard

        #region IMovableCard
        public void MoveToNewPlace(IPlace place, int cardIndex, Action<IPlayingCard, int> onEndMove) =>
            _mover.MoveToNewPlace(place, cardIndex, onEndMove);
        public void MoveToNewPlaceShort(IPlace place) =>
            _mover.MoveToNewPlaceShort(place);

        public void MoveFromMouse(Vector3 mousePos) =>
            _mover.MoveFromMouse(mousePos);

        public void MoveToPreClickPosition(List<IPlayingCard> cards)
        {
            foreach (var card in cards)
                card.Collider.enabled = true;

            _mover.MoveToPreClickPosition();
        }
        #endregion IMovableCard

        public void EnableCollider(bool enable) =>
            Collider.enabled = enable;

        private void Awake()
        {
            _mover = new(this);
        }
    }
}