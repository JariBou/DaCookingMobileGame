using System;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using UnityEngine;

namespace _project.Scripts.Core
{
    [Serializable]
    public class Meal
    {
        [SerializeField] private Vector3 _stats;
        [SerializeField] private Vector3 _addedStats;
        [SerializeField] private List<IngredientSo> _ingredients = new(3);
        [SerializeField] private Sprite _icon;
        
        public Vector3 Stats => _stats;
        public Vector3 AddedStats => _addedStats;
        public List<IngredientSo> Ingredients => _ingredients;
        public Sprite Icon => _icon;

        public Meal(Meal baseMeal) : this(baseMeal.Ingredients[0], baseMeal.Ingredients[1], baseMeal.Ingredients[2])
        {
            _icon = baseMeal.Icon ? baseMeal.Icon : null;
        }

        public Meal(IngredientSo ingredient1, IngredientSo ingredient2, IngredientSo ingredient3)
        {
            _stats = ingredient1.Stats + ingredient2.Stats + ingredient3.Stats;
            _addedStats = ingredient1.RandomAddedstats + ingredient2.RandomAddedstats + ingredient3.RandomAddedstats;
            
            _ingredients.Add(ingredient1);
            _ingredients.Add(ingredient2);
            _ingredients.Add(ingredient3);
        }

        public List<IngredientFamily> GetIngredientsFamilies()
        {
            List<IngredientFamily> list = new List<IngredientFamily>(3);
            
            foreach (IngredientSo ingredient in _ingredients)
            {
                list.Add(ingredient.Family);
            }

            return list;
        }

        public Meal CookMeal(Vector3 multiplier)
        {
            _stats.x *= multiplier.x;
            _stats.y *= multiplier.y;
            _stats.z *= multiplier.z;
            
            //TODO does multiplier affect random added stats?
            
            return this;
        }

        public Meal AddCondiment(CondimentSo condimentSo)
        {
            _stats += condimentSo.Value;
            
            return this;
        }

        public Meal CreateIcon(CookingParamsSo cookingParamsSo)
        {
            _icon = cookingParamsSo.GetMealIcon(this);
            return this;
        }
    }

    [Serializable]
    public class MealBaseInfo
    {
        [SerializeField] private IngredientFamily _ingredientFamilyA;
        [SerializeField] private IngredientFamily _ingredientFamilyB;
        [SerializeField] private IngredientFamily _ingredientFamilyC;

        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;

        public IngredientFamily IngredientFamilyA => _ingredientFamilyA;
        public IngredientFamily IngredientFamilyB => _ingredientFamilyB;
        public IngredientFamily IngredientFamilyC => _ingredientFamilyC;

        public List<IngredientFamily> IngredientFamilies => new()
            { _ingredientFamilyA, _ingredientFamilyB, _ingredientFamilyC };
        public Sprite Icon => _icon;

        public string Name => _name;
    }
}