using System;
using System.Collections.Generic;
using _project.Scripts.Core;
using NaughtyAttributes;
using UnityEngine;

namespace _project.Scripts
{
    public class CameraScript : MonoBehaviour
    {

        [SerializeField] private List<Vector3> _positions;
        private int _currentPosIndex;

        [SerializeField] private float _timeToSlide;
        [SerializeField] private AnimationCurve _slideCurve;
        private float _timer;
        private bool _isMoving;
        public int CurrentIndex => _currentPosIndex;


        // Start is called before the first frame update
        void Start()
        {
            _currentPosIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isMoving) return;

            _timer = Math.Clamp(_timer + Time.deltaTime, 0, _timeToSlide);
            Debug.Log(Utils.Mod(_currentPosIndex - 1, _positions.Count));
            transform.position = Vector3.Lerp(_positions[Utils.Mod(_currentPosIndex - 1, _positions.Count)],
                _positions[_currentPosIndex], _slideCurve.Evaluate(_timer / _timeToSlide));

            if (Math.Abs(_timer - _timeToSlide) < 0.00001)
            {
                _isMoving = false;
            }
        }
        
        [Button]
        public void NextPhase()
        {
            _currentPosIndex = (_currentPosIndex + 1) % _positions.Count;
            _isMoving = true;
            _timer = 0;
        }
        
       
    }
}
