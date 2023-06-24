using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsController : MonoBehaviour
{
    // Volume Slider floats
    public Slider volume;
    public AudioSource sampleAudio;


    // Start

    void Start()
    {
        // Sets volume to last saved value
        volume.value = PlayerPrefs.GetFloat("musicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        // Updates Volume Slider
        volume.onValueChanged.AddListener(delegate { PlayAudio(); });
        PlayerPrefs.SetFloat("musicVolume", volume.value);
        sampleAudio.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    public void PlayAudio()
    {
        if (Math.Abs(value: volume.value - PlayerPrefs.GetFloat("musicVolume") ) > 0.1)
        {
            sampleAudio.Play(0);
        }
    }
 
  


}