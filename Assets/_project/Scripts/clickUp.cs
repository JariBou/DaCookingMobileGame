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
    [SerializeField, Range(0, 3)] private float _moveDuration = 1f; 

    private Vector3 _initialScale;
    private Vector3 _initialPosition;
    private bool _isScaled = false;

    private static List<clickUp> _enlargedSprites = new List<clickUp>();

    void Start()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (!_isScaled)
        {
            if (_enlargedSprites.Count >= 3)
            {
                var firstSprite = _enlargedSprites[0];
                firstSprite.Shrink();
                _enlargedSprites.RemoveAt(0);
            }

            transform.localScale = Vector3.Scale(_initialScale, _scaleMultiplier);
            StartCoroutine(MoveObject(transform.position, transform.position + Vector3.up * _heightOffset, _moveDuration));
            _isScaled = true;

            _enlargedSprites.Add(this);
                      
        }
        else
        {
            transform.localScale = Vector3.Scale(_initialScale, _scaleMultiplier *-1);
            StartCoroutine(MoveObject(transform.position, _initialPosition, _moveDuration));
            _isScaled = false;

            _enlargedSprites.Remove(this);
                       
        }
    }

    void Shrink()
    {
        transform.localScale = Vector3.Scale(_initialScale, _scaleMultiplier * -1);
        StartCoroutine(MoveObject(transform.position, _initialPosition, _moveDuration));
        _isScaled = false;
    }

    IEnumerator MoveObject(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float smoothT = SmoothStep(t);
            transform.position = Vector3.Lerp(startPos, endPos, smoothT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }

    float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
