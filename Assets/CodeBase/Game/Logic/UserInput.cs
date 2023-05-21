using CodeBase.Game.Card;
using CodeBase.Game.Place;
using CodeBase.Utility.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Logic
{
    public class UserInput: MonoBehaviour
    {
        [SerializeField, Range(1, 5)] private float _overlapRadius = 3f;

        private UnityEngine.Camera _camera;
        private List<IPlayingCard> _hand;
        private Vector3 _clickPoint;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                OnClick();
            else if (Input.GetMouseButton(0))
                OnDrag();
            else if (Input.GetMouseButtonUp(0))
                OnDragEnd();
        }

        private void OnClick()
        {
            var clickPosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            var hit = Physics2D.Raycast(clickPosition, Vector2.zero);
            if (hit)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.TryGetComponent(out IPlayingCard card))
                {
                    _hand = card.PickUpMe();
                    _clickPoint = card.Transform.InverseTransformPoint(hit.point);
                }
            }
        }

        private void OnDrag()
        {
            if(_hand.IsNullOrEmpty()) return;

            var clickPosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
            _hand[0].MoveFromMouse(clickPosition - _clickPoint);
        }

        private void OnDragEnd()
        {
            if (_hand.IsNullOrEmpty()) return;

            var clickPosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            var hit = Physics2D.Raycast(clickPosition, Vector2.zero);
            if (hit)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.TryGetComponent(out IPlayingCard card))
                    CheckCards(card);

                else if (hit.collider.TryGetComponent(out Table _))
                    MakeOverlap(hit);

                else if(hit.collider.TryGetComponent(out IPlace place))
                    CheckPlace(place);

                else
                    _hand[0].MoveToPreClickPosition(_hand);
            }
            else
                _hand[0].MoveToPreClickPosition(_hand);

            _hand.Clear();
        }

        private void CheckCards(IPlayingCard card)
        {
            if (_hand[0].Place == card.Place)
                _hand[0].MoveToPreClickPosition(_hand);
            else
            {
                if (card.Place.CheckRulesToJoin(_hand[0]))
                    _hand[0].ChangePlace(_hand, card.Place);
                else
                    _hand[0].MoveToPreClickPosition(_hand);
            }
        }

        private void MakeOverlap(RaycastHit2D hit)
        {
            var hitColliders = Physics2D.OverlapCircleAll(hit.point, _overlapRadius);

            var minDistante = _overlapRadius * 10;
            IPlayingCard nearestCard = null;
            IPlace nearestPlace = null;
            var isPlaceNearly = false;

            foreach (var collider in hitColliders)
            {
                if (collider.TryGetComponent(out IPlayingCard playingCard))
                {
                    var distance = Vector2.Distance(hit.point, playingCard.Transform.position);
                    if (distance <= minDistante)
                    {
                        minDistante = distance;
                        nearestCard = playingCard;
                        isPlaceNearly = false;
                    }
                }
                else if(collider.TryGetComponent(out IPlace place))
                {
                    var distance = Vector2.Distance(hit.point, place.Transform.position);
                    if (distance <= minDistante)
                    {
                        minDistante = distance;
                        nearestPlace = place;
                        isPlaceNearly = true;
                    }
                }
            }

            if (nearestCard != null && isPlaceNearly == false)
                CheckCards(nearestCard);
            else if(nearestPlace != null && isPlaceNearly == true)
                CheckPlace(nearestPlace);
            else
                _hand[0].MoveToPreClickPosition(_hand);
        }

        private void CheckPlace(IPlace place)
        {
            if (place.CheckRulesToJoin(_hand[0]))
                _hand[0].ChangePlace(_hand, place);
            else
                _hand[0].MoveToPreClickPosition(_hand);
        }
    }
}
