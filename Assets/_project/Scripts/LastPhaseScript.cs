using System;
using System.Collections;
using System.Collections.Generic;
using _project.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class LastPhaseScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private DraggedMealScript _draggedMeal;
        [SerializeField] private BossScript _bossScript;
        [SerializeField] private Transform _uiTransform;
        [SerializeField] private Transform _decorTransform;
        [SerializeField] private Transform _meal;
        [SerializeField] private float _slideTime;
        [SerializeField] private float _zoomTime;
        [SerializeField] private AnimationCurve _uiSlideCurve;
        [SerializeField] private ReRoll _reRoll;
        [SerializeField] private List<DragableObject> _condiments;
        [SerializeField] private Button _button;

        private Vector2 _startBgScale;
        private Vector2 _startUiPos;

        public CookingManager Manager => _cookingManager;

        private void Start()
        {
            _startUiPos = _uiTransform.position;
            _startBgScale = _decorTransform.localScale;
        }

        public void PassBossScript(BossScript bossScript)
        {
            _bossScript = bossScript;
        }

        public void GoNextPhase()
        {
            _cookingManager.Camera.NextPhase();
            StartCoroutine(SlideUi());
        }

        private IEnumerator SlideUi()
        {
            _button.gameObject.SetActive(false);
            float timer = 0;
            while (timer < _slideTime)
            {
                timer += Time.deltaTime;
                _uiTransform.position = Vector2.Lerp(_startUiPos, _startUiPos + Vector2.down * 30f,
                    _uiSlideCurve.Evaluate(timer / _slideTime));
                yield return new WaitForEndOfFrame();
            }

            foreach (DragableObject condiment in _condiments)
            {
                condiment.DisableUse();
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
            _bossScript.ActivateFeeding();
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
            _reRoll.RedistributeCards();

            while (_cookingManager.GetCurrentPhase() != PhaseCode.Phase2)
            {
                yield return new WaitForFixedUpdate();
            }

            _uiTransform.position = _startUiPos;
            _draggedMeal.ResetPosition();
            _draggedMeal.Activate();
            _bossScript.DeactivateFeeding();
            foreach (DragableObject condiment in _condiments)
            {
                condiment.EnableUse();
            }
            _button.gameObject.SetActive(true);
        }

        public void EndFeedingPhase()
        {
            StartCoroutine(EndFeedingPhaseRoutine());
        }
    }
}
