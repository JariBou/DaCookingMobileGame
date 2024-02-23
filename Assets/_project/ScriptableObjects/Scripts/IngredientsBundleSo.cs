using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class IngredientsBundleSo : ScriptableObject
    {
        [SerializeField, ReadOnly] private Vector3 _totalStatsOfBundles;

        [SerializeField] private List<IngredientSo> _bundleIngredients;


        private void OnValidate()
        {
            _totalStatsOfBundles = GetTotalStats();
        }

        public Vector3 GetTotalStats()
        {
            Vector3 total = new Vector3();

            foreach (IngredientSo ingredient in _bundleIngredients)
            {
                total += ingredient.Stats;
            }

            return total;
        }
    }
}