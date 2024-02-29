using _project.ScriptableObjects.Scripts;
using _project.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class ReRoll : MonoBehaviour
{

    [SerializeField] private IngredientsBundleSo _bundleSo;
    [SerializeField] private ClickUp[] _cards;
    [SerializeField, Range(1, 10)] private int _rerollChance = 2;
    private int _rerollCount = 0;
    private bool _isRerolling = false;
    [SerializeField] private bool _canHaveSameIngredientInDeck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_rerollCount < _rerollChance)
        {
            _rerollCount++;
            ReRollBundle();
        }
    }

    private void ReRollBundle()
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            if (!_cards[i].IsScaled)
            {
               _cards[i].PassIngredient(RandomIngredient(_cards[i].Ingredient, i));
            }
        }
    }

    private IngredientSo RandomIngredient(IngredientSo currentIngredient, int rank)
    {
        
        List<IngredientSo> possibleIngredients = new List<IngredientSo>(_bundleSo.BundleIngredients);
        possibleIngredients.Remove(currentIngredient);

        if (!_canHaveSameIngredientInDeck)
        {
            possibleIngredients.RemoveAll(ingredient => IsInDeck(ingredient, rank));
        }

       
        if (possibleIngredients.Count > 0)
        {
            IngredientSo newIngredient = possibleIngredients[Random.Range(0, possibleIngredients.Count)];
            return newIngredient;
            
        }

        
        return currentIngredient;
    }

    private bool IsInDeck(IngredientSo ingredient, int rank)
    {
        for (int i = 0; i < rank + 1; i++)
        {
            if (_cards[i].Ingredient == ingredient)
            {
                return true;
            }
        }
        return false;
    }
}
