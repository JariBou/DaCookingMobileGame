using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class clickUp : MonoBehaviour
{
    public Vector3 scaleMultiplier = new Vector3(1.5f, 1.5f, 1f);
    public float heightOffset = 1f;
    public float moveDuration = 1f; 

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private bool isScaled = false;

    private static List<clickUp> enlargedSprites = new List<clickUp>();

    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (!isScaled)
        {
            if (enlargedSprites.Count >= 3)
            {
                var firstSprite = enlargedSprites[0];
                firstSprite.Shrink();
                enlargedSprites.RemoveAt(0);
            }

            transform.localScale = Vector3.Scale(initialScale, scaleMultiplier);
            StartCoroutine(MoveObject(transform.position, transform.position + Vector3.up * heightOffset, moveDuration));
            isScaled = true;

            enlargedSprites.Add(this);
                      
        }
        else
        {
            transform.localScale = initialScale;
            transform.position = initialPosition;
            isScaled = false;

            enlargedSprites.Remove(this);
                       
        }
    }

    void Shrink()
    {
        transform.localScale = initialScale;
        transform.position = initialPosition;
        isScaled = false;
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
