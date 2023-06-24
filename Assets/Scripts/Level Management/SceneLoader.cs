using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public bool autoLoad;
    private float timer;

    void Start()
    {
        if (autoLoad)
        {
            timer = (Time.time + 5);
        }       
    
    }

    void Update()
    {
        if (Time.time > timer && autoLoad)
        {
            LoadSpecificScene("MenuScene");
        }

    }


    public void LoadSpecificScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitScene()
    {
        Application.Quit();
    }


}
