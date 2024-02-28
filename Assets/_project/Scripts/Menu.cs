using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] IngredientStats[] _ingredientStats;
    [SerializeField] TextMeshProUGUI _finalHunger;
    [SerializeField] TextMeshProUGUI _finalSatisfaction;
    [SerializeField] TextMeshProUGUI _finalPower;
    [SerializeField] Image _finalMealImage;
    [SerializeField] Button _GoPhase2Button;

    [SerializeField] clickUp clickUp;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _ingredientStats.Length; i++)
        {
            _ingredientStats[i]._cardName.text = "";
            _ingredientStats[i]._cardHunger.text = "";
            _ingredientStats[i]._cardSatisfaction.text = "";
            _ingredientStats[i]._cardPower.text = "";
            _ingredientStats[i]._cardImage.sprite = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMenu()
    {
        if (clickUp._enlargedSprites.Count > 0)
        {
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                if (i < clickUp._enlargedSprites.Count)
                {
                    UpdateIngredient(i);
                }
                else
                {
                    _ingredientStats[i]._cardName.text = "";
                    _ingredientStats[i]._cardHunger.text = "";
                    _ingredientStats[i]._cardSatisfaction.text = "";
                    _ingredientStats[i]._cardPower.text = "";
                    _ingredientStats[i]._cardImage.sprite = null;
                }
            }
        }
        else
        {
            for (int i = 0; i < _ingredientStats.Length; i++)
            {
                _ingredientStats[i]._cardName.text = "";
                _ingredientStats[i]._cardHunger.text = "";
                _ingredientStats[i]._cardSatisfaction.text = "";
                _ingredientStats[i]._cardPower.text = "";
                _ingredientStats[i]._cardImage.sprite = null;
            }
        }
        _finalHunger.text = clickUp._enlargedSprites.Sum(x => x._ingredientSo.Stats.x).ToString();
        _finalSatisfaction.text = clickUp._enlargedSprites.Sum(x => x._ingredientSo.Stats.y).ToString();
        _finalPower.text = clickUp._enlargedSprites.Sum(x => x._ingredientSo.Stats.z).ToString();

    }

    private void UpdateIngredient(int order)
    {
        _ingredientStats[order]._cardName.text = clickUp._enlargedSprites[order]._ingredientSo.name;
        _ingredientStats[order]._cardHunger.text = clickUp._enlargedSprites[order]._ingredientSo.Stats.x.ToString();
        _ingredientStats[order]._cardSatisfaction.text = clickUp._enlargedSprites[order]._ingredientSo.Stats.y.ToString();
        _ingredientStats[order]._cardPower.text = clickUp._enlargedSprites[order]._ingredientSo.Stats.z.ToString();
        _ingredientStats[order]._cardImage.sprite = clickUp._enlargedSprites[order]._ingredientSo.Icon;

    }
}
[Serializable]
public struct IngredientStats
{
    public TextMeshProUGUI _cardName;
    public TextMeshProUGUI _cardHunger;
    public TextMeshProUGUI _cardSatisfaction;
    public TextMeshProUGUI _cardPower;
    public Image _cardImage;
}