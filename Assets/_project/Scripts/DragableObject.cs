using _project.ScriptableObjects.Scripts;
using _project.Scripts;
using _project.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    [SerializeField] private SeasoningType _seasoningType;
    [SerializeField] private TextMeshProUGUI _stats;
    [SerializeField] private TextMeshProUGUI _quantity;
    [SerializeField] private CondimentSo _condimentSo;
    private Vector3 _initialPosition;
    [SerializeField] private CookingManager _cookingManager;
    [SerializeField] private MealDisplayScript _mealDisplayScript;
    public Vector3 InitialPosition => _initialPosition;

    [SerializeField] private int _maxQuantity;
     
    void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStats(CondimentSo condiment)
    {
        switch (_seasoningType)
        {
            case SeasoningType.Hunger:
                _stats.text = "+/- " + condiment.Value.x.ToString();
                _stats.color = Color.green;
                break;
            case SeasoningType.Satisfaction:
                _stats.text = "+/- " + condiment.Value.y.ToString();
                _stats.color = Color.red;
                break;
            case SeasoningType.Power:
                _stats.text = "+/- " + condiment.Value.z.ToString();
                _stats.color = Color.blue;
                break;
        }
    }

    private void ChangeQuantity()
    {
        _quantity.text = _maxQuantity.ToString();
    }

    public void AddSeosoning(int sign)
    {
        if (_maxQuantity <= 0) return;
        _cookingManager.AddCondiment(_condimentSo, sign);
        _mealDisplayScript.UpdateDisplay(_cookingManager.GetCurrentMeal());
        _maxQuantity--;
        ChangeQuantity();

    }

    private void OnValidate()
    {
        ChangeQuantity();
        SetStats(_condimentSo);
    }

}

[Serializable]
public enum SeasoningType
{
    Hunger,
    Satisfaction,
    Power
}
