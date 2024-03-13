using System;
using _project.Scripts.Core;
using _project.Scripts.Meals;
using NaughtyAttributes;
using UnityEngine;

namespace _project.Scripts.Gauges
{
    public class GaugeHandler : MonoBehaviour
    {

        [SerializeField] private Gauge _gaugeX;
        [SerializeField] private Gauge _gaugeY;
        [SerializeField] private Gauge _gaugeZ;

        [SerializeField] private MonsterInstance _monsterInstance;

        private void Start()
        {
            UpdateAll();
        }

        public void PrevisualizeMeal(Meal meal)
        {
            _gaugeX.PassPrevisualizationValue(ClampValue(meal.Stats.x, _monsterInstance.CurrentStats.x));
            _gaugeY.PassPrevisualizationValue(ClampValue(meal.Stats.y, _monsterInstance.CurrentStats.y));
            _gaugeZ.PassPrevisualizationValue(ClampValue(meal.Stats.z, _monsterInstance.CurrentStats.z));
        }
        
        public void RestartPrevGauges()
        {
            _gaugeX.PassPrevisualizationValue(_monsterInstance.CurrentStats.x);
            _gaugeY.PassPrevisualizationValue(_monsterInstance.CurrentStats.y);
            _gaugeZ.PassPrevisualizationValue(_monsterInstance.CurrentStats.z);
        }

        public void SetGaugesTolerance(int value)
        {
            _gaugeX.SetTolerance(value);
            _gaugeY.SetTolerance(value);
            _gaugeZ.SetTolerance(value);
        }

        public void SetGaugesMarks(int min, int max)
        {
            _gaugeX.SetMarks(min, max);
            _gaugeY.SetMarks(min, max);
            _gaugeZ.SetMarks(min, max);
        }
        private int ClampValue(int val, float valToAdd)
        {
            return (int)Math.Clamp(val + valToAdd, 0, 100);
        }

        public void UpdateAll()
        {
            _gaugeX.PassBoth(_monsterInstance.CurrentStats.x);
            _gaugeX.SetMarks(_monsterInstance.CurrentMarks.x, _monsterInstance.MonsterData.StatsMax.x);

            _gaugeY.PassBoth(_monsterInstance.CurrentStats.y);
            _gaugeY.SetMarks(_monsterInstance.CurrentMarks.x, _monsterInstance.MonsterData.StatsMax.x);

            _gaugeZ.PassBoth(_monsterInstance.CurrentStats.z);
            _gaugeZ.SetMarks(_monsterInstance.CurrentMarks.x, _monsterInstance.MonsterData.StatsMax.x);

        }

        [Button]
        public bool AllGaugesAreSetUp()
        {
            Debug.Log(!_gaugeX.IsPassingValue && !_gaugeY.IsPassingValue && !_gaugeZ.IsPassingValue);
            return !_gaugeX.IsPassingValue && !_gaugeY.IsPassingValue && !_gaugeZ.IsPassingValue;
        }
    }
}
