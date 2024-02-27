using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class clickUp : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleMultiplier = new Vector3(1.5f, 1.5f, 1f);
    [SerializeField, Range(0,3)] private float _heightOffset = 1f;
    [SerializeField, Range(0, 3)] private float _moveSpeed = 1f; 
    public float MoveSpeed => _moveSpeed;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;
    private Vector3 _endPos;
    private Vector3 _endScale;
    public Vector3 InitialPosition => _initialPosition;
    public Vector3 InitialScale => _initialScale;
    private bool _isScaled = false;
    private bool _isMoving = false;

    private static List<clickUp> _enlargedSprites = new List<clickUp>();

    void Start()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
    }

    void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _endPos, Time.deltaTime * _moveSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, _endScale, Time.deltaTime * _moveSpeed);
            if (Vector3.Distance(transform.position, _endPos) < 0.001f)
            {
                _isMoving = false;
            }
        }
    }
    void OnMouseDown()
    {
        if (!_isScaled && !_enlargedSprites.Contains(this))
        {
            if (_enlargedSprites.Count >= 3)
            {
                var firstSprite = _enlargedSprites[0];
                firstSprite.StartMoving(firstSprite.InitialPosition, firstSprite.InitialScale);
                _enlargedSprites.RemoveAt(0);
            }

            StartMoving(transform.position + Vector3.up * _heightOffset, Vector3.Scale(_initialScale, _scaleMultiplier));
            _isScaled = true;
            _enlargedSprites.Add(this);
                      
        }
        else if (_isScaled && _enlargedSprites.Contains(this))
        {
            StartMoving(_initialPosition, _initialScale);
            _isScaled = false;
            _enlargedSprites.Remove(this);
                       
        }
    }

    public void StartMoving(Vector3 endPos, Vector3 scale)
    {
        _endPos = endPos;
        _endScale = scale;
        _isMoving = true;
    }


}
