using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            if (_valueSaver != null)
            {
                _isMusicMuted = _valueSaver.IsMusicMuted;
                _isSfxMuted = _valueSaver.IsSfxMuted;
                _musicSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _isMusicMuted);
                _sfxSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _isSfxMuted);
                _musicVolume = _valueSaver.MusicVolume;
                _sfxVolume = _valueSaver.SfxVolume;
            }
            SetMusicVolume();
            SetSfxVolume();
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
            _isOptionPanelOpen = false;
        }

        public void OpenSettingsPanel()
        {
            _settingsPanel.SetActive(true);
        }

        public void CloseSettingsPanel()
        {
            _settingsPanel.SetActive(false);
        }

        public void RestartRound()
        {
            Debug.Log("Restarting Round");
        }

        public void GoToMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public void SetMusicVolume()
        {
            _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _musicSlider.value);
            if (_musicSlider.value > _musicSlider.minValue && _isMusicMuted)
            {
                _soundManager.UnmuteMusic();
                _isMusicMuted = false;
            }
            else if (_musicSlider.value == _musicSlider.minValue && !_isMusicMuted)
            {
                _soundManager.MuteMusic();
                _isMusicMuted = true;

            }
            if (!_isMusicMuted)
            {
                _musicVolume = _musicSlider.value;
                _valueSaver.MusicVolume = _musicVolume;
            }
            _valueSaver.IsMusicMuted = _isMusicMuted;
        }


        public void MusicButton()
        {
            if (_isMusicMuted)
            {
                _isMusicMuted = false;
                _musicSlider.value = _musicVolume;
                _soundManager.UnmuteMusic();
            }
            else
            {
                _isMusicMuted = true;
                _musicVolume = _musicSlider.value;
                _valueSaver.MusicVolume = _musicVolume;

                _soundManager.MuteMusic();
                _musicSlider.value = _musicSlider.minValue;
            }
            _valueSaver.IsMusicMuted = _isMusicMuted;
        }


        public void SetSfxVolume()
        {
            _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _sfxSlider.value);
            if (_sfxSlider.value > _sfxSlider.minValue && _isSfxMuted)
            {
                _soundManager.UnmuteSfx();
                _isSfxMuted = false;
            }
            else if (_sfxSlider.value == _sfxSlider.minValue && !_isSfxMuted)
            {
                _soundManager.MuteSfx();
                _isSfxMuted = true;
            }
            if (!_isSfxMuted)
            {
                _sfxVolume = _sfxSlider.value;
                _valueSaver.SfxVolume = _sfxVolume;
            }
            _valueSaver.IsSfxMuted = _isSfxMuted;
        }

        public void SfxButton()
        {
            if (_isSfxMuted)
            {
                _isSfxMuted = false;
                _sfxSlider.value = _sfxVolume;
                _soundManager.UnmuteSfx();
            }
            else
            {
                _isSfxMuted = true;
                _sfxVolume = _sfxSlider.value;
                _valueSaver.SfxVolume = _sfxVolume;

                _soundManager.MuteSfx();
                _sfxSlider.value = _sfxSlider.minValue;
            }
            _valueSaver.IsSfxMuted = _isSfxMuted;
        }


    }
}
