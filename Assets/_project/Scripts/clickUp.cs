using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using _project.ScriptableObjects.Scripts;
using System;

[Serializable]
public class clickUp : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier = 1.25f;
    [SerializeField, Range(0,4)] private float _heightOffset = 2f;
    [SerializeField, Range(1, 10)] private float _moveSpeed = 5f;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;
    public Vector3 InitialPosition => _initialPosition;
    public Vector3 InitialScale => _initialScale;


    private Vector3 _endPos;
    private Vector3 _endScale;

    private bool _isScaled = false;
    private bool _isMoving = false;

    public IngredientSo _ingredientSo;

    public static List<clickUp> _enlargedSprites = new List<clickUp>();

    private Menu _menu;

    void Start()
    {
        _ingredientSo = GetComponent<Card>()._ingredientSo;
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
        _menu = FindFirstObjectByType<Menu>();
    }

    void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _endPos, Time.deltaTime * _moveSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, _endScale, Time.deltaTime * _moveSpeed);
            if (Vector3.Distance(transform.position, _endPos) < 0.001f)
            {
                transform.position = _endPos;
                transform.localScale = _endScale;
                _isMoving = false;
            }
        }
    }
    void OnMouseDown()
    {
        if (!_isScaled && !_isMoving)
        {
            if (_enlargedSprites.Count >= 3)
            {
                _enlargedSprites[0].StartMoving(_enlargedSprites[0].InitialPosition, _enlargedSprites[0].InitialScale);
                _enlargedSprites.RemoveAt(0);
                _menu.UpdateMenu();
            }

            StartMoving(transform.position + Vector3.up * _heightOffset, new Vector3(transform.localScale.x * _scaleMultiplier, transform.localScale.y * _scaleMultiplier, transform.localScale.z));
            _isScaled = true;
            _enlargedSprites.Add(this);
            _menu.UpdateMenu();
                      
        }
        else if (_isScaled && !_isMoving)
        {
            StartMoving(_initialPosition, _initialScale);
            _isScaled = false;
            _enlargedSprites.Remove(this);
            _menu.UpdateMenu();
                       
        }
    }

    public void StartMoving(Vector3 endPos, Vector3 scale)
    {
        _endPos = endPos;
        _endScale = scale;
        _isMoving = true;
    }


}
