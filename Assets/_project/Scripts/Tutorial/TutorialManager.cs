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
        }

        #region Static

        public static bool CanClickOnCardStatic(ClickUp card)
        {
            if (Instance == null) return true;
            return Instance.CanClickOnCard(card);
        }
        
        public static bool CanClickOnCookingMethodStatic(ClickableHeat cookingMethodSource)
        {
            if (Instance == null) return true;
            return Instance.CanClickOnCookingMethod(cookingMethodSource);
        }

        public static List<IngredientSo> GetRolledIngredientsStatic()
        {
            if (Instance == null) return null;
            return Instance.GetRolledIngredients();
        }
        
        /// <summary>
        /// Returns only the new ingredients considering that the good ones have been locked before
        /// So you should check before reRolling that the cards are locked
        /// </summary>
        /// <returns></returns>
        public static List<IngredientSo> GetReRolledIngredientsStatic()
        {
            if (Instance == null) return null;
            return Instance.GetReRolledIngredients();
        }

        #endregion
        
        private bool CanClickOnCard(ClickUp card)
        {
            RoundInfo roundInfo = _roundInfos[_roundNumber];

            return roundInfo.CanSelectCard(card.Ingredient);
        }
        
        private bool CanClickOnCookingMethod(ClickableHeat cookingMethodSource)
        {
            RoundInfo roundInfo = _roundInfos[_roundNumber];

            return roundInfo.CanSelectCookingMethod(cookingMethodSource.Method);
        }

        private List<IngredientSo> GetRolledIngredients()
        {
            RoundInfo roundInfo = _roundInfos[_roundNumber];
            
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
            RoundInfo roundInfo = _roundInfos[_roundNumber];

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

        public List<IngredientSo> SelectableIngredients => _selectableIngredients;
        public List<IngredientSo> OtherIngredients => _otherIngredients;

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