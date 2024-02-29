using System;
using System.Collections;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts;
using _project.Scripts.Core;
using UnityEngine;

public class CookingPhaseScript : MonoBehaviour
{
    [SerializeField] private MealDisplayScript _resultMealDisplayScript;
    [SerializeField] private CookingParamsSo _cookingParams;
    [SerializeField] private CookingManager _cookingManager;
    
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
        _resultMealDisplayScript.UpdateDisplay(new Meal(_cookingManager.GetCurrentMeal()).CookMeal(_cookingParams.GetMultiplier(SelectedCookingMethod)));
    }

    public void GoToNextPhase()
    {
        if (SelectedCookingMethod == CookingMethod.Null) return;
        
        _cookingManager.CookMeal(SelectedCookingMethod);
        _cookingManager.Camera.NextPhase();
    }
}
