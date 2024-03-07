using System;
using System.Collections;
using _project.Scripts.Core;
using UnityEngine;

namespace _project.Scripts
{
    public class LastPhaseScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private GameObject _monsterInfoDisplay;
        [SerializeField] private DraggedMealScript _draggedMeal;
        [SerializeField] private Transform _uiTransform;
        [SerializeField] private Transform _decorTransform;
        [SerializeField] private Transform _meal;
        [SerializeField] private float _slideTime;
        [SerializeField] private float _zoomTime;
        [SerializeField] private AnimationCurve _uiSlideCurve;
        [SerializeField] private ReRoll _reRoll;

        private Vector2 _startBgScale;
        private Vector2 _startUiPos;

        public CookingManager Manager => _cookingManager;

        private void Start()
        {
            _startUiPos = _uiTransform.position;
            _startBgScale = _decorTransform.localScale;
        }


        public void GoNextPhase()
        {
            _cookingManager.Camera.NextPhase();
            StartCoroutine(SlideUi());
            _monsterInfoDisplay.SetActive(false);
        }

        private IEnumerator SlideUi()
        {
            float timer = 0;
            while (timer < _slideTime)
            {
                timer += Time.deltaTime;
                _uiTransform.position = Vector2.Lerp(_startUiPos, _startUiPos + Vector2.down * 30f,
                    _uiSlideCurve.Evaluate(timer / _slideTime));
                yield return new WaitForEndOfFrame();
            }
            
            timer = 0;
            Vector2 startMealPos = _meal.position;
            
            while (timer < _zoomTime)
            {
                timer += Time.deltaTime;
                _decorTransform.localScale = Vector2.Lerp(_startBgScale, _startBgScale * 3, timer / _zoomTime);
                _meal.position = Vector2.Lerp(startMealPos, startMealPos + Vector2.down * 1.2f, timer / _zoomTime);
                yield return new WaitForEndOfFrame();
            }
            _draggedMeal.EnableUse();
        }
        
        private IEnumerator EndFeedingPhaseRoutine()
        {
            float timer = 0;
            while (timer < _zoomTime)
            {
                timer += Time.deltaTime;
                _decorTransform.localScale = Vector2.Lerp( _startBgScale* 3, _startBgScale, timer / _zoomTime);
                yield return new WaitForEndOfFrame();
            }
            
            _cookingManager.Camera.NextPhase();


            while (_cookingManager.GetCurrentPhase() != PhaseCode.Phase2)
            {
                yield return new WaitForFixedUpdate();
            }

            _uiTransform.position = _startUiPos;
            _draggedMeal.ResetPosition();
            _draggedMeal.Activate();
            _reRoll.RedistributeCards();
        }

        public void EndFeedingPhase()
        {
            StartCoroutine(EndFeedingPhaseRoutine());
        }
    }
}
