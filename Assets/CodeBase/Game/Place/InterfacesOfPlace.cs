using CodeBase.Game.Card;
using CodeBase.Game.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Place
{
    public interface IPlace: ISelectable, ITempPlace
    {
        //public Vector3 Offset { get; }
        public Vector3 LocalPositionToAdding { get; }
        public IPlayingCard LastCard { get; }
        public Transform Last { get; }

        public void AddCard(IPlayingCard card);
    }

    public interface IDownPlace: IPlace
    {
        public bool StartFilling(IPlayingCard card, ref int cardIndex);
    }

    public interface IDeckPlace : IPlace
    {
        public IPlayingCard Pop();
    }

    public interface ITempPlace
    {
        public List<IPlayingCard> SelectCardsToHand(IPlayingCard card);
        public bool CheckRulesToJoin(IPlayingCard card);
        public void SayMeGoobye(IPlayingCard card);
        public void SayMeHello(List<IPlayingCard> cards);
    }
}