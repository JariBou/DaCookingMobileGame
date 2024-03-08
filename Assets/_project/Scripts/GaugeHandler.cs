using System;
using _project.Scripts.Core;
using UnityEngine;

namespace _project.Scripts
{
    public class GaugeHandler : MonoBehaviour
    {

        [SerializeField] private Gauge _gaugeX;
        [SerializeField] private Gauge _gaugeY;
        [SerializeField] private Gauge _gaugeZ;

        [SerializeField] private MonsterInstance _monsterInstance;

        private void Start()
        {
            NewPhase();
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
        private int ClampValue(int val, float valToAdd)
        {
            return (int)Math.Clamp(val + valToAdd, 0, 100);
        }

        public void NewPhase()
        {
            _gaugeX.PassBoth(_monsterInstance.CurrentStats.x);
            _gaugeY.PassBoth(_monsterInstance.CurrentStats.y);
            _gaugeZ.PassBoth(_monsterInstance.CurrentStats.z);
        }
    }
}
