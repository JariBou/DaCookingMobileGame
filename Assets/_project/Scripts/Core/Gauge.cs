using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts.Core
{
    public class Gauge : MonoBehaviour
    {
        [Header("Gauge's Pivot")]
        [Range(0, -5)]
        [SerializeField] private float _yOffset;
        [FormerlySerializedAs("_GaugeOffset")]
        [Range(-5, 5)]
        [SerializeField] private float _gaugeOffset;
        
        [SerializeField, Range(-5, 5)] private float _leftMarkerOffset;
        [SerializeField, Range(-5, 5)] private float _rightMarkerOffset;

        [Header("Gauge's Values")]
        [Range(0, 180)]
        [SerializeField] private float _leftAngleCramped = 0;
        [Range(0, 180)]
        [SerializeField] private float _rightAngleCramped = 180;
        [SerializeField] private float _maxValue = 100;
        private float _angle;
        private float _angle2;
        
        [SerializeField, Range(0, 100)] private int _currentValue = 0;
        private int _finalValue;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField, Range(0, 100)] private int _previsualizationValue = 0;
        private float _previousValue = 0;

        [Header("Gauge's Animation")]
        [SerializeField] private AnimationCurve _appearCurve;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _previsualizationAnimationDuration = 0.5f;
        private float _timer = 0;
        private float _timer2 = 0;

        [Header("Gauge's Needle")]
        [SerializeField] private GameObject _needle;
        [SerializeField] private GameObject _needlePrevisualization;

        [Header("Gauge's Limits Marks")]
        [SerializeField] private GameObject _leftLimit;
        [SerializeField] private GameObject _rightLimit;
/*        [SerializeField, Range(0, 100)] private int _leftLimitValue;
        [SerializeField, Range(0, 100)] private int _rightLimitValue;*/
        [SerializeField, Range(0, 50)] private int _tolerance = 5;

        private bool _isPassingValue = false;
        public bool IsPassingValue => _isPassingValue;
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
                _needle.transform.rotation = Quaternion.Lerp(_needle.transform.rotation, Quaternion.Euler(0, 0, _angle - 90), _appearCurve.Evaluate(_timer / _animationDuration));
                _currentValue = (int) Mathf.Lerp(_currentValue, _finalValue, _appearCurve.Evaluate(_timer / _animationDuration));
                _valueText.text = _currentValue.ToString();
                float differenceBetweenAngles = Mathf.Abs(_needle.transform.rotation.eulerAngles.z - (_angle- 90));
                if (_timer >= _animationDuration || differenceBetweenAngles < 0.01f)
                {
                    _timer = 0;
                    _isPassingValue = false;
                    _currentValue = _finalValue;
                    _valueText.text = _currentValue.ToString();
                }
            }
            if (_isPassingPrevisualizationValue)
            {
                _timer2 += Time.deltaTime;
                _needlePrevisualization.transform.rotation = Quaternion.Lerp(_needlePrevisualization.transform.rotation, Quaternion.Euler(0, 0, _angle2 - 90),_appearCurve.Evaluate(_timer2/ _previsualizationAnimationDuration));
                float differenceBetweenAnglesPrev = Mathf.Abs(_needlePrevisualization.transform.rotation.eulerAngles.z - (_angle2 - 90));
                if (_timer2 >= _previsualizationAnimationDuration || differenceBetweenAnglesPrev < 0.01f)
                {
                    _timer2 = 0;
                    _isPassingPrevisualizationValue = false;
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

            SetTolerance(_tolerance);

            float normalizedDifference = Mathf.Abs(_currentValue - _previsualizationValue)/50f;
            normalizedDifference = Mathf.Lerp(0.7f, 1f, normalizedDifference);


            if (_previsualizationValue > _currentValue)
            {
                _needlePrevisualization.GetComponent<Image>().color = new Color(0, normalizedDifference, 0, 0.7f);
            }
            else
            {
                _needlePrevisualization.GetComponent<Image>().color = new Color(normalizedDifference, 0, 0, 0.7f);
            }
        }

        public void PassBoth(int value)
        {
            PassValue(value);
            PassPrevisualizationValue(value);
        }


        public void PassValue(int value)
        {
            _timer = 0;
            Vector3 position = transform.position + Vector3.up * _yOffset;
            float x = position.x + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Cos(ConvertValueToAngle(value) * Mathf.Deg2Rad));
            float y = position.y + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Sin(ConvertValueToAngle(value) * Mathf.Deg2Rad));
            _needle.transform.position = new Vector3(x, y, 0);
            _angle = Mathf.Atan2(_needle.transform.position.y - position.y, _needle.transform.position.x - position.x) * Mathf.Rad2Deg;
            _isPassingValue = true;
            _finalValue = value;
        }

        public void PassPrevisualizationValue(int prevValue)
        {
            _timer2 = 0;
            Vector3 position2 = transform.position + Vector3.up * _yOffset;
            float x2 = position2.x + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Cos(ConvertValueToAngle(prevValue) * Mathf.Deg2Rad));
            float y2 = position2.y + (Mathf.Abs(_yOffset + _gaugeOffset) * Mathf.Sin(ConvertValueToAngle(prevValue) * Mathf.Deg2Rad));
            _needlePrevisualization.transform.position = new Vector3(x2, y2, 0);
            _angle2 = Mathf.Atan2(_needlePrevisualization.transform.position.y - position2.y, _needlePrevisualization.transform.position.x - position2.x) * Mathf.Rad2Deg;
            _isPassingPrevisualizationValue = true;
            _previsualizationValue = prevValue;
        }

        public void SetTolerance(int value)
        {
            value = Mathf.Clamp(value, 0, 50); // C'est une s�curit� pour �viter que la tol�rance soit plus grande que la moiti� de la jauge
            float mid = _maxValue / 2;
            SetMarks((int)mid - value, (int)mid + value);
        }
        public void SetMarks(int left, int right)
        {
            SetMark(left, _leftLimit, true);
            SetMark(right, _rightLimit, false);
        }

        public void SetMark(int value, GameObject mark, bool _isLeftMarker)
        {
            if (_isLeftMarker)
            {
                SetMarkerOffset(ref _leftMarkerOffset, value);
            }
            else
            {
                SetMarkerOffset(ref _rightMarkerOffset, value);
            }
            float MarkerOffset = _isLeftMarker ? _leftMarkerOffset : _rightMarkerOffset;
            Vector3 position = transform.position + Vector3.up * _yOffset;
            float x = position.x + (Mathf.Abs(_yOffset + MarkerOffset) * Mathf.Cos(ConvertValueToAngle(value) * Mathf.Deg2Rad));
            float y = position.y + (Mathf.Abs(_yOffset + MarkerOffset) * Mathf.Sin(ConvertValueToAngle(value) * Mathf.Deg2Rad));
            mark.transform.position = new Vector3(x, y, 0);
            float angle = Mathf.Atan2(mark.transform.position.y - position.y, mark.transform.position.x - position.x) * Mathf.Rad2Deg;
            mark.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        private void SetMarkerOffset(ref float gaugeOffset, int markerValue)
        {
            float distanceFrom50 = Mathf.Abs(markerValue - 50);
            float maxDistance = _maxValue/2;
            float t = distanceFrom50 / maxDistance;
            gaugeOffset = Mathf.Lerp(0.25f, 0.1f, t);
        }
        public float ConvertValueToAngle(float value)
        {
            float ratio = ((90 - _rightAngleCramped) - (90 + _leftAngleCramped)) / _maxValue;
            return (90 - _rightAngleCramped) - ((_maxValue - value) * ratio);
        }

    }
}
