using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class MonsterDataSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        [FormerlySerializedAs("_numberOfMeals")] [SerializeField] private int _maxNumberOfMeals;
        [SerializeField] private int _maxRerolls;

        [SerializeField, Tooltip("Min stats to have for that stat to win"), TabProperty("Stats Config")] private Vector3Int _statsMin;
        [SerializeField, Tooltip("Max stats to have for that stat to win"), TabProperty("Stats Config")] private Vector3Int _statsMax;
        [SerializeField, TabProperty("Stats Config")] private List<IngredientsBundleSo> _ingredientBundles;
        [SerializeField, Tooltip("Min possible random rolled value for a stat"), TabProperty("Stats Config")] private Vector3Int _randomStatsMin;
        [SerializeField, Tooltip("Max possible random rolled value for a stat"), TabProperty("Stats Config")] private Vector3Int _randomStatsMax;

        [SerializeField, TabProperty("Visuals")] private Sprite _icon;
        [SerializeField, TabProperty("Visuals")] private Sprite _sleepingSprite;
        [SerializeField, TabProperty("Visuals")] private Sprite _normalSprite;
        [SerializeField, TabProperty("Visuals")] private Sprite _angrySprite;
        [SerializeField, TabProperty("Visuals")] private GameObject _monsterPrefab;
        [SerializeField, TabProperty("Visuals")] private Sprite _background;
        [SerializeField, TabProperty("Visuals"), Tooltip("Must contain 4 positions")] private List<Vector3> _positionsOnScreen = new()
        {
            new Vector3(-11.6f,-4,5),
            new Vector3(50.1f,-4.3f,5),
            new Vector3(79, -3, 5),
            new Vector3(79, -5, 5),
        };

        public string Name => _name;
        public string Description => _description;
        
        public Vector3Int StatsMin => _statsMin;
        public Vector3Int StatsMax => _statsMax;
        public List<IngredientsBundleSo> IngredientBundles => _ingredientBundles;
        
        public Sprite Icon => _icon;
        public Sprite SleepingSprite => _sleepingSprite;
        public Sprite NormalSprite => _normalSprite;
        public Sprite AngrySprite => _angrySprite;
        public Vector3Int RandomStatsMin => _randomStatsMin;
        public Vector3Int RandomStatsMax => _randomStatsMax;
        public int MaxNumberOfMeals => _maxNumberOfMeals;
        public GameObject MonsterPrefab => _monsterPrefab;

        public int MaxRerolls => _maxRerolls;
        public Sprite Background => _background;

        public List<Vector3> PositionsOnScreen => _positionsOnScreen;
    }
}