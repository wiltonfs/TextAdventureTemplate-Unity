using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsLoader : MonoBehaviour
{
    // Initializes Audio Source
    public AudioSource myMusic;

    // Start 

    void Start()
    {
        // Sets Volume
        myMusic.volume = PlayerPrefs.GetFloat("musicVolume");
    }

}
