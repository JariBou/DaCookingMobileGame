using System.Collections.Generic;
using System.Globalization;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace _project.Scripts
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cardName;
        [SerializeField] private TextMeshProUGUI _cardDescription;
        [SerializeField] private TextMeshProUGUI _cardHunger;
        [SerializeField] private TextMeshProUGUI _cardSatisfaction;
        [SerializeField] private TextMeshProUGUI _cardPower;
        [SerializeField] private SpriteRenderer _cardImage;
        [SerializeField] private SpriteRenderer _cardBack;

        [SerializeField] private List<Sprite> _cardBacks = new List<Sprite>();


        [Header("Ingredient of the card")]  
        public IngredientSo _ingredientSo;

        private void Start()
        {
            InitializeCard(_ingredientSo);
        }

        public void InitializeCard(IngredientSo ingredientSo)
        {
            InitializeCard(ingredientSo.Name, ingredientSo.Description, ingredientSo.Stats.x, ingredientSo.Stats.y, ingredientSo.Stats.z, ingredientSo.Icon, ingredientSo);
        }

        public void InitializeCard(string ingredientName, string description, float hunger, float satisfaction, float power, Sprite image, IngredientSo ingredientSo)
        {
            _cardName.text = ingredientName;
            _cardDescription.text = description;
            
            _cardHunger.text = (hunger > 0 ? "+" : "") + hunger.ToString(CultureInfo.InvariantCulture);
            _cardSatisfaction.text = (satisfaction > 0 ? "+" : "") + satisfaction.ToString(CultureInfo.InvariantCulture);
            _cardPower.text = (power > 0 ? "+" : "") + power.ToString(CultureInfo.InvariantCulture);
            
            _cardImage.sprite = image;
            _cardBack.sprite = SetCardSprite(ingredientSo);
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

        private Sprite SetCardSprite(IngredientSo ingredientSo)
        {
            switch (ingredientSo.Family)
            {
                case IngredientFamily.Meat:
                    return _cardBacks[0];
                case IngredientFamily.Vegetable:
                    return _cardBacks[1];
                case IngredientFamily.Seafood:
                    return _cardBacks[2];
                default:
                    return null;
            }
        }
    }
}
