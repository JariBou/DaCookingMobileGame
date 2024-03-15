using System;
using System.Collections;
using System.Collections.Generic;
using _project.Scripts.Core;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace _project.Scripts
{
    public class CameraScript : MonoBehaviour
    {

        [SerializeField] private List<Vector3> _positions;
        [SerializeField] private List<Vector3> _scales;
        [SerializeField] private List<Vector3> _monsterPositions;
        private int _currentPosIndex;
        private int _currentScaleIndex;

        [SerializeField] private float _timeToSlide;
        [SerializeField] private AnimationCurve _slideCurve;
        [SerializeField] private UnityEvent _onSlideCam;
        
        private Transform _monster;
        private float _timer;
        private bool _isMoving;
        private bool _isScaling;
        public int CurrentIndex => _currentPosIndex;
        public int CurrentScaleIndex => _currentScaleIndex;


        // Start is called before the first frame update
        void Start()
        {
            _currentPosIndex = 0;
            _currentScaleIndex = 0;
        }

        private IEnumerator ScaleMonster()
        {
            yield return new WaitForSeconds(1.5f);
            _isScaling = true;
        }
        public void PassMonsterTransform(Transform monsterTransform)
        {
            _monster = monsterTransform;
            _monster.position = _monsterPositions[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isMoving) return;

            _timer = Math.Clamp(_timer + Time.deltaTime, 0, _timeToSlide);
            transform.position = Vector3.Lerp(_positions[Utils.Mod(_currentPosIndex - 1, _positions.Count)],
                _positions[_currentPosIndex], _slideCurve.Evaluate(_timer / _timeToSlide));
            
            _monster.position = Vector3.Lerp(_monsterPositions[Utils.Mod(_currentPosIndex - 1, _monsterPositions.Count)],
                _monsterPositions[_currentPosIndex], _slideCurve.Evaluate(_timer / _timeToSlide));

            _monster.localScale = Vector3.Lerp(_scales[Utils.Mod(_currentScaleIndex - 1, _scales.Count)],
                               _scales[_currentScaleIndex], _slideCurve.Evaluate(_timer / _timeToSlide));

            if (Math.Abs(_timer - _timeToSlide) < 0.00001)
            {
                _isMoving = false;
            }
        }



        public void Restart()
        {
            _currentPosIndex = 0;
            _currentScaleIndex = 0;
            _isMoving = true;
            _onSlideCam?.Invoke();
            _timer = 0;
        }
        
        [Button]
        public void NextPhase()
        {
            _currentPosIndex = (_currentPosIndex + 1) % _positions.Count;
            _currentScaleIndex = (_currentScaleIndex + 1) % _scales.Count;
            _isMoving = true;
            _onSlideCam?.Invoke();
            _timer = 0;
        }
        
       
    }
}
