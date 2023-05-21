using CodeBase.Game.Card;
using UnityEngine;

namespace CodeBase.Game.Info
{
    [System.Serializable]
    public class CardImage
    {
        [field: SerializeField] public Suit Suit { get; private set; } 
        [field: SerializeField] public Rank Rank { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
    }
}