using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class OptionMenu : MonoBehaviour
{
    public static OptionMenu instance;
    [SerializeField] private GameObject _optionPanel;
    [Header("Settings")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _musicSlider;
    private bool _isMusicMuted;
    public float MusicVolume;
    [SerializeField] private Slider _sfxSlider;
    private bool _isSfxMuted;
    public float SfxVolume;
    private MMSoundManager _soundManager;
    private ValueSaver _valueSaver;

    private void Awake()
    {
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
        
    }
    void Start()
    {   
        if (_valueSaver != null)
        {
            _isMusicMuted = _valueSaver.IsMusicMuted;
            _isSfxMuted = _valueSaver.IsSfxMuted;
            _musicSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _isMusicMuted);
            _sfxSlider.value = _soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _isSfxMuted);
        }
        SetMusicVolume();
        SetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOptionPanel()
    {
        
        _optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        _optionPanel.SetActive(false);
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
        SceneManager.LoadScene(0);
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
        _valueSaver.MusicVolume = _musicSlider.value;
        _valueSaver.IsMusicMuted = _isMusicMuted;
    }


        public void MusicButton()
        {
            if (_isMusicMuted)
            {
                _musicSlider.value = MusicVolume;
                _soundManager.UnmuteMusic();
            }
            else
            {
                MusicVolume = _musicSlider.value;
                _valueSaver.MusicVolume = MusicVolume;

                _soundManager.MuteMusic();
                _musicSlider.value = _musicSlider.minValue;
            }
            _isMusicMuted = !_isMusicMuted;
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
        _valueSaver.SfxVolume = _sfxSlider.value;
        _valueSaver.IsSfxMuted = _isSfxMuted;
    }

        public void SFXButton()
    {
        if (_isSfxMuted)
        {
            _soundManager.UnmuteSfx();
            _sfxSlider.value = SfxVolume;
        }
        else
        {
            SfxVolume = _sfxSlider.value;
            _valueSaver.SfxVolume = SfxVolume;

            _soundManager.MuteSfx();
            _sfxSlider.value = _sfxSlider.minValue;
        }
        _isSfxMuted = !_isSfxMuted;
        _valueSaver.IsSfxMuted = _isSfxMuted;
    }

    
}
