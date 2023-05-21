using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Info
{
    [CreateAssetMenu(menuName = nameof(CodeBase) + "/" + nameof(Info) + nameof(CardDeckImageSO))]
    public class CardDeckImageSO : ScriptableObject
    {
        [field: SerializeField] public Sprite CardReverseSide { get; private set; }
        [field: SerializeField] public List<CardImage> CardImages { get; private set; }
    }
}