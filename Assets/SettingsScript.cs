// using System.Collections;
// using System.Collections.Generic;
// using MoreMountains.Tools;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class SettingsScript : MonoBehaviour
// {
//     [SerializeField] private Slider _musicSlider;
//     private MMSoundManager _soundManager;
//     private bool _isMusicMuted;
//     [SerializeField] private Slider _sfxSlider;
//     private bool _isSfxMuted;
//     private float _musicVolume;
//     private float _sfxVolume;
//
//     public void SetMusicVolume()
//     {
//         _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, _musicSlider.value);
//         if (_musicSlider.value > _musicSlider.minValue && _isMusicMuted)
//         {
//             _soundManager.UnmuteMusic();
//             _isMusicMuted = false;
//         }
//         else if (_musicSlider.value == _musicSlider.minValue && !_isMusicMuted)
//         {
//             _soundManager.MuteMusic();
//             _isMusicMuted = true;
//
//         }
//         if (!_isMusicMuted)
//         {
//             _musicVolume = _musicSlider.value;
//             _valueSaver.MusicVolume = _musicVolume;
//         }
//         _valueSaver.IsMusicMuted = _isMusicMuted;
//     }
//
//     public void UpdateMusicVolume(float value)
//     {
//         
//     }
//     
//     public void MusicButton()
//     {
//         if (_isMusicMuted)
//         {
//             _isMusicMuted = false;
//             _musicSlider.value = _musicVolume;
//             _soundManager.UnmuteMusic();
//         }
//         else
//         {
//             _isMusicMuted = true;
//             _musicVolume = _musicSlider.value;
//             _valueSaver.MusicVolume = _musicVolume;
//
//             _soundManager.MuteMusic();
//             _musicSlider.value = _musicSlider.minValue;
//         }
//         _valueSaver.IsMusicMuted = _isMusicMuted;
//     }
//
//     public void SetSFXVolume()
//     {
//         _soundManager.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, _sfxSlider.value);
//         if (_sfxSlider.value > _sfxSlider.minValue && _isSfxMuted)
//         {
//             _soundManager.UnmuteSfx();
//             _isSfxMuted = false;
//         }
//         else if (_sfxSlider.value == _sfxSlider.minValue && !_isSfxMuted)
//         {
//             _soundManager.MuteSfx();
//             _isSfxMuted = true;
//         }
//         if (!_isSfxMuted)
//         {
//             _sfxVolume = _sfxSlider.value;
//             _valueSaver.SfxVolume = _sfxVolume;
//         }
//         _valueSaver.IsSfxMuted = _isSfxMuted;
//     }
// }
