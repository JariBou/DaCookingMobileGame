using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Pointer = UnityEngine.InputSystem.Pointer; // Pour éviter les ambiguités

public class SlideScreen : MonoBehaviour
{
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private bool _isDragging = false;
    [SerializeField] private GameObject _camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isDragging)
        {
            float deltaX = Pointer.current.delta.x.ReadValue();
            float deltaY = Pointer.current.delta.y.ReadValue();
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
               Vector3 _cameraPos = _camera.transform.position;
               _cameraPos.x = Mathf.Clamp(Mathf.Lerp(_camera.transform.position.x, _camera.transform.position.x + deltaX, 1f), -11, 11);
               _camera.transform.position = new Vector3(_cameraPos.x, 0, -1);
            }
            else
            {
                Vector3 _cameraPos = _camera.transform.position;
                _cameraPos.y = Mathf.Clamp(Mathf.Lerp(_camera.transform.position.y, _camera.transform.position.y + deltaY, 1f), 0, 20);
                _camera.transform.position = new Vector3(0, _cameraPos.y, -1); 
            }
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
       if (context.performed)
       {
            _startTouchPosition = Pointer.current.position.ReadValue();
            _isDragging = true;

       }
       else if (context.canceled)
       {
            _endTouchPosition = Pointer.current.position.ReadValue();
            if (_isDragging)
            {
                float deltaX = Pointer.current.delta.x.ReadValue();
                float deltaY = Pointer.current.delta.y.ReadValue();
                if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
                {
                    if (deltaX > 0)
                    {
                        Debug.Log("Right");
                    }
                    else
                    {
                        Debug.Log("Left");
                    }
                }
                else
                {
                    if (deltaY > 0)
                    {
                        Debug.Log("Up");
                    }
                    else
                    {
                        Debug.Log("Down");
                    }
                }
                _isDragging = false;
            }
        }
    }
}
