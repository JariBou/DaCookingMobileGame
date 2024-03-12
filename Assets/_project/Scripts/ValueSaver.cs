using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSaver : MonoBehaviour
{
    public static ValueSaver instance;
    [Header("Music")]
    public float MusicVolume;
    public bool IsMusicMuted;
    [Header("SFX")]
    public float SfxVolume;
    public bool IsSfxMuted;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
