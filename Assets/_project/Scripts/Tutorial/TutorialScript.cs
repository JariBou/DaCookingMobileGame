using System;
using System.Collections.Generic;
using _project.Scripts.Meals;
using _project.Scripts.Phases;
using _project.Scripts.UI;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts.Tutorial
{
    public class TutorialScript : MonoBehaviour
    {
        [SerializeField] private TutorialDialogDisplayScript _dialogDisplayScript;

        [SerializeField] private List<DialogInfo> _dialogInfos;
        private int _currDialogIndex;

        private void Start()
        {
            ActivateTutorial();
        }


        [Button]
        public void NextDialog()
        {
            _currDialogIndex++;
            DialogInfo dialogInfo = GetCurrentDialogInfo();

            if (dialogInfo.GetActionState == DialogInfo.ActionState.Disable)
            {
                _dialogDisplayScript.Disable();
            }
            else
            {
                _dialogDisplayScript.UpdateInfo(dialogInfo);
                _dialogDisplayScript.Enable();
            }
            
        }

        [Button]
        public void ActivateTutorial()
        {
            DialogInfo dialogInfo = _dialogInfos[_currDialogIndex];
            _dialogDisplayScript.UpdateInfo(dialogInfo).Enable();
        }

        public DialogInfo GetCurrentDialogInfo()
        {
            return _dialogInfos[_currDialogIndex];
        }
        
        private void OnMealConfirm()
        {
            NextDialog();
            // TODO
        }

        private void OnCookingMethodSelect()
        {
            NextDialog();
            // TODO
        }

        private void OnSeasoningAdded()
        {
            //NextDialog();
            // TODO
        }

        private void OnMealFed(Meal meal, bool satisfied, int numberOfMeals, bool rerolledForMeal)
        {
            //NextDialog();
            // TODO
        }
        
        private void OnEnable()
        {
            RecipeDisplayScript.MealConfirm += OnMealConfirm;
            CookingPhaseScript.CookingMethodConfirm += OnCookingMethodSelect;
            SeasoningScript.SeasoningAdded += OnSeasoningAdded;
            CookingManager.MealFed += OnMealFed;
        }
        
        private void OnDisable()
        {
            RecipeDisplayScript.MealConfirm -= OnMealConfirm;
            CookingPhaseScript.CookingMethodConfirm -= OnCookingMethodSelect;
            SeasoningScript.SeasoningAdded -= OnSeasoningAdded;
            CookingManager.MealFed -= OnMealFed;
        }


        // TODO: script of tutorial, like when to change dialogs and stuff
    }

    [Serializable]
    public class DialogInfo
    {
        [SerializeField] private ActionState _actionState;
        [SerializeField] private String _title;
        [SerializeField, TextArea] private String _dialogText;
        [SerializeField] private Sprite _catImage;
        [SerializeField] private bool _shouldVeil;
        [SerializeField] private bool _hasButton;
        [SerializeField] private Vector3 _position;

        public string Title => _title;
        public string DialogText => _dialogText;
        public Sprite CatImage => _catImage;
        public bool ShouldVeil => _shouldVeil;
        public bool HasButton => _hasButton;
        public Vector3 Position => _position;

        public ActionState GetActionState => _actionState;

        public enum ActionState
        {
            Enable, Disable
        }
    }
}