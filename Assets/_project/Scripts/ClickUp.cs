using System;
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
        private bool _isMoving = false;

        [SerializeField] private IngredientSo _ingredientSo;

        public static List<ClickUp> _enlargedSprites = new List<ClickUp>();

        private Menu _menu;

        private void Start()
        {
            _ingredientSo = GetComponent<Card>()._ingredientSo;
            _initialScale = transform.localScale;
            _initialPosition = transform.position;
            _menu = FindFirstObjectByType<Menu>();
        }

        public void PassIngredient(IngredientSo newIngredient)
        {
            _ingredientSo = newIngredient;
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
                        _enlargedSprites.RemoveAt(0);
                        _menu.UpdateMenu();
                    }

                    StartMoving(_initialPosition + Vector3.up * _heightOffset, new Vector3(_initialScale.x * _scaleMultiplier, _initialScale.y * _scaleMultiplier, _initialScale.z));
                    _isScaled = true;
                    _enlargedSprites.Add(this);
                    _menu.UpdateMenu();
                    break;
                }
                case true:
                    StartMoving(_initialPosition, _initialScale);
                    _isScaled = false;
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
