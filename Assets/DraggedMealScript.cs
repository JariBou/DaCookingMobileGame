using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedMealScript : MonoBehaviour
{
    private Vector3 _initialPosition;
    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = _initialPosition;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
