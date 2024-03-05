using System;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using UnityEngine;
using UnityEngine.Events;

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
        private IngredientSo _newIngredient;
        [SerializeField] private Card _cardDisplay;

        [SerializeField] private GameObject _padlock;

        public static List<ClickUp> EnlargedSprites = new();

        private RecipeDisplayScript _recipeDisplayScript;

        [Header("Changing Card")]
        [SerializeField] private bool _isPassing = false;
        [SerializeField] private bool _isAppearing = false;
        [SerializeField] private AnimationCurve _appearCurve;
        [SerializeField, Range(0,10)] private float _animationDuration = 1f;
        private float _timer;

        [SerializeField, Range(0, -6)] private float _negativeHeightOffset = 2f;


        [Header("GameFeel")]
        [SerializeField] private UnityEvent _onCardClick;
        [SerializeField] private UnityEvent _onCardUnClick;


        private void Start()
        {
            _ingredientSo = GetComponent<Card>()._ingredientSo;
            _initialScale = transform.localScale;
            _initialPosition = transform.position;
            _recipeDisplayScript = FindFirstObjectByType<RecipeDisplayScript>();
        }

        public void PassIngredient(IngredientSo newIngredient)
        {
            /*Debug.Log("Changing " + _ingredientSo.Name + " with " + newIngredient.Name);*/
            _isAppearing = false;
            _isPassing = true; // La carte descend en dehors de l'écran
            _newIngredient = newIngredient; 
            transform.position = _initialPosition;
        }

        private void Update()
        {
           /* if (!_isMoving && !_isAppearing && _isPassing) return;*/
            if (_isMoving)
            {
                transform.position = Vector3.Lerp(transform.position, _endPos, Time.deltaTime * _moveSpeed);
                transform.localScale = Vector3.Lerp(transform.localScale, _endScale, Time.deltaTime * _moveSpeed);
                if (Vector3.Distance(transform.position, _endPos) < 0.001f)
                {
                    transform.position = _endPos;
                    transform.localScale = _endScale;
                    _isMoving = false;
                }
            }

            if (_isPassing)
            {
                _timer += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, _initialPosition + Vector3.up * _negativeHeightOffset, _appearCurve.Evaluate(_timer/_animationDuration));
                if (Vector3.Distance(transform.position, _initialPosition + Vector3.up * _negativeHeightOffset) < 0.01f)
                {
                    transform.position = _initialPosition + Vector3.up * _negativeHeightOffset;
                    _cardDisplay.InitializeCard(_newIngredient);
                    _ingredientSo = _newIngredient;
                    _newIngredient = null;
                    _isPassing = false;
                    _isAppearing = true;
                    _timer = 0;
                }
            }
            if (_isAppearing)
            {
                _timer += Time.deltaTime;
                _isPassing = false;
                transform.position = Vector3.Lerp(transform.position, _initialPosition, _appearCurve.Evaluate(_timer/_animationDuration));
                if (Vector3.Distance(transform.position, _initialPosition) < 0.01f)
                {
                    transform.position = _initialPosition;
                    _isAppearing = false;
                    _timer = 0;
                }
            }
        }

        private void OnMouseDown()
        {
            if (_isPassing || _isAppearing || _recipeDisplayScript.CookingManager.GetCurrentPhase() != PhaseCode.Phase1) return;
            
            switch (_isScaled)
            {
                case false:
                {
                    if (EnlargedSprites.Count >= 3)
                    {
                        EnlargedSprites[0].StartMoving(EnlargedSprites[0].InitialPosition, EnlargedSprites[0].InitialScale, false);
                        EnlargedSprites[0]._padlock.SetActive(false);
                        EnlargedSprites.RemoveAt(0);
                        _recipeDisplayScript.UpdateMenu();
                    }

                    StartMoving(_initialPosition + Vector3.up * _heightOffset, new Vector3(_initialScale.x * _scaleMultiplier, _initialScale.y * _scaleMultiplier, _initialScale.z));
                    _isScaled = true;
                    _onCardClick?.Invoke();
                    _padlock.SetActive(true);
                    EnlargedSprites.Add(this);
                    _recipeDisplayScript.UpdateMenu();
                    break;
                }
                case true:
                    StartMoving(_initialPosition, _initialScale);
                    _isScaled = false;
                    _onCardUnClick?.Invoke();
                    _padlock.SetActive(false);
                    EnlargedSprites.Remove(this);
                    _recipeDisplayScript.UpdateMenu();
                    break;
            }
        }

        public void StartMoving(Vector3 endPos, Vector3 scale, bool willScale = true)
        {
            _endPos = endPos;
            _endScale = scale;
            _isMoving = true;
            if (!willScale) _onCardUnClick?.Invoke();
            _isScaled = willScale;
        }


    }
}
