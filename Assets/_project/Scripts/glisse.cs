using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glisse : MonoBehaviour
{
    public Transform teleportationZone; 
    public float moveSpeed = 5f; 

    private bool isMoving = false; 

    private void OnMouseDown()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToTeleportationZone());
        }
    }

    private IEnumerator MoveToTeleportationZone()
    {
        isMoving = true;

        Vector3 targetPosition = teleportationZone.position;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
