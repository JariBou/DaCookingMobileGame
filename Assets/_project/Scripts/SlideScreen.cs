using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _project.Scripts
{
    public class SlideScreen : MonoBehaviour
    {
        private bool _isSliding = false;
        private bool _isSetUp = true;
        [SerializeField] private DragAndDrop _dragAndDrop;
        [Range(0, 1)]
        public float _slideMultiplicator = 0.1f;
        [SerializeField] private AnimationCurve _slideCurve;
/*    [Header("Cinemachine")]
    [SerializeField] private float _cinemachineIntensity = 0.5f;
    [SerializeField] private float _cinemachineDuration = 0.5f;
    private float _shakeTimer;
    private float _shakeTimerMax;
    private float _shakeIntensity;*/

        private CinemachineVirtualCamera _virtualCamera;
        // Start is called before the first frame update
        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _isSetUp = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isSliding && Mathf.Abs(Pointer.current.delta.ReadValue().x) < Mathf.Abs(Pointer.current.delta.ReadValue().y))
            {
            
                float deltaY = Pointer.current.delta.ReadValue().y;
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Lerp(transform.position.y, transform.position.y - deltaY * _slideMultiplicator, _slideCurve.Evaluate(Time.deltaTime)), 0, 20), transform.position.z);
                if (transform.position.y != 20 || transform.position.y != 0) _isSetUp = false;
            }
            else if (!_isSliding && transform.position.y != 0 && transform.position.y != 20)
            {
                if (transform.position.y < 10 && !_isSetUp)
                {
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, 0, _slideCurve.Evaluate(Time.deltaTime)), transform.position.z);
                }
                else if (transform.position.y > 10 && !_isSetUp)
                {
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, 20, _slideCurve.Evaluate(Time.deltaTime)), transform.position.z);
                }
            } 
/*        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0)
            {
                _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
                    Mathf.Lerp(_shakeIntensity, 0, _shakeTimer/_shakeTimerMax);
                _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;

            }
        }
        if ((transform.position.y == 20 || transform.position.y == 0) && !_isSetUp)
        {
            _isSetUp = true;
            ShakeCamera(_cinemachineIntensity, _cinemachineDuration);
        }*/
        }

        public void OnSlide(InputAction.CallbackContext context)
        {
            if (context.performed && !_dragAndDrop.IsDragging)
            {
                _isSliding = true;
            }
            else if (context.canceled)
            {
                _isSliding = false;
            }
        }

/*    public void ShakeCamera(float intensity, float duration)
    {
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = duration;
        _shakeIntensity = intensity;
        _shakeTimerMax = duration;
        _shakeTimer = duration;
    }*/


    }
}
