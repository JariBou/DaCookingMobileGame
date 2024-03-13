using System;
using _project.Scripts.Core;
using _project.Scripts.Meals;
using GooglePlayGames;
using Hellcooker;
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
            //throw new NotImplementedException();
        }

        private void OnMealFed(Meal meal, bool satisfied, int numberOfMeals, bool rerolledForMeal)
        {
            UnlockAchievement(GPGSIds.achievement_cuistot_en_herbe);

            if (!rerolledForMeal)
            {
                UnlockAchievement(GPGSIds.achievement_dbrouillard);
            }
            
            if (satisfied)
            {
                UnlockAchievement(GPGSIds.achievement_rassasi_pour_linstant);
                if (numberOfMeals == 1)
                {
                    UnlockAchievement(GPGSIds.achievement_let_me_cook);
                }
            }
            
            if (meal.GetIngredientsFamilies().FindAll(m => m == IngredientFamily.Meat).Count == 3)
            {
                UnlockAchievement(GPGSIds.achievement_viande_viande);
            } else if (meal.GetIngredientsFamilies().FindAll(m => m == IngredientFamily.Vegetable).Count == 3)
            {
                UnlockAchievement(GPGSIds.achievement_monstres_vegans);
            } else if (meal.GetIngredientsFamilies().FindAll(m => m == IngredientFamily.Seafood).Count == 3)
            {
                UnlockAchievement(GPGSIds.achievement_poiscaille_party);
            }
        }

        public static void UnlockAchievement(string achievementId)
        {
            PlayGamesPlatform.Instance.ReportProgress(achievementId, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
    }
}