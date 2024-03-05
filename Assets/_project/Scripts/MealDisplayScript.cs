using System.Globalization;
using _project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class MealDisplayScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text _mealName;
        [SerializeField] private TMP_Text _mealStatX;
        [SerializeField] private TMP_Text _mealStatY;
        [SerializeField] private TMP_Text _mealStatZ;
        [SerializeField] private Image _mealImage;

        [Header("GameFeel")]
        private bool _canChangeMealValues;

        private int _currentFinalHungerValue;
        private int _finalHungerValue;

        private int _currentFinalSatisfactionValue;
        private int _finalSatisfactionValue;

        private int _currentPowerValue;
        private int _finalPowerValue;

        [SerializeField] private float _animationDuration = 1f;
        private float _timer;
        [SerializeField] private AnimationCurve AnimationCurve;

        public void UpdateDisplay(Meal meal)
        {
            if (_mealName) _mealName.text = meal.Name ?? "";
            _mealStatX.text = (meal.Stats.x >= 0 ? "+" : "") + meal.Stats.x.ToString(CultureInfo.InvariantCulture);
            _mealStatY.text = (meal.Stats.y >= 0 ? "+" : "") + meal.Stats.y.ToString(CultureInfo.InvariantCulture);
            _mealStatZ.text = (meal.Stats.z >= 0 ? "+" : "") + meal.Stats.z.ToString(CultureInfo.InvariantCulture);
            _mealImage.sprite = meal.Icon;

            if (_canChangeMealValues)
            {
                _timer += Time.deltaTime;


                float progress = AnimationCurve.Evaluate(_timer / _animationDuration);


                _currentFinalHungerValue = (int)Mathf.Lerp(_currentFinalHungerValue, _finalHungerValue, progress);
                _currentFinalSatisfactionValue = (int)Mathf.Lerp(_currentFinalSatisfactionValue, _finalSatisfactionValue, progress);
                _currentPowerValue = (int)Mathf.Lerp(_currentPowerValue, _finalPowerValue, progress);


                UpdateFinalMealDisplay();


                if (_timer >= _animationDuration)
                {
                    _canChangeMealValues = false;
                }
            }
        }
        
        public void ResetDisplay()
        {
            if (_mealName) _mealName.text = "";
            _mealStatX.text = "0";
            _mealStatY.text = "0";
            _mealStatZ.text = "0";
            _mealImage.sprite = null;
        }

        private void ChangeFinalMealStats(int hunger, int satisfaction, int power)
        {
            _finalHungerValue = hunger;
            _finalSatisfactionValue = satisfaction;
            _finalPowerValue = power;

            _timer = 0f;
            _canChangeMealValues = true;
        }

        private void UpdateFinalMealDisplay()
        {
            if (_currentFinalHungerValue > 0)
                _mealStatX.text = "+" + _currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);
            else
                _mealStatX.text = _currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);

            if (_currentFinalSatisfactionValue > 0)
                _mealStatY.text = "+" + _currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);
            else
                _mealStatY.text = _currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);

            if (_currentPowerValue > 0)
                _mealStatZ.text = "+" + _currentPowerValue.ToString(CultureInfo.InvariantCulture);
            else
                _mealStatZ.text = _currentPowerValue.ToString(CultureInfo.InvariantCulture);

        }
    }
}
