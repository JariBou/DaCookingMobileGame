using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class MonsterBundlesSo : ScriptableObject
    {
        [SerializeField, ReadOnly] private Vector3 _totalStatsOfBundle;

        [SerializeField] private List<IngredientsBundleSo> _baseBundles;
        [SerializeField, ReadOnly, TabProperty("Runtimes")] private List<IngredientsBundleSo> _possibleBundles = new();
        [SerializeField, ReadOnly, TabProperty("Runtimes")] private List<IngredientsBundleSo> _bannedBundles = new ();
        
        //TODO: rerolls will change maybe not bundles but full random


        public void ResetBundles()
        {
            _possibleBundles = _baseBundles;
            _bannedBundles = new List<IngredientsBundleSo>(_baseBundles.Count);
        }

        public IngredientsBundleSo GetNextBundle()
        {
            int selectedIndex = Random.Range(0, _possibleBundles.Count);
            IngredientsBundleSo selectedBundle = _possibleBundles[selectedIndex];
            _bannedBundles.Add(selectedBundle); // If reroling does not ban a bundle change here
            _possibleBundles.RemoveAt(selectedIndex);
            return selectedBundle;
        }

        private void OnValidate()
        {
            Vector3 total = new Vector3();

            foreach (IngredientsBundleSo bundle in _baseBundles)
            {
                total += bundle.GetTotalStats();
            }

            _totalStatsOfBundle = total;
        }
    }
}