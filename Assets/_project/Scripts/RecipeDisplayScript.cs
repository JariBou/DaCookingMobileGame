using System;
using System.Globalization;
using System.Linq;
using _project.Scripts.Core;
using TMPro;
using UnityEngine;
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

        private float _animationDuration = 1f;
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
        }

        public void Update()
        {
            if (_canChangeMealValues)
            {
                if (_currentFinalHungerValue != _finalHungerValue || _currentFinalSatisfactionValue != _finalSatisfactionValue || _currentPowerValue != _finalPowerValue)
                {
                    ChangeFinalMealString(_finalHungerValue, _finalSatisfactionValue, _finalPowerValue);
                }
                if (_currentFinalHungerValue == _finalHungerValue && _currentFinalSatisfactionValue == _finalSatisfactionValue && _currentPowerValue == _finalPowerValue)
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

                _finalHungerValue = _currentMeal.Stats.x;
                _finalSatisfactionValue = _currentMeal.Stats.y;
                _finalPowerValue = _currentMeal.Stats.z;

                _cookingManager.GaugeManager.PrevisualizeMeal(_currentMeal);
            }
            else
            {
                _finalHungerValue= ClickUp.EnlargedSprites.Sum(ingredient => (int) ingredient.Ingredient.Stats.x);
                _finalSatisfactionValue = ClickUp.EnlargedSprites.Sum(ingredient => (int) ingredient.Ingredient.Stats.y);
                _finalPowerValue = ClickUp.EnlargedSprites.Sum(ingredient => (int) ingredient.Ingredient.Stats.z);
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

        private void ChangeFinalMealString(float hunger, float satisfaction, float power)
        {
            if (hunger > 0)
                _finalHunger.text = "+" + hunger.ToString(CultureInfo.InvariantCulture);
            else
                _finalHunger.text = hunger.ToString(CultureInfo.InvariantCulture);
            if (satisfaction > 0)
                _finalSatisfaction.text = "+" + satisfaction.ToString(CultureInfo.InvariantCulture);
            else
                _finalSatisfaction.text = satisfaction.ToString(CultureInfo.InvariantCulture);
            if (power > 0)
                _finalPower.text = "+" + power.ToString(CultureInfo.InvariantCulture);
            else
                _finalPower.text = power.ToString(CultureInfo.InvariantCulture);

        }

        private void ChangeFinalMealString(string name,float hunger, float satisfaction, float power)
        {
            _finalMealName.text = name;

            if (hunger > 0)
                _finalHunger.text = "+" + hunger.ToString(CultureInfo.InvariantCulture);
            else
                _finalHunger.text = hunger.ToString(CultureInfo.InvariantCulture);
            if (satisfaction > 0)
                _finalSatisfaction.text = "+" + satisfaction.ToString(CultureInfo.InvariantCulture);
            else
                _finalSatisfaction.text = satisfaction.ToString(CultureInfo.InvariantCulture);
            if (power > 0)
                _finalPower.text = "+" + power.ToString(CultureInfo.InvariantCulture);
            else
                _finalPower.text = power.ToString(CultureInfo.InvariantCulture);

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