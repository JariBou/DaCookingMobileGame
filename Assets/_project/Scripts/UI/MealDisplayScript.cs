using System.Globalization;
using _project.Scripts.Meals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class MealDisplayScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text _mealName;
        [SerializeField] private TMP_Text _mealStatX;
        [SerializeField] private TMP_Text _mealStatY;
        [SerializeField] private TMP_Text _mealStatZ;
        [SerializeField] private Image _mealImage;

        private bool _canChangeMealValues;
        private float _timer;
        [SerializeField] private float _animationDuration;
        [SerializeField] private AnimationCurve AnimationCurve;

        private int _finalHungerValue;
        private int _finalSatisfactionValue;
        private int _finalPowerValue;

        private int _currentFinalHungerValue;
        private int _currentFinalSatisfactionValue;
        private int _currentPowerValue;

        private Meal _currentMeal;

        private void Update()
        {
            if (_canChangeMealValues)
            {
                _timer += Time.deltaTime;


                float progress = AnimationCurve.Evaluate(_timer / _animationDuration);


                _currentFinalHungerValue = (int)Mathf.Lerp(_currentFinalHungerValue, _finalHungerValue, progress);
                _currentFinalSatisfactionValue = (int)Mathf.Lerp(_currentFinalSatisfactionValue, _finalSatisfactionValue, progress);
                _currentPowerValue = (int)Mathf.Lerp(_currentPowerValue, _finalPowerValue, progress);


                UpdateFinalMealDisplay(_currentMeal);


                if (_timer >= _animationDuration)
                {
                    _canChangeMealValues = false;
                    _timer = 0;
                }
            }
        }

        private void UpdateFinalMealDisplay(Meal meal)
        {
            _mealStatX.text = (meal.Stats.x > 0 ? "+" : "") +_currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);
            _mealStatY.text = (meal.Stats.y > 0 ? "+" : "") +_currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);
            _mealStatZ.text = (meal.Stats.z > 0 ? "+" : "") +_currentPowerValue.ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateDisplay(Meal meal)
        {
            _timer = 0;
            _currentMeal = meal;
            if (_mealName) _mealName.text = meal.Name ?? "";
            _finalHungerValue = meal.Stats.x;
            _finalSatisfactionValue = meal.Stats.y;
            _finalPowerValue = meal.Stats.z;
            if (_mealImage) _mealImage.enabled = true;
            if (_mealImage) _mealImage.sprite = meal.Icon;
            _canChangeMealValues = true;
        }
        
        public void ResetDisplay()
        {
            if (_mealName) _mealName.text = "";
            _mealStatX.text = "0";
            _mealStatY.text = "0";
            _mealStatZ.text = "0";
            if (_mealImage) _mealImage.sprite = null;
            if (_mealImage) _mealImage.enabled = false;
            _canChangeMealValues = true;
        }
    }
}
