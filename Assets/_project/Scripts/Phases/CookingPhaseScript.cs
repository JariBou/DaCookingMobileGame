using System;
using System.Collections;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using _project.Scripts.Meals;
using _project.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Phases
{
    public class CookingPhaseScript : MonoBehaviour
    {
        [SerializeField] private MealDisplayScript _resultMealDisplayScript;
        [SerializeField] private CookingParamsSo _cookingParams;
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private MealDisplayScript _nextPhaseMealDisplayScript;
        [SerializeField] private Button _button;
        [SerializeField, Tooltip("Reference images in order")] private List<GameObject> _hovenImages;

        public CookingMethod SelectedCookingMethod { get; set; }

        public CookingParamsSo CookingParams => _cookingParams;
        public CookingManager CookingManager => _cookingManager;

        private void Start()
        {
            SelectedCookingMethod = CookingMethod.Null;
        }

        public void UpdateMealDisplay()
        {
            _resultMealDisplayScript.gameObject.SetActive(true);
            Meal tempMeal =
                new Meal(_cookingManager.GetCurrentMeal()).CookMeal(
                    _cookingParams.GetMultiplier(SelectedCookingMethod));
            _resultMealDisplayScript.UpdateDisplay(tempMeal);
            _cookingManager.GaugeManager.PrevisualizeMeal(tempMeal);

            for (int i = 0; i < _hovenImages.Count; i++)
            {
                _hovenImages[i].SetActive(false);
            }

            switch (SelectedCookingMethod)
            {
                case CookingMethod.Method1:
                    _hovenImages[0].SetActive(true);
                    break;
                case CookingMethod.Method2:
                    _hovenImages[1].SetActive(true);
                    break;
                case CookingMethod.Method3:
                    _hovenImages[2].SetActive(true);
                    break;
                case CookingMethod.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void GoToNextPhase()
        {
            if (SelectedCookingMethod == CookingMethod.Null) return;
            
            _cookingManager.CookMeal(SelectedCookingMethod);
            _cookingManager.Camera.NextPhase();
            _nextPhaseMealDisplayScript.UpdateDisplay(_cookingManager.GetCurrentMeal());
            StartCoroutine(ResetCooking());
        }

        private IEnumerator ResetCooking()
        {
            _button.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            _button.gameObject.SetActive(true);
            SelectedCookingMethod = CookingMethod.Null;
            _resultMealDisplayScript.ResetDisplay();
        }
    }
}
