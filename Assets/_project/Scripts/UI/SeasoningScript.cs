using System;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using _project.Scripts.Gauges;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _project.Scripts.UI
{
    public class SeasoningScript : MonoBehaviour, IDraggable
    {
        /*    [SerializeField] private SeasoningType _seasoningType;*/
        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI _hungerStats;
        [SerializeField] private TextMeshProUGUI _satisfactionStats;
        [SerializeField] private TextMeshProUGUI _powerStats;

        [Header("Quantity")]    
        [SerializeField] private TextMeshProUGUI _quantity;
        [SerializeField] private int _maxQuantity;

        [Header("Condiment")]
        [SerializeField] private CondimentSo _condimentSo;
        private Vector3 _initialPosition;

        [Header("Particles")]
        public ParticleSystem ParticleSystem;

        [Header("GameFeel")]
        [SerializeField] private UnityEvent _OnCondimentsClick;

        [Header("References")]
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private MealDisplayScript _mealDisplayScript;
        [SerializeField] private GaugeHandler _gaugeHandler;
        private bool _usable;
        public Vector3 InitialPosition => _initialPosition;

        public static event Action SeasoningAdded;

    
     
        void Start()
        {
            _initialPosition = transform.position;
            ParticleSystem.Stop();
        }

        private void OnMouseDown()
        {
            if (_OnCondimentsClick != null)
            {
                _OnCondimentsClick.Invoke();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetStats(CondimentSo condiment)
        {
            _hungerStats.text = condiment.Value.x.ToString();
            _satisfactionStats.text = condiment.Value.y.ToString();
            _powerStats.text = condiment.Value.z.ToString();
        }

        private void UpdateQuantity()
        {
            _quantity.text = _maxQuantity.ToString();
        }

        public void AddSeosoning(int sign)
        {
            /*Debug.Log("Add seasoning");*/
            if (_maxQuantity <= 0) return;
            _cookingManager.AddCondiment(_condimentSo, sign);
            _mealDisplayScript.UpdateDisplay(_cookingManager.GetCurrentMeal());
            _maxQuantity--;
            UpdateQuantity();
            _gaugeHandler.PrevisualizeMeal(_cookingManager.GetCurrentMeal());
        }

        public void AddSeosoning()
        {
            /*Debug.Log("Add seasoning");*/
            if (_maxQuantity <= 0) return;
            _cookingManager.AddCondiment(_condimentSo);
            _mealDisplayScript.UpdateDisplay(_cookingManager.GetCurrentMeal());
            _maxQuantity--;
            UpdateQuantity();
            SeasoningAdded?.Invoke();
            _gaugeHandler.PrevisualizeMeal(_cookingManager.GetCurrentMeal());
        }

        private void OnValidate()
        {
            UpdateQuantity();
            SetStats(_condimentSo);
        }
    
        public void EnableUse()
        {
            _usable = true;
        }

        public void DisableUse()
        {
            _usable = false;
        }

        public bool IsActive()
        {
            return _usable;
        }
    }
}

/*[Serializable]
public enum SeasoningType
{
    Hunger,
    Satisfaction,
    Power
}
*/