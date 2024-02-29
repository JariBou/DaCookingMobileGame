using System;
using System.Collections.Generic;
using System.Linq;
using _project.Scripts.Core;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Editable, CreateAssetMenu(menuName = "ScriptableObjects/CookingMethodSo")]
    public class CookingParamsSo : ScriptableObject
    {

        [TabProperty(nameof(CookingMethod.Method1)), SerializeField, ReadOnly] private CookingMethod _cookingMethod1 = CookingMethod.Method1;
        [TabProperty(nameof(CookingMethod.Method1)), SerializeField] private Vector3 _method1StatsMultiplier;
        
        [TabProperty(nameof(CookingMethod.Method2)), SerializeField, ReadOnly] private CookingMethod _cookingMethod2 = CookingMethod.Method2;
        [TabProperty(nameof(CookingMethod.Method2)), SerializeField] private Vector3 _method2StatsMultiplier;
        
        [TabProperty(nameof(CookingMethod.Method3)), SerializeField, ReadOnly] private CookingMethod _cookingMethod3 = CookingMethod.Method3;
        [TabProperty(nameof(CookingMethod.Method3)), SerializeField] private Vector3 _method3StatsMultiplier;

        [SerializeField] private List<MealBaseInfo> _mealIcons;

        public List<MealBaseInfo> MealIcons => _mealIcons;


        public Vector3 GetMultiplier(CookingMethod cookingMethod)
        {
            switch (cookingMethod)
            {
                case CookingMethod.Method1:
                    return _method1StatsMultiplier;
                case CookingMethod.Method2:
                    return _method2StatsMultiplier;
                case CookingMethod.Method3:
                    return _method3StatsMultiplier;
                default:
                    Debug.LogWarning("You should add that method in here first");
                    throw new ArgumentOutOfRangeException(nameof(cookingMethod), cookingMethod, null);
            }
        }

        public Sprite GetMealIcon(Meal meal)
        {
            foreach (MealBaseInfo mealIcon in _mealIcons.Where(mealIcon => Utils.ListHasAllElements(meal.GetIngredientsFamilies(), mealIcon.IngredientFamilies)))
            {
                return mealIcon.Icon;
            }

            throw new ArgumentException();
        }

    }
}