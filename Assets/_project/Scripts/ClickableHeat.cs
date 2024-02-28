using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableHeat : MonoBehaviour
{
    [SerializeField] private Image imageComponent;
    [SerializeField, Range(0, 0.5f)] private float grayDuration = 1f;
    [SerializeField] private Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    private bool isGrayed = false;

    private void OnMouseDown()
    {
        if (!isGrayed)
        {
            StartCoroutine(GrayImage());
        }
    }

    private IEnumerator GrayImage()
    {
        isGrayed = true;
        Debug.Log("L'image a été cliquée !");

        imageComponent.color = grayColor;

        yield return new WaitForSeconds(grayDuration);

        imageComponent.color = Color.white;

        isGrayed = false;
    }
}
