using System;
using GraphicsLabor.Scripts.Core.Tags;
using GraphicsLabor.Scripts.Core.Tags.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using GraphicsLabor.Scripts.Core.Utility;
using TMPro;
using System.Collections;

namespace _project.Scripts
{
    public class DragAndDrop : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask = 0;
        [SerializeField, Range(1f, 2f)] private float _scaleOnDrag = 1.5f;
        [SerializeField] private LayerMask _layerMaskCondiment = 0;
        
        private bool _onBoss = false;
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private TMP_Text _bossStateText;
        public bool IsDragging => _isDragging;
        
        [SerializeField, Range(0, 1)]
        private float _lerpValue = 0.1f;
        [SerializeField] private GaugeHandler _gaugeHandler;
        [SerializeField] private InputAction _press, _screenMousePosition, _click, _screenTouchPosition;
        // Start is called before the first frame update

        [SerializeField] private Vector2 _currentMousePosition;
        [SerializeField] private Vector2 _currentTouchPosition;
        [SerializeField] private Vector2 _lastTouchScreenPosition;
        [SerializeField] private Collider2D _hit;
        /*[SerializeField]*/ private bool _isDragging = false;
        /*[SerializeField]*/ private bool _isTouch = false;
        private void Awake()
        {
            _press.Enable();
            _click.Enable();
            _screenMousePosition.Enable();
            _screenTouchPosition.Enable();

            _screenMousePosition.performed += context => { _currentMousePosition = context.ReadValue<Vector2>();};
            /*_screenTouchPosition.performed += context => { _currentTouchPosition = context.ReadValue<Vector2>();};*/ // J'update la position du touch dans l'update pour éviter un bug avec le drag and drop


            _press.performed += _ => OnTouchHandler(_); 
            _press.canceled += _ => OnTouchHandler(_);


            _click.performed += _ => OnClickHandler(_);
            _click.canceled += _ => OnClickHandler(_);

        }


        // Update is called once per frame
        private void Update()
        {
            if (_isTouch)
            {
                _currentTouchPosition = Touchscreen.current.position.ReadValue();
            }
            else if (!_isTouch)
            {
                _currentTouchPosition = Vector2.zero;
            }
            if (_isDragging)
            {
                _hit.transform.localScale = new Vector3(_scaleOnDrag, _scaleOnDrag, 1);
                MouseScreenCheck(_hit);
                if (_currentMousePosition.x != 0 || _currentMousePosition.y != 0)
                {
                    //Effet sonore à ajouter pour le mouvement de l'objet
                }
            }
        }

        private void MouseScreenCheck(Collider2D hitObject)
        {
            Vector2 screenPointerPosition = (_isTouch && _isDragging) ? _currentTouchPosition : _currentMousePosition;
            
#if UNITY_EDITOR
            if (screenPointerPosition.x < 0) 
                hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, 0 - Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).x, _lerpValue), hitObject.transform.position.y, hitObject.transform.position.z);
            else if (screenPointerPosition.x > Handles.GetMainGameViewSize().x)
                hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).x, _lerpValue), hitObject.transform.position.y, hitObject.transform.position.z);
            else 
                hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(screenPointerPosition).x, _lerpValue), hitObject.transform.position.y, hitObject.transform.position.z);

            if (screenPointerPosition.y < 0)
                hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, 0 - Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).y, _lerpValue), hitObject.transform.position.z);
            else if (screenPointerPosition.y > Handles.GetMainGameViewSize().y - 1)
                hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(Handles.GetMainGameViewSize()).y - 1, _lerpValue), hitObject.transform.position.z);
            else 
                hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(screenPointerPosition).y, _lerpValue), hitObject.transform.position.z);
#else
        Vector2 ScreenWidthHeight = new Vector2(Screen.width, Screen.height);
        if (screenMousePosition.x < 0)
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, 0 - Camera.main.ScreenToWorldPoint(ScreenWidthHeight).x, _lerpValue), hitObject.transform.position.y, hitObject.transform.position.z);
        else if (screenMousePosition.x > Screen.width)
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(ScreenWidthHeight).x, _lerpValue), hitObject.transform.position.y, hitObject.transform.position.z);
        else 
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(screenMousePosition).x, _lerpValue), hitObject.transform.position.y, hitObject.transform.position.z);

        if (screenMousePosition.y < 0)
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, 0 - Camera.main.ScreenToWorldPoint(ScreenWidthHeight).y, _lerpValue), hitObject.transform.position.z);
        else if (screenMousePosition.y > Screen.height - 1)
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(ScreenWidthHeight).y - 1, _lerpValue), hitObject.transform.position.z);
        else 
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(screenMousePosition).y, _lerpValue), hitObject.transform.position.z);
#endif
        }
        private Vector2 _initialPosition;
        public void OnClickHandler(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                /*Debug.Log("Click");*/
                Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(_currentMousePosition), (int)_layerMask);
                if (hit != null)
                {
                    _initialPosition = hit.transform.position;
                    Debug.Log("Hit");
                    _isDragging = true;
                    _hit = hit;
                    Image hitImage = _hit.GetComponent<Image>();
                    _hit.GetComponent<Image>().color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b,0.2f);
                    //Effet sonore à rajouter pour le ramassage de l'objet
                }
            }
            else if (context.canceled)
            {
                /*Debug.Log("Unclick");*/
                if (_hit == null) return;
                _isDragging = false;
                if (_hit != null) _hit.transform.localScale = new Vector3(1, 1, 1);

                try
                {
                    _hit.transform.position = _hit.GetComponent<DragableObject>().InitialPosition;
                }
                catch (Exception e)
                {
                  
                }
                
                Collider2D hitObject = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(_currentMousePosition), (int)_layerMaskCondiment);
                if (hitObject != null)
                {
                    /*Debug.Log("Yes");*/
                    if (hitObject.gameObject.CompareTag("MinusButton"))
                    {
                        Negative();
                    }
                    else if (hitObject.gameObject.CompareTag("PlusButton"))
                    {
                        Positive();
                    }
                    else if (hitObject.gameObject.CompareTag("Seosoning"))
                    {
                        AddCond();
                    }
                    else if (hitObject.gameObject.CompareTag("Boss"))
                    {
                        Dropped(_hit.gameObject);
                        return;
                    }
                    //Effet sonore à rajouter pour le lâché de l'objet
                }
                Image hitImage = _hit.GetComponent<Image>();
                _hit.GetComponent<Image>().color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b, 1);
                _hit = null;
                
                

                //Effet sonore à rajouter pour le lâché de l'objet
            }

        }

        public void OnTouchHandler(InputAction.CallbackContext context)
        {
            
            if (context.performed)
            {
                _isTouch = true;
                Debug.Log("Touch");
                Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(_currentTouchPosition), (int)_layerMask);
                if (hit != null)
                {
                    _initialPosition = hit.transform.position;
                    Debug.Log("Hit");
                    _isDragging = true;
                    _hit = hit;
                    Image hitImage = _hit.GetComponent<Image>();
                    _hit.GetComponent<Image>().color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b, 0.2f);
                    //Effet sonore à rajouter pour le ramassage de l'objet
                }
            }
            else if (context.canceled)
            {
                _lastTouchScreenPosition = _currentTouchPosition;
                _isTouch = false;
                _isDragging = false;
                /*Debug.Log("Untouch");*/
                if (_hit == null) return;

                if (_hit != null) _hit.transform.localScale = new Vector3(1, 1, 1);

                try
                {
                    _hit.transform.position = _hit.GetComponent<DragableObject>().InitialPosition;
                }
                catch (Exception e)
                {

                }
                Debug.Log(_lastTouchScreenPosition);
                Collider2D hitObject = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(_lastTouchScreenPosition), (int)_layerMaskCondiment);

                if (hitObject != null)
                {
                    /*Debug.Log("Yes");*/
                    if (hitObject.gameObject.CompareTag("MinusButton"))
                    {
                        Negative();
                    }
                    else if (hitObject.gameObject.CompareTag("PlusButton"))
                    {
                        Positive();
                    }
                    else if (hitObject.gameObject.CompareTag("Seosoning"))
                    {
                        AddCond();
                    }
                    else if (hitObject.gameObject.CompareTag("Boss"))
                    {
                        Dropped(_hit.gameObject);
                        return;
                    }
                    //Effet sonore à rajouter pour le lâché de l'objet
                }
                Image hitImage = _hit.GetComponent<Image>();
                _hit.GetComponent<Image>().color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b, 1);
                _hit = null;

                //Effet sonore à rajouter pour le lâché de l'objet
            }

        }

        private void AddCond()
        {
           /* Debug.Log("Add cond");*/
            _hit.GetComponent<DragableObject>().AddSeosoning();
        }

        private void Positive()
        {
            /*Debug.Log("Positive");*/
            _hit.GetComponent<DragableObject>().AddSeosoning(1);
        }
        private void Negative()
        {
            /*Debug.Log("Negative");*/
            _hit.GetComponent<DragableObject>().AddSeosoning(-1);
        }

        private void Dropped(GameObject hitGameObject)
        {
            bool result = _cookingManager.FeedMeal();

            _bossStateText.text = result ? "Monster Satisfied GG" : "Monster not happy";
            _bossStateText.color = result ? Color.green : Color.red;
            _bossStateText.gameObject.SetActive(true);
            
            hitGameObject.GetComponent<DraggedMealScript>().Deactivate();
        }

        private void OnMouseDrag()
        {
            if (_isDragging)
            {
                Vector2 screenMousePosition = Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
                _hit.transform.position = new Vector3(screenMousePosition.x, screenMousePosition.y, _hit.transform.position.z);
            }
        }
    }
}
