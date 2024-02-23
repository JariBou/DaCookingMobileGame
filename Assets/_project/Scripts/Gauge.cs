using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    [Header("Gauge's Pivot")]
    [Range(0, -5)]
    [SerializeField] private float _yOffset;

    [Header("Gauge's Values")]
    [Range(0, 180)]
    [SerializeField] private float _leftAngleCramped = 0;
    [Range(0, 180)]
    [SerializeField] private float _rightAngleCramped = 180;
    [SerializeField] private float _maxValue = 100;
    [Range(0, 100)]
    [SerializeField] private float _currentValue = 0;
    private float _previousValue = 0;

    [Header("Gauge's Needle")]
    [SerializeField] private GameObject _needle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * _yOffset, 0.2f);
        Gizmos.color = Color.red;
    }

    private void OnValidate()
    {
        Vector3 position = transform.position + Vector3.up * _yOffset;
        float x = position.x + (Mathf.Abs(_yOffset) * Mathf.Cos(ConvertValueToAngle(_currentValue) * Mathf.Deg2Rad));
        float y = position.y + (Mathf.Abs(_yOffset) * Mathf.Sin(ConvertValueToAngle(_currentValue) * Mathf.Deg2Rad));
        _needle.transform.position = new Vector3(x, y, 0);
        float angle = Mathf.Atan2(_needle.transform.position.y - position.y, _needle.transform.position.x - position.x) * Mathf.Rad2Deg;
        _needle.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        
    }


    public float ConvertValueToAngle(float value)
    {
        float ratio = ((90 - _rightAngleCramped) - (90 + _leftAngleCramped)) / _maxValue;
        return (90 - _rightAngleCramped) - ((_maxValue - value) * ratio);
    }

}
