using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts
{
    public class CardPlacementScript : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Transform _firstCard;
        [SerializeField] private List<Transform> _otherCards;

        [Header("Params")]
        [SerializeField] private Vector3 _firstCardPosition = new Vector3(-1, -1, -50);
        [SerializeField] private Vector3 _cardScale = new Vector3(-1, -1, -50);
        [SerializeField, Range(0, 10f)] private float _cardSpacing = 1f;
    
        private void PlaceCards()
        {
            for (int i = 0; i < _otherCards.Count; i++)
            {
                Transform cardTransform = _otherCards[i];

                cardTransform.position = _firstCardPosition + new Vector3(1, 0, 0) * (i + 1) * _cardSpacing;
                cardTransform.localScale = _cardScale;
            }
        }

        private void OnValidate()
        {
            if (_firstCardPosition == new Vector3(-1, -1, -50)) _firstCardPosition = _firstCard.position;
            if (_cardScale == new Vector3(-1, -1, -50)) _cardScale = _firstCard.localScale;
        
            _firstCard.position = _firstCardPosition;
            _firstCard.localScale = _cardScale;
        
        
            PlaceCards();
        }
    }
}
