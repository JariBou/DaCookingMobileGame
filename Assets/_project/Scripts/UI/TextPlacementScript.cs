using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts.UI
{
    public class TextPlacementScript : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Transform _firstElement;
        [SerializeField] private List<Transform> _otherElements;

        [Header("Params")]
        [SerializeField] private Vector3 _firstElementPosition = new Vector3(-1, -1, -50);
        [SerializeField] private Vector3 _elementScale = new Vector3(-1, -1, -50);
        [SerializeField, Range(0, 10f)] private float _cardSpacing = 1f;
    
        private void PlaceElements()
        {
            for (int i = 0; i < _otherElements.Count; i++)
            {
                Transform cardTransform = _otherElements[i];

                cardTransform.position = _firstElementPosition + new Vector3(1, 0, 0) * (i + 1) * _cardSpacing;
                cardTransform.localScale = _elementScale;
            }
        }

        private void OnValidate()
        {
            if (_firstElementPosition == new Vector3(-1, -1, -50)) _firstElementPosition = _firstElement.position;
            if (_elementScale == new Vector3(-1, -1, -50)) _elementScale = _firstElement.localScale;
        
            _firstElement.position = _firstElementPosition;
            _firstElement.localScale = _elementScale;
        
        
            PlaceElements();
        }
    }
}
