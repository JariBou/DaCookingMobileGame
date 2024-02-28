using _project.ScriptableObjects.Scripts;
using _project.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ReRoll : MonoBehaviour
{

    [SerializeField] private IngredientsBundleSo _bundleSo;
    [SerializeField] private ClickUp[] _cards;
    [SerializeField, Range(1, 10)] private int _rerollChance = 2;
    private int _rerollCount = 0;
    private bool _isRerolling = false;
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
        Debug.Log("Rerolling");
        for (int i = 0; i < _cards.Length; i++)
        {
            if (!_cards[i].IsScaled)
            {
                StartCoroutine(_cards[i].PassIngredient(RandomIngredient(_cards[i].Ingredient)));
            }
        }
    }

    private IngredientSo RandomIngredient(IngredientSo ingredient)
    {
        IngredientSo newIngredient;
        for (int i = 0; i < _bundleSo.BundleIngredients.Count; i++)
        {
            newIngredient = _bundleSo.BundleIngredients[Random.Range(0, _bundleSo.BundleIngredients.Count)];
            if (newIngredient != ingredient)
            {
                return newIngredient;
            }
        }
        return ingredient;
    }
}
