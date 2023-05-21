using UnityEngine;

namespace CodeBase.Game.Card
{
    [System.Serializable]
    public class CardVisual
    {
        [SerializeField] private SpriteRenderer _renderer;

        private Sprite _showedSide;
        private Sprite _reverseSide;

        public void Init(Sprite showedSide, Sprite reverseSide) 
        {
            _showedSide = showedSide;
            _reverseSide = reverseSide;

            _renderer.sprite = _reverseSide;
        }

        public void Show(bool isOpen)
        {
            _renderer.sprite = isOpen ? _showedSide : _reverseSide;
        }
    }
}