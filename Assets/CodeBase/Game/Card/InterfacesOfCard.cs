using CodeBase.Game.Logic;
using CodeBase.Game.Place;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Card
{
    public interface IPlayingCard : ISelectable, IMovableCard, IClickableCard
    {
        public Suit Suit { get; }
        public Rank Rank { get; }
        public IPlace Place { get; }
        public bool IsOpen { get; set; }

        public void SetPlace(IPlace place);
        public List<IPlayingCard> PickUpMe();
        public string ToString();
        public void ChangePlace(List<IPlayingCard> cards, IPlace newPlace);
    }

    public interface IMovableCard
    {
        public void MoveToNewPlace(IPlace place, int cardIndex, Action<IPlayingCard, int> onEndMove);
        public void MoveToNewPlaceShort(IPlace place);
        public void MoveFromMouse(Vector3 mousePos);
        public void MoveToPreClickPosition(List<IPlayingCard> cards);
    }

    public interface IClickableCard
    {
        public BoxCollider2D Collider { get; }

        public void EnableCollider(bool enable);
    }
}
