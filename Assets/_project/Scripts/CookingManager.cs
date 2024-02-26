using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using UnityEngine;

namespace _project.Scripts
{
    public class CookingManager : MonoBehaviour
    {
        [SerializeField] private CookingParamsSo _cookingParamsSo;

        [SerializeField] private Meal _currentMeal;


        public Meal CreateMeal(IngredientSo ingredient1, IngredientSo ingredient2, IngredientSo ingredient3)
        {
            return new Meal(ingredient1, ingredient2, ingredient3);
        }

        public Meal SetCurrentMeal(Meal meal)
        {
            _currentMeal = meal;
            return meal;
        }

        public void FeedMeal(MonsterDataSo monsterDataSo)
        {
            // TODO: idk if monster current stats are going to stay on SO
            _currentMeal = null;
        }
        
        public Meal CookMeal(CookingMethod cookingMethod)
        {
            return _currentMeal?.CookMeal(_cookingParamsSo.GetMultiplier(cookingMethod));
        }
        
        public Meal CookMeal(Meal meal, CookingMethod cookingMethod)
        {
            return meal.CookMeal(_cookingParamsSo.GetMultiplier(cookingMethod));
        }
        
        public Meal AddCondiment()
        {
            // TODO
            return _currentMeal?.AddCondiment();
        }

        public Meal AddCondiment(Meal meal)
        {
            // TODO
            return meal.AddCondiment();
        }
        
    }
}