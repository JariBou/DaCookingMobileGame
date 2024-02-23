using System;
using _project.ScriptableObjects.Scripts;
using UnityEngine;

namespace _project.Scripts.Core
{
    [Serializable]
    public class Meal
    {
        [SerializeField] private Vector3 _stats;

        public Vector3 Stats => _stats;

        public Meal(IngredientSo ingredient1, IngredientSo ingredient2, IngredientSo ingredient3)
        {
            _stats = ingredient1.Stats + ingredient2.Stats + ingredient3.Stats;
        }

        public Meal CookMeal(Vector3 multiplier)
        {
            _stats.x *= multiplier.x;
            _stats.y *= multiplier.y;
            _stats.z *= multiplier.z;
            
            return this;
        }

        public Meal AddCondiment()
        {
            // TODO
            
            return this;
        }
    }
}