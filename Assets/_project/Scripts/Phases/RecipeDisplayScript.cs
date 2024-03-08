using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _project.Scripts.Core;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class RecipeDisplayScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private IngredientStats[] _ingredientStats;
        [SerializeField] private CameraScript _camera;
        [SerializeField] private MealDisplayScript _nextPhaseMealDisplay;
        private Meal _currentMeal;
        [SerializeField] private TMP_Text _finalHunger;
        [SerializeField] private TMP_Text _finalSatisfaction;
        [SerializeField] private TMP_Text _finalPower;
        [SerializeField] private float _slideTime = 1f;
        [SerializeField] private Transform _uiTransform;
        [SerializeField] private Transform _cauldronTransform;

        [SerializeField] private AnimationCurve _uiSlideCurve;
        [SerializeField] private AnimationCurve _mealSlideCurve;
        [SerializeField] private AnimationCurve _ingredientSlideCurve;
        public CookingManager CookingManager => _cookingManager;

        [Header("GameFeel")]
        private bool _canChangeMealValues;

        private int _currentFinalHungerValue;
        private int _finalHungerValue;

        private int _currentFinalSatisfactionValue;
        private int _finalSatisfactionValue;

        private int _currentPowerValue;
        private int _finalPowerValue;

        [SerializeField] private float _animationDuration = 1f;
        private float _timer;
        [SerializeField] private AnimationCurve AnimationCurve;
        [SerializeField] private TMP_Text _finalMealName;
        [SerializeField] private Image _finalMealImage;
        [SerializeField] private Button _button;
        [SerializeField] private Button _rerollButton;

        [Header("Game Feel")]
        [SerializeField] private UnityEvent _onMealAppear;
        [SerializeField] private UnityEvent _onMealChange;
        [SerializeField] private UnityEvent _onMealDisappear;
        [SerializeField] private UnityEvent _goNextPhase;
        [SerializeField] private ReRoll _reroll;

        // Start is called before the first frame update
        private void Start()
        {
            _finalMealImage.enabled = false;
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                ResetIngredientStats(_ingredientStats[i]);
                _currentMeal = null;
            }
            UpdateFinalMealDisplay();
        }

        public void Update()
        {
            if (_canChangeMealValues)
            {
                _timer += Time.deltaTime;

                
                float progress = AnimationCurve.Evaluate(_timer / _animationDuration);

                
                _currentFinalHungerValue = (int)Mathf.Lerp(_currentFinalHungerValue, _finalHungerValue, progress);
                _currentFinalSatisfactionValue = (int)Mathf.Lerp(_currentFinalSatisfactionValue, _finalSatisfactionValue, progress);
                _currentPowerValue = (int)Mathf.Lerp(_currentPowerValue, _finalPowerValue, progress);

               
                UpdateFinalMealDisplay();

                
                if (_timer >= _animationDuration)
                {
                    _canChangeMealValues = false;
                }
            }
        }
    

        public void UpdateMenu()
        {
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                if (i < ClickUp.EnlargedSprites.Count)
                {
                    UpdateIngredient(i);
                }
                else
                {
                    ResetIngredientStats(_ingredientStats[i]);
                }
            }

            if (ClickUp.EnlargedSprites.Count == 3)
            {   
                if (_currentMeal == null) _onMealAppear?.Invoke();
                else if (_currentMeal != null) _onMealChange?.Invoke();
                _currentMeal = _cookingManager.SetCurrentMeal(_cookingManager.CreateMeal(ClickUp.EnlargedSprites[0].Ingredient,
                    ClickUp.EnlargedSprites[1].Ingredient, ClickUp.EnlargedSprites[2].Ingredient));
                //_finalMealImage.sprite = _currentMeal.Icon;
                ChangeFinalMealStats(_currentMeal.Stats.x, _currentMeal.Stats.y, _currentMeal.Stats.z);

                _cookingManager.GaugeManager.PrevisualizeMeal(_currentMeal);
            }
            else
            {
                _cookingManager.GaugeManager.RestartPrevGauges();
                _onMealDisappear?.Invoke();
                _finalMealImage.sprite = null;
                _finalMealImage.sprite = null;
                _currentMeal = null;
                _finalMealName.text = "";
                ChangeFinalMealStats(ClickUp.EnlargedSprites.Sum(ingredient => (int)ingredient.Ingredient.Stats.x), 
                        ClickUp.EnlargedSprites.Sum(ingredient => (int)ingredient.Ingredient.Stats.y), 
                    ClickUp.EnlargedSprites.Sum(ingredient => (int)ingredient.Ingredient.Stats.z));     
                /*_goPhase2Button.interactable = false;*/
            }
        }

        private void UpdateIngredient(int order)
        {
            _ingredientStats[order]._cardName.text = ClickUp.EnlargedSprites[order].Ingredient.name;
            if (ClickUp.EnlargedSprites[order].Ingredient.Stats.x > 0)
                _ingredientStats[order]._cardHunger.text = "+" + ClickUp.EnlargedSprites[order].Ingredient.Stats.x.ToString(CultureInfo.InvariantCulture);
            else
                _ingredientStats[order]._cardHunger.text = ClickUp.EnlargedSprites[order].Ingredient.Stats.x.ToString(CultureInfo.InvariantCulture);
            
            if (ClickUp.EnlargedSprites[order].Ingredient.Stats.y > 0)
                _ingredientStats[order]._cardSatisfaction.text = "+" + ClickUp.EnlargedSprites[order].Ingredient.Stats.y.ToString(CultureInfo.InvariantCulture);
            else
                _ingredientStats[order]._cardSatisfaction.text = ClickUp.EnlargedSprites[order].Ingredient.Stats.y.ToString(CultureInfo.InvariantCulture);

            if (ClickUp.EnlargedSprites[order].Ingredient.Stats.z > 0)
                _ingredientStats[order]._cardPower.text = "+" + ClickUp.EnlargedSprites[order].Ingredient.Stats.z.ToString(CultureInfo.InvariantCulture);
            else
                _ingredientStats[order]._cardPower.text = ClickUp.EnlargedSprites[order].Ingredient.Stats.z.ToString(CultureInfo.InvariantCulture);
            _ingredientStats[order]._cardImage.sprite = ClickUp.EnlargedSprites[order].Ingredient.Icon;
            _ingredientStats[order]._cardImage.enabled = true;
        }

        public void ConfirmMeal()
        {
            if (_currentMeal != null)
            {
                StartCoroutine(SlideIngredients());
                
                Debug.Log("Going to Phase2");
                _nextPhaseMealDisplay.UpdateDisplay(_currentMeal);
                _goNextPhase?.Invoke();
            }
            else
            {
                Debug.Log("No meal");
            }
        }
        
        private IEnumerator SlideIngredients()
        {
            _button.gameObject.SetActive(false);
            _rerollButton.gameObject.SetActive(false);

            List<Vector2> startPositions = new List<Vector2>(3)
            {
                _ingredientStats[0]._cardImage.transform.position,
                _ingredientStats[1]._cardImage.transform.position,
                _ingredientStats[2]._cardImage.transform.position
            };

            foreach (ClickUp card in _reroll.Cards)
            {
                card.Lock();
            }
            
            float timer = 0;
            while (timer < _slideTime)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < 3; i++)
                {
                    _ingredientStats[i]._cardImage.transform.position = Vector2.Lerp(startPositions[i], _finalMealImage.transform.position,
                        _ingredientSlideCurve.Evaluate(timer / _slideTime));
                }
                
                yield return new WaitForEndOfFrame();
            }
            
            for (int i = 0; i < 3; i++)
            {
                _ingredientStats[i]._cardImage.enabled = false;
            }

            _finalMealImage.enabled = true;
            _finalMealImage.gameObject.SetActive(true);
            _finalMealImage.sprite = _currentMeal.Icon;

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SlideUi());

            while (_cookingManager.GetCurrentPhase() != PhaseCode.Phase3)
            {
                yield return new WaitForFixedUpdate();
            }
            
            _button.gameObject.SetActive(true);
            _rerollButton.gameObject.SetActive(true);
            foreach (ClickUp card in _reroll.Cards)
            {
                card.Unlock();
            }
            yield return new WaitForSeconds(1);
            
            for (int i = 0; i < 3; i++)
            {
                _ingredientStats[i]._cardImage.transform.position = startPositions[i];
                _ingredientStats[i]._cardImage.enabled = true;
            }
        }

        private IEnumerator SlideMeal()
        {
            _camera.NextPhase();
            
            Vector2 startPos = _finalMealImage.transform.position;
            float timer = 0;
            while (timer < _slideTime)
            {
                timer += Time.deltaTime;
                _finalMealImage.transform.position = Vector2.Lerp(startPos, _nextPhaseMealDisplay.transform.position,
                    _mealSlideCurve.Evaluate(timer / _slideTime));
                yield return new WaitForEndOfFrame();
            }

            while (_cookingManager.GetCurrentPhase() != PhaseCode.Phase3)
            {
                yield return new WaitForFixedUpdate();
            }

            _finalMealImage.enabled = false;
            _finalMealImage.transform.position = startPos;
        }
        
        private IEnumerator SlideUi()
        {
            Vector2 startCauldronPos = _cauldronTransform.position;
            Vector2 startUiPos = _uiTransform.position;
            
            float timer = 0;
            float slideTime = 0.5f;
            while (timer < slideTime)
            {
                timer += Time.deltaTime;
                _uiTransform.position = Vector2.Lerp(startUiPos, startUiPos + Vector2.down * 20f,
                    _uiSlideCurve.Evaluate(timer / slideTime));
                _cauldronTransform.position = Vector2.Lerp(startCauldronPos, startCauldronPos + Vector2.up * 15f,
                    _uiSlideCurve.Evaluate(timer / slideTime));
                yield return new WaitForEndOfFrame();
            }
            
            StartCoroutine(SlideMeal());
            
            while (_cookingManager.GetCurrentPhase() != PhaseCode.Phase3)
            {
                yield return new WaitForFixedUpdate();
            }

            _uiTransform.position = startUiPos;
            _cauldronTransform.position = startCauldronPos;
        }

        private void ResetIngredientStats(IngredientStats ingredientStats)
        {
            ingredientStats._cardName.text = "";
            ingredientStats._cardHunger.text = "";
            ingredientStats._cardSatisfaction.text = "";
            ingredientStats._cardPower.text = "";
            ingredientStats._cardImage.sprite = null;
            ingredientStats._cardImage.enabled = false;
        }

        private void ChangeFinalMealStats(int hunger, int satisfaction, int power)
        {
            _finalHungerValue = hunger;
            _finalSatisfactionValue = satisfaction;
            _finalPowerValue = power;

            _timer = 0f;
            _canChangeMealValues = true;
        }
        private void ChangeFinalMealStats(string name, int hunger, int satisfaction, int power)
        {
            _finalMealName.text = name;
            _finalHungerValue = hunger;
            _finalSatisfactionValue = satisfaction;
            _finalPowerValue = power;

            _timer = 0f;
            _canChangeMealValues = true;
        }

        private void UpdateFinalMealDisplay()
        {
            if (_currentFinalHungerValue > 0)
                _finalHunger.text = "+" + _currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);
            else
                _finalHunger.text = _currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);

            if (_currentFinalSatisfactionValue > 0) 
                _finalSatisfaction.text = "+" + _currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);
            else 
                _finalSatisfaction.text = _currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);

            if (_currentPowerValue > 0)
                _finalPower.text = "+" + _currentPowerValue.ToString(CultureInfo.InvariantCulture);
            else
                _finalPower.text = _currentPowerValue.ToString(CultureInfo.InvariantCulture);

        }



        [Serializable]
        public struct IngredientStats
        {
            public TextMeshProUGUI _cardName;
            public TextMeshProUGUI _cardHunger;
            public TextMeshProUGUI _cardSatisfaction;
            public TextMeshProUGUI _cardPower;
            public Image _cardImage;
        }
    }
}