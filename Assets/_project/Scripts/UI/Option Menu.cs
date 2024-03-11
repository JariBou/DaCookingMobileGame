using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionPanel;
    [Header("Settings")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _musicSlider;
    private bool _isMusicMuted;
    private float _musicVolume;
    [SerializeField] private Slider _sfxSlider;
    private bool _isSfxMuted;
    private float _sfxVolume;
    [SerializeField] private MMSoundManager _soundManager;
    void Start()
    {
        /*SetMusicVolume();
        SetSFXVolume();*/
        Debug.Log(_soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, false));
        Debug.Log(_soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, false));
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
    }

    public void MusicButton()
    {
        if (_isMusicMuted)
        {
            _soundManager.UnmuteMusic();
            _musicSlider.value = _musicVolume;
        }
        else
        {
            _musicVolume = _musicSlider.value;
            _soundManager.MuteMusic();
            _musicSlider.value = _musicSlider.minValue;
        }
        _isMusicMuted = !_isMusicMuted;
    }


    public void SetSFXVolume()
    {
        _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _sfxSlider.value);
    }

    public void SFXButton()
    {
        if (_isSfxMuted)
        {
            _soundManager.UnmuteSfx();
            _sfxSlider.value = _sfxVolume;
        }
        else
        {
            _sfxVolume = _sfxSlider.value;
            _soundManager.MuteSfx();
            _sfxSlider.value = _sfxSlider.minValue;
        }
        _isSfxMuted = !_isSfxMuted;
    }
}
