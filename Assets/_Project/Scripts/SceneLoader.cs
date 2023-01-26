using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    static public SceneLoader instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
