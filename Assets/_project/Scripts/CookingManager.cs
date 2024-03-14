using System;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using _project.Scripts.Gauges;
using _project.Scripts.Meals;
using _project.Scripts.UI;
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
        private DialogMenuScript _dialogMenuScript;
        
        public CameraScript Camera => _camera;
        public GaugeHandler GaugeManager => _gaugeGaugeManager;

        public PhaseCode GetCurrentPhase() => (PhaseCode)_camera.CurrentIndex;

        private void Start()
        {
            _dialogMenuScript = GetComponent<DialogMenuScript>();
        }

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
            bool result = _monsterInstance.FeedMeal(_currentMeal);
            _gaugeGaugeManager.UpdateGauges();
            _currentMeal = null;
            Debug.Log($"Result: {result}");
            if (result)
            {
                _gaugeGaugeManager.HasWin = true;
                _gaugeGaugeManager.HasToInvokeWinPanel = true;
            }
            else if (_monsterInstance.NumberOfMeals > _monsterInstance.MaxNumberOfMeals && !result)
            {
                _gaugeGaugeManager.HasWin = false;
                _gaugeGaugeManager.HasToInvokeWinPanel = true;
            }
            return result;
        }

        public void InvokeWinPanel(bool hasWin)
        {
            _dialogMenuScript.ActivateMenu(hasWin);
        }
/*        public void WinPanel()
        {
            _dialogMenuScript.ActivateMenu(true);
        }

        public void LosePanel()
        {
            _dialogMenuScript.ActivateMenu(false);
        }*/
        
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