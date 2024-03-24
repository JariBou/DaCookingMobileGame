using System;
using System.Collections;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using _project.Scripts.Meals;
using _project.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

namespace _project.Scripts.Phases
{
    public class CookingPhaseScript : MonoBehaviour
    {
        [SerializeField] private MealDisplayScript _resultMealDisplayScript;
        [SerializeField] private CookingParamsSo _cookingParams;
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private MealDisplayScript _nextPhaseMealDisplayScript;
        [SerializeField, Tooltip("Reference images in order")] private List<GameObject> _hovenImages;
        [SerializeField, Tooltip("Reference colors in order")] private List<Color> _particlesColors;
        [SerializeField] private ParticleSystem _particles;
        [Header("Button")]
        [FormerlySerializedAs("_button")] [SerializeField] private Button _nextButton;
        [SerializeField] private List<Sprite> _buttonSprites;

        public CookingMethod SelectedCookingMethod { get; set; }

        public CookingParamsSo CookingParams => _cookingParams;
        public CookingManager CookingManager => _cookingManager;
        
        public static event Action CookingMethodConfirm; 


        private void Start()
        {
            SelectedCookingMethod = CookingMethod.Null;
        }

        public void UpdateMealDisplay()
        {
            _resultMealDisplayScript.gameObject.SetActive(true);
            Meal tempMeal =
                new Meal(_cookingManager.GetCurrentMeal()).CookMeal(
                    _cookingParams, SelectedCookingMethod);
            _resultMealDisplayScript.UpdateDisplay(tempMeal);
            _cookingManager.GaugeManager.PrevisualizeMeal(tempMeal);

            for (int i = 0; i < _hovenImages.Count; i++)
            {
                _hovenImages[i].SetActive(false);
            }

            EmissionModule emission = _particles.emission;
            Burst[] bursts = new Burst[emission.burstCount];
            int burstCount = emission.GetBursts(bursts);

            switch (SelectedCookingMethod)
            {
                case CookingMethod.Method1:
                    _particles?.Stop();
                    if (burstCount > 0) bursts[0].count = 250; // Supposons qu'il y ait au moins un burst � modifier
                    emission.SetBursts(bursts, burstCount);
                    _particles.startColor = _particlesColors[0];
                    _particles?.Play();
                    _hovenImages[0].SetActive(true);
                    _nextButton.image.sprite = _buttonSprites[1];
                    break;
                case CookingMethod.Method2:
                    _particles?.Stop();
                    if (burstCount > 0) bursts[0].count = 500;
                    emission.SetBursts(bursts, burstCount);
                    _particles.startColor = _particlesColors[1];
                    _particles?.Play();
                    _hovenImages[1].SetActive(true);
                    _nextButton.image.sprite = _buttonSprites[1];
                    break;
                case CookingMethod.Method3:
                    _particles?.Stop();
                    if (burstCount > 0) bursts[0].count = 1000;
                    emission.SetBursts(bursts, burstCount);
                    _particles.startColor = _particlesColors[2];
                    _particles?.Play();
                    _hovenImages[2].SetActive(true);
                    _nextButton.image.sprite = _buttonSprites[1];
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
            CookingMethodConfirm?.Invoke();
            _nextPhaseMealDisplayScript.UpdateDisplay(_cookingManager.GetCurrentMeal());
            StartCoroutine(ResetCooking());
        }

        private IEnumerator ResetCooking()
        {
            _nextButton.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            _nextButton.gameObject.SetActive(true);
            SelectedCookingMethod = CookingMethod.Null;
            _nextButton.image.sprite = _buttonSprites[0];
            _resultMealDisplayScript.ResetDisplay();
        }
    }
}
