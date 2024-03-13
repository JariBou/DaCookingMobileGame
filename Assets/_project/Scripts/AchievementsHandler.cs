using System;
using _project.Scripts.Meals;
using UnityEngine;

namespace _project.Scripts
{
    public class AchievementsHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            CookingManager.MealFed += OnMealFed;
            MonsterInstance.NewMonster += OnNewMonster;
        }
        
        private void OnDisable()
        {
            CookingManager.MealFed -= OnMealFed;
            MonsterInstance.NewMonster -= OnNewMonster;
        }

        private void OnNewMonster()
        {
            throw new NotImplementedException();
        }

        private void OnMealFed(Meal obj)
        {
            throw new NotImplementedException();
        }
    }
}