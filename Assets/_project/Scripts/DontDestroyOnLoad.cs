using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] objects;
    public static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance != null)
        {
            foreach (var element in objects)
            {
                Destroy(element);
            }
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            foreach (var element in objects)
            {
                DontDestroyOnLoad(element);
            }
            DontDestroyOnLoad(gameObject);
        }

    }

}
