using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickUp : MonoBehaviour
{
    public Vector3 scaleMultiplier = new Vector3(1.5f, 1.5f, 1f);
    public float heightOffset = 1f;
    public float moveDuration = 1f; // Dur�e du mouvement

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private bool isScaled = false;

    // Liste des sprites agrandis
    private static List<clickUp> enlargedSprites = new List<clickUp>();

    void Start()
    {
        // Sauvegarde la position et l'�chelle initiales
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        // V�rifie si l'objet est d�j� agrandi
        if (!isScaled)
        {
            // V�rifie si la limite de sprites agrandis est atteinte
            if (enlargedSprites.Count >= 3)
            {
                // Si oui, r�tr�cit le premier sprite agrandi
                var firstSprite = enlargedSprites[0];
                firstSprite.Shrink();
                enlargedSprites.RemoveAt(0);
            }

            // Agrandit l'objet en fonction du facteur de multiplication
            transform.localScale = Vector3.Scale(initialScale, scaleMultiplier);
            // Lance la coroutine pour d�placer l'objet vers le haut de mani�re fluide
            StartCoroutine(MoveObject(transform.position, transform.position + Vector3.up * heightOffset, moveDuration));
            isScaled = true;

            // Ajoute ce sprite � la liste des sprites agrandis
            enlargedSprites.Add(this);
        }
        else
        {
            // R�tablit l'�chelle et la position initiales
            transform.localScale = initialScale;
            transform.position = initialPosition;
            isScaled = false;

            // Retire ce sprite de la liste des sprites agrandis
            enlargedSprites.Remove(this);
        }
    }

    void Shrink()
    {
        // R�tr�cit le sprite
        transform.localScale = initialScale;
        transform.position = initialPosition;
        isScaled = false;
    }

    IEnumerator MoveObject(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Utilise une courbe d'interpolation cubique B�zier pour un mouvement plus fluide
            float t = elapsedTime / duration;
            float smoothT = SmoothStep(t);
            transform.position = Vector3.Lerp(startPos, endPos, smoothT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Assure que l'objet soit bien positionn� � la fin du mouvement
        transform.position = endPos;
    }

    // Fonction pour une interpolation cubique B�zier
    float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
