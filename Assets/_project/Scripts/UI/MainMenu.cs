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
            if (_valueSaver != null)
            {
                _isMusicMuted = _valueSaver.IsMusicMuted;
                _isSfxMuted = _valueSaver.IsSfxMuted;
                _musicVolume = _valueSaver.MusicVolume;
                _sfxVolume = _valueSaver.SfxVolume;
            }
            _soundManager = FindFirstObjectByType<MMSoundManager>();
            _musicSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _isMusicMuted);
            _sfxSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _isSfxMuted);

        }
        public void PlayGame()
        {
            if (_valueSaver != null)
            {
                _valueSaver.MusicVolume = _musicVolume;
                _valueSaver.SfxVolume = _sfxVolume;
                _valueSaver.IsMusicMuted = _isMusicMuted;
                _valueSaver.IsSfxMuted = _isSfxMuted;
            }
            SceneManager.LoadSceneAsync(1);
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

        public void SetSFXVolume()
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




        /*public void QuitGame()
        {
            Application.Quit();
        }*/


    }
}
