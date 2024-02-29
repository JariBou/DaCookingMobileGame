using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{    
    public Transform virtualCamera; // R�f�rence � la virtual camera
    public bool moveRightOnClick = false; // Bool�en pour d�clencher le d�placement

    private bool isMoving = false; // Indique si la cam�ra est en cours de d�placement
    private Vector3 targetPosition; // Position cible de la cam�ra
    private float moveSpeed = 5f; // Vitesse de d�placement

    void Update()
    {
        // V�rifie si le bool�en moveRightOnClick est vrai et si le bouton gauche de la souris est cliqu�
        if (moveRightOnClick && Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Calcule la nouvelle position cible de la cam�ra
            targetPosition = virtualCamera.transform.position + new Vector3(35.59f, 0, 0);
            // D�marre le d�placement de la cam�ra
            isMoving = true;
        }

        // Si la cam�ra est en cours de d�placement
        if (isMoving)
        {
            // D�place la cam�ra vers la nouvelle position
            virtualCamera.transform.position = Vector3.MoveTowards(virtualCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // V�rifie si la cam�ra a atteint sa position cible
            if (virtualCamera.transform.position == targetPosition)
            {
                // D�sactive le d�placement et r�initialise la position cible
                isMoving = false;
                targetPosition = Vector3.zero;
                // D�sactive le bool�en pour �viter les d�placements continus
                moveRightOnClick = false;
            }
        }
    }
}
