using Assets.CodeBase.Game.Place;
using CodeBase.Game.Card;
using CodeBase.Utility.Extension;

namespace CodeBase.Game.Place
{
    public class PlaceDeck : PlayingPlace, IDeckPlace
    {
        public IPlayingCard Pop()
        {
            var card = LastCard;
            Cards.RemoveAt(Cards.LastIndex());
            return card;
        }
    }
}
