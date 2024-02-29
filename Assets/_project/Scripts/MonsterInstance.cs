using System;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts
{
    public class MonsterInstance : MonoBehaviour
    {
        [SerializeField] private MonsterDataSo _baseMonsterDataSo;
        public Vector3 CurrentStats { get; private set; }

        public MonsterDataSo MonsterData { get; private set; }


        private void Start()
        {
            InitializeMonster(_baseMonsterDataSo);
        }

        public void InitializeMonster(MonsterDataSo dataSo)
        {
            MonsterData = dataSo;

            int x = Random.Range(dataSo.RandomStatsMin.x, dataSo.RandomStatsMax.x);
            int y = Random.Range(dataSo.RandomStatsMin.y, dataSo.RandomStatsMax.y);
            int z = Random.Range(dataSo.RandomStatsMin.z, dataSo.RandomStatsMax.z);
            CurrentStats = new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns true if meal satisfied the monster
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        public bool FeedMeal(Meal meal)
        {
            CurrentStats += meal.Stats;
            CurrentStats.Clamp(Vector3.zero, new Vector3(100, 100, 100));

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