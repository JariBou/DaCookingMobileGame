using System.Collections.Generic;
using System.Linq;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private UnityEvent _OnReRoll;
/*        [SerializeField] private bool _canHaveSameIngredientInDeck;*/

        private void OnMouseDown()
        {
            if (_rerollCount >= _rerollChance || _recipeDisplayScript.CookingManager.GetCurrentPhase() != PhaseCode.Phase1 ) return;
        
            _rerollCount++;
            ReRollBundle();
            _OnReRoll?.Invoke();
        }

        public void ReRollBundle()
        {
            List<IngredientSo> possibleIngredients = new List<IngredientSo>(_bundleSo.BundleIngredients);

            foreach (IngredientSo ingredientSo in GetSelectedIngredients())
            {
                int index = possibleIngredients.FindIndex(el => el == ingredientSo);
                if (index != -1) // Vérifiez si l'élément a été trouvé
                {
                    possibleIngredients.RemoveAt(index);
                }
            }

            foreach (ClickUp clickUp in _cards)
            {
                if (clickUp.IsScaled) continue; // If not selected

                if (possibleIngredients.Count > 0) // Vérifiez si la liste contient encore des éléments
                {
                    int rIndex = Random.Range(0, possibleIngredients.Count);
                    clickUp.PassIngredient(possibleIngredients[rIndex]);
                    possibleIngredients.RemoveAt(rIndex); // Assurez-vous que cet index est valide
                }
                else
                {
                    // Gérez le cas où il n'y a plus d'ingrédients disponibles
                    Debug.LogWarning("Plus d'ingrédients disponibles pour le re-roll");
                    break; // Sortez de la boucle si aucun ingrédient n'est disponible
                }
            }
        }

        /*        public void ReRollBundle()
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
                }*/

        private List<IngredientSo> GetSelectedIngredients()
        {
            List<IngredientSo> list = new List<IngredientSo>(3);
        
            list.AddRange(from clickUp in _cards where clickUp.IsScaled select clickUp.Ingredient);

            return list;
        }

        [NaughtyAttributes.Button]
        public void ResetCards()
        {
            foreach (ClickUp clickUp in _cards)
            {
                clickUp.ResetCard();
            }
        }
    }
}
