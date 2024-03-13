using _project.Scripts.Phases;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class DialogMenuScript : MonoBehaviour
    {
        [SerializeField] private GameObject _menuGameObject;
        [SerializeField] private Button _actionButton;
        [SerializeField] private TMP_Text _actionButtonText;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LastPhaseScript _lastPhaseScript;
        private MonsterInstance _monsterInstance;


        private void Start()
        {
            _monsterInstance = GetComponent<MonsterInstance>();
        }

        public void ActivateMenu(bool won)
        {
            _actionButton.onClick.RemoveAllListeners();
            if (won)
            {
                _actionButton.onClick.AddListener(NextMonster);
                _actionButtonText.text = "Next Monster";
                _title.text = "Won";
            }
            else
            {
                _actionButton.onClick.AddListener(Retry);
                _actionButtonText.text = "Retry";
                _title.text = "Lost";
            }
        
            _menuGameObject.SetActive(true);
        }


        public void NextMonster()
        {   
            _lastPhaseScript.EndFeedingPhase();
        
            // TODO
            // Add change of monster
            _monsterInstance.NewRandomMonster();
        
            _menuGameObject.SetActive(false);
        }

        public void Retry()
        {
            // TODO
            // Add reset of monster
            _monsterInstance.RetryMonster();
            _menuGameObject.SetActive(false);
        }

        [Button]
        private void Win()
        {
            ActivateMenu(true);
        }

        [Button]
        private void Lost()
        {
            ActivateMenu(false);
        }

        public void Menu()
        {
            SceneManager.LoadSceneAsync(0);
        }
    
    }
}
