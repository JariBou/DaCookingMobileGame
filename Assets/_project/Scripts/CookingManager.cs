using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts
{
    public class CookingManager : MonoBehaviour
    {
        [SerializeField] private CookingParamsSo _cookingParamsSo;
        [SerializeField] private Meal _currentMeal;
        [SerializeField] private CameraScript _camera;
        [SerializeField] private MonsterInstance _monsterInstance;
        [FormerlySerializedAs("_gaugeHandler")] [SerializeField] private GaugeHandler _gaugeGaugeManager;
        
        public CameraScript Camera => _camera;
        public GaugeHandler GaugeManager => _gaugeGaugeManager;

        public PhaseCode GetCurrentPhase() => (PhaseCode)_camera.CurrentIndex;

        public Meal CreateMeal(IngredientSo ingredient1, IngredientSo ingredient2, IngredientSo ingredient3)
        {
            return new Meal(ingredient1, ingredient2, ingredient3).Initialize(_cookingParamsSo);
        }

        public Meal SetCurrentMeal(Meal meal)
        {
            _currentMeal = meal;
            return meal;
        }

        /// <summary>
        /// Returns true if meal satisfied the monster
        /// </summary>
        /// <param name="monsterInstance"></param>
        /// <returns></returns>
        public bool FeedMeal()
        {
            Debug.Log("Feeding Boss");
            bool result = _monsterInstance.FeedMeal(_currentMeal);
            _gaugeGaugeManager.NewPhase();
            _currentMeal = null;
            Debug.Log($"Result: {result}");
            return result;
        }
        
        public Meal CookMeal(CookingMethod cookingMethod)
        {
            return _currentMeal?.CookMeal(_cookingParamsSo.GetMultiplier(cookingMethod));
        }
        
        public Meal CookMeal(Meal meal, CookingMethod cookingMethod)
        {
            return meal.CookMeal(_cookingParamsSo.GetMultiplier(cookingMethod));
        }
        
        public Meal AddCondiment(CondimentSo condimentSo)
        {
            // TODO add *1 or *-1 multiplier
            return _currentMeal?.AddCondiment(condimentSo);
        }

        public Meal AddCondiment(CondimentSo condimentSO , int sign)
        {
            // TODO add *1 or *-1 multiplier
            return _currentMeal?.AddCondiment(condimentSO, sign);
        }

        public Meal AddCondiment(Meal meal, CondimentSo condimentSo)
        {
            // TODO add *1 or *-1 multiplier
            return meal.AddCondiment(condimentSo);
        }

        public Meal GetCurrentMeal()
        {
            return _currentMeal;
        }
    }
}