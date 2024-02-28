using System;
using System.Collections;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using UnityEngine;

namespace _project.Scripts
{
    [Serializable]
    public class ClickUp : MonoBehaviour
    {
        [SerializeField] private float _scaleMultiplier = 1.25f;
        [SerializeField, Range(0,4)] private float _heightOffset = 2f;
        [SerializeField, Range(1, 10)] private float _moveSpeed = 5f;

        private Vector3 _initialScale;
        private Vector3 _initialPosition;
        public Vector3 InitialPosition => _initialPosition;
        public Vector3 InitialScale => _initialScale;

        public IngredientSo Ingredient => _ingredientSo;


        private Vector3 _endPos;
        private Vector3 _endScale;

        private bool _isScaled = false;
        public bool IsScaled => _isScaled;
        private bool _isMoving = false;

        [SerializeField] private IngredientSo _ingredientSo;
        [SerializeField] private Card _cardDisplay;

        [SerializeField] private GameObject _padlock;

        public static List<ClickUp> _enlargedSprites = new List<ClickUp>();

        private Menu _menu;

        [Header("Changing Card")]
        private bool _isPassing = false;
        private bool _isAppearing = false;
        [SerializeField, Range(1, 10)] private float _transitionSpeed = 5f;
        [SerializeField, Range(0, -6)] private float _negativeHeightOffset = 2f;


        private void Start()
        {
            _ingredientSo = GetComponent<Card>()._ingredientSo;
            _initialScale = transform.localScale;
            _initialPosition = transform.position;
            _menu = FindFirstObjectByType<Menu>();
        }


        public IEnumerator PassIngredient(IngredientSo newIngredient)
        {
            _isPassing = true; // La carte descend en dehors de l'écran
            if (!_isPassing)
            {
                _ingredientSo = newIngredient;
                _cardDisplay.InitializeCard(newIngredient);
            }
            yield return new WaitForSeconds(1f);
            _isAppearing = true; // La carte remonte à sa position initiale


        }



        private void Update()
        {
            if (!_isMoving) return;
            
            transform.position = Vector3.Lerp(transform.position, _endPos, Time.deltaTime * _moveSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, _endScale, Time.deltaTime * _moveSpeed);
            if (Vector3.Distance(transform.position, _endPos) < 0.001f)
            {
                transform.position = _endPos;
                transform.localScale = _endScale;
                _isMoving = false;
            }

            
            if (_isPassing)
            {
                transform.position = Vector3.Lerp(transform.position, _initialPosition + Vector3.up * _negativeHeightOffset, Time.deltaTime * _transitionSpeed);
                if (Vector3.Distance(transform.position, _initialPosition + Vector3.up * _negativeHeightOffset) < 0.001f)
                {
                    transform.position = _initialPosition + Vector3.up * _negativeHeightOffset;
                    _isPassing = false;
                }
            }
            if (_isAppearing)
            {
                transform.position = Vector3.Lerp(transform.position, _initialPosition, Time.deltaTime * _transitionSpeed);
                if (Vector3.Distance(transform.position, _initialPosition + Vector3.up * _heightOffset) < 0.001f)
                {
                    transform.position = _initialPosition + Vector3.up * _heightOffset;
                    _isAppearing = false;
                }
            }
        }

        private void OnMouseDown()
        {
            switch (_isScaled)
            {
                case false:
                {
                    if (_enlargedSprites.Count >= 3)
                    {
                        _enlargedSprites[0].StartMoving(_enlargedSprites[0].InitialPosition, _enlargedSprites[0].InitialScale, false);
                        _enlargedSprites[0]._padlock.SetActive(false);
                        _enlargedSprites.RemoveAt(0);
                        _menu.UpdateMenu();
                    }

                    StartMoving(_initialPosition + Vector3.up * _heightOffset, new Vector3(_initialScale.x * _scaleMultiplier, _initialScale.y * _scaleMultiplier, _initialScale.z));
                    _isScaled = true;
                    _padlock.SetActive(true);
                    _enlargedSprites.Add(this);
                    _menu.UpdateMenu();
                    break;
                }
                case true:
                    StartMoving(_initialPosition, _initialScale);
                    _isScaled = false;
                    _padlock.SetActive(false);
                    _enlargedSprites.Remove(this);
                    _menu.UpdateMenu();
                    break;
            }
        }

        public void StartMoving(Vector3 endPos, Vector3 scale, bool willScale = true)
        {
            _endPos = endPos;
            _endScale = scale;
            _isMoving = true;
            _isScaled = willScale;
        }


    }
}
