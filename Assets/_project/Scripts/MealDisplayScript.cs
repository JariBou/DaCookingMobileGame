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
        [SerializeField] private TMP_Text _finalHunger;
        [SerializeField] private TMP_Text _finalSatisfaction;
        [SerializeField] private TMP_Text _finalPower;
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
            _finalHunger.text = (meal.Stats.x >= 0 ? "+" : "") + meal.Stats.x.ToString(CultureInfo.InvariantCulture);
            _finalSatisfaction.text = (meal.Stats.y >= 0 ? "+" : "") + meal.Stats.y.ToString(CultureInfo.InvariantCulture);
            _finalPower.text = (meal.Stats.z >= 0 ? "+" : "") + meal.Stats.z.ToString(CultureInfo.InvariantCulture);
            _mealImage.sprite = meal.Icon;
          
        }

        public void Update()
        {
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
                _finalHunger.text = "+" + _currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);
            else
                _finalHunger.text = _currentFinalHungerValue.ToString(CultureInfo.InvariantCulture);

            if (_currentFinalSatisfactionValue > 0)
                _finalSatisfaction.text = "+" + _currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);
            else
                _finalSatisfaction.text = _currentFinalSatisfactionValue.ToString(CultureInfo.InvariantCulture);

            if (_currentPowerValue > 0)
                _finalPower.text = "+" + _currentPowerValue.ToString(CultureInfo.InvariantCulture);
            else
                _finalPower.text = _currentPowerValue.ToString(CultureInfo.InvariantCulture);

        }

        public void ResetDisplay()
        {
            if (_mealName) _mealName.text = "";
            _finalHunger.text = "0";
            _finalSatisfaction.text = "0";
            _finalPower.text = "0";
            _mealImage.sprite = null;
        }      
     
    }
}
