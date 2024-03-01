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
/*            _mealName.text = "dd";*/
            _mealStatX.text = meal.Stats.x.ToString(CultureInfo.InvariantCulture);
            _mealStatY.text = meal.Stats.y.ToString(CultureInfo.InvariantCulture);
            _mealStatZ.text = meal.Stats.z.ToString(CultureInfo.InvariantCulture);
            _mealImage.sprite = meal.Icon;
        }
    }
}
