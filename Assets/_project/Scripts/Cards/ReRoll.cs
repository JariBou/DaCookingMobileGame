using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _project.ScriptableObjects.Scripts;
using _project.Scripts.Core;
using _project.Scripts.Phases;
using _project.Scripts.Tutorial;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using _project.Scripts.UI;
using TMPro;
using UnityEngine.UI;

namespace _project.Scripts.Cards
{
    public class ReRoll : MonoBehaviour
    {

        [SerializeField] private MonsterInstance _monsterInstance;
        [SerializeField] private ClickUp[] _cards;
        [SerializeField] private RecipeDisplayScript _recipeDisplayScript;
        private int MaxRerollChances => _monsterInstance.MaxNumberOfRerolls;
        
        private int RerollCount => _monsterInstance.NumberOfRerolls;
        private bool _isRerolling = false;
        [FormerlySerializedAs("_OnReRoll")] [SerializeField] private UnityEvent _onReRoll;
        private SpriteRenderer _spriteRenderer;
        
        [Header("Buttons")]
        [SerializeField] private Button _rerollButton;
        [SerializeField] private TMP_Text _rerollNumberText;
        [SerializeField] private List<Sprite> _buttonSprites;

        public ClickUp[] Cards => _cards;

        /*        [SerializeField] private bool _canHaveSameIngredientInDeck;*/
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (TutorialManager.IsPresent())
            {
                RoundInfo roundInfo = TutorialManager.GetCurrentRoundInfoStatic();
                RedistributeCards(roundInfo.GetAllIngredients());
            }
            else
            {
                RedistributeCards();
            }

            UpdateButtonAppearance();
            // RedistributeCards();
        }
        /*        private void OnMouseDown()
                {
                    _spriteRenderer.color = new Color32(200, 200, 200, 255);
                    if (_rerollCount >= _rerollChance || _recipeDisplayScript.CookingManager.GetCurrentPhase() != PhaseCode.Phase1) return;

                    _rerollCount++;
                    ReRollBundle();
                    _OnReRoll?.Invoke();
                }
                private void OnMouseUp()
                {
                    _spriteRenderer.color = new Color32(255, 255, 255, 255);
                }*/


        public void Reroll()
        {
            if (OptionMenu.Instance.IsOptionPanelOpen || _isRerolling) return;
            if (RerollCount >= MaxRerollChances || _recipeDisplayScript.CookingManager.GetCurrentPhase() != PhaseCode.Phase1) return;

            if (TutorialManager.IsPresent())
            {
                if (TutorialManager.ShouldReroll())
                {
                    _monsterInstance.AddReroll();
                    TutorialManager.NextRoundStatic();
                    ReRollBundle(TutorialManager.GetReRolledIngredientsStatic());
                    StartCoroutine(DelayReRoll());
                    _onReRoll?.Invoke();
                    return;
                }
            }
            
            _monsterInstance.AddReroll();
            ReRollBundle();
            StartCoroutine(DelayReRoll());
            _onReRoll?.Invoke();
        }

        private IEnumerator DelayReRoll()
        {
            _isRerolling = true;
            yield return new WaitForSeconds(1);
            _isRerolling = false;
        }

        private void UpdateButtonAppearance()
        {
            if (TutorialManager.IsPresent())
            {
                _rerollButton.image.sprite = _buttonSprites[TutorialManager.GetCurrentRoundInfoStatic().ShouldReroll ? 1 : 0];
                _rerollNumberText.text = TutorialManager.GetCurrentRoundInfoStatic().ShouldReroll ? "1" : "0";

            }
            else
            {
                _rerollButton.image.sprite = _buttonSprites[RerollCount >= MaxRerollChances ? 0 : 1];
                
                _rerollNumberText.text = (MaxRerollChances - RerollCount).ToString();
            }
        }
        
        public void ReRollBundle()
        {
            ReRollBundle(_monsterInstance.GetIngredients());
        }

        public void ReRollBundle(IEnumerable<IngredientSo> ingredients)
        {
            List<IngredientSo> possibleIngredients = new List<IngredientSo>(ingredients);

            foreach (IngredientSo ingredientSo in GetSelectedIngredients())
            {
                int index = possibleIngredients.FindIndex(el => el == ingredientSo);
                if (index != -1) // V�rifiez si l'�l�ment a �t� trouv�
                {
                    possibleIngredients.RemoveAt(index);
                }
            }

            foreach (ClickUp clickUp in Cards)
            {
                if (clickUp.IsScaled) continue; // If not selected

                if (possibleIngredients.Count > 0) // V�rifiez si la liste contient encore des �l�ments
                {
                    int rIndex = Random.Range(0, possibleIngredients.Count);
                    clickUp.PassIngredient(possibleIngredients[rIndex]);
                    possibleIngredients.RemoveAt(rIndex); // Assurez-vous que cet index est valide
                }
                else
                {
                    // G�rez le cas o� il n'y a plus d'ingr�dients disponibles
                    Debug.LogWarning("Plus d'ingr�dients disponibles pour le re-roll");
                    break; // Sortez de la boucle si aucun ingr�dient n'est disponible
                }
            }
            UpdateButtonAppearance();
        }
        

        /*        public void ReRollBundle()
                {
                    List<IngredientSo> possibleIngredients = new List<IngredientSo>(_bundleSo.BundleIngredients);

                    foreach (IngredientSo ingredientSo in GetSelectedIngredients())
                    {
                        possibleIngredients.RemoveAt(possibleIngredients.FindIndex(el => el == ingredientSo));
                    }


                    foreach (ClickUp clickUp in _cards)
                    {
                        if (clickUp.IsScaled) continue; // If not selected

                        int rIndex = Random.Range(0, possibleIngredients.Count);

                        clickUp.PassIngredient(possibleIngredients[rIndex]);
                        possibleIngredients.RemoveAt(rIndex);
                    }
                }*/

        private List<IngredientSo> GetSelectedIngredients()
        {
            List<IngredientSo> list = new List<IngredientSo>(3);
        
            list.AddRange(from clickUp in Cards where clickUp.IsScaled select clickUp.Ingredient);

            return list;
        }

        [NaughtyAttributes.Button]
        public void ResetCards()
        {
            foreach (ClickUp clickUp in Cards)
            {
                clickUp.ResetCard();
            }
        }

        public void RedistributeCards()
        {
            RedistributeCards(_monsterInstance.GetIngredients());
        }
        
        public void RedistributeCards(IEnumerable<IngredientSo> ingredients)
        {
            List<IngredientSo> possibleIngredients = new List<IngredientSo>(ingredients);
            
            foreach (ClickUp clickUp in Cards)
            {
                if (clickUp.IsScaled)
                {
                    clickUp.DoClick();
                }
                
                if (possibleIngredients.Count > 0) // V�rifiez si la liste contient encore des �l�ments
                {
                    int rIndex = Random.Range(0, possibleIngredients.Count);
                    clickUp.PassIngredient(possibleIngredients[rIndex]);
                    possibleIngredients.RemoveAt(rIndex); // Assurez-vous que cet index est valide
                }
                else
                {
                    // G�rez le cas o� il n'y a plus d'ingr�dients disponibles
                    Debug.LogWarning("Plus d'ingr�dients disponibles pour le re-roll");
                    break; // Sortez de la boucle si aucun ingr�dient n'est disponible
                }
            }

            UpdateButtonAppearance();
        }
    }
}
