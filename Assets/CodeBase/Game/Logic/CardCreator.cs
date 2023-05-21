using CodeBase.Game.Card;
using CodeBase.Game.Info;
using CodeBase.Game.Place;
using CodeBase.Utility.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Game.Logic
{
    public class CardCreator : MonoBehaviour
    {
        [Header("Card")]
        [SerializeField] private CardDeckImageSO _allCardsInfo;
        [SerializeField] private PlayingCard _cardPrefab;

        [Header("Deck")]
        [SerializeField] private PlaceDeck _startDeck;
        [field: SerializeField] private List<PlaceDown> _downPlaces;

        [Header("Another")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private UserInput _userInput;

        private void Start()
        {
            _userInput.enabled = false;
            CreateAllCards();
            StartCoroutine(FillingDownPlaces());
        }

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(OnResetButtonClick);
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(OnResetButtonClick);
        }

        private void OnResetButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void CreateAllCards()
        {
            foreach(var card in _allCardsInfo.CardImages.Shuffled())
            {
                var playingCard = Instantiate(_cardPrefab);
                playingCard.Init(card.Suit, card.Rank, false, card.Image, _allCardsInfo.CardReverseSide);
                _startDeck.AddCard(playingCard);
                playingCard.Transform.localPosition = _startDeck.LocalPositionToAdding;
            }
        }

        private IEnumerator FillingDownPlaces()
        {
            var wait = new WaitForSeconds(0.1f);
            foreach(var place in _downPlaces)
            {
                var index = 0;
                IPlayingCard card;
                do
                {
                    yield return wait;
                    card = _startDeck.Pop();
                }
                while (place.StartFilling(card, ref index));
            }

            _userInput.enabled = true;
        }
    }
}