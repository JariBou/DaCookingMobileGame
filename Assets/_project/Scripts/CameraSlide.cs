using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{    
    public Transform virtualCamera; // Référence à la virtual camera
    public bool moveRightOnClick = false; // Booléen pour déclencher le déplacement

    private bool isMoving = false; // Indique si la caméra est en cours de déplacement
    private Vector3 targetPosition; // Position cible de la caméra
    private float moveSpeed = 5f; // Vitesse de déplacement

    void Update()
    {
        // Vérifie si le booléen moveRightOnClick est vrai et si le bouton gauche de la souris est cliqué
        if (moveRightOnClick && Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Calcule la nouvelle position cible de la caméra
            targetPosition = virtualCamera.transform.position + new Vector3(35.59f, 0, 0);
            // Démarre le déplacement de la caméra
            isMoving = true;
        }

        // Si la caméra est en cours de déplacement
        if (isMoving)
        {
            // Déplace la caméra vers la nouvelle position
            virtualCamera.transform.position = Vector3.MoveTowards(virtualCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Vérifie si la caméra a atteint sa position cible
            if (virtualCamera.transform.position == targetPosition)
            {
                // Désactive le déplacement et réinitialise la position cible
                isMoving = false;
                targetPosition = Vector3.zero;
                // Désactive le booléen pour éviter les déplacements continus
                moveRightOnClick = false;
            }
        }
    }
}
