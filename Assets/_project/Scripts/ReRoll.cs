using System.Collections.Generic;
using System.Linq;
using _project.ScriptableObjects.Scripts;
using UnityEngine;

namespace _project.Scripts
{
    public class ReRoll : MonoBehaviour
    {

        [SerializeField] private IngredientsBundleSo _bundleSo;
        [SerializeField] private ClickUp[] _cards;
        [SerializeField] private RecipeDisplayScript _recipeDisplayScript;
        [SerializeField, Range(1, 10)] private int _rerollChance = 2;
        private int _rerollCount = 0;
        private bool _isRerolling = false;
/*        [SerializeField] private bool _canHaveSameIngredientInDeck;*/

        private void OnMouseDown()
        {
            if (_rerollCount >= _rerollChance || !_recipeDisplayScript.IsEnabled) return;
        
            _rerollCount++;
            ReRollBundle();
        }

        public void ReRollBundle()
        {
            List<IngredientSo> possibleIngredients = new List<IngredientSo>(_bundleSo.BundleIngredients);

            foreach (IngredientSo ingredientSo in GetSelectedIngredients())
            {
                possibleIngredients.RemoveAt(possibleIngredients.FindIndex(el => el == ingredientSo));
            }
        
        
            foreach (ClickUp clickUp in _cards)
            {
                if (clickUp.IsScaled) continue; // If not selected
                
                int rIndex = Random.Range(0, possibleIngredients.Count);
                
                clickUp.PassIngredient(possibleIngredients[rIndex]);
                possibleIngredients.RemoveAt(rIndex);
            }
        }

        private List<IngredientSo> GetSelectedIngredients()
        {
            List<IngredientSo> list = new List<IngredientSo>(3);
        
            list.AddRange(from clickUp in _cards where clickUp.IsScaled select clickUp.Ingredient);

            return list;
        }
    }
}
