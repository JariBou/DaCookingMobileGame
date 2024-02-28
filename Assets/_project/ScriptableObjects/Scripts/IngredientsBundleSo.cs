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

        public List<IngredientSo> BundleIngredients => _bundleIngredients;


        private void OnValidate()
        {
            _totalStatsOfBundles = GetTotalStats();
        }

        public Vector3 GetTotalStats()
        {
            Vector3 total = new();

            foreach (IngredientSo ingredient in BundleIngredients)
            {
                total += ingredient.Stats;
            }
            
            Vector3 addedTotal = new();

            foreach (IngredientSo ingredient in BundleIngredients)
            {
                addedTotal += ingredient.RandomAddedstats;
            }
            //TODO: add random added stats to display

            return total;
        }
    }
}