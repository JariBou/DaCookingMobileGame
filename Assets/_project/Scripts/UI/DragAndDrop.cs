using System;
using System.Collections;
using _project.Scripts.Core;
using _project.Scripts.Gauges;
using _project.Scripts.Meals;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class DragAndDrop : MonoBehaviour
    {
        public static DragAndDrop instance;
        [SerializeField] private LayerMask _layerMask = 0;
        [SerializeField, Range(1f, 2f)] private float _scaleOnDrag = 1.5f;
        [SerializeField] private LayerMask _layerMaskCondiment = 0;
        
        private bool _onBoss = false;
        [SerializeField] private CookingManager _cookingManager;
        public bool IsDragging => _isDragging;
        
        [SerializeField, Range(0, 1)]
        private float _lerpValue = 0.1f;
        [SerializeField] private GaugeHandler _gaugeHandler;
        [SerializeField] private InputAction _touch, _screenTouchPosition, _click, _screenClickPosition;
        // Start is called before the first frame update

        [SerializeField] private Vector2 _currentMousePosition;
        public Vector2 CurrentMousePosition => _currentMousePosition;
        [SerializeField] private Vector2 _currentTouchPosition;
        public Vector2 CurrentTouchPosition => _currentTouchPosition;
        private Collider2D _hit;
        /*[SerializeField]*/ private bool _isDragging = false;
        [SerializeField] private bool _isTouch = false;
        public bool IsTouch => _isTouch;

        private bool _canEndPhase = false;
        private DraggedMealScript _draggedMealScript;

        [Header("GameFeel")]
        [SerializeField] private UnityEvent _OnDrag, _OnDrop;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            _touch.Enable();
            _screenTouchPosition.Enable();
            _screenTouchPosition.performed += context => { _currentTouchPosition = context.ReadValue<Vector2>(); };

            #if UNITY_EDITOR
            _click.Enable();
            _screenClickPosition.Enable();
            _screenClickPosition.performed += context => { _currentMousePosition = context.ReadValue<Vector2>(); };
            _click.performed += _ => OnPointerDownHandler(_);
            _click.canceled += _ => OnPointerDownHandler(_);
            #endif
            _touch.performed += _ =>
            {
                _isTouch = true;
                #if UNITY_EDITOR
                _click.Disable();
                #endif
                OnPointerDownHandler(_);
                
            };
            _touch.canceled += _ =>
            {
                _isTouch = false;
                #if UNITY_EDITOR
                _click.Enable();
                #endif
                OnPointerDownHandler(_);
            };




        }


        // Update is called once per frame
        private void Update()
        {
            if (_isDragging)
            {
                _hit.transform.localScale = new Vector3(_scaleOnDrag, _scaleOnDrag, 1);
                MouseScreenCheck(_hit);
            }
            if (_canEndPhase && _gaugeHandler.AllGaugesAreSetUp())
            {
                _canEndPhase = false;
                _draggedMealScript.EndPhase();
            }
        }

        private void MouseScreenCheck(Collider2D hitObject)
        {
            if (OptionMenu.Instance.IsOptionPanelOpen || !_isDragging) return;
            Vector2 screenPointerPosition;

#if UNITY_EDITOR
            screenPointerPosition = (_isTouch) ? _currentTouchPosition : _currentMousePosition;
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
            screenPointerPosition = _currentTouchPosition;
            hitObject.transform.position = new Vector3(Mathf.Lerp(_hit.transform.position.x, Camera.main.ScreenToWorldPoint(screenPointerPosition).x, _lerpValue), Mathf.Lerp(_hit.transform.position.y, Camera.main.ScreenToWorldPoint(screenPointerPosition).y, _lerpValue), hitObject.transform.position.z);
#endif
        }
        private Vector2 _initialPosition;
        private Vector3 _initialScale;
        public void OnPointerDownHandler(InputAction.CallbackContext context)
        {
        #if UNITY_EDITOR
            Vector2 pointerPosition = _isTouch ? Touchscreen.current.position.ReadValue() : Mouse.current.position.ReadValue();
        #else
            Vector2 pointerPosition = _currentTouchPosition;
        #endif
            if (context.performed)
            {
                Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pointerPosition), (int)_layerMask);
                if (OptionMenu.Instance.IsOptionPanelOpen)
                {
                    IsTouchingOrClickingThisLayer(OptionMenu.Instance._backgroundOptionLayer, _isTouch);
                    return;
                }
                if (hit != null)
                {
                    _initialPosition = hit.transform.position;
                    _initialScale = hit.transform.localScale;
                    _isDragging = true;
                    _OnDrag?.Invoke();
                    _hit = hit;
                    try
                    {
                       _hit.GetComponent<SeasoningScript>().ParticleSystem.Play();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                    Image hitImage = _hit.GetComponent<Image>();
                    _hit.GetComponent<Image>().color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b,0.5f);
                    //Effet sonore à rajouter pour le ramassage de l'objet
                }
            }
            else if (context.canceled)
            {
                if (!OptionMenu.Instance.IsOptionPanelOpen && !OptionMenu.Instance._settingsButton.raycastTarget) OptionMenu.Instance._settingsButton.raycastTarget = true;
                if (_hit == null) return;
                _isDragging = false;
                _OnDrop?.Invoke();
                if (_hit != null) _hit.transform.localScale = _initialScale;

                Collider2D hitObject = Physics2D.OverlapPoint(_hit.transform.position, (int)_layerMaskCondiment);
                if (hitObject != null)
                {
                    /*Debug.Log("Yes");*/
                    if (hitObject.gameObject.CompareTag("Seosoning"))
                    {
                        AddCond();
                    }
                    else if (hitObject.gameObject.CompareTag("Boss"))
                    {
                        Dropped(_hit.gameObject);
                        _hit = null;
                        
                        return;
                    }
                    //Effet sonore à rajouter pour le lâché de l'objet
                }
                try
                {
                    _hit.transform.position = _hit.GetComponent<SeasoningScript>().InitialPosition;
                    _hit.GetComponent<SeasoningScript>().ParticleSystem.Stop();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                Image hitImage = _hit.GetComponent<Image>();
                _hit.GetComponent<Image>().color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b, 1);
                _hit = null;

                //Effet sonore à rajouter pour le lâché de l'objet
            }

        }


        public void IsTouchingOrClickingThisLayer(LayerMask _layer, bool _isTouchingScreen)
        {
            Vector2 pointerPosition = (_isTouchingScreen) ? Touchscreen.current.position.ReadValue() : Mouse.current.position.ReadValue();
/*            Debug.Log(pointerPosition +" "+_isTouchingScreen);*/
            Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pointerPosition), (int)_layer);
            if (hit == null)
            {
                OptionMenu.Instance.CloseOptionPanel();
            }
        }

        private void AddCond()
        {
           /* Debug.Log("Add cond");*/
            _hit.GetComponent<SeasoningScript>().AddSeosoning();
        }

        private void Dropped(GameObject hitGameObject)
        {
            /*Debug.Log("Dropped");*/
            bool result = _cookingManager.FeedMeal();
            
            Image hitImage = hitGameObject.GetComponent<Image>();
            hitImage.color = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b, 1);

            _draggedMealScript = hitGameObject.GetComponent<DraggedMealScript>();
            _draggedMealScript.Deactivate();
            if (!result)
            {
                _canEndPhase = true;
            }
        }
    }
}
