using System;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Cards;
using _project.Scripts.Core;
using _project.Scripts.UI;
using MoreMountains.Tools;
using NaughtyAttributes;
using UnityEngine;

namespace _project.Scripts.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance { get; private set; }

        [SerializeField] private TutorialDialogDisplayScript _dialogDisplayScript;

        [SerializeField] private List<RoundInfo> _roundInfos;
        private int _roundNumber;
        
        private void Awake()
        {
            Instance ??= this;
            _roundNumber = 0;
        }

        public void NextRound()
        {
            _roundNumber++;
        }

        public RoundInfo GetCurrentRoundInfo()
        {
            Debug.Log($"RoundNumber: {_roundNumber} || RoundInfosCount: {_roundInfos.Count}");
            return _roundInfos[_roundNumber];
        }

        #region Static

        public static bool CanClickOnCardStatic(ClickUp card)
        {
            if (!IsPresent()) return true;
            return Instance.CanClickOnCard(card);
        }
        
        public static bool CanClickOnCookingMethodStatic(ClickableHeat cookingMethodSource)
        {
            if (!IsPresent()) return true;
            return Instance.CanClickOnCookingMethod(cookingMethodSource);
        }

        public static List<IngredientSo> GetRolledIngredientsStatic()
        {
            if (!IsPresent()) return null;
            return Instance.GetRolledIngredients();
        }
        
        /// <summary>
        /// Returns only the new ingredients considering that the good ones have been locked before
        /// So you should check before reRolling that the cards are locked
        /// </summary>
        /// <returns></returns>
        public static List<IngredientSo> GetReRolledIngredientsStatic()
        {
            if (!IsPresent()) return null;
            return Instance.GetReRolledIngredients();
        }
        
        
        public static bool IsPresent()
        {
            return Instance != null;
        }
        
        public static void NextRoundStatic()
        {
            if (!IsPresent()) return;
            Instance._roundNumber++;
        }

        public static RoundInfo GetCurrentRoundInfoStatic()
        {
            if (!IsPresent()) return null;
            return Instance.GetCurrentRoundInfo();
        }
        
        public static bool ShouldReroll()
        {
            if (!IsPresent()) return true;
            return Instance.GetCurrentRoundInfo().ShouldReroll;
        }

        #endregion
        
        private bool CanClickOnCard(ClickUp card)
        {
            RoundInfo roundInfo = GetCurrentRoundInfo();

            return roundInfo.CanSelectCard(card.Ingredient);
        }
        
        private bool CanClickOnCookingMethod(ClickableHeat cookingMethodSource)
        {
            RoundInfo roundInfo = GetCurrentRoundInfo();

            return roundInfo.CanSelectCookingMethod(cookingMethodSource.Method);
        }

        private List<IngredientSo> GetRolledIngredients()
        {
            RoundInfo roundInfo = GetCurrentRoundInfo();
            
            return roundInfo.GetAllIngredients();
        }
        
        /// <summary>
        /// Returns only the new ingredients considering that the good ones have been locked before
        /// So you should check before reRolling that the cards are locked
        /// </summary>
        /// <returns></returns>
        private List<IngredientSo> GetReRolledIngredients()
        {
            RoundInfo prevRoundInfo = _roundInfos[_roundNumber-1];
            RoundInfo roundInfo = GetCurrentRoundInfo();

            List<IngredientSo> prevRoundSelectedCards = prevRoundInfo.SelectableIngredients;
            List<IngredientSo> roundCards = roundInfo.GetAllIngredients();

            foreach (IngredientSo selectedIngredient in prevRoundSelectedCards)
            {
                roundCards.Remove(selectedIngredient);
            }
            
            return roundCards;
        }

    }

    [Serializable]
    public class RoundInfo
    {
        [SerializeField, InfoBox("Look at tooltips"), Tooltip("Selectable ingredients for this round")] private List<IngredientSo> _selectableIngredients;
        [SerializeField, Tooltip("Other non-selectable ingredients, total must add up to 6 ingredients")] private List<IngredientSo> _otherIngredients;

        [SerializeField, Tooltip("The cooking method selectable by the player this round")] private CookingMethod _selectableCookingMethod;

        [SerializeField, Tooltip("The Usable Condiment (if any) by the player")] private CondimentSo _usableCondiment;
        [SerializeField, Tooltip("If true, player will have to reroll this round (will go to the next round)")] private bool _shouldReroll;

        public List<IngredientSo> SelectableIngredients => _selectableIngredients;
        public List<IngredientSo> OtherIngredients => _otherIngredients;

        public bool ShouldReroll => _shouldReroll;

        //TODO: find a way to contain reroll Info
        // Probably will do like a different card round tbh

        public List<IngredientSo> GetAllIngredients()
        {
            List<IngredientSo> list = new List<IngredientSo>(SelectableIngredients);
            list.AddRange(OtherIngredients);
            list.MMShuffle();
            return list;
        }

        public bool CanSelectCard(IngredientSo ingredient)
        {
            return SelectableIngredients.Contains(ingredient);
        }
        
        public bool CanSelectCookingMethod(CookingMethod cookingMethod)
        {
            return _selectableCookingMethod == cookingMethod;
        }

        public bool CanUseCondiment(CondimentSo condiment)
        {
            return _usableCondiment != null && _usableCondiment == condiment;
        }
    }
}