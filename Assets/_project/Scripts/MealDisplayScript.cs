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


        public void UpdateDisplay(Meal meal)
        {
            if (_mealName) _mealName.text = meal.Name ?? "";
            _mealStatX.text = (meal.Stats.x >= 0 ? "+" : "") + meal.Stats.x.ToString(CultureInfo.InvariantCulture);
            _mealStatY.text = (meal.Stats.y >= 0 ? "+" : "") + meal.Stats.y.ToString(CultureInfo.InvariantCulture);
            _mealStatZ.text = (meal.Stats.z >= 0 ? "+" : "") + meal.Stats.z.ToString(CultureInfo.InvariantCulture);
            _mealImage.sprite = meal.Icon;
        }
        
        public void ResetDisplay()
        {
            if (_mealName) _mealName.text = "";
            _mealStatX.text = "0";
            _mealStatY.text = "0";
            _mealStatZ.text = "0";
            _mealImage.sprite = null;
        }
    }
}
