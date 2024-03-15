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
        [SerializeField] private Sprite _nextMonsterSprite;
        [SerializeField] private Sprite _retrySprite;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LastPhaseScript _lastPhaseScript;
        [SerializeField] AudioSource _backgroundMusic;
        [SerializeField] AudioSource _winSound;
        [SerializeField] AudioSource _loseSound;
        private MonsterInstance _monsterInstance;


        private void Start()
        {
            _monsterInstance = GetComponent<MonsterInstance>();
        }

        public void ActivateMenu(bool won)
        {
            _actionButton.onClick.RemoveAllListeners();   
            _backgroundMusic.Stop();
            if (won)
            {
                _actionButton.onClick.AddListener(NextMonster);
                _actionButton.image.sprite = _nextMonsterSprite;
                _title.text = "Won";   
                
                _winSound.gameObject.SetActive(true);
                _winSound.Play();
            
            }
            else
            {
                _actionButton.onClick.AddListener(Retry);
                _actionButton.image.sprite = _retrySprite;
                _title.text = "Lost";  
                
                _loseSound.gameObject.SetActive(true);
                _loseSound.Play();
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
