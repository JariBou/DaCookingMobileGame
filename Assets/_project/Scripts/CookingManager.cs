using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using UnityEngine;

namespace _project.Scripts
{
    public class CookingManager : MonoBehaviour
    {
        [SerializeField] private CookingMethodSo _cookingMethodSo;

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
        }
        
        public Meal CookMeal(Meal meal, CookingMethod cookingMethod)
        {
            return meal.CookMeal(_cookingMethodSo.GetMultiplier(cookingMethod));
        }

        public Meal AddCondiment(Meal meal)
        {
            // TODO
            return meal.AddCondiment();
        }
        
    }
}