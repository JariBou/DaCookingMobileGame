using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask = 0;
    private bool _isDragging = false;
    private Collider2D _hit;
    [Range(0, 1)]
    public float _lerpValue = 0.1f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (_isDragging)
        {
            Vector2 ScreenMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _hit.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            MouseScreenCheck(_hit);
            if (Mouse.current.delta.ReadValue().x != 0 || Mouse.current.delta.ReadValue().y != 0)
            {
                //Effet sonore à ajouter pour le mouvement de l'objet
            }
        }
    }

    private void MouseScreenCheck(Collider2D hitObject)
    {
        Vector2 screenMousePosition = Mouse.current.position.ReadValue();
#if UNITY_EDITOR
        if (screenMousePosition.x < 0) 
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, 0 - Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).x, _lerpValue), hitObject.transform.position.y, 0);
        else if (screenMousePosition.x > Handles.GetMainGameViewSize().x)
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).x, _lerpValue), hitObject.transform.position.y, 0);
        else 
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(screenMousePosition).x, _lerpValue), hitObject.transform.position.y, 0);

        if (screenMousePosition.y < 0)
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, 0 - Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).y, _lerpValue), 0);
        else if (screenMousePosition.y > Handles.GetMainGameViewSize().y)
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).y, _lerpValue), 0);
        else 
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(screenMousePosition).y, _lerpValue), 0);
#else
        Vector2 ScreenWidthHeight = new Vector2(Screen.width, Screen.height);
        if (screenMousePosition.x < 0)
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, 0 - Camera.main.ScreenToWorldPoint(ScreenWidthHeight).x, _lerpValue), hitObject.transform.position.y, 0);
        else if (screenMousePosition.x > Screen.width)
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(ScreenWidthHeight).x, _lerpValue), hitObject.transform.position.y, 0);
        else 
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(screenMousePosition).x, _lerpValue), hitObject.transform.position.y, 0);

        if (screenMousePosition.y < 0)
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, 0 - Camera.main.ScreenToWorldPoint(ScreenWidthHeight).y, _lerpValue), 0);
        else if (screenMousePosition.y > Screen.height)
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(ScreenWidthHeight).y, _lerpValue), 0);
        else 
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(screenMousePosition).y, _lerpValue), 0);
#endif
    }
    public void OnClickHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), (int)_layerMask);
            if (hit != null)
            {
                _isDragging = true;
                _hit = hit;
                //Effet sonore à rajouter pour le ramassage de l'objet
            }
        }
        else if (context.canceled)
        {
            _isDragging = false;
            if (_hit != null) _hit.transform.localScale = new Vector3(1, 1, 1);
            //Effet sonore à rajouter pour le lâché de l'objet
        }

    }   
}
