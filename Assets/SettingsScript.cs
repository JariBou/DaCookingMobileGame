using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Image _muteMusicButton;
    [SerializeField] private List<Sprite> _musicIcons;
    private MMSoundManager _soundManager;
    private bool _isMusicMuted;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Image _muteSfxButton;
    [SerializeField] private List<Sprite> _sfxIcons;
    private bool _isSfxMuted;
    private float _musicVolume;
    private float _sfxVolume;
    [SerializeField] private ValueSaver _valueSaver;

    private const string MusicVolume = "music_volume";
    private const string SfxVolume = "sfx_volume";
    private const string MusicMuted = "music_muted";
    private const string SfxMuted = "sfx_muted";

    private void Awake()
    {
        _soundManager = FindFirstObjectByType<MMSoundManager>();
        try
        {
            _valueSaver = FindFirstObjectByType<ValueSaver>();
        }
        catch
        {
            Debug.LogError("ValueSaver not found");
        }

        try
        {
            _musicSlider.value = EditorPrefs.GetFloat(MusicVolume);
            _sfxSlider.value = EditorPrefs.GetFloat(SfxVolume);

            _isMusicMuted = EditorPrefs.GetBool(MusicMuted);
            _isSfxMuted = EditorPrefs.GetBool(SfxMuted);
        }
        catch (Exception e)
        {
            _musicSlider.value = 1;
            _sfxSlider.value = 1;

            _isMusicMuted = false;
            _isSfxMuted = false;
        }
        
        SetMusicVolume();
        SetSfxVolume();
        
        UpdateMusicButtonSprite();
        UpdateSfxButtonSprite();
    }

    public void UpdateMusicButtonSprite()
    {
        if (_isMusicMuted)
        {
            _muteMusicButton.sprite = _musicIcons[0];
            return;
        }
        
        switch (_musicSlider.value)
        {
            case > 0.66f:
                _muteMusicButton.sprite = _musicIcons[3];
                break;
            case > 0.33f:
                _muteMusicButton.sprite = _musicIcons[2];
                break;
            case > 0.01f:
                _muteMusicButton.sprite = _musicIcons[1];
                break;
            default:
                _muteMusicButton.sprite = _musicIcons[0];
                break;
        }
    }
    
    public void UpdateSfxButtonSprite()
    {
        if (_isSfxMuted)
        {
            _muteSfxButton.sprite = _sfxIcons[0];
            return;
        }
        
        switch (_sfxSlider.value)
        {
            case > 0.66f:
                _muteSfxButton.sprite = _sfxIcons[3];
                break;
            case > 0.33f:
                _muteSfxButton.sprite = _sfxIcons[2];
                break;
            case > 0.01f:
                _muteSfxButton.sprite = _sfxIcons[1];
                break;
            default:
                _muteSfxButton.sprite = _sfxIcons[0];
                break;
        }
    }

    public void SetMusicVolume()
    {
        _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _musicSlider.value);
        if (_musicSlider.value > _musicSlider.minValue && _isMusicMuted)
        {
            _soundManager.UnmuteMusic();
            _isMusicMuted = false;
        }
        else if (Math.Abs(_musicSlider.value - _musicSlider.minValue) < 0.01 && !_isMusicMuted)
        {
            _soundManager.MuteMusic();
            _isMusicMuted = true;

        }
        
        _musicVolume = _musicSlider.value;
        
        UpdateMusicButtonSprite();
        _valueSaver.MusicVolume = _musicVolume;
        
        EditorPrefs.SetBool(MusicMuted, _isMusicMuted);
        EditorPrefs.SetFloat(MusicVolume, _musicVolume);
    }
    
    public void MusicButton()
    {
        if (_isMusicMuted)
        {
            _isMusicMuted = false;
            _musicSlider.value = Math.Clamp(_musicVolume, 0.1f, 1f);
            _soundManager.UnmuteMusic();
        }
        else
        {
            _isMusicMuted = true;
            _musicVolume = _musicSlider.value;

            _soundManager.MuteMusic();
            _musicSlider.value = _musicSlider.minValue;
        }
        UpdateMusicButtonSprite();
        _valueSaver.IsMusicMuted = _isMusicMuted;
        
        EditorPrefs.SetBool(MusicMuted, _isMusicMuted);
    }

    public void SetSfxVolume()
    {
        _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _sfxSlider.value);
        if (_sfxSlider.value > _sfxSlider.minValue && _isSfxMuted)
        {
            _soundManager.UnmuteSfx();
            _isSfxMuted = false;
        }
        else if (Math.Abs(_sfxSlider.value - _sfxSlider.minValue) < 0.01 && !_isSfxMuted)
        {
            _soundManager.MuteSfx();
            _isSfxMuted = true;
        }
        
        _sfxVolume = _sfxSlider.value;
        
        UpdateSfxButtonSprite();
        _valueSaver.SfxVolume = _sfxVolume;
        
        EditorPrefs.SetBool(SfxMuted, _isSfxMuted);
        EditorPrefs.SetFloat(SfxVolume, _musicVolume);
    }
    
    public void SfxButton()
    {
        if (_isSfxMuted)
        {
            _isSfxMuted = false;
            _sfxSlider.value = Math.Clamp(_sfxVolume, 0.1f, 1f);
            _soundManager.UnmuteSfx();
        }
        else
        {
            _isSfxMuted = true;
            _sfxVolume = _sfxSlider.value;

            _soundManager.MuteSfx();
            _sfxSlider.value = _sfxSlider.minValue;
        }
        UpdateSfxButtonSprite();
        _valueSaver.IsSfxMuted = _isSfxMuted;
        
        EditorPrefs.SetBool(SfxMuted, _isSfxMuted);
    }
}
