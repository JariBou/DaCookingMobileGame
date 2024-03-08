using System;
using System.Collections;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class ClickableHeat : MonoBehaviour
    {
        [FormerlySerializedAs("imageComponent")][SerializeField] private Image _imageComponent;
        [SerializeField] private CookingMethod _cookingMethod;
        [SerializeField] private CookingPhaseScript _manager;
        [FormerlySerializedAs("grayDuration")][SerializeField, Range(0, 0.5f)] private float _grayDuration = 1f;
        [FormerlySerializedAs("grayColor")][SerializeField] private Color _grayColor = new Color(0.5f, 0.5f, 0.5f, 1f);

        [Header("Display")]
        [SerializeField] private TMP_Text _multiplierX;
        [SerializeField] private TMP_Text _multiplierY;
        [SerializeField] private TMP_Text _multiplierZ;
        private bool _isGrayed = false;

        [Header("GameFeel")]
        [SerializeField] private UnityEvent _OnHeatClick;

        [Header("Particle System")]
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Color _particleColor = Color.red;
        [SerializeField] private float _particleSpeed = 1f;
        [SerializeField] private float _particleSize = 1f;

        private bool _IsClick;
        public bool IsClick => _IsClick;

        private void Start()
        {
            Vector3 multipliers = GetMultipliers();
            _multiplierX.text = $"x{multipliers.x}";
            _multiplierY.text = $"x{multipliers.y}";
            _multiplierZ.text = $"x{multipliers.z}";
        }

        private void OnMouseDown()
        {
            if (_manager.CookingManager.GetCurrentPhase() != PhaseCode.Phase2) return;

            if (!_isGrayed)
            {
                StartCoroutine(GrayImage());
            }

            _manager.SelectedCookingMethod = _cookingMethod;
            _manager.UpdateMealDisplay();
            
            if (_OnHeatClick != null)
            {
                _OnHeatClick.Invoke();
            }

            var mainModule = _particleSystem.main;
            mainModule.startColor = _particleColor;
            mainModule.startSpeed = _particleSpeed;
            mainModule.startSize = _particleSize;

        }


        public Vector3 GetMultipliers()
        {
            return _manager.CookingParams.GetMultiplier(_cookingMethod);
        }

        private IEnumerator GrayImage()
        {
            _isGrayed = true;
            Debug.Log("L'image a  t  cliqu e !");

            _imageComponent.color = _grayColor;

            yield return new WaitForSeconds(_grayDuration);

            _imageComponent.color = Color.white;

            _isGrayed = false;
        }
    }
}
