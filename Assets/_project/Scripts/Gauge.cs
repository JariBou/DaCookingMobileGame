using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class Gauge : MonoBehaviour
    {
        [Header("Gauge's Pivot")]
        [Range(0, -5)]
        [SerializeField] private float _yOffset;
        [FormerlySerializedAs("_GaugeOffset")]
        [Range(-5, 5)]
        [SerializeField] private float _gaugeOffset;

        [Header("Gauge's Values")]
        [Range(0, 180)]
        [SerializeField] private float _leftAngleCramped = 0;
        [Range(0, 180)]
        [SerializeField] private float _rightAngleCramped = 180;
        [SerializeField] private float _maxValue = 100;
        private float _angle;
        private float _angle2;
        
        [SerializeField, Range(0, 100)] private int _currentValue = 0;
        [SerializeField, Range(0, 100)] private int _previsualizationValue = 0;
        private float _previousValue = 0;

        [Header("Gauge's Animation")]
        [SerializeField] private AnimationCurve _appearCurve;
        [SerializeField] private float _animationDuration = 0.5f;
        private float _timer = 0;
        private float _timer2 = 0;

        [Header("Gauge's Needle")]
        [SerializeField] private GameObject _needle;
        [SerializeField] private GameObject _needlePrevisualization;

        private bool _isPassingValue = false;
        private bool _isPassingPrevisualizationValue = false;


        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isPassingValue)
            {
                _timer += Time.deltaTime;
                _needle.transform.rotation = Quaternion.Lerp(_needlePrevisualization.transform.rotation, Quaternion.Euler(0, 0, _angle - 90), _appearCurve.Evaluate(_timer / _animationDuration));
                if (_timer >= _animationDuration)
                {
                    _isPassingValue = false;
                    _timer = 0;
                }
            }
            if (_isPassingPrevisualizationValue)
            {
                _timer2 += Time.deltaTime;
                _needlePrevisualization.transform.rotation = Quaternion.Lerp(_needlePrevisualization.transform.rotation, Quaternion.Euler(0, 0, _angle2 - 90),_appearCurve.Evaluate(_timer2/_animationDuration));
                if (_timer2 >= _animationDuration)
                {
                    _isPassingPrevisualizationValue = false;
                    _timer2 = 0;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.up * _yOffset, 0.2f);
            Gizmos.color = Color.red;
        }

        private void OnValidate()
        {
            PassValue(_currentValue);
            
            PassPrevisualizationValue(_previsualizationValue);

            float normalizedDifference = Mathf.Abs(_currentValue - _previsualizationValue)/50f;
            

            if (_previsualizationValue > _currentValue)
            {
                _needlePrevisualization.GetComponent<Image>().color = new Color(0, normalizedDifference, 0, 0.2f);
            }
            else
            {
                _needlePrevisualization.GetComponent<Image>().color = new Color(normalizedDifference, 0, 0, 0.2f);
            }
        }

        public void PassBoth(int value)
        {
            PassValue(value);
            PassPrevisualizationValue(value);
        }

        public void PassValue(int value)
        {
            Vector3 position = transform.position + Vector3.up * _yOffset;
            float x = position.x + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Cos(ConvertValueToAngle(value) * Mathf.Deg2Rad));
            float y = position.y + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Sin(ConvertValueToAngle(value) * Mathf.Deg2Rad));
            _needle.transform.position = new Vector3(x, y, 0);
            _angle = Mathf.Atan2(_needle.transform.position.y - position.y, _needle.transform.position.x - position.x) * Mathf.Rad2Deg;
            _isPassingValue = true;
            _currentValue = value;
        }

        public void PassPrevisualizationValue(int prevValue)
        {
            Vector3 position2 = transform.position + Vector3.up * _yOffset;
            float x2 = position2.x + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Cos(ConvertValueToAngle(prevValue) * Mathf.Deg2Rad));
            float y2 = position2.y + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Sin(ConvertValueToAngle(prevValue) * Mathf.Deg2Rad));
            _needlePrevisualization.transform.position = new Vector3(x2, y2, 0);
            _angle2 = Mathf.Atan2(_needlePrevisualization.transform.position.y - position2.y, _needlePrevisualization.transform.position.x - position2.x) * Mathf.Rad2Deg;
            _isPassingPrevisualizationValue = true;
            _previsualizationValue = prevValue;
        }


        public float ConvertValueToAngle(float value)
        {
            float ratio = ((90 - _rightAngleCramped) - (90 + _leftAngleCramped)) / _maxValue;
            return (90 - _rightAngleCramped) - ((_maxValue - value) * ratio);
        }

    }
}
