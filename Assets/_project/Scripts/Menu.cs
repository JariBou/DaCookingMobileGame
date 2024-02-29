using System;
using System.Globalization;
using System.Linq;
using _project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private IngredientStats[] _ingredientStats;
        [SerializeField] private TextMeshProUGUI _finalHunger;
        [SerializeField] private TextMeshProUGUI _finalSatisfaction;
        [SerializeField] private TextMeshProUGUI _finalPower;
        [SerializeField] private Image _finalMealImage;
        [SerializeField] private Button _goPhase2Button;

        // Start is called before the first frame update
        private void Start()
        {
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                _ingredientStats[i]._cardName.text = "";
                _ingredientStats[i]._cardHunger.text = "";
                _ingredientStats[i]._cardSatisfaction.text = "";
                _ingredientStats[i]._cardPower.text = "";
                _ingredientStats[i]._cardImage.sprite = null;
            }
        }

        public void UpdateMenu()
        {
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                if (i < ClickUp._enlargedSprites.Count)
                {
                    UpdateIngredient(i);
                }
                else
                {
                    _ingredientStats[i]._cardName.text = "";
                    _ingredientStats[i]._cardHunger.text = "";
                    _ingredientStats[i]._cardSatisfaction.text = "";
                    _ingredientStats[i]._cardPower.text = "";
                    _ingredientStats[i]._cardImage.sprite = null;
                    _finalMealImage.sprite = null;
                }
            }

            if (ClickUp._enlargedSprites.Count == 3)
            {
                Meal meal = _cookingManager.SetCurrentMeal(_cookingManager.CreateMeal(ClickUp._enlargedSprites[0].Ingredient,
                    ClickUp._enlargedSprites[1].Ingredient, ClickUp._enlargedSprites[2].Ingredient));
                _finalMealImage.sprite = meal.Icon;
                
                _finalHunger.text = (meal.Stats.x > 0 ? "+" : "") + meal.Stats.x.ToString(CultureInfo.InvariantCulture);

                _finalSatisfaction.text = (meal.Stats.y > 0 ? "+" : "") + meal.Stats.y.ToString(CultureInfo.InvariantCulture);

                _finalPower.text = (meal.Stats.z > 0 ? "+" : "") + meal.Stats.z.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                if (ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.x) > 0)
                    _finalHunger.text = "+" + ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.x).ToString(CultureInfo.InvariantCulture);
                else
                    _finalHunger.text = ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.x).ToString(CultureInfo.InvariantCulture);

                if (ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.y) > 0)
                    _finalSatisfaction.text = "+" + ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.y).ToString(CultureInfo.InvariantCulture);
                else
                    _finalSatisfaction.text = ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.y).ToString(CultureInfo.InvariantCulture);

                if (ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.z) > 0)
                    _finalPower.text = "+" + ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.z).ToString(CultureInfo.InvariantCulture);
                else
                    _finalPower.text = ClickUp._enlargedSprites.Sum(x => x.Ingredient.Stats.z).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void UpdateIngredient(int order)
        {
            _ingredientStats[order]._cardName.text = ClickUp._enlargedSprites[order].Ingredient.name;
            if (ClickUp._enlargedSprites[order].Ingredient.Stats.x > 0)
                _ingredientStats[order]._cardHunger.text = "+" + ClickUp._enlargedSprites[order].Ingredient.Stats.x.ToString(CultureInfo.InvariantCulture);
            else
                _ingredientStats[order]._cardHunger.text = ClickUp._enlargedSprites[order].Ingredient.Stats.x.ToString(CultureInfo.InvariantCulture);


            if (ClickUp._enlargedSprites[order].Ingredient.Stats.y > 0)
                _ingredientStats[order]._cardSatisfaction.text = "+" + ClickUp._enlargedSprites[order].Ingredient.Stats.y.ToString(CultureInfo.InvariantCulture);
            else
                _ingredientStats[order]._cardSatisfaction.text = ClickUp._enlargedSprites[order].Ingredient.Stats.y.ToString(CultureInfo.InvariantCulture);

            if (ClickUp._enlargedSprites[order].Ingredient.Stats.z > 0)
                _ingredientStats[order]._cardPower.text = "+" + ClickUp._enlargedSprites[order].Ingredient.Stats.z.ToString(CultureInfo.InvariantCulture);
            else
                _ingredientStats[order]._cardPower.text = ClickUp._enlargedSprites[order].Ingredient.Stats.z.ToString(CultureInfo.InvariantCulture);
            _ingredientStats[order]._cardImage.sprite = ClickUp._enlargedSprites[order].Ingredient.Icon;
        }

        public void ConfirmMeal()
        {
            // TODO
        }
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