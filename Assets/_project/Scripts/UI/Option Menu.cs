using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace _project.Scripts.UI
{
    public class OptionMenu : MonoBehaviour
    {
        public static OptionMenu Instance;
        [SerializeField] private GameObject _optionPanel;
        [FormerlySerializedAs("SettingsButton")] [Header("Settings")]
        public Image _settingsButton;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _creditsPanel;
        [SerializeField] private Slider _musicSlider;
        private bool _isMusicMuted;
        [FormerlySerializedAs("MusicVolume")] public float _musicVolume;
        [SerializeField] private Slider _sfxSlider;
        private bool _isSfxMuted;
        [FormerlySerializedAs("SfxVolume")] public float _sfxVolume;
        private MMSoundManager _soundManager;
        private ValueSaver _valueSaver;

        [FormerlySerializedAs("IsOptionPanelOpen")] public bool _isOptionPanelOpen;
        private DragAndDrop _dragAndDrop;

        [FormerlySerializedAs("BackgroundOptionLayer")] public LayerMask _backgroundOptionLayer;

        public bool IsOptionPanelOpen => _isOptionPanelOpen;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            try
            {
                _valueSaver = FindFirstObjectByType<ValueSaver>();
            }
            catch
            {
                Debug.LogError("ValueSaver not found");
            }
            _soundManager = FindFirstObjectByType<MMSoundManager>();
            if (_soundManager == null)
            {
                gameObject.AddComponent<MMSoundManager>();
                _soundManager = FindFirstObjectByType<MMSoundManager>();
            }

            _dragAndDrop = gameObject.GetComponent<DragAndDrop>();

        }

        void Start()
        {
            // if (_valueSaver != null)
            // {
            //     _isMusicMuted = _valueSaver.IsMusicMuted;
            //     _isSfxMuted = _valueSaver.IsSfxMuted;
            //     _musicSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _isMusicMuted);
            //     _sfxSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _isSfxMuted);
            //     _musicVolume = _valueSaver.MusicVolume;
            //     _sfxVolume = _valueSaver.SfxVolume;
            // }
        }

        // Update is called once per frame


        public void OpenOptionPanel()
        {
            if (!IsOptionPanelOpen)
            {
                _optionPanel.SetActive(true);
                _isOptionPanelOpen = true;
                _settingsButton.raycastTarget = false;
            }

        }

        public void CloseOptionPanel()
        {
            _optionPanel.SetActive(false);
            CloseSettingsPanel();
            CloseCreditsPanel();
            _isOptionPanelOpen = false;
        }

        public void OpenSettingsPanel()
        {
            _settingsPanel.SetActive(true);
        }
        
        public void OpenCreditsPanel()
        {
            _creditsPanel.SetActive(true);
        }

        public void CloseSettingsPanel()
        {
            _settingsPanel.SetActive(false);
        }
        
        public void CloseCreditsPanel()
        {
            _creditsPanel.SetActive(false);
        }

        public void GoToMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

    }
}
