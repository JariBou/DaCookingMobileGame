using System;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts
{
    public class MonsterInstance : MonoBehaviour
    {
        [SerializeField] private MonsterDataSo _baseMonsterDataSo;
        [SerializeField] private TMP_Text _numberOfMealsText;
        public Vector3Int CurrentStats { get; private set; }
        public Vector2Int CurrentMarks { get; private set; }
        
        private int _maxNumberOfMeals;
        private int _numberOfMeals;

        public MonsterDataSo MonsterData { get; private set; }

        public int MaxNumberOfMeals => _maxNumberOfMeals;
        public int NumberOfMeals => _numberOfMeals;

        public List<MonsterDataSo> MonsterDatas = new List<MonsterDataSo>();

        private void Awake()
        {
            InitializeMonster(_baseMonsterDataSo);
        }

        public void InitializeMonster(MonsterDataSo dataSo)
        {
            MonsterData = dataSo;
            _maxNumberOfMeals = dataSo.MaxNumberOfMeals;
            _numberOfMeals = 0;
            if (_numberOfMealsText) _numberOfMealsText.text = $"{_numberOfMeals}/{MaxNumberOfMeals}";

            int x = Random.Range(dataSo.RandomStatsMin.x, dataSo.RandomStatsMax.x);
            int y = Random.Range(dataSo.RandomStatsMin.y, dataSo.RandomStatsMax.y);
            int z = Random.Range(dataSo.RandomStatsMin.z, dataSo.RandomStatsMax.z);
            CurrentStats = new Vector3Int(x, y, z);
            CurrentMarks = new Vector2Int(dataSo.StatsMin.x, dataSo.StatsMin.y);
        }

        public void BackToMenu()
        {

        }

        public void ChangeMonster(MonsterDataSo dataSo)
        {

        }
        
        /// <summary>
        /// Returns true if meal satisfied the monster
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        public bool FeedMeal(Meal meal)
        {
            CurrentStats += meal.Stats;
            CurrentStats = CurrentStats.ClampCustom(Vector3Int.zero, new Vector3Int(100, 100, 100));
            _numberOfMeals++;
            if (_numberOfMealsText) _numberOfMealsText.text = $"{_numberOfMeals}/{MaxNumberOfMeals}";
            return CurrentStats.IsInBounds(MonsterData.StatsMin, MonsterData.StatsMax);
        }

        public List<IngredientSo> GetPossibleIngredients()
        {
            return MonsterData.Ingredients.BundleIngredients;
        }

        public Sprite GetDisplayedSprite()
        {
            int xDelta = (int)Math.Abs((MonsterData.StatsMax.x - MonsterData.StatsMin.x) / 2f - CurrentStats.x);
            int yDelta = (int)Math.Abs((MonsterData.StatsMax.y - MonsterData.StatsMin.y) / 2f - CurrentStats.y);
            int zDelta = (int)Math.Abs((MonsterData.StatsMax.z - MonsterData.StatsMin.z) / 2f - CurrentStats.z);

            int averageDistance = (xDelta + yDelta + zDelta) / 3;
            
            return averageDistance switch
            {
                < 50 => MonsterData.SleepingSprite,
                > 100 => MonsterData.AngrySprite,
                _ => MonsterData.NormalSprite
            };
        }
    }
}