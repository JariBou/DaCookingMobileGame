using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHeatHoven : MonoBehaviour
{
    public SpriteRenderer[] spritesToToggle; // Référence aux sprites que vous souhaitez rendre visibles
    public SpriteRenderer defaultSprite; // Sprite par défaut à afficher

    private void Start()
    {
        // Assurez-vous que le sprite par défaut est visible et que les autres sont invisibles au début
        foreach (var sprite in spritesToToggle)
        {
            sprite.gameObject.SetActive(sprite == defaultSprite);
        }
    }

    private void OnMouseDown()
    {
        // Parcours tous les sprites et rend le sprite cliqué visible et les autres invisibles
        foreach (var sprite in spritesToToggle)
        {
            sprite.gameObject.SetActive(sprite == GetComponent<SpriteRenderer>());
        }
    }
}
