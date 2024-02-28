using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class MonsterDataSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        
        [SerializeField, Tooltip("Min stats to have for that stat to win"), TabProperty("Stats Config")] private Vector3Int _statsMin;
        [SerializeField, Tooltip("Max stats to have for that stat to win"), TabProperty("Stats Config")] private Vector3Int _statsMax;
        [SerializeField, TabProperty("Stats Config")] private IngredientsBundleSo _ingredients;
        [SerializeField, Tooltip("Min possible random rolled value for a stat"), TabProperty("Stats Config")] private Vector3Int _randomStatsMin;
        [SerializeField, Tooltip("Max possible random rolled value for a stat"), TabProperty("Stats Config")] private Vector3Int _randomStatsMax;

        [SerializeField, TabProperty("Sprites")] private Sprite _icon;
        [SerializeField, TabProperty("Sprites")] private Sprite _sleepingSprite;
        [SerializeField, TabProperty("Sprites")] private Sprite _normalSprite;
        [SerializeField, TabProperty("Sprites")] private Sprite _angrySprite;

        public string Name => _name;
        public string Description => _description;
        
        public Vector3Int StatsMin => _statsMin;
        public Vector3Int StatsMax => _statsMax;
        public IngredientsBundleSo Ingredients => _ingredients;
        
        public Sprite Icon => _icon;
        public Sprite SleepingSprite => _sleepingSprite;
        public Sprite NormalSprite => _normalSprite;
        public Sprite AngrySprite => _angrySprite;
        public Vector3Int RandomStatsMin => _randomStatsMin;
        public Vector3Int RandomStatsMax => _randomStatsMax;
    }
}