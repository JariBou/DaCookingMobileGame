using _project.ScriptableObjects.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _cardName;
    [SerializeField] TextMeshProUGUI _cardDescription;
    [SerializeField] TextMeshProUGUI _cardHunger;
    [SerializeField] TextMeshProUGUI _cardSatisfaction;
    [SerializeField] TextMeshProUGUI _cardPower;
    [SerializeField] SpriteRenderer _cardImage;
    [SerializeField] SpriteRenderer _cardBack;
    public IngredientSo _ingredientSo;

    void Start()
    {
        InitializeCard(_ingredientSo.Name, _ingredientSo.Description, _ingredientSo.Stats.x, _ingredientSo.Stats.y, _ingredientSo.Stats.z, _ingredientSo.Icon);
    }


    void Update()
    {
        
    }

    public void InitializeCard(string name, string description, float hunger, float satisfaction, float power, Sprite image)
    {
        _cardName.text = name;
        _cardDescription.text = description;
        _cardHunger.text = hunger.ToString();
        _cardSatisfaction.text = satisfaction.ToString();
        _cardPower.text = power.ToString();
        _cardImage.sprite = image;
        _cardBack.color = CalculateAverageColor(image);
    }

    private Color CalculateAverageColor(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        Rect rect = sprite.textureRect;
        int x = Mathf.FloorToInt(rect.x);
        int y = Mathf.FloorToInt(rect.y);
        int width = Mathf.FloorToInt(rect.width);
        int height = Mathf.FloorToInt(rect.height);

        Color[] pixels = texture.GetPixels(x, y, width, height);
        float totalR = 0, totalG = 0, totalB = 0;

        foreach (Color pixel in pixels)
        {
            totalR += pixel.r;
            totalG += pixel.g;
            totalB += pixel.b;
        }

        float averageR = totalR / pixels.Length;
        float averageG = totalG / pixels.Length;
        float averageB = totalB / pixels.Length;
/*        Debug.Log(averageR + " " + averageG + " " + averageB);*/
        return new Color(averageR, averageG, averageB);
    }
}
