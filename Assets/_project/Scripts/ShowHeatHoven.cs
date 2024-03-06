using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHeatHoven : MonoBehaviour
{
    public SpriteRenderer[] spritesToToggle; // R�f�rence aux sprites que vous souhaitez rendre visibles
    public SpriteRenderer defaultSprite; // Sprite par d�faut � afficher

    private void Start()
    {
        // Assurez-vous que le sprite par d�faut est visible et que les autres sont invisibles au d�but
        foreach (var sprite in spritesToToggle)
        {
            sprite.gameObject.SetActive(sprite == defaultSprite);
        }
    }

    private void OnMouseDown()
    {
        // Parcours tous les sprites et rend le sprite cliqu� visible et les autres invisibles
        foreach (var sprite in spritesToToggle)
        {
            sprite.gameObject.SetActive(sprite == GetComponent<SpriteRenderer>());
        }
    }
}
