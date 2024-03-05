using System;
using System.Globalization;
using System.Linq;
using _project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class RecipeDisplayScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private IngredientStats[] _ingredientStats;
        [SerializeField] private TextMeshProUGUI _finalHunger;
        [SerializeField] private TextMeshProUGUI _finalSatisfaction;
        [SerializeField] private TextMeshProUGUI _finalPower;
        [SerializeField] private Image _finalMealImage;
        [SerializeField] private TextMeshProUGUI _finalMealName;
        [SerializeField] private CameraScript _camera;
        [SerializeField] private MealDisplayScript _nextPhaseMealDisplay;
        private Meal _currentMeal;
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

        // Start is called before the first frame update
        private void Start()
        {
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                ResetIngredientStats(_ingredientStats[i]);
                _finalMealImage.sprite = null;
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

                    _cookingManager.GaugeManager.RestartPrevGauges();
                    _finalMealImage.sprite = null;
                    _currentMeal = null;
                }
            }

            if (ClickUp.EnlargedSprites.Count == 3)
            {
                _currentMeal = _cookingManager.SetCurrentMeal(_cookingManager.CreateMeal(ClickUp.EnlargedSprites[0].Ingredient,
                    ClickUp.EnlargedSprites[1].Ingredient, ClickUp.EnlargedSprites[2].Ingredient));
                _finalMealImage.sprite = _currentMeal.Icon;
                ChangeFinalMealStats(_currentMeal.Stats.x, _currentMeal.Stats.y, _currentMeal.Stats.z);

                _cookingManager.GaugeManager.PrevisualizeMeal(_currentMeal);
            }
            else
            {
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
        }

        public void ConfirmMeal()
        {
            if (_currentMeal != null)
            {
                Debug.Log("Going to Phase2");
                _camera.NextPhase();
                _nextPhaseMealDisplay.UpdateDisplay(_currentMeal);
            }
            else
            {
                Debug.Log("No meal");
            }
        }

        private void ResetIngredientStats(IngredientStats ingredientStats)
        {
            ingredientStats._cardName.text = "";
            ingredientStats._cardHunger.text = "";
            ingredientStats._cardSatisfaction.text = "";
            ingredientStats._cardPower.text = "";
            ingredientStats._cardImage.sprite = null;
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