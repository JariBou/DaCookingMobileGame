using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private OptionMenu _optionMenu;
        [SerializeField] private ValueSaver _valueSaver;
        [SerializeField] private Slider _musicSlider;
        private MMSoundManager _soundManager;
        private bool _isMusicMuted;
        [SerializeField] private Slider _sfxSlider;
        private bool _isSfxMuted;
        private float _musicVolume;
        private float _sfxVolume;

        private void Start()
        {
            try
            {
                _optionMenu = FindFirstObjectByType<OptionMenu>();
            }
            catch
            {
                Debug.LogError("OptionMenu not found");
            }
            try
            {
                _valueSaver = FindFirstObjectByType<ValueSaver>();
            }
            catch
            {
                Debug.LogError("ValueSaver not found");
            }

            if (_optionMenu != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("TempSoundManager"));
            }
            _soundManager = FindFirstObjectByType<MMSoundManager>();
            _musicSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _isMusicMuted);
            _sfxSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _isSfxMuted);
            if (_valueSaver != null)
            {
                _musicSlider.value = _valueSaver.MusicVolume;
                _sfxSlider.value = _valueSaver.SfxVolume;
                _isMusicMuted = _valueSaver.IsMusicMuted;
                _isSfxMuted = _valueSaver.IsSfxMuted;
            }


        }
        public void PlayGame()
        {
            if (_valueSaver != null)
            {
                _valueSaver.MusicVolume = _musicSlider.value;
                _valueSaver.SfxVolume = _sfxSlider.value;
                _valueSaver.IsMusicMuted = _isMusicMuted;
                _valueSaver.IsSfxMuted = _isSfxMuted;
            }
            SceneManager.LoadSceneAsync(1);
        }

        public void SetMusicVolume()
        {
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
            _valueSaver.IsMusicMuted = _isMusicMuted;
        }

        public void MusicButton()
        {
            if (_isMusicMuted)
            {
                _optionMenu?.MusicButton();
                _musicSlider.value = _musicVolume;
            }
            else
            {
                _musicVolume = _musicSlider.value;
                _optionMenu?.MusicButton();
                _musicSlider.value = _musicSlider.minValue;
            }
            _isMusicMuted = !_isMusicMuted;
        }

        public void SetSFXVolume()
        {
            _soundManager.SetVolumeSfx(_sfxSlider.value);
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
            _valueSaver.IsSfxMuted = _isSfxMuted;
        }

        public void SFXButton()
        {
            if (_isSfxMuted)
            {
                _optionMenu?.SFXButton();
                _sfxSlider.value = _sfxVolume;
            }
            else
            {
                _sfxVolume = _sfxSlider.value;
                _optionMenu?.SFXButton();
                _sfxSlider.value = _sfxSlider.minValue;
            }
            _isSfxMuted = !_isSfxMuted;
        }




        /*public void QuitGame()
        {
            Application.Quit();
        }*/


    }
}
